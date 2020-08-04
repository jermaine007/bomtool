using System;
using System.Reactive;
using NooneUI.Framework;
using ReactiveUI;

namespace BomTool.NetCore.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {

        public ReactiveCommand<object, Unit> OpenFileCommand { get; }
        public MenuViewModel()
        {
            //OpenFileCommand = ReactiveCommand.Create<Unit, Unit>(()=>{});

            // OpenFileCommand = ReactiveCommand.CreateFromTask(async () =>
            // {
            //     var files = await DialogService.OpenFileDialogAsync();
            // });

            OpenFileCommand = ReactiveCommand.Create<object>((o)=>
            {

            });


        }
    }
}