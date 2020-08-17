using Noone.UI.Core;
using Noone.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomTool.NetCore.Models
{
    /// <summary>
    /// Data model for history
    /// </summary>
    public class HistoryData : ModelBase
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
