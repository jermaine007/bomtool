using System.Threading.Tasks;

namespace Noone.UI
{
    /// <summary>
    /// WindowViewModel interface
    /// </summary>
    public interface IWindowViewModel : IViewModel
    {
        /// <summary>
        ///  Window dialog result
        /// </summary>
        object DialogResult { get; set; }

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
