using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    public class LearnerFacade
    {
        /// <summary>
        /// Get Requested By Id Parameter
        /// /// </summary>
        /// <returns></returns>
        public static string GetRequestedById(string strUserID, string strClientId)
        {
            try
            {
                if (!String.IsNullOrEmpty(strUserID))
                {
                    LearnerDAM mgrLearner = new LearnerDAM();
                    Learner eUser = new Learner();
                    eUser.ID = strUserID;
                    eUser.ClientId = strClientId;
                    //eUser = mgrLearner.Execute(eUser, Learner.Method.Get);
                    eUser = mgrLearner.GetUserRequestedByID(eUser); //Execute(eUser, Learner.Method.GetUserForReqById);
                    if (eUser == null && String.IsNullOrEmpty(eUser.ID))
                    {
                        eUser = new Learner();
                        eUser.ID = strUserID;
                        eUser.ClientId = Common.BaseClientID;
                        //eUser = mgrLearner.Execute(eUser, Learner.Method.Get);
                        eUser = mgrLearner.GetUserRequestedByID(eUser);  //Execute(eUser, Learner.Method.GetUserForReqById);
                    }

                    UserAdminRole entAdminRole = eUser.UserAdminRole.Find(delegate (UserAdminRole entRoleToFind)
                    { return entRoleToFind.RoleId == AdminRole.SITE_ADMIN_ROLE_ID || entRoleToFind.RoleId == AdminRole.SUPER_ADMIN_ROLE_ID; });
                    if (entAdminRole == null)
                    {
                        return eUser.ID;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return strUserID;
            }
        }
    }
}
