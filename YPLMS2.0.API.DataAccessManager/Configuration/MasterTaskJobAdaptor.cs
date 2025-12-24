using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;
using TaskStatus = YPLMS2._0.API.Entity.TaskStatus;

namespace YPLMS2._0.API.DataAccessManager
{
    public class MasterTaskJobAdaptor : IDataManager<MasterTaskJob>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;

        SqlDataReader _sqlreader = null;
        MasterTaskJob _entMasterTaskJob = null;

        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.MasterTaskJob.ERROR_MSG_ID;
        #endregion

        /// <summary>
        /// Fill Object
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <returns></returns>
        private MasterTaskJob FillObject(SqlDataReader pSqlreader)
        {
            _entMasterTaskJob = new MasterTaskJob();
            int iIndex;
            if (pSqlreader.HasRows)
            {
                iIndex = pSqlreader.GetOrdinal(Schema.MasterTaskJob.COL_CLIENT_ID);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entMasterTaskJob.ClientId = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.MasterTaskJob.COL_JOB_ID);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entMasterTaskJob.ID = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.MasterTaskJob.COL_TASK_STATUS);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entMasterTaskJob.Status = (TaskStatus)(Enum.Parse(typeof(TaskStatus), Convert.ToString(pSqlreader.GetString(iIndex))));

                iIndex = pSqlreader.GetOrdinal(Schema.MasterTaskJob.COL_TASK_TYPE);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entMasterTaskJob.Type = (TaskType)(Enum.Parse(typeof(TaskType), Convert.ToString(pSqlreader.GetString(iIndex))));


                iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entMasterTaskJob.DateCreated = pSqlreader.GetDateTime(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entMasterTaskJob.CreatedById = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.MasterTaskJob.COL_SCHEDULEDDATETIME);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entMasterTaskJob.ScheduledDateTime = pSqlreader.GetDateTime(iIndex);

            }
            return _entMasterTaskJob;
        }


        /// <summary>
        /// BulkUpdate MasterTaskJob
        /// </summary>
        /// <param name="pEntListMasterTaskJob"></param>
        /// <returns></returns>
        public List<MasterTaskJob> BulkUpdate(List<MasterTaskJob> pEntListMasterTaskJob)
        {
            _sqlObject = new SQLObject();
            List<MasterTaskJob> entListMasterTaskJob = new List<MasterTaskJob>();
            SqlDataAdapter sqladapter = null;
            DataTable dtable;
            SqlCommand sqlcmdDel;
            int iBatchSize = 0;
            DataRow drow = null;
            EntityRange entRange = new EntityRange();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntListMasterTaskJob[0].ClientId);
                SqlConnection _sqlcon = new SqlConnection(_strConnString);
                dtable = new DataTable();
                dtable.Columns.Add(Schema.MasterTaskJob.COL_JOB_ID);
                dtable.Columns.Add(Schema.MasterTaskJob.COL_TASK_STATUS);


                foreach (MasterTaskJob entMasterTaskJob in pEntListMasterTaskJob)
                {
                    drow = dtable.NewRow();
                    drow[Schema.MasterTaskJob.COL_JOB_ID] = entMasterTaskJob.ID;
                    drow[Schema.MasterTaskJob.COL_TASK_STATUS] = entMasterTaskJob.Status;
                    dtable.Rows.Add(drow);
                    entListMasterTaskJob.Add(entMasterTaskJob);
                    iBatchSize++;
                }

                if (dtable.Rows.Count > 0)
                {
                    sqlcmdDel = new SqlCommand(Schema.MasterTaskJob.PROC_UPS_MASTER_TASK, _sqlcon);
                    sqlcmdDel.CommandType = CommandType.StoredProcedure;
                    sqlcmdDel.Parameters.Add(Schema.MasterTaskJob.PARA_JOB_ID, SqlDbType.VarChar, 100, Schema.MasterTaskJob.COL_JOB_ID);
                    sqlcmdDel.Parameters.Add(Schema.MasterTaskJob.PARA_TASK_STATUS, SqlDbType.VarChar, 100, Schema.MasterTaskJob.COL_TASK_STATUS);
                    sqladapter = new SqlDataAdapter();
                    sqladapter.InsertCommand = sqlcmdDel;
                    sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    sqladapter.UpdateBatchSize = iBatchSize;
                    entRange.TotalRows = sqladapter.Update(dtable);
                    sqladapter.Dispose();
                }

            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListMasterTaskJob;
        }

        /// <summary>
        /// Get MasterTaskJob Email List
        /// </summary>
        /// <param name="pEntMasterTaskJob"></param>
        /// <returns></returns>
        public List<MasterTaskJob> GetMasterTaskJobOpenTaskList(MasterTaskJob pEntMasterTaskJob)
        {
            _sqlObject = new SQLObject();
            List<MasterTaskJob> entListMasterTaskJob = new List<MasterTaskJob>();
            SqlConnection sqlConnection = null;
            SqlParameter _sqlpara = null;
            _sqlreader = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntMasterTaskJob.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.Connection = sqlConnection;

                switch (pEntMasterTaskJob.Type)
                {
                    case TaskType.Import:
                    case TaskType.Assignment:
                    case TaskType.UnAssignment:
                    case TaskType.MarkComplete:
                    case TaskType.User:
                        {
                            _sqlcmd.CommandText = Schema.MasterTaskJob.PROC_GET_ALL_OPENIMPORT;
                            break;
                        }
                    case TaskType.Email:
                        {
                            _sqlcmd.CommandText = Schema.MasterTaskJob.PROC_GET_ALL_OPENEMAIL;
                            break;
                        }
                    case TaskType.Report:
                        {
                            _sqlcmd.CommandText = Schema.MasterTaskJob.PROC_GET_ALL_OPENREPORT;
                            break;
                        }
                    default:
                        break;
                }

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entMasterTaskJob = FillObject(_sqlreader);
                    entListMasterTaskJob.Add(_entMasterTaskJob);
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
            return entListMasterTaskJob;
        }

        #region Interface Methods
        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="pEntMasterTaskJob"></param>
        /// <returns>null</returns>
        public MasterTaskJob Get(MasterTaskJob pEntMasterTaskJob)
        {
            return null;
        }
        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="pEntMasterTaskJob"></param>
        /// <returns>null</returns>
        public MasterTaskJob Update(MasterTaskJob pEntMasterTaskJob)
        {
            return null;
        }
        #endregion

    }
}
