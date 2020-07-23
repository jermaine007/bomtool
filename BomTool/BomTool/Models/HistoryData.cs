using System;
using System.Collections.Generic;
using System.Text;

namespace BomTool.Models
{
    /// <summary>
    /// Data model for history
    /// </summary>
    public class HistoryData
    {
        public string Version { get; set; }
        public string Reason { get; set; }
        public string Signature { get; set; }
        public string Date { get; set; }

        public override string ToString() => string.Join(Environment.NewLine, new[] {
            "History",
            $"Version： {Version}",
            $"Reason： {Reason}",
            $"Signature: {Signature}",
            $"Date: {Date}"

        });
       
    }
}
