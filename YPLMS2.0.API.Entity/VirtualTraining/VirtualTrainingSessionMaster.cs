using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class VirtualTrainingSessionMaster : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public VirtualTrainingSessionMaster()
        { }

        private string _strVirtualTrainingId;
        /// <summary>
        /// TrainingId
        /// </summary>
        public string TrainingId
        {
            get { return _strVirtualTrainingId; }
            set { if (this._strVirtualTrainingId != value) { _strVirtualTrainingId = value; } }
        }

        private string _strVirtualTrainingType;
        /// <summary>
        /// TrainingType
        /// </summary>
        public string TrainingType
        {
            get { return _strVirtualTrainingType; }
            set { if (this._strVirtualTrainingType != value) { _strVirtualTrainingType = value; } }
        }

        private string _strVirtualTrainingTitle;
        /// <summary>
        /// TrainingTitle
        /// </summary>
        public string Title
        {
            get { return _strVirtualTrainingTitle; }
            set { if (this._strVirtualTrainingTitle != value) { _strVirtualTrainingTitle = value; } }
        }

        private string _strVirtualTrainingSessionPassword;
        /// <summary>
        /// SessionPassword
        /// </summary>
        public string SessionPassword
        {
            get { return _strVirtualTrainingSessionPassword; }
            set { if (this._strVirtualTrainingSessionPassword != value) { _strVirtualTrainingSessionPassword = value; } }
        }


        private string _strVirtualTrainingAgenda;
        /// <summary>
        /// Agenda
        /// </summary>
        public string Agenda
        {
            get { return _strVirtualTrainingAgenda; }
            set { if (this._strVirtualTrainingAgenda != value) { _strVirtualTrainingAgenda = value; } }
        }

        private string _strVirtualTrainingDescription;
        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get { return _strVirtualTrainingDescription; }
            set { if (this._strVirtualTrainingDescription != value) { _strVirtualTrainingDescription = value; } }
        }

        private DateTime _strVirtualTrainingStartDate;
        /// <summary>
        /// StartDate
        /// </summary>
        public DateTime StartDate
        {
            get { return _strVirtualTrainingStartDate; }
            set { if (this._strVirtualTrainingStartDate != value) { _strVirtualTrainingStartDate = value; } }
        }

        private DateTime _strVirtualTrainingEndDate;
        /// <summary>
        /// EndDate
        /// </summary>
        public DateTime EndDate
        {
            get { return _strVirtualTrainingEndDate; }
            set { if (this._strVirtualTrainingEndDate != value) { _strVirtualTrainingEndDate = value; } }
        }

        private string _strVirtualTrainingDuration;
        /// <summary>
        /// Duration
        /// </summary>
        public string Duration
        {
            get { return _strVirtualTrainingDuration; }
            set { if (this._strVirtualTrainingDuration != value) { _strVirtualTrainingDuration = value; } }
        }

        private string _strVirtualTrainingTimeZone;
        /// <summary>
        /// TimeZone
        /// </summary>
        public string TimeZone
        {
            get { return _strVirtualTrainingTimeZone; }
            set { if (this._strVirtualTrainingTimeZone != value) { _strVirtualTrainingTimeZone = value; } }
        }

        private int _strVirtualTrainingTimeZoneId;
        /// <summary>
        /// TimeZoneId
        /// </summary>
        public int TimeZoneId
        {
            get { return _strVirtualTrainingTimeZoneId; }
            set { if (this._strVirtualTrainingTimeZoneId != value) { _strVirtualTrainingTimeZoneId = value; } }
        }

        private string _strVirtualTrainingOccurence;
        /// <summary>
        /// Occurence
        /// </summary>
        public string Occurence
        {
            get { return _strVirtualTrainingOccurence; }
            set { if (this._strVirtualTrainingOccurence != value) { _strVirtualTrainingOccurence = value; } }
        }


        private bool _strVirtualTrainingIsCancelled;
        /// <summary>
        /// IsCancelled
        /// </summary>
        public bool IsCancelled
        {
            get { return _strVirtualTrainingIsCancelled; }
            set { if (this._strVirtualTrainingIsCancelled != value) { _strVirtualTrainingIsCancelled = value; } }
        }


        private bool _strVirtualTrainingIsActive;
        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive
        {
            get { return _strVirtualTrainingIsActive; }
            set { if (this._strVirtualTrainingIsActive != value) { _strVirtualTrainingIsActive = value; } }
        }

        private string _strVirtualTrainingSessionKey;
        /// <summary>
        /// SessionKey
        /// </summary>
        public string SessionKey
        {
            get { return _strVirtualTrainingSessionKey; }
            set { if (this._strVirtualTrainingSessionKey != value) { _strVirtualTrainingSessionKey = value; } }
        }


        private bool _strVirtualTrainingIsSelfRegistration;
        /// <summary>
        /// IsSelfRegistration
        /// </summary>
        public bool IsSelfRegistration
        {
            get { return _strVirtualTrainingIsSelfRegistration; }
            set { if (this._strVirtualTrainingIsSelfRegistration != value) { _strVirtualTrainingIsSelfRegistration = value; } }
        }

        //Added by Shailesh
        private int _strVirtualTrainingMaxRegistration;
        public int MaxRegistration
        {
            get { return _strVirtualTrainingMaxRegistration; }
            set { if (this._strVirtualTrainingMaxRegistration != value) { _strVirtualTrainingMaxRegistration = value; } }
        }

        private int _strVirtualTrainingMinRegistraion;

        public int MinRegistraion
        {
            get { return _strVirtualTrainingMinRegistraion; }
            set { if (this._strVirtualTrainingMinRegistraion != value) { _strVirtualTrainingMinRegistraion = value; } }
        }

        private bool _strVirtualTrainingISWaitlisted;

        public bool ISWaitlisted
        {
            get { return _strVirtualTrainingISWaitlisted; }
            set { if (this._strVirtualTrainingISWaitlisted != value) { _strVirtualTrainingISWaitlisted = value; } }
        }

        private string _strVirtualTrainingMaxWaitlisted;

        public string MaxWaitlisted
        {
            get { return _strVirtualTrainingMaxWaitlisted; }
            set { if (this._strVirtualTrainingMaxWaitlisted != value) { _strVirtualTrainingMaxWaitlisted = value; } }
        }

        private bool _strVirtualTrainingAutoapprove;

        public bool Autoapprove
        {
            get { return _strVirtualTrainingAutoapprove; }
            set { if (this._strVirtualTrainingAutoapprove != value) { _strVirtualTrainingAutoapprove = value; } }
        }

        private string _strVirtualTrainingEmailID;
        private string _strSystemUserGUID;

        /// <summary>

        /// SessionKey
        /// </summary>
        public string EmailID
        {
            get { return _strVirtualTrainingEmailID; }
            set { if (this._strVirtualTrainingEmailID != value) { _strVirtualTrainingEmailID = value; } }
        }

        private int iNoOfAttended;
        /// <summary>
        /// SessionKey
        /// </summary>
        public int NoOfAttended
        {
            get { return iNoOfAttended; }
            set { if (this.iNoOfAttended != value) { iNoOfAttended = value; } }
        }

        private bool _bReportImported;

        public bool ReportImported
        {
            get { return _bReportImported; }
            set { if (this._bReportImported != value) { _bReportImported = value; } }
        }

        /// <summary>
        /// SystemUserGUID
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }


        private string _strStatus;
        /// <summary>
        /// SystemUserGUID
        /// </summary>
        public string Status
        {
            get { return _strStatus; }
            set { if (this._strStatus != value) { _strStatus = value; } }
        }

        private string _strAttendeeId;
        /// <summary>
        /// Attendee_Id
        /// </summary>
        public string AttendeeId
        {
            get { return _strAttendeeId; }
            set { if (this._strAttendeeId != value) { _strAttendeeId = value; } }
        }

        private int _totalCount;
        /// <summary>
        /// totalRows
        /// </summary>
        public int TotalCount
        {
            get { return _totalCount; }
            set { if (this._totalCount != value) { _totalCount = value; } }
        }

        private int _strNoOfRegistered;
        /// <summary>
        /// NoOfRegistered
        /// </summary>
        public int NoOfRegistered 
        {
            get { return _strNoOfRegistered; }
            set { if (this._strNoOfRegistered != value) { _strNoOfRegistered = value; } }
        }

        private int _strNoOfWaitList;
        /// <summary>
        /// NoOfWaitList
        /// </summary>
        public int NoOfWaitList
        {
            get { return _strNoOfWaitList; }
            set { if (this._strNoOfWaitList != value) { _strNoOfWaitList = value; } }
        }

        private string _strTrainingStatus;

   
        /// <summary>

        ///Training Status
        /// </summary>
        public string TrainingStatus
        {
            get { return _strTrainingStatus; }
            set { if (this._strTrainingStatus != value) { _strTrainingStatus = value; } }
        }

        private DateTime _strTrainingStartTime;
        /// <summary>
        /// Training Start Time
        /// </summary>
        public DateTime TrainingStartTime
        {
            get { return _strTrainingStartTime; }
            set { if (this._strTrainingStartTime != value) { _strTrainingStartTime = value; } }
        }

        private DateTime _strTrainingEndTime;
        /// <summary>
        /// Training End Time
        /// </summary>
        public DateTime TrainingEndTime
        {
            get { return _strTrainingEndTime; }
            set { if (this._strTrainingEndTime != value) { _strTrainingEndTime = value; } }
        }


        private int _strTrainingDuration;
        /// <summary>
        /// Training Duration
        /// </summary>
        public int TrainingDuration
        {
            get { return _strTrainingDuration; }
            set { if (this._strTrainingDuration != value) { _strTrainingDuration = value; } }
        }
        /// <summary>

        private string _strVirtualWebexSystemUserid;
        /// _strVirtualSystemUserid
        /// </summary>
        public string VirtualWebexSystemUserid
        {
            get { return _strVirtualWebexSystemUserid; }
            set { if (this._strVirtualWebexSystemUserid != value) { _strVirtualWebexSystemUserid = value; } }
        }

        private bool _strIsAdminAdded;
        /// <summary>
        /// IsAdminAdded
        /// </summary>
        public bool IsAdminAdded
        {
            get { return _strIsAdminAdded; }
            set { if (this._strIsAdminAdded != value) { _strIsAdminAdded = value; } }
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
            UpdateSessionAttendeeList,
            UpdateCountAttended,            
            UpdateRegister,
            UpdateTrainingTotalTime,
            GetAdminAccountUserID,
            UpdateNoOfRegistered

        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllSession,
            ActivateDeActivateStatus,
            GetAllSessionKey,
            GetSystemUserGUID,
            GetAllSelfTrainingStatus,
            GetAllVIRTUALTRAININGClient,
            Search_VIRTUALTRAINING_Names,
            GetAllForCurriculum,
        }
    }
}
