/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author: Ashish Phate
* Created:<19/09/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    ///  class Task 
    /// </summary>
    [Serializable]
     public class Task : BaseEntity
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Task()
        {
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete,
            UpdateStatus,
            GetInProgressTaskCount,
            UpdateSetAbort
        }

        private string _strTaskName;
        /// <summary>
        /// Task Name
        /// </summary>
        public string TaskName
        {
            get { return _strTaskName; }
            set { if (this._strTaskName != value) { _strTaskName = value; } }
        }

        private string _strTaskType;
        /// <summary>
        /// Task Name
        /// </summary>
        public string TaskType
        {
            get { return _strTaskType; }
            set { if (this._strTaskType != value) { _strTaskType = value; } }
        }

        private string _strLastRunStatus;
        /// <summary>
        ///LastRunStatus
        /// </summary>
        public string LastRunStatus
        {
            get { return _strLastRunStatus; }
            set { if (this._strLastRunStatus != value) { _strLastRunStatus = value; } }
        }

        private ExecutionStatus _strExecutionStatus;
        /// <summary>
        /// Exectuon Status
        /// </summary>
        public ExecutionStatus ExecutionStatus
        {
            get { return _strExecutionStatus; }
            set { if (this._strExecutionStatus != value) { _strExecutionStatus = value; } }
        }

        private int _iDayOfWeek;
        /// <summary>
        /// Day Of Week
        /// </summary>
        public int DayOfWeek
        {
            get { return _iDayOfWeek; }
            set { if (this._iDayOfWeek != value) { _iDayOfWeek = value; } }
        }

        private int _iDayOftheMonth;
        /// <summary>
        /// Day Of Month
        /// </summary>
        public int DayOftheMonth
        {
            get { return _iDayOftheMonth; }
            set { if (this._iDayOftheMonth != value) { _iDayOftheMonth = value; } }
        }

        private int _iRecurrence;
        /// <summary>
        /// recurrence
        /// </summary>
        public int Recurrence
        {
            get { return _iRecurrence; }
            set { if (this._iRecurrence != value) { _iRecurrence = value; } }
        }

        private string _strTaskDescription;
        /// <summary>
        /// Task Description
        /// </summary>
        public string TaskDescription
        {
            get { return _strTaskDescription; }
            set { if (this._strTaskDescription != value) { _strTaskDescription = value; } }
        }

        private string _strBaseReportName;
        /// <summary>
        /// Task Description
        /// </summary>
        public string BaseReportName
        {
            get { return _strBaseReportName; }
            set { if (this._strBaseReportName != value) { _strBaseReportName = value; } }
        }


        private bool _bIsOneTime;
        /// <summary>
        /// One Time
        /// </summary>
        public bool IsOneTime
        {
            get { return _bIsOneTime; }
            set { if (this._bIsOneTime != value) { _bIsOneTime = value; } }
        }


        private bool _bIsImmediate = true;
        /// <summary>
        /// Is Immediate
        /// </summary>
        public bool IsImmediate
        {
            get { return _bIsImmediate; }
            set { if (this._bIsImmediate != value) { _bIsImmediate = value; } }
        }


        private bool _bIsDaily = true;
        /// <summary>
        /// Is Daily
        /// </summary>
        public bool IsDaily
        {
            get { return _bIsDaily; }
            set { if (this._bIsDaily != value) { _bIsDaily = value; } }
        }

        private bool _bIsWeekly;
        /// <summary>
        /// Is Weekly
        /// </summary>
        public bool IsWeekly
        {
            get { return _bIsWeekly; }
            set { if (this._bIsWeekly != value) { _bIsWeekly = value; } }
        }

        private bool _bIsMonthly;
        /// <summary>
        /// Is Monthly
        /// </summary>
        public bool IsMonthly
        {
            get { return _bIsMonthly; }
            set { if (this._bIsMonthly != value) { _bIsMonthly = value; } }
        }

        private bool _bIsWeekDaysOnly;
        /// <summary>
        /// Is WeekDays Only
        /// </summary>
        public bool IsWeekDaysOnly
        {
            get { return _bIsWeekDaysOnly; }
            set { if (this._bIsWeekDaysOnly != value) { _bIsWeekDaysOnly = value; } }
        }

        private DateTime _dateTaskStart;
        /// <summary>
        /// Task StartDate
        /// </summary>
        public DateTime TaskStartDate
        {
            get { return _dateTaskStart; }
            set { if (this._dateTaskStart != value) { _dateTaskStart = value; } }
        }

        private DateTime _dateTaskEnd;
        /// <summary>
        /// Task EndDate
        /// </summary>
        public DateTime TaskEndDate
        {
            get { return _dateTaskEnd; }
            set { if (this._dateTaskEnd != value) { _dateTaskEnd = value; } }
        }

        private DateTime _RunEndTime;
        /// <summary>
        /// Last RunEndTime 
        /// </summary>
        public DateTime RunEndTime
        {
            get { return _RunEndTime; }
            set { if (this._RunEndTime != value) { _RunEndTime = value; } }
        }

        private bool _bIsEnabled = true;
        /// <summary>
        /// Is Enabled
        /// </summary>
        public bool IsEnabled
        {
            get { return _bIsEnabled; }
            set { if (this._bIsEnabled != value) { _bIsEnabled = value; } }
        }

        private int _iTaskCount;
        /// <summary>
        /// Task Count
        /// </summary>
        public int TaskCount
        {
            get { return _iTaskCount; }
            set { if (this._iTaskCount != value) { _iTaskCount = value; } }
        }

        public bool Validate(bool pIsUpdate)
        {
            if (pIsUpdate)
            {
                if (String.IsNullOrEmpty(ID))
                    return false;
            }
            else
            {
                if (String.IsNullOrEmpty(this.ClientId))
                    return false;
            }
            if (String.IsNullOrEmpty(LastModifiedById))
                return false;
            return true;
        }
        private DateTime _dNextRunDate;
        public DateTime NextRunDate
        {
            get { return _dNextRunDate; }
            set { if (this._dNextRunDate != value) { _dNextRunDate = value; } }
        }

        private string _masterClientId;
        /// <summary>
        /// To Get Entity object MasterClientId
        /// </summary>
        public string MasterClientId
        {
            get { return _masterClientId; }
            set { if (this._masterClientId != value) { _masterClientId = value; } }
        }

        private string _reportDeliveryDashboardId;
        /// <summary>
        /// To Get Entity object ReportDeliveryDashboardId
        /// </summary>
        public string ReportDeliveryDashboardId
        {
            get { return _reportDeliveryDashboardId; }
            set { if (this._reportDeliveryDashboardId != value) { _reportDeliveryDashboardId = value; } }
        }
    }
}