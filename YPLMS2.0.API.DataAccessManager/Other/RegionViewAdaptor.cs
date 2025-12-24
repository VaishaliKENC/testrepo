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
    public class RegionViewAdaptor : IDataManager<RegionView>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        RegionView _EntRegionView = null;
        List<RegionView> _entListRegionView = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.RegionView.REGION_VIEW_ERROR;
        #endregion

        /// <summary>
        /// Get Region View By Id
        /// </summary>
        /// <param name="pEntRegionView"></param>
        /// <returns></returns>
        public RegionView GetRegionViewById(RegionView pEntRegionView)
        {
            _sqlObject = new SQLObject();
            _EntRegionView = new RegionView();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntRegionView.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.RegionView.PROC_GET_REGION_VIEW, sqlConnection);
                _sqlpara = new SqlParameter(Schema.RegionView.PARA_REGION_VIEW_ID, pEntRegionView.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _EntRegionView = FillObject(_sqlreader, _sqlObject);
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
            return _EntRegionView;
        }

        /// <summary>
        /// Get Region View By Id
        /// </summary>
        /// <param name="pEntRegionView"></param>
        /// <returns></returns>
        public RegionView GetRegionViewNameById(RegionView pEntRegionView)
        {
            _sqlObject = new SQLObject();
            _EntRegionView = new RegionView();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntRegionView.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.RegionView.PROC_GET_REGION_VIEW, sqlConnection);
                _sqlpara = new SqlParameter(Schema.RegionView.PARA_REGION_VIEW_ID, pEntRegionView.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                Object obj = null;
                obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
                if (obj != null)
                {
                    _EntRegionView.RegionViewName = Convert.ToString(obj);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return _EntRegionView;
        }

        /// <summary>
        /// to get by region  view name
        /// </summary>
        /// <param name="pEntRegionView"></param>
        /// <returns></returns>
        public RegionView GetRegionViewByName(RegionView pEntRegionView)
        {
            _sqlObject = new SQLObject();
            _EntRegionView = new RegionView();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntRegionView.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.RegionView.PROC_GET_REGION_VIEW_BY_NAME, sqlConnection);
                _sqlpara = new SqlParameter(Schema.RegionView.PARA_REGION_VIEW_NAME, pEntRegionView.RegionViewName);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _EntRegionView = FillObject(_sqlreader, _sqlObject);
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
            return _EntRegionView;
        }

        /// <summary>
        /// Get All Region View
        /// </summary>
        /// <param name="pEntRegionView"></param>
        /// <returns></returns>
        public List<RegionView> GetAllRegionView(RegionView pEntRegionView)
        {
            _sqlObject = new SQLObject();
            _EntRegionView = new RegionView();
            _entListRegionView = new List<RegionView>();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntRegionView.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.RegionView.PROC_GET_ALL_REGION_VIEW, sqlConnection);
                if (pEntRegionView.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntRegionView.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntRegionView.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntRegionView.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _EntRegionView = FillObject(_sqlreader, _sqlObject);
                    _entListRegionView.Add(_EntRegionView);
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
            return _entListRegionView;
        }

        /// <summary>
        /// Find Region View
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<RegionView> FindRegionView(Search pEntSearch)
        {
            _sqlObject = new SQLObject();
            _EntRegionView = new RegionView();
            RegionView entRegionView = new RegionView();
            _entListRegionView = new List<RegionView>();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.RegionView.PROC_FIND_REGION_VIEW, sqlConnection);

                if (!string.IsNullOrEmpty(pEntSearch.KeyWord))
                    _sqlcmd.Parameters.AddWithValue(Schema.Common.PARA_KEY_WORD, pEntSearch.KeyWord);

                if (pEntSearch.SearchObject != null && pEntSearch.SearchObject.Count > 0)
                {
                    entRegionView = new RegionView();
                    entRegionView = (RegionView)pEntSearch.SearchObject[0];

                    if (!string.IsNullOrEmpty(entRegionView.RuleId))
                    {
                        _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_ID, entRegionView.RuleId);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                if (pEntSearch.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntSearch.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntSearch.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _EntRegionView = FillObject(_sqlreader, _sqlObject);
                    _entListRegionView.Add(_EntRegionView);
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
            return _entListRegionView;
        }

        /// <summary>
        /// To fill Region View object data from reader.
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <returns>Region View object</returns>
        private RegionView FillObject(SqlDataReader pSqlreader, SQLObject pSqlObject)
        {
            RegionView entRegionView = new RegionView();
            int index;

            if (pSqlreader.HasRows)
            {
                index = pSqlreader.GetOrdinal(Schema.RegionView.COL_REGION_VIEW_ID);
                if (!pSqlreader.IsDBNull(index))
                    entRegionView.ID = pSqlreader.GetString(index);

                index = pSqlreader.GetOrdinal(Schema.RegionView.COL_REGION_VIEW_NAME);
                if (!pSqlreader.IsDBNull(index))
                    entRegionView.RegionViewName = pSqlreader.GetString(index);

                index = pSqlreader.GetOrdinal(Schema.RegionView.COL_REGION_VIEW_DESC);
                if (!pSqlreader.IsDBNull(index))
                    entRegionView.RegionViewDescription = pSqlreader.GetString(index);

                index = pSqlreader.GetOrdinal(Schema.RegionView.COL_RULE_ID);
                if (!pSqlreader.IsDBNull(index))
                    entRegionView.RuleId = pSqlreader.GetString(index);

                index = pSqlreader.GetOrdinal(Schema.RegionView.COL_CLIENT_ID);
                if (!pSqlreader.IsDBNull(index))
                    entRegionView.ClientId = pSqlreader.GetString(index);

                index = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pSqlreader.IsDBNull(index))
                    entRegionView.CreatedById = pSqlreader.GetString(index);
                else
                    entRegionView.CreatedById = "";

                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Common.COL_CREATED_BY_NAME))
                {
                    index = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                    if (!pSqlreader.IsDBNull(index))
                        entRegionView.CreatedByName = pSqlreader.GetString(index);
                    else
                        entRegionView.CreatedByName = "";
                }

                index = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                if (!pSqlreader.IsDBNull(index))
                    entRegionView.DateCreated = pSqlreader.GetDateTime(index);

                index = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                if (!pSqlreader.IsDBNull(index))
                    entRegionView.LastModifiedById = pSqlreader.GetString(index);
                else
                    entRegionView.LastModifiedById = "";

                index = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                if (!pSqlreader.IsDBNull(index))
                    entRegionView.LastModifiedDate = pSqlreader.GetDateTime(index);


                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Common.COL_TOTAL_COUNT))
                {
                    index = pSqlreader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlreader.IsDBNull(index))
                        if (pSqlreader.GetInt32(index) > 0)
                        {
                            _entRange = new EntityRange();
                            _entRange.TotalRows = pSqlreader.GetInt32(index);
                            entRegionView.ListRange = _entRange;
                        }

                }




                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Common.COL_IS_USED))
                {
                    index = pSqlreader.GetOrdinal(Schema.Common.COL_IS_USED);
                    if (!pSqlreader.IsDBNull(index))
                        entRegionView.IsUsed = pSqlreader.GetBoolean(index);
                }
            }
            return entRegionView;
        }

        /// <summary>
        /// Add Menu Item
        /// </summary>
        /// <param name="pEntRegionView"></param>
        /// <returns></returns>
        public RegionView AddRegionView(RegionView pEntRegionView)
        {
            _EntRegionView = new RegionView();
            _EntRegionView = Update(pEntRegionView, Schema.Common.VAL_INSERT_MODE);
            return _EntRegionView;
        }

        /// <summary>
        /// Update Menu Item
        /// </summary>
        /// <param name="pEntRegionView"></param>
        /// <returns></returns>
        public RegionView EditRegionView(RegionView pEntRegionView)
        {
            _EntRegionView = new RegionView();
            _EntRegionView = Update(pEntRegionView, Schema.Common.VAL_UPDATE_MODE);
            return _EntRegionView;
        }

        /// <summary>
        /// Delete Region View
        /// </summary>
        /// <param name="pEntRegionView"></param>
        /// <returns></returns>
        public RegionView DeleteRegionView(RegionView pEntRegionView)
        {
            int iRowsAffected = -1;
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.RegionView.PROC_DELETE_REGION_VIEW;
            if (!String.IsNullOrEmpty(pEntRegionView.ID))
                _sqlpara = new SqlParameter(Schema.RegionView.PARA_REGION_VIEW_ID, pEntRegionView.ID);
            else
                _sqlpara = new SqlParameter(Schema.RegionView.PARA_REGION_VIEW_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntRegionView.ClientId);
                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                if (iRowsAffected > 0)
                    pEntRegionView = null;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntRegionView;
        }

        /// <summary>
        /// Update Menu Item
        /// </summary>
        /// <param name="pEntRegionView"></param>
        /// <param name="pStrUpdateMode"></param>
        /// <returns></returns>
        private RegionView Update(RegionView pEntRegionView, string pStrUpdateMode)
        {
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlObject = new SQLObject();
            _sqlcmd.CommandText = Schema.RegionView.PROC_UPDATE_REGION_VIEW;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntRegionView.ClientId);
                if (pStrUpdateMode == Schema.Common.VAL_INSERT_MODE)
                {
                    pEntRegionView.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                }
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.RegionView.PARA_REGION_VIEW_ID, pEntRegionView.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntRegionView.RegionViewName))
                    _sqlpara = new SqlParameter(Schema.RegionView.PARA_REGION_VIEW_NAME, pEntRegionView.RegionViewName);
                else
                    _sqlpara = new SqlParameter(Schema.RegionView.PARA_REGION_VIEW_NAME, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntRegionView.RegionViewDescription))
                    _sqlpara = new SqlParameter(Schema.RegionView.PARA_REGION_VIEW_DESC, pEntRegionView.RegionViewDescription);
                else
                    _sqlpara = new SqlParameter(Schema.RegionView.PARA_REGION_VIEW_DESC, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntRegionView.RuleId))
                    _sqlpara = new SqlParameter(Schema.RegionView.PARA_RULE_ID, pEntRegionView.RuleId);
                else
                    _sqlpara = new SqlParameter(Schema.RegionView.PARA_RULE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntRegionView.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntRegionView.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntRegionView;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntRegionView"></param>
        /// <returns></returns>
        public RegionView Get(RegionView pEntRegionView)
        {
            return GetRegionViewById(pEntRegionView);
        }
        /// <summary>
        /// Update RegionView
        /// </summary>
        /// <param name="pEntRegionView"></param>
        /// <returns></returns>
        public RegionView Update(RegionView pEntRegionView)
        {
            return EditRegionView(pEntRegionView);
        }
        #endregion
    }
}
