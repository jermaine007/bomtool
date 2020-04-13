using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BomTool.Models
{
    class HistoryEntries
    {
        public static string HistoryFile { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "__BOM__TOOL__");

        static HistoryEntries()
        {
            if (!File.Exists(HistoryFile))
            {
                var stream = File.Create(HistoryFile);
                stream.Close();
            }
        }

        public static IEnumerable<string> Read() => File.ReadAllLines(HistoryFile);

        public static void Write(IEnumerable<string> data) => File.WriteAllLines(HistoryFile, data);
        
    }
}
