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
    public class LookupAdaptor : IDataManager<Lookup>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        SqlDataReader _sqlreader = null;
        Lookup _entLookup = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.Lookup.LOOK_UP_ERROR;
        #endregion

        /// <summary>
        /// Get Lookups details By Type
        /// </summary>
        /// <param name="pEntLookup"></param>
        /// <returns>List of Lookup objects</returns>
        public List<Lookup> GetLookupsByType(Lookup pEntLookup)
        {
            Lookup entLookUp = null;
            List<Lookup> entListLookUps;
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLookup.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Lookup.PROC_GET_LOOKUPS, sqlConnection);
                _sqlpara = new SqlParameter(Schema.Lookup.PARA_LOOKUP_TYPE, pEntLookup.LookupType.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Lookup.PARA_IS_Curriculum, pEntLookup.IsCurriculum);
                _sqlcmd.Parameters.Add(_sqlpara);
                if (!string.IsNullOrEmpty(pEntLookup.LanguageId))
                {
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntLookup.LanguageId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntLookup.ListRange != null)
                {
                    if (!string.IsNullOrEmpty(pEntLookup.ListRange.SortExpression))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntLookup.ListRange.SortExpression);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                entListLookUps = new List<Lookup>();
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entLookUp = FillObject(_sqlreader);
                    entListLookUps.Add(entLookUp);
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
            return entListLookUps;
        }

        /// <summary>
        /// Get Lookups details By Type
        /// </summary>
        /// <param name="pEntLookup"></param>
        /// <returns>List of Lookup objects</returns>
        public List<Lookup> GetStatusAndTypeByLookupType(Lookup pEntLookup)
        {
            Lookup entLookUp = null;
            List<Lookup> entListLookUps;
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLookup.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Lookup.PROC_GET_LOOKUPTEXT, sqlConnection);
                _sqlpara = new SqlParameter(Schema.Lookup.PARA_LOOKUP_TYPE, pEntLookup.LookupText.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);
                if (!string.IsNullOrEmpty(pEntLookup.LanguageId))
                {
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntLookup.LanguageId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntLookup.ListRange != null)
                {
                    if (!string.IsNullOrEmpty(pEntLookup.ListRange.SortExpression))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntLookup.ListRange.SortExpression);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                entListLookUps = new List<Lookup>();
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entLookUp = FillObject(_sqlreader);
                    entListLookUps.Add(entLookUp);
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
            return entListLookUps;
        }

        /// <summary>
        /// Get Lookup Type details 
        /// </summary>
        /// <param name="pLookTypeText"></param>
        /// <returns>Lookup Type</returns>
        private string GetLookupType(string pLookTypeText)
        {
            string lookUpTypeId = string.Empty;
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Lookup.PROC_GET_LOOKUP_TYPE;
            _sqlpara = new SqlParameter(Schema.Lookup.PARA_LOOKUP_TYPE, pLookTypeText);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    lookUpTypeId = _sqlreader[Schema.Lookup.COL_LOOKUP_TYPE_ID].ToString();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return lookUpTypeId;
        }

        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>Lookup object</returns>
        private Lookup FillObject(SqlDataReader pReader)
        {
            _entLookup = new Lookup();
            int iIndex;
            if (pReader.HasRows)
            {
                iIndex = pReader.GetOrdinal(Schema.Lookup.COL_LOOKUP_ID);
                if (!pReader.IsDBNull(iIndex))
                    _entLookup.ID = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Lookup.COL_LOOKUP_TEXT);
                if (!pReader.IsDBNull(iIndex))
                    _entLookup.LookupText = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Lookup.COL_LOOKUP_VALUE);
                if (!pReader.IsDBNull(iIndex))
                    _entLookup.LookupValue = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Lookup.COL_IS_DEFAULT);
                if (!pReader.IsDBNull(iIndex))
                    _entLookup.IsDefault = pReader.GetBoolean(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Lookup.COL_LOOKUP_TYPE);
                if (!pReader.IsDBNull(iIndex))
                    _entLookup.LookupType = (LookupType)Enum.Parse(typeof(LookupType), pReader.GetString(iIndex));

                iIndex = pReader.GetOrdinal(Schema.Lookup.COL_LOOKUP_TYPE_ID);
                if (!pReader.IsDBNull(iIndex))
                    _entLookup.LookupTypeId = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Language.COL_LANGUAGE_ID);
                if (!pReader.IsDBNull(iIndex))
                    _entLookup.LanguageId = pReader.GetString(iIndex);

                if (SQLHelper.ReaderHasColumn(pReader, Schema.Common.COL_CREATED_BY))
                {

                    iIndex = pReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pReader.IsDBNull(iIndex))
                        _entLookup.CreatedById = pReader.GetString(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pReader, Schema.Common.COL_CREATED_ON))
                {

                    iIndex = pReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pReader.IsDBNull(iIndex))
                        _entLookup.DateCreated = pReader.GetDateTime(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pReader, Schema.Common.COL_MODIFIED_BY))
                {

                    iIndex = pReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pReader.IsDBNull(iIndex))
                        _entLookup.LastModifiedById = pReader.GetString(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pReader, Schema.Common.COL_MODIFIED_ON))
                {

                    iIndex = pReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pReader.IsDBNull(iIndex))
                        _entLookup.LastModifiedDate = pReader.GetDateTime(iIndex);
                }
            }
            return _entLookup;
        }

        /// <summary>
        /// Fill Reader Object For Single 
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>Lookup object</returns>
        private Lookup FillObjectForSingle(SqlDataReader pReader)
        {
            _entLookup = new Lookup();
            int iIndex;
            if (pReader.HasRows)
            {
                while (pReader.Read())
                {
                    iIndex = pReader.GetOrdinal(Schema.Lookup.COL_LOOKUP_ID);
                    if (!pReader.IsDBNull(iIndex))
                        _entLookup.ID = pReader.GetString(iIndex);

                    iIndex = pReader.GetOrdinal(Schema.Lookup.COL_LOOKUP_TEXT);
                    if (!pReader.IsDBNull(iIndex))
                        _entLookup.LookupText = pReader.GetString(iIndex);

                    iIndex = pReader.GetOrdinal(Schema.Lookup.COL_LOOKUP_VALUE);
                    if (!pReader.IsDBNull(iIndex))
                        _entLookup.LookupValue = pReader.GetString(iIndex);

                    iIndex = pReader.GetOrdinal(Schema.Lookup.COL_IS_DEFAULT);
                    if (!pReader.IsDBNull(iIndex))
                        _entLookup.IsDefault = pReader.GetBoolean(iIndex);

                    iIndex = pReader.GetOrdinal(Schema.Lookup.COL_LOOKUP_TYPE);
                    if (!pReader.IsDBNull(iIndex))
                        _entLookup.LookupType = (LookupType)Enum.Parse(typeof(LookupType), pReader.GetString(iIndex));

                    iIndex = pReader.GetOrdinal(Schema.Lookup.COL_LOOKUP_TYPE_ID);
                    if (!pReader.IsDBNull(iIndex))
                        _entLookup.LookupTypeId = pReader.GetString(iIndex);

                    iIndex = pReader.GetOrdinal(Schema.Language.COL_LANGUAGE_ID);
                    if (!pReader.IsDBNull(iIndex))
                        _entLookup.LanguageId = pReader.GetString(iIndex);

                    iIndex = pReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pReader.IsDBNull(iIndex))
                        _entLookup.CreatedById = pReader.GetString(iIndex);

                    iIndex = pReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pReader.IsDBNull(iIndex))
                        _entLookup.DateCreated = pReader.GetDateTime(iIndex);

                    iIndex = pReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pReader.IsDBNull(iIndex))
                        _entLookup.LastModifiedById = pReader.GetString(iIndex);

                    iIndex = pReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pReader.IsDBNull(iIndex))
                        _entLookup.LastModifiedDate = pReader.GetDateTime(iIndex);
                }
            }
            return _entLookup;
        }

        /// <summary>
        /// Edit Lookup
        /// </summary>
        /// <param name="pEntLookup"></param>
        /// <returns>Lookup object</returns>
        public Lookup EditLookup(Lookup pEntLookup)
        {
            _entLookup = new Lookup();
            _entLookup = Update(pEntLookup, Schema.Common.VAL_UPDATE_MODE);
            return _entLookup;
        }

        /// <summary>
        /// Get Lookup By ID
        /// </summary>
        /// <param name="pEntLookup"></param>
        /// <returns>Lookup object</returns>
        public Lookup GetLookupByID(Lookup pEntLookup)
        {
            _sqlObject = new SQLObject();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLookup.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Lookup.PROC_GET_LOOKUP, sqlConnection);
                if (!String.IsNullOrEmpty(pEntLookup.ID))
                    _sqlpara = new SqlParameter(Schema.Lookup.PARA_LOOKUP_ID, pEntLookup.ID.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.Lookup.PARA_LOOKUP_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _entLookup = FillObjectForSingle(_sqlreader);
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
            return _entLookup;
        }

        /// <summary>
        /// Update Lookup
        /// </summary>
        /// <param name="pEntLookup"></param>
        /// <param name="pUpdateMode"></param>
        /// <returns>updated Lookup object</returns>
        private Lookup Update(Lookup pEntLookup, string pUpdateMode)
        {
            int iRows = 0;
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Lookup.PROC_UPDATE_LOOKUP;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLookup.ClientId);
                if (pUpdateMode == Schema.Common.VAL_INSERT_MODE)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                    pEntLookup.ID = YPLMS.Services.IDGenerator.GetStringGUID();
                }
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Lookup.PARA_LOOKUP_ID, pEntLookup.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Lookup.PARA_LOOKUP_TEXT, pEntLookup.LookupText);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Lookup.PARA_LOOKUP_VALUE, pEntLookup.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Lookup.PARA_IS_DEFAULT, pEntLookup.IsDefault);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Lookup.PARA_LOOKUP_TYPE_ID, GetLookupType(pEntLookup.LookupType.ToString()));
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.COL_MODIFIED_BY, pEntLookup.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.COL_CREATED_BY, pEntLookup.CreatedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Lookup.COL_LOOKUP_LANGUAGEID, pEntLookup.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntLookup;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntLookup"></param>
        /// <returns></returns>
        public Lookup Get(Lookup pEntLookup)
        {
            return GetLookupByID(pEntLookup);
        }
        /// <summary>
        /// Update Lookup
        /// </summary>
        /// <param name="pEntLookup"></param>
        /// <returns></returns>
        public Lookup Update(Lookup pEntLookup)
        {
            return EditLookup(pEntLookup);
        }
        #endregion
    }
}
