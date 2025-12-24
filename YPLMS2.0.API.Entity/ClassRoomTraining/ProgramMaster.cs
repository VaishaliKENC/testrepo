/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<12/27/2011>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ProgramMaster : BaseEntity 
    /// </summary>
    /// 
    public class ProgramMaster : ProgramLanguage
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ProgramMaster()
        { }


        private string _strProgramTypeId;
        /// <summary>
        /// ProgramTypeId
        /// </summary>
        public string ProgramTypeId
        {
            get { return _strProgramTypeId; }
            set { if (this._strProgramTypeId != value) { _strProgramTypeId = value; } }
        }

        private string _strProgramTypeName;
        /// <summary>
        /// ProgramTypeId
        /// </summary>
        public string ProgramTypeName
        {
            get { return _strProgramTypeName; }
            set { if (this._strProgramTypeName != value) { _strProgramTypeName = value; } }
        }

        //private string _strProgramName;
        ///// <summary>
        ///// ProgramName
        ///// </summary>
        //public string ProgramName
        //{
        //    get { return _strProgramName; }
        //    set { if (this._strProgramName != value) { _strProgramName = value; } }
        //}

        //private string _strProgramDescription;
        ///// <summary>
        ///// ProgramDescription
        ///// </summary>
        //public string ProgramDescription
        //{
        //    get { return _strProgramDescription; }
        //    set { if (this._strProgramDescription != value) { _strProgramDescription = value; } }
        //}


        private float _strProgramDuration;
        /// <summary>
        /// ProgramDuration
        /// </summary>
        public float ProgramDuration
        {
            get { return _strProgramDuration; }
            set { if (this._strProgramDuration != value) { _strProgramDuration = value; } }
        }

        private decimal _strProgramCost;
        /// <summary>
        /// ProgramCost
        /// </summary>
        public decimal ProgramCost
        {
            get { return _strProgramCost; }
            set { if (this._strProgramCost != value) { _strProgramCost = value; } }
        }


        //private string _strContactPersonEmailID;
        ///// <summary>
        ///// ContactPersonEmailID
        ///// </summary>
        //public string ContactPersonEmailID
        //{
        //    get { return _strContactPersonEmailID; }
        //    set { if (this._strContactPersonEmailID != value) { _strContactPersonEmailID = value; } }
        //}


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


        private string _strInstructorId;
        /// <summary>
        /// SpeakerId
        /// </summary>
        public string InstructorId
        {
            get { return _strInstructorId; }
            set { if (this._strInstructorId != value) { _strInstructorId = value; } }
        }

        private string _strInstructorName;
        /// <summary>
        /// SpeakerName
        /// </summary>
        public string InstructorName
        {
            get { return _strInstructorName; }
            set { if (this._strInstructorName != value) { _strInstructorName = value; } }
        }

        private bool _strSelfNomination;
        /// <summary>
        /// SelfNomination
        /// </summary>
        public bool SelfNomination
        {
            get { return _strSelfNomination; }
            set { if (this._strSelfNomination != value) { _strSelfNomination = value; } }
        }

        private DateTime _strProgramOpenFromDate;
        /// <summary>
        /// ProgramOpenFromDate
        /// </summary>
        public DateTime ProgramOpenFromDate
        {
            get { return _strProgramOpenFromDate; }
            set { if (this._strProgramOpenFromDate != value) { _strProgramOpenFromDate = value; } }
        }

        private DateTime _strProgramOpenToDate;
        /// <summary>
        /// ProgramOpenToDate
        /// </summary>
        public DateTime ProgramOpenToDate
        {
            get { return _strProgramOpenToDate; }
            set { if (this._strProgramOpenToDate != value) { _strProgramOpenToDate = value; } }
        }

        //private string _strProgramPreWork;
        ///// <summary>
        ///// SpeakerId
        ///// </summary>
        //public string ProgramPreWork
        //{
        //    get { return _strProgramPreWork; }
        //    set { if (this._strProgramPreWork != value) { _strProgramPreWork = value; } }
        //}

        //private string _strProgramPostWork;
        ///// <summary>
        ///// SpeakerId
        ///// </summary>
        //public string ProgramPostWork
        //{
        //    get { return _strProgramPostWork; }
        //    set { if (this._strProgramPostWork != value) { _strProgramPostWork = value; } }
        //}

        private bool _strDirectApprove;
        /// <summary>
        /// SelfNomination
        /// </summary>
        public bool DirectApprove
        {
            get { return _strDirectApprove; }
            set { if (this._strDirectApprove != value) { _strDirectApprove = value; } }
        }

        private bool _strIncludeWaitlisted;
        /// <summary>
        /// SelfNomination
        /// </summary>
        public bool IncludeWaitlisted
        {
            get { return _strIncludeWaitlisted; }
            set { if (this._strIncludeWaitlisted != value) { _strIncludeWaitlisted = value; } }
        }

        private DateTime _strLastDateOfNomination;
        /// <summary>
        /// LastDateOfNomination
        /// </summary>
        public DateTime LastDateOfNomination
        {
            get { return _strLastDateOfNomination; }
            set { if (this._strLastDateOfNomination != value) { _strLastDateOfNomination = value; } }
        }

        private DateTime _strLastDateOfCancellation;
        /// <summary>
        /// LastDateOfCancellation
        /// </summary>
        public DateTime LastDateOfCancellation
        {
            get { return _strLastDateOfCancellation; }
            set { if (this._strLastDateOfCancellation != value) { _strLastDateOfCancellation = value; } }
        }

        private bool _strWaitlisting;
        /// <summary>
        /// Waitlisting
        /// </summary>
        public bool Waitlisting
        {
            get { return _strWaitlisting; }
            set { if (this._strWaitlisting != value) { _strWaitlisting = value; } }
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

        private int _strMaxRegistrations;
        /// <summary>
        /// MaxRegistrations
        /// </summary>
        public int MaxRegistrations
        {
            get { return _strMaxRegistrations; }
            set { if (this._strMaxRegistrations != value) { _strMaxRegistrations = value; } }
        }

        private int _strMinRegistrations;
        /// <summary>
        /// MinRegistrations
        /// </summary>
        public int MinRegistrations
        {
            get { return _strMinRegistrations; }
            set { if (this._strMinRegistrations != value) { _strMinRegistrations = value; } }
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

        private string _strSystemUserGUID;
        /// <summary>
        /// SessionStatus
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }


        private string _strPreferredTimeZone;
        /// <summary>
        /// ProgramTypeId
        /// </summary>
        public string PreferredTimeZone
        {
            get { return _strPreferredTimeZone; }
            set { if (this._strPreferredTimeZone != value) { _strPreferredTimeZone = value; } }
        }


        private Nullable<bool> _isActive;
        /// <summary>
        /// ProgramIsActive
        /// </summary>
        public Nullable<bool> IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
        }


        //Add by Kunal
        private string _strLocationVenue;
        /// <summary>
        /// SessionLocationVenue
        /// </summary>
        public string LocationVenue
        {
            get { return _strLocationVenue; }
            set { if (this._strLocationVenue != value) { _strLocationVenue = value; } }
        }

        private string _strInstructorEmail;
        /// <summary>
        /// SessionInstructorEmail
        /// </summary>
        public string InstructorEmail
        {
            get { return _strInstructorEmail; }
            set { if (this._strInstructorEmail != value) { _strInstructorEmail = value; } }
        }

        private string _strInstructorDetails;
        /// <summary>
        /// SessionInstructorDetails
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
            UpdateLanguage
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllLearner,
            GetAllForNomination,
            GetAllForNominationForCurriculum,
            GetProgramLanguages
        }
    }
}