using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.User
{
    public class ImportDefinationManager
    {
        public static Stream CreateCSVFile(DataTable dt)
        {
            MemoryStream sRetStream = new MemoryStream();
            StreamWriter sw = new StreamWriter(sRetStream, Encoding.UTF8);

            // Write headers
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sw.Write(dt.Columns[i].ColumnName);
                if (i < dt.Columns.Count - 1)
                    sw.Write(",");
            }
            sw.Write("\r\n");

            // Write rows
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    var value = dr[i].ToString().Replace("\"", "\"\""); // Escape quotes
                    sw.Write($"\"{value}\""); // Surround with quotes
                    if (i < dt.Columns.Count - 1)
                        sw.Write(",");
                }
                sw.Write("\r\n");
            }

            sw.Flush();
            sRetStream.Position = 0; // Important: reset position before returning
            return sRetStream;
        }

        //public static Stream CreateCSVFile(DataTable dt)
        //{
        //    Stream sRetStream = Stream.Null;
        //    StringBuilder sbCSV = new StringBuilder();
        //    int iRowCount = 1;
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        if (Convert.ToBoolean(dr["Include"]) || Convert.ToBoolean(dr["IsMandatory"]))
        //        {
        //            string strCSVCol = FromatDisplaytext(dr["FieldName"].ToString(), dr["FieldTypes"].ToString());
        //            if (sbCSV.ToString().IndexOf(strCSVCol) != -1)
        //            {
        //                sbCSV.Append(strCSVCol + iRowCount.ToString());
        //            }
        //            else
        //            {
        //                sbCSV.Append(strCSVCol);
        //            }
        //            if (iRowCount < dt.Rows.Count)
        //            {
        //                sbCSV.Append(",");
        //            }
        //        }
        //        iRowCount++;
        //    }
        //    byte[] byteArray = Encoding.ASCII.GetBytes(sbCSV.ToString());
        //    sRetStream = new MemoryStream(byteArray);
        //    return sRetStream;
        //}

        public static string FromatDisplaytext(string pstrText, string strFieldType)
        {
            string strDisplayText = string.Empty;
            try
            {
                if (strFieldType == "Standard" || strFieldType == "StandardCustom")
                {
                    if (pstrText != "UserNameAlias")
                    {
                        pstrText = pstrText.Replace("ID", string.Empty);
                        pstrText = pstrText.Replace("Id", string.Empty);
                        pstrText = pstrText.Replace("iD", string.Empty);
                        foreach (char chrFT in pstrText)
                        {
                            if (chrFT.ToString() == chrFT.ToString().ToUpper())
                            {
                                strDisplayText = strDisplayText + " " + chrFT.ToString();
                            }
                            else
                            {
                                strDisplayText = strDisplayText + chrFT.ToString();
                            }
                        }
                    }
                    else
                    {
                        strDisplayText = "Login ID";
                    }
                }
                else if (strFieldType == "Title")
                {
                    switch (pstrText)
                    {
                        //case "Standard":
                        //    strDisplayText = "Standard Fields";
                        //    break;
                        //case "CustomField":
                        //    strDisplayText = "Custom Fields";
                        //    break;
                        //case "OrgTreeLevels":
                        //    strDisplayText = "Organization Levels";
                        //    break;

                        case "Standard":
                            strDisplayText = "<h5>Standard Fields</h5>";
                            break;
                        case "CustomField":
                            strDisplayText = "<h5>Custom Fields</h5>";
                            break;
                        case "OrgTreeLevels":
                            strDisplayText = "<h5>Organization Levels</h5>";
                            break;
                    }
                }
                else
                {
                    strDisplayText = pstrText;
                }
            }
            catch { }
            return strDisplayText.Trim();
        }
    }
}
