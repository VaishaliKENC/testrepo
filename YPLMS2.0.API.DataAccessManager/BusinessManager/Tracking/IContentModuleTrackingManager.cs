using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Tracking;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager
{
    public interface IContentModuleTrackingManager
    {
        ContentModuleTracking SaveTracking(LessonTracking tracking, TrackingSessionMetaData metaData);
        ContentModuleTracking SaveTracking(LessonTracking2004 tracking, TrackingSessionMetaData metaData);
    }
}
