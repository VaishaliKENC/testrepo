using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    /// <summary>
    /// class UserAssetTrackingDAM
    /// </summary>
    public class UserAssetTrackingDAM : IDataManager<UserAssetTracking>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlParameter _sqlpara = null;
        SqlCommand _sqlcmd = null;
        EntityRange _entRange = null;
        SqlConnection _sqlConnection = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.EmailTemplate.DL_ERROR;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// Fill object - user
        /// </summary>
        /// <returns>List of UserAssetTracking Object</returns>
        private UserAssetTracking FillObjectUser(SqlDataReader pSqlReader, bool pRangeList)
        {
            UserAssetTracking entUserAssetTracking = new UserAssetTracking();
            int iIndex = 0;
            if (pSqlReader.HasRows)
            {


                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.Common.COL_ATTEMPT_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_ATTEMPT_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entUserAssetTracking.ID = pSqlReader.GetString(iIndex);
                }

                iIndex = pSqlReader.GetOrdinal(Schema.UserAssetTracking.COL_ASSET_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserAssetTracking.AssetId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserAssetTracking.COL_SYSTEM_USER_GUID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserAssetTracking.SystemUserGUID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserAssetTracking.COL_COMPLETION_STATUS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserAssetTracking.CompletionStatus = (ActivityCompletionStatus)Enum.Parse(typeof(ActivityCompletionStatus),
                        pSqlReader.GetString(iIndex));

                iIndex = pSqlReader.GetOrdinal(Schema.UserAssetTracking.COL_DATE_OF_COMPLETION);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserAssetTracking.DateOfCompletion = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserAssetTracking.COL_MARKED_COMPLETED_BY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserAssetTracking.MarkedCompletedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserAssetTracking.COL_SCANNED_CERTIFICATION_FILE_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserAssetTracking.ScannedCertificationFileName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserAssetTracking.COL_ACTIVITY_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserAssetTracking.ActivityName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserAssetTracking.COL_WATCHEDINMINS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserAssetTracking.WatchedInMins = pSqlReader.GetDecimal(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserAssetTracking.COL_BOOKMARK_WATCHEDINMINS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserAssetTracking.BookmarkWatchedInMins = pSqlReader.GetDecimal(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserAssetTracking.COL_PROGRESS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserAssetTracking.Progress = pSqlReader.GetDecimal(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserAssetTracking.COL_IS_FOR_ADMIN_PREVIEW);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserAssetTracking.IsForAdminPreview = pSqlReader.GetBoolean(iIndex);

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    entUserAssetTracking.ListRange = _entRange;
                }
            }
            return entUserAssetTracking;
        }

        /// <summary>
        /// Get UserAssetTracking Details By Id
        /// </summary>
        /// <returns>List of UserAssetTracking Object</returns>
        public UserAssetTracking GetUserAssetTrackingById(UserAssetTracking pEntUserAssetTracking)
        {
            _sqlObject = new SQLObject();
            UserAssetTracking entUserAssetTracking = null;
            _strConnString = _sqlObject.GetClientDBConnString(pEntUserAssetTracking.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.UserAssetTracking.PROC_GET_USER_ASSET_TRACKING, _sqlConnection);
            _sqlpara = new SqlParameter(Schema.Common.PARA_ATTEMPT_ID, pEntUserAssetTracking.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            entUserAssetTracking = new UserAssetTracking();
            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    entUserAssetTracking = FillObjectUser(_sqlreader, false);
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
            return entUserAssetTracking;
        }

        /// <summary>
        /// To update/add new Email Template
        /// </summary>
        /// <param name="pEntUserAssetTracking"></param>        
        /// <returns>UserAssetTracking object</returns>
        public UserAssetTracking AddUpdateUserAssetTracking(UserAssetTracking pEntUserAssetTracking)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.UserAssetTracking.PROC_UPDATE_USER_ASSET_TRACKING;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntUserAssetTracking.ClientId);
                if (string.IsNullOrEmpty(pEntUserAssetTracking.ID))
                {
                    pEntUserAssetTracking.ID = Schema.Common.VAL_ATTEMPT_ID_PREFIX + YPLMS.Services.IDGenerator.GetUniqueKey(Schema.Common.VAL_ATTEMPT_ID_LENGTH);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                }
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_ATTEMPT_ID, pEntUserAssetTracking.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_ASSET_ID, pEntUserAssetTracking.AssetId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_COMPLETION_STATUS, pEntUserAssetTracking.CompletionStatus.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntUserAssetTracking.DateOfCompletion != null)
                    _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_DATE_OF_COMPLETION, pEntUserAssetTracking.DateOfCompletion);
                else
                    _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_DATE_OF_COMPLETION, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntUserAssetTracking.DateOfStart != null)
                    _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_DATE_OF_START, pEntUserAssetTracking.DateOfStart);
                else
                    _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_DATE_OF_START, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_SYSTEM_USER_GUID, pEntUserAssetTracking.SystemUserGUID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntUserAssetTracking.MarkedCompletedById))
                    _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_MARKED_COMPLETED_BY_ID, pEntUserAssetTracking.MarkedCompletedById);
                else
                    _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_MARKED_COMPLETED_BY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntUserAssetTracking.ScannedCertificationFileName))
                    _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_SCANNED_CERTIFICATION_FILE_NAME, pEntUserAssetTracking.ScannedCertificationFileName);
                else
                    _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_SCANNED_CERTIFICATION_FILE_NAME, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                //ActivityName and UserFirstLastName is Read Only                
                if (!string.IsNullOrEmpty(pEntUserAssetTracking.ReviewComments))
                    _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_REVIEWER_COMMENTS, pEntUserAssetTracking.ReviewComments);
                else
                    _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_REVIEWER_COMMENTS, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_IS_FOR_ADMIN_PREVIEW, pEntUserAssetTracking.IsForAdminPreview);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_WATCHEDINMINS, pEntUserAssetTracking.WatchedInMins);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_PROGRESS, pEntUserAssetTracking.Progress);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntUserAssetTracking;
        }

        public UserAssetTracking UpdateUserAssetTrackingVideo(UserAssetTracking pEntUserAssetTracking)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.UserAssetTracking.PROC_UPDATE_USER_ASSET_TRACKING_VIDEO;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntUserAssetTracking.ClientId);

                _sqlpara = new SqlParameter(Schema.Common.PARA_ATTEMPT_ID, pEntUserAssetTracking.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_ASSET_ID, pEntUserAssetTracking.AssetId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_SYSTEM_USER_GUID, pEntUserAssetTracking.SystemUserGUID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_WATCHEDINMINS, pEntUserAssetTracking.WatchedInMins);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_PROGRESS, pEntUserAssetTracking.Progress);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_TOTAL_VIDEO_DURATION_IN_MINS, pEntUserAssetTracking.TotalVideoDurationInMins);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntUserAssetTracking;
        }

        /// <summary>
        /// Bulk Add/Update
        /// </summary>
        /// <param name="pEntListBusinessRuleUsers"></param>
        /// <returns>List of added/updated BusinessRuleUsers</returns>
        public List<UserAssetTracking> BulkMarkCompleted(List<UserAssetTracking> pEntListUserAssetTrackingBase, bool pIsBulkMarkCompleted)
        {
            List<UserAssetTracking> entListUserAssetTracking = new List<UserAssetTracking>();
            SqlDataAdapter sqladapter = null;
            DataTable dtable;
            SqlCommand sqlcmdDel;
            int iBatchSize = 0;
            DataRow drow = null;
            EntityRange entRange = new EntityRange();
            _sqlObject = new SQLObject();
            string strMode = string.Empty;
            try
            {
                if (pEntListUserAssetTrackingBase.Count > 0)
                {
                    List<UserAssetTracking> entListUserAssetTrackingTemp = new List<UserAssetTracking>();
                    entListUserAssetTrackingTemp.Add(pEntListUserAssetTrackingBase[0]);

                    _strConnString = _sqlObject.GetClientDBConnString(entListUserAssetTrackingTemp[0].ClientId);
                    _sqlConnection = new SqlConnection(_strConnString);
                    dtable = new DataTable();
                    dtable.Columns.Add(Schema.Common.COL_ATTEMPT_ID);
                    dtable.Columns.Add(Schema.UserAssetTracking.COL_ASSET_ID);
                    dtable.Columns.Add(Schema.UserAssetTracking.COL_COMPLETION_STATUS);
                    dtable.Columns.Add(Schema.UserAssetTracking.COL_DATE_OF_COMPLETION);
                    dtable.Columns.Add(Schema.UserAssetTracking.COL_SYSTEM_USER_GUID);
                    dtable.Columns.Add(Schema.UserAssetTracking.COL_MARKED_COMPLETED_BY_ID);
                    dtable.Columns.Add(Schema.UserAssetTracking.COL_SCANNED_CERTIFICATION_FILE_NAME);
                    dtable.Columns.Add(Schema.UserAssetTracking.COL_REVIEWER_COMMENTS);
                    dtable.Columns.Add(Schema.UserAssetTracking.COL_IS_FOR_ADMIN_PREVIEW);
                    dtable.Columns.Add(Schema.UserAssetTracking.COL_PROGRESS);
                    dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);
                    dtable.Columns.Add(Schema.Common.COL_IS_BULK_MARK_COMPLETED);

                    foreach (UserAssetTracking entUserAssetTracking in pEntListUserAssetTrackingBase)
                    {

                        drow = dtable.NewRow();

                        if (!String.IsNullOrEmpty(entUserAssetTracking.ID))
                        {
                            drow[Schema.Common.COL_ATTEMPT_ID] = entUserAssetTracking.ID;
                            strMode = Schema.Common.VAL_UPDATE_MODE;
                        }
                        else
                        {
                            entUserAssetTracking.ID = Schema.Common.VAL_ATTEMPT_ID_PREFIX + YPLMS.Services.IDGenerator.GetUniqueKey(Schema.Common.VAL_ATTEMPT_ID_LENGTH);
                            drow[Schema.Common.COL_ATTEMPT_ID] = entUserAssetTracking.ID;
                            strMode = Schema.Common.VAL_INSERT_MODE;
                        }

                        drow[Schema.UserAssetTracking.COL_ASSET_ID] = entUserAssetTracking.AssetId;
                        drow[Schema.UserAssetTracking.COL_COMPLETION_STATUS] = entUserAssetTracking.CompletionStatus;
                        drow[Schema.UserAssetTracking.COL_DATE_OF_COMPLETION] = entUserAssetTracking.DateOfCompletion;
                        drow[Schema.UserAssetTracking.COL_SYSTEM_USER_GUID] = entUserAssetTracking.SystemUserGUID;
                        drow[Schema.UserAssetTracking.COL_MARKED_COMPLETED_BY_ID] = entUserAssetTracking.MarkedCompletedById;
                        drow[Schema.UserAssetTracking.COL_SCANNED_CERTIFICATION_FILE_NAME] = entUserAssetTracking.ScannedCertificationFileName;
                        drow[Schema.UserAssetTracking.COL_REVIEWER_COMMENTS] = entUserAssetTracking.ReviewComments;
                        drow[Schema.UserAssetTracking.COL_IS_FOR_ADMIN_PREVIEW] = entUserAssetTracking.IsForAdminPreview;
                        drow[Schema.UserAssetTracking.COL_PROGRESS] = entUserAssetTracking.Progress;
                        drow[Schema.Common.COL_UPDATE_MODE] = strMode;
                        drow[Schema.Common.COL_IS_BULK_MARK_COMPLETED] = entUserAssetTracking.IsBulkMarkCompleted;

                        dtable.Rows.Add(drow);
                        entListUserAssetTracking.Add(entUserAssetTracking);
                        iBatchSize++;
                    }

                    if (dtable.Rows.Count > 0)
                    {
                        sqlcmdDel = new SqlCommand(Schema.UserAssetTracking.PROC_MARK_COMPLETED, _sqlConnection);
                        sqlcmdDel.CommandType = CommandType.StoredProcedure;

                        sqlcmdDel.Parameters.Add(Schema.Common.PARA_ATTEMPT_ID, SqlDbType.NVarChar, 100, Schema.Common.COL_ATTEMPT_ID);
                        sqlcmdDel.Parameters.Add(Schema.UserAssetTracking.PARA_ASSET_ID, SqlDbType.NVarChar, 100, Schema.UserAssetTracking.COL_ASSET_ID);
                        sqlcmdDel.Parameters.Add(Schema.UserAssetTracking.PARA_COMPLETION_STATUS, SqlDbType.NVarChar, 100, Schema.UserAssetTracking.COL_COMPLETION_STATUS);
                        sqlcmdDel.Parameters.Add(Schema.UserAssetTracking.PARA_DATE_OF_COMPLETION, SqlDbType.DateTime, 100, Schema.UserAssetTracking.COL_DATE_OF_COMPLETION);
                        sqlcmdDel.Parameters.Add(Schema.UserAssetTracking.PARA_SYSTEM_USER_GUID, SqlDbType.NVarChar, 100, Schema.UserAssetTracking.COL_SYSTEM_USER_GUID);
                        sqlcmdDel.Parameters.Add(Schema.UserAssetTracking.PARA_MARKED_COMPLETED_BY_ID, SqlDbType.NVarChar, 100, Schema.UserAssetTracking.COL_MARKED_COMPLETED_BY_ID);
                        sqlcmdDel.Parameters.Add(Schema.UserAssetTracking.PARA_SCANNED_CERTIFICATION_FILE_NAME, SqlDbType.NVarChar, 500, Schema.UserAssetTracking.COL_SCANNED_CERTIFICATION_FILE_NAME);
                        sqlcmdDel.Parameters.Add(Schema.UserAssetTracking.PARA_REVIEWER_COMMENTS, SqlDbType.NVarChar, 2000, Schema.UserAssetTracking.COL_REVIEWER_COMMENTS);
                        sqlcmdDel.Parameters.Add(Schema.UserAssetTracking.PARA_IS_FOR_ADMIN_PREVIEW, SqlDbType.Bit, 10, Schema.UserAssetTracking.COL_IS_FOR_ADMIN_PREVIEW);
                        sqlcmdDel.Parameters.Add(Schema.Common.PARA_IS_BULK_MARK_COMPLETED, SqlDbType.Bit, 1, Schema.Common.COL_IS_BULK_MARK_COMPLETED);
                        sqlcmdDel.Parameters.Add(Schema.UserAssetTracking.PARA_PROGRESS, SqlDbType.Decimal, 10, Schema.UserAssetTracking.COL_PROGRESS);
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
            return entListUserAssetTracking;
        }

        /// <summary>
        /// To update FileName
        /// </summary>
        /// <param name="pEntUserAssetTracking"></param>        
        /// <returns>UserAssetTracking object</returns>
        public UserAssetTracking UpdateScannedFileName(UserAssetTracking pEntUserAssetTracking)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Common.PROC_UPDATE_SCANNED_FILE_NAME;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntUserAssetTracking.ClientId);
                if (!string.IsNullOrEmpty(pEntUserAssetTracking.AssetId))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_ACTIVITY_ID, pEntUserAssetTracking.AssetId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntUserAssetTracking.SystemUserGUID))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SYSTEM_USER_GUID, pEntUserAssetTracking.SystemUserGUID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntUserAssetTracking.ScannedCertificationFileName))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SCANNED_FILE_NAME, pEntUserAssetTracking.ScannedCertificationFileName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlpara = new SqlParameter(Schema.Common.PARA_ACTIVITY_TYPE, ActivityContentType.Asset.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntUserAssetTracking;
        }

        ///

        public UserAssetTracking UpdateUserAssetVideoBookmark(UserAssetTracking pEntUserAssetTracking)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.UserAssetTracking.PROC_UPDATE_BOOKMARK_ASSET_VIDEO;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntUserAssetTracking.ClientId);

                _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_ASSET_ID, pEntUserAssetTracking.AssetId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_SYSTEM_USER_GUID, pEntUserAssetTracking.SystemUserGUID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_BOOKMARK_WATCHEDINMINS, pEntUserAssetTracking.BookmarkWatchedInMins);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetTracking.PARA_TOTAL_VIDEO_DURATION_IN_MINS, pEntUserAssetTracking.TotalVideoDurationInMins);
                _sqlcmd.Parameters.Add(_sqlpara);
                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntUserAssetTracking;
        }
        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntUserAssetTracking"></param>
        /// <returns></returns>
        public UserAssetTracking Get(UserAssetTracking pEntUserAssetTracking)
        {
            return GetUserAssetTrackingById(pEntUserAssetTracking);
        }
        /// <summary>
        /// Update UserAssetTracking
        /// </summary>
        /// <param name="pEntUserAssetTracking"></param>
        /// <returns></returns>
        public UserAssetTracking Update(UserAssetTracking pEntUserAssetTracking)
        {
            return AddUpdateUserAssetTracking(pEntUserAssetTracking);
        }
        ///UpdateVideoBookmark
        public UserAssetTracking UpdateVideoBookmark(UserAssetTracking pEntUserAssetTracking)
        {
            return UpdateUserAssetVideoBookmark(pEntUserAssetTracking);
        }

        #endregion
    }
}
