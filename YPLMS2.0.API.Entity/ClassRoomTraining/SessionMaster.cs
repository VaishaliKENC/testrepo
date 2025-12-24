/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<12/26/2011>
* Last Modified:
*/

using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class SessionMaster : BaseEntity 
    /// </summary>
    /// 
    public class SessionMaster : SessionLanguage
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public  SessionMaster()
        { }


        private string _strProgramId;
        /// <summary>
        /// ProgramId
        /// </summary>
        public string ProgramId
        {
            get { return _strProgramId; }
            set { if (this._strProgramId != value) { _strProgramId = value; } }
        }

        private string _strProgramName;
        /// <summary>
        /// ProgramId
        /// </summary>
        public string ProgramName
        {
            get { return _strProgramName; }
            set { if (this._strProgramName != value) { _strProgramName = value; } }
        }

        //private string _strSessionName;
        ///// <summary>
        ///// SessionName
        ///// </summary>
        //public string SessionName
        //{
        //    get { return _strSessionName; }
        //    set { if (this._strSessionName != value) { _strSessionName = value; } }
        //}

        private string _strSessionTypeId;
        /// <summary>
        /// SessionTypeId
        /// </summary>
        public string SessionTypeId
        {
            get { return _strSessionTypeId; }
            set { if (this._strSessionTypeId != value) { _strSessionTypeId = value; } }
        }

        private string _strSessionId;
        /// <summary>
        /// SessionTypeId
        /// </summary>
        public string SessionId
        {
            get { return _strSessionId; }
            set { if (this._strSessionId != value) { _strSessionId = value; } }
        }

        private string _strSessionLocationId;
        /// <summary>
        /// SessionLocationId
        /// </summary>
        public string SessionLocationId
        {
            get { return _strSessionLocationId; }
            set { if (this._strSessionLocationId != value) { _strSessionLocationId = value; } }
        }

        private string _strSessionLocationName;
        /// <summary>
        /// SessionLocationName
        /// </summary>
        public string SessionLocationName
        {
            get { return _strSessionLocationName; }
            set { if (this._strSessionLocationName != value) { _strSessionLocationName = value; } }
        }


        private DateTime _strSessionFromDate;
        /// <summary>
        /// SessionFromDate
        /// </summary>
        public DateTime SessionFromDate
        {
            get { return _strSessionFromDate; }
            set { if (this._strSessionFromDate != value) { _strSessionFromDate = value; } }
        }

        private DateTime _strSessionToDate;
        /// <summary>
        /// SessionToDate
        /// </summary>
        public DateTime SessionToDate
        {
            get { return _strSessionToDate; }
            set { if (this._strSessionToDate != value) { _strSessionToDate = value; } }
        }

        //private bool? _strSessionStatus;
        ///// <summary>
        ///// SessionStatus
        ///// </summary>
        //public bool? SessionStatus
        //{
        //    get { return _strSessionStatus; }
        //    set { if (this._strSessionStatus != value) { _strSessionStatus = value; } }
        //}

        public bool? SessionStatus { get; set; }

        private string _strSessionResourse;
        /// <summary>
        /// SessionResourse
        /// </summary>
        public string SessionResourse
        {
            get { return _strSessionResourse; }
            set { if (this._strSessionResourse != value) { _strSessionResourse = value; } }
        }

        private string _strInstructorId;
        /// <summary>
        /// InstructorId
        /// </summary>
        public string InstructorId
        {
            get { return _strInstructorId; }
            set { if (this._strInstructorId != value) { _strInstructorId = value; } }
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

        private decimal _strDuration;
        /// <summary>
        /// MinRegistrations
        /// </summary>
        public decimal Duration
        {
            get { return _strDuration; }
            set { if (this._strDuration != value) { _strDuration = value; } }
        }
        
        private float _strSessionReminder;
        /// <summary>
        /// SessionReminder
        /// </summary>
        public float SessionReminder
        {
            get { return _strSessionReminder; }
            set { if (this._strSessionReminder != value) { _strSessionReminder = value; } }
        }

        private decimal _strSessionCost;
        /// <summary>
        /// SessionCost
        /// </summary>
        public decimal SessionCost
        {
            get { return _strSessionCost; }
            set { if (this._strSessionCost != value) { _strSessionCost = value; } }
        }

        private string _strSessionFromTime;
        /// <summary>
        /// _strSessionFromTime
        /// </summary>
        public string SessionFromTime
        {
            get { return _strSessionFromTime; }
            set { if (this._strSessionFromTime != value) { _strSessionFromTime = value; } }
        }

        private string _strSessionToTime;
        /// <summary>
        /// _strSessionToTime
        /// </summary>
        public string SessionToTime
        {
            get { return _strSessionToTime; }
            set { if (this._strSessionToTime != value) { _strSessionToTime = value; } }
        }

        //private string _strSessionDescription;
        ///// <summary>
        ///// SpeakerName
        ///// </summary>
        //public string SessionDescription
        //{
        //    get { return _strSessionDescription; }
        //    set { if (this._strSessionDescription != value) { _strSessionDescription = value; } }
        //}

        private List<SessionAllocatedSpeakers> _entSAS;
        /// <summary>
        /// SessionAllocatedSpeakers
        /// </summary>
        public List<SessionAllocatedSpeakers> entSessionAllocatedSpeakers
        {
            get { return _entSAS; }
            set { _entSAS = value; }
        }

        private List<SessionAllocatedResources> _entSAR;
        /// <summary>
        /// SessionAllocatedResources
        /// </summary>
        public List<SessionAllocatedResources> entSessionAllocatedResources
        {
            get { return _entSAR; }
            set { _entSAR = value; }
        }

        //Add by Kunal
        private string _strSessionLocationVenue;
        /// <summary>
        /// SessionLocationVenue
        /// </summary>
        public string SessionLocationVenue
        {
            get { return _strSessionLocationVenue; }
            set { if (this._strSessionLocationVenue != value) { _strSessionLocationVenue = value; } }
        }

        private string _strInstructorEmail;
        /// <summary>
        /// InstructorEmail
        /// </summary>
        public string InstructorEmail
        {
            get { return _strInstructorEmail; }
            set { if (this._strInstructorEmail != value) { _strInstructorEmail = value; } }
        }

        private string _strInstructorDetails;
        /// <summary>
        /// InstructorDetails
        /// </summary>
        public string InstructorDetails
        {
            get { return _strInstructorDetails; }
            set { if (this._strInstructorDetails != value) { _strInstructorDetails = value; } }
        }
        //End Add by Kunal


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
            UpdateLanguage
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllForNomination,
            GetAllForProgram,
            GetSessionLanguages,
            GetAllForProgramDetails
        }
    }
}