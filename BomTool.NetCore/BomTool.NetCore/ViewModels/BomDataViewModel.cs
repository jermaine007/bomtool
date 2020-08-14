using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BomTool.NetCore.Models;
using NooneUI.Framework;

namespace BomTool.NetCore.ViewModels
{
    public class BomDataViewModel : DynamicViewModelBase
    {
        private readonly string template;
        private object dataSource;
        private ExcelData excelData;
        private List<BomData> bomData;

        public override string Template => template;

        public override object DataSource => dataSource;

        public IEnumerable<BomData> BomData => bomData;

        public BomDataViewModel()
        {
            template = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template", "BomView.template");
        }

        public void Initialize(ExcelData data)
        {
            this.excelData = data;
            this.dataSource = data.BomData.First();
            this.bomData = data.BomData.Skip(1).ToList();
        }
    }
}
