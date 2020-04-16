﻿using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BomTool.Models
{
    class ExcelReader
    {
        public string XlsPath { get; private set; }
        public Action<string> Log { get; set; }

        public ExcelReader(string xlsPath, Action<string> Log)
        {

            this.Log = Log;
            this.XlsPath = xlsPath;
        }

        public IEnumerable<ExcelData> Read()
        {
            var result = new List<ExcelData>();
            Log($"Opening {this.XlsPath} ...");
            IWorkbook workbook = Path.GetExtension(this.XlsPath) == ".xls" ?
                (IWorkbook)new HSSFWorkbook(File.OpenRead(this.XlsPath)) : new XSSFWorkbook(File.OpenRead(this.XlsPath));

            var worksheet = workbook.GetSheetAt(0);

            var startRowIndex = FindStartRowIndex("Reference", worksheet);
            if (startRowIndex == -1)
            {
                return Enumerable.Empty<ExcelData>();
            }
            int rowCount = worksheet.LastRowNum;
            var lastColumnIndex = FindLastColumnIndex(startRowIndex, worksheet);
            for (int row = startRowIndex; row <= rowCount; row++)
            {
                var rowData = worksheet.GetRow(row);
                var data = new ExcelData
                {
                    Reference = rowData.GetCell(0)?.StringCellValue,
                    Code = rowData.GetCell(1)?.StringCellValue,
                    Type = rowData.GetCell(2)?.StringCellValue,
                    Description = rowData.GetCell(3)?.StringCellValue,
                    Value = rowData.GetCell(4)?.StringCellValue,

                    Pth = rowData.GetCell(5)?.StringCellValue,
                    Smd = rowData.GetCell(6)?.StringCellValue

                };
                for (int columnIndex = 5; columnIndex < lastColumnIndex + 1; columnIndex++)
                {
                    data.Lines.Add(rowData.GetCell(columnIndex)?.StringCellValue);
                }
                result.Add(data);
            }

            var header = result.First();
            var aboveRow = worksheet.GetRow(startRowIndex - 1);

            for (int columnIndex = 5; columnIndex < lastColumnIndex - columnIndex + 1; columnIndex++)
            {

                var line = aboveRow.GetCell(columnIndex)?.StringCellValue;
                header.Lines[columnIndex - 5] = $"({line}){header.Lines[columnIndex - 5]}";
            }
            header.Pth = $"{header.Pth}(PTH)";
            header.Smd = $"{header.Smd}(SMD)";
            Log("Opened successfully.");
            return result;
        }

        private int FindLastColumnIndex(int startRowIndex, ISheet worksheet) => worksheet.GetRow(startRowIndex).LastCellNum - 1;

        private int FindStartRowIndex(string identity, ISheet worksheet)
        {
            int rowCount = worksheet.LastRowNum;
            for (int row = 1; row < rowCount; row++)
            {
                if (worksheet.GetRow(row).GetCell(0)?.StringCellValue == identity)
                {
                    return row;
                }
            }
            return -1;
        }
    }
}

