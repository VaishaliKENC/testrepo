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
    public class UserPageManager : IManager<UserPage, UserPage.Method, UserPage.ListMethod>
    {

        private readonly LearnerDAM _learnerdam = new LearnerDAM();
        private readonly EmailDeliveryDashboardDAM _emailDeliveryDashboardDAM = new EmailDeliveryDashboardDAM();
        private readonly AutoEmailTemplateSettingAdaptor _autoEmailTemplateSettingAdaptor = new AutoEmailTemplateSettingAdaptor();
        private readonly EmailTemplateDAM _emailTemplateDAM = new EmailTemplateDAM();
        /// <summary>
        /// Execute  for GetAll
        /// </summary>
        /// <param name="pEntUserPage"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<UserPage> Execute(UserPage pEntUserPage, UserPage.ListMethod pMethod)
        {
            List<UserPage> entListUserPage = null;
            UserPageAdaptor adaptorUserPage = new UserPageAdaptor();
            switch (pMethod)
            {
                case UserPage.ListMethod.GetAll:
                    entListUserPage = adaptorUserPage.GetUserPageList(pEntUserPage);
                    break;

                default:
                    entListUserPage = null;
                    break;
            }
            return entListUserPage;
        }

        /// <summary>
        /// Execute for add/update,get, delete
        /// </summary>
        /// <param name="pEntUserPage"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public UserPage Execute(UserPage pEntUserPage, UserPage.Method pMethod)
        {
            UserPage entUserPage = null;
            switch (pMethod)
            {
                case UserPage.Method.Get:
                    entUserPage = GetPageFromCache(pEntUserPage);
                    break;
                default:
                    entUserPage = null;
                    break;
            }
            return entUserPage;
        }

        /// <summary>
        /// Get Page From Cache
        /// </summary>
        /// <param name="pEntPage"></param>
        /// <returns></returns>
        private UserPage GetPageFromCache(UserPage pEntPage)
        {
            UserPageAdaptor adaptorUserPage = new UserPageAdaptor();
            UserPage entUserPage = null;
            List<UserPage> entListpages;
            string strClientKey = pEntPage.ClientId;
            try
            {

                if (String.IsNullOrEmpty(strClientKey))
                {
                    CustomException expCustom = new CustomException(YPLMS.Services.Messages.Client.INVALID_CLIENT_ID, "BusinessManager.UserPageManager.GetPageFromCache", ExceptionSeverityLevel.Critical, null, true);
                    throw expCustom;
                }

                entUserPage = SearchInCache(strClientKey, pEntPage.ID, pEntPage.ParaLanguageId);
                if (entUserPage == null)
                {
                    entUserPage = adaptorUserPage.GetUserPageById(pEntPage);
                    //Add to Cache
                    if (entUserPage != null)
                    {
                        if (LMSCache.IsInCache(strClientKey + UserPage.CACHE_SUFFIX))
                        {
                            entListpages = (List<UserPage>)LMSCache.GetValue(strClientKey + UserPage.CACHE_SUFFIX);
                            LMSCache.RemoveCacheItems(strClientKey + UserPage.CACHE_SUFFIX);
                        }
                        else
                        {
                            entListpages = new List<UserPage>();
                        }
                        entListpages.Add(entUserPage);
                        LMSCache.AddCacheItem(strClientKey + UserPage.CACHE_SUFFIX, entListpages, strClientKey);
                    }
                }
            }
            catch (Exception expCommon)
            {
                CustomException expCustom = new CustomException(YPLMS.Services.Messages.UserPage.BL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw expCustom;
            }
            return entUserPage;
        }

        /// <summary>
        /// Search Page in Cache
        /// </summary>
        /// <param name="pPageId"></param>
        /// <param name="pLanguageId"></param>
        /// <returns></returns>
        private UserPage SearchInCache(string pClientId, string pPageId, string pLanguageId)
        {
            UserPage entPage = null;
            List<UserPage> entListpages;
            if (string.IsNullOrEmpty(pLanguageId))
            {
                pLanguageId = Language.SYSTEM_DEFAULT_LANG_ID;
            }
            if (LMSCache.IsInCache(pClientId + UserPage.CACHE_SUFFIX))
            {
                entListpages = (List<UserPage>)LMSCache.GetValue(pClientId + UserPage.CACHE_SUFFIX);
                entPage = (UserPage)entListpages.Find(delegate (UserPage pageToFind)
                { return (pageToFind.ID == pPageId && pageToFind.ParaLanguageId == pLanguageId); });
            }
            return entPage;
        }


        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="UserPage"></typeparam>
        /// <param name="pEntBase">UserPage object</param>
        /// <param name="pMethod">UserPage.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(UserPage pEntBase, UserPage.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<UserPage> listUserPage = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<UserPage>(listUserPage);
            return dataSet;

        }

        public string SendEmail(string pstrLearnerId, string pstrTitle, Learner pLearner)
        {
            try
            {
                EmailDeliveryDashboard entEmailDashBoard = new EmailDeliveryDashboard();
                if (!string.IsNullOrEmpty(pstrLearnerId))
                {

                    if (pLearner.IsAutoEmail)
                    {
                        return AddAutoEmailToDashBoard(pstrLearnerId, pstrTitle, pLearner);
                    }
                    else
                    {
                       // entEmailDashBoard = sendEmailToUser.EmailScheduleDelivery; need to solve vinod
                        if (entEmailDashBoard != null)
                        {
                            EmailTemplate entTemplate = new EmailTemplate();
                            entTemplate = entEmailDashBoard.EmailTemplate;

                            if (entTemplate != null && !string.IsNullOrEmpty(entTemplate.ID))
                            {
                                return ScheduledEmail(pLearner.ClientId, pstrLearnerId, entEmailDashBoard);
                            }
                            else
                            {
                                return "Email template not configured.";
                            }
                        }
                        else
                        {
                            return "Schedule email not configured.";
                        }
                    }
                }
                else
                {
                    return "User not provided.";
                }


            }
            catch (CustomException ex)
            {
                return ex.Message;
            }
            catch (Exception exc)
            {
               // ExceptionManager.WriteError(exc, string.Empty, 1, string.Empty);
                return exc.Message;
            }
        }
        /// <summary>
        /// Add auto email to dashboard.
        /// </summary>
        /// <param name="pstrClientId"></param>
        /// <param name="pstrLearnerId"></param>
        /// <param name="pstrAdditionalData"></param>
        /// <param name="pCC"></param>
        /// <param name="pBCC"></param>
        /// <param name="pstrTitle"></param>
        /// <returns></returns>
        public string AddAutoEmailToDashBoard(string pstrLearnerId, string pstrTitle, Learner pLearner)
        {
            try
            {
                AutoEmailTemplateSetting entAutoEmail = new AutoEmailTemplateSetting();
                entAutoEmail.ID = AutoEmailTemplateSetting.EVENT_LEARNER_ADMIN_ADD_INTERFACE;
                entAutoEmail.ClientId = pLearner.ClientId;
                entAutoEmail = _autoEmailTemplateSettingAdaptor.GetEmailEventById(entAutoEmail);
                if (entAutoEmail != null && !string.IsNullOrEmpty(entAutoEmail.EmailTemplateID))
                {
                    return AddToEmailDashBoard(pstrLearnerId, entAutoEmail.EmailTemplateID, pstrTitle, pLearner);
                }
                else
                {
                    return "Configured auto email not found.";
                }
            }
            catch (CustomException ex)
            {
                return ex.Message; 
            }
            catch (Exception exc)
            {
                //ExceptionManager.WriteError(exc, string.Empty, 1, string.Empty);
                return exc.Message;
            }
        }

        private string AddToEmailDashBoard(string pstrLearnerId, string pstrEmailTemplateId, string pstrTitle, Learner pLearner)
        {
            try
            {
                List<Learner> entLearnerList = new List<Learner>();
                EmailTemplate entEmailTemplate = new EmailTemplate();
                string _strLearnerId = string.Empty;
                entEmailTemplate.ClientId = pLearner.ClientId;
                entEmailTemplate.ID = pstrEmailTemplateId;
                entEmailTemplate = (EmailTemplate)_emailTemplateDAM.GetEmailTemplateById(entEmailTemplate);
                if (entEmailTemplate != null && !string.IsNullOrEmpty(entEmailTemplate.ID))
                {
                    if (!string.IsNullOrEmpty(pstrLearnerId))
                    {
                        Learner entLearner = new Learner();
                        entLearner.ClientId = pLearner.ClientId;
                        entLearner.UserNameAlias = pstrLearnerId;
                        entLearner = _learnerdam.GetUserByAlias(entLearner);
                        if (entLearner != null && !string.IsNullOrEmpty(entLearner.ID))
                        {
                            entLearnerList.Add(entLearner);
                            // added by Gitanjali 28.7.2010
                            _strLearnerId = _strLearnerId + entLearner.ID + ",";
                        }
                    }
                    else
                    {
                        return "User does not exists.";
                    }
                    EmailService sendEmail = new EmailService(pLearner.ClientId);
                    EmailDeliveryDashboard emailInfo = new EmailDeliveryDashboard();
                    if (pLearner.IsDirectSendMail == true)
                    {
                        emailInfo.AddToDashboard = false;
                    }
                    else
                    {
                        emailInfo.AddToDashboard = true;
                    }
                    emailInfo.EmailTemplateID = entEmailTemplate.ID;
                    emailInfo.EmailTemplate = entEmailTemplate;
                    emailInfo.PreferredLanguageId = string.Empty;
                    emailInfo.PreferredLanguageId = pLearner.DefaultLanguageId;// ddlLanguage.SelectedValue.ToString(); need to solve vinod

                    emailInfo.CCList = string.Empty;
                    emailInfo.BCCList = string.Empty;
                    emailInfo.ToList = string.Empty;
                    emailInfo.EmailDeliveryTitle = entEmailTemplate.EmailTemplateTitle;
                    emailInfo.CreatedById = pLearner.CreatedById;
                    // added by Gitanjali 28.7.2010

                    emailInfo.LearnerId = _strLearnerId.Substring(0, _strLearnerId.LastIndexOf(","));
                    try
                    {
                        if (sendEmail.SendPersonalizedMailForSchedular(entLearnerList, emailInfo, string.Empty, null))
                        {
                            return string.Empty;
                        }
                        else
                        {
                            return "Failed";
                        }
                    }
                    catch (CustomException ex)
                    {
                        return ex.Message;
                    }
                    catch (Exception exc)
                    {
                        //ExceptionManager.WriteError(exc, string.Empty, 1, string.Empty);
                        return exc.Message;
                    }
                }
                else
                {
                    return "Configured email template not found.";
                }
            }
            catch (CustomException ex)
            {
                return ex.Message;
            }
            catch (Exception exc)
            {
                //ExceptionManager.WriteError(exc, string.Empty, 1, string.Empty);
                return exc.Message;
            }
        }

        /// <summary>
        /// Add email to email delivery dashboard.
        /// </summary>
        /// <param name="pstrClientId"></param>
        /// <param name="pstrLearnerId"></param>
        /// <param name="pEmailDeliveryDashboard"></param>
        /// <returns></returns>
        public string ScheduledEmail(string pstrClientId, string pstrLearnerId, EmailDeliveryDashboard pEmailDeliveryDashboard)
        {
            try
            {
                List<Learner> entLearnerList = new List<Learner>();
                EmailMessages mailMessage = new EmailMessages(pstrClientId);
                string _strLearnerId = string.Empty;

                if (!string.IsNullOrEmpty(pstrLearnerId))
                {
                    Learner entLearner = new Learner();
                    entLearner.ClientId = pstrClientId;
                    entLearner.UserNameAlias = pstrLearnerId;
                    entLearner = _learnerdam.GetUserByAlias(entLearner);
                    if (entLearner != null && !string.IsNullOrEmpty(entLearner.ID))
                    {
                        entLearnerList.Add(entLearner);
                        // added by Gitanjali 28.7.2010
                        _strLearnerId = _strLearnerId + entLearner.ID + ",";
                    }
                    else
                    {
                        return "User does not exists.";
                    }
                    string strMailAddress = mailMessage.GetTOListFromLearners(entLearnerList);
                    pEmailDeliveryDashboard.DistributionListId = null;
                    pEmailDeliveryDashboard.ToList = strMailAddress;
                    // added by Gitanjali 28.7.2010
                    pEmailDeliveryDashboard.LearnerId = _strLearnerId.Substring(0, _strLearnerId.LastIndexOf(","));
                    pEmailDeliveryDashboard.DeliveryApprovalStatus = YPLMS.Services.Common.GetDescription(EmailDeliveryDashboard.ApprovalStatus.PendingApproval);
                    pEmailDeliveryDashboard = (EmailDeliveryDashboard)_emailDeliveryDashboardDAM.AddUpdateEmailDeliveryDashboard(pEmailDeliveryDashboard);
                    if (pEmailDeliveryDashboard != null && !string.IsNullOrEmpty(pEmailDeliveryDashboard.ID))
                    {
                        return string.Empty;
                    }
                    else
                    {
                        return "Failed";
                    }
                }
                else
                {
                    return "User not provided.";
                }
            }
            catch (CustomException ex)
            {
                return ex.Message;
            }
            catch (Exception exc)
            {
                //ExceptionManager.WriteError(exc, string.Empty, 1, string.Empty);
                return exc.Message;
            }
        }
    }
}
