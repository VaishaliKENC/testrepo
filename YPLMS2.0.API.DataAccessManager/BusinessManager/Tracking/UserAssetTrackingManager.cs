using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager
{
    public class UserAssetTrackingManager : IManager<UserAssetTracking, UserAssetTracking.Method, UserAssetTracking.ListMethod>
    {
        /// <summary>
        /// Execute: GetAll
        /// </summary>
        /// <param name="pEntUserAssetTracking"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        ///  Thread entThred
        public List<UserAssetTracking> Execute(UserAssetTracking pEntUserAssetTracking, UserAssetTracking.ListMethod pMethod)
        {
            return null;
        }

        /// <summary>
        /// Execute for BulkUpdateAdd
        /// </summary>
        /// <param name="pEntListUserAssetTracking"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<UserAssetTracking> Execute(List<UserAssetTracking> pEntListUserAssetTracking, UserAssetTracking.ListMethod pMethod)
        {
            List<UserAssetTracking> entListUserAssetTracking = null;
            UserAssetTrackingDAM adaptorUserAssetTracking = new UserAssetTrackingDAM();
            if (pEntListUserAssetTracking.Count > 0)
            {
                switch (pMethod)
                {
                    case UserAssetTracking.ListMethod.MarkCompleted:
                        entListUserAssetTracking = adaptorUserAssetTracking.BulkMarkCompleted(pEntListUserAssetTracking, false);

                        break;
                    case UserAssetTracking.ListMethod.BulkMarkCompleted:
                        entListUserAssetTracking = adaptorUserAssetTracking.BulkMarkCompleted(pEntListUserAssetTracking, true);

                        break;
                    default:
                        break;
                }
            }
            //foreach (UserAssetTracking entTracking in pEntListUserAssetTracking)
            //{
            //    GotoDynamicAssignment(entTracking);
            //}
            return entListUserAssetTracking;
        }

        /// <summary>
        /// Execute: Get/Add
        /// </summary>
        /// <param name="pEntUserAssetTracking"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public UserAssetTracking Execute(UserAssetTracking pEntUserAssetTracking, UserAssetTracking.Method pMethod)
        {
            UserAssetTracking entUserAssetTracking = null;
            UserAssetTrackingDAM UserAssetTrackingAdaptor = new UserAssetTrackingDAM();

            switch (pMethod)
            {
                case UserAssetTracking.Method.Add:
                    entUserAssetTracking = UserAssetTrackingAdaptor.AddUpdateUserAssetTracking(pEntUserAssetTracking);
                    //GotoDynamicAssignment(entUserAssetTracking);
                    break;
                case UserAssetTracking.Method.UpdateScannedFileName:
                    entUserAssetTracking = UserAssetTrackingAdaptor.UpdateScannedFileName(pEntUserAssetTracking);
                    break;
                case UserAssetTracking.Method.UpdateVideoTracking:
                    entUserAssetTracking = UserAssetTrackingAdaptor.UpdateUserAssetTrackingVideo(pEntUserAssetTracking);
                    break;
                case UserAssetTracking.Method.UpdateVideoBookmark:
                    entUserAssetTracking = UserAssetTrackingAdaptor.UpdateVideoBookmark(pEntUserAssetTracking);
                    break;
                case UserAssetTracking.Method.Get:
                    entUserAssetTracking = UserAssetTrackingAdaptor.Get(pEntUserAssetTracking);
                    break;
                default:
                    entUserAssetTracking = null;
                    break;
            }
            return entUserAssetTracking;
        }


        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="UserAssetTracking"></typeparam>
        /// <param name="pEntBase">UserAssetTracking object</param>
        /// <param name="pMethod">UserAssetTracking.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(UserAssetTracking pEntBase, UserAssetTracking.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<UserAssetTracking> listUserAssetTracking = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<UserAssetTracking>(listUserAssetTracking);
            return dataSet;

        }
    }
}
