using System;
using System.Collections.Generic;
using System.Text;

namespace BomTool.Models
{
    /// <summary>
    /// Bom data from excel file
    /// </summary>
    public class BomData
    {
        public string Reference { get; set; }

        public string Code { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

        public List<string> Lines { get; set; } = new List<string>();

        public int Count { get; set; }

        public override string ToString() =>
            string.Join(",", new[] {
                this.Reference,
                this.Code,
                this.Type,
                this.Description,
                this.Value,
                string.Join(",", this.Lines)
            });
    }
}
