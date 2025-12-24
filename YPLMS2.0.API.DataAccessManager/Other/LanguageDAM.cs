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
    public class LanguageDAM : IDataManager<Language>, ILanguageDAM<Language>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        SqlDataReader _sqlreader = null;
        Language _entLanguage = null;
        List<Language> _entListLanguage = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.Language.LANGUAGE_ERROR;
        #endregion

        /// <summary>
        /// Get Language By ID
        /// </summary>
        /// <param name="pEntLanguage"></param>
        /// <returns>Language object</returns>
        public Language GetLanguageByID(Language pEntLanguage)
        {
            _sqlObject = new SQLObject();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLanguage.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Language.PROC_GET_LANGUAGE, sqlConnection);
                if (!String.IsNullOrEmpty(pEntLanguage.ID))
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntLanguage.ID.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLanguage = FillObject(_sqlreader);
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
            return _entLanguage;
        }

        /// <summary>
        /// Get Master Languages
        /// </summary>
        /// <returns>list of Language objects from Master database</returns>
        public List<Language> GetMasterLanguages()
        {
            _sqlObject = new SQLObject();
            List<Language> entListlanguagesAll;
            SqlConnection sqlMasterConnection = new SqlConnection(_sqlObject.GetMasterDBConnString());
            _sqlcmd = new SqlCommand(Schema.Language.PROC_GET_ALL_MASTER_LANGS, sqlMasterConnection);
            try
            {
                _sqlreader = _sqlObject.ExecuteMasterReader(_sqlcmd, false);
                entListlanguagesAll = new List<Language>();
                while (_sqlreader.Read())
                {
                    _entLanguage = FillObject(_sqlreader);
                    entListlanguagesAll.Add(_entLanguage);
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
                if (sqlMasterConnection != null && sqlMasterConnection.State != ConnectionState.Closed)
                    sqlMasterConnection.Close();

            }
            return entListlanguagesAll;
        }

        /// <summary>
        /// Get Master Languages By ID
        /// </summary>
        /// <param name="pEntLanguage"></param>
        /// <returns>Language object from master database</returns>
        public Language GetMasterLanguageByID(Language pEntLanguage)
        {
            _sqlObject = new SQLObject();
            _entLanguage = new Language();
            SqlConnection sqlMasterConnection = new SqlConnection(_sqlObject.GetMasterDBConnString());
            _sqlcmd = new SqlCommand(Schema.Language.PROC_GET_LANGUAGE, sqlMasterConnection);
            try
            {
                _sqlcmd.Parameters.AddWithValue(Schema.Language.PARA_LANGUAGE_ID, pEntLanguage.ID);
                _sqlreader = _sqlObject.ExecuteMasterReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entLanguage = FillObject(_sqlreader);
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
                if (sqlMasterConnection != null && sqlMasterConnection.State != ConnectionState.Closed)
                    sqlMasterConnection.Close();
            }
            return _entLanguage;
        }

        /// <summary>
        /// Get Client Languages
        /// </summary>
        /// <param name="pLanguage"></param>
        /// <returns>List of Language objects from client database</returns>
        public List<Language> GetClientLanguages(Language pLanguage)
        {
            _sqlObject = new SQLObject();
            List<Language> entListLanguagesAll = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pLanguage.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Language.PROC_GET_ALL_CLIENT_LANGS, sqlConnection);
                if (!String.IsNullOrEmpty(pLanguage.ClientId))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pLanguage.ClientId);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListLanguagesAll = new List<Language>();
                while (_sqlreader.Read())
                {
                    _entLanguage = FillObject(_sqlreader);
                    entListLanguagesAll.Add(_entLanguage);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListLanguagesAll;
        }

        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>Language object</returns>
        private Language FillObject(SqlDataReader pReader)
        {
            _entLanguage = new Language();
            int iIndex;
            if (pReader.HasRows)
            {
                iIndex = pReader.GetOrdinal(Schema.Language.COL_LANGUAGE_ID);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.ID = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Language.COL_LANG_ENG_NAME);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.LanguageEnglishName = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Language.COL_LANG_NAME);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.LanguageName = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Language.COL_CHAR_SET_TYPE);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.CharacterSetType = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Language.COL_TEXT_DIRECTION);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.TextDirection = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.ClientId = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.CreatedById = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.DateCreated = pReader.GetDateTime(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.LastModifiedById = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.LastModifiedDate = pReader.GetDateTime(iIndex);
            }
            return _entLanguage;
        }

        /// <summary>
        /// Below method is not in use.
        /// </summary>
        /// <param name="pEntListLanguage"></param>
        /// <returns>List of newly added Language objects</returns>
        public List<Language> AddSelectedClientLanguages(List<Language> pEntListLanguage)
        {
            _sqlObject = new SQLObject();
            _entListLanguage = new List<Language>();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable tblData = new DataTable();
            SqlConnection con;
            int batchSize = 0;
            try
            {
                if (pEntListLanguage.Count > 0)
                {
                    tblData = new DataTable();
                    tblData.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    tblData.Columns.Add(Schema.Client.COL_CLIENT_ID);
                    foreach (Language objEntity in pEntListLanguage)
                    {
                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(objEntity.ClientId);
                        DataRow row = tblData.NewRow();
                        row[Schema.Language.COL_LANGUAGE_ID] = objEntity.ID;
                        row[Schema.Client.COL_CLIENT_ID] = objEntity.ClientId;
                        tblData.Rows.Add(row);
                        batchSize = batchSize + 1;
                    }
                    if (tblData.Rows.Count > 0)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = Schema.Language.PROC_INSERT_SELECTED_CLIENT_LANG;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        con = new SqlConnection(_strConnString);
                        cmd.Connection = con;
                        adapter.InsertCommand = cmd;
                        adapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                        adapter.InsertCommand.Parameters.Add(Schema.Client.PARA_CLIENT_ID, SqlDbType.VarChar, 100, Schema.Client.COL_CLIENT_ID);
                        adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        adapter.UpdateBatchSize = batchSize;
                        adapter.Update(tblData);
                        adapter.Dispose();
                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return _entListLanguage;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntLanguage"></param>
        /// <returns></returns>
        public Language Get(Language pEntLanguage)
        {
            return GetLanguageByID(pEntLanguage);
        }
        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="pEntLanguage"></param>
        /// <returns>null</returns>
        public Language Update(Language pEntLanguage)
        {
            return null;
        }
        #endregion
    }
}
