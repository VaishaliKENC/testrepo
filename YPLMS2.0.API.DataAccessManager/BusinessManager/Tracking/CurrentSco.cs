using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Content;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.Tracking
{
    public class CurrentSco
    {
         private readonly IContentModuleRetriever _contentModuleRetriever;
        private readonly IContentModuleTrackingRepository _contentModuleTrackingRepository;

       // IContentModuleSessionRepository sessionRepository;
        ICourseConfigurationAdaptor<CourseConfiguration> configurationRepository = new CourseConfigurationAdaptor();

        // Manually instantiate all dependencies
        


        public IContentModuleTrackingManager TrackingManager = new ContentModuleTracker();


        #region SaveCurrentSco

        public void SaveCurrentSco(XmlDocument requestXml, LessonTracking pEntContentModuleTracking)
        {
            TrackingSessionMetaData trackingMetaData = GetTrackingMetaData();
            //Since only SCORM clients will call this, initializing the serializer is ok
            var lesson = new ScoTrackingSerializer().ParseLesson(requestXml.SelectSingleNode("//sco"));
            string strContentModuleId = string.Empty;
            string strClientId = string.Empty;

            trackingMetaData.CourseId = pEntContentModuleTracking.CourseId;

            //if (LMSSession.IsInSession(Client.CLIENT_SESSION_ID))
            strClientId = Convert.ToString(pEntContentModuleTracking.ClientId);

            if (!string.IsNullOrEmpty(strClientId))
            {
                ContentModuleTracking objTrack = null;
                //    if (IsUpdateTracking)
                trackingMetaData.ClientId = pEntContentModuleTracking.ClientId;
                trackingMetaData.SessionId= pEntContentModuleTracking.SessionId;
                  TrackingManager.SaveTracking(lesson, trackingMetaData);

            }

        }
        #endregion
        #region GetTrackingMetaData

        private TrackingSessionMetaData GetTrackingMetaData()
        {
            var metaData = new TrackingSessionMetaData
            {
                ContentType = ActivityContentType.Scorm12.ToString()
            };
            return metaData;
        }
        #endregion

    }
}
