using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.Entity
{
    public class ActivityAssignmentVM: BaseEntityVM
    {
        //public ActivityAssignmentMode? _strAssignmentModeForOverride;
       // public ActivityAssignmentType? _strAssignmentTypeId;
        public string? _strRuleId;
        public string? _strLicenseId;
        public Nullable<bool> _bIsCurrentlyAssigned;
        public bool? _bIsEditfromUI;

        public string? _strUserID;
        /// <summary>
        /// UserID
        /// </summary>
        public string? UserID
        {
            get { return _strUserID; }
            set { if (this._strUserID != value) { _strUserID = value; } }
        }

        public string? _strUserName;
        /// <summary>
        /// UserName
        /// </summary>
        public string? UserName
        {
            get { return _strUserName; }
            set { if (this._strUserName != value) { _strUserName = value; } }
        }

        public ActivityCompletionCondition? _strCompletionCondition;
        /// <summary>
        /// Completion Condition
        /// </summary>
        public ActivityCompletionCondition? CompletionCondition
        {
            get { return _strCompletionCondition; }
            set { if (this._strCompletionCondition != value) { _strCompletionCondition = value; } }
        }

        //public string? _strCompletionCondition;

        public string? _strActivityName;
        /// <summary>
        /// Activity Name
        /// </summary>
        public string? ActivityName
        {
            get { return _strActivityName; }
            set { if (this._strActivityName != value) { _strActivityName = value; } }
        }

        //
        public string? _strActivityID;
        public string? ActivityID
        {
            get { return _strActivityID; }
            set { if (this._strActivityID != value) { _strActivityID = value; } }
        }
        //
        public string? _strActivityDesc;
        /// <summary>
        /// Activity Description
        /// </summary>
        public string? ActivityDescription
        {
            get { return _strActivityDesc; }
            set { if (this._strActivityDesc != value) { _strActivityDesc = value; } }
        }

        public string? _strActivitySubTypeID;
        /// <summary>
        /// Activity Name
        /// </summary>
        public string? ActivitySubTypeID
        {
            get { return _strActivitySubTypeID; }
            set { if (this._strActivitySubTypeID != value) { _strActivitySubTypeID = value; } }
        }

        public string? _strActivityURL;
        /// <summary>
        /// Activity URL
        /// </summary>
        public string? ActivityURL
        {
            get { return _strActivityURL; }
            set { if (this._strActivityURL != value) { _strActivityURL = value; } }
        }

        public ActivityCompletionStatus? _strActivityStatus;
        /// <summary>
        /// Activity Status
        /// </summary>
        public ActivityCompletionStatus? ActivityStatus
        {
            get { return _strActivityStatus; }
            set { if (this._strActivityStatus != value) { _strActivityStatus = value; } }
        }

       // public string? _strActivityStatus;
        /// <summary>
        /// Activity Status
        /// </summary>
       

        public string? _strAssignmentStatus;
        public string? AssignmentStatus
        {
            get { return _strAssignmentStatus; }
            set { if (this._strAssignmentStatus != value) { _strAssignmentStatus = value; } }
        }


        public DateTime? _dateOfStart;
        /// <summary>
        /// Date Of Start
        /// </summary>
        public DateTime? DateOfStart
        {
            get { return _dateOfStart; }
            set { if (this._dateOfStart != value) { _dateOfStart = value; } }
        }

        public DateTime? _dateOfCompletion;
        /// <summary>
        /// Date Of Completion
        /// </summary>
        public DateTime? DateOfCompletion
        {
            get { return _dateOfCompletion; }
            set { if (this._dateOfCompletion != value) { _dateOfCompletion = value; } }
        }

        public DateTime? _assignmentDateOfCompletion;
        /// <summary>
        /// Date Of Completion
        /// </summary>
        public DateTime? AssignmentDateOfCompletion
        {
            get { return _assignmentDateOfCompletion; }
            set { if (this._assignmentDateOfCompletion != value) { _assignmentDateOfCompletion = value; } }
        }

        public ActivityContentType? _strActivityType;
        /// <summary>
        /// Activity Type
        /// </summary>
        public ActivityContentType? ActivityType
        {
            get { return _strActivityType; }
            set { if (this._strActivityType != value) { _strActivityType = value; } }
        }

       // public string? _strActivityType;
        /// <summary>
        /// Activity Type
        /// </summary>
        //public string? ActivityType
        //{
        //    get { return _strActivityType; }
        //    set { if (this._strActivityType != value) { _strActivityType = value; } }
        //}

        public bool? _bIsPrintCertificate;
        /// <summary>
        /// To know the Print Certificate is allowed or not
        /// </summary>
        public bool? IsPrintCertificate
        {
            get { return _bIsPrintCertificate; }
            set { if (this._bIsPrintCertificate != value) { _bIsPrintCertificate = value; } }
        }
        public string? _strAssignmentMode;
        public string? AssignmentMode
        {
            get { return _strAssignmentMode; }
            set { if (this._strAssignmentMode != value) { _strAssignmentMode = value; } }
        }

        public int? _iAttemptCount;
        /// <summary>
        /// Totot Attempts 
        /// </summary>
        public int? AttemptCount
        {
            get { return _iAttemptCount; }
            set { if (this._iAttemptCount != value) { _iAttemptCount = value; } }
        }

        public bool? _IsOverridePreviousAssignments;
        public bool? OverridePreviousAssignments
        {
            get { return _IsOverridePreviousAssignments; }
            set { if (this._IsOverridePreviousAssignments != value) { _IsOverridePreviousAssignments = value; } }
        }

        public string? _OperationModeForBulkAssignment;
        public string? OperationModeForBulkAssignment
        {
            get { return _OperationModeForBulkAssignment; }
            set { if (this._OperationModeForBulkAssignment != value) { _OperationModeForBulkAssignment = value; } }
        }


        public string? _strPreviousAssignmentId;
        public string? PreviousAssignmentId
        {
            get { return _strPreviousAssignmentId; }
            set { if (this._strPreviousAssignmentId != value) { _strPreviousAssignmentId = value; } }
        }

        /*** Added by Bajirao for Course Count and Curriculum Percentage ***/
        public int? _iTotalRowsCompleted;
        /// <summary>
        /// Totot Attempts 
        /// </summary>
        public int? TotalRowsCompleted
        {
            get { return _iTotalRowsCompleted; }
            set { if (this._iTotalRowsCompleted != value) { _iTotalRowsCompleted = value; } }
        }

        public int? _iTotalRowsAssigned;
        /// <summary>
        /// Totot Attempts 
        /// </summary>
        public int? TotalRowsAssigned
        {
            get { return _iTotalRowsAssigned; }
            set { if (this._iTotalRowsAssigned != value) { _iTotalRowsAssigned = value; } }
        }

        public decimal? _iCurriculumCompletionPercentage;
        /// <summary>
        /// Totot Attempts 
        /// </summary>
        public decimal? CurriculumCompletionPercentage
        {
            get { return _iCurriculumCompletionPercentage; }
            set { if (this._iCurriculumCompletionPercentage != value) { _iCurriculumCompletionPercentage = value; } }
        }
        /*** End of - Added by Bajirao for Course Count and Curriculum Percentage ***/


        //***** Actual AssignmentTable Fields

        public string? _strActivityMode;
        public string? ActivityMode
        {
            get { return _strActivityMode; }
            set { if (this._strActivityMode != value) { _strActivityMode = value; } }
        }

        public string? _strActivityTypeId;
        /// <summary>
        /// Activity Type Id
        /// </summary>
        public string? ActivityTypeId
        {
            get { return _strActivityTypeId; }
            set { if (this._strActivityTypeId != value) { _strActivityTypeId = value; } }
        }
        public ActivityCompletionCondition? _strCompletionConditionId;
        /// <summary>
        /// Completion Condition Id
        /// </summary>
        public ActivityCompletionCondition? CompletionConditionId
        {
            get { return _strCompletionConditionId; }
            set { if (this._strCompletionConditionId != value) { _strCompletionConditionId = value; } }
        }

        //public string? _strCompletionConditionId;
        ///// <summary>
        ///// Completion Condition Id
        ///// </summary>
        //public string? CompletionConditionId
        //{
        //    get { return _strCompletionConditionId; }
        //    set { if (this._strCompletionConditionId != value) { _strCompletionConditionId = value; } }
        //}
        public ActivityAssignmentMode? _strAssignmentModeForOverride;
        public ActivityAssignmentMode? AssignmentModeForOverride
        {
            get { return _strAssignmentModeForOverride; }
            set { if (this._strAssignmentModeForOverride != value) { _strAssignmentModeForOverride = value; } }
        }

       // public string? AssignmentModeForOverride;

        //public ActivityAssignmentType? AssignmentTypeId
        //{
        //    get { return _strAssignmentTypeId; }
        //    set { if (this._strAssignmentTypeId != value) { _strAssignmentTypeId = value; } }
        //}

        public string? AssignmentTypeId;
       
        public string? RuleId
        {
            get { return _strRuleId; }
            set { if (this._strRuleId != value) { _strRuleId = value; } }
        }
        public string? LicenseId
        {
            get { return _strLicenseId; }
            set { if (this._strLicenseId != value) { _strLicenseId = value; } }
        }
        public Nullable<bool> IsCurrentlyAssigned
        {
            get { return _bIsCurrentlyAssigned; }
            set { if (this._bIsCurrentlyAssigned != value) { _bIsCurrentlyAssigned = value; } }
        }
        public bool? IsEditfromUI
        {
            get { return _bIsEditfromUI; }
            set { if (this._bIsEditfromUI != value) { _bIsEditfromUI = value; } }
        }

        public bool? _bIsAssignmentBasedOnHireDate;
        public bool? IsAssignmentBasedOnHireDate
        {
            get { return _bIsAssignmentBasedOnHireDate; }
            set { if (this._bIsAssignmentBasedOnHireDate != value) { _bIsAssignmentBasedOnHireDate = value; } }
        }
        public bool? _bIsAssignmentBasedOnCreationDate;
        public bool? IsAssignmentBasedOnCreationDate
        {
            get { return _bIsAssignmentBasedOnCreationDate; }
            set { if (this._bIsAssignmentBasedOnCreationDate != value) { _bIsAssignmentBasedOnCreationDate = value; } }
        }
        public int? _iAssignAfterDaysOf;
        public int? AssignAfterDaysOf
        {
            get { return _iAssignAfterDaysOf; }
            set { if (this._iAssignAfterDaysOf != value) { _iAssignAfterDaysOf = value; } }
        }
        public DateTime? _assignmentDateSet;
        public DateTime? AssignmentDateSet
        {
            get { return _assignmentDateSet; }
            set { if (this._assignmentDateSet != value) { _assignmentDateSet = value; } }
        }

        public bool? _bIsNoDueDate;
        public bool? IsNoDueDate
        {
            get { return _bIsNoDueDate; }
            set { if (this._bIsNoDueDate != value) { _bIsNoDueDate = value; } }
        }
        public bool? _bIsDueBasedOnAssignDate;
        public bool? IsDueBasedOnAssignDate
        {
            get { return _bIsDueBasedOnAssignDate; }
            set { if (this._bIsDueBasedOnAssignDate != value) { _bIsDueBasedOnAssignDate = value; } }
        }
        public bool? _bIsDueBasedOnHireDate;
        public bool? IsDueBasedOnHireDate
        {
            get { return _bIsDueBasedOnHireDate; }
            set { if (this._bIsDueBasedOnHireDate != value) { _bIsDueBasedOnHireDate = value; } }
        }
        public bool? _bIsDueBasedOnCreationDate;
        public bool? IsDueBasedOnCreationDate
        {
            get { return _bIsDueBasedOnCreationDate; }
            set { if (this._bIsDueBasedOnCreationDate != value) { _bIsDueBasedOnCreationDate = value; } }
        }

        public bool? _bIsDueBasedOnStartDate;
        public bool? IsDueBasedOnStartDate
        {
            get { return _bIsDueBasedOnStartDate; }
            set { if (this._bIsDueBasedOnStartDate != value) { _bIsDueBasedOnStartDate = value; } }
        }
        public int? _iDueAfterDaysOf;
        public int? DueAfterDaysOf
        {
            get { return _iDueAfterDaysOf; }
            set { if (this._iDueAfterDaysOf != value) { _iDueAfterDaysOf = value; } }
        }

        public DateTime? _dateDueSet;
        public DateTime? DueDateSet
        {
            get { return _dateDueSet; }
            set { if (this._dateDueSet != value) { _dateDueSet = value; } }
        }

        public bool? _bIsNoExpiryDate;
        public bool? IsNoExpiryDate
        {
            get { return _bIsNoExpiryDate; }
            set { if (this._bIsNoExpiryDate != value) { _bIsNoExpiryDate = value; } }
        }

        public bool? _bIsExpiryBasedOnAssignDate;
        public bool? IsExpiryBasedOnAssignDate
        {
            get { return _bIsExpiryBasedOnAssignDate; }
            set { if (this._bIsExpiryBasedOnAssignDate != value) { _bIsExpiryBasedOnAssignDate = value; } }
        }

        public bool? _bIsExpiryBasedOnStartDate;
        public bool? IsExpiryBasedOnStartDate
        {
            get { return _bIsExpiryBasedOnStartDate; }
            set { if (this._bIsExpiryBasedOnStartDate != value) { _bIsExpiryBasedOnStartDate = value; } }
        }

        public bool? _bIsExpiryBasedOnDueDate;
        public bool? IsExpiryBasedOnDueDate
        {
            get { return _bIsExpiryBasedOnDueDate; }
            set { if (this._bIsExpiryBasedOnDueDate != value) { _bIsExpiryBasedOnDueDate = value; } }
        }
        public int? _iExpireAfterDaysOf;
        public int? ExpireAfterDaysOf
        {
            get { return _iExpireAfterDaysOf; }
            set { if (this._iExpireAfterDaysOf != value) { _iExpireAfterDaysOf = value; } }
        }
        public DateTime? _dateExpirySet;
        public DateTime? ExpiryDateSet
        {
            get { return _dateExpirySet; }
            set { if (this._dateExpirySet != value) { _dateExpirySet = value; } }
        }

        public bool? _bSendEmail;
        public bool? SendEmail
        {
            get { return _bSendEmail; }
            set { if (this._bSendEmail != value) { _bSendEmail = value; } }
        }

        public string? _strSendEmailType;
        public string? SendEmailType
        {
            get { return _strSendEmailType; }
            set { if (this._strSendEmailType != value) { _strSendEmailType = value; } }
        }

        public string? _strEmailTemplateId;
        public string? EmailTemplateId
        {
            get { return _strEmailTemplateId; }
            set { if (this._strEmailTemplateId != value) { _strEmailTemplateId = value; } }
        }

        //-- New Properties for Re-Assignment

        public bool? _bIsReAssignmentBasedOnAssignmentDate;
        public bool? IsReAssignmentBasedOnAssignmentDate
        {
            get { return _bIsReAssignmentBasedOnAssignmentDate; }
            set { if (this._bIsReAssignmentBasedOnAssignmentDate != value) { _bIsReAssignmentBasedOnAssignmentDate = value; } }
        }

        public bool? _bIsReAssignmentBasedOnAssignmentCompletionDate;
        public bool? IsReAssignmentBasedOnAssignmentCompletionDate
        {
            get { return _bIsReAssignmentBasedOnAssignmentCompletionDate; }
            set { if (this._bIsReAssignmentBasedOnAssignmentCompletionDate != value) { _bIsReAssignmentBasedOnAssignmentCompletionDate = value; } }
        }

        public int? _iReassignmentCount;
        /// <summary>
        /// Reassignment Count
        /// </summary>
        public int? ReassignmentCount
        {
            get { return _iReassignmentCount; }
            set { if (this._iReassignmentCount != value) { _iReassignmentCount = value; } }
        }

        public int? _iReAssignAfterDaysOf;
        /// <summary>
        /// Reassignment after days of 
        /// </summary>
        public int? ReAssignAfterDaysOf
        {
            get { return _iReAssignAfterDaysOf; }
            set { if (this._iReAssignAfterDaysOf != value) { _iReAssignAfterDaysOf = value; } }
        }
        public DateTime? _reAssignmentDateSet;
        /// <summary>
        /// ReAssignment Data Set
        /// </summary>
        public DateTime? ReAssignmentDateSet
        {
            get { return _reAssignmentDateSet; }
            set { if (this._reAssignmentDateSet != value) { _reAssignmentDateSet = value; } }
        }
        public bool? _bIsReassignNoDueDate;
        /// <summary>
        /// iS no Due Date
        /// </summary>
        public bool? IsReassignNoDueDate
        {
            get { return _bIsReassignNoDueDate; }
            set { if (this._bIsReassignNoDueDate != value) { _bIsReassignNoDueDate = value; } }
        }

        public bool? _bIsReassignDueBasedOnAssignmentCompletionDate;
        public bool? IsReassignDueBasedOnAssignmentCompletionDate
        {
            get { return _bIsReassignDueBasedOnAssignmentCompletionDate; }
            set { if (this._bIsReassignDueBasedOnAssignmentCompletionDate != value) { _bIsReassignDueBasedOnAssignmentCompletionDate = value; } }
        }

        public bool? _bIsReassignDueBasedOnReassignmentDate;
        public bool? IsReassignDueBasedOnReassignmentDate
        {
            get { return _bIsReassignDueBasedOnReassignmentDate; }
            set { if (this._bIsReassignDueBasedOnReassignmentDate != value) { _bIsReassignDueBasedOnReassignmentDate = value; } }
        }

        public int? _iReassignDueAfterDaysOf;
        /// <summary>
        /// Reassignment Due After Days of 
        /// </summary>
        public int? ReassignDueAfterDaysOf
        {
            get { return _iReassignDueAfterDaysOf; }
            set { if (this._iReassignDueAfterDaysOf != value) { _iReassignDueAfterDaysOf = value; } }
        }
        public DateTime? _reassignDueDateSet;
        /// <summary>
        /// ReAssignment Due Date
        /// </summary>
        public DateTime? ReassignDueDateSet
        {
            get { return _reassignDueDateSet; }
            set { if (this._reassignDueDateSet != value) { _reassignDueDateSet = value; } }
        }

        public bool? _bIsReassignNoExpiryDate;
        /// <summary>
        /// Is No Expiry Date for ReAssignment 
        /// </summary>
        public bool? IsReassignNoExpiryDate
        {
            get { return _bIsReassignNoExpiryDate; }
            set { if (this._bIsReassignNoExpiryDate != value) { _bIsReassignNoExpiryDate = value; } }
        }

        public bool? _bIsReassignExpiryBasedOnReassignmentDueDate;
        public bool? IsReassignExpiryBasedOnReassignmentDueDate
        {
            get { return _bIsReassignExpiryBasedOnReassignmentDueDate; }
            set { if (this._bIsReassignExpiryBasedOnReassignmentDueDate != value) { _bIsReassignExpiryBasedOnReassignmentDueDate = value; } }
        }

        public bool? _bIsReassignExpiryBasedOnReassignmentDate;
        public bool? IsReassignExpiryBasedOnReassignmentDate
        {
            get { return _bIsReassignExpiryBasedOnReassignmentDate; }
            set { if (this._bIsReassignExpiryBasedOnReassignmentDate != value) { _bIsReassignExpiryBasedOnReassignmentDate = value; } }
        }

        public bool? _bIsReassignExpiryBasedOnAssignmentCompletionDate;
        public bool? IsReassignExpiryBasedOnAssignmentCompletionDate
        {
            get { return _bIsReassignExpiryBasedOnAssignmentCompletionDate; }
            set { if (this._bIsReassignExpiryBasedOnAssignmentCompletionDate != value) { _bIsReassignExpiryBasedOnAssignmentCompletionDate = value; } }
        }

        public int? _iReassignExpireAfterDaysOf;
        /// <summary>
        /// Expiry after days for Reassignment
        /// </summary>
        public int? ReassignExpireAfterDaysOf
        {
            get { return _iReassignExpireAfterDaysOf; }
            set { if (this._iReassignExpireAfterDaysOf != value) { _iReassignExpireAfterDaysOf = value; } }
        }

        public DateTime? _reassignExpiryDateSet;
        /// <summary>
        /// Expiry Date for Reassignment
        /// </summary>
        public DateTime? ReassignExpiryDateSet
        {
            get { return _reassignExpiryDateSet; }
            set { if (this._reassignExpiryDateSet != value) { _reassignExpiryDateSet = value; } }
        }

        public string? _strParaLanguageId;
        /// <summary>
        /// Language Id for Parameter
        /// </summary>
        public string? ParaLanguageId
        {
            get { return _strParaLanguageId; }
            set { if (this._strParaLanguageId != value) { _strParaLanguageId = value; } }
        }

        public bool? _bIsActiveActivity;
        /// <summary>
        /// Activity Is Active
        /// </summary>
        public bool? IsActiveActivity
        {
            get { return _bIsActiveActivity; }
            set { if (this._bIsActiveActivity != value) { _bIsActiveActivity = value; } }
        }

        public string? _strUserDataXML;
        /// <summary>
        /// User Data XML
        /// </summary>
        public string? UserDataXML
        {
            get { return _strUserDataXML; }
            set { if (this._strUserDataXML != value) { _strUserDataXML = value; } }
        }

        public string? _strReviewerComments;
        /// <summary>
        /// ReviewerComments
        /// </summary>
        public string? ReviewerComments
        {
            get { return _strReviewerComments; }
            set { if (this._strReviewerComments != value) { _strReviewerComments = value; } }
        }

        public string? _strAttemptId;
        /// <summary>
        /// AttemptId
        /// </summary>
        public string? AttemptId
        {
            get { return _strAttemptId; }
            set { if (this._strAttemptId != value) { _strAttemptId = value; } }
        }

        public bool? _isAdminAssignment;
        public bool? IsAdminAssignment
        {
            get { return _isAdminAssignment; }
            set { if (this._isAdminAssignment != value) { _isAdminAssignment = value; } }
        }

        public bool? _bIsForAdminPreview;
        public bool? IsForAdminPreview
        {
            get { return _bIsForAdminPreview; }
            set { if (this._bIsForAdminPreview != value) { _bIsForAdminPreview = value; } }
        }

        public string? _strDisplayActivityStatus;
        /// <summary>
        /// Display Activity Status
        /// </summary>
        public string? DisplayActivityStatus
        {
            get { return _strDisplayActivityStatus; }
            set { if (this._strDisplayActivityStatus != value) { _strDisplayActivityStatus = value; } }
        }

        public string? _strScore;
        /// <summary>
        /// Display Score
        /// </summary>
        public string? Score
        {
            get { return _strScore; }
            set { if (this._strScore != value) { _strScore = value; } }
        }

        public DateTime? _assignFromDate;
        /// <summary>
        /// ReAssignment Due Date
        /// </summary>
        public DateTime? AssignFromDate
        {
            get { return _assignFromDate; }
            set { if (this._assignFromDate != value) { _assignFromDate = value; } }
        }
        public DateTime? _assignToDate;
        /// <summary>
        /// ReAssignment Due Date
        /// </summary>
        public DateTime? AssignToDate
        {
            get { return _assignToDate; }
            set { if (this._assignToDate != value) { _assignToDate = value; } }
        }


        public bool? _bForceReAssignment;
        /// <summary>
        /// To know the Print Certificate is allowed or not
        /// </summary>
        public bool? ForceReAssignment
        {
            get { return _bForceReAssignment; }
            set { if (this._bForceReAssignment != value) { _bForceReAssignment = value; } }
        }

        public bool? _CouseAssessmentFlag;
        /// <summary>
        /// CouseAssessmentFlag
        /// </summary>
        public bool? CouseAssessmentFlag
        {
            get { return _CouseAssessmentFlag; }
            set { if (this._CouseAssessmentFlag != value) { _CouseAssessmentFlag = value; } }
        }
      
        //added by Gitanjali 16.10.2010
        public string? _strCustomReportId;
        public string? CustomReportId
        {
            get { return _strCustomReportId; }
            set { if (this._strCustomReportId != value) { _strCustomReportId = value; } }
        }

        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public string? CategoryId { get; set; }
        public string? SubCategoryId { get; set; }

        public bool? _bIsApproved;
        public bool? IsApproved
        {
            get { return _bIsApproved; }
            set { if (this._bIsApproved != value) { _bIsApproved = value; } }
        }


        public bool? _bIsRejected;
        public bool? IsRejected
        {
            get { return _bIsRejected; }
            set { if (this._bIsRejected != value) { _bIsRejected = value; } }
        }

        //public bool _bIsCertificationProgram;
        //public bool IsCertificationProgram
        //{
        //    get { return _bIsCertificationProgram; }
        //    set { if (this._bIsCertificationProgram != value) { _bIsCertificationProgram = value; } }
        //}
    }

    //public enum ActivityContentType
    //{
    //    None,
    //    [Description("Scorm 1.2")]
    //    Scorm12,
    //    [Description("Scorm 2004")]
    //    Scorm2004,
    //    AICC,
    //    [Description("Non Compliant")]
    //    NonCompliant,
    //    Curriculum,
    //    Asset,
    //    Certification,
    //    Policy,
    //    Questionnaire,
    //    Course,
    //    Assessment,
    //    Classroom_Training,
    //    Virtual_Training
    //}
    //public enum ActivityAssignmentType
    //{
    //    OneTimeAssignment,
    //    DynamicAssignment,
    //    ReRegister
    //}
    //public enum ActivityAssignmentMode
    //{
    //    UI,
    //    BulkImport,
    //    BusinessRule,
    //    DefaultCourse
    //}
    //public enum ActivityCompletionCondition
    //{
    //    Optional,
    //    Mandatory
    //}
    //public enum ActivityCompletionStatus
    //{
    //    None,
    //    Completed,
    //    Started,
    //    [Description("Not Started")]
    //    NotStarted,
    //    [Description("In Progress")]
    //    InProgress,
    //    [Description("Pending Review")]
    //    PendingReview,
    //    [Description("Not Completed")]
    //    NotCompleted,
    //    [Description("Completed By Admin")]
    //    CompletedByAdmin,
    //    [Description("Completed By Proxy")]
    //    CompletedByProxy,
    //    [Description("Failed")]
    //    Failed
    //}
    //public enum MarkCompletionStatus
    //{
    //    Completed,
    //    NotCompleted
    //}

}
