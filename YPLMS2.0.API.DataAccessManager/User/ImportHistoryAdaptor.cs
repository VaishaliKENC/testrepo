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
    public class ImportHistoryAdaptor : IDataManager<ImportHistory>,IImportHistoryAdaptor<ImportHistory>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        SqlDataAdapter _sqladapter = null;
        ImportHistory _entImportHistory = null;
        List<ImportHistory> _entListImportHistory = null;
        SqlConnection _sqlcon = null;
        DataTable _dtable = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.ImportHistory.IMP_HISTORY_ERROR_MSG_ID;
        #endregion

        /// <summary>
        /// Get Import History By Id
        /// </summary>
        /// <param name="pEntImportHistory"></param>
        /// <returns></returns>
        public ImportHistory GetImportHistory(ImportHistory pEntImportHistory)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.ImportHistory.PROC_GET_IMPORT_HISTORY;
            _entImportHistory = new ImportHistory();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntImportHistory.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_ID, pEntImportHistory.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entImportHistory = FillObject(_sqlreader, _sqlObject);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return _entImportHistory;
        }

        /// <summary>
        /// Find Import History
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<ImportHistory> FindImportHistory(Search pEntSearch)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.ImportHistory.PROC_GET_ALL_IMPORT_HISTORY;
            _entListImportHistory = new List<ImportHistory>();
            _entImportHistory = new ImportHistory();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntSearch.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntSearch.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntSearch.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntSearch.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntSearch.SearchObject.Count > 0)
                {
                    _entImportHistory = new ImportHistory();
                    _entImportHistory = (ImportHistory)pEntSearch.SearchObject[0];

                    _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_TYPE, _entImportHistory.ImportType.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (_entImportHistory.ImportAction != ImportAction.None)
                    {
                        _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_ACTION, _entImportHistory.ImportAction.ToString());
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (_entImportHistory.ImportStatus != ImportStatus.None)
                    {
                        _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_STATUS, _entImportHistory.ImportStatus.ToString());
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (!string.IsNullOrEmpty(_entImportHistory.FileName))
                    {
                        _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_FILE_NAME, _entImportHistory.FileName);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (pEntSearch.SearchObject.Count > 1)
                    {
                        _entImportHistory = new ImportHistory();
                        _entImportHistory = (ImportHistory)pEntSearch.SearchObject[1];
                        if (DateTime.MinValue.CompareTo(_entImportHistory.DateCreated) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_DATE_FROM, _entImportHistory.DateCreated);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                    if (pEntSearch.SearchObject.Count > 2)
                    {
                        _entImportHistory = new ImportHistory();
                        _entImportHistory = (ImportHistory)pEntSearch.SearchObject[2];
                        if (DateTime.MinValue.CompareTo(_entImportHistory.DateCreated) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_DATE_TO, _entImportHistory.DateCreated);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entImportHistory = FillObject(_sqlreader, _sqlObject);
                    _entListImportHistory.Add(_entImportHistory);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return _entListImportHistory;
        }

        /// <summary>
        /// Find Assignment Import History
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<ImportHistory> FindAssignmentImportHistory(Search pEntSearch)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.ImportHistory.PROC_GET_ALL_IMPORT_HISTORY_ASSIGNMENT;
            _entListImportHistory = new List<ImportHistory>();
            _entImportHistory = new ImportHistory();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntSearch.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntSearch.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntSearch.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntSearch.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntSearch.SearchObject.Count > 0)
                {
                    _entImportHistory = new ImportHistory();
                    _entImportHistory = (ImportHistory)pEntSearch.SearchObject[0];

                    if (_entImportHistory.ImportAction != ImportAction.None)
                    {
                        _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_ACTION, _entImportHistory.ImportAction.ToString());
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (_entImportHistory.ImportStatus != ImportStatus.None)
                    {
                        _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_STATUS, _entImportHistory.ImportStatus.ToString());
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (!string.IsNullOrEmpty(_entImportHistory.FileName))
                    {
                        _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_FILE_NAME, _entImportHistory.FileName);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (pEntSearch.SearchObject.Count > 1)
                    {
                        _entImportHistory = new ImportHistory();
                        _entImportHistory = (ImportHistory)pEntSearch.SearchObject[1];
                        if (DateTime.MinValue.CompareTo(_entImportHistory.DateCreated) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_DATE_FROM, _entImportHistory.DateCreated);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                    if (pEntSearch.SearchObject.Count > 2)
                    {
                        _entImportHistory = new ImportHistory();
                        _entImportHistory = (ImportHistory)pEntSearch.SearchObject[2];
                        if (DateTime.MinValue.CompareTo(_entImportHistory.DateCreated) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_DATE_TO, _entImportHistory.DateCreated);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                    if (!string.IsNullOrEmpty(_entImportHistory.CreatedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, _entImportHistory.CreatedById.ToString());
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entImportHistory = FillObject(_sqlreader, _sqlObject);
                    _entListImportHistory.Add(_entImportHistory);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return _entListImportHistory;
        }

        /// <summary>
        /// Find Questions Import History
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<ImportHistory> FindQuestionsImportHistory(Search pEntSearch)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.ImportHistory.PROC_GET_ALL_IMPORT_HISTORY_QUESTIONS;
            _entListImportHistory = new List<ImportHistory>();
            _entImportHistory = new ImportHistory();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntSearch.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntSearch.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntSearch.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntSearch.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntSearch.SearchObject.Count > 0)
                {
                    _entImportHistory = new ImportHistory();
                    _entImportHistory = (ImportHistory)pEntSearch.SearchObject[0];

                    if (_entImportHistory.ImportAction != ImportAction.None)
                    {
                        _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_ACTION, _entImportHistory.ImportAction.ToString());
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (_entImportHistory.ImportStatus != ImportStatus.None)
                    {
                        _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_STATUS, _entImportHistory.ImportStatus.ToString());
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (!string.IsNullOrEmpty(_entImportHistory.CreatedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, _entImportHistory.CreatedById.ToString());
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (!string.IsNullOrEmpty(_entImportHistory.FileName))
                    {
                        _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_FILE_NAME, _entImportHistory.FileName);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (pEntSearch.SearchObject.Count > 1)
                    {
                        _entImportHistory = new ImportHistory();
                        _entImportHistory = (ImportHistory)pEntSearch.SearchObject[1];
                        if (DateTime.MinValue.CompareTo(_entImportHistory.DateCreated) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_DATE_FROM, _entImportHistory.DateCreated);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                    if (pEntSearch.SearchObject.Count > 2)
                    {
                        _entImportHistory = new ImportHistory();
                        _entImportHistory = (ImportHistory)pEntSearch.SearchObject[2];
                        if (DateTime.MinValue.CompareTo(_entImportHistory.DateCreated) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_DATE_TO, _entImportHistory.DateCreated);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entImportHistory = FillObject(_sqlreader, _sqlObject);
                    _entListImportHistory.Add(_entImportHistory);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return _entListImportHistory;
        }

        /// <summary>
        /// To fill Import History object data from reader.
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <returns>ImportHistory object</returns>
        private ImportHistory FillObject(SqlDataReader pSqlreader, SQLObject pSqlObject)
        {
            ImportHistory entImportHistory = new ImportHistory();
            EntityRange entRange = null;
            int index;

            index = pSqlreader.GetOrdinal(Schema.ImportHistory.COL_IMPORT_ID);
            if (!pSqlreader.IsDBNull(index))
                entImportHistory.ID = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.ImportHistory.COL_FILE_NAME);
            if (!pSqlreader.IsDBNull(index))
                entImportHistory.FileName = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.ImportHistory.COL_FILE_PATH);
            if (!pSqlreader.IsDBNull(index))
                entImportHistory.FilePath = pSqlreader.GetString(index);
            if (entImportHistory.FilePath.Contains(@"Clients"))
            {
                entImportHistory.FilePath = EncryptionManager.Encrypt(entImportHistory.FilePath);
            }
            entImportHistory.FilePath = EncryptionManager.Decrypt(entImportHistory.FilePath);

            index = pSqlreader.GetOrdinal(Schema.ImportHistory.COL_IMPORT_BY_ID);
            if (!pSqlreader.IsDBNull(index))
                entImportHistory.CreatedById = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.ImportHistory.COL_IMPORT_DATE);
            if (!pSqlreader.IsDBNull(index))
                entImportHistory.ImportDate = pSqlreader.GetDateTime(index);

            index = pSqlreader.GetOrdinal(Schema.ImportHistory.COL_IMPORT_TYPE);
            entImportHistory.ImportType = (ImportType)Enum.Parse(typeof(ImportType), pSqlreader.GetString(index));

            index = pSqlreader.GetOrdinal(Schema.ImportHistory.COL_IMPORT_ACTION);
            entImportHistory.ImportAction = (ImportAction)Enum.Parse(typeof(ImportAction), pSqlreader.GetString(index));

            index = pSqlreader.GetOrdinal(Schema.ImportHistory.COL_STATUS);
            if (!pSqlreader.IsDBNull(index))
                entImportHistory.ImportStatus = (ImportStatus)Enum.Parse(typeof(ImportStatus), pSqlreader.GetString(index));

            index = pSqlreader.GetOrdinal(Schema.ImportHistory.COL_CLIENT_ID);
            if (!pSqlreader.IsDBNull(index))
                entImportHistory.ClientId = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.ImportHistory.COL_IMPORT_LOG);
            if (!pSqlreader.IsDBNull(index))
                entImportHistory.ImportLog = pSqlreader.GetString(index);

            if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ImportHistory.COL_ADMINISTRATOR_NAME))
            {
                index = pSqlreader.GetOrdinal(Schema.ImportHistory.COL_ADMINISTRATOR_NAME);
                if (!pSqlreader.IsDBNull(index))
                    entImportHistory.AdministratorName = pSqlreader.GetString(index);
            }

            //In every proc can not have these columns So it check that it exist in it.
            if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Common.COL_TOTAL_COUNT))
            {
                index = pSqlreader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                if (!pSqlreader.IsDBNull(index))
                {

                    entRange = new EntityRange();
                    index = pSqlreader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlreader.IsDBNull(index))
                        entRange.TotalRows = pSqlreader.GetInt32(index);
                    entImportHistory.ListRange = entRange;
                }
            }

            return entImportHistory;
        }

        /// <summary>
        /// Add import History
        /// </summary>
        /// <param name="pEntImportHistory"></param>
        /// <returns></returns>
        public ImportHistory AddImportHistory(ImportHistory pEntImportHistory)
        {
            _entImportHistory = new ImportHistory();
            _entImportHistory = Update(pEntImportHistory, Schema.Common.VAL_INSERT_MODE);
            return _entImportHistory;
        }

        /// <summary>
        /// Add import History
        /// </summary>
        /// <param name="pEntImportHistory"></param>
        /// <returns></returns>
        public ImportHistory AddImportHistoryWithFile(ImportHistory pEntImportHistory)
        {
            string _strCompletePath = string.Empty;

            #region Add in Database

            _entImportHistory = new ImportHistory();
            // Store Uploaded FileName for Log creation in DB
            _entImportHistory = Update(pEntImportHistory, Schema.Common.VAL_INSERT_MODE);

            #endregion

            #region Upload Error File Log to Content Server
            try
            {
                FileHandler FtpUpload = new FileHandler(pEntImportHistory.ClientId);
                string strFileName = pEntImportHistory.ID + ".html";
                System.Text.StringBuilder sbHTML = new System.Text.StringBuilder();
                sbHTML.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
                sbHTML.Append(Convert.ToString(pEntImportHistory.ImportLog));
                byte[] bytFileData = null;
                bytFileData = System.Text.Encoding.ASCII.GetBytes(sbHTML.ToString());
                //check for upload  folder exist or not 
                if (!FtpUpload.IsFolderExist(FileHandler.CSV_FOLDER_PATH, pEntImportHistory.ClientId))
                {
                    FtpUpload.CreateFolder(FileHandler.CSV_FOLDER_PATH, pEntImportHistory.ClientId);
                }
                // Get Uploaded FileName for Log creation
                _strCompletePath = FtpUpload.Uploadfile(FileHandler.CSV_FOLDER_PATH + "/" + pEntImportHistory.ClientId, strFileName, bytFileData);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            #endregion

            #region Update FileName in DB

            ImportHistory pEntImportHistoryFileName = new ImportHistory();
            pEntImportHistoryFileName.ID = pEntImportHistory.ID;
            pEntImportHistoryFileName.ImportLog = _strCompletePath;
            pEntImportHistoryFileName.ClientId = pEntImportHistory.ClientId;
            pEntImportHistoryFileName = UpdateLogFileName(pEntImportHistoryFileName);

            #endregion


            return _entImportHistory;
        }


        /// <summary>
        /// Update import history details: Pre Log maintain here With File
        /// </summary>
        /// <param name="pEntImportHistory"></param>        
        /// <returns></returns>
        public ImportHistory UpdateDetailsWithFile(ImportHistory pEntImportHistory)
        {

            try
            {
                FileHandler FtpUpload = new FileHandler(pEntImportHistory.ClientId);
                string strFileName = pEntImportHistory.ID + ".html";

                string strUploadFolderPath = FtpUpload.RootSharedPath;
                strUploadFolderPath = strUploadFolderPath.Replace("\\\\", @"\");
                string pstrFtpFolderpath = FileHandler.CSV_FOLDER_PATH + "/" + pEntImportHistory.ClientId;
                pstrFtpFolderpath = pstrFtpFolderpath.Replace("//", "/");
                pstrFtpFolderpath = pstrFtpFolderpath.Replace("/", @"\");
                // Get Uploaded Error File Log path and Name
                string pstrFileName = strUploadFolderPath + pstrFtpFolderpath + @"\" + strFileName;
                // Write Error log
                using (StreamWriter sw = File.AppendText(pstrFileName))
                {
                    sw.WriteLine(Convert.ToString(pEntImportHistory.ImportLog));

                }

            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntImportHistory;
        }

        /// <summary>
        /// Edit Import History
        /// </summary>
        /// <param name="pEntImportHistory"></param>
        /// <returns></returns>
        public ImportHistory EditImportHistory(ImportHistory pEntImportHistory)
        {
            _entImportHistory = new ImportHistory();
            _entImportHistory = Update(pEntImportHistory, Schema.Common.VAL_UPDATE_MODE);
            return _entImportHistory;
        }

        /// <summary>
        /// Update 
        /// </summary>
        /// <param name="pEntImportHistory"></param>
        /// <param name="pStrUpdateMode"></param>
        /// <returns></returns>
        private ImportHistory Update(ImportHistory pEntImportHistory, string pStrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.ImportHistory.PROC_UPDATE_IMPORT_HISTORY;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntImportHistory.ClientId);
                if (pStrUpdateMode == Schema.Common.VAL_INSERT_MODE)
                {
                    if (string.IsNullOrEmpty(pEntImportHistory.ID))
                        pEntImportHistory.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                }
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_ID, pEntImportHistory.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntImportHistory.FileName))
                    _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_FILE_NAME, pEntImportHistory.FileName);
                else
                    _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_FILE_NAME, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntImportHistory.FilePath))
                    _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_FILE_PATH, pEntImportHistory.FilePath);
                else
                    _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_FILE_PATH, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_TYPE, pEntImportHistory.ImportType.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_STATUS, pEntImportHistory.ImportStatus.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntImportHistory.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_BY_ID, pEntImportHistory.CreatedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_ACTION, pEntImportHistory.ImportAction.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntImportHistory.ImportLog))
                    _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_LOG, pEntImportHistory.ImportLog);
                else
                    _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_LOG, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntImportHistory;
        }

        /// <summary>
        /// Update 
        /// </summary>
        /// <param name="pEntImportHistory"></param>
        /// <param name="pStrUpdateMode"></param>
        /// <returns></returns>
        public ImportHistory UpdateLogFileName(ImportHistory pEntImportHistory)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.ImportHistory.PROC_UPDATE_IMPORT_HISTORY_BULK_ASSIGNMENT;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntImportHistory.ClientId);

                _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_ID, pEntImportHistory.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntImportHistory.ImportLog))
                    _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_LOG, pEntImportHistory.ImportLog);
                else
                    _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_LOG, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntImportHistory;
        }

        /// <summary>
        /// Update import history details: Pre Log maintain here
        /// </summary>
        /// <param name="pEntImportHistory"></param>        
        /// <returns></returns>
        public ImportHistory UpdateDetails(ImportHistory pEntImportHistory)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.ImportHistory.PROC_UPDATE_IMPORT_HISTORY_DETAILS;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntImportHistory.ClientId);

                _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_ID, pEntImportHistory.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntImportHistory.FileName))
                    _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_FILE_NAME, pEntImportHistory.FileName);
                else
                    _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_FILE_NAME, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntImportHistory.FilePath))
                    _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_FILE_PATH, pEntImportHistory.FilePath);
                else
                    _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_FILE_PATH, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_TYPE, pEntImportHistory.ImportType.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_STATUS, pEntImportHistory.ImportStatus.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntImportHistory.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_BY_ID, pEntImportHistory.CreatedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_ACTION, pEntImportHistory.ImportAction.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntImportHistory.ImportLog))
                    _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_LOG, pEntImportHistory.ImportLog);
                else
                    _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_LOG, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntImportHistory;
        }

        /// <summary>
        /// Delete Selected Import History
        /// </summary>
        /// <param name="pEntListImportHistory"></param>
        /// <returns></returns>
        public List<ImportHistory> DeleteSelectedImportHistory(List<ImportHistory> pEntListImportHistory)
        {
            string strImportHistoryIds = string.Empty;
            string strClientId = string.Empty;
            int iBatchSize = 0;
            List<ImportHistory> entListImportHistory = new List<ImportHistory>();
            _dtable = new DataTable();
            _dtable.Columns.Add(Schema.ImportHistory.COL_IMPORT_ID);
            _sqlObject = new SQLObject();
            try
            {
                foreach (ImportHistory entImportHistory in pEntListImportHistory)
                {

                    if (string.IsNullOrEmpty(_strConnString))
                        _strConnString = _sqlObject.GetClientDBConnString(entImportHistory.ClientId);

                    DataRow drow = _dtable.NewRow();
                    drow[Schema.ImportHistory.COL_IMPORT_ID] = entImportHistory.ID;
                    _dtable.Rows.Add(drow);
                    iBatchSize = iBatchSize + 1;
                    entListImportHistory.Add(entImportHistory);
                }
                if (_dtable.Rows.Count > 0)
                {
                    _sqlcon = new SqlConnection(_strConnString);
                    _sqlcmd = new SqlCommand();
                    _sqlcmd.CommandText = Schema.ImportHistory.PROC_DELETE_SELECTED_IMPORT_HISTORY;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;
                    _sqladapter = new SqlDataAdapter();
                    _sqlcmd.Connection = _sqlcon;
                    _sqladapter.InsertCommand = _sqlcmd;
                    _sqladapter.InsertCommand.Parameters.Add(Schema.ImportHistory.PARA_IMPORT_ID, SqlDbType.VarChar, 100, Schema.ImportHistory.COL_IMPORT_ID);
                    _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    _sqladapter.UpdateBatchSize = iBatchSize;
                    _sqladapter.Update(_dtable);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListImportHistory;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntImportHistory"></param>
        /// <returns></returns>
        public ImportHistory Get(ImportHistory pEntImportHistory)
        {
            return GetImportHistory(pEntImportHistory);
        }
        /// <summary>
        /// Update ImportHistory
        /// </summary>
        /// <param name="pEntImportHistory"></param>
        /// <returns></returns>
        public ImportHistory Update(ImportHistory pEntImportHistory)
        {
            return EditImportHistory(pEntImportHistory);
        }
        #endregion
    }
}
