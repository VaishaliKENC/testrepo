using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BulkImportController : ControllerBase
    {
        private readonly IBulkImportAdaptor<BulkImport> _bulkimportadaptor;
        public BulkImportController(IBulkImportAdaptor<BulkImport> bulkimportadaptor)
        {
            _bulkimportadaptor= bulkimportadaptor;
        }

        [HttpPost]
        [Route("getbulkimportmasterbyid")]
        [Authorize]
        public async Task<IActionResult> GetBulkImportMasterByID(BulkImport pEntBulkImportMaster)
        {

            BulkImport entBulkImportMaster = new BulkImport();
            entBulkImportMaster = _bulkimportadaptor.GetBulkImportMasterByID(pEntBulkImportMaster);
            if (entBulkImportMaster != null)
            {
                return Ok(new { BulkImport = entBulkImportMaster });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("addbulkimportmaster")]
        [Authorize]
        public async Task<IActionResult> AddBulkImportMaster(BulkImport pEntBulkImportMaster)
        {

            BulkImport entBulkImportMaster = new BulkImport();
            entBulkImportMaster = _bulkimportadaptor.AddBulkImportMaster(pEntBulkImportMaster);
            if (entBulkImportMaster != null)
            {
                return Ok(new { BulkImport = entBulkImportMaster });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("editbulkimportmaster")]
        [Authorize]
        public async Task<IActionResult> EditBulkImportMaster(BulkImport pEntBulkImportMaster)
        {

            BulkImport entBulkImportMaster = new BulkImport();
            entBulkImportMaster = _bulkimportadaptor.EditBulkImportMaster(pEntBulkImportMaster);
            if (entBulkImportMaster != null)
            {
                return Ok(new { BulkImport = entBulkImportMaster });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("deletebulkimportmaster")]
        [Authorize]
        public async Task<IActionResult> DeleteBulkImportMaster(BulkImport pEntBulkImportMaster)
        {

            BulkImport entBulkImportMaster = new BulkImport();
            entBulkImportMaster = _bulkimportadaptor.DeleteBulkImportMaster(pEntBulkImportMaster);
            if (entBulkImportMaster != null)
            {
                return Ok(new { BulkImport = entBulkImportMaster });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("findbulkimporttasksschedular")]
        [Authorize]
        public async Task<IActionResult> FindBulkImportTasksSchedular(Search pEntSearch)
        {

            List<BulkImport> entListBulkImport = new List<BulkImport>();
            entListBulkImport = _bulkimportadaptor.FindBulkImportTasksSchedular(pEntSearch);
            if (entListBulkImport != null)
            {
                return Ok(new { BulkImportListImport = entListBulkImport });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
