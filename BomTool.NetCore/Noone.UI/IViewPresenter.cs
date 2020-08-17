using System.Threading.Tasks;

namespace Noone.UI
{
    /// <summary>
    /// View presenter
    /// </summary>
    public interface IViewPresenter
    {
        void Close(IWindowViewModel vm);
        void Show(IWindowViewModel vm);
        Task ShowDialog(IWindowViewModel vm);
        Task<TResult> ShowDialog<TResult>(IWindowViewModel vm);
        IView GetView(IViewModel vm);
        void AddOrUpdateStore(IViewModel vm, IView view);
    }
}
