using System.Diagnostics;
using System.Windows;

namespace ValheimMjod
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Updater _updater;
        protected override void OnStartup(StartupEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
            _updater = new Updater();
            base.OnStartup(e);

            if (Settings.Instance.Main.FirstLaunch)
            {
                Settings.Instance.Main.FirstLaunch = false;
                Process fileopener = new Process();
                fileopener.StartInfo.FileName = "explorer";
                fileopener.StartInfo.Arguments = "\"" + Settings.AppPath + "whatsnew.txt\"";
                fileopener.Start();
            }
        }
    }
}
