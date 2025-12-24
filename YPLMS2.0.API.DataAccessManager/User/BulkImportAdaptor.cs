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
    public class BulkImportAdaptor : IDataManager<BulkImport>, IBulkImportAdaptor<BulkImport>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        YPLMS.Services.CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        BulkImport _entBulkImportMaster = null;
        EntityRange entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.BulkImport.ERROR_MSG_ID;
        #endregion

        /// <summary>
        /// Get Bulk Import Master List
        /// </summary>
        /// <param name="pEntBulkImportMaster"></param>
        /// <returns></returns>
        public List<BulkImport> GetBulkImportMasterList(BulkImport pEntBulkImportMaster)
        {
            List<BulkImport> entListBulkImportMaster = new List<BulkImport>();
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntBulkImportMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.Connection = sqlConnection;
                if (!String.IsNullOrEmpty(pEntBulkImportMaster.ID))
                {
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_IMPORT_ID, pEntBulkImportMaster.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlcmd.CommandText = Schema.BulkImport.PROC_SEL_BULK_IMP_MSTR;
                }
                else
                {
                    _sqlcmd.CommandText = Schema.BulkImport.PROC_LSTALL_BULK_IMP_MSTR;
                    if (pEntBulkImportMaster.ListRange != null)
                    {
                        if (pEntBulkImportMaster.ListRange != null)
                            _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntBulkImportMaster.ListRange.PageIndex);
                        else
                            _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        if (pEntBulkImportMaster.ListRange != null)
                            _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntBulkImportMaster.ListRange.PageSize);
                        else
                            _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        if (pEntBulkImportMaster.ListRange.SortExpression != null)
                            _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntBulkImportMaster.ListRange.SortExpression);
                        else
                            _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entBulkImportMaster = FillObject(_sqlreader);
                    entListBulkImportMaster.Add(_entBulkImportMaster);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new YPLMS.Services.CustomException(_strMessageId, YPLMS.Services.CustomException.WhoCallsMe(), YPLMS.Services.ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListBulkImportMaster;
        }

        /// <summary>
        /// Fill Object
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <returns></returns>
        private BulkImport FillObject(SqlDataReader pSqlreader)
        {
            _entBulkImportMaster = new BulkImport();
            int iIndex;

            if (pSqlreader.HasRows)
            {
                iIndex = pSqlreader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.ClientId = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.CreatedById = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_CREATE_PWD);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.CreatePassword = pSqlreader.GetBoolean(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_KEEP_EXISTING_PWD);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.KeepExistingPassword = pSqlreader.GetBoolean(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_ADD_NEW_CUSTOM_FIELDS);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.AddNewCustomFields = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_ADD_NEW_ORG_LEVEL);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.AddNewOrganizationLevels = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_UPLOAD_FILE_NAME);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.UploadedFileName = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.DateCreated = pSqlreader.GetDateTime(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_EMAIL_TEMPLATE_ID);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.EmailTemplateId = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_EMAIL_TYPE);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.EmailType = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_FIELD_MAPPING);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.FieldMapping = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_IMPORT_ACTION);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.ImportAction = (ImportAction)Enum.Parse(typeof(ImportAction), pSqlreader.GetString(iIndex));

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_IMPORT_STATUS);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.ImportStatus = (ImportStatus)Enum.Parse(typeof(ImportStatus), pSqlreader.GetString(iIndex));

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_IMPORT_ID);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.ID = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_IMPORT_FILE_PATH);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.ImportFilePath = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_IS_ACTIVE);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.IsActive = pSqlreader.GetBoolean(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_IS_ASSIGNED);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.IsAssigned = pSqlreader.GetBoolean(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_IS_OVERRIDE_PREVIOUS_ASSGNMT);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.IsOverridePreviousAssignment = pSqlreader.GetBoolean(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_PWD);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.Password = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_SCH_DATE_AND_TIME);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.ScheduledDateAndTime = pSqlreader.GetDateTime(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_SCHEDULED_EMAIL_TASK_ID);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.ScheduledEmailTaskId = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_SEND_EMAIL);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.SendEmail = pSqlreader.GetBoolean(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_DIRECT_SEND_EMAIL);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.DirectSendEmail = pSqlreader.GetBoolean(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_TITLE);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.Title = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Task.COL_TASK_ID);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.TaskId = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_IMPORT_TYPE);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.ImportType = (ImportType)Enum.Parse(typeof(ImportType), pSqlreader.GetString(iIndex));

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_COMMENT);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.Comment = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.BulkImport.COL_COMPLETION_DATE);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entBulkImportMaster.CompletionDate = pSqlreader.GetDateTime(iIndex);

                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Common.COL_TOTAL_COUNT))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlreader.IsDBNull(iIndex))
                    {
                        if (pSqlreader.GetInt32(iIndex) > 0)
                        {
                            entRange = new EntityRange();
                            entRange.TotalRows = pSqlreader.GetInt32(iIndex);
                            _entBulkImportMaster.ListRange = entRange;
                        }
                    }
                }

            }
            return _entBulkImportMaster;
        }

        /// <summary>
        /// Add Bulk Import Master
        /// </summary>
        /// <param name="pEntBulkImportMaster"></param>
        /// <returns></returns>
        public BulkImport AddBulkImportMaster(BulkImport pEntBulkImportMaster)
        {
            _entBulkImportMaster = new BulkImport();
            _entBulkImportMaster = Update(pEntBulkImportMaster, Schema.Common.VAL_INSERT_MODE);
            return _entBulkImportMaster;
        }

        /// <summary>
        /// Edit Bulk Import Master
        /// </summary>
        /// <param name="pEntBulkImportMaster"></param>
        /// <returns></returns>
        public BulkImport EditBulkImportMaster(BulkImport pEntBulkImportMaster)
        {
            _entBulkImportMaster = new BulkImport();
            _entBulkImportMaster = Update(pEntBulkImportMaster, Schema.Common.VAL_UPDATE_MODE);
            return _entBulkImportMaster;
        }

        /// <summary>
        /// Delete Bulk Import Master
        /// </summary>
        /// <param name="pEntBulkImportMaster"></param>
        /// <returns></returns>
        public BulkImport DeleteBulkImportMaster(BulkImport pEntBulkImportMaster)
        {
            int iRows = 0;
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.BulkImport.PROC_DEL_BULK_IMP_MSTR;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntBulkImportMaster.ClientId);

                _sqlpara = new SqlParameter(Schema.BulkImport.PARA_IMPORT_ID, pEntBulkImportMaster.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                pEntBulkImportMaster = null;
            }
            catch (Exception expCommon)
            {
                _expCustom = new YPLMS.Services.CustomException(_strMessageId, YPLMS.Services.CustomException.WhoCallsMe(), YPLMS.Services.ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntBulkImportMaster;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="pEntBulkImportMaster"></param>
        /// <param name="pUpdateMode"></param>
        /// <returns></returns>
        private BulkImport Update(BulkImport pEntBulkImportMaster, string pUpdateMode)
        {
            int iRows = 0;
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.BulkImport.PROC_UPS_BULK_IMP_MSTR;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntBulkImportMaster.ClientId);
                if (pUpdateMode == Schema.Common.VAL_INSERT_MODE)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                    pEntBulkImportMaster.ID = YPLMS.Services.IDGenerator.GetStringGUID();
                }
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.BulkImport.PARA_IMPORT_ID, pEntBulkImportMaster.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntBulkImportMaster.ClientId))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntBulkImportMaster.ClientId);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntBulkImportMaster.CreatedById))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntBulkImportMaster.CreatedById);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.BulkImport.PARA_IS_ACTIVE, pEntBulkImportMaster.IsActive);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.BulkImport.PARA_SEND_EMAIL, pEntBulkImportMaster.SendEmail);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.BulkImport.PARA_DIRECT_SEND_EMAIL, pEntBulkImportMaster.DirectSendEmail);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.BulkImport.PARA_CREATE_PWD, pEntBulkImportMaster.CreatePassword);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_IS_IN_PROCESS, pEntBulkImportMaster.IsInProcess);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.BulkImport.PARA_KEEP_EXISTING_PWD, pEntBulkImportMaster.KeepExistingPassword);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntBulkImportMaster.AddNewCustomFields))
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_ADD_NEW_CUSTOM_FIELDS, pEntBulkImportMaster.AddNewCustomFields);
                else
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_ADD_NEW_CUSTOM_FIELDS, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntBulkImportMaster.AddNewOrganizationLevels))
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_ADD_NEW_ORG_LEVEL, pEntBulkImportMaster.AddNewOrganizationLevels);
                else
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_ADD_NEW_ORG_LEVEL, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntBulkImportMaster.UploadedFileName))
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_UPLOAD_FILE_NAME, pEntBulkImportMaster.UploadedFileName);
                else
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_UPLOAD_FILE_NAME, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.BulkImport.PARA_IMPORT_ACTION, pEntBulkImportMaster.ImportAction.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntBulkImportMaster.ScheduledDateAndTime) < 0)
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_SCH_DATE_AND_TIME, pEntBulkImportMaster.ScheduledDateAndTime);
                else
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_SCH_DATE_AND_TIME, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntBulkImportMaster.FieldMapping))
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_FIELD_MAPPING, pEntBulkImportMaster.FieldMapping);
                else
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_FIELD_MAPPING, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntBulkImportMaster.ImportFilePath))
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_IMPORT_FILE_PATH, pEntBulkImportMaster.ImportFilePath);
                else
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_IMPORT_FILE_PATH, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntBulkImportMaster.Password))
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_PWD, pEntBulkImportMaster.Password);
                else
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_PWD, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntBulkImportMaster.ScheduledEmailTaskId))
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_SCHEDULED_EMAIL_TASK_ID, pEntBulkImportMaster.ScheduledEmailTaskId);
                else
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_SCHEDULED_EMAIL_TASK_ID, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntBulkImportMaster.Title))
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_TITLE, pEntBulkImportMaster.Title);
                else
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_TITLE, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntBulkImportMaster.EmailType))
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_EMAIL_TYPE, pEntBulkImportMaster.EmailType);
                else
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_EMAIL_TYPE, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntBulkImportMaster.EmailTemplateId))
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_EMAIL_TEMPLATE_ID, pEntBulkImportMaster.EmailTemplateId);
                else
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_EMAIL_TEMPLATE_ID, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.BulkImport.PARA_IS_OVERRIDE_PREVIOUS_ASSGNMT, pEntBulkImportMaster.IsOverridePreviousAssignment);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.BulkImport.PARA_IS_ASSIGNED, pEntBulkImportMaster.IsAssigned);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.BulkImport.PARA_IMPORT_STATUS, pEntBulkImportMaster.ImportStatus.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntBulkImportMaster.TaskId))
                    _sqlpara = new SqlParameter(Schema.Task.PARA_TASK_ID, pEntBulkImportMaster.TaskId);
                else
                    _sqlpara = new SqlParameter(Schema.Task.PARA_TASK_ID, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.BulkImport.PARA_IMPORT_TYPE, pEntBulkImportMaster.ImportType.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntBulkImportMaster.Comment))
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_COMMENT, pEntBulkImportMaster.Comment);
                else
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_COMMENT, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntBulkImportMaster.CompletionDate.HasValue)
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_COMPLETION_DATE, pEntBulkImportMaster.CompletionDate.Value);
                else
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_COMPLETION_DATE, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new YPLMS.Services.CustomException(_strMessageId, YPLMS.Services.CustomException.WhoCallsMe(), YPLMS.Services.ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntBulkImportMaster;
        }

        /// <summary>
        /// Get Bulk Import Master By ID
        /// </summary>
        /// <param name="pEntBulkImportMaster"></param>
        /// <returns></returns>
        public BulkImport GetBulkImportMasterByID(BulkImport pEntBulkImportMaster)
        {
            List<BulkImport> entListBulkImportMaster = new List<BulkImport>();
            try
            {
                entListBulkImportMaster = GetBulkImportMasterList(pEntBulkImportMaster);
                if (entListBulkImportMaster != null && entListBulkImportMaster.Count > 0)
                    _entBulkImportMaster = entListBulkImportMaster[0];
            }
            catch (Exception expCommon)
            {
                _expCustom = new YPLMS.Services.CustomException(_strMessageId, YPLMS.Services.CustomException.WhoCallsMe(), YPLMS.Services.ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return _entBulkImportMaster;
        }

        /// <summary>
        /// To search bulk import tasks by import status and date range criteria.
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<BulkImport> FindBulkImportTasksSchedular(Search pEntSearch)
        {
            BulkImportAdaptor entBulkImportAdpt = new BulkImportAdaptor();
            BulkImport entBulkImport = new BulkImport();
            List<BulkImport> entListBulkImport = new List<BulkImport>();
            SqlConnection _sqlConnection = null;
            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.BulkImport.PROC_FIND_BULK_IMPORT_TASKS_SCHEDULAR, _sqlConnection);
            if (pEntSearch.ListRange != null)
            {
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntSearch.ListRange.PageIndex);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntSearch.ListRange.PageSize);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                _sqlcmd.Parameters.Add(_sqlpara);
                if (!string.IsNullOrEmpty(pEntSearch.ListRange.RequestedById))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
            }
            if (pEntSearch.SearchObject != null && pEntSearch.SearchObject.Count > 0)
            {
                entBulkImport = new BulkImport();
                entBulkImport = (BulkImport)pEntSearch.SearchObject[0];
                if (entBulkImport.ImportStatus != ImportStatus.None)
                {
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_IMPORT_STATUS, entBulkImport.ImportStatus.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (DateTime.MinValue.CompareTo(entBulkImport.ScheduledDateAndTime) < 0)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_FROM_DATE, entBulkImport.ScheduledDateAndTime);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (entBulkImport.ImmeidateForSchedular)
                {
                    _sqlpara = new SqlParameter(Schema.BulkImport.PARA_IS_IMMEDIATE_FOR_SCHEDULAR, entBulkImport.ImmeidateForSchedular);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntSearch.SearchObject.Count > 1)
                {
                    entBulkImport = new BulkImport();
                    entBulkImport = (BulkImport)pEntSearch.SearchObject[1];
                    if (DateTime.MinValue.CompareTo(entBulkImport.ScheduledDateAndTime) < 0)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_TO_DATE, entBulkImport.ScheduledDateAndTime);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
            }
            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entBulkImport = new BulkImport();
                    entBulkImport = FillObject(_sqlreader);
                    entListBulkImport.Add(entBulkImport);
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
            return entListBulkImport;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntBulkImport"></param>
        /// <returns></returns>
        public BulkImport Get(BulkImport pEntBulkImport)
        {
            return GetBulkImportMasterByID(pEntBulkImport);
        }
        /// <summary>
        /// Update BulkImport
        /// </summary>
        /// <param name="pEntBulkImport"></param>
        /// <returns></returns>
        public BulkImport Update(BulkImport pEntBulkImport)
        {
            return EditBulkImportMaster(pEntBulkImport);
        }
        #endregion
    }
}
