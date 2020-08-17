using Noone.UI.Core;
using Noone.UI.Models;
using ReactiveUI;

namespace Noone.UI.ViewModels
{
    /// <summary>
    /// Message box view model
    /// </summary>
    public class MessageBoxViewModel : ViewModelBase
    {
        private MessageBoxSettingsViewModel settings;

        /// <summary>
        /// Message box settings, contain title, width height icons buttons and so on.
        /// </summary>
        public MessageBoxSettingsViewModel Settings
        {
            get => settings;
            set => this.RaiseAndSetIfChanged(ref settings, value);
        }
    }
}
