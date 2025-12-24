using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public class EmailTemplateManager : IManager<EmailTemplate, EmailTemplate.Method, EmailTemplate.ListMethod>
    {
        /// <summary>
        /// Default Layout Constructor
        /// </summary>
        public EmailTemplateManager()
        {
        }

        /// <summary>
        /// Execute: GetAll
        /// </summary>
        /// <param name="pEntEmailTemplate"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<EmailTemplate> Execute(EmailTemplate pEntEmailTemplate, EmailTemplate.ListMethod pMethod)
        {
            List<EmailTemplate> entListEmailTemplateReturn = null;
            EmailTemplateDAM EmailTemplateAdaptor = new EmailTemplateDAM();
            switch (pMethod)
            {
                case YPLMS2._0.API.Entity.EmailTemplate.ListMethod.GetAll:
                    entListEmailTemplateReturn = EmailTemplateAdaptor.GetListAllEmailTemplate(pEntEmailTemplate);
                    break;
                default:
                    break;
            }
            return entListEmailTemplateReturn;
        }

        /// <summary>
        /// Execute for bulkUpdate
        /// </summary>
        /// <param name="pEntListRule"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<EmailTemplate> Execute(List<EmailTemplate> pEntListRule, EmailTemplate.ListMethod pMethod)
        {
            List<EmailTemplate> entListRule = null;
            EmailTemplateDAM adaptorRule = new EmailTemplateDAM();
            if (pEntListRule.Count > 0)
            {
                switch (pMethod)
                {
                    case EmailTemplate.ListMethod.DeleteEmailTemplate:
                        entListRule = adaptorRule.DeleteEmailTemplate(pEntListRule);
                        break;
                    default:
                        break;
                }
            }
            return entListRule;
        }

        /// <summary>
        /// Execute: Add/Get/SelectByName/GetEmailTemplateLanguage/DeleteEmailTemplateLanguage
        /// </summary>
        /// <param name="pEntEmailTemplate"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public EmailTemplate Execute(EmailTemplate pEntEmailTemplate, EmailTemplate.Method pMethod)
        {
            EmailTemplate entEmailTemplate = null;
            EmailTemplateDAM EmailTemplateAdaptor = new EmailTemplateDAM();

            switch (pMethod)
            {
                case EmailTemplate.Method.Get:
                    entEmailTemplate = EmailTemplateAdaptor.GetEmailTemplateById(pEntEmailTemplate);
                    break;
                case EmailTemplate.Method.GetTypeById:
                    entEmailTemplate = EmailTemplateAdaptor.GetEmailTypeById(pEntEmailTemplate);
                    break;
                case EmailTemplate.Method.SelectByName:
                    entEmailTemplate = EmailTemplateAdaptor.GetEmailTemplateByName(pEntEmailTemplate);
                    break;
                case EmailTemplate.Method.Add:
                    entEmailTemplate = EmailTemplateAdaptor.AddUpdateEmailTemplate(pEntEmailTemplate);
                    break;
                case EmailTemplate.Method.DeleteEmailTemplateLanguage:
                    entEmailTemplate = EmailTemplateAdaptor.DeleteEmailTemplateLanguage(pEntEmailTemplate);
                    break;
                default:
                    entEmailTemplate = null;
                    break;
            }
            return entEmailTemplate;
        }


        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="EmailTemplate"></typeparam>
        /// <param name="pEntBase">EmailTemplate object</param>
        /// <param name="pMethod">EmailTemplate.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(EmailTemplate pEntBase, EmailTemplate.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<EmailTemplate> listEmailTemplate = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<EmailTemplate>(listEmailTemplate);
            return dataSet;

        }
    }
}
