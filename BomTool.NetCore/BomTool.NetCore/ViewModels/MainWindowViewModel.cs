using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Threading;
using NLog.Fluent;
using Noone.UI;
using Noone.UI.Core;
using Noone.UI.ViewModels;

namespace BomTool.NetCore.ViewModels
{
    [AutoRegister(Singleton = true)]
    public class MainWindowViewModel : WindowViewModelBase
    {
        public MenuViewModel MenuBar { get; }

        public FileListViewModel FileList { get; }

        public AboutWindowViewModel AboutWindow { get; }

        public FileContentViewModel FileContent { get; }

        public StatusBarViewModel StatusBar { get; }

        public IDispatcherService DispatcherService { get; }

        public MainWindowViewModel()
        {
            this.MenuBar = container.Get<MenuViewModel>();
            this.FileList = container.Get<FileListViewModel>();
            this.AboutWindow = container.Get<AboutWindowViewModel>();
            this.FileContent = container.Get<FileContentViewModel>();
            this.StatusBar = container.Get<StatusBarViewModel>();
            this.DispatcherService = container.Get<IDispatcherService>();
        }

        public async void Waiting(Action<StatusBarViewModel> action)
        {
            try
            {
                this.StatusBar.IsBusy = true;
                await this.DispatcherService.InvokeAsync(() => action(this.StatusBar));
            }
            finally
            {
                this.StatusBar.IsBusy = false;
            }

        }

    }
}
