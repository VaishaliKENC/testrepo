using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.DataAccessManager.VirtualTraining;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.VirtualTraining
{
    public class VirtualTrainingAttendeeManager : IManager<VirtualTrainingAttendeeMaster, VirtualTrainingAttendeeMaster.Method, VirtualTrainingAttendeeMaster.ListMethod>
    {
        /// <summary>
        /// Default constructor for VirtualTrainingAttendeeManager
        /// </summary>
        public VirtualTrainingAttendeeManager()
        { }

        /// <summary>
        /// Use for Read,Add,Update,Delete VirtualTrainingAttendeeMaster transactions
        /// </summary>
        /// <param name="pEntVirtualTrainingAttendeeMaster"></param>
        /// <param name="pMethod"></param>
        /// <returns>VirtualTrainingAttendeeMaster object</returns>
        public VirtualTrainingAttendeeMaster Execute(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster, VirtualTrainingAttendeeMaster.Method pMethod)
        {
            VirtualTrainingAttendeeMaster entVirtualTrainingAttendeeMasterReturn = null;            
            VirtualTrainingAttendeeAdaptor adaptorVirtualTrainingAttendeeMaster = new VirtualTrainingAttendeeAdaptor();
            switch (pMethod)
            {
                case VirtualTrainingAttendeeMaster.Method.Get:
                    entVirtualTrainingAttendeeMasterReturn = adaptorVirtualTrainingAttendeeMaster.Get(pEntVirtualTrainingAttendeeMaster);
                    break;
                case VirtualTrainingAttendeeMaster.Method.Add:
                    entVirtualTrainingAttendeeMasterReturn = adaptorVirtualTrainingAttendeeMaster.Add(pEntVirtualTrainingAttendeeMaster);
                    break;
                case VirtualTrainingAttendeeMaster.Method.Update:
                    entVirtualTrainingAttendeeMasterReturn = adaptorVirtualTrainingAttendeeMaster.Update(pEntVirtualTrainingAttendeeMaster);
                    break;
                case VirtualTrainingAttendeeMaster.Method.Delete:
                    entVirtualTrainingAttendeeMasterReturn = adaptorVirtualTrainingAttendeeMaster.Delete(pEntVirtualTrainingAttendeeMaster);
                    break;

                case VirtualTrainingAttendeeMaster.Method.UpdateSessionAttendeeList:
                    entVirtualTrainingAttendeeMasterReturn = adaptorVirtualTrainingAttendeeMaster.UpdateSessionAttendeeList(pEntVirtualTrainingAttendeeMaster);
                    break;
                case VirtualTrainingAttendeeMaster.Method.CheckEmailExist:
                    entVirtualTrainingAttendeeMasterReturn = adaptorVirtualTrainingAttendeeMaster.CheckEmailExist(pEntVirtualTrainingAttendeeMaster);
                    break;
                default:
                    entVirtualTrainingAttendeeMasterReturn = null;
                    break;
            }
            return entVirtualTrainingAttendeeMasterReturn;
        }

        /// <summary>
        /// Used for finding VirtualTrainingAttendeeMaster List and Get all VirtualTrainingAttendeeMaster List
        /// </summary>
        /// <param name="pEntVirtualTrainingAttendeeMaster"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of VirtualTrainingAttendeeMaster objects</returns>
        public List<VirtualTrainingAttendeeMaster> Execute(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster, VirtualTrainingAttendeeMaster.ListMethod pMethod)
        {
            List<VirtualTrainingAttendeeMaster> entListVirtualTrainingAttendeeMaster = new List<VirtualTrainingAttendeeMaster>();
            VirtualTrainingAttendeeAdaptor adaptorVirtualTrainingAttendeeMaster = new VirtualTrainingAttendeeAdaptor();
            switch (pMethod)
            {
                case VirtualTrainingAttendeeMaster.ListMethod.GetAll:
                    entListVirtualTrainingAttendeeMaster = adaptorVirtualTrainingAttendeeMaster.GetAll(pEntVirtualTrainingAttendeeMaster);
                    break;
                case VirtualTrainingAttendeeMaster.ListMethod.GetAllAttendeeList:
                    entListVirtualTrainingAttendeeMaster = adaptorVirtualTrainingAttendeeMaster.GetAssignedAttendeeByTrainingID(pEntVirtualTrainingAttendeeMaster);
                    break;
                case VirtualTrainingAttendeeMaster.ListMethod.GetAllUsers:
                    entListVirtualTrainingAttendeeMaster = adaptorVirtualTrainingAttendeeMaster.GetUserByName(pEntVirtualTrainingAttendeeMaster);
                    break;
                case VirtualTrainingAttendeeMaster.ListMethod.GetAllACCEPTANDREGISTER:
                    entListVirtualTrainingAttendeeMaster = adaptorVirtualTrainingAttendeeMaster.GetAllACCEPTANDREGISTER(pEntVirtualTrainingAttendeeMaster);
                    break;
                default:
                    break;
            }
            return entListVirtualTrainingAttendeeMaster;
        }


        public List<VirtualTrainingAttendeeMaster> Execute(List<VirtualTrainingAttendeeMaster> pEntListQues, VirtualTrainingAttendeeMaster.ListMethod pMethod)
        {
            List<VirtualTrainingAttendeeMaster> entListVirtualTrainingAttendeeMaster = new List<VirtualTrainingAttendeeMaster>();
            VirtualTrainingAttendeeAdaptor adaptorVirtualTrainingAttendeeMaster = new VirtualTrainingAttendeeAdaptor();
            switch (pMethod)
            {
                case VirtualTrainingAttendeeMaster.ListMethod.BulkUpdateVirtualTrainingAttendeeMaster:
                    entListVirtualTrainingAttendeeMaster = adaptorVirtualTrainingAttendeeMaster.BulkUpdateVirtualTrainingAttendeeMaster(pEntListQues);
                    break;

                case VirtualTrainingAttendeeMaster.ListMethod.AssignedAttendeeDelete:
                    entListVirtualTrainingAttendeeMaster = adaptorVirtualTrainingAttendeeMaster.AssignedAttendeeDelete(pEntListQues);
                    break;
                case VirtualTrainingAttendeeMaster.ListMethod.BulkUpdateVirtualTrainingFailureList:
                    entListVirtualTrainingAttendeeMaster = adaptorVirtualTrainingAttendeeMaster.BulkUpdateVirtualTrainingFailureList(pEntListQues);
                    break;
                default:
                    entListVirtualTrainingAttendeeMaster = null;
                    break;
            }
            return entListVirtualTrainingAttendeeMaster;
        }


        public DataSet ExecuteDataSet(VirtualTrainingAttendeeMaster pEntBase, VirtualTrainingAttendeeMaster.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<VirtualTrainingAttendeeMaster> listVirtualTrainingAttendeeMaster = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<VirtualTrainingAttendeeMaster>(listVirtualTrainingAttendeeMaster);
            return dataSet;
        }
    }
}
