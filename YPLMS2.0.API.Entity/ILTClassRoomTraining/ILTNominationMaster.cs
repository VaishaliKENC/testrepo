/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:Atul
* Created:<12/04/23>
* Last Modified:
*/

using System;
using System.Collections.Generic;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ILTNominationMaster : BaseEntity 
    /// </summary>
    /// 
    public class ILTNominationMaster : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ILTNominationMaster()
        { }

        private string _strSessionRegistrationId;
        /// <summary>
        /// SessionRegistrationId
        /// </summary>
        public string SessionRegistrationId
        {
            get { return _strSessionRegistrationId; }
            set { if (this._strSessionRegistrationId != value) { _strSessionRegistrationId = value; } }
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
        /// ModuleId
        /// </summary>
        public string ModuleId
        {
            get { return _strModuleId; }
            set { if (this._strModuleId != value) { _strModuleId = value; } }
        }
        
        private string _strKeyword;
        /// <summary>
        /// Keyword
        /// </summary>
        public string KeyWord
        {
            get { return _strKeyword; }
            set { if (this._strKeyword != value) { _strKeyword = value; } }
        }

        private string _strBussinessRuleId;
        /// <summary>
        /// BussinessRuleId
        /// </summary>
        public string BussinessRuleId
        {
            get { return _strBussinessRuleId; }
            set { if (this._strBussinessRuleId != value) { _strBussinessRuleId = value; } }
        }

        private string _strSystemUserGUID;
        /// <summary>
        /// SystemUserGUID
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private string _strMessage;
        /// <summary>
        /// Message
        /// </summary>
        public string Message
        {
            get { return _strMessage; }
            set { if (this._strMessage != value) { _strMessage = value; } }
        }

        private string _strFirstName;
        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName
        {
            get { return _strFirstName; }
            set { if (this._strFirstName != value) { _strFirstName = value; } }
        }

        private string _strMiddleName;
        /// <summary>
        /// Middle Name
        /// </summary>
        public string MiddleName
        {
            get { return _strMiddleName; }
            set { if (this._strMiddleName != value) { _strMiddleName = value; } }
        }

        private string _strLastName;
        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName
        {
            get { return _strLastName; }
            set { if (this._strLastName != value) { _strLastName = value; } }
        }

        private string _strEmailID;
        /// <summary>
        /// EmailID
        /// </summary>
        public string EmailID
        {
            get { return _strEmailID; }
            set { if (this._strEmailID != value) { _strEmailID = value; } }
        }

        private string _strNominatedById;
        /// <summary>
        /// Nominated By Id
        /// </summary>
        public string NominatedById
        {
            get { return _strNominatedById; }
            set { if (this._strNominatedById != value) { _strNominatedById = value; } }
        }

        private DateTime _dateNominatedDate;
        /// <summary>
        /// Nominated Date
        /// </summary>
        public DateTime NominatedDate
        {
            get { return _dateNominatedDate; }
            set { if (this._dateNominatedDate != value) { _dateNominatedDate = value; } }
        }

        private string _strNominatedTime;
        /// <summary>
        /// NominatedTime
        /// </summary>
        public string NominatedTime
        {
            get { return _strNominatedTime; }
            set { if (this._strNominatedTime != value) { _strNominatedTime = value; } }
        }

        private DateTime _dateIntrestedDate;
        /// <summary>
        /// Interested Date
        /// </summary>
        public DateTime InterestedDate
        {
            get { return _dateIntrestedDate; }
            set { if (this._dateIntrestedDate != value) { _dateIntrestedDate = value; } }
        }

        private string _strInterestedTime;
        /// <summary>
        /// InterestedTime
        /// </summary>
        public string InterestedTime
        {
            get { return _strInterestedTime; }
            set { if (this._strInterestedTime != value) { _strInterestedTime = value; } }
        }

        private DateTime _dateUserCancelDate;
        /// <summary>
        /// User Cancel Date
        /// </summary>
        public DateTime UserCancelDate
        {
            get { return _dateUserCancelDate; }
            set { if (this._dateUserCancelDate != value) { _dateUserCancelDate = value; } }
        }

        private bool _bIsActive;
        /// <summary>
        /// To check Is Active
        /// </summary>
        public bool IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
        }

        private bool _bIsSessionCancel;
        /// <summary>
        /// To check Is Session Cancel
        /// </summary>
        public bool IsSessionCancel
        {
            get { return _bIsSessionCancel; }
            set { if (this._bIsSessionCancel != value) { _bIsSessionCancel = value; } }
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

        private string _strUserNameAlias;
        /// <summary>
        /// User Name Alias
        /// </summary>
        public string UserNameAlias
        {
            get { return _strUserNameAlias; }
            set { if (this._strUserNameAlias != value) { _strUserNameAlias = value; } }
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll_Users_ForNomination,
            GetAll_RegisteredUsers_ForNomination,
            GetAll_InterestedUsers_ForNomination
        }
    }
}