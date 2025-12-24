/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh & Ashish
* Created:<13/07/09>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// UserPassCode Class inherited from BaseEntity
    /// </summary>
    /// 
    [Serializable]
     public class UserPassCodeInstance : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public UserPassCodeInstance()
        {
            _listUserPassCode = new List<UserPassCode>();

        }

        /// <summary>
        /// ENUM Method
        /// </summary>
        public new enum Method
        {
            Get,
            AddPassCodeInstance,
            UpdateStatus,
            Delete,
            SelByName
        }

        /// <summary>
        /// List method ENUM
        /// </summary>
        public new enum ListMethod
        {
            GetAllUserPassCodesInstance
        }

        private string _strInstanceTitle;
        /// <summary>
        /// PassCode
        /// </summary>
        public string InstanceTitle
        {
            get { return _strInstanceTitle; }
            set { if (this._strInstanceTitle != value) { _strInstanceTitle = value; } }
        }

        private string _strEmailList;
        /// <summary>
        /// Email List
        /// </summary>
        public string EmailList
        {
            get { return _strEmailList; }
            set { if (this._strEmailList != value) { _strEmailList = value; } }
        }

        private int _iMaximumUsage;
        /// <summary>
        /// PassCode MaximumUsage
        /// </summary>
        public int MaximumUsage
        {
            get { return _iMaximumUsage; }
            set { if (this._iMaximumUsage != value) { _iMaximumUsage = value; } }
        }

        private int _iTotalConsumed;
        /// <summary>
        /// PassCode MaximumUsage
        /// </summary>
        public int TotalConsumed
        {
            get { return _iTotalConsumed; }
            set { if (this._iTotalConsumed != value) { _iTotalConsumed = value; } }
        }

        private int _iNoOfTimes;
        /// <summary>
        /// Count
        /// </summary>
        public int NoOfTimes
        {
            get { return _iNoOfTimes; }
            set { if (this._iNoOfTimes != value) { _iNoOfTimes = value; } }
        }

        private int _iNoOfPassCodes;
        /// <summary>
        /// Number of Passcodes
        /// </summary>
        public int NoOfPassCodes
        {
            get { return _iNoOfPassCodes; }
            set { if (this._iNoOfPassCodes != value) { _iNoOfPassCodes = value; } }
        }

        private int _iNoOfMonths;
        /// <summary>
        /// Passcode validity duration in Months
        /// </summary>
        public int NoOfMonths
        {
            get { return _iNoOfMonths; }
            set { if (this._iNoOfMonths != value) { _iNoOfMonths = value; } }
        }

        private DateTime _dateExpiryDate;
        /// <summary>
        /// Passcode Expiry Date
        /// </summary>
        public DateTime ExpiryDate
        {
            get { return _dateExpiryDate; }
            set { if (this._dateExpiryDate != value) { _dateExpiryDate = value; } }
        }

        private bool _bIsActive;
        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
        }

        private List<UserPassCode> _listUserPassCode;
        /// <summary>
        /// List of Passcode
        /// </summary>
        public List<UserPassCode> UserPassCode
        {
            get { return _listUserPassCode; }
        }
    }
}