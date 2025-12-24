/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author: Ashish and Shailesh Patil
* Created:<15/10/09>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class Report : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public Report()
        {
            _listAdditionalReportParameters = new List<ReportParameters>();
            _columns = new List<ReportColumn>();
            _parameters = new List<ReportParameter>();
        }

        public new enum ListMethod
        {
            ClientExpiresWithinMonth,
            ClientExpiresWithinMonthAdminHome,
            ClientContact,
            ClientContactAdminHome,
            ClientSummary,
            CourseLibrary,
            CourseAssignment,
            CourseAssignmentAdminHome,
            CertificationAssignment,
            CertificationAssignmentAdminHome,
            CurriculumAssignment,
            CurriculumAssignmentAdminHome,
            SystemErrorLog,
            Statistics,            
            CourseLicensingByClientEnrollmentConsumed,
            CourseLicensingByClientEnrollmentBasis,
            CourseLicensingByClientSubscriptionBasis,
            CourseLicensingByCourse,
            DelinquencyHistory,
            ActivityCompletionProgress,
            UsersByRole,
            LearnerDump,
            LearnerListByOrgGroup,
            AssignedUsersByActivityStatus,
            CourseAssessmentInteractionTrackingReport,
            AssignedUsersByActivityStatusAll, //For Previous Tracking
            NotAssignedUsersByActivity,
            AggregateResultsByQuestion,
            NonPreferredAnswersForCertification,
            GetUserAssignmentsForMark,
            GetUserAssignmentsForMarkCalendar,
            UserNonpreferredAnswersForCertification,
            DetailedCertificationResultsbyUser,
            DetailedCertificationUserResponses,
            GetUserCertificationResponses,
            ClientSummaryDashboard,
            ClientSummaryDashboardAdminHome,
            ActivityCompletionProgressChart,
            ActivityCompletionProgressChartMultiple,
            RunReportingTool,
            RunReportingToolWithWhere,
            RunReportingToolGroupBy,
            RunReportingToolGroupDtls,
            GetQuestionnaireResponse,
            GetCertQuestionnaireResponse,
            GetAssignmentsForCompleteByUser,
            LearnerUserActivityAssignmentNotCompletedDashboard,
            LearnerUserActivityAssignmentNotCompletedMainPage,
            LearnerUserActivityAssignmentCompletedDashboard,
            LearnerUserActivityAssignmentCompletedMainPage,
            LearnerUserCertifiedTrainingNotCompletedDashboard, //Added for certified trainer
            LearnerUserCertifiedTrainingNotCompletedMainPage,
            LearnerCurriculumActivityLstAll,
            LearnerCertificationActivityLstAll,
            ShowAssigneUsers,
            GetAllLookupValues,
            GetAllFreeFormText,
            GetCertificationResponseForPDF,
            GetCertQuestionnaireResponseType,
			PendingReview,
            GetAssessmentResponse,
            AssessmentIndividualResult,
            AssessmentIndividualResultByQuestion,
            AssessmentAggregrateResult,
            AssessmentUsers,
            AssessmentCompletion,
            AssessmentInProgress,
            ClassroomTrainingSession,
            ClassroomTrainingNominationAttendeeBySession,
            OrderHistoryReport,
            CourseAssessmentReport,
            QuestionnaireIndividualResult,
            QuestionnaireCompletion,
            QuestionnaireInProgress,
            QuestionnaireIndividualResultByQuestion,
            QuestionnaireAggregrateResult,
            QuestionnaireUsers,
            DetailedQuestionnaireResultsbyUser,
            GetQuestionnaireQuestResponse,
            GetQuestionnaireQuestResponseType,
            GetCourseObjectiveTracking,
            GetCourseInteractionTracking,
            GetQuestionnaireResponseForPDF,
            VirtualTrainingStatus,
            VirtualTrainingAttendeeResult,
            GetMOHRsult,
            ActivityCompletionGraphDashboard,
            RecentlyAccessedActivityCompletionGraph,
            MostpurchasedCatalogues,
            GroupwiseAssignmentsCompletionGraph,
            GetNewsLetterSubscriptionData,
            GetAbandonedCartReport,
            GetProductDetailReport,
            CurriculumDetailsReport,
            ModuleDetailsReport,
            TrainingAttendanceReport,
            EventDetailReport,
            ILTSessionDetailsReport,
            UserWiseILTReport
        }

        List<ReportColumn> _columns;
        public List<ReportColumn> Columns
        {
            get { return _columns; }
        }

        List<ReportParameter> _parameters;
        public List<ReportParameter> Parameters
        {
            get { return _parameters; }
        }

        /// <summary>
        /// Add Parameter to Parameter List
        /// </summary>
        /// <param name="pParameter"></param>
        public void AddParameter(ReportParameter pParameter)
        {
            _parameters.Add(pParameter);
        }

        /// <summary>
        /// Add Parameter with name and DataType to Parameter List
        /// </summary>
        /// <param name="pParameter"></param>
        public void AddParameter(string pParameterName, ReportDataType pParameterDataType, object pValue)
        {
            ReportParameter rParameter = new ReportParameter();
            rParameter.Name = pParameterName;
            rParameter.DataType = pParameterDataType;
            rParameter.Value = pValue;
            AddParameter(rParameter);
        }

        private List<ReportParameters> _listAdditionalReportParameters;
        /// <summary>
        /// ReportParameterGroup List
        /// </summary>
        public List<ReportParameters> AdditionalReportParameterGroupList
        {
            get { return _listAdditionalReportParameters; }
        }

        private string strWhereClause;
        /// <summary>
        /// Where Clause
        /// </summary>
        public string WhereClause
        {
            get 
            {
                return strWhereClause; 
            }
            set 
            { 
                if (this.strWhereClause != value) 
                { 
                    strWhereClause = value; 
                }
            }
        }
        private string strClientId;
        /// <summary>
        /// Where Clause
        /// </summary>
        public string ClientId
        {
            get
            {
                return strClientId;
            }
            set
            {
                if (this.strClientId != value)
                {
                    strClientId = value;
                }
            }
        }
    }

    /// <summary>
    /// DataTypes
    /// </summary>
    public enum ReportDataType
    {
        NVarChar = 0,
        Date,
        Numeric,
        VarChar,
        Boolean
    }

    /// <summary>
    /// Column
    /// </summary>
   [Serializable] public class ReportColumn
    {
        string strName;
        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }
        ReportDataType dataType;
        public ReportDataType DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }
    }

    /// <summary>
    /// Parameter
    /// </summary>
   [Serializable] public class ReportParameter
    {
        string strName;
        public string Name
        {
            get { return strName; }
            set { strName = value; }
        }

        object objValue;
        public object Value
        {
            get { return objValue; }
            set { objValue = value; }
        }

        ReportDataType dataType;
        public ReportDataType DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }
    }
}