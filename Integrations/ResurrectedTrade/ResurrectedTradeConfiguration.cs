using Microsoft.Win32;
using ResurrectedTrade.AgentBase;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MapAssist.Integrations.ResurrectedTrade
{
    public partial class ResurrectedTradeConfiguration : UserControl
    {
        private readonly ResurrectedTradeIntegration _integration;

        public ResurrectedTradeConfiguration(ResurrectedTradeIntegration integration)
        {
            _integration = integration;
            InitializeComponent();
            UpdateVisible();
            Username.Text = Utils.AgentRegistryKey.GetValue("USERNAME", "") as string;
            Password.Text = Utils.AgentRegistryKey.GetValue("PASSWORD", "") as string;

            //var check = Utils.AgentRegistryKey.GetValue("AUTOLOGIN", false);

            //if (check == null || (bool)check == false)
            //{
            //    checkBoxAutoLogin.Checked = false;
            //}
            //else
            //{
            //    checkBoxAutoLogin.Checked = true;
            //    // login now
            //    var task = Task.Run(async () =>
            //    {
            //        var response = await _integration.Login(Username.Text, Password.Text);
            //    });
            //}
        }

        public void UpdateVisible()
        {
            var loggedIn = _integration.Profile != null;
            UsernameLabel.Visible = !loggedIn;
            Username.Visible = !loggedIn;
            PasswordLabel.Visible = !loggedIn;
            Password.Visible = !loggedIn;
            LoginButton.Visible = !loggedIn;
            RegisterButton.Visible = !loggedIn;
            LogoutButton.Visible = loggedIn;
            ViewOnlineButton.Visible = loggedIn;
            if (loggedIn)
            {
                StatusLabel.Text = $"用户： {_integration.Profile.UserId}. 版本: {typeof(Runner).Assembly.GetName().Version?.ToString() ?? "Unknown"}";
            }
            else
            {
                StatusLabel.Text = "";
            }
        }

        private void ChangeControlEnablement(bool enabled)
        {
            LoginButton.Enabled = enabled;
            Username.ReadOnly = !enabled;
            Password.ReadOnly = !enabled;
            LogoutButton.Enabled = enabled;
        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            StatusLabel.Text = "";
            ChangeControlEnablement(false);
            var response = await _integration.Login(Username.Text, Password.Text);
            ChangeControlEnablement(true);
            
            if (!response.Success)
            {
                if (response.Errors.Any(o => o.Code == "LoginFailed"))
                {
                    StatusLabel.Text = "用户名或者密码错误";
                }
                else
                {
                    StatusLabel.Text = string.Join(", ", response.Errors.Select(o => o.Description));
                    
                }
            }
            else
            {
                // save password for next
                Utils.AgentRegistryKey.SetValue("PASSWORD", Password.Text, RegistryValueKind.String);
                Password.Text = "";
                UpdateVisible();
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            Utils.OpenUrl("https://resurrected.trade/auth?t=1");
        }

        private async void LogoutButton_Click(object sender, EventArgs e)
        {
            StatusLabel.Text = "";
            ChangeControlEnablement(false);
            var response = await _integration.Logout();
            ChangeControlEnablement(true);
            UpdateVisible();
            if (!response.Success)
            {
                StatusLabel.Text = string.Join(", ", response.Errors.Select(o => o.Description));
            }
        }

        private void Inputs_TextChanged(object sender, EventArgs e)
        {
            LoginButton.Enabled = Username.Text.Length > 0 && Password.Text.Length > 0;
            Utils.AgentRegistryKey.SetValue("USERNAME", Username.Text, RegistryValueKind.String);
        }

        private void Inputs_KeyPress(object sender, KeyPressEventArgs e)
        {
            var textBox = sender as TextBox;
            if (e.KeyChar == (char)Keys.Enter && LoginButton.Enabled)
            {
                LoginButton.PerformClick();
                textBox?.Focus();
            }
        }

        private void ViewOnlineButton_Click(object sender, EventArgs e)
        {
            _integration.OpenOverview();
        }

        private void checkBoxAutoLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAutoLogin.Checked)
                Utils.AgentRegistryKey.SetValue("AUTOLOGIN", 0, RegistryValueKind.Binary);
            else
                Utils.AgentRegistryKey.SetValue("AUTOLOGIN", 1, RegistryValueKind.Binary);
        }
    }
}
