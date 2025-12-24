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
    /// class ILTManageEventMaster : BaseEntity 
    /// </summary>
    /// 
    public class ILTManageEventMaster : ILTManageEventLanguageMaster
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ILTManageEventMaster()
        { }

        private string _strEventId;
        /// <summary>
        /// EventId
        /// </summary>
        public string EventId
        {
            get { return _strEventId; }
            set { if (this._strEventId != value) { _strEventId = value; } }
        }

        private bool _strIsActive;
        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive
        {
            get { return _strIsActive; }
            set { if (this._strIsActive != value) { _strIsActive = value; } }
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

        private Int32 _strDuration;
        /// <summary>
        /// Duration
        /// </summary>
        public Int32 Duration
        {
            get { return _strDuration; }
            set { if (this._strDuration != value) { _strDuration = value; } }
        }

        private Int32 _strNoOfSessions;
        /// <summary>
        /// NoOfSessions
        /// </summary>
        public Int32 NoOfSessions
        {
            get { return _strNoOfSessions; }
            set { if (this._strNoOfSessions != value) { _strNoOfSessions = value; } }
        }

        private string _strNominationCount;
        /// <summary>
        /// NominationCount
        /// </summary>
        public string NominationCount
        {
            get { return _strNominationCount; }
            set { if (this._strNominationCount != value) { _strNominationCount = value; } }
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
            Add,
            Update,
            Delete,
            UpdateLanguage,
            CopyEventDetails,
            GetByName
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllUsers
        }
    }
}