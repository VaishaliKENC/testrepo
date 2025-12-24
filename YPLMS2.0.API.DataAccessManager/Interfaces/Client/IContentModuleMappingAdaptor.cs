using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IContentModuleMappingAdaptor<T>
    {
        ContentModuleMapping GetContentModuleMappingByID(ContentModuleMapping pEntContentModuleMapping);
        List<ContentModuleMapping> GetAllContentModuleMapping(ContentModuleMapping pEntContentModuleMapping);
        ContentModuleMapping UpdateContentModuleMapping(ContentModuleMapping pEntContentModuleMapping, ContentModuleMapping.Method pMethod);
        ContentModuleMapping DeleteContentModuleMapping(ContentModuleMapping pEntContentModuleMapping);
        List<ContentModuleMapping> BulkDelete(List<ContentModuleMapping> pEntListContentModuleMapping);
        List<ContentModuleMapping> BulkActivateDeactivate(List<ContentModuleMapping> pEntListContentModuleMapping);
    }
}
