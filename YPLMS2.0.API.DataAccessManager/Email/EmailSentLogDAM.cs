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
    public class EmailSentLogDAM : IDataManager<EmailSentLog>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlParameter _sqlpara = null;
        SqlCommand _sqlcmd = null;
        EntityRange _entRange = null;
        SqlConnection _sqlConnection = null;
        string _strMessageId = YPLMS.Services.Messages.EmailMessages.EMAIL_ERROR;
        string _strConnString = string.Empty;
        SQLObject _sqlObject = null;
        #endregion

        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>EmailSentLog Object</returns>
        private EmailSentLog FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            EmailSentLog entEmailSentLog = new EmailSentLog();
            EmailTemplate emailTemplate = new EmailTemplate();
            EmailDeliveryDashboard emailDeliveryDashboard = new EmailDeliveryDashboard();
            int iIndex = 0;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.EmailSentLog.COL_EMAIL_DISPATCH_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailSentLog.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailSentLog.COL_EMAIL_DELIVERY_INSTANCE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailSentLog.EmailDeliveryInstanceId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailSentLog.COL_AUTO_EMAIL_EVENT_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailSentLog.AutoEmailEventId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailSentLog.COL_RECIPIANT_EMAIL_ADDRESS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailSentLog.RecipiantEmailAddress = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailSentLog.COL_DATE_SENT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailSentLog.DateSent = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailSentLog.COL_RECIPIANT_BCC_EMAIL_ADDRESS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailSentLog.RecipiantBCCEmailAddress = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailSentLog.COL_RECIPIANT_CC_EMAIL_ADDRESS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailSentLog.RecipiantCCEmailAddress = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailSentLog.COL_EMAIL_TEMPLATE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailSentLog.EmailTemplateId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailSentLog.COL_EMAIL_MSG_FILE_NAME_PATH);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailSentLog.EmailMsgFileNamePath = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailSentLog.COL_LOG);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailSentLog.EmailLog = pSqlReader.GetString(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.EmailTemplate.COL_EMAIL_TEMPLATE_DEFAULT_TITLE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_EMAIL_TEMPLATE_DEFAULT_TITLE);
                    if (!pSqlReader.IsDBNull(iIndex))
                    {
                        emailTemplate.EmailTemplateDefaultTitle = pSqlReader.GetString(iIndex);
                        entEmailSentLog.EmailTemplate = emailTemplate;
                    }
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.EmailDeliveryDashboard.COL_EMAIL_DELIVERY_TITLE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_EMAIL_DELIVERY_TITLE);
                    if (!pSqlReader.IsDBNull(iIndex))
                    {
                        emailDeliveryDashboard.EmailDeliveryTitle = pSqlReader.GetString(iIndex);
                        entEmailSentLog.EmailDeliveryDashboard = emailDeliveryDashboard;
                    }
                }
                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    entEmailSentLog.ListRange = _entRange;
                }
            }
            return entEmailSentLog;
        }

        /// <summary>
        /// Get EmailSentLog Details By Id
        /// </summary>
        /// <returns>List of EmailSentLog Object</returns>
        public EmailSentLog GetEmailSentLogById(EmailSentLog pEntEmailSentLog)
        {
            EmailSentLog entEmailSentLog = null;
            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(pEntEmailSentLog.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.EmailSentLog.PROC_GET_EMAIL_SENT_LOG, _sqlConnection);
            _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_EMAIL_DISPATCH_ID, pEntEmailSentLog.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            entEmailSentLog = new EmailSentLog();
            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    entEmailSentLog = FillObject(_sqlreader, false, _sqlObject);
                }
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
                if (_sqlConnection != null && _sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.Close();
            }
            return entEmailSentLog;
        }

        /// <summary>
        /// To update/add new Email Template
        /// </summary>
        /// <param name="pEntEmailSentLog"></param>        
        /// <returns>EmailSentLog object</returns>
        public EmailSentLog AddUpdateEmailSentLog(EmailSentLog pEntEmailSentLog)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.EmailSentLog.PROC_UPDATE_EMAIL_SENT_LOG;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntEmailSentLog.ClientId);
                if (string.IsNullOrEmpty(pEntEmailSentLog.ID))
                {
                    //pEntEmailSentLog.ID = Services.IDGenerator.GetUniqueKey(8);
                    pEntEmailSentLog.ID = YPLMS.Services.IDGenerator.GetStringGUID();
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                }
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_EMAIL_DISPATCH_ID, pEntEmailSentLog.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailSentLog.EmailDeliveryInstanceId))
                    _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_EMAIL_DELIVERY_INSTANCE_ID, pEntEmailSentLog.EmailDeliveryInstanceId);
                else
                    _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_EMAIL_DELIVERY_INSTANCE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailSentLog.AutoEmailEventId))
                    _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_AUTO_EMAIL_EVENT_ID, pEntEmailSentLog.AutoEmailEventId);
                else
                    _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_AUTO_EMAIL_EVENT_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailSentLog.RecipiantEmailAddress))
                    _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_RECIPIANT_EMAIL_ADDRESS, pEntEmailSentLog.RecipiantEmailAddress);
                else
                    _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_RECIPIANT_EMAIL_ADDRESS, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailSentLog.RecipiantBCCEmailAddress))
                    _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_RECIPIANT_BCC_EMAIL_ADDRESS, pEntEmailSentLog.RecipiantBCCEmailAddress);
                else
                    _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_RECIPIANT_BCC_EMAIL_ADDRESS, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailSentLog.RecipiantCCEmailAddress))
                    _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_RECIPIANT_CC_EMAIL_ADDRESS, pEntEmailSentLog.RecipiantCCEmailAddress);
                else
                    _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_RECIPIANT_CC_EMAIL_ADDRESS, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailSentLog.EmailTemplateId))
                    _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_EMAIL_TEMPLATE_ID, pEntEmailSentLog.EmailTemplateId);
                else
                    _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_EMAIL_TEMPLATE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailSentLog.EmailMsgFileNamePath))
                    _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_EMAIL_MSG_FILE_NAME_PATH, pEntEmailSentLog.EmailMsgFileNamePath);
                else
                    _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_EMAIL_MSG_FILE_NAME_PATH, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailSentLog.EmailLog))
                    _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_LOG, pEntEmailSentLog.EmailLog);
                else
                    _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_LOG, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_DATE_SENT, pEntEmailSentLog.DateSent);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailSentLog.SystemUserGuId))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntEmailSentLog.SystemUserGuId);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);



                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntEmailSentLog;
        }

        /// <summary>
        /// Find Email Sent Log
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<EmailSentLog> FindEmailSentLog(Search pEntSearch)
        {
            EmailSentLog entEmailSentLog = null;
            List<EmailSentLog> entListEmailSentLog = null;
            _sqlObject = new SQLObject();

            _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.EmailSentLog.PROC_FIND_EMAIL_SENT_LOG, _sqlConnection);
            entEmailSentLog = new EmailSentLog();
            entListEmailSentLog = new List<EmailSentLog>();

            if (pEntSearch.ListRange != null)
            {
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntSearch.ListRange.PageIndex);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntSearch.ListRange.PageSize);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            if (pEntSearch.SearchObject != null && pEntSearch.SearchObject.Count > 0)
            {
                entEmailSentLog = new EmailSentLog();
                entEmailSentLog = (EmailSentLog)pEntSearch.SearchObject[0];
                if (DateTime.MinValue.CompareTo(entEmailSentLog.DateSent) < 0)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_FROM_DATE, entEmailSentLog.DateSent);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntSearch.SearchObject.Count > 1)
                {
                    entEmailSentLog = new EmailSentLog();
                    entEmailSentLog = (EmailSentLog)pEntSearch.SearchObject[1];
                    if (DateTime.MinValue.CompareTo(entEmailSentLog.DateSent) < 0)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_TO_DATE, entEmailSentLog.DateSent);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }

                entEmailSentLog = new EmailSentLog();
                entEmailSentLog = (EmailSentLog)pEntSearch.SearchObject[2];
                if (entEmailSentLog != null)
                {
                    if (entEmailSentLog.EmailDeliveryDashboard != null)
                    {
                        if (!string.IsNullOrEmpty(entEmailSentLog.EmailDeliveryDashboard.EmailDeliveryTitle))
                        {
                            _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_EMAIL_DELIVERY_TITLE, entEmailSentLog.EmailDeliveryDashboard.EmailDeliveryTitle);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                }


                entEmailSentLog = new EmailSentLog();
                entEmailSentLog = (EmailSentLog)pEntSearch.SearchObject[3];
                if (entEmailSentLog != null)
                {
                    if (entEmailSentLog.EmailTemplate != null)
                    {
                        if (!string.IsNullOrEmpty(entEmailSentLog.EmailTemplate.EmailTemplateDefaultTitle))
                        {
                            _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_EMAIL_TEMPLATE_DEFAULT_TITLE, entEmailSentLog.EmailTemplate.EmailTemplateDefaultTitle);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                }


                entEmailSentLog = new EmailSentLog();
                entEmailSentLog = (EmailSentLog)pEntSearch.SearchObject[4];
                if (!string.IsNullOrEmpty(entEmailSentLog.RecipiantEmailAddress))
                {
                    _sqlpara = new SqlParameter(Schema.EmailSentLog.PARA_EMAIL_ID, entEmailSentLog.RecipiantEmailAddress);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
            }
            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entEmailSentLog = FillObject(_sqlreader, true, _sqlObject);
                    if (entEmailSentLog != null)
                        entListEmailSentLog.Add(entEmailSentLog);
                }
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
                if (_sqlConnection != null && _sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.Close();
            }
            return entListEmailSentLog;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntEmailSentLog"></param>
        /// <returns></returns>
        public EmailSentLog Get(EmailSentLog pEntEmailSentLog)
        {
            return GetEmailSentLogById(pEntEmailSentLog);
        }
        /// <summary>
        /// Update EmailSentLog
        /// </summary>
        /// <param name="pEntEmailSentLog"></param>
        /// <returns></returns>
        public EmailSentLog Update(EmailSentLog pEntEmailSentLog)
        {
            return AddUpdateEmailSentLog(pEntEmailSentLog);
        }
        #endregion
    }
}
