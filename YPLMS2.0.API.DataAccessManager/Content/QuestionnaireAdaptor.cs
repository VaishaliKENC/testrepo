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
    /// class Questionnaire Adaptor
    /// </summary>
    public class QuestionnaireAdaptor : IDataManager<Questionnaire>
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
        string _strMessageId = YPLMS.Services.Messages.Questionnaire.QUESTIONNAIRE_ERROR;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// To Get Questionnaire details by Questionnaire Id.
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns>Questionnaire Object</returns>
        public Questionnaire GetQuestionnaireById(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            Questionnaire entQuestionnaire = new Questionnaire();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Questionnaire.PROC_SELECT_QUESTIONNAIRE;
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntQuestionnaire.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);


                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entQuestionnaire = FillObject(_sqlreader, false, _sqlObject);
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
            return entQuestionnaire;
        }

        /// <summary>
        /// To Get Questionnaire Default Sequencing by Questionnaire Id.
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns>Questionnaire Object</returns>
        public Questionnaire GetQuestionnaireDefaultSequence(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            Questionnaire entQuestionnaire = new Questionnaire();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Questionnaire.PROC_SELECT_QUESTIONNAIRE_DEFAULT_SEQ;
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entQuestionnaire = FillObjectDefaultSequencing(_sqlreader, false, _sqlObject);
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
            return entQuestionnaire;
        }
        /// <summary>
        /// Fill Object Default Sequencing
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <param name="pRangeList"></param>
        /// <param name="pSqlObject"></param>
        /// <returns></returns>
        private Questionnaire FillObjectDefaultSequencing(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            Questionnaire entQuestionnaire = new Questionnaire();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTIONNAIRE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_IS_DEFAULT_SEQ);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.IsDefaultSequence = pSqlReader.GetBoolean(iIndex);


            }
            return entQuestionnaire;
        }


        /// <summary>
        /// To Get Questionnaire Default Sequencing by Questionnaire Id.
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns>Questionnaire Object</returns>
        public Questionnaire GetQuestionnaireShowQuestionNumber(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            Questionnaire entQuestionnaire = new Questionnaire();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Questionnaire.PROC_SELECT_QUESTIONNAIRE_QUEST_NUMBER;
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entQuestionnaire = FillObjectShowQuestionNumber(_sqlreader, false, _sqlObject);
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
            return entQuestionnaire;
        }
        /// <summary>
        /// Fill Object Default Sequencing
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <param name="pRangeList"></param>
        /// <param name="pSqlObject"></param>
        /// <returns></returns>
        private Questionnaire FillObjectShowQuestionNumber(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            Questionnaire entQuestionnaire = new Questionnaire();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTIONNAIRE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_IS_SHOW_QUESTIONNUMBER);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.IsShowQuestionNumber = pSqlReader.GetBoolean(iIndex);


            }
            return entQuestionnaire;
        }


        /// <summary>
        /// To Get Questionnaire details by Questionnaire Id.
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns>Questionnaire Object</returns>
        public Questionnaire GetQuestionnaireTypeById_Learner(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            Questionnaire entQuestionnaire = new Questionnaire();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Questionnaire.PROC_SELECT_QUESTIONNAIRE_TYPE_LEARNER;
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntQuestionnaire.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                //entQuestionnaire = FillObject(_sqlreader, false, _sqlObject);
                entQuestionnaire = FillObjectQuestionnaireTypeLearner(_sqlreader, false, _sqlObject);
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
            return entQuestionnaire;
        }

        /// <summary>
        /// To check duplicate questionnaire title.
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns></returns>
        public Questionnaire CheckQuestionnaireByName(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            Questionnaire entQuestionnaire = new Questionnaire();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Questionnaire.PROC_CHECK_QUESTIONNAIRE_BY_NAME;
                _sqlcmd.Connection = sqlConnection;

                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_TITLE, pEntQuestionnaire.QuestionnaireTitle);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntQuestionnaire.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entQuestionnaire = FillQuestionnaireTitle(_sqlreader);
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
            return entQuestionnaire;
        }

        /// <summary>
        /// Fill Questionnaire Title
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns></returns>
        private Questionnaire FillQuestionnaireTitle(SqlDataReader pSqlReader)
        {
            Questionnaire entQuestionnaire = new Questionnaire();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTIONNAIRE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTN_LANG_TITLE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.QuestionnaireTitle = pSqlReader.GetString(iIndex);
            }
            return entQuestionnaire;
        }

        /// <summary>
        ///  To Fill Questionnaire object properties values from reader data 
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>Questionnaire Object</returns>
        private Questionnaire FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            Questionnaire entQuestionnaire = new Questionnaire();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTIONNAIRE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTIONNAIRE_TYPE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.QuestionnaireType = (Questionnaire.QuestionnaireDisplayType)Enum.Parse(typeof(Questionnaire.QuestionnaireDisplayType), pSqlReader.GetString(iIndex));


                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_LOGO_POSITION);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.LogoPosition = (Questionnaire.QuestionnaireLogoPosition)Enum.Parse(typeof(Questionnaire.QuestionnaireLogoPosition), pSqlReader.GetString(iIndex));

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_IS_ACTIVE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.IsActive = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_IS_PRINT_CERTIFICATE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.IsPrintCertificate = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_ALLOW_SEQUENCING);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.AllowSequencing = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_BGCOLOR);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.BGColor = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_DEFAULTLOGOPATH);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.DefaultLogoPath = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_IS_LOGO_ONALL);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.IsLogoOnAll = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_IS_PARTIAL_SUBMITALLOWED);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.IsPartialSubmitAllowed = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_IS_MULTI_SUBMITALLOWED);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.IsMultiSubmitAllowed = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_IS_REVIEW_ANSWER);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.IsReviewAnswer = pSqlReader.GetBoolean(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Questionnaire.COL_IS_LOCKED))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_IS_LOCKED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.IsLocked = pSqlReader.GetBoolean(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Questionnaire.COL_IS_LEARNERPRINTABLE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_IS_LEARNERPRINTABLE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.IsLearnerPrintable = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Questionnaire.COL_IS_SHOW_QUESTIONNUMBER))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_IS_SHOW_QUESTIONNUMBER);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.IsShowQuestionNumber = pSqlReader.GetBoolean(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Questionnaire.COL_QUESTION_COUNT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTION_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.ParaQuestionCount = pSqlReader.GetInt32(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Questionnaire.COL_TRACKING_TYPE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_TRACKING_TYPE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.TrackingType = (Questionnaire.QuestionnaireSubmitType)Enum.Parse(typeof(Questionnaire.QuestionnaireSubmitType), Convert.ToString(pSqlReader.GetString(iIndex)));
                }


                //new properies added: on 13-apr-2010
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Questionnaire.COL_IS_USESECTIONS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_IS_USESECTIONS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.IsUseSections = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Questionnaire.COL_IS_LOGOHIDE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_IS_LOGOHIDE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.IsLogoHide = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Questionnaire.COL_IS_LOGOONFIRSTPAGEONLY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_IS_LOGOONFIRSTPAGEONLY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.IsLogoOnFirstPageOnly = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Questionnaire.COL_BGHEADER_HF))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_BGHEADER_HF);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.BGColorHF = pSqlReader.GetString(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Questionnaire.COL_IS_ALL_ANSWER_MUST))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_IS_ALL_ANSWER_MUST);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.IsAllAnswerMust = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Questionnaire.COL_FONT_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_FONT_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.FontName = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Questionnaire.COL_FONT_SIZE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_FONT_SIZE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.FontSize = pSqlReader.GetInt32(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Questionnaire.COL_FONT_COLOR))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_FONT_COLOR);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.FontColor = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Questionnaire.COL_QUEBG_COLOR))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUEBG_COLOR);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.QuestionaireBGColor = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Questionnaire.COL_MAX_RESPONSE_LENGTH))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_MAX_RESPONSE_LENGTH);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.MaxResponseLength = pSqlReader.GetInt32(iIndex);
                }

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_ALLOW_USER_LANGSELECTION);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.AllowUserLangSelection = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.ClientId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.CreatedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.DateCreated = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.LastModifiedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.LastModifiedDate = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_LANGUAGE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.LanguageId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTN_LANG_TITLE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.QuestionnaireTitle = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTN_LANG_DESC);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.QuestionnaireDescription = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTN_LANG_INST_TOP);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.QuestionnaireInstructionTop = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTN_LANG_INST_BOT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.QuestionnaireInstructionBottom = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTN_LANG_CF_REVIEW_EMAIL);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.CFForReviewEmail = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTN_LANG_REVIEW_SENT_DATE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.DateLastReviewSent = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTN_LANG_REVIEW_EMAIL);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.ReviewEmail = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTN_LANG_APPROVAL_STATUS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.ApprovalStatus = (Questionnaire.QuestionnaireApprovalStatus)Enum.Parse(typeof(Questionnaire.QuestionnaireApprovalStatus), pSqlReader.GetString(iIndex));

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTN_LANG_APPROVED_BY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.ApprovedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTN_LANG_DATE_APPROVED);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.DateApproved = pSqlReader.GetDateTime(iIndex);


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Questionnaire.COL_BUTTONPRINTTXT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_BUTTONPRINTTXT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.ButtonPrintTxt = pSqlReader.GetString(iIndex);
                }


                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTN_LANG_BTN_NEXT_TXT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.ButtonNextTxt = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTN_LANG_BTN_PREVIOUS_TXT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.ButtonPreviousTxt = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTN_LANG_BTN_SUBMIT_TXT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.ButtonSubmitTxt = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTN_LANG_BTN_EXIT_TXT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.ButtonExitTxt = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_BTN_SAVE_TXT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.ButtonSaveTxt = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTN_LANG_LOGO_PATH);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.LanguageLogoPath = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.CreatedById = pSqlReader.GetString(iIndex);


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Questionnaire.COL_IS_QUESTIONNAIRE_SURVEY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_IS_QUESTIONNAIRE_SURVEY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.IsQuestionnaireForSurvey = pSqlReader.GetBoolean(iIndex);
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
                            entQuestionnaire.ListRange = _entRange;
                        }
                    }
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.CreatedByName = pSqlReader.GetString(iIndex);

                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.ModifiedByName = pSqlReader.GetString(iIndex);

                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.Category.COL_CATEGORYNAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Category.COL_CATEGORYNAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.CategoryName = pSqlReader.GetString(iIndex);

                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.SubCategory.COL_SUBCATEGORYNAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.SubCategory.COL_SUBCATEGORYNAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestionnaire.SubCategoryName = pSqlReader.GetString(iIndex);

                }

            }
            return entQuestionnaire;
        }

        /// <summary>
        ///  To Fill Questionnaire object properties values from reader data 
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>Questionnaire Object</returns>
        private Questionnaire FillObjectQuestionnaireTypeLearner(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            Questionnaire entQuestionnaire = new Questionnaire();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTIONNAIRE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Questionnaire.COL_QUESTIONNAIRE_TYPE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entQuestionnaire.QuestionnaireType = (Questionnaire.QuestionnaireDisplayType)Enum.Parse(typeof(Questionnaire.QuestionnaireDisplayType), pSqlReader.GetString(iIndex));
            }
            return entQuestionnaire;
        }

        /// <summary>
        /// Find Questionnaire infomration
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<Questionnaire> FindQuestionnaires(Search pEntSearch)
        {
            _sqlObject = new SQLObject();
            List<Questionnaire> entListQuestionnaire = new List<Questionnaire>();
            Questionnaire entQuestionnaire = new Questionnaire();
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Questionnaire.PROC_FIND_QUESTIONNAIRE;
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

                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntSearch.KeyWord))
                {
                    _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_TITLE, pEntSearch.KeyWord);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntSearch.SearchObject != null && pEntSearch.SearchObject.Count > 0)
                {
                    entQuestionnaire = new Questionnaire();
                    entQuestionnaire = (Questionnaire)pEntSearch.SearchObject[0];
                    if (DateTime.MinValue.CompareTo(entQuestionnaire.DateCreated) < 0)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_DATE_FROM, entQuestionnaire.DateCreated);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (!string.IsNullOrEmpty(entQuestionnaire.CreatedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, entQuestionnaire.CreatedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (!string.IsNullOrEmpty(entQuestionnaire.LanguageId))
                    {
                        _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, entQuestionnaire.LanguageId);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (!string.IsNullOrEmpty(entQuestionnaire.CategoryId))
                    {
                        _sqlpara = new SqlParameter(Schema.Category.PARA_CATEGORYID, entQuestionnaire.CategoryId);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (!string.IsNullOrEmpty(entQuestionnaire.SubCategoryId))
                    {
                        _sqlpara = new SqlParameter(Schema.SubCategory.PARA_SUBCATEGORYID, entQuestionnaire.SubCategoryId);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (pEntSearch.SearchObject.Count > 1)
                    {
                        entQuestionnaire = new Questionnaire();
                        entQuestionnaire = (Questionnaire)pEntSearch.SearchObject[1];
                        if (DateTime.MinValue.CompareTo(entQuestionnaire.DateCreated) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.Common.PARA_DATE_TO, entQuestionnaire.DateCreated);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entQuestionnaire = FillObject(_sqlreader, true, _sqlObject);
                    entListQuestionnaire.Add(entQuestionnaire);
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
            return entListQuestionnaire;
        }

        /// <summary>
        /// Get All Questionnaire
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns>List of Questionnaire Object</returns>
        public List<Questionnaire> GetQuestionnaireList(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            List<Questionnaire> entListQuestionnaire = new List<Questionnaire>();
            Questionnaire entQuestionnaire = null;
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Questionnaire.PROC_GET_ALL_QUESTIONNAIRE;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                if (pEntQuestionnaire.ApprovalStatus == Questionnaire.QuestionnaireApprovalStatus.Approved)
                {
                    _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_APPROVAL_STATUS, pEntQuestionnaire.ApprovalStatus.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntQuestionnaire.LanguageId))
                {
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntQuestionnaire.LanguageId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntQuestionnaire.IsActive != null)
                {
                    _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_ACTIVE, pEntQuestionnaire.IsActive);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntQuestionnaire.LastModifiedById))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntQuestionnaire.LastModifiedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntQuestionnaire.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntQuestionnaire.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntQuestionnaire.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntQuestionnaire.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntQuestionnaire.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntQuestionnaire.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entQuestionnaire = FillObject(_sqlreader, true, _sqlObject);
                    entListQuestionnaire.Add(entQuestionnaire);
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
            return entListQuestionnaire;
        }

        /// <summary>
        /// Get non preferred Questionnaire for assignment
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns>List of Questionnaire Object</returns>
        public List<Questionnaire> GetQuestionnaireForAssignment(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            List<Questionnaire> entListQuestionnaire = new List<Questionnaire>();
            Questionnaire entQuestionnaire = null;
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Questionnaire.PROC_GET_QUESTIONNAIRE_FOR_ASSIGNMENT;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                if (pEntQuestionnaire.ApprovalStatus == Questionnaire.QuestionnaireApprovalStatus.Approved)
                {
                    _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_APPROVAL_STATUS, pEntQuestionnaire.ApprovalStatus.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntQuestionnaire.LanguageId))
                {
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntQuestionnaire.LanguageId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntQuestionnaire.IsActive != null)
                {
                    _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_ACTIVE, pEntQuestionnaire.IsActive);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntQuestionnaire.CreatedById != null)
                {
                    _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_CREATED_BY_ID, pEntQuestionnaire.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntQuestionnaire.CategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.Category.PARA_CATEGORYID, pEntQuestionnaire.CategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntQuestionnaire.SubCategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.SubCategory.PARA_SUBCATEGORYID, pEntQuestionnaire.SubCategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntQuestionnaire.QuestionnaireTitle))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntQuestionnaire.QuestionnaireTitle);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntQuestionnaire.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntQuestionnaire.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntQuestionnaire.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntQuestionnaire.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entQuestionnaire = FillObject(_sqlreader, true, _sqlObject);
                    entListQuestionnaire.Add(entQuestionnaire);
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
            return entListQuestionnaire;
        }

        /// <summary>
        /// To Get a Questionnaire languages list
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns></returns>
        public List<Questionnaire> GetQuestionnaireLanguageList(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            List<Questionnaire> entListQuestionnaire = new List<Questionnaire>();
            Questionnaire entQuestionnaire = null;
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Questionnaire.PROC_GET_QUESTIONNAIRE_LANGUAGES;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                if (pEntQuestionnaire.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntQuestionnaire.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntQuestionnaire.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntQuestionnaire.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entQuestionnaire = FillObject(_sqlreader, true, _sqlObject);
                    entListQuestionnaire.Add(entQuestionnaire);
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
            return entListQuestionnaire;
        }

        /// <summary>
        /// Get Questionnaire details with respective sections,questions,options. 
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns></returns>
        public List<Questionnaire> GetQuestionnaireDtls(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            List<Questionnaire> entListQuestionnaire = new List<Questionnaire>();
            Questionnaire entQuestionnaire = null;
            QuestionnaireSections entSection = null;
            QuestionnaireQuestion entQuestion = null;
            QuestionOptions entOption = null;
            _sqlcmd = new SqlCommand();
            DataSet dset = new DataSet();
            DataTable dtableQuestionnaire;
            DataTable dtableSections;
            DataTable dtableQuestions;
            DataTable dtableOptions;
            _sqlcmd.CommandText = Schema.Questionnaire.PROC_GET_QUESTIONNAIRE_DTLS;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
                if (!string.IsNullOrEmpty(pEntQuestionnaire.ID))
                {
                    _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntQuestionnaire.ParaSectionId))
                {
                    _sqlpara = new SqlParameter(Schema.QuestionnaireSections.PARA_SECTION_ID, pEntQuestionnaire.ParaSectionId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntQuestionnaire.ParaQuestionId))
                {
                    _sqlpara = new SqlParameter(Schema.QuestionnaireQuestion.PARA_QUESTION_ID, pEntQuestionnaire.ParaQuestionId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntQuestionnaire.ParaOptionId))
                {
                    _sqlpara = new SqlParameter(Schema.QuestionnaireOptions.PARA_OPTION_ID, pEntQuestionnaire.ParaOptionId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntQuestionnaire.LanguageId))
                {
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntQuestionnaire.LanguageId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                dset = _sqlObject.SqlDataAdapter(_sqlcmd, _strConnString);
                dtableQuestionnaire = dset.Tables[0];
                dtableSections = dset.Tables[1];
                dtableQuestions = dset.Tables[2];
                dtableOptions = dset.Tables[3];
                foreach (DataRow drow in dtableQuestionnaire.Rows)
                {
                    entQuestionnaire = new Questionnaire();
                    entQuestionnaire = FillQuestionnaire(drow);
                    foreach (DataRow drowSection in dtableSections.Select(Schema.Questionnaire.COL_QUESTIONNAIRE_ID + "='" + entQuestionnaire.ID + "'"))
                    {
                        entSection = new QuestionnaireSections();
                        entSection = FillSection(drowSection);
                        foreach (DataRow drowQuestion in dtableQuestions.Select(Schema.QuestionnaireQuestion.COL_SECTION_ID + "='" + entSection.ID + "'"))
                        {
                            entQuestion = new QuestionnaireQuestion();
                            entQuestion = FillQuestion(drowQuestion);
                            foreach (DataRow drowOption in dtableOptions.Select(Schema.QuestionnaireQuestion.COL_QUESTION_ID + "='" + entQuestion.ID + "'"))
                            {
                                entOption = new QuestionOptions();
                                entOption = FillOption(drowOption);
                                entQuestion.QuestionOptions.Add(entOption);
                            }
                            entSection.QuestionnaireQuestion.Add(entQuestion);
                        }
                        entQuestionnaire.Sections.Add(entSection);
                    }
                    entListQuestionnaire.Add(entQuestionnaire);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListQuestionnaire;
        }

        /// <summary>
        /// Get Questionnaire data with tracking infomration.
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns></returns>
        public List<Questionnaire> GetQuestionnaireTracking(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            List<Questionnaire> entListQuestionnaire = new List<Questionnaire>();
            Questionnaire entQuestionnaire = null;
            QuestionnaireSections entSection = null;
            QuestionnaireQuestion entQuestion = null;
            QuestionOptions entOption = null;

            UserQuestionnaireTracking entAttempt = null;
            UserQuestionnaireSessionResponses entResponse = null;

            _sqlcmd = new SqlCommand();
            DataSet dset = new DataSet();
            DataTable dtableQuestionnaire;
            DataTable dtableSections;
            DataTable dtableQuestions;
            DataTable dtableOptions;
            DataTable dtableAttempts = null;
            DataTable dtableResponse = null;

            _sqlcmd.CommandText = Schema.Questionnaire.PROC_GET_QUESTIONNAIRE_TRACKING;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);

                if (!string.IsNullOrEmpty(pEntQuestionnaire.ID))
                {
                    _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntQuestionnaire.UserAttemptTracking != null && pEntQuestionnaire.UserAttemptTracking.Count > 0)
                {
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntQuestionnaire.UserAttemptTracking[0].SystemUserGUID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntQuestionnaire.UserAttemptTracking != null && pEntQuestionnaire.UserAttemptTracking.Count > 0)
                {

                    if (pEntQuestionnaire.UserAttemptTracking[0].IsForAdminPreview)
                    {
                        _sqlpara = new SqlParameter(Schema.UserQuestionnaireTracking.PARA_IS_FOR_ADMIN_PREVIEW, pEntQuestionnaire.UserAttemptTracking[0].IsForAdminPreview);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                if (!string.IsNullOrEmpty(pEntQuestionnaire.ParaQuestionId))
                {
                    _sqlpara = new SqlParameter(Schema.QuestionnaireQuestion.PARA_QUESTION_ID, pEntQuestionnaire.ParaQuestionId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntQuestionnaire.LanguageId))
                {
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntQuestionnaire.LanguageId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                dset = _sqlObject.SqlDataAdapter(_sqlcmd, _strConnString);
                dtableQuestionnaire = dset.Tables[0];
                dtableSections = dset.Tables[1];
                dtableQuestions = dset.Tables[2];
                dtableOptions = dset.Tables[3];
                if (dset.Tables.Count > 4)
                { dtableAttempts = dset.Tables[4]; }
                if (dset.Tables.Count == 6)
                    dtableResponse = dset.Tables[5];
                foreach (DataRow drow in dtableQuestionnaire.Rows)
                {
                    entQuestionnaire = new Questionnaire();
                    entQuestionnaire = FillQuestionnaire(drow);
                    if (dset.Tables.Count > 4)
                    {
                        foreach (DataRow drowAttempt in dtableAttempts.Select(Schema.Questionnaire.COL_QUESTIONNAIRE_ID + "='" + entQuestionnaire.ID + "'"))
                        {
                            entAttempt = new UserQuestionnaireTracking();
                            entAttempt = FillAttempt(drowAttempt);
                            if (dset.Tables.Count == 6)
                            {
                                foreach (DataRow drowResponse in dtableResponse.Select(Schema.UserQuestionnaireTracking.COL_ATTEMPT_ID + "='" + entAttempt.ID + "'"))
                                {
                                    entResponse = new UserQuestionnaireSessionResponses();
                                    entResponse = FillResponse(drowResponse);
                                    entAttempt.UserSessionResponse.Add(entResponse);
                                }
                            }
                            entQuestionnaire.UserAttemptTracking.Add(entAttempt);
                        }
                    }
                    foreach (DataRow drowSection in dtableSections.Select(Schema.Questionnaire.COL_QUESTIONNAIRE_ID + "='" + entQuestionnaire.ID + "'"))
                    {
                        entSection = new QuestionnaireSections();
                        entSection = FillSection(drowSection);

                        foreach (DataRow drowQuestion in dtableQuestions.Select(Schema.QuestionnaireQuestion.COL_SECTION_ID + "='" + entSection.ID + "'"))
                        {
                            entQuestion = new QuestionnaireQuestion();
                            entQuestion = FillQuestion(drowQuestion);
                            foreach (DataRow drowOption in dtableOptions.Select(Schema.QuestionnaireQuestion.COL_QUESTION_ID + "='" + entQuestion.ID + "'"))
                            {
                                entOption = new QuestionOptions();
                                entOption = FillOption(drowOption);
                                entQuestion.QuestionOptions.Add(entOption);
                            }
                            entSection.QuestionnaireQuestion.Add(entQuestion);
                        }
                        entQuestionnaire.Sections.Add(entSection);
                    }
                    entListQuestionnaire.Add(entQuestionnaire);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListQuestionnaire;
        }




        /// <summary>
        /// Get Questionnaire data with tracking infomration.
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns></returns>
        public List<Questionnaire> GetQuestionnaireTrackingWithOutPaging(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            List<Questionnaire> entListQuestionnaire = new List<Questionnaire>();
            Questionnaire entQuestionnaire = null;
            QuestionnaireSections entSection = null;
            QuestionnaireQuestion entQuestion = null;
            QuestionOptions entOption = null;

            UserQuestionnaireTracking entAttempt = null;
            UserQuestionnaireSessionResponses entResponse = null;

            _sqlcmd = new SqlCommand();
            DataSet dset = new DataSet();
            DataTable dtableQuestionnaire;
            DataTable dtableSections;
            DataTable dtableQuestions;
            DataTable dtableOptions;
            DataTable dtableAttempts = null;
            DataTable dtableResponse = null;

            _sqlcmd.CommandText = Schema.Questionnaire.PROC_GET_QUESTIONNAIRE_TRACKING_WITHOUT_PAGING;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);

                if (!string.IsNullOrEmpty(pEntQuestionnaire.ID))
                {
                    _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntQuestionnaire.UserAttemptTracking != null && pEntQuestionnaire.UserAttemptTracking.Count > 0)
                {
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntQuestionnaire.UserAttemptTracking[0].SystemUserGUID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntQuestionnaire.ParaQuestionId))
                {
                    _sqlpara = new SqlParameter(Schema.QuestionnaireQuestion.PARA_QUESTION_ID, pEntQuestionnaire.ParaQuestionId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntQuestionnaire.LanguageId))
                {
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntQuestionnaire.LanguageId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                dset = _sqlObject.SqlDataAdapter(_sqlcmd, _strConnString);
                dtableQuestionnaire = dset.Tables[0];
                dtableSections = dset.Tables[1];
                dtableQuestions = dset.Tables[2];
                dtableOptions = dset.Tables[3];
                if (dset.Tables.Count > 4)
                { dtableAttempts = dset.Tables[4]; }
                if (dset.Tables.Count == 6)
                    dtableResponse = dset.Tables[5];
                foreach (DataRow drow in dtableQuestionnaire.Rows)
                {
                    entQuestionnaire = new Questionnaire();
                    entQuestionnaire = FillQuestionnaireWithOutPaging(drow);
                    if (dset.Tables.Count > 4)
                    {
                        foreach (DataRow drowAttempt in dtableAttempts.Select(Schema.Questionnaire.COL_QUESTIONNAIRE_ID + "='" + entQuestionnaire.ID + "'"))
                        {
                            entAttempt = new UserQuestionnaireTracking();
                            entAttempt = FillAttempt(drowAttempt);
                            if (dset.Tables.Count == 6)
                            {
                                foreach (DataRow drowResponse in dtableResponse.Select(Schema.UserQuestionnaireTracking.COL_ATTEMPT_ID + "='" + entAttempt.ID + "'"))
                                {
                                    entResponse = new UserQuestionnaireSessionResponses();
                                    entResponse = FillResponse(drowResponse);
                                    entAttempt.UserSessionResponse.Add(entResponse);
                                }
                            }
                            entQuestionnaire.UserAttemptTracking.Add(entAttempt);
                        }
                    }
                    foreach (DataRow drowSection in dtableSections.Select(Schema.Questionnaire.COL_QUESTIONNAIRE_ID + "='" + entQuestionnaire.ID + "'"))
                    {
                        entSection = new QuestionnaireSections();
                        entSection = FillSectionWithOutPaging(drowSection);

                        foreach (DataRow drowQuestion in dtableQuestions.Select(Schema.QuestionnaireQuestion.COL_SECTION_ID + "='" + entSection.ID + "'"))
                        {
                            entQuestion = new QuestionnaireQuestion();
                            entQuestion = FillQuestionWithOutPaging(drowQuestion);
                            foreach (DataRow drowOption in dtableOptions.Select(Schema.QuestionnaireQuestion.COL_QUESTION_ID + "='" + entQuestion.ID + "'"))
                            {
                                entOption = new QuestionOptions();
                                entOption = FillOptionWithOutPaging(drowOption);
                                entQuestion.QuestionOptions.Add(entOption);
                            }
                            entSection.QuestionnaireQuestion.Add(entQuestion);
                        }
                        entQuestionnaire.Sections.Add(entSection);
                    }
                    entListQuestionnaire.Add(entQuestionnaire);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListQuestionnaire;
        }


        /// <summary>
        /// Fill Questionnaire object from DataRow.
        /// </summary>
        /// <param name="pDataRow"></param>
        /// <returns></returns>
        private Questionnaire FillQuestionnaire(DataRow pDataRow)
        {
            Questionnaire entQuestN = new Questionnaire();
            try
            {
                entQuestN.ID = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTIONNAIRE_ID]);
                entQuestN.QuestionnaireDescription = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_DESC]);
                entQuestN.QuestionnaireInstructionBottom = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_INST_BOT]);
                entQuestN.QuestionnaireInstructionTop = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_INST_TOP]);
                entQuestN.QuestionnaireTitle = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_TITLE]);
                entQuestN.QuestionnaireType = (Questionnaire.QuestionnaireDisplayType)Enum.Parse(typeof(Questionnaire.QuestionnaireDisplayType), Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTIONNAIRE_TYPE]));
                entQuestN.ReviewEmail = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_REVIEW_EMAIL]);

                entQuestN.AllowUserLangSelection = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_ALLOW_USER_LANGSELECTION]);
                entQuestN.AllowSequencing = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_ALLOW_SEQUENCING]);
                entQuestN.ApprovalStatus = (Questionnaire.QuestionnaireApprovalStatus)Enum.Parse(typeof(Questionnaire.QuestionnaireApprovalStatus), Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_APPROVAL_STATUS]));
                if (!string.IsNullOrEmpty(Convert.ToString(pDataRow[Schema.Questionnaire.COL_LOGO_POSITION])))
                    entQuestN.LogoPosition = (Questionnaire.QuestionnaireLogoPosition)Enum.Parse(typeof(Questionnaire.QuestionnaireLogoPosition), Convert.ToString(pDataRow[Schema.Questionnaire.COL_LOGO_POSITION]));
                if (!string.IsNullOrEmpty(Convert.ToString(pDataRow[Schema.Questionnaire.COL_TRACKING_TYPE])))
                    entQuestN.TrackingType = (Questionnaire.QuestionnaireSubmitType)Enum.Parse(typeof(Questionnaire.QuestionnaireSubmitType), Convert.ToString(pDataRow[Schema.Questionnaire.COL_TRACKING_TYPE]));
                entQuestN.ApprovedById = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_APPROVED_BY_ID]);
                entQuestN.BGColor = Convert.ToString(pDataRow[Schema.Questionnaire.COL_BGCOLOR]);
                entQuestN.ButtonExitTxt = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_BTN_EXIT_TXT]);
                entQuestN.ButtonNextTxt = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_BTN_NEXT_TXT]);
                entQuestN.ButtonPreviousTxt = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_BTN_PREVIOUS_TXT]);
                entQuestN.ButtonSubmitTxt = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_BTN_SUBMIT_TXT]);
                entQuestN.ButtonSaveTxt = Convert.ToString(pDataRow[Schema.Questionnaire.COL_BTN_SAVE_TXT]);
                entQuestN.ButtonPrintTxt = Convert.ToString(pDataRow[Schema.Questionnaire.COL_BUTTONPRINTTXT]);
                if (!string.IsNullOrEmpty(Convert.ToString(pDataRow[Schema.Questionnaire.COL_IS_LEARNERPRINTABLE])))
                    entQuestN.IsLearnerPrintable = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_LEARNERPRINTABLE]);
                entQuestN.CFForReviewEmail = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_CF_REVIEW_EMAIL]);

                entQuestN.CreatedById = Convert.ToString(pDataRow[Schema.Common.COL_CREATED_BY]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_DATE_APPROVED].ToString()))
                    entQuestN.DateApproved = Convert.ToDateTime(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_DATE_APPROVED]);
                entQuestN.DateCreated = Convert.ToDateTime(pDataRow[Schema.Common.COL_CREATED_ON]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_REVIEW_SENT_DATE].ToString()))
                    entQuestN.DateLastReviewSent = Convert.ToDateTime(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_REVIEW_SENT_DATE]);

                entQuestN.DefaultLogoPath = Convert.ToString(pDataRow[Schema.Questionnaire.COL_DEFAULTLOGOPATH]);
                entQuestN.IsActive = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_ACTIVE]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_LOGO_ONALL].ToString()))
                    entQuestN.IsLogoOnAll = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_LOGO_ONALL]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_PRINT_CERTIFICATE].ToString()))
                    entQuestN.IsPrintCertificate = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_PRINT_CERTIFICATE]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_MULTI_SUBMITALLOWED].ToString()))
                    entQuestN.IsMultiSubmitAllowed = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_MULTI_SUBMITALLOWED]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_PARTIAL_SUBMITALLOWED].ToString()))
                    entQuestN.IsPartialSubmitAllowed = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_PARTIAL_SUBMITALLOWED]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_REVIEW_ANSWER].ToString()))
                    entQuestN.IsReviewAnswer = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_REVIEW_ANSWER]);
                entQuestN.LanguageId = Convert.ToString(pDataRow[Schema.Language.COL_LANGUAGE_ID]);
                entQuestN.LanguageLogoPath = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_LOGO_PATH]);
                entQuestN.LastModifiedById = Convert.ToString(pDataRow[Schema.Common.COL_MODIFIED_BY]);
                entQuestN.LastModifiedDate = Convert.ToDateTime(pDataRow[Schema.Common.COL_MODIFIED_ON]);
                entQuestN.IsLocked = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_LOCKED]);
                //-- new col added
                entQuestN.MaxResponseLength = Convert.ToInt32(pDataRow[Schema.Questionnaire.COL_MAX_RESPONSE_LENGTH]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_USESECTIONS].ToString()))
                    entQuestN.IsUseSections = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_USESECTIONS]);

                entQuestN.BGColorHF = Convert.ToString(pDataRow[Schema.Questionnaire.COL_BGHEADER_HF]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_LOGOHIDE].ToString()))
                    entQuestN.IsLogoHide = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_LOGOHIDE]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_LOGOONFIRSTPAGEONLY].ToString()))
                    entQuestN.IsLogoOnFirstPageOnly = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_LOGOONFIRSTPAGEONLY]);

                if (pDataRow.Table.Columns.Contains(Schema.Questionnaire.COL_IS_ALL_ANSWER_MUST))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_ALL_ANSWER_MUST].ToString()))
                        entQuestN.IsAllAnswerMust = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_ALL_ANSWER_MUST]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Questionnaire.COL_IS_SHOW_QUESTIONNUMBER))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_SHOW_QUESTIONNUMBER].ToString()))
                        entQuestN.IsShowQuestionNumber = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_SHOW_QUESTIONNUMBER]);
                }


                if (pDataRow.Table.Columns.Contains(Schema.Questionnaire.COL_FONT_NAME))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_FONT_NAME].ToString()))
                        entQuestN.FontName = Convert.ToString(pDataRow[Schema.Questionnaire.COL_FONT_NAME]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Questionnaire.COL_FONT_SIZE))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_FONT_SIZE].ToString()))
                        entQuestN.FontSize = Convert.ToInt32(pDataRow[Schema.Questionnaire.COL_FONT_SIZE]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Questionnaire.COL_FONT_COLOR))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_FONT_COLOR].ToString()))
                        entQuestN.FontColor = Convert.ToString(pDataRow[Schema.Questionnaire.COL_FONT_COLOR]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Questionnaire.COL_QUEBG_COLOR))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_QUEBG_COLOR].ToString()))
                        entQuestN.QuestionaireBGColor = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUEBG_COLOR]);
                }
                if (pDataRow.Table.Columns.Contains(Schema.Questionnaire.COL_IS_QUESTIONNAIRE_SURVEY))
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(pDataRow[Schema.Questionnaire.COL_IS_QUESTIONNAIRE_SURVEY])))
                        entQuestN.IsQuestionnaireForSurvey = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_QUESTIONNAIRE_SURVEY]);
                }


            }
            catch (Exception exc)
            {
                throw exc;
            }
            return entQuestN;

        }

        /// <summary>
        /// Fill Questionnaire object from DataRow.
        /// </summary>
        /// <param name="pDataRow"></param>
        /// <returns></returns>
        private Questionnaire FillQuestionnaireWithOutPaging(DataRow pDataRow)
        {
            Questionnaire entQuestN = new Questionnaire();
            try
            {
                entQuestN.ID = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTIONNAIRE_ID]);
                entQuestN.QuestionnaireDescription = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_DESC]);
                entQuestN.QuestionnaireInstructionBottom = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_INST_BOT]);
                entQuestN.QuestionnaireInstructionTop = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_INST_TOP]);
                entQuestN.QuestionnaireTitle = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_TITLE]);
                entQuestN.QuestionnaireType = (Questionnaire.QuestionnaireDisplayType)Enum.Parse(typeof(Questionnaire.QuestionnaireDisplayType), Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTIONNAIRE_TYPE]));
                entQuestN.ReviewEmail = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_REVIEW_EMAIL]);

                entQuestN.AllowUserLangSelection = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_ALLOW_USER_LANGSELECTION]);
                entQuestN.AllowSequencing = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_ALLOW_SEQUENCING]);
                entQuestN.ApprovalStatus = (Questionnaire.QuestionnaireApprovalStatus)Enum.Parse(typeof(Questionnaire.QuestionnaireApprovalStatus), Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_APPROVAL_STATUS]));
                if (!string.IsNullOrEmpty(Convert.ToString(pDataRow[Schema.Questionnaire.COL_LOGO_POSITION])))
                    entQuestN.LogoPosition = (Questionnaire.QuestionnaireLogoPosition)Enum.Parse(typeof(Questionnaire.QuestionnaireLogoPosition), Convert.ToString(pDataRow[Schema.Questionnaire.COL_LOGO_POSITION]));
                if (!string.IsNullOrEmpty(Convert.ToString(pDataRow[Schema.Questionnaire.COL_TRACKING_TYPE])))
                    entQuestN.TrackingType = (Questionnaire.QuestionnaireSubmitType)Enum.Parse(typeof(Questionnaire.QuestionnaireSubmitType), Convert.ToString(pDataRow[Schema.Questionnaire.COL_TRACKING_TYPE]));
                entQuestN.ApprovedById = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_APPROVED_BY_ID]);
                entQuestN.BGColor = Convert.ToString(pDataRow[Schema.Questionnaire.COL_BGCOLOR]);
                entQuestN.ButtonExitTxt = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_BTN_EXIT_TXT]);
                entQuestN.ButtonNextTxt = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_BTN_NEXT_TXT]);
                entQuestN.ButtonPreviousTxt = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_BTN_PREVIOUS_TXT]);
                entQuestN.ButtonSubmitTxt = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_BTN_SUBMIT_TXT]);
                entQuestN.ButtonSaveTxt = Convert.ToString(pDataRow[Schema.Questionnaire.COL_BTN_SAVE_TXT]);

                if (!string.IsNullOrEmpty(Convert.ToString(pDataRow[Schema.Questionnaire.COL_BUTTONPRINTTXT])))
                    entQuestN.ButtonPrintTxt = Convert.ToString(pDataRow[Schema.Questionnaire.COL_BUTTONPRINTTXT]);

                if (!string.IsNullOrEmpty(Convert.ToString(pDataRow[Schema.Questionnaire.COL_IS_LEARNERPRINTABLE])))
                    entQuestN.IsLearnerPrintable = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_LEARNERPRINTABLE]);

                entQuestN.CFForReviewEmail = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_CF_REVIEW_EMAIL]);


                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_DATE_APPROVED].ToString()))
                    entQuestN.DateApproved = Convert.ToDateTime(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_DATE_APPROVED]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_REVIEW_SENT_DATE].ToString()))
                    entQuestN.DateLastReviewSent = Convert.ToDateTime(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_REVIEW_SENT_DATE]);

                entQuestN.DefaultLogoPath = Convert.ToString(pDataRow[Schema.Questionnaire.COL_DEFAULTLOGOPATH]);
                entQuestN.IsActive = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_ACTIVE]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_LOGO_ONALL].ToString()))
                    entQuestN.IsLogoOnAll = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_LOGO_ONALL]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_PRINT_CERTIFICATE].ToString()))
                    entQuestN.IsPrintCertificate = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_PRINT_CERTIFICATE]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_MULTI_SUBMITALLOWED].ToString()))
                    entQuestN.IsMultiSubmitAllowed = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_MULTI_SUBMITALLOWED]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_PARTIAL_SUBMITALLOWED].ToString()))
                    entQuestN.IsPartialSubmitAllowed = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_PARTIAL_SUBMITALLOWED]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_REVIEW_ANSWER].ToString()))
                    entQuestN.IsReviewAnswer = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_REVIEW_ANSWER]);
                entQuestN.LanguageId = Convert.ToString(pDataRow[Schema.Language.COL_LANGUAGE_ID]);
                entQuestN.LanguageLogoPath = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUESTN_LANG_LOGO_PATH]);

                //-- new col added
                entQuestN.MaxResponseLength = Convert.ToInt32(pDataRow[Schema.Questionnaire.COL_MAX_RESPONSE_LENGTH]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_USESECTIONS].ToString()))
                    entQuestN.IsUseSections = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_USESECTIONS]);

                entQuestN.BGColorHF = Convert.ToString(pDataRow[Schema.Questionnaire.COL_BGHEADER_HF]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_LOGOHIDE].ToString()))
                    entQuestN.IsLogoHide = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_LOGOHIDE]);

                if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_LOGOONFIRSTPAGEONLY].ToString()))
                    entQuestN.IsLogoOnFirstPageOnly = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_LOGOONFIRSTPAGEONLY]);

                if (pDataRow.Table.Columns.Contains(Schema.Questionnaire.COL_IS_ALL_ANSWER_MUST))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_IS_ALL_ANSWER_MUST].ToString()))
                        entQuestN.IsAllAnswerMust = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_ALL_ANSWER_MUST]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Questionnaire.COL_FONT_NAME))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_FONT_NAME].ToString()))
                        entQuestN.FontName = Convert.ToString(pDataRow[Schema.Questionnaire.COL_FONT_NAME]);
                }
                if (pDataRow.Table.Columns.Contains(Schema.Questionnaire.COL_FONT_COLOR))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_FONT_COLOR].ToString()))
                        entQuestN.FontColor = Convert.ToString(pDataRow[Schema.Questionnaire.COL_FONT_COLOR]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Questionnaire.COL_FONT_SIZE))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_FONT_SIZE].ToString()))
                        entQuestN.FontSize = Convert.ToInt32(pDataRow[Schema.Questionnaire.COL_FONT_SIZE]);
                }

                if (pDataRow.Table.Columns.Contains(Schema.Questionnaire.COL_QUEBG_COLOR))
                {
                    if (!string.IsNullOrEmpty(pDataRow[Schema.Questionnaire.COL_QUEBG_COLOR].ToString()))
                        entQuestN.QuestionaireBGColor = Convert.ToString(pDataRow[Schema.Questionnaire.COL_QUEBG_COLOR]);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(pDataRow[Schema.Questionnaire.COL_IS_QUESTIONNAIRE_SURVEY])))
                    entQuestN.IsQuestionnaireForSurvey = Convert.ToBoolean(pDataRow[Schema.Questionnaire.COL_IS_QUESTIONNAIRE_SURVEY]);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return entQuestN;

        }

        /// <summary>
        /// Fill Section Object
        /// </summary>
        /// <param name="pDataRow"></param>
        /// <returns></returns>
        public QuestionnaireSections FillSection(DataRow pDataRow)
        {
            QuestionnaireSections entSection = new QuestionnaireSections();
            try
            {
                entSection.ID = Convert.ToString(pDataRow[Schema.QuestionnaireSections.COL_SECTION_ID]);
                entSection.IsActive = Convert.ToBoolean(pDataRow[Schema.QuestionnaireSections.COL_IS_ACTIVE]);
                entSection.LanguageId = Convert.ToString(pDataRow[Schema.Language.COL_LANGUAGE_ID]);
                entSection.QuestionnaireId = Convert.ToString(pDataRow[Schema.QuestionnaireSections.COL_QUESTIONNAIRE_ID]);
                entSection.SectionDescription = Convert.ToString(pDataRow[Schema.QuestionnaireSections.COL_SECTION_DESC]);
                entSection.SectionName = Convert.ToString(pDataRow[Schema.QuestionnaireSections.COL_SECTION_NAME]);
                entSection.SequenceOrder = Convert.ToInt32(pDataRow[Schema.QuestionnaireSections.COL_SEQUENCE_ORDER]);
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
        public QuestionnaireSections FillSectionWithOutPaging(DataRow pDataRow)
        {
            QuestionnaireSections entSection = new QuestionnaireSections();
            try
            {
                entSection.ID = Convert.ToString(pDataRow[Schema.QuestionnaireSections.COL_SECTION_ID]);
                entSection.IsActive = Convert.ToBoolean(pDataRow[Schema.QuestionnaireSections.COL_IS_ACTIVE]);
                entSection.LanguageId = Convert.ToString(pDataRow[Schema.Language.COL_LANGUAGE_ID]);
                entSection.QuestionnaireId = Convert.ToString(pDataRow[Schema.QuestionnaireSections.COL_QUESTIONNAIRE_ID]);
                entSection.SectionDescription = Convert.ToString(pDataRow[Schema.QuestionnaireSections.COL_SECTION_DESC]);
                entSection.SectionName = Convert.ToString(pDataRow[Schema.QuestionnaireSections.COL_SECTION_NAME]);
                entSection.SequenceOrder = Convert.ToInt32(pDataRow[Schema.QuestionnaireSections.COL_SEQUENCE_ORDER]);

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
        public QuestionnaireQuestion FillQuestion(DataRow pDataRow)
        {
            QuestionnaireQuestion entQuestion = new QuestionnaireQuestion();
            try
            {
                entQuestion.ID = Convert.ToString(pDataRow[Schema.QuestionnaireQuestion.COL_QUESTION_ID]);
                entQuestion.IsActive = Convert.ToBoolean(pDataRow[Schema.QuestionnaireQuestion.COL_IS_ACTIVE]);
                entQuestion.IsReviewAlert = Convert.ToBoolean(pDataRow[Schema.QuestionnaireQuestion.COL_IS_REVIEW_ALERT]);
                entQuestion.LanguageId = Convert.ToString(pDataRow[Schema.Language.COL_LANGUAGE_ID]);
                entQuestion.QuestionDescription = Convert.ToString(pDataRow[Schema.QuestionnaireQuestion.COL_QUESTION_DESC]);
                entQuestion.QuestionnaireID = Convert.ToString(pDataRow[Schema.QuestionnaireQuestion.COL_QUESTIONNAIRE_ID]);
                entQuestion.QuestionTitle = Convert.ToString(pDataRow[Schema.QuestionnaireQuestion.COL_QUESTION_TITLE]);
                entQuestion.QuestionType = (QuestionnaireQuestion.QuestionnaireQuestionType)Enum.Parse(typeof(QuestionnaireQuestion.QuestionnaireQuestionType), Convert.ToString(pDataRow[Schema.QuestionnaireQuestion.COL_QUESTION_TYPE]));
                entQuestion.SectionID = Convert.ToString(pDataRow[Schema.QuestionnaireQuestion.COL_SECTION_ID]);
                entQuestion.SequenceOrder = Convert.ToInt32(pDataRow[Schema.QuestionnaireQuestion.COL_SEQUENCE_ORDER]);
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
        public QuestionnaireQuestion FillQuestionWithOutPaging(DataRow pDataRow)
        {
            QuestionnaireQuestion entQuestion = new QuestionnaireQuestion();
            try
            {
                entQuestion.ID = Convert.ToString(pDataRow[Schema.QuestionnaireQuestion.COL_QUESTION_ID]);
                entQuestion.IsActive = Convert.ToBoolean(pDataRow[Schema.QuestionnaireQuestion.COL_IS_ACTIVE]);
                entQuestion.IsReviewAlert = Convert.ToBoolean(pDataRow[Schema.QuestionnaireQuestion.COL_IS_REVIEW_ALERT]);
                entQuestion.LanguageId = Convert.ToString(pDataRow[Schema.Language.COL_LANGUAGE_ID]);
                entQuestion.QuestionDescription = Convert.ToString(pDataRow[Schema.QuestionnaireQuestion.COL_QUESTION_DESC]);
                entQuestion.QuestionnaireID = Convert.ToString(pDataRow[Schema.QuestionnaireQuestion.COL_QUESTIONNAIRE_ID]);
                entQuestion.QuestionTitle = Convert.ToString(pDataRow[Schema.QuestionnaireQuestion.COL_QUESTION_TITLE]);
                entQuestion.QuestionType = (QuestionnaireQuestion.QuestionnaireQuestionType)Enum.Parse(typeof(QuestionnaireQuestion.QuestionnaireQuestionType), Convert.ToString(pDataRow[Schema.QuestionnaireQuestion.COL_QUESTION_TYPE]));
                entQuestion.SectionID = Convert.ToString(pDataRow[Schema.QuestionnaireQuestion.COL_SECTION_ID]);
                entQuestion.SequenceOrder = Convert.ToInt32(pDataRow[Schema.QuestionnaireQuestion.COL_SEQUENCE_ORDER]);

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
        private QuestionOptions FillOption(DataRow pDataRow)
        {
            QuestionOptions entOption = new QuestionOptions();
            try
            {
                entOption.ID = Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_OPTION_ID]);
                entOption.QuestionID = Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_QUESTION_ID]);
                entOption.QuestionnaireId = Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID]);
                entOption.SectionID = Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_SECTION_ID]);
                entOption.LanguageId = Convert.ToString(pDataRow[Schema.Language.COL_LANGUAGE_ID]);
                entOption.OptionDescription = Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_OPTION_DESC]);
                entOption.OptionTitle = Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_OPTION_TITLE]);
                entOption.SequenceOrder = Convert.ToInt32(pDataRow[Schema.QuestionnaireOptions.COL_SEQUENCE_ORDER]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.QuestionnaireOptions.COL_OPTION_TYPE].ToString()))
                    entOption.OptionType = (QuestionOptions.QuestionnaireOptionType)Enum.Parse(typeof(QuestionOptions.QuestionnaireOptionType), Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_OPTION_TYPE]));
                entOption.GoToQuestion = Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_GO_TO_QUESTION]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.QuestionnaireOptions.COL_IS_ACTIVE].ToString()))
                    entOption.IsActive = Convert.ToBoolean(pDataRow[Schema.QuestionnaireOptions.COL_IS_ACTIVE]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.QuestionnaireOptions.COL_IS_ALERT].ToString()))
                    entOption.IsAlert = Convert.ToBoolean(pDataRow[Schema.QuestionnaireOptions.COL_IS_ALERT]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.QuestionnaireOptions.COL_IS_EXPLANATION].ToString()))
                    entOption.IsExplanation = Convert.ToBoolean(pDataRow[Schema.QuestionnaireOptions.COL_IS_EXPLANATION]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.QuestionnaireOptions.COL_IS_LAUNCH_LU].ToString()))
                    entOption.IsLaunchLU = Convert.ToBoolean(pDataRow[Schema.QuestionnaireOptions.COL_IS_LAUNCH_LU]);
                entOption.ExplainationTitle = Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_EXPLAINATION_TITLE]);
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
        private QuestionOptions FillOptionWithOutPaging(DataRow pDataRow)
        {
            QuestionOptions entOption = new QuestionOptions();
            try
            {
                entOption.ID = Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_OPTION_ID]);
                entOption.QuestionID = Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_QUESTION_ID]);
                entOption.QuestionnaireId = Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID]);
                entOption.SectionID = Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_SECTION_ID]);
                entOption.LanguageId = Convert.ToString(pDataRow[Schema.Language.COL_LANGUAGE_ID]);
                entOption.OptionDescription = Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_OPTION_DESC]);
                entOption.OptionTitle = Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_OPTION_TITLE]);
                entOption.SequenceOrder = Convert.ToInt32(pDataRow[Schema.QuestionnaireOptions.COL_SEQUENCE_ORDER]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.QuestionnaireOptions.COL_OPTION_TYPE].ToString()))
                    entOption.OptionType = (QuestionOptions.QuestionnaireOptionType)Enum.Parse(typeof(QuestionOptions.QuestionnaireOptionType), Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_OPTION_TYPE]));
                entOption.GoToQuestion = Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_GO_TO_QUESTION]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.QuestionnaireOptions.COL_IS_ACTIVE].ToString()))
                    entOption.IsActive = Convert.ToBoolean(pDataRow[Schema.QuestionnaireOptions.COL_IS_ACTIVE]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.QuestionnaireOptions.COL_IS_ALERT].ToString()))
                    entOption.IsAlert = Convert.ToBoolean(pDataRow[Schema.QuestionnaireOptions.COL_IS_ALERT]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.QuestionnaireOptions.COL_IS_EXPLANATION].ToString()))
                    entOption.IsExplanation = Convert.ToBoolean(pDataRow[Schema.QuestionnaireOptions.COL_IS_EXPLANATION]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.QuestionnaireOptions.COL_IS_LAUNCH_LU].ToString()))
                    entOption.IsLaunchLU = Convert.ToBoolean(pDataRow[Schema.QuestionnaireOptions.COL_IS_LAUNCH_LU]);
                entOption.ExplainationTitle = Convert.ToString(pDataRow[Schema.QuestionnaireOptions.COL_EXPLAINATION_TITLE]);

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
        public UserQuestionnaireTracking FillAttempt(DataRow pDataRow)
        {
            UserQuestionnaireTracking entAttempt = new UserQuestionnaireTracking();
            try
            {
                entAttempt.ID = Convert.ToString(pDataRow[Schema.UserQuestionnaireTracking.COL_ATTEMPT_ID]);
                entAttempt.QuestionnaireId = Convert.ToString(pDataRow[Schema.UserQuestionnaireTracking.COL_QUESTIONNAIRE_ID]);
                entAttempt.AttemptLanguageId = Convert.ToString(pDataRow[Schema.UserQuestionnaireTracking.COL_ATTEMP_LANGUAGE_ID]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.UserQuestionnaireTracking.COL_COMPLETED_DATE].ToString()))
                    entAttempt.CompletatedDate = Convert.ToDateTime(pDataRow[Schema.UserQuestionnaireTracking.COL_COMPLETED_DATE]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.UserQuestionnaireTracking.COL_START_DATE].ToString()))
                    entAttempt.StartDate = Convert.ToDateTime(pDataRow[Schema.UserQuestionnaireTracking.COL_START_DATE]);
                entAttempt.SubmissionStatus = (ActivityCompletionStatus)Enum.Parse(typeof(ActivityCompletionStatus), Convert.ToString(pDataRow[Schema.UserQuestionnaireTracking.COL_SUBMISSION_STATUS]));
                entAttempt.SystemUserGUID = Convert.ToString(pDataRow[Schema.Learner.COL_USER_ID]);

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
        public UserQuestionnaireSessionResponses FillResponse(DataRow pDataRow)
        {
            UserQuestionnaireSessionResponses entResponse = new UserQuestionnaireSessionResponses();
            try
            {
                entResponse.ID = Convert.ToString(pDataRow[Schema.UserQuestSessionResponse.COL_ATTEMPT_ID]);
                entResponse.QuestionnaireId = Convert.ToString(pDataRow[Schema.UserQuestSessionResponse.COL_QUESTIONNAIRE_ID]);
                entResponse.SectionID = Convert.ToString(pDataRow[Schema.UserQuestSessionResponse.COL_SECTION_ID]);
                entResponse.QuestionID = Convert.ToString(pDataRow[Schema.UserQuestSessionResponse.COL_QUESTION_ID]);
                entResponse.QuestionOptionsID = Convert.ToString(pDataRow[Schema.UserQuestSessionResponse.COL_OPTION_ID]);
                if (!string.IsNullOrEmpty(pDataRow[Schema.UserQuestSessionResponse.COL_DATE_SUBMITTED].ToString()))
                    entResponse.DateSubmitted = Convert.ToDateTime(pDataRow[Schema.UserQuestSessionResponse.COL_DATE_SUBMITTED]);
                entResponse.ExplanationText = Convert.ToString(pDataRow[Schema.UserQuestSessionResponse.COL_EXPLANATION_TEXT]);
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return entResponse;
        }

        /// <summary>
        /// To get languages for questionnaire import functionality.
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns></returns>
        public List<Language> GetImportLanguages(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            List<Language> entListLanguage = new List<Language>();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Questionnaire.PROC_GET_IMPORT_LANGUAGE;
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
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
        /// To get list of languages for questionnaire export functionality
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns></returns>
        public List<Language> GetExportLanguages(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            List<Language> entListLanguage = new List<Language>();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Questionnaire.PROC_GET_EXPORT_LANGUAGE;
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntQuestionnaire.ClientId);
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
        /// To insert / update questionnaire data.
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <param name="pStrUpdateMode"></param>
        /// <param name="pIsCopy"></param>
        /// <returns></returns>
        public Questionnaire UpdateQuestionnaire(Questionnaire pEntQuestionnaire, string pStrUpdateMode, bool pIsCopy)
        {
            _sqlObject = new SQLObject();
            Questionnaire entQuestionnaire = new Questionnaire();
            SqlConnection sqlConn = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
                using (TransactionScope ts = new TransactionScope())
                {
                    sqlConn = new SqlConnection(_strConnString);
                    sqlConn.Open();
                    entQuestionnaire = SaveQuestionnaire(pEntQuestionnaire, sqlConn, pStrUpdateMode, pIsCopy);
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
            return entQuestionnaire;
        }

        /// <summary>
        /// Delete Questionnaire From Server and DataBase
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns>Questionnaire Object</returns>
        public Questionnaire DeleteQuestionnaire(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            Questionnaire entQuestionnaire = new Questionnaire();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Questionnaire.PROC_DELET_QUESTIONNAIRE;
            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
                //Delete Questionnaire from database
                int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                pEntQuestionnaire = null;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntQuestionnaire;
        }

        public Questionnaire DeleteQuestionnaireLanguage(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            Questionnaire entQuestionnaire = new Questionnaire();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Questionnaire.PROC_DELET_QUESTIONNAIRE_LANGUAGE;

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntQuestionnaire.LanguageId);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
                //Delete Questionnaire from database
                int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                pEntQuestionnaire = null;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntQuestionnaire;
        }

        /// <summary>
        /// To copy Questionnaire with all its childs data - sections,questions,options.
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns></returns>
        public Questionnaire CopyQuestionnaire(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            QuestionnaireSectionAdaptor sectionAdaptor = new QuestionnaireSectionAdaptor();
            QuestionnaireQuestionAdaptor questionAdaptor = new QuestionnaireQuestionAdaptor();
            QuestionOptionsAdaptor optionAdaptor = new QuestionOptionsAdaptor();
            Questionnaire entQuestioner = new Questionnaire();
            List<BaseEntity> entListBase = new List<BaseEntity>();

            SqlConnection sqlConn = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
                using (TransactionScope ts = new TransactionScope())
                {
                    sqlConn = new SqlConnection(_strConnString);
                    sqlConn.Open();
                    pEntQuestionnaire.ID = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_QUESTIONNAIRE_ID_PREFIX, Schema.Common.VAL_QUESTIONNAIRE_ID_LENGTH);
                    entQuestioner = pEntQuestionnaire;
                    SaveQuestionnaire(entQuestioner, sqlConn, Schema.Common.VAL_INSERT_MODE, true);

                    StringDictionary objStrQuestionIdDic = new StringDictionary();


                    foreach (QuestionnaireSections objSection in pEntQuestionnaire.Sections)
                    {
                        objSection.QuestionnaireId = pEntQuestionnaire.ID;
                        objSection.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                        List<QuestionnaireSections> listSection = new List<QuestionnaireSections>();
                        listSection.Add(objSection);
                        sectionAdaptor.AddSectionWithCopy(listSection, sqlConn);

                        foreach (QuestionnaireQuestion objQuestion in objSection.QuestionnaireQuestion)
                        {
                            string strOldId = objQuestion.ID;
                            objQuestion.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                            string strNewId = objQuestion.ID;
                            objStrQuestionIdDic.Add(strOldId, strNewId);
                            objQuestion.SectionID = objSection.ID;
                            objQuestion.QuestionnaireID = pEntQuestionnaire.ID;
                            foreach (QuestionOptions objOption in objQuestion.QuestionOptions)
                            {
                                objOption.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12); ;
                                objOption.QuestionID = objQuestion.ID;
                                objOption.SectionID = objSection.ID;
                                objOption.QuestionnaireId = pEntQuestionnaire.ID;
                            }


                        }



                    }

                    /*code change for duplicate question issue while copying records. By:rajendra on 16-apr-2010. */
                    foreach (QuestionnaireSections objSection in pEntQuestionnaire.Sections)
                    {
                        questionAdaptor.AddQuestionWithCopy(objSection.QuestionnaireQuestion, sqlConn, objStrQuestionIdDic);

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
        /// Import Questionnaire
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns></returns>
        public Questionnaire ImportQuestionnaire(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            QuestionnaireSectionAdaptor sectionAdaptor = new QuestionnaireSectionAdaptor();
            QuestionnaireQuestionAdaptor questionAdaptor = new QuestionnaireQuestionAdaptor();
            QuestionOptionsAdaptor optionAdaptor = new QuestionOptionsAdaptor();
            List<QuestionnaireSections> entListSecLanguage = new List<QuestionnaireSections>();
            List<QuestionnaireQuestion> entListQuestLanguage = new List<QuestionnaireQuestion>();
            List<QuestionOptions> entListOptLanguage = new List<QuestionOptions>();
            SqlConnection sqlConn = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
                using (TransactionScope ts = new TransactionScope())
                {
                    sqlConn = new SqlConnection(_strConnString);
                    sqlConn.Open();
                    SaveQuestionnaireLanguage(pEntQuestionnaire, sqlConn);
                    foreach (QuestionnaireSections objSection in pEntQuestionnaire.Sections)
                    {
                        entListSecLanguage.Add(objSection);
                        foreach (QuestionnaireQuestion objQuestion in objSection.QuestionnaireQuestion)
                        {
                            entListQuestLanguage.Add(objQuestion);
                            foreach (QuestionOptions objOption in objQuestion.QuestionOptions)
                            {
                                objOption.QuestionnaireId = pEntQuestionnaire.ID;
                                entListOptLanguage.Add(objOption);
                            }
                        }
                    }
                    sectionAdaptor.SaveImportSectionLanguages(entListSecLanguage, sqlConn);
                    questionAdaptor.SaveImportQuestionLanguages(entListQuestLanguage, sqlConn);
                    optionAdaptor.SaveImportOptionLanguages(entListOptLanguage, sqlConn);
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
            return pEntQuestionnaire;
        }

        /// <summary>
        /// Copy Import Questionnaire Languages
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns></returns>
        public Questionnaire CopyImportQuestionnaireLanguages(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            Questionnaire entQuestionnaire = new Questionnaire();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
                _sqlcmd = new SqlCommand();
                _sqlcmd.Connection = sqlConnection;
                _sqlcmd.CommandText = Schema.Questionnaire.PROC_ADD_IMPORT_LANGUAGE;
                _sqlcmd.Connection = sqlConnection;

                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntQuestionnaire.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_BASE_LANGUAGE_ID, pEntQuestionnaire.BaseLanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntQuestionnaire.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

                entQuestionnaire = GetQuestionnaireById(pEntQuestionnaire);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {

            }
            return entQuestionnaire;
        }

        /// <summary>
        /// To insert/update/copy questionnaire data.
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <param name="pSqlConn"></param>
        /// <param name="pStrUpdateMode"></param>
        /// <param name="pIsCopy"></param>
        /// <returns></returns>
        private Questionnaire SaveQuestionnaire(Questionnaire pEntQuestionnaire, SqlConnection pSqlConn, string pStrUpdateMode, bool pIsCopy)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Questionnaire.PROC_UPDATE_QUESTIONNAIRE;
            _sqlcmd.Connection = pSqlConn;
            if (string.IsNullOrEmpty(pEntQuestionnaire.ID))
            {
                pEntQuestionnaire.ID = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_QUESTIONNAIRE_ID_PREFIX, Schema.Common.VAL_QUESTIONNAIRE_ID_LENGTH);
            }


            // Added By rajendra for Question Number Display
            if (pStrUpdateMode == Schema.Common.VAL_INSERT_MODE && !(pIsCopy))
            {
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_SHOW_QUESTIONNUMBER, true);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            else
            {

                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_SHOW_QUESTIONNUMBER, pEntQuestionnaire.IsShowQuestionNumber);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, pStrUpdateMode);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_TYPE, pEntQuestionnaire.QuestionnaireType.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_IS_ACTIVE, pEntQuestionnaire.IsActive);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_IS_PRINT_CERTIFICATE, pEntQuestionnaire.IsPrintCertificate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_LEARNERPRINTABLE, pEntQuestionnaire.IsLearnerPrintable);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_ALLOW_SEQUENCING, pEntQuestionnaire.AllowSequencing);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_BGCOLOR, pEntQuestionnaire.BGColor);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_DEFAULTLOGOPATH, pEntQuestionnaire.DefaultLogoPath);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_LOGO_ONALL, pEntQuestionnaire.IsLogoOnAll);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_PARTIAL_SUBMITALLOWED, pEntQuestionnaire.IsPartialSubmitAllowed);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_MULTI_SUBMITALLOWED, pEntQuestionnaire.IsMultiSubmitAllowed);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_REVIEW_ANSWER, pEntQuestionnaire.IsReviewAnswer);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_ALLOW_USER_LANGSELECTION, pEntQuestionnaire.AllowUserLangSelection);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntQuestionnaire.ClientId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntQuestionnaire.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_TRACKING_TYPE, pEntQuestionnaire.TrackingType.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_LOGO_POSITION, pEntQuestionnaire.LogoPosition.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            //-- new col added
            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_MAX_RESPONSE_LENGTH, pEntQuestionnaire.MaxResponseLength);
            _sqlcmd.Parameters.Add(_sqlpara);

            // Code added on 12-Apr-2010.
            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_USESECTIONS, pEntQuestionnaire.IsUseSections);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_BGHEADER_HF, pEntQuestionnaire.BGColorHF);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_LOGOHIDE, pEntQuestionnaire.IsLogoHide);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_LOGOONFIRSTPAGEONLY, pEntQuestionnaire.IsLogoOnFirstPageOnly);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_ALL_ANSWER_MUST, pEntQuestionnaire.IsAllAnswerMust);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_FONT_NAME, pEntQuestionnaire.FontName);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_FONT_SIZE, pEntQuestionnaire.FontSize);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_FONT_COLOR, pEntQuestionnaire.FontColor);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUEBG_COLOR, pEntQuestionnaire.QuestionaireBGColor);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_QUESTIONNAIRE_SURVEY, pEntQuestionnaire.IsQuestionnaireForSurvey);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlObject.ExecuteNonQuery(_sqlcmd);

            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Questionnaire.PROC_UPDATE_QUESTN_LANGUAGE;
            _sqlcmd.CommandType = CommandType.StoredProcedure;
            _sqlcmd.Connection = pSqlConn;
            _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, pStrUpdateMode);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntQuestionnaire.LanguageId);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pIsCopy)
            {

                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_APPROVAL_STATUS, Questionnaire.QuestionnaireApprovalStatus.Draft.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_APPROVED_BY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_DATE_APPROVED, null);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            else
            {
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_APPROVAL_STATUS, pEntQuestionnaire.ApprovalStatus.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_APPROVED_BY_ID, pEntQuestionnaire.ApprovedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntQuestionnaire.DateApproved) < 0)
                    _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_DATE_APPROVED, pEntQuestionnaire.DateApproved);
                else
                    _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_DATE_APPROVED, null);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_TITLE, pEntQuestionnaire.QuestionnaireTitle);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_DESC, pEntQuestionnaire.QuestionnaireDescription);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_INST_TOP, pEntQuestionnaire.QuestionnaireInstructionTop);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_INST_BOT, pEntQuestionnaire.QuestionnaireInstructionBottom);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_CF_REVIEW_EMAIL, pEntQuestionnaire.CFForReviewEmail);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntQuestionnaire.DateLastReviewSent) < 0)
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_REVIEW_SENT_DATE, pEntQuestionnaire.DateLastReviewSent);
            else
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_REVIEW_SENT_DATE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_REVIEW_EMAIL, pEntQuestionnaire.ReviewEmail);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_BTN_NEXT_TXT, pEntQuestionnaire.ButtonNextTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_BTN_PREVIOUS_TXT, pEntQuestionnaire.ButtonPreviousTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_BTN_SUBMIT_TXT, pEntQuestionnaire.ButtonSubmitTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_BUTTONPRINTTXT, pEntQuestionnaire.ButtonPrintTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_BTN_EXIT_TXT, pEntQuestionnaire.ButtonExitTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_BTN_SAVE_TXT, pEntQuestionnaire.ButtonSaveTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_LOGO_PATH, pEntQuestionnaire.LanguageLogoPath);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntQuestionnaire.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlObject.ExecuteNonQuery(_sqlcmd);

            return pEntQuestionnaire;
        }

        /// <summary>
        /// Save Questionnaire Language
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <param name="pSqlConn"></param>
        /// <returns></returns>
        public Questionnaire SaveQuestionnaireLanguage(Questionnaire pEntQuestionnaire, SqlConnection pSqlConn)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Questionnaire.PROC_UPDATE_QUESTN_LANGUAGE;
            _sqlcmd.CommandType = CommandType.StoredProcedure;
            _sqlcmd.Connection = pSqlConn;

            _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntQuestionnaire.LanguageId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_APPROVAL_STATUS, pEntQuestionnaire.ApprovalStatus.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_APPROVED_BY_ID, pEntQuestionnaire.ApprovedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntQuestionnaire.DateApproved) < 0)
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_DATE_APPROVED, pEntQuestionnaire.DateApproved);
            else
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_DATE_APPROVED, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_TITLE, pEntQuestionnaire.QuestionnaireTitle);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_DESC, pEntQuestionnaire.QuestionnaireDescription);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_INST_TOP, pEntQuestionnaire.QuestionnaireInstructionTop);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_INST_BOT, pEntQuestionnaire.QuestionnaireInstructionBottom);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_CF_REVIEW_EMAIL, pEntQuestionnaire.CFForReviewEmail);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntQuestionnaire.DateLastReviewSent) < 0)
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_REVIEW_SENT_DATE, pEntQuestionnaire.DateLastReviewSent);
            else
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_REVIEW_SENT_DATE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_REVIEW_EMAIL, pEntQuestionnaire.ReviewEmail);
            _sqlcmd.Parameters.Add(_sqlpara);


            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_BTN_NEXT_TXT, pEntQuestionnaire.ButtonNextTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_BTN_PREVIOUS_TXT, pEntQuestionnaire.ButtonPreviousTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_BTN_SUBMIT_TXT, pEntQuestionnaire.ButtonSubmitTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_BTN_EXIT_TXT, pEntQuestionnaire.ButtonExitTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_BTN_SAVE_TXT, pEntQuestionnaire.ButtonSaveTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_BUTTONPRINTTXT, pEntQuestionnaire.ButtonPrintTxt);
            _sqlcmd.Parameters.Add(_sqlpara);



            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_LOGO_PATH, pEntQuestionnaire.LanguageLogoPath);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntQuestionnaire.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlObject.ExecuteNonQuery(_sqlcmd);

            return pEntQuestionnaire;
        }

        public Questionnaire SaveQuestionnaireLanguage(Questionnaire pEntQuestionnaire)
        {
            SqlConnection pSqlConn = null;

            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);
            pSqlConn = new SqlConnection(_strConnString);
            pSqlConn.Open();

            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Questionnaire.PROC_UPDATE_QUESTN_LANGUAGE;
            _sqlcmd.CommandType = CommandType.StoredProcedure;
            _sqlcmd.Connection = pSqlConn;

            _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntQuestionnaire.LanguageId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_APPROVAL_STATUS, pEntQuestionnaire.ApprovalStatus.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_APPROVED_BY_ID, pEntQuestionnaire.ApprovedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntQuestionnaire.DateApproved) < 0)
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_DATE_APPROVED, pEntQuestionnaire.DateApproved);
            else
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_DATE_APPROVED, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_TITLE, pEntQuestionnaire.QuestionnaireTitle);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_DESC, pEntQuestionnaire.QuestionnaireDescription);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_INST_TOP, pEntQuestionnaire.QuestionnaireInstructionTop);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_INST_BOT, pEntQuestionnaire.QuestionnaireInstructionBottom);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_CF_REVIEW_EMAIL, pEntQuestionnaire.CFForReviewEmail);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntQuestionnaire.DateLastReviewSent) < 0)
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_REVIEW_SENT_DATE, pEntQuestionnaire.DateLastReviewSent);
            else
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_REVIEW_SENT_DATE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_REVIEW_EMAIL, pEntQuestionnaire.ReviewEmail);
            _sqlcmd.Parameters.Add(_sqlpara);


            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_BTN_NEXT_TXT, pEntQuestionnaire.ButtonNextTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_BTN_PREVIOUS_TXT, pEntQuestionnaire.ButtonPreviousTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_BTN_SUBMIT_TXT, pEntQuestionnaire.ButtonSubmitTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_BTN_EXIT_TXT, pEntQuestionnaire.ButtonExitTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_BTN_SAVE_TXT, pEntQuestionnaire.ButtonSaveTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_BUTTONPRINTTXT, pEntQuestionnaire.ButtonPrintTxt);
            _sqlcmd.Parameters.Add(_sqlpara);



            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_LOGO_PATH, pEntQuestionnaire.LanguageLogoPath);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntQuestionnaire.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlObject.ExecuteNonQuery(_sqlcmd);

            return pEntQuestionnaire;
        }

        /// <summary>
        /// To Edit the Questionnaire data 
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns>Questionnaire Object</returns>
        public Questionnaire EditQuestionnaire(Questionnaire pEntQuestionnaire)
        {
            Questionnaire entQuestionnaire = new Questionnaire();
            try
            {
                //Update information in DataBase
                entQuestionnaire = Update(pEntQuestionnaire, Schema.Common.VAL_UPDATE_MODE);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entQuestionnaire;
        }

        /// <summary>
        /// private method to support both Add and Edit Questionnaire transactions.
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <param name="pUpdateMode"></param>
        /// <returns>Questionnaire Object</returns>
        private Questionnaire Update(Questionnaire pEntQuestionnaire, string pStrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Questionnaire.PROC_UPDATE_QUESTIONNAIRE;
            _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);

            if (string.IsNullOrEmpty(pEntQuestionnaire.ID))
            {
                pEntQuestionnaire.ID = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_QUESTIONNAIRE_ID_PREFIX, Schema.Common.VAL_QUESTIONNAIRE_ID_LENGTH);
            }
            if (pStrUpdateMode == Schema.Common.VAL_UPDATE_MODE)
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_TYPE, pEntQuestionnaire.QuestionnaireType.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_IS_ACTIVE, pEntQuestionnaire.IsActive);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_IS_PRINT_CERTIFICATE, pEntQuestionnaire.IsPrintCertificate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_ALLOW_SEQUENCING, pEntQuestionnaire.AllowSequencing);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_BGCOLOR, pEntQuestionnaire.BGColor);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_DEFAULTLOGOPATH, pEntQuestionnaire.DefaultLogoPath);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_LOGO_ONALL, pEntQuestionnaire.IsLogoOnAll);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_PARTIAL_SUBMITALLOWED, pEntQuestionnaire.IsPartialSubmitAllowed);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_MULTI_SUBMITALLOWED, pEntQuestionnaire.IsMultiSubmitAllowed);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_REVIEW_ANSWER, pEntQuestionnaire.IsReviewAnswer);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_ALLOW_USER_LANGSELECTION, pEntQuestionnaire.AllowUserLangSelection);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntQuestionnaire.ClientId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntQuestionnaire.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_TRACKING_TYPE, pEntQuestionnaire.TrackingType.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_LOGO_POSITION, pEntQuestionnaire.LogoPosition.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_MAX_RESPONSE_LENGTH, pEntQuestionnaire.MaxResponseLength);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_FONT_NAME, pEntQuestionnaire.FontName);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_FONT_SIZE, pEntQuestionnaire.FontSize);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_FONT_COLOR, pEntQuestionnaire.FontColor);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUEBG_COLOR, pEntQuestionnaire.QuestionaireBGColor);
            _sqlcmd.Parameters.Add(_sqlpara);


            int iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Questionnaire.PROC_UPDATE_QUESTN_LANGUAGE;
            _sqlcmd.CommandType = CommandType.StoredProcedure;

            if (pStrUpdateMode == Schema.Common.VAL_UPDATE_MODE)
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntQuestionnaire.LanguageId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_TITLE, pEntQuestionnaire.QuestionnaireTitle);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_DESC, pEntQuestionnaire.QuestionnaireDescription);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_INST_TOP, pEntQuestionnaire.QuestionnaireInstructionTop);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_INST_BOT, pEntQuestionnaire.QuestionnaireInstructionBottom);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_CF_REVIEW_EMAIL, pEntQuestionnaire.CFForReviewEmail);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntQuestionnaire.DateLastReviewSent) < 0)
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_REVIEW_SENT_DATE, pEntQuestionnaire.DateLastReviewSent);
            else
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_REVIEW_SENT_DATE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_REVIEW_EMAIL, pEntQuestionnaire.ReviewEmail);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_APPROVAL_STATUS, pEntQuestionnaire.ApprovalStatus.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_APPROVED_BY_ID, pEntQuestionnaire.ApprovedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntQuestionnaire.DateApproved) < 0)
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_DATE_APPROVED, pEntQuestionnaire.DateApproved);
            else
                _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_DATE_APPROVED, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_BTN_NEXT_TXT, pEntQuestionnaire.ButtonNextTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_BTN_PREVIOUS_TXT, pEntQuestionnaire.ButtonPreviousTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_BTN_SUBMIT_TXT, pEntQuestionnaire.ButtonSubmitTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_BTN_EXIT_TXT, pEntQuestionnaire.ButtonExitTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_BTN_SAVE_TXT, pEntQuestionnaire.ButtonSaveTxt);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTN_LANG_LOGO_PATH, pEntQuestionnaire.LanguageLogoPath);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntQuestionnaire.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            return pEntQuestionnaire;
        }

        /// <summary>
        /// Delete Questionnaire List From Server and DataBase
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns>Questionnaire Object</returns>
        public List<Questionnaire> DeleteQuestionnaireList(List<Questionnaire> pEntListQuestionnaire)
        {
            List<Questionnaire> entListQuestionnarie = new List<Questionnaire>();
            Questionnaire entQuestionarie;
            int _iTotalDeleted = 0;
            try
            {
                if (pEntListQuestionnaire.Count > 0)
                {
                    foreach (Questionnaire objBase in pEntListQuestionnaire)
                    {
                        try
                        {
                            entQuestionarie = DeleteQuestionnaire(objBase);
                            _iTotalDeleted = _iTotalDeleted + 1;
                        }
                        catch { }
                    }
                    EntityRange _entRange = new EntityRange();
                    _entRange.TotalRows = _iTotalDeleted;
                    entQuestionarie = new Questionnaire();
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
        /// Approve Questionnaire List From Server and DataBase
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns>Questionnaire Object</returns>
        public List<Questionnaire> ApproveQuestionnaireList(List<Questionnaire> pEntListQuestionnaire)
        {
            List<Questionnaire> entListQuestionnarie = new List<Questionnaire>();
            List<Questionnaire> pentListQuestionnarie = new List<Questionnaire>();
            Questionnaire entQuestionarie;
            try
            {
                _sqlObject = new SQLObject();
                _sqladapter = new SqlDataAdapter();
                _dtable = new DataTable();
                DataTable dQuestionTable = new DataTable();
                int iBatchSize = 0;

                if (pEntListQuestionnaire.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.Questionnaire.COL_QUESTIONNAIRE_ID);
                    _dtable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    _dtable.Columns.Add(Schema.Questionnaire.COL_QUESTN_LANG_APPROVAL_STATUS);
                    _dtable.Columns.Add(Schema.Questionnaire.COL_QUESTN_LANG_APPROVED_BY_ID);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);

                    foreach (Questionnaire objBase in pEntListQuestionnaire)
                    {
                        entQuestionarie = new Questionnaire();
                        entQuestionarie = objBase;

                        DataRow drow = _dtable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entQuestionarie.ClientId);

                        if (!String.IsNullOrEmpty(entQuestionarie.ID))
                        {
                            drow[Schema.Questionnaire.COL_QUESTIONNAIRE_ID] = entQuestionarie.ID;
                            if (!String.IsNullOrEmpty(entQuestionarie.LanguageId))
                            {
                                drow[Schema.Language.COL_LANGUAGE_ID] = entQuestionarie.LanguageId;
                            }
                            drow[Schema.Questionnaire.COL_QUESTN_LANG_APPROVAL_STATUS] = entQuestionarie.ApprovalStatus.ToString();
                            if (!String.IsNullOrEmpty(entQuestionarie.ApprovedById))
                            {
                                drow[Schema.Questionnaire.COL_QUESTN_LANG_APPROVED_BY_ID] = entQuestionarie.ApprovedById;
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
                        _sqlcmd.CommandText = Schema.Questionnaire.PROC_APPROVE_QUESTIONNAIRE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, SqlDbType.VarChar, 100, Schema.Questionnaire.COL_QUESTIONNAIRE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Questionnaire.PARA_QUESTN_LANG_APPROVAL_STATUS, SqlDbType.VarChar, 100, Schema.Questionnaire.COL_QUESTN_LANG_APPROVAL_STATUS);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Questionnaire.PARA_QUESTN_LANG_APPROVED_BY_ID, SqlDbType.VarChar, 100, Schema.Questionnaire.COL_QUESTN_LANG_APPROVED_BY_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        int _iDeleteRows = _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();

                        pentListQuestionnarie = new List<Questionnaire>();
                        EntityRange _entRange = new EntityRange();
                        _entRange.TotalRows = _iDeleteRows;
                        entQuestionarie = new Questionnaire();
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
        /// Approve Questionnaire List From Server and DataBase
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns>Questionnaire Object</returns>
        public List<Questionnaire> ActivateDeActivateStatusList(List<Questionnaire> pEntListQuestionnaire)
        {
            List<Questionnaire> entListQuestionnarie = new List<Questionnaire>();
            List<Questionnaire> pentListQuestionnarie = new List<Questionnaire>();
            Questionnaire entQuestionarie;
            try
            {
                _sqlObject = new SQLObject();
                _sqladapter = new SqlDataAdapter();
                _dtable = new DataTable();
                DataTable dQuestionTable = new DataTable();
                int iBatchSize = 0;
                if (pEntListQuestionnaire.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.Questionnaire.COL_QUESTIONNAIRE_ID);
                    _dtable.Columns.Add(Schema.Questionnaire.COL_IS_ACTIVE);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    foreach (Questionnaire objBase in pEntListQuestionnaire)
                    {
                        entQuestionarie = new Questionnaire();
                        entQuestionarie = objBase;

                        DataRow drow = _dtable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entQuestionarie.ClientId);

                        if (!String.IsNullOrEmpty(entQuestionarie.ID))
                        {
                            drow[Schema.Questionnaire.COL_QUESTIONNAIRE_ID] = entQuestionarie.ID;
                            drow[Schema.Questionnaire.COL_IS_ACTIVE] = entQuestionarie.IsActive;


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
                        _sqlcmd.CommandText = Schema.Questionnaire.PROC_ACTIVE_QUESTIONNAIRE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, SqlDbType.VarChar, 100, Schema.Questionnaire.COL_QUESTIONNAIRE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Questionnaire.PARA_IS_ACTIVE, SqlDbType.Bit, 100, Schema.Questionnaire.COL_IS_ACTIVE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        int _iDeleteRows = _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();

                        pentListQuestionnarie = new List<Questionnaire>();
                        EntityRange _entRange = new EntityRange();
                        _entRange.TotalRows = _iDeleteRows;
                        entQuestionarie = new Questionnaire();
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
        /// Update Default Sequence
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <param name="pStrUpdateMode"></param>
        /// <returns></returns>
        public Questionnaire UpdateDefaultSequence(Questionnaire pEntQuestionnaire)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Questionnaire.PROC_UPDATE_QUESTIONNAIRE_DEFAULT_SEQ;
            _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaire.ClientId);


            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaire.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_IS_DEFAULT_SEQ, pEntQuestionnaire.IsDefaultSequence);
            _sqlcmd.Parameters.Add(_sqlpara);

            int iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            return pEntQuestionnaire;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns></returns>
        public Questionnaire Get(Questionnaire pEntQuestionnaire)
        {
            return GetQuestionnaireById(pEntQuestionnaire);
        }
        /// <summary>
        /// Update Questionnaire
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <returns></returns>
        public Questionnaire Update(Questionnaire pEntQuestionnaire)
        {
            return EditQuestionnaire(pEntQuestionnaire);
        }
        #endregion
    }
}
