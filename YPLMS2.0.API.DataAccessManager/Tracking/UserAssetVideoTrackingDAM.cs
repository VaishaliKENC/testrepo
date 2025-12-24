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
    /// class UserAssetVideoTrackingDAM
    /// </summary>
    public class UserAssetVideoTrackingDAM : IDataManager<UserAssetVideoTracking>
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
        /// <returns>List of UserAssetVideoTracking Object</returns>
        private UserAssetVideoTracking FillObjectUser(SqlDataReader pSqlReader, bool pRangeList)
        {
            UserAssetVideoTracking entUserAssetVideoTracking = new UserAssetVideoTracking();
            int iIndex = 0;
            if (pSqlReader.HasRows)
            {
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.UserAssetVideoTracking.COL_ATTEMPT_VIDEO_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.UserAssetVideoTracking.COL_ATTEMPT_VIDEO_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entUserAssetVideoTracking.AttemptVideoId = pSqlReader.GetString(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.Common.COL_ATTEMPT_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_ATTEMPT_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entUserAssetVideoTracking.ID = pSqlReader.GetString(iIndex);
                }

                iIndex = pSqlReader.GetOrdinal(Schema.UserAssetVideoTracking.COL_ASSET_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserAssetVideoTracking.AssetId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserAssetVideoTracking.COL_SYSTEM_USER_GUID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserAssetVideoTracking.SystemUserGUID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserAssetVideoTracking.COL_ACTIVITY_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserAssetVideoTracking.ActivityName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.UserAssetVideoTracking.COL_WATCHEDINMINS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entUserAssetVideoTracking.WatchedInMins = pSqlReader.GetDecimal(iIndex);

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    entUserAssetVideoTracking.ListRange = _entRange;
                }
            }
            return entUserAssetVideoTracking;
        }

        /// <summary>
        /// Get UserAssetVideoTracking Details By Id
        /// </summary>
        /// <returns>List of UserAssetVideoTracking Object</returns>
        public UserAssetVideoTracking GetUserAssetVideoTrackingById(UserAssetVideoTracking pEntUserAssetVideoTracking)
        {
            _sqlObject = new SQLObject();
            UserAssetVideoTracking entUserAssetVideoTracking = null;
            _strConnString = _sqlObject.GetClientDBConnString(pEntUserAssetVideoTracking.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.UserAssetVideoTracking.PROC_GET_USER_ASSET_TRACKING, _sqlConnection);
            _sqlpara = new SqlParameter(Schema.Common.PARA_ATTEMPT_ID, pEntUserAssetVideoTracking.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            entUserAssetVideoTracking = new UserAssetVideoTracking();
            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    entUserAssetVideoTracking = FillObjectUser(_sqlreader, false);
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
            return entUserAssetVideoTracking;
        }

        public UserAssetVideoTracking GetVideoTracking(UserAssetVideoTracking pEntUserAssetVideoTracking)
        {
            _sqlObject = new SQLObject();
            UserAssetVideoTracking entVideoTrackingLib = new UserAssetVideoTracking();
            EntityRange entRange = new EntityRange();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntUserAssetVideoTracking.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.UserAssetVideoTracking.PROC_GET_USER_ASSET_TRACKING_ATTEMPT_COUNT, sqlConnection);

                _sqlpara = new SqlParameter(Schema.Common.PARA_ATTEMPT_ID, pEntUserAssetVideoTracking.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                Object obj = null;
                obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
                if (obj != null)
                {
                    entRange.TotalRows = Convert.ToInt32(obj);
                }
                entVideoTrackingLib.ListRange = entRange;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entVideoTrackingLib;
        }
        /// <summary>
        /// To update/add new Email Template
        /// </summary>
        /// <param name="pEntUserAssetVideoTracking"></param>        
        /// <returns>UserAssetVideoTracking object</returns>
        public UserAssetVideoTracking AddUpdateUserAssetVideoTracking(UserAssetVideoTracking pEntUserAssetVideoTracking)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.UserAssetVideoTracking.PROC_UPDATE_USER_ASSET_TRACKING;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntUserAssetVideoTracking.ClientId);
                if (string.IsNullOrEmpty(pEntUserAssetVideoTracking.AttemptVideoId))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                }
                else
                {
                    pEntUserAssetVideoTracking.AttemptVideoId = Schema.Common.VAL_ATTEMPTVIDEO_ID_PREFIX + pEntUserAssetVideoTracking.AttemptVideoId;
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                }
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetVideoTracking.COL_ATTEMPT_VIDEO_ID, pEntUserAssetVideoTracking.AttemptVideoId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_ATTEMPT_ID, pEntUserAssetVideoTracking.AttemptId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetVideoTracking.PARA_ASSET_ID, pEntUserAssetVideoTracking.AssetId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetVideoTracking.PARA_ACTIVITY_NAME, pEntUserAssetVideoTracking.ActivityName);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntUserAssetVideoTracking.DateOfStart != null)
                    _sqlpara = new SqlParameter(Schema.UserAssetVideoTracking.PARA_DATE_OF_START, pEntUserAssetVideoTracking.DateOfStart);
                else
                    _sqlpara = new SqlParameter(Schema.UserAssetVideoTracking.PARA_DATE_OF_START, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetVideoTracking.PARA_SYSTEM_USER_GUID, pEntUserAssetVideoTracking.SystemUserGUID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetVideoTracking.PARA_WATCHEDINMINS, pEntUserAssetVideoTracking.WatchedInMins);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetVideoTracking.PARA_TOTAL_VIDEO_DURATION_INMINS, pEntUserAssetVideoTracking.TotalVideoDurationInMins);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntUserAssetVideoTracking;
        }

        public UserAssetVideoTracking UpdateUserAssetVideoTrackingVideo(UserAssetVideoTracking pEntUserAssetVideoTracking)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.UserAssetVideoTracking.PROC_UPDATE_USER_ASSET_TRACKING_VIDEO;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntUserAssetVideoTracking.ClientId);

                _sqlpara = new SqlParameter(Schema.Common.PARA_ATTEMPT_ID, pEntUserAssetVideoTracking.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetVideoTracking.PARA_ASSET_ID, pEntUserAssetVideoTracking.AssetId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetVideoTracking.PARA_SYSTEM_USER_GUID, pEntUserAssetVideoTracking.SystemUserGUID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.UserAssetVideoTracking.PARA_WATCHEDINMINS, pEntUserAssetVideoTracking.WatchedInMins);
                _sqlcmd.Parameters.Add(_sqlpara);



                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntUserAssetVideoTracking;
        }

        /// <summary>
        /// To update FileName
        /// </summary>
        /// <param name="pEntUserAssetVideoTracking"></param>        
        /// <returns>UserAssetVideoTracking object</returns>
        public UserAssetVideoTracking UpdateScannedFileName(UserAssetVideoTracking pEntUserAssetVideoTracking)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.UserAssetVideoTracking.PROC_UPDATE_USER_ASSET_TRACKING_VIDEO;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntUserAssetVideoTracking.ClientId);

                if (!string.IsNullOrEmpty(pEntUserAssetVideoTracking.AttemptVideoId))
                {
                    _sqlpara = new SqlParameter(Schema.UserAssetVideoTracking.PARA_ATTEMPT_VIDEO_ID, pEntUserAssetVideoTracking.AttemptVideoId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntUserAssetVideoTracking.AssetId))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_ACTIVITY_ID, pEntUserAssetVideoTracking.AssetId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntUserAssetVideoTracking.SystemUserGUID))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SYSTEM_USER_GUID, pEntUserAssetVideoTracking.SystemUserGUID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntUserAssetVideoTracking.WatchedInMins > 0)
                {
                    _sqlpara = new SqlParameter(Schema.UserAssetVideoTracking.PARA_WATCHEDINMINS, pEntUserAssetVideoTracking.WatchedInMins);
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
            return pEntUserAssetVideoTracking;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntUserAssetVideoTracking"></param>
        /// <returns></returns>
        public UserAssetVideoTracking Get(UserAssetVideoTracking pEntUserAssetVideoTracking)
        {
            return GetUserAssetVideoTrackingById(pEntUserAssetVideoTracking);
        }
        /// <summary>
        /// Update UserAssetVideoTracking
        /// </summary>
        /// <param name="pEntUserAssetVideoTracking"></param>
        /// <returns></returns>
        public UserAssetVideoTracking Update(UserAssetVideoTracking pEntUserAssetVideoTracking)
        {
            return AddUpdateUserAssetVideoTracking(pEntUserAssetVideoTracking);
        }
        #endregion
    }
}
