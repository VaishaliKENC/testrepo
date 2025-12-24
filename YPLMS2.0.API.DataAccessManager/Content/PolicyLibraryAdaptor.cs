using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    /// <summary>
    /// class PolicyLibrary Adaptor
    /// </summary>
    public class PolicyLibraryAdaptor : IDataManager<PolicyLibrary>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.PolicyLibrary.LIBRARY_BL_ERROR;
        string _strPolicyAssignMsgId = YPLMS.Services.Messages.PolicyLibrary.LIBRARY_ASSIGN;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// To Get Policy Library details by PolicyLibrary Id.
        /// </summary>
        /// <param name="pEntPolicyLib"></param>
        /// <returns>PolicyLibrary Object</returns>
        public PolicyLibrary GetPolicyLibraryById(PolicyLibrary pEntPolicyLib)
        {
            _sqlObject = new SQLObject();
            PolicyLibrary entPolicyLib = new PolicyLibrary();
            _sqlcmd = new SqlCommand();
            string strXml = "<root>";
            XmlDocument folderTree = new XmlDocument();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntPolicyLib.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlcmd.CommandText = Schema.PolicyLibrary.PROC_GET_POLICY_LIBRARY;
                if (!string.IsNullOrEmpty(pEntPolicyLib.ID))
                    _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_POLICY_FOLDER_ID, pEntPolicyLib.ID);
                else
                    _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_POLICY_FOLDER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntPolicyLib.CreatedById))
                    _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_CREATED_BY_ID, pEntPolicyLib.CreatedById);
                else
                    _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_CREATED_BY_ID, null);

                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                if (!String.IsNullOrEmpty(pEntPolicyLib.ID))
                {
                    entPolicyLib = FillObject(_sqlreader, false);
                }
                if (_sqlreader.NextResult())
                {
                    while (_sqlreader.Read())
                    {
                        if (!_sqlreader.IsDBNull(0))
                        {
                            strXml += _sqlreader.GetString(0);
                        }
                    }
                    strXml += "</root>";
                    folderTree.LoadXml(strXml);
                    entPolicyLib.FolderTreeXml = folderTree;
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entPolicyLib;
        }

        public PolicyLibrary GetPolicyLibraryByIdForAssignment(PolicyLibrary pEntPolicyLib)
        {
            _sqlObject = new SQLObject();
            PolicyLibrary entPolicyLib = new PolicyLibrary();
            _sqlcmd = new SqlCommand();
            string strXml = "<root>";
            XmlDocument folderTree = new XmlDocument();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntPolicyLib.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlcmd.CommandText = Schema.PolicyLibrary.PROC_GET_POLICY_LIBRARY_FORAssignment;
                if (!string.IsNullOrEmpty(pEntPolicyLib.ID))
                    _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_POLICY_FOLDER_ID, pEntPolicyLib.ID);
                else
                    _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_POLICY_FOLDER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntPolicyLib.CreatedById))
                    _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_CREATED_BY_ID, pEntPolicyLib.CreatedById);
                else
                    _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_CREATED_BY_ID, null);

                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                if (!String.IsNullOrEmpty(pEntPolicyLib.ID))
                {
                    entPolicyLib = FillObject(_sqlreader, false);
                }
                if (_sqlreader.NextResult())
                {
                    while (_sqlreader.Read())
                    {
                        if (!_sqlreader.IsDBNull(0))
                        {
                            strXml += _sqlreader.GetString(0);
                        }
                    }
                    strXml += "</root>";
                    folderTree.LoadXml(strXml);
                    entPolicyLib.FolderTreeXml = folderTree;
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entPolicyLib;
        }
        /// <summary>
        /// To Get Policy Library Child Count.
        /// </summary>
        /// <param name="pEntAssetLib"></param>
        /// <returns>PolicyLibrary Object</returns>
        public PolicyLibrary GetPolicyLibraryChildCount(PolicyLibrary pEntPolicyLib)
        {
            _sqlObject = new SQLObject();
            PolicyLibrary entAssetLib = new PolicyLibrary();
            EntityRange entRange = new EntityRange();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntPolicyLib.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.PolicyLibrary.PROC_GET_POLICY_LIB_CHILDREN_COUNT, sqlConnection);

                _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_POLICY_FOLDER_ID, pEntPolicyLib.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                Object obj = null;
                obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
                if (obj != null)
                {
                    entRange.TotalRows = Convert.ToInt32(obj);
                }
                entAssetLib.ListRange = entRange;
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
            return entAssetLib;
        }

        /// <summary>
        ///  To Fill PolicyLibrary object properties values from reader data 
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>PolicyLibrary Object</returns>
        private PolicyLibrary FillObject(SqlDataReader pSqlReader, bool pRangeList)
        {
            PolicyLibrary entPolicyLib = new PolicyLibrary();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.PolicyLibrary.COL_POLICY_FOLDER_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicyLib.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.PolicyLibrary.COL_POLICY_FOLDER_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicyLib.PolicyFolderName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.PolicyLibrary.COL_POLICY_FOLDER_DESCRIPTION);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicyLib.PolicyFolderDescription = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.PolicyLibrary.COL_EMAIL_ID_STRING);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicyLib.EmailIDString = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.PolicyLibrary.COL_IS_VISIBLE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicyLib.IsVisible = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.PolicyLibrary.COL_PARENT_FOLDER_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicyLib.ParentFolderId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.PolicyLibrary.COL_RELATIVE_PATH);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicyLib.RelativePath = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.PolicyLibrary.COL_CLIENT_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicyLib.ClientId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicyLib.CreatedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicyLib.DateCreated = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicyLib.LastModifiedById = pSqlReader.GetString(iIndex);

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    entPolicyLib.ListRange = _entRange;
                }
            }
            return entPolicyLib;
        }

        /// <summary>
        /// To Add new Policy Library 
        /// </summary>
        /// <param name="pEntPolicyLib"></param>
        /// <returns>PolicyLibrary Object</returns>
        public PolicyLibrary AddPolicyLibrary(PolicyLibrary pEntPolicyLib)
        {
            PolicyLibrary entPolicyLib = new PolicyLibrary();
            try
            {
                if (AddFolder(ref pEntPolicyLib))
                {
                    entPolicyLib = Update(pEntPolicyLib, Schema.Common.VAL_INSERT_MODE);
                }
                else
                {
                    entPolicyLib = null;
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entPolicyLib;
        }

        /// <summary>
        /// Add folder in File System and update Relative Path
        /// </summary>
        /// <param name="pPolicyLibrary"></param>
        /// <returns>true if success</returns>
        private bool AddFolder(ref PolicyLibrary pPolicyLibrary)
        {
            FileHandler fileHandler = new FileHandler(pPolicyLibrary.ClientId);
            string strRootPath = FileHandler.CLIENTS_FOLDER_PATH + "/" + pPolicyLibrary.ClientId + "/";
            string strRelativePath = "";
            //Create Policy folder in client folder  ** CLIENT root folder check **
            if (!fileHandler.IsFolderExist(strRootPath, FileHandler.POLICY_FOLDER_PATH))
            {
                fileHandler.CreateFolder(strRootPath, FileHandler.POLICY_FOLDER_PATH);
            }
            //--
            if (!String.IsNullOrEmpty(pPolicyLibrary.ParentFolderId))
            {
                PolicyLibrary parentLib = new PolicyLibrary();
                parentLib.ID = pPolicyLibrary.ParentFolderId;
                parentLib.ClientId = pPolicyLibrary.ClientId;
                parentLib.CreatedById = pPolicyLibrary.CreatedById;
                parentLib = GetPolicyLibraryById(parentLib);
                strRootPath += parentLib.RelativePath;
                strRelativePath = parentLib.RelativePath;
            }
            if (string.IsNullOrEmpty(strRelativePath))
            {
                strRootPath += FileHandler.POLICY_FOLDER_PATH;
                strRelativePath = FileHandler.POLICY_FOLDER_PATH;
            }
            string strPhysicalFolderName = "";
            strPhysicalFolderName = pPolicyLibrary.PolicyFolderName + "_" + YPLMS.Services.IDGenerator.GetStringGUID();

            strRelativePath += "/" + strPhysicalFolderName + "/";
            fileHandler.CreateFolder(strRootPath, strPhysicalFolderName);
            pPolicyLibrary.RelativePath = strRelativePath;
            return true;
        }

        /// <summary>
        /// To Edit the Policy Library data 
        /// </summary>
        /// <param name="pEntPolicyLib"></param>
        /// <returns>PolicyLibrary Object</returns>
        public PolicyLibrary EditPolicyLibrary(PolicyLibrary pEntPolicyLib)
        {
            PolicyLibrary entPolicyLib = new PolicyLibrary();
            try
            {
                entPolicyLib = Update(pEntPolicyLib, Schema.Common.VAL_UPDATE_MODE);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entPolicyLib;
        }

        /// <summary>
        /// private method to support both Add and Edit Policy Library transactions.
        /// </summary>
        /// <param name="pEntPolicyLib"></param>
        /// <param name="pUpdateMode"></param>
        /// <returns>PolicyLibrary Object</returns>
        private PolicyLibrary Update(PolicyLibrary pEntPolicyLib, string pStrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.PolicyLibrary.PROC_UPDATE_POLICY_LIBRARY;
            _strConnString = _sqlObject.GetClientDBConnString(pEntPolicyLib.ClientId);
            if (pStrUpdateMode == Schema.Common.VAL_INSERT_MODE)
            {
                pEntPolicyLib.ID = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_POLICY_FOLDER_ID_PREFIX, Schema.Common.VAL_POLICY_FOLDER_ID_LENGTH);
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
            }
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_POLICY_FOLDER_ID, pEntPolicyLib.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pStrUpdateMode == Schema.Common.VAL_INSERT_MODE)
            {
                _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_RELATIVE_PATH, pEntPolicyLib.RelativePath);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_PARENT_FOLDER_ID, pEntPolicyLib.ParentFolderId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntPolicyLib.CreatedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntPolicyLib.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_POLICY_FOLDER_NAME, pEntPolicyLib.PolicyFolderName);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_POLICY_FOLDER_DESCRIPTION, pEntPolicyLib.PolicyFolderDescription);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_EMAIL_ID_STRING, pEntPolicyLib.EmailIDString);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_IS_VISIBLE, pEntPolicyLib.IsVisible);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntPolicyLib.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            int iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            return pEntPolicyLib;
        }

        /// <summary>
        /// Delete PolicyLibrary
        /// </summary>
        /// <param name="pEntPolicyLib"></param>
        /// <returns>PolicyLibrary Object</returns>
        public PolicyLibrary DeletePolicyLibrary(PolicyLibrary pEntPolicyLib)
        {
            _sqlObject = new SQLObject();
            FileHandler fileHandler = new FileHandler(pEntPolicyLib.ClientId);
            PolicyLibrary entPolicyLibToDelete;
            string strRootPath = FileHandler.CLIENTS_FOLDER_PATH + "/" + pEntPolicyLib.ClientId;
            entPolicyLibToDelete = GetPolicyLibraryById(pEntPolicyLib);
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.PolicyLibrary.PROC_DELETE_POLICY_LIBRARY;
            _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_POLICY_FOLDER_ID, entPolicyLibToDelete.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntPolicyLib.ClientId);
                //Delete Policy from database
                int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                if (iDelStatus > 0)
                {
                    //Delete Policy Folder from server                    
                    strRootPath += "/" + entPolicyLibToDelete.RelativePath;
                    if (fileHandler.RemoveFolder(strRootPath))
                        pEntPolicyLib = null;
                    else
                        pEntPolicyLib = null;
                }
                else
                {
                    _expCustom = new CustomException(_strPolicyAssignMsgId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, new Exception("Policy assigned to " + iDelStatus.ToString() + " Learner(s)"), false);
                    throw _expCustom;
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(YPLMS.Services.Messages.PolicyLibrary.LIBRARY_DEL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, expCommon, true);
                throw _expCustom;
            }
            return pEntPolicyLib;
        }

        /// <summary>
        /// Get All PolicyLibrary
        /// </summary>
        /// <param name="pEntPolicyLib"></param>
        /// <returns>List of PolicyLibrary Object</returns>
        public List<PolicyLibrary> GetPolicyLibraryList(PolicyLibrary pEntPolicyLib)
        {
            _sqlObject = new SQLObject();
            List<PolicyLibrary> entListPolicyLib = new List<PolicyLibrary>();
            PolicyLibrary entPolicyLibrary = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntPolicyLib.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.PolicyLibrary.PROC_GET_ALL_POLICY_LIBRARY;
                _sqlcmd.Connection = sqlConnection;
                if (!string.IsNullOrEmpty(pEntPolicyLib.ParentFolderId))
                    _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_PARENT_FOLDER_ID, pEntPolicyLib.ParentFolderId);
                else
                    _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_PARENT_FOLDER_ID, null);

                _sqlcmd.Parameters.Add(_sqlpara);

                //if (!string.IsNullOrEmpty(pEntPolicyLib.CreatedById)) //Add Comments: Bharat-26-Nov-2013
                //    _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_CREATED_BY_ID, pEntPolicyLib.CreatedById);
                //else
                //    _sqlpara = new SqlParameter(Schema.PolicyLibrary.PARA_CREATED_BY_ID, null);

                //_sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entPolicyLibrary = FillObject(_sqlreader, false);
                    entListPolicyLib.Add(entPolicyLibrary);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();

            }
            return entListPolicyLib;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntPolicyLibrary"></param>
        /// <returns></returns>
        public PolicyLibrary Get(PolicyLibrary pEntPolicyLibrary)
        {
            return GetPolicyLibraryById(pEntPolicyLibrary);
        }
        /// <summary>
        /// Update PolicyLibrary
        /// </summary>
        /// <param name="pEntPolicyLibrary"></param>
        /// <returns></returns>
        public PolicyLibrary Update(PolicyLibrary pEntPolicyLibrary)
        {
            return EditPolicyLibrary(pEntPolicyLibrary);
        }
        #endregion
    }
}
