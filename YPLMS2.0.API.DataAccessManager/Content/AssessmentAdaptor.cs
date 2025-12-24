using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    /// <summary>
    /// class Assessment Adaptor
    /// </summary>
    public class AssessmentAdaptor : IDataManager<AssessmentDates>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlDataAdapter _sqladapter = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        DataTable _dtable = null;
        SqlConnection _sqlcon = null;
        string _strMessageId = YPLMS.Services.Messages.Assessment.ASSESSMENT_ERROR;
        string _strConnString = string.Empty;
        #endregion


        /// <summary>
        /// To Get Assessment details by Assessment Id.
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns>Assessment Object</returns>
        public AssessmentDates GetAssessmentById(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            AssessmentDates entAssessment = new AssessmentDates();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Assessment.PROC_SELECT_ASSESSMENT;
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAssessment.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);


                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entAssessment = FillObject(_sqlreader, false, _sqlObject);
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
            return entAssessment;
        }

        /// <summary>
        /// To Get Assessment Result by Assessment Id.
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns>Assessment Object</returns>
        public AssessmentDates GetAssessmentResult(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            AssessmentDates entAssessment = new AssessmentDates();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Assessment.PROC_SHOW_ASSESSMENT_RESULT;
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAssessment.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entAssessment = FillAssessmentResultObject(_sqlreader, false, _sqlObject);
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
            return entAssessment;
        }
        /// <summary>
        /// To Get Assessment Default Sequencing by Assessment Id.
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns>Assessment Object</returns>
        public AssessmentDates GetAssessmentDefaultSequence(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            AssessmentDates entAssessment = new AssessmentDates();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Assessment.PROC_SELECT_ASSESSMENT_DEFAULT_SEQ;
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entAssessment = FillObjectDefaultSequencing(_sqlreader, false, _sqlObject);
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
            return entAssessment;
        }
        /// <summary>
        /// Fill Object Default Sequencing
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <param name="pRangeList"></param>
        /// <param name="pSqlObject"></param>
        /// <returns></returns>
        private AssessmentDates FillObjectDefaultSequencing(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            AssessmentDates entAssessment = new AssessmentDates();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_IS_DEFAULT_SEQ);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.IsDefaultSequence = pSqlReader.GetBoolean(iIndex);


            }
            return entAssessment;
        }
        /// <summary>
        /// FillAssessmentResultObject
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <param name="pRangeList"></param>
        /// <param name="pSqlObject"></param>
        /// <returns></returns>
        private AssessmentDates FillAssessmentResultObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            AssessmentDates entAssessment = new AssessmentDates();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_TYPE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.AssessmentType = (AssessmentDates.AssessmentDisplayType)Enum.Parse(typeof(AssessmentDates.AssessmentDisplayType), pSqlReader.GetString(iIndex));

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_IS_ACTIVE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.IsActive = pSqlReader.GetBoolean(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_SHOW_ASSESSMENT_RESULT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_SHOW_ASSESSMENT_RESULT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.ShowAssessmentResult = (AssessmentDates.ShowAssessmentResultType)Enum.Parse(typeof(AssessmentDates.ShowAssessmentResultType), pSqlReader.GetString(iIndex));
                }
                iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_LANGUAGE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.LanguageId = pSqlReader.GetString(iIndex);
            }
            return entAssessment;
        }
        /// <summary>
        /// To Get Assessment Default Sequencing by Assessment Id.
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns>Assessment Object</returns>
        public AssessmentDates GetAssessmentShowQuestionNumber(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            AssessmentDates entAssessment = new AssessmentDates();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Assessment.PROC_SELECT_ASSESSMENT_QUEST_NUMBER;
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entAssessment = FillObjectShowQuestionNumber(_sqlreader, false, _sqlObject);
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
            return entAssessment;
        }
        /// <summary>
        /// Fill Object Default Sequencing
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <param name="pRangeList"></param>
        /// <param name="pSqlObject"></param>
        /// <returns></returns>
        private AssessmentDates FillObjectShowQuestionNumber(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            AssessmentDates entAssessment = new AssessmentDates();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.ID = pSqlReader.GetString(iIndex);

                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_IS_SHOW_QUESTIONNUMBER);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.IsShowQuestionNumber = pSqlReader.GetBoolean(iIndex);

                }
            }
            return entAssessment;
        }


        /// <summary>
        /// To Get Assessment details by Assessment Id.
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns>Assessment Object</returns>
        public AssessmentDates GetAssessmentTypeById_Learner(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            AssessmentDates entAssessment = new AssessmentDates();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Assessment.PROC_SELECT_ASSESSMENT_TYPE_LEARNER;
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAssessment.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                //entAssessment = FillObject(_sqlreader, false, _sqlObject);
                entAssessment = FillObjectAssessmentTypeLearner(_sqlreader, false, _sqlObject);
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
            return entAssessment;
        }

        /// <summary>
        /// To check duplicate Assessment title.
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns></returns>
        public AssessmentDates CheckAssessmentByName(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            AssessmentDates entAssessment = new AssessmentDates();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Assessment.PROC_CHECK_ASSESSMENT_BY_NAME;
                _sqlcmd.Connection = sqlConnection;

                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_TITLE, pEntAssessment.AssessmentTitle);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAssessment.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entAssessment = FillAssessmentTitle(_sqlreader);
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
            return entAssessment;
        }

        /// <summary>
        /// Fill Assessment Title
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns></returns>
        private AssessmentDates FillAssessmentTitle(SqlDataReader pSqlReader)
        {
            AssessmentDates entAssessment = new AssessmentDates();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_LANG_TITLE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.AssessmentTitle = pSqlReader.GetString(iIndex);
            }
            return entAssessment;
        }

        /// <summary>
        ///  To Fill Assessment object properties values from reader data 
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>Assessment Object</returns>
        private AssessmentDates FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            AssessmentDates entAssessment = new AssessmentDates();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_TYPE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.AssessmentType = (AssessmentDates.AssessmentDisplayType)Enum.Parse(typeof(AssessmentDates.AssessmentDisplayType), pSqlReader.GetString(iIndex));


                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_LOGO_POSITION);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.LogoPosition = (AssessmentDates.AssessmentLogoPosition)Enum.Parse(typeof(AssessmentDates.AssessmentLogoPosition), pSqlReader.GetString(iIndex));

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENTTIME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.AssessmentTime = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENTALERTTIME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.AssessmentAlertTime = pSqlReader.GetString(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_NUMBER_OF_ATTEMPTS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_NUMBER_OF_ATTEMPTS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.AssessmentNumberOfAttempts = pSqlReader.GetString(iIndex);

                }

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_IS_ACTIVE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.IsActive = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_IS_PRINT_CERTIFICATE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.IsPrintCertificate = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ALLOW_SEQUENCING);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.AllowSequencing = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_BGCOLOR);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.BGColor = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_DEFAULTLOGOPATH);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.DefaultLogoPath = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_IS_LOGO_ONALL);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.IsLogoOnAll = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_IS_PARTIAL_SUBMITALLOWED);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.IsPartialSubmitAllowed = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_IS_MULTI_SUBMITALLOWED);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.IsMultiSubmitAllowed = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_IS_REVIEW_ANSWER);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.IsReviewAnswer = pSqlReader.GetBoolean(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_IS_LOCKED))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_IS_LOCKED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.IsLocked = pSqlReader.GetBoolean(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_IS_LEARNERPRINTABLE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_IS_LEARNERPRINTABLE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.IsLearnerPrintable = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_IS_SHOW_QUESTIONNUMBER))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_IS_SHOW_QUESTIONNUMBER);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.IsShowQuestionNumber = pSqlReader.GetBoolean(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_QUESTION_COUNT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_QUESTION_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.ParaQuestionCount = pSqlReader.GetInt32(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_TRACKING_TYPE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_TRACKING_TYPE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.TrackingType = (AssessmentDates.AssessmentSubmitType)Enum.Parse(typeof(AssessmentDates.AssessmentSubmitType), Convert.ToString(pSqlReader.GetString(iIndex)));
                }


                //new properies added: on 13-apr-2010
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_IS_USESECTIONS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_IS_USESECTIONS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.IsUseSections = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_IS_LOGOHIDE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_IS_LOGOHIDE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.IsLogoHide = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_IS_LOGOONFIRSTPAGEONLY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_IS_LOGOONFIRSTPAGEONLY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.IsLogoOnFirstPageOnly = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_BGHEADER_HF))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_BGHEADER_HF);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.BGColorHF = pSqlReader.GetString(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_IS_ALL_ANSWER_MUST))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_IS_ALL_ANSWER_MUST);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.IsAllAnswerMust = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_FONT_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_FONT_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.FontName = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_FONT_SIZE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_FONT_SIZE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.FontSize = pSqlReader.GetInt32(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_FONT_COLOR))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_FONT_COLOR);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.FontColor = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_QUEBG_COLOR))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_QUEBG_COLOR);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.QuestionaireBGColor = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_MAX_RESPONSE_LENGTH))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_MAX_RESPONSE_LENGTH);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.MaxResponseLength = pSqlReader.GetInt32(iIndex);
                }

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ALLOW_USER_LANGSELECTION);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.AllowUserLangSelection = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.ClientId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.CreatedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.DateCreated = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.LastModifiedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.LastModifiedDate = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_LANGUAGE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.LanguageId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_LANG_TITLE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.AssessmentTitle = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_LANG_DESC);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.AssessmentDescription = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_LANG_INST_TOP);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.AssessmentInstructionTop = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_LANG_INST_BOT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.AssessmentInstructionBottom = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_LANG_CF_REVIEW_EMAIL);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.CFForReviewEmail = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_LANG_REVIEW_SENT_DATE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.DateLastReviewSent = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_LANG_REVIEW_EMAIL);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.ReviewEmail = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_LANG_APPROVAL_STATUS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.ApprovalStatus = (AssessmentDates.AssessmentApprovalStatus)Enum.Parse(typeof(AssessmentDates.AssessmentApprovalStatus), pSqlReader.GetString(iIndex));

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_LANG_APPROVED_BY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.ApprovedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_LANG_DATE_APPROVED);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.DateApproved = pSqlReader.GetDateTime(iIndex);


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_BUTTONPRINTTXT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_BUTTONPRINTTXT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.ButtonPrintTxt = pSqlReader.GetString(iIndex);
                }


                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_LANG_BTN_NEXT_TXT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.ButtonNextTxt = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_LANG_BTN_PREVIOUS_TXT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.ButtonPreviousTxt = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_LANG_BTN_SUBMIT_TXT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.ButtonSubmitTxt = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_LANG_BTN_EXIT_TXT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.ButtonExitTxt = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_BTN_SAVE_TXT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.ButtonSaveTxt = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_LANG_LOGO_PATH);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.LanguageLogoPath = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.CreatedById = pSqlReader.GetString(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_SHOW_ASSESSMENT_RESULT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_SHOW_ASSESSMENT_RESULT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.ShowAssessmentResult = (AssessmentDates.ShowAssessmentResultType)Enum.Parse(typeof(AssessmentDates.ShowAssessmentResultType), pSqlReader.GetString(iIndex));
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_QUESTION_COUNT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_QUESTION_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.QuestionCount = pSqlReader.GetInt32(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_QUESTIONMAPPINGCOUNT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_QUESTIONMAPPINGCOUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.MappingQuestionCount = pSqlReader.GetInt32(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_SEND_EMAIL_OF_RESULT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_SEND_EMAIL_OF_RESULT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.IsSendEmailOfResult = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_SendEmailTo))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_SendEmailTo);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.SendEmailTo = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_SendEmailToAdminUser))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_SendEmailToAdminUser);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.SendEmailToAdminUser = pSqlReader.GetInt32(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_AllowQueOptionRandamization))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_AllowQueOptionRandamization);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.AllowOptionRandamization = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_ISBOOK_MARKING))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ISBOOK_MARKING);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.IsBookmarking = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_ISBOOK_MARKING))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ISBOOK_MARKING);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.strIsBookmarking = Convert.ToString(pSqlReader.GetBoolean(iIndex));
                }

                if (pRangeList)
                {
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                        if (!pSqlReader.IsDBNull(iIndex))
                        {
                            _entRange = new EntityRange();
                            _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                            entAssessment.ListRange = _entRange;
                        }
                    }
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.CreatedByName = pSqlReader.GetString(iIndex);

                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.ModifiedByName = pSqlReader.GetString(iIndex);

                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_Is_Complete_Assignment_Dep_On_Scrore))
                {

                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_Is_Complete_Assignment_Dep_On_Scrore);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.IsCompleteAssignmentDepOnScrore = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_Passing_Score))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_Passing_Score);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.PassingScore = Convert.ToDouble(pSqlReader.GetDecimal(iIndex));
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_IS_INCORRECT_QUESTIONS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_IS_INCORRECT_QUESTIONS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.IsIncorrectQuestions = pSqlReader.GetBoolean(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.Category.COL_CATEGORYNAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Category.COL_CATEGORYNAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.CategoryName = pSqlReader.GetString(iIndex);

                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.SubCategory.COL_SUBCATEGORYNAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.SubCategory.COL_SUBCATEGORYNAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.SubCategoryName = pSqlReader.GetString(iIndex);

                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Assessment.COL_AllowQueOptionRandamization))
                {

                    iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_AllowQueOptionRandamization);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entAssessment.AllowOptionRandamization = pSqlReader.GetBoolean(iIndex);
                }

            }
            return entAssessment;
        }

        /// <summary>
        ///  To Fill Assessment object properties values from reader data 
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>Assessment Object</returns>
        private AssessmentDates FillObjectAssessmentTypeLearner(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            AssessmentDates entAssessment = new AssessmentDates();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENT_TYPE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.AssessmentType = (AssessmentDates.AssessmentDisplayType)Enum.Parse(typeof(AssessmentDates.AssessmentDisplayType), pSqlReader.GetString(iIndex));

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENTTIME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.AssessmentTime = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Assessment.COL_ASSESSMENTALERTTIME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entAssessment.AssessmentAlertTime = pSqlReader.GetString(iIndex);
            }
            return entAssessment;
        }

        /// <summary>
        /// Find Assessment infomration
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<AssessmentDates> FindAssessments(Search pEntSearch)
        {
            _sqlObject = new SQLObject();
            List<AssessmentDates> entListAssessment = new List<AssessmentDates>();
            AssessmentDates entAssessment = new AssessmentDates();
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Assessment.PROC_FIND_ASSESSMENT;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                if (pEntSearch.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntSearch.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntSearch.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntSearch.KeyWord))
                {
                    _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_TITLE, pEntSearch.KeyWord);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntSearch.SearchObject != null && pEntSearch.SearchObject.Count > 0)
                {
                    entAssessment = new AssessmentDates();
                    entAssessment = (AssessmentDates)pEntSearch.SearchObject[0];
                    if (DateTime.MinValue.CompareTo(entAssessment.DateCreated) < 0)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_DATE_FROM, entAssessment.DateCreated);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (!string.IsNullOrEmpty(entAssessment.CreatedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, entAssessment.CreatedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (!string.IsNullOrEmpty(entAssessment.LanguageId))
                    {
                        _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, entAssessment.LanguageId);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (!string.IsNullOrEmpty(entAssessment.LastModifiedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Assessment.PARA_REQUESTED_BY_ID, entAssessment.LastModifiedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (!string.IsNullOrEmpty(entAssessment.FeatureCreatedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Assessment.PARA_FEATURE_CREATED_BY_ID, entAssessment.FeatureCreatedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (!string.IsNullOrEmpty(entAssessment.CategoryId))
                    {
                        _sqlpara = new SqlParameter(Schema.Category.PARA_CATEGORYID, entAssessment.CategoryId);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (!string.IsNullOrEmpty(entAssessment.SubCategoryId))
                    {
                        _sqlpara = new SqlParameter(Schema.SubCategory.PARA_SUBCATEGORYID, entAssessment.SubCategoryId);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (pEntSearch.SearchObject.Count > 1)
                    {
                        entAssessment = new AssessmentDates();
                        entAssessment = (AssessmentDates)pEntSearch.SearchObject[1];
                        if (DateTime.MinValue.CompareTo(entAssessment.DateCreated) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.Common.PARA_DATE_TO, entAssessment.DateCreated);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entAssessment = FillObject(_sqlreader, true, _sqlObject);
                    entListAssessment.Add(entAssessment);
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
            return entListAssessment;
        }

        /// <summary>
        /// Get All Assessment
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns>List of Assessment Object</returns>
        public List<AssessmentDates> GetAssessmentList(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            List<AssessmentDates> entListAssessment = new List<AssessmentDates>();
            AssessmentDates entAssessment = null;
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Assessment.PROC_GET_ALL_ASSESSMENT;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                if (pEntAssessment.ApprovalStatus == AssessmentDates.AssessmentApprovalStatus.Approved)
                {
                    _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_APPROVAL_STATUS, pEntAssessment.ApprovalStatus.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntAssessment.LanguageId))
                {
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAssessment.LanguageId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntAssessment.LastModifiedById))
                {
                    _sqlpara = new SqlParameter(Schema.Assessment.PARA_REQUESTED_BY_ID, pEntAssessment.LastModifiedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntAssessment.IsActive != null)
                {
                    _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_ACTIVE, pEntAssessment.IsActive);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENTTIME, pEntAssessment.AssessmentTime);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENTALERTTIME, pEntAssessment.AssessmentAlertTime);
                _sqlcmd.Parameters.Add(_sqlpara);
                if (pEntAssessment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntAssessment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntAssessment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntAssessment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entAssessment = FillObject(_sqlreader, true, _sqlObject);
                    entListAssessment.Add(entAssessment);
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
            return entListAssessment;
        }

        /// <summary>
        /// Get non preferred Assessment for assignment
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns>List of Assessment Object</returns>
        public List<AssessmentDates> GetAssessmentForAssignment(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            List<AssessmentDates> entListAssessment = new List<AssessmentDates>();
            AssessmentDates entAssessment = null;
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Assessment.PROC_GET_ASSESSMENT_FOR_ASSIGNMENT;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                if (pEntAssessment.ApprovalStatus == AssessmentDates.AssessmentApprovalStatus.Approved)
                {
                    _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_APPROVAL_STATUS, pEntAssessment.ApprovalStatus.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntAssessment.LanguageId))
                {
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAssessment.LanguageId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntAssessment.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.Assessment.PARA_CREATED_BY_ID, pEntAssessment.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntAssessment.IsActive != null)
                {
                    _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_ACTIVE, pEntAssessment.IsActive);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }



                if (!string.IsNullOrEmpty(pEntAssessment.CategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.Category.PARA_CATEGORYID, pEntAssessment.CategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntAssessment.SubCategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.SubCategory.PARA_SUBCATEGORYID, pEntAssessment.SubCategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntAssessment.AssessmentTitle))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntAssessment.AssessmentTitle);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }



                if (pEntAssessment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntAssessment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntAssessment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntAssessment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entAssessment = FillObject(_sqlreader, true, _sqlObject);
                    entListAssessment.Add(entAssessment);
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
            return entListAssessment;
        }

        /// <summary>
        /// To Get a Assessment languages list
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns></returns>
        public List<AssessmentDates> GetAssessmentLanguageList(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            List<AssessmentDates> entListAssessment = new List<AssessmentDates>();
            AssessmentDates entAssessment = null;
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Assessment.PROC_GET_ASSESSMENT_LANGUAGES;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                if (pEntAssessment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntAssessment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntAssessment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntAssessment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entAssessment = FillObject(_sqlreader, true, _sqlObject);
                    entListAssessment.Add(entAssessment);
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
            return entListAssessment;
        }

        /// <summary>
        /// Get Assessment details with respective sections,questions,options. 
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns></returns>
        public List<AssessmentDates> GetAssessmentDtls(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            List<AssessmentDates> entListAssessment = new List<AssessmentDates>();
            AssessmentDates entAssessment = null;
            AssessmentSections entSection = null;
            AssessmentQuestion entQuestion = null;
            AssessmentOptions entOption = null;
            _sqlcmd = new SqlCommand();
            DataSet dset = new DataSet();
            DataTable dtableAssessment;
            DataTable dtableSections;
            DataTable dtableQuestions;
            DataTable dtableOptions;
            _sqlcmd.CommandText = Schema.Assessment.PROC_GET_ASSESSMENT_DTLS;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
                if (!string.IsNullOrEmpty(pEntAssessment.ID))
                {
                    _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntAssessment.ParaSectionId))
                {
                    _sqlpara = new SqlParameter(Schema.AssessmentSections.PARA_SECTION_ID, pEntAssessment.ParaSectionId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntAssessment.ParaQuestionId))
                {
                    _sqlpara = new SqlParameter(Schema.AssessmentQuestion.PARA_QUESTION_ID, pEntAssessment.ParaQuestionId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntAssessment.ParaOptionId))
                {
                    _sqlpara = new SqlParameter(Schema.AssessmentOptions.PARA_OPTION_ID, pEntAssessment.ParaOptionId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntAssessment.LanguageId))
                {
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAssessment.LanguageId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                dset = _sqlObject.SqlDataAdapter(_sqlcmd, _strConnString);
                dtableAssessment = dset.Tables[0];
                dtableSections = dset.Tables[1];
                dtableQuestions = dset.Tables[2];
                dtableOptions = dset.Tables[3];
                foreach (DataRow drow in dtableAssessment.Rows)
                {
                    entAssessment = new AssessmentDates();
                    entAssessment = FillAssessment(drow);
                    foreach (DataRow drowSection in dtableSections.Select(Schema.Assessment.COL_ASSESSMENT_ID + "='" + entAssessment.ID + "'"))
                    {
                        entSection = new AssessmentSections();
                        entSection = FillSection(drowSection);
                        foreach (DataRow drowQuestion in dtableQuestions.Select(Schema.AssessmentQuestion.COL_SECTION_ID + "='" + entSection.ID + "'"))
                        {
                            entQuestion = new AssessmentQuestion();
                            entQuestion = FillQuestion(drowQuestion);
                            foreach (DataRow drowOption in dtableOptions.Select(Schema.AssessmentQuestion.COL_QUESTION_ID + "='" + entQuestion.ID + "'"))
                            {
                                entOption = new AssessmentOptions();
                                entOption = FillOption(drowOption);
                                entQuestion.AssessmentOptions.Add(entOption);
                            }
                            entSection.AssessmentQuestion.Add(entQuestion);
                        }
                        entAssessment.Sections.Add(entSection);
                    }
                    entListAssessment.Add(entAssessment);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListAssessment;
        }

        /// <summary>
        /// Get Assessment data with tracking infomration.
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns></returns>
        public List<AssessmentDates> GetAssessmentTracking(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            List<AssessmentDates> entListAssessment = new List<AssessmentDates>();
            AssessmentDates entAssessment = null;
            AssessmentSections entSection = null;
            AssessmentQuestion entQuestion = null;
            AssessmentOptions entOption = null;

            UserAssessmentTracking entAttempt = null;
            UserAssessmentSessionResponses entResponse = null;

            _sqlcmd = new SqlCommand();
            DataSet dset = new DataSet();
            DataTable dtableAssessment;
            DataTable dtableSections;
            DataTable dtableQuestions;
            DataTable dtableOptions;
            DataTable dtableAttempts = null;
            DataTable dtableResponse = null;


            _sqlcmd.CommandText = Schema.Assessment.PROC_GET_ASSESSMENT_TRACKING;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);

                if (!string.IsNullOrEmpty(pEntAssessment.ID))
                {
                    _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntAssessment.UserAttemptTracking != null && pEntAssessment.UserAttemptTracking.Count > 0)
                {
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntAssessment.UserAttemptTracking[0].SystemUserGUID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntAssessment.UserAttemptTracking != null && pEntAssessment.UserAttemptTracking.Count > 0)
                {

                    if (pEntAssessment.UserAttemptTracking[0].IsForAdminPreview)
                    {
                        _sqlpara = new SqlParameter(Schema.UserAssessmentTracking.PARA_IS_FOR_ADMIN_PREVIEW, pEntAssessment.UserAttemptTracking[0].IsForAdminPreview);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                if (!string.IsNullOrEmpty(pEntAssessment.ParaQuestionId))
                {
                    _sqlpara = new SqlParameter(Schema.AssessmentQuestion.PARA_QUESTION_ID, pEntAssessment.ParaQuestionId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntAssessment.LanguageId))
                {
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAssessment.LanguageId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }


                dset = _sqlObject.SqlDataAdapter(_sqlcmd, _strConnString);
                dtableAssessment = dset.Tables[0];
                dtableSections = dset.Tables[1];
                dtableQuestions = dset.Tables[2];
                dtableOptions = dset.Tables[3];
                if (dset.Tables.Count > 4)
                { dtableAttempts = dset.Tables[4]; }
                if (dset.Tables.Count == 6)
                    dtableResponse = dset.Tables[5];
                foreach (DataRow drow in dtableAssessment.Rows)
                {
                    entAssessment = new AssessmentDates();
                    entAssessment = FillAssessment(drow);
                    if (dset.Tables.Count > 4)
                    {
                        foreach (DataRow drowAttempt in dtableAttempts.Select(Schema.Assessment.COL_ASSESSMENT_ID + "='" + entAssessment.ID + "'"))
                        {
                            entAttempt = new UserAssessmentTracking();
                            entAttempt = FillAttempt(drowAttempt);
                            if (dset.Tables.Count == 6)
                            {
                                foreach (DataRow drowResponse in dtableResponse.Select(Schema.UserAssessmentTracking.COL_ATTEMPT_ID + "='" + entAttempt.ID + "'"))
                                {
                                    entResponse = new UserAssessmentSessionResponses();
                                    entResponse = FillResponse(drowResponse);
                                    entAttempt.UserSessionResponse.Add(entResponse);
                                }
                            }
                            entAssessment.UserAttemptTracking.Add(entAttempt);
                        }
                    }
                    foreach (DataRow drowSection in dtableSections.Select(Schema.Assessment.COL_ASSESSMENT_ID + "='" + entAssessment.ID + "'"))
                    {
                        entSection = new AssessmentSections();
                        entSection = FillSection(drowSection);

                        foreach (DataRow drowQuestion in dtableQuestions.Select(Schema.AssessmentQuestion.COL_SECTION_ID + "='" + entSection.ID + "'"))
                        {
                            entQuestion = new AssessmentQuestion();
                            entQuestion = FillQuestion(drowQuestion);
                            foreach (DataRow drowOption in dtableOptions.Select(Schema.AssessmentQuestion.COL_QUESTION_ID + "='" + entQuestion.ID + "'"))
                            {
                                entOption = new AssessmentOptions();
                                entOption = FillOption(drowOption);
                                entQuestion.AssessmentOptions.Add(entOption);
                            }
                            entSection.AssessmentQuestion.Add(entQuestion);
                        }
                        entAssessment.Sections.Add(entSection);
                    }
                    entListAssessment.Add(entAssessment);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListAssessment;
        }



        /// <summary>
        /// Get Assessment data with tracking infomration.
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns></returns>
        public List<AssessmentDates> GetAssessmentTrackingPreviewAssessment(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            List<AssessmentDates> entListAssessment = new List<AssessmentDates>();
            AssessmentDates entAssessment = null;
            AssessmentSections entSection = null;
            AssessmentQuestion entQuestion = null;
            AssessmentOptions entOption = null;

            UserAssessmentTracking entAttempt = null;
            UserAssessmentSessionResponses entResponse = null;

            _sqlcmd = new SqlCommand();
            DataSet dset = new DataSet();
            DataTable dtableAssessment;
            DataTable dtableSections;
            DataTable dtableQuestions;
            DataTable dtableOptions;
            DataTable dtableAttempts = null;
            DataTable dtableResponse = null;


            _sqlcmd.CommandText = Schema.Assessment.PROC_GET_ASSESSMENT_TRACKING_PREVIEWASSESSMENT;


            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);

                if (!string.IsNullOrEmpty(pEntAssessment.ID))
                {
                    _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntAssessment.UserAttemptTracking != null && pEntAssessment.UserAttemptTracking.Count > 0)
                {
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntAssessment.UserAttemptTracking[0].SystemUserGUID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntAssessment.UserAttemptTracking != null && pEntAssessment.UserAttemptTracking.Count > 0)
                {

                    if (pEntAssessment.UserAttemptTracking[0].IsForAdminPreview)
                    {
                        _sqlpara = new SqlParameter(Schema.UserAssessmentTracking.PARA_IS_FOR_ADMIN_PREVIEW, pEntAssessment.UserAttemptTracking[0].IsForAdminPreview);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (pEntAssessment.UserAttemptTracking[0].ID != null)
                    {
                        _sqlpara = new SqlParameter(Schema.UserAssessmentTracking.PARA_ATTEMPT_ID, pEntAssessment.UserAttemptTracking[0].ID);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                }
                if (!string.IsNullOrEmpty(pEntAssessment.ParaQuestionId))
                {
                    _sqlpara = new SqlParameter(Schema.AssessmentQuestion.PARA_QUESTION_ID, pEntAssessment.ParaQuestionId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntAssessment.LanguageId))
                {
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAssessment.LanguageId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }


                dset = _sqlObject.SqlDataAdapter(_sqlcmd, _strConnString);
                dtableAssessment = dset.Tables[0];
                dtableSections = dset.Tables[1];
                dtableQuestions = dset.Tables[2];
                dtableOptions = dset.Tables[3];
                if (dset.Tables.Count > 4)
                { dtableAttempts = dset.Tables[4]; }
                if (dset.Tables.Count == 6)
                    dtableResponse = dset.Tables[5];
                foreach (DataRow drow in dtableAssessment.Rows)
                {
                    entAssessment = new AssessmentDates();
                    entAssessment = FillAssessment(drow);
                    if (dset.Tables.Count > 4)
                    {
                        foreach (DataRow drowAttempt in dtableAttempts.Select(Schema.Assessment.COL_ASSESSMENT_ID + "='" + entAssessment.ID + "'"))
                        {
                            entAttempt = new UserAssessmentTracking();
                            entAttempt = FillAttempt(drowAttempt);
                            if (dset.Tables.Count == 6)
                            {
                                foreach (DataRow drowResponse in dtableResponse.Select(Schema.UserAssessmentTracking.COL_ATTEMPT_ID + "='" + entAttempt.ID + "'"))
                                {
                                    entResponse = new UserAssessmentSessionResponses();
                                    entResponse = FillResponse(drowResponse);
                                    entAttempt.UserSessionResponse.Add(entResponse);
                                }
                            }
                            entAssessment.UserAttemptTracking.Add(entAttempt);
                        }
                    }
                    foreach (DataRow drowSection in dtableSections.Select(Schema.Assessment.COL_ASSESSMENT_ID + "='" + entAssessment.ID + "'"))
                    {
                        entSection = new AssessmentSections();
                        entSection = FillSection(drowSection);

                        foreach (DataRow drowQuestion in dtableQuestions.Select(Schema.AssessmentQuestion.COL_SECTION_ID + "='" + entSection.ID + "'"))
                        {
                            entQuestion = new AssessmentQuestion();
                            entQuestion = FillQuestion(drowQuestion);
                            foreach (DataRow drowOption in dtableOptions.Select(Schema.AssessmentQuestion.COL_QUESTION_ID + "='" + entQuestion.ID + "'"))
                            {
                                entOption = new AssessmentOptions();
                                entOption = FillOption(drowOption);
                                entQuestion.AssessmentOptions.Add(entOption);
                            }
                            entSection.AssessmentQuestion.Add(entQuestion);
                        }
                        entAssessment.Sections.Add(entSection);
                    }
                    entListAssessment.Add(entAssessment);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListAssessment;
        }

        /// <summary>
        /// Get Assessment data with tracking infomration.
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns></returns>
        public List<AssessmentDates> GetAssessmentTrackingWithOutPaging(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            List<AssessmentDates> entListAssessment = new List<AssessmentDates>();
            AssessmentDates entAssessment = null;
            AssessmentSections entSection = null;
            AssessmentQuestion entQuestion = null;
            AssessmentOptions entOption = null;

            UserAssessmentTracking entAttempt = null;
            UserAssessmentSessionResponses entResponse = null;

            _sqlcmd = new SqlCommand();
            DataSet dset = new DataSet();
            DataTable dtableAssessment;
            DataTable dtableSections;
            DataTable dtableQuestions;
            DataTable dtableOptions;
            DataTable dtableAttempts = null;
            DataTable dtableResponse = null;

            _sqlcmd.CommandText = Schema.Assessment.PROC_GET_ASSESSMENT_TRACKING_WITHOUT_PAGING;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);

                if (!string.IsNullOrEmpty(pEntAssessment.ID))
                {
                    _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntAssessment.UserAttemptTracking != null && pEntAssessment.UserAttemptTracking.Count > 0)
                {
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntAssessment.UserAttemptTracking[0].SystemUserGUID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntAssessment.ParaQuestionId))
                {
                    _sqlpara = new SqlParameter(Schema.AssessmentQuestion.PARA_QUESTION_ID, pEntAssessment.ParaQuestionId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntAssessment.LanguageId))
                {
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAssessment.LanguageId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                dset = _sqlObject.SqlDataAdapter(_sqlcmd, _strConnString);
                dtableAssessment = dset.Tables[0];
                dtableSections = dset.Tables[1];
                dtableQuestions = dset.Tables[2];
                dtableOptions = dset.Tables[3];
                if (dset.Tables.Count > 4)
                { dtableAttempts = dset.Tables[4]; }
                if (dset.Tables.Count == 6)
                    dtableResponse = dset.Tables[5];
                foreach (DataRow drow in dtableAssessment.Rows)
                {
                    entAssessment = new AssessmentDates();
                    entAssessment = FillAssessmentWithOutPaging(drow);
                    if (dset.Tables.Count > 4)
                    {
                        foreach (DataRow drowAttempt in dtableAttempts.Select(Schema.Assessment.COL_ASSESSMENT_ID + "='" + entAssessment.ID + "'"))
                        {
                            entAttempt = new UserAssessmentTracking();
                            entAttempt = FillAttempt(drowAttempt);
                            if (dset.Tables.Count == 6)
                            {
                                foreach (DataRow drowResponse in dtableResponse.Select(Schema.UserAssessmentTracking.COL_ATTEMPT_ID + "='" + entAttempt.ID + "'"))
                                {
                                    entResponse = new UserAssessmentSessionResponses();
                                    entResponse = FillResponse(drowResponse);
                                    entAttempt.UserSessionResponse.Add(entResponse);
                                }
                            }
                            entAssessment.UserAttemptTracking.Add(entAttempt);
                        }
                    }
                    foreach (DataRow drowSection in dtableSections.Select(Schema.Assessment.COL_ASSESSMENT_ID + "='" + entAssessment.ID + "'"))
                    {
                        entSection = new AssessmentSections();
                        entSection = FillSectionWithOutPaging(drowSection);

                        foreach (DataRow drowQuestion in dtableQuestions.Select(Schema.AssessmentQuestion.COL_SECTION_ID + "='" + entSection.ID + "'"))
                        {
                            entQuestion = new AssessmentQuestion();
                            entQuestion = FillQuestionWithOutPaging(drowQuestion);
                            foreach (DataRow drowOption in dtableOptions.Select(Schema.AssessmentQuestion.COL_QUESTION_ID + "='" + entQuestion.ID + "'"))
                            {
                                entOption = new AssessmentOptions();
                                entOption = FillOptionWithOutPaging(drowOption);
                                entQuestion.AssessmentOptions.Add(entOption);
                            }
                            entSection.AssessmentQuestion.Add(entQuestion);
                        }
                        entAssessment.Sections.Add(entSection);
                    }
                    entListAssessment.Add(entAssessment);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListAssessment;
        }


        /// <summary>
        /// Fill Assessment object from DataRow.
        /// </summary>
        /// <param name="pDataRow"></param>
        /// <returns></returns>
        private AssessmentDates FillAssessment(DataRow pDataRow)
        {
            AssessmentDates entAssessment = new AssessmentDates();
            try
            {
                entAssessment.ID = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_ID]);
                entAssessment.AssessmentDescription = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_DESC]);
                entAssessment.AssessmentInstructionBottom = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_INST_BOT]);
                entAssessment.AssessmentInstructionTop = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_INST_TOP]);
                entAssessment.AssessmentTitle = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_TITLE]);
                entAssessment.AssessmentType = (AssessmentDates.AssessmentDisplayType)Enum.Parse(typeof(AssessmentDates.AssessmentDisplayType), Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_TYPE]));
                entAssessment.ReviewEmail = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_REVIEW_EMAIL]);

                entAssessment.AllowUserLangSelection = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_ALLOW_USER_LANGSELECTION]);
                entAssessment.AllowSequencing = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_ALLOW_SEQUENCING]);
                entAssessment.ApprovalStatus = (AssessmentDates.AssessmentApprovalStatus)Enum.Parse(typeof(AssessmentDates.AssessmentApprovalStatus), Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_APPROVAL_STATUS]));
                if (!string.IsNullOrEmpty(Convert.ToString(pDataRow[Schema.Assessment.COL_LOGO_POSITION])))
                    entAssessment.LogoPosition = (AssessmentDates.AssessmentLogoPosition)Enum.Parse(typeof(AssessmentDates.AssessmentLogoPosition), Convert.ToString(pDataRow[Schema.Assessment.COL_LOGO_POSITION]));
                if (!string.IsNullOrEmpty(Convert.ToString(pDataRow[Schema.Assessment.COL_TRACKING_TYPE])))
                    entAssessment.TrackingType = (AssessmentDates.AssessmentSubmitType)Enum.Parse(typeof(AssessmentDates.AssessmentSubmitType), Convert.ToString(pDataRow[Schema.Assessment.COL_TRACKING_TYPE]));
                entAssessment.ApprovedById = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_APPROVED_BY_ID]);
                entAssessment.BGColor = Convert.ToString(pDataRow[Schema.Assessment.COL_BGCOLOR]);
                entAssessment.ButtonExitTxt = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_BTN_EXIT_TXT]);
                entAssessment.ButtonNextTxt = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_BTN_NEXT_TXT]);
                entAssessment.ButtonPreviousTxt = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_BTN_PREVIOUS_TXT]);
                entAssessment.ButtonSubmitTxt = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_BTN_SUBMIT_TXT]);
                entAssessment.ButtonSaveTxt = Convert.ToString(pDataRow[Schema.Assessment.COL_BTN_SAVE_TXT]);
                entAssessment.ButtonPrintTxt = Convert.ToString(pDataRow[Schema.Assessment.COL_BUTTONPRINTTXT]);
                if (!string.IsNullOrEmpty(Convert.ToString(pDataRow[Schema.Assessment.COL_IS_LEARNERPRINTABLE])))
                    entAssessment.IsLearnerPrintable = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_LEARNERPRINTABLE]);
                entAssessment.CFForReviewEmail = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_CF_REVIEW_EMAIL]);

                entAssessment.CreatedById = Convert.ToString(pDataRow[Schema.Common.COL_CREATED_BY]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_DATE_APPROVED].ToString()))
                    entAssessment.DateApproved = Convert.ToDateTime(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_DATE_APPROVED]);
                entAssessment.DateCreated = Convert.ToDateTime(pDataRow[Schema.Common.COL_CREATED_ON]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_REVIEW_SENT_DATE].ToString()))
                    entAssessment.DateLastReviewSent = Convert.ToDateTime(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_REVIEW_SENT_DATE]);

                entAssessment.DefaultLogoPath = Convert.ToString(pDataRow[Schema.Assessment.COL_DEFAULTLOGOPATH]);
                entAssessment.IsActive = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_ACTIVE]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_LOGO_ONALL].ToString()))
                    entAssessment.IsLogoOnAll = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_LOGO_ONALL]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_PRINT_CERTIFICATE].ToString()))
                    entAssessment.IsPrintCertificate = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_PRINT_CERTIFICATE]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_MULTI_SUBMITALLOWED].ToString()))
                    entAssessment.IsMultiSubmitAllowed = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_MULTI_SUBMITALLOWED]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_PARTIAL_SUBMITALLOWED].ToString()))
                    entAssessment.IsPartialSubmitAllowed = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_PARTIAL_SUBMITALLOWED]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_REVIEW_ANSWER].ToString()))
                    entAssessment.IsReviewAnswer = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_REVIEW_ANSWER]);
                entAssessment.LanguageId = Convert.ToString(pDataRow[Schema.Language.COL_LANGUAGE_ID]);
                entAssessment.LanguageLogoPath = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_LOGO_PATH]);
                entAssessment.LastModifiedById = Convert.ToString(pDataRow[Schema.Common.COL_MODIFIED_BY]);
                entAssessment.LastModifiedDate = Convert.ToDateTime(pDataRow[Schema.Common.COL_MODIFIED_ON]);
                entAssessment.IsLocked = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_LOCKED]);
                //-- new col added
                entAssessment.MaxResponseLength = Convert.ToInt32(pDataRow[Schema.Assessment.COL_MAX_RESPONSE_LENGTH]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_USESECTIONS].ToString()))
                    entAssessment.IsUseSections = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_USESECTIONS]);

                entAssessment.BGColorHF = Convert.ToString(pDataRow[Schema.Assessment.COL_BGHEADER_HF]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_LOGOHIDE].ToString()))
                    entAssessment.IsLogoHide = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_LOGOHIDE]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_LOGOONFIRSTPAGEONLY].ToString()))
                    entAssessment.IsLogoOnFirstPageOnly = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_LOGOONFIRSTPAGEONLY]);

                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_IS_ALL_ANSWER_MUST))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_ALL_ANSWER_MUST].ToString()))
                        entAssessment.IsAllAnswerMust = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_ALL_ANSWER_MUST]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_IS_SHOW_QUESTIONNUMBER))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_SHOW_QUESTIONNUMBER].ToString()))
                        entAssessment.IsShowQuestionNumber = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_SHOW_QUESTIONNUMBER]);
                }


                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_FONT_NAME))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_FONT_NAME].ToString()))
                        entAssessment.FontName = Convert.ToString(pDataRow[Schema.Assessment.COL_FONT_NAME]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_FONT_SIZE))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_FONT_SIZE].ToString()))
                        entAssessment.FontSize = Convert.ToInt32(pDataRow[Schema.Assessment.COL_FONT_SIZE]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_FONT_COLOR))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_FONT_COLOR].ToString()))
                        entAssessment.FontColor = Convert.ToString(pDataRow[Schema.Assessment.COL_FONT_COLOR]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_QUEBG_COLOR))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_QUEBG_COLOR].ToString()))
                        entAssessment.QuestionaireBGColor = Convert.ToString(pDataRow[Schema.Assessment.COL_QUEBG_COLOR]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_ASSESSMENTTIME))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_ASSESSMENTTIME].ToString()))
                        entAssessment.AssessmentTime = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENTTIME]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_ASSESSMENTALERTTIME))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_ASSESSMENTALERTTIME].ToString()))
                        entAssessment.AssessmentAlertTime = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENTALERTTIME]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_NUMBER_OF_ATTEMPTS))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_NUMBER_OF_ATTEMPTS].ToString()))
                        entAssessment.AssessmentNumberOfAttempts = Convert.ToString(pDataRow[Schema.Assessment.COL_NUMBER_OF_ATTEMPTS]);
                }
                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_SHOW_ASSESSMENT_RESULT))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_SHOW_ASSESSMENT_RESULT].ToString()))
                        entAssessment.ShowAssessmentResult = (AssessmentDates.ShowAssessmentResultType)Enum.Parse(typeof(AssessmentDates.ShowAssessmentResultType), Convert.ToString(pDataRow[Schema.Assessment.COL_SHOW_ASSESSMENT_RESULT]));
                }
                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_QUESTION_COUNT))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_QUESTION_COUNT].ToString()))
                        entAssessment.QuestionCount = Convert.ToInt32(pDataRow[Schema.Assessment.COL_QUESTION_COUNT]);
                }
                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_SEND_EMAIL_OF_RESULT))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_SEND_EMAIL_OF_RESULT].ToString()))
                        entAssessment.IsSendEmailOfResult = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_SEND_EMAIL_OF_RESULT]);
                }
                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_Is_Complete_Assignment_Dep_On_Scrore))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_Is_Complete_Assignment_Dep_On_Scrore].ToString()))
                        entAssessment.IsCompleteAssignmentDepOnScrore = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_Is_Complete_Assignment_Dep_On_Scrore]);
                }
                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_Passing_Score))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_Passing_Score].ToString()))
                        entAssessment.PassingScore = Convert.ToDouble(pDataRow[Schema.Assessment.COL_Passing_Score]);
                }
                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_IS_INCORRECT_QUESTIONS))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_INCORRECT_QUESTIONS].ToString()))
                        entAssessment.IsIncorrectQuestions = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_INCORRECT_QUESTIONS]);
                }
                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_SendEmailTo))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_SendEmailTo].ToString()))
                        entAssessment.SendEmailTo = Convert.ToString(pDataRow[Schema.Assessment.COL_SendEmailTo]);
                }
                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_SendEmailToAdminUser))
                {
                    if (pDataRow[Schema.Assessment.COL_SendEmailToAdminUser] != null)
                        entAssessment.SendEmailToAdminUser = Convert.ToInt32(pDataRow[Schema.Assessment.COL_SendEmailToAdminUser]);
                }
                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_SEND_EMAIL_OF_RESULT))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_SEND_EMAIL_OF_RESULT].ToString()))
                        entAssessment.IsSendEmailOfResult = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_SEND_EMAIL_OF_RESULT]);
                }
                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_AllowQueOptionRandamization))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_AllowQueOptionRandamization].ToString()))
                        entAssessment.AllowOptionRandamization = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_AllowQueOptionRandamization]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_ISBOOK_MARKING))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_ISBOOK_MARKING].ToString()))
                        entAssessment.IsBookmarking = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_ISBOOK_MARKING]);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return entAssessment;

        }

        /// <summary>
        /// Fill Assessment object from DataRow.
        /// </summary>
        /// <param name="pDataRow"></param>
        /// <returns></returns>
        private AssessmentDates FillAssessmentWithOutPaging(DataRow pDataRow)
        {
            AssessmentDates entAssessment = new AssessmentDates();
            try
            {
                entAssessment.ID = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_ID]);
                entAssessment.AssessmentDescription = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_DESC]);
                entAssessment.AssessmentInstructionBottom = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_INST_BOT]);
                entAssessment.AssessmentInstructionTop = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_INST_TOP]);
                entAssessment.AssessmentTitle = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_TITLE]);
                entAssessment.AssessmentType = (AssessmentDates.AssessmentDisplayType)Enum.Parse(typeof(AssessmentDates.AssessmentDisplayType), Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_TYPE]));
                entAssessment.ReviewEmail = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_REVIEW_EMAIL]);

                entAssessment.AllowUserLangSelection = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_ALLOW_USER_LANGSELECTION]);
                entAssessment.AllowSequencing = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_ALLOW_SEQUENCING]);
                entAssessment.ApprovalStatus = (AssessmentDates.AssessmentApprovalStatus)Enum.Parse(typeof(AssessmentDates.AssessmentApprovalStatus), Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_APPROVAL_STATUS]));
                if (!string.IsNullOrEmpty(Convert.ToString(pDataRow[Schema.Assessment.COL_LOGO_POSITION])))
                    entAssessment.LogoPosition = (AssessmentDates.AssessmentLogoPosition)Enum.Parse(typeof(AssessmentDates.AssessmentLogoPosition), Convert.ToString(pDataRow[Schema.Assessment.COL_LOGO_POSITION]));
                if (!string.IsNullOrEmpty(Convert.ToString(pDataRow[Schema.Assessment.COL_TRACKING_TYPE])))
                    entAssessment.TrackingType = (AssessmentDates.AssessmentSubmitType)Enum.Parse(typeof(AssessmentDates.AssessmentSubmitType), Convert.ToString(pDataRow[Schema.Assessment.COL_TRACKING_TYPE]));
                entAssessment.ApprovedById = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_APPROVED_BY_ID]);
                entAssessment.BGColor = Convert.ToString(pDataRow[Schema.Assessment.COL_BGCOLOR]);
                entAssessment.ButtonExitTxt = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_BTN_EXIT_TXT]);
                entAssessment.ButtonNextTxt = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_BTN_NEXT_TXT]);
                entAssessment.ButtonPreviousTxt = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_BTN_PREVIOUS_TXT]);
                entAssessment.ButtonSubmitTxt = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_BTN_SUBMIT_TXT]);
                entAssessment.ButtonSaveTxt = Convert.ToString(pDataRow[Schema.Assessment.COL_BTN_SAVE_TXT]);

                if (!string.IsNullOrEmpty(Convert.ToString(pDataRow[Schema.Assessment.COL_BUTTONPRINTTXT])))
                    entAssessment.ButtonPrintTxt = Convert.ToString(pDataRow[Schema.Assessment.COL_BUTTONPRINTTXT]);

                if (!string.IsNullOrEmpty(Convert.ToString(pDataRow[Schema.Assessment.COL_IS_LEARNERPRINTABLE])))
                    entAssessment.IsLearnerPrintable = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_LEARNERPRINTABLE]);

                entAssessment.CFForReviewEmail = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_CF_REVIEW_EMAIL]);


                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_DATE_APPROVED].ToString()))
                    entAssessment.DateApproved = Convert.ToDateTime(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_DATE_APPROVED]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_REVIEW_SENT_DATE].ToString()))
                    entAssessment.DateLastReviewSent = Convert.ToDateTime(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_REVIEW_SENT_DATE]);

                entAssessment.DefaultLogoPath = Convert.ToString(pDataRow[Schema.Assessment.COL_DEFAULTLOGOPATH]);
                entAssessment.IsActive = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_ACTIVE]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_LOGO_ONALL].ToString()))
                    entAssessment.IsLogoOnAll = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_LOGO_ONALL]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_PRINT_CERTIFICATE].ToString()))
                    entAssessment.IsPrintCertificate = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_PRINT_CERTIFICATE]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_MULTI_SUBMITALLOWED].ToString()))
                    entAssessment.IsMultiSubmitAllowed = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_MULTI_SUBMITALLOWED]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_PARTIAL_SUBMITALLOWED].ToString()))
                    entAssessment.IsPartialSubmitAllowed = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_PARTIAL_SUBMITALLOWED]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_REVIEW_ANSWER].ToString()))
                    entAssessment.IsReviewAnswer = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_REVIEW_ANSWER]);
                entAssessment.LanguageId = Convert.ToString(pDataRow[Schema.Language.COL_LANGUAGE_ID]);
                entAssessment.LanguageLogoPath = Convert.ToString(pDataRow[Schema.Assessment.COL_ASSESSMENT_LANG_LOGO_PATH]);

                //-- new col added
                entAssessment.MaxResponseLength = Convert.ToInt32(pDataRow[Schema.Assessment.COL_MAX_RESPONSE_LENGTH]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_USESECTIONS].ToString()))
                    entAssessment.IsUseSections = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_USESECTIONS]);

                entAssessment.BGColorHF = Convert.ToString(pDataRow[Schema.Assessment.COL_BGHEADER_HF]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_LOGOHIDE].ToString()))
                    entAssessment.IsLogoHide = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_LOGOHIDE]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_LOGOONFIRSTPAGEONLY].ToString()))
                    entAssessment.IsLogoOnFirstPageOnly = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_LOGOONFIRSTPAGEONLY]);

                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_IS_ALL_ANSWER_MUST))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_IS_ALL_ANSWER_MUST].ToString()))
                        entAssessment.IsAllAnswerMust = Convert.ToBoolean(pDataRow[Schema.Assessment.COL_IS_ALL_ANSWER_MUST]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_FONT_NAME))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_FONT_NAME].ToString()))
                        entAssessment.FontName = Convert.ToString(pDataRow[Schema.Assessment.COL_FONT_NAME]);
                }
                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_FONT_COLOR))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_FONT_COLOR].ToString()))
                        entAssessment.FontColor = Convert.ToString(pDataRow[Schema.Assessment.COL_FONT_COLOR]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_FONT_SIZE))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_FONT_SIZE].ToString()))
                        entAssessment.FontSize = Convert.ToInt32(pDataRow[Schema.Assessment.COL_FONT_SIZE]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Assessment.COL_QUEBG_COLOR))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Assessment.COL_QUEBG_COLOR].ToString()))
                        entAssessment.QuestionaireBGColor = Convert.ToString(pDataRow[Schema.Assessment.COL_QUEBG_COLOR]);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return entAssessment;

        }

        /// <summary>
        /// Fill Section Object
        /// </summary>
        /// <param name="pDataRow"></param>
        /// <returns></returns>
        public AssessmentSections FillSection(DataRow pDataRow)
        {
            AssessmentSections entSection = new AssessmentSections();
            try
            {
                entSection.ID = Convert.ToString(pDataRow[Schema.AssessmentSections.COL_SECTION_ID]);
                entSection.IsActive = Convert.ToBoolean(pDataRow[Schema.AssessmentSections.COL_IS_ACTIVE]);
                entSection.LanguageId = Convert.ToString(pDataRow[Schema.Language.COL_LANGUAGE_ID]);
                entSection.AssessmentId = Convert.ToString(pDataRow[Schema.AssessmentSections.COL_Assessment_ID]);
                entSection.SectionDescription = Convert.ToString(pDataRow[Schema.AssessmentSections.COL_SECTION_DESC]);
                entSection.SectionName = Convert.ToString(pDataRow[Schema.AssessmentSections.COL_SECTION_NAME]);
                entSection.SequenceOrder = Convert.ToInt32(pDataRow[Schema.AssessmentSections.COL_SEQUENCE_ORDER]);
                entSection.CreatedById = Convert.ToString(pDataRow[Schema.Common.COL_CREATED_BY]);
                entSection.DateCreated = Convert.ToDateTime(pDataRow[Schema.Common.COL_CREATED_ON]);
                entSection.LastModifiedById = Convert.ToString(pDataRow[Schema.Common.COL_MODIFIED_BY]);
                entSection.LastModifiedDate = Convert.ToDateTime(pDataRow[Schema.Common.COL_MODIFIED_ON]);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return entSection;

        }

        /// <summary>
        /// Fill Section Object
        /// </summary>
        /// <param name="pDataRow"></param>
        /// <returns></returns>
        public AssessmentSections FillSectionWithOutPaging(DataRow pDataRow)
        {
            AssessmentSections entSection = new AssessmentSections();
            try
            {
                entSection.ID = Convert.ToString(pDataRow[Schema.AssessmentSections.COL_SECTION_ID]);
                entSection.IsActive = Convert.ToBoolean(pDataRow[Schema.AssessmentSections.COL_IS_ACTIVE]);
                entSection.LanguageId = Convert.ToString(pDataRow[Schema.Language.COL_LANGUAGE_ID]);
                entSection.AssessmentId = Convert.ToString(pDataRow[Schema.AssessmentSections.COL_Assessment_ID]);
                entSection.SectionDescription = Convert.ToString(pDataRow[Schema.AssessmentSections.COL_SECTION_DESC]);
                entSection.SectionName = Convert.ToString(pDataRow[Schema.AssessmentSections.COL_SECTION_NAME]);
                entSection.SequenceOrder = Convert.ToInt32(pDataRow[Schema.AssessmentSections.COL_SEQUENCE_ORDER]);

            }
            catch (Exception exc)
            {
                throw exc;
            }
            return entSection;

        }

        /// <summary>
        /// Fill Question object
        /// </summary>
        /// <param name="pDataRow"></param>
        /// <returns></returns>
        public AssessmentQuestion FillQuestion(DataRow pDataRow)
        {
            AssessmentQuestion entQuestion = new AssessmentQuestion();
            try
            {
                entQuestion.ID = Convert.ToString(pDataRow[Schema.AssessmentQuestion.COL_QUESTION_ID]);
                entQuestion.IsActive = Convert.ToBoolean(pDataRow[Schema.AssessmentQuestion.COL_IS_ACTIVE]);
                entQuestion.IsMappingActive = Convert.ToBoolean(pDataRow[Schema.AssessmentQuestion.COL_IS_MAPPING_ACTIVE]);
                entQuestion.LanguageId = Convert.ToString(pDataRow[Schema.Language.COL_LANGUAGE_ID]);
                entQuestion.QuestionDescription = Convert.ToString(pDataRow[Schema.AssessmentQuestion.COL_QUESTION_DESC]);
                entQuestion.AssessmentID = Convert.ToString(pDataRow[Schema.AssessmentQuestion.COL_ASSESSMENT_ID]);
                entQuestion.QuestionTitle = Convert.ToString(pDataRow[Schema.AssessmentQuestion.COL_QUESTION_TITLE]);
                entQuestion.QuestionType = (AssessmentQuestion.AssessmentQuestionType)Enum.Parse(typeof(AssessmentQuestion.AssessmentQuestionType), Convert.ToString(pDataRow[Schema.AssessmentQuestion.COL_QUESTION_TYPE]));
                entQuestion.SectionID = Convert.ToString(pDataRow[Schema.AssessmentQuestion.COL_SECTION_ID]);
                entQuestion.SequenceOrder = Convert.ToInt32(pDataRow[Schema.AssessmentQuestion.COL_SEQUENCE_ORDER]);
                entQuestion.CreatedById = Convert.ToString(pDataRow[Schema.Common.COL_CREATED_BY]);
                entQuestion.DateCreated = Convert.ToDateTime(pDataRow[Schema.Common.COL_CREATED_ON]);
                entQuestion.LastModifiedById = Convert.ToString(pDataRow[Schema.Common.COL_MODIFIED_BY]);
                entQuestion.LastModifiedDate = Convert.ToDateTime(pDataRow[Schema.Common.COL_MODIFIED_ON]);

            }
            catch (Exception exc)
            {
                throw exc;
            }
            return entQuestion;
        }

        /// <summary>
        /// Fill Question object
        /// </summary>
        /// <param name="pDataRow"></param>
        /// <returns></returns>
        public AssessmentQuestion FillQuestionWithOutPaging(DataRow pDataRow)
        {
            AssessmentQuestion entQuestion = new AssessmentQuestion();
            try
            {
                entQuestion.ID = Convert.ToString(pDataRow[Schema.AssessmentQuestion.COL_QUESTION_ID]);
                entQuestion.IsActive = Convert.ToBoolean(pDataRow[Schema.AssessmentQuestion.COL_IS_ACTIVE]);
                entQuestion.LanguageId = Convert.ToString(pDataRow[Schema.Language.COL_LANGUAGE_ID]);
                entQuestion.QuestionDescription = Convert.ToString(pDataRow[Schema.AssessmentQuestion.COL_QUESTION_DESC]);
                entQuestion.AssessmentID = Convert.ToString(pDataRow[Schema.AssessmentQuestion.COL_ASSESSMENT_ID]);
                entQuestion.QuestionTitle = Convert.ToString(pDataRow[Schema.AssessmentQuestion.COL_QUESTION_TITLE]);
                entQuestion.QuestionType = (AssessmentQuestion.AssessmentQuestionType)Enum.Parse(typeof(AssessmentQuestion.AssessmentQuestionType), Convert.ToString(pDataRow[Schema.AssessmentQuestion.COL_QUESTION_TYPE]));
                entQuestion.SectionID = Convert.ToString(pDataRow[Schema.AssessmentQuestion.COL_SECTION_ID]);
                entQuestion.SequenceOrder = Convert.ToInt32(pDataRow[Schema.AssessmentQuestion.COL_SEQUENCE_ORDER]);

            }
            catch (Exception exc)
            {
                throw exc;
            }
            return entQuestion;
        }


        /// <summary>
        /// Fill Option
        /// </summary>
        /// <param name="pDataRow"></param>
        /// <returns></returns>
        private AssessmentOptions FillOption(DataRow pDataRow)
        {
            AssessmentOptions entOption = new AssessmentOptions();
            try
            {
                entOption.ID = Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_OPTION_ID]);
                entOption.QuestionID = Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_QUESTION_ID]);
                entOption.AssessmentId = Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_ASSESSMENT_ID]);
                entOption.SectionID = Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_SECTION_ID]);
                entOption.LanguageId = Convert.ToString(pDataRow[Schema.Language.COL_LANGUAGE_ID]);
                entOption.OptionDescription = Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_OPTION_DESC]);
                entOption.OptionTitle = Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_OPTION_TITLE]);
                entOption.SequenceOrder = Convert.ToInt32(pDataRow[Schema.AssessmentOptions.COL_SEQUENCE_ORDER]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.AssessmentOptions.COL_OPTION_TYPE].ToString()))
                    entOption.OptionType = (AssessmentOptions.AssessmentOptionType)Enum.Parse(typeof(AssessmentOptions.AssessmentOptionType), Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_OPTION_TYPE]));
                entOption.GoToQuestion = Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_GO_TO_QUESTION]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.AssessmentOptions.COL_IS_ACTIVE].ToString()))
                    entOption.IsActive = Convert.ToBoolean(pDataRow[Schema.AssessmentOptions.COL_IS_ACTIVE]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.AssessmentOptions.COL_IS_ALERT].ToString()))
                    entOption.IsAlert = Convert.ToBoolean(pDataRow[Schema.AssessmentOptions.COL_IS_ALERT]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.AssessmentOptions.COL_IS_EXPLANATION].ToString()))
                    entOption.IsExplanation = Convert.ToBoolean(pDataRow[Schema.AssessmentOptions.COL_IS_EXPLANATION]);
                entOption.ExplainationTitle = Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_EXPLAINATION_TITLE]);
                entOption.CreatedById = Convert.ToString(pDataRow[Schema.Common.COL_CREATED_BY]);
                entOption.DateCreated = Convert.ToDateTime(pDataRow[Schema.Common.COL_CREATED_ON]);
                entOption.LastModifiedById = Convert.ToString(pDataRow[Schema.Common.COL_MODIFIED_BY]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.Common.COL_MODIFIED_ON].ToString()))
                    entOption.LastModifiedDate = Convert.ToDateTime(pDataRow[Schema.Common.COL_MODIFIED_ON]);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return entOption;
        }

        /// <summary>
        /// Fill Option
        /// </summary>
        /// <param name="pDataRow"></param>
        /// <returns></returns>
        private AssessmentOptions FillOptionWithOutPaging(DataRow pDataRow)
        {
            AssessmentOptions entOption = new AssessmentOptions();
            try
            {
                entOption.ID = Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_OPTION_ID]);
                entOption.QuestionID = Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_QUESTION_ID]);
                entOption.AssessmentId = Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_ASSESSMENT_ID]);
                entOption.SectionID = Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_SECTION_ID]);
                entOption.LanguageId = Convert.ToString(pDataRow[Schema.Language.COL_LANGUAGE_ID]);
                entOption.OptionDescription = Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_OPTION_DESC]);
                entOption.OptionTitle = Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_OPTION_TITLE]);
                entOption.SequenceOrder = Convert.ToInt32(pDataRow[Schema.AssessmentOptions.COL_SEQUENCE_ORDER]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.AssessmentOptions.COL_OPTION_TYPE].ToString()))
                    entOption.OptionType = (AssessmentOptions.AssessmentOptionType)Enum.Parse(typeof(AssessmentOptions.AssessmentOptionType), Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_OPTION_TYPE]));
                entOption.GoToQuestion = Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_GO_TO_QUESTION]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.AssessmentOptions.COL_IS_ACTIVE].ToString()))
                    entOption.IsActive = Convert.ToBoolean(pDataRow[Schema.AssessmentOptions.COL_IS_ACTIVE]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.AssessmentOptions.COL_IS_ALERT].ToString()))
                    entOption.IsAlert = Convert.ToBoolean(pDataRow[Schema.AssessmentOptions.COL_IS_ALERT]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.AssessmentOptions.COL_IS_EXPLANATION].ToString()))
                    entOption.IsExplanation = Convert.ToBoolean(pDataRow[Schema.AssessmentOptions.COL_IS_EXPLANATION]);
                entOption.ExplainationTitle = Convert.ToString(pDataRow[Schema.AssessmentOptions.COL_EXPLAINATION_TITLE]);

            }
            catch (Exception exc)
            {
                throw exc;
            }
            return entOption;
        }


        /// <summary>
        /// To fill Attempt data
        /// </summary>
        /// <param name="pDataRow"></param>
        /// <returns></returns>
        public UserAssessmentTracking FillAttempt(DataRow pDataRow)
        {
            UserAssessmentTracking entAttempt = new UserAssessmentTracking();
            try
            {
                entAttempt.ID = Convert.ToString(pDataRow[Schema.UserAssessmentTracking.COL_ATTEMPT_ID]);
                entAttempt.AssessmentId = Convert.ToString(pDataRow[Schema.UserAssessmentTracking.COL_ASSESSMENT_ID]);
                entAttempt.AttemptLanguageId = Convert.ToString(pDataRow[Schema.UserAssessmentTracking.COL_ATTEMP_LANGUAGE_ID]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.UserAssessmentTracking.COL_COMPLETED_DATE].ToString()))
                    entAttempt.CompletatedDate = Convert.ToDateTime(pDataRow[Schema.UserAssessmentTracking.COL_COMPLETED_DATE]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.UserAssessmentTracking.COL_START_DATE].ToString()))
                    entAttempt.StartDate = Convert.ToDateTime(pDataRow[Schema.UserAssessmentTracking.COL_START_DATE]);
                entAttempt.SubmissionStatus = (ActivityCompletionStatus)Enum.Parse(typeof(ActivityCompletionStatus), Convert.ToString(pDataRow[Schema.UserAssessmentTracking.COL_SUBMISSION_STATUS]));
                entAttempt.SystemUserGUID = Convert.ToString(pDataRow[Schema.Learner.COL_USER_ID]);
                entAttempt.DisplaySubmissionStatus = Convert.ToString(pDataRow[Schema.UserAssessmentTracking.COL_Display_SUBMISSION_STATUS]);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return entAttempt;
        }

        /// <summary>
        /// To fill user session response data
        /// </summary>
        /// <param name="pDataRow"></param>
        /// <returns></returns>
        public UserAssessmentSessionResponses FillResponse(DataRow pDataRow)
        {
            UserAssessmentSessionResponses entResponse = new UserAssessmentSessionResponses();
            try
            {
                entResponse.ID = Convert.ToString(pDataRow[Schema.UserAssessmentSessionResponse.COL_ATTEMPT_ID]);
                entResponse.AssessmentId = Convert.ToString(pDataRow[Schema.UserAssessmentSessionResponse.COL_ASSESSMENT_ID]);
                entResponse.SectionID = Convert.ToString(pDataRow[Schema.UserAssessmentSessionResponse.COL_SECTION_ID]);
                entResponse.QuestionID = Convert.ToString(pDataRow[Schema.UserAssessmentSessionResponse.COL_QUESTION_ID]);
                entResponse.AssessmentOptionsID = Convert.ToString(pDataRow[Schema.UserAssessmentSessionResponse.COL_OPTION_ID]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.UserAssessmentSessionResponse.COL_DATE_SUBMITTED].ToString()))
                    entResponse.DateSubmitted = Convert.ToDateTime(pDataRow[Schema.UserAssessmentSessionResponse.COL_DATE_SUBMITTED]);
                entResponse.ExplanationText = Convert.ToString(pDataRow[Schema.UserAssessmentSessionResponse.COL_EXPLANATION_TEXT]);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return entResponse;
        }

        /// <summary>
        /// To get languages for Assessment import functionality.
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns></returns>
        public List<Language> GetImportLanguages(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            List<Language> entListLanguage = new List<Language>();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Assessment.PROC_GET_IMPORT_LANGUAGE;
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entListLanguage.Add(FillLanguage(_sqlreader));
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
            return entListLanguage;
        }

        /// <summary>
        /// To get list of languages for Assessment export functionality
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns></returns>
        public List<Language> GetExportLanguages(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            List<Language> entListLanguage = new List<Language>();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Assessment.PROC_GET_EXPORT_LANGUAGE;
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntAssessment.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entListLanguage.Add(FillLanguage(_sqlreader));
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
            return entListLanguage;
        }

        /// <summary>
        /// To fill language object data.
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns></returns>
        private Language FillLanguage(SqlDataReader pReader)
        {
            Language _entLanguage = new Language();
            int iIndex;
            if (pReader.HasRows)
            {
                iIndex = pReader.GetOrdinal(Schema.Language.COL_LANGUAGE_ID);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.ID = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Language.COL_LANG_ENG_NAME);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.LanguageEnglishName = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Language.COL_LANG_NAME);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.LanguageName = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Language.COL_CHAR_SET_TYPE);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.CharacterSetType = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Language.COL_TEXT_DIRECTION);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.TextDirection = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.ClientId = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.CreatedById = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.DateCreated = pReader.GetDateTime(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.LastModifiedById = pReader.GetString(iIndex);

                iIndex = pReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                if (!pReader.IsDBNull(iIndex))
                    _entLanguage.LastModifiedDate = pReader.GetDateTime(iIndex);
            }
            return _entLanguage;
        }

        /// <summary>
        /// To insert / update Assessment data.
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <param name="pStrUpdateMode"></param>
        /// <param name="pIsCopy"></param>
        /// <returns></returns>
        public AssessmentDates UpdateAssessment(AssessmentDates pEntAssessment, string pStrUpdateMode, bool pIsCopy)
        {
            _sqlObject = new SQLObject();
            AssessmentDates entAssessment = new AssessmentDates();
            SqlConnection sqlConn = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
                using (TransactionScope ts = new TransactionScope())
                {
                    sqlConn = new SqlConnection(_strConnString);
                    sqlConn.Open();
                    entAssessment = SaveAssessment(pEntAssessment, sqlConn, pStrUpdateMode, pIsCopy);
                    ts.Complete();
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlConn != null && sqlConn.State != ConnectionState.Closed) sqlConn.Close();
            }
            return entAssessment;
        }

        /// <summary>
        /// Delete Assessment From Server and DataBase
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns>Assessment Object</returns>
        public AssessmentDates DeleteAssessment(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            AssessmentDates entAssessment = new AssessmentDates();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Assessment.PROC_DELET_ASSESSMENT;
            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
                //Delete Assessment from database
                int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                pEntAssessment = null;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntAssessment;
        }

        public AssessmentDates DeleteAssessmentLanguage(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            AssessmentDates entAssessment = new AssessmentDates();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Assessment.PROC_DELET_ASSESSMENTLANGUAGE;

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAssessment.LanguageId);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
                //Delete Assessment from database
                int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                pEntAssessment = null;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntAssessment;
        }

        /// <summary>
        /// To copy Assessment with all its childs data - sections,questions,options.
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns></returns>
        public AssessmentDates CopyAssessment(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            AssessmentSectionAdaptor sectionAdaptor = new AssessmentSectionAdaptor();
            AssessmentQuestionAdaptor questionAdaptor = new AssessmentQuestionAdaptor();
            AssessmentOptionsAdaptor optionAdaptor = new AssessmentOptionsAdaptor();
            AssessmentDates entQuestioner = new AssessmentDates();
            List<BaseEntity> entListBase = new List<BaseEntity>();

            SqlConnection sqlConn = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);

                using (TransactionScope ts = new TransactionScope())
                {
                    sqlConn = new SqlConnection(_strConnString);
                    sqlConn.Open();
                    pEntAssessment.ID = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_ASSESSMENT_ID_PREFIX, Schema.Common.VAL_ASSESSMENT_ID_LENGTH);
                    entQuestioner = pEntAssessment;
                    SaveAssessment(entQuestioner, sqlConn, Schema.Common.VAL_INSERT_MODE, true);

                    StringDictionary objStrQuestionIdDic = new StringDictionary();


                    foreach (AssessmentSections objSection in pEntAssessment.Sections)
                    {
                        objSection.AssessmentId = pEntAssessment.ID;
                        objSection.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                        List<AssessmentSections> listSection = new List<AssessmentSections>();
                        listSection.Add(objSection);
                        sectionAdaptor.AddSectionWithCopy(listSection, sqlConn);

                        foreach (AssessmentQuestion objQuestion in objSection.AssessmentQuestion)
                        {
                            string strOldId = objQuestion.ID;
                            objQuestion.SectionID = objSection.ID;
                            objQuestion.AssessmentID = pEntAssessment.ID;
                        }

                    }

                    /*code change for duplicate question issue while copying records. By:rajendra on 16-apr-2010. */
                    foreach (AssessmentSections objSection in pEntAssessment.Sections)
                    {
                        AddQuestionMappingWithCopy(objSection.AssessmentQuestion, sqlConn);

                    }

                    ts.Complete();
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlConn != null && sqlConn.State != ConnectionState.Closed) sqlConn.Close();
            }
            return entQuestioner;
        }

        /// <summary>
        /// Import Assessment
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns></returns>
        public AssessmentDates ImportAssessment(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            AssessmentSectionAdaptor sectionAdaptor = new AssessmentSectionAdaptor();
            ////  AssessmentQuestionAdaptor questionAdaptor = new AssessmentQuestionAdaptor();
            // // AssessmentOptionsAdaptor optionAdaptor = new AssessmentOptionsAdaptor();
            List<AssessmentSections> entListSecLanguage = new List<AssessmentSections>();
            // //  List<AssessmentQuestion> entListQuestLanguage = new List<AssessmentQuestion>();
            ////   List<AssessmentOptions> entListOptLanguage = new List<AssessmentOptions>();
            SqlConnection sqlConn = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
                using (TransactionScope ts = new TransactionScope())
                {
                    sqlConn = new SqlConnection(_strConnString);
                    sqlConn.Open();
                    SaveAssessmentLanguage(pEntAssessment, sqlConn);
                    foreach (AssessmentSections objSection in pEntAssessment.Sections)
                    {
                        entListSecLanguage.Add(objSection);
                        ////foreach (AssessmentQuestion objQuestion in objSection.AssessmentQuestion)
                        ////{
                        ////    entListQuestLanguage.Add(objQuestion);
                        ////    foreach (AssessmentOptions objOption in objQuestion.AssessmentOptions)
                        ////    {
                        ////        objOption.AssessmentId = pEntAssessment.ID;
                        ////        entListOptLanguage.Add(objOption);
                        ////    }
                        ////}
                    }
                    sectionAdaptor.SaveImportSectionLanguages(entListSecLanguage, sqlConn);
                    ////  questionAdaptor.SaveImportQuestionLanguages(entListQuestLanguage, sqlConn);
                    ////  optionAdaptor.SaveImportOptionLanguages(entListOptLanguage, sqlConn);
                    ts.Complete();
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlConn != null && sqlConn.State != ConnectionState.Closed) sqlConn.Close();
            }
            return pEntAssessment;
        }


        /// <summary>
        /// Copy Import Assessment Languages
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns></returns>
        public AssessmentDates CopyImportAssessmentLanguages(AssessmentDates pEntAssessment)
        {
            _sqlObject = new SQLObject();
            AssessmentDates entAssessment = new AssessmentDates();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
                _sqlcmd = new SqlCommand();
                _sqlcmd.Connection = sqlConnection;
                _sqlcmd.CommandText = Schema.Assessment.PROC_ADD_IMPORT_LANGUAGE;
                _sqlcmd.Connection = sqlConnection;

                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAssessment.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assessment.PARA_BASE_LANGUAGE_ID, pEntAssessment.BaseLanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntAssessment.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

                entAssessment = GetAssessmentById(pEntAssessment);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {

            }
            return entAssessment;
        }

        /// <summary>
        /// To insert/update/copy Assessment data.
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <param name="pSqlConn"></param>
        /// <param name="pStrUpdateMode"></param>
        /// <param name="pIsCopy"></param>
        /// <returns></returns>
        private AssessmentDates SaveAssessment(AssessmentDates pEntAssessment, SqlConnection pSqlConn, string pStrUpdateMode, bool pIsCopy)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Assessment.PROC_UPDATE_ASSESSMENT;
            _sqlcmd.Connection = pSqlConn;
            if (string.IsNullOrEmpty(pEntAssessment.ID))
            {
                pEntAssessment.ID = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_ASSESSMENT_ID_PREFIX, Schema.Common.VAL_ASSESSMENT_ID_LENGTH);
            }


            // Added By rajendra for Question Number Display
            if (pStrUpdateMode == Schema.Common.VAL_INSERT_MODE && !(pIsCopy))
            {
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_SHOW_QUESTIONNUMBER, true);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            else
            {

                _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_SHOW_QUESTIONNUMBER, pEntAssessment.IsShowQuestionNumber);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, pStrUpdateMode);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_TYPE, pEntAssessment.AssessmentType.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_IS_ACTIVE, pEntAssessment.IsActive);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_IS_PRINT_CERTIFICATE, pEntAssessment.IsPrintCertificate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_LEARNERPRINTABLE, pEntAssessment.IsLearnerPrintable);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ALLOW_SEQUENCING, pEntAssessment.AllowSequencing);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_BGCOLOR, pEntAssessment.BGColor);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_DEFAULTLOGOPATH, pEntAssessment.DefaultLogoPath);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_LOGO_ONALL, pEntAssessment.IsLogoOnAll);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_PARTIAL_SUBMITALLOWED, pEntAssessment.IsPartialSubmitAllowed);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_MULTI_SUBMITALLOWED, pEntAssessment.IsMultiSubmitAllowed);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_REVIEW_ANSWER, pEntAssessment.IsReviewAnswer);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ALLOW_USER_LANGSELECTION, pEntAssessment.AllowUserLangSelection);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntAssessment.ClientId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntAssessment.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_TRACKING_TYPE, pEntAssessment.TrackingType.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_LOGO_POSITION, pEntAssessment.LogoPosition.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            //-- new col added
            _sqlpara = new SqlParameter(Schema.Assessment.PARA_MAX_RESPONSE_LENGTH, pEntAssessment.MaxResponseLength);
            _sqlcmd.Parameters.Add(_sqlpara);

            // Code added on 12-Apr-2010.
            _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_USESECTIONS, pEntAssessment.IsUseSections);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_BGHEADER_HF, pEntAssessment.BGColorHF);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_LOGOHIDE, pEntAssessment.IsLogoHide);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_LOGOONFIRSTPAGEONLY, pEntAssessment.IsLogoOnFirstPageOnly);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_ALL_ANSWER_MUST, pEntAssessment.IsAllAnswerMust);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_FONT_NAME, pEntAssessment.FontName);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_FONT_SIZE, pEntAssessment.FontSize);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_FONT_COLOR, pEntAssessment.FontColor);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_QUEBG_COLOR, pEntAssessment.QuestionaireBGColor);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENTTIME, pEntAssessment.AssessmentTime);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENTALERTTIME, pEntAssessment.AssessmentAlertTime);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_NUMBER_OF_ATTEMPTS, pEntAssessment.AssessmentNumberOfAttempts);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntAssessment.ShowAssessmentResult != AssessmentDates.ShowAssessmentResultType.Null)
            {
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_SHOW_ASSESSMENT_RESULT, pEntAssessment.ShowAssessmentResult.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            _sqlpara = new SqlParameter(Schema.Assessment.PARA_QUESTIONCOUNT, pEntAssessment.QuestionCount);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_IsSendEmailOfResult, pEntAssessment.IsSendEmailOfResult);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_IsCompleteAssignmentDepOnScrore, pEntAssessment.IsCompleteAssignmentDepOnScrore);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_PassingScore, pEntAssessment.PassingScore);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_INCORRECT_QUESTIONS, pEntAssessment.IsIncorrectQuestions);
            _sqlcmd.Parameters.Add(_sqlpara);
            _sqlpara = new SqlParameter(Schema.Assessment.PARA_AllowQueOptionRandamization, pEntAssessment.AllowOptionRandamization);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ISBOOK_MARKING, pEntAssessment.IsBookmarking);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!string.IsNullOrEmpty(pEntAssessment.SendEmailTo))
            {
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_SendEmailTo, pEntAssessment.SendEmailTo.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            if (pEntAssessment.SendEmailToAdminUser != null)
            {
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_SendEmailToAdminUser, pEntAssessment.SendEmailToAdminUser);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            _sqlObject.ExecuteNonQuery(_sqlcmd);

            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Assessment.PROC_UPDATE_ASSESSMENT_LANGUAGE;
            _sqlcmd.CommandType = CommandType.StoredProcedure;
            _sqlcmd.Connection = pSqlConn;
            _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, pStrUpdateMode);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAssessment.LanguageId);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pIsCopy)
            {

                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_APPROVAL_STATUS, AssessmentDates.AssessmentApprovalStatus.Draft.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_APPROVED_BY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_DATE_APPROVED, null);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            else
            {
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_APPROVAL_STATUS, pEntAssessment.ApprovalStatus.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_APPROVED_BY_ID, pEntAssessment.ApprovedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntAssessment.DateApproved) < 0)
                    _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_DATE_APPROVED, pEntAssessment.DateApproved);
                else
                    _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_DATE_APPROVED, null);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_TITLE, pEntAssessment.AssessmentTitle);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_DESC, pEntAssessment.AssessmentDescription);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_INST_TOP, pEntAssessment.AssessmentInstructionTop);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_INST_BOT, pEntAssessment.AssessmentInstructionBottom);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_CF_REVIEW_EMAIL, pEntAssessment.CFForReviewEmail);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntAssessment.DateLastReviewSent) < 0)
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_REVIEW_SENT_DATE, pEntAssessment.DateLastReviewSent);
            else
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_REVIEW_SENT_DATE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_REVIEW_EMAIL, pEntAssessment.ReviewEmail);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_BTN_NEXT_TXT, pEntAssessment.ButtonNextTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_BTN_PREVIOUS_TXT, pEntAssessment.ButtonPreviousTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_BTN_SUBMIT_TXT, pEntAssessment.ButtonSubmitTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_BUTTONPRINTTXT, pEntAssessment.ButtonPrintTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_BTN_EXIT_TXT, pEntAssessment.ButtonExitTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_BTN_SAVE_TXT, pEntAssessment.ButtonSaveTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_LOGO_PATH, pEntAssessment.LanguageLogoPath);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntAssessment.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlObject.ExecuteNonQuery(_sqlcmd);

            return pEntAssessment;
        }

        /// <summary>
        /// Save Assessment Language
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <param name="pSqlConn"></param>
        /// <returns></returns>
        public AssessmentDates SaveAssessmentLanguage(AssessmentDates pEntAssessment, SqlConnection pSqlConn)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Assessment.PROC_UPDATE_ASSESSMENT_LANGUAGE;
            _sqlcmd.CommandType = CommandType.StoredProcedure;
            _sqlcmd.Connection = pSqlConn;

            _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAssessment.LanguageId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_APPROVAL_STATUS, pEntAssessment.ApprovalStatus.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_APPROVED_BY_ID, pEntAssessment.ApprovedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntAssessment.DateApproved) < 0)
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_DATE_APPROVED, pEntAssessment.DateApproved);
            else
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_DATE_APPROVED, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_TITLE, pEntAssessment.AssessmentTitle);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_DESC, pEntAssessment.AssessmentDescription);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_INST_TOP, pEntAssessment.AssessmentInstructionTop);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_INST_BOT, pEntAssessment.AssessmentInstructionBottom);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_CF_REVIEW_EMAIL, pEntAssessment.CFForReviewEmail);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntAssessment.DateLastReviewSent) < 0)
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_REVIEW_SENT_DATE, pEntAssessment.DateLastReviewSent);
            else
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_REVIEW_SENT_DATE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_REVIEW_EMAIL, pEntAssessment.ReviewEmail);
            _sqlcmd.Parameters.Add(_sqlpara);


            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_BTN_NEXT_TXT, pEntAssessment.ButtonNextTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_BTN_PREVIOUS_TXT, pEntAssessment.ButtonPreviousTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_BTN_SUBMIT_TXT, pEntAssessment.ButtonSubmitTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_BTN_EXIT_TXT, pEntAssessment.ButtonExitTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_BTN_SAVE_TXT, pEntAssessment.ButtonSaveTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_BUTTONPRINTTXT, pEntAssessment.ButtonPrintTxt);
            _sqlcmd.Parameters.Add(_sqlpara);



            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_LOGO_PATH, pEntAssessment.LanguageLogoPath);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntAssessment.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);



            _sqlObject.ExecuteNonQuery(_sqlcmd);

            return pEntAssessment;
        }

        public AssessmentDates SaveAssessmentLanguage(AssessmentDates pEntAssessment)
        {
            SqlConnection pSqlConn = null;

            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);
            pSqlConn = new SqlConnection(_strConnString);
            pSqlConn.Open();

            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Assessment.PROC_UPDATE_ASSESSMENT_LANGUAGE;
            _sqlcmd.CommandType = CommandType.StoredProcedure;
            _sqlcmd.Connection = pSqlConn;

            _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAssessment.LanguageId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_APPROVAL_STATUS, pEntAssessment.ApprovalStatus.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_APPROVED_BY_ID, pEntAssessment.ApprovedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntAssessment.DateApproved) < 0)
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_DATE_APPROVED, pEntAssessment.DateApproved);
            else
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_DATE_APPROVED, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_TITLE, pEntAssessment.AssessmentTitle);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_DESC, pEntAssessment.AssessmentDescription);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_INST_TOP, pEntAssessment.AssessmentInstructionTop);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_INST_BOT, pEntAssessment.AssessmentInstructionBottom);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_CF_REVIEW_EMAIL, pEntAssessment.CFForReviewEmail);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntAssessment.DateLastReviewSent) < 0)
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_REVIEW_SENT_DATE, pEntAssessment.DateLastReviewSent);
            else
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_REVIEW_SENT_DATE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_REVIEW_EMAIL, pEntAssessment.ReviewEmail);
            _sqlcmd.Parameters.Add(_sqlpara);


            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_BTN_NEXT_TXT, pEntAssessment.ButtonNextTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_BTN_PREVIOUS_TXT, pEntAssessment.ButtonPreviousTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_BTN_SUBMIT_TXT, pEntAssessment.ButtonSubmitTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_BTN_EXIT_TXT, pEntAssessment.ButtonExitTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_BTN_SAVE_TXT, pEntAssessment.ButtonSaveTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_BUTTONPRINTTXT, pEntAssessment.ButtonPrintTxt);
            _sqlcmd.Parameters.Add(_sqlpara);



            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_LOGO_PATH, pEntAssessment.LanguageLogoPath);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntAssessment.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);



            _sqlObject.ExecuteNonQuery(_sqlcmd);

            return pEntAssessment;
        }
        /// <summary>
        /// To Edit the Assessment data 
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns>Assessment Object</returns>
        public AssessmentDates EditAssessment(AssessmentDates pEntAssessment)
        {
            AssessmentDates entAssessment = new AssessmentDates();
            try
            {
                //Update information in DataBase
                entAssessment = Update(pEntAssessment, Schema.Common.VAL_UPDATE_MODE);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entAssessment;
        }

        /// <summary>
        /// private method to support both Add and Edit Assessment transactions.
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <param name="pUpdateMode"></param>
        /// <returns>Assessment Object</returns>
        private AssessmentDates Update(AssessmentDates pEntAssessment, string pStrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Assessment.PROC_UPDATE_ASSESSMENT;
            _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);

            if (string.IsNullOrEmpty(pEntAssessment.ID))
            {
                pEntAssessment.ID = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_ASSESSMENT_ID_PREFIX, Schema.Common.VAL_ASSESSMENT_ID_LENGTH);
            }
            if (pStrUpdateMode == Schema.Common.VAL_UPDATE_MODE)
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_TYPE, pEntAssessment.AssessmentType.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_IS_ACTIVE, pEntAssessment.IsActive);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_IS_PRINT_CERTIFICATE, pEntAssessment.IsPrintCertificate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ALLOW_SEQUENCING, pEntAssessment.AllowSequencing);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_BGCOLOR, pEntAssessment.BGColor);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_DEFAULTLOGOPATH, pEntAssessment.DefaultLogoPath);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_LOGO_ONALL, pEntAssessment.IsLogoOnAll);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_PARTIAL_SUBMITALLOWED, pEntAssessment.IsPartialSubmitAllowed);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_MULTI_SUBMITALLOWED, pEntAssessment.IsMultiSubmitAllowed);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_REVIEW_ANSWER, pEntAssessment.IsReviewAnswer);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ALLOW_USER_LANGSELECTION, pEntAssessment.AllowUserLangSelection);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntAssessment.ClientId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntAssessment.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_TRACKING_TYPE, pEntAssessment.TrackingType.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_LOGO_POSITION, pEntAssessment.LogoPosition.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_MAX_RESPONSE_LENGTH, pEntAssessment.MaxResponseLength);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_FONT_NAME, pEntAssessment.FontName);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_FONT_SIZE, pEntAssessment.FontSize);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_FONT_COLOR, pEntAssessment.FontColor);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_QUEBG_COLOR, pEntAssessment.QuestionaireBGColor);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntAssessment.ShowAssessmentResult != AssessmentDates.ShowAssessmentResultType.Null)
            {
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_SHOW_ASSESSMENT_RESULT, pEntAssessment.ShowAssessmentResult.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            _sqlpara = new SqlParameter(Schema.Assessment.PARA_QUESTIONCOUNT, pEntAssessment.QuestionCount);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!string.IsNullOrEmpty(pEntAssessment.SendEmailTo))
            {
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_SendEmailTo, pEntAssessment.SendEmailTo.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            if (pEntAssessment.SendEmailToAdminUser != null)
            {
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_SendEmailToAdminUser, pEntAssessment.SendEmailToAdminUser);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            int iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Assessment.PROC_UPDATE_ASSESSMENT_LANGUAGE;
            _sqlcmd.CommandType = CommandType.StoredProcedure;

            if (pStrUpdateMode == Schema.Common.VAL_UPDATE_MODE)
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntAssessment.LanguageId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_TITLE, pEntAssessment.AssessmentTitle);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_DESC, pEntAssessment.AssessmentDescription);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_INST_TOP, pEntAssessment.AssessmentInstructionTop);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_INST_BOT, pEntAssessment.AssessmentInstructionBottom);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_CF_REVIEW_EMAIL, pEntAssessment.CFForReviewEmail);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntAssessment.DateLastReviewSent) < 0)
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_REVIEW_SENT_DATE, pEntAssessment.DateLastReviewSent);
            else
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_REVIEW_SENT_DATE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_REVIEW_EMAIL, pEntAssessment.ReviewEmail);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_APPROVAL_STATUS, pEntAssessment.ApprovalStatus.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_APPROVED_BY_ID, pEntAssessment.ApprovedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntAssessment.DateApproved) < 0)
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_DATE_APPROVED, pEntAssessment.DateApproved);
            else
                _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_DATE_APPROVED, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_BTN_NEXT_TXT, pEntAssessment.ButtonNextTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_BTN_PREVIOUS_TXT, pEntAssessment.ButtonPreviousTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_BTN_SUBMIT_TXT, pEntAssessment.ButtonSubmitTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_BTN_EXIT_TXT, pEntAssessment.ButtonExitTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_BTN_SAVE_TXT, pEntAssessment.ButtonSaveTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_LANG_LOGO_PATH, pEntAssessment.LanguageLogoPath);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntAssessment.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            return pEntAssessment;
        }

        /// <summary>
        /// Delete Assessment List From Server and DataBase
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns>Assessment Object</returns>
        public List<AssessmentDates> DeleteAssessmentList(List<AssessmentDates> pEntListAssessment)
        {
            List<AssessmentDates> entListQuestionnarie = new List<AssessmentDates>();
            AssessmentDates entQuestionarie;
            int _iTotalDeleted = 0;
            try
            {
                if (pEntListAssessment.Count > 0)
                {
                    foreach (AssessmentDates objBase in pEntListAssessment)
                    {
                        try
                        {
                            entQuestionarie = DeleteAssessment(objBase);
                            _iTotalDeleted = _iTotalDeleted + 1;
                        }
                        catch { }
                    }
                    EntityRange _entRange = new EntityRange();
                    _entRange.TotalRows = _iTotalDeleted;
                    entQuestionarie = new AssessmentDates();
                    entQuestionarie.ListRange = _entRange;
                    entListQuestionnarie.Add(entQuestionarie);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListQuestionnarie;
        }

        /// <summary>
        /// Approve Assessment List From Server and DataBase
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns>Assessment Object</returns>
        public List<AssessmentDates> ApproveAssessmentList(List<AssessmentDates> pEntListAssessment)
        {
            List<AssessmentDates> entListQuestionnarie = new List<AssessmentDates>();
            List<AssessmentDates> pentListQuestionnarie = new List<AssessmentDates>();
            AssessmentDates entQuestionarie;
            try
            {
                _sqlObject = new SQLObject();
                _sqladapter = new SqlDataAdapter();
                _dtable = new DataTable();
                DataTable dQuestionTable = new DataTable();
                int iBatchSize = 0;

                if (pEntListAssessment.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.Assessment.COL_ASSESSMENT_ID);
                    _dtable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    _dtable.Columns.Add(Schema.Assessment.COL_ASSESSMENT_LANG_APPROVAL_STATUS);
                    _dtable.Columns.Add(Schema.Assessment.COL_ASSESSMENT_LANG_APPROVED_BY_ID);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);

                    foreach (AssessmentDates objBase in pEntListAssessment)
                    {
                        entQuestionarie = new AssessmentDates();
                        entQuestionarie = objBase;

                        DataRow drow = _dtable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entQuestionarie.ClientId);

                        if (!String.IsNullOrEmpty(entQuestionarie.ID))
                        {
                            drow[Schema.Assessment.COL_ASSESSMENT_ID] = entQuestionarie.ID;
                            if (!String.IsNullOrEmpty(entQuestionarie.LanguageId))
                            {
                                drow[Schema.Language.COL_LANGUAGE_ID] = entQuestionarie.LanguageId;
                            }
                            drow[Schema.Assessment.COL_ASSESSMENT_LANG_APPROVAL_STATUS] = entQuestionarie.ApprovalStatus.ToString();
                            if (!String.IsNullOrEmpty(entQuestionarie.ApprovedById))
                            {
                                drow[Schema.Assessment.COL_ASSESSMENT_LANG_APPROVED_BY_ID] = entQuestionarie.ApprovedById;
                            }

                            if (!String.IsNullOrEmpty(entQuestionarie.LastModifiedById))
                            {
                                drow[Schema.Common.COL_MODIFIED_BY] = entQuestionarie.LastModifiedById;
                            }
                            iBatchSize = iBatchSize + 1;
                            _dtable.Rows.Add(drow);
                            entListQuestionnarie.Add(entQuestionarie);
                        }
                    }
                    if (_dtable.Rows.Count > 0)
                    {
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.Assessment.PROC_APPROVE_ASSESSMENT;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Assessment.PARA_ASSESSMENT_ID, SqlDbType.VarChar, 100, Schema.Assessment.COL_ASSESSMENT_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Assessment.PARA_ASSESSMENT_LANG_APPROVAL_STATUS, SqlDbType.VarChar, 100, Schema.Assessment.COL_ASSESSMENT_LANG_APPROVAL_STATUS);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Assessment.PARA_ASSESSMENT_LANG_APPROVED_BY_ID, SqlDbType.VarChar, 100, Schema.Assessment.COL_ASSESSMENT_LANG_APPROVED_BY_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        int _iDeleteRows = _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();

                        pentListQuestionnarie = new List<AssessmentDates>();
                        EntityRange _entRange = new EntityRange();
                        _entRange.TotalRows = _iDeleteRows;
                        entQuestionarie = new AssessmentDates();
                        entQuestionarie.ListRange = _entRange;
                        pentListQuestionnarie.Add(entQuestionarie);
                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pentListQuestionnarie;
        }

        /// <summary>
        /// Approve Assessment List From Server and DataBase
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns>Assessment Object</returns>
        public List<AssessmentDates> ActivateDeActivateStatusList(List<AssessmentDates> pEntListAssessment)
        {
            List<AssessmentDates> entListQuestionnarie = new List<AssessmentDates>();
            List<AssessmentDates> pentListQuestionnarie = new List<AssessmentDates>();
            AssessmentDates entQuestionarie;
            try
            {
                _sqlObject = new SQLObject();
                _sqladapter = new SqlDataAdapter();
                _dtable = new DataTable();
                DataTable dQuestionTable = new DataTable();
                int iBatchSize = 0;
                if (pEntListAssessment.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.Assessment.COL_ASSESSMENT_ID);
                    _dtable.Columns.Add(Schema.Assessment.COL_IS_ACTIVE);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    foreach (AssessmentDates objBase in pEntListAssessment)
                    {
                        entQuestionarie = new AssessmentDates();
                        entQuestionarie = objBase;

                        DataRow drow = _dtable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entQuestionarie.ClientId);

                        if (!String.IsNullOrEmpty(entQuestionarie.ID))
                        {
                            drow[Schema.Assessment.COL_ASSESSMENT_ID] = entQuestionarie.ID;
                            drow[Schema.Assessment.COL_IS_ACTIVE] = entQuestionarie.IsActive;


                            if (!String.IsNullOrEmpty(entQuestionarie.LastModifiedById))
                            {
                                drow[Schema.Common.COL_MODIFIED_BY] = entQuestionarie.LastModifiedById;
                            }
                            iBatchSize = iBatchSize + 1;
                            _dtable.Rows.Add(drow);
                            entListQuestionnarie.Add(entQuestionarie);
                        }
                    }
                    if (_dtable.Rows.Count > 0)
                    {
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.Assessment.PROC_ACTIVE_ASSESSMENT;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Assessment.PARA_ASSESSMENT_ID, SqlDbType.VarChar, 100, Schema.Assessment.COL_ASSESSMENT_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Assessment.PARA_IS_ACTIVE, SqlDbType.Bit, 100, Schema.Assessment.COL_IS_ACTIVE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        int _iDeleteRows = _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();

                        pentListQuestionnarie = new List<AssessmentDates>();
                        EntityRange _entRange = new EntityRange();
                        _entRange.TotalRows = _iDeleteRows;
                        entQuestionarie = new AssessmentDates();
                        entQuestionarie.ListRange = _entRange;
                        pentListQuestionnarie.Add(entQuestionarie);
                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pentListQuestionnarie;
        }

        /////// <summary>
        /////// Update Default Sequence
        /////// </summary>
        /////// <param name="pEntAssessment"></param>
        /////// <param name="pStrUpdateMode"></param>
        /////// <returns></returns>
        ////public Assessment UpdateDefaultSequence(Assessment pEntAssessment)
        ////{
        ////    _sqlObject = new SQLObject();
        ////    _sqlcmd = new SqlCommand();
        ////    _sqlcmd.CommandText = Schema.Assessment.PROC_UPDATE_ASSESSMENT_DEFAULT_SEQ;
        ////    _strConnString = _sqlObject.GetClientDBConnString(pEntAssessment.ClientId);


        ////    _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessment.ID);
        ////    _sqlcmd.Parameters.Add(_sqlpara);

        ////    _sqlpara = new SqlParameter(Schema.Assessment.PARA_IS_DEFAULT_SEQ, pEntAssessment.IsDefaultSequence);
        ////    _sqlcmd.Parameters.Add(_sqlpara);

        ////    int iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

        ////    return pEntAssessment;
        ////}
        /// <summary>
        /// Add Question With Copy
        /// </summary>
        /// <param name="pEntListQuestions"></param>
        /// <param name="pSqlConn"></param>
        /// <returns></returns>
        public List<AssessmentQuestion> AddQuestionMappingWithCopy(List<AssessmentQuestion> pEntListQuestions, SqlConnection pSqlConn)
        {
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();

            List<AssessmentQuestion> entListQuestions = new List<AssessmentQuestion>();
            int iBatchSize = 0;
            try
            {
                if (pEntListQuestions.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.AssessmentQuestion.COL_QUESTION_ID);
                    _dtable.Columns.Add(Schema.AssessmentQuestion.COL_ASSESSMENT_ID);
                    _dtable.Columns.Add(Schema.AssessmentQuestion.COL_SECTION_ID);
                    _dtable.Columns.Add(Schema.AssessmentQuestion.COL_QUESTION_TYPE);
                    _dtable.Columns.Add(Schema.AssessmentQuestion.COL_SEQUENCE_ORDER);
                    _dtable.Columns.Add(Schema.AssessmentQuestion.COL_IS_ACTIVE);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (AssessmentQuestion entQuestion in pEntListQuestions)
                    {
                        /*code change for duplicate question issue while copying records. By:rajendra on 16-apr-2010. */
                        _dtable.Rows.Clear();
                        DataRow drow = _dtable.NewRow();
                        drow[Schema.AssessmentQuestion.COL_QUESTION_ID] = entQuestion.ID;

                        drow[Schema.AssessmentQuestion.COL_ASSESSMENT_ID] = entQuestion.AssessmentID;
                        drow[Schema.AssessmentQuestion.COL_SECTION_ID] = entQuestion.SectionID;
                        drow[Schema.AssessmentQuestion.COL_SEQUENCE_ORDER] = entQuestion.SequenceOrder;
                        drow[Schema.AssessmentQuestion.COL_IS_ACTIVE] = entQuestion.IsActive;
                        drow[Schema.Common.COL_MODIFIED_BY] = entQuestion.LastModifiedById;
                        drow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_INSERT_MODE;


                        _dtable.Rows.Add(drow);

                        iBatchSize = 1;

                        if (_dtable.Rows.Count > 0)
                        {
                            _sqlcmd = new SqlCommand();
                            _sqlcmd.CommandText = Schema.AssessmentQuestionMapping.PROC_UPDATE_ASSESSMENTQUESTIONMAPPING; ////Schema.AssessmentQuestion.PROC_UPDATE_ASSESSMENT_QUESTION;
                            _sqlcmd.CommandType = CommandType.StoredProcedure;
                            _sqlcmd.Connection = pSqlConn;
                            _sqladapter.InsertCommand = _sqlcmd;
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_QUESTION_ID, SqlDbType.VarChar, 100, Schema.AssessmentQuestion.COL_QUESTION_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_ASSESSMENT_ID, SqlDbType.VarChar, 100, Schema.AssessmentQuestion.COL_ASSESSMENT_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.AssessmentQuestion.COL_SECTION_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_SEQUENCE_ORDER, SqlDbType.Int, 10, Schema.AssessmentQuestion.COL_SEQUENCE_ORDER);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_IS_ACTIVE, SqlDbType.Bit, 1, Schema.AssessmentQuestion.COL_IS_ACTIVE);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                            _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                            _sqladapter.UpdateBatchSize = iBatchSize;
                            _sqladapter.Update(_dtable);
                            _sqladapter.Dispose();
                        }

                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListQuestions;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns></returns>
        public AssessmentDates Get(AssessmentDates pEntAssessment)
        {
            return GetAssessmentById(pEntAssessment);
        }
        /// <summary>
        /// Update Assessment
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <returns></returns>
        public AssessmentDates Update(AssessmentDates pEntAssessment)
        {
            return EditAssessment(pEntAssessment);
        }
        #endregion
    }
}
