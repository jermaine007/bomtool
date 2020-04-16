using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BomTool.Models
{
    internal class HistoryEntry
    {
        public static string HistoryFile { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "__BOM__TOOL__");

        public HistoryEntry()
        {
            if (!File.Exists(HistoryFile))
            {
                var stream = File.Create(HistoryFile);
                stream.Close();
            }
        }

        public IEnumerable<string> Read() => File.ReadAllLines(HistoryFile);

        public void Write(IEnumerable<string> data) => File.WriteAllLines(HistoryFile, data);

    }
}
