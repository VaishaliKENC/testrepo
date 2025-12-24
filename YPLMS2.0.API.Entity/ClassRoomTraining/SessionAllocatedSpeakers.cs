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

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class SessionAllocatedSpeakers : BaseEntity 
    /// </summary>
    /// 
    public class SessionAllocatedSpeakers : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public SessionAllocatedSpeakers()
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

        private string _strSpeakerId;
        /// <summary>
        /// SessionId
        /// </summary>
        public string SpeakerId
        {
            get { return _strSpeakerId; }
            set { if (this._strSpeakerId != value) { _strSpeakerId = value; } }
        }

        private float _strSpeakerSessionTime;
        /// <summary>
        /// SpeakerSessionTime
        /// </summary>
        public float SpeakerSessionTime
        {
            get { return _strSpeakerSessionTime; }
            set { if (this._strSpeakerSessionTime != value) { _strSpeakerSessionTime = value; } }
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
            GetAll
        }
    }
}