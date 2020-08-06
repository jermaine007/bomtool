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
            logger.Debug($"ViewLocator locating view for {data}");
            var view = container.Get<IMvvmRelationships>().GetView(data as IViewModel);
            var viewModeltype = data.GetType();

            if (view is IControl control)
            {
                return control;
            }

            var name = viewModeltype.FullName.Replace("ViewModel", "View");
            return new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object data) => data is IViewModel;
    }
}
