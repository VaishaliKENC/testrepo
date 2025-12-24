using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity;
namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientFeatureDAMController : ControllerBase
    {
        
       
        private readonly IClientFeatureDAM<ClientFeature> _clientfeaturedam ;
        ClientFeature _entClientFeature = new ClientFeature();
        public ClientFeatureDAMController(IClientFeatureDAM<ClientFeature> clientfeaturedam)
        {
            _clientfeaturedam= clientfeaturedam;

        }

        [HttpPost]
        [Route("getclientfeaturebyid")]
        [Authorize]
        public async Task<IActionResult> GetClientFeatureByID(ClientFeature pEntClientFeature)
        {

            _entClientFeature = _clientfeaturedam.GetClientFeatureByID(pEntClientFeature);
            if (_entClientFeature != null)
            {
                return Ok(new { ClientFeature = _entClientFeature });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
