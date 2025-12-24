using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.Client
{
    /// <summary>
    /// class ClientManager
    /// </summary>
    public class ClientFeatureManager : IManager<ClientFeature, ClientFeature.Method, ClientFeature.ListMethod>
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ClientFeatureManager()
        {

        }

        /// <summary>
        /// Use to execute Add,Update,Get,Delete transactions on Client object. 
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <param name="pMethod"></param>
        /// <returns>Client object info</returns>
        public ClientFeature Execute(ClientFeature pEntClientFeature, ClientFeature.Method pMethod)
        {
            ClientFeature entClientFeatureReturn = null;
            ClientFeatureDAM adaptorClientFeature = new ClientFeatureDAM();
            string strClientId = pEntClientFeature.ID;
            switch (pMethod)
            {
                case ClientFeature.Method.Get:
                    entClientFeatureReturn = adaptorClientFeature.Get(pEntClientFeature);
                    break;
                case ClientFeature.Method.Add:
                    entClientFeatureReturn = adaptorClientFeature.Add(pEntClientFeature);
                    break;
                case ClientFeature.Method.Update:
                    entClientFeatureReturn = adaptorClientFeature.UpdateClientFeature(pEntClientFeature, ClientFeature.Method.Update);

                    break;
                //case ClientFeature.Method.Delete:
                //    entClientFeatureReturn = adaptorClientFeature.Delete(pEntClientFeature);
                //    break;
                default:
                    entClientFeatureReturn = null;
                    break;
            }
            adaptorClientFeature = null;
            return entClientFeatureReturn;
        }

        /// <summary>
        /// Returns List of client object.
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of Client objects</returns>
        public List<ClientFeature> Execute(ClientFeature pEntClientFeature, ClientFeature.ListMethod pMethod)
        {
            List<ClientFeature> entListClientFeature = null;
            ClientFeatureDAM adaptorClientFeature = new ClientFeatureDAM();
            switch (pMethod)
            {
                case ClientFeature.ListMethod.GetAll:
                    entListClientFeature = adaptorClientFeature.GetAll(pEntClientFeature);
                    break;

                default:
                    break;
            }
            return entListClientFeature;
        }

        public DataSet ExecuteDataSet(ClientFeature pEntBase, ClientFeature.ListMethod pMethod)
        {
            DataSet dataSet = null;
            ClientFeatureDAM adaptorClientFeature = new ClientFeatureDAM();

            switch (pMethod)
            {

                case ClientFeature.ListMethod.IS_Exist_YPTAB_IperformRegKey:
                    dataSet = adaptorClientFeature.IsYPTab_Iperform_Key_Exist(pEntBase);
                    break;
                case ClientFeature.ListMethod.IS_Exist_YPTAB_RegKey:
                    dataSet = adaptorClientFeature.IS_Exist_YPTAB_RegKey(pEntBase);
                    break;
                case ClientFeature.ListMethod.IS_Exist_IperformRegKey:
                    dataSet = adaptorClientFeature.IS_Exist_IperformRegKey(pEntBase);
                    break;
                case ClientFeature.ListMethod.Get_YPTAB_IperformRegKey:
                    dataSet = adaptorClientFeature.GetYPTab_Iperform_Key(pEntBase);
                    break;
                default:
                    List<ClientFeature> listClient = Execute(pEntBase, pMethod);
                    Converter dsConverter = new Converter();
                    dataSet = dsConverter.ConvertToDataSet<ClientFeature>(listClient);
                    break;
            }
            return dataSet;
        }

        public static bool GetGRSIFeature(string ClientId)
        {
            ClientFeatureManager _objClientCourseFeatureManager = new ClientFeatureManager();

            ClientFeature _entClientAssLock = new ClientFeature();
            _entClientAssLock.ClientId = ClientId;
            _entClientAssLock.ClientFeatureID = ClientFeature.FEA_ID_ASSESSMENT_LOCK;

            _entClientAssLock = _objClientCourseFeatureManager.Execute(_entClientAssLock, ClientFeature.Method.Get);
            if (_entClientAssLock != null)
            {
                if (_entClientAssLock.IsActive)
                    return true;
                else
                    return false;

            }
            return false;
        }
    }
}
