using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Noone.UI;
using Noone.UI.Models;

namespace BomTool.NetCore.Models
{
    /// <summary>
    /// Use for generate pdf data
    /// </summary>
    public class PdfDataGenerator : ModelBase, IContainerProvider
    {
        private static readonly int SINGLE_TEMPLATE_THRESHOLD = 36;
        private readonly IContainer container;

        public ExcelData Data { get; private set; }

        public Action<string> Log { get; set; }

        public PdfDataGenerator() => container = ((IContainerProvider)this).Container;

        public void Initialize(ExcelData data, Action<string> Log)
        {
            this.Data = data;
            this.Log = Log;
        }

        public PdfData Generate()
        {
            var data = container.Get<PdfData>().Setup(o =>
            {
                o.Bom = this.Data.BomData.Group(Log)
                               .OrderBy(d => d.Data.Count()).ToList();
                o.History = this.Data.HistoryData;
                o.Info = this.Data.Information;
            });
            data.UseSingleTemplate = data.Bom.SelectMany(b => b.Data).Count() <= SINGLE_TEMPLATE_THRESHOLD;
            return data;
        }
    }
}
