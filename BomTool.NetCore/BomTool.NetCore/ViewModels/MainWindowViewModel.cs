using System;
using System.Collections.Generic;
using System.Text;
using NooneUI.Framework;

namespace BomTool.NetCore.ViewModels
{
    public class MainWindowViewModel : WindowViewModelBase
    {
        public MenuViewModel MenuBar { get; }
        
        public FileListViewModel FileList { get; }

        public AboutWindowViewModel AboutWindow { get; }

        public MainWindowViewModel()
        {
            this.MenuBar = Container.Get<MenuViewModel>();
            this.FileList = Container.Get<FileListViewModel>();
            this.AboutWindow = Container.Get<AboutWindowViewModel>();
        }
    }
}
