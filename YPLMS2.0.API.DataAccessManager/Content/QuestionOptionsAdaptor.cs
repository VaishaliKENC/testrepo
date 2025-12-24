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
    public class QuestionOptionsAdaptor : IDataManager<QuestionOptions>
    {

        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataAdapter _sqladapter = null;
        QuestionOptions _entOption = null;
        SqlConnection _sqlcon = null;
        DataTable _dtable = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.GroupRule.RULE_ERROR;
        #endregion

        /// <summary>
        /// Update Options
        /// </summary>
        /// <param name="pEntListOptions"></param>
        /// <returns></returns>
        public List<QuestionOptions> UpdateOptions(List<QuestionOptions> pEntListOptions)
        {
            _sqlObject = new SQLObject();
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            DataTable dLangTable = new DataTable();
            List<QuestionOptions> entListOptions = new List<QuestionOptions>();
            int iBatchSize = 0;
            string strQuestionId = string.Empty;
            string strQuestionnairId = string.Empty;
            string strSectionId = string.Empty;
            try
            {
                if (pEntListOptions.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_GO_TO_QUESTION);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_IS_ALERT);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_IS_EXPLANATION);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);

                    foreach (QuestionOptions entOption in pEntListOptions)
                    {


                        DataRow dLangRow = dLangTable.NewRow();
                        DataRow drow = _dtable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entOption.ClientId);

                        if (!String.IsNullOrEmpty(entOption.ID))
                        {
                            drow[Schema.QuestionnaireOptions.COL_OPTION_ID] = entOption.ID;
                            drow[Schema.QuestionnaireOptions.COL_IS_ALERT] = entOption.IsAlert;
                            drow[Schema.QuestionnaireOptions.COL_IS_EXPLANATION] = entOption.IsExplanation;
                            drow[Schema.QuestionnaireOptions.COL_GO_TO_QUESTION] = entOption.GoToQuestion;
                            drow[Schema.Common.COL_MODIFIED_BY] = entOption.LastModifiedById;
                            iBatchSize = iBatchSize + 1;
                            _dtable.Rows.Add(drow);
                            entListOptions.Add(entOption);
                        }
                    }
                    if (_dtable.Rows.Count > 0)
                    {
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.QuestionnaireOptions.PROC_UPDATE_OPTION_MASTER;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_OPTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_IS_EXPLANATION, SqlDbType.Bit, 1, Schema.QuestionnaireOptions.COL_IS_EXPLANATION);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_GO_TO_QUESTION, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_GO_TO_QUESTION);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_IS_ALERT, SqlDbType.Bit, 1, Schema.QuestionnaireOptions.COL_IS_ALERT);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();
                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListOptions;
        }

        /// <summary>
        /// Add Option
        /// </summary>
        /// <param name="pEntOption"></param>
        /// <returns></returns>
        public QuestionOptions AddOption(QuestionOptions pEntOption)
        {
            List<QuestionOptions> entListOptions = new List<QuestionOptions>();
            List<QuestionOptions> entListOptionsReturn = new List<QuestionOptions>();
            QuestionOptions entOptions = new QuestionOptions();
            try
            {
                entListOptions.Add(pEntOption);
                entListOptionsReturn = BulkImportInsert(entListOptions, Schema.Common.VAL_INSERT_MODE);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {

            }
            if (entListOptionsReturn.Count > 0)
                return entListOptionsReturn[0];
            else return pEntOption;
        }

        /// <summary>
        /// Bulk Update
        /// </summary>
        /// <param name="pEntListOptions"></param>
        /// <param name="pstrUpdateMode"></param>
        /// <returns></returns>
        public List<QuestionOptions> BulkUpdate(List<QuestionOptions> pEntListOptions, string pstrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            DataTable dLangTable = new DataTable();
            List<QuestionOptions> entListOptions = new List<QuestionOptions>();
            int iBatchSize = 0;
            string strQuestionId = string.Empty;
            string strQuestionnairId = string.Empty;
            string strSectionId = string.Empty;
            try
            {
                if (pEntListOptions.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_SECTION_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_QUESTION_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_SEQUENCE_ORDER);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_IS_ACTIVE);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_TYPE);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_GO_TO_QUESTION);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_IS_ALERT);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_IS_LAUNCH_LU);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_IS_EXPLANATION);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_ID);
                    dLangTable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_TITLE);
                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_DESC);
                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID);
                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_EXPLAINATION_TITLE);

                    dLangTable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (QuestionOptions objBase in pEntListOptions)
                    {
                        _entOption = new QuestionOptions();
                        _entOption = objBase;

                        DataRow dLangRow = dLangTable.NewRow();
                        DataRow drow = _dtable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(_entOption.ClientId);

                        if (!String.IsNullOrEmpty(_entOption.ID))
                        {
                            drow[Schema.QuestionnaireOptions.COL_OPTION_ID] = _entOption.ID;
                            pstrUpdateMode = Schema.Common.VAL_UPDATE_MODE;
                        }
                        else
                        {
                            _entOption.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                            drow[Schema.QuestionnaireOptions.COL_OPTION_ID] = _entOption.ID;
                            pstrUpdateMode = Schema.Common.VAL_INSERT_MODE;
                        }
                        drow[Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID] = _entOption.QuestionnaireId;
                        drow[Schema.QuestionnaireOptions.COL_SECTION_ID] = _entOption.SectionID;
                        drow[Schema.QuestionnaireOptions.COL_QUESTION_ID] = _entOption.QuestionID;
                        drow[Schema.QuestionnaireOptions.COL_SEQUENCE_ORDER] = _entOption.SequenceOrder;
                        drow[Schema.QuestionnaireOptions.COL_IS_ACTIVE] = _entOption.IsActive;
                        drow[Schema.QuestionnaireOptions.COL_IS_ALERT] = _entOption.IsAlert;
                        drow[Schema.QuestionnaireOptions.COL_IS_EXPLANATION] = _entOption.IsExplanation;
                        drow[Schema.QuestionnaireOptions.COL_OPTION_TYPE] = _entOption.OptionType.ToString();
                        drow[Schema.QuestionnaireOptions.COL_GO_TO_QUESTION] = _entOption.GoToQuestion;
                        drow[Schema.QuestionnaireOptions.COL_IS_LAUNCH_LU] = _entOption.IsLaunchLU;
                        drow[Schema.Common.COL_MODIFIED_BY] = _entOption.LastModifiedById;
                        drow[Schema.Common.COL_UPDATE_MODE] = pstrUpdateMode;
                        strQuestionId = _entOption.QuestionID;
                        strQuestionnairId = _entOption.QuestionnaireId;
                        strSectionId = _entOption.SectionID;
                        _dtable.Rows.Add(drow);

                        dLangRow[Schema.QuestionnaireOptions.COL_OPTION_ID] = _entOption.ID;
                        dLangRow[Schema.Language.COL_LANGUAGE_ID] = _entOption.LanguageId;
                        dLangRow[Schema.QuestionnaireOptions.COL_OPTION_TITLE] = _entOption.OptionTitle;
                        dLangRow[Schema.QuestionnaireOptions.COL_OPTION_DESC] = _entOption.OptionDescription;
                        dLangRow[Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID] = _entOption.QuestionnaireId;
                        dLangRow[Schema.QuestionnaireOptions.COL_EXPLAINATION_TITLE] = _entOption.ExplainationTitle;
                        dLangRow[Schema.Common.COL_UPDATE_MODE] = pstrUpdateMode;
                        dLangTable.Rows.Add(dLangRow);

                        iBatchSize = iBatchSize + 1;
                        entListOptions.Add(_entOption);
                    }
                    if (_dtable.Rows.Count > 0)
                    {
                        // in case of update delete the previous entrys
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.QuestionnaireOptions.PROC_DELETE_OPTIONS;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcon.Open();
                        _sqlcmd.Connection = _sqlcon;
                        _sqlpara = new SqlParameter(Schema.QuestionnaireOptions.PARA_QUESTIONNAIRE_ID, strQuestionnairId);
                        _sqlcmd.Parameters.Add(_sqlpara);
                        _sqlpara = new SqlParameter(Schema.QuestionnaireOptions.PARA_SECTION_ID, strSectionId);
                        _sqlcmd.Parameters.Add(_sqlpara);
                        _sqlpara = new SqlParameter(Schema.QuestionnaireOptions.PARA_QUESTION_ID, strQuestionId);
                        _sqlcmd.Parameters.Add(_sqlpara);
                        _sqlObject.ExecuteNonQuery(_sqlcmd);

                        //To add new option records
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.QuestionnaireOptions.PROC_UPDATE_QUESTNN_OPTION;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_OPTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_QUESTIONNAIRE_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_SECTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_QUESTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_QUESTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_IS_EXPLANATION, SqlDbType.Bit, 1, Schema.QuestionnaireOptions.COL_IS_EXPLANATION);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_GO_TO_QUESTION, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_GO_TO_QUESTION);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_TYPE, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_OPTION_TYPE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_SEQUENCE_ORDER, SqlDbType.Int, 10, Schema.QuestionnaireOptions.COL_SEQUENCE_ORDER);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_IS_ACTIVE, SqlDbType.Bit, 1, Schema.QuestionnaireOptions.COL_IS_ACTIVE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_IS_LAUNCH_LU, SqlDbType.Bit, 1, Schema.QuestionnaireOptions.COL_IS_LAUNCH_LU);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_IS_ALERT, SqlDbType.Bit, 1, Schema.QuestionnaireOptions.COL_IS_ALERT);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();

                        _sqladapter = new SqlDataAdapter();
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.QuestionnaireOptions.PROC_UPDATE_QUESTNN_OPT_LANGUAGE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_OPTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_TITLE, SqlDbType.NVarChar, 0, Schema.QuestionnaireOptions.COL_OPTION_TITLE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_DESC, SqlDbType.NVarChar, 0, Schema.QuestionnaireOptions.COL_OPTION_DESC);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_QUESTIONNAIRE_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_EXPLAINATION_TITLE, SqlDbType.NVarChar, 200, Schema.QuestionnaireOptions.COL_EXPLAINATION_TITLE);
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
            finally
            {
                if (_sqlcon != null && _sqlcon.State != ConnectionState.Closed) _sqlcon.Close();
            }
            return entListOptions;
        }

        /// <summary>
        /// Bulk Import Insert
        /// </summary>
        /// <param name="pEntListOptions"></param>
        /// <param name="pstrUpdateMode"></param>
        /// <returns></returns>
        private List<QuestionOptions> BulkImportInsert(List<QuestionOptions> pEntListOptions, string pstrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            DataTable dLangTable = new DataTable();
            List<QuestionOptions> entListOptions = new List<QuestionOptions>();
            int iBatchSize = 0;
            string strQuestionId = string.Empty;
            string strQuestionnairId = string.Empty;
            string strSectionId = string.Empty;
            try
            {
                if (pEntListOptions.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_SECTION_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_QUESTION_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_SEQUENCE_ORDER);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_IS_ACTIVE);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_TYPE);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_GO_TO_QUESTION);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_IS_ALERT);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_IS_EXPLANATION);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_IS_LAUNCH_LU);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_ID);
                    dLangTable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_TITLE);
                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_DESC);
                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_EXPLAINATION_TITLE);
                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID);
                    dLangTable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (QuestionOptions objBase in pEntListOptions)
                    {
                        _entOption = new QuestionOptions();
                        _entOption = objBase;

                        DataRow dLangRow = dLangTable.NewRow();
                        DataRow drow = _dtable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(_entOption.ClientId);

                        if (!String.IsNullOrEmpty(_entOption.ID))
                        {
                            drow[Schema.QuestionnaireOptions.COL_OPTION_ID] = _entOption.ID;
                            pstrUpdateMode = Schema.Common.VAL_UPDATE_MODE;
                        }
                        else
                        {
                            _entOption.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                            drow[Schema.QuestionnaireOptions.COL_OPTION_ID] = _entOption.ID;
                            pstrUpdateMode = Schema.Common.VAL_INSERT_MODE;
                        }
                        drow[Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID] = _entOption.QuestionnaireId;
                        drow[Schema.QuestionnaireOptions.COL_SECTION_ID] = _entOption.SectionID;
                        drow[Schema.QuestionnaireOptions.COL_QUESTION_ID] = _entOption.QuestionID;
                        drow[Schema.QuestionnaireOptions.COL_SEQUENCE_ORDER] = _entOption.SequenceOrder;
                        drow[Schema.QuestionnaireOptions.COL_IS_ACTIVE] = _entOption.IsActive;
                        drow[Schema.QuestionnaireOptions.COL_IS_ALERT] = _entOption.IsAlert;
                        drow[Schema.QuestionnaireOptions.COL_IS_EXPLANATION] = _entOption.IsExplanation;
                        drow[Schema.QuestionnaireOptions.COL_OPTION_TYPE] = _entOption.OptionType.ToString();
                        drow[Schema.QuestionnaireOptions.COL_GO_TO_QUESTION] = _entOption.GoToQuestion;
                        drow[Schema.QuestionnaireOptions.COL_IS_LAUNCH_LU] = _entOption.IsLaunchLU;

                        drow[Schema.Common.COL_MODIFIED_BY] = _entOption.LastModifiedById;
                        drow[Schema.Common.COL_UPDATE_MODE] = pstrUpdateMode;
                        strQuestionId = _entOption.QuestionID;
                        strQuestionnairId = _entOption.QuestionnaireId;
                        strSectionId = _entOption.SectionID;
                        _dtable.Rows.Add(drow);

                        dLangRow[Schema.QuestionnaireOptions.COL_OPTION_ID] = _entOption.ID;
                        dLangRow[Schema.Language.COL_LANGUAGE_ID] = _entOption.LanguageId;
                        dLangRow[Schema.QuestionnaireOptions.COL_OPTION_TITLE] = _entOption.OptionTitle;
                        dLangRow[Schema.QuestionnaireOptions.COL_OPTION_DESC] = _entOption.OptionDescription;
                        dLangRow[Schema.QuestionnaireOptions.COL_EXPLAINATION_TITLE] = _entOption.ExplainationTitle;
                        dLangRow[Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID] = _entOption.QuestionnaireId;
                        dLangRow[Schema.Common.COL_UPDATE_MODE] = pstrUpdateMode;
                        dLangTable.Rows.Add(dLangRow);

                        iBatchSize = iBatchSize + 1;
                        entListOptions.Add(_entOption);
                    }

                    if (_dtable.Rows.Count > 0)
                    {
                        //To add new option records
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.QuestionnaireOptions.PROC_UPDATE_QUESTNN_OPTION;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_OPTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_QUESTIONNAIRE_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_SECTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_QUESTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_QUESTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_IS_EXPLANATION, SqlDbType.Bit, 1, Schema.QuestionnaireOptions.COL_IS_EXPLANATION);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_GO_TO_QUESTION, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_GO_TO_QUESTION);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_TYPE, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_OPTION_TYPE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_SEQUENCE_ORDER, SqlDbType.Int, 10, Schema.QuestionnaireOptions.COL_SEQUENCE_ORDER);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_IS_ACTIVE, SqlDbType.Bit, 1, Schema.QuestionnaireOptions.COL_IS_ACTIVE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_IS_ALERT, SqlDbType.Bit, 1, Schema.QuestionnaireOptions.COL_IS_ALERT);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_IS_LAUNCH_LU, SqlDbType.Bit, 1, Schema.QuestionnaireOptions.COL_IS_LAUNCH_LU);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();

                        _sqladapter = new SqlDataAdapter();
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.QuestionnaireOptions.PROC_UPDATE_QUESTNN_OPT_LANGUAGE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_OPTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_TITLE, SqlDbType.NVarChar, 0, Schema.QuestionnaireOptions.COL_OPTION_TITLE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_DESC, SqlDbType.NVarChar, 0, Schema.QuestionnaireOptions.COL_OPTION_DESC);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_QUESTIONNAIRE_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_EXPLAINATION_TITLE, SqlDbType.NVarChar, 100, Schema.QuestionnaireOptions.COL_EXPLAINATION_TITLE);
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
            finally
            {
                if (_sqlcon != null && _sqlcon.State != ConnectionState.Closed) _sqlcon.Close();
            }
            return entListOptions;
        }

        /// <summary>
        /// Add Options With Copy
        /// </summary>
        /// <param name="pEntListOptions"></param>
        /// <param name="pSqlConn"></param>
        /// <returns></returns>
        public List<QuestionOptions> AddOptionsWithCopy(List<QuestionOptions> pEntListOptions, SqlConnection pSqlConn, StringDictionary objStrQuestionIdDic)
        {
            _sqlObject = new SQLObject();
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            DataTable dLangTable = new DataTable();
            List<QuestionOptions> entListOptions = new List<QuestionOptions>();
            int iBatchSize = 0;
            string strQuestionId = string.Empty;
            string strQuestionnairId = string.Empty;
            string strSectionId = string.Empty;
            try
            {
                if (pEntListOptions.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_SECTION_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_QUESTION_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_SEQUENCE_ORDER);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_IS_ACTIVE);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_TYPE);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_GO_TO_QUESTION);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_IS_ALERT);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_IS_EXPLANATION);
                    _dtable.Columns.Add(Schema.QuestionnaireOptions.COL_IS_LAUNCH_LU);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_ID);
                    dLangTable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_TITLE);
                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_DESC);
                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_EXPLAINATION_TITLE);
                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID);
                    dLangTable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (QuestionOptions objBase in pEntListOptions)
                    {
                        _entOption = new QuestionOptions();
                        _entOption = objBase;

                        DataRow dLangRow = dLangTable.NewRow();
                        DataRow drow = _dtable.NewRow();

                        drow[Schema.QuestionnaireOptions.COL_OPTION_ID] = _entOption.ID;
                        drow[Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID] = _entOption.QuestionnaireId;
                        drow[Schema.QuestionnaireOptions.COL_SECTION_ID] = _entOption.SectionID;
                        drow[Schema.QuestionnaireOptions.COL_QUESTION_ID] = _entOption.QuestionID;
                        drow[Schema.QuestionnaireOptions.COL_SEQUENCE_ORDER] = _entOption.SequenceOrder;
                        drow[Schema.QuestionnaireOptions.COL_IS_ACTIVE] = _entOption.IsActive;
                        drow[Schema.QuestionnaireOptions.COL_IS_ALERT] = _entOption.IsAlert;
                        drow[Schema.QuestionnaireOptions.COL_IS_EXPLANATION] = _entOption.IsExplanation;
                        drow[Schema.QuestionnaireOptions.COL_OPTION_TYPE] = _entOption.OptionType.ToString();
                        drow[Schema.QuestionnaireOptions.COL_IS_LAUNCH_LU] = _entOption.IsLaunchLU;

                        bool IsGoTo = false;
                        foreach (System.Collections.DictionaryEntry item in objStrQuestionIdDic)
                        {
                            if (Convert.ToString(item.Key).ToLower().Trim() == _entOption.GoToQuestion.ToLower().Trim())
                            {
                                _entOption.GoToQuestion = Convert.ToString(item.Value);
                                IsGoTo = true;
                                break;
                            }

                        }
                        if (!IsGoTo)
                        {
                            if (_entOption.GoToQuestion.Trim() == "END")
                            {
                                drow[Schema.QuestionnaireOptions.COL_GO_TO_QUESTION] = "END";
                            }
                            else
                            {
                                drow[Schema.QuestionnaireOptions.COL_GO_TO_QUESTION] = null;
                            }
                        }
                        else
                            drow[Schema.QuestionnaireOptions.COL_GO_TO_QUESTION] = _entOption.GoToQuestion;

                        drow[Schema.Common.COL_MODIFIED_BY] = _entOption.LastModifiedById;
                        drow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_INSERT_MODE;
                        strQuestionId = _entOption.QuestionID;
                        strQuestionnairId = _entOption.QuestionnaireId;
                        strSectionId = _entOption.SectionID;
                        _dtable.Rows.Add(drow);

                        dLangRow[Schema.QuestionnaireOptions.COL_OPTION_ID] = _entOption.ID;
                        dLangRow[Schema.Language.COL_LANGUAGE_ID] = _entOption.LanguageId;
                        dLangRow[Schema.QuestionnaireOptions.COL_OPTION_TITLE] = _entOption.OptionTitle;
                        dLangRow[Schema.QuestionnaireOptions.COL_OPTION_DESC] = _entOption.OptionDescription;
                        dLangRow[Schema.QuestionnaireOptions.COL_EXPLAINATION_TITLE] = _entOption.ExplainationTitle;
                        dLangRow[Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID] = _entOption.QuestionnaireId;
                        dLangRow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_INSERT_MODE;
                        dLangTable.Rows.Add(dLangRow);

                        iBatchSize = iBatchSize + 1;
                        entListOptions.Add(_entOption);
                    }
                    if (_dtable.Rows.Count > 0)
                    {
                        // in case of update delete the previous entrys
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.QuestionnaireOptions.PROC_DELETE_OPTIONS;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcmd.Connection = pSqlConn;
                        _sqlpara = new SqlParameter(Schema.QuestionnaireOptions.PARA_QUESTIONNAIRE_ID, strQuestionnairId);
                        _sqlcmd.Parameters.Add(_sqlpara);
                        _sqlpara = new SqlParameter(Schema.QuestionnaireOptions.PARA_SECTION_ID, strSectionId);
                        _sqlcmd.Parameters.Add(_sqlpara);
                        _sqlpara = new SqlParameter(Schema.QuestionnaireOptions.PARA_QUESTION_ID, strQuestionId);
                        _sqlcmd.Parameters.Add(_sqlpara);
                        _sqlObject.ExecuteNonQuery(_sqlcmd);

                        //To add new option records
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.QuestionnaireOptions.PROC_UPDATE_QUESTNN_OPTION;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcmd.Connection = pSqlConn;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_OPTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_QUESTIONNAIRE_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_SECTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_QUESTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_QUESTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_IS_EXPLANATION, SqlDbType.Bit, 1, Schema.QuestionnaireOptions.COL_IS_EXPLANATION);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_GO_TO_QUESTION, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_GO_TO_QUESTION);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_TYPE, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_OPTION_TYPE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_SEQUENCE_ORDER, SqlDbType.Int, 10, Schema.QuestionnaireOptions.COL_SEQUENCE_ORDER);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_IS_ACTIVE, SqlDbType.Bit, 1, Schema.QuestionnaireOptions.COL_IS_ACTIVE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_IS_ALERT, SqlDbType.Bit, 1, Schema.QuestionnaireOptions.COL_IS_ALERT);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_IS_LAUNCH_LU, SqlDbType.Bit, 1, Schema.QuestionnaireOptions.COL_IS_LAUNCH_LU);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();

                        _sqladapter = new SqlDataAdapter();
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.QuestionnaireOptions.PROC_UPDATE_QUESTNN_OPT_LANGUAGE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcmd.Connection = pSqlConn;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_OPTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_TITLE, SqlDbType.NVarChar, 0, Schema.QuestionnaireOptions.COL_OPTION_TITLE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_DESC, SqlDbType.NVarChar, 0, Schema.QuestionnaireOptions.COL_OPTION_DESC);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_QUESTIONNAIRE_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_EXPLAINATION_TITLE, SqlDbType.NVarChar, 100, Schema.QuestionnaireOptions.COL_EXPLAINATION_TITLE);
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
            return entListOptions;
        }

        /// <summary>
        /// Save Import Option Languages
        /// </summary>
        /// <param name="pEntListOptions"></param>
        /// <param name="pSqlConn"></param>
        /// <returns></returns>
        public List<QuestionOptions> SaveImportOptionLanguages(List<QuestionOptions> pEntListOptions, SqlConnection pSqlConn)
        {
            _sqladapter = new SqlDataAdapter();
            DataTable dLangTable = new DataTable();
            List<QuestionOptions> entListOptions = new List<QuestionOptions>();
            int iBatchSize = 0;
            try
            {
                if (pEntListOptions.Count > 0)
                {
                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_ID);
                    dLangTable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_TITLE);
                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_OPTION_DESC);
                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_EXPLAINATION_TITLE);
                    dLangTable.Columns.Add(Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID);
                    dLangTable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (QuestionOptions objOption in pEntListOptions)
                    {
                        DataRow dLangRow = dLangTable.NewRow();

                        dLangRow[Schema.QuestionnaireOptions.COL_OPTION_ID] = objOption.ID;
                        dLangRow[Schema.Language.COL_LANGUAGE_ID] = objOption.LanguageId;
                        dLangRow[Schema.QuestionnaireOptions.COL_OPTION_TITLE] = objOption.OptionTitle;
                        dLangRow[Schema.QuestionnaireOptions.COL_OPTION_DESC] = objOption.OptionDescription;
                        dLangRow[Schema.QuestionnaireOptions.COL_EXPLAINATION_TITLE] = objOption.ExplainationTitle;
                        dLangRow[Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID] = objOption.QuestionnaireId;
                        dLangRow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_UPDATE_MODE;
                        dLangTable.Rows.Add(dLangRow);

                        iBatchSize = iBatchSize + 1;
                        entListOptions.Add(objOption);
                    }
                    if (dLangTable.Rows.Count > 0)
                    {
                        _sqladapter = new SqlDataAdapter();
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.QuestionnaireOptions.PROC_UPDATE_QUESTNN_OPT_LANGUAGE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcmd.Connection = pSqlConn;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_OPTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_TITLE, SqlDbType.NVarChar, 0, Schema.QuestionnaireOptions.COL_OPTION_TITLE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_OPTION_DESC, SqlDbType.NVarChar, 0, Schema.QuestionnaireOptions.COL_OPTION_DESC);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_QUESTIONNAIRE_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireOptions.COL_QUESTIONNAIRE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireOptions.PARA_EXPLAINATION_TITLE, SqlDbType.NVarChar, 100, Schema.QuestionnaireOptions.COL_EXPLAINATION_TITLE);
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
            return entListOptions;
        }

        #region Interface Methods
        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="pEntQuestionOptions"></param>
        /// <returns>null</returns>
        public QuestionOptions Get(QuestionOptions pEntQuestionOptions)
        {
            return null;
        }
        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="pEntQuestionOptions"></param>
        /// <returns>null</returns>
        public QuestionOptions Update(QuestionOptions pEntQuestionOptions)
        {
            return null;
        }
        #endregion
    }
}
