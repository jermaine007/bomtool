using BomTool.NetCore.Models;
using DynamicData;
using NooneUI.Framework;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BomTool.NetCore.ViewModels
{
    public class FileContentViewModel : ViewModelBase
    {
        private MainWindowViewModel mainWindow;

        private object selectedContent;

        private readonly SourceList<TabContentViewModel> sourceList;

        private readonly ReadOnlyObservableCollection<TabContentViewModel> items;

        public ReadOnlyObservableCollection<TabContentViewModel> Items => items;

        public ReactiveCommand<TabContentViewModel, Unit> RemoveCommand { get; }

        public ReactiveCommand<Unit, Task> GenerateBomCommand { get; }

        public ExcelData ExcelData { get; private set; }

        public MainWindowViewModel MainWindow => mainWindow ??= container.Get<MainWindowViewModel>();

        public object SelectedContent
        {
            get => selectedContent;
            set => this.RaiseAndSetIfChanged(ref selectedContent, value);
        }


        public FileContentViewModel()
        {
            sourceList = new SourceList<TabContentViewModel>();
            sourceList.Connect().Bind(out items).Subscribe();

            RemoveCommand = ReactiveCommand.Create<TabContentViewModel>(vm => sourceList.Remove(vm));
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
                });
            });
        }

        public void Remove(FileItem item)
        {
            TabContentViewModel vm = sourceList.Items.FirstOrDefault(o => item.Location == o.Location);
            if (vm == null)
            {
                return;
            }

            if (SelectedContent == vm && sourceList.Count > 0)
            {
                SelectedContent = sourceList.Items.ElementAt(0);
            }
            sourceList.Remove(vm);

        }

        public bool IsOpened(FileItem item)
        {
            TabContentViewModel vm = sourceList.Items.FirstOrDefault(o => item.Location == o.Location);
            if (vm != null)
            {
                this.SelectedContent = vm;
                return true;
            }

            return false;

        }
        public void Add(FileItem item, ExcelData data)
        {
            this.ExcelData = data;
            sourceList.Add(container.Get<TabContentViewModel>().Setup(vm =>
            {
                vm.Header = Path.GetFileName(item.Name);
                vm.Location = item.Location;
                vm.Content = container.Get<BomDataViewModel>().Setup(o => o.Initialize(data));
            }));
        }

    }
}
