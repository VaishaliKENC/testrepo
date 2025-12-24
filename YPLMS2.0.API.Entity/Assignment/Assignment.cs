/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:<Rajendra Yadav>
* Created:<05/10/09>
 *Last Modified By:Fattesinh Pisal
* Last Modified:<30/10/09>
 * 
*/
using System;
using static YPLMS2._0.API.Entity.AssignmentRequestModel;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    ///  class Assignment tblMasterActivityAssignment
    /// </summary>
    [Serializable]
   public class Assignment:BaseEntity
    {
        /// <summary>
        /// Default Contructor 
        /// <summary>
        public Assignment()
        { }


        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetUsersByRuleId,
            GetRules,
            BulkDelete,
            BulkDeactivate,
            GerForEmailTemplate
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            GetByName
        }

        /// <summary>
        /// enum Assignment Mode
        /// </summary>
        public enum AssignMode
        {
            Individual,
            CustomGroup,
            BulkImport,
            BusinessRule
        }        
        
        private string _strRuleId;
        public string RuleId
        {
            get { return _strRuleId; }
            set { if (this._strRuleId != value) { _strRuleId = value; } }
        }
        private string _strAssignmentName;
        public string AssignmentName
        {
            get { return _strAssignmentName; }
            set { if (this._strAssignmentName != value) { _strAssignmentName = value; } }
        }


        private string _strAssignmentDescription;
        public string AssignmentDescription
        {
            get { return _strAssignmentDescription; }
            set { if (this._strAssignmentDescription != value) { _strAssignmentDescription = value; } }
        }


        private ActivityContentType _strActivityType;
        public ActivityContentType ActivityType
        {
            get { return _strActivityType; }
            set { if (this._strActivityType != value) { _strActivityType = value; } }
        }

        private int _iTotalAssignments;
        public int TotalAssignments
        {
            get { return _iTotalAssignments; }
            set { if (this._iTotalAssignments != value) { _iTotalAssignments = value; } }
        }

        private int _iTotalCompletions;
        public int TotalCompletions
        {
            get { return _iTotalCompletions; }
            set { if (this._iTotalCompletions != value) { _iTotalCompletions = value; } }
        }

        //*********** New Properties


        private ActivityAssignmentMode _strAssignmentModeForOverride;
        public ActivityAssignmentMode AssignmentModeForOverride
        {
            get { return _strAssignmentModeForOverride; }
            set { if (this._strAssignmentModeForOverride != value) { _strAssignmentModeForOverride = value; } }
        }

        private bool _bIsAssignmentBasedOnHireDate;
        public bool IsAssignmentBasedOnHireDate
        {
            get { return _bIsAssignmentBasedOnHireDate; }
            set { if (this._bIsAssignmentBasedOnHireDate != value) { _bIsAssignmentBasedOnHireDate = value; } }
        }
        private bool _bIsAssignmentBasedOnCreationDate;
        public bool IsAssignmentBasedOnCreationDate
        {
            get { return _bIsAssignmentBasedOnCreationDate; }
            set { if (this._bIsAssignmentBasedOnCreationDate != value) { _bIsAssignmentBasedOnCreationDate = value; } }
        }

        private bool _bIsAssignmentBasedOnCurrentDate;
        public bool IsAssignmentBasedOnCurrentDate
        {
            get { return _bIsAssignmentBasedOnCurrentDate; }
            set { if (this._bIsAssignmentBasedOnCurrentDate != value) { _bIsAssignmentBasedOnCurrentDate = value; } }
        }

         private bool _bIsNewHireAssignmentBasedOnCurrentDate;
        public bool IsNewHireAssignmentBasedOnCurrentDate
        {
            get { return _bIsNewHireAssignmentBasedOnCurrentDate; }
            set { if (this._bIsNewHireAssignmentBasedOnCurrentDate != value) { _bIsNewHireAssignmentBasedOnCurrentDate = value; } }
        }

     
         



        private int _iAssignAfterDaysOf;
        public int AssignAfterDaysOf
        {
            get { return _iAssignAfterDaysOf; }
            set { if (this._iAssignAfterDaysOf != value) { _iAssignAfterDaysOf = value; } }
        }

        private System.DateTime _dateAssignmentDateSet;
        public System.DateTime AssignmentDateSet
        {
            get { return _dateAssignmentDateSet; }
            set { if (this._dateAssignmentDateSet != value) { _dateAssignmentDateSet = value; } }
        }


        private bool _isNoDueDate;
        public bool IsNoDueDate
        {
            get { return _isNoDueDate; }
            set { if (this._isNoDueDate != value) { _isNoDueDate = value; } }
        }

        private bool _bIsDueBasedOnAssignDate;
        public bool IsDueBasedOnAssignDate
        {
            get { return _bIsDueBasedOnAssignDate; }
            set { if (this._bIsDueBasedOnAssignDate != value) { _bIsDueBasedOnAssignDate = value; } }
        }
        private bool _bIsDueBasedOnHireDate;
        public bool IsDueBasedOnHireDate
        {
            get { return _bIsDueBasedOnHireDate; }
            set { if (this._bIsDueBasedOnHireDate != value) { _bIsDueBasedOnHireDate = value; } }
        }
        private bool _bIsDueBasedOnCreationDate;
        public bool IsDueBasedOnCreationDate
        {
            get { return _bIsDueBasedOnCreationDate; }
            set { if (this._bIsDueBasedOnCreationDate != value) { _bIsDueBasedOnCreationDate = value; } }
        }

        private bool _bIsDueBasedOnStartDate;
        public bool IsDueBasedOnStartDate
        {
            get { return _bIsDueBasedOnStartDate; }
            set { if (this._bIsDueBasedOnStartDate != value) { _bIsDueBasedOnStartDate = value; } }
        }
        private int _iDueAfterDaysOf;
        public int DueAfterDaysOf
        {
            get { return _iDueAfterDaysOf; }
            set { if (this._iDueAfterDaysOf != value) { _iDueAfterDaysOf = value; } }
        }

        private System.DateTime _dateDueDateSet;
        public System.DateTime DueDateSet
        {
            get { return _dateDueDateSet; }
            set { if (this._dateDueDateSet != value) { _dateDueDateSet = value; } }
        }


        private bool _isNoExpiryDate;
        public bool IsNoExpiryDate
        {
            get { return _isNoExpiryDate; }
            set { if (this._isNoExpiryDate != value) { _isNoExpiryDate = value; } }
        }

        private bool _bIsExpiryBasedOnAssignDate;
        public bool IsExpiryBasedOnAssignDate
        {
            get { return _bIsExpiryBasedOnAssignDate; }
            set { if (this._bIsExpiryBasedOnAssignDate != value) { _bIsExpiryBasedOnAssignDate = value; } }
        }

        private bool _bIsExpiryBasedOnStartDate;
        public bool IsExpiryBasedOnStartDate
        {
            get { return _bIsExpiryBasedOnStartDate; }
            set { if (this._bIsExpiryBasedOnStartDate != value) { _bIsExpiryBasedOnStartDate = value; } }
        }

        private bool _bIsExpiryBasedOnDueDate;
        public bool IsExpiryBasedOnDueDate
        {
            get { return _bIsExpiryBasedOnDueDate; }
            set { if (this._bIsExpiryBasedOnDueDate != value) { _bIsExpiryBasedOnDueDate = value; } }
        }
        private int _iExpireAfterDaysOf;
        public int ExpireAfterDaysOf
        {
            get { return _iExpireAfterDaysOf; }
            set { if (this._iExpireAfterDaysOf != value) { _iExpireAfterDaysOf = value; } }
        }

        private System.DateTime _dateExpiryDateSet;
        public System.DateTime ExpiryDateSet
        {
            get { return _dateExpiryDateSet; }
            set { if (this._dateExpiryDateSet != value) { _dateExpiryDateSet = value; } }
        }

        private ActivityCompletionCondition _strCompletionConditionId;
        public ActivityCompletionCondition CompletionConditionId
        {
            get { return _strCompletionConditionId; }
            set { if (this._strCompletionConditionId != value) { _strCompletionConditionId = value; } }
        }


        private bool _IsOverridePreviousAssignments;
        public bool OverridePreviousAssignments
        {
            get { return _IsOverridePreviousAssignments; }
            set { if (this._IsOverridePreviousAssignments != value) { _IsOverridePreviousAssignments = value; } }
        }
        
        /// -------------- For New Hire

        private System.DateTime _newHireFromDate;
        public System.DateTime NewHireFromDate
        {
            get { return _newHireFromDate; }
            set { if (this._newHireFromDate != value) { _newHireFromDate = value; } }
        }

        private System.DateTime _newHireToDate;
        public System.DateTime NewHireToDate
        {
            get { return _newHireToDate; }
            set { if (this._newHireToDate != value) { _newHireToDate = value; } }
        }

        private bool _bIsNewHireAssignmentBasedOnHireDate;
        public bool IsNewHireAssignmentBasedOnHireDate
        {
            get { return _bIsNewHireAssignmentBasedOnHireDate; }
            set { if (this._bIsNewHireAssignmentBasedOnHireDate != value) { _bIsNewHireAssignmentBasedOnHireDate = value; } }
        }
        private bool _bIsNewHireAssignmentBasedOnCreationDate;
        public bool IsNewHireAssignmentBasedOnCreationDate
        {
            get { return _bIsNewHireAssignmentBasedOnCreationDate; }
            set { if (this._bIsNewHireAssignmentBasedOnCreationDate != value) { _bIsNewHireAssignmentBasedOnCreationDate = value; } }
        }
        private int _iNewHireAssignAfterDaysOf;
        public int NewHireAssignAfterDaysOf
        {
            get { return _iNewHireAssignAfterDaysOf; }
            set { if (this._iNewHireAssignAfterDaysOf != value) { _iNewHireAssignAfterDaysOf = value; } }
        }

        private System.DateTime _dateNewHireAssignmentDateSet;
        public System.DateTime NewHireAssignmentDateSet
        {
            get { return _dateNewHireAssignmentDateSet; }
            set { if (this._dateNewHireAssignmentDateSet != value) { _dateNewHireAssignmentDateSet = value; } }
        }

        private bool _isNoNewHireDueDate;
        public bool IsNoNewHireDueDate
        {
            get { return _isNoNewHireDueDate; }
            set { if (this._isNoNewHireDueDate != value) { _isNoNewHireDueDate = value; } }
        }

        private bool _bIsNewHireDueBasedOnAssignDate;
        public bool IsNewHireDueBasedOnAssignDate
        {
            get { return _bIsNewHireDueBasedOnAssignDate; }
            set { if (this._bIsNewHireDueBasedOnAssignDate != value) { _bIsNewHireDueBasedOnAssignDate = value; } }
        }
        private bool _bIsNewHireDueBasedOnHireDate;
        public bool IsNewHireDueBasedOnHireDate
        {
            get { return _bIsNewHireDueBasedOnHireDate; }
            set { if (this._bIsNewHireDueBasedOnHireDate != value) { _bIsNewHireDueBasedOnHireDate = value; } }
        }
        private bool _bIsNewHireDueBasedOnCreationDate;
        public bool IsNewHireDueBasedOnCreationDate
        {
            get { return _bIsNewHireDueBasedOnCreationDate; }
            set { if (this._bIsNewHireDueBasedOnCreationDate != value) { _bIsNewHireDueBasedOnCreationDate = value; } }
        }

        private bool _bIsNewHireDueBasedOnStartDate;
        public bool IsNewHireDueBasedOnStartDate
        {
            get { return _bIsNewHireDueBasedOnStartDate; }
            set { if (this._bIsNewHireDueBasedOnStartDate != value) { _bIsNewHireDueBasedOnStartDate = value; } }
        }
        private int _iNewHireDueAfterDaysOf;
        public int NewHireDueAfterDaysOf
        {
            get { return _iNewHireDueAfterDaysOf; }
            set { if (this._iNewHireDueAfterDaysOf != value) { _iNewHireDueAfterDaysOf = value; } }
        }

        private System.DateTime _dateNewHireDueDateSet;
        public System.DateTime NewHireDueDateSet
        {
            get { return _dateNewHireDueDateSet; }
            set { if (this._dateNewHireDueDateSet != value) { _dateNewHireDueDateSet = value; } }
        }

        private bool _isNoNewHireExpiryDate;
        public bool IsNoNewHireExpiryDate
        {
            get { return _isNoNewHireExpiryDate; }
            set { if (this._isNoNewHireExpiryDate != value) { _isNoNewHireExpiryDate = value; } }
        }

        private bool _bIsNewHireExpiryBasedOnAssignDate;
        public bool IsNewHireExpiryBasedOnAssignDate
        {
            get { return _bIsNewHireExpiryBasedOnAssignDate; }
            set { if (this._bIsNewHireExpiryBasedOnAssignDate != value) { _bIsNewHireExpiryBasedOnAssignDate = value; } }
        }

        private bool _bIsNewHireExpiryBasedOnStartDate;
        public bool IsNewHireExpiryBasedOnStartDate
        {
            get { return _bIsNewHireExpiryBasedOnStartDate; }
            set { if (this._bIsNewHireExpiryBasedOnStartDate != value) { _bIsNewHireExpiryBasedOnStartDate = value; } }
        }

        private bool _bIsNewHireExpiryBasedOnDueDate;
        public bool IsNewHireExpiryBasedOnDueDate
        {
            get { return _bIsNewHireExpiryBasedOnDueDate; }
            set { if (this._bIsNewHireExpiryBasedOnDueDate != value) { _bIsNewHireExpiryBasedOnDueDate = value; } }
        }
        private int _iNewHireExpireAfterDaysOf;
        public int NewHireExpireAfterDaysOf
        {
            get { return _iNewHireExpireAfterDaysOf; }
            set { if (this._iNewHireExpireAfterDaysOf != value) { _iNewHireExpireAfterDaysOf = value; } }
        }

        private System.DateTime _dateNewHireExpiryDateSet;
        public System.DateTime NewHireExpiryDateSet
        {
            get { return _dateNewHireExpiryDateSet; }
            set { if (this._dateNewHireExpiryDateSet != value) { _dateNewHireExpiryDateSet = value; } }
        }
      
        //-- For Re-Assignment

        private bool _bIsReAssignmentBasedOnAssignmentDate;
        public bool IsReAssignmentBasedOnAssignmentDate
        {
            get { return _bIsReAssignmentBasedOnAssignmentDate; }
            set { if (this._bIsReAssignmentBasedOnAssignmentDate != value) { _bIsReAssignmentBasedOnAssignmentDate = value; } }
        }

        private bool _bIsReAssignmentBasedOnAssignmentCompletionDate;
        public bool IsReAssignmentBasedOnAssignmentCompletionDate
        {
            get { return _bIsReAssignmentBasedOnAssignmentCompletionDate; }
            set { if (this._bIsReAssignmentBasedOnAssignmentCompletionDate != value) { _bIsReAssignmentBasedOnAssignmentCompletionDate = value; } }
        }

        private int _iReAssignAfterDaysOf;
        /// <summary>
        /// Reassignment after days of 
        /// </summary>
        public int ReAssignAfterDaysOf
        {
            get { return _iReAssignAfterDaysOf; }
            set { if (this._iReAssignAfterDaysOf != value) { _iReAssignAfterDaysOf = value; } }
        }
        private DateTime? _reAssignmentDateSet;
        /// <summary>
        /// ReAssignment Data Set
        /// </summary>
        public DateTime? ReAssignmentDateSet
        {
            get { return _reAssignmentDateSet; }
            set { if (this._reAssignmentDateSet != value) { _reAssignmentDateSet = value; } }
        }
        private bool _bIsReassignNoDueDate;
        /// <summary>
        /// iS no Due Date
        /// </summary>
        public bool IsReassignNoDueDate
        {
            get { return _bIsReassignNoDueDate; }
            set { if (this._bIsReassignNoDueDate != value) { _bIsReassignNoDueDate = value; } }
        }

        private bool _bIsReassignDueBasedOnAssignmentCompletionDate;
        public bool IsReassignDueBasedOnAssignmentCompletionDate
        {
            get { return _bIsReassignDueBasedOnAssignmentCompletionDate; }
            set { if (this._bIsReassignDueBasedOnAssignmentCompletionDate != value) { _bIsReassignDueBasedOnAssignmentCompletionDate = value; } }
        }

        private bool _bIsReassignDueBasedOnReassignmentDate;
        public bool IsReassignDueBasedOnReassignmentDate
        {
            get { return _bIsReassignDueBasedOnReassignmentDate; }
            set { if (this._bIsReassignDueBasedOnReassignmentDate != value) { _bIsReassignDueBasedOnReassignmentDate = value; } }
        }


        private int _iReassignDueAfterDaysOf;
        /// <summary>
        /// Reassignment Due After Days of 
        /// </summary>
        public int ReassignDueAfterDaysOf
        {
            get { return _iReassignDueAfterDaysOf; }
            set { if (this._iReassignDueAfterDaysOf != value) { _iReassignDueAfterDaysOf = value; } }
        }
        private DateTime? _reassignDueDateSet;
        /// <summary>
        /// ReAssignment Due Date
        /// </summary>
        public DateTime? ReassignDueDateSet
        {
            get { return _reassignDueDateSet; }
            set { if (this._reassignDueDateSet != value) { _reassignDueDateSet = value; } }
        }

        private bool _bIsReassignNoExpiryDate;
        /// <summary>
        /// Is No Expiry Date for ReAssignment 
        /// </summary>
        public bool IsReassignNoExpiryDate
        {
            get { return _bIsReassignNoExpiryDate; }
            set { if (this._bIsReassignNoExpiryDate != value) { _bIsReassignNoExpiryDate = value; } }
        }
        private bool _bIsReassignExpiryBasedOnReassignmentDueDate;
        public bool IsReassignExpiryBasedOnReassignmentDueDate
        {
            get { return _bIsReassignExpiryBasedOnReassignmentDueDate; }
            set { if (this._bIsReassignExpiryBasedOnReassignmentDueDate != value) { _bIsReassignExpiryBasedOnReassignmentDueDate = value; } }
        }

        private bool _bIsReassignExpiryBasedOnReassignmentDate;
        public bool IsReassignExpiryBasedOnReassignmentDate
        {
            get { return _bIsReassignExpiryBasedOnReassignmentDate; }
            set { if (this._bIsReassignExpiryBasedOnReassignmentDate != value) { _bIsReassignExpiryBasedOnReassignmentDate = value; } }
        }

        private bool _bIsReassignExpiryBasedOnAssignmentCompletionDate;
        public bool IsReassignExpiryBasedOnAssignmentCompletionDate
        {
            get { return _bIsReassignExpiryBasedOnAssignmentCompletionDate; }
            set { if (this._bIsReassignExpiryBasedOnAssignmentCompletionDate != value) { _bIsReassignExpiryBasedOnAssignmentCompletionDate = value; } }
        }


        private int _iReassignExpireAfterDaysOf;
        /// <summary>
        /// Expiry after days for Reassignment
        /// </summary>
        public int ReassignExpireAfterDaysOf
        {
            get { return _iReassignExpireAfterDaysOf; }
            set { if (this._iReassignExpireAfterDaysOf != value) { _iReassignExpireAfterDaysOf = value; } }
        }

        private DateTime? _reassignExpiryDateSet;
        /// <summary>
        /// Expiry Date for Reassignment
        /// </summary>
        public DateTime? ReassignExpiryDateSet
        {
            get { return _reassignExpiryDateSet; }
            set { if (this._reassignExpiryDateSet != value) { _reassignExpiryDateSet = value; } }
        }
        /// - New properties added as on 23-Nov-09

      
      
      
        /// - Code for New Property added on 23-Nov-09 end here.
        ///----------------------- Other properties
        
        private bool _IsSendEmail;
        public bool SendEmail
        {
            get { return _IsSendEmail; }
            set { if (this._IsSendEmail != value) { _IsSendEmail = value; } }
        }


        private string _strSendEmailType;
        public string SendEmailType
        {
            get { return _strSendEmailType; }
            set { if (this._strSendEmailType != value) { _strSendEmailType = value; } }
        }


        private string _strEmailTemplateId;
        public string EmailTemplateId
        {
            get { return _strEmailTemplateId; }
            set { if (this._strEmailTemplateId != value) { _strEmailTemplateId = value; } }
        }
        private AssignMode _enumMode;
        /// <summary>
        ///  Assignment Mode
        /// </summary>
        public AssignMode AssignmentMode
        {
            get { return _enumMode; }
            set { if (this._enumMode != value) { _enumMode = value; } }
        }

        ////-----------------

        private string _strPreviousRuleId;
        /// <summary>
        /// Used in update/edit master activity assignment record from tblMasterActivityAssignment.
        /// </summary>
        public string PreviousRuleId
        {
            get { return _strPreviousRuleId; }
            set { if (this._strPreviousRuleId != value) { _strPreviousRuleId = value; } }
        }
        private string _strModifiedByName;
        public string ModifiedByName
        {
            get { return _strModifiedByName; }
            set { if (this._strModifiedByName != value) { _strModifiedByName = value; } }
        }

        private bool _bIsActive;
        public bool IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
        }

        private bool _bIsActivityCategory;
        public bool IsActivityCategory
        {
            get { return _bIsActivityCategory; }
            set { if (this._bIsActivityCategory != value) { _bIsActivityCategory = value; } }
        }


        private string _strCategoryName;
        public string CategoryName
        {
            get { return _strCategoryName; }
            set { if (this._strCategoryName != value) { _strCategoryName = value; } }
        }

        private string _strCategoryId;
        public string CategoryId
        {
            get { return _strCategoryId; }
            set { if (this._strCategoryId != value) { _strCategoryId = value; } }
        }

        public bool IsMailChecked { get; set; } = false;
        public bool rdbtnSchedule { get; set; } = false;
        public bool rdbtnAuto { get; set; } = false;
     

        public List<ActivityModel> Activities { get; set; }
        public List<LearnerModel> Learners { get; set; }

        public int AssignmentDateType { get; set; } // Corresponds to ddlAssignmentDate.SelectedIndex
        public string? AssignmentDateText { get; set; }
        public string? AssignmentDaysText { get; set; }

        public bool NoDueDate { get; set; }
        public int DueDateType { get; set; }
        public string? DueDateText { get; set; }
        public string? DueDaysText { get; set; }

        public bool NoExpiryDate { get; set; }
        public int ExpiryDateType { get; set; }
        public string? ExpiryDateText { get; set; }
        public string? ExpiryDaysText { get; set; }



        public bool IsReassignmentDateEmpty { get; set; }

        public int ReassignmentDateType { get; set; } // ddlReassignmentDate.SelectedIndex
        public string? ReassignmentDateText { get; set; }
        public string? ReassignmentDaysText { get; set; }

        public bool NoReassignDueDate { get; set; }
        public int ReassignDueDateType { get; set; } // ddlReDueDate.SelectedIndex
        public string? ReassignDueDateText { get; set; }
        public string? ReassignDueDaysText { get; set; }

        public bool NoReassignExpiryDate { get; set; }
        public int ReassignExpiryDateType { get; set; } // ddlReExpiryDate.SelectedIndex
        public string? ReassignExpiryDateText { get; set; }
        public string? ReassignExpiryDaysText { get; set; }

        private List<ActivityAssignment> _lstActAssignments;
        public List<ActivityAssignment> LstActAssignments
        {
            get { return _lstActAssignments; }
            set { _lstActAssignments = value; }
        }

        private string _strActivtyId;
        public string ActivtyId
        {
            get { return _strActivtyId; }
            set { _strActivtyId = value; }
        }
        
        public string SystemUserGuid { get; set; }
        

        private EmailTemplate _emailTemplate;
        public EmailTemplate EmailTemplate
        {
            get { return _emailTemplate; }
            set { _emailTemplate = value; }
        }
        
        private bool isActivitySelected;
        public bool IsActivitySelected
        {
            get { return isActivitySelected; }
            set { isActivitySelected = value; }
        }

        private bool isLearnerSelected;
        public bool IsLearnerSelected
        {
            get { return isLearnerSelected; }
            set { isLearnerSelected = value; }
        }
        private bool _ForceReassignment;
        public bool ForceReassignment
        {
            get { return _ForceReassignment; }
            set { if (this._ForceReassignment != value) { _ForceReassignment = value; } }
        }

        public bool IsReassignmentChecked { get; set; }
        public bool IsDirectSendMail { get; set; }

        public class SaveAssignmentInputModel
        {
            public bool PageIsValid { get; set; }
            public bool chkAssignmentDates { get; set; }
            public bool chkIsForDynamic { get; set; }

            public bool rbAbsoluteDate { get; set; }
            public bool rbRelativeDate { get; set; }
            public string? txtDefaultAssignmnetDays { get; set; }
            public string? txtAssignmentDays { get; set; }
            public string? ddlAssignmentDate { get; set; }            
            public bool rbAbsoluteDueDate { get; set; }
            public bool rbRelativeDueDate { get; set; }
            public bool rbNoDueDate { get; set; }
            public string? txtDefaultDueDays { get; set; }
            public string? txtDueDays { get; set; }
            public string? ddlDueDate { get; set; }

            public bool rbAbsoluteExpiryDate { get; set; }
            public bool rbRelativeExpiryDate { get; set; }
            public bool rbNoExpiryDate { get; set; }
            public string? txtDefaultExpDays { get; set; }
            public string? txtExprDays { get; set; }
            public string? ddlExprDate { get; set; }

            public string? ClientId { get; set; }
            public string? CurrentUrl { get; set; }
            public string? CreatedById { get; set; }
        }

        public class AssignmentParameters
        {

            public AssignmentParameters() { }

            private string _strSystemUserGuid;
            private string _strActivtyId;
            private EmailTemplate _emailTemplate;

            private bool isActivitySelected;
            private bool isLearnerSelected;
            private bool isBusinessRuleSelected;

            private List<ActivityAssignment> _lstActAssignments;

            private string _ValDateErrorMsg;
            private string _ZeroMsg;
            private string _ReqDaysErrorMsg;
            private string _ReqDateErrorMsg;
            private string _ScriptKey;
            private string _assignmentMode;
            private bool isCertificationProgram; // to check if certification Program


            public EmailTemplate EmailTemplate
            {
                get { return _emailTemplate; }
                set { _emailTemplate = value; }
            }

            public List<ActivityAssignment> LstActAssignments
            {
                get { return _lstActAssignments; }
                set { _lstActAssignments = value; }
            }

            public bool IsActivitySelected
            {
                get { return isActivitySelected; }
                set { isActivitySelected = value; }
            }
            public bool IsLearnerSelected
            {
                get { return isLearnerSelected; }
                set { isLearnerSelected = value; }
            }
            public bool IsBusinessRuleSelected
            {
                get { return isBusinessRuleSelected; }
                set { isBusinessRuleSelected = value; }
            }

            public string SystemUserGuid
            {
                get { return _strSystemUserGuid; }
                set { _strSystemUserGuid = value; }
            }
            public string ActivtyId
            {
                get { return _strActivtyId; }
                set { _strActivtyId = value; }
            }


            public string ValDateErrorMsg
            {
                get { return _ValDateErrorMsg; }
                set { _ValDateErrorMsg = value; }
            }
            public string ZeroMsg
            {
                get { return _ZeroMsg; }
                set { _ZeroMsg = value; }
            }
            public string ReqDaysErrorMsg
            {
                get { return _ReqDaysErrorMsg; }
                set { _ReqDaysErrorMsg = value; }
            }
            public string ReqDateErrorMsg
            {
                get { return _ReqDateErrorMsg; }
                set { _ReqDateErrorMsg = value; }
            }
            public string ScriptKey
            {
                get { return _ScriptKey; }
                set { _ScriptKey = value; }
            }

            //public bool IsCertificationProgram
            //{
            //    get { return isCertificationProgram; }
            //    set { isCertificationProgram = value; }
            //}
        }
    }
}
