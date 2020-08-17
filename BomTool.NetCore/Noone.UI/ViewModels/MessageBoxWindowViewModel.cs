using Noone.UI.Core;
using Noone.UI.Models;
using ReactiveUI;
using System.Reactive;

namespace Noone.UI.ViewModels
{
    public class MessageBoxWindowViewModel : WindowViewModelBase
    {

        private object content;
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

    }
}
