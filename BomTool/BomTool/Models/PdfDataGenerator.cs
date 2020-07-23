using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BomTool.Models
{
    /// <summary>
    /// Use for generate pdf data
    /// </summary>
    class PdfDataGenerator
    {
        private static readonly int SINGLE_TEMPLATE_THRESHOLD = 36;

        public ExcelData Data { get; private set; }

        public Action<string> Log { get; set; }

        public void Initialize(ExcelData data, Action<string> Log)
        {
            this.Data = data;
            this.Log = Log;
        }

        public PdfData Generate()
        {
            var data = new PdfData
            {
                Bom = this.Data.BomData.Group(Log)
                .OrderBy(d => d.Data.Count()).ToList(),
                History = this.Data.HistoryData,
                Info = this.Data.Information
            };
            data.UseSingleTemplate = data.Bom.SelectMany(b => b.Data).Count() <= SINGLE_TEMPLATE_THRESHOLD;

            return data;
        }
    }
}
