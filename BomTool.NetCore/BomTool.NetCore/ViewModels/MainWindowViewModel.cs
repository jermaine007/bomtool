using System;
using System.Collections.Generic;
using System.Text;
using NooneUI.Framework;

namespace BomTool.NetCore.ViewModels
{
    [AutoRegister(Singleton = true)]
    public class MainWindowViewModel : WindowViewModelBase
    {
        public MenuViewModel MenuBar { get; }

        public FileListViewModel FileList { get; }

        public AboutWindowViewModel AboutWindow { get; }

        public MainWindowViewModel()
        {
            this.MenuBar = container.Get<MenuViewModel>();
            this.FileList = container.Get<FileListViewModel>();
            this.AboutWindow = container.Get<AboutWindowViewModel>();
        }
    }
}
