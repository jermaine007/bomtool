using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace Noone.UI.Core
{
    internal class ViewLocator : Locator, IDataTemplate
    {
        private readonly IMvvmRelationships mvvmRelationships;
        private readonly IDynamicViewPresenter dynamicViewPresenter;

        public ViewLocator()
        {
            mvvmRelationships = container.Get<IMvvmRelationships>();
            dynamicViewPresenter = container.Get<IDynamicViewPresenter>();
        }

        public bool SupportsRecycling => false;

        public IControl Build(object data)
        {
            logger.Debug($"ViewLocator locating view for {data}");
            IControl view = null;
            if (data is IDynamicViewModel dynamicVm)
            {
                view = BuildDynamicView(dynamicVm);
            }
            else if (data is IViewModel vm)
            {
                view = BuildStaticView(vm);
            }
            if (view == null)
            {
               return new TextBlock { Text = $"Found no view for {data}"};
            }
            return view;
        }

        private IControl BuildStaticView(IViewModel vm)
        {
            IView view = mvvmRelationships.GetView(vm);
            if (view is IControl control)
            {
                return control;
            }
            return null;
        }

        private IControl BuildDynamicView(IDynamicViewModel vm)
        {
            IView view = dynamicViewPresenter.GetView(vm);
            if (view is IControl control)
            {
                return control;
            }
            return null;
        }

        public bool Match(object data) => data is IViewModel;
    }
}
