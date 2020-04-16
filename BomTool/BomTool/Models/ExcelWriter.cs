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

        public IEnumerable<ExcelData> Data { get; set; }

        public IEnumerable<ExcelGrouppedData> GrouppedData { get; set; }

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
    
            var grouppedData = new List<ExcelGrouppedData>();
            int index = 0;

            foreach (var line in header.Lines)
            {
                grouppedData.Add(GroupData(index, line));
                index++;
            }

            WriteData(grouppedData);

            this.GrouppedData = grouppedData;


            Log("Generated BOM successfully.");
        }

       

        private void WriteData(IEnumerable<ExcelGrouppedData> grouppedData)
        {
            var path = Path.Combine(Folder, $"Bom-{DateTime.Now.ToString("yyyyMMddHHmmss")}.xls");
            var workbook = new HSSFWorkbook();

            foreach (var data in grouppedData)
            {
                var sheet = workbook.CreateSheet($"{ data.Line}@Col{data.ColumnIndex}");
                var headerRow = sheet.CreateRow(0);

                headerRow.CreateCell(0).SetCellValue("Code");
                headerRow.CreateCell(1).SetCellValue("Type");
                headerRow.CreateCell(2).SetCellValue("Description");
                headerRow.CreateCell(3).SetCellValue("Value");
                headerRow.CreateCell(4).SetCellValue("References");

                for (int i = 0; i < data.Data.Count(); i++)
                {
                    var row = sheet.CreateRow(i + 1);
                    var item = data.Data.ElementAt(i);
                    row.CreateCell(0).SetCellValue(item.Code);
                    row.CreateCell(1).SetCellValue(item.Type);
                    row.CreateCell(2).SetCellValue(item.Description);
                    row.CreateCell(3).SetCellValue(item.Value);
                    row.CreateCell(4).SetCellValue(item.Reference);
                }
            }

            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                workbook.Write(fs);
            }
        }

        private ExcelGrouppedData GroupData(int index, string line)
        {
            Log($"Parsing data for {line}...");
            var result = new List<ExcelData>();
            var filteredData = Data.Where(o =>
            {
                var l = o.Lines[index];
                return string.IsNullOrEmpty(l);
            }).GroupBy(o => o.Code);

            foreach (var group in filteredData)
            {
                var first = group.First();
                var references = string.Join(",", group.AsEnumerable().Select(o => o.Reference));
                result.Add(new ExcelData
                {
                    Reference = references,
                    Code = first.Code,
                    Type = first.Type,
                    Description = first.Description,
                    Value = first.Value

                });
            }
            return new ExcelGrouppedData(line, index, result);
        }

    }
}
