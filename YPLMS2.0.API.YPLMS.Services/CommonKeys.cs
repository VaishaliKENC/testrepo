using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.YPLMS.Services
{
    public class CommonKeys
    {
        public const string SESSION_VIRTUAL = "virtual";
        public const string SINGLE_SIGN_ON_ROLE_ID = "ROL0005";
        public const string SITE_ADMIN_ROLE_ID = "ROL0005";
        public const string LEARNER_ROLE_ID = "ROL0001";
        public const string SUPER_ADMIN_ROLE_ID = "ROL0006";
        public const string FIRST_ADMIN_USER_TYPE = "FirstAdmin";
        public const string ORG_ROOT_LEVEL_ID = "L00000";
        public const string ORG_ROOT_LEVEL_UNIT_ID = "U00000";
        public const string ADMIN_SITE_DEFAULT_LANGUAGE = "en-US";
        public const string SESSION_SELECTED_CLIENT_ID = "SelectedClientId";
        public const string SESSION_EDIT_CLIENT_ID = "EditClientId";
        public const string SESSION_SITE_PAGE_UPLOADED_IMAGE_NAME = "SitePageUploadedImageName";
        public const string SESSION_MENU_ITEMS = "MenuItems";
        public static int GRID_DEFAULT_PAGE_SIZE = 5;
        public const string RANGE_TOTAL_ROWS = "TotalRows";
        public const string RANGE_TOTAL_COUNT = "TotalCount";
        public const int TEXT_MAX_LENGTH = 250;
        public const int TEXT_DESCRIPTION_MAX_LENGTH = 250;
        public const string SESSION_TIMEOUT = "SessionTimeout";
        public const string CSS_BUTTON_ENABLED = "btn btn-danger";
        public const string CSS_BUTTON_DISABLED = "btn btn-danger disabled";
        public const string DETAILED_CERTIFICATION_RESULTS_BY_USER_FEATURE_ID = "FEA0310";
        public const string INCIDENCES_OF_NON_PREFERRED_ANSWERS_FOR_THE_CERTIFICATION__FEATURE_ID = "FEA0311";
        public const string SESSION_SCHEDULE_SEARCH = "SessionScheduleSearch";
        public const string LU_WEBSERVICE_TOKEN = "LUWebServiceToken";
        public const string GRID_DATE_FORMAT = "{0:dd-MMM-yyyy}";
        public const string CURRENT_PAGE = "CurrentPage";
        ////public const string MORTGAGEU_CLIENTID = "CLIjMkXutOfL5J6";
        public const string DefaultLangId = "en-US";

        public const string Enable_Social_Media_Comment_on_Menu = "FEA007";
        public const string Social_Media_On_Activity_Completion = "FEA008";

    }

    public class OrgKeys
    {
        public const string SELECT = "select";
        public const string LEVEL_DELETION_KEY = "DELETELEVEL";
        public const string ADD_LEVEL_TAB = "Add Level";
        public const string ADD_UNIT_TAB = "Add Unit";
        public const string TOP_TAB = "Manage Organization Tree";
        public const string RIGHTS = "checkrights";
        public const string COL_HEADING_PARENTS = "Parents";//For parentunit id's


        public const string LEVEL_CHECKBOX_ID = "chkClient";
        public const string LEVELGRID_TITLE = "Organization Level";
        public const string UNITGRID_TITLE = "Organization Unit";
        public const string UNIT_CHECKBOX_ID = "cbxUnits";
        public const string ADD_UNITFORM_TITLE = "Add Organization Unit";
        public const string UPDATE_UNITFORM_TITLE = "Update Organization Unit";
        public const string MSG_NULL_CLIENT = "RedirectToLogin();";
        public const string FLAG_TRUE = "TRUE";

        public const string COLUMN = "COL";
        public const string LEVEL_COLUMN = "Level_Column";
        public const string SORT_DESC = " DESC";
        public const string SORT_ASC = " ASC";

        public const string COLUMN_LAST_MODIFIED_DATE = "LastModifiedDate";
        public const string COLUMN_DATE_Created = "DateCreated";
        public const string COLUMN_LAST_MODIFIED_BY_ID = "LastModifiedById";
        public const string PARENT_UNIT_ID = "ParentUnitId";
        public const string SESSION_METHOD = "Method";
        public const string COLUMN_LEVEL_ORDER = "LevelOrder";
        public const string COLUMN_LEVEL_NAME = "LevelName";
        public const string COLUMN_IS_USED = "IsUsed";
        public const string COLUMN_PARENT_UNITS = "ParentUnits";//For parentunit id's
        public const string COLUMN_HEADING_IS_USED = "Is Allocated";
        public const string COLUMN_HEADING_CHILD_UNITS = "ChildUnits";
        public const string COLUMN_HEADING_ISUSED = "isused";


        public const string ADD_LEVELFORM_TITLE = "Add organization level";
        public const string CANNOT_EDIT_ROOT = "Cannot Edit Root.";
        public const string EDIT_LEVELFORM_TITLE = "Update Organization Level";
        public const string COLUMN_LEVEL_ID = "LevelId";
        public const string ID = "ID";
        public const string LEVEL_TABLE = "OrganizationLevel";
        public const string UNIT_TABLE = "OrganizationUnits";
        public const string COLUMN_IF_USERS_ALLOCATED = "IsUsed";
        public const string COLUMN_CHILD_COUNT = "ChildCount";


        #region Level & Unit grid heading
        public const string COL_HEADING_LEVEL_NAME = "Level Name";
        public const string COL_HEADING_LEVEL_ORDER = "Level Order";
        public const string COL_HEADING_CREATED_DATE = "Created Date";

        public const string COL_HEADING_UNIT_NAME = "Unit Name";
        public const string COL_HEADING_PARENT_UNIT = "Parent Unit";
        public const string COL_HEADING_ISACTIVE = "Is Active";
        public const string COL_HEADING_NO_OF_CHILD_UNITS = "Child Unit Count";
        #endregion


        public const string METHOD_UPDATE = "update";
        public const string METHOD_ADD = "add";
        public const string UNIT_NAME = "UnitName";
        public const string STRING_ZERO = "0";
        public const string BUTTON_EDIT = "lnkbtnEdit";
        public const string BUTTON_DELETE = "lnkbtnDelete";
        public const string BUTTON_LEVEL_EDIT = "Edit Level";
        public const string BUTTON_LEVEL_DELETE = "Delete Levels";
        public const string BUTTON_UNIT_EDIT = "Edit Unit";
        public const string BUTTON_UNIT_DELETE = "Delete Unit";
        public const string COMMAND_NAME_EDIT = "EditRow";
        public const string COMMAND_NAME_DELETE = "DeleteRow";
        public const string COLUMN_PARENT_UNIT = "ParentUnit";
        public const string COLUMN_SEQUENCE = "Sequence";

        public const string COLUMN_IS_ACTIVE = "IsActive";
        public const string COLUMN_SEQUENCEORDER = "SequenceOrder";
        public const string ROOT_VALUE = "Root";

        public const int BASE_NODE_LEVEL = 1;
        public const string ROOT_NODE_VALUE = "RootNode";
        public const string CLIENT_NAME = "RootNode";

        public const string LOGIN_PAGE = "login.aspx";
        public const string SESSION_CLIENT_ID = "sClientId";

        public const string SESSION_UIDS_FOR_DEL = "UnitIdsForDelete";//UnitIdsForDelete
        public const string SESSION_UID_FOR_UPDATE = "UnitIdFromUpdateMethod";//_strUnitIdFromUpdateMethod
        public const string SESSION_PARENT_UNIT_ID = "ParentUnitId";//ParentUnitId
        public const string SESSION_ROOT_LEVEL_ID = "RootNodeLevelId";//_strRootNodeLevelId
        public const string SESSION_SEQ_ORDER = "SequenceOrder";//_iSequenceOrder
        public const string SESSION_LEVELID_FOR_EDIT = "LevelIdForEdit";//_strLevelIdForEdit
        public const string SESSION_LEVEL_ORDER = "LevelOrder";//_strLevelOrder
        public const string SESSION_CLIENT_NAME = "ClientName";//_strLevelOrder
    }

    public class UserKeys
    {
        public const string SESSION_SELECTED_USER_ID = "SelectedUserId";

    }

    public class AdminRoleKeys
    {
        public const string SESSION_SELECTED_ADMIN_ROLE_ID = "SelectedAdminRoleId";
        public const string SESSION_METHOD_ADMIN_ROLE = "AdminRoleMethod";
        public const string SESSION_VALUE_ADMIN_ROLE_ADD = "AdminRoleMethodAdd";
        public const string SESSION_VALUE_ADMIN_ROLE_UPDATE = "AdminRoleMethodUpdate";
        public const string SESSION_IS_COPY_ADMIN_ROLE = "AdminRoleMethodCopy";
        public const string SESSION_ROLE_NAME = "RoleName";
    }

    public class ContentKeys
    {
        public const string SESSION_COURSEID = "CourseId";
        public const string SESSION_COURSETYPE = "CourseType";
        public const string SESSION_ALLOCATE_TO_CLIENTID = "AllocateToClientId";
        public const string SESSION_SELECTED_ENROLLMENTID = "SelectedEnrollmentId";
        public const string SESSION_SUBSCRIPTION_ID_TO_EDIT = "SubscriptionIDToEdit";
        public const string SESSION_MasterScoreFromLMS = "MasterScoreFromLMS";
        public const string SESSION_COURSESETTINGS = "CourseSettings";
        public const string SESSION_IS_FOR_ADMIN_PREVIEW = "IsForAdminPreview";
        public const string SESSION_LEARNER_ID = "CourseLearnerId";

        #region Questionnaire
        public const string QUESTIONNAIRE_EXPORT_FILE_NAME = "ExportTranslation.xls";

        #endregion

        #region Content Mgmt.

        public const string SESSION_ACTIVITYID = "ActivityId";
        public const string SESSION_COURSENAME = "CourseName";
        public const string SESSION_ACTIVITY_ISAUTH = "IsAuthenticate";


        #region Asset Policies
        public const string Asset_STR = "Status";
        #endregion

        #endregion
    }

    public class EmailKeys
    {
        public const string SESSION_EMAIL_DELIVERY_EDIT_ID = "EmailDeliveryEditID";
        public const string SESSION_EMAIL_DELIVERY_PREVIEW_ID = "EmailDeliveryPreviewID";
        public const string SESSION_EMAIL_DELIVERY_PREVIEW_VALUES = "EmailDeliveryPreviewValues";
    }

    public class QuestionnaireKeys
    {
        #region Questionnaire Bulk Import

        public const string QuestionnaireDetails = "QuestionnaireDetails";
        public const string SectionsTitle = "SectionsTitle";
        public const string QuestionTitle = "QuestionTitle";
        public const string QuestionType = "QuestionType";
        public const string OptionTitle = "OptionTitle";
        public const string OptionType = "OptionType";
        public const string Preferred = "preferred";
        public const string Non_Preferred = "non-preferred";
        public const string Neutral = "neutral";

        public const string MCQ = "mcq";
        public const string MRQ = "mrq";
        public const string FreeText = "freetext";

        #endregion

    }

    public class AssessmentKeys
    {
        #region Assessment Bulk Import

        public const string AssessmentDetails = "AssessmentDetails";
        public const string SectionsTitle = "SectionsTitle";
        public const string QuestionTitle = "QuestionTitle";
        public const string QuestionType = "QuestionType";
        public const string OptionTitle = "OptionTitle";
        public const string OptionType = "OptionType";
        public const string Preferred = "preferred";
        public const string Non_Preferred = "non-preferred";
        public const string Neutral = "neutral";

        public const string MCQ = "mcq";
        public const string MRQ = "mrq";
        public const string FreeText = "freetext";
        public const string Correct = "correct";
        public const string Incorrect = "incorrect";

        #endregion

    }

    #region BulImportColumn Constant

    public class BulkImportAssignmentCommonKeys
    {
        public const string ASSIGNMENT_DATE = "AssignmentDateSet";
        public const string EXPIRY_DATE = "ExpiryDateSet";
        public const string DUE_DATE = "DueDateSet";
        public const string LEARNER_ID = "LoginId";
        public const string ACTIVITY_ID = "ActivityId";
        public const string TRACK_SCORE = "TrackScore";
        public const string TRACK_RESPONSE = "TrackResponse";

        public const string REASSIGNEMENT_DATE = "ReassignmentDateSet";
        public const string REASSIGNEMENT_DUE_DATE = "ReassignmentDueDateSet";
        public const string REASSIGNEMENT_EXPIRY_DATE = "ReassignmentExpiryDateSet";

        public const string ASSIGNEMENT_DATE_REL_To = "AssignmentRelTO";
        public const string ASSIGNEMENT_DUE_REL_TO = "AssignmentDueRelTO";
        public const string ASSIGNEMENT_EXPIRY_REL_TO = "AssignmentExpiryRelTO";

        public const string REASSIGNEMENT_DT_REL_TO = "ReassignmentRelTo";
        public const string REASSIGNEMENT_DUE_DT_REL_TO = "ReassignmentDueRelTo";
        public const string REASSIGNEMENT_EXPIRY_DT_REL_TO = "ReassignmentExpiryRelTo";

        public const string BUSINESS_RULE = "BusinessRule";
        public const string ISOVERRIDE = "IsOverride";
        public const string CATEGORY_ID = "CategoryId";
    }

    #endregion


    #region ILT Bulk Import Session

    public class BulkImportSessionsCommonKeys
    {
        public const string SESSION_ID = "SessionId";
        public const string SESSION_NO = "SessionNo";
        public const string SESSION_TITLE = "SessionTitle";
        public const string START_DATE = "SessionStartDate";
        public const string END_DATE = "SessionEndDate";
        public const string TOTAL_SESSION_DURATION = "SessionDuration";
        public const string SESSION_LOCATION = "SessionLocation";
        public const string SESSION_INSTRUCTOR = "SessionInstructor";
        public const string SESSION_DESCRIPTION = "SessionDescription";
        public const string SESSION_TIMEZONE = "SessionTimeZone";
        public const string SESSION_PREWORK = "SessionPreWork";
        public const string SESSION_POSTWORK = "SessionPostWork";

    }


    #endregion

    #region ILT Bulk Import Module

    public class BulkImportModuleCommonKeys
    {
        public const string MODULE_ID = "ModuleId";
        public const string MODULE_NAME = "ModuleName";
        public const string SESSION_ID = "SessionId";
        public const string SESSION_NAME = "SessionName";
        public const string DAY = "Day";
    }


    #endregion


    #region ILT Bulk Import User Registration and Attendance

    public class BulkImportUserRegistrationAttendance
    {
        public const string EVENT_ID = "EventId";
        public const string EVENT_TITLE = "Event_title";
        public const string SESSION_NO = "Session_No";
        public const string SESSION_TITLE = "Session_title";
        public const string MODULE_ID = "ModuleId";
        public const string MODULE_NAME = "ModuleName";
        public const string LOGIN_ID = "LoginId";
        public const string ATTENDANCE_DATE_TIME = "AttendanceDateTime";
        public const string ATTENDED = "isAttended";
    }


    #endregion

    public class BusinessRuleReportsKeys
    {
        public const string STANDARD_CUSTOM = "StandardCustom";
        public const string CUSTOM_FIELD = "CustomField";
        public const string LNG = "LNG";
        public const string USERS = "Users";
        public const string USERS_EXCLUDE = "UsersExclude";
        public const string ORG_TREE = "OrgTree";
        public const string EQUAL = "equal";
        public const string EQUAL_TEXT = "=";
        public const string NOT_EQUAL = "notequal";
        public const string NOT_EQUAL_TEXT = "<>";
        public const string LIKE = "like";
        public const string LIKE_TEXT = "like";
        public const string NOT_LIKE = "notlike";
        public const string NOT_LIKE_TEXT = "not like";

        //public const string NOT_LIKE = "notlike";
        public const string BETWEEN = "between";
        public const string NOT_BETWEEN = "notbetween";
        public const string ASSIGNMENT = "Assignment";
        public const string ACTIVITY = "Activity";
        public const string COMPLETION = "Completion";
        public const string BUSINESS_RULE = "BusinessRule";
        public const string NULL_TEXT = "";
        public const string NULL_VALUE = "N00001";
        public const string ALL_TEXT = "All";
        public const string ALL_VALUE = "All";
        public const string NO_VALUE = "NULL(Value Not Present)";
        //-aw uat
        public const string RELATIVE_DATE = "relativedate";
    }

    #region BulImportQuestionColumn Constant

    public class BulkImportQuestionCommonKeys
    {
        public const string CATEGORYID = "Question Category";
        public const string SUBCATEGORYID = "Question Sub Category";
        public const string QUESTION_TYPE = "Question Type";
        public const string KEYWORDS = "Keywords";
        public const string QUESTION_TITLE = "Question Title";

        public const string OPTION1_TITLE = "Option1 Title";
        public const string OPTION1_TYPE = "Option1 Type";
        public const string OPTION2_TITLE = "Option2 Title";
        public const string OPTION2_TYPE = "Option2 Type";
        public const string OPTION3_TITLE = "Option3 Title";
        public const string OPTION3_TYPE = "Option3 Type";
        public const string OPTION4_TITLE = "Option4 Title";
        public const string OPTION4_TYPE = "Option4 Type";
        public const string OPTION5_TITLE = "Option5 Title";
        public const string OPTION5_TYPE = "Option5 Type";
        public const string OPTION6_TITLE = "Option6 Title";
        public const string OPTION6_TYPE = "Option6 Type";
        public const string OPTION7_TITLE = "Option7 Title";
        public const string OPTION7_TYPE = "Option7 Type";
        public const string OPTION8_TITLE = "Option8 Title";
        public const string OPTION8_TYPE = "Option8 Type";
        public const string OPTION9_TITLE = "Option9 Title";
        public const string OPTION9_TYPE = "Option9 Type";
        public const string OPTION10_TITLE = "Option10 Title";
        public const string OPTION10_TYPE = "Option10 Type";

        public const string ISOVERRIDE = "IsOverride";
    }

    #endregion


    public class VirtualTrainingKeys
    {
        public const string VTJOINSTATUS_ACCEPT = "ACCEPT";
        public const string VTJOINSTATUS_REJECT = "REJECT";
        public const string VTJOINSTATUS_REGISTER = "REGISTER";
        public const string VTJOINSTATUS_INVITE = "INVITE";
        public const string VTJOINSTATUS_WAITLIST = "WAITLIST";
    }

    public class CertificateKeys
    {
        public const string CETIFICATE_ID = "CertificateId";
    }
}
