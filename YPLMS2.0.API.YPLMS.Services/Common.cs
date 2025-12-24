using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Collections;


//using YPLMS.DataAccessManager;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Http;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.YPLMS.Services
{
    public class Common
    {
        public static string BaseClientID = string.Empty;
        public static string CourseMasteryScore = string.Empty;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UrlConverter _urlConverterFactory;
        private readonly UrlConverterFactory _urlConverterFactory1;

        public Common(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _urlConverterFactory = new DefaultUrlConverter(httpContextAccessor);
            _urlConverterFactory1 = new UrlConverterFactory();
        }

        public Client GetClientByURL()
        {
            Client _entClient = new Client();
            string _strURL = string.Empty;

            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null) return _entClient;

                var currentRequest = _httpContextAccessor.HttpContext?.Request;
                UrlConverter converter = _urlConverterFactory1.CreateUrlConverter(currentRequest);
                string url = converter.ConvertUrl();


                if (!string.IsNullOrEmpty(url))
                {
                    _strURL = url;
                }
                else if (currentRequest.Cookies.ContainsKey("ClientID"))
                {
                    string encryptedClientId = currentRequest.Cookies["ClientID"];
                    string clientId = EncryptionManager.Decrypt(encryptedClientId);

                    _entClient.ID = clientId;
                    //_entClient = _clientManager.Execute(_entClient, Client.Method.Get);
                }
                else if (Common.IsDebugMode())
                {
                    // _strURL = "YP11-dev-local.encora.com:443";
                    _strURL = "YP08-dev-local.encora.com:443";
                    // _strURL = "YP02-dev-local.encora.com:443";
                    // _strURL = "YP01-dev-local.encora.com";
                    // _strURL = "YPMaster-dev-local.encora.com";
                }
                else
                {
                    var host = currentRequest.Host.Host;
                    var port = currentRequest.Host.Port;

                    if (port.HasValue && port.Value != 80)
                        _strURL = $"{host}:{port.Value}";
                    else
                        _strURL = host;
                }

                if (!string.IsNullOrEmpty(_strURL))
                {
                    _entClient.ClientAccessURL = _strURL;
                    //_entClient = _clientManager.Execute(_entClient, Client.Method.Get);

                    if (_entClient != null && !string.IsNullOrEmpty(_entClient.ID))
                    {
                        //LMSSession.AddSessionItem(Client.CLIENT_SESSION_ID, _entClient.ID);
                        return _entClient;
                    }
                }
            }
            catch (Exception ex)
            {
                // Optional: Add logging here
            }

            return _entClient;
        }

        public static Boolean IsDebugMode()
        {

            return false;

        }

        /// <summary>   
        /// This method adds the items in the first list to the second list.               
        /// </summary>   
        /// <typeparam name="FROM_TYPE"></typeparam>   
        /// <typeparam name="TO_TYPE"></typeparam>   
        /// <param name="listToCopyFrom"></param>   
        /// <param name="listToCopyTo"></param>   
        /// <returns>Returns added item object</returns>   
        public static List<TO_TYPE> AddRange<FROM_TYPE, TO_TYPE>(List<FROM_TYPE> listToCopyFrom,
            List<TO_TYPE> listToCopyTo) where FROM_TYPE : TO_TYPE
        {
            // loop through the list to copy, and   
            foreach (FROM_TYPE item in listToCopyFrom)
            {
                // add items to the copy tolist   
                listToCopyTo.Add(item);
            }
            // return the copy to list   
            return listToCopyTo;
        }

        public static  string GetEnumName(Type value, string description)
        {

            try
            {
                FieldInfo[] fis = value.GetFields();
                foreach (FieldInfo fi in fis)
                {
                    DescriptionAttribute[] attributes =
                      (DescriptionAttribute[])fi.GetCustomAttributes
                      (typeof(DescriptionAttribute), false);
                    if (attributes.Length > 0)
                    {
                        if (attributes[0].Description == description)
                        {
                            return fi.Name;
                        }
                    }
                }
                return description;
            }
            catch
            {
                return description;
            }
        }

        /// <summary>
        /// Removes tags from string
        /// </summary>
        /// <param name="pText"></param>
        /// <returns>Text without tag</returns>
        public static string RemoveTags(string pText)
        {
            if (!string.IsNullOrEmpty(pText))
            {
                string Expression = "</?[^<>]+/?>";
                pText = Regex.Replace(pText, Expression, "");
                return pText;
            }
            else
                return "";
        }

        /// <summary>
        /// Retrieve the description on the enum, e.g.
        /// [Description("Bright Pink")]
        /// BrightPink = 2,
        /// Then when you pass in the enum, it will retrieve the description
        /// </summary>
        /// <param name="en">The Enumeration</param>
        /// <returns>A string representing the friendly name</returns>
        public static string GetDescription(Enum pEnum)
        {
            Type type = pEnum.GetType();
            MemberInfo[] memInfo = type.GetMember(pEnum.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return pEnum.ToString();
        }

        public static string GetDescription(string pValue, Type pType)
        {
            if (pType.IsEnum && !string.IsNullOrEmpty(pValue))
            {
                MemberInfo[] memInfo = pType.GetMember(pValue);
                if (memInfo != null && memInfo.Length > 0)
                {
                    object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attrs != null && attrs.Length > 0)
                    {
                        return ((DescriptionAttribute)attrs[0]).Description;
                    }
                }
                return pValue;
            }
            else
            {
                return pValue;
            }
        }

        /// <summary>
        /// Get Field Mapping from String To StringDictionary
        /// </summary>
        /// <param name="pstrFieldMapping"></param>
        /// <returns></returns>
        public static StringDictionary GetFieldMappingFromStringToStringDictionary(string pstrFieldMapping)
        {
            StringDictionary sdictionaryReturn = new StringDictionary();
            string[] arrayEmails = null;
            if (pstrFieldMapping.Contains("##"))
            {
                pstrFieldMapping = pstrFieldMapping.Replace("##", ",");
                arrayEmails = pstrFieldMapping.Split(',');
            }
            
            foreach (string str in arrayEmails)
            {
                string[] arrayKeys = null;
                if (str.Contains("$$"))
                {
                    string str1 = str.Replace("$$", ",");
                    arrayKeys = str1.Split(',');
                }
                if (arrayKeys != null)
                {
                    if (!string.IsNullOrEmpty(arrayKeys[0]) && !string.IsNullOrEmpty(arrayKeys[1]))
                    {
                        sdictionaryReturn.Add(arrayKeys[0], arrayKeys[1]);
                    }
                }
            }
            return sdictionaryReturn;
        }

        public static StringDictionary GetFieldMappingFromStringToStringDictionaryForAssessment(string pstrFieldMapping)
        {
            StringDictionary sdictionaryReturn = new StringDictionary();
            string[] arrayEmails = null;
            if (pstrFieldMapping.Contains("##"))
            {
                //pstrFieldMapping = pstrFieldMapping.Replace("##", ",");
                //arrayEmails = pstrFieldMapping.Split(',');
                pstrFieldMapping = pstrFieldMapping.Replace("##", ";");
                arrayEmails = pstrFieldMapping.Split(';');
            }
            string[] arrayKeys = null;
            foreach (string str in arrayEmails)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    string str1 = string.Empty;
                    if (str.Contains("$$"))
                    {
                        //str1 = str.Replace("$$", ",");
                        //arrayKeys = str1.Split(',');
                        str1 = str.Replace("$$", ";");
                        arrayKeys = str1.Split(';');
                    }
                    if (arrayKeys != null)
                    {
                        if (!string.IsNullOrEmpty(arrayKeys[0]) && !string.IsNullOrEmpty(arrayKeys[1]))
                        {
                            sdictionaryReturn.Add(arrayKeys[0], arrayKeys[1]);
                        }
                    }
                }
             
            }


            
            return sdictionaryReturn;
        }
        //// added by Gitanjali 03.09.2010
        public static StringDictionary GetFieldMappingFromStringToStringDictionaryForManagerReporting(string pstrFieldMapping)
        {
            StringDictionary sdictionaryReturn = new StringDictionary();
            string[] arrayEmails = null;
            if (pstrFieldMapping.Contains("##,"))
            {
                pstrFieldMapping = pstrFieldMapping.Replace("##,", "*");
                arrayEmails = pstrFieldMapping.Split('*');
            }
            foreach (string str in arrayEmails)
            {
                string[] arrayKeys = null;
                if (str.Contains("$$"))
                {
                    //string str1 = str.Replace("$$", "$");
                    //arrayKeys = str1.Split('$');
                    arrayKeys = new string[2]; 
                    int pos = str.IndexOf("$$");
                    string param  = str.Substring(0, pos);
                    string paramValue  = str.Substring(pos + 2);
                    arrayKeys[0]= param ;
                    arrayKeys[1]=paramValue ;  
                }
                if (arrayKeys != null)
                {
                    if (!string.IsNullOrEmpty(arrayKeys[0]) && !string.IsNullOrEmpty(arrayKeys[1]))
                    {
                        sdictionaryReturn.Add(arrayKeys[0], arrayKeys[1]);
                    }
                }
            }
            return sdictionaryReturn;
        }

        /// <summary>
        /// Get Field mapping from StringDictionary To String
        /// </summary>
        /// <param name="psdictionary"></param>
        /// <returns></returns>
        public static string GetFieldMappingFromStringDictionaryToString(StringDictionary psdictionary)
        {
            string strRetValue = string.Empty;

            if (psdictionary.Count > 0)
            {
                foreach (DictionaryEntry item in psdictionary)
                {
                    strRetValue = strRetValue + item.Key + "$$" + item.Value + "##";
                }
            }
            return strRetValue;
        }

        /// <summary>
        /// Get Field Mapping from String To StringDictionary For Learner Dump Report
        /// </summary>
        /// <param name="pstrFieldMapping"></param>
        /// <returns></returns>
        public static StringDictionary GetFieldMappingFromStringToStringDictionaryLearnerReport(string pstrFieldMapping)
        {
            StringDictionary sdictionaryReturn = new StringDictionary();
            string[] arrayEmails = null;
            if (pstrFieldMapping.Contains("##,"))
            {
                pstrFieldMapping = pstrFieldMapping.Replace("##,", ",");
                arrayEmails = pstrFieldMapping.Split(',');
            }
            foreach (string str in arrayEmails)
            {
                string[] arrayKeys = null;
                if (str.Contains("$$"))
                {
                    string str1 = str.Replace("$$", ",");
                    arrayKeys = str1.Split(',');
                }
                if (arrayKeys != null)
                {
                    if (!string.IsNullOrEmpty(arrayKeys[0]) && !string.IsNullOrEmpty(arrayKeys[1]))
                    {
                        sdictionaryReturn.Add(arrayKeys[0], arrayKeys[1]);
                    }
                }
            }
            return sdictionaryReturn;
        }

        /// <summary>
        /// Get Field mapping from StringDictionary To String For Learner Dump Report
        /// </summary>
        /// <param name="psdictionary"></param>
        /// <returns></returns>
        public static string GetFieldMappingFromStringDictionaryToStringLearnerDumpReport(StringDictionary psdictionary)
        {
            string strRetValue = string.Empty;
            if (psdictionary.Count > 0)
            {
                foreach (DictionaryEntry item in psdictionary)
                {
                    strRetValue = strRetValue + item.Key + "$$" + item.Value + "##,";
                }
            }
            return strRetValue;
        }

        /// <summary>
        /// Remove Start and End Tags : Method overloading
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        public static string TrimStringStartEnd(string str, string strStart, string strEnd)
        {
            if (!string.IsNullOrEmpty(strStart))
            {
                if (str.StartsWith(strStart))
                {
                    str = str.Remove(0, strStart.Length);
                }
            }
            if (!string.IsNullOrEmpty(strEnd))
            {
                if (str.EndsWith(strEnd))
                {
                    int iEndIndex = str.LastIndexOf(strEnd);
                    if (iEndIndex >= 0)
                        str = str.Remove(iEndIndex);
                }
            }
            return str;
        }

        /// <summary>
        /// Remove Start and End Tags : Method overloading
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        public static string TrimStringStartEnd(string str, string strStart, string strEnd, string strStartEnd)
        {
            if (!string.IsNullOrEmpty(strStart))
            {
                if (str.StartsWith(strStart))
                {
                    str = str.Remove(0, strStart.Length);
                }
            }
            if (!string.IsNullOrEmpty(strEnd))
            {
                if (str.EndsWith(strEnd))
                {
                    int iEndIndex = str.LastIndexOf(strEnd);
                    if (iEndIndex >= 0)
                        str = str.Remove(iEndIndex);
                }
            }

            if (!string.IsNullOrEmpty(strStartEnd))
            {
                if (str.StartsWith(strStartEnd))
                {
                    str = str.Remove(0, strStartEnd.Length);
                }
                if (str.EndsWith(strStartEnd))
                {
                    int iEndIndex = str.LastIndexOf(strStartEnd);
                    if (iEndIndex >= 0)
                        str = str.Remove(iEndIndex);
                }
            }
            return str;
        }
        /// <summary>
        /// Return Specific Date Format
        /// </summary>
        /// <param name="pDateValue"></param>
        /// <returns></returns>
        public static DateTime GetSpecificDateFormat(string pDateValue, string pstrDateFormat)
        {
            bool IsValid = false;

            char strSeparator = '/';
            if (pstrDateFormat.Contains("."))
                strSeparator = '.';
            else if (pstrDateFormat.Contains("/"))
                strSeparator = '/';
            else if (pstrDateFormat.Contains("-"))
                strSeparator = '-';

            if (pDateValue.Contains(strSeparator.ToString()))
                IsValid = true;
            else
                IsValid = false;

            string strDateFormat = pstrDateFormat;
            DateTime dt = new DateTime();
            if (IsValid)
            {
                string strConcatenatedValue = string.Empty;

                string[] strSplit = pDateValue.Split(strSeparator);
                for (int iLength = 0; iLength <= strSplit.Length - 1; iLength++)
                {
                    if (strSplit[iLength].Length <= 1)
                        strSplit[iLength] = "0" + strSplit[iLength];

                    strConcatenatedValue = strConcatenatedValue + strSplit[iLength] + strSeparator;
                }

                int iMonth = strDateFormat.IndexOf('M');
                int iday = strDateFormat.IndexOf('d');
                int iYear = strDateFormat.IndexOf('y');
                dt = new DateTime(Convert.ToInt32(strConcatenatedValue.Substring(iYear, 4)), Convert.ToInt32(strConcatenatedValue.Substring(iMonth, 2)), Convert.ToInt32(strConcatenatedValue.Substring(iday, 2)));
            }
            else
            {
                throw new InvalidDataException();
            }
            return dt;
        }
        ///
        public static string GetClientAccessURL(string ClientAccessURL, bool bIsHTTPSAllowed)
        {
            if (bIsHTTPSAllowed)
            {  
                  if (ClientAccessURL.ToLower().EndsWith(":443/yplms"))                  
                        ClientAccessURL = "https://" + ClientAccessURL.ToLower().Replace(":443/yplms", "");
                  
                  else if (ClientAccessURL.ToLower().EndsWith("/yplms"))
                      ClientAccessURL = "https://" + TrimStringStartEnd(ClientAccessURL.ToLower(), "https", "/yplms");                  

                  else
                      ClientAccessURL = "https://" + ClientAccessURL;                
            }
            else
            {
                if (ClientAccessURL.ToLower().EndsWith("/yplms"))                
                    ClientAccessURL = "http://" + TrimStringStartEnd(ClientAccessURL.ToLower(), "http", "/yplms");
                
                else
                    ClientAccessURL = "http://" + ClientAccessURL;
            }
            return ClientAccessURL;
        }

        }
}