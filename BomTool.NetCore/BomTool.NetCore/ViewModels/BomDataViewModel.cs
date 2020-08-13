using System;
using System.IO;
using NooneUI.Framework;

namespace BomTool.NetCore.ViewModels
{
    public class BomDataViewModel : DynamicViewModelBase
    {
        private readonly string template;
        private readonly object data;

        public override string Template => template;

        public override object DataSource => data;

        public string Text => "Hello From Data Context";

        public BomDataViewModel()
        {
            template = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template", "BomView.template");
            data = new
            {
                HelloText = "Hello dynamic view"
            };
        }
    }
}
