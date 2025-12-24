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
    /// <summary>
    /// class FeatureListAdaptor
    /// </summary>
    public class FeatureListAdaptor : IDataManager<FeatureList>
    {

        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        FeatureList _entFeatureList = null;
        List<FeatureList> _entListFeatureList = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.FeatureList.FEATURELIST_DAM_ERROR;
        #endregion
        /// <summary>
        /// Default constructor
        /// </summary>
        public FeatureListAdaptor()
        {
        }
        /// <summary>
        /// To get FeatureList details by ID.
        /// </summary>
        /// <param name="pEntFeatureList"></param>
        /// <returns>FeatureList</returns>
        private FeatureList GetFeatureListByID(FeatureList pEntFeatureList)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.FeatureList.PROC_GET_FEATURELIST;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntFeatureList.ID))
                _sqlpara = new SqlParameter(Schema.FeatureList.PARA_FEATUREID, pEntFeatureList.ID.ToString());
            else
                _sqlpara = new SqlParameter(Schema.FeatureList.PARA_FEATUREID, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntFeatureList.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entFeatureList = FillObject(_sqlreader, false, _sqlObject);
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
            return _entFeatureList;
        }

        /// <summary>
        /// FeatureList Range
        /// </summary>
        /// <param name="pEntFeatureList"></param>
        /// <returns></returns>
        private List<FeatureList> GetAllFeatureList(FeatureList pEntFeatureList)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.FeatureList.PROC_GET_ALL_FEATURELIST;
            _entListFeatureList = new List<FeatureList>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntFeatureList.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                if (pEntFeatureList.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntFeatureList.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntFeatureList.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntFeatureList.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entFeatureList = FillObject(_sqlreader, true, _sqlObject);
                    _entListFeatureList.Add(_entFeatureList);
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
            return _entListFeatureList;
        }

        /// <summary>
        /// Fill Object
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>FeatureList</returns>
        private FeatureList FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entFeatureList = new FeatureList();
            int index;
            if (pSqlReader.HasRows)
            {
                index = pSqlReader.GetOrdinal(Schema.FeatureList.COL_FEATUREID);
                if (!pSqlReader.IsDBNull(index))
                    _entFeatureList.ID = pSqlReader.GetString(index);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.FeatureList.COL_FEATURENAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.FeatureList.COL_FEATURENAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entFeatureList.FeatureName = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.FeatureList.COL_FEATUREDESCRIPTION))
                {
                    index = pSqlReader.GetOrdinal(Schema.FeatureList.COL_FEATUREDESCRIPTION);
                    if (!pSqlReader.IsDBNull(index))
                        _entFeatureList.FeatureDescription = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.FeatureList.COL_ISACTIVE))
                {
                    index = pSqlReader.GetOrdinal(Schema.FeatureList.COL_ISACTIVE);
                    if (!pSqlReader.IsDBNull(index))
                        _entFeatureList.IsActive = pSqlReader.GetBoolean(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.FeatureList.COL_FEATUREVALUE))
                {
                    index = pSqlReader.GetOrdinal(Schema.FeatureList.COL_FEATUREVALUE);
                    if (!pSqlReader.IsDBNull(index))
                        _entFeatureList.Value = pSqlReader.GetString(index);
                }

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(index))
                        _entRange.TotalRows = pSqlReader.GetInt32(index);
                    _entFeatureList.ListRange = _entRange;
                    return _entFeatureList;
                }
            }
            return _entFeatureList;
        }

        /// <summary>
        /// To add/update a FeatureList.
        /// </summary>
        /// <param name="pEntFeatureList"></param>
        /// <returns>FeatureList</returns>
        private FeatureList UpdateFeatureList(FeatureList pEntFeatureList, FeatureList.Method pMethod)
        {
            int iRowsAffected = 0;
            SqlConnection sqlConn = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.FeatureList.PROC_UPDATE_FEATURELIST;
            _sqlObject = new SQLObject();

            if (string.IsNullOrEmpty(pEntFeatureList.ID))
                pEntFeatureList.ID = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_FEATURELIST_ID_PREFIX, Schema.Common.VAL_FEATURELIST_ID_LENGTH);

            _sqlpara = new SqlParameter(Schema.FeatureList.PARA_FEATUREID, pEntFeatureList.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntFeatureList.FeatureName))
                _sqlpara = new SqlParameter(Schema.FeatureList.PARA_FEATURENAME, pEntFeatureList.FeatureName);
            else
                _sqlpara = new SqlParameter(Schema.FeatureList.PARA_FEATURENAME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntFeatureList.FeatureDescription))
                _sqlpara = new SqlParameter(Schema.FeatureList.PARA_FEATUREDESCRIPTION, pEntFeatureList.FeatureDescription);
            else
                _sqlpara = new SqlParameter(Schema.FeatureList.PARA_FEATUREDESCRIPTION, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.FeatureList.PARA_ISACTIVE, pEntFeatureList.IsActive);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                if (pMethod == FeatureList.Method.Update)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else if (pMethod == FeatureList.Method.Add)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _strConnString = _sqlObject.GetClientDBConnString(pEntFeatureList.ClientId);
                sqlConn = new SqlConnection(_strConnString);
                sqlConn.Open();
                _sqlcmd.Connection = sqlConn;
                iRowsAffected = _sqlObject.ExecuteNonQueryCount(_sqlcmd);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlConn.State != ConnectionState.Closed)
                    sqlConn.Close();
            }
            return pEntFeatureList;
        }

        #region Interface Methods
        /// <summary>
        /// Get FeatureList By ID
        /// </summary>
        /// <param name="pEntFeatureList"></param>
        /// <returns></returns>
        public FeatureList Get(FeatureList pEntFeatureList)
        {
            return GetFeatureListByID(pEntFeatureList);
        }
        /// <summary>
        /// List of All FeatureList
        /// </summary>
        /// <param name="pEntFeatureList"></param>
        /// <returns></returns>
        public List<FeatureList> GetAll(FeatureList pEntFeatureList)
        {
            return GetAllFeatureList(pEntFeatureList);
        }
        /// <summary>
        /// Add FeatureList
        /// </summary>
        /// <param name="pEntFeatureList"></param>
        /// <returns></returns>
        public FeatureList Add(FeatureList pEntFeatureList)
        {
            return UpdateFeatureList(pEntFeatureList, FeatureList.Method.Add);
        }
        /// <summary>
        /// Update FeatureList
        /// </summary>
        /// <param name="pEntFeatureList"></param>
        /// <returns></returns>
        public FeatureList Update(FeatureList pEntFeatureList)
        {
            return UpdateFeatureList(pEntFeatureList, FeatureList.Method.Update);
        }
        /// <summary>
        /// Delete FeatureList
        /// </summary>
        /// <param name="pEntFeatureList"></param>
        /// <returns>FeatureList</returns>
        public FeatureList Delete(FeatureList pEntFeatureList)
        {
            return null;
        }
        #endregion
    }
}
