using Noone.UI.Models;
using System.Collections.Generic;

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
