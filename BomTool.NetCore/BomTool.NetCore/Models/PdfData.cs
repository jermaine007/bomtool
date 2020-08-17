using System;
using System.Collections.Generic;
using System.Text;
using Noone.UI.Models;

namespace BomTool.NetCore.Models
{
    /// <summary>
    /// Pdf data used to generate pdf
    /// </summary>
    public class PdfData : ModelBase
    {
        public IEnumerable<GrouppedBomData> Bom { get; set; }

        public Information Info { get; set; }

        public IEnumerable<HistoryData> History { get; set; }

        public bool UseSingleTemplate { get; set; }
    }
}
