using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BomTool.Models
{
    /// <summary>
    /// Class log the opened xls file path
    /// </summary>
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

        public IEnumerable<string> Read() => File.ReadAllLines(HistoryFile).Where(f => File.Exists(f)).ToList();

        /// <summary>
        /// Save opened files to file
        /// </summary>
        /// <param name="data"></param>
        public void Write(IEnumerable<string> data) => File.WriteAllLines(HistoryFile, data);

    }
}
