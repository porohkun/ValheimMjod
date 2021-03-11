using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ValheimCharacterTrainer
{
    public static class AutoSelect
    {
        public static readonly DependencyProperty AutoSelectFirstTabProperty
            = DependencyProperty.RegisterAttached("AutoSelectFirstTab", typeof(bool),
            typeof(AutoSelect), new PropertyMetadata(false, AutoSelectFirstTabChanged));

        public static bool GetAutoSelectFirstTab(TabControl obj) => (bool)obj.GetValue(AutoSelectFirstTabProperty);
        public static void SetAutoSelectFirstTab(TabControl obj, bool value) => obj.SetValue(AutoSelectFirstTabProperty, value);

        private static void AutoSelectFirstTabChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tabControl = (TabControl)d;
            tabControl.ItemContainerGenerator.ItemsChanged += ItemContainerGenerator_ItemsChanged;
            async void ItemContainerGenerator_ItemsChanged(object _1, ItemsChangedEventArgs args)
            {
                await Task.Yield();
                if (GetAutoSelectFirstTab(tabControl)
                    && args.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add
                    && tabControl.Items.Count > 0
                    && tabControl.SelectedIndex == -1)
                    tabControl.SelectedIndex = 0;
            }
        }
    }
}
