using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity;
namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientDAMController : ControllerBase
    {
        
        private readonly IClientDAM<Client> _clientdam;
        public ClientDAMController(IClientDAM<Client> clientdam)
        {
            _clientdam= clientdam;
        }

        [HttpPost]
        [Route("getclientidbyurl")]        
        public async Task<IActionResult> GetClientIdByURL(Client pEntClient)
        {
            Client entClient = new Client();
            entClient = _clientdam.GetClientIdByURL(pEntClient);
            if (entClient.ID != null)
            {
                entClient = _clientdam.GetClientByID(entClient);
            }

            if (entClient.ID != null)
            {
                return Ok(new { Client = entClient, Code = 200 });
            }
            else
            {
                return BadRequest(new { Code = 400, Msg = "No data found"});
            }
        }
    }
}
