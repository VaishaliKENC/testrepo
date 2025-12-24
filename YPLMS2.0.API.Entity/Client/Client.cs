/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh & Ashish
* Created:<13/07/09>
* Last Modified:<25/12/09>
*/
using System;
using System.Collections.Generic;
using System.Xml;
using System.Data.SqlClient;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// Serializable class Client : BaseEntity    
    /// </summary>
    [Serializable]
    public class Client : BaseEntity
    {
        /// <summary>
        /// const CLIENT_SESSION_ID
        /// </summary>
        public const string CLIENT_SESSION_ID = "client";

        /// <summary>
        /// const string Base client id in webconfig
        /// </summary>
        public const string BASE_CLIENT_KEY = "BaseClientId";

        /// <summary>
        /// const string Base client id 
        /// </summary>
        public const string BASE_CLIENT_ID = "1";
        /// <summary>
        /// Method enum
        /// </summary>
        public new enum Method
        {
            Get,
            GetFeedBackEmail,
            GetAllowUser,
            GetClientId,
            GetClientByID,
            CheckClientByURL,
            CheckClientByName,
            Add,
            Update,
            UpdateAllowUser,
            UpdateFeedbackReceiverEmailId,
            Delete,
            SetSessionTimeOut,
            UpdateSystemLockUnLock,
            SetDefaultTheme,
            ManageForgotPasswordLink,
            SetLogoutRedirectionURL,
            SetMaxUploadFileSize,
            SetSelfRegistrationType,
            SetSSOType,
            SetDefaultLogo,
            ActivateDeactivateClient,
            SetIsRSSEnabled,
            UpdateIsAnnouncementsEnabled,
            SetDefaultPageSize,
            SetImportConnection,
            GetHttpsAllowed,
            UpdateHttpsAllowed,
            SetNonRestrictedDomain,
            SaveDisplayPhotoSettings,
            SaveAllowUploadPhotoSettings,
            GetDisplayPhotoSettings,
            GetAllowuploadPhotoSettings,
            GetClientAccessURL,
            GetClientDetaildFromCourseId,

            SetAuditTrailPeriod,
            GetAuditTrailPeriod,
            GetJIRAHelpDesk,
            GetDocLicensePath
        }
        /// <summary>
        /// List method enum
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            DeleteAll,
            ActivateDeactivateAll,
            GetAllVirtualTrainingClient
        }

        /// <summary>
        /// Default Contructor
        /// </summary>
        public Client()
        {
            _entListCustomFields = new List<CustomField>();
        }

        private List<CustomField> _entListCustomFields;
        /// <summary>
        /// List of CustomFields
        /// </summary>
        public List<CustomField> CustomFields
        {
            get { return _entListCustomFields; }
        }

        private SystemConfiguration _entSysConfiguration;
        /// <summary>
        /// SysConfiguration object 
        /// </summary>
        public SystemConfiguration SysConfiguration
        {
            get { return _entSysConfiguration; }
            set { if (this._entSysConfiguration != value) { _entSysConfiguration = value; } }
        }

        private Cluster _entClientCluster;
        /// <summary>
        /// SysConfiguration object 
        /// </summary>
        public Cluster ClientCluster
        {
            get { return _entClientCluster; }
            set { if (this._entClientCluster != value) { _entClientCluster = value; } }
        }

        private Language _entLanguage;
        /// <summary>
        /// To Get Client Language details
        /// </summary>
        public Language Language
        {
            get { return _entLanguage; }
            set { if (this._entLanguage != value) { _entLanguage = value; } }
        }

        private Layout _entLayout;
        /// <summary>
        /// To Get Client Layout
        /// </summary>
        public Layout Layout
        {
            get { return _entLayout; }
            set { if (this._entLayout != value) { _entLayout = value; } }
        }

        private Theme _entTheme;
        /// <summary>
        /// To Get Default Theme object details
        /// </summary>
        public Theme Theme
        {
            get { return _entTheme; }
            set { if (this._entTheme != value) { _entTheme = value; } }
        }

        private CourseConfiguration _entCourseConfiguration;
        /// <summary>
        /// Client Course Configuration
        /// </summary>
        public CourseConfiguration CourseConfiguration
        {
            get { return _entCourseConfiguration; }
            set { if (this._entCourseConfiguration != value) { _entCourseConfiguration = value; } }
        }

        private PasswordPolicyConfiguration _entPasswordPolicyConfiguration;
        /// <summary>
        ///Client Password Policy Configuration
        /// </summary>
        public PasswordPolicyConfiguration PasswordPolicyConfiguration
        {
            get { return _entPasswordPolicyConfiguration; }
            set { if (this._entPasswordPolicyConfiguration != value) { _entPasswordPolicyConfiguration = value; } }
        }

        private RSSFeedConfiguration _entRSSFeedConfiguration;
        /// <summary>
        ///Client RSS Feed Configuration
        /// </summary>
        public RSSFeedConfiguration RSSFeedConfiguration
        {
            get { return _entRSSFeedConfiguration; }
            set { if (this._entRSSFeedConfiguration != value) { _entRSSFeedConfiguration = value; } }
        }

        private LearnerProfileAccessConfiguration _entLearnerProfileAccessConfiguration;
        /// <summary>
        /// Learner Profile Access Configuration
        /// </summary>
        public LearnerProfileAccessConfiguration LearnerProfileAccessConfiguration
        {
            get { return _entLearnerProfileAccessConfiguration; }
            set { if (this._entLearnerProfileAccessConfiguration != value) { _entLearnerProfileAccessConfiguration = value; } }
        }


        private string _strClientName;
        /// <summary>
        /// Client Name 
        /// </summary>
        public string ClientName
        {
            get { return _strClientName; }
            set { if (this._strClientName != value) { _strClientName = value; } }
        }

        private string _strClientDescription;
        /// <summary>
        /// Client Description
        /// </summary>
        public string ClientDescription
        {
            get { return _strClientDescription; }
            set { if (this._strClientDescription != value) { _strClientDescription = value; } }
        }

        private string _strClientAccessURL;
        /// <summary>
        /// Client Access URL
        /// </summary>
        public string ClientAccessURL
        {
            get { return _strClientAccessURL; }
            set { if (this._strClientAccessURL != value) { _strClientAccessURL = value; } }
        }

        private string _strDatabaseName;
        /// <summary>
        /// Database Name
        /// </summary>
        public string DatabaseName
        {
            get { return _strDatabaseName; }
            set { if (this._strDatabaseName != value) { _strDatabaseName = value; } }
        }

        private string _strDBIPAddress;
        /// <summary>
        /// DBIP Address
        /// </summary>
        public string DBIPAddress
        {
            get { return _strDBIPAddress; }
            set { if (this._strDBIPAddress != value) { _strDBIPAddress = value; } }
        }

        private string _strDBUID;
        /// <summary>
        /// DBUID
        /// </summary>
        public string DBUID
        {
            get { return _strDBUID; }
            set { if (this._strDBUID != value) { _strDBUID = value; } }
        }

        private string _strDBPassword;
        /// <summary>
        /// DB Password
        /// </summary>
        public string DBPassword
        {
            get { return _strDBPassword; }
            set { if (this._strDBPassword != value) { _strDBPassword = value; } }
        }

        private string _strDefaultLanguageId;
        /// <summary>
        /// Clients Default Language Id
        /// </summary>
        public string DefaultLanguageId
        {
            get { return _strDefaultLanguageId; }
            set { if (this._strDefaultLanguageId != value) { _strDefaultLanguageId = value; } }
        }

        private string _strDefaultLayoutId;
        /// <summary>
        /// Clients Default Layout Id
        /// </summary>
        public string DefaultLayoutId
        {
            get { return _strDefaultLayoutId; }
            set { if (this._strDefaultLayoutId != value) { _strDefaultLayoutId = value; } }
        }

        private string _strYPLSLayoutId;
        /// <summary>
        /// Clients YPLS Layout Id
        /// </summary>
        public string YPLSLayoutId
        {
            get { return _strYPLSLayoutId; }
            set { if (this._strYPLSLayoutId != value) { _strYPLSLayoutId = value; } }
        }

        private string _strYPLSThemeId;
        /// <summary>
        /// Clients Default Theme Id
        /// </summary>
        public string YPLSThemeId
        {
            get { return _strYPLSThemeId; }
            set { if (this._strYPLSThemeId != value) { _strYPLSThemeId = value; } }
        }


        private string _strDefaultThemeId;
        /// <summary>
        /// Clients Default Theme Id
        /// </summary>
        public string DefaultThemeId
        {
            get { return _strDefaultThemeId; }
            set { if (this._strDefaultThemeId != value) { _strDefaultThemeId = value; } }
        }

        private string _strContentServerURL;
        /// <summary>
        /// Content Server URL
        /// </summary>
        public string ContentServerURL
        {
            get { return _strContentServerURL; }
            set { if (this._strContentServerURL != value) { _strContentServerURL = value; } }
        }

        private int _iNumberOfUserLicenses = 0;
        /// <summary>
        /// Number Of User Licenses
        /// </summary>
        public int NumberOfUserLicenses
        {
            get { return _iNumberOfUserLicenses; }
            set { if (this._iNumberOfUserLicenses != value) { _iNumberOfUserLicenses = value; } }
        }

        private XmlDocument _xmlOrgTreeXML;
        /// <summary>
        /// XmlDocument OrganizationTreeXML
        /// </summary>
        public XmlDocument OrganizationTreeXML
        {
            get { return _xmlOrgTreeXML; }
            set { if (this._xmlOrgTreeXML != value) { _xmlOrgTreeXML = value; } }
        }

        private bool _bIsActive;
        /// <summary>
        /// To check Is Active
        /// </summary>
        public bool IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
        }

        /// <summary>
        /// Default value 60
        /// </summary>
        private int _iSessionTimeOut = 60;
        /// <summary>
        /// Session Time out Value 
        /// </summary>
        public int SessionTimeOut
        {
            get { return _iSessionTimeOut; }
            set { if (this._iSessionTimeOut != value) { _iSessionTimeOut = value; } }
        }

        private bool _bIsSelfRegistration = true;
        /// <summary>
        /// To check Is SelfRegistration
        /// </summary>
        public bool IsSelfRegistration
        {
            get { return _bIsSelfRegistration; }
            set { if (this._bIsSelfRegistration != value) { _bIsSelfRegistration = value; } }
        }

        private bool _bIsPassCodeBased = true;
        /// <summary>
        /// To check Is PassCodeBased
        /// </summary>
        public bool IsPassCodeBased
        {
            get { return _bIsPassCodeBased; }
            set { if (this._bIsPassCodeBased != value) { _bIsPassCodeBased = value; } }
        }

        private bool _bIsEmailDomainBased = true;
        /// <summary>
        /// To check Is EmailDomainBased
        /// </summary>
        public bool IsEmailDomainBased
        {
            get { return _bIsEmailDomainBased; }
            set { if (this._bIsEmailDomainBased != value) { _bIsEmailDomainBased = value; } }
        }

        private bool _bIsSSOConfigured = false;
        /// <summary>
        /// To check Is SSOConfigured
        /// </summary>
        public bool IsSSOConfigured
        {
            get { return _bIsSSOConfigured; }
            set { if (this._bIsSSOConfigured != value) { _bIsSSOConfigured = value; } }
        }

        private SingleSignOnType _ssoType;
        /// <summary>
        /// SSO Type Is
        /// </summary>
        public SingleSignOnType SSOType
        {
            get { return _ssoType; }
            set { if (this._ssoType != value) { _ssoType = value; } }
        }

        private string _strLogoutRedirectionURL;
        /// <summary>
        /// Logout Redirection URL
        /// </summary>
        public string LogoutRedirectionURL
        {
            get { return _strLogoutRedirectionURL; }
            set { if (this._strLogoutRedirectionURL != value) { _strLogoutRedirectionURL = value; } }
        }

        private bool _bIsLocked = false;
        /// <summary>
        /// To check Is IsLocked
        /// </summary>
        public bool IsLocked
        {
            get { return _bIsLocked; }
            set { if (this._bIsLocked != value) { _bIsLocked = value; } }
        }

        private bool _bIsForgotPasswordEnabled = true;
        /// <summary>
        /// To check Is IsForgotPasswordEnabled
        /// </summary>
        public bool IsForgotPasswordEnabled
        {
            get { return _bIsForgotPasswordEnabled; }
            set { if (this._bIsForgotPasswordEnabled != value) { _bIsForgotPasswordEnabled = value; } }
        }

        private bool _bIsContactUsEnabled = true;
        /// <summary>
        /// To check Is IsForgotPasswordEnabled
        /// </summary>
        public bool IsContactUsEnabled
        {
            get { return _bIsContactUsEnabled; }
            set { if (this._bIsContactUsEnabled != value) { _bIsContactUsEnabled = value; } }
        }

        private bool _bIsFeedbackEnabled = true;
        /// <summary>
        /// To check Is IsForgotPasswordEnabled
        /// </summary>
        public bool IsFeedbackEnabled
        {
            get { return _bIsFeedbackEnabled; }
            set { if (this._bIsFeedbackEnabled != value) { _bIsFeedbackEnabled = value; } }
        }

        private bool _bIsRSSEnabled = true;
        /// <summary>
        /// To check Is IsForgotPasswordEnabled
        /// </summary>
        public bool IsRSSEnabled
        {
            get { return _bIsRSSEnabled; }
            set { if (this._bIsRSSEnabled != value) { _bIsRSSEnabled = value; } }
        }

        private bool _bIsAnnouncementsEnabled = true;
        /// <summary>
        /// To check Is IsIsAnnouncementsEnabled
        /// </summary>
        public bool IsAnnouncementsEnabled
        {
            get { return _bIsAnnouncementsEnabled; }
            set { if (this._bIsAnnouncementsEnabled != value) { _bIsAnnouncementsEnabled = value; } }
        }
        /// <summary>
        /// Default value added.
        /// </summary>
        private int _iMaxFileUploadSizeMB = 20;
        /// <summary>
        /// To check Is MaxFileUploadSizeMB
        /// </summary>
        public int MaxFileUploadSizeMB
        {
            get { return _iMaxFileUploadSizeMB; }
            set { if (this._iMaxFileUploadSizeMB != value) { _iMaxFileUploadSizeMB = value; } }
        }

        private DateTime _dateContractEnd;
        /// <summary>
        /// Contract End Date
        /// </summary>
        public DateTime ContractEndDate
        {
            get { return _dateContractEnd; }
            set { if (this._dateContractEnd != value) { _dateContractEnd = value; } }
        }

        private DateTime _dateContractStart;
        /// <summary>
        /// Contract Start Date
        /// </summary>
        public DateTime ContractStartDate
        {
            get { return _dateContractStart; }
            set { if (this._dateContractStart != value) { _dateContractStart = value; } }
        }


        private Nullable<int> _iMaxConcurrentSessions;
        /// <summary>
        /// Maximum Concurrent Sessions
        /// </summary>
        public Nullable<int> MaxConcurrentSessions
        {
            get { return _iMaxConcurrentSessions; }
            set { if (this._iMaxConcurrentSessions != value) { _iMaxConcurrentSessions = value; } }
        }

        private string _strSMTPServerIP;
        /// <summary>
        /// SMTP Server IP
        /// </summary>
        public string SMTPServerIP
        {
            get { return _strSMTPServerIP; }
            set { if (this._strSMTPServerIP != value) { _strSMTPServerIP = value; } }
        }

        private string _strSMTPUserName;
        /// <summary>
        /// SMTP User Name
        /// </summary>
        public string SMTPUserName
        {
            get { return _strSMTPUserName; }
            set { if (this._strSMTPUserName != value) { _strSMTPUserName = value; } }
        }

        private string _strSMTPPassword;
        /// <summary>
        /// SMTP Password
        /// </summary>
        public string SMTPPassword
        {
            get { return _strSMTPPassword; }
            set { if (this._strSMTPPassword != value) { _strSMTPPassword = value; } }
        }

        private string _strSiteSubDomainName;
        /// <summary>
        /// SMTP Sub Domain Name
        /// </summary>
        public string SiteSubDomainName
        {
            get { return _strSiteSubDomainName; }
            set { if (this._strSiteSubDomainName != value) { _strSiteSubDomainName = value; } }
        }

        private string _strSAIConsultingServiceMgr;
        /// <summary>
        /// SAI Consulting Service Manager 
        /// </summary>
        public string SAIConsultingServiceManager
        {
            get { return _strSAIConsultingServiceMgr; }
            set { if (this._strSAIConsultingServiceMgr != value) { _strSAIConsultingServiceMgr = value; } }
        }

        private string _strAdminName;
        /// <summary>
        /// For Client Contract Report data
        /// </summary>
        public string AdminName
        {
            get { return _strAdminName; }
            set { if (this._strAdminName != value) { _strAdminName = value; } }
        }

        private string _strAdminEmailId;
        /// <summary>
        /// For Client Contract Report data
        /// </summary>
        public string AdminEmailId
        {
            get { return _strAdminEmailId; }
            set { if (this._strAdminEmailId != value) { _strAdminEmailId = value; } }
        }

        private string _strAdminNameSearch;
        /// <summary>
        /// For Client Contract Report data
        /// </summary>
        public string AdminNameSearch
        {
            get { return _strAdminNameSearch; }
            set { if (this._strAdminNameSearch != value) { _strAdminNameSearch = value; } }
        }

        private Int64 _iTotalUsers;
        /// <summary>
        /// For Client - Total Users
        /// </summary>
        public Int64 TotalUsers
        {
            get { return _iTotalUsers; }
            set { if (this._iTotalUsers != value) { _iTotalUsers = value; } }
        }

        private Int64 _iTotalAllocation;
        /// <summary>
        /// For Client - Total Users
        /// </summary>
        public Int64 TotalAllocation
        {
            get { return _iTotalAllocation; }
            set { if (this._iTotalAllocation != value) { _iTotalAllocation = value; } }
        }

        private bool _bAllowUserProfileEdit;
        /// <summary>
        /// Allow User Profile Edit
        /// </summary>
        public bool AllowUserProfileEdit
        {
            get { return _bAllowUserProfileEdit; }
            set { if (this._bAllowUserProfileEdit != value) { _bAllowUserProfileEdit = value; } }
        }

        private string _strFeedbackReceiverEmailId;
        /// <summary>
        /// Feed back Receiver EmailId
        /// </summary>
        public string FeedbackReceiverEmailId
        {
            get { return _strFeedbackReceiverEmailId; }
            set { if (this._strFeedbackReceiverEmailId != value) { _strFeedbackReceiverEmailId = value; } }
        }

        private bool _bIsClientContractStarted;
        /// <summary>
        /// IsClientContractExpired     
        /// </summary>
        public bool IsClientContractStarted
        {
            get { return _bIsClientContractStarted; }
            set { if (this._bIsClientContractStarted != value) { _bIsClientContractStarted = value; } }
        }

        private int _iPageSize = 5;
        /// <summary>
        /// PageSize
        /// </summary>
        public int PageSize
        {
            get { return _iPageSize; }
            set { if (this._iPageSize != value) { _iPageSize = value; } }
        }

        private bool _bIsClientContractExpired;
        /// <summary>
        /// IsClientContractExpired   
        /// </summary>
        public bool IsClientContractExpired
        {
            get { return _bIsClientContractExpired; }
            set { if (this._bIsClientContractExpired != value) { _bIsClientContractExpired = value; } }
        }



        private bool _bIsHTTPSAllowed;
        /// <summary>
        /// IsClientContractExpired   
        /// </summary>
        public bool IsHTTPSAllowed
        {
            get { return _bIsHTTPSAllowed; }
            set { if (this._bIsHTTPSAllowed != value) { _bIsHTTPSAllowed = value; } }
        }


        private string _strLUClientId;
        /// <summary>
        /// Mapped LU ClientId           
        /// </summary>
        public string LUClientId
        {
            get { return _strLUClientId; }
            set { if (this._strLUClientId != value) { _strLUClientId = value; } }
        }

        /// <summary>
        /// Import Connection
        /// </summary>
        private SqlConnection _sImportConn;
        public SqlConnection ImportConnection
        {
            get { return _sImportConn; }
            set { if (this._sImportConn != value) { _sImportConn = value; } }
        }

        private string _strLUClientName;
        /// <summary>
        /// Mapped LU ClientName           
        /// </summary>
        public string LUClientName
        {
            get { return _strLUClientName; }
            set { if (this._strLUClientName != value) { _strLUClientName = value; } }
        }


        private bool _bIsCertifcationEnabled;
        /// <summary>
        /// IsCertifcationEnabled     
        /// </summary>
        public bool IsCertifcationEnabled
        {
            get { return _bIsCertifcationEnabled; }
            set { if (this._bIsCertifcationEnabled != value) { _bIsCertifcationEnabled = value; } }
        }

        private bool _isSystemMaintMessageEnabled;
        public bool IsSystemMaintMessageEnabled
        {
            get
            {
                return _isSystemMaintMessageEnabled;
            }
            set
            {
                if (_isSystemMaintMessageEnabled != value)
                {
                    _isSystemMaintMessageEnabled = value;
                }
            }
        }

        public DateTime? SystemMaintenanceStart
        {
            get;
            set;
        }

        public DateTime? SystemMaintenanceEnd
        {
            get;
            set;
        }

        public bool Validate(bool pIsUpdate)
        {
            if (pIsUpdate)
            {
                if (String.IsNullOrEmpty(ID))
                    return false;
            }
            else
            {
                if (String.IsNullOrEmpty(ClientName))
                    return false;
            }
            return true;
        }

        public enum SingleSignOnType
        {
            None,
            LoginOnly,
            FullProfile,
            Both
        }

        private string _strDBNamePrefix;
        /// <summary>
        /// Mapped LU ClientId           
        /// </summary>
        public string strDBNamePrefix
        {
            get { return _strDBNamePrefix; }
            set { if (this._strDBNamePrefix != value) { _strDBNamePrefix = value; } }
        }
        private int _iSMTPPORT;
        /// <summary>
        /// SMTP Server IP
        /// </summary>
        public int SMTPPORT
        {
            get { return _iSMTPPORT; }
            set { if (this._iSMTPPORT != value) { _iSMTPPORT = value; } }
        }

        private bool _bSMTPEnableSSL;
        /// <summary>
        /// SMTP Server IP
        /// </summary>
        public bool SMTPEnableSSL
        {
            get { return _bSMTPEnableSSL; }
            set { if (this._bSMTPEnableSSL != value) { _bSMTPEnableSSL = value; } }
        }

        private string _securityProtocol;
        /// <summary>
        /// Security Protocol        
        /// </summary>
        public string SecurityProtocol
        {
            get { return _securityProtocol; }
            set { if (this._securityProtocol != value) { _securityProtocol = value; } }
        }
        private bool _bIsSecured;
        /// <summary>
        /// SMTP Server IP
        /// </summary>
        public bool IsSecured
        {
            get { return _bIsSecured; }
            set { if (this._bIsSecured != value) { _bIsSecured = value; } }
        }
    }
}