// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace NooneUI.Framework
{
    internal class ViewLocator : IDataTemplate, IContainerProvider
    {
        public bool SupportsRecycling => false;

        public IControl Build(object data)
        {
            var viewModeltype = data.GetType();
            var viewType = Relationship.Current.Lookup(data.GetType());
            if (viewType != null)
            {
                var view = (this as IContainerProvider).Container.Get(viewType);
                if (view is IControl control)
                {
                    //control.DataContext = data;
                    (data as IViewModel).View = control as IView;
                    return control;
                }
            }
            var name = viewModeltype.FullName.Replace("ViewModel", "View");
            return new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}