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
    public class ClientFeatureDAM : IDataManager<ClientFeature>,IClientFeatureDAM<ClientFeature>
    {
        #region Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        ClientFeature _entClientFeature = null;
        List<ClientFeature> _entListClientFeature = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.Client.CLIENT_ANN_UPDATE_ERROR;
        #endregion


        //public ClientFeatureDAM()
        //{
        //}

        /// <summary>
        /// To get ClientFeature details by ID.
        /// </summary>
        /// <param name="pEntClientFeature"></param>
        /// <returns>ClientFeature</returns>
        public ClientFeature GetClientFeatureByID(ClientFeature pEntClientFeature)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.ClientFeature.PROC_GET_CLIENTFEATURE;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntClientFeature.ClientId))
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_CLIENTID, pEntClientFeature.ClientId);
            else
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_CLIENTID, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            if (!String.IsNullOrEmpty(pEntClientFeature.ClientFeatureID))
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_CLIENTFEATUREID, pEntClientFeature.ClientFeatureID);
            else
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_CLIENTFEATUREID, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetMasterDBConnString();
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entClientFeature = FillObject(_sqlreader, false, _sqlObject);
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
            return _entClientFeature;
        }

        /// <summary>
        /// ClientFeature Range
        /// </summary>
        /// <param name="pEntClientFeature"></param>
        /// <returns></returns>
        public List<ClientFeature> GetAllClientFeature(ClientFeature pEntClientFeature)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.ClientFeature.PROC_GET_ALL_CLIENTFEATURE;
            _entListClientFeature = new List<ClientFeature>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetMasterDBConnString();
                sqlConnection = new SqlConnection(_strConnString);
                if (!String.IsNullOrEmpty(pEntClientFeature.ClientId))
                    _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_CLIENTID, pEntClientFeature.ClientId.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_CLIENTID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntClientFeature.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntClientFeature.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntClientFeature.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntClientFeature.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entClientFeature = FillObject(_sqlreader, true, _sqlObject);
                    _entListClientFeature.Add(_entClientFeature);
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
            return _entListClientFeature;
        }

        /// <summary>
        /// Fill Object
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>ClientFeature</returns>
        public ClientFeature FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entClientFeature = new ClientFeature();
            int index;
            if (pSqlReader.HasRows)
            {
                index = pSqlReader.GetOrdinal(Schema.ClientFeature.COL_FEATUREID);
                if (!pSqlReader.IsDBNull(index))
                    _entClientFeature.ID = pSqlReader.GetString(index);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ClientFeature.COL_ISACTIVE))
                {
                    index = pSqlReader.GetOrdinal(Schema.ClientFeature.COL_ISACTIVE);
                    if (!pSqlReader.IsDBNull(index))
                        _entClientFeature.IsActive = pSqlReader.GetString(index) == "1" ? true : false;
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ClientFeature.COL_CLIENTID))
                {
                    index = pSqlReader.GetOrdinal(Schema.ClientFeature.COL_CLIENTID);
                    if (!pSqlReader.IsDBNull(index))
                        _entClientFeature.ClientId = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ClientFeature.COL_CLIENTFEATUREID))
                {
                    index = pSqlReader.GetOrdinal(Schema.ClientFeature.COL_CLIENTFEATUREID);
                    if (!pSqlReader.IsDBNull(index))
                        _entClientFeature.ClientFeatureID = pSqlReader.GetString(index);
                }

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(index))
                        _entRange.TotalRows = pSqlReader.GetInt32(index);
                    _entClientFeature.ListRange = _entRange;
                    return _entClientFeature;
                }
            }
            return _entClientFeature;
        }

        public DataSet IsYPTab_Iperform_Key_Exist(ClientFeature pEntClient)
        {
            DataSet dsReturn = null;
            _sqlObject = new SQLObject();

            SqlConnection sqlclientConnection = new SqlConnection(_sqlObject.GetClientDBConnString(pEntClient.ClientId));
            _sqlcmd = new SqlCommand(Schema.ClientFeature.PROC_ISYPTAB_IPerform_Key_Exist, sqlclientConnection);

            if (!String.IsNullOrEmpty(pEntClient.IperFormRegKey))
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_IperFormRegKey, pEntClient.IperFormRegKey);
            else
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_IperFormRegKey, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntClient.YPTabRegKey))
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_YPTabRegKey, pEntClient.YPTabRegKey);
            else
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_YPTabRegKey, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                dsReturn = SQLHelper.SqlDataAdapter(_sqlcmd, sqlclientConnection.ConnectionString);
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
                if (sqlclientConnection != null && sqlclientConnection.State != ConnectionState.Closed)
                    sqlclientConnection.Close();
            }
            return dsReturn;
        }
        //
        public DataSet IS_Exist_YPTAB_RegKey(ClientFeature pEntClient)
        {
            DataSet dsReturn = null;
            _sqlObject = new SQLObject();

            SqlConnection sqlclientConnection = new SqlConnection(_sqlObject.GetClientDBConnString(pEntClient.ClientId));
            _sqlcmd = new SqlCommand(Schema.ClientFeature.PROC_ISYPTAB_Key_Exist, sqlclientConnection);

            if (!String.IsNullOrEmpty(pEntClient.YPTabRegKey))
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_YPTabRegKey, pEntClient.YPTabRegKey);
            else
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_YPTabRegKey, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                dsReturn = SQLHelper.SqlDataAdapter(_sqlcmd, sqlclientConnection.ConnectionString);
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
                if (sqlclientConnection != null && sqlclientConnection.State != ConnectionState.Closed)
                    sqlclientConnection.Close();
            }
            return dsReturn;
        }
        //
        public DataSet IS_Exist_IperformRegKey(ClientFeature pEntClient)
        {
            DataSet dsReturn = null;
            _sqlObject = new SQLObject();

            SqlConnection sqlclientConnection = new SqlConnection(_sqlObject.GetClientDBConnString(pEntClient.ClientId));
            _sqlcmd = new SqlCommand(Schema.ClientFeature.PROC_IPerform_Key_Exist, sqlclientConnection);

            if (!String.IsNullOrEmpty(pEntClient.IperFormRegKey))
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_IperFormRegKey, pEntClient.IperFormRegKey);
            else
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_IperFormRegKey, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                dsReturn = SQLHelper.SqlDataAdapter(_sqlcmd, sqlclientConnection.ConnectionString);
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
                if (sqlclientConnection != null && sqlclientConnection.State != ConnectionState.Closed)
                    sqlclientConnection.Close();
            }
            return dsReturn;
        }
        public DataSet GetYPTab_Iperform_Key(ClientFeature pEntClient)
        {
            DataSet dsReturn = null;
            _sqlObject = new SQLObject();

            SqlConnection sqlclientConnection = new SqlConnection(_sqlObject.GetClientDBConnString(pEntClient.ClientId));
            _sqlcmd = new SqlCommand(Schema.ClientFeature.PROC_GETYPTAB_IPerform_Key, sqlclientConnection);

            if (!String.IsNullOrEmpty(pEntClient.ClientId))
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_CLIENTID, pEntClient.ID);
            else
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_CLIENTID, null);
            _sqlcmd.Parameters.Add(_sqlpara);



            try
            {
                dsReturn = SQLHelper.SqlDataAdapter(_sqlcmd, sqlclientConnection.ConnectionString);
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
                if (sqlclientConnection != null && sqlclientConnection.State != ConnectionState.Closed)
                    sqlclientConnection.Close();
            }
            return dsReturn;
        }

        /// <summary>
        /// To add/update a ClientFeature.
        /// </summary>
        /// <param name="pEntClientFeature"></param>
        /// <returns>ClientFeature</returns>
        public ClientFeature UpdateClientFeature(ClientFeature pEntClientFeature, ClientFeature.Method pMethod)
        {
            int iRowsAffected = 0;
            SqlConnection sqlConn = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.ClientFeature.PROC_UPDATE_CLIENTFEATURE;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntClientFeature.ID))
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_FEATUREID, pEntClientFeature.ID);
            else
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_FEATUREID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            ////if (!String.IsNullOrEmpty(pEntClientFeature.FeatureName))
            ////    _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_FEATURENAME, pEntClientFeature.FeatureName);
            ////else
            ////    _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_FEATURENAME, null);
            ////_sqlcmd.Parameters.Add(_sqlpara);

            ////if (!String.IsNullOrEmpty(Convert.ToString(pEntClientFeature.IsAssessmentEnabled)))
            ////    _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_ISACTIVE, pEntClientFeature.IsAssessmentEnabled);
            ////else
            ////    _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_ISACTIVE, null);
            ////_sqlcmd.Parameters.Add(_sqlpara);

            ////if (!String.IsNullOrEmpty(Convert.ToString(pEntClientFeature.IsReportAdmin)))
            ////    _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_IS_REPORTADMIN, pEntClientFeature.IsReportAdmin);
            ////else
            ////    _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_IS_REPORTADMIN, null);
            ////_sqlcmd.Parameters.Add(_sqlpara);


            ////if (!String.IsNullOrEmpty(Convert.ToString(pEntClientFeature.IsProgramAdmin)))
            ////    _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_IS_PROGRAMADMIN, pEntClientFeature.IsProgramAdmin);
            ////else
            ////    _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_IS_PROGRAMADMIN, null);
            ////_sqlcmd.Parameters.Add(_sqlpara);


            if (!String.IsNullOrEmpty(pEntClientFeature.ClientId))
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_CLIENTID, pEntClientFeature.ClientId);
            else
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_CLIENTID, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            if (!String.IsNullOrEmpty(pEntClientFeature.ClientFeatureID))
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_CLIENTFEATUREID, pEntClientFeature.ClientFeatureID);
            else
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_CLIENTFEATUREID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(Convert.ToString(pEntClientFeature.IsActive)))
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_ISACTIVE, pEntClientFeature.IsActive);
            else
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_ISACTIVE, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            if (!String.IsNullOrEmpty(Convert.ToString(pEntClientFeature.SystemUserGuid)))
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_SYSTEMUSERGUID, pEntClientFeature.SystemUserGuid);
            else
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_SYSTEMUSERGUID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(Convert.ToString(pEntClientFeature.PubPortalCustomFields)))
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_PUPPORTALCUSTOMFILDS, pEntClientFeature.PubPortalCustomFields);
            else
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_PUPPORTALCUSTOMFILDS, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(Convert.ToString(pEntClientFeature.IperFormRegKey)))
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_IperFormRegKey, pEntClientFeature.IperFormRegKey);
            else
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_IperFormRegKey, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(Convert.ToString(pEntClientFeature.IPerformDBName)))
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_IPerformDBName, pEntClientFeature.IPerformDBName);
            else
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_IPerformDBName, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(Convert.ToString(pEntClientFeature.YPTabRegKey)))
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_YPTabRegKey, pEntClientFeature.YPTabRegKey);
            else
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_YPTabRegKey, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                if (pMethod == ClientFeature.Method.Update)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else if (pMethod == ClientFeature.Method.Add)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _strConnString = _sqlObject.GetMasterDBConnString();
                sqlConn = new SqlConnection(_strConnString);
                sqlConn.Open();
                _sqlcmd.Connection = sqlConn;
                //_sqlcmd.CommandTimeout = 200;
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
            return pEntClientFeature;
        }
        /// <summary>
        /// Delete ClientFeature by ID
        /// </summary>
        /// <param name="pEntClientFeature">ClientFeature with ID</param>
        /// <returns>Deleted ClientFeature with only ID</returns>
        public ClientFeature DeleteClientFeature(ClientFeature pEntClientFeature)
        {
            ClientFeature entClientFeature = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.ClientFeature.PROC_DELETE_CLIENTFEATURE;
            _sqlObject = new SQLObject();
            int i = 0;
            if (!String.IsNullOrEmpty(pEntClientFeature.ID))
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_FEATUREID, pEntClientFeature.ID);
            else
                _sqlpara = new SqlParameter(Schema.ClientFeature.PARA_FEATUREID, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetMasterDBConnString();
                i = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                if (i < 0)
                {
                    entClientFeature = new ClientFeature();
                    entClientFeature.ID = pEntClientFeature.ID;
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entClientFeature;
        }

        #region Interface Methods
        /// <summary>
        /// Get ClientFeature By ID
        /// </summary>
        /// <param name="pEntClientFeature"></param>
        /// <returns></returns>
        public ClientFeature Get(ClientFeature pEntClientFeature)
        {
            return GetClientFeatureByID(pEntClientFeature);
        }
        /// <summary>
        /// List of All ClientFeature
        /// </summary>
        /// <param name="pEntClientFeature"></param>
        /// <returns></returns>
        public List<ClientFeature> GetAll(ClientFeature pEntClientFeature)
        {
            return GetAllClientFeature(pEntClientFeature);
        }
        /// <summary>
        /// Add ClientFeature
        /// </summary>
        /// <param name="pEntClientFeature"></param>
        /// <returns></returns>
        public ClientFeature Add(ClientFeature pEntClientFeature)
        {
            return UpdateClientFeature(pEntClientFeature, ClientFeature.Method.Add);
        }
        /// <summary>
        /// Update ClientFeature
        /// </summary>
        /// <param name="pEntClientFeature"></param>
        /// <returns></returns>
        public ClientFeature Update(ClientFeature pEntClientFeature)
        {
            return UpdateClientFeature(pEntClientFeature, ClientFeature.Method.Update);
        }
        /// <summary>
        /// Delete ClientFeature
        /// </summary>
        /// <param name="pEntClientFeature"></param>
        /// <returns>ClientFeature</returns>
        public ClientFeature Delete(ClientFeature pEntClientFeature)
        {
            return DeleteClientFeature(pEntClientFeature);
        }
        #endregion
    }
}
