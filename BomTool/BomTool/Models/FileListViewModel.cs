using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BomTool.Models
{
    [AutoRegisterType]
    public class FileListViewModel
    {
        public List<string> Paths { get; private set; } = new List<string>();

        public void MakeTestData()
        {
            Func<IEnumerable<string>> generate = () => Directory.GetFiles(@"E:\Jermaine\Interface", "*.lua", SearchOption.AllDirectories);
            this.Paths.AddRange(generate().Take(50));
        }
    }
}
