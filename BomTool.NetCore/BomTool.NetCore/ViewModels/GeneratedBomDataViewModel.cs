using BomTool.NetCore.Models;
using Noone.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;

namespace BomTool.NetCore.ViewModels
{
    public class GeneratedBomDataViewModel : DynamicViewModelBase
    {
        private readonly string template;

        public IEnumerable<GrouppedBomData> Data { get; private set; }

        public override string Template => template;

        public GeneratedBomDataViewModel()
        {
            template = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template", "GeneratedBomView.xaml.template");
        }

        public void Initialize(IEnumerable<GrouppedBomData> data)
        {
            this.Data = data;
        }
    }
}
