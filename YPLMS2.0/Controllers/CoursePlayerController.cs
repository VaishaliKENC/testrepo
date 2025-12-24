using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Assignment;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CoursePlayerController : ControllerBase
    {
     private readonly ICoursePlayerAssignmentManager _coursePlayerAssignmentManager;

        public CoursePlayerController(ICoursePlayerAssignmentManager coursePlayerAssignmentManager)
        {
            _coursePlayerAssignmentManager = coursePlayerAssignmentManager;

        }

        [HttpGet]
        [Route("getassignmentforlaunch")]
        [Authorize]
        public async Task<IActionResult> GetAssignmentForLaunch(string ClientId,string ContentModuleId,string SystemUserGUID,string LaunchType)
        {
            try
            {
                YPLMS2._0.API.Entity.LaunchSite type;
                if(LaunchType.Trim() == "Learner")
                {
                    type = YPLMS2._0.API.Entity.LaunchSite.Learner;
                }
                else
                {
                    type = YPLMS2._0.API.Entity.LaunchSite.Admin;
                } 

                var activityAssignment = _coursePlayerAssignmentManager.GetAssignmentForLaunch(ClientId, ContentModuleId, SystemUserGUID, type);

                if (activityAssignment != null)
                {
                    if (activityAssignment.ID != null) 
                    {
                        if (activityAssignment.SystemUserGuid == null)
                            activityAssignment.SystemUserGuid = SystemUserGUID;

                        if (activityAssignment.ClientId == null)
                            activityAssignment.ClientId = ClientId;

                        string retCourse= _coursePlayerAssignmentManager.LaunchActivity(activityAssignment, type);
                       if(retCourse != string.Empty)
                        {
                            return NotFound(new { Code = 404, Msg = retCourse });
                        }
                    }
                    return Ok(new { AssetAssignment = activityAssignment, Code = 200 });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No Data Found" });
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
