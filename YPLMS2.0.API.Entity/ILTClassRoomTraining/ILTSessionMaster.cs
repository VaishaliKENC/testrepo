/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:Atul/Chetan Dabire
* Created:12 Dec 2023
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ILTSessionMaster : BaseEntity 
    /// </summary>
    /// 
    public class ILTSessionMaster : ILTSessionLanguageMaster
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ILTSessionMaster()
        { }

        private string _strSessionId;
        /// <summary>
        /// SessionId
        /// </summary>
        public string SessionId
        {
            get { return _strSessionId; }
            set { if (this._strSessionId != value) { _strSessionId = value; } }
        }

        private string _strModuleId;
        /// <summary>
        /// SessionId
        /// </summary>
        public string ModuleId
        {
            get { return _strModuleId; }
            set { if (this._strModuleId != value) { _strModuleId = value; } }
        }

        private string _systemUserGUID;
        /// <summary>
        /// systemUserGUID
        /// </summary>
        public string SystemUserGUID
        {
            get { return _systemUserGUID; }
            set { if (this._systemUserGUID != value) { _systemUserGUID = value; } }
        }

        private string _strSessionNo;
        /// <summary>
        /// SessionNo
        /// </summary>
        public string SessionNo
        {
            get { return _strSessionNo; }
            set { if (this._strSessionNo != value) { _strSessionNo = value; } }
        }

        private string _strNoOfModules;
        /// <summary>
        /// NoOfModules
        /// </summary>
        public string NoOfModules
        {
            get { return _strNoOfModules; }
            set { if (this._strNoOfModules != value) { _strNoOfModules = value; } }
        }

        private string _strEventName;
        /// <summary>
        /// EventName
        /// </summary>
        public string EventName
        {
            get { return _strEventName; }
            set { if (this._strEventName != value) { _strEventName = value; } }
        }

        private string _strSearchName;
        /// <summary>
        /// EventName
        /// </summary>
        public string SearchName
        {
            get { return _strSearchName; }
            set { if (this._strSearchName != value) { _strSearchName = value; } }
        }

        private string _strModuleName;
        /// <summary>
        /// EventName
        /// </summary>
        public string ModuleName
        {
            get { return _strModuleName; }
            set { if (this._strModuleName != value) { _strModuleName = value; } }
        }

        private string _strDocumentName;
        /// <summary>
        /// DocumentName
        /// </summary>
        public string DocumentName
        {
            get { return _strDocumentName; }
            set { if (this._strDocumentName != value) { _strDocumentName = value; } }
        }

        private string _strFileName;
        /// <summary>
        /// DocumentName
        /// </summary>
        public string FileName
        {
            get { return _strFileName; }
            set { if (this._strFileName != value) { _strFileName = value; } }
        }

        private string _strRefMaterialId;
        /// <summary>
        /// DocumentName
        /// </summary>
        public string RefMaterialId
        {
            get { return _strRefMaterialId; }
            set { if (this._strRefMaterialId != value) { _strRefMaterialId = value; } }
        }


        private string _strEmailId;
        /// <summary>
        /// DocumentName
        /// </summary>
        public string EmailID
        {
            get { return _strEmailId; }
            set { if (this._strEmailId != value) { _strEmailId = value; } }
        }

        private string _strInstructorEmail;
        /// <summary>
        /// iNSTRUCTOReMAIL
        /// </summary>
        public string InstructorEmail
        {
            get { return _strInstructorEmail; }
            set { if (this._strInstructorEmail != value) { _strInstructorEmail = value; } }
        }

        private Nullable<bool> _strMarkAttendance;
        /// <summary>
        /// AdminApproval
        /// </summary>
        public Nullable<bool> MarkAttendance
        {
            get { return _strMarkAttendance; }
            set { if (this._strMarkAttendance != value) { _strMarkAttendance = value; } }
        }

        private string _strILTMarkAttendance;
        /// <summary>
        /// AdminApproval
        /// </summary>
        public string ILTMarkAttendance
        {
            get { return _strILTMarkAttendance; }
            set { if (this._strILTMarkAttendance != value) { _strILTMarkAttendance = value; } }
        }

        private string _strILTUserName;
        /// <summary>
        /// AdminApproval
        /// </summary>
        public string UserName
        {
            get { return _strILTUserName; }
            set { if (this._strILTUserName != value) { _strILTUserName = value; } }
        }

        private string _strILTUSERID;
        /// <summary>
        /// AdminApproval
        /// </summary>
        public string UserId
        {
            get { return _strILTUSERID; }
            set { if (this._strILTUSERID != value) { _strILTUSERID = value; } }
        }

        private Nullable<int> _strModuleDay;
        /// <summary>
        /// TotalSessionDuration
        /// </summary>
        public Nullable<int> Day
        {
            get { return _strModuleDay; }
            set { if (this._strModuleDay != value) { _strModuleDay = value; } }
        }

        private string _strDay;
        /// <summary>
        /// TotalSessionDuration
        /// </summary>
        public string StringDays
        {
            get { return _strDay; }
            set { if (this._strDay != value) { _strDay = value; } }
        }

        private string _strDescription;
        /// <summary>
        /// EventName
        /// </summary>
        public string Description  
        {
            get { return _strDescription; }
            set { if (this._strDescription != value) { _strDescription = value; } }
        }

        private string _strObjective;
        /// <summary>
        /// EventName
        /// </summary>
        public string Objective
        {
            get { return _strObjective; }
            set { if (this._strObjective != value) { _strObjective = value; } }
        }

        private string _strEventId;
        /// <summary>
        /// EventId
        /// </summary>
        public string EventId
        {
            get { return _strEventId; }
            set { if (this._strEventId != value) { _strEventId = value; } }
        }        

        private string _strTimeZone;
        /// <summary>
        /// PreferredTimeZone
        /// </summary>
        public string TimeZone
        {
            get { return _strTimeZone; }
            set { if (this._strTimeZone != value) { _strTimeZone = value; } }
        }

        private Nullable<DateTime> _strStartDate;
        /// <summary>
        /// StartDate
        /// </summary>
        public Nullable<DateTime> StartDate
        {
            get { return _strStartDate; }
            set { if (this._strStartDate != value) { _strStartDate = value; } }
        }

        private Nullable<DateTime> _strStartDateFrom;
        /// <summary>
        /// StartDate
        /// </summary>
        public Nullable<DateTime> StartDateFrom
        {
            get { return _strStartDateFrom; }
            set { if (this._strStartDateFrom != value) { _strStartDateFrom = value; } }
        }

        private Nullable<DateTime> _strStartDateTo;
        /// <summary>
        /// StartDate
        /// </summary>
        public Nullable<DateTime> StartDateTo
        {
            get { return _strStartDateTo; }
            set { if (this._strStartDateTo != value) { _strStartDateTo = value; } }
        }


        private Nullable<DateTime> _strEndDate;
        /// <summary>
        /// EndDate
        /// </summary>
        public Nullable<DateTime> EndDate
        {
            get { return _strEndDate; }
            set { if (this._strEndDate != value) { _strEndDate = value; } }
        }


        private Nullable<DateTime> _strEndDateFrom;
        /// <summary>
        /// EndDate
        /// </summary>
        public Nullable<DateTime> EndDateFrom
        {
            get { return _strEndDateFrom; }
            set { if (this._strEndDateFrom != value) { _strEndDateFrom = value; } }
        }

        private Nullable<DateTime> _strEndDateTo;
        /// <summary>
        /// EndDate
        /// </summary>
        public Nullable<DateTime> EndDateTo
        {
            get { return _strEndDateTo; }
            set { if (this._strEndDateTo != value) { _strEndDateTo = value; } }
        }


        private DateTime _strCompletionDate;
        /// <summary>
        /// EndDate
        /// </summary>
        public DateTime CompletionDate
        {
            get { return _strCompletionDate; }
            set { if (this._strCompletionDate != value) { _strCompletionDate = value; } }
        }

        private Nullable<int> _strTotalSessionDuration;
        /// <summary>
        /// TotalSessionDuration
        /// </summary>
        public Nullable<int> TotalSessionDuration
        {
            get { return _strTotalSessionDuration; }
            set { if (this._strTotalSessionDuration != value) { _strTotalSessionDuration = value; } }
        }

        private Nullable<bool> _strRegistrationByAdmin;
        /// <summary>
        /// RegistrationByAdmin
        /// </summary>
        public Nullable<bool> RegistrationByAdmin
        {
            get { return _strRegistrationByAdmin; }
            set { if (this._strRegistrationByAdmin != value) { _strRegistrationByAdmin = value; } }
        }

        private Nullable<bool> _strAdminApproval;
        /// <summary>
        /// AdminApproval
        /// </summary>
        public Nullable<bool> AdminApproval
        {
            get { return _strAdminApproval; }
            set { if (this._strAdminApproval != value) { _strAdminApproval = value; } }
        }

        private string _strSessionStatus;
        /// <summary>
        /// SessionStatus
        /// </summary>
        public string SessionStatus
        {
            get { return _strSessionStatus; }
            set { if (this._strSessionStatus != value) { _strSessionStatus = value; } }
        }

        private string _strStatus;
        /// <summary>
        /// SessionStatus
        /// </summary>
        public string Status
        {
            get { return _strStatus; }
            set { if (this._strStatus != value) { _strStatus = value; } }
        }

        private string _strLocationId;
        /// <summary>
        /// SessionLocationId
        /// </summary>
        public string LocationId
        {
            get { return _strLocationId; }
            set { if (this._strLocationId != value) { _strLocationId = value; } }
        }

        private string _strLocationName;
        /// <summary>
        /// SessionLocationName
        /// </summary>
        public string LocationName
        {
            get { return _strLocationName; }
            set { if (this._strLocationName != value) { _strLocationName = value; } }
        }

        private string _strLocationVenue;
        /// <summary>
        /// SessionLocationName
        /// </summary>
        public string LocationVenue
        {
            get { return _strLocationVenue; }
            set { if (this._strLocationVenue != value) { _strLocationVenue = value; } }
        }

        private string _strPrimaryInstructorId;
        /// <summary>
        /// PrimaryInstructorId
        /// </summary>
        public string PrimaryInstructorId
        {
            get { return _strPrimaryInstructorId; }
            set { if (this._strPrimaryInstructorId != value) { _strPrimaryInstructorId = value; } }
        }

        private string _strPrimaryInstructorName;
        /// <summary>
        /// PrimaryInstructorName
        /// </summary>
        public string PrimaryInstructorName
        {
            get { return _strPrimaryInstructorName; }
            set { if (this._strPrimaryInstructorName != value) { _strPrimaryInstructorName = value; } }
        }

        private Nullable<bool> _IsActive;
        /// <summary>
        /// IsActive
        /// </summary>
        public Nullable<bool> IsActive
        {
            get { return _IsActive; }
            set { if (this._IsActive != value) { _IsActive = value; } }
        }

        private Nullable<bool> _strIsSessionCancel;
        /// <summary>
        /// IsSessionCancel
        /// </summary>
        public Nullable<bool> IsSessionCancel
        {
            get { return _strIsSessionCancel; }
            set { if (this._strIsSessionCancel != value) { _strIsSessionCancel = value; } }
        }

        private string _strMaxSessionNo;
        /// <summary>
        /// MaxSessionNo
        /// </summary>
        public string MaxSessionNo
        {
            get { return _strMaxSessionNo; }
            set { if (this._strMaxSessionNo != value) { _strMaxSessionNo = value; } }
        }

        private string _strPrimaryInstructorEmail;
        /// <summary>
        /// PrimaryInstructorEmail
        /// </summary>
        public string PrimaryInstructorEmail
        {
            get { return _strPrimaryInstructorEmail; }
            set { if (this._strPrimaryInstructorEmail != value) { _strPrimaryInstructorEmail = value; } }
        }

        private string _strPrimaryInstructorLoginID;
        /// <summary>
        /// PrimaryInstructorLoginID
        /// </summary>
        public string PrimaryInstructorLoginID
        {
            get { return _strPrimaryInstructorLoginID; }
            set { if (this._strPrimaryInstructorLoginID != value) { _strPrimaryInstructorLoginID = value; } }
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            AddUpdateImport,
            Update,
            Delete,
            UpdateLanguage,
            CopySessionDetails,
            GetByName,
            CancelSession,
            UserCancleSession,
            UserIntrestSession,
            GetSessionDetails,
            GetMaxSessionNo,
            Get_SessionID,
            Get_Email
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAll_Session_Event,
            GetRules,
            GetUsersByRuleId,
            GetUser,
            GetAllAvailableSession,
            GetAllRegistredSession,
            GetAllCompletedSession,
            GetModuleDetails,
            GetReferenceMaterial,
            GetModelReferenceMaterial,
            GetModuleDetailsReport,
            GetTrainingAttendanceReport,
            GetILTSessionDetailsReport,
            GetUserWiseILTReport,
            GetAllInstructor,
            GetAll_Session_BulkImport
        }
    }
}