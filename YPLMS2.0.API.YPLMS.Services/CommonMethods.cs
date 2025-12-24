using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.YPLMS.Services
{
    public class CommonMethods
    {
        /// <summary>
        /// Method to remove special character
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSpecialChars(string str)
        {
            string[] chars = new string[] { ",", ".", "/", "'", "\"", "<", ">" };
            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }
            return str;
        }

        //private static void LearnerGetSsoLogin()
        //{
            
        //    Learner entLearner = (Learner)LMSSession.GetValue(Learner.USER_SESSION_ID);
        //    LearnerManager entLearnerMgr = new LearnerManager();

        //    if (entLearner != null)
        //    {
        //        entLearnerMgr.Execute(entLearner, Learner.Method.GetSSOLOGIN);
        //    }
        //}

        //private static string BuildPreferredTimeZone()
        //{
        //    LearnerGetSsoLogin();

        //    if (LMSSession.IsInSession("PREFERRED_TIME_FORMAT"))
        //    {
        //        return (string)LMSSession.GetDirectValue("PREFERRED_TIME_FORMAT");
        //    }

        //    // return empty if still not found
        //    return "";
        //}

        //public static string GetPreferredTimeZone()
        //{
        //    return GetStringFromSessionAndStoreItIfNecessary("PreferredTimeFormat", BuildPreferredTimeZone);
        //}

        public static bool CheckSpecialChars(string str)
        {
            string[] chars = new string[] { ",", "&", "/", "'", "\"", "<", ">", "^", "+", "[", "]" };

            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static string RemoveHTMLChars(string str)
        {
            string[] chars = new string[] { "&nbsp;", "&quot;", "&amp;", "\n", "\r", ",", "&ldquo;", "&rdquo;", "&rsquo;" };
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
                    str = str.Replace(chars[6], "\"");
                    str = str.Replace(chars[7], "\"");
                    str = str.Replace(chars[8], "\'");
                }
            }
            return str;
        }

        public static string FormatDataForCSV(string pstrData)
        {
            if (pstrData.IndexOf(Convert.ToChar(",")) != -1)
            {
                pstrData = "\"" + pstrData.Replace("\"", "\"\"") + "\"";
            }
            return pstrData;
        }

        /// <summary>
        /// While inseritng in Db means setting a property of an Object
        /// </summary>
        /// <param name="pDateValue"></param>
        /// <returns></returns>
        //public static DateTime GetSpecificDateFormat(string pDateValue)
        //{
        //    string strDateFormat = GetPreferredDateFormat();
        //    DateTime dt = new DateTime();
        //    int iMonth = strDateFormat.IndexOf('M');
        //    int iday = strDateFormat.IndexOf('d');
        //    int iYear = strDateFormat.IndexOf('y');
        //    dt = new DateTime(Convert.ToInt32(pDateValue.Substring(iYear, 4)), Convert.ToInt32(pDateValue.Substring(iMonth, 2)), Convert.ToInt32(pDateValue.Substring(iday, 2)));
        //    return dt;
        //}
        //public static DateTime GetUserDateTimeFromUTC(DateTime pDateTime)
        //{
        //    DateTime userDateTime = pDateTime;

        //    if (!string.IsNullOrEmpty(Convert.ToString(pDateTime)))
        //    {
        //        try
        //        {
        //            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(CommonMethods.GetPreferredTimeZone());
        //            DateTime utcDateTime = pDateTime;
        //            userDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, tzi);
        //        }
        //        catch { }
        //    }

        //    return userDateTime;

        //}

        //public static bool IsValidDateFormat(string pStrDate)
        //{
        //    Regex regEx = new Regex(CommonMethods.SetDateValidationExpression());
        //    return regEx.IsMatch(pStrDate);
        //}

        public static string GetStringFromSessionAndStoreItIfNecessary(string lookupKey, Func<string> buildValueFunction)
        {            
                // not found, so build the value and add it to the session, then return it
                string retValue = buildValueFunction();
               
                return retValue;
            
            // session not in use, call buildValueFunction to return the value
            return buildValueFunction();
        }
        /// <summary>
        /// Method to removed special character for filename
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetValidFileName(string fileName)
        {
            String ret = Regex.Replace(fileName.Trim(), "[^A-Za-z0-9_. ]+", "");
            return ret.Replace(" ", String.Empty);
        }

        private static string BuildPreferredDateFormat()
        {
            string strPreferredDate = "";

            //LearnerGetSsoLogin();
            //if (LMSSession.IsInSession(PREFERRED_DATE_FORMAT))
            //{
            //    strPreferredDate = (string)LMSSession.GetDirectValue(PREFERRED_DATE_FORMAT);
            //}

            return strPreferredDate;
        }
        public static string GetPreferredDateFormat()
        {
            return GetStringFromSessionAndStoreItIfNecessary("PreferredDateFormat", BuildPreferredDateFormat);
        }

        public static DateTime GetSpecificDateFormat(string pDateValue)
        {
            string strDateFormat = GetPreferredDateFormat();
            DateTime dt = new DateTime();
            int iMonth = strDateFormat.IndexOf('M');
            int iday = strDateFormat.IndexOf('d');
            int iYear = strDateFormat.IndexOf('y');
            dt = new DateTime(Convert.ToInt32(pDateValue.Substring(iYear, 4)), Convert.ToInt32(pDateValue.Substring(iMonth, 2)), Convert.ToInt32(pDateValue.Substring(iday, 2)));
            return dt;
        }

        //public static string GetPreferredDateFormat()
        //{
        //    return GetStringFromSessionAndStoreItIfNecessary(PREFERRED_DATE_FORMAT, BuildPreferredDateFormat);
        //}

        //private static string BuildPreferredDateFormat()
        //{
        //    string strPreferredDate = "";

        //    LearnerGetSsoLogin();
        //    if (LMSSession.IsInSession(PREFERRED_DATE_FORMAT))
        //    {
        //        strPreferredDate = (string)LMSSession.GetDirectValue(PREFERRED_DATE_FORMAT);
        //    }

        //    return strPreferredDate;
        //}
        //public static string SetDateValidationExpression()
        //{
        //    string strDateFormat = GetPreferredDateFormat();
        //    string strValidationExpression = string.Empty;
        //    switch (strDateFormat)
        //    {
        //        case "dd/MM/yyyy":
        //            //strValidationExpression = @"(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d";
        //            strValidationExpression = @"(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/](19|20)\d\d";
        //            break;
        //        case "dd.MM.yyyy":
        //            //strValidationExpression = @"(0[1-9]|[12][0-9]|3[01])[-+.](0[1-9]|1[012])[-+.](19|20)\d\d";
        //            strValidationExpression = @"(0[1-9]|[12][0-9]|3[01])[.](0[1-9]|1[012])[.](19|20)\d\d";
        //            break;
        //        case "dd-MM-yyyy":
        //            //strValidationExpression = @"(0[1-9]|[12][0-9]|3[01])[-+-](0[1-9]|1[012])[-+-](19|20)\d\d";
        //            strValidationExpression = @"(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-](19|20)\d\d";
        //            break;
        //        case "MM/dd/yyyy":
        //            // strValidationExpression = @"([1-9]|1[012])[-/.]([1-9]|[12][0-9]|3[01])[-/.](19|20)\d\d";
        //            strValidationExpression = @"(0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01])[/](19|20)\d\d";
        //            break;
        //        case "MM-dd-yyyy":
        //            //strValidationExpression = @"([1-9]|1[012])[-+-]([1-9]|[12][0-9]|3[01])[-+-](19|20)\d\d";
        //            strValidationExpression = @"(0[1-9]|1[012])[-](0[1-9]|[12][0-9]|3[01])[-](19|20)\d\d";
        //            break;
        //        case "yyyy-MM-dd":
        //            //strValidationExpression = @"([1-9]|1[012])[-+-]([1-9]|[12][0-9]|3[01])[-+-](19|20)\d\d";
        //            strValidationExpression = @"^[0-9]{4}-(((0[13578]|(10|12))-(0[1-9]|[1-2][0-9]|3[0-1]))|(02-(0[1-9]|[1-2][0-9]))|((0[469]|11)-(0[1-9]|[1-2][0-9]|30)))$";

        //            break;
        //        case "yyyy/MM/dd":
        //            //strValidationExpression = @"([1-9]|1[012])[-+-]([1-9]|[12][0-9]|3[01])[-+-](19|20)\d\d";
        //            //strValidationExpression = @"^[0-9]{4}/(((0[13578]|(10|12))/(0[1-9]|[1-2][0-9]|3[0-1]))|(02-(0[1-9]|[1-2][0-9]))|((0[469]|11)/(0[1-9]|[1-2][0-9]|30)))$";
        //            strValidationExpression = @"^(?:(?:1[6-9]|[2-9]\d)?\d{2})(\/|-|\.)(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)|(?:(?:0?[1,3-9]|1[0-2])(\/|-|\.)(?:29|30)))$|^(?:(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(\/|-|\.)0?2(\/|-|\.)29)$|^(?:(?:1[6-9]|[2-9]\d)?\d{2})(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])$";
        //            break;
        //    }
        //    return strValidationExpression;
        //}


    }
}
