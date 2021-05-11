using System;
using System.Globalization;
using System.Windows.Data;

namespace ValheimMjod
{
    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double doubleValue))
                return value;
            return string.Format("{0:N1}", doubleValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
