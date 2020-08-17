using Noone.UI;
using Noone.UI.Core;
using Noone.UI.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BomTool.NetCore.Models
{
    /// <summary>
    /// Bom data reader
    /// </summary>
    public class ExcelDataReader : ModelBase,
        IContainerProvider,
        ILoggerProvider
    {
        public static readonly string InsertType = "INSERT";
        public static readonly string SurfaceType = "SURFACE";
        private readonly IContainer container;
        private readonly ILogger logger;

        /// <summary>
        /// Excel path
        /// </summary>
        public string XlsPath { get; private set; }

        /// <summary>
        /// Log something to show on the status bar
        /// </summary>
        public Action<string> Log { get; set; }

        public ExcelDataReader()
        {
            container = ((IContainerProvider)this).Container;
            logger = ((ILoggerProvider)this).Logger;
        }

        public void Initialize(string xlsPath, Action<string> Log)
        {
            this.Log = Log;
            this.XlsPath = xlsPath;
        }

        /// <summary>
        /// Read bom data from 1st sheet
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="evaluator"></param>
        /// <returns></returns>
        private IEnumerable<BomData> ReadBom(IWorkbook workbook, IFormulaEvaluator evaluator)
        {
            var worksheet = workbook.GetSheetAt(0);
            var result = new List<BomData>();
            var startRowIndex = FindStartRowIndex("Reference", worksheet);
            if (startRowIndex == -1)
            {
                return Enumerable.Empty<BomData>();
            }
            int rowCount = worksheet.LastRowNum;
            var lastColumnIndex = FindLastColumnIndex(startRowIndex, worksheet);
            logger.Debug($"Last column:{lastColumnIndex}");

            for (int row = startRowIndex; row <= rowCount; row++)
            {
                var rowData = worksheet.GetRow(row);
                var type = GetValue(rowData.GetCell(2), evaluator);
                var description = GetValue(rowData.GetCell(3), evaluator);
                var value = GetValue(rowData.GetCell(4), evaluator);
                if (type == SurfaceType)
                {
                    description = $"{description}  {value}";
                    value = string.Empty;
                }

                var data = container.Get<BomData>().Setup(o =>
                {
                    o.Reference = GetValue(rowData.GetCell(0), evaluator);
                    o.Code = GetValue(rowData.GetCell(1), evaluator);
                    o.Type = type;
                    o.Description = description;
                    o.Value = value;
                });

                for (int columnIndex = 5; columnIndex < lastColumnIndex + 1; columnIndex++)
                {
                    data.Lines.Add(GetValue(rowData.GetCell(columnIndex), evaluator));
                }
                logger.Debug($"Read data: {data}");
                result.Add(data);
            }

            var header = result.First();
            var aboveRow = worksheet.GetRow(startRowIndex - 1);

            for (int columnIndex = 5; columnIndex < lastColumnIndex + 1; columnIndex++)
            {
                var line = aboveRow.GetCell(columnIndex)?.StringCellValue;
                header.Lines[columnIndex - 5] = $"({line}){header.Lines[columnIndex - 5]}";
            }
            return result;
        }

        /// <summary>
        /// Read information for 2nd sheet
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="evaluator"></param>
        /// <returns></returns>
        private Information ReadInformation(IWorkbook workbook, IFormulaEvaluator evaluator)
        {
            logger.Debug("Read Information");
            var worksheet = workbook.GetSheetAt(1);
            var startRowIndex = FindStartRowIndex("Information", worksheet);
            if (startRowIndex == -1)
            {
                return Information.Empty;
            }
            var index = startRowIndex + 1;
            var info = container.Get<Information>().Setup(o =>
            {
                o.Picture = Path.Combine(Path.GetDirectoryName(this.XlsPath),
                    GetValue(worksheet.GetRow(index).GetCell(1), evaluator));
                o.Approver = GetValue(worksheet.GetRow(index + 1).GetCell(1), evaluator);
                o.ApproveDate = GetValue(worksheet.GetRow(index + 2).GetCell(1), evaluator, true);
                o.PreparedBy = GetValue(worksheet.GetRow(index + 3).GetCell(1), evaluator);
                o.PreparedDate = GetValue(worksheet.GetRow(index + 4).GetCell(1), evaluator, true);
                o.DrawingNo = GetValue(worksheet.GetRow(index + 5).GetCell(1), evaluator);
                o.Version = GetValue(worksheet.GetRow(index + 6).GetCell(1), evaluator);
            });

            logger.Debug($"Read information: {info}");
            return info;
        }

        /// <summary>
        /// Read history from 2nd sheet
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="evaluator"></param>
        /// <returns></returns>
        private IEnumerable<HistoryData> ReadHistory(IWorkbook workbook, IFormulaEvaluator evaluator)
        {
            var worksheet = workbook.GetSheetAt(1);
            var startRowIndex = FindStartRowIndex("History", worksheet);
            if (startRowIndex == -1)
            {
                return Enumerable.Empty<HistoryData>();
            }

            var rowCount = worksheet.LastRowNum;
            var result = new List<HistoryData>();
            for (int index = startRowIndex + 2; index <= rowCount; index++)
            {
                var row = worksheet.GetRow(index);
                // if all cells is blank break the loop
                if (row.Cells.All(c => c.CellType == CellType.Blank))
                {
                    break;
                }
                var data = container.Get<HistoryData>().Setup(o =>
                {
                    o.Version = GetValue(row.GetCell(0), evaluator);
                    o.Reason = GetValue(row.GetCell(1), evaluator);
                    o.Signature = GetValue(row.GetCell(2), evaluator);
                    o.Date = GetValue(row.GetCell(3), evaluator, true);
                });
                logger.Debug(data.ToString());
                result.Add(data);

            }

            return result;
        }

        /// <summary>
        /// Get cell value according to cell type
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="evaluator"></param>
        /// <param name="isDate"></param>
        /// <returns></returns>
        private string GetValue(ICell cell, IFormulaEvaluator evaluator = null, bool isDate = false) => cell?.CellType switch
        {
            CellType.String => cell?.StringCellValue,
            CellType.Numeric => isDate ? cell?.DateCellValue.ToString("yyyy/M/d") : cell?.NumericCellValue.ToString("f0"),
            CellType.Boolean => cell?.BooleanCellValue.ToString(),
            CellType.Formula => cell == null ? string.Empty : GetValue(evaluator.Evaluate(cell)),
            _ => string.Empty
        };

        /// <summary>
        /// Get cell value according to cell type
        /// </summary>
        /// <param name="cellValue"></param>
        /// <returns></returns>
        private string GetValue(CellValue cellValue) => cellValue?.CellType switch
        {
            CellType.String => cellValue?.StringValue,
            CellType.Numeric => cellValue?.NumberValue.ToString("f0"),
            CellType.Boolean => cellValue?.BooleanValue.ToString(),
            _ => string.Empty
        };

        /// <summary>
        /// Read all data
        /// </summary>
        /// <returns></returns>
        public ExcelData Read()
        {
            Log($"Opening {this.XlsPath} ...");
            logger.Debug($"Reading file {this.XlsPath} ..");

            IWorkbook workbook = Path.GetExtension(this.XlsPath) == ".xls" ?
                (IWorkbook)new HSSFWorkbook(File.OpenRead(this.XlsPath)) : new XSSFWorkbook(File.OpenRead(this.XlsPath));

            IFormulaEvaluator evaluator = Path.GetExtension(this.XlsPath) == ".xls" ?
                (IFormulaEvaluator)new HSSFFormulaEvaluator(workbook) : new XSSFFormulaEvaluator(workbook);

            var result = container.Get<ExcelData>().Setup(o =>
            {
                o.BomData = ReadBom(workbook, evaluator);
                o.Information = ReadInformation(workbook, evaluator);
                o.HistoryData = ReadHistory(workbook, evaluator);
            });
            Log("Opened successfully.");
            return result;
        }

        /// <summary>
        /// Find last column index
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="worksheet"></param>
        /// <returns></returns>
        private int FindLastColumnIndex(int startRowIndex, ISheet worksheet) => worksheet.GetRow(startRowIndex).LastCellNum - 1;

        /// <summary>
        /// Find the start row by specified word
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="worksheet"></param>
        /// <returns></returns>
        private int FindStartRowIndex(string identity, ISheet worksheet)
        {
            logger.Debug($"Try to find the start row.");
            int rowCount = worksheet.LastRowNum;
            for (int row = 0; row <= rowCount; row++)
            {
                if (worksheet.GetRow(row).GetCell(0)?.StringCellValue == identity)
                {
                    logger.Debug($"Start row found: {row}");
                    return row;
                }
            }
            return -1;
        }
    }
}
