using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace ValheimMjod
{
    public class StringCamelCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string stringValue))
                return value;
            return Regex.Replace(
                Regex.Replace(
                    stringValue,
                    @"(\P{Ll})(\P{Ll}\p{Ll})",
                    "$1 $2"
                    ),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2"
                );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string stringValue))
                return value;
            return string.Concat(stringValue.Split(' '));
        }
    }
}
