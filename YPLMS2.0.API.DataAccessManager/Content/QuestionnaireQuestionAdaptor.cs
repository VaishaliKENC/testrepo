using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    public class QuestionnaireQuestionAdaptor : IDataManager<QuestionnaireQuestion>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataAdapter _sqladapter = null;
        QuestionnaireQuestion _entQuestion = null;
        List<QuestionnaireQuestion> _entListQuestions = null;
        SqlConnection _sqlcon = null;
        DataTable _dtable = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.QuestionnaireQuestions.QUESTIONNAIRE_QUET_ERROR;
        SqlDataReader _sqlreader = null;
        EntityRange _entRange = null;
        #endregion

        public List<QuestionnaireQuestion> GetQuestionLanguageList(QuestionnaireQuestion pEntQuestions)
        {
            _sqlObject = new SQLObject();
            List<QuestionnaireQuestion> entListQuestions = new List<QuestionnaireQuestion>();
            QuestionnaireQuestion entQuestions = null;
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.QuestionnaireQuestion.PROC_GET_QUESTIONS_LANGUAGES;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestions.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.QuestionnaireQuestion.PARA_QUESTION_ID, pEntQuestions.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                if (pEntQuestions.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntQuestions.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntQuestions.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntQuestions.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entQuestions = FillObject(_sqlreader, true, _sqlObject);
                    entListQuestions.Add(entQuestions);
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
            return entListQuestions;
        }

        public QuestionnaireQuestion GetQuestionBySectionId(QuestionnaireQuestion pEntQuestion)
        {
            _sqlObject = new SQLObject();
            QuestionnaireQuestion entQuestion = new QuestionnaireQuestion();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestion.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                _sqlcmd = new SqlCommand(Schema.QuestionnaireQuestion.PROC_GET_QUESTIONS_BY_SECTIONID_WISE, sqlConnection);
                _sqlpara = new SqlParameter(Schema.QuestionnaireQuestion.PARA_SECTION_ID, pEntQuestion.SectionID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntQuestion.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entQuestion = FillObject(_sqlreader, false, _sqlObject);
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
            return entQuestion;
        }

        private QuestionnaireQuestion FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            QuestionnaireQuestion entQuestion = new QuestionnaireQuestion();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.QuestionnaireQuestion.COL_QUESTION_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.QuestionnaireQuestion.COL_QUESTION_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestion.ID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.QuestionnaireQuestion.COL_QUESTIONNAIRE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.QuestionnaireQuestion.COL_QUESTIONNAIRE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestion.QuestionnaireID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.QuestionnaireQuestion.COL_SECTION_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.QuestionnaireQuestion.COL_SECTION_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestion.SectionID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Language.COL_LANGUAGE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_LANGUAGE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestion.LanguageId = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Language.COL_LANG_ENG_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_LANG_ENG_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestion.LanguageName = pSqlReader.GetString(iIndex);
                    else
                        entQuestion.LanguageName = "English";
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.QuestionnaireQuestion.COL_QUESTION_TITLE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.QuestionnaireQuestion.COL_QUESTION_TITLE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestion.QuestionTitle = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.QuestionnaireQuestion.COL_QUESTION_DESC))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.QuestionnaireQuestion.COL_QUESTION_DESC);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestion.QuestionDescription = pSqlReader.GetString(iIndex);
                    else
                        entQuestion.QuestionDescription = " ";
                }



                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.QuestionnaireQuestion.COL_IS_ACTIVE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.QuestionnaireQuestion.COL_IS_ACTIVE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestion.IsActive = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestion.CreatedById = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestion.DateCreated = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestion.LastModifiedById = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestion.LastModifiedDate = pSqlReader.GetDateTime(iIndex);
                }

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                        if (!pSqlReader.IsDBNull(iIndex))
                            _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                        entQuestion.ListRange = _entRange;
                    }

                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestion.CreatedByName = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entQuestion.ModifiedByName = pSqlReader.GetString(iIndex);
                }

                if (string.IsNullOrEmpty(entQuestion.CreatedByName))
                    entQuestion.CreatedByName = "";

                if (string.IsNullOrEmpty(entQuestion.ModifiedByName))
                    entQuestion.ModifiedByName = "";


            }
            return entQuestion;
        }

        /// <summary>
        /// Add Question
        /// </summary>
        /// <param name="pEntQuestion"></param>
        /// <returns></returns>
        public QuestionnaireQuestion AddQuestion(QuestionnaireQuestion pEntQuestion)
        {
            _entQuestion = new QuestionnaireQuestion();
            _entListQuestions = new List<QuestionnaireQuestion>();
            _entListQuestions.Add(pEntQuestion);
            _entListQuestions = BulkUpdate(_entListQuestions, Schema.Common.VAL_INSERT_MODE);
            return _entListQuestions[0];
        }

        /// <summary>
        /// Edit Question
        /// </summary>
        /// <param name="pEntQuestion"></param>
        /// <returns></returns>
        public QuestionnaireQuestion EditQuestion(QuestionnaireQuestion pEntQuestion)
        {
            _entQuestion = new QuestionnaireQuestion();
            _entListQuestions = new List<QuestionnaireQuestion>();
            _entListQuestions.Add(pEntQuestion);
            _entListQuestions = BulkUpdate(_entListQuestions, Schema.Common.VAL_UPDATE_MODE);
            return _entListQuestions[0];
        }

        /// <summary>
        /// Delete Question
        /// </summary>
        /// <param name="pEntQuestion"></param>
        /// <returns></returns>
        public QuestionnaireQuestion DeleteQuestion(QuestionnaireQuestion pEntQuestion)
        {
            QuestionnaireQuestion entQuestion = new QuestionnaireQuestion();
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.QuestionnaireQuestion.PROC_DELETE_QUESTION;
            _sqlpara = new SqlParameter(Schema.QuestionnaireQuestion.PARA_QUESTIONNAIRE_ID, pEntQuestion.QuestionnaireID);
            _sqlcmd.Parameters.Add(_sqlpara);
            _sqlpara = new SqlParameter(Schema.QuestionnaireQuestion.PARA_QUESTION_ID, pEntQuestion.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestion.ClientId);
                int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                pEntQuestion = null;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntQuestion;
        }

        public QuestionnaireQuestion DeleteQuestionLanguage(QuestionnaireQuestion pEntQuestion)
        {
            QuestionnaireQuestion entQuestion = new QuestionnaireQuestion();
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.QuestionnaireQuestion.PROC_DELETE_QUESTION_LANGUAGE;

            //_sqlpara = new SqlParameter(Schema.QuestionnaireQuestion.PARA_QUESTIONNAIRE_ID, pEntQuestion.QuestionnaireID);
            //_sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.QuestionnaireQuestion.PARA_QUESTION_ID, pEntQuestion.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntQuestion.LanguageId);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestion.ClientId);
                int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                pEntQuestion = null;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntQuestion;
        }

        /// <summary>
        /// Bulk OR single add/edit question
        /// </summary>
        /// <param name="pEntListQuestions"></param>
        /// <param name="pstrUpdateMode"></param>
        /// <returns></returns>
        public List<QuestionnaireQuestion> BulkUpdate(List<QuestionnaireQuestion> pEntListQuestions, string pstrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            DataTable dLangTable = new DataTable();
            QuestionOptionsAdaptor optionAdaptor = new QuestionOptionsAdaptor();
            List<QuestionOptions> entListOptions = new List<QuestionOptions>();
            List<QuestionnaireQuestion> entListQuestions = new List<QuestionnaireQuestion>();
            int iBatchSize = 0;
            try
            {
                if (pEntListQuestions.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.QuestionnaireQuestion.COL_QUESTION_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireQuestion.COL_QUESTIONNAIRE_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireQuestion.COL_SECTION_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireQuestion.COL_QUESTION_TYPE);
                    _dtable.Columns.Add(Schema.QuestionnaireQuestion.COL_SEQUENCE_ORDER);
                    _dtable.Columns.Add(Schema.QuestionnaireQuestion.COL_IS_ACTIVE);
                    _dtable.Columns.Add(Schema.QuestionnaireQuestion.COL_IS_REVIEW_ALERT);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    dLangTable.Columns.Add(Schema.QuestionnaireQuestion.COL_QUESTION_ID);
                    dLangTable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    dLangTable.Columns.Add(Schema.QuestionnaireQuestion.COL_QUESTION_TITLE);
                    dLangTable.Columns.Add(Schema.QuestionnaireQuestion.COL_QUESTION_DESC);
                    dLangTable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    dLangTable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (QuestionnaireQuestion entQuestion in pEntListQuestions)
                    {
                        DataRow drow = _dtable.NewRow();
                        DataRow dLangRow = dLangTable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entQuestion.ClientId);

                        if (!String.IsNullOrEmpty(entQuestion.ID))
                            drow[Schema.QuestionnaireQuestion.COL_QUESTION_ID] = entQuestion.ID;
                        else
                        {
                            entQuestion.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                            drow[Schema.QuestionnaireQuestion.COL_QUESTION_ID] = entQuestion.ID;
                        }
                        drow[Schema.QuestionnaireQuestion.COL_QUESTIONNAIRE_ID] = entQuestion.QuestionnaireID;
                        drow[Schema.QuestionnaireQuestion.COL_SECTION_ID] = entQuestion.SectionID;
                        drow[Schema.QuestionnaireQuestion.COL_QUESTION_TYPE] = entQuestion.QuestionType;
                        drow[Schema.QuestionnaireQuestion.COL_SEQUENCE_ORDER] = entQuestion.SequenceOrder;
                        drow[Schema.QuestionnaireQuestion.COL_IS_ACTIVE] = entQuestion.IsActive;
                        drow[Schema.QuestionnaireQuestion.COL_IS_REVIEW_ALERT] = entQuestion.IsReviewAlert;
                        drow[Schema.Common.COL_MODIFIED_BY] = entQuestion.LastModifiedById;
                        drow[Schema.Common.COL_UPDATE_MODE] = pstrUpdateMode;

                        dLangRow[Schema.QuestionnaireQuestion.COL_QUESTION_ID] = entQuestion.ID;
                        dLangRow[Schema.Language.COL_LANGUAGE_ID] = entQuestion.LanguageId;
                        dLangRow[Schema.QuestionnaireQuestion.COL_QUESTION_TITLE] = entQuestion.QuestionTitle;
                        dLangRow[Schema.QuestionnaireQuestion.COL_QUESTION_DESC] = entQuestion.QuestionDescription;
                        dLangRow[Schema.Common.COL_MODIFIED_BY] = entQuestion.LastModifiedById;
                        dLangRow[Schema.Common.COL_UPDATE_MODE] = pstrUpdateMode;

                        _dtable.Rows.Add(drow);
                        dLangTable.Rows.Add(dLangRow);

                        iBatchSize = 1;

                        if (_dtable.Rows.Count > 0)
                        {
                            _sqlcmd = new SqlCommand();
                            _sqlcmd.CommandText = Schema.QuestionnaireQuestion.PROC_UPDATE_QUESTNN_QUESTION;
                            _sqlcmd.CommandType = CommandType.StoredProcedure;
                            _sqlcon = new SqlConnection(_strConnString);
                            _sqlcmd.Connection = _sqlcon;
                            _sqladapter.InsertCommand = _sqlcmd;
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_QUESTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireQuestion.COL_QUESTION_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_QUESTIONNAIRE_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireQuestion.COL_QUESTIONNAIRE_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireQuestion.COL_SECTION_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_QUESTION_TYPE, SqlDbType.VarChar, 100, Schema.QuestionnaireQuestion.COL_QUESTION_TYPE);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_SEQUENCE_ORDER, SqlDbType.Int, 10, Schema.QuestionnaireQuestion.COL_SEQUENCE_ORDER);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_IS_ACTIVE, SqlDbType.Bit, 1, Schema.QuestionnaireQuestion.COL_IS_ACTIVE);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_IS_REVIEW_ALERT, SqlDbType.Bit, 1, Schema.QuestionnaireQuestion.COL_IS_REVIEW_ALERT);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                            _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                            _sqladapter.UpdateBatchSize = iBatchSize;
                            _sqladapter.Update(_dtable);
                            _sqladapter.Dispose();

                            _sqladapter = new SqlDataAdapter();
                            _sqlcmd = new SqlCommand();
                            _sqlcmd.CommandText = Schema.QuestionnaireQuestion.PROC_UPDATE_QUESTNN_QUEST_LANGUAGE;
                            _sqlcmd.CommandType = CommandType.StoredProcedure;
                            _sqlcon = new SqlConnection(_strConnString);
                            _sqlcmd.Connection = _sqlcon;
                            _sqladapter.InsertCommand = _sqlcmd;
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_QUESTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireQuestion.COL_QUESTION_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_QUESTION_TITLE, SqlDbType.NVarChar, 0, Schema.QuestionnaireQuestion.COL_QUESTION_TITLE);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_QUESTION_DESC, SqlDbType.NVarChar, 0, Schema.QuestionnaireQuestion.COL_QUESTION_DESC);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                            _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                            _sqladapter.UpdateBatchSize = iBatchSize;
                            _sqladapter.Update(dLangTable);
                            _sqladapter.Dispose();
                        }
                        foreach (QuestionOptions objOption in entQuestion.QuestionOptions)
                        {
                            if (string.IsNullOrEmpty(objOption.QuestionID))
                                objOption.QuestionID = entQuestion.ID;

                            if (string.IsNullOrEmpty(objOption.QuestionnaireId))
                                objOption.QuestionnaireId = entQuestion.QuestionnaireID;

                            if (string.IsNullOrEmpty(objOption.SectionID))
                                objOption.SectionID = entQuestion.SectionID;

                            if (string.IsNullOrEmpty(objOption.ClientId))
                                objOption.ClientId = entQuestion.ClientId;

                            if (string.IsNullOrEmpty(objOption.CreatedById))
                                objOption.CreatedById = entQuestion.CreatedById;

                            if (string.IsNullOrEmpty(objOption.LastModifiedById))
                                objOption.LastModifiedById = entQuestion.LastModifiedById;

                            if (string.IsNullOrEmpty(objOption.LanguageId))
                                objOption.LanguageId = entQuestion.LanguageId;

                            entListOptions.Add(objOption);
                        }
                        entListOptions = optionAdaptor.BulkUpdate(entListOptions, pstrUpdateMode);
                        entListQuestions.Add(entQuestion);
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

        /// <summary>
        /// Add Question With Copy
        /// </summary>
        /// <param name="pEntListQuestions"></param>
        /// <param name="pSqlConn"></param>
        /// <returns></returns>
        public List<QuestionnaireQuestion> AddQuestionWithCopy(List<QuestionnaireQuestion> pEntListQuestions, SqlConnection pSqlConn, StringDictionary objStrQuestionIdDic)
        {
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            DataTable dLangTable = new DataTable();
            QuestionOptionsAdaptor optionAdaptor = new QuestionOptionsAdaptor();
            List<QuestionOptions> entListOptions = new List<QuestionOptions>();
            List<QuestionnaireQuestion> entListQuestions = new List<QuestionnaireQuestion>();
            int iBatchSize = 0;
            try
            {
                if (pEntListQuestions.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.QuestionnaireQuestion.COL_QUESTION_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireQuestion.COL_QUESTIONNAIRE_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireQuestion.COL_SECTION_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireQuestion.COL_QUESTION_TYPE);
                    _dtable.Columns.Add(Schema.QuestionnaireQuestion.COL_SEQUENCE_ORDER);
                    _dtable.Columns.Add(Schema.QuestionnaireQuestion.COL_IS_ACTIVE);
                    _dtable.Columns.Add(Schema.QuestionnaireQuestion.COL_IS_REVIEW_ALERT);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    dLangTable.Columns.Add(Schema.QuestionnaireQuestion.COL_QUESTION_ID);
                    dLangTable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    dLangTable.Columns.Add(Schema.QuestionnaireQuestion.COL_QUESTION_TITLE);
                    dLangTable.Columns.Add(Schema.QuestionnaireQuestion.COL_QUESTION_DESC);
                    dLangTable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    dLangTable.Columns.Add(Schema.Common.COL_UPDATE_MODE);


                    foreach (QuestionnaireQuestion entQuestion in pEntListQuestions)
                    {
                        /*code change for duplicate question issue while copying records. By:rajendra on 16-apr-2010. */
                        _dtable.Rows.Clear();
                        dLangTable.Rows.Clear();

                        DataRow drow = _dtable.NewRow();
                        DataRow dLangRow = dLangTable.NewRow();
                        if (!String.IsNullOrEmpty(entQuestion.ID))
                            drow[Schema.QuestionnaireQuestion.COL_QUESTION_ID] = entQuestion.ID;
                        else
                        {
                            entQuestion.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                            drow[Schema.QuestionnaireQuestion.COL_QUESTION_ID] = entQuestion.ID;
                        }
                        drow[Schema.QuestionnaireQuestion.COL_QUESTIONNAIRE_ID] = entQuestion.QuestionnaireID;
                        drow[Schema.QuestionnaireQuestion.COL_SECTION_ID] = entQuestion.SectionID;
                        drow[Schema.QuestionnaireQuestion.COL_QUESTION_TYPE] = entQuestion.QuestionType;
                        drow[Schema.QuestionnaireQuestion.COL_SEQUENCE_ORDER] = entQuestion.SequenceOrder;
                        drow[Schema.QuestionnaireQuestion.COL_IS_ACTIVE] = entQuestion.IsActive;
                        drow[Schema.QuestionnaireQuestion.COL_IS_REVIEW_ALERT] = entQuestion.IsReviewAlert;
                        drow[Schema.Common.COL_MODIFIED_BY] = entQuestion.LastModifiedById;
                        drow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_INSERT_MODE;

                        dLangRow[Schema.QuestionnaireQuestion.COL_QUESTION_ID] = entQuestion.ID;
                        dLangRow[Schema.Language.COL_LANGUAGE_ID] = entQuestion.LanguageId;
                        dLangRow[Schema.QuestionnaireQuestion.COL_QUESTION_TITLE] = entQuestion.QuestionTitle;
                        dLangRow[Schema.QuestionnaireQuestion.COL_QUESTION_DESC] = entQuestion.QuestionDescription;
                        dLangRow[Schema.Common.COL_MODIFIED_BY] = entQuestion.LastModifiedById;
                        dLangRow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_INSERT_MODE;

                        _dtable.Rows.Add(drow);
                        dLangTable.Rows.Add(dLangRow);

                        iBatchSize = 1;

                        if (_dtable.Rows.Count > 0)
                        {
                            _sqlcmd = new SqlCommand();
                            _sqlcmd.CommandText = Schema.QuestionnaireQuestion.PROC_UPDATE_QUESTNN_QUESTION;
                            _sqlcmd.CommandType = CommandType.StoredProcedure;
                            _sqlcmd.Connection = pSqlConn;
                            _sqladapter.InsertCommand = _sqlcmd;
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_QUESTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireQuestion.COL_QUESTION_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_QUESTIONNAIRE_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireQuestion.COL_QUESTIONNAIRE_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireQuestion.COL_SECTION_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_QUESTION_TYPE, SqlDbType.VarChar, 100, Schema.QuestionnaireQuestion.COL_QUESTION_TYPE);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_SEQUENCE_ORDER, SqlDbType.Int, 10, Schema.QuestionnaireQuestion.COL_SEQUENCE_ORDER);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_IS_ACTIVE, SqlDbType.Bit, 1, Schema.QuestionnaireQuestion.COL_IS_ACTIVE);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_IS_REVIEW_ALERT, SqlDbType.Bit, 1, Schema.QuestionnaireQuestion.COL_IS_REVIEW_ALERT);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                            _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                            _sqladapter.UpdateBatchSize = iBatchSize;
                            _sqladapter.Update(_dtable);
                            _sqladapter.Dispose();

                            _sqladapter = new SqlDataAdapter();
                            _sqlcmd = new SqlCommand();
                            _sqlcmd.CommandText = Schema.QuestionnaireQuestion.PROC_UPDATE_QUESTNN_QUEST_LANGUAGE;
                            _sqlcmd.CommandType = CommandType.StoredProcedure;
                            _sqlcmd.Connection = pSqlConn;
                            _sqladapter.InsertCommand = _sqlcmd;
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_QUESTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireQuestion.COL_QUESTION_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_QUESTION_TITLE, SqlDbType.NVarChar, 0, Schema.QuestionnaireQuestion.COL_QUESTION_TITLE);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_QUESTION_DESC, SqlDbType.NVarChar, 0, Schema.QuestionnaireQuestion.COL_QUESTION_DESC);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                            _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                            _sqladapter.UpdateBatchSize = iBatchSize;
                            _sqladapter.Update(dLangTable);
                            _sqladapter.Dispose();
                        }
                        entListOptions.Clear();

                        foreach (QuestionOptions objOption in entQuestion.QuestionOptions)
                        {
                            if (string.IsNullOrEmpty(objOption.QuestionID))
                                objOption.QuestionID = entQuestion.ID;

                            if (string.IsNullOrEmpty(objOption.QuestionnaireId))
                                objOption.QuestionnaireId = entQuestion.QuestionnaireID;

                            if (string.IsNullOrEmpty(objOption.SectionID))
                                objOption.SectionID = entQuestion.SectionID;

                            if (string.IsNullOrEmpty(objOption.ClientId))
                                objOption.ClientId = entQuestion.ClientId;

                            if (string.IsNullOrEmpty(objOption.CreatedById))
                                objOption.CreatedById = entQuestion.CreatedById;

                            if (string.IsNullOrEmpty(objOption.LastModifiedById))
                                objOption.LastModifiedById = entQuestion.LastModifiedById;

                            if (string.IsNullOrEmpty(objOption.LanguageId))
                                objOption.LanguageId = entQuestion.LanguageId;

                            entListOptions.Add(objOption);
                        }
                        entListOptions = optionAdaptor.AddOptionsWithCopy(entListOptions, pSqlConn, objStrQuestionIdDic);
                        entListQuestions.Add(entQuestion);
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

        /// <summary>
        /// Save Import Question Languages
        /// </summary>
        /// <param name="pEntListQuestions"></param>
        /// <param name="pSqlConn"></param>
        /// <returns></returns>
        public List<QuestionnaireQuestion> SaveImportQuestionLanguages(List<QuestionnaireQuestion> pEntListQuestions, SqlConnection pSqlConn)
        {
            _sqladapter = new SqlDataAdapter();
            DataTable dLangTable = new DataTable();
            QuestionOptionsAdaptor optionAdaptor = new QuestionOptionsAdaptor();
            List<QuestionnaireQuestion> entListQuestions = new List<QuestionnaireQuestion>();
            int iBatchSize = 0;
            try
            {
                if (pEntListQuestions.Count > 0)
                {
                    dLangTable.Columns.Add(Schema.QuestionnaireQuestion.COL_QUESTION_ID);
                    dLangTable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    dLangTable.Columns.Add(Schema.QuestionnaireQuestion.COL_QUESTION_TITLE);
                    dLangTable.Columns.Add(Schema.QuestionnaireQuestion.COL_QUESTION_DESC);
                    dLangTable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    dLangTable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (QuestionnaireQuestion objQuestion in pEntListQuestions)
                    {
                        DataRow dLangRow = dLangTable.NewRow();

                        dLangRow[Schema.QuestionnaireQuestion.COL_QUESTION_ID] = objQuestion.ID;
                        dLangRow[Schema.Language.COL_LANGUAGE_ID] = objQuestion.LanguageId;
                        dLangRow[Schema.QuestionnaireQuestion.COL_QUESTION_TITLE] = objQuestion.QuestionTitle;
                        dLangRow[Schema.QuestionnaireQuestion.COL_QUESTION_DESC] = objQuestion.QuestionDescription;
                        dLangRow[Schema.Common.COL_MODIFIED_BY] = objQuestion.LastModifiedById;
                        dLangRow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_UPDATE_MODE;

                        dLangTable.Rows.Add(dLangRow);

                        iBatchSize = iBatchSize + 1;
                        entListQuestions.Add(objQuestion);
                    }

                    if (dLangTable.Rows.Count > 0)
                    {
                        _sqladapter = new SqlDataAdapter();
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.QuestionnaireQuestion.PROC_UPDATE_QUESTNN_QUEST_LANGUAGE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcmd.Connection = pSqlConn;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_QUESTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireQuestion.COL_QUESTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_QUESTION_TITLE, SqlDbType.NVarChar, 0, Schema.QuestionnaireQuestion.COL_QUESTION_TITLE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireQuestion.PARA_QUESTION_DESC, SqlDbType.NVarChar, 0, Schema.QuestionnaireQuestion.COL_QUESTION_DESC);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        _sqladapter.Update(dLangTable);
                        _sqladapter.Dispose();
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

        /// <summary>
        /// Update Sequence
        /// </summary>
        /// <param name="pEntQuestionnaireQuestion"></param>
        /// <returns></returns>
        public QuestionnaireQuestion UpdateSequence(QuestionnaireQuestion pEntQuestionnaireQuestion)
        {
            int iRows = 0;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.QuestionnaireQuestion.PROC_UPDATE_SEQUENCE;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaireQuestion.ClientId);

                if (!string.IsNullOrEmpty(pEntQuestionnaireQuestion.QuestionnaireID))
                {
                    _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaireQuestion.QuestionnaireID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntQuestionnaireQuestion.SectionID))
                {
                    _sqlpara = new SqlParameter(Schema.QuestionnaireQuestion.PARA_SECTION_ID, pEntQuestionnaireQuestion.SectionID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.QuestionnaireQuestion.PARA_SECTION_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (Convert.ToString(pEntQuestionnaireQuestion.SequenceOrder) != string.Empty)
                {
                    _sqlpara = new SqlParameter(Schema.QuestionnaireQuestion.PARA_SEQUENCE_ORDER, pEntQuestionnaireQuestion.SequenceOrder);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.QuestionnaireQuestion.PARA_SEQUENCE_ORDER, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlpara = new SqlParameter(Schema.QuestionnaireQuestion.PARA_IS_UP, pEntQuestionnaireQuestion.IsMoveUp);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntQuestionnaireQuestion.ID))
                {
                    _sqlpara = new SqlParameter(Schema.QuestionnaireQuestion.PARA_QUESTION_ID, pEntQuestionnaireQuestion.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.QuestionnaireQuestion.PARA_QUESTION_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntQuestionnaireQuestion.LastModifiedById))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntQuestionnaireQuestion.LastModifiedById);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntQuestionnaireQuestion;
        }

        #region Interface Methods
        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="pEntQuestionnaireQuestion"></param>
        /// <returns>null</returns>
        public QuestionnaireQuestion Get(QuestionnaireQuestion pEntQuestionnaireQuestion)
        {
            return null;
        }
        /// <summary>
        /// Update QuestionnaireQuestion
        /// </summary>
        /// <param name="pEntQuestionnaireQuestion"></param>
        /// <returns></returns>
        public QuestionnaireQuestion Update(QuestionnaireQuestion pEntQuestionnaireQuestion)
        {
            return EditQuestion(pEntQuestionnaireQuestion);
        }
        #endregion
    }
}
