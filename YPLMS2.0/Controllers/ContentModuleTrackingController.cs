using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.Entity.Tracking;
using static YPLMS2._0.API.DataAccessManager.ContentModuleTrackingAdaptor;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContentModuleTrackingController : ControllerBase
    {
       
        private readonly IContentModuleTrackingAdaptor _contentModuleTrackingAdaptor;       

        public ContentModuleTrackingController(IContentModuleTrackingAdaptor contentModuleTrackingAdaptor) 
        {
            _contentModuleTrackingAdaptor= contentModuleTrackingAdaptor;
        }
        [HttpPost]
        [Route("getcontentmoduletrackingbyid")]
        [Authorize]
        public async Task<IActionResult> GetContentModuleTrackingByID(ContentModuleTracking pEntContModTracking)
        {
            ContentModuleTrackingAdaptor contentModuleTrackingAdaptor = new ContentModuleTrackingAdaptor();
            ContentModuleTracking entContModTracking = new ContentModuleTracking();

            entContModTracking = contentModuleTrackingAdaptor.GetContentModuleTrackingByID(pEntContModTracking);
            if (entContModTracking != null)
            {
                return Ok(new { ContentModuleTracking = entContModTracking });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("getcontentmoduletrackingstatusbyidlearner")]
        [Authorize]
        public async Task<IActionResult> GetContentModuleTrackingStatusById_Learner(ContentModuleTracking pEntContModTracking)
        {
            ContentModuleTrackingAdaptor contentModuleTrackingAdaptor = new ContentModuleTrackingAdaptor();
            ContentModuleTracking entContModTracking = new ContentModuleTracking();

            entContModTracking = contentModuleTrackingAdaptor.GetContentModuleTrackingStatusById_Learner(pEntContModTracking);
            if (entContModTracking != null)
            {
                return Ok(new { ContentModuleTracking = entContModTracking });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("getcontentmoduletrackingafterupdate")]
        [Authorize]
        public async Task<IActionResult> GetContentModuleTrackingAfterUpdate(ContentModuleTracking pEntContModTracking)
        {
            ContentModuleTrackingAdaptor contentModuleTrackingAdaptor = new ContentModuleTrackingAdaptor();
            ContentModuleTracking entContModTracking = new ContentModuleTracking();

            entContModTracking = contentModuleTrackingAdaptor.GetContentModuleTrackingAfterUpdate(pEntContModTracking);
            if (entContModTracking != null)
            {
                return Ok(new { ContentModuleTracking = entContModTracking });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("addcontentmoduletracking")]
        [Authorize]
        public async Task<IActionResult> AddContentModuleTracking(ContentModuleTracking pEntContModTracking)
        {
            ContentModuleTrackingAdaptor contentModuleTrackingAdaptor = new ContentModuleTrackingAdaptor();
            ContentModuleTrackingUpdateResult entContModTracking = new ContentModuleTrackingUpdateResult();
            entContModTracking = contentModuleTrackingAdaptor.AddContentModuleTracking(pEntContModTracking);
            if (entContModTracking != null)
            {
                return Ok(new { ContentModuleTracking = entContModTracking });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("editcontentmoduletracking")]
        [Authorize]
        public async Task<IActionResult> EditContentModuleTracking(ContentModuleTracking pEntContModTracking)
        {
            ContentModuleTrackingAdaptor contentModuleTrackingAdaptor = new ContentModuleTrackingAdaptor();
            ContentModuleTrackingUpdateResult entContModTracking = new ContentModuleTrackingUpdateResult();
            entContModTracking = contentModuleTrackingAdaptor.EditContentModuleTracking(pEntContModTracking);
            if (entContModTracking != null)
            {
                return Ok(new { ContentModuleTracking = entContModTracking });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("bulkupdate")]
        [Authorize]
        public async Task<IActionResult> BulkUpdate(List<ContentModuleTracking> pEntListTracking, bool pIsBulkMarkCompleted)
        {
            ContentModuleTrackingAdaptor contentModuleTrackingAdaptor = new ContentModuleTrackingAdaptor();
            List<ContentModuleTracking> entListTracking = new List<ContentModuleTracking>();
            entListTracking = contentModuleTrackingAdaptor.BulkUpdate(pEntListTracking, pIsBulkMarkCompleted);
            if (entListTracking != null)
            {
                return Ok(new { ContentModuleTrackingList = entListTracking });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("updateassessmentcourse")]
        [Authorize]
        public async Task<IActionResult> UpdateAssessmentCourse(ContentModuleTracking pEntContModTracking)
        {
            ContentModuleTrackingAdaptor contentModuleTrackingAdaptor = new ContentModuleTrackingAdaptor();
            ContentModuleTracking entContModTracking = new ContentModuleTracking();
            entContModTracking = contentModuleTrackingAdaptor.UpdateAssessmentCourse(pEntContModTracking);
            if (entContModTracking != null)
            {
                return Ok(new { ContentModuleTracking = entContModTracking });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("updatescannedfilename")]
        [Authorize]
        public async Task<IActionResult> UpdateScannedFileName(ContentModuleTracking pEntContModTracking)
        {
            ContentModuleTrackingAdaptor contentModuleTrackingAdaptor = new ContentModuleTrackingAdaptor();
            ContentModuleTracking entContModTracking = new ContentModuleTracking();
            entContModTracking = contentModuleTrackingAdaptor.UpdateScannedFileName(pEntContModTracking);
            if (entContModTracking != null)
            {
                return Ok(new { ContentModuleTracking = entContModTracking });
            }
            else
            {
                return BadRequest();
            }
        }

        

    }

}
