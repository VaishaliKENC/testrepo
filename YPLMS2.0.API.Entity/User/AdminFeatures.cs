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
    /// Admin Features Class 
    /// </summary>
    [Serializable]
    public class AdminFeatures : BaseEntity
    {
        #region CONSTANT FEATURE IDS
        public const string CACHE_SUFFIX = "_FEATURES";

        /// <summary>
        /// Client Management
        /// </summary>
        public const string FEA_ID_CLIENT_MNGT = "FEA0001";
        /// <summary>
        /// Client Site Set-up'
        /// </summary>
        public const string FEA_ID_CLIENT_SETUP = "FEA0002";
        /// <summary>
        /// Search Client
        /// </summary>
        //public const string FEA_ID_CLIENT_SEARCH = "FEA0003";
        /// <summary>
        /// Allocate Course Licenses
        /// </summary>
        public const string FEA_ID_ALLOCATE_COURSE_LIC = "FEA0004";
        /// <summary>
        /// Set Predefined Roles
        /// </summary>
        public const string FEA_ID_SET_ROLES = "FEA0005";
        /// <summary>
        /// Perform Client Site Activities
        /// </summary>
        public const string FEA_ID_PERFORM_CLIENT_ACT = "FEA0006";
        /// <summary>
        /// Client UserName/Password/Launch Site
        /// </summary>
        public const string FEA_ID_LAUNCH_SITE = "FEA0007";
        /// <summary>
        /// Languages
        /// </summary>
        public const string FEA_ID_LANGUAGES = "FEA0008";
        /// <summary>
        /// Client Default Language
        /// </summary>
        public const string FEA_ID_CLIENT_DEFAULT_LNG = "FEA0009";
        /// <summary>
        /// User Management
        /// </summary>
        public const string FEA_ID_USER_MNGT = "FEA0010";
        /// <summary>
        /// Register User Through Interface
        /// </summary>
        public const string FEA_ID_REG_USER_FORM = "FEA0011";
        /// <summary>
        /// Register User Through Bulk Import
        /// </summary>
        public const string FEA_ID_REG_USER_BULK = "FEA0012";
        /// <summary>
        /// Custom Fields
        /// </summary>
        public const string FEA_ID_CUSTOM_FLDS = "FEA0013";
        /// <summary>
        /// BUSINESS_RULE
        /// </summary>
        public const string FEA_ID_BUSINESS_RULE = "FEA0014";
        /// <summary>
        /// BUSINESS_RULE
        /// </summary>
        public const string FEA_ID_BUSINESS_RULE_COMPONANT = "FEA0368";
        /// <summary>
        /// Organization Tree
        /// </summary>
        public const string FEA_ID_ORG_TREE = "FEA0015";
        /// <summary>
        /// Organization Tree
        /// </summary>
        public const string FEA_ID_ORG_TREE_COMPONANT = "FEA0369";
        /// <summary>
        /// Roles & Associate Rights
        /// </summary>
        public const string FEA_ID_ROLES_RIGHTS = "FEA0016";
        /// <summary>
        /// Search Users
        /// </summary>
        //public const string FEA_ID_SEARCH_USERS = "FEA0017";
        /// <summary>
        /// Content Management
        /// </summary>
        public const string FEA_ID_CONTENT_MNGT = "FEA0018";
        /// <summary>
        /// Course Library
        /// </summary>
        public const string FEA_ID_COURSE_LIB = "FEA0019";
        /// <summary>
        /// Assets Library
        /// </summary>
        public const string FEA_ID_ASSET_LIB = "FEA0020";
        /// <summary>
        /// Policy Library
        /// </summary>
        public const string FEA_ID_POLICY_LIB = "FEA0021";
        /// <summary>
        /// Questionnaires
        /// </summary>
        public const string FEA_ID_QUESTIONNAIRES = "FEA0022";
        /// <summary>
        /// manage questionnaires
        /// </summary>
        public const string FEA_ID_QUESTIONNAIRES_MANAGE = "FEA0023";
        /// <summary>
        /// questions
        /// </summary>
        public const string FEA_ID_QUESTIONS = "FEA0024";
        /// <summary>
        /// Bulk Import Questions
        /// </summary>
        //public const string FEA_ID_QUESTIONS_BULK_IMPORT = "FEA0025";
        ///// <summary>
        ///// Define question sequencing
        ///// </summary>
        //public const string FEA_ID_QUESTIONS_DEFINE_SEQ = "FEA0026";
        /// <summary>
        /// Set up alerts
        /// </summary>
        //public const string FEA_ID_SETUP_ALERT = "FEA0027";
        /// <summary>
        /// Configure questionnaires
        /// </summary>
        public const string FEA_ID_QUESTIONNAIRES_CONFIG = "FEA0028";
        /// <summary>
        /// Translate questionnaires
        /// </summary>
        public const string FEA_ID_QUESTIONNAIRES_TRANSLATE = "FEA0029";
        /// <summary>
        /// ASSESSMENT
        /// </summary>
        public const string FEA_ID_ASSESSMENT = "FEA0336";
        
        //Comment by Kunal because keys are not used but values are assigned to different keys
        
        ///// <summary>
        ///// manage ASSESSMENT
        ///// </summary>
        //public const string FEA_ID_ASSESSMENT_MANAGE = "FEA0337";
        ///// <summary>
        ///// Configure ASSESSMENT
        ///// </summary>
        //public const string FEA_ID_ASSESSMENT_CONFIG = "FEA0338";
        ///// <summary>
        ///// Translate ASSESSMENT
        ///// </summary>
        //public const string FEA_ID_ASSESSMENT_TRANSLATE = "FEA0339";
        
        //End Comment

        /// <summary>
        /// Policy Certification 
        /// </summary>
        public const string FEA_ID_POLICY_CERT = "FEA0030";
        /// <summary>
        /// Create policy certifications
        /// </summary>
        public const string FEA_ID_POLICY_CERT_CREATE = "FEA0031";
        /// <summary>
        /// Define recertification paths
        /// </summary>
        public const string FEA_ID_DEFINE_RECERT_PATH = "FEA0032";
        /// <summary>
        /// Certify by proxy for offline users
        /// </summary>
        //public const string FEA_ID_CERT_BY_PROXY = "FEA0033";
        /// <summary>
        /// Curriculum Plans Parent
        /// </summary>
        public const string FEA_ID_CURIM_PLAN_PARENT = "FEA0034";
        /// <summary>
        /// Curriculum Plans
        /// </summary>
        public const string FEA_ID_CURIM_PLAN = "FEA0035";
        /// <summary>
        /// Learning Activities
        /// </summary>
        //public const string FEA_ID_LEANR_ACT = "FEA0036";
        /// <summary>
        /// Order and Sequencing Curriculum Plans
        /// </summary>
        //public const string FEA_ID_CURIM_PLAN_ORDER_SEQ = "FEA0037";
        /// <summary>
        /// Reports
        /// </summary>
        public const string FEA_ID_REPORTS = "FEA0038";
        /// <summary>
        /// Standard Reports
        /// </summary>
        public const string FEA_ID_REPORTS_STND = "FEA0039";
        /// <summary>
        /// Ethical Moments Reports
        /// </summary>
        ////public const string FEA_ID_REPORTS_ETHICAL = "FEA0040";
        //// <summary>
        //// Client Contact Report
        //// </summary>
        public const string FEA_ID_REPORTS_CLIENT_CONTACT = "FEA0041";
        //// <summary>
        //// Client Summary Report
        //// </summary>
        public const string FEA_ID_REPORTS_CLIENT_SUM = "FEA0042";
        //// <summary>
        //// Course Catalog/Library Report
        //// </summary>
        public const string FEA_ID_REPORTS_COURSE_CATA = "FEA0043";
        //// <summary>
        //// Full Licensing Report - Group by Client
        //// </summary>
        public const string FEA_ID_REPORTS_FULL_LIC_CLIENT = "FEA0044";
        //// <summary>
        //// Full Licensing Report - Group by Course
        //// </summary>
        public const string FEA_ID_REPORTS_FULL_LIC_COURSE = "FEA0045";
        //// <summary>
        //// Client Course Report 
        //// </summary>
        ////public const string FEA_ID_REPORTS_CLIENT_COURSE = "FEA0046";
        //// <summary>
        ////  Client Learner Report 
        //// </summary>
        ///public const string FEA_ID_REPORTS_CLIENT_LEARNER = "FEA0047";
        //// <summary>
        //// Certification Reports
        //// </summary>
        public const string FEA_ID_REPORTS_CERT = "FEA0310";

        /// <summary>
           public const string FEA_ID_REPORTS_DQRT = "FEA0384";
        /// </summary>
        //// <summary>
        //// Client Learner Dump Report 
        //// </summary>
        public const string FEA_ID_REPORTS_LEARNER_DUMP = "FEA0312";
        //// <summary>
        //// User Transcript Report
        //// </summary>
        public const string FEA_ID_USER_TRANSCRIPT = "FEA0334";
        //// <summary>
        //// Delinquency History Report 
        //// </summary>
        public const string FEA_ID_REPORTS_DELINQUENCY = "FEA0314";
        //// <summary>
        //// Activity Completion Progress Report - Single Course
        //// </summary>
        public const string FEA_ID_REPORTS_COURSE_STATUS = "FEA0051";
        //// <summary>
        ////  Activity Completion Progress Report - Multiple Course 
        //// </summary>
        public const string FEA_ID_REPORTS_COURSE_ROSTER = "FEA0052";
        //// <summary>
        ////Course Summary Report
        //// </summary>
        ////public const string FEA_ID_REPORTS_COURSE_SUMMARY = "FEA0053";
        //// <summary>
        //// Dashboard reports 
        //// </summary>
        public const string FEA_ID_REPORTS_DASHBOARD = "FEA0054";
        //// <summary>
        //// Status Details Report 
        //// </summary>
        ////public const string FEA_ID_REPORTS_STATUS_DETAILS = "FEA0055";
        //// <summary>
        //// Student List by Organizational Group 
        //// </summary>
        public const string FEA_ID_REPORTS_LEARNER_DATA = "FEA0313";
        //// <summary>
        //// Learner Summary Report
        //// </summary>
        ////public const string FEA_ID_REPORTS_LEARNER_SUMM = "FEA0057";
        //// <summary>
        //// Regional view Learner report 
        //// </summary>
        ////public const string FEA_ID_REPORTS_LEARNER_REGION_VIEW = "FEA0058";
        //// <summary>
        //// Regional view History Report'
        //// </summary>
        ////public const string FEA_ID_REPORTS_LEARNER_REGION_HISTORY = "FEA0059";
        /// <summary>
        /// Custom Reports 
        /// </summary>
        public const string FEA_ID_REPORTS_CUSTOM = "FEA0060";
        /// <summary>
        /// Reporting Tool
        /// </summary>
        public const string FEA_ID_REPORTS_TOOL = "FEA0061";
        /// <summary>
        /// Aggregate Results by Question
        /// </summary>
        public const string FEA_ID_REPORTS_AGG_RES_BY_QUE = "FEA0309";
        /// <summary>
        /// Incidences of Non-Preferred Answers
        /// </summary>
        public const string FEA_ID_REPORTS_INS_OF_NON_PRF = "FEA0311";
        /// <summary>
        /// Activity Completion Progress Report
        /// </summary>
        public const string FEA_ID_REPORTS_ACT_COMP_PROGRESS = "FEA0315";
        /// <summary>
        /// View Assigned Users by Activity
        /// </summary>
        public const string FEA_ID_REPORTS_ASS_ACT_USERS = "FEA0316";

        /// Course Assessment Interaction Report
        /// </summary>
        public const string FEA_ID_REPORTS_COURSE_ASS_INTERACTION = "FEA0436";
        /// <summary>
        /// View Not Assigned Users by Activity
        /// </summary>
        public const string FEA_ID_REPORTS_NOT_ASS_ACT_USERS = "FEA0317";
        /// <summary>
        /// View Users by Role
        /// </summary>
        public const string FEA_ID_REPORTS_USERS_BY_ROLE = "FEA0318";
        //// <summary>
        //// Pending Review Email Report 
        //// </summary>
        public const string FEA_ID_REPORTS_PENDINGREVIEW = "FEA0335";
        /// <summary>
        /// Assignment
        /// </summary>
        public const string FEA_ID_REPORTS_EVENTDETAIL= "FEA0439";
        public const string FEA_ID_ASSIGNMENT = "FEA0062";
        /// <summary>
        /// Assignment  Through Interface
        /// </summary>
        //public const string FEA_ID_ASSIGNMENT_INTERFACE = "FEA0063";
        /// <summary>
        /// Multiple Learner To Single Assignment
        /// </summary>
        /// public const string FEA_ID_MULTI_LEARNER_ASSIGNMENT_ = "FEA0064";
        public const string FEA_ID_MULTI_LEARNER_ASSIGNMENT = "FEA0301";
        public const string FEA_ID_MANAGE_LOCK_CURRICULUM_ASSESSMENT = "FEA0432";
        /// <summary>
        /// bulk import Assignment 
        /// </summary>
        public const string FEA_ID_BULK_IMPORT_ASSIGNMENT = "FEA0367";

        /// <summary>
        /// Single Learner To Multiple Assignment 
        /// </summary>
        public const string FEA_ID_SINGLE_LEARNER_MULTI_ASSIGNMENT = "FEA0065";
        /// <summary>
        /// Unassignment 
        /// </summary>
        //public const string FEA_ID_SINGLE_LEARNER_MULTI_UNASSIGNMENT = "FEA0066";
        ///// <summary>
        ///// Reassignment
        ///// </summary>
        //public const string FEA_ID_SINGLE_LEARNER_MULTI_REASSIGNMENT = "FEA0067";
        /// <summary>
        /// Assignment  Through Bulk Import
        ///// </summary>
        //public const string FEA_ID_MANAGE_ASSIGNMENT = "FEA0068";
        /// <summary>
        /// Unassignment 
        /// </summary>
        //public const string FEA_ID_BULK_IMPORT_UNASSIGNMENT = "FEA0069";
        ///// <summary>
        /////  Reassignment
        ///// </summary>
        //public const string FEA_ID_BULK_IMPORT_REASSIGNMENT = "FEA0070";
        /// <summary>
        /// Assignment  Through Business Rule
        /// </summary> 
        /// public const string FEA_ID_BUSINESS_RULE_ASSIGNMENT = "FEA0071";
        public const string FEA_ID_BUSINESS_RULE_ASSIGNMENT = "FEA0302";
        /// <summary>
        /// Unassignment 
        /// </summary>
        //public const string FEA_ID_BUSINESS_RULE_UNASSIGNMENT = "FEA0072";
        ///// <summary>
        ///// Reassignment
        ///// </summary>
        //public const string FEA_ID_BUSINESS_RULE_REASSIGNMENT = "FEA0073";
        /// <summary>
        /// Record Completion Through Interface
        /// </summary>
        public const string FEA_ID_RECORD_COMPLETION_TH_INTERFACE = "FEA0074";
        ///// <summary>
        ///// Record Completion Through Bulk Import
        ///// </summary>
        public const string FEA_ID_RECORD_COMPLETION_TH_BULK_IMPORT = "FEA0075";
        /// <summary>
        /// Emails
        /// </summary>
        public const string FEA_ID_EMAILS = "FEA0076";
        /// <summary>
        /// Email Template
        /// </summary>
        public const string FEA_ID_EMAIL_TEMPLATE = "FEA0077";
        /// <summary>
        /// Email Distributions
        /// </summary>
        public const string FEA_ID_EMAIL_DISTRIBUTION = "FEA0078";
        /// <summary>
        /// Email Messages
        /// </summary>
        public const string FEA_ID_EMAIL_MESSAGES = "FEA0115";//"FEA0079";
        /// <summary>
        /// System Configuration 
        /// </summary>
        public const string FEA_ID_SYSTEM_CONFIG = "FEA0080";
        /// <summary>
        /// UI (User Interface)
        /// </summary>
        public const string FEA_ID_UI = "FEA0081";
        /// <summary>
        /// Site Logo
        /// </summary>
        public const string FEA_ID_SITE_LOGO = "FEA0082";
        /// <summary>
        /// Color Theme
        /// </summary>
        public const string FEA_ID_COLOR_THEME = "FEA0083";
        /// <summary>
        /// Layout (Theme) Template
        /// </summary>
        public const string FEA_ID_LAYOUT = "FEA0084";
        /// <summary>
        /// System
        /// </summary>
        public const string FEA_ID_SYSTEM = "FEA0085";
        /// <summary>
        /// Authentication
        /// </summary>
        public const string FEA_ID_AUTHENTICATION = "FEA0086";
        /// <summary>
        /// Sessions
        /// </summary>
        public const string FEA_ID_SESSION = "FEA0087";
        /// <summary>
        /// E-mail Server
        /// </summary>
        public const string FEA_ID_EMAIL_SERVER = "FEA0088";
        /// <summary>
        /// Forget Password On/Off
        /// </summary>
        public const string FEA_ID_FORGOT_PWD_ON_OFF = "FEA0089";
        /// <summary>
        /// Lock/Unlock System
        /// </summary>
        public const string FEA_ID_SYSTEM_LOCK_UNLOCK = "FEA0090";
        /// <summary>
        /// Password Policy
        /// </summary>
        public const string FEA_ID_PWD_POLICY = "FEA0091";
        /// <summary>
        /// Logout Redirect URL''s'
        /// </summary>
        public const string FEA_ID_LOGOUT_URL = "FEA0092";
        /// <summary>
        /// Publish Policy Settings
        /// </summary>
        public const string FEA_ID_PUBL_POLICY_SETTINGS = "FEA0093";
        /// <summary>
        /// Export / Import System Interface Language Files
        /// </summary>
        public const string FEA_ID_IMP_EXP_SYS_INTERFACE_LANG_FILE = "FEA0094";
        /// <summary>
        /// Web part Setting
        /// </summary>
        public const string FEA_ID_WEB_PART_SETT = "FEA0095";
        /// <summary>
        /// Organization Hierarchy Settings
        /// </summary>
        public const string FEA_ID_ORG_HIER_SETT = "FEA0096";
        /// <summary>
        /// Feedback Page On/Off
        /// </summary>
        public const string FEA_ID_FEEDBACK_PAGE_ON_OFF = "FEA0097";
        /// <summary>
        /// HELP  On/Off
        /// </summary>
        public const string FEA_ID_HELP = "FEA0098";
        /// <summary>
        /// Course
        /// </summary>
        public const string FEA_ID_COURSE = "FEA0099";
        /// <summary>
        /// Course Book Marking
        /// </summary>
        public const string FEA_ID_COURSE_BOOK_MRKT = "FEA0100";
        /// <summary>
        /// Score Tracking
        /// </summary>
        public const string FEA_ID_SCORE_TRACK = "FEA0101";
        /// <summary>
        /// Course Launching Screen Settings
        /// </summary>
        public const string FEA_ID_COURSE_LAUNCH_SETT = "FEA0102";
        /// <summary>
        /// User
        /// </summary>
        public const string FEA_ID_USER = "FEA0103";
        /// <summary>
        /// Learner Profile Access
        /// </summary>
        public const string FEA_ID_LEARNER_PROF_ACCESS = "FEA0104";
        /// <summary>
        /// Self Registration User
        /// </summary>
        public const string FEA_ID_SELF_REGIST_USER = "FEA0105";
        /// <summary>
        /// Pages
        /// </summary>
        public const string FEA_ID_PAGES = "FEA0106";
        /// <summary>
        /// Login Page
        /// </summary>
        public const string FEA_ID_LOGIN_PAGE = "FEA0107";
        /// <summary>
        /// Welcome Page
        /// </summary>
        public const string FEA_ID_WELCOME_PAGE = "FEA0108";
        /// <summary>
        /// Feedback Page 
        /// </summary>
        public const string FEA_ID_FEEDBACK_PAGE = "FEA0109";
        /// <summary>
        /// Self Registration Page
        /// </summary>
        public const string FEA_ID_SELF_REGIST_PAGE = "FEA0110";
        /// <summary>
        /// Schedule Report
        /// </summary>
        public const string FEA_ID_SCHEDULE_REPORT = "FEA0132";

        /// <summary>
        /// //Login page Image'
        /// </summary>
        public const string FEA_ID_LOGIN_IMG = "FEA0111";
        /// <summary>
        /// Welcome Image
        /// </summary>
        public const string FEA_ID_WELCOME_IMG = "FEA0112";
        /// <summary>
        /// Auto Task
        /// </summary>
        public const string FEA_ID_AUTO_TASK = "FEA0113";
        /// <summary>
        /// Auto Task Reports
        /// </summary>
        public const string FEA_ID_REPORTS_AUTO_TASK = "FEA0114";
        /// <summary>
        /// Auto Task Emails'
        /// </summary>
        public const string FEA_ID_EMAILS_AUTO_TASK = "FEA0079";//"FEA0115";
        /// <summary>
        /// Auto Task Assignments'
        /// </summary>
        //public const string FEA_ID_ASSIGNMENTS_AUTO_TASK = "FEA0116";
        /// <summary>
        /// Perform as a Site Administrator
        /// </summary>
        public const string FEA_ID_PERFORM_SITE_ADMIN = "FEA0117";

        //New Ids

        /// <summary>
        /// A/V Path Setting
        /// </summary>
        public const string FEA_ID_AV_PATH_SETTING = "FEA0201";

        /// <summary>
        /// Single  Sign-On Setting
        /// </summary>
        public const string FEA_ID_SINGLE_SON_SETTING = "FEA0202";

        /// <summary>
        /// Passcode Setting
        /// </summary>
        public const string FEA_ID_PASSCODE_SETTING = "FEA0134";


        /// <summary>
        /// RSS URL
        /// </summary>
        public const string FEA_ID_RSSURL_SETTING = "FEA0128"; //FEA0085

        /// <summary>
        /// MAX UPLOAD SIZE
        /// </summary>
        public const string FEA_ID_MAXUPSIZE_SETTING = "FEA0203"; //FEA0085

        /// <summary>
        /// Region View
        /// </summary>
        public const string FEA_ID_REGIONVIEW_SETTING = "FEA0204"; //FEA0085

        /// <summary>
        /// Email Log
        /// </summary>
        public const string FEA_ID_EMAIL_LOG = "FEA0206";

        /// <summary>
        /// ANNOUNCEMENTS
        /// </summary>
        public const string FEA_ID_ANNOUNCEMENTS = "FEA0119";

        /// <summary>
        /// Manage Announcements
        /// </summary>
        public const string FEA_ID_MANAGE_ANNOUNCEMENTS = "FEA0331";

        /// <summary>
        /// System Error Log Report
        /// </summary>
        public const string FEA_ID_REPORT_ERROR_LOG = "FEA0130";

        /// <summary>
        /// Edit Assignments
        /// </summary>
        //public const string FEA_ID_EDIT_ASSIGNMENTS = "FEA0138";

        /// <summary>
        /// Site Statistics
        /// </summary>
        public const string FEA_ID_SITE_STATISTICS = "FEA0319";

        /// <summary>
        /// Email Dashboard
        /// </summary>
        public const string FEA_ID_EMAIL_DASHBOARD = "FEA0205";

        /// <summary>
        /// Email Dashboard
        /// </summary>
        public const string FEA_ID_MANAGE_MESSAGES = "FEA0332";



        /// <summary>
        /// Admin Calendar
        /// </summary>
        public const string FEA_ID_ADMIN_CALENDAR = "FEA0330";

        public const string FEA_ID_SAML_CONFIGURATION = "FEA0329";

        // since last few are not consectutive - starting at 350 to be safe
        public const string FEA_ID_CLIENT_FEATURE_CONFIGURATION = "FEA0350";


        /// <summary>
        /// Assessment Aggregate Result By Question
        /// </summary>
        public const string FEA_ID_REPORT_AGGREGATE_RESULT_BY_QUESTION = "FEA0337";

        /// <summary>
        /// Assessment Individual Result By Question
        /// </summary>
        public const string FEA_ID_REPORT_INDIVIDUAL_RESULT_BY_QUESTION = "FEA0338";

        /// <summary>
        /// Assement Completion Report
        /// </summary>
        public const string FEA_ID_REPORT_ASSESSMENT_COMPLETION = "FEA0339";

        /// <summary>
        /// Assesment UserTranscript Report
        /// </summary>
        public const string FEA_ID_REPORT_ASSESSMENT_USER_TRANSCRIPT = "FEA0340";




        /// <summary>
        /// Blog Post
        /// </summary>
        public const string FEA_ID_BLOG_POST = "FEA0352";
        /// <summary>
        /// Blog Post Comments
        /// </summary>
        public const string FEA_ID_BLOG_POST_COMMENTS = "FEA0353";
        /// <summary>
        /// Blog Post settings
        /// </summary>
        public const string FEA_ID_BLOG_POST_SETTINGS = "FEA0354";

        /// Blog Post Category
        /// </summary>
        public const string FEA_ID_BLOG_POST_CATEGORY = "FEA0356";


        /// <summary>
        /// Forum posts
        /// </summary>
        public const string FEA_ID_FORUM_POST = "FEA0360";
        /// <summary>
        /// Forum Threads
        /// </summary>
        public const string FEA_ID_FORUM_THREADS = "FEA0359";
        /// <summary>
        /// Forum  SubCategory
        /// </summary>
        public const string FEA_ID_FORUM_SUBCATEGORY = "FEA0358";
        /// Forum Category
        /// </summary>
        public const string FEA_ID_FORUM_CATEGORY = "FEA0358";

        /// <summary>
        /// BLOGS
        /// </summary>
        /// <summary>
        /// 
        /// <summary>
        /// FORUM
        /// </summary>
        public const string FEA_ID_FORUM = "FEA0361";
        /// <summary>

        /// <summary>
        /// ESTORE
        /// </summary>
        public const string FEA_ID_ESTORE = "FEA0365";

        /// <summary>
        /// ORDER HISTORY REPORT
        /// </summary>
        public const string FEA_ID_ORDERHISTORY_REPORT = "FEA0366";

        /// <summary>
        /// COUPON USAGE REPORT
        /// </summary>
        public const string FEA_ID_COUPONUSAGE_REPORT = "FEA0430";


        /// <summary>
        /// PRODUCT DETAIL REPORT
        /// </summary>
        public const string FEA_ID_PRODUCTDETAIL_REPORT = "FEA0431";

        /// <summary>
        /// Question Category
        /// </summary>
        public const string FEA_ID_QUESTION_CATEGORY = "FEA0363";

        /// <summary>
        /// Question Bank
        /// </summary>
        public const string FEA_ID_QUESTION_BANK = "FEA0364";


        /// <summary>
        /// Reference Document Upload
        /// </summary>
        public const string FEA_ID_REF_DOCUMENT_UPLOAD = "FEA0372";


        /// <summary>
        /// Questionnaire Aggregate Result By Question
        /// </summary>
        public const string FEA_ID_REPORT_AGGREGATE_RESULT_BY_QUESTIONNAIRE = "FEA0373";

        /// <summary>
        /// QUESTIONNAIRE Individual Result By Question
        /// </summary>
        public const string FEA_ID_REPORT_INDIVIDUAL_RESULT_BY_QUESTIONNAIRE = "FEA0374";

        /// <summary>
        /// QUESTIONNAIRE Completion Report
        /// </summary>
        public const string FEA_ID_REPORT_QUESTIONNAIRE_COMPLETION = "FEA0375";

        /// <summary>
        /// QUESTIONNAIRE UserTranscript Report
        /// </summary>
        public const string FEA_ID_REPORT_QUESTIONNAIRE_USER_TRANSCRIPT = "FEA0376";

        /// <summary>
        /// Site Section
        /// </summary>
        public const string FEA_ID_WEB_SERVICE = "FEA0377";

        public const string FEA_ID_DEFAULT_ASSIGNMENT_SETTINGS = "FEA0379";

        public const string FEA_ID_USER_EXPIRY = "FEA0383";

        /// <summary>
        /// FAQ
        /// </summary>
        public const string FEA_ID_FAQS = "FEA0381";

		/// <summary>
        /// CLASSROOM TRAINING
        /// </summary>
        public const string FEA_ID_CLASSROOM_TRAINING = "FEA0341";

        /// <summary>
        /// VIRTUAL TRAINING
        /// </summary>
        //public const string FEA_ID_VIRTUAL_TRAINING = "FEA0393";
        public const string FEA_ID_VIRTUAL_TRAINING = "FEA0393";
        /*Added by Kunal*/

        /// <summary>
        /// VIRTUAL TRAINING
        /// </summary>
        //public const string FEA_ID_VIRTUAL_TRAINING_ADMINUSER_MAPPING = "FEA0404";
        public const string FEA_ID_VIRTUAL_TRAINING_ADMINUSER_MAPPING = "FEA0409";
        /*Added by Sujit*/

        /// <summary>
        /// Manage FAQ
        /// </summary>
        public const string FEA_ID_MANAGE_FAQS = "FEA0380";
		public const string FEA_ID_CATEGORY = "FEA0382";
        public const string FEA_ID_DOCUMENT_LIB = "FEA0387";

        public const string FEA_ID_MERCHANT_INFO = "FEA0378";

        public const string FEA_ID_MESSAGES_AND_ALERTS = "FEA0397";

        public const string FEA_ID_ASSIGNMENTS = "FEA0413";

        public const string FEA_ID_MOH_REPORT = "FEA0414";

        public const string FEA_ID_AUDIT_TRAIL = "FEA0415";

        public const string FEA_ID_MANAGE_CERTIFICATE = "FEA0422";


        public const string FEA_ID_MANAGE_CERTIFICATE_Mapping = "FEA0424";

        public const string FEA_ID_CHAT = "FEA0423";

        public const string FEA_NewsSubscription_Report = "FEA0427";


        #endregion
        /// <summary>
        /// Default Contructor
        /// </summary>
        public AdminFeatures()
        {
            _entListAdminRoleFeatures = new List<AdminRoleFeatures>();
        }

        /// <summary>
        ///  AdminFeatures ListMethod enum for GetAllFeatureList
        /// </summary>
        public new enum ListMethod
        {
            GetAllFeatureList
        }

        /// <summary>
        /// methos enum for Get,Add,Update,Delete
        /// </summary>
        public new enum Method
        {
            Get,
            UpdateIsVisible
        }

        private string _strFeatureName;
        /// <summary>
        /// Feature Name
        /// </summary>
        public string FeatureName
        {
            get { return _strFeatureName; }
            set { if (this._strFeatureName != value) { _strFeatureName = value; } }
        }

        private string _strFeatureDescription;
        /// <summary>
        /// Feature Description
        /// </summary>
        public string FeatureDescription
        {
            get { return _strFeatureDescription; }
            set { if (this._strFeatureDescription != value) { _strFeatureDescription = value; } }
        }

        private string _strParentFeatureId;
        /// <summary>
        /// Parent Feature Id
        /// </summary>
        public string ParentFeatureId
        {
            get { return _strParentFeatureId; }
            set { if (this._strParentFeatureId != value) { _strParentFeatureId = value; } }
        }

        private List<AdminRoleFeatures> _entListAdminRoleFeatures;
        /// <summary>
        /// List of AdminRoleFeatures
        /// </summary>
        public List<AdminRoleFeatures> AdminRoleFeatures
        {
            get { return _entListAdminRoleFeatures; }
            set { if (this._entListAdminRoleFeatures != value) { _entListAdminRoleFeatures = value; } }
        }

        /// <summary>
        /// To check Can View rights
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>retuns true if success</returns>
        public bool CanView(Learner pEntLearner)
        {
            if (!String.IsNullOrEmpty(this.ID))
            {
                foreach (UserAdminRole entRole in pEntLearner.UserAdminRole)
                {
                    if (entRole.RoleId == AdminRole.SUPER_ADMIN_ROLE_ID)
                        return true;

                    AdminRoleFeatures entAdminRoleFeature = this.AdminRoleFeatures.Find(delegate(AdminRoleFeatures entRoleFeature)
                    { return entRoleFeature.RoleId == entRole.RoleId; });
                    if (entAdminRoleFeature != null)
                    {
                        if (entAdminRoleFeature.CanView)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// To check Can add rights
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>retuns true if success</returns>
        public bool CanAdd(Learner pEntLearner)
        {
            if (!String.IsNullOrEmpty(this.ID))
            {
                foreach (UserAdminRole entRole in pEntLearner.UserAdminRole)
                {
                    if (entRole.RoleId == AdminRole.SUPER_ADMIN_ROLE_ID)
                        return true;
                    AdminRoleFeatures entAdminRoleFeature = this.AdminRoleFeatures.Find(delegate(AdminRoleFeatures entRoleFeature)
                    { return entRoleFeature.RoleId == entRole.RoleId; });

                    if (entAdminRoleFeature != null)
                    {
                        if (entAdminRoleFeature.CanAdd)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// To check Can Edit rights
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>retuns true if success</returns>
        public bool CanEdit(Learner pEntLearner)
        {
            if (!String.IsNullOrEmpty(this.ID))
            {
                foreach (UserAdminRole entRole in pEntLearner.UserAdminRole)
                {
                    if (entRole.RoleId == AdminRole.SUPER_ADMIN_ROLE_ID)
                        return true;
                    AdminRoleFeatures entAdminRoleFeature = this.AdminRoleFeatures.Find(delegate(AdminRoleFeatures entRoleFeature)
                    { return entRoleFeature.RoleId == entRole.RoleId; });
                    if (entAdminRoleFeature != null)
                    {
                        if (entAdminRoleFeature.CanEdit)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// To check Can Delete rights
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>retuns true if success</returns>
        public bool CanDelete(Learner pEntLearner)
        {
            if (!String.IsNullOrEmpty(this.ID))
            {
                foreach (UserAdminRole entRole in pEntLearner.UserAdminRole)
                {
                    if (entRole.RoleId == AdminRole.SUPER_ADMIN_ROLE_ID)
                        return true;
                    AdminRoleFeatures entAdminRoleFeature = this.AdminRoleFeatures.Find(delegate(AdminRoleFeatures entRoleFeature)
                    { return entRoleFeature.RoleId == entRole.RoleId; });
                    if (entAdminRoleFeature != null)
                    {
                        if (entAdminRoleFeature.CanDelete)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// To check Can print rights
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>retuns true if success</returns>
        public bool CanPrint(Learner pEntLearner)
        {
            if (!String.IsNullOrEmpty(this.ID))
            {
                foreach (UserAdminRole entRole in pEntLearner.UserAdminRole)
                {
                    if (entRole.RoleId == AdminRole.SUPER_ADMIN_ROLE_ID)
                        return true;
                    AdminRoleFeatures entAdminRoleFeature = this.AdminRoleFeatures.Find(delegate(AdminRoleFeatures entRoleFeature)
                    { return entRoleFeature.RoleId == entRole.RoleId; });
                    if (entAdminRoleFeature != null)
                    {
                        if (entAdminRoleFeature.CanPrint)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// To check Can Export rights
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>retuns true if success</returns>
        public bool CanExport(Learner pEntLearner)
        {
            if (!String.IsNullOrEmpty(this.ID))
            {
                foreach (UserAdminRole entRole in pEntLearner.UserAdminRole)
                {
                    if (entRole.RoleId == AdminRole.SUPER_ADMIN_ROLE_ID)
                        return true;
                    AdminRoleFeatures entAdminRoleFeature = this.AdminRoleFeatures.Find(delegate(AdminRoleFeatures entRoleFeature)
                    { return entRoleFeature.RoleId == entRole.RoleId; });
                    if (entAdminRoleFeature != null)
                    {
                        if (entAdminRoleFeature.CanExport)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// To check Can Upload rights
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>retuns true if success</returns>
        public bool CanUpload(Learner pEntLearner)
        {
            if (!String.IsNullOrEmpty(this.ID))
            {
                foreach (UserAdminRole entRole in pEntLearner.UserAdminRole)
                {
                    if (entRole.RoleId == AdminRole.SUPER_ADMIN_ROLE_ID)
                        return true;
                    AdminRoleFeatures entAdminRoleFeature = this.AdminRoleFeatures.Find(delegate(AdminRoleFeatures entRoleFeature)
                    { return entRoleFeature.RoleId == entRole.RoleId; });
                    if (entAdminRoleFeature != null)
                    {
                        if (entAdminRoleFeature.CanUpload)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// To check Can Import rights
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>retuns true if success</returns>
        public bool CanImport(Learner pEntLearner)
        {
            if (!String.IsNullOrEmpty(this.ID))
            {
                foreach (UserAdminRole entRole in pEntLearner.UserAdminRole)
                {
                    if (entRole.RoleId == AdminRole.SUPER_ADMIN_ROLE_ID)
                        return true;
                    AdminRoleFeatures entAdminRoleFeature = this.AdminRoleFeatures.Find(delegate(AdminRoleFeatures entRoleFeature)
                    { return entRoleFeature.RoleId == entRole.RoleId; });
                    if (entAdminRoleFeature != null)
                    {
                        if (entAdminRoleFeature.CanImport)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// To check Can Email rights
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>retuns true if success</returns>
        public bool CanEmail(Learner pEntLearner)
        {
            if (!String.IsNullOrEmpty(this.ID))
            {
                foreach (UserAdminRole entRole in pEntLearner.UserAdminRole)
                {
                    if (entRole.RoleId == AdminRole.SUPER_ADMIN_ROLE_ID)
                        return true;
                    AdminRoleFeatures entAdminRoleFeature = this.AdminRoleFeatures.Find(delegate(AdminRoleFeatures entRoleFeature)
                    { return entRoleFeature.RoleId == entRole.RoleId; });
                    if (entAdminRoleFeature != null)
                    {
                        if (entAdminRoleFeature.CanEmail)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// To check Can Copy rights
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>retuns true if success</returns>
        public bool CanCopy(Learner pEntLearner)
        {
            if (!String.IsNullOrEmpty(this.ID))
            {
                foreach (UserAdminRole entRole in pEntLearner.UserAdminRole)
                {
                    if (entRole.RoleId == AdminRole.SUPER_ADMIN_ROLE_ID)
                        return true;
                    AdminRoleFeatures entAdminRoleFeature = this.AdminRoleFeatures.Find(delegate(AdminRoleFeatures entRoleFeature)
                    { return entRoleFeature.RoleId == entRole.RoleId; });
                    if (entAdminRoleFeature != null)
                    {
                        if (entAdminRoleFeature.CanCopy)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// To check Can Activate rights
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>retuns true if success</returns>
        public bool CanActivate(Learner pEntLearner)
        {
            if (!String.IsNullOrEmpty(this.ID))
            {
                foreach (UserAdminRole entRole in pEntLearner.UserAdminRole)
                {
                    if (entRole.RoleId == AdminRole.SUPER_ADMIN_ROLE_ID)
                        return true;
                    AdminRoleFeatures entAdminRoleFeature = this.AdminRoleFeatures.Find(delegate(AdminRoleFeatures entRoleFeature)
                    { return entRoleFeature.RoleId == entRole.RoleId; });
                    if (entAdminRoleFeature != null)
                    {
                        if (entAdminRoleFeature.CanActivate)
                            return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// To check Can Deactivate rights
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>retuns true if success</returns>
        public bool CanDeactivate(Learner pEntLearner)
        {
            if (!String.IsNullOrEmpty(this.ID))
            {
                foreach (UserAdminRole entRole in pEntLearner.UserAdminRole)
                {
                    if (entRole.RoleId == AdminRole.SUPER_ADMIN_ROLE_ID)
                        return true;
                    AdminRoleFeatures entAdminRoleFeature = this.AdminRoleFeatures.Find(delegate(AdminRoleFeatures entRoleFeature)
                    { return entRoleFeature.RoleId == entRole.RoleId; });
                    if (entAdminRoleFeature != null)
                    {
                        if (entAdminRoleFeature.CanDeactivate)
                            return true;
                    }
                }
            }
            return false;
        }

        private bool _bIsCBPrintVisible;
        public bool IsCBPrintVisible { get { return _bIsCBPrintVisible; } set { _bIsCBPrintVisible = value; } }

        private bool _bIsCBViewVisible;
        public bool IsCBViewVisible { get { return _bIsCBViewVisible; } set { _bIsCBViewVisible = value; } }

        private bool _bIsCBAddVisible;
        public bool IsCBAddVisible { get { return _bIsCBAddVisible; } set { _bIsCBAddVisible = value; } }

        private bool _bIsCBEditVisible;
        public bool IsCBEditVisible { get { return _bIsCBEditVisible; } set { _bIsCBEditVisible = value; } }

        private bool _bIsCBDeleteVisible;
        public bool IsCBDeleteVisible { get { return _bIsCBDeleteVisible; } set { _bIsCBDeleteVisible = value; } }

        private bool _bIsCBUploadVisible;
        public bool IsCBUploadVisible { get { return _bIsCBUploadVisible; } set { _bIsCBUploadVisible = value; } }

        private bool _bIsCBImportVisible;
        public bool IsCBImportVisible { get { return _bIsCBImportVisible; } set { _bIsCBImportVisible = value; } }

        private bool _bIsCBExportVisible;
        public bool IsCBExportVisible { get { return _bIsCBExportVisible; } set { _bIsCBExportVisible = value; } }

        private bool _bIsCBEmailVisible;
        public bool IsCBEmailVisible { get { return _bIsCBEmailVisible; } set { _bIsCBEmailVisible = value; } }

        private bool _bIsCBCopyVisible;
        public bool IsCBCopyVisible { get { return _bIsCBCopyVisible; } set { _bIsCBCopyVisible = value; } }

        private bool _bIsCBActivateVisible;
        public bool IsCBActivateVisible { get { return _bIsCBActivateVisible; } set { _bIsCBActivateVisible = value; } }

        private bool _bIsCBDeActivateVisible;
        public bool IsCBDeActivateVisible { get { return _bIsCBDeActivateVisible; } set { _bIsCBDeActivateVisible = value; } }

        private Nullable<bool> _bIsVisible;
        public Nullable<bool> IsVisible
        {
            get { return _bIsVisible; }
            set { if (this._bIsVisible != value) { _bIsVisible = value; } }
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
                if (String.IsNullOrEmpty(FeatureName))
                    return false;

                if (String.IsNullOrEmpty(CreatedById))
                    return false;

            }
            if (String.IsNullOrEmpty(LastModifiedById))
                return false;
            return true;
        }
        private string _strSystemUserGUID;
        /// <summary>
        /// Client Id
        /// </summary>
        /// 
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private bool _bCanPrintVisible;
        public bool bCanPrintVisible { get { return _bCanPrintVisible; } set { _bCanPrintVisible = value; } }

        private bool _bCanViewVisible;
        public bool bCanViewVisible { get { return _bCanViewVisible; } set { _bCanViewVisible = value; } }

        private bool _bCanAddVisible;
        public bool bCanAddVisible { get { return _bCanAddVisible; } set { _bCanAddVisible = value; } }

        private bool _bCanEditVisible;
        public bool bCanEditVisible { get { return _bCanEditVisible; } set { _bCanEditVisible = value; } }

        private bool _bCanDeleteVisible;
        public bool bCanDeleteVisible { get { return _bCanDeleteVisible; } set { _bCanDeleteVisible = value; } }

        private bool _bCanUploadVisible;
        public bool bCanUploadVisible { get { return _bCanUploadVisible; } set { _bCanUploadVisible = value; } }

        private bool _bCanImportVisible;
        public bool bCanImportVisible { get { return _bCanImportVisible; } set { _bCanImportVisible = value; } }

        private bool _bCanExportVisible;
        public bool bCanExportVisible { get { return _bCanExportVisible; } set { _bCanExportVisible = value; } }

        private bool _bCanEmailVisible;
        public bool bCanEmailVisible { get { return _bCanEmailVisible; } set { _bCanEmailVisible = value; } }

        private bool _bCanCopyVisible;
        public bool bCanCopyVisible { get { return _bCanCopyVisible; } set { _bCanCopyVisible = value; } }

        private bool _bCanActivateVisible;
        public bool bCanActivateVisible { get { return _bCanActivateVisible; } set { _bCanActivateVisible = value; } }

        private bool _bCanDeActivateVisible;
        public bool bCanDeActivateVisible { get { return _bCanDeActivateVisible; } set { _bCanDeActivateVisible = value; } }

    }
        
}