using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IImportHistoryAdaptor<T>
    {
        ImportHistory GetImportHistory(ImportHistory pEntImportHistory);
        List<ImportHistory> FindImportHistory(Search pEntSearch);
        List<ImportHistory> FindAssignmentImportHistory(Search pEntSearch);
        List<ImportHistory> FindQuestionsImportHistory(Search pEntSearch);
        ImportHistory AddImportHistory(ImportHistory pEntImportHistory);
        ImportHistory AddImportHistoryWithFile(ImportHistory pEntImportHistory);
        ImportHistory UpdateDetailsWithFile(ImportHistory pEntImportHistory);
        ImportHistory EditImportHistory(ImportHistory pEntImportHistory);
        ImportHistory UpdateLogFileName(ImportHistory pEntImportHistory);
        List<ImportHistory> DeleteSelectedImportHistory(List<ImportHistory> pEntListImportHistory);
        ImportHistory UpdateDetails(ImportHistory pEntImportHistory);
    }
}
