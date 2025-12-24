using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IOrganizationLevelDAM<T>
    {
        OrganizationLevel GetOrganizationLevelsByID(OrganizationLevel pEntOrganizationLevel);
        OrganizationLevel AddOrganizationLevel(OrganizationLevel pEntOrgLevel);
        OrganizationLevel EditOrganizationLevel(OrganizationLevel pEntOrgLevel);
        List<OrganizationLevel> GetAllLevels(OrganizationLevel pEntOrgLevel);
        List<OrganizationLevel> GetOnlyLevels(OrganizationLevel pEntOrgLevel);
        
        OrganizationLevel CheckandAddOrganizationLevel(OrganizationLevel pOrgLevel);
        OrganizationLevel DeleteOrganizationLevel(OrganizationLevel pEntOrgLevel);
        OrganizationLevel GetOrganizationTreeDHTML(OrganizationLevel pEntOrganizationLevel);

    }
}
