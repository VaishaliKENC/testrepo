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
    /// class ILTManageEventLanguageMaster : BaseEntity 
    /// </summary>
    /// 
    public class ILTManageEventLanguageMaster : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ILTManageEventLanguageMaster()
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

        private string _strEventName;
        /// <summary>
        /// EventName
        /// </summary>
        public string EventName
        {
            get { return _strEventName; }
            set { if (this._strEventName != value) { _strEventName = value; } }
        }

        private string _strUserNameAlias;
        /// <summary>
        /// UserNameAlias
        /// </summary>
        public string UserNameAlias
        {
            get { return _strUserNameAlias; }
            set { if (this._strUserNameAlias != value) { _strUserNameAlias = value; } }
        }

        private string _strSystemUserGUID;
        /// <summary>
        /// UserNameAlias
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private string _strEmailId;
        /// <summary>
        /// UserNameAlias
        /// </summary>
        public string EmailId
        {
            get { return _strEmailId; }
            set { if (this._strEmailId != value) { _strEmailId = value; } }
        }

        private string _strUserName;
        /// <summary>
        /// UserNameAlias
        /// </summary>
        public string UserName
        {
            get { return _strUserName; }
            set { if (this._strUserName != value) { _strUserName = value; } }
        }

        private string _strKeyword;
        /// <summary>
        /// Keyword
        /// </summary>
        public string Keyword
        {
            get { return _strKeyword; }
            set { if (this._strKeyword != value) { _strKeyword = value; } }
        }

        private string _strDescription;
        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get { return _strDescription; }
            set { if (this._strDescription != value) { _strDescription = value; } }
        }

        private string _strObjective;
        /// <summary>
        /// Objective
        /// </summary>
        public string Objective
        {
            get { return _strObjective; }
            set { if (this._strObjective != value) { _strObjective = value; } }
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