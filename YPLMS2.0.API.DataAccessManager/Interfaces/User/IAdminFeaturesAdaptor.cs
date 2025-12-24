using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IAdminFeaturesAdaptor<T>
    {
        AdminFeatures GetFeatureByID(AdminFeatures pEntAdminFeature);
        List<AdminFeatures> GetFeaturesList(AdminFeatures pEntAdminFeatures);
        AdminFeatures UpdateIsVisible(AdminFeatures pEntAdminFeature);
        AdminFeatures EditFeature(AdminFeatures pEntAdminFeature);

    }
}
