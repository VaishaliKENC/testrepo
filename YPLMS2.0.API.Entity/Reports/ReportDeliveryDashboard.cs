/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author: Ashish
* Created:<10/12/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
     public class ReportDeliveryDashboard : Task
    {
        /// <summary>
        /// Default Contructor tblReportDeliveryDashboard
        /// <summary>
        public ReportDeliveryDashboard()
        {
        }

        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete
        }
        public new enum ListMethod
        {
            GetAll
        }

        private string _reportId;
        public string ReportId
        {
            get { return _reportId; }
            set { if (this._reportId != value) { _reportId = value; } }
        }

        private ReportType _reportTypeSelected;
        public ReportType SelectedReportType
        {
            get { return _reportTypeSelected; }
            set { if (this._reportTypeSelected != value) { _reportTypeSelected = value; } }
        }

        private ReportFileFormat _fileExportFormat;
        public ReportFileFormat FileExportFormat
        {
            get { return _fileExportFormat; }
            set { if (this._fileExportFormat != value) { _fileExportFormat = value; } }
        }

        private string _generatedReportFileName;
        public string GeneratedReportFileName
        {
            get { return _generatedReportFileName; }
            set { if (this._generatedReportFileName != value) { _generatedReportFileName = value; } }
        }

        private string _taskId;
        public string TaskId
        {
            get { return _taskId; }
            set { if (this._taskId != value) { _taskId = value; } }
        }

        private string _distributionListId;
        public string DistributionListId
        {
            get { return _distributionListId; }
            set { if (this._distributionListId != value) { _distributionListId = value; } }
        }

        private string _emailTemplateID;
        public string EmailTemplateID
        {
            get { return _emailTemplateID; }
            set { if (this._emailTemplateID != value) { _emailTemplateID = value; } }
        }

        private string _parametersMapping;
        public string ParametersMapping
        {
            get { return _parametersMapping; }
            set { if (this._parametersMapping != value) { _parametersMapping = value; } }
        }

        private string _reportName;
        public string ReportName
        {
            get { return _reportName; }
            set { if (this._reportName != value) { _reportName = value; } }
        }

        private int _iNoOfRecordsInReport;
        public int NoOfRecordsInReport
        {
            get { return _iNoOfRecordsInReport; }
            set { if (this._iNoOfRecordsInReport != value) { _iNoOfRecordsInReport = value; } }
        }

        private DateTime _dRunStartTime;
        public DateTime RunStartTime
        {
            get { return _dRunStartTime; }
            set { if (this._dRunStartTime != value) { _dRunStartTime = value; } }
        }

        private DateTime _dNextRunDate;
        public DateTime NextRunDate
        {
            get { return _dNextRunDate; }
            set { if (this._dNextRunDate != value) { _dNextRunDate = value; } }
        }
        private bool _isInProcess;
        public bool IsInProcess
        {
            get { return _isInProcess; }
            set { if (this._isInProcess != value) { _isInProcess = value; } }
        }

        // added by Gitanjali 23.08.2010
        private bool _isDistributionToManager;
        public bool IsDistributionToManager
        {
            get
            {
                return _isDistributionToManager;
            }
            set { if (this._isDistributionToManager != value) { _isDistributionToManager = value; } }
        }

        //added by Gitanjali 7.10.2010
        private bool _isEmailDeliveryApproved;
        public bool IsEmailDeliveryApproved
        {
            get { return _isEmailDeliveryApproved; }
            set { if (this._isEmailDeliveryApproved !=value) { _isEmailDeliveryApproved =value ;} }
        }

        private string _paraPreferredDateFormat;
        public string PreferredDateFormat
        {
            get { return _paraPreferredDateFormat; }
            set { if (this._paraPreferredDateFormat != value) { _paraPreferredDateFormat = value; } }
        }


        private string _paraPreferredTimeZone;
        public string PreferredTimeZone
        {
            get { return _paraPreferredTimeZone; }
            set { if (this._paraPreferredTimeZone != value) { _paraPreferredTimeZone = value; } }
        }
    }

    public enum ReportType
    {
        Standard,
        StandardCustom,
        ReportTool
    }

    public enum ReportFileFormat
    {
        CSV,
        PDF,
        HTML,
        EXCEL
    }

    public enum ExecutionStatus
    {
        Active,
        Suspended,
        Complete
    }
}