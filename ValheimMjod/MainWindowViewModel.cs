using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using Valheim;

namespace ValheimMjod
{
    public class ObservableCollectionDependencyObject : ObservableCollection<DependencyObject> { }

    public class MainWindowViewModelDummy : MainWindowViewModel
    {
        public MainWindowViewModelDummy()
        {
            Characters.Add(new CharacterViewModelDummy());
        }
    }

    public class MainWindowViewModel : BindingBase
    {
        public System.Version Version => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        public ObservableCollection<CharacterViewModel> Characters { get; set; } = new ObservableCollection<CharacterViewModel>();

        public DelegateCommand LoadedCommand { get; }

        public MainWindowViewModel()
        {
            LoadedCommand = new DelegateCommand(Loaded);
        }

        private void Loaded()
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"AppData\LocalLow\IronGate\Valheim\characters");
            if (!Directory.Exists(dir))
            {
                MessageBox.Show($"Directory '{dir}' containing character information not found.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }

            Characters.AddRange(Directory.GetFiles(dir, "*.fch").Select(file =>
            {
                var player = new Player();
                var profile = new PlayerProfile(file);
                if (profile.Load())
                    profile.LoadPlayerData(player);
                else
                    return null;
                //profile.SavePlayerData(player);
                //profile.Save();
                return new CharacterViewModel(player, profile);
            }));
            if (Characters.Count == 0)
            {
                MessageBox.Show($"No character data files found in '{dir}'.", "ERROR");
                Application.Current.Shutdown();
            }
        }

    }
}
