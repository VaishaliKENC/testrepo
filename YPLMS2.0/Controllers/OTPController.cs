using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OTPController : ControllerBase
    {
        private readonly IOTPAdaptor<OTP> _oTPAdaptor;
        public OTPController(IOTPAdaptor<OTP> oTPAdaptor) 
        {
            _oTPAdaptor = oTPAdaptor;
        }

        [HttpPost]
        [Route("addotp")]
        [Authorize]
        public async Task<IActionResult> AddOTP(OTP pEntOTP)
        {

            OTP entOTP = new OTP();

            entOTP = _oTPAdaptor.AddOTP(pEntOTP);
            if (entOTP != null)
            {

                return Ok(new { OTP = entOTP });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Getbyotpnumber")]
        [Authorize]
        public async Task<IActionResult> Get(OTP pEntOTP)
        {

            OTP entOTP = new OTP();

            entOTP = _oTPAdaptor.Get(pEntOTP);
            if (entOTP != null)
            {

                return Ok(new { OTP = entOTP });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("getotpnumber")]
        [Authorize]
        public async Task<IActionResult> GetOTPNumber(OTP pEntOTP)
        {

            OTP entOTP = new OTP();

            entOTP = _oTPAdaptor.GetOTPNumber(pEntOTP);
            if (entOTP != null)
            {

                return Ok(new { OTP = entOTP });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("checkexpireotp")]
        [Authorize]
        public async Task<IActionResult> CheckExpireOTP(OTP pEntOTP)
        {

            OTP entOTP = new OTP();

            entOTP = _oTPAdaptor.CheckExpireOTP(pEntOTP);
            if (entOTP != null)
            {

                return Ok(new { OTP = entOTP });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
