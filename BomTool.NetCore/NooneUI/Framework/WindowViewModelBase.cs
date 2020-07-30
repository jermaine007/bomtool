using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;

namespace NooneUI.Framework
{
    public class WindowViewModelBase : ViewModelBase, IWindowViewModel
    {
        public void Close()
        {
            if (this.View is Window window)
            {
                window.Close();
            }
        }

        public void Show()
        {
            if (this.View is Window window)
            {
                window.Show();
            }
        }

        public Task ShowDialog()
        {
            if (this.View is Window window)
            {
                Window owner = (Application.Current as LightApplicationBase).Desktop?.MainWindow;
                if (owner != window)
                {
                    return window.ShowDialog(owner);
                }
            }
            return null;
        }
    }
}