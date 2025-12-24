namespace YPLMS2._0.API.YPLMS.Services.Messages
{
    public static class iPerformMessages
    {
        public const string UNAUTHORISED_ACCESS = "mUnauthorisediPerformAccess";
        public const string iPerform_USER_SERVICE_ERROR = "miPerformUserServiceError";
        public const string iPerform_SERVICE_ERROR = "miPerformServiceError";
        public const string iPerform_APPLICATION_ERROR = "miPerformApplicationError";
    }

    public static class SamlSsoConfiguration
    {
        public const string DATAACCESS_ERROR = "mSamlSsoDataAccessError";
    }

    public static class SignUp
    {
        public const string SIGNUP_ERROR = "mSignUpError";
        public const string User_Register_Success = "UserRegisterSuccess";
    }

    public class RapidelUserService
    {
        public const string UNAUTHORISED_ACCESS = "mUnauthorisedRapidelAccess";
        public const string RAPIDEL_USER_SERVICE_ERROR = "mRapidelUserServiceError";
        public const string YP_SERVICE_ERROR = "mYPServiceError";
    }

    public class JIRAUserService
    {
        public const string JIRA_USER_SERVICE_ERROR = "mJIRAUserServiceError";
    }

    //Samreen 23Nov 2016
    public class PeruseUserService
    {
        public const string UNAUTHORISED_ACCESS = "mUnauthorisedPeruseAccess";
        public const string PERUSE_USER_SERVICE_ERROR = "mPeruseUserServiceError";
    }

    //added by sujata ...16/10/2015
    public class PublishingPortalService
    {
        public const string UNAUTHORISED_ACCESS = "mUnauthorisedPublishingPortalAccess";
        public const string PUBLISHING_PORTAL_USER_SERVICE_ERROR = "mPublidhingPortalServiceError";
    }

    public class iPerformService
    {
        public const string UNAUTHORISED_ACCESS = "mUnauthorisediPerformAccess";
        public const string IPERFORM_USER_SERVICE_ERROR = "miPerformServiceError";
    }

    public class User
    {
        public const string ENTER_USERID = "mEnterUserId";
        public const string ENTER_USER_PASS = "mEnterUserPassword";
        public const string ENTER_CONFIRM_PASSWORD_FAIL = "mConfirmPasswordFail";
        //The specified username and password does not exists. Please type the correct username and password
        public const string INVALID_USERNAME_PASS = "InvalidUserPass";
        public const string ENTER_LOGIN_ID_PASSWORD = "mEnterLoginIdPassword";
        public const string ALREADY_LOGIN = "mAlreadyLogin";
        public const string INVALID_USERID = "InvalidUserId";
        public const string INVALID_USERID_ForgotPassword = "InvalidUserId_ForgotPassword";
        public const string BLANK_USERID = "BlankUserId";
        public const string NO_EMAILID = "NOEmailId";
        public const string INVALID_FIRST_NAME = "mINFirstName";
        public const string INVALID_LAST_NAME = "mINLastName";
        public const string INVALID_EMAIL_ID = "mINEmail";
        public const string ENTER_FIRST_NAME = "mENTFirstName";  //"Please enter first name";
        public const string ENTER_LAST_NAME = "mENTLastName";
        public const string ENTER_EMAIL_ADDRESS = "mENTEmail"; //"Please enter email address";
        public const string INVALID_EMAIL_DOMAIN = "mINEmailDomain";
        public const string EMAIL_EXISTS = "mEmailExists";
        public const string INVALID_PROFILE = "mInvalidProfile";
        public const string PASSWORD_SEND = "mPWDSend";
        public const string LEARNER_ERROR = "mLEARNERERROR";
        public const string ENTER_MIDDLE_NAME = "mENTMiddleName";
        public const string ENTER_ADDRESS = "mENTAddress";
        public const string ENTER_BIRTH_DATE = "mENTBirthDate";
        public const string ENTER_PHONE_NO = "mENTPhoneNo";
        // Email Not Available
        public const string EMAIL_NOT_AVAIL = "mEmailNotAvailable";
        //Email is Available
        public const string EMAIL_IS_AVAIL = "mEmailIsAvailable";
        // Your Profile is updated successfully
        public const string PROFILE_UPDATED = "mPROFILE_UPDATED";
        //User name is available. 
        public const string USER_NAME_AVAIL = "mUserNameAvail";
        //User name is not available. 
        public const string USER_NAME_NOT_AVAIL = "mUserNameNotAvail";
        //Please enter user name. 
        public const string ENT_USER_NAME = "mENT_UserName";
        //Please enter user name alias. 
        public const string ENT_USER_NAME_ALIAS = "mENT_UserNameAlias";
        //Please do not use '*,%,\,/,<,>,|,:,&quot;,?' in username. 
        public const string DO_NOT_USE_SPECIAL_CHARS = "mDoNotUseSpecialChars";
        //Registration successfull. Please check Email for your Username and password 
        public const string REGISTERED_CHECK_EMAIL_FOR_PWD = "mRegisteredCheckEmailForPWD";
        public const string REGISTERED_CHECK_EMAIL_FOR_PWD_FOR_STUDENT = "mRegisteredCheckEmailForPWD_ForStudent";
        //Registration successfull. Please Contact your site administrator fro username and password 
        public const string REGISTERED_CONTACT_ADMIN_FOR_PWD = "mRegisteredContactAdminForPWD";
        public const string SSO_UNAUTHORISED = "mSSO_UNAUTHORISED";
        public const string SSO_ERROR = "mSSO_ERROR";
        //Profile settings updated successfully.
        public const string PROFILE_SET_UPDAT_SUCC = "mProfileSettingUpdateSuccess";
        //Single Sign On updated successfully
        public const string SINGLE_SIGN_ON_UPDATED_SUCCESS = "mSingleSignOnUpdatedSuccess";
        public const string SELF_REGISTRATION_SUCCESS = "mSelfRegistrationSuccess";
        public const string SELECT_IS_INCLUDED_OR_SSO_FORM_CONTROL_ID = "mSelectIsIncludedORSSOFormControl";
        public const string CHECK_LOGOUT_REDIRECTION_URL = "mCheckLogoutRedirectionURL";
        public const string CHECK_RSS_FEED_URL = "mCheckRSSFeedURL";
        public const string CHECK_SSO_URL = "mCheckSSOURL";
        public const string CHECK_SSO_LOGOUT_URL = "mCheckSSOLogOutURL";
        public const string IS_INLUDE_MANDATORY = "mIsIncludedMandatory";
        public const string SSO_FORM_CONTROL_ID_MANDAROTY = "mSSOFormControlIDMandatory";
        public const string ENTER_HIRE_DATE = "mEnterHireDate";
        public const string ENTER_LANGUAGE = "mEnterLanguage";
        public const string ENTER_CURRENT_PASSWORD = "mEnterCurrentPassword";//
        public const string ENTER_PASSWORD = "mEnterPassword";
        public const string ENTER_CONFIRM_PASSWORD = "mEnterConfirmPassword";
        public const string ENTER_MANAGER_EMAIL_ID = "menterManagerEmailID";
        public const string ENTER_REGIONAL_VALUE = "mEnterRegionalValue";
        public const string ENTER_GENDER = "mEnterGender";
        public const string ENTER_THEME = "mEnterTheme";
        public const string ENTER_USER_STATUS = "mEnterUserStatus";
        //Your e-mail Id not in database. Please contact to the site Administrator
        public const string EMAIL_ID_NOT_AVAIL_CONTACT_SITE_ADMIN = "mEmailNotAvailContactSiteAdmin";
        public const string IN_ACTIVE = "mInActiveUserAccount";
        public const string SELF_REGISTRATON_PENDING = "mSelfRegistrationPending";
        //You have already used this password. Please try another.
        public const string TRY_ANOTHER_PWD = "mUserTryAnotherPWD";
        //Max Concurrent sessions limit error.
        public const string MAX_CONCURRENT_SESSION_LIMIT_ERROR = "mMaxConcurrentSessionsLimitError";
        //Browse - Please select .csv or .xls file for upload.
        public const string SELECT_CSVORXLS_FILE_FOR_UPLOAD = "mSelectCSVorXLSFileForUpload";
        //Upload validation -        Please select .csv or .xls file for upload. The file size needs to be within the maximum configured limit. 
        public const string SELECT_CORRECT_DOC_FILE_FOR_UPLOAD = "mSelectCorreectFileForDocumentUpload";

        //Upload validation -        Please select .csv or .xls file for upload. The file size needs to be within the maximum configured limit. 
        public const string STUDENT_SELECT_PLEASE_UPLOAD_DOCUMENT = "mStudentUploadDocument";

        //Upload validation -        Please select .csv or .xls file for upload. The file size needs to be within the maximum configured limit. 
        public const string UPLOAD_FILESIZE_NEEDTOBE_WITHIN_MAX_CONFIG_LIMIT = "mUploadFileSizeNeedToBeWithInMaxConfigLimit";
        //Your profile is updated successfully but system is NOT able to send mail. 
        public const string NOTABLETO_SEND_MAIL_ON_PROFILE_UPDATE = "mNotAbletoSendMailOnProfileUpdate";
        //Please Complete Policies before launching Questionnaire
        public const string COMPLETE_POLICIES_BEFORE_QUESTIONNAIRE = "mCompletePoliciesBeforeQuestionnaire";
        public const string OUT_OF_LENGTH = "mOutOfLength";
        public const string INVALID_HIRE_DATE = "mInvalidHireDate";
        public const string INVALID_TERM_DATE = "mInvalidTermDate";
        public const string ENTER_TERM_DATE = "mEnterTermDate";
        //Invalid organization!
        public const string INVALID_ORG = "mInvalidOrganization";
        //Password does not meet the Password Policy!\\n
        public const string PASS_NOT_POLICY = "mPasswordNotPolicy";
        //Error occurred while setting up user custom field values.
        public const string CUSTOM_FLD_SET_ERROR = "mCusomFieldSetError";
        //Error occurred while setting up user values.
        public const string FLD_SET_ERROR = "mFieldSetError";
        //Error occurred while setting up user values.
        public const string ADD_SUCCESS = "mUserAdded";
        public const string ADD_SUCCESS_SIGNUP_STUDENT = "mUserAdded_SignupStudent";
        //Manager EmailId does not exist!
        public const string INVAILD_MGR_ID = "mInvailMgrEmaiID";
        //Learner EmailId and Manager EmailId should not same!
        public const string LEARNER_MGR_EMAIL_SAME = "mLearnerMgrEmailSame";
        //Your max login Attempts is exceeds, Please close the page and try again.
        public const string LEARNER_MAX_LOGIN_ATTEMPT_EXCEED = "mLearnerMaxLoginAttemptExceed";
        //You have reached the maximum requests get the password.
        public const string USER_MAX_LOGIN_ATTEMPT_REQUEST_EXCEED = "mUserMaxLoginAttemptRequestExceed";
        //Bulk Import action is complete, please view log for import summary. 
        public const string BULK_IMPORT_COMPLETE = "mBulkImportComplete";
        //Bulk Import action is Scheduled. 
        public const string BULK_IMPORT_SCHEDULED = "mBulkImportSCHEDULED";
        //Error occurred while Scheduling!
        public const string BULK_IMPORT_SCHEDULED_ERROR = "mBulkImportSCHEDULEDERROR";
        public const string USER_UPDATED = "mUserUpdated";
        public const string MANAGER_NOT_ACTIVE = "mMGRNotActive";
        //Enter Administrator Id
        public const string ENTER_ADMIN_ID = "mEnterAdminId";
        //Manager EmailId does not exist!  
        public const string MANAGER_EMAIL_NOT_EXIST = "mMGRNotExist";
        //Please enter valid data  
        public const string ENTER_VALID_DATA = "mEnterValidData";
        //Password selection instruction  
        public const string PASSWORD_INSTRUCTION = "mPASSWORDINSTRUCTION";
        // Please add manager name
        public const string ENTER_MANAGER_NAME = "menterManagerName";
        // Invalid manager name
        public const string INVALID_MANAGER_NAME = "minvalidManagerName";
        // Your password has been changed successfully 
        public const string CHANGE_PASSWORD_SUCCESS = "mChangePasswordSuccess";
        public const string REGISTRATION_STATUS_CHANGED_SUCCESS = "mRegistrationStatusChanged";
        public const string REGISTRATION_STATUS_CHANGED_SUCCESS_REJECTED = "mRegistrationStatusChanged_Rejected";
        public const string PROFILE_PHOTO_UPDATE_SUCCESS = "mProfilePhotoUpdateSuccess";
        public const string ALLOW_PROFILE_PHOTO_UPDATE_SUCCESS = "mAllowProfilePhotoUpdateSuccess";
        public const string ENTER_LOCATION = "mSelectLocation";
        public const string CONFIRM_Email_FAIL = "mConfirmEmailFail";
        public const string INVALID_PHONE = "mInvalidPhoneNo";
        public const string LEARNER_PROFILE_PHOTO_UPDATE_SUCCESS = "mLearnerProfilePhotoUpdateSuccess";
        public const string LEARNER_PROFILE_PHOTO_UPDATE_FAIL = "mLearnerProfilePhotoUpdateFail";
        public const string LEARNER_PROFILE_PHOTO_SELECT = "mLearnerProfilePhotoSelect";
        public const string INVALID_EMAILID = "InvalidEmailId";
        public const string Add_Commnet = "AddYourComment";
        public const string ENT_LEARNERTYPE = "mENT_LearnerType";
        public const string ENT_CAPTCHA = "mENT_Captcha";
        public const string ENT_CORRECT_CAPTCHA = "mENT_CorrectCaptcha";
        public const string ENT_CORRECT_IMAGE_FILE = "mENT_CorrectImageFile";
        public const string LEARNER_PROFILE_PICTURE_UPLOAD = "mAllowProfilePictureUpload";
        public const string INVALID_ORG_NAME = "mINOrgName";
        public const string ENTER_ORG_NAME = "mENTOrgName";
        public const string IS_ACTIVE = "mSelectIsActive";
    }

    public class UserPassCode
    {
        public const string INVALID_PASS_CODE = "mInvalidPassCode";
        public const string PASS_CODE_EXPIRED = "mPassCodeExpired";
        public const string MAX_USAGES_LIMIT = "mMaxUsageLimitExceed";
        public const string ENTER_PASS_CODE = "mENTPassCode";
        public const string USER_PASS_CODE_ERROR = "mUSERPASSCODEERROR";
        public const string ENTER_POSITIVE_NO = "mEnter_Positive_Number";
        public const string ENTER_TOTAL_PASS_CODE = "mEnter_Total_Pass_Code";
        public const string ENTER_MAXIMUM_USAGE = "mEnter_Max_Usage";
        public const string ENTER_DATE_RANGE = "mEnter_Date_Range";
        public const string SELECT_FUTURE_DATE = "mSelectFutureDate";
        public const string PASS_CODE_SUCCESS = "mPass_Code_Success";
        public const string INVALID_EMAIL = "mPassCodeCommaSeparateEmail";
        public const string ENTER_PASS_CODE_TITLE = "mEnterPassCodeTitle";
        public const string ENTER_VALID_MONTHS = "mEnterValidMonths";
        //PassCodeInstanse is InActive.
        public const string INACTIVE_PASS_CODE_INSTANCE = "mInActivePassCodeInstance";
        public const string PASSCODE_GREATER_ZERO_ERROR = "mPassCodeNumberZeroError";
        public const string PASSCODE_USAGE_ZERO_ERROR = "mPassCodeUsageZeroError";
        public const string PASSCODE_ALPHANUMERIC_ERROR = "mEnterAlphanumericCharacter";
        public const string PASSCODE_MONTH_GREATER_THAN_ZERO = "mInvalidMonth";
        public const string PASSCODE_UPDATE_SUCCESS = "mPassCodeUpdateSuccess";
        //Passcode(s) activated successfully
        public const string PASSCODE_ACTIVATE_SUCCESS = "mPassCodeActivateSuccess";
        //Passcode(s) are not activated
        public const string PASSCODE_NOT_ACTIVATE = "mPassCodeNotActivate";
    }

    public class Client
    {
        //The URL that you are trying to access is incorrect. Please check the URL
        public const string SYSTEM_MAINT_MESSAGE_SETTING_SAVED = "mClientMaintMessSuccess";
        public const string INVALID_CLIENT_URL = "InvalidClientUrl";
        public const string INVALID_CLIENT_ID = "InvalidClientId";
        public const string CLIENT_DL_ERROR = "mCLIERROR";
        public const string CLIENT_CREATE_ERROR = "mCLICREATEERROR";
        public const string CLIENT_UPDATE_ERROR = "mCLIUPDATEERROR";
        public const string CLIENT_CREATE_SUCCESS = "mCLICREATESUCCESS";
        public const string CLIENT_UPDATE_SUCCESS = "mCLIUPDATESUCCESS";
        public const string CLIENT_SUB_DOMAIN_IN_USE = "mCLISUBDOMAININUSE";
        public const string PROFILE_UPDATED_SUCCESS = "mProfileUpdateSuccess";
        public const string LAYOUT_DL_ERROR = "mLAY_ERROR";
        public const string ORG_LVL_DL_ERROR = "mORG_LEVEL_ERROR";
        public const string ORG_UNIT_DL_ERROR = "mORG_LEVEL_UINIT_ERROR";
        public const string SYS_CONFIG_DL_ERROR = "mORG_SYS_CONFIG_ERROR";
        public const string SSO_CONFIG_DL_ERROR = "mSSO_CONFIG_ERROR";
        public const string THM_DL_ERROR = "mTHEME_ERROR";
        public const string THM_LNG_DL_ERROR = "mTHEME_LANG_ERROR";
        //If selected Level has no Units,the record will been successfully deleted."
        public const string LEVEL_DELETION_ALERT_KEY = "mLEVELDEL";
        //Remove dependent records;
        public const string REMOVE_DEPENDENT = "mREDEPENDENT";
        //Your record has been added successfully.;
        public const string RECORD_ADDED = "mRECORD_ADDED";
        //Your record has not been  successfully added.;
        public const string RECORD_NOT_ADDED = "mRECORD_NOT_ADDED";
        //Your record has been edited successfully.
        public const string RECORD_EDITED = "mRECORD_EDITED";
        //Your record has not been  successfully edited.
        public const string RECORD_NOT_EDITED = "mRECORD_NOT_EDITED";
        //The deletion will be successful if the selected Item does not have dependents.
        public const string RECORD_DELETED = "mRECORD_DELETED";
        //Your record has not been deleted.;
        public const string RECORD_NOT_DELETED = "mRECORD_NOT_DELETED";
        //Select atleast one record for deletion.;
        public const string SELECT_ATLEAST_ONE_MESSAGE = "mSELECT_ONE";
        //Some items have dependency with other items.Remove those items and then delete these.
        public const string DEPENDENCY_DELETION_MESSAGE = "mDEPENDENCY_DELETE";
        //Seesion has expiried. Please login again.
        public const string MSG_SESSION_EXPIRY = "mSESSION_EXPIRY";
        //You need to add the # unit level before you can add unit levels further.
        public const string UNIT_LEVEL_ADD_MESSAGE = "mUNIT_ADD";
        //Unit with the same name already Exists Under
        public const string MSG_UNIT_EXISTS = "mUNIT_ALREADY_EXISTS";
        //Please Enter Client Name
        public const string ENT_CLIENT_NAME = "mENT_ClientName";
        //Please Select Contract Start Date   
        public const string SEL_CONT_START_DATE = "mSEL_CotractStartDate";
        //Contract Start date should be future date or today
        public const string SEL_FUT_CONT_START_DATE = "mSEL_FurtureCotractStartDate";
        //Please Select Contract End Date
        public const string SEL_CONT_END_DATE = "mSEL_CotractEndDate";
        //Contract End date should be future date
        public const string SEL_FUT_CONT_END_DATE = "mSEL_FurtureCotractEndDate";
        //Please Enter Site Sub Domain Name
        public const string ENT_DOMAIN_NAME = "mENT_SiteDomainName";
        //Please Enter Administrator Name
        public const string ENT_ADMIN_NAME = "mENT_AdminName";
        //Please Enter Administrator Phone Number
        public const string ENT_ADMIN_PHONE = "mENT_AdminPhoneNo";
        //Please Enter Valid Email
        public const string ENT_VALID_EMAIL = "mENT_ValidEmail";
        //Please Enter Administrator Login ID
        public const string ENT_ADMIN_LOGIN = "mENT_AdminLogin";
        //Please Enter Administrator Password
        public const string ENT_ADMIN_PWD = "mENT_AdminPwd";
        //Please Enter Confirm Password  
        public const string ENT_ADMIN_CONF_PWD = "mENT_AdminConfPwd";
        //Passwords do not match, please retype
        public const string PWD_CONF_NOT_MATCH = "mPWD_CONFNotmatch";
        //Please Enter/Select SAI Global Program Consultant
        public const string SEL_PROG_CONSULTANT = "mSEL_ProgramConsultant";
        //Please Select Cluster Database Location
        public const string SEL_CLUSTER_DB_LOC = "mSEL_ClusterDBLocation";
        //Please select language(s).
        public const string SEL_LANGUAGE = "mSEL_Language";
        //Please select Predefined Roles.
        public const string SEL_PREDEFIND_ROLE = "mSEL_PredefinedRoles";
        //Please Select Preferred Language
        public const string SEL_PREFERRED_LANG = "mSEL_PreferredLanguage";
        //Please Select Preferred Layout
        public const string SEL_PREFERRED_LAYOUT = "mSEL_PreferredLayout";
        //Please Select Preferred Layout Theme
        public const string SEL_PREFERRED_THEME = "mSEL_PreferredTheme";
        //Enter Number only
        public const string ENT_ONLY_NUMBERS = "mENT_OnlyNumbers";
        //Site Sub Domain Name is available.
        public const string SITE_DOMAIN_IS_AVAIL = "mSITE_Domain_Is_Avail";
        //Site Sub Domain Name is not available.
        public const string SITE_DOMAIN_IS_NOT_AVAIL = "mSITE_Domain_Is_Not_Avail";
        //Please Enter Valid Phone No
        public const string ENT_VALID_PHONE = "mENT_VALID_Phone";
        //Please Enter Administrator Email
        public const string ENT_ADMIN_EMAIL = "mENT_AdminEmail";
        //Poor; Weak; Good; Strong; Excellent
        //Password Strength:
        //Please select item to preview ( Client Related) 
        public const string SEL_ITEM_TO_PREVIEW = "mSEL_ItemToPreview";
        //Logo upload
        public const string LOGO_UPLOAD = "mLogoUploaded";
        //Message if Client Locked
        public const string LOCKED = "mClientLocked";
        //Message if Client Locked
        public const string INACTIVE = "mClientInActive";
        //Message if Client Contract not Started
        public const string CONTRACT_NOT_STARTED = "mClientContractNotStarted";
        //Message if Client contract Expired
        public const string CONTRACT_EXPIRED = "mClientContractExpired";
        //Session updated successfully.
        public const string SESSION_TIME_OUT_UPDATED = "mSessionTimeOutValueUpdated";
        //Lock system updated successfully.
        public const string SYSTEM_LOCK_UPDATED = "mSystemLockUpdated";
        //Unlock system updated successfully.
        public const string SYSTEM_UN_LOCK_UPDATED = "mSystemUNLockUpdated";
        //Forgot password updated successfully.
        public const string FORGOT_PWD_LINK_UPDATED = "mForgotPasswordLinkUpdated";
        //Logout redirection URL updated successfully.
        public const string LOGOUT_REDIRECTION_URL_UPDATED = "mLogOutRedirectionURLUpdated";
        //Configure maximum uploaded file size updated successfully.
        public const string MAX_UPLOAD_SIZE_UPDATED = "mMaxUploadSizeUpdated";
        //PassCode based  updated successfully.
        public const string PASS_CODE_UPDATE_SUCCESS = "mPass_Code_Base_Update_Success";
        //Email domain based  updated successfully.
        public const string EMAIL_DOMAIN_BASE = "mEmail_Domain_Base_Update_Success";
        //Direct registration based updated successfully.
        public const string Direct_REGISTRATION_BASE = "Direct_Registration_Base_Update_Success";
        // Theme Updated Successfully
        public const string THEME_UPLOAD_SUCCESSFULLY = "mThemeUploadSuccessfully";
        // Theme Error
        public const string THEME_UPLOAD_ERROR = "mThemeError";
        //Admin Password is not strong. Please enter strong admin passwordâ€
        public const string STRONG_ADMIN_PWD_REQUIRED = "mStrongAdminPWDRequired";
        //Client site will expire on a date
        public const string CLIENT_SITE_EXPIRATION_DATE = "mClientSiteExpirationDate";
        //Client site expired on a date
        public const string CLIENT_SITE_EXPIRED_DATE = "mClientSiteExpiredDate";
        //Please select only one client.
        public const string SEL_ONLY_ONE_CLIENT = "mSelOnlyOneClient";
        //Please select client.
        public const string SEL_CLIENT = "mSelClient";
        //Record Updated Successfully.
        public const string REC_UPDATE_SUCC = "mRecUpdateSucc";
        //Client Deleted Successfully.
        public const string CLIENT_DEL_SUCC = "mClientDelSucc";
        //Please select Inactive client(s).
        public const string SEL_INACTIVE_CLIENT = "mSelInactiveClient";
        //Please select Active client(s).
        public const string SEL_ACTIVE_CLIENT = "mSelActiveClient";
        //Client Name Already used. Please enter another Client Name.
        public const string CLIENT_ALREADY_USED_ENTER_ANOTHER = "mClientNameAlreadyUsedEnterAnother";
        public const string FEEDBACK_CONFIG_UPDATE_SUCCESS = "mFeedbackConfigUpdateSuccess";
        public const string FEEDBACK_CONFIG_UPDATE_ERROR = "mFeedbackConfigUpdateError";
        public const string CLIENT_ANN_UPDATE_SUCCESS = "mClientAnnUpdateSuccess";
        public const string CLIENT_ANN_UPDATE_ERROR = "mClientAnnUpdateError";
        //Please enter valid data
        public const string ENTER_VALID_DATA = "mPleaseEnterValidData";
        //SAI LMS Client is mapped successfully 
        public const string SAI_LMS_CLIENT_MAP_SUCCESS = "mSaiLmsClientMapSuccess";
        //SAI LMS Client(s) unmapped successfully 
        public const string SAI_LMS_CLIENT_UN_MAP_SUCCESS = "mSaiLmsClientUnMapSuccess";
        //Please select SAI LMS Client Name 
        public const string PLZ_SEL_SAI_LMS_CLIENT_NAME = "mPlzSelSaiLmsClientName";
        //Please select LU Client Name 
        public const string PLZ_SEL_LU_CLIENT_NAME = "mPlzSelLuClientName";
        // Select LU Client Name is already mapped
        public const string SEL_LU_CLNT_NAME_ALREADY_MAPPED = "mSelLuClntAlreadyMapped";
        //Do you want to disclose any information ? 
        public const string DO_U_WANT_TO_DISCLOSE_ANY_INFO = "mDoYouWantToDiscloseAnyInfo";

        //1)Please enter row size more than 5
        public const string MIN_PAGE_SIZE_VALUE = "mMinPageSizeValue";
        //2)Please enter positive numbers only
        public const string ENTER_POSITIVE_NUMBERS = "mEnterPositiveNumbers";
        //3)Please enter default row size
        public const string ENTER_DEFAULT_ROW_SIZE = "mEnterDefaultRowSize";
        //4)Default row size setting saved successfully.
        public const string ROW_SIZE_SETTING_SAVED = "mRowSizeSettingSaved";

        public const string AUDITTRAILPERIOD_SAVED = "mAuditTrailPeriodSaved";

        public const string IPerform_RegKey_Unique = "IPerform_RegKey_Unique";
        public const string YPTab_RegKey_Unique = "YPTab_RegKey_Unique";
    }

    public class EmailMessages
    {
        public const string EMAIL_ERROR = "mEMAILMSGERROR";
        public const string EMAIL_NOT_CONFIGURED = "mEMAILNOTCONFIG";
        //Send auto email succeeded.  
        public const string AUTO_EMAIL_SENT = "mAUTOEMAILMSENT";
        //Unable to process auto email.  
        public const string AUTO_EMAIL_ERROR = "mAUTOEMAILMError";
        //Schedule email succeeded.
        public const string SCHEDULE_EMAIL_SENT = "mSCHEDULEEMAILMSENT";
        //Unable to process Schedule email.  
        public const string SCHEDULE_EMAIL_ERROR = "mSCHEDULEEMAILMError";
        //Please enter body message
        public const string PLZ_ENTER_BODY_MSG = "mPlzEnterBodyMsg";
    }
    public class DefaultValue
    {
        public const string RECORD_ADDED = "mDefaultValueRecordAdded";

    }

    public class Common
    {
        public const string INVALID_KEY = "InvalidKey";
        //Email sent successfully to 
        public const string EMAIL_SUCCESS_TO = "mEMAILMSSUCCESS";
        //Please specify the URL of the RSS feed in webpart properties.
        public const string RSS_URL_ERROR = "mRSSURLERROR";
        //Error occured:
        public const string ERROR = "mERROR";
        public const string EMAIL_ERROR = "mEmailError";
        //Click here to View all items
        public const string CLICK_HERE = "mCLICK";
        //Your session is expired. Please login again.
        public const string SESSION_EXP = "mSESSIONEXP";
        //No records found.
        public const string NO_RECORD = "mNORECORD";
        // Feedback send 
        public const string FEEDBACK_SEND = "mFeedBackSend";
        // Feedback send 
        public const string HELPDESK_SEND = "mHelpdeskSend";
        // Record Added successfully
        public const string RECORD_ADDED = "mRecordAdded";
        // Registration successfully
        public const string REGISTER_SUCCESSFULLY = "mRegisterSuccessfully";
        public const string RECORD_NOT_SAVED = "mRECORD_NOT_Saved";
        //Invalid File
        public const string FILE_XSL_INVALID = "mXLSERROR";
        public const string IMPORT_ERROR = "mImportHistoryERROR";
        public const string APP_LOG = "mAPPLOG";
        //The code you typed has expired after 
        public const string CODE_EXPIRED = "mExpiredCode";
        //Code was typed too quickly. Wait at least 
        public const string WAIT_TO_ENTER_CODE = "mWaitToEnterCode";
        //The text you typed does not match the text in the image.
        public const string TEXT_NOT_MATCH = "mTEXTNotMatch";
        //Please specify the URL of the RSS feed in webpart properties.
        public const string SPECIFY_RSS_URL = "mSPECIFYRssURL";
        //File Save Error
        public const string FILE_ERROR = "mFLIERROR";
        public const string FILE_INVALIDEXT_ERROR = "mFILEINVALIDERROR";
        public const string MANIFEST_ERROR = "mMANIFESTREADERROR";
        public const string METADATA_ERROR = "mMETADATAREADERROR";
        // Enter the code shown
        public const string ENTER_CODE_SHOWN = "mENTCodeShown";
        //The code you typed does not match the code in the image
        public const string CODE_NOT_MATCH = "mCodeNotMatch";
        //Password and confirm password does not match
        public const string PWD_NCONF_PWD_NOT_MATCH = "mPWDnCONFPWDNotMatch";    
        //Old Password does not match
        public const string PWD_OLD_PWD_NOT_MATCH = "mOldPasswordDoesNotMatch";
        //Enter password is not valid.
        public const string INVALID_PWD = "mInvalidPWD";
        //Password is not changed successfully.
        public const string PWD_NOT_CHANGED = "mPWDnotChanged";
        //Error occurred while getting information from Server.
        public const string ERR_TO_GET_INFO_FROM_SERVER = "mErrorInGetInfoFromServer";
        //Error is occurred in binding the current course list in the grid
        public const string ERR_IN_CURR_COURSE_BIND = "mCurrCourseBindingError";
        //Error is occurred in binding the completed course list in the grid.
        public const string ERR_IN_COMP_COURSE_BIND = "mCompCourseBindingError";
        //No any current course found
        public const string NO_CURR_COURSE_FIND = "mNoCurrCourseFind";
        //No any completed course found
        public const string NO_COMP_COURSE_FIND = "mNoCompCourseFind";
        // Register successfully but unable to send a mail to you.
        public const string REGISTERED_BUT_UN_SEND_MAIL = "mRegisteredButUnableToSendMail";
        //You are not authorised to view this content
        public const string INVALID_VIEW = "mInvalidView";
        //Invalid Description
        public const string INVALID_DESC = "mInvalidDesc";
        //Invalid Display Order
        public const string INVALID_DISP_ORDER = "mInvalidDisplayOrder";
        //Invalid Query String Value  
        public const string INVALID_QS = "mInvalidQS";
        //Access Error
        public const string NO_ACCESS = "mNoAccess";
        //This course is expired. Please contact to the Site Administrator.
        public const string COURSE_EXPIRED_CONTACT_SITE_ADMIN = "mCourseExpiredContactSiteAdmin";
        //File path is not valid. Please check the file path
        public const string INVALID_FILE_PATH = "mInvalidFilePath";
        public const string SESSION_EXPIRED_LOGIN_AGAIN = "mSessionExpiredLoginAgain";
        // Page under Development
        public const string PAGE_UNDER_DEVELOPMENT = "mPageUnderDevelopment";
        // File Size Greater than configured max size
        public const string FILE_GREATER_MAX = "mFileGreateThanMax";
        // Please enter valid To Date
        public const string VALID_TO_DATE = "mValidToDate";
        // Please enter To Date
        public const string ENTER_TO_DATE = "mEnterToDate";
        // Please enter From Date
        public const string ENTER_FROM_DATE = "mEnterFromDate";
        // Please enter #
        public const string PLEASE_ENTER = "mPleaseEnter";
        // # Delete Successfully.
        public const string DEL_SUCC = "mDel_Succ";
        // # Added Successfully.
        public const string ADD_SUCC = "mAdd_Succ";
        // # Updated Successfully.
        public const string UPDATE_SUCC = "mUpdate_Succ";
        // Are you sure to delete # ?
        public const string DEL_CONFIRM = "mDel_Confirm";
        // You do not have permission to # ?
        public const string PERMISSION = "mPermission";
        // Insufficient Rights
        public const string COMMON_INSUFFICIENT_RIGHTS = "mCommonInsufficientRights";
        //Page element text is not found in the database. Please contact to your Site Administrator. 
        public const string PAGE_ELEMENT_TEXT_NOT_FOUND = "mPageElementTextNotFound";
        public const string COMMON_ENTER_EMAIL = "mCommonEnterEmail";
        public const string COMMON_INVALID_EMAIL = "mCommonInvalidEmail";
        public const string COMMON_ENTER_SCHEDULE_ST_DATE = "mCommonEnterScheduleStDate";
        public const string COMMON_ENTER_SCHEDULE_END_DATE = "mCommonEnterScheduleEndDate";
        public const string SCH_END_DT_MST_B_GRTR_THN_SCH_ST_DT = "mSchEndDtMstBgrtrThnSchStDt";
        public const string COMMON_ENTER_TASK_TITLE = "mCommonEnterTaskTitle";
        public const string COMMON_ENTER_SCH_START_DATE = "mCommonEnterSchStartDate";
        public const string COMMON_ENTER_SCH_END_DATE = "mCommonEnterSchEndDate";
        public const string SCH_END_DT_MST_BGRTR_THN_SCH_STRT = "mSchEndDtMstBGrtrThnStart";
        //Please select not used record  
        public const string SELECT_NOT_USED_RECORD = "mCommonSelectNotUsedRecord";
        //No records deleted
        public const string NO_RECORDS_DELETED = "mNoRecordsDeleted";
        //Please enter valid numeric field # 
        public const string ENTER_VALID_NUMERIC_RANGE_VALUE = "mEnterValidNumericRangeValue";
        //1. Error is occurred in retrive the RSS url.
        public const string ERROR_IN_RSS_URL_RETRIEVAL = "mErrorInRSSURLRetrieval";
        //Please enter numeric value only. 
        public const string ENTER_NUMERIC_VALUE_ONLY = "mEnterNumericValueOnly";
        //Web Address is not valid. 
        public const string INVALID_WEB_ADDRESS = "mInvalidWebAddress";
        //Upload Failed.
        public const string FILE_UPLOAD_FAILED = "mFileUploadFailed";
        //Configure from email id 
        public const string CONFIGURE_FROM_EMAIL_ID = "mConfigureFromEmailID";
        ///Date range must be in between 1/1/1753 to 12/31/9999
        public const string DATE_OUT_OF_RANGE = "mOutOfDate";
        //Error while executing!  
        public const string EXECUTION_ERROR = "mEXECUTIONERROR";
        //Error while reading file!
        public const string XLS_READ_ERROR = "mXLSReadERROR";
        //Error while downloading!!! 
        public const string DOWNLOAD_ERROR = "mDOWNLOADERROR";
        //Please select field(s) to add. 
        public const string SELECT_FIELD_ADD = "mSELECTFIELDADD";
        //Please select field(s) to remove.
        public const string SELECT_FIELD_REMOVE = "mSELECTFIELDREMOVE";
        //Please select only one field to move up.
        public const string SELECT_FIELD_UP = "mSELECTFIELDUP";
        //Please select only one field to move down.
        public const string SELECT_FIELD_DOWN = "mSELECTFIELDDOWN";
        //Please select field to move up.
        public const string SELECT_FIELD_TO_UP = "mSELECTFIELDTOUP";
        //Please select field to move down.
        public const string SELECT_FIELD_TO_DOWN = "mSELECTFIELDTODOWN";
        //Please select other field to move up.
        public const string SELECT_FIELD_OTHER_UP = "mSELECTFIELDOTHERUP";
        //Please select other field to move down.
        public const string SELECT_FIELD_OTHER_DOWN = "mSELECTFIELDOTHERDOWN";
        //Classification Name Not Found 
        public const string CLASSIFICATION_NAME_NOT_FOUND = "mClassificationNameNotFound";
        //Selected Client is not mapped with LU Client 
        public const string CLIENT_NOT_MAPPED_WITH_LU = "mClientNotMappedWithLU";
        //Could not found LU web service url 
        public const string LU_WEB_SERVICE_URL_NOT_FOUND = "mLUWebServiceUrlNotFound";
        //Pdf Control Expired
        public const string PDF_CONTROL_EXPIRED_MSG = "mPDFControlExpiredMsg";
        // Please Enter Valid Data
        public const string PLZ_ENTER_VALID_DATA = "mCommonPlzEnterValidData";
        //Yes
        public const string LU_LAUNCH_YES = "mLULaunchYES";
        // No
        public const string LU_LAUNCH_NO = "mLULaunchNO";
        //Select Date Between
        public const string SELECT_DATE_BETWEEN = "mSelectDateBetween";
        //To
        public const string DATE_CONCATENATION_TO = "mDateConcatenationTo";
        //InValid Date
        public const string INVALID_DATE_ENTERED = "mInValidDateEntered";
        //InValid Date
        public const string DATE_REQUIRED = "mDateRequired";
        // Enter enroll key
        public const string ENTER_ENROLL_KEY = "mENTEnrollKey";
        // Please enter proper captcha text.
        public const string ENTER_CapchaText = "EntCapchaText";

        public const string Email_send_sucessfully = "emailsendsucessfully";

        public const string Mandatory_fields = "Askquestionmadnatoryfields";
    }

    public class Content
    {
        public const string INVALID_COURSE_ID = "InvalidCourseId";
        public const string COURSE_EXISTS = "CourseExists";
        public const string CONT_MOD_BL_ERROR = "mCONTENT_MODULE_ERROR";
        public const string CONT_MOD_TRACK_ERROR = "mContent_Mod_Tracking_ERR";
        public const string ADMIN_CONT_MOD_TRACK_ERROR = "mAdmin_Content_Mod_Tracking_ERR";
        public const string FILE_UPLOADED = "mFILE_UPLOADED";
        public const string FILE_UNZIPPED = "mFILE_UNZIPPED";
        public const string BACK_TO_HOME = "mBACK_TO_HOME";
        public const string BLK_IMP_QUESTION_SCHED_SUCCESS = "mBlkImpQuestionSchedSuccess";
        public const string BLK_IMP_QUESTION_NOT_SCHED = "mBlkImpQuestionNotSched";
        public const string MRK_BLK_CMPLTN_NT_SCH_CHK_IMP_ERR_LOG = "mMrkBlkCmpltnNtSchChkImpErrLog";
        public const string MRK_BLK_CMPLTN_SCH_SUCCESS = "mMrkBlkCmpltnNtSchSuccess";


    }


    public class Asset
    {
        public const string INVALID_ASSET_ID = "InvalidAssetId";
        public const string ASSET_BL_ERROR = "mASSET_ERROR";
        public const string ASSET_ASSIGN = "AssetAssignExist";
        public const string ASSET_SEL_FOLDER = "mAssetSelectFolder";
        public const string ASSET_SEL_FOLDER_ADD = "mAssetSelectFolderForAdd";
        //"Add Asset Operation Failed."
        public const string ASSET_EDIT_SUCCESS = "mAssetEditSuccess";
        public const string ASSET_ADD_SUCCESS = "mAssetAddSuccess";
        public const string ASSET_DELETE_SUCCESS = "mAssetDeleteSuccess";
        public const string ASSET_ADD_FAIL = "mAssetAddFail";
        public const string ASSET_EDIT_FAIL = "mAssetEditFail";
        public const string ASSET_DELETE_FAIL = "mAssetDeleteFail";
        // Error loading asset information
        public const string ASSET_INFO_ERROR = "mAssetInfoError";
        //â€ The Asset is successfully deactivatedâ€
        public const string ASSET_DEACTIVATED = "mAssetDeactivated";
        //â€The Asset is already deactivated.â€
        public const string ASSET_ALREADY_DEACTIVATED = "mAssetAlreadyDeactivated";
        //Selected Asset FileType does not match with selected File for upload.
        public const string FILETYPE_MISMATCH = "mAsset_FileTypeMisMatch";
        //â€œFile with the name $ already exist in the Folder. While uploading the file, system will rename the file to # ."
        public const string FILE_EXISTS_RENAME_FOR_ADD = "mAsset_FileExistsRenameForAdd";
        //â€œAsset Name is Required.â€
        public const string NAME_REQ = "mAsset_NameRequired";
        //"Select an Asset FileType from the dropdown.";
        public const string FILETYPE_REQ = "mAsset_SelectFileType";
        //"Select an Asset File to upload."
        public const string FILE_REQ = "mAsset_FileRequiredForUpload";
        //Assigned Asset cannot be deleted.
        public const string ASSET_CANT_BE_DELETED = "mAsset_CantBeDeleted";
        //VALID_EMAIL Please input valid Email Addresses.
        public const string ENT_VALID_EMAIL_ID = "mAsset_ENTValidEmailId";
        //FLD_REQ_Folder Name is required.
        public const string FOLDER_NAME_REQUIRED = "mAsset_FolderNameRequired";
        //Only the Administrator or the Owner can update the Folder
        public const string ONLY_ADMIN_OR_OWNER_CAN_UPDATE = "mAsset_OnlyAdminOrOwnerCanUpdate";
        //Common Asset/Policy   -  You do not have adequate rights to delete the Folder.
        public const string NO_ADEQUATE_DELETE_RIGHTS = "mAsset_NoAdequateDeleteRights";
        //SELECT_ONLY_ONE -  Select only one item for edit
        public const string SELECT_ONE_ITEM_FOR_EDIT = "mAssetEditoneItemForEdit";
        // Select a Valid Parent Unit.
        public const string SELECT_VALID_PARENT_UNIT = "mAssetSelectValidParentUnit";
        // Select Parent Unit from the dropdown again.
        public const string SELECT_PARENT_UNIT = "mAssetSelectParentUnit";
        //Asset Folder with the same name already exists
        public const string ASSET_FOLDER_NAME_ALREADY_EXITS = "mAssetFolderNameAlreadyExists";
        //"Asset Folder Name is Required."
        public const string ASSET_FOLDER_NAME_REQUIRED = "mAssetFolderNameRequired";
        //You do not have adequate rights to edit the selected Asset folder.
        public const string NO_FOLDER_EDIT_RIGHTS = "mAssetNoFolderEditRights";
        // Folder already contains a File with the same Name. Please rename the file and then upload it.
        public const string FILE_EXIST_WITH_SAME_NAME = "mAssetFileExistWithSameName";
        // -  "Asset File should not be blank."
        public const string FILE_SHOULD_NOTBE_EMPTY = "mAssetFileShouldNotBeEmpty";
        //You do not have adequate rights to modify this Asset.
        public const string NO_FILE_EDIT_RIGHTS = "mAssetNoFileEditRights";
        //You do not have adequate rights to delete this Asset.
        public const string NO_FILE_DELETE_RIGHTS = "mAssetNoFileDeleteRights";
        //  Enter Valid Asset Name
        public const string ENTER_VALID_ASSET_NAME = "mAssetEnterValidAssetName";
        //-    Enter Valid Description
        public const string ENTER_VALID_DESCRIPTION = "mAssetEnterValidDerscription";
        //-    No Special Characters are allowed in FileName
        public const string NO_SPECIAL_CHARS_ALLOWED_IN_FILENAME = "mAssetNoSpecialCharsAllowedInFileName";
        //â€œThe Asset selected for edit is already assigned and the change will be reflected immediately for users accessing it."
        public const string ASSIGN_ASSET_SELECTED_FOR_EDIT = "mAssetAssignPolicySelectedForEdit";
        //Asset Type is required *"
        public const string ASSET_TYPE_IS_REQUIRED = "mAssetTypeisRequired";
        //File Upload is required*
        public const string FILE_UPLOAD_IS_REQUIRED = "mAssetFileUploadIsRequired";
        //Select a Asset File Type.
        public const string SELECT_ASSET_FILE_TYPE = "mAssetSelectPolicyFileType";
        //Selected Asset File for upload is Invalid.
        public const string INVALID_ASSET_FILE_SELECTED_FORUPLOAD = "mAssetInvalidAssetFileSelectedForUpload";
        // Asset  with the same name already exists in the current Asset Folder. 
        public const string ASSET_NAME_EXISTS = "mAssetNameExists";
    }

    public class AssetLibrary
    {
        public const string INVALID_FOLDER_ID = "InvalidFolderId";
        public const string INVALID_PARENT_FOLDER_ID = "InvalidParentFolderId";
        public const string ASSET_LIB_BL_ERROR = "mASSET_LIB_ERROR";
        public const string ASSET_LIB_DEL_ERROR = "mASSET_LIB_DEL_ERROR";
        public const string ASSET_LIBRARY_ASSIGN = "AssetLibAssignExist";
        //"Delete the assets under the folder and then delete the folder."
        public const string DELETE_ASSETS_UNDER_FOLDER = "mFirstDeleteAssetsUnderFolder";
        public const string FOLDER_ADD_SUCCESS = "mAssetFolderAddSuccess";
        public const string FOLDER_EDIT_SUCCESS = "mAssetFolderEditSuccess";
        public const string FOLDER_DELETE_SUCCESS = "mAssetFolderDeleteSuccess";
        public const string FOLDER_ADD_FAIL = "mAssetFolderAddFail";
        public const string FOLDER_EDIT_FAIL = "mAssetFolderEditFail";
        public const string FOLDER_DELETE_FAIL = "mAssetFolderDeleteFail";
        //AssetLibrary-Enter Valid Folder Name
        public const string ENTER_VALID_FOLDER_NAME = "mAssetLibraryEnterValidFolderName";
    }
    public class RefDocument
    {
        public const string REFDOCUMENT_DAM_ERROR = "mREFDOCUMENT_DAM_ERROR";
        public const string REFDOCUMENT_NAME_EXIST = "mREFDOCUMENT_NAME_EXIST";
        public const string REFDOCUMENT_PLEASE_SELECT_ACTIVITY = "mREFDOCUMENT_PLEASE_SELECT_ACTIVITY";
        public const string REFDOCUMENT_ADD_FAIL = "mREFDOCUMENT_ADD_FAIL";
        public const string REFDOCUMENT_ADD_SUCCESS = "mREFDOCUMENT_ADD_SUCCESS";
        public const string REFDOCUMENT_UPDATE_SUCCESS = "mREFDOCUMENT_UPDATE_SUCCESS";
        public const string REFDOCUMENT_DELETE_FAIL = "mREFDOCUMENT_DELETE_FAIL";
        public const string REFDOCUMENT_DELETE_SUCCESS = "mREFDOCUMENT_DELETE_SUCCESS";
        public const string REFDOCUMENT_NUMBERSONLY = "mREFDOCUMENT_NUMBERSONLY";
        public const string REFDOCUMENT_NAME_IS_REQUIRED = "mREFDOCUMENT_NAME_IS_REQUIRED";
        public const string REFDOCUMENT_DESCRIPTION_IS_REQUIRED = "mREFDOCUMENT_DESCRIPTION_IS_REQUIRED";
        public const string REFDOCUMENT_REFDOCTYPE_IS_REQUIRED = "mREFDOCUMENT_REFDOCTYPE_IS_REQUIRED";
        public const string REFDOCUMENT_REFDOCFILE_IS_REQUIRED = "mREFDOCUMENT_REFDOCFILE_IS_REQUIRED";
    }
    public class PasswordPolicy
    {
        public const string INVALID_PWD_POLICY_ID = "InvalidPwdPolicyConfigurationId";
        public const string PWD_POLICY_CONFIG_BL_ERROR = "mPWDPOLICY_ERROR";
        public const string PWD_MAX_LEN_ERROR = "mPWDMaxLenError";
        public const string PWD_MIN_LEN_ERROR = "mPWDMinLenError";
    }

    public class Policy
    {
        public const string INVALID_POLICY_ID = "InvalidPolicyId";
        public const string INVALID_FOLDER_ID = "InvalidParentFolderId";
        public const string POLICY_BL_ERROR = "mPOLICY_ERROR";
        public const string POLICY_ASSIGN = "PolicyAssignExist";
        // Error loading asset information
        public const string POLICY_INFO_ERROR = "mPolicyInfoError";
        public const string POLICY_SELECT_FOLDER = "mPolicySelectFolderForAdd";
        public const string POLICY_ADD_SUCCESS = "mPolicyAddSuccess";
        public const string POLICY_EDIT_SUCCESS = "mPolicyEditSuccess";
        public const string POLICY_DELETE_SUCCESS = "mPolicyDeleteSuccess";
        public const string POLICY_ADD_FAIL = "mPolicyAddFail";
        public const string POLICY_EDIT_FAIL = "mPolicyEditFail";
        public const string POLICY_DELETE_FAIL = "mPolicyDeleteFail";
        //â€ The Policy is successfully deactivatedâ€
        public const string POLICY_DEACTIVATED = "mPolicyDeactivated";
        //â€The Policy is already deactivated.â€
        public const string POLICY_ALREADY_DEACTIVATED = "mPolicyAlreadyDeactivated";
        //"You must either upload a file OR provide a policy URL and not both of them.";
        public const string EITHER_FILE_OR_URL_NOT_BOTH = "mPolicy_UploadEitherFileOrURLNotBoth";
        //â€You must either upload a file OR provide a policy URLâ€;
        public const string EITHER_FILE_OR_URL = "mPolicy_UploadEitherFileOrURL";
        //â€œSelected Policy FileType does not match with selected File for upload.â€
        public const string FILETYPE_MISMATCH = "mPolicy_FileTypeMisMatch";
        //â€œPolicy Name is Required.â€
        public const string NAME_REQ = "mPolicy_NameRequired";
        //"Select an Policy FileType from the dropdown.";
        public const string FILETYPE_REQ = "mPolicy_SelectFileType";
        //"Select an Policy File to upload."
        public const string FILE_REQ = "mPolicy_FileRequiredForUpload";
        //VALID_EMAIL Please input valid Email Addresses.
        public const string ENT_VALID_EMAIL_ID = "mAsset_ENTValidEmailId";
        //Assigned Policy cannot be deleted.
        public const string POLICY_CANT_BE_DELETED = "mPolicy_CantBeDeleted";
        //Policy Folder with the same name already exists
        public const string POLICY_FOLDER_NAME_ALREADY_EXITS = "mPolicyFolderNameAlreadyExists";
        //"Policy Folder Name is Required."
        public const string POLICY_FOLDER_NAME_REQUIRED = "mPolicyFolderNameRequired";
        // Select a Valid Parent Unit.
        public const string SELECT_VALID_PARENT_UNIT = "mPolicySelectValidParentUnit";
        // Select Parent Unit from the dropdown again.
        public const string SELECT_PARENT_UNIT = "mPolicySelectParentUnit";
        //NO_RIGHTS_FOR_FOLDER  You do not have adequate rights to edit the selected Policy folder.
        public const string NO_FOLDER_EDIT_RIGHTS = "mPolicyNoFolderEditRights";
        // Folder already contains a File with the same Name. Please rename the file and then upload it.
        public const string FILE_EXIST_WITH_SAME_NAME = "mPolicyFileExistWithSameName";
        //-  "Policy File should not be blank."
        public const string FILE_SHOULD_NOTBE_EMPTY = "mPolicyFileShouldNotBeEmpty";
        //File_EDIT_NO_RIGHTS  You do not have adequate rights to modify this Policy.
        public const string NO_FILE_EDIT_RIGHTS = "mPolicyNoFileEditRights";
        //File_DELETE_NO_RIGHTS You do not have adequate rights to delete this Policy.
        public const string NO_FILE_DELETE_RIGHTS = "mPolicyNoFileDeleteRights";
        //EMAIL_SENT "Email Notification Sent.";
        public const string EMAIL_NOTIFICATION_SENT = "mPolicyEmailNotificationSent";
        //EMAIL_FAILED  "But Email Notification Failed.";
        public const string EMAIL_NOTIFICATION_FAILED = "mPolicyEmailNotificationFailed";
        //        Policy -  Enter a Valid URL.
        public const string ENTER_VALID_URL = "mPolicyEnterValidURL";
        //-    Enter Valid Description
        public const string ENTER_VALID_DESCRIPTION = "mPolicyEnterValidDescription";
        //-    Enter Valid Policy Name
        public const string ENTER_VALID_POLICY_NAME = "mPolicyEnterValidPolicyName";
        //-    No Special Characters are allowed in FileName
        public const string NO_SPECIAL_CAHR_IN_FILENAME = "mPolicyNoSpecialCharInFileName";
        //-    Either Upload File or Policy URL is required.
        public const string UPLOAD_FILE_OR_POLICY_URL_REQUIRED = "mPolicyFileOrPolicyURLRequired";
        //â€œThe Policy selected for edit is already assigned and the change will be reflected immediately for users accessing it."
        public const string ASSIGN_POLICY_SELECTED_FOR_EDIT = "mPolicyAssignPolicySelectedForEdit";
        //PolicyLibrary-Enter Valid Folder Name
        public const string ENTER_VALID_FOLDER_NAME = "mPolicyLibraryEnterValidFolderName";
        //Policy Type is required *"
        public const string POLICY_TYPE_IS_REQUIRED = "mPolicyTypeisRequired";
        //File Upload is required*
        public const string FILE_UPLOAD_IS_REQUIRED = "mPolicyFileUploadIsRequired";
        //Select a Policy File Type.
        public const string SELECT_POLICY_FILE_TYPE = "mPolicySelectPolicyFileType";
        //Selected Asset File for upload is Invalid.
        public const string INVALID_ASSET_FILE_SELECTED_FORUPLOAD = "mPolicyInvalidAssetFileSelectedForUpload";
        // -  Select an Email Template
        public const string SELECT_EMAIL_TEMPLATE = "mPolicySelectEmailTemplate";
        // - Email Notification on policy Creation/Update
        public const string NOTIFY_EMAIL_ADD_UDPATE = "mPolicyNotifyEmailAddUpdate";
        //POLICY_NAME_EXISTS â€“ Policy  with the same name already exists in the current Policy Folder.
        public const string POLICY_NAME_EXISTS = "mPolicyNameExists";
        //EMAILID_INVALID - Email Notification cancelled as specified EmailId is Invalid.
        public const string EMAILID_INVALID = "mPolicyEmailIdInvalid";
        //        Policy -  Policy URL is not accept PDF file name..
        public const string POLICY_URL_IS_NOT_ALLOW_PDF = "mPolicyURLIsNotAllowPDF";
    }

    public class PolicyLibrary
    {
        public const string INVALID_FOLDER_ID = "InvalidFolderId";
        public const string LIBRARY_BL_ERROR = "mPOLICY_LIB_ERROR";
        public const string LIBRARY_DEL_ERROR = "mPOLICY_LIB_DEL_ERROR";
        public const string LIBRARY_ASSIGN = "mPolicyLibAssignExist";
        public const string FOLDER_ADD_SUCCESS = "mPolicyFolderAddSuccess";
        public const string FOLDER_EDIT_SUCCESS = "mPolicyFolderEditSuccess";
        public const string FOLDER_DELETE_SUCCESS = "mPolicyFolderDeleteSuccess";
        public const string FOLDER_ADD_FAIL = "mPolicyFolderAddFail";
        public const string FOLDER_EDIT_FAIL = "mPolicyFolderEditFail";
        public const string FOLDER_DELETE_FAIL = "mPolicyFolderDeleteFail";
        //"Delete the assets under the folder and then delete the folder."
        public const string DELETE_POLICIES_UNDER_FOLDER = "mFirstDeletePoliciesUnderFolder";
        //â€œAdd New Libraryâ€
        public const string ADD_NEW_LIBRARY = "mPolicyFolderAddNewLibrary";
    }

    public class Calendar
    {
        public const string INVALID_MONTH = "InvalidMonth";
        public const string CALENDER_ERROR = "CalenderError";
    }

    public class Announcement
    {
        public const string ERROR_MSG_ID = "mANNOUNCEERROR";

        public const string ANNOUNCE_DEL_SUCCESS = "mAnnounceDelSuccess";
        public const string SEL_ANNOUNCE_CANT_B_DEL = "mSelAnnounceCantBDel";
        public const string ANNOUNCE_DEACTIVE_SUCCESS = "mAnnounceDeactivateSuccess";
        public const string ANNOUNCE_DEACTIVE_ALREADY = "mAnnounceDeactivateAlready";
        public const string ANNOUNCE_EXP_TO_VIEW_SAMPLE = "mAnnounceExpToViewSample";
        public const string ANNOUNCE_SEL_ATLST_1_FIELD = "mAnnounceSelAtlst1Field";
        public const string ANNOUNCE_IMPORT_SUCCESS = "mAnnounceImportSuccess";
        public const string ANNOUNCE_SEL_EXCEL_FILE = "mAnnounceSelExcelFile";
        public const string ANNOUNCE_OWNR_SITEADMIN_HAVE_PRIVILAGE = "mAnnounceOwnerSiteAdminHavePrivilage";
        public const string PLZ_SEL_ANNOUNCE = "mPlzSelectAnnounce";
        public const string DEL_ALL_TRANSLT_VER_ANNOUNNCE_B4_DEL_SEL_ANNOUN = "mDelAlltransltverAnnounceB4DelSelAnnounc";
        public const string ANN_ST_DT_MST_B_GRTR_THN_OR_EQ_TDYS_DT = "mAnStDtMstBGrtrThnTdysDt";
        //Selected Announcement is already Deactivated
        public const string ANNOUNCEMENT_IS_ALREADY_DEACTIVIATED = "mAnnouncementIsAlreadyDeactiviated";
        // Please deactivate announcement before deleting 
        public const string DEACTIVATE_ANN_B_4_DELETE = "mDeactivateAnnB4Delete";
        public const string ANNOUNCEMENT_INVALID_URL = "mAnnouncementInvalidURL";
    }

    public class ImportHistory
    {
        public const string IMP_HISTORY_ERROR_MSG_ID = "mIMPHISTORYERROR";
    }

    public class StudentList
    {
        public const string IMP_HISTORY_ERROR_MSG_ID = "mIMPHISTORYERROR";
    }

    public class ILTImportHistory
    {
        public const string IMP_HISTORY_ERROR_MSG_ID = "mIMPHISTORYERROR";
    }

    public class FeedBack
    {
        public const string FEEDBACK_ERROR_MSG_ID = "mFeedBackERROR";
        public const string ENTER_COMMENTS = "mENTComments";
        public const string ENTER_EMAIL = "mENTEmail";
        public const string FEEDBACK_NOT_SEND = "mFEEDBACK_Not_Saved";
        public const string UNABLE_TO_SEND_MAIL = "mUNSendMail";
        //We have received your feedback but unable to send mail.
        public const string FEDBK_RECD_BUT_UNABLE_TO_SEND_MAIL = "mFeedBackRecdButUNSendMail";

        public const string RECEIPT_MAIL_SEND = "mReceiptSendMail";
    }
    public class Helpdesk
    {
        public const string HELPDESK_ERROR_MSG_ID = "mHelpdeskERROR";
        public const string ENTER_EMAIL = "mENTEmail";
        public const string HELPDESK_NOT_SEND = "mHELPDESK_Not_Saved";
        public const string UNABLE_TO_SEND_MAIL = "mUNSendMail";
        public const string RECEIPT_MAIL_SEND = "mReceiptSendMail";
    }

    public class ImportDefinition
    {
        public const string IDF_ERROR = "mIMPORTDEFERROR";
    }

    public class ReportDeliveryDashboard
    {
        public const string RPT_DEL_DASH_ERROR_MSG_ID = "mRptDelDashErrorMsgId";
        //Please select report scheduler task 
        public const string SELECT_REPORT_SCHEDULER_TASK = "mSelectReportSchedulerTask";
        //Selected report scheduler task is suspended successfully 
        public const string SELECTED_TASK_SUSPENDED = "mSelectTaskSuspended";
        //Selected report scheduler task is resumed successfully 
        public const string SELECTED_TASK_RESUMED = "mSelectTaskResumed";
        //Selected report scheduler task has been deleted successfully 
        public const string SELECTED_TASK_DELETED = "mSelectTaskDeleted";
        //Only task owner and site admin have privileges to edit the task 
        public const string OWNER_N_SITEADMIN_HAVE_TASK_EDIT_RIGHTS = "mOwnerNSiteAdminHaveTaskEditRights";
        //Only task owner and site admin have privileges to delete the task 
        public const string OWNER_N_SITEADMIN_HAVE_TASK_DELETE_RIGHTS = "mOwnerNSiteAdminHaveTaskDeleteRights";
        //Only task owner and site admin have privileges to resume the task 
        public const string OWNER_N_SITEADMIN_HAVE_TASK_RESUME_RIGHTS = "mOwnerNSiteAdminHaveTaskResumeRights";
        //Only task owner and site admin have privileges to suspend the task 
        public const string OWNER_N_SITEADMIN_HAVE_TASK_SUSPEND_RIGHTS = "mOwnerNSiteAdminHaveTaskSuspendRights";
        //Report has been scheduled successfully
        public const string RPT_SCHEDULED_SUCCESS = "mRptScheduledSuccess";
        //Start Date cannot be less than todays date 
        public const string RPT_ST_DT_CNT_B_LESS_THN_TDYS_DT = "mRptStDtCntBLessThnTdysDt";
        //Please select File Format 
        public const string RPT_PLZ_SEL_FILE_FRMT = "mRptPlzSelFileFrmt";
        //Please select Email Template 
        public const string RPT_PLZ_SEL_EMAIL_TEMP = "mRptPlzSelEmailTemp";
        //Please select Distribution List 
        public const string RPT_PLZ_SEL_DIST_LIST = "mRptPlzSelDistList";
        //An error occured while adding scheduler entry, please contact your site administrator 
        public const string RPT_SCH_ERR_CNTCT_SITE_ADMIN = "mRptSchErrCntctSiteAdmin";
        //15. Selected Report Scheduler Task is already in Active State
        public const string SELECTED_TASK_IS_IN_ACTIVE_STATE = "mSelectedTaskIsInActiveState";
        //16. Selected Report Scheduler Task is already in Suspend State
        public const string SELECTED_TASK_IS_IN_SUSPEND_STATE = "mSelectedTaskIsInSuspendState";
    }

    public class AdminRole
    {
        public const string ADMIN_ROLE_ERROR = "mAROLEERROR";
        //Please select Role 
        public const string SEL_ROLE = "mSEL_Role";
        //Enter First Name or Learner ID 
        public const string ENT_NAME_OR_ID = "mENT_LearnerNameOrId";
        //No User(s) Found. 
        public const string NO_USERS_FOUND = "mNO_UsersFound";
        //Please Enter Keyword/s or Phrase 
        public const string ENT_KEYWORDS_OR_PHASE = "mENT_KeywordsOrPhase";
        //Please Enter Valid Keyword/s or Phrase 
        public const string ENT_VALID_KEYWORDS_OR_PHASE = "mENT_ValidKeywordsOrPhase";
        //Please Enter Role Name. 
        public const string ENT_ROLE = "mENT_Role";
        //Please select User(s)/Custom Group. 
        public const string SEL_USER_CUSTOM_GROUP = "mSEL_UserCustomGroup";
        //Role Assigned to selected User(s). 
        public const string ROLE_ASSIGNED_TO_USER = "mROLE_AssignedToUser";
        //Please select Scope. 
        public const string SEL_SCOPE = "mSEL_Scope";
        //Role Un Assigned to selected User(s). 
        public const string ROLE_UNASSIGNED_TO_USER = "mROLE_UnAssignedToUser";
        //Please select only one Role. 
        public const string SEL_ONE_ROLE = "mSEL_OnlyOneRole";
        //Role(s) not assigned to user(s) deleted successfully
        public const string ROLE_DELETED = "mRoleDeleted";
        //You can modify only Active Roles.
        public const string ONLY_ACTIVE_ROLES_CANBE_MODIFIED = "mActiveRolesCanbeModified";
        //You do not have adequate rights to modify this Role.
        public const string NO_RIGHTS_TO_MODIFY_ROLE = "mNoRightsToModifyRole";
        //Role(s) Owned by you Activated Successfully.
        public const string ROLE_ACTIVATED = "mRoleActivated";
        //You do not have adequate rights to Activate Role(s).
        public const string NO_RIGHTS_TO_ACTIVATE_ROLE = "mNoRightsToActivateRole";
        //Role(s) Owned by you Deactivated Successfully.
        public const string ROLE_DEACTIVATED = "mRoleDeActivated";
        //You do not have adequate rights to Deactivate Role(s).
        public const string NO_RIGHTS_TO_DEACTIVATE_ROLE = "mNoRightsToDeActivateRole";
        //You do not have adequate rights to Delete Role(s).
        public const string NO_RIGHTS_TO_DELETE_ROLE = "mNoRightsToDeleteRole";
        //Role Name exists. Please enter another Role Name.
        public const string ROLE_NAME_EXISTS = "mAdminRoleNameExists";
    }

    public class AdminFeature
    {
        public const string ADMIN_FEATURE_ERROR = "mAFEATUREERROR";
    }

    public class SysMessage
    {
        public const string SYS_MSG_ERROR = "mMESSAGE_ERROR";
        //Select at least one langauge to import. 
        public const string SEL_LANGUAGE_TO_IMPORT = "mSEL_LanguageToImport";
        //Error occured while importing messages. 
        public const string ERR_IN_IMPORT_PROCESS = "mERR_InImportProcess";
        //Messages imported to system successfully.
        public const string MSG_IMPORTED_SUCCESSFULLY = "mMsgImportedSuccessfully";
        //Messages updated successfully.
        public const string MSG_UPDATED_SUCCESSFULLY = "mMsgUpdatedSuccessfully";
    }

    public class GroupRule
    {
        public const string RULE_ERROR = "mRULE_ERROR";
        public const string SEL_CON_GROUP = "mCustGroupSelect";
        //You can not delete rule, please select rule created by you
        public const string CAN_NOT_DEL_RULE = "mCanNotDelRule";
        //You can not deactivate rule, please select rule created by you
        public const string CAN_NOT_DEACT_RULE = "mCanNotDeactvateRule";
    }

    public class ReportSelectedColumns
    {
        public const string REPORT_SELECTED_COLUMNS_ERROR = "mReportSelectedColumnsError";
    }

    public class GroupReport
    {
        public const string CUST_REPORT_ERROR = "mCustReportError";
        public const string SEL_CUST_GROUP = "mCustGroupSelect";
        //You can not delete report, please select report created by you
        public const string CAN_NOT_DEL_REPORT = "mCanNotDelReport";
        //You can not deactivate report, please select report created by you
        public const string CAN_NOT_DEACT_REPORT = "mCanNotDeactvateReport";
        // Please select group.
        public const string SELECT_GROUP = "mRptSelectGroup";
        //Please add at least single condition
        public const string ADD_LEAST_SINGLE_CONDITION = "mRptAddLeastSingleCondition";
        //Report updated successfully 
        public const string REPORT_UPDATED_SUCCESS = "mReportUpdateSuccess";
        //Activated # REPORT(s) 
        public const string ACTIVATED_REPORT = "mActivatedReport";
        //Deactivated # Report(s) 
        public const string DEACTIVATED_REPORT = "mDeactivatedReport";
        //Business REPORT(s) # Deleted Sucessfully
        public const string BUSINESS_REPORT_DEL_SUCESS = "mBusinessReportDelSucess";
        //You can not edit this Report 
        public const string CAN_NOT_EDIT_REPORT = "mCanNotEditReport";
        //Report copied successfully
        public const string REPORT_COPIED_SUCCESS = "mReportCopiedSuccess";
        //Report added successfully 
        public const string REPORT_ADDED_SUCCESS = "mReportAddedSuccess";
        //1) Please select inactive Reports only
        public const string SELECT_INACTIVE_REPORT = "mSelectInActiveReportOnly";
        //2) Please select active Reports only
        public const string SELECT_ACTIVE_REPORT = "mSelectActiveReportOnly";
        //No records deleted
        public const string NO_REC_DEL = "mRptNoRecDel";
        //No records deactivated
        public const string NO_REC_DEACTIVATED = "mRptNoRecDeactivated";
        //Report name already exists
        public const string REPORT_NAME_EXIST = "mReportNameExist";
        //You dont have permission to # Edit Report
        public const string NOT_PERMISSION_EDIT_REPORT = "mNotPermissionEditReport";
        // Report successfully shared.
        public const string REPORT_SUCC_SHARED = "mRptSuccShared";
        //Please select at least single column to generate report
        public const string SELECT_COLUMN_TO_GENERATE_REPORT = "mRptSelectColumnToGenerateReport";
    }

    public class MenuItem
    {
        public const string MENU_ITEM_ERROR = "mMenuItem_ERROR";
    }

    public class Lookup
    {
        public const string LOOK_UP_ERROR = "mLOOKUP_ERROR";
    }

    public class Language
    {
        public const string LANGUAGE_ERROR = "mLANGUAGE_ERROR";
    }

    public class RegionView
    {
        public const string REGION_VIEW_ERROR = "mRegion_View_ERROR";
        //1.You do not have rights
        public const string YOU_DONT_HAVE_RIGHTS = "mRegionViewUdontHaveRights";
        //2.Please select Regional View
        public const string SELECT_REGION_VIEW = "mSelectRegionView";
        //3.Please select only one Regional View
        public const string SELECT_ONLY_ONE_REGION_VIEW = "mSelectOnlyOneRegionView";
        //4.Regional View(s) deleted Successfully.
        public const string REGION_VIEW_DELETED = "mRegionViewDeleted";
        //5.Regional View added Successfully.
        public const string REGION_VIEW_ADDED = "mRegionViewAdded";
        //6.Regional View updated Successfully.
        public const string REGION_VIEW_UPDATED = "mRegionViewUpdated";
        //7.Regional View name required.
        public const string REGION_VIEW_NAME_REQUIRED = "mRegionViewNameRequired";
        //8.Select Business Rule.
        public const string SELECT_BUSINESS_RULE = "mSelectBusinessRule";
        //Regional View Name already Exists
        public const string REGION_VIEW_NAME_EXISTS = "mRegionViewNameExists";
    }

    public class CustomFieldItem
    {
        public const string CUSTOM_FLD_ITEM_ERROR = "mCUSTOM_fld_Item_ERR";
        public const string CUSTOM_FLD_ITEM_ADD = "mCustItemAdd";
        public const string CUSTOM_FLD_ITEM_DEL = "mCustDelete";
        public const string CUSTOM_FIELD_ITEM_UPDATE = "mCustFitemupdate";
        public const string INVALID_NAME = "mCFItemInvalidName";
        public const string LANG_UPDATED = "mCustFItemLangUp";
        public const string CUSTOM_FIELD_ITEM_NAME_BLANK = "mCustFItemNameBlank";
        //Custom Field Display Text should not be blank
        public const string CF_DISPLAY_TEXT_EMPTY = "mCustFDisplayTextEmpty";
        //Custom Field Item deleted successfully. 
        public const string CF_ITEM_DELETED = "mCustFItemDeleted";
        //Custom Field Item cannot be deleted. 
        public const string CF_ITEM_CANT_DELETED = "mCustFItemCantDeleted";
    }

    public class CustomField
    {
        public const string CUSTOM_FIELD_ERROR = "mCUSTOM_field_ERR";
        public const string CUSTOM_FIELD_ADD = "mCustFAdd";
        public const string CUSTOM_FIELD_UPDATE = "mCustFupdate";
        public const string CUSTOM_FIELD_SEL = "mCustFselect";
        public const string CUSTOM_FIELD_ONE_SEL = "mCustOneSelect";
        public const string INVALID_NAME = "mInvalidName";
        public const string LANG_UPDATED = "mCustFLangUp";
        public const string CUSTOM_FIELD_NAME_BLANK = "mCustFNameBlank";
        //Custom Field Name already exists 
        public const string CF_ALREADY_EXISTS = "mCustFAlreadyExists";
        //Custom Field deleted successfully 
        public const string CF_DELETED = "mCustFDeleted";
        //Custom Field cannot be deleted, it is in use 
        public const string CF_INUSE_CANT_DELETED = "mCustFInUseCantDeleted";
        //Custom Field(s) # Deleted Sucessfully
        public const string CUSTOM_FIELDS_DEL_SUCESS = "mCustomFieldDelSucess";
        public const string CUSTOM_SORT_BY_DISPLAY_ORDER = "mSortByDisplayOrder";
    }

    public class SourceCluster
    {
        public const string SOURCE_CLUSTER_UPDATE_SUCCESS = "mSourceClusterUpdateSuccess";
        public const string SOURCE_CLUSTER_UPDATE_ERROR = "mSourceClusterUpdateError";
        public const string SOURCE_CLUSTER_PASSWORD_MISMATCH = "mSourceClusterPasswordMismatch";

    }

    public class ControlType
    {
        public const string CONTROL_TYPE_ERROR = "mControl_Type_ERR";
    }

    public class ActivityAssignment
    {
        public const string ACTIVITY_ASSIGN_ERROR = "mActivity_Assign_ERR";
        public const string ACTIVITY_ASSIGN_ADD_SUCCESS = "mActivityAssignAddSuccess";
        public const string PAST_DATE_NOT_ALLOWED_RE_CREATION = "mPastDateNotAllowedReCreation";
        public const string PAST_DATE_NOT_ALLOWED_EXPIRY = "mPastDateNotAllowedExpiry";
        public const string PAST_DATE_NOT_ALLOWED_DUE = "mPastDateNotAllowedDue";
        public const string PAST_DATE_NOT_ALLOWED_ASSIGN_DATE = "mPastDateNotAllowedAssign";
        public const string ENTER_POSITIVE_NUMBERS = "mEnterPositiveNumber";
        public const string ENTER_VALID_DATE = "mEnterValidDate";
        public const string ENTER_ASSIGNMENT_DATE = "mEnterAssignmentDate";
        public const string ENTER_DAYS_FOR_ASSIGNMENT = "mEnterDaysForAssignment";
        //Please enter valid from hire date and to hire date.
        public const string ENT_VALID_VALUES_FOR_DATE_RANGE = "mEnterValidValuesForDateRange";
        //Please select assignments and learners.
        public const string SELECT_ACTIVITY_AND_USERS = "mUserAssignment_SelectActivityNUsers";
        //2) Please enter assignment due date.
        public const string ENT_ASSIGN_DUE_DATE = "mUserAssignment_EntAssignDueDate";
        //3) Please enter assignment due days.
        public const string ENT_ASSIGN_DUE_DAYS = "mUserAssignment_EntAssignDueDays";
        //4) Please enter assignment expiry date.
        public const string ENT_ASSIGN_EXPIRY_DATE = "mUserAssignment_EntAssignExpiryDate";
        //5) Please enter assignment expiry days.
        public const string ENT_ASSIGN_EXPIRY_DAYS = "mUserAssignment_EntAssignExpiryDays";
        //6) Please enter assignment hire due date.
        public const string ENT_NEWHIRE_DUE_DATE = "mUserAssignment_EntNewHireDueDate";
        //7) Please enter assignment hire due days.
        public const string ENT_NEWHIRE_DUE_DAYS = "mUserAssignment_EntNewHireDueDays";
        //8) Please enter assignment hire expiry date.
        public const string ENT_NEWHIRE_EXPIRY_DATE = "mUserAssignment_EntNewHireExpiryDate";
        //9) Please enter assignment hire expiry days.
        public const string ENT_NEWHIRE_EXPIRY_DAYS = "mUserAssignment_EntNewHireExpiryDays";
        //10) Please enter reassignment due date.
        public const string ENT_REASSIGN_DUE_DATE = "mUserAssignment_EntReassignDueDate";
        //11) Please enter reassignment due days.
        public const string ENT_REASSIGN_DUE_DAYS = "mUserAssignment_EntReassignDueDays";
        //12) Please enter reassignment expiry date.
        public const string ENT_REASSIGN_EXPIRY_DATE = "mUserAssignment_EntReassignExpiryDate";
        //13) Please enter reassignment expiry days.
        public const string ENT_REASSIGN_EXPIRY_DAYS = "mUserAssignment_EntReassignExpiryDays";
        //1) Assignment due date must be greater than assignment date.
        public const string DUE_DT_MUSTBE_GE_ASSIGN_DT = "mUserAssignment_DueDtMustbeGEAssignDt";
        //2) Assignment expiry date must be greater than assignment due date.
        public const string EXPIRY_DT_MUSTBE_GE_DUE_DT = "mUserAssignment_ExpiryDtMustbeGEDueDt";
        //3) Assignment hire due date must be greater than hire assignment date
        public const string NHIRE_DUE_DT_MUSTBE_GE_NHIRE_ASSIGN_DT = "mUserAssignment_NHireDueDtMustbeGEAssignDt";
        //4) Assignment hire expiry date must be greater than hire due date.
        public const string NHIRE_EXPIRY_DT_MUSTBE_GE_NHIRE_ASSIGN_DT = "mUserAssignment_NHireExpiryDtMustbeGEDueDt";
        //5) Reassignment due date must be greater than reassignment date.
        public const string REASSIGN_DUE_DT_MUSTBE_GE_REASSIGN_DT = "mUserAssignment_ReassignDueDtMustbeGEReassignDt";
        //6) Reassignment expiry date must be greater than reassignment due date
        public const string REASSIGN_EXPIRY_DT_MUSTBE_GE_REASSIGN_DUE_DT = "mUserAssignment_ReassignExpiryDtMustbeGEReassignDueDt";
        //Please select email template.
        public const string SELECT_EMAIL_TEMPLATE = "mUserAssignment_SelectEmailTemplate";
        //New expiry date must be greater than new hire due date
        public const string NEWHIRE_EXPIRYDT_MUSTBE_GT_DUEDT = "mAssignmentNewHireExpiryDtMustBeGTDueDt";
        //Please select activity(s) for mark completion.
        public const string SELECT_ACTIVITY_FOR_MARK_COMPLETION = "mSelectActivityForMarkCompletion";
        public const string ACTIVITY_REASSIGNMENT_WARNING = "mACTIVITY_REASSIGNMENT_WARNING";
        public const string ACTIVITY_REASSIGNMENT_WARNING_ReRegister = "mACTIVITY_REASSIGNMENT_WARNING_ReRegister";
        public const string ACTIVITY_REREGISTER_SUCCESS_MSG = "mACTIVITY_REREGISTER_SUCCESS_MSG";
        public const string ACTIVITY_REREGISTER_FAIL_MSG = "mACTIVITY_REREGISTER_FAIL_MSG";

        public const string ACTIVITY_IS_ALREADY_ASSIGNED = "mActivityAlreadyAssigned";
        public const string CERTIFICATION_PROGRAM_APPROVED = "mCertificationActivityApprove";
        public const string CERTIFICATION_PROGRAM_REJECTED = "mCertificationActivityReject";
        public const string DESELECT_FORCEFUL_CHECKBOX_WARNING = "mDeselect_Forceful_Checkbox_Warning";
    }

    public class OrgLevel
    {
        public const string ORG_LEVEL_ERROR = "mORG_Level_ERR";
        //LEVEL_EXISTS -  Level with the same name already exists.
        public const string LEVEL_EXISTS = "mORGLevelExists";
        //Orglevel - You need to add a level first and then add a unit at the selected parent.
        public const string ADD_LEVEL_FIRST = "mORGLevelAddLevelFirst";
        // - Error generating tree.
        public const string ERROR_IN_GENERATING_TREE = "mORGLevelErrorinGeneratingTree";
    }

    public class OrgLevelUnit
    {
        public const string ORG_LEVEL_UNIT_ERROR = "mORG_Level_Unit_ERR";
        //Delete_unallocated_Units -  Units Not allocated deleted successfully."
        public const string DELETE_UNALLOCATED_UNITS = "mDeleteUnaloocatedORGLevelUnits";
        //ALLOCATED_DEL  Selected Units for deletion are Allocated and cannot be deleted."
        public const string UNABLE_DELETE_ALLOCATED_UNITS = "mUnableToDeleteAllocatedUnits";
        //SELECT_PARENTUNIT - Select Parent Unit from the dropdown again.
        public const string SELECT_PARENT_UNIT = "mORGLevelSelectParentUnit";
        //SELECT_VALIDPARENT - Select a Valid Parent Unit.
        public const string SELECT_VALID_PARENT = "mORGLevelSelectValidParent";
        //UNIT_REQ - Unit Name is required.
        public const string UNIT_REQUIRED = "mORGLevelUnitRequired";
        //UNIT_EXISTS - Unit with same name already exists under the select parent unit.
        public const string UNIT_EXISTS = "mORGLevelUnitExists";
        //  OrganizationUnit - Selected Organization Level is out of your current Scope.
        public const string OUT_OF_SCOPE_LEVEL_SELECTED = "mOutOfScopeLevelSelected";
        //-    Level name is  required *
        public const string LEVEL_NAME_REQUIRED = "mLevelNameRequired";
        //-    Level unit name is required *
        public const string LEVEL_UNIT_NAME_REQUIRED = "mLevelUnitNameRequired";
        //-    Please enter validate Unit Name.
        public const string ENTER_VALID_UNIT_NAME = "mEnterValidUnitName";
        //-    Please select a level.
        public const string SELECT_LEVEL = "mSelectLevel";
        //-    Select the Parent Unit.
        public const string SELECT_PARENT_LEVEL = "mSelectParentLevel";
        //-    Some of the units selected have child units. Child Units will also be deleted.
        public const string CHILD_UNITS_WILL_ALSOBE_DELETED = "mChildUnitsWillbeDeleted";
        //-    Unit Item row nos. - # are allocated and cannot be deleted. Uncheck those.
        public const string CANT_DELETE_ALLOCATED_UNIT_ITEMS = "mCantDeleteAllocatedUnitItems";
        //-    Level Item row nos. - # is having Units under it and cannot be deleted. Uncheck those.
        public const string CANT_DELETE_PARENT_LEVEL_ITEMS = "mCantDeleteParentLevel";
        //-    Cannot delete Root Node    
        public const string CANT_DELETE_ROOT_NODE = "mCantDeleteRootNode";
        //-    Are you sure to delete selected records.
        public const string CONFIRM_DELETE = "mOrgLevelUnitConfirmDelete";
        //Please enter Valid Level Name
        public const string ENTER_VALID_LEVEL_NAME = "mOrgLevelEnterValidLevelName";
    }

    public class SysConfiguration
    {
        public const string SYS_CONFIG_ERROR = "mSYS_Config_ERROR";
        public const string FILE_SIZE_EXCEED = "mFileSizeExceed";
        public const string LOGO_SELECT_FILE = "mLogoSelectFile";
        public const string LOGO_IE_SECURITY_ISSUE_MSG = "mLogoIESecurityIssueMsg";
        //SMTP Settings updated successfully.
        public const string SMTP_SETTING_UPDATED = "mSMTPSettingUpdated";
        //Date Format settings updated successfully.
        public const string DATE_FORMAT_SETTING_UPDATED = "mDateFormatSettingUpdated";
        public const string HTTPS_ALLOWED_SUCCESS = "mHTTPSAllowedSucces";
        public const string HTTPS_ALLOWED_FAILED = "mHTTPSAllowedFailed";
        public const string LOGO_VALID_FORMAT = "mLogoValidFormat";
    }

    public class ThemeLanguage
    {
        public const string THEME_LANG_ERROR = "mTHEME_LANG_ERROR";
    }

    public class Questionnaire
    {
        public const string QUESTIONNAIRE_ERROR = "mQUESTIONNAIRE_ERROR";
        //File Extension must be .xls or .csv only. 
        public const string FILE_EXTENTION_MUST_BE = "mFileExtentionMustBeXLSCSV";
        //Error occured on Export/Import For Questionnaire Translation page. 
        public const string ERR_ON_TRANSLATION_PAGE = "mERRonQUESTNTransationPage";
        //Error occured while exporting questioner. 
        public const string ERR_QUESTIONNAIRE_EXPORT = "mERRInQUESTIONNAIREExport";
        //File Extension must be .xls only. 
        public const string IMPORT_FILE_EXTENTION = "mQuestN_XLSFileExtention";
        //Please locate file to upload 
        public const string LOCATE_UPLOAD_FILE = "mLocateQuestnnaireFileToUpload";
        //Error occured while importing questioner. 
        public const string ERR_QUESTIONNAIRE_IMPORT = "mERRInQUESTIONNAIREImport";
        //Questionnaire approved successfully. 
        public const string QUESTIONNAIRE_APPROVED = "mQUESTIONNAIREApproved";
        //Questionnaire added successfully.
        public const string QUESTIONNAIRE_ADDED = "mQuestonnaireAdded";
        //Questionnaire addition failed.
        public const string QUESTIONNAIRE_ADD_FAILED = "mQuestonnaireAddFailed";
        //Questionnaire edited successfully.
        public const string QUESTIONNAIRE_EDITED = "mQuestonnaireEdited";
        //Questionnaire edition failed.
        public const string QUESTIONNAIRE_EDIT_FAILED = "mQuestonnaireEditFailed";
        //Question added successfully.
        public const string QUESTION_ADDED = "mQuestonAdded";
        //Question addition failed.
        public const string QUESTION_ADD_FAILED = "mQuestonAddFailed";
        //Question edited successfully.
        public const string QUESTION_EDITED = "mQuestonEdited";
        //Question edition failed.
        public const string QUESTION_EDIT_FAILED = "mQuestonEditFailed";
        //Section added successfully.
        public const string SECTION_ADDED = "mSectionAdded";
        //Section addition failed.
        public const string SECTION_ADD_FAILED = "mSectionAddFailed";
        //Section edited successfully.
        public const string SECTION_EDITED = "mSectionEdited";
        //Section edition failed.
        public const string SECTION_EDIT_FAILED = "mSectionEditFailed";
        //Questionnaire deactivated successfully.
        public const string QUESTIONNAIRE_DEACTIVATED = "mQuestionnaireDeactivated";
        //Questionnaire deactivation failed.
        public const string QUESTIONNAIRE_DEACTIVATE_FAILED = "mQuestionnaireDeactivateFailed";
        //Questionnaire copied successfully.
        public const string QUESTIONNAIRE_COPIED = "mQuestionnaireCopied";
        //Questionnaire copy failed.
        public const string QUESTIONNAIRE_COPY_FAILED = "mQuestionnaireCopyFailed";
        //The email has been sent successfully for review.
        public const string EMAIL_SENT_FOR_REVIEW = "mEmailSentForReview";
        //Invalid email addresses.
        public const string INVALID_EMAIL = "mInvalidEmailAddress";
        //Questionnaire deleted successfully.
        public const string QUESTIONNAIRE_DELETED = "mQuestonnaireDeleted";
        //Questionnaire deletion failed.
        public const string QUESTIONNAIRE_DELETE_FAILED = "mQuestonnaireDeleteFailed";

        public const string USED_QUESTIONNAIRE_IN_ACTIVITIES = "mQuestonnaireUsedInActivities";
        //Questionnaire approved failed.
        public const string QUESTIONNAIRE_APPROVAL_FAILED = "mQuestonnaireApprovalFailed";
        //Congratulations !! You have completed Questionnaire Successfullly.
        public const string QUESTIONNAIRE_COMPLETED = "mQuestonnaireCompleted";
        //Questionnaire Submit Failed.
        public const string QUESTIONNAIRE_SUBMIT_FAILED = "mQuestonnaireSubmitFailed";
        //You have finished the Questionnaire. Please click on Submit button to complete the Questionnaire.
        public const string CLICK_SUBMIT_TO_COMPLETE = "mClickSubmitToCompleteQuestionnaire";
        //ConfigLogoUploaded:     "Logo uploaded successfully. Please click on \"Update Configuration\" button to save the logo.";
        public const string LOGO_UPDATED = "mQuestionnaireLogoUpdated";
        //ConfigUpdateSuccess:    Configuration updated Successfully.
        public const string CONFIGURATION_UPDATED = "mQuestionnaireConfigurationUpdated";
        //ConfigUpdateFailed:     Configuration updation failed.    
        public const string CONFIGURATION_UPDATE_FAILED = "mQuestionnaireConfigurationUpdateFailed";
        //No file selected to upload. Please select file then click on upload
        public const string SELECT_FILE_TO_UPLOAD = "mQuestionnaireSelectFileToUpload";
        //Please answere atleast one question, then clik on save.
        public const string ANSWERE_ATLEAST_ONE_QUESTION = "mQuestionnaireAnswereAtleastOneQuest";
        //Questionnaire Saved successfullly?
        public const string QUESTIONNAIRE_SAVED = "mQuestionnaireSaved";
        //Please enter explanation text and then click submit.
        public const string ENTER_EXPLANATION_AND_SUBMIT = "mQuestionnaireEnterExplanation";
        //Questionnaire name already exists.
        public const string QUESTIONNAIRE_NAME_ALREADY_EXISTS = "mQuestionnaireNameAlreadyExists";
        //Please enter explanation text.
        public const string ENTER_EXPLANATION_TEXT = "mQuestionnaireEnterExplanationText";
        //Please select atleast one Option.
        public const string SELECT_ONE_OPTION = "mQuestionnaireSelectOneOption";
        //Questionnaire modified, re-edit sequencing before preview.
        public const string REEDIT_SEQUENCE_BEFORE_PREVIEW = "mReEditSequencingBeforePreview";
        //Switching language will delete all responses of current language. Do you wish to continue?
        public const string LANGUAGE_SWITCH_DELETES_RESPONSES = "mLanguageSwitchDeletesResponses";
        //INVALID Excel File.
        public const string INVALID_EXCEL_FILE = "mInvalidExcelFile";
        //Bulk Import completed, Successfull.
        public const string IMPORT_COMPLETED = "mqImportCompleted";
        //Bulk Import Completed, with Errors.
        public const string IMPORT_COMPLETED_WITH_ERROR = "mImportCompletedWithError";
        //Please describe in the Explanation Box provided below:
        public const string DESCRIBE_IN_EXPLANATION_BOX = "mDescribeInExplanationBox";
        //Questionnaire structure do not match, please re-export questionnaire and then Import it!! 
        public const string STRUCTURE_DONOTMATCH_REEXPORT = "mQstructureDoNotMatchReExport";
        //Empty translation found in uploaded Excel. Translation Failed.
        public const string TRANSLATION_FAILED_BEING_EMPTY = "mQTranslationFailedBeingEmpty";
        //Questionnaire Title is missing. Translation Failed.
        public const string TRANSLATION_FAILED_QTITLE_MISSING = "mQTranslationFailedQTitleMissing";
        //â€œQuestionnaire activated successfully.â€
        public const string QUEST_ACTIVATE_SUCCESS = "mQuestionActivateSuccess";
        //â€œQuestionnaire activation failed.â€
        public const string QUEST_ACTIVATE_FAILED = "mQuestionActivateFailed";
        //â€œSelected questionnaire is already active.â€ 
        public const string QUEST_ALREADY_ACTIVATE = "mQuestionAlreadyActive";
        //Your responses is pending for administrator review. Hence new attempt cannot be started.â€ 
        public const string QUEST_RES_PEND_FOR_ADMIN_REVIEW = "mQuestResPendForAdminReview";
        //Please select atleast one questionnaire.
        public const string PLZ_SEL_ATLEAST_1_QUEST = "mPlzSelAtleast1Quest";
        //Allocated questionnaire cannot be deleted.
        public const string ALLOCATED_QUEST_CNT_B_DEL = "mAllocatedQuestCntBDel";
        //Please select only one Questionnaire for copy.
        public const string SELECT_ONE_QUESTIONNAIRE_FOR_COPY = "mSelectOneQuestionnaireForCopy";
        public const string QUESTIONARIE_NO_FILE_SELECTED_2_UPLOAD = "mQuestionarieNoFileSelectedToUpload";
        public const string QUESTIONARIE_EXIT_CONFIRM_MSG = "mQuestionarieExitConfirmMsg";
        public const string QUESTIONARIE_SEQUENCE_SAVE_SUCCESS = "mQuestionarieSequenceSaveSuccess";
        public const string QUESTIONARIE_SEQUENCE_SAVE_ERROR = "mQuestionarieSequenceSaveError";
        //Questionnaire name is required
        public const string QUESTIONARIE_NAME_REQ = "mQuestionaireNameReq";
        //Please select the color or write the color code
        public const string QUESTIONARIE_COLOR_CODE_REQ = "mQuestionaireColorCodeReq";
        // Answer All Questions and then Submit
        public const string ANS_ALL_QUEST_AND_SUBMIT = "mAnsAllQuestAndSumbit"; 
        
        // Answer All Survey Questions and then Submit
        public const string ANS_ALL_SURVEY_RESPONSEQUEST_AND_SUBMIT = "mSurveyQuestionnaireEnterExplanation";
        // Questionnaire was completed by Admin
        public const string QUEST_WAS_COMPLETED_BY_ADMIN = "mQuestWasCompletedByAdmin";
        //Do you want to print the questionnaire?
        public const string QUESTIONARIE_PRINT_CONFIRM_MSG = "mQuestionariePrintConfirmMsg";
    }

    public class Assessment
    {
        public const string ASSESSMENT_ERROR = "mASSESSMENT_ERROR";
        //File Extension must be .xls or .csv only. 
        public const string FILE_EXTENTION_MUST_BE = "mFileExtentionMustBeXLSCSV";
        //Error occured on Export/Import For Assessment Translation page. 
        public const string ERR_ON_TRANSLATION_PAGE = "mERRonQUESTNTransationPage";
        //Error occured while exporting questioner. 
        public const string ERR_ASSESSMENT_EXPORT = "mERRInASSESSMENTExport";
        //File Extension must be .xls only. 
        public const string IMPORT_FILE_EXTENTION = "mQuestN_XLSFileExtention";
        //Please locate file to upload 
        public const string LOCATE_UPLOAD_FILE = "mLocateQuestnnaireFileToUpload";
        //Error occured while importing questioner. 
        public const string ERR_ASSESSMENT_IMPORT = "mERRInASSESSMENTImport";
        //Assessment approved successfully. 
        public const string ASSESSMENT_APPROVED = "mASSESSMENTApproved";
        //Assessment added successfully.
        public const string ASSESSMENT_ADDED = "mAssessmentAdded";
        //Assessment addition failed.
        public const string ASSESSMENT_ADD_FAILED = "mAssessmentAddFailed";
        //Assessment edited successfully.
        public const string ASSESSMENT_EDITED = "mAssessmentEdited";
        //Assessment edition failed.
        public const string ASSESSMENT_EDIT_FAILED = "mAssessmentEditFailed";
        //Question added successfully.
        public const string QUESTION_ADDED = "mQuestionAdded";
        //Question addition failed.
        public const string QUESTION_ADD_FAILED = "mQuestionAddFailed";
        //Question edited successfully.
        public const string QUESTION_EDITED = "mQuestionEdited";
        //Question edition failed.
        public const string QUESTION_EDIT_FAILED = "mQuestionEditFailed";
        //Section added successfully.
        public const string SECTION_ADDED = "mSectionAdded";
        //Section addition failed.
        public const string SECTION_ADD_FAILED = "mSectionAddFailed";
        //Section edited successfully.
        public const string SECTION_EDITED = "mSectionEdited";
        //Section edition failed.
        public const string SECTION_EDIT_FAILED = "mSectionEditFailed";
        //Assessment deactivated successfully.
        public const string ASSESSMENT_DEACTIVATED = "mAssessmentDeactivated";
        //Assessment deactivation failed.
        public const string ASSESSMENT_DEACTIVATE_FAILED = "mAssessmentDeactivateFailed";
        //Assessment copied successfully.
        public const string ASSESSMENT_COPIED = "mAssessmentCopied";
        //Assessment copy failed.
        public const string ASSESSMENT_COPY_FAILED = "mAssessmentCopyFailed";
        //The email has been sent successfully for review.
        public const string EMAIL_SENT_FOR_REVIEW = "mEmailSentForReview";
        //Invalid email addresses.
        public const string INVALID_EMAIL = "mInvalidEmailAddress";
        //Assessment deleted successfully.
        public const string ASSESSMENT_DELETED = "mAssessmentDeleted";
        //Assessment deletion failed.
        public const string ASSESSMENT_DELETE_FAILED = "mAssessmentDeleteFailed";
        //Assessment approved failed.
        public const string ASSESSMENT_APPROVAL_FAILED = "mAssessmentApprovalFailed";
        //Congratulations !! You have completed Assessment Successfullly.
        public const string ASSESSMENT_COMPLETED = "mAssessmentCompleted";
        //Assessment Submit Failed.
        public const string ASSESSMENT_SUBMIT_FAILED = "mAssessmentSubmitFailed";
        //You have finished the Assessment. Please click on Submit button to complete the Assessment.
        public const string CLICK_SUBMIT_TO_COMPLETE = "mClickSubmitToCompleteAssessment";
        //ConfigLogoUploaded:     "Logo uploaded successfully. Please click on \"Update Configuration\" button to save the logo.";
        public const string LOGO_UPDATED = "mAssessmentLogoUpdated";
        //ConfigUpdateSuccess:    Configuration updated Successfully.
        public const string CONFIGURATION_UPDATED = "mAssessmentConfigurationUpdated";
        //ConfigUpdateFailed:     Configuration updation failed.    
        public const string CONFIGURATION_UPDATE_FAILED = "mAssessmentConfigurationUpdateFailed";
        //No file selected to upload. Please select file then click on upload
        public const string SELECT_FILE_TO_UPLOAD = "mAssessmentSelectFileToUpload";
        //Please answere atleast one question, then clik on save.
        public const string ANSWERE_ATLEAST_ONE_QUESTION = "mAssessmentAnswereAtleastOneQuest";
        //Assessment Saved successfullly?
        public const string ASSESSMENT_SAVED = "mAssessmentSaved";
        //Please enter explanation text and then click submit.
        public const string ENTER_EXPLANATION_AND_SUBMIT = "mAssessmentEnterExplanation";
        //Assessment name already exists.
        public const string ASSESSMENT_NAME_ALREADY_EXISTS = "mAssessmentNameAlreadyExists";
        //Please enter explanation text.
        public const string ENTER_EXPLANATION_TEXT = "mAssessmentEnterExplanationText";
        //Please select atleast one Option.
        public const string SELECT_ONE_OPTION = "mAssessmentSelectOneOption";
        //Assessment modified, re-edit sequencing before preview.
        public const string REEDIT_SEQUENCE_BEFORE_PREVIEW = "mReEditSequencingBeforePreview";
        //Switching language will delete all responses of current language. Do you wish to continue?
        public const string LANGUAGE_SWITCH_DELETES_RESPONSES = "mLanguageSwitchDeletesResponses";
        //INVALID Excel File.
        public const string INVALID_EXCEL_FILE = "mInvalidExcelFile";
        //Bulk Import completed, Successfull.
        public const string IMPORT_COMPLETED = "mImportCompleted";
        //Bulk Import Completed, with Errors.
        public const string IMPORT_COMPLETED_WITH_ERROR = "mImportCompletedWithError";
        //Please describe in the Explanation Box provided below:
        public const string DESCRIBE_IN_EXPLANATION_BOX = "mDescribeInExplanationBox";
        //Assessment structure do not match, please re-export questionnaire and then Import it!! 
        public const string STRUCTURE_DONOTMATCH_REEXPORT = "mQstructureDoNotMatchReExport";
        //Empty translation found in uploaded Excel. Translation Failed.
        public const string TRANSLATION_FAILED_BEING_EMPTY = "mQTranslationFailedBeingEmpty";
        //Assessment Title is missing. Translation Failed.
        public const string TRANSLATION_FAILED_QTITLE_MISSING = "mTranslation_Failed_Qtitle_Missing";
        //â€œAssessment activated successfully.â€
        public const string ASSESSMENT_ACTIVATE_SUCCESS = "mAssessmentActivateSuccess";
        //â€œAssessment activation failed.â€
        public const string ASSESSMENT_ACTIVATE_FAILED = "mAssessmentActivateFailed";
        //â€œSelected questionnaire is already active.â€ 
        public const string ASSESSMENT_ALREADY_ACTIVATE = "mAssessmentAlreadyActive";
        //Your responses is pending for administrator review. Hence new attempt cannot be started.â€ 
        public const string ASSESSMENT_RES_PEND_FOR_ADMIN_REVIEW = "mAssessmentResPendForAdminReview";
        //Please select atleast one questionnaire.
        public const string PLZ_SEL_ATLEAST_1_ASSESSMENT = "mPlzSelAtleast1Assessment";
        //Allocated questionnaire cannot be deleted.
        public const string ALLOCATED_QUEST_CNT_B_DEL = "mAllocatedQuestCntBDel";
        //Please select only one Assessment for copy.
        public const string SELECT_ONE_ASSESSMENT_FOR_COPY = "mSelectOneAssessmentForCopy";
        public const string ASSESSMENT_NO_FILE_SELECTED_2_UPLOAD = "mQuestionarieNoFileSelectedToUpload";
        public const string ASSESSMENT_EXIT_CONFIRM_MSG = "mQuestionarieExitConfirmMsg";
        public const string ASSESSMENT_SEQUENCE_SAVE_SUCCESS = "mQuestionarieSequenceSaveSuccess";
        public const string ASSESSMENT_SEQUENCE_SAVE_ERROR = "mQuestionarieSequenceSaveError";
        //Assessment name is required
        public const string ASSESSMENT_NAME_REQ = "mAssessmentNameReq";
        //Please select the color or write the color code
        public const string ASSESSMENT_COLOR_CODE_REQ = "mQuestionaireColorCodeReq";
        // Answer All Questions and then Submit
        public const string ANS_ALL_QUEST_AND_SUBMIT = "mAnsAllQuestAndSumbit";
        // Assessment was completed by Admin
        public const string ASSESSMENT_WAS_COMPLETED_BY_ADMIN = "mAssessmentWasCompletedByAdmin";
        public const string ASSESSMENT_ANSWER_ATLEAST_ONE_QUESTION = "mAssessmentAnswerAtleastOneQuest";
        public const string ASSESSMENT_ALERTTIME_CANNOT_BE_SPECIFY = "mAssessmentAlerttimeCannotbeSpecify";
        public const string ASSESSMENT_QUESTIONS_NOT_ATTEMPTED = "mNotAttemptedQuestionInAssessment";
        public const string ASSESSMENT_TIME_FINISHED = "mAssessmentTimeFinished";
        public const string ASSESSMENT_COMPLETED_INSTRUCTION = "mAssessmentCompleteInstruction";
        public const string ASSESSMENT_REQ = "mAssessmentReq";
        public const string ASSESSMENT_MAPPING_SEQ_NO_REQUIRED = "mASSESSMENT_MAPPING_SEQ_NO_REQUIRED";
        public const string ASSESSMENT_MAPPING_NUMBER_GREATERTHAN_0 = "mASSESSMENT_MAPPING_NUMBER_GREATERTHAN_0";
        public const string ASSESSMENT_MAPPING_NUMBER_REQ = "mASSESSMENT_MAPPING_NUMBER_REQ";
        public const string ASSESSMENT_EXIT_CONFIRM = "mAssessmentExitConfirmMsg";
        public const string ASSESSMENT_IS_INUSE = "mSelectedAssessmentIsInUse";
        public const string IS_SUBMIT_ASSESSMENT = "IsSubmitAssessment";
        public const string ASSESSMENT_LOCK = "AssessmentLock";
        public const string ASSIGNMENT_UNLOCK = "mAssignmentUnlock";
    }

    public class QuestionnaireSections
    {
        public const string QUESTIONNAIRE_SECTION_ERROR = "mQUESTIONNAIRE_SEC_ERROR";
        public const string QUESTIONNAIRE_OPT_SEQ_SUCCESS = "mQUESTIONNAIRE_OPT_SEQ_SUCCESS";
        public const string QUESTIONNAIRE_OPT_SEQ_ERROR = "mQUESTIONNAIRE_OPT_SEQ_ERROR";
        public const string QUESTIONNAIRE_SECTION_SELECT = "mQUESTIONNAIRE_SEC_SELECT";

        public const string QUESTIONNAIRE_SECTION_DEL_SUCCESS = "mQUESTIONNAIRE_SECTION_DEL_SUCCESS";
        public const string QUESTIONNAIRE_SECTION_DEL_ERROR = "mQUESTIONNAIRE_SECTION_DEL_ERROR";
        public const string QUESTIONNAIRE_SECTION_NAME_REQ = "mQUESTIONNAIRE_SECTION_NAME_REQ";

    }

    public class AssessmentSections
    {
        public const string ASSESSMENT_SECTION_ERROR = "mASSESSMENT_SEC_ERROR";
        public const string ASSESSMENT_OPT_SEQ_SUCCESS = "mASSESSMENT_OPT_SEQ_SUCCESS";
        public const string ASSESSMENT_OPT_SEQ_ERROR = "mASSESSMENT_OPT_SEQ_ERROR";
        public const string ASSESSMENT_SECTION_SELECT = "mASSESSMENT_SEC_SELECT";

        public const string ASSESSMENT_SECTION_DEL_SUCCESS = "mASSESSMENT_SECTION_DEL_SUCCESS";
        public const string ASSESSMENT_SECTION_DEL_ERROR = "mASSESSMENT_SECTION_DEL_ERROR";
        public const string ASSESSMENT_SECTION_NAME_REQ = "mASSESSMENT_SECTION_NAME_REQ";

    }

    public class QuestionnaireQuestions
    {
        public const string QUESTIONNAIRE_QUET_ERROR = "mQUESTIONNAIRE_QUT_ERROR";
        public const string QUESTIONNAIRE_QUET_SEQ_SUCCESS = "mQUESTIONNAIRE_QUT_SEQ_SUCCESS";
        public const string QUESTIONNAIRE_QUET_SEQ_ERROR = "mQUESTIONNAIRE_QUT_SEQ_ERROR";
        public const string SEQ_EDIT_CONFIRM_MSG = "mSEQ_EDIT_CONFIRM_MSG";
        public const string QUESTIONNAIRE_QUESTION_SELECT = "mQUESTIONNAIRE_QUESTION_SELECT";
        public const string QUESTIONNAIRE_QUESTION_ANS_ATLEAST_1 = "mQUESTIONNAIRE_QUESTION_ANS_ATLEAST_1";
        public const string QUESTIONNAIRE_QUESTION_DEL_SUCCESS = "mQUESTIONNAIRE_QUESTION_DEL_SUCCESS";
        public const string QUESTIONNAIRE_QUESTION_DEL_ERROR = "mQUESTIONNAIRE_QUESTION_DEL_ERROR";
        public const string QUESTIONNAIRE_QUESTION_NAME_REQ = "mQUESTIONNAIRE_QUESTION_NAME_REQ";
    }

    public class AssessmentQuestionMapping
    {
        public const string ASSESSMENTQUESTIONMAPPING_DAM_ERROR = "mASSESSMENTQUESTIONMAPPING_DAM_ERROR";
    }

    public class AssessmentQuestions
    {
        public const string ASSESSMENT_QUET_ERROR = "mASSESSMENT_QUT_ERROR";
        public const string ASSESSMENT_QUET_SEQ_SUCCESS = "mASSESSMENT_QUT_SEQ_SUCCESS";
        public const string ASSESSMENT_QUET_SEQ_ERROR = "mASSESSMENT_QUT_SEQ_ERROR";
        public const string SEQ_EDIT_CONFIRM_MSG = "mSEQ_EDIT_CONFIRM_MSG";
        public const string ASSESSMENT_QUESTION_SELECT = "mASSESSMENT_QUESTION_SELECT";
        public const string ASSESSMENT_QUESTION_ANS_ATLEAST_1 = "mASSESSMENT_QUESTION_ANS_ATLEAST_1";
        public const string ASSESSMENT_QUESTION_DEL_SUCCESS = "mASSESSMENT_QUESTION_DEL_SUCCESS";
        public const string ASSESSMENT_QUESTION_DEL_ERROR = "mASSESSMENT_QUESTION_DEL_ERROR";
        public const string ASSESSMENT_QUESTION_NAME_REQ = "mASSESSMENT_QUESTION_NAME_REQ";
        public const string ASSESSMENT_QUESTION_DEACTIVATE = "mASSESSMENT_QUESTION_DEACTIVATE";
        public const string ASSESSMENT_QUESTION_ACTIVATE = "mASSESSMENT_QUESTION_ACTIVATE";
    }

    public class Task
    {
        public const string ERROR_MSG_ID = "mTASKERROR";
        // Selected Task is already in active state 
        public const string SEL_TASK_ALREADY_ACTIVE = "mSelTaskAlreadyActive";
        //Selected Task is resumed successfully 
        public const string SEL_TASK_RESUME_SUCCESS = "mSelTaskResumeSuccess";
        //Selected Task is already in suspended state 
        public const string SEL_TASK_ALREADY_SUSPENDED = "mSelTaskAlreadySuspended";
        //Selected Task is suspended successfully 
        public const string SEL_TASK_SUSPENDED_SUCCESS = "mSelTaskSuspendedSuccess";
        //Selected task is deleted successfully. 
        public const string SEL_TASK_DELETE_SUCCESS = "mSelTaskDeleteSuccess";
        //Please select task. 
        public const string PLZ_SELECT_TASK = "mPlzSelectTask";
        //End Date cannot be less than Start Date 
        public const string TASK_END_DT_CNT_B_LESS_THN_ST_DT = "mTaskEndDtCntBLessThnStDt";
    }

    public class EmailDistributionList
    {
        public const string ERROR_MSG_ID = "mEMAILDISTRIBUTIONERROR";
        public const string EMAIL_DIST_NO_RIGHTS = "mEmailDistNorights";
        public const string SELECT_EMAIL_DIST_LIST = "mSelectEmailDistList";
        public const string SELECT_INACTIVE_EMAIL_DIST_LIST = "mSelectInactiveEmailDistList";
        public const string SELECT_ONE_EMAIL_DIST_LIST = "mSelectOneEmailDistList";
        public const string EMAIL_DIST_LIST_DEL_SUCCESS = "mEmailDistListDelSuccess";
        public const string EMAIL_DIST_LIST_ACTIVE_SUCCESS = "mEmailDistListActiveSuccess";
        public const string EMAIL_DIST_DEACTIVE_SUCCESS = "mEmailDistDeactiveSuccess";
        public const string EMAIL_DIST_LIST_ADD_SUCCESS = "mEmailDistListAddSuccess";
        public const string EMAIL_DIST_LIST_UPDATE_SUCCESS = "mEmailDistListUpdateSuccess";
        public const string EMAIL_DIST_LIST_TITLE_REQ = "mEmailDistListTitlereq";
        public const string EMAIL_DIST_LIST_COPY_SUCCESS = "mEmailDistCopySuccess";
        public const string EMAIL_DIST_LIST_IS_IN_USE = "mEmailDistListIsIsUse";
        //"Select Business Rule"
        public const string SELECT_BUSINESS_RULE = "mEmailDistSelectBusinessRule";
        //Email Distribution List Title already exists
        public const string TITLE_ALREADY_EXISTS = "mEmailDistTitleAlreadyExists";
        public const string SELECT_ACTIVE_EMAIL_DIST_LIST = "mSelectActiveDistList";
    }

    public class EmailDeliverySchedule
    {
        public const string ENTER_EMAIL_DELIVERY_TITLE = "mEnterEmailDeliveryTitle";
        public const string SELECT_EMAIL_TEMPLATE = "mSelectEmailTemplate";
        public const string SELECT_DISTRIBUTION_LIST = "mSelectDistributionList";
        public const string INVALID_EMAIL_IDS = "mInvalidEmailIds";
        public const string SELECT_LANGUAGE_EMAIL_DELIVERY = "mSelectLangEmailDel";
        public const string SELECT_SEND_TO_ALL_OR_INDIVIDUAL = "mSelectSendToAllOrIndividual";
        public const string FUTURE_DATE_REQUIRED = "mFutureDateRequired";
        public const string SELECT_DATE = "mSelectDate";
        public const string EMAIL_DELIVERY_ADDED_SUCCESS = "mEmailDeliveryAddSuccess";
        public const string EMAIL_DELIVERY_ADDED_ERROR = "mEmailDeliveryAddError";
        public const string EMAIL_DELIVERY_UPDATED_SUCCESS = "mEmailDeliveryUpdatedSuccess";
        public const string EMAIL_DELIVERY_UPDATED_ERROR = "mEmailDeliveryUpdatedError";
        //Email Delivery Approved Successfully.
        public const string EMAIL_DELIVERY_APPROVED = "mEmailDeliveryApproved";
        //Email Delivery Already Approved.
        public const string EMAIL_DELIVERY_ALREADY_APPROVED = "mEmailDeliveryAlreadyApproved";
        //Email Delivery Canceled Successfully.
        public const string EMAIL_DELIVERY_CANCELED = "mEmailDeliveryCanceled";
        //Email Delivery Already Canceled.
        public const string EMAIL_DELIVERY_ALREADY_CANCELED = "mEmailDeliveryAlreadyCanceled";
        //Email Delivery Deleted Successfully.
        public const string EMAIL_DELIVERY_DELETED = "mEmailDeliveryDeleted";
        //Only email deliveries Pending for approval, Approved and Draft can be edited.
        public const string EMAIL_DELIVERY_APPROVAL_PENDING = "mEmailDeliveryPendingApproval";
        //Email Template not approved for Site's default language.
        public const string EMAIL_TEMPLATE_NOT_APPROVED = "mEmailTemplateNotApproved";
        //Canceled job cant be approved.
        public const string CANCELED_JOB_CANT_APPROVED = "mCanceledJobCantApproved";
        //Email Delivery Title exists. Please enter another Email Delivery Title.
        public const string EMAIL_DELIVERY_TITLE_EXISTS = "mEmailDeliveryTitleExists";
        //Approved Email Delivery  can not be Deleted.
        public const string APPROVED_EMAIL_DELIVERY_CANT_DELETE = "mApprovedEmailDeliveryCantDelete";
        // This Email delivery can not be edited.
        public const string EMAIL_DELIVERY_CANT_EDITED = "mEmailDeliveryCantEdited";
        //Only recurring deliveries can be cancelled
        public const string ONLY_RECURRING_CAN_B_CANCELLED = "mOnlyRecurringCanBCancelled";

    }

    public class Assignment
    {
        public const string ERROR_MSG_ID = "mASSIGNMENTERROR";
        public const string ASSIGNMENT_DELETE_SUCCESS = "mASSIGNMENTDELETESUCCESS";
        public const string ASSIGNMENT_DELETE_ERROR = "mASSIGNMENTDELETEERROR";
        public const string ASSIGNMENT_VIEW_ERROR = "mASSIGNMENTVIEWERROR";
        //Assigments updated successfully.
        public const string ASSIGNMENT_UPDATED = "mAssignmentUpdated";
        public const string ENTER_ASSIGNMENT_NAME = "mAssignmentEnterAssignmentName";
        public const string ENTER_ASSIGNMENT_DESC = "mAssignmentEnterAssignmentDesc";
        public const string SELECT_ACTIVITY = "mAssignmentSelectActivity";
        public const string SELECT_BUSINESS_RULE = "mAssignmentSelectBusinessRule";
        //Please select activity type
        public const string SELECT_ACTIVITY_TYPE = "mAssignmentSelectActivityType";
        //        1)Please enter valid assignment days
        public const string ENTER_VALID_ASSIGNMENT_DAYS = "mAssignmentEnterValidAssignmentDays";
        //2)Please enter valid assignment due days
        public const string ENTER_VALID_DUE_DAYS = "mAssignmentEnterValidDueDays";
        //3)Please enter valid assignment expiry days
        public const string ENTER_VALID_EXPIRY_DAYS = "mAssignmentEnterValidExpiryDays";
        //4)Please enter valid new hire assignment days
        public const string ENTER_VALID_NHIRE_ASSIGNMENT_DAYS = "mAssignmentEnterValidNHireAssignmentDays";
        //5)Please enter valid new hire assignment due days
        public const string ENTER_VALID_NHIRE_DUE_DAYS = "mAssignmentEnterValidNHireDueDays";
        //6)Please enter valid new hire assignment expiry days
        public const string ENTER_VALID_NHIRE_EXPIRY_DAYS = "mAssignmentEnterValidNHireExpiryDays";
        //7)Please enter valid reassignment due days
        public const string ENTER_VALID_REASSIGN_DUE_DAYS = "mAssignmentEnterValidReassignDueDays";
        //8)Please enter valid reassignment expiry days
        public const string ENTER_VALID_REASSIGN_EXPIRY_DAYS = "mAssignmentEnterValidReassignExpiryDays";
        //9)Please enter valid reassignment days
        public const string ENTER_VALID_REASSIGNMENT_DAYS = "mAssignmentEnterValidReassignmentDays";
        //  1) Please select Activity to assign
        public const string SELECT_ACTIVITY_TO_ASSIGN = "mAssignmentSelectActivityToAssign";
        //2) Please select User(s)
        public const string SELECT_USER = "mAssignmentSelectUser";
        public const string SELECTED_HISTORY_DELETED = "mSelectedHistoryDeleted";
        public const string IMP_TO_DT_MUST_B_GRTR_THN_IMP_FRM_DT = "mImpToDtMustBGrtrThnImpFrmDt";
        public const string PLS_MAP_ALL_CSV_XSL_FIELDS = "mPlsMapAllCsvXslFields";
        public const string ERROR_WHILE_BLK_IMP_ASSGNMT = "mErrorWhileBlkImptAssgnmt";
        public const string ERROR_WHILE_UPLOADING_FILE = "mErrorWhileUploadingFile";
        public const string ASSGNMT_IMPORT_SUCCESSS = "mAssgnmtImportSuccess";
        public const string UN_ASSGNMT_IMPORT_SUCCESS = "mUnAssgnmtImportSuccess";
        public const string ASSGNMT_EXP_DT_MUST_B_GRTR_ASSGNMT_DT = "mAssgnmtExpDtMustBGrtrAssgnmtdt";
        public const string ASSGNMT_EXP_DT_CNT_B_REL_ASSGNMT_DU_DT = "mAssgnmtExpDtCntBRelAssgnmtDuDt";
        public const string REASSGNMT_EXP_DTCNT_B_REL_REASSGNMT_DU_DT = "mReAssgnmtExpDtCntBRelReAssgnmtDuDt";
        public const string REASSGNMT_EX_DT_MST_B_GRTR_THN_REASSGN_DT = "mReAssgnmtExDtMstBGrtrThnReassgnDt";
        public const string NW_HR_EXP_DT_CNT_B_SET_REL_NEW_HR_DU_DT = "mNwHrExpDtCntBSetRelNewHrDuDt";
        public const string NW_HR_EXP_DT_CNT_B_GRT_NEW_HR_ASSGN_DT = "mNwHrExpDtCntBGrtNewAssgnDt";
        public const string BLK_IMP_ASSGNMT_SCHED_SUCCESS = "mBlkImpAssgnmtSchedSuccess";
        public const string BLK_IMP_ASSGNMT_NOT_SCHED = "mBlkImpAssgnmtNotSched";
        public const string MRK_BLK_CMPLTN_NT_SCH_CHK_IMP_ERR_LOG = "mMrkBlkCmpltnNtSchChkImpErrLog";
        public const string MRK_BLK_CMPLTN_SCH_SUCCESS = "mMrkBlkCmpltnNtSchSuccess";
        public const string ASS_MRK_COMPLTD_SUCCESS = "mAssMrkCompltdSuccess";
        public const string RE_ASS_DT_MST_B_GRTR_THN_ASS_DT = "mReAsstDtMstBGrtrThnAssDt";
        public const string RE_ASS_DT_MST_B_GRTR_THN_ASS_EXP_DT = "mReAsstDtMstBGrtrThnAssExpDt";
        public const string RE_ASS_DT_MST_B_GRTR_THN_ZERO = "mReAsstDtMstBGrtrThnZero";
        public const string PLZ_ENTER_VLD_REASS_DT = "mPlzEnterVldReassDt";
        public const string PLZ_UN_CHECK_RCRDS_FOR_UN_ASSIGNMENT = "mPlzUnCheckRcrdsForUnAssignment";
        //1)       Import to date must be greater than import from date
        public const string IMP_TO_DT_MST_B_GRTR_THN_IMP_FRM_DT = "mImpToDtMstBGrtrThnimpFromDt";
        //2)       Please enter valid import from date
        public const string PLZ_ENTER_VLD_IMP_FRM_DT = "mPlzEnterVldImpFromDt";
        //3)       Please enter valid import to date
        public const string PLZ_ENTER_VLD_IMP_TO_DT = "mPlzEnterVldImpToDt";
        //4)       Select date between 1/1/1753 to 12/31/9999
        public const string SEL_DT_BET_RANGE = "mSelDtBtwnRange";
        //5)       Please select xsl or csv file for import
        public const string PLZ_SELECT_XSL_OR_CSV = "mPlzSelXlsOrCsv";
        //6)       No Records Found
        public const string NO_RECORDS_FOUND = "mNoRecordsFound";
        //7)       Selected history(s) are deleted
        public const string SELECT_HISTORY_DELETED = "mSelectHistoryDeleted";
        //8)       Not valid page size
        public const string NOT_VALID_PAGE_SIZE = "mNotValidPageSize";
        //9)       Please select record(s) those created by you only
        public const string SEL_RECORDS_CREATED_BY_YOU = "mSelRecordsCreatedByYou";
        //10)   Please select atleast one record
        public const string PLZ_SEL_ATLEAST_1_RECORD = "mPlzSelAtleast1Record";
        //11)   Are you sure you want to delete record(s)?
        public const string ASSIGNMENT_CONFIRM_DELETE = "mAssignmentConfirmDelete";
        //12)   Please enter valid assignment to date
        public const string PLZ_ENTER_VLD_ASS_TO_DT = "mPlzEnterVldAssToDt";
        //13)   Please enter valid assignment from date
        public const string PLZ_ENTER_VLD_ASS_FROM_DT = "mPlzEnterVldAssFromDt";
        //14)   Assignment to date must be greater than assignment from date
        public const string ASS_TO_DT_MST_B_GRTR_THN_ASS_FROM_DT = "mPlzEnterVldAssToDt";
        //Please enter valid completion from date 
        public const string ASS_PLZ_ENTER_VLD_COMPLTN_DT_FRM = "mPlzEnterVldCompltnDtfrm";
        //Please enter valid completion to date 
        public const string ASS_PLZ_ENTER_VLD_COMPLTN_DT_FROM = "mPlzEnterVldCompltnDtTo";
        //Completion to date must be greater than completion from date 
        public const string ASS_COMPLTN_TO_DT_MST_B_GRTR_THN_FROM_DT = "mAssCompltnToDtMstBGrtrThnFromDt";
        public const string PRODUCT_LICENSE_EXCEEDED = "mProduct_License_Exceeded";
        //# out of $ assignment(s) deaactivated
        public const string ASSIGNMENT_DEACTIVATE_SUCCESS = "mASSIGNMENTDEACTIVATESUCCESS";
        //No assignment(s) deactivated
        public const string ASSIGNMENT_DEACTIVATE_ERROR = "mASSIGNMENTDEACTIVATEERROR";
        //Activity unassigned successfully
        public const string ASSIGNMENT_UNASSIGNED_SUCCESS = "mASSIGNMENTUNASSIGNEDSUCCESS";
        //Activity unassigned successfully
        public const string ASSIGNMENT_UNASSIGNED_ERROR = "mASSIGNMENTUNASSIGNEDERROR";


    }

    public class TaskRunHistory
    {
        public const string ERROR_MSG_ID = "mTASKRUNHISTORYERROR";
    }

    public class AVPathSetting
    {
        public const string ERROR_MSG_ID = "mAVPathSettingBLError";
        //1.Please enter A/V path.
        public const string PLEASE_ENTER_AVPATH = "mEnterAVPath";
        //2.Fill the all A/V path entries for selected custom field.
        public const string FILL_ALL_AVPATH_ENTRIES = "mFillAllAVPathEntries";
        //3.Please select at least one option.
        public const string PLEASE_SELECT_OPTION = "mAVPathPleaseSelectOption";
        //4.A/V Path Settings updated Successfully.
        public const string AVPATH_UPDATED = "mAVPathUpdated";
        //â€œItems not found for selected custom fieldâ€
        public const string ITEM_NOT_FOUND = "mAVPathItemNotFoundForCF";
    }

    public class CourseConfiguration
    {
        public const string ERROR_MSG_ID = "mCOURSECONFIGURATIONERROR";
        //Course Settings updated successfully. 
        public const string UPDATED_SUCCESSFULLY = "mCourseSettingsUpdatedSuccessfully";
        //Please enter numeric value. 
        public const string ENT_NUMERIC_VALUES = "mENT_NumericValues";
        //Course Launching Screen Settings updated successfully. 
        public const string COURSE_LAUNCH_UPDATED_SUCCESSFULLY = "mCourseLaunchUpdatedSuccessfully";
        //Please enter Height and/or Width numeric. 
        public const string ENT_HEIGHT_WIDTH = "mENT_HeightAndOrWidth";
        //A/V Path Setting updated successfully. 
        public const string AV_PATH_UPDATED_SUCCESSFULLY = "mAVPathUpdatedSuccessfully";
        //Please enter A/V Path
        public const string ENT_AV_PATH = "mENT_AVPath";
    }

    public class RSSFeedConfiguration
    {
        public const string ERROR_MSG_ID = "mRSSFEEDCONFIGURATIONERROR";
        public const string INVALID_RSS_FEED_URL = "mRSSInvalidURL";
        public const string UPDATED_SUCCESSFULLY = "mRSSFeedUpdatedSuccesfully";
    }

    public class ContentModuleAllocationLicensing
    {
        public const string DL_ERROR = "mContentModuleAllocationLicensingDATAERR";
        public const string SELECT_COURSE = "mSelectCourse";
        public const string SELECT_SUBSCRIPTION = "mSelectSubscription";
        public const string SUBSCRIPTION_DELETED_SUCCESS = "mSubscriptionDeletedSuccess";
        //Selected subscription cannot be deleted
        public const string SUBSCRIPTION_CANT_DELETED = "mSubcriptionCantDeleted";
        //Not valid Expiry Date.
        public const string NOT_VALID_EXPIRY_DATE = "mNotValidExpiryDate";
        //3. * Please enter number of license purchased
        public const string ENT_NUMOF_LICENSE_PURCHASED = "mNumOfLicensePurchased";
        //4. * Date range must be in between 1/1/1753 to 12/31/9999
        public const string DATE_RANGE_MUST_BE = "mDateRangeMustBe";
        //5. * License purchased must be numeric and greater than 0
        public const string LICENSE_PURCHASED_MUST_BE = "mLicensePurchaseMustBe";
        //Allocated courses cannot be deleted.
        public const string ALLOCATED_COURSES_CANT_DELETED = "mAllocatedCoursesCantDeleted";
        //Course Assignment date should be greater than License Allocation date 
        public const string ASSIGNMENTDT_SHLD_GRTN_ALLOCATIONDT = "mAssignmentDtShldGrtnAllocationDate";
        //Course Expiry date should not be greater than License Expiry date 
        public const string COUEXPIRYDT_SHLD_LSTN_LICENSE_EXPIRYDT = "mCouExpiryDtShldlstnLicenseExpiryDate";

        //Subscription added successfully 
        public const string SUBSCRIPT_ADDED_SUCCESS = "mSubScriptAddedSucces";

        //No default course found
        public const string NO_DEFAULT_COURSE_FOUND = "mNODefaultCourseFound";
        //Please set as Default Course before saving.
        public const string PLZ_SET_DEFAULT_COURSE = "mPlzSetDefaultCourse";
        //Default course is added successfully.
        public const string DEFAULT_COURSE_ADD_SUCCESS = "mDefaultCourseAddSuccess";
        //Default course is updated successfully.
        public const string DEFAULT_COURSE_UPDATE_SUCCESS = "mDefaultCourseUpdateSuccess";
        //Enter Body Message
        public const string ENTER_BODY_MESSAGE = "mEnterBodyMessage";

    }

    public class ContentModule
    {
        public const string UNAUTHORIZED_VIEW_COURSE = "mUnWuthorizedViewCourse";
        public const string INVALID_AICC_COURSE_CONTACT_ADMIN = "mInvalidAICCCourseContactAdmin";
        public const string COULD_NOT_FIND_MANIFEST_FILE = "mCouldNotFindManifestFile";
        //Course uploaded successfully.
        public const string COURSE_UPLOADED = "mContModuleCourseUploaded";
        //Course upload failed.
        public const string COURSE_UPLOAD_FAILED = "mContModuleCourseUploadFailed";
        //Invalid AICC course. Please check the uploaded course.
        public const string INVALID_AICC_COURSE = "mContModuleInvalidAICCCourse";
        //No vaild manifest file found. Please check the uploaded course.
        public const string INVALID_MANIFEST_FILE = "mContModuleInvalidManifestFile";
        //Course unzip failed.
        public const string COURSE_UNZIP_FAILED = "mContModuleCourseUnZipFailed";
        //Course partially updated.
        public const string COURSE_PARTLY_UPDATED = "mContModuleCoursePartlyUpdated";
        //Course partially added.
        public const string COURSE_PARTLY_ADDED = "mContModuleCoursePartlyAdded";
        //Course updated successfully.
        public const string COURSE_UPDATED = "mContModuleCourseUpdated";
        //Course Added successfully.
        public const string COURSE_ADDED = "mContModuleCourseAdded";
        //Course updation failed.
        public const string COURSE_UPDATION_FAILED = "mContModuleCourseUpdationFailed";
        //Course Addition failed.
        public const string COURSE_ADDITION_FAILED = "mContModuleCourseAdditionFailed";
        //Course Deleted Successfully.
        public const string COURSE_DELETED = "mContModuleDeleted";
        //Course Deletion Failed.â€
        public const string COURSE_DELETION_FAILED = "mContModuleDeleteFailed";
        //â€œCourse currently in use, deletion failed. â€
        public const string COURSE_USED_DELETION_FAILED = "mContModuleInUsed";
        //â€œCourse Activated successfully.â€
        public const string COURSE_ACTIVATED = "mContModuleActivated";
        //â€œCourse Activation failed.â€
        public const string COURSE_ACTIVAT_FAILED = "mContModuleActiveFailed";
        //Selected course(s) are deactivated.
        public const string COURSES_DEACTIVATED = "mCoursesDeactivated";
        //Selected course(s) are deactivation failed.
        public const string COURSES_RIGHTS = "mContModuleNoRightsForCourse";
        public const string COURSES_DEACTIVATION_FAILED = "mCoursesDeactivationFailed";
        //Please select only one course
        public const string SELECT_ONE_COURSE = "mSelectOneCourse";
        //This will update existing course content. Do you want to proceed?  
        public const string UPDATE_COURSE_CONTENT = "mUpdateCourseContent";

        public const string SELECT_ASSIGN_COURSE = "mSelectAssignCourse";
        //Please select course.
        public const string SELECT_UPLOADXML = "mUploadXML";
        //Please select upload xml.
        public const string UPLOADXML_CONFIRM_MESG = "mUploadXMLConfirmMessage";
        //Are you sure you want to upload xml file?
        public const string SELECT_VALID_FILE_UPLOAD = "mSelectValidFileUpload";
        //Please select valid file to upload
        public const string COURSE_TRACKING_UPLOADED = "mCourseTrackingUploaded";
        //Course Traking uploaded sucessfully.

        public const string COURSE_NAME = "mCourseName";
        //Course Name.
        public const string UPLOAD_XML = "mUpload_XML";
        //UPLOAD XML.
        public const string SELECT_COURSE = "mSelectTrackCourse";
        //Select Course
        public const string UPLOAD = "mUpload";
        //Upload
        public const string OFFLINECOURSEFORM_PAGEHEADING = "mofflineCourseFormPageHeading";

        public const string COURSE_INTERNET_CONNECTION_ERROR = "mCourseInternetConnectionError";
        public const string COURSE_ASSESSMENT_UPLOAD_PROBLEM = "mContModuleAssessmentUploadProblem";
        public const string COURSE_ASSESSMENT_LOCK = "CourseAssessmentLock";
        public const string ASSIGNMENT_UNLOCK = "mAssignmentUnlock";
    }

    public class PWDPolicyConfiguration
    {
        //Please Enter Confirm Password Same As Default Password.
        public const string ENT_CONF_PWD_AS_DEF_PWD = "mENT_ConfirmPWDAsDefaultPWD";
        //Password Must Conatain Atleast One Upper Case Character.
        public const string PWD_MUST_CONT_UCASE_CHAR = "mPWDMustContainUCaseChar";
        //Password Must Conatain Atleast One Lower Case Character.
        public const string PWD_MUST_CONT_LCASE_CHAR = "mPWDMustContainLCaseChar";
        //Password Must Conatain Atleast One Special Character.
        public const string PWD_MUST_CONT_SPECIAL_CHAR = "mPWDMustContainSpeChar";
        //Password Must Conatian Atleast One Number.
        public const string PWD_MUST_CONT_NUMBER = "mPWDMustContainNumber";
        //Please Enter Minimum Password length.
        public const string ENT_PWD_MIN_LENGHT = "mENT_MinimunPWDLength";
        //Please Enter Maximum Password length.
        public const string ENT_PWD_MAX_LENGHT = "mENT_MaxPWDLength";
        //Password Length Should be Maximum
        public const string PWD_MAX_LENGHT = "mMaxPWDLength";
        //Password Length Should be Minimum
        public const string PWD_MIN_LENGHT = "mMinPWDLength";
        //Please Enter Positive Numbers only.
        public const string ENT_POSITIVE_NUMS_ONLY = "mENT_PositiveNumbersOnly";
        //Please Enter A Default Password.
        public const string ENT_DEFAULT_PWD = "mENT_DefaultPWD";
        //Please Enter A Confirm Password.
        public const string ENT_CONFIRM_PWD = "mENT_ConfirmPWD";
        //Password policy saved successfully.
        public const string POLICY_SAVED_SUCCESSFULLY = "mPolicySavedSuccessfully";
        public const string MAX_PWD_ERROR = "mMaxPwdLenMustBeGrtrThanMin";
        public const string PWD_SPACE_NOT_ALLOWED = "mPwdSpaceNotAllowed";
        //Password should not content blank space
        public const string SPACE_NOT_ALLOWED_IN_PWD = "mSpaceNotAllowedInPWD";
        public const string MIN_PWD_LEN_MST_B_GRTR_THN_ZERO = "mMinPwdLenMstBGrtrThnZero";
        public const string MAX_PWD_LEN_MST_B_GRTR_THN_ZERO = "mMaxPwdLenMstBGrtrThnZero";
        //Minimum password length should be configured more than 3 characters
        public const string MIN_PWD_LEN_SHUD_B_MORE_THAN_3 = "mMinPwdLenShudBMoreThan3";
        //Maximum password length can be configured up to 12 characters only
        public const string MAX_PWD_LEN_CAN_B_UPTO_12 = "mMaxPwdLenCanBUpto12";
    }

    public class UserPage
    {
        public const string SELECT_PAGE = "mSelectPage";
        public const string SELECT_PAGE_ELEMENT = "mSelectPageElement";
        public const string SELECT_PAGE_ELEMENT_TEXT = "mSelectPageElementText";
        public const string USER_PAGE_UPDATED_SUCCESS = "mUserPageUpdatedSuccess";
        public const string SELECT_UPLOAD_IMAGE = "mSelectUploadImage";
        public const string INVALID_FILE_EXTENSION = "mInvalidFileExtension";
        public const string FILE_UPLOAD_SUCCESS = "mFileUploadSuccess";
        public const string BL_ERROR = "mUserPageGetError";
    }

    public class UserPassCodeInstance
    {
        public const string USER_PASS_INSTANCE_CODE_ERROR = "mUSERPASSCODEINSTANCEERROR";
        public const string USER_PASS_INSTANCE_DELETE_SUCCESS = "mUSERPASSCODEINSTANCEDELETESUCCESS";
        public const string USER_PASS_INSTANCE_DELETE_ERROR = "mUSERPASSCODEINSTANCEDELETEERROR";
        public const string USER_PASS_INSTANCE_DEACTIVATE_SUCCCESS = "mUSERPASSCODEINSTANCEDEACTIVATESUCCESS";
        public const string USER_PASS_INSTANCE_DEACTIVATE_ERROR = "mUSERPASSCODEINSTANCEDEACTIVATEERROR";
        public const string USER_PASS_INSTANCE_EDIT_ERROR = "mPassCodeInstanceEditError";
        public const string PASSCODE_NO_EMAIL_ERROR = "mPassCodeNoEmailError";
        public const string MAIL_SENT_SUCCESS = "mMailSentSuccess";
        public const string MAIL_SENT_ERROR = "mMailSentError";
        public const string PASSCODE_TITLE_ALREADY_IN_USE = "mPassCodeTitleAlreadyInUse";
    }

    public class EmailTemplate
    {
        public const string DL_ERROR = "mEMAILTEMPLATEEERROR";
        public const string SELECT_TEMPLATE = "mEMAILTEMPSELTEMPLATE";
        public const string SELECT_ALREADY_APPROVE = "mEMAILTEMPSELTEMPLATEALREADYAPPROVE";
        public const string SELECT_ALREADY_SUBMITTED_FOR_APPROVE = "mEMAILTEMPSELTEMPLATEALREADYSUBMITTEDFORAPPROVE";
        public const string SELECT_SUBMITTED_FOR_APPROVE = "mEMAILTEMPSELTEMPLATESUBMITTEDFORAPPROVE";
        public const string GET_TEMPLATE_APPROVED_BEFORE_TRANSLATION = "mEMAILTEMPGETTEMPLATEAPPROVEDBEFORETRANSLATION";
        public const string DELETED_SUCCESSFULLY = "mEMAILTEMPDELETEDSUCCESSFULLY";
        public const string APPROVED_SUCCESSFULLY = "mEMAILTEMPAPPROVEDSUCCESSFULLY";
        public const string CAN_NOT_DELETED_SUCCESSFULLY = "mEMAILTEMPCANNOTDELETEDSUCCESSFULLY";
        public const string COPIED_SUCCESSFULLY = "mEMAILTEMPCOPIEDSUCCESSFULLY";
        public const string CAN_NOT_MAKE_COPY_SELECTED_TEMPLATE = "mEMAILTEMPCANNOTMAKECOPYSELECTEDTEMPLATE";
        public const string SHARED_SUCCESSFULLY = "mEMAILTEMPSHAREDSUCCESSFULLY";
        public const string ALREADY_SHARED = "mEMAILTEMPALREADYSHARED";
        public const string DEACTIVATED_SUCCESSFULLY = "mEMAILTEMPDEACTIVATEDSUCCESSFULLY";
        public const string ALREADY_DEACTIVATED = "mEMAILTEMPALREADYDEACTIVATED";
        public const string SELECT_EXCEL_FILE = "mEMAILTEMPSELECTEXCELFILE";
        public const string IMPORTED_SUCCESSFULLY = "mEMAILTEMPIMPORTEDSUCCESSFULLY";
        public const string NOT_APPROVED = "mEMAILTEMPNOTAPPROVED";
        public const string INVALID_APPROVERS_EMAIL_ID = "mEMAILTEMPINVALIDAPPROVERSEMAILID";
        public const string PRIVILEGES_EDIT_TEMPLATE = "mEMAILTEMPPRIVILEGESEDITTEMPLATE";
        public const string INVALID_APPROVERS_EMAIL_ID_UPDATED_EMAIL = "mEMAILTEMPINVALIDAPPROVERSEMAILIDUPDATEDEMAIL";
        public const string INVLD_CNTNT_PLZ_EXPT_TOVIEW_SAMP_FL_FRMT = "mInvldCntPlzExptToViewSampFlFrmt";
        public const string INVLD_MPG_PLZ_SEL_ATLST_1_FIELD = "mInvldMpgPlzSelAtlst1Field";
        public const string EMAIL_TEMP_IMP_SUCCESS = "mEmailTempImpSuccess";
        public const string EMAIL_TEMP_TRY_DIFFRNT_NAME = "mEmailTempTryDiffrntName";
        public const string APPR_EMAIL_TEMP_AND_CONFIRM_TO_CONC_PERSON = "mApprEmailTempAndConfirmToConcPerson";
        public const string ONLY_ACTIVE_EMAIL_TEMP_CAN_B_MARK_APPR = "mOnlyActiveEmailTempCanBMarkAppr";
        public const string EMAIL_TEMP_CNT_B_DELETED_ITS_IN_USE = "mEmailTempCntBDeleteditsInUse";
        public const string DEL_EMAIL_TEMP_OTH_LANG_B4_DEL_BASE_EMAIL_TEMP = "mDelEmailTempOtherLangB4DelBaseEmailTemp";
        public const string OWNER_AND_ST_ADMIN_CAN_DEL = "mOwnerAndStAdminCanDel";
        public const string BASE_EMAIL_TMP_CNT_B_DACTVTD = "mBaseEmailTmpCntBDactvtd";
        public const string BASE_EMAIL_TMP_CNT_B_DEL = "mBaseEmailTmpCntBDel";
        //Please deactivate the email template before deleting 
        public const string DEACTIVATE_BEFORE_DELETING = "mDeactivateEmailTmpBeforeDelete";
        //Please send the email template for approval before making it as approved 
        public const string SEND_FOR_APPROVAL_BEFORE_APPROVED = "mSendForApprovalBeforeApproved";

        //System default email template cannot be deleted. 
        public const string DEFAULT_DELETE = "mDefaultDeleteCheck";
        //System default email template cannot be deactivated. 
        public const string DEFAULT_DEACTIVATE = "mDefaultDeactivateCheck";
    }

    public class QuestionnaireSessionResponses
    {
        public const string QUESTIONNAIRE_USER_SESSION_RESPONSE_ERR = "mUserSessionResponseError";
        public const string QUESTIONNAIRE_ADMIN_SESSION_RESPONSE_ERR = "mAdminSessionResponseError";
    }

    public class AssessmentSessionResponses
    {
        public const string ASSESSMENT_USER_SESSION_RESPONSE_ERR = "mUserSessionResponseError";
        public const string ASSESSMENT_ADMIN_SESSION_RESPONSE_ERR = "mAdminSessionResponseError";
    }

    public class QuestionnaireTracking
    {
        // Error in User questionnaire Traking
        public const string QUESTIONNAIRE_USER_TRACKING_ERR = "mUserQuestionnaireTrackingError";
        public const string QUESTIONNAIRE_ADMIN_TRACKING_ERR = "mAdminQuestionnaireTrackingError";
    }

    public class AssessmentTracking
    {
        // Error in User Assessment Traking
        public const string ASSESSMENT_USER_TRACKING_ERR = "mUserAssessmentTrackingError";
        public const string ASSESSMENT_ADMIN_TRACKING_ERR = "mAdminAssessmentTrackingError";
    }

    public class AutoEmailTemplateSetting
    {
        public const string AUTO_EMAIL_TEMPLATE_ERR = "mAutoEmailTemplateError";
        //1.You do not have rights to view this page
        public const string NO_VIEW_RIGHTS = "mAutoEmailTemplateNoViewRights";
        //2. Email template settings updated successfully
        public const string UPDATED_SUCCESSFULLY = "mAutoEmailTemplateUpdatedSuccessfully";
    }

    public class CurriculumPlan
    {
        public const string MAX_LIMIT_EXCEED = "mMaxLimitExceed";
        public const string LAST_ROW_REACHED = "mLastRowReached";
        public const string FIRST_ROW_REACHED = "mFirstRowReached";
        public const string SELECT_ATLEAST_ONE_RECORD = "mSelectAtleastOneRecord";
        public const string SELECTED_CURRICULUM_PLAN_USED = "mSelectedCurriculumPlanIsInUse";
        public const string SELECTED_CURRICULUM_PLAN_USED_ACTIVITIES = "mSelectedCurriculumPlanIsInUseActivities";
        public const string RECORD_ALREADY_DEACTIVATED = "mCurriculumPlanRecordAlreadyDeactivated";
        public const string CURRICULUM_DEACTIVATE_SUCCESS = "mCurriculumDeactivateSuccess";
        public const string CURRICULUM_NAME_ALREADY_EXISTS = "mCurriculumNameAlreadyExists";
        public const string CURRICULUM_DL_ERROR = "mCurriculumDataError";
        public const string ADMIN_CURR_TRACKING_ERROR = "mAdminCurriculumTrackingDataError";
        public const string CURRICULUM_NO_RIGHTS = "mCurriculumNoRights";
        public const string INVALID_CURRICULUM_NAME= "mInvalidCurriculumName";
        //Activity Name Should Not be Blank
        public const string ACTIVITY_NAME_REQUIRED = "mCurriculumActivityNameRequired";
        public const string SELECT_ATLEAST_SINGLE_ACTIVITY = "mSelectAtleastSingleActivity";
        public const string CURRICULUM_PLAN_DELETE_SUCCESS = "mCurriculumPlanDeleteSuccess";
        public const string CURRICULUM_PLAN_COPY_SUCCESS = "mCurriculumPlanCopySuccess";
        public const string CURRICULUM_PLAN_UPDATE_SUCCESS = "mCurriculumPlanUpdateSuccess";
        public const string CURRICULUM_PLAN_ADD_SUCCESS = "mCurriculumPlanAddSuccess";
    }

    public class Certification
    {
        public const string CERTIFICATION_NAME_REQ = "mCertificationNameReq";
        public const string QUESTIONAIRE_REQ = "mQuestionaireReq";
        public const string SELECT_ITEM_TO_MOVE = "mSelectItemToMove";
        public const string ITEM_EXISTS = "mItemAlreadyExists";
        public const string RECORD_ALREADY_DEACTIVATED = "mRecordAlreadyDeactivated";
        public const string DELETE_CONFIRM = "mCertificationDeleteConfirm";
        //Certification deactivated successfully
        public const string DEACTIVATE_SUCCESS = "mCertificationDeactivateSuccess";
        //Certification is in use.     
        public const string USED_CERTIFICATION = "mUsedCertification";

        public const string USED_CERTIFICATION_ACTIVITIES = "mUsedCertificationActivities";
        //Please enter valid URL/Link.
        public const string ENT_VALID_URL_LINK = "mENTValidURLOrLink";
        public const string CERT_COPY_SUCCESS = "mCertCopySuccess";
        public const string IN_APPROPRIATE_RIGHTS = "mInAppropriateRights";
        public const string CERTIFICATION_IN_USE = "mCertificationInUse";
        public const string CERTIFICATION_DEL_SUCCESS = "mCertificationDelSuccess";
        public const string CERTIFICATION_ADD_SUCCESS = "mCertificationAddSuccess";
        public const string CERTIFICATION_ADD_FAIL = "mCertificationAddFail";
        public const string CERTIFICATION_UPDATE_SUCCEDD = "mCertificationUpdateSuccess";
        public const string CERTIFICATION_UPDATE_FAIL = "mCertificationUpdateFail";
        //Please select certification 
        public const string PLZ_SELECT_CERTIFICATION = "mPlzSelectCertification";
        //Not a valid From Date 
        public const string CERT_NOT_VALID_FROM_DATE = "mCertNotValidFromDate";
        //Certification Name already Exists
        public const string CERT_NAME_ALREADY_EXISTS = "mCertNameAlreadyExists";
        //Please select category name
        public const string CERT_PLZ_SELECT_CATEGORY_NAME = "mCertPlzSelectCategoryName";
        public const string CERT_INFO_ERROR = "mCertificationInfoError";
        public const string CERTIFICATION_DEL_FAIL = "mCertificationDelError";
    }

    public class LiveSession
    {
        public const string DL_ERROR = "mLiveSessionDataError";
        public const string NOT_STARTED = "mLiveSessionNotStarted";
        public const string NOT_ACTIVE = "mLiveSessionNotActive";
        public const string UNABLE_TO_START = "mUnableToStart";
    }
    public class SocialIntegration
    {
        public const string DL_ERROR = "mSocialIntegrationDataError";
    }
    public class Report
    {
        public const string DL_ERROR = "mReportDataError";
        public const string INVALID_EXPIRY_START_DT = "mInvalidExpiryStartDate";
        public const string INVALID_EXPIRY_END_DT = "mInvalidExpiryEndDate";
        public const string INVALID_EXPIRY_DATES = "mInvalidExpiryDates";
        public const string INVALID_DUE_START_DT = "mInvalidDueStartDate";
        public const string INVALID_DUE_END_DT = "mInvalidDueEndDate";
        public const string INVALID_DUE_DATES = "mInvalidDueDates";
        public const string INVALID_ASSIGNMT_START_DT = "mInvalidAssignMTStartDate";
        public const string INVALID_ASSIGNMT_END_DT = "mInvalidAssignMTEndDate";
        public const string INVALID_ASSIGNMT_DATES = "mInvalidAssignMTDates";
        public const string ENTER_ACTIVITY_NAME = "mEnterActivityName";
        public const string SELECT_ACTIVITY_TYPE = "mSelectActivityType";
        public const string SELECT_ORG_HIERARCHY = "mSelectOrgHierarchy";
        public const string DISPLAY_FIELD_DEF_SUCCEDD = "mDisplayFieldDefSuccess";
        //Assignment From Date not valid
        public const string RPT_INVLD_ASS_FROM_DT = "mRptInvldAssFromDt";
        //Assignment To Date not valid
        public const string RPT_INVLD_ASS_TO_DT = "mRptInvldAssToDt";
        //Assignment To Date must be greater than Assignment From Date
        public const string RPT_ASS_TO_DT_MST_B_GRTR_THN_FROM = "mRptAssToDtMstBGrtrThnFrm";
        // Hire From Date not valid
        public const string RPT_HIRE_FRM_DT_NOT_VALID = "mRptHireFrmDtNotValod";
        //Hire To Date not valid
        public const string RPT_HIRE_TO_DATE_NOT_VALID = "mRptHireToDateNotValid";
        //Hire To Date must be greater than Hire From Date
        public const string RPT_HIRE_TO_DATE_MST_B_GRTR_THN_HIRE_FRM = "mRptHireToDateMstBGrtrThnHireFrom";
        //    Creation From Date not valid
        public const string RPT_INVALID_CREATION_FROM_DATE = "mRptInvalidCreationFromDate";
        //    Creation To Date not valid
        public const string RPT_INVALID_CREATION_TO_DATE = "mRptInvalidCreationToDate";
        //Creation To Date must be greater than Creation From Date
        public const string RPT_CRTN_TO_MST_B_GRTR_THN_FROM = "mRptCrtnToMstBGrtrThnFrom";
        //Error From Date not valid
        public const string RPT_INVALID_ERROR_FROM_DT = "mRptInvalidErrorFromDt";
        //Error To Date not valid
        public const string RPT_INVALID_ERROR_TO_DATE = "mRptInvalidErrorToDate";
        //    Error To Date must be greater than Error From Date
        public const string RPT_ERROR_TO_MST_B_GRTR_THN_FROM = "mRptErrorToMstBGrtrThnFrom";

        public const string INVALID_Completion_DATES = "mInvalidCompletionDates";
        public const string INVALID_Completion_END_DT = "mInvalidCompletionEndDate";
        public const string INVALID_Completion_START_DT = "mInvalidCompletionStartDate";
    }

    public class RuleRoleScope
    {
        public const string ERROR_MSG_ID = "mRuleRoleScopeError";
    }

    public class UserPageElement
    {
        public const string BL_ERROR = "mUserPageElementBLError";
        // Error occurred while importing Site Page Settings.
        public const string ERROR_IN_PAGE_SETTING_IMPORT = "mErrorInPageSettingImport";
        //Site Page Settings imported to system successfully.
        public const string PAGE_SETTING_IMPORTED = "mPageSettingImported";
        // site maintenance message
        public const string SYSTEM_MAINTENANCE_MESSAGE = "mSystemMaintenanceMessage";
    }

    public class BusinessRuleUsers
    {
        //Business Rule Users BL Error
        public const string BL_ERROR = "mBusinessRuleUsersBLError";
        // Please select group.
        public const string SELECT_GROUP = "mSelectGroup";
        //Please add at least single condition
        public const string ADD_LEAST_SINGLE_CONDITION = "mAddLeastSingleCondition";
        //Rule updated successfully 
        public const string RULE_UPDATED_SUCCESS = "mRuleUpdatSuccess";
        //Activated # Rule(s) 
        public const string ACTIVATED_RULE = "mActivatedRule";
        //Deactivated # Rule(s) 
        public const string DEACTIVATED_RULE = "mDeactivatedRule";
        //Business Rule(s) # Deleted Sucessfully
        public const string BUSINESS_RULE_DEL_SUCESS = "mBusinessRuleDelSucess";
        //You can not edit this rule 
        public const string CAN_NOT_EDIT_RULE = "mCanNotEditRule";
        //Rule copied successfully
        public const string RULE_COPIED_SUCCESS = "mRuleCopiedSuccess";
        //Rule added successfully 
        public const string RULE_ADDED_SUCCESS = "mRuleAddedSuccess";
        //1) Please select inactive rules only
        public const string SELECT_INACTIVE_RULE = "mSelectInActiveRuleOnly";
        //2) Please select active rules only
        public const string SELECT_ACTIVE_RULE = "mSelectActiveRuleOnly";
        //No records deleted
        public const string NO_REC_DEL = "mNoRecDel";
        //No records deactivated
        public const string NO_REC_DEACTIVATED = "mNoRecDeactivated";
        //Rule name already exists
        public const string RULE_NAME_EXIST = "mRuleNameExist";
        //You dont have permission to # Edit Rule
        public const string NOT_PERMISSION_EDIT_RULE = "mNotPermissionEditRule";
        //Please select at least one condition
        public const string SELECT_ATLEAST_ONE_CONDITION = "mSelectAtleastOneCondition";
    }

    public class UserControlGrid
    {
        //Are you sure to delete this record(s)
        public const string GRID_DELETE_CONFIRMATION = "mGridDeleteConfirmation";
        // Please select InActive Clients
        public const string GRID_SELECT_INACTIVE_CLIENTS = "mGridSelectInActiveClients";
        //Please Select At Least One Record
        public const string GRID_SELECT_LEAST_ONE_RECORD = "mGridSelectLeastOneRecord";
        //Please Select Only One Record
        public const string GRID_SELECT_ONLY_ONE_RECORD = "mGridSelectOnlyOneRecord";

        //Are you sure to activate this record(s)?
        public const string BVGRID_CONFIRM_ACTIVATE = "mBVGridConfirmActivate";
        //Are you sure to deactivate this record(s)?
        public const string BVGRID_CONFIRM_DEACTIVATE = "mBVGridConfirmDeActivate";
        //Are you sure to delete this record(s)?
        public const string BVGRID_CONFIRM_DELETE = "mBVGridConfirmDelete";
        //Editing the approved template will make its status to Draft,\n Click OK to proceed?
        public const string BVGRID_EDITING_MAKES_STATUS_TO_APPRVD = "mBVGridEditingMakesStatusToApprv";
        //Please select records those created by you only.
        public const string BVGRID_RCDS_CREATED_BY_U = "mBVGridRcdsCreatedByU";
        //Please select one active record.
        public const string BVGRID_SELECT_ACTIVE_RCD = "mBVGridCSelectActiveRcd";
        //Record is already activated.
        public const string BVGRID_RCD_ALREADY_ACTIVE = "mBVGridRecordAlreadyActive";
        //Please select only inactive record(s).
        public const string BVGRID_ONLY_INACTIVE_RCD = "mBVGridOnlyInActiveRcd";
        //Record is already deactivated.
        public const string BVGRID_RCD_ALREADY_DEACTIVE = "mBVGridRcdAlreadyDeactive";
        //Please select only active record(s).
        public const string BVGRID_ONLY_ACTIVE_RCD = "mBVGridOnlyActiveRcd";
        //Please select not shared record(s).
        public const string BVGRID_SEL_SHARED_RCD = "mBVGridSelSharedRcd";
        //Are you sure to delete this record(s)?
        public const string BVGRID_CONFIRM_UNMAP = "mBVGridConfirmUnmap";
    }

    public class StandardReport
    {
        public const string STANDARD_REPORT_ERROR = "mStandardReportError";
        public const string STANDARD_REPORT_SEL = "mSelectStandardReport";
    }

    public class StandardCustomReport
    {
        public const string STANDARD_CUST_REPORT_ERROR = "mStandardCustomReportError";
        public const string STANDARD_CUST_REPORT_EDIT_NO_RIGHT = "mStandardCustomReportEditNoRight";
        public const string STANDARD_CUST_REPORT_COPY_NO_RIGHT = "mStandardCustomReportCopyNoRight";
        public const string STANDARD_CUST_REPORT_COPY_SUCCESS = "mStandardCustomReportCopySuccess";
        public const string STANDARD_CUST_REPORT_DELETE_SUCCESS = "mStandardCustomReportDeleteSuccess";
        public const string STANDARD_CUST_REPORT_SHARED_SUCCESS = "mStandardCustomReportSharedSuccess";
        public const string STANDARD_CUST_REPORT_ENTER_NAME = "mStandardCustomReportEnterName";
        public const string STANDARD_CUST_REPORT_ENTER_PAGESIZE = "mStandardCustomReportEnterPageSize";
        public const string STANDARD_CUST_REPORT_PAGE_SIZE_BTWN_1_TO_99999 = "mStandardCustomReportPageBtwn1To99999";
        public const string FIRSTSORT_SECONDSORT_SHUD_B_DIFFERENT = "FirstSortSecondSortShudBDifferent";
        public const string STANDARD_CUST_REPORT_SEL_ATLEAST_SINGLE_COL = "mStandardCustomReportSelAtleastsingleCol";
        public const string STANDARD_CUST_REPORT_ALREADY_SHARED = "mStandardCustomReportAlreadyShared";
        public const string STANDARD_CUST_REPORT_COL_BLANK_ERR = "mStandardCustomReportColBlankErr";
        public const string STANDARD_CUST_REPORT_NAME_ALRDY_EXSTS = "mStandardCustomReportNameAlrdyExists";
    }

    public class CourseBranding
    {
        public const string COURSE_BRANDING_SUCCESS = "mCourseBrandingSuccess";
        public const string COURSE_BRANDING_UNZIP_FAILED = "mCourseBrandingUnzipFailed";
        public const string COURSE_BRANDING_UPLOAD_FAILED = "mCourseBrandingUploadFailed";
        public const string COURSE_BRANDING_UPLOAD_FILE = "mCourseBrandingUploadFile";
        public const string COURSE_BRANDING_INVALID_FILE = "mCourseBrandingInvalidFile";
    }

    public class BulkImport
    {
        public const string ERROR_MSG_ID = "mBulkImportMasterError";
    }

    public class BulkMarkCompleted
    {
        // â€œError while importing Bulk Assignment Completion.â€
        public const string IMPORT_ERROR = "mBulkMarkCompletionImportError";
        //â€œBulk import assignment completion done successfully.â€
        public const string BULK_MARK_COMPLETED = "mBulkMarkCompletedSuccessfully";
    }

    public class UserImportLog
    {
        public const string ERROR_MSG_ID = "mUserImportLogError";
    }

    public class MasterTaskJob
    {
        public const string ERROR_MSG_ID = "mMasterTaskJobError";
    }
    public class MasterContentsReader
    {
        public const string ERROR_MSG_ID = "mMasterTaskJobError";
    }
    public class CourseAssessmentQuestion
    {
        public const string ERROR_MSG_ID = "mAssessmentQuestionError";
    }
    public class UserAssessmentQuestionTracking
    {
        public const string ERROR_MSG_ID = "mUserAssessmentQuestionTrackingError";
    }
    public class UserSessionAttendence
    {
        public const string UserSessionAttendence_DAM_ERROR = "mUserSessionAttendenceERROR";
        public const string UserSessionAttendence_SUCCESS = "mUserSessionAttendenceSuccess";
        public const string UserSessionAttendence_FAIL = "mUserSessionAttendenceFail";

        public const string USERSESSION_PLEASE_SELECT_ATLEAST_ONE_LEARNER = "mUserSessionPleaseSelectAtLeastOneLearner";
    }
    public class UserSessionFeedback
    {
        public const string UserSessionFeedback_DAM_ERROR = "mUserSessionFeedbackERROR";
    }
    public class UserSessionAttendenceDays
    {
        public const string UserSessionAttendenceDays_DAM_ERROR = "mUserSessionFeedbackERROR";

    }
    public class SessionAllocatedSpeakers
    {
        public const string SessionAllocatedSpeakers_DAM_ERROR = "mSessionAllocatedSpeakersERROR";
    }
    public class SessionAllocatedResources
    {
        public const string SessionAllocatedResources_DAM_ERROR = "mSessionAllocatedResourcesERROR";
        public const string SESSIONALLOCATEDRESOURCES_ADD_SUCCESS = "mSessionAllocatedResourcesAddSuccess";
        public const string SESSIONALLOCATEDRESOURCES_ADD_FAILED = "mSessionAllocatedResourcesAddFailed";
        public const string SESSIONALLOCATEDRESOURCES_REMOVE_SUCCESS = "mSessionAllocatedResourcesRemoveSuccess";
        public const string SESSIONALLOCATEDRESOURCES_UNMAP_SUCCESS = "mSessionAllocatedResourcesUnmapSuccess";
    }
    public class InstructorMaster
    {
        public const string INSTRUCTORMASTER_DAM_ERROR = "mInstructorERROR";
        public const string INSTRUCTOR_ADD_SUCESS = "mInstructor_Add_Sucess";
        public const string INSTRUCTOR_IS_In_USE = "mInstructor_Is_In_Use";
        public const string INSTRUCTOR_SELECT = "mInstructor_Select";
        public const string INSTRUCTOR_DELETE_SUCESS = "mInstructor_Delete_Sucess";
        public const string INSTRUCTOR_DELETE_ERROR = "mInstructor_Delete_Error";
        public const string INSTRUCTOR_UPDATE_SUCESS = "mInstructor_Update_Sucess";
        public const string INSTRUCTOR_NAME_ALREADY_EXISTS = "mInstructorAlreadyExists";
        public const string INSTRUCTOR_DEACTIVATE_FAILED = "mInstructorDeactivate_Fail";
        public const string INSTRUCTOR_DEACTIVATED = "mInstructorDeactivate";
        public const string PLZ_SEL_ATLEAST_1_INSTRUCTOR = "mAtleastOneInstructor";
        public const string INSTRUCTOR_ACTIVATE_FAILED = "mInstructorActivate_Fail";
        public const string INSTRUCTOR_ACTIVATED = "mInstructorActivate";
    }

    public class SessionMaster
    {
        public const string SessionMaster_DAM_ERROR = "mSessionMasterERROR";
        public const string SESSION_ADD_SUCESS = "mSessionAddSucess";
        public const string SESSION_IS_In_USE = "mSessionIsInUse";
        public const string SESSION_SELECT = "mSessionSelect";
        public const string SESSION_DELETE_SUCESS = "mSessionDelete";
        public const string SESSION_DELETE_ERROR = "mSessionDeleteError";
        public const string SESSION_UPDATE_SUCESS = "mSessionUpdateSucess";
        public const string SESSION_NAME_ALREADY_EXISTS = "mSessionNameAlreadyExists";
        public const string SESSION_MAX_REGISTRATION = "mEnterMaxRegistration";
        public const string SESSION_CLOSE = "mSessionClose";
        public const string SESSION_PLEASE_ADD = "mPleaseAdd";

        public const string SESSION_AT_LEAST_ONE_SESSION_SHOULD_BE_ACTIVE = "mSessionAtLeastOneSessionShouldBeActive";
        public const string SESSION_STARTDATE_ENDDATE_SHOULD_BE_IN_EVENT_STARTEND_DATES = "mSessionStartDateEndDateShouldBeInEventStartEndDates";
        public const string SESSION_STARTDATE_ENDDATE_SHOULD_BE_GREATER_THAN_CURRENTDATE = "mSessionStartDateEndDateShouldBeGreaterThanCurrentDate";
    }

    public class ProgramTypeMaster
    {
        public const string PROGRAMTYPEMASTER_DAM_ERROR = "mProgramtypemaster_Dam_Error";
        public const string PROGRAMTYPEMASTER_ADD_SUCESS = "mProgramtypemaster_Add_Sucess";
        public const string PROGRAMTYPEMASTER_UPDATE_SUCESS = "mProgramtypemaster_Update_Sucess";
        public const string PROGRAMTYPEMASTER_DELETE_ERROR = "mProgramtypemaster_Delete_Error";
        public const string PROGRAMTYPEMASTER_IS_In_USE = "mProgramtypemaster_Is_In_Use";
        public const string PROGRAMTYPEMASTER_SELECT = "mProgramtypemaster_Select";
        public const string PROGRAMTYPEMASTER_DELETE_SUCESS = "mProgramtypemaster_Delete_Sucess";
        public const string PROGRAMTYPEMASTER_NAME_ALREADY_EXISTS = "mProgramtypemaster_Already_Exists";
    }

    public class UserSessionRegistration
    {
        public const string UserSessionRegistration_DAM_ERROR = "mUserSessionRegistrationERROR";
        public const string UserSessionRegistration_ADD_SUCESS = "mUserSessionRegistrationAdd";
        public const string UserSessionRegistration_LEARNER_DELETE_SUCESS = "mLearnerDeleteSucess";
        public const string UserSessionRegistration_LEARNER_REJECT_SUCESS = "mLearnerRejectSucess";
        public const string UserSessionRegistration_LEARNER_APPROVE_SUCESS = "mLearnerApproveSucess";
        public const string UserSessionRegistration_LEARNER_WAITLIST_SUCESS = "mLearnerWaitlistSucess";
        public const string UserSessionRegistration_LEARNER_ISIN_USE = "mLearnerIsInuse";
        public const string UserSessionRegistration_SELECT_LEARNER = "mSelectLearner";
        public const string UserSessionRegistration_SELECT_SESSION = "mSelectSession";
        public const string UserSessionRegistration_SELECT_EVENT = "mSelectEvent";
        public const string UserSessionRegistration_SELFNOMINATION_SUCESS = "mSelfNominationSucess";
        public const string Nominis_GreaterThan_MaxSessionRegistration = "mNominisGreaterThanMaxSessionRegistration";
        public const string Nominis_GreaterThan_MaxSessionRegistration_OtherAreWaitlisted = "mNominisGreaterThanMaxSessionRegistrationOtherAreWaitlisted";
        public const string UserSessionRegistration_ALREADY_APPROVEVE = "mUserAlreadyApprove";

        public const string USERSESSIONREGISTRATION_APPROVED = "mUserSessionRegistrationApproved";
        public const string USERSESSIONREGISTRATION_WAITLISTED = "mUserSessionRegistrationWaitlisted";
        public const string USERSESSIONREGISTRATION_PENDING = "mUserSessionRegistrationPending";
        public const string UserCTEventCapacityExceed = "mCTEventCapacityExceed";
        public const string UserVTEventCapacityExceed = "mVTEventCapacityExceed";

    }

    public class SessionTypeMaster
    {
        public const string SessionTypeMaster_DAM_ERROR = "mSessionTypeMasterERROR";
        public const string SessionTypeMaster_ADD_SUCESS = "mSessionTypeMasterAddSucess";
        public const string SESSIONTYPEMASTER_IS_In_USE = "mSessionTypeMasterIsInUse";
        public const string SESSIONTYPEMASTER_SELECT = "mSessionTypeMasterSelect";
        public const string SESSIONTYPEMASTER_DELETE_SUCESS = "mSessionTypeMasterDelete";
        public const string SESSIONTYPEMASTER_DELETE_ERROR = "mSessionTypeDeleteError";
        public const string SESSIONTYPEMASTER_UPDATE_SUCESS = "mSessionTypeUpdateSucess";
        public const string SESSIONTYPEMASTER_NAME_ALREADY_EXISTS = "mSessionTypeAlreadyExists";
    }

    public class SupportResourceTypeMaster
    {
        public const string SupportResourceTypeMaster_DAM_ERROR = "mSupportResourceTypeMasterERROR";
        public const string SupportResourceTypeMaster_ADD_SUCESS = "mSupportResourceTypeMaster_Add_Sucess";
        public const string SupportResourceTypeMaster_UPDATE_SUCESS = "mSupportResourceTypeMaster_Update_Sucess";
        public const string SupportResourceTypeMaster_DELETE_SUCESS = "mSupportResourceTypeMaster_Delete_Sucess";
        public const string SupportResourceTypeMaster_IS_In_USE = "mSupportResourceTypeMaster_Is_In_Use";
        public const string SupportResourceTypeMaster_DELETE_ERROR = "mSupportResourceTypeMaster_Delete_Error";
        public const string SupportResourceTypeMaster_SELECT = "mSupportResourceTypeMaster_Select";
        public const string SupportResourceTypeMaster_NAME_ALREADY_EXISTS = "mSupportResourceTypeAlreadyExists";
    }

    public class SessionLocation
    {
        public const string SessionLocation_DAM_ERROR = "mSessionLocationERROR";
        public const string LOCATION_ADD_SUCESS = "mLocation_Add_Sucess";
        public const string LOCATION_ISIN_USE = "mLocation_Is_In_Use";
        public const string LOCATION_SELECT = "mLocation_Select";
        public const string LOCATION_DELETE_SUCESS = "mLocation_Delete_Sucess";
        public const string LOCATION_DELETE_ERROR = "mLocation_Delete_Error";
        public const string LOCATION_UPDATE_SUCESS = "mLocation_Update_Sucess";
        public const string LOCATION_NAME_ALREADY_EXISTS = "mLocationAlreadyExists";
        public const string LOCATION_DEACTIVATE_FAILED = "mLocationDeactivate_Fail";
        public const string LOCATION_DEACTIVATED = "mLocationDeactivate";
        public const string PLZ_SEL_ATLEAST_1_LOCATION = "mAtleastOneLocation";
        public const string LOCATION_ACTIVATE_FAILED = "mLocationActivate_Fail";
        public const string LOCATION_ACTIVATED = "mLocationActivate";
    }



    public class Vendor
    {
        public const string Vendor_DL_ERROR = "mVENDORERROR";
        public const string VENDOR_ADD_SUCESS = "mVendor_Add_Sucess";
        public const string VENDOR_UPDATE_SUCESS = "mVendor_Update_Sucess";
        public const string VENDOR_DELETE_SUCESS = "mVendor_Delete_Sucess";
        public const string Vendor_IS_In_USE = "mVendor_Is_In_Use";
        public const string Vendor_SELECT = "mVendor_Select";
        public const string Vendor_DELETE_ERROR = "mVendor_Delete_Error";
        public const string Vendor_NAME_ALREADY_EXISTS = "mVendorAlreadyExists";
    }

    public class RefMaterialMaster
    {
        public const string REFMATERIALMASTER_DAM_ERROR = "mSupportResourceMasterERROR";
        public const string REFMATERIALMASTER_ADD_SUCESS = "mRefMaterialMaster_Add_Sucess";
        public const string REFMATERIALMASTER_UPDATE_SUCCESS = "mRefMaterialMaster_Update_Sucess";
        public const string REFMATERIALMASTER_UPDATE_ERROR = "mRefMaterial_Update_Error";
        public const string PLEASE_SELECT_RERMATERIAL = "mSelectRefMaterial";
        public const string PLEASE_SELECT_ONLY_ONE_RERMATERIAL = "mSelectOnlyOneRefMaterial";
        public const string RERMATERIAL_IS_In_USE = "mRefMaterial_Is_In_Use";
        public const string REFMATERIAL_DELETE_SUCESS = "mRefMaterial_Delete_Sucess";
        public const string REFMATERIAL_DELETE_ERROR = "mRefMaterial_Delete_Error";
        public const string RERMATERIAL_NAME_ALREADY_EXISTS = "mSupportResourceNameAlreadyExists";
        //Selected Asset FileType does not match with selected File for upload.
        public const string RERMATERIAL_FILETYPE_MISMATCH = "mRefMaterial_FileTypeMisMatch";

        public const string RERMATERIAL_DEACTIVATED = "mRefMaterialDeactivate";
        public const string RERMATERIAL_DEACTIVATE_FAILED = "mRefMaterialDeactivate_Fail";
        public const string PLZ_SEL_ATLEAST_1_RERMATERIAL = "mAtleastOneSupportRefMaterial";
        public const string RERMATERIAL_ACTIVATED = "mRefMaterialActivate";
        public const string RERMATERIAL_ACTIVATE_FAILED = "mRefMaterialActivate_Fail";

    }

    public class ProgramMaster
    {
        public const string PROGRAMMASTER_DAM_ERROR = "mPROGRAMERROR";
        public const string PROGRAM_ADD_SUCCESS = "mProgramAddSucess";
        public const string PROGRAM_UPDATE_SUCCESS = "mProgramUpdateSucess";
        public const string PLEASE_SELECT_PROGRAM = "mSelectProgram";
        public const string PLEASE_SELECT_ONLY_ONE_PROGRAM = "mSelectOnlyOneProgram";
        public const string PROGRAM_IS_In_USE = "mProgram_Is_In_Use";
        public const string PROGRAM_DELETE_SUCESS = "mProgram_Delete_Sucess";
        public const string PROGRAM_DELETE_ERROR = "mProgram_Delete_Error";
        public const string PROGRAM_NAME_ALREADY_EXISTS = "mProgramName_Already_Exists";
    }

    public class ContentModuleLanguageTranslate
    {
        public const string COURSE_TRANSLATE_UPDATED_SUCCESS = "mCourseTransUpdateSuccess";
        public const string COURSE_TRANSLATE_UPDATION_FAILED = "mCourseTransFailed";
        public const string COURSE_TRANSLATE_APPROVED_SUCCESS = "mCourseTransApproveSuccess";
        public const string COURSE_TRANSLATE_APPROVE_FAILED = "mCourseTransApproveFailed";
        public const string COURSE_TRANSLATE_ENTER_EXTERNAL_LINK = "mCourseTransEnterExtLink";
        public const string COURSE_TRANSLATE_SELECT_FILE_TO_UPLOAD = "mCourseTransSelectFile";
        public const string COURSE_TRANSLATE_SELECT_ATLEAST_ONE_RECORD_TO_APPROVE = "mCourseTransSelectOneRec";
        public const string COURSE_TRANSLATE_IMPORT_FILE_EXCEL_ONLY = "mCourseTransExcelFileOnly";
        public const string COURSE_TRANSLATE_IMPORT_FILE_ZIP_ONLY = "mCourseTransZipFileOnly";
        public const string COURSE_TRANSLATE_SELECT_ATLEAST_ONE_OPTION = "mCourseTransSelectOneOption";

    }

    #region Question Bank
    public class QuestionBankOptions
    {
        public const string QUESTIONBANKOPTIONS_DAM_ERROR = "mQuestionBankOptions_DAM_Error";
    }
    public class QuestionCategory
    {
        public const string QUESTIONCATEGORY_DAM_ERROR = "mQuestionCategory_DAM_Error";
        public const string QUESTIONCATEGORY_DELETE_FAILED = "mQuestionCategory_Delete_Failed";
        public const string QUESTIONCATEGORY_DELETED = "mQuestionCategory_Deleted";
        public const string PLZ_SEL_ATLEAST_1_QUESTIONCATEGORY = "mPLZ_SEL_ATLEAST_1_QUESTIONCATEGORY";
        public const string QUESTION_CATEGORY_DUPLICATE = "mQUESTION_CATEGORY_DUPLICATE";
        public const string QUESTION_CATEGORY_EXITS = "mQUESTION_CATEGORY_EXITS";
    }
    public class QuestionSubCategory
    {
        public const string QUESTIONSUBCATEGORY_DAM_ERROR = "mQuestionSubCategory_DAM_Error";
        public const string QUESTIONSUBCATEGORY_DELETE_FAILED = "mQuestionSubCategory_Delete_Failed";
        public const string QUESTIONSUBCATEGORY_DELETED = "mQuestionSubCategory_Deleted";
        public const string PLZ_SEL_ATLEAST_1_QUESTIONSUBCATEGORY = "mPLZ_SEL_ATLEAST_1_QUESTIONSUBCATEGORY";
        public const string QUESTION_SUBCATEGORY_DUPLICATE = "mQUESTION_SUBCATEGORY_DUPLICATE";
        public const string QUESTION_SUBCATEGORY_EXITS = "mQUESTION_SUBCATEGORY_EXITS";
    }
    public class QuestionBankOptionsLanguage
    {
        public const string QUESTIONBANKOPTIONSLANGUAGE_DAM_ERROR = "mQuestionBankOptionsLanguage_DAM_Error";
    }
    public class QuestionBank
    {
        public const string QUESTION_ADDED = "mQuestion_Added";
        public const string QUESTION_EDITED = "mQuestion_Edited";
        public const string QUESTION_ADD_FAILED = "mQuestion_Add_Failed";
        public const string QUESTION_EDIT_FAILED = "mQuestion_Edit_Failed";
        public const string QUESTIONBANK_ERROR = "mQuestionBank_Error";
        public const string QUESTION_NAME_REQ = "mQuestion_Name_Req";
        public const string QUESTIONBANK_DAM_ERROR = "mQuestionBank_DAM_Error";
        public const string QUESTIONBANK_DELETE_FAILED = "mQuestionBank_Delete_Failed";
        public const string QUESTIONBANK_DELETED = "mQuestionBank_Deleted";
        public const string PLZ_SEL_ATLEAST_1_QUESTIONBANK = "mPLZ_SEL_ATLEAST_1_QUESTIONBANK";
        public const string QUESTIONBANK_DEACTIVATED = "mQuestionbank_deactivated";
        public const string QUESTIONBANK_DEACTIVATE_FAILED = "mQUESTIONBANK_DEACTIVATE_FAILED";
        public const string QUESTIONBANK_ACTIVATE_FAILED = "mQUESTIONBANK_ACTIVATE_FAILED";
        public const string QUESTIONBANK_ACTIVATE_SUCCESS = "mQUESTIONBANK_ACTIVATE_SUCCESS";
        public const string IMPORT_COMPLETED = "mImport_Completed";
        public const string STRUCTURE_DONOTMATCH_REEXPORT = "mStructure_Donotmatch_Reexport";
        public const string TRANSLATION_FAILED_BEING_EMPTY = "mTranslation_Failed_Being_Empty";
        public const string TRANSLATION_FAILED_QTITLE_MISSING = "mTranslation_Failed_Qtitle_Missing";
        public const string IMPORT_FILE_EXTENTION = "mImport_File_Extention";
        public const string LOCATE_UPLOAD_FILE = "mLocate_Upload_File";
        public const string ERROR_WHILE_BLK_IMP_QUESTION = "mErrorWhileBlkQuestion";
        public const string SUCCESS_WHILE_BLK_IMP_QUESTION = "mSuccessWhileBlkQuestion";
        public const string ERROR_WHILE_UPLOADING_FILE = "mErrorWhileUploadingFile";
    }
    public class QuestionBankLanguage
    {
        public const string QUESTIONBANKLANGUAGE_DAM_ERROR = "mQuestionBankLanguage_DAM_Error";
    }
    #endregion


    #region eStore
    public class Category
    {
        public const string CATEGORY_ADDED = "mProductCategory_Added";
        public const string CATEGORY_EDITED = "mProductCategory_Edited";
        public const string CATEGORY_ADD_FAILED = "mProductCategory_Add_Failed";
        public const string CATEGORY_EDIT_FAILED = "mProductCategory_Edit_Failed";
        public const string CATEGORY_ERROR = "mProductCategory_Error";
        public const string CATEGORY_NAME_REQ = "mProductCategory_Name_Req";
        public const string CATEGORY_LANGUAGE_REQ = "mProductCategory_Language_Req";
        public const string CATEGORY_DAM_ERROR = "mProductCategory_DAM_Error";
        public const string CATEGORY_DELETE_FAILED = "mProductCategory_Delete_Failed";
        public const string CATEGORY_DELETED = "mProductCategory_Deleted";
        public const string PLZ_SEL_ATLEAST_1_ProductCategory = "mPLZ_SEL_ATLEAST_1_ProductCategory";
        public const string CATEGORY_NAME_ALREADY_EXISTS = "mProductCategory_Already_Exists";
        public const string CATEGORY_DEACTIVATED = "mCategoryDeactivated";
        public const string CATEGORY_ACTIVATED = "mCategoryActivated";
    }
    public class ProductLanguage
    {
        public const string CATEGORYLANGUAGE_DAM_ERROR = "mProductCategoryLanguage_DAM_Error";
    }

    public class SubCategory
    {
        public const string SUBCATEGORY_ADDED = "mProductSubCategory_Added";
        public const string SUBCATEGORY_EDITED = "mProductSubCategory_Edited";
        public const string SUBCATEGORY_ADD_FAILED = "mProductSubCategory_Add_Failed";
        public const string SUBCATEGORY_EDIT_FAILED = "mProductSubCategory_Edit_Failed";
        public const string SUBCATEGORY_ERROR = "mProductSubCategory_Error";
        public const string SUBCATEGORY_NAME_REQ = "mProductSubCategory_Name_Req";
        public const string SUBCATEGORY_LANGUAGE_REQ = "mProductSubCategory_Language_Req";
        public const string SUBCATEGORY_DAM_ERROR = "mProductSubCategory_DAM_Error";
        public const string SUBCATEGORY_DELETE_FAILED = "mProductSubCategory_Delete_Failed";
        public const string SUBCATEGORY_DELETED = "mProductSubCategory_Deleted";
        public const string SUBCATEGORY_PLZ_SEL_ATLEAST_One = "mProductSubCategory_Plz_Sel_Atleast_One_SubCategory";
        public const string SUBCATEGORY_NAME_ALREADY_EXISTS = "mProductSubCategory_Already_Exists";

        public const string SUBCATEGORY_DEACTIVATED = "mSubCategoryDeactivated";
        public const string SUBCATEGORY_ACTIVATED = "mSubCategoryActivated";
    }
    public class SubCategoryLanguage
    {
        public const string PRODUCTSUBCATEGORYLANGUAGE_DAM_ERROR = "mProductSubCategoryLanguage_DAM_Error";
    }
    public class ProductLocation
    {
        public const string PRODUCTLOCATION_ADDED = "mProductLocation_Added";
        public const string PRODUCTLOCATION_EDITED = "mProductLocation_Edited";
        public const string PRODUCTLOCATION_ADD_FAILED = "mProductLocation_Add_Failed";
        public const string PRODUCTLOCATION_EDIT_FAILED = "mProductLocation_Edit_Failed";
        public const string PRODUCTLOCATION_ERROR = "mProductLocation_Error";
        public const string PRODUCTLOCATION_NAME_REQ = "mProductLocation_Name_Req";
        public const string PRODUCTLOCATION_LANGUAGE_REQ = "mProductLocation_Language_Req";
        public const string PRODUCTLOCATION_DAM_ERROR = "mProductLocation_DAM_Error";
        public const string PRODUCTLOCATION_DELETE_FAILED = "mProductLocation_Delete_Failed";
        public const string PRODUCTLOCATION_DELETED = "mProductLocation_Deleted";
        public const string PLZ_SEL_ATLEAST_1_PRODUCTLOCATION = "mPLZ_SEL_ATLEAST_1_ProductLocation";
        public const string PRODUCTLOCATION_NAME_ALREADY_EXISTS = "mProductLocation_Already_Exists";
        public const string PRODUCTLOCATION_IS_IN_USE = "mProductLocation_IsInUse";

    }
    public class ProductLocationLanguage
    {
        public const string PRODUCTLOCATIONLANGUAGE_DAM_ERROR = "mProductLocationLanguage_DAM_Error";
    }
    public class MerchantInfo
    {
        public const string MERCHANTINFO_DAM_ERROR = "mMerchantInfo_DAM_Error";
    }
    public class CouponMaster
    {
        public const string COUPONMASTER_DAM_ERROR = "mCOUPONERROR";
        public const string COUPON_ADD_SUCCESS = "mCouponAddSucess";
        public const string COUPON_UPDATE_SUCCESS = "mCouponUpdateSucess";
        public const string PLEASE_SELECT_COUPON = "mSelectCoupon";
        public const string PLEASE_SELECT_ONLY_ONE_COUPON = "mSelectOnlyOneCoupon";
        public const string COUPON_IS_In_USE = "mCoupon_Is_In_Use";
        public const string COUPON_DELETE_SUCESS = "mCoupon_Delete_Sucess";
        public const string COUPON_DELETE_ERROR = "mCoupon_Delete_Error";
        public const string COUPON_NAME_ALREADY_EXISTS = "mCouponCode_Already_Exists";
        public const string COUPON_IN_USED_CAN_NOT_EDITED = "Coupon_In_Used_Can_Not_Edited";
    }
    public class Products
    {
        public const string PRODUCTS_ADDED = "mProducts_Added";
        public const string PRODUCTS_RELATED_ADDED = "mProducts_Related_Added";
        public const string PRODUCTS_EDITED = "mProducts_Edited";
        public const string PRODUCTS_ADD_FAILED = "mProducts_Add_Failed";
        public const string PRODUCTS_EDIT_FAILED = "mProducts_Edit_Failed";
        public const string PRODUCTS_ERROR = "mProducts_Error";
        public const string PRODUCTS_TITLE_REQ = "mProducts_Title_Req";
        public const string PRODUCTS_CODE_REQ = "mProducts_Code_Req";
        public const string PRODUCTS_DAM_ERROR = "mProducts_DAM_Error";
        public const string PRODUCTS_DELETE_FAILED = "mProducts_Delete_Failed";
        public const string PRODUCTS_DELETED = "mProducts_Deleted";
        public const string PLZ_SEL_ATLEAST_1_PRODUCTS = "mPLZ_SEL_ATLEAST_1_Products";
        public const string PLEASE_SELECT_ONLY_ONE_PRODUCT = "mSelectOnlyOneProduct";
        public const string PRODUCT_IS_In_USE = "mProduct_Is_In_Use";
        public const string PRODUCT_CODE_ALREADY_EXISTS = "mProductCode_Already_Exists";
        public const string PRODUCTS_DEACTIVATED = "mProducts_deactivated";
        public const string PRODUCTS_DEACTIVATE_FAILED = "mPRODUCTS_DEACTIVATE_FAILED";
        public const string PRODUCTS_ACTIVATE_FAILED = "mPRODUCTS_ACTIVATE_FAILED";
        public const string PRODUCTS_ACTIVATE_SUCCESS = "mPRODUCTS_ACTIVATE_SUCCESS";
        public const string PRODUCTS_ACTIVITY_REQ = "mProducts_Activity_Req";
        public const string PRODUCTS_PUBLISH_SUCCESS = "mProducts_Publish_Success";
        public const string PRODUCTS_PUBLISH_FAIL = "mProducts_Publish_Fail";
        public const string PRODUCTS_ALREADY_ACTIVATED = "mProducts_Already_Activated";
        public const string PRODUCTS_ALREADY_DEACTIVATED = "mProducts_Already_Deactivated";
        public const string PRODUCT_ORDER_CANCEL_SUCCESS = "mCancelOrderSuccess";
        public const string PRODUCT_ORDER_CANCEL_CONFIRM = "mCancelOrderConfirm";
        public const string PRODUCT_DELETE_CONFIRM = "mProductDeleteConfirm";
        public const string PRODUCT_EXPIRED = "mProduct_Expired";
        public const string PRODUCT_ASSIGNED_SUCCESS = "mProduct_assigned_sucessfully";
        public const string PRODUCT_DISCOUNT_LESS_PRODUCT_VALUE = "mProduct_discount_less_product_value";
        public const string PRODUCT_DISCOUNT_LESS_PRODUCT_VALUE_MULTIPLE = "mProduct_discount_less_product_value_multiple";
        public const string PRODUCT_BESE_PRIZE_MORE_THAN_ZERO = "mProduct_BasePrize_more_than_zero";
        public const string PRODUCT_REDIRECTION_CANNOT_ADD_FOR_SAME = "mProduct_Redirection_cannot_add_for_same";
    }

    public class ProductGroup
    {
        public const string PRODUCTGROUP_DAM_ERROR = "mProductGroup_DAM_Error";
        public const string PRODUCTGROUP_EDITED = "mProductGroup_Edited";
        public const string PRODUCTGROUP_ADDED = "mProductGroup_Added";
        public const string PRODUCTGROUP_CODE_ALREADY_EXISTS = "mProductGroup_Already_Exists";
        public const string PRODUCTGROUP_CODE_URL_EXISTS = "mProductGroup_Already_URL_Exists";
        public const string PRODUCTGROUP_NAME_EXISTS = "mProductGroup_Already_Name_Exists";
        public const string PRODUCTGROUP_MAPPED_SUCCESSFULLY = "mProducts_Mapped_Successfully";
        public const string PRODUCTGROUP_SEQUENCE_UPDATED = "mProductGroup_Sequence_Updated";

    }

    public class VoucherMapping
    {
        public const string VOUCHERMAPPING_EDIT_SUCCESS = "mVoucherMapping_Edit_Success";
        public const string VOUCHERMAPPING_EDIT_FAILED = "mVoucherMapping_Edit_Failed";
    }
    public class LocationMapping
    {
        public const string LOCATIONMAPPING_EDIT_SUCCESS = "mLocationMapping_Edit_Success";
        public const string LOCATIONMAPPING_EDIT_FAILED = "mLocationMapping_Edit_Failed";
    }

    public class Currency
    {
        public const string CURRENCY_ACTIVATED = "mCurrency_Activated";
        public const string CURRENCY_DEACTIVATED = "mCurrency_DeActivated";
        public const string CURRENCY_ACTIVATED_FAILED = "mCurrency_Activated_Failed";
        public const string CURRENCY_DEACTIVATED_FAILED = "mCurrency_DeActivated_Failed";
        public const string PLZ_SEL_ATLEAST_1_CURRENCY = "mPLZ_SEL_ATLEAST_1_Currency";
        public const string CURRENCY_DAM_ERROR = "mCurrency_DAM_Error";
    }

    public class OrderHistory
    {
        public const string ORDERHISTORY_DAM_ERROR = "mOrderHistory_DAM_Error";
        public const string PURCHASE_ORDER_CANCELLED = "PURCHASE_ORDER_CANCELLED";
        public const string PURCHASE_ORDER_USER_CREATION_FAILED = "PURCHASE_ORDER_USER_CREATION_FAILED";
        public const string PURCHASE_ORDER_CREATION_FAILED = "PURCHASE_ORDER_CREATION_FAILED";
        public const string PURCHASE_DISCOUNT_COUPON_REQUIRED = "PURCHASE_DISCOUNT_COUPON_REQUIRED";
        public const string PURCHASE_ORDER_VOUCHERS_NOT_AVAILABLE = "PURCHASE_ORDER_VOUCHERS_NOT_AVAILABLE";
        public const string PURCHASE_ORDER_REGISTRATION_FAILED = "PURCHASE_ORDER_REGISTRATION_FAILED";
        public const string PURCHASE_ENTER_NO_OF_LICENSE_COUNT = "PURCHASE_ENTER_NO_OF_LICENSE_COUNT";
        public const string PURCHASE_ENTER_VALID_LICENSE_COUNT_RANGE = "PURCHASE_ENTER_VALID_LICENSE_COUNT_RANGE";

        public const string MANAGE_ORDER_COMPLETEORDER_FAILED = "mMANAGE_ORDER_COMPLETEORDER_FAILED";
        public const string MANAGE_ORDER_COMPLETEORDER_SUCCESS = "mMANAGE_ORDER_COMPLETEORDER_SUCCESS";
        public const string MANAGE_ORDER_SENDVOUCHER_SUCCESS = "mMANAGE_ORDER_SENDVOUCHER_SUCCESS";
        public const string MANAGE_ORDER_SENDVOUCHER_FAILED = "mMANAGE_ORDER_SENDVOUCHER_FAILED";


    }

    public class TransactionErrorLog
    {
        public const string TRANSACTIONERRORLOG_DAM_ERROR = "mTransactionErrorLog_DAM_Error";
    }
    public class TransactionLog
    {
        public const string TRANSACTIONLOG_DAM_ERROR = "mTransactionLog_DAM_Error";
    }
    public class TransactionSummary
    {
        public const string TRANSACTIONSUMMARY_DAM_ERROR = "mTransactionSummary_DAM_Error";
    }
    public class DiscountCoupon
    {
        public const string DISCOUNTCOUPON_DAM_ERROR = "DiscountCoupon_DAM_ERROR";
        public const string DISCOUNTCOUPON_ADD_SUCC = "DISCOUNTCOUPON_ADD_SUCC";
        public const string DISCOUNTCOUPON_UPDATE_SUCC = "DISCOUNTCOUPON_UPDATE_SUCC";
        public const string DISCOUNTCOUPON_ADD_FAILED = "DISCOUNTCOUPON_ADD_FAILED";
        public const string DISCOUNTCOUPON_UPDATE_FAILED = "DISCOUNTCOUPON_UPDATE_FAILED";
        public const string DISCOUNTCOUPON_TITLE_REQ = "DISCOUNTCOUPON_TITLE_REQ";
        public const string DISCOUNTCOUPON_COUPON_PREFIX_REQ = "DISCOUNTCOUPON_COUPON_PREFIX_REQ";
        public const string DISCOUNTCOUPON_NOOFCOUPONS_REQ = "DISCOUNTCOUPON_NOOFCOUPONS_REQ";
        public const string DISCOUNTCOUPON_PRICE_REQ = "DISCOUNTCOUPON_PRICE_REQ";
        public const string DISCOUNTCOUPON_TYPE_REQ = "DISCOUNTCOUPON_TYPE_REQ";
        public const string DISCOUNTCOUPON_TITLE_EXISTS = "DISCOUNTCOUPON_TITLE_EXISTS";
        public const string DISCOUNTCOUPON_PRICE_INVALID = "DISCOUNTCOUPON_PRICE_INVALID";
        public const string DISCOUNTCOUPON_NOOF_COUPONS_INVALID = "DISCOUNTCOUPON_NOOF_COUPONS_INVALID";
        public const string DISCOUNT_COUPON_ALREADY_USED = "DISCOUNT_COUPON_ALREADY_USED";
        public const string DISCOUNT_COUPON_CODE_EXPIRED = "DISCOUNT_COUPON_CODE_EXPIRED";
        public const string DISCOUNT_COUPON_INVALID_CODE = "DISCOUNT_COUPON_INVALID_CODE";

        public const string DISCOUNT_COUPON_STARTDATE_GREATERTHAN = "DISCOUNT_COUPON_STARTDATE_GREATERTHAN";
        public const string DISCOUNT_COUPON_STARTDATE_SMALLERTHAN = "DISCOUNT_COUPON_STARTDATE_SMALLERTHAN";
        public const string DISCOUNTCOUPON_ACTIVATED_DEACTIVATED_SUCC = "DISCOUNTCOUPON_ACTIVATED_DEACTIVATED_SUCC";
        public const string DISCOUNTCOUPON_SELECT_PROD_CATE = "DISCOUNTCOUPON_SELECT_PROD_CATE";

        public const string DISCOUNT_COUPON_STARTDATE_GREATERTHAN_EXPIRYDATE = "DISCOUNT_COUPON_STARTDATE_GREATERTHAN_EXPIRYDATE";

        public const string DISCOUNT_COUPON_DEACTIVATE_FROM_CART = "DISCOUNT_COUPON_DEACTIVATE_FROM_CART";


        public const string DISCOUNT_COUPON_DEL = "DISCOUNT_COUPON_DELETED";
        public const string DISCOUNT_COUPON_USED_COUPON_CANNOT_DEL = "DISCOUNT_COUPON_USED_COUPON_CANNOT_DEL";
        public const string DISCOUNTCOUPON_CODE_EXISTS = "DISCOUNTCOUPON_CODE_EXISTS";
        public const string DISCOUNTCOUPON_APPLIED_SUCC = "DISCOUNTCOUPON_APPLIED_SUCC";
    }

    public class ProductCatalogRegRequest
    {

        public const string PRODUCT_CATALOG_REG_REQ_DAM_ERROR = "PRODUCT_CATALOG_REG_REQ_DAM_ERROR";
        public const string PRODUCT_CATALOG_REG_REQ_SENT_SUCC = "PRODUCT_CATALOG_REG_REQ_SENT_SUCC";
        public const string PRODUCT_CATALOG_REG_REQ_SENT_FAILED = "PRODUCT_CATALOG_REG_REQ_SENT_FAILED";
        public const string PRODUCT_CATALOG_REG_REQ_ALREADY_SENT = "PRODUCT_CATALOG_REG_REQ_ALREADY_SENT";

        public const string PRODUCT_CATALOG_REG_REQ_APPROVED = "PRODUCT_CATALOG_REG_REQ_APPROVED";
        public const string PRODUCT_CATALOG_REG_REQ_REJECTED = "PRODUCT_CATALOG_REG_REQ_REJECTED";
        public const string PRODUCT_CATALOG_SETTING_UPDATED = "PRODUCT_CATALOG_SETTING_UPDATED";

        public const string PRODUCT_RECEIPT_MAIL_SEND = "PRODUCT_RECEIPT_MAIL_SEND";
        public const string PRODUCT_RECD_BUT_UNABLE_TO_SEND_MAIL = "PRODUCT_RECD_BUT_UNABLE_TO_SEND_MAIL";
    }

    public class ProductCategory
    {
        public const string PRODUCTCATEGORY_ADDED = "mProductCategory_Added";
        public const string PRODUCTCATEGORY_EDITED = "mProductCategory_Edited";
        public const string PRODUCTCATEGORY_ADD_FAILED = "mProductCategory_Add_Failed";
        public const string PRODUCTCATEGORY_EDIT_FAILED = "mProductCategory_Edit_Failed";
        public const string PRODUCTCATEGORY_ERROR = "mProductCategory_Error";
        public const string PRODUCTCATEGORY_NAME_REQ = "mProductCategory_Name_Req";
        public const string PRODUCTCATEGORY_LANGUAGE_REQ = "mProductCategory_Language_Req";
        public const string PRODUCTCATEGORY_DAM_ERROR = "mProductCategory_DAM_Error";
        public const string PRODUCTCATEGORY_DELETE_FAILED = "mProductCategory_Delete_Failed";
        public const string PRODUCTCATEGORY_DELETED = "mProductCategory_Deleted";
        public const string PLZ_SEL_ATLEAST_1_ProductCategory = "mPLZ_SEL_ATLEAST_1_ProductCategory";
        public const string PRODUCTCATEGORY_NAME_ALREADY_EXISTS = "mProductCategory_Already_Exists";
    }
    public class ProductCategoryLanguage
    {
        public const string PRODUCTCATEGORYLANGUAGE_DAM_ERROR = "mProductCategoryLanguage_DAM_Error";
    }

    public class ProductSubCategory
    {
        public const string PRODUCTSUBCATEGORY_ADDED = "mProductSubCategory_Added";
        public const string PRODUCTSUBCATEGORY_EDITED = "mProductSubCategory_Edited";
        public const string PRODUCTSUBCATEGORY_ADD_FAILED = "mProductSubCategory_Add_Failed";
        public const string PRODUCTSUBCATEGORY_EDIT_FAILED = "mProductSubCategory_Edit_Failed";
        public const string PRODUCTSUBCATEGORY_ERROR = "mProductSubCategory_Error";
        public const string PRODUCTSUBCATEGORY_NAME_REQ = "mProductSubCategory_Name_Req";
        public const string PRODUCTSUBCATEGORY_LANGUAGE_REQ = "mProductSubCategory_Language_Req";
        public const string PRODUCTSUBCATEGORY_DAM_ERROR = "mProductSubCategory_DAM_Error";
        public const string PRODUCTSUBCATEGORY_DELETE_FAILED = "mProductSubCategory_Delete_Failed";
        public const string PRODUCTSUBCATEGORY_DELETED = "mProductSubCategory_Deleted";
        public const string PRODUCTSUBCATEGORY_PLZ_SEL_ATLEAST_One = "mProductSubCategory_Plz_Sel_Atleast_One_SubCategory";
        public const string PRODUCTSUBCATEGORY_NAME_ALREADY_EXISTS = "mProductSubCategory_Already_Exists";
    }
    public class ProductSubCategoryLanguage
    {
        public const string PRODUCTSUBCATEGORYLANGUAGE_DAM_ERROR = "mProductSubCategoryLanguage_DAM_Error";
    }

    #endregion
    //created by deepak dangat
    #region Blog

    public class BlogPost
    {
        public const string BLOGPOST_DAM_ERROR = "BLOGPOST_DAM_ERROR";
        public const string BLOG_POST_ADD_SUCCESS = "mBlogPostAddSuccess";
        public const string BLOG_POST_ADD_FAILED = "mBlogPostAddFailed";
        public const string BLOG_POST_UPDATE_SUCCESS = "mBlogPostUpdateSuccess";
        public const string BLOG_POST_UPDATE_FAILED = "mBlogPostUpdateFailed";
        public const string BLOG_POST_DEL_SUCCESS = "mBlogPostDelSuccess";
        public const string BLOG_POST_DEL_FAILED = "mBlogPostDelFailed";
        public const string BLOG_POST_ENTER = "mBlogPostEnter";
        public const string BLOG_ALREADY_PUBLISHED = "mBlogPostAlreadyPubished";

    }
    public class BlogPostComments
    {

        public const string BLOGPOSTCOMMENTS_DAM_ERROR = "mBlogPostCommDamError";
        public const string BLOG_POST_COMMENT_APPROVED = "mBlogPostCommApproved";
        public const string BLOG_POST_COMMENT_NOT_APPROVED = "mBlogPostCommApprovedFailed";
        public const string BLOG_POST_COMM_DEL_SUCCESS = "mBlogPostCommDelSuccess";
        public const string BLOG_POST_COMM_DEL_FAILED = "mBlogPostCommDelFailed";
        public const string BLOG_POST_COMM_UPDATE_SUCCESS = "mBlogPostCommUpdateSuccess";
        public const string BLOG_POST_COMM_UPDATE_FAILED = "mBlogPostCommUpdateFailed";
        public const string BLOG_POST_COMM_PENDING_STATUS = "mBlogPostCommPendingStatus";
        public const string BLOG_POST_COMM_PLEASE_ENTER_COMMENT = "mBlogPostCommPleaseEnterComment";

    }
    public class BlogCategories
    {

        public const string BLOGCATEGORIES_DAM_ERROR = "mSystemMaintenanceMessage";
        public const string BLOGCATEGORY_NAME_ALREADY_EXISTS = "mBlogCategory_Already_Exists";
        public const string BlogCategory_IS_In_USE = "mBlogCategoryISInUSE";
        public const string PLZ_SEL_ATLEAST_1_BlogCATEGORY = "mPLZSELATLEAST1BlogCATEGORY";
        public const string BLOGCATEGORY_DELETED = "mBLOGCATEGORYDELETED";
        public const string BLOGCATEGORY_DELETED_FAILED = "mBLOGCATEGORYDELETEDFAILED";

    }

    public class BlogCategoriesLanguage
    {

        public const string BLOGCATEGORIES_DAM_ERROR = "mSystemMaintenanceMessage";
    }

    #endregion

    public class BrandImages
    {
        public const string BRAND_IMAGE_UPLOADED_SUCCESS = "mImageUploadedSuccess";
        public const string BRAND_IMAGE_DELETE_SUCCESS = "mImageDeleteSuccess";
    }

    #region LearnerComponentsSettings
    public class LearnerComponentsSettings
    {
        public const string LEARNERCOMPONENT_UPDATE_SUCCESS = "mLearnerComponentUpdateSucess";
    }
    #endregion

    #region FeatureList
    public class FeatureList
    {
        public const string FEATURELIST_DAM_ERROR = "mFEATURELIST_DAM_ERROR";
    }
    #endregion

    #region Browser
    public class Browser
    {
        public const string BROWSER_DAM_ERROR = "mBROWSER_DAM_ERROR";
    }
    #endregion
    #region Forum
    public class ForumCategory
    {
        public const string FORUMCATEGORY_DAM_ERROR = "FORUMCATEGORY_DAM_ERROR";
        public const string FORUMCATEGORY_NAME_ALREADY_EXISTS = "mForumCategory_Already_Exists";
        public const string ForumCategory_Deleted = "mForumCategoryDeleted";
        public const string ForumCategory_Deleted_Fail = "mForumCategoryDeletedFail";
    }
    public class ForumSubCategory
    {
        public const string FORUMSUBCATEGORY_DAM_ERROR = "FORUMSUBCATEGORY_DAM_ERROR";
        public const string FORUMSUBCATEGORY_NAME_ALREADY_EXISTS = "mForumSubCategory_Already_Exists";
        public const string ForumSubCategory_IS_In_USE = "mForumSubCategoryISInUSE";
        public const string PLZ_SEL_ATLEAST_1_FORUMSUBCATEGORY = "mPLZSELATLEAST1FORUMSUBCATEGORY";
        public const string FORUMSUBCATEGORY_DELETED = "mFORUMSUBCATEGORYDELETED";
        public const string FORUMSUBCATEGORY_DELETED_FAILED = "mFORUMSUBCATEGORYDELETEDFAILED";

    }
    public class ForumThread
    {
        public const string FORUMTHREAD_DAM_ERROR = "FORUMTHREAD_DAM_ERROR";
        public const string FORUM_THREAD_ADD_SUCC = "mForumThreadAddSucc";
        public const string FORUM_THREAD_OF_MODERATION_ADD_SUCC = "mForumThreadOfModerationAddSucc";
        public const string FORUM_THREAD_ADD_FAIL = "mForumThreadAddFail";
        public const string FORUMTHREAD_DELETED_FAILED = "mFORUMTHREADDELETEDFAILED";
        public const string PLZ_SEL_ATLEAST_1_FORUMTHREAD = "mPLZSELATLEAST1FORUMTHREAD";
        public const string FORUMTHREAD_DELETED = "mFORUMTHREADDELETED";
        public const string FORUMTHREAD_ADDSUBJECT = "mFORUMTHREADDSUBJECT";
        public const string FORUMTHREAD_ADD_DESCRIPTION = "mFORUMTHREADDDescription";
    }
    public class ForumPost
    {
        public const string FORUMPOST_DAM_ERROR = "FORUMPOST_DAM_ERROR";
        public const string FORUM_POST_ADD_SUCC = "mForumPostAddSucc";
        public const string FORUM_POST_ADD_FAIL = "mForumPostAddFail";
        public const string FORUM_POST_PLEASE_ENTER_REPLY = "mForumPostPleaseEnterReply";
    }

    public class ForumCategoryLanguage
    {
        public const string ForumCategoryLanguage_DAM_ERROR = "ForumCategoryLanguage_DAM_ERROR";
    }
    public class ForumSubCategoryLanguage
    {
        public const string FORUMSUBCATEGORYLANGUAGE_DAM_ERROR = "FORUMSUBCATEGORYLANGUAGE_DAM_ERROR";
    }
    #endregion

    #region FAQ
    public class FAQ
    {
        public const string FAQ_ERROR_MSG_ID = "mFAQERROR";
        public const string FAQ_DEL_SUCCESS = "mFAQDelSuccess";
        public const string SEL_FAQ_CANT_B_DEL = "mSelFAQCantBDel";
        public const string FAQ_DEACTIVE_SUCCESS = "mFAQDeactivateSuccess";
        public const string FAQ_ACTIVE_SUCCESS = "mFAQActivateSuccess";
        public const string FAQ_DEACTIVE_ALREADY = "mFAQDeactivateAlready";
        public const string FAQ_OWNR_SITEADMIN_HAVE_PRIVILAGE = "mFAQOwnerSiteAdminHavePrivilage";
        public const string PLZ_SEL_FAQ = "mPlzSelectFAQ";
        public const string DEACTIVATE_FAQ_B_4_DELETE = "mDeactivateFAQB4Delete";
        public const string FAQ_FILE_ATTACHED_SUCCESS = "mFAQFileAttachedSuccess";
        public const string FAQ_INVALID_URL = "mFAQInvalidURL";
        public const string FAQ_ATTACHED_FILE_FORMAT_NOT_ALLOWED = "mFAQAttachedFileFormatNotAllowed";
        public const string FAQ_ENTER_ANSWER = "mPleaseEnterAnswer";
    }
    #endregion

    public class UserExpiry
    {
        public const string Rule_User_Expiry_Delete = "mRule_User_Expiry_Delete";
        public const string Rule_User_Expiry_Delete_Sel = "mRule_User_Expiry_Delete_Sel";
        public const string User_Expiry_Added = "mUser_Expiry_Added";
        public const string User_Expiry_Updated = "mUser_Expiry_Updated";
        public const string User_Expiry = "mUser_Expiry";
    }


    public class CurriculumSection
    {
        public const string CURRICULAM_SECTION_DAM_ERROR = "mCurriculamSectionERROR";
        public const string CURRICULAM_SECTION_ADDED = "mCurriculumSection_Added";
        public const string CURRICULAM_SECTION_EDITED = "mCurriculumSection_Edited";
        public const string CURRICULAM_SECTION_ADD_FAILED = "mCurriculumSection_Add_Failed";
        public const string CURRICULAM_SECTION_EDIT_FAILED = "mCurriculumSection_Edit_Failed";
        public const string CURRICULAM_SECTION_NAME_REQ = "mCurriculumSection_Name_Req";
        public const string CURRICULAM_SECTION_LANGUAGE_REQ = "mCurriculumSection_Language_Req";
        public const string CURRICULAM_SECTION_DELETE_FAILED = "mCurriculumSection_Delete_Failed";
        public const string CURRICULAM_SECTION_DELETED = "mCurriculumSection_Deleted";
        public const string CURRICULAM_SECTION_PLZ_ATLEAST_ONE_RECORD = "mCurriculumSection_Select_Atleast_One_Record";
        public const string CURRICULAM_SECTION_NAME_ALREADY_EXISTS = "mCurriculumSection_Already_Exists";

    }

    public class CategoryMapping
    {
        public const string CATEGORY_MAPPING_ADDED = "mCategoryMapping_Added";
        public const string CATEGORY_MAPPING_EDITED = "mCategoryMapping_Edited";
        public const string CATEGORY_MAPPING_ADD_FAILED = "mCategoryMapping_Add_Failed";
        public const string CATEGORY_MAPPING_EDIT_FAILED = "mCategoryMapping_Edit_Failed";
        public const string CATEGORY_MAPPING_ERROR = "mCategoryMapping_Error";
        public const string CATEGORY_MAPPING_DELETE_FAILED = "mCategoryMapping_Delete_Failed";
        public const string CATEGORY_MAPPING_DELETED = "mCategoryMapping_Deleted";
        public const string CATEGORY_MAPPING_SELECT_ATLEAST_ONE_RECORD = "mCategoryMapping_Select_Atleast_One_Record";

    }

    public class DocumentLibrary
    {
        public const string INVALID_FOLDER_ID = "InvalidFolderId";
        public const string INVALID_PARENT_FOLDER_ID = "InvalidParentFolderId";
        public const string DOCUMENT_LIB_BL_ERROR = "mDOCUMNENT_LIB_ERROR";
        public const string DOCUMENT_LIB_DEL_ERROR = "mDOCUMENT_LIB_DEL_ERROR";
        public const string DOCUMENT_LIBRARY_ASSIGN = "DocumentLibAssignExist";
        //"Delete the document under the folder and then delete the folder."
        public const string DELETE_DOCUMENTS_UNDER_FOLDER = "mFirstDeleteDocumentsUnderFolder";
        public const string FOLDER_ADD_SUCCESS = "mDocumentFolderAddSuccess";
        public const string FOLDER_EDIT_SUCCESS = "mDocumentFolderEditSuccess";
        public const string FOLDER_DELETE_SUCCESS = "mDocumentFolderDeleteSuccess";
        public const string FOLDER_ADD_FAIL = "mDocumentFolderAddFail";
        public const string FOLDER_EDIT_FAIL = "mDocumentFolderEditFail";
        public const string FOLDER_DELETE_FAIL = "mDocumentFolderDeleteFail";
        //AssetLibrary-Enter Valid Folder Name
        public const string ENTER_VALID_FOLDER_NAME = "mDocumentLibraryEnterValidFolderName";
    }

    public class Document
    {
        public const string INVALID_DOCUMent_LIBRARY_ID = "InvalidLibraryId";
        public const string DOCUMENT_BL_ERROR = "mDOCUMENT_ERROR";
        public const string DOCUMENT_SEL_FOLDER = "mDocumentSelectFolder";
        public const string DOCUMENT_SEL_FOLDER_ADD = "mDocumentSelectFolderForAdd";
        //"Add Asset Operation Failed."
        public const string DOCUMENT_EDIT_SUCCESS = "mDocumentEditSuccess";
        public const string DOCUMENT_ADD_SUCCESS = "mDocumentAddSuccess";
        public const string DOCUMENT_DELETE_SUCCESS = "mDocumentDeleteSuccess";
        public const string DOCUMENT_ADD_FAIL = "mDocumentAddFail";
        public const string DOCUMENT_EDIT_FAIL = "mDocumentEditFail";
        public const string DOCUMENT_DELETE_FAIL = "mDocumentDeleteFail";
        // Error loading asset information
        public const string DOCUMENT_INFO_ERROR = "mDocumentInfoError";
        //â€ The Asset is successfully deactivatedâ€
        public const string DOCUMENT_DEACTIVATED = "mDocumentDeactivated";
        public const string DOCUMENT_ACTIVATED = "mDocumentActivated";
        //â€The Asset is already deactivated.â€
        public const string DOCUMENT_ALREADY_DEACTIVATED = "mDocumentAlreadyDeactivated";
        public const string DOCUMENT_ALREADY_ACTIVATED = "mDocumentAlreadyActivated";
        //Selected Asset FileType does not match with selected File for upload.
        public const string FILETYPE_MISMATCH = "mDocument_FileTypeMisMatch";
        //â€œFile with the name $ already exist in the Folder. While uploading the file, system will rename the file to # ."
        public const string FILE_EXISTS_RENAME_FOR_ADD = "mDocument_FileExistsRenameForAdd";
        //â€œAsset Name is Required.â€
        public const string NAME_REQ = "mDocument_NameRequired";
        //"Select an Asset FileType from the dropdown.";
        public const string FILETYPE_REQ = "mDocument_SelectFileType";
        //"Select an Asset File to upload."
        public const string FILE_REQ = "mDocument_FileRequiredForUpload";
        //Assigned Asset cannot be deleted.
        public const string Document_CANT_BE_DELETED = "mDocument_CantBeDeleted";
        //VALID_EMAIL Please input valid Email Addresses.
        public const string ENT_VALID_EMAIL_ID = "mDocument_ENTValidEmailId";
        //FLD_REQ_Folder Name is required.
        public const string FOLDER_NAME_REQUIRED = "mDocument_FolderNameRequired";
        //Only the Administrator or the Owner can update the Folder
        public const string ONLY_ADMIN_OR_OWNER_CAN_UPDATE = "mDocument_OnlyAdminOrOwnerCanUpdate";
        //Common Asset/Policy   -  You do not have adequate rights to delete the Folder.
        public const string NO_ADEQUATE_DELETE_RIGHTS = "mDocument_NoAdequateDeleteRights";
        //SELECT_ONLY_ONE -  Select only one item for edit
        public const string SELECT_ONE_ITEM_FOR_EDIT = "mDocumentEditoneItemForEdit";
        // Select a Valid Parent Unit.
        public const string SELECT_VALID_PARENT_UNIT = "mDocumentSelectValidParentUnit";
        // Select Parent Unit from the dropdown again.
        public const string SELECT_PARENT_UNIT = "mDocumentSelectParentUnit";
        //Asset Folder with the same name already exists
        public const string DOCUMENT_FOLDER_NAME_ALREADY_EXITS = "mDocumentFolderNameAlreadyExists";
        //"Asset Folder Name is Required."
        public const string DOCUMENT_FOLDER_NAME_REQUIRED = "mDocumentFolderNameRequired";
        //You do not have adequate rights to edit the selected Asset folder.
        public const string NO_FOLDER_EDIT_RIGHTS = "mDocumentNoFolderEditRights";
        // Folder already contains a File with the same Name. Please rename the file and then upload it.
        public const string FILE_EXIST_WITH_SAME_NAME = "mDocumentFileExistWithSameName";
        // -  "Asset File should not be blank."
        public const string FILE_SHOULD_NOTBE_EMPTY = "mDocumentFileShouldNotBeEmpty";
        //You do not have adequate rights to modify this Asset.
        public const string NO_FILE_EDIT_RIGHTS = "mDocumentNoFileEditRights";
        //You do not have adequate rights to delete this Asset.
        public const string NO_FILE_DELETE_RIGHTS = "mDocumentNoFileDeleteRights";
        //  Enter Valid Asset Name
        public const string ENTER_VALID_DOCUMENT_NAME = "mDocumentEnterValidDocumentName";
        //-    Enter Valid Description
        public const string ENTER_VALID_DESCRIPTION = "mDocumentEnterValidDerscription";
        //-    No Special Characters are allowed in FileName
        public const string NO_SPECIAL_CHARS_ALLOWED_IN_FILENAME = "mDocumentNoSpecialCharsAllowedInFileName";
        //â€œThe Asset selected for edit is already assigned and the change will be reflected immediately for users accessing it."
        public const string ASSIGN_DOCUMENT_SELECTED_FOR_EDIT = "mDocumentAssignPolicySelectedForEdit";
        //Asset Type is required *"
        public const string DOCUMENT_TYPE_IS_REQUIRED = "mDocumentTypeisRequired";
        //File Upload is required*
        public const string FILE_UPLOAD_IS_REQUIRED = "mDocumentFileUploadIsRequired";
        //Select a Asset File Type.
        public const string SELECT_DOCUMENT_FILE_TYPE = "mDocumentSelectPolicyFileType";
        //Selected Asset File for upload is Invalid.
        public const string INVALID_DOCUMENT_FILE_SELECTED_FORUPLOAD = "mDocumentInvalidAssetFileSelectedForUpload";
        // Asset  with the same name already exists in the current Asset Folder. 
        public const string DOCUMENT_NAME_EXISTS = "mDocumentNameExists";
    }

    public class Bookmark
    {
        public const string BOOKMARK_EDIT_SUCCESS = "mBookmarkEditSuccess";
        public const string BOOKMARK_ADD_SUCCESS = "mBookmarkAddSuccess";
        public const string BOOKMARK_DELETE_SUCCESS = "mBookmarkDeleteSuccess";
        public const string BOOKMARK_ADD_FAIL = "mBookmarkAddFail";
        public const string BOOKMARK_EDIT_FAIL = "mBookmarkEditFail";
        public const string BOOKMARK_DELETE_FAIL = "mBookmarkDeleteFail";
    }

    public class VirtualTrainingRefMaterialMaster
    {
        public const string REFMATERIALMASTER_DAM_ERROR = "mSupportResourceMasterERROR";
        public const string REFMATERIALMASTER_ADD_SUCESS = "mRefMaterialMaster_Add_Sucess";
        public const string REFMATERIALMASTER_UPDATE_SUCCESS = "mRefMaterialMaster_Update_Sucess";
        public const string REFMATERIALMASTER_UPDATE_ERROR = "mRefMaterial_Update_Error";
        public const string PLEASE_SELECT_RERMATERIAL = "mSelectRefMaterial";
        public const string PLEASE_SELECT_ONLY_ONE_RERMATERIAL = "mSelectOnlyOneRefMaterial";
        public const string RERMATERIAL_IS_In_USE = "mRefMaterial_Is_In_Use";
        public const string RERMATERIAL_DELETE_SUCESS = "mRefMaterial_Delete_Sucess";
        public const string RERMATERIAL_DELETE_ERROR = "mRefMaterial_Delete_Error";
        public const string RERMATERIAL_NAME_ALREADY_EXISTS = "mSupportResourceNameAlreadyExists";
        //Selected Asset FileType does not match with selected File for upload.
        public const string RERMATERIAL_FILETYPE_MISMATCH = "mRefMaterial_FileTypeMisMatch";

        public const string RERMATERIAL_DEACTIVATED = "mRefMaterialDeactivate";
        public const string RERMATERIAL_DEACTIVATE_FAILED = "mRefMaterialDeactivate_Fail";
        public const string PLZ_SEL_ATLEAST_1_RERMATERIAL = "mAtleastOneSupportRefMaterial";
        public const string RERMATERIAL_ACTIVATED = "mRefMaterialActivate";
        public const string RERMATERIAL_ACTIVATE_FAILED = "mRefMaterialActivate_Fail";
        public const string RERMATERIAL_UNMAPPED_SUCESS = "mRefMaterial_Unmapped_Sucess";
        public const string RERMATERIAL_MAPPED_SUCESS = "mRefMaterialMaster_Mapped_Sucess";

    }

    public class VirtualTrainingSessionMaster
    {
        public const string VIRTUALTRAININGSESSIONMASTER_DAM_ERROR = "mVirtualTrainingSessionMasterDAMERROR";
        public const string TRINING_SESSION_ADD_SUCESS = "mVirtualTrainingSessionAddSucess";
        public const string TRINING_SESSION_UPDATE_SUCESS = "mVirtualTrainingSessionUpdateSucess";
        public const string TRINING_SESSION_NAME_ALREADY_EXISTS = "mVirtualTrainingSessionNameAlreadyExist";
        public const string TRAININGSESSION_IS_IN_USE = "mVirtualTrainingSessionIsInUse";
        public const string PLEASE_SELECT_TRAININGSESSION = "mVirtualTrainingSessionSelect";
        public const string TRAININGSESSION_DELETE_SUCESS = "mVirtualTrainingSessionDeleteSucess";
        public const string TRAININGSESSION_DELETE_ERROR = "mVirtualTrainingSessionDeleteError";
        public const string VIRTUALTRAININGSESSIONMASTER_INVALID_INPUT_DATA = "mVirtualTrainingSessionInvalidInput";
        public const string VIRTUALTRAININGSESSIONMASTER_INVALID_SESSION = "mVirtualTrainingSessionInvalidSession";
        public const string VIRTUALTRAININGSESSIONMASTER_WEBEX_UPDATION_FAIL = "mVirtualTrainingSessionWebexUpdationFail";
        public const string VIRTUALTRAININGSESSIONMASTER_WEBEXAPICALLINGERROR = "mVirtualTrainingSessionWebexAPICallingError";
        public const string VIRTUALTRAININGSESSIONMASTER_NOTRAININGHOST = "mVirtualTraining_NoAccountHost";
    }

    public class VirtualTrainingAttendeeMaster
    {
        public const string VIRTUALTRAININGATTENDEEMASTER_DAM_ERROR = "mVirtualTrainingAttendeeMasterDAMERROR";
        public const string VIRTUALTRINING_ATTENDEE_ADD_SUCESS = "mVirtualTrainingAttendeeAddSucess";
        public const string VIRTUALTRINING_ATTENDEE_UPDATE_SUCESS = "mVirtualTrainingAttendeeUpdateSucess";
        public const string VIRTUALTRINING_ATTENDEE_NAME_ALREADY_EXISTS = "mVirtualTrainingAttendeeNameAlreadyExist";

        public const string VIRTUALTRAININGATTENDEE_IS_IN_USE = "mVirtualTrainingAttendeeIsInUse";
        public const string PLEASE_SELECT_VIRTUALTRAININGATTENDEE = "mVirtualTrainingAttendeeSelect";
        public const string VIRTUALTRAININGATTENDEE_DELETE_SUCESS = "mVirtualTrainingAttendeeDeleteSucess";
        public const string VIRTUALTRAININGATTENDEE_DELETE_ERROR = "mVirtualTrainingAttendeeDeleteError";
        public const string VIRTUALTRAININGATTENDEE_SELECT_TRAINING_SESSION = "mVirtualTrainingAttendeeSelectTrainngSession";
        public const string VIRTUALTRAININGATTENDEE_SELECT_TRAINING_SESSION_LEARNER = "mVirtualTrainingAttendeeSelectTrainngSessionLearner";
        public const string VIRTUALTRINING_ATTENDEE_ACCEPT_SUCESS = "mVirtualTrainingAttendeeAcceptSucess";
        public const string VIRTUALTRINING_ATTENDEE_REJECT_SUCESS = "mVirtualTrainingAttendeeRejectSucess";
        public const string VIRTUALTRINING_PLEASE_SELECT_ATTENDEE = "mVirtualTrainingPleaseSelectAttendee";
        public const string VIRTUALTRAININGATTENDEEMASTER_ACCEPT = "mVirtualTrainingAttendeeAccept";
        public const string VIRTUALTRAININGATTENDEEMASTER_REJECT = "mVirtualTrainingAttendeeReject";
        public const string VIRTUALTRAININGATTENDEEMASTER_REGISTER = "mVirtualTrainingAttendeeRegister";
        public const string VIRTUALTRAININGATTENDEEMASTER_ERROR = "mVirtualTrainingAttendeeError";

    }

    public class VirtualTrainingUserMaster
    {
        //Training User management
        public const string VIRTUALTRAINING_USER_ADD_SUCCESS = "mVirtualTraninngUserAdded";
        public const string VIRTUALTRAINING_USER_DAM_ERROR = "mVirtualTraninngUserDamError";
        public const string VIRTUALTRAINING_USER_UPDATE_SUCCESS = "mVirtualTraninngUserUpdated";
        public const string VIRTUALTRAINING_USER_NAME_ALREADY_EXISTS = "mVirtualTraninngUserAlreadyExists";
        public const string PLEASE_SELECT_VIRTUALTRAINING = "mVirtualTraninngPleaseSelect";
        public const string VIRTUALTRAINING_DELETE_SUCESS = "mVirtualTraninngDeleteSucess";
        public const string VIRTUALTRAINING_DELETE_ERROR = "mVirtualTraninngDeleteError";
        public const string VIRTUALTRAINING_IS_ACTIVE = "mVirtualTraninngIsActive";
        public const string VIRTUALTRAINING_USER_ALREADY_USED = "mVirtualTraniningAlreadyUsed";
        public const string VIRTUALTRAINING_USER_ADMIN_USER_ADD_SUCESS = "mVirtualTraniningAdminUserAdd";
        public const string PLZ_SEL_ATLEAST_1_USER = "mVirtualTraniningAdminUserselectatleastOne";
        public const string VIRTUALTRAINING_ADMINUSER_DELETE_SUCESS = "mVirtualTraniningAdminUserDelete";
        public const string VIRTUALTRAINING_USER_NOT_EXIST_WEBEX = "mVirtualTraniningUserNotExist";

    }

    public static class TimeZoneMaster
    {
        public const string TIMEZONEMASTER_DAM_ERROR = "mTimeZoneDamError";
    }

    public static class MessagesAndAlerts
    {
        public const string MESSAGES_EDIT_SUCCESS = "mMessagesEditSuccess";
        public const string MESSAGES_ADD_SUCCESS = "mMessagesAddSuccess";
        public const string MESSAGES_DELETE_SUCCESS = "mMessagesDeleteSuccess";
        public const string MESSAGES_ADD_FAIL = "mMessagesAddFail";
        public const string MESSAGES_EDIT_FAIL = "mMessagesEditFail";
        public const string MESSAGES_DELETE_FAIL = "mMessagesDeleteFail";
    }

    public static class MessageTemplate
    {
        public const string MESSAGESTEMPLATE_EDIT_SUCCESS = "mMessageTemplateEditSuccess";
        public const string MESSAGESTEMPLATE_ADD_SUCCESS = "mMessageTemplateAddSuccess";
        public const string MESSAGESTEMPLATE_DELETE_SUCCESS = "mMessageTemplateDeleteSuccess";
        public const string MESSAGESTEMPLATE_ADD_FAIL = "mMessageTemplateAddFail";
        public const string MESSAGESTEMPLATE_EDIT_FAIL = "mMessageTemplateEditFail";
        public const string MESSAGESTEMPLATE_DELETE_FAIL = "mMessageTemplateDeleteFail";
        public const string MESSAGESTEMPLATE_DEACTIVATED = "mMessageTemplateDeactivated";
        public const string MESSAGESTEMPLATE_ACTIVATED = "mMessageTemplateActivated";
        //â€The Asset is already deactivated.â€
        public const string MESSAGESTEMPLATE_ALREADY_DEACTIVATED = "mMessageTemplateAlreadyDeactivated";
        public const string MESSAGESTEMPLATE_ALREADY_ACTIVATED = "mMessageTemplateAlreadyActivated";
    }

    public class LicensesActivity
    {
        public const string ACTIVITY_DELETE_FAILED = "mACTIVITY_Deleted_Failed";
        public const string ACTIVITY_DELETED = "mACTIVITY_Deleted";
        public const string PLZ_SEL_ATLEAST_1_ACTIVITY = "mPLZ_SEL_ATLEAST_1_ACTIVITY";
        public const string LICENSE_NAME_EXISTS = "mLICENSE_Name_Exists";
    }

    public class Assignments
    {
        public const string ASSIGNMENTS_EDIT_SUCCESS = "mAssignmentsEditSuccess";
        public const string ASSIGNMENTS_ADD_SUCCESS = "mAssignmentsAddSuccess";
        public const string ASSIGNMENTS_DELETE_SUCCESS = "mAssignmentsDeleteSuccess";
        public const string ASSIGNMENTS_ADD_FAIL = "mAssignmentsAddFail";
        public const string ASSIGNMENTS_EDIT_FAIL = "mAssignmentsEditFail";
        public const string ASSIGNMENTS_DELETE_FAIL = "mAssignmentsDeleteFail";
        public const string ASSIGNMENTS_DEACTIVATED = "mAssignmentsDeactivated";
        public const string ASSIGNMENTS_ACTIVATED = "mAssignmentsActivated";
        //â€The Asset is already deactivated.â€
        public const string ASSIGNMENTS_ALREADY_DEACTIVATED = "mAssignmentsAlreadyDeactivated";
        public const string ASSIGNMENTS_ALREADY_ACTIVATED = "mAssignmentsAlreadyActivated";
        public const string ASSIGNMENTS_BL_ERROR = "mDOCUMNENT_LIB_ERROR";
        public const string ASSIGNMENTS_DEL_ERROR = "mDOCUMENT_LIB_DEL_ERROR";
        public const string ASSIGNMENTS_ASSIGN = "DocumentLibAssignExist";
        // Asset  with the same name already exists in the current Asset Folder. 
        public const string ASSIGNMENT_NAME_EXISTS = "mAssignmentNameExists";
        //â€œFile with the name $ already exist in the Folder. While uploading the file, system will rename the file to # ."
        public const string FILE_EXISTS_RENAME_FOR_ADD = "mAssignment_FileExistsRenameForAdd";
        //â€œAsset Name is Required.â€
        public const string NAME_REQ = "mAssignment_NameRequired";
        //"Select an Asset File to upload."
        public const string FILE_REQ = "mAssignment_FileRequiredForUpload";
        //Assigned Asset cannot be deleted.
        public const string ASSIGNMENT_TOPIC_CANT_BE_DELETED = "mAssignment_CantBeDeleted";

        // -  "Asset File should not be blank."
        public const string FILE_SHOULD_NOTBE_EMPTY = "mAssignmentFileShouldNotBeEmpty";
        //You do not have adequate rights to modify this Asset.
        public const string NO_FILE_EDIT_RIGHTS = "mAssignmentNoFileEditRights";
        //You do not have adequate rights to delete this Asset.
        public const string NO_FILE_DELETE_RIGHTS = "mAssignmentNoFileDeleteRights";
        //  Enter Valid Asset Name

        //Selected Asset File for upload is Invalid.
        public const string INVALID_ASSIGNMENT_TOPIC_FILE_SELECTED_FORUPLOAD = "mAssignmentInvalidAssignmentFileSelectedForUpload";
    }

    public class AssignmentTopic
    {
        public const string INVALID_TOPIC_ID = "InvalidTopicId";
        public const string ASSIGNMENT_BL_ERROR = "mAssignment_ERROR";
        public const string ASSIGNMENT_ASSIGN = "AssetAssignExist";

        public const string ASSIGNMENT_TOPIC_EDIT_SUCCESS = "mAssignmentTopicEditSuccess";
        public const string ASSIGNMENT_TOPIC_ADD_SUCCESS = "mAssignmentTopicAddSuccess";
        public const string ASSIGNMENT_TOPIC_DELETE_SUCCESS = "mAssignmentTopicDeleteSuccess";
        public const string ASSIGNMENT_TOPIC_ADD_FAIL = "mAssignmentTopicAddFail";
        public const string ASSIGNMENT_TOPIC_EDIT_FAIL = "mAssignmentTopicEditFail";
        public const string ASSIGNMENT_TOPIC_DELETE_FAIL = "mAssignmentTopicDeleteFail";
        // Error loading asset information
        public const string ASSIGNMENT_TOPIC_INFO_ERROR = "mAssignmentTopicInfoError";
        //â€ The Asset is successfully deactivatedâ€
        public const string ASSIGNMENT_TOPIC_DEACTIVATED = "mAssignmentTopicDeactivated";
        public const string ASSIGNMENT_TOPIC_ACTIVATED = "mAssignmentTopicActivated";
        //â€The Asset is already deactivated.â€
        public const string ASSIGNMENT_TOPIC_ALREADY_DEACTIVATED = "mAssignmentTopicAlreadyDeactivated";
        // Asset  with the same name already exists in the current Asset Folder. 
        public const string ASSIGNMENT_TOPIC_NAME_EXISTS = "mAssignmentNameExists";
    }

    public class AssignmentAttachment
    {
        public const string ATTACHMENTS_EDIT_SUCCESS = "mAttachmentEditSuccess";
        public const string ATTACHMENTS_ADD_SUCCESS = "mAttachmentAddSuccess";
        public const string ATTACHMENTS_DELETE_SUCCESS = "mAttachmentDeleteSuccess";
        public const string ATTACHMENTS_ADD_FAIL = "mAttachmentAddFail";
        public const string ATTACHMENTS_EDIT_FAIL = "mAttachmentEditFail";
        public const string ATTACHMENTS_DELETE_FAIL = "mAttachmentDeleteFail";
        public const string ATTACHMENTS_DEACTIVATED = "mAttachmentDeactivated";
        public const string ATTACHMENTS_ACTIVATED = "mAttachmentActivated";
        //â€The Asset is already deactivated.â€
        public const string ASSIGNMENTS_ALREADY_DEACTIVATED = "mAttachmentAlreadyDeactivated";
        public const string ASSIGNMENTS_ALREADY_ACTIVATED = "mAttachmentAlreadyActivated";
        public const string ASSIGNMENTS_BL_ERROR = "mAttachment_LIB_ERROR";
        public const string ASSIGNMENTS_DEL_ERROR = "mAttachment_LIB_DEL_ERROR";
        public const string ASSIGNMENTS_ASSIGN = "AttachmentAssignExist";
        // Asset  with the same name already exists in the current Asset Folder. 
        public const string ASSIGNMENT_NAME_EXISTS = "mAttachmentNameExists";
        //â€œFile with the name $ already exist in the Folder. While uploading the file, system will rename the file to # ."
        public const string FILE_EXISTS_RENAME_FOR_ADD = "mAttachment_FileExistsRenameForAdd";
        //â€œAsset Name is Required.â€
        public const string NAME_REQ = "mAttachment_NameRequired";
        //"Select an Asset File to upload."
        public const string FILE_REQ = "mAttachment_FileRequiredForUpload";
        //Assigned Asset cannot be deleted.
        public const string ATTACHMENT_CANT_BE_DELETED = "mAttachment_CantBeDeleted";

        // -  "Asset File should not be blank."
        public const string FILE_SHOULD_NOTBE_EMPTY = "mAssignmentFileShouldNotBeEmpty";
        //You do not have adequate rights to modify this Asset.
        public const string NO_FILE_EDIT_RIGHTS = "mAssignmentNoFileEditRights";
        //You do not have adequate rights to delete this Asset.
        public const string NO_FILE_DELETE_RIGHTS = "mAssignmentNoFileDeleteRights";
        //  Enter Valid Asset Name

        //Selected Asset File for upload is Invalid.
        public const string INVALID_ASSIGNMENT_TOPIC_FILE_SELECTED_FORUPLOAD = "mAssignmentInvalidAssignmentFileSelectedForUpload";
    }

    public class AssignmentNotes
    {
        public const string NOTES_EDIT_SUCCESS = "mNotesEditSuccess";
        public const string NOTES_ADD_SUCCESS = "mNotesAddSuccess";
        public const string NOTES_DELETE_SUCCESS = "mNotesDeleteSuccess";
        public const string NOTES_ADD_FAIL = "mNotesAddFail";
        public const string NOTES_EDIT_FAIL = "mNotesEditFail";
        public const string NOTES_DELETE_FAIL = "mNotesDeleteFail";
        public const string NOTES_DEACTIVATED = "mNotesDeactivated";
        public const string NOTES_ACTIVATED = "mNotesActivated";
        //â€The Asset is already deactivated.â€
        public const string NOTES_ALREADY_DEACTIVATED = "mNotesAlreadyDeactivated";
        public const string NOTES_ALREADY_ACTIVATED = "mNotesAlreadyActivated";
        public const string NOTES_BL_ERROR = "mNotes_LIB_ERROR";
        public const string NOTES_DEL_ERROR = "mNotes_LIB_DEL_ERROR";
        public const string NOTES_ASSIGN = "NotesAssignExist";
        // Asset  with the same name already exists in the current Asset Folder. 
        public const string NOTES_NAME_EXISTS = "mNotesNameExists";
        //â€œFile with the name $ already exist in the Folder. While uploading the file, system will rename the file to # ."
        public const string FILE_EXISTS_RENAME_FOR_ADD = "mNotes_FileExistsRenameForAdd";
        //â€œAsset Name is Required.â€
        public const string NAME_REQ = "mNotes_NameRequired";
        //"Select an Asset File to upload."
        public const string FILE_REQ = "mNotes_FileRequiredForUpload";
        //Assigned Asset cannot be deleted.
        public const string NOTES_CANT_BE_DELETED = "mNotes_CantBeDeleted";

        // -  "Asset File should not be blank."
        public const string FILE_SHOULD_NOTBE_EMPTY = "mNotesFileShouldNotBeEmpty";
        //You do not have adequate rights to modify this Asset.
        public const string NO_FILE_EDIT_RIGHTS = "mNotesNoFileEditRights";
        //You do not have adequate rights to delete this Asset.
        public const string NO_FILE_DELETE_RIGHTS = "mNotesNoFileDeleteRights";
        //  Enter Valid Asset Name

        //Selected Asset File for upload is Invalid.
        public const string INVALID_ASSIGNMENT_TOPIC_FILE_SELECTED_FORUPLOAD = "mNotesInvalidAssignmentFileSelectedForUpload";
    }
    public class OTP
    {
        public const string OTP_NUMBER_EMAIL = "mOTP_Number_Email";
        //Generated OTP Number send your email. Please enter same OTP Number.
        public const string OTP_EXPIRED = "mOTP_Expired";
        //OTP Number has been Expired.
        public const string INVALID_OTP = "mInvalid_OTP";
        //Invalid OTP Number.
        public const string USER_UNLOCK = "mUserUnlock";
        //Your Account Unlock and Send Email to you for Login details.
        public const string USER_LOCK = "mOTP_User_Lock";
        //Your Account has been lock. Please generate OTP Number.
        public const string ONLYUNLOCK_USER = "mOTP_OnlyUnLock_User";
        //Please enter only unlock user Login ID or Email Address.
        public const string LEARNER_EMAIL_NOTEXIST = "mLearnererEmailNotExist";
    }

    public class ActivityCertificateMess
    {
        public const string ActivityCertificate_ADD_FAILED = "mActivityCertificateAddFailed";
        public const string ActivityCertificate_ADD_SUCCESS = "mActivityCertificateAddSuccess";
        public const string ActivityCertificate_DEL_FAILED = "mActivityCertificateDelFailed";
        public const string ActivityCertificate_DEL_SUCCESS = "mActivityCertificateDelSuccess";
        public const string ActivityCertificate_ENTER = "mActivityCertificateEnter";
        public const string ActivityCertificate_UPDATE_FAILED = "mActivityCertificateUpdateFailed";
        public const string ActivityCertificate_UPDATE_SUCCESS = "mActivityCertificateUpdateSuccess";
        public const string ActivityCertificate_DAM_ERROR = "ActivityCertificate_DAM_ERROR";
        public const string ActivityCertificate_Allocated = "ActivityCertificate_Allocated";
        public const string ActivityCertificate_ALLOCATED_CANT_DELETED = "mAllocatedActivityCertificateCantDeleted";

        public const string ActivityCertificate_Activate_SUCCESS = "mActivityCertificateActivateSuccess";
        public const string ActivityCertificate_Deactivate_SUCCESS = "mActivityCertificateDeactivateSuccess";

        public const string ActivityCertificate_Sel = "mActivityCertificateSelect";
        public const string ActivityCertificate_RecommendedMessage = "mRecommendedSize";
    }

    public class ActivityCertificateMappingMess
    {
        public const string Activity_Certificate_MAPPING_DELETE_FAILED = "mActivityCertificateMappingDelFailed";
        public const string Activity_Certificate_MAPPING_DELETED = "mActivityCertificateMappingDelSuccess";
        public const string Activity_Certificate_MAPPING_Added = "mActivityCertificateMappingAdded";
        public const string Activity_Certificate_MAPPING_Updated = "mActivityCertificateMappingUpdated";
        public const string Activity_Certificate_MAPPING_Failed = "mActivityCertificateMappingFailed";
        public const string ActivityCertificate_Mapping_SelRecord = "mActivityCertificate_Mapping_SelRecord";
        public const string ActivityCertificate_DEL_SUCCESS = "mActivityCertificateDelSuccess";
        public const string ActivityCertificate_ENTER = "mActivityCertificateEnter";
        public const string ActivityCertificate_UPDATE_FAILED = "mActivityCertificateUpdateFailed";
        public const string ActivityCertificate_UPDATE_SUCCESS = "mActivityCertificateUpdateSuccess";
        public const string ActivityCertificate_DAM_ERROR = "ActivityCertificate_DAM_ERROR";
        public const string ActivityCertificate_Allocated = "ActivityCertificate_Allocated";
        public const string ActivityCertificate_ALLOCATED_CANT_DELETED = "mAllocatedActivityCertificateCantDeleted";

        public const string ActivityCertificate_Activate_SUCCESS = "mActivityCertificateActivateSuccess";
        public const string ActivityCertificate_Deactivate_SUCCESS = "mActivityCertificateDeactivateSuccess";

        public const string ActivityCertificate_Sel = "mActivityCertificateSelect";
    }

    #region AuditTrail

    public class AuditTrail
    {
        public const string ERROR_MSG_ID = "mAUDITTRAILERROR";
    }

    #endregion

    #region Chat

    public class Chat
    {
        public const string CHAT_ERROR = "mCHATERROR";
        public const string CHAT_DELETE_FAILED = "mChatDeleteFail";
        public const string CHAT_DELETED = "mChatDeleted";
        public const string PLZ_SEL_ATLEAST_1_CHAT = "mPlzSelAtleast1Chat";
    }
    public class Tax
    {
        public const string TAX_INVALIDADDRESS = "mInValidAddress";
        public const string TAX_DEFAULTLOCATION_NOTFOUND = "mDefaultLocationNotFound";
    }
    #endregion

    public class ProductLandingPageBanner
    {
        public const string PRODUCTLANDINGPAGEBANNER_DAM_ERROR = "mProduct_Landing_Banner_DAM_ERROR";
        public const string PRODUCTLANDINGPAGEBANNER_BANNER_ADDED = "Product_Landing_Banner_ADDED";
        public const string PRODUCTLANDINGPAGEBANNER_BANNER_UPDATED = "mProductLandingBannerUpdated";
        public const string PRODUCTLANDINGPAGEBANNER_BANNER_DELETED = "mProductLandingBannerDeleted";
        public const string PRODUCTLANDINGPAGEBANNER_BANNER_DELETED_FAILED = "mProductLandingBannerDeletedFailed";
    }
    public class ProductLandingPageBannerLanguage
    {
        public const string PRODUCTLANDINGPAGEBANNERLANGUAGE_DAM_ERROR = "Product_Landing_Banner_Language_DAM_ERROR";
    }

    public class ContentModuleMapping
    {
        public const string CONTENTMODULEMAPPING_DAM_ERROR = "mContent_Module_Mapping_DAM_ERROR";
        public const string CONTENTMODULEMAPPING_ERROR = "mContent_Module_Mapping_ERROR";
    }

    public class UserMasterSSO
    {
        public const string USERMASTERSSO_DAM_ERROR = "mUserMaster_SSO_DAM_ERROR";

    }
    #region LockUnlockCurriculumAssessment

    public class LockUnlockCurriculumAssessment
    {
        public const string ERROR_MSG_ID = "mLockUnlockCurriculumAssessmentERROR";
        public const string COURSEASSESSMENT_MASTERYSCORE_MSG = "CourseAssessment_MasteryScore_AlertMesg";
    }

    #endregion

    #region LockNumberOfAttemptsCourseAssessment

    public class LockNumberOfAttemptsCourseAssessment
    {
        public const string ERROR_MSG_ID = "mLockNumberOfAttemptsCourseAssessmentERROR";
    }

    #endregion
    #region UserContentAssessmentInteractionTracking

    public class UserContentAssessmentInteractionTracking
    {
        public const string CONT_MOD_TRACK_ERROR = "mContent_Mod_Tracking_ERR";
    }

    #endregion
    
    #region ILTModuleMaster

    public class ILTModuleMaster
    {
        public const string ILTMODULEMASTER_DAM_ERROR = "mILT_Mod_Master_ERR";
        public const string ILTMODULEMASTER_MOD_NAME_EXIST = "mILT_Module_Master_EXIST";
        public const string ILTMODULEMASTER_MOD_NAME_REQ = "mILT_Module_Master_REQ";
        public const string ILTMODULEMASTER_MOD_SEL_REQ = "mILT_Module_Master_SEL_REQ";
        public const string MODULE_IMPORT_SUCCESSS = "mILTModuleImportSuccess";
        public const string ILTMODULEMASTER_MOD_DAY_REQ = "mILT_Module_Day_REQ";
    }

    #endregion
    
    #region ILTManageEventMaster

    public class ILTManageEventMaster
    {
        public const string ILTMANAGEEVENTMASTER_DAM_ERROR = "mILT_Manage_Event_Master_ERR";
        public const string ILTMANAGEEVENTMASTER_EVENT_ADDED = "mILT_Manage_Event_Master_ADD";
        public const string ILTMANAGEEVENTMASTER_EVENT_UPDATE = "mILT_Manage_Event_Master_UPDATE";
        public const string ILTMANAGEEVENTMASTER_EVENT_FAIL = "mILT_Manage_Event_Master_FAIL";
        public const string ILTMANAGEEVENTMASTER_EVENT_NAME_EXIST = "mILT_Manage_Event_Master_EXIST";
        public const string ILTMANAGEEVENTMASTER_EVENT_NAME_REQ = "mILT_Manage_Event_Master_REQ";
        public const string ILTMANAGEEVENTMASTER_EVENT_COPY = "mILT_Manage_Event_Master_COPY";
    }


    #endregion    
    
    #region ILTSessionMaster
    public class ILTSessionMaster
    {
        public const string SessionMaster_DAM_ERROR = "mSessionMasterERROR";
        public const string SESSION_ADD_SUCESS = "mSessionAddSucess";
        public const string SESSION_IS_In_USE = "mSessionIsInUse";
        public const string SESSION_CANCEL = "mSession_Cancel";
        public const string SESSION_SELECT = "mSessionSelect";
        public const string SESSION_DELETE_SUCESS = "mSessionDelete";
        public const string SESSION_DELETE_ERROR = "mSessionDeleteError";
        public const string SESSION_UPDATE_SUCESS = "mSessionUpdateSucess";
        public const string SESSION_NAME_ALREADY_EXISTS = "mSessionNameAlreadyExists";
        public const string SESSION_MAX_REGISTRATION = "mEnterMaxRegistration";
        public const string SESSION_CLOSE = "mSessionClose";
        public const string SESSION_PLEASE_ADD = "mPleaseAdd";
        public const string SESSION_INVALID_START_DT = "mSessionInvalidStartDate";
        public const string SESSION_INVALID_END_DT = "mSessionInvalidEndDate";
        public const string SESSION_INVALID_DATES = "mSessionInvalidDates";
        public const string SESSION_NAME_COPY = "mSession_Name_COPY";
        public const string SESSION_ALREADY_CANCELED = "mSession_already_canceled";
        public const string SESSION_NO_AVAILABLE = "mSessionNoAvail";
        public const string SESSION_NO_NOT_AVAILABLE = "mSessionNoNotAvail";
        public const string SESSION_NO_ENTR = "mSessionNoEnt";

        public const string SESSION_IMPORT_SUCCESSS = "mILTSessionImportSuccess";
        public const string SESSION_IMPORT_ERROR = "mILTSessionImportError";
        public const string SESSION_AT_LEAST_ONE_SESSION_SHOULD_BE_ACTIVE = "mSessionAtLeastOneSessionShouldBeActive";
        public const string SESSION_STARTDATE_ENDDATE_SHOULD_BE_IN_EVENT_STARTEND_DATES = "mSessionStartDateEndDateShouldBeInEventStartEndDates";
        public const string SESSION_STARTDATE_ENDDATE_SHOULD_BE_GREATER_THAN_CURRENTDATE = "mSessionStartDateEndDateShouldBeGreaterThanCurrentDate";

        public const string SESSION_CANCLE_CONFIRMATION = "mSessionCancleConfirmation";
    }


    #endregion


    #region
    public class ILTBulkImport
    {
        public const string ERROR_WHILE_BLK_IMP_SESSION = "mErrorWhileBlkImptSession";
    }
    #endregion

   #region ILTSessionModuleAllocatedResources
        public class SessionModuleAllocatedResources
    {
        //Module
        public const string ModuleAllocatedResources_DAM_ERROR = "mModuleAllocatedResourcesERROR";
        public const string MODULEALLOCATEDRESOURCES_ADD_SUCCESS = "mModuleAllocatedResourcesAddSuccess";
        public const string MODULEALLOCATEDRESOURCES_ADD_FAILED = "mModuleAllocatedResourcesAddFailed";
        public const string MODULEALLOCATEDRESOURCES_REMOVE_SUCCESS = "mModuleAllocatedResourcesRemoveSuccess";
        public const string MODULEALLOCATEDRESOURCES_UNMAP_SUCCESS = "mModuleAllocatedResourcesUnmapSuccess";
        //Session
        public const string SessionAllocatedResources_DAM_ERROR = "mSessionAllocatedResourcesERROR";
        public const string SESSIONALLOCATEDRESOURCES_ADD_SUCCESS = "mSessionAllocatedResourcesAddSuccess";
        public const string SESSIONALLOCATEDRESOURCES_ADD_FAILED = "mSessionAllocatedResourcesAddFailed";
        public const string SESSIONALLOCATEDRESOURCES_REMOVE_SUCCESS = "mSessionAllocatedResourcesRemoveSuccess";
        public const string SESSIONALLOCATEDRESOURCES_UNMAP_SUCCESS = "mSessionAllocatedResourcesUnmapSuccess";
    }
    #endregion

    #region ILTNominationMaster

    public class ILTNominationMaster
    {
        public const string ILTNOMINATIONMASTER_DAM_ERROR = "mILT_Nomination_Master_ERR";
        public const string ILTNOMINATIONMASTER_USER_ADDED = "mILT_Nomination_Master_ADD";
        public const string ILTNOMINATIONMASTER_USER_SELECT = "mILT_Nomination_Master_SEL";
        public const string ILTNOMINATIONMASTER_REMOVE_REGISTERED_USER = "mILT_Nomination_Master_Rmove_RegUsr";
        public const string USER_REG_ATTD_IMPORT_SUCCESSS = "mILTUsrRegAttImportSuccess";
    }

    #endregion

    #region ILTAttendanceMaster

    public class ILTAttendanceMaster
    {
        public const string ILTATTENDANCEMASTER_DAM_ERROR = "mILT_Attendance_Master_ERR";
        public const string ILTATTENDANCEMASTER_EVENT_ATNDNC_SUCCESS = "mILT_Attendance_Event_MarkAllPresent_Success";
        public const string ILTATTENDANCEMASTER_EVENT_PARTIALLY_SESSION = "mILT_Attendance_Event_Partially_Session";
        public const string ILTATTENDANCEMASTER_ATNDNC_DATE_VALIDATION_EVENT = "mILT_Attendance_Event_Atndnc_Date_Validation";
        public const string ILTATTENDANCEMASTER_EVENT_NO_SESSION = "mILT_Attendance_Event_No_Session";

        public const string ILTATTENDANCEMASTER_SESSION_ATNDNC_SUCCESS = "mILT_Attendance_Session_MarkAllPresent_Success";
        public const string ILTATTENDANCEMASTER_ATNDNC_DATE_VALIDATION_SESSION = "mILT_Attendance_Session_Atndnc_Date_Validation";

        public const string ILTATTENDANCEMASTER_SESSION_INDVL_ATNDNC_SUCCESS = "mILT_Attendance_Session_Individual_Success";
        public const string ILTATTENDANCEMASTER_MODULE_INDVL_ATNDNC_SUCCESS = "mILT_Attendance_Module_Individual_Success";

    }

    #endregion    
}