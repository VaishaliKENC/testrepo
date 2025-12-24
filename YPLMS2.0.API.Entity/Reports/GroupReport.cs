/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author: SHaileSH
* Created:<27/11/09>
* Last Modified:
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
   public class GroupReport : BaseEntity
    {
        public GroupReport()
        {
            _listReportParameterGroup = new List<ReportParameterGroup>();
            _listReportSelectedColumns = new List<ReportSelectedColumns>();
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            DeactivateReport,
            ActivateReport,
            DeleteReport
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetReportPrintExportOption,
            GetByName,
            Add,
            UpdateGroupReport,
            UpdateAllStatus,
            Delete,
            UpdateIsPrivate
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

        private string _strReportDesc;
        /// <summary>
        /// ReportDerscription
        /// </summary>
        public string ReportDescription
        {
            get { return _strReportDesc; }
            set { if (this._strReportDesc != value) { _strReportDesc = value; } }
        }

        private bool _bIsActive;
        /// <summary>
        /// Is Active
        /// </summary>
        public bool IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
        }

        private bool _bIsPrivate;
        /// <summary>
        /// Is Private
        /// </summary>
        public bool IsPrivate
        {
            get { return _bIsPrivate; }
            set { if (this._bIsPrivate != value) { _bIsPrivate = value; } }
        }

        private bool _bIsPrint;
        /// <summary>
        /// Is Print
        /// </summary>
        public bool IsPrint
        {
            get { return _bIsPrint; }
            set { if (this._bIsPrint != value) { _bIsPrint = value; } }
        }

        private bool _bIsHTML;
        /// <summary>
        /// Is HTML
        /// </summary>
        public bool IsHTML
        {
            get { return _bIsHTML; }
            set { if (this._bIsHTML != value) { _bIsHTML = value; } }
        }

        private bool _bIsPDF;
        /// <summary>
        /// Is Private
        /// </summary>
        public bool IsPDF
        {
            get { return _bIsPDF; }
            set { if (this._bIsPDF != value) { _bIsPDF = value; } }
        }

        private bool _bIsCSV;
        /// <summary>
        /// Is CSV
        /// </summary>
        public bool IsCSV
        {
            get { return _bIsCSV; }
            set { if (this._bIsCSV != value) { _bIsCSV = value; } }
        }

        private bool _bIsExcel;
        /// <summary>
        /// Is Excel
        /// </summary>
        public bool IsExcel
        {
            get { return _bIsExcel; }
            set { if (this._bIsExcel != value) { _bIsExcel = value; } }
        }

        private bool _bIsEmail;
        /// <summary>
        /// Is Email
        /// </summary>
        public bool IsEmail
        {
            get { return _bIsEmail; }
            set { if (this._bIsEmail != value) { _bIsEmail = value; } }
        }

        private List<ReportParameterGroup> _listReportParameterGroup;
        /// <summary>
        /// ReportParameterGroup List
        /// </summary>
        public List<ReportParameterGroup> ReportParameterGroupList
        {
            get { return _listReportParameterGroup; }
        }

        private List<ReportSelectedColumns> _listReportSelectedColumns;
        /// <summary>
        /// Report Selected Columns List
        /// </summary>
        public List<ReportSelectedColumns> ReportSelectedColumnList
        {
            get { return _listReportSelectedColumns; }
        }
    }
}