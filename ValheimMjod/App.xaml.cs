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
        }
    }
}
