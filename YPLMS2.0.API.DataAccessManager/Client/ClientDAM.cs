using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;
using YPLMS2._0.API.DataAccessManager;


namespace YPLMS2._0.API.DataAccessManager
{
    /// <summary>
    /// class ClientDAM
    /// </summary>
    public class ClientDAM : IDataManager<Client>, IClientDAM<Client>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlParameter _sqlpara = null;
        SqlCommand _sqlcmd = null;
        EntityRange _entRange = null;
        string _strMessageId = YPLMS.Services.Messages.Client.CLIENT_DL_ERROR;
        string _strConnString = string.Empty;
        SQLObject _sqlObject = null;
        SqlConnection sqlConnection = null;
        #endregion

        public void UpdateSystemMaintenanceConfiguration(string clientId, bool enableMaintenanceMessage, DateTime? startDate, DateTime? endDate, string memberId)
        {
            SQLObject sqlObject = new SQLObject();

            using (SqlConnection connection = new SqlConnection(sqlObject.GetMasterDBConnString()))
            {
                connection.Open();
                using (var sqlCommand = new SqlCommand("sproc_Client_UpdateSystemMaintenanceMessage", connection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@ClientID", clientId));
                    sqlCommand.Parameters.Add(new SqlParameter("@EnableSystemMaintMessage", enableMaintenanceMessage));

                    if (startDate.HasValue)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("@MaintenanceStart", startDate.Value));
                    }

                    if (endDate.HasValue)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("@MaintenanceEnd", endDate.Value));
                    }

                    sqlCommand.Parameters.Add(new SqlParameter("@ModifiedBy", memberId));

                    try
                    {
                        sqlObject.ExecuteNonQuery(sqlCommand);
                    }
                    catch (Exception expCommon)
                    {
                        throw new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                    }
                }
            }
        }

        /// <summary>
        /// Get client details by ID
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns>Returns Client object</returns>
        public Client GetClientByID(Client pEntClient)
        {
            Client entClient = null;
            _sqlObject = new SQLObject();
            if (string.IsNullOrEmpty(pEntClient.ID))
            {
                return null;
            }
            SqlConnection sqlMasterConnection = new SqlConnection(_sqlObject.GetMasterDBConnString());
            _sqlcmd = new SqlCommand(Schema.Client.PROC_GET_CLIENT_MASTER_ID, sqlMasterConnection);

            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlcmd.CommandTimeout = 0;
                _sqlreader = _sqlObject.ExecuteMasterReader(_sqlcmd, false);
                entClient = FillObject(_sqlreader, _sqlObject);
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
                if (sqlMasterConnection != null && sqlMasterConnection.State != ConnectionState.Closed)
                    sqlMasterConnection.Close();

            }
            return entClient;
        }

        /// <summary>
        /// Get client ID By URL
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns>Returns Client object</returns>
        public Client GetClientIdByURL(Client pEntClient)
        {
            Client entClient = new Client();
            Object obj = null;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _sqlcmd.CommandText = Schema.Client.PROC_GET_CLIENT_MASTER_ID_BY_URL;
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_ACCESS_URL, pEntClient.ClientAccessURL);
                _sqlcmd.Parameters.Add(_sqlpara);
                obj = _sqlObject.ExecuteMasterScalar(_sqlcmd);
                if (obj != null)
                    entClient.ID = Convert.ToString(obj);

                else
                    entClient = null;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entClient;
        }


        /// <summary>
        /// Get client ID By URL
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns>Returns Client object</returns>
        public Client GetClientDetaildFromCourseId(string CourseId)
        {
            Client entClient = new Client();

            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_GetClientDetailsFromCourseIdForBenecke;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(CourseId))
                _sqlpara = new SqlParameter(Schema.ContentModule.PARA_CONTENT_MODULE_ID, CourseId);
            _sqlcmd.Parameters.Add(_sqlpara);



            try
            {
                _strConnString = _sqlObject.GetMasterDBConnString();
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                //  _entProducts = FillObject(_sqlreader, false, _sqlObject);

                int index;
                if (_sqlreader.HasRows)
                {
                    index = _sqlreader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
                    if (!_sqlreader.IsDBNull(index))
                        entClient.ID = _sqlreader.GetString(index);

                    if (_sqlObject.ReaderHasColumn(_sqlreader, Schema.Client.COL_ACCESS_URL))
                    {
                        index = _sqlreader.GetOrdinal(Schema.Client.COL_ACCESS_URL);
                        if (!_sqlreader.IsDBNull(index))
                            entClient.ClientAccessURL = _sqlreader.GetString(index);
                    }
                    if (_sqlObject.ReaderHasColumn(_sqlreader, Schema.Client.COL_ACCESS_COURSE_PLAYER_URL))
                    {
                        index = _sqlreader.GetOrdinal(Schema.Client.COL_ACCESS_COURSE_PLAYER_URL);
                        if (!_sqlreader.IsDBNull(index))
                        {
                            Cluster entCluster = new Cluster();
                            entCluster.CoursePlayerURL = _sqlreader.GetString(index);
                            entClient.ClientCluster = entCluster;
                        }
                    }

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
            return entClient; ;
        }

        public string GetClientAccessURL(Client pEntClient)
        {
            string AccessURL = string.Empty;
            Object obj = null;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _sqlcmd.CommandText = Schema.Client.PROC_GET_CLIENT_ACCESS_URL;
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetClientDBConnString(pEntClient.ID);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;

                obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
                if (obj != null)
                    AccessURL = Convert.ToString(obj);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return AccessURL;
        }


        /// <summary>
        /// Get client FeedBackEmail and Announcements
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns>Returns Client object</returns>
        public Client GetFeedBackEmail(Client pEntClient)
        {
            Client entClient = new Client();
            _sqlcmd = new SqlCommand();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(pEntClient.ClientId);
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = _strConnString;
            _sqlcmd = new SqlCommand(Schema.Client.PROC_GET_CLIENT_MASTER_FEED_BACK_EMAIL, sqlConnection);
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    entClient = FillFeedBack_Announcements(_sqlreader);
                }
                else
                {
                    entClient = null;
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
            return entClient;
        }

        /// <summary>
        /// Get client HTTPS Alive
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns>Returns Client object</returns>
        public Client GetHTTPSAllow(Client pEntClient)
        {
            Client entClient = new Client();
            _sqlcmd = new SqlCommand();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(pEntClient.ClientId);
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = _strConnString;
            _sqlcmd = new SqlCommand(Schema.Client.PROC_GET_HTTPSALLOWED, sqlConnection);
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    entClient = FillHTTPSALLOWED(_sqlreader);
                }
                else
                {
                    entClient = null;
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
            return entClient;
        }

        /// <summary>
        /// Get client AllowUser
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns>Returns Client object</returns>
        public Client GetAllowUser(Client pEntClient)
        {
            Client entClient = new Client();
            Object obj = null;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _sqlcmd.CommandText = Schema.Client.PROC_GET_CLIENT_MASTER_ALLOW_USER;
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);
                obj = _sqlObject.ExecuteMasterScalar(_sqlcmd);
                if (obj != null)
                    entClient.AllowUserProfileEdit = Convert.ToBoolean(obj);
                else
                    entClient = null;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entClient;
        }

        /// <summary>
        /// Check Client Exists By URL
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns>Returns Client object</returns>
        public Client CheckClientByURL(Client pEntClient)
        {
            Client entClient = new Client();
            Object obj = null;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _sqlcmd.CommandText = Schema.Client.PROC_GET_CLIENT_MASTER_CLIENT_EXISTS_BY_URL;
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_ACCESS_URL, pEntClient.ClientAccessURL);
                _sqlcmd.Parameters.Add(_sqlpara);
                obj = _sqlObject.ExecuteMasterScalar(_sqlcmd);
                if (obj != null)
                    entClient.ID = Convert.ToString(obj);
                else
                    entClient = null;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entClient;
        }

        /// <summary>
        /// Check Client exist with same name
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns>Returns Client object</returns>
        public Client CheckClientIdByName(Client pEntClient)
        {
            Client entClient = new Client();
            Object obj = null;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _sqlcmd.CommandText = Schema.Client.PROC_GET_CLIENT_MASTER_ID_BY_NAME;
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_NAME, pEntClient.ClientName);
                _sqlcmd.Parameters.Add(_sqlpara);
                obj = _sqlObject.ExecuteMasterScalar(_sqlcmd);
                if (obj != null)
                    entClient.ID = Convert.ToString(obj);
                else
                    entClient = null;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entClient;
        }




        /// <summary>
        /// Find Clients
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<Client> FindClients(Search pEntSearch)
        {
            Client entClient = null;
            List<Client> entListClient = new List<Client>();
            _sqlObject = new SQLObject();
            SqlConnection sqlMasterConnection = new SqlConnection(_sqlObject.GetMasterDBConnString());
            _sqlcmd = new SqlCommand(Schema.Client.PROC_SEARCH_CLIENTS, sqlMasterConnection);

            if (!String.IsNullOrEmpty(pEntSearch.KeyWord))
                _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntSearch.KeyWord);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntSearch.ListRange != null)
            {
                if (pEntSearch.ListRange.PageIndex > 0)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntSearch.ListRange.PageIndex);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntSearch.ListRange.PageSize > 0)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntSearch.ListRange.PageSize);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntSearch.ListRange.SortExpression))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            if (pEntSearch.SearchObject != null && pEntSearch.SearchObject.Count > 0)
            {
                entClient = new Client();
                entClient = (Client)pEntSearch.SearchObject[0];

                if (!string.IsNullOrEmpty(entClient.SAIConsultingServiceManager))
                {
                    _sqlpara = new SqlParameter(Schema.Client.PARA_SAI_SERVICE_MANAGER, entClient.SAIConsultingServiceManager);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntSearch.SearchObject.Count > 1)
                {
                    entClient = new Client();
                    entClient = (Client)pEntSearch.SearchObject[1];
                    _sqlpara = new SqlParameter(Schema.Client.PARA_IS_ACTIVE, entClient.IsActive);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
            }
            try
            {
                _sqlreader = _sqlObject.ExecuteMasterReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entClient = FillClientMaster(_sqlreader, true, _sqlObject);
                    entListClient.Add(entClient);
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
                if (sqlMasterConnection != null && sqlMasterConnection.State != ConnectionState.Closed)
                    sqlMasterConnection.Close();

            }
            return entListClient;
        }

        /// <summary>
        /// Fill reader of client master
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>Client Object</returns>
        private Client FillFeedBack_Announcements(SqlDataReader pSqlReader)
        {
            Client entClient = new Client();
            int iIndex;

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_FEED_BACK_RECEIVER_EMAIL_ID);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.FeedbackReceiverEmailId = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_ANNOUNCEMENTS_ENABLED);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.IsAnnouncementsEnabled = pSqlReader.GetBoolean(iIndex);

            return entClient;
        }


        /// <summary>
        /// Fill reader of client master
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>Client Object</returns>
        private Client FillHTTPSALLOWED(SqlDataReader pSqlReader)
        {
            Client entClient = new Client();
            int iIndex;

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_HTTPS_ALLOWED);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.IsHTTPSAllowed = pSqlReader.GetBoolean(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.ClientId = pSqlReader.GetString(iIndex);

            return entClient;
        }

        private void GetValueOrDefault<TProperty>(IDataReader reader, string columnName, Func<int, TProperty> getPropertyValue, Action<TProperty> setValue)
        {
            try
            {
                int index = reader.GetOrdinal(columnName);

                if (!reader.IsDBNull(index))
                {
                    TProperty prop = getPropertyValue(index);

                    setValue(prop);
                }
            }
            catch (IndexOutOfRangeException)
            {
            }
        }

        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>Client Object</returns>
        private Client FillObject(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            Client entClient = new Client();
            SystemConfiguration entSysConfig = new SystemConfiguration();
            Cluster entCluster = new Cluster();
            Layout entLayout = new Layout();
            Language entLanguage = new Language();
            Theme entDefaultTheme = new Theme();
            Theme entNewTheme = null;
            int iIndex = 0;

            if (pSqlReader.HasRows)
            {

                while (pSqlReader.Read())
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.ID = pSqlReader.GetString(iIndex);

                    GetValueOrDefault(pSqlReader, "IsMaintenanceMessageEnabled",
                                        i => pSqlReader.GetBoolean(i),
                                        v => entClient.IsSystemMaintMessageEnabled = v);

                    GetValueOrDefault(pSqlReader, "MaintenanceStartDate",
                    i => pSqlReader.GetDateTime(i),
                    v => entClient.SystemMaintenanceStart = v);

                    GetValueOrDefault(pSqlReader, "MaintenanceEndDate",
                    i => pSqlReader.GetDateTime(i),
                    v => entClient.SystemMaintenanceEnd = v);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CLIENT_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.ClientName = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_ACTIVE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.IsActive = pSqlReader.GetBoolean(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CLIENT_DESC);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.ClientDescription = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_ACCESS_URL);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.ClientAccessURL = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_DBIP_ADDRESS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.DBIPAddress = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_DB_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.DatabaseName = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_DB_UID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.DBUID = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_DB_PWD);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.DBPassword = pSqlReader.GetString(iIndex);
                    entClient.DBPassword = EncryptionManager.Decrypt(entClient.DBPassword);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_DEFAULT_LANG_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.DefaultLanguageId = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_DEFAULT_LAOUT_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.DefaultLayoutId = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_DEFAULT_THEME_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.DefaultThemeId = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CONT_SRVR_URL);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.ContentServerURL = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_NO_USER_LICS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.NumberOfUserLicenses = pSqlReader.GetInt32(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CONTRACT_START_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.ContractStartDate = pSqlReader.GetDateTime(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CONTRACT_END_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.ContractEndDate = pSqlReader.GetDateTime(iIndex);

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_ISCERTIFCATIONENABLED))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_ISCERTIFCATIONENABLED);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entClient.IsCertifcationEnabled = pSqlReader.GetBoolean(iIndex);
                    }
                    //Added by ISHTTPS_Allow
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_IS_HTTPS_ALLOWED))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_HTTPS_ALLOWED);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entClient.IsHTTPSAllowed = pSqlReader.GetBoolean(iIndex);
                    }
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_IS_CONTRACT_EXPIRED))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_CONTRACT_EXPIRED);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entClient.IsClientContractExpired = pSqlReader.GetBoolean(iIndex);
                    }

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_IS_CONTRACT_STARTED))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_CONTRACT_STARTED);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entClient.IsClientContractStarted = pSqlReader.GetBoolean(iIndex);
                    }

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SMTP_SERVER_IP);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.SMTPServerIP = pSqlReader.GetString(iIndex);

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_SMTP_PORT))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SMTP_PORT);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entClient.SMTPPORT = pSqlReader.GetInt32(iIndex);
                        else
                            entClient.SMTPPORT = 0;
                    }
                    else
                        entClient.SMTPPORT = 0;
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_SMTP_ENABLESSL))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SMTP_ENABLESSL);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entClient.SMTPEnableSSL = pSqlReader.GetBoolean(iIndex);
                    }
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_SECURITY_PROTOCOL))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SECURITY_PROTOCOL);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entClient.SecurityProtocol = pSqlReader.GetString(iIndex);
                    }
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_IS_SECURED))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_SECURED);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entClient.IsSecured = pSqlReader.GetBoolean(iIndex);
                    }

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SMTP_USER_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.SMTPUserName = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SMTP_PWD);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.SMTPPassword = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SITE_SUB_DOMAIN_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.SiteSubDomainName = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SAI_SERVICE_MANAGER);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.SAIConsultingServiceManager = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SESSION_TIME_OUT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.SessionTimeOut = pSqlReader.GetInt32(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_SELF_REGISTRATION);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.IsSelfRegistration = pSqlReader.GetBoolean(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_PASSCODE_BASED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.IsPassCodeBased = pSqlReader.GetBoolean(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_EMAIL_DOMAIN_NASED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.IsEmailDomainBased = pSqlReader.GetBoolean(iIndex);


                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_SSO_CONFIGURED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.IsSSOConfigured = pSqlReader.GetBoolean(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SSO_TYPE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.SSOType = (Client.SingleSignOnType)Enum.Parse(typeof(Client.SingleSignOnType), Convert.ToString(pSqlReader.GetString(iIndex)));

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_LOGOUT_URL);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.LogoutRedirectionURL = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_LOCKED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.IsLocked = pSqlReader.GetBoolean(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_FORGOT_PWD_ENABLED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.IsForgotPasswordEnabled = pSqlReader.GetBoolean(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_CONTACT_US_ENABLED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.IsContactUsEnabled = pSqlReader.GetBoolean(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_FEEDBACK_ENABLED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.IsFeedbackEnabled = pSqlReader.GetBoolean(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_RSS_ENABLED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.IsRSSEnabled = pSqlReader.GetBoolean(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_ANNOUNCEMENTS_ENABLED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.IsAnnouncementsEnabled = pSqlReader.GetBoolean(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_MAX_FILEUPLOAD_SIZEMB);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.MaxFileUploadSizeMB = pSqlReader.GetInt32(iIndex);

                    //-- new col added
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_ALLOW_USER_PROFILE_EDIT))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_ALLOW_USER_PROFILE_EDIT);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entClient.AllowUserProfileEdit = pSqlReader.GetBoolean(iIndex);
                    }

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_DEFAULT_PAGE_SIZE))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_DEFAULT_PAGE_SIZE);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entClient.PageSize = pSqlReader.GetInt32(iIndex);
                    }

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_FEED_BACK_RECEIVER_EMAIL_ID))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_FEED_BACK_RECEIVER_EMAIL_ID);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entClient.FeedbackReceiverEmailId = pSqlReader.GetString(iIndex);
                    }

                    //For Listenup
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_LU_CLIENTID))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_LU_CLIENTID);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entClient.LUClientId = pSqlReader.GetString(iIndex);
                    }
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_LU_CLIENT_NAME))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_LU_CLIENT_NAME);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entClient.LUClientName = pSqlReader.GetString(iIndex);
                    }

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_YPLS_DEFAULT_LAYOUT_ID))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_YPLS_DEFAULT_LAYOUT_ID);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entClient.YPLSLayoutId = pSqlReader.GetString(iIndex);
                    }

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_YPLS_DEFAULT_THEME_ID))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_YPLS_DEFAULT_THEME_ID);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entClient.YPLSThemeId = pSqlReader.GetString(iIndex);
                    }
                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_MAX_CONCURRENT_SESSIONS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.MaxConcurrentSessions = pSqlReader.GetInt32(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.CreatedById = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.DateCreated = pSqlReader.GetDateTime(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.LastModifiedById = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entClient.LastModifiedDate = pSqlReader.GetDateTime(iIndex);

                    // --------------- Code to fill the layout object data ---------------
                    entLayout.ID = Convert.ToString(pSqlReader[Schema.Layout.COL_LAYOUT_ID]);
                    entLayout.LayoutName = Convert.ToString(pSqlReader[Schema.Layout.COL_LAYOUT_NAME]);
                    entLayout.MasterPageURL = Convert.ToString(pSqlReader[Schema.Layout.COL_MST_PAGE_URL]);
                    entLayout.MasterPageURLRTL = Convert.ToString(pSqlReader[Schema.Layout.COL_MST_PAGE_URL_RTL]);

                    if (entLayout != null)
                        entClient.Layout = entLayout;

                    // Code to fill the language object data.
                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_DEFAULT_LANG_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entLanguage.ID = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_LANG_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entLanguage.LanguageName = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_LANG_ENG_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entLanguage.LanguageEnglishName = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_TEXT_DIRECTION);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entLanguage.TextDirection = pSqlReader.GetString(iIndex);

                    if (entLanguage != null)
                        entClient.Language = entLanguage;

                    // --------------- Code to fill the default Theme object data ---------------
                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_DEFAULT_THEME_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entDefaultTheme.ID = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Theme.COL_THEME_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entDefaultTheme.ThemeName = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Theme.COL_BASE_URL);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entDefaultTheme.ThemeBaseURL = pSqlReader.GetString(iIndex);

                    if (entDefaultTheme != null)
                        entClient.Theme = entDefaultTheme;

                    // --------------- Code to fill the system configuration object data ---------------
                    entSysConfig.ClientId = Convert.ToString(pSqlReader[Schema.Client.COL_CLIENT_ID]);
                    entSysConfig.ID = Convert.ToString(pSqlReader[Schema.Configuration.COL_CONFIG_ID]);

                    iIndex = pSqlReader.GetOrdinal(Schema.Configuration.COL_CONFIG_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entSysConfig.ID = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Configuration.COL_CONFIG_XML);

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Configuration.COL_ALLOWED_DOMAIN_LIST))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Configuration.COL_ALLOWED_DOMAIN_LIST);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entSysConfig.AllowedDomainsList = pSqlReader.GetString(iIndex);
                    }
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Configuration.COL_NONRESTRICTED_DOMAIN_LIST))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Configuration.COL_NONRESTRICTED_DOMAIN_LIST);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entSysConfig.NonRestrictedDomainList = pSqlReader.GetString(iIndex);
                    }

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Configuration.COL_DEFAULT_SITE_LOGO))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Configuration.COL_DEFAULT_SITE_LOGO);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entSysConfig.DefaultSiteLogo = pSqlReader.GetString(iIndex);
                    }

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Configuration.COL_DOCLICENSEPATH))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Configuration.COL_DOCLICENSEPATH);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entSysConfig.DocLicensePath = pSqlReader.GetString(iIndex);
                    }
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Configuration.COL_DOCLICENSEPATH))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Configuration.COL_DOCLICENSEPATH);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entSysConfig.DocLicensePath = pSqlReader.GetString(iIndex);
                    }

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Configuration.COL_RECIPIENTEMAILID))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Configuration.COL_RECIPIENTEMAILID);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entSysConfig.RecipientEmailId = pSqlReader.GetString(iIndex);
                    }
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Configuration.COL_GWTCADATABASENAME))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Configuration.COL_GWTCADATABASENAME);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entSysConfig.GWTCADatabaseName = pSqlReader.GetString(iIndex);
                    }
                    if (entSysConfig != null)
                        entClient.SysConfiguration = entSysConfig;

                    // --------------- Code to fill the Cluster object data ---------------
                    entCluster.ClientId = Convert.ToString(pSqlReader[Schema.Client.COL_CLIENT_ID]);
                    entCluster.ID = Convert.ToString(pSqlReader[Schema.Cluster.COL_CLUSTER_ID]);
                    entCluster.ClusterName = Convert.ToString(pSqlReader[Schema.Cluster.COL_CLUSTER_NAME]);
                    entCluster.ClusterIP = Convert.ToString(pSqlReader[Schema.Cluster.COL_CLUSTER_IP]);
                    entCluster.ContentServerIP = Convert.ToString(pSqlReader[Schema.Cluster.COL_CONTENT_SERVER_IP]);
                    entCluster.DatabaseIP = Convert.ToString(pSqlReader[Schema.Cluster.COL_DATABASE_IP]);
                    entCluster.DNSServerIP = Convert.ToString(pSqlReader[Schema.Cluster.COL_CLUSTER_DNSSERVER_IP]);
                    entCluster.DNSServerPassword = Convert.ToString(pSqlReader[Schema.Cluster.COL_CLUSTER_DNSSERVER_PASSWORD]);
                    entCluster.DNSServerPassword = EncryptionManager.Decrypt(entCluster.DNSServerPassword);
                    entCluster.DNSServerUserName = Convert.ToString(pSqlReader[Schema.Cluster.COL_CLUSTER_DNSSERVER_USERNAME]);
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Cluster.COL_CONTENT_FOLDER_PATH))
                        entCluster.ContentFolderPath = Convert.ToString(pSqlReader[Schema.Cluster.COL_CONTENT_FOLDER_PATH]);
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Cluster.COL_CONTENT_FOLDER_URL))
                        entCluster.ContentFolderURL = Convert.ToString(pSqlReader[Schema.Cluster.COL_CONTENT_FOLDER_URL]);
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Cluster.COL_FTP_UPLOAD_PATH))
                        entCluster.FTPUploadPath = Convert.ToString(pSqlReader[Schema.Cluster.COL_FTP_UPLOAD_PATH]);

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Cluster.COL_FTP_USER_NAME))
                        entCluster.FTPUserName = Convert.ToString(pSqlReader[Schema.Cluster.COL_FTP_USER_NAME]);

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Cluster.COL_FTP_PASSWORD))
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(pSqlReader[Schema.Cluster.COL_FTP_PASSWORD])))
                            entCluster.FTPPassword = EncryptionManager.Decrypt(Convert.ToString(pSqlReader[Schema.Cluster.COL_FTP_PASSWORD]));
                    }

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Cluster.COL_COURSE_PLAYERURL))
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(pSqlReader[Schema.Cluster.COL_COURSE_PLAYERURL])))
                            entCluster.CoursePlayerURL = Convert.ToString(pSqlReader[Schema.Cluster.COL_COURSE_PLAYERURL]);
                    }
                    //For ListenUp
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Cluster.COL_LU_WEBSERVICEURL))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_LU_WEBSERVICEURL);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entCluster.LUWebServiceURL = pSqlReader.GetString(iIndex);
                    }
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Cluster.COL_LU_APPLICATIONLAUNCH_URL))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_LU_APPLICATIONLAUNCH_URL);
                        if (!pSqlReader.IsDBNull(iIndex))
                            entCluster.LUApplicationLaunchURL = pSqlReader.GetString(iIndex);
                    }

                    if (entCluster != null)
                        entClient.ClientCluster = entCluster;
                }
                //  --------------- Fill Theme list for current client layout ---------------
                pSqlReader.NextResult();
                while (pSqlReader.Read())
                {
                    entNewTheme = new Theme();
                    entNewTheme.ID = Convert.ToString(pSqlReader[Schema.Theme.COL_THEME_ID]);
                    entNewTheme.ThemeName = Convert.ToString(pSqlReader[Schema.Theme.COL_THEME_NAME]);
                    entNewTheme.LayoutID = Convert.ToString(pSqlReader[Schema.Layout.COL_LAYOUT_ID]);
                    entNewTheme.ThemeBaseURL = Convert.ToString(pSqlReader[Schema.Theme.COL_BASE_URL]);
                    if (entNewTheme != null)
                        entClient.Layout.Themes.Add(entNewTheme);
                }

                pSqlReader.NextResult();
                /*
                 Date:03-Mar-2010
                 Change: OrgTree and ThemeLanguages code excluded, Respective prcedure is also updated to exclude the below resultset.
                 By: Shailesh+fatte+ashish
                 */
                //  --------------- Fill organization tree for current client ---------------
                //entOrgTree = new XmlDocument();
                //string strOrgXMLValue = string.Empty;
                //while (pSqlReader.Read())
                //{
                //    iIndex = 0;
                //    if (!pSqlReader.IsDBNull(iIndex))
                //    {
                //        strOrgXMLValue += pSqlReader.GetString(iIndex);
                //    }
                //}
                //strOrgXMLValue = "<Root>" + strOrgXMLValue + "</Root>";
                //entOrgTree.LoadXml(strOrgXMLValue);
                //entClient.OrganizationTreeXML = entOrgTree;

                //pSqlReader.NextResult();
                //  --------------- Fill Theme languages for current client languages ---------------

                ThemeLanguages _entThemeLanguages;
                while (pSqlReader.Read())
                {
                    _entThemeLanguages = new ThemeLanguages();
                    _entThemeLanguages.ThemeId = Convert.ToString(pSqlReader[Schema.Theme.COL_THEME_ID]);
                    _entThemeLanguages.LanguageId = Convert.ToString(pSqlReader[Schema.Language.COL_LANGUAGE_ID]);
                    _entThemeLanguages.CSSFileName1 = Convert.ToString(pSqlReader[Schema.ThemeLanguages.COL_CSS_FILENAME_1]);
                    _entThemeLanguages.CSSFileName2 = Convert.ToString(pSqlReader[Schema.ThemeLanguages.COL_CSS_FILENAME_2]);
                    _entThemeLanguages.CSSFileName3 = Convert.ToString(pSqlReader[Schema.ThemeLanguages.COL_CSS_FILENAME_3]);
                    _entThemeLanguages.CSSFileName4 = Convert.ToString(pSqlReader[Schema.ThemeLanguages.COL_CSS_FILENAME_4]);

                    _entThemeLanguages.CSSFileName5 = Convert.ToString(pSqlReader[Schema.ThemeLanguages.COL_CSS_FILENAME_5]);
                    _entThemeLanguages.CSSFileName6 = Convert.ToString(pSqlReader[Schema.ThemeLanguages.COL_CSS_FILENAME_6]);
                    _entThemeLanguages.CSSFileName7 = Convert.ToString(pSqlReader[Schema.ThemeLanguages.COL_CSS_FILENAME_7]);
                    _entThemeLanguages.CSSFileName8 = Convert.ToString(pSqlReader[Schema.ThemeLanguages.COL_CSS_FILENAME_8]);


                    if (_entThemeLanguages != null)
                        entClient.Theme.ThemeLanguages.Add(_entThemeLanguages);
                }
            }
            return entClient;
        }

        public Client FillPhoto(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            Client entClient = new Client();
            SystemConfiguration entSysConfig = new SystemConfiguration();
            int index;
            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_ISDISPLAY))
                {
                    index = pSqlReader.GetOrdinal(Schema.Client.COL_ISDISPLAY);
                    if (!pSqlReader.IsDBNull(index))
                        entSysConfig.IsDisplay = pSqlReader.GetBoolean(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_ISALLOWUPLOADPHOTO))
                {
                    index = pSqlReader.GetOrdinal(Schema.Client.COL_ISALLOWUPLOADPHOTO);
                    if (!pSqlReader.IsDBNull(index))
                        entSysConfig.IsAllowUploadPhoto = pSqlReader.GetBoolean(index);
                }

                entClient.SysConfiguration = entSysConfig;
            }
            return entClient;
        }

        /// <summary>
        /// Fill reader of client master
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>Client Object</returns>
        private Client FillClientMaster(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            Client entClient = new Client();
            int iIndex;

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.ID = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CLIENT_NAME);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.ClientName = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_ACTIVE);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.IsActive = pSqlReader.GetBoolean(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CLIENT_DESC);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.ClientDescription = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_ACCESS_URL);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.ClientAccessURL = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_DB_NAME);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.DatabaseName = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_DBIP_ADDRESS);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.DBIPAddress = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_DB_UID);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.DBUID = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_DB_PWD);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.DBPassword = pSqlReader.GetString(iIndex);
            //For Encryption
            entClient.DBPassword = EncryptionManager.Decrypt(entClient.DBPassword);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_DEFAULT_LANG_ID);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.DefaultLanguageId = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_DEFAULT_LAOUT_ID);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.DefaultLayoutId = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_DEFAULT_THEME_ID);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.DefaultThemeId = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CONT_SRVR_URL);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.ContentServerURL = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_NO_USER_LICS);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.NumberOfUserLicenses = pSqlReader.GetInt32(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CONTRACT_START_DATE);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.ContractStartDate = pSqlReader.GetDateTime(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CONTRACT_END_DATE);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.ContractEndDate = pSqlReader.GetDateTime(iIndex);

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_IS_CONTRACT_EXPIRED))
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_CONTRACT_EXPIRED);
                if (!pSqlReader.IsDBNull(iIndex))
                    entClient.IsClientContractExpired = pSqlReader.GetBoolean(iIndex);
            }

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_IS_CONTRACT_STARTED))
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_CONTRACT_STARTED);
                if (!pSqlReader.IsDBNull(iIndex))
                    entClient.IsClientContractStarted = pSqlReader.GetBoolean(iIndex);
            }

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SMTP_SERVER_IP);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.SMTPServerIP = pSqlReader.GetString(iIndex);

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_SMTP_PORT))
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SMTP_PORT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entClient.SMTPPORT = pSqlReader.GetInt32(iIndex);
                else
                    entClient.SMTPPORT = 0;
            }
            else
                entClient.SMTPPORT = 0;

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_SMTP_ENABLESSL))
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SMTP_ENABLESSL);
                if (!pSqlReader.IsDBNull(iIndex))
                    entClient.SMTPEnableSSL = pSqlReader.GetBoolean(iIndex);
            }
            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_SECURITY_PROTOCOL))
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SECURITY_PROTOCOL);
                if (!pSqlReader.IsDBNull(iIndex))
                    entClient.SecurityProtocol = pSqlReader.GetString(iIndex);
            }
            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_IS_SECURED))
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_SECURED);
                if (!pSqlReader.IsDBNull(iIndex))
                    entClient.IsSecured = pSqlReader.GetBoolean(iIndex);
            }

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SMTP_USER_NAME);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.SMTPUserName = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SMTP_PWD);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.SMTPPassword = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SITE_SUB_DOMAIN_NAME);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.SiteSubDomainName = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SAI_SERVICE_MANAGER);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.SAIConsultingServiceManager = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SESSION_TIME_OUT);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.SessionTimeOut = pSqlReader.GetInt32(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_SELF_REGISTRATION);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.IsSelfRegistration = pSqlReader.GetBoolean(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_PASSCODE_BASED);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.IsPassCodeBased = pSqlReader.GetBoolean(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_EMAIL_DOMAIN_NASED);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.IsEmailDomainBased = pSqlReader.GetBoolean(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_SSO_CONFIGURED);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.IsSSOConfigured = pSqlReader.GetBoolean(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SSO_TYPE_ID);
            if (!pSqlReader.IsDBNull(iIndex) && pSqlReader.GetString(iIndex).Length > 0)
                entClient.SSOType = (Client.SingleSignOnType)Enum.Parse(typeof(Client.SingleSignOnType), pSqlReader.GetString(iIndex));

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_LOGOUT_URL);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.LogoutRedirectionURL = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_LOCKED);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.IsLocked = pSqlReader.GetBoolean(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_FORGOT_PWD_ENABLED);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.IsForgotPasswordEnabled = pSqlReader.GetBoolean(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_CONTACT_US_ENABLED);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.IsContactUsEnabled = pSqlReader.GetBoolean(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_FEEDBACK_ENABLED);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.IsFeedbackEnabled = pSqlReader.GetBoolean(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_RSS_ENABLED);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.IsRSSEnabled = pSqlReader.GetBoolean(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_IS_ANNOUNCEMENTS_ENABLED);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.IsAnnouncementsEnabled = pSqlReader.GetBoolean(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_MAX_FILEUPLOAD_SIZEMB);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.MaxFileUploadSizeMB = pSqlReader.GetInt32(iIndex);

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_ALLOW_USER_PROFILE_EDIT))
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_ALLOW_USER_PROFILE_EDIT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entClient.AllowUserProfileEdit = pSqlReader.GetBoolean(iIndex);
            }

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_FEED_BACK_RECEIVER_EMAIL_ID))
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_FEED_BACK_RECEIVER_EMAIL_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entClient.FeedbackReceiverEmailId = pSqlReader.GetString(iIndex);
            }

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_LU_CLIENTID))
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_LU_CLIENTID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entClient.LUClientId = pSqlReader.GetString(iIndex);
            }
            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_LU_CLIENT_NAME))
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_LU_CLIENT_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entClient.LUClientName = pSqlReader.GetString(iIndex);
            }

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_SEARCH_ADMIN_NAME))
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_SEARCH_ADMIN_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entClient.AdminNameSearch = pSqlReader.GetString(iIndex);
            }

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_ADMIN_EMAIL_ID))
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_ADMIN_EMAIL_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entClient.AdminEmailId = pSqlReader.GetString(iIndex);
            }

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_TOTAL_USERS))
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_TOTAL_USERS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entClient.TotalUsers = pSqlReader.GetInt32(iIndex);
            }

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_TOTAL_ALLOCATION))
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_TOTAL_ALLOCATION);
                if (!pSqlReader.IsDBNull(iIndex))
                    entClient.TotalAllocation = pSqlReader.GetInt32(iIndex);
            }

            iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.CreatedById = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.DateCreated = pSqlReader.GetDateTime(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.LastModifiedById = pSqlReader.GetString(iIndex);

            iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.LastModifiedDate = pSqlReader.GetDateTime(iIndex);

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY_NAME))
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entClient.CreatedByName = pSqlReader.GetString(iIndex);
            }

            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY_NAME))
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entClient.LastModifiedByName = pSqlReader.GetString(iIndex);
            }

            if (pRangeList)
            {
                _entRange = new EntityRange();
                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                if (!pSqlReader.IsDBNull(iIndex))
                    _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                entClient.ListRange = _entRange;
            }

            return entClient;
        }

        /// <summary>
        /// FillVirtualTrainingClient
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <param name="pRangeList"></param>
        /// <param name="pSqlObject"></param>
        /// <returns></returns>
        private Client FillVirtualTrainingClient(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            Client entClient = new Client();
            int iIndex;

            iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
            if (!pSqlReader.IsDBNull(iIndex))
                entClient.ID = pSqlReader.GetString(iIndex);

            return entClient;
        }

        /// <summary>
        /// Get All Client Details
        /// </summary>
        /// <returns>List of Client Object</returns>
        public List<Client> GetAllClients()
        {
            Client entClient = null;
            List<Client> entListClient = null;
            _sqlObject = new SQLObject();
            SqlConnection sqlMasterConnection = new SqlConnection(_sqlObject.GetMasterDBConnString());
            _sqlcmd = new SqlCommand(Schema.Client.PROC_GET_ALL_CLIENTS, sqlMasterConnection);
            entClient = new Client();
            entListClient = new List<Client>();
            try
            {
                _sqlreader = _sqlObject.ExecuteMasterReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entClient = FillClientMaster(_sqlreader, false, _sqlObject);
                    entListClient.Add(entClient);
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
                if (sqlMasterConnection != null && sqlMasterConnection.State != ConnectionState.Closed)
                    sqlMasterConnection.Close();
            }
            return entListClient;
        }

        public List<Client> GetAllVirtualTrainingClient()
        {
            Client entClient = null;
            List<Client> entListClient = null;
            _sqlObject = new SQLObject();
            SqlConnection sqlMasterConnection = new SqlConnection(_sqlObject.GetMasterDBConnString());
            _sqlcmd = new SqlCommand(Schema.Client.PROC_GET_ALL_CLIENTS_VIRTUAL_TRAINING, sqlMasterConnection);
            entClient = new Client();
            entListClient = new List<Client>();
            try
            {
                _sqlreader = _sqlObject.ExecuteMasterReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entClient = FillVirtualTrainingClient(_sqlreader, false, _sqlObject);
                    entListClient.Add(entClient);
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
                if (sqlMasterConnection != null && sqlMasterConnection.State != ConnectionState.Closed)
                    sqlMasterConnection.Close();
            }
            return entListClient;
        }
        /// <summary>
        /// Add Client Details
        /// </summary>
        /// <param name="pObjClient"></param>
        /// <param name="strMode"></param>
        /// <param name="cmd"></param>
        /// <param name="con"></param>
        /// <returns>Client Object</returns>
        private Client AddClientDetails(Client pObjClient, string strMode, SqlCommand cmd, SqlConnection con)
        {
            cmd.CommandText = Schema.Client.PROC_UPDATE_CLIENT;
            cmd.CommandType = CommandType.StoredProcedure;
            string strClientId = string.Empty;
            Cluster entCluster = new Cluster();
            ClusterDAM clusterAdaptor = new ClusterDAM();

            FileHandler objFileHandler = new FileHandler(Client.BASE_CLIENT_ID);
            int i = 0;

            if (String.IsNullOrEmpty(pObjClient.ID))
            {
                strClientId = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_CLIENT_ID_PREFIX, Schema.Common.VAL_CLIENT_ID_LENGTH);
                pObjClient.ID = strClientId;
                if (!string.IsNullOrEmpty(pObjClient.strDBNamePrefix))
                {
                    pObjClient.DatabaseName = pObjClient.strDBNamePrefix + pObjClient.ID;
                }
                else
                {
                    pObjClient.DatabaseName = Schema.Client.VAL_CLIENT_DB_NAME_PREFIX + pObjClient.ID;
                }

            }

            if (pObjClient.ClientCluster != null)
            {
                entCluster.ID = pObjClient.ClientCluster.ID;
                entCluster = clusterAdaptor.GetClusterByID(entCluster);
                pObjClient.DBIPAddress = entCluster.DatabaseIP;
                pObjClient.DBPassword = EncryptionManager.Decrypt(entCluster.DatabasePassword);
                pObjClient.DBUID = entCluster.DatabaseUserName;
                if (!string.IsNullOrEmpty(pObjClient.ID) && !objFileHandler.IsFolderExist(FileHandler.CLIENTS_FOLDER_PATH, pObjClient.ID))
                {
                    objFileHandler.CreateFolder(FileHandler.CLIENTS_FOLDER_PATH, pObjClient.ID);
                }

                if (!string.IsNullOrEmpty(entCluster.ContentFolderURL))
                    pObjClient.ContentServerURL = entCluster.ContentFolderURL + FileHandler.CLIENTS_FOLDER_PATH + "/" + pObjClient.ID + "/";
                try
                {
                    if (!objFileHandler.IsFolderExist(FileHandler.CLIENTS_FOLDER_PATH + "/" + pObjClient.ID, FileHandler.SITE_IMAGES_PATH))
                    {
                        objFileHandler.CreateFolder(FileHandler.CLIENTS_FOLDER_PATH + "/" + pObjClient.ID, FileHandler.SITE_IMAGES_PATH);
                    }
                    ////objFileHandler.CopyFile(FileHandler.SITE_IMAGES_PATH + "/" + FileHandler.DEFAULT_LOGO_FILE_NAME, FileHandler.CLIENTS_FOLDER_PATH + "/" + pObjClient.ID + "/" + FileHandler.SITE_IMAGES_PATH + "/" + FileHandler.DEFAULT_LOGO_FILE_NAME);

                    ////#region ADDED by sarita for certification
                    ////if (!objFileHandler.IsFolderExist(FileHandler.CLIENTS_FOLDER_PATH + "/" + pObjClient.ID, FileHandler.CERTIFICATE_PATH))
                    ////{
                    ////    objFileHandler.CreateFolder(FileHandler.CLIENTS_FOLDER_PATH + "/" + pObjClient.ID, FileHandler.CERTIFICATE_PATH);
                    ////}
                    ////if (!objFileHandler.IsFolderExist(FileHandler.CLIENTS_FOLDER_PATH + "/" + pObjClient.ID, FileHandler.CERTIFICATE_PATH + "/" + FileHandler.CERTIFICATE_CSS_PATH))
                    ////{
                    ////    objFileHandler.CreateFolder(FileHandler.CLIENTS_FOLDER_PATH + "/" + pObjClient.ID, FileHandler.CERTIFICATE_PATH + "/" + FileHandler.CERTIFICATE_CSS_PATH);
                    ////}
                    ////if (!objFileHandler.IsFolderExist(FileHandler.CLIENTS_FOLDER_PATH + "/" + pObjClient.ID, FileHandler.CERTIFICATE_PATH + "/" + FileHandler.CERTIFICATE_IMAGE_PATH))
                    ////{
                    ////    objFileHandler.CreateFolder(FileHandler.CLIENTS_FOLDER_PATH + "/" + pObjClient.ID, FileHandler.CERTIFICATE_PATH + "/" + FileHandler.CERTIFICATE_IMAGE_PATH);
                    ////}
                    ////objFileHandler.CopyFile(FileHandler.CERTIFICATE_PATH + "/" + FileHandler.CERTIFICATE_CSS_PATH + "/" + FileHandler.CERTIFICATE_CSS_FILENAME, FileHandler.CLIENTS_FOLDER_PATH + "/" + pObjClient.ID + "/" + FileHandler.CERTIFICATE_PATH + "/" + FileHandler.CERTIFICATE_CSS_PATH + "/" + FileHandler.CERTIFICATE_CSS_FILENAME);
                    ////objFileHandler.CopyFile(FileHandler.CERTIFICATE_PATH + "/" + FileHandler.CERTIFICATE_IMAGE_PATH + "/" + FileHandler.CERTIFICATE_IMAGE_FILENAME, FileHandler.CLIENTS_FOLDER_PATH + "/" + pObjClient.ID + "/" + FileHandler.CERTIFICATE_PATH + "/" + FileHandler.CERTIFICATE_IMAGE_PATH + "/" + FileHandler.CERTIFICATE_IMAGE_FILENAME);
                    ////#endregion ADDED by sarita for certification



                    objFileHandler.CopyFolderContentNew(FileHandler.CLIENTS_FOLDER_PATH + "/" + "ClientTemplateStructre", FileHandler.CLIENTS_FOLDER_PATH + "/" + pObjClient.ID);

                }
                catch (Exception expCommon)
                {
                    _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                    throw _expCustom;
                }
            }

            _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pObjClient.ID);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Cluster.PARA_CLUSTER_ID, pObjClient.ClientCluster.ID);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.ClientName))
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_NAME, pObjClient.ClientName);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_NAME, null);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_IS_ACTIVE, pObjClient.IsActive);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_ISCERTIFCATIONENABLED, pObjClient.IsCertifcationEnabled);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.ClientDescription))
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_DESCRIPTION, pObjClient.ClientDescription);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_DESCRIPTION, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.ClientAccessURL))
                _sqlpara = new SqlParameter(Schema.Client.PARA_ACCESS_URL, pObjClient.ClientAccessURL);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_ACCESS_URL, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.DatabaseName))
                _sqlpara = new SqlParameter(Schema.Client.PARA_DATABASE_NAME, pObjClient.DatabaseName);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_DATABASE_NAME, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.DBIPAddress))
                _sqlpara = new SqlParameter(Schema.Client.PARA_DBIP_ADDRESS, pObjClient.DBIPAddress);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_DBIP_ADDRESS, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.DBUID))
                _sqlpara = new SqlParameter(Schema.Client.PARA_DB_UID, pObjClient.DBUID);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_DB_UID, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.DBPassword))
            {
                string password = EncryptionManager.Encrypt(pObjClient.DBPassword);
                _sqlpara = new SqlParameter(Schema.Client.PARA_DB_PASSWORD, password);
            }
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_DB_PASSWORD, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.DefaultLanguageId))
                _sqlpara = new SqlParameter(Schema.Client.PARA_DEFAULT_LANGUAGE_ID, pObjClient.DefaultLanguageId);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_DEFAULT_LANGUAGE_ID, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.DefaultLayoutId))
                _sqlpara = new SqlParameter(Schema.Client.PARA_DEFAULT_LAOUT_ID, pObjClient.DefaultLayoutId);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_DEFAULT_LAOUT_ID, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.DefaultThemeId))
                _sqlpara = new SqlParameter(Schema.Client.PARA_DEFAULT_THEME_ID, pObjClient.DefaultThemeId);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_DEFAULT_THEME_ID, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.ContentServerURL))
                _sqlpara = new SqlParameter(Schema.Client.PARA_CONTENT_SERVER_URL, pObjClient.ContentServerURL);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_CONTENT_SERVER_URL, null);
            cmd.Parameters.Add(_sqlpara);

            if (pObjClient.NumberOfUserLicenses != 0)
                _sqlpara = new SqlParameter(Schema.Client.PARA_NUMBER_OF_USER_LICENSES, pObjClient.NumberOfUserLicenses);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_NUMBER_OF_USER_LICENSES, null);
            cmd.Parameters.Add(_sqlpara);

            if (pObjClient.SessionTimeOut != 0)
                _sqlpara = new SqlParameter(Schema.Client.PARA_SESSION_TIME_OUT, pObjClient.SessionTimeOut);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_SESSION_TIME_OUT, null);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_IS_SELF_REGISTRATION, pObjClient.IsSelfRegistration);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_IS_PASSCODE_BASED, pObjClient.IsPassCodeBased);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_IS_EMAIL_DOMAIN_NASED, pObjClient.IsEmailDomainBased);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_IS_SSO_CONFIGURED, pObjClient.IsSSOConfigured);
            cmd.Parameters.Add(_sqlpara);

            if (pObjClient.SSOType != Client.SingleSignOnType.None)
                _sqlpara = new SqlParameter(Schema.Client.PARA_SSO_TYPE_ID, pObjClient.SSOType);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_SSO_TYPE_ID, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.LogoutRedirectionURL))
                _sqlpara = new SqlParameter(Schema.Client.PARA_LOGOUT_URL, pObjClient.LogoutRedirectionURL);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_LOGOUT_URL, null);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_IS_LOCKED, pObjClient.IsLocked);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_IS_FORGOT_PWD_ENABLED, pObjClient.IsForgotPasswordEnabled);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_IS_CONTACT_US_ENABLED, pObjClient.IsContactUsEnabled);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_IS_FEEDBACK_ENABLED, pObjClient.IsFeedbackEnabled);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_IS_RSS_ENABLED, pObjClient.IsRSSEnabled);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_IS_ANNOUNCEMENTS_ENABLED, pObjClient.IsAnnouncementsEnabled);
            cmd.Parameters.Add(_sqlpara);

            if (pObjClient.MaxFileUploadSizeMB != 0)
                _sqlpara = new SqlParameter(Schema.Client.PARA_MAX_FILEUPLOAD_SIZEMB, pObjClient.MaxFileUploadSizeMB);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_MAX_FILEUPLOAD_SIZEMB, null);
            cmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pObjClient.ContractStartDate) < 0)
                _sqlpara = new SqlParameter(Schema.Client.PARA_CONTRACT_START_DATE, pObjClient.ContractStartDate);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_CONTRACT_START_DATE, null);
            cmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pObjClient.ContractEndDate) < 0)
                _sqlpara = new SqlParameter(Schema.Client.PARA_CONTRACT_END_DATE, pObjClient.ContractEndDate);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_CONTRACT_END_DATE, null);
            cmd.Parameters.Add(_sqlpara);

            if (pObjClient.MaxConcurrentSessions != null)
                _sqlpara = new SqlParameter(Schema.Client.PARA_MAX_CONCURRENT_SESSIONS, pObjClient.MaxConcurrentSessions);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_MAX_CONCURRENT_SESSIONS, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.SMTPServerIP))
                _sqlpara = new SqlParameter(Schema.Client.PARA_SMTP_SERVER_IP, pObjClient.SMTPServerIP);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_SMTP_SERVER_IP, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.SMTPUserName))
                _sqlpara = new SqlParameter(Schema.Client.PARA_SMTP_USER_NAME, pObjClient.SMTPUserName);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_SMTP_USER_NAME, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.SMTPPassword))
                _sqlpara = new SqlParameter(Schema.Client.PARA_SMTP_PWD, pObjClient.SMTPPassword);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_SMTP_PWD, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.SiteSubDomainName))
                _sqlpara = new SqlParameter(Schema.Client.PARA_SITE_SUB_DOMAIN_NAME, pObjClient.SiteSubDomainName);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_SITE_SUB_DOMAIN_NAME, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.SAIConsultingServiceManager))
                _sqlpara = new SqlParameter(Schema.Client.PARA_SAI_SERVICE_MANAGER, pObjClient.SAIConsultingServiceManager);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_SAI_SERVICE_MANAGER, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.CreatedById))
                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pObjClient.CreatedById);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pObjClient.LastModifiedById))
                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pObjClient.LastModifiedById);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, null);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, strMode);
            cmd.Parameters.Add(_sqlpara);

            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            i = cmd.ExecuteNonQuery();

            return pObjClient;
        }




        /// <summary>
        /// Create Client (Client Database with database script) - Also adds the client entry into master database and new created client database
        /// </summary>
        /// <param name="pObjClient"></param>
        /// <returns>Client Object</returns>
        public Client AddClient(Client pObjClient)
        {
            string strDataBaseName = string.Empty;
            bool IsHTTPS = false;
            IsHTTPS = pObjClient.IsHTTPSAllowed;
            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetMasterDBConnString();
            string strMode = Schema.Common.VAL_INSERT_MODE;
            bool trMaster = false;
            bool trRollBackMaster = false;
            SqlConnection sqlconMaster = new SqlConnection(_strConnString);
            SqlCommand sqlcmdMaster = new SqlCommand();
            SqlTransaction sqltransactionMaster = null;

            SqlCommand cmdNewDB = new SqlCommand();
            cmdNewDB.CommandText = Schema.Client.PROC_CREATE_CLIENT_DATABASE;
            cmdNewDB.CommandType = CommandType.StoredProcedure;
            SqlParameter paraNewDB;

            SqlCommand cmdDBDrop = new SqlCommand();
            cmdDBDrop.CommandText = Schema.Client.PROC_DROP_CLIENT_DATABASE;
            cmdDBDrop.CommandType = CommandType.StoredProcedure;
            SqlParameter paraDBdrop;


            SqlCommand cmdCreateCluster = new SqlCommand();
            cmdCreateCluster.CommandType = CommandType.StoredProcedure;
            try
            {
                //Open Master Connection
                sqlconMaster.Open();
                sqltransactionMaster = sqlconMaster.BeginTransaction();
                sqlcmdMaster.Transaction = sqltransactionMaster;
                pObjClient = AddClientDetails(pObjClient, strMode, sqlcmdMaster, sqlconMaster);
                if (pObjClient != null)
                {
                    //--------------Create New Client DataBase(Only Create Database and Drop Database is not in Transaction so used common connection string)---------------------------- -
                    _strConnString = _sqlObject.GetMasterDBConnString();

                    paraNewDB = new SqlParameter(Schema.Common.PARA_DATABASE_NAME, pObjClient.DatabaseName);
                    cmdNewDB.Parameters.Add(paraNewDB);
                    strDataBaseName = pObjClient.DatabaseName;
                    cmdNewDB.CommandTimeout = 0;
                    bool iNewDB = _sqlObject.ExecuteNonQuery(cmdNewDB, _strConnString, false);
                    if (iNewDB)
                    {

                        //**---------------Create New Client(Executing the script and add default entry in database) ------------------
                        pObjClient = CreateNewClient(pObjClient, strMode);
                        if (pObjClient != null)
                        {
                            //Commit Master DataBase Transaction
                            sqltransactionMaster.Commit();

                            #region to insert new cluster for new client

                            cmdCreateCluster.CommandText = Schema.Client.PROC_CREATE_CLUSTER_FOR_NEWCLIENT;
                            _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pObjClient.ID);
                            cmdCreateCluster.Parameters.Add(_sqlpara);

                            _sqlpara = new SqlParameter(Schema.Client.PARA_IS_HTTPS_ALLOWED, IsHTTPS);
                            cmdCreateCluster.Parameters.Add(_sqlpara);

                            if (!String.IsNullOrEmpty(pObjClient.ClientName))
                                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_NAME, pObjClient.ClientName);
                            else
                                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_NAME, null);
                            cmdCreateCluster.Parameters.Add(_sqlpara);


                            if (!String.IsNullOrEmpty(pObjClient.ClientAccessURL))
                                _sqlpara = new SqlParameter(Schema.Client.PARA_ACCESS_URL, pObjClient.ClientAccessURL);
                            else
                                _sqlpara = new SqlParameter(Schema.Client.PARA_ACCESS_URL, null);
                            cmdCreateCluster.Parameters.Add(_sqlpara);

                            if (!String.IsNullOrEmpty(pObjClient.DatabaseName))
                                _sqlpara = new SqlParameter(Schema.Client.PARA_DATABASE_NAME, pObjClient.DatabaseName);
                            else
                                _sqlpara = new SqlParameter(Schema.Client.PARA_DATABASE_NAME, null);
                            cmdCreateCluster.Parameters.Add(_sqlpara);

                            _strConnString = _sqlObject.GetMasterDBConnString();
                            _sqlObject.ExecuteNonQuery(cmdCreateCluster, _strConnString, false);

                            #endregion to insert new cluster for new client
                            trMaster = true;
                        }
                        else
                        {
                            //---------Drop the New Created Client DataBase------------------------------------
                            _strConnString = _sqlObject.GetMasterDBConnString();
                            paraDBdrop = new SqlParameter(Schema.Common.PARA_DATABASE_NAME, strDataBaseName);
                            cmdDBDrop.Parameters.Add(paraDBdrop);
                            cmdDBDrop.CommandTimeout = 0;
                            bool iDbDrop = _sqlObject.ExecuteNonQuery(cmdDBDrop, _strConnString, false);
                            if (iDbDrop)
                            {
                                //Database Successfully Dropped
                            }
                            else
                            {
                                //Error: Database Not Dropped
                            }
                        }
                    }
                    else
                    {
                        pObjClient = null;
                    }
                }
                else
                {
                    pObjClient = null;
                }
            }
            catch (Exception expCommon)
            {
                if (!trMaster)
                {
                    sqltransactionMaster.Rollback();
                    trRollBackMaster = true;
                    pObjClient = null;
                }
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (!trMaster)
                {
                    if (trRollBackMaster == false)
                    {
                        sqltransactionMaster.Rollback();
                        trRollBackMaster = true;
                    }
                }

                if (sqlconMaster != null && sqlconMaster.State == ConnectionState.Open)
                    sqlconMaster.Close();
            }
            return pObjClient;
        }

        /// <summary>
        /// Create New Client Database
        /// </summary>
        /// <param name="pObjClient"></param>
        /// <param name="strMode"></param>
        /// <returns>Client Object</returns>
        private Client CreateNewClient(Client pObjClient, string strMode)
        {
            _strConnString = "Data Source="
                + pObjClient.DBIPAddress
                + ";Initial Catalog="
                + pObjClient.DatabaseName
                + ";User ID="
                + pObjClient.DBUID
                + ";password="
                + pObjClient.DBPassword + ";";

            using (SqlConnection sqlcon = new SqlConnection(_strConnString))
            {
                using (SqlCommand cmdNewDBScript = sqlcon.CreateCommand())
                {
                    SqlTransaction transaction = null;
                    bool tr = false;
                    bool trRollBack = false;
                    try
                    {
                        //-------------- Create New Schema for the new database -----------
                        // Open New Client DataBase Connection
                        sqlcon.Open();
                        cmdNewDBScript.CommandTimeout = 0;
                        transaction = sqlcon.BeginTransaction();
                        cmdNewDBScript.Transaction = transaction;

                        //-------------- Here SQL Script Read and Create Script Store procedure in Master DataBase --------------
                        int j = 0;
                        cmdNewDBScript.CommandText = Schema.Client.PROC_CREATE_CLIENT_DATABASE_SCHEMA;
                        cmdNewDBScript.CommandType = CommandType.StoredProcedure;
                        cmdNewDBScript.Connection = sqlcon;

                        SqlParameter sqlparaExecScript = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pObjClient.ID);
                        cmdNewDBScript.Parameters.Add(sqlparaExecScript);
                        cmdNewDBScript.CommandTimeout = 0;
                        //--------- Here execute Script Store procedure -------------
                        j = cmdNewDBScript.ExecuteNonQuery();

                        //Clear the previous command object Parameters
                        cmdNewDBScript.Parameters.Clear();

                        //------------- Add Default entry in New Client Database (Client Master table)
                        pObjClient = AddClientDetails(pObjClient, strMode, cmdNewDBScript, sqlcon);
                        if (pObjClient != null)
                        {
                            // Commit "New Client DataBase" Transaction
                            transaction.Commit();
                            tr = true;
                        }
                    }
                    catch (Exception expCommon)
                    {
                        if (!tr)
                        {
                            transaction.Rollback();
                            trRollBack = true;
                            pObjClient = null;
                        }
                        _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                        throw _expCustom;
                    }
                    finally
                    {
                        if (!tr)
                        {
                            if (trRollBack == false)
                            {
                                transaction.Rollback();
                                trRollBack = true;
                            }
                        }
                        // Close New Client DataBase Connection
                        if (sqlcon != null && sqlcon.State == ConnectionState.Open)
                            sqlcon.Close();
                    }
                }
            }
            return pObjClient;
        }

        /// <summary>
        /// Update Client Information
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns>Client Object</returns>
        public Client UpdateClient(Client pEntClient)
        {
            int i = 0;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_UPDATE_CLIENT;
            _sqlObject = new SQLObject();
            try
            {

                _strConnString = _sqlObject.GetMasterDBConnString();

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.ClientName))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_NAME, pEntClient.ClientName);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_NAME, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_ACTIVE, pEntClient.IsActive);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_ISCERTIFCATIONENABLED, pEntClient.IsCertifcationEnabled);
                _sqlcmd.Parameters.Add(_sqlpara);


                if (!String.IsNullOrEmpty(pEntClient.ClientDescription))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_DESCRIPTION, pEntClient.ClientDescription);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_DESCRIPTION, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.ClientAccessURL))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_ACCESS_URL, pEntClient.ClientAccessURL);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_ACCESS_URL, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.DatabaseName))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_DATABASE_NAME, pEntClient.DatabaseName);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_DATABASE_NAME, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.DBIPAddress))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_DBIP_ADDRESS, pEntClient.DBIPAddress);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_DBIP_ADDRESS, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.DBUID))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_DB_UID, pEntClient.DBUID);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_DB_UID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.DBPassword))
                {
                    string passWord = EncryptionManager.Encrypt(pEntClient.DBPassword);
                    _sqlpara = new SqlParameter(Schema.Client.PARA_DB_PASSWORD, passWord);
                }
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_DB_PASSWORD, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.DefaultLanguageId))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_DEFAULT_LANGUAGE_ID, pEntClient.DefaultLanguageId);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_DEFAULT_LANGUAGE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.DefaultLayoutId))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_DEFAULT_LAOUT_ID, pEntClient.DefaultLayoutId);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_DEFAULT_LAOUT_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.DefaultThemeId))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_DEFAULT_THEME_ID, pEntClient.DefaultThemeId);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_DEFAULT_THEME_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.ContentServerURL))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_CONTENT_SERVER_URL, pEntClient.ContentServerURL);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_CONTENT_SERVER_URL, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntClient.NumberOfUserLicenses != 0)
                    _sqlpara = new SqlParameter(Schema.Client.PARA_NUMBER_OF_USER_LICENSES, pEntClient.NumberOfUserLicenses);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_NUMBER_OF_USER_LICENSES, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntClient.SessionTimeOut != 0)
                    _sqlpara = new SqlParameter(Schema.Client.PARA_SESSION_TIME_OUT, pEntClient.SessionTimeOut);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_SESSION_TIME_OUT, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_SELF_REGISTRATION, pEntClient.IsSelfRegistration);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_PASSCODE_BASED, pEntClient.IsPassCodeBased);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_EMAIL_DOMAIN_NASED, pEntClient.IsEmailDomainBased);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_SSO_CONFIGURED, pEntClient.IsSSOConfigured);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntClient.SSOType != Client.SingleSignOnType.None)
                    _sqlpara = new SqlParameter(Schema.Client.PARA_SSO_TYPE_ID, pEntClient.SSOType.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_SSO_TYPE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.LogoutRedirectionURL))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_LOGOUT_URL, pEntClient.LogoutRedirectionURL);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_LOGOUT_URL, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_LOCKED, pEntClient.IsLocked);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_FORGOT_PWD_ENABLED, pEntClient.IsForgotPasswordEnabled);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_CONTACT_US_ENABLED, pEntClient.IsContactUsEnabled);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_FEEDBACK_ENABLED, pEntClient.IsFeedbackEnabled);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_RSS_ENABLED, pEntClient.IsRSSEnabled);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_ANNOUNCEMENTS_ENABLED, pEntClient.IsAnnouncementsEnabled);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntClient.MaxFileUploadSizeMB != 0)
                    _sqlpara = new SqlParameter(Schema.Client.PARA_MAX_FILEUPLOAD_SIZEMB, pEntClient.MaxFileUploadSizeMB);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_MAX_FILEUPLOAD_SIZEMB, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntClient.ContractStartDate) < 0)
                    _sqlpara = new SqlParameter(Schema.Client.PARA_CONTRACT_START_DATE, pEntClient.ContractStartDate);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_CONTRACT_START_DATE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntClient.ContractEndDate) < 0)
                    _sqlpara = new SqlParameter(Schema.Client.PARA_CONTRACT_END_DATE, pEntClient.ContractEndDate);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_CONTRACT_END_DATE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntClient.MaxConcurrentSessions != null)
                    _sqlpara = new SqlParameter(Schema.Client.PARA_MAX_CONCURRENT_SESSIONS, pEntClient.MaxConcurrentSessions);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_MAX_CONCURRENT_SESSIONS, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.SMTPServerIP))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_SMTP_SERVER_IP, pEntClient.SMTPServerIP);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_SMTP_SERVER_IP, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.SMTPUserName))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_SMTP_USER_NAME, pEntClient.SMTPUserName);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_SMTP_USER_NAME, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.SMTPPassword))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_SMTP_PWD, pEntClient.SMTPPassword);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_SMTP_PWD, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.SiteSubDomainName))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_SITE_SUB_DOMAIN_NAME, pEntClient.SiteSubDomainName);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_SITE_SUB_DOMAIN_NAME, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.SAIConsultingServiceManager))
                    _sqlpara = new SqlParameter(Schema.Client.PARA_SAI_SERVICE_MANAGER, pEntClient.SAIConsultingServiceManager);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_SAI_SERVICE_MANAGER, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntClient.ClientCluster != null && !string.IsNullOrEmpty(pEntClient.ClientCluster.ID))
                    _sqlpara = new SqlParameter(Schema.Cluster.PARA_CLUSTER_ID, pEntClient.ClientCluster.ID);
                else
                    _sqlpara = new SqlParameter(Schema.Cluster.PARA_CLUSTER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.CreatedById))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntClient.CreatedById);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntClient.LastModifiedById))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntClient.LastModifiedById);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                //Update
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_HTTPS_ALLOWED, pEntClient.IsHTTPSAllowed);
                _sqlcmd.Parameters.Add(_sqlpara);
                i = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        /// <summary>
        /// Delete Client
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns>Client Object</returns>
        public Client DeleteClient(Client pEntClient)
        {
            //Delete Client: ClientID may be single or multiple(Seperated by comma)
            int i = 0;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_DELETE_CLIENT;
            _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetMasterDBConnString();
                i = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        /// <summary>
        /// Update Session Time Out
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client UpdateSessionTimeOut(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_UPDATE_SESSION_TIME_OUT;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_SESSION_TIME_OUT, pEntClient.SessionTimeOut);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.CourseConfiguration.PARA_COURSE_SESSION_NEVER_EXPIRES, pEntClient.CourseConfiguration.IsCourseSessionNoExpiry);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY_ID, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetMasterDBConnString();

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        /// <summary>
        /// Update IsAnnouncements Enabled
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client UpdateIsAnnouncementsEnabled(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_UPDATE_IS_ANNOUNCEMENTS_ENABLED;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_ANNOUNCEMENTS_ENABLED, pEntClient.IsAnnouncementsEnabled);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetMasterDBConnString();

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }


        /// <summary>
        /// Update HTTPS Allowed
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client UpdateHTTPSAllowed(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_UPDATE_HTTPSALLOWED;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_HTTPS_ALLOWED, pEntClient.IsHTTPSAllowed);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetClientDBConnString(pEntClient.ClientId);

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }


        /// <summary>
        /// Update AllowUser
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client UpdateAllowUser(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_UPDATE_ALLOW_USER;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_ALLOW_USER_PROFILE_EDIT, pEntClient.AllowUserProfileEdit);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetMasterDBConnString();

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        /// <summary>
        /// Update FeedbackReceiverEmailId
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client UpdateFeedbackReceiverEmailId(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_UPDATE_FEEDBACK_RECEIVER_EMAILID;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_FEED_BACK_RECEIVER_EMAIL_ID, pEntClient.FeedbackReceiverEmailId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetMasterDBConnString();

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        /// <summary>
        /// Update Lock Unlock System
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client UpdateLockUnlockSystem(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_UPDATE_SYSTEM_LOCK_UNLOCK;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_LOCKED, pEntClient.IsLocked);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY_ID, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetMasterDBConnString();

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        /// <summary>
        /// Set Default Theme
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client SetDefaultTheme(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_SET_DEFAULT_THEME;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Theme.PARA_THEME_ID, pEntClient.DefaultThemeId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY_ID, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlcmd.CommandTimeout = 0;
                _strConnString = _sqlObject.GetMasterDBConnString();

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        /// <summary>
        /// Update Logo
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client SetDefaultLogo(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_SET_DEFAULT_LOGO;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Configuration.PARA_DEFAULT_SITE_LOGO, pEntClient.SysConfiguration.DefaultSiteLogo);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetMasterDBConnString();

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        /// <summary>
        /// Manage Forgot Password Link
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client ManageForgotPasswordLink(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_MANAGE_FORGOT_PWD_LINK;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_FORGOT_PWD_ENABLED, pEntClient.IsForgotPasswordEnabled);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY_ID, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetMasterDBConnString();

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        /// <summary>
        /// SetLogoutRedirectionURL
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client SetLogoutRedirectionURL(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_SET_LOGOUT_REDIRECTION_URL;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_LOGOUT_URL, pEntClient.LogoutRedirectionURL);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY_ID, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetMasterDBConnString();

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        /// <summary>
        /// Set Self Registration Type
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client SetSelfRegistrationType(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_SET_SELF_REGISTRATION_TYPE;
            _sqlObject = new SQLObject();
            try
            {

                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_SELF_REGISTRATION, pEntClient.IsSelfRegistration);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_PASSCODE_BASED, pEntClient.IsPassCodeBased);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_EMAIL_DOMAIN_NASED, pEntClient.IsEmailDomainBased);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_ALLOWED_DOMAINS_LIST, pEntClient.SysConfiguration.AllowedDomainsList);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY_ID, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetMasterDBConnString();

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }



        /// <summary>
        /// Set Self Registration Type
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client SetNonRestrictedDomain(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();

            _sqlcmd.CommandText = Schema.Client.PROC_SET_NON_RESTRICTED_DOMAIN;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_NON_RESTRICTED_DOMAIN_LIST, pEntClient.SysConfiguration.NonRestrictedDomainList);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY_ID, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetClientDBConnString(pEntClient.ID);

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        /// <summary>
        /// Set if Photo is visible on home page for learner
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client SaveUserPhotoDisplaySettings(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();

            _sqlcmd.CommandText = Schema.Client.PROC_UPDATE_USER_PHOTO_ISDISPLAY;
            _sqlObject = new SQLObject();
            try
            {
                if (pEntClient.SysConfiguration.IsDisplay != null)
                    _sqlpara = new SqlParameter(Schema.Client.PARA_ISDISPLAY, pEntClient.SysConfiguration.IsDisplay);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_ISDISPLAY, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetClientDBConnString(pEntClient.ID);

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        /// <summary>
        /// Set if User is allowed to upload photo
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client SaveAllowUploadPhotoSettings(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();

            _sqlcmd.CommandText = Schema.Client.PROC_UPDATE_USER_PHOTO_ISALLOWUPLOADPHOTO;
            _sqlObject = new SQLObject();
            try
            {
                if (pEntClient.SysConfiguration.IsAllowUploadPhoto != null)
                    _sqlpara = new SqlParameter(Schema.Client.PARA_ISALLOWUPLOADPHOTO, pEntClient.SysConfiguration.IsAllowUploadPhoto);
                else
                    _sqlpara = new SqlParameter(Schema.Client.PARA_ISALLOWUPLOADPHOTO, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetClientDBConnString(pEntClient.ID);

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }


        public Client GetPhotoDisplaySettings(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();

            _sqlcmd.CommandText = Schema.Client.PROC_GET_USER_PHOTO_ISDISPLAY_SETTINGS;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntClient.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                pEntClient = FillPhoto(_sqlreader, _sqlObject);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        public Client GetAllowUploadPhotoSettings(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();

            _sqlcmd.CommandText = Schema.Client.PROC_GET_USER_PHOTO_ISALLOWUPLOADPHOTO_SETTINGS;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntClient.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                pEntClient = FillPhoto(_sqlreader, _sqlObject);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        /// <summary>
        /// Set Max Upload FileSize
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client SetMaxUploadFileSize(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_SET_MAX_UPLOAD_FILE_SIZE;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_MAX_FILEUPLOAD_SIZEMB, pEntClient.MaxFileUploadSizeMB);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY_ID, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetMasterDBConnString();

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        public Client SetSSOType(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_SET_SSO_TYPE;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_SSO_CONFIGURED, pEntClient.IsSSOConfigured);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_SSO_TYPE_ID, pEntClient.SSOType.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetMasterDBConnString();

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        /// <summary>
        /// Set IsRSS Enabled
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client SetIsRSSEnabled(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_SET_IS_RSS_ENABLED;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_RSS_ENABLED, pEntClient.IsRSSEnabled);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY_ID, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetMasterDBConnString();

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        /// <summary>
        /// Update Activate/Deactivate Client
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client ActivateDeactivateClient(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_ACTIVATE_DEACTIVATE_CLIENT;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_IS_ACTIVE, pEntClient.IsActive);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetMasterDBConnString();

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        /// <summary>
        /// Set Default Page Size
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client SetDefaultpageSize(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_SET_DEFAULT_PAGE_SIZE;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntClient.PageSize);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetClientDBConnString(pEntClient.ID);

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }



        /// <summary>
        /// Set Audit Trail Period
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Client SetAuditTrailPeriod(Client pEntClient)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Client.PROC_UPDATE_AUDITTRAILPERIOD;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_AUDITTRAILPERIOD, pEntClient.SysConfiguration.AuditTrailPeriod);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntClient.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetClientDBConnString(pEntClient.ID);

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntClient;
        }

        /// <summary>
        /// Get client AuditTrailPeriod
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns>Returns Client object</returns>
        public Client GetAuditTrailPeriod(Client pEntClient)
        {
            Client entClient = new Client();
            _sqlcmd = new SqlCommand();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(pEntClient.ClientId);
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = _strConnString;
            _sqlcmd = new SqlCommand(Schema.Client.PROC_GET_AUDITTRAILPERIOD, sqlConnection);
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    entClient = FillAuditTrailPeriod(_sqlreader);
                }
                else
                {
                    entClient = null;
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
            return entClient;
        }

        /// <summary>
        /// Get client AuditTrailPeriod
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns>Returns Client object</returns>
        public Client GetJIRAHelpDesk(Client pEntClient)
        {
            Client entClient = new Client();
            _sqlcmd = new SqlCommand();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(pEntClient.ClientId);
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = _strConnString;
            _sqlcmd = new SqlCommand(Schema.Client.PROC_GET_JIRAHELPDESK, sqlConnection);
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    entClient = FillJIRAHelpDesk(_sqlreader);
                }
                else
                {
                    entClient = null;
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
            return entClient;
        }


        /// <summary>
        /// Get client DOC Licence Path
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns>Returns Client object</returns>
        public Client GetDocLicensePath(Client pEntClient)
        {
            Client entClient = new Client();
            _sqlcmd = new SqlCommand();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(pEntClient.ClientId);
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = _strConnString;
            _sqlcmd = new SqlCommand(Schema.Client.PROC_GET_GETDOCLICENSEPATH, sqlConnection);
            try
            {
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntClient.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    entClient = FillGroupDocLicensePath(_sqlreader);
                }
                else
                {
                    entClient = null;
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
            return entClient;
        }


        /// <summary>
        /// Fill reader of client master
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>Client Object</returns>
        private Client FillAuditTrailPeriod(SqlDataReader pSqlReader)
        {
            Client entClient = new Client();
            SystemConfiguration entSystemConfiguration = new SystemConfiguration();
            int iIndex;

            iIndex = pSqlReader.GetOrdinal(Schema.Configuration.COL_AUDITTRAILPERIOD);
            if (!pSqlReader.IsDBNull(iIndex))
            {
                entSystemConfiguration.AuditTrailPeriod = pSqlReader.GetInt32(iIndex);
                entClient.SysConfiguration = entSystemConfiguration;
            }

            return entClient;
        }

        private Client FillJIRAHelpDesk(SqlDataReader pSqlReader)
        {
            Client entClient = new Client();
            SystemConfiguration entSystemConfiguration = new SystemConfiguration();
            int iIndex;

            iIndex = pSqlReader.GetOrdinal(Schema.Configuration.COL_JIRAHELPDESKPROJECTID);
            if (!pSqlReader.IsDBNull(iIndex))
            {
                entSystemConfiguration.JiraHelpDeskProjectId = pSqlReader.GetString(iIndex);
                entClient.SysConfiguration = entSystemConfiguration;
            }

            iIndex = pSqlReader.GetOrdinal(Schema.Configuration.COL_JIRAHELPDESKREQUESTTYPE);
            if (!pSqlReader.IsDBNull(iIndex))
            {
                entSystemConfiguration.JiraHelpDeskRequestType = pSqlReader.GetString(iIndex);
                entClient.SysConfiguration = entSystemConfiguration;
            }
            return entClient;
        }
        //Samreen
        private Client FillGroupDocLicensePath(SqlDataReader pSqlReader)
        {
            Client entClient = new Client();
            SystemConfiguration entSystemConfiguration = new SystemConfiguration();
            int iIndex;

            iIndex = pSqlReader.GetOrdinal(Schema.Configuration.COL_DOCLICENSEPATH);
            if (!pSqlReader.IsDBNull(iIndex))
            {
                entSystemConfiguration.DocLicensePath = pSqlReader.GetString(iIndex);
                entClient.SysConfiguration = entSystemConfiguration;
            }
            return entClient;
        }

        #region Interface Methods
        /// <summary>
        /// Get Client for specific ID
        /// </summary>
        /// <param name="pEntBase">Client with Id</param>
        /// <returns>Client</returns>
        public Client Get(Client pEntBase)
        {
            return GetClientByID(pEntBase);
        }

        /// <summary>
        /// Update Client with given data
        /// </summary>
        /// <param name="pEntBase">Client with data to update</param>
        /// <returns>Updated Client</returns>
        public Client Update(Client pEntBase)
        {
            return UpdateClient(pEntBase);
        }

        public Client SetConnectionForImport(Client pEntClient)
        {
            _sqlObject = new SQLObject();
            SqlConnection sqlConnectionImport = null;
            if (pEntClient.IsActive)
            {
                sqlConnectionImport = new SqlConnection(_sqlObject.GetClientDBConnString(pEntClient));
                sqlConnectionImport.Open();
                pEntClient.ImportConnection = sqlConnectionImport;
            }
            else
            {
                sqlConnectionImport = pEntClient.ImportConnection;
                if (sqlConnectionImport != null && sqlConnectionImport.State != ConnectionState.Closed)
                    sqlConnectionImport.Close();
                pEntClient.ImportConnection = null;
            }
            return pEntClient;
        }
        #endregion
    }
}
