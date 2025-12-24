using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager
{
    public class UserAssetVideoTrackingManager : IManager<UserAssetVideoTracking, UserAssetVideoTracking.Method, UserAssetVideoTracking.ListMethod>
    {
        /// <summary>
        /// Execute: GetAll
        /// </summary>
        /// <param name="pEntUserAssetVideoTracking"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        ///  Thread entThred
        public List<UserAssetVideoTracking> Execute(UserAssetVideoTracking pEntUserAssetVideoTracking, UserAssetVideoTracking.ListMethod pMethod)
        {
            return null;
        }

        /// <summary>
        /// Execute for BulkUpdateAdd
        /// </summary>
        /// <param name="pEntListUserAssetVideoTracking"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<UserAssetVideoTracking> Execute(List<UserAssetVideoTracking> pEntListUserAssetVideoTracking, UserAssetVideoTracking.ListMethod pMethod)
        {
            List<UserAssetVideoTracking> entListUserAssetVideoTracking = null;
            UserAssetVideoTrackingDAM adaptorUserAssetVideoTracking = new UserAssetVideoTrackingDAM();
            ////if (pEntListUserAssetVideoTracking.Count > 0)
            ////{
            ////    switch (pMethod)
            ////    {
            ////        case UserAssetVideoTracking.ListMethod.MarkCompleted:
            ////            entListUserAssetVideoTracking = adaptorUserAssetVideoTracking.BulkMarkCompleted(pEntListUserAssetVideoTracking, false);

            ////            break;
            ////        case UserAssetVideoTracking.ListMethod.BulkMarkCompleted:
            ////            entListUserAssetVideoTracking = adaptorUserAssetVideoTracking.BulkMarkCompleted(pEntListUserAssetVideoTracking, true);

            ////            break;
            ////        default:
            ////            break;
            ////    }
            ////}

            return entListUserAssetVideoTracking;
        }

        /// <summary>
        /// Execute: Get/Add
        /// </summary>
        /// <param name="pEntUserAssetVideoTracking"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public UserAssetVideoTracking Execute(UserAssetVideoTracking pEntUserAssetVideoTracking, UserAssetVideoTracking.Method pMethod)
        {
            UserAssetVideoTracking entUserAssetVideoTracking = null;
            UserAssetVideoTrackingDAM UserAssetVideoTrackingAdaptor = new UserAssetVideoTrackingDAM();

            switch (pMethod)
            {
                case UserAssetVideoTracking.Method.Add:
                    entUserAssetVideoTracking = UserAssetVideoTrackingAdaptor.AddUpdateUserAssetVideoTracking(pEntUserAssetVideoTracking);

                    break;
                case UserAssetVideoTracking.Method.Update:
                    entUserAssetVideoTracking = UserAssetVideoTrackingAdaptor.Update(pEntUserAssetVideoTracking);
                    break;
                case UserAssetVideoTracking.Method.Get:
                    entUserAssetVideoTracking = UserAssetVideoTrackingAdaptor.Get(pEntUserAssetVideoTracking);
                    break;
                case UserAssetVideoTracking.Method.GetVideoTracking:
                    entUserAssetVideoTracking = UserAssetVideoTrackingAdaptor.GetVideoTracking(pEntUserAssetVideoTracking);
                    break;
                default:
                    entUserAssetVideoTracking = null;
                    break;
            }
            return entUserAssetVideoTracking;
        }


        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="UserAssetVideoTracking"></typeparam>
        /// <param name="pEntBase">UserAssetVideoTracking object</param>
        /// <param name="pMethod">UserAssetVideoTracking.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(UserAssetVideoTracking pEntBase, UserAssetVideoTracking.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<UserAssetVideoTracking> listUserAssetVideoTracking = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<UserAssetVideoTracking>(listUserAssetVideoTracking);
            return dataSet;

        }
    }
}
