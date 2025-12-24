using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.Entity
{
    public class LearnerVM: BaseEntityVM
    {
       
        public bool? IsPPUser { get; set; }
        public string? PPCustomFields { get; set; }
        /// <summary>
        /// constant USER_SESSION_ID
        /// </summary>
        public const string USER_SESSION_ID = "user";

        private string? _strUserNameAlias;
        /// <summary>
        /// User Name Alias
        /// </summary>
        public string? UserNameAlias
        {
            get { return _strUserNameAlias; }
            set { if (this._strUserNameAlias != value) { _strUserNameAlias = value; } }
        }

        private string? _strUserPassword;
        /// <summary>
        /// User Password
        /// </summary>
        public string? UserPassword
        {
            get { return _strUserPassword; }
            set { if (this._strUserPassword != value) { _strUserPassword = value; } }
        }

        private string? _strFirstName;
        /// <summary>
        /// First Name
        /// </summary>
        public string? FirstName
        {
            get { return _strFirstName; }
            set { if (this._strFirstName != value) { _strFirstName = value; } }
        }

        private bool? _IsDoNotDeleteCustomeFiledValue;
        public bool? IsDoNotDeleteCustomeFiledValue
        {
            get { return _IsDoNotDeleteCustomeFiledValue; }
            set { if (this._IsDoNotDeleteCustomeFiledValue != value) { _IsDoNotDeleteCustomeFiledValue = value; } }
        }

        private string? _strpreferredDate;
        /// <summary>
        /// First Name
        /// </summary>
        public string? PreferredDateFormat
        {
            get { return _strpreferredDate; }
            set { if (this._strpreferredDate != value) { _strpreferredDate = value; } }
        }

        private string? _strPreferredTimeZone;
        /// <summary>
        /// First Name
        /// </summary>
        public string? PreferredTimeZone
        {
            get { return _strPreferredTimeZone; }
            set { if (this._strPreferredTimeZone != value) { _strPreferredTimeZone = value; } }
        }

        private string? _strMiddleName;
        /// <summary>
        /// Middle Name
        /// </summary>
        public string? MiddleName
        {
            get { return _strMiddleName; }
            set { if (this._strMiddleName != value) { _strMiddleName = value; } }
        }

        private string? _strLastName;
        /// <summary>
        /// Last Name
        /// </summary>
        public string? LastName
        {
            get { return _strLastName; }
            set { if (this._strLastName != value) { _strLastName = value; } }
        }

        private string? _strAddress;
        /// <summary>
        /// Address
        /// </summary>
        public string? Address
        {
            get { return _strAddress; }
            set { if (this._strAddress != value) { _strAddress = value; } }
        }

        private string? _strEmailID;
        /// <summary>
        /// EmailID
        /// </summary>
        public string? EmailID
        {
            get { return _strEmailID; }
            set { if (this._strEmailID != value) { _strEmailID = value; } }
        }

        private string? _strSystemUserGUID;
        /// <summary>
        /// SystemUserGUID
        /// </summary>
        public string? SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private string? _strParaRuleID;
        /// <summary>
        /// Para Rule Id
        /// </summary>
        public string? ParaRuleID
        {
            get { return _strParaRuleID; }
            set { if (this._strParaRuleID != value) { _strParaRuleID = value; } }
        }


        private string? _strParaActivityID;
        /// <summary>
        /// Para Activity Id
        /// </summary>
        public string? ParaActivityID
        {
            get { return _strParaActivityID; }
            set { if (this._strParaActivityID != value) { _strParaActivityID = value; } }
        }

        private DateTime? _dateOfBirth;
        /// <summary>
        /// DateOfBirth
        /// </summary>
        public DateTime? DateOfBirth
        {
            get { return _dateOfBirth; }
            set { if (this._dateOfBirth != value) { _dateOfBirth = value; } }
        }

        private DateTime? _dateOfRegistration;
        /// <summary>
        /// Date Of Registration
        /// </summary>
        public DateTime? DateOfRegistration
        {
            get { return _dateOfRegistration; }
            set { if (this._dateOfRegistration != value) { _dateOfRegistration = value; } }
        }

        private string? _strUserTypeId;
        /// <summary>
        /// User Type Id
        /// </summary>
        public string? UserTypeId
        {
            get { return _strUserTypeId; }
            set { if (this._strUserTypeId != value) { _strUserTypeId = value; } }
        }

        private string? _strDefaultLanguageId;
        /// <summary>
        /// Users Default Language Id
        /// </summary>
        public string? DefaultLanguageId
        {
            get { return _strDefaultLanguageId; }
            set { if (this._strDefaultLanguageId != value) { _strDefaultLanguageId = value; } }
        }

        private string? _strDefaultThemeID;
        /// <summary>
        /// Users Default Theme ID
        /// </summary>
        public string? DefaultThemeID
        {
            get { return _strDefaultThemeID; }
            set { if (this._strDefaultThemeID != value) { _strDefaultThemeID = value; } }
        }

        private bool? _bGender;
        /// <summary>
        /// Gender
        /// </summary>
        public bool? Gender
        {
            get { return _bGender; }
            set { if (this._bGender != value) { _bGender = value; } }
        }

        private bool? _bCanUpdate;
        /// <summary>
        /// Gender
        /// </summary>
        public bool? CanUpdate
        {
            get { return _bCanUpdate; }
            set { if (this._bCanUpdate != value) { _bCanUpdate = value; } }
        }

        private string? _strManagerId;
        /// <summary>
        /// Manager Id
        /// </summary>
        public string? ManagerId
        {
            get { return _strManagerId; }
            set { if (this._strManagerId != value) { _strManagerId = value; } }
        }

        private string? _strManagerEmailId;
        /// <summary>
        /// Manager Email Id
        /// </summary>
        public string? ManagerEmailId
        {
            get { return _strManagerEmailId; }
            set { if (this._strManagerEmailId != value) { _strManagerEmailId = value; } }
        }

        private string? _strManagerName;
        /// <summary>
        /// Manager Name dynamic column
        /// </summary>
        public string? ManagerName
        {
            get { return _strManagerName; }
            set { if (this._strManagerName != value) { _strManagerName = value; } }
        }


        private DateTime? _dateLastLogin;
        /// <summary>
        /// Date Last Login
        /// </summary>
        public DateTime? DateLastLogin
        {
            get { return _dateLastLogin; }
            set { if (this._dateLastLogin != value) { _dateLastLogin = value; } }
        }

        private bool? _bIsActive;
        /// <summary>
        /// To check Is Active
        /// </summary>
        public bool? IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
        }

        private string? _strUnitId;
        /// <summary>
        /// Unit Id
        /// </summary>
        public string? UnitId
        {
            get { return _strUnitId; }
            set { if (this._strUnitId != value) { _strUnitId = value; } }
        }

        private string? _strLevelId;
        /// <summary>
        /// Level Id
        /// </summary>
        public string? LevelId
        {
            get { return _strLevelId; }
            set { if (this._strLevelId != value) { _strLevelId = value; } }
        }

        private string? _strDefaultRegionView;
        /// <summary>
        /// DefaultRV
        /// </summary>
        public string? DefaultRegionView
        {
            get { return _strDefaultRegionView; }
            set { if (this._strDefaultRegionView != value) { _strDefaultRegionView = value; } }
        }

        private string? _strCurrentRegionView;
        /// <summary>
        /// CurrentRV   
        /// </summary>
        public string? CurrentRegionView
        {
            get { return _strCurrentRegionView; }
            set { if (this._strCurrentRegionView != value) { _strCurrentRegionView = value; } }
        }

        private Int32? _strILTRoleCount;
        /// <summary>
        /// ILTRoleCount   
        /// </summary>
        public Int32? ILTRoleCount
        {
            get { return _strILTRoleCount; }
            set { if (this._strILTRoleCount != value) { _strILTRoleCount = value; } }
        }

        private DateTime? _userExpiryDate;
        /// <summary>
        /// DateOfBirth
        /// </summary>
        public DateTime? userExpiryDate
        {
            get { return _userExpiryDate; }
            set { if (this._userExpiryDate != value) { _userExpiryDate = value; } }
        }

        public string? AuthenticationToken { get; set; }  //added for publishing portal

        /// <summary>
        /// Name of current region view if it exists, otherwise name of default region view
        /// </summary>
        public string? RegionViewName { get; set; }

        /// <summary>
        /// The AV path that pertains to this user.
        /// </summary>
        public string? AvPath { get; set; }

        private Nullable<DateTime> _dateTermination;
        /// <summary>
        /// Date of Termination
        /// </summary>
        public Nullable<DateTime> DateOfTermination
        {
            get { return _dateTermination; }
            set { if (this._dateTermination != value) { _dateTermination = value; } }
        }
        private List<UserAdminRole>? _entListUserAdminRole;
        /// <summary>
        /// User Admin Role
        /// </summary>
        public List<UserAdminRole>? UserAdminRole
        {
            get { return _entListUserAdminRole; }
        }


        private List<UserCustomFieldValue>? _entListUserCustomFieldValue;
        /// <summary>
        /// User Custom Field Value
        /// </summary>
        public List<UserCustomFieldValue>? UserCustomFieldValue
        {
            get { return _entListUserCustomFieldValue; }
        }

        private bool? _bIsFirstLogin;
        /// <summary>
        /// To check Is First Login
        /// </summary>
        public bool? IsFirstLogin
        {
            get { return _bIsFirstLogin; }
            set { if (this._bIsFirstLogin != value) { _bIsFirstLogin = value; } }
        }

        private bool? _bIsInScope;
        /// <summary>
        /// To check Is in scope
        /// </summary>
        public bool? IsInScope
        {
            get { return _bIsInScope; }
            set { if (this._bIsInScope != value) { _bIsInScope = value; } }
        }

        private string? _strPhoneNo;
        /// <summary>
        /// Phone No   
        /// </summary>
        public string? PhoneNo
        {
            get { return _strPhoneNo; }
            set { if (this._strPhoneNo != value) { _strPhoneNo = value; } }
        }


        private bool? _bIsPasswordExpired;
        /// <summary>
        /// To check Is Active
        /// </summary>
        public bool? IsPasswordExpired
        {
            get { return _bIsPasswordExpired; }
            set { if (this._bIsPasswordExpired != value) { _bIsPasswordExpired = value; } }
        }
        private bool? _bIsUserLock; //Added by bharat: 16-Dec-2015
        /// <summary>
        /// To check Is Active
        /// </summary>
        public bool? IsUserLock
        {
            get { return _bIsUserLock; }
            set { if (this._bIsUserLock != value) { _bIsUserLock = value; } }
        }
        private string? _strUserScope;
        /// <summary>
        /// User Scope  
        /// </summary>
        public string? UserScope
        {
            get { return _strUserScope; }
            set { if (this._strUserScope != value) { _strUserScope = value; } }
        }

        private string? _strUserDefaultOrg;
        /// <summary>
        /// User Default Org
        /// </summary>
        public string? UserDefaultOrg
        {
            get { return _strUserDefaultOrg; }
            set { if (this._strUserDefaultOrg != value) { _strUserDefaultOrg = value; } }
        }

        private string? _strNMLSID;
        /// <summary>
        /// NMUID
        /// </summary>
        public string? NMLSID
        {
            get { return _strNMLSID; }
            set { if (this._strNMLSID != value) { _strNMLSID = value; } }
        }

        //private string _strVirtualTrainingId;
        ///// <summary>
        ///// TrainingId
        ///// </summary>
        //public string TrainingId
        //{
        //    get { return _strVirtualTrainingId; }
        //    set { if (this._strVirtualTrainingId != value) { _strVirtualTrainingId = value; } }
        //}

        private string? _strActivityId;
        /// <summary>
        /// TrainingId
        /// </summary>
        public string? ActivityId
        {
            get { return _strActivityId; }
            set { if (this._strActivityId != value) { _strActivityId = value; } }
        }

        private string? _strIsReassign;
        /// <summary>
        /// TrainingId
        /// </summary>
        public string? IsReassign
        {
            get { return _strIsReassign; }
            set { if (this._strIsReassign != value) { _strIsReassign = value; } }
        }
        ///// <summary>
        ///// 
        ///// </summary>
        //private SqlConnection _sImportConn;
        //public SqlConnection ImportConnection
        //{
        //    get { return _sImportConn; }
        //    set { if (this._sImportConn != value) { _sImportConn = value; } }
        //}

        private string? _OTPNumber;
        /// <summary>
        /// OTP Number
        /// </summary>
        public string? OTPNumber
        {
            get { return _OTPNumber; }
            set { if (this._OTPNumber != value) { _OTPNumber = value; } }
        }

        /* Change this later - Baji */

        private string? _strRegistrationStatus;
        public string? RegistrationStatus
        {
            get { return _strRegistrationStatus; }
            set { if (this._strRegistrationStatus != value) { _strRegistrationStatus = value; } }
        }

        private string? _strIdProofDocument;
        public string? IdProofDocument
        {
            get { return _strIdProofDocument; }
            set { if (this._strIdProofDocument != value) { _strIdProofDocument = value; } }
        }
        private string? _strRejectComments;
        public string? RejectComments
        {
            get { return _strRejectComments; }
            set { if (this._strRejectComments != value) { _strRejectComments = value; } }
        }

        private bool? _bIsSignUpUser;
        public bool? IsSignUpUser
        {
            get { return _bIsSignUpUser; }
            set { if (this._bIsSignUpUser != value) { _bIsSignUpUser = value; } }
        }

        private bool? _bIsSubscribeForNewsLetter;
        public bool? IsSubscribeForNewsLetter
        {
            get { return _bIsSubscribeForNewsLetter; }
            set { if (this._bIsSubscribeForNewsLetter != value) { _bIsSubscribeForNewsLetter = value; } }
        }


        private bool? _bIsTermsandConditionAccepted;
        public bool? IsTermsandConditionAccepted
        {
            get { return _bIsTermsandConditionAccepted; }
            set { if (this._bIsTermsandConditionAccepted != value) { _bIsTermsandConditionAccepted = value; } }
        }
        //private string _strIdProofDocument;
        //public string IdProofDocument
        //{
        //    get { return _strIdProofDocument; }
        //    set { if (this._strIdProofDocument != value) { _strIdProofDocument = value; } }
        //} 

        private bool? _IsTermsAndCondAccepted;
        public bool? IsTermsAndCondAccepted
        {
            get { return _IsTermsAndCondAccepted; }
            set { if (this._IsTermsAndCondAccepted != value) { _IsTermsAndCondAccepted = value; } }
        }

        private bool? _IsSendEmail;
        public bool? IsSendEmail
        {
            get { return _IsSendEmail; }
            set { if (this._IsSendEmail != value) { _IsSendEmail = value; } }
        }
        private bool? _IsAutoEmail;
        public bool? IsAutoEmail
        {
            get { return _IsAutoEmail; }
            set { if (this._IsAutoEmail != value) { _IsAutoEmail = value; } }
        }

        private bool? _IsDirectSendMail;
        public bool? IsDirectSendMail
        {
            get { return _IsDirectSendMail; }
            set { if (this._IsDirectSendMail != value) { _IsDirectSendMail = value; } }
        }

        private bool? _flagAddUserPage = false;
        /// <summary>
        /// Allow Blank
        /// </summary>
        public bool? FlagAddUserPage
        {
            get { return _flagAddUserPage; }
            set { if (this._flagAddUserPage != value) { _flagAddUserPage = value; } }
        }      
        
    }
}
