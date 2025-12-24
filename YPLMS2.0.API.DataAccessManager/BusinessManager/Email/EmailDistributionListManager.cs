using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public class EmailDistributionListManager : IManager<EmailDistributionList, EmailDistributionList.Method, EmailDistributionList.ListMethod>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EmailDistributionListManager()
        { }

        /// <summary>
        /// Execute List
        /// </summary>
        /// <param name="pEntEmailDistributionList"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<EmailDistributionList> Execute(EmailDistributionList pEntEmailDistributionList, EmailDistributionList.ListMethod pMethod)
        {
            List<EmailDistributionList> entListEmailDistributionList = null;
            EmailDistributionListAdaptor EmailDistributionListtAdaptor = new EmailDistributionListAdaptor();
            switch (pMethod)
            {
                case EmailDistributionList.ListMethod.GetAll:
                    entListEmailDistributionList = EmailDistributionListtAdaptor.GetEmailDistributionListLIST(pEntEmailDistributionList);
                    break;
                case EmailDistributionList.ListMethod.Search:
                    entListEmailDistributionList = EmailDistributionListtAdaptor.SearchEmailDistributionListLIST(pEntEmailDistributionList);
                    break;
                default:
                    break;
            }
            return entListEmailDistributionList;
        }

        /// <summary>
        /// Execute object
        /// </summary>
        /// <param name="pEntEmailDistributionList"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public EmailDistributionList Execute(EmailDistributionList pEntEmailDistributionList, EmailDistributionList.Method pMethod)
        {
            EmailDistributionList entEmailDistributionList = null;
            EmailDistributionListAdaptor adaptorEmailDistributionList = new EmailDistributionListAdaptor();

            switch (pMethod)
            {
                case EmailDistributionList.Method.GetRuleUsers:
                    entEmailDistributionList = adaptorEmailDistributionList.GetBusinessRuleUsersByDistributionListID(pEntEmailDistributionList);
                    break;
                case EmailDistributionList.Method.GetListUsers:
                    entEmailDistributionList = adaptorEmailDistributionList.GetUsersByDistributionListID(pEntEmailDistributionList);
                    break;
                case EmailDistributionList.Method.Get:
                    entEmailDistributionList = adaptorEmailDistributionList.GetEmailDistributionListByID(pEntEmailDistributionList);
                    break;
                case EmailDistributionList.Method.GetByName:
                    entEmailDistributionList = adaptorEmailDistributionList.GetEmailDistributionByName(pEntEmailDistributionList);
                    break;
                case EmailDistributionList.Method.Add:
                    entEmailDistributionList = adaptorEmailDistributionList.AddEmailDistributionList(pEntEmailDistributionList);
                    break;
                case EmailDistributionList.Method.Update:
                    entEmailDistributionList = adaptorEmailDistributionList.EditEmailDistributionList(pEntEmailDistributionList);
                    break;
                case EmailDistributionList.Method.Delete:
                    entEmailDistributionList = adaptorEmailDistributionList.DeleteEmailDistributionList(pEntEmailDistributionList);
                    break;
                default:
                    entEmailDistributionList = null;
                    break;
            }
            return entEmailDistributionList;
        }


        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="EmailDistributionList"></typeparam>
        /// <param name="pEntBase">EmailDistributionList object</param>
        /// <param name="pMethod">EmailDistributionList.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(EmailDistributionList pEntBase, EmailDistributionList.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<EmailDistributionList> listEmailDistributionList = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<EmailDistributionList>(listEmailDistributionList);
            return dataSet;

        }
    }
}
