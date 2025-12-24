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
    public class StudentListAdaptor : IDataManager<StudentList>, IStudentListAdaptor<StudentList>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        SqlDataAdapter _sqladapter = null;
        StudentList _entStudentList = null;
        List<StudentList> _entListStudentList = null;
        SqlConnection _sqlcon = null;
        DataTable _dtable = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.StudentList.IMP_HISTORY_ERROR_MSG_ID;
        #endregion

        /// <summary>
        /// Get Import History By Id
        /// </summary>
        /// <param name="pEntStudentList"></param>
        /// <returns></returns>
        public StudentList GetStudentList(StudentList pEntStudentList)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.StudentList.PROC_GET_IMPORT_HISTORY;
            _entStudentList = new StudentList();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntStudentList.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_ID, pEntStudentList.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entStudentList = FillObject(_sqlreader, _sqlObject);
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
            return _entStudentList;
        }

        /// <summary>
        /// Find Import History
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<StudentList> FindStudentList(Search pEntSearch)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.StudentList.PROC_GET_ALL_IMPORT_HISTORY;
            _entListStudentList = new List<StudentList>();
            _entStudentList = new StudentList();
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
                    _entStudentList = new StudentList();
                    _entStudentList = (StudentList)pEntSearch.SearchObject[0];

                    _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_TYPE, _entStudentList.ImportType.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (_entStudentList.ImportAction != ImportAction.None)
                    {
                        _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_ACTION, _entStudentList.ImportAction.ToString());
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (_entStudentList.ImportStatus != ImportStatus.None)
                    {
                        _sqlpara = new SqlParameter(Schema.StudentList.PARA_STATUS, _entStudentList.ImportStatus.ToString());
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (!string.IsNullOrEmpty(_entStudentList.FileName))
                    {
                        _sqlpara = new SqlParameter(Schema.StudentList.PARA_FILE_NAME, _entStudentList.FileName);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (pEntSearch.SearchObject.Count > 1)
                    {
                        _entStudentList = new StudentList();
                        _entStudentList = (StudentList)pEntSearch.SearchObject[1];
                        if (DateTime.MinValue.CompareTo(_entStudentList.DateCreated) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_DATE_FROM, _entStudentList.DateCreated);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                    if (pEntSearch.SearchObject.Count > 2)
                    {
                        _entStudentList = new StudentList();
                        _entStudentList = (StudentList)pEntSearch.SearchObject[2];
                        if (DateTime.MinValue.CompareTo(_entStudentList.DateCreated) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_DATE_TO, _entStudentList.DateCreated);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entStudentList = FillObject(_sqlreader, _sqlObject);
                    _entListStudentList.Add(_entStudentList);
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
            return _entListStudentList;
        }

        /// <summary>
        /// Find Assignment Import History
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<StudentList> FindAssignmentStudentList(Search pEntSearch)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.StudentList.PROC_GET_ALL_IMPORT_HISTORY_ASSIGNMENT;
            _entListStudentList = new List<StudentList>();
            _entStudentList = new StudentList();
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
                    _entStudentList = new StudentList();
                    _entStudentList = (StudentList)pEntSearch.SearchObject[0];

                    if (_entStudentList.ImportAction != ImportAction.None)
                    {
                        _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_ACTION, _entStudentList.ImportAction.ToString());
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (_entStudentList.ImportStatus != ImportStatus.None)
                    {
                        _sqlpara = new SqlParameter(Schema.StudentList.PARA_STATUS, _entStudentList.ImportStatus.ToString());
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (!string.IsNullOrEmpty(_entStudentList.FileName))
                    {
                        _sqlpara = new SqlParameter(Schema.StudentList.PARA_FILE_NAME, _entStudentList.FileName);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (pEntSearch.SearchObject.Count > 1)
                    {
                        _entStudentList = new StudentList();
                        _entStudentList = (StudentList)pEntSearch.SearchObject[1];
                        if (DateTime.MinValue.CompareTo(_entStudentList.DateCreated) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_DATE_FROM, _entStudentList.DateCreated);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                    if (pEntSearch.SearchObject.Count > 2)
                    {
                        _entStudentList = new StudentList();
                        _entStudentList = (StudentList)pEntSearch.SearchObject[2];
                        if (DateTime.MinValue.CompareTo(_entStudentList.DateCreated) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_DATE_TO, _entStudentList.DateCreated);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                    if (!string.IsNullOrEmpty(_entStudentList.CreatedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, _entStudentList.CreatedById.ToString());
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entStudentList = FillObject(_sqlreader, _sqlObject);
                    _entListStudentList.Add(_entStudentList);
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
            return _entListStudentList;
        }

        /// <summary>
        /// Find Questions Import History
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<StudentList> FindQuestionsStudentList(Search pEntSearch)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.StudentList.PROC_GET_ALL_IMPORT_HISTORY_QUESTIONS;
            _entListStudentList = new List<StudentList>();
            _entStudentList = new StudentList();
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
                    _entStudentList = new StudentList();
                    _entStudentList = (StudentList)pEntSearch.SearchObject[0];

                    if (_entStudentList.ImportAction != ImportAction.None)
                    {
                        _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_ACTION, _entStudentList.ImportAction.ToString());
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (_entStudentList.ImportStatus != ImportStatus.None)
                    {
                        _sqlpara = new SqlParameter(Schema.StudentList.PARA_STATUS, _entStudentList.ImportStatus.ToString());
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (!string.IsNullOrEmpty(_entStudentList.CreatedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, _entStudentList.CreatedById.ToString());
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (!string.IsNullOrEmpty(_entStudentList.FileName))
                    {
                        _sqlpara = new SqlParameter(Schema.StudentList.PARA_FILE_NAME, _entStudentList.FileName);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (pEntSearch.SearchObject.Count > 1)
                    {
                        _entStudentList = new StudentList();
                        _entStudentList = (StudentList)pEntSearch.SearchObject[1];
                        if (DateTime.MinValue.CompareTo(_entStudentList.DateCreated) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_DATE_FROM, _entStudentList.DateCreated);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                    if (pEntSearch.SearchObject.Count > 2)
                    {
                        _entStudentList = new StudentList();
                        _entStudentList = (StudentList)pEntSearch.SearchObject[2];
                        if (DateTime.MinValue.CompareTo(_entStudentList.DateCreated) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_DATE_TO, _entStudentList.DateCreated);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entStudentList = FillObject(_sqlreader, _sqlObject);
                    _entListStudentList.Add(_entStudentList);
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
            return _entListStudentList;
        }

        /// <summary>
        /// To fill Import History object data from reader.
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <returns>StudentList object</returns>
        private StudentList FillObject(SqlDataReader pSqlreader, SQLObject pSqlObject)
        {
            StudentList entStudentList = new StudentList();
            EntityRange entRange = null;
            int index;

            index = pSqlreader.GetOrdinal(Schema.StudentList.COL_IMPORT_ID);
            if (!pSqlreader.IsDBNull(index))
                entStudentList.ID = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.StudentList.COL_FILE_NAME);
            if (!pSqlreader.IsDBNull(index))
                entStudentList.FileName = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.StudentList.COL_FILE_PATH);
            if (!pSqlreader.IsDBNull(index))
                entStudentList.FilePath = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.StudentList.COL_IMPORT_BY_ID);
            if (!pSqlreader.IsDBNull(index))
                entStudentList.CreatedById = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.StudentList.COL_IMPORT_DATE);
            if (!pSqlreader.IsDBNull(index))
                entStudentList.ImportDate = pSqlreader.GetDateTime(index);

            index = pSqlreader.GetOrdinal(Schema.StudentList.COL_IMPORT_TYPE);
            entStudentList.ImportType = (ImportType)Enum.Parse(typeof(ImportType), pSqlreader.GetString(index));

            index = pSqlreader.GetOrdinal(Schema.StudentList.COL_IMPORT_ACTION);
            entStudentList.ImportAction = (ImportAction)Enum.Parse(typeof(ImportAction), pSqlreader.GetString(index));

            index = pSqlreader.GetOrdinal(Schema.StudentList.COL_STATUS);
            if (!pSqlreader.IsDBNull(index))
                entStudentList.ImportStatus = (ImportStatus)Enum.Parse(typeof(ImportStatus), pSqlreader.GetString(index));

            index = pSqlreader.GetOrdinal(Schema.StudentList.COL_CLIENT_ID);
            if (!pSqlreader.IsDBNull(index))
                entStudentList.ClientId = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.StudentList.COL_IMPORT_LOG);
            if (!pSqlreader.IsDBNull(index))
                entStudentList.ImportLog = pSqlreader.GetString(index);

            if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.StudentList.COL_ADMINISTRATOR_NAME))
            {
                index = pSqlreader.GetOrdinal(Schema.StudentList.COL_ADMINISTRATOR_NAME);
                if (!pSqlreader.IsDBNull(index))
                    entStudentList.AdministratorName = pSqlreader.GetString(index);
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
                    entStudentList.ListRange = entRange;
                }
            }

            return entStudentList;
        }

        /// <summary>
        /// Add import History
        /// </summary>
        /// <param name="pEntStudentList"></param>
        /// <returns></returns>
        public StudentList AddStudentList(StudentList pEntStudentList)
        {
            _entStudentList = new StudentList();
            _entStudentList = Update(pEntStudentList, Schema.Common.VAL_INSERT_MODE);
            return _entStudentList;
        }

        /// <summary>
        /// Add import History
        /// </summary>
        /// <param name="pEntStudentList"></param>
        /// <returns></returns>
        public StudentList AddStudentListWithFile(StudentList pEntStudentList)
        {
            string _strCompletePath = string.Empty;

            #region Add in Database

            _entStudentList = new StudentList();
            // Store Uploaded FileName for Log creation in DB
            _entStudentList = Update(pEntStudentList, Schema.Common.VAL_INSERT_MODE);

            #endregion

            #region Upload Error File Log to Content Server
            try
            {
                FileHandler FtpUpload = new FileHandler(pEntStudentList.ClientId);
                string strFileName = pEntStudentList.ID + ".html";
                System.Text.StringBuilder sbHTML = new System.Text.StringBuilder();
                sbHTML.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
                sbHTML.Append(Convert.ToString(pEntStudentList.ImportLog));
                byte[] bytFileData = null;
                bytFileData = System.Text.Encoding.ASCII.GetBytes(sbHTML.ToString());
                //check for upload  folder exist or not 
                if (!FtpUpload.IsFolderExist(FileHandler.CSV_FOLDER_PATH, pEntStudentList.ClientId))
                {
                    FtpUpload.CreateFolder(FileHandler.CSV_FOLDER_PATH, pEntStudentList.ClientId);
                }
                // Get Uploaded FileName for Log creation
                _strCompletePath = FtpUpload.Uploadfile(FileHandler.CSV_FOLDER_PATH + "/" + pEntStudentList.ClientId, strFileName, bytFileData);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            #endregion

            #region Update FileName in DB

            StudentList pEntStudentListFileName = new StudentList();
            pEntStudentListFileName.ID = pEntStudentList.ID;
            pEntStudentListFileName.ImportLog = _strCompletePath;
            pEntStudentListFileName.ClientId = pEntStudentList.ClientId;
            pEntStudentListFileName = UpdateLogFileName(pEntStudentListFileName);

            #endregion


            return _entStudentList;
        }


        /// <summary>
        /// Update import history details: Pre Log maintain here With File
        /// </summary>
        /// <param name="pEntStudentList"></param>        
        /// <returns></returns>
        public StudentList UpdateDetailsWithFile(StudentList pEntStudentList)
        {

            try
            {
                FileHandler FtpUpload = new FileHandler(pEntStudentList.ClientId);
                string strFileName = pEntStudentList.ID + ".html";

                string strUploadFolderPath = FtpUpload.RootSharedPath;
                strUploadFolderPath = strUploadFolderPath.Replace("\\\\", @"\");
                string pstrFtpFolderpath = FileHandler.CSV_FOLDER_PATH + "/" + pEntStudentList.ClientId;
                pstrFtpFolderpath = pstrFtpFolderpath.Replace("//", "/");
                pstrFtpFolderpath = pstrFtpFolderpath.Replace("/", @"\");
                // Get Uploaded Error File Log path and Name
                string pstrFileName = strUploadFolderPath + pstrFtpFolderpath + @"\" + strFileName;
                // Write Error log
                using (StreamWriter sw = File.AppendText(pstrFileName))
                {
                    sw.WriteLine(Convert.ToString(pEntStudentList.ImportLog));

                }

            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntStudentList;
        }

        /// <summary>
        /// Edit Import History
        /// </summary>
        /// <param name="pEntStudentList"></param>
        /// <returns></returns>
        public StudentList EditStudentList(StudentList pEntStudentList)
        {
            _entStudentList = new StudentList();
            _entStudentList = Update(pEntStudentList, Schema.Common.VAL_UPDATE_MODE);
            return _entStudentList;
        }

        /// <summary>
        /// Update 
        /// </summary>
        /// <param name="pEntStudentList"></param>
        /// <param name="pStrUpdateMode"></param>
        /// <returns></returns>
        private StudentList Update(StudentList pEntStudentList, string pStrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.StudentList.PROC_UPDATE_IMPORT_HISTORY;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntStudentList.ClientId);
                if (pStrUpdateMode == Schema.Common.VAL_INSERT_MODE)
                {
                    if (string.IsNullOrEmpty(pEntStudentList.ID))
                        pEntStudentList.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                }
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_ID, pEntStudentList.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntStudentList.FileName))
                    _sqlpara = new SqlParameter(Schema.StudentList.PARA_FILE_NAME, pEntStudentList.FileName);
                else
                    _sqlpara = new SqlParameter(Schema.StudentList.PARA_FILE_NAME, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntStudentList.FilePath))
                    _sqlpara = new SqlParameter(Schema.StudentList.PARA_FILE_PATH, pEntStudentList.FilePath);
                else
                    _sqlpara = new SqlParameter(Schema.StudentList.PARA_FILE_PATH, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_TYPE, pEntStudentList.ImportType.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.StudentList.PARA_STATUS, pEntStudentList.ImportStatus.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntStudentList.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_BY_ID, pEntStudentList.CreatedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_ACTION, pEntStudentList.ImportAction.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntStudentList.ImportLog))
                    _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_LOG, pEntStudentList.ImportLog);
                else
                    _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_LOG, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntStudentList;
        }

        /// <summary>
        /// Update 
        /// </summary>
        /// <param name="pEntStudentList"></param>
        /// <param name="pStrUpdateMode"></param>
        /// <returns></returns>
        private StudentList UpdateLogFileName(StudentList pEntStudentList)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.StudentList.PROC_UPDATE_IMPORT_HISTORY_BULK_ASSIGNMENT;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntStudentList.ClientId);

                _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_ID, pEntStudentList.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntStudentList.ImportLog))
                    _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_LOG, pEntStudentList.ImportLog);
                else
                    _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_LOG, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntStudentList;
        }

        /// <summary>
        /// Update import history details: Pre Log maintain here
        /// </summary>
        /// <param name="pEntStudentList"></param>        
        /// <returns></returns>
        public StudentList UpdateDetails(StudentList pEntStudentList)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.StudentList.PROC_UPDATE_IMPORT_HISTORY_DETAILS;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntStudentList.ClientId);

                _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_ID, pEntStudentList.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntStudentList.FileName))
                    _sqlpara = new SqlParameter(Schema.StudentList.PARA_FILE_NAME, pEntStudentList.FileName);
                else
                    _sqlpara = new SqlParameter(Schema.StudentList.PARA_FILE_NAME, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntStudentList.FilePath))
                    _sqlpara = new SqlParameter(Schema.StudentList.PARA_FILE_PATH, pEntStudentList.FilePath);
                else
                    _sqlpara = new SqlParameter(Schema.StudentList.PARA_FILE_PATH, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_TYPE, pEntStudentList.ImportType.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.StudentList.PARA_STATUS, pEntStudentList.ImportStatus.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntStudentList.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_BY_ID, pEntStudentList.CreatedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_ACTION, pEntStudentList.ImportAction.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntStudentList.ImportLog))
                    _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_LOG, pEntStudentList.ImportLog);
                else
                    _sqlpara = new SqlParameter(Schema.StudentList.PARA_IMPORT_LOG, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntStudentList;
        }

        /// <summary>
        /// Delete Selected Import History
        /// </summary>
        /// <param name="pEntListStudentList"></param>
        /// <returns></returns>
        public List<StudentList> DeleteSelectedStudentList(List<StudentList> pEntListStudentList)
        {
            string strStudentListIds = string.Empty;
            string strClientId = string.Empty;
            int iBatchSize = 0;
            List<StudentList> entListStudentList = new List<StudentList>();
            _dtable = new DataTable();
            _dtable.Columns.Add(Schema.StudentList.COL_IMPORT_ID);
            _sqlObject = new SQLObject();
            try
            {
                foreach (StudentList entStudentList in pEntListStudentList)
                {

                    if (string.IsNullOrEmpty(_strConnString))
                        _strConnString = _sqlObject.GetClientDBConnString(entStudentList.ClientId);

                    DataRow drow = _dtable.NewRow();
                    drow[Schema.StudentList.COL_IMPORT_ID] = entStudentList.ID;
                    _dtable.Rows.Add(drow);
                    iBatchSize = iBatchSize + 1;
                    entListStudentList.Add(entStudentList);
                }
                if (_dtable.Rows.Count > 0)
                {
                    _sqlcon = new SqlConnection(_strConnString);
                    _sqlcmd = new SqlCommand();
                    _sqlcmd.CommandText = Schema.StudentList.PROC_DELETE_SELECTED_IMPORT_HISTORY;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;
                    _sqladapter = new SqlDataAdapter();
                    _sqlcmd.Connection = _sqlcon;
                    _sqladapter.InsertCommand = _sqlcmd;
                    _sqladapter.InsertCommand.Parameters.Add(Schema.StudentList.PARA_IMPORT_ID, SqlDbType.VarChar, 100, Schema.StudentList.COL_IMPORT_ID);
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
            return entListStudentList;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntStudentList"></param>
        /// <returns></returns>
        public StudentList Get(StudentList pEntStudentList)
        {
            return GetStudentList(pEntStudentList);
        }
        /// <summary>
        /// Update StudentList
        /// </summary>
        /// <param name="pEntStudentList"></param>
        /// <returns></returns>
        public StudentList Update(StudentList pEntStudentList)
        {
            return EditStudentList(pEntStudentList);
        }
        #endregion
    }
}
