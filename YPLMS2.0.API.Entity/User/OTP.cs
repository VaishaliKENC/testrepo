/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Bharat
* Created:<17/12/15>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// OTP Class inherited from BaseEntity
    /// </summary>
    [Serializable]
     public class OTP : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public OTP()
        {            
            
        }

        /// <summary>
        /// ENUM Method
        /// </summary>
        public new enum Method
        {           
            GetOTPNumber,
            AddOTP,
            CheckExpireOTP,
            CheckOTPNumber
        }

        /// <summary>
        /// ENUM ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetOTPNumber
        }
        /// <summary>
        /// constant USER_OTP_ID
        /// </summary>
        public const string USER_OTP_ID = "otp";

        private string _systemUserGuid;
        /// <summary>
        /// SystemUserGuid
        /// </summary>
        public string SystemUserGuid
        {
            get { return _systemUserGuid; }
            set { if (this._systemUserGuid != value) { _systemUserGuid = value; } }
        }

        private string _OTPNumber;
        /// <summary>
        /// OTP Number
        /// </summary>
        public string OTPNumber
        {
            get { return _OTPNumber; }
            set { if (this._OTPNumber != value) { _OTPNumber = value; } }
        }

        private DateTime _ExpireDateTime;
        /// <summary>
        /// Date Of Expire 
        /// </summary>
        public DateTime ExpireDateTime
        {
            get { return _ExpireDateTime; }
            set { if (this._ExpireDateTime != value) { _ExpireDateTime = value; } }
        }
        private string _UserFirstName;
        /// <summary>
        /// User First tName
        /// </summary>
          public string UserFirstName
        {
            get { return _UserFirstName; }
            set { if (this._UserFirstName != value) { _UserFirstName = value; } }
        }
          private string _UserLastName;
          /// <summary>
          /// User Last tName
          /// </summary>
          public string UserLastName
          {
              get { return _UserLastName; }
              set { if (this._UserLastName != value) { _UserLastName = value; } }
          }
    }
}