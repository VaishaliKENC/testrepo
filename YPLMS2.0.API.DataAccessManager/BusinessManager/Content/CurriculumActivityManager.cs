using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.DataAccessManager.Content;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager
{
    /// <summary>
    /// class ContentModuleManager
    /// </summary>
    public class CurriculumActivityManager : IManager<CurriculumActivity, CurriculumActivity.Method, CurriculumActivity.ListMethod>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public CurriculumActivityManager()
        {

        }

        /// <summary>
        /// Use for FindByname,Read,Add,Update,Delete transactions
        /// </summary>
        /// <param name="pEntContModule"></param>
        /// <param name="pMethod"></param>
        /// <returns>ContentModule object</returns>
        public CurriculumActivity Execute(CurriculumActivity pEntCurActivity, CurriculumActivity.Method pMethod)
        {
            CurriculumActivity entCurActivityReturn = new CurriculumActivity();
            CurriculumActivityAdaptor adaptorCurActivity = new CurriculumActivityAdaptor();
            switch (pMethod)
            {
                case CurriculumActivity.Method.Get:
                    entCurActivityReturn = adaptorCurActivity.GetCurriculumById(pEntCurActivity);
                    break;

                default:
                    entCurActivityReturn = null;
                    break;
            }
            return entCurActivityReturn;
        }

        /// <summary>
        /// Used for finding ContentModule List ByName+Type and Get all ContentModuleList
        /// </summary>
        /// <param name="pEntContModule"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of ContentModule objects</returns>
        public List<CurriculumActivity> Execute(CurriculumActivity pEntCurActivity, CurriculumActivity.ListMethod pMethod)
        {
            List<CurriculumActivity> entListCurActivity = new List<CurriculumActivity>();
            CurriculumActivityAdaptor adaptorCurActivity = new CurriculumActivityAdaptor();
            switch (pMethod)
            {
                case CurriculumActivity.ListMethod.GetAll:
                    entListCurActivity = adaptorCurActivity.GetAllCurriculumActivities(pEntCurActivity);
                    break;
                case CurriculumActivity.ListMethod.GetAllAttempt:
                    entListCurActivity = adaptorCurActivity.GetAllCurriculumActivitiesByAttempt(pEntCurActivity);
                    break;
                case CurriculumActivity.ListMethod.GetAll_Curriculum:
                    entListCurActivity = adaptorCurActivity.GetAllCurriculum(pEntCurActivity);
                    break;
                default:
                    break;
            }
            return entListCurActivity;
        }

        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="CurriculumActivity"></typeparam>
        /// <param name="pEntBase">CurriculumActivity object</param>
        /// <param name="pMethod">CurriculumActivity.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(CurriculumActivity pEntBase, CurriculumActivity.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<CurriculumActivity> listCurriculumActivity = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<CurriculumActivity>(listCurriculumActivity);
            return dataSet;

        }
    }
}
