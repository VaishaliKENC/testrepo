/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh 
* Created:08/10/09
* Last Modified:<dd/mm/yy>
*/
using System;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class AutoEmailTemplateSetting : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public AutoEmailTemplateSetting()
        { }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetEmailTempId,
            Update
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkUpdate
        }

        private string _autoEmailEventId;
        public string AutoEmailEventId
        {
            get { return _autoEmailEventId; }
            set { if (this._autoEmailEventId != value) { _autoEmailEventId = value; } }
        }

        private string _eventName;
        public string EventName
        {
            get { return _eventName; }
            set { if (this._eventName != value) { _eventName = value; } }
        }

        private string _emailTemplateID;
        public string EmailTemplateID
        {
            get { return _emailTemplateID; }
            set { if (this._emailTemplateID != value) { _emailTemplateID = value; } }
        }

        private string _featureId;
        public string FeatureId
        {
            get { return _featureId; }
            set { if (this._featureId != value) { _featureId = value; } }
        }

        private bool _bIsRecurrenceApprovalRequired;
        public bool IsRecurrenceApprovalRequired
        {
            get { return _bIsRecurrenceApprovalRequired; }
            set { if (this._bIsRecurrenceApprovalRequired != value) { _bIsRecurrenceApprovalRequired = value; } }
        }

        #region EVENT ID CONSTANTS
        public const string EVENT_LEARNER_FORGOT_PASSWORD = "AEEVT00001";
        public const string EVENT_LEARNER_CHANGE_PASSWORD = "AEEVT00002";
        public const string EVENT_LEARNER_SELF_REGISTRATION_PASSCODE = "AEEVT00003";
        public const string EVENT_LEARNER_SELF_REGISTRATION_EMAIL = "AEEVT00004";
        public const string EVENT_LEARNER_SELF_REGISTRATION_MALE = "AEEVT00041";  //Customised for benecke web service - Male
        public const string EVENT_LEARNER_SELF_REGISTRATION_FEMALE = "AEEVT00042"; //Customised for benecke web service - Female

        public const string EVENT_LEARNER_ADMIN_ADD_INTERFACE = "AEEVT00005";
        public const string EVENT_LEARNER_ADD_BULK_IMPORT = "AEEVT00006";
        public const string EVENT_LEARNER_ACTIVATE = "AEEVT00007";
        public const string EVENT_LEARNER_DEACTIVATE = "AEEVT00008";
        public const string EVENT_LEARNER_ASSIGNMENT = "AEEVT00009";//	Learner Assignments
        public const string EVENT_LEARNER_UNASSIGNMENT = "AEEVT00010";//	Learner un-assignment
        public const string EVENT_LEARNER_DUEDATE_REMINDER = "AEEVT00011";//	Due-date reminders
        public const string EVENT_ADMIN_ROLE_ASSIGNMENT = "AEEVT00012";//	Administrator role assignment
        public const string EVENT_ADMIN_ROLE_UN_ASSIGNMENT = "AEEVT00013";//	Administrator role un-assignment
        public const string EVENT_CLIENT_CREATION = "AEEVT00014";//	Client Account Creation
        public const string EVENT_CLIENT_NEW_COURSE_ALLOCATION = "AEEVT00015";//	New Courses Allocation to a client
        public const string EVENT_CLIENT_CONTRACT_END_DATE_REMINDER = "AEEVT00016";//	Client Contract End date Reminder
        public const string EVENT_CLIENT_COURSE_LIC_DUE_DATE_REMINDER = "AEEVT00017";//	Course Licenses due dates Reminder
        public const string EVENT_ADMIN_POLICY_ADD_UPS = "AEEVT00018";//	Policy Document Add and Update 
        public const string EVENT_ADMIN_REVIEW_CER_RESPONSE_ALERT = "AEEVT00019";//	Review Certification Response Alert
        public const string EVENT_ADMIN_QUESTIONNAIRE_SEND_APPROVAL = "AEEVT00020";//	Questionnaire SendForApproval
        public const string EVENT_ADMIN_QUESTIONNAIRE_REVIEW = "AEEVT00021";//	Questionnaire AdministratorReviewMail
        public const string EVENT_LEARNER_FEEDBACK = "AEEVT00022";//	Learner FeedBack
        public const string EVENT_COMMON_FEEDBACK = "AEEVT00023";//	Public Feedback 
        public const string EVENT_ADMIN_SENDPASSCODE = "AEEVT00024";//	Send Passcodes
        public const string EVENT_ADMIN_SEND_IDF = "AEEVT00025";//Import Definition File
        public const string EVENT_ADMIN_FEEDBACK = "AEEVT00026";//Admin Feedback
        public const string EVENT_ROLE_ASSIGNMENT = "AEEVT00027";//Role Assignment
        public const string EVENT_ROLE_UNASSIGNMENT = "AEEVT00028";//Role UnAssignment
        public const string EVENT_LEARNER_PROFILE_UPDATE = "AEEVT00029";//Learner Profile Update
        public const string EVENT_ADMIN_ASSESSMENT_REVIEW = "AEEVT00030";//	Assessment AdministratorReviewMail
        public const string EVENT_ADMIN_ASSESSMENT_SEND_APPROVAL = "AEEVT00031";//	Assessment SendForApproval
        public const string EVENT_ASSESSMENT_RESULT = "AEEVT00032";//	Assessment Result
        public const string EVENT_PASSCODE_EMAIL = "EMT0001";//	Passcode email
        public const string ILT_LEARNER_REGISTRATION_CANCELLED_FOR_SESSION = "AEEVT00078";//Learner cancelled reg session.

        public const string ESTORE_ORDER_RECEIPT = "AEEVT00038";//estore order receipt
        public const string ESTORE_ORDER_RECEIPT_ADMIN = "AEEVT00039";//estore order receipt admin
        public const string ESTORE_ORDER_USER_CREATION = "AEEVT00040";//estore order -user creation
        public const string ESTORE_ORDER_OFFLINE_PAYMENT = "AEEVT00057";//estore order -offline payment

        public const string EVENT_BULKIMPORT_QUESTION_ADDITION = "AEEVT00041";
        public const string ESTORE_LEARNER_PRODUCT_REGISTER_REQUEST = "AEEVT00043";//estore user product request
        public const string ESTORE_LEARNER_PRODUCT_REGISTER_REQUEST_APPROVED = "AEEVT00044";//estore user product request APPROVE
        public const string ESTORE_LEARNER_PRODUCT_REGISTER_REQUEST_REJECTED = "AEEVT00045";//estore user product request REJECTED
        public const string ESTORE_Catalog_Ask_a_Question_Pop_Up = "AEEVT00058";//estore Catalog Ask a Question Pop Up
        public const string ESTORE_Catalog_Product_Inquiry = "AEEVT00059";//estore LEARNER Catalog Product Inquiry


        public const string EVENT_USER_NOMINATION_STATUS_CHANGE_BY_ADMIN = "AEEVT00033";//Change by Admin Approved/Reject/Waitlist
        public const string EVENT_USER_NOMINATION_BY_ADMIN_WITH_STATUS = "AEEVT00034";//Add Nomination by Admin first time
        public const string EVENT_USER_SELF_NOMINATION_WITH_AUTO_APPROVED = "AEEVT00035";//User Self Nomination with auto Approved
        public const string EVENT_USER_SELF_NOMINATION_WITH_STATUS_PENDING_OR_WAITLIST = "AEEVT00036";//User Self Nomination with status pending or waitlist
        public const string EVENT_USER_SELF_NOMINATION_STATUS_SEND_TO_ADMIN = "AEEVT00037";//User Self Nomination status send to Admin
        public const string EVENT_LEARNER_OFFLINE_COURSE_REG = "AEEVT00050";
        public const string EVENT_OTP_NUMBER = "AEEVT00051";//OTP Number send to user
        public const string EVENT_USER_UNLOCKED = "AEEVT00052";//User Account Unlocked send to user
        public const string EVENT_SELF_REGI_STUDENT = "AEEVT00053";//Self registration notification email for Student
        public const string EVENT_SELF_REGI_ADMIN = "AEEVT00054";//Self registration notification email for Admin
        public const string EVENT_SELF_REGI_STUDENT_APPROVED = "AEEVT00055";//Self registration Approved
        public const string EVENT_SELF_REGI_STUDENT_REJECTED = "AEEVT00056";//Self registration Approved
        public const string EVENT_Share_By_Email = "AEEVT00060";//Self registration Approved

        public const string EVENT_User_Creation_Product_Admin = "AEEVT00061";//Order user creation - product admin
        public const string EVENT_LEARNER_PASSWORD_RESET = "AEEVT00062";//Password reset
        public const string EVENT_DISCOUNT_COUPON_DISTRIBUTION = "AEEVT00063";//Discount coupon distribution
        public const string EVENT_ESTORE_INCOMPLETE_ORDER_NOTIFICATION = "AEEVT00064";//eStore incomplete order notification
        public const string EVENT_ESTORE_COMPLETE_ORDER_NOTIFICATION = "AEEVT00065";//eStore complete order notification
        public const string EVENT_ASSESSMENT_FIRST_FAIL_LOCKED = "AEEVT00066";
        public const string EVENT_LEARNER_UNLOCKED_ASSIGNMENT = "AEEVT00067";
        public const string EVENT_ASSESSMENT_SECOND_FAIL_LOCKED = "AEEVT00068";
        public const string EVENT_ASSESSMENT_ATTEMPT_FAILED = "AEEVT00069";
        public const string EVENT_ASSESSMENT_ATTEMPT_FAILED_LOCKED = "AEEVT00070";
        public const string EVENT_CERTIFICATION_PROGRAM_APPROVAL_EMAIL = "AEEVT00071";
        public const string EVENT_LEARNER_HELPDESK = "AEEVT00072";
        public const string EVENT_LEARNER_HELPDESK_CONFIRMATION = "AEEVT00073";
        public const string EVENT_ADMIN_CANCEL_SESSION = "AEEVT00079";
        public const string EVENT_LEARNER_CANCEL_SESSION = "AEEVT00078";
        public const string EVENT_ADD_NOMINATION = "AEEVT00075";
        public const string EVENT_REJECT_NOMINATION = "AEEVT00076";
        public const string EVENT_MARK_ATTENDANCE = "AEEVT00074";
        #endregion
    }
}
