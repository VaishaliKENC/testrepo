using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager.VirtualTraining
{
    public class VirtualTrainingAttendeeAdaptor : IDataManager<VirtualTrainingAttendeeMaster>
    {

        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        VirtualTrainingAttendeeMaster _entVirtualTrainingAttendeeMaster = null;
        List<VirtualTrainingAttendeeMaster> _entListVirtualTrainingAttendeeMaster = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.VirtualTrainingAttendeeMaster.VIRTUALTRAININGATTENDEEMASTER_DAM_ERROR;
        DataTable _dtable = null;
        SqlDataAdapter _sqladapter = null;
        List<VirtualTrainingAttendeeMaster> _entListLearner = null;
        UserAdminRole entUserAdminRole = null;
        VirtualTrainingAttendeeMaster entLearner = new VirtualTrainingAttendeeMaster();

        #endregion
        /// <summary>
        /// Default constructor
        /// </summary>
        /// 
        public VirtualTrainingAttendeeAdaptor()
        { }

        private VirtualTrainingAttendeeMaster GetVirtualTrainingAttendeeMasterByID(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingAttendeeMaster.PROC_VIRTUALTRAINING_ATTENDEE_GET;
            _sqlObject = new SQLObject();

            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.SystemUserGUID))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING__ATTENDEE_SYSTEMUSERGUID, pEntVirtualTrainingAttendeeMaster.SystemUserGUID.ToString());
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING__ATTENDEE_SYSTEMUSERGUID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.TrainingID))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, pEntVirtualTrainingAttendeeMaster.TrainingID.ToString());
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingAttendeeMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entVirtualTrainingAttendeeMaster = FillObject(_sqlreader, false, _sqlObject);
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
            return _entVirtualTrainingAttendeeMaster;
        }



        /// <summary>
        /// VirtualTrainingAttendeeMaster Range
        /// </summary>
        /// <param name="pEntVirtualTrainingAttendeeMaster"></param>
        /// <returns></returns>

        private VirtualTrainingAttendeeMaster GetVirtualTrainingAttendeeMasterByEmailID(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingAttendeeMaster.PROC_VIRTUALTRAINING_ATTENDEE_EMAIL_EXIST;
            _sqlObject = new SQLObject();

            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.EmailId))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_EMAILID, pEntVirtualTrainingAttendeeMaster.EmailId.ToString());
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_EMAILID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.TrainingID))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, pEntVirtualTrainingAttendeeMaster.TrainingID.ToString());
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingAttendeeMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entVirtualTrainingAttendeeMaster = FillObject(_sqlreader, false, _sqlObject);
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
            return _entVirtualTrainingAttendeeMaster;
        }
        /// <summary>
        /// VirtualTrainingAttendeeMaster Range
        /// </summary>
        /// <param name="pEntVirtualTrainingAttendeeMaster"></param>
        /// <returns></returns>
        private List<VirtualTrainingAttendeeMaster> GetAllVirtualTrainingAttendeeMaster(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingAttendeeMaster.PROC_VIRTUALTRAINING_ATTENDEE_GET_ALL;
            _entListVirtualTrainingAttendeeMaster = new List<VirtualTrainingAttendeeMaster>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingAttendeeMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                if (pEntVirtualTrainingAttendeeMaster.SystemUserGUID != null)
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING__ATTENDEE_SYSTEMUSERGUID, pEntVirtualTrainingAttendeeMaster.SystemUserGUID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.TrainingID))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, pEntVirtualTrainingAttendeeMaster.TrainingID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntVirtualTrainingAttendeeMaster.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntVirtualTrainingAttendeeMaster.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntVirtualTrainingAttendeeMaster.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntVirtualTrainingAttendeeMaster.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingAttendeeMaster = FillObject(_sqlreader, true, _sqlObject);
                    _entListVirtualTrainingAttendeeMaster.Add(_entVirtualTrainingAttendeeMaster);
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
            return _entListVirtualTrainingAttendeeMaster;
        }

        /// <summary>
        /// VirtualTrainingAttendeeMaster Range
        /// </summary>
        /// <param name="pEntVirtualTrainingAttendeeMaster"></param>
        /// <returns></returns>
        private List<VirtualTrainingAttendeeMaster> GetAllACCEPTANDREGISTERVirtualTrainingAttendeeMaster(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingAttendeeMaster.PROC_VIRTUALTRAINING_ATTENDEE_GET_ALL_ACCEPTANDREJECT;
            _entListVirtualTrainingAttendeeMaster = new List<VirtualTrainingAttendeeMaster>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingAttendeeMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                if (pEntVirtualTrainingAttendeeMaster.SystemUserGUID != null)
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING__ATTENDEE_SYSTEMUSERGUID, pEntVirtualTrainingAttendeeMaster.SystemUserGUID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.TrainingID))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, pEntVirtualTrainingAttendeeMaster.TrainingID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(Convert.ToString(pEntVirtualTrainingAttendeeMaster.CreatedById)))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntVirtualTrainingAttendeeMaster.CreatedById);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntVirtualTrainingAttendeeMaster.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntVirtualTrainingAttendeeMaster.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntVirtualTrainingAttendeeMaster.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntVirtualTrainingAttendeeMaster.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingAttendeeMaster = FillObject(_sqlreader, true, _sqlObject);
                    _entListVirtualTrainingAttendeeMaster.Add(_entVirtualTrainingAttendeeMaster);
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
            return _entListVirtualTrainingAttendeeMaster;
        }

        /// <summary>
        /// Fill Object
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>VirtualTrainingAttendeeMaster</returns>
        private VirtualTrainingAttendeeMaster FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entVirtualTrainingAttendeeMaster = new VirtualTrainingAttendeeMaster();
            int index;
            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_SYSTEMUSERGUID))
                {

                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_SYSTEMUSERGUID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.ID = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_SYSTEMUSERGUID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_SYSTEMUSERGUID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.SystemUserGUID = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_TRAININGID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_TRAININGID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.TrainingID = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_SESSION_KEY))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_SESSION_KEY);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.SessionKey = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEEID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEEID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.AttendeeID = pSqlReader.GetString(index);
                }


                //if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ISDELETED))
                //{
                //    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ISDELETED);
                //    if (!pSqlReader.IsDBNull(index))
                //        _entVirtualTrainingAttendeeMaster.IsDeleted = pSqlReader.GetBoolean(index);
                //}

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_IsDeleted))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_IsDeleted);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.IsDeleted = pSqlReader.GetBoolean(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ISADMINADDED))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ISADMINADDED);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.IsAdminAdded = pSqlReader.GetBoolean(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_REGISTERID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_REGISTERID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.RegisterID = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_FIRST_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_FIRST_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.UserFirstName = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_LAST_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_LAST_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.UserLastName = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_EMAIL_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_EMAIL_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.EmailId = pSqlReader.GetString(index);
                }



                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(index))
                        _entRange.TotalRows = pSqlReader.GetInt32(index);
                    _entVirtualTrainingAttendeeMaster.ListRange = _entRange;
                    return _entVirtualTrainingAttendeeMaster;
                }
            }
            return _entVirtualTrainingAttendeeMaster;
        }

        private VirtualTrainingAttendeeMaster UpdateVirtualTrainingAttendeeMaster(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster, VirtualTrainingAttendeeMaster.Method pMethod)
        {
            int iRowsAffected = 0;
            SqlConnection sqlConn = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingAttendeeMaster.PROC_VIRTUALTRAINING_ATTENDEE_UPDATE;
            _sqlObject = new SQLObject();


            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.TrainingID))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, pEntVirtualTrainingAttendeeMaster.TrainingID);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.SystemUserGUID))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING__ATTENDEE_SYSTEMUSERGUID, pEntVirtualTrainingAttendeeMaster.SystemUserGUID);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING__ATTENDEE_SYSTEMUSERGUID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.SessionKey))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_SESSION_KEY, pEntVirtualTrainingAttendeeMaster.SessionKey);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_SESSION_KEY, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.Status))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_STATUS, pEntVirtualTrainingAttendeeMaster.Status);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_STATUS, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.AttendeeID))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEEID, pEntVirtualTrainingAttendeeMaster.AttendeeID);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEEID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.RegisterID))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_REGISTERID, pEntVirtualTrainingAttendeeMaster.RegisterID);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_REGISTERID, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.LastModifiedById))
                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntVirtualTrainingAttendeeMaster.LastModifiedById);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, null);
            _sqlcmd.Parameters.Add(_sqlpara);



            try
            {
                if (pMethod == VirtualTrainingAttendeeMaster.Method.Update)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else if (pMethod == VirtualTrainingAttendeeMaster.Method.Add)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    ///pEntVirtualTrainingAttendeeMaster.ID = YPLMS.Services.IDGenerator.GetStringGUIDWithPrefix(Schema.VirtualTrainingAttendeeMaster.VAL_SESSION_ID_PREFIX);
                }
                //_sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_SESSIONID, pEntVirtualTrainingAttendeeMaster.ID);
                //_sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingAttendeeMaster.ClientId);
                sqlConn = new SqlConnection(_strConnString);
                sqlConn.Open();
                _sqlcmd.Connection = sqlConn;
                iRowsAffected = _sqlObject.ExecuteNonQueryCount(_sqlcmd);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlConn.State != ConnectionState.Closed)
                    sqlConn.Close();
            }
            return pEntVirtualTrainingAttendeeMaster;
        }

        //
        private VirtualTrainingAttendeeMaster UpdateVirtualTrainingAttendeeList(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster, VirtualTrainingAttendeeMaster.Method pMethod)
        {
            int iRowsAffected = 0;
            SqlConnection sqlConn = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingAttendeeMaster.PROC_VIRTUALTRAINING_SESSION_ATTENDEE_LIST_UPDATE;
            _sqlObject = new SQLObject();

            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.SystemUserGUID))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING__ATTENDEE_SYSTEMUSERGUID, pEntVirtualTrainingAttendeeMaster.SystemUserGUID);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING__ATTENDEE_SYSTEMUSERGUID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.SessionKey))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_SESSION_KEY, pEntVirtualTrainingAttendeeMaster.SessionKey);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_SESSION_KEY, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingAttendeeMaster.StartTime != DateTime.MinValue)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_STARTTIME, pEntVirtualTrainingAttendeeMaster.StartTime);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_STARTTIME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingAttendeeMaster.EndTime != DateTime.MinValue)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ENDTIME, pEntVirtualTrainingAttendeeMaster.EndTime);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ENDTIME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.Duration))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_DURATION, pEntVirtualTrainingAttendeeMaster.Duration);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_DURATION, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.IPAddress))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_IPADDRESS, pEntVirtualTrainingAttendeeMaster.IPAddress);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_IPADDRESS, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.ClientAgent))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_REPORT_TRAINING_CLIENTAGENT, pEntVirtualTrainingAttendeeMaster.ClientAgent);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_REPORT_TRAINING_CLIENTAGENT, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingAttendeeMaster.AttendedStatus)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_REPORT_TRAINING_ATTENEDSTATUS, pEntVirtualTrainingAttendeeMaster.AttendedStatus);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_REPORT_TRAINING_ATTENEDSTATUS, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingAttendeeMaster.ClientId);
                sqlConn = new SqlConnection(_strConnString);
                sqlConn.Open();
                _sqlcmd.Connection = sqlConn;
                iRowsAffected = _sqlObject.ExecuteNonQueryCount(_sqlcmd);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlConn.State != ConnectionState.Closed)
                    sqlConn.Close();
            }
            return pEntVirtualTrainingAttendeeMaster;
        }

        /// <summary>
        /// Delete VirtualTrainingAttendeeMaster by ID
        /// </summary>
        /// <param name="pEntVirtualTrainingAttendeeMaster">VirtualTrainingAttendeeMaster with ID</param>
        /// <returns>Deleted VirtualTrainingAttendeeMaster with only ID</returns>
        private VirtualTrainingAttendeeMaster DeleteVirtualTrainingAttendeeMaster(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster)
        {
            VirtualTrainingAttendeeMaster entVirtualTrainingAttendeeMaster = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingAttendeeMaster.PROC_VIRTUALTRAINING_ATTENDEE_DELETE;
            _sqlObject = new SQLObject();
            int i = 0;
            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.SystemUserGUID))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING__ATTENDEE_SYSTEMUSERGUID, pEntVirtualTrainingAttendeeMaster.SystemUserGUID);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING__ATTENDEE_SYSTEMUSERGUID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.TrainingID))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, pEntVirtualTrainingAttendeeMaster.TrainingID);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingAttendeeMaster.ClientId);
                i = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                if (i < 0)
                {
                    entVirtualTrainingAttendeeMaster = new VirtualTrainingAttendeeMaster();
                    entVirtualTrainingAttendeeMaster.SystemUserGUID = pEntVirtualTrainingAttendeeMaster.SystemUserGUID;
                    entVirtualTrainingAttendeeMaster.TrainingID = pEntVirtualTrainingAttendeeMaster.TrainingID;

                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entVirtualTrainingAttendeeMaster;
        }

        public List<VirtualTrainingAttendeeMaster> GetUserByName(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster)
        {
            SqlConnection sqlConnection = null;
            _entVirtualTrainingAttendeeMaster = new VirtualTrainingAttendeeMaster();
            _sqlObject = new SQLObject();
            List<VirtualTrainingAttendeeMaster> entListVirtualTrainingAttendeeMaster = new List<VirtualTrainingAttendeeMaster>();
            _sqlreader = null;

            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _sqlcmd.CommandText = Schema.VirtualTrainingAttendeeMaster.PROC_GET_USER_TRAINING_LIST;

            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.FirstName))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_FIRST_NAME, pEntVirtualTrainingAttendeeMaster.FirstName);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_FIRST_NAME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.LastName))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_LAST_NAME, pEntVirtualTrainingAttendeeMaster.LastName);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_LAST_NAME, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.TrainingID))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, pEntVirtualTrainingAttendeeMaster.TrainingID);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingAttendeeMaster.IsActive)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_IS_ACTIVE, pEntVirtualTrainingAttendeeMaster.IsActive);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_IS_ACTIVE, false);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingAttendeeMaster.ListRange != null)
            {
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntVirtualTrainingAttendeeMaster.ListRange.PageIndex);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntVirtualTrainingAttendeeMaster.ListRange.PageSize);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntVirtualTrainingAttendeeMaster.ListRange.SortExpression);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntVirtualTrainingAttendeeMaster.ListRange.RequestedById);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingAttendeeMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingAttendeeMaster = FillUserByName(_sqlreader, true, _sqlObject);
                    entListVirtualTrainingAttendeeMaster.Add(_entVirtualTrainingAttendeeMaster);
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
            return entListVirtualTrainingAttendeeMaster;
        }


        public List<VirtualTrainingAttendeeMaster> GetAssignedAttendeeByTrainingID(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster)
        {
            SqlConnection sqlConnection = null;
            _entVirtualTrainingAttendeeMaster = new VirtualTrainingAttendeeMaster();
            _sqlObject = new SQLObject();
            List<VirtualTrainingAttendeeMaster> entListLearner = new List<VirtualTrainingAttendeeMaster>();
            _sqlreader = null;

            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _sqlcmd.CommandText = Schema.VirtualTrainingAttendeeMaster.PROC_GET_USER_ASSIGNEDTRAINING_LIST;

            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.TrainingID))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, pEntVirtualTrainingAttendeeMaster.TrainingID);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.VTApprovePage))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_APPROVEPAGE, pEntVirtualTrainingAttendeeMaster.VTApprovePage);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_APPROVEPAGE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingAttendeeMaster.Status))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_STATUS, pEntVirtualTrainingAttendeeMaster.Status);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_STATUS, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingAttendeeMaster.ListRange != null)
            {
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntVirtualTrainingAttendeeMaster.ListRange.PageIndex);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntVirtualTrainingAttendeeMaster.ListRange.PageSize);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntVirtualTrainingAttendeeMaster.ListRange.SortExpression);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntVirtualTrainingAttendeeMaster.ListRange.RequestedById);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingAttendeeMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingAttendeeMaster = FillUserByName(_sqlreader, true, _sqlObject);
                    entListLearner.Add(_entVirtualTrainingAttendeeMaster);
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
            return entListLearner;
        }

        public VirtualTrainingAttendeeMaster FillUserByName(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entVirtualTrainingAttendeeMaster = new VirtualTrainingAttendeeMaster();
            UserCustomFieldValue entUserCustomFieldValue;
            int index;
            if (pSqlReader.HasRows)
            {
                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entVirtualTrainingAttendeeMaster.ID = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_FIRST_NAME);
                if (!pSqlReader.IsDBNull(index))
                    _entVirtualTrainingAttendeeMaster.FirstName = pSqlReader.GetString(index);
                else
                    _entVirtualTrainingAttendeeMaster.FirstName = "";

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_MIDDLE_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_MIDDLE_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.MiddleName = pSqlReader.GetString(index);
                }
                if (string.IsNullOrEmpty(_entVirtualTrainingAttendeeMaster.MiddleName))
                    _entVirtualTrainingAttendeeMaster.MiddleName = "";

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_LAST_NAME);
                if (!pSqlReader.IsDBNull(index))
                    _entVirtualTrainingAttendeeMaster.LastName = pSqlReader.GetString(index);
                else
                    _entVirtualTrainingAttendeeMaster.LastName = "";

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_EMAIL_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_EMAIL_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.EmailID = pSqlReader.GetString(index);
                }

                if (string.IsNullOrEmpty(_entVirtualTrainingAttendeeMaster.EmailID))
                    _entVirtualTrainingAttendeeMaster.EmailID = "";



                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_PREFERRED_DATE_FORMAT))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_PREFERRED_DATE_FORMAT);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.PreferredDateFormat = pSqlReader.GetString(index);

                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_PREFERREDTIMEZONE))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_PREFERREDTIMEZONE);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.PreferredTimeZone = pSqlReader.GetString(index);

                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_PHONE_NO))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_PHONE_NO);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.PhoneNo = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_NAME_ALIAS))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_NAME_ALIAS);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.UserNameAlias = pSqlReader.GetString(index);
                    else
                        _entVirtualTrainingAttendeeMaster.UserNameAlias = "";
                }



                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_PASSWORD))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_PASSWORD);
                    if (!pSqlReader.IsDBNull(index))
                    {
                        _entVirtualTrainingAttendeeMaster.UserPassword = pSqlReader.GetString(index);
                        _entVirtualTrainingAttendeeMaster.UserPassword = EncryptionManager.Decrypt(_entVirtualTrainingAttendeeMaster.UserPassword);
                    }
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_ADDRESS))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_ADDRESS);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.Address = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DATE_OF_BIRTH))
                {

                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_DATE_OF_BIRTH);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.DateOfBirth = pSqlReader.GetDateTime(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DATE_OF_REGISTRATION))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_DATE_OF_REGISTRATION);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.DateOfRegistration = pSqlReader.GetDateTime(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DATE_OF_TERMINATION))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_DATE_OF_TERMINATION);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.DateOfTermination = pSqlReader.GetDateTime(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_TYPE_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_TYPE_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.UserTypeId = pSqlReader.GetString(index);
                    else
                        _entVirtualTrainingAttendeeMaster.UserTypeId = "";
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DEFAULT_LANGUAGE_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_DEFAULT_LANGUAGE_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.DefaultLanguageId = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DEFAULT_THEME_ID))
                {

                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_DEFAULT_THEME_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.DefaultThemeID = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_GENDER))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_GENDER);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.Gender = pSqlReader.GetBoolean(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_MANAGER_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_MANAGER_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.ManagerId = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_MANAGER_EMAIL))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_MANAGER_EMAIL);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.ManagerEmailId = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_MANAGER_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_MANAGER_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.ManagerName = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DATE_LAST_LOGIN))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_DATE_LAST_LOGIN);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.DateLastLogin = pSqlReader.GetDateTime(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_IS_ACTIVE))
                {

                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_IS_ACTIVE);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.IsActive = pSqlReader.GetBoolean(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_CLIENT_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.ClientId = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.CreatedById = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_ON))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.DateCreated = pSqlReader.GetDateTime(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.LastModifiedById = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.LastModifiedDate = pSqlReader.GetDateTime(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_UNIT_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_UNIT_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.UnitId = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_LEVEL_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_LEVEL_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.LevelId = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DEFAULT_RV))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_DEFAULT_RV);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.DefaultRegionView = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_CURRENT_RV))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_CURRENT_RV);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.CurrentRegionView = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_IS_FIRST_LOGIN))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_IS_FIRST_LOGIN);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.IsFirstLogin = pSqlReader.GetBoolean(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_IS_PASSWORD_EXPIRED))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_IS_PASSWORD_EXPIRED);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.IsPasswordExpired = pSqlReader.GetBoolean(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_SCOPE))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_SCOPE);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.UserScope = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_DEFAULT_ORG))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_DEFAULT_ORG);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.UserDefaultOrg = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_UserExpiryDate))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_UserExpiryDate);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.userExpiryDate = pSqlReader.GetDateTime(index);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_STATUS))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_STATUS);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingAttendeeMaster.VTStatus = pSqlReader.GetString(index);
                }


                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(index))
                        _entRange.TotalRows = pSqlReader.GetInt32(index);
                    _entVirtualTrainingAttendeeMaster.ListRange = _entRange;
                    return _entVirtualTrainingAttendeeMaster;

                }
            }
            return _entVirtualTrainingAttendeeMaster;
        }



        #region Bulk Data Inserted
        public List<VirtualTrainingAttendeeMaster> BulkUpdateVirtualTrainingAttendeeMaster(List<VirtualTrainingAttendeeMaster> pEntListVirtualTrainingAttendeeMaster)
        {
            SqlConnection sqlConn = new SqlConnection();
            List<VirtualTrainingAttendeeMaster> entListVirtualTrainingAttendeeMaster = new List<VirtualTrainingAttendeeMaster>();
            List<VirtualTrainingAttendeeMaster> pentListVirtualTrainingAttendeeMaster = new List<VirtualTrainingAttendeeMaster>();
            VirtualTrainingAttendeeMaster entVirtualTrainingAttendeeMaster;
            try
            {
                _sqlObject = new SQLObject();
                _sqladapter = new SqlDataAdapter();
                _dtable = new DataTable();
                DataTable dQuestionTable = new DataTable();
                int iBatchSize = 0;
                if (pEntListVirtualTrainingAttendeeMaster.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_SYSTEMUSERGUID);
                    _dtable.Columns.Add(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_TRAININGID);
                    _dtable.Columns.Add(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_SESSION_KEY);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);

                    _dtable.Columns.Add(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_STATUS);

                    _dtable.Columns.Add(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEEID);
                    _dtable.Columns.Add(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ISADMINADDED);
                    _dtable.Columns.Add(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_REGISTERID);


                    foreach (VirtualTrainingAttendeeMaster objBase in pEntListVirtualTrainingAttendeeMaster)
                    {
                        entVirtualTrainingAttendeeMaster = new VirtualTrainingAttendeeMaster();
                        entVirtualTrainingAttendeeMaster = objBase;

                        DataRow drow = _dtable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entVirtualTrainingAttendeeMaster.ClientId);

                        if (!String.IsNullOrEmpty(entVirtualTrainingAttendeeMaster.SystemUserGUID))
                        {
                            drow[Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_SYSTEMUSERGUID] = entVirtualTrainingAttendeeMaster.SystemUserGUID;
                            drow[Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_TRAININGID] = entVirtualTrainingAttendeeMaster.TrainingID;
                            drow[Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_SESSION_KEY] = entVirtualTrainingAttendeeMaster.SessionKey;

                            drow[Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_STATUS] = entVirtualTrainingAttendeeMaster.Status;

                            drow[Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEEID] = entVirtualTrainingAttendeeMaster.AttendeeID;
                            drow[Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ISADMINADDED] = entVirtualTrainingAttendeeMaster.IsAdminAdded;
                            drow[Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_REGISTERID] = entVirtualTrainingAttendeeMaster.RegisterID;
                            if (!String.IsNullOrEmpty(entVirtualTrainingAttendeeMaster.LastModifiedById))
                            {
                                drow[Schema.Common.COL_MODIFIED_BY] = entVirtualTrainingAttendeeMaster.LastModifiedById;
                            }


                            iBatchSize = iBatchSize + 1;
                            _dtable.Rows.Add(drow);
                            entListVirtualTrainingAttendeeMaster.Add(entVirtualTrainingAttendeeMaster);
                        }
                    }
                    if (_dtable.Rows.Count > 0)
                    {
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.VirtualTrainingAttendeeMaster.PROC_VIRTUALTRAINING_ATTENDEE_UPDATE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlConn = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = sqlConn;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING__ATTENDEE_SYSTEMUSERGUID, SqlDbType.VarChar, 100, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_SYSTEMUSERGUID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, SqlDbType.VarChar, 100, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_TRAININGID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_SESSION_KEY, SqlDbType.VarChar, 100, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_SESSION_KEY);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);

                        _sqladapter.InsertCommand.Parameters.Add(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_STATUS, SqlDbType.VarChar, 100, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_STATUS);

                        _sqladapter.InsertCommand.Parameters.Add(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEEID, SqlDbType.VarChar, 100, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEEID);

                        _sqladapter.InsertCommand.Parameters.Add(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ISADMINADDED, SqlDbType.VarChar, 100, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ISADMINADDED);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_REGISTERID, SqlDbType.VarChar, 100, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_REGISTERID);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        int _iDeleteRows = _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();

                        pentListVirtualTrainingAttendeeMaster = new List<VirtualTrainingAttendeeMaster>();
                        EntityRange _entRange = new EntityRange();
                        _entRange.TotalRows = _iDeleteRows;
                        entVirtualTrainingAttendeeMaster = new VirtualTrainingAttendeeMaster();
                        entVirtualTrainingAttendeeMaster.ListRange = _entRange;
                        pentListVirtualTrainingAttendeeMaster.Add(entVirtualTrainingAttendeeMaster);
                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pentListVirtualTrainingAttendeeMaster;
        }
        #endregion

        #region Bulk Data Inserted
        public List<VirtualTrainingAttendeeMaster> BulkUpdateVirtualTrainingFailureList(List<VirtualTrainingAttendeeMaster> pEntListVirtualTrainingFailureList)
        {
            SqlConnection sqlConn = new SqlConnection();
            List<VirtualTrainingAttendeeMaster> entListVirtualTrainingAttendeeMaster = new List<VirtualTrainingAttendeeMaster>();
            List<VirtualTrainingAttendeeMaster> pentListVirtualTrainingAttendeeMaster = new List<VirtualTrainingAttendeeMaster>();
            VirtualTrainingAttendeeMaster entVirtualTrainingAttendeeMaster;
            try
            {
                _sqlObject = new SQLObject();
                _sqladapter = new SqlDataAdapter();
                _dtable = new DataTable();
                DataTable dQuestionTable = new DataTable();
                int iBatchSize = 0;
                if (pEntListVirtualTrainingFailureList.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_SYSTEMUSERGUID);
                    _dtable.Columns.Add(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_TRAININGID);
                    _dtable.Columns.Add(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_SESSION_KEY);


                    _dtable.Columns.Add(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_STATUS);




                    foreach (VirtualTrainingAttendeeMaster objBase in pEntListVirtualTrainingFailureList)
                    {
                        entVirtualTrainingAttendeeMaster = new VirtualTrainingAttendeeMaster();
                        entVirtualTrainingAttendeeMaster = objBase;

                        DataRow drow = _dtable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entVirtualTrainingAttendeeMaster.ClientId);

                        if (!String.IsNullOrEmpty(entVirtualTrainingAttendeeMaster.SystemUserGUID))
                        {
                            drow[Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_SYSTEMUSERGUID] = entVirtualTrainingAttendeeMaster.SystemUserGUID;
                            drow[Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_TRAININGID] = entVirtualTrainingAttendeeMaster.TrainingID;
                            drow[Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_SESSION_KEY] = entVirtualTrainingAttendeeMaster.SessionKey;

                            drow[Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_STATUS] = entVirtualTrainingAttendeeMaster.Status;

                            iBatchSize = iBatchSize + 1;
                            _dtable.Rows.Add(drow);
                            entListVirtualTrainingAttendeeMaster.Add(entVirtualTrainingAttendeeMaster);
                        }
                    }
                    if (_dtable.Rows.Count > 0)
                    {
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.VirtualTrainingAttendeeMaster.PROC_VIRTUALTRAINING_AddFailureAttendeeList;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlConn = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = sqlConn;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING__ATTENDEE_SYSTEMUSERGUID, SqlDbType.VarChar, 100, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_SYSTEMUSERGUID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, SqlDbType.VarChar, 100, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_TRAININGID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_SESSION_KEY, SqlDbType.VarChar, 100, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_SESSION_KEY);

                        _sqladapter.InsertCommand.Parameters.Add(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_STATUS, SqlDbType.VarChar, 100, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_STATUS);


                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        int _iDeleteRows = _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();

                        pentListVirtualTrainingAttendeeMaster = new List<VirtualTrainingAttendeeMaster>();
                        EntityRange _entRange = new EntityRange();
                        _entRange.TotalRows = _iDeleteRows;
                        entVirtualTrainingAttendeeMaster = new VirtualTrainingAttendeeMaster();
                        entVirtualTrainingAttendeeMaster.ListRange = _entRange;
                        pentListVirtualTrainingAttendeeMaster.Add(entVirtualTrainingAttendeeMaster);
                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pentListVirtualTrainingAttendeeMaster;
        }
        #endregion

        #region Bulk Data Inserted
        public List<VirtualTrainingAttendeeMaster> AssignedAttendeeDelete(List<VirtualTrainingAttendeeMaster> pEntListVirtualTrainingAttendeeMaster)
        {
            SqlConnection sqlConn = new SqlConnection();
            List<VirtualTrainingAttendeeMaster> entListVirtualTrainingAttendeeMaster = new List<VirtualTrainingAttendeeMaster>();
            List<VirtualTrainingAttendeeMaster> pentListVirtualTrainingAttendeeMaster = new List<VirtualTrainingAttendeeMaster>();
            VirtualTrainingAttendeeMaster entVirtualTrainingAttendeeMaster;
            try
            {
                _sqlObject = new SQLObject();
                _sqladapter = new SqlDataAdapter();
                _dtable = new DataTable();
                DataTable dQuestionTable = new DataTable();
                int iBatchSize = 0;
                if (pEntListVirtualTrainingAttendeeMaster.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_SYSTEMUSERGUID);
                    _dtable.Columns.Add(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_TRAININGID);
                    _dtable.Columns.Add(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_SESSION_KEY);
                    foreach (VirtualTrainingAttendeeMaster objBase in pEntListVirtualTrainingAttendeeMaster)
                    {
                        entVirtualTrainingAttendeeMaster = new VirtualTrainingAttendeeMaster();
                        entVirtualTrainingAttendeeMaster = objBase;

                        DataRow drow = _dtable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entVirtualTrainingAttendeeMaster.ClientId);

                        if (!String.IsNullOrEmpty(entVirtualTrainingAttendeeMaster.SystemUserGUID))
                        {
                            drow[Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_SYSTEMUSERGUID] = entVirtualTrainingAttendeeMaster.SystemUserGUID;
                            drow[Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_TRAININGID] = entVirtualTrainingAttendeeMaster.TrainingID;
                            //drow[Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_SESSION_KEY] = entVirtualTrainingAttendeeMaster.SessionKey;

                            iBatchSize = iBatchSize + 1;
                            _dtable.Rows.Add(drow);
                            entListVirtualTrainingAttendeeMaster.Add(entVirtualTrainingAttendeeMaster);
                        }
                    }
                    if (_dtable.Rows.Count > 0)
                    {
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.VirtualTrainingAttendeeMaster.PROC_VIRTUALTRAINING_ATTENDEE_DELETE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlConn = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = sqlConn;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING__ATTENDEE_SYSTEMUSERGUID, SqlDbType.VarChar, 100, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_SYSTEMUSERGUID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, SqlDbType.VarChar, 100, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_TRAININGID);
                        //_sqladapter.InsertCommand.Parameters.Add(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_SESSION_KEY, SqlDbType.VarChar, 100, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_SESSION_KEY);

                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        int _iDeleteRows = _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();

                        pentListVirtualTrainingAttendeeMaster = new List<VirtualTrainingAttendeeMaster>();
                        EntityRange _entRange = new EntityRange();
                        _entRange.TotalRows = _iDeleteRows;
                        entVirtualTrainingAttendeeMaster = new VirtualTrainingAttendeeMaster();
                        entVirtualTrainingAttendeeMaster.ListRange = _entRange;
                        pentListVirtualTrainingAttendeeMaster.Add(entVirtualTrainingAttendeeMaster);
                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pentListVirtualTrainingAttendeeMaster;
        }

        /// <summary>
        /// Find Learners
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <param name="pIsIn"></param>
        /// <returns></returns>
        //private List<VirtualTrainingAttendeeMaster> FindVirtualTrainingAllUsers(Search pEntSearch, bool pIsIn)
        //{
        //    VirtualTrainingAttendeeMaster pEntLearner = null;
        //    _sqlcmd = new SqlCommand();
        //    _sqlObject = new SQLObject();
        //    _entListLearner = new List<VirtualTrainingAttendeeMaster>();
        //    SqlConnection sqlConnection = null;
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(pEntSearch.ClientId))
        //        {

        //            _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
        //            sqlConnection = new SqlConnection(_strConnString);
        //            _sqlcmd.CommandText = Schema.VirtualTrainingAttendeeMaster.PROC_GET_USER_TRAINING_LIST;
        //            _sqlcmd.CommandType = CommandType.StoredProcedure;

        //            if (pEntSearch.ListRange != null)
        //            {
        //                if (pEntSearch.ListRange.PageIndex > 0)
        //                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntSearch.ListRange.PageIndex);
        //                else
        //                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
        //                _sqlcmd.Parameters.Add(_sqlpara);

        //                if (pEntSearch.ListRange.PageSize > 0)
        //                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntSearch.ListRange.PageSize);
        //                else
        //                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
        //                _sqlcmd.Parameters.Add(_sqlpara);

        //                if (!string.IsNullOrEmpty(pEntSearch.ListRange.SortExpression))
        //                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
        //                else
        //                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
        //                _sqlcmd.Parameters.Add(_sqlpara);

        //                if (!string.IsNullOrEmpty(pEntSearch.ListRange.RequestedById))
        //                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
        //                else
        //                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, null);
        //                _sqlcmd.Parameters.Add(_sqlpara);
        //            }

        //            if (pEntSearch.SearchObject.Count > 0)
        //            {
        //                entLearner = (VirtualTrainingAttendeeMaster)pEntSearch.SearchObject[0];
        //                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_IS_ACTIVE, entLearner.IsActive);
        //                _sqlcmd.Parameters.Add(_sqlpara);
        //            }

        //            if (!String.IsNullOrEmpty(pEntSearch.TrainingId))
        //                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, pEntSearch.TrainingId);
        //            else
        //                _sqlpara = new SqlParameter(Schema.VirtualTrainingAttendeeMaster.PARA_VIRTUALTRAINING_ATTENDEE_TRAININGID, null);

        //            _sqlcmd.Parameters.Add(_sqlpara);

        //            _sqlcmd.Connection = sqlConnection;
        //            _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

        //            while (_sqlreader.Read())
        //            {
        //                _entVirtualTrainingAttendeeMaster = FillUserByName(_sqlreader, true, _sqlObject);
        //                _entListLearner.Add(_entVirtualTrainingAttendeeMaster);
        //            }
        //        }
        //    }
        //    catch (Exception expCommon)
        //    {
        //        _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
        //        throw _expCustom;
        //    }
        //    finally
        //    {
        //        if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
        //        if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
        //            sqlConnection.Close();
        //    }
        //    return _entListLearner;
        //}
        #endregion


        #region Interface Methods
        /// <summary>
        /// Get VirtualTrainingAttendeeMaster By ID
        /// </summary>
        /// <param name="pEntVirtualTrainingAttendeeMaster"></param>
        /// <returns></returns>
        public VirtualTrainingAttendeeMaster Get(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster)
        {
            return GetVirtualTrainingAttendeeMasterByID(pEntVirtualTrainingAttendeeMaster);
        }

        /// <summary>
        /// Get VirtualTrainingAttendeeMaster By ID
        /// </summary>
        /// <param name="pEntVirtualTrainingAttendeeMaster"></param>
        /// <returns></returns>
        public VirtualTrainingAttendeeMaster CheckEmailExist(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster)
        {
            return GetVirtualTrainingAttendeeMasterByEmailID(pEntVirtualTrainingAttendeeMaster);
        }

        /// <summary>
        /// List of All VirtualTrainingAttendeeMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingAttendeeMaster"></param>
        /// <returns></returns>
        public List<VirtualTrainingAttendeeMaster> GetAll(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster)
        {
            return GetAllVirtualTrainingAttendeeMaster(pEntVirtualTrainingAttendeeMaster);
        }
        /// <summary>
        /// List of All VirtualTrainingAttendeeMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingAttendeeMaster"></param>
        /// <returns></returns>
        public List<VirtualTrainingAttendeeMaster> GetAllACCEPTANDREGISTER(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster)
        {
            return GetAllACCEPTANDREGISTERVirtualTrainingAttendeeMaster(pEntVirtualTrainingAttendeeMaster);
        }


        /// <summary>
        /// Add VirtualTrainingAttendeeMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingAttendeeMaster"></param>
        /// <returns></returns>
        public VirtualTrainingAttendeeMaster Add(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster)
        {
            return UpdateVirtualTrainingAttendeeMaster(pEntVirtualTrainingAttendeeMaster, VirtualTrainingAttendeeMaster.Method.Add);
        }
        /// <summary>
        /// Update VirtualTrainingAttendeeMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingAttendeeMaster"></param>
        /// <returns></returns>
        public VirtualTrainingAttendeeMaster Update(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster)
        {
            return UpdateVirtualTrainingAttendeeMaster(pEntVirtualTrainingAttendeeMaster, VirtualTrainingAttendeeMaster.Method.Update);
        }
        //
        public VirtualTrainingAttendeeMaster UpdateSessionAttendeeList(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster)
        {
            return UpdateVirtualTrainingAttendeeList(pEntVirtualTrainingAttendeeMaster, VirtualTrainingAttendeeMaster.Method.Update);
        }
        /// <summary>
        /// Delete VirtualTrainingAttendeeMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingAttendeeMaster"></param>
        /// <returns>VirtualTrainingAttendeeMaster</returns>
        public VirtualTrainingAttendeeMaster Delete(VirtualTrainingAttendeeMaster pEntVirtualTrainingAttendeeMaster)
        {
            return DeleteVirtualTrainingAttendeeMaster(pEntVirtualTrainingAttendeeMaster);
        }

        /// <summary>
        /// Find Learners
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        //public List<VirtualTrainingAttendeeMaster> FindVirtualTrainingAllUsers(Search pEntSearch)
        //{
        //    return FindVirtualTrainingAllUsers(pEntSearch, true);
        //}
        #endregion
    }
}
