using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager.Reports
{
    /// <summary>
    /// Used for Standard Report to return DataSet Only
    /// </summary>
    public class StandardReportDAM
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        const string _strMessageId = YPLMS.Services.Messages.Report.DL_ERROR;
        string _strConnString = string.Empty;
        DataSet _dsReturn = null;
        StringBuilder sb = null;
        #endregion

        /// <summary>
        /// Get Standard Reports
        /// </summary>
        /// <param name="pEntReport"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public DataSet GetStandardReports(Report pEntReport, Report.ListMethod pMethod)
        {
            string strSPName = string.Empty;
            switch (pMethod)
            {
                case Report.ListMethod.ClientExpiresWithinMonth:
                    strSPName = Schema.StandardReportSP.CLIENT_EXPIRES_WITHIN_MONTH;
                    break;
                case Report.ListMethod.ClientExpiresWithinMonthAdminHome:
                    strSPName = Schema.StandardReportSP.CLIENT_EXPIRES_WITHIN_MONTH_ADMIN_HOME;
                    break;
                case Report.ListMethod.ShowAssigneUsers:
                    strSPName = Schema.StandardReportSP.PROC_SHOW_ACTIVITY_ASSIGNED_LEARNER;
                    break;
                case Report.ListMethod.ClientContact:
                    strSPName = Schema.StandardReportSP.CLIENT_CONTACT;
                    break;
                case Report.ListMethod.ClientContactAdminHome:
                    strSPName = Schema.StandardReportSP.CLIENT_CONTACT_ADMIN_HOME;
                    break;
                case Report.ListMethod.ClientSummary:
                    strSPName = Schema.StandardReportSP.CLIENT_SUMMARY;
                    break;
                case Report.ListMethod.CourseLibrary:
                    strSPName = Schema.StandardReportSP.COURSE_LIBRARY;
                    break;
                case Report.ListMethod.CourseAssignment:
                    strSPName = Schema.StandardReportSP.PROC_COURSE_ASSIGNMENT_DASHBOARD;
                    break;
                case Report.ListMethod.GetCourseObjectiveTracking:
                    strSPName = Schema.ContentModuleTracking.PROC_COURSEOBJECTIVETRACKING_USERSPECIFIC;
                    break;
                case Report.ListMethod.GetCourseInteractionTracking:
                    strSPName = Schema.ContentModuleTracking.PROC_COURSEINTERACTIONTRACKING_USERSPECIFIC;
                    break;
                case Report.ListMethod.CourseAssignmentAdminHome:
                    strSPName = Schema.StandardReportSP.PROC_COURSE_ASSIGNMENT_DASHBOARD_ADMIN_HOME;
                    break;
                case Report.ListMethod.CertificationAssignment:
                    strSPName = Schema.StandardReportSP.PROC_CERTIFICATION_ASSIGNMENT_DASHBOARD;
                    break;
                case Report.ListMethod.CertificationAssignmentAdminHome:
                    strSPName = Schema.StandardReportSP.PROC_CERTIFICATION_ASSIGNMENT_DASHBOARD_ADMIN_HOME;
                    break;
                case Report.ListMethod.CurriculumAssignment:
                    strSPName = Schema.StandardReportSP.PROC_CURRICULUM_ASSIGNMENT_DASHBOARD;
                    break;
                case Report.ListMethod.CurriculumAssignmentAdminHome:
                    strSPName = Schema.StandardReportSP.PROC_CURRICULUM_ASSIGNMENT_DASHBOARD_ADMIN_HOME;
                    break;
                case Report.ListMethod.Statistics:
                    strSPName = Schema.StandardReportSP.PROC_DASHBOARD_STATISTICS;
                    break;
                case Report.ListMethod.SystemErrorLog:
                    strSPName = Schema.StandardReportSP.SYSTEM_ERROR_LOG;
                    break;
                case Report.ListMethod.CourseLicensingByClientEnrollmentConsumed:
                    strSPName = Schema.StandardReportSP.COURSE_LICENSING_BY_CLIENT_ENROLLMENT_CONSUMED;
                    break;
                case Report.ListMethod.CourseLicensingByClientEnrollmentBasis:
                    strSPName = Schema.StandardReportSP.COURSE_LICENSING_BY_CLIENT_ENROLLMENT_BASIS;
                    break;
                case Report.ListMethod.CourseLicensingByClientSubscriptionBasis:
                    strSPName = Schema.StandardReportSP.COURSE_LICENSING_BY_CLIENT_SUBSCRIPTION_BASIS;
                    break;
                case Report.ListMethod.CourseLicensingByCourse:
                    strSPName = Schema.StandardReportSP.COURSE_LICENSING_BY_COURSE;
                    break;
                case Report.ListMethod.DelinquencyHistory:
                    strSPName = Schema.StandardReportSP.DELINQUENCY_HISTORY;
                    break;
                case Report.ListMethod.LearnerDump:
                    strSPName = Schema.StandardReportSP.LEARNER_DUMP;
                    break;
                case Report.ListMethod.LearnerListByOrgGroup:
                    strSPName = Schema.StandardReportSP.LEARNER_LIST_BY_ORG_GROUP;
                    break;
                case Report.ListMethod.ActivityCompletionProgress:
                    strSPName = Schema.StandardReportSP.ACTIVITY_COMPLETION_PROGRESS;
                    break;
                case Report.ListMethod.AssignedUsersByActivityStatus:
                    strSPName = Schema.StandardReportSP.ASSIGNED_USERS_BY_ACTIVITY_STATUS_NEW;
                    break;
                case Report.ListMethod.CurriculumDetailsReport:
                    strSPName = Schema.StandardReportSP.CURRICULUM_DETAILS_REPORT;
                    break;
                case Report.ListMethod.ModuleDetailsReport:
                    strSPName = Schema.StandardReportSP.MODULE_REPORT;
                    break;
                case Report.ListMethod.TrainingAttendanceReport:
                    strSPName = Schema.StandardReportSP.MODULE_TRAININGATTENDANCEREPORT;
                    break;
                case Report.ListMethod.UserWiseILTReport:
                    strSPName = Schema.StandardReportSP.PROC_GET_USERWISEILTREPORT;
                    break;

                case Report.ListMethod.CourseAssessmentInteractionTrackingReport:
                    strSPName = Schema.StandardReportSP.COURSE_ASSESSMENT_INTERACTION_TRACKING;
                    break;
                case Report.ListMethod.AssignedUsersByActivityStatusAll:
                    strSPName = Schema.StandardReportSP.ASSIGNED_USERS_BY_ACTIVITY_STATUS_ALL;
                    break;
                case Report.ListMethod.UsersByRole:
                    strSPName = Schema.StandardReportSP.USERS_BY_ROLE;
                    break;
                case Report.ListMethod.NotAssignedUsersByActivity:
                    strSPName = Schema.StandardReportSP.NOT_ASSIGNED_USERS_BY_ACTIVITY;
                    break;
                case Report.ListMethod.AggregateResultsByQuestion:
                    strSPName = Schema.StandardReportSP.AGGREGATE_RESULTS_BY_QUESTION;
                    break;
                case Report.ListMethod.NonPreferredAnswersForCertification:
                    strSPName = Schema.StandardReportSP.NON_PREFERRED_ANSWERS_FOR_CERTIFICATION;
                    break;
                case Report.ListMethod.GetUserAssignmentsForMark:
                    strSPName = Schema.StandardReportSP.PROC_USER_ASSIGNMENTS_FOR_MARK;
                    break;
                case Report.ListMethod.GetUserAssignmentsForMarkCalendar:
                    strSPName = Schema.StandardReportSP.PROC_USER_ASSIGNMENTS_FOR_MARK_CALENDAR;
                    break;
                case Report.ListMethod.UserNonpreferredAnswersForCertification:
                    strSPName = Schema.StandardReportSP.USER_NON_PREFERRED_ANSWERS_FOR_CERTIFICATION;
                    break;
                case Report.ListMethod.DetailedCertificationResultsbyUser:
                    strSPName = Schema.StandardReportSP.DETAILED_CERTIFICATION_RESULTS_BY_USER;
                    break;
                case Report.ListMethod.DetailedCertificationUserResponses:
                    strSPName = Schema.StandardReportSP.DETAILED_CERTIFICATION_USER_RESPONSES;
                    break;
                case Report.ListMethod.GetUserCertificationResponses:
                    strSPName = Schema.StandardReportSP.PROC_GET_USER_CERTIFICATION_RESPONSES;
                    break;
                case Report.ListMethod.ClientSummaryDashboard:
                    strSPName = Schema.StandardReportSP.PROC_GET_CLIENT_SUMMARY_DASHBOARD;
                    break;
                case Report.ListMethod.ClientSummaryDashboardAdminHome:
                    strSPName = Schema.StandardReportSP.PROC_GET_CLIENT_SUMMARY_DASHBOARD_ADMIN_HOME;
                    break;
                case Report.ListMethod.ActivityCompletionProgressChart:
                    strSPName = Schema.StandardReportSP.PROC_ACTIVITY_COMPLETION_CHART;
                    break;
                case Report.ListMethod.ActivityCompletionProgressChartMultiple:
                    strSPName = Schema.StandardReportSP.PROC_ACTIVITY_COMPLETION_CHART_MULTIPLE;
                    break;
                case Report.ListMethod.RunReportingTool:
                    strSPName = Schema.StandardReportSP.PROC_REPORTING_TOOL_RUN;
                    break;
                case Report.ListMethod.RunReportingToolWithWhere:
                    strSPName = Schema.StandardReportSP.PROC_REPORTING_TOOL_RUN_WITH_WHERE;
                    break;
                case Report.ListMethod.RunReportingToolGroupBy:
                    strSPName = Schema.StandardReportSP.PROC_REPORTING_TOOL_GROUP_BY;
                    break;
                case Report.ListMethod.RunReportingToolGroupDtls:
                    strSPName = Schema.StandardReportSP.PROC_REPORTING_TOOL_GROUP_DTLS;
                    break;
                case Report.ListMethod.GetQuestionnaireResponse:
                    strSPName = Schema.StandardReportSP.PROC_QUESTIONNAIRE_RESPONSE;
                    break;
                case Report.ListMethod.GetCertQuestionnaireResponse:
                    strSPName = Schema.StandardReportSP.PROC_CERTIFICATION_QUEST_RESPONSE;
                    break;
                case Report.ListMethod.GetAssessmentResponse:
                    strSPName = Schema.StandardReportSP.PROC_ASSESSMENT_RESPONSE;
                    break;
                case Report.ListMethod.GetAssignmentsForCompleteByUser:
                    strSPName = Schema.StandardReportSP.PROC_MARKCOMPLETE_BY_USER;
                    break;
                case Report.ListMethod.LearnerUserActivityAssignmentNotCompletedDashboard:
                    strSPName = Schema.StandardReportSP.PROC_USER_ACTIVITY_ASSIGNMENT_NOTCOMPLETED_DASHBOARD;
                    break;
                case Report.ListMethod.LearnerUserActivityAssignmentNotCompletedMainPage:
                    strSPName = Schema.StandardReportSP.PROC_USER_ACTIVITY_ASSIGNMENT_NOTCOMPLETED_MAINPAGE;
                    break;
                //Added for certified courses list
                case Report.ListMethod.LearnerUserCertifiedTrainingNotCompletedDashboard:
                    strSPName = Schema.StandardReportSP.PROC_USER_CERTIFIED_TRAINING_COURSES_DASHBOARD;
                    break;
                case Report.ListMethod.LearnerUserCertifiedTrainingNotCompletedMainPage:
                    strSPName = Schema.StandardReportSP.PROC_USER_CERTIFIED_TRAINING_COURSES_MAINPAGE;
                    break;
                case Report.ListMethod.LearnerUserActivityAssignmentCompletedDashboard:
                    strSPName = Schema.StandardReportSP.PROC_USER_ACTIVITY_ASSIGNMENT_COMPLETED_DASHBOARD;
                    break;
                case Report.ListMethod.LearnerUserActivityAssignmentCompletedMainPage:
                    strSPName = Schema.StandardReportSP.PROC_USER_ACTIVITY_ASSIGNMENT_COMPLETED_MAINPAGE;
                    break;
                case Report.ListMethod.LearnerCurriculumActivityLstAll:
                    strSPName = Schema.StandardReportSP.PROC_CURRICULUM_ACTIVITY_LSTALL_LEARNER;
                    break;
                case Report.ListMethod.LearnerCertificationActivityLstAll:
                    strSPName = Schema.StandardReportSP.PROC_CERTIFICATION_ACTIVITY_LSTALL_LEARNER;
                    break;
                case Report.ListMethod.GetAllLookupValues:
                    strSPName = Schema.StandardReportSP.PROC_LOOKUP_VALUE_LSTALL;
                    break;
                case Report.ListMethod.GetAllFreeFormText:
                    strSPName = Schema.StandardReportSP.PROC_GET_ALL_FREE_FORM_TEXT;
                    break;
                case Report.ListMethod.GetCertificationResponseForPDF:
                    strSPName = Schema.StandardReportSP.DETAILED_CERTIFICATION_USER_RESPONSES_FOR_PDF;
                    break;
                case Report.ListMethod.GetCertQuestionnaireResponseType:
                    strSPName = Schema.StandardReportSP.DETAILED_CERTIFICATION_GET_QUESTIONNAIRE_RESPONSE_TYPE;
                    break;
                case Report.ListMethod.AssessmentIndividualResult:
                    strSPName = Schema.StandardReportSP.PROC_ASSESSMENT_INDIVIDUAL_RESULT;
                    break;
                case Report.ListMethod.AssessmentIndividualResultByQuestion:
                    strSPName = Schema.StandardReportSP.PROC_ASSESSMENT_INDIVIDUAL_RESULT_BY_QUESTION;
                    break;
                case Report.ListMethod.AssessmentAggregrateResult:
                    strSPName = Schema.StandardReportSP.PROC_AGGREGATE_RESULT_BY_ASSESSMENT;
                    break;
                case Report.ListMethod.AssessmentUsers:
                    strSPName = Schema.StandardReportSP.PROC_AGGREGATE_RESULT_BY_ASSESSMENT_USERS;
                    break;
                case Report.ListMethod.AssessmentCompletion:
                    strSPName = Schema.StandardReportSP.PROC_ASSESSMENT_COMPLETION;
                    break;
                case Report.ListMethod.AssessmentInProgress:
                    strSPName = Schema.StandardReportSP.PROC_ASSESSMENT_INPROGRESS;
                    break;
                case Report.ListMethod.ClassroomTrainingSession:
                    strSPName = Schema.StandardReportSP.PROC_GET_ALL_SESSIONMASTER;
                    break;
                case Report.ListMethod.ClassroomTrainingNominationAttendeeBySession:
                    strSPName = Schema.StandardReportSP.PROC_GET_ALL_USERSESSIONREGISTRATION;
                    break;
                case Report.ListMethod.OrderHistoryReport:
                    strSPName = Schema.StandardReportSP.PROC_GET_ALL_ORDERHISTORY;
                    break;
                case Report.ListMethod.CourseAssessmentReport:
                    strSPName = Schema.StandardReportSP.PROC_GET_ALL_COURSE_ASSESMENT_RESULT;
                    break;
                case Report.ListMethod.QuestionnaireIndividualResult:
                    strSPName = Schema.StandardReportSP.PROC_QUESTIONNAIRE_INDIVIDUAL_RESULT;
                    break;
                case Report.ListMethod.QuestionnaireCompletion:
                    strSPName = Schema.StandardReportSP.PROC_QUESTIONNAIRE_COMPLETION;
                    break;
                case Report.ListMethod.QuestionnaireInProgress:
                    strSPName = Schema.StandardReportSP.PROC_QUESTIONNAIRE_INPROGRESS;
                    break;
                case Report.ListMethod.QuestionnaireIndividualResultByQuestion:
                    strSPName = Schema.StandardReportSP.PROC_QUESTIONNAIRE_INDIVIDUAL_RESULT_BY_QUESTION;
                    break;
                case Report.ListMethod.QuestionnaireAggregrateResult:
                    strSPName = Schema.StandardReportSP.PROC_AGGREGATE_RESULT_BY_QUESTIONNAIRE;
                    break;
                case Report.ListMethod.QuestionnaireUsers:
                    strSPName = Schema.StandardReportSP.PROC_AGGREGATE_RESULT_BY_QUESTIONNAIRE_USERS;
                    break;
                case Report.ListMethod.DetailedQuestionnaireResultsbyUser:
                    strSPName = Schema.StandardReportSP.PROC_DETAILED_QUESTIONNAIRE_RESULTS_BY_USER;
                    break;
                case Report.ListMethod.GetQuestionnaireQuestResponse:
                    strSPName = Schema.StandardReportSP.PROC_QUESTIONNAIRE_QUEST_RESPONSE;
                    break;
                case Report.ListMethod.GetQuestionnaireQuestResponseType:
                    strSPName = Schema.StandardReportSP.DETAILED_QUESTIONNAIRE_GET_QUESTION_RESPONSE_TYPE;
                    break;
                case Report.ListMethod.GetQuestionnaireResponseForPDF:
                    strSPName = Schema.StandardReportSP.DETAILED_QUESTIONNAIRE_USER_RESPONSES_FOR_PDF;
                    break;
                case Report.ListMethod.VirtualTrainingStatus:
                    strSPName = Schema.StandardReportSP.PROC_VIRTUALTRAINING_STATUS;
                    break;
                case Report.ListMethod.VirtualTrainingAttendeeResult:
                    strSPName = Schema.StandardReportSP.PROC_VirtualTrainingAttendeeResult;
                    break;
                case Report.ListMethod.GetMOHRsult:
                    strSPName = Schema.ContentModuleTracking.PROC_USER_CONTENTMODULETRACKING_LSTALL;
                    break;

                //below methods added for admin home dashboard usercontrols  ...22/02/2016
                case Report.ListMethod.RecentlyAccessedActivityCompletionGraph:
                    strSPName = Schema.StandardReportSP.PROC_RECENTLYACCESSEDACTIVITY_COMPLETIONGRAPH;
                    break;

                case Report.ListMethod.ActivityCompletionGraphDashboard:
                    strSPName = Schema.StandardReportSP.PROC_ACTIVITY_COMPLETIONGRAPH_DASHBOARD;
                    break;

                case Report.ListMethod.MostpurchasedCatalogues:
                    strSPName = Schema.StandardReportSP.PROC_MOSTPURCHASED_CATALOGUES;
                    break;

                case Report.ListMethod.GroupwiseAssignmentsCompletionGraph:
                    strSPName = Schema.StandardReportSP.PROC_GROUPWISEASSIGNMENTS_COMPLETIONGRAPH;
                    break;
                case Report.ListMethod.GetNewsLetterSubscriptionData:
                    strSPName = Schema.StandardReportSP.PROC_GETUSERS_SUBSCRIBE_FORNEWSLETTER;
                    break;
                case Report.ListMethod.GetAbandonedCartReport:
                    strSPName = Schema.StandardReportSP.PROC_ABANDONED_CART_REPORT;
                    break;
                case Report.ListMethod.EventDetailReport:
                    strSPName = Schema.StandardReportSP.PROC_EVENT_DETAIL_REPORT;
                    break;
                case Report.ListMethod.ILTSessionDetailsReport:
                    strSPName = Schema.StandardReportSP.PROC_SESSION_DETAIL_REPORT;
                    break;
                default:
                case Report.ListMethod.GetProductDetailReport:
                    strSPName = Schema.StandardReportSP.PROC_PRODUCT_DETAIL_REPORT;
                    break;
            }
            if (!string.IsNullOrEmpty(strSPName))
                _dsReturn = GetData(pEntReport, strSPName);
            return _dsReturn;
        }

        /// <summary>
        /// Get Data
        /// </summary>
        /// <param name="pEntReport"></param>
        /// <returns>DataSet</returns>
        private DataSet GetData(Report pEntReport, string strSPName)
        {
            DataSet dsReturn = null;
            _sqlcmd = new SqlCommand(strSPName);
            _sqlcmd.CommandTimeout = 0;
            try
            {
                if (!string.IsNullOrEmpty(pEntReport.ClientId))
                    _strConnString = SQLHelper.GetClientDBConnString(pEntReport.ClientId);
                else
                    _strConnString = SQLHelper.GetMasterDBConnString();

                foreach (ReportParameter param in pEntReport.Parameters)
                {
                    _sqlcmd.Parameters.Add(SQLHelper.ConvertToSQLPara(param));
                }
                if (pEntReport.ListRange != null)
                {
                    if (pEntReport.ListRange.PageIndex > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntReport.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntReport.ListRange.PageSize > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntReport.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntReport.ListRange.SortExpression))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntReport.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntReport.ListRange.KeyWord))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntReport.ListRange.KeyWord);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, null);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntReport.AdditionalReportParameterGroupList != null && pEntReport.AdditionalReportParameterGroupList.Count > 0)
                {
                    sb = new StringBuilder();
                    bool bFlagAdd = false;
                    for (int temp = 0; temp < pEntReport.AdditionalReportParameterGroupList.Count; temp++)
                    {
                        if (pEntReport.AdditionalReportParameterGroupList[temp].ParameterFieldType.ToString().Equals(ImportDefination.ValueType.Date.ToString()))
                        {
                            #region Date
                            //From Date
                            if (!string.IsNullOrEmpty(pEntReport.AdditionalReportParameterGroupList[temp].LeftConditionValue))
                            {
                                if (bFlagAdd == false)
                                {
                                    sb.Append(" where ");
                                    bFlagAdd = true;
                                }
                                else
                                {
                                    sb.Append(" and ");
                                }
                                sb.Append(pEntReport.AdditionalReportParameterGroupList[temp].LeftConditionId + " >= '");
                                sb.Append(pEntReport.AdditionalReportParameterGroupList[temp].LeftConditionValue.Trim().Replace("'", "''"));
                                sb.Append("' ");
                            }
                            //To Date
                            if (!string.IsNullOrEmpty(pEntReport.AdditionalReportParameterGroupList[temp].RightConditionValue))
                            {

                                if (bFlagAdd == false)
                                {
                                    sb.Append(" where ");
                                    bFlagAdd = true;
                                }
                                else
                                {
                                    sb.Append(" and ");
                                }
                                sb.Append(pEntReport.AdditionalReportParameterGroupList[temp].LeftConditionId + " <= '");
                                sb.Append(pEntReport.AdditionalReportParameterGroupList[temp].RightConditionValue.Trim().Replace("'", "''"));
                                sb.Append("' ");
                            }
                            #endregion
                        }
                        else if (pEntReport.AdditionalReportParameterGroupList[temp].ParameterFieldType.ToString().Equals(ImportDefination.ValueType.Yes_No.ToString()))
                        {
                            #region System.Boolean
                            //Yes_No
                            if (!string.IsNullOrEmpty(pEntReport.AdditionalReportParameterGroupList[temp].LeftConditionValue))
                            {
                                if (bFlagAdd == false)
                                {
                                    sb.Append(" where ");
                                    bFlagAdd = true;
                                }
                                else
                                {
                                    sb.Append(" and ");
                                }
                                sb.Append(pEntReport.AdditionalReportParameterGroupList[temp].LeftConditionId + " = ");
                                sb.Append(pEntReport.AdditionalReportParameterGroupList[temp].LeftConditionValue.Trim().Replace("'", "''"));
                                sb.Append(" ");
                            }
                            #endregion
                        }
                        else if (pEntReport.AdditionalReportParameterGroupList[temp].ParameterFieldType.ToString().Equals(ImportDefination.ValueType.Alphanumeric.ToString()))
                        {
                            #region Text Only
                            //Alphanumeric
                            if (bFlagAdd == false)
                            {
                                sb.Append(" where ");
                                bFlagAdd = true;
                            }
                            else
                            {
                                sb.Append(" and ");
                            }
                            sb.Append(pEntReport.AdditionalReportParameterGroupList[temp].LeftConditionId + " like '%");
                            sb.Append(pEntReport.AdditionalReportParameterGroupList[temp].LeftConditionValue.Trim().Replace("'", "''"));
                            sb.Append("%' ");
                            #endregion
                        }
                    }

                    _sqlpara = new SqlParameter(Schema.Common.PARA_WHERE_CLAUSE, sb.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else if (!string.IsNullOrEmpty(pEntReport.WhereClause))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_WHERE_CLAUSE, pEntReport.WhereClause);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                dsReturn = SQLHelper.SqlDataAdapter(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                dsReturn = null;
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            return dsReturn;
        }
    }
}
