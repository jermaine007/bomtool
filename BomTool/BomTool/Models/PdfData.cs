using System;
using System.Collections.Generic;
using System.Text;

namespace BomTool.Models
{
    /// <summary>
    /// Pdf data used to generate pdf
    /// </summary>
    public class PdfData
    {
        public IEnumerable<GrouppedBomData> Bom { get; set; }

        public Information Info { get; set; }

        public IEnumerable<HistoryData> History { get; set; }

        public bool UseSingleTemplate { get; set; }
    }
}
