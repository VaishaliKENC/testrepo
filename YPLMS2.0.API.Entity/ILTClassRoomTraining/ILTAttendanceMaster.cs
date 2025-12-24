/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:Atul
* Created:<07/26/23>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ILTAttendanceMaster : BaseEntity 
    /// </summary>
    /// 
    public class ILTAttendanceMaster : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ILTAttendanceMaster()
        { }

        private string _strUserSessionAttendanceId;
        /// <summary>
        /// UserSessionAttendanceId
        /// </summary>
        public string UserSessionAttendanceId
        {
            get { return _strUserSessionAttendanceId; }
            set { if (this._strUserSessionAttendanceId != value) { _strUserSessionAttendanceId = value; } }
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

        private string _strEventName;
        /// <summary>
        /// EventName
        /// </summary>
        public string EventName
        {
            get { return _strEventName; }
            set { if (this._strEventName != value) { _strEventName = value; } }
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

        private int _strSessionCount;
        /// <summary>
        /// SessionCount
        /// </summary>
        public int SessionCount
        {
            get { return _strSessionCount; }
            set { if (this._strSessionCount != value) { _strSessionCount = value; } }
        }

        private string _strSessionName;
        /// <summary>
        /// SessionName
        /// </summary>
        public string SessionName
        {
            get { return _strSessionName; }
            set { if (this._strSessionName != value) { _strSessionName = value; } }
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

        private string _strModuleName;
        /// <summary>
        /// ModuleName
        /// </summary>
        public string ModuleName
        {
            get { return _strModuleName; }
            set { if (this._strModuleName != value) { _strModuleName = value; } }
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

        private string _strIsAttended;
        /// <summary>
        /// IsAttended
        /// </summary>
        public string IsAttended
        {
            get { return _strIsAttended; }
            set { if (this._strIsAttended != value) { _strIsAttended = value; } }
        }

        private Nullable<bool> _strMarkAttendance;
        /// <summary>
        /// MarkAttendance
        /// </summary>
        public Nullable<bool> MarkAttendance
        {
            get { return _strMarkAttendance; }
            set { if (this._strMarkAttendance != value) { _strMarkAttendance = value; } }
        }

        private string _strAttendanceDate;
        /// <summary>
        /// AttendanceDate
        /// </summary>
        public string AttendanceDate
        {
            get { return _strAttendanceDate; }
            set { if (this._strAttendanceDate != value) { _strAttendanceDate = value; } }
        }

        private string _strAttendanceTime;
        /// <summary>
        /// AttendanceTime
        /// </summary>
        public string AttendanceTime
        {
            get { return _strAttendanceTime; }
            set { if (this._strAttendanceTime != value) { _strAttendanceTime = value; } }
        }

        private string _strMessage;
        /// <summary>
        /// AttendanceTime
        /// </summary>
        public string Message
        {
            get { return _strMessage; }
            set { if (this._strMessage != value) { _strMessage = value; } }
        }

        private string _strInstructorName;
        /// <summary>
        /// InstructorName
        /// </summary>
        public string InstructorName
        {
            get { return _strInstructorName; }
            set { if (this._strInstructorName != value) { _strInstructorName = value; } }
        }

        private Nullable<int> _strDay;
        /// <summary>
        /// Day
        /// </summary>
        public Nullable<int> Day
        {
            get { return _strDay; }
            set { if (this._strDay != value) { _strDay = value; } }
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

        private bool _bIsActive;
        /// <summary>
        /// To check Is Active
        /// </summary>
        public bool IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
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

        private bool? _strILTRole;
        /// <summary>
        /// ILTRole
        /// </summary>
        public bool? ILTRole
        {
            get { return _strILTRole; }
            set { if (this._strILTRole != value) { _strILTRole = value; } }
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add_MarkAll_Event,
            Add_MarkAll_Session,
            Add_Individual_Session,
            Add_Individual_Module,
            Update,
            Delete
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll_EventDetails,
            GetAll_SessionDetails,
            GetAll_Session_List,
            GetAll_ModuleDetails,
            GetAll_Session_MarkAttendance_UserList,
            GetAll_Module_MarkAttendance_UserList
        }
    }
}