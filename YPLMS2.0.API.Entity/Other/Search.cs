/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh
* Created:25/08/09
* Last Modified:25/08/09
*/
using System.Collections.Generic;
using System;
namespace YPLMS2._0.API.Entity
{
   [Serializable] public class Search : BaseEntity
    {
        public Search()
        {
            _entSearchObject = new List<BaseEntity>();
        }

        /// <summary>
        /// ENUM Method
        /// </summary>
        public new enum Method
        {
            GetUserByKeyWord,
        }
        /// <summary>
        /// List method ENUM
        /// </summary>
        public new enum ListMethod
        {
            FindUsersByStatus,
            FindAllUsers,
            FindUsers,
            FindBussinessRuleUsers,
            FindUsersNotInRole,
            FindUsersInRole,
            FindClients,
            FindContentModule,
            FindCurriculums,
            FindQuestionnaires,
            FindCertifications,
            FindRegionView,
            SearchLearners,
            SearchRegionalAdmins,
            FindLearnersForAssignment,
            FindLearnersForAssignmentOptimized,
            FindLearnersForUnAssignment,
            FindLearnersForUnlockAssignment,
            FindLearnersForUnAssignmentOptimized,
            FindEmailDashBoard,
            FindEmailSentLog,
            FindImportHistory,
            FindSelfRegistration,
            FindAssignmentImportHistory,
            FindQuestionImportHistory,
            FindGroupReport,
            FindCustomFields,
            FindCustomFieldItems,
            FindLevelUnits,
            GroupEditAssignments,
            FindAllUsersForAllRole,
            FindBulkImportTasksSchedular,
            FindEmailDashBoardSchedular,
            FindReportDelivaryDashBoardSchedular,
            FindEmailTemplates,
            FindSubscriptionLicenses,
            FindAssessments,
            FindLearnersForSession,
            FindQuestionBanks,
            FindQuestionCategories,
            FindVirtualTrainingAllUsers,
            SearchLearnersForIPerform,
            FindILTSessionImportHistory,
        }

        private string _strKeyword;
        private string _strBussinessRuleId;
        private string _strManagerName;
        /// <summary>
        /// Key word
        /// </summary>
        public string KeyWord
        {
            get { return _strKeyword; }
            set { if (this._strKeyword != value) { _strKeyword = value; } }
        }

        public string BussinessRuleId
        {
            get { return _strBussinessRuleId; }
            set { if (this._strBussinessRuleId != value) { _strBussinessRuleId = value; } }
        }

        public string ManagerName
        {
            get { return _strManagerName; }
            set { if (this._strManagerName != value) { _strManagerName = value; } }

        }
        private List<BaseEntity> _entSearchObject;
        /// <summary>
        /// Search Object
        /// </summary>
        public List<BaseEntity> SearchObject1
        {
            get { return _entSearchObject; }
            set { if (this._entSearchObject != value) { _entSearchObject = value; } }
        }

        public List<object>? SearchObject { get; set; }

        public UserSearchCriteria UserCriteria { get; set; }

        private string _strSessionId;
        /// <summary>
        /// SessionId
        /// </summary>
        public string SessionId
        {
            get { return _strSessionId; }
            set { if (this._strSessionId != value) { _strSessionId = value; } }
        }

        private string _strProgramId;
        /// <summary>
        /// SessionId
        /// </summary>
        public string ProgramId
        {
            get { return _strProgramId; }
            set { if (this._strProgramId != value) { _strProgramId = value; } }
        }

        private string _strSystemUserGUID;
        /// <summary>
        /// SessionId
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private string _strLockType;

        public string LockType
        {
            get { return _strLockType; }
            set { if (this._strLockType != value) { _strLockType = value; } }
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
    }
}