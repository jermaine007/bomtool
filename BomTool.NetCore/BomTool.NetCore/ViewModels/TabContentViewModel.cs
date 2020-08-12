using NooneUI.Framework;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomTool.NetCore.ViewModels
{
    public class TabContentViewModel : ViewModelBase
    {
        private string header = string.Empty;
        private object content;

        public string Header
        {
            get => header;
            set => this.RaiseAndSetIfChanged(ref header, value);
        }

        public object Content
        {
            get => content;
            set => this.RaiseAndSetIfChanged(ref content, value);
        }
    }
}
