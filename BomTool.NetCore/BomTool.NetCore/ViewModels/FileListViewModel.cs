using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using NooneUI.Framework;

namespace BomTool.NetCore.ViewModels
{
    public class FileListViewModel : ViewModelBase
    {
        public ObservableCollection<string> Files { get; } = new ObservableCollection<string>();

        public FileListViewModel() { }

        public void AddFile(string file) => Files.Add(file);
    }
}
