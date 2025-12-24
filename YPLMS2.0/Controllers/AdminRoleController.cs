using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminRoleController : ControllerBase
    {
        private readonly IAdminRoleAdaptor<AdminRole> _adminRoleAdaptor;
        private AdminRole _entAdminRole = new AdminRole();
        private DataSet _dataset=new DataSet();
        List<AdminRole> _entListAdminRole = new List<AdminRole>();

        public AdminRoleController(IAdminRoleAdaptor<AdminRole> adminRoleAdaptor)
        {
            _adminRoleAdaptor = adminRoleAdaptor;
        }

        [HttpPost]
        [Route("getalluserbyrule")]
        [Authorize]
        public async Task<IActionResult> GetAllUserByRule(AdminRole pEntAdminRole)
        {
            _dataset = _adminRoleAdaptor.GetAllUserByRule(pEntAdminRole);
            if (_dataset != null)
            {
                return Ok(new { AdminRole = _dataset });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("getadminrolebyid")]
        [Authorize]
        public async Task<IActionResult> GetAdminRoleByID(AdminRole pEntAdminRole)
        {
            _entAdminRole = _adminRoleAdaptor.GetAdminRoleByID(pEntAdminRole);
            if (_entAdminRole != null)
            {
                return Ok(new { AdminRole = _entAdminRole });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("getadminrolenamebyid")]
        [Authorize]
        public async Task<IActionResult> GetAdminRoleNameByID(AdminRole pEntAdminRole)
        {
            _entAdminRole = _adminRoleAdaptor.GetAdminRoleNameByID(pEntAdminRole);
            if (_entAdminRole != null)
            {
                return Ok(new { AdminRole = _entAdminRole });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("checkrolename")]
        [Authorize]
        public async Task<IActionResult> CheckRoleName(AdminRole pEntAdminRole)
        {
            _entAdminRole = _adminRoleAdaptor.CheckRoleName(pEntAdminRole);
            if (_entAdminRole != null)
            {
                return Ok(new { AdminRole = _entAdminRole });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("getallroles")]
        [Authorize]
        public async Task<IActionResult> GetAllRoles(AdminRole pEntAdminRole)
        {
            _entListAdminRole = _adminRoleAdaptor.GetAllRoles(pEntAdminRole);
            if (_entListAdminRole != null)
            {
                return Ok(new { RoleList = _entListAdminRole });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("getallrolesforreport")]
        [Authorize]
        public async Task<IActionResult> GetAllRolesForReport(AdminRole pEntAdminRole)
        {
            _entListAdminRole = _adminRoleAdaptor.GetAllRolesForReport(pEntAdminRole);
            if (_entListAdminRole != null)
            {
                return Ok(new { RoleList = _entListAdminRole });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("getallrolesbyactivestatus")]
        [Authorize]
        public async Task<IActionResult> GetAllRolesByActiveStatus(AdminRole pEntAdminRole)
        {
            _entListAdminRole = _adminRoleAdaptor.GetAllRolesByActiveStatus(pEntAdminRole);
            if (_entListAdminRole != null)
            {
                return Ok(new { RoleList = _entListAdminRole });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("addadminrole")]
        [Authorize]
        public async Task<IActionResult> AddAdminRole(AdminRole pEntAdminRole)
        {
            _entAdminRole = _adminRoleAdaptor.AddAdminRole(pEntAdminRole);
            if (_entAdminRole != null)
            {
                return Ok(new { AdminRole = _entAdminRole });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("editadminrole")]
        [Authorize]
        public async Task<IActionResult> EditAdminRole(AdminRole pEntAdminRole)
        {
            _entAdminRole = _adminRoleAdaptor.EditAdminRole(pEntAdminRole);
            if (_entAdminRole != null)
            {
                return Ok(new { AdminRole = _entAdminRole });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("updatelistofadminroles")]
        [Authorize]
        public async Task<IActionResult> UpdateListOfAdminRoles(List<AdminRole> pEntListBaseAdminRole)
        {
            _entListAdminRole = _adminRoleAdaptor.UpdateListOfAdminRoles(pEntListBaseAdminRole);
            if (_entListAdminRole != null)
            {
                return Ok(new { AdminRoleList = _entListAdminRole });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("deleteadminrole")]
        [Authorize]
        public async Task<IActionResult> DeleteAdminRole(AdminRole pEntAdminRole)
        {
            _entAdminRole = _adminRoleAdaptor.DeleteAdminRole(pEntAdminRole);
            if (_entAdminRole != null)
            {
                return Ok(new { AdminRole = _entAdminRole });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("addmultipleadminroles")]
        [Authorize]
        public async Task<IActionResult> AddMultipleAdminRoles(List<AdminRole> pEntListBaseAdminRole)
        {
            _entListAdminRole = _adminRoleAdaptor.AddMultipleAdminRoles(pEntListBaseAdminRole);
            if (_entListAdminRole != null)
            {
                return Ok(new { AdminRoleList = _entListAdminRole });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("assignroletouser")]
        [Authorize]
        public async Task<IActionResult> AssignRoleToUser(AdminRole pEntAdminRole)
        {
            _entAdminRole = _adminRoleAdaptor.AssignRoleToUser(pEntAdminRole);
            if (_entAdminRole != null)
            {
                return Ok(new { AdminRole = _entAdminRole });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("allunassignroletouser")]
        [Authorize]
        public async Task<IActionResult> AllUnAssignRoleToUser(AdminRole pEntAdminRole)
        {
            _entAdminRole = _adminRoleAdaptor.AllUnAssignRoleToUser(pEntAdminRole);
            if (_entAdminRole != null)
            {
                return Ok(new { AdminRole = _entAdminRole });
            }
            else
            {
                return BadRequest();
            }
        }

    }  

}
