using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.YPLMS.Services.Messages;
using YPLMS2._0.API.YPLMS.Services;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public class PasswordCreator
    {
        /// <summary>
        /// Function to get password as per configuration.
        /// </summary>
        /// <returns>returns unique number with provided prefix attached and requested size </returns>
        public static string CreatePassword(string pClinetID)
        {
            string PASSWORD_CHARS_LCASE = "abcdefgijklmnopqrstuwxyz";
            string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTUWXYZ";
            string PASSWORD_CHARS_NUMERIC = "0123456789";
            string PASSWORD_CHARS_SPECIAL = "*$-+?_&=!%{}/";
            Random objRndm = new Random();
            int nextCharIdx;
            int nextPwdIndex = 0;
            int noChars = 0;
            char[][] charGroups = new char[][]
                                      {
                                          PASSWORD_CHARS_LCASE.ToCharArray(),
                                          PASSWORD_CHARS_UCASE.ToCharArray(),
                                          PASSWORD_CHARS_NUMERIC.ToCharArray(),
                                          PASSWORD_CHARS_SPECIAL.ToCharArray()
                                      };
            int NoCharsOfType = 0;
            string strPassWord = string.Empty;
            PasswordPolicyAdaptor adaptorPwdPolicyConfig = new PasswordPolicyAdaptor();
            PasswordPolicyConfiguration entPasswordPolicyConfiguration = new PasswordPolicyConfiguration();
            PasswordPolicyConfiguration entPwdPolicyConfigReturn = new PasswordPolicyConfiguration();

            entPasswordPolicyConfiguration.ClientId = pClinetID;
            string strClientKey = pClinetID;
            try
            {
                entPwdPolicyConfigReturn = adaptorPwdPolicyConfig.GetPasswordPolicyById(entPasswordPolicyConfiguration);

                entPwdPolicyConfigReturn.ClientId = strClientKey;
                entPasswordPolicyConfiguration = entPwdPolicyConfigReturn;
                //AddPolicyToCache(entPwdPolicyConfigReturn);
                LMSCache.AddCacheItem(entPwdPolicyConfigReturn.ClientId + PasswordPolicyConfiguration.CACHE_SUFFIX, entPwdPolicyConfigReturn, entPwdPolicyConfigReturn.ClientId);
                if (entPasswordPolicyConfiguration != null)
                {
                    int maxLength = entPasswordPolicyConfiguration.MaxPaswordLength;
                    int minLength = entPasswordPolicyConfiguration.MinPaswordLength;
                    strPassWord = entPasswordPolicyConfiguration.DefaultPassword;
                    if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
                        return strPassWord;
                    char[] passWord = new char[objRndm.Next(minLength, maxLength + 1)];
                    if (entPasswordPolicyConfiguration.IsSpecialCaracter)
                        noChars += 1;
                    if (entPasswordPolicyConfiguration.IsUpperCase)
                        noChars += 1;
                    if (entPasswordPolicyConfiguration.IsLowerCase)
                        noChars += 1;
                    if (entPasswordPolicyConfiguration.IsNumber)
                        noChars += 1;
                    if (noChars > 0)
                        NoCharsOfType = passWord.Length / noChars;
                    else
                        NoCharsOfType = passWord.Length;
                    if (entPasswordPolicyConfiguration.IsLowerCase)
                    {
                        for (int i = 0; i < NoCharsOfType; i++)
                        {
                            nextCharIdx = objRndm.Next(0, PASSWORD_CHARS_LCASE.Length - i);
                            if (nextPwdIndex < passWord.Length)
                                passWord[nextPwdIndex] = charGroups[0][nextCharIdx];
                            nextPwdIndex = nextPwdIndex + 1;
                        }
                    }
                    if (entPasswordPolicyConfiguration.IsUpperCase)
                    {
                        for (int i = 0; i < NoCharsOfType; i++)
                        {
                            nextCharIdx = objRndm.Next(0, PASSWORD_CHARS_UCASE.Length - i);
                            if (nextPwdIndex < passWord.Length)
                            {
                                passWord[nextPwdIndex] = charGroups[1][nextCharIdx];
                            }
                            nextPwdIndex = nextPwdIndex + 1;
                        }
                    }
                    if (entPasswordPolicyConfiguration.IsNumber)
                    {
                        for (int i = 0; i < NoCharsOfType; i++)
                        {
                            {
                                nextCharIdx = objRndm.Next(0, PASSWORD_CHARS_NUMERIC.Length - i);
                            }
                            if (nextPwdIndex < passWord.Length)
                            {
                                passWord[nextPwdIndex] = charGroups[2][nextCharIdx];
                            }
                            nextPwdIndex = nextPwdIndex + 1;
                        }
                    }
                    if (entPasswordPolicyConfiguration.IsSpecialCaracter)
                    {
                        for (int i = 0; i < NoCharsOfType; i++)
                        {
                            nextCharIdx = objRndm.Next(0, PASSWORD_CHARS_SPECIAL.Length - i);
                            if (nextPwdIndex < passWord.Length)
                            {
                                passWord[nextPwdIndex] = charGroups[3][nextCharIdx];
                            }
                            nextPwdIndex = nextPwdIndex + 1;
                        }
                    }
                    int intRemaChars = passWord.Length - nextPwdIndex;
                    for (int i = 0; i < intRemaChars; i++)
                    {
                        nextCharIdx = objRndm.Next(0, PASSWORD_CHARS_LCASE.Length - i);
                        if (nextPwdIndex < passWord.Length)
                        {
                            passWord[nextPwdIndex] = charGroups[0][nextCharIdx];
                        }
                        nextPwdIndex = nextPwdIndex + 1;
                    }
                    strPassWord = "";
                    for (int i = 0; i < passWord.Length; i++)
                    {
                        strPassWord = strPassWord + passWord[i].ToString();
                    }
                    return strPassWord;
                }
            }
            catch (Exception expCommon)
            {
                new CustomException(PasswordPolicy.PWD_POLICY_CONFIG_BL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
            }
            return strPassWord;
        }

        /// <summary>
        /// Generat password from password policy
        /// </summary>
        /// <returns>returns password as per policy </returns>
        public static string CreatePassword(string pClinetID, PasswordPolicyConfiguration pentPasswordPolicyConfiguration)
        {
            string PASSWORD_CHARS_LCASE = "abcdefgijklmnopqrstuwxyz";
            string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTUWXYZ";
            string PASSWORD_CHARS_NUMERIC = "0123456789";
            string PASSWORD_CHARS_SPECIAL = "*$-+?_&=!%{}/";
            Random objRndm = new Random();
            int nextCharIdx;
            int nextPwdIndex = 0;
            int noChars = 0;
            char[][] charGroups = new char[][]
                                      {
                                          PASSWORD_CHARS_LCASE.ToCharArray(),
                                          PASSWORD_CHARS_UCASE.ToCharArray(),
                                          PASSWORD_CHARS_NUMERIC.ToCharArray(),
                                          PASSWORD_CHARS_SPECIAL.ToCharArray()
                                      };
            int NoCharsOfType = 0;
            string strPassWord = string.Empty;
            try
            {
                if (pentPasswordPolicyConfiguration != null)
                {
                    int maxLength = pentPasswordPolicyConfiguration.MaxPaswordLength;
                    int minLength = pentPasswordPolicyConfiguration.MinPaswordLength;
                    strPassWord = pentPasswordPolicyConfiguration.DefaultPassword;
                    if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
                        return strPassWord;
                    char[] passWord = new char[objRndm.Next(minLength, maxLength + 1)];
                    if (pentPasswordPolicyConfiguration.IsSpecialCaracter)
                        noChars += 1;
                    if (pentPasswordPolicyConfiguration.IsUpperCase)
                        noChars += 1;
                    if (pentPasswordPolicyConfiguration.IsLowerCase)
                        noChars += 1;
                    if (pentPasswordPolicyConfiguration.IsNumber)
                        noChars += 1;
                    if (noChars > 0)
                        NoCharsOfType = passWord.Length / noChars;
                    else
                        NoCharsOfType = passWord.Length;
                    if (pentPasswordPolicyConfiguration.IsLowerCase)
                    {
                        for (int i = 0; i < NoCharsOfType; i++)
                        {
                            nextCharIdx = objRndm.Next(0, PASSWORD_CHARS_LCASE.Length - i);
                            if (nextPwdIndex < passWord.Length)
                                passWord[nextPwdIndex] = charGroups[0][nextCharIdx];
                            nextPwdIndex = nextPwdIndex + 1;
                        }
                    }
                    if (pentPasswordPolicyConfiguration.IsUpperCase)
                    {
                        for (int i = 0; i < NoCharsOfType; i++)
                        {
                            nextCharIdx = objRndm.Next(0, PASSWORD_CHARS_UCASE.Length - i);
                            if (nextPwdIndex < passWord.Length)
                            {
                                passWord[nextPwdIndex] = charGroups[1][nextCharIdx];
                            }
                            nextPwdIndex = nextPwdIndex + 1;
                        }
                    }
                    if (pentPasswordPolicyConfiguration.IsNumber)
                    {
                        for (int i = 0; i < NoCharsOfType; i++)
                        {
                            {
                                nextCharIdx = objRndm.Next(0, PASSWORD_CHARS_NUMERIC.Length - i);
                            }
                            if (nextPwdIndex < passWord.Length)
                            {
                                passWord[nextPwdIndex] = charGroups[2][nextCharIdx];
                            }
                            nextPwdIndex = nextPwdIndex + 1;
                        }
                    }
                    if (pentPasswordPolicyConfiguration.IsSpecialCaracter)
                    {
                        for (int i = 0; i < NoCharsOfType; i++)
                        {
                            nextCharIdx = objRndm.Next(0, PASSWORD_CHARS_SPECIAL.Length - i);
                            if (nextPwdIndex < passWord.Length)
                            {
                                passWord[nextPwdIndex] = charGroups[3][nextCharIdx];
                            }
                            nextPwdIndex = nextPwdIndex + 1;
                        }
                    }
                    int intRemaChars = passWord.Length - nextPwdIndex;
                    for (int i = 0; i < intRemaChars; i++)
                    {
                        nextCharIdx = objRndm.Next(0, PASSWORD_CHARS_LCASE.Length - i);
                        if (nextPwdIndex < passWord.Length)
                        {
                            passWord[nextPwdIndex] = charGroups[0][nextCharIdx];
                        }
                        nextPwdIndex = nextPwdIndex + 1;
                    }
                    strPassWord = "";
                    for (int i = 0; i < passWord.Length; i++)
                    {
                        strPassWord = strPassWord + passWord[i].ToString();
                    }
                    return strPassWord;
                }
                else
                {
                    return CreatePassword(pClinetID);
                }
            }
            catch (Exception expCommon)
            {
                new CustomException(PasswordPolicy.PWD_POLICY_CONFIG_BL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
            }
            return strPassWord;
        }
    }
}
