using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NooneUI.Framework
{
    internal class ViewPresenter : IViewPresenter
    {
        public void Close(IWindowViewModel vm)
        {
            if (vm.View is Window window)
            {
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
            if (vm.View is Window window)
            {
                window.Show();
            }
        }

        public Task ShowDialog(IWindowViewModel vm)
        {
            if (vm.View is Window window)
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
            if (vm.View is Window window)
            {
                Window owner = LightApplicationBase.MainWindow ?? window;
                if (owner != window)
                {
                    return window.ShowDialog<TResult>(owner);
                }
            }
            return null;
        }
    }
}
