using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.DataAccessManager;
using Microsoft.AspNetCore.Authorization;
namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScoTrackingSerializerController : ControllerBase
    {
        
        private readonly ILessonTrackingSerializer _scoTrackingSerializer;
        public ScoTrackingSerializerController(ILessonTrackingSerializer scoTrackingSerializer)
        {
           
            _scoTrackingSerializer = scoTrackingSerializer;
        }

        [HttpGet]
        [Route("readlessontracking")]
        [Authorize]
        public async Task<IActionResult> ReadLessonTracking(string userDataXml)
        {
            
            var lessons = new Dictionary<string, LessonTracking>();
            lessons = _scoTrackingSerializer.ReadLessonTracking(userDataXml);
            if (lessons != null)
            {
                return Ok(new { ContentModuleTrackingLessons = lessons });
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("writelessontracking")]
        [Authorize]
        public async Task<IActionResult> WriteLessonTracking(ContentModuleTracking contentModuleTracking)
        {
           
            string xDoc = _scoTrackingSerializer.WriteLessonTracking(contentModuleTracking);
            if (!string.IsNullOrEmpty(xDoc))
            {
                return Ok(new { ContentModuleTrackingLessonsxDoc = xDoc });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
