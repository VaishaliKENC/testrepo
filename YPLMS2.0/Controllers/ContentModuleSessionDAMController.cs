using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.DataAccessManager;
using static System.Collections.Specialized.BitVector32;
using YPLMS2._0.API.Entity.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContentModuleSessionDAMController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IContentModuleSessionDAM<ContentModuleSession> _ContentModuleSessionDAM;  
        private readonly LearnerDAM _learnerDAM = new LearnerDAM();
        private readonly IActivityAssignmentAdaptor<ActivityAssignment> _activityAssignmentAdaptor;
       private readonly IContentModuleTrackingManager<ContentModuleSession> _ContentModuleTrackingManage ;
        public ContentModuleSessionDAMController(IContentModuleSessionDAM<ContentModuleSession> ContentModuleSessionDAM, IMapper mapper, IActivityAssignmentAdaptor<ActivityAssignment> activityAssignmentAdaptor, IContentModuleTrackingManager<ContentModuleSession> ContentModuleTrackingManage)
        {
            _ContentModuleSessionDAM = ContentModuleSessionDAM;
            _activityAssignmentAdaptor= activityAssignmentAdaptor;
            _ContentModuleTrackingManage = ContentModuleTrackingManage;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("contentmodulesessionsave")]
        [Authorize]
        //public async Task<IActionResult> ContentModuleSessionSave(string clientId, ContentModuleSession session)
        public async Task<IActionResult> ContentModuleSessionSave( ContentModuleSessionVM session)
        {
            
            ContentModuleSession Session = new ContentModuleSession();
            // Session = _ContentModuleSessionDAM.Save(clientId, session);
            Session = _ContentModuleSessionDAM.SaveSession( _mapper.Map<ContentModuleSession>(session));
           
            if (Session != null)
            {
                return Ok(new { ContentModuleSession = Session });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("contentmodulesessiongetbyid")]
        [Authorize]
        public async Task<IActionResult> ContentModuleSessionGetById(string clientId, string sessionId)
        {
           
            ContentModuleSession Session = new ContentModuleSession();
            Session = _ContentModuleSessionDAM.GetById(clientId, sessionId);
            if (Session != null)
            {
                return Ok(new { ContentModuleSession = Session });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("getbyidforcourselaunch")]
        [Authorize]
        public async Task<IActionResult> GetByIdForCourseLaunch(string clientId, string sessionId)
        {
            try
            {
                var session = _ContentModuleSessionDAM.GetById(clientId, sessionId);
                var currentUser = new Learner { ID = session.SystemUserGuid, ClientId = clientId };
                session.Learner = _learnerDAM.GetUserByID_CoursePlayer(currentUser);

                var assignment = new ActivityAssignment { ID = session.ContentModuleId, UserID = session.SystemUserGuid, ClientId = clientId };
                session.Assignment = _activityAssignmentAdaptor.CheckUserAssignmentByID_CoursePlayer(assignment);

                session.ContentModule = _ContentModuleTrackingManage.GetContentModule(clientId, session.ContentModuleId);

                session.ContentModuleTracking = _ContentModuleTrackingManage.GetTracking(clientId, session);                

                if (session != null)
                {
                    return Ok(new { Session = session, Code = 200 });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "Session not found" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
               
            }
            
        }

        
    }
}
