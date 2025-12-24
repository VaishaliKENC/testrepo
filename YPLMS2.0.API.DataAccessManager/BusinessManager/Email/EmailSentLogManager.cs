using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public class EmailSentLogManager : IManager<EmailSentLog, EmailSentLog.Method, EmailSentLog.ListMethod>
    {
        /// <summary>
        /// Default Layout Constructor
        /// </summary>
        public EmailSentLogManager()
        {
        }

        /// <summary>
        /// Execute: GetAll
        /// </summary>
        /// <param name="pEntEmailSentLog"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<EmailSentLog> Execute(EmailSentLog pEntEmailSentLog, EmailSentLog.ListMethod pMethod)
        {
            return null;
        }

        /// <summary>
        /// Execute: Delete List
        /// </summary>
        /// <param name="pEntListEmailSentLogBase"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<EmailSentLog> Execute(List<EmailSentLog> pEntListEmailSentLogBase, EmailSentLog.ListMethod pMethod)
        {
            return null;
        }

        /// <summary>
        /// Execute: Get/Add
        /// </summary>
        /// <param name="pEntEmailSentLog"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public EmailSentLog Execute(EmailSentLog pEntEmailSentLog, EmailSentLog.Method pMethod)
        {
            EmailSentLog entEmailSentLog = null;
            EmailSentLogDAM EmailSentLogAdaptor = new EmailSentLogDAM();
            switch (pMethod)
            {
                case EmailSentLog.Method.Get:
                    entEmailSentLog = EmailSentLogAdaptor.GetEmailSentLogById(pEntEmailSentLog);
                    break;
                case EmailSentLog.Method.Add:
                    entEmailSentLog = EmailSentLogAdaptor.AddUpdateEmailSentLog(pEntEmailSentLog);
                    break;
                default:
                    entEmailSentLog = null;
                    break;
            }
            return entEmailSentLog;
        }


        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="EmailSentLog"></typeparam>
        /// <param name="pEntBase">EmailSentLog object</param>
        /// <param name="pMethod">EmailSentLog.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(EmailSentLog pEntBase, EmailSentLog.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<EmailSentLog> listEmailSentLog = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<EmailSentLog>(listEmailSentLog);
            return dataSet;

        }
    }
}
