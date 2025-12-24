using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;
using NPOI.SS.Formula.Functions;

namespace YPLMS2._0.API.DataAccessManager
{
    public class OrganizationLevelDAM : IDataManager<OrganizationLevel>, IOrganizationLevelDAM<OrganizationLevel>
    {
        #region Declaration
        string _strMessageId = YPLMS.Services.Messages.Client.ORG_LVL_DL_ERROR;
        string _strConnString = string.Empty;
        SQLObject _sqlObject = null;
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        #endregion

        /// <summary>
        /// Get Organization Levels By ID
        /// </summary>
        /// <param name="pEntOrganizationLevel"></param>
        /// <returns>OrganizationLevel Object</returns>
        public OrganizationLevel GetOrganizationLevelsByID(OrganizationLevel pEntOrganizationLevel)
        {
            _sqlObject = new SQLObject();
            OrganizationLevel entOrganizationLevel = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntOrganizationLevel.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.OrganizationLevel.PROC_GET_ORGANIZATION_LEVEL_MASTER_ID, sqlConnection);

                _sqlpara = new SqlParameter(Schema.OrganizationLevel.PARA_LEVEL_ID, pEntOrganizationLevel.ID.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entOrganizationLevel = FillObject(_sqlreader);
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
            return entOrganizationLevel;
        }

        /// <summary>
        /// Fill reader object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>OrganizationLevel Object</returns>
        private OrganizationLevel FillObject(SqlDataReader pSqlReader)
        {
            OrganizationLevel entOrgLevel = new OrganizationLevel();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                while (pSqlReader.Read())
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.OrganizationLevel.COL_LEVEL_ID);
                    entOrgLevel.ID = pSqlReader.GetString(iIndex);
                    entOrgLevel.LevelName = pSqlReader[Schema.OrganizationLevel.COL_LEVEL_NAME].ToString();
                    entOrgLevel.LevelOrder = Convert.ToInt32(pSqlReader[Schema.OrganizationLevel.COL_LEVEL_ORDER]);
                    entOrgLevel.ClientId = pSqlReader[Schema.OrganizationLevel.COL_CLIENT_ID].ToString();
                    entOrgLevel.CreatedById = pSqlReader[Schema.Common.COL_CREATED_BY].ToString();
                    entOrgLevel.DateCreated = Convert.ToDateTime(pSqlReader[Schema.Common.COL_CREATED_ON]);
                    entOrgLevel.LastModifiedDate = Convert.ToDateTime(pSqlReader[Schema.Common.COL_MODIFIED_ON].ToString());
                    entOrgLevel.LastModifiedById = pSqlReader[Schema.Common.COL_MODIFIED_BY].ToString();
                    entOrgLevel.OrganizationUnits = GetAllLevelUnits(entOrgLevel);
                }
            }
            return entOrgLevel;
        }

        /// <summary>
        /// Fill reader object for Get All
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>OrganizationLevel Object</returns>
        private OrganizationLevel FillObjectForGetAll(SqlDataReader pReader, SQLObject pSqlObject)
        {
            OrganizationLevel entOrgLevel = new OrganizationLevel();
            EntityRange entRange = new EntityRange();
            int iIndex;
            if (pReader.HasRows)
            {
                iIndex = pReader.GetOrdinal(Schema.OrganizationLevel.COL_LEVEL_ID);
                entOrgLevel.ID = pReader.GetString(iIndex);
                entOrgLevel.LevelName = pReader[Schema.OrganizationLevel.COL_LEVEL_NAME].ToString();
                entOrgLevel.LevelOrder = Convert.ToInt32(pReader[Schema.OrganizationLevel.COL_LEVEL_ORDER]);
                entOrgLevel.ClientId = pReader[Schema.OrganizationLevel.COL_CLIENT_ID].ToString();
                entOrgLevel.CreatedById = pReader[Schema.Common.COL_CREATED_BY].ToString();
                entOrgLevel.DateCreated = Convert.ToDateTime(pReader[Schema.Common.COL_CREATED_ON]);
                entOrgLevel.LastModifiedDate = Convert.ToDateTime(pReader[Schema.Common.COL_MODIFIED_ON].ToString());
                entOrgLevel.LastModifiedById = pReader[Schema.Common.COL_MODIFIED_BY].ToString();

                if (pSqlObject.ReaderHasColumn(pReader, Schema.Common.COL_IS_USED))
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(pReader[Schema.Common.COL_IS_USED])))
                        entOrgLevel.IsUsed = Convert.ToBoolean(pReader[Schema.Common.COL_IS_USED]);
                }
                entOrgLevel.OrganizationUnits = GetAllLevelUnits(entOrgLevel);

                if (pSqlObject.ReaderHasColumn(pReader, Schema.Common.COL_TOTAL_COUNT))
                {
                    iIndex = pReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pReader.IsDBNull(iIndex))
                    {
                        if (pReader.GetInt32(iIndex) > 0)
                        {
                            entRange = new EntityRange();
                            entRange.TotalRows = pReader.GetInt32(iIndex);
                            entOrgLevel.ListRange = entRange;

                        }
                    }
                }

            }
            return entOrgLevel;
        }

        /// <summary>
        /// Fill reader object for Get All - Without Unit
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>OrganizationLevel Object</returns>
        private OrganizationLevel FillObjectOnlyLevels(SqlDataReader pReader, SQLObject pSqlObject)
        {
            OrganizationLevel entOrgLevel = new OrganizationLevel();
            EntityRange entRange = new EntityRange();
            int iIndex;
            if (pReader.HasRows)
            {
                iIndex = pReader.GetOrdinal(Schema.OrganizationLevel.COL_LEVEL_ID);
                entOrgLevel.ID = pReader.GetString(iIndex);
                entOrgLevel.LevelName = pReader[Schema.OrganizationLevel.COL_LEVEL_NAME].ToString();
                entOrgLevel.LevelOrder = Convert.ToInt32(pReader[Schema.OrganizationLevel.COL_LEVEL_ORDER]);
                entOrgLevel.ClientId = pReader[Schema.OrganizationLevel.COL_CLIENT_ID].ToString();
                entOrgLevel.CreatedById = pReader[Schema.Common.COL_CREATED_BY].ToString();
                entOrgLevel.DateCreated = Convert.ToDateTime(pReader[Schema.Common.COL_CREATED_ON]);
                entOrgLevel.LastModifiedDate = Convert.ToDateTime(pReader[Schema.Common.COL_MODIFIED_ON].ToString());
                entOrgLevel.LastModifiedById = pReader[Schema.Common.COL_MODIFIED_BY].ToString();

                if (pSqlObject.ReaderHasColumn(pReader, Schema.Common.COL_IS_USED))
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(pReader[Schema.Common.COL_IS_USED])))
                        entOrgLevel.IsUsed = Convert.ToBoolean(pReader[Schema.Common.COL_IS_USED]);
                }

                if (pSqlObject.ReaderHasColumn(pReader, Schema.Common.COL_TOTAL_COUNT))
                {
                    iIndex = pReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pReader.IsDBNull(iIndex))
                    {
                        if (pReader.GetInt32(iIndex) > 0)
                        {
                            entRange = new EntityRange();
                            entRange.TotalRows = pReader.GetInt32(iIndex);
                            entOrgLevel.ListRange = entRange;
                        }
                    }
                }

            }
            return entOrgLevel;
        }

        /// <summary>
        /// Add Organization Level
        /// </summary>
        /// <param name="pEntOrgLevel"></param>
        /// <returns>OrganizationLevel Object</returns>
        public OrganizationLevel AddOrganizationLevel(OrganizationLevel pEntOrgLevel)
        {
            OrganizationLevel entOrgBaseLevel;
            List<OrganizationLevel> entListOrgLevels;
            OrganizationLevelUnit entOrgBaseLevelUnit;
            OrganizationLevelUnitDAM entOrgBaseLevelUnitDAM;
            try
            {
                entListOrgLevels = GetAllLevels(pEntOrgLevel);
                if (entListOrgLevels.Count == 0)
                {
                    entOrgBaseLevel = new OrganizationLevel();
                    entOrgBaseLevel.LevelName = Schema.Common.VAL_ORGNIZATION_BASE_LEVEL_NAME;
                    entOrgBaseLevel.LevelOrder = 0;
                    entOrgBaseLevel.ClientId = pEntOrgLevel.ClientId;
                    entOrgBaseLevel.CreatedById = pEntOrgLevel.ClientId;
                    entOrgBaseLevel.LastModifiedById = pEntOrgLevel.ClientId;
                    entOrgBaseLevel = UpdateOrganizationLevel(entOrgBaseLevel, Schema.Common.VAL_INSERT_MODE);

                    entOrgBaseLevelUnit = new OrganizationLevelUnit();
                    entOrgBaseLevelUnitDAM = new OrganizationLevelUnitDAM();
                    entOrgBaseLevelUnit.UnitName = Schema.Common.VAL_ORGNIZATION_BASE_UNIT_NAME;
                    entOrgBaseLevelUnit.SequenceOrder = 0;
                    entOrgBaseLevelUnit.ClientId = pEntOrgLevel.ClientId;
                    entOrgBaseLevelUnit.CreatedById = pEntOrgLevel.ClientId;
                    entOrgBaseLevelUnit.LevelId = entOrgBaseLevel.ID;
                    entOrgBaseLevelUnit.ParentUnitId = Schema.Common.VAL_ORGNIZATION_BASE_PARENT_UNIT_ID;
                    entOrgBaseLevelUnit.LastModifiedById = pEntOrgLevel.ClientId;
                    entOrgBaseLevelUnit = entOrgBaseLevelUnitDAM.UpdateOrganizationLevelUnit(entOrgBaseLevelUnit, Schema.Common.VAL_INSERT_MODE);
                }
                pEntOrgLevel = UpdateOrganizationLevel(pEntOrgLevel, Schema.Common.VAL_INSERT_MODE);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntOrgLevel;
        }

        /// <summary>
        /// Edit Organization Level
        /// </summary>
        /// <param name="pEntOrgLevel"></param>
        /// <returns>OrganizationLevel Object</returns>
        public OrganizationLevel EditOrganizationLevel(OrganizationLevel pEntOrgLevel)
        {
            try
            {
                pEntOrgLevel = UpdateOrganizationLevel(pEntOrgLevel, Schema.Common.VAL_UPDATE_MODE);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntOrgLevel;
        }

        /// <summary>
        /// Get All Organization Levels
        /// </summary>
        /// <param name="pEntOrgLevel"></param>
        /// <returns>List of OrganizationLevel Object</returns>
        public List<OrganizationLevel> GetAllLevels(OrganizationLevel pEntOrgLevel)
        {
            _sqlObject = new SQLObject();
            List<OrganizationLevel> entListOrgLevels = new List<OrganizationLevel>();
            OrganizationLevel entOrgLevel;
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntOrgLevel.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.OrganizationLevel.PROC_GET_ALL_ORGANIZATION_LEVELS, sqlConnection);

                if (pEntOrgLevel.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntOrgLevel.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntOrgLevel.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntOrgLevel.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntOrgLevel.ListRange.RequestedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                /* below line added for time out issue.*/
                _sqlcmd.CommandTimeout = 0;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListOrgLevels = new List<OrganizationLevel>();
                while (_sqlreader.Read())
                {
                    entOrgLevel = FillObjectForGetAll(_sqlreader, _sqlObject);
                    entListOrgLevels.Add(entOrgLevel);
                }
            }
            catch
            {
                //In Child Method & SqlReader is used then use Try - Catch - Finally Block and Close that reader(if required) in that same method.
                throw;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListOrgLevels;
        }

        /// <summary>
        /// Get Only  Organization Levels WithOut Unit
        /// </summary>
        /// <param name="pEntOrgLevel"></param>
        /// <returns>List of OrganizationLevel Object</returns>
        public List<OrganizationLevel> GetOnlyLevels(OrganizationLevel pEntOrgLevel)
        {
            _sqlObject = new SQLObject();
            List<OrganizationLevel> entListOrgLevels = new List<OrganizationLevel>();
            OrganizationLevel entOrgLevel;
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntOrgLevel.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.OrganizationLevel.PROC_GET_ALL_ORGANIZATION_LEVELS, sqlConnection);

                if (pEntOrgLevel.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntOrgLevel.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntOrgLevel.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntOrgLevel.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListOrgLevels = new List<OrganizationLevel>();
                while (_sqlreader.Read())
                {
                    entOrgLevel = FillObjectOnlyLevels(_sqlreader, _sqlObject);
                    entListOrgLevels.Add(entOrgLevel);
                }
            }
            catch
            {
                //In Child Method & SqlReader is used then use Try - Catch - Finally Block and Close that reader(if required) in that same method.
                throw;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListOrgLevels;
        }

        /// <summary>
        /// Get All Organization Level Units
        /// </summary>
        /// <param name="pEntOrgLevel"></param>
        /// <returns>List of OrganizationLevelUnit Object</returns>
        private List<OrganizationLevelUnit> GetAllLevelUnits(OrganizationLevel pEntOrgLevel)
        {
            _sqlObject = new SQLObject();
            List<OrganizationLevelUnit> entListOrgLevelUnits;
            OrganizationLevelUnit entOrgLevelUnit;
            SqlDataReader sqlreaderGetAllLevelUnits;
            sqlreaderGetAllLevelUnits = null;
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.OrganizationLevel.PROC_GET_ALL_ORGANIZATION_UNITS, sqlConnection);
                _sqlpara = new SqlParameter(Schema.OrganizationLevel.PARA_LEVEL_ID, pEntOrgLevel.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                sqlreaderGetAllLevelUnits = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListOrgLevelUnits = new List<OrganizationLevelUnit>();
                while (sqlreaderGetAllLevelUnits.Read())
                {
                    entOrgLevelUnit = FillUnits(sqlreaderGetAllLevelUnits);
                    entListOrgLevelUnits.Add(entOrgLevelUnit);
                }
            }
            catch
            {
                //In Child Method & SqlReader is used then use Try - Catch - Finally Block and Close that reader(if required) in that same method.
                throw;
            }
            finally
            {
                if (sqlreaderGetAllLevelUnits != null && !sqlreaderGetAllLevelUnits.IsClosed)
                    sqlreaderGetAllLevelUnits.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListOrgLevelUnits;
        }

        /// <summary>
        /// Fill reader object for Units
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>OrganizationLevelUnit Object</returns>
        private OrganizationLevelUnit FillUnits(SqlDataReader pReader)
        {
            OrganizationLevelUnit entOrgLevelUnit = new OrganizationLevelUnit();
            _sqlObject = new SQLObject();
            int iIndex;
            if (pReader.HasRows)
            {
                iIndex = pReader.GetOrdinal(Schema.OrganizationLevelUnit.COL_UNIT_ID);
                entOrgLevelUnit.ID = pReader.GetString(iIndex);
                entOrgLevelUnit.UnitName = pReader[Schema.OrganizationLevelUnit.COL_UNIT_NAME].ToString();
                entOrgLevelUnit.SequenceOrder = Convert.ToInt32(pReader[Schema.OrganizationLevelUnit.COL_UNIT_ORDER]);
                entOrgLevelUnit.ClientId = pReader[Schema.OrganizationLevelUnit.COL_UNIT_CLIENT_ID].ToString();
                entOrgLevelUnit.LevelId = pReader[Schema.OrganizationLevelUnit.COL_LEVEL_ID].ToString();
                entOrgLevelUnit.ParentUnitId = pReader[Schema.OrganizationLevelUnit.COL_UNIT_PARENT_ID].ToString();
                entOrgLevelUnit.IsActive = Convert.ToBoolean(pReader[Schema.OrganizationLevelUnit.COL_UNIT_ISACTIVE]);
                entOrgLevelUnit.CreatedById = pReader[Schema.Common.COL_CREATED_BY].ToString();
                entOrgLevelUnit.LastModifiedById = pReader[Schema.Common.COL_CREATED_BY].ToString();
                entOrgLevelUnit.LastModifiedDate = Convert.ToDateTime(pReader[Schema.Common.COL_MODIFIED_ON]);
                entOrgLevelUnit.DateCreated = Convert.ToDateTime(pReader[Schema.Common.COL_CREATED_ON]);
                entOrgLevelUnit.OrgTreeUnitId = Convert.ToInt32(pReader[Schema.OrganizationLevelUnit.COL_ORGTREE_UNIT_ID]);
                if (_sqlObject.ReaderHasColumn(pReader, Schema.OrganizationLevelUnit.COL_ORGTREE_PARENT_UNIT_ID))
                {
                    iIndex = pReader.GetOrdinal(Schema.OrganizationLevelUnit.COL_ORGTREE_PARENT_UNIT_ID);
                    if (!pReader.IsDBNull(iIndex))
                    {
                        entOrgLevelUnit.OrgTreeParentUnitId = Convert.ToInt32(pReader[Schema.OrganizationLevelUnit.COL_ORGTREE_PARENT_UNIT_ID]);
                    }


                }


            }
            return entOrgLevelUnit;
        }

        /// <summary>
        /// Update the organization level
        /// </summary>
        /// <param name="pOrgLevel"></param>
        /// <param name="pUpdateMode"></param>
        /// <returns>OrganizationLevel Object</returns>
        public OrganizationLevel UpdateOrganizationLevel(OrganizationLevel pOrgLevel, string pUpdateMode)
        {
            _sqlObject = new SQLObject();
            int iUpdateStatus = 0;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.OrganizationLevel.PROC_UPDATE_ORGANIZATION_LEVEL;

            _strConnString = _sqlObject.GetClientDBConnString(pOrgLevel.ClientId);
            if (pUpdateMode == Schema.Common.VAL_INSERT_MODE)
            {
                pOrgLevel.ID = YPLMS.Services.IDGenerator.GetStringGUID();
            }
            _sqlpara = new SqlParameter(Schema.OrganizationLevel.PARA_LEVEL_ID, pOrgLevel.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.OrganizationLevel.PARA_LEVEL_NAME, pOrgLevel.LevelName);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.OrganizationLevel.PARA_LEVEL_ORDER, pOrgLevel.LevelOrder);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.OrganizationLevel.PARA_CLIENT_ID, pOrgLevel.ClientId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pOrgLevel.CreatedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pOrgLevel.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, pUpdateMode);
            _sqlcmd.Parameters.Add(_sqlpara);

            iUpdateStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            return pOrgLevel;
        }

        /// <summary>
        /// Check if Organiztion level name already exist otherwise return matching Organization Level
        /// </summary>
        /// <param name="pOrgLevel"></param>
        /// <returns>OrganizationLevel</returns>
        public OrganizationLevel CheckandAddOrganizationLevel(OrganizationLevel pOrgLevel)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            OrganizationLevel entOrgLevel = new OrganizationLevel();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pOrgLevel.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.OrganizationLevel.PROC_CAA_ORGANIZATION_LEVEL, sqlConnection);

                pOrgLevel.ID = YPLMS.Services.IDGenerator.GetStringGUID();

                _sqlpara = new SqlParameter(Schema.OrganizationLevel.PARA_LEVEL_ID, pOrgLevel.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.OrganizationLevel.PARA_LEVEL_NAME, pOrgLevel.LevelName);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.OrganizationLevel.PARA_LEVEL_ORDER, pOrgLevel.LevelOrder);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.OrganizationLevel.PARA_CLIENT_ID, pOrgLevel.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pOrgLevel.CreatedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pOrgLevel.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entOrgLevel = FillObjectOnlyLevels(_sqlreader, _sqlObject);
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
            return entOrgLevel;
        }

        /// <summary>
        /// Delete organization level
        /// </summary>
        /// <param name="pEntOrgLevel"></param>
        /// <returns>OrganizationLevel Object</returns>
        public OrganizationLevel DeleteOrganizationLevel(OrganizationLevel pEntOrgLevel)
        {
            _sqlObject = new SQLObject();
            int iDelStatus = 0;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.OrganizationLevel.PROC_DELETE_ORGANIZATION_LEVEL;
            _sqlpara = new SqlParameter(Schema.OrganizationLevel.PARA_LEVEL_IDS, pEntOrgLevel.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntOrgLevel.ClientId);
                iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                if (iDelStatus < 0)
                {
                    pEntOrgLevel = null;
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntOrgLevel;
        }

        /// <summary>
        /// Get Organization Tree
        /// </summary>
        /// <param name="pEntOrganizationLevel"></param>
        /// <returns></returns>
        public OrganizationLevel GetOrganizationTreeDHTML(OrganizationLevel pEntOrganizationLevel)
        {
            _sqlObject = new SQLObject();
            OrganizationLevel entOrganizationLevel = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntOrganizationLevel.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.OrganizationLevel.PROC_GET_ORGANIZATION_TREE_LIST, sqlConnection);
                _sqlcmd.CommandTimeout = 0;
                if (pEntOrganizationLevel.ListRange != null && !string.IsNullOrEmpty(pEntOrganizationLevel.ListRange.RequestedById))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntOrganizationLevel.ListRange.RequestedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                DataSet ds = _sqlObject.SqlDataAdapter(_sqlcmd, _strConnString);
                entOrganizationLevel = FillOrgTree_DHTML(ds);
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
            return entOrganizationLevel;
        }

        /// <summary>
        /// Fill Org Tree
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns></returns>
        private OrganizationLevel FillOrgTree_DHTML(DataSet pDataSet)
        {
            OrganizationLevel entOrgLevel = new OrganizationLevel();
            XmlDocument xmlDocOrgTree = new XmlDocument();
            string strOrgXMLValue = string.Empty;
            try
            {
                if (pDataSet.Tables.Count > 0)
                {

                    DataView dvParent = pDataSet.Tables[0].DefaultView;
                    XmlDeclaration xmlDeclaration = xmlDocOrgTree.CreateXmlDeclaration("1.0", "utf-8", null);

                    DataRow[] drowGetAllLevelCount = null;
                    string strExp = "";
                    string strSort = "LevelOrder desc";
                    drowGetAllLevelCount = pDataSet.Tables[0].Select(strExp, strSort);
                    int iLvlCnt = 0;
                    if (drowGetAllLevelCount.Length > 0)
                    {
                        iLvlCnt = Convert.ToInt32(drowGetAllLevelCount[0]["LevelOrder"].ToString());
                    }

                    XmlElement parentNode = null;

                    for (int i = 0; i <= iLvlCnt; i++)
                    {
                        DataRow[] foundRows;
                        strExp = "LevelOrder='" + i + "'";
                        strSort = "SequenceOrder";
                        foundRows = pDataSet.Tables[0].Select(strExp, strSort);
                        string strPrevPraentId = "";
                        string strOldParentId = "";
                        XmlNode xNode = null;
                        for (int j = 0; j <= foundRows.GetUpperBound(0); j++)
                        {
                            string strUnitId = foundRows[j]["Unitid"].ToString();
                            string strParentUnitId = foundRows[j]["ParentUnitId"].ToString();
                            string strUnitName = foundRows[j]["UnitName"].ToString();
                            string strSqnOrdNo = foundRows[j]["SequenceOrder"].ToString();
                            string strLevelId = foundRows[j]["LevelId"].ToString();
                            string strParentId = strParentUnitId;

                            if (j == 0 && i == 0)
                            {
                                if (strUnitName == Schema.Common.VAL_ORGNIZATION_BASE_UNIT_NAME)
                                {
                                    parentNode = xmlDocOrgTree.CreateElement("O");
                                }
                                else
                                {
                                    parentNode = xmlDocOrgTree.CreateElement(strUnitName);
                                }
                                parentNode.SetAttribute("d", strUnitId);//id
                                parentNode.SetAttribute("p", strParentUnitId);//parentid
                                parentNode.SetAttribute("t", strUnitName);//text


                                parentNode.SetAttribute("e", "1");//open
                                parentNode.SetAttribute("c", "1");//call
                                parentNode.SetAttribute("s", "1");//select

                                //parentNode.SetAttribute("LevelOrder", i.ToString());
                                parentNode.SetAttribute("v", strLevelId);//LevelId
                                //For RuleScope org 
                                if (foundRows[j].Table.Columns.Contains("IsRuleScope"))
                                    parentNode.SetAttribute("z", foundRows[j]["IsRuleScope"].ToString());
                                else
                                    parentNode.SetAttribute("z", "0");

                                xmlDocOrgTree.AppendChild(parentNode);
                            }
                            else
                            {
                                parentNode = xmlDocOrgTree.CreateElement("i");//item

                                parentNode.SetAttribute("d", strUnitId);//id
                                parentNode.SetAttribute("p", strParentUnitId);//parentid
                                parentNode.SetAttribute("t", strUnitName);//text

                                //For RuleScope org 
                                if (foundRows[j].Table.Columns.Contains("IsRuleScope"))
                                    parentNode.SetAttribute("z", foundRows[j]["IsRuleScope"].ToString());
                                else
                                    parentNode.SetAttribute("z", "0");

                                //parentNode.SetAttribute("LevelOrder", i.ToString());
                                parentNode.SetAttribute("v", strLevelId);//LevelId
                            }




                            //------------------------- Find Has Child, First Node, Last Node ----------------                        

                            string strExpn = "";
                            DataRow[] drowParentHasChild = null;
                            DataRow[] drowParentHasChildNew = null;
                            DataRow[] drowParentHasChildNew1 = null;
                            strExpn = "ParentUnitId= '" + strParentUnitId + "'";
                            strSort = "SequenceOrder";
                            drowParentHasChild = pDataSet.Tables[0].Select(strExpn, strSort);

                            strExpn = "ParentUnitId= '" + strParentUnitId + "' and Unitid= '" + strUnitId + "' and SequenceOrder='1'";
                            strSort = "SequenceOrder";
                            drowParentHasChildNew = pDataSet.Tables[0].Select(strExpn, strSort);

                            strExpn = "ParentUnitId= '" + strParentUnitId + "'";
                            strSort = "SequenceOrder Desc";
                            drowParentHasChildNew1 = pDataSet.Tables[0].Select(strExpn, strSort);

                            int iCnt = drowParentHasChild.Length;

                            bool bFirst = false;
                            if (drowParentHasChildNew != null && drowParentHasChildNew.Length > 0)
                            {
                                bFirst = true;
                            }
                            int iLast = Convert.ToInt32(drowParentHasChild[drowParentHasChildNew1.GetUpperBound(0)]["SequenceOrder"]);

                            if (Convert.ToInt32(strSqnOrdNo) == iCnt || iCnt == 1 || iLast == (iCnt - 1))
                                parentNode.SetAttribute("l", "1");//IsLast
                            else
                                parentNode.SetAttribute("l", "0");//IsLast

                            if (bFirst || j == 0)
                            {
                                parentNode.SetAttribute("f", "1");//IsFirst
                            }
                            else
                            {
                                parentNode.SetAttribute("f", "0");//IsFirst
                            }
                            //-------------------------
                            if (strPrevPraentId != strParentUnitId)
                            {
                                xNode = xmlDocOrgTree.SelectSingleNode("//*[@d='" + strParentUnitId + "']");//id
                            }
                            if (xNode != null)
                            {
                                xNode.AppendChild(parentNode);
                            }
                            strOldParentId = strParentId;
                        }
                    }
                }
            }
            catch
            {
            }
            entOrgLevel.OrganizationTreeXML = xmlDocOrgTree;
            return entOrgLevel;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntOrganizationLevel"></param>
        /// <returns></returns>
        public OrganizationLevel Get(OrganizationLevel pEntOrganizationLevel)
        {
            return GetOrganizationLevelsByID(pEntOrganizationLevel);
        }
        /// <summary>
        /// Update OrganizationLevel
        /// </summary>
        /// <param name="pEntOrganizationLevel"></param>
        /// <returns></returns>
        public OrganizationLevel Update(OrganizationLevel pEntOrganizationLevel)
        {
            return EditOrganizationLevel(pEntOrganizationLevel);
        }
        #endregion
    }
}
