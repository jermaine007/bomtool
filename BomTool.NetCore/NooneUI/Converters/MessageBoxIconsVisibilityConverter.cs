using System;
using System.Globalization;
using Avalonia.Data.Converters;
using NooneUI.Models;

namespace NooneUI.Converters
{
    public class MessageBoxIconsVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is MessageBoxIcons expected
                && value is MessageBoxIcons actual)
            {
                return actual == expected;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    }
}
