using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using BomTool.NetCore.Models;
using DynamicData;
using NooneUI.Framework;
using ReactiveUI;

namespace BomTool.NetCore.ViewModels
{
    public class FileListViewModel : ViewModelBase
    {

        private readonly SourceList<FileItem> sourceList;
        private readonly ReadOnlyObservableCollection<FileItem> files;
        private FileContentViewModel fileContent;

        private MainWindowViewModel mainWindow;

        public ReadOnlyObservableCollection<FileItem> Files => files;

        public ReactiveCommand<FileItem, Unit> RemoveFileCommand { get; }
        public ReactiveCommand<FileItem, Unit> OpenFileCommand { get; }

        public FileContentViewModel FileContent => fileContent ??= this.MainWindow.FileContent;
        public MainWindowViewModel MainWindow => mainWindow ??= container.Get<MainWindowViewModel>();

        public FileListViewModel()
        {
            sourceList = new SourceList<FileItem>();
            sourceList.AddRange(FileItem.Load());
            sourceList.Connect()
                      .OnItemAdded(_ => FileItem.Save(sourceList.Items))
                      .OnItemRemoved(_ => FileItem.Save(sourceList.Items))
                      .Bind(out files)
                      .Subscribe();

            RemoveFileCommand = ReactiveCommand.Create<FileItem>(item =>
            {
                sourceList.Remove(item);
                FileContent.Remove(item);
            });

            OpenFileCommand = ReactiveCommand.Create<FileItem>(item => ProcessFile(item, true));
        }

        public void AddFile(string file)
        {
            if (sourceList.Items.Any(item => item.Location == file))
            {
                return;
            }
            var item = FileItem.Create(file);
            ProcessFile(item);
        }

        private void ProcessFile(FileItem item, bool alreadyOpened = false) =>
             this.MainWindow.Waiting((statusBar) =>
               {
                   if (!alreadyOpened)
                   {
                       sourceList.Add(item);
                   }
                   if (FileContent.IsOpened(item))
                   {
                       return;
                   }
                   ExcelDataReader reader = container.Get<ExcelDataReader>().Setup(r =>
                   {
                       r.Initialize(item.Location, msg => statusBar.Message = msg);
                   });
                   ExcelData data = reader.Read();

                   FileContent.Add(item, data);
               });

    }
}
