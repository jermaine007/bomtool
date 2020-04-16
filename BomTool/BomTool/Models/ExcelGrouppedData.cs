using System;
using System.Collections.Generic;
using System.Text;

namespace BomTool.Models
{
    public class ExcelGrouppedData
    {
        public string Line { get; private set; }
        public IEnumerable<ExcelData> Data { get; private set; }

        public int ColumnIndex { get; private set; }

        public ExcelGrouppedData(string line, int index, IEnumerable<ExcelData> data)
        {
            this.Line = line;
            this.ColumnIndex = index;
            this.Data = data;
        }
    }
}
