using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BusinessRuleUsersController : ControllerBase
    {
        private readonly IBusinessRuleUsersAdaptor<BusinessRuleUsers> _businessRuleUsersAdaptor;
        public BusinessRuleUsersController(IBusinessRuleUsersAdaptor<BusinessRuleUsers> businessRuleUsersAdaptor)
        {
            _businessRuleUsersAdaptor = businessRuleUsersAdaptor;
        }

        [HttpPost]
        [Route("getbusinessruleresult")]
        [Authorize]
        public async Task<IActionResult> GetBusinessRuleResult(BusinessRuleUsers pEntBusinessRuleUsers)
        {

            List<BusinessRuleUsers> entListBusinessRuleUsers = new List<BusinessRuleUsers>();
            entListBusinessRuleUsers = _businessRuleUsersAdaptor.GetBusinessRuleResult(pEntBusinessRuleUsers);
            if (entListBusinessRuleUsers != null)
            {
                return Ok(new { BusinessRuleUsers = entListBusinessRuleUsers });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("getbusinessrulemultiresult")]
        [Authorize]
        public async Task<IActionResult> GetBusinessRuleMultiResult(BusinessRuleUsers pEntBusinessRuleUsers)
        {

            List<BusinessRuleUsers> entListBusinessRuleUsers = new List<BusinessRuleUsers>();
            entListBusinessRuleUsers = _businessRuleUsersAdaptor.GetBusinessRuleMultiResult(pEntBusinessRuleUsers);
            if (entListBusinessRuleUsers != null)
            {
                return Ok(new { BusinessRuleUsers = entListBusinessRuleUsers });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("getbusinessruleactiveusers")]
        [Authorize]
        public async Task<IActionResult> GetBusinessRuleActiveUsers(BusinessRuleUsers pEntBusinessRuleUsers)
        {

            List<BusinessRuleUsers> entListBusinessRuleUsers = new List<BusinessRuleUsers>();
            entListBusinessRuleUsers = _businessRuleUsersAdaptor.GetBusinessRuleActiveUsers(pEntBusinessRuleUsers);
            if (entListBusinessRuleUsers != null)
            {
                return Ok(new { BusinessRuleUsers = entListBusinessRuleUsers });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
