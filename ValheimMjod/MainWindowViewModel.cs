using System;
using System.Collections.Generic;
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
            EditingItem = Characters[0].ItemsProps[0];
        }
    }

    public partial class MainWindowViewModel : BindableBase
    {
        public System.Version Version => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        public ObservableCollection<CharacterViewModel> Characters { get; set; } = new ObservableCollection<CharacterViewModel>();

        public DelegateCommand LoadedCommand { get; }
        public DelegateCommand<CharacterViewModel> SaveCommand { get; }
        public DelegateCommand<Prop> EditItemCommand { get; }
        public DelegateCommand EndEditItemCommand { get; }
        public DelegateCommand<Prop> RemoveItemCommand { get; }
        public DelegateCommand ChangeItemCommand { get; }
        public DelegateCommand EndChangeItemCommand { get; }

        public MainWindowViewModel()
        {
            LoadedCommand = new DelegateCommand(Loaded);
            SaveCommand = new DelegateCommand<CharacterViewModel>(Save, CanSave);
            EditItemCommand = new DelegateCommand<Prop>(EditItem);
            EndEditItemCommand = new DelegateCommand(EndEditItem);
            RemoveItemCommand = new DelegateCommand<Prop>(RemoveItem);
            ChangeItemCommand = new DelegateCommand(ChangeItem);
            EndChangeItemCommand = new DelegateCommand(EndChangeItem);
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
                return new CharacterViewModel(player, profile);
            }));
            if (Characters.Count == 0)
            {
                MessageBox.Show($"No character data files found in '{dir}'.", "ERROR");
                Application.Current.Shutdown();
            }
        }

        private bool CanSave(CharacterViewModel arg)
        {
            return true;
        }

        private void Save(CharacterViewModel model)
        {
            model.Profile.SavePlayerData(model.Player);
            model.Profile.Save();
        }

        private void EditItem(Prop item)
        {
            EditingItem = item;
            if (EditingItemName == null)
                SelectedItem = "";
        }

        private void EndEditItem()
        {
            EditingItem = null;
            SelectedItem = null;
        }

        private void RemoveItem(Prop item)
        {
            var props = (Dictionary<string, Prop>)item.Value;
            props["name"].Value = null;
        }

    }
}
