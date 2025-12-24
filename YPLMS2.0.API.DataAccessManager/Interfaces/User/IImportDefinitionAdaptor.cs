using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IImportDefinitionAdaptor<T>
    {
        ImportDefination GetImportDefinationById(ImportDefination pEntImportDefination);
        ImportDefination EditImportDefination(ImportDefination pEntImportDefinition);
        List<ImportDefination> GetImportDefinationList(ImportDefination pEntImportDefination);
    }
}
