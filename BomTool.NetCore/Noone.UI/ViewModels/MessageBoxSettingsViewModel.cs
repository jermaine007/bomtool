
using Noone.UI.Models;
using ReactiveUI;

namespace Noone.UI.ViewModels
{
    /// <summary>
    /// MessageBox settings, would be auto register into ioc container
    /// </summary>
    public class MessageBoxSettingsViewModel : ViewModelBase
    {
        private string title = string.Empty;
        private int width = 400;
        private int height = 200;
        private MessageBoxButtons buttons = MessageBoxButtons.None;
        private MessageBoxIcons icons = MessageBoxIcons.None;
        private string message = string.Empty;
        private bool isFixedPosition = true;

        /// <summary>
        /// Message Box title
        /// </summary>
        public string Title
        {
            get => title;
            set => this.RaiseAndSetIfChanged(ref title, value);
        }

        /// <summary>
        /// Message box window width
        /// </summary>
        public int Width
        {
            get => width;
            set => this.RaiseAndSetIfChanged(ref width, value);
        }

        /// <summary>
        /// Message box height
        /// </summary>
        public int Height
        {
            get => height;
            set => this.RaiseAndSetIfChanged(ref height, value);
        }

        /// <summary>
        /// Message box buttons
        /// </summary>
        public MessageBoxButtons Buttons
        {
            get => buttons;
            set => this.RaiseAndSetIfChanged(ref buttons, value);
        }

        /// <summary>
        /// Message box icons
        /// </summary>
        public MessageBoxIcons Icons
        {
            get => icons;
            set => this.RaiseAndSetIfChanged(ref icons, value);
        }

        /// <summary>
        /// The message would be displayed in the message box
        /// </summary>
        public string Message
        {
            get => message;
            set => this.RaiseAndSetIfChanged(ref message, value);
        }

        public bool IsFixedPosition
        {
            get => isFixedPosition;
            set => this.RaiseAndSetIfChanged(ref isFixedPosition, value);
        }
    }
}
