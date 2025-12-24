using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Assignment;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Content;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.Tracking
{

    public class ContentModuleSessionManager : IContentModuleSessionRepository
    {
        private readonly IContentModuleRetriever _contentModuleRetriever;
        private readonly IContentModuleTrackingRepository _contentModuleTrackingRepository;

        public ContentModuleSessionManager(IContentModuleRetriever contentModuleRetriever, IContentModuleTrackingRepository contentModuleTrackingRepository)
        {
            _contentModuleRetriever = contentModuleRetriever;
            _contentModuleTrackingRepository = contentModuleTrackingRepository;
        }

        public ContentModuleSession Save(string clientId, ContentModuleSession session)
        {
            var contentModuleSessionDAM = new ContentModuleSessionDAM();
            return contentModuleSessionDAM.Save(clientId, session);
        }

        public ContentModuleSession GetByIdForCourseLaunch(string clientId, string sessionId)
        {
            ContentModuleSessionDAM contentModuleSessionDAM = new ContentModuleSessionDAM();
            var session = contentModuleSessionDAM.GetById(clientId, sessionId);
            session.Learner = GetLearner(clientId, session);
            session.Assignment = GetAssignment(clientId, session);
            session.ContentModule = GetContentModule(clientId, session);
            session.ContentModuleTracking = GetTracking(clientId, session);
            return session;
        }
        public ContentModuleSession GetByIdForCourseLaunchForScrom2004(string clientId, string sessionId)
        {
            ContentModuleSessionDAM contentModuleSessionDAM = new ContentModuleSessionDAM();
            var session = contentModuleSessionDAM.GetById(clientId, sessionId);
            session.Learner = GetLearner(clientId, session);
            session.Assignment = GetAssignment(clientId, session);
            session.ContentModule = GetContentModuleForScrom2004(clientId, session);
            session.ContentModuleTracking = GetTracking(clientId, session);
            return session;
        }
        private ContentModule GetContentModuleForScrom2004(string clientId, ContentModuleSession session)
        {
            return _contentModuleRetriever.GetContentModuleForScrom2004(clientId, session.ContentModuleId);
        }
        private ContentModule GetContentModule(string clientId, ContentModuleSession session)
        {
            return _contentModuleRetriever.GetContentModule(clientId, session.ContentModuleId);
        }
        private ActivityAssignment GetAssignment(string clientId, ContentModuleSession session)
        {
            var assignment = new ActivityAssignment { ID = session.ContentModuleId, UserID = session.SystemUserGuid, ClientId = clientId };
            return new ActivityAssignmentManager().Execute(assignment, ActivityAssignment.Method.CheckAssignment_CoursePlayer);
        }

        private Learner GetLearner(string clientId, ContentModuleSession session)
        {
            var currentUser = new Learner { ID = session.SystemUserGuid, ClientId = clientId };
            return new LearnerManager().Execute(currentUser, Learner.Method.GetUser_CoursePlayer);
        }


        private ContentModuleTracking GetTracking(string clientId, ContentModuleSession session)
        {
            var contModTracking = new ContentModuleTracking
            {
                UserID = session.SystemUserGuid,
                ContentModuleId = session.ContentModuleId,
                ClientId = clientId
            };
            if (session.Attempt.HasValue)
            {
                contModTracking.ID = session.Assignment.ID + "-" + session.SystemUserGuid + "-" + session.Attempt.Value;
            }
            contModTracking.IsResume = session.IsReview;
            contModTracking.ContentType = session.ContentModule.ContentModuleTypeId;
            if (contModTracking.ContentType == ActivityContentType.Scorm2004.ToString())
            {
                contModTracking = _contentModuleTrackingRepository.GetContentModuleLessonTracking2004(contModTracking);
            }
            else
            {
                contModTracking = _contentModuleTrackingRepository.GetContentModuleLessonTracking(contModTracking);
            }
            contModTracking.IsForAdminPreview = (session.LaunchSite == LaunchSite.Admin);
            contModTracking.UserID = session.SystemUserGuid;
            contModTracking.UserFirstLastName = session.Learner.LastName + ", " + session.Learner.FirstName;
            contModTracking.ContentModuleId = session.ContentModuleId;
            contModTracking.SessionId = session.SessionId;
            contModTracking.TotalNoOfPages = session.ContentModule.TotalLessons;
            contModTracking.ContentType = session.ContentModule.ContentModuleTypeId;

            return contModTracking;
        }
    }
}
