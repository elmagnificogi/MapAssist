using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using MapAssist.Helpers;
using Microsoft.Win32;
using NLog.Fluent;
using ResurrectedTrade.AgentBase;
using ResurrectedTrade.AgentBase.Memory;
using ResurrectedTrade.Protocol.Agent;
using ResurrectedTrade.Protocol.Profile;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapAssist.Integrations.ResurrectedTrade
{
    enum State
    {
        Ok,
        Unauthenticated,
        Down
    }

    public class ResurrectedTradeIntegration : IIntegration, IOffsets, ILogger
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private static readonly ResolvablePattern
            SessionDataPattern = new ResolvablePattern("48 8D 2D ? ? ? ? 48 8D 44 24", 3);

        private static readonly ResolvablePattern PetListPattern = new ResolvablePattern(
            "48 8B 05 ? ? ? ? 8B 4A 08 48 85 C0 74 14", 3
        );


        private static readonly ResolvablePattern IsOnlineGamePattern = new ResolvablePattern(
            "8B 0D ? ? ? ? 48 8D 15 ? ? ? ? 83 F9 01 48 8D 05 ? ? ? ? 48 0F 45 C2 33 D2", 2
        );

        private static readonly ResolvablePattern WidgetStatesPattern =
            new ResolvablePattern("48 8B 0D ? ? ? ? 48 C7 C7", 3);

        private static readonly ResolvablePattern CharFlagsPattern = new ResolvablePattern("48 8B 2D ? ? ? ? 8B FE", 3);

        private bool _initialized;
        private bool _alertedAboutReloginOnce;

        private CookieContainer _cookieContainer = new CookieContainer();
        private AgentService.AgentServiceClient _agentService;
        private ProfileService.ProfileServiceClient _profileService;

        public Profile Profile { get; private set; }
        private Runner _runner;
        private bool _firstRun;
        private bool _debugLog;
        private bool _paused;
        private string _lastBattleTag;
        private string _lastCharacter;
        private DateTime _nextExport = DateTime.MaxValue;
        private int _nextExportPid;
        private State _state = State.Ok;
        private readonly BlockingCollection<Export> _exportQueue = new BlockingCollection<Export>();
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();
        private readonly SynchronizationContext _ctx;
        private readonly NotifyIcon _trayIcon;
        private readonly ToolStripItem[] _contextMenuItems;
        private readonly ResurrectedTradeConfiguration _config;

        public ResurrectedTradeIntegration(SynchronizationContext ctx, NotifyIcon trayIcon)
        {
            _ctx = ctx;
            _trayIcon = trayIcon;
            var version = typeof(Runner).Assembly.GetName().Version?.ToString();

            var sync = Utils.AgentRegistryKey.GetValue("SYNC", false);

            _contextMenuItems = new ToolStripItem[]
            {
                new ToolStripMenuItem("等待游戏...") { Enabled = false }, new ToolStripSeparator(),
                new ToolStripMenuItem("在线查看", null, (sender, args) => OpenOverview()),
                new ToolStripMenuItem(
                    "同步中", null, (sender, args) =>
                    {
                        _nextExport = DateTime.Now;
                    }
                ) { Visible = false },
                new ToolStripMenuItem("版本")
                {
                    DropDownItems =
                    {
                        new ToolStripMenuItem(version)
                        {
                            Enabled = false
                        },
                        new ToolStripMenuItem(
                            "Debug Logging", null, (sender, args) =>
                            {
                                _debugLog = !_debugLog;
                                (sender as ToolStripMenuItem).Checked = _debugLog;
                            }
                        ),
                    }
                },
                new ToolStripMenuItem(
                    "停止同步", null, (sender, args) =>
                    {
                        _paused = !_paused;
                        if( _paused)
                            Utils.AgentRegistryKey.SetValue("SYNC", 0, RegistryValueKind.DWord);
                        else
                            Utils.AgentRegistryKey.SetValue("SYNC", 1, RegistryValueKind.DWord);
                        (sender as ToolStripMenuItem).Checked = _paused;
                    }
                ),
            };

            var a = (ToolStripMenuItem)_contextMenuItems[_contextMenuItems.Length - 1];
            a.Checked = !Convert.ToBoolean(sync);

            _cookieContainer = Utils.LoadCookieContainer();
            var handler = new HttpClientHandler
            {
                UseCookies = true, CookieContainer = _cookieContainer
            };
            var client = new HttpClient(new GrpcWebHandler(handler));
            client.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue(
                    new ProductHeaderValue(
                        (GetType().Assembly.GetName().Name ?? "Unknown").Replace(" ", ""),
                        (GetType().Assembly.GetName().Version?.ToString() ?? "Unknown") + "-" +
                        (typeof(Runner).Assembly.GetName().Version?.ToString() ?? "Unknown")
                    )
                )
            );

            var grpcChannel = GrpcChannel.ForAddress(
                Utils.ApiAddress,
                new GrpcChannelOptions { HttpClient = client, MaxRetryAttempts = 3, }
            );

            _profileService = new ProfileService.ProfileServiceClient(grpcChannel);
            _agentService = new AgentService.AgentServiceClient(grpcChannel);
            _runner = new Runner(this, this, _agentService);
            _firstRun = (int)Utils.AgentRegistryKey.GetValue("RAN_BEFORE", 0) == 0;
            _config = new ResurrectedTradeConfiguration(this);

            Logger.Info($"Initialized with address {Utils.ApiAddress}, {nameof(_firstRun)}: {_firstRun}");
            foreach (ProductInfoHeaderValue productInfoHeaderValue in client.DefaultRequestHeaders.UserAgent)
            {
                Logger.Info($"User agent: {productInfoHeaderValue}");
            }

            _ = TryFetchProfile();
        }

        public void OpenOverview()
        {
            if (string.IsNullOrWhiteSpace(_lastBattleTag) || string.IsNullOrWhiteSpace(_lastCharacter))
            {
                Utils.OpenUrl("https://resurrected.trade/overview");
            }
            else
            {
                Utils.OpenUrl($"https://resurrected.trade/overview?viewType=0&account={_lastBattleTag}&character={_lastCharacter}");
            }
        }

        private void ResetState()
        {
            Profile = null;
            SetContextMenuItemVisibility("Sync now", false);
            UIThread(() => _config.UpdateVisible());
            _exportQueue.Add(null); // Wake up export thread to realise we are no longer logged in.
        }

        private void UIThread(Action action)
        {
            _ctx.Send(
                _ =>
                {
                    action();
                }, null
            );
        }

        private void SetContextMenuItemVisibility(string itemText, bool visibility)
        {
            var entry = _contextMenuItems.FirstOrDefault(o => o.Text == itemText);
            if (entry != null)
            {
                UIThread(() => entry.Visible = visibility);
            }
        }

        public UserControl ConfigurationControls => _config;

        public void Initialize(byte[] buffer, ProcessContext context)
        {
            if (_initialized) return;

            UnitHashTable = (Ptr)GameManager.UnitHashTableOffset - context.BaseAddr;
            SessionData = FindAndResolvePattern(buffer, context, SessionDataPattern);
            Pets = FindAndResolvePattern(buffer, context, PetListPattern);
            UIState = (Ptr)GameManager.MenuDataOffset - context.BaseAddr;
            IsOnlineGame = FindAndResolvePattern(buffer, context, IsOnlineGamePattern);
            WidgetStates = FindAndResolvePattern(buffer, context, WidgetStatesPattern);
            CharFlags = FindAndResolvePattern(buffer, context, CharFlagsPattern);

            var allOffsets = new Dictionary<string, Ptr>
            {
                [nameof(UnitHashTable)] = UnitHashTable,
                [nameof(SessionData)] = SessionData,
                [nameof(Pets)] = Pets,
                [nameof(UIState)] = UIState,
                [nameof(IsOnlineGame)] = IsOnlineGame,
                [nameof(WidgetStates)] = WidgetStates,
                [nameof(CharFlags)] = CharFlags
            };

            var allValid = true;
            foreach (KeyValuePair<string, Ptr> keyValuePair in allOffsets)
            {
                var valid = true;
                try
                {
                    context.Read<Ptr>(keyValuePair.Value + context.BaseAddr);
                }
                catch (Exception)
                {
                    valid = false;
                    allValid = false;
                }

                Logger.Info($"Found {keyValuePair.Key}: {keyValuePair.Value} valid: {valid}");
            }

            if (!allValid)
            {
                Logger.Warn("Skipping initialization as something is not quite right...");
                return;
            }

            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += ExportLoop;
            _backgroundWorker.RunWorkerAsync();
            _initialized = true;
        }

        private async void ExportLoop(object sender, DoWorkEventArgs args)
        {
            if (Thread.CurrentThread.Name == null)
            {
                Thread.CurrentThread.Name = Name;
            }

            try
            {
                while (true)
                {

                    if (_paused)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                        

                    try
                    {
                        Logger.Info("Trying to acquire single instance mutex");
                        if (!Utils.SingleInstanceMutex.WaitOne(TimeSpan.FromMinutes(1), false))
                        {
                            Logger.Info("Failed to acqurie single instance mutex");
                            continue;
                        }
                    }
                    catch (AbandonedMutexException)
                    {
                    }

                    Logger.Info("Acquired single instance mutex");
                    break;
                }

                while (!_backgroundWorker.CancellationPending)
                {
                    if (_paused)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }

                    if (Profile == null)
                    {
                        await WaitForProfile();
                        if (_state == State.Ok)
                        {
                            _nextExport = DateTime.MinValue;
                        }

                        continue;
                    }

                    if (!_runner.IsInitialized())
                    {
                        Logger.Info("Initializing runner...");
                        await _runner.Initialize();
                    }

                    var export = _exportQueue.Take();
                    if (export == null) continue;
                    try
                    {
                        var outcome = await _runner.SubmitExport(export);
                        if (!outcome.Attempted)
                        {
                            // Debounced, suggest we want to get another export straight away.
                            _nextExport = DateTime.MinValue;
                            continue;
                        }

                        Utils.SaveCookieContainer(_cookieContainer);

                        if (outcome.CooldownMilliseconds > 0)
                        {
                            _nextExport = DateTime.Now + TimeSpan.FromMilliseconds(outcome.CooldownMilliseconds);
                        }
                        else
                        {
                            // Should not happen...
                            _nextExport = DateTime.Now + TimeSpan.FromSeconds(30);
                        }

                        if (!outcome.Success)
                        {
                            Logger.Warn($"Failed to export: {outcome.ErrorId}: {outcome.ErrorMessage}");
                        }
                    }
                    catch (Exception e)
                    {
                        if (Utils.IsStatusCodeException(e, StatusCode.Unauthenticated))
                        {
                            Utils.SaveCookieContainer(_cookieContainer);
                            ResetState();
                            continue;
                        }

                        if (Utils.IsStatusCodeException(e, StatusCode.Unavailable))
                        {
                            ResetState();
                            continue;
                        }

                        Logger.Error($"Got exception doing export: {e}");
                    }
                }
            }
            finally
            {
                Utils.SingleInstanceMutex.ReleaseMutex();
            }
        }

        private async Task<State> TryFetchProfile()
        {
            if (Utils.HasValidCookie(_cookieContainer))
            {
                try
                {
                    Profile = await _profileService.GetProfileAsync(new Empty());
                    SetContextMenuItemVisibility("Sync now", true);
                    Utils.SaveCookieContainer(_cookieContainer);
                    UIThread(
                        () =>
                        {
                            _contextMenuItems[0].Text = Profile.UserId;
                        }
                    );
                    return State.Ok;
                }
                catch (Exception e)
                {
                    if (Utils.IsStatusCodeException(e, StatusCode.Unauthenticated))
                    {
                        return State.Unauthenticated;
                    }

                    if (Utils.IsStatusCodeException(e, StatusCode.Unavailable))
                    {
                        return State.Down;
                    }

                    Logger.Error($"Got exception retrieving profile: {e}");
                    return State.Down;
                }
            }

            return State.Unauthenticated;
        }

        private async Task WaitForProfile()
        {
            Logger.Info($"Entering profile wait loop while in state {_state}");

            while (Profile == null && !_backgroundWorker.CancellationPending)
            {
                if (_paused)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                var newState = await TryFetchProfile();
                if (newState == State.Ok && Profile != null)
                {
                    Logger.Info("Retrieved profile");
                    UIThread(() => _config.UpdateVisible());
                    _state = newState;
                    return;
                }

                if (newState == State.Down)
                {
                    UIThread(
                        () =>
                        {
                            _contextMenuItems[0].Text = "Service unavailable";
                        }
                    );
                }
                else if (newState == State.Unauthenticated)
                {
                    UIThread(
                        () =>
                        {
                            _contextMenuItems[0].Text = "Logged out";
                        }
                    );
                }

                if (_state != newState && newState == State.Unauthenticated)
                {
                    if (_firstRun)
                    {
                        _trayIcon.ShowBalloonTip(
                            30, "Resurrected Trade", "Map Assist now comes with resurrected.trade integration, " +
                                                     "you can configure it in the integration tab in the configuration dialog",
                            ToolTipIcon.Info
                        );
                        Utils.AgentRegistryKey.SetValue("RAN_BEFORE", 1, RegistryValueKind.DWord);
                    }
                    else
                    {
                        MaybeAlertAboutRelogin();
                    }
                }

                if (_state != newState)
                {
                    Logger.Info($"Transitioning from {_state} to {newState}");
                }

                _state = newState;
                await Task.Delay(10000);
            }
        }

        private void MaybeAlertAboutRelogin()
        {
            // This will only be shown once, on startup.
            var loggedInBefore = (int)Utils.AgentRegistryKey.GetValue("PREVIOUSLY_LOGGED_IN", 0) != 0;
            if (!_alertedAboutReloginOnce && loggedInBefore)
            {
                _alertedAboutReloginOnce = true;
                _trayIcon.ShowBalloonTip(
                    30, "Resurrected Trade",
                    "You need to re-login into resurrected.trade if you wish to continue using it.", ToolTipIcon.Warning
                );
            }
        }

        private Ptr FindAndResolvePattern(byte[] buffer, ProcessContext context, ResolvablePattern pattern)
        {
            var patternAddress = (Ptr)context.FindPattern(buffer, pattern);
            if (patternAddress == Ptr.Zero) return Ptr.Zero;
            var patternOffset = patternAddress - context.BaseAddr;
            return pattern.Resolve(buffer, patternOffset);
        }


        public void Run(ProcessContext context)
        {
            if (!_initialized || Profile == null || !_runner.IsInitialized() ||
                (_nextExportPid == context.ProcessId && _nextExport > DateTime.Now)) return;
            var export = _runner.GetExport(context.Handle, context.BaseAddr);
            if (export == null) return;
            // Prevent posting more exports until the loop submitting the exports have adjusted the time.
            _nextExport = DateTime.MaxValue;
            // Update the last process id.
            _nextExportPid = context.ProcessId;
            _exportQueue.Add(export);
            if (export.Characters.Any(o => !o.Name.StartsWith("_")))
            {
                _lastBattleTag = export.BattleTag;
                _lastCharacter = export.Characters.Select(o => o.Name).FirstOrDefault(o => !o.StartsWith("_"));
            }
        }

        public async Task<AuthResponse> Login(string username, string password)
        {
            try
            {
                var response =
                    await _profileService.LoginAsync(new LoginRequest { UserId = username, Password = password });
                if (response.Success)
                {
                    Utils.SaveCookieContainer(_cookieContainer);
                    Logger.Info("Successfully logegd in");
                    var state = await TryFetchProfile();
                    if (state != State.Ok)
                    {
                        Logger.Info($"Failed to look up profile: {state}");
                        return new AuthResponse
                        {
                            Success = false,
                            Errors =
                            {
                                new AuthError
                                {
                                    Code = "NoProfile", Description = "Failed to fetch profile after login."
                                }
                            }
                        };
                    }
                    Logger.Info($"Successfully looked up profile: {Profile?.UserId}");
                    Utils.AgentRegistryKey.SetValue(
                        "PREVIOUSLY_LOGGED_IN", 1, RegistryValueKind.DWord
                    );
                    _alertedAboutReloginOnce = false;
                }

                return response;
            }
            catch (Exception e)
            {
                Logger.Error($"Failed to log in: {e}");
                return new AuthResponse
                {
                    Success = false,
                    Errors =
                    {
                        new AuthError
                        {
                            Code = "UnknownError", Description = "Failed to log in, check the logs"
                        }
                    }
                };
            }
        }

        public async Task<AuthResponse> Logout()
        {
            // Not to bother with notifications about needing to log in if the user willingly logged out.
            Utils.AgentRegistryKey.SetValue(
                "PREVIOUSLY_LOGGED_IN", 0, RegistryValueKind.DWord
            );
            _alertedAboutReloginOnce = true;
            AuthResponse response;
            try
            {
                response = await _profileService.LogoutAsync(new Empty());
            }
            catch (Exception e)
            {
                if (Utils.IsStatusCodeException(e, StatusCode.Unauthenticated))
                {
                    response = new AuthResponse { Success = true };
                }
                else
                {
                    response = new AuthResponse { Success = false };
                }
            }

            if (response.Success)
            {
                Logger.Info("Successfully logged out");
                ResetState();
            }

            Utils.SaveCookieContainer(_cookieContainer);
            return response;
        }

        public Ptr UnitHashTable { get; set; }
        public Ptr SessionData { get; set; }
        public Ptr Pets { get; set; }
        public Ptr UIState { get; set; }
        public Ptr IsOnlineGame { get; set; }
        public Ptr WidgetStates { get; set; }
        public Ptr CharFlags { get; set; }

        public string Name => "仓库同步";

        public ToolStripItem[] ContextMenuItems => _contextMenuItems;
        public void Info(string message)
        {
            Logger.Info(message);
        }

        public void Debug(string message)
        {
            if (_debugLog)
            {
                Logger.Debug(message);
            }
        }
    }
}
