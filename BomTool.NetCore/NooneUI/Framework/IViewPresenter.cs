using System.Threading.Tasks;

namespace NooneUI.Framework
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
