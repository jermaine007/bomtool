using Noone.UI.Core;
using Noone.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomTool.NetCore.Models
{
    /// <summary>
    /// All excel data read from excel file
    /// </summary>
    public class ExcelData : ModelBase
    {
        /// <summary>
        /// Bom data
        /// </summary>
        public IEnumerable<BomData> BomData { get; set; }
        /// <summary>
        /// History data
        /// </summary>
        public IEnumerable<HistoryData> HistoryData { get; set; }
        /// <summary>
        /// All other information
        /// </summary>
        public Information Information { get; set; }

    }
}
