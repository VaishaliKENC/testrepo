using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Content;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.Assignment
{
    public interface ICoursePlayerAssignmentManager
    {
        ActivityAssignment GetAssignmentForLaunch(string clientId, string courseId, string learnerId,
                                                  LaunchSite courseLaunchSite);
        ActivityAssignmentExt IsAlreadyCourseLaunchedMgr(string clientId, string tokenKey);

        string LaunchActivity(ActivityAssignment activityAssignment, LaunchSite type);
    }
    public class CoursePlayerAssignmentManager : ICoursePlayerAssignmentManager
    {
        ActivityAssignmentAdaptor _courseAssignmentRepository = new ActivityAssignmentAdaptor();
        FilesystemContentModuleRetriever _contentModuleRetriever = new FilesystemContentModuleRetriever();

        //public CoursePlayerAssignmentManager( IContentModuleRetriever contentModuleRetriever)
        //{
        //    _courseAssignmentRepository = courseAssignmentRepository;
        //    _contentModuleRetriever = contentModuleRetriever;
        //}

        public ActivityAssignment GetAssignmentForLaunch(string clientId, string courseId, string learnerId, LaunchSite courseLaunchSite)
        {
            var assignment = _courseAssignmentRepository.GetForCoursePlayer(clientId, courseId, learnerId);
            if (String.IsNullOrEmpty(assignment.ID) && courseLaunchSite == LaunchSite.Admin)
            {
                assignment = AddAdminAssignment(clientId, courseId, learnerId);
            }
            return assignment;
        }
        public ActivityAssignmentExt IsAlreadyCourseLaunchedMgr(string clientId, string tokenKey)
        {
            ActivityAssignmentExt oActivityAssignmentExt = new ActivityAssignmentExt();
            oActivityAssignmentExt = _courseAssignmentRepository.IsAlreadyCourseLaunched(clientId, tokenKey);

            return oActivityAssignmentExt;
        }

        private ActivityAssignment AddAdminAssignment(string clientId, string courseId, string learnerId)
        {
            var entContentModule = _contentModuleRetriever.GetMetaData(clientId, courseId);
            var assignment = new ActivityAssignment
            {
                ID = courseId,
                UserID = learnerId,
                ClientId = clientId,
                CreatedById = learnerId,
                DateCreated = DateTime.UtcNow,
                ActivityTypeId = entContentModule.ContentModuleTypeId,
                ActivityName = entContentModule.ContentModuleName,
                IsAdminAssignment = true,
                IsForAdminPreview = true,
                CompletionConditionId = ActivityCompletionCondition.Mandatory,
                LastModifiedById = learnerId
            };
            assignment.ActivityType = (ActivityContentType)Enum.Parse(typeof(ActivityContentType), assignment.ActivityTypeId);

            assignment = _courseAssignmentRepository.AddActivityAssignment(assignment);
            assignment.ClientId = clientId;
            assignment.ActivityName = entContentModule.ContentModuleName;

            return assignment;
        }
        public string LaunchActivity(ActivityAssignment activityAssignment, LaunchSite type)
        {
            LearnerManager mgrLearner = new LearnerManager();
            YPLMS2._0.API.DataAccessManager.BusinessManager.Tracking.ContentModuleTrackingManager mgrContentModuleTracking = new YPLMS2._0.API.DataAccessManager.BusinessManager.Tracking.ContentModuleTrackingManager();
            ContentModuleTracking contModTracking = new ContentModuleTracking();
            string sLaunchPlayer;

            switch (activityAssignment.ActivityType)
            {
                case ActivityContentType.Scorm12:
                    contModTracking.UserID = activityAssignment.SystemUserGuid;
                    contModTracking.ClientId = activityAssignment.ClientId;
                    contModTracking.ContentModuleId = activityAssignment.ID;
                    contModTracking.TotalNoOfPages = 1;
                    contModTracking.NoOfPagesCompleted = 1;
                    contModTracking.DateOfStart = DateTime.Now;
                    //contModTracking.DateOfCompletion = DateTime.Now;
                    contModTracking.IsForAdminPreview = type.ToString().Trim().ToLower() == "admin" ? true : false;
                    contModTracking.CompletionStatus = ActivityCompletionStatus.Started; //"Started";
                    break;
                case ActivityContentType.AICC:
                    //sLaunchPlayer = "LaunchContentAICC.htm?sessionId=" + sessionId + "&clientId=" + Request.Params["client"] + "&LangId=" + langId;
                    break;

                case ActivityContentType.Scorm2004:
                   // sLaunchPlayer = "LaunchContentScorm2004.htm?sessionId=" + sessionId + "&clientId=" + Request.Params["client"] + "&LangId=" + langId;
                    break;

                default:
                    contModTracking.UserID = activityAssignment.SystemUserGuid;
                    contModTracking.ClientId = activityAssignment.ClientId;
                    contModTracking.ContentModuleId = activityAssignment.ID;
                    contModTracking.TotalNoOfPages = 1;
                    contModTracking.NoOfPagesCompleted = 1;
                    contModTracking.DateOfStart = DateTime.Now;
                    contModTracking.DateOfCompletion = DateTime.Now;
                    contModTracking.IsForAdminPreview = type.ToString().Trim().ToLower() == "admin" ? true : false;
                    contModTracking.CompletionStatus = ActivityCompletionStatus.Completed; //"Started";
                    break;
            }
            try
            {
                contModTracking = mgrContentModuleTracking.Execute(contModTracking, ContentModuleTracking.Method.Add);
                return sLaunchPlayer = string.Empty;
            }
            catch
            {
                sLaunchPlayer = YPLMS.Services.Messages.Common.NO_CURR_COURSE_FIND;
                return sLaunchPlayer;
            }

        }
    }
}
