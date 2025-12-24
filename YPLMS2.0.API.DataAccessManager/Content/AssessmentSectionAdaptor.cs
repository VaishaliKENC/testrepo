using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.YPLMS.Services;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public class AssessmentSectionAdaptor : IDataManager<AssessmentSections>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataAdapter _sqladapter = null;
        AssessmentSections _entSection = null;
        List<AssessmentSections> _entListSections = null;
        SqlConnection _sqlcon = null;
        DataSet _dset = null;
        DataTable _dtable = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.GroupRule.RULE_ERROR;
        #endregion

        /// <summary>
        /// Get Section By Id
        /// </summary>
        /// <param name="pEntSection"></param>
        /// <returns></returns>
        public AssessmentSections GetSectionById(AssessmentSections pEntSection)
        {
            _sqlObject = new SQLObject();
            _entSection = new AssessmentSections();
            AssessmentAdaptor questionnairAdaptor;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.AssessmentSections.PROC_GET_SCTION;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntSection.ClientId);

                _sqlpara = new SqlParameter(Schema.AssessmentSections.PARA_SECTION_ID, pEntSection.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntSection.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _dset = new DataSet();
                _dset = _sqlObject.SqlDataAdapter(_sqlcmd, _strConnString);
                if (_dset.Tables.Count > 0 && _dset.Tables[0].Rows.Count > 0)
                {
                    questionnairAdaptor = new AssessmentAdaptor();
                    _entSection = questionnairAdaptor.FillSection(_dset.Tables[0].Rows[0]);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return _entSection;
        }

        /// <summary>
        /// Insert section
        /// </summary>
        /// <param name="pEntSection"></param>
        /// <returns></returns>
        public AssessmentSections AddSection(AssessmentSections pEntSection)
        {
            _entSection = new AssessmentSections();
            _entListSections = new List<AssessmentSections>();
            _entListSections.Add(pEntSection);
            _entListSections = BulkUpdate(_entListSections, Schema.Common.VAL_INSERT_MODE);
            return _entListSections[0];
        }

        /// <summary>
        /// Edit section
        /// </summary>
        /// <param name="pEntSection"></param>
        /// <returns></returns>
        public AssessmentSections EditSection(AssessmentSections pEntSection)
        {
            _entSection = new AssessmentSections();
            _entListSections = new List<AssessmentSections>();
            _entListSections.Add(pEntSection);
            _entListSections = BulkUpdate(_entListSections, Schema.Common.VAL_UPDATE_MODE);
            return _entListSections[0];
        }

        /// <summary>
        /// Bulk add/update
        /// </summary>
        /// <param name="pEntListSections"></param>
        /// <param name="pstrUpdateMode"></param>
        /// <returns></returns>
        public List<AssessmentSections> BulkUpdate(List<AssessmentSections> pEntListSections, string pstrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            DataTable dLangTable = new DataTable();
            List<AssessmentSections> entListSections = new List<AssessmentSections>();
            int iBatchSize = 0;
            try
            {
                if (pEntListSections.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.AssessmentSections.COL_Assessment_ID);
                    _dtable.Columns.Add(Schema.AssessmentSections.COL_SECTION_ID);
                    _dtable.Columns.Add(Schema.AssessmentSections.COL_SEQUENCE_ORDER);
                    _dtable.Columns.Add(Schema.AssessmentSections.COL_IS_ACTIVE);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    dLangTable.Columns.Add(Schema.AssessmentSections.COL_SECTION_ID);
                    dLangTable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    dLangTable.Columns.Add(Schema.AssessmentSections.COL_SECTION_TITLE);
                    dLangTable.Columns.Add(Schema.AssessmentSections.COL_SECTION_NAME);
                    dLangTable.Columns.Add(Schema.AssessmentSections.COL_SECTION_DESC);
                    dLangTable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    dLangTable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (AssessmentSections entSection in pEntListSections)
                    {

                        DataRow drow = _dtable.NewRow();
                        DataRow dLangRow = dLangTable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entSection.ClientId);

                        if (!String.IsNullOrEmpty(entSection.ID))
                            drow[Schema.AssessmentSections.COL_SECTION_ID] = entSection.ID;
                        else
                        {
                            entSection.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                            drow[Schema.AssessmentSections.COL_SECTION_ID] = entSection.ID;
                        }
                        drow[Schema.AssessmentSections.COL_Assessment_ID] = entSection.AssessmentId;
                        drow[Schema.AssessmentSections.COL_SEQUENCE_ORDER] = entSection.SequenceOrder;
                        drow[Schema.AssessmentSections.COL_IS_ACTIVE] = entSection.IsActive;
                        drow[Schema.Common.COL_MODIFIED_BY] = entSection.LastModifiedById;
                        drow[Schema.Common.COL_UPDATE_MODE] = pstrUpdateMode;

                        dLangRow[Schema.AssessmentSections.COL_SECTION_ID] = entSection.ID;
                        dLangRow[Schema.Language.COL_LANGUAGE_ID] = entSection.LanguageId;
                        dLangRow[Schema.AssessmentSections.COL_SECTION_TITLE] = entSection.SectionTitle;
                        dLangRow[Schema.AssessmentSections.COL_SECTION_NAME] = entSection.SectionName;
                        dLangRow[Schema.AssessmentSections.COL_SECTION_DESC] = entSection.SectionDescription;
                        dLangRow[Schema.Common.COL_MODIFIED_BY] = entSection.LastModifiedById;
                        dLangRow[Schema.Common.COL_UPDATE_MODE] = pstrUpdateMode;

                        _dtable.Rows.Add(drow);
                        dLangTable.Rows.Add(dLangRow);
                        iBatchSize = iBatchSize + 1;
                        entListSections.Add(entSection);
                    }
                    if (_dtable.Rows.Count > 0)
                    {
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.AssessmentSections.PROC_UPDATE_QUESTNN_SCTION;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.AssessmentSections.COL_SECTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_Assessment_ID, SqlDbType.VarChar, 100, Schema.AssessmentSections.COL_Assessment_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_SEQUENCE_ORDER, SqlDbType.Int, 10, Schema.AssessmentSections.COL_SEQUENCE_ORDER);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_IS_ACTIVE, SqlDbType.Bit, 1, Schema.AssessmentSections.COL_IS_ACTIVE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();

                        _sqladapter = new SqlDataAdapter();
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.AssessmentSections.PROC_UPDATE_QUESTNN_SCT_LANGUAGE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.AssessmentSections.COL_SECTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_SECTION_TITLE, SqlDbType.NVarChar, 0, Schema.AssessmentSections.COL_SECTION_TITLE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_SECTION_NAME, SqlDbType.NVarChar, 0, Schema.AssessmentSections.COL_SECTION_NAME);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_SECTION_DESC, SqlDbType.NVarChar, 0, Schema.AssessmentSections.COL_SECTION_DESC);
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
            return entListSections;
        }

        /// <summary>
        /// Add Section With Copy
        /// </summary>
        /// <param name="pEntListSections"></param>
        /// <param name="pSqlConn"></param>
        /// <returns></returns>
        public List<AssessmentSections> AddSectionWithCopy(List<AssessmentSections> pEntListSections, SqlConnection pSqlConn)
        {
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            DataTable dLangTable = new DataTable();
            List<AssessmentSections> entListSections = new List<AssessmentSections>();
            int iBatchSize = 0;
            try
            {
                if (pEntListSections.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.AssessmentSections.COL_Assessment_ID);
                    _dtable.Columns.Add(Schema.AssessmentSections.COL_SECTION_ID);
                    _dtable.Columns.Add(Schema.AssessmentSections.COL_SEQUENCE_ORDER);
                    _dtable.Columns.Add(Schema.AssessmentSections.COL_IS_ACTIVE);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    dLangTable.Columns.Add(Schema.AssessmentSections.COL_SECTION_ID);
                    dLangTable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    dLangTable.Columns.Add(Schema.AssessmentSections.COL_SECTION_TITLE);
                    dLangTable.Columns.Add(Schema.AssessmentSections.COL_SECTION_NAME);
                    dLangTable.Columns.Add(Schema.AssessmentSections.COL_SECTION_DESC);
                    dLangTable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    dLangTable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (AssessmentSections entSection in pEntListSections)
                    {

                        DataRow drow = _dtable.NewRow();
                        DataRow dLangRow = dLangTable.NewRow();

                        drow[Schema.AssessmentSections.COL_SECTION_ID] = entSection.ID;
                        drow[Schema.AssessmentSections.COL_Assessment_ID] = entSection.AssessmentId;
                        drow[Schema.AssessmentSections.COL_SEQUENCE_ORDER] = entSection.SequenceOrder;
                        drow[Schema.AssessmentSections.COL_IS_ACTIVE] = entSection.IsActive;
                        drow[Schema.Common.COL_MODIFIED_BY] = entSection.LastModifiedById;
                        drow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_INSERT_MODE;

                        dLangRow[Schema.AssessmentSections.COL_SECTION_ID] = entSection.ID;
                        dLangRow[Schema.Language.COL_LANGUAGE_ID] = entSection.LanguageId;
                        dLangRow[Schema.AssessmentSections.COL_SECTION_TITLE] = entSection.SectionTitle;
                        dLangRow[Schema.AssessmentSections.COL_SECTION_NAME] = entSection.SectionName;
                        dLangRow[Schema.AssessmentSections.COL_SECTION_DESC] = entSection.SectionDescription;
                        dLangRow[Schema.Common.COL_MODIFIED_BY] = entSection.LastModifiedById;
                        dLangRow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_INSERT_MODE;

                        _dtable.Rows.Add(drow);
                        dLangTable.Rows.Add(dLangRow);
                        iBatchSize = iBatchSize + 1;
                        entListSections.Add(entSection);
                    }

                    if (_dtable.Rows.Count > 0)
                    {
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.AssessmentSections.PROC_UPDATE_QUESTNN_SCTION;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcmd.Connection = pSqlConn;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.AssessmentSections.COL_SECTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_Assessment_ID, SqlDbType.VarChar, 100, Schema.AssessmentSections.COL_Assessment_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_SEQUENCE_ORDER, SqlDbType.Int, 10, Schema.AssessmentSections.COL_SEQUENCE_ORDER);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_IS_ACTIVE, SqlDbType.Bit, 1, Schema.AssessmentSections.COL_IS_ACTIVE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();

                        _sqladapter = new SqlDataAdapter();
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.AssessmentSections.PROC_UPDATE_QUESTNN_SCT_LANGUAGE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcmd.Connection = pSqlConn;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.AssessmentSections.COL_SECTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_SECTION_TITLE, SqlDbType.NVarChar, 0, Schema.AssessmentSections.COL_SECTION_TITLE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_SECTION_NAME, SqlDbType.NVarChar, 0, Schema.AssessmentSections.COL_SECTION_NAME);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_SECTION_DESC, SqlDbType.NVarChar, 0, Schema.AssessmentSections.COL_SECTION_DESC);
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
            return entListSections;
        }

        /// <summary>
        /// Save Import Section Languages
        /// </summary>
        /// <param name="pEntListSections"></param>
        /// <param name="pSqlConn"></param>
        /// <returns></returns>
        public List<AssessmentSections> SaveImportSectionLanguages(List<AssessmentSections> pEntListSections, SqlConnection pSqlConn)
        {
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            DataTable dLangTable = new DataTable();
            List<AssessmentSections> entListSections = new List<AssessmentSections>();
            int iBatchSize = 0;
            try
            {
                if (pEntListSections.Count > 0)
                {
                    dLangTable.Columns.Add(Schema.AssessmentSections.COL_SECTION_ID);
                    dLangTable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    dLangTable.Columns.Add(Schema.AssessmentSections.COL_SECTION_TITLE);
                    dLangTable.Columns.Add(Schema.AssessmentSections.COL_SECTION_NAME);
                    dLangTable.Columns.Add(Schema.AssessmentSections.COL_SECTION_DESC);
                    dLangTable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    dLangTable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (AssessmentSections objSection in pEntListSections)
                    {

                        DataRow dLangRow = dLangTable.NewRow();
                        dLangRow[Schema.AssessmentSections.COL_SECTION_ID] = objSection.ID;
                        dLangRow[Schema.Language.COL_LANGUAGE_ID] = objSection.LanguageId;
                        dLangRow[Schema.AssessmentSections.COL_SECTION_TITLE] = objSection.SectionTitle;
                        dLangRow[Schema.AssessmentSections.COL_SECTION_NAME] = objSection.SectionName;
                        dLangRow[Schema.AssessmentSections.COL_SECTION_DESC] = objSection.SectionDescription;
                        dLangRow[Schema.Common.COL_MODIFIED_BY] = objSection.LastModifiedById;
                        dLangRow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_UPDATE_MODE;

                        dLangTable.Rows.Add(dLangRow);

                        iBatchSize = iBatchSize + 1;
                        entListSections.Add(objSection);
                    }

                    if (dLangTable.Rows.Count > 0)
                    {
                        _sqladapter = new SqlDataAdapter();
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.AssessmentSections.PROC_UPDATE_QUESTNN_SCT_LANGUAGE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcmd.Connection = pSqlConn;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.AssessmentSections.COL_SECTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_SECTION_TITLE, SqlDbType.NVarChar, 0, Schema.AssessmentSections.COL_SECTION_TITLE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_SECTION_NAME, SqlDbType.NVarChar, 0, Schema.AssessmentSections.COL_SECTION_NAME);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.AssessmentSections.PARA_SECTION_DESC, SqlDbType.NVarChar, 0, Schema.AssessmentSections.COL_SECTION_DESC);
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
            return entListSections;
        }

        /// <summary>
        /// Delete Section
        /// </summary>
        /// <param name="pEntSection"></param>
        /// <returns></returns>
        public AssessmentSections DeleteSection(AssessmentSections pEntSection)
        {
            _sqlObject = new SQLObject();
            AssessmentSections entSection = new AssessmentSections();
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.AssessmentSections.PROC_DELETE_SCTION;
            _sqlpara = new SqlParameter(Schema.AssessmentSections.PARA_Assessment_ID, pEntSection.AssessmentId);
            _sqlcmd.Parameters.Add(_sqlpara);
            _sqlpara = new SqlParameter(Schema.AssessmentSections.PARA_SECTION_ID, pEntSection.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntSection.ClientId);
                //Delete Assessment from database
                int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                pEntSection = null;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntSection;
        }

        /// <summary>
        /// Update Sequence
        /// </summary>
        /// <param name="pEntAssessmentSections"></param>
        /// <returns></returns>
        public AssessmentSections UpdateSequence(AssessmentSections pEntAssessmentSections)
        {
            int iRows = 0;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.AssessmentSections.PROC_UPDATE_SEQUENCE;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssessmentSections.ClientId);

                if (!string.IsNullOrEmpty(pEntAssessmentSections.AssessmentId))
                {
                    _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, pEntAssessmentSections.AssessmentId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Assessment.PARA_ASSESSMENT_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntAssessmentSections.ID))
                {
                    _sqlpara = new SqlParameter(Schema.AssessmentSections.PARA_SECTION_ID, pEntAssessmentSections.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.AssessmentSections.PARA_SECTION_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (Convert.ToString(pEntAssessmentSections.SequenceOrder) != string.Empty)
                {
                    _sqlpara = new SqlParameter(Schema.AssessmentSections.PARA_SEQUENCE_ORDER, pEntAssessmentSections.SequenceOrder);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.AssessmentSections.PARA_SEQUENCE_ORDER, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlpara = new SqlParameter(Schema.AssessmentQuestion.PARA_IS_UP, pEntAssessmentSections.IsMoveUp);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntAssessmentSections.LastModifiedById))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntAssessmentSections.LastModifiedById);
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
            return pEntAssessmentSections;
        }
        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntAssessmentSections"></param>
        /// <returns></returns>
        public AssessmentSections Get(AssessmentSections pEntAssessmentSections)
        {
            return GetSectionById(pEntAssessmentSections);
        }
        /// <summary>
        /// Update AssessmentSections
        /// </summary>
        /// <param name="pEntAssessmentSections"></param>
        /// <returns></returns>
        public AssessmentSections Update(AssessmentSections pEntAssessmentSections)
        {
            return EditSection(pEntAssessmentSections);
        }
        #endregion
    }
}
