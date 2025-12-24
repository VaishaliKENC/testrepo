using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data;
using System.Diagnostics;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GroupRuleController : ControllerBase
    {
        private readonly IGroupRuleAdaptor<GroupRule> _groupRuleAdaptor;
        private readonly IMapper _mapper;
        private readonly IBusinessRuleUsersAdaptor<BusinessRuleUsers> _businessRuleUsersAdaptor;
        private readonly ILanguageDAM<Language> _languageDAM;
        private readonly ILearnerDAM<Learner> _learnerdam;
        public GroupRuleController(IGroupRuleAdaptor<GroupRule> groupRuleAdaptor, IMapper mapper, IBusinessRuleUsersAdaptor<BusinessRuleUsers> businessRuleUsersAdaptor, ILanguageDAM<Language> languageDAM, ILearnerDAM<Learner> learnerdam) 
        {
            _groupRuleAdaptor = groupRuleAdaptor;
            _mapper = mapper;
            _businessRuleUsersAdaptor = businessRuleUsersAdaptor;
            _languageDAM = languageDAM;
            _learnerdam = learnerdam;
        }

        [HttpPost]
        [Route("getgrouprulelist")]
        [Authorize]
        public async Task<IActionResult> GetGroupRuleList(GroupRuleVM pEntGroupRule)
        {
            try
            {
                List<GroupRule> entListRule = new List<GroupRule>();

                if (!string.IsNullOrEmpty(pEntGroupRule.RuleName))
                {
                    pEntGroupRule.RuleName = Convert.ToString(CommonMethods.RemoveSpecialChars(pEntGroupRule.RuleName).Trim());
                }


                entListRule = _groupRuleAdaptor.GetGroupRuleList(_mapper.Map<GroupRule>(pEntGroupRule));
                if (entListRule != null && entListRule.Count>0)
                {
                    return Ok(new { GroupRuleList = entListRule, Code = 200, TotalRows = entListRule[0].ListRange.TotalRows });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found" });
                }
            }
            catch (Exception ex)
            {

               return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        [Route("getrulebyname")]
        [Authorize]
        public async Task<IActionResult> GetRuleByName(GroupRuleVM pEntGroupRule)
        {
            try
            {
                GroupRule entGrpRuleReturn= new GroupRule();
                entGrpRuleReturn = _groupRuleAdaptor.GetRuleByName(_mapper.Map<GroupRule>(pEntGroupRule));
                if (entGrpRuleReturn.ID != null)
                {
                    return Ok(new { GroupRule = entGrpRuleReturn, Code = 200 });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("getruleidbyname")]
        [Authorize]
        public async Task<IActionResult> GetRuleIdByName(GroupRuleVM pEntGroupRule)
        {
            try
            {
                GroupRule entGrpRuleReturn = new GroupRule();
                entGrpRuleReturn = _groupRuleAdaptor.GetRuleIdByName(_mapper.Map<GroupRule>(pEntGroupRule));
                if (entGrpRuleReturn.ID != null)
                {
                    return Ok(new { GroupRule = entGrpRuleReturn, Code = 200 });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("editrule")]
        [Authorize]
        public async Task<IActionResult> EditRule(GroupRuleVM pEntGroupRule)
        {
            try
            {
                GroupRule entGrpRuleReturn = new GroupRule();
                entGrpRuleReturn = _groupRuleAdaptor.EditRule(_mapper.Map<GroupRule>(pEntGroupRule));
                if (entGrpRuleReturn.ID != null)
                {
                    return Ok(new { GroupRule = entGrpRuleReturn, Code = 200 });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("getruleidbyparentid")]
        [Authorize]
        public async Task<IActionResult> GetRuleIdByParentId(GroupRuleVM pEntGroupRule)
        {
            try
            {
                GroupRule entGrpRuleReturn = new GroupRule();
                entGrpRuleReturn = _groupRuleAdaptor.GetRuleIdByParentId(_mapper.Map<GroupRule>(pEntGroupRule));
                if (entGrpRuleReturn.ID != null)
                {
                    return Ok(new { GroupRule = entGrpRuleReturn, Code = 200 });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("addsinglerule")]
        [Authorize]
        public async Task<IActionResult> AddSingleRule(GroupRuleVM pEntGroupRule)
        {
            try
            {
                GroupRule entGrpRuleReturn = new GroupRule();
                entGrpRuleReturn = _groupRuleAdaptor.AddSingleRule(_mapper.Map<GroupRule>(pEntGroupRule));
                if (entGrpRuleReturn.ID != null)
                {
                    return Ok(new { GroupRule = entGrpRuleReturn, Code = 200, Msg = "Rule Added Successfully" });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("updatesinglerule")]
        [Authorize]
        public async Task<IActionResult> UpdateSingleRule(GroupRuleVM pEntGroupRule)
        {
            try
            {
                GroupRule entGrpRuleReturn = new GroupRule();
                entGrpRuleReturn = _groupRuleAdaptor.UpdateSingleRule(_mapper.Map<GroupRule>(pEntGroupRule));
                if (entGrpRuleReturn.ID != null)
                {
                    return Ok(new { GroupRule = entGrpRuleReturn, Code = 200, Msg = "Rule Added Successfully" });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("getgrouprulelistiperform")]
        [Authorize]
        public async Task<IActionResult> GetGroupRuleList_IPerform(GroupRuleVM pEntGroupRule)
        {
            try
            {
                List<GroupRule> entListRule = new List<GroupRule>();

                entListRule = _groupRuleAdaptor.GetGroupRuleList_IPerform(_mapper.Map<GroupRule>(pEntGroupRule));
                if (entListRule != null && entListRule.Count > 0)
                {
                    return Ok(new { GroupRuleList = entListRule, Code = 200, TotalRows = entListRule[0].ListRange.TotalRows });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("getgrouprulelistbyuserid")]
        [Authorize]
        public async Task<IActionResult> GetGroupRuleListByUserId(GroupRuleVM pEntGroupRule)
        {
            try
            {
                List<GroupRule> entListRule = new List<GroupRule>();

                entListRule = _groupRuleAdaptor.GetGroupRuleListByUserId(_mapper.Map<GroupRule>(pEntGroupRule));
                if (entListRule != null && entListRule.Count > 0)
                {
                    return Ok(new { GroupRuleList = entListRule, Code = 200, TotalRows = entListRule[0].ListRange.TotalRows });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("getgrouprulelistfordistribution")]
        [Authorize]
        public async Task<IActionResult> GetGroupRuleListForDistribution(GroupRuleVM pEntGroupRule)
        {
            try
            {
                List<GroupRule> entListRule = new List<GroupRule>();

                entListRule = _groupRuleAdaptor.GetGroupRuleListForDistribution(_mapper.Map<GroupRule>(pEntGroupRule));
                if (entListRule != null && entListRule.Count > 0)
                {
                    return Ok(new { GroupRuleList = entListRule, Code = 200, TotalRows = entListRule[0].ListRange.TotalRows });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("getrulebyid")]
        [Authorize]
        public async Task<IActionResult> GetRuleByID(GroupRuleVM pEntGroupRule)
        {
            try
            {
                GroupRule entGrpRuleReturn = new GroupRule();
                entGrpRuleReturn = _groupRuleAdaptor.GetRuleByID(_mapper.Map<GroupRule>(pEntGroupRule));
                if (entGrpRuleReturn.ID != null)
                {
                    return Ok(new { GroupRule = entGrpRuleReturn, Code = 200 });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        //[HttpPost]
        //[Route("deactivategrouprule")]
        //public async Task<IActionResult> DeactivateGroupRule(List<GroupRuleVM> pEntListGroupRule)
        //{
        //    try
        //    {
        //        List<GroupRule> entListGroupRule = new List<GroupRule>();
        //        Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = pEntListGroupRule[0].LastModifiedById, ClientId = pEntListGroupRule[0].ClientId });
        //        if (_entCurrentUser.ID != null && (_entCurrentUser.IsSuperAdmin() || _entCurrentUser.IsSiteAdmin()))
        //        {

        //            entListGroupRule = _groupRuleAdaptor.DeactivateGroupRule(_mapper.Map<List<GroupRule>>(pEntListGroupRule));
        //            if (entListGroupRule != null && entListGroupRule.Count > 0)
        //            {
        //                return Ok(new
        //                {
        //                    GroupRuleList = entListGroupRule,
        //                    Code = 200,
        //                    Masg = MessageAdaptor.GetMessage
        //                                (API.YPLMS.Services.Messages.BusinessRuleUsers.DEACTIVATED_RULE).Replace("#",
        //                            Convert.ToString(entListGroupRule.Count)).Replace("$", pEntListGroupRule.Count.ToString())

        //                });
        //            }
        //            else
        //            {
        //                return NotFound(new { Code = 404, Msg = "No data found" });
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(new { Code = 400, Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.GroupRule.CAN_NOT_DEACT_RULE)});
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }

        //}


        [HttpPost]
        [Route("deactivategrouprule")]
        [Authorize]
        public async Task<IActionResult> DeactivateGroupRule(List<GroupRuleVM> pEntListGroupRule)
        {
            try
            {

                string strRecordID = string.Empty;
                string strOldData = string.Empty;
                string strNewData = string.Empty;
                bool IsInvalidSelection = false;
                bool isSelectionNotValid = false;
                int totalRecords = 0;
                List<GroupRule> entListGroupRule = new List<GroupRule>();
                GroupRule _entGroupRule = new GroupRule();
                Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = pEntListGroupRule[0].LastModifiedById, ClientId = pEntListGroupRule[0].ClientId });
                foreach (var entGroupRule in pEntListGroupRule)
                {
                   // HtmlInputCheckBox chk = row.FindControl("chkClient") as HtmlInputCheckBox;

                    //string strStatus = row.Cells[4].Text;

                   // if (chk.Checked)
                    {
                        totalRecords++;
                       // if (row.Cells[4].Text.ToLower().Equals("yes"))
                        {
                            if ((_entCurrentUser.ID.ToLower().Trim().Equals(entGroupRule.CreatedById.Trim().ToLower())) ||
                                _entCurrentUser.IsSiteAdmin() || _entCurrentUser.IsSuperAdmin())
                            {
                                
                                _entGroupRule.ID = entGroupRule.ID;
                                _entGroupRule.ClientId = entGroupRule.ClientId;
                                _entGroupRule.LastModifiedById = _entCurrentUser.ID;
                                entListGroupRule.Add(_entGroupRule);

                                //strRecordID += chk.Value + ", ";
                                //strOldData += (strStatus.Trim().ToLower() == "yes" ? chk.Value + " : 1" : chk.Value + " : 0") + ", ";
                                //strNewData += chk.Value + " : 0" + ", ";
                            }
                            else
                            {
                                isSelectionNotValid = true; //For Proper Message Of User 
                            }
                        }
                        //else
                        //{
                        //    IsInvalidSelection = true;
                        //}

                    }
                }

                if (isSelectionNotValid && entListGroupRule.Count == 0)
                {

                    return BadRequest ( new
                    {
                        code = 400,
                        msg = MessageAdaptor.GetMessage
                           (API.YPLMS.Services.Messages.GroupRule.CAN_NOT_DEACT_RULE)
                    });
                }

                else if (IsInvalidSelection && entListGroupRule.Count == 0)
                {
                    return BadRequest(new
                    {
                        code = 400,
                        msg = MessageAdaptor.GetMessage
                            (API.YPLMS.Services.Messages.BusinessRuleUsers.SELECT_ACTIVE_RULE)
                    });
                }
                else
                {
                    entListGroupRule = _groupRuleAdaptor.DeactivateGroupRule(entListGroupRule);
                    if (entListGroupRule != null)
                    {
                        if (entListGroupRule.Count > 0)
                        {
                           return Ok ( new
                           {
                               code = 200,
                               msg = MessageAdaptor.GetMessage
                                (API.YPLMS.Services.Messages.BusinessRuleUsers.DEACTIVATED_RULE).Replace("#",
                                Convert.ToString(entListGroupRule.Count)).Replace("$", totalRecords.ToString())
                           });

                            #region Deactivate User Show All Rules Audit Log
                            //if (!string.IsNullOrEmpty(strRecordID))
                            //{
                            //    AuditTrailManager entAuditTrailManager = new AuditTrailManager();
                            //    AuditTrail entAuditTrail = new AuditTrail();

                            //    strRecordID = strRecordID.Trim().Trim(',');
                            //    strOldData = strOldData.Trim().Trim(',');
                            //    strNewData = strNewData.Trim().Trim(',');

                            //    entAuditTrail.EntityName = "User Show All Rules";
                            //    entAuditTrail.SystemUserGUID = _entCurrentUser.ID;
                            //    entAuditTrail.RecordID = strRecordID;
                            //    entAuditTrail.OldData = strOldData;
                            //    entAuditTrail.NewData = strNewData;
                            //    entAuditTrail.ActionId = CommonManager.AUDITTRAILDEACTIVATE;
                            //    entAuditTrail.ClientId = _strSelectedClientId;

                            //    entAuditTrail = entAuditTrailManager.Execute(entAuditTrail, AuditTrail.Method.Add);
                            //}
                            #endregion

                        }
                        else
                        {
                            return BadRequest(new
                            {
                                code = 400,
                                msg = MessageAdaptor.GetMessage
                                (API.YPLMS.Services.Messages.BusinessRuleUsers.NO_REC_DEACTIVATED)
                            });
                        }
                    }
                    else
                    {
                        return BadRequest(new
                        {
                            code = 400,
                            msg = MessageAdaptor.GetMessage
                               (API.YPLMS.Services.Messages.BusinessRuleUsers.NO_REC_DEACTIVATED)
                        });

                    }
                }




               
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("activategrouprule")]
        [Authorize]
        public async Task<IActionResult> ActivateGroupRule(List<GroupRuleVM> pEntListGroupRule)
        {
            try
            {

                List<GroupRule> entListGroupRule = new List<GroupRule>();
                entListGroupRule = _groupRuleAdaptor.ActivateGroupRule(_mapper.Map<List<GroupRule>>(pEntListGroupRule));
                if (entListGroupRule != null && entListGroupRule.Count > 0)
                {
                    return Ok(new { GroupRuleList = entListGroupRule, Code = 200, Masg = MessageAdaptor.GetMessage
                                (API.YPLMS.Services.Messages.BusinessRuleUsers.ACTIVATED_RULE), TotalRows = entListGroupRule[0].ListRange.TotalRows });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //[HttpDelete]
        //[Route("deletegrouprule")]
        //public async Task<IActionResult> DeleteGroupRule(List<GroupRuleVM> pEntListGroupRule)
        //{
        //    try
        //    {
        //        Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = pEntListGroupRule[0].LastModifiedById, ClientId = pEntListGroupRule[0].ClientId });
        //        foreach (var ent in pEntListGroupRule)
        //        {

        //        }
        //        if (_entCurrentUser.ID != null && (_entCurrentUser.IsSuperAdmin() || _entCurrentUser.IsSiteAdmin()))
        //        {
        //            List<GroupRule> entListGroupRule = new List<GroupRule>();
        //            entListGroupRule = _groupRuleAdaptor.DeleteGroupRule(_mapper.Map<List<GroupRule>>(pEntListGroupRule));
        //            if (entListGroupRule != null && entListGroupRule.Count > 0)
        //            {
        //                return Ok(new
        //                {
        //                    GroupRuleList = entListGroupRule,
        //                    Code = 200,
        //                    Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.BusinessRuleUsers.
        //                            BUSINESS_RULE_DEL_SUCESS).Replace("#",
        //                            Convert.ToString(entListGroupRule[0].ListRange.TotalRows)).Replace("$", pEntListGroupRule.Count.ToString())

        //                });
        //            }
        //            else
        //            {
        //                return NotFound(new { Code = 404, Msg = "No data found" });
        //            }
        //        }
        //        else
        //        {
        //            return BadRequest(new  { Code = 400, Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.GroupRule.CAN_NOT_DEL_RULE) });
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}


        [HttpDelete]
        [Route("deletegrouprule")]
        [Authorize]
        public async Task<IActionResult> DeleteGroupRule(List<GroupRuleVM> pEntListGroupRule)
        {
            try
            {

                string strRecordID = string.Empty;
                int totalRecords = 0;
                bool IsInvalidSelection = false;
                bool isSelectionNotValid = false;
                List<GroupRule> entListGroupRule = new List<GroupRule>();
                GroupRule _entGroupRule = new GroupRule();
                Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = pEntListGroupRule[0].LastModifiedById, ClientId = pEntListGroupRule[0].ClientId });
                foreach (var entGroupRule in pEntListGroupRule)
                {
                    //HtmlInputCheckBox chk = row.FindControl("chkClient") as HtmlInputCheckBox;
                    //if (chk.Checked)
                    {
                        totalRecords++;
                        if ((_entCurrentUser.ID.ToLower().Trim().Equals(entGroupRule.CreatedById.Trim().ToLower()))
                            || _entCurrentUser.IsSiteAdmin() || _entCurrentUser.IsSuperAdmin())
                        {
                            //if (row.Cells[9].Text.ToLower().Equals("yes") || row.Cells[9].Text.ToLower().Equals("true"))
                            //{ }
                            //else
                            {
                                
                                _entGroupRule.ID = entGroupRule.ID;
                                _entGroupRule.ClientId = entGroupRule.ClientId;
                                _entGroupRule.LastModifiedById = _entCurrentUser.ID;
                                entListGroupRule.Add(_entGroupRule);

                                strRecordID += entGroupRule.ID + ", ";
                            }
                        }
                        else
                        {
                            isSelectionNotValid = true;
                        }
                    }
                }
                if (isSelectionNotValid && entListGroupRule.Count == 0)
                {
                    return BadRequest(new
                    {
                        code = 400,
                        msg = MessageAdaptor.GetMessage
                      (API.YPLMS.Services.Messages.GroupRule.CAN_NOT_DEL_RULE)
                    });
                  
                }
                else
                {
                    entListGroupRule = _groupRuleAdaptor.DeleteGroupRule(entListGroupRule);
                    if (entListGroupRule != null)
                    {
                        if (entListGroupRule[0].ListRange.TotalRows > 0)
                        {
                            return Ok( new
                            {
                                code = 200,
                                msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.BusinessRuleUsers.
                                BUSINESS_RULE_DEL_SUCESS).Replace("#",
                                Convert.ToString(entListGroupRule[0].ListRange.TotalRows)).Replace("$", totalRecords.ToString())
                            } );

                            #region Delete User Show All Rules Audit Log
                            //if (!string.IsNullOrEmpty(strRecordID))
                            //{
                            //    AuditTrailManager entAuditTrailManager = new AuditTrailManager();
                            //    AuditTrail entAuditTrail = new AuditTrail();

                            //    strRecordID = strRecordID.Trim().Trim(',');

                            //    entAuditTrail.EntityName = "User Show All Rules";
                            //    entAuditTrail.SystemUserGUID = _entCurrentUser.ID;
                            //    entAuditTrail.RecordID = strRecordID;
                            //    entAuditTrail.ActionId = CommonManager.AUDITTRAILDELETE;
                            //    entAuditTrail.ClientId = _strSelectedClientId;

                            //    entAuditTrail = entAuditTrailManager.Execute(entAuditTrail, AuditTrail.Method.Add);
                            //}
                            #endregion

                        }
                        else
                        {
                           return BadRequest ( new
                           {
                               code = 400,
                               msg = MessageAdaptor.GetMessage
                                (API.YPLMS.Services.Messages.BusinessRuleUsers.NO_REC_DEL)
                           });

                        }
                    }
                    else
                    {
                        return BadRequest(new
                        {
                            code = 400,
                            msg = MessageAdaptor.GetMessage
                                 (API.YPLMS.Services.Messages.BusinessRuleUsers.NO_REC_DEL)
                        });
                    }


                }

                
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("copygrouprule")]
        [Authorize]
        public async Task<IActionResult> CopyBusinessRule(GroupRuleVM pEntGroupRule)
        {
            try
            {
                GroupRule _entGroupRule = new GroupRule();              
                _entGroupRule = _groupRuleAdaptor.GetRuleByID(_mapper.Map<GroupRule>(pEntGroupRule));

                _entGroupRule.ID = API.YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix("BR-", 12);
                _entGroupRule.IsActive = true;
                _entGroupRule.CreatedById = pEntGroupRule.CreatedById;
                _entGroupRule.LastModifiedById = pEntGroupRule.LastModifiedById;

                string originalCurrName = _entGroupRule.RuleName;
                _entGroupRule.RuleName = "Copy of " + _entGroupRule.RuleName;
                _entGroupRule.RuleName = _groupRuleAdaptor.CheckBusinessRuleNameExists(_entGroupRule.RuleName, 0, originalCurrName, _entGroupRule);
                foreach (RuleParameterGroup _entListGroupRule in _entGroupRule.RuleParameterGroupList)
                {
                    string _strRuleGroupID = null;
                    _strRuleGroupID = API.YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix("BRG-", 12);
                    _entListGroupRule.ID = _strRuleGroupID;
                    foreach (RuleParameters _entListRuleParameter in _entListGroupRule.RuleParameterList)
                    {
                        _entListRuleParameter.ID = null;
                        _entListRuleParameter.RuleId = _entGroupRule.ID;
                        _entListRuleParameter.RuleParameterGroupId = _strRuleGroupID;

                        if (_entListRuleParameter.GroupType.Equals("Users") || _entListRuleParameter.GroupType.Equals("UsersExclude"))
                        {
                            foreach (BusinessRuleUsers _entListBusinessRuleUsers in _entListGroupRule.BusinessRuleUsersList)
                            {
                                _entListBusinessRuleUsers.BusinessRuleId = _entGroupRule.ID;
                                _entListBusinessRuleUsers.ClientId = _entGroupRule.ClientId;
                                _entListBusinessRuleUsers.ParameterGroupId = _strRuleGroupID;
                                _entGroupRule.BusinessRuleUsers.Add(_entListBusinessRuleUsers);
                            }
                        }
                    }
                    _entListGroupRule.ID = _strRuleGroupID;
                    _entListGroupRule.RuleId = _entGroupRule.ID;
                }
                _entGroupRule = _groupRuleAdaptor.AddSingleRule(_entGroupRule);
                if (_entGroupRule.ID != null)
                {
                    return Ok(new { GroupRule = _entGroupRule, Code = 200, Msg = MessageAdaptor.GetMessage
                                    (API.YPLMS.Services.Messages.BusinessRuleUsers.RULE_COPIED_SUCCESS)
                    });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found" });
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
        public async Task<IActionResult> GetBussinessRuleUsers(BusinessRuleUsersVM pentBusinessRuleUsers)
        {
            try
            {

                //DataSet tempDS;
                //BusinessRuleUsers.ListMethod.GetResult 
                //entSearchUsers = new Search();
                BusinessRuleUsers entBusinessRuleUsers = new BusinessRuleUsers();

                //if (ViewState["SearchText"] != null)
                //entBusinessRuleUsers.KeyWord = ViewState["SearchText"].ToString();

                if (string.IsNullOrEmpty(pentBusinessRuleUsers.ClientId))
                {
                    return BadRequest(new { Code = 200, Msg = "Please provide client Id" });
                }
                entBusinessRuleUsers.ClientId = Convert.ToString(pentBusinessRuleUsers.ClientId);

                //entBusinessRuleUsers.BusinessRuleId = EncryptionManager.Decrypt(Convert.ToString(Page.Request.QueryString["rid"]).Replace(" ", "+"));
                entBusinessRuleUsers.BusinessRuleId = pentBusinessRuleUsers.BusinessRuleId;

                EntityRange listRange = new EntityRange();

                if (pentBusinessRuleUsers.ListRange.PageIndex != 0 && pentBusinessRuleUsers.ListRange.PageSize != 0)
                {
                    listRange.PageIndex = 0;
                    listRange.PageSize = 0;
                }

                if (!string.IsNullOrEmpty(pentBusinessRuleUsers.ListRange.SortExpression))
                    listRange.SortExpression = "SystemUserGUID";

                listRange.RequestedById = pentBusinessRuleUsers.ListRange.RequestedById;
                entBusinessRuleUsers.ListRange = listRange;

                List<BusinessRuleUsers> entListBusinessRuleUsers = _businessRuleUsersAdaptor.GetBusinessRuleResult(entBusinessRuleUsers);
               
                if (entListBusinessRuleUsers != null && entListBusinessRuleUsers.Count > 0)
                {
                    return Ok(new { BusinessRuleUsers = entListBusinessRuleUsers,Code = 200,TotalRows = entListBusinessRuleUsers[0].ListRange.TotalRows });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("getbussinessruleusersexport")]
        [Authorize]
        public async Task<IActionResult> GetBussinessRuleUsersExport(BusinessRuleUsersVM pentBusinessRuleUsers)
        {
            try
            {

                //DataSet tempDS;
                //BusinessRuleUsers.ListMethod.GetResult 
                //entSearchUsers = new Search();
                BusinessRuleUsers entBusinessRuleUsers = new BusinessRuleUsers();

                //if (ViewState["SearchText"] != null)
                //entBusinessRuleUsers.KeyWord = ViewState["SearchText"].ToString();

                if (string.IsNullOrEmpty(pentBusinessRuleUsers.ClientId))
                {
                    return BadRequest(new { Code = 200, Msg = "Please provide client Id" });
                }
                entBusinessRuleUsers.ClientId = Convert.ToString(pentBusinessRuleUsers.ClientId);

                //entBusinessRuleUsers.BusinessRuleId = EncryptionManager.Decrypt(Convert.ToString(Page.Request.QueryString["rid"]).Replace(" ", "+"));
                entBusinessRuleUsers.BusinessRuleId = pentBusinessRuleUsers.BusinessRuleId;

                EntityRange listRange = new EntityRange();

                if (pentBusinessRuleUsers.ListRange.PageIndex != 0 && pentBusinessRuleUsers.ListRange.PageSize != 0)
                {
                    listRange.PageIndex = 0;
                    listRange.PageSize = 0;
                }

                if (!string.IsNullOrEmpty(pentBusinessRuleUsers.ListRange.SortExpression))
                    listRange.SortExpression = "SystemUserGUID";

                listRange.RequestedById = pentBusinessRuleUsers.ListRange.RequestedById;
                entBusinessRuleUsers.ListRange = listRange;

                List<BusinessRuleUsers> entListBusinessRuleUsers = _businessRuleUsersAdaptor.GetBusinessRuleResult(entBusinessRuleUsers);
                API.YPLMS.Services.Converter dsConverter = new API.YPLMS.Services.Converter();
                DataSet tempDS = dsConverter.ConvertToDataSet<BusinessRuleUsers>(entListBusinessRuleUsers);
                DataTable dtable = new DataTable();
                DataTable dt = new DataTable();

                if (tempDS != null)
                {
                    if (tempDS.Tables.Count != 0)
                        dtable = tempDS.Tables[0];


                    dt.Columns.Add(new DataColumn("First Name"));
                    dt.Columns.Add(new DataColumn("Last Name"));
                    dt.Columns.Add(new DataColumn("Login ID"));
                    dt.Columns.Add(new DataColumn("Email ID"));
                    dt.Columns.Add(new DataColumn("User Org Hierarchy"));

                    if (dtable.Rows.Count > 0)
                    {

                        foreach (DataRow row in dtable.Rows)
                        {
                            DataRow drow = dt.NewRow();

                            drow["First Name"] = Convert.ToString(row["FirstName"]);
                            drow["Last Name"] = Convert.ToString(row["LastName"]);
                            drow["Login ID"] = Convert.ToString(row["UserNameAlias"]);
                            drow["Email ID"] = Convert.ToString(row["EmailID"]);
                            drow["User Org Hierarchy"] = Convert.ToString(row["UserOrgHierarchy"]);

                            dt.Rows.Add(drow);
                        }
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    ExcelWriter excWrite = new ExcelWriter();
                    //export excel or csv

                    //Response.ContentType = ExcelWriter.CSVContentType;
                    //Response.AddHeader("content-disposition", "attachment; filename=BusinessRuleUsers.csv");
                    //Response.Clear();
                    //Response.BinaryWrite((excWrite.ExportToCSV(dt)));
                    //Response.End();
                    byte[] csvData = excWrite.ExportToCSV(dt);

                    // Set the file content type and attachment header
                    var fileName = "BusinessRuleUsers.csv";
                    var fileContentType = "text/csv"; // CSV mime type

                    // Return the file as an attachment
                    return File(csvData, fileContentType, fileName);

                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found" });
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("getclientfieldsforgouprule")]
        [Authorize]
        public async Task<IActionResult> GetClientFieldsForGoupRule(GroupRuleVM pEntGroupRule)
        {
            try
            {
                GroupRuleManager groupRuleManager = new GroupRuleManager();
                DataView dw = groupRuleManager.GetClientFieldsFilter("FieldTypes", BusinessRuleReportsKeys.STANDARD_CUSTOM, "=",_mapper.Map<GroupRule>(pEntGroupRule));
                API.YPLMS.Services.Converter dsConverter = new API.YPLMS.Services.Converter();
                List<ImportDefination> entImpdefStandardField = dsConverter.ConvertDataTableToList<ImportDefination>(dw.ToTable());
                dw = new DataView();
                dw = groupRuleManager.GetClientFieldsFilter("FieldTypes", BusinessRuleReportsKeys.CUSTOM_FIELD, "=", _mapper.Map<GroupRule>(pEntGroupRule));
                List<ImportDefination> entImpdefCustomField = dsConverter.ConvertDataTableToList<ImportDefination>(dw.ToTable());

                dw = new DataView();
                dw = groupRuleManager.GetClientFieldsFilter("FieldTypes", "Assignment", "=", _mapper.Map<GroupRule>(pEntGroupRule));
                List<ImportDefination> entImpdefAssignment = dsConverter.ConvertDataTableToList<ImportDefination>(dw.ToTable());

                //For Completion
                dw = new DataView();
                dw = groupRuleManager.GetClientFieldsFilter("FieldTypes", "Completion", "=", _mapper.Map<GroupRule>(pEntGroupRule));
                //AddListData(dw.ToTable(), ref lstCompletion);
                List<ImportDefination> entImpdefCompletion = dsConverter.ConvertDataTableToList<ImportDefination>(dw.ToTable());

                //For Activity
                dw = new DataView();
                dw = groupRuleManager.GetClientFieldsFilter("FieldTypes", "Activity", "=", _mapper.Map<GroupRule>(pEntGroupRule));
                List<ImportDefination> entImpdefActivity = dsConverter.ConvertDataTableToList<ImportDefination>(dw.ToTable());

                Language _entLanguage = new Language();
                _entLanguage.ClientId = pEntGroupRule.ClientId;
               
                List<Language> entListLanguageClientBase = _languageDAM.GetClientLanguages(_entLanguage);

                List<GroupRule> entListGroupRule = new List<GroupRule>();
                //entListGroupRule = _groupRuleAdaptor.DeleteGroupRule(_mapper.Map<List<GroupRule>>(pEntListGroupRule));
                if (entImpdefStandardField != null && entImpdefStandardField.Count > 0)
                {
                    return Ok(new
                    {
                        StandardField = entImpdefStandardField, Code = 200, CustomField = entImpdefCustomField,
                        Assignment = entImpdefAssignment,
                        Completion = entImpdefCompletion,
                        Activity = entImpdefActivity,
                        Language = entListLanguageClientBase
                    });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
