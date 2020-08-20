using System.Threading.Tasks;

namespace Noone.UI.ViewModels
{
    public abstract class WindowViewModelBase : ViewModelBase, IWindowViewModel
    {
        private IViewPresenter presenter;

        public IViewPresenter Presenter => presenter ??= container.Get<IViewPresenter>();

        public virtual void Close() => Presenter.Close(this);

        public virtual void Show() => Presenter.Show(this);

        public virtual Task ShowDialog() => Presenter.ShowDialog(this);

        public virtual Task<TResult> ShowDialog<TResult>() => Presenter.ShowDialog<TResult>(this);

        public virtual object DialogResult { get; set; }
    }
}
