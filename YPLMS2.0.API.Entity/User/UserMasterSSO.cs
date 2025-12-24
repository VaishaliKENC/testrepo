/*
* Copyright Encora
* This source file and source code is proprietary property of Encora* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:Shrihari
* Created:<9/16/2019>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class UserMasterSSO : BaseEntity 
    /// </summary>
    /// 
    public class UserMasterSSO : BaseEntity
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public UserMasterSSO()
        { }


        private string _strSSOSystemUserGUID;
        /// <summary>
        /// SSOSystemUserGUID
        /// </summary>
        public string SSOSystemUserGUID
        {
            get { return _strSSOSystemUserGUID; }
            set { if (this._strSSOSystemUserGUID != value) { _strSSOSystemUserGUID = value; } }
        }

        private string _strClientId;
        /// <summary>
        /// ClientId
        /// </summary>
        public string ClientId
        {
            get { return _strClientId; }
            set { if (this._strClientId != value) { _strClientId = value; } }
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

        private string _strUserPassword;
        /// <summary>
        /// User Password
        /// </summary>
        public string UserPassword
        {
            get { return _strUserPassword; }
            set { if (this._strUserPassword != value) { _strUserPassword = value; } }
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