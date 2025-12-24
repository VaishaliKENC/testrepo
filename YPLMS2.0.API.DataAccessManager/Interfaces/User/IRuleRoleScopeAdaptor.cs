using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IRuleRoleScopeAdaptor<T>
    {
        RuleRoleScope GetRuleRoleByID(RuleRoleScope pEntRole);
        RuleRoleScope EditRuleRoleScope(RuleRoleScope pEntRole);
        List<RuleRoleScope> GetInRole(RuleRoleScope pEntRole);
        List<RuleRoleScope> GetNotInRole(RuleRoleScope pEntRole);
        List<RuleRoleScope> GetListAllByAllRole(RuleRoleScope pEntRole);
    }
}
