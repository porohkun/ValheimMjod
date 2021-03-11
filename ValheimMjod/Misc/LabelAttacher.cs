using System.Windows;

namespace ValheimMjod
{
    public static class LabelAttacher
    {
        public static readonly DependencyProperty LabelProperty = DependencyProperty.RegisterAttached(
            "Label",
            typeof(string),
            typeof(LabelAttacher),
            new PropertyMetadata(null));

        public static void SetLabel(DependencyObject element, string value) => element.SetValue(LabelProperty, value);

        public static string GetLabel(DependencyObject element) => (string)element.GetValue(LabelProperty);

    }
}
