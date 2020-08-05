// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace NooneUI.Framework
{
    internal class ViewLocator : Locator, IDataTemplate
    {
        public bool SupportsRecycling => false;

        public IControl Build(object data)
        {
            var view = Relationships.Current.GetView(data as IViewModel);
            var viewModeltype = data.GetType();

            if (view is IControl control)
            {
                return control;
            }

            var name = viewModeltype.FullName.Replace("ViewModel", "View");
            return new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object data)
        {
            return data is IViewModel;
        }
    }
}