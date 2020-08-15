using BomTool.NetCore.Models;
using NooneUI.Framework;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace BomTool.NetCore.ViewModels
{
    public class TabContentViewModel : ViewModelBase
    {

        private MainWindowViewModel mainWindow;

        private string header = string.Empty;
        private object content;
        private string location = string.Empty;
        private bool canGeneratePdf = false;

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

        public bool CanGeneratePdf
        {
            get => canGeneratePdf;
            set => this.RaiseAndSetIfChanged(ref canGeneratePdf, value);
        }

        public MainWindowViewModel MainWindow => mainWindow ??= container.Get<MainWindowViewModel>();
        public ReactiveCommand<Unit, Task> GenerateBomCommand { get; }

        public ExcelData ExcelData { get; set; }

        public TabContentViewModel()
        {
            GenerateBomCommand = ReactiveCommand.Create(async () =>
            {
               string folder = await dialog.OpenFolderDialogAsync("Select a folder");
               if (string.IsNullOrEmpty(folder))
               {
                   return;
               }
               MainWindow.Waiting((statusBar) =>
               {
                   ExcelWriter writer = container.Get<ExcelWriter>().Setup(o =>
                   {
                       o.Initialize(ExcelData.BomData, folder, msg => statusBar.Message = msg);
                   });
                   writer.Write();
                   this.Content = container.Get<GeneratedBomDataViewModel>().Setup(vm =>
                   {
                       vm.Initialize(writer.GrouppedData);
                       this.CanGeneratePdf = true;
                   });
               });
           });
        }
    }
}
