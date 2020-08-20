using Noone.UI.Models;
using ReactiveUI;
using System.Reactive;

namespace Noone.UI.ViewModels
{
    public class MessageBoxWindowViewModel : WindowViewModelBase
    {

        private object content;

        private MessageBoxSettingsViewModel settings;

        /// <summary>
        /// Container represents to the real view
        /// </summary>
        public object Content
        {
            get => content;
            set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public MessageBoxWindowViewModel()
        {
            this.DialogResult = MessageBoxResults.None;
            CloseCommand = ReactiveCommand.Create<MessageBoxResults>(result =>
                            {
                                this.DialogResult = result;
                                this.Close();
                            });
        }


        public ReactiveCommand<MessageBoxResults, Unit> CloseCommand { get; }


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
