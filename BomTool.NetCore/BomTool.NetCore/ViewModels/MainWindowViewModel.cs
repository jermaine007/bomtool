﻿using System;
using System.Collections.Generic;
using System.Text;
using NooneUI.Framework;

namespace BomTool.NetCore.ViewModels
{
    public class MainWindowViewModel : WindowViewModelBase
    {
        public MenuViewModel MenuBar { get; }
        public FileListViewModel FileList { get; }

        public MainWindowViewModel()
        {
            this.MenuBar = container.Get<MenuViewModel>();
            this.FileList = container.Get<FileListViewModel>();

            this.MenuBar.Parent = this;
            this.FileList.Parent = this;
        }
    }
}
