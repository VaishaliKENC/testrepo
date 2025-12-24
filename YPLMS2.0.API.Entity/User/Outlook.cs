/* 
* Copyright Indecomm Global Services
* This source file and source code is proprietary property of Indecomm Global Services 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Indecomm's Client.
* Author:Bharat
* Created:<09/04/19>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// Outlook Class inherited from BaseEntity
    /// </summary>
    [Serializable]
     public class Outlook : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public Outlook()
        {            
            
        }

        /// <summary>
        /// ENUM Method
        /// </summary>
        public new enum Method
        {           
            //GetOTPNumber,
            //AddOTP,
            //CheckExpireOTP,
            //CheckOTPNumber
        }

        /// <summary>
        /// ENUM ListMethod
        /// </summary>
        public new enum ListMethod
        {
            //GetOTPNumber
        }
        /// <summary>
        /// constant USER_OTP_ID
        /// </summary>
        //public const string USER_OTP_ID = "otp";

        private string _systemUserGuid;
        /// <summary>
        /// System User Guid
        /// </summary>
        public string SystemUserGuid
        {
            get { return _systemUserGuid; }
            set { if (this._systemUserGuid != value) { _systemUserGuid = value; } }
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
      

         private string _strpreferredDate;
        /// <summary>
        /// Preferred Date Format
        /// </summary>
        public string PreferredDateFormat
        {
            get { return _strpreferredDate; }
            set { if (this._strpreferredDate != value) { _strpreferredDate = value; } }
        }
        
        private string _strPreferredTimeZone;
        /// <summary>
        /// Preferred Time Zone
        /// </summary>
        public string PreferredTimeZone
        {
            get { return _strPreferredTimeZone; }
            set { if (this._strPreferredTimeZone != value) { _strPreferredTimeZone = value; } }
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

        private string _strEmailID;
        /// <summary>
        /// Email ID
        /// </summary>
        public string EmailID
        {
            get { return _strEmailID; }
            set { if (this._strEmailID != value) { _strEmailID = value; } }
        }
        private string _strDefaultLanguageId;
        /// <summary>
        /// Users Default Language Id
        /// </summary>
        public string DefaultLanguageId
        {
            get { return _strDefaultLanguageId; }
            set { if (this._strDefaultLanguageId != value) { _strDefaultLanguageId = value; } }
        }

        private string _strActivityId;
        /// <summary>
        /// Activity Id
        /// </summary>
        public string ActivityId
        {
            get { return _strActivityId; }
            set { if (this._strActivityId != value) { _strActivityId = value; } }
        }

        private string _strActivityName;
        /// <summary>
        /// Activity Name
        /// </summary>
        public string ActivityName
        {
            get { return _strActivityName; }
            set { if (this._strActivityName != value) { _strActivityName = value; } }
        }

        private string _strSubject;
        /// <summary>
        /// Subject
        /// </summary>
        public string Subject
        {
            get { return _strSubject; }
            set { if (this._strSubject != value) { _strSubject = value; } }
        }

        private string strDescription;
        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get { return strDescription; }
            set { if (this.strDescription != value) { strDescription = value; } }
        }

        private string strLocation;
        /// <summary>
        /// Location
        /// </summary>
        public string Location
        {
            get { return strLocation; }
            set { if (this.strLocation != value) { strLocation = value; } }
        }

        private string strStartDate;
        /// <summary>
        /// StartDate
        /// </summary>
        public string StartDate
        {
            get { return strStartDate; }
            set { if (this.strStartDate != value) { strStartDate = value; } }
        }

        private string strEndDate;
        /// <summary>
        /// EndDate
        /// </summary>
        public string EndDate
        {
            get { return strEndDate; }
            set { if (this.strEndDate != value) { strEndDate = value; } }
        }

        private string strClientID;
        /// <summary>
        /// ClientID
        /// </summary>
        public string ClientID
        {
            get { return strClientID; }
            set { if (this.strClientID != value) { strClientID = value; } }
        }

        private string strOperation;
        /// <summary>
        /// Operation
        /// </summary>
        public string Operation
        {
            get { return strOperation; }
            set { if (this.strOperation != value) { strOperation = value; } }
        }
    }
}