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
    public class LanguageManager : IManager<Language, Language.Method, Language.ListMethod>
    {
        /// <summary>
        /// Default LanguageManager constructor
        /// </summary>
        public LanguageManager()
        {

        }
        /// <summary>
        ///  Used for get list of languages from master or client database. 
        /// </summary>
        /// <param name="pEntLanguage"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of language objects </returns>
        public List<Language> Execute(Language pEntLanguage, Language.ListMethod pMethod)
        {
            List<Language> entListLanguage = null;
            LanguageDAM adaptorLanguage = new LanguageDAM();
            if (string.IsNullOrEmpty(pEntLanguage.ClientId))
            {
                pEntLanguage.ClientId = "";
            }
            switch (pMethod)
            {
                case Language.ListMethod.GetClientList:
                    if (LMSCache.IsInCache(pEntLanguage.ClientId + Language.LANG_SUFFIX))
                    {
                        entListLanguage = (List<Language>)LMSCache.GetValue(pEntLanguage.ClientId + Language.LANG_SUFFIX);
                    }
                    else
                    {
                        entListLanguage = adaptorLanguage.GetClientLanguages(pEntLanguage);
                        LMSCache.AddCacheItem(pEntLanguage.ClientId + Language.LANG_SUFFIX, entListLanguage, pEntLanguage.ClientId);
                    }
                    break;
                case Language.ListMethod.GetMasterList:
                    entListLanguage = adaptorLanguage.GetMasterLanguages();
                    break;
                default:
                    break;
            }
            return entListLanguage;
        }

        /// <summary>
        /// Used for read action. 
        /// </summary>
        /// <param name="pEntLanguage"></param>
        /// <param name="pMethod"></param>
        /// <returns>language object</returns>
        public Language Execute(Language pEntLanguage, Language.Method pMethod)
        {
            Language entLanguage = null;
            LanguageDAM adaptorLanguage = new LanguageDAM();
            List<Language> entListLanguage = null;
            switch (pMethod)
            {
                case Language.Method.Get:
                    if (entLanguage == null && !string.IsNullOrEmpty(pEntLanguage.ID)
                                && !string.IsNullOrEmpty(pEntLanguage.ClientId) && (LMSCache.IsInCache(pEntLanguage.ClientId + Language.LANG_SUFFIX)))
                    {
                        entListLanguage = (List<Language>)LMSCache.GetValue(pEntLanguage.ClientId + Language.LANG_SUFFIX);
                        entLanguage = entListLanguage.Find(delegate (Language entLangFind)
                        { return entLangFind.ID == pEntLanguage.ID; });
                    }
                    if (entLanguage == null)
                    {
                        if (pEntLanguage.ID != null && pEntLanguage.ClientId != null)
                        {
                            entLanguage = adaptorLanguage.GetLanguageByID(pEntLanguage);
                        }
                        else if (pEntLanguage.ID != null && pEntLanguage.ClientId == null)
                            entLanguage = adaptorLanguage.GetMasterLanguageByID(pEntLanguage);
                    }
                    break;
                case Language.Method.Update:
                    break;
                default:
                    entLanguage = null;
                    break;
            }
            return entLanguage;
        }

        /// <summary>
        /// Used for bulk add operation
        /// </summary>
        /// <param name="pEntListLanguage"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of Language objects</returns>
        public List<Language> Execute(List<Language> pEntListLanguage, Language.ListMethod pMethod)
        {
            List<Language> entListLanguage = new List<Language>();
            LanguageDAM adaptorLanguage = new LanguageDAM();
            if (pEntListLanguage.Count > 0)
            {
                string strClientKey = pEntListLanguage[0].ClientId;
                switch (pMethod)
                {
                    case Language.ListMethod.AddClientLanguages:
                        entListLanguage = adaptorLanguage.AddSelectedClientLanguages(pEntListLanguage);
                        if (!string.IsNullOrEmpty(strClientKey))
                            LMSCache.UpdateCacheKeyFile(strClientKey);
                        break;
                    default:
                        entListLanguage = null;
                        break;
                }
            }
            return entListLanguage;
        }


        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="Language"></typeparam>
        /// <param name="pEntBase">Language object</param>
        /// <param name="pMethod">Language.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(Language pEntBase, Language.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<Language> listLanguage = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<Language>(listLanguage);
            return dataSet;

        }
    }
}
