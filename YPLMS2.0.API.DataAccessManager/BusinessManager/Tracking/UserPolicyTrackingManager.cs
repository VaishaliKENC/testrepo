using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager
{

    public class UserPolicyTrackingManager : IManager<UserPolicyTracking, UserPolicyTracking.Method, UserPolicyTracking.ListMethod>
    {
        /// <summary>
        /// Default UserPolicyTrackingManager Constructor
        /// </summary>
        public UserPolicyTrackingManager()
        { }

        /// <summary>
        /// Execute GetAll
        /// </summary>
        /// <param name="pEntUserPolicyTracking"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<UserPolicyTracking> Execute(UserPolicyTracking pEntUserPolicyTracking, UserPolicyTracking.ListMethod pMethod)
        {
            return null;
        }

        /// <summary>
        /// Execute for BulkUpdateAdd
        /// </summary>
        /// <param name="pEntListUserAssetTracking"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<UserPolicyTracking> Execute(List<UserPolicyTracking> pEntListUserPolicyTracking, UserPolicyTracking.ListMethod pMethod)
        {
            List<UserPolicyTracking> entListUserPolicyTracking = null;
            UserPolicyTrackingDAM adaptorUserPolicyTracking = new UserPolicyTrackingDAM();
            if (pEntListUserPolicyTracking.Count > 0)
            {
                switch (pMethod)
                {
                    case UserPolicyTracking.ListMethod.MarkCompleted:
                        entListUserPolicyTracking = adaptorUserPolicyTracking.BulkMarkCompleted(pEntListUserPolicyTracking, false);
                        break;
                    case UserPolicyTracking.ListMethod.BulkMarkCompleted:
                        entListUserPolicyTracking = adaptorUserPolicyTracking.BulkMarkCompleted(pEntListUserPolicyTracking, true);
                        break;
                    default:
                        break;
                }
            }
            foreach (UserPolicyTracking entTracking in pEntListUserPolicyTracking)
            {
                GotoDynamicAssignment(entTracking);
            }
            return entListUserPolicyTracking;
        }

        /// <summary>
        /// Execute Get/Add/Update
        /// </summary>
        /// <param name="pEntUserPolicyTracking"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public UserPolicyTracking Execute(UserPolicyTracking pEntUserPolicyTracking, UserPolicyTracking.Method pMethod)
        {
            UserPolicyTracking entUserPolicyTracking = null;
            UserPolicyTracking entTrackStatus = null;
            UserPolicyTrackingDAM UserPolicyTrackingAdaptor = new UserPolicyTrackingDAM();

            switch (pMethod)
            {
                case UserPolicyTracking.Method.Add:
                    entTrackStatus = UserPolicyTrackingAdaptor.GetActivityStatus(pEntUserPolicyTracking);
                    entUserPolicyTracking = UserPolicyTrackingAdaptor.AddUpdateUserPolicyTracking(pEntUserPolicyTracking);
                    if (entTrackStatus.CompletionStatus != ActivityCompletionStatus.Completed && entTrackStatus.CompletionStatus != ActivityCompletionStatus.CompletedByProxy && entTrackStatus.CompletionStatus != ActivityCompletionStatus.CompletedByProxy)
                    {
                        GotoDynamicAssignment(pEntUserPolicyTracking);
                    }
                    break;
                case UserPolicyTracking.Method.UpdateScannedFileName:
                    entUserPolicyTracking = UserPolicyTrackingAdaptor.UpdateScannedFileName(pEntUserPolicyTracking);
                    break;
                default:
                    entUserPolicyTracking = null;
                    break;
            }
            return entUserPolicyTracking;
        }

        private void GotoDynamicAssignment(UserPolicyTracking pEntUserPolicyTracking)
        {
            if (!(pEntUserPolicyTracking.IsForAdminPreview))
            {
                BackgroundBRuleAssignmentManager entMgr = new BackgroundBRuleAssignmentManager(pEntUserPolicyTracking.PolicyId, pEntUserPolicyTracking.SystemUserGUID, ActivityContentType.Policy.ToString(), pEntUserPolicyTracking.ClientId);
                entMgr.AssignActivties();
            }
        }

        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="UserPolicyTracking"></typeparam>
        /// <param name="pEntBase">UserPolicyTracking object</param>
        /// <param name="pMethod">UserPolicyTracking.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(UserPolicyTracking pEntBase, UserPolicyTracking.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<UserPolicyTracking> listUserPolicyTracking = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<UserPolicyTracking>(listUserPolicyTracking);
            return dataSet;

        }
    }
}
