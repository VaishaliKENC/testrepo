using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public class LookUpManager : IManager<Lookup, Lookup.Method, Lookup.ListMethod>
    {
        /// <summary>
        /// Default LookUpManager constructor
        /// </summary>
        public LookUpManager()
        {
        }

        /// <summary>
        /// Used for list of lookup objects by type
        /// </summary>
        /// <param name="pEntLookUp"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of Lookup objects</returns>
        public List<Lookup> Execute(Lookup pEntLookUp, Lookup.ListMethod pMethod)
        {
            List<Lookup> entListLookUp = null;
            LookupAdaptor adaptorLookUp = new LookupAdaptor();
            switch (pMethod)
            {
                case Lookup.ListMethod.GetStatusAndTypeByLookupType:
                    entListLookUp = adaptorLookUp.GetStatusAndTypeByLookupType(pEntLookUp);
                    break;
                case Lookup.ListMethod.GetAllByLookupType:
                    {
                        List<Lookup> entListLookUpInner = null;
                        entListLookUpInner = adaptorLookUp.GetLookupsByType(pEntLookUp);

                        #region Added By Rajendra on 16 Oct for Certification Module - Enable Disable
                        if (pEntLookUp.LookupType == LookupType.ActivityType)
                        {
                            entListLookUp = new List<Lookup>();
                            Client _objClient = new Client();
                            _objClient.ID = pEntLookUp.ClientId;
                            ClientDAM _objManager = new ClientDAM();
                            _objClient = _objManager.GetClientByID(_objClient); //Execute(_objClient, Client.Method.Get); comment by vinod
                            if (!(_objClient.IsCertifcationEnabled))
                            {
                                foreach (Lookup _entLookUp in entListLookUpInner)
                                {
                                    if (_entLookUp.LookupText.Trim() == ActivityContentType.Policy.ToString().Trim() || _entLookUp.LookupText == ActivityContentType.Certification.ToString().Trim())
                                    {
                                    }
                                    else
                                    {
                                        entListLookUp.Add(_entLookUp);
                                    }
                                }
                            }
                            else
                            {
                                entListLookUp = entListLookUpInner;
                            }

                        }
                        else
                        {
                            entListLookUp = new List<Lookup>();
                            entListLookUp = entListLookUpInner;
                        }

                        #endregion Added By Rajendra on 16 Oct for Certification Module - Enable Disable
                    }



                    break;
                default:
                    break;
            }
            return entListLookUp;
        }

        /// <summary>
        /// Used for add,Update,Delete,read transactions.
        /// </summary>
        /// <param name="pEntLookUp"></param>
        /// <param name="pMethod"></param>
        /// <returns>Lookup object</returns>
        public Lookup Execute(Lookup pEntLookUp, Lookup.Method pMethod)
        {
            return null;
        }


        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="Lookup"></typeparam>
        /// <param name="pEntBase">Lookup object</param>
        /// <param name="pMethod">Lookup.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(Lookup pEntBase, Lookup.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<Lookup> listLookup = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<Lookup>(listLookup);
            return dataSet;

        }
    }
}
