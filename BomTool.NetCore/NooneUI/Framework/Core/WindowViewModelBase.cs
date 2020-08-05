using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;

namespace NooneUI.Framework
{
    public class WindowViewModelBase : ViewModelBase, IWindowViewModel
    {
        private ViewPresenter presenter;

        public ViewPresenter Presenter => presenter ??= container.Get<ViewPresenter>();

        public void Close() => Presenter.Close(this);

        public void Show() => Presenter.Show(this);

        public Task ShowDialog() => Presenter.ShowDialog(this);

        public Task<TResult> ShowDialog<TResult>() => Presenter.ShowDialog<TResult>(this);
    }
}