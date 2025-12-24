using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using static YPLMS2._0.API.DataAccessManager.ContentModuleTrackingAdaptor;

namespace YPLMS2._0.API.DataAccessManager
{ 
    public interface IContentModuleTrackingAdaptor<T> 
    {
        ContentModuleTracking GetContentModuleTrackingByID(ContentModuleTracking pEntContModTracking);
        ContentModuleTracking GetContentModuleTrackingStatusById_Learner(ContentModuleTracking pEntContModTracking);
        ContentModuleTracking GetContentModuleTrackingAfterUpdate(ContentModuleTracking pEntContModTracking);
        ContentModuleTrackingUpdateResult AddContentModuleTracking(ContentModuleTracking pEntContModTracking);
        ContentModuleTrackingUpdateResult EditContentModuleTracking(ContentModuleTracking pEntContModTracking);
        List<ContentModuleTracking> BulkUpdate(List<ContentModuleTracking> pEntListTracking, bool pIsBulkMarkCompleted);
        ContentModuleTracking UpdateAssessmentCourse(ContentModuleTracking pEntContentModuleTracking);
        ContentModuleTracking UpdateScannedFileName(ContentModuleTracking pEntContentModuleTracking);
    }
}
