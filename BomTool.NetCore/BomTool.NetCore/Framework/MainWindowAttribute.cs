using System;
using Avalonia.Controls;

namespace BomTool.NetCore.Framework
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class MainWindowAttribute : Attribute
    {
        public MainWindowAttribute(Type type)
        {
            if (!typeof(Window).IsAssignableFrom(type))
            {
                throw new ArgumentException(nameof(type));
            }
            this.Type = type;
        }

        public Type Type { get; }
    }
}