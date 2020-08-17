using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BomTool.NetCore.Converters
{
    public class BoundedWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double margin = 0;
                if (parameter != null)
                {
                    Double.TryParse((string)parameter, out margin);
                }
                return Math.Max((double)value - margin, 0);
            }
            catch
            {
                return null;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
