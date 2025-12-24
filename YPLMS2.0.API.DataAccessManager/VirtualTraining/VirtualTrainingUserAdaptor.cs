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
    public class VirtualTrainingUserAdaptor : IDataManager<VirtualTrainingUserMaster>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        VirtualTrainingUserMaster _entVirtualTrainingUserMaster = null;
        List<VirtualTrainingUserMaster> _entListVirtualTrainingUserMaster = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.VirtualTrainingUserMaster.VIRTUALTRAINING_USER_DAM_ERROR;


        #endregion

        public VirtualTrainingUserAdaptor()
        {
        }

        private List<VirtualTrainingUserMaster> GetAllTrainingUsersVirtualTrainingUserMaster(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {

            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingUserMaster.PROC_VIRTUALTRAINING_USER_GET;
            _entListVirtualTrainingUserMaster = new List<VirtualTrainingUserMaster>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingUserMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);


                if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.VirtualSystemUserid))
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_SYSTEMUSERID, pEntVirtualTrainingUserMaster.VirtualSystemUserid.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_SYSTEMUSERID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                //if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.UserFirstName))
                //    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_FIRST_NAME, pEntVirtualTrainingUserMaster.UserFirstName.ToString());
                //else
                //    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_FIRST_NAME, null);
                //_sqlcmd.Parameters.Add(_sqlpara);

                //if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.UserLastName))
                //    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_LAST_NAME, pEntVirtualTrainingUserMaster.UserLastName.ToString());
                //else
                //    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_LAST_NAME, null);
                //_sqlcmd.Parameters.Add(_sqlpara);

                //if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.Userid))
                //    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERID, pEntVirtualTrainingUserMaster.Userid.ToString());
                //else
                //    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERID, null);
                //_sqlcmd.Parameters.Add(_sqlpara);

                if (pEntVirtualTrainingUserMaster.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntVirtualTrainingUserMaster.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntVirtualTrainingUserMaster.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntVirtualTrainingUserMaster.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingUserMaster = FillObject(_sqlreader, true, _sqlObject);
                    _entListVirtualTrainingUserMaster.Add(_entVirtualTrainingUserMaster);
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

            return _entListVirtualTrainingUserMaster;

        }

        //
        private List<VirtualTrainingUserMaster> GetAllWebServiceDefaultUser(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {

            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingUserMaster.PROC_VIRTUALTRAINING_WEBSERVICE_DEFAULTUSER_GET;
            _entListVirtualTrainingUserMaster = new List<VirtualTrainingUserMaster>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingUserMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingUserMaster = FillObjectWebServiceDefaultUser(_sqlreader, true, _sqlObject);
                    _entListVirtualTrainingUserMaster.Add(_entVirtualTrainingUserMaster);
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

            return _entListVirtualTrainingUserMaster;

        }
        /// <summary>
        /// Fill Object
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>VirtualTrainingUserMaster</returns>
        private VirtualTrainingUserMaster FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entVirtualTrainingUserMaster = new VirtualTrainingUserMaster();
            int index;
            if (pSqlReader.HasRows)
            {

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_SYSTEMUSERID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_SYSTEMUSERID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.ID = pSqlReader.GetString(index);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USERID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USERID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.Userid = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_FIRST_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_FIRST_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.UserFirstName = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_LAST_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_LAST_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.UserLastName = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_EMAIL))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_EMAIL);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.EmailId = pSqlReader.GetString(index);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_PASSWORD))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_PASSWORD);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.Password = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_SITEID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_SITEID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.TrainingUserSiteId = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_PARTNERID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_PARTNERID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.TrainingUserPartnerId = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_TYPE))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_TYPE);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.TrainingType = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_XMLSERVER))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_XMLSERVER);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.XMLserver = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_ISACTIVE))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_ISACTIVE);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.TrainingUserIsActive = pSqlReader.GetBoolean(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_EMAILID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_EMAILID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.EmailId = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_WEBEX_URL))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_WEBEX_URL);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.WebExURL = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_ZOOMACCESSTOKEN))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_ZOOMACCESSTOKEN);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.ZoomAccessToken = pSqlReader.GetString(index);
                }
                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(index))
                        _entRange.TotalRows = pSqlReader.GetInt32(index);
                    _entVirtualTrainingUserMaster.ListRange = _entRange;
                    return _entVirtualTrainingUserMaster;
                }
            }
            return _entVirtualTrainingUserMaster;
        }


        /// Fill Object
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>VirtualTrainingUserMaster</returns>
        private VirtualTrainingUserMaster FillObjectGroupAdmin(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entVirtualTrainingUserMaster = new VirtualTrainingUserMaster();
            int index;
            if (pSqlReader.HasRows)
            {

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USERMASTER_SYSTEMUSERGUID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USERMASTER_SYSTEMUSERGUID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.ID = pSqlReader.GetString(index);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_FIRST_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_FIRST_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.UserFirstName = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_LAST_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_LAST_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.UserLastName = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_ISACTIVE))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_ISACTIVE);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.TrainingUserIsActive = pSqlReader.GetBoolean(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_EMAILID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingAttendeeMaster.COL_VIRTUALTRAINING_ATTENDEE_EMAILID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.EmailId = pSqlReader.GetString(index);
                }

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(index))
                        _entRange.TotalRows = pSqlReader.GetInt32(index);
                    _entVirtualTrainingUserMaster.ListRange = _entRange;
                    return _entVirtualTrainingUserMaster;
                }
            }
            return _entVirtualTrainingUserMaster;
        }
        //
        /// <summary>
        /// FillObjectWebServiceDefaultUser
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <param name="pRangeList"></param>
        /// <param name="pSqlObject"></param>
        /// <returns></returns>
        private VirtualTrainingUserMaster FillObjectWebServiceDefaultUser(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entVirtualTrainingUserMaster = new VirtualTrainingUserMaster();
            int index;
            if (pSqlReader.HasRows)
            {

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USERID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USERID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.Userid = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_FIRST_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_FIRST_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.UserFirstName = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_LAST_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_LAST_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.UserLastName = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_EMAIL))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_EMAIL);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.EmailId = pSqlReader.GetString(index);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_PASSWORD))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_PASSWORD);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.Password = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_SITEID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_SITEID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.TrainingUserSiteId = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_PARTNERID))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_USER_PARTNERID);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.TrainingUserPartnerId = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_TYPE))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_TYPE);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.TrainingType = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_XMLSERVER))
                {
                    index = pSqlReader.GetOrdinal(Schema.VirtualTrainingUserMaster.COL_VIRTUALTRAINING_XMLSERVER);
                    if (!pSqlReader.IsDBNull(index))
                        _entVirtualTrainingUserMaster.XMLserver = pSqlReader.GetString(index);
                }

                //if (pRangeList)
                //{
                //    _entRange = new EntityRange();
                //    index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                //    if (!pSqlReader.IsDBNull(index))
                //        _entRange.TotalRows = pSqlReader.GetInt32(index);
                //    _entVirtualTrainingUserMaster.ListRange = _entRange;
                //    return _entVirtualTrainingUserMaster;
                //}
            }
            return _entVirtualTrainingUserMaster;
        }

        private VirtualTrainingUserMaster VirtualTrainingUserMasterAddUser(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster, VirtualTrainingUserMaster.Method pMethod)
        {
            int iRowsAffected = 0;
            SqlConnection sqlConn = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingUserMaster.PROC_VIRTUALTRAINING_USER_UPDATE;
            _sqlObject = new SQLObject();

            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.Userid))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERID, pEntVirtualTrainingUserMaster.Userid);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.TrainingType))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_TYPE, pEntVirtualTrainingUserMaster.TrainingType);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_TYPE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.UserFirstName))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_FIRST_NAME, pEntVirtualTrainingUserMaster.UserFirstName);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_FIRST_NAME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.UserLastName))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_LAST_NAME, pEntVirtualTrainingUserMaster.UserLastName);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_LAST_NAME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.Password))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_PASSWORD, pEntVirtualTrainingUserMaster.Password);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_PASSWORD, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if ((pEntVirtualTrainingUserMaster.TrainingUserIsActive))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_ISACTIVE, pEntVirtualTrainingUserMaster.TrainingUserIsActive);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_ISACTIVE, pEntVirtualTrainingUserMaster.TrainingUserIsActive);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.TrainingUserSiteId))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_SITEID, pEntVirtualTrainingUserMaster.TrainingUserSiteId);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_SITEID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.TrainingUserPartnerId))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_PARTNERID, pEntVirtualTrainingUserMaster.TrainingUserPartnerId);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_PARTNERID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.XMLserver))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_XMLSERVER, pEntVirtualTrainingUserMaster.XMLserver);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_XMLSERVER, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.EmailId))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_EMAIL, pEntVirtualTrainingUserMaster.EmailId);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_EMAIL, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.LastModifiedById))
                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntVirtualTrainingUserMaster.LastModifiedById);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.WebExURL))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_WEBEX_URL, pEntVirtualTrainingUserMaster.WebExURL);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_WEBEX_URL, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            //Column added for ZOOM Access Token
            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.ZoomAccessToken))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_ZOOMACCESSTOKEN, pEntVirtualTrainingUserMaster.ZoomAccessToken);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_ZOOMACCESSTOKEN, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            //if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.VirtualSystemUserid))
            //    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_SYSTEMUSERID, pEntVirtualTrainingUserMaster.VirtualSystemUserid);
            //else
            //    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_SYSTEMUSERID, null);
            //_sqlcmd.Parameters.Add(_sqlpara);



            try
            {
                if (pMethod == VirtualTrainingUserMaster.Method.Update)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else if (pMethod == VirtualTrainingUserMaster.Method.Add)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    pEntVirtualTrainingUserMaster.VirtualSystemUserid = YPLMS.Services.IDGenerator.GetStringGUIDWithPrefix(Schema.VirtualTrainingUserMaster.VAL_VIRTUALTRAINING_USERID_PREFIX);
                }
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_SYSTEMUSERID, pEntVirtualTrainingUserMaster.VirtualSystemUserid);
                _sqlcmd.Parameters.Add(_sqlpara);



                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingUserMaster.ClientId);
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

            return pEntVirtualTrainingUserMaster;
        }

        /// <summary>
        /// VirtualTrainingUserMaster Range
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <returns></returns>
        private List<VirtualTrainingUserMaster> GetAllVirtualTrainingUserMaster(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingUserMaster.PROC_VIRTUALTRAINING_USER_GETALL;
            _entListVirtualTrainingUserMaster = new List<VirtualTrainingUserMaster>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingUserMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);


                if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.Userid))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERID, pEntVirtualTrainingUserMaster.Userid);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.UserFirstName))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_FIRST_NAME, pEntVirtualTrainingUserMaster.UserFirstName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.UserLastName))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_LAST_NAME, pEntVirtualTrainingUserMaster.UserLastName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.EmailId))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_EMAIL, pEntVirtualTrainingUserMaster.EmailId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntVirtualTrainingUserMaster.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntVirtualTrainingUserMaster.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntVirtualTrainingUserMaster.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntVirtualTrainingUserMaster.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntVirtualTrainingUserMaster.ListRange.RequestedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingUserMaster = FillObject(_sqlreader, true, _sqlObject);
                    _entListVirtualTrainingUserMaster.Add(_entVirtualTrainingUserMaster);
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
            return _entListVirtualTrainingUserMaster;
        }



        /// VirtualTrainingUserMaster Range
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <returns></returns>
        private List<VirtualTrainingUserMaster> GetAllVirtualTrainingUserMappingMaster(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingUserMaster.PROC_VIRTUALTRAINING_USER_MAPPING_GETALL;
            _entListVirtualTrainingUserMaster = new List<VirtualTrainingUserMaster>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingUserMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);


                if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.Userid))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERID, pEntVirtualTrainingUserMaster.Userid);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.UserFirstName))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_FIRST_NAME, pEntVirtualTrainingUserMaster.UserFirstName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.UserLastName))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_LAST_NAME, pEntVirtualTrainingUserMaster.UserLastName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.EmailId))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USER_EMAIL, pEntVirtualTrainingUserMaster.EmailId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntVirtualTrainingUserMaster.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntVirtualTrainingUserMaster.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntVirtualTrainingUserMaster.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntVirtualTrainingUserMaster.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntVirtualTrainingUserMaster.ListRange.RequestedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingUserMaster = FillObject(_sqlreader, true, _sqlObject);
                    _entListVirtualTrainingUserMaster.Add(_entVirtualTrainingUserMaster);
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
            return _entListVirtualTrainingUserMaster;
        }

        /// <summary>
        /// To get VirtualTrainingUserMaster details by ID.
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <returns>VirtualTrainingUserMaster</returns>
        private VirtualTrainingUserMaster GetVirtualTrainingUserMasterByID(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingUserMaster.PROC_VIRTUALTRAINING_USER_GET;
            _sqlObject = new SQLObject();

            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.ID))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_SYSTEMUSERID, pEntVirtualTrainingUserMaster.ID.ToString());
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_SYSTEMUSERID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingUserMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entVirtualTrainingUserMaster = FillObject(_sqlreader, false, _sqlObject);

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
            return _entVirtualTrainingUserMaster;
        }



        public VirtualTrainingUserMaster DeleteVirtualTrainingUserMaster(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            int iRowsAffected = 0;
            SqlConnection sqlConn = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingUserMaster.PROC_VIRTUALTRAINING_USER_DELETE;
            _sqlObject = new SQLObject();

            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.VirtualSystemUserid))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_SYSTEMUSERID, pEntVirtualTrainingUserMaster.VirtualSystemUserid);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_SYSTEMUSERID, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            try
            {
                //if (pMethod == VirtualTrainingUserMaster.Method.Update)
                //{
                //    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                //    _sqlcmd.Parameters.Add(_sqlpara);
                //}
                //else if (pMethod == VirtualTrainingUserMaster.Method.Add)
                //{
                //    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                //    _sqlcmd.Parameters.Add(_sqlpara);

                //    pEntVirtualTrainingUserMaster.ID = YPLMS.Services.IDGenerator.GetStringGUIDWithPrefix(Schema.VirtualTrainingUserMaster.VAL_VIRTUALTRAINING_USERID_PREFIX);
                //}

                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingUserMaster.ClientId);
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

            return pEntVirtualTrainingUserMaster;
        }

        /// <summary>
        /// To FindIsNameAvailable
        /// </summary>
        /// <param name="pEntUserMaster"></param>
        /// <returns>Category Object</returns>
        private object FindUserNameAvailable(VirtualTrainingUserMaster pEntUserMaster)
        {
            object obj = null;
            _sqlObject = new SQLObject();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntUserMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.VirtualTrainingUserMaster.PROC_VIRTUALTRAINING_USER_AVAILABLE, sqlConnection);

                if (!String.IsNullOrEmpty(pEntUserMaster.Userid))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERID, pEntUserMaster.Userid);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
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

            return obj;
        }


        /// <summary>
        /// VirtualTrainingUserMaster Range
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <returns></returns>
        private List<VirtualTrainingUserMaster> AdminGetAllUsersVirtualTrainingMaster(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingUserMaster.PROC_VIRTUALTRAINING_GET_GROUP_ADMIN_USERLIST_ALL;
            _entListVirtualTrainingUserMaster = new List<VirtualTrainingUserMaster>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingUserMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);


                if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.UserTypeId))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERMASTER_USERTYPEID, pEntVirtualTrainingUserMaster.UserTypeId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.VirtualSystemUserid))
                {
                    _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERMASTER_VIRTUALSYSTEMUSERID, pEntVirtualTrainingUserMaster.VirtualSystemUserid);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntVirtualTrainingUserMaster.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntVirtualTrainingUserMaster.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntVirtualTrainingUserMaster.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntVirtualTrainingUserMaster.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntVirtualTrainingUserMaster.ListRange.RequestedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingUserMaster = FillObjectGroupAdmin(_sqlreader, true, _sqlObject);
                    _entListVirtualTrainingUserMaster.Add(_entVirtualTrainingUserMaster);
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
            return _entListVirtualTrainingUserMaster;
        }

        /// <summary>
        /// Delete VirtualTrainingUserMaster by ID
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster">VirtualTrainingUserMaster with ID</param>
        /// <returns>Deleted VirtualTrainingUserMaster with only ID</returns>
        private VirtualTrainingUserMaster DeleteVirtualTrainingUserMasterMapping(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            VirtualTrainingUserMaster entVirtualTrainingUserMaster = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingUserMaster.PROC_VIRTUALTRAINING_DELETE_GROUP_ADMIN_MAPPING_USERLIST;
            _sqlObject = new SQLObject();
            int i = 0;
            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.ID))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERMASTER_SYSTEMUSERGUID, pEntVirtualTrainingUserMaster.ID);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERMASTER_SYSTEMUSERGUID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.VirtualSystemUserid))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERMASTER_VIRTUALSYSTEMUSERID, pEntVirtualTrainingUserMaster.VirtualSystemUserid);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERMASTER_VIRTUALSYSTEMUSERID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingUserMaster.ClientId);
                i = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                if (i < 0)
                {
                    entVirtualTrainingUserMaster = new VirtualTrainingUserMaster();
                    entVirtualTrainingUserMaster.ID = pEntVirtualTrainingUserMaster.ID;
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entVirtualTrainingUserMaster;
        }



        /// <summary>
        /// To add/update a VirtualTraining Admin Group Master.
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <returns>VirtualTrainingUserMaster</returns>
        private VirtualTrainingUserMaster UpdateVirtualTrainingAdminGroupMasterMapping(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster, VirtualTrainingUserMaster.Method pMethod)
        {
            int iRowsAffected = 0;
            SqlConnection sqlConn = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingUserMaster.PROC_VIRTUALTRAINING_UPDATE_GROUP_ADMIN_MAPPING_USERLIST;
            _sqlObject = new SQLObject();

            _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERMASTER_SYSTEMUSERGUID, pEntVirtualTrainingUserMaster.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.VirtualSystemUserid))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERMASTER_VIRTUALSYSTEMUSERID, pEntVirtualTrainingUserMaster.VirtualSystemUserid);
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERMASTER_VIRTUALSYSTEMUSERID, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.LastModifiedById))
                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntVirtualTrainingUserMaster.LastModifiedById);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                if (pMethod == VirtualTrainingUserMaster.Method.Update)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else if (pMethod == VirtualTrainingUserMaster.Method.Add)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingUserMaster.ClientId);
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
            return pEntVirtualTrainingUserMaster;
        }
        //Add by Sujit
        /// <summary>
        /// Event/SessionMappedResources Only
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <returns></returns>
        private List<VirtualTrainingUserMaster> GetAllVirtualTrainingGroupAdminMasterMapping(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.VirtualTrainingUserMaster.PROC_VIRTUALTRAINING_GET_GROUP_ADMIN_MAPPING_USERLIST_ALL;
            _entListVirtualTrainingUserMaster = new List<VirtualTrainingUserMaster>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();

            if (!String.IsNullOrEmpty(pEntVirtualTrainingUserMaster.VirtualSystemUserid))
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERMASTER_VIRTUALSYSTEMUSERID, pEntVirtualTrainingUserMaster.VirtualSystemUserid.ToString());
            else
                _sqlpara = new SqlParameter(Schema.VirtualTrainingUserMaster.PARA_VIRTUALTRAINING_USERMASTER_VIRTUALSYSTEMUSERID, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntVirtualTrainingUserMaster.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                if (pEntVirtualTrainingUserMaster.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntVirtualTrainingUserMaster.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntVirtualTrainingUserMaster.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntVirtualTrainingUserMaster.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entVirtualTrainingUserMaster = FillObjectGroupAdmin(_sqlreader, true, _sqlObject);
                    _entListVirtualTrainingUserMaster.Add(_entVirtualTrainingUserMaster);
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
            return _entListVirtualTrainingUserMaster;
        }



        #region Interface Methods

        /// <summary>
        /// List of All VirtualTrainingUserMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingRefMaterialMaster"></param>
        /// <returns></returns>
        public List<VirtualTrainingUserMaster> GetAllGroupAdminMapping(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            return GetAllVirtualTrainingGroupAdminMasterMapping(pEntVirtualTrainingUserMaster);
        }

        /// <summary>
        /// Get VirtualTrainingUserMaster By ID
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <returns></returns>

        public VirtualTrainingUserMaster AddGroupAdmin(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            return UpdateVirtualTrainingAdminGroupMasterMapping(pEntVirtualTrainingUserMaster, VirtualTrainingUserMaster.Method.AddGroupAdmin);
        }

        /// <summary>
        /// Get VirtualTrainingUserMaster By ID
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <returns></returns>
        public VirtualTrainingUserMaster Get(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            return GetVirtualTrainingUserMasterByID(pEntVirtualTrainingUserMaster);
            //return null;
        }
        /// <summary>
        /// List of All VirtualTrainingUserMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <returns></returns>
        public List<VirtualTrainingUserMaster> GetAll(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            return GetAllVirtualTrainingUserMaster(pEntVirtualTrainingUserMaster);

        }
        /// <summary>
        /// List of All VirtualTrainingUserMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <returns></returns>
        public List<VirtualTrainingUserMaster> GetUserMappingAll(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            return GetAllVirtualTrainingUserMappingMaster(pEntVirtualTrainingUserMaster);

        }

        /// <summary> 
        /// List of All VirtualTrainingUserMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <returns></returns>
        public List<VirtualTrainingUserMaster> GetAllTrainingUsers(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            return GetAllTrainingUsersVirtualTrainingUserMaster(pEntVirtualTrainingUserMaster);
        }
        //
        /// <summary>
        /// GetWebServiceDefaultUser
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <returns></returns>
        public List<VirtualTrainingUserMaster> GetWebServiceDefaultUser(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            return GetAllWebServiceDefaultUser(pEntVirtualTrainingUserMaster);
        }
        /// <summary>
        /// Add VirtualTrainingUserMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <returns></returns>
        public VirtualTrainingUserMaster Add(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            return VirtualTrainingUserMasterAddUser(pEntVirtualTrainingUserMaster, VirtualTrainingUserMaster.Method.Add);
        }

        /// <summary>
        /// Update VirtualTrainingUserMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <returns></returns>
        public VirtualTrainingUserMaster Update(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            return VirtualTrainingUserMasterAddUser(pEntVirtualTrainingUserMaster, VirtualTrainingUserMaster.Method.Update);
        }



        /// <summary>
        /// Delete VirtualTrainingUserMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <returns>VirtualTrainingUserMaster</returns>
        public VirtualTrainingUserMaster Delete(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            return DeleteVirtualTrainingUserMaster(pEntVirtualTrainingUserMaster);

        }
        /// <summary>
        /// Delete VirtualTrainingUserMaster
        /// </summary>
        /// <param name="pEntVirtualTrainingUserMaster"></param>
        /// <returns>VirtualTrainingUserMaster</returns>
        public VirtualTrainingUserMaster DeleteGroupAdmin(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            return DeleteVirtualTrainingUserMasterMapping(pEntVirtualTrainingUserMaster);

        } /// <summary>
          /// List of All VirtualTrainingUserMaster
          /// </summary>
          /// <param name="pEntVirtualTrainingUserMaster"></param>
          /// <returns></returns>
        public List<VirtualTrainingUserMaster> AdminGetAllUsers(VirtualTrainingUserMaster pEntVirtualTrainingUserMaster)
        {
            return AdminGetAllUsersVirtualTrainingMaster(pEntVirtualTrainingUserMaster);

        }

        public object GetUserNameAvailable(VirtualTrainingUserMaster pEntUserMaster)
        {
            return FindUserNameAvailable(pEntUserMaster);
        }


        #endregion
    }
}
