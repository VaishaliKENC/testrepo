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
    public class SystemMessageAdaptor : IDataManager<SystemMessage>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        SqlDataAdapter _sqladapter = null;
        SqlConnection _sqlcon = null;
        EntityRange _entRange = null;
        DataTable _dtable = null;
        SystemMessage entMessage = null;
        List<SystemMessage> entListMessage = null;

        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.SysMessage.SYS_MSG_ERROR;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// Edit message
        /// </summary>
        /// <param name="pEntMessage"></param>
        /// <returns></returns>
        public SystemMessage EditMessage(SystemMessage pEntMessage)
        {
            entMessage = new SystemMessage();
            entListMessage = new List<SystemMessage>();
            entListMessage.Add(pEntMessage);
            entListMessage = BulkUpdate(entListMessage);
            return entListMessage[0];
        }

        /// <summary>
        /// Update Messages 
        /// </summary>
        /// <param name="pEntListSystemMessage"></param>
        /// <returns></returns>
        public List<SystemMessage> BulkUpdate(List<SystemMessage> pEntListSystemMessage)
        {
            _sqlObject = new SQLObject();
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            List<SystemMessage> entListSystemMessage = new List<SystemMessage>();
            int iBatchSize = 0;
            try
            {
                if (pEntListSystemMessage.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.SystemMessage.COL_MESSAGE_ID);
                    _dtable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    _dtable.Columns.Add(Schema.SystemMessage.COL_MESSAGE_TEXT);
                    _dtable.Columns.Add(Schema.SystemMessage.COL_MESSAGE_DESC);
                    _dtable.Columns.Add(Schema.SystemMessage.COL_MESSAGE_TYPE);
                    _dtable.Columns.Add(Schema.SystemMessage.COL_MESSAGE_FOR);
                    _dtable.Columns.Add(Schema.Common.COL_CREATED_BY);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (SystemMessage objBase in pEntListSystemMessage)
                    {
                        entMessage = new SystemMessage();
                        entMessage = objBase;
                        DataRow drow = _dtable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entMessage.ClientId);
                        if (!String.IsNullOrEmpty(entMessage.ID))
                        {
                            drow[Schema.SystemMessage.COL_MESSAGE_ID] = entMessage.ID;
                            drow[Schema.SystemMessage.COL_MESSAGE_TEXT] = entMessage.MessageText;
                            drow[Schema.Language.COL_LANGUAGE_ID] = Convert.ToString(entMessage.LanguageId);
                            drow[Schema.SystemMessage.COL_MESSAGE_TYPE] = entMessage.MessageType;
                            drow[Schema.SystemMessage.COL_MESSAGE_FOR] = entMessage.MessageFor.ToString();
                            drow[Schema.SystemMessage.COL_MESSAGE_DESC] = entMessage.MessageDescription;
                            drow[Schema.Common.COL_CREATED_BY] = entMessage.CreatedById;
                            drow[Schema.Common.COL_MODIFIED_BY] = entMessage.LastModifiedById;
                            _dtable.Rows.Add(drow);
                            iBatchSize = iBatchSize + 1;
                            entListSystemMessage.Add(entMessage);
                        }
                    }
                    if (_dtable.Rows.Count > 0)
                    {
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.SystemMessage.PROC_UPDATE_MESSAGE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.SystemMessage.PARA_MESSAGE_ID, SqlDbType.VarChar, 100, Schema.SystemMessage.COL_MESSAGE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.NVarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.SystemMessage.PARA_MESSAGE_TEXT, SqlDbType.NVarChar, 2000, Schema.SystemMessage.COL_MESSAGE_TEXT);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.SystemMessage.PARA_MESSAGE_DESC, SqlDbType.NVarChar, 2000, Schema.SystemMessage.COL_MESSAGE_DESC);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.SystemMessage.PARA_MESSAGE_TYPE, SqlDbType.NVarChar, 100, Schema.SystemMessage.COL_MESSAGE_TYPE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.SystemMessage.PARA_MESSAGE_FOR, SqlDbType.VarChar, 100, Schema.SystemMessage.COL_MESSAGE_FOR);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_CREATED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_CREATED_BY);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.Parameters.AddWithValue(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();
                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListSystemMessage;
        }

        /// <summary>
        /// Get Message list for Client for language
        /// </summary>
        /// <param name="pEntMessage"></param>
        /// <returns></returns>
        public List<SystemMessage> GetMessageList(SystemMessage pEntMessage)
        {
            SqlConnection sqlConnection = null;
            entListMessage = new List<SystemMessage>();
            entMessage = new SystemMessage();
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.SystemMessage.PROC_GET_SYS_MESSAGE;
            if (!String.IsNullOrEmpty(pEntMessage.ID))
            {
                _sqlcmd.CommandText = Schema.SystemMessage.PROC_GET_SYS_MESSAGE_SINGLE;
                _sqlpara = new SqlParameter(Schema.SystemMessage.PARA_MESSAGE_ID, pEntMessage.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            if (!string.IsNullOrEmpty(pEntMessage.LanguageId))
            {
                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntMessage.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            if (!string.IsNullOrEmpty(pEntMessage.MessageText))
            {
                _sqlpara = new SqlParameter(Schema.SystemMessage.PARA_MESSAGE_TEXT, pEntMessage.MessageText);
                _sqlcmd.Parameters.Add(_sqlpara);
            }


            try
            {
                if (pEntMessage.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntMessage.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntMessage.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntMessage.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntMessage.ClientId))
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
                }
                else
                {
                    _strConnString = _sqlObject.GetMasterDBConnString();
                }
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entMessage = FillObject(_sqlreader);
                    entMessage.ClientId = pEntMessage.ClientId;
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListMessage;
        }

        /// <summary>
        /// Fills Message Object
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <returns></returns>
        private SystemMessage FillObject(SqlDataReader pSqlreader)
        {
            entMessage = new SystemMessage();
            int index;

            if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.SystemMessage.COL_MESSAGE_ID))
            {
                index = pSqlreader.GetOrdinal(Schema.SystemMessage.COL_MESSAGE_ID);
                if (!pSqlreader.IsDBNull(index))
                    entMessage.ID = pSqlreader.GetString(index);
            }

            if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Language.COL_LANGUAGE_ID))
            {
                index = pSqlreader.GetOrdinal(Schema.Language.COL_LANGUAGE_ID);
                if (!pSqlreader.IsDBNull(index))
                    entMessage.LanguageId = pSqlreader.GetString(index);
            }

            if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.SystemMessage.COL_MESSAGE_TEXT))
            {

                index = pSqlreader.GetOrdinal(Schema.SystemMessage.COL_MESSAGE_TEXT);
                if (!pSqlreader.IsDBNull(index))
                    entMessage.MessageText = pSqlreader.GetString(index);
            }

            if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.SystemMessage.COL_MESSAGE_TYPE))
            {
                index = pSqlreader.GetOrdinal(Schema.SystemMessage.COL_MESSAGE_TYPE);
                if (!pSqlreader.IsDBNull(index))
                    entMessage.MessageType = pSqlreader.GetString(index);
            }

            if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.SystemMessage.COL_MESSAGE_DESC))
            {

                index = pSqlreader.GetOrdinal(Schema.SystemMessage.COL_MESSAGE_DESC);
                if (!pSqlreader.IsDBNull(index))
                    entMessage.MessageDescription = pSqlreader.GetString(index);
            }

            if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.SystemMessage.COL_MESSAGE_FOR))
            {
                index = pSqlreader.GetOrdinal(Schema.SystemMessage.COL_MESSAGE_FOR);
                if (!pSqlreader.IsDBNull(index))
                {
                    if (Enum.IsDefined(typeof(MessageFor), pSqlreader.GetString(index)))
                        entMessage.MessageFor = (MessageFor)Enum.Parse(typeof(MessageFor), pSqlreader.GetString(index));

                }
            }


            if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Common.COL_CREATED_BY))
            {
                index = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pSqlreader.IsDBNull(index))
                    entMessage.CreatedById = pSqlreader.GetString(index);
            }


            if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Common.COL_CREATED_ON))
            {
                index = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                if (!pSqlreader.IsDBNull(index))
                    entMessage.DateCreated = pSqlreader.GetDateTime(index);
            }


            if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Common.COL_MODIFIED_BY))
            {
                index = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                if (!pSqlreader.IsDBNull(index))
                    entMessage.LastModifiedById = pSqlreader.GetString(index);
            }

            if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Common.COL_MODIFIED_ON))
            {
                index = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                if (!pSqlreader.IsDBNull(index))
                    entMessage.LastModifiedDate = pSqlreader.GetDateTime(index);
            }


            if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Common.COL_TOTAL_COUNT))
            {
                index = pSqlreader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                if (!pSqlreader.IsDBNull(index))
                {
                    if (pSqlreader.GetInt32(index) > 0)
                    {
                        _entRange = new EntityRange();
                        _entRange.TotalRows = pSqlreader.GetInt32(index);
                        entMessage.ListRange = _entRange;

                    }
                }
            }

            return entMessage;
        }

        /// <summary>
        /// Get Message by Id
        /// </summary>
        /// <param name="pEntMessage"></param>
        /// <returns></returns>    
        public SystemMessage GetMessageByID(SystemMessage pEntMessage)
        {
            entMessage = new SystemMessage();
            entListMessage = new List<SystemMessage>();
            try
            {
                entListMessage = GetMessageList(pEntMessage);
                if (entListMessage != null && entListMessage.Count > 0)
                    entMessage = entListMessage[0];
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entMessage;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntSystemMessage"></param>
        /// <returns></returns>
        public SystemMessage Get(SystemMessage pEntSystemMessage)
        {
            return GetMessageByID(pEntSystemMessage);
        }
        /// <summary>
        /// Update SystemMessage
        /// </summary>
        /// <param name="pEntSystemMessage"></param>
        /// <returns></returns>
        public SystemMessage Update(SystemMessage pEntSystemMessage)
        {
            return EditMessage(pEntSystemMessage);
        }
        #endregion
    }
}
