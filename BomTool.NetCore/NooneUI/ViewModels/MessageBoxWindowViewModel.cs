using NooneUI.Framework;
using NooneUI.Models;
using ReactiveUI;
using System.Reactive;

namespace NooneUI.ViewModels
{
    public class MessageBoxWindowViewModel : WindowViewModelBase
    {
        /// <summary>
        /// Container represents to the real view
        /// </summary>
        public object Content { get; set; }

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
