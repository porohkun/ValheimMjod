using Squirrel;
using System;
using System.Threading;
using System.Windows;

namespace ValheimMjod
{
    public class Updater
    {
        private readonly string Path = @"https://github.com/porohkun/ValheimMjod/releases/latest/download/";
        private readonly AutoResetEvent _autoEvent;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "<Pending>")]
        private readonly Timer _timer;

        public Updater()
        {
#if DEBUG
            return;
#endif
            _autoEvent = new AutoResetEvent(false);
            _timer = new Timer(CheckStatus, _autoEvent, 5000, 30000);
        }

        private void CheckStatus(object stateInfo)
        {
            CheckUpdates();
        }

        public async void CheckUpdates()
        {
            try
            {
                using (var mgr = new UpdateManager(Path))
                {
                    SquirrelAwareApp.HandleEvents(
                        onInitialInstall: v =>
                        {
                            mgr.CreateShortcutForThisExe();
                        },
                        onAppUpdate: v =>
                        {
                            mgr.CreateShortcutForThisExe();
                        },
                        onAppUninstall: v =>
                        {
                            mgr.RemoveShortcutForThisExe();
                        },
                        onFirstRun: () =>
                        {
                        });

                    if (!mgr.IsInstalledApp) //not installed during Squirrel, skipping update
                        return;

                    try
                    {
                        var installedVersion = mgr.CurrentlyInstalledVersion();
                        var entry = await mgr.UpdateApp();
                        if (entry != null && (installedVersion == null || entry.Version > installedVersion))
                        {
                            MessageBox.Show("You need to restart app for apply changes", "Valheim Mjöð have been updated", MessageBoxButton.OK);
                        }
                    }
                    catch (Exception ex)
                    {
                        var errorMessage = $"{ex.Message}\r\n\r\n{ex.StackTrace}\r\n\r\nCopy error message to clipboard?";
                        if (MessageBox.Show(errorMessage, "Error in update/install process", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            Clipboard.SetText(errorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"{ex.Message}\r\n\r\n{ex.StackTrace}\r\n\r\nCopy error message to clipboard?";
                if (MessageBox.Show(errorMessage, "Error before update/install process", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    Clipboard.SetText(errorMessage);
            }
        }
    }
}
