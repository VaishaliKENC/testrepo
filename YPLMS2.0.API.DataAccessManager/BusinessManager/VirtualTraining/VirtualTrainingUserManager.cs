using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.DataAccessManager.VirtualTraining;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.VirtualTraining
{
    public class VirtualTrainingUserManager : IManager<VirtualTrainingUserMaster, VirtualTrainingUserMaster.Method, VirtualTrainingUserMaster.ListMethod>
    {
        /// <summary>
        /// Default constructor for VirtualTrainingUserManager
        /// </summary>
        public VirtualTrainingUserManager()
        { }

        /// <summary>
        /// Use for Read,Add,Update,Delete VirtualTrainingUserMaster transactions
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <param name="pMethod"></param>
        /// <returns>VirtualTrainingUserMaster object</returns>
        public VirtualTrainingUserMaster Execute(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster, VirtualTrainingUserMaster.Method pMethod)
        {
            VirtualTrainingUserMaster entVirtualTrainingUserMasterReturn = null;
            VirtualTrainingUserAdaptor adaptorVirtualTrainingUserMaster = new VirtualTrainingUserAdaptor();
            switch (pMethod)
            {
                case VirtualTrainingUserMaster.Method.Get:
                    entVirtualTrainingUserMasterReturn = adaptorVirtualTrainingUserMaster.Get(pEntVirtualTrainingUserMaster);
                    break;
                case VirtualTrainingUserMaster.Method.Add:
                    entVirtualTrainingUserMasterReturn = adaptorVirtualTrainingUserMaster.Add(pEntVirtualTrainingUserMaster);
                    break;
                case VirtualTrainingUserMaster.Method.Update:
                    entVirtualTrainingUserMasterReturn = adaptorVirtualTrainingUserMaster.Update(pEntVirtualTrainingUserMaster);
                    break;
                case VirtualTrainingUserMaster.Method.Delete:
                    entVirtualTrainingUserMasterReturn = adaptorVirtualTrainingUserMaster.Delete(pEntVirtualTrainingUserMaster);
                    break;
                case VirtualTrainingUserMaster.Method.AddGroupAdmin:
                    entVirtualTrainingUserMasterReturn = adaptorVirtualTrainingUserMaster.AddGroupAdmin(pEntVirtualTrainingUserMaster);
                    break;
                case VirtualTrainingUserMaster.Method.DeleteGroupAdmin:
                    entVirtualTrainingUserMasterReturn = adaptorVirtualTrainingUserMaster.DeleteGroupAdmin(pEntVirtualTrainingUserMaster);
                    break;
                default:
                    entVirtualTrainingUserMasterReturn = null;
                    break;
            }
            return entVirtualTrainingUserMasterReturn;
        }

        /// <summary>
        /// Used for finding VirtualTrainingUserMaster List and Get all VirtualTrainingUserMaster List
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of VirtualTrainingUserMaster objects</returns>
        public List<VirtualTrainingUserMaster> Execute(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster, VirtualTrainingUserMaster.ListMethod pMethod)
        {
            List<VirtualTrainingUserMaster> entListVirtualTrainingUserMaster = new List<VirtualTrainingUserMaster>();
            VirtualTrainingUserAdaptor adaptorVirtualTrainingUserMaster = new VirtualTrainingUserAdaptor();
            switch (pMethod)
            {
                case VirtualTrainingUserMaster.ListMethod.GetAll:
                    entListVirtualTrainingUserMaster = adaptorVirtualTrainingUserMaster.GetAll(pEntVirtualTrainingUserMaster);
                    break;
                case VirtualTrainingUserMaster.ListMethod.GetUserMappingAll:
                    entListVirtualTrainingUserMaster = adaptorVirtualTrainingUserMaster.GetUserMappingAll(pEntVirtualTrainingUserMaster);
                    break;
                //case VirtualTrainingUserMaster.ListMethod.GetAllAttendeeList:
                //    entListVirtualTrainingUserMaster = adaptorVirtualTrainingUserMaster.GetAssignedAttendeeByTrainingID(pEntVirtualTrainingUserMaster);
                //    break;
                //case VirtualTrainingUserMaster.ListMethod.GetAllUsers:
                //    entListVirtualTrainingUserMaster = adaptorVirtualTrainingUserMaster.GetUserByName(pEntVirtualTrainingUserMaster);
                //    break;
                case VirtualTrainingUserMaster.ListMethod.GetAllTrainingUsers:
                    entListVirtualTrainingUserMaster = adaptorVirtualTrainingUserMaster.GetAllTrainingUsers(pEntVirtualTrainingUserMaster);
                    break;
                //
                case VirtualTrainingUserMaster.ListMethod.GetWebServiceDefaultUser:
                    entListVirtualTrainingUserMaster = adaptorVirtualTrainingUserMaster.GetWebServiceDefaultUser(pEntVirtualTrainingUserMaster);
                    break;
                case VirtualTrainingUserMaster.ListMethod.AdminGetAllUsers:
                    entListVirtualTrainingUserMaster = adaptorVirtualTrainingUserMaster.AdminGetAllUsers(pEntVirtualTrainingUserMaster);
                    break;

                case VirtualTrainingUserMaster.ListMethod.AdminMappedGetAllUsers:
                    entListVirtualTrainingUserMaster = adaptorVirtualTrainingUserMaster.GetAllGroupAdminMapping(pEntVirtualTrainingUserMaster);
                    break;

                default:
                    break;
            }
            return entListVirtualTrainingUserMaster;
        }


        public List<VirtualTrainingUserMaster> Execute(List<VirtualTrainingUserMaster> pEntListQues, VirtualTrainingUserMaster.ListMethod pMethod)
        {
            List<VirtualTrainingUserMaster> entListVirtualTrainingUserMaster = new List<VirtualTrainingUserMaster>();
            VirtualTrainingUserAdaptor adaptorVirtualTrainingUserMaster = new VirtualTrainingUserAdaptor();
            switch (pMethod)
            {
                //case VirtualTrainingUserMaster.ListMethod.BulkUpdateVirtualTrainingUserMaster:
                //    entListVirtualTrainingUserMaster = adaptorVirtualTrainingUserMaster.BulkUpdateVirtualTrainingUserMaster(pEntListQues);
                //    break;

                //case VirtualTrainingUserMaster.ListMethod.AssignedAttendeeDelete:
                //    entListVirtualTrainingUserMaster = adaptorVirtualTrainingUserMaster.AssignedAttendeeDelete(pEntListQues);
                //    break;

                default:
                    entListVirtualTrainingUserMaster = null;
                    break;
            }
            return entListVirtualTrainingUserMaster;
        }


        public DataSet ExecuteDataSet(VirtualTrainingUserMaster pEntBase, VirtualTrainingUserMaster.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<VirtualTrainingUserMaster> listVirtualTrainingUserMaster = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<VirtualTrainingUserMaster>(listVirtualTrainingUserMaster);
            return dataSet;
        }
        /// <summary>
        /// Use for GetUserNameAvailable
        /// </summary>
        /// <param name="pEntUserMaster"></param>
        /// <param name="pMethod"></param>
        /// <returns>object</returns>
        public object ExecuteScalar(VirtualTrainingUserMaster pEntUserMaster, VirtualTrainingUserMaster.Method pMethod)
        {
            object obj = null;
            VirtualTrainingUserAdaptor adaptorUserMaster = new VirtualTrainingUserAdaptor();
            switch (pMethod)
            {
                case VirtualTrainingUserMaster.Method.GetUserNameAvailable:
                    obj = adaptorUserMaster.GetUserNameAvailable(pEntUserMaster);
                    break;
                default:
                    break;
            }
            return obj;
        }
    }
}
