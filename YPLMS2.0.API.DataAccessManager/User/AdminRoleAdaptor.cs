using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    /// <summary>
    /// class AdminRoleAdaptor
    /// </summary>
    public class AdminRoleAdaptor : IDataManager<AdminRole>, IAdminRoleAdaptor<AdminRole>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlTransaction _sqltrans = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        SqlDataAdapter _sqladapter = null;
        SqlConnection _sqlcon = null;
        DataSet _dset = null;
        DataTable _dtable = null;
        AdminRole _entAdminRole = null;
        UserAdminRole _entUserAdminRole = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.AdminRole.ADMIN_ROLE_ERROR;
        #endregion

        public DataSet GetAllUserByRule(AdminRole pEntMenuItems)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.AdminRole.PROC_GET_USERID_BY_ROLE_ID;
            DataSet _dsLoginKey = new DataSet();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntMenuItems.ClientId);
                sqlConnection = new SqlConnection(_strConnString);


                if (!String.IsNullOrEmpty(pEntMenuItems.ID))
                    _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, pEntMenuItems.ID);
                else
                    _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlcmd.Connection = sqlConnection;
                _dsLoginKey = _sqlObject.SqlDataAdapter(_sqlcmd, _strConnString);

            }

            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return  _dsLoginKey;
        }


        /// <summary>
        /// To get admin role data by role id
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <returns>Role object</returns>
        public AdminRole GetAdminRoleByID(AdminRole pEntAdminRole)
        {
            _sqlObject = new SQLObject();
            _entAdminRole = new AdminRole();
            AdminRoleFeatures entAdminRoleFeatures = new AdminRoleFeatures();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAdminRole.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.AdminRole.PROC_GET_ROLE_MASTER, sqlConnection);
                if (!String.IsNullOrEmpty(pEntAdminRole.ID))
                    _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, pEntAdminRole.ID);
                else
                    _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    _entAdminRole = FillObject(_sqlreader, _sqlObject);
                }
                //------- Fill Features of the Role
                entAdminRoleFeatures.RoleId = pEntAdminRole.ID;
                entAdminRoleFeatures.ClientId = pEntAdminRole.ClientId;
                _entAdminRole.Features.AddRange(GetAllFeatures(entAdminRoleFeatures));
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
            return _entAdminRole;
        }

        /// <summary>
        /// To get admin role data by role id
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <returns>Role object</returns>
        public AdminRole GetAdminRoleNameByID(AdminRole pEntAdminRole)
        {
            _sqlObject = new SQLObject();
            _entAdminRole = new AdminRole();
            AdminRoleFeatures entAdminRoleFeatures = new AdminRoleFeatures();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAdminRole.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.AdminRole.PROC_GET_ROLE_MASTER_SEL_NAME, sqlConnection);
                if (!String.IsNullOrEmpty(pEntAdminRole.ID))
                    _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, pEntAdminRole.ID);
                else
                    _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);
                Object obj = null;
                obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
                if (obj != null)
                {
                    _entAdminRole.RoleName = Convert.ToString(obj);
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
            return _entAdminRole;
        }

        /// <summary>
        /// To get admin role data by role id
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <returns>Role object</returns>
        public AdminRole CheckRoleName(AdminRole pEntAdminRole)
        {
            _sqlObject = new SQLObject();
            AdminRole entAdminRole = new AdminRole();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAdminRole.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                _sqlcmd = new SqlCommand(Schema.AdminRole.PROC_GET_ROLE_NAME, sqlConnection);

                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_NAME, pEntAdminRole.RoleName);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    entAdminRole = FillObject_ByName(_sqlreader);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entAdminRole;
        }

        /// <summary>
        /// Get All Features List
        /// </summary>
        /// <param name="pEntAdminRoleFeatures"></param>
        /// <returns></returns>
        public List<AdminRoleFeatures> GetAllFeatures(AdminRoleFeatures pEntAdminRoleFeatures)
        {
            AdminRoleFeatures entAdminRoleFeatures = new AdminRoleFeatures();
            List<AdminRoleFeatures> entListAdminRoleFeatures = new List<AdminRoleFeatures>();
            SqlDataReader sqlreaderGetAllFeatures = null;
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAdminRoleFeatures.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.AdminRoleFeatures.PROC_GET_ROLE_WISE_FEATURES, sqlConnection);
                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, pEntAdminRoleFeatures.RoleId);
                _sqlcmd.Parameters.Add(_sqlpara);
                sqlreaderGetAllFeatures = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (sqlreaderGetAllFeatures.Read())
                {
                    entAdminRoleFeatures = FillObjectAdminRoleFeatures(sqlreaderGetAllFeatures);
                    entListAdminRoleFeatures.Add(entAdminRoleFeatures);
                }
            }
            finally
            {
                if (sqlreaderGetAllFeatures != null && !sqlreaderGetAllFeatures.IsClosed)
                    sqlreaderGetAllFeatures.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListAdminRoleFeatures;
        }

        /// <summary>
        /// Get All Roles List
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <returns></returns>
        public List<AdminRole> GetAllRoles(AdminRole pEntAdminRole)
        {
            AdminRole entAdminRole = new AdminRole();
            List<AdminRole> entListAdminRole = new List<AdminRole>();
            SqlDataReader sqlreaderGetAll = null;
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAdminRole.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.AdminRole.PROC_GET_ALL_ROLE_MASTER, sqlConnection);
                if (pEntAdminRole.ListRange != null)
                {
                    if (pEntAdminRole.ListRange.PageIndex > 0)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntAdminRole.ListRange.PageIndex);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (pEntAdminRole.ListRange.PageSize > 0)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntAdminRole.ListRange.PageSize);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (!string.IsNullOrEmpty(pEntAdminRole.ListRange.SortExpression))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntAdminRole.ListRange.SortExpression);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (pEntAdminRole.ListRange.RequestedById != null)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntAdminRole.ListRange.RequestedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                sqlreaderGetAll = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (sqlreaderGetAll.Read())
                {
                    entAdminRole = FillObject(sqlreaderGetAll, _sqlObject);
                    entListAdminRole.Add(entAdminRole);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (sqlreaderGetAll != null && !sqlreaderGetAll.IsClosed) sqlreaderGetAll.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListAdminRole;
        }

        /// <summary>
        /// Get All Roles List For Report
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <returns></returns>
        public List<AdminRole> GetAllRolesForReport(AdminRole pEntAdminRole)
        {
            AdminRole entAdminRole = new AdminRole();
            List<AdminRole> entListAdminRole = new List<AdminRole>();
            SqlDataReader sqlreaderGetAll = null;
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAdminRole.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.AdminRole.PROC_GET_ALL_ROLE_MASTER_FOR_REPORT, sqlConnection);
                if (pEntAdminRole.ListRange != null)
                {
                    if (pEntAdminRole.ListRange.PageIndex > 0)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntAdminRole.ListRange.PageIndex);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (pEntAdminRole.ListRange.PageSize > 0)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntAdminRole.ListRange.PageSize);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (!string.IsNullOrEmpty(pEntAdminRole.ListRange.SortExpression))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntAdminRole.ListRange.SortExpression);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (pEntAdminRole.ListRange.RequestedById != null)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntAdminRole.ListRange.RequestedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                sqlreaderGetAll = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (sqlreaderGetAll.Read())
                {
                    entAdminRole = FillObject(sqlreaderGetAll, _sqlObject);
                    entListAdminRole.Add(entAdminRole);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (sqlreaderGetAll != null && !sqlreaderGetAll.IsClosed) sqlreaderGetAll.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListAdminRole;
        }


        /// <summary>
        /// Get All Roles List - Active/Inactive Only
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <returns></returns>
        public List<AdminRole> GetAllRolesByActiveStatus(AdminRole pEntAdminRole)
        {
            AdminRole entAdminRole = new AdminRole();
            List<AdminRole> entListAdminRole = new List<AdminRole>();
            SqlDataReader sqlreaderGetAll = null;
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAdminRole.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.AdminRole.PROC_GET_ALL_ROLE_MASTER, sqlConnection);

                _sqlpara = new SqlParameter(Schema.Common.PARA_IS_ACTIVE, pEntAdminRole.IsActive);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntAdminRole.ListRange != null)
                {
                    if (pEntAdminRole.ListRange.PageIndex > 0)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntAdminRole.ListRange.PageIndex);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (pEntAdminRole.ListRange.PageSize > 0)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntAdminRole.ListRange.PageSize);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (!string.IsNullOrEmpty(pEntAdminRole.ListRange.SortExpression))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntAdminRole.ListRange.SortExpression);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    //if (!String.IsNullOrEmpty(pEntAdminRole.ListRange.RequestedById))
                    //    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntAdminRole.ListRange.RequestedById);
                    //else
                    //    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, null);
                    //_sqlcmd.Parameters.Add(_sqlpara);
                    if (!string.IsNullOrEmpty(pEntAdminRole.ListRange.RequestedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntAdminRole.ListRange.RequestedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                sqlreaderGetAll = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (sqlreaderGetAll.Read())
                {
                    entAdminRole = FillObject(sqlreaderGetAll, _sqlObject);
                    entListAdminRole.Add(entAdminRole);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (sqlreaderGetAll != null && !sqlreaderGetAll.IsClosed) sqlreaderGetAll.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListAdminRole;
        }

        /// <summary>
        /// To fill role object data from reader.
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <returns>Role object</returns>
        private AdminRole FillObject_ByName(SqlDataReader pSqlreader)
        {
            _entAdminRole = new AdminRole();
            int index;

            index = pSqlreader.GetOrdinal(Schema.AdminRole.COL_ROLE_ID);
            if (!pSqlreader.IsDBNull(index))
                _entAdminRole.ID = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.AdminRole.COL_ROLE_NAME);
            if (!pSqlreader.IsDBNull(index))
                _entAdminRole.RoleName = pSqlreader.GetString(index);

            return _entAdminRole;
        }

        /// <summary>
        /// To fill role object data from reader.
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <returns>Role object</returns>
        private AdminRole FillObject(SqlDataReader pSqlreader, SQLObject pSqlObject)
        {
            _entAdminRole = new AdminRole();
            int index;

            index = pSqlreader.GetOrdinal(Schema.AdminRole.COL_ROLE_ID);
            if (!pSqlreader.IsDBNull(index))
                _entAdminRole.ID = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.AdminRole.COL_ROLE_NAME);
            if (!pSqlreader.IsDBNull(index))
                _entAdminRole.RoleName = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.AdminRole.COL_ROLE_DESC);
            if (!pSqlreader.IsDBNull(index))
                _entAdminRole.RoleDescription = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.AdminRole.COL_ROLE_TYPE);
            if (!pSqlreader.IsDBNull(index))
                _entAdminRole.AdminRoleType = (RoleType)Enum.Parse(typeof(RoleType), pSqlreader.GetString(index));

            index = pSqlreader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
            if (!pSqlreader.IsDBNull(index))
                _entAdminRole.ClientId = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.AdminRole.COL_IS_ACTIVE);
            if (!pSqlreader.IsDBNull(index))
                _entAdminRole.IsActive = pSqlreader.GetBoolean(index);

            index = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_BY);
            if (!pSqlreader.IsDBNull(index))
                _entAdminRole.CreatedById = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_ON);
            if (!pSqlreader.IsDBNull(index))
                _entAdminRole.DateCreated = pSqlreader.GetDateTime(index);

            index = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
            if (!pSqlreader.IsDBNull(index))
                _entAdminRole.LastModifiedById = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
            if (!pSqlreader.IsDBNull(index))
                _entAdminRole.LastModifiedDate = pSqlreader.GetDateTime(index);

            if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Common.COL_CREATED_BY_NAME))
            {
                index = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                if (!pSqlreader.IsDBNull(index))
                    _entAdminRole.CreatedByName = pSqlreader.GetString(index);
            }
            if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Common.COL_MODIFIED_BY_NAME))
            {
                index = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_BY_NAME);
                if (!pSqlreader.IsDBNull(index))
                    _entAdminRole.LastModifiedByName = pSqlreader.GetString(index);
            }
            if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Common.COL_TOTAL_COUNT))
            {
                index = pSqlreader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                if (!pSqlreader.IsDBNull(index))
                {
                    _entRange = new EntityRange();
                    _entRange.TotalRows = pSqlreader.GetInt32(index);
                    _entAdminRole.ListRange = _entRange;
                }
            }
            return _entAdminRole;
        }

        /// <summary>
        /// To fill Admin Role Features object data from reader.
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <returns></returns>
        private AdminRoleFeatures FillObjectAdminRoleFeatures(SqlDataReader pSqlreader)
        {
            AdminRoleFeatures entAdminRoleFeatures = new AdminRoleFeatures();
            int iIndex;

            iIndex = pSqlreader.GetOrdinal(Schema.AdminRole.COL_ROLE_ID);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.RoleId = pSqlreader.GetString(iIndex);

            iIndex = pSqlreader.GetOrdinal(Schema.AdminRoleFeatures.COL_FEATURE_ID);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.ID = pSqlreader.GetString(iIndex);

            iIndex = pSqlreader.GetOrdinal(Schema.AdminRoleFeatures.COL_CAN_ACTIVATE);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.CanActivate = pSqlreader.GetBoolean(iIndex);

            iIndex = pSqlreader.GetOrdinal(Schema.AdminRoleFeatures.COL_CAN_ADD);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.CanAdd = pSqlreader.GetBoolean(iIndex);

            iIndex = pSqlreader.GetOrdinal(Schema.AdminRoleFeatures.COL_CAN_COPY);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.CanCopy = pSqlreader.GetBoolean(iIndex);

            iIndex = pSqlreader.GetOrdinal(Schema.AdminRoleFeatures.COL_CAN_DEACTIVATE);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.CanDeactivate = pSqlreader.GetBoolean(iIndex);

            iIndex = pSqlreader.GetOrdinal(Schema.AdminRoleFeatures.COL_CAN_DELETE);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.CanDelete = pSqlreader.GetBoolean(iIndex);

            iIndex = pSqlreader.GetOrdinal(Schema.AdminRoleFeatures.COL_CAN_EDIT);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.CanEdit = pSqlreader.GetBoolean(iIndex);

            iIndex = pSqlreader.GetOrdinal(Schema.AdminRoleFeatures.COL_CAN_EMAIL);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.CanEmail = pSqlreader.GetBoolean(iIndex);

            iIndex = pSqlreader.GetOrdinal(Schema.AdminRoleFeatures.COL_CAN_EXPORT);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.CanExport = pSqlreader.GetBoolean(iIndex);

            iIndex = pSqlreader.GetOrdinal(Schema.AdminRoleFeatures.COL_CAN_IMPORT);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.CanImport = pSqlreader.GetBoolean(iIndex);

            iIndex = pSqlreader.GetOrdinal(Schema.AdminRoleFeatures.COL_CAN_PRINT);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.CanPrint = pSqlreader.GetBoolean(iIndex);

            iIndex = pSqlreader.GetOrdinal(Schema.AdminRoleFeatures.COL_CAN_UPLOAD);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.CanUpload = pSqlreader.GetBoolean(iIndex);

            iIndex = pSqlreader.GetOrdinal(Schema.AdminRoleFeatures.COL_CAN_VIEW);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.CanView = pSqlreader.GetBoolean(iIndex);

            iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_BY);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.CreatedById = pSqlreader.GetString(iIndex);

            iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_ON);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.DateCreated = pSqlreader.GetDateTime(iIndex);

            iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.LastModifiedById = pSqlreader.GetString(iIndex);

            iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
            if (!pSqlreader.IsDBNull(iIndex))
                entAdminRoleFeatures.LastModifiedDate = pSqlreader.GetDateTime(iIndex);


            return entAdminRoleFeatures;
        }

        /// <summary>
        /// To add new role.
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <returns>newly added admin role object</returns>
        public AdminRole AddAdminRole(AdminRole pEntAdminRole)
        {
            _entAdminRole = new AdminRole();
            _entAdminRole = AddUpdateRoleWithFeature(pEntAdminRole, Schema.Common.VAL_INSERT_MODE);
            return _entAdminRole;
        }

        /// <summary>
        /// To Add/Update List of Roles.
        /// </summary>
        /// <param name="pEntListAdminRole"></param>
        /// <returns>List of AdminRole object</returns>
        public List<AdminRole> UpdateListOfAdminRoles(List<AdminRole> pEntListBaseAdminRole)
        {
            List<AdminRole> _entlistAdminRole = new List<AdminRole>();
            _entlistAdminRole = BulkUdateAdminRoles(pEntListBaseAdminRole);
            return _entlistAdminRole;
        }

        /// <summary>
        /// Bulk Add/Update AdminRoles
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <returns>List Of AdminRole Object</returns>
        private List<AdminRole> BulkUdateAdminRoles(List<AdminRole> pEntListBaseAdminRole)
        {
            List<AdminRole> entListAdminRoleReturn = new List<AdminRole>();
            List<AdminRole> entListNewAdminRole = new List<AdminRole>();
            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(pEntListBaseAdminRole[0].ClientId);
            _sqlcon = new SqlConnection(_strConnString);
            _sqladapter = new SqlDataAdapter();
            _dset = new DataSet();
            _dtable = new DataTable();
            _sqlcmd = new SqlCommand();
            string strSQL = string.Empty;
            int iBatchSize = 0;
            if (pEntListBaseAdminRole.Count > 0)
            {
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.AdminRole.PROC_GET_ALL_ROLE_MASTER;
                _sqlcmd.CommandType = CommandType.StoredProcedure;
                _sqlcmd.Connection = _sqlcon;
                _sqladapter = new SqlDataAdapter(_sqlcmd);
                _sqladapter.Fill(_dset);
                _dtable = new DataTable();
                _dtable = _dset.Tables[0];
                _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);
                _dtable.PrimaryKey = new DataColumn[] { _dtable.Columns[Schema.AdminRole.COL_ROLE_ID] };
                foreach (AdminRole entARole in pEntListBaseAdminRole)
                {
                    DataRow drow = _dtable.Rows.Find(entARole.ID);
                    if (drow != null)
                    {
                        //In case of Update
                        drow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_UPDATE_MODE;
                        drow[Schema.AdminRole.COL_ROLE_ID] = entARole.ID;
                        drow[Schema.Client.COL_CLIENT_ID] = entARole.ClientId;
                        drow[Schema.AdminRole.COL_IS_ACTIVE] = entARole.IsActive;
                        if (!string.IsNullOrEmpty(entARole.RoleDescription))
                            drow[Schema.AdminRole.COL_ROLE_DESC] = entARole.RoleDescription;
                        if (!string.IsNullOrEmpty(entARole.RoleName))
                            drow[Schema.AdminRole.COL_ROLE_NAME] = entARole.RoleName;
                        drow[Schema.AdminRole.COL_ROLE_TYPE] = entARole.AdminRoleType.ToString();
                        drow[Schema.Common.COL_CREATED_BY] = entARole.CreatedById;
                        drow[Schema.Common.COL_MODIFIED_BY] = entARole.LastModifiedById;
                        iBatchSize = iBatchSize + 1;
                        entListAdminRoleReturn.Add(entARole);
                    }
                    else
                    {
                        //In case of Add
                        entListNewAdminRole.Add(entARole);
                    }
                }
            }
            if (_dtable.Rows.Count > 0)
            {
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.AdminRole.PROC_UPDATE_ROLE_MASTER;
                _sqlcmd.CommandType = CommandType.StoredProcedure;
                _sqlcmd.Connection = _sqlcon;

                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, SqlDbType.VarChar, 100, Schema.AdminRole.COL_ROLE_ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, SqlDbType.VarChar, 100, Schema.Client.COL_CLIENT_ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_IS_ACTIVE, SqlDbType.Bit, 10, Schema.AdminRole.COL_IS_ACTIVE);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_DESC, SqlDbType.VarChar, 500, Schema.AdminRole.COL_ROLE_DESC);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_NAME, SqlDbType.VarChar, 100, Schema.AdminRole.COL_ROLE_NAME);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_TYPE, SqlDbType.VarChar, 100, Schema.AdminRole.COL_ROLE_TYPE);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_CREATED_BY);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, SqlDbType.NVarChar, 10, Schema.Common.COL_UPDATE_MODE);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqladapter.UpdateCommand = _sqlcmd;
                _sqladapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                _sqladapter.UpdateBatchSize = iBatchSize;
                _sqladapter.Update(_dtable);
                _sqladapter.Dispose();
                _dset = null;
            }
            return entListAdminRoleReturn;
        }

        /// <summary>
        /// To edit admin role data
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <returns>updated admin role object </returns>
        public AdminRole EditAdminRole(AdminRole pEntAdminRole)
        {
            _entAdminRole = new AdminRole();
            _entAdminRole = AddUpdateRoleWithFeature(pEntAdminRole, Schema.Common.VAL_UPDATE_MODE);
            return _entAdminRole;
        }

        /// <summary>
        /// To delete admin role.
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <returns>returns null admin role object if role gets deleted</returns>
        public AdminRole DeleteAdminRole(AdminRole pEntAdminRole)
        {
            int iRowsAffected = -1;
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.AdminRole.PROC_DELETE_ROLE_MASTER;
            if (!String.IsNullOrEmpty(pEntAdminRole.ID))
                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, pEntAdminRole.ID);
            else
                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAdminRole.ClientId);
                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                if (iRowsAffected > 0)
                    pEntAdminRole = null;
            }
            catch (Exception expCommon)
            {
               _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntAdminRole;
        }

        /// <summary>
        /// To update/add new admin role data
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <param name="pStrUpdateMode"></param>
        /// <returns>AdminRole object</returns>
        private AdminRole AddUpdateRoleWithFeature(AdminRole pEntAdminRole, string pStrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.AdminRole.PROC_UPDATE_ROLE_MASTER;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAdminRole.ClientId);
                if (pStrUpdateMode == Schema.Common.VAL_INSERT_MODE)
                {
                    //-- Re-assign new id
                    pEntAdminRole.ID = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_ROLE_ID_PREFIX, Schema.Common.VAL_ROLE_ID_LENGTH);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                }
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);

                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, pEntAdminRole.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_NAME, pEntAdminRole.RoleName);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntAdminRole.RoleDescription))
                    _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_DESC, pEntAdminRole.RoleDescription);
                else
                    _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_DESC, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_TYPE, pEntAdminRole.AdminRoleType.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntAdminRole.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_IS_ACTIVE, pEntAdminRole.IsActive);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntAdminRole.CreatedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntAdminRole.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

                //---------- Bulk Add/Update Admin Role Feature List ---------------
                BulkAddUpdateAdminRoleFeature(pEntAdminRole);
            }
            catch (Exception expCommon)
            {
               _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntAdminRole;
        }

        /// <summary>
        /// Bulk Add/Update AdminRoleFeatures
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <returns>List Of AdminRoleFeatures Object</returns>
        private List<AdminRoleFeatures> BulkAddUpdateAdminRoleFeature(AdminRole pEntAdminRole)
        {
            _sqlObject = new SQLObject();
            List<AdminRoleFeatures> entListAdminRoleFeatures = new List<AdminRoleFeatures>();
            entListAdminRoleFeatures.AddRange(pEntAdminRole.Features);
            List<AdminRoleFeatures> entListNewAdminRoleFeatures = new List<AdminRoleFeatures>();
            _strConnString = _sqlObject.GetClientDBConnString(pEntAdminRole.ClientId);
            _sqlcon = new SqlConnection(_strConnString);
            _sqladapter = new SqlDataAdapter();
            _dset = new DataSet();
            _dtable = new DataTable();
            _sqlcmd = new SqlCommand();
            string strSQL = string.Empty;
            string strRoleId = pEntAdminRole.ID;
            int iBatchSize = 0;
            if (entListAdminRoleFeatures.Count > 0)
            {
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.AdminRoleFeatures.PROC_GET_ROLE_WISE_FEATURES;
                _sqlcmd.CommandType = CommandType.StoredProcedure;
                _sqlcmd.Parameters.AddWithValue(Schema.AdminRole.PARA_ROLE_ID, strRoleId);
                _sqlcmd.Connection = _sqlcon;
                _sqladapter = new SqlDataAdapter(_sqlcmd);
                _sqladapter.Fill(_dset);
                _dtable = new DataTable();
                _dtable = _dset.Tables[0];
                _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);
                _dtable.PrimaryKey = new DataColumn[] { _dtable.Columns[Schema.AdminFeatures.COL_FEATURE_ID] };
                foreach (AdminRoleFeatures entARFeatures in entListAdminRoleFeatures)
                {
                    DataRow drow = _dtable.Rows.Find(entARFeatures.ID);
                    if (drow != null)
                    {
                        drow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_UPDATE_MODE;
                        drow[Schema.AdminRole.COL_ROLE_ID] = pEntAdminRole.ID;
                        drow[Schema.AdminRoleFeatures.COL_FEATURE_ID] = entARFeatures.ID;
                        drow[Schema.AdminRoleFeatures.COL_CAN_ACTIVATE] = entARFeatures.CanActivate;
                        drow[Schema.AdminRoleFeatures.COL_CAN_ADD] = entARFeatures.CanAdd;
                        drow[Schema.AdminRoleFeatures.COL_CAN_COPY] = entARFeatures.CanCopy;
                        drow[Schema.AdminRoleFeatures.COL_CAN_DEACTIVATE] = entARFeatures.CanDeactivate;
                        drow[Schema.AdminRoleFeatures.COL_CAN_DELETE] = entARFeatures.CanDelete;
                        drow[Schema.AdminRoleFeatures.COL_CAN_EDIT] = entARFeatures.CanEdit;
                        drow[Schema.AdminRoleFeatures.COL_CAN_EMAIL] = entARFeatures.CanEmail;
                        drow[Schema.AdminRoleFeatures.COL_CAN_EXPORT] = entARFeatures.CanExport;
                        drow[Schema.AdminRoleFeatures.COL_CAN_IMPORT] = entARFeatures.CanImport;
                        drow[Schema.AdminRoleFeatures.COL_CAN_PRINT] = entARFeatures.CanPrint;
                        drow[Schema.AdminRoleFeatures.COL_CAN_UPLOAD] = entARFeatures.CanUpload;
                        drow[Schema.AdminRoleFeatures.COL_CAN_VIEW] = entARFeatures.CanView;
                        drow[Schema.Common.COL_CREATED_BY] = entARFeatures.CreatedById;
                        drow[Schema.Common.COL_MODIFIED_BY] = entARFeatures.LastModifiedById;
                        iBatchSize = iBatchSize + 1;
                    }
                    else
                    {
                        entARFeatures.RoleId = strRoleId;
                        entListNewAdminRoleFeatures.Add(entARFeatures);
                    }
                }
            }
            if (_dtable.Rows.Count > 0)
            {
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.AdminRoleFeatures.PROC_UPDATE_ROLE_FEATURE;
                _sqlcmd.CommandType = CommandType.StoredProcedure;
                _sqlcmd.Connection = _sqlcon;

                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, SqlDbType.VarChar, 100, Schema.AdminRole.COL_ROLE_ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRoleFeatures.PARA_FEATURE_ID, SqlDbType.VarChar, 100, Schema.AdminRoleFeatures.COL_FEATURE_ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRoleFeatures.PARA_CAN_ACTIVATE, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_ACTIVATE);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRoleFeatures.PARA_CAN_ADD, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_ADD);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRoleFeatures.PARA_CAN_COPY, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_COPY);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRoleFeatures.PARA_CAN_DEACTIVATE, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_DEACTIVATE);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRoleFeatures.PARA_CAN_DELETE, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_DELETE);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRoleFeatures.PARA_CAN_EDIT, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_EDIT);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRoleFeatures.PARA_CAN_EMAIL, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_EMAIL);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRoleFeatures.PARA_CAN_EXPORT, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_EXPORT);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRoleFeatures.PARA_CAN_IMPORT, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_IMPORT);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRoleFeatures.PARA_CAN_PRINT, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_PRINT);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRoleFeatures.PARA_CAN_UPLOAD, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_UPLOAD);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRoleFeatures.PARA_CAN_VIEW, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_VIEW);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_CREATED_BY);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, SqlDbType.NVarChar, 10, Schema.Common.COL_UPDATE_MODE);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqladapter.UpdateCommand = _sqlcmd;
                _sqladapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                _sqladapter.UpdateBatchSize = iBatchSize;
                _sqladapter.Update(_dtable);
                _sqladapter.Dispose();
                _dset = null;
            }
            if (entListNewAdminRoleFeatures.Count > 0)
            {
                AddMultipleAdminRoleFeatures(entListNewAdminRoleFeatures);
            }
            return entListAdminRoleFeatures;
        }

        /// <summary>
        /// Add Multiple Admin Role Features
        /// </summary>
        /// <param name="pEntListAdminRoleFeatures"></param>
        private void AddMultipleAdminRoleFeatures(List<AdminRoleFeatures> pEntListAdminRoleFeatures)
        {
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            _sqlcon = new SqlConnection(_strConnString);
            int iBatchSize = 0;
            if (pEntListAdminRoleFeatures.Count > 0)
            {
                _dtable = new DataTable();
                _dtable.Columns.Add(Schema.AdminRole.COL_ROLE_ID);
                _dtable.Columns.Add(Schema.AdminRoleFeatures.COL_FEATURE_ID);
                _dtable.Columns.Add(Schema.AdminRoleFeatures.COL_CAN_ACTIVATE);
                _dtable.Columns.Add(Schema.AdminRoleFeatures.COL_CAN_ADD);
                _dtable.Columns.Add(Schema.AdminRoleFeatures.COL_CAN_COPY);
                _dtable.Columns.Add(Schema.AdminRoleFeatures.COL_CAN_DEACTIVATE);
                _dtable.Columns.Add(Schema.AdminRoleFeatures.COL_CAN_DELETE);
                _dtable.Columns.Add(Schema.AdminRoleFeatures.COL_CAN_EDIT);
                _dtable.Columns.Add(Schema.AdminRoleFeatures.COL_CAN_EMAIL);
                _dtable.Columns.Add(Schema.AdminRoleFeatures.COL_CAN_EXPORT);
                _dtable.Columns.Add(Schema.AdminRoleFeatures.COL_CAN_IMPORT);
                _dtable.Columns.Add(Schema.AdminRoleFeatures.COL_CAN_PRINT);
                _dtable.Columns.Add(Schema.AdminRoleFeatures.COL_CAN_UPLOAD);
                _dtable.Columns.Add(Schema.AdminRoleFeatures.COL_CAN_VIEW);
                _dtable.Columns.Add(Schema.Common.COL_CREATED_BY);
                _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                for (int i = 0; i < pEntListAdminRoleFeatures.Count; i++)
                {
                    DataRow drow = _dtable.NewRow();

                    drow[Schema.AdminRole.COL_ROLE_ID] = pEntListAdminRoleFeatures[i].RoleId;
                    drow[Schema.AdminRoleFeatures.COL_FEATURE_ID] = pEntListAdminRoleFeatures[i].ID;
                    drow[Schema.AdminRoleFeatures.COL_CAN_ACTIVATE] = pEntListAdminRoleFeatures[i].CanActivate;
                    drow[Schema.AdminRoleFeatures.COL_CAN_ADD] = pEntListAdminRoleFeatures[i].CanAdd;
                    drow[Schema.AdminRoleFeatures.COL_CAN_COPY] = pEntListAdminRoleFeatures[i].CanCopy;
                    drow[Schema.AdminRoleFeatures.COL_CAN_DEACTIVATE] = pEntListAdminRoleFeatures[i].CanDeactivate;
                    drow[Schema.AdminRoleFeatures.COL_CAN_DELETE] = pEntListAdminRoleFeatures[i].CanDelete;
                    drow[Schema.AdminRoleFeatures.COL_CAN_EDIT] = pEntListAdminRoleFeatures[i].CanEdit;
                    drow[Schema.AdminRoleFeatures.COL_CAN_EMAIL] = pEntListAdminRoleFeatures[i].CanEmail;
                    drow[Schema.AdminRoleFeatures.COL_CAN_EXPORT] = pEntListAdminRoleFeatures[i].CanExport;
                    drow[Schema.AdminRoleFeatures.COL_CAN_IMPORT] = pEntListAdminRoleFeatures[i].CanImport;
                    drow[Schema.AdminRoleFeatures.COL_CAN_PRINT] = pEntListAdminRoleFeatures[i].CanPrint;
                    drow[Schema.AdminRoleFeatures.COL_CAN_UPLOAD] = pEntListAdminRoleFeatures[i].CanUpload;
                    drow[Schema.AdminRoleFeatures.COL_CAN_VIEW] = pEntListAdminRoleFeatures[i].CanView;
                    drow[Schema.Common.COL_CREATED_BY] = pEntListAdminRoleFeatures[i].CreatedById;
                    drow[Schema.Common.COL_MODIFIED_BY] = pEntListAdminRoleFeatures[i].LastModifiedById;
                    drow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_INSERT_MODE;

                    _dtable.Rows.Add(drow);
                    iBatchSize = iBatchSize + 1;
                }
                if (_dtable.Rows.Count > 0)
                {
                    _sqlcmd = new SqlCommand();
                    _sqlcmd.CommandText = Schema.AdminRoleFeatures.PROC_UPDATE_ROLE_FEATURE;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;
                    try
                    {
                        _sqlcon.Open();
                        _sqltrans = _sqlcon.BeginTransaction();
                        _sqlcmd.Connection = _sqlcon;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqlcmd.Transaction = _sqltrans;

                        _sqladapter.InsertCommand.Parameters.Add(Schema.AdminRole.PARA_ROLE_ID, SqlDbType.VarChar, 100, Schema.AdminRole.COL_ROLE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AdminRoleFeatures.PARA_FEATURE_ID, SqlDbType.VarChar, 100, Schema.AdminRoleFeatures.COL_FEATURE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AdminRoleFeatures.PARA_CAN_ACTIVATE, SqlDbType.Bit, 100, Schema.AdminRoleFeatures.COL_CAN_ACTIVATE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AdminRoleFeatures.PARA_CAN_ADD, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_ADD);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AdminRoleFeatures.PARA_CAN_COPY, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_COPY);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AdminRoleFeatures.PARA_CAN_DEACTIVATE, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_DEACTIVATE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AdminRoleFeatures.PARA_CAN_DELETE, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_DELETE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AdminRoleFeatures.PARA_CAN_EDIT, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_EDIT);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AdminRoleFeatures.PARA_CAN_EMAIL, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_EMAIL);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AdminRoleFeatures.PARA_CAN_EXPORT, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_EXPORT);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AdminRoleFeatures.PARA_CAN_IMPORT, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_IMPORT);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AdminRoleFeatures.PARA_CAN_PRINT, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_PRINT);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AdminRoleFeatures.PARA_CAN_UPLOAD, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_UPLOAD);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AdminRoleFeatures.PARA_CAN_VIEW, SqlDbType.Bit, 10, Schema.AdminRoleFeatures.COL_CAN_VIEW);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_CREATED_BY, SqlDbType.NVarChar, 100, Schema.Common.COL_CREATED_BY);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.NVarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.Parameters.AddWithValue(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);

                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        _sqladapter.Update(_dtable);

                        _sqltrans.Commit();
                    }
                    catch
                    {
                        _sqltrans.Rollback();
                    }
                    finally
                    {
                        if (_sqlcon != null && _sqlcon.State != ConnectionState.Closed)
                            _sqlcon.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Add Multiple Admin Roles
        /// </summary>
        /// <param name="pEntListBaseAdminRole"></param>
        /// <returns></returns>
        public List<AdminRole> AddMultipleAdminRoles(List<AdminRole> pEntListBaseAdminRole)
        {
            List<AdminRole> entListAdminRoleReturn = new List<AdminRole>();
            List<AdminRoleFeatures> entListRoleFeatures = new List<AdminRoleFeatures>();
            _sqladapter = new SqlDataAdapter();
            _dset = new DataSet();
            _dtable = new DataTable();
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            string strSQL = string.Empty;
            int iBatchSize = 0;

            if (pEntListBaseAdminRole.Count > 0)
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntListBaseAdminRole[0].ClientId);
                _sqlcon = new SqlConnection(_strConnString);
                _dtable = new DataTable();
                _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);
                _dtable.Columns.Add(Schema.AdminRole.COL_ROLE_ID);
                _dtable.Columns.Add(Schema.Client.COL_CLIENT_ID);
                _dtable.Columns.Add(Schema.AdminRole.COL_IS_ACTIVE);
                _dtable.Columns.Add(Schema.AdminRole.COL_ROLE_DESC);
                _dtable.Columns.Add(Schema.AdminRole.COL_ROLE_NAME);
                _dtable.Columns.Add(Schema.AdminRole.COL_ROLE_TYPE);
                _dtable.Columns.Add(Schema.Common.COL_CREATED_BY);
                _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);

                _dtable.PrimaryKey = new DataColumn[] { _dtable.Columns[Schema.AdminRole.COL_ROLE_ID] };

                foreach (AdminRole entARole in pEntListBaseAdminRole)
                {

                    DataRow drow = _dtable.NewRow();
                    drow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_INSERT_MODE;
                    drow[Schema.AdminRole.COL_ROLE_ID] = entARole.ID;
                    drow[Schema.Client.COL_CLIENT_ID] = entARole.ClientId;
                    drow[Schema.AdminRole.COL_IS_ACTIVE] = entARole.IsActive;
                    drow[Schema.AdminRole.COL_ROLE_DESC] = entARole.RoleDescription;
                    drow[Schema.AdminRole.COL_ROLE_NAME] = entARole.RoleName;
                    drow[Schema.AdminRole.COL_ROLE_TYPE] = entARole.AdminRoleType.ToString();
                    drow[Schema.Common.COL_CREATED_BY] = entARole.CreatedById;
                    drow[Schema.Common.COL_MODIFIED_BY] = entARole.LastModifiedById;
                    _dtable.Rows.Add(drow);
                    iBatchSize = iBatchSize + 1;
                    entListRoleFeatures.AddRange(entARole.Features);
                    entListAdminRoleReturn.Add(entARole);
                }
            }
            if (_dtable.Rows.Count > 0)
            {
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.AdminRole.PROC_UPDATE_ROLE_MASTER;
                _sqlcmd.CommandType = CommandType.StoredProcedure;
                _sqlcmd.Connection = _sqlcon;

                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, SqlDbType.VarChar, 100, Schema.AdminRole.COL_ROLE_ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, SqlDbType.VarChar, 100, Schema.Client.COL_CLIENT_ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_IS_ACTIVE, SqlDbType.Bit, 10, Schema.AdminRole.COL_IS_ACTIVE);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_DESC, SqlDbType.VarChar, 500, Schema.AdminRole.COL_ROLE_DESC);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_NAME, SqlDbType.VarChar, 100, Schema.AdminRole.COL_ROLE_NAME);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_TYPE, SqlDbType.VarChar, 100, Schema.AdminRole.COL_ROLE_TYPE);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_CREATED_BY);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, SqlDbType.NVarChar, 10, Schema.Common.COL_UPDATE_MODE);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqladapter.InsertCommand = _sqlcmd;
                _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                _sqladapter.UpdateBatchSize = iBatchSize;
                _sqladapter.Update(_dtable);
                _sqladapter.Dispose();
                _dset = null;

                // To add admin features.
                AddMultipleAdminRoleFeatures(entListRoleFeatures);
            }
            return entListAdminRoleReturn;
        }

        /// <summary>
        /// Assign Role To User
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <returns></returns>
        public AdminRole AssignRoleToUser(AdminRole pEntAdminRole)
        {
            AdminRole entAdminRoleReturn = new AdminRole();
            _sqlreader = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAdminRole.ClientId);
                _sqlcon = new SqlConnection(_strConnString);
                _sqlcon.Open();
                using (TransactionScope ts = new TransactionScope())
                {
                    _sqladapter = new SqlDataAdapter();
                    _dtable = new DataTable();
                    int iBatchSize = 0;

                    if (pEntAdminRole.Users.Count > 0)
                    {
                        _dtable = new DataTable();
                        _dtable.Columns.Add(Schema.AdminRole.COL_ROLE_ID);
                        _dtable.Columns.Add(Schema.Learner.COL_USER_ID);
                        _dtable.Columns.Add(Schema.Learner.COL_UNIT_ID);
                        _dtable.Columns.Add(Schema.Learner.COL_LEVEL_ID);
                        _dtable.Columns.Add(Schema.CustomGroup.COL_CSG_ID);
                        _dtable.Columns.Add(Schema.RuleRoleScope.COL_RULE_ID);
                        _dtable.Columns.Add(Schema.RuleRoleScope.COL_IS_DEFAULT);
                        _dtable.Columns.Add(Schema.Common.COL_CREATED_BY);
                        _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                        _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);
                        _dtable.Columns.Add(Schema.RuleRoleScope.COL_IS_TCAAdminRights);

                        foreach (UserAdminRole entUserAdminRole in pEntAdminRole.Users)
                        {
                            DataRow drow = _dtable.NewRow();
                            drow[Schema.AdminRole.COL_ROLE_ID] = pEntAdminRole.ID;
                            //entUserAdminRole.ID is LearnerId
                            drow[Schema.Learner.COL_USER_ID] = entUserAdminRole.ID;
                            drow[Schema.Learner.COL_UNIT_ID] = entUserAdminRole.UnitId;
                            drow[Schema.Learner.COL_LEVEL_ID] = entUserAdminRole.LevelId;
                            drow[Schema.CustomGroup.COL_CSG_ID] = entUserAdminRole.CustomGroupId;
                            drow[Schema.RuleRoleScope.COL_RULE_ID] = entUserAdminRole.RuleId;
                            drow[Schema.RuleRoleScope.COL_IS_DEFAULT] = entUserAdminRole.IsDefaultScope;
                            drow[Schema.RuleRoleScope.COL_IS_TCAAdminRights] = pEntAdminRole.SelectedRuleRoleScope[0].IsTCAAdminRights;
                            drow[Schema.Common.COL_CREATED_BY] = entUserAdminRole.CreatedById;
                            drow[Schema.Common.COL_MODIFIED_BY] = entUserAdminRole.LastModifiedById;
                            if (entUserAdminRole.UserAction == DataAction.Insert)
                                drow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_INSERT_MODE;
                            else
                                drow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_DELETE_MODE;

                            _dtable.Rows.Add(drow);
                            iBatchSize = iBatchSize + 1;
                            if (entUserAdminRole.UserAction == DataAction.Insert)
                            {
                                entAdminRoleReturn.ID = entUserAdminRole.RoleId;
                                entAdminRoleReturn.Users.Add(entUserAdminRole);
                            }
                        }
                        if (_dtable.Rows.Count > 0)
                        {
                            _sqlcmd = new SqlCommand();
                            _sqlcmd.CommandText = Schema.AdminRole.PROC_UPDATE_USER_ADMIN_ROLE;
                            _sqlcmd.CommandType = CommandType.StoredProcedure;
                            _sqlcmd.Connection = _sqlcon;

                            _sqladapter.InsertCommand = _sqlcmd;
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AdminRole.PARA_ROLE_ID, SqlDbType.VarChar, 100, Schema.AdminRole.COL_ROLE_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Learner.PARA_USER_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_USER_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Learner.PARA_UNIT_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_UNIT_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Learner.PARA_LEVEL_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_LEVEL_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.CustomGroup.PARA_CSG_ID, SqlDbType.VarChar, 100, Schema.CustomGroup.COL_CSG_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.RuleRoleScope.PARA_RULE_ID, SqlDbType.VarChar, 100, Schema.RuleRoleScope.COL_RULE_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.RuleRoleScope.PARA_IS_DEFAULT, SqlDbType.Bit, 1, Schema.RuleRoleScope.COL_IS_DEFAULT);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_CREATED_BY, SqlDbType.NVarChar, 100, Schema.Common.COL_CREATED_BY);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.NVarChar, 100, Schema.Common.COL_MODIFIED_BY);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.RuleRoleScope.PARA_IsTCAAdminRights, SqlDbType.Bit, 1, Schema.RuleRoleScope.COL_IS_TCAAdminRights);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.NVarChar, 100, Schema.Common.COL_UPDATE_MODE);

                            _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                            _sqladapter.UpdateBatchSize = iBatchSize;
                            _sqladapter.Update(_dtable);

                        }
                    }
                    if (pEntAdminRole.SelectedRuleRoleScope != null && pEntAdminRole.SelectedRuleRoleScope.Count > 0)
                    {

                        foreach (RuleRoleScope entRuleRoleScopeLoop in pEntAdminRole.SelectedRuleRoleScope)
                        {
                            RuleRoleScope entRuleRoleScope = new RuleRoleScope();
                            entRuleRoleScope = entRuleRoleScopeLoop;

                            _sqlcmd = new SqlCommand();
                            _sqlcmd.CommandText = Schema.RuleRoleScope.PROC_UPDATE_RULE_ROLE_SCOPE;
                            _sqlcmd.CommandType = CommandType.StoredProcedure;
                            _sqlcmd.Connection = _sqlcon;
                            _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, entRuleRoleScope.RoleId);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_RULE_ID, entRuleRoleScope.RuleId);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            if (!string.IsNullOrEmpty(entRuleRoleScope.ScopeLevelId))
                                _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPELEVEL_ID, entRuleRoleScope.ScopeLevelId);
                            else
                                _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPELEVEL_ID, System.DBNull.Value);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            if (!string.IsNullOrEmpty(entRuleRoleScope.ScopeRuleId))
                                _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPE_RULE_ID, entRuleRoleScope.ScopeRuleId);
                            else
                                _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPE_RULE_ID, System.DBNull.Value);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            if (!string.IsNullOrEmpty(entRuleRoleScope.ScopeUnitId))
                                _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPEUNIT_ID, entRuleRoleScope.ScopeUnitId);
                            else
                                _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPEUNIT_ID, System.DBNull.Value);
                            _sqlcmd.Parameters.Add(_sqlpara);

                            _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_IS_DEFAULT_SCOPE, entRuleRoleScope.IsDefaultScope);
                            _sqlcmd.Parameters.Add(_sqlpara);

                            //_sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_IsTCAAdminRights, entRuleRoleScope.IsTCAAdminRights);
                            //_sqlcmd.Parameters.Add(_sqlpara);

                            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, entRuleRoleScope.LastModifiedById);
                            _sqlcmd.Parameters.Add(_sqlpara);

                            if (entRuleRoleScope.UserAction == DataAction.Insert)
                                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                            else
                                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_DELETE_MODE);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, true);
                            while (_sqlreader.Read())
                            {
                                entAdminRoleReturn.Users.Add(FillUserObject(_sqlreader));
                            }

                            _sqlreader.Close();
                        }
                    }
                    ts.Complete();
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
                if (_sqlcon != null && _sqlcon.State != ConnectionState.Closed)
                    _sqlcon.Close();
            }
            return entAdminRoleReturn;
        }

        /// <summary>
        /// Fill User Object
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <returns></returns>
        private UserAdminRole FillUserObject(SqlDataReader pSqlreader)
        {
            _entUserAdminRole = new UserAdminRole();
            int index;
            if (pSqlreader.HasRows)
            {
                index = pSqlreader.GetOrdinal(Schema.Learner.COL_USER_ID);
                if (!pSqlreader.IsDBNull(index))
                    _entUserAdminRole.ID = pSqlreader.GetString(index);
            }
            return _entUserAdminRole;
        }

        /// <summary>
        /// All UnAssign Role To User
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <returns></returns>
        public AdminRole AllUnAssignRoleToUser(AdminRole pEntAdminRole)
        {
            AdminRole entAdminRoleReturn = new AdminRole();
            _sqlreader = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAdminRole.ClientId);
                _sqlcon = new SqlConnection(_strConnString);
                _sqlcon.Open();
                using (TransactionScope ts = new TransactionScope())
                {
                    _sqladapter = new SqlDataAdapter();
                    _dtable = new DataTable();
                    int iBatchSize = 0;

                    if (pEntAdminRole.Users.Count > 0)
                    {
                        _dtable = new DataTable();
                        _dtable.Columns.Add(Schema.AdminRole.COL_ROLE_ID);
                        _dtable.Columns.Add(Schema.Learner.COL_USER_ID);
                        _dtable.Columns.Add(Schema.Learner.COL_UNIT_ID);
                        _dtable.Columns.Add(Schema.Learner.COL_LEVEL_ID);
                        _dtable.Columns.Add(Schema.CustomGroup.COL_CSG_ID);
                        _dtable.Columns.Add(Schema.RuleRoleScope.COL_RULE_ID);
                        _dtable.Columns.Add(Schema.RuleRoleScope.COL_IS_DEFAULT);
                        _dtable.Columns.Add(Schema.Common.COL_CREATED_BY);
                        _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                        _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                        foreach (UserAdminRole entUserAdminRole in pEntAdminRole.Users)
                        {
                            DataRow drow = _dtable.NewRow();
                            drow[Schema.AdminRole.COL_ROLE_ID] = entUserAdminRole.RoleId;
                            //entUserAdminRole.ID is LearnerId
                            drow[Schema.Learner.COL_USER_ID] = entUserAdminRole.ID;
                            drow[Schema.Learner.COL_UNIT_ID] = entUserAdminRole.UnitId;
                            drow[Schema.Learner.COL_LEVEL_ID] = entUserAdminRole.LevelId;
                            drow[Schema.CustomGroup.COL_CSG_ID] = entUserAdminRole.CustomGroupId;
                            drow[Schema.RuleRoleScope.COL_RULE_ID] = entUserAdminRole.RuleId;
                            drow[Schema.RuleRoleScope.COL_IS_DEFAULT] = entUserAdminRole.IsDefaultScope;
                            drow[Schema.Common.COL_CREATED_BY] = entUserAdminRole.CreatedById;
                            drow[Schema.Common.COL_MODIFIED_BY] = entUserAdminRole.LastModifiedById;
                            if (entUserAdminRole.UserAction == DataAction.Insert)
                                drow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_INSERT_MODE;
                            else
                                drow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_DELETE_MODE;

                            _dtable.Rows.Add(drow);
                            iBatchSize = iBatchSize + 1;
                            if (entUserAdminRole.UserAction == DataAction.Insert)
                            {
                                entAdminRoleReturn.ID = entUserAdminRole.RoleId;
                                entAdminRoleReturn.Users.Add(entUserAdminRole);
                            }
                        }
                        if (_dtable.Rows.Count > 0)
                        {
                            _sqlcmd = new SqlCommand();
                            _sqlcmd.CommandText = Schema.AdminRole.PROC_UPDATE_USER_ADMIN_ROLE;
                            _sqlcmd.CommandType = CommandType.StoredProcedure;
                            _sqlcmd.Connection = _sqlcon;

                            _sqladapter.InsertCommand = _sqlcmd;
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AdminRole.PARA_ROLE_ID, SqlDbType.VarChar, 100, Schema.AdminRole.COL_ROLE_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Learner.PARA_USER_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_USER_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Learner.PARA_UNIT_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_UNIT_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Learner.PARA_LEVEL_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_LEVEL_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.CustomGroup.PARA_CSG_ID, SqlDbType.VarChar, 100, Schema.CustomGroup.COL_CSG_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.RuleRoleScope.PARA_RULE_ID, SqlDbType.VarChar, 100, Schema.RuleRoleScope.COL_RULE_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.RuleRoleScope.PARA_IS_DEFAULT, SqlDbType.Bit, 1, Schema.RuleRoleScope.COL_IS_DEFAULT);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_CREATED_BY, SqlDbType.NVarChar, 100, Schema.Common.COL_CREATED_BY);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.NVarChar, 100, Schema.Common.COL_MODIFIED_BY);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.NVarChar, 100, Schema.Common.COL_UPDATE_MODE);

                            _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                            _sqladapter.UpdateBatchSize = iBatchSize;
                            _sqladapter.Update(_dtable);

                        }
                    }
                    if (pEntAdminRole.SelectedRuleRoleScope != null && pEntAdminRole.SelectedRuleRoleScope.Count > 0)
                    {

                        foreach (RuleRoleScope entRuleRoleScopeLoop in pEntAdminRole.SelectedRuleRoleScope)
                        {
                            RuleRoleScope entRuleRoleScope = new RuleRoleScope();
                            entRuleRoleScope = entRuleRoleScopeLoop;

                            _sqlcmd = new SqlCommand();
                            _sqlcmd.CommandText = Schema.RuleRoleScope.PROC_UPDATE_RULE_ROLE_SCOPE;
                            _sqlcmd.CommandType = CommandType.StoredProcedure;
                            _sqlcmd.Connection = _sqlcon;
                            _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, entRuleRoleScope.RoleId);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_RULE_ID, entRuleRoleScope.RuleId);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            if (!string.IsNullOrEmpty(entRuleRoleScope.ScopeLevelId))
                                _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPELEVEL_ID, entRuleRoleScope.ScopeLevelId);
                            else
                                _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPELEVEL_ID, System.DBNull.Value);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            if (!string.IsNullOrEmpty(entRuleRoleScope.ScopeRuleId))
                                _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPE_RULE_ID, entRuleRoleScope.ScopeRuleId);
                            else
                                _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPE_RULE_ID, System.DBNull.Value);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            if (!string.IsNullOrEmpty(entRuleRoleScope.ScopeUnitId))
                                _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPEUNIT_ID, entRuleRoleScope.ScopeUnitId);
                            else
                                _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_SCOPEUNIT_ID, System.DBNull.Value);
                            _sqlcmd.Parameters.Add(_sqlpara);

                            _sqlpara = new SqlParameter(Schema.RuleRoleScope.PARA_IS_DEFAULT_SCOPE, entRuleRoleScope.IsDefaultScope);
                            _sqlcmd.Parameters.Add(_sqlpara);

                            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, entRuleRoleScope.LastModifiedById);
                            _sqlcmd.Parameters.Add(_sqlpara);

                            if (entRuleRoleScope.UserAction == DataAction.Insert)
                                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                            else
                                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_DELETE_MODE);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, true);
                            while (_sqlreader.Read())
                            {
                                entAdminRoleReturn.Users.Add(FillUserObject(_sqlreader));
                            }

                            _sqlreader.Close();
                        }
                    }
                    ts.Complete();
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
                if (_sqlcon != null && _sqlcon.State != ConnectionState.Closed)
                    _sqlcon.Close();
            }
            return entAdminRoleReturn;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <returns></returns>
        public AdminRole Get(AdminRole pEntAdminRole)
        {
            return GetAdminRoleByID(pEntAdminRole);
        }
        /// <summary>
        /// Update AdminRole
        /// </summary>
        /// <param name="pEntAdminRole"></param>
        /// <returns></returns>
        public AdminRole Update(AdminRole pEntAdminRole)
        {
            return EditAdminRole(pEntAdminRole);
        }
        #endregion
    }
}
