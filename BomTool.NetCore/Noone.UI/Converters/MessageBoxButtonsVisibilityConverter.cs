using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using Noone.UI.Models;

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
