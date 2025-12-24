using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.Tracking
{
    public interface IContentModuleTrackingRepository
    {
        ContentModuleTracking GetContentModuleTracking(ContentModuleTracking trackingParameters);
        ContentModuleTracking GetContentModuleLessonTracking(ContentModuleTracking trackingParameters);
        ContentModuleTracking GetContentModuleLessonTracking2004(ContentModuleTracking trackingParameters);
        ContentModuleTracking UpdateContentModuleTracking(ContentModuleTracking trackingParameters);
        ContentModuleTracking UpdateContentModuleTracking2004(ContentModuleTracking trackingParameters);
    }
}
