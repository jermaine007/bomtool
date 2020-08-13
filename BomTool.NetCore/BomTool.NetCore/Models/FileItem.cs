using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using NooneUI.Framework;

namespace BomTool.NetCore.Models
{
    public class FileItem : ModelBase
    {
        private readonly static string HistoryFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "__BOM__TOOL__");

        public string Name { get; private set; }

        public string Location { get; private set; }

        static FileItem()
        {
            if (!File.Exists(HistoryFile))
            {
                FileStream stream = File.Create(HistoryFile);
                stream.Close();
            }
        }

        public static FileItem Create(string file) => new FileItem
        {
            Name = $"{Directory.GetParent(file).Name}{Path.DirectorySeparatorChar}{Path.GetFileName(file)}",
            Location = file
        };

        /// <summary>
        /// Load the history
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<FileItem> Load()
            => File.ReadAllLines(HistoryFile)
                .Where(f => File.Exists(f))
                .Select(f => FileItem.Create(f))
                .ToList();

        /// <summary>
        /// Save the history
        /// </summary>
        /// <param name="items"></param>
        public static void Save(IEnumerable<FileItem> items) => File.WriteAllLines(HistoryFile, items.Select(f => f.Location));
    }
}
