using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IBusinessRuleUsersAdaptor<T>
    {
        List<BusinessRuleUsers> GetBusinessRuleResult(BusinessRuleUsers pEntBusinessRuleUsers);
        List<BusinessRuleUsers> GetBusinessRuleMultiResult(BusinessRuleUsers pEntBusinessRuleUsers);
        List<BusinessRuleUsers> GetBusinessRuleActiveUsers(BusinessRuleUsers pEntBusinessRuleUsers);

    }
}
