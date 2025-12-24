using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IPasswordPolicyAdaptor<T>
    {
        PasswordPolicyConfiguration GetPasswordPolicyById(PasswordPolicyConfiguration pEntPwdPolicyConfig);
        PasswordPolicyConfiguration AddPasswordPolicyConfiguration(PasswordPolicyConfiguration pEntPwdPolicyConfig);
        PasswordPolicyConfiguration EditPasswordPolicyConfiguration(PasswordPolicyConfiguration pEntPwdPolicyConfig);
        PasswordPolicyConfiguration GetEmailRequestDetails(PasswordPolicyConfiguration pEntPasswordPolicyConfiguration);
        PasswordPolicyConfiguration AddUpdateEmailRequests(PasswordPolicyConfiguration pEntPwdPolicyConfig);
        PasswordPolicyConfiguration AddUpdateOTPEmailRequests(PasswordPolicyConfiguration pEntPwdPolicyConfig);
    }
}
