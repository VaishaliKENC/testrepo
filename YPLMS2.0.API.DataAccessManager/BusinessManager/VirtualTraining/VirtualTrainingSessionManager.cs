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
    public class VirtualTrainingSessionManager : IManager<VirtualTrainingSessionMaster, VirtualTrainingSessionMaster.Method, VirtualTrainingSessionMaster.ListMethod>
    {
        /// <summary>
        /// Default constructor for VirtualTrainingSessionManager
        /// </summary>
        public VirtualTrainingSessionManager()
        {
        }

        /// <summary>
        /// Use for Read,Add,Update,Delete RefMaterialMaster transactions
        /// </summary>
        /// <param name="pEntRefMaterialMaster"></param>
        /// <param name="pMethod"></param>
        /// <returns>VirtualTrainingSessionMaster object</returns>
        public VirtualTrainingSessionMaster Execute(VirtualTrainingSessionMaster pEntRefMaterialMaster, VirtualTrainingSessionMaster.Method pMethod)
        {
            VirtualTrainingSessionMaster entSessionMasterReturn = null;
            VirtualTrainingSessionAdapter adaptorSesionMaster = new VirtualTrainingSessionAdapter();
            switch (pMethod)
            {
                case VirtualTrainingSessionMaster.Method.Get:
                    entSessionMasterReturn = adaptorSesionMaster.Get(pEntRefMaterialMaster);
                    break;
                case VirtualTrainingSessionMaster.Method.GetAdminAccountUserID:
                    entSessionMasterReturn = adaptorSesionMaster.GetAdminAccountUserID(pEntRefMaterialMaster);
                    break;
                case VirtualTrainingSessionMaster.Method.Add:
                    entSessionMasterReturn = adaptorSesionMaster.Add(pEntRefMaterialMaster);
                    break;
                case VirtualTrainingSessionMaster.Method.Update:
                    entSessionMasterReturn = adaptorSesionMaster.Update(pEntRefMaterialMaster);
                    break;
                case VirtualTrainingSessionMaster.Method.Delete:
                    entSessionMasterReturn = adaptorSesionMaster.Delete(pEntRefMaterialMaster);
                    break;
                case VirtualTrainingSessionMaster.Method.UpdateCountAttended:
                    entSessionMasterReturn = adaptorSesionMaster.UpdateCountAttended(pEntRefMaterialMaster);
                    break;
                case VirtualTrainingSessionMaster.Method.UpdateRegister:
                    entSessionMasterReturn = adaptorSesionMaster.UpdateRegister(pEntRefMaterialMaster);
                    break;
                case VirtualTrainingSessionMaster.Method.UpdateTrainingTotalTime:
                    entSessionMasterReturn = adaptorSesionMaster.UpdateTrainingTotalTime(pEntRefMaterialMaster);
                    break;
                case VirtualTrainingSessionMaster.Method.UpdateNoOfRegistered:
                    entSessionMasterReturn = adaptorSesionMaster.UpdateNoOfRegistered(pEntRefMaterialMaster);
                    break;

                default:
                    entSessionMasterReturn = null;
                    break;
            }
            return entSessionMasterReturn;
        }

        /// <summary>
        /// Used for finding RefMaterialMaster List and Get all RefMaterialMaster List
        /// </summary>
        /// <param name="pEntRefMaterialMaster"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of RefMaterialMaster objects</returns>
        public List<VirtualTrainingSessionMaster> Execute(VirtualTrainingSessionMaster pEntRefMaterialMaster, VirtualTrainingSessionMaster.ListMethod pMethod)
        {
            List<VirtualTrainingSessionMaster> entListSessionMaster = new List<VirtualTrainingSessionMaster>();
            VirtualTrainingSessionAdapter adaptorSessionMaster = new VirtualTrainingSessionAdapter();
            switch (pMethod)
            {
                case VirtualTrainingSessionMaster.ListMethod.GetAll:
                    entListSessionMaster = adaptorSessionMaster.GetAll(pEntRefMaterialMaster);
                    break;

                case VirtualTrainingSessionMaster.ListMethod.GetAllSessionKey:
                    entListSessionMaster = adaptorSessionMaster.GetAllSessionKey(pEntRefMaterialMaster);
                    break;

                case VirtualTrainingSessionMaster.ListMethod.GetSystemUserGUID:
                    entListSessionMaster = adaptorSessionMaster.GetSystemUserGUID(pEntRefMaterialMaster);
                    break;

                case VirtualTrainingSessionMaster.ListMethod.GetAllSession:
                    entListSessionMaster = adaptorSessionMaster.GetAllSession(pEntRefMaterialMaster);
                    break;
                case VirtualTrainingSessionMaster.ListMethod.GetAllSelfTrainingStatus:
                    entListSessionMaster = adaptorSessionMaster.GetAllSelfTrainingStatus(pEntRefMaterialMaster);
                    break;
                case VirtualTrainingSessionMaster.ListMethod.GetAllVIRTUALTRAININGClient:
                    entListSessionMaster = adaptorSessionMaster.GetAllVIRTUALTRAININGClient(pEntRefMaterialMaster);
                    break;

                case VirtualTrainingSessionMaster.ListMethod.Search_VIRTUALTRAINING_Names:
                    entListSessionMaster = adaptorSessionMaster.Search_VIRTUALTRAINING_Names(pEntRefMaterialMaster);
                    break;

                case VirtualTrainingSessionMaster.ListMethod.GetAllForCurriculum:
                    entListSessionMaster = adaptorSessionMaster.GetAllVirtualTrainingSessionMaster_ForCurriculum(pEntRefMaterialMaster);
                    break;

                default:
                    break;
            }
            return entListSessionMaster;
        }

        public List<VirtualTrainingSessionMaster> Execute(List<VirtualTrainingSessionMaster> pEntListResource, VirtualTrainingSessionMaster.ListMethod pMethod)
        {
            List<VirtualTrainingSessionMaster> entListSessionMaster = new List<VirtualTrainingSessionMaster>();
            VirtualTrainingSessionAdapter adaptorSessionMaster = new VirtualTrainingSessionAdapter();
            switch (pMethod)
            {
                case VirtualTrainingSessionMaster.ListMethod.ActivateDeActivateStatus:
                    entListSessionMaster = adaptorSessionMaster.ActivateDeActivateStatusList(pEntListResource);
                    break;
                default:
                    entListSessionMaster = null;
                    break;
            }
            return entListSessionMaster;
        }


        public DataSet ExecuteDataSet(VirtualTrainingSessionMaster pEntBase, VirtualTrainingSessionMaster.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<VirtualTrainingSessionMaster> listSessionMaster = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<VirtualTrainingSessionMaster>(listSessionMaster);
            return dataSet;
        }
    }
}
