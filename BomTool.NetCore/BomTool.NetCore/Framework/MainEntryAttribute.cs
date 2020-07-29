using System;
using Avalonia.Controls;

namespace BomTool.NetCore.Framework
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class MainEntryAttribute : Attribute, IContainerProvider
    {
        public MainEntryAttribute(Type viewType, Type viewModelType)
        {
            if (!typeof(Window).IsAssignableFrom(viewType))
            {
                throw new ArgumentException(nameof(viewType));
            }
            this.ViewType = viewType;

            if (!typeof(ViewModelBase).IsAssignableFrom(viewModelType))
            {
                throw new ArgumentException(nameof(viewModelType));
            }
            this.ViewModelType = viewModelType;
            var container = ((IContainerProvider)this).Container;
            container.Bind(viewType);
            container.Bind(viewModelType);
        }

        public Type ViewType { get; }

        public Type ViewModelType { get; }
    }
}