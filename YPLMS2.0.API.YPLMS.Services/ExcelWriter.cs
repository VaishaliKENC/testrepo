using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.YPLMS.Services
{
    public class ExcelWriter : IDisposable
    {
        HSSFWorkbook hssfworkbook;
        public const string ExcelContentType = "application/vnd.ms-excel";
        public const string CSVContentType = "text/csv";
        const int MAX_EXCEL_ROWCOUNT = 65536;
        public MemoryStream streamFile;

        /// <summary>
        /// Distructore
        /// </summary>
        public void Dispose()
        {
            if (streamFile != null)
            {
                streamFile.Close();
            }
        }

        #region Export to Excel
        /// <summary>
        /// Converts Data to Excelsheet
        /// Response.ContentType = ExcelWriter.ExcelContentType;
        /// Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", pfileName));
        /// Response.Clear();
        /// Response.BinaryWrite(ExportToXMLExcel(dtData).GetBuffer());
        /// Response.End();
        /// </summary>
        /// <param name="dtData">DataTable</param>
        /// <returns>MemoryStream</returns>
        public MemoryStream ExportToXMLExcel(DataTable dtData)
        {
            //Write the stream data of workbook to the root directory
            hssfworkbook = new HSSFWorkbook();
            streamFile = new MemoryStream();
            try
            {
                WriteData(dtData);
                hssfworkbook.Write(streamFile);
            }
            catch (Exception expCommon)
            {
                CustomException expCustom = new CustomException(Messages.Common.FILE_XSL_INVALID, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expCommon, true);
                throw expCustom;
            }
            return streamFile;
        }

        /// <summary>
        /// Write Data to Worksheet
        /// </summary>
        /// <param name="pDtData">DataTable</param>
        private void WriteData(DataTable pDtData)
        {
            int indexC = 0;
            int indexR = 1;
            HSSFSheet sheet1 = CreateSheet(pDtData);
            indexC = 0;
            foreach (DataRow dtrow in pDtData.Rows)
            {
                HSSFRow row = (HSSFRow)sheet1.CreateRow(indexR);
                foreach (DataColumn column in pDtData.Columns)
                {
                    if (!dtrow.IsNull(column))
                    {
                        switch (column.DataType.Name)
                        {
                            case "DateTime":
                                row.CreateCell(indexC).SetCellValue(Convert.ToDateTime(dtrow[column]));
                                break;
                            case "Double":
                                row.CreateCell(indexC).SetCellValue(Convert.ToDouble(dtrow[column]));
                                break;
                            case "Boolean":
                                row.CreateCell(indexC).SetCellValue(Convert.ToBoolean(dtrow[column]));
                                break;
                            default:
                                row.CreateCell(indexC).SetCellValue(dtrow[column].ToString());
                                break;
                        }
                    }
                    else
                    {
                        row.CreateCell(indexC).SetCellValue("");
                    }
                    indexC++;
                }
                indexC = 0;
                indexR++;
                if (indexR >= MAX_EXCEL_ROWCOUNT)
                {
                    sheet1 = CreateSheet(pDtData);
                    indexR = 1;
                }
            }
        }

        /// <summary>
        /// Create new Sheet
        /// </summary>
        /// <param name="pDTable">DataTable</param>
        /// <returns>Shhet</returns>
        private HSSFSheet CreateSheet(DataTable pDTable)
        {
            const string SHEET_NAME = "Sheet";
            int indexC = 0;
            int iSheet = hssfworkbook.NumberOfSheets + 1;
            HSSFSheet sheet1 = (HSSFSheet)hssfworkbook.CreateSheet(SHEET_NAME + iSheet.ToString());
            sheet1.CreateRow(0);
            foreach (DataColumn column in pDTable.Columns)
            {
                sheet1.GetRow(0).CreateCell(indexC).SetCellValue(column.ColumnName);
                indexC++;
            }
            return sheet1;
        }
        #endregion

        /// <summary>
        /// Write Table to Memory Stream
        /// </summary>
        /// <param name="pdataTable">DataTable</param>
        /// <returns>Stream</returns>
        public byte[] ExportToCSV(DataTable pdataTable)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int iCurrColCount = 1;
            foreach (DataColumn column in pdataTable.Columns)
            {

                stringBuilder.Append(column.ColumnName);
                if (iCurrColCount < pdataTable.Columns.Count)
                {
                    stringBuilder.Append(",");
                }
                iCurrColCount++;
            }
            stringBuilder.Append(Environment.NewLine);
            iCurrColCount = 1;
            foreach (DataRow dr in pdataTable.Rows)
            {
                iCurrColCount = 1;
                foreach (DataColumn column in pdataTable.Columns)
                {
                    if (!dr.IsNull(column))
                    {
                        stringBuilder.Append(dr[column].ToString());
                    }
                    if (iCurrColCount < pdataTable.Columns.Count)
                    {
                        stringBuilder.Append(",");
                    }
                    iCurrColCount++;
                }
                stringBuilder.Append(Environment.NewLine);
            }
            //Commeted by Vasudha
            //byte[] byteArray = Encoding.ASCII.GetBytes(stringBuilder.ToString());

            //Changed to Windows 1252 for Latin1 of Sql database:Vasudha
            byte[] byteArray = Encoding.GetEncoding(1252).GetBytes(stringBuilder.ToString());

            return byteArray;
        }
    }
}
