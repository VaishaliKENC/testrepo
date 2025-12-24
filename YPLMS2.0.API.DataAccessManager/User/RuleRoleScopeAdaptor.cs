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
    public class RuleRoleScopeAdaptor : IDataManager<RuleRoleScope>, IRuleRoleScopeAdaptor<RuleRoleScope>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        RuleRoleScope _entRole = null;
        EntityRange entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.RuleRoleScope.ERROR_MSG_ID;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public RuleRoleScopeAdaptor()
        { }

        /// <summary>
        /// Get All RuleRoleScope
        /// </summary>
        /// <param name="pEntRole"></param>
        /// <returns></returns>
        public List<RuleRoleScope> GetRuleRoleScopeList(RuleRoleScope pEntRole)
        {
            List<RuleRoleScope> entListRole = new List<RuleRoleScope>();
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntRole.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.Connection = sqlConnection;
                if (!String.IsNullOrEmpty(pEntRole.RoleId))
                {
                    if (pEntRole.RoleId != null)
                        _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_ROLE_ID, pEntRole.RoleId);
                    else
                        _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_ROLE_ID, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntRole.RuleId != null)
                        _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_RULE_ID, pEntRole.RuleId);
                    else
                        _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_RULE_ID, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlcmd.CommandText = Schema.RuleRoleScope.PROC_GET_RULE_ROLE_SCOPE;
                }
                else
                {
                    _sqlcmd.CommandText = Schema.RuleRoleScope.PROC_GET_ALL_RULE_ROLE_SCOPE;
                    if (pEntRole.ListRange != null)
                    {
                        if (pEntRole.ListRange != null)
                            _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntRole.ListRange.PageIndex);
                        else
                            _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        if (pEntRole.ListRange != null)
                            _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntRole.ListRange.PageSize);
                        else
                            _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        if (pEntRole.ListRange.SortExpression != null)
                            _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntRole.ListRange.SortExpression);
                        else
                            _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entRole = FillObject(_sqlreader, _sqlObject);
                    entListRole.Add(_entRole);
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
            return entListRole;
        }

        /// <summary>
        /// Fill RuleRoleScope Object
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns></returns>
        private RuleRoleScope FillObject(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            _entRole = new RuleRoleScope();
            int index;

            index = pSqlReader.GetOrdinal(Schema.RuleRoleScope.COL_IS_DEFAULT_SCOPE);
            if (!pSqlReader.IsDBNull(index))
                _entRole.IsDefaultScope = pSqlReader.GetBoolean(index);

            index = pSqlReader.GetOrdinal(Schema.RuleRoleScope.COL_ROLE_ID);
            if (!pSqlReader.IsDBNull(index))
                _entRole.RoleId = pSqlReader.GetString(index);

            index = pSqlReader.GetOrdinal(Schema.RuleRoleScope.COL_RULE_ID);
            if (!pSqlReader.IsDBNull(index))
                _entRole.RuleId = pSqlReader.GetString(index);

            index = pSqlReader.GetOrdinal(Schema.RuleRoleScope.COL_SCOPE_RULE_ID);
            if (!pSqlReader.IsDBNull(index))
                _entRole.ScopeRuleId = pSqlReader.GetString(index);

            index = pSqlReader.GetOrdinal(Schema.RuleRoleScope.COL_SCOPELEVEL_ID);
            if (!pSqlReader.IsDBNull(index))
                _entRole.ScopeLevelId = pSqlReader.GetString(index);

            index = pSqlReader.GetOrdinal(Schema.RuleRoleScope.COL_SCOPEUNIT_ID);
            if (!pSqlReader.IsDBNull(index))
                _entRole.ScopeUnitId = pSqlReader.GetString(index);

            index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
            if (!pSqlReader.IsDBNull(index))
                _entRole.CreatedById = pSqlReader.GetString(index);

            index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
            if (!pSqlReader.IsDBNull(index))
                _entRole.DateCreated = pSqlReader.GetSqlDateTime(index).Value;

            index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
            if (!pSqlReader.IsDBNull(index))
                _entRole.LastModifiedById = pSqlReader.GetString(index);

            index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
            if (!pSqlReader.IsDBNull(index))
                _entRole.LastModifiedDate = pSqlReader.GetSqlDateTime(index).Value;

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.GroupRule.COL_RULE_NAME))
            {
                index = pSqlReader.GetOrdinal(Schema.GroupRule.COL_RULE_NAME);
                if (!pSqlReader.IsDBNull(index))
                    _entRole.RuleName = pSqlReader.GetString(index);
            }
            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY_NAME))
            {
                index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                if (!pSqlReader.IsDBNull(index))
                    _entRole.CreatedByName = pSqlReader.GetString(index);
            }
            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY_NAME))
            {
                index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY_NAME);
                if (!pSqlReader.IsDBNull(index))
                    _entRole.LastModifiedByName = pSqlReader.GetString(index);
            }

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
            {
                index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                if (!pSqlReader.IsDBNull(index))
                {
                    if (pSqlReader.GetInt32(index) > 0)
                    {

                        entRange = new EntityRange();
                        entRange.TotalRows = pSqlReader.GetInt32(index);
                        _entRole.ListRange = entRange;

                    }
                }
            }

            return _entRole;
        }

        /// <summary>
        /// Get RuleRoleScope by RoleId and RuleId
        /// </summary>
        /// <param name="pEntRole"></param>
        /// <returns></returns>
        public RuleRoleScope GetRuleRoleByID(RuleRoleScope pEntRole)
        {
            List<RuleRoleScope> entListRole = new List<RuleRoleScope>();
            try
            {
                entListRole = GetRuleRoleScopeList(pEntRole);
                if (entListRole != null && entListRole.Count > 0)
                    _entRole = entListRole[0];
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return _entRole;
        }


        /// <summary>
        /// Update RuleRoleScope
        /// </summary>
        /// <param name="pEntRole"></param>
        /// <returns></returns>
        public RuleRoleScope EditRuleRoleScope(RuleRoleScope pEntRole)
        {
            _entRole = new RuleRoleScope();
            _entRole = Update(pEntRole, Schema.Common.VAL_UPDATE_MODE);
            return _entRole;
        }

        /// <summary>
        /// Add - Update RuleRoleScope Method
        /// </summary>
        /// <param name="pEntRole"></param>
        /// <param name="pUpdateMode"></param>
        /// <returns></returns>
        private RuleRoleScope Update(RuleRoleScope pEntRole, string pUpdateMode)
        {
            int iRows = 0;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.RuleRoleScope.PROC_UPDATE_RULE_ROLE_SCOPE;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntRole.ClientId);
                if (pUpdateMode == Schema.Common.VAL_INSERT_MODE)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, System.DBNull.Value);
                }
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntRole.IsDefaultScope.ToString()))
                    _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_IS_DEFAULT_SCOPE, pEntRole.IsDefaultScope);
                else
                    _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_IS_DEFAULT_SCOPE, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntRole.RoleId))
                    _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_ROLE_ID, pEntRole.RoleId);
                else
                    _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_ROLE_ID, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntRole.RuleId))
                    _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_RULE_ID, pEntRole.RuleId);
                else
                    _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_RULE_ID, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntRole.ScopeLevelId))
                    _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPELEVEL_ID, pEntRole.ScopeLevelId);
                else
                    _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPELEVEL_ID, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntRole.ScopeRuleId))
                    _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPE_RULE_ID, pEntRole.ScopeRuleId);
                else
                    _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPE_RULE_ID, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntRole.ScopeUnitId))
                    _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPEUNIT_ID, pEntRole.ScopeUnitId);
                else
                    _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPEUNIT_ID, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntRole.LastModifiedById))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntRole.LastModifiedById);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntRole;
        }

        /// <summary>
        /// List of RuleRoleList in Sent RoleID parameter
        /// </summary>
        /// <param name="pEntRole"></param>
        /// <returns></returns>
        public List<RuleRoleScope> GetInRole(RuleRoleScope pEntRole)
        {
            return GetInORNotInRoleList(pEntRole, true);
        }

        /// <summary>
        /// List of RuleRoleList Not In RoleId Parameter
        /// </summary>
        /// <param name="pEntRole"></param>
        /// <returns></returns>
        public List<RuleRoleScope> GetNotInRole(RuleRoleScope pEntRole)
        {
            return GetInORNotInRoleList(pEntRole, false);
        }

        /// <summary>
        /// Get In OR Not In Role List
        /// </summary>
        /// <param name="pEntRole"></param>
        /// <param name="bInRole"></param>
        /// <returns></returns>
        private List<RuleRoleScope> GetInORNotInRoleList(RuleRoleScope pEntRole, bool bInRole)
        {
            RuleRoleScope entRole = new RuleRoleScope();
            List<RuleRoleScope> entListRole = new List<RuleRoleScope>();
            SqlConnection sqlConnection = null;
            SqlDataReader sqlreader = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntRole.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.RuleRoleScope.PROC_BY_ROLE_RULE_ROLE_SCOPE, sqlConnection);
                _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_ROLE_ID, pEntRole.RoleId);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_QUERY_TYPE, bInRole);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.GroupRule.PARA_IS_ACTIVE, pEntRole.IsActive);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntRole.CreatedById != null)
                {
                    _sqlpara = new SqlParameter(Schema.GroupRule.PARA_CREATED_BY_ID, pEntRole.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntRole.ListRange != null)
                {
                    if (pEntRole.ListRange.PageIndex > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntRole.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntRole.ListRange.PageSize > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntRole.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntRole.ListRange.SortExpression))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntRole.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                while (sqlreader.Read())
                {
                    entRole = FillObjectInAndNotIn(sqlreader, _sqlObject);
                    entListRole.Add(entRole);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlreader != null && !sqlreader.IsClosed) sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListRole;
        }

        /// <summary>
        /// List All By All Role
        /// </summary>
        /// <param name="pEntRole"></param>
        /// <returns></returns>
        public List<RuleRoleScope> GetListAllByAllRole(RuleRoleScope pEntRole)
        {
            return GetListAllByAllRoleList(pEntRole, false);
        }

        /// <summary>
        /// Get List All By All Role List
        /// </summary>
        /// <param name="pEntRole"></param>
        /// <param name="bInRole"></param>
        /// <returns></returns>
        private List<RuleRoleScope> GetListAllByAllRoleList(RuleRoleScope pEntRole, bool bInRole)
        {
            RuleRoleScope entRole = new RuleRoleScope();
            List<RuleRoleScope> entListRole = new List<RuleRoleScope>();
            SqlConnection sqlConnection = null;
            SqlDataReader sqlreader = null;
            _sqlObject = new SQLObject();
            try
            {

                _strConnString = _sqlObject.GetClientDBConnString(pEntRole.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.RuleRoleScope.PROC_BY_ROLE_RULE_ROLE_SCOPE_ALL, sqlConnection);
                _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_ROLE_ID, pEntRole.RoleId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_QUERY_TYPE, bInRole);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntRole.IsActive)
                {
                    _sqlpara = new SqlParameter(Schema.GroupRule.PARA_IS_ACTIVE, pEntRole.IsActive);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntRole.ListRange != null)
                {
                    if (pEntRole.ListRange.PageIndex > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntRole.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntRole.ListRange.PageSize > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntRole.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntRole.ListRange.SortExpression))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntRole.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                while (sqlreader.Read())
                {
                    entRole = FillObjectInAndNotIn(sqlreader, _sqlObject);
                    entListRole.Add(entRole);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlreader != null && !sqlreader.IsClosed) sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListRole;
        }

        /// <summary>
        /// Fill RuleRoleScope Object
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns></returns>
        private RuleRoleScope FillObjectInAndNotIn(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            _entRole = new RuleRoleScope();
            int index;
            index = pSqlReader.GetOrdinal(Schema.Asset.COL_CLIENT_ID);
            if (!pSqlReader.IsDBNull(index))
                _entRole.ClientId = pSqlReader.GetString(index);

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.RuleRoleScope.COL_ROLE_ID))
            {
                index = pSqlReader.GetOrdinal(Schema.RuleRoleScope.COL_ROLE_ID);
                if (!pSqlReader.IsDBNull(index))
                {

                    _entRole.RoleId = pSqlReader.GetString(index);


                }
            }

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.AdminRole.COL_ROLE_NAME))
            {
                index = pSqlReader.GetOrdinal(Schema.AdminRole.COL_ROLE_NAME);
                if (!pSqlReader.IsDBNull(index))
                    _entRole.RoleName = pSqlReader.GetString(index);
            }

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.AdminRole.COL_RULE_SCOPE))
            {
                index = pSqlReader.GetOrdinal(Schema.AdminRole.COL_RULE_SCOPE);
                if (!pSqlReader.IsDBNull(index))
                    _entRole.RuleScope = pSqlReader.GetString(index);
            }



            index = pSqlReader.GetOrdinal(Schema.RuleRoleScope.COL_RULE_ID);
            if (!pSqlReader.IsDBNull(index))
                _entRole.RuleId = pSqlReader.GetString(index);

            index = pSqlReader.GetOrdinal(Schema.GroupRule.COL_RULE_NAME);
            if (!pSqlReader.IsDBNull(index))
                _entRole.RuleName = pSqlReader.GetString(index);

            index = pSqlReader.GetOrdinal(Schema.GroupRule.COL_RULE_DESC);
            if (!pSqlReader.IsDBNull(index))
                _entRole.RuleDescription = pSqlReader.GetString(index);

            index = pSqlReader.GetOrdinal(Schema.GroupRule.COL_IS_ACTIVE);
            if (!pSqlReader.IsDBNull(index))
                _entRole.IsActive = pSqlReader.GetBoolean(index);

            index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
            if (!pSqlReader.IsDBNull(index))
                _entRole.CreatedById = pSqlReader.GetString(index);

            index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
            if (!pSqlReader.IsDBNull(index))
                _entRole.DateCreated = pSqlReader.GetSqlDateTime(index).Value;

            index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
            if (!pSqlReader.IsDBNull(index))
                _entRole.LastModifiedById = pSqlReader.GetString(index);

            index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
            if (!pSqlReader.IsDBNull(index))
                _entRole.LastModifiedDate = pSqlReader.GetSqlDateTime(index).Value;

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY_NAME))
            {
                index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                if (!pSqlReader.IsDBNull(index))
                    _entRole.CreatedByName = pSqlReader.GetString(index);
            }
            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY_NAME))
            {
                index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY_NAME);
                if (!pSqlReader.IsDBNull(index))
                    _entRole.LastModifiedByName = pSqlReader.GetString(index);
            }

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
            {
                index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                if (!pSqlReader.IsDBNull(index))
                {
                    if (pSqlReader.GetInt32(index) > 0)
                    {
                        entRange = new EntityRange();
                        entRange.TotalRows = pSqlReader.GetInt32(index);
                        _entRole.ListRange = entRange;

                    }
                }
            }

            return _entRole;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntRuleRoleScope"></param>
        /// <returns></returns>
        public RuleRoleScope Get(RuleRoleScope pEntRuleRoleScope)
        {
            return GetRuleRoleByID(pEntRuleRoleScope);
        }
        /// <summary>
        /// Update RuleRoleScope
        /// </summary>
        /// <param name="pEntRuleRoleScope"></param>
        /// <returns></returns>
        public RuleRoleScope Update(RuleRoleScope pEntRuleRoleScope)
        {
            return EditRuleRoleScope(pEntRuleRoleScope);
        }
        #endregion
    }
}
