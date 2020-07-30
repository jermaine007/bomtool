using System.Threading.Tasks;

namespace NooneUI.Framework
{
    public interface IWindowViewModel : IViewModel
    {
         void Close();

         void Show();

         Task ShowDialog();
    }
}