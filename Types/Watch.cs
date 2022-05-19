using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Diagnostics;
using MapAssist.Settings;
using System.Threading;
using MapAssist.Helpers;
using System.Runtime.Serialization;
using MapAssist.Interfaces;

namespace MapAssist.Types
{
    public class Watch
    {
        private int last_life;

        public Watch()
        {
        }

        public void Check(GameData g)
        {
            if (!MapAssistConfiguration.Loaded.HC.WatchLifeEnabled)
                return;

            var LifeLeft = MapAssistConfiguration.Loaded.HC.WatchLifeLeft;

            LifeLeft = MapAssistConfiguration.Loaded.HC.WatchLifeLeft;
            try
            {
                var curlife = g?.PlayerUnit?.LifePercentage;
                if (curlife != null)
                    if (curlife < LifeLeft  && last_life != (int)curlife)
                    {
                        WindowsExternal.SetForegroundWindow(g.MainWindowHandle);
                        WindowsExternal.SendEscapeKey(g.MainWindowHandle);
                        var windowRect = WindowsExternal.GetWindowRect(g.MainWindowHandle);

                        var width = windowRect.Right - windowRect.Left;
                        var height = windowRect.Bottom - windowRect.Top;
                        WindowsExternal.LeftMouseClick(windowRect.Left + width / 2, windowRect.Top + height / 20 * 9);
                        last_life = (int)curlife;
                    }
            }
            catch { }
        }
    }
}
