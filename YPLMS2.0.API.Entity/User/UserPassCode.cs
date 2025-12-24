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
    public class UserPassCode : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public UserPassCode()
        {
        }

        /// <summary>
        /// ENUM Method
        /// </summary>
        public new enum Method
        {
            Get,
            AddTracking
        }

        /// <summary>
        /// List method ENUM
        /// </summary>
        public new enum ListMethod
        {
            GetAllUserPassCodes
        }

        private string _strLearnerId;
        /// <summary>
        /// PassCode
        /// </summary>
        public string LearnerID
        {
            get { return _strLearnerId; }
            set { if (this._strLearnerId != value) { _strLearnerId = value; } }
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

        private string _strStatus;
        /// <summary>
        /// Status
        /// </summary>
        public string Status
        {
            get { return _strStatus; }
            set { if (this._strStatus != value) { _strStatus = value; } }
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

        private Nullable<bool> _bIsConsumed;
        /// <summary>
        /// Is Consumed - Physical Column in DB not present. It is calculated based on Actual and Maximum usage. Parameter to be send for LstAll Method
        /// </summary>
        public Nullable<bool> IsConsumed
        {
            get { return _bIsConsumed; }
            set { if (this._bIsConsumed != value) { _bIsConsumed = value; } }
        }

        private int _iActualUsage;
        /// <summary>
        ///PassCode ActualUsage
        /// </summary>
        public int ActualUsage
        {
            get { return _iActualUsage; }
            set { if (this._iActualUsage != value) { _iActualUsage = value; } }
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

        private DateTime _dateOfRegistration;
        /// <summary>
        /// Date Of Registration
        /// </summary>
        public DateTime DateOfRegistration
        {
            get { return _dateOfRegistration; }
            set { if (this._dateOfRegistration != value) { _dateOfRegistration = value; } }
        }

        private string _strPassCodeInstanceId;
        /// <summary>
        /// Client Id
        /// </summary>
        public string PassCodeInstanceId
        {
            get { return _strPassCodeInstanceId; }
            set { if (this._strPassCodeInstanceId != value) { _strPassCodeInstanceId = value; } }
        }

        public bool Validate(bool pIsUpdate)
        {

            if (pIsUpdate)
            {
                if (String.IsNullOrEmpty(ID))
                    return false;

                if (DateCreated.AddMonths(NoOfMonths) > DateTime.Now)
                    return false;

                if (ActualUsage >= MaximumUsage)
                    return false;

                if (ExpiryDate > DateTime.Now)
                    return false;

            }
            else
            {
                if (String.IsNullOrEmpty(ClientId))
                    return false;
            }
            return true;
        }
    }
}