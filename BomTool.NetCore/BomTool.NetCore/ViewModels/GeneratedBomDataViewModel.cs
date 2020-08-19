using System;
using System.Collections.Generic;
using System.IO;
using BomTool.NetCore.Models;
using Noone.UI.Core;
using Noone.UI.ViewModels;

namespace BomTool.NetCore.ViewModels
{
    public class GeneratedBomDataViewModel : DynamicViewModelBase
    {
        private readonly string template;

        public IEnumerable<GrouppedBomData> Data {get; private set;}

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
