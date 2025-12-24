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
    public class OTPAdaptor : IDataManager<OTP>, IOTPAdaptor<OTP>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        SqlConnection _sqlcon = null;
        OTP _entOTP = null;
        List<OTP> _entListOTP = null;
        EntityRange _entRange = null;
        DataSet _dset = null;
        DataTable _dtable = null;
        string _strMessageId = YPLMS.Services.Messages.User.LEARNER_ERROR;
        static int iRecordsUpdated = 0;
        SQLObject _sqlObject = null;
        SqlDataAdapter _sqladapter = null;
        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public OTPAdaptor()
        {
        }
        /// <summary>
        /// Fill reader object for Get All
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>OTP Get All Object</returns>
        private OTP FillObjectForGetAll(SqlDataReader pReader, SQLObject pSqlObject)
        {
            OTP entOTP = new OTP();
            EntityRange entRange = new EntityRange();
            int index;
            if (pReader.HasRows)
            {

                if (pSqlObject.ReaderHasColumn(pReader, Schema.OTP.COL_SYSTEMUSERGUID))
                {
                    index = pReader.GetOrdinal(Schema.OTP.COL_SYSTEMUSERGUID);
                    if (!pReader.IsDBNull(index))
                        entOTP.SystemUserGuid = pReader.GetString(index);
                }


                if (pSqlObject.ReaderHasColumn(pReader, Schema.OTP.COL_EXPIRE_DATETIME))
                {
                    index = pReader.GetOrdinal(Schema.OTP.COL_EXPIRE_DATETIME);
                    if (!pReader.IsDBNull(index))
                        entOTP.ExpireDateTime = pReader.GetDateTime(index);
                }

                if (pSqlObject.ReaderHasColumn(pReader, Schema.OTP.COL_OTP_NUMBER))
                {
                    index = pReader.GetOrdinal(Schema.OTP.COL_OTP_NUMBER);
                    if (!pReader.IsDBNull(index))
                        entOTP.OTPNumber = pReader.GetString(index);
                }
            }
            return entOTP;
        }

        public OTP AddOTP(OTP pEntOTP)
        {
            try
            {
                _sqlObject = new SQLObject();
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.OTP.PROC_ADDUPDATE_OTP;
                _strConnString = _sqlObject.GetClientDBConnString(pEntOTP.ClientId);

                if (string.IsNullOrEmpty(pEntOTP.ID))
                {
                    pEntOTP.ID = YPLMS.Services.IDGenerator.GetStringGUIDWithPrefix(Schema.Common.VAL_OTP_PREFIX);
                }
                _sqlpara = new SqlParameter(Schema.OTP.PARA_OTP_ID, pEntOTP.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.OTP.PARA_SYSTEMUSERGUID, pEntOTP.SystemUserGuid);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.OTP.PARA_OTP_NUMBER, pEntOTP.OTPNumber);
                _sqlcmd.Parameters.Add(_sqlpara);

                int iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntOTP;
        }
        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntID"></param>
        /// <returns></returns>
        public OTP Get(OTP pEntOTP)
        {
            _sqlObject = new SQLObject();
            OTP entOTP = null;
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntOTP.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.OTP.PROC_GET_OTP, sqlConnection);

                _sqlpara = new SqlParameter(Schema.OTP.PARA_OTP_NUMBER, pEntOTP.OTPNumber);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entOTP = FillObjectForGetAll(_sqlreader, _sqlObject);

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
            return entOTP;
        }

        /// <summary>
        /// Get OTP Number
        /// </summary>
        /// <param name="pEntID"></param>
        /// <returns></returns>
        public OTP GetOTPNumber(OTP pEntOTP)
        {
            _sqlObject = new SQLObject();
            OTP entOTP = null;
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntOTP.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.OTP.PROC_GET_OTP_NUMBER, sqlConnection);

                _sqlpara = new SqlParameter(Schema.OTP.PARA_SYSTEMUSERGUID, pEntOTP.SystemUserGuid);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entOTP = FillObjectForGetAll(_sqlreader, _sqlObject);

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
            return entOTP;
        }
        //
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntID"></param>
        /// <returns></returns>
        public OTP CheckExpireOTP(OTP pEntOTP)
        {
            _sqlObject = new SQLObject();
            OTP entOTP = null;
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntOTP.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.OTP.PROC_CHECK_OTP_EXPIRE, sqlConnection);

                _sqlpara = new SqlParameter(Schema.OTP.PARA_SYSTEMUSERGUID, pEntOTP.SystemUserGuid);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entOTP = FillObjectForGetAll(_sqlreader, _sqlObject);
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
            return entOTP;
        }

        /// <summary>
        /// Update OTP
        /// </summary>
        /// <param name="pOTP"></param>
        /// <returns></returns>
        public OTP Update(OTP pEntOTP)
        {
            return pEntOTP;
        }

        #endregion
    }
}
