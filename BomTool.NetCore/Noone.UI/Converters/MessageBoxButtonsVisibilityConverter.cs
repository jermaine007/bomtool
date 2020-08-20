using Avalonia.Data.Converters;
using Noone.UI.Models;
using System;
using System.Globalization;

namespace Noone.UI.Converters
{
    public class MessageBoxButtonsVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MessageBoxButtonsBase type = (MessageBoxButtonsBase)parameter;

            if (value is MessageBoxButtons button)
            {
                return ((MessageBoxButtonsBase)button).HasFlag(type);
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    }
}
