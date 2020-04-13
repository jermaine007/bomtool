using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BomTool.Models
{
    class ExcelWriter
    {
        public string Folder { get; set; }

        public List<ExcelData> Data { get; set; }

        public Action<string> Log { get; set; }

        public ExcelWriter(IEnumerable<ExcelData> data, string folder, Action<string> Log)
        {
            this.Folder = folder;
            this.Data = data;
            this.Log = Log;
        }

        public void Write()
        {
            Log("Generating BOM...");
            var header = Data.ElementAt(0);
            var pthCode = header.Pth.Replace("(PTH)", "");
            var smdCode = header.Pth.Replace("(SMD)", "");
            WriteFile(pthCode, GenerateData("PTH"));
            WriteFile(smdCode, GenerateData("SMD"));
            Log("Generated BOM successfully.");
        }

        public void WriteFile(string code, IEnumerable<ExcelData> data)
        {
            var path = Path.Combine(Folder, $"{code}.xls");
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet();
            var headerRow = sheet.CreateRow(0);
            
            headerRow.CreateCell(0).SetCellValue("Code");
            headerRow.CreateCell(1).SetCellValue("Type");
            headerRow.CreateCell(2).SetCellValue("Description");
            headerRow.CreateCell(3).SetCellValue("Value");
            headerRow.CreateCell(4).SetCellValue("References");

            for (int i = 0; i < data.Count(); i++)
            {
                var row = sheet.CreateRow(i+1);
                var item = data.ElementAt(i);
                row.CreateCell(0).SetCellValue(item.Code);
                row.CreateCell(1).SetCellValue(item.Type);
                row.CreateCell(2).SetCellValue(item.Description);
                row.CreateCell(3).SetCellValue(item.Value);
                row.CreateCell(4).SetCellValue(item.Reference);
            }

            workbook.Write(File.Create(path));
        }

        private IEnumerable<ExcelData> GenerateData(string line)
        {
            Log($"Parsing data for {line}...");
            var result = new List<ExcelData>();
            var filteredData = Data.Where(o =>
            {
                var l = line == "PTH" ? o.Pth : o.Smd;
                return string.IsNullOrEmpty(l);
            }).GroupBy(o => o.Code);

            foreach (var group in filteredData)
            {
                var first = group.First();
                var references = string.Join(",", group.AsEnumerable());
                result.Add(new ExcelData
                {
                    Reference = references,
                    Code = first.Code,
                    Type = first.Type,
                    Description = first.Description,
                    Value = first.Value

                });
            }
            return result;

        }
    }
}
