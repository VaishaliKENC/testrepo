using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IOrganizationLevelUnitDAM<T>
    {
        OrganizationLevelUnit GetOrganizationUnitsByID(OrganizationLevelUnit pEntObjOrganizationUnit);
        OrganizationLevelUnit GetOrganizationUnitByName(OrganizationLevelUnit pEntObjOrganizationUnit);
        OrganizationLevelUnit AddOrganizationUnit(OrganizationLevelUnit pEntOrgUnit);
        OrganizationLevelUnit EditOrganizationUnit(OrganizationLevelUnit pEntOrgUnit);
        List<OrganizationLevelUnit> GetAllUnits(OrganizationLevelUnit pEntOrgUnit);
        List<OrganizationLevelUnit> GetAllUnits_ForImport(OrganizationLevelUnit pEntOrgUnit);
        List<OrganizationLevelUnit> FindAllUnits(Search pEntSearch);
        OrganizationLevelUnit CheckandAddOrganizationLevelUnit(OrganizationLevelUnit pEntOrgLevelUnit);
        OrganizationLevelUnit DeleteOrganizationLevelUnit(OrganizationLevelUnit pEntOrgLevelUnit);
    }
}
