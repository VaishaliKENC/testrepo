using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;

namespace YPLMS2._0.API.YPLMS.Services
{
    public class LMSCache
    {

        static string strMessageId = "mCACHEERROR";
        const string CACHE_CONNECTION = "cacheconnection";
        const string CACHE_DURATION = "CacheDuration";
        const string PROC_CACHE_UPDATE = "AspNet_SqlCacheUpdateChangeIdStoredProcedure";
        const string PROC_CACHE_INSERT = "AspNet_SqlCheckCache";
        const string PARA_CACHE_ID = "@tableName";
        public static bool IS_IN_USE = true;
        static CustomException expCustom;

        #region GetValue(string)
        /// <summary>
        /// This method retrive value from the cache 
        /// </summary>
        /// <param name="pValueKey"> Key</param>
        /// <returns>returns a cached object using provided cached object key</returns>
        public static object GetValue(string pValueKey)
        {
            object objReturn = null;
            //if (IS_IN_USE && !String.IsNullOrEmpty(pValueKey))
            //{
            //    try
            //    {
            //        objReturn = HttpRuntime.Cache[pValueKey];
            //    }
            //    catch (Exception expCommon)
            //    {
            //        expCustom = new CustomException(strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, expCommon, true);
            //        throw expCommon;
            //    }
            //}
            return objReturn;
        }
        #endregion

        #region InCache(string)
        /// <summary>
        /// This method check the Cache contains value or not 
        /// if it contains value return true otherwise false
        /// </summary>
        /// <param name="pInstanceName"></param>
        /// <returns>True/false</returns>
        /// 
        public static bool IsInCache(string pValueKey)
        {
            object objCache;
            //if (IS_IN_USE && !String.IsNullOrEmpty(pValueKey))
            //{
            //    try
            //    {
            //        objCache = HttpRuntime.Cache.Get(pValueKey);
            //    }
            //    catch
            //    {
            //        return false;
            //    }
            //    if (objCache != null)
            //        return true;

            //}
            return false;
        }
        #endregion

        #region RemoveCacheItems
        /// <summary>
        /// Remove from the ASP.NET cache all items whose key starts with the input prefix
        /// </summary>
        public static void RemoveCacheItems(string pPrefix)
        {
            //if (IS_IN_USE && !String.IsNullOrEmpty(pPrefix))
            //{
            //    try
            //    {
            //        pPrefix = pPrefix.ToLower();
            //        List<string> objListItemsToRemove = new List<string>();
            //        IDictionaryEnumerator objIEnumerator = HttpRuntime.Cache.GetEnumerator();
            //        while (objIEnumerator.MoveNext())
            //        {
            //            if (objIEnumerator.Key.ToString().ToLower().StartsWith(pPrefix))
            //                objListItemsToRemove.Add(objIEnumerator.Key.ToString());
            //        }
            //        foreach (string itemToRemove in objListItemsToRemove)
            //            HttpRuntime.Cache.Remove(itemToRemove);
            //    }
            //    catch (Exception expCommon)
            //    {
            //        expCustom = new CustomException(strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
            //        throw expCustom;
            //    }
            //}
        }
        #endregion

        /// <summary>
        /// Add items into Cache with cachekey Dependency
        /// </summary>
        /// <param name="pInstanceName">Prefix with Client Id</param>
        /// <param name="pValue"></param>
        public static void AddCacheItem(string pInstanceName, object pValue, string pClientId)
        {
            if (!IS_IN_USE)
            {
                return;
            }
            //CacheItemRemovedCallback RemoveCallBack = new CacheItemRemovedCallback(OnCacheRemove);
            //try
            //{
            //    if (!String.IsNullOrEmpty(pInstanceName) && pValue != null)
            //    {
            //        string strTabelName = EncryptionManager.Encrypt(pClientId);
            //        CheckCacheKey(strTabelName, false);
            //        SqlCacheDependency sqlDependency = new SqlCacheDependency("YPLMSCache", strTabelName);
            //        double CacheDuration = Convert.ToDouble(System.Configuration.ConfigurationSettings.AppSettings[CACHE_DURATION]);
            //        System.Web.Caching.Cache DataCache = HttpRuntime.Cache;
            //        DataCache.Insert(pInstanceName, pValue, sqlDependency, DateTime.Now.AddMinutes(CacheDuration),
            //                        System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Normal, RemoveCallBack);
            //    }
            //}
            //catch (Exception expCommon)
            //{
            //    expCustom = new CustomException(strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
            //    throw expCustom;
            //}
        }

        /// <summary>
        /// To Remove all Dependent objects
        /// </summary>
        /// <param name="pKey"></param>
        /// <param name="pItem"></param>
        /// <param name="pReason"></param>
        //private static void OnCacheRemove(string pKey, object pItem, CacheItemRemovedReason pReason)
        //{
        //    //TO DO
        //}

        /// <summary>
        /// update cachekey row cache database  
        /// </summary>
        /// <param name="pClientId">Client Id</param>
        public static void UpdateCacheKeyFile(string pClientId)
        {
            ////To clear client application variable (it used in UI)
            //if (HttpContext.Current != null)
            //{
            //    HttpContext.Current.Application[pClientId] = null;
            //}
            if (!IS_IN_USE)
            {
                return;
            }
            CheckCacheKey(EncryptionManager.Encrypt(pClientId), true);
        }

        /// <summary>
        /// Check if key is added
        /// </summary>
        /// <param name="pClientId"></param>
        /// <param name="pUpdate"></param>
        private static void CheckCacheKey(string pEncrClientId, bool pUpdate)
        {
            string connString = ConfigurationManager.ConnectionStrings[CACHE_CONNECTION].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[CACHE_CONNECTION].ConnectionString);
            SqlCommand sqlComUpdate;
            try
            {
                if (pUpdate)
                {
                    sqlComUpdate = new SqlCommand(PROC_CACHE_UPDATE, sqlConnection);
                }
                else
                {
                    sqlComUpdate = new SqlCommand(PROC_CACHE_INSERT, sqlConnection);
                }
                sqlComUpdate.CommandType = CommandType.StoredProcedure;
                sqlComUpdate.Parameters.AddWithValue(PARA_CACHE_ID, pEncrClientId);
                sqlConnection.Open();
                sqlComUpdate.ExecuteNonQuery();
                sqlConnection.Close();
               // SqlCacheDependencyAdmin.EnableTableForNotifications(connString, pEncrClientId);
            }
            catch (Exception expCommon)
            {
                throw new CustomException(strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expCommon, true);
            }
            finally
            {
                if (sqlConnection.State != System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}
