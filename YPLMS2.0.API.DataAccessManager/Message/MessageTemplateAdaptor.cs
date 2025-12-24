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
    public class MessageTemplateAdaptor : IDataManager<MessageTemplate>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        //string _strMessageId = Services.Document.DOCUMENT_BL_ERROR;
        //string strDocumentAssignMsgId = Services.MessageTemplate.DocumentLibrary.DOCUMENT_LIBRARY_ASSIGN;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// To Get Asset details by Asset Id.
        /// </summary>
        /// <param name="pentTemplate"></param>
        /// <returns>Asset Object</returns>
        public MessageTemplate GetTemplateById(MessageTemplate pEntTemplate)
        {
            _sqlObject = new SQLObject();
            MessageTemplate entTemplate = new MessageTemplate();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntTemplate.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                _sqlcmd = new SqlCommand(Schema.MessageTemplate.PROC_GET_MESSAGE_TEMPLATE, sqlConnection);
                _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_TEMPLATE_ID, pEntTemplate.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntTemplate.LanguageID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entTemplate = FillObject(_sqlreader, false, _sqlObject);
            }
            catch (Exception expCommon)
            {
                // _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                // throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed)
                    _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entTemplate;
        }

        public List<MessageTemplate> GetMessageList(MessageTemplate pEntMessage)
        {
            _sqlObject = new SQLObject();
            List<MessageTemplate> entListMessage = new List<MessageTemplate>();
            MessageTemplate entMessage = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.MessageTemplate.PROC_GET_ALL_MESSAGE_TEMPLATE, sqlConnection);

                //if (!string.IsNullOrEmpty(pEntMessage.ID))
                //    _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_TEMPLATE_ID, pEntMessage.ID);
                //else
                //    _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_TEMPLATE_ID, null);
                //_sqlcmd.Parameters.Add(_sqlpara);

                //if (!string.IsNullOrEmpty(pEntMessage.LanguageID))
                //    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntMessage.LanguageID);
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
                //_expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                //throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed)
                    _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entListMessage;
        }

        public List<MessageTemplate> GetMessageTemplateLanguageList(MessageTemplate pEntTemplate)
        {
            _sqlObject = new SQLObject();
            List<MessageTemplate> entListMessageTemplate = new List<MessageTemplate>();
            MessageTemplate entTemplate = null;
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.MessageTemplate.PROC_GET_ALL_MESSAGETEMPLATE_LANGUAGE;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntTemplate.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_TEMPLATE_ID, pEntTemplate.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                if (pEntTemplate.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntTemplate.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntTemplate.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntTemplate.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entTemplate = FillObject(_sqlreader, true, _sqlObject);
                    entListMessageTemplate.Add(entTemplate);
                }
            }
            catch (Exception expCommon)
            {
                //_expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                //throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed)
                    _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entListMessageTemplate;
        }

        private MessageTemplate FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            MessageTemplate entTemplate = new MessageTemplate();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.MessageTemplate.COL_TEMPLATE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.MessageTemplate.COL_TEMPLATE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.ID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.MessageTemplate.COL_TEMPLATE_TITLE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.MessageTemplate.COL_TEMPLATE_TITLE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.TemplateTitle = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Language.COL_LANGUAGE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_LANGUAGE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.LanguageID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Language.COL_LANG_ENG_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_LANG_ENG_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.LanguageName = pSqlReader.GetString(iIndex);
                    else
                        entTemplate.LanguageName = "English";
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.MessageTemplate.COL_MESSAGE_TITLE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.MessageTemplate.COL_MESSAGE_TITLE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.MessageTitle = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.MessageTemplate.COL_MESSAGE_BODY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.MessageTemplate.COL_MESSAGE_BODY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.MessageBody = pSqlReader.GetString(iIndex);
                    else
                        entTemplate.MessageBody = " ";
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.MessageTemplate.COL_IS_ACTIVE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.MessageTemplate.COL_IS_ACTIVE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.IsActive = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.MessageTemplate.COL_IS_DEFAULT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.MessageTemplate.COL_IS_DEFAULT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.IsDefault = Convert.ToBoolean(pSqlReader[Schema.MessageTemplate.COL_IS_DEFAULT]);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.CreatedById = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.DateCreated = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.LastModifiedById = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.LastModifiedDate = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.CreatedByName = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.LastModifiedByName = pSqlReader.GetString(iIndex);
                }

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                        if (!pSqlReader.IsDBNull(iIndex))
                            _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                        entTemplate.ListRange = _entRange;
                    }

                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.CreatedByName = pSqlReader.GetString(iIndex);
                }

            }
            return entTemplate;
        }

        /// <summary>
        /// To Edit the Message data 
        /// </summary>
        /// <param name="pEntMessage"></param>
        /// <returns>Message Object</returns>
        public MessageTemplate EditMessageTemplate(MessageTemplate pEntMessage)
        {
            MessageTemplate entMessage = new MessageTemplate();

            try
            {
                //Update information in DataBase
                entMessage = Update(pEntMessage, Schema.Common.VAL_UPDATE_MODE);

            }
            catch (Exception expCommon)
            {
                entMessage = null;
                //_expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                //throw _expCustom;
            }
            return entMessage;
        }

        public MessageTemplate AddMessageTemplate(MessageTemplate pEntMessage)
        {
            MessageTemplate entMessage = new MessageTemplate();
            try
            {
                entMessage = Update(pEntMessage, Schema.Common.VAL_INSERT_MODE);
            }
            catch (Exception expCommon)
            {
                entMessage = null;
                //_expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                //throw _expCustom;
            }
            return entMessage;
        }

        private MessageTemplate Update(MessageTemplate pEntMessage, string pStrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.MessageTemplate.PROC_UPDATE_MESSAGETEMPLATE;
            _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
            if (pStrUpdateMode == Schema.Common.VAL_INSERT_MODE)
            {
                pEntMessage.ID = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_MessagesTemplate_ID_PREFIX, Schema.Common.VAL_MessagesTemplate_ID_LENGTH);
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
            }
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);

            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_TEMPLATE_ID, pEntMessage.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntMessage.TemplateTitle != null)
                _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_TEMPLATE_TITLE, pEntMessage.TemplateTitle);
            else
                _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_TEMPLATE_TITLE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntMessage.IsActive != null)
                _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_IS_ACTIVE, pEntMessage.IsActive);
            else
                _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_IS_ACTIVE, DBNull.Value);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntMessage.IsDefault != null)
                _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_IS_DEFAULT, pEntMessage.IsDefault);
            else
                _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_IS_DEFAULT, DBNull.Value);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntMessage.CreatedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntMessage.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            int iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            pEntMessage = UpdateLanguage(pEntMessage);

            return pEntMessage;
        }

        public MessageTemplate UpdateLanguage(MessageTemplate pEntMessage)
        {
            return UpdateMessageTemplateLanguage(pEntMessage, MessageTemplate.Method.UpdateLanguage);
        }

        private MessageTemplate UpdateMessageTemplateLanguage(MessageTemplate pEntMessage, MessageTemplate.Method pMethod)
        {
            int iRowsAffected = 0;
            SqlConnection sqlConn = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.MessageTemplate.PROC_UPDATE_MESSAGE_LANGUAGE;
            _sqlObject = new SQLObject();


            if (!String.IsNullOrEmpty(pEntMessage.LanguageID))
                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntMessage.LanguageID);
            else
                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntMessage.MessageTitle))
                _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_MESSAGE_TITLE, pEntMessage.MessageTitle);
            else
                _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_MESSAGE_TITLE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntMessage.MessageBody))
                _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_MESSAGE_BODY, pEntMessage.MessageBody);
            else
                _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_MESSAGE_BODY, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                if (pMethod == MessageTemplate.Method.Update)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else if (pMethod == MessageTemplate.Method.Add)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    pEntMessage.ID = YPLMS.Services.IDGenerator.GetStringGUIDWithPrefix(Schema.Common.VAL_ASSET_ID_PREFIX);
                }
                else if (pMethod == MessageTemplate.Method.UpdateLanguage)
                {

                }
                _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_TEMPLATE_ID, pEntMessage.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
                sqlConn = new SqlConnection(_strConnString);
                sqlConn.Open();
                _sqlcmd.Connection = sqlConn;
                iRowsAffected = _sqlObject.ExecuteNonQueryCount(_sqlcmd);
            }
            catch (Exception expCommon)
            {
                //_expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                //throw _expCustom;
            }
            finally
            {
                if (sqlConn.State != ConnectionState.Closed)
                    sqlConn.Close();
            }
            return pEntMessage;
        }

        public MessageTemplate DeleteMessageTemplate(MessageTemplate pEntMessage)
        {
            _sqlObject = new SQLObject();
            MessageTemplate entMessage = new MessageTemplate();

            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.MessageTemplate.PROC_DELETE_MESSAGETEMPLATE;
            _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_TEMPLATE_ID, pEntMessage.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
                //Delete Asset from database
                int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            }
            catch (Exception expCommon)
            {
                //_expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                //throw _expCustom;
            }

            return pEntMessage;
        }

        public MessageTemplate DeleteMessageTemplateLanguage(MessageTemplate pEntMessage)
        {
            _sqlObject = new SQLObject();
            MessageTemplate entMessage = new MessageTemplate();

            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.MessageTemplate.PROC_DELETE_MESSAGETEMPLATE_LANGUAGES;
            _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_TEMPLATE_ID, pEntMessage.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntMessage.LanguageID);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
                //Delete Asset from database
                int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            }
            catch (Exception expCommon)
            {
                //_expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                //throw _expCustom;
            }
            return pEntMessage;
        }

        public MessageTemplate Get(MessageTemplate pEntBase)
        {
            throw new NotImplementedException();
        }

        public MessageTemplate Update(MessageTemplate pEntBase)
        {
            return EditMessageTemplate(pEntBase);
            //throw new NotImplementedException();
        }
    }
}
