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
    public class VirtualTrainingSessionAdapter : IDataManager<VirtualTrainingSessionMaster>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        VirtualTrainingSessionMaster _entVirtualTrainingSessionMaster = null;
        List<VirtualTrainingSessionMaster> _entListVirtualTrainingSessionMaster = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        SqlDataAdapter _sqladapter = null;
        DataTable _dtable = null;
        string _strMessageId = YPLMS.Services.Messages.VirtualTrainingSessionMaster.VIRTUALTRAININGSESSIONMASTER_DAM_ERROR;
        #endregion
        /// <summary>
        /// Default constructor
        /// </summary>
        /// 
        public VirtualTrainingSessionAdapter()
        { }

        /// <summary>
        /// To get VirtualTrainingSessionMaster details by ID.
        /// </summary>
        /// <param name="pEntVirtualTrainingSessionMaster"></param>
        /// <returns>VirtualTrainingSessionMaster</returns>
        private VirtualTrainingSessionMaster GetVirtualTrainingSessionMasterByID(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingSessionMaster.PROC_VIRTUALTRAINING_GET;
            _sqlObject = new SQLObject();

            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.ID))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TRAININGID, pEntVirtualTrainingSessionMaster.ID.ToString());
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TRAININGID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingSessionMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entVirtualTrainingSessionMaster = FillObject(_sqlreader, false, _sqlObject);

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
            return _entVirtualTrainingSessionMaster;
        }


        /// <summary>
        /// To get VirtualTrainingSessionMaster details by ID.
        /// </summary>
        /// <param name="pEntVirtualTrainingSessionMaster"></param>
        /// <returns>VirtualTrainingSessionMaster</returns>
        private VirtualTrainingSessionMaster GetVirtualTrainingSessionMasterByAdminAccountUserID(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingSessionMaster.PROC_VIRTUALTRAINING_GET_ADMIN_ACCOUNT_USERID;
            _sqlObject = new SQLObject();

            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.VirtualWebexSystemUserid))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_WEBEXUSERID, pEntVirtualTrainingSessionMaster.VirtualWebexSystemUserid.ToString());
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_WEBEXUSERID, null);
            _sqlcmd.Parameters.Add(_sqlpara);



            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingSessionMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entVirtualTrainingSessionMaster = FillObject(_sqlreader, false, _sqlObject);

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
            return _entVirtualTrainingSessionMaster;
        }

        /// <summary>
        /// VirtualTrainingSessionMaster Range
        /// </summary>
        /// <param name="pEntVirtualTrainingSessionMaster"></param>
        /// <returns></returns>
        private List<VirtualTrainingSessionMaster> GetAllVirtualTrainingSessionMaster(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingSessionMaster.PROC_VIRTUALTRAINING_GETALL;
            _entListVirtualTrainingSessionMaster = new List<VirtualTrainingSessionMaster>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingSessionMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.Title))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TITLE, pEntVirtualTrainingSessionMaster.Title);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntVirtualTrainingSessionMaster.StartDate != DateTime.MinValue)
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_STARTDATE, pEntVirtualTrainingSessionMaster.StartDate);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntVirtualTrainingSessionMaster.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntVirtualTrainingSessionMaster.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntVirtualTrainingSessionMaster.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntVirtualTrainingSessionMaster.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntVirtualTrainingSessionMaster.ListRange.RequestedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingSessionMaster = FillObject(_sqlreader, true, _sqlObject);
                    _entListVirtualTrainingSessionMaster.Add(_entVirtualTrainingSessionMaster);
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
            return _entListVirtualTrainingSessionMaster;
        }
        public List<VirtualTrainingSessionMaster> GetAllVirtualTrainingSessionMaster_ForCurriculum(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingSessionMaster.PROC_VIRTUALTRAINING_GETALL_ForCurriculum;
            _entListVirtualTrainingSessionMaster = new List<VirtualTrainingSessionMaster>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingSessionMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.Title))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TITLE, pEntVirtualTrainingSessionMaster.Title);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntVirtualTrainingSessionMaster.StartDate != DateTime.MinValue)
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_STARTDATE, pEntVirtualTrainingSessionMaster.StartDate);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntVirtualTrainingSessionMaster.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntVirtualTrainingSessionMaster.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntVirtualTrainingSessionMaster.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntVirtualTrainingSessionMaster.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntVirtualTrainingSessionMaster.ListRange.RequestedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingSessionMaster = FillObject(_sqlreader, true, _sqlObject);
                    _entListVirtualTrainingSessionMaster.Add(_entVirtualTrainingSessionMaster);
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
            return _entListVirtualTrainingSessionMaster;
        }

        private List<VirtualTrainingSessionMaster> GetAllVirtualSessionKey(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingSessionMaster.PROC_VIRTUALTRAINING_GET_SESSION_KEY;
            _entListVirtualTrainingSessionMaster = new List<VirtualTrainingSessionMaster>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingSessionMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingSessionMaster = FillObject(_sqlreader, false, _sqlObject);
                    _entListVirtualTrainingSessionMaster.Add(_entVirtualTrainingSessionMaster);
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
            return _entListVirtualTrainingSessionMaster;
        }
        //
        private List<VirtualTrainingSessionMaster> GetAllVIRTUALTRAININGClientID(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingSessionMaster.PROC_VIRTUALTRAINING_CLIENT_LIST;
            _entListVirtualTrainingSessionMaster = new List<VirtualTrainingSessionMaster>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingSessionMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingSessionMaster = FillAllClientObject(_sqlreader, _sqlObject);
                    _entListVirtualTrainingSessionMaster.Add(_entVirtualTrainingSessionMaster);
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
            return _entListVirtualTrainingSessionMaster;
        }

        //
        private List<VirtualTrainingSessionMaster> Get_VIRTUALTRAINING_Names(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingSessionMaster.PROC_FIND_VIRTUALTRAINING_NAMES;
            _entListVirtualTrainingSessionMaster = new List<VirtualTrainingSessionMaster>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {

                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingSessionMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.Title))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TITLE, pEntVirtualTrainingSessionMaster.Title);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TITLE, null);
                }

                if (pEntVirtualTrainingSessionMaster.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntVirtualTrainingSessionMaster.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntVirtualTrainingSessionMaster.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntVirtualTrainingSessionMaster.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntVirtualTrainingSessionMaster.ListRange.RequestedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingSessionMaster = FillAllVirtualTrainingNames(_sqlreader, true, _sqlObject);
                    _entListVirtualTrainingSessionMaster.Add(_entVirtualTrainingSessionMaster);
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
            return _entListVirtualTrainingSessionMaster;
        }
        /// <summary>
        /// VirtualTrainingSessionMaster Range
        /// </summary>
        /// <param name="pEntVirtualTrainingSessionMaster"></param>
        /// <returns></returns>
        private List<VirtualTrainingSessionMaster> GetAllSessionVirtualTrainingSessionUserMaster(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingSessionMaster.PROC_VIRTUALTRAINING_GETALLSESSIONSUSERTATUS;
            _entListVirtualTrainingSessionMaster = new List<VirtualTrainingSessionMaster>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingSessionMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.SystemUserGUID))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_SYSTEMUSERGUID, pEntVirtualTrainingSessionMaster.SystemUserGUID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.SessionKey))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_SESSION_KEY, pEntVirtualTrainingSessionMaster.SessionKey);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.Title))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TITLE, pEntVirtualTrainingSessionMaster.Title);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.EmailID))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_EMAILID, pEntVirtualTrainingSessionMaster.EmailID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!String.IsNullOrEmpty(Convert.ToString(pEntVirtualTrainingSessionMaster.CreatedById)))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntVirtualTrainingSessionMaster.CreatedById);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntVirtualTrainingSessionMaster.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntVirtualTrainingSessionMaster.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntVirtualTrainingSessionMaster.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntVirtualTrainingSessionMaster.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingSessionMaster = FillObject(_sqlreader, true, _sqlObject);
                    _entListVirtualTrainingSessionMaster.Add(_entVirtualTrainingSessionMaster);
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
            return _entListVirtualTrainingSessionMaster;
        }
        private List<VirtualTrainingSessionMaster> GetVirtualSystemUserGUID(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingSessionMaster.PROC_VIRTUALTRAINING_GET_SYSTEMUSERGUID;
            _entListVirtualTrainingSessionMaster = new List<VirtualTrainingSessionMaster>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingSessionMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.SessionKey))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_SESSION_KEY, pEntVirtualTrainingSessionMaster.SessionKey);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.EmailID))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_EMAILID, pEntVirtualTrainingSessionMaster.EmailID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }


                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingSessionMaster = FillObjectSystemGUID(_sqlreader, false, _sqlObject);
                    _entListVirtualTrainingSessionMaster.Add(_entVirtualTrainingSessionMaster);
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
            return _entListVirtualTrainingSessionMaster;
        }

        /// <summary>
        /// Fill Object
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>VirtualTrainingSessionMaster</returns>
        private VirtualTrainingSessionMaster FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entVirtualTrainingSessionMaster = new VirtualTrainingSessionMaster();
            int index;
            if (pSqlReader.HasRows)
            {
                index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_TRAININGID);
                if (!pSqlReader.IsDBNull(index))
                    _entVirtualTrainingSessionMaster.ID = pSqlReader.GetString(index);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_SESSION_KEY))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_SESSION_KEY);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.SessionKey = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_WEBEXUSERID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_WEBEXUSERID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.VirtualWebexSystemUserid = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_TITLE))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_TITLE);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.Title = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_TYPE))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_TYPE);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.TrainingType = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_SESSION_PASSWORD))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_SESSION_PASSWORD);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.SessionPassword = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_AGENDA))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_AGENDA);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.Agenda = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_DESCRIPTION))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_DESCRIPTION);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.Description = pSqlReader.GetString(index);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_STARTDATE))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_STARTDATE);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.StartDate = pSqlReader.GetDateTime(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_ENDDATE))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_ENDDATE);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.EndDate = pSqlReader.GetDateTime(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_DURATION))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_DURATION);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.Duration = pSqlReader.GetString(index);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_TIMEZONE))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_TIMEZONE);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.TimeZone = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_TIMEZONEID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_TIMEZONEID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.TimeZoneId = pSqlReader.GetInt32(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_OCCURENCE))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_OCCURENCE);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.Occurence = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_SESSION_KEY))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_SESSION_KEY);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.SessionKey = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_ISCANCELLED))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_ISCANCELLED);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.IsCancelled = pSqlReader.GetBoolean(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_ISACTIVE))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_ISACTIVE);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.IsActive = pSqlReader.GetBoolean(index);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.CreatedById = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_ON))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.DateCreated = pSqlReader.GetDateTime(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.LastModifiedById = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.LastModifiedDate = pSqlReader.GetDateTime(index);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_ISSELFREGISTRATION))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_ISSELFREGISTRATION);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.IsSelfRegistration = pSqlReader.GetBoolean(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_MAXREGISTRATION))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_MAXREGISTRATION);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.MaxRegistration = pSqlReader.GetInt32(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_MINREGISTRAION))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_MINREGISTRAION);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.MinRegistraion = pSqlReader.GetInt32(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_ISWAITLISTED))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_ISWAITLISTED);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.ISWaitlisted = pSqlReader.GetBoolean(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_MAXWAITLISTED))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_MAXWAITLISTED);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.MaxWaitlisted = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_AUTOAPPROVE))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_AUTOAPPROVE);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.Autoapprove = pSqlReader.GetBoolean(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_NOOFREGISTERED))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_NOOFREGISTERED);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.NoOfRegistered = pSqlReader.GetInt32(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_REPORT_TRAINING_STATUS))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_REPORT_TRAINING_STATUS);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.TrainingStatus = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_STATUS))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_STATUS);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.Status = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEEID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEEID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.AttendeeId = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_WEBEXUSERID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_WEBEXUSERID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.VirtualWebexSystemUserid = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ISADMINADDED))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ISADMINADDED);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.IsAdminAdded = pSqlReader.GetBoolean(index);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.TotalCount = pSqlReader.GetInt32(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_CreatedByName))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_CreatedByName);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.CreatedByName = pSqlReader.GetString(index);
                }

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(index))
                        _entRange.TotalRows = pSqlReader.GetInt32(index);
                    _entVirtualTrainingSessionMaster.ListRange = _entRange;
                    return _entVirtualTrainingSessionMaster;
                }
            }
            return _entVirtualTrainingSessionMaster;
        }

        /// <summary>
        /// Fill Object
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>VirtualTrainingSessionMaster</returns>
        private VirtualTrainingSessionMaster FillObjectSystemGUID(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entVirtualTrainingSessionMaster = new VirtualTrainingSessionMaster();
            int index;
            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_SYSTEMUSERGUID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_SYSTEMUSERGUID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.SystemUserGUID = pSqlReader.GetString(index);
                }
            }
            return _entVirtualTrainingSessionMaster;
        }
        //
        /// <summary>
        /// FillAllClientObject
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <param name="pRangeList"></param>
        /// <param name="pSqlObject"></param>
        /// <returns></returns>
        private VirtualTrainingSessionMaster FillAllClientObject(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            _entVirtualTrainingSessionMaster = new VirtualTrainingSessionMaster();
            int index;
            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CLIENT_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_CLIENT_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.SystemUserGUID = pSqlReader.GetString(index);
                }
            }
            return _entVirtualTrainingSessionMaster;
        }
        private VirtualTrainingSessionMaster FillAllVirtualTrainingNames(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entVirtualTrainingSessionMaster = new VirtualTrainingSessionMaster();
            int index;
            if (pSqlReader.HasRows)
            {
                index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_TRAININGID);
                if (!pSqlReader.IsDBNull(index))
                    _entVirtualTrainingSessionMaster.ID = pSqlReader.GetString(index);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_TITLE))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_TITLE);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingSessionMaster.Title = pSqlReader.GetString(index);
                }


                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(index))
                        _entRange.TotalRows = pSqlReader.GetInt32(index);
                    _entVirtualTrainingSessionMaster.ListRange = _entRange;
                    return _entVirtualTrainingSessionMaster;
                }
            }
            return _entVirtualTrainingSessionMaster;
        }
        /// <summary>
        /// To add/update a VirtualTrainingSessionMaster.
        /// </summary>
        /// <param name="pEntVirtualTrainingSessionMaster"></param>
        /// <returns>VirtualTrainingSessionMaster</returns>
        private VirtualTrainingSessionMaster UpdateVirtualTrainingSessionMaster(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster, VirtualTrainingSessionMaster.Method pMethod)
        {
            int iRowsAffected = 0;
            SqlConnection sqlConn = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingSessionMaster.PROC_VIRTUALTRAINING_UPDATE;
            _sqlObject = new SQLObject();

            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.SessionKey))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_SESSION_KEY, pEntVirtualTrainingSessionMaster.SessionKey);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_SESSION_KEY, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.TrainingType))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TYPE, pEntVirtualTrainingSessionMaster.TrainingType);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TYPE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.Title))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TITLE, pEntVirtualTrainingSessionMaster.Title);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TITLE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.SessionPassword))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_SESSION_PASSWORD, pEntVirtualTrainingSessionMaster.SessionPassword);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_SESSION_PASSWORD, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.Agenda))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_AGENDA, pEntVirtualTrainingSessionMaster.Agenda);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_AGENDA, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.Description))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_DESCRIPTION, pEntVirtualTrainingSessionMaster.Description);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_DESCRIPTION, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.StartDate != DateTime.MinValue)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_STARTDATE, pEntVirtualTrainingSessionMaster.StartDate);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_STARTDATE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.EndDate != DateTime.MinValue)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_ENDDATE, pEntVirtualTrainingSessionMaster.EndDate);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_ENDDATE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.Duration))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_DURATION, pEntVirtualTrainingSessionMaster.Duration);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_DURATION, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.TimeZone))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TIMEZONE, pEntVirtualTrainingSessionMaster.TimeZone);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TIMEZONE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.TimeZoneId != null)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TIMEZONEID, pEntVirtualTrainingSessionMaster.TimeZoneId);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TIMEZONEID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.Occurence))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_OCCURENCE, pEntVirtualTrainingSessionMaster.Occurence);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_OCCURENCE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.IsCancelled != null)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_ISCANCELLED, pEntVirtualTrainingSessionMaster.IsCancelled);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_ISCANCELLED, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.IsActive != null)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_ISACTIVE, pEntVirtualTrainingSessionMaster.IsActive);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_ISACTIVE, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.LastModifiedById))
                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntVirtualTrainingSessionMaster.LastModifiedById);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.IsSelfRegistration != null)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_ISSELFREGISTRATION, pEntVirtualTrainingSessionMaster.IsSelfRegistration);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_ISSELFREGISTRATION, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.MaxRegistration != null)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_MAXREGISTRATION, pEntVirtualTrainingSessionMaster.MaxRegistration);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_MAXREGISTRATION, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.MinRegistraion != null)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_MINREGISTRAION, pEntVirtualTrainingSessionMaster.MinRegistraion);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_MINREGISTRAION, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.ISWaitlisted != null)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_ISWAITLISTED, pEntVirtualTrainingSessionMaster.ISWaitlisted);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_ISWAITLISTED, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.MaxWaitlisted != null)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_MAXWAITLISTED, pEntVirtualTrainingSessionMaster.MaxWaitlisted);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_MAXWAITLISTED, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.Autoapprove != null)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_AUTOAPPROVE, pEntVirtualTrainingSessionMaster.Autoapprove);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_AUTOAPPROVE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.VirtualWebexSystemUserid != null)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_WEBEXUSERID, pEntVirtualTrainingSessionMaster.VirtualWebexSystemUserid);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_WEBEXUSERID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                if (pMethod == VirtualTrainingSessionMaster.Method.Update)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else if (pMethod == VirtualTrainingSessionMaster.Method.Add)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    pEntVirtualTrainingSessionMaster.ID = YPLMS.Services.IDGenerator.GetStringGUIDWithPrefix(Schema.VirtualTrainingSessionMaster.VAL_VIRTUALTRAINING_ID_PREFIX);
                }

                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TRAININGID, pEntVirtualTrainingSessionMaster.ID);
                _sqlcmd.Parameters.Add(_sqlpara);


                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingSessionMaster.ClientId);
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
            return pEntVirtualTrainingSessionMaster;
        }
        //
        private VirtualTrainingSessionMaster UpdateVirtualTrainingCountAttended(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster, VirtualTrainingSessionMaster.Method pMethod)
        {
            int iRowsAffected = 0;
            SqlConnection sqlConn = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingSessionMaster.PROC_VIRTUALTRAINING_COUNT_ATTEBDED_UPDATE;
            _sqlObject = new SQLObject();

            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.SessionKey))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_SESSION_KEY, pEntVirtualTrainingSessionMaster.SessionKey);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_SESSION_KEY, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.NoOfAttended != null)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_NO_OF_ATTENDED, pEntVirtualTrainingSessionMaster.NoOfAttended);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_NO_OF_ATTENDED, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.ReportImported != null)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_REPORT_IMPORTED, pEntVirtualTrainingSessionMaster.ReportImported);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_REPORT_IMPORTED, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.TrainingStatus))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_REPORT_TRAINING_STATUS, pEntVirtualTrainingSessionMaster.TrainingStatus);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_REPORT_TRAINING_STATUS, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingSessionMaster.ClientId);
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
            return pEntVirtualTrainingSessionMaster;
        }
        //
        private VirtualTrainingSessionMaster UpdaterVirtualTrainingNoOfRegistered(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            int iRowsAffected = 0;
            SqlConnection sqlConn = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingSessionMaster.PROC_VIRTUALTRAINING_SESSIONMASTER_UPDATE;
            _sqlObject = new SQLObject();

            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.TrainingId))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_TRAININGID, pEntVirtualTrainingSessionMaster.TrainingId);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_TRAININGID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingSessionMaster.ClientId);
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
            return pEntVirtualTrainingSessionMaster;
        }
        //
        private VirtualTrainingSessionMaster UpdaterVirtualTrainingTotalTime(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            int iRowsAffected = 0;
            SqlConnection sqlConn = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingSessionMaster.PROC_VIRTUALTRAINING_TOTAL_TIME_UPDATE;
            _sqlObject = new SQLObject();

            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.SessionKey))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_SESSION_KEY, pEntVirtualTrainingSessionMaster.SessionKey);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_SESSION_KEY, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.TrainingDuration != null)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_REPORT_TRAINING_DURATION, pEntVirtualTrainingSessionMaster.TrainingDuration);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_REPORT_TRAINING_DURATION, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.TrainingStartTime != DateTime.MinValue)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_REPORT_TRAINING_START_TIME, pEntVirtualTrainingSessionMaster.TrainingStartTime);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_REPORT_TRAINING_START_TIME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.TrainingEndTime != DateTime.MinValue)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_REPORT_TRAINING_END_TIME, pEntVirtualTrainingSessionMaster.TrainingEndTime);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_REPORT_TRAINING_END_TIME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.NoOfAttended != null)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_NO_OF_ATTENDED, pEntVirtualTrainingSessionMaster.NoOfAttended);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_NO_OF_ATTENDED, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.ReportImported != null)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_REPORT_IMPORTED, pEntVirtualTrainingSessionMaster.ReportImported);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_REPORT_IMPORTED, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.TrainingStatus))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_REPORT_TRAINING_STATUS, pEntVirtualTrainingSessionMaster.TrainingStatus);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_REPORT_TRAINING_STATUS, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingSessionMaster.ClientId);
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
            return pEntVirtualTrainingSessionMaster;
        }
        /// <summary>
        /// To add/update a VirtualTrainingSessionMaster Nomination.
        /// </summary>
        /// <param name="pEntVirtualTrainingSessionMaster"></param>
        /// <returns>VirtualTrainingSessionMaster</returns>
        private VirtualTrainingSessionMaster UpdateRegisterVirtualTrainingSessionMaster(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            int iRowsAffected = 0;
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingSessionMaster.PROC_VIRTUALTRAINING_UPDATE_REGISTER;
            _sqlObject = new SQLObject();

            if (pEntVirtualTrainingSessionMaster.NoOfRegistered != null)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_NOOFREGISTERED, pEntVirtualTrainingSessionMaster.NoOfRegistered);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_NOOFREGISTERED, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntVirtualTrainingSessionMaster.ID != null)
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TRAININGID, pEntVirtualTrainingSessionMaster.ID);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TRAININGID, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingSessionMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entVirtualTrainingSessionMaster = FillObject(_sqlreader, false, _sqlObject);

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
            return _entVirtualTrainingSessionMaster;
        }


        #region ActivateDeactive
        public List<VirtualTrainingSessionMaster> ActivateDeActivateStatusList(List<VirtualTrainingSessionMaster> pEntListVirtualTrainingSessionMaster)
        {
            SqlConnection sqlConn = new SqlConnection();
            List<VirtualTrainingSessionMaster> entListVirtualTrainingSessionMaster = new List<VirtualTrainingSessionMaster>();
            List<VirtualTrainingSessionMaster> pentListVirtualTrainingSessionMaster = new List<VirtualTrainingSessionMaster>();
            VirtualTrainingSessionMaster entVirtualTrainingSessionMaster;
            try
            {
                _sqlObject = new SQLObject();
                _sqladapter = new SqlDataAdapter();
                _dtable = new DataTable();
                DataTable dQuestionTable = new DataTable();
                int iBatchSize = 0;
                if (pEntListVirtualTrainingSessionMaster.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_TRAININGID);
                    _dtable.Columns.Add(Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_ISACTIVE);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    foreach (VirtualTrainingSessionMaster objBase in pEntListVirtualTrainingSessionMaster)
                    {
                        entVirtualTrainingSessionMaster = new VirtualTrainingSessionMaster();
                        entVirtualTrainingSessionMaster = objBase;

                        DataRow drow = _dtable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entVirtualTrainingSessionMaster.ClientId);

                        if (!String.IsNullOrEmpty(entVirtualTrainingSessionMaster.ID))
                        {
                            drow[Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_TRAININGID] = entVirtualTrainingSessionMaster.ID;
                            drow[Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_ISACTIVE] = entVirtualTrainingSessionMaster.IsActive;


                            if (!String.IsNullOrEmpty(entVirtualTrainingSessionMaster.LastModifiedById))
                            {
                                drow[Schema.Common.COL_MODIFIED_BY] = entVirtualTrainingSessionMaster.LastModifiedById;
                            }
                            iBatchSize = iBatchSize + 1;
                            _dtable.Rows.Add(drow);
                            entListVirtualTrainingSessionMaster.Add(entVirtualTrainingSessionMaster);
                        }
                    }
                    if (_dtable.Rows.Count > 0)
                    {
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.VirtualTrainingSessionMaster.PROC_VIRTUALTRAINING_BULK_UPDATE_ACTIVE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        sqlConn = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = sqlConn;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TRAININGID, SqlDbType.VarChar, 100, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_TRAININGID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_ISACTIVE, SqlDbType.Bit, 100, Schema.VirtualTrainingSessionMaster.COL_VIRTUALTRAINING_ISACTIVE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        int _iDeleteRows = _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();

                        pentListVirtualTrainingSessionMaster = new List<VirtualTrainingSessionMaster>();
                        EntityRange _entRange = new EntityRange();
                        _entRange.TotalRows = _iDeleteRows;
                        entVirtualTrainingSessionMaster = new VirtualTrainingSessionMaster();
                        entVirtualTrainingSessionMaster.ListRange = _entRange;
                        pentListVirtualTrainingSessionMaster.Add(entVirtualTrainingSessionMaster);
                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pentListVirtualTrainingSessionMaster;
        }
        #endregion

        /// <summary>
        /// Delete VirtualTrainingSessionMaster by ID
        /// </summary>
        /// <param name="pEntVirtualTrainingSessionMaster">VirtualTrainingSessionMaster with ID</param>
        /// <returns>Deleted VirtualTrainingSessionMaster with only ID</returns>
        private VirtualTrainingSessionMaster DeleteVirtualTrainingSessionMaster(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            VirtualTrainingSessionMaster entVirtualTrainingSessionMaster = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingSessionMaster.PROC_VIRTUALTRAINING_DELETE;
            _sqlObject = new SQLObject();
            int i = 0;
            if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.ID))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TRAININGID, pEntVirtualTrainingSessionMaster.ID);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TRAININGID, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingSessionMaster.ClientId);
                i = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                if (i < 0)
                {
                    entVirtualTrainingSessionMaster = new VirtualTrainingSessionMaster();
                    entVirtualTrainingSessionMaster.ID = pEntVirtualTrainingSessionMaster.ID;
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entVirtualTrainingSessionMaster;
        }

        /// <summary>
        /// VirtualTrainingSessionMaster Range
        /// </summary>
        /// <param name="GetAllVirtualTrainingSessionSelfStatus"></param>
        /// <returns></returns>
        private List<VirtualTrainingSessionMaster> GetAllVirtualTrainingSessionSelfStatus(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingSessionMaster.PROC_VIRTUALTRAINING_GETALLSESSIONSSELFSTATUS;
            _entListVirtualTrainingSessionMaster = new List<VirtualTrainingSessionMaster>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingSessionMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.Title))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_TITLE, pEntVirtualTrainingSessionMaster.Title);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntVirtualTrainingSessionMaster.StartDate != DateTime.MinValue)
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_STARTDATE, pEntVirtualTrainingSessionMaster.StartDate);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!String.IsNullOrEmpty(pEntVirtualTrainingSessionMaster.SystemUserGUID))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingSessionMaster.PARA_VIRTUALTRAINING_SYSTEMUSERGUID, pEntVirtualTrainingSessionMaster.SystemUserGUID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!String.IsNullOrEmpty(Convert.ToString(pEntVirtualTrainingSessionMaster.CreatedById)))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntVirtualTrainingSessionMaster.CreatedById);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntVirtualTrainingSessionMaster.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntVirtualTrainingSessionMaster.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntVirtualTrainingSessionMaster.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntVirtualTrainingSessionMaster.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingSessionMaster = FillObject(_sqlreader, true, _sqlObject);
                    _entListVirtualTrainingSessionMaster.Add(_entVirtualTrainingSessionMaster);
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
            return _entListVirtualTrainingSessionMaster;
        }

        #region Interface Methods
        /// <summary>
        /// Get VirtualTrainingSessionMaster By ID
        /// </summary>
        /// <param name="pEntVirtualTrainingSessionMaster"></param>
        /// <returns></returns>
        public VirtualTrainingSessionMaster Get(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            return GetVirtualTrainingSessionMasterByID(pEntVirtualTrainingSessionMaster);
        }

        /// <summary>
        /// Get VirtualTrainingSessionMaster By AdminAccountUserID
        /// </summary>
        /// <param name="pEntVirtualTrainingSessionMaster"></param>
        /// <returns></returns>
        public VirtualTrainingSessionMaster GetAdminAccountUserID(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            return GetVirtualTrainingSessionMasterByAdminAccountUserID(pEntVirtualTrainingSessionMaster);
        }
        /// <summary>
        /// List of All VirtualTrainingSessionMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingSessionMaster"></param>
        /// <returns></returns>

        public List<VirtualTrainingSessionMaster> GetAllSession(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            return GetAllSessionVirtualTrainingSessionUserMaster(pEntVirtualTrainingSessionMaster);
        }

        /// <summary>
        /// List of All VirtualTrainingSessionMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingSessionMaster"></param>
        /// <returns></returns>

        public List<VirtualTrainingSessionMaster> GetAllSelfTrainingStatus(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            return GetAllVirtualTrainingSessionSelfStatus(pEntVirtualTrainingSessionMaster);
        }
        /// <summary>
        /// List of All VirtualTrainingSessionMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingSessionMaster"></param>
        /// <returns></returns>

        public List<VirtualTrainingSessionMaster> GetAll(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            return GetAllVirtualTrainingSessionMaster(pEntVirtualTrainingSessionMaster);
        }

        public List<VirtualTrainingSessionMaster> GetAllSessionKey(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            return GetAllVirtualSessionKey(pEntVirtualTrainingSessionMaster);
        }
        //
        public List<VirtualTrainingSessionMaster> GetAllVIRTUALTRAININGClient(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            return GetAllVIRTUALTRAININGClientID(pEntVirtualTrainingSessionMaster);
        }
        public List<VirtualTrainingSessionMaster> GetSystemUserGUID(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            return GetVirtualSystemUserGUID(pEntVirtualTrainingSessionMaster);
        }
        //
        public List<VirtualTrainingSessionMaster> Search_VIRTUALTRAINING_Names(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            return Get_VIRTUALTRAINING_Names(pEntVirtualTrainingSessionMaster);
        }

        /// <summary>
        /// Add VirtualTrainingSessionMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingSessionMaster"></param>
        /// <returns></returns>
        public VirtualTrainingSessionMaster Add(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            return UpdateVirtualTrainingSessionMaster(pEntVirtualTrainingSessionMaster, VirtualTrainingSessionMaster.Method.Add);
        }
        /// <summary>
        /// Update VirtualTrainingSessionMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingSessionMaster"></param>
        /// <returns></returns>
        public VirtualTrainingSessionMaster Update(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            return UpdateVirtualTrainingSessionMaster(pEntVirtualTrainingSessionMaster, VirtualTrainingSessionMaster.Method.Update);
        }

        public VirtualTrainingSessionMaster UpdateCountAttended(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            return UpdateVirtualTrainingCountAttended(pEntVirtualTrainingSessionMaster, VirtualTrainingSessionMaster.Method.Update);
        }

        /// <summary>
        /// Delete VirtualTrainingSessionMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingSessionMaster"></param>
        /// <returns>VirtualTrainingSessionMaster</returns>
        public VirtualTrainingSessionMaster Delete(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            return DeleteVirtualTrainingSessionMaster(pEntVirtualTrainingSessionMaster);
        }

        /// <summary>
        /// UpdateRegister
        /// </summary>
        /// <param name="pEntVirtualTrainingSessionMaster"></param>
        /// <returns></returns>
        public VirtualTrainingSessionMaster UpdateRegister(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            return UpdateRegisterVirtualTrainingSessionMaster(pEntVirtualTrainingSessionMaster);
        }
        /// <summary>
        /// UpdateRegister
        /// </summary>
        /// <param name="pEntVirtualTrainingSessionMaster"></param>
        /// <returns></returns>
        public VirtualTrainingSessionMaster UpdateTrainingTotalTime(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            return UpdaterVirtualTrainingTotalTime(pEntVirtualTrainingSessionMaster);
        }

        public VirtualTrainingSessionMaster UpdateNoOfRegistered(VirtualTrainingSessionMaster pEntVirtualTrainingSessionMaster)
        {
            return UpdaterVirtualTrainingNoOfRegistered(pEntVirtualTrainingSessionMaster);
        }
        #endregion

    }
}
