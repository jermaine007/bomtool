using System;
using System.Collections.Generic;
using System.Text;
using NooneUI.Framework;

namespace BomTool.NetCore.ViewModels
{
    public class MainWindowViewModel : WindowViewModelBase
    {
        public MenuViewModel MenuBar { get; }

        public MainWindowViewModel()
        {
            this.MenuBar = container.Get<MenuViewModel>();
        }
    }
}
