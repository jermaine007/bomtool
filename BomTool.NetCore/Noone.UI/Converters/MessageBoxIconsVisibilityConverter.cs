using Avalonia.Data.Converters;
using Noone.UI.Models;
using System;
using System.Globalization;

namespace Noone.UI.Converters
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
