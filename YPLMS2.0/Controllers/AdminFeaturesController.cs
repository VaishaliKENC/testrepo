using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminFeaturesController : ControllerBase
    {
        
        private readonly IAdminFeaturesAdaptor<AdminFeatures> _adminFeaturesAdaptor;
        
        public AdminFeaturesController(IAdminFeaturesAdaptor<AdminFeatures> adminFeaturesAdaptor)
        {
            _adminFeaturesAdaptor = adminFeaturesAdaptor;
        }

        [HttpPost]
        [Route("getfeaturebyid")]
        [Authorize]
        public async Task<IActionResult> GetFeatureByID(AdminFeatures pEntAdminFeature)
        {
            
            AdminFeatures entAdminFeature = new AdminFeatures();

            entAdminFeature = _adminFeaturesAdaptor.GetFeatureByID(pEntAdminFeature);
            if (entAdminFeature != null)
            {
                return Ok(new { AdminFeatures = entAdminFeature });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("getfeatureslist")]
        [Authorize]
        public async Task<IActionResult> GetFeaturesList(AdminFeatures pEntAdminFeature)
        {
           
            List<AdminFeatures> entAdminFeatureList = new List<AdminFeatures>();

            entAdminFeatureList = _adminFeaturesAdaptor.GetFeaturesList(pEntAdminFeature);
            if (entAdminFeatureList != null)
            {
                return Ok(new { AdminFeaturesList = entAdminFeatureList });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("updateisvisible")]
        [Authorize]
        public async Task<IActionResult> UpdateIsVisible(AdminFeatures pEntAdminFeature)
        {
            
            AdminFeatures entAdminFeature = new AdminFeatures();

            entAdminFeature = _adminFeaturesAdaptor.UpdateIsVisible(pEntAdminFeature);
            if (entAdminFeature != null)
            {
                return Ok(new { AdminFeatures = entAdminFeature });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("editfeature")]
        [Authorize]
        public async Task<IActionResult> EditFeature(AdminFeatures pEntAdminFeature)
        {
            
            AdminFeatures entAdminFeature = new AdminFeatures();

            entAdminFeature = _adminFeaturesAdaptor.EditFeature(pEntAdminFeature);
            if (entAdminFeature != null)
            {
                return Ok(new { AdminFeatures = entAdminFeature });
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
