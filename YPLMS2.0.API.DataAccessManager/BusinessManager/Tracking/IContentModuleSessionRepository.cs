using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.Tracking
{
    public interface IContentModuleSessionRepository
    {
        ContentModuleSession GetByIdForCourseLaunch(string clientId, string sessionId);
        ContentModuleSession GetByIdForCourseLaunchForScrom2004(string clientId, string sessionId);
        ContentModuleSession Save(string clientId, ContentModuleSession session);
    }
}
