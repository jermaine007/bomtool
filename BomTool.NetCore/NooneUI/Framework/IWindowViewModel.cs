using System.Threading.Tasks;

namespace NooneUI.Framework
{
    /// <summary>
    /// WindowViewModel interface
    /// </summary>
    public interface IWindowViewModel : IViewModel
    {
        /// <summary>
        /// Close window
        /// </summary>
         void Close();

         /// <summary>
         /// Show window non modal
         /// </summary>
         void Show();

         /// <summary>
         /// Show window modal
         /// </summary>
         /// <returns></returns>
         Task ShowDialog();

         /// <summary>
         /// Show window
         /// </summary>
         /// <typeparam name="TResult"></typeparam>
         /// <returns></returns>
         Task<TResult> ShowDialog<TResult>();
    }
}
