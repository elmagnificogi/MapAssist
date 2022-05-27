using MapAssist.Helpers;
using MapAssist.Interfaces;
using MapAssist.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MapAssist.Types
{
    public class Watch
    {
        private int lastLife;
        private int lastMana;
        private GameData gameData;

        public Watch()
        {
        }

        private void QuitGame()
        {
            WindowsExternal.SetForegroundWindow(gameData.MainWindowHandle);
            WindowsExternal.SendEscapeKey(gameData.MainWindowHandle);
            var windowRect = WindowsExternal.GetWindowRect(gameData.MainWindowHandle);

            var width = windowRect.Right - windowRect.Left;
            var height = windowRect.Bottom - windowRect.Top;
            WindowsExternal.LeftMouseClick(windowRect.Left + width / 2, windowRect.Top + height / 20 * 9);
        }

        private void CheckLife()
        {
            if (!MapAssistConfiguration.Loaded.HC.WatchLifeEnabled)
                return;

            var LifeLeft = MapAssistConfiguration.Loaded.HC.WatchLifeLeft;

            try
            {
                var curlife = gameData.PlayerUnit?.LifePercentage;
                var inTown = AreaExtensions.IsTown(gameData.Area);
                if (inTown)
                    return;
                if (curlife != null)
                    if (curlife < LifeLeft && lastLife != (int)curlife)
                    {
                        QuitGame();
                        lastLife = (int)curlife;
                    }
            }
            catch { }
        }

        private void CheckMana()
        {
            if (!MapAssistConfiguration.Loaded.HC.WatchManaEnabled)
                return;

            var ManaLeft = MapAssistConfiguration.Loaded.HC.WatchManaLeft;

            try
            {
                var curMana = gameData.PlayerUnit?.ManaPercentage;
                var inTown = AreaExtensions.IsTown(gameData.Area);
                if (inTown)
                    return;
                if (curMana != null)
                    if (curMana < ManaLeft && lastMana != (int)curMana)
                    {
                        QuitGame();
                        lastMana = (int)curMana;
                    }
            }
            catch { }
        }

        public void Check(GameData g)
        {
            if (g == null)
                return;
            gameData = g;
            CheckLife();
            CheckMana();
        }
    }
}
