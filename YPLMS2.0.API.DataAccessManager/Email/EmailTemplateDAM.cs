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
    public class EmailTemplateDAM : IDataManager<EmailTemplate>
    {
        #region Declaration

        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlDataReader _sqlreader1 = null;
        SqlParameter _sqlpara = null;
        SqlCommand _sqlcmd = null;
        EntityRange _entRange = null;
        SqlConnection _sqlConnection = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.EmailTemplate.DL_ERROR;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>EmailTemplate Object</returns>
        private EmailTemplateLanguage FillObjectTempleteLanguage(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            EmailTemplateLanguage entEmailTemplateLanguage = new EmailTemplateLanguage();
            int iIndex = 0;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_EMAIL_TEMPLATE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplateLanguage.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_LANGUAGE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplateLanguage.LanguageId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_DISPLAY_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplateLanguage.DisplayName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_EMAIL_SUBJECT_TEXT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplateLanguage.EmailSubjectText = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_EMAIL_BODY_TEXT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplateLanguage.EmailBodyText = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_RTL_TYPE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplateLanguage.RTLType = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_APPROVAL_STATUS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplateLanguage.ApprovalStatus = (EmailTemplateLanguage.EmailApprovalStatus)Enum.Parse(typeof(EmailTemplateLanguage.EmailApprovalStatus), pSqlReader.GetString(iIndex));

                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_EMAIL_ADDRESS_STRING);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplateLanguage.EmailAddressString = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_EMAIL_TYPE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplateLanguage.EmailType = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_IS_ACTIVE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplateLanguage.IsActive = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_APPROVED_BY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplateLanguage.ApprovedById = pSqlReader.GetString(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailTemplateLanguage.CreatedByName = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailTemplateLanguage.ModifiedByName = pSqlReader.GetString(iIndex);
                }
                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    entEmailTemplateLanguage.ListRange = _entRange;
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailTemplateLanguage.CreatedById = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailTemplateLanguage.DateCreated = pSqlReader.GetDateTime(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailTemplateLanguage.LastModifiedById = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailTemplateLanguage.LastModifiedDate = pSqlReader.GetDateTime(iIndex);
                }

            }
            return entEmailTemplateLanguage;
        }

        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>EmailTemplate Object</returns>
        private YPLMS2._0.API.Entity.EmailTemplate FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            YPLMS2._0.API.Entity.EmailTemplate entEmailTemplate = new YPLMS2._0.API.Entity.EmailTemplate();
            int iIndex = 0;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_EMAIL_TEMPLATE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplate.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_EMAIL_TEMPLATE_DEFAULT_TITLE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplate.EmailTemplateDefaultTitle = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_IS_PRIVATE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplate.IsPrivate = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_IS_DEFAULT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplate.IsDefualt = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_IS_ACTIVE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplate.IsActive = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_EMAIL_FROM_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplate.EmailFromId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_EMAIL_REPLY_TO_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplate.EmailReplyToId = pSqlReader.GetString(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.EmailTemplate.COL_IS_USED))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_IS_USED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailTemplate.IsUsed = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.EmailTemplate.COL_USED_COUNT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_USED_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailTemplate.NoOfTimesUsed = pSqlReader.GetInt32(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailTemplate.CreatedByName = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailTemplate.ModifiedByName = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.EmailTemplate.COL_APPROVED_BY_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_APPROVED_BY_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailTemplate.ApprovedById = pSqlReader.GetString(iIndex);
                }
                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    entEmailTemplate.ListRange = _entRange;
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailTemplate.CreatedById = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailTemplate.DateCreated = pSqlReader.GetDateTime(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailTemplate.LastModifiedById = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailTemplate.LastModifiedDate = pSqlReader.GetDateTime(iIndex);
                }

            }
            return entEmailTemplate;
        }

        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>EmailTemplate Object</returns>
        private YPLMS2._0.API.Entity.EmailTemplate FillObject_GetEmailType(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            YPLMS2._0.API.Entity.EmailTemplate entEmailTemplate = new YPLMS2._0.API.Entity.EmailTemplate();
            int iIndex = 0;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_EMAIL_TEMPLATE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplate.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailTemplate.COL_EMAIL_TYPE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailTemplate.EmailType = pSqlReader.GetString(iIndex);
            }
            return entEmailTemplate;
        }


        /// <summary>
        /// Get All EmailTemplate Details
        /// </summary>
        /// <returns>List of EmailTemplate Object</returns>
        public List<EmailTemplate> GetListAllEmailTemplate(EmailTemplate pEntEmailTemp)
        {
            _sqlObject = new SQLObject();
            EmailTemplate entEmailTemplate = null;
            List<EmailTemplate> entListEmailTemplate = null;
            _strConnString = _sqlObject.GetClientDBConnString(pEntEmailTemp.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.EmailTemplate.PROC_GET_ALL_EMAIL_TEMPLATE, _sqlConnection);
            entEmailTemplate = new EmailTemplate();
            entListEmailTemplate = new List<EmailTemplate>();

            if (pEntEmailTemp.IsActive != null)
            {
                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_IS_ACTIVE, pEntEmailTemp.IsActive);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            //-- if the approval (Approved) then Get only Approved languages Otherwise Get all languages.
            if (pEntEmailTemp.ApprovalStatus == EmailTemplateLanguage.EmailApprovalStatus.None)
            {
                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_APPROVAL_STATUS, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            else
            {
                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_APPROVAL_STATUS, pEntEmailTemp.ApprovalStatus.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            if (pEntEmailTemp.ListRange != null)
            {
                if (pEntEmailTemp.ListRange.PageIndex > 0)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntEmailTemp.ListRange.PageIndex);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntEmailTemp.ListRange.PageSize > 0)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntEmailTemp.ListRange.PageSize);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailTemp.ListRange.SortExpression))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntEmailTemp.ListRange.SortExpression);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailTemp.ListRange.RequestedById))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntEmailTemp.ListRange.RequestedById);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailTemp.LanguageId))
                    _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_LANGUAGE_ID, pEntEmailTemp.LanguageId);
                else
                    _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_LANGUAGE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            if (!string.IsNullOrEmpty(pEntEmailTemp.ID))
            {
                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_EMAIL_TEMPLATE_ID, pEntEmailTemp.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entEmailTemplate = FillObject(_sqlreader, true, _sqlObject);

                    if (entEmailTemplate != null)
                    {
                        entEmailTemplate.ClientId = pEntEmailTemp.ClientId;
                        entEmailTemplate.ApprovalStatus = pEntEmailTemp.ApprovalStatus;
                        List<EmailTemplateLanguage> entListEmailLang = GetLanguageTemplete(entEmailTemplate, false);
                        if (entListEmailLang.Count > 0)
                            entEmailTemplate.EmailTemplateLanguage.AddRange(entListEmailLang);
                    }
                    entListEmailTemplate.Add(entEmailTemplate);
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
                if (_sqlConnection != null && _sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.Close();
            }
            return entListEmailTemplate;
        }

        /// <summary>
        /// Get EmailTemplate Details By Id
        /// </summary>
        /// <returns>List of EmailTemplate Object</returns>
        public EmailTemplate GetEmailTemplateById(EmailTemplate pEntEmailTemp)
        {
            _sqlObject = new SQLObject();
            EmailTemplate entEmailTemplate = null;
            _strConnString = _sqlObject.GetClientDBConnString(pEntEmailTemp.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.EmailTemplate.PROC_GET_EMAIL_TEMPLATE, _sqlConnection);
            _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_EMAIL_TEMPLATE_ID, pEntEmailTemp.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            //-- if the approval (Approved) then Get only Approved languages Otherwise Get all languages.
            if (pEntEmailTemp.IsActive != null)
            {
                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_IS_ACTIVE, pEntEmailTemp.IsActive);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            entEmailTemplate = new EmailTemplate();
            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    entEmailTemplate = FillObject(_sqlreader, false, _sqlObject);

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
                if (_sqlConnection != null && _sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.Close();
            }
            //by shrihari on 29 june
            if (entEmailTemplate != null)
            {
                if (pEntEmailTemp.ListRange != null)
                    entEmailTemplate.ListRange = pEntEmailTemp.ListRange;

                entEmailTemplate.ClientId = pEntEmailTemp.ClientId;

                List<EmailTemplateLanguage> entListEmailLang = GetLanguageTemplete(entEmailTemplate, true);
                if (entListEmailLang.Count > 0)
                    entEmailTemplate.EmailTemplateLanguage.AddRange(entListEmailLang);
            }
            //end by shrihari on 29 june
            return entEmailTemplate;
        }

        /// <summary>
        /// Get EmailTemplate Details By Id
        /// </summary>
        /// <returns>List of EmailTemplate Object</returns>
        public EmailTemplate GetEmailTypeById(EmailTemplate pEntEmailTemp)
        {
            _sqlObject = new SQLObject();
            EmailTemplate entEmailTemplate = null;
            _strConnString = _sqlObject.GetClientDBConnString(pEntEmailTemp.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.EmailTemplate.PROC_GET_EMAIL_TEMPLATE_EMAIL_TYPE, _sqlConnection);
            _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_EMAIL_TEMPLATE_ID, pEntEmailTemp.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            //-- if the approval (Approved) then Get only Approved languages Otherwise Get all languages.
            if (pEntEmailTemp.IsActive != null)
            {
                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_IS_ACTIVE, pEntEmailTemp.IsActive);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            entEmailTemplate = new EmailTemplate();
            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    entEmailTemplate = FillObject_GetEmailType(_sqlreader, false, _sqlObject);
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
                if (_sqlConnection != null && _sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.Close();
            }
            return entEmailTemplate;
        }

        /// <summary>
        /// Get EmailTemplate Details By Name
        /// </summary>
        /// <returns>List of EmailTemplate Object</returns>
        public EmailTemplate GetEmailTemplateByName(EmailTemplate pEntEmailTemp)
        {
            EmailTemplate entEmailTemplate = null;
            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(pEntEmailTemp.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.EmailTemplate.PROC_GET_EMAIL_TEMPLATE_BY_NAME, _sqlConnection);
            _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_EMAIL_TEMPLATE_DEFAULT_TITLE, pEntEmailTemp.EmailTemplateDefaultTitle);
            _sqlcmd.Parameters.Add(_sqlpara);
            entEmailTemplate = new EmailTemplate();
            bool IsExist = false;
            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    IsExist = true;
                    entEmailTemplate = FillObject(_sqlreader, false, _sqlObject);
                    if (entEmailTemplate != null)
                    {
                        if (pEntEmailTemp.ListRange != null)
                            entEmailTemplate.ListRange = pEntEmailTemp.ListRange;

                        entEmailTemplate.ClientId = pEntEmailTemp.ClientId;

                        List<EmailTemplateLanguage> entListEmailLang = GetLanguageTemplete(entEmailTemplate, true);
                        if (entListEmailLang.Count > 0)
                            entEmailTemplate.EmailTemplateLanguage.AddRange(entListEmailLang);
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
                if (_sqlreader != null && !_sqlreader.IsClosed)
                    _sqlreader.Close();
                if (_sqlConnection != null && _sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.Close();
            }
            if (IsExist == false)
            {
                entEmailTemplate = null;
            }
            return entEmailTemplate;
        }

        /// Get All EmailTemplate Language Details
        /// </summary>
        /// <returns>List of EmailTemplate Language Object</returns>
        public List<EmailTemplateLanguage> GetLanguageTemplete(EmailTemplate pEntEmailTemplate, bool pRangeList)
        {
            List<EmailTemplateLanguage> entListEmailTemplateLanguage = new List<EmailTemplateLanguage>();
            EmailTemplateLanguage entEmailTemplateLanguage = new EmailTemplateLanguage();
            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(pEntEmailTemplate.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.EmailTemplate.PROC_GET_ALL_EMAIL_TEMPLATE_LANGUAGE, _sqlConnection);
            if (!string.IsNullOrEmpty(pEntEmailTemplate.ID))
            {
                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_EMAIL_TEMPLATE_ID, pEntEmailTemplate.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            //-- if the approval (Approved) then Get only Approved languages Otherwise Get all languages.
            if (pEntEmailTemplate.ApprovalStatus == EmailTemplateLanguage.EmailApprovalStatus.None)
            {
                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_APPROVAL_STATUS, null);
            }
            else
            {
                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_APPROVAL_STATUS, pEntEmailTemplate.ApprovalStatus.ToString());
            }
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntEmailTemplate.ListRange != null)
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntEmailTemplate.ListRange.PageIndex);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntEmailTemplate.ListRange != null)
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntEmailTemplate.ListRange.PageSize);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _sqlreader1 = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader1.Read())
                {
                    entEmailTemplateLanguage = FillObjectTempleteLanguage(_sqlreader1, pRangeList, _sqlObject);
                    entListEmailTemplateLanguage.Add(entEmailTemplateLanguage);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader1 != null && !_sqlreader1.IsClosed)
                    _sqlreader1.Close();
                if (_sqlConnection != null && _sqlConnection.State != ConnectionState.Closed)
                    _sqlConnection.Close();
            }
            return entListEmailTemplateLanguage;
        }

        /// <summary>
        /// To update/add new Email Template
        /// </summary>
        /// <param name="pEntEmailTemplate"></param>        
        /// <returns>EmailTemplate object</returns>
        public EmailTemplate AddUpdateEmailTemplate(EmailTemplate pEntEmailTemplate)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            string strUpdateMode = string.Empty;
            _sqlcmd.CommandText = Schema.EmailTemplate.PROC_UPDATE_EMAIL_TEMPLATE;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntEmailTemplate.ClientId);
                if (string.IsNullOrEmpty(pEntEmailTemplate.ID))
                {
                    pEntEmailTemplate.ID = YPLMS.Services.IDGenerator.GetUniqueKey(8);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                    strUpdateMode = Schema.Common.VAL_INSERT_MODE;
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);
                    strUpdateMode = Schema.Common.VAL_UPDATE_MODE;
                }
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_EMAIL_TEMPLATE_ID, pEntEmailTemplate.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_EMAIL_TEMPLATE_DEFAULT_TITLE, pEntEmailTemplate.EmailTemplateDefaultTitle);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_IS_PRIVATE, pEntEmailTemplate.IsPrivate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_IS_DEFAULT, pEntEmailTemplate.IsDefualt);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_IS_ACTIVE, pEntEmailTemplate.IsActive);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailTemplate.EmailFromId))
                    _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_EMAIL_FROM_ID, pEntEmailTemplate.EmailFromId);
                else
                    _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_EMAIL_FROM_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailTemplate.EmailReplyToId))
                    _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_EMAIL_REPLY_TO_ID, pEntEmailTemplate.EmailReplyToId);
                else
                    _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_EMAIL_REPLY_TO_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntEmailTemplate.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

                //---------- Add/Update Email Tempelate Language ---------------
                if (pEntEmailTemplate.EmailTemplateLanguage.Count > 0)
                {
                    foreach (EmailTemplateLanguage entEmailLang in pEntEmailTemplate.EmailTemplateLanguage)
                    {
                        entEmailLang.ID = pEntEmailTemplate.ID;
                        entEmailLang.ClientId = pEntEmailTemplate.ClientId;
                        entEmailLang.LastModifiedById = pEntEmailTemplate.LastModifiedById;
                        //Add languages
                        AddUpdateEmailTemplateLanguages(entEmailLang, strUpdateMode);
                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntEmailTemplate;
        }

        /// <summary>
        /// Add Update Email Template Languages
        /// </summary>
        /// <param name="pEntEmailTemplateLanguage"></param>
        /// <returns></returns>
        private EmailTemplateLanguage AddUpdateEmailTemplateLanguages(EmailTemplateLanguage pEntEmailTemplateLanguage, string pstrUpdateMode)
        {
            EmailTemplateLanguage entEmailTemplateLanguage = null;
            entEmailTemplateLanguage = pEntEmailTemplateLanguage;
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.EmailTemplate.PROC_UPDATE_EMAIL_TEMPLATE_LANGUAGE;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(entEmailTemplateLanguage.ClientId);

                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_EMAIL_TEMPLATE_ID, entEmailTemplateLanguage.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_LANGUAGE_ID, entEmailTemplateLanguage.LanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_DISPLAY_NAME, entEmailTemplateLanguage.DisplayName);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_EMAIL_SUBJECT_TEXT, entEmailTemplateLanguage.EmailSubjectText);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_EMAIL_BODY_TEXT, entEmailTemplateLanguage.EmailBodyText);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_RTL_TYPE, entEmailTemplateLanguage.RTLType);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_APPROVAL_STATUS, entEmailTemplateLanguage.ApprovalStatus.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_IS_ACTIVE, entEmailTemplateLanguage.IsActive);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_EMAIL_ADDRESS_STRING, entEmailTemplateLanguage.EmailAddressString);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_EMAIL_TYPE, entEmailTemplateLanguage.EmailType);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(entEmailTemplateLanguage.ApprovedById))
                    _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_APPROVED_BY_ID, entEmailTemplateLanguage.ApprovedById);
                else
                    _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_APPROVED_BY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, entEmailTemplateLanguage.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, pstrUpdateMode);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entEmailTemplateLanguage;
        }

        /// <summary>
        /// Delete Email Template
        /// </summary>
        /// <param name="pEntListEmailTemplate"></param>
        /// <returns></returns>
        public List<EmailTemplate> DeleteEmailTemplate(List<EmailTemplate> pEntListEmailTemplate)
        {
            List<EmailTemplate> entListEmailTemplate = BulkDelete(pEntListEmailTemplate);
            return entListEmailTemplate;
        }

        /// <summary>
        /// Bulk Add/Update
        /// </summary>
        /// <param name="pEntListBusinessRuleUsers"></param>
        /// <returns>List of added/updated BusinessRuleUsers</returns>
        private List<EmailTemplate> BulkDelete(List<EmailTemplate> pEntListEmailTemplate)
        {
            List<EmailTemplate> entListEmailTemplate = new List<EmailTemplate>();
            SqlDataAdapter sqladapter = null;
            DataTable dtable;
            SqlCommand sqlcmdDel;
            int iBatchSize = 0;
            DataRow drow = null;
            EntityRange entRange = new EntityRange();
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntListEmailTemplate[0].ClientId);
                _sqlConnection = new SqlConnection(_strConnString);
                dtable = new DataTable();
                dtable.Columns.Add(Schema.EmailTemplate.COL_EMAIL_TEMPLATE_ID);

                //-- Adding Dummy Record on First Row(must be on 1st row) 
                if (pEntListEmailTemplate.Count > 0)
                {
                    //Add Dummy Record in Table, Bcoz if records which is all asssign then it will not execute nonquery so not get count
                    drow = dtable.NewRow();
                    drow[Schema.EmailTemplate.COL_EMAIL_TEMPLATE_ID] = "Temp0123456789";
                    dtable.Rows.Add(drow);
                    iBatchSize++;
                }

                foreach (EmailTemplate entGR in pEntListEmailTemplate)
                {
                    drow = dtable.NewRow();
                    drow[Schema.EmailTemplate.COL_EMAIL_TEMPLATE_ID] = entGR.ID;
                    dtable.Rows.Add(drow);
                    entListEmailTemplate.Add(entGR);
                    iBatchSize++;
                }

                if (dtable.Rows.Count > 0)
                {
                    sqlcmdDel = new SqlCommand(Schema.EmailTemplate.PROC_DELETE_EMAIL_TEMPLATE, _sqlConnection);
                    sqlcmdDel.CommandType = CommandType.StoredProcedure;
                    sqlcmdDel.Parameters.Add(Schema.EmailTemplate.PARA_EMAIL_TEMPLATE_ID, SqlDbType.NVarChar, 100, Schema.EmailTemplate.COL_EMAIL_TEMPLATE_ID);
                    sqladapter = new SqlDataAdapter();
                    sqladapter.InsertCommand = sqlcmdDel;
                    sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;

                    sqladapter.UpdateBatchSize = iBatchSize;
                    entRange.TotalRows = sqladapter.Update(dtable);
                }
                //Bind Total Rows to List: To know how many records are affected/delete.
                entListEmailTemplate[0].ListRange = entRange;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListEmailTemplate;
        }

        /// <summary>
        /// Delete Email Template Language
        /// </summary>
        /// <param name="pEntEmailTemplate"></param>
        /// <returns></returns>
        public EmailTemplate DeleteEmailTemplateLanguage(EmailTemplate pEntEmailTemplate)
        {
            if (pEntEmailTemplate.EmailTemplateLanguage.Count > 0)
                pEntEmailTemplate = BulkDelete_EmailTemplateLanguage(pEntEmailTemplate);
            else
                pEntEmailTemplate = null;
            return pEntEmailTemplate;
        }

        /// <summary>
        /// Bulk Delete Email Template Language
        /// </summary>
        /// <param name="pEntEmailTemplate"></param>
        /// <returns></returns>
        private EmailTemplate BulkDelete_EmailTemplateLanguage(EmailTemplate pEntEmailTemplate)
        {
            EmailTemplate entEmailTemplateReturn = new EmailTemplate();
            List<EmailTemplateLanguage> entListEmailTemplateLanguage = new List<EmailTemplateLanguage>();
            SqlDataAdapter sqladapter = null;
            DataTable dtable;
            SqlCommand sqlcmdDel;
            int iBatchSize = 0;
            DataRow drow = null;
            EntityRange entRange = new EntityRange();
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntEmailTemplate.ClientId);
                _sqlConnection = new SqlConnection(_strConnString);
                dtable = new DataTable();
                dtable.Columns.Add(Schema.EmailTemplate.COL_EMAIL_TEMPLATE_ID);
                dtable.Columns.Add(Schema.EmailTemplate.COL_LANGUAGE_ID);

                //Set Languages
                entListEmailTemplateLanguage = pEntEmailTemplate.EmailTemplateLanguage;

                foreach (EmailTemplateLanguage entGR in entListEmailTemplateLanguage)
                {
                    drow = dtable.NewRow();
                    drow[Schema.EmailTemplate.COL_EMAIL_TEMPLATE_ID] = pEntEmailTemplate.ID;
                    drow[Schema.EmailTemplate.COL_LANGUAGE_ID] = entGR.LanguageId;
                    dtable.Rows.Add(drow);
                    iBatchSize++;
                }
                if (dtable.Rows.Count > 0)
                {
                    sqlcmdDel = new SqlCommand(Schema.EmailTemplate.PROC_DELETE_EMAIL_TEMPLATE_LANGUAGE, _sqlConnection);
                    sqlcmdDel.CommandType = CommandType.StoredProcedure;
                    sqlcmdDel.Parameters.Add(Schema.EmailTemplate.PARA_EMAIL_TEMPLATE_ID, SqlDbType.NVarChar, 100, Schema.EmailTemplate.COL_EMAIL_TEMPLATE_ID);
                    sqlcmdDel.Parameters.Add(Schema.EmailTemplate.PARA_LANGUAGE_ID, SqlDbType.NVarChar, 100, Schema.EmailTemplate.COL_LANGUAGE_ID);
                    sqladapter = new SqlDataAdapter();
                    sqladapter.InsertCommand = sqlcmdDel;
                    sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;

                    sqladapter.UpdateBatchSize = iBatchSize;
                    entRange.TotalRows = sqladapter.Update(dtable);
                }
                //Bind Total Rows to List: To know how many records are affected/delete.
                entEmailTemplateReturn = pEntEmailTemplate;
                entEmailTemplateReturn.ListRange = entRange;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entEmailTemplateReturn;
        }

        /// <summary>
        /// Find Email Templates
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<EmailTemplate> FindEmailTemplates(Search pEntSearch)
        {
            _sqlObject = new SQLObject();
            List<EmailTemplate> entListEmailTemplate = new List<EmailTemplate>();
            EmailTemplate entEmailTemplateSearch = new EmailTemplate();
            EmailTemplate entEmailTemplateToDate = new EmailTemplate();
            EmailTemplate entEmailTemplateLanguage = new EmailTemplate();
            EmailTemplate entEmailTemplate = new EmailTemplate();


            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.EmailTemplate.PROC_FIND_EMAIL_TEMPLATE;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                if (pEntSearch.ListRange != null)
                {
                    if (pEntSearch.ListRange.PageIndex > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntSearch.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntSearch.ListRange.PageSize > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntSearch.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntSearch.ListRange.SortExpression))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntSearch.ListRange.RequestedById))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, null);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }


                if (pEntSearch.SearchObject != null && pEntSearch.SearchObject.Count > 0)
                {
                    entEmailTemplateSearch = (EmailTemplate)pEntSearch.SearchObject[0];

                    if (!string.IsNullOrEmpty(pEntSearch.KeyWord))
                    {
                        _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_EMAIL_TEMPLATE_DEFAULT_TITLE, pEntSearch.KeyWord);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (!string.IsNullOrEmpty(entEmailTemplateSearch.LanguageId))
                        _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_LANGUAGE_ID, entEmailTemplateSearch.LanguageId);
                    else
                        _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_LANGUAGE_ID, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(Convert.ToString(entEmailTemplateSearch.ApprovalStatus)))
                    {
                        if (entEmailTemplateSearch.ApprovalStatus == EmailTemplateLanguage.EmailApprovalStatus.None)
                        {
                            _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_APPROVAL_STATUS, System.DBNull.Value);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                        else
                        {
                            _sqlpara = new SqlParameter(Schema.EmailTemplate.PARA_APPROVAL_STATUS, entEmailTemplateSearch.ApprovalStatus.ToString());
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }

                    if (DateTime.MinValue.CompareTo(entEmailTemplateSearch.DateCreated) < 0)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_DATE_FROM, entEmailTemplateSearch.DateCreated);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (!string.IsNullOrEmpty(entEmailTemplateSearch.CreatedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, entEmailTemplateSearch.CreatedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (pEntSearch.SearchObject.Count > 1)
                    {
                        entEmailTemplateToDate = (EmailTemplate)pEntSearch.SearchObject[1];
                        if (DateTime.MinValue.CompareTo(entEmailTemplateToDate.DateCreated) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.Common.PARA_DATE_TO, entEmailTemplateToDate.DateCreated);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entEmailTemplate = FillObject(_sqlreader, true, _sqlObject);

                    if (entEmailTemplate != null)
                    {
                        entEmailTemplate.ClientId = entEmailTemplateSearch.ClientId;
                        entEmailTemplate.ApprovalStatus = entEmailTemplateSearch.ApprovalStatus;
                        List<EmailTemplateLanguage> entListEmailLang = GetLanguageTemplete(entEmailTemplate, false);
                        if (entListEmailLang.Count > 0)
                            entEmailTemplate.EmailTemplateLanguage.AddRange(entListEmailLang);
                    }
                    entListEmailTemplate.Add(entEmailTemplate);
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
            return entListEmailTemplate;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntEmailTemplate"></param>
        /// <returns></returns>
        public EmailTemplate Get(EmailTemplate pEntEmailTemplate)
        {
            return GetEmailTemplateById(pEntEmailTemplate);
        }
        /// <summary>
        /// Update EmailTemplate
        /// </summary>
        /// <param name="pEntEmailTemplate"></param>
        /// <returns></returns>
        public EmailTemplate Update(EmailTemplate pEntEmailTemplate)
        {
            return AddUpdateEmailTemplate(pEntEmailTemplate);
        }
        #endregion
    }
}
