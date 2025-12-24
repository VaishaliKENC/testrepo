using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    public class PasswordPolicyAdaptor : IDataManager<PasswordPolicyConfiguration>, IPasswordPolicyAdaptor<PasswordPolicyConfiguration>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.PasswordPolicy.PWD_POLICY_CONFIG_BL_ERROR;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// To Get PasswordPolicyConfiguration details by PasswordPolicyConfiguration Id.
        /// </summary>
        /// <param name="pEntPwdPolicyConfig"></param>
        /// <returns>PasswordPolicyConfiguration Object</returns>
        public PasswordPolicyConfiguration GetPasswordPolicyById(PasswordPolicyConfiguration pEntPwdPolicyConfig)
        {
            _sqlObject = new SQLObject();
            PasswordPolicyConfiguration entPwdPolicyConfig = new PasswordPolicyConfiguration();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntPwdPolicyConfig.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.PasswordPolicyConfiguration.PROC_GET_ALL_PASSWORD_POLICY_CONFIGURATION, sqlConnection);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entPwdPolicyConfig = FillObject(_sqlreader, false);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed)
                    _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entPwdPolicyConfig;
        }

        /// <summary>
        ///  To Fill PasswordPolicyConfiguration object properties values from reader data 
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>PasswordPolicyConfiguration Object</returns>
        private PasswordPolicyConfiguration FillObject(SqlDataReader pSqlReader, bool pRangeList)
        {
            PasswordPolicyConfiguration entPwdPolicyConfig = new PasswordPolicyConfiguration();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_PWD_POLICY_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_PWD_POLICY_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.ID = pSqlReader.GetString(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_MAX_PWD_LEN))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_MAX_PWD_LEN);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.MaxPaswordLength = pSqlReader.GetInt32(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_MIN_PWD_LEN))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_MIN_PWD_LEN);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.MinPaswordLength = pSqlReader.GetInt32(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_DEFAULT_PWD))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_DEFAULT_PWD);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.DefaultPassword = pSqlReader.GetString(iIndex);
                    else
                        entPwdPolicyConfig.DefaultPassword = "";
                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_IS_CONFIRM_PWD))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_IS_CONFIRM_PWD);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.IsConfirmPassword = pSqlReader.GetBoolean(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_PWD_EXPIRY_DURATION))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_PWD_EXPIRY_DURATION);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.PasswordExpiryDuration = pSqlReader.GetInt32(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_MAX_LOGIN_ATTEMPTS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_MAX_LOGIN_ATTEMPTS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.MaxLoginAttempts = pSqlReader.GetInt32(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_PWD_NEVER_EXPIRES))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_PWD_NEVER_EXPIRES);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.PasswordNeverExpires = pSqlReader.GetBoolean(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_PWD_CAN_NOT_CHANGE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_PWD_CAN_NOT_CHANGE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.PasswordCannotChange = pSqlReader.GetBoolean(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_IS_PRE_PWD_ALLOWED))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_IS_PRE_PWD_ALLOWED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.IsPrevPasswordAllowed = pSqlReader.GetBoolean(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_PRE_PWD_HISTORY_COUNT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_PRE_PWD_HISTORY_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.PrevPasswordHistoryCount = pSqlReader.GetInt32(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_IS_UPPER_CASE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_IS_UPPER_CASE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.IsUpperCase = pSqlReader.GetBoolean(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_IS_LOWER_CASE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_IS_LOWER_CASE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.IsLowerCase = pSqlReader.GetBoolean(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_IS_NUMBER))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_IS_NUMBER);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.IsNumber = pSqlReader.GetBoolean(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_IS_SPECIAL_CHAR))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_IS_SPECIAL_CHAR);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.IsSpecialCaracter = pSqlReader.GetBoolean(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.Client.COL_CLIENT_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.ClientId = pSqlReader.GetString(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.CreatedById = pSqlReader.GetString(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.DateCreated = pSqlReader.GetDateTime(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.LastModifiedById = pSqlReader.GetString(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.LastModifiedDate = pSqlReader.GetDateTime(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_IS_PasswordChange))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_IS_PasswordChange);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.IsPasswordChange = pSqlReader.GetBoolean(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_IS_DEFALUTPASSWORD_FORUI))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_IS_DEFALUTPASSWORD_FORUI);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.DefaultPasswordforUI = pSqlReader.GetBoolean(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_OTP_PWD_EXPIRY_HRS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_OTP_PWD_EXPIRY_HRS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.OTPPasswordExpiryHrs = pSqlReader.GetInt32(iIndex);
                }
                //Samreen 7th Oct 2021
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_NO_OF_REQUESTS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_NO_OF_REQUESTS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.NoOfRequest = pSqlReader.GetInt32(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_REQUEST_VALIDITY_TIME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_REQUEST_VALIDITY_TIME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.RequestValidityTime = pSqlReader.GetInt32(iIndex);
                }


                //Samreen 10th May 2022
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_NO_OF_OTP_EMAILREQUESTS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_NO_OF_OTP_EMAILREQUESTS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.NoOfOtpEmailRequests = pSqlReader.GetInt32(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_REQUEST_VALIDITY_TIME_FOR_OTP))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_REQUEST_VALIDITY_TIME_FOR_OTP);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.RequestValidityTimeForOtp = pSqlReader.GetInt32(iIndex);
                }

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    entPwdPolicyConfig.ListRange = _entRange;
                }
            }
            return entPwdPolicyConfig;
        }


        /// <summary>
        ///  To Fill PasswordPolicyConfiguration object properties values from reader data 
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>PasswordPolicyConfiguration Object</returns>
        private PasswordPolicyConfiguration FillObjectForgotPassword(SqlDataReader pSqlReader, bool pRangeList)
        {
            PasswordPolicyConfiguration entPwdPolicyConfig = new PasswordPolicyConfiguration();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                //Added for Email Request
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_SYSTEMUSERGUID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_SYSTEMUSERGUID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.RequestValidityTime = pSqlReader.GetInt32(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.PasswordPolicyConfiguration.COL_NO_OF_EMAIL_REQUESTS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.PasswordPolicyConfiguration.COL_NO_OF_EMAIL_REQUESTS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPwdPolicyConfig.RequestValidityTime = pSqlReader.GetInt32(iIndex);
                }

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    entPwdPolicyConfig.ListRange = _entRange;
                }

            }
            return entPwdPolicyConfig;
        }

        /// <summary>
        /// To Add new PasswordPolicyConfiguration
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <returns>PasswordPolicyConfiguration Object</returns>
        public PasswordPolicyConfiguration AddPasswordPolicyConfiguration(PasswordPolicyConfiguration pEntPwdPolicyConfig)
        {
            PasswordPolicyConfiguration entPwdPolicyConfig = new PasswordPolicyConfiguration();
            try
            {
                entPwdPolicyConfig = Update(pEntPwdPolicyConfig, Schema.Common.VAL_INSERT_MODE);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entPwdPolicyConfig;
        }

        /// <summary>
        /// To Edit the PasswordPolicyConfiguration data 
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <returns>PasswordPolicyConfiguration Object</returns>
        public PasswordPolicyConfiguration EditPasswordPolicyConfiguration(PasswordPolicyConfiguration pEntPwdPolicyConfig)
        {
            PasswordPolicyConfiguration entPwdPolicyConfig = new PasswordPolicyConfiguration();
            try
            {
                entPwdPolicyConfig = Update(pEntPwdPolicyConfig, Schema.Common.VAL_UPDATE_MODE);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entPwdPolicyConfig;
        }

        /// <summary>
        /// private method to support both Add and Edit PasswordPolicyConfiguration transactions.
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <param name="pUpdateMode"></param>
        /// <returns>PasswordPolicyConfiguration Object</returns>
        private PasswordPolicyConfiguration Update(PasswordPolicyConfiguration pEntPwdPolicyConfig, string pStrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.PasswordPolicyConfiguration.PROC_UPDATE_PASSWORD_POLICY_CONFIGURATION;
            _strConnString = _sqlObject.GetClientDBConnString(pEntPwdPolicyConfig.ClientId);
            if (pStrUpdateMode == Schema.Common.VAL_INSERT_MODE)
            {
                pEntPwdPolicyConfig.ID = YPLMS.Services.IDGenerator.GetUniqueKey(Schema.Common.VAL_PWD_POLICY_CONFIG_ID_LENGTH);
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
            }
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);

            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_PWD_POLICY_ID, pEntPwdPolicyConfig.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_MAX_PWD_LEN, pEntPwdPolicyConfig.MaxPaswordLength);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_MIN_PWD_LEN, pEntPwdPolicyConfig.MinPaswordLength);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_DEFAULT_PWD, pEntPwdPolicyConfig.DefaultPassword);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_IS_CONFIRM_PWD, pEntPwdPolicyConfig.IsConfirmPassword);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_PWD_EXPIRY_DURATION, pEntPwdPolicyConfig.PasswordExpiryDuration);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_MAX_LOGIN_ATTEMPTS, pEntPwdPolicyConfig.MaxLoginAttempts);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_PWD_NEVER_EXPIRES, pEntPwdPolicyConfig.PasswordNeverExpires);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_PWD_CAN_NOT_CHANGE, pEntPwdPolicyConfig.PasswordCannotChange);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_IS_PRE_PWD_ALLOWED, pEntPwdPolicyConfig.IsPrevPasswordAllowed);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_PRE_PWD_HISTORY_COUNT, pEntPwdPolicyConfig.PrevPasswordHistoryCount);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_IS_UPPER_CASE, pEntPwdPolicyConfig.IsUpperCase);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_IS_LOWER_CASE, pEntPwdPolicyConfig.IsLowerCase);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_IS_NUMBER, pEntPwdPolicyConfig.IsNumber);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_IS_SPECIAL_CHAR, pEntPwdPolicyConfig.IsSpecialCaracter);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntPwdPolicyConfig.ClientId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntPwdPolicyConfig.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_IS_PASSWORD_CHANGE, pEntPwdPolicyConfig.IsPasswordChange);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_IS_DEFALUTPASSWORD_FORUI, pEntPwdPolicyConfig.DefaultPasswordforUI);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_OTP_PWD_EXPIRY_HRS, pEntPwdPolicyConfig.OTPPasswordExpiryHrs);
            _sqlcmd.Parameters.Add(_sqlpara);

            //Samreen 7th Oct 2021
            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_NO_OF_REQUESTS, pEntPwdPolicyConfig.NoOfRequest);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_REQUEST_VALIDITY_TIME, pEntPwdPolicyConfig.RequestValidityTime);
            _sqlcmd.Parameters.Add(_sqlpara);

            //Samreen 10th May 2022
            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_NO_OF_OTP_EMAILREQUESTS, pEntPwdPolicyConfig.NoOfOtpEmailRequests);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_REQUEST_VALIDITY_TIME_FOR_OTP, pEntPwdPolicyConfig.RequestValidityTimeForOtp);
            _sqlcmd.Parameters.Add(_sqlpara);

            int iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            return pEntPwdPolicyConfig;
        }



        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntPasswordPolicyConfiguration"></param>
        /// <returns></returns>
        public PasswordPolicyConfiguration Get(PasswordPolicyConfiguration pEntPasswordPolicyConfiguration)
        {
            return GetPasswordPolicyById(pEntPasswordPolicyConfiguration);
        }
        /// <summary>
        /// Update PasswordPolicyConfiguration
        /// </summary>
        /// <param name="pEntPasswordPolicyConfiguration"></param>
        /// <returns></returns>
        public PasswordPolicyConfiguration Update(PasswordPolicyConfiguration pEntPasswordPolicyConfiguration)
        {
            return EditPasswordPolicyConfiguration(pEntPasswordPolicyConfiguration);
        }

        public PasswordPolicyConfiguration GetEmailRequestDetails(PasswordPolicyConfiguration pEntPasswordPolicyConfiguration)
        {
            _sqlObject = new SQLObject();
            PasswordPolicyConfiguration entPwdPolicyConfig = new PasswordPolicyConfiguration();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntPasswordPolicyConfiguration.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.PasswordPolicyConfiguration.PROC_GET_EMAILREQUESTS_DETAILS, sqlConnection);

                if (!String.IsNullOrEmpty(pEntPasswordPolicyConfiguration.LearnerId))
                {
                    _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.COL_SYSTEMUSERGUID, pEntPasswordPolicyConfiguration.LearnerId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                /*if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.EmailID))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_EMAILID, pEntVirtualTrainingSessionMaster.EmailID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }*/

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entPwdPolicyConfig = FillObjectForgotPassword(_sqlreader, false);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed)
                    _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entPwdPolicyConfig;
        }


        public PasswordPolicyConfiguration AddUpdateEmailRequests(PasswordPolicyConfiguration pEntPwdPolicyConfig)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.PasswordPolicyConfiguration.PROC_ADDUPDATE_NO_OF_EMAILREQUESTS;
            _strConnString = _sqlObject.GetClientDBConnString(pEntPwdPolicyConfig.ClientId);

            SqlConnection sqlConnection = null;
            sqlConnection = new SqlConnection(_strConnString);
            sqlConnection.Open();

            _sqlcmd = new SqlCommand(Schema.PasswordPolicyConfiguration.PROC_ADDUPDATE_NO_OF_EMAILREQUESTS, sqlConnection);

            if (string.IsNullOrEmpty(pEntPwdPolicyConfig.EmailRequestID))
            {
                pEntPwdPolicyConfig.EmailRequestID = YPLMS.Services.IDGenerator.GetStringGUIDWithPrefix(Schema.Common.VAL_EMAIL_REQUEST_PREFIX);
            }

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_EMAIL_REQUEST_ID, pEntPwdPolicyConfig.EmailRequestID);
            _sqlcmd.Parameters.Add(_sqlpara);

            //_sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_SYSTEMUSERGUID, pEntPwdPolicyConfig.LearnerId);
            //_sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_SYSTEMUSERGUID,
                string.IsNullOrEmpty(pEntPwdPolicyConfig.LearnerId)? DBNull.Value : (object)pEntPwdPolicyConfig.LearnerId);

            _sqlcmd.Parameters.Add(_sqlpara);


            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_NO_OF_EMAIL_REQUESTS, SqlDbType.VarChar, 100);
            _sqlpara.Direction = ParameterDirection.Output;

            _sqlcmd.Parameters.Add(_sqlpara);

            bool retValue = _sqlObject.ExecuteNonQuery(_sqlcmd);

            pEntPwdPolicyConfig.NoOfCurrentEmailRequest = Convert.ToInt16(_sqlcmd.Parameters[Schema.PasswordPolicyConfiguration.PARA_NO_OF_EMAIL_REQUESTS].Value);
            return pEntPwdPolicyConfig;
        }


        public PasswordPolicyConfiguration AddUpdateOTPEmailRequests(PasswordPolicyConfiguration pEntPwdPolicyConfig)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.PasswordPolicyConfiguration.PROC_ADDUPDATE_OTP_NO_OF_EMAILREQUESTS;
            _strConnString = _sqlObject.GetClientDBConnString(pEntPwdPolicyConfig.ClientId);

            SqlConnection sqlConnection = null;
            sqlConnection = new SqlConnection(_strConnString);
            sqlConnection.Open();

            _sqlcmd = new SqlCommand(Schema.PasswordPolicyConfiguration.PROC_ADDUPDATE_OTP_NO_OF_EMAILREQUESTS, sqlConnection);

            if (string.IsNullOrEmpty(pEntPwdPolicyConfig.EmailRequestID))
            {
                pEntPwdPolicyConfig.EmailRequestID = YPLMS.Services.IDGenerator.GetStringGUIDWithPrefix(Schema.Common.VAL_EMAIL_REQUEST_PREFIX);
            }

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_EMAIL_REQUEST_ID, pEntPwdPolicyConfig.EmailRequestID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_SYSTEMUSERGUID, pEntPwdPolicyConfig.LearnerId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PasswordPolicyConfiguration.PARA_NO_OF_EMAIL_REQUESTS, SqlDbType.VarChar, 100);
            _sqlpara.Direction = ParameterDirection.Output;

            _sqlcmd.Parameters.Add(_sqlpara);

            bool retValue = _sqlObject.ExecuteNonQuery(_sqlcmd);

            pEntPwdPolicyConfig.NoOfCurrentEmailRequest = Convert.ToInt16(_sqlcmd.Parameters[Schema.PasswordPolicyConfiguration.PARA_NO_OF_EMAIL_REQUESTS].Value);
            return pEntPwdPolicyConfig;
        }


        #endregion
    }
}
