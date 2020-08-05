using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;

namespace NooneUI.Framework
{
    public class WindowViewModelBase : ViewModelBase, IWindowViewModel
    {
        private IViewPresenter presenter;

        public IViewPresenter Presenter => presenter ??= Container.Get<IViewPresenter>();

        public void Close() => Presenter.Close(this);

        public void Show() => Presenter.Show(this);

        public Task ShowDialog() => Presenter.ShowDialog(this);

        public Task<TResult> ShowDialog<TResult>() => Presenter.ShowDialog<TResult>(this);
    }
}