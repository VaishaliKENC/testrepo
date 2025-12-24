using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImportHistoryController : ControllerBase
    {
       private readonly IImportHistoryAdaptor<ImportHistory> _importHistoryAdaptor;
        public ImportHistoryController(IImportHistoryAdaptor<ImportHistory> importHistoryAdaptor)
        {
            _importHistoryAdaptor = importHistoryAdaptor;
        }

        [HttpPost]
        [Route("getimporthistory")]
        [Authorize]
        public async Task<IActionResult> GetImportHistory(ImportHistory pEntImportHistory)
        {

            ImportHistory entImportHistory =  new ImportHistory();

            entImportHistory = _importHistoryAdaptor.GetImportHistory(pEntImportHistory);
            if (entImportHistory != null)
            {

                return Ok(new { ImportHistory = entImportHistory });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("findimporthistory")]
        [Authorize]
        public async Task<IActionResult> FindImportHistory(Search pEntSearch)
        {

            List<ImportHistory> entImportHistorylist = new List<ImportHistory>();

            entImportHistorylist = _importHistoryAdaptor.FindImportHistory(pEntSearch);
            if (entImportHistorylist != null)
            {

                return Ok(new { ImportHistoryList = entImportHistorylist });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("findassignmentimporthistory")]
        [Authorize]
        public async Task<IActionResult> FindAssignmentImportHistory(Search pEntSearch)
        {

            List<ImportHistory> entImportHistorylist = new List<ImportHistory>();

            entImportHistorylist = _importHistoryAdaptor.FindAssignmentImportHistory(pEntSearch);
            if (entImportHistorylist != null)
            {

                return Ok(new { ImportHistoryList = entImportHistorylist });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("findquestionsimporthistory")]
        [Authorize]
        public async Task<IActionResult> FindQuestionsImportHistory(Search pEntSearch)
        {

            List<ImportHistory> entImportHistorylist = new List<ImportHistory>();

            entImportHistorylist = _importHistoryAdaptor.FindQuestionsImportHistory(pEntSearch);
            if (entImportHistorylist != null)
            {

                return Ok(new { ImportHistoryList = entImportHistorylist });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("addimporthistory")]
        [Authorize]
        public async Task<IActionResult> AddImportHistory(ImportHistory pEntImportHistory)
        {

            ImportHistory entImportHistory = new ImportHistory();

            entImportHistory = _importHistoryAdaptor.AddImportHistory(pEntImportHistory);
            if (entImportHistory != null)
            {

                return Ok(new { ImportHistoryList = entImportHistory });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("addimporthistorywithfile")]
        [Authorize]
        public async Task<IActionResult> AddImportHistoryWithFile(ImportHistory pEntImportHistory)
        {

            ImportHistory entImportHistory = new ImportHistory();

            entImportHistory = _importHistoryAdaptor.AddImportHistoryWithFile(pEntImportHistory);
            if (entImportHistory != null)
            {

                return Ok(new { ImportHistoryList = entImportHistory });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("updatedetailswithfile")]
        [Authorize]
        public async Task<IActionResult> UpdateDetailsWithFile(ImportHistory pEntImportHistory)
        {

            ImportHistory entImportHistory = new ImportHistory();

            entImportHistory = _importHistoryAdaptor.UpdateDetailsWithFile(pEntImportHistory);
            if (entImportHistory != null)
            {

                return Ok(new { ImportHistoryList = entImportHistory });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("editimporthistory")]
        [Authorize]
        public async Task<IActionResult> EditImportHistory(ImportHistory pEntImportHistory)
        {

            ImportHistory entImportHistory = new ImportHistory();

            entImportHistory = _importHistoryAdaptor.EditImportHistory(pEntImportHistory);
            if (entImportHistory != null)
            {

                return Ok(new { ImportHistoryList = entImportHistory });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("updatelogfilename")]
        [Authorize]
        public async Task<IActionResult> UpdateLogFileName(ImportHistory pEntImportHistory)
        {

            ImportHistory entImportHistory = new ImportHistory();

            entImportHistory = _importHistoryAdaptor.UpdateLogFileName(pEntImportHistory);
            if (entImportHistory != null)
            {

                return Ok(new { ImportHistoryList = entImportHistory });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("updatedetails")]
        [Authorize]
        public async Task<IActionResult> UpdateDetails(ImportHistory pEntImportHistory)
        {

            ImportHistory entImportHistory = new ImportHistory();

            entImportHistory = _importHistoryAdaptor.UpdateDetails(pEntImportHistory);
            if (entImportHistory != null)
            {

                return Ok(new { ImportHistoryList = entImportHistory });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("deleteselectedimporthistory")]
        [Authorize]
        public async Task<IActionResult> DeleteSelectedImportHistory(List<ImportHistory> pEntListImportHistory)
        {

            List<ImportHistory> entImportHistorylist = new List<ImportHistory>();

            entImportHistorylist = _importHistoryAdaptor.DeleteSelectedImportHistory(pEntListImportHistory);
            if (entImportHistorylist != null)
            {

                return Ok(new { ImportHistoryList = entImportHistorylist });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
