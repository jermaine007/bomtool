using System;
using NooneUI.Framework;
using ReactiveUI;

namespace BomTool.NetCore.ViewModels
{
    public class StatusBarViewModel : ViewModelBase
    {
        private string message;
        private bool isBusy;

        public string Message
        {
            get => message;
            set => this.RaiseAndSetIfChanged(ref message, value);
        }

        public bool IsBusy
        {
            get => isBusy;
            set => this.RaiseAndSetIfChanged(ref isBusy, value);
        }
    }
}
