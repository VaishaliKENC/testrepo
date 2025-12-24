using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.YPLMS.Services.Messages;
using YPLMS2._0.API.YPLMS.Services;
using AutoMapper;
using MathNet.Numerics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Tls;
using System.Collections;
using System.Data;
using ImportUsersBL = YPLMS2._0.API.DataAccessManager.ImportUsersBL;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
namespace YPLMS2._0.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class LearnerDAMController : ControllerBase
    {
        private readonly ILearnerDAM<Learner> _learnerdam;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserPageManager _userPageManager = new UserPageManager();
        private readonly IImportDefinitionAdaptor<ImportDefination> _importDefinitionAdaptor;
        string _strBaseClientId = "1";
        public LearnerDAMController(ILearnerDAM<Learner> learnerdam, IConfiguration config, IMapper mapper, IImportDefinitionAdaptor<ImportDefination> importDefinitionAdaptor) 
        {
            _learnerdam = learnerdam;
            _config = config;
            _mapper = mapper;
            _importDefinitionAdaptor = importDefinitionAdaptor;
        }

        [HttpGet]
        [Route("getuserbySamlidentifier")]
        [Authorize]
        public async Task<IActionResult> GetUserBySamlIdentifier(string identifier, long userIdentifierColumnId, string clientId)
        {
            try
            {
                Learner learner = new Learner();
                learner = _learnerdam.GetUserBySamlIdentifier(identifier, userIdentifierColumnId, clientId);
                if (learner.ID != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new {Code = 404, Msg = "No data found"});
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }
        [HttpPost]
        [Route("getuserbyid")]
        [Authorize]
        public async Task<IActionResult> GetUserByID(LearnerVM pEntLearner)
        {
            try
            {
                Learner learner = new Learner();

                learner = _learnerdam.GetUserByID(_mapper.Map<Learner>(pEntLearner));
                if (learner.ID != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "User not found"});
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }
        [HttpPost]
        [Route("getuserbyIDSelfRegi")]
        [Authorize]
        public async Task<IActionResult> GetUserByIDSelfRegi(LearnerVM pEntLearner)
        {
            try
            {
                Learner learner = new Learner();

                learner = _learnerdam.GetUserByIDSelfRegi(_mapper.Map<Learner>(pEntLearner));
                if (learner.ID != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "User not found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
        [HttpPost]
        [Route("getuserbyidcourseplayer")]
        [Authorize]
        public async Task<IActionResult> GetUserByID_CoursePlayer(LearnerVM pEntLearner)
        {
           
            Learner learner = new Learner();
            learner = _learnerdam.GetUserByID_CoursePlayer(_mapper.Map<Learner>(pEntLearner));

            if (learner.ID != null)
            {
                return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("getuserrequestedByID")]
        [Authorize]
        public async Task<IActionResult> GetUserRequestedByID(LearnerVM pEntLearner)
        {
            Learner learner = new Learner();
            learner = _learnerdam.GetUserRequestedByID(_mapper.Map<Learner>(pEntLearner));

            if (learner.ID != null)
            {
                return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("getprefferreddateTime")]
        [Authorize]
        public async Task<IActionResult> GetPrefferredDateTime(LearnerVM pEntLearner)
        {
            Learner learner = new Learner();
            learner = _learnerdam.GetPrefferredDateTime(_mapper.Map<Learner>(pEntLearner));

            if (learner.ID != null)
            {
                return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("getuserbytypeid")]
        [Authorize]
        public async Task<IActionResult> GetUserByTypeID(LearnerVM pEntLearner)
        {
            Learner learner = new Learner();
            learner = _learnerdam.GetUserByTypeID(_mapper.Map<Learner>(pEntLearner));

            if (learner != null)
            {
                return Ok(new { Learner = learner }); // Sends a JSON response with status code 200
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("getuserdetailsbytypeid")]
        [Authorize]
        public async Task<IActionResult> GetUserDetailsByTypeID(LearnerVM pEntLearner)
        {
            Learner learner = new Learner();
            learner = _learnerdam.GetUserDetailsByTypeID(_mapper.Map<Learner>(pEntLearner));

            if (learner != null)
            {
                return Ok(new { Learner = learner }); // Sends a JSON response with status code 200
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("findlearners")]
        [Authorize]
        public async Task<IActionResult> FindLearners(Search pEntSearch)
        {
            List<Learner> learnerlist = new List<Learner>();

            learnerlist = _learnerdam.FindLearners(pEntSearch);
            if (learnerlist != null)
            {
                return Ok(new { LearnerList = learnerlist }); // Sends a JSON response with status code 200
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("findselfregistration")]
        public async Task<IActionResult> FindSelfRegistration(Search pEntSearch)
        {
            List<Learner> learnerlist = _learnerdam.FindSelfRegistration(pEntSearch);

            if (learnerlist != null)
            {
                return Ok(new { LearnerList = learnerlist }); // Sends a JSON response with status code 200
            }
            else
            {
                return BadRequest();
            } 
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> GetUserByLogin(LearnerVM pEntLearner)
        {
            try
            {
                Learner learner = _learnerdam.GetUserByLogin(_mapper.Map<Learner>(pEntLearner));

                if (learner.UserNameAlias != null)
                {
                    if (learner.IsActive)
                    {
                        var token = JwtFactory.GenerateJwtToken(learner.UserNameAlias,
                                    _config["Jwt:SecretKey"],
                                    _config["Jwt:Issuer"],
                                    _config["Jwt:Audience"],
                                    pEntLearner.ClientId);

                            pEntLearner.Token= token;
                            pEntLearner.SystemUserGUID = learner.ID;
                            _learnerdam.SaveTokenToDB(_mapper.Map<Learner>(pEntLearner));
                            return Ok(new { Learner = learner, Msg = "Login Successfully", Token = token }); 
                    }
                    else
                    {
                        return BadRequest(new { code = 400, msg = "User is inactive." });
                    }
                }
                else
                {
                    return NotFound(new { Msg = "Invalid loginId or password" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("getUserSystemGUID")]
        [Authorize]
        public async Task<IActionResult> GetUserSystemGUID(LearnerVM pEntLearner)
        {
           
            Learner learner = _learnerdam.GetUserSystemGUID(_mapper.Map<Learner>(pEntLearner));

            if (learner.ID != null)
            {
                return Ok(new { Learner = learner }); // Sends a JSON response with status code 200
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("getIsUserLock")]
        [Authorize]
        public async Task<IActionResult> GetIsUserLock(LearnerVM pEntLearner)
        {
           
            Learner learner = _learnerdam.GetIsUserLock(_mapper.Map<Learner>(pEntLearner));

            if (learner.ID != null)
            {
                return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
            }
            else
            {
                return BadRequest();
            } 
        }

        [HttpPost]
        [Route("getOTPNumber")]
        public async Task<IActionResult> GetOTPNumber(LearnerVM pEntLearner)
        {
            
            Learner learner = _learnerdam.GetOTPNumber(_mapper.Map<Learner>(pEntLearner));

            if (learner.OTPNumber != null)
            {
                return Ok(new { Learner = learner }); // Sends a JSON response with status code 200
            }
            else
            {
                return BadRequest();
            } 
        }

        [HttpPost]
        [Route("getuserbyaliasforgotpassword")]
        public async Task<IActionResult> GetUserByAliasForgotPassword(LearnerVM pEntLearner)
        {
            PasswordPolicyAdaptor objPasswordPolicyAdaptor = new PasswordPolicyAdaptor();
            PasswordPolicyConfiguration entPasswordPolicyConfiguration = new PasswordPolicyConfiguration();
            entPasswordPolicyConfiguration.ClientId = pEntLearner.ClientId;
            Dictionary<string, string> dictResult = new Dictionary<string, string>();
            string strMessage = string.Empty;
            Learner learner = new Learner();
            if (string.IsNullOrEmpty(pEntLearner.UserNameAlias))
            {
                strMessage = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.User.BLANK_USERID);
                dictResult.Add("Type", "Error");
                dictResult.Add("Message", strMessage);
                return BadRequest(new { Code = 404,Msg = dictResult});
            }
            Learner _entLearner = _learnerdam.GetIsUserLock(_mapper.Map<Learner>(pEntLearner));

            if ((_entLearner != null) && (!String.IsNullOrEmpty(_entLearner.ID)) && (_entLearner.IsUserLock))
            {
                if (string.IsNullOrEmpty(_entLearner.EmailID))
                {
                    strMessage = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.OTP.LEARNER_EMAIL_NOTEXIST);
                    dictResult.Add("Type", "Error");
                    dictResult.Add("Message", strMessage);
                    return BadRequest(new { Code = 404, Msg = dictResult });
                }
                else
                {
                    strMessage = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.OTP.USER_LOCK);
                    dictResult.Add("Type", "Error");
                    dictResult.Add("Message", strMessage);
                    return BadRequest(new { Code = 404, Msg = dictResult });
                }
            }
            else
            {
                entPasswordPolicyConfiguration = objPasswordPolicyAdaptor.GetPasswordPolicyById(entPasswordPolicyConfiguration);

                if (entPasswordPolicyConfiguration.IsPasswordChange == true)
                {
                    pEntLearner.UserPassword = PasswordCreator.CreatePassword(pEntLearner.ClientId);
                    learner = _learnerdam.GetUserByAliasForgotPassword(_mapper.Map<Learner>(pEntLearner), "UpdatePassword");
                }
                else
                {
                    learner = _learnerdam.GetUserByAliasForgotPassword(_mapper.Map<Learner>(pEntLearner), "");
                }
                entPasswordPolicyConfiguration.LearnerId = learner.ID;
                
                entPasswordPolicyConfiguration = objPasswordPolicyAdaptor.AddUpdateEmailRequests(entPasswordPolicyConfiguration);
                if (entPasswordPolicyConfiguration.NoOfCurrentEmailRequest != 0)
                {
                    if (learner == null || string.IsNullOrEmpty(learner.ID))
                    {
                        strMessage = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.User.INVALID_USERID_ForgotPassword);
                        dictResult.Add("Type", "Error");
                        dictResult.Add("Message", strMessage);
                        return BadRequest(new { Code = 404, Msg = dictResult });
                    }

                    if (string.IsNullOrEmpty(learner.EmailID))
                    {
                        strMessage = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.User.EMAIL_ID_NOT_AVAIL_CONTACT_SITE_ADMIN);
                        dictResult.Add("Type", "Error");
                        dictResult.Add("Message", strMessage);
                        return BadRequest(new { Code = 404, Msg = dictResult });
                    }

                    if (!ValidationManager.ValidateString(learner.EmailID, ValidationManager.DataType.EmailID))
                    {
                        strMessage = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.User.INVALID_EMAIL_ID);
                        dictResult.Add("Type", "Error");
                        dictResult.Add("Message", strMessage);
                        return BadRequest(new { Code = 404, Msg = dictResult });
                    }

                    API.Entity.AutoEmailTemplateSetting entAutoEmail = new API.Entity.AutoEmailTemplateSetting();
                    AutoEmailTemplateSettingManager entAutoEmailTemplateSettingManager = new AutoEmailTemplateSettingManager();

                    entAutoEmail.ID = API.Entity.AutoEmailTemplateSetting.EVENT_LEARNER_FORGOT_PASSWORD;
                    entAutoEmail.ClientId = pEntLearner.ClientId;
                    entAutoEmail = entAutoEmailTemplateSettingManager.Execute(entAutoEmail, API.Entity.AutoEmailTemplateSetting.Method.GetEmailTempId);
                    if ((entAutoEmail != null) && (!string.IsNullOrEmpty(entAutoEmail.EmailTemplateID)))
                    {
                        API.DataAccessManager.EmailMessages mailMessage = new API.DataAccessManager.EmailMessages(pEntLearner.ClientId);
                        mailMessage.SendPersonalizedMail(learner, entAutoEmail.EmailTemplateID, null,
                                                          learner.DefaultLanguageId, null,
                                                         null, null, false, null);//_learner.DefaultLanguageId
                    }

                    strMessage = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.User.PASSWORD_SEND);

                    //dictResult.Add("Type", "Success");
                    //dictResult.Add("Message", strMessage);
                    //return Ok(new { Code = 404, Msg = dictResult });
                    return Ok(new { Code = 200, Msg = "Passowrod sent to your email."});
                }
                else
                {
                    strMessage = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.User.INVALID_USERID_ForgotPassword);
                    return BadRequest(new { Code = 404, Msg = "Login ID does not exist. Please enter valid Login ID." });
                    //dictResult.Add("Type", "Error");
                    //dictResult.Add("Message", strMessage);
                    //return BadRequest(new { Code = 404, Msg = dictResult });
                }
            }            

            //if (learner.ID != null)
            //{
            //    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
            //}
            //else
            //{
            //    return NotFound(new {Code = 404, Status = "No data found"});
            //}
        }

        [HttpPost]
        [Route("getactiveuseremail")]
        [Authorize]
        public async Task<IActionResult> GetActiveUserEmail(LearnerVM pEntLearner)
        {
            try
            {
                Learner learner = _learnerdam.GetActiveUserEmail(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }

        [HttpPost]
        [Route("getusersnotinrole")]
        [Authorize]
        public async Task<IActionResult> FindUsersNotInRole(Search pEntSearch)
        {
            try
            {
                List<Learner> learnerlist = _learnerdam.FindLearnersForRoleAssignment(pEntSearch, false);

                if (learnerlist != null)
                {
                    return Ok(new { LearnerList = learnerlist, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("getusersinrole")]
        [Authorize]
        public async Task<IActionResult> FindUsersInRole(Search pEntSearch)
        {
            try
            {
                List<Learner> learnerlist = _learnerdam.FindLearnersForRoleAssignment(pEntSearch, true);

                if (learnerlist != null)
                {
                    return Ok(new { LearnerList = learnerlist, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("getbussinessruleusers")]
        [Authorize]
        public async Task<IActionResult> FindBussinessRuleUsers(Search pEntSearch)
        {
            try
            {
                List<Learner> learnerlist = _learnerdam.FindBussinessRuleUsers(pEntSearch);

                if (learnerlist != null)
                {
                    return Ok(new { LearnerList = learnerlist, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("getlearnersforassignment")]
        [Authorize]
        public async Task<IActionResult> FindLearnersForAssignment(Search pEntSearch)
        {
            try
            {
                List<Learner> learnerlist = _learnerdam.FindLearnersForAssignment(pEntSearch);

                if (learnerlist != null && learnerlist.Count>0)
                {
                    return Ok(new { LearnerList = learnerlist, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("getlearnersforassignmentoptimized")]
        [Authorize]
        public async Task<IActionResult> FindLearnersForAssignmentOptimized(Search pEntSearch)
        {
            try
            {
                List<Learner> learnerlist = _learnerdam.FindLearnersForAssignmentOptimized(pEntSearch);

                if (learnerlist != null)
                {
                    return Ok(new { LearnerList = learnerlist, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("getlearnersforunassignment")]
        [Authorize]
        public async Task<IActionResult> FindLearnersForUnAssignment(Search pEntSearch)
        {
            try
            {
                List<Learner> learnerlist = _learnerdam.FindLearnersForUnAssignment(pEntSearch);

                if (learnerlist != null)
                {
                    return Ok(new { LearnerList = learnerlist, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("getlearnersforunassignmentoptimized")]
        [Authorize]
        public async Task<IActionResult> FindLearnersForUnAssignmentOptimized(Search pEntSearch)
        {
            try
            {
                List<Learner> learnerlist = _learnerdam.FindLearnersForUnAssignmentOptimized(pEntSearch);

                if (learnerlist != null)
                {
                    return Ok(new { LearnerList = learnerlist, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("searchlearners")]
        [Authorize]
        public async Task<IActionResult> SearchLearners(SearchVM pEntSearch)
        {
            try
            {
                var regex = new Regex(@"^[^'<>&]+$");

                if(!(String.IsNullOrEmpty(pEntSearch.KeyWord))) 
                {
                    if (!regex.IsMatch(pEntSearch.KeyWord))
                    {
                        return BadRequest(new { Code = 400, Msg = "Invalid characters." });
                    }
                }

                if (pEntSearch.UserCriteria != null) 
                {
                    if (!(String.IsNullOrEmpty(pEntSearch.UserCriteria.FirstName)))
                    { 
                        if (!regex.IsMatch(pEntSearch.UserCriteria.FirstName))
                        {
                            return BadRequest(new { Code = 400, Msg = "Invalid characters in first name." });
                        }
                    }

                    if (!(String.IsNullOrEmpty(pEntSearch.UserCriteria.LastName)))
                    {
                        if (!regex.IsMatch(pEntSearch.UserCriteria.LastName))
                        {
                            return BadRequest(new { Code = 400, Msg = "Invalid characters in last name." });
                        }
                    }

                    if (!(String.IsNullOrEmpty(pEntSearch.UserCriteria.Email)))
                    {
                        if (!regex.IsMatch(pEntSearch.UserCriteria.Email))
                        {
                            return BadRequest(new { Code = 400, Msg = "Invalid characters in Email." });
                        }
                    }
                }

                List<Learner> learnerlist = _learnerdam.SearchLearners(_mapper.Map<Search>( pEntSearch));

                if (learnerlist != null && learnerlist.Count>0)
                {
                    return Ok(new { LearnerList = learnerlist, Code = 200, TotalRows = learnerlist[0].ListRange.TotalRows }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("getuseraprovedstudent")]
        [Authorize]
        public async Task<IActionResult> GetUserAprovedStudent(Learner pEntLearner)
        {
            try
            {
                Learner learner = _learnerdam.GetUserAprovedStudent(pEntLearner);

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("getlearnersforiperform")]
        [Authorize]
        public async Task<IActionResult> SearchLearners_ForIPerform(Search pEntSearch)
        {
            try
            {
                List<Learner> learnerlist = _learnerdam.SearchLearners_ForIPerform(pEntSearch);

                if (learnerlist != null)
                {
                    return Ok(new { LearnerList = learnerlist, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("updatedateformat")]
        [Authorize]
        public async Task<IActionResult> UpdateDateFormat(LearnerVM pEntLearner)
        {
            try
            {
                Learner learner = _learnerdam.UpdateDateFormat(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("getuserbyalias")]
        [Authorize]
        public async Task<IActionResult> GetUserByAlias(LearnerVM pEntLearner)
        {
            try
            {
                Learner learner = _learnerdam.GetUserByAlias(_mapper.Map<Learner>(pEntLearner));

                if (learner.ID != null)
                {
                    return Ok(new { Learner = learner, Code = 200, Msg = "Username is available" }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "Username is not available" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("getuserbyaliasilt")]
        [Authorize]
        public async Task<IActionResult> GetUserByAliasILT(LearnerVM pEntLearner)
        {
            try
            {
                Learner learner = _learnerdam.GetUserByAliasILT(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("updatetermsandcondition")]
        [Authorize]
        public async Task<IActionResult> UpdateTermsAndCondition(LearnerVM pEntLearner)
        {
            try
            {
                Learner learner = _learnerdam.UpdateTermsAndCondition(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("checkuserbyalias")]
        [Authorize]
        public async Task<IActionResult> CheckUserByAlias(LearnerVM pEntLearner)
        {
            try
            {                
                Learner learner = _learnerdam.CheckUserByAlias(_mapper.Map<Learner>(pEntLearner));
                //Learner learner = _learnerdam.IsLoginIdExists(_mapper.Map<Learner>(pEntLearner));

                if (learner.ID != null)
                {
                    //
                    return NotFound(new { Code = 404, Msg = "Login Id already exists. Please choose a different one." });
                    
                }
                else
                {
                    //
                    return Ok(new { Learner = learner, Code = 200, Msg = "Login Id is available." }); // Sends a JSON response with status code 200
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("getuserbyemailupdateprofile")]
        [Authorize]
        public async Task<IActionResult> GetUserByEmailUpdateProfile(LearnerVM pEntLearner)
        {
            try
            {
                Learner learner = _learnerdam.GetUserByEmailUpdateProfile(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("getuserbysystemuserguid")]
        [Authorize]
        public async Task<IActionResult> GetUserBySystemUserGUID(LearnerVM pEntLearner)
        {
            try
            {
                Learner learner = _learnerdam.GetUserBySystemUserGUID(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("updateuser")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(LearnerVM pEntLearner)
        {
            try
            {
                Learner learner = _learnerdam.UpdateUser(_mapper.Map<Learner>(pEntLearner), false);
                if (Convert.ToBoolean(pEntLearner.IsSendEmail))
                {
                  string  strEmailMsg = _userPageManager.SendEmail(pEntLearner.UserNameAlias, pEntLearner.UserNameAlias + " Updated", _mapper.Map<Learner>(pEntLearner));
                    if (!string.IsNullOrEmpty(strEmailMsg))
                    {
                        strEmailMsg = " Failed to configure/send email. " + strEmailMsg;
                        return NotFound(new { Code = 404, Msg = strEmailMsg });
                    }
                }

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200, Msg = ImportUsersBL.GetMessage("User updated successfully", API.YPLMS.Services.Messages.User.USER_UPDATED, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID)
                }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }           
            catch (Exception ex)
            {
                if (ex.Message == "Email ID already exists for another user.")
                {
                    return BadRequest(new { msg = ex.Message });
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("updateuserregistatus")]
        [Authorize]
        public async Task<IActionResult> UpdateUserRegiStatus(LearnerVM pEntLearner)
        {
            try
            {
                Learner learner = _learnerdam.UpdateUserRegiStatus(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("UpdateAllUserStatus")]
        [Authorize]
        public async Task<IActionResult> UpdateAllUserStatus(LearnerVM pEntLearner)
        {
            try
            {
                Learner learner = _learnerdam.UpdateAllUserStatus(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("registeruser")]
        [Authorize]
        public async Task<IActionResult> RegisterUser(LearnerVM pEntLearner)
        {
            try
            {
                if (string.IsNullOrEmpty(pEntLearner.UserPassword))
                {
                    pEntLearner.UserPassword = PasswordCreator.CreatePassword(pEntLearner.ClientId);
                }
                else
                {
                    PasswordPolicyConfiguration _entPasswordPolicy = ImportUsersBL.GetPasswordPolicyConfiguration(pEntLearner.ClientId);
                    string strValidatePassword = ImportUsersBL.ValidatePasswordPolicy(_entPasswordPolicy, pEntLearner.ClientId, pEntLearner.ID, pEntLearner.UserPassword.Trim(), "en-US");
                    if (strValidatePassword == string.Empty)
                    {
                        pEntLearner.UserPassword = pEntLearner.UserPassword;
                    }
                    else
                    {
                        strValidatePassword = strValidatePassword.Replace("<br />", "\\n");
                        return BadRequest(new { code = 400, Msg = ImportUsersBL.GetMessage("Password does not meet the Password Policy!", API.YPLMS.Services.Messages.User.PASS_NOT_POLICY, null) });

                    }
                }
                pEntLearner.DateOfRegistration = Convert.ToDateTime(DateTime.UtcNow);
                pEntLearner.UserTypeId = "Learner";
                pEntLearner.IsActive = true;
                pEntLearner.IsFirstLogin = true;
                Learner learner = _learnerdam.AddUser(_mapper.Map<Learner>(pEntLearner), false);               

                if (learner.ID != null)
                {
                    return Ok(new
                    {
                        Learner = learner,
                        Code = 200,
                        Msg = ImportUsersBL.GetMessage("The user added successfully", API.YPLMS.Services.Messages.User.ADD_SUCCESS, null)
                    }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "User Not Added" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("adduser")]
        [Authorize]
        public async Task<IActionResult> AddUser(LearnerVM pEntLearner)
        {
            try
            {
                var regex = new Regex(@"^[^'<>&]+$");
                var emailRegex = new Regex(@"^[^@]+@[^@]+\.[^@]+$");

                // First Name Validation
                if (string.IsNullOrWhiteSpace(pEntLearner.FirstName))                                    
                    return BadRequest(new { code = 400, msg = "First name is required." });                
                else if (!regex.IsMatch(pEntLearner.FirstName))                
                    return BadRequest(new { Code = 400, Msg = "Invalid characters in first name." });

                // Last Name Validation
                if (string.IsNullOrWhiteSpace(pEntLearner.LastName))                
                    return BadRequest(new { code = 400, msg = "Last name is required." });                
                else if (!regex.IsMatch(pEntLearner.LastName))                
                    return BadRequest(new { code = 400, msg = "Invalid characters in last name." });
                

                // Login ID Validation
                if (string.IsNullOrWhiteSpace(pEntLearner.UserNameAlias))
                    return BadRequest(new { code = 400, msg = "Login ID is required." });
                else if (!regex.IsMatch(pEntLearner.UserNameAlias))
                    return BadRequest(new { code = 400, msg = "Invalid characters in login ID." });

                // Check if LoginId already exists
                bool loginExists = _learnerdam.IsLoginIdExists(_mapper.Map<Learner>(pEntLearner));
                if (loginExists)
                {
                    return BadRequest(new { code = 400, msg = "Login ID already exists. Please choose a different one." });
                }

                // Email Validation
                if (string.IsNullOrWhiteSpace(pEntLearner.EmailID))
                    return BadRequest(new { code = 400, msg = "Email ID is required." });
                else if (!emailRegex.IsMatch(pEntLearner.EmailID))
                    return BadRequest(new { code = 400, msg = "Invalid characters in email ID." });

                // Check if EmailId already exists
                bool existingUserByEmail = _learnerdam.CheckEmailIdExists(_mapper.Map<Learner>(pEntLearner));
                if (existingUserByEmail)
                {
                    return BadRequest(new { code = 400, msg = "Email ID already exists. Please use a different email address." });
                }

                if (pEntLearner.FlagAddUserPage == true)
                {

                    // Manager Email Validation
                    if (string.IsNullOrWhiteSpace(pEntLearner.ManagerEmailId))
                        return BadRequest(new { code = 400, msg = "Manager Email ID is required." });
                    else if (!emailRegex.IsMatch(pEntLearner.ManagerEmailId))
                        return BadRequest(new { code = 400, msg = "Invalid manager email ID." });

                    // Manager User Password
                    //if (string.IsNullOrWhiteSpace(pEntLearner.UserPassword))
                    //    return BadRequest(new { code = 400, msg = "UserPassword is required." });
                    if (!regex.IsMatch(pEntLearner.UserPassword))
                        return BadRequest(new { code = 400, msg = "Invalid UserPassword." });

                }

                if (string.IsNullOrEmpty(pEntLearner.UserPassword))
                {
                    pEntLearner.UserPassword = PasswordCreator.CreatePassword(pEntLearner.ClientId);
                }
                else
                {
                    PasswordPolicyConfiguration _entPasswordPolicy  = ImportUsersBL.GetPasswordPolicyConfiguration(pEntLearner.ClientId);
                    string strValidatePassword = ImportUsersBL.ValidatePasswordPolicy(_entPasswordPolicy, pEntLearner.ClientId, pEntLearner.ID, pEntLearner.UserPassword.Trim(), "en-US");
                    if (strValidatePassword == string.Empty)
                    {
                        pEntLearner.UserPassword = pEntLearner.UserPassword;
                    }
                    else
                    {
                        strValidatePassword = strValidatePassword.Replace("<br />", "\\n");
                       return BadRequest(new { code = 400, Msg = ImportUsersBL.GetMessage("Password does not meet the Password Policy!", API.YPLMS.Services.Messages.User.PASS_NOT_POLICY, null) });
                        
                    }
                }
                pEntLearner.DateOfRegistration = Convert.ToDateTime(DateTime.UtcNow);
                pEntLearner.UserTypeId = "Learner";
                pEntLearner.IsActive = true;
                pEntLearner.IsFirstLogin = true;
                Learner learner = _learnerdam.AddUser(_mapper.Map<Learner>(pEntLearner), false);
                if (Convert.ToBoolean(pEntLearner.IsSendEmail))
                {
                  string  strEmailMsg = _userPageManager.SendEmail(pEntLearner.UserNameAlias, pEntLearner.UserNameAlias + " Added", _mapper.Map<Learner>(pEntLearner));
                    if (!string.IsNullOrEmpty(strEmailMsg))
                    {
                        strEmailMsg = " Failed to configure/send email. " + strEmailMsg;
                        return NotFound(new { Code = 404, Msg = strEmailMsg });
                    }
                }

                if (learner.ID != null)
                {
                    return Ok(new { Learner = learner, Code = 200, Msg = ImportUsersBL.GetMessage("The user added successfully", API.YPLMS.Services.Messages.User.ADD_SUCCESS, null)
                }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "User Not Added" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        [Route("addusergetclientfields")]
        [Authorize]
        public async Task<IActionResult> AddUserGetClientFields(ImportDefinationVM pEntImportDefination)
        {
            try
            {
                //check git changes
                List<ImportDefination> entListImportDefination = new List<ImportDefination>();
                List<ImportDefination> entListImportDefinationObj = new List<ImportDefination>();
                ImportDefination importdefination = new ImportDefination();
                EntityRange _entRange = new EntityRange();                
                pEntImportDefination.ImportAction = ImportAction.None;
                if (pEntImportDefination.ListRange == null)
                {
                    _entRange.PageIndex = 0;
                    _entRange.PageSize = 0;
                    _entRange.SortExpression = "MinLength";
                    pEntImportDefination.ListRange = _entRange;
                }
               
                entListImportDefination = _importDefinitionAdaptor.GetImportDefinationList(_mapper.Map<ImportDefination>(pEntImportDefination));
                foreach (var impdef in entListImportDefination)
                {
                    if (impdef.ID.Trim() == "LoginId" || impdef.ID.Trim() == "UserNameAlias")
                    {
                        impdef.lengthError = ImportUsersBL.GetMessage("Entered text length must be in between ( to )", API.YPLMS.Services.Messages.User.OUT_OF_LENGTH, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                        impdef.lengthError = impdef.lengthError.Replace(Convert.ToString("("), Convert.ToString(impdef.MinLength));
                        impdef.lengthError = impdef.lengthError.Replace(Convert.ToString(")"), Convert.ToString(impdef.MaxLength));
                        impdef.OtherError = ImportUsersBL.GetMessage("Enter Login Id", API.YPLMS.Services.Messages.User.ENT_USER_NAME_ALIAS, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                    }
                    if (impdef.ID.Trim() == "FirstName")
                    {
                        impdef.lengthError = ImportUsersBL.GetMessage("Entered text length must be in between ( to )", API.YPLMS.Services.Messages.User.OUT_OF_LENGTH, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                        impdef.lengthError = impdef.lengthError.Replace(Convert.ToString("("), Convert.ToString(impdef.MinLength));
                        impdef.lengthError = impdef.lengthError.Replace(Convert.ToString(")"), Convert.ToString(impdef.MaxLength));
                        impdef.OtherError = ImportUsersBL.GetMessage("Enter first name", API.YPLMS.Services.Messages.User.ENTER_FIRST_NAME, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                    }
                    if (impdef.ID.Trim() == "LastName")
                    {
                        impdef.lengthError = ImportUsersBL.GetMessage("Entered text length must be in between ( to )", API.YPLMS.Services.Messages.User.OUT_OF_LENGTH, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                        impdef.lengthError = impdef.lengthError.Replace(Convert.ToString("("), Convert.ToString(impdef.MinLength));
                        impdef.lengthError = impdef.lengthError.Replace(Convert.ToString(")"), Convert.ToString(impdef.MaxLength));
                        impdef.OtherError = ImportUsersBL.GetMessage("Enter last name", API.YPLMS.Services.Messages.User.ENTER_LAST_NAME, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                    }
                    if (impdef.ID.Trim() == "EmailID")
                    {
                        impdef.lengthError = ImportUsersBL.GetMessage("Entered text length must be in between ( to )", API.YPLMS.Services.Messages.User.OUT_OF_LENGTH, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                        impdef.lengthError = impdef.lengthError.Replace(Convert.ToString("("), Convert.ToString(impdef.MinLength));
                        impdef.lengthError = impdef.lengthError.Replace(Convert.ToString(")"), Convert.ToString(impdef.MaxLength));
                        impdef.OtherError = ImportUsersBL.GetMessage("Enter email address", API.YPLMS.Services.Messages.User.ENTER_EMAIL_ADDRESS, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                    }
                    if (impdef.ID.Trim() == "UserPassword")
                    {
                        impdef.lengthError = ImportUsersBL.GetMessage("Entered text length must be in between ( to )", API.YPLMS.Services.Messages.User.OUT_OF_LENGTH, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                        impdef.lengthError = impdef.lengthError.Replace(Convert.ToString("("), Convert.ToString(impdef.MinLength));
                        impdef.lengthError = impdef.lengthError.Replace(Convert.ToString(")"), Convert.ToString(impdef.MaxLength));
                        impdef.OtherError = ImportUsersBL.GetMessage("Enter password", API.YPLMS.Services.Messages.User.ENTER_PASSWORD, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                    }
                    if (impdef.ID.Trim() == "ManagerEmailId")
                    {
                        impdef.lengthError = ImportUsersBL.GetMessage("Entered text length must be in between ( to )", API.YPLMS.Services.Messages.User.OUT_OF_LENGTH, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                        impdef.lengthError = impdef.lengthError.Replace(Convert.ToString("("), Convert.ToString(impdef.MinLength));
                        impdef.lengthError = impdef.lengthError.Replace(Convert.ToString(")"), Convert.ToString(impdef.MaxLength));
                        impdef.OtherError = ImportUsersBL.GetMessage("Enter email address", API.YPLMS.Services.Messages.User.ENTER_MANAGER_EMAIL_ID, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                    }
                    if (impdef.FieldName.Trim() == "DefaultLanguageId")
                    {
                        impdef.lengthError = ImportUsersBL.GetMessage("Entered text length must be in between ( to )", API.YPLMS.Services.Messages.User.OUT_OF_LENGTH, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                        impdef.lengthError = impdef.lengthError.Replace(Convert.ToString("("), Convert.ToString(impdef.MinLength));
                        impdef.lengthError = impdef.lengthError.Replace(Convert.ToString(")"), Convert.ToString(impdef.MaxLength));
                        impdef.OtherError = ImportUsersBL.GetMessage("Enter Default Language", API.YPLMS.Services.Messages.Client.SEL_PREFERRED_LANG, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                    }
                    if (impdef.FieldName.Trim() == "IsActive")
                    {
                        impdef.lengthError = ImportUsersBL.GetMessage("Entered text length must be in between ( to )", API.YPLMS.Services.Messages.User.OUT_OF_LENGTH, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                        impdef.lengthError = impdef.lengthError.Replace(Convert.ToString("("), Convert.ToString(impdef.MinLength));
                        impdef.lengthError = impdef.lengthError.Replace(Convert.ToString(")"), Convert.ToString(impdef.MaxLength));
                        impdef.OtherError = ImportUsersBL.GetMessage("select IsActive", API.YPLMS.Services.Messages.User.IS_ACTIVE, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                    }
                    entListImportDefinationObj.Add(impdef);
                }
                string rfvUserNameErrorMessage = string.Empty;
                string cvManagerEmailErrorMessage = string.Empty;
                string cvManagerNameErrorMessage = string.Empty;
                string rfvManagerEmailErrorMessage = string.Empty;
                string rfvManagerNameErrorMessage = string.Empty;
                string rfvDateOfRegistrationErrorMessage = string.Empty;
                string rvDateOfRegistrationErrorMessage = string.Empty;
                string rfvCurrentRegionViewErrorMessage = string.Empty;
                string rvDateOfTerminationErrorMessage = string.Empty;


              //  string strLelMsg = ImportUsersBL.GetMessage("Entered text length must be in between ( to )", API.YPLMS.Services.Messages.User.OUT_OF_LENGTH, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
              // string  cvFirstNameErrorMessage = strLelMsg;
              // string rfvFirstNameErrorMessage = ImportUsersBL.GetMessage("Enter first name", API.YPLMS.Services.Messages.User.ENTER_FIRST_NAME, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
              // string cvLastNameErrorMessage = strLelMsg;
              // string rfvLastNameErrorMessage = ImportUsersBL.GetMessage("Enter last name", API.YPLMS.Services.Messages.User.ENTER_LAST_NAME, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
              // string cvUserNameErrorMessage = strLelMsg;
                //if (pEntLearner.ClientId != _strBaseClientId)
                //{
                //  // rfvUserNameErrorMessage = ImportUsersBL.GetMessage("Enter Login Id", API.YPLMS.Services.Messages.User.ENT_USER_NAME_ALIAS, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                //   cvManagerEmailErrorMessage = strLelMsg;
                //   cvManagerNameErrorMessage = strLelMsg;
                //   rfvManagerEmailErrorMessage = ImportUsersBL.GetMessage("Enter email address", API.YPLMS.Services.Messages.User.ENTER_EMAIL_ADDRESS, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                //   rfvManagerNameErrorMessage = ImportUsersBL.GetMessage("Enter manager name", null, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                //   rfvDateOfRegistrationErrorMessage = ImportUsersBL.GetMessage("Enter hire date", API.YPLMS.Services.Messages.User.ENTER_HIRE_DATE, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                //   rvDateOfRegistrationErrorMessage = ImportUsersBL.GetMessage("Date range must be in between 1/1/1753 to 12/31/9999", API.YPLMS.Services.Messages.Common.DATE_OUT_OF_RANGE, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                //   rfvCurrentRegionViewErrorMessage = ImportUsersBL.GetMessage("Select regional view", API.YPLMS.Services.Messages.RegionView.SELECT_REGION_VIEW, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                //    //rfvDateOfTermination.ErrorMessage = ImportUsersBL.GetMessage("Enter termination date", Services.Messages.User.ENTER_TERM_DATE, Language.SYSTEM_DEFAULT_LANG_ID);
                //   rvDateOfTerminationErrorMessage = ImportUsersBL.GetMessage("Date range must be in between 1/1/1753 to 12/31/9999", API.YPLMS.Services.Messages.Common.DATE_OUT_OF_RANGE, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                //}
                //else
                //{
                //    rfvUserNameErrorMessage = ImportUsersBL.GetMessage("Enter Administrator Id", API.YPLMS.Services.Messages.User.ENTER_ADMIN_ID, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
                //}
              // string cvEmailErrorMessage = strLelMsg;
              // string rfvEmailErrorMessage = ImportUsersBL.GetMessage("Enter email address", API.YPLMS.Services.Messages.User.ENTER_EMAIL_ADDRESS, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
              // string rfvPasswordErrorMessage = ImportUsersBL.GetMessage("Enter password", API.YPLMS.Services.Messages.User.ENTER_PASSWORD, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
               //string rfvConfirmPasswordErrorMessage = ImportUsersBL.GetMessage("Enter confirm password", API.YPLMS.Services.Messages.User.ENTER_CONFIRM_PASSWORD, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);
               //string cvPasswordErrorMessage = ImportUsersBL.GetMessage("Password and Confirm Password must be same", API.YPLMS.Services.Messages.Common.PWD_NCONF_PWD_NOT_MATCH, API.Entity.Language.SYSTEM_DEFAULT_LANG_ID);


                if (entListImportDefinationObj != null && entListImportDefinationObj.Count>0)
                {
                    return Ok(new { ImportDefination = entListImportDefinationObj, Code = 200,
                        //cvFirstNameErrorMessage= cvFirstNameErrorMessage,
                        //rfvFirstNameErrorMessage = rfvFirstNameErrorMessage,
                        //cvLastNameErrorMessage = cvLastNameErrorMessage,
                        //rfvLastNameErrorMessage = rfvLastNameErrorMessage,
                        //cvUserNameErrorMessage = cvUserNameErrorMessage,
                        //rfvUserNameErrorMessage = rfvUserNameErrorMessage,
                        //cvManagerEmailErrorMessage = cvManagerEmailErrorMessage,
                        //cvManagerNameErrorMessage= cvManagerNameErrorMessage,
                        //rfvManagerEmailErrorMessage = rfvManagerEmailErrorMessage,
                        //rfvManagerNameErrorMessage = rfvManagerNameErrorMessage,
                        //rfvDateOfRegistrationErrorMessage = rfvDateOfRegistrationErrorMessage,
                        //rvDateOfRegistrationErrorMessage = rvDateOfRegistrationErrorMessage,
                        //rfvCurrentRegionViewErrorMessage = rfvCurrentRegionViewErrorMessage,
                        //rvDateOfTerminationErrorMessage = rvDateOfTerminationErrorMessage,                        
                        //cvEmailErrorMessage = cvEmailErrorMessage,
                        //rfvEmailErrorMessage = rfvEmailErrorMessage,
                        //rfvPasswordErrorMessage = rfvPasswordErrorMessage,
                        //rfvConfirmPasswordErrorMessage = rfvConfirmPasswordErrorMessage,
                        //cvPasswordErrorMessage = cvPasswordErrorMessage
                    }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("adduserimport")]
        [Authorize]
        public async Task<IActionResult> AddUserImport(LearnerVM pEntLearner)
        {
            try
            {
                if (string.IsNullOrEmpty(pEntLearner.UserPassword))
                {
                    pEntLearner.UserPassword = PasswordCreator.CreatePassword(pEntLearner.ClientId);
                }
                Learner learner = _learnerdam.AddUserImport(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("updateuserimport")]
        [Authorize]
        public async Task<IActionResult> UpdateUserImport(LearnerVM pEntLearner)
        {
            try
            {
              
                Learner learner = _learnerdam.UpdateUserImport(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("deleteuser")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(LearnerVM pEntLearner)
        {
            try
            {

                Learner learner = _learnerdam.DeleteUser(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("assignmanagers")]
        [Authorize]
        public async Task<IActionResult> AssignManagers(LearnerVM pEntLearner)
        {
            try
            {

                Learner learner = _learnerdam.AssignManagers(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("updatedbindexes")]
        [Authorize]
        public async Task<IActionResult> UpdateDBIndexes(LearnerVM pEntLearner)
        {
            try
            {

                Learner learner = _learnerdam.UpdateDBIndexes(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("synchbusinessruleusersonimport")]
        [Authorize]
        public async Task<IActionResult> SynchBusinessRuleUsersOnImport(LearnerVM pEntLearner)
        {
            try
            {

                Learner learner = _learnerdam.SynchBusinessRuleUsersOnImport(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("setuserinitialsettingonimport")]
        [Authorize]
        public async Task<IActionResult> SetUserInitialSettingOnImport(LearnerVM pEntLearner)
        {
            try
            {

                Learner learner = _learnerdam.SetUserInitialSettingOnImport(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("validateaddedit")]
        [Authorize]
        public async Task<IActionResult> ValidateImportUserAddEdit(LearnerVM pEntLearner)
        {
            try
            {

                Learner learner = _learnerdam.ValidateImportUser(_mapper.Map<Learner>(pEntLearner), ImportAction.Add_Edit);

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("validateadc")]
        [Authorize]
        public async Task<IActionResult> ValidateImportUserValidateADC(LearnerVM pEntLearner)
        {
            try
            {

                Learner learner = _learnerdam.ValidateImportUser(_mapper.Map<Learner>(pEntLearner), ImportAction.Activate);

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ValidateResetPassword")]
        [Authorize]
        public async Task<IActionResult> ValidateImportUserPasswordReset(LearnerVM pEntLearner)
        {
            try
            {

                Learner learner = _learnerdam.ValidateImportUser(_mapper.Map<Learner>(pEntLearner), ImportAction.PasswordReset);

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("updateuserfirstlogin")]
        [Authorize]
        public async Task<IActionResult> UpdateUserFirstLogin(LearnerVM pEntLearner)
        {
            try
            {

                Learner learner = _learnerdam.UpdateUserFirstLogin(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("updatelockunlockotp")]
        [Authorize]
        public async Task<IActionResult> UpdateLockUnlockOTP(LearnerVM pEntLearner)
        {
            try
            {

                Learner learner = _learnerdam.UpdateLockUnlockOTP(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("updateuserlanguage")]
        [Authorize]
        public async Task<IActionResult> UpdateUserLanguage(LearnerVM pEntLearner)
        {
            try
            {

                Learner learner = _learnerdam.UpdateUserLanguage(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("deleteselecteduser")]
        [Authorize]
        public async Task<IActionResult> DeleteSelectedUser(List<LearnerVM> pEntListBase)
        {
            try
            {
                List<Learner> entListUsers = new List<Learner>();
                 entListUsers = _learnerdam.DeleteSelectedUser(_mapper.Map<List<Learner>>(pEntListBase));

                if (entListUsers != null)
                {
                    return Ok(new { LearnerList = entListUsers, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("checknewpassword")]
        [Authorize]
        public async Task<IActionResult> ChecknewPwd(LearnerVM pEntLearner)
        {
            try
            {

                Learner learner = _learnerdam.ChecknewPwd(_mapper.Map<Learner>(pEntLearner));

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("checksetnewpassword")]
        [Authorize]
        public async Task<IActionResult> CheckSetNewPassword(LearnerVM pEntLearner)
        {
            try
            {
               string strMessage = string.Empty;
                Learner learner = _learnerdam.CheckSetNewPassword(_mapper.Map<Learner>(pEntLearner), out strMessage);

                if (learner != null)
                {
                    return Ok(new { Learner = learner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("bulkactivatedeactivate")]
        [Authorize]
        public async Task<IActionResult> BulkActivateDeactivate(List<LearnerVM> pEntListLearners)
        {
            try
            {
                List<Learner> entListLearner = new List<Learner>();
                entListLearner = _learnerdam.BulkActivateDeactivate(_mapper.Map<List<Learner>>(pEntListLearners));

                if (entListLearner != null)
                {
                    return Ok(new { LearnerList = entListLearner, Code = 200, Msg = "Users Activate/Deactivate Successfully" }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("bulkuserspasswordreset")]
        [Authorize]
        public async Task<IActionResult> BulkUsersPasswordReset(List<LearnerVM> pEntListLearners)
        {
            try
            {
                List<Learner> entListLearner = new List<Learner>();
                entListLearner = _learnerdam.BulkUsersPasswordReset(_mapper.Map<List<Learner>>(pEntListLearners));

                if (entListLearner != null)
                {
                    return Ok(new { LearnerList = entListLearner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("bulkchangeid")]
        [Authorize]
        public async Task<IActionResult> BulkChangeId(List<LearnerVM> pEntListLearners)
        {
            try
            {
                List<Learner> entListLearner = new List<Learner>();
                entListLearner = _learnerdam.BulkChangeId(_mapper.Map<List<Learner>>(pEntListLearners));

                if (entListLearner != null)
                {
                    return Ok(new { LearnerList = entListLearner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("findlearnersforallroleassignment")]
        [Authorize]
        public async Task<IActionResult> FindLearnersForAllRoleAssignment(Search pEntSearch)
        {
            try
            {
                List<Learner> entListLearner = new List<Learner>();
                entListLearner = _learnerdam.FindLearnersForAllRoleAssignment(pEntSearch,true);

                if (entListLearner != null)
                {
                    return Ok(new { LearnerList = entListLearner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("getdynamicuserlist")]
        [Authorize]
        public async Task<IActionResult> GetDynamicUserList(LearnerVM pEntLearner)
        {
            try
            {
                List<Learner> entListLearner = new List<Learner>();
                entListLearner = _learnerdam.GetDynamicUserList(_mapper.Map<Learner>(pEntLearner));

                if (entListLearner != null)
                {
                    return Ok(new { LearnerList = entListLearner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("getonetimeuserlist")]
        [Authorize]
        public async Task<IActionResult> GetOneTimeUserList(LearnerVM pEntLearner)
        {
            try
            {
                List<Learner> entListLearner = new List<Learner>();
                entListLearner = _learnerdam.GetOneTimeUserList(_mapper.Map<Learner>(pEntLearner));

                if (entListLearner != null)
                {
                    return Ok(new { LearnerList = entListLearner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("getbulkimport")]
        [Authorize]
        public async Task<IActionResult> GetBulkImport(LearnerVM pEntLearner)
        {
            try
            {
                List<Learner> entListLearner = new List<Learner>();
                entListLearner = _learnerdam.GetBulkImport(_mapper.Map<Learner>(pEntLearner));

                if (entListLearner != null)
                {
                    return Ok(new { LearnerList = entListLearner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("getuserscope")]
        [Authorize]
        public async Task<IActionResult> GetUserScope(LearnerVM pEntLearner)
        {
            try
            {
                Learner entLearner = new Learner();
                entLearner = _learnerdam.GetUserScope(_mapper.Map<Learner>(pEntLearner));

                if (entLearner != null)
                {
                    return Ok(new { Learner = entLearner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("getlearnersforassessmentcourse")]
        [Authorize]
        public async Task<IActionResult> GetLearnersForAssessmentCourse(LearnerVM pEntLearner)
        {
            try
            {
                List<Learner> entListLearner = new List<Learner>();
                entListLearner = _learnerdam.GetLearnersForAssessmentCourse(_mapper.Map<Learner>(pEntLearner));

                if (entListLearner != null)
                {
                    return Ok(new { LearnerList = entListLearner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("findlearnersforunlockassignment")]
        [Authorize]
        public async Task<IActionResult> FindLearnersForUnlockAssignment(Search pEntSearch)
        {
            try
            {
                List<Learner> entListLearner = new List<Learner>();
                entListLearner = _learnerdam.FindLearnersForUnlockAssignment(pEntSearch);

                if (entListLearner != null)
                {
                    return Ok(new { LearnerList = entListLearner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("findlearnersforsession")]
        [Authorize]
        public async Task<IActionResult> FindLearnersForSession(Search pEntSearch)
        {
            try
            {
                List<Learner> entListLearner = new List<Learner>();
                entListLearner = _learnerdam.FindLearnersForSession(pEntSearch);

                if (entListLearner != null)
                {
                    return Ok(new { LearnerList = entListLearner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("addsubscriptionofuserfornewsletter")]
        [Authorize]
        public async Task<IActionResult> AddSubscriptionOfUserForNewsLetter(LearnerVM pEntLearner)
        {
            try
            {
                Learner entLearner = new Learner();
                entLearner = _learnerdam.AddSubscriptionOfUserForNewsLetter(_mapper.Map<Learner>(pEntLearner));

                if (entLearner != null)
                {
                    return Ok(new { LearnerList = entLearner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("searchregionaladmins")]
        [Authorize]
        public async Task<IActionResult> SearchRegionalAdmins(Search pEntSearch)
        {
            try
            {
                List<Learner> entListLearner = new List<Learner>();
                entListLearner = _learnerdam.SearchRegionalAdmins(pEntSearch);

                if (entListLearner != null)
                {
                    return Ok(new { LearnerList = entListLearner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("getallusersubscribefornewsletter")]
        [Authorize]
        public async Task<IActionResult> GetAllUserSubscribeForNewsLetter(LearnerVM pEntLearner)
        {
            try
            {
                List<Learner> entListLearner = new List<Learner>();
                entListLearner = _learnerdam.GetAllUserSubscribeForNewsLetter(_mapper.Map<Learner>(pEntLearner));

                if (entListLearner != null)
                {
                    return Ok(new { LearnerList = entListLearner, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("updateusercustomfield")]
        [Authorize]
        public async Task<IActionResult> UpdateUserCustomField(LearnerVM pEntLearner)
        {
            try
            {
                
                bool result= _learnerdam.UpdateUserCustomField(_mapper.Map<Learner>(pEntLearner));

                if (result ==true)
                {
                    return Ok(new {  Code = 200, Msg="CustomField updated successfully" }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "Something went wrong" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        
    }
}
