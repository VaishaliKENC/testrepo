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
    /// class UserPolicyTrackingDAM
    /// </summary>
    public class UserPolicyTrackingDAM : IDataManager<UserPolicyTracking>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlParameter _sqlpara = null;
        SqlCommand _sqlcmd = null;
        EntityRange _entRange = null;
        SqlConnection _sqlConnection = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.Policy.POLICY_BL_ERROR;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>UserPolicyTracking Object</returns>
        private UserPolicyTracking FillObjectUser(SqlDataReader pSqlReader, bool pRangeList)
        {
            UserPolicyTracking entUserPolicyTracking = new UserPolicyTracking();
            int iIndex = 0;
            if (pSqlReader.HasRows)
            {

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.Common.COL_ATTEMPT_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_ATTEMPT_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entUserPolicyTracking.ID = pSqlReader.GetString(iIndex);
                }

                iIndex = pSqlReader.GetOrdinal(Schema.UserPolicyTracking.COL_POLICY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserPolicyTracking.PolicyId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserPolicyTracking.COL_SYSTEM_USER_GUID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserPolicyTracking.SystemUserGUID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserPolicyTracking.COL_COMPLETION_STATUS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserPolicyTracking.CompletionStatus = (ActivityCompletionStatus)Enum.Parse(typeof(ActivityCompletionStatus), pSqlReader.GetString(iIndex));

                iIndex = pSqlReader.GetOrdinal(Schema.UserPolicyTracking.COL_DATE_OF_COMPLETION);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserPolicyTracking.DateOfCompletion = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserPolicyTracking.COL_MARKED_COMPLETED_BY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserPolicyTracking.MarkedCompletedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserPolicyTracking.COL_SCANNED_CERTIFICATION_FILE_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserPolicyTracking.ScannedCertificationFileName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserPolicyTracking.PARA_ACTIVITY_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserPolicyTracking.ActivityName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserPolicyTracking.PARA_USER_FIRST_LAST_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserPolicyTracking.UserFirstLastName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserPolicyTracking.PARA_REVIEWER_COMMENTS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserPolicyTracking.ReviewComments = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserPolicyTracking.PARA_IS_FOR_ADMIN_PREVIEW);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserPolicyTracking.IsForAdminPreview = pSqlReader.GetBoolean(iIndex);

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    entUserPolicyTracking.ListRange = _entRange;
                }
            }
            return entUserPolicyTracking;
        }

        /// <summary>
        /// Get UserPolicyTracking Details By Id
        /// </summary>
        /// <returns>List of UserPolicyTracking Object</returns>
        public UserPolicyTracking GetUserPolicyTrackingById(UserPolicyTracking pEntUserPolicyTracking)
        {
            _sqlObject = new SQLObject();
            UserPolicyTracking entUserPolicyTracking = null;
            _strConnString = _sqlObject.GetClientDBConnString(pEntUserPolicyTracking.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.UserPolicyTracking.PROC_GET_USER_POLICY_TRACKING, _sqlConnection);
            _sqlpara = new SqlParameter(Schema.Common.PARA_ATTEMPT_ID, pEntUserPolicyTracking.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            entUserPolicyTracking = new UserPolicyTracking();
            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    entUserPolicyTracking = FillObjectUser(_sqlreader, false);
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
            return entUserPolicyTracking;
        }

        /// <summary>
        /// To update/add new Email Template
        /// </summary>
        /// <param name="pEntUserPolicyTracking"></param>        
        /// <returns>UserPolicyTracking object</returns>
        public UserPolicyTracking AddUpdateUserPolicyTracking(UserPolicyTracking pEntUserPolicyTracking)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.UserPolicyTracking.PROC_UPDATE_USER_POLICY_TRACKING;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntUserPolicyTracking.ClientId);
                if (string.IsNullOrEmpty(pEntUserPolicyTracking.ID))
                {
                    //assign new id
                    pEntUserPolicyTracking.ID = Schema.Common.VAL_ATTEMPT_ID_PREFIX + YPLMS.Services.IDGenerator.GetUniqueKey(Schema.Common.VAL_ATTEMPT_ID_LENGTH);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                }
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_ATTEMPT_ID, pEntUserPolicyTracking.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPolicyTracking.PARA_POLICY_ID, pEntUserPolicyTracking.PolicyId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPolicyTracking.PARA_COMPLETION_STATUS, pEntUserPolicyTracking.CompletionStatus.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntUserPolicyTracking.DateOfCompletion != null)
                    _sqlpara = new SqlParameter(Schema.UserPolicyTracking.PARA_DATE_OF_COMPLETION, pEntUserPolicyTracking.DateOfCompletion);
                else
                    _sqlpara = new SqlParameter(Schema.UserPolicyTracking.PARA_DATE_OF_COMPLETION, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPolicyTracking.PARA_SYSTEM_USER_GUID, pEntUserPolicyTracking.SystemUserGUID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntUserPolicyTracking.MarkedCompletedById))
                    _sqlpara = new SqlParameter(Schema.UserPolicyTracking.PARA_MARKED_COMPLETED_BY_ID, pEntUserPolicyTracking.MarkedCompletedById);
                else
                    _sqlpara = new SqlParameter(Schema.UserPolicyTracking.PARA_MARKED_COMPLETED_BY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntUserPolicyTracking.ScannedCertificationFileName))
                    _sqlpara = new SqlParameter(Schema.UserPolicyTracking.PARA_SCANNED_CERTIFICATION_FILE_NAME, pEntUserPolicyTracking.ScannedCertificationFileName);
                else
                    _sqlpara = new SqlParameter(Schema.UserPolicyTracking.PARA_SCANNED_CERTIFICATION_FILE_NAME, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                //ActivityName and UserFirstLastName is Read Only
                if (!string.IsNullOrEmpty(pEntUserPolicyTracking.ReviewComments))
                    _sqlpara = new SqlParameter(Schema.UserPolicyTracking.PARA_REVIEWER_COMMENTS, pEntUserPolicyTracking.ReviewComments);
                else
                    _sqlpara = new SqlParameter(Schema.UserPolicyTracking.PARA_REVIEWER_COMMENTS, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserPolicyTracking.PARA_IS_FOR_ADMIN_PREVIEW, pEntUserPolicyTracking.IsForAdminPreview);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntUserPolicyTracking;
        }

        /// <summary>
        /// Bulk Add/Update
        /// </summary>
        /// <param name="pEntListBusinessRuleUsers"></param>
        /// <returns>List of added/updated BusinessRuleUsers</returns>
        public List<UserPolicyTracking> BulkMarkCompleted(List<UserPolicyTracking> pEntListUserPolicyTrackingBase, bool pIsBulkMarkCompleted)
        {
            _sqlObject = new SQLObject();
            List<UserPolicyTracking> entListUserPolicyTracking = new List<UserPolicyTracking>();
            SqlDataAdapter sqladapter = null;
            DataTable dtable;
            SqlCommand sqlcmdDel;
            int iBatchSize = 0;
            DataRow drow = null;
            EntityRange entRange = new EntityRange();
            string strMode = string.Empty;
            try
            {
                if (pEntListUserPolicyTrackingBase.Count > 0)
                {
                    List<UserPolicyTracking> entListUserPolicyTrackingTemp = new List<UserPolicyTracking>();
                    entListUserPolicyTrackingTemp.Add(pEntListUserPolicyTrackingBase[0]);

                    _strConnString = _sqlObject.GetClientDBConnString(entListUserPolicyTrackingTemp[0].ClientId);
                    _sqlConnection = new SqlConnection(_strConnString);
                    dtable = new DataTable();
                    dtable.Columns.Add(Schema.Common.COL_ATTEMPT_ID);
                    dtable.Columns.Add(Schema.UserPolicyTracking.COL_POLICY_ID);
                    dtable.Columns.Add(Schema.UserPolicyTracking.COL_COMPLETION_STATUS);
                    dtable.Columns.Add(Schema.UserPolicyTracking.COL_DATE_OF_COMPLETION);
                    dtable.Columns.Add(Schema.UserPolicyTracking.COL_SYSTEM_USER_GUID);
                    dtable.Columns.Add(Schema.UserPolicyTracking.COL_MARKED_COMPLETED_BY_ID);
                    dtable.Columns.Add(Schema.UserPolicyTracking.COL_SCANNED_CERTIFICATION_FILE_NAME);
                    dtable.Columns.Add(Schema.UserPolicyTracking.COL_REVIEWER_COMMENTS);
                    dtable.Columns.Add(Schema.UserPolicyTracking.COL_IS_FOR_ADMIN_PREVIEW);
                    dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);
                    dtable.Columns.Add(Schema.Common.COL_IS_BULK_MARK_COMPLETED);

                    foreach (UserPolicyTracking entUserPolicyTracking in pEntListUserPolicyTrackingBase)
                    {

                        drow = dtable.NewRow();

                        if (!String.IsNullOrEmpty(entUserPolicyTracking.ID))
                        {
                            drow[Schema.Common.COL_ATTEMPT_ID] = entUserPolicyTracking.ID;
                            strMode = Schema.Common.VAL_UPDATE_MODE;
                        }
                        else
                        {
                            entUserPolicyTracking.ID = Schema.Common.VAL_ATTEMPT_ID_PREFIX + YPLMS.Services.IDGenerator.GetUniqueKey(Schema.Common.VAL_ATTEMPT_ID_LENGTH);
                            drow[Schema.Common.COL_ATTEMPT_ID] = entUserPolicyTracking.ID;
                            strMode = Schema.Common.VAL_INSERT_MODE;
                        }

                        drow[Schema.UserPolicyTracking.COL_POLICY_ID] = entUserPolicyTracking.PolicyId;
                        drow[Schema.UserPolicyTracking.COL_COMPLETION_STATUS] = entUserPolicyTracking.CompletionStatus;
                        drow[Schema.UserPolicyTracking.COL_DATE_OF_COMPLETION] = entUserPolicyTracking.DateOfCompletion;
                        drow[Schema.UserPolicyTracking.COL_SYSTEM_USER_GUID] = entUserPolicyTracking.SystemUserGUID;
                        drow[Schema.UserPolicyTracking.COL_MARKED_COMPLETED_BY_ID] = entUserPolicyTracking.MarkedCompletedById;
                        drow[Schema.UserPolicyTracking.COL_SCANNED_CERTIFICATION_FILE_NAME] = entUserPolicyTracking.ScannedCertificationFileName;
                        drow[Schema.UserPolicyTracking.COL_REVIEWER_COMMENTS] = entUserPolicyTracking.ReviewComments;
                        drow[Schema.UserPolicyTracking.COL_IS_FOR_ADMIN_PREVIEW] = entUserPolicyTracking.IsForAdminPreview;
                        drow[Schema.Common.COL_UPDATE_MODE] = strMode;
                        drow[Schema.Common.COL_IS_BULK_MARK_COMPLETED] = entUserPolicyTracking.IsBulkMarkCompleted;

                        dtable.Rows.Add(drow);
                        entListUserPolicyTracking.Add(entUserPolicyTracking);
                        iBatchSize++;
                    }

                    if (dtable.Rows.Count > 0)
                    {
                        sqlcmdDel = new SqlCommand(Schema.UserPolicyTracking.PROC_UPDATE_USER_POLICY_TRACKING, _sqlConnection);
                        sqlcmdDel.CommandType = CommandType.StoredProcedure;

                        sqlcmdDel.Parameters.Add(Schema.Common.PARA_ATTEMPT_ID, SqlDbType.NVarChar, 100, Schema.Common.COL_ATTEMPT_ID);
                        sqlcmdDel.Parameters.Add(Schema.UserPolicyTracking.PARA_POLICY_ID, SqlDbType.NVarChar, 100, Schema.UserPolicyTracking.COL_POLICY_ID);
                        sqlcmdDel.Parameters.Add(Schema.UserPolicyTracking.PARA_COMPLETION_STATUS, SqlDbType.NVarChar, 100, Schema.UserPolicyTracking.COL_COMPLETION_STATUS);
                        sqlcmdDel.Parameters.Add(Schema.UserPolicyTracking.PARA_DATE_OF_COMPLETION, SqlDbType.DateTime, 100, Schema.UserPolicyTracking.COL_DATE_OF_COMPLETION);
                        sqlcmdDel.Parameters.Add(Schema.UserPolicyTracking.PARA_SYSTEM_USER_GUID, SqlDbType.NVarChar, 100, Schema.UserPolicyTracking.COL_SYSTEM_USER_GUID);
                        sqlcmdDel.Parameters.Add(Schema.UserPolicyTracking.PARA_MARKED_COMPLETED_BY_ID, SqlDbType.NVarChar, 100, Schema.UserPolicyTracking.COL_MARKED_COMPLETED_BY_ID);
                        sqlcmdDel.Parameters.Add(Schema.UserPolicyTracking.PARA_SCANNED_CERTIFICATION_FILE_NAME, SqlDbType.NVarChar, 500, Schema.UserPolicyTracking.COL_SCANNED_CERTIFICATION_FILE_NAME);
                        sqlcmdDel.Parameters.Add(Schema.UserPolicyTracking.PARA_REVIEWER_COMMENTS, SqlDbType.NVarChar, 2000, Schema.UserPolicyTracking.COL_REVIEWER_COMMENTS);
                        sqlcmdDel.Parameters.Add(Schema.UserPolicyTracking.PARA_IS_FOR_ADMIN_PREVIEW, SqlDbType.Bit, 10, Schema.UserPolicyTracking.COL_IS_FOR_ADMIN_PREVIEW);
                        sqlcmdDel.Parameters.Add(Schema.Common.PARA_IS_BULK_MARK_COMPLETED, SqlDbType.Bit, 1, Schema.Common.COL_IS_BULK_MARK_COMPLETED);
                        sqlcmdDel.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.NVarChar, 100, Schema.Common.COL_UPDATE_MODE);

                        sqladapter = new SqlDataAdapter();
                        sqladapter.InsertCommand = sqlcmdDel;
                        sqladapter.InsertCommand.CommandTimeout = 0;
                        sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        sqladapter.UpdateBatchSize = iBatchSize;
                        sqladapter.Update(dtable);
                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListUserPolicyTracking;
        }

        /// <summary>
        /// To update FileName
        /// </summary>
        /// <param name="pEntUserPolicyTracking"></param>        
        /// <returns>UserPolicyTracking object</returns>
        public UserPolicyTracking UpdateScannedFileName(UserPolicyTracking pEntUserPolicyTracking)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Common.PROC_UPDATE_SCANNED_FILE_NAME;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntUserPolicyTracking.ClientId);
                if (!string.IsNullOrEmpty(pEntUserPolicyTracking.PolicyId))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_ACTIVITY_ID, pEntUserPolicyTracking.PolicyId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntUserPolicyTracking.SystemUserGUID))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SYSTEM_USER_GUID, pEntUserPolicyTracking.SystemUserGUID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntUserPolicyTracking.ScannedCertificationFileName))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SCANNED_FILE_NAME, pEntUserPolicyTracking.ScannedCertificationFileName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlpara = new SqlParameter(Schema.Common.PARA_ACTIVITY_TYPE, ActivityContentType.Policy.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntUserPolicyTracking;
        }

        public UserPolicyTracking GetActivityStatus(UserPolicyTracking pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            UserPolicyTracking entActivityAssignment = new UserPolicyTracking();
            SqlConnection sqlConnection = null;
            int iIndex;

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_GET_ACTIVITY_STATUS, sqlConnection);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.PolicyId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.SystemUserGUID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, ActivityContentType.Policy.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();

                if (_sqlreader.HasRows)
                {
                    iIndex = _sqlreader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_STATUS);
                    if (!_sqlreader.IsDBNull(iIndex))
                    {
                        entActivityAssignment.CompletionStatus = (ActivityCompletionStatus)Enum.Parse(typeof(ActivityCompletionStatus), _sqlreader.GetString(iIndex));
                    }
                }

                // //return entActivityAssignment;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entActivityAssignment;
        }
        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntUserPolicyTracking"></param>
        /// <returns></returns>
        public UserPolicyTracking Get(UserPolicyTracking pEntUserPolicyTracking)
        {
            return GetUserPolicyTrackingById(pEntUserPolicyTracking);
        }
        /// <summary>
        /// Update UserPolicyTracking
        /// </summary>
        /// <param name="pEntUserPolicyTracking"></param>
        /// <returns></returns>
        public UserPolicyTracking Update(UserPolicyTracking pEntUserPolicyTracking)
        {
            return AddUpdateUserPolicyTracking(pEntUserPolicyTracking);
        }
        #endregion
    }
}
