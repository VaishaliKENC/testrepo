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
    public class MessagesAdaptor : IDataManager<Message>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.Document.DOCUMENT_BL_ERROR;
        string strDocumentAssignMsgId = YPLMS.Services.Messages.DocumentLibrary.DOCUMENT_LIBRARY_ASSIGN;
        string _strConnString = string.Empty;
        #endregion

        public Message GetMessage(Message pEntMessage)
        {
            _sqlObject = new SQLObject();
            Message entMessage = new Message();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                _sqlcmd = new SqlCommand(Schema.Messages.PROC_GET_MESSAGE, sqlConnection);

                _sqlpara = new SqlParameter(Schema.Messages.PARA_MESSAGE_ID, pEntMessage.MessageID);
                _sqlcmd.Parameters.Add(_sqlpara);

                //_sqlpara = new SqlParameter(Schema.Messages.PARA_RECIPIENT_ID, pEntMessage.RecipientID);
                //_sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entMessage = FillObject(_sqlreader, false, _sqlObject);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entMessage;
        }

        public List<Message> GetMessageList(Message pEntMessage)
        {
            _sqlObject = new SQLObject();
            List<Message> entListMessage = new List<Message>();
            Message entMessage = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Messages.PROC_GET_ALL_MESSAGE, sqlConnection);

                if (!string.IsNullOrEmpty(pEntMessage.MessageTitle))
                    _sqlpara = new SqlParameter(Schema.Messages.PARA_MESSAGE_TITLE, pEntMessage.MessageTitle);
                else
                    _sqlpara = new SqlParameter(Schema.Messages.PARA_MESSAGE_TITLE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntMessage.DateCreated != DateTime.MinValue)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_DATE_FROM, pEntMessage.DateCreated);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_DATE_FROM, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntMessage.DateCreatedTo != DateTime.MinValue)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_DATE_TO, pEntMessage.DateCreatedTo);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_DATE_TO, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntMessage.RecipientID))
                    _sqlpara = new SqlParameter(Schema.Messages.PARA_RECIPIENT_ID, pEntMessage.RecipientID);
                else
                    _sqlpara = new SqlParameter(Schema.Messages.PARA_RECIPIENT_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);


                if (pEntMessage.ListRange != null)
                {
                    if (pEntMessage.ListRange.PageIndex > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntMessage.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntMessage.ListRange.PageSize > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntMessage.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntMessage.ListRange.SortExpression))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntMessage.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntMessage.ListRange.RequestedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntMessage.ListRange.RequestedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entMessage = FillObject(_sqlreader, true, _sqlObject);
                    entListMessage.Add(entMessage);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entListMessage;
        }

        public List<Message> GetMessageListLearner_Top10(Message pEntMessage)
        {
            _sqlObject = new SQLObject();
            List<Message> entListMessage = new List<Message>();
            Message entMessage = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Messages.PROC_GET_TOP10_MESSAGE_LEARNER, sqlConnection);

                if (!string.IsNullOrEmpty(pEntMessage.RecipientID))
                    _sqlpara = new SqlParameter(Schema.Messages.PARA_RECIPIENT_ID, pEntMessage.RecipientID);
                else
                    _sqlpara = new SqlParameter(Schema.Messages.PARA_RECIPIENT_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                //if (!string.IsNullOrEmpty(pEntMessage.LanguageId))
                //    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntMessage.LanguageId);
                //else
                //    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, null);
                //_sqlcmd.Parameters.Add(_sqlpara);


                if (pEntMessage.ListRange != null)
                {
                    if (pEntMessage.ListRange.PageIndex > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntMessage.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntMessage.ListRange.PageSize > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntMessage.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntMessage.ListRange.SortExpression))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntMessage.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entMessage = FillObject(_sqlreader, true, _sqlObject);
                    entListMessage.Add(entMessage);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entListMessage;
        }

        public List<Message> GetMessageListLearner(Message pEntMessage)
        {
            _sqlObject = new SQLObject();
            List<Message> entListMessage = new List<Message>();
            Message entMessage = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Messages.PROC_GET_ALL_MESSAGE_LEARNER, sqlConnection);

                if (!string.IsNullOrEmpty(pEntMessage.RecipientID))
                    _sqlpara = new SqlParameter(Schema.Messages.PARA_RECIPIENT_ID, pEntMessage.RecipientID);
                else
                    _sqlpara = new SqlParameter(Schema.Messages.PARA_RECIPIENT_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                //if (!string.IsNullOrEmpty(pEntMessage.LanguageId))
                //    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntMessage.LanguageId);
                //else
                //    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, null);
                //_sqlcmd.Parameters.Add(_sqlpara);


                if (pEntMessage.ListRange != null)
                {
                    if (pEntMessage.ListRange.PageIndex > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntMessage.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntMessage.ListRange.PageSize > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntMessage.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntMessage.ListRange.SortExpression))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntMessage.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entMessage = FillObject(_sqlreader, true, _sqlObject);
                    entListMessage.Add(entMessage);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entListMessage;
        }

        public object Get_Count(Message pEntMessage)
        {
            return GetMessageCount(pEntMessage);
        }

        public object GetMessageCount(Message pEntMessage)
        {
            object obj = null;
            _sqlObject = new SQLObject();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Messages.PROC_GET_MESSAGE_COUNT, sqlConnection);

                if (!String.IsNullOrEmpty(pEntMessage.RecipientID))
                {
                    _sqlpara = new SqlParameter(Schema.Messages.PARA_RECIPIENT_ID, pEntMessage.RecipientID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }

            return obj;
        }

        public object Get_UnreadCustom_Count(Message pEntMessage)
        {
            return GetCustomMessageCount(pEntMessage);
        }

        public object GetCustomMessageCount(Message pEntMessage)
        {
            object obj = null;
            _sqlObject = new SQLObject();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Messages.PROC_GET_CUSTOM_MESSAGE_COUNT, sqlConnection);

                if (!String.IsNullOrEmpty(pEntMessage.RecipientID))
                {
                    _sqlpara = new SqlParameter(Schema.Messages.PARA_RECIPIENT_ID, pEntMessage.RecipientID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }

            return obj;
        }

        public object Get_UnreadSystem_Count(Message pEntMessage)
        {
            return GetSystemMessageCount(pEntMessage);
        }

        public object GetSystemMessageCount(Message pEntMessage)
        {
            object obj = null;
            _sqlObject = new SQLObject();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Messages.PROC_GET_SYSTEM_MESSAGE_COUNT, sqlConnection);

                if (!String.IsNullOrEmpty(pEntMessage.RecipientID))
                {
                    _sqlpara = new SqlParameter(Schema.Messages.PARA_RECIPIENT_ID, pEntMessage.RecipientID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }

            return obj;
        }

        public object Get_TotalCount(Message pEntMessage)
        {
            return GetTotalMessageCount(pEntMessage);
        }

        public object GetTotalMessageCount(Message pEntMessage)
        {
            object obj = null;
            _sqlObject = new SQLObject();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Messages.PROC_GET_TOTAL_MESSAGE_COUNT, sqlConnection);

                if (!String.IsNullOrEmpty(pEntMessage.RecipientID))
                {
                    _sqlpara = new SqlParameter(Schema.Messages.PARA_RECIPIENT_ID, pEntMessage.RecipientID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }

            return obj;
        }

        private Message FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            Message entMessage = new Message();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Messages.COL_MESSAGE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Messages.COL_MESSAGE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entMessage.ID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Messages.COL_RECIPIENT_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Messages.COL_RECIPIENT_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entMessage.RecipientID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Messages.COL_BUSINESS_RULE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Messages.COL_BUSINESS_RULE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entMessage.BusinessRuleID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Messages.COL_MESSAGE_TITLE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Messages.COL_MESSAGE_TITLE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entMessage.MessageTitle = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Messages.COL_MESSAGE_BODY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Messages.COL_MESSAGE_BODY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entMessage.MessageBody = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Messages.COL_ISREAD))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Messages.COL_ISREAD);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entMessage.IsRead = pSqlReader.GetBoolean(iIndex);

                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Messages.COL_ISDELETED))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Messages.COL_ISDELETED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entMessage.IsDeleted = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Messages.COL_READDATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Messages.COL_READDATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entMessage.ReadDate = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Messages.COL_ISCUSTOM_MESSAGE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Messages.COL_ISCUSTOM_MESSAGE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entMessage.IsCustomMessage = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Messages.COL_USERNAMEALIAS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Messages.COL_USERNAMEALIAS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entMessage.UsernameAlias = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Messages.COL_RECIPIENTS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Messages.COL_RECIPIENTS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entMessage.Recipients = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entMessage.CreatedById = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entMessage.DateCreated = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Messages.COL_CUSTOM_COUNT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Messages.COL_CUSTOM_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entMessage.CustomCount = pSqlReader.GetInt32(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Messages.COL_SYSTEM_COUNT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Messages.COL_SYSTEM_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entMessage.SystemCount = pSqlReader.GetInt32(iIndex);
                }

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                        if (!pSqlReader.IsDBNull(iIndex))
                            _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                        entMessage.ListRange = _entRange;
                    }

                }

            }
            return entMessage;
        }

        /// <summary>
        /// To Edit the Message data 
        /// </summary>
        /// <param name="pEntMessage"></param>
        /// <returns>Message Object</returns>
        public Message EditMessage(Message pEntMessage)
        {
            Message entMessage = new Message();

            try
            {
                //Update information in DataBase
                entMessage = Update(pEntMessage, Schema.Common.VAL_UPDATE_MODE);

            }
            catch (Exception expCommon)
            {
                entMessage = null;
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entMessage;
        }

        public Message AddMessage(Message pEntMessage)
        {
            Message entMessage = new Message();
            try
            {
                entMessage = Update(pEntMessage, Schema.Common.VAL_INSERT_MODE);
            }
            catch (Exception expCommon)
            {
                entMessage = null;
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entMessage;
        }

        private Message Update(Message pEntMessage, string pStrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Messages.PROC_UPDATE_MESSAGE;
            _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
            if (pStrUpdateMode == Schema.Common.VAL_INSERT_MODE)
            {
                pEntMessage.ID = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_Messages_ID_PREFIX, Schema.Common.VAL_Messages_ID_LENGTH);
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
            }
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);

            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Messages.PARA_MESSAGE_ID, pEntMessage.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Messages.PARA_RECIPIENT_ID, pEntMessage.RecipientID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Messages.PARA_BUSINESS_RULE_ID, pEntMessage.BusinessRuleID);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!string.IsNullOrEmpty(pEntMessage.MessageTitle))
            {
                _sqlpara = new SqlParameter(Schema.Messages.PARA_MESSAGE_TITLE, pEntMessage.MessageTitle);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            if (!string.IsNullOrEmpty(pEntMessage.MessageBody))
            {
                _sqlpara = new SqlParameter(Schema.Messages.PARA_MESSAGE_BODY, pEntMessage.MessageBody);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntMessage.CreatedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            int iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            string[] strLearners = pEntMessage.RecipientID.Split(',');
            for (int i = 0; i < strLearners.Length; i++)
            {
                Message entMessage = new Message();
                entMessage = pEntMessage;
                entMessage.RecipientID = Convert.ToString(strLearners[i]);
                pEntMessage = UpdateDetails(entMessage);
            }
            return pEntMessage;
        }

        public Message UpdateDetails(Message pEntMessage)
        {
            return UpdateMessageDetails(pEntMessage, Message.Method.UpdateDetails);
        }

        private Message UpdateMessageDetails(Message pEntMessage, Message.Method pMethod)
        {
            int iRowsAffected = 0;
            SqlConnection sqlConn = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Messages.PROC_UPDATE_MESSAGE_DETAILS;
            _sqlObject = new SQLObject();

            //_sqlpara = new SqlParameter(Schema.Messages.PARA_MESSAGE_ID, pEntMessage.ID);
            //_sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Messages.PARA_RECIPIENT_ID, pEntMessage.RecipientID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Messages.PARA_ISREAD, pEntMessage.IsRead);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Messages.PARA_ISDELETED, pEntMessage.IsDeleted);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntMessage.IsCustomMessage != null)
            {
                _sqlpara = new SqlParameter(Schema.Messages.PARA_ISCUSTOM_MESSAGE, pEntMessage.IsCustomMessage);
                _sqlcmd.Parameters.Add(_sqlpara);
            }


            if (pEntMessage.ReadDate != DateTime.MinValue)
            {
                _sqlpara = new SqlParameter(Schema.Messages.PARA_READDATE, pEntMessage.ReadDate);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            try
            {
                if (pMethod == Message.Method.Update)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else if (pMethod == Message.Method.Add)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    pEntMessage.ID = YPLMS.Services.IDGenerator.GetStringGUIDWithPrefix(Schema.Common.VAL_Messages_ID_PREFIX);
                }
                else if (pMethod == Message.Method.UpdateDetails)
                {

                }
                _sqlpara = new SqlParameter(Schema.Messages.PARA_MESSAGE_ID, pEntMessage.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
                sqlConn = new SqlConnection(_strConnString);
                sqlConn.Open();
                _sqlcmd.Connection = sqlConn;
                iRowsAffected = _sqlObject.ExecuteNonQueryCount(_sqlcmd);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlConn.State != ConnectionState.Closed)
                    sqlConn.Close();
            }
            return pEntMessage;
        }

        public Message DeleteMessage(Message pEntMessage)
        {
            _sqlObject = new SQLObject();
            Message entBookmark = new Message();

            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Messages.PROC_DELETE_MESSAGE;

            _sqlpara = new SqlParameter(Schema.Messages.PARA_MESSAGE_ID, pEntMessage.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Messages.PARA_RECIPIENT_ID, pEntMessage.RecipientID);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
                //Delete Document from database
                int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }

            return pEntMessage;
        }



        public Message Get(Message pEntBase)
        {
            throw new NotImplementedException();
        }



        public Message Update(Message pEntBase)
        {
            return EditMessage(pEntBase);
            //throw new NotImplementedException();
        }
    }
}
