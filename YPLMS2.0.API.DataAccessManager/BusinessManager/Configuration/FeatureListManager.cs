using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager
{
    /// <summary>
    /// class FeatureListManager
    /// </summary>
    public class FeatureListManager : IManager<FeatureList, FeatureList.Method, FeatureList.ListMethod>
    {
        /// <summary>
        /// Default constructor for FeatureListManager
        /// </summary>
        public FeatureListManager()
        {
        }
        /// <summary>
        /// Use for Read,Add,Update,Delete FeatureList transactions
        /// </summary>
        /// <param name="pEntFeatureList"></param>
        /// <param name="pMethod"></param>
        /// <returns>FeatureList object</returns>
        public FeatureList Execute(FeatureList pEntFeatureList, FeatureList.Method pMethod)
        {
            FeatureList entFeatureListReturn = null;
            FeatureListAdaptor adaptorFeatureList = new FeatureListAdaptor();
            switch (pMethod)
            {
                case FeatureList.Method.Get:
                    entFeatureListReturn = adaptorFeatureList.Get(pEntFeatureList);
                    break;
                case FeatureList.Method.Add:
                    entFeatureListReturn = adaptorFeatureList.Add(pEntFeatureList);
                    break;
                case FeatureList.Method.Update:
                    entFeatureListReturn = adaptorFeatureList.Update(pEntFeatureList);
                    break;
                case FeatureList.Method.Delete:
                    entFeatureListReturn = adaptorFeatureList.Delete(pEntFeatureList);
                    break;
                default:
                    entFeatureListReturn = null;
                    break;
            }
            return entFeatureListReturn;
        }
        /// <summary>
        /// Used for finding FeatureList List and Get all FeatureList List
        /// </summary>
        /// <param name="pEntFeatureList"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of FeatureList objects</returns>
        public List<FeatureList> Execute(FeatureList pEntFeatureList, FeatureList.ListMethod pMethod)
        {
            List<FeatureList> entListFeatureList = new List<FeatureList>();
            FeatureListAdaptor adaptorFeatureList = new FeatureListAdaptor();
            switch (pMethod)
            {
                case FeatureList.ListMethod.GetAll:
                    entListFeatureList = adaptorFeatureList.GetAll(pEntFeatureList);
                    break;
                default:
                    break;
            }
            return entListFeatureList;
        }
        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="FeatureList"></typeparam>
        /// <param name="pEntBase">FeatureList object</param>
        /// <param name="pMethod">FeatureList.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(FeatureList pEntBase, FeatureList.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<FeatureList> listFeatureList = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<FeatureList>(listFeatureList);
            return dataSet;
        }
    }
}
