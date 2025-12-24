using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.YPLMS.Services;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public class ClusterDAM : IDataManager<Cluster>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlParameter _sqlpara = null;
        SqlCommand _sqlcmd = null;
        Cluster _entCluster = null;
        List<Cluster> _entListCluster = null;
        string _strMessageId = YPLMS.Services.Messages.Client.CLIENT_DL_ERROR;
        string _strConnString = string.Empty;
        SQLObject _sqlObject = null;
        #endregion

        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>Cluster Object</returns>
        private Cluster FillObject(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            Cluster entCluster = new Cluster();
            int iIndex = 0;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_CLUSTER_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_CLUSTER_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.ClusterName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_IS_ACTIVE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.IsActive = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_CLUSTER_IP);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.ClusterIP = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_CONTENT_SERVER_IP);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.ContentServerIP = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_DATABASE_IP);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.DatabaseIP = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_CLUSTER_DNSSERVER_IP);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.DNSServerIP = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_CLUSTER_DNSSERVER_PASSWORD);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.DNSServerPassword = pSqlReader.GetString(iIndex);
                entCluster.DNSServerPassword = EncryptionManager.Decrypt(entCluster.DNSServerPassword);

                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_CLUSTER_DNSSERVER_USERNAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.DNSServerUserName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_DATABASE_USERNAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.DatabaseUserName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_DATABASE_PASSWORD);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.DatabasePassword = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_CONTENT_FOLDER_PATH);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.ContentFolderPath = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_CONTENT_FOLDER_URL);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.ContentFolderURL = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_FTP_UPLOAD_PATH);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.FTPUploadPath = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_DOMAIN_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.DomainName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.CreatedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.DateCreated = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.LastModifiedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.LastModifiedDate = pSqlReader.GetDateTime(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Cluster.COL_FTP_USER_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_FTP_USER_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entCluster.FTPUserName = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Cluster.COL_FTP_PASSWORD))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_FTP_PASSWORD);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entCluster.FTPPassword = EncryptionManager.Decrypt(pSqlReader.GetString(iIndex));
                }

                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_COURSE_PLAYERURL);
                if (!pSqlReader.IsDBNull(iIndex))
                {
                    entCluster.CoursePlayerURL = pSqlReader.GetString(iIndex);
                    //entCluster.CoursePlayerURL = "http://localhost:3768/CoursePlayer/LaunchPlayer.aspx";
                }
                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_LU_WEBSERVICEURL);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.LUWebServiceURL = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Cluster.COL_LU_APPLICATIONLAUNCH_URL);
                if (!pSqlReader.IsDBNull(iIndex))
                    entCluster.LUApplicationLaunchURL = pSqlReader.GetString(iIndex);

                entCluster.ClientId = Client.BASE_CLIENT_ID;
            }
            return entCluster;
        }

        /// <summary>
        /// Get All Cluster Details
        /// </summary>
        /// <returns>List of Cluster Object</returns>
        public List<Cluster> GetAllClusters(Cluster pEntCluster)
        {
            _sqlObject = new SQLObject();
            Cluster entCluster = null;
            List<Cluster> entListCluster = null;
            SqlConnection sqlMasterConnection = new SqlConnection(_sqlObject.GetMasterDBConnString());
            _sqlcmd = new SqlCommand(Schema.Cluster.PROC_GET_ALL_CLUSTERS, sqlMasterConnection);

            if (!string.IsNullOrEmpty(pEntCluster.ID))
            {
                _sqlpara = new SqlParameter(Schema.Cluster.PARA_CLUSTER_ID, pEntCluster.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            entCluster = new Cluster();
            entListCluster = new List<Cluster>();
            try
            {
                _sqlreader = _sqlObject.ExecuteMasterReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entCluster = FillObject(_sqlreader, _sqlObject);
                    entListCluster.Add(entCluster);
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
            return entListCluster;
        }
        /// <summary>
        /// Get Cluster for specific ID
        /// </summary>
        /// <param name="pEntCluster">Cluster with Id</param>
        /// <returns>Cluster</returns>
        public Cluster GetClusterByID(Cluster pEntCluster)
        {
            _entCluster = new Cluster();
            _entListCluster = new List<Cluster>();
            try
            {
                _entListCluster = GetAllClusters(pEntCluster);
                if (_entListCluster != null && _entListCluster.Count > 0)
                    _entCluster = _entListCluster[0];
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return _entCluster;
        }

        /// <summary>
        /// Set or Update LU Client
        /// </summary>
        /// <param name="pEntClient"></param>
        /// <returns></returns>
        public Cluster UpdateCluster(Cluster pEntCluster)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Cluster.PROC_UPDATE_CLUSTER;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Cluster.PARA_CLUSTER_ID, pEntCluster.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntCluster.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Cluster.PARA_LU_APPLICATIONLAUNCH_URL, pEntCluster.LUApplicationLaunchURL);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Cluster.PARA_LU_WEBSERVICEURL, pEntCluster.LUWebServiceURL);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.PARA_UPDATE_MODE);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetMasterDBConnString();

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntCluster;
        }
        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntCluster"></param>
        /// <returns></returns>
        public Cluster Get(Cluster pEntCluster)
        {
            return GetClusterByID(pEntCluster);
        }
        /// <summary>
        /// Update Cluster
        /// </summary>
        /// <param name="pEntCluster"></param>
        /// <returns></returns>
        public Cluster Update(Cluster pEntCluster)
        {
            return UpdateCluster(pEntCluster);
        }
        #endregion
    }
}
