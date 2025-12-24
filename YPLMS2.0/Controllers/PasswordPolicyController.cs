using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PasswordPolicyController : ControllerBase
    {
        private readonly IPasswordPolicyAdaptor<PasswordPolicyConfiguration> _passwordPolicyAdaptor;
        public PasswordPolicyController(IPasswordPolicyAdaptor<PasswordPolicyConfiguration> passwordPolicyAdaptor)
        {
            _passwordPolicyAdaptor = passwordPolicyAdaptor;
        }

        [HttpPost]
        [Route("getpasswordpolicybyid")]
        [Authorize]
        public async Task<IActionResult> GetPasswordPolicyById(PasswordPolicyConfiguration pEntPwdPolicyConfig)
        {

            PasswordPolicyConfiguration entPwdPolicyConfig = new PasswordPolicyConfiguration();

            entPwdPolicyConfig = _passwordPolicyAdaptor.GetPasswordPolicyById(pEntPwdPolicyConfig);
            if (entPwdPolicyConfig != null)
            {

                return Ok(new { PasswordPolicyConfiguration = entPwdPolicyConfig });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("addpasswordpolicyconfiguration")]
        [Authorize]
        public async Task<IActionResult> AddPasswordPolicyConfiguration(PasswordPolicyConfiguration pEntPwdPolicyConfig)
        {

            PasswordPolicyConfiguration entPwdPolicyConfig = new PasswordPolicyConfiguration();

            entPwdPolicyConfig = _passwordPolicyAdaptor.AddPasswordPolicyConfiguration(pEntPwdPolicyConfig);
            if (entPwdPolicyConfig != null)
            {

                return Ok(new { PasswordPolicyConfiguration = entPwdPolicyConfig });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("editpasswordpolicyconfiguration")]
        [Authorize]
        public async Task<IActionResult> EditPasswordPolicyConfiguration(PasswordPolicyConfiguration pEntPwdPolicyConfig)
        {

            PasswordPolicyConfiguration entPwdPolicyConfig = new PasswordPolicyConfiguration();

            entPwdPolicyConfig = _passwordPolicyAdaptor.EditPasswordPolicyConfiguration(pEntPwdPolicyConfig);
            if (entPwdPolicyConfig != null)
            {

                return Ok(new { PasswordPolicyConfiguration = entPwdPolicyConfig });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("getemailrequestdetails")]
        [Authorize]
        public async Task<IActionResult> GetEmailRequestDetails(PasswordPolicyConfiguration pEntPwdPolicyConfig)
        {

            PasswordPolicyConfiguration entPwdPolicyConfig = new PasswordPolicyConfiguration();

            entPwdPolicyConfig = _passwordPolicyAdaptor.GetEmailRequestDetails(pEntPwdPolicyConfig);
            if (entPwdPolicyConfig != null)
            {

                return Ok(new { PasswordPolicyConfiguration = entPwdPolicyConfig });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("addupdateemailrequests")]
        [Authorize]
        public async Task<IActionResult> AddUpdateEmailRequests(PasswordPolicyConfiguration pEntPwdPolicyConfig)
        {

            PasswordPolicyConfiguration entPwdPolicyConfig = new PasswordPolicyConfiguration();

            entPwdPolicyConfig = _passwordPolicyAdaptor.AddUpdateEmailRequests(pEntPwdPolicyConfig);
            if (entPwdPolicyConfig != null)
            {

                return Ok(new { PasswordPolicyConfiguration = entPwdPolicyConfig });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("addupdateotpemailrequests")]
        [Authorize]
        public async Task<IActionResult> AddUpdateOTPEmailRequests(PasswordPolicyConfiguration pEntPwdPolicyConfig)
        {

            PasswordPolicyConfiguration entPwdPolicyConfig = new PasswordPolicyConfiguration();

            entPwdPolicyConfig = _passwordPolicyAdaptor.AddUpdateOTPEmailRequests(pEntPwdPolicyConfig);
            if (entPwdPolicyConfig != null)
            {

                return Ok(new { PasswordPolicyConfiguration = entPwdPolicyConfig });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
