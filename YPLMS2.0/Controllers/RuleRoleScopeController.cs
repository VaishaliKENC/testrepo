using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RuleRoleScopeController : ControllerBase
    {
        private readonly IRuleRoleScopeAdaptor<RuleRoleScope> _ruleRoleScopeAdaptor;
        public RuleRoleScopeController(IRuleRoleScopeAdaptor<RuleRoleScope> ruleRoleScopeAdaptor)
        {
            _ruleRoleScopeAdaptor = ruleRoleScopeAdaptor;
        }

        [HttpPost]
        [Route("getrulerolebyid")]
        [Authorize]
        public async Task<IActionResult> GetRuleRoleByID(RuleRoleScope pEntRole)
        {
            try
            {
                RuleRoleScope entRole = new RuleRoleScope();
                entRole = _ruleRoleScopeAdaptor.GetRuleRoleByID(pEntRole);

                if (entRole != null)
                {
                    return Ok(new { RuleRole = entRole, Code = 200 }); // Sends a JSON response with status code 200
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
        [Route("editrulerolescope")]
        [Authorize]
        public async Task<IActionResult> EditRuleRoleScope(RuleRoleScope pEntRole)
        {
            try
            {
                RuleRoleScope entRole = new RuleRoleScope();
                entRole = _ruleRoleScopeAdaptor.EditRuleRoleScope(pEntRole);

                if (entRole != null)
                {
                    return Ok(new { RuleRole = entRole, Code = 200 }); // Sends a JSON response with status code 200
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
        [Route("getinrolelist")]
        [Authorize]
        public async Task<IActionResult> GetInRole(RuleRoleScope pEntRole)
        {
            try
            {
                List<RuleRoleScope> entListRole = new List<RuleRoleScope>();
                entListRole = _ruleRoleScopeAdaptor.GetInRole(pEntRole);

                if (entListRole != null)
                {
                    return Ok(new { RuleRoleList = entListRole, Code = 200 }); // Sends a JSON response with status code 200
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
        [Route("GetNotInRolelist")]
        [Authorize]
        public async Task<IActionResult> GetNotInRole(RuleRoleScope pEntRole)
        {
            try
            {
                List<RuleRoleScope> entListRole = new List<RuleRoleScope>();
                entListRole = _ruleRoleScopeAdaptor.GetNotInRole(pEntRole);

                if (entListRole != null)
                {
                    return Ok(new { RuleRoleList = entListRole, Code = 200 }); // Sends a JSON response with status code 200
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
        [Route("getlistallbyallrolelist")]
        [Authorize]
        public async Task<IActionResult> GetListAllByAllRoleList(RuleRoleScope pEntRole)
        {
            try
            {
                List<RuleRoleScope> entListRole = new List<RuleRoleScope>();
                entListRole = _ruleRoleScopeAdaptor.GetListAllByAllRole(pEntRole);

                if (entListRole != null)
                {
                    return Ok(new { RuleRoleList = entListRole, Code = 200 }); // Sends a JSON response with status code 200
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
    }
}
