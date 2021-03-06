using BomTool.NetCore.Models;
using Noone.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BomTool.NetCore.ViewModels
{
    public class BomDataViewModel : DynamicViewModelBase
    {
        private readonly string template;
        private object dataSource;
        private List<BomData> bomData;

        public override string Template => template;

        public override object DataSource => dataSource;

        public IEnumerable<BomData> BomData => bomData;

        public BomDataViewModel()
        {
            template = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template", "BomView.xaml.template");
        }

        public void Initialize(ExcelData data)
        {
            this.dataSource = data.BomData.First();
            this.bomData = data.BomData.Skip(1).ToList();
        }
    }
}
