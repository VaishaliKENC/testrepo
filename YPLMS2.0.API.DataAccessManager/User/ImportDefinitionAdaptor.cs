using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;
using System.Diagnostics.Eventing.Reader;

namespace YPLMS2._0.API.DataAccessManager
{
    public class ImportDefinitionAdaptor : IDataManager<ImportDefination>,IImportDefinitionAdaptor<ImportDefination>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        ImportDefination _entImportDefinition = null;
        EntityRange entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.ImportDefinition.IDF_ERROR;
        #endregion

        /// <summary>
        /// Get Import Defination By Id
        /// </summary>
        /// <param name="pEntImportDefination"></param>
        /// <returns></returns>
        public ImportDefination GetImportDefinationById(ImportDefination pEntImportDefination)
        {
            _sqlObject = new SQLObject();
            _entImportDefinition = new ImportDefination();
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntImportDefination.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ImportDefinition.PROC_GET_IMPORT_DEF, sqlConnection);
                _sqlpara = new SqlParameter(Schema.ImportDefinition.PARA_IMPORT_DEF_ID, pEntImportDefination.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entImportDefinition = FillObject(_sqlreader);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return _entImportDefinition;
        }

        /// <summary>
        /// To edit 
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <returns>updated admin role object </returns>
        public ImportDefination EditImportDefination(ImportDefination pEntImportDefinition)
        {
            _entImportDefinition = new ImportDefination();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntImportDefinition.ClientId);
                _entImportDefinition = Update(pEntImportDefinition, Schema.Common.VAL_UPDATE_MODE);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            return _entImportDefinition;
        }

        /// <summary>
        /// Get Import Defination List
        /// </summary>
        /// <param name="pEntImportDefination"></param>
        /// <returns></returns>
        public List<ImportDefination> GetImportDefinationList(ImportDefination pEntImportDefination)
        {
            _sqlObject = new SQLObject();
            _entImportDefinition = new ImportDefination();
            List<ImportDefination> entListImportDefination = new List<ImportDefination>();
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            try
            {
                _sqlcmd = new SqlCommand();
                if (!String.IsNullOrEmpty(pEntImportDefination.ClientId))
                {
                    switch (pEntImportDefination.ImportAction)
                    {
                        case ImportAction.Activate:
                            _sqlcmd.CommandText = Schema.ImportDefinition.PROC_GET_ACTIVATE_IMPORT_DEF;
                            break;
                        case ImportAction.ChangeId:
                            _sqlcmd.CommandText = Schema.ImportDefinition.PROC_GET_CHANGED_ID_IMPORT_DEF;
                            break;
                        case ImportAction.Deactivate:
                            _sqlcmd.CommandText = Schema.ImportDefinition.PROC_GET_DEACTIVATE_IMPORT_DEF;
                            break;
                        case ImportAction.Report: //Added for Reporting Tool

                            _sqlcmd.CommandText = Schema.ImportDefinition.PROC_GET_REPORT_IMPORT_DEF;
                            break;
                        case ImportAction.None:
                            _sqlcmd.CommandText = Schema.ImportDefinition.PROC_GET_CURRENT_IMPORT_DEF;
                            
                            break;
                        case ImportAction.PasswordReset:
                            _sqlcmd.CommandText = Schema.ImportDefinition.PROC_GET_PASSWORD_RESET_IMPORT_DEF;
                            break;
                        default:
                            _sqlcmd.CommandText = Schema.ImportDefinition.PROC_GET_ALL_IMPORT_DEF;
                            break;
                    }
                    _strConnString = _sqlObject.GetClientDBConnString(pEntImportDefination.ClientId);
                    sqlConnection = new SqlConnection(_strConnString);
                    _sqlcmd.Connection = sqlConnection;
                    _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntImportDefination.ClientId);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (_sqlcmd.CommandText == Schema.ImportDefinition.PROC_GET_REPORT_IMPORT_DEF)
                    {
                        if (pEntImportDefination.ReportId != null)
                            _sqlpara = new SqlParameter(Schema.Common.PARA_REPORT_ID, pEntImportDefination.ReportId);
                        else
                            _sqlpara = new SqlParameter(Schema.Common.PARA_REPORT_ID, null);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (_sqlcmd.CommandText == Schema.ImportDefinition.PROC_GET_CURRENT_IMPORT_DEF)
                    {
                        if (!String.IsNullOrEmpty(pEntImportDefination.KeyWord))
                            _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntImportDefination.KeyWord);
                        else
                            _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, null);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (pEntImportDefination.ListRange != null)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntImportDefination.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntImportDefination.ListRange != null)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntImportDefination.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntImportDefination.ListRange != null)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntImportDefination.ListRange.SortExpression);
                   
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                     _sqlcmd.Parameters.Add(_sqlpara);

                    

                    _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                    while (_sqlreader.Read())
                    {
                        _entImportDefinition = FillObject(_sqlreader);
                        entListImportDefination.Add(_entImportDefinition);
                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListImportDefination;
        }

        /// <summary>
        /// To fill 
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <returns>Role object</returns>
        private ImportDefination FillObject(SqlDataReader pSqlreader)
        {
            _entImportDefinition = new ImportDefination();
            int iIndex;

            if (pSqlreader.HasRows)
            {
                iIndex = pSqlreader.GetOrdinal(Schema.ImportDefinition.COL_IMPORT_DEF_ID);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entImportDefinition.ID = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.ImportDefinition.COL_FIELD_NAME);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entImportDefinition.FieldName = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.ImportDefinition.COL_MIN_LENGTH);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entImportDefinition.MinLength = Convert.ToInt16(pSqlreader.GetInt32(iIndex));

                iIndex = pSqlreader.GetOrdinal(Schema.ImportDefinition.COL_MAX_LENGTH);
                _entImportDefinition.MaxLength = Convert.ToInt16(pSqlreader.GetInt32(iIndex));

                iIndex = pSqlreader.GetOrdinal(Schema.ImportDefinition.COL_FIELD_DATA_TYPE);
                if (!pSqlreader.IsDBNull(iIndex))
                {
                    _entImportDefinition.FieldValueType = (ImportDefination.ValueType)Enum.Parse(typeof(ImportDefination.ValueType), pSqlreader.GetString(iIndex));
                    _entImportDefinition.FieldDataType =  pSqlreader.GetString(iIndex);
                }
                iIndex = pSqlreader.GetOrdinal(Schema.ImportDefinition.COL_FIELD_TYPE);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entImportDefinition.FieldTypes = (ImportDefination.FieldType)Enum.Parse(typeof(ImportDefination.FieldType), pSqlreader.GetString(iIndex));

                iIndex = pSqlreader.GetOrdinal(Schema.ImportDefinition.COL_ALLOW_BLANKS);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entImportDefinition.AllowBlank = pSqlreader.GetBoolean(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.ImportDefinition.COL_ERROR_LEVEL);
                if (!pSqlreader.IsDBNull(iIndex))
                {
                    _entImportDefinition.FieldErrorLevel = (ImportDefination.ErrorLevel)Enum.Parse(typeof(ImportDefination.ErrorLevel), pSqlreader.GetString(iIndex));
                    _entImportDefinition.ErrorLevelType = pSqlreader.GetString(iIndex);
                }
                iIndex = pSqlreader.GetOrdinal(Schema.ImportDefinition.COL_INCLUDE);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entImportDefinition.Include = pSqlreader.GetBoolean(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.ImportDefinition.COL_MAX_LENGHT_IN_DB);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entImportDefinition.MaxLengthInDB = pSqlreader.GetInt32(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entImportDefinition.ClientId = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.ImportDefinition.COL_IS_MANDATORY);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entImportDefinition.IsMandatory = pSqlreader.GetBoolean(iIndex);

                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.ImportDefinition.COL_IS_DEFAULT))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ImportDefinition.COL_IS_DEFAULT);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entImportDefinition.IsDefault = pSqlreader.GetBoolean(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.ImportDefinition.COL_DEFAULT_VALUE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ImportDefinition.COL_DEFAULT_VALUE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entImportDefinition.DefaultValue = pSqlreader.GetString(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Common.COL_TOTAL_COUNT))
                {
                    if (_entImportDefinition.ListRange == null)
                    {
                        entRange = new EntityRange();
                        iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                        if (pSqlreader.GetInt32(iIndex) > 0)
                        {
                            entRange.TotalRows = pSqlreader.GetInt32(iIndex);
                            _entImportDefinition.ListRange = entRange;
                        }
                    }

                }

            }
            return _entImportDefinition;
        }

        /// <summary>
        /// To update/add data
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <param name="pStrUpdateMode"></param>
        /// <returns>AdminRole object</returns>
        private ImportDefination Update(ImportDefination pEntImportDefination, string pMode)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected ;
            _sqlcmd.CommandText = Schema.ImportDefinition.PROC_UPDATE_IMPORT_DEF;
            _strConnString = _sqlObject.GetClientDBConnString(pEntImportDefination.ClientId);

            _sqlpara = new SqlParameter(Schema.ImportDefinition.PARA_IMPORT_DEF_ID, pEntImportDefination.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntImportDefination.FieldName))
                _sqlpara = new SqlParameter(Schema.ImportDefinition.PARA_FIELD_NAME, pEntImportDefination.FieldName);
            else
                _sqlpara = new SqlParameter(Schema.ImportDefinition.PARA_FIELD_NAME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ImportDefinition.PARA_MIN_LENGTH, pEntImportDefination.MinLength);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ImportDefinition.PARA_MAX_LENGTH, pEntImportDefination.MaxLength);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ImportDefinition.PARA_FIELD_DATA_TYPE, pEntImportDefination.FieldValueType.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ImportDefinition.PARA_ALLOW_BLANKS, pEntImportDefination.AllowBlank);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ImportDefinition.PARA_ERROR_LEVEL, pEntImportDefination.ErrorLevelType.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ImportDefinition.PARA_INCLUDE, pEntImportDefination.Include);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ImportDefinition.PARA_IS_DEFAULT, pEntImportDefination.IsDefault);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ImportDefinition.PARA_DEFAULT_VALUE, pEntImportDefination.DefaultValue);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntImportDefination.ClientId);
            _sqlcmd.Parameters.Add(_sqlpara);

            iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            return pEntImportDefination;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntImportDefination"></param>
        /// <returns></returns>
        public ImportDefination Get(ImportDefination pEntImportDefination)
        {
            return GetImportDefinationById(pEntImportDefination);
        }
        /// <summary>
        /// Update ImportDefination
        /// </summary>
        /// <param name="pEntImportDefination"></param>
        /// <returns></returns>
        public ImportDefination Update(ImportDefination pEntImportDefination)
        {
            return EditImportDefination(pEntImportDefination);
        }
        #endregion
    }
}
