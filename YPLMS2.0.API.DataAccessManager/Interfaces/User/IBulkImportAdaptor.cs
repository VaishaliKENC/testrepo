using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IBulkImportAdaptor<T>
    {
        BulkImport GetBulkImportMasterByID(BulkImport pEntBulkImportMaster);
        BulkImport AddBulkImportMaster(BulkImport pEntBulkImportMaster);
        BulkImport EditBulkImportMaster(BulkImport pEntBulkImportMaster);
        BulkImport DeleteBulkImportMaster(BulkImport pEntBulkImportMaster);
        List<BulkImport> FindBulkImportTasksSchedular(Search pEntSearch);
    }
}
