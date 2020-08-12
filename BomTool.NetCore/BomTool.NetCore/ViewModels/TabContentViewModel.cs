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
        private string location = string.Empty;

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

        public string Location
        {
            get => location;
            set => this.RaiseAndSetIfChanged(ref location, value);
        }
    }
}
