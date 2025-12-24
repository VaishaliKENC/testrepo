using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{

    /// <summary>
    /// class Policy Adaptor
    /// </summary>
    public class PolicyAdaptor : IDataManager<Policy>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.Policy.POLICY_BL_ERROR;
        string _strMessageId1 = YPLMS.Services.Messages.PolicyLibrary.FOLDER_DELETE_FAIL;
        string _strPolicyAssignMsgId = YPLMS.Services.Messages.Policy.POLICY_ASSIGN;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// To Get Policy details by Policy Id.
        /// </summary>
        /// <param name="pEntPolicy"></param>
        /// <returns>Policy Object</returns>
        public Policy GetPolicyById(Policy pEntPolicy)
        {
            _sqlObject = new SQLObject();
            Policy entPolicy = new Policy();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntPolicy.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                _sqlcmd = new SqlCommand(Schema.Policy.PROC_GET_POLICY, sqlConnection);
                _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_ID, pEntPolicy.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntPolicy.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entPolicy = FillObject(_sqlreader, false, _sqlObject);
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
            return entPolicy;
        }

        /// <summary>
        /// To Get Policy details by Policy Id.
        /// </summary>
        /// <param name="pEntPolicy"></param>
        /// <returns>Policy Object</returns>
        public Policy GetPolicyByIdForLearner(Policy pEntPolicy)
        {
            _sqlObject = new SQLObject();
            Policy entPolicy = new Policy();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntPolicy.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Policy.PROC_GET_POLICY_FOR_LEARNER, sqlConnection);
                _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_ID, pEntPolicy.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entPolicy = FillObjectForLEarner(_sqlreader, false, _sqlObject);
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
            return entPolicy;
        }

        public List<Policy> GetPolicyLanguageList(Policy pEntPolicy)
        {
            _sqlObject = new SQLObject();
            List<Policy> entListAssets = new List<Policy>();
            Policy entPolicy = null;
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Policy.PROC_GET_POLICY_LANGUAGES;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntPolicy.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_ID, pEntPolicy.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                if (pEntPolicy.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntPolicy.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntPolicy.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntPolicy.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entPolicy = FillObject(_sqlreader, true, _sqlObject);
                    entListAssets.Add(entPolicy);
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


        public List<Policy> GetPolicyList(Policy pEntPolicy)
        {
            _sqlObject = new SQLObject();
            List<Policy> entListPolicy = new List<Policy>();
            Policy entPolicy = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntPolicy.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Policy.PROC_GET_ALL_POLICY, sqlConnection);

                if (!string.IsNullOrEmpty(pEntPolicy.PolicyFolderId))
                    _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_FOLDER_ID, pEntPolicy.PolicyFolderId);
                else
                    _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_FOLDER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntPolicy.IsActive != null)
                    _sqlpara = new SqlParameter(Schema.Policy.PARA_IS_ACTIVE, pEntPolicy.IsActive);
                else
                    _sqlpara = new SqlParameter(Schema.Policy.COL_IS_ACTIVE, DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntPolicy.CategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.Category.PARA_CATEGORYID, pEntPolicy.CategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntPolicy.SubCategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.SubCategory.PARA_SUBCATEGORYID, pEntPolicy.SubCategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntPolicy.PolicyName))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntPolicy.PolicyName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }


                if (pEntPolicy.ListRange != null)
                {
                    if (pEntPolicy.ListRange.PageIndex > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntPolicy.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntPolicy.ListRange.PageSize > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntPolicy.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntPolicy.ListRange.SortExpression))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntPolicy.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntPolicy.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNED_BY_ID, pEntPolicy.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entPolicy = FillObject(_sqlreader, true, _sqlObject);
                    entListPolicy.Add(entPolicy);
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
            return entListPolicy;
        }

        public List<Policy> GetPolicyListForAssignment(Policy pEntPolicy)
        {
            _sqlObject = new SQLObject();
            List<Policy> entListPolicy = new List<Policy>();
            Policy entPolicy = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntPolicy.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Policy.PROC_GET_ALL_POLICY_ForAssignment, sqlConnection);

                if (!string.IsNullOrEmpty(pEntPolicy.PolicyFolderId))
                    _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_FOLDER_ID, pEntPolicy.PolicyFolderId);
                else
                    _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_FOLDER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntPolicy.IsActive != null)
                    _sqlpara = new SqlParameter(Schema.Policy.PARA_IS_ACTIVE, pEntPolicy.IsActive);
                else
                    _sqlpara = new SqlParameter(Schema.Policy.COL_IS_ACTIVE, DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntPolicy.CategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.Category.PARA_CATEGORYID, pEntPolicy.CategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntPolicy.SubCategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.SubCategory.PARA_SUBCATEGORYID, pEntPolicy.SubCategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntPolicy.PolicyName))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntPolicy.PolicyName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntPolicy.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNED_BY_ID, pEntPolicy.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntPolicy.ListRange != null)
                {
                    if (pEntPolicy.ListRange.PageIndex > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntPolicy.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntPolicy.ListRange.PageSize > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntPolicy.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntPolicy.ListRange.SortExpression))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntPolicy.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entPolicy = FillObject(_sqlreader, true, _sqlObject);
                    entListPolicy.Add(entPolicy);
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
            return entListPolicy;
        }
        /// <summary>
        ///  To Fill Policy object properties values from reader data 
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>Policy Object</returns>
        private Policy FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            Policy entPolicy = new Policy();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Policy.COL_POLICY_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_POLICY_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.ID = pSqlReader.GetString(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Language.COL_LANGUAGE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_LANGUAGE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.LanguageId = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Language.COL_LANG_ENG_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_LANG_ENG_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.LanguageName = pSqlReader.GetString(iIndex);
                    else
                        entPolicy.LanguageName = "English";
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Policy.COL_POLICY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_POLICY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.PolicyName = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Policy.COL_POLICY_DESCRIPTION))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_POLICY_DESCRIPTION);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.PolicyDescription = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Policy.COL_POLICY_KEYWORDS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_POLICY_KEYWORDS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.PolicyKeywords = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Policy.COL_POLICY_FOLDER_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_POLICY_FOLDER_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.PolicyFolderId = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Policy.COL_IS_ACTIVE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_IS_ACTIVE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.IsActive = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Policy.COL_POLICY_FILE_NAME_LINK))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_POLICY_FILE_NAME_LINK);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.PolicyFileNameLink = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Policy.COL_POLICY_FILE_TYPE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_POLICY_FILE_TYPE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.PolicyFileType = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Policy.COL_IS_LINK))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_IS_LINK);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.IsLink = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Policy.COL_IS_SECURED))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_IS_SECURED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.IsSecured = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Policy.COL_RELATIVE_PATH))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_RELATIVE_PATH);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.RelativePath = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Policy.COL_CLIENT_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_CLIENT_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.ClientId = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Policy.COL_IS_PRINT_CERTIFICATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_IS_PRINT_CERTIFICATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.IsPrintCertificate = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Asset.COL_IS_ASSIGNED))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Asset.COL_IS_ASSIGNED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.IsAssigned = Convert.ToBoolean(pSqlReader[Schema.Asset.COL_IS_ASSIGNED]);
                }

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicy.CreatedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicy.DateCreated = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicy.LastModifiedById = pSqlReader.GetString(iIndex);

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    entPolicy.ListRange = _entRange;
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.LastModifiedDate = pSqlReader.GetDateTime(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.CreatedByName = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.ModifiedByName = pSqlReader.GetString(iIndex);
                }

                if (string.IsNullOrEmpty(entPolicy.CreatedByName))
                    entPolicy.CreatedByName = "";

                if (string.IsNullOrEmpty(entPolicy.ModifiedByName))
                    entPolicy.ModifiedByName = "";


                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.Category.COL_CATEGORYNAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Category.COL_CATEGORYNAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.CategoryName = pSqlReader.GetString(iIndex);

                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.SubCategory.COL_SUBCATEGORYNAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.SubCategory.COL_SUBCATEGORYNAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entPolicy.SubCategoryName = pSqlReader.GetString(iIndex);

                }
            }
            return entPolicy;
        }

        ///// <summary>
        /////  To Fill Policy object properties values from reader data 
        ///// </summary>
        ///// <param name="pReader"></param>
        ///// <returns>Policy Object</returns>
        //private Policy FillObjectPolicyPathDetails(SqlDataReader pSqlReader, SQLObject pSqlObject)
        //{
        //    Policy entPolicy = new Policy();
        //    int iIndex;
        //    if (pSqlReader.HasRows)
        //    {                
        //        iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_IS_LINK);
        //        if (!pSqlReader.IsDBNull(iIndex))
        //            entPolicy.IsLink = pSqlReader.GetBoolean(iIndex);

        //        iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_POLICY_FILE_NAME_LINK);
        //        if (!pSqlReader.IsDBNull(iIndex))
        //            entPolicy.PolicyFileNameLink = pSqlReader.GetString(iIndex);

        //        iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_RELATIVE_PATH);
        //        if (!pSqlReader.IsDBNull(iIndex))
        //            entPolicy.RelativePath = pSqlReader.GetString(iIndex);                
        //    }
        //    return entPolicy;
        //}


        /// <summary>
        ///  To Fill Policy object properties values from reader data 
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>Policy Object</returns>
        private Policy FillObjectForLEarner(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            Policy entPolicy = new Policy();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_POLICY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicy.ID = pSqlReader.GetString(iIndex);


                iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_POLICY_FILE_NAME_LINK);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicy.PolicyFileNameLink = pSqlReader.GetString(iIndex);


                iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_IS_LINK);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicy.IsLink = pSqlReader.GetBoolean(iIndex);


                iIndex = pSqlReader.GetOrdinal(Schema.Policy.COL_RELATIVE_PATH);
                if (!pSqlReader.IsDBNull(iIndex))
                    entPolicy.RelativePath = pSqlReader.GetString(iIndex);




            }
            return entPolicy;
        }

        /// <summary>
        /// To Add new Policy
        /// </summary>
        /// <param name="pEntPolicy"></param>
        /// <returns>Policy Object</returns>
        public Policy AddPolicy(Policy pEntPolicy)
        {
            Policy entPolicy = new Policy();
            try
            {
                if (pEntPolicy.PolicyFile != null && pEntPolicy.PolicyFile.Length > 0)
                {
                    if (AddPolicyFile(ref pEntPolicy))
                        entPolicy = Update(pEntPolicy, Schema.Common.VAL_INSERT_MODE);
                    else
                        entPolicy = null;
                }
                else
                {
                    pEntPolicy.RelativePath = pEntPolicy.PolicyFileNameLink;
                    entPolicy = Update(pEntPolicy, Schema.Common.VAL_INSERT_MODE);
                }
            }
            catch (Exception expCommon)
            {
                entPolicy = null;
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entPolicy;
        }

        /// <summary>
        /// Add Policy File on Relative Path
        /// </summary>
        /// <param name="pEntPolicy"></param>
        /// <returns>true if success</returns>
        private bool AddPolicyFile(ref Policy pEntPolicy)
        {
            PolicyLibraryAdaptor entPolicyLibAdp = new PolicyLibraryAdaptor();
            FileHandler fileHandler = new FileHandler(pEntPolicy.ClientId);
            string strRootPath = FileHandler.CLIENTS_FOLDER_PATH + "/" + pEntPolicy.ClientId + "/";
            string strFileName = "";
            try
            {
                //---- Get the Relative Path from PolicyFolderId
                PolicyLibrary entPolicyLibrary = new PolicyLibrary();
                entPolicyLibrary.ID = pEntPolicy.PolicyFolderId;
                entPolicyLibrary.ClientId = pEntPolicy.ClientId;
                entPolicyLibrary.CreatedById = pEntPolicy.CreatedById;
                entPolicyLibrary = entPolicyLibAdp.GetPolicyLibraryById(entPolicyLibrary);
                if (!string.IsNullOrEmpty(entPolicyLibrary.RelativePath))
                {
                    strRootPath += entPolicyLibrary.RelativePath;
                    strFileName = pEntPolicy.PolicyFileNameLink;
                    //-- Upload file
                    fileHandler.Uploadfile(strRootPath, strFileName, pEntPolicy.PolicyFile);
                    //--
                    if (pEntPolicy.LanguageId == Language.SYSTEM_DEFAULT_LANG_ID)
                    {
                        pEntPolicy.RelativePath = entPolicyLibrary.RelativePath + strFileName;
                    }

                    pEntPolicy.PolicyFileType = Path.GetExtension(strFileName);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// private method to Delete Policy File on server
        /// </summary>
        /// <param name="pEntPolicy"></param>
        /// <returns>true if success</returns>
        private bool DeletePolicyFile(Policy pEntPolicy)
        {
            FileHandler fileHandler = new FileHandler(pEntPolicy.ClientId);
            string strRootPath = FileHandler.CLIENTS_FOLDER_PATH + "/" + pEntPolicy.ClientId + "/";

            PolicyLibrary entPolicyLibrary = new PolicyLibrary();
            PolicyLibraryAdaptor entPolicyLibAdp = new PolicyLibraryAdaptor();
            entPolicyLibrary.ID = pEntPolicy.PolicyFolderId;
            entPolicyLibrary.ClientId = pEntPolicy.ClientId;
            //entAssetLibrary.CreatedById = pEntAsset.CreatedById;
            entPolicyLibrary = entPolicyLibAdp.GetPolicyLibraryById(entPolicyLibrary);

            pEntPolicy.RelativePath = entPolicyLibrary.RelativePath + pEntPolicy.PolicyFileNameLink;


            if (!string.IsNullOrEmpty(pEntPolicy.RelativePath))
            {
                strRootPath += pEntPolicy.RelativePath;
                if (fileHandler.DeleteFileOnServer(strRootPath))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// To Edit the Policy data 
        /// </summary>
        /// <param name="pEntPolicy"></param>
        /// <returns>Policy Object</returns>
        public Policy EditPolicy(Policy pEntPolicy)
        {
            Policy entPolicy = new Policy();
            Policy entPolicyNew = new Policy();
            try
            {
                if (!pEntPolicy.IsLink && string.IsNullOrEmpty(pEntPolicy.RelativePath))
                {
                    //-- In case of Delete 
                    entPolicyNew = GetPolicyById(pEntPolicy);
                    //Check that New File has been uploaded
                    if (pEntPolicy.PolicyFile != null && !entPolicyNew.IsLink)
                    {
                        //try
                        //{
                        //    DeletePolicyFile(entPolicyNew);
                        //}
                        //catch (Exception expCommon)
                        //{
                        //    pEntPolicy = null;
                        //    _expCustom = new CustomException(_strMessageId1, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                        //    throw _expCustom;
                        //}
                    }
                    if (pEntPolicy.PolicyFile != null)
                    {
                        if (AddPolicyFile(ref pEntPolicy))
                        {
                            entPolicy = Update(pEntPolicy, Schema.Common.VAL_UPDATE_MODE);
                        }
                    }
                    else
                    {
                        entPolicy = Update(pEntPolicy, Schema.Common.VAL_UPDATE_MODE);
                    }
                }
                else
                {
                    //-- In case of Delete 
                    entPolicyNew = GetPolicyById(pEntPolicy);
                    //Check that New File has been uploaded
                    if (!entPolicyNew.IsLink)
                    {
                        try
                        {
                            DeletePolicyFile(entPolicyNew);
                        }
                        catch (Exception expCommon)
                        {
                            pEntPolicy = null;
                            _expCustom = new CustomException(_strMessageId1, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                            throw _expCustom;
                        }
                    }
                    pEntPolicy.RelativePath = "";
                    entPolicy = Update(pEntPolicy, Schema.Common.VAL_UPDATE_MODE);
                }
            }
            catch (Exception expCommon)
            {
                entPolicy = null;
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entPolicy;
        }

        /// <summary>
        /// private method to support both Add and Edit Policy transactions.
        /// </summary>
        /// <param name="pEntPolicy"></param>
        /// <param name="pUpdateMode"></param>
        /// <returns>Policy Object</returns>
        private Policy Update(Policy pEntPolicy, string pStrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Policy.PROC_UPDATE_POLICY;
            _strConnString = _sqlObject.GetClientDBConnString(pEntPolicy.ClientId);
            if (pStrUpdateMode == Schema.Common.VAL_INSERT_MODE)
            {
                pEntPolicy.ID = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_POLICY_ID_PREFIX, Schema.Common.VAL_POLICY_ID_LENGTH);
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
            }
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_ID, pEntPolicy.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            //_sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_NAME, pEntPolicy.PolicyName);
            //_sqlcmd.Parameters.Add(_sqlpara);

            //if (!String.IsNullOrEmpty(pEntPolicy.PolicyDescription))
            //    _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_DESCRIPTION, pEntPolicy.PolicyDescription);
            //else
            //    _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_DESCRIPTION, null);
            //_sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntPolicy.PolicyKeywords))
                _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_KEYWORDS, pEntPolicy.PolicyKeywords);
            else
                _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_KEYWORDS, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_FOLDER_ID, pEntPolicy.PolicyFolderId);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntPolicy.IsActive != null)
                _sqlpara = new SqlParameter(Schema.Policy.PARA_IS_ACTIVE, pEntPolicy.IsActive);
            else
                _sqlpara = new SqlParameter(Schema.Policy.PARA_IS_ACTIVE, DBNull.Value);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Policy.PARA_IS_PRINT_CERTIFICATE, pEntPolicy.IsPrintCertificate);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntPolicy.PolicyFile != null && pEntPolicy.PolicyFile.Length > 0)
            {
                //_sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_FILE_NAME_LINK, pEntPolicy.PolicyFileNameLink);
                //_sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_FILE_TYPE, pEntPolicy.PolicyFileType);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Policy.PARA_RELATIVE_PATH, pEntPolicy.RelativePath);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            _sqlpara = new SqlParameter(Schema.Policy.PARA_IS_LINK, pEntPolicy.IsLink);
            _sqlcmd.Parameters.Add(_sqlpara);

            //if (pEntPolicy.IsLink && pEntPolicy.PolicyFile == null)
            //{
            //    _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_FILE_NAME_LINK, pEntPolicy.PolicyFileNameLink);
            //    _sqlcmd.Parameters.Add(_sqlpara);
            //}

            _sqlpara = new SqlParameter(Schema.Policy.PARA_IS_SECURED, pEntPolicy.IsSecured);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntPolicy.ClientId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntPolicy.CreatedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntPolicy.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            int iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            pEntPolicy = UpdateLanguage(pEntPolicy);

            return pEntPolicy;
        }

        /// <summary>
        /// Delete Policy From Server and DataBase
        /// </summary>
        /// <param name="pEntPolicy"></param>
        /// <returns>Policy Object</returns>
        public Policy DeletePolicy(Policy pEntPolicy)
        {
            _sqlObject = new SQLObject();
            Policy entPolicy = new Policy();
            //Get the Relative Path
            entPolicy = GetPolicyById(pEntPolicy);
            if (entPolicy != null)
            {
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Policy.PROC_DELETE_POLICY;
                _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_ID, pEntPolicy.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                try
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntPolicy.ClientId);
                    //Delete Policy from database
                    int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                    if (iDelStatus > 0)
                    {
                        if (!pEntPolicy.IsLink)
                        {
                            //Delete Policy from server
                            if (DeletePolicyFile(entPolicy))
                            {
                                pEntPolicy = null;
                            }
                            else
                                throw new Exception("Unable to delete Policy file");
                        }
                        else
                        {
                            pEntPolicy = null;
                        }
                    }
                    else
                    {
                        Exception exc = null;
                        _expCustom = new CustomException(_strPolicyAssignMsgId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, exc, false);
                        throw _expCustom;
                    }
                }
                catch (Exception expCommon)
                {
                    pEntPolicy = null;
                    _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                    throw _expCustom;
                }
            }
            else
            {
                pEntPolicy = null;
                throw new Exception("Unable to delete Policy file");
            }
            return pEntPolicy;
        }

        public Policy DeletePolicyLanguage(Policy pEntPolicy)
        {
            _sqlObject = new SQLObject();
            Policy entPolicy = new Policy();
            //Get the Relative Path
            entPolicy = GetPolicyById(pEntPolicy);
            if (entPolicy != null)
            {
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Policy.PROC_DELETE_POLICYLANGUAGE;

                _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_ID, pEntPolicy.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntPolicy.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                try
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntPolicy.ClientId);
                    //Delete Policy from database
                    int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                    if (iDelStatus > 0)
                    {
                        if (!pEntPolicy.IsLink)
                        {
                            //Delete Policy from server
                            if (DeletePolicyFile(entPolicy))
                            {
                                pEntPolicy = null;
                            }
                            else
                                throw new Exception("Unable to delete Policy file");
                        }
                        else
                        {
                            pEntPolicy = null;
                        }
                    }
                    else
                    {
                        Exception exc = null;
                        _expCustom = new CustomException(_strPolicyAssignMsgId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, exc, false);
                        throw _expCustom;
                    }
                }
                catch (Exception expCommon)
                {
                    pEntPolicy = null;
                    _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                    throw _expCustom;
                }
            }
            else
            {
                pEntPolicy = null;
                throw new Exception("Unable to delete Policy file");
            }
            return pEntPolicy;
        }

        /// <summary>
        /// Get All Policy
        /// </summary>
        /// <param name="pEntPolicy"></param>
        /// <returns>List of Policy Object</returns>

        /// <summary>
        /// private method to support both Activate and Deactivate Policy.
        /// </summary>
        /// <param name="pEntPolicy"></param>        
        /// <returns>Policy Object</returns>
        public Policy UpdateStatus(Policy pEntPolicy)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Policy.PROC_UPDATE_POLICY_STATUS;
            _strConnString = _sqlObject.GetClientDBConnString(pEntPolicy.ClientId);

            _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_ID, pEntPolicy.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntPolicy.IsActive != null)
                _sqlpara = new SqlParameter(Schema.Policy.PARA_IS_ACTIVE, pEntPolicy.IsActive);
            else
                _sqlpara = new SqlParameter(Schema.Policy.PARA_IS_ACTIVE, DBNull.Value);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntPolicy.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            int iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            return pEntPolicy;
        }

        public List<Policy> GetAllPolicyLanguage(Policy pEntPolicyMaster)
        {
            return GetPolicyLanguageList(pEntPolicyMaster);
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntPolicy"></param>
        /// <returns></returns>
        public Policy Get(Policy pEntPolicy)
        {
            return GetPolicyById(pEntPolicy);
        }
        /// <summary>
        /// Update Policy
        /// </summary>
        /// <param name="pEntPolicy"></param>
        /// <returns></returns>
        public Policy Update(Policy pEntPolicy)
        {
            return EditPolicy(pEntPolicy);
        }

        public Policy UpdateLanguage(Policy pEntPolicyMaster)
        {
            return UpdatePolicyMasterLanguage(pEntPolicyMaster, Policy.Method.UpdateLanguage);
        }

        private Policy UpdatePolicyMasterLanguage(Policy pEntPolicyMaster, Policy.Method pMethod)
        {
            int iRowsAffected = 0;
            SqlConnection sqlConn = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Policy.PROC_UPDATE_POLICY_LANGUAGE;
            _sqlObject = new SQLObject();


            if (!String.IsNullOrEmpty(pEntPolicyMaster.LanguageId))
                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntPolicyMaster.LanguageId);
            else
                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntPolicyMaster.PolicyName))
                _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_NAME, pEntPolicyMaster.PolicyName);
            else
                _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_NAME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntPolicyMaster.PolicyDescription))
                _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_DESCRIPTION, pEntPolicyMaster.PolicyDescription);
            else
                _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_DESCRIPTION, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntPolicyMaster.PolicyFileNameLink))
                _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_FILE_NAME_LINK, pEntPolicyMaster.PolicyFileNameLink);
            else
                _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_FILE_NAME_LINK, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            try
            {
                if (pMethod == Policy.Method.Update)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else if (pMethod == Policy.Method.Add)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    pEntPolicyMaster.ID = YPLMS.Services.IDGenerator.GetStringGUIDWithPrefix(Schema.Common.VAL_POLICY_ID_PREFIX);
                }

                _sqlpara = new SqlParameter(Schema.Policy.PARA_POLICY_ID, pEntPolicyMaster.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetClientDBConnString(pEntPolicyMaster.ClientId);
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
            return pEntPolicyMaster;
        }

        #endregion
    }
}
