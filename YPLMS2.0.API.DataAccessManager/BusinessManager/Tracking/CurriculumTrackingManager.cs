using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager
{
    public class CurriculumTrackingManager : IManager<CurriculumTracking, CurriculumTracking.Method, CurriculumTracking.ListMethod>
    {

        /// <summary>
        /// Default CurriculumTrackingManager Constructor
        /// </summary>
        public CurriculumTrackingManager()
        { }

        /// <summary>
        /// Execute: GetAll
        /// </summary>
        /// <param name="pEntCurriculumTracking"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<CurriculumTracking> Execute(CurriculumTracking pEntCurriculumTracking, CurriculumTracking.ListMethod pMethod)
        {
            return null;
        }

        /// <summary>
        /// Execute: Get/Add/Update
        /// </summary>
        /// <param name="pEntCurriculumTracking"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public CurriculumTracking Execute(CurriculumTracking pEntCurriculumTracking, CurriculumTracking.Method pMethod)
        {
            CurriculumTracking entCurriculumTracking = null;
            //CurriculumTracking entTrackStatus = null;
            CurriculumTrackingDAM curriculumTrackingAdaptor = new CurriculumTrackingDAM();

            switch (pMethod)
            {
                case CurriculumTracking.Method.UpdateScannedFileName:
                    entCurriculumTracking = curriculumTrackingAdaptor.UpdateScannedFileName(pEntCurriculumTracking);
                    break;
                case CurriculumTracking.Method.DeleteAdminCurriculumTracking:
                    entCurriculumTracking = curriculumTrackingAdaptor.DeleteAdminCurriculumTracking(pEntCurriculumTracking);
                    break;
                case CurriculumTracking.Method.DeleteAdminPreviewTracking:
                    entCurriculumTracking = curriculumTrackingAdaptor.DeleteAdminPreviewTracking(pEntCurriculumTracking);
                    break;
                case CurriculumTracking.Method.GetStatus:
                    entCurriculumTracking = curriculumTrackingAdaptor.GetCurriculumTrackingByStatus(pEntCurriculumTracking);
                    break;
                default:
                    entCurriculumTracking = null;
                    break;
            }
            return entCurriculumTracking;
        }

        /// <summary>
        /// For Mark Completed.
        /// </summary>
        /// <param name="pEntListTracking"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<CurriculumTracking> Execute(List<CurriculumTracking> pEntListTracking, CurriculumTracking.ListMethod pMethod)
        {
            List<CurriculumTracking> entListTracking = new List<CurriculumTracking>();
            CurriculumTrackingDAM adaptorTracking = new CurriculumTrackingDAM();
            switch (pMethod)
            {
                case CurriculumTracking.ListMethod.MarkCompleted:
                    entListTracking = adaptorTracking.BulkUpdate(pEntListTracking, false);
                    break;
                case CurriculumTracking.ListMethod.BulkMarkCompleted:
                    entListTracking = adaptorTracking.BulkUpdate(pEntListTracking, true);
                    break;
                default:
                    entListTracking = null;
                    break;
            }
            foreach (CurriculumTracking entTracking in pEntListTracking)
            {
                GotoDynamicAssignment(entTracking);
            }
            return entListTracking;
        }

        public void GotoDynamicAssignment(CurriculumTracking pEntCurriculumTracking)
        {
            if (!(pEntCurriculumTracking.IsForAdminPreview))
            {
                BackgroundBRuleAssignmentManager entMgr = new BackgroundBRuleAssignmentManager(pEntCurriculumTracking.CurriculumId, pEntCurriculumTracking.UserID, ActivityContentType.Curriculum.ToString(), pEntCurriculumTracking.ClientId);
                entMgr.AssignActivties();
            }
        }

        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="CurriculumTracking"></typeparam>
        /// <param name="pEntBase">CurriculumTracking object</param>
        /// <param name="pMethod">CurriculumTracking.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(CurriculumTracking pEntBase, CurriculumTracking.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<CurriculumTracking> listCurriculumTracking = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<CurriculumTracking>(listCurriculumTracking);
            return dataSet;

        }
    }
}
