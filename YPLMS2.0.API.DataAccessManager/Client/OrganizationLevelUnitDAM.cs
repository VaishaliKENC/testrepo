using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;
using NPOI.SS.Formula.Functions;

namespace YPLMS2._0.API.DataAccessManager
{
    public class OrganizationLevelUnitDAM : IDataManager<OrganizationLevelUnit>, IOrganizationLevelUnitDAM<OrganizationLevelUnit>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.Client.ORG_UNIT_DL_ERROR;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// Get Organization Units By ID
        /// </summary>
        /// <param name="pEntObjOrganizationUnit"></param>
        /// <returns>OrganizationLevelUnit Object</returns>
        public OrganizationLevelUnit GetOrganizationUnitsByID(OrganizationLevelUnit pEntObjOrganizationUnit)
        {
            _sqlObject = new SQLObject();
            OrganizationLevelUnit entOrgUnit = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntObjOrganizationUnit.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.OrganizationLevelUnit.PROC_GET_ORGANIZATION_LEVEL_UNIT_MASTER_ID, sqlConnection);
                _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_UNIT_ID, pEntObjOrganizationUnit.ID.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entOrgUnit = FillObject(_sqlreader);
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
            return entOrgUnit;
        }

        /// <summary>
        /// To get unit by name
        /// </summary>
        /// <param name="pEntObjOrganizationUnit"></param>
        /// <returns></returns>
        public OrganizationLevelUnit GetOrganizationUnitByName(OrganizationLevelUnit pEntObjOrganizationUnit)
        {
            _sqlObject = new SQLObject();
            OrganizationLevelUnit entOrgUnit = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntObjOrganizationUnit.ClientId);

                sqlConnection = new SqlConnection(_strConnString);

                _sqlcmd = new SqlCommand(Schema.OrganizationLevelUnit.PROC_GET_ORG_LEVELUNIT_BYNAME, sqlConnection);

                _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_UNIT_NAME, pEntObjOrganizationUnit.UnitName);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_UNIT_PARENT_ID, pEntObjOrganizationUnit.ParentUnitId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                if (_sqlreader.HasRows)
                    entOrgUnit = FillObject(_sqlreader);
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
            return entOrgUnit;
        }

        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>OrganizationLevelUnit Object</returns>
        private OrganizationLevelUnit FillObject(SqlDataReader pReader)
        {
            OrganizationLevelUnit entOrganizationLevelUnit = new OrganizationLevelUnit();
            int iIndex;
            while (pReader.Read())
            {
                iIndex = pReader.GetOrdinal(Schema.OrganizationLevelUnit.COL_UNIT_ID);
                entOrganizationLevelUnit.ID = pReader.GetString(iIndex);
                entOrganizationLevelUnit.UnitName = pReader[Schema.OrganizationLevelUnit.COL_UNIT_NAME].ToString();
                entOrganizationLevelUnit.SequenceOrder = Convert.ToInt32(pReader[Schema.OrganizationLevelUnit.COL_UNIT_ORDER]);
                entOrganizationLevelUnit.ClientId = pReader[Schema.OrganizationLevelUnit.COL_UNIT_CLIENT_ID].ToString();
                entOrganizationLevelUnit.LevelId = pReader[Schema.OrganizationLevelUnit.COL_LEVEL_ID].ToString();
                entOrganizationLevelUnit.ParentUnitId = pReader[Schema.OrganizationLevelUnit.COL_UNIT_PARENT_ID].ToString();
                entOrganizationLevelUnit.CreatedById = pReader[Schema.Common.COL_CREATED_BY].ToString();
                entOrganizationLevelUnit.LastModifiedById = pReader[Schema.Common.COL_CREATED_BY].ToString();
                entOrganizationLevelUnit.LastModifiedDate = Convert.ToDateTime(pReader[Schema.Common.COL_MODIFIED_ON]);
                entOrganizationLevelUnit.DateCreated = Convert.ToDateTime(pReader[Schema.Common.COL_CREATED_ON]);
                entOrganizationLevelUnit.IsActive = Convert.ToBoolean(pReader[Schema.OrganizationLevelUnit.COL_UNIT_ISACTIVE]);
                entOrganizationLevelUnit.OrgTreeUnitId = Convert.ToInt32(pReader[Schema.OrganizationLevelUnit.COL_ORGTREE_UNIT_ID]);
                entOrganizationLevelUnit.OrgTreeParentUnitId = Convert.ToInt32(pReader[Schema.OrganizationLevelUnit.COL_ORGTREE_PARENT_UNIT_ID]);
            }
            return entOrganizationLevelUnit;
        }

        /// <summary>
        /// Fill Reader Object For Get All
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>OrganizationLevelUnit object</returns>
        private OrganizationLevelUnit FillObjectForGetall(SqlDataReader pReader, SQLObject pSqlObject)
        {
            OrganizationLevelUnit entOrganizationLevelUnit = new OrganizationLevelUnit();
            EntityRange entRange = new EntityRange();
            int iIndex;
            if (pReader.HasRows)
            {
                iIndex = pReader.GetOrdinal(Schema.OrganizationLevelUnit.COL_UNIT_ID);
                entOrganizationLevelUnit.ID = pReader.GetString(iIndex);
                entOrganizationLevelUnit.UnitName = pReader[Schema.OrganizationLevelUnit.COL_UNIT_NAME].ToString();
                entOrganizationLevelUnit.SequenceOrder = Convert.ToInt32(pReader[Schema.OrganizationLevelUnit.COL_UNIT_ORDER]);
                entOrganizationLevelUnit.ClientId = pReader[Schema.OrganizationLevelUnit.COL_UNIT_CLIENT_ID].ToString();
                entOrganizationLevelUnit.LevelId = pReader[Schema.OrganizationLevelUnit.COL_LEVEL_ID].ToString();
                entOrganizationLevelUnit.ParentUnitId = pReader[Schema.OrganizationLevelUnit.COL_UNIT_PARENT_ID].ToString();
                entOrganizationLevelUnit.CreatedById = pReader[Schema.Common.COL_CREATED_BY].ToString();
                entOrganizationLevelUnit.LastModifiedById = pReader[Schema.Common.COL_CREATED_BY].ToString();
                entOrganizationLevelUnit.LastModifiedDate = Convert.ToDateTime(pReader[Schema.Common.COL_MODIFIED_ON]);
                entOrganizationLevelUnit.DateCreated = Convert.ToDateTime(pReader[Schema.Common.COL_CREATED_ON]);
                entOrganizationLevelUnit.IsActive = Convert.ToBoolean(pReader[Schema.OrganizationLevelUnit.COL_UNIT_ISACTIVE]);
                entOrganizationLevelUnit.OrgTreeUnitId = Convert.ToInt32(pReader[Schema.OrganizationLevelUnit.COL_ORGTREE_UNIT_ID]);
                entOrganizationLevelUnit.OrgTreeParentUnitId = Convert.ToInt32(pReader[Schema.OrganizationLevelUnit.COL_ORGTREE_PARENT_UNIT_ID]);

                if (pSqlObject.ReaderHasColumn(pReader, Schema.Common.COL_IS_USED))
                    entOrganizationLevelUnit.IsUsed = Convert.ToBoolean(pReader[Schema.Common.COL_IS_USED]);

                if (pSqlObject.ReaderHasColumn(pReader, Schema.Common.COL_CHILD_COUNT))
                    entOrganizationLevelUnit.ChildCount = Convert.ToInt32(pReader[Schema.Common.COL_CHILD_COUNT]);

                if (pSqlObject.ReaderHasColumn(pReader, Schema.OrganizationLevelUnit.COL_PARENT_UNITS))
                    entOrganizationLevelUnit.ParentUnits = Convert.ToString(pReader[Schema.OrganizationLevelUnit.COL_PARENT_UNITS]);

                if (pSqlObject.ReaderHasColumn(pReader, Schema.OrganizationLevelUnit.COL_CHILD_UNITS))
                    entOrganizationLevelUnit.ChildUnits = Convert.ToString(pReader[Schema.OrganizationLevelUnit.COL_CHILD_UNITS]);

                if (pSqlObject.ReaderHasColumn(pReader, Schema.OrganizationLevel.COL_LEVEL_NAME))
                    entOrganizationLevelUnit.LevelName = Convert.ToString(pReader[Schema.OrganizationLevel.COL_LEVEL_NAME]);

                if (pSqlObject.ReaderHasColumn(pReader, Schema.Common.COL_TOTAL_COUNT))
                {
                    entRange = new EntityRange();
                    iIndex = pReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pReader.IsDBNull(iIndex))
                        entRange.TotalRows = pReader.GetInt32(iIndex);
                    entOrganizationLevelUnit.ListRange = entRange;
                }
            }
            return entOrganizationLevelUnit;
        }

        /// <summary>
        /// Add Organization Level Unit
        /// </summary>
        /// <param name="pEntOrgUnit"></param>
        /// <returns>OrganizationLevelUnit Object</returns>
        public OrganizationLevelUnit AddOrganizationUnit(OrganizationLevelUnit pEntOrgUnit)
        {
            try
            {
                pEntOrgUnit = UpdateOrganizationLevelUnit(pEntOrgUnit, Schema.Common.VAL_INSERT_MODE);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntOrgUnit;
        }

        /// <summary>
        /// Update Organization Level Unit
        /// </summary>
        /// <param name="pEntOrgUnit"></param>
        /// <returns>OrganizationLevelUnit Object</returns>
        public OrganizationLevelUnit EditOrganizationUnit(OrganizationLevelUnit pEntOrgUnit)
        {
            try
            {
                pEntOrgUnit = UpdateOrganizationLevelUnit(pEntOrgUnit, Schema.Common.VAL_UPDATE_MODE);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntOrgUnit;
        }

        /// <summary>
        /// Get All Organization Level Unit
        /// </summary>
        /// <param name="pEntOrgUnit"></param>
        /// <returns>List of OrganizationLevelUnit Objects</returns>
        public List<OrganizationLevelUnit> GetAllUnits(OrganizationLevelUnit pEntOrgUnit)
        {
            _sqlObject = new SQLObject();
            List<OrganizationLevelUnit> entListOrgUnits = new List<OrganizationLevelUnit>();
            OrganizationLevelUnit entOrgUnit;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntOrgUnit.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.OrganizationLevelUnit.PROC_GET_ALL_ORGANIZATION_LEVEL_UNITS, sqlConnection);

                if (pEntOrgUnit.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntOrgUnit.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntOrgUnit.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntOrgUnit.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntOrgUnit.ListRange.RequestedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntOrgUnit.ListRange.RequestedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                _sqlcmd.CommandTimeout = 0;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListOrgUnits = new List<OrganizationLevelUnit>();
                while (_sqlreader.Read())
                {
                    entOrgUnit = FillObjectForGetall(_sqlreader, _sqlObject);
                    entListOrgUnits.Add(entOrgUnit);
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
            return entListOrgUnits;
        }

        /// <summary>
        /// Get All Organization Level Unit
        /// </summary>
        /// <param name="pEntOrgUnit"></param>
        /// <returns>List of OrganizationLevelUnit Objects</returns>
        public List<OrganizationLevelUnit> GetAllUnits_ForImport(OrganizationLevelUnit pEntOrgUnit)
        {
            _sqlObject = new SQLObject();
            List<OrganizationLevelUnit> entListOrgUnits = new List<OrganizationLevelUnit>();
            OrganizationLevelUnit entOrgUnit;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntOrgUnit.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.OrganizationLevelUnit.PROC_GET_ALL_ORGANIZATION_LEVEL_UNITS_FOR_IMPORT, sqlConnection);
                if (pEntOrgUnit.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntOrgUnit.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntOrgUnit.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntOrgUnit.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntOrgUnit.ListRange.RequestedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntOrgUnit.ListRange.RequestedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListOrgUnits = new List<OrganizationLevelUnit>();
                while (_sqlreader.Read())
                {
                    entOrgUnit = FillObjectForGetall(_sqlreader, _sqlObject);
                    entListOrgUnits.Add(entOrgUnit);
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
            return entListOrgUnits;
        }

        /// <summary>
        /// To find level units
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<OrganizationLevelUnit> FindAllUnits(Search pEntSearch)
        {
            _sqlObject = new SQLObject();
            List<OrganizationLevelUnit> entListOrgUnits = new List<OrganizationLevelUnit>();
            OrganizationLevelUnit entOrgUnit;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                _sqlcmd = new SqlCommand(Schema.OrganizationLevelUnit.PROC_FIND_ALL_UNITS, sqlConnection);

                if (!string.IsNullOrEmpty(pEntSearch.KeyWord))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntSearch.KeyWord);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntSearch.ID))
                {
                    _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_LEVEL_ID, pEntSearch.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
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
                /* below line added for time out issue.*/
                _sqlcmd.CommandTimeout = 0;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListOrgUnits = new List<OrganizationLevelUnit>();
                while (_sqlreader.Read())
                {
                    entOrgUnit = FillObjectForGetall(_sqlreader, _sqlObject);
                    entListOrgUnits.Add(entOrgUnit);
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
            return entListOrgUnits;
        }

        /// <summary>
        /// Update Organization Level Unit
        /// </summary>
        /// <param name="pEntOrgLevelUnit"></param>
        /// <param name="pUpdateMode"></param>
        /// <returns>OrganizationLevelUnit object</returns>
        public OrganizationLevelUnit UpdateOrganizationLevelUnit(OrganizationLevelUnit pEntOrgLevelUnit, string pUpdateMode)
        {
            _sqlObject = new SQLObject();
            int iUpdateStatus = 0;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.OrganizationLevelUnit.PROC_UPDATE_ORGANIZATION_LEVEL_UNIT;
            _strConnString = _sqlObject.GetClientDBConnString(pEntOrgLevelUnit.ClientId);
            if (pUpdateMode == Schema.Common.VAL_INSERT_MODE)
            {
                pEntOrgLevelUnit.ID = YPLMS.Services.IDGenerator.GetStringGUID();
            }
            _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_UNIT_ID, pEntOrgLevelUnit.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_UNIT_NAME, pEntOrgLevelUnit.UnitName);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_UNIT_ORDER, pEntOrgLevelUnit.SequenceOrder);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_LEVEL_ID, pEntOrgLevelUnit.LevelId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntOrgLevelUnit.CreatedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_CLIENT_ID, pEntOrgLevelUnit.ClientId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_UNIT_PARENT_ID, pEntOrgLevelUnit.ParentUnitId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_UNIT_ISACTIVE, pEntOrgLevelUnit.IsActive);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_ORGTREE_PARENT_UNIT_ID, pEntOrgLevelUnit.OrgTreeParentUnitId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntOrgLevelUnit.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, pUpdateMode);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_ORGTREE_UNIT_ID, SqlDbType.Int, 4);
            _sqlpara.Direction = ParameterDirection.Output;
            _sqlcmd.Parameters.Add(_sqlpara);

            iUpdateStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            pEntOrgLevelUnit.OrgTreeUnitId = Convert.ToInt32(_sqlpara.Value);

            return pEntOrgLevelUnit;
        }
        /// <summary>
        /// Reset Organization Level Unit
        /// </summary>
        /// <returns>True - successful reset, False - unsuccessfull</returns>
        public bool ResetOrganizationLevelUnit(string ClientId)
        {
            _sqlObject = new SQLObject();
            int iUpdateStatus = 0;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.OrganizationLevelUnit.PROC_UPDATE_ORGANIZATION_LEVEL_UNIT_RESET;
            _strConnString = _sqlObject.GetClientDBConnString(ClientId);

            iUpdateStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            if (iUpdateStatus < 0) return false;
            return true;
        }
        /// <summary>
        /// Check OrganiztionUnit By UnitName & ParentUnitId add if not exists
        /// </summary>
        /// <param name="pEntOrgLevelUnit"></param>
        /// <returns>OrganizationLevelUnit</returns>
        public OrganizationLevelUnit CheckandAddOrganizationLevelUnit(OrganizationLevelUnit pEntOrgLevelUnit)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            OrganizationLevelUnit entOrgUnit = new OrganizationLevelUnit();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntOrgLevelUnit.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.OrganizationLevelUnit.PROC_CAA_ORGANIZATION_UNIT, sqlConnection);

                pEntOrgLevelUnit.ID = YPLMS.Services.IDGenerator.GetStringGUID();

                _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_UNIT_ID, pEntOrgLevelUnit.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_UNIT_NAME, pEntOrgLevelUnit.UnitName);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_UNIT_ORDER, pEntOrgLevelUnit.SequenceOrder);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_LEVEL_ID, pEntOrgLevelUnit.LevelId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntOrgLevelUnit.CreatedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_CLIENT_ID, pEntOrgLevelUnit.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_UNIT_PARENT_ID, pEntOrgLevelUnit.ParentUnitId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_UNIT_ISACTIVE, pEntOrgLevelUnit.IsActive);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_ORGTREE_PARENT_UNIT_ID, pEntOrgLevelUnit.OrgTreeParentUnitId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntOrgLevelUnit.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_ORGTREE_UNIT_ID, SqlDbType.Int, 4);
                _sqlpara.Direction = ParameterDirection.Output;
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entOrgUnit = FillObjectForGetall(_sqlreader, _sqlObject);
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

            /*
             Change: actual result set object return.
             Date:18-Feb-2010
             By;Fattesinh
             */
            //return pEntOrgLevelUnit;
            return entOrgUnit;
        }

        /// <summary>
        /// Delete Organization Level Unit
        /// </summary>
        /// <param name="pEntOrgLevelUnit"></param>
        /// <returns>OrganizationLevelUnit object</returns>
        public OrganizationLevelUnit DeleteOrganizationLevelUnit(OrganizationLevelUnit pEntOrgLevelUnit)
        {
            _sqlObject = new SQLObject();
            EntityRange entRange = new EntityRange();

            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.OrganizationLevelUnit.PROC_DELETE_ORGANIZATION_LEVEL_UNIT;
            SqlParameter sqlpara = new SqlParameter(Schema.OrganizationLevelUnit.PARA_UNIT_IDS, pEntOrgLevelUnit.ID);
            _sqlcmd.Parameters.Add(sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntOrgLevelUnit.ClientId);
                entRange.TotalRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                pEntOrgLevelUnit.ListRange = entRange;
                return pEntOrgLevelUnit;
                /*
                if (iDelStatus < 0)
                    return pEntOrgLevelUnit;
                else
                    return null;
                 */
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntOrganizationLevelUnit"></param>
        /// <returns></returns>
        public OrganizationLevelUnit Get(OrganizationLevelUnit pEntOrganizationLevelUnit)
        {
            return GetOrganizationUnitsByID(pEntOrganizationLevelUnit);
        }
        /// <summary>
        /// Update OrganizationLevelUnit
        /// </summary>
        /// <param name="pEntOrganizationLevelUnit"></param>
        /// <returns>OrganizationLevelUnit</returns>
        public OrganizationLevelUnit Update(OrganizationLevelUnit pEntOrganizationLevelUnit)
        {
            return EditOrganizationUnit(pEntOrganizationLevelUnit);
        }
        /// <summary>
        /// Reset OrganizationLevelUnit
        /// </summary>
        /// <returns>True - successful reset, False - unsuccessfull</returns>
        public bool Reset(string ClientId)
        {
            return ResetOrganizationLevelUnit(ClientId);
        }
        #endregion
    }
}
