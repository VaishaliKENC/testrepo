using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public class EmailDeliveryDashboardManager : IManager<EmailDeliveryDashboard, EmailDeliveryDashboard.Method, EmailDeliveryDashboard.ListMethod>
    {
        /// <summary>
        /// Default EmailDeliveryDashboardManager Const
        /// </summary>
        public EmailDeliveryDashboardManager()
        {
        }

        /// <summary>
        /// Execute List
        /// </summary>
        /// <param name="pEntEmailDeliveryDashboard"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<EmailDeliveryDashboard> Execute(EmailDeliveryDashboard pEntEmailDeliveryDashboard, EmailDeliveryDashboard.ListMethod pMethod)
        {
            return null;
        }

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="pEntEmailDeliveryDashboard"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public EmailDeliveryDashboard Execute(EmailDeliveryDashboard pEntEmailDeliveryDashboard, EmailDeliveryDashboard.Method pMethod)
        {
            EmailDeliveryDashboard entEmailDeliveryDashboard = null;
            EmailDeliveryDashboardDAM EmailDeliveryDashboardAdaptor = new EmailDeliveryDashboardDAM();
            switch (pMethod)
            {
                case EmailDeliveryDashboard.Method.Get:
                    entEmailDeliveryDashboard = EmailDeliveryDashboardAdaptor.GetEmailDeliveryDashboardById(pEntEmailDeliveryDashboard);
                    break;
                case EmailDeliveryDashboard.Method.CheckExistByName:
                    entEmailDeliveryDashboard = EmailDeliveryDashboardAdaptor.CheckExistByName(pEntEmailDeliveryDashboard);
                    break;
                case EmailDeliveryDashboard.Method.Add:
                    entEmailDeliveryDashboard = EmailDeliveryDashboardAdaptor.AddUpdateEmailDeliveryDashboard(pEntEmailDeliveryDashboard);
                    break;
                case EmailDeliveryDashboard.Method.Update:
                    entEmailDeliveryDashboard = EmailDeliveryDashboardAdaptor.AddUpdateEmailDeliveryDashboard(pEntEmailDeliveryDashboard);
                    break;
                case EmailDeliveryDashboard.Method.Delete:
                    entEmailDeliveryDashboard = EmailDeliveryDashboardAdaptor.DeleteEmailDeliveryDashboard(pEntEmailDeliveryDashboard);
                    break;
                case EmailDeliveryDashboard.Method.GetPendingApproval:
                    entEmailDeliveryDashboard = EmailDeliveryDashboardAdaptor.GetEmailDeliveryDashboardPendingApproval(pEntEmailDeliveryDashboard);
                    break;
                default:
                    entEmailDeliveryDashboard = null;
                    break;
            }
            return entEmailDeliveryDashboard;
        }


        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="EmailDeliveryDashboard"></typeparam>
        /// <param name="pEntBase">EmailDeliveryDashboard object</param>
        /// <param name="pMethod">EmailDeliveryDashboard.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(EmailDeliveryDashboard pEntBase, EmailDeliveryDashboard.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<EmailDeliveryDashboard> listEmailDeliveryDashboard = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<EmailDeliveryDashboard>(listEmailDeliveryDashboard);
            return dataSet;

        }


        public List<Learner> GetDynamicAssignmentUserList(EmailDeliveryDashboard pentEmailDashBoard, EmailDeliveryDashboard.ListMethod pMethod)
        {
            EmailDeliveryDashboardDAM entEmailDelDam = new EmailDeliveryDashboardDAM();
            List<Learner> lstLeareners = null;
            try
            {
                switch (pMethod)
                {
                    case EmailDeliveryDashboard.ListMethod.GetDynamicAssignmentUserList:
                        lstLeareners = entEmailDelDam.GetDynamicAssignmentUserList(pentEmailDashBoard);
                        break;
                }
            }
            catch
            {
            }
            return lstLeareners;
        }
    }
}
