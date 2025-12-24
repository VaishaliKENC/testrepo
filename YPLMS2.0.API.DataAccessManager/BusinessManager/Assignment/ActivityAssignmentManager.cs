using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.Assignment
{
    /// <summary>
    /// class ActivityAssignmentManager
    /// </summary>
    public class ActivityAssignmentManager : IManager<ActivityAssignment, ActivityAssignment.Method, ActivityAssignment.ListMethod>
    {
        DataSet dataSet = null;
        /// <summary>
        /// Default ActivityAssignmentManager constructor
        /// </summary>
        public ActivityAssignmentManager()
        {
        }

        /// <summary>
        /// To get single or multiple assignment list for requested/all users.
        /// </summary>
        /// <param name="pEntAssignment"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of ActivityAssignment objects</returns>
        public List<ActivityAssignment> Execute(ActivityAssignment pEntAssignment, ActivityAssignment.ListMethod pMethod)
        {
            List<ActivityAssignment> entListAssignmentsReturn = null;
            ActivityAssignmentAdaptor adaptorAssignment = new ActivityAssignmentAdaptor();
            switch (pMethod)
            {
                case ActivityAssignment.ListMethod.GetAssignmentList:
                    entListAssignmentsReturn = adaptorAssignment.GetUserActivityAssignmentList(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.GetUserAssignmentsForEdit:
                    entListAssignmentsReturn = adaptorAssignment.GetUserAssignmentListForEdit(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.GetUserAssignmentsForUnlock:
                    entListAssignmentsReturn = adaptorAssignment.GetUserAssignmentListForUnlock(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.GetUserAssignmentsForAttemptUnlock:
                    entListAssignmentsReturn = adaptorAssignment.GetUserAssignmentsForAttemptUnlock(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.FindActivityListByName:
                    entListAssignmentsReturn = adaptorAssignment.FindActivityListByName(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.FindActivityListByNameOptimized:
                    entListAssignmentsReturn = adaptorAssignment.FindActivityListByNameOptimized(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.FindActivityListForDelequencyHistory:
                    entListAssignmentsReturn = adaptorAssignment.FindActivityListForDelequencyHistory(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.FindActivityListForInteractionTracking:
                    entListAssignmentsReturn = adaptorAssignment.FindActivityListForInteractionTracking(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.FindActivityForAssignment:
                    entListAssignmentsReturn = adaptorAssignment.FindActivityOneTimeAssignment(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.FindActivityForMarkCompletion:
                    entListAssignmentsReturn = adaptorAssignment.GetActivityforMarkCompletion(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.FindActivityForMarkCompletionOptimized:
                    entListAssignmentsReturn = adaptorAssignment.GetActivityforMarkCompletionOptimized(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.GetCurrentAttempts:
                    entListAssignmentsReturn = adaptorAssignment.GetUserActivityAttempts(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.GetCurrentAttemptsForMaxCourseScore:
                    entListAssignmentsReturn = adaptorAssignment.GetUserActivityAttemptsForMaxCourseScore(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.GetUserAssignmentListForEmail:
                    entListAssignmentsReturn = adaptorAssignment.GetUserAssignmentListForEmail(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.GetUserAssignmentListForEmailTemplate:
                    entListAssignmentsReturn = adaptorAssignment.GetUserAssignmentListForEmailTemplate(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.GetInnerActivity:
                    entListAssignmentsReturn = adaptorAssignment.GetInnerActivity(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.GetAllActivityByCategoryMapping:
                    entListAssignmentsReturn = adaptorAssignment.GetAllActivityByCategoryMapping(pEntAssignment);
                    break;
                //case ActivityAssignment.ListMethod.GetAllActivityByUnifiedMappingMapping:
                //    entListAssignmentsReturn = adaptorAssignment.GetAllActivityByUnifiedMappingMapping(pEntAssignment);
                //    break;
                case ActivityAssignment.ListMethod.GetAssignmentListForDL:
                    entListAssignmentsReturn = adaptorAssignment.GetUserActivityAssignmentListFoDL(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.GetAllActivityByActivityCertificateMapping:
                    entListAssignmentsReturn = adaptorAssignment.GetAllActivityByActivityCertificateMapping(pEntAssignment);
                    break;

                case ActivityAssignment.ListMethod.FindActivityForAssignment_ForCurriculum:
                    entListAssignmentsReturn = adaptorAssignment.FindActivityOneTimeAssignment_ForCurriculum(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.GetAllCertificationPrograms:
                    entListAssignmentsReturn = adaptorAssignment.GetAllCertificationPrograms_ForApproval(pEntAssignment);
                    break;
                case ActivityAssignment.ListMethod.GetAllCertificationProgramsFromLearner:
                    entListAssignmentsReturn = adaptorAssignment.GetAllCertificationPrograms_ForApproval_FromLearner(pEntAssignment);
                    break;
                default:
                    break;
            }
            return entListAssignmentsReturn;
        }

        /// <summary>
        /// For Bulk add action.
        /// </summary>
        /// <param name="pEntListAssignment"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of ActivityAssignment objects</returns>
        public List<ActivityAssignment> Execute(List<ActivityAssignment> pEntListAssignment, ActivityAssignment.ListMethod pMethod)
        {
            List<ActivityAssignment> entListAssignmentsReturn = null;
            ActivityAssignmentAdaptor adaptorAssignments = new ActivityAssignmentAdaptor();
            switch (pMethod)
            {
                case ActivityAssignment.ListMethod.AddAll:
                    if (pEntListAssignment.Count > 0)
                    {
                        entListAssignmentsReturn = adaptorAssignments.AddActivityAssignments(pEntListAssignment, false);
                    }
                    break;
                case ActivityAssignment.ListMethod.BulkImportAssignment:
                    if (pEntListAssignment.Count > 0)
                    {
                        entListAssignmentsReturn = adaptorAssignments.BulkImportAssignments(pEntListAssignment);
                    }
                    break;
                case ActivityAssignment.ListMethod.EditSelected:
                    if (pEntListAssignment.Count > 0)
                    {
                        entListAssignmentsReturn = adaptorAssignments.EditAssignments(pEntListAssignment);
                    }
                    break;
                case ActivityAssignment.ListMethod.AddAllReRgister:
                    if (pEntListAssignment.Count > 0)
                    {
                        entListAssignmentsReturn = adaptorAssignments.AddActivityAssignments(pEntListAssignment, true);
                    }
                    break;

                default:
                    break;
            }
            return entListAssignmentsReturn;
        }

        /// <summary>
        /// For Add,Update,Delete,read transactions.
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <param name="pMethod"></param>
        /// <returns>ActivityAssignment object</returns>
        public ActivityAssignment Execute(ActivityAssignment pEntActivityAssignment, ActivityAssignment.Method pMethod)
        {
            ActivityAssignment entActivityAssignmentReturn = null;
            ActivityAssignmentAdaptor adaptorAssignment = new ActivityAssignmentAdaptor();

            switch (pMethod)
            {
                case ActivityAssignment.Method.GetActivityForPrint:
                    entActivityAssignmentReturn = adaptorAssignment.GetActivityAssignmentByID_Learner_Print(pEntActivityAssignment);
                    break;
                case ActivityAssignment.Method.GetActivityForPrint_ALL:
                    entActivityAssignmentReturn = adaptorAssignment.GetActivityAssignmentByID_Learner_Print_ALL(pEntActivityAssignment);
                    break;
                case ActivityAssignment.Method.Get:
                    entActivityAssignmentReturn = adaptorAssignment.GetActivityAssignmentByID(pEntActivityAssignment);
                    break;
                //
                case ActivityAssignment.Method.GetActivity:
                    entActivityAssignmentReturn = adaptorAssignment.GetUsersActivityAssignmentByID(pEntActivityAssignment);
                    break;
                case ActivityAssignment.Method.CheckAssignment_CoursePlayer:
                    entActivityAssignmentReturn = adaptorAssignment.CheckUserAssignmentByID_CoursePlayer(pEntActivityAssignment);
                    break;
                case ActivityAssignment.Method.CheckAssignment:
                    entActivityAssignmentReturn = adaptorAssignment.CheckUserAssignmentByID(pEntActivityAssignment);
                    break;
                case ActivityAssignment.Method.CheckAssignmentOptimized:
                    entActivityAssignmentReturn = adaptorAssignment.CheckUserAssignmentByIDOptimized(pEntActivityAssignment);
                    break;
                case ActivityAssignment.Method.Add:
                    // Code changed for Admin Assignment on 01-Jan-2010 (license consumed issue)
                    pEntActivityAssignment.IsAdminAssignment = true;
                    entActivityAssignmentReturn = adaptorAssignment.AddActivityAssignment(pEntActivityAssignment);
                    break;
                case ActivityAssignment.Method.Update:
                    entActivityAssignmentReturn = adaptorAssignment.EditActivityAssignment(pEntActivityAssignment);
                    break;
                case ActivityAssignment.Method.UnAssignByActivityId:
                    entActivityAssignmentReturn = adaptorAssignment.UnassignByActivity(pEntActivityAssignment);
                    break;
                case ActivityAssignment.Method.GetCoursesCount:
                    entActivityAssignmentReturn = adaptorAssignment.GetCoursesCount(pEntActivityAssignment);
                    break;
                case ActivityAssignment.Method.Get_AssignmentForBulkImport:
                    entActivityAssignmentReturn = adaptorAssignment.GetUserActivity_ForBulkImport(pEntActivityAssignment);
                    break;
                case ActivityAssignment.Method.UpdateCertificationPrograms:
                    entActivityAssignmentReturn = adaptorAssignment.UpdateCertificationPrograms_ForApproval(pEntActivityAssignment);
                    break;
                default:
                    entActivityAssignmentReturn = null;
                    break;
            }
            return entActivityAssignmentReturn;
        }

        /// <summary>
        /// For 
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <param name="pMethod"></param>
        /// <returns>ActivityAssignment object</returns>
        public Object ExecuteScalar(ActivityAssignment pEntActivityAssignment, ActivityAssignment.Method pMethod)
        {
            Object objReturn = null;
            ActivityAssignmentAdaptor adaptorAssignment = new ActivityAssignmentAdaptor();

            switch (pMethod)
            {
                case ActivityAssignment.Method.IsProductLicenseExceed:
                    objReturn = adaptorAssignment.IsProductExceedLicense(pEntActivityAssignment);
                    break;
                default:
                    objReturn = null;
                    break;
            }
            return objReturn;
        }

        /// <summary>
        /// Returns DataSet as per requested method
        /// </summary>
        /// <param name="pEntBase">Entity Onject as BaseEntity</param>
        /// <param name="pMethod">Method type</param>
        /// <returns>Return DataSet filled with data</returns>
        public DataSet ExecuteDataSet(ActivityAssignment pEntActivityAssignment, ActivityAssignment.ListMethod pMethod)
        {
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            List<ActivityAssignment> entListAssignment = Execute(pEntActivityAssignment, pMethod);
            dataSet = dsConverter.ConvertToDataSet<ActivityAssignment>(entListAssignment);
            return dataSet;
        }
        public DataSet ExecuteDataSet1(ActivityAssignment pEntActivityAssignment, ActivityAssignment.ListMethod pMethod)
        {


            DataSet dataSet = null;
            ActivityAssignmentAdaptor adaptorAssignment = new ActivityAssignmentAdaptor();
            switch (pMethod)
            {
                case ActivityAssignment.ListMethod.GetVirtualClassroomNominationDetailsDuringOneTimeAssignment:
                    dataSet = adaptorAssignment.GetVirtualClassroomNominationDetailsDuringOneTimeAssignment(pEntActivityAssignment);
                    break;
                case ActivityAssignment.ListMethod.Delete_VT_CT_AtTimeOf_EditAssignment:
                    dataSet = adaptorAssignment.Delete_VT_CT_AtTimeOf_EditAssignment(pEntActivityAssignment);
                    break;
                case ActivityAssignment.ListMethod.GetVirtualClassroomNominationRegistrationCountOneTimeAssignment:
                    dataSet = adaptorAssignment.GetVirtualClassroomNominationRegistrationCountOneTimeAssignment(pEntActivityAssignment);
                    break;
                default:
                    dataSet = null;
                    break;
            }

            return dataSet;
        }
    }
}
