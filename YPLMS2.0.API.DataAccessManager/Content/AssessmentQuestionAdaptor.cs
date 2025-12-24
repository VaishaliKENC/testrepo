using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.YPLMS.Services;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public class AssessmentQuestionAdaptor : IDataManager<AssessmentQuestion>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataAdapter _sqladapter = null;
        AssessmentQuestion _entQuestion = null;
        List<AssessmentQuestion> _entListQuestions = null;
        SqlConnection _sqlcon = null;
        DataTable _dtable = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.AssessmentQuestions.ASSESSMENT_QUET_ERROR;
        #endregion

        /// <summary>
        /// Add Question
        /// </summary>
        /// <param name="pEntQuestion"></param>
        /// <returns></returns>
        public AssessmentQuestion AddQuestion(AssessmentQuestion pEntQuestion)
        {
            _entQuestion = new AssessmentQuestion();
            _entListQuestions = new List<AssessmentQuestion>();
            _entListQuestions.Add(pEntQuestion);
            _entListQuestions = BulkUpdate(_entListQuestions, Schema.Common.VAL_INSERT_MODE);
            return _entListQuestions[0];
        }

        /// <summary>
        /// Edit Question
        /// </summary>
        /// <param name="pEntQuestion"></param>
        /// <returns></returns>
        public AssessmentQuestion EditQuestion(AssessmentQuestion pEntQuestion)
        {
            _entQuestion = new AssessmentQuestion();
            _entListQuestions = new List<AssessmentQuestion>();
            _entListQuestions.Add(pEntQuestion);
            _entListQuestions = BulkUpdate(_entListQuestions, Schema.Common.VAL_UPDATE_MODE);
            return _entListQuestions[0];
        }

        /// <summary>
        /// Delete Question
        /// </summary>
        /// <param name="pEntQuestion"></param>
        /// <returns></returns>
        public AssessmentQuestion DeleteQuestion(AssessmentQuestion pEntQuestion)
        {
            AssessmentQuestion entQuestion = new AssessmentQuestion();
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.AssessmentQuestion.PROC_DELETE_QUESTION;
            _sqlpara = new SqlParameter(Schema.AssessmentQuestion.PARA_ASSESSMENT_ID, pEntQuestion.AssessmentID);
            _sqlcmd.Parameters.Add(_sqlpara);
            _sqlpara = new SqlParameter(Schema.AssessmentQuestion.PARA_QUESTION_ID, pEntQuestion.ID);
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
        public List<AssessmentQuestion> BulkUpdate(List<AssessmentQuestion> pEntListQuestions, string pstrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            DataTable dLangTable = new DataTable();
            AssessmentOptionsAdaptor optionAdaptor = new AssessmentOptionsAdaptor();
            List<AssessmentOptions> entListOptions = new List<AssessmentOptions>();
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

                    dLangTable.Columns.Add(Schema.AssessmentQuestion.COL_QUESTION_ID);
                    dLangTable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    dLangTable.Columns.Add(Schema.AssessmentQuestion.COL_QUESTION_TITLE);
                    dLangTable.Columns.Add(Schema.AssessmentQuestion.COL_QUESTION_DESC);
                    dLangTable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    dLangTable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (AssessmentQuestion entQuestion in pEntListQuestions)
                    {
                        DataRow drow = _dtable.NewRow();
                        DataRow dLangRow = dLangTable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entQuestion.ClientId);

                        if (!String.IsNullOrEmpty(entQuestion.ID))
                            drow[Schema.AssessmentQuestion.COL_QUESTION_ID] = entQuestion.ID;
                        else
                        {
                            entQuestion.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                            drow[Schema.AssessmentQuestion.COL_QUESTION_ID] = entQuestion.ID;
                        }
                        drow[Schema.AssessmentQuestion.COL_ASSESSMENT_ID] = entQuestion.AssessmentID;
                        drow[Schema.AssessmentQuestion.COL_SECTION_ID] = entQuestion.SectionID;
                        drow[Schema.AssessmentQuestion.COL_QUESTION_TYPE] = entQuestion.QuestionType;
                        drow[Schema.AssessmentQuestion.COL_SEQUENCE_ORDER] = entQuestion.SequenceOrder;
                        drow[Schema.AssessmentQuestion.COL_IS_ACTIVE] = entQuestion.IsActive;
                        drow[Schema.Common.COL_MODIFIED_BY] = entQuestion.LastModifiedById;
                        drow[Schema.Common.COL_UPDATE_MODE] = pstrUpdateMode;

                        dLangRow[Schema.AssessmentQuestion.COL_QUESTION_ID] = entQuestion.ID;
                        dLangRow[Schema.Language.COL_LANGUAGE_ID] = entQuestion.LanguageId;
                        dLangRow[Schema.AssessmentQuestion.COL_QUESTION_TITLE] = entQuestion.QuestionTitle;
                        dLangRow[Schema.AssessmentQuestion.COL_QUESTION_DESC] = entQuestion.QuestionDescription;
                        dLangRow[Schema.Common.COL_MODIFIED_BY] = entQuestion.LastModifiedById;
                        dLangRow[Schema.Common.COL_UPDATE_MODE] = pstrUpdateMode;

                        _dtable.Rows.Add(drow);
                        dLangTable.Rows.Add(dLangRow);

                        iBatchSize = 1;

                        if (_dtable.Rows.Count > 0)
                        {
                            _sqlcmd = new SqlCommand();
                            _sqlcmd.CommandText = Schema.AssessmentQuestion.PROC_UPDATE_ASSESSMENT_QUESTION;
                            _sqlcmd.CommandType = CommandType.StoredProcedure;
                            _sqlcon = new SqlConnection(_strConnString);
                            _sqlcmd.Connection = _sqlcon;
                            _sqladapter.InsertCommand = _sqlcmd;
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_QUESTION_ID, SqlDbType.VarChar, 100, Schema.AssessmentQuestion.COL_QUESTION_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_ASSESSMENT_ID, SqlDbType.VarChar, 100, Schema.AssessmentQuestion.COL_ASSESSMENT_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.AssessmentQuestion.COL_SECTION_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_QUESTION_TYPE, SqlDbType.VarChar, 100, Schema.AssessmentQuestion.COL_QUESTION_TYPE);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_SEQUENCE_ORDER, SqlDbType.Int, 10, Schema.AssessmentQuestion.COL_SEQUENCE_ORDER);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_IS_ACTIVE, SqlDbType.Bit, 1, Schema.AssessmentQuestion.COL_IS_ACTIVE);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                            _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                            _sqladapter.UpdateBatchSize = iBatchSize;
                            _sqladapter.Update(_dtable);
                            _sqladapter.Dispose();

                            _sqladapter = new SqlDataAdapter();
                            _sqlcmd = new SqlCommand();
                            _sqlcmd.CommandText = Schema.AssessmentQuestion.PROC_UPDATE_ASSESSMENT_QUEST_LANGUAGE;
                            _sqlcmd.CommandType = CommandType.StoredProcedure;
                            _sqlcon = new SqlConnection(_strConnString);
                            _sqlcmd.Connection = _sqlcon;
                            _sqladapter.InsertCommand = _sqlcmd;
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_QUESTION_ID, SqlDbType.VarChar, 100, Schema.AssessmentQuestion.COL_QUESTION_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_QUESTION_TITLE, SqlDbType.NVarChar, 0, Schema.AssessmentQuestion.COL_QUESTION_TITLE);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_QUESTION_DESC, SqlDbType.NVarChar, 0, Schema.AssessmentQuestion.COL_QUESTION_DESC);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                            _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                            _sqladapter.UpdateBatchSize = iBatchSize;
                            _sqladapter.Update(dLangTable);
                            _sqladapter.Dispose();
                        }
                        foreach (AssessmentOptions objOption in entQuestion.AssessmentOptions)
                        {
                            if (string.IsNullOrEmpty(objOption.QuestionID))
                                objOption.QuestionID = entQuestion.ID;

                            if (string.IsNullOrEmpty(objOption.AssessmentId))
                                objOption.AssessmentId = entQuestion.AssessmentID;

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
        public List<AssessmentQuestion> AddQuestionWithCopy(List<AssessmentQuestion> pEntListQuestions, SqlConnection pSqlConn, StringDictionary objStrQuestionIdDic)
        {
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            DataTable dLangTable = new DataTable();
            AssessmentOptionsAdaptor optionAdaptor = new AssessmentOptionsAdaptor();
            List<AssessmentOptions> entListOptions = new List<AssessmentOptions>();
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

                    dLangTable.Columns.Add(Schema.AssessmentQuestion.COL_QUESTION_ID);
                    dLangTable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    dLangTable.Columns.Add(Schema.AssessmentQuestion.COL_QUESTION_TITLE);
                    dLangTable.Columns.Add(Schema.AssessmentQuestion.COL_QUESTION_DESC);
                    dLangTable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    dLangTable.Columns.Add(Schema.Common.COL_UPDATE_MODE);


                    foreach (AssessmentQuestion entQuestion in pEntListQuestions)
                    {
                        /*code change for duplicate question issue while copying records. By:rajendra on 16-apr-2010. */
                        _dtable.Rows.Clear();
                        dLangTable.Rows.Clear();

                        DataRow drow = _dtable.NewRow();
                        DataRow dLangRow = dLangTable.NewRow();
                        if (!String.IsNullOrEmpty(entQuestion.ID))
                            drow[Schema.AssessmentQuestion.COL_QUESTION_ID] = entQuestion.ID;
                        else
                        {
                            entQuestion.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                            drow[Schema.AssessmentQuestion.COL_QUESTION_ID] = entQuestion.ID;
                        }
                        drow[Schema.AssessmentQuestion.COL_ASSESSMENT_ID] = entQuestion.AssessmentID;
                        drow[Schema.AssessmentQuestion.COL_SECTION_ID] = entQuestion.SectionID;
                        drow[Schema.AssessmentQuestion.COL_QUESTION_TYPE] = entQuestion.QuestionType;
                        drow[Schema.AssessmentQuestion.COL_SEQUENCE_ORDER] = entQuestion.SequenceOrder;
                        drow[Schema.AssessmentQuestion.COL_IS_ACTIVE] = entQuestion.IsActive;
                        drow[Schema.Common.COL_MODIFIED_BY] = entQuestion.LastModifiedById;
                        drow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_INSERT_MODE;

                        dLangRow[Schema.AssessmentQuestion.COL_QUESTION_ID] = entQuestion.ID;
                        dLangRow[Schema.Language.COL_LANGUAGE_ID] = entQuestion.LanguageId;
                        dLangRow[Schema.AssessmentQuestion.COL_QUESTION_TITLE] = entQuestion.QuestionTitle;
                        dLangRow[Schema.AssessmentQuestion.COL_QUESTION_DESC] = entQuestion.QuestionDescription;
                        dLangRow[Schema.Common.COL_MODIFIED_BY] = entQuestion.LastModifiedById;
                        dLangRow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_INSERT_MODE;

                        _dtable.Rows.Add(drow);
                        dLangTable.Rows.Add(dLangRow);

                        iBatchSize = 1;

                        if (_dtable.Rows.Count > 0)
                        {
                            _sqlcmd = new SqlCommand();
                            _sqlcmd.CommandText = Schema.AssessmentQuestion.PROC_UPDATE_ASSESSMENT_QUESTION;
                            _sqlcmd.CommandType = CommandType.StoredProcedure;
                            _sqlcmd.Connection = pSqlConn;
                            _sqladapter.InsertCommand = _sqlcmd;
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_QUESTION_ID, SqlDbType.VarChar, 100, Schema.AssessmentQuestion.COL_QUESTION_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_ASSESSMENT_ID, SqlDbType.VarChar, 100, Schema.AssessmentQuestion.COL_ASSESSMENT_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.AssessmentQuestion.COL_SECTION_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_QUESTION_TYPE, SqlDbType.VarChar, 100, Schema.AssessmentQuestion.COL_QUESTION_TYPE);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_SEQUENCE_ORDER, SqlDbType.Int, 10, Schema.AssessmentQuestion.COL_SEQUENCE_ORDER);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_IS_ACTIVE, SqlDbType.Bit, 1, Schema.AssessmentQuestion.COL_IS_ACTIVE);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                            _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                            _sqladapter.UpdateBatchSize = iBatchSize;
                            _sqladapter.Update(_dtable);
                            _sqladapter.Dispose();

                            _sqladapter = new SqlDataAdapter();
                            _sqlcmd = new SqlCommand();
                            _sqlcmd.CommandText = Schema.AssessmentQuestion.PROC_UPDATE_ASSESSMENT_QUEST_LANGUAGE;
                            _sqlcmd.CommandType = CommandType.StoredProcedure;
                            _sqlcmd.Connection = pSqlConn;
                            _sqladapter.InsertCommand = _sqlcmd;
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_QUESTION_ID, SqlDbType.VarChar, 100, Schema.AssessmentQuestion.COL_QUESTION_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_QUESTION_TITLE, SqlDbType.NVarChar, 0, Schema.AssessmentQuestion.COL_QUESTION_TITLE);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_QUESTION_DESC, SqlDbType.NVarChar, 0, Schema.AssessmentQuestion.COL_QUESTION_DESC);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                            _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                            _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                            _sqladapter.UpdateBatchSize = iBatchSize;
                            _sqladapter.Update(dLangTable);
                            _sqladapter.Dispose();
                        }
                        entListOptions.Clear();

                        foreach (AssessmentOptions objOption in entQuestion.AssessmentOptions)
                        {
                            if (string.IsNullOrEmpty(objOption.QuestionID))
                                objOption.QuestionID = entQuestion.ID;

                            if (string.IsNullOrEmpty(objOption.AssessmentId))
                                objOption.AssessmentId = entQuestion.AssessmentID;

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
        public List<AssessmentQuestion> SaveImportQuestionLanguages(List<AssessmentQuestion> pEntListQuestions, SqlConnection pSqlConn)
        {
            _sqladapter = new SqlDataAdapter();
            DataTable dLangTable = new DataTable();
            QuestionOptionsAdaptor optionAdaptor = new QuestionOptionsAdaptor();
            List<AssessmentQuestion> entListQuestions = new List<AssessmentQuestion>();
            int iBatchSize = 0;
            try
            {
                if (pEntListQuestions.Count > 0)
                {
                    dLangTable.Columns.Add(Schema.AssessmentQuestion.COL_QUESTION_ID);
                    dLangTable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    dLangTable.Columns.Add(Schema.AssessmentQuestion.COL_QUESTION_TITLE);
                    dLangTable.Columns.Add(Schema.AssessmentQuestion.COL_QUESTION_DESC);
                    dLangTable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    dLangTable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (AssessmentQuestion objQuestion in pEntListQuestions)
                    {
                        DataRow dLangRow = dLangTable.NewRow();

                        dLangRow[Schema.AssessmentQuestion.COL_QUESTION_ID] = objQuestion.ID;
                        dLangRow[Schema.Language.COL_LANGUAGE_ID] = objQuestion.LanguageId;
                        dLangRow[Schema.AssessmentQuestion.COL_QUESTION_TITLE] = objQuestion.QuestionTitle;
                        dLangRow[Schema.AssessmentQuestion.COL_QUESTION_DESC] = objQuestion.QuestionDescription;
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
                        _sqlcmd.CommandText = Schema.AssessmentQuestion.PROC_UPDATE_ASSESSMENT_QUEST_LANGUAGE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcmd.Connection = pSqlConn;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_QUESTION_ID, SqlDbType.VarChar, 100, Schema.AssessmentQuestion.COL_QUESTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_QUESTION_TITLE, SqlDbType.NVarChar, 0, Schema.AssessmentQuestion.COL_QUESTION_TITLE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentQuestion.PARA_QUESTION_DESC, SqlDbType.NVarChar, 0, Schema.AssessmentQuestion.COL_QUESTION_DESC);
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
        /// <param name="pEntAssessmentQuestion"></param>
        /// <returns></returns>
        public AssessmentQuestion UpdateSequence(AssessmentQuestion pEntAssessmentQuestion)
        {
            int iRows = 0;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.AssessmentQuestion.PROC_UPDATE_SEQUENCE;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessmentQuestion.ClientId);

                if (!string.IsNullOrEmpty(pEntAssessmentQuestion.AssessmentID))
                {
                    _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessmentQuestion.AssessmentID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntAssessmentQuestion.SectionID))
                {
                    _sqlpara = new SqlParameter(Schema.AssessmentQuestion.PARA_SECTION_ID, pEntAssessmentQuestion.SectionID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.AssessmentQuestion.PARA_SECTION_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (Convert.ToString(pEntAssessmentQuestion.SequenceOrder) != string.Empty)
                {
                    _sqlpara = new SqlParameter(Schema.AssessmentQuestion.PARA_SEQUENCE_ORDER, pEntAssessmentQuestion.SequenceOrder);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.AssessmentQuestion.PARA_SEQUENCE_ORDER, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlpara = new SqlParameter(Schema.AssessmentQuestion.PARA_IS_UP, pEntAssessmentQuestion.IsMoveUp);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntAssessmentQuestion.ID))
                {
                    _sqlpara = new SqlParameter(Schema.AssessmentQuestion.PARA_QUESTION_ID, pEntAssessmentQuestion.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.AssessmentQuestion.PARA_QUESTION_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntAssessmentQuestion.LastModifiedById))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntAssessmentQuestion.LastModifiedById);
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
            return pEntAssessmentQuestion;
        }

        #region Interface Methods
        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="pEntAssessmentQuestion"></param>
        /// <returns>null</returns>
        public AssessmentQuestion Get(AssessmentQuestion pEntAssessmentQuestion)
        {
            return null;
        }
        /// <summary>
        /// Update AssessmentQuestion
        /// </summary>
        /// <param name="pEntAssessmentQuestion"></param>
        /// <returns></returns>
        public AssessmentQuestion Update(AssessmentQuestion pEntAssessmentQuestion)
        {
            return EditQuestion(pEntAssessmentQuestion);
        }
        #endregion
    }
}
