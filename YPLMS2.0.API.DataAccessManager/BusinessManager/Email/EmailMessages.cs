using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    public class EmailMessages
    {
        public CustomException _expCustom = null;
        public const string ADMIN_SITE_DEFAULT_LANGUAGE = "en-US";
        SmtpClient SMTPServer;
        MailMessage _mMailMessage;
        public string LogMessage = string.Empty;
        string _strClientLanguageId = string.Empty;
        string _strClientDateFormat = "{0:dd/MM/yyyy}";
        public static int MailServerToWaitAfterSend = 5000;
        string _strClientId = string.Empty;
        public static string AllowedDomains = string.Empty;

        private StringBuilder strLog;
        private StringBuilder strInvalidEmails;

        private StringBuilder strMailAddresses;

        private int emailsCount;

        private string _autoEmailEventID = string.Empty;
        public string AutoEmailEventID
        {
            get { return _autoEmailEventID; }
            set { if (this._autoEmailEventID != value) { _autoEmailEventID = value; } }
        }

        /// <summary>
        /// Constructor of class EmailMessages, Check for SMTP
        /// </summary>
        public EmailMessages(string pClientId)
        {
            Client entClient = new Client();
            strLog = new StringBuilder();
            strInvalidEmails = new StringBuilder();
            strMailAddresses = new StringBuilder();
            emailsCount = 0;
            ClientDAM mgrClient = new ClientDAM();
            entClient.ID = pClientId;
            entClient = mgrClient.GetClientByID(entClient); //Execute(entClient, Client.Method.Get); comment by vinod
            _strClientLanguageId = entClient.DefaultLanguageId;
            _strClientId = pClientId;
            SMTPServer = new SmtpClient();
            if (!string.IsNullOrEmpty(entClient.SMTPServerIP))
            {
                SMTPServer.Host = entClient.SMTPServerIP;

                //Add new code

                //if (!String.IsNullOrEmpty(Convert.ToString(entClient.SMTPPORT)))
                if (entClient.SMTPPORT != 0)
                    SMTPServer.Port = entClient.SMTPPORT;

                if (!string.IsNullOrEmpty(entClient.SMTPUserName) && !string.IsNullOrEmpty(entClient.SMTPPassword))
                {

                    SMTPServer.UseDefaultCredentials = false;

                    if (entClient.IsSecured)
                        SMTPServer.Credentials = new System.Net.NetworkCredential(entClient.SMTPUserName, ToSecureString(entClient.SMTPPassword));
                    else
                        SMTPServer.Credentials = new System.Net.NetworkCredential(entClient.SMTPUserName, entClient.SMTPPassword);
                }
                SMTPServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                SMTPServer.EnableSsl = entClient.SMTPEnableSSL;

                if (!String.IsNullOrEmpty(entClient.SecurityProtocol) && entClient.SMTPEnableSSL)
                {
                    switch (entClient.SecurityProtocol.ToLower())
                    {
                        case "ssl3":
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                            break;
                        case "tls":
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                            break;
                        case "tls1":
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
                            break;
                        case "tls2":
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                            break;
                    }
                }

                //End new code
            }
            else
            {
                throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
            }
        }
        /// <summary>
        /// Returns a Secure string from the source string
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public SecureString ToSecureString(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return null;
            else
            {
                SecureString result = new SecureString();
                foreach (char c in source.ToCharArray())
                    result.AppendChar(c);
                return result;
            }
        }
        /// <summary>
        /// Send mail to a Learner
        /// </summary>
        /// <param name="pLearner">Learner Object</param>
        /// <param name="pEmailTemplateId">Email Template ID</para>
        /// <param name="pCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param> 
        /// <param name="pBCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <param name="pTO">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <returns></returns>
        public bool SendPersonalizedMail(Learner pLearner, string pEmailTemplateId, string pstrAdditionalEmailBody,
            string pLanguageId, string pCC, string pBCC, string pTO, Boolean pbAddToDashboard, List<Attachment> pListAttachments)
        {
            EmailTemplate mailTemplate = new EmailTemplate();
            EmailTemplateManager mgrEmailTemplate = new EmailTemplateManager();
            mailTemplate.ID = pEmailTemplateId;
            mailTemplate.ClientId = _strClientId;
            mailTemplate = mgrEmailTemplate.Execute(mailTemplate, EmailTemplate.Method.Get);
            return SendPersonalizedMail(pLearner, mailTemplate, pstrAdditionalEmailBody, pLanguageId, pCC, pBCC, pTO, pbAddToDashboard, pListAttachments);
        }

        /// <summary>
        /// Send mail to a Learner
        /// </summary>
        /// <param name="pLearner">Learner Object</param>
        /// <param name="pMailTemplate">Email Template</para>
        ///  <param name="pstrAdditionalEmailBody">Additional Email Body</para>
        /// <param name="pCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param> 
        /// <param name="pBCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <param name="pTO">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <returns></returns>
        public bool SendPersonalizedMail(Learner pLearner, EmailTemplate pMailTemplate,
            string pstrAdditionalEmailBody, string pLanguageId, string pCC, string pBCC, string pTO, Boolean pbAddToDashboard, List<Attachment> pListAttachments)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            string strFromDispalyName = string.Empty;
            try
            {
                if (pbAddToDashboard)
                {
                    return AddEmailToDashBoard(pMailTemplate, pLanguageId, pCC, pBCC, pTO, true, null, pListAttachments, pLearner);
                }
                //Add To
                if (!ValidationManager.ValidateString(pLearner.EmailID, ValidationManager.DataType.EmailID))
                {
                    LogMessage += "Invalid Email: " + pLearner.EmailID + " ";
                    return false;
                }
                if (pMailTemplate != null)
                {
                    if (!string.IsNullOrEmpty(pLanguageId))
                    {
                        userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        { return entTempToFind.LanguageId == pLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == ADMIN_SITE_DEFAULT_LANGUAGE && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate != null)
                        {
                            if (string.IsNullOrEmpty(strFromDispalyName))
                            {
                                if (!string.IsNullOrEmpty(userMailTemplate.DisplayName))
                                {
                                    strFromDispalyName = userMailTemplate.DisplayName;
                                }
                            }
                            _mMailMessage.Subject = GetMailBodyFromTemplate(RemoveHTMLChars(userMailTemplate.EmailSubjectText), userMailTemplate.LanguageId, pLearner);
                            _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, pLearner);
                            if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                            {
                                _mMailMessage.Body += pstrAdditionalEmailBody;
                            }
                        }
                        else
                        {
                            LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                            return false;
                        }
                    }
                    else
                    {
                        foreach (EmailTemplateLanguage emailtemplatelang in pMailTemplate.EmailTemplateLanguage)
                        {
                            if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                            {
                                if (string.IsNullOrEmpty(strFromDispalyName))
                                {
                                    if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                    {
                                        strFromDispalyName = emailtemplatelang.DisplayName;
                                    }
                                }
                                _mMailMessage.Subject += GetMailBodyFromTemplate(RemoveHTMLChars(emailtemplatelang.EmailSubjectText), emailtemplatelang.LanguageId, pLearner) + " ";
                                _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, pLearner) + "<br/> ";
                                if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                {
                                    _mMailMessage.Body += pstrAdditionalEmailBody;
                                }
                            }
                        }
                    }
                }
                else
                {
                    LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                    return false;
                }
                //Add To
                if (string.IsNullOrEmpty(pTO))
                {
                    if (!string.IsNullOrEmpty(pLearner.FirstName) && !string.IsNullOrEmpty(pLearner.LastName))
                    {
                        _mMailMessage.To.Add(new MailAddress(pLearner.EmailID, pLearner.FirstName + " " + pLearner.LastName));
                    }
                    else
                    {
                        _mMailMessage.To.Add(new MailAddress(pLearner.EmailID));
                    }
                }
                else
                {
                    foreach (MailAddress address in GetAdresses(pTO))
                    {

                        if (!string.IsNullOrEmpty(address.DisplayName))
                        {
                            _mMailMessage.To.Add(new MailAddress(address.Address, address.DisplayName));
                        }
                        else
                        {
                            _mMailMessage.To.Add(new MailAddress(address.Address));
                        }
                    }
                }
                //Add Reply
                if (!string.IsNullOrEmpty(pMailTemplate.EmailReplyToId))
                {
                    _mMailMessage.ReplyTo = new MailAddress(pMailTemplate.EmailReplyToId);
                }
                //Add From
                if (!string.IsNullOrEmpty(pMailTemplate.EmailFromId))
                {
                    if (string.IsNullOrEmpty(strFromDispalyName))
                    {
                        _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId);
                    }
                    else
                    {
                        _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId, strFromDispalyName);
                    }
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pCC))
                {
                    foreach (MailAddress address in GetAdresses(pCC))
                    {
                        if (!string.IsNullOrEmpty(address.DisplayName))
                        {
                            _mMailMessage.CC.Add(new MailAddress(address.Address, address.DisplayName));
                        }
                        else
                        {
                            _mMailMessage.CC.Add(new MailAddress(address.Address));
                        }
                    }
                }
                //Add BCC
                if (!string.IsNullOrEmpty(pBCC))
                {
                    foreach (MailAddress address in GetAdresses(pBCC))
                    {
                        _mMailMessage.Bcc.Add(address);
                    }
                }
                if (pListAttachments != null)
                {
                    if (pListAttachments.Count > 0)
                    {
                        foreach (Attachment attachment in pListAttachments)
                        {
                            _mMailMessage.Attachments.Add(attachment);
                        }
                    }
                }
                try
                {
                    //SendEmail(_mMailMessage);
                    strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                    strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                    SaveEmailSentLog(pMailTemplate.ID, _mMailMessage);

                }
                catch (SmtpFailedRecipientsException ex)
                {
                    for (int i = 0; i < ex.InnerExceptions.Length; i++)
                    {
                        SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                        if (status == SmtpStatusCode.MailboxBusy ||
                            status == SmtpStatusCode.MailboxUnavailable)
                        {
                            //System.Threading.Thread.Sleep(5000);
                            //SendEmail(_mMailMessage);
                            strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                            strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                            SaveEmailSentLog(pMailTemplate.ID, _mMailMessage);
                        }
                    }
                }
            }
            catch (Exception expCommon)
            {
                throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
            }
            return true;
        }

        /// <summary>
        /// Send mail to a Learner
        /// </summary>
        /// <param name="pListLearner">Learner Object</param>
        /// <param name="pMailTemplate">Email Template</para>
        ///  <param name="pstrAdditionalEmailBody">Additional Email Body</para>
        /// <param name="pCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param> 
        /// <param name="pBCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <param name="pTO">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <param name="pstrDeliveryTitle">Delivery Title</param>
        /// <returns></returns>
        public bool SendPersonalizedMail(List<Learner> pListLearner, EmailTemplate pMailTemplate,
            string pstrAdditionalEmailBody, string pLanguageId, string pCC, string pBCC, string pTO,
            Boolean pbAddToDashboard, string pstrDeliveryTitle, List<Attachment> pListAttachments)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            StringBuilder strEmailAddress = new StringBuilder("");
            string strLearnerId = string.Empty;
            string strFromDispalyName = string.Empty;
            try
            {
                if (pbAddToDashboard)
                {
                    if (pListLearner != null && pListLearner.Count > 0)
                    {
                        foreach (Learner learner in pListLearner)
                        {
                            strEmailAddress.Append(learner.EmailID + ",");
                            strLearnerId = strLearnerId + learner.ID + ",";
                        }
                        if (!string.IsNullOrEmpty(strLearnerId.ToString()))
                        {
                            strLearnerId = strLearnerId.ToString().Substring(0, strLearnerId.ToString().LastIndexOf(","));
                        }
                        if (!string.IsNullOrEmpty(pTO))
                        {
                            pTO = pTO.Replace(";", ",");
                            pTO = pTO + "," + strEmailAddress.ToString();
                        }
                        else
                        {
                            pTO = strEmailAddress.ToString();
                        }
                    }
                    return AddEmailToDashBoard(strLearnerId.ToString(), pMailTemplate, pLanguageId, pCC, pBCC, pTO, true, pstrDeliveryTitle, pListAttachments);
                }
                foreach (Learner learner in pListLearner)
                {
                    #region Send Mail
                    //Add To
                    if (!ValidationManager.ValidateString(learner.EmailID, ValidationManager.DataType.EmailID))
                    {
                        LogMessage += "Invalid Email: " + learner.EmailID + " ";
                        return false;
                    }
                    if (pMailTemplate != null)
                    {
                        if (!string.IsNullOrEmpty(pLanguageId))
                        {
                            userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == pLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                            if (userMailTemplate == null)
                            {
                                userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                            }
                            if (userMailTemplate == null)
                            {
                                userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                { return entTempToFind.LanguageId == ADMIN_SITE_DEFAULT_LANGUAGE && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                            }
                            if (userMailTemplate != null)
                            {
                                if (string.IsNullOrEmpty(strFromDispalyName))
                                {
                                    if (!string.IsNullOrEmpty(userMailTemplate.DisplayName))
                                    {
                                        strFromDispalyName = userMailTemplate.DisplayName;
                                    }
                                }
                                _mMailMessage.Subject = GetMailBodyFromTemplate(RemoveHTMLChars(userMailTemplate.EmailSubjectText), userMailTemplate.LanguageId, learner);
                                _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, learner);
                                if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                {
                                    _mMailMessage.Body += pstrAdditionalEmailBody;
                                }
                            }
                            else
                            {
                                LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                                return false;
                            }
                        }
                        else
                        {
                            foreach (EmailTemplateLanguage emailtemplatelang in pMailTemplate.EmailTemplateLanguage)
                            {
                                if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                                {
                                    if (string.IsNullOrEmpty(strFromDispalyName))
                                    {
                                        if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                        {
                                            strFromDispalyName = emailtemplatelang.DisplayName;
                                        }
                                    }
                                    _mMailMessage.Subject += GetMailBodyFromTemplate(RemoveHTMLChars(emailtemplatelang.EmailSubjectText), emailtemplatelang.LanguageId, learner) + " ";
                                    _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, learner) + "<br/> ";
                                    if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                    {
                                        _mMailMessage.Body += pstrAdditionalEmailBody;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                        return false;
                    }
                    //Add To
                    if (string.IsNullOrEmpty(pTO))
                    {
                        if (!string.IsNullOrEmpty(learner.FirstName) && !string.IsNullOrEmpty(learner.LastName))
                        {
                            _mMailMessage.To.Add(new MailAddress(learner.EmailID, learner.FirstName + " " + learner.LastName));
                        }
                        else
                        {
                            _mMailMessage.To.Add(new MailAddress(learner.EmailID));
                        }
                    }
                    else
                    {
                        foreach (MailAddress address in GetAdresses(pTO))
                        {
                            if (!string.IsNullOrEmpty(address.DisplayName))
                            {
                                _mMailMessage.To.Add(new MailAddress(address.Address, address.DisplayName));
                            }
                            else
                            {
                                _mMailMessage.To.Add(new MailAddress(address.Address));
                            }
                        }
                    }
                    //Add Reply
                    if (!string.IsNullOrEmpty(pMailTemplate.EmailReplyToId))
                    {
                        _mMailMessage.ReplyTo = new MailAddress(pMailTemplate.EmailReplyToId);
                    }
                    //Add From
                    if (!string.IsNullOrEmpty(pMailTemplate.EmailFromId))
                    {
                        if (string.IsNullOrEmpty(strFromDispalyName))
                        {
                            _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId);
                        }
                        else
                        {
                            _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId, strFromDispalyName);
                        }
                    }
                    else
                    {
                        throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                    }
                    //Add CC
                    if (!string.IsNullOrEmpty(pCC))
                    {
                        foreach (MailAddress address in GetAdresses(pCC))
                        {
                            if (!string.IsNullOrEmpty(address.DisplayName))
                            {
                                _mMailMessage.CC.Add(new MailAddress(address.Address, address.DisplayName));
                            }
                            else
                            {
                                _mMailMessage.CC.Add(new MailAddress(address.Address));
                            }
                        }
                    }
                    //Add BCC
                    if (!string.IsNullOrEmpty(pBCC))
                    {
                        foreach (MailAddress address in GetAdresses(pBCC))
                        {
                            _mMailMessage.Bcc.Add(address);
                        }
                    }
                    if (pListAttachments != null)
                    {
                        if (pListAttachments.Count > 0)
                        {
                            foreach (Attachment attachment in pListAttachments)
                            {
                                _mMailMessage.Attachments.Add(attachment);
                            }
                        }
                    }
                    try
                    {
                        strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                        strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                        SaveEmailSentLog(pMailTemplate.ID, _mMailMessage);
                    }
                    catch (SmtpFailedRecipientsException ex)
                    {
                        for (int i = 0; i < ex.InnerExceptions.Length; i++)
                        {
                            SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                            if (status == SmtpStatusCode.MailboxBusy ||
                                status == SmtpStatusCode.MailboxUnavailable)
                            {
                                Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                                strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                                strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                                SaveEmailSentLog(pMailTemplate.ID, _mMailMessage);
                            }
                            else
                            {
                                Console.WriteLine("Failed to deliver message to {0}",
                                    ex.InnerExceptions[i].FailedRecipient);
                            }
                        }
                    }

                    #endregion
                }
            }
            catch (Exception expCommon)
            {
                CustomException expCustom = new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                return false;
            }
            return true;
        }


        /// <summary>
        /// Send mail to a Learner
        /// </summary>
        /// <param name="pListLearner">Learner Object</param>
        /// <param name="pMailTemplate">Email Template</para>
        ///  <param name="pstrAdditionalEmailBody">Additional Email Body</para>
        /// <param name="pLanguageId"> Language ID </param>
        /// <param name="pCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param> 
        /// <param name="pBCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <param name="pTO">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <param name="pListAttachments"> Attachment List</param>
        /// <param name="strDictionaryDynamicValues">String Dictionary to resolve dynamic values</param>
        /// <returns></returns>
        public bool SendPersonalizedMail(List<Learner> pListLearner, EmailTemplate pMailTemplate,
            string pstrAdditionalEmailBody, string pLanguageId, string pCC, string pBCC, string pTO,
            List<Attachment> pListAttachments, Dictionary<string, string> strDictionaryDynamicValues)
        {
            EmailTemplateLanguage userMailTemplate = null;
            string strFromDispalyName = string.Empty;
            try
            {
                #region Send Email If Learner list
                try
                {
                    if (string.IsNullOrEmpty(pTO))    // If added by sarita to avoid sending email to times
                    {
                        if (pListLearner != null && pListLearner.Count > 0)
                        {
                            foreach (Learner learner in pListLearner)
                            {
                                _mMailMessage = new MailMessage();
                                _mMailMessage.IsBodyHtml = true;

                                #region Send Mail
                                //Add To
                                if (!ValidationManager.ValidateString(learner.EmailID, ValidationManager.DataType.EmailID))
                                {
                                    LogMessage += "Invalid Email: " + learner.EmailID + " ";
                                    return false;
                                }
                                if (pMailTemplate != null)
                                {
                                    if (!string.IsNullOrEmpty(pLanguageId))
                                    {
                                        userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                        { return entTempToFind.LanguageId == pLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                        if (userMailTemplate == null)
                                        {
                                            userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                        }
                                        if (userMailTemplate == null)
                                        {
                                            userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                            { return entTempToFind.LanguageId == ADMIN_SITE_DEFAULT_LANGUAGE && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                        }
                                        if (userMailTemplate != null)
                                        {
                                            if (string.IsNullOrEmpty(strFromDispalyName))
                                            {
                                                if (!string.IsNullOrEmpty(userMailTemplate.DisplayName))
                                                {
                                                    strFromDispalyName = userMailTemplate.DisplayName;
                                                }
                                            }
                                            _mMailMessage.Subject = GetMailBodyFromTemplate(RemoveHTMLChars(userMailTemplate.EmailSubjectText), strDictionaryDynamicValues);
                                            _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, strDictionaryDynamicValues);
                                            if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                            {
                                                _mMailMessage.Body += pstrAdditionalEmailBody;
                                            }
                                        }
                                        else
                                        {
                                            LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        foreach (EmailTemplateLanguage emailtemplatelang in pMailTemplate.EmailTemplateLanguage)
                                        {
                                            if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                                            {
                                                if (string.IsNullOrEmpty(strFromDispalyName))
                                                {
                                                    if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                                    {
                                                        strFromDispalyName = emailtemplatelang.DisplayName;
                                                    }
                                                }
                                                _mMailMessage.Subject += GetMailBodyFromTemplate(RemoveHTMLChars(emailtemplatelang.EmailSubjectText), strDictionaryDynamicValues) + " ";
                                                _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, strDictionaryDynamicValues) + "<br/> ";
                                                if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                                {
                                                    _mMailMessage.Body += pstrAdditionalEmailBody;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                                    return false;
                                }
                                //Add To
                                if (string.IsNullOrEmpty(pTO))
                                {
                                    if (!string.IsNullOrEmpty(learner.FirstName) && !string.IsNullOrEmpty(learner.LastName))
                                    {
                                        _mMailMessage.To.Add(new MailAddress(learner.EmailID, learner.FirstName + " " + learner.LastName));
                                    }
                                    else
                                    {
                                        _mMailMessage.To.Add(new MailAddress(learner.EmailID));
                                    }
                                }
                                else
                                {
                                    foreach (MailAddress address in GetAdresses(pTO))
                                    {
                                        if (!string.IsNullOrEmpty(address.DisplayName))
                                        {
                                            _mMailMessage.To.Add(new MailAddress(address.Address, address.DisplayName));
                                        }
                                        else
                                        {
                                            _mMailMessage.To.Add(new MailAddress(address.Address));
                                        }
                                    }
                                }
                                //Add Reply
                                if (!string.IsNullOrEmpty(pMailTemplate.EmailReplyToId))
                                {
                                    _mMailMessage.ReplyTo = new MailAddress(pMailTemplate.EmailReplyToId);
                                }
                                //Add From
                                if (!string.IsNullOrEmpty(pMailTemplate.EmailFromId))
                                {
                                    if (string.IsNullOrEmpty(strFromDispalyName))
                                    {
                                        _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId);
                                    }
                                    else
                                    {
                                        _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId, strFromDispalyName);
                                    }
                                }
                                else
                                {
                                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                                }
                                //Add CC
                                if (!string.IsNullOrEmpty(pCC))
                                {
                                    foreach (MailAddress address in GetAdresses(pCC))
                                    {
                                        if (!string.IsNullOrEmpty(address.DisplayName))
                                        {
                                            _mMailMessage.CC.Add(new MailAddress(address.Address, address.DisplayName));
                                        }
                                        else
                                        {
                                            _mMailMessage.CC.Add(new MailAddress(address.Address));
                                        }
                                    }
                                }
                                //Add BCC
                                if (!string.IsNullOrEmpty(pBCC))
                                {
                                    foreach (MailAddress address in GetAdresses(pBCC))
                                    {
                                        _mMailMessage.Bcc.Add(address);
                                    }
                                }
                                if (pListAttachments != null)
                                {
                                    if (pListAttachments.Count > 0)
                                    {
                                        foreach (Attachment attachment in pListAttachments)
                                        {
                                            _mMailMessage.Attachments.Add(attachment);
                                        }
                                    }
                                }
                                try
                                {

                                    //SendEmail(_mMailMessage);

                                    strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                                    strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                                    SaveEmailSentLog(pMailTemplate.ID, _mMailMessage);

                                }
                                catch (SmtpFailedRecipientsException ex)
                                {
                                    for (int i = 0; i < ex.InnerExceptions.Length; i++)
                                    {
                                        SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                                        if (status == SmtpStatusCode.MailboxBusy ||
                                            status == SmtpStatusCode.MailboxUnavailable)
                                        {
                                            Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                                            //System.Threading.Thread.Sleep(5000);
                                            //SendEmail(_mMailMessage);
                                            strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                                            strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                                            SaveEmailSentLog(pMailTemplate.ID, _mMailMessage);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Failed to deliver message to {0}",
                                                ex.InnerExceptions[i].FailedRecipient);
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }
                catch (Exception expCommon)
                {
                    CustomException expCustom = new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                }
                #endregion

                #region Send Email To List
                try
                {
                    _mMailMessage = new MailMessage();
                    _mMailMessage.IsBodyHtml = true;

                    if (!string.IsNullOrEmpty(pTO))
                    {
                        #region Send Mail

                        if (pMailTemplate != null)
                        {
                            if (!string.IsNullOrEmpty(pLanguageId))
                            {
                                userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                { return entTempToFind.LanguageId == pLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                if (userMailTemplate == null)
                                {
                                    userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                    { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                }
                                if (userMailTemplate == null)
                                {
                                    userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                    { return entTempToFind.LanguageId == ADMIN_SITE_DEFAULT_LANGUAGE && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                }
                                if (userMailTemplate != null)
                                {
                                    if (string.IsNullOrEmpty(strFromDispalyName))
                                    {
                                        if (!string.IsNullOrEmpty(userMailTemplate.DisplayName))
                                        {
                                            strFromDispalyName = userMailTemplate.DisplayName;
                                        }
                                    }
                                    _mMailMessage.Subject = GetMailBodyFromTemplate(RemoveHTMLChars(userMailTemplate.EmailSubjectText), strDictionaryDynamicValues);
                                    _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, strDictionaryDynamicValues);
                                    if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                    {
                                        _mMailMessage.Body += pstrAdditionalEmailBody;
                                    }
                                }
                                else
                                {
                                    LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                                    return false;
                                }
                            }
                            else
                            {
                                foreach (EmailTemplateLanguage emailtemplatelang in pMailTemplate.EmailTemplateLanguage)
                                {
                                    if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                                    {
                                        if (string.IsNullOrEmpty(strFromDispalyName))
                                        {
                                            if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                            {
                                                strFromDispalyName = emailtemplatelang.DisplayName;
                                            }
                                        }
                                        _mMailMessage.Subject += GetMailBodyFromTemplate(RemoveHTMLChars(emailtemplatelang.EmailSubjectText), strDictionaryDynamicValues) + " ";
                                        _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, strDictionaryDynamicValues) + "<br/> ";
                                        if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                        {
                                            _mMailMessage.Body += pstrAdditionalEmailBody;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                            return false;
                        }
                        //Add To                   
                        foreach (MailAddress address in GetAdresses(pTO))
                        {
                            if (!string.IsNullOrEmpty(address.DisplayName))
                            {
                                _mMailMessage.To.Add(new MailAddress(address.Address, address.DisplayName));
                            }
                            else
                            {
                                _mMailMessage.To.Add(new MailAddress(address.Address));
                            }
                        }

                        //Add Reply
                        if (!string.IsNullOrEmpty(pMailTemplate.EmailReplyToId))
                        {
                            _mMailMessage.ReplyTo = new MailAddress(pMailTemplate.EmailReplyToId);
                        }
                        //Add From
                        if (!string.IsNullOrEmpty(pMailTemplate.EmailFromId))
                        {
                            if (string.IsNullOrEmpty(strFromDispalyName))
                            {
                                _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId);
                            }
                            else
                            {
                                _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId, strFromDispalyName);
                            }
                        }
                        else
                        {
                            throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                        }
                        //Add CC
                        if (!string.IsNullOrEmpty(pCC))
                        {
                            foreach (MailAddress address in GetAdresses(pCC))
                            {
                                if (!string.IsNullOrEmpty(address.DisplayName))
                                {
                                    _mMailMessage.CC.Add(new MailAddress(address.Address, address.DisplayName));
                                }
                                else
                                {
                                    _mMailMessage.CC.Add(new MailAddress(address.Address));
                                }
                            }
                        }
                        //Add BCC
                        if (!string.IsNullOrEmpty(pBCC))
                        {
                            foreach (MailAddress address in GetAdresses(pBCC))
                            {
                                _mMailMessage.Bcc.Add(address);
                            }
                        }
                        if (pListAttachments != null)
                        {
                            if (pListAttachments.Count > 0)
                            {
                                foreach (Attachment attachment in pListAttachments)
                                {
                                    _mMailMessage.Attachments.Add(attachment);
                                }
                            }
                        }
                        try
                        {

                            //SendEmail(_mMailMessage);
                            strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                            strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                            SaveEmailSentLog(pMailTemplate.ID, _mMailMessage);
                        }
                        catch (SmtpFailedRecipientsException ex)
                        {
                            for (int i = 0; i < ex.InnerExceptions.Length; i++)
                            {
                                SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                                if (status == SmtpStatusCode.MailboxBusy ||
                                    status == SmtpStatusCode.MailboxUnavailable)
                                {
                                    Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                                    //System.Threading.Thread.Sleep(5000);
                                    //SendEmail(_mMailMessage);
                                    strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                                    strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                                    SaveEmailSentLog(pMailTemplate.ID, _mMailMessage);
                                }
                                else
                                {
                                    Console.WriteLine("Failed to deliver message to {0}",
                                        ex.InnerExceptions[i].FailedRecipient);
                                }
                            }
                        }
                        #endregion
                    }
                }
                catch (Exception expCommon)
                {
                    CustomException expCustom = new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                }
                #endregion
            }
            catch (Exception expCommon)
            {
                CustomException expCustom = new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Send mail to a Learner
        /// </summary>
        /// <param name="pListLearner">Learner Object</param>
        /// <param name="pMailTemplate">Email Template</para>
        ///  <param name="pstrAdditionalEmailBody">Additional Email Body</para>
        /// <param name="pLanguageId"> Language ID </param>
        /// <param name="pCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param> 
        /// <param name="pBCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <param name="pTO">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <param name="pListAttachments"> Attachment List</param>
        /// <param name="strDictionaryDynamicValues">String Dictionary to resolve dynamic values</param>
        /// <returns></returns>
        public bool SendPersonalizedAssignmentMail(List<Learner> pListLearner, EmailTemplate pMailTemplate,
            string pstrAdditionalEmailBody, string pLanguageId, string pCC, string pBCC, string pTO,
            List<Attachment> pListAttachments, Dictionary<string, string> strDictionaryDynamicValues, DataTable dtable)
        {
            EmailTemplateLanguage userMailTemplate = null;
            string strFromDispalyName = string.Empty;
            try
            {
                #region Send Email If Learner list
                try
                {
                    if (string.IsNullOrEmpty(pTO))    // If added by sarita to avoid sending email to times
                    {
                        if (pListLearner != null && pListLearner.Count > 0)
                        {
                            foreach (Learner learner in pListLearner)
                            {
                                _mMailMessage = new MailMessage();
                                _mMailMessage.IsBodyHtml = true;

                                #region Send Mail
                                //Add To
                                if (!ValidationManager.ValidateString(learner.EmailID, ValidationManager.DataType.EmailID))
                                {
                                    LogMessage += "Invalid Email: " + learner.EmailID + " ";
                                    return false;
                                }
                                if (pMailTemplate != null)
                                {
                                    if (!string.IsNullOrEmpty(pLanguageId))
                                    {
                                        userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                        { return entTempToFind.LanguageId == pLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                        if (userMailTemplate == null)
                                        {
                                            userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                        }
                                        if (userMailTemplate == null)
                                        {
                                            userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                            { return entTempToFind.LanguageId == ADMIN_SITE_DEFAULT_LANGUAGE && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                        }
                                        if (userMailTemplate != null)
                                        {
                                            if (string.IsNullOrEmpty(strFromDispalyName))
                                            {
                                                if (!string.IsNullOrEmpty(userMailTemplate.DisplayName))
                                                {
                                                    strFromDispalyName = userMailTemplate.DisplayName;
                                                }
                                            }
                                            _mMailMessage.Subject = GetMailBodyFromTemplate(RemoveHTMLChars(userMailTemplate.EmailSubjectText), strDictionaryDynamicValues);
                                            _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, strDictionaryDynamicValues);
                                            if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                            {
                                                _mMailMessage.Body += pstrAdditionalEmailBody;
                                            }
                                        }
                                        else
                                        {
                                            LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        foreach (EmailTemplateLanguage emailtemplatelang in pMailTemplate.EmailTemplateLanguage)
                                        {
                                            if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                                            {
                                                if (string.IsNullOrEmpty(strFromDispalyName))
                                                {
                                                    if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                                    {
                                                        strFromDispalyName = emailtemplatelang.DisplayName;
                                                    }
                                                }
                                                _mMailMessage.Subject += GetMailBodyFromTemplate(RemoveHTMLChars(emailtemplatelang.EmailSubjectText), strDictionaryDynamicValues) + " ";
                                                _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, strDictionaryDynamicValues) + "<br/> ";
                                                if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                                {
                                                    _mMailMessage.Body += pstrAdditionalEmailBody;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                                    return false;
                                }
                                //Add To
                                if (string.IsNullOrEmpty(pTO))
                                {
                                    if (!string.IsNullOrEmpty(learner.FirstName) && !string.IsNullOrEmpty(learner.LastName))
                                    {
                                        _mMailMessage.To.Add(new MailAddress(learner.EmailID, learner.FirstName + " " + learner.LastName));
                                    }
                                    else
                                    {
                                        _mMailMessage.To.Add(new MailAddress(learner.EmailID));
                                    }
                                }
                                else
                                {
                                    foreach (MailAddress address in GetAdresses(pTO))
                                    {
                                        if (!string.IsNullOrEmpty(address.DisplayName))
                                        {
                                            _mMailMessage.To.Add(new MailAddress(address.Address, address.DisplayName));
                                        }
                                        else
                                        {
                                            _mMailMessage.To.Add(new MailAddress(address.Address));
                                        }
                                    }
                                }
                                //Add Reply
                                if (!string.IsNullOrEmpty(pMailTemplate.EmailReplyToId))
                                {
                                    _mMailMessage.ReplyTo = new MailAddress(pMailTemplate.EmailReplyToId);
                                }
                                //Add From
                                if (!string.IsNullOrEmpty(pMailTemplate.EmailFromId))
                                {
                                    if (string.IsNullOrEmpty(strFromDispalyName))
                                    {
                                        _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId);
                                    }
                                    else
                                    {
                                        _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId, strFromDispalyName);
                                    }
                                }
                                else
                                {
                                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                                }
                                //Add CC
                                if (!string.IsNullOrEmpty(pCC))
                                {
                                    foreach (MailAddress address in GetAdresses(pCC))
                                    {
                                        if (!string.IsNullOrEmpty(address.DisplayName))
                                        {
                                            _mMailMessage.CC.Add(new MailAddress(address.Address, address.DisplayName));
                                        }
                                        else
                                        {
                                            _mMailMessage.CC.Add(new MailAddress(address.Address));
                                        }
                                    }
                                }
                                //Add BCC
                                if (!string.IsNullOrEmpty(pBCC))
                                {
                                    foreach (MailAddress address in GetAdresses(pBCC))
                                    {
                                        _mMailMessage.Bcc.Add(address);
                                    }
                                }
                                if (pListAttachments != null)
                                {
                                    if (pListAttachments.Count > 0)
                                    {
                                        foreach (Attachment attachment in pListAttachments)
                                        {
                                            _mMailMessage.Attachments.Add(attachment);
                                        }
                                    }
                                }
                                try
                                {
                                    //SendEmail(_mMailMessage);                                   

                                    foreach (DataRow dataRow in dtable.Rows)
                                    {
                                        string attachmentSessionFromFilename = string.Empty;
                                        string attachmentSessionToFilename = string.Empty;
                                        DateTime strSessionFormDateTime = Convert.ToDateTime(Convert.ToString(Convert.ToDateTime(dataRow["SessionFromDate"]).ToShortDateString()) + " " + Convert.ToString(dataRow["SessionFromTime"])); // Convert.ToDateTime(dataRow["SessionFromDate"]+ " " + dataRow["SessionFromTime"]);
                                        attachmentSessionFromFilename = FileHandler.GetOutlookClassRoomFromDatefile(strSessionFormDateTime, pListLearner[0].ClientId, Convert.ToString(dataRow["SessionName"]), Convert.ToString(dataRow["SessionLocationName"]));
                                        Thread.Sleep(5000);
                                        DateTime strSessionToDateTime = Convert.ToDateTime(Convert.ToString(Convert.ToDateTime(dataRow["SessionToDate"]).ToShortDateString()) + " " + Convert.ToString(dataRow["SessionToTime"]));// Convert.ToDateTime(dataRow["SessionToDate"] + " " + dataRow["SessionFromTime"]);
                                        attachmentSessionToFilename = FileHandler.GetOutlookClassRoomToDatefile(strSessionToDateTime, pListLearner[0].ClientId, Convert.ToString(dataRow["SessionName"]), Convert.ToString(dataRow["SessionLocationName"]));
                                        Thread.Sleep(5000);
                                        if (!string.IsNullOrEmpty(attachmentSessionFromFilename))
                                        {
                                            Attachment attachment = new Attachment(attachmentSessionFromFilename, MediaTypeNames.Application.Octet);
                                            ContentDisposition disposition = attachment.ContentDisposition;
                                            disposition.CreationDate = File.GetCreationTime(attachmentSessionFromFilename);
                                            disposition.ModificationDate = File.GetLastWriteTime(attachmentSessionFromFilename);
                                            disposition.ReadDate = File.GetLastAccessTime(attachmentSessionFromFilename);
                                            disposition.FileName = Path.GetFileName(attachmentSessionFromFilename);
                                            disposition.Size = new FileInfo(attachmentSessionFromFilename).Length;
                                            disposition.DispositionType = DispositionTypeNames.Attachment;
                                            _mMailMessage.Attachments.Add(attachment);
                                        }
                                        Thread.Sleep(5000);
                                        if (!string.IsNullOrEmpty(attachmentSessionToFilename))
                                        {

                                            Attachment attachment = new Attachment(attachmentSessionToFilename, MediaTypeNames.Application.Octet);
                                            ContentDisposition disposition = attachment.ContentDisposition;
                                            disposition.CreationDate = File.GetCreationTime(attachmentSessionToFilename);
                                            disposition.ModificationDate = File.GetLastWriteTime(attachmentSessionToFilename);
                                            disposition.ReadDate = File.GetLastAccessTime(attachmentSessionToFilename);
                                            disposition.FileName = Path.GetFileName(attachmentSessionToFilename);
                                            disposition.Size = new FileInfo(attachmentSessionToFilename).Length;
                                            disposition.DispositionType = DispositionTypeNames.Attachment;
                                            _mMailMessage.Attachments.Add(attachment);
                                        }
                                    }

                                    strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                                    strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                                    SaveEmailSentLog(pMailTemplate.ID, _mMailMessage);

                                }
                                catch (SmtpFailedRecipientsException ex)
                                {
                                    for (int i = 0; i < ex.InnerExceptions.Length; i++)
                                    {
                                        SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                                        if (status == SmtpStatusCode.MailboxBusy ||
                                            status == SmtpStatusCode.MailboxUnavailable)
                                        {
                                            Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                                            //System.Threading.Thread.Sleep(5000);
                                            //SendEmail(_mMailMessage);
                                            strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                                            strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                                            SaveEmailSentLog(pMailTemplate.ID, _mMailMessage);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Failed to deliver message to {0}",
                                                ex.InnerExceptions[i].FailedRecipient);
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }
                catch (Exception expCommon)
                {
                    CustomException expCustom = new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                }
                #endregion

                #region Send Email To List
                try
                {
                    _mMailMessage = new MailMessage();
                    _mMailMessage.IsBodyHtml = true;

                    if (!string.IsNullOrEmpty(pTO))
                    {
                        #region Send Mail

                        if (pMailTemplate != null)
                        {
                            if (!string.IsNullOrEmpty(pLanguageId))
                            {
                                userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                { return entTempToFind.LanguageId == pLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                if (userMailTemplate == null)
                                {
                                    userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                    { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                }
                                if (userMailTemplate == null)
                                {
                                    userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                    { return entTempToFind.LanguageId == ADMIN_SITE_DEFAULT_LANGUAGE && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                }
                                if (userMailTemplate != null)
                                {
                                    if (string.IsNullOrEmpty(strFromDispalyName))
                                    {
                                        if (!string.IsNullOrEmpty(userMailTemplate.DisplayName))
                                        {
                                            strFromDispalyName = userMailTemplate.DisplayName;
                                        }
                                    }
                                    _mMailMessage.Subject = GetMailBodyFromTemplate(RemoveHTMLChars(userMailTemplate.EmailSubjectText), strDictionaryDynamicValues);
                                    _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, strDictionaryDynamicValues);
                                    if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                    {
                                        _mMailMessage.Body += pstrAdditionalEmailBody;
                                    }
                                }
                                else
                                {
                                    LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                                    return false;
                                }
                            }
                            else
                            {
                                foreach (EmailTemplateLanguage emailtemplatelang in pMailTemplate.EmailTemplateLanguage)
                                {
                                    if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                                    {
                                        if (string.IsNullOrEmpty(strFromDispalyName))
                                        {
                                            if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                            {
                                                strFromDispalyName = emailtemplatelang.DisplayName;
                                            }
                                        }
                                        _mMailMessage.Subject += GetMailBodyFromTemplate(RemoveHTMLChars(emailtemplatelang.EmailSubjectText), strDictionaryDynamicValues) + " ";
                                        _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, strDictionaryDynamicValues) + "<br/> ";
                                        if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                        {
                                            _mMailMessage.Body += pstrAdditionalEmailBody;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                            return false;
                        }
                        //Add To                   
                        foreach (MailAddress address in GetAdresses(pTO))
                        {
                            if (!string.IsNullOrEmpty(address.DisplayName))
                            {
                                _mMailMessage.To.Add(new MailAddress(address.Address, address.DisplayName));
                            }
                            else
                            {
                                _mMailMessage.To.Add(new MailAddress(address.Address));
                            }
                        }

                        //Add Reply
                        if (!string.IsNullOrEmpty(pMailTemplate.EmailReplyToId))
                        {
                            _mMailMessage.ReplyTo = new MailAddress(pMailTemplate.EmailReplyToId);
                        }
                        //Add From
                        if (!string.IsNullOrEmpty(pMailTemplate.EmailFromId))
                        {
                            if (string.IsNullOrEmpty(strFromDispalyName))
                            {
                                _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId);
                            }
                            else
                            {
                                _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId, strFromDispalyName);
                            }
                        }
                        else
                        {
                            throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                        }
                        //Add CC
                        if (!string.IsNullOrEmpty(pCC))
                        {
                            foreach (MailAddress address in GetAdresses(pCC))
                            {
                                if (!string.IsNullOrEmpty(address.DisplayName))
                                {
                                    _mMailMessage.CC.Add(new MailAddress(address.Address, address.DisplayName));
                                }
                                else
                                {
                                    _mMailMessage.CC.Add(new MailAddress(address.Address));
                                }
                            }
                        }
                        //Add BCC
                        if (!string.IsNullOrEmpty(pBCC))
                        {
                            foreach (MailAddress address in GetAdresses(pBCC))
                            {
                                _mMailMessage.Bcc.Add(address);
                            }
                        }
                        if (pListAttachments != null)
                        {
                            if (pListAttachments.Count > 0)
                            {
                                foreach (Attachment attachment in pListAttachments)
                                {
                                    _mMailMessage.Attachments.Add(attachment);
                                }
                            }
                        }
                        try
                        {

                            //SendEmail(_mMailMessage);
                            strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                            strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                            SaveEmailSentLog(pMailTemplate.ID, _mMailMessage);
                        }
                        catch (SmtpFailedRecipientsException ex)
                        {
                            for (int i = 0; i < ex.InnerExceptions.Length; i++)
                            {
                                SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                                if (status == SmtpStatusCode.MailboxBusy ||
                                    status == SmtpStatusCode.MailboxUnavailable)
                                {
                                    Console.WriteLine("Delivery failed - retrying in 5 seconds.");
                                    //System.Threading.Thread.Sleep(5000);
                                    //SendEmail(_mMailMessage);
                                    strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                                    strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                                    SaveEmailSentLog(pMailTemplate.ID, _mMailMessage);
                                }
                                else
                                {
                                    Console.WriteLine("Failed to deliver message to {0}",
                                        ex.InnerExceptions[i].FailedRecipient);
                                }
                            }
                        }
                        #endregion
                    }
                }
                catch (Exception expCommon)
                {
                    CustomException expCustom = new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                }
                #endregion
            }
            catch (Exception expCommon)
            {
                CustomException expCustom = new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Send mail to a All
        /// </summary>
        /// <param name="pLearner">Learner Object</param>
        /// <param name="pEmailTemplateId">Email Template ID</para>
        /// <param name="pCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param> 
        /// <param name="pBCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <param name="pTO">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <returns></returns>
        public bool SendPublicMail(string pEmailTemplateId, string pstrAdditionalEmailBody, string pLanguageId,
            string pCC, string pBCC, string pTO, Boolean pbAddToDashboard, List<Attachment> pListAttachments)
        {
            EmailTemplate mailTemplate = new EmailTemplate();
            EmailTemplateManager mgrEmailTemplate = new EmailTemplateManager();
            mailTemplate.ID = pEmailTemplateId;
            mailTemplate.ClientId = _strClientId;
            mailTemplate = mgrEmailTemplate.Execute(mailTemplate, EmailTemplate.Method.Get);
            return SendPublicMail(mailTemplate, pstrAdditionalEmailBody, pLanguageId, pCC, pBCC, pTO, pbAddToDashboard, pListAttachments);
        }

        /// <summary>
        /// Send mail to a All
        /// </summary>
        /// <param name="pLearner">Learner Object</param>
        /// <param name="pMailTemplate">Email Template</para>
        ///  <param name="pstrAdditionalEmailBody">Additional Email Body</para>
        /// <param name="pCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param> 
        /// <param name="pBCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <param name="pTO">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <returns></returns>                
        public bool SendPublicMail(EmailTemplate pMailTemplate, string pstrAdditionalEmailBody,
            string pLanguageId, string pCC, string pBCC, string pTO, Boolean pbAddToDashboard, List<Attachment> pListAttachments)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            string strFromDispalyName = string.Empty;
            Learner learner = new Learner();
            try
            {
                if (pbAddToDashboard)
                {
                    return AddEmailToDashBoard(pMailTemplate, pLanguageId, pCC, pBCC, pTO, false, null, pListAttachments, learner);
                }


                if (pMailTemplate != null && !string.IsNullOrEmpty(pMailTemplate.ID))
                {
                    if (!string.IsNullOrEmpty(pLanguageId))
                    {
                        userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        { return entTempToFind.LanguageId == pLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == ADMIN_SITE_DEFAULT_LANGUAGE && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate != null)
                        {
                            if (string.IsNullOrEmpty(strFromDispalyName))
                            {
                                if (!string.IsNullOrEmpty(userMailTemplate.DisplayName))
                                {
                                    strFromDispalyName = userMailTemplate.DisplayName;
                                }
                            }
                            _mMailMessage.Subject = RemoveHTMLChars(userMailTemplate.EmailSubjectText);
                            _mMailMessage.Body = userMailTemplate.EmailBodyText;
                            if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                            {
                                _mMailMessage.Body += pstrAdditionalEmailBody;
                            }
                        }
                        else
                        {
                            LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                            return false;
                        }
                    }
                    else
                    {
                        foreach (EmailTemplateLanguage emailtemplatelang in pMailTemplate.EmailTemplateLanguage)
                        {
                            if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                            {
                                if (string.IsNullOrEmpty(strFromDispalyName))
                                {
                                    if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                    {
                                        strFromDispalyName = emailtemplatelang.DisplayName;
                                    }
                                }
                                _mMailMessage.Subject += emailtemplatelang.EmailSubjectText + " ";
                                _mMailMessage.Body += emailtemplatelang.EmailBodyText + "<br/> ";
                                if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                {
                                    _mMailMessage.Body += pstrAdditionalEmailBody;
                                }
                            }
                        }
                    }
                }
                else
                {
                    LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                    return false;
                }
                //Add To
                if (!string.IsNullOrEmpty(pTO))
                {
                    foreach (MailAddress address in GetAdresses(pTO))
                    {
                        if (!string.IsNullOrEmpty(address.DisplayName))
                        {
                            _mMailMessage.To.Add(new MailAddress(address.Address, address.DisplayName));
                        }
                        else
                        {
                            _mMailMessage.To.Add(new MailAddress(address.Address));
                        }

                    }
                }
                else
                {
                    LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.User.INVALID_EMAIL_ID);
                    return false;
                }

                //Add To and Reply
                if (!string.IsNullOrEmpty(pMailTemplate.EmailReplyToId))
                {
                    _mMailMessage.ReplyTo = new MailAddress(pMailTemplate.EmailReplyToId);
                }
                //Add From 
                if (!string.IsNullOrEmpty(pMailTemplate.EmailFromId))
                {
                    if (userMailTemplate != null)
                    {
                        if (string.IsNullOrEmpty(strFromDispalyName))
                        {
                            _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId);
                        }
                        else
                        {
                            _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId, strFromDispalyName);
                        }
                    }
                    else
                    {
                        _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId);
                    }
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pCC))
                {
                    foreach (MailAddress address in GetAdresses(pCC))
                    {
                        if (!string.IsNullOrEmpty(address.DisplayName))
                        {
                            _mMailMessage.CC.Add(new MailAddress(address.Address, address.DisplayName));
                        }
                        else
                        {
                            _mMailMessage.CC.Add(new MailAddress(address.Address));
                        }
                    }
                }
                //Add BCC
                if (!string.IsNullOrEmpty(pBCC))
                {
                    foreach (MailAddress address in GetAdresses(pBCC))
                    {
                        _mMailMessage.Bcc.Add(address);
                    }
                }
                if (pListAttachments != null)
                {
                    if (pListAttachments.Count > 0)
                    {
                        foreach (Attachment attachment in pListAttachments)
                        {
                            _mMailMessage.Attachments.Add(attachment);
                        }
                    }
                }

                //SendEmail(_mMailMessage);
                strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                SaveEmailSentLog(pMailTemplate.ID, _mMailMessage);
            }
            catch (Exception expCommon)
            {
                throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
            }
            return true;
        }

        /// <summary>
        /// Send mail to a All
        /// </summary>
        /// <param name="pLearner">Learner Object</param>
        /// <param name="pMailTemplate">Email Template</para>
        ///  <param name="pstrAdditionalEmailBody">Additional Email Body</para>
        /// <param name="pCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param> 
        /// <param name="pBCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <param name="pTO">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        ///  <param name="pstrDeliveryTitle">Delivery Title</param>
        /// <returns></returns>                
        public bool SendPublicMail(EmailTemplate pMailTemplate, string pstrAdditionalEmailBody,
            string pLanguageId, string pCC, string pBCC, string pTO, Boolean pbAddToDashboard, string pstrDeliveryTitle, List<Attachment> pListAttachments)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            string strFromDispalyName = string.Empty;
            Learner learner = new Learner();
            try
            {
                if (pbAddToDashboard)
                {
                    return AddEmailToDashBoard(pMailTemplate, pLanguageId, pCC, pBCC, pTO, false, pstrDeliveryTitle, pListAttachments, learner);
                }
                if (pMailTemplate != null && !string.IsNullOrEmpty(pMailTemplate.ID))
                {
                    if (!string.IsNullOrEmpty(pLanguageId))
                    {
                        userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        { return entTempToFind.LanguageId == pLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == ADMIN_SITE_DEFAULT_LANGUAGE && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate != null)
                        {
                            if (string.IsNullOrEmpty(strFromDispalyName))
                            {
                                if (!string.IsNullOrEmpty(userMailTemplate.DisplayName))
                                {
                                    strFromDispalyName = userMailTemplate.DisplayName;
                                }
                            }
                            _mMailMessage.Subject = userMailTemplate.EmailSubjectText;
                            _mMailMessage.Body = userMailTemplate.EmailBodyText;
                            if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                            {
                                _mMailMessage.Body += pstrAdditionalEmailBody;
                            }
                        }
                        else
                        {
                            LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                            return false;
                        }
                    }
                    else
                    {
                        foreach (EmailTemplateLanguage emailtemplatelang in pMailTemplate.EmailTemplateLanguage)
                        {
                            if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                            {
                                if (string.IsNullOrEmpty(strFromDispalyName))
                                {
                                    if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                    {
                                        strFromDispalyName = emailtemplatelang.DisplayName;
                                    }
                                }

                                _mMailMessage.Subject += emailtemplatelang.EmailSubjectText + " ";

                                _mMailMessage.Body += emailtemplatelang.EmailBodyText + "<br/> ";

                                if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                {
                                    _mMailMessage.Body += "<br/>" + pstrAdditionalEmailBody;
                                }
                            }
                        }
                    }
                }
                else
                {
                    LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                    return false;
                }

                //Add To and Reply
                if (!string.IsNullOrEmpty(pMailTemplate.EmailReplyToId))
                {
                    _mMailMessage.ReplyTo = new MailAddress(pMailTemplate.EmailReplyToId);
                }
                //Add From 
                if (!string.IsNullOrEmpty(pMailTemplate.EmailFromId))
                {
                    if (userMailTemplate != null)
                    {
                        if (string.IsNullOrEmpty(strFromDispalyName))
                        {
                            _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId);
                        }
                        else
                        {
                            _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId, strFromDispalyName);
                        }
                    }
                    else
                    {
                        _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId);
                    }
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pCC))
                {
                    foreach (MailAddress address in GetAdresses(pCC))
                    {
                        if (!string.IsNullOrEmpty(address.DisplayName))
                        {
                            _mMailMessage.CC.Add(new MailAddress(address.Address, address.DisplayName));
                        }
                        else
                        {
                            _mMailMessage.CC.Add(new MailAddress(address.Address));
                        }
                    }
                }
                //Add BCC
                if (!string.IsNullOrEmpty(pBCC))
                {
                    foreach (MailAddress address in GetAdresses(pBCC))
                    {
                        _mMailMessage.Bcc.Add(address);
                    }
                }
                if (pListAttachments != null)
                {
                    if (pListAttachments.Count > 0)
                    {
                        foreach (Attachment attachment in pListAttachments)
                        {
                            _mMailMessage.Attachments.Add(attachment);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(pTO))
                {
                    foreach (MailAddress address in GetAdresses(pTO))
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(address.DisplayName))
                            {
                                _mMailMessage.To.Add(new MailAddress(address.Address, address.DisplayName));
                            }
                            else
                            {
                                _mMailMessage.To.Add(new MailAddress(address.Address));
                            }
                            strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                            strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                        }
                        catch (Exception ex)
                        {

                        }
                        if (_mMailMessage.To.Count >= 0)
                            _mMailMessage.To.RemoveAt(0);
                    }
                }
                else
                {
                    LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.User.INVALID_EMAIL_ID);
                    return false;
                }


                if (!string.IsNullOrEmpty(pTO))
                {
                    foreach (MailAddress address in GetAdresses(pTO))
                    {
                        if (!string.IsNullOrEmpty(address.DisplayName))
                        {
                            _mMailMessage.To.Add(new MailAddress(address.Address, address.DisplayName));
                        }
                        else
                        {
                            _mMailMessage.To.Add(new MailAddress(address.Address));
                        }
                    }
                }
                /* Gitanjali */

                //SendEmail(_mMailMessage);
                SaveEmailSentLog(pMailTemplate.ID, _mMailMessage);
            }
            catch (Exception expCommon)
            {
                throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
            }
            return true;
        }

        /// <summary>
        /// Send Public Email Without Template
        /// </summary>
        /// <param name="pFromEmail"></param>
        /// <param name="pTO"></param>
        /// <param name="pCC"></param>
        /// <param name="pBCC"></param>
        /// <param name="pSubject"></param>
        /// <param name="pstrEmailBody"></param>
        /// <param name="pListAttachments"></param>
        /// <returns></returns>
        public bool SendPublicMail(string pFromEmail, string pTO,
           string pCC, string pBCC, string pSubject, string pstrEmailBody, List<Attachment> pListAttachments)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            try
            {
                _mMailMessage.Body = pstrEmailBody;

                //Add From 
                if (!string.IsNullOrEmpty(pFromEmail))
                {
                    _mMailMessage.From = new MailAddress(pFromEmail);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pCC))
                {
                    foreach (MailAddress address in GetAdresses(pCC))
                    {
                        if (!string.IsNullOrEmpty(address.DisplayName))
                        {
                            _mMailMessage.CC.Add(new MailAddress(address.Address, address.DisplayName));
                        }
                        else
                        {
                            _mMailMessage.CC.Add(new MailAddress(address.Address));
                        }
                    }
                }
                //Add BCC
                if (!string.IsNullOrEmpty(pBCC))
                {
                    foreach (MailAddress address in GetAdresses(pBCC))
                    {
                        _mMailMessage.Bcc.Add(address);
                    }
                }
                //Add BCC
                if (!string.IsNullOrEmpty(pSubject))
                {
                    _mMailMessage.Subject = pSubject;
                }
                if (pListAttachments != null)
                {
                    if (pListAttachments.Count > 0)
                    {
                        foreach (Attachment attachment in pListAttachments)
                        {
                            _mMailMessage.Attachments.Add(attachment);
                        }
                    }
                }
                try
                {
                    // added by Gitanjali 25.7.2010
                    if (!string.IsNullOrEmpty(pTO))
                    {
                        foreach (MailAddress address in GetAdresses(pTO))
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(address.DisplayName))
                                {
                                    _mMailMessage.To.Add(new MailAddress(address.Address, address.DisplayName));
                                }
                                else
                                {
                                    _mMailMessage.To.Add(new MailAddress(address.Address));
                                }
                                strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                                strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                            }
                            catch (Exception ex)
                            {

                            }
                            if (_mMailMessage.To.Count >= 0)
                                _mMailMessage.To.RemoveAt(0);
                        }

                        foreach (MailAddress address in GetAdresses(pTO))
                        {
                            if (!string.IsNullOrEmpty(address.DisplayName))
                            {
                                _mMailMessage.To.Add(new MailAddress(address.Address, address.DisplayName));
                            }
                            else
                            {
                                _mMailMessage.To.Add(new MailAddress(address.Address));
                            }
                        }

                    }

                    SaveEmailSentLog("", _mMailMessage);

                }
                catch
                {
                    return false;
                }
            }
            catch { return false; }
            return true;
        }

        /// <summary>
        /// Converts ',' seperated adress to MailAdressCollection
        /// </summary>
        /// <param name="pstrList">, seperated list</param>
        /// <returns>MailAddressCollection</returns>
        private MailAddressCollection GetAdresses(string pstrList)
        {
            MailAddressCollection eAdresses = new MailAddressCollection();
            MailAddress eAdresse;
            string[] arrayEmails = null;
            try
            {
                pstrList = pstrList.Replace(Environment.NewLine, "");
                if (pstrList.Contains(","))
                {
                    arrayEmails = pstrList.Split(',');
                }
                else
                {
                    arrayEmails = pstrList.Split(';');
                }
                foreach (string strAdress in arrayEmails)
                {

                    try
                    {
                        if (strAdress.Contains("<") && strAdress.Contains(">"))
                        {
                            string strEmail = strAdress.Substring(strAdress.IndexOf("<") + 1);
                            strEmail = strEmail.Replace(">", "");
                            if (ValidationManager.ValidateString(strEmail.Trim(), ValidationManager.DataType.EmailID))
                            {
                                eAdresse = new MailAddress(strEmail.Trim(), strAdress.Substring(0, strAdress.IndexOf("<")));
                                eAdresses.Add(eAdresse);
                            }
                            else
                            {
                                LogMessage += "Invalid Email: " + strAdress + " ";
                            }
                        }
                        else
                        {
                            if (ValidationManager.ValidateString(strAdress.Trim(), ValidationManager.DataType.EmailID))
                            {
                                eAdresse = new MailAddress(strAdress.Trim());
                                eAdresses.Add(eAdresse);
                            }
                            else
                            {
                                LogMessage += "Invalid Email: " + strAdress + " ";
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception expString)
            {
                _expCustom = new CustomException(YPLMS.Services.Messages.User.INVALID_EMAIL_ID, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, expString, true);
               // LogMessage = _expCustom.GetCustomMessage(null);
            }
            return eAdresses;
        }

        /// <summary>
        /// Get Mail Body From Template
        /// </summary>
        /// <param name="pstrEmailTemplateContent"></param>
        /// <param name="pstrLanguage"></param>
        /// <param name="pLearner"></param>
        /// <returns></returns>
        public string GetMailBodyFromTemplate(string pstrEmailTemplateContent, string pstrLanguage, Learner pLearner)
        {
            string emailTemplateContent = string.Empty;
            DataSet dsetCntnModLst = new DataSet();
            ImportDefination importDefination = new ImportDefination();
            ImportDefinitionAdaptor mgrImportDefination = new ImportDefinitionAdaptor();
            List<ImportDefination> listImportDefination = new List<ImportDefination>();
            string strFieldType = string.Empty;
            string strEmailFieldName = string.Empty;
            string strImportDefinationID = string.Empty;
            string strActivityInfo = string.Empty;
            try
            {
                emailTemplateContent = pstrEmailTemplateContent;
                #region Replace IDF Values
                try
                {
                    importDefination.ClientId = _strClientId;
                    //importDefination.ImportAction = ImportAction.Report;
                    importDefination.ImportAction = ImportAction.None;
                    listImportDefination = mgrImportDefination.GetImportDefinationList(importDefination); //ExecuteDataSet(importDefination, ImportDefination.ListMethod.GetAll);
                    YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
                    dsetCntnModLst = dsConverter.ConvertToDataSet<ImportDefination>(listImportDefination);
                    if (dsetCntnModLst != null)
                    {
                        if (dsetCntnModLst.Tables["ImportDefination"].Rows.Count > 0)
                        {
                            foreach (DataRow drowTemplate in dsetCntnModLst.Tables["ImportDefination"].Rows)
                            {
                                if (!string.IsNullOrEmpty(drowTemplate["FieldTypes"].ToString()) && !string.IsNullOrEmpty(drowTemplate["FieldName"].ToString()))
                                {
                                    try
                                    {
                                        strFieldType = string.Empty;
                                        strEmailFieldName = string.Empty;
                                        strImportDefinationID = string.Empty;
                                        strFieldType = drowTemplate["FieldTypes"].ToString().Trim();
                                        strImportDefinationID = drowTemplate["ID"].ToString().Trim();
                                        strEmailFieldName = strFieldType + "." + strImportDefinationID;
                                        if (pLearner != null)
                                        {
                                            //added by Gitanjali 24.01.2010 to get learner/user date format
                                            if (!string.IsNullOrEmpty(pLearner.PreferredDateFormat))
                                                _strClientDateFormat = "{0:" + pLearner.PreferredDateFormat + "}";

                                            emailTemplateContent = emailTemplateContent.Replace("&lt;%" + strEmailFieldName + "%&gt;", GetIDFieldValue(strImportDefinationID, strFieldType, pstrLanguage, pLearner));
                                        }
                                        else
                                        {
                                            emailTemplateContent = emailTemplateContent.Replace("&lt;%" + strEmailFieldName + "%&gt;", "");
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
                #endregion
            }
            catch
            {
                return emailTemplateContent;
            }
            return emailTemplateContent;
        }


        /// <summary>
        /// Get Mail Body From Template
        /// </summary>
        /// <param name="pstrEmailTemplateContent"></param>
        /// <param name="pstrDictDynamicValues"> String Dictionary to resolve dynamic values</param>        
        /// <returns></returns>
        public string GetMailBodyFromTemplate(string pstrEmailTemplateContent, Dictionary<string, string> pstrDictDynamicValues)
        {
            string emailTemplateContent = string.Empty;
            try
            {
                emailTemplateContent = pstrEmailTemplateContent;
                if (emailTemplateContent.Contains("Miscellaneous.URL") && pstrDictDynamicValues != null)
                {
                    Client entClient = null;
                    entClient = new Client();
                    entClient.ID = _strClientId;
                    ClientDAM _clientmanager = new ClientDAM();
                    //string strAccessURL = _clientmanager.ExecuteForClientAccessURL(entClient, Client.Method.GetClientAccessURL); comment by vinod
                    string strAccessURL = _clientmanager.GetClientAccessURL(entClient);
                    pstrDictDynamicValues.Add("Miscellaneous.URL", strAccessURL);
                }
                if (pstrDictDynamicValues != null & pstrDictDynamicValues.Count > 0)
                {
                    foreach (KeyValuePair<string, string> item in pstrDictDynamicValues)
                    {
                        if (emailTemplateContent.Contains("&lt;%"))
                        {
                            emailTemplateContent = emailTemplateContent.Replace("&lt;%" + item.Key.ToString() + "%&gt;", Convert.ToString(item.Value));
                        }
                        else
                        if (emailTemplateContent.Contains("&lt; %"))
                        {
                            emailTemplateContent = emailTemplateContent.Replace("&lt; %" + item.Key.ToString() + "% &gt;", Convert.ToString(item.Value));
                        }
                        else
                        {
                            emailTemplateContent = emailTemplateContent.Replace("<%" + item.Key.ToString() + "%>", Convert.ToString(item.Value));
                        }
                    }
                }
            }
            catch
            {
                return emailTemplateContent;
            }
            return emailTemplateContent;
        }

        /// <summary>
        /// Get IDF Vlaue
        /// </summary>
        /// <param name="pstrFieldID"></param>
        /// <param name="pstrFieldType"></param>
        /// <param name="learner"></param>
        /// <returns></returns>
        private string GetIDFieldValue(string pstrFieldID, string pstrFieldType, string pstrLanguage, Learner learner)
        {
            string strRetValue = string.Empty;
            try
            {
                if (learner != null)
                {
                    if (pstrFieldType == ImportDefination.FieldType.Standard.ToString() || pstrFieldType == ImportDefination.FieldType.StandardCustom.ToString())
                    {
                        PropertyInfo pi = learner.GetType().GetProperty(pstrFieldID);
                        string strPropType = pi != null ? Convert.ToString(pi.PropertyType.FullName) : "";
                        switch (strPropType)
                        {
                            case "System.String":
                                {
                                    if (pstrFieldID == "CurrentRegionView")
                                    {
                                        strRetValue =  GetRegionalView(Convert.ToString( pi.GetValue(learner, null)));
                                    }
                                    else
                                    {
                                        strRetValue = Convert.ToString( pi.GetValue(learner, null));
                                    }
                                    break;
                                }
                            case "System.DateTime":
                                {
                                    strRetValue = Convert.ToString(pi.GetValue(learner, null));
                                    if (!string.IsNullOrEmpty(strRetValue))
                                    {
                                        DateTime dtPropVal = Convert.ToDateTime(strRetValue);
                                        if (DateTime.MinValue.CompareTo(dtPropVal) < 0)
                                        {
                                            strRetValue = string.Format(_strClientDateFormat, dtPropVal);
                                        }
                                        else
                                            strRetValue = "";
                                    }
                                    break;
                                }
                            case "System.Boolean":
                                {
                                    Boolean bFieldValue = (Boolean)pi.GetValue(learner, null);
                                    string strValue = "0";
                                    if (bFieldValue)
                                    {
                                        strValue = "1";
                                    }
                                    LookupType lookupType = (LookupType)Enum.Parse(typeof(LookupType), pi.Name);
                                    strRetValue = GetLookUpText(lookupType, strValue, pstrLanguage);
                                    break;
                                }
                            default:
                                break;
                        }
                        return strRetValue;
                    }
                    else if (pstrFieldType == ImportDefination.FieldType.CustomField.ToString())
                    {
                        if (learner.UserCustomFieldValue.Count > 0)
                        {
                            UserCustomFieldValue usercustomfieldvalue = learner.UserCustomFieldValue.Find(
                                delegate (UserCustomFieldValue usercustomfieldvalueToMatch)
                                {
                                    return (usercustomfieldvalueToMatch.CustomFieldId == pstrFieldID);
                                });
                            if (usercustomfieldvalue != null)
                            {
                                if (!string.IsNullOrEmpty(usercustomfieldvalue.EnteredValue))
                                {
                                    //return usercustomfieldvalue.EnteredValue;
                                    //added by Gitanjali 27.01.2010 for date formatting as per user format
                                    DateTime dtPropVal;
                                    if (DateTime.TryParse(usercustomfieldvalue.EnteredValue, out dtPropVal))
                                    {
                                        if (DateTime.MinValue.CompareTo(dtPropVal) < 0)
                                        {
                                            strRetValue = string.Format(_strClientDateFormat, dtPropVal);
                                        }
                                        else
                                            strRetValue = "";
                                    }
                                    else
                                    {
                                        strRetValue = usercustomfieldvalue.EnteredValue;
                                    }
                                    return strRetValue;
                                }
                            }
                        }
                        return strRetValue;
                    }
                    else if (pstrFieldType == ImportDefination.FieldType.Miscellaneous.ToString()) // this else if added by sarita on 18-Nov-13
                    {
                        if (pstrFieldID == "URL")
                        {
                            Client entClient = null;
                            entClient = new Client();
                            entClient.ID = _strClientId;
                            ClientDAM _clientmanager = new ClientDAM();
                           // strRetValue = _clientmanager.ExecuteForClientAccessURL(entClient, Client.Method.GetClientAccessURL); comment by vinod
                            strRetValue = _clientmanager.GetClientAccessURL(entClient);
                        }
                        else
                        {
                            OTP _entOTP = new OTP();
                            OTPAdaptor _mgrOTP = new OTPAdaptor();
                            _entOTP.ClientId = _strClientId;
                            _entOTP.SystemUserGuid = learner.ID;
                            //_entOTP = _mgrOTP.Execute(_entOTP, OTP.Method.GetOTPNumber); comment by vinod
                            _entOTP = _mgrOTP.GetOTPNumber(_entOTP);
                            strRetValue = _entOTP.OTPNumber.ToString();
                        }
                        return strRetValue;
                    }
                    else
                    {
                        return strRetValue;
                    }
                }
                else
                {
                    return strRetValue;
                }
            }
            catch
            {
                return strRetValue;
            }
        }

        /// <summary>
        /// Save Email Sent Log
        /// </summary>
        /// <param name="pstrEmailTemplateID">Email Template ID</param>
        /// <param name="pMailMessage">Mail Message</param>
        private void SaveEmailSentLog(string pstrEmailTemplateID, MailMessage pMailMessage)
        {
            try
            {
                if (pMailMessage != null)
                {
                    EmailSentLog emailsentlog = new EmailSentLog();
                    EmailSentLogManager mgrEmailSentLog = new EmailSentLogManager();
                    emailsentlog.ClientId = _strClientId;
                    emailsentlog.EmailTemplateId = pstrEmailTemplateID;
                    emailsentlog.AutoEmailEventId = AutoEmailEventID;
                    emailsentlog.DateSent = DateTime.Now.ToUniversalTime();

                    emailsCount = emailsCount + pMailMessage.CC.Count + pMailMessage.Bcc.Count;
                    strLog.Append(DateTime.Now.ToUniversalTime() + " Sent to " + emailsCount + " Recipient(s).");
                    strLog.Append("<br/>");
                    strLog.Append(DateTime.Now.ToUniversalTime() + " Close Log");
                    strLog.Append("<br/><br/>");
                    strLog.Append("<b>Invalid Email Addresses:</b><br/>" + strInvalidEmails);
                    emailsentlog.EmailLog = Convert.ToString(strLog);

                    emailsentlog.RecipiantEmailAddress = pMailMessage.To.ToString();
                    emailsentlog.RecipiantCCEmailAddress = pMailMessage.CC.ToString();
                    emailsentlog.RecipiantBCCEmailAddress = pMailMessage.Bcc.ToString();
                    emailsentlog = mgrEmailSentLog.Execute(emailsentlog, EmailSentLog.Method.Add);
                }
            }
            catch
            {
            }
        }

        public StringBuilder SendEmailWithReturnLog(MailMessage pMailMessage)
        {
            StringBuilder strReturn = new StringBuilder();
            string toEAddress = string.Empty;
            try
            {
                MailMessage mailMessage = new MailMessage();
                List<MailAddress> ToAddresses = RemovedRestrictedDomains(pMailMessage.To);
                List<MailAddress> CCAddresses = RemovedRestrictedDomains(pMailMessage.CC);
                List<MailAddress> BCCAddresses = RemovedRestrictedDomains(pMailMessage.Bcc);

                pMailMessage.To.Clear();
                pMailMessage.CC.Clear();
                pMailMessage.Bcc.Clear();

                foreach (MailAddress toaddress in ToAddresses)
                {
                    strReturn.Append(DateTime.Now.ToUniversalTime());
                    strReturn.Append("  Message sending to: " + formatMailAddress(toaddress));
                    toEAddress = toaddress.Address;
                    pMailMessage.To.Add(toaddress);
                }
                if (pMailMessage.To.Count < 0 || pMailMessage.To.Count == 0)
                {
                    if (!string.IsNullOrEmpty(pMailMessage.From.ToString()))
                    {
                        strReturn.Append(DateTime.Now.ToUniversalTime());
                        strReturn.Append("  Message sending to: " + formatMailAddress(pMailMessage.From));
                        pMailMessage.To.Add(pMailMessage.From);
                    }
                }
                foreach (MailAddress ccaddress in CCAddresses)
                {
                    strReturn.Append("  CC: " + formatMailAddress(ccaddress));
                    pMailMessage.CC.Add(ccaddress);
                }
                foreach (MailAddress bccaddress in BCCAddresses)
                {
                    strReturn.Append("  BCC: " + formatMailAddress(bccaddress));
                    pMailMessage.Bcc.Add(bccaddress);
                }
                SMTPServer.Send(pMailMessage);


            }
            catch (Exception ex)
            {
                strReturn.Append("Sending failed :" + ex.Message);
                strInvalidEmails.Append(toEAddress + "<br/>");
            }

            return strReturn.Append("<br/>");
        }

        public StringBuilder SendEmailAddresses(MailMessage pMailMessage)
        {
            StringBuilder strReturn = new StringBuilder();
            try
            {
                MailMessage mailMessage = new MailMessage();
                List<MailAddress> ToAddresses = RemovedRestrictedDomains(pMailMessage.To);
                List<MailAddress> CCAddresses = RemovedRestrictedDomains(pMailMessage.CC);
                List<MailAddress> BCCAddresses = RemovedRestrictedDomains(pMailMessage.Bcc);

                pMailMessage.To.Clear();
                pMailMessage.CC.Clear();
                pMailMessage.Bcc.Clear();

                foreach (MailAddress toaddress in ToAddresses)
                {
                    strReturn.Append(toaddress + ",");

                    pMailMessage.To.Add(toaddress);
                    emailsCount++;
                }
                if (pMailMessage.To.Count < 0 || pMailMessage.To.Count == 0)
                {
                    if (!string.IsNullOrEmpty(pMailMessage.From.ToString()))
                    {
                        pMailMessage.To.Add(pMailMessage.From);
                    }
                }
                foreach (MailAddress ccaddress in CCAddresses)
                {
                    pMailMessage.CC.Add(ccaddress);
                }
                foreach (MailAddress bccaddress in BCCAddresses)
                {
                    pMailMessage.Bcc.Add(bccaddress);
                }

            }
            catch (Exception ex)
            {
                return strReturn;
            }

            return strReturn;
        }

        public string formatMailAddress(MailAddress mailAddress)
        {
            string retMailAddress = string.Empty;
            string address = string.Empty;
            string user = string.Empty;
            MailAddress eMailAdd = new MailAddress(mailAddress.ToString());
            address = eMailAdd.Address.ToString();
            user = eMailAdd.DisplayName.ToString();
            retMailAddress = address + "(Name: " + user + ")";

            return retMailAddress;
        }

        /// <summary>
        /// Add Email To DashBoard
        /// </summary>
        /// <param name="pEmailTemplate"></param>
        /// <param name="pLanguageId"></param>
        /// <param name="pCC"></param>
        /// <param name="pBCC"></param>
        /// <param name="pTO"></param>
        /// <param name="pPersonalize"></param>
        /// <param name="pstrDeliveryTitle"></param>
        /// <param name="pListAttachments"></param>
        /// <returns></returns>
        private bool AddEmailToDashBoard(EmailTemplate pEmailTemplate, string pLanguageId,
            string pCC, string pBCC, string pTO, Boolean pPersonalize, string pstrDeliveryTitle, List<Attachment> pListAttachments, Learner pLearner)
        {
            bool RetValue = false;
            Learner _entCurrentUser = new Learner();
            EmailDeliveryDashboardManager mgrEmailDeliveryDashboard = new EmailDeliveryDashboardManager();
            EmailDeliveryDashboard emaildeliverydashboard = new EmailDeliveryDashboard();
            LearnerDAM mgrLearner = new LearnerDAM();
            if (!string.IsNullOrEmpty(pLearner.ID) && !string.IsNullOrEmpty(pLearner.ClientId))  //if (LMSSession.IsInSession(Client.CLIENT_SESSION_ID) && LMSSession.IsInSession(Learner.USER_SESSION_ID))
            {
                _entCurrentUser.ClientId = _strClientId;
                _entCurrentUser.ID = pLearner.ID;
                _entCurrentUser = mgrLearner.GetUserByID(_entCurrentUser);// (Learner)LMSSession.GetValue(Learner.USER_SESSION_ID);
                emaildeliverydashboard.ClientId = _strClientId;
                if (!string.IsNullOrEmpty(pstrDeliveryTitle))
                {
                    emaildeliverydashboard.EmailDeliveryTitle = pstrDeliveryTitle;
                }
                else
                {
                    emaildeliverydashboard.EmailDeliveryTitle = "Auto Email";
                }
                emaildeliverydashboard.EmailTemplateID = pEmailTemplate.ID;
                if (!string.IsNullOrEmpty(pTO))
                {
                    emaildeliverydashboard.ToList = pTO;
                }
                if (!string.IsNullOrEmpty(pCC))
                {
                    emaildeliverydashboard.CCList = pCC;
                }
                if (!string.IsNullOrEmpty(pBCC))
                {
                    emaildeliverydashboard.BCCList = pBCC;
                }
                emaildeliverydashboard.IsCCManager = false;
                emaildeliverydashboard.IsBCCManager = false;
                emaildeliverydashboard.IsImmediate = true;
                emaildeliverydashboard.DateTimeSet = DateTime.Now.ToUniversalTime();
                if (!string.IsNullOrEmpty(pLanguageId) || pPersonalize)
                {
                    emaildeliverydashboard.IsUserPreferredLanguage = true;
                    emaildeliverydashboard.IsSiteDefaultLanguage = false;
                    emaildeliverydashboard.IsAllLanguages = false;
                }
                else
                {
                    emaildeliverydashboard.IsUserPreferredLanguage = false;
                    emaildeliverydashboard.IsSiteDefaultLanguage = true;
                    emaildeliverydashboard.IsAllLanguages = false;

                }
                emaildeliverydashboard.IsPersonalized = pPersonalize;
                if (pListAttachments != null)
                {
                    if (pListAttachments.Count > 0)
                    {
                        emaildeliverydashboard.AttachmentPathList = GetAttachmentsPath(pListAttachments);
                    }
                }
                emaildeliverydashboard.DeliveryApprovalStatus = YPLMS.Services.Common.GetDescription(EmailDeliveryDashboard.ApprovalStatus.PendingApproval);
                emaildeliverydashboard.CreatedById = _entCurrentUser.ID;
                emaildeliverydashboard.LastModifiedById = _entCurrentUser.ID;
                emaildeliverydashboard = mgrEmailDeliveryDashboard.Execute(emaildeliverydashboard, EmailDeliveryDashboard.Method.Add);
                if (emaildeliverydashboard != null)
                {
                    RetValue = true;
                }
            }
            else
            {
                RetValue = false;
            }
            return RetValue;
        }


        // added by Gitanjali 4.8.2010
        private bool AddEmailToDashBoard(string pLearnerId, EmailTemplate pEmailTemplate, string pLanguageId,
        string pCC, string pBCC, string pTO, Boolean pPersonalize, string pstrDeliveryTitle, List<Attachment> pListAttachments)
        {
            bool RetValue = false;
            Learner _entCurrentUser = new Learner();
            EmailDeliveryDashboardManager mgrEmailDeliveryDashboard = new EmailDeliveryDashboardManager();
            EmailDeliveryDashboard emaildeliverydashboard = new EmailDeliveryDashboard();
            LearnerDAM mgrLearner = new LearnerDAM();
            if(!string.IsNullOrEmpty(pLearnerId)) // if (LMSSession.IsInSession(Client.CLIENT_SESSION_ID) && LMSSession.IsInSession(Learner.USER_SESSION_ID))
            {
                _entCurrentUser.ClientId = _strClientId;
                _entCurrentUser.ID = pLearnerId;
                _entCurrentUser = mgrLearner.GetUserByID(_entCurrentUser);// (Learner)LMSSession.GetValue(Learner.USER_SESSION_ID);
                emaildeliverydashboard.ClientId = _strClientId;
                if (!string.IsNullOrEmpty(pstrDeliveryTitle))
                {
                    emaildeliverydashboard.EmailDeliveryTitle = pstrDeliveryTitle;
                }
                else
                {
                    emaildeliverydashboard.EmailDeliveryTitle = "Auto Email";
                }
                emaildeliverydashboard.EmailTemplateID = pEmailTemplate.ID;
                if (!string.IsNullOrEmpty(pTO))
                {
                    emaildeliverydashboard.ToList = pTO;
                }
                if (!string.IsNullOrEmpty(pCC))
                {
                    emaildeliverydashboard.CCList = pCC;
                }
                if (!string.IsNullOrEmpty(pBCC))
                {
                    emaildeliverydashboard.BCCList = pBCC;
                }
                emaildeliverydashboard.IsCCManager = false;
                emaildeliverydashboard.IsBCCManager = false;
                emaildeliverydashboard.IsImmediate = true;
                emaildeliverydashboard.DateTimeSet = DateTime.Now.ToUniversalTime();
                if (!string.IsNullOrEmpty(pLanguageId) || pPersonalize)
                {
                    emaildeliverydashboard.IsUserPreferredLanguage = true;
                    emaildeliverydashboard.IsSiteDefaultLanguage = false;
                    emaildeliverydashboard.IsAllLanguages = false;
                }
                else
                {
                    emaildeliverydashboard.IsUserPreferredLanguage = false;
                    emaildeliverydashboard.IsSiteDefaultLanguage = true;
                    emaildeliverydashboard.IsAllLanguages = false;

                }
                emaildeliverydashboard.IsPersonalized = pPersonalize;
                if (pListAttachments != null)
                {
                    if (pListAttachments.Count > 0)
                    {
                        emaildeliverydashboard.AttachmentPathList = GetAttachmentsPath(pListAttachments);
                    }
                }
                emaildeliverydashboard.DeliveryApprovalStatus = YPLMS.Services.Common.GetDescription(EmailDeliveryDashboard.ApprovalStatus.PendingApproval);
                emaildeliverydashboard.CreatedById = _entCurrentUser.ID;
                emaildeliverydashboard.LastModifiedById = _entCurrentUser.ID;
                emaildeliverydashboard.LearnerId = pLearnerId;
                emaildeliverydashboard = mgrEmailDeliveryDashboard.Execute(emaildeliverydashboard, EmailDeliveryDashboard.Method.Add);
                if (emaildeliverydashboard != null)
                {
                    RetValue = true;
                }
            }
            else
            {
                RetValue = false;
            }
            return RetValue;
        }

        /// <summary>
        /// Get Look Up Text
        /// </summary>
        /// <param name="lookupType"></param>
        /// <param name="pstrValue"></param>
        /// <param name="pstrLang"></param>
        /// <returns></returns>
        public string GetLookUpText(LookupType lookupType, string pstrValue, string pstrLang)
        {
            Lookup entLookup = new Lookup();
            entLookup.ClientId = _strClientId;
            entLookup.LookupType = lookupType;
            entLookup.LanguageId = pstrLang;
            LookUpManager mgrLookUp = new LookUpManager();
            try
            {

                List<Lookup> entListLookup = mgrLookUp.Execute(entLookup, Lookup.ListMethod.GetAllByLookupType);
                if (entListLookup != null)
                {
                    foreach (Lookup entLookupLi in entListLookup)
                    {
                        if (entLookupLi.LookupValue.ToLower() == pstrValue.ToLower())
                        {
                            return entLookupLi.LookupText;
                        }
                    }
                }
                else
                {
                    return pstrValue;
                }
            }
            catch
            {
            }
            return string.Empty;
        }

        /// <summary>
        /// Get To List For Dashboard from List of Learners
        /// </summary>
        /// <param name="pListLearner"> Learners List</param>
        /// <returns></returns>
        public string GetTOListFromLearnersEmails(List<Learner> pListLearner)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            StringBuilder strEmailAddress = new StringBuilder("");
            string strTOList = string.Empty;
            try
            {
                if (pListLearner != null && pListLearner.Count > 0)
                {
                    foreach (Learner learner in pListLearner)
                    {
                        if (!string.IsNullOrEmpty(learner.EmailID))
                        {
                            //if (ValidationManager.ValidateString(learner.EmailID, ValidationManager.DataType.EmailID))
                            {
                                strEmailAddress.Append(learner.EmailID + ",");
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(strTOList))
                    {
                        strTOList = strTOList.Replace(";", ",");
                        strTOList = strTOList + "," + strEmailAddress.ToString();
                    }
                    else
                    {
                        strTOList = strEmailAddress.ToString().Remove(strEmailAddress.ToString().LastIndexOf(','));
                    }
                }
                return strTOList;
            }
            catch { }
            return strTOList;
        }



        /// <summary>
        /// Get To List For Dashboard from List of Learners
        /// </summary>
        /// <param name="pListLearner"> Learners List</param>
        /// <returns></returns>
        public string GetTOListFromLearners(List<Learner> pListLearner)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            StringBuilder strEmailAddress = new StringBuilder("");
            string strTOList = string.Empty;
            try
            {
                if (pListLearner != null && pListLearner.Count > 0)
                {
                    foreach (Learner learner in pListLearner)
                    {
                        if (!string.IsNullOrEmpty(learner.EmailID))
                        {
                            if (ValidationManager.ValidateString(learner.EmailID, ValidationManager.DataType.EmailID))
                            {
                                strEmailAddress.Append(learner.EmailID + ",");
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(strTOList))
                    {
                        strTOList = strTOList.Replace(";", ",");
                        strTOList = strTOList + "," + strEmailAddress.ToString();
                    }
                    else
                    {
                        strTOList = strEmailAddress.ToString().Remove(strEmailAddress.ToString().LastIndexOf(','));
                    }
                }
                return strTOList;
            }
            catch { }
            return strTOList;
        }

        /// <summary>
        /// Get Attachments Path
        /// </summary>
        /// <param name="pListAttachments"></param>
        /// <returns></returns>
        public string GetAttachmentsPath(List<Attachment> pListAttachments)
        {
            string strAttachments = string.Empty;
            try
            {
                if (pListAttachments != null)
                {
                    if (pListAttachments.Count > 0)
                    {
                        foreach (Attachment attachment in pListAttachments)
                        {
                            if (!string.IsNullOrEmpty(attachment.Name))
                            {
                                if (!string.IsNullOrEmpty(strAttachments))
                                {
                                    strAttachments = strAttachments + "," + attachment.Name.ToString();
                                }
                                else
                                {
                                    strAttachments = attachment.Name.ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return strAttachments;
        }

        /// <summary>
        /// Removed Restricted Domains
        /// </summary>
        /// <param name="pMailAddressCollection"></param>
        /// <returns></returns>
        public List<MailAddress> RemovedRestrictedDomains(MailAddressCollection pMailAddressCollection)
        {
            List<MailAddress> eAdresses = new List<MailAddress>();
            if (!string.IsNullOrEmpty(AllowedDomains))
            {

                string[] arrayIDs = null;
                AllowedDomains = AllowedDomains.Replace(Environment.NewLine, "");
                if (AllowedDomains.Contains(","))
                {
                    arrayIDs = AllowedDomains.Split(',');
                }
                else
                {
                    arrayIDs = new string[] { AllowedDomains };
                }

                foreach (MailAddress address in pMailAddressCollection)
                {
                    foreach (string strID in arrayIDs)
                    {
                        if (strID.ToLower() == address.Host.ToLower())
                        {
                            eAdresses.Add(address);
                        }
                    }
                }
            }
            else
            {
                foreach (MailAddress address in pMailAddressCollection)
                {
                    eAdresses.Add(address);
                }
            }
            return eAdresses;
        }

        /// <summary>
        /// Get Regional View
        /// </summary>
        /// <param name="strRegionViewID"></param>
        /// <returns></returns>
        private string GetRegionalView(string strRegionViewID)
        {
            string strRegionView = string.Empty;
            RegionViewAdaptor mgrRegionView = new RegionViewAdaptor();
            RegionView entRegView = new RegionView();
            try
            {
                entRegView.ClientId = _strClientId;
                entRegView.ID = strRegionViewID;
                // entRegView = mgrRegionView.Execute(entRegView, RegionView.Method.Get);
                entRegView = mgrRegionView.GetRegionViewById(entRegView);
                if (!string.IsNullOrEmpty(entRegView.ID) && !string.IsNullOrEmpty(entRegView.RegionViewName))
                {
                    strRegionView = entRegView.RegionViewName;
                }

            }
            catch
            {
            }
            return strRegionView;
        }

        /// <summary>
        /// Send mail to a All
        /// </summary>
        /// Sujit Added
        /// <param name="pLearner">Learner Object</param>
        /// <param name="pEmailTemplateId">Email Template ID</para>
        /// <param name="pCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param> 
        /// <param name="pBCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <param name="pTO">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <returns></returns>
        public bool SendPublicMailForLink(string pEmailTemplateId, string pstrAdditionalEmailBody, string pLanguageId,
            string pCC, string pBCC, string pTO, Boolean pbAddToDashboard, List<Attachment> pListAttachments, Learner learner)
        {
            EmailTemplate mailTemplate = new EmailTemplate();
            EmailTemplateManager mgrEmailTemplate = new EmailTemplateManager();
            mailTemplate.ID = pEmailTemplateId;
            mailTemplate.ClientId = _strClientId;
            mailTemplate = mgrEmailTemplate.Execute(mailTemplate, EmailTemplate.Method.Get);
            return SendPublicMailForLink(mailTemplate, pstrAdditionalEmailBody, pLanguageId, pCC, pBCC, pTO, pbAddToDashboard, pListAttachments, learner);
        }

        /// <summary>
        /// Send mail to a All
        /// </summary>
        /// Sujit  Added
        /// <param name="pLearner">Learner Object</param>
        /// <param name="pMailTemplate">Email Template</para>
        ///  <param name="pstrAdditionalEmailBody">Additional Email Body</para>
        /// <param name="pCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param> 
        /// <param name="pBCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <param name="pTO">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <returns></returns>                
        public bool SendPublicMailForLink(EmailTemplate pMailTemplate, string pstrAdditionalEmailBody,
            string pLanguageId, string pCC, string pBCC, string pTO, Boolean pbAddToDashboard, List<Attachment> pListAttachments, Learner pLearner)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            Learner learner = new Learner();
            string strFromDispalyName = string.Empty;
            try
            {
                if (pbAddToDashboard)
                {
                    return AddEmailToDashBoard(pMailTemplate, pLanguageId, pCC, pBCC, pTO, false, null, pListAttachments, learner);
                }


                if (pMailTemplate != null && !string.IsNullOrEmpty(pMailTemplate.ID))
                {
                    if (!string.IsNullOrEmpty(pLanguageId))
                    {
                        userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        { return entTempToFind.LanguageId == pLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = pMailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == ADMIN_SITE_DEFAULT_LANGUAGE && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate != null)
                        {
                            //_mMailMessage.Subject = userMailTemplate.EmailSubjectText;
                            //_mMailMessage.Body = userMailTemplate.EmailBodyText;
                            if (string.IsNullOrEmpty(strFromDispalyName))
                            {
                                if (!string.IsNullOrEmpty(userMailTemplate.DisplayName))
                                {
                                    strFromDispalyName = userMailTemplate.DisplayName;
                                }
                            }

                            _mMailMessage.Subject = GetMailBodyFromTemplate(RemoveHTMLChars(userMailTemplate.EmailSubjectText), userMailTemplate.LanguageId, pLearner);
                            _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, pLearner);
                            if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                            {
                                _mMailMessage.Body += pstrAdditionalEmailBody;
                            }
                        }
                        else
                        {
                            LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                            return false;
                        }
                    }
                    else
                    {
                        foreach (EmailTemplateLanguage emailtemplatelang in pMailTemplate.EmailTemplateLanguage)
                        {
                            if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                            {
                                //_mMailMessage.Subject += emailtemplatelang.EmailSubjectText + " ";
                                //_mMailMessage.Body += emailtemplatelang.EmailBodyText + "<br/> ";
                                if (string.IsNullOrEmpty(strFromDispalyName))
                                {
                                    if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                    {
                                        strFromDispalyName = emailtemplatelang.DisplayName;
                                    }
                                }
                                _mMailMessage.Subject = GetMailBodyFromTemplate(RemoveHTMLChars(emailtemplatelang.EmailSubjectText), emailtemplatelang.LanguageId, pLearner);
                                _mMailMessage.Body = GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, pLearner);
                                if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                {
                                    _mMailMessage.Body += pstrAdditionalEmailBody;
                                }
                            }
                        }
                    }
                }
                else
                {
                    LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                    return false;
                }
                //Add To
                if (!string.IsNullOrEmpty(pTO))
                {
                    foreach (MailAddress address in GetAdresses(pTO))
                    {
                        if (!string.IsNullOrEmpty(address.DisplayName))
                        {
                            _mMailMessage.To.Add(new MailAddress(address.Address, address.DisplayName));
                        }
                        else
                        {
                            _mMailMessage.To.Add(new MailAddress(address.Address));
                        }

                    }
                }
                else
                {
                    LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.User.INVALID_EMAIL_ID);
                    return false;
                }

                //Add To and Reply
                if (!string.IsNullOrEmpty(pMailTemplate.EmailReplyToId))
                {
                    _mMailMessage.ReplyTo = new MailAddress(pMailTemplate.EmailReplyToId);
                }
                //Add From 
                if (!string.IsNullOrEmpty(pMailTemplate.EmailFromId))
                {
                    if (userMailTemplate != null)
                    {
                        if (string.IsNullOrEmpty(strFromDispalyName))
                        {
                            _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId);
                        }
                        else
                        {
                            _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId, strFromDispalyName);
                        }
                    }
                    else
                    {
                        _mMailMessage.From = new MailAddress(pMailTemplate.EmailFromId);
                    }
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pCC))
                {
                    foreach (MailAddress address in GetAdresses(pCC))
                    {
                        if (!string.IsNullOrEmpty(address.DisplayName))
                        {
                            _mMailMessage.CC.Add(new MailAddress(address.Address, address.DisplayName));
                        }
                        else
                        {
                            _mMailMessage.CC.Add(new MailAddress(address.Address));
                        }
                    }
                }
                //Add BCC
                if (!string.IsNullOrEmpty(pBCC))
                {
                    foreach (MailAddress address in GetAdresses(pBCC))
                    {
                        _mMailMessage.Bcc.Add(address);
                    }
                }
                if (pListAttachments != null)
                {
                    if (pListAttachments.Count > 0)
                    {
                        foreach (Attachment attachment in pListAttachments)
                        {
                            _mMailMessage.Attachments.Add(attachment);
                        }
                    }
                }

                //SendEmail(_mMailMessage);
                strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                SaveEmailSentLog(pMailTemplate.ID, _mMailMessage);
            }
            catch (Exception expCommon)
            {
                throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
            }
            return true;
        }
        public static string RemoveHTMLChars(string str)
        {
            string[] chars = new string[] { "&nbsp;", "&quot;", "&amp;", "\n", "\r", "," };
            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[0], " ");
                    str = str.Replace(chars[1], "\"");
                    str = str.Replace(chars[2], "&");
                    str = str.Replace(chars[3], "");
                    str = str.Replace(chars[4], "");
                    str = str.Replace(chars[5], "");
                }
            }
            return str;
        }

    }
}
