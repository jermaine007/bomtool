using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace BomTool.NetCore.Models
{
    public class FileItem
    {

        private readonly static string HistoryFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "__BOM__TOOL__");

        public string Name { get; private set; }

        public string Location { get; private set; }

        public FileItem()
        {
            if (!File.Exists(HistoryFile))
            {
                FileStream stream = File.Create(HistoryFile);
                stream.Close();
            }
        }

        public static IEnumerable<FileItem> Load()
            => File.ReadAllLines(HistoryFile)
                .Where(f => File.Exists(f))
                .Select(f => new FileItem
                {
                    Name = Path.GetFileName(f),
                    Location = f
                })
                .ToList();

        public void Save(IEnumerable<FileItem> items) => File.WriteAllLines(HistoryFile, items.Select(f => f.Location));
    }
}
