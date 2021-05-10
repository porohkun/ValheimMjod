using System.Windows;
using System.Windows.Controls;

namespace ValheimMjod
{
    public class MarginSetter
    {
        public static Thickness GetMargin(DependencyObject obj)
        {
            return (Thickness)obj.GetValue(MarginProperty);
        }

        public static void SetMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(MarginProperty, value);
        }

        public static readonly DependencyProperty MarginProperty =
            DependencyProperty.RegisterAttached("Margin", typeof(Thickness), typeof(MarginSetter), new UIPropertyMetadata(new Thickness(), MarginChangedCallback));

        public static void MarginChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(sender is Panel panel)) return;
            panel.Loaded += new RoutedEventHandler(Panel_Loaded);
        }

        static void Panel_Loaded(object sender, RoutedEventArgs e)
        {
            var panel = sender as Panel;

            foreach (var child in panel.Children)
            {
                if (!(child is FrameworkElement fe)) continue;
                fe.Margin = GetMargin(panel);
            }
        }
    }
}
