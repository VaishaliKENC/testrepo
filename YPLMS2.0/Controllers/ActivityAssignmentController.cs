using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Org.BouncyCastle.Tls;
using System.Data;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Assignment;
using YPLMS2._0.API.DataAccessManager.BusinessManager;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;
using static YPLMS2._0.API.Entity.AssignmentRequestModel;
using static YPLMS2._0.API.Entity.Assignment;
using Lucene.Net.Messages;
using Microsoft.AspNetCore.Authorization;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActivityAssignmentController : ControllerBase
    {


        private readonly IActivityAssignmentAdaptor<ActivityAssignment> _activityAssignmentAdaptor;
        private readonly ILearnerDAM<Learner> _learnerDAM;
        private readonly IMapper _mapper;
        OTA_Methods oTA_Methods = new OTA_Methods();

        AssignmentAdaptor _assignmentAdaptor = new AssignmentAdaptor();
        API.DataAccessManager.BusinessManager.AssignmentManager entMgrAssignment = new API.DataAccessManager.BusinessManager.AssignmentManager();

        public ActivityAssignmentController(IActivityAssignmentAdaptor<ActivityAssignment> activityAssignmentAdaptor, IMapper mapper, ILearnerDAM<Learner> learnerDAM)
        {
            _activityAssignmentAdaptor = activityAssignmentAdaptor;
            _mapper = mapper;
            _learnerDAM = learnerDAM;
        }

        [HttpPost]
        [Route("getactivityassignmentbyid")]
        [Authorize]
        public async Task<IActionResult> GetActivityAssignmentByID(ActivityAssignmentVM pEntActivityAssignment)
        {

            ActivityAssignment entActivityAssignment = new ActivityAssignment();

            entActivityAssignment = _activityAssignmentAdaptor.GetActivityAssignmentByID(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entActivityAssignment != null)
            {
                return Ok(new { ActivityAssignment = entActivityAssignment, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getusersactivityassignmentbyid")]
        [Authorize]
        public async Task<IActionResult> GetUsersActivityAssignmentByID(ActivityAssignmentVM pEntActivityAssignment)
        {

            ActivityAssignment entActivityAssignment = new ActivityAssignment();

            entActivityAssignment = _activityAssignmentAdaptor.GetUsersActivityAssignmentByID(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entActivityAssignment != null)
            {
                return Ok(new { ActivityAssignment = entActivityAssignment, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getuseractivityforbulkimport")]
        [Authorize]
        public async Task<IActionResult> GetUserActivity_ForBulkImport(ActivityAssignmentVM pEntActivityAssignment)
        {

            ActivityAssignment entActivityAssignment = new ActivityAssignment();

            entActivityAssignment = _activityAssignmentAdaptor.GetUserActivity_ForBulkImport(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entActivityAssignment != null)
            {
                return Ok(new { ActivityAssignment = entActivityAssignment, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getcoursescount")]
        [Authorize]
        public async Task<IActionResult> GetCoursesCount(ActivityAssignmentVM pEntActivityAssignment)
        {

            ActivityAssignment entActivityAssignment = new ActivityAssignment();

            entActivityAssignment = _activityAssignmentAdaptor.GetCoursesCount(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entActivityAssignment != null)
            {
                return Ok(new { ActivityAssignment = entActivityAssignment, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getactivityassignmentbyidlearnerprint")]
        [Authorize]
        public async Task<IActionResult> GetActivityAssignmentByID_Learner_Print(ActivityAssignmentVM pEntActivityAssignment)
        {

            ActivityAssignment entActivityAssignment = new ActivityAssignment();

            entActivityAssignment = _activityAssignmentAdaptor.GetActivityAssignmentByID_Learner_Print(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entActivityAssignment != null)
            {
                return Ok(new { ActivityAssignment = entActivityAssignment, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getactivityassignmentbyidlearnerprintall")]
        [Authorize]
        public async Task<IActionResult> GetActivityAssignmentByID_Learner_Print_ALL(ActivityAssignmentVM pEntActivityAssignment)
        {

            ActivityAssignment entActivityAssignment = new ActivityAssignment();

            entActivityAssignment = _activityAssignmentAdaptor.GetActivityAssignmentByID_Learner_Print_ALL(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entActivityAssignment != null)
            {
                return Ok(new { ActivityAssignment = entActivityAssignment, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("checkuserassignmentbyid")]
        [Authorize]
        public async Task<IActionResult> CheckUserAssignmentByID(ActivityAssignmentVM pEntActivityAssignment)
        {

            ActivityAssignment entActivityAssignment = new ActivityAssignment();

            entActivityAssignment = _activityAssignmentAdaptor.CheckUserAssignmentByID(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entActivityAssignment != null)
            {
                return Ok(new { ActivityAssignment = entActivityAssignment, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpGet]
        [Route("isalreadycourselaunched")]
        [Authorize]
        public async Task<IActionResult> IsAlreadyCourseLaunched(string clientId, string tokenKey)
        {

            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            ActivityAssignmentExt oActivityAssignmentExt = new ActivityAssignmentExt();
            oActivityAssignmentExt = _activityAssignmentAdaptor.IsAlreadyCourseLaunched(clientId, tokenKey);
            if (oActivityAssignmentExt != null)
            {
                return Ok(new { ActivityAssignment = oActivityAssignmentExt, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("checkuserassignmentbyidcourseplayer")]
        [Authorize]
        public async Task<IActionResult> CheckUserAssignmentByID_CoursePlayer(ActivityAssignmentVM pEntActivityAssignment)
        {

            ActivityAssignment entActivityAssignment = new ActivityAssignment();

            entActivityAssignment = _activityAssignmentAdaptor.CheckUserAssignmentByID_CoursePlayer(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entActivityAssignment != null)
            {
                return Ok(new { ActivityAssignment = entActivityAssignment, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("checkuserassignmentbyidoptimized")]
        [Authorize]
        public async Task<IActionResult> CheckUserAssignmentByIDOptimized(ActivityAssignmentVM pEntActivityAssignment)
        {

            ActivityAssignment entActivityAssignment = new ActivityAssignment();

            entActivityAssignment = _activityAssignmentAdaptor.CheckUserAssignmentByIDOptimized(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entActivityAssignment != null)
            {
                return Ok(new { ActivityAssignment = entActivityAssignment, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("addactivityassignment")]
        [Authorize]
        public async Task<IActionResult> AddActivityAssignment(ActivityAssignmentVM pEntActivityAssignment)
        {

            ActivityAssignment entActivityAssignment = new ActivityAssignment();

            entActivityAssignment = _activityAssignmentAdaptor.AddActivityAssignment(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entActivityAssignment != null)
            {

                return Ok(new { ActivityAssignment = entActivityAssignment, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPut]
        [Route("editactivityassignment")]
        [Authorize]
        public async Task<IActionResult> EditActivityAssignment(ActivityAssignmentVM pEntActivityAssignment)
        {

            ActivityAssignment entActivityAssignment = new ActivityAssignment();

            entActivityAssignment = _activityAssignmentAdaptor.EditActivityAssignment(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entActivityAssignment != null)
            {
                return Ok(new { ActivityAssignment = entActivityAssignment, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getuseractivityassignmentlist")]
        [Authorize]
        public async Task<IActionResult> GetUserActivityAssignmentList(ActivityAssignmentVM pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();

            entListLearnerAssignments = _activityAssignmentAdaptor.GetUserActivityAssignmentList(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getuseractivityassignmentlistfodl")]
        [Authorize]
        public async Task<IActionResult> GetUserActivityAssignmentListFoDL(ActivityAssignmentVM pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();

            entListLearnerAssignments = _activityAssignmentAdaptor.GetUserActivityAssignmentListFoDL(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getuserassignmentlistforemailtemplate")]
        [Authorize]
        public async Task<IActionResult> GetUserAssignmentListForEmailTemplate(ActivityAssignmentVM pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();

            entListLearnerAssignments = _activityAssignmentAdaptor.GetUserAssignmentListForEmailTemplate(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getuserassignmentlistforedit")]
        [Authorize]
        public async Task<IActionResult> GetUserAssignmentListForEdit(ActivityAssignmentVM pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();

            entListLearnerAssignments = _activityAssignmentAdaptor.GetUserAssignmentListForEdit(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getuserassignmentlistforunlock")]
        [Authorize]
        public async Task<IActionResult> GetUserAssignmentListForUnlock(ActivityAssignmentVM pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();

            entListLearnerAssignments = _activityAssignmentAdaptor.GetUserAssignmentListForUnlock(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getuserassignmentsforattemptunlock")]
        [Authorize]
        public async Task<IActionResult> GetUserAssignmentsForAttemptUnlock(ActivityAssignmentVM pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();

            entListLearnerAssignments = _activityAssignmentAdaptor.GetUserAssignmentsForAttemptUnlock(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getactivitylistbyname")]
        [Authorize]
        public async Task<IActionResult> FindActivityListByName(ActivityAssignmentVM pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();
            entListLearnerAssignments = _activityAssignmentAdaptor.FindActivityListByName(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getactivitylistbynameoptimized")]
        [Authorize]
        public async Task<IActionResult> FindActivityListByNameOptimized(ActivityAssignmentVM pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();
            entListLearnerAssignments = _activityAssignmentAdaptor.FindActivityListByNameOptimized(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getactivitylistfordelequencyhistory")]
        [Authorize]
        public async Task<IActionResult> FindActivityListForDelequencyHistory(ActivityAssignmentVM pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();
            entListLearnerAssignments = _activityAssignmentAdaptor.FindActivityListForDelequencyHistory(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getactivitylistforinteractiontracking")]
        [Authorize]
        public async Task<IActionResult> FindActivityListForInteractionTracking(ActivityAssignmentVM pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();
            entListLearnerAssignments = _activityAssignmentAdaptor.FindActivityListForInteractionTracking(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getactivityonetimeassignment")]
        [Authorize]
        public async Task<IActionResult> FindActivityOneTimeAssignment(ActivityAssignmentVM pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();
            entListLearnerAssignments = _activityAssignmentAdaptor.FindActivityOneTimeAssignment(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getallcertificationprogramsforapprovalfromlearner")]
        [Authorize]
        public async Task<IActionResult> GetAllCertificationPrograms_ForApproval_FromLearner(ActivityAssignmentVM pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();
            entListLearnerAssignments = _activityAssignmentAdaptor.GetAllCertificationPrograms_ForApproval_FromLearner(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getallcertificationprogramsforapproval")]
        [Authorize]
        public async Task<IActionResult> GetAllCertificationPrograms_ForApproval(ActivityAssignmentVM pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();
            entListLearnerAssignments = _activityAssignmentAdaptor.GetAllCertificationPrograms_ForApproval(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("updatecertificationprogramsforapproval")]
        [Authorize]
        public async Task<IActionResult> UpdateCertificationPrograms_ForApproval(ActivityAssignmentVM pEntActivityAssignment)
        {

            ActivityAssignment pEntActivityAssignments = new ActivityAssignment();
            pEntActivityAssignments = _activityAssignmentAdaptor.UpdateCertificationPrograms_ForApproval(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (pEntActivityAssignments != null)
            {
                return Ok(new { ActivityAssignment = pEntActivityAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getactivityonetimeassignmentforcurriculum")]
        [Authorize]
        public async Task<IActionResult> FindActivityOneTimeAssignment_ForCurriculum(ActivityAssignment pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();
            //entListLearnerAssignments = _activityAssignmentAdaptor.FindActivityOneTimeAssignment_ForCurriculum(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            entListLearnerAssignments = _activityAssignmentAdaptor.FindActivityOneTimeAssignment_ForCurriculum(pEntActivityAssignment);
            if (entListLearnerAssignments != null && entListLearnerAssignments.Count>0)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "No data found" });
            }
        }

        [HttpPost]
        [Route("getallactivitybycategorymapping")]
        [Authorize]
        public async Task<IActionResult> GetAllActivityByCategoryMapping(ActivityAssignmentVM pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();
            entListLearnerAssignments = _activityAssignmentAdaptor.GetAllActivityByCategoryMapping(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getallactivitybyunifiedmappingmapping")]
        [Authorize]
        public async Task<IActionResult> GetAllActivityByUnifiedMappingMapping(ActivityAssignmentVM pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();
            entListLearnerAssignments = _activityAssignmentAdaptor.GetAllActivityByUnifiedMappingMapping(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getallactivitybyactivitycertificatemapping")]
        [Authorize]
        public async Task<IActionResult> GetAllActivityByActivityCertificateMapping(ActivityAssignmentVM pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();
            entListLearnerAssignments = _activityAssignmentAdaptor.GetAllActivityByActivityCertificateMapping(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpGet]
        [Route("getactivitytype")]
        [Authorize]
        public async Task<IActionResult> GetActivityType(string ClientId)
        {
            try
            {
                LookUpManager _lookupManager = new LookUpManager();
                List<Lookup> entBaseList;
                Lookup entLookUpData = new Lookup();
                entLookUpData.LookupType = LookupType.ActivityType;
                entLookUpData.ClientId = ClientId;
                entBaseList = _lookupManager.Execute(entLookUpData, Lookup.ListMethod.GetAllByLookupType);
                if (entBaseList != null && entBaseList.Count > 0)
                {
                    return Ok(new { AssetTypeList = entBaseList, Code = 200 });
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

        [HttpPost]
        [Route("getallcategory")]
        [Authorize]
        public async Task<IActionResult> GetAllCategory(ActivityAssignment pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();
            entListLearnerAssignments = _activityAssignmentAdaptor.GetAllCategory(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getproductsubcategorybyid")]
        [Authorize]
        public async Task<IActionResult> GetProductSubCategoryByID(ActivityAssignment pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = new List<ActivityAssignment>();
            entListLearnerAssignments = _activityAssignmentAdaptor.GetProductSubCategoryByID(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListLearnerAssignments != null)
            {
                return Ok(new { ActivityAssignmentList = entListLearnerAssignments, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }

        [HttpPost]
        [Route("getbusinessrule")]
        [Authorize]
        public async Task<IActionResult> GetBusinessRule(Assignment pEntActivityAssignment)
        {
            List<GroupRule> entListRules = new List<GroupRule>();
            entListRules = _assignmentAdaptor.GetRuleList(pEntActivityAssignment);
            if (entListRules != null)
            {
                return Ok(new { ActivityAssignmentList = entListRules, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Not Found" });
            }
        }


        [HttpPost("submitonetimeassignment")]
        [Authorize]
        public async Task<IActionResult> SubmitOneTimeAssignment(Assignment pEntActivityAssignment)
        {
            try
            {
                //if (!ModelState.IsValid)
                //    return BadRequest(new { Code = 400, Msg = "Invalid request data." });

                if (pEntActivityAssignment is null)
                       return BadRequest(new { Code = 400, Msg = "Invalid request data." });


                DateTime UsrRegstrationDate = DateTime.MinValue;
                EmailTemplate entTemplate = null;
                entTemplate = oTA_Methods.GetEmailTemplate(pEntActivityAssignment);
                bool isActivitySelected = false;
                bool isLearnerSelected = false;
                bool isBusinessRuleSelected = false;
                string strSystemUserGuid = string.Empty;
                ActivityAssignment assignment = null;

                var entListBase = new List<ActivityAssignment>();
                var entListActivityMessages = new List<ActivityAssignment>();
                string strActivtyId = "", strActivtyIdCurrt = "";

                //if (!IsPageValid(model) || !IsDatesValids(model))
                //    return BadRequest(new { Code = 400, Msg = "Page or date validation failed." });

                foreach (var activity in pEntActivityAssignment.Activities)
                {
                    if (!activity.IsSelected) continue;

                    isActivitySelected = true;
                    strActivtyId += activity.ID + ",";
                    if (activity.Type == "Curriculum")
                        strActivtyIdCurrt += activity.ID + ",";

                    foreach (var learner in pEntActivityAssignment.Learners)
                    {
                        if (!learner.IsSelected) continue;

                        isLearnerSelected = true;
                        assignment = new ActivityAssignment
                        {
                            ID = activity.ID,
                            ActivityName = activity.Name,
                            UserID = learner.ID,
                            ActivityTypeId = oTA_Methods.GetActivityContentType(activity.Type),
                            AssignmentModeForOverride = ActivityAssignmentMode.UI,
                            IsCurrentlyAssigned = true,
                            IsEditfromUI = false,
                            AssignmentTypeId = ActivityAssignmentType.OneTimeAssignment,
                            CompletionConditionId = ActivityCompletionCondition.Mandatory,
                            CreatedById = pEntActivityAssignment.CurrentUserID,
                            LastModifiedById = pEntActivityAssignment.CurrentUserID,
                            ClientId = pEntActivityAssignment.ClientId,
                            IsReassignmentChecked = pEntActivityAssignment.IsReassignmentChecked,
                            ForceReAssignment = pEntActivityAssignment.IsReassignmentChecked,
                            IsMailChecked = pEntActivityAssignment.IsMailChecked,
                            BusinessRuleId = pEntActivityAssignment.BusinessRuleId,
                            IsAutoMail = pEntActivityAssignment.IsAutoMail,
                            IsDirectSendMail = pEntActivityAssignment.IsDirectSendMail,
                            IsAssignmentBasedOnHireDate = pEntActivityAssignment.IsAssignmentBasedOnHireDate,
                            IsAssignmentBasedOnCreationDate = pEntActivityAssignment.IsAssignmentBasedOnCreationDate,
                            AssignAfterDaysOf = pEntActivityAssignment.AssignAfterDaysOf,
                            IsNoDueDate = pEntActivityAssignment.IsNoDueDate,
                            IsDueBasedOnAssignDate = pEntActivityAssignment.IsDueBasedOnAssignDate,
                            IsDueBasedOnHireDate = pEntActivityAssignment.IsDueBasedOnHireDate,
                            IsDueBasedOnCreationDate = pEntActivityAssignment.IsDueBasedOnCreationDate,
                            IsDueBasedOnStartDate = pEntActivityAssignment.IsDueBasedOnStartDate,
                            DueAfterDaysOf = pEntActivityAssignment.DueAfterDaysOf,
                            IsNoExpiryDate = pEntActivityAssignment.IsNoExpiryDate,
                            IsExpiryBasedOnAssignDate = pEntActivityAssignment.IsExpiryBasedOnAssignDate,
                            IsExpiryBasedOnStartDate = pEntActivityAssignment.IsExpiryBasedOnStartDate,
                            IsExpiryBasedOnDueDate = pEntActivityAssignment.IsExpiryBasedOnDueDate,
                            ExpireAfterDaysOf = pEntActivityAssignment.ExpireAfterDaysOf,
                            SendEmail = pEntActivityAssignment.IsMailChecked,

                            IsReAssignmentBasedOnAssignmentDate = false,
                            ReAssignAfterDaysOf = 0,
                            ReAssignmentDateSet = (DateTime?)null,
                            IsReassignNoDueDate = false,
                            ReassignDueAfterDaysOf = 0,
                            ReassignDueDateSet = (DateTime?)null,
                            IsReassignNoExpiryDate = false,
                            ReassignExpireAfterDaysOf = 0,
                            ReassignExpiryDateSet = (DateTime?)null,
                            IsReassignDueBasedOnAssignmentCompletionDate = false,
                            IsReassignDueBasedOnReassignmentDate = false,
                            IsReassignExpiryBasedOnAssignmentCompletionDate = false,
                            IsReassignExpiryBasedOnReassignmentDate = false,
                            IsReassignExpiryBasedOnReassignmentDueDate = false

                            //IsReAssignmentBasedOnAssignmentDate = pEntActivityAssignment.IsReAssignmentBasedOnAssignmentDate,
                            //ReAssignAfterDaysOf = pEntActivityAssignment.ReAssignAfterDaysOf,
                            //ReAssignmentDateSet = pEntActivityAssignment.ReAssignmentDateSet,
                            //IsReassignNoDueDate = pEntActivityAssignment.IsReassignNoDueDate,
                            //ReassignDueAfterDaysOf = pEntActivityAssignment.ReassignDueAfterDaysOf,
                            //ReassignDueDateSet = pEntActivityAssignment.ReassignDueDateSet,
                            //IsReassignNoExpiryDate = pEntActivityAssignment.IsReassignNoExpiryDate,
                            //ReassignExpireAfterDaysOf = pEntActivityAssignment.ReassignExpireAfterDaysOf,
                            //ReassignExpiryDateSet = pEntActivityAssignment.ReassignExpiryDateSet,
                            //IsReassignDueBasedOnAssignmentCompletionDate = pEntActivityAssignment.IsReassignDueBasedOnAssignmentCompletionDate,
                            //IsReassignDueBasedOnReassignmentDate = pEntActivityAssignment.IsReassignDueBasedOnReassignmentDate,
                            //IsReassignExpiryBasedOnAssignmentCompletionDate = pEntActivityAssignment.IsReassignExpiryBasedOnAssignmentCompletionDate,
                            //IsReassignExpiryBasedOnReassignmentDate = pEntActivityAssignment.IsReassignExpiryBasedOnReassignmentDate,
                            //IsReassignExpiryBasedOnReassignmentDueDate = pEntActivityAssignment.IsReassignExpiryBasedOnReassignmentDueDate
                        };

                        if (!string.IsNullOrWhiteSpace(learner.RegistrationDate))
                            UsrRegstrationDate = CommonMethods.GetSpecificDateFormat(learner.RegistrationDate);

                        strSystemUserGuid += learner.ID + ",";

                        oTA_Methods.setAssignmnetDate(pEntActivityAssignment, ref assignment);

                        oTA_Methods.setReassignmnetdate(pEntActivityAssignment, ref assignment);

                        if (pEntActivityAssignment.SendMail && pEntActivityAssignment.MailOptionsVisible && entTemplate != null)
                        {
                            assignment.EmailTemplateId = entTemplate.ID;
                            assignment.SendEmailType = pEntActivityAssignment.IsAutoMail ? "autoemail" : "scheduleemail";
                            assignment.SendEmail = true;
                        }

                        entListBase.Add(assignment);
                        if (pEntActivityAssignment.BusinessRuleId == "0" && !entListActivityMessages.Contains(assignment))
                            entListActivityMessages.Add(assignment);
                    }

                    // Handle business rule
                    if (pEntActivityAssignment.BusinessRuleId != "0")
                    {
                        var entUsers = new Assignment
                        {
                            ClientId = pEntActivityAssignment.ClientId,
                            RuleId = pEntActivityAssignment.BusinessRuleId,
                            //ListRange = new EntityRange { RequestedById = CommonMethods.GetRequestedById() }
                            ListRange = new EntityRange { RequestedById = LearnerFacade.GetRequestedById(strSystemUserGuid, pEntActivityAssignment.ClientId) }

                        };

                        var entBusinessRuleUsers = entMgrAssignment.Execute((BaseEntity)entUsers, Assignment.ListMethod.GetUsersByRuleId);


                        if (entBusinessRuleUsers != null)
                        {
                            foreach (BaseEntity entUser in entBusinessRuleUsers)
                            {
                                isBusinessRuleSelected = true;

                                assignment = new ActivityAssignment
                                {
                                    ID = activity.ID,
                                    ActivityName = activity.Name,
                                    UserID = entUser.ID,
                                    ActivityTypeId = oTA_Methods.GetActivityContentType(activity.Type),
                                    CreatedById = pEntActivityAssignment.CurrentUserID,
                                    LastModifiedById = pEntActivityAssignment.CurrentUserID,
                                    ClientId = pEntActivityAssignment.ClientId,
                                    IsCurrentlyAssigned = true,
                                    ForceReAssignment = pEntActivityAssignment.ForceReassignment,
                                    IsMailChecked = pEntActivityAssignment.IsMailChecked,
                                    BusinessRuleId = pEntActivityAssignment.BusinessRuleId,
                                    IsAutoMail = pEntActivityAssignment.IsAutoMail,
                                    IsDirectSendMail = pEntActivityAssignment.IsDirectSendMail,
                                    IsAssignmentBasedOnHireDate = pEntActivityAssignment.IsAssignmentBasedOnHireDate,
                                    IsAssignmentBasedOnCreationDate = pEntActivityAssignment.IsAssignmentBasedOnCreationDate,
                                    AssignAfterDaysOf = pEntActivityAssignment.AssignAfterDaysOf,
                                    IsNoDueDate = pEntActivityAssignment.IsNoDueDate,
                                    IsDueBasedOnAssignDate = pEntActivityAssignment.IsDueBasedOnAssignDate,
                                    IsDueBasedOnHireDate = pEntActivityAssignment.IsDueBasedOnHireDate,
                                    IsDueBasedOnCreationDate = pEntActivityAssignment.IsDueBasedOnCreationDate,
                                    IsDueBasedOnStartDate = pEntActivityAssignment.IsDueBasedOnStartDate,
                                    DueAfterDaysOf = pEntActivityAssignment.DueAfterDaysOf,
                                    IsNoExpiryDate = pEntActivityAssignment.IsNoExpiryDate,
                                    IsExpiryBasedOnAssignDate = pEntActivityAssignment.IsExpiryBasedOnAssignDate,
                                    IsExpiryBasedOnStartDate = pEntActivityAssignment.IsExpiryBasedOnStartDate,
                                    IsExpiryBasedOnDueDate = pEntActivityAssignment.IsExpiryBasedOnDueDate,
                                    ExpireAfterDaysOf = pEntActivityAssignment.ExpireAfterDaysOf,
                                    SendEmail = pEntActivityAssignment.IsMailChecked,

                                    IsReAssignmentBasedOnAssignmentDate = false,
                                    ReAssignAfterDaysOf = 0,
                                    ReAssignmentDateSet = (DateTime?)null,
                                    IsReassignNoDueDate = false,
                                    ReassignDueAfterDaysOf = 0,
                                    ReassignDueDateSet = (DateTime?)null,
                                    IsReassignNoExpiryDate = false,
                                    ReassignExpireAfterDaysOf = 0,
                                    ReassignExpiryDateSet = (DateTime?)null,
                                    IsReassignDueBasedOnAssignmentCompletionDate = false,
                                    IsReassignDueBasedOnReassignmentDate = false,
                                    IsReassignExpiryBasedOnAssignmentCompletionDate = false,
                                    IsReassignExpiryBasedOnReassignmentDate = false,
                                    IsReassignExpiryBasedOnReassignmentDueDate = false

                                    //IsReAssignmentBasedOnAssignmentDate = pEntActivityAssignment.IsReAssignmentBasedOnAssignmentDate,
                                    //ReAssignAfterDaysOf = pEntActivityAssignment.ReAssignAfterDaysOf,
                                    //ReAssignmentDateSet = pEntActivityAssignment.ReAssignmentDateSet,
                                    //IsReassignNoDueDate = pEntActivityAssignment.IsReassignNoDueDate,
                                    //ReassignDueAfterDaysOf = pEntActivityAssignment.ReassignDueAfterDaysOf,
                                    //ReassignDueDateSet = pEntActivityAssignment.ReassignDueDateSet,
                                    //IsReassignNoExpiryDate = pEntActivityAssignment.IsReassignNoExpiryDate,
                                    //ReassignExpireAfterDaysOf = pEntActivityAssignment.ReassignExpireAfterDaysOf,
                                    //ReassignExpiryDateSet = pEntActivityAssignment.ReassignExpiryDateSet,
                                    //IsReassignDueBasedOnAssignmentCompletionDate = pEntActivityAssignment.IsReassignDueBasedOnAssignmentCompletionDate,
                                    //IsReassignDueBasedOnReassignmentDate = pEntActivityAssignment.IsReassignDueBasedOnReassignmentDate,
                                    //IsReassignExpiryBasedOnAssignmentCompletionDate = pEntActivityAssignment.IsReassignExpiryBasedOnAssignmentCompletionDate,
                                    //IsReassignExpiryBasedOnReassignmentDate = pEntActivityAssignment.IsReassignExpiryBasedOnReassignmentDate,
                                    //IsReassignExpiryBasedOnReassignmentDueDate = pEntActivityAssignment.IsReassignExpiryBasedOnReassignmentDueDate
                                };

                                strSystemUserGuid += entUser.ID + ",";

                                oTA_Methods.setAssignmnetDate(pEntActivityAssignment, ref assignment);

                                oTA_Methods.setReassignmnetdate(pEntActivityAssignment, ref assignment);


                                if (pEntActivityAssignment.SendMail && pEntActivityAssignment.MailOptionsVisible && entTemplate != null)
                                {
                                    assignment.EmailTemplateId = entTemplate.ID;
                                    assignment.SendEmailType = pEntActivityAssignment.IsAutoMail ? "autoemail" : "scheduleemail";
                                    assignment.SendEmail = true;
                                }

                                entListBase.Add(assignment);
                            }
                            if (entListActivityMessages.Contains(assignment) == false)
                                entListActivityMessages.Add(assignment);
                        }

                        //if (!entListActivityMessages.Contains(entListBase.Last()))
                        //    entListActivityMessages.Add(entListBase.Last());
                    }
                }

                // Capacity check for VT/CT
                var entVTCT = new ActivityAssignment
                {
                    ClientId = pEntActivityAssignment.ClientId,
                    ActivityID = strActivtyIdCurrt,
                    UserID = strSystemUserGuid,
                    LastModifiedById = pEntActivityAssignment.LastModifiedById
                };

                var ds = new ActivityAssignmentManager().ExecuteDataSet1(entVTCT, ActivityAssignment.ListMethod.GetVirtualClassroomNominationRegistrationCountOneTimeAssignment);
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                        return Conflict(new { Code = 409, Msg = "CT event capacity exceeded." });
                    if (ds.Tables[1].Rows.Count > 0)
                        return Conflict(new { Code = 409, Msg = "VT event capacity exceeded." });
                }

                // Product license check
                if (pEntActivityAssignment.IsProductAdmin)
                {
                    var entPL = new ActivityAssignment
                    {
                        ClientId = pEntActivityAssignment.ClientId,
                        ActivityID = strActivtyId,
                        UserID = strSystemUserGuid,
                        LastModifiedById = pEntActivityAssignment.CurrentUserID
                    };

                    var isExceeded = Convert.ToBoolean(new ActivityAssignmentManager().ExecuteScalar(entPL, ActivityAssignment.Method.IsProductLicenseExceed));
                    if (isExceeded)
                        return Conflict(new { Code = 409, Msg = "Product license limit exceeded." });
                }


                // Save & Send Mail
                var entParams = new ActivityAssignment
                {
                    LstActAssignments = entListBase,
                    ActivtyId = strActivtyId,
                    SystemUserGuid = strSystemUserGuid,
                    EmailTemplate = entTemplate,
                    IsActivitySelected = isActivitySelected,
                    IsLearnerSelected = isLearnerSelected,
                    IsBusinessRuleSelected = isBusinessRuleSelected,
                    ClientId = pEntActivityAssignment.ClientId,
                    CurrentUserID = pEntActivityAssignment.CurrentUserID,
                    IsReassignmentChecked = pEntActivityAssignment.IsReassignmentChecked,
                    ForceReAssignment = pEntActivityAssignment.IsReassignmentChecked,
                    IsMailChecked = pEntActivityAssignment.IsMailChecked,
                    BusinessRuleId = pEntActivityAssignment.BusinessRuleId,
                    IsAutoMail = pEntActivityAssignment.IsAutoMail,
                    IsDirectSendMail = pEntActivityAssignment.IsDirectSendMail,
                    IsAssignmentBasedOnHireDate = pEntActivityAssignment.IsAssignmentBasedOnHireDate,
                    IsAssignmentBasedOnCreationDate = pEntActivityAssignment.IsAssignmentBasedOnCreationDate,
                    AssignAfterDaysOf = pEntActivityAssignment.AssignAfterDaysOf,
                    IsNoDueDate = pEntActivityAssignment.IsNoDueDate,
                    IsDueBasedOnAssignDate = pEntActivityAssignment.IsDueBasedOnAssignDate,
                    IsDueBasedOnHireDate = pEntActivityAssignment.IsDueBasedOnHireDate,
                    IsDueBasedOnCreationDate = pEntActivityAssignment.IsDueBasedOnCreationDate,
                    IsDueBasedOnStartDate = pEntActivityAssignment.IsDueBasedOnStartDate,
                    DueAfterDaysOf = pEntActivityAssignment.DueAfterDaysOf,
                    IsNoExpiryDate = pEntActivityAssignment.IsNoExpiryDate,
                    IsExpiryBasedOnAssignDate = pEntActivityAssignment.IsExpiryBasedOnAssignDate,
                    IsExpiryBasedOnStartDate = pEntActivityAssignment.IsExpiryBasedOnStartDate,
                    IsExpiryBasedOnDueDate = pEntActivityAssignment.IsExpiryBasedOnDueDate,
                    ExpireAfterDaysOf = pEntActivityAssignment.ExpireAfterDaysOf,

                    //IsReAssignmentBasedOnAssignmentDate=pEntActivityAssignment.IsReAssignmentBasedOnAssignmentDate,
                    //ReAssignAfterDaysOf=pEntActivityAssignment.ReAssignAfterDaysOf,
                    //ReAssignmentDateSet = pEntActivityAssignment.ReAssignmentDateSet,
                    //IsReassignNoDueDate = pEntActivityAssignment.IsReassignNoDueDate,
                    //ReassignDueAfterDaysOf = pEntActivityAssignment.ReassignDueAfterDaysOf,
                    //ReassignDueDateSet = pEntActivityAssignment.ReassignDueDateSet,
                    //IsReassignNoExpiryDate = pEntActivityAssignment.IsReassignNoExpiryDate,
                    //ReassignExpireAfterDaysOf = pEntActivityAssignment.ReassignExpireAfterDaysOf,
                    //ReassignExpiryDateSet = pEntActivityAssignment.ReassignExpiryDateSet,
                    //IsReassignDueBasedOnAssignmentCompletionDate = pEntActivityAssignment.IsReassignDueBasedOnAssignmentCompletionDate,
                    //IsReassignDueBasedOnReassignmentDate = pEntActivityAssignment.IsReassignDueBasedOnReassignmentDate,
                    //IsReassignExpiryBasedOnAssignmentCompletionDate = pEntActivityAssignment.IsReassignExpiryBasedOnAssignmentCompletionDate,
                    //IsReassignExpiryBasedOnReassignmentDate = pEntActivityAssignment.IsReassignExpiryBasedOnReassignmentDate,
                    //IsReassignExpiryBasedOnReassignmentDueDate = pEntActivityAssignment.IsReassignExpiryBasedOnReassignmentDueDate

                    IsReAssignmentBasedOnAssignmentDate = false,
                    ReAssignAfterDaysOf = 0,
                    ReAssignmentDateSet = (DateTime?)null,
                    IsReassignNoDueDate = false,
                    ReassignDueAfterDaysOf = 0,
                    ReassignDueDateSet = (DateTime?)null,
                    IsReassignNoExpiryDate = false,
                    ReassignExpireAfterDaysOf = 0,
                    ReassignExpiryDateSet = (DateTime?)null,
                    IsReassignDueBasedOnAssignmentCompletionDate = false,
                    IsReassignDueBasedOnReassignmentDate = false,
                    IsReassignExpiryBasedOnAssignmentCompletionDate = false,
                    IsReassignExpiryBasedOnReassignmentDate = false,
                    IsReassignExpiryBasedOnReassignmentDueDate = false
                };

                if (pEntActivityAssignment.IsMailChecked && entTemplate != null)
                {
                    entParams.EmailTemplateId = entTemplate.ID;
                    entParams.SendEmailType = pEntActivityAssignment.IsAutoMail ? "autoemail" : "scheduleemail";
                    entParams.SendEmail = true;
                }

                var ota = new OTA_Methods();
                ota.SaveAssignmentAndSendEmail(entParams);

                // Send alerts
                //if (pEntActivityAssignment.SendAlerts)
                //    SendLearnerAlerts(pEntActivityAssignment.ClientId, pEntActivityAssignment.CurrentUserID, pEntActivityAssignment.BusinessRuleId, entListActivityMessages, pEntActivityAssignment.Learners);

                return Ok(new { Code = 200, Msg = "Assignment submitted successfully." });
            }
            catch (CustomException ex)
            {
                return StatusCode(500, new { Code = 500, Msg = $"Custom error occurred: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Code = 500, Msg = $"Unexpected error: {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("saveselecteduser")]
        [Authorize]
        public async Task<IActionResult> SaveSelectedUser(List<LearnerVM> pEntLearner)
        {
            List<Learner> entListLearner = new List<Learner>();
            entListLearner = _activityAssignmentAdaptor.DeleteSelectedLearner(_mapper.Map<List<Learner>>(pEntLearner));
            entListLearner = _activityAssignmentAdaptor.SaveSelectedLearner(_mapper.Map<List<Learner>>(pEntLearner));
            if (entListLearner != null)
            {
                return Ok(new { UserList = entListLearner, Code = 200, Msg = "Users added successfully" });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Failed to save User" });
            }
        }


        [HttpDelete]
        [Route("deleteselecteduser")]
        [Authorize]
        public async Task<IActionResult> DeleteSelectedUser(List<LearnerVM> pEntLearner)
        {
            List<Learner> entListLearner = new List<Learner>();
            entListLearner = _activityAssignmentAdaptor.DeleteSelectedLearner(_mapper.Map<List<Learner>>(pEntLearner));
            if (entListLearner != null)
            {
                return Ok(new { UserList = entListLearner, Msg = "Users deleted successfully", Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Failed to delete users" });
            }
        }

        [HttpPost]
        [Route("saveselectedactivity")]
        [Authorize]
        public async Task<IActionResult> SaveSelectedActivity(List<ActivityAssignmentVM> pEntListActivityAssignment)
        {
            List<ActivityAssignment> entListActivityAssignment = new List<ActivityAssignment>();
            List<ActivityAssignmentVM> pEntDeleteActivity = new List<ActivityAssignmentVM>();
            ActivityAssignmentVM deleteActivity = new ActivityAssignmentVM();
            deleteActivity.CreatedById = pEntListActivityAssignment[0].CreatedById;
            deleteActivity.ClientId = pEntListActivityAssignment[0].ClientId;
            pEntDeleteActivity.Add(deleteActivity);
            entListActivityAssignment = _activityAssignmentAdaptor.DeleteSelectedActivity(_mapper.Map<List<ActivityAssignment>>(pEntDeleteActivity));
            entListActivityAssignment = _activityAssignmentAdaptor.SaveSelectedActivity(_mapper.Map<List<ActivityAssignment>>(pEntListActivityAssignment));
            if (entListActivityAssignment != null)
            {
                return Ok(new { ActivityList = entListActivityAssignment, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Failed to save Activity" });
            }
        }

        [HttpDelete]
        [Route("deleteselectedactivity")]
        [Authorize]
        public async Task<IActionResult> DeleteSelectedActivity(List<ActivityAssignmentVM> pEntListActivityAssignment)
        {
            List<ActivityAssignment> entListActivityAssignment = new List<ActivityAssignment>();
            entListActivityAssignment = _activityAssignmentAdaptor.DeleteSelectedActivity(_mapper.Map<List<ActivityAssignment>>(pEntListActivityAssignment));
            if (entListActivityAssignment != null)
            {
                return Ok(new { ActivityList = entListActivityAssignment, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "No data found" });
            }
        }

        [HttpPost]
        [Route("getselecteduser")]
        [Authorize]
        public async Task<IActionResult> GetSelectedUserForOnetimeAssignment(Search pEntSearch)
        {
            List<Learner> entListLearner = new List<Learner>();
            entListLearner = _learnerDAM.GetUserByRequestedID(pEntSearch);
            if (entListLearner != null && entListLearner.Count > 0)
            {
                return Ok(new { UserList = entListLearner, Code = 200, TotalRows = entListLearner[0].ListRange.TotalRows });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "No data found" });
            }
        }

        [HttpPost]
        [Route("getselectedactivity")]
        [Authorize]
        public async Task<IActionResult> GetSelectedActivityForOnetimeAssignment(ActivityAssignmentVM pEntActivityAssignment)
        {
            List<ActivityAssignment> entListActivityAssignment = new List<ActivityAssignment>();
            entListActivityAssignment = _activityAssignmentAdaptor.GetActivityByRequestedID(_mapper.Map<ActivityAssignment>(pEntActivityAssignment));
            if (entListActivityAssignment != null && entListActivityAssignment.Count > 0)
            {
                return Ok(new { ActivityList = entListActivityAssignment, Code = 200, TotalRows = entListActivityAssignment[0].ListRange.TotalRows });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "No data found" });
            }
        }

        [HttpPost]
        [Route("getbussinessruleusers")]
        [Authorize]
        public async Task<IActionResult> GetBussinessRuleUsers(LearnerVM pEntLearner)
        {
            List<Learner> entListLearner = new List<Learner>();
            entListLearner = _learnerDAM.GetUserByRuleID(_mapper.Map<Learner>(pEntLearner));
            if (entListLearner != null)
            {
                return Ok(new { UserList = entListLearner, Code = 200 });
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "No data found" });
            }
        }


        [HttpPost("savedefaultassignment")]
        [Authorize]
        public async Task<IActionResult> SaveDefaultAssignment(SaveAssignmentInputModel input)
        {
            List<DefaultAssignmentValue> pEntDefaultAssignmentValue = new List<DefaultAssignmentValue>();
            DefaultAssignmentValueManager objDefaultAssignmentValueManager = new DefaultAssignmentValueManager();

            try
            {
                if (input.PageIsValid)
                {
                    if (input.chkAssignmentDates)
                    {
                        string lblAssDtError = string.Empty;
                        string lblDueDtError = string.Empty;
                        string lblExpDtError = string.Empty;

                        if (!OTA_Methods.IsPageValid(input, ref lblAssDtError, ref lblDueDtError, ref lblExpDtError))
                            return BadRequest(new { Msg = "Validation failed." });
                        //return BadRequest("Validation failed.");


                        string message;
                        if (!OTA_Methods.IsDatesValids(input, out message))
                            return BadRequest(new { Msg = message });                     
                            
                        //return BadRequest(message);

                        #region Assessment Date
                        var DefaultAssignmentValue1 = new DefaultAssignmentValue
                        {
                            ClientId = input.ClientId,
                            ModuleName = "DefaultAssignmentDate",
                            DataTypee = "Numeric",
                            CreatedById = input.CreatedById,
                            IsUsedForDynamicAssignment = input.chkIsForDynamic,
                            FieldName = input.rbAbsoluteDate ? "AbsoluteDate" : "RelativeDate",
                            DefaultValue = input.rbAbsoluteDate ? input.txtDefaultAssignmnetDays : input.txtAssignmentDays,
                            Condition = input.rbAbsoluteDate ? $"DATEADD(DAY,{input.txtDefaultAssignmnetDays},getdate())" : input.ddlAssignmentDate
                        };
                        pEntDefaultAssignmentValue.Add(DefaultAssignmentValue1);
                        #endregion

                        #region Due Date
                        var DefaultAssignmentValue2 = new DefaultAssignmentValue
                        {
                            ClientId = input.ClientId,
                            ModuleName = "DefaultDueDate",
                            DataTypee = "Numeric",
                            CreatedById = input.CreatedById,
                            IsUsedForDynamicAssignment = input.chkIsForDynamic
                        };

                        if (input.rbAbsoluteDueDate)
                        {
                            DefaultAssignmentValue2.FieldName = "AbsoluteDate";
                            DefaultAssignmentValue2.DefaultValue = input.txtDefaultDueDays;
                            DefaultAssignmentValue2.Condition = $"DATEADD(DAY,{input.txtDefaultDueDays},getdate())";
                        }
                        else if (input.rbRelativeDueDate)
                        {
                            DefaultAssignmentValue2.FieldName = "RelativeDate";
                            DefaultAssignmentValue2.DefaultValue = input.txtDueDays;
                            DefaultAssignmentValue2.Condition = input.ddlDueDate;
                        }
                        else
                        {
                            DefaultAssignmentValue2.FieldName = "NoDueDate";
                        }

                        pEntDefaultAssignmentValue.Add(DefaultAssignmentValue2);
                        #endregion

                        #region Expiry Date
                        var DefaultAssignmentValue3 = new DefaultAssignmentValue
                        {
                            ClientId = input.ClientId,
                            ModuleName = "DefaultExpiryDate",
                            DataTypee = "Numeric",
                            CreatedById = input.CreatedById,
                            IsUsedForDynamicAssignment = input.chkIsForDynamic
                        };

                        if (input.rbAbsoluteExpiryDate)
                        {
                            DefaultAssignmentValue3.FieldName = "AbsoluteDate";
                            DefaultAssignmentValue3.DefaultValue = input.txtDefaultExpDays;
                            DefaultAssignmentValue3.Condition = $"DATEADD(DAY,{input.txtDefaultExpDays},getdate())";
                        }
                        else if (input.rbRelativeExpiryDate)
                        {
                            DefaultAssignmentValue3.FieldName = "RelativeDate";
                            DefaultAssignmentValue3.DefaultValue = input.txtExprDays;
                            DefaultAssignmentValue3.Condition = input.ddlExprDate;
                        }
                        else
                        {
                            DefaultAssignmentValue3.FieldName = "NoDueDate";
                        }

                        pEntDefaultAssignmentValue.Add(DefaultAssignmentValue3);
                        #endregion

                        var result = objDefaultAssignmentValueManager.Execute(pEntDefaultAssignmentValue, DefaultAssignmentValue.ListMethod.BulkAddDefaultValueList);
                    }
                    else
                    {
                        var entDefaultAssignmentValue = new DefaultAssignmentValue
                        {
                            ClientId = input.ClientId,
                            ModuleName = "DefaultAssignmentDate,DefaultDueDate,DefaultExpiryDate"
                        };
                        var entBaseReturn = objDefaultAssignmentValueManager.Execute(entDefaultAssignmentValue, DefaultAssignmentValue.Method.delete);
                    }
                    
                    return Ok(new
                    {
                        //Message = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.DefaultValue.RECORD_ADDED),
                        Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.DefaultValue.RECORD_ADDED, "", input.ClientId),
                        ReloadUrl = input.CurrentUrl
                    });
                }

                return BadRequest("Invalid page state.");
            }
            catch (Exception ex)
            {
                var _expCustom = new CustomException(
                    API.YPLMS.Services.Messages.PolicyLibrary.LIBRARY_DEL_ERROR,
                    CustomException.WhoCallsMe(),
                    ExceptionSeverityLevel.Information,
                    ex, true
                );

                return StatusCode(500, "An error occurred while saving.");
            }
        }


        [HttpGet("getdefaultsettings")]
        [Authorize]
        public ActionResult<SaveAssignmentInputModel> GetDefaultAssignmentSettings(string clientId)
        {
            try
            {
                var objManager = new DefaultAssignmentValueManager();
                var requestEntity = new DefaultAssignmentValue { ClientId = clientId };
                var list = objManager.Execute(requestEntity, DefaultAssignmentValue.ListMethod.GetDefaultValueList);

                var model = new SaveAssignmentInputModel
                {
                    ClientId = clientId,
                    chkAssignmentDates = list != null && list.Count > 0
                };

                if (!model.chkAssignmentDates)
                {
                    // assignment dates not enabled, disable panel on UI
                    return Ok(model);
                }

                foreach (var item in list)
                {
                    model.chkIsForDynamic = item.IsUsedForDynamicAssignment;

                    if (item.ModuleName == "DefaultAssignmentDate")
                    {
                        if (item.FieldName == "AbsoluteDate")
                        {
                            model.rbAbsoluteDate = true;
                            model.txtDefaultAssignmnetDays = item.DefaultValue;
                        }
                        else
                        {
                            model.rbRelativeDate = true;
                            model.txtAssignmentDays = item.DefaultValue;
                            model.ddlAssignmentDate = item.Condition;
                        }
                    }

                    if (item.ModuleName == "DefaultDueDate")
                    {
                        if (item.FieldName == "AbsoluteDate")
                        {
                            model.rbAbsoluteDueDate = true;
                            model.txtDefaultDueDays = item.DefaultValue;
                        }
                        else if (item.FieldName == "RelativeDate")
                        {
                            model.rbRelativeDueDate = true;
                            model.txtDueDays = item.DefaultValue;
                            model.ddlDueDate = item.Condition;
                        }
                        else
                        {
                            model.rbNoDueDate = true;
                        }
                    }

                    if (item.ModuleName == "DefaultExpiryDate")
                    {
                        if (item.FieldName == "AbsoluteDate")
                        {
                            model.rbAbsoluteExpiryDate = true;
                            model.txtDefaultExpDays = item.DefaultValue;
                        }
                        else if (item.FieldName == "RelativeDate")
                        {
                            model.rbRelativeExpiryDate = true;
                            model.txtExprDays = item.DefaultValue;
                            model.ddlExprDate = item.Condition;
                        }
                        else
                        {
                            model.rbNoExpiryDate = true;
                        }
                    }
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                // log if needed
                return StatusCode(500, "An error occurred while retrieving default assignment settings.");
            }


        }
    }
}
