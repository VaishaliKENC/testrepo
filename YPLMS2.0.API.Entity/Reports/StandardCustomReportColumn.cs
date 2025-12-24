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
     public class StandardCustomReportColumn : StandardReportColumn
    {
        /// <summary>
        /// Default Contructor tblStandardCustomReportColumns
        /// <summary>
        public StandardCustomReportColumn()
        { }

        private string _columnHeaderText;
        public string CustomColumnHeaderText
        {
            get { return _columnHeaderText; }
            set { if (this._columnHeaderText != value) { _columnHeaderText = value; } }
        }

        private string _columnId;
        /// <summary>
        /// Parent Standard Report Column Id 
        /// </summary>
        public string ColumnId
        {
            get { return _columnId; }
            set { if (this._columnId != value) { _columnId = value; } }
        }

        private string _customReportId;
        /// <summary>
        /// Parent Custom Report Id
        /// </summary>
        public string CustomReportId
        {
            get { return _customReportId; }
            set { if (this._customReportId != value) { _customReportId = value; } }
        }

        private int _columnSortOrder;
        public int ColumnSortOrder
        {
            get { return _columnSortOrder; }
            set { if (this._columnSortOrder != value) { _columnSortOrder = value; } }
        }
        // added by Gitanjali 13.10.2010
        private string _columnDataType;
        public string ColumnDataType
        {
            get { return _columnDataType; }
            set { if (this._columnDataType != value) { _columnDataType = value; } }
        }

        // added by Gitanjali 20.10.2010
        private int _columnDisplayOrder;
        public int ColumnDisplayOrder
        {
            get { return _columnDisplayOrder; }
            set { if (this._columnDisplayOrder != value) { _columnDisplayOrder = value; } }
        }
        // added by Gitanjali 20.12.2010
        private string _columnType;
        public string ColumnType
        {
            get { return _columnType; }
            set { if (this._columnType != value) { _columnType = value; } }
        }
    }
}