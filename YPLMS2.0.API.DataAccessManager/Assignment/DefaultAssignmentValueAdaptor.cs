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
    public class DefaultAssignmentValueAdaptor : IDataManager<DefaultAssignmentValue>
    {
        #region Declaration
        string _strMessageId = YPLMS.Services.Messages.Client.ORG_LVL_DL_ERROR;
        string _strConnString = string.Empty;
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        SQLObject _sqlObject = null;
        SqlConnection _sqlcon = null;
        DataTable _dtable = null;
        SqlDataAdapter _sqladapter = null;
        #endregion



        /// <summary>
        /// Fill reader object for Get All
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>CurriculumActivity Object</returns>
        private DefaultAssignmentValue FillObjectForGetAll(SqlDataReader pReader, SQLObject pSqlObject)
        {
            DefaultAssignmentValue entDefaultAssignmentValue = new DefaultAssignmentValue();
            EntityRange entRange = new EntityRange();
            int iIndex;
            if (pReader.HasRows)
            {
                iIndex = pReader.GetOrdinal(Schema.DefaultAssignmentValue.COL_DEFAULTVALUE_ID);
                entDefaultAssignmentValue.ID = pReader.GetString(iIndex);
                iIndex = pReader.GetOrdinal(Schema.DefaultAssignmentValue.COL_MODULENAME);
                entDefaultAssignmentValue.ModuleName = pReader.GetString(iIndex);
                entDefaultAssignmentValue.FieldName = Convert.ToString(pReader[Schema.DefaultAssignmentValue.COL_FIELDNAME]);
                entDefaultAssignmentValue.DataTypee = Convert.ToString(pReader[Schema.DefaultAssignmentValue.COL_DATA_tYPEE]);
                entDefaultAssignmentValue.DefaultValue = Convert.ToString(pReader[Schema.DefaultAssignmentValue.COL_DEFAULTVALUE]);
                entDefaultAssignmentValue.Condition = Convert.ToString(pReader[Schema.DefaultAssignmentValue.COL_CONDITION]);
                entDefaultAssignmentValue.CurrentDate = Convert.ToDateTime(pReader[Schema.DefaultAssignmentValue.COL_CurrntDate]);
                entDefaultAssignmentValue.IsUsedForDynamicAssignment = Convert.ToBoolean(pReader[Schema.DefaultAssignmentValue.COL_IS_USED_FOR_DYNAMIC_ASSIGNMENT]);
            }
            return entDefaultAssignmentValue;
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="pEntOrgLevel"></param>
        /// <returns>CurriculumActivity Object</returns>
        public DefaultAssignmentValue DeleteDefaultAssignmentValue(DefaultAssignmentValue pEntDefaultAssignmentValue)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.DefaultAssignmentValue.sproc_DELETEDEFAULT_ASSIGNMENT_SETTING;
            _sqlpara = new SqlParameter(Schema.DefaultAssignmentValue.PARA_MODULENAME, pEntDefaultAssignmentValue.ModuleName);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntDefaultAssignmentValue.ClientId);
                int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                pEntDefaultAssignmentValue = null;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntDefaultAssignmentValue;
        }

        /// <summary>
        /// Get All CurriculumActivity
        /// </summary>
        /// <param name="pEntOrgLevel"></param>
        /// <returns>List of CurriculumActivity Object</returns>
        public List<DefaultAssignmentValue> GetAllDefaultAssignmentValue(DefaultAssignmentValue pEntDefaultAssignmentValue)
        {
            _sqlObject = new SQLObject();
            List<DefaultAssignmentValue> entListDefaultAssignmentValue = new List<DefaultAssignmentValue>();
            DefaultAssignmentValue entDefaultAssignmentValue;
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntDefaultAssignmentValue.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.DefaultAssignmentValue.PROC_GET_ALL_DEFAULT_ASSIGNMENT_SETTING, sqlConnection);
                if (!string.IsNullOrEmpty(pEntDefaultAssignmentValue.ID))
                {
                    _sqlpara = new SqlParameter(Schema.DefaultAssignmentValue.PARA_DEFAULTVALUE_ID, pEntDefaultAssignmentValue.ID.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntDefaultAssignmentValue.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntDefaultAssignmentValue.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntDefaultAssignmentValue.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntDefaultAssignmentValue.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntDefaultAssignmentValue.ListRange.RequestedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Learner.PARA_REQUESTED_BY_ID, pEntDefaultAssignmentValue.ListRange.RequestedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListDefaultAssignmentValue = new List<DefaultAssignmentValue>();
                while (_sqlreader.Read())
                {
                    entDefaultAssignmentValue = FillObjectForGetAll(_sqlreader, _sqlObject);
                    entListDefaultAssignmentValue.Add(entDefaultAssignmentValue);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entListDefaultAssignmentValue;
        }



        /// <summary>
        /// Update the CurriculumActivity
        /// </summary>
        /// <param name="pOrgLevel"></param>
        /// <param name="pUpdateMode"></param>
        /// <returns>CurriculumActivity Object</returns>
        public List<DefaultAssignmentValue> BulkUpdateDefaultAssignmentValue(List<DefaultAssignmentValue> pDefaultAssignmentValue)
        {

            _sqlObject = new SQLObject();
            List<DefaultAssignmentValue> entListDefaultAssignmentValue = new List<DefaultAssignmentValue>();
            int iBatchSize = 0;
            try
            {
                if (pDefaultAssignmentValue.Count > 0)
                {
                    _dtable = new DataTable();

                    _dtable.Columns.Add(Schema.DefaultAssignmentValue.COL_DEFAULTVALUE_ID);
                    _dtable.Columns.Add(Schema.DefaultAssignmentValue.COL_MODULENAME);
                    _dtable.Columns.Add(Schema.DefaultAssignmentValue.COL_FIELDNAME);
                    _dtable.Columns.Add(Schema.DefaultAssignmentValue.COL_DATA_tYPEE);
                    _dtable.Columns.Add(Schema.DefaultAssignmentValue.COL_DEFAULTVALUE);
                    _dtable.Columns.Add(Schema.DefaultAssignmentValue.COL_CONDITION);
                    _dtable.Columns.Add(Schema.DefaultAssignmentValue.COL_IS_USED_FOR_DYNAMIC_ASSIGNMENT);
                    _dtable.Columns.Add(Schema.Common.COL_CREATED_BY);


                    foreach (DefaultAssignmentValue entAttempt in pDefaultAssignmentValue)
                    {
                        DataRow drow = _dtable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entAttempt.ClientId);
                        if (string.IsNullOrEmpty(entAttempt.ID))
                            entAttempt.ID = YPLMS.Services.IDGenerator.GetStringGUID();

                        drow[Schema.DefaultAssignmentValue.COL_DEFAULTVALUE_ID] = entAttempt.ID;
                        drow[Schema.DefaultAssignmentValue.COL_MODULENAME] = entAttempt.ModuleName;
                        drow[Schema.DefaultAssignmentValue.COL_FIELDNAME] = entAttempt.FieldName;
                        drow[Schema.DefaultAssignmentValue.COL_DATA_tYPEE] = entAttempt.DataTypee;
                        drow[Schema.DefaultAssignmentValue.COL_DEFAULTVALUE] = entAttempt.DefaultValue;
                        drow[Schema.DefaultAssignmentValue.COL_CONDITION] = entAttempt.Condition;
                        drow[Schema.DefaultAssignmentValue.COL_IS_USED_FOR_DYNAMIC_ASSIGNMENT] = entAttempt.IsUsedForDynamicAssignment;
                        drow[Schema.Common.COL_CREATED_BY] = entAttempt.CreatedById;
                        iBatchSize = iBatchSize + 1;
                        _dtable.Rows.Add(drow);
                        entListDefaultAssignmentValue.Add(entAttempt);
                    }
                    if (_dtable.Rows.Count > 0)
                    {
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.DefaultAssignmentValue.sproc_DEFAULT_ASSIGNMENT_SETTING_UPS;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;

                        _sqlcmd.Parameters.Add(Schema.DefaultAssignmentValue.PARA_DEFAULTVALUE_ID, SqlDbType.VarChar, 50, Schema.DefaultAssignmentValue.COL_DEFAULTVALUE_ID);
                        _sqlcmd.Parameters.Add(Schema.DefaultAssignmentValue.PARA_MODULENAME, SqlDbType.VarChar, 100, Schema.DefaultAssignmentValue.COL_MODULENAME);
                        _sqlcmd.Parameters.Add(Schema.DefaultAssignmentValue.PARA_FIELDNAME, SqlDbType.VarChar, 100, Schema.DefaultAssignmentValue.COL_FIELDNAME);
                        _sqlcmd.Parameters.Add(Schema.DefaultAssignmentValue.PARA_DATA_tYPEE, SqlDbType.VarChar, 50, Schema.DefaultAssignmentValue.COL_DATA_tYPEE);
                        _sqlcmd.Parameters.Add(Schema.DefaultAssignmentValue.PARA_DEFAULTVALUE, SqlDbType.VarChar, 500, Schema.DefaultAssignmentValue.COL_DEFAULTVALUE);
                        _sqlcmd.Parameters.Add(Schema.DefaultAssignmentValue.PARA_CONDITION, SqlDbType.VarChar, 500, Schema.DefaultAssignmentValue.COL_CONDITION);
                        _sqlcmd.Parameters.Add(Schema.DefaultAssignmentValue.PARA_IS_USED_FOR_DYNAMIC_ASSIGNMENT, SqlDbType.Bit, 500, Schema.DefaultAssignmentValue.COL_IS_USED_FOR_DYNAMIC_ASSIGNMENT);
                        _sqlcmd.Parameters.Add(Schema.Common.PARA_CREATED_BY, SqlDbType.VarChar, 50, Schema.Common.COL_CREATED_BY);


                        _sqladapter = new SqlDataAdapter();
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.CommandTimeout = 0;
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
            return entListDefaultAssignmentValue;

        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntCurriculumActivity"></param>
        /// <returns></returns>
        public DefaultAssignmentValue Get(DefaultAssignmentValue pEntDefaultAssignmentValue)
        {
            return pEntDefaultAssignmentValue;
        }
        /// <summary>
        /// Update CurriculumActivity
        /// </summary>
        /// <param name="pEntCurriculumActivity"></param>
        /// <returns></returns>
        public DefaultAssignmentValue Update(DefaultAssignmentValue pEntDefaultAssignmentValue)
        {
            return (pEntDefaultAssignmentValue);
        }

        public DefaultAssignmentValue delete(DefaultAssignmentValue pEntDefaultAssignmentValue)
        {
            return DeleteDefaultAssignmentValue(pEntDefaultAssignmentValue);
        }

        #endregion
    }
}
