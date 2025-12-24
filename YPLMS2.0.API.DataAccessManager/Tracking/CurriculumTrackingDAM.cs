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
    /// class CurriculumTrackingDAM
    /// </summary>
    public class CurriculumTrackingDAM : IDataManager<CurriculumTracking>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlParameter _sqlpara = null;
        SqlCommand _sqlcmd = null;
        EntityRange _entRange = null;
        SqlConnection _sqlConnection = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.CurriculumPlan.CURRICULUM_DL_ERROR;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// Fill Object User
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <param name="pRangeList"></param>
        /// <returns></returns>
        private CurriculumTracking FillObjectUser(SqlDataReader pSqlReader, bool pRangeList)
        {
            CurriculumTracking entCurriculumTracking = new CurriculumTracking();
            int iIndex = 0;
            if (pSqlReader.HasRows)
            {

                try
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_ATTEMPT_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entCurriculumTracking.ID = pSqlReader.GetString(iIndex);
                }
                catch { }

                iIndex = pSqlReader.GetOrdinal(Schema.CurriculumTracking.COL_CURRICULUM_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCurriculumTracking.CurriculumId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCurriculumTracking.UserID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.CurriculumTracking.COL_COMPLETION_STATUS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCurriculumTracking.CompletionStatus = (ActivityCompletionStatus)Enum.Parse(typeof(ActivityCompletionStatus), pSqlReader.GetString(iIndex));

                iIndex = pSqlReader.GetOrdinal(Schema.CurriculumTracking.COL_DATE_OF_COMPLETION);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCurriculumTracking.DateOfCompletion = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.CurriculumTracking.COL_DATE_OF_START);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCurriculumTracking.DateOfStart = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.CurriculumTracking.COL_MARKED_COMPLETED_BY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCurriculumTracking.MarkedCompletedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.CurriculumTracking.COL_SCANNED_CERTIFICATION_FILE_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCurriculumTracking.ScannedCertificationFileName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.CurriculumTracking.COL_ACTIVITY_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCurriculumTracking.ActivityName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.CurriculumTracking.COL_USER_FIRST_LAST_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCurriculumTracking.UserFirstLastName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.CurriculumTracking.COL_REVIEWER_COMMENTS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCurriculumTracking.ReviewComments = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.CurriculumTracking.COL_IS_FOR_ADMIN_PREVIEW);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCurriculumTracking.IsForAdminPreview = pSqlReader.GetBoolean(iIndex);

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    entCurriculumTracking.ListRange = _entRange;
                }
            }
            return entCurriculumTracking;
        }

        /// <summary>
        /// Get CurriculumTracking Details By Id
        /// </summary>
        /// <returns>List of CurriculumTracking Object</returns>
        public CurriculumTracking GetCurriculumTrackingById(CurriculumTracking pEntCurriculumTracking)
        {
            _sqlObject = new SQLObject();
            CurriculumTracking entCurriculumTracking = null;
            _strConnString = _sqlObject.GetClientDBConnString(pEntCurriculumTracking.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.CurriculumTracking.PROC_GET_USER_CURRICULUM_TRACKING, _sqlConnection);
            _sqlpara = new SqlParameter(Schema.Common.PARA_ATTEMPT_ID, pEntCurriculumTracking.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            entCurriculumTracking = new CurriculumTracking();
            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    entCurriculumTracking = FillObjectUser(_sqlreader, false);
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
                if (_sqlConnection != null && _sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.Close();
            }
            return entCurriculumTracking;
        }

        /// <summary>
        /// Get CurriculumTracking Details By Status
        /// </summary>
        /// <returns>List of CurriculumTracking Object</returns>
        public CurriculumTracking GetCurriculumTrackingByStatus(CurriculumTracking pEntCurriculumTracking)
        {
            _sqlObject = new SQLObject();
            CurriculumTracking entCurriculumTracking = null;
            _strConnString = _sqlObject.GetClientDBConnString(pEntCurriculumTracking.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.CurriculumTracking.PROC_GET_USER_CURRICULUM_TRACKING_STATUS, _sqlConnection);
            _sqlpara = new SqlParameter(Schema.CurriculumTracking.PARA_SYSTEM_USER_ID, pEntCurriculumTracking.UserID);
            _sqlcmd.Parameters.Add(_sqlpara);
            _sqlpara = new SqlParameter(Schema.CurriculumTracking.PARA_CURRICULUM_ID, pEntCurriculumTracking.CurriculumId);
            _sqlcmd.Parameters.Add(_sqlpara);
            entCurriculumTracking = new CurriculumTracking();
            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    entCurriculumTracking = FillObjectUser(_sqlreader, false);
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
                if (_sqlConnection != null && _sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.Close();
            }
            return entCurriculumTracking;
        }

        /// <summary>
        /// To update/add new Email Template
        /// </summary>
        /// <param name="pEntCurriculumTracking"></param>        
        /// <returns>CurriculumTracking object</returns>
        public CurriculumTracking AddUpdateCurriculumTracking(CurriculumTracking pEntCurriculumTracking)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.CurriculumTracking.PROC_UPDATE_USER_CURRICULUM_TRACKING;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntCurriculumTracking.ClientId);
                if (string.IsNullOrEmpty(pEntCurriculumTracking.ID))
                {
                    //assign new id
                    pEntCurriculumTracking.ID = Schema.Common.VAL_ATTEMPT_ID_PREFIX + YPLMS.Services.IDGenerator.GetUniqueKey(Schema.Common.VAL_ATTEMPT_ID_LENGTH);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                }
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_ATTEMPT_ID, pEntCurriculumTracking.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.CurriculumTracking.PARA_CURRICULUM_ID, pEntCurriculumTracking.CurriculumId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.CurriculumTracking.PARA_COMPLETION_STATUS, pEntCurriculumTracking.CompletionStatus.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.CurriculumTracking.PARA_DATE_OF_COMPLETION, pEntCurriculumTracking.DateOfCompletion);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.CurriculumTracking.PARA_DATE_OF_START, pEntCurriculumTracking.DateOfStart);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Learner.COL_USER_ID, pEntCurriculumTracking.UserID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntCurriculumTracking.MarkedCompletedById))
                    _sqlpara = new SqlParameter(Schema.CurriculumTracking.PARA_MARKED_COMPLETED_BY_ID, pEntCurriculumTracking.MarkedCompletedById);
                else
                    _sqlpara = new SqlParameter(Schema.CurriculumTracking.PARA_MARKED_COMPLETED_BY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntCurriculumTracking.ScannedCertificationFileName))
                    _sqlpara = new SqlParameter(Schema.CurriculumTracking.PARA_SCANNED_CERTIFICATION_FILE_NAME, pEntCurriculumTracking.ScannedCertificationFileName);
                else
                    _sqlpara = new SqlParameter(Schema.CurriculumTracking.PARA_SCANNED_CERTIFICATION_FILE_NAME, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                //ActivityName and UserFirstLastName is Read Only
                if (!string.IsNullOrEmpty(pEntCurriculumTracking.ReviewComments))
                    _sqlpara = new SqlParameter(Schema.CurriculumTracking.PARA_REVIEWER_COMMENTS, pEntCurriculumTracking.ReviewComments);
                else
                    _sqlpara = new SqlParameter(Schema.CurriculumTracking.PARA_REVIEWER_COMMENTS, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.CurriculumTracking.PARA_IS_FOR_ADMIN_PREVIEW, pEntCurriculumTracking.IsForAdminPreview);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntCurriculumTracking;
        }

        /// <summary>
        /// for Bulk Update
        /// </summary>
        /// <param name="pEntListTracking"></param>
        /// <returns></returns>
        public List<CurriculumTracking> BulkUpdate(List<CurriculumTracking> pEntListTracking, bool pIsBulkMarkCompleted)
        {
            _sqlObject = new SQLObject();
            List<CurriculumTracking> entListTracking = new List<CurriculumTracking>();
            int iBatchSize = 0;
            SqlConnection _sqlcon = new SqlConnection();
            DataTable _dtable = null;
            SqlDataAdapter _sqladapter = null;
            try
            {
                if (pEntListTracking.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.CurriculumTracking.COL_ATTEMPT_ID);
                    _dtable.Columns.Add(Schema.CurriculumTracking.COL_CURRICULUM_ID);
                    _dtable.Columns.Add(Schema.Learner.COL_USER_ID);
                    _dtable.Columns.Add(Schema.CurriculumTracking.COL_DATE_OF_COMPLETION);
                    _dtable.Columns.Add(Schema.CurriculumTracking.COL_COMPLETION_STATUS);
                    _dtable.Columns.Add(Schema.CurriculumTracking.COL_MARKED_COMPLETED_BY_ID);
                    _dtable.Columns.Add(Schema.CurriculumTracking.COL_REVIEWER_COMMENTS);
                    _dtable.Columns.Add(Schema.CurriculumTracking.COL_SCANNED_CERTIFICATION_FILE_NAME);
                    _dtable.Columns.Add(Schema.Common.COL_IS_BULK_MARK_COMPLETED);


                    foreach (CurriculumTracking entAttempt in pEntListTracking)
                    {

                        DataRow drow = _dtable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entAttempt.ClientId);
                        if (string.IsNullOrEmpty(entAttempt.ID))
                            entAttempt.ID = YPLMS.Services.IDGenerator.GetStringGUID();

                        drow[Schema.CurriculumTracking.COL_ATTEMPT_ID] = entAttempt.ID;
                        drow[Schema.CurriculumTracking.COL_CURRICULUM_ID] = entAttempt.CurriculumId;
                        drow[Schema.Learner.COL_USER_ID] = entAttempt.UserID;
                        drow[Schema.CurriculumTracking.COL_DATE_OF_COMPLETION] = entAttempt.DateOfCompletion;
                        drow[Schema.CurriculumTracking.COL_COMPLETION_STATUS] = entAttempt.CompletionStatus.ToString();
                        drow[Schema.CurriculumTracking.COL_MARKED_COMPLETED_BY_ID] = entAttempt.MarkedCompletedById;
                        drow[Schema.CurriculumTracking.COL_REVIEWER_COMMENTS] = entAttempt.ReviewComments;
                        drow[Schema.CurriculumTracking.COL_SCANNED_CERTIFICATION_FILE_NAME] = entAttempt.ScannedCertificationFileName;
                        drow[Schema.Common.COL_IS_BULK_MARK_COMPLETED] = entAttempt.IsBulkMarkCompleted;

                        iBatchSize = iBatchSize + 1;
                        _dtable.Rows.Add(drow);
                        entListTracking.Add(entAttempt);
                    }

                    if (_dtable.Rows.Count > 0)
                    {
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.CurriculumTracking.PROC_UPDATE_MARK_COMPLTED;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;

                        _sqlcmd.Parameters.Add(Schema.CurriculumTracking.PARA_ATTEMPT_ID, SqlDbType.VarChar, 100, Schema.CurriculumTracking.COL_ATTEMPT_ID);
                        _sqlcmd.Parameters.Add(Schema.CurriculumTracking.PARA_CURRICULUM_ID, SqlDbType.VarChar, 100, Schema.CurriculumTracking.COL_CURRICULUM_ID);
                        _sqlcmd.Parameters.Add(Schema.Learner.PARA_USER_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_USER_ID);
                        _sqlcmd.Parameters.Add(Schema.CurriculumTracking.PARA_COMPLETED_DATE, SqlDbType.DateTime, 20, Schema.CurriculumTracking.COL_DATE_OF_COMPLETION);
                        _sqlcmd.Parameters.Add(Schema.CurriculumTracking.PARA_COMPLETION_STATUS, SqlDbType.VarChar, 100, Schema.CurriculumTracking.COL_COMPLETION_STATUS);
                        _sqlcmd.Parameters.Add(Schema.CurriculumTracking.PARA_MARKED_COMPLETED_BY_ID, SqlDbType.VarChar, 100, Schema.CurriculumTracking.COL_MARKED_COMPLETED_BY_ID);
                        _sqlcmd.Parameters.Add(Schema.CurriculumTracking.PARA_REVIEWER_COMMENTS, SqlDbType.NVarChar, 2000, Schema.CurriculumTracking.COL_REVIEWER_COMMENTS);
                        _sqlcmd.Parameters.Add(Schema.CurriculumTracking.PARA_SCANNED_CERTIFICATION_FILE_NAME, SqlDbType.VarChar, 500, Schema.CurriculumTracking.COL_SCANNED_CERTIFICATION_FILE_NAME);
                        _sqlcmd.Parameters.Add(Schema.Common.PARA_IS_BULK_MARK_COMPLETED, SqlDbType.Bit, 1, Schema.Common.COL_IS_BULK_MARK_COMPLETED);
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
            return entListTracking;
        }

        /// <summary>
        /// To update FileName
        /// </summary>
        /// <param name="pEntCurriculumTracking"></param>        
        /// <returns>CurriculumTracking object</returns>
        public CurriculumTracking UpdateScannedFileName(CurriculumTracking pEntCurriculumTracking)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Common.PROC_UPDATE_SCANNED_FILE_NAME;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntCurriculumTracking.ClientId);
                if (!string.IsNullOrEmpty(pEntCurriculumTracking.CurriculumId))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_ACTIVITY_ID, pEntCurriculumTracking.CurriculumId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntCurriculumTracking.UserID))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SYSTEM_USER_GUID, pEntCurriculumTracking.UserID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntCurriculumTracking.ScannedCertificationFileName))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SCANNED_FILE_NAME, pEntCurriculumTracking.ScannedCertificationFileName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlpara = new SqlParameter(Schema.Common.PARA_ACTIVITY_TYPE, ActivityContentType.Curriculum.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntCurriculumTracking;
        }

        /// <summary>
        /// Questionnaire Tracking For Admin Preview Delete
        /// </summary>
        /// <param name="pEntAttempt"></param>
        /// <returns></returns>
        public CurriculumTracking DeleteAdminCurriculumTracking(CurriculumTracking pEntCurriculumTracking)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.CurriculumTracking.PROC_DELETE_ADMIN_CUURICULUM_TRACKING;
            _sqlcmd.CommandType = CommandType.StoredProcedure;

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntCurriculumTracking.ClientId);

                _sqlcmd.Parameters.AddWithValue(Schema.CurriculumTracking.PARA_ACTIVITY_ID, pEntCurriculumTracking.ID);
                _sqlcmd.Parameters.AddWithValue(Schema.Learner.PARA_USER_ID, pEntCurriculumTracking.UserID);

                int iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }

            return pEntCurriculumTracking;
        }

        /// <summary>
        /// Questionnaire Tracking For Admin Preview Delete
        /// </summary>
        /// <param name="pEntAttempt"></param>
        /// <returns></returns>
        public CurriculumTracking DeleteAdminPreviewTracking(CurriculumTracking pEntCurriculumTracking)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.CurriculumTracking.PROC_DELETE_ADMIN_PREVIEW_TRACKING;
            _sqlcmd.CommandType = CommandType.StoredProcedure;

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntCurriculumTracking.ClientId);

                _sqlcmd.Parameters.AddWithValue(Schema.CurriculumTracking.PARA_ACTIVITY_ID, pEntCurriculumTracking.ID);
                _sqlcmd.Parameters.AddWithValue(Schema.Learner.PARA_USER_ID, pEntCurriculumTracking.UserID);
                _sqlcmd.Parameters.AddWithValue(Schema.CurriculumTracking.PARA_ACTIVITY_TYPE, pEntCurriculumTracking.ActivityName);

                int iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }

            return pEntCurriculumTracking;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntCurriculumTracking"></param>
        /// <returns></returns>
        public CurriculumTracking Get(CurriculumTracking pEntCurriculumTracking)
        {
            return GetCurriculumTrackingById(pEntCurriculumTracking);
        }
        /// <summary>
        /// Update CurriculumTracking
        /// </summary>
        /// <param name="pEntCurriculumTracking"></param>
        /// <returns></returns>
        public CurriculumTracking Update(CurriculumTracking pEntCurriculumTracking)
        {
            return AddUpdateCurriculumTracking(pEntCurriculumTracking);
        }
        #endregion
    }
}
