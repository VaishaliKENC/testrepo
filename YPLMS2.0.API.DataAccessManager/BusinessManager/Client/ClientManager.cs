using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.YPLMS.Services;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.Client
{
    /// <summary>
    /// class ClientManager
    /// </summary>
    public class ClientManager : IManager<Entity.Client, Entity.Client.Method, Entity.Client.ListMethod>
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ClientManager()
        {

        }

        /// <summary>
        /// Use to execute Add,Update,Get,Delete transactions on Client object. 
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <param name="pMethod"></param>
        /// <returns>Client object info</returns>
        public Entity.Client Execute(Entity.Client pEntClient, Entity.Client.Method pMethod)
        {
            Entity.Client entClientReturn = null;
            ClientDAM adaptorClient = new ClientDAM();
            string strClientId = pEntClient.ID;
            switch (pMethod)
            {
                case Entity.Client.Method.Add:
                    entClientReturn = adaptorClient.AddClient(pEntClient);
                    if (!string.IsNullOrEmpty(entClientReturn.ID))
                    {
                        entClientReturn = Execute(entClientReturn, Entity.Client.Method.Get);
                       // AddToCache(entClientReturn);
                    }
                    break;
                case Entity.Client.Method.Update:

                    entClientReturn = adaptorClient.UpdateClient(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.Delete:

                    entClientReturn = adaptorClient.DeleteClient(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    break;
                case Entity.Client.Method.Get:
                    //Check For Cache
                    if (!string.IsNullOrEmpty(pEntClient.ID) && LMSCache.IsInCache(pEntClient.ID))
                    {
                        entClientReturn = (Entity.Client)LMSCache.GetValue(pEntClient.ID);
                    }
                    ///Check for Url
                    if (entClientReturn == null)
                    {
                        if (string.IsNullOrEmpty(pEntClient.ID) && !string.IsNullOrEmpty(pEntClient.ClientAccessURL))
                        {

                            //Get Id
                            entClientReturn = adaptorClient.GetClientIdByURL(pEntClient);
                            //Get By Id
                            if (entClientReturn != null && !string.IsNullOrEmpty(entClientReturn.ID))
                            {
                                entClientReturn = Execute(entClientReturn, Entity.Client.Method.Get);
                            }
                            else
                            {
                                throw new CustomException(YPLMS.Services.Messages.Client.INVALID_CLIENT_URL, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, null, false);
                            }

                            if (entClientReturn != null)
                            {

                                //This check only if user login by URL
                                if (!entClientReturn.IsActive)
                                {
                                    // throw new CustomException(Services.Messages.Client.INACTIVE, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, null, false);
                                }
                                if (!entClientReturn.IsClientContractStarted)  // if (DateTime.Compare(entClientReturn.ContractStartDate,DateTime.Now) > 0)
                                {
                                    throw new CustomException(YPLMS.Services.Messages.Client.CONTRACT_NOT_STARTED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, null, false);
                                }
                                if (entClientReturn.IsClientContractExpired) //if (DateTime.Compare(entClientReturn.ContractEndDate, DateTime.Now) < 0)
                                {
                                    //throw new CustomException(Services.Messages.Client.CONTRACT_EXPIRED, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, null, false);
                                }
                                if (!string.IsNullOrEmpty(entClientReturn.ID))
                                {
                                    //AddToCache(entClientReturn);
                                }
                            }
                        }
                    }
                    //If not found in cache
                    if (entClientReturn == null)
                    {
                        entClientReturn = adaptorClient.GetClientByID(pEntClient);
                        if (entClientReturn != null && !string.IsNullOrEmpty(entClientReturn.ID))
                        {
                            //AddToCache(entClientReturn);
                        }
                        else
                        {
                            throw new CustomException(YPLMS.Services.Messages.Client.INVALID_CLIENT_ID, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                        }
                    }
                    break;
                case Entity.Client.Method.ManageForgotPasswordLink:
                    entClientReturn = adaptorClient.ManageForgotPasswordLink(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.SetDefaultTheme:
                    entClientReturn = adaptorClient.SetDefaultTheme(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    if (!string.IsNullOrEmpty(entClientReturn.ID))
                    {
                        entClientReturn = Execute(entClientReturn, Entity.Client.Method.Get);
                    }
                    break;
                case Entity.Client.Method.SetDefaultLogo:
                    entClientReturn = adaptorClient.SetDefaultLogo(pEntClient);
                    if (!string.IsNullOrEmpty(entClientReturn.ID))
                    {
                        entClientReturn = Execute(entClientReturn, Entity.Client.Method.Get);
                    }
                    else
                    {
                        throw new CustomException(YPLMS.Services.Messages.Client.INVALID_CLIENT_URL, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, null, false);
                    }
                    //This check only if user login by URL
                    if (!string.IsNullOrEmpty(entClientReturn.ID))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.SetSessionTimeOut:
                    entClientReturn = adaptorClient.UpdateSessionTimeOut(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.SetLogoutRedirectionURL:
                    entClientReturn = adaptorClient.SetLogoutRedirectionURL(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.SetMaxUploadFileSize:
                    entClientReturn = adaptorClient.SetMaxUploadFileSize(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.SetSelfRegistrationType:
                    entClientReturn = adaptorClient.SetSelfRegistrationType(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.SetNonRestrictedDomain:
                    entClientReturn = adaptorClient.SetNonRestrictedDomain(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.SaveDisplayPhotoSettings:
                    entClientReturn = adaptorClient.SaveUserPhotoDisplaySettings(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.SaveAllowUploadPhotoSettings:
                    entClientReturn = adaptorClient.SaveAllowUploadPhotoSettings(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.GetDisplayPhotoSettings:
                    entClientReturn = adaptorClient.GetPhotoDisplaySettings(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.GetAllowuploadPhotoSettings:
                    entClientReturn = adaptorClient.GetAllowUploadPhotoSettings(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.UpdateSystemLockUnLock:
                    entClientReturn = adaptorClient.UpdateLockUnlockSystem(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.UpdateAllowUser:
                    entClientReturn = adaptorClient.UpdateAllowUser(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.UpdateFeedbackReceiverEmailId:
                    entClientReturn = adaptorClient.UpdateFeedbackReceiverEmailId(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.UpdateIsAnnouncementsEnabled:
                    entClientReturn = adaptorClient.UpdateIsAnnouncementsEnabled(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.UpdateHttpsAllowed:
                    entClientReturn = adaptorClient.UpdateHTTPSAllowed(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;

                case Entity.Client.Method.SetSSOType:
                    entClientReturn = adaptorClient.SetSSOType(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.SetIsRSSEnabled:
                    entClientReturn = adaptorClient.SetIsRSSEnabled(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.GetClientId:
                    entClientReturn = adaptorClient.GetClientIdByURL(pEntClient);
                    break;
                case Entity.Client.Method.GetFeedBackEmail:
                    entClientReturn = adaptorClient.GetFeedBackEmail(pEntClient);
                    break;
                case Entity.Client.Method.GetHttpsAllowed:
                    entClientReturn = adaptorClient.GetHTTPSAllow(pEntClient);
                    break;
                case Entity.Client.Method.GetAllowUser:
                    entClientReturn = adaptorClient.GetAllowUser(pEntClient);
                    break;
                case Entity.Client.Method.ActivateDeactivateClient:
                    entClientReturn = adaptorClient.ActivateDeactivateClient(pEntClient);
                    if (!string.IsNullOrEmpty(strClientId))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.CheckClientByName:
                    entClientReturn = adaptorClient.CheckClientIdByName(pEntClient);
                    break;
                case Entity.Client.Method.CheckClientByURL:
                    entClientReturn = adaptorClient.CheckClientByURL(pEntClient);
                    break;
                case Entity.Client.Method.SetDefaultPageSize:
                    entClientReturn = adaptorClient.SetDefaultpageSize(pEntClient);
                    //This check only if user login by URL
                    if (!string.IsNullOrEmpty(entClientReturn.ID))
                    {
                        LMSCache.UpdateCacheKeyFile(strClientId);
                    }
                    break;
                case Entity.Client.Method.SetImportConnection:
                    entClientReturn = adaptorClient.SetConnectionForImport(pEntClient);
                    break;
                case Entity.Client.Method.SetAuditTrailPeriod:
                    entClientReturn = adaptorClient.SetAuditTrailPeriod(pEntClient);
                    break;
                case Entity.Client.Method.GetAuditTrailPeriod:
                    entClientReturn = adaptorClient.GetAuditTrailPeriod(pEntClient);
                    break;
                case Entity.Client.Method.GetJIRAHelpDesk:
                    entClientReturn = adaptorClient.GetJIRAHelpDesk(pEntClient);
                    break;
                case Entity.Client.Method.GetClientByID:
                    entClientReturn = adaptorClient.GetClientByID(pEntClient);
                    break;
                default:
                    entClientReturn = null;
                    break;
            }
            adaptorClient = null;
            return entClientReturn;
        }

        /// <summary>
        /// Returns List of client object.
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of Client objects</returns>
        public List<Entity.Client> Execute(Entity.Client pEntClient, Entity.Client.ListMethod pMethod)
        {
            List<Entity.Client> entListClientReturn = null;
            ClientDAM adaptorClient = new ClientDAM();
            switch (pMethod)
            {
                case Entity.Client.ListMethod.GetAll:
                    entListClientReturn = adaptorClient.GetAllClients();
                    break;
                case Entity.Client.ListMethod.GetAllVirtualTrainingClient:
                    entListClientReturn = adaptorClient.GetAllVirtualTrainingClient();
                    break;
                default:
                    break;
            }
            return entListClientReturn;
        }


        /// <summary>
        /// Returns List of client object.
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of Client objects</returns>
        public Entity.Client Execute(string ContentModuleId, Entity.Client.Method pMethod)
        {
            Entity.Client entListClientReturn = null;
            ClientDAM adaptorClient = new ClientDAM();
            switch (pMethod)
            {
                case Entity.Client.Method.GetClientDetaildFromCourseId:
                    entListClientReturn = adaptorClient.GetClientDetaildFromCourseId(ContentModuleId);
                    break;
                default:
                    break;
            }
            return entListClientReturn;
        }


        public string ExecuteForClientAccessURL(Entity.Client pEntClient, Entity.Client.Method pMethod)
        {
            string AccessURL = string.Empty;
            ClientDAM adaptorClient = new ClientDAM();
            switch (pMethod)
            {
                case Entity.Client.Method.GetClientAccessURL:
                    AccessURL = adaptorClient.GetClientAccessURL(pEntClient);
                    break;
                default:
                    break;
            }
            return AccessURL;
        }

        /// <summary>
        /// Add Client to Cache
        /// </summary>
        /// <param name="pClient"></param>
        //private void AddToCache(Entity.Client pClient)
        //{
        //    if (LMSCache.IS_IN_USE)
        //    {
        //        if (!LMSCache.IsInCache(pClient.ID))
        //        {
        //            LMSCache.AddCacheItem(pClient.ID, pClient, pClient.ID);
        //        }
        //        //To add Feature List in Cache       
        //        if (!LMSCache.IsInCache(pClient.ID + AdminFeatures.CACHE_SUFFIX))
        //        {
        //            AdminFeatureManager featureManager = new AdminFeatureManager();
        //            featureManager.AddFeaturesToCache(pClient.ID);
        //        }
        //        //To add Feature List in Cache       
        //        if (!LMSCache.IsInCache(pClient.ID + SystemMessage.CACHE_SUFFIX))
        //        {
        //            SystemMessageManager sysMessageManager = new SystemMessageManager();
        //            sysMessageManager.AddMessagesToCache(pClient.ID);
        //        }
        //    }
        //}

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <typeparam name="Client"></typeparam>
        /// <param name="pEntBase"></param>
        /// <param name="pMethod"></param>        /// <returns></returns>
        public DataSet ExecuteDataSet(Entity.Client pEntBase, Entity.Client.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<Entity.Client> listClient = Execute(pEntBase, pMethod);
            Converter dsConverter = new Converter();
            dataSet = dsConverter.ConvertToDataSet<Entity.Client>(listClient);
            return dataSet;
        }

    }
}
