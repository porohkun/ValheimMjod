using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using UnityEngine;

namespace ValheimMjod
{
    public class Vector3ToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Vector3 vector))
                return Brushes.Transparent;
            return Color.FromRgb((byte)(vector.x * 255), (byte)(vector.y * 255), (byte)(vector.z * 255));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Color color))
                return Vector3.one;
            return new Vector3((float)color.R / 255, (float)color.G / 255, (float)color.B / 255);
        }
    }
}
