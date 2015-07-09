using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TicTacToe.Convertors
{
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (value is bool && (bool) value);
            if (parameter != null && string.Equals(parameter.ToString(), "not"))
                boolValue = !boolValue;
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = value is Visibility && (Visibility)value == Visibility.Visible;
            if (parameter != null && string.Equals(parameter.ToString(), "not"))
                boolValue = !boolValue;
            return boolValue;
        }
    }
}
