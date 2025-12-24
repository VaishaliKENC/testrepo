/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author: Fattesinh & Shailesh
* Created:08/10/09
* Last Modified:08/10/09
*/
using System;
using System.ComponentModel;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class tblEmailDeliveryDashboard 
    /// </summary>
    [Serializable]
   public class EmailDeliveryDashboard : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public EmailDeliveryDashboard()
        { }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            CheckExistByName,
            Add,
            Update,
            Delete,
            GetPendingApproval
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetDynamicAssignmentUserList  
        }

        public enum ApprovalStatus
        {
            Draft,
            [Description("Pending Approval")]
            PendingApproval,
            Approved,
            [Description("Job was canceled")]
            JobWasCanceled,
            [Description("Unsent Preview Message")]
            UnsentPreviewMessage,
            [Description("Pending Text Approval")]
            PendingTextApproval,
            [Description("Email Sent")]
            EmailSent
        }

        private string _emailDeliveryTitle;
        public string EmailDeliveryTitle
        {
            get { return _emailDeliveryTitle; }
            set { if (this._emailDeliveryTitle != value) { _emailDeliveryTitle = value; } }
        }

        private string _RuleId;
        public string RuleId
        {
            get { return _RuleId; }
            set { if (this._RuleId != value) { _RuleId = value; } }
        }
       
        private string  _FromRecipants ;
        public string FromRecipants
        {
            get { return _FromRecipants; }
            set { if (this._FromRecipants != value) { _FromRecipants = value; } }
        }

        private bool _IsDynamicAssignment;  
        public bool IsDynamicAssignment
        {
            get { return _IsDynamicAssignment; }
            set { if (this._IsDynamicAssignment != value) { _IsDynamicAssignment = value; } }
        }

        private string  _AssignmentTypeID ;
        public string AssignmentTypeID
        {
            get { return _AssignmentTypeID; }
            set { if (this._AssignmentTypeID != value) { _AssignmentTypeID = value; } }
        }


        private string _emailTemplateID;
        public string EmailTemplateID
        {
            get { return _emailTemplateID; }
            set { if (this._emailTemplateID != value) { _emailTemplateID = value; } }
        }

        private string _distributionListId;
        public string DistributionListId
        {
            get { return _distributionListId; }
            set { if (this._distributionListId != value) { _distributionListId = value; } }
        }

        private string _cToList;
        /// <summary>
        /// Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com>
        /// </summary>
        public string ToList
        {
            get { return _cToList; }
            set { if (this._cToList != value) { _cToList = value; } }
        }

        private bool _isCCManager;
        public bool IsCCManager
        {
            get { return _isCCManager; }
            set { if (this._isCCManager != value) { _isCCManager = value; } }
        }

        private string _cCList;
        /// <summary>
        /// Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com>
        /// </summary>
        public string CCList
        {
            get { return _cCList; }
            set { if (this._cCList != value) { _cCList = value; } }
        }

        private bool _isBCCManager;
        public bool IsBCCManager
        {
            get { return _isBCCManager; }
            set { if (this._isBCCManager != value) { _isBCCManager = value; } }
        }

        private string _bCCList;
        /// <summary>
        /// Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com>
        /// </summary>
        public string BCCList
        {
            get { return _bCCList; }
            set { if (this._bCCList != value) { _bCCList = value; } }
        }

        private bool _isSiteDefaultLanguage;
        public bool IsSiteDefaultLanguage
        {
            get { return _isSiteDefaultLanguage; }
            set { if (this._isSiteDefaultLanguage != value) { _isSiteDefaultLanguage = value; } }
        }

        private bool _isUserPreferredLanguage;
        public bool IsUserPreferredLanguage
        {
            get { return _isUserPreferredLanguage; }
            set { if (this._isUserPreferredLanguage != value) { _isUserPreferredLanguage = value; } }
        }

        private bool _isAllLanguages;
        public bool IsAllLanguages
        {
            get { return _isAllLanguages; }
            set { if (this._isAllLanguages != value) { _isAllLanguages = value; } }
        }

        private bool _isInProcess;
        public bool IsInProcess
        {
            get { return _isInProcess; }
            set { if (this._isInProcess != value) { _isInProcess = value; } }
        }

        private bool _isPersonalized;
        public bool IsPersonalized
        {
            get { return _isPersonalized; }
            set { if (this._isPersonalized != value) { _isPersonalized = value; } }
        }

        private bool _isImmediate;
        public bool IsImmediate
        {
            get { return _isImmediate; }
            set { if (this._isImmediate != value) { _isImmediate = value; } }
        }

        private bool _isOneTime;
        public bool IsOneTime
        {
            get { return _isOneTime; }
            set { if (this._isOneTime != value) { _isOneTime = value; } }
        }

        private System.DateTime _oneTimeDateSet;
        public System.DateTime OneTimeDateSet
        {
            get { return _oneTimeDateSet; }
            set { if (this._oneTimeDateSet != value) { _oneTimeDateSet = value; } }
        }

        private bool _isDaily;
        public bool IsDaily
        {
            get { return _isDaily; }
            set { if (this._isDaily != value) { _isDaily = value; } }
        }

        private bool _isWeekly;
        public bool IsWeekly
        {
            get { return _isWeekly; }
            set { if (this._isWeekly != value) { _isWeekly = value; } }
        }

        private bool _isMonthly;
        public bool IsMonthly
        {
            get { return _isMonthly; }
            set { if (this._isMonthly != value) { _isMonthly = value; } }
        }

        private System.DateTime _dateTimeSet;
        public System.DateTime DateTimeSet
        {
            get { return _dateTimeSet; }
            set { if (this._dateTimeSet != value) { _dateTimeSet = value; } }
        }
        private System.DateTime _endDate;
        public System.DateTime EndDate
        {
            get { return _endDate; }
            set { if (this._endDate != value) { _endDate = value; } }
        }

        private int _recurrence;
        public int Recurrence
        {
            get { return _recurrence; }
            set { if (this._recurrence != value) { _recurrence = value; } }
        }

        private bool _isRecurrenceApprovalRequired;
        public bool IsRecurrenceApprovalRequired
        {
            get { return _isRecurrenceApprovalRequired; }
            set { if (this._isRecurrenceApprovalRequired != value) { _isRecurrenceApprovalRequired = value; } }
        }

        private string _deliveryApprovalStatus;
        public string DeliveryApprovalStatus
        {
            get { return _deliveryApprovalStatus; }
            set { if (this._deliveryApprovalStatus != value) { _deliveryApprovalStatus = value; } }
        }

        private string _approvedById;
        public string ApprovedById
        {
            get { return _approvedById; }
            set { if (this._approvedById != value) { _approvedById = value; } }
        }

        private System.DateTime _approvalDate;
        public System.DateTime ApprovalDate
        {
            get { return _approvalDate; }
            set { if (this._approvalDate != value) { _approvalDate = value; } }
        }

        private EmailTemplate _emailTemplate;
        public EmailTemplate EmailTemplate
        {
            get { return _emailTemplate; }
            set { if (this._emailTemplate != value) { _emailTemplate = value; } }
        }

        private string _strActivityId;
        public string ActivityId
        {
            get { return _strActivityId; }
            set { if (this._strActivityId != value) { _strActivityId = value; } }
        }

        private string _strAssignmentId;
        public string AssignmentId
        {
            get { return _strAssignmentId; }
            set { if (this._strAssignmentId != value) { _strAssignmentId = value; } }
        }

        private string _strAttachmentPathList;
        public string AttachmentPathList
        {
            get { return _strAttachmentPathList; }
            set { if (this._strAttachmentPathList != value) { _strAttachmentPathList = value; } }
        }

        private string _strTaskId;
        public string TaskId
        {
            get { return _strTaskId; }
            set { if (this._strTaskId != value) { _strTaskId = value; } }
        }
        private bool _bAddToDashboard=false;
        public bool AddToDashboard
        {
            get { return _bAddToDashboard; }
            set { if (this._bAddToDashboard != value) { _bAddToDashboard = value; } }
        }
        private string _strPreferredLangId;
        public string PreferredLanguageId
        {
            get { return _strPreferredLangId; }
            set { if (this._strPreferredLangId != value) { _strPreferredLangId = value; } }
        }
        // added by gitanjali 22.7.2010
        private string _cLearnerId;
        /// <summary>
        /// Comma ',' seperated learner list 
        /// </summary>
        public string LearnerId
        {
            get { return _cLearnerId; }
            set { if (this._cLearnerId != value) { _cLearnerId = value; } }
        }
        private ActivityAssignmentMode _strAssignmentMode;
        public ActivityAssignmentMode AssignmentMode
        {
            get { return _strAssignmentMode; }
            set { if (this._strAssignmentMode != value) { _strAssignmentMode = value; } }
        }
        
        // added by Gitanjali 26.08.2010
        private bool _bIsDistributionToManager = false;
        public bool IsDistributionToManager
        {
            get { return _bIsDistributionToManager; }
            set { if (this._bIsDistributionToManager != value) { _bIsDistributionToManager = value; } }
        }

        private string _strManagerEmailAddress;
        public string ManagerEmailId
        {
            get
            {
                return _strManagerEmailAddress;
            }
            set
            {
                if (this._strManagerEmailAddress != value)
                {
                    _strManagerEmailAddress = value;
                }
            }
        }

        private string _strManagerName;
        public string ManagerName
        {
            get
            {
                return _strManagerName;
            }
            set
            {
                if (this._strManagerName != value)
                {
                    _strManagerName = value;
                }
            }
        }

        // added by Gitanjali 27.12.2010
        private bool _bIsWeekdaysOnly = false;
        public bool IsWeekdaysOnly
        {
            get { return _bIsWeekdaysOnly; }
            set { if (this._bIsWeekdaysOnly != value) { _bIsWeekdaysOnly = value; } }
        }
        
    }
}