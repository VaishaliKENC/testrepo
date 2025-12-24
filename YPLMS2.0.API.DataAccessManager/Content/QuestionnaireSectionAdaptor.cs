using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using System.Data;
using System.Data.SqlClient;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    public class QuestionnaireSectionAdaptor : IDataManager<QuestionnaireSections>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataAdapter _sqladapter = null;
        QuestionnaireSections _entSection = null;
        List<QuestionnaireSections> _entListSections = null;
        SqlConnection _sqlcon = null;
        DataSet _dset = null;
        DataTable _dtable = null;
        SQLObject _sqlObject = null;
        SqlDataReader _sqlreader = null;
        string _strMessageId = YPLMS.Services.Messages.GroupRule.RULE_ERROR;
        EntityRange _entRange = null;
        #endregion

        /// <summary>
        /// Get Section By Id
        /// </summary>
        /// <param name="pEntSection"></param>
        /// <returns></returns>
        public QuestionnaireSections GetSectionById(QuestionnaireSections pEntSection)
        {
            _sqlObject = new SQLObject();
            _entSection = new QuestionnaireSections();
            QuestionnaireAdaptor questionnairAdaptor;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.QuestionnaireSections.PROC_GET_SCTION;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntSection.ClientId);

                _sqlpara = new SqlParameter(Schema.QuestionnaireSections.PARA_SECTION_ID, pEntSection.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntSection.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _dset = new DataSet();
                _dset = _sqlObject.SqlDataAdapter(_sqlcmd, _strConnString);
                if (_dset.Tables.Count > 0 && _dset.Tables[0].Rows.Count > 0)
                {
                    questionnairAdaptor = new QuestionnaireAdaptor();
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

        public QuestionnaireSections GetSectionByQuestionnaireId(QuestionnaireSections pEntSection)
        {
            _sqlObject = new SQLObject();
            QuestionnaireSections entQuestion = new QuestionnaireSections();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntSection.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                _sqlcmd = new SqlCommand(Schema.QuestionnaireSections.PROC_GET_SECTONS_BY_QUESTIONNAIREID_WISE, sqlConnection);
                _sqlpara = new SqlParameter(Schema.QuestionnaireSections.PARA_QUESTIONNAIRE_ID, pEntSection.QuestionnaireId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntSection.LanguageId);
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


        public List<Language> GetImportLanguages(QuestionnaireSections pEntQuestionnaireSec)
        {
            _sqlObject = new SQLObject();
            List<Language> entListLanguage = new List<Language>();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaireSec.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.QuestionnaireSections.PROC_GET_IMPORT_LANGUAGE;
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.QuestionnaireSections.PARA_SECTION_ID, pEntQuestionnaireSec.ID);
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


        public List<QuestionnaireSections> GetSectionLanguageList(QuestionnaireSections pEntSections)
        {
            _sqlObject = new SQLObject();
            List<QuestionnaireSections> entListSections = new List<QuestionnaireSections>();
            QuestionnaireSections entSections = null;
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.QuestionnaireSections.PROC_GET_SECTIONS_LANGUAGES;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntSections.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.QuestionnaireSections.PARA_SECTION_ID, pEntSections.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                if (pEntSections.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntSections.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntSections.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSections.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entSections = FillObject(_sqlreader, true, _sqlObject);
                    entListSections.Add(entSections);
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
            return entListSections;
        }

        private QuestionnaireSections FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            QuestionnaireSections entSection = new QuestionnaireSections();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.QuestionnaireSections.COL_SECTION_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.QuestionnaireSections.COL_SECTION_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entSection.ID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.QuestionnaireSections.COL_QUESTIONNAIRE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.QuestionnaireSections.COL_QUESTIONNAIRE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entSection.QuestionnaireId = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Language.COL_LANGUAGE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_LANGUAGE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entSection.LanguageId = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Language.COL_LANG_ENG_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_LANG_ENG_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entSection.LanguageName = pSqlReader.GetString(iIndex);
                    else
                        entSection.LanguageName = "English";
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.QuestionnaireSections.COL_SECTION_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.QuestionnaireSections.COL_SECTION_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entSection.SectionName = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.QuestionnaireSections.COL_SECTION_DESC))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.QuestionnaireSections.COL_SECTION_DESC);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entSection.SectionDescription = pSqlReader.GetString(iIndex);
                    else
                        entSection.SectionDescription = " ";
                }



                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.QuestionnaireSections.COL_IS_ACTIVE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.QuestionnaireSections.COL_IS_ACTIVE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entSection.IsActive = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entSection.CreatedById = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entSection.DateCreated = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entSection.LastModifiedById = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entSection.LastModifiedDate = pSqlReader.GetDateTime(iIndex);
                }

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                        if (!pSqlReader.IsDBNull(iIndex))
                            _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                        entSection.ListRange = _entRange;
                    }

                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entSection.CreatedByName = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entSection.ModifiedByName = pSqlReader.GetString(iIndex);
                }

                if (string.IsNullOrEmpty(entSection.CreatedByName))
                    entSection.CreatedByName = "";

                if (string.IsNullOrEmpty(entSection.ModifiedByName))
                    entSection.ModifiedByName = "";


            }
            return entSection;
        }

        /// <summary>
        /// Insert section
        /// </summary>
        /// <param name="pEntSection"></param>
        /// <returns></returns>
        public QuestionnaireSections AddSection(QuestionnaireSections pEntSection)
        {
            _entSection = new QuestionnaireSections();
            _entListSections = new List<QuestionnaireSections>();
            _entListSections.Add(pEntSection);
            _entListSections = BulkUpdate(_entListSections, Schema.Common.VAL_INSERT_MODE);
            return _entListSections[0];
        }

        /// <summary>
        /// Edit section
        /// </summary>
        /// <param name="pEntSection"></param>
        /// <returns></returns>
        public QuestionnaireSections EditSection(QuestionnaireSections pEntSection)
        {
            _entSection = new QuestionnaireSections();
            _entListSections = new List<QuestionnaireSections>();
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
        public List<QuestionnaireSections> BulkUpdate(List<QuestionnaireSections> pEntListSections, string pstrUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            DataTable dLangTable = new DataTable();
            List<QuestionnaireSections> entListSections = new List<QuestionnaireSections>();
            int iBatchSize = 0;
            try
            {
                if (pEntListSections.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.QuestionnaireSections.COL_QUESTIONNAIRE_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireSections.COL_SECTION_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireSections.COL_SEQUENCE_ORDER);
                    _dtable.Columns.Add(Schema.QuestionnaireSections.COL_IS_ACTIVE);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    dLangTable.Columns.Add(Schema.QuestionnaireSections.COL_SECTION_ID);
                    dLangTable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    dLangTable.Columns.Add(Schema.QuestionnaireSections.COL_SECTION_TITLE);
                    dLangTable.Columns.Add(Schema.QuestionnaireSections.COL_SECTION_NAME);
                    dLangTable.Columns.Add(Schema.QuestionnaireSections.COL_SECTION_DESC);
                    dLangTable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    dLangTable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (QuestionnaireSections entSection in pEntListSections)
                    {

                        DataRow drow = _dtable.NewRow();
                        DataRow dLangRow = dLangTable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entSection.ClientId);

                        if (!String.IsNullOrEmpty(entSection.ID))
                            drow[Schema.QuestionnaireSections.COL_SECTION_ID] = entSection.ID;
                        else
                        {
                            entSection.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                            drow[Schema.QuestionnaireSections.COL_SECTION_ID] = entSection.ID;
                        }
                        drow[Schema.QuestionnaireSections.COL_QUESTIONNAIRE_ID] = entSection.QuestionnaireId;
                        drow[Schema.QuestionnaireSections.COL_SEQUENCE_ORDER] = entSection.SequenceOrder;
                        drow[Schema.QuestionnaireSections.COL_IS_ACTIVE] = entSection.IsActive;
                        drow[Schema.Common.COL_MODIFIED_BY] = entSection.LastModifiedById;
                        drow[Schema.Common.COL_UPDATE_MODE] = pstrUpdateMode;

                        dLangRow[Schema.QuestionnaireSections.COL_SECTION_ID] = entSection.ID;
                        dLangRow[Schema.Language.COL_LANGUAGE_ID] = entSection.LanguageId;
                        dLangRow[Schema.QuestionnaireSections.COL_SECTION_TITLE] = entSection.SectionTitle;
                        dLangRow[Schema.QuestionnaireSections.COL_SECTION_NAME] = entSection.SectionName;
                        dLangRow[Schema.QuestionnaireSections.COL_SECTION_DESC] = entSection.SectionDescription;
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
                        _sqlcmd.CommandText = Schema.QuestionnaireSections.PROC_UPDATE_QUESTNN_SCTION;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireSections.COL_SECTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_QUESTIONNAIRE_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireSections.COL_QUESTIONNAIRE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_SEQUENCE_ORDER, SqlDbType.Int, 10, Schema.QuestionnaireSections.COL_SEQUENCE_ORDER);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_IS_ACTIVE, SqlDbType.Bit, 1, Schema.QuestionnaireSections.COL_IS_ACTIVE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();

                        _sqladapter = new SqlDataAdapter();
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.QuestionnaireSections.PROC_UPDATE_QUESTNN_SCT_LANGUAGE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireSections.COL_SECTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_SECTION_TITLE, SqlDbType.NVarChar, 500, Schema.QuestionnaireSections.COL_SECTION_TITLE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_SECTION_NAME, SqlDbType.NVarChar, 4000, Schema.QuestionnaireSections.COL_SECTION_NAME);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_SECTION_DESC, SqlDbType.NVarChar, 4000, Schema.QuestionnaireSections.COL_SECTION_DESC);
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
        public List<QuestionnaireSections> AddSectionWithCopy(List<QuestionnaireSections> pEntListSections, SqlConnection pSqlConn)
        {
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            DataTable dLangTable = new DataTable();
            List<QuestionnaireSections> entListSections = new List<QuestionnaireSections>();
            int iBatchSize = 0;
            try
            {
                if (pEntListSections.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.QuestionnaireSections.COL_QUESTIONNAIRE_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireSections.COL_SECTION_ID);
                    _dtable.Columns.Add(Schema.QuestionnaireSections.COL_SEQUENCE_ORDER);
                    _dtable.Columns.Add(Schema.QuestionnaireSections.COL_IS_ACTIVE);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    dLangTable.Columns.Add(Schema.QuestionnaireSections.COL_SECTION_ID);
                    dLangTable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    dLangTable.Columns.Add(Schema.QuestionnaireSections.COL_SECTION_TITLE);
                    dLangTable.Columns.Add(Schema.QuestionnaireSections.COL_SECTION_NAME);
                    dLangTable.Columns.Add(Schema.QuestionnaireSections.COL_SECTION_DESC);
                    dLangTable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    dLangTable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (QuestionnaireSections entSection in pEntListSections)
                    {

                        DataRow drow = _dtable.NewRow();
                        DataRow dLangRow = dLangTable.NewRow();

                        drow[Schema.QuestionnaireSections.COL_SECTION_ID] = entSection.ID;
                        drow[Schema.QuestionnaireSections.COL_QUESTIONNAIRE_ID] = entSection.QuestionnaireId;
                        drow[Schema.QuestionnaireSections.COL_SEQUENCE_ORDER] = entSection.SequenceOrder;
                        drow[Schema.QuestionnaireSections.COL_IS_ACTIVE] = entSection.IsActive;
                        drow[Schema.Common.COL_MODIFIED_BY] = entSection.LastModifiedById;
                        drow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_INSERT_MODE;

                        dLangRow[Schema.QuestionnaireSections.COL_SECTION_ID] = entSection.ID;
                        dLangRow[Schema.Language.COL_LANGUAGE_ID] = entSection.LanguageId;
                        dLangRow[Schema.QuestionnaireSections.COL_SECTION_TITLE] = entSection.SectionTitle;
                        dLangRow[Schema.QuestionnaireSections.COL_SECTION_NAME] = entSection.SectionName;
                        dLangRow[Schema.QuestionnaireSections.COL_SECTION_DESC] = entSection.SectionDescription;
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
                        _sqlcmd.CommandText = Schema.QuestionnaireSections.PROC_UPDATE_QUESTNN_SCTION;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcmd.Connection = pSqlConn;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireSections.COL_SECTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_QUESTIONNAIRE_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireSections.COL_QUESTIONNAIRE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_SEQUENCE_ORDER, SqlDbType.Int, 10, Schema.QuestionnaireSections.COL_SEQUENCE_ORDER);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_IS_ACTIVE, SqlDbType.Bit, 1, Schema.QuestionnaireSections.COL_IS_ACTIVE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();

                        _sqladapter = new SqlDataAdapter();
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.QuestionnaireSections.PROC_UPDATE_QUESTNN_SCT_LANGUAGE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcmd.Connection = pSqlConn;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireSections.COL_SECTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_SECTION_TITLE, SqlDbType.NVarChar, 500, Schema.QuestionnaireSections.COL_SECTION_TITLE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_SECTION_NAME, SqlDbType.NVarChar, 4000, Schema.QuestionnaireSections.COL_SECTION_NAME);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_SECTION_DESC, SqlDbType.NVarChar, 4000, Schema.QuestionnaireSections.COL_SECTION_DESC);
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
        public List<QuestionnaireSections> SaveImportSectionLanguages(List<QuestionnaireSections> pEntListSections, SqlConnection pSqlConn)
        {
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            DataTable dLangTable = new DataTable();
            List<QuestionnaireSections> entListSections = new List<QuestionnaireSections>();
            int iBatchSize = 0;
            try
            {
                if (pEntListSections.Count > 0)
                {
                    dLangTable.Columns.Add(Schema.QuestionnaireSections.COL_SECTION_ID);
                    dLangTable.Columns.Add(Schema.Language.COL_LANGUAGE_ID);
                    dLangTable.Columns.Add(Schema.QuestionnaireSections.COL_SECTION_TITLE);
                    dLangTable.Columns.Add(Schema.QuestionnaireSections.COL_SECTION_NAME);
                    dLangTable.Columns.Add(Schema.QuestionnaireSections.COL_SECTION_DESC);
                    dLangTable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    dLangTable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (QuestionnaireSections objSection in pEntListSections)
                    {

                        DataRow dLangRow = dLangTable.NewRow();
                        dLangRow[Schema.QuestionnaireSections.COL_SECTION_ID] = objSection.ID;
                        dLangRow[Schema.Language.COL_LANGUAGE_ID] = objSection.LanguageId;
                        dLangRow[Schema.QuestionnaireSections.COL_SECTION_TITLE] = objSection.SectionTitle;
                        dLangRow[Schema.QuestionnaireSections.COL_SECTION_NAME] = objSection.SectionName;
                        dLangRow[Schema.QuestionnaireSections.COL_SECTION_DESC] = objSection.SectionDescription;
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
                        _sqlcmd.CommandText = Schema.QuestionnaireSections.PROC_UPDATE_QUESTNN_SCT_LANGUAGE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcmd.Connection = pSqlConn;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_SECTION_ID, SqlDbType.VarChar, 100, Schema.QuestionnaireSections.COL_SECTION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Language.PARA_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Language.COL_LANGUAGE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_SECTION_TITLE, SqlDbType.NVarChar, 500, Schema.QuestionnaireSections.COL_SECTION_TITLE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_SECTION_NAME, SqlDbType.NVarChar, 4000, Schema.QuestionnaireSections.COL_SECTION_NAME);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.QuestionnaireSections.PARA_SECTION_DESC, SqlDbType.NVarChar, 4000, Schema.QuestionnaireSections.COL_SECTION_DESC);
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
        public QuestionnaireSections DeleteSection(QuestionnaireSections pEntSection)
        {
            _sqlObject = new SQLObject();
            QuestionnaireSections entSection = new QuestionnaireSections();
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.QuestionnaireSections.PROC_DELETE_SCTION;
            _sqlpara = new SqlParameter(Schema.QuestionnaireSections.PARA_QUESTIONNAIRE_ID, pEntSection.QuestionnaireId);
            _sqlcmd.Parameters.Add(_sqlpara);
            _sqlpara = new SqlParameter(Schema.QuestionnaireSections.PARA_SECTION_ID, pEntSection.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntSection.ClientId);
                //Delete Questionnaire from database
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

        public QuestionnaireSections DeleteSectionLanguage(QuestionnaireSections pEntSection)
        {
            _sqlObject = new SQLObject();
            QuestionnaireSections entSection = new QuestionnaireSections();
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.QuestionnaireSections.PROC_DELETE_SECTIONLANGUAGE;
            //_sqlpara = new SqlParameter(Schema.QuestionnaireSections.PARA_QUESTIONNAIRE_ID, pEntSection.QuestionnaireId);
            //_sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.QuestionnaireSections.PARA_SECTION_ID, pEntSection.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntSection.LanguageId);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntSection.ClientId);
                //Delete Questionnaire from database
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
        /// <param name="pEntQuestionnaireSections"></param>
        /// <returns></returns>
        public QuestionnaireSections UpdateSequence(QuestionnaireSections pEntQuestionnaireSections)
        {
            int iRows = 0;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.QuestionnaireSections.PROC_UPDATE_SEQUENCE;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntQuestionnaireSections.ClientId);

                if (!string.IsNullOrEmpty(pEntQuestionnaireSections.QuestionnaireId))
                {
                    _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, pEntQuestionnaireSections.QuestionnaireId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Questionnaire.PARA_QUESTIONNAIRE_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntQuestionnaireSections.ID))
                {
                    _sqlpara = new SqlParameter(Schema.QuestionnaireSections.PARA_SECTION_ID, pEntQuestionnaireSections.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.QuestionnaireSections.PARA_SECTION_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (Convert.ToString(pEntQuestionnaireSections.SequenceOrder) != string.Empty)
                {
                    _sqlpara = new SqlParameter(Schema.QuestionnaireSections.PARA_SEQUENCE_ORDER, pEntQuestionnaireSections.SequenceOrder);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.QuestionnaireSections.PARA_SEQUENCE_ORDER, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlpara = new SqlParameter(Schema.QuestionnaireQuestion.PARA_IS_UP, pEntQuestionnaireSections.IsMoveUp);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntQuestionnaireSections.LastModifiedById))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntQuestionnaireSections.LastModifiedById);
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
            return pEntQuestionnaireSections;
        }
        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntQuestionnaireSections"></param>
        /// <returns></returns>
        public QuestionnaireSections Get(QuestionnaireSections pEntQuestionnaireSections)
        {
            return GetSectionById(pEntQuestionnaireSections);
        }
        /// <summary>
        /// Update QuestionnaireSections
        /// </summary>
        /// <param name="pEntQuestionnaireSections"></param>
        /// <returns></returns>
        public QuestionnaireSections Update(QuestionnaireSections pEntQuestionnaireSections)
        {
            return EditSection(pEntQuestionnaireSections);
        }
        #endregion
    }
}
