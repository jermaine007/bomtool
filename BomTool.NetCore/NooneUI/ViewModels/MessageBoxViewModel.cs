using NooneUI.Framework;
using NooneUI.Models;
using ReactiveUI;

namespace NooneUI.ViewModels
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
