using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using UnityEngine;

namespace ValheimMjod
{
    public class Vector3ToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Vector3 vector))
                return Brushes.Transparent;
            return new SolidColorBrush(Color.FromRgb((byte)(vector.x * 255), (byte)(vector.y * 255), (byte)(vector.z * 255)));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is SolidColorBrush brush))
                return Vector3.one;
            return new Vector3((float)brush.Color.R / 255, (float)brush.Color.G / 255, (float)brush.Color.B / 255);
        }
    }
}
