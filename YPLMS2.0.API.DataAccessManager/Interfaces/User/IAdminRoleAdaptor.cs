using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IAdminRoleAdaptor<T>
    {
        DataSet GetAllUserByRule(AdminRole pEntMenuItems);
        AdminRole GetAdminRoleByID(AdminRole pEntAdminRole);
        AdminRole GetAdminRoleNameByID(AdminRole pEntAdminRole);
        AdminRole CheckRoleName(AdminRole pEntAdminRole);
        List<AdminRole> GetAllRoles(AdminRole pEntAdminRole);
        List<AdminRole> GetAllRolesForReport(AdminRole pEntAdminRole);
        List<AdminRole> GetAllRolesByActiveStatus(AdminRole pEntAdminRole);
        AdminRole AddAdminRole(AdminRole pEntAdminRole);
        AdminRole EditAdminRole(AdminRole pEntAdminRole);
        List<AdminRole> UpdateListOfAdminRoles(List<AdminRole> pEntListBaseAdminRole);
        AdminRole DeleteAdminRole(AdminRole pEntAdminRole);
        List<AdminRole> AddMultipleAdminRoles(List<AdminRole> pEntListBaseAdminRole);
        AdminRole AssignRoleToUser(AdminRole pEntAdminRole);
        AdminRole AllUnAssignRoleToUser(AdminRole pEntAdminRole);
    }
}
