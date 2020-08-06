using System.Threading.Tasks;

namespace NooneUI.Framework
{
    public interface IViewPresenter
    {
        void Close(IWindowViewModel vm);
        void Show(IWindowViewModel vm);
        Task ShowDialog(IWindowViewModel vm);
        Task<TResult> ShowDialog<TResult>(IWindowViewModel vm);
    }
}
