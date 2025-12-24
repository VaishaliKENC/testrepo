using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    public class MessageAdaptor
    {
        const string MESSAGE_NOT_FOUND = "Invalid Message Id";

        /// <summary>
        /// Get message as per Id in en-US
        /// </summary>
        /// <param name="pMessageId">Messsage Id</param>
        /// <returns>Message</returns>
        public static string GetMessage(string pMessageId)
        {
            return GetMessage(pMessageId, null);
        }

        /// <summary>
        /// Get message as per Id in specified language
        /// </summary>
        /// <param name="pMessageId">Messsage Id</param>
        /// <param name="pLanguageId">Language Id</param>
        /// <returns>Message</returns>
        public static string GetMessage(string pMessageId, string pLanguageId)
        {
            string strMessage = string.Empty;
            SystemMessageManager mgrMessage = new SystemMessageManager();
            SystemMessage entSysMessage = new SystemMessage();
            entSysMessage.ID = pMessageId;
            if (!String.IsNullOrEmpty(pLanguageId))
            {
                entSysMessage.LanguageId = pLanguageId;
            }
            try
            {

                //if (!String.IsNullOrEmpty(LMSSession.GetClientIdInSeesion()))
                //{
                //    entSysMessage.ClientId = LMSSession.GetClientIdInSeesion();
                //}
                //else
                {
                    entSysMessage.ClientId = Entity.Client.BASE_CLIENT_ID;
                }
                entSysMessage = mgrMessage.Execute(entSysMessage, SystemMessage.Method.Get);
                if (entSysMessage != null && !String.IsNullOrEmpty(entSysMessage.MessageText))
                    strMessage = entSysMessage.MessageText;
                else if (ValidationManager.ValidateString(pMessageId, ValidationManager.DataType.Alphanumeric))
                {
                    strMessage = pMessageId;
                }
                else
                {
                    strMessage = MESSAGE_NOT_FOUND;
                }
            }
            catch (Exception expCommon)
            {
                try
                {
                    ExceptionManager.WriteError(expCommon, CustomException.WhoCallsMe(), Convert.ToInt16(ExceptionSeverityLevel.Fatal), pMessageId,"");
                }
                catch
                {
                    if (ValidationManager.ValidateString(pMessageId, ValidationManager.DataType.Alphanumeric))
                    {
                        strMessage = pMessageId;
                    }
                    else
                    {
                        strMessage = MESSAGE_NOT_FOUND;
                    }
                }
            }
            return strMessage.Replace("'", "\"");
        }

        public static string GetMessage(string pMessageId, string pLanguageId, string ClientId)
        {
            string strMessage = string.Empty;
            SystemMessageManager mgrMessage = new SystemMessageManager();
            SystemMessage entSysMessage = new SystemMessage();
            entSysMessage.ID = pMessageId;
            if (!String.IsNullOrEmpty(pLanguageId))
            {
                entSysMessage.LanguageId = pLanguageId;
            }
            try
            {

                entSysMessage.ClientId = ClientId;
                entSysMessage = mgrMessage.Execute(entSysMessage, SystemMessage.Method.Get);

                if (entSysMessage != null && !String.IsNullOrEmpty(entSysMessage.MessageText))
                    strMessage = entSysMessage.MessageText;
                else if (ValidationManager.ValidateString(pMessageId, ValidationManager.DataType.Alphanumeric))
                {
                    strMessage = pMessageId;
                }
                else
                {
                    strMessage = MESSAGE_NOT_FOUND;
                }
            }
            catch (Exception expCommon)
            {
                try
                {
                    ExceptionManager.WriteError(expCommon, CustomException.WhoCallsMe(), Convert.ToInt16(ExceptionSeverityLevel.Fatal), pMessageId,"");
                }
                catch
                {
                    if (ValidationManager.ValidateString(pMessageId, ValidationManager.DataType.Alphanumeric))
                    {
                        strMessage = pMessageId;
                    }
                    else
                    {
                        strMessage = MESSAGE_NOT_FOUND;
                    }
                }
            }
            return strMessage.Replace("'", "\"");
        }

    }
}
