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
    public class AutoMessageMappingAdaptor : IDataManager<AutoMessageMapping>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
       // string _strMessageId = YPLMS.Services.Document.DOCUMENT_BL_ERROR;
        //string strDocumentAssignMsgId = Services.MessageTemplate.DocumentLibrary.DOCUMENT_LIBRARY_ASSIGN;
        string _strConnString = string.Empty;
        #endregion

        public AutoMessageMapping GetTemplateById(AutoMessageMapping pEntTemplate)
        {
            _sqlObject = new SQLObject();
            AutoMessageMapping entTemplate = new AutoMessageMapping();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntTemplate.ClientId);
                sqlConnection = new SqlConnection(_strConnString);

                _sqlcmd = new SqlCommand(Schema.MessageTemplate.PROC_GET_MESSAGE_TEMPLATE_AUTOMAPPING, sqlConnection);

                _sqlpara = new SqlParameter(Schema.MessageTemplate.PARA_AUTO_EVENT_ID, pEntTemplate.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entTemplate = FillObject(_sqlreader, false, _sqlObject);
            }
            catch (Exception expCommon)
            {
                // _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                // throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed)
                    _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entTemplate;
        }

        public List<AutoMessageMapping> GetMessageList(AutoMessageMapping pEntMessage)
        {
            _sqlObject = new SQLObject();
            List<AutoMessageMapping> entListMessage = new List<AutoMessageMapping>();
            AutoMessageMapping entMessage = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntMessage.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.MessageTemplate.PROC_GET_ALL_MESSAGE_TEMPLATE_AUTOMAPPING, sqlConnection);

                if (pEntMessage.ListRange != null)
                {
                    if (pEntMessage.ListRange.PageIndex > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntMessage.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntMessage.ListRange.PageSize > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntMessage.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntMessage.ListRange.SortExpression))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntMessage.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntMessage.ListRange.RequestedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntMessage.ListRange.RequestedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entMessage = FillObject(_sqlreader, true, _sqlObject);
                    entListMessage.Add(entMessage);
                }
            }
            catch (Exception expCommon)
            {
                //_expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
               // throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed)
                    _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entListMessage;
        }

        private AutoMessageMapping FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            AutoMessageMapping entTemplate = new AutoMessageMapping();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.MessageTemplate.COL_AUTO_EVENT_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.MessageTemplate.COL_AUTO_EVENT_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.ID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.MessageTemplate.COL_EVENT_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.MessageTemplate.COL_EVENT_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.EventName = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.MessageTemplate.COL_TEMPLATE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.MessageTemplate.COL_TEMPLATE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.TemplateID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.MessageTemplate.COL_TEMPLATE_TITLE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.MessageTemplate.COL_TEMPLATE_TITLE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.TemplateTitle = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Language.COL_LANGUAGE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_LANGUAGE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.LanguageID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Language.COL_LANG_ENG_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Language.COL_LANG_ENG_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.LanguageName = pSqlReader.GetString(iIndex);
                    else
                        entTemplate.LanguageName = "English";
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.LastModifiedById = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.LastModifiedDate = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entTemplate.LastModifiedByName = pSqlReader.GetString(iIndex);
                }

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                    {
                        iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                        if (!pSqlReader.IsDBNull(iIndex))
                            _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                        entTemplate.ListRange = _entRange;
                    }

                }

            }
            return entTemplate;
        }

        public AutoMessageMapping Get(AutoMessageMapping pEntBase)
        {
            throw new NotImplementedException();
        }

        public AutoMessageMapping Update(AutoMessageMapping pEntBase)
        {
            throw new NotImplementedException();
        }
    }
}
