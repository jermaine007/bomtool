using System;
using System.Collections.Generic;
using System.Text;

namespace BomTool.Models
{
    /// <summary>
    /// Bom data which has been groupped
    /// </summary>
    public class GrouppedBomData
    {
        /// <summary>
        /// Line name (PTH, SMD)
        /// </summary>
        public string Line { get; private set; }

        /// <summary>
        /// Group bom data
        /// </summary>
        public IEnumerable<BomData> Data { get; private set; }

        /// <summary>
        /// ColumnIndex used for generated sheet name
        /// </summary>
        public int ColumnIndex { get; private set; }

        public GrouppedBomData(string line, int index, IEnumerable<BomData> data)
        {
            this.Line = line;
            this.ColumnIndex = index;
            this.Data = data;
        }
    }
}
