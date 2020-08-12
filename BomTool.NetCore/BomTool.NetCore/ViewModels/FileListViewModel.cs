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
        public ReadOnlyObservableCollection<FileItem> Files => files;

        public ReactiveCommand<FileItem, Unit> RemoveFileCommand { get; }
        public ReactiveCommand<FileItem, Unit> OpenFileCommand { get; }


        public FileContentViewModel FileContent => fileContent ??= container.Get<MainWindowViewModel>().FileContent;

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

            OpenFileCommand = ReactiveCommand.Create<FileItem>(item => FileContent.Add(item));
        }

        public void AddFile(string file)
        {
            if (sourceList.Items.Any(item => item.Location == file))
            {
                return;
            }
            var item = FileItem.Create(file);

            sourceList.Add(item);
            FileContent.Add(item);
        }
    }
}
