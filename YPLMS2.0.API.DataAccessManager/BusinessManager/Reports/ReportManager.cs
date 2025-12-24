using System.Data;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.DataAccessManager;
using System.Collections.Generic;
using YPLMS2._0.API.DataAccessManager.Reports;

namespace YPLMS2._0.API.DataAccessManager
{
    public class ReportManager : IManager<Report, Report.Method, Report.ListMethod>
    {
        DataSet dataReport = null;

        /// <summary>
        /// Default ReportManager Constructor
        /// </summary>        
        public ReportManager()
        {
        } 

        /// <summary>
        /// Get Report Data
        /// </summary>
        /// <param name="pEntReport"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(Report pEntReport, Report.ListMethod pMethod)
        {
            StandardReportDAM StandardReportAdaptor = new StandardReportDAM();
            dataReport = StandardReportAdaptor.GetStandardReports(pEntReport, pMethod);
            return dataReport;
        }
        /// <summary>
        /// Not implemented for ReportManager
        /// </summary>
        /// <param name="pEntBase">GroupReport</param>
        /// <param name="pMethod">GroupReport.Method</param>
        /// <returns>null</returns>
        public Report Execute(Report pEntBase, Report.Method pMethod)
        {
            return null;
        }
        /// <summary>
        /// Not implemented for ReportManager
        /// </summary>
        /// <typeparam name="Report"></typeparam>
        /// <param name="pEntBase">Report object</param>
        /// <param name="pMethod">Report.ListMethod</param>
        /// <returns>null</returns>
        public List<Report> Execute(Report pEntBase, Report.ListMethod pMethod)
        {
            return null;

        }
    }
}
