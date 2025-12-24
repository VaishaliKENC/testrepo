/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh
* Created:<21/09/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// 
    /// </summary>
   [Serializable] public class PasswordPolicyConfiguration : BaseEntity
    {
        //For Cache
        public const string CACHE_SUFFIX = "_PWDPOLICY";

        /// <summary>
        /// Default Contructor
        /// <summary>
        public PasswordPolicyConfiguration()
        { }



        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete,
            GetEmailRequestDetails,
            AddUpdateEmailRequests,
            AddUpdateOTPEmailRequests
        }

        private string _passwordPolicyId;
        public string PasswordPolicyId
        {
            get { return _passwordPolicyId; }
            set { if (this._passwordPolicyId != value) { _passwordPolicyId = value; } }
        }

        private int _maxPaswordLength;
        public int MaxPaswordLength
        {
            get { return _maxPaswordLength; }
            set { if (this._maxPaswordLength != value) { _maxPaswordLength = value; } }
        }

        private int _minPaswordLength;
        public int MinPaswordLength
        {
            get { return _minPaswordLength; }
            set { if (this._minPaswordLength != value) { _minPaswordLength = value; } }
        }

        private string _defaultPassword;
        public string DefaultPassword
        {
            get { return _defaultPassword; }
            set { if (this._defaultPassword != value) { _defaultPassword = value; } }
        }

        private bool _isConfirmPassword;
        public bool IsConfirmPassword
        {
            get { return _isConfirmPassword; }
            set { if (this._isConfirmPassword != value) { _isConfirmPassword = value; } }
        }

        private int _passwordExpiryDuration;
        public int PasswordExpiryDuration
        {
            get { return _passwordExpiryDuration; }
            set { if (this._passwordExpiryDuration != value) { _passwordExpiryDuration = value; } }
        }

        private int _maxLoginAttempts;
        public int MaxLoginAttempts
        {
            get { return _maxLoginAttempts; }
            set { if (this._maxLoginAttempts != value) { _maxLoginAttempts = value; } }
        }

        private bool _passwordNeverExpires;
        public bool PasswordNeverExpires
        {
            get { return _passwordNeverExpires; }
            set { if (this._passwordNeverExpires != value) { _passwordNeverExpires = value; } }
        }

        private bool _passwordCannotChange;
        public bool PasswordCannotChange
        {
            get { return _passwordCannotChange; }
            set { if (this._passwordCannotChange != value) { _passwordCannotChange = value; } }
        }

        private bool _isPrevPasswordAllowed;
        public bool IsPrevPasswordAllowed
        {
            get { return _isPrevPasswordAllowed; }
            set { if (this._isPrevPasswordAllowed != value) { _isPrevPasswordAllowed = value; } }
        }

        private int _prevPasswordHistoryCount;
        public int PrevPasswordHistoryCount
        {
            get { return _prevPasswordHistoryCount; }
            set { if (this._prevPasswordHistoryCount != value) { _prevPasswordHistoryCount = value; } }
        }

        private bool _isUpperCase;
        public bool IsUpperCase
        {
            get { return _isUpperCase; }
            set { if (this._isUpperCase != value) { _isUpperCase = value; } }
        }

        private bool _isLowerCase;
        public bool IsLowerCase
        {
            get { return _isLowerCase; }
            set { if (this._isLowerCase != value) { _isLowerCase = value; } }
        }

        private bool _isNumber;
        public bool IsNumber
        {
            get { return _isNumber; }
            set { if (this._isNumber != value) { _isNumber = value; } }
        }

        private bool _isSpecialCaracter;
        public bool IsSpecialCaracter
        {
            get { return _isSpecialCaracter; }
            set { if (this._isSpecialCaracter != value) { _isSpecialCaracter = value; } }
        }

        private bool _IsPasswordChange;
        public bool IsPasswordChange
        {
            get { return _IsPasswordChange; }
            set { if (this._IsPasswordChange != value) { _IsPasswordChange = value; } }
        }

        private bool _IsDefaultPasswordForUi;
        public bool DefaultPasswordforUI
        {
            get { return _IsDefaultPasswordForUi; }
            set { if (this._IsDefaultPasswordForUi != value) { _IsDefaultPasswordForUi = value; } }
        }
        private int _OTPPasswordExpiryHrs;//Add Bharat: 16-Dec-2015
        public int OTPPasswordExpiryHrs
        {
            get { return _OTPPasswordExpiryHrs; }
            set { if (this._OTPPasswordExpiryHrs != value) { _OTPPasswordExpiryHrs = value; } }
        }

        private int _NoOfRequest;// Samreen: 7th-oct-2021
        public int NoOfRequest
        {
            get { return _NoOfRequest; }
            set { if (this._NoOfRequest != value) { _NoOfRequest = value; } }
        }

        private int _RequestValidityTime;// Samreen: 7th-oct-2021
        public int RequestValidityTime
        {
            get { return _RequestValidityTime; }
            set { if (this._RequestValidityTime != value) { _RequestValidityTime = value; } }
        }

        private int _NoOfOtpEmailRequests;// Samreen: 10th May 2022
        public int NoOfOtpEmailRequests
        {
            get { return _NoOfOtpEmailRequests; }
            set { if (this._NoOfOtpEmailRequests != value) { _NoOfOtpEmailRequests = value; } }
        }

        private int _RequestValidityTimeForOtp;// Samreen: 10th May 2022
        public int RequestValidityTimeForOtp
        {
            get { return _RequestValidityTimeForOtp; }
            set { if (this._RequestValidityTimeForOtp != value) { _RequestValidityTimeForOtp = value; } }
        }

        private string _LearnerId;
        public string LearnerId
        {
            get { return _LearnerId; }
            set { if (this._LearnerId != value) { _LearnerId = value; } }
        }

        private int _NoOfEmailRequest;// Samreen: 13th-oct-2021 -- This field is the current request of email sent to the users
        public int NoOfCurrentEmailRequest
        {
            get { return _NoOfEmailRequest; }
            set { if (this._NoOfEmailRequest != value) { _NoOfEmailRequest = value; } }
        }

        private string _EmailRequestID;// Samreen: 13th-oct-2021 -- This field is the current request of email sent to the users
        public string EmailRequestID
        {
            get { return _EmailRequestID; }
            set { if (this._EmailRequestID != value) { _EmailRequestID = value; } }
        }
    }
}