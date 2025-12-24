using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    public class SystemMessageManager : IManager<SystemMessage, SystemMessage.Method, SystemMessage.ListMethod>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SystemMessageManager()
        {

        }

        /// <summary>
        /// Bulk Add/Update
        /// </summary>
        /// <param name="pEntListMessage">Message List</param>
        /// <param name="pMethod">Add/Update</param>
        /// <returns></returns>
        public List<SystemMessage> Execute(List<SystemMessage> pEntListMessage, SystemMessage.ListMethod pMethod)
        {
            List<SystemMessage> entListMessage = null;
            SystemMessageAdaptor adaptorMessage = new SystemMessageAdaptor();
            string strClientKey;
            if (pEntListMessage.Count > 0)
            {
                strClientKey = pEntListMessage[0].ClientId;
                switch (pMethod)
                {
                    case SystemMessage.ListMethod.BulkUpdate:
                        entListMessage = adaptorMessage.BulkUpdate(pEntListMessage);
                        LMSCache.UpdateCacheKeyFile(strClientKey);
                        break;
                    default:
                        break;
                }
            }
            return entListMessage;
        }

        /// <summary>
        /// Get/Update List
        /// </summary>
        /// <param name="pEntMessage"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<SystemMessage> Execute(SystemMessage pEntMessage, SystemMessage.ListMethod pMethod)
        {
            List<SystemMessage> entListMessage = null;
            SystemMessageAdaptor adaptorMessage = new SystemMessageAdaptor();
            switch (pMethod)
            {
                case SystemMessage.ListMethod.GetAllMessages:
                    entListMessage = adaptorMessage.GetMessageList(pEntMessage);
                    break;
                default:
                    break;
            }
            return entListMessage;
        }

        /// <summary>
        /// Single Get/Add/Update/Delete
        /// </summary>
        /// <param name="pEntMessage"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public SystemMessage Execute(SystemMessage pEntMessage, SystemMessage.Method pMethod)
        {
            SystemMessage entMessage = null;
            SystemMessageAdaptor adaptorMessage = new SystemMessageAdaptor();
            string strClientKey = pEntMessage.ClientId;
            SystemMessageCache entCMessage = null;
            switch (pMethod)
            {
                case SystemMessage.Method.Get:
                    if (LMSCache.IsInCache(pEntMessage.ClientId + SystemMessage.CACHE_SUFFIX))
                    {
                        List<SystemMessageCache> listMessage = (List<SystemMessageCache>)LMSCache.GetValue(pEntMessage.ClientId + SystemMessage.CACHE_SUFFIX);
                        if (!String.IsNullOrEmpty(pEntMessage.LanguageId))
                        {
                            entCMessage = (SystemMessageCache)listMessage.Find(delegate (SystemMessageCache messageToFind)
                            { return (messageToFind.ID == pEntMessage.ID && messageToFind.LanguageId == pEntMessage.LanguageId); });
                        }
                        else
                        {
                            entCMessage = (SystemMessageCache)listMessage.Find(delegate (SystemMessageCache messageToFind)
                            { return (messageToFind.ID == pEntMessage.ID && messageToFind.LanguageId == Language.SYSTEM_DEFAULT_LANG_ID); });
                        }
                    }
                    // if not found in cache
                    if (entCMessage == null)
                    {
                        entMessage = adaptorMessage.GetMessageByID(pEntMessage);
                    }
                    else
                    {
                        entMessage = new SystemMessage();
                        entMessage.ID = entCMessage.ID;
                        entMessage.LanguageId = entCMessage.LanguageId;
                        entMessage.MessageText = entCMessage.MessageText;
                    }
                    break;
                default:
                    entMessage = null;
                    break;
            }
            return entMessage;
        }

        /// <summary>
        /// Add Messages to Cache
        /// </summary>
        /// <param name="ClientId"></param>
        public void AddMessagesToCache(string pClientId)
        {
            SystemMessage entMessage = new SystemMessage();
            SystemMessageAdaptor adaptorMessage = new SystemMessageAdaptor();
            SystemMessageCache entCMessage;
            List<SystemMessageCache> entListCMessage = new List<SystemMessageCache>();
            entMessage.ClientId = pClientId;
            try
            {
                List<SystemMessage> entListMessage = adaptorMessage.GetMessageList(entMessage);
                foreach (SystemMessage message in entListMessage)
                {
                    entCMessage = new SystemMessageCache();
                    entCMessage.ID = message.ID;
                    entCMessage.LanguageId = message.LanguageId;
                    entCMessage.MessageText = message.MessageText;
                    entListCMessage.Add(entCMessage);
                }
                if (LMSCache.IsInCache(pClientId + SystemMessage.CACHE_SUFFIX))
                {
                    LMSCache.RemoveCacheItems(pClientId);
                }
                LMSCache.AddCacheItem(pClientId + SystemMessage.CACHE_SUFFIX, entListCMessage, pClientId);
            }
            catch (Exception expCommon)
            {
                throw new CustomException(YPLMS.Services.Messages.Common.ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expCommon, true);
            }
        }


        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="SystemMessage"></typeparam>
        /// <param name="pEntBase">SystemMessage object</param>
        /// <param name="pMethod">SystemMessage.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(SystemMessage pEntBase, SystemMessage.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<SystemMessage> listSystemMessage = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<SystemMessage>(listSystemMessage);
            return dataSet;

        }
    }
}
