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
using NPOI.SS.Formula.Functions;

namespace YPLMS2._0.API.DataAccessManager
{
    public class AssetLibraryAdaptor : IDataManager<AssetLibrary>, IAssetLibraryAdaptor<AssetLibrary>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.AssetLibrary.ASSET_LIB_BL_ERROR;
        string _strAssetAssignMsgId = YPLMS.Services.Messages.AssetLibrary.ASSET_LIBRARY_ASSIGN;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// To Get Asset Library details by AssetLibrary Id.
        /// </summary>
        /// <param name="pEntAssetLib"></param>
        /// <returns>AssetLibrary Object</returns>
        public AssetLibrary GetAssetLibraryById(AssetLibrary pEntAssetLib)
        {
            _sqlObject = new SQLObject();
            AssetLibrary entAssetLib = new AssetLibrary();
            string strXml = "<root>";
            XmlDocument folderTree = new XmlDocument();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssetLib.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.AssetLibrary.PROC_GET_ASSET_LIBRARY, sqlConnection);

                if (!string.IsNullOrEmpty(pEntAssetLib.ID))
                {
                    _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_ASSET_FOLDER_ID, pEntAssetLib.ID);
                }
                else
                    _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_ASSET_FOLDER_ID, null);

                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntAssetLib.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_CREATED_BY_ID, pEntAssetLib.CreatedById);
                }
                else
                    _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_CREATED_BY_ID, null);

                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                if (!String.IsNullOrEmpty(pEntAssetLib.ID))
                {
                    entAssetLib = FillObject(_sqlreader, false);
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
                    entAssetLib.FolderTreeXml = folderTree;
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
            return entAssetLib;
        }

        public AssetLibrary GetAssetLibraryById_ForAssignment(AssetLibrary pEntAssetLib)
        {
            _sqlObject = new SQLObject();
            AssetLibrary entAssetLib = new AssetLibrary();
            string strXml = "<root>";
            XmlDocument folderTree = new XmlDocument();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssetLib.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.AssetLibrary.PROC_GET_ASSET_LIBRARY_ForAssignment, sqlConnection);

                if (!string.IsNullOrEmpty(pEntAssetLib.ID))
                {
                    _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_ASSET_FOLDER_ID, pEntAssetLib.ID);
                }
                else
                    _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_ASSET_FOLDER_ID, null);

                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntAssetLib.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_CREATED_BY_ID, pEntAssetLib.CreatedById);
                }
                else
                    _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_CREATED_BY_ID, null);

                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                if (!String.IsNullOrEmpty(pEntAssetLib.ID))
                {
                    entAssetLib = FillObject(_sqlreader, false);
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
                    entAssetLib.FolderTreeXml = folderTree;
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
            return entAssetLib;
        }

        /// <summary>
        /// To Get Asset Library Child Count.
        /// </summary>
        /// <param name="pEntAssetLib"></param>
        /// <returns>AssetLibrary Object</returns>
        public AssetLibrary GetAssetLibraryChildCount(AssetLibrary pEntAssetLib)
        {
            _sqlObject = new SQLObject();
            AssetLibrary entAssetLib = new AssetLibrary();
            EntityRange entRange = new EntityRange();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssetLib.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.AssetLibrary.PROC_GET_ASSET_LIB_CHILDREN_COUNT, sqlConnection);

                _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_ASSET_FOLDER_ID, pEntAssetLib.ID);
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
        ///  To Fill AssetLibrary object properties values from reader data 
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>AssetLibrary Object</returns>
        private AssetLibrary FillObject(SqlDataReader pSqlReader, bool pRangeList)
        {
            AssetLibrary entAssetLib = new AssetLibrary();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.AssetLibrary.COL_ASSET_FOLDER_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssetLib.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.AssetLibrary.COL_ASSET_FOLDER_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssetLib.AssetFolderName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.AssetLibrary.COL_EMAIL_ID_STRING);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssetLib.EmailIDString = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.AssetLibrary.COL_ASSET_FOLDER_DESC);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssetLib.AssetFolderDescription = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.AssetLibrary.COL_IS_VISIBLE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssetLib.IsVisible = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.AssetLibrary.COL_PARENT_FOLDER_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssetLib.ParentFolderId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.AssetLibrary.COL_RELATIVE_PATH);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssetLib.RelativePath = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.AssetLibrary.COL_CLIENT_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssetLib.ClientId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssetLib.CreatedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssetLib.DateCreated = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssetLib.LastModifiedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssetLib.LastModifiedDate = pSqlReader.GetDateTime(iIndex);

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    entAssetLib.ListRange = _entRange;
                }
            }
            return entAssetLib;
        }

        /// <summary>
        /// To Add new Asset Library 
        /// </summary>
        /// <param name="pEntAssetLib"></param>
        /// <returns>AssetLibrary Object</returns>
        public AssetLibrary AddAssetLibrary(AssetLibrary pEntAssetLib)
        {
            AssetLibrary entAssetLib = new AssetLibrary();
            try
            {
                if (AddFolder(ref pEntAssetLib))
                {
                    entAssetLib = Update(pEntAssetLib, Schema.Common.VAL_INSERT_MODE);
                }
                else
                {
                    entAssetLib = null;
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entAssetLib;
        }

        /// <summary>
        /// Add folder in File System and update Relative Path
        /// </summary>
        /// <param name="pAssetLibrary"></param>
        /// <returns>true if success</returns>
        private bool AddFolder(ref AssetLibrary pAssetLibrary)
        {
            FileHandler fileHandler = new FileHandler(pAssetLibrary.ClientId);
            string strRootPath = FileHandler.CLIENTS_FOLDER_PATH + "/" + pAssetLibrary.ClientId + "/";
            string strRelativePath = "";
            try
            {
                //--Create Asset folder in client folder ** CLIENT root folder check **
                if (!fileHandler.IsFolderExist(strRootPath, FileHandler.ASSET_FOLDER_PATH))
                {
                    fileHandler.CreateFolder(strRootPath, FileHandler.ASSET_FOLDER_PATH);
                }
                //--
                if (!String.IsNullOrEmpty(pAssetLibrary.ParentFolderId))
                {
                    AssetLibrary parentLib = new AssetLibrary();
                    parentLib.ID = pAssetLibrary.ParentFolderId;
                    parentLib.ClientId = pAssetLibrary.ClientId;
                    parentLib.CreatedById = pAssetLibrary.CreatedById;
                    parentLib = GetAssetLibraryById(parentLib);
                    strRootPath += parentLib.RelativePath;
                    strRelativePath = parentLib.RelativePath;
                }
                if (string.IsNullOrEmpty(strRelativePath))
                {
                    strRootPath += FileHandler.ASSET_FOLDER_PATH;
                    strRelativePath = FileHandler.ASSET_FOLDER_PATH;
                }

                string strPhysicalFolderName = "";
                strPhysicalFolderName = pAssetLibrary.AssetFolderName + "_" + YPLMS.Services.IDGenerator.GetStringGUID();

                strRelativePath += "/" + strPhysicalFolderName + "/";
                bool bReturn = fileHandler.CreateFolder(strRootPath, strPhysicalFolderName);
                if (!bReturn)
                {
                    return false;
                }
                pAssetLibrary.RelativePath = strRelativePath;
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// To Edit the Asset Library data 
        /// </summary>
        /// <param name="pEntAssetLib"></param>
        /// <returns>AssetLibrary Object</returns>
        public AssetLibrary EditAssetLibrary(AssetLibrary pEntAssetLib)
        {
            AssetLibrary entAssetLib = new AssetLibrary();
            try
            {
                entAssetLib = Update(pEntAssetLib, Schema.Common.VAL_UPDATE_MODE);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entAssetLib;
        }

        /// <summary>
        /// private method to support both Add and Edit Asset Library transactions.
        /// </summary>
        /// <param name="pEntAssetLib"></param>
        /// <param name="pUpdateMode"></param>
        /// <returns>AssetLibrary Object</returns>
        private AssetLibrary Update(AssetLibrary pEntAssetLib, string pStrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.AssetLibrary.PROC_UPDATE_ASSET_LIBRARY;
            _strConnString = _sqlObject.GetClientDBConnString(pEntAssetLib.ClientId);
            if (pStrUpdateMode == Schema.Common.VAL_INSERT_MODE)
            {
                pEntAssetLib.ID = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_ASSET_FOLDER_ID_PREFIX, Schema.Common.VAL_ASSET_FOLDER_ID_LENGTH);
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
            }
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);

            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_ASSET_FOLDER_ID, pEntAssetLib.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pStrUpdateMode == Schema.Common.VAL_INSERT_MODE)
            {
                _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_RELATIVE_PATH, pEntAssetLib.RelativePath);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_PARENT_FOLDER_ID, pEntAssetLib.ParentFolderId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntAssetLib.CreatedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntAssetLib.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_ASSET_FOLDER_NAME, pEntAssetLib.AssetFolderName);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_ASSET_FOLDER_DESC, pEntAssetLib.AssetFolderDescription);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_EMAIL_ID_STRING, pEntAssetLib.EmailIDString);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_IS_VISIBLE, pEntAssetLib.IsVisible);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntAssetLib.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            int iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            return pEntAssetLib;
        }

        /// <summary>
        /// Delete AssetLibrary
        /// </summary>
        /// <param name="pEntAssetLib"></param>
        /// <returns>AssetLibrary Object</returns>
        public AssetLibrary DeleteAssetLibrary(AssetLibrary pEntAssetLib)
        {
            _sqlObject = new SQLObject();
            FileHandler fileHandler = new FileHandler(pEntAssetLib.ClientId);
            AssetLibrary entAssetLibToDelete;
            string strRootPath = FileHandler.CLIENTS_FOLDER_PATH + "/" + pEntAssetLib.ClientId;
            entAssetLibToDelete = GetAssetLibraryById(pEntAssetLib);
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.AssetLibrary.PROC_DELETE_CHILD_ASSET_LIBRARY;
            _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_ASSET_FOLDER_ID, entAssetLibToDelete.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssetLib.ClientId);
                //Delete Asset from database
                int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                if (iDelStatus > 0)
                {
                    //Delete Asset Folder from server
                    strRootPath += "/" + entAssetLibToDelete.RelativePath;
                    if (fileHandler.RemoveFolder(strRootPath))
                        pEntAssetLib = null;
                    else
                        pEntAssetLib = null;
                }
                else
                {
                    Exception exc = null;
                    _expCustom = new CustomException(_strAssetAssignMsgId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, exc, false);
                    throw _expCustom;
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(YPLMS.Services.Messages.AssetLibrary.ASSET_LIB_DEL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, expCommon, true);
                throw _expCustom;
            }
            return pEntAssetLib;
        }

        /// <summary>
        /// Get All AssetLibrary
        /// </summary>
        /// <param name="pEntAssetLib"></param>
        /// <returns>List of AssetLibrary Object</returns>
        public List<AssetLibrary> GetAssetLibraryList(AssetLibrary pEntAssetLib)
        {
            _sqlObject = new SQLObject();
            List<AssetLibrary> entListAssetLib = new List<AssetLibrary>();
            AssetLibrary entAssetLibrary = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssetLib.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.AssetLibrary.PROC_GET_ALL_ASSET_LIBRARY, sqlConnection);
                if (!string.IsNullOrEmpty(pEntAssetLib.ParentFolderId))
                    _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_PARENT_FOLDER_ID, pEntAssetLib.ParentFolderId);
                else
                    _sqlpara = new SqlParameter(Schema.AssetLibrary.PARA_PARENT_FOLDER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entAssetLibrary = FillObject(_sqlreader, false);
                    entListAssetLib.Add(entAssetLibrary);
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
            return entListAssetLib;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntAssetLibrary"></param>
        /// <returns></returns>
        public AssetLibrary Get(AssetLibrary pEntAssetLibrary)
        {
            return GetAssetLibraryById(pEntAssetLibrary);
        }
        /// <summary>
        /// Update AssetLibrary
        /// </summary>
        /// <param name="pEntAssetLibrary"></param>
        /// <returns></returns>
        public AssetLibrary Update(AssetLibrary pEntAssetLibrary)
        {
            return EditAssetLibrary(pEntAssetLibrary);
        }
        #endregion
    }
}
