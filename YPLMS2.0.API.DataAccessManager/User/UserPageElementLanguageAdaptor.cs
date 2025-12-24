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
    public class UserPageElementLanguageAdaptor : IDataManager<UserPageElementLanguage>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        SqlDataAdapter _sqladapter = null;
        SqlConnection _sqlcon = null;
        DataTable _dtable = null;
        UserPageElementLanguage _entPageElement = null;
        List<UserPageElementLanguage> _entListElements = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.UserPageElement.BL_ERROR;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// Get Element By Id
        /// </summary>
        /// <param name="pEntElement"></param>
        /// <returns></returns>
        public UserPageElementLanguage GetElementById(UserPageElementLanguage pEntElement)
        {
            _entPageElement = new UserPageElementLanguage();
            _entListElements = new List<UserPageElementLanguage>();
            _entListElements = GetPageElementList(pEntElement);
            if (_entListElements.Count > 0)
                _entPageElement = _entListElements[0];
            return _entPageElement;
        }

        /// <summary>
        /// Get Page Element List
        /// </summary>
        /// <param name="pEntElement"></param>
        /// <returns></returns>
        public List<UserPageElementLanguage> GetPageElementList(UserPageElementLanguage pEntElement)
        {
            SqlConnection sqlConnection = null;
            _entListElements = new List<UserPageElementLanguage>();
            _entPageElement = new UserPageElementLanguage();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.UserPage.PROC_GET_ALL_PAGE_ELEMENTS;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntElement.ID))
            {
                _sqlpara = new SqlParameter(Schema.UserPage.PARA_PAGE_ID, pEntElement.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            if (!String.IsNullOrEmpty(pEntElement.PageElementId))
            {
                _sqlpara = new SqlParameter(Schema.UserPage.PARA_PAGE_ELEMENT_ID, pEntElement.PageElementId);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            if (!String.IsNullOrEmpty(pEntElement.LanguageID))
            {
                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntElement.LanguageID);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            if (pEntElement.ListRange != null)
            {
                if (!string.IsNullOrEmpty(pEntElement.ListRange.SortExpression))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntElement.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
            }
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntElement.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entPageElement = FillObject(_sqlreader, _sqlObject);
                    _entListElements.Add(_entPageElement);
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
            return _entListElements;
        }

        //GelAllMultipleElementsList
        public List<UserPageElementLanguage> GelAllMultipleElementsList(UserPageElementLanguage pEntElement)
        {
            SqlConnection sqlConnection = null;
            _entListElements = new List<UserPageElementLanguage>();
            _entPageElement = new UserPageElementLanguage();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.UserPage.PROC_GET_ALL_PAGE_ELEMENTS_LST_MUL;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntElement.ID))
            {
                _sqlpara = new SqlParameter(Schema.UserPage.PARA_PAGE_ID, pEntElement.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            if (!String.IsNullOrEmpty(pEntElement.PageElementId))
            {
                _sqlpara = new SqlParameter(Schema.UserPage.PARA_PAGE_ELEMENT_ID, pEntElement.PageElementId);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            if (!String.IsNullOrEmpty(pEntElement.LanguageID))
            {
                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntElement.LanguageID);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            if (pEntElement.ListRange != null)
            {
                if (!string.IsNullOrEmpty(pEntElement.ListRange.SortExpression))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntElement.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
            }
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntElement.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entPageElement = FillObject(_sqlreader, _sqlObject);
                    _entListElements.Add(_entPageElement);
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
            return _entListElements;
        }
        /// <summary>
        /// Get All Element List
        /// </summary>
        /// <param name="pEntElement"></param>
        /// <returns></returns>
        public List<UserPageElementLanguage> GetAllElementList(UserPageElementLanguage pEntElement)
        {
            SqlConnection sqlConnection = null;
            _entListElements = new List<UserPageElementLanguage>();
            _entPageElement = new UserPageElementLanguage();
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _sqlcmd.CommandText = Schema.UserPage.PROC_GET_ALL_ELEMENTS;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntElement.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entPageElement = FillObject(_sqlreader, _sqlObject);
                    _entListElements.Add(_entPageElement);
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
            return _entListElements;
        }


        /// <summary>
        /// Fill Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <param name="pSqlObject"></param>
        /// <returns></returns>
        private UserPageElementLanguage FillObject(SqlDataReader pReader, SQLObject pSqlObject)
        {
            UserPageElementLanguage entElement = new UserPageElementLanguage();
            int iIndex;
            if (pReader.HasRows)
            {
                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_PAGE_ID);
                if (!pReader.IsDBNull(iIndex))
                    entElement.ID = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_ELEMENT_IMAGE_FILE_NAME);
                if (!pReader.IsDBNull(iIndex))
                    entElement.ElementImageFileName = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_ELEMENT_NAME);
                if (!pReader.IsDBNull(iIndex))
                    entElement.ElementName = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_ELEMENT_TEXT);
                if (!pReader.IsDBNull(iIndex))
                    entElement.ElementText = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_IS_IMAGE_AVAIL);
                if (!pReader.IsDBNull(iIndex))
                    entElement.IsImageAvailable = pReader.GetBoolean(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Language.COL_LANGUAGE_ID);
                if (!pReader.IsDBNull(iIndex))
                    entElement.LanguageID = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_PAGE_ELEMENT_ID);
                if (!pReader.IsDBNull(iIndex))
                    entElement.PageElementId = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_ELEMENT_DISPLAY_NAME);
                if (!pReader.IsDBNull(iIndex))
                    entElement.ElementDisplayName = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_ELEMENT_TYPE);
                if (!pReader.IsDBNull(iIndex))
                    entElement.ElementType = (UserPageElementLanguage.UserPagesElementType)Enum.Parse(typeof(UserPageElementLanguage.UserPagesElementType), pReader.GetString(iIndex));

                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_IS_MANDATORY);
                if (!pReader.IsDBNull(iIndex))
                    entElement.IsMandatory = pReader.GetBoolean(iIndex);

                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_VALIDATION_ID);
                if (!pReader.IsDBNull(iIndex))
                    entElement.ValidationId = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_IS_READ_ONLY);
                if (!pReader.IsDBNull(iIndex))
                    entElement.IsReadOnly = pReader.GetBoolean(iIndex);


                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_ELEMENT_IMAGE_WIDTH);
                if (!pReader.IsDBNull(iIndex))
                    entElement.ImageWidth = pReader.GetInt32(iIndex);

                iIndex = pReader.GetOrdinal(Schema.UserPage.COL_ELEMENT_IMAGE_HEIGHT);
                if (!pReader.IsDBNull(iIndex))
                    entElement.ImageHeight = pReader.GetInt32(iIndex);


                if (pSqlObject.ReaderHasColumn(pReader, Schema.Common.COL_CREATED_BY))
                {
                    iIndex = pReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pReader.IsDBNull(iIndex))
                        entElement.CreatedById = pReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pReader, Schema.Common.COL_CREATED_ON))
                {
                    iIndex = pReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pReader.IsDBNull(iIndex))
                        entElement.DateCreated = pReader.GetDateTime(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pReader, Schema.Common.COL_MODIFIED_BY))
                {
                    iIndex = pReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pReader.IsDBNull(iIndex))
                        entElement.LastModifiedById = pReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pReader, Schema.Common.COL_MODIFIED_ON))
                {
                    iIndex = pReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pReader.IsDBNull(iIndex))
                        entElement.LastModifiedDate = pReader.GetDateTime(iIndex);
                }
            }
            return entElement;
        }

        /// <summary>
        /// Edit Element
        /// </summary>
        /// <param name="pEntElement"></param>
        /// <returns></returns>
        public UserPageElementLanguage EditElement(UserPageElementLanguage pEntElement)
        {
            _entPageElement = new UserPageElementLanguage();
            _entPageElement = Update(pEntElement, Schema.Common.VAL_UPDATE_MODE);
            return _entPageElement;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="pEntElement"></param>
        /// <param name="pStrUpdateMode"></param>
        /// <returns></returns>
        private UserPageElementLanguage Update(UserPageElementLanguage pEntElement, string pStrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.UserPage.PROC_UPDATE_PAGE_ELEMENTS;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntElement.ClientId);

                if (string.IsNullOrEmpty(pEntElement.PageElementId))
                {
                    pEntElement.PageElementId = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                }
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPage.PARA_PAGE_ID, pEntElement.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPage.PARA_PAGE_ELEMENT_ID, pEntElement.PageElementId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPage.PARA_ELEMENT_NAME, pEntElement.ElementName);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPage.PARA_ELEMENT_TEXT, pEntElement.ElementText);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPage.PARA_ELEMENT_IMAGE_FILE_NAME, pEntElement.ElementImageFileName);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPage.PARA_IS_IMAGE_AVAIL, pEntElement.IsImageAvailable);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntElement.LanguageID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPage.PARA_ELEMENT_DISPLAY_NAME, pEntElement.ElementDisplayName);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPage.PARA_ELEMENT_TYPE, pEntElement.ElementType.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPage.PARA_IS_MANDATORY, pEntElement.IsMandatory);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPage.PARA_IS_READ_ONLY, pEntElement.IsReadOnly);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPage.PARA_VALIDATION_ID, pEntElement.ValidationId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPage.PARA_ELEMENT_IMAGE_HEIGHT, pEntElement.ImageHeight);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPage.PARA_ELEMENT_IMAGE_WIDTH, pEntElement.ImageWidth);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPage.PARA_ELEMENT_IMAGE_OVERRIDE, pEntElement.IsImageoverride);
                _sqlcmd.Parameters.Add(_sqlpara);


                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntElement.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntElement;
        }

        /// <summary>
        /// Bulk Update
        /// </summary>
        /// <param name="pEntListElements"></param>
        /// <returns></returns>
        public List<UserPageElementLanguage> BulkUpdate(List<UserPageElementLanguage> pEntListElements)
        {
            _sqlObject = new SQLObject();
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            List<UserPageElementLanguage> entListElements = new List<UserPageElementLanguage>();
            int iBatchSize = 0;
            try
            {
                if (pEntListElements.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.UserPage.COL_PAGE_ELEMENT_ID);
                    _dtable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    _dtable.Columns.Add(Schema.UserPage.COL_ELEMENT_TEXT);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);

                    foreach (UserPageElementLanguage entElement in pEntListElements)
                    {
                        DataRow drow = _dtable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entElement.ClientId);

                        if (!String.IsNullOrEmpty(entElement.PageElementId))
                        {
                            drow[Schema.UserPage.COL_PAGE_ELEMENT_ID] = entElement.PageElementId;
                            drow[Schema.UserPage.COL_ELEMENT_TEXT] = entElement.ElementText;
                            drow[Schema.Language.COL_LANGUAGE_ID] = Convert.ToString(entElement.LanguageID);
                            drow[Schema.Common.COL_MODIFIED_BY] = entElement.LastModifiedById;
                            _dtable.Rows.Add(drow);
                            iBatchSize = iBatchSize + 1;
                            entListElements.Add(entElement);
                        }
                    }
                    if (_dtable.Rows.Count > 0)
                    {
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.UserPage.PROC_UPDATE_ELEMENTS;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.UserPage.PARA_PAGE_ELEMENT_ID, SqlDbType.VarChar, 100, Schema.UserPage.COL_PAGE_ELEMENT_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.NVarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.UserPage.PARA_ELEMENT_TEXT, SqlDbType.NVarChar, 2000, Schema.UserPage.COL_ELEMENT_TEXT);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.NVarChar, 100, Schema.Common.COL_MODIFIED_BY);
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
            return entListElements;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntUserPageElementLanguage"></param>
        /// <returns></returns>
        public UserPageElementLanguage Get(UserPageElementLanguage pEntUserPageElementLanguage)
        {
            return GetElementById(pEntUserPageElementLanguage);
        }
        /// <summary>
        /// Update UserPageElementLanguage
        /// </summary>
        /// <param name="pEntUserPageElementLanguage"></param>
        /// <returns></returns>
        public UserPageElementLanguage Update(UserPageElementLanguage pEntUserPageElementLanguage)
        {
            return EditElement(pEntUserPageElementLanguage);
        }
        #endregion
    }
}
