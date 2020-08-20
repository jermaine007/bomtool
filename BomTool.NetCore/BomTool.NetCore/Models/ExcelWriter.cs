using Noone.UI.Models;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BomTool.NetCore.Models
{
    /// <summary>
    /// Excel writer, to generate bom data
    /// </summary>
    public class ExcelWriter : ModelBase
    {
        /// <summary>
        /// Destination folder
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// Original bom data
        /// </summary>
        public IEnumerable<BomData> Data { get; set; }

        /// <summary>
        /// Group bom data
        /// </summary>
        public IEnumerable<GrouppedBomData> GrouppedData { get; set; }

        /// <summary>
        /// Log something to show on the status bar
        /// </summary>
        public Action<string> Log { get; set; }

        /// <summary>
        /// Initialize writer
        /// </summary>
        /// <param name="data"></param>
        /// <param name="folder"></param>
        /// <param name="Log"></param>
        public void Initialize(IEnumerable<BomData> data, string folder, Action<string> Log)
        {
            this.Folder = folder;
            this.Data = data;
            this.Log = Log;
        }

        /// <summary>
        /// Write the new bom data to xls
        /// </summary>
        public Task Write() => Task.Factory.StartNew(() =>
        {
            Log("Generating BOM...");
            this.GrouppedData = this.Data.Group(Log);

            WriteData(this.GrouppedData);
            Log("Generated BOM successfully.");
        });


        /// <summary>
        /// WriteData
        /// </summary>
        /// <param name="grouppedData"></param>
        private void WriteData(IEnumerable<GrouppedBomData> grouppedData)
        {
            var path = Path.Combine(Folder, $"Bom-{DateTime.Now:yyyyMMddHHmmss}.xls");
            var workbook = new HSSFWorkbook();
            Log($"Generate file to : {path}");

            foreach (var data in grouppedData)
            {
                var name = $"{data.Line}@Col{data.ColumnIndex}";
                Log($"Writing worksheet: {name}");
                var sheet = workbook.CreateSheet(name);
                var headerRow = sheet.CreateRow(0);

                headerRow.CreateCell(0).SetCellValue("Code");
                //headerRow.CreateCell(1).SetCellValue("Type");
                headerRow.CreateCell(1).SetCellValue("Description");
                headerRow.CreateCell(2).SetCellValue("Value");
                headerRow.CreateCell(3).SetCellValue("References");
                headerRow.CreateCell(4).SetCellValue("Count");

                for (int i = 0; i < data.Data.Count(); i++)
                {
                    var row = sheet.CreateRow(i + 1);
                    var item = data.Data.ElementAt(i);
                    row.CreateCell(0).SetCellValue(item.Code);
                    //row.CreateCell(1).SetCellValue(item.Type);
                    row.CreateCell(1).SetCellValue(item.Description);
                    row.CreateCell(2).SetCellValue(item.Value);
                    row.CreateCell(3).SetCellValue(item.Reference);
                    row.CreateCell(4).SetCellValue(item.Count);
                }
            }

            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                workbook.Write(fs);
            }
        }

    }
}
