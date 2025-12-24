using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.YPLMS.Services;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public class AutoEmailTemplateSettingAdaptor : IDataManager<AutoEmailTemplateSetting>, IAutoEmailTemplateSettingAdaptor<AutoEmailTemplateSetting>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlDataAdapter _sqladapter = null;
        SqlParameter _sqlpara = null;
        SqlCommand _sqlcmd = null;
        SqlConnection _sqlConnection = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.AutoEmailTemplateSetting.AUTO_EMAIL_TEMPLATE_ERR;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// To Get single record
        /// </summary>
        /// <param name="pEntEmailEvent"></param>
        /// <returns></returns>
        public AutoEmailTemplateSetting GetEmailEventById(AutoEmailTemplateSetting pEntEmailEvent)
        {
            AutoEmailTemplateSetting entEmailEvent = new AutoEmailTemplateSetting();
            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(pEntEmailEvent.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);

            _sqlcmd = new SqlCommand(Schema.AutoEmailTemplateSetting.PROC_GET_AUTO_EMAIL_EVENT, _sqlConnection);
            _sqlcmd.Parameters.AddWithValue(Schema.AutoEmailTemplateSetting.PARA_AUTO_EMAIL_EVENT_ID, pEntEmailEvent.ID);

            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entEmailEvent = FillObject(_sqlreader, true);
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
            return entEmailEvent;
        }

        /// <summary>
        /// To Get single record
        /// </summary>
        /// <param name="pEntEmailEvent"></param>
        /// <returns></returns>
        public AutoEmailTemplateSetting GetEmailTempId(AutoEmailTemplateSetting pEntEmailEvent)
        {
            AutoEmailTemplateSetting entEmailEvent = new AutoEmailTemplateSetting();
            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(pEntEmailEvent.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);

            _sqlcmd = new SqlCommand(Schema.AutoEmailTemplateSetting.PROC_GET_EMAIL_TEMP_ID_AUTO_EMAIL_EVENT, _sqlConnection);
            _sqlcmd.Parameters.AddWithValue(Schema.AutoEmailTemplateSetting.PARA_AUTO_EMAIL_EVENT_ID, pEntEmailEvent.ID);

            try
            {
                Object obj = null;
                obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
                if (obj != null)
                {
                    entEmailEvent.EmailTemplateID = Convert.ToString(obj);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (_sqlConnection != null && _sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.Close();
            }
            return entEmailEvent;
        }

        /// <summary>
        /// To Get all auto email events
        /// </summary>
        /// <param name="pEntEmailEvent"></param>
        /// <returns></returns>
        public List<AutoEmailTemplateSetting> GetEmailTemplateSettingList(AutoEmailTemplateSetting pEntEmailEvent)
        {
            AutoEmailTemplateSetting entEmailEvent = new AutoEmailTemplateSetting();
            List<AutoEmailTemplateSetting> entListEmailEvents = new List<AutoEmailTemplateSetting>();
            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(pEntEmailEvent.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.AutoEmailTemplateSetting.PROC_GET_ALL_AUTO_EMAIL_EVENTS, _sqlConnection);

            if (pEntEmailEvent.ListRange != null)
            {
                if (pEntEmailEvent.ListRange.PageIndex > 0)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntEmailEvent.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntEmailEvent.ListRange.PageSize > 0)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntEmailEvent.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntEmailEvent.ListRange.SortExpression))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntEmailEvent.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
            }
            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entEmailEvent = FillObject(_sqlreader, true);
                    entListEmailEvents.Add(entEmailEvent);
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
            return entListEmailEvents;
        }

        /// <summary>
        /// Fill Object
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <param name="pRangeList"></param>
        /// <returns></returns>
        private AutoEmailTemplateSetting FillObject(SqlDataReader pSqlReader, bool pRangeList)
        {
            AutoEmailTemplateSetting entEmailEvent = new AutoEmailTemplateSetting();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.AutoEmailTemplateSetting.COL_AUTO_EMAIL_EVENT_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailEvent.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.AutoEmailTemplateSetting.COL_EMAIL_TEMPLATE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailEvent.EmailTemplateID = pSqlReader.GetString(iIndex);
                else
                    entEmailEvent.EmailTemplateID = "";

                iIndex = pSqlReader.GetOrdinal(Schema.AutoEmailTemplateSetting.COL_EVENT_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailEvent.EventName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.AutoEmailTemplateSetting.COL_FEATURE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailEvent.FeatureId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.AutoEmailTemplateSetting.COL_IS_RECURRENCE_APPROVAL_REQUIRED);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailEvent.IsRecurrenceApprovalRequired = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailEvent.CreatedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailEvent.DateCreated = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailEvent.LastModifiedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailEvent.LastModifiedDate = pSqlReader.GetDateTime(iIndex);
            }
            return entEmailEvent;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="pEntListEmailEvent"></param>
        /// <returns></returns>
        public List<AutoEmailTemplateSetting> Update(List<AutoEmailTemplateSetting> pEntListEmailEvent)
        {
            List<AutoEmailTemplateSetting> entListEmailEvents = new List<AutoEmailTemplateSetting>();
            _sqladapter = new SqlDataAdapter();
            DataTable dTable = new DataTable();
            _sqlObject = new SQLObject();
            int iBatchSize = 0;
            try
            {
                if (pEntListEmailEvent.Count > 0)
                {
                    dTable.Columns.Add(Schema.AutoEmailTemplateSetting.COL_AUTO_EMAIL_EVENT_ID);
                    dTable.Columns.Add(Schema.AutoEmailTemplateSetting.COL_EMAIL_TEMPLATE_ID);
                    dTable.Columns.Add(Schema.AutoEmailTemplateSetting.COL_EVENT_NAME);
                    dTable.Columns.Add(Schema.AutoEmailTemplateSetting.COL_FEATURE_ID);
                    dTable.Columns.Add(Schema.AutoEmailTemplateSetting.COL_IS_RECURRENCE_APPROVAL_REQUIRED);
                    dTable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    dTable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (AutoEmailTemplateSetting entEmailEvent in pEntListEmailEvent)
                    {

                        if (string.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entEmailEvent.ClientId);

                        DataRow dRow = dTable.NewRow();

                        dRow[Schema.AutoEmailTemplateSetting.COL_AUTO_EMAIL_EVENT_ID] = entEmailEvent.ID;
                        dRow[Schema.AutoEmailTemplateSetting.COL_EMAIL_TEMPLATE_ID] = entEmailEvent.EmailTemplateID;
                        dRow[Schema.AutoEmailTemplateSetting.COL_EVENT_NAME] = entEmailEvent.EventName; ;
                        dRow[Schema.AutoEmailTemplateSetting.COL_FEATURE_ID] = entEmailEvent.FeatureId;
                        dRow[Schema.AutoEmailTemplateSetting.COL_IS_RECURRENCE_APPROVAL_REQUIRED] = entEmailEvent.IsRecurrenceApprovalRequired;
                        dRow[Schema.Common.COL_MODIFIED_BY] = entEmailEvent.LastModifiedById;
                        dRow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_UPDATE_MODE;

                        dTable.Rows.Add(dRow);

                        iBatchSize = iBatchSize + 1;
                        entListEmailEvents.Add(entEmailEvent);
                    }
                    if (dTable.Rows.Count > 0)
                    {
                        _sqladapter = new SqlDataAdapter();
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.AutoEmailTemplateSetting.PROC_UPDATE_AUTO_EMAIL_EVENT;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlConnection = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlConnection;

                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AutoEmailTemplateSetting.PARA_AUTO_EMAIL_EVENT_ID, SqlDbType.VarChar, 100, Schema.AutoEmailTemplateSetting.COL_AUTO_EMAIL_EVENT_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AutoEmailTemplateSetting.PARA_EMAIL_TEMPLATE_ID, SqlDbType.VarChar, 100, Schema.AutoEmailTemplateSetting.COL_EMAIL_TEMPLATE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AutoEmailTemplateSetting.PARA_EVENT_NAME, SqlDbType.NVarChar, 100, Schema.AutoEmailTemplateSetting.COL_EVENT_NAME);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AutoEmailTemplateSetting.PARA_FEATURE_ID, SqlDbType.VarChar, 100, Schema.AutoEmailTemplateSetting.COL_FEATURE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AutoEmailTemplateSetting.PARA_IS_RECURRENCE_APPROVAL_REQUIRED, SqlDbType.Bit, 1, Schema.AutoEmailTemplateSetting.COL_IS_RECURRENCE_APPROVAL_REQUIRED);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        _sqladapter.Update(dTable);
                        _sqladapter.Dispose();
                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListEmailEvents;
        }
        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntAutoEmailTemplateSetting"></param>
        /// <returns></returns>
        public AutoEmailTemplateSetting Get(AutoEmailTemplateSetting pEntAutoEmailTemplateSetting)
        {
            return GetEmailEventById(pEntAutoEmailTemplateSetting);
        }
        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="pEntAutoEmailTemplateSetting"></param>
        /// <returns>null</returns>
        public AutoEmailTemplateSetting Update(AutoEmailTemplateSetting pEntAutoEmailTemplateSetting)
        {
            return null;
        }
        #endregion
    }
}
