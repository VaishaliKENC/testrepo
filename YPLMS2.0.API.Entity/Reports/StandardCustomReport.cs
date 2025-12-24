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
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class StandardCustomReport:StandardReport 
    {
        /// <summary>
        /// Default Contructor tblStandardCustomReportMaster
        /// <summary>
        public StandardCustomReport()
        {
            _customColumns = new List<StandardCustomReportColumn>();  
        }
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete,
            SelByName
        }
        public new enum ListMethod
        {
            GetAllCustStdReport,
            GetAllCustStdReportAdminHome,
            GetSharedCustReport
        }

        private string _customReportId;
        public string CustomReportId
        {
            get { return _customReportId; }
            set { if (this._customReportId != value) { _customReportId = value; } }
        }


        private string _customReportName;
        public string CustomReportName
        {
            get { return _customReportName; }
            set { if (this._customReportName != value) { _customReportName = value; } }
        }


        private string _customReportDescription;
        public string CustomReportDescription
        {
            get { return _customReportDescription; }
            set { if (this._customReportDescription != value) { _customReportDescription = value; } }
        }

        private int _iPageSize;
        public int PageSize
        {
            get { return _iPageSize; }
            set { if (this._iPageSize != value) { _iPageSize = value; } }
        }


        private string _sortOn1ColumnId;
        public string SortOn1ColumnId
        {
            get { return _sortOn1ColumnId; }
            set { if (this._sortOn1ColumnId != value) { _sortOn1ColumnId = value; } }
        }

       


        private string _sortOn2ColumnId;
        public string SortOn2ColumnId
        {
            get { return _sortOn2ColumnId; }
            set { if (this._sortOn2ColumnId != value) { _sortOn2ColumnId = value; } }
        }


        private SortDirection _sort1Direction;
        public SortDirection Sort1Direction
        {
            get { return _sort1Direction; }
            set { if (this._sort1Direction != value) { _sort1Direction = value; } }
        }


        private SortDirection _sort2Direction;
        public SortDirection Sort2Direction
        {
            get { return _sort2Direction; }
            set { if (this._sort2Direction != value) { _sort2Direction = value; } }
        }


        private bool _isPrivate;
        public bool IsPrivate
        {
            get { return _isPrivate; }
            set { if (this._isPrivate != value) { _isPrivate = value; } }
        }


        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
        }


        private string _reportId;
        /// <summary>
        /// Parent Standard Report Id
        /// </summary>
        public string ReportId
        {
            get { return _reportId; }
            set { if (this._reportId != value) { _reportId = value; } }
        }

        private string _strReportType;
        /// <summary>
        /// Report Type
        /// </summary>
        public string ReportType
        {
            get { return _strReportType; }
            set { if (this._strReportType != value) { _strReportType = value; } }
        }

        /// <summary>
        /// Base Report Name from which the Report is Derived.
        /// </summary>
        private string _baseReport;
        public string BaseReport
        {
            get { return _baseReport; }
            set { if (this._baseReport != value) { _baseReport = value; } }
        }



        private DateTime _dateCreatedTo;
        /// <summary>
        /// Date Created To For Passing Parameter for Shared Reports
        /// </summary>
        public DateTime DateCreatedTo
        {
            get { return _dateCreatedTo; }
            set { if (this._dateCreatedTo != value) { _dateCreatedTo = value; } }
        }


        private DateTime _dateLastModifiedTo;
        /// <summary>
        /// Last Modified Date To For passing Parameter for Shared Reports
        /// </summary>
        public DateTime LastModifiedDateTo
        {
            get { return _dateLastModifiedTo; }
            set { if (this._dateLastModifiedTo != value) { _dateLastModifiedTo = value; } }
        }


        private bool _isAllReportVisible;
        public bool IsAllReportVisible
        {
            get { return _isAllReportVisible; }
            set { if (this._isAllReportVisible != value) { _isAllReportVisible = value; } }
        }



        private List<StandardCustomReportColumn> _customColumns;
        public List<StandardCustomReportColumn> CustomColumns
        {
            get { return _customColumns; }
        }


        public enum SortDirection
        {
            Asc,
            Desc
        }
        
    }
}
