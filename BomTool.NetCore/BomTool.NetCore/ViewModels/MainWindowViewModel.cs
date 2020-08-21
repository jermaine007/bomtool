using Noone.UI;
using Noone.UI.Core;
using Noone.UI.ViewModels;
using ReactiveUI;
using System;
using System.Reactive;

namespace BomTool.NetCore.ViewModels
{
    [AutoRegister(Singleton = true)]
    public class MainWindowViewModel : WindowViewModelBase
    {
        private bool showSideBar = true;

        public bool ShowSideBar
        {
            get => showSideBar;
            set => this.RaiseAndSetIfChanged(ref showSideBar, value);
        }

        public ReactiveCommand<MainWindowViewModel, Unit> ShowSideBarCommand { get; }

        public MenuViewModel MenuBar { get; }

        public FileListViewModel FileList { get; }

        public FileContentViewModel FileContent { get; }

        public StatusBarViewModel StatusBar { get; }

        public IDispatcherService DispatcherService { get; }

        public MainWindowViewModel()
        {
            this.MenuBar = container.Get<MenuViewModel>();
            this.FileList = container.Get<FileListViewModel>();
            this.FileContent = container.Get<FileContentViewModel>();
            this.StatusBar = container.Get<StatusBarViewModel>();
            this.DispatcherService = container.Get<IDispatcherService>();
            this.ShowSideBarCommand = ReactiveCommand.Create<MainWindowViewModel>((vm) =>
            {
                vm.ShowSideBar = !vm.ShowSideBar;
            });
        }

        public void Waiting(Action<StatusBarViewModel, Action> action)
        {
            this.StatusBar.IsBusy = true;
            action(this.StatusBar, () => this.StatusBar.IsBusy = false);
        }

        internal void Output(string message)
        {
            logger.Debug(message);
            this.StatusBar.Message = message;
        }

    }
}
