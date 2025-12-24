/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author: 
* Created:
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class ReportConfig : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public ReportConfig()
        { 

        }
        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get
        }

        private string _strReportName;
        /// <summary>
        /// Report Name
        /// </summary>
        public string ReportName
        {
            get { return _strReportName; }
            set { if (this._strReportName != value) { _strReportName = value; } }
        }

        private string _strColumnDataTypes;
        /// <summary>
        /// ColumnDataTypes
        /// </summary>
        public string ColumnDataTypes
        {
            get { return _strColumnDataTypes; }
            set { if (this._strColumnDataTypes != value) { _strColumnDataTypes = value; } }
        }

        private string _strColumns;
        /// <summary>
        /// Columns
        /// </summary>
        public string Columns
        {
            get { return _strColumns; }
            set { if (this._strColumns != value) { _strColumns = value; } }
        }

        private string _strColumnsHT;
        /// <summary>
        /// ColumnsHT
        /// </summary>
        public string ColumnsHT
        {
            get { return _strColumnsHT; }
            set { if (this._strColumnsHT != value) { _strColumnsHT = value; } }
        }

        private string _strColumnWidth;
        /// <summary>
        /// ColumnWidth
        /// </summary>
        public string ColumnWidth
        {
            get { return _strColumnWidth; }
            set { if (this._strColumnWidth != value) { _strColumnWidth = value; } }
        }

        private string _strAlign;
        /// <summary>
        /// Align
        /// </summary>
        public string Align
        {
            get { return _strAlign; }
            set { if (this._strAlign != value) { _strAlign = value; } }
        }

        private string _strReportWidth;
        /// <summary>
        /// reportWidth
        /// </summary>
        public string ReportWidth
        {
            get { return _strReportWidth; }
            set { if (this._strReportWidth != value) { _strReportWidth = value; } }
        }

    }
}
