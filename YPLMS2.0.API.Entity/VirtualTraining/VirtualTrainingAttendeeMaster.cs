using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class VirtualTrainingAttendeeMaster:Learner
    {

        public VirtualTrainingAttendeeMaster()
        { }


        private string _strSystemUserGUID;
        /// <summary>
        /// SystemUserGUID
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private string _strTrainingID;
        /// <summary>
        /// TrainingID
        /// </summary>
        public string TrainingID
        {
            get { return _strTrainingID; }
            set { if (this._strTrainingID != value) { _strTrainingID = value; } }
        }

        private string _strSessionKey;
        /// <summary>
        /// SessionKey
        /// </summary>
        public string SessionKey
        {
            get { return _strSessionKey; }
            set { if (this._strSessionKey != value) { _strSessionKey = value; } }
        }

        private bool _strIsDeleted;
        /// <summary>
        /// SessionKey
        /// </summary>
        public bool IsDeleted
        {
            get { return _strIsDeleted; }
            set { if (this._strIsDeleted != value) { _strIsDeleted = value; } }
        }

        private string _strUserFirstName;
        /// <summary>
        /// UserFirstName
        /// </summary>
        public string UserFirstName
        {
            get { return _strUserFirstName; }
            set { if (this._strUserFirstName != value) { _strUserFirstName = value; } }
        }

        private string _strUserLastName;
        /// <summary>
        /// UserLastName
        /// </summary>
        public string UserLastName
        {
            get { return _strUserLastName; }
            set { if (this._strUserLastName != value) { _strUserLastName = value; } }
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


        private string _strStatus;
        /// <summary>
        /// Status
        /// </summary>
        public string Status
        {
            get { return _strStatus; }
            set { if (this._strStatus != value) { _strStatus = value; } }
        }

        private string _strRegisterID;
        /// <summary>
        /// RegisterID
        /// </summary>
        public string RegisterID
        {
            get { return _strRegisterID; }
            set { if (this._strRegisterID != value) { _strRegisterID = value; } }
        }

        private string _strAttendeeID;
        /// <summary>
        /// AttendeeID
        /// </summary>
        public string AttendeeID
        {
            get { return _strAttendeeID; }
            set { if (this._strAttendeeID != value) { _strAttendeeID = value; } }
        }

        private bool _strIsAdminAdded;
        /// <summary>
        /// SessionKey
        /// </summary>
        public bool IsAdminAdded
        {
            get { return _strIsAdminAdded; }
            set { if (this._strIsAdminAdded != value) { _strIsAdminAdded = value; } }
        }


        //private string _strVirtualTrainingId;
        ///// <summary>
        ///// TrainingId
        ///// </summary>
        //public string TrainingId
        //{
        //    get { return _strVirtualTrainingId; }
        //    set { if (this._strVirtualTrainingId != value) { _strVirtualTrainingId = value; } }
        //}


        private string _strVTStatus;
        /// <summary>
        /// VTStatus
        /// </summary>
        public string VTStatus
        {
            get { return _strVTStatus; }
            set { if (this._strVTStatus != value) { _strVTStatus = value; } }
        }

        private string _strVTApprovePage;
        /// <summary>
        /// VTApprovePage
        /// </summary>
        public string VTApprovePage
        {
            get { return _strVTApprovePage; }
            set { if (this._strVTApprovePage != value) { _strVTApprovePage = value; } }
        }

        private DateTime _strStartTime;
        /// <summary>
        /// StartTime
        /// </summary>
        public DateTime StartTime
        {
            get { return _strStartTime; }
            set { if (this._strStartTime != value) { _strStartTime = value; } }
        }

        private DateTime _strEndTime;
        /// <summary>
        /// End Time
        /// </summary>
        public DateTime EndTime
        {
            get { return _strEndTime; }
            set { if (this._strEndTime != value) { _strEndTime = value; } }
        }

        private string _strDuration;
        /// <summary>
        /// _strDuration
        /// </summary>
        public string Duration 
        {
            get { return _strDuration; }
            set { if (this._strDuration != value) { _strDuration = value; } }
        }

        private string _stripAddress;
        /// <summary>
        /// ipAddress
        /// </summary>
        public string IPAddress
        {
            get { return _stripAddress; }
            set { if (this._stripAddress != value) { _stripAddress = value; } }
        }
        
        private string _strClientAgent;
        /// <summary>
        /// _strClientAgent
        /// </summary>
        public string ClientAgent
        {
            get { return _strClientAgent; }
            set { if (this._strClientAgent != value) { _strClientAgent = value; } }
        }
        private bool _strAttendedStatus;
        /// <summary>
        /// AttendedStatus
        /// </summary>
        public bool AttendedStatus
        {
            get { return _strAttendedStatus; }
            set { if (this._strAttendedStatus != value) { _strAttendedStatus = value; } }
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
            UpdateSessionAttendeeList,
            CheckEmailExist
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkUpdateVirtualTrainingAttendeeMaster,
            GetAllAttendeeList,
            AssignedAttendeeDelete,
            GetAllUsers,
            GetAllACCEPTANDREGISTER,
            BulkUpdateVirtualTrainingFailureList,
        }
    }
}
