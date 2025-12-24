using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.Content
{
    public interface IContentModuleRetriever
    {
        ContentModule GetContentModule(string clientId, string courseId);
        ContentModule GetContentModuleForScrom2004(string clientId, string courseId);
        ContentModule GetMetaData(string clientId, string courseId);
    }
}
