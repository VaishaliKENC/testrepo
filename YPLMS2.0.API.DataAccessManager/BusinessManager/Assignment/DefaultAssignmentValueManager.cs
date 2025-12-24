using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.Assignment
{
    public class DefaultAssignmentValueManager : IManager<DefaultAssignmentValue, DefaultAssignmentValue.Method, DefaultAssignmentValue.ListMethod>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DefaultAssignmentValueManager()
        {

        }

        /// <summary>
        /// Use for FindByname,Read,Add,Update,Delete transactions
        /// </summary>
        /// <param name="pEntContModule"></param>
        /// <param name="pMethod"></param>
        /// <returns>ContentModule object</returns>
        public DefaultAssignmentValue Execute(DefaultAssignmentValue pEntCurActivity, DefaultAssignmentValue.Method pMethod)
        {
            DefaultAssignmentValue entDefaultAssignmentValue = new DefaultAssignmentValue();
            DefaultAssignmentValueAdaptor adaptorDefaultAssignmentValue = new DefaultAssignmentValueAdaptor();
            switch (pMethod)
            {
                case DefaultAssignmentValue.Method.delete:
                    entDefaultAssignmentValue = adaptorDefaultAssignmentValue.delete(pEntCurActivity);
                    break;
                default:
                    break;
            }
            return entDefaultAssignmentValue;
        }

        /// <summary>
        /// Used for finding ContentModule List ByName+Type and Get all ContentModuleList
        /// </summary>
        /// <param name="pEntContModule"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of ContentModule objects</returns>
        public List<DefaultAssignmentValue> Execute(DefaultAssignmentValue pEntDefaultAssignmentValue, DefaultAssignmentValue.ListMethod pMethod)
        {
            List<DefaultAssignmentValue> entListDefaultAssignmentValue = new List<DefaultAssignmentValue>();
            DefaultAssignmentValueAdaptor adaptorDefaultAssignmentValue = new DefaultAssignmentValueAdaptor();
            switch (pMethod)
            {
                case DefaultAssignmentValue.ListMethod.GetDefaultValueList:
                    entListDefaultAssignmentValue = adaptorDefaultAssignmentValue.GetAllDefaultAssignmentValue(pEntDefaultAssignmentValue);
                    break;
                default:
                    break;
            }
            return entListDefaultAssignmentValue;
        }

        public List<DefaultAssignmentValue> Execute(List<DefaultAssignmentValue> pEntDefaultAssignmentValue, DefaultAssignmentValue.ListMethod pMethod)
        {
            List<DefaultAssignmentValue> entListDefaultAssignmentValue = new List<DefaultAssignmentValue>();
            DefaultAssignmentValueAdaptor adaptorDefaultAssignmentValue = new DefaultAssignmentValueAdaptor();
            switch (pMethod)
            {
                case DefaultAssignmentValue.ListMethod.BulkAddDefaultValueList:
                    entListDefaultAssignmentValue = adaptorDefaultAssignmentValue.BulkUpdateDefaultAssignmentValue(pEntDefaultAssignmentValue);
                    break;
                default:
                    break;
            }
            return entListDefaultAssignmentValue;
        }



        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="CurriculumActivity"></typeparam>
        /// <param name="pEntBase">CurriculumActivity object</param>
        /// <param name="pMethod">CurriculumActivity.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(DefaultAssignmentValue pEntBase, DefaultAssignmentValue.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<DefaultAssignmentValue> listDefaultAssignmentValue = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<DefaultAssignmentValue>(listDefaultAssignmentValue);
            return dataSet;

        }
    }
}
