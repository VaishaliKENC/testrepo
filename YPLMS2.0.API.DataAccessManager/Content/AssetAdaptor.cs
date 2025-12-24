using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;
using NPOI.SS.Formula.Functions;

namespace YPLMS2._0.API.DataAccessManager
{
    public class AssetAdaptor : IDataManager<Asset>, IAssetAdaptor<Asset>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = API.YPLMS.Services.Messages.Asset.ASSET_BL_ERROR;
        string _strAssetAssignMsgId = API.YPLMS.Services.Messages.Asset.ASSET_ASSIGN;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// To Get Asset details by Asset Id.
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <returns>Asset Object</returns>
        public Asset GetAssetById(Asset pEntAsset)
        {
            _sqlObject = new SQLObject();
            Asset entAsset = new Asset();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAsset.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                _sqlcmd = new SqlCommand(Schema.Asset.PROC_GET_ASSET, sqlConnection);
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_ID, pEntAsset.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAsset.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entAsset = FillObject(_sqlreader, false, _sqlObject);
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
            return entAsset;
        }

        /// <summary>
        /// To Get Asset details by Asset Id.
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <returns>Asset Object</returns>
        public Asset GetAssetRelativePathById(Asset pEntAsset)
        {
            _sqlObject = new SQLObject();
            Asset entAsset = new Asset();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAsset.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Asset.PROC_GET_ASSET_RELATIVE_PATH, sqlConnection);

                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_ID, pEntAsset.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAsset.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                Object obj = null;
                obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
                if (obj != null)
                {
                    entAsset.RelativePath = Convert.ToString(obj);
                }
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
            return entAsset;
        }


        public List<Asset> GetAssetLanguageList(Asset pEntAssets)
        {
            _sqlObject = new SQLObject();
            List<Asset> entListAssets = new List<Asset>();
            Asset entAssets = null;
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Asset.PROC_GET_ASSETS_LANGUAGES;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssets.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_ID, pEntAssets.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                if (pEntAssets.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntAssets.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntAssets.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntAssets.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entAssets = FillObject(_sqlreader, true, _sqlObject);
                    entListAssets.Add(entAssets);
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
            return entListAssets;
        }

        /// <summary>
        ///  To Fill Asset object properties values from reader data 
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>Asset Object</returns>
        private Asset FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            Asset entAsset = new Asset();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Asset.COL_ASSET_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Asset.COL_ASSET_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.ID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Language.COL_LANGUAGE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_LANGUAGE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.LanguageId = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Language.COL_LANG_ENG_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_LANG_ENG_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.LanguageName = pSqlReader.GetString(iIndex);
                    else
                        entAsset.LanguageName = "English";
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Asset.COL_ASSET_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Asset.COL_ASSET_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.AssetName = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Asset.COL_ASSET_DESCRIPTION))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Asset.COL_ASSET_DESCRIPTION);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.AssetDescription = pSqlReader.GetString(iIndex);
                    else
                        entAsset.AssetDescription = " ";
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Asset.COL_ASSET_KEYWORDS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Asset.COL_ASSET_KEYWORDS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.AssetKeywords = pSqlReader.GetString(iIndex);
                    else
                        entAsset.AssetKeywords = " ";
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Asset.COL_ASSET_FOLDER_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Asset.COL_ASSET_FOLDER_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.AssetFolderId = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Asset.COL_IS_ACTIVE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Asset.COL_IS_ACTIVE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.IsActive = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Asset.COL_ASSET_FILE_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Asset.COL_ASSET_FILE_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.AssetFileName = pSqlReader.GetString(iIndex);
                    else
                        entAsset.AssetFileName = " ";
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Asset.COL_ASSET_FILE_TYPE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Asset.COL_ASSET_FILE_TYPE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.AssetFileType = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Asset.COL_RELATIVE_PATH))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Asset.COL_RELATIVE_PATH);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.RelativePath = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Asset.COL_THUMBNAIL_IMG_PATH))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Asset.COL_THUMBNAIL_IMG_PATH);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.ThumbnailImgRelativePath = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Asset.COL_CLIENT_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Asset.COL_CLIENT_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.ClientId = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Asset.COL_IS_PRINT_CERTIFICATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Asset.COL_IS_PRINT_CERTIFICATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.IsPrintCertificate = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Asset.COL_IS_ASSIGNED))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Asset.COL_IS_ASSIGNED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.IsAssigned = Convert.ToBoolean(pSqlReader[Schema.Asset.COL_IS_ASSIGNED]);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.CreatedById = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.DateCreated = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.LastModifiedById = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.LastModifiedDate = pSqlReader.GetDateTime(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_CONT_SRVR_URL))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CONT_SRVR_URL);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.ContentServerURL = pSqlReader.GetString(iIndex);
                }

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                        if (!pSqlReader.IsDBNull(iIndex))
                            _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                        entAsset.ListRange = _entRange;
                    }

                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.CreatedByName = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.ModifiedByName = pSqlReader.GetString(iIndex);
                }

                if (string.IsNullOrEmpty(entAsset.CreatedByName))
                    entAsset.CreatedByName = "";

                if (string.IsNullOrEmpty(entAsset.ModifiedByName))
                    entAsset.ModifiedByName = "";

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.Category.COL_CATEGORYNAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Category.COL_CATEGORYNAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.CategoryName = pSqlReader.GetString(iIndex);

                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.SubCategory.COL_SUBCATEGORYNAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.SubCategory.COL_SUBCATEGORYNAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.SubCategoryName = pSqlReader.GetString(iIndex);

                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Asset.COL_IS_DOWNLOAD))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Asset.COL_IS_DOWNLOAD);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.IsDownload = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Asset.COL_ASSET_FILENAME_LINK))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Asset.COL_ASSET_FILENAME_LINK);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAsset.AssetFileNameLink = pSqlReader.GetString(iIndex);
                }

            }
            return entAsset;
        }

        /// <summary>
        /// To Add new Asset
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <returns>Asset Object</returns>
        public Asset AddAsset(Asset pEntAsset)
        {
            Asset entAsset = new Asset();
            try
            {
                if (AddAssetFile(ref pEntAsset))
                    entAsset = Update(pEntAsset, Schema.Common.VAL_INSERT_MODE);
                else
                    entAsset = null;
            }
            catch (Exception expCommon)
            {
                entAsset = null;
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entAsset;
        }

        /// <summary>
        /// Add Asset File on Relative Path
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <returns>true if success</returns>
        private bool AddAssetFile(ref Asset pEntAsset)
        {
            try
            {
                AssetLibraryAdaptor entAssetLibAdp = new AssetLibraryAdaptor();
                FileHandler fileHandler = new FileHandler(pEntAsset.ClientId);
                string strRootPath = FileHandler.CLIENTS_FOLDER_PATH + "/" + pEntAsset.ClientId + "/";
                string strFileName = "";
                AssetLibrary entAssetLibrary = new AssetLibrary();
                entAssetLibrary.ID = pEntAsset.AssetFolderId;
                entAssetLibrary.ClientId = pEntAsset.ClientId;
                entAssetLibrary.CreatedById = pEntAsset.CreatedById;
                entAssetLibrary = entAssetLibAdp.GetAssetLibraryById(entAssetLibrary);
                if (!string.IsNullOrEmpty(entAssetLibrary.RelativePath))
                {
                    strFileName = pEntAsset.AssetFileName;
                    strRootPath += entAssetLibrary.RelativePath;
                }
                fileHandler.Uploadfile(strRootPath, strFileName, pEntAsset.AssetFile);

                pEntAsset.RelativePath = entAssetLibrary.RelativePath + strFileName;

                pEntAsset.AssetFileType = Path.GetExtension(strFileName);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Update Asset File
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <returns></returns>
        private bool UpdateAssetFile(ref Asset pEntAsset)
        {
            AssetLibraryAdaptor entAssetLibAdp = new AssetLibraryAdaptor();
            FileHandler fileHandler = new FileHandler(pEntAsset.ClientId);
            string strRootPath = FileHandler.CLIENTS_FOLDER_PATH + "/" + pEntAsset.ClientId + "/";
            string strFileName = "";
            if (!string.IsNullOrEmpty(pEntAsset.RelativePath))
            {
                strRootPath += pEntAsset.RelativePath;
                strFileName = pEntAsset.AssetFileName;
                strRootPath = strRootPath.Substring(0, strRootPath.LastIndexOf("/") + 1);
                fileHandler.Uploadfile(strRootPath, strFileName, pEntAsset.AssetFile);
                pEntAsset.RelativePath = pEntAsset.RelativePath;
                pEntAsset.AssetFileType = Path.GetExtension(strFileName);
            }
            return true;
        }

        /// <summary>
        /// private method to Delete Asset File on server
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <returns>true if success</returns>
        private bool DeleteAssetFile(Asset pEntAsset)
        {
            bool bValue = false;
            FileHandler fileHandler = new FileHandler(pEntAsset.ClientId);
            string strRootPath = FileHandler.CLIENTS_FOLDER_PATH + "/" + pEntAsset.ClientId + "/";

            AssetLibrary entAssetLibrary = new AssetLibrary();
            AssetLibraryAdaptor entAssetLibAdp = new AssetLibraryAdaptor();
            entAssetLibrary.ID = pEntAsset.AssetFolderId;
            entAssetLibrary.ClientId = pEntAsset.ClientId;
            //entAssetLibrary.CreatedById = pEntAsset.CreatedById;
            entAssetLibrary = entAssetLibAdp.GetAssetLibraryById(entAssetLibrary);

            pEntAsset.RelativePath = entAssetLibrary.RelativePath + pEntAsset.AssetFileName;

            if (!string.IsNullOrEmpty(pEntAsset.RelativePath))
            {
                strRootPath += pEntAsset.RelativePath;
                if (fileHandler.DeleteFileOnServer(strRootPath))
                    bValue = true;
                else
                    bValue = false;
            }
            fileHandler.Dispose();
            return bValue;
        }

        /// <summary>
        /// To Edit the Asset data 
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <returns>Asset Object</returns>
        public Asset EditAsset(Asset pEntAsset)
        {
            Asset entAsset = new Asset();
            Asset entAssetNew = new Asset();
            //Get the Relative Path
            entAssetNew = GetAssetById(pEntAsset);
            pEntAsset.RelativePath = entAssetNew.RelativePath;
            string strNewRelPath = pEntAsset.RelativePath;
            if (!string.IsNullOrEmpty(strNewRelPath))
            {
                strNewRelPath = strNewRelPath.Substring(0, strNewRelPath.LastIndexOf("/") + 1);
            }

            strNewRelPath = strNewRelPath + pEntAsset.AssetFileName;
            try
            {
                //Check that New File has been uploaded
                if (pEntAsset.AssetFile != null && pEntAsset.AssetFile.Length > 0)
                {
                    //Delete previous existing asset from server
                    //if (DeleteAssetFile(pEntAsset))
                    //{
                    //    pEntAsset.RelativePath = strNewRelPath;
                    //    //Add New Asset 
                    //    if (UpdateAssetFile(ref pEntAsset))
                    //    {
                    //        //Update information in DataBase
                    //        entAsset = Update(pEntAsset, Schema.Common.VAL_UPDATE_MODE);
                    //    }
                    //}

                    if (pEntAsset.LanguageId == Language.SYSTEM_DEFAULT_LANG_ID)
                    {
                        pEntAsset.RelativePath = strNewRelPath;
                    }

                    //Add New Asset 
                    if (UpdateAssetFile(ref pEntAsset))
                    {
                        //Update information in DataBase
                        entAsset = Update(pEntAsset, Schema.Common.VAL_UPDATE_MODE);
                    }

                }
                else
                {
                    //Update information in DataBase
                    entAsset = Update(pEntAsset, Schema.Common.VAL_UPDATE_MODE);
                }
            }
            catch (Exception expCommon)
            {
                entAsset = null;
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entAsset;
        }

        /// <summary>
        /// private method to support both Add and Edit Asset transactions.
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <param name="pUpdateMode"></param>
        /// <returns>Asset Object</returns>
        private Asset Update(Asset pEntAsset, string pStrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Asset.PROC_UPDATE_ASSET;
            _strConnString = _sqlObject.GetClientDBConnString(pEntAsset.ClientId);
            if (pStrUpdateMode == Schema.Common.VAL_INSERT_MODE)
            {
                pEntAsset.ID = API.YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_ASSET_ID_PREFIX, Schema.Common.VAL_ASSET_ID_LENGTH);
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
            }
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);

            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_ID, pEntAsset.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            //_sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_NAME, pEntAsset.AssetName);
            //_sqlcmd.Parameters.Add(_sqlpara);

            //if (!String.IsNullOrEmpty(pEntAsset.AssetDescription))
            //    _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_DESCRIPTION, pEntAsset.AssetDescription);
            //else
            //    _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_DESCRIPTION, null);
            //_sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntAsset.AssetKeywords))
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_KEYWORDS, pEntAsset.AssetKeywords);
            else
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_KEYWORDS, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntAsset.AssetFolderId))
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_FOLDER_ID, pEntAsset.AssetFolderId);
            else
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_FOLDER_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntAsset.IsActive != null)
                _sqlpara = new SqlParameter(Schema.Asset.PARA_IS_ACTIVE, pEntAsset.IsActive);
            else
                _sqlpara = new SqlParameter(Schema.Asset.PARA_IS_ACTIVE, DBNull.Value);
            _sqlcmd.Parameters.Add(_sqlpara);

            //if (!String.IsNullOrEmpty(pEntAsset.AssetFileName))
            //    _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_FILE_NAME, pEntAsset.AssetFileName);
            //else
            //    _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_FILE_NAME, null);
            //_sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntAsset.AssetFileType))
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_FILE_TYPE, pEntAsset.AssetFileType);
            else
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_FILE_TYPE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntAsset.RelativePath))
                _sqlpara = new SqlParameter(Schema.Asset.PARA_RELATIVE_PATH, pEntAsset.RelativePath);
            else
                _sqlpara = new SqlParameter(Schema.Asset.PARA_RELATIVE_PATH, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Asset.PARA_IS_DOWNLOAD, pEntAsset.IsDownload);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Asset.PARA_IS_PRINT_CERTIFICATE, pEntAsset.IsPrintCertificate);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntAsset.AssetFileNameLink))
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_FILENAME_LINK, pEntAsset.AssetFileNameLink);
            else
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_FILENAME_LINK, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntAsset.ClientId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntAsset.CreatedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntAsset.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntAsset.ThumbnailImgRelativePath))
                _sqlpara = new SqlParameter(Schema.Asset.PARA_THUMBNAILIMGRELATIVEPATH, pEntAsset.ThumbnailImgRelativePath);
            else
                _sqlpara = new SqlParameter(Schema.Asset.PARA_THUMBNAILIMGRELATIVEPATH, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            int iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            pEntAsset = UpdateLanguage(pEntAsset);

            return pEntAsset;
        }

        /// <summary>
        /// Delete Asset From Server and DataBase
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <returns>Asset Object</returns>
        public Asset DeleteAsset(Asset pEntAsset)
        {
            _sqlObject = new SQLObject();
            Asset entAsset = new Asset();
            //Get the Relative Path
            entAsset = GetAssetById(pEntAsset);
            if (entAsset != null)
            {
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Asset.PROC_DELETE_ASSET;
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_ID, pEntAsset.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                try
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntAsset.ClientId);
                    //Delete Asset from database
                    int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                    if (iDelStatus > 0)
                    {
                        //Delete Asset from server

                        //---------Not Deleting file from server

                        //if (DeleteAssetFile(entAsset))
                        //{
                        //    pEntAsset = null;
                        //}
                        //else
                        //    throw new Exception("Unable to delete asset file");
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
                    _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                    throw _expCustom;
                }
            }
            else
            {
                throw new Exception("Unable to delete asset file");
            }
            return pEntAsset;
        }

        /// <summary>
        /// To Get Asset details by Asset Id.
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <returns>Asset Object</returns>
        public Asset SearchRelativePath(Asset pEntAsset)
        {
            _sqlObject = new SQLObject();
            Asset entAsset = new Asset();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAsset.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Asset.PROC_SEARCH_ASSET_RELATIVE_PATH, sqlConnection);

                _sqlpara = new SqlParameter(Schema.Asset.PARA_RELATIVE_PATH, pEntAsset.RelativePath);
                _sqlcmd.Parameters.Add(_sqlpara);

                Object obj = null;
                obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
                if (obj != null)
                {
                    entAsset.RelativePath = Convert.ToString(obj);
                }
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
            return entAsset;
        }

        public Asset DeleteAssetLanguage(Asset pEntAsset)
        {
            _sqlObject = new SQLObject();
            Asset entAsset = new Asset();
            //Get the Relative Path
            entAsset = GetAssetById(pEntAsset);
            if (entAsset != null)
            {
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Asset.PROC_DELETE_ASSETLANGUAGE;
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_ID, pEntAsset.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAsset.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                try
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntAsset.ClientId);
                    //Delete Asset from database
                    int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                    if (iDelStatus > 0)
                    {
                        //Delete Asset from server
                        if (DeleteAssetFile(entAsset))
                        {
                            pEntAsset = null;
                        }
                        else
                            throw new Exception("Unable to delete asset file");
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
                    _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                    throw _expCustom;
                }
            }
            else
            {
                throw new Exception("Unable to delete asset file");
            }
            return pEntAsset;
        }

        /// <summary>
        /// Get All Asset
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <returns>List of Asset Object</returns>
        public List<Asset> GetAssetList(Asset pEntAsset)
        {
            _sqlObject = new SQLObject();
            List<Asset> entListAsset = new List<Asset>();
            Asset entAsset = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAsset.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                _sqlcmd = new SqlCommand(Schema.Asset.PROC_GET_ALL_ASSET, sqlConnection);

                if (!string.IsNullOrEmpty(pEntAsset.AssetFolderId))
                    _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_FOLDER_ID, pEntAsset.AssetFolderId);
                else
                    _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_FOLDER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntAsset.IsActive != null)
                    _sqlpara = new SqlParameter(Schema.Asset.PARA_IS_ACTIVE, pEntAsset.IsActive);
                else
                    _sqlpara = new SqlParameter(Schema.Asset.COL_IS_ACTIVE, DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntAsset.CategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.Category.PARA_CATEGORYID, pEntAsset.CategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntAsset.SubCategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.SubCategory.PARA_SUBCATEGORYID, pEntAsset.SubCategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntAsset.AssetFileName))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntAsset.AssetFileName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntAsset.Keyword != null)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntAsset.Keyword);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);


                if (pEntAsset.ListRange != null)
                {
                    if (pEntAsset.ListRange.PageIndex > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntAsset.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntAsset.ListRange.PageSize > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntAsset.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntAsset.ListRange.SortExpression))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntAsset.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntAsset.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNED_BY_ID, pEntAsset.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entAsset = FillObject(_sqlreader, true, _sqlObject);
                    entListAsset.Add(entAsset);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed)
                    _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entListAsset;
        }

        public List<Asset> GetAssetListForAssignments(Asset pEntAsset)
        {
            _sqlObject = new SQLObject();
            List<Asset> entListAsset = new List<Asset>();
            Asset entAsset = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAsset.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                _sqlcmd = new SqlCommand(Schema.Asset.PROC_GET_ALL_ASSET_FORASSIGNMENTS, sqlConnection);

                if (!string.IsNullOrEmpty(pEntAsset.AssetFolderId))
                    _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_FOLDER_ID, pEntAsset.AssetFolderId);
                else
                    _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_FOLDER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntAsset.IsActive != null)
                    _sqlpara = new SqlParameter(Schema.Asset.PARA_IS_ACTIVE, pEntAsset.IsActive);
                else
                    _sqlpara = new SqlParameter(Schema.Asset.COL_IS_ACTIVE, DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntAsset.CategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.Category.PARA_CATEGORYID, pEntAsset.CategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntAsset.SubCategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.SubCategory.PARA_SUBCATEGORYID, pEntAsset.SubCategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntAsset.AssetFileName))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntAsset.AssetFileName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntAsset.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNED_BY_ID, pEntAsset.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntAsset.ListRange != null)
                {
                    if (pEntAsset.ListRange.PageIndex > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntAsset.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntAsset.ListRange.PageSize > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntAsset.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntAsset.ListRange.SortExpression))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntAsset.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entAsset = FillObject(_sqlreader, true, _sqlObject);
                    entListAsset.Add(entAsset);
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
            return entListAsset;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <returns></returns>
        public Asset Get(Asset pEntAsset)
        {
            return GetAssetById(pEntAsset);
        }

        public List<Asset> GetAllProgramLanguage(Asset pEntAssetMaster)
        {
            return GetAssetLanguageList(pEntAssetMaster);
        }

        /// <summary>
        /// Update Asset
        /// </summary>
        /// <param name="pEntAsset"></param>
        /// <returns></returns>
        public Asset Update(Asset pEntAsset)
        {
            return EditAsset(pEntAsset);
        }

        public Asset UpdateLanguage(Asset pEntAssetMaster)
        {
            return UpdateAssetMasterLanguage(pEntAssetMaster, Asset.Method.UpdateLanguage);
        }

        private Asset UpdateAssetMasterLanguage(Asset pEntAssetMaster, Asset.Method pMethod)
        {
            int iRowsAffected = 0;
            SqlConnection sqlConn = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Asset.PROC_UPDATE_ASSET_LANGUAGE;
            _sqlObject = new SQLObject();


            if (!String.IsNullOrEmpty(pEntAssetMaster.LanguageId))
                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAssetMaster.LanguageId);
            else
                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntAssetMaster.AssetName))
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_NAME, pEntAssetMaster.AssetName);
            else
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_NAME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntAssetMaster.AssetDescription))
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_DESCRIPTION, pEntAssetMaster.AssetDescription);
            else
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_DESCRIPTION, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntAssetMaster.AssetFileName))
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_FILE_NAME, pEntAssetMaster.AssetFileName);
            else
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_FILE_NAME, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            try
            {
                if (pMethod == Asset.Method.Update)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else if (pMethod == Asset.Method.Add)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    pEntAssetMaster.ID = YPLMS.Services.IDGenerator.GetStringGUIDWithPrefix(Schema.Common.VAL_ASSET_ID_PREFIX);
                }
                else if (pMethod == Asset.Method.UpdateLanguage)
                {

                }
                _sqlpara = new SqlParameter(Schema.Asset.PARA_ASSET_ID, pEntAssetMaster.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetClientDBConnString(pEntAssetMaster.ClientId);
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
            return pEntAssetMaster;
        }

        #endregion
    }
}
