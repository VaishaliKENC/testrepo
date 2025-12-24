/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:Abhay
* Created:<12/26/2011>
* Last Modified:
*/

using System;
using System.Collections.Generic;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class UserSessionRegistration : BaseEntity 
    /// </summary>
    /// 
    public class UserSessionRegistration : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public UserSessionRegistration()
        { }

        private string _strLanguageId;
        /// <summary>
        /// LanguageId
        /// </summary>
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }

        private string _strSessionId;
        /// <summary>
        /// SessionId
        /// </summary>
        public string SessionId
        {
            get { return _strSessionId; }
            set { if (this._strSessionId != value) { _strSessionId = value; } }
        }

        private string _strProgramId;
        /// <summary>
        /// SessionId
        /// </summary>
        public string ProgramId
        {
            get { return _strProgramId; }
            set { if (this._strProgramId != value) { _strProgramId = value; } }
        }

        private string _strSessionName;
        /// <summary>
        /// SessionId
        /// </summary>
        public string SessionName
        {
            get { return _strSessionName; }
            set { if (this._strSessionName != value) { _strSessionName = value; } }
        }

        private string _strProgramName;
        /// <summary>
        /// SessionId
        /// </summary>
        public string ProgramName
        {
            get { return _strProgramName; }
            set { if (this._strProgramName != value) { _strProgramName = value; } }
        }

        private int _strMaxRegistration;
        /// <summary>
        /// SessionId
        /// </summary>
        public int MaxRegistration
        {
            get { return _strMaxRegistration; }
            set { if (this._strMaxRegistration != value) { _strMaxRegistration = value; } }
        }

        private int _strMaxWaitlisting;
        /// <summary>
        /// MaxWaitlisting
        /// </summary>
        public int MaxWaitlisting
        {
            get { return _strMaxWaitlisting; }
            set { if (this._strMaxWaitlisting != value) { _strMaxWaitlisting = value; } }
        }

        private int _strNominationCount;
        /// <summary>
        /// SessionId
        /// </summary>
        public int NominationCount
        {
            get { return _strNominationCount; }
            set { if (this._strNominationCount != value) { _strNominationCount = value; } }
        }

        private int _strWaitlistCount;
        /// <summary>
        /// SessionId
        /// </summary>
        public int WaitlistCount
        {
            get { return _strWaitlistCount; }
            set { if (this._strWaitlistCount != value) { _strWaitlistCount = value; } }
        }

        private string _strStatus;
        /// <summary>
        /// Status
        /// </summary>
        public string Status
        {
            get { return _strStatus; }
            set { if (this._strStatus != value) { _strStatus = value; } }
        }

        private string _strFirstName;
        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName
        {
            get { return _strFirstName; }
            set { if (this._strFirstName != value) { _strFirstName = value; } }
        }

        private string _strLastName;
        /// <summary>
        /// FirstName
        /// </summary>
        public string LastName
        {
            get { return _strLastName; }
            set { if (this._strLastName != value) { _strLastName = value; } }
        }

        private string _strUserName;
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName
        {
            get { return _strUserName; }
            set { if (this._strUserName != value) { _strUserName = value; } }
        }

        private string _strEmailId;
        /// <summary>
        /// EmailId
        /// </summary>
        public string EmailId
        {
            get { return _strEmailId; }
            set { if (this._strEmailId != value) { _strEmailId = value; } }
        }

        private string _strManagerId;
        /// <summary>
        /// ManagerId
        /// </summary>
        public string ManagerId
        {
            get { return _strManagerId; }
            set { if (this._strManagerId != value) { _strManagerId = value; } }
        }

        private DateTime _strNominatedOnDate;
        /// <summary>
        /// NominatedOnDate
        /// </summary>
        public DateTime NominatedOnDate
        {
            get { return _strNominatedOnDate; }
            set { if (this._strNominatedOnDate != value) { _strNominatedOnDate = value; } }
        }

        private DateTime _strManagerApprovalDate;
        /// <summary>
        /// ManagerApprovalDate
        /// </summary>
        public DateTime ManagerApprovalDate
        {
            get { return _strManagerApprovalDate; }
            set { if (this._strManagerApprovalDate != value) { _strManagerApprovalDate = value; } }
        }

        private string _strManagerApprovalStatus;
        /// <summary>
        /// ManagerApprovalStatus
        /// </summary>
        public string ManagerApprovalStatus
        {
            get { return _strManagerApprovalStatus; }
            set { if (this._strManagerApprovalStatus != value) { _strManagerApprovalStatus = value; } }
        }

        private DateTime _strRejectionDate;
        /// <summary>
        /// RejectionDate
        /// </summary>
        public DateTime RejectionDate
        {
            get { return _strRejectionDate; }
            set { if (this._strRejectionDate != value) { _strRejectionDate = value; } }
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

        private DateTime _strSessionToDate;
        /// <summary>
        /// RejectionDate
        /// </summary>
        public DateTime SessionToDate
        {
            get { return _strSessionToDate; }
            set { if (this._strSessionToDate != value) { _strSessionToDate = value; } }
        }

        private DateTime _strSessionFromDate;
        /// <summary>
        /// RejectionDate
        /// </summary>
        public DateTime SessionFromDate
        {
            get { return _strSessionFromDate; }
            set { if (this._strSessionFromDate != value) { _strSessionFromDate = value; } }
        }

        private bool _strWaitlistStatus;
        /// <summary>
        /// ManagerApprovalStatus
        /// </summary>
        public bool WaitlistStatus
        {
            get { return _strWaitlistStatus; }
            set { if (this._strWaitlistStatus != value) { _strWaitlistStatus = value; } }
        }

        private string _strSessionFromTime;
        /// <summary>
        /// SpeakerName
        /// </summary>
        public string SessionFromTime
        {
            get { return _strSessionFromTime; }
            set { if (this._strSessionFromTime != value) { _strSessionFromTime = value; } }
        }

        private string _strSessionToTime;
        /// <summary>
        /// SpeakerName
        /// </summary>
        public string SessionToTime
        {
            get { return _strSessionToTime; }
            set { if (this._strSessionToTime != value) { _strSessionToTime = value; } }
        }

        private bool _strSelfNomination;
        /// <summary>
        /// SpeakerName
        /// </summary>
        public bool SelfNomination
        {
            get { return _strSelfNomination; }
            set { if (this._strSelfNomination != value) { _strSelfNomination = value; } }
        }

        private string _strSystemUserGUID;
        /// <summary>
        /// SessionId
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private List<SessionMaster> _entSM;
        /// <summary>
        /// SessionAllocatedSpeakers
        /// </summary>
        public List<SessionMaster> entSessionMaster
        {
            get { return _entSM; }
            set { _entSM = value; }
        }

        private string _strNominatedById;
        /// <summary>
        /// SessionId
        /// </summary>
        public string NominatedById
        {
            get { return _strNominatedById; }
            set { if (this._strNominatedById != value) { _strNominatedById = value; } }
        }


        private bool _strDirectApprove;
        /// <summary>
        /// SelfNomination
        /// </summary>
        public bool DirectApprove
        {
            get { return _strDirectApprove; }
            set { if (this._strDirectApprove != value) { _strDirectApprove = value; } }
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
            GetNomination,
            NominationStatusChange,
            UpdateCompleted

        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetSessionbyUser,
            GetAll_Completed,
        }
    }
}