using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// Learner Class inherited from BaseEntity
    /// </summary>
    [Serializable]
    public class UserExpiry: BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public UserExpiry()
        {            
            
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
            AddExpiryType,
            AddUserExpriyBusinessRuleAndGlobalSettings,
            UpdateUserExpriyBusinessRuleAndGlobalSettings,
            AddEditUserExpiryWhileEditingRule,
            InsertUserExpiryWhilAddEditUser,
            UpdateUserExpiryWhilAddEditUser
        }
        /// <summary>
        /// List method ENUM
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            DeleteAll, 
            DeleteAllBusinessRule,
            BulkAdd,           
            GetUserExpirySettings,
            GetAllUserExpiryByExpiryType,
            GetUserListNotAssignedSpecificUserExpiry,
            GetBusinessRule,
           GetUSERLISTOFBUSINESSRULE
        }

        public enum UserExpiryType
        {
            SpecificUser=0,
            BusinessRule=1,
            GlobalSetting=2,
            None = 3
        }


        private string _strBusinessRuleId;
        /// <summary>
        /// Business Rule Id
        /// </summary>
        public string BusinessRuleId
        {
            get { return _strBusinessRuleId; }
            set { if (this._strBusinessRuleId != value) { _strBusinessRuleId = value; } }
        }

        private DateTime? _ExpiryDate;
        /// <summary>
        /// Expiry Date
        /// </summary>
        public DateTime?  ExpiryDate
        {
            get { return _ExpiryDate; }
            set { if (this._ExpiryDate != value) { _ExpiryDate = value; } }
        }

        private int _iDays;
        /// <summary>
        ///Days
        /// </summary>
        public int  Days
        {
            get { return _iDays; }
            set { if (this._iDays != value) { _iDays = value; } }
        }

        private bool _IsIncludeAdminUser;
        public bool IsIncludeAdminUser
        {
            get { return _IsIncludeAdminUser; }
            set { if (this._IsIncludeAdminUser != value) { _IsIncludeAdminUser = value; } }
        }


        private bool _IsBusinessOrGlobalSettings;
        public bool IsBusinessOrGlobalSettings
        {
            get { return _IsBusinessOrGlobalSettings; }
            set { if (this._IsBusinessOrGlobalSettings != value) { _IsBusinessOrGlobalSettings = value; } }
        }


        private string _strSystemUserGUID;
        /// <summary>
        /// System User GUID
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private UserExpiryType _strExpiryType;
        /// <summary>
        ///Expiry Type
        /// </summary>
        public UserExpiryType ExpiryType
        {
            get { return _strExpiryType; }
            set { if (this._strExpiryType != value) { _strExpiryType = value; } }
        }

        private bool _IsSpecificUser;
        public bool IsSpecificUser
        {
            get { return _IsSpecificUser; }
            set { if (this._IsSpecificUser != value) { _IsSpecificUser = value; } }
        }

        private bool _IsBusinessRule;
        public bool IsBusinessRule
        {
            get { return _IsBusinessRule; }
            set { if (this._IsBusinessRule != value) { _IsBusinessRule = value; } }
        }

        private bool _IsGlobalSettings;
        public bool IsGlobalSettings
        {
            get { return _IsGlobalSettings; }
            set { if (this._IsGlobalSettings != value) { _IsGlobalSettings = value; } }
        }

        private string _strUserNameAliase;
        /// <summary>
        /// System User GUID
        /// </summary>
        public string UserNameAliase
        {
            get { return _strUserNameAliase; }
            set { if (this._strUserNameAliase != value) { _strUserNameAliase = value; } }
        }

        private string _strFirstName;
        /// <summary>
        /// System User GUID
        /// </summary>
        public string FirstName
        {
            get { return _strFirstName; }
            set { if (this._strFirstName != value) { _strFirstName = value; } }
        }

        private string _strLastName;
        /// <summary>
        /// System User GUID
        /// </summary>
        public string LastName
        {
            get { return _strLastName; }
            set { if (this._strLastName != value) { _strLastName = value; } }
        }

        private string _strKeyword;
        /// <summary>
        /// System User GUID
        /// </summary>
        public string Keyword
        {
            get { return _strKeyword; }
            set { if (this._strKeyword != value) { _strKeyword = value; } }
        }

        private string _strRuleName;
        /// <summary>
        /// System User GUID
        /// </summary>
        public string RuleName
        {
            get { return _strRuleName; }
            set { if (this._strRuleName != value) { _strRuleName = value; } }
        }
    }
}