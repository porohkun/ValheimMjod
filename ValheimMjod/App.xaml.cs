using Rollbar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace ValheimMjod
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var state = RollbarAssistant.CaptureState(this, "Self");
            RollbarLocator.RollbarInstance.Error(e.Exception, new Dictionary<string, object>()
            {
                ["version"] = Settings.Version.ToString()
            });

            MessageBox.Show("Sending message about it to author", "The application is crashed");
        }

        Updater _updater;
        protected override void OnStartup(StartupEventArgs e)
        {
            RollbarLocator.RollbarInstance.Configure(new RollbarConfig("1aa91b699a0f4717a444415cfe20e879")
            {
#if DEBUG
                Environment = "debug"
#endif
            }).AsBlockingLogger(TimeSpan.FromSeconds(3));

            var window = new MainWindow();
            window.Show();
            _updater = new Updater();
            base.OnStartup(e);

            PlayFab.LoginUser();

            if (Settings.Instance.Main.LastLaunchedVersion < Settings.Version)
            {
                Settings.Instance.Main.LastLaunchedVersion = Settings.Version;
                Process fileopener = new Process();
                fileopener.StartInfo.FileName = "explorer";
                fileopener.StartInfo.Arguments = "\"" + Settings.AppPath + "whatsnew.txt\"";
                fileopener.Start();
            }
        }
    }
}
