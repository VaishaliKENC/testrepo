using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IActivityAssignmentAdaptor<T>
    {
        ActivityAssignment GetActivityAssignmentByID(ActivityAssignment pEntActivityAssignment);
        ActivityAssignment GetForCoursePlayer(string clientId, string courseId, string learnerId);
        ActivityAssignmentExt IsAlreadyCourseLaunched(string clientId, string tokenKey);
        ActivityAssignment AddActivityAssignment(ActivityAssignment assignmentToSave);
        ActivityAssignment GetUsersActivityAssignmentByID(ActivityAssignment pEntActivityAssignment);
        ActivityAssignment GetUserActivity_ForBulkImport(ActivityAssignment pEntActivityAssignment);
        ActivityAssignment GetCoursesCount(ActivityAssignment pEntActivityAssignment);
        ActivityAssignment GetActivityAssignmentByID_Learner_Print(ActivityAssignment pEntActivityAssignment);
        ActivityAssignment GetActivityAssignmentByID_Learner_Print_ALL(ActivityAssignment pEntActivityAssignment);
        ActivityAssignment CheckUserAssignmentByID(ActivityAssignment pEntActivityAssignment);
        ActivityAssignment CheckUserAssignmentByID_CoursePlayer(ActivityAssignment pEntActivityAssignment);
        ActivityAssignment CheckUserAssignmentByIDOptimized(ActivityAssignment pEntActivityAssignment);
        ActivityAssignment EditActivityAssignment(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> GetUserActivityAssignmentList(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> GetUserActivityAssignmentListFoDL(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> GetUserAssignmentListForEmailTemplate(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> GetUserAssignmentListForEdit(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> GetUserAssignmentListForUnlock(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> GetUserAssignmentsForAttemptUnlock(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> FindActivityListByName(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> FindActivityListByNameOptimized(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> FindActivityListForDelequencyHistory(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> FindActivityListForInteractionTracking(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> FindActivityOneTimeAssignment(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> GetAllCertificationPrograms_ForApproval_FromLearner(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> GetAllCertificationPrograms_ForApproval(ActivityAssignment pEntActivityAssignment);
        ActivityAssignment UpdateCertificationPrograms_ForApproval(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> FindActivityOneTimeAssignment_ForCurriculum(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> GetAllActivityByCategoryMapping(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> GetAllActivityByUnifiedMappingMapping(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> GetAllActivityByActivityCertificateMapping(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> GetAllCategory(ActivityAssignment pEntActivityAssignment);
        List<ActivityAssignment> GetProductSubCategoryByID(ActivityAssignment pEntActivityAssignment);
        List<Learner> SaveSelectedLearner(List<Learner> pEntListLearner);
        List<ActivityAssignment> SaveSelectedActivity(List<ActivityAssignment> pEntListActivityAssignment);
        List<Learner> DeleteSelectedLearner(List<Learner> pEntListLearner);
        List<ActivityAssignment> DeleteSelectedActivity(List<ActivityAssignment> pEntListActivityAssignment);
        List<ActivityAssignment> GetActivityByRequestedID(ActivityAssignment pEntActivityAssignment);
    }
}
