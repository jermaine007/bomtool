using BomTool.NetCore.Models;
using DynamicData;
using NooneUI.Framework;
using System;
using System.Collections.ObjectModel;

namespace BomTool.NetCore.ViewModels
{
    public class FileContentViewModel : ViewModelBase
    {
        private readonly SourceList<TabContentViewModel> sourceList;
        private readonly ReadOnlyObservableCollection<TabContentViewModel> items;
        public ReadOnlyObservableCollection<TabContentViewModel> Items => items;

        public FileContentViewModel()
        {
            sourceList = new SourceList<TabContentViewModel>();
            sourceList.Connect().Bind(out items).Subscribe();
        }

        public void Add(FileItem item) => sourceList.Add(container.Get<TabContentViewModel>().Setup(vm =>
        {
            vm.Header = item.Name;
            vm.Content = item.Location;
        }));
    }
}
