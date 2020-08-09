using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NooneUI.Framework
{
    internal class ViewPresenter : IViewPresenter, IBaseServiceProvider
    {
        private readonly Dictionary<IWindowViewModel, IView> viewStore;
        private readonly IContainer container;
        
        public ViewPresenter()
        {
            viewStore = new Dictionary<IWindowViewModel, IView>();
            container = ((IBaseServiceProvider)this).Container;
        }

        public void Close(IWindowViewModel vm)
        {
            if (GetView(vm) is Window window)
            {
                // remove view first
                viewStore.Remove(vm);
                if (vm.DialogResult != null)
                {
                    window.Close(vm.DialogResult);
                }
                else
                {
                    window.Close();
                }
            }
        }

        public void Show(IWindowViewModel vm)
        {
            if (GetView(vm) is Window window)
            {
                window.Show();
            }
        }

        public Task ShowDialog(IWindowViewModel vm)
        {
            if (GetView(vm) is Window window)
            {
                Window owner = LightApplicationBase.MainWindow ?? window;
                if (owner != window)
                {
                    return window.ShowDialog(owner);
                }
            }
            return null;
        }

        public Task<TResult> ShowDialog<TResult>(IWindowViewModel vm)
        {
            if (GetView(vm) is Window window)
            {
                Window owner = LightApplicationBase.MainWindow ?? window;
                if (owner != window)
                {
                    return window.ShowDialog<TResult>(owner);
                }
            }
            return null;
        }

        private IView GetView(IWindowViewModel viewModel)
        {
            if (!viewStore.TryGetValue(viewModel, out var view))
            {
                view = container.Get<IMvvmRelationships>().GetView(viewModel);
                if (view != null)
                {
                    viewStore.Add(viewModel, view);
                }
            }
            return view;
        }
    }
}
