using NooneUI.Framework;
using NooneUI.Models;

namespace NooneUI.ViewModels
{
    /// <summary>
    /// Message box view model
    /// </summary>
    public class MessageBoxViewModel : ViewModelBase
    {
        /// <summary>
        /// Message box settings, contain title, width height icons buttons and so on.
        /// </summary>
        public MessageBoxSettings Settings { get; set; }
    }
}
