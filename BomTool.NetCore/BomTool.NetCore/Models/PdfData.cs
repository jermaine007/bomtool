using Noone.UI.Models;
using System.Collections.Generic;

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
