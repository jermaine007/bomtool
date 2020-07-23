using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NooneUI.Pdf.View
{
    [AttributeUsage(AttributeTargets.Class)]
    class ViewLocatorAttribute : Attribute
    {
        public Type Type { get; private set; }
        public ViewLocatorAttribute(Type type)
        {
            if (typeof(FrameworkElement).IsAssignableFrom(type))
            {
                this.Type = type;
            }
            else
            {
                throw new ArgumentException("type");
            }
        }
    }
}
