using System;
using System.Reactive;
using NooneUI.Framework;
using ReactiveUI;

namespace BomTool.NetCore.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {

        public ReactiveCommand<MainWindowViewModel, Unit> OpenFileCommand { get; }
        public ReactiveCommand<AboutWindowViewModel, Unit> ShowAboutCommand { get; }
        public MenuViewModel()
        {
            OpenFileCommand = ReactiveCommand.Create<MainWindowViewModel>(async(window)=>
            {
                var files = await this.DialogService.OpenFileDialogAsync();
            });

            ShowAboutCommand = ReactiveCommand.Create<AboutWindowViewModel>(async(window)=>{
                await window.ShowDialog();
            });
        }
    }
}