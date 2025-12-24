using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.Tracking
{

    public class ContentModuleTracker : IContentModuleTrackingManager
    {
        private readonly IContentModuleTrackingRepository _dataManager;
        private readonly ICourseTrackingUpdater _trackingUpdater;
        private readonly IContentModuleSessionRepository _sessionRepository;
        private readonly ICourseConfigurationAdaptor<CourseConfiguration> _courseConfigurationRepository;

        public ContentModuleTracker()
        {
        }

        public ContentModuleTracker(IContentModuleTrackingRepository dataManager, ICourseTrackingUpdater trackingUpdater, IContentModuleSessionRepository sessionRepository, ICourseConfigurationAdaptor<CourseConfiguration> configurationRepository)
        {
            _dataManager = dataManager;
            _trackingUpdater = trackingUpdater;
            _sessionRepository = sessionRepository;
            _courseConfigurationRepository = configurationRepository;
        }

        public ContentModuleTracking SaveTracking(LessonTracking tracking, TrackingSessionMetaData metaData)
        {
            ContentModuleTracking trackingToGet = new ContentModuleTracking
            {
                ClientId = metaData.ClientId,
                ContentType = metaData.ContentType,
                SessionId = metaData.SessionId
            };
            ContentModuleTrackingManager obj = new ContentModuleTrackingManager();

            //temp
            //trackingToGet.SessionId = tracking.SessionId;
            //metaData.SessionId = "68B53A8A-CE1E-4903-9CF1-020312270032";

            var currentTracking = obj.GetContentModuleLessonTracking(trackingToGet);

            if (String.IsNullOrEmpty(currentTracking.ID))
            {
                var session = _sessionRepository.GetByIdForCourseLaunch(metaData.ClientId, metaData.SessionId);
                currentTracking = session.ContentModuleTracking;
            }

            currentTracking.SessionId = metaData.SessionId;
            currentTracking.ClientId = metaData.ClientId;
            currentTracking.ContentType = metaData.ContentType;

            if (tracking.RawScore.HasValue)
            {
                ContentModule pEntContModule = new ContentModule();
                pEntContModule.ClientId = metaData.ClientId;
                if (string.IsNullOrEmpty(metaData.CourseId))
                    pEntContModule.ID = currentTracking.ContentModuleId;
                else
                    pEntContModule.ID = metaData.CourseId;
               // pEntContModule.ID = YPLMS.Services.EncryptionManager.Decrypt(metaData.CourseId);

                ContentModuleAdaptor objAdaptor = new ContentModuleAdaptor();
                pEntContModule = objAdaptor.GetContentModuleByID(pEntContModule);
                tracking.MasteryScore = pEntContModule.MasteryScore;

            }
            CourseTrackingUpdater _trackingUpdaterObj = new CourseTrackingUpdater();
            var trackingToSave = _trackingUpdaterObj.UpdateTracking(tracking, currentTracking);

            return obj.UpdateContentModuleTracking(trackingToSave);
        }

        //SCORM 2004 Course tracking

        public ContentModuleTracking SaveTracking(LessonTracking2004 tracking, TrackingSessionMetaData metaData)
        {
            ContentModuleTracking trackingToGet = new ContentModuleTracking
            {
                ClientId = metaData.ClientId,
                ContentType = metaData.ContentType,
                SessionId = metaData.SessionId
            };

            var currentTracking = _dataManager.GetContentModuleLessonTracking2004(trackingToGet);

            if (String.IsNullOrEmpty(currentTracking.ID))
            {
                var session = _sessionRepository.GetByIdForCourseLaunchForScrom2004(metaData.ClientId, metaData.SessionId);
                currentTracking = session.ContentModuleTracking;
            }

            currentTracking.SessionId = metaData.SessionId;
            currentTracking.ClientId = metaData.ClientId;
            currentTracking.ContentType = metaData.ContentType;

            if (tracking.RawScore.HasValue)
            {
                // var clientCourseConfig = _courseConfigurationRepository.GetConfiguration(metaData.ClientId); 
                //// tracking.MasteryScore = clientCourseConfig.MasteryScore; // this line commenetd by sarita bez, mastery score is course specifc now and need to pick from the course table.
                // if (!clientCourseConfig.ScoreTracking)
                // {
                //     tracking.RawScore = null;
                // }

                ContentModule pEntContModule = new ContentModule();
                pEntContModule.ClientId = metaData.ClientId;
                pEntContModule.ID = YPLMS.Services.EncryptionManager.Decrypt(metaData.CourseId);

                ContentModuleAdaptor objAdaptor = new ContentModuleAdaptor();
                pEntContModule = objAdaptor.GetContentModuleByID(pEntContModule);
                tracking.MasteryScore = pEntContModule.MasteryScore;

            }

            var trackingToSave = _trackingUpdater.UpdateTracking(tracking, currentTracking);

            return _dataManager.UpdateContentModuleTracking2004(trackingToSave);
        }
    }
}
