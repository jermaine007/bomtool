using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data.Converters;
using NooneUI.Models;

namespace NooneUI.Converters
{
    public class MessageBoxButtonsVisibilityConverter : IValueConverter
    {
        public readonly static string OK = "OK";
        public readonly static string Cancel = "Cancel";
        public readonly static string Yes = "Yes";
        public readonly static string No = "No";

        private static readonly Dictionary<string, Func<MessageBoxButtons, bool>> VisibilityFunc = new Dictionary<string, Func<MessageBoxButtons, bool>>
        {
            [OK] = (m) => m == MessageBoxButtons.OK || m == MessageBoxButtons.OKCancel,
            [Cancel] = (m) => m == MessageBoxButtons.OKCancel || m == MessageBoxButtons.YesNoCancel,
            [Yes] = (m) => m == MessageBoxButtons.YesNo || m == MessageBoxButtons.YesNoCancel,
            [No] = (m) => m == MessageBoxButtons.YesNo || m == MessageBoxButtons.YesNoCancel
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var name = (string)parameter;
            if (name == null)
            {
                return false;
            }
            if (value is MessageBoxButtons m)
            {
                return VisibilityFunc[name](m);
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

    }
}
