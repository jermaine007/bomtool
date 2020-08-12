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
        public ReadOnlyObservableCollection<FileItem> Files => files;

        public ReactiveCommand<FileItem, Unit> RemoveFileCommand { get; }

        public FileListViewModel()
        {
            sourceList = new SourceList<FileItem>();
            sourceList.AddRange(FileItem.Load());
            sourceList.Connect()
                      .OnItemAdded(_ =>
                      {
                          FileItem.Save(sourceList.Items);
                      })
                      .OnItemRemoved(_ =>
                      {
                          FileItem.Save(sourceList.Items);
                      }).Bind(out files)
                      .Subscribe();

            RemoveFileCommand = ReactiveCommand.Create<FileItem>((fileItem) =>
            {
                sourceList.Remove(fileItem);
            });
        }

        public void AddFile(string file) => sourceList.Add(FileItem.Create(file));
    }
}
