/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Ashish
* Created:<11/27/09>
* Last Modified:<12/01/10>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// LoginKey to track Session tblLoginKeyMaster
    /// </summary>
   [Serializable] public class LoginKey : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public LoginKey()
        { }

        /// <summary>
        /// ENUM Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add
        }

        private string _strSystemUserGUID;
        /// <summary>
        /// User Id
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
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

        private bool _bIsSSOLogin;
        /// <summary>
        /// Is Singlie Sign On Login
        /// </summary>
        public bool IsSSOLogin
        {
            get { return _bIsSSOLogin; }
            set { if (this._bIsSSOLogin != value) { _bIsSSOLogin = value; } }
        }

        private int _iPageSize;
        /// <summary>
        /// Page Size
        /// </summary>
        public int PageSize
        {
            get { return _iPageSize; }
            set { if (this._iPageSize != value) { _iPageSize = value; } }
        }

        private string _strStartUrl;
        /// <summary>
        /// StartUrl
        /// </summary>
        public string StartUrl
        {
            get { return _strStartUrl; }
            set { if (this._strStartUrl != value) { _strStartUrl = value; } }
        }
    }
}