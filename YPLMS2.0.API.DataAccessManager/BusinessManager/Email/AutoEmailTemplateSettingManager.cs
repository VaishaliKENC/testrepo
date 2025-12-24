using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public class AutoEmailTemplateSettingManager : IManager<AutoEmailTemplateSetting, AutoEmailTemplateSetting.Method, AutoEmailTemplateSetting.ListMethod>
    {
        /// <summary>
        /// To get single reocrd. 
        /// </summary>
        /// <param name="pEntEmailEvent"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>

        public AutoEmailTemplateSetting Execute(AutoEmailTemplateSetting pEntEmailEvent, AutoEmailTemplateSetting.Method pMethod)
        {
            AutoEmailTemplateSetting entEmailEvents = new AutoEmailTemplateSetting();
            AutoEmailTemplateSettingAdaptor emailEventsAdaptor = new AutoEmailTemplateSettingAdaptor();
            switch (pMethod)
            {
                case AutoEmailTemplateSetting.Method.Get:
                    entEmailEvents = emailEventsAdaptor.GetEmailEventById(pEntEmailEvent);
                    break;
                case AutoEmailTemplateSetting.Method.GetEmailTempId:
                    entEmailEvents = emailEventsAdaptor.GetEmailTempId(pEntEmailEvent);
                    break;
                default:
                    break;
            }
            return entEmailEvents;
        }
        /// <summary>
        /// Get all Email event list
        /// </summary>
        /// <param name="pEntEmailEvent"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<AutoEmailTemplateSetting> Execute(AutoEmailTemplateSetting pEntEmailEvent, AutoEmailTemplateSetting.ListMethod pMethod)
        {
            List<AutoEmailTemplateSetting> entListEmailEvents = null;
            AutoEmailTemplateSettingAdaptor emailEventsAdaptor = new AutoEmailTemplateSettingAdaptor();
            switch (pMethod)
            {
                case AutoEmailTemplateSetting.ListMethod.GetAll:
                    entListEmailEvents = emailEventsAdaptor.GetEmailTemplateSettingList(pEntEmailEvent);
                    break;
                default:
                    break;
            }
            return entListEmailEvents;
        }

        /// <summary>
        /// For Bulk Update
        /// </summary>
        /// <param name="pEntListEmailEvent"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<AutoEmailTemplateSetting> Execute(List<AutoEmailTemplateSetting> pEntListEmailEvent, AutoEmailTemplateSetting.ListMethod pMethod)
        {
            List<AutoEmailTemplateSetting> entListEmailEvent = new List<AutoEmailTemplateSetting>();
            AutoEmailTemplateSettingAdaptor adaptorEmailEvent = new AutoEmailTemplateSettingAdaptor();
            switch (pMethod)
            {
                case AutoEmailTemplateSetting.ListMethod.BulkUpdate:
                    entListEmailEvent = adaptorEmailEvent.Update(pEntListEmailEvent);
                    break;
                default:
                    entListEmailEvent = null;
                    break;
            }
            return entListEmailEvent;
        }


        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="AutoEmailTemplateSetting"></typeparam>
        /// <param name="pEntBase">AutoEmailTemplateSetting object</param>
        /// <param name="pMethod">AutoEmailTemplateSetting.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(AutoEmailTemplateSetting pEntBase, AutoEmailTemplateSetting.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<AutoEmailTemplateSetting> listAutoEmailTemplateSetting = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<AutoEmailTemplateSetting>(listAutoEmailTemplateSetting);
            return dataSet;

        }
    }
}
