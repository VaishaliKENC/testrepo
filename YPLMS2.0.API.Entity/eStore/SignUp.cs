/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:
* Created:
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// Learner Class inherited from BaseEntity
    /// </summary>
    [Serializable]
     public class SignUp : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public SignUp()
        {
            _entListUserCustomFieldValue = new List<UserCustomFieldValue>();
        }

        /// <summary>
        /// ENUM Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete,
            IsLoginIdAvailable,
            IsEmailAvailbale,
            UpdateSignUpCustomField,
        }
        /// <summary>
        /// List method ENUM
        /// </summary>
        public new enum ListMethod
        {
            GetAll
        }

        private bool _IsDoNotDeleteCustomeFiledValue;
        public bool IsDoNotDeleteCustomeFiledValue
        {
            get { return _IsDoNotDeleteCustomeFiledValue; }
            set { if (this._IsDoNotDeleteCustomeFiledValue != value) { _IsDoNotDeleteCustomeFiledValue = value; } }
        }


        private List<UserCustomFieldValue> _entListUserCustomFieldValue;
        /// <summary>
        /// User Custom Field Value
        /// </summary>
        public List<UserCustomFieldValue> UserCustomFieldValue
        {
            get { return _entListUserCustomFieldValue; }
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

        private string _strFirstName;
        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName
        {
            get { return _strFirstName; }
            set { if (this._strFirstName != value) { _strFirstName = value; } }
        }

        private string _strMiddleName;
        /// <summary>
        /// Middle Name
        /// </summary>
        public string MiddleName
        {
            get { return _strMiddleName; }
            set { if (this._strMiddleName != value) { _strMiddleName = value; } }
        }

        private string _strLastName;
        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName
        {
            get { return _strLastName; }
            set { if (this._strLastName != value) { _strLastName = value; } }
        }

        private string _strAddress;
        /// <summary>
        /// Address
        /// </summary>
        public string Address
        {
            get { return _strAddress; }
            set { if (this._strAddress != value) { _strAddress = value; } }
        }

        private string _strEmailID;
        /// <summary>
        /// EmailID
        /// </summary>
        public string EmailID
        {
            get { return _strEmailID; }
            set { if (this._strEmailID != value) { _strEmailID = value; } }
        }

        private DateTime _dateOfBirth;
        /// <summary>
        /// DateOfBirth
        /// </summary>
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { if (this._dateOfBirth != value) { _dateOfBirth = value; } }
        }

        private DateTime _dateOfRegistration;
        /// <summary>
        /// Date Of Registration
        /// </summary>
        public DateTime DateOfRegistration
        {
            get { return _dateOfRegistration; }
            set { if (this._dateOfRegistration != value) { _dateOfRegistration = value; } }
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

        private string _strPhoneNo;
        /// <summary>
        /// Phone No   
        /// </summary>
        public string PhoneNo
        {
            get { return _strPhoneNo; }
            set { if (this._strPhoneNo != value) { _strPhoneNo = value; } }
        }

        private string _strLocationId;
        /// <summary>
        /// Users LocationId
        /// </summary>
        public string LocationID
        {
            get { return _strLocationId; }
            set { if (this._strLocationId != value) { _strLocationId = value; } }
        }

        private string _strCurrencyId;
        /// <summary>
        /// Users CurrencyId
        /// </summary>
        public string CurrencyID
        {
            get { return _strCurrencyId; }
            set { if (this._strCurrencyId != value) { _strCurrencyId = value; } }
        }

        private string _strSystemUserGUID;
        /// <summary>
        /// Users _strSystemUserGUID
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private string _strpreferredDate;
        /// <summary>
        /// First Name
        /// </summary>
        public string PreferredDateFormat
        {
            get { return _strpreferredDate; }
            set { if (this._strpreferredDate != value) { _strpreferredDate = value; } }
        }

        private string _strPreferredTimeZone;
        /// <summary>
        /// First Name
        /// </summary>
        public string PreferredTimeZone
        {
            get { return _strPreferredTimeZone; }
            set { if (this._strPreferredTimeZone != value) { _strPreferredTimeZone = value; } }
        }

    }
}
