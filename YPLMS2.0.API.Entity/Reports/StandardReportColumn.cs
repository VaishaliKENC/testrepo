/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author: Ashish
* Created:<10/11/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
   public class StandardReportColumn : BaseEntity
    {
        /// <summary>
        /// Default Contructor tblStandardReportColumns
        /// <summary>
        public StandardReportColumn()
        { }

        private string _columnHeaderText;
        public string ColumnHeaderText
        {
            get { return _columnHeaderText; }
            set { if (this._columnHeaderText != value) { _columnHeaderText = value; } }
        }

        private string _columnDBFieldName;
        public string ColumnDBFieldName
        {
            get { return _columnDBFieldName; }
            set { if (this._columnDBFieldName != value) { _columnDBFieldName = value; } }
        }

        private string _reportId;
        public string ReportId
        {
            get { return _reportId; }
            set { if (this._reportId != value) { _reportId = value; } }
        }
    }
}