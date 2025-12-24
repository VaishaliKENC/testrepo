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
    /// <summary>
    /// class ContentModuleMappingAdaptor
    /// </summary>
    public class ContentModuleMappingAdaptor : IDataManager<ContentModuleMapping>,IContentModuleMappingAdaptor<ContentModuleMapping>
    {

        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        ContentModuleMapping _entContentModuleMapping = null;
        List<ContentModuleMapping> _entListContentModuleMapping = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.ContentModuleMapping.CONTENTMODULEMAPPING_DAM_ERROR;
        #endregion
        /// <summary>
        /// Default constructor
        /// </summary>
        public ContentModuleMappingAdaptor()
        {
        }
        /// <summary>
        /// To get ContentModuleMapping details by ID.
        /// </summary>
        /// <param name="pEntContentModuleMapping"></param>
        /// <returns>ContentModuleMapping</returns>
        public ContentModuleMapping GetContentModuleMappingByID(ContentModuleMapping pEntContentModuleMapping)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.ContentModuleMapping.PROC_GET_CONTENTMODULEMAPPING;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntContentModuleMapping.ID))
                _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_CONTENTMODULEID, pEntContentModuleMapping.ID.ToString());
            else
                _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_CONTENTMODULEID, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetMasterDBConnString();
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entContentModuleMapping = FillObject(_sqlreader, false, _sqlObject);
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
            return _entContentModuleMapping;
        }

        /// <summary>
        /// ContentModuleMapping Range
        /// </summary>
        /// <param name="pEntContentModuleMapping"></param>
        /// <returns></returns>
        public List<ContentModuleMapping> GetAllContentModuleMapping(ContentModuleMapping pEntContentModuleMapping)
        {
            _sqlcmd = new SqlCommand();

            if (pEntContentModuleMapping.FilterType == "assign")
                _sqlcmd.CommandText = Schema.ContentModuleMapping.PROC_GET_ALL_CONTENTMODULEMAPPING_ASSIGN;

            if (pEntContentModuleMapping.FilterType == "unassign")
                _sqlcmd.CommandText = Schema.ContentModuleMapping.PROC_GET_ALL_CONTENTMODULEMAPPING_UNASSIGN;

            _entListContentModuleMapping = new List<ContentModuleMapping>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetMasterDBConnString();
                sqlConnection = new SqlConnection(_strConnString);


                if (pEntContentModuleMapping.ListRange != null)
                {
                    if (pEntContentModuleMapping.ListRange.PageIndex > 0)
                    {
                        _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_PAGE_INDEX, pEntContentModuleMapping.ListRange.PageIndex);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (pEntContentModuleMapping.ListRange.PageSize > 0)
                    {
                        _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_PAGE_SIZE, pEntContentModuleMapping.ListRange.PageSize);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (!string.IsNullOrEmpty(pEntContentModuleMapping.ListRange.SortExpression))
                    {
                        _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_SORT_EXP, pEntContentModuleMapping.ListRange.SortExpression);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }


                }

                if (!String.IsNullOrEmpty(pEntContentModuleMapping.ContentModuleKeyWords))
                    _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_KEY_WORD, pEntContentModuleMapping.ContentModuleKeyWords);
                else
                    _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_KEY_WORD, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntContentModuleMapping.LanguageId))
                    _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_LANGUAGEID, pEntContentModuleMapping.LanguageId);
                else
                    _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_LANGUAGEID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntContentModuleMapping.ContentModuleTypeId))
                    _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_CONTENTMODULETYPEID, pEntContentModuleMapping.ContentModuleTypeId);
                else
                    _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_CONTENTMODULETYPEID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntContentModuleMapping.FilterType))
                    _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_FILTERTYPE, pEntContentModuleMapping.FilterType);
                else
                    _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_FILTERTYPE, null);
                _sqlcmd.Parameters.Add(_sqlpara);



                if (!String.IsNullOrEmpty(pEntContentModuleMapping.ClientId))
                {
                    _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntContentModuleMapping.ClientId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entContentModuleMapping = FillObject(_sqlreader, true, _sqlObject);
                    _entListContentModuleMapping.Add(_entContentModuleMapping);
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
            return _entListContentModuleMapping;
        }

        /// <summary>
        /// Fill Object
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>ContentModuleMapping</returns>
        private ContentModuleMapping FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entContentModuleMapping = new ContentModuleMapping();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_CONTENTMODULEID);
                if (!pSqlReader.IsDBNull(iIndex))
                    _entContentModuleMapping.ID = pSqlReader.GetString(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_CLIENTID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_CLIENTID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.ClientId = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_CONTENT_MODULE_ENGLISH_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_CONTENT_MODULE_ENGLISH_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.ContentModuleEnglishName = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_ISACTIVE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_ISACTIVE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.IsActive = pSqlReader.GetBoolean(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_CONTENT_MODULE_DESC))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_CONTENT_MODULE_DESC);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.ContentModuleDescription = pSqlReader.GetString(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_KEY_WORDS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_KEY_WORDS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.ContentModuleKeyWords = pSqlReader.GetString(iIndex);
                }


                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_CONTENT_MODULE_TYPE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_CONTENT_MODULE_TYPE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.ContentModuleTypeId = pSqlReader.GetString(iIndex);
                    else
                        _entContentModuleMapping.ContentModuleTypeId = "";
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_CONTENT_MODULE_URL))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_CONTENT_MODULE_URL);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.ContentModuleURL = pSqlReader.GetString(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_IS_MASTERCONTENT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_IS_MASTERCONTENT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.IsMasterContent = pSqlReader.GetBoolean(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_IS_ACTIVE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_IS_ACTIVE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.IsActive = pSqlReader.GetBoolean(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_IS_UPLOADED))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_IS_UPLOADED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.IsUploaded = pSqlReader.GetBoolean(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_AVPATH))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_AVPATH);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.AVPath = pSqlReader.GetString(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_ALLOW_RESIZE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_ALLOW_RESIZE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.AllowResize = pSqlReader.GetBoolean(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_ALLOW_SCROLL))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_ALLOW_SCROLL);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.AllowScroll = pSqlReader.GetBoolean(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_COURSE_LAUNCH_NEW_WIND0W))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_COURSE_LAUNCH_NEW_WIND0W);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.CourseLaunchNewWindow = pSqlReader.GetBoolean(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_COURSE_LAUNCH_SAME_WIND0W))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_COURSE_LAUNCH_SAME_WIND0W);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.CourseLaunchSameWindow = pSqlReader.GetBoolean(iIndex);

                }


                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_COURSE_WINDOW_HEIGHT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_COURSE_WINDOW_HEIGHT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.CourseWindowHeight = pSqlReader.GetInt32(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_COURSE_WINDOW_WIDTH))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_COURSE_WINDOW_WIDTH);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.CourseWindowWidth = pSqlReader.GetInt32(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_MASTERY_SCORE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_MASTERY_SCORE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.MasteryScore = pSqlReader.GetInt32(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_SCORE_TRACKING))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_SCORE_TRACKING);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.ScoreTracking = pSqlReader.GetBoolean(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_QUESTION_RESPONSE_TRACKING))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_QUESTION_RESPONSE_TRACKING);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.QuestionResponseTracking = pSqlReader.GetBoolean(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_SUB_TYPE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_SUB_TYPE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.ContentModuleSubTypeId = pSqlReader.GetString(iIndex);
                    else
                        _entContentModuleMapping.ContentModuleSubTypeId = "";
                }
                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_IS_PRINT_CERTIFICATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_IS_PRINT_CERTIFICATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.IsPrintCertificate = pSqlReader.GetBoolean(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_IS_COURSE_SESSION_NO_EXPIRY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_IS_COURSE_SESSION_NO_EXPIRY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.IsCourseSessionNoExpiry = pSqlReader.GetBoolean(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_COURSE_GROUP_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_COURSE_GROUP_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.CourseGroupId = pSqlReader.GetString(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_IMANIFEST_URL))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_IMANIFEST_URL);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.ImanifestUrl = pSqlReader.GetString(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_IS_SHORT_LANGUAGE_CODE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_IS_SHORT_LANGUAGE_CODE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.IsShortLanguageCode = pSqlReader.GetBoolean(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_LANGUAGE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_LANGUAGE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.LanguageId = pSqlReader.GetString(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.CreatedById = pSqlReader.GetString(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.DateCreated = pSqlReader.GetDateTime(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.LastModifiedById = pSqlReader.GetString(iIndex);
                }

                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.LastModifiedDate = pSqlReader.GetDateTime(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_IS_ALLOCATED))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_IS_ALLOCATED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.IsAllocated = pSqlReader.GetBoolean(iIndex);
                }
                else
                    _entContentModuleMapping.IsAllocated = false;

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_IS_COURSE_MODIFIED_BY_ADMIN))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_IS_COURSE_MODIFIED_BY_ADMIN);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.IsCourseModifiedByAdmin = pSqlReader.GetBoolean(iIndex);
                    else
                        _entContentModuleMapping.IsCourseModifiedByAdmin = false;
                }

                //-aw 6/15/2011 Added course protocol
                if (_sqlObject != null && _sqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_PROTOCOL))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_PROTOCOL);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.Protocol = pSqlReader.GetString(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_ISASSESSMENT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_ISASSESSMENT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.IsAssessment = pSqlReader.GetBoolean(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_ISMIDDLEPAGE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_ISMIDDLEPAGE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.IsMiddlePage = pSqlReader.GetBoolean(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_IS_HTML_5))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_IS_HTML_5);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.IsHTML5 = pSqlReader.GetBoolean(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_KEEP_ZIP_FILE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_KEEP_ZIP_FILE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.KeepZipFile = pSqlReader.GetBoolean(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_RAPIDEL_COURSEID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_RAPIDEL_COURSEID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.RapidelCourseId = pSqlReader.GetString(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.ContentModuleMapping.COL_PERUSE_COURSEID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleMapping.COL_PERUSE_COURSEID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entContentModuleMapping.PeruseCourseId = pSqlReader.GetString(iIndex);
                }
                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    _entContentModuleMapping.ListRange = _entRange;
                    return _entContentModuleMapping;
                }
            }
            return _entContentModuleMapping;
        }

        /// <summary>
        /// To add/update a ContentModuleMapping.
        /// </summary>
        /// <param name="pEntContentModuleMapping"></param>
        /// <returns>ContentModuleMapping</returns>
        public ContentModuleMapping UpdateContentModuleMapping(ContentModuleMapping pEntContentModuleMapping, ContentModuleMapping.Method pMethod)
        {
            int iRowsAffected = 0;
            SqlConnection sqlConn = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.ContentModuleMapping.PROC_UPDATE_CONTENTMODULEMAPPING;
            _sqlObject = new SQLObject();
            _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_CONTENTMODULEID, pEntContentModuleMapping.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntContentModuleMapping.ClientId))
                _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_CLIENTID, pEntContentModuleMapping.ClientId);
            else
                _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_CLIENTID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_ISACTIVE, pEntContentModuleMapping.IsActive);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                if (pMethod == ContentModuleMapping.Method.Update)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else if (pMethod == ContentModuleMapping.Method.Add)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _strConnString = _sqlObject.GetMasterDBConnString();
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
            return pEntContentModuleMapping;
        }
        /// <summary>
        /// Delete ContentModuleMapping by ID
        /// </summary>
        /// <param name="pEntContentModuleMapping">ContentModuleMapping with ID</param>
        /// <returns>Deleted ContentModuleMapping with only ID</returns>
        public ContentModuleMapping DeleteContentModuleMapping(ContentModuleMapping pEntContentModuleMapping)
        {
            ContentModuleMapping entContentModuleMapping = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.ContentModuleMapping.PROC_DELETE_CONTENTMODULEMAPPING;
            _sqlObject = new SQLObject();
            int i = 0;
            if (!String.IsNullOrEmpty(pEntContentModuleMapping.ID))
                _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_CONTENTMODULEID, pEntContentModuleMapping.ID);
            else
                _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_CONTENTMODULEID, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetMasterDBConnString();
                i = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                if (i < 0)
                {
                    entContentModuleMapping = new ContentModuleMapping();
                    entContentModuleMapping.ID = pEntContentModuleMapping.ID;
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entContentModuleMapping;
        }

        public List<ContentModuleMapping> BulkDelete(List<ContentModuleMapping> pEntListContentModuleMapping)
        {
            _sqlObject = new SQLObject();
            List<ContentModuleMapping> entListContentModule = new List<ContentModuleMapping>();
            DataTable dtable;
            SqlCommand sqlcmdDel;
            SqlConnection _sqlcon = null;
            DataRow drow = null;

            EntityRange entRange = new EntityRange();

            try
            {
                _strConnString = _sqlObject.GetMasterDBConnString();
                _sqlcon = new SqlConnection(_strConnString);

                foreach (ContentModuleMapping entContentModule in pEntListContentModuleMapping)
                {

                    entListContentModule.Add(entContentModule);

                    sqlcmdDel = new SqlCommand(Schema.ContentModuleMapping.PROC_DELETE_CONTENTMODULEMAPPING, _sqlcon);
                    sqlcmdDel.CommandType = CommandType.StoredProcedure;
                    _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_CONTENTMODULEID, entContentModule.ID);
                    sqlcmdDel.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_CLIENTID, entContentModule.ClientId);
                    sqlcmdDel.Parameters.Add(_sqlpara);

                    int iDelStatus = _sqlObject.ExecuteNonQuery(sqlcmdDel, _strConnString);
                    if (iDelStatus > 0)
                    {
                        entListContentModule.Remove(entContentModule);
                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListContentModule;
        }

        public List<ContentModuleMapping> BulkActivateDeactivate(List<ContentModuleMapping> pEntListContentModuleMapping)
        {
            _sqlObject = new SQLObject();
            List<ContentModuleMapping> entListContentModule = new List<ContentModuleMapping>();
            //DataTable dtable;
            SqlCommand sqlcmdDel;
            SqlConnection _sqlcon = null;
            //DataRow drow = null;

            EntityRange entRange = new EntityRange();

            try
            {
                _strConnString = _sqlObject.GetMasterDBConnString();
                _sqlcon = new SqlConnection(_strConnString);

                foreach (ContentModuleMapping entContentModule in pEntListContentModuleMapping)
                {

                    entListContentModule.Add(entContentModule);

                    sqlcmdDel = new SqlCommand(Schema.ContentModuleMapping.PROC_ACTIVATE_DEACTIVATE_CONTENTMODULEMAPPING, _sqlcon);
                    sqlcmdDel.CommandType = CommandType.StoredProcedure;
                    _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_CONTENTMODULEID, entContentModule.ID);
                    sqlcmdDel.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_CLIENTID, entContentModule.ClientId);
                    sqlcmdDel.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.ContentModuleMapping.PARA_ISACTIVE, entContentModule.IsActive);
                    sqlcmdDel.Parameters.Add(_sqlpara);

                    int iDelStatus = _sqlObject.ExecuteNonQuery(sqlcmdDel, _strConnString);
                    if (iDelStatus > 0)
                    {
                        entListContentModule.Remove(entContentModule);
                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListContentModule;
        }

        #region Interface Methods
        /// <summary>
        /// Get ContentModuleMapping By ID
        /// </summary>
        /// <param name="pEntContentModuleMapping"></param>
        /// <returns></returns>
        public ContentModuleMapping Get(ContentModuleMapping pEntContentModuleMapping)
        {
            return GetContentModuleMappingByID(pEntContentModuleMapping);
        }
        /// <summary>
        /// List of All ContentModuleMapping
        /// </summary>
        /// <param name="pEntContentModuleMapping"></param>
        /// <returns></returns>
        public List<ContentModuleMapping> GetAll(ContentModuleMapping pEntContentModuleMapping)
        {
            return GetAllContentModuleMapping(pEntContentModuleMapping);
        }
        /// <summary>
        /// Add ContentModuleMapping
        /// </summary>
        /// <param name="pEntContentModuleMapping"></param>
        /// <returns></returns>
        public ContentModuleMapping Add(ContentModuleMapping pEntContentModuleMapping)
        {
            return UpdateContentModuleMapping(pEntContentModuleMapping, ContentModuleMapping.Method.Add);
        }
        /// <summary>
        /// Update ContentModuleMapping
        /// </summary>
        /// <param name="pEntContentModuleMapping"></param>
        /// <returns></returns>
        public ContentModuleMapping Update(ContentModuleMapping pEntContentModuleMapping)
        {
            return UpdateContentModuleMapping(pEntContentModuleMapping, ContentModuleMapping.Method.Update);
        }
        /// <summary>
        /// Delete ContentModuleMapping
        /// </summary>
        /// <param name="pEntContentModuleMapping"></param>
        /// <returns>ContentModuleMapping</returns>
        public ContentModuleMapping Delete(ContentModuleMapping pEntContentModuleMapping)
        {
            return DeleteContentModuleMapping(pEntContentModuleMapping);
        }
        #endregion
    }
}
