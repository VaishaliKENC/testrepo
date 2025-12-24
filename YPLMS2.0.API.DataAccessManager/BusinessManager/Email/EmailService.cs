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
    public class EmailService
    {
        public CustomException _expCustom = null;
        public const string ADMIN_SITE_DEFAULT_LANGUAGE = "en-US";
        SmtpClient SMTPServer;
        MailMessage _mMailMessage;
        public string LogMessage = string.Empty;
        string _strClientLanguageId = string.Empty;
        string _strClientDateFormat = "{0:dd/MM/yyyy}";
        string _strClientId = string.Empty;
        public static string AllowedDomains = string.Empty;
        bool isEmailSent = false;
        private string _autoEmailEventID = string.Empty;
        public string AutoEmailEventID
        {
            get { return _autoEmailEventID; }
            set { if (this._autoEmailEventID != value) { _autoEmailEventID = value; } }
        }

        private StringBuilder strLog;
        private StringBuilder strInvalidEmails;

        private StringBuilder strMailAddresses;
        private StringBuilder strSystemUserGuId;


        private int emailsCount;

        /// <summary>
        /// Constructor of class EmailMessages, Check for SMTP
        /// </summary>
        public EmailService(string pClientId)
        {
            strLog = new StringBuilder();
            strInvalidEmails = new StringBuilder();
            strMailAddresses = new StringBuilder();
            strSystemUserGuId = new StringBuilder();
            Client entClient = new Client();
            ClientDAM mgrClient = new ClientDAM();
            emailsCount = 0;
            entClient.ID = pClientId;
            entClient = mgrClient.GetClientByID(entClient); // Execute(entClient, Client.Method.Get); comment by vinod 
            _strClientLanguageId = entClient.DefaultLanguageId;
            _strClientId = pClientId;
            SMTPServer = new SmtpClient();
            if (!string.IsNullOrEmpty(entClient.SMTPServerIP))
            {
                SMTPServer.Host = entClient.SMTPServerIP;
                //Add new code

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
        /// Get Email template from EmailDeliveryDashboard
        /// </summary>
        /// <param name="pEmailInfo">EmailDeliveryDashboard</param>
        /// <returns>EmailTemplate</returns>
        public EmailTemplate GetEmailTemplate(EmailDeliveryDashboard pEmailInfo)
        {
            EmailTemplate mailTemplate = null;
            if (pEmailInfo.EmailTemplate != null)
            {
                return pEmailInfo.EmailTemplate;
            }
            if (!string.IsNullOrEmpty(pEmailInfo.EmailTemplateID))
            {
                mailTemplate = new EmailTemplate();
                mailTemplate.ID = pEmailInfo.EmailTemplateID;
                mailTemplate.ClientId = _strClientId;
                EmailTemplateManager mgrEmailTemplate = new EmailTemplateManager();
                mailTemplate = mgrEmailTemplate.Execute(mailTemplate, EmailTemplate.Method.Get);
                return mailTemplate;
            }
            return mailTemplate;
        }

        /// <summary>
        /// Send mail to a Learner
        /// </summary>
        /// <param name="pLearner">Learner Object with specific language id</param>        
        /// <param name="pEmailInfo">EmailDeliveryDashboard Information</param>
        /// <param name="pstrAdditionalEmailBody">Additional body text to attached</param>
        /// <param name="pLanguageId">Specific language id</param>
        /// <param name="pListAttachments">List of attachements</param>
        /// <returns></returns>
        public bool SendPersonalizedMail(Learner pLearner, EmailDeliveryDashboard pEmailInfo, string pstrAdditionalEmailBody,
                                        List<Attachment> pListAttachments)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            EmailTemplate entEmailTemplate;
            string strFromDispalyName = string.Empty;

            try
            {

                entEmailTemplate = GetEmailTemplate(pEmailInfo);
                if (pEmailInfo.AddToDashboard)
                {
                    pEmailInfo.IsPersonalized = true;
                    return AddEmailToDashBoard(pEmailInfo, pListAttachments);
                }
                //Add To
                if (!ValidationManager.ValidateString(pLearner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                {
                    LogMessage += "Invalid Email: " + pLearner.EmailID.Trim() + " ";
                    return false;
                }
                if (entEmailTemplate != null)
                {
                    if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                    {
                        userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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

                            _mMailMessage.Subject = GetMailBodyFromTemplate(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, pLearner);
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

                        EmailTemplateLanguage emailtemplatelang;
                        if (string.IsNullOrEmpty(pLearner.DefaultLanguageId))
                            pLearner.DefaultLanguageId = "en-US";
                        //foreach (EmailTemplateLanguage emailtemplatelang in entEmailTemplate.EmailTemplateLanguage)
                        //{
                        emailtemplatelang = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        {
                            return entTempToFind.LanguageId == pLearner.DefaultLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved;
                        });
                        if (emailtemplatelang == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            {
                                return entTempToFind.LanguageId == ADMIN_SITE_DEFAULT_LANGUAGE && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved;
                            });
                        }
                        if (emailtemplatelang != null)
                        {

                            if (string.IsNullOrEmpty(strFromDispalyName))
                            {
                                if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                {
                                    strFromDispalyName = emailtemplatelang.DisplayName;
                                }
                            }

                            _mMailMessage.Subject += GetMailBodyFromTemplate(emailtemplatelang.EmailSubjectText, emailtemplatelang.LanguageId, pLearner) + " ";
                            _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, pLearner) + "<br/> ";
                            if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                            {
                                _mMailMessage.Body += pstrAdditionalEmailBody;
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
                if (string.IsNullOrEmpty(pEmailInfo.ToList))
                {
                    if (!string.IsNullOrEmpty(pLearner.FirstName) && !string.IsNullOrEmpty(pLearner.LastName))
                    {
                        _mMailMessage.To.Add(new MailAddress(pLearner.EmailID.Trim(), pLearner.FirstName + " " + pLearner.LastName));
                    }
                    else
                    {
                        _mMailMessage.To.Add(new MailAddress(pLearner.EmailID.Trim()));
                    }
                }
                else
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailReplyToId))
                {
                    _mMailMessage.ReplyTo = new MailAddress(entEmailTemplate.EmailReplyToId);
                }
                //Add From
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailFromId))
                {
                    if (string.IsNullOrEmpty(strFromDispalyName))
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                    }
                    else
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId, strFromDispalyName);
                    }
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
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

                    SaveEmailSentLog(entEmailTemplate.ID, _mMailMessage);

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
                            SaveEmailSentLog(entEmailTemplate.ID, _mMailMessage);
                        }
                        else
                        {

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
        /// <param name="strUserID">User ID</param>
        /// <param name="plistActivityAssignment">Activity Assignment List</param>
        /// <param name="plistAssignment">Assignment List</param>
        /// <returns></returns>
        public bool SendPersonalizedMail(List<Learner> pListLearner, EmailDeliveryDashboard pEmailInfo, string pstrAdditionalEmailBody,
            List<ActivityAssignment> plistActivityAssignment, List<Assignment> plistAssignment)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            StringBuilder strEmailAddress = new StringBuilder("");
            EmailTemplate entTemplate;
            StringBuilder _strLearnerId = new StringBuilder("");
            string strFromDispalyName = string.Empty;

            try
            {
                entTemplate = GetEmailTemplate(pEmailInfo);
                #region To Add in Dashboard
                if (pEmailInfo.AddToDashboard)
                {
                    if (pListLearner != null && pListLearner.Count > 0)
                    {
                        foreach (Learner learner in pListLearner)
                        {
                            if (!string.IsNullOrEmpty(learner.EmailID))
                            {

                                if (ValidationManager.ValidateString(learner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                                {
                                    string trimEmailID = learner.EmailID.Trim();
                                    strEmailAddress.Append(trimEmailID + ",");
                                    _strLearnerId.Append(learner.ID + ",");
                                }
                            }
                        }
                        // updated by Gitanjali 11.08.2010
                        if (string.IsNullOrEmpty(pEmailInfo.LearnerId))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(_strLearnerId)))
                            {
                                pEmailInfo.LearnerId = _strLearnerId.ToString().Substring(0, _strLearnerId.ToString().LastIndexOf(',')); ;
                            }
                        }

                        if (!string.IsNullOrEmpty(pEmailInfo.ToList))
                        {
                            pEmailInfo.ToList = pEmailInfo.ToList.Replace(";", ",");
                            pEmailInfo.ToList = pEmailInfo.ToList + "," + strEmailAddress.ToString();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(pEmailInfo.RuleId) || Convert.ToString(pEmailInfo.AssignmentTypeID) == "UI_ONETINMEASSIGNMENT")
                                pEmailInfo.ToList = strEmailAddress.ToString();
                        }
                    }
                    return AddEmailToDashBoard(pEmailInfo, null, plistActivityAssignment, plistAssignment);
                }
                #endregion

                #region Send Email If Learner list
                try
                {

                    if (pListLearner != null && pListLearner.Count > 0)
                    {
                        foreach (Learner learner in pListLearner)
                        {
                            _mMailMessage = new MailMessage();
                            strEmailAddress = new StringBuilder("");
                            strMailAddresses = new StringBuilder("");
                            emailsCount = 0;

                            #region Send Mail
                            //Add To
                            if (!ValidationManager.ValidateString(learner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                            {
                                LogMessage += "Invalid Email: " + learner.EmailID.Trim() + " ";
                                return false;
                            }
                            if (entTemplate != null)
                            {
                                if (!string.IsNullOrEmpty(learner.DefaultLanguageId))
                                {
                                    userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                    { return entTempToFind.LanguageId == learner.DefaultLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                    if (userMailTemplate == null)
                                    {
                                        userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                        { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                    }
                                    if (userMailTemplate == null)
                                    {
                                        userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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

                                        _mMailMessage.IsBodyHtml = true;
                                        if (plistActivityAssignment.Count > 0)
                                        {
                                            for (int i = 0; i < plistActivityAssignment.Count; i++)
                                            {
                                                if (plistActivityAssignment[i].AssignmentDateSet == DateTime.MinValue)
                                                {
                                                    ActivityAssignment entActAss = new ActivityAssignment();
                                                    ActivityAssignmentAdaptor entActAssMngr = new ActivityAssignmentAdaptor();
                                                    entActAss.ID = plistActivityAssignment[i].ID;
                                                    entActAss.ClientId = learner.ClientId;
                                                    entActAss.UserID = learner.ID;

                                                    entActAss = entActAssMngr.GetActivityAssignmentByID(entActAss); //Execute(entActAss, ActivityAssignment.Method.Get);
                                                    plistActivityAssignment.RemoveAt(i);
                                                    plistActivityAssignment.Insert(i, entActAss);
                                                }
                                            }
                                        }

                                        _mMailMessage.Subject = GetMailBodyFromTemplate(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, learner, plistActivityAssignment, plistAssignment);
                                        _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, learner, plistActivityAssignment, plistAssignment);

                                        if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                        {
                                            _mMailMessage.Body += "<br/>" + pstrAdditionalEmailBody;
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
                                    foreach (EmailTemplateLanguage emailtemplatelang in entTemplate.EmailTemplateLanguage)
                                    {
                                        if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                                        {
                                            _mMailMessage = new MailMessage();
                                            _mMailMessage.IsBodyHtml = true;

                                            if (string.IsNullOrEmpty(strFromDispalyName))
                                            {
                                                if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                                {
                                                    strFromDispalyName = emailtemplatelang.DisplayName;
                                                }
                                            }

                                            _mMailMessage.Subject += GetMailBodyFromTemplate(emailtemplatelang.EmailSubjectText, emailtemplatelang.LanguageId, learner, plistActivityAssignment, plistAssignment) + " ";
                                            _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, learner, plistActivityAssignment, plistAssignment) + "<br/> ";

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
                            //Add To
                            if (string.IsNullOrEmpty(pEmailInfo.ToList))
                            {
                                if (!string.IsNullOrEmpty(learner.FirstName) && !string.IsNullOrEmpty(learner.LastName))
                                {
                                    _mMailMessage.To.Add(new MailAddress(learner.EmailID.Trim(), learner.FirstName + " " + learner.LastName));
                                }
                                else
                                {
                                    _mMailMessage.To.Add(new MailAddress(learner.EmailID.Trim()));
                                }
                            }
                            else
                            {
                                foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                            if (!string.IsNullOrEmpty(entTemplate.EmailReplyToId))
                            {
                                _mMailMessage.ReplyTo = new MailAddress(entTemplate.EmailReplyToId);
                            }
                            //Add From
                            if (!string.IsNullOrEmpty(entTemplate.EmailFromId))
                            {
                                //if (string.IsNullOrEmpty(entTemplate.DisplayName))
                                //{
                                //    _mMailMessage.From = new MailAddress(entTemplate.EmailFromId);
                                //}
                                //else
                                //{
                                //    _mMailMessage.From = new MailAddress(entTemplate.EmailFromId, entTemplate.DisplayName);
                                //}
                                if (string.IsNullOrEmpty(strFromDispalyName))
                                {
                                    _mMailMessage.From = new MailAddress(entTemplate.EmailFromId);
                                }
                                else
                                {
                                    _mMailMessage.From = new MailAddress(entTemplate.EmailFromId, strFromDispalyName);
                                }
                            }
                            else
                            {
                                throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                            }
                            //Add CC
                            if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                            {
                                foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                            if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                            {
                                foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
                                {
                                    _mMailMessage.Bcc.Add(address);
                                }
                            }


                            #endregion


                            try
                            {

                                //SendEmail(_mMailMessage);
                                strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                                strLog = new StringBuilder();
                                strLog.Append(SendEmailWithReturnLog(_mMailMessage));
                                SaveEmailSentLog(entTemplate.ID, _mMailMessage);
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
                                        SaveEmailSentLog(entTemplate.ID, _mMailMessage);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed to deliver message to {0}",
                                            ex.InnerExceptions[i].FailedRecipient);
                                    }
                                }
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
                    //if (!string.IsNullOrEmpty(pEmailInfo.ToList))
                    //{
                    if (!string.IsNullOrEmpty(pEmailInfo.ToList) && pListLearner.Count == 0)
                    {
                        #region Send Mail

                        if (entTemplate != null)
                        {
                            if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                            {
                                userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                if (userMailTemplate == null)
                                {
                                    userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                    { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                }
                                if (userMailTemplate == null)
                                {
                                    userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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

                                    _mMailMessage.Subject = GetMailBodyFromTemplate(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, null, plistActivityAssignment, plistAssignment);
                                    _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, null, plistActivityAssignment, plistAssignment);
                                    if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                    {
                                        _mMailMessage.Body += "<br/>" + pstrAdditionalEmailBody;
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
                                foreach (EmailTemplateLanguage emailtemplatelang in entTemplate.EmailTemplateLanguage)
                                {
                                    if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                                    {
                                        _mMailMessage = new MailMessage();
                                        _mMailMessage.IsBodyHtml = true;
                                        if (string.IsNullOrEmpty(strFromDispalyName))
                                        {
                                            if (!string.IsNullOrEmpty(userMailTemplate.DisplayName))
                                            {
                                                strFromDispalyName = emailtemplatelang.DisplayName;
                                            }
                                        }
                                        _mMailMessage.Subject += GetMailBodyFromTemplate(emailtemplatelang.EmailSubjectText, emailtemplatelang.LanguageId, null, plistActivityAssignment, plistAssignment) + " ";
                                        _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, null, plistActivityAssignment, plistAssignment) + "<br/> ";
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
                        //Add To                   
                        foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                        if (!string.IsNullOrEmpty(entTemplate.EmailReplyToId))
                        {
                            _mMailMessage.ReplyTo = new MailAddress(entTemplate.EmailReplyToId);
                        }
                        //Add From
                        if (!string.IsNullOrEmpty(entTemplate.EmailFromId))
                        {
                            if (string.IsNullOrEmpty(strFromDispalyName))
                            {
                                _mMailMessage.From = new MailAddress(entTemplate.EmailFromId);
                            }
                            else
                            {
                                _mMailMessage.From = new MailAddress(entTemplate.EmailFromId, strFromDispalyName);
                            }
                        }
                        else
                        {
                            throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                        }
                        //Add CC
                        if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                        {
                            foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                        if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                        {
                            foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
                            {
                                _mMailMessage.Bcc.Add(address);
                            }
                        }
                        try
                        {

                            //SendEmail(_mMailMessage);
                            strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                            strLog = new StringBuilder();
                            strLog.Append(SendEmailWithReturnLog(_mMailMessage));
                            SaveEmailSentLog(entTemplate.ID, _mMailMessage);
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
                                    SaveEmailSentLog(entTemplate.ID, _mMailMessage);
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

        public bool SendPersonalizedMailUnlockAssessment(List<Learner> pListLearner, EmailDeliveryDashboard pEmailInfo, string pstrAdditionalEmailBody,
        List<ActivityAssignment> plistActivityAssignment, List<Assignment> plistAssignment)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            StringBuilder strEmailAddress = new StringBuilder("");
            EmailTemplate entTemplate;
            StringBuilder _strLearnerId = new StringBuilder("");
            string strFromDispalyName = string.Empty;

            List<Attachment> plistAttachments = new List<Attachment>();
            List<Attachment> AssignmentDatelistAttachments = new List<Attachment>();
            List<Attachment> AssignmentDueDatelistAttachments = new List<Attachment>();
            try
            {
                entTemplate = GetEmailTemplate(pEmailInfo);
                #region To Add in Dashboard
                if (pEmailInfo.AddToDashboard)
                {
                    if (pListLearner != null && pListLearner.Count > 0)
                    {
                        foreach (Learner learner in pListLearner)
                        {
                            if (!string.IsNullOrEmpty(learner.EmailID))
                            {

                                if (ValidationManager.ValidateString(learner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                                {
                                    string trimEmailID = learner.EmailID.Trim();
                                    strEmailAddress.Append(trimEmailID + ",");
                                    _strLearnerId.Append(learner.ID + ",");
                                }
                            }
                        }
                        // updated by Gitanjali 11.08.2010
                        if (string.IsNullOrEmpty(pEmailInfo.LearnerId))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(_strLearnerId)))
                            {
                                pEmailInfo.LearnerId = _strLearnerId.ToString().Substring(0, _strLearnerId.ToString().LastIndexOf(',')); ;
                            }
                        }

                        if (!string.IsNullOrEmpty(pEmailInfo.ToList))
                        {
                            pEmailInfo.ToList = pEmailInfo.ToList.Replace(";", ",");
                            pEmailInfo.ToList = pEmailInfo.ToList + "," + strEmailAddress.ToString();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(pEmailInfo.RuleId) || Convert.ToString(pEmailInfo.AssignmentTypeID) == "UI_ONETINMEASSIGNMENT")
                                pEmailInfo.ToList = strEmailAddress.ToString();
                        }
                        ///Add new
                        ///
                        //string attachmentAssignmentFilename = string.Empty;
                        //string attachmentDuedateFilename = string.Empty;

                        //if (plistActivityAssignment.Count > 0)
                        //{
                        //    int i = 1;
                        //    if (plistActivityAssignment[i - 1].AssignmentDateSet.ToString() != "01/01/0001 00:00:00") //&& Count == 0
                        //    {
                        //        attachmentAssignmentFilename = FileHandler.CancelOutlookAssignment(i, plistActivityAssignment, pListLearner[0].ClientId, _mMailMessage.Subject);
                        //        //AssignmentDatelistAttachments=GetAttachments(attachmentAssignmentFilename);
                        //        plistAttachments.Add(new Attachment(attachmentAssignmentFilename));

                        //    }
                        //    if (plistActivityAssignment[i - 1].DueDateSet.ToString() != "01/01/0001 00:00:00")// && Count == 0
                        //    {
                        //        attachmentDuedateFilename = FileHandler.CancelOutlookDueDate(i, plistActivityAssignment, pListLearner[0].ClientId, _mMailMessage.Subject);
                        //        //AssignmentDueDatelistAttachments = GetAttachments(attachmentDuedateFilename);
                        //        plistAttachments.Add(new Attachment(attachmentDuedateFilename));
                        //    }
                        //    //Count++;

                        //    //_mMailMessage.IsBodyHtml = true;
                        //    //if (!string.IsNullOrEmpty(attachmentAssignmentFilename))
                        //    //{

                        //    //    Attachment attachment = new Attachment(attachmentAssignmentFilename, MediaTypeNames.Application.Octet);
                        //    //    ContentDisposition disposition = attachment.ContentDisposition;
                        //    //    disposition.CreationDate = File.GetCreationTime(attachmentAssignmentFilename);
                        //    //    disposition.ModificationDate = File.GetLastWriteTime(attachmentAssignmentFilename);
                        //    //    disposition.ReadDate = File.GetLastAccessTime(attachmentAssignmentFilename);
                        //    //    disposition.FileName = Path.GetFileName(attachmentAssignmentFilename);
                        //    //    disposition.Size = new FileInfo(attachmentAssignmentFilename).Length;
                        //    //    disposition.DispositionType = DispositionTypeNames.Attachment;
                        //    //    _mMailMessage.Attachments.Add(attachment);
                        //    //}
                        //    //if (!string.IsNullOrEmpty(attachmentDuedateFilename))
                        //    //{

                        //    //    Attachment attachment = new Attachment(attachmentDuedateFilename, MediaTypeNames.Application.Octet);
                        //    //    ContentDisposition disposition = attachment.ContentDisposition;
                        //    //    disposition.CreationDate = File.GetCreationTime(attachmentDuedateFilename);
                        //    //    disposition.ModificationDate = File.GetLastWriteTime(attachmentDuedateFilename);
                        //    //    disposition.ReadDate = File.GetLastAccessTime(attachmentDuedateFilename);
                        //    //    disposition.FileName = Path.GetFileName(attachmentDuedateFilename);
                        //    //    disposition.Size = new FileInfo(attachmentDuedateFilename).Length;
                        //    //    disposition.DispositionType = DispositionTypeNames.Attachment;
                        //    //    _mMailMessage.Attachments.Add(attachment);
                        //    //}
                        //}

                    }
                    return AddEmailToDashBoardOutlook(pEmailInfo, plistAttachments, plistActivityAssignment, plistAssignment);
                }
                #endregion

                #region Send Email If Learner list
                try
                {

                    if (pListLearner != null && pListLearner.Count > 0)
                    {
                        foreach (Learner learner in pListLearner)
                        {
                            _mMailMessage = new MailMessage();
                            strEmailAddress = new StringBuilder("");
                            strMailAddresses = new StringBuilder("");
                            emailsCount = 0;

                            #region Send Mail
                            //Add To
                            if (!ValidationManager.ValidateString(learner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                            {
                                LogMessage += "Invalid Email: " + learner.EmailID.Trim() + " ";
                                return false;
                            }
                            if (entTemplate != null)
                            {
                                if (!string.IsNullOrEmpty(learner.DefaultLanguageId))
                                {
                                    userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                    { return entTempToFind.LanguageId == learner.DefaultLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                    if (userMailTemplate == null)
                                    {
                                        userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                        { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                    }
                                    if (userMailTemplate == null)
                                    {
                                        userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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

                                        _mMailMessage.IsBodyHtml = true;
                                        if (plistActivityAssignment.Count > 0)
                                        {
                                            for (int i = 0; i < plistActivityAssignment.Count; i++)
                                            {
                                                if (plistActivityAssignment[i].AssignmentDateSet == DateTime.MinValue)
                                                {
                                                    ActivityAssignment entActAss = new ActivityAssignment();
                                                    ActivityAssignmentAdaptor entActAssMngr = new ActivityAssignmentAdaptor();
                                                    entActAss.ID = plistActivityAssignment[i].ID;
                                                    entActAss.ClientId = learner.ClientId;
                                                    entActAss.UserID = learner.ID;

                                                    entActAss = entActAssMngr.GetActivityAssignmentByID(entActAss); // (entActAss, ActivityAssignment.Method.Get); comment by vinod 
                                                    plistActivityAssignment.RemoveAt(i);
                                                    plistActivityAssignment.Insert(i, entActAss);
                                                }
                                            }
                                        }

                                        _mMailMessage.Subject = GetMailBodyFromTemplate(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, learner, plistActivityAssignment, plistAssignment);
                                        _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, learner, plistActivityAssignment, plistAssignment);

                                        if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                        {
                                            _mMailMessage.Body += "<br/>" + pstrAdditionalEmailBody;
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
                                    foreach (EmailTemplateLanguage emailtemplatelang in entTemplate.EmailTemplateLanguage)
                                    {
                                        if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                                        {
                                            _mMailMessage = new MailMessage();
                                            _mMailMessage.IsBodyHtml = true;

                                            if (string.IsNullOrEmpty(strFromDispalyName))
                                            {
                                                if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                                {
                                                    strFromDispalyName = emailtemplatelang.DisplayName;
                                                }
                                            }

                                            _mMailMessage.Subject += GetMailBodyFromTemplate(emailtemplatelang.EmailSubjectText, emailtemplatelang.LanguageId, learner, plistActivityAssignment, plistAssignment) + " ";
                                            _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, learner, plistActivityAssignment, plistAssignment) + "<br/> ";

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
                            //Add To
                            if (string.IsNullOrEmpty(pEmailInfo.ToList))
                            {
                                if (!string.IsNullOrEmpty(learner.FirstName) && !string.IsNullOrEmpty(learner.LastName))
                                {
                                    _mMailMessage.To.Add(new MailAddress(learner.EmailID.Trim(), learner.FirstName + " " + learner.LastName));
                                }
                                else
                                {
                                    _mMailMessage.To.Add(new MailAddress(learner.EmailID.Trim()));
                                }
                            }
                            else
                            {
                                foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                            if (!string.IsNullOrEmpty(entTemplate.EmailReplyToId))
                            {
                                _mMailMessage.ReplyTo = new MailAddress(entTemplate.EmailReplyToId);
                            }
                            //Add From
                            if (!string.IsNullOrEmpty(entTemplate.EmailFromId))
                            {
                                if (string.IsNullOrEmpty(strFromDispalyName))
                                {
                                    _mMailMessage.From = new MailAddress(entTemplate.EmailFromId);
                                }
                                else
                                {
                                    _mMailMessage.From = new MailAddress(entTemplate.EmailFromId, strFromDispalyName);
                                }
                            }
                            else
                            {
                                throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                            }
                            //Add CC
                            if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                            {
                                foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                            if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                            {
                                foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
                                {
                                    _mMailMessage.Bcc.Add(address);
                                }
                            }
                            #endregion

                            try
                            {
                                strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                                strLog = new StringBuilder();
                                strLog.Append(SendEmailWithReturnLog(_mMailMessage));
                                SaveEmailSentLog(entTemplate.ID, _mMailMessage);
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
                                        SaveEmailSentLog(entTemplate.ID, _mMailMessage);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed to deliver message to {0}",
                                            ex.InnerExceptions[i].FailedRecipient);
                                    }
                                }
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

                    if (!string.IsNullOrEmpty(pEmailInfo.ToList) && pListLearner.Count == 0)
                    {
                        #region Send Mail

                        if (entTemplate != null)
                        {
                            if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                            {
                                userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                if (userMailTemplate == null)
                                {
                                    userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                    { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                }
                                if (userMailTemplate == null)
                                {
                                    userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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

                                    _mMailMessage.Subject = GetMailBodyFromTemplate(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, null, plistActivityAssignment, plistAssignment);
                                    _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, null, plistActivityAssignment, plistAssignment);
                                    if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                    {
                                        _mMailMessage.Body += "<br/>" + pstrAdditionalEmailBody;
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
                                foreach (EmailTemplateLanguage emailtemplatelang in entTemplate.EmailTemplateLanguage)
                                {
                                    if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                                    {
                                        _mMailMessage = new MailMessage();
                                        _mMailMessage.IsBodyHtml = true;
                                        if (string.IsNullOrEmpty(strFromDispalyName))
                                        {
                                            if (!string.IsNullOrEmpty(userMailTemplate.DisplayName))
                                            {
                                                strFromDispalyName = emailtemplatelang.DisplayName;
                                            }
                                        }
                                        _mMailMessage.Subject += GetMailBodyFromTemplate(emailtemplatelang.EmailSubjectText, emailtemplatelang.LanguageId, null, plistActivityAssignment, plistAssignment) + " ";
                                        _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, null, plistActivityAssignment, plistAssignment) + "<br/> ";
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
                        //Add To                   
                        foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                        if (!string.IsNullOrEmpty(entTemplate.EmailReplyToId))
                        {
                            _mMailMessage.ReplyTo = new MailAddress(entTemplate.EmailReplyToId);
                        }
                        //Add From
                        if (!string.IsNullOrEmpty(entTemplate.EmailFromId))
                        {
                            if (string.IsNullOrEmpty(strFromDispalyName))
                            {
                                _mMailMessage.From = new MailAddress(entTemplate.EmailFromId);
                            }
                            else
                            {
                                _mMailMessage.From = new MailAddress(entTemplate.EmailFromId, strFromDispalyName);
                            }
                        }
                        else
                        {
                            throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                        }
                        //Add CC
                        if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                        {
                            foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                        if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                        {
                            foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
                            {
                                _mMailMessage.Bcc.Add(address);
                            }
                        }

                        //Add New Code
                        string attachmentAssignmentFilename = string.Empty;
                        string attachmentDuedateFilename = string.Empty;

                        if (plistActivityAssignment.Count > 0)
                        {
                            int i = 1;
                            if (plistActivityAssignment[i - 1].AssignmentDateSet.ToString() != "01/01/0001 00:00:00") //&& Count == 0
                                attachmentAssignmentFilename = FileHandler.CancelOutlookAssignment(i, plistActivityAssignment, pListLearner[0].ClientId, _mMailMessage.Subject);


                            if (plistActivityAssignment[i - 1].DueDateSet.ToString() != "01/01/0001 00:00:00")// && Count == 0
                                attachmentDuedateFilename = FileHandler.CancelOutlookDueDate(i, plistActivityAssignment, pListLearner[0].ClientId, _mMailMessage.Subject);
                            //Count++;

                            _mMailMessage.IsBodyHtml = true;
                            if (!string.IsNullOrEmpty(attachmentAssignmentFilename))
                            {

                                Attachment attachment = new Attachment(attachmentAssignmentFilename, MediaTypeNames.Application.Octet);
                                ContentDisposition disposition = attachment.ContentDisposition;
                                disposition.CreationDate = File.GetCreationTime(attachmentAssignmentFilename);
                                disposition.ModificationDate = File.GetLastWriteTime(attachmentAssignmentFilename);
                                disposition.ReadDate = File.GetLastAccessTime(attachmentAssignmentFilename);
                                disposition.FileName = Path.GetFileName(attachmentAssignmentFilename);
                                disposition.Size = new FileInfo(attachmentAssignmentFilename).Length;
                                disposition.DispositionType = DispositionTypeNames.Attachment;
                                _mMailMessage.Attachments.Add(attachment);
                            }
                            if (!string.IsNullOrEmpty(attachmentDuedateFilename))
                            {

                                Attachment attachment = new Attachment(attachmentDuedateFilename, MediaTypeNames.Application.Octet);
                                ContentDisposition disposition = attachment.ContentDisposition;
                                disposition.CreationDate = File.GetCreationTime(attachmentDuedateFilename);
                                disposition.ModificationDate = File.GetLastWriteTime(attachmentDuedateFilename);
                                disposition.ReadDate = File.GetLastAccessTime(attachmentDuedateFilename);
                                disposition.FileName = Path.GetFileName(attachmentDuedateFilename);
                                disposition.Size = new FileInfo(attachmentDuedateFilename).Length;
                                disposition.DispositionType = DispositionTypeNames.Attachment;
                                _mMailMessage.Attachments.Add(attachment);
                            }
                        }

                        try
                        {
                            strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                            strLog = new StringBuilder();
                            strLog.Append(SendEmailWithReturnLog(_mMailMessage));
                            SaveEmailSentLog(entTemplate.ID, _mMailMessage);
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
                                    SaveEmailSentLog(entTemplate.ID, _mMailMessage);
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
        public bool SendPersonalizedMailOutlook(List<Learner> pListLearner, EmailDeliveryDashboard pEmailInfo, string pstrAdditionalEmailBody,
            List<ActivityAssignment> plistActivityAssignment, List<Assignment> plistAssignment)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            StringBuilder strEmailAddress = new StringBuilder("");
            EmailTemplate entTemplate;
            StringBuilder _strLearnerId = new StringBuilder("");
            string strFromDispalyName = string.Empty;

            List<Attachment> plistAttachments = new List<Attachment>();
            List<Attachment> AssignmentDatelistAttachments = new List<Attachment>();
            List<Attachment> AssignmentDueDatelistAttachments = new List<Attachment>();
            try
            {
                entTemplate = GetEmailTemplate(pEmailInfo);
                #region To Add in Dashboard
                if (pEmailInfo.AddToDashboard)
                {
                    if (pListLearner != null && pListLearner.Count > 0)
                    {
                        foreach (Learner learner in pListLearner)
                        {
                            if (!string.IsNullOrEmpty(learner.EmailID))
                            {

                                if (ValidationManager.ValidateString(learner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                                {
                                    string trimEmailID = learner.EmailID.Trim();
                                    strEmailAddress.Append(trimEmailID + ",");
                                    _strLearnerId.Append(learner.ID + ",");
                                }
                            }
                        }
                        // updated by Gitanjali 11.08.2010
                        if (string.IsNullOrEmpty(pEmailInfo.LearnerId))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(_strLearnerId)))
                            {
                                pEmailInfo.LearnerId = _strLearnerId.ToString().Substring(0, _strLearnerId.ToString().LastIndexOf(',')); ;
                            }
                        }

                        if (!string.IsNullOrEmpty(pEmailInfo.ToList))
                        {
                            pEmailInfo.ToList = pEmailInfo.ToList.Replace(";", ",");
                            pEmailInfo.ToList = pEmailInfo.ToList + "," + strEmailAddress.ToString();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(pEmailInfo.RuleId) || Convert.ToString(pEmailInfo.AssignmentTypeID) == "UI_ONETINMEASSIGNMENT")
                                pEmailInfo.ToList = strEmailAddress.ToString();
                        }


                        ///Add new
                        ///
                        string attachmentAssignmentFilename = string.Empty;
                        string attachmentDuedateFilename = string.Empty;

                        if (plistActivityAssignment.Count > 0)
                        {
                            int i = 1;
                            if (plistActivityAssignment[i - 1].AssignmentDateSet.ToString() != "01/01/0001 00:00:00") //&& Count == 0
                            {
                                attachmentAssignmentFilename = FileHandler.CancelOutlookAssignment(i, plistActivityAssignment, pListLearner[0].ClientId, _mMailMessage.Subject);
                                //AssignmentDatelistAttachments=GetAttachments(attachmentAssignmentFilename);
                                plistAttachments.Add(new Attachment(attachmentAssignmentFilename));

                            }
                            if (plistActivityAssignment[i - 1].DueDateSet.ToString() != "01/01/0001 00:00:00")// && Count == 0
                            {
                                attachmentDuedateFilename = FileHandler.CancelOutlookDueDate(i, plistActivityAssignment, pListLearner[0].ClientId, _mMailMessage.Subject);
                                //AssignmentDueDatelistAttachments = GetAttachments(attachmentDuedateFilename);
                                plistAttachments.Add(new Attachment(attachmentDuedateFilename));
                            }
                            //Count++;

                            //_mMailMessage.IsBodyHtml = true;
                            //if (!string.IsNullOrEmpty(attachmentAssignmentFilename))
                            //{

                            //    Attachment attachment = new Attachment(attachmentAssignmentFilename, MediaTypeNames.Application.Octet);
                            //    ContentDisposition disposition = attachment.ContentDisposition;
                            //    disposition.CreationDate = File.GetCreationTime(attachmentAssignmentFilename);
                            //    disposition.ModificationDate = File.GetLastWriteTime(attachmentAssignmentFilename);
                            //    disposition.ReadDate = File.GetLastAccessTime(attachmentAssignmentFilename);
                            //    disposition.FileName = Path.GetFileName(attachmentAssignmentFilename);
                            //    disposition.Size = new FileInfo(attachmentAssignmentFilename).Length;
                            //    disposition.DispositionType = DispositionTypeNames.Attachment;
                            //    _mMailMessage.Attachments.Add(attachment);
                            //}
                            //if (!string.IsNullOrEmpty(attachmentDuedateFilename))
                            //{

                            //    Attachment attachment = new Attachment(attachmentDuedateFilename, MediaTypeNames.Application.Octet);
                            //    ContentDisposition disposition = attachment.ContentDisposition;
                            //    disposition.CreationDate = File.GetCreationTime(attachmentDuedateFilename);
                            //    disposition.ModificationDate = File.GetLastWriteTime(attachmentDuedateFilename);
                            //    disposition.ReadDate = File.GetLastAccessTime(attachmentDuedateFilename);
                            //    disposition.FileName = Path.GetFileName(attachmentDuedateFilename);
                            //    disposition.Size = new FileInfo(attachmentDuedateFilename).Length;
                            //    disposition.DispositionType = DispositionTypeNames.Attachment;
                            //    _mMailMessage.Attachments.Add(attachment);
                            //}
                        }

                    }
                    return AddEmailToDashBoardOutlook(pEmailInfo, plistAttachments, plistActivityAssignment, plistAssignment);
                }
                #endregion

                #region Send Email If Learner list
                try
                {

                    if (pListLearner != null && pListLearner.Count > 0)
                    {
                        foreach (Learner learner in pListLearner)
                        {
                            _mMailMessage = new MailMessage();
                            strEmailAddress = new StringBuilder("");
                            strMailAddresses = new StringBuilder("");
                            emailsCount = 0;

                            #region Send Mail
                            //Add To
                            if (!ValidationManager.ValidateString(learner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                            {
                                LogMessage += "Invalid Email: " + learner.EmailID.Trim() + " ";
                                return false;
                            }
                            if (entTemplate != null)
                            {
                                if (!string.IsNullOrEmpty(learner.DefaultLanguageId))
                                {
                                    userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                    { return entTempToFind.LanguageId == learner.DefaultLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                    if (userMailTemplate == null)
                                    {
                                        userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                        { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                    }
                                    if (userMailTemplate == null)
                                    {
                                        userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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

                                        _mMailMessage.IsBodyHtml = true;
                                        if (plistActivityAssignment.Count > 0)
                                        {
                                            for (int i = 0; i < plistActivityAssignment.Count; i++)
                                            {
                                                if (plistActivityAssignment[i].AssignmentDateSet == DateTime.MinValue)
                                                {
                                                    ActivityAssignment entActAss = new ActivityAssignment();
                                                    ActivityAssignmentAdaptor entActAssMngr = new ActivityAssignmentAdaptor();
                                                    entActAss.ID = plistActivityAssignment[i].ID;
                                                    entActAss.ClientId = learner.ClientId;
                                                    entActAss.UserID = learner.ID;

                                                    entActAss = entActAssMngr.GetActivityAssignmentByID(entActAss); //Execute(entActAss, ActivityAssignment.Method.Get); comment by vinod
                                                    plistActivityAssignment.RemoveAt(i);
                                                    plistActivityAssignment.Insert(i, entActAss);
                                                }
                                            }
                                        }

                                        _mMailMessage.Subject = GetMailBodyFromTemplate(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, learner, plistActivityAssignment, plistAssignment);
                                        _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, learner, plistActivityAssignment, plistAssignment);

                                        if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                        {
                                            _mMailMessage.Body += "<br/>" + pstrAdditionalEmailBody;
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
                                    foreach (EmailTemplateLanguage emailtemplatelang in entTemplate.EmailTemplateLanguage)
                                    {
                                        if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                                        {
                                            _mMailMessage = new MailMessage();
                                            _mMailMessage.IsBodyHtml = true;

                                            if (string.IsNullOrEmpty(strFromDispalyName))
                                            {
                                                if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                                {
                                                    strFromDispalyName = emailtemplatelang.DisplayName;
                                                }
                                            }

                                            _mMailMessage.Subject += GetMailBodyFromTemplate(emailtemplatelang.EmailSubjectText, emailtemplatelang.LanguageId, learner, plistActivityAssignment, plistAssignment) + " ";
                                            _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, learner, plistActivityAssignment, plistAssignment) + "<br/> ";

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
                            //Add To
                            if (string.IsNullOrEmpty(pEmailInfo.ToList))
                            {
                                if (!string.IsNullOrEmpty(learner.FirstName) && !string.IsNullOrEmpty(learner.LastName))
                                {
                                    _mMailMessage.To.Add(new MailAddress(learner.EmailID.Trim(), learner.FirstName + " " + learner.LastName));
                                }
                                else
                                {
                                    _mMailMessage.To.Add(new MailAddress(learner.EmailID.Trim()));
                                }
                            }
                            else
                            {
                                foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                            if (!string.IsNullOrEmpty(entTemplate.EmailReplyToId))
                            {
                                _mMailMessage.ReplyTo = new MailAddress(entTemplate.EmailReplyToId);
                            }
                            //Add From
                            if (!string.IsNullOrEmpty(entTemplate.EmailFromId))
                            {
                                if (string.IsNullOrEmpty(strFromDispalyName))
                                {
                                    _mMailMessage.From = new MailAddress(entTemplate.EmailFromId);
                                }
                                else
                                {
                                    _mMailMessage.From = new MailAddress(entTemplate.EmailFromId, strFromDispalyName);
                                }
                            }
                            else
                            {
                                throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                            }
                            //Add CC
                            if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                            {
                                foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                            if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                            {
                                foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
                                {
                                    _mMailMessage.Bcc.Add(address);
                                }
                            }
                            #endregion

                            try
                            {
                                strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                                strLog = new StringBuilder();
                                strLog.Append(SendEmailWithReturnLog(_mMailMessage));
                                SaveEmailSentLog(entTemplate.ID, _mMailMessage);
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
                                        SaveEmailSentLog(entTemplate.ID, _mMailMessage);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed to deliver message to {0}",
                                            ex.InnerExceptions[i].FailedRecipient);
                                    }
                                }
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

                    if (!string.IsNullOrEmpty(pEmailInfo.ToList) && pListLearner.Count == 0)
                    {
                        #region Send Mail

                        if (entTemplate != null)
                        {
                            if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                            {
                                userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                if (userMailTemplate == null)
                                {
                                    userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                    { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                }
                                if (userMailTemplate == null)
                                {
                                    userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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

                                    _mMailMessage.Subject = GetMailBodyFromTemplate(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, null, plistActivityAssignment, plistAssignment);
                                    _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, null, plistActivityAssignment, plistAssignment);
                                    if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                    {
                                        _mMailMessage.Body += "<br/>" + pstrAdditionalEmailBody;
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
                                foreach (EmailTemplateLanguage emailtemplatelang in entTemplate.EmailTemplateLanguage)
                                {
                                    if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                                    {
                                        _mMailMessage = new MailMessage();
                                        _mMailMessage.IsBodyHtml = true;
                                        if (string.IsNullOrEmpty(strFromDispalyName))
                                        {
                                            if (!string.IsNullOrEmpty(userMailTemplate.DisplayName))
                                            {
                                                strFromDispalyName = emailtemplatelang.DisplayName;
                                            }
                                        }
                                        _mMailMessage.Subject += GetMailBodyFromTemplate(emailtemplatelang.EmailSubjectText, emailtemplatelang.LanguageId, null, plistActivityAssignment, plistAssignment) + " ";
                                        _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, null, plistActivityAssignment, plistAssignment) + "<br/> ";
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
                        //Add To                   
                        foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                        if (!string.IsNullOrEmpty(entTemplate.EmailReplyToId))
                        {
                            _mMailMessage.ReplyTo = new MailAddress(entTemplate.EmailReplyToId);
                        }
                        //Add From
                        if (!string.IsNullOrEmpty(entTemplate.EmailFromId))
                        {
                            if (string.IsNullOrEmpty(strFromDispalyName))
                            {
                                _mMailMessage.From = new MailAddress(entTemplate.EmailFromId);
                            }
                            else
                            {
                                _mMailMessage.From = new MailAddress(entTemplate.EmailFromId, strFromDispalyName);
                            }
                        }
                        else
                        {
                            throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                        }
                        //Add CC
                        if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                        {
                            foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                        if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                        {
                            foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
                            {
                                _mMailMessage.Bcc.Add(address);
                            }
                        }

                        //Add New Code
                        string attachmentAssignmentFilename = string.Empty;
                        string attachmentDuedateFilename = string.Empty;

                        if (plistActivityAssignment.Count > 0)
                        {
                            int i = 1;
                            if (plistActivityAssignment[i - 1].AssignmentDateSet.ToString() != "01/01/0001 00:00:00") //&& Count == 0
                                attachmentAssignmentFilename = FileHandler.CancelOutlookAssignment(i, plistActivityAssignment, pListLearner[0].ClientId, _mMailMessage.Subject);


                            if (plistActivityAssignment[i - 1].DueDateSet.ToString() != "01/01/0001 00:00:00")// && Count == 0
                                attachmentDuedateFilename = FileHandler.CancelOutlookDueDate(i, plistActivityAssignment, pListLearner[0].ClientId, _mMailMessage.Subject);
                            //Count++;

                            _mMailMessage.IsBodyHtml = true;
                            if (!string.IsNullOrEmpty(attachmentAssignmentFilename))
                            {

                                Attachment attachment = new Attachment(attachmentAssignmentFilename, MediaTypeNames.Application.Octet);
                                ContentDisposition disposition = attachment.ContentDisposition;
                                disposition.CreationDate = File.GetCreationTime(attachmentAssignmentFilename);
                                disposition.ModificationDate = File.GetLastWriteTime(attachmentAssignmentFilename);
                                disposition.ReadDate = File.GetLastAccessTime(attachmentAssignmentFilename);
                                disposition.FileName = Path.GetFileName(attachmentAssignmentFilename);
                                disposition.Size = new FileInfo(attachmentAssignmentFilename).Length;
                                disposition.DispositionType = DispositionTypeNames.Attachment;
                                _mMailMessage.Attachments.Add(attachment);
                            }
                            if (!string.IsNullOrEmpty(attachmentDuedateFilename))
                            {

                                Attachment attachment = new Attachment(attachmentDuedateFilename, MediaTypeNames.Application.Octet);
                                ContentDisposition disposition = attachment.ContentDisposition;
                                disposition.CreationDate = File.GetCreationTime(attachmentDuedateFilename);
                                disposition.ModificationDate = File.GetLastWriteTime(attachmentDuedateFilename);
                                disposition.ReadDate = File.GetLastAccessTime(attachmentDuedateFilename);
                                disposition.FileName = Path.GetFileName(attachmentDuedateFilename);
                                disposition.Size = new FileInfo(attachmentDuedateFilename).Length;
                                disposition.DispositionType = DispositionTypeNames.Attachment;
                                _mMailMessage.Attachments.Add(attachment);
                            }
                        }

                        try
                        {
                            strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                            strLog = new StringBuilder();
                            strLog.Append(SendEmailWithReturnLog(_mMailMessage));
                            SaveEmailSentLog(entTemplate.ID, _mMailMessage);
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
                                    SaveEmailSentLog(entTemplate.ID, _mMailMessage);
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
        /// SendPersonalizedAssignmentMail with attachment outlook (ics) file
        /// </summary>
        /// <param name="pListLearner"></param>
        /// <param name="pEmailInfo"></param>
        /// <param name="pstrAdditionalEmailBody"></param>
        /// <param name="plistActivityAssignment"></param>
        /// <param name="plistAssignment"></param>
        /// <param name="pListAttachments"></param>
        /// <returns></returns>
        public bool SendPersonalizedAssignmentMail(List<Learner> pListLearner, EmailDeliveryDashboard pEmailInfo, string pstrAdditionalEmailBody,
       List<ActivityAssignment> plistActivityAssignment, List<Assignment> plistAssignment, List<Attachment> pListAttachments)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            StringBuilder strEmailAddress = new StringBuilder("");
            EmailTemplate entTemplate;
            StringBuilder _strLearnerId = new StringBuilder("");
            string strFromDispalyName = string.Empty;
            string strLookupFile = string.Empty;
            int Count = 0;
            string attachmentAssignmentFilename = string.Empty;
            string attachmentDuedateFilename = string.Empty;
            try
            {
                entTemplate = GetEmailTemplate(pEmailInfo);
                #region To Add in Dashboard
                if (pEmailInfo.AddToDashboard)
                {
                    if (pListLearner != null && pListLearner.Count > 0)
                    {
                        foreach (Learner learner in pListLearner)
                        {
                            if (!string.IsNullOrEmpty(learner.EmailID))
                            {

                                if (ValidationManager.ValidateString(learner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                                {
                                    string trimEmailID = learner.EmailID.Trim();
                                    strEmailAddress.Append(trimEmailID + ",");
                                    _strLearnerId.Append(learner.ID + ",");
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(pEmailInfo.LearnerId))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(_strLearnerId)))
                            {
                                pEmailInfo.LearnerId = _strLearnerId.ToString().Substring(0, _strLearnerId.ToString().LastIndexOf(',')); ;
                            }
                        }

                        if (!string.IsNullOrEmpty(pEmailInfo.ToList))
                        {
                            pEmailInfo.ToList = pEmailInfo.ToList.Replace(";", ",");
                            pEmailInfo.ToList = pEmailInfo.ToList + "," + strEmailAddress.ToString();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(pEmailInfo.RuleId) || Convert.ToString(pEmailInfo.AssignmentTypeID) == "UI_ONETINMEASSIGNMENT")
                                pEmailInfo.ToList = strEmailAddress.ToString();
                        }
                    }
                    return AddEmailToDashBoard(pEmailInfo, pListAttachments, plistActivityAssignment, plistAssignment);
                }
                #endregion

                #region Send Email If Learner list
                try
                {

                    if (pListLearner != null && pListLearner.Count > 0)
                    {
                        int i = 0;
                        foreach (Learner learner in pListLearner)
                        {
                            _mMailMessage = new MailMessage();
                            strEmailAddress = new StringBuilder("");
                            emailsCount = 0;

                            #region Send Mail
                            //Add To
                            if (!ValidationManager.ValidateString(learner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                            {
                                LogMessage += "Invalid Email: " + learner.EmailID.Trim() + " ";
                                return false;
                            }
                            if (entTemplate != null)
                            {
                                if (!string.IsNullOrEmpty(learner.DefaultLanguageId))
                                {
                                    userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                    { return entTempToFind.LanguageId == learner.DefaultLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                    if (userMailTemplate == null)
                                    {
                                        userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                        { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                    }
                                    if (userMailTemplate == null)
                                    {
                                        userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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

                                        _mMailMessage.IsBodyHtml = true;
                                        if (plistActivityAssignment.Count > 0)
                                        {
                                            for (; i < plistActivityAssignment.Count; i++)
                                            {
                                                if (plistActivityAssignment[i].AssignmentDateSet == DateTime.MinValue)
                                                {
                                                    ActivityAssignment entActAss = new ActivityAssignment();
                                                    ActivityAssignmentAdaptor entActAssMngr = new ActivityAssignmentAdaptor();
                                                    entActAss.ID = plistActivityAssignment[i].ID;
                                                    entActAss.ClientId = learner.ClientId;
                                                    entActAss.UserID = learner.ID;

                                                    entActAss = entActAssMngr.GetActivityAssignmentByID(entActAss); // Execute(entActAss, ActivityAssignment.Method.Get);
                                                    plistActivityAssignment.RemoveAt(i);
                                                    plistActivityAssignment.Insert(i, entActAss);
                                                }
                                            }
                                        }

                                        _mMailMessage.Subject = GetMailBodyFromTemplate(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, learner, plistActivityAssignment, plistAssignment);
                                        _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, learner, plistActivityAssignment, plistAssignment);

                                        if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                        {
                                            _mMailMessage.Body += "<br/>" + pstrAdditionalEmailBody;
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
                                    foreach (EmailTemplateLanguage emailtemplatelang in entTemplate.EmailTemplateLanguage)
                                    {
                                        if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                                        {
                                            _mMailMessage = new MailMessage();
                                            _mMailMessage.IsBodyHtml = true;

                                            if (string.IsNullOrEmpty(strFromDispalyName))
                                            {
                                                if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                                {
                                                    strFromDispalyName = emailtemplatelang.DisplayName;
                                                }
                                            }

                                            _mMailMessage.Subject += GetMailBodyFromTemplate(emailtemplatelang.EmailSubjectText, emailtemplatelang.LanguageId, learner, plistActivityAssignment, plistAssignment) + " ";
                                            _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, learner, plistActivityAssignment, plistAssignment) + "<br/> ";

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
                            //Add To
                            if (string.IsNullOrEmpty(pEmailInfo.ToList))
                            {
                                if (!string.IsNullOrEmpty(learner.FirstName) && !string.IsNullOrEmpty(learner.LastName))
                                {
                                    _mMailMessage.To.Add(new MailAddress(learner.EmailID.Trim(), learner.FirstName + " " + learner.LastName));
                                }
                                else
                                {
                                    _mMailMessage.To.Add(new MailAddress(learner.EmailID.Trim()));
                                }
                            }
                            else
                            {
                                foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                            if (!string.IsNullOrEmpty(entTemplate.EmailReplyToId))
                            {
                                _mMailMessage.ReplyTo = new MailAddress(entTemplate.EmailReplyToId);
                            }
                            //Add From
                            if (!string.IsNullOrEmpty(entTemplate.EmailFromId))
                            {
                                if (string.IsNullOrEmpty(strFromDispalyName))
                                {
                                    _mMailMessage.From = new MailAddress(entTemplate.EmailFromId);
                                }
                                else
                                {
                                    _mMailMessage.From = new MailAddress(entTemplate.EmailFromId, strFromDispalyName);
                                }
                            }
                            else
                            {
                                throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                            }
                            //Add CC
                            if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                            {
                                foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                            if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                            {
                                foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
                                {
                                    _mMailMessage.Bcc.Add(address);
                                }
                            }

                            if (plistActivityAssignment.Count > 0)
                            {

                                if (plistActivityAssignment[i - 1].AssignmentDateSet.ToString() != "01/01/0001 00:00:00" && Count == 0)
                                    attachmentAssignmentFilename = FileHandler.GetOutlookAssignmentfile(i, plistActivityAssignment, pListLearner[0].ClientId, _mMailMessage.Subject);


                                if (plistActivityAssignment[i - 1].DueDateSet.ToString() != "01/01/0001 00:00:00" && Count == 0)
                                    attachmentDuedateFilename = FileHandler.GetOutlookDueDatefile(i, plistActivityAssignment, pListLearner[0].ClientId, _mMailMessage.Subject);
                                Count++;

                                _mMailMessage.IsBodyHtml = true;
                                if (!string.IsNullOrEmpty(attachmentAssignmentFilename))
                                {

                                    Attachment attachment = new Attachment(attachmentAssignmentFilename, MediaTypeNames.Application.Octet);
                                    ContentDisposition disposition = attachment.ContentDisposition;
                                    disposition.CreationDate = File.GetCreationTime(attachmentAssignmentFilename);
                                    disposition.ModificationDate = File.GetLastWriteTime(attachmentAssignmentFilename);
                                    disposition.ReadDate = File.GetLastAccessTime(attachmentAssignmentFilename);
                                    disposition.FileName = Path.GetFileName(attachmentAssignmentFilename);
                                    disposition.Size = new FileInfo(attachmentAssignmentFilename).Length;
                                    disposition.DispositionType = DispositionTypeNames.Attachment;
                                    _mMailMessage.Attachments.Add(attachment);
                                }
                                if (!string.IsNullOrEmpty(attachmentDuedateFilename))
                                {

                                    Attachment attachment = new Attachment(attachmentDuedateFilename, MediaTypeNames.Application.Octet);
                                    ContentDisposition disposition = attachment.ContentDisposition;
                                    disposition.CreationDate = File.GetCreationTime(attachmentDuedateFilename);
                                    disposition.ModificationDate = File.GetLastWriteTime(attachmentDuedateFilename);
                                    disposition.ReadDate = File.GetLastAccessTime(attachmentDuedateFilename);
                                    disposition.FileName = Path.GetFileName(attachmentDuedateFilename);
                                    disposition.Size = new FileInfo(attachmentDuedateFilename).Length;
                                    disposition.DispositionType = DispositionTypeNames.Attachment;
                                    _mMailMessage.Attachments.Add(attachment);
                                }
                            }
                            #endregion

                            try
                            {
                                strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                                strLog = new StringBuilder();
                                strLog.Append(SendEmailWithReturnLog(_mMailMessage));
                                SaveEmailSentLog(entTemplate.ID, _mMailMessage);
                                ////if (File.Exists(attachmentAssignmentFilename))
                                ////    File.Delete(attachmentAssignmentFilename);

                                ////if (File.Exists(attachmentDuedateFilename))
                                ////    File.Delete(attachmentDuedateFilename);

                            }
                            catch (SmtpFailedRecipientsException ex)
                            {
                                for (int j = 0; j < ex.InnerExceptions.Length; j++)
                                {
                                    SmtpStatusCode status = ex.InnerExceptions[j].StatusCode;
                                    if (status == SmtpStatusCode.MailboxBusy ||
                                        status == SmtpStatusCode.MailboxUnavailable)
                                    {
                                        Console.WriteLine("Delivery failed - retrying in 5 seconds.");

                                        strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                                        strLog.Append(SendEmailWithReturnLog(_mMailMessage));
                                        SaveEmailSentLog(entTemplate.ID, _mMailMessage);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed to deliver message to {0}",
                                            ex.InnerExceptions[i].FailedRecipient);
                                    }
                                }
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

                    if (!string.IsNullOrEmpty(pEmailInfo.ToList) && pListLearner.Count == 0)
                    {
                        #region Send Mail

                        if (entTemplate != null)
                        {
                            if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                            {
                                userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                if (userMailTemplate == null)
                                {
                                    userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                    { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                }
                                if (userMailTemplate == null)
                                {
                                    userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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

                                    _mMailMessage.Subject = GetMailBodyFromTemplate(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, null, plistActivityAssignment, plistAssignment);
                                    _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, null, plistActivityAssignment, plistAssignment);
                                    if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                    {
                                        _mMailMessage.Body += "<br/>" + pstrAdditionalEmailBody;
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
                                foreach (EmailTemplateLanguage emailtemplatelang in entTemplate.EmailTemplateLanguage)
                                {
                                    if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                                    {
                                        _mMailMessage = new MailMessage();
                                        _mMailMessage.IsBodyHtml = true;
                                        if (string.IsNullOrEmpty(strFromDispalyName))
                                        {
                                            if (!string.IsNullOrEmpty(userMailTemplate.DisplayName))
                                            {
                                                strFromDispalyName = emailtemplatelang.DisplayName;
                                            }
                                        }
                                        _mMailMessage.Subject += GetMailBodyFromTemplate(emailtemplatelang.EmailSubjectText, emailtemplatelang.LanguageId, null, plistActivityAssignment, plistAssignment) + " ";
                                        _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, null, plistActivityAssignment, plistAssignment) + "<br/> ";
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
                        //Add To                   
                        foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                        if (!string.IsNullOrEmpty(entTemplate.EmailReplyToId))
                        {
                            _mMailMessage.ReplyTo = new MailAddress(entTemplate.EmailReplyToId);
                        }
                        //Add From
                        if (!string.IsNullOrEmpty(entTemplate.EmailFromId))
                        {
                            if (string.IsNullOrEmpty(strFromDispalyName))
                            {
                                _mMailMessage.From = new MailAddress(entTemplate.EmailFromId);
                            }
                            else
                            {
                                _mMailMessage.From = new MailAddress(entTemplate.EmailFromId, strFromDispalyName);
                            }
                        }
                        else
                        {
                            throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                        }
                        //Add CC
                        if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                        {
                            foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                        if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                        {
                            foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
                            {
                                _mMailMessage.Bcc.Add(address);
                            }
                        }
                        try
                        {
                            strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                            strLog = new StringBuilder();
                            strLog.Append(SendEmailWithReturnLog(_mMailMessage));
                            SaveEmailSentLog(entTemplate.ID, _mMailMessage);
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
                                    SaveEmailSentLog(entTemplate.ID, _mMailMessage);
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

        /// SendPersonalizedAssignmentMail with attachment outlook (ics) file
        /// </summary>
        /// <param name="pListLearner"></param>
        /// <param name="pEmailInfo"></param>
        /// <param name="pstrAdditionalEmailBody"></param>
        /// <param name="plistActivityAssignment"></param>
        /// <param name="plistAssignment"></param>
        /// <param name="pListAttachments"></param>
        /// <returns></returns>
        public bool SendPersonalizedBulkAssignmentMail(List<Learner> pListLearner, EmailDeliveryDashboard pEmailInfo, string pstrAdditionalEmailBody,
       List<ActivityAssignment> plistActivityAssignment, List<Assignment> plistAssignment, List<Attachment> pListAttachments)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            StringBuilder strEmailAddress = new StringBuilder("");
            EmailTemplate entTemplate;
            StringBuilder _strLearnerId = new StringBuilder("");
            string strFromDispalyName = string.Empty;
            string strLookupFile = string.Empty;
            int Count = 0;
            string attachmentAssignmentFilename = string.Empty;
            string attachmentDuedateFilename = string.Empty;
            try
            {
                entTemplate = GetEmailTemplate(pEmailInfo);
                #region To Add in Dashboard
                if (pEmailInfo.AddToDashboard)
                {
                    if (pListLearner != null && pListLearner.Count > 0)
                    {
                        foreach (Learner learner in pListLearner)
                        {
                            if (!string.IsNullOrEmpty(learner.EmailID))
                            {

                                if (ValidationManager.ValidateString(learner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                                {
                                    string trimEmailID = learner.EmailID.Trim();
                                    strEmailAddress.Append(trimEmailID + ",");
                                    _strLearnerId.Append(learner.ID + ",");
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(pEmailInfo.LearnerId))
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(_strLearnerId)))
                            {
                                pEmailInfo.LearnerId = _strLearnerId.ToString().Substring(0, _strLearnerId.ToString().LastIndexOf(',')); ;
                            }
                        }

                        if (!string.IsNullOrEmpty(pEmailInfo.ToList))
                        {
                            pEmailInfo.ToList = pEmailInfo.ToList.Replace(";", ",");
                            pEmailInfo.ToList = pEmailInfo.ToList + "," + strEmailAddress.ToString();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(pEmailInfo.RuleId) || Convert.ToString(pEmailInfo.AssignmentTypeID) == "UI_ONETINMEASSIGNMENT")
                                pEmailInfo.ToList = strEmailAddress.ToString();
                        }
                    }
                    return AddEmailToDashBoard(pEmailInfo, pListAttachments, plistActivityAssignment, plistAssignment);
                }
                #endregion

                #region Send Email If Learner list
                try
                {

                    if (pListLearner != null && pListLearner.Count > 0)
                    {
                        int i = 0;
                        foreach (Learner learner in pListLearner)
                        {
                            _mMailMessage = new MailMessage();
                            strEmailAddress = new StringBuilder("");
                            emailsCount = 0;

                            #region Send Mail
                            //Add To
                            if (!ValidationManager.ValidateString(learner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                            {
                                LogMessage += "Invalid Email: " + learner.EmailID.Trim() + " ";
                                return false;
                            }
                            if (entTemplate != null)
                            {
                                if (!string.IsNullOrEmpty(learner.DefaultLanguageId))
                                {
                                    userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                    { return entTempToFind.LanguageId == learner.DefaultLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                    if (userMailTemplate == null)
                                    {
                                        userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                        { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                    }
                                    if (userMailTemplate == null)
                                    {
                                        userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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

                                        _mMailMessage.IsBodyHtml = true;
                                        if (plistActivityAssignment.Count > 0)
                                        {
                                            for (; i < plistActivityAssignment.Count; i++)
                                            {
                                                //if (plistActivityAssignment[i].AssignmentDateSet == DateTime.MinValue)
                                                //{
                                                ActivityAssignment entActAss = new ActivityAssignment();
                                                ActivityAssignmentAdaptor entActAssMngr = new ActivityAssignmentAdaptor();
                                                entActAss.ID = plistActivityAssignment[i].ID;
                                                entActAss.ClientId = learner.ClientId;
                                                entActAss.UserID = learner.ID;
                                                //if (plistActivityAssignment[i].AssignmentDateSet != DateTime.MinValue)
                                                //    entActAss.AssignmentDateSet = plistActivityAssignment[i].AssignmentDateSet;

                                                entActAss = entActAssMngr.GetUsersActivityAssignmentByID(entActAss); //Execute(entActAss, ActivityAssignment.Method.GetActivity); comment by vinod 
                                                if (entActAss.ActivityName == null)
                                                {
                                                    entActAss.ID = plistActivityAssignment[i].ID;
                                                    entActAss.ClientId = learner.ClientId;
                                                    entActAss.UserID = learner.ID;
                                                    entActAss = entActAssMngr.GetActivityAssignmentByID(entActAss); //Execute(entActAss, ActivityAssignment.Method.Get); comment by vinod
                                                }
                                                if (plistActivityAssignment[i].AssignmentDateSet != DateTime.MinValue)
                                                    entActAss.AssignmentDateSet = plistActivityAssignment[i].AssignmentDateSet;
                                                if (plistActivityAssignment[i].DueDateSet != DateTime.MinValue)
                                                    entActAss.DueDateSet = plistActivityAssignment[i].DueDateSet;

                                                if (plistActivityAssignment[i].ExpiryDateSet != DateTime.MinValue)
                                                    entActAss.ExpiryDateSet = plistActivityAssignment[i].ExpiryDateSet;

                                                if (plistActivityAssignment[i].ActivityType.ToString() != "None")
                                                    entActAss.ActivityType = plistActivityAssignment[i].ActivityType;

                                                plistActivityAssignment.RemoveAt(i);
                                                plistActivityAssignment.Insert(i, entActAss);
                                                //}
                                            }
                                        }

                                        _mMailMessage.Subject = GetMailBodyFromTemplate(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, learner, plistActivityAssignment, plistAssignment);
                                        _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, learner, plistActivityAssignment, plistAssignment);

                                        if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                        {
                                            _mMailMessage.Body += "<br/>" + pstrAdditionalEmailBody;
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
                                    foreach (EmailTemplateLanguage emailtemplatelang in entTemplate.EmailTemplateLanguage)
                                    {
                                        if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                                        {
                                            _mMailMessage = new MailMessage();
                                            _mMailMessage.IsBodyHtml = true;

                                            if (string.IsNullOrEmpty(strFromDispalyName))
                                            {
                                                if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                                {
                                                    strFromDispalyName = emailtemplatelang.DisplayName;
                                                }
                                            }

                                            _mMailMessage.Subject += GetMailBodyFromTemplate(emailtemplatelang.EmailSubjectText, emailtemplatelang.LanguageId, learner, plistActivityAssignment, plistAssignment) + " ";
                                            _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, learner, plistActivityAssignment, plistAssignment) + "<br/> ";

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
                            //Add To
                            if (string.IsNullOrEmpty(pEmailInfo.ToList))
                            {
                                if (!string.IsNullOrEmpty(learner.FirstName) && !string.IsNullOrEmpty(learner.LastName))
                                {
                                    _mMailMessage.To.Add(new MailAddress(learner.EmailID.Trim(), learner.FirstName + " " + learner.LastName));
                                }
                                else
                                {
                                    _mMailMessage.To.Add(new MailAddress(learner.EmailID.Trim()));
                                }
                            }
                            else
                            {
                                foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                            if (!string.IsNullOrEmpty(entTemplate.EmailReplyToId))
                            {
                                _mMailMessage.ReplyTo = new MailAddress(entTemplate.EmailReplyToId);
                            }
                            //Add From
                            if (!string.IsNullOrEmpty(entTemplate.EmailFromId))
                            {
                                if (string.IsNullOrEmpty(strFromDispalyName))
                                {
                                    _mMailMessage.From = new MailAddress(entTemplate.EmailFromId);
                                }
                                else
                                {
                                    _mMailMessage.From = new MailAddress(entTemplate.EmailFromId, strFromDispalyName);
                                }
                            }
                            else
                            {
                                throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                            }
                            //Add CC
                            if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                            {
                                foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                            if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                            {
                                foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
                                {
                                    _mMailMessage.Bcc.Add(address);
                                }
                            }

                            if (plistActivityAssignment.Count > 0)
                            {
                                int iLoop = 0;
                                for (int sCount = 0; sCount < plistActivityAssignment.Count; sCount++)
                                {
                                    if (plistActivityAssignment[sCount].AssignmentDateSet.ToString() != "01/01/0001 00:00:00" && Count == 0 && iLoop == 0)
                                    {
                                        attachmentAssignmentFilename = FileHandler.GetOutlookAssignmentfile(i, plistActivityAssignment, pListLearner[0].ClientId, _mMailMessage.Subject);
                                        iLoop = 1;
                                    }


                                    if (plistActivityAssignment[sCount].DueDateSet.ToString() != "01/01/0001 00:00:00" && Count == 0 && iLoop == 1)
                                    {
                                        attachmentDuedateFilename = FileHandler.GetOutlookDueDatefile(i, plistActivityAssignment, pListLearner[0].ClientId, _mMailMessage.Subject);
                                        iLoop = 1;
                                    }
                                }
                                Count++;

                                _mMailMessage.IsBodyHtml = true;
                                if (!string.IsNullOrEmpty(attachmentAssignmentFilename))
                                {

                                    Attachment attachment = new Attachment(attachmentAssignmentFilename, MediaTypeNames.Application.Octet);
                                    ContentDisposition disposition = attachment.ContentDisposition;
                                    disposition.CreationDate = File.GetCreationTime(attachmentAssignmentFilename);
                                    disposition.ModificationDate = File.GetLastWriteTime(attachmentAssignmentFilename);
                                    disposition.ReadDate = File.GetLastAccessTime(attachmentAssignmentFilename);
                                    disposition.FileName = Path.GetFileName(attachmentAssignmentFilename);
                                    disposition.Size = new FileInfo(attachmentAssignmentFilename).Length;
                                    disposition.DispositionType = DispositionTypeNames.Attachment;
                                    _mMailMessage.Attachments.Add(attachment);
                                }
                                if (!string.IsNullOrEmpty(attachmentDuedateFilename))
                                {

                                    Attachment attachment = new Attachment(attachmentDuedateFilename, MediaTypeNames.Application.Octet);
                                    ContentDisposition disposition = attachment.ContentDisposition;
                                    disposition.CreationDate = File.GetCreationTime(attachmentDuedateFilename);
                                    disposition.ModificationDate = File.GetLastWriteTime(attachmentDuedateFilename);
                                    disposition.ReadDate = File.GetLastAccessTime(attachmentDuedateFilename);
                                    disposition.FileName = Path.GetFileName(attachmentDuedateFilename);
                                    disposition.Size = new FileInfo(attachmentDuedateFilename).Length;
                                    disposition.DispositionType = DispositionTypeNames.Attachment;
                                    _mMailMessage.Attachments.Add(attachment);
                                }
                            }
                            #endregion

                            try
                            {
                                strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                                strLog = new StringBuilder();
                                strLog.Append(SendEmailWithReturnLog(_mMailMessage));
                                SaveEmailSentLog(entTemplate.ID, _mMailMessage);
                                ////if (File.Exists(attachmentAssignmentFilename))
                                ////    File.Delete(attachmentAssignmentFilename);

                                ////if (File.Exists(attachmentDuedateFilename))
                                ////    File.Delete(attachmentDuedateFilename);

                            }
                            catch (SmtpFailedRecipientsException ex)
                            {
                                for (int j = 0; j < ex.InnerExceptions.Length; j++)
                                {
                                    SmtpStatusCode status = ex.InnerExceptions[j].StatusCode;
                                    if (status == SmtpStatusCode.MailboxBusy ||
                                        status == SmtpStatusCode.MailboxUnavailable)
                                    {
                                        Console.WriteLine("Delivery failed - retrying in 5 seconds.");

                                        strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                                        strLog.Append(SendEmailWithReturnLog(_mMailMessage));
                                        SaveEmailSentLog(entTemplate.ID, _mMailMessage);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed to deliver message to {0}",
                                            ex.InnerExceptions[i].FailedRecipient);
                                    }
                                }
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

                    if (!string.IsNullOrEmpty(pEmailInfo.ToList) && pListLearner.Count == 0)
                    {
                        #region Send Mail

                        if (entTemplate != null)
                        {
                            if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                            {
                                userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                if (userMailTemplate == null)
                                {
                                    userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                    { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                                }
                                if (userMailTemplate == null)
                                {
                                    userMailTemplate = entTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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

                                    _mMailMessage.Subject = GetMailBodyFromTemplate(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, null, plistActivityAssignment, plistAssignment);
                                    _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, null, plistActivityAssignment, plistAssignment);
                                    if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                    {
                                        _mMailMessage.Body += "<br/>" + pstrAdditionalEmailBody;
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
                                foreach (EmailTemplateLanguage emailtemplatelang in entTemplate.EmailTemplateLanguage)
                                {
                                    if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                                    {
                                        _mMailMessage = new MailMessage();
                                        _mMailMessage.IsBodyHtml = true;
                                        if (string.IsNullOrEmpty(strFromDispalyName))
                                        {
                                            if (!string.IsNullOrEmpty(userMailTemplate.DisplayName))
                                            {
                                                strFromDispalyName = emailtemplatelang.DisplayName;
                                            }
                                        }
                                        _mMailMessage.Subject += GetMailBodyFromTemplate(emailtemplatelang.EmailSubjectText, emailtemplatelang.LanguageId, null, plistActivityAssignment, plistAssignment) + " ";
                                        _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, null, plistActivityAssignment, plistAssignment) + "<br/> ";
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
                        //Add To                   
                        foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                        if (!string.IsNullOrEmpty(entTemplate.EmailReplyToId))
                        {




                            _mMailMessage.ReplyTo = new MailAddress(entTemplate.EmailReplyToId);
                        }
                        //Add From
                        if (!string.IsNullOrEmpty(entTemplate.EmailFromId))
                        {
                            if (string.IsNullOrEmpty(strFromDispalyName))
                            {
                                _mMailMessage.From = new MailAddress(entTemplate.EmailFromId);
                            }
                            else
                            {
                                _mMailMessage.From = new MailAddress(entTemplate.EmailFromId, strFromDispalyName);
                            }
                        }
                        else
                        {
                            throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                        }
                        //Add CC
                        if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                        {
                            foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                        if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                        {
                            foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
                            {
                                _mMailMessage.Bcc.Add(address);
                            }
                        }
                        try
                        {
                            strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                            strLog = new StringBuilder();
                            strLog.Append(SendEmailWithReturnLog(_mMailMessage));
                            SaveEmailSentLog(entTemplate.ID, _mMailMessage);
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
                                    SaveEmailSentLog(entTemplate.ID, _mMailMessage);
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
        //public static string WriteErrorToFile(string pLog)
        //{
        //    string strErrCode = string.Empty;

        //    string _strFilPthForErr = string.Empty;
        //    try
        //    {

        //        _strFilPthForErr = "C:\\Log\\Error.txt";
        //    }
        //    catch (Exception ExcFc)
        //    {
        //    }


        //    try
        //    {
        //        StreamWriter swFile = new StreamWriter(_strFilPthForErr, true);
        //        swFile.WriteLine(pLog);
        //        swFile.Close();
        //    }
        //    catch (Exception ExcFw)
        //    {
        //    }
        //    return string.Empty;
        //}

        /// <summary>
        /// Send Personalized Mail For Schedular
        /// </summary>
        /// <param name="pListLearner"></param>
        /// <param name="pMailTemplate"></param>
        /// <param name="pstrAdditionalEmailBody"></param>
        /// <param name="pLanguageId"></param>
        /// <param name="pCC"></param>
        /// <param name="pBCC"></param>
        /// <param name="pTO"></param>
        /// <param name="pbAddToDashboard"></param>
        /// <param name="pstrDeliveryTitle"></param>
        /// <param name="pListAttachments"></param>
        /// <returns></returns>
        public bool SendPersonalizedMailForSchedular(List<Learner> pListLearner, EmailDeliveryDashboard pEmailInfo,
                                                    string pstrAdditionalEmailBody, List<Attachment> pListAttachments)
        {
            ////_mMailMessage = new MailMessage();
            ////_mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            StringBuilder strEmailAddress = new StringBuilder("");
            EmailTemplate entEmailTemplate;
            StringBuilder strLearnerIDs = new StringBuilder("");
            string strFromDispalyName = string.Empty;

            try
            {
                entEmailTemplate = GetEmailTemplate(pEmailInfo);

                if (pEmailInfo.AddToDashboard)
                {
                    if (pListLearner != null && pListLearner.Count > 0)
                    {
                        foreach (Learner learner in pListLearner)
                        {
                            //Validation remove- Abhay -Issue 167
                            if (ValidationManager.ValidateString(learner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                            {
                                string trimEmailID = learner.EmailID.Trim();
                                strEmailAddress.Append(trimEmailID + ",");
                                strLearnerIDs.Append(learner.ID + ",");
                            }
                        }
                        if (!string.IsNullOrEmpty(pEmailInfo.ToList))
                        {
                            pEmailInfo.ToList = pEmailInfo.ToList.Replace(";", ",");
                            pEmailInfo.ToList = pEmailInfo.ToList + "," + strEmailAddress.ToString();
                        }
                        else
                        {
                            pEmailInfo.ToList = strEmailAddress.ToString();
                        }
                    }
                    pEmailInfo.IsPersonalized = true;
                    if (!string.IsNullOrEmpty(strLearnerIDs.ToString()))
                    {
                        pEmailInfo.LearnerId = strLearnerIDs.ToString().Remove(strLearnerIDs.ToString().LastIndexOf(','));
                    }

                    return AddEmailToDashBoard(pEmailInfo, pListAttachments);
                }
                foreach (Learner learner in pListLearner)
                {
                    #region Send Mail
                    _mMailMessage = new MailMessage();
                    _mMailMessage.IsBodyHtml = true;
                    //Add To
                    if (!ValidationManager.ValidateString(learner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                    {
                        LogMessage += "Invalid Email: " + learner.EmailID.Trim() + " ";
                        return false;
                    }
                    if (entEmailTemplate != null)
                    {
                        if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                            if (userMailTemplate == null)
                            {
                                userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                            }
                            if (userMailTemplate == null)
                            {
                                userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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
                            EmailTemplateLanguage emailtemplatelang;
                            if (string.IsNullOrEmpty(learner.DefaultLanguageId))
                                learner.DefaultLanguageId = "en-US";
                            //foreach (EmailTemplateLanguage emailtemplatelang in entEmailTemplate.EmailTemplateLanguage)
                            //{
                            emailtemplatelang = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            {
                                return entTempToFind.LanguageId == learner.DefaultLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved;
                            });
                            if (emailtemplatelang == null)
                            {
                                userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                                {
                                    return entTempToFind.LanguageId == ADMIN_SITE_DEFAULT_LANGUAGE && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved;
                                });
                            }
                            if (emailtemplatelang != null)
                            {
                                if (string.IsNullOrEmpty(strFromDispalyName))
                                {
                                    if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                    {
                                        strFromDispalyName = emailtemplatelang.DisplayName;
                                    }
                                }

                                _mMailMessage.Subject += GetMailBodyFromTemplate(emailtemplatelang.EmailSubjectText, emailtemplatelang.LanguageId, learner) + " ";
                                _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, learner) + "<br/> ";
                                if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                {
                                    _mMailMessage.Body += pstrAdditionalEmailBody;
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

                    if (string.IsNullOrEmpty(pEmailInfo.ToList))
                    {
                        if (!string.IsNullOrEmpty(learner.FirstName) && !string.IsNullOrEmpty(learner.LastName))
                        {
                            _mMailMessage.To.Add(new MailAddress(learner.EmailID.Trim(), learner.FirstName + " " + learner.LastName));
                        }
                        else
                        {
                            _mMailMessage.To.Add(new MailAddress(learner.EmailID.Trim()));
                        }
                    }
                    else
                    {
                        foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                    if (!string.IsNullOrEmpty(entEmailTemplate.EmailReplyToId))
                    {
                        _mMailMessage.ReplyTo = new MailAddress(entEmailTemplate.EmailReplyToId);
                    }
                    //Add From
                    if (!string.IsNullOrEmpty(entEmailTemplate.EmailFromId))
                    {
                        if (string.IsNullOrEmpty(strFromDispalyName))
                        {
                            _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                        }
                        else
                        {
                            _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId, strFromDispalyName);
                        }
                    }
                    else
                    {
                        throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                    }
                    //Add CC
                    if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                    {
                        foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                    if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                    {
                        foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
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
                        SaveEmailSentLog(entEmailTemplate.ID, _mMailMessage);
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
                                SaveEmailSentLog(entEmailTemplate.ID, _mMailMessage);
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
        private MailMessage SendScheduledPersonalizedMail(Learner pLearner, EmailDeliveryDashboard pEmailInfo,
                            string pAttachments, List<ActivityAssignment> plistActivityAssignment,
                            List<Assignment> plistAssignment)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            string strFromDispalyName = string.Empty;
            EmailTemplateLanguage userMailTemplate = null;
            EmailTemplate entEmailTemplate;
            string strEmailBody = string.Empty;
            string strHeading = string.Empty;
            string strBody = string.Empty;
            //////AutoEmailTemplateSetting entAutoEmail = new AutoEmailTemplateSetting();
            //////AutoEmailTemplateSettingManager mgrAutoEmailTemplateSetting = new AutoEmailTemplateSettingManager();
            string strHeadingTemp = string.Empty;
            string strBodyTemp = string.Empty;
            int cntLink = 0;
            try
            {
                //////entAutoEmail.ClientId = pLearner.ClientId;
                //////entAutoEmail.ID = pEmailInfo.EmailTemplateID;
                //////entAutoEmail = mgrAutoEmailTemplateSetting.Execute(entAutoEmail, AutoEmailTemplateSetting.Method.Get);
                //////if (entAutoEmail != null && !string.IsNullOrEmpty(entAutoEmail.EmailTemplateID))
                //////{
                //////    pEmailInfo.EmailTemplateID = entAutoEmail.EmailTemplateID;
                //////}
                entEmailTemplate = GetEmailTemplate(pEmailInfo);
                //Add To
                if (!ValidationManager.ValidateString(pLearner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                {
                    LogMessage += "Invalid Email: " + pLearner.EmailID.Trim() + " ";
                    return null;
                }
                if (entEmailTemplate != null)
                {
                    if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                    {
                        userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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
                            _mMailMessage.Subject = GetMailBodyFromTemplate(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, pLearner, plistActivityAssignment, plistAssignment);
                            _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, pLearner, plistActivityAssignment, plistAssignment);

                        }
                        else
                        {
                            LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                            return null;
                        }
                    }
                    else
                    {
                        foreach (EmailTemplateLanguage emailtemplatelang in entEmailTemplate.EmailTemplateLanguage)
                        {
                            if (emailtemplatelang.LanguageId == "en-US")
                            {
                                _mMailMessage.Subject += GetMailBodyFromTemplate(emailtemplatelang.EmailSubjectText, emailtemplatelang.LanguageId, pLearner, plistActivityAssignment, plistAssignment) + " ";
                            }

                            if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                            {
                                if (string.IsNullOrEmpty(strFromDispalyName))
                                {
                                    if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                    {
                                        strFromDispalyName = emailtemplatelang.DisplayName;
                                    }
                                }

                                string strLanguageName = string.Empty;

                                if (!string.IsNullOrEmpty(emailtemplatelang.LanguageId))
                                {
                                    Language lang = new Language();
                                    Language langReturn = null;
                                    LanguageManager langManager = new LanguageManager();
                                    lang.ClientId = _strClientId;
                                    lang.ID = emailtemplatelang.LanguageId;

                                    try
                                    {

                                        langReturn = langManager.Execute(lang, Language.Method.Get);
                                        if (langReturn != null && !string.IsNullOrEmpty(langReturn.ID))
                                            strLanguageName = langReturn.LanguageEnglishName;
                                    }
                                    catch { }
                                }
                                strBody = "<div>" + emailtemplatelang.EmailBodyText + "<br/><br/>" +
                                     "</div>";

                                strHeadingTemp += GetMailBodyFromTemplate(strHeading, emailtemplatelang.LanguageId, pLearner, plistActivityAssignment, plistAssignment);// + "<br/> ";
                                strBodyTemp += GetMailBodyFromTemplate(strBody, emailtemplatelang.LanguageId, pLearner, plistActivityAssignment, plistAssignment) + "<br/> ";
                                cntLink = cntLink + 1;
                            }
                        }
                        _mMailMessage.Body = "<html><body>" + strHeadingTemp + "<br/><br/><br/>" + strBodyTemp + "</body></html>";
                        ////EmailTemplateLanguage emailtemplatelang;
                        ////if (string.IsNullOrEmpty(pLearner.DefaultLanguageId))
                        ////    pLearner.DefaultLanguageId = "en-US";
                        ////emailtemplatelang = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        ////{
                        ////    return entTempToFind.LanguageId == pLearner.DefaultLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved;
                        ////});
                        ////if (emailtemplatelang == null)
                        ////{
                        ////    userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        ////    {
                        ////        return entTempToFind.LanguageId == ADMIN_SITE_DEFAULT_LANGUAGE && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved;
                        ////    });
                        ////}
                        ////if (emailtemplatelang != null)
                        ////{
                        ////    _mMailMessage.Subject += GetMailBodyFromTemplate(emailtemplatelang.EmailSubjectText, emailtemplatelang.LanguageId, pLearner, plistActivityAssignment, plistAssignment) + " ";

                        //if (!string.IsNullOrEmpty(strLanguageName))
                        //{
                        //    strHeading = "<div style=\"float:left;\"><span style='color: #0070C0;float:left;width:100px;'> " + "  " +
                        //    "<a href='#bg" + cntLink + "'>" + strLanguageName + "</a></span></div>";
                        //}
                        //else
                        //{
                        //    strHeading = "<div style=\"float:left;\"><span style='color: #0070C0;float:left;width:100px;'> " + "  " +
                        //    "<a href='#bg" + cntLink + "'>" + emailtemplatelang.LanguageId + "</a></span></div>";
                        //}


                        ////    if (string.IsNullOrEmpty(strFromDispalyName))
                        ////    {
                        ////        if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                        ////        {
                        ////            strFromDispalyName = emailtemplatelang.DisplayName;
                        ////        }
                        ////    }



                        ////    strBodyTemp += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, pLearner, plistActivityAssignment, plistAssignment) + "<br/> ";
                        ////    cntLink = cntLink + 1;
                        ////}


                        ////_mMailMessage.Body = strBodyTemp;//"<html><body>" + strHeadingTemp + strBodyTemp + "</body></html>";
                    }
                }
                else
                {
                    LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                    return null;
                }
                //Add To
                if (string.IsNullOrEmpty(pEmailInfo.ToList))
                {
                    if (!string.IsNullOrEmpty(pLearner.FirstName) && !string.IsNullOrEmpty(pLearner.LastName))
                    {
                        _mMailMessage.To.Add(new MailAddress(pLearner.EmailID.Trim(), pLearner.FirstName + " " + pLearner.LastName));
                    }
                    else
                    {
                        _mMailMessage.To.Add(new MailAddress(pLearner.EmailID.Trim()));
                    }
                }
                else
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailReplyToId))
                {
                    _mMailMessage.ReplyTo = new MailAddress(entEmailTemplate.EmailReplyToId);
                }
                //Add From 
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailFromId))
                {

                    if (string.IsNullOrEmpty(strFromDispalyName))
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                    }
                    else
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId, strFromDispalyName);
                    }
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
                    {
                        _mMailMessage.Bcc.Add(address);
                    }
                }
                //Add Attachments
                if (!string.IsNullOrEmpty(pAttachments))
                {
                    List<Attachment> listAttachments = GetAttachments(pAttachments);
                    if (listAttachments != null)
                    {
                        if (listAttachments.Count > 0)
                        {
                            foreach (Attachment attachments in listAttachments)
                            {
                                _mMailMessage.Attachments.Add(attachments);
                            }
                        }
                    }
                }
                strMailAddresses.Append(SendEmailAddresses(_mMailMessage));

                isEmailSent = false;

                strLog.Append(SendEmailWithReturnLog(_mMailMessage));
                if (isEmailSent)
                {
                    if (pLearner != null && !string.IsNullOrEmpty(pLearner.ID))
                        strSystemUserGuId.Append(pLearner.ID + ",");
                }

            }
            catch (SmtpException ex)
            {
                CustomException objCustEx = new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, ex, true);
                throw;
            }
            catch (Exception expCommon)
            {
                CustomException objCustEx = new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw;
            }


            return _mMailMessage;
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
        private MailMessage SendScheduledPersonalizedAssignmentMail(Learner pLearner, EmailDeliveryDashboard pEmailInfo,
                            string pAttachments, List<ActivityAssignment> plistActivityAssignment,
                            List<Assignment> plistAssignment)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            string strFromDispalyName = string.Empty;
            EmailTemplateLanguage userMailTemplate = null;
            EmailTemplate entEmailTemplate;
            string strEmailBody = string.Empty;
            string strHeading = string.Empty;
            string strBody = string.Empty;

            string strHeadingTemp = string.Empty;
            string strBodyTemp = string.Empty;
            int cntLink = 0;
            int i = 0;
            try
            {

                entEmailTemplate = GetEmailTemplate(pEmailInfo);
                //Add To
                if (!ValidationManager.ValidateString(pLearner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                {
                    LogMessage += "Invalid Email: " + pLearner.EmailID.Trim() + " ";
                    return null;
                }
                if (entEmailTemplate != null)
                {
                    if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                    {
                        userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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
                            _mMailMessage.Subject = GetMailBodyFromTemplate(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, pLearner, plistActivityAssignment, plistAssignment);
                            _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, pLearner, plistActivityAssignment, plistAssignment);

                        }
                        else
                        {
                            LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                            return null;
                        }
                    }
                    else
                    {
                        foreach (EmailTemplateLanguage emailtemplatelang in entEmailTemplate.EmailTemplateLanguage)
                        {
                            if (emailtemplatelang.LanguageId == "en-US")
                            {
                                _mMailMessage.Subject += GetMailBodyFromTemplate(emailtemplatelang.EmailSubjectText, emailtemplatelang.LanguageId, pLearner, plistActivityAssignment, plistAssignment) + " ";
                            }

                            if (emailtemplatelang.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved)
                            {
                                if (string.IsNullOrEmpty(strFromDispalyName))
                                {
                                    if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                    {
                                        strFromDispalyName = emailtemplatelang.DisplayName;
                                    }
                                }

                                string strLanguageName = string.Empty;

                                if (!string.IsNullOrEmpty(emailtemplatelang.LanguageId))
                                {
                                    Language lang = new Language();
                                    Language langReturn = null;
                                    LanguageManager langManager = new LanguageManager();
                                    lang.ClientId = _strClientId;
                                    lang.ID = emailtemplatelang.LanguageId;

                                    try
                                    {

                                        langReturn = langManager.Execute(lang, Language.Method.Get);
                                        if (langReturn != null && !string.IsNullOrEmpty(langReturn.ID))
                                            strLanguageName = langReturn.LanguageEnglishName;
                                    }
                                    catch { }
                                }
                                strBody = "<div>" + emailtemplatelang.EmailBodyText + "<br/><br/>" +
                                     "</div>";

                                strHeadingTemp += GetMailBodyFromTemplate(strHeading, emailtemplatelang.LanguageId, pLearner, plistActivityAssignment, plistAssignment);// + "<br/> ";
                                strBodyTemp += GetMailBodyFromTemplate(strBody, emailtemplatelang.LanguageId, pLearner, plistActivityAssignment, plistAssignment) + "<br/> ";
                                cntLink = cntLink + 1;
                            }
                        }
                        _mMailMessage.Body = "<html><body>" + strHeadingTemp + "<br/><br/><br/>" + strBodyTemp + "</body></html>";

                    }
                }
                else
                {
                    LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                    return null;
                }
                //Add To
                if (string.IsNullOrEmpty(pEmailInfo.ToList))
                {
                    if (!string.IsNullOrEmpty(pLearner.FirstName) && !string.IsNullOrEmpty(pLearner.LastName))
                    {
                        _mMailMessage.To.Add(new MailAddress(pLearner.EmailID.Trim(), pLearner.FirstName + " " + pLearner.LastName));
                    }
                    else
                    {
                        _mMailMessage.To.Add(new MailAddress(pLearner.EmailID.Trim()));
                    }
                }
                else
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailReplyToId))
                {
                    _mMailMessage.ReplyTo = new MailAddress(entEmailTemplate.EmailReplyToId);
                }
                //Add From 
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailFromId))
                {

                    if (string.IsNullOrEmpty(strFromDispalyName))
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                    }
                    else
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId, strFromDispalyName);
                    }
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
                    {
                        _mMailMessage.Bcc.Add(address);
                    }
                }
                //Add Attachments
                if (!string.IsNullOrEmpty(pAttachments))
                {
                    List<Attachment> listAttachments = GetAttachments(pAttachments);
                    if (listAttachments != null)
                    {
                        if (listAttachments.Count > 0)
                        {
                            foreach (Attachment attachments in listAttachments)
                            {
                                _mMailMessage.Attachments.Add(attachments);
                            }
                        }
                    }
                }
                string attachmentAssignmentFilename = string.Empty;
                string attachmentDuedateFilename = string.Empty;
                if (plistActivityAssignment.Count > 0)
                {

                    if (plistActivityAssignment[i].AssignmentDateSet.ToString() != "01/01/0001 00:00:00")
                        attachmentAssignmentFilename = FileHandler.GetOutlookAssignmentfile(i, plistActivityAssignment, pLearner.ClientId, _mMailMessage.Subject);
                    if (plistActivityAssignment[i].DueDateSet.ToString() != "01/01/0001 00:00:00")
                        attachmentDuedateFilename = FileHandler.GetOutlookDueDatefile(i, plistActivityAssignment, pLearner.ClientId, _mMailMessage.Subject);

                    _mMailMessage.IsBodyHtml = true;
                    if (!string.IsNullOrEmpty(attachmentAssignmentFilename))
                    {

                        Attachment attachment = new Attachment(attachmentAssignmentFilename, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = attachment.ContentDisposition;
                        disposition.CreationDate = File.GetCreationTime(attachmentAssignmentFilename);
                        disposition.ModificationDate = File.GetLastWriteTime(attachmentAssignmentFilename);
                        disposition.ReadDate = File.GetLastAccessTime(attachmentAssignmentFilename);
                        disposition.FileName = Path.GetFileName(attachmentAssignmentFilename);
                        disposition.Size = new FileInfo(attachmentAssignmentFilename).Length;
                        disposition.DispositionType = DispositionTypeNames.Attachment;
                        _mMailMessage.Attachments.Add(attachment);
                    }
                    if (!string.IsNullOrEmpty(attachmentDuedateFilename))
                    {

                        Attachment attachment = new Attachment(attachmentDuedateFilename, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = attachment.ContentDisposition;
                        disposition.CreationDate = File.GetCreationTime(attachmentDuedateFilename);
                        disposition.ModificationDate = File.GetLastWriteTime(attachmentDuedateFilename);
                        disposition.ReadDate = File.GetLastAccessTime(attachmentDuedateFilename);
                        disposition.FileName = Path.GetFileName(attachmentDuedateFilename);
                        disposition.Size = new FileInfo(attachmentDuedateFilename).Length;
                        disposition.DispositionType = DispositionTypeNames.Attachment;
                        _mMailMessage.Attachments.Add(attachment);
                    }
                }
                strMailAddresses.Append(SendEmailAddresses(_mMailMessage));

                isEmailSent = false;

                strLog.Append(SendEmailWithReturnLog(_mMailMessage));
                if (isEmailSent)
                {
                    if (pLearner != null && !string.IsNullOrEmpty(pLearner.ID))
                        strSystemUserGuId.Append(pLearner.ID + ",");
                }
                ////if (File.Exists(attachmentAssignmentFilename))
                ////    File.Delete(attachmentAssignmentFilename);

                ////if (File.Exists(attachmentDuedateFilename))
                ////    File.Delete(attachmentDuedateFilename);
            }
            catch (SmtpException ex)
            {
                CustomException objCustEx = new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, ex, true);
                throw;
            }
            catch (Exception expCommon)
            {
                CustomException objCustEx = new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw;
            }


            return _mMailMessage;
        }
        /// <summary>
        /// Send Test mail to a To/CC/BCC
        /// </summary>        
        /// <param name="pMailTemplate">Email Template</para>
        ///  <param name="pstrAdditionalEmailBody">Additional Email Body</para>
        /// <param name="pCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param> 
        /// <param name="pBCC">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <param name="pTO">Comma ',' seperated email list if Display Name set as - User Name<user@mail.com>,User2 Name<user2@mail.com></param>
        /// <returns></returns>
        public bool SendTemplateTestMail(EmailDeliveryDashboard pEmailInfo, string pstrAdditionalEmailBody,
             Learner pLearner, List<ActivityAssignment> plistActivityAssignment, List<Assignment> plistAssignment)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            EmailTemplate entEmailTemplate;
            string strFromDispalyName = string.Empty;
            try
            {
                entEmailTemplate = GetEmailTemplate(pEmailInfo);
                if (entEmailTemplate != null)
                {
                    userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                    { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId; });
                    if (userMailTemplate != null)
                    {
                        if (string.IsNullOrEmpty(strFromDispalyName))
                        {
                            if (!string.IsNullOrEmpty(userMailTemplate.DisplayName))
                            {
                                strFromDispalyName = userMailTemplate.DisplayName;
                            }
                        }
                        _mMailMessage.Subject = GetMailBodyFromTemplate(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, pLearner, plistActivityAssignment, plistAssignment);
                        _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, pLearner, plistActivityAssignment, plistAssignment);
                        if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                        {
                            _mMailMessage.Body += pstrAdditionalEmailBody;
                        }
                    }
                    else
                    {
                        LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED);
                        return false;
                    }
                }
                else
                {
                    LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED);
                    return false;
                }
                //Add To
                if (!string.IsNullOrEmpty(pEmailInfo.ToList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                    LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.User.EMAIL_NOT_AVAIL);
                    return false;
                }
                //Add Reply
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailReplyToId))
                {
                    _mMailMessage.ReplyTo = new MailAddress(entEmailTemplate.EmailReplyToId);
                }
                //Add From
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailFromId))
                {
                    if (string.IsNullOrEmpty(strFromDispalyName))
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                    }
                    else
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId, strFromDispalyName);
                    }
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
                    {
                        _mMailMessage.Bcc.Add(address);
                    }
                }
                //SendEmail(_mMailMessage);
                strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                strLog.Append(SendEmailWithReturnLog(_mMailMessage));
                SaveEmailSentLog(entEmailTemplate.ID, _mMailMessage);
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
        /// <returns></returns>                
        public bool SendPublicMail(EmailDeliveryDashboard pEmailInfo, string pstrAdditionalEmailBody, List<Attachment> pListAttachments)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            EmailTemplate entEmailTemplate;
            string strFromDispalyName = string.Empty;
            try
            {
                entEmailTemplate = GetEmailTemplate(pEmailInfo);
                if (pEmailInfo.AddToDashboard)
                {
                    pEmailInfo.IsPersonalized = false;
                    return AddEmailToDashBoard(pEmailInfo, pListAttachments);
                }


                if (entEmailTemplate != null)
                {
                    if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                    {
                        userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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
                        if (entEmailTemplate.EmailTemplateLanguage.Count > 0)
                        {
                            foreach (EmailTemplateLanguage emailtemplatelang in entEmailTemplate.EmailTemplateLanguage)
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
                        else
                        {
                            EmailTemplate eTemp = pEmailInfo.EmailTemplate;
                            if (eTemp != null)
                            {
                                if (string.IsNullOrEmpty(strFromDispalyName))
                                {
                                    if (!string.IsNullOrEmpty(eTemp.DisplayName))
                                    {
                                        strFromDispalyName = eTemp.DisplayName;
                                    }
                                }
                                _mMailMessage.Subject += eTemp.EmailSubjectText + " ";
                                _mMailMessage.Body += eTemp.EmailBodyText + "<br/> ";
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
                if (!string.IsNullOrEmpty(pEmailInfo.ToList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailReplyToId))
                {
                    _mMailMessage.ReplyTo = new MailAddress(entEmailTemplate.EmailReplyToId);
                }
                //Add From 
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailFromId))
                {
                    if (userMailTemplate != null)
                    {
                        if (string.IsNullOrEmpty(strFromDispalyName))
                        {
                            _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                        }
                        else
                        {
                            _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId, strFromDispalyName);
                        }
                    }
                    else
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                    }
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
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
                SaveEmailSentLog(entEmailTemplate.ID, _mMailMessage);
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
        /// <returns></returns>                
        private MailMessage SendScheduledPublicMail(EmailDeliveryDashboard pEmailInfo, string pstrAdditionalEmailBody, string pAttachments)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            string strFromDispalyName = string.Empty;
            EmailTemplateLanguage userMailTemplate = null;
            EmailTemplate entEmailTemplate;
            string strEmailBody = string.Empty;
            string strHeading = string.Empty;
            string strBody = string.Empty;

            string strHeadingTemp = string.Empty;
            string strBodyTemp = string.Empty;
            try
            {
                entEmailTemplate = GetEmailTemplate(pEmailInfo);
                if (entEmailTemplate != null)
                {
                    if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                    {
                        userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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
                            return null;
                        }
                    }
                    else
                    {
                        foreach (EmailTemplateLanguage emailtemplatelang in entEmailTemplate.EmailTemplateLanguage)
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
                                //_mMailMessage.Body += emailtemplatelang.EmailBodyText + "<br/> ";
                                strBody = "<div>" + emailtemplatelang.EmailBodyText + "<br/><br/>" +
                                     "</div>";

                                strHeadingTemp += GetMailBodyFromTemplate(strHeading, emailtemplatelang.LanguageId, null, null, null);
                                strBodyTemp += GetMailBodyFromTemplate(strBody, emailtemplatelang.LanguageId, null, null, null) + "<br/> ";

                                if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                {
                                    _mMailMessage.Body += pstrAdditionalEmailBody;
                                }
                            }
                        }
                        _mMailMessage.Body = "<html><body>" + strHeadingTemp + "<br/><br/><br/>" + strBodyTemp + "</body></html>";
                    }
                }
                else
                {
                    LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                    return null;
                }
                //Add To
                //if (!string.IsNullOrEmpty(pEmailInfo.ToList))
                //{
                //    foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
                //    {
                //        if (!string.IsNullOrEmpty(address.DisplayName))
                //        {
                //            _mMailMessage.To.Add(new MailAddress(address.Address, address.DisplayName));
                //        }
                //        else
                //        {
                //            _mMailMessage.To.Add(new MailAddress(address.Address));
                //        }

                //    }
                //}
                //Add To and Reply
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailReplyToId))
                {
                    _mMailMessage.ReplyTo = new MailAddress(entEmailTemplate.EmailReplyToId);
                }
                //Add From 
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailFromId))
                {

                    if (string.IsNullOrEmpty(strFromDispalyName))
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                    }
                    else
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId, strFromDispalyName);
                    }
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
                    {
                        _mMailMessage.Bcc.Add(address);
                    }
                }
                //Add Attachments
                if (!string.IsNullOrEmpty(pAttachments))
                {
                    List<Attachment> listAttachments = GetAttachments(pAttachments);

                    if (listAttachments != null)
                    {
                        if (listAttachments.Count > 0)
                        {
                            foreach (Attachment attachments in listAttachments)
                            {
                                _mMailMessage.Attachments.Add(attachments);
                            }
                        }
                    }
                }
                // added by Gitanjali 25.7.2010
                if (!string.IsNullOrEmpty(pEmailInfo.ToList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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


                            //SendEmail(_mMailMessage);

                            //Thread.Sleep(new TimeSpan(0, 0, 0, 0, MailServerToWaitAfterSend));
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                        if (_mMailMessage.To.Count >= 0)
                            _mMailMessage.To.RemoveAt(0);
                    }

                    foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                //SendEmail(_mMailMessage);
            }
            catch (Exception expCommon)
            {
                throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
            }
            return _mMailMessage;
        }
        /// <summary>
        ///  Send Scheduled Mail
        /// </summary>
        /// <param name="pEmailDelivery">EmailDeliveryDashboard</param>
        public void SendScheduledMail(EmailDeliveryDashboard pEmailDelivery)
        {

            List<Learner> entListLearner;
            List<ActivityAssignment> entActivityAssignment = null;
            List<ActivityAssignment> allUserActivityAssignmentList = new List<ActivityAssignment>();
            List<Assignment> entAssignment = null;
            MailMessage retMailMessage = null;
            StringBuilder strEmailAddress = new StringBuilder("");
            StringBuilder strCCEmailAddress = new StringBuilder("");
            StringBuilder strBCCEmailAddress = new StringBuilder("");
            string strToEmailAddress = string.Empty;
            if (pEmailDelivery != null)
            {
                entListLearner = new List<Learner>();
                if (!string.IsNullOrEmpty(pEmailDelivery.DistributionListId))
                {
                    entListLearner = null;
                    entListLearner = GetInScopeLearnersFromDistrubutionList(pEmailDelivery.DistributionListId, pEmailDelivery.CreatedById);
                    #region  To get the assignment details of the user who are in distribution list
                    if (entListLearner.Count > 0)
                    {
                        string strAllUser = string.Empty;
                        ActivityAssignment entObj = new ActivityAssignment();
                        ActivityAssignmentAdaptor _entActivityManager = new ActivityAssignmentAdaptor();
                        foreach (Learner entUser in entListLearner)
                            strAllUser += entUser.ID + ",";
                        if (!string.IsNullOrEmpty(strAllUser))
                        {
                            strAllUser = strAllUser.Substring(0, strAllUser.LastIndexOf(","));

                            entObj.ClientId = pEmailDelivery.ClientId;
                            entObj.ID = pEmailDelivery.DistributionListId;
                            entObj.UserID = strAllUser;

                            allUserActivityAssignmentList = _entActivityManager.GetUserAssignmentListForEmail(entObj); //Execute(entObj, ActivityAssignment.ListMethod.GetUserAssignmentListForEmail); comment by vinod
                        }
                    }

                    #endregion

                }//updated by Gitanjali 22.7.2010
                //Added by Mahesh for Dynamic asssignment Email
                else if (!string.IsNullOrEmpty(pEmailDelivery.RuleId) && Convert.ToString(pEmailDelivery.AssignmentTypeID) != "UI_ONETINMEASSIGNMENT")
                {
                    entListLearner = null;
                    entListLearner = GetDynmicassignmentUserListByRule(pEmailDelivery);
                }  //Added by Mahesh for Dynamic asssignment Email
                else if (!string.IsNullOrEmpty(pEmailDelivery.ToList) && string.IsNullOrEmpty(pEmailDelivery.LearnerId))
                {
                    entListLearner = null;
                    entListLearner = GetLearnersFromToList(pEmailDelivery.ToList);
                }
                else if (!string.IsNullOrEmpty(pEmailDelivery.ToList) && !string.IsNullOrEmpty(pEmailDelivery.LearnerId))
                {
                    entListLearner = null;
                    entListLearner = GetLearnersFromLearnerId(pEmailDelivery.LearnerId);
                }// added by gitanjali 27.08.2010 for email to managers
                else if (pEmailDelivery.IsDistributionToManager && !string.IsNullOrEmpty(pEmailDelivery.ManagerEmailId))
                {
                    pEmailDelivery.ToList = pEmailDelivery.ManagerEmailId.Trim();
                    try
                    {
                        pEmailDelivery.PreferredLanguageId = _strClientLanguageId;
                        //retMailMessage = SendScheduledPersonalizedMail(learner, pEmailDelivery, pEmailDelivery.AttachmentPathList, entActivityAssignment, entAssignment);
                        retMailMessage = SendScheduledMailToManager(pEmailDelivery, pEmailDelivery.AttachmentPathList);
                        SaveEmailSentLog(pEmailDelivery, retMailMessage, null, "");
                        return;
                    }
                    catch (Exception ex)
                    {
                        return;
                    }

                }
                //end Gitanjali
                else
                {
                    return;
                }

                //Added by Manoj - Issue No - DHD-19227 
                if (pEmailDelivery.AssignmentMode == ActivityAssignmentMode.BulkImport && !string.IsNullOrEmpty(pEmailDelivery.ActivityId) && entListLearner != null && entListLearner.Count > 0)
                {
                    entActivityAssignment = GetActivityAssignmentListForBulkImport(pEmailDelivery.ActivityId, entListLearner);
                }
                else if (!string.IsNullOrEmpty(pEmailDelivery.ActivityId) && entListLearner != null && entListLearner.Count > 0)
                {
                    entActivityAssignment = GetActivityAssignmentList(pEmailDelivery.ActivityId, entListLearner);
                }


                // updated by Gitanjali for bulk import - for assigning activity to learner
                //if (!string.IsNullOrEmpty(pEmailDelivery.ActivityId) && pEmailDelivery.AssignmentMode == ActivityAssignmentMode.BulkImport)
                //{
                //    entActivityAssignment = GetActivityAssignmentList(pEmailDelivery.ActivityId, entListLearner);
                //}
                //else if (!string.IsNullOrEmpty(pEmailDelivery.ActivityId) && pEmailDelivery.AssignmentMode != ActivityAssignmentMode.BulkImport)
                //{
                //    entActivityAssignment = GetActivityAssignmentList(pEmailDelivery.ActivityId);
                //}

                if (!string.IsNullOrEmpty(pEmailDelivery.AssignmentId))
                {
                    entAssignment = GetAssignmentList(pEmailDelivery.AssignmentId);
                }

                if (!string.IsNullOrEmpty(pEmailDelivery.CCList))
                {
                    strCCEmailAddress.Append(pEmailDelivery.CCList.Replace(";", ","));
                }
                if (!string.IsNullOrEmpty(pEmailDelivery.BCCList))
                {
                    strBCCEmailAddress.Append(pEmailDelivery.BCCList.Replace(";", ","));
                }
                if (pEmailDelivery.IsPersonalized)
                {
                    if (!string.IsNullOrEmpty(pEmailDelivery.ToList))
                        strToEmailAddress = pEmailDelivery.ToList;

                    int i = 0;
                    if (entListLearner != null)
                    {
                        foreach (Learner learner in entListLearner)
                        {
                            string strCCList = strCCEmailAddress.ToString();
                            string strBCCList = strBCCEmailAddress.ToString();

                            #region Personlize Mail
                            if (pEmailDelivery.IsCCManager)
                            {
                                if (!string.IsNullOrEmpty(learner.ManagerEmailId) && ValidationManager.ValidateString(learner.ManagerEmailId.Trim(), ValidationManager.DataType.EmailID))
                                {
                                    //updated by Gitanjali 28.7.2010
                                    // strCCEmailAddress.Append("," + learner.ManagerEmailId + ",");
                                    strCCList = strCCList + "," + learner.ManagerEmailId.Trim();
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(learner.ManagerEmailId))
                                        LogMessage += "Invalid Email: " + learner.ManagerEmailId.Trim() + " ";
                                }
                            }
                            if (pEmailDelivery.IsBCCManager)
                            {
                                if (!string.IsNullOrEmpty(learner.ManagerEmailId) && ValidationManager.ValidateString(learner.ManagerEmailId.Trim(), ValidationManager.DataType.EmailID))
                                {
                                    //updated by Gitanjali 28.7.2010
                                    //strBCCEmailAddress.Append("," + learner.ManagerEmailId + ",");
                                    strBCCList = strBCCList + "," + learner.ManagerEmailId.Trim();
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(learner.ManagerEmailId))
                                        LogMessage += "Invalid Email: " + learner.ManagerEmailId.Trim() + " ";
                                }
                            }
                            pEmailDelivery.CCList = Convert.ToString(strCCList);
                            pEmailDelivery.BCCList = Convert.ToString(strBCCList);

                            #endregion

                            // added by Gitanjali 15.7.2010 to select only assigned activity
                            List<ActivityAssignment> lstActivityAssignment = new List<ActivityAssignment>();
                            if (pEmailDelivery.AssignmentMode == ActivityAssignmentMode.BulkImport)
                            {
                                //if (entActivityAssignment != null && entActivityAssignment.Count >= i && entActivityAssignment.Count != 0)
                                //    lstActivityAssignment.Add(entActivityAssignment[i]);
                                lstActivityAssignment = entActivityAssignment;
                            }
                            else if (!string.IsNullOrEmpty(pEmailDelivery.DistributionListId))
                            {
                                //To get the activities of the user who are in distribution list
                                if (allUserActivityAssignmentList != null && allUserActivityAssignmentList.Count >= i && allUserActivityAssignmentList.Count != 0)
                                    lstActivityAssignment = allUserActivityAssignmentList.FindAll(delegate (ActivityAssignment entAct) { return entAct.UserID == learner.ID; });
                            }
                            else
                            {
                                //if (entActivityAssignment != null && entActivityAssignment.Count >= i && entActivityAssignment.Count != 0)
                                //    lstActivityAssignment = entActivityAssignment.FindAll(delegate(ActivityAssignment entAct) { return entAct.UserID == learner.ID; });
                                lstActivityAssignment = entActivityAssignment;
                            }

                            string trimEmailID = learner.EmailID.Trim();
                            if (strToEmailAddress.Contains(trimEmailID))
                                pEmailDelivery.ToList = learner.EmailID.Trim();

                            if (pEmailDelivery.IsUserPreferredLanguage)
                            {
                                try
                                {
                                    pEmailDelivery.PreferredLanguageId = learner.DefaultLanguageId;
                                    //retMailMessage = SendScheduledPersonalizedMail(learner, pEmailDelivery, pEmailDelivery.AttachmentPathList, entActivityAssignment, entAssignment);
                                    retMailMessage = SendScheduledPersonalizedMail(learner, pEmailDelivery, pEmailDelivery.AttachmentPathList, lstActivityAssignment, entAssignment);
                                    if (retMailMessage == null)
                                    {
                                        retMailMessage = new MailMessage();
                                        strInvalidEmails.Append(learner.EmailID.ToString() + "<br/>");
                                        strMailAddresses.Append(learner.EmailID.ToString() + "<br/>,");
                                    }
                                    //SaveEmailSentLog(pEmailDelivery, retMailMessage);
                                }
                                catch (Exception ex)
                                {
                                    continue;
                                }
                            }
                            else if (pEmailDelivery.IsSiteDefaultLanguage)
                            {
                                try
                                {
                                    pEmailDelivery.PreferredLanguageId = _strClientLanguageId;
                                    //retMailMessage = SendScheduledPersonalizedMail(learner, pEmailDelivery, pEmailDelivery.AttachmentPathList, entActivityAssignment, entAssignment);
                                    if (lstActivityAssignment != null) //Issue No YPLS-30 Resolved - added by Atul
                                    {
                                        if (lstActivityAssignment.Count > 0)
                                        {
                                            if (lstActivityAssignment[0].AssignmentTypeId == ActivityAssignmentType.OneTimeAssignment || lstActivityAssignment[0].AssignmentTypeId == ActivityAssignmentType.DynamicAssignment)
                                                retMailMessage = SendScheduledPersonalizedAssignmentMail(learner, pEmailDelivery, pEmailDelivery.AttachmentPathList, lstActivityAssignment, entAssignment);
                                            else
                                                retMailMessage = SendScheduledPersonalizedMail(learner, pEmailDelivery, pEmailDelivery.AttachmentPathList, lstActivityAssignment, entAssignment);
                                        }
                                    }
                                    else
                                        retMailMessage = SendScheduledPersonalizedMail(learner, pEmailDelivery, pEmailDelivery.AttachmentPathList, lstActivityAssignment, entAssignment);
                                    //SaveEmailSentLog(pEmailDelivery, retMailMessage);
                                    if (retMailMessage == null)
                                    {
                                        retMailMessage = new MailMessage();
                                        strInvalidEmails.Append(learner.EmailID.ToString() + "<br/>");
                                        strMailAddresses.Append(learner.EmailID.ToString() + "<br/>,");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    continue;
                                }

                            }
                            else
                            {
                                try
                                {
                                    // retMailMessage = SendScheduledPersonalizedMail(learner, pEmailDelivery, pEmailDelivery.AttachmentPathList, entActivityAssignment, entAssignment);
                                    if (lstActivityAssignment != null) // check null value - added by Atul
                                    {
                                        if (lstActivityAssignment.Count > 0)
                                        {
                                            if (lstActivityAssignment[0].AssignmentTypeId == ActivityAssignmentType.OneTimeAssignment || lstActivityAssignment[0].AssignmentTypeId == ActivityAssignmentType.DynamicAssignment)
                                                retMailMessage = SendScheduledPersonalizedAssignmentMail(learner, pEmailDelivery, pEmailDelivery.AttachmentPathList, lstActivityAssignment, entAssignment);
                                            else
                                                retMailMessage = SendScheduledPersonalizedMail(learner, pEmailDelivery, pEmailDelivery.AttachmentPathList, lstActivityAssignment, entAssignment);
                                        }
                                    }
                                    else
                                        retMailMessage = SendScheduledPersonalizedMail(learner, pEmailDelivery, pEmailDelivery.AttachmentPathList, lstActivityAssignment, entAssignment);
                                    //SaveEmailSentLog(pEmailDelivery, retMailMessage);
                                    if (retMailMessage == null)
                                    {
                                        retMailMessage = new MailMessage();
                                        strInvalidEmails.Append(learner.EmailID.ToString() + "<br/>");
                                        strMailAddresses.Append(learner.EmailID.ToString() + "<br/>,");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    continue;

                                }
                            }

                            //Thread.Sleep(new TimeSpan(0, 0, 0, 0, MailServerToWaitAfterSend));

                            i++;
                        }

                    }

                    SaveEmailSentLog(pEmailDelivery, retMailMessage, null, null);
                }
                else
                {
                    #region Public Mail

                    StringBuilder strInvalidAddress = new StringBuilder();
                    string strInvalidIds = string.Empty;
                    foreach (Learner learner in entListLearner)
                    {
                        if (ValidationManager.ValidateString(learner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                        {
                            if (!string.IsNullOrEmpty(learner.FirstName) && !string.IsNullOrEmpty(learner.LastName))
                            {
                                string trimEmailID = learner.EmailID.Trim();
                                strEmailAddress.Append(learner.FirstName + " " + learner.LastName + "<" + trimEmailID + ">,");
                                strSystemUserGuId.Append(learner.ID + ",");
                            }
                            else
                            {
                                string trimEmailID = learner.EmailID.Trim();
                                strEmailAddress.Append(trimEmailID + ",");
                            }
                            if (pEmailDelivery.IsCCManager)
                            {
                                if (!string.IsNullOrEmpty(learner.ManagerEmailId) && ValidationManager.ValidateString(learner.ManagerEmailId.Trim(), ValidationManager.DataType.EmailID))
                                {
                                    strCCEmailAddress.Append("," + learner.ManagerEmailId.Trim() + ",");
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(learner.ManagerEmailId))
                                        LogMessage += "Invalid Email: " + learner.ManagerEmailId.Trim() + " ";
                                }
                            }
                            if (pEmailDelivery.IsBCCManager)
                            {
                                if (!string.IsNullOrEmpty(learner.ManagerEmailId) && ValidationManager.ValidateString(learner.ManagerEmailId.Trim(), ValidationManager.DataType.EmailID))
                                {
                                    strBCCEmailAddress.Append("," + learner.ManagerEmailId.Trim() + ",");
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(learner.ManagerEmailId))
                                        LogMessage += "Invalid Email: " + learner.ManagerEmailId.Trim() + " ";
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(learner.FirstName) && !string.IsNullOrEmpty(learner.LastName))
                                strInvalidAddress.Append("\"" + Convert.ToString(learner.FirstName) + " " + Convert.ToString(learner.LastName) + "\"" + " <" + Convert.ToString(learner.EmailID).Trim() + ">,");
                            else
                                strInvalidAddress.Append(Convert.ToString(learner.EmailID).Trim() + ", ");

                            strInvalidIds = strInvalidIds + learner.EmailID.Trim() + "<br/>";
                            LogMessage += "Invalid Email: " + learner.EmailID.Trim() + " ";
                        }
                    }
                    #endregion

                    //if (!string.IsNullOrEmpty(pEmailDelivery.ToList))
                    if (!string.IsNullOrEmpty(strEmailAddress.ToString()))
                    {
                        pEmailDelivery.ToList = strEmailAddress.ToString();
                    }
                    //else
                    //{
                    //    strBCCEmailAddress.Append(strEmailAddress.ToString());
                    //}


                    pEmailDelivery.CCList = Convert.ToString(strCCEmailAddress);
                    pEmailDelivery.BCCList = Convert.ToString(strBCCEmailAddress);

                    if (pEmailDelivery.IsSiteDefaultLanguage)
                    {
                        pEmailDelivery.PreferredLanguageId = _strClientLanguageId;
                        retMailMessage = SendScheduledPublicMail(pEmailDelivery, null, pEmailDelivery.AttachmentPathList);
                        SaveEmailSentLog(pEmailDelivery, retMailMessage, strInvalidAddress, strInvalidIds);
                    }
                    else
                    {
                        retMailMessage = SendScheduledPublicMail(pEmailDelivery, null, pEmailDelivery.AttachmentPathList);
                        SaveEmailSentLog(pEmailDelivery, retMailMessage, strInvalidAddress, strInvalidIds);
                    }
                    //Thread.Sleep(new TimeSpan(0, 0, 0, 0, MailServerToWaitAfterSend));
                }
            }
            else
            {
                LogMessage += "Invalid Email Delivery";
            }
        }


        private List<Learner> GetDynmicassignmentUserListByRule(EmailDeliveryDashboard entDashBoard)
        {
            List<Learner> entListLearner = null;
            EmailDeliveryDashboardManager entDashBoardMgr = new EmailDeliveryDashboardManager();
            try
            {
                entListLearner = entDashBoardMgr.GetDynamicAssignmentUserList(entDashBoard, EmailDeliveryDashboard.ListMethod.GetDynamicAssignmentUserList);
            }
            catch
            {
            }
            return entListLearner;
        }
        /// <summary>
        /// Get InScope Learners From Distrubution List
        /// </summary>
        /// <param name="pstrDistributionListId"></param>
        /// <param name="pstrSystemUserGuid"></param>
        /// <returns></returns>
        private List<Learner> GetInScopeLearnersFromDistrubutionList(string pstrDistributionListId, string pstrSystemUserGuid)
        {
            EmailDistributionList _entEmailDistributionList;
            List<BaseEntity> entListLearnerFromScope = new List<BaseEntity>();
            List<Learner> entListLearnerRet = new List<Learner>();
            Learner cCUser = new YPLMS2._0.API.Entity.Learner();
            EntityRange entRange = new EntityRange();
            EmailDistributionListManager mgrEmailDistributionList = new EmailDistributionListManager();
            try
            {
                _entEmailDistributionList = new EmailDistributionList();
                _entEmailDistributionList.ClientId = _strClientId;
                _entEmailDistributionList.ID = pstrDistributionListId;
                entRange = new EntityRange();
                entRange.RequestedById = LearnerFacade.GetRequestedById(pstrSystemUserGuid, _strClientId);
                _entEmailDistributionList.ListRange = entRange;
                _entEmailDistributionList = mgrEmailDistributionList.Execute(_entEmailDistributionList, EmailDistributionList.Method.GetRuleUsers);

                if (_entEmailDistributionList != null)
                {
                    if (_entEmailDistributionList.BusinessRuleUsers.Count > 0)
                    {
                        foreach (Learner learner in _entEmailDistributionList.BusinessRuleUsers)
                        {
                            entListLearnerRet.Add(learner);
                        }
                    }
                }
            }
            catch
            {
            }
            return entListLearnerRet;
        }

        /// <summary>
        /// Get Learners From To List
        /// </summary>
        /// <param name="pstrTo"></param>
        /// <returns></returns>
        private List<Learner> GetLearnersFromToList(string pstrTo)
        {
            List<Learner> entListLearnerRet = new List<Learner>();
            LearnerDAM mgrLearner = new LearnerDAM();
            try
            {
                MailAddressCollection mailToAddressList = GetAdresses(pstrTo);
                foreach (MailAddress mAddress in mailToAddressList)
                {
                    YPLMS2._0.API.Entity.Learner eUser = new YPLMS2._0.API.Entity.Learner();
                    eUser.ClientId = _strClientId;
                    eUser.EmailID = mAddress.Address;
                    eUser = mgrLearner.GetUserByAlias(eUser); //Execute(eUser, Learner.Method.GetUserByEmail); comment by vinod
                    if (eUser != null)
                    {
                        entListLearnerRet.Add(eUser);
                    }
                }
            }
            catch
            {
            }
            return entListLearnerRet;
        }
        // added by Gitanjali 28.7.2010
        private List<Learner> GetLearnersFromLearnerId(string pstrID)
        {
            List<Learner> entListLearnerRet = new List<Learner>();
            LearnerDAM mgrLearner = new LearnerDAM();
            string[] arrayIDs = null;
            try
            {
                pstrID = pstrID.Replace(Environment.NewLine, "");
                if (pstrID.Contains(","))
                {
                    arrayIDs = pstrID.Split(',');
                }
                else
                {
                    arrayIDs = new string[] { pstrID };
                }


                foreach (string strID in arrayIDs)
                {
                    if (!string.IsNullOrEmpty(strID))
                    {
                        try
                        {
                            Learner eUser = new Learner();
                            eUser.ClientId = _strClientId;
                            eUser.ID = strID;
                            eUser = mgrLearner.GetUserByID(eUser); //Execute(eUser, Learner.Method.Get); comment by vinod 
                            if (eUser != null)
                            {
                                entListLearnerRet.Add(eUser);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
            }
            return entListLearnerRet;
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
                //LogMessage = _expCustom.GetCustomMessage(null); comment by vinod
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
                    importDefination.ImportAction = ImportAction.Report;
                    //importDefination.ImportAction = ImportAction.None;
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

                                            // added by Gitanjali 24.01.2011 to set dateformat
                                            if (!string.IsNullOrEmpty(pLearner.PreferredDateFormat))
                                            {
                                                _strClientDateFormat = "{0:" + pLearner.PreferredDateFormat + "}";

                                            }

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
        /// <param name="pstrLanguage"></param>
        /// <param name="pLearner"></param>
        /// <param name="plistActivityAssignment"></param>
        /// <param name="plistAssignment"></param>
        /// <returns></returns>
        public string GetMailBodyFromTemplate(string pstrEmailTemplateContent, string pstrLanguage, Learner pLearner,
            List<ActivityAssignment> plistActivityAssignment, List<Assignment> plistAssignment)
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
                    importDefination.ImportAction = ImportAction.Report;
                    listImportDefination = mgrImportDefination.GetImportDefinationList(importDefination); // ExecuteDataSet(importDefination, ImportDefination.ListMethod.GetAll); comment by vinod
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


                                            if (strFieldType == ImportDefination.FieldType.Assignment.ToString() ||
                                                strFieldType == ImportDefination.FieldType.Activity.ToString() ||
                                                strFieldType == ImportDefination.FieldType.Completion.ToString())
                                            {
                                                emailTemplateContent = emailTemplateContent.Replace("&lt;%" + strEmailFieldName + "%&gt;", GetIDFieldValue(strImportDefinationID, strFieldType, pstrLanguage, plistActivityAssignment, plistAssignment));
                                            }
                                            else
                                            {
                                                emailTemplateContent = emailTemplateContent.Replace("&lt;%" + strEmailFieldName + "%&gt;", GetIDFieldValue(strImportDefinationID, strFieldType, pstrLanguage, pLearner));
                                            }
                                        }
                                        else
                                        {
                                            if (strFieldType == ImportDefination.FieldType.Assignment.ToString() ||
                                               strFieldType == ImportDefination.FieldType.Activity.ToString() ||
                                               strFieldType == ImportDefination.FieldType.Completion.ToString())
                                            {
                                                emailTemplateContent = emailTemplateContent.Replace("&lt;%" + strEmailFieldName + "%&gt;", GetIDFieldValue(strImportDefinationID, strFieldType, pstrLanguage, plistActivityAssignment, plistAssignment));
                                            }
                                            else
                                            {
                                                // updated by Gitanjali 30.08.2010
                                                if (strEmailFieldName.ToString().Contains("ManagerName"))
                                                {
                                                    emailTemplateContent = emailTemplateContent.Replace("&lt;%" + strEmailFieldName + "%&gt;", Convert.ToString(drowTemplate["ManagerName"]));
                                                }
                                                else
                                                {
                                                    emailTemplateContent = emailTemplateContent.Replace("&lt;%" + strEmailFieldName + "%&gt;", "");
                                                }
                                            }
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
                        string strPropType = string.Empty;
                        if (pstrFieldID.ToString().ToLower() == "issubscribenewsletter" || pstrFieldID.ToString().ToLower() == "istermsandcondaccepted")
                        {
                            return null;// strPropType = "System.Boolean";
                        }
                        else if (pstrFieldID.ToString().ToLower() == "subscribedate")
                            return null;//strPropType = "System.DateTime";
                        else
                            strPropType = pi.PropertyType.FullName; ;

                        if (pi.PropertyType.FullName.Contains("System.DateTime"))
                        {
                            strPropType = "System.DateTime";
                        }

                        switch (strPropType)
                        {
                            case "System.String":
                                {
                                    if (pstrFieldID == "CurrentRegionView")
                                    {
                                        strRetValue = GetRegionalView(Convert.ToString(pi.GetValue(learner, null)));
                                    }
                                    else
                                    {
                                        strRetValue = Convert.ToString(pi.GetValue(learner, null));
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
                            strRetValue = _clientmanager.GetClientAccessURL(entClient); //ExecuteForClientAccessURL(entClient, Client.Method.GetClientAccessURL); comment by vinod
                        }
                        else if (pstrFieldID != "SelfRegiRejectComments")
                        {
                            OTP _entOTP = new OTP();
                            OTPAdaptor _mgrOTP = new OTPAdaptor();
                            _entOTP.ClientId = _strClientId;
                            _entOTP.SystemUserGuid = learner.ID;
                            _entOTP = _mgrOTP.GetOTPNumber(_entOTP); // Execute(_entOTP, OTP.Method.GetOTPNumber); comment by vinod
                            strRetValue = Convert.ToString(_entOTP.OTPNumber);
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
        /// Get ID Field Value
        /// </summary>
        /// <param name="pstrFieldID"></param>
        /// <param name="pstrFieldType"></param>
        /// <param name="pstrLanguage"></param>
        /// <param name="plistActivityAssignment"></param>
        /// <param name="plistAssignment"></param>
        /// <returns></returns>
        private string GetIDFieldValue(string pstrFieldID, string pstrFieldType, string pstrLanguage,
            List<ActivityAssignment> plistActivityAssignment, List<Assignment> plistAssignment)
        {
            string strRetValue = string.Empty;
            try
            {
                #region  For Activity & Completion
                if (pstrFieldType == ImportDefination.FieldType.Activity.ToString()
                     || pstrFieldType == ImportDefination.FieldType.Completion.ToString()
                     || pstrFieldType == ImportDefination.FieldType.Assignment.ToString())
                {
                    if (plistActivityAssignment != null)
                    {
                        if (plistActivityAssignment.Count > 0)
                        {
                            foreach (ActivityAssignment activityassignment in plistActivityAssignment)
                            {
                                PropertyInfo pi = null;
                                if (pstrFieldID.ToLower() == "activityid")
                                {
                                    PropertyInfo[] pInfo = activityassignment.GetType().GetProperties();
                                    foreach (PropertyInfo p in pInfo)
                                    {
                                        if (p.Name.ToLower() == pstrFieldID.ToLower())
                                        {
                                            pi = p;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    pi = activityassignment.GetType().GetProperty(pstrFieldID);
                                }
                                string strPropType = string.Empty;
                                if (pi != null)
                                    strPropType = Convert.ToString(pi.PropertyType.FullName);
                                switch (strPropType)
                                {
                                    case "System.String":
                                        {
                                            if (!string.IsNullOrEmpty(strRetValue))
                                            {
                                                if (pi != null)
                                                    strRetValue = strRetValue + ", " + Convert.ToString(pi.GetValue(activityassignment, null));
                                            }
                                            else
                                            {
                                                if (pi != null)
                                                    strRetValue = Convert.ToString(pi.GetValue(activityassignment, null));
                                            }
                                            break;
                                        }
                                    case "System.DateTime":
                                        {
                                            string strDate = pi.GetValue(activityassignment, null).ToString();
                                            if (!string.IsNullOrEmpty(strDate))
                                            {
                                                DateTime dtPropVal = Convert.ToDateTime(strDate);
                                                if (DateTime.MinValue.CompareTo(dtPropVal) < 0)
                                                {
                                                    if (!string.IsNullOrEmpty(strRetValue))
                                                    {
                                                        if (!(strRetValue == string.Format(_strClientDateFormat, dtPropVal))) //To Check date is exists.
                                                            strRetValue = strRetValue + ", " + string.Format(_strClientDateFormat, dtPropVal);
                                                    }
                                                    else
                                                    {
                                                        strRetValue = string.Format(_strClientDateFormat, dtPropVal);
                                                    }
                                                }
                                                else
                                                {
                                                    strRetValue += "";
                                                }
                                            }

                                            break;
                                        }
                                    case "System.Boolean":
                                        {
                                            Boolean bFieldValue = (Boolean)pi.GetValue(activityassignment, null);
                                            string strValue = "0";
                                            if (bFieldValue)
                                            {
                                                strValue = "1";
                                            }
                                            LookupType lookupType = (LookupType)Enum.Parse(typeof(LookupType), pi.Name);
                                            if (!string.IsNullOrEmpty(strRetValue))
                                            {
                                                strRetValue = strRetValue + ", " + GetLookUpText(lookupType, strValue, pstrLanguage);
                                            }
                                            else
                                            {
                                                strRetValue = GetLookUpText(lookupType, strValue, pstrLanguage);
                                            }

                                            break;
                                        }
                                    // added by Gitanjali 6.8.2010
                                    case "YPLMS.Entity.ActivityContentType":
                                        {
                                            if (pi != null)
                                            {
                                                if (string.IsNullOrEmpty(strRetValue))
                                                {
                                                    strRetValue = Convert.ToString(pi.GetValue(activityassignment, null));
                                                }
                                                else
                                                {
                                                    if (Convert.ToString(pi.GetValue(activityassignment, null)) != "None")
                                                        strRetValue = strRetValue + ", " + Convert.ToString(pi.GetValue(activityassignment, null));
                                                }
                                            }
                                            else
                                                strRetValue += "";

                                            break;
                                        }
                                    default:
                                        break;
                                }
                            }
                        }
                        return strRetValue;
                    }
                }
                #endregion

                #region  For Assignment
                if (pstrFieldType == ImportDefination.FieldType.Assignment.ToString())
                {
                    if (plistAssignment != null)
                    {
                        if (plistAssignment.Count > 0)
                        {
                            foreach (Assignment assignment in plistAssignment)
                            {
                                PropertyInfo pi = assignment.GetType().GetProperty(pstrFieldID);
                                string strPropType = pi.PropertyType.FullName;
                                switch (strPropType)
                                {
                                    case "System.String":
                                        {
                                            if (!string.IsNullOrEmpty(strRetValue))
                                            {
                                                strRetValue = strRetValue + ", " + pi.GetValue(assignment, null).ToString();
                                            }
                                            else
                                            {
                                                strRetValue = pi.GetValue(assignment, null).ToString();
                                            }
                                            break;
                                        }
                                    case "System.DateTime":
                                        {
                                            string strDate = pi.GetValue(assignment, null).ToString();
                                            if (!string.IsNullOrEmpty(strDate))
                                            {
                                                DateTime dtPropVal = Convert.ToDateTime(strDate);
                                                if (DateTime.MinValue.CompareTo(dtPropVal) < 0)
                                                {
                                                    if (!string.IsNullOrEmpty(strRetValue))
                                                    {
                                                        strRetValue = strRetValue + ", " + string.Format(_strClientDateFormat, dtPropVal);
                                                    }
                                                    else
                                                    {
                                                        strRetValue = string.Format(_strClientDateFormat, dtPropVal);
                                                    }
                                                }
                                                else
                                                {
                                                    strRetValue += "";
                                                }
                                            }

                                            break;
                                        }
                                    case "System.Boolean":
                                        {
                                            Boolean bFieldValue = (Boolean)pi.GetValue(assignment, null);
                                            string strValue = "0";
                                            if (bFieldValue)
                                            {
                                                strValue = "1";
                                            }
                                            LookupType lookupType = (LookupType)Enum.Parse(typeof(LookupType), pi.Name);
                                            if (!string.IsNullOrEmpty(strRetValue))
                                            {
                                                strRetValue = strRetValue + ", " + GetLookUpText(lookupType, strValue, pstrLanguage);
                                            }
                                            else
                                            {
                                                strRetValue = GetLookUpText(lookupType, strValue, pstrLanguage);
                                            }

                                            break;
                                        }
                                    default:
                                        break;
                                }
                            }
                        }
                        return strRetValue;
                    }
                }

                #endregion
            }
            catch
            {
                return strRetValue;
            }
            return strRetValue;
        }

        /// <summary>
        /// Save Email Sent Log
        /// </summary>
        /// <param name="pEmailDelivery">Email Delivery</param>
        /// <param name="pMailMessage">Mail Message</param>
        private void SaveEmailSentLog(EmailDeliveryDashboard pEmailDelivery, MailMessage pMailMessage, StringBuilder strInValidAddress, string pStrInvlidEmailID)
        {
            //if (pMailMessage != null)
            //{
            EmailSentLog emailsentlog = new EmailSentLog();
            EmailSentLogManager mgrEmailSentLog = new EmailSentLogManager();
            emailsentlog.ClientId = _strClientId;
            emailsentlog.EmailDeliveryInstanceId = pEmailDelivery.ID;
            emailsentlog.EmailTemplateId = pEmailDelivery.EmailTemplateID;
            emailsentlog.DateSent = DateTime.Now.ToUniversalTime();

            //emailsentlog.RecipiantEmailAddress = pMailMessage.To.ToString();
            if (strInValidAddress != null)
                emailsentlog.RecipiantEmailAddress = Convert.ToString(strMailAddresses) + strInValidAddress.ToString();
            else
                emailsentlog.RecipiantEmailAddress = Convert.ToString(strMailAddresses);

            if (pMailMessage != null)
            {
                emailsCount = emailsCount + pMailMessage.CC.Count + pMailMessage.Bcc.Count;
            }


            strLog.Append(DateTime.Now.ToUniversalTime() + " Sent to " + emailsCount + " Recipient(s)");
            strLog.Append("<br/>");
            strLog.Append(DateTime.Now.ToUniversalTime() + " Close Log");
            strLog.Append("<br/><br/>");

            if (!string.IsNullOrEmpty(pStrInvlidEmailID))
                strLog.Append("<b>Invalid Email Addresses:</b><br/>" + strInvalidEmails + pStrInvlidEmailID);
            else
                strLog.Append("<b>Invalid Email Addresses:</b><br/>" + strInvalidEmails);

            emailsentlog.EmailLog = Convert.ToString(strLog);

            if (strSystemUserGuId.Length > 0)
            {
                emailsentlog.SystemUserGuId = strSystemUserGuId.ToString();
            }
            if (pMailMessage != null)
            {
                emailsentlog.RecipiantCCEmailAddress = pMailMessage.CC.ToString();
                emailsentlog.RecipiantBCCEmailAddress = pMailMessage.Bcc.ToString();
            }
            //emailsentlog.RecipiantCCEmailAddress = pMailMessage.CC.ToString();
            //emailsentlog.RecipiantBCCEmailAddress = pMailMessage.Bcc.ToString();
            emailsentlog = mgrEmailSentLog.Execute(emailsentlog, EmailSentLog.Method.Add);
            UpdateEmailDeliveryStatus(pEmailDelivery);
            // }
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
                    strLog.Append(DateTime.Now.ToUniversalTime() + " Sent to " + emailsCount + " Recipient(s)");
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

        /// <summary>
        /// Update Email Delivery Status
        /// </summary>
        /// <param name="pEmailDelivery"></param>       
        private void UpdateEmailDeliveryStatus(EmailDeliveryDashboard pEmailDelivery)
        {
            EmailDeliveryDashboardManager mgrEmailDeliveryDashboard = new EmailDeliveryDashboardManager();
            EmailDeliveryDashboard pEmailDeliveryGet = null;
            try
            {
                if (pEmailDelivery != null && !string.IsNullOrEmpty(pEmailDelivery.ID))
                {
                    pEmailDeliveryGet = mgrEmailDeliveryDashboard.Execute(pEmailDelivery, EmailDeliveryDashboard.Method.Get);
                }

                if (pEmailDeliveryGet != null)
                {
                    pEmailDeliveryGet.ClientId = _strClientId;
                    pEmailDeliveryGet.IsInProcess = false;
                    if ((pEmailDeliveryGet.IsWeekly || pEmailDeliveryGet.IsMonthly || pEmailDeliveryGet.IsDaily))
                    {
                        if (pEmailDeliveryGet.IsRecurrenceApprovalRequired)
                        {
                            pEmailDeliveryGet.DeliveryApprovalStatus = YPLMS.Services.Common.GetDescription(EmailDeliveryDashboard.ApprovalStatus.PendingApproval);
                        }
                        else
                        {
                            pEmailDeliveryGet.DeliveryApprovalStatus = YPLMS.Services.Common.GetDescription(EmailDeliveryDashboard.ApprovalStatus.Approved);
                        }
                    }
                    else
                    {
                        pEmailDeliveryGet.DeliveryApprovalStatus = YPLMS.Services.Common.GetDescription(EmailDeliveryDashboard.ApprovalStatus.EmailSent);
                    }

                    pEmailDeliveryGet = mgrEmailDeliveryDashboard.Execute(pEmailDeliveryGet, EmailDeliveryDashboard.Method.Update);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Add email information to Dashboard
        /// </summary>
        /// <param name="pEmailDelivery">Email Infor</param>
        /// <param name="pLanguageId">Language ID</param>
        /// <param name="pListAttachments">Attachment List</param>
        /// <param name="pCreatedById">Created by Id</param>
        /// <returns></returns>
        private bool AddEmailToDashBoard(EmailDeliveryDashboard pEmailDelivery, List<Attachment> pListAttachments)
        {
            bool retValue = false;
            EmailDeliveryDashboardManager mgrEmailDeliveryDashboard = new EmailDeliveryDashboardManager();
            EmailDeliveryDashboard emaildeliverydashboard = new EmailDeliveryDashboard();
            string createdById = pEmailDelivery.CreatedById;
            //if (string.IsNullOrEmpty(createdById) && LMSSession.IsInSession(Client.CLIENT_SESSION_ID) && LMSSession.IsInSession(Learner.USER_SESSION_ID))
            //{
            //    createdById = ((Learner)LMSSession.GetValue(Learner.USER_SESSION_ID)).ID;  comment by vinod
            //}
            if (!string.IsNullOrEmpty(createdById))
            {
                emaildeliverydashboard.ClientId = _strClientId;
                if (!string.IsNullOrEmpty(pEmailDelivery.EmailDeliveryTitle))
                {
                    emaildeliverydashboard.EmailDeliveryTitle = pEmailDelivery.EmailDeliveryTitle;
                }
                else
                {
                    emaildeliverydashboard.EmailDeliveryTitle = "Auto Email";
                }
                if (!string.IsNullOrEmpty(pEmailDelivery.EmailTemplateID))
                {
                    emaildeliverydashboard.EmailTemplateID = pEmailDelivery.EmailTemplateID;
                }
                else if (pEmailDelivery.EmailTemplate != null)
                {
                    emaildeliverydashboard.EmailTemplateID = pEmailDelivery.EmailTemplate.ID;
                }

                emaildeliverydashboard.ToList = pEmailDelivery.ToList;
                emaildeliverydashboard.CCList = pEmailDelivery.CCList;
                emaildeliverydashboard.BCCList = pEmailDelivery.BCCList;
                emaildeliverydashboard.IsCCManager = false;
                emaildeliverydashboard.IsBCCManager = false;
                emaildeliverydashboard.IsImmediate = true;
                emaildeliverydashboard.DateTimeSet = DateTime.Now.ToUniversalTime();
                if (!string.IsNullOrEmpty(pEmailDelivery.PreferredLanguageId) || pEmailDelivery.IsPersonalized)
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
                emaildeliverydashboard.IsPersonalized = pEmailDelivery.IsPersonalized;
                if (pListAttachments != null)
                {
                    if (pListAttachments.Count > 0)
                    {
                        emaildeliverydashboard.AttachmentPathList = GetAttachmentsPath(pListAttachments);
                    }
                }
                emaildeliverydashboard.DeliveryApprovalStatus = YPLMS.Services.Common.GetDescription(EmailDeliveryDashboard.ApprovalStatus.PendingApproval);
                emaildeliverydashboard.CreatedById = createdById;
                emaildeliverydashboard.LastModifiedById = createdById;
                //added by Gitanjali 28.7.2010
                emaildeliverydashboard.LearnerId = pEmailDelivery.LearnerId;
                emaildeliverydashboard = mgrEmailDeliveryDashboard.Execute(emaildeliverydashboard, EmailDeliveryDashboard.Method.Add);
                if (emaildeliverydashboard != null)
                {
                    retValue = true;
                }
            }
            else
            {
                retValue = false;
            }
            return retValue;
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
        /// <param name="pstrUserID"></param>
        /// <returns></returns>
        private bool AddEmailToDashBoard(EmailDeliveryDashboard pEmailDelivery, List<Attachment> pListAttachments,
            List<ActivityAssignment> plistActivityAssignment, List<Assignment> plistAssignment)
        {
            bool retValue = false;
            EmailDeliveryDashboardManager mgrEmailDeliveryDashboard = new EmailDeliveryDashboardManager();
            EmailDeliveryDashboard emaildeliverydashboard = new EmailDeliveryDashboard();
            string createdById = pEmailDelivery.CreatedById;
            //if (string.IsNullOrEmpty(createdById) && LMSSession.IsInSession(Client.CLIENT_SESSION_ID) && LMSSession.IsInSession(Learner.USER_SESSION_ID))
            //{
            //    createdById = ((Learner)LMSSession.GetValue(Learner.USER_SESSION_ID)).ID; comment by vinod
            //}
            if (!string.IsNullOrEmpty(createdById))
            {
                emaildeliverydashboard.ClientId = _strClientId;
                if (!string.IsNullOrEmpty(pEmailDelivery.EmailDeliveryTitle))
                {
                    emaildeliverydashboard.EmailDeliveryTitle = pEmailDelivery.EmailDeliveryTitle;
                }
                else
                {
                    emaildeliverydashboard.EmailDeliveryTitle = "Auto Email";
                }
                if (!string.IsNullOrEmpty(pEmailDelivery.EmailTemplateID))
                {
                    emaildeliverydashboard.EmailTemplateID = pEmailDelivery.EmailTemplateID;
                }
                else if (pEmailDelivery.EmailTemplate != null)
                {
                    emaildeliverydashboard.EmailTemplateID = pEmailDelivery.EmailTemplate.ID;
                }
                emaildeliverydashboard.ToList = pEmailDelivery.ToList;
                emaildeliverydashboard.CCList = pEmailDelivery.CCList;
                emaildeliverydashboard.BCCList = pEmailDelivery.BCCList;
                //Added by mahesh for Dynamic assignment email 
                emaildeliverydashboard.RuleId = pEmailDelivery.RuleId;
                emaildeliverydashboard.AssignmentTypeID = pEmailDelivery.AssignmentTypeID;

                emaildeliverydashboard.IsCCManager = false;
                emaildeliverydashboard.IsBCCManager = false;
                emaildeliverydashboard.IsImmediate = true;
                emaildeliverydashboard.DateTimeSet = DateTime.Now.ToUniversalTime();

                if (!string.IsNullOrEmpty(pEmailDelivery.PreferredLanguageId) || pEmailDelivery.IsPersonalized)
                {
                    emaildeliverydashboard.IsUserPreferredLanguage = true;
                    emaildeliverydashboard.IsSiteDefaultLanguage = false;
                }
                else
                {
                    emaildeliverydashboard.IsUserPreferredLanguage = false;
                    emaildeliverydashboard.IsSiteDefaultLanguage = true;

                }
                emaildeliverydashboard.IsAllLanguages = false;
                if (plistActivityAssignment != null && plistActivityAssignment.Count > 0)
                {
                    string strActivityIDs = string.Empty;
                    foreach (ActivityAssignment activityAssignment in plistActivityAssignment)
                    {
                        if (!string.IsNullOrEmpty(activityAssignment.ID))
                        {
                            strActivityIDs = strActivityIDs + activityAssignment.ID + ",";
                        }

                    }
                    if (!string.IsNullOrEmpty(strActivityIDs))
                    {
                        strActivityIDs = strActivityIDs.Substring(0, strActivityIDs.LastIndexOf(","));
                        emaildeliverydashboard.ActivityId = strActivityIDs;
                    }
                }
                if (plistAssignment != null && plistAssignment.Count > 0)
                {
                    string strAssignmentIDs = string.Empty;
                    foreach (Assignment assignment in plistAssignment)
                    {
                        if (!string.IsNullOrEmpty(assignment.ID))
                        {
                            strAssignmentIDs = strAssignmentIDs + assignment.ID + ",";
                        }
                    }

                    if (!string.IsNullOrEmpty(strAssignmentIDs))
                    {
                        strAssignmentIDs = strAssignmentIDs.Substring(0, strAssignmentIDs.LastIndexOf(","));
                        emaildeliverydashboard.AssignmentId = strAssignmentIDs;
                    }
                }
                emaildeliverydashboard.IsPersonalized = pEmailDelivery.IsPersonalized;
                if (pListAttachments != null)
                {
                    if (pListAttachments.Count > 0)
                    {
                        emaildeliverydashboard.AttachmentPathList = GetAttachmentsPath(pListAttachments);
                    }
                }
                emaildeliverydashboard.DeliveryApprovalStatus = YPLMS.Services.Common.GetDescription(EmailDeliveryDashboard.ApprovalStatus.PendingApproval);
                emaildeliverydashboard.CreatedById = createdById;
                emaildeliverydashboard.LastModifiedById = createdById;
                emaildeliverydashboard.LearnerId = pEmailDelivery.LearnerId;
                emaildeliverydashboard.AssignmentMode = pEmailDelivery.AssignmentMode;
                emaildeliverydashboard = mgrEmailDeliveryDashboard.Execute(emaildeliverydashboard, EmailDeliveryDashboard.Method.Add);
                if (emaildeliverydashboard != null)
                {
                    retValue = true;
                }

            }
            else
            {
                retValue = false;
            }
            return retValue;
        }

        private bool AddEmailToDashBoardOutlook(EmailDeliveryDashboard pEmailDelivery, List<Attachment> pListAttachment,
           List<ActivityAssignment> plistActivityAssignment, List<Assignment> plistAssignment)
        {
            bool retValue = false;
            EmailDeliveryDashboardManager mgrEmailDeliveryDashboard = new EmailDeliveryDashboardManager();
            EmailDeliveryDashboard emaildeliverydashboard = new EmailDeliveryDashboard();
            string createdById = pEmailDelivery.CreatedById;
            int iCount = 0;
            //if (string.IsNullOrEmpty(createdById) && LMSSession.IsInSession(Client.CLIENT_SESSION_ID) && LMSSession.IsInSession(Learner.USER_SESSION_ID))
            //{
            //    createdById = ((Learner)LMSSession.GetValue(Learner.USER_SESSION_ID)).ID; comment by vinod
            //}
            if (!string.IsNullOrEmpty(createdById))
            {
                emaildeliverydashboard.ClientId = _strClientId;
                if (!string.IsNullOrEmpty(pEmailDelivery.EmailDeliveryTitle))
                {
                    emaildeliverydashboard.EmailDeliveryTitle = pEmailDelivery.EmailDeliveryTitle;
                }
                else
                {
                    emaildeliverydashboard.EmailDeliveryTitle = "Auto Email";
                }
                if (!string.IsNullOrEmpty(pEmailDelivery.EmailTemplateID))
                {
                    emaildeliverydashboard.EmailTemplateID = pEmailDelivery.EmailTemplateID;
                }
                else if (pEmailDelivery.EmailTemplate != null)
                {
                    emaildeliverydashboard.EmailTemplateID = pEmailDelivery.EmailTemplate.ID;
                }
                emaildeliverydashboard.ToList = pEmailDelivery.ToList;
                emaildeliverydashboard.CCList = pEmailDelivery.CCList;
                emaildeliverydashboard.BCCList = pEmailDelivery.BCCList;
                //Added by mahesh for Dynamic assignment email 
                emaildeliverydashboard.RuleId = pEmailDelivery.RuleId;
                emaildeliverydashboard.AssignmentTypeID = pEmailDelivery.AssignmentTypeID;

                emaildeliverydashboard.IsCCManager = false;
                emaildeliverydashboard.IsBCCManager = false;
                emaildeliverydashboard.IsImmediate = true;
                emaildeliverydashboard.DateTimeSet = DateTime.Now.ToUniversalTime();

                if (!string.IsNullOrEmpty(pEmailDelivery.PreferredLanguageId) || pEmailDelivery.IsPersonalized)
                {
                    emaildeliverydashboard.IsUserPreferredLanguage = true;
                    emaildeliverydashboard.IsSiteDefaultLanguage = false;
                }
                else
                {
                    emaildeliverydashboard.IsUserPreferredLanguage = false;
                    emaildeliverydashboard.IsSiteDefaultLanguage = true;

                }
                emaildeliverydashboard.IsAllLanguages = false;
                if (plistActivityAssignment != null && plistActivityAssignment.Count > 0)
                {
                    string strActivityIDs = string.Empty;
                    foreach (ActivityAssignment activityAssignment in plistActivityAssignment)
                    {
                        if (!string.IsNullOrEmpty(activityAssignment.ID))
                        {
                            strActivityIDs = strActivityIDs + activityAssignment.ID + ",";
                        }

                    }
                    if (!string.IsNullOrEmpty(strActivityIDs))
                    {
                        strActivityIDs = strActivityIDs.Substring(0, strActivityIDs.LastIndexOf(","));
                        emaildeliverydashboard.ActivityId = strActivityIDs;
                    }
                }
                if (plistAssignment != null && plistAssignment.Count > 0)
                {
                    string strAssignmentIDs = string.Empty;
                    foreach (Assignment assignment in plistAssignment)
                    {
                        if (!string.IsNullOrEmpty(assignment.ID))
                        {
                            strAssignmentIDs = strAssignmentIDs + assignment.ID + ",";
                        }
                    }

                    if (!string.IsNullOrEmpty(strAssignmentIDs))
                    {
                        strAssignmentIDs = strAssignmentIDs.Substring(0, strAssignmentIDs.LastIndexOf(","));
                        emaildeliverydashboard.AssignmentId = strAssignmentIDs;
                    }
                }
                emaildeliverydashboard.IsPersonalized = pEmailDelivery.IsPersonalized;
                if (pListAttachment != null)
                {
                    //if (pListAttachment.Count > 0)
                    //{
                    foreach (Attachment attachment in pListAttachment)
                    {
                        if (iCount == 0)
                            emaildeliverydashboard.AttachmentPathList = ((System.IO.FileStream)pListAttachment[iCount].ContentStream).Name.ToString();// GetAttachmentsPath(pListAttachments);
                        else
                            emaildeliverydashboard.AttachmentPathList += "," + ((System.IO.FileStream)pListAttachment[iCount].ContentStream).Name.ToString();// GetAttachmentsPath(pListAttachments);
                        iCount++;
                    }
                    //}
                }

                emaildeliverydashboard.DeliveryApprovalStatus = YPLMS.Services.Common.GetDescription(EmailDeliveryDashboard.ApprovalStatus.PendingApproval);
                emaildeliverydashboard.CreatedById = createdById;
                emaildeliverydashboard.LastModifiedById = createdById;
                emaildeliverydashboard.LearnerId = pEmailDelivery.LearnerId;
                emaildeliverydashboard.AssignmentMode = pEmailDelivery.AssignmentMode;
                emaildeliverydashboard = mgrEmailDeliveryDashboard.Execute(emaildeliverydashboard, EmailDeliveryDashboard.Method.Add);
                if (emaildeliverydashboard != null)
                {
                    retValue = true;
                }

            }
            else
            {
                retValue = false;
            }
            return retValue;
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
                            if (ValidationManager.ValidateString(learner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                            {
                                string trimEmailID = learner.EmailID.Trim();
                                strEmailAddress.Append(trimEmailID + ",");
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
        /// Get Attachments
        /// </summary>
        /// <param name="pstrAttachmentsPath"></param>
        /// <returns></returns>
        public List<Attachment> GetAttachments(string pstrAttachmentsPath)
        {
            List<Attachment> listAttachments = new List<Attachment>();
            string[] arrayPath = null;
            try
            {
                pstrAttachmentsPath = pstrAttachmentsPath.Replace(Environment.NewLine, "");
                if (pstrAttachmentsPath.Contains(","))
                {
                    arrayPath = pstrAttachmentsPath.Split(',');

                    foreach (string path in arrayPath)
                    {
                        if (!string.IsNullOrEmpty(path))
                        {
                            listAttachments.Add(new Attachment(path));
                        }
                    }
                }
                else
                {
                    listAttachments.Add(new Attachment(pstrAttachmentsPath));
                }
            }
            catch
            {
            }
            return listAttachments;
        }

        /// <summary>
        /// Get Activity Assignment List
        /// </summary>
        /// <param name="pstrID"></param>
        /// <returns></returns>
        private List<ActivityAssignment> GetActivityAssignmentList(string pstrID, List<Learner> lstLearner)
        {
            string[] arrayIDs = null;
            List<ActivityAssignment> listListActivityAssignment = new List<ActivityAssignment>();
            ActivityAssignmentAdaptor mgrActivityAssignment = new ActivityAssignmentAdaptor();
            ActivityAssignment entEntity;
            try
            {
                pstrID = pstrID.Replace(Environment.NewLine, "");
                if (pstrID.Contains(","))
                {
                    arrayIDs = pstrID.Split(',');
                }
                else
                {
                    arrayIDs = new string[] { pstrID };
                }

                int learnerCounter = 0;

                foreach (string strID in arrayIDs)
                {
                    if (!string.IsNullOrEmpty(strID))
                    {
                        try
                        {

                            entEntity = new ActivityAssignment();
                            entEntity.ClientId = _strClientId;
                            entEntity.ID = strID;
                            // updated by Gitanjali 23.7.2010

                            //Commented by Prajakta on 06 Aug 2014
                            //if (lstLearner.Count >= learnerCounter)
                            if (lstLearner.Count > learnerCounter)
                                entEntity.UserID = lstLearner[learnerCounter].ID;
                            else
                                entEntity.UserID = lstLearner[0].ID;
                            entEntity = mgrActivityAssignment.GetActivityAssignmentByID(entEntity); //  comment by vinod  Execute(entEntity, ActivityAssignment.Method.Get); //it did not return assignemtn details // mgrActivityAssignment.Execute(entEntity, ActivityAssignment.Method.Get_UnAssignment);

                            if (entEntity != null)
                            {
                                if (!string.IsNullOrEmpty(entEntity.ID))
                                {
                                    listListActivityAssignment.Add(entEntity);
                                }
                            }
                        }
                        catch
                        {
                        }
                        learnerCounter++;

                    }
                }
            }
            catch
            {
            }
            return listListActivityAssignment;
        }

        private List<ActivityAssignment> GetActivityAssignmentListForBulkImport(string pstrID, List<Learner> lstLearner)
        {
            string[] arrayIDs = null;
            List<ActivityAssignment> listListActivityAssignment = new List<ActivityAssignment>();
            ActivityAssignmentAdaptor mgrActivityAssignment = new ActivityAssignmentAdaptor();
            ActivityAssignment entEntity;
            try
            {
                pstrID = pstrID.Replace(Environment.NewLine, "");
                if (pstrID.Contains(","))
                {
                    arrayIDs = pstrID.Split(',');
                }
                else
                {
                    arrayIDs = new string[] { pstrID };
                }

                int learnerCounter = 0;

                foreach (string strID in arrayIDs)
                {
                    if (!string.IsNullOrEmpty(strID))
                    {
                        try
                        {

                            entEntity = new ActivityAssignment();
                            entEntity.ClientId = _strClientId;
                            entEntity.ID = strID;
                            // updated by Gitanjali 23.7.2010

                            //Commented by Prajakta on 06 Aug 2014
                            //if (lstLearner.Count >= learnerCounter)
                            if (lstLearner.Count > learnerCounter)
                                entEntity.UserID = lstLearner[learnerCounter].ID;
                            else
                                entEntity.UserID = lstLearner[0].ID;
                            entEntity = mgrActivityAssignment.GetUserActivity_ForBulkImport(entEntity); // comment by vinod  Execute(entEntity, ActivityAssignment.Method.Get_AssignmentForBulkImport); //it did not return assignemtn details // mgrActivityAssignment.Execute(entEntity, ActivityAssignment.Method.Get_UnAssignment);

                            if (entEntity != null)
                            {
                                if (!string.IsNullOrEmpty(entEntity.ID))
                                {
                                    listListActivityAssignment.Add(entEntity);
                                }
                            }
                        }
                        catch
                        {
                        }
                        learnerCounter++;

                    }
                }
            }
            catch
            {
            }
            return listListActivityAssignment;
        }
        /// <summary>
        /// Get Assignment List
        /// </summary>
        /// <param name="pstrID"></param>
        /// <returns></returns>
        private List<Assignment> GetAssignmentList(string pstrID)
        {
            string[] arrayIDs = null;
            List<Assignment> listListAssignment = new List<Assignment>();
            AssignmentAdaptor mgrAssignment = new AssignmentAdaptor();
            Assignment entEntity;
            try
            {
                pstrID = pstrID.Replace(Environment.NewLine, "");
                if (pstrID.Contains(","))
                {
                    arrayIDs = pstrID.Split(',');
                }
                else
                {
                    arrayIDs = new string[] { pstrID };
                }
                foreach (string strID in arrayIDs)
                {
                    if (!string.IsNullOrEmpty(strID))
                    {
                        try
                        {
                            entEntity = new Assignment();
                            entEntity.ClientId = _strClientId;
                            entEntity.ID = strID;
                            entEntity = mgrAssignment.GetAssignmentByID(entEntity); //Execute(entEntity, Assignment.Method.Get); comment by vinod
                            if (entEntity != null)
                            {
                                if (!string.IsNullOrEmpty(entEntity.ID))
                                {
                                    listListAssignment.Add(entEntity);
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
            }
            return listListAssignment;
        }

        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="pMailMessage"></param>
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
                        //pMailMessage.To.Add(pMailMessage.From);
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
                //SMTPServer.EnableSsl = true;
                SMTPServer.Send(pMailMessage);
                isEmailSent = true;

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
                        //pMailMessage.To.Add(pMailMessage.From);
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
                entRegView = mgrRegionView.GetRegionViewById(entRegView); //Execute(entRegView, RegionView.Method.Get); comment by vinod 
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
        // added by Gitanjali 27.08.2010
        private MailMessage SendScheduledMailToManager(EmailDeliveryDashboard pEmailInfo, string pAttachments)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            string strFromDispalyName = string.Empty;
            EmailTemplateLanguage userMailTemplate = null;
            EmailTemplate entEmailTemplate;
            try
            {
                entEmailTemplate = GetEmailTemplate(pEmailInfo);
                //Add To
                if (!ValidationManager.ValidateString(pEmailInfo.ToList, ValidationManager.DataType.EmailID))
                {
                    LogMessage += "Invalid Email: " + pEmailInfo.ToList + " ";
                    return null;
                }
                if (entEmailTemplate != null)
                {
                    if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                    {
                        userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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
                            _mMailMessage.Subject = GetMailBodyFromTemplateForManager(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, null);
                            _mMailMessage.Body = GetMailBodyFromTemplateForManager(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, pEmailInfo.ManagerName);

                        }
                        else
                        {
                            LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                            return null;
                        }
                    }
                    else
                    {
                        foreach (EmailTemplateLanguage emailtemplatelang in entEmailTemplate.EmailTemplateLanguage)
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
                                _mMailMessage.Subject += GetMailBodyFromTemplate(emailtemplatelang.EmailSubjectText, emailtemplatelang.LanguageId, null, null, null) + " ";
                                _mMailMessage.Body += GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, null, null, null) + "<br/> ";

                            }
                        }
                    }
                }
                else
                {
                    LogMessage = MessageAdaptor.GetMessage(YPLMS.Services.Messages.EmailTemplate.NOT_APPROVED);
                    return null;
                }
                //Add To
                if (!string.IsNullOrEmpty(pEmailInfo.ToList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailReplyToId))
                {
                    _mMailMessage.ReplyTo = new MailAddress(entEmailTemplate.EmailReplyToId);
                }
                //Add From 
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailFromId))
                {

                    if (string.IsNullOrEmpty(strFromDispalyName))
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                    }
                    else
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId, strFromDispalyName);
                    }
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
                    {
                        _mMailMessage.Bcc.Add(address);
                    }
                }
                //Add Attachments
                if (!string.IsNullOrEmpty(pAttachments))
                {
                    List<Attachment> listAttachments = GetAttachments(pAttachments);
                    if (listAttachments != null)
                    {
                        if (listAttachments.Count > 0)
                        {
                            foreach (Attachment attachments in listAttachments)
                            {
                                _mMailMessage.Attachments.Add(attachments);
                            }
                        }
                    }
                }

                strMailAddresses.Append(SendEmailAddresses(_mMailMessage));
                strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                //SendEmail(_mMailMessage);
            }
            catch (SmtpException ex)
            {
                CustomException objCustEx = new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, ex, true);
                throw;
            }
            catch (Exception expCommon)
            {
                CustomException objCustEx = new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw;
            }


            return _mMailMessage;
        }

        // added by Gitanjali 30.08.2010 to replace manager name in email template
        public string GetMailBodyFromTemplateForManager(string pstrEmailTemplateContent, string pstrLanguage, string pManagerName)
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
                    importDefination.ImportAction = ImportAction.Report;
                    listImportDefination = mgrImportDefination.GetImportDefinationList(importDefination); //ExecuteDataSet(importDefination, ImportDefination.ListMethod.GetAll); comment by vinod
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

                                        if (strEmailFieldName.ToString().Contains("ManagerName") && !string.IsNullOrEmpty(pManagerName))
                                        {
                                            emailTemplateContent = emailTemplateContent.Replace("&lt;%" + strEmailFieldName + "%&gt;", pManagerName.ToString());
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

        // added by Abhay 29.03.2011 to solved issue 175
        /// <summary>
        /// Send mail to a Learner
        /// </summary>
        /// <param name="pLearner">Learner Object with specific language id</param>        
        /// <param name="pEmailInfo">EmailDeliveryDashboard Information</param>
        /// <param name="pstrAdditionalEmailBody">Additional body text to attached</param>
        /// <param name="pLanguageId">Specific language id</param>
        /// <param name="pListAttachments">List of attachements</param>
        /// <returns></returns>
        public bool SendPersonalizedMailFeedback(Learner pLearner, EmailDeliveryDashboard pEmailInfo, string pstrAdditionalEmailBody, string pstrEmailID, string pstrName,
                                        List<Attachment> pListAttachments)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            EmailTemplate entEmailTemplate;
            string strFromDispalyName = string.Empty;
            try
            {

                entEmailTemplate = GetEmailTemplate(pEmailInfo);
                if (pEmailInfo.AddToDashboard)
                {
                    pEmailInfo.IsPersonalized = true;
                    return AddEmailToDashBoard(pEmailInfo, pListAttachments);
                }
                //Add To
                if (!ValidationManager.ValidateString(pLearner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                {
                    LogMessage += "Invalid Email: " + pLearner.EmailID.Trim() + " ";
                    return false;
                }
                if (entEmailTemplate != null)
                {
                    if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                    {
                        userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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
                            _mMailMessage.Subject = GetMailBodyFromTemplate(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, pLearner);
                            _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, pLearner);

                            List<UserPageElementLanguage> entListElements = new List<UserPageElementLanguage>();
                            entListElements = GetPageElements(pLearner.ClientId, "Pg00007", pLearner.DefaultLanguageId);
                            if (entListElements != null && entListElements.Count > 0)
                            {
                                if (!string.IsNullOrEmpty(pstrName))
                                {
                                    _mMailMessage.Body += "<b>" + GetPageElementsText(entListElements, "learnername") + "</b> " + pstrName + "<br><br>";
                                }
                                if (!string.IsNullOrEmpty(pstrEmailID))
                                {
                                    _mMailMessage.Body += "<b>" + GetPageElementsText(entListElements, "learneremail") + "</b> " + pstrEmailID.Trim() + "<br><br>";
                                }
                                if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                {
                                    _mMailMessage.Body += "<b>" + GetPageElementsText(entListElements, "feedback") + ":</b> " + pstrAdditionalEmailBody;
                                }
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
                        foreach (EmailTemplateLanguage emailtemplatelang in entEmailTemplate.EmailTemplateLanguage)
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
                                _mMailMessage.Subject += GetMailBodyFromTemplate(emailtemplatelang.EmailSubjectText, emailtemplatelang.LanguageId, pLearner) + " ";
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
                if (string.IsNullOrEmpty(pEmailInfo.ToList))
                {
                    if (!string.IsNullOrEmpty(pLearner.FirstName) && !string.IsNullOrEmpty(pLearner.LastName))
                    {
                        _mMailMessage.To.Add(new MailAddress(pLearner.EmailID.Trim(), pLearner.FirstName + " " + pLearner.LastName));
                    }
                    else
                    {
                        _mMailMessage.To.Add(new MailAddress(pLearner.EmailID.Trim()));
                    }
                }
                else
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailReplyToId))
                {
                    _mMailMessage.ReplyTo = new MailAddress(entEmailTemplate.EmailReplyToId);
                }
                //Add From
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailFromId))
                {
                    if (string.IsNullOrEmpty(userMailTemplate.DisplayName))
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                    }
                    else
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId, strFromDispalyName);
                    }
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
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

                    SaveEmailSentLog(entEmailTemplate.ID, _mMailMessage);

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
                            SaveEmailSentLog(entEmailTemplate.ID, _mMailMessage);
                        }
                        else
                        {

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
        // added by Abhay 29.03.2011 to solved issue 175
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
        public bool SendPublicMailFeedback(EmailDeliveryDashboard pEmailInfo, string pstrAdditionalEmailBody, string pstrEmailID, string pstrName, List<Attachment> pListAttachments)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;

            EmailTemplateLanguage userMailTemplate = null;
            EmailTemplate entEmailTemplate;
            string strFromDispalyName = string.Empty;
            try
            {
                entEmailTemplate = GetEmailTemplate(pEmailInfo);
                if (pEmailInfo.AddToDashboard)
                {
                    pEmailInfo.IsPersonalized = false;
                    return AddEmailToDashBoard(pEmailInfo, pListAttachments);
                }


                if (entEmailTemplate != null)
                {
                    if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                    {
                        userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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
                            if (!string.IsNullOrEmpty(pstrName))
                            {
                                _mMailMessage.Body += "<b>Learner Name:</b> " + pstrName + "<br><br>";
                            }
                            if (!string.IsNullOrEmpty(pstrEmailID))
                            {
                                _mMailMessage.Body += "<b>Learner Email:</b> " + pstrEmailID.Trim() + "<br><br>";
                            }
                            if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                            {
                                _mMailMessage.Body += "<b>Feedback:</b> " + pstrAdditionalEmailBody;
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
                        if (entEmailTemplate.EmailTemplateLanguage.Count > 0)
                        {
                            foreach (EmailTemplateLanguage emailtemplatelang in entEmailTemplate.EmailTemplateLanguage)
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
                        else
                        {
                            EmailTemplate eTemp = pEmailInfo.EmailTemplate;
                            if (eTemp != null)
                            {
                                _mMailMessage.Subject += eTemp.EmailSubjectText + " ";
                                _mMailMessage.Body += eTemp.EmailBodyText + "<br/> ";
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
                if (!string.IsNullOrEmpty(pEmailInfo.ToList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailReplyToId))
                {
                    _mMailMessage.ReplyTo = new MailAddress(entEmailTemplate.EmailReplyToId);
                }
                //Add From 
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailFromId))
                {
                    if (userMailTemplate != null)
                    {
                        if (string.IsNullOrEmpty(userMailTemplate.DisplayName))
                        {
                            _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                        }
                        else
                        {
                            _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId, strFromDispalyName);
                        }
                    }
                    else
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                    }
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
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
                SaveEmailSentLog(entEmailTemplate.ID, _mMailMessage);
            }
            catch (Exception expCommon)
            {
                throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
            }
            return true;
        }

        public bool SendPublicMailForLink(EmailDeliveryDashboard pEmailInfo, string pstrAdditionalEmailBody, List<Attachment> pListAttachments, Learner learner)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            EmailTemplate entEmailTemplate;
            string strFromDispalyName = string.Empty;
            try
            {
                entEmailTemplate = GetEmailTemplate(pEmailInfo);
                if (pEmailInfo.AddToDashboard)
                {
                    pEmailInfo.IsPersonalized = false;
                    return AddEmailToDashBoard(pEmailInfo, pListAttachments);
                }


                if (entEmailTemplate != null)
                {
                    if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                    {
                        userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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
                            //_mMailMessage.Body = userMailTemplate.EmailBodyText;
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
                        if (entEmailTemplate.EmailTemplateLanguage.Count > 0)
                        {
                            foreach (EmailTemplateLanguage emailtemplatelang in entEmailTemplate.EmailTemplateLanguage)
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
                                    //_mMailMessage.Body += emailtemplatelang.EmailBodyText + "<br/> ";
                                    _mMailMessage.Body = GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, learner) + "<br/> ";
                                    if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                    {
                                        _mMailMessage.Body += pstrAdditionalEmailBody;
                                    }
                                }
                            }
                        }
                        else
                        {
                            EmailTemplate eTemp = pEmailInfo.EmailTemplate;
                            if (eTemp != null)
                            {
                                if (string.IsNullOrEmpty(strFromDispalyName))
                                {
                                    if (!string.IsNullOrEmpty(eTemp.DisplayName))
                                    {
                                        strFromDispalyName = eTemp.DisplayName;
                                    }
                                }

                                _mMailMessage.Subject += eTemp.EmailSubjectText + " ";
                                //_mMailMessage.Body += eTemp.EmailBodyText + "<br/> ";
                                _mMailMessage.Body = GetMailBodyFromTemplate(eTemp.EmailBodyText, eTemp.LanguageId, learner) + "<br/> ";
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
                if (!string.IsNullOrEmpty(pEmailInfo.ToList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailReplyToId))
                {
                    _mMailMessage.ReplyTo = new MailAddress(entEmailTemplate.EmailReplyToId);
                }
                //Add From 
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailFromId))
                {
                    if (userMailTemplate != null)
                    {
                        if (string.IsNullOrEmpty(userMailTemplate.DisplayName))
                        {
                            _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                        }
                        else
                        {
                            _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId, strFromDispalyName);
                        }
                    }
                    else
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                    }
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
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
                SaveEmailSentLog(entEmailTemplate.ID, _mMailMessage);
            }
            catch (Exception expCommon)
            {
                throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
            }
            return true;
        }
        public bool SendPublicMailForPasscode(EmailDeliveryDashboard pEmailInfo, string pstrAdditionalEmailBody, List<Attachment> pListAttachments, Learner learner)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            EmailTemplate entEmailTemplate;
            string strFromDispalyName = string.Empty;
            try
            {
                entEmailTemplate = GetEmailTemplate(pEmailInfo);
                if (pEmailInfo.AddToDashboard)
                {
                    pEmailInfo.IsPersonalized = false;
                    return AddEmailToDashBoard(pEmailInfo, pListAttachments);
                }


                if (entEmailTemplate != null)
                {
                    if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                    {
                        userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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
                            //_mMailMessage.Body = userMailTemplate.EmailBodyText;
                            _mMailMessage.Body = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, learner);
                            if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                            {
                                string str = _mMailMessage.Body.Replace("&lt;%PasscodeList%&gt;", pstrAdditionalEmailBody);
                                _mMailMessage.Body = str;
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
                        if (entEmailTemplate.EmailTemplateLanguage.Count > 0)
                        {
                            foreach (EmailTemplateLanguage emailtemplatelang in entEmailTemplate.EmailTemplateLanguage)
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
                                    //_mMailMessage.Body += emailtemplatelang.EmailBodyText + "<br/> ";
                                    _mMailMessage.Body = GetMailBodyFromTemplate(emailtemplatelang.EmailBodyText, emailtemplatelang.LanguageId, learner) + "<br/> ";
                                    if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                                    {
                                        _mMailMessage.Body += pstrAdditionalEmailBody;
                                    }
                                }
                            }
                        }
                        else
                        {
                            EmailTemplate eTemp = pEmailInfo.EmailTemplate;
                            if (eTemp != null)
                            {
                                if (string.IsNullOrEmpty(strFromDispalyName))
                                {
                                    if (!string.IsNullOrEmpty(eTemp.DisplayName))
                                    {
                                        strFromDispalyName = eTemp.DisplayName;
                                    }
                                }

                                _mMailMessage.Subject += eTemp.EmailSubjectText + " ";
                                //_mMailMessage.Body += eTemp.EmailBodyText + "<br/> ";
                                _mMailMessage.Body = GetMailBodyFromTemplate(eTemp.EmailBodyText, eTemp.LanguageId, learner) + "<br/> ";
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
                if (!string.IsNullOrEmpty(pEmailInfo.ToList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailReplyToId))
                {
                    _mMailMessage.ReplyTo = new MailAddress(entEmailTemplate.EmailReplyToId);
                }
                //Add From 
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailFromId))
                {
                    if (userMailTemplate != null)
                    {
                        if (string.IsNullOrEmpty(userMailTemplate.DisplayName))
                        {
                            _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                        }
                        else
                        {
                            _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId, strFromDispalyName);
                        }
                    }
                    else
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                    }
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
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
                SaveEmailSentLog(entEmailTemplate.ID, _mMailMessage);
            }
            catch (Exception expCommon)
            {
                throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
            }
            return true;
        }
        public static string GetPageElementsText(List<UserPageElementLanguage> entListPageElements, string pElementName)
        {
            string strElementText = string.Empty;
            UserPageElementLanguage entElementSearch = new UserPageElementLanguage();
            entElementSearch.ElementName = pElementName;

            UserPageElementLanguage entElementReturn = entListPageElements.Find(delegate (UserPageElementLanguage entElement)
            { return entElement.ElementName == entElementSearch.ElementName; });
            if (entElementReturn != null)
            {
                strElementText = entElementReturn.ElementText;
            }

            return strElementText;
        }
        public static List<UserPageElementLanguage> GetPageElements(string pClientId, string pPageId, string pLangId)
        {
            List<UserPageElementLanguage> entListPageElements = new List<UserPageElementLanguage>();
            UserPage entPage = new UserPage();
            UserPageManager mgrUserPage = new UserPageManager();
            entPage.ClientId = pClientId;
            entPage.ID = pPageId;
            entPage.ParaLanguageId = pLangId;

            entPage = mgrUserPage.Execute(entPage, UserPage.Method.Get);
            entListPageElements = entPage.PageElementLanguage;

            return entListPageElements;
        }


        public bool SendPersonalizedMailForShareByEmail(string EmailTo, string strEmailsubject, string strEmailBody, string strEmailFromID)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            string[] arrayEmails = null;

            try
            {
                arrayEmails = EmailTo.Split(';');
                foreach (string strAdress in arrayEmails)
                {
                    _mMailMessage.To.Add(strAdress);
                }
                _mMailMessage.Subject = strEmailsubject;
                _mMailMessage.Body = strEmailBody;



                _mMailMessage.From = new MailAddress(strEmailFromID);




                strLog.Append(SendEmailWithReturnLog(_mMailMessage));

                SaveEmailSentLog("", _mMailMessage);

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
                        SaveEmailSentLog("", _mMailMessage);
                    }
                    else
                    {

                    }
                }
            }





            return true;
        }

        public bool SendPublicMailHelpdesk(EmailDeliveryDashboard pEmailInfo, string pstrAdditionalEmailBody, string pstrEmailID, string pstrTitle, string pstrIssueType, string pstrActivityName, string pstrActivityType, string pstrIssueDesc, string pstrTicketNo, string pstrUserName, List<Attachment> pListAttachments)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;

            EmailTemplateLanguage userMailTemplate = null;
            EmailTemplate entEmailTemplate;
            string strFromDispalyName = string.Empty;
            string strBody = string.Empty;
            string strSubject = string.Empty;
            try
            {
                entEmailTemplate = GetEmailTemplate(pEmailInfo);
                if (pEmailInfo.AddToDashboard)
                {
                    pEmailInfo.IsPersonalized = false;
                    return AddEmailToDashBoard(pEmailInfo, pListAttachments);
                }

                if (entEmailTemplate != null)
                {
                    if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                    {
                        userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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

                            strSubject = userMailTemplate.EmailSubjectText;
                            strBody = userMailTemplate.EmailBodyText;
                            _mMailMessage.Subject = strSubject.Replace("{TicketNo}", Convert.ToString(pstrTicketNo));
                            _mMailMessage.Body = strBody.Replace("{TicketNo}", Convert.ToString(pstrTicketNo));

                            if (!string.IsNullOrEmpty(pstrTitle))
                            {
                                _mMailMessage.Body += "<b>Title:</b> " + pstrTitle + "<br>";
                            }
                            if (!string.IsNullOrEmpty(pstrUserName))
                            {
                                _mMailMessage.Body += "<b>User Name:</b> " + pstrUserName.Trim() + "<br>";
                            }
                            if (!string.IsNullOrEmpty(pstrEmailID))
                            {
                                _mMailMessage.Body += "<b>Email:</b> " + pstrEmailID.Trim() + "<br>";
                            }
                            if (!string.IsNullOrEmpty(pstrIssueType))
                            {
                                _mMailMessage.Body += "<b>Issue Type:</b> " + pstrIssueType.Trim() + "<br>";
                            }
                            if (!string.IsNullOrEmpty(pstrActivityType))
                            {
                                _mMailMessage.Body += "<b>Training Type:</b> " + pstrActivityType.Trim() + "<br>";
                            }
                            if (!string.IsNullOrEmpty(pstrActivityName))
                            {
                                _mMailMessage.Body += "<b>Training Name:</b> " + pstrActivityName.Trim() + "<br>";
                            }
                            if (!string.IsNullOrEmpty(pstrIssueDesc))
                            {
                                _mMailMessage.Body += "<b>Issue Description:</b> " + pstrIssueDesc.Trim() + "<br>";
                            }
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
                        if (entEmailTemplate.EmailTemplateLanguage.Count > 0)
                        {
                            foreach (EmailTemplateLanguage emailtemplatelang in entEmailTemplate.EmailTemplateLanguage)
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
                        else
                        {
                            EmailTemplate eTemp = pEmailInfo.EmailTemplate;
                            if (eTemp != null)
                            {
                                _mMailMessage.Subject += eTemp.EmailSubjectText + " ";
                                _mMailMessage.Body += eTemp.EmailBodyText + "<br/> ";

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
                if (!string.IsNullOrEmpty(pEmailInfo.ToList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailReplyToId))
                {
                    _mMailMessage.ReplyTo = new MailAddress(entEmailTemplate.EmailReplyToId);
                }
                //Add From 
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailFromId))
                {
                    if (userMailTemplate != null)
                    {
                        if (string.IsNullOrEmpty(userMailTemplate.DisplayName))
                        {
                            _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                        }
                        else
                        {
                            _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId, strFromDispalyName);
                        }
                    }
                    else
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                    }
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
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
                SaveEmailSentLog(entEmailTemplate.ID, _mMailMessage);
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
        /// <param name="pLearner">Learner Object with specific language id</param>        
        /// <param name="pEmailInfo">EmailDeliveryDashboard Information</param>
        /// <param name="pstrAdditionalEmailBody">Additional body text to attached</param>
        /// <param name="pLanguageId">Specific language id</param>
        /// <param name="pListAttachments">List of attachements</param>
        /// <returns></returns>
        public bool SendPersonalizedMailHelpdesk(Learner pLearner, EmailDeliveryDashboard pEmailInfo, string TicketNo, string pstrAdditionalEmailBody,
                                        List<Attachment> pListAttachments)
        {
            _mMailMessage = new MailMessage();
            _mMailMessage.IsBodyHtml = true;
            EmailTemplateLanguage userMailTemplate = null;
            EmailTemplate entEmailTemplate;
            string strFromDispalyName = string.Empty;
            string strBody = string.Empty;
            string strSubject = string.Empty;

            try
            {
                entEmailTemplate = GetEmailTemplate(pEmailInfo);
                if (pEmailInfo.AddToDashboard)
                {
                    pEmailInfo.IsPersonalized = true;
                    return AddEmailToDashBoard(pEmailInfo, pListAttachments);
                }
                //Add To
                if (!ValidationManager.ValidateString(pLearner.EmailID.Trim(), ValidationManager.DataType.EmailID))
                {
                    LogMessage += "Invalid Email: " + pLearner.EmailID.Trim() + " ";
                    return false;
                }
                if (entEmailTemplate != null)
                {
                    if (!string.IsNullOrEmpty(pEmailInfo.PreferredLanguageId))
                    {
                        userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        { return entTempToFind.LanguageId == pEmailInfo.PreferredLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            { return entTempToFind.LanguageId == _strClientLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved; });
                        }
                        if (userMailTemplate == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
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

                            //strSubject= GetMailBodyFromTemplate(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, pLearner);
                            //strBody= GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, pLearner);

                            strSubject = userMailTemplate.EmailSubjectText;
                            strBody = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, pLearner);

                            _mMailMessage.Subject = strSubject.Replace("{TicketNo}", Convert.ToString(TicketNo));
                            _mMailMessage.Body = strBody.Replace("{TicketNo}", Convert.ToString(TicketNo));
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
                        EmailTemplateLanguage emailtemplatelang;
                        if (string.IsNullOrEmpty(pLearner.DefaultLanguageId))
                            pLearner.DefaultLanguageId = "en-US";
                        //foreach (EmailTemplateLanguage emailtemplatelang in entEmailTemplate.EmailTemplateLanguage)
                        //{
                        emailtemplatelang = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                        {
                            return entTempToFind.LanguageId == pLearner.DefaultLanguageId && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved;
                        });
                        if (emailtemplatelang == null)
                        {
                            userMailTemplate = entEmailTemplate.EmailTemplateLanguage.Find(delegate (EmailTemplateLanguage entTempToFind)
                            {
                                return entTempToFind.LanguageId == ADMIN_SITE_DEFAULT_LANGUAGE && entTempToFind.ApprovalStatus == EmailTemplate.EmailApprovalStatus.Approved;
                            });
                        }
                        if (emailtemplatelang != null)
                        {

                            if (string.IsNullOrEmpty(strFromDispalyName))
                            {
                                if (!string.IsNullOrEmpty(emailtemplatelang.DisplayName))
                                {
                                    strFromDispalyName = emailtemplatelang.DisplayName;
                                }
                            }

                            //strSubject = GetMailBodyFromTemplate(userMailTemplate.EmailSubjectText, userMailTemplate.LanguageId, pLearner);
                            //strBody = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, pLearner);

                            strSubject = userMailTemplate.EmailSubjectText;
                            strBody = GetMailBodyFromTemplate(userMailTemplate.EmailBodyText, userMailTemplate.LanguageId, pLearner);

                            _mMailMessage.Subject = strSubject.Replace("{TicketNo}", Convert.ToString(TicketNo));
                            _mMailMessage.Body = strBody.Replace("{TicketNo}", Convert.ToString(TicketNo));
                            if (!string.IsNullOrEmpty(pstrAdditionalEmailBody))
                            {
                                _mMailMessage.Body += pstrAdditionalEmailBody;
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
                if (string.IsNullOrEmpty(pEmailInfo.ToList))
                {
                    if (!string.IsNullOrEmpty(pLearner.FirstName) && !string.IsNullOrEmpty(pLearner.LastName))
                    {
                        _mMailMessage.To.Add(new MailAddress(pLearner.EmailID.Trim(), pLearner.FirstName + " " + pLearner.LastName));
                    }
                    else
                    {
                        _mMailMessage.To.Add(new MailAddress(pLearner.EmailID.Trim()));
                    }
                }
                else
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.ToList))
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
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailReplyToId))
                {
                    _mMailMessage.ReplyTo = new MailAddress(entEmailTemplate.EmailReplyToId);
                }
                //Add From
                if (!string.IsNullOrEmpty(entEmailTemplate.EmailFromId))
                {
                    if (string.IsNullOrEmpty(strFromDispalyName))
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId);
                    }
                    else
                    {
                        _mMailMessage.From = new MailAddress(entEmailTemplate.EmailFromId, strFromDispalyName);
                    }
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
                //Add CC
                if (!string.IsNullOrEmpty(pEmailInfo.CCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.CCList))
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
                if (!string.IsNullOrEmpty(pEmailInfo.BCCList))
                {
                    foreach (MailAddress address in GetAdresses(pEmailInfo.BCCList))
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

                    SaveEmailSentLog(entEmailTemplate.ID, _mMailMessage);

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
                            SaveEmailSentLog(entEmailTemplate.ID, _mMailMessage);
                        }
                        else
                        {

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
    }
}
