using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IContentModuleSessionDAM<T>
    {
        ContentModuleSession Save(string clientId, ContentModuleSession session);
        ContentModuleSession SaveSession( ContentModuleSession session);
        ContentModuleSession GetById(string clientId, string sessionId);
    }
}
