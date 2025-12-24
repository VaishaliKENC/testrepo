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
    /// class ILTSessionLanguageMaster : BaseEntity 
    /// </summary>
    /// 
    public class ILTSessionLanguageMaster : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ILTSessionLanguageMaster()
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

        private string _strSessionName;
        /// <summary>
        /// SessionName
        /// </summary>
        public string SessionName
        {
            get { return _strSessionName; }
            set { if (this._strSessionName != value) { _strSessionName = value; } }
        }

        private string _strSessionInterest;
        /// <summary>
        /// SessionInterest
        /// </summary>
        public string Interest
        {
            get { return _strSessionInterest; }
            set { if (this._strSessionInterest != value) { _strSessionInterest = value; } }
        }

        private string _strSessionDescription;
        /// <summary>
        /// SessionDescription
        /// </summary>
        public string SessionDescription
        {
            get { return _strSessionDescription; }
            set { if (this._strSessionDescription != value) { _strSessionDescription = value; } }
        }

        private string _strSessionPreWork;
        /// <summary>
        /// SessionPreWork
        /// </summary>
        public string SessionPreWork
        {
            get { return _strSessionPreWork; }
            set { if (this._strSessionPreWork != value) { _strSessionPreWork = value; } }
        }

        private string _strSessionPostWork;
        /// <summary>
        /// SessionPostWork
        /// </summary>
        public string SessionPostWork
        {
            get { return _strSessionPostWork; }
            set { if (this._strSessionPostWork != value) { _strSessionPostWork = value; } }
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