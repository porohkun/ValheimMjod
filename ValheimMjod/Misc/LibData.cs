using System.Windows;

namespace ValheimMjod
{
    public class LibData
    {
        public string Name { get; set; }
        public string Authors { get; set; }
        public string Site { get; set; }
        public string License { get; set; }
        public string Icon { get; set; }
        public Visibility ShowIcon => string.IsNullOrWhiteSpace(Icon) ? Visibility.Collapsed : Visibility.Visible;
    }
}
