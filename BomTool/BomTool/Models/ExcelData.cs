using System;
using System.Collections.Generic;
using System.Text;

namespace BomTool.Models
{
    /// <summary>
    /// All excel data read from excel file
    /// </summary>
    public class ExcelData
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
