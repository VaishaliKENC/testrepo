using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    public class EmailDeliveryDashboardDAM : IDataManager<EmailDeliveryDashboard>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlParameter _sqlpara = null;
        SqlCommand _sqlcmd = null;
        EntityRange _entRange = null;
        SqlConnection _sqlConnection = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.ActivityAssignment.ACTIVITY_ASSIGN_ERROR;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>EmailDeliveryDashboard Object</returns>
        private EmailDeliveryDashboard FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            EmailDeliveryDashboard entEmailDeliveryDashboard = new EmailDeliveryDashboard();
            int iIndex = 0;
            if (pSqlReader.HasRows)
            {

                //For Dynamic assignment Email 

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_RULE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.RuleId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_ASSIGNMENTTYPEID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.AssignmentTypeID = pSqlReader.GetString(iIndex);



                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_EMAIL_DELIVERY_INSTANCE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_APPROVALDATE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.ApprovalDate = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_APPROVED_BY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.ApprovedById = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_TO_LIST);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.ToList = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_BCC_LIST);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.BCCList = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_CC_LIST);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.CCList = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_DATETIME_SET);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.DateTimeSet = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_END_DATE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.EndDate = pSqlReader.GetDateTime(iIndex);


                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_DELIVERY_APPROVAL_STATUS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.DeliveryApprovalStatus = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_DISTRIBUTION_LIST_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.DistributionListId = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_EMAIL_DELIVERY_TITLE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.EmailDeliveryTitle = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_Email_Template_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.EmailTemplateID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_IS_ALL_LANGUAGES);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.IsAllLanguages = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_IS_BCC_MANAGER);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.IsBCCManager = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_IS_CCMANAGER);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.IsCCManager = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_IS_IMMEDIATE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.IsImmediate = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_IS_MONTHLY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.IsMonthly = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_IS_ONETIME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.IsOneTime = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_IS_PERSONALIZED);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.IsPersonalized = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_IS_RECURRENCE_APPROVAL_REQUIRED);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.IsRecurrenceApprovalRequired = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_IS_SITE_DEFAULT_LANGUAGE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.IsSiteDefaultLanguage = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_IS_USER_PREFERRED_LANGUAGE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.IsUserPreferredLanguage = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_IS_DAILY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.IsDaily = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_IS_WEEKLY);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.IsWeekly = pSqlReader.GetBoolean(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_ONETIME_DATESET);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.OneTimeDateSet = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_RECURRENCE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entEmailDeliveryDashboard.Recurrence = pSqlReader.GetInt32(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.EmailDeliveryDashboard.COL_ACTIVITY_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_ACTIVITY_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailDeliveryDashboard.ActivityId = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.EmailDeliveryDashboard.COL_ASSIGNMENT_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_ASSIGNMENT_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailDeliveryDashboard.AssignmentId = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.EmailDeliveryDashboard.COL_ATTACHMENTPATHLIST))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_ATTACHMENTPATHLIST);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailDeliveryDashboard.AttachmentPathList = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.EmailDeliveryDashboard.COL_TASK_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_TASK_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailDeliveryDashboard.TaskId = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_CLIENT_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailDeliveryDashboard.ClientId = pSqlReader.GetString(iIndex);
                }
                //added by Gitanjali 27.12.2010
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.EmailDeliveryDashboard.COL_IS_WEEKDAYS_ONLY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_IS_WEEKDAYS_ONLY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailDeliveryDashboard.IsWeekdaysOnly = pSqlReader.GetBoolean(iIndex);
                }
                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    entEmailDeliveryDashboard.ListRange = _entRange;
                }



                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailDeliveryDashboard.CreatedById = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailDeliveryDashboard.DateCreated = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailDeliveryDashboard.LastModifiedById = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailDeliveryDashboard.LastModifiedDate = pSqlReader.GetDateTime(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.EmailDeliveryDashboard.COL_LEARNER_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_LEARNER_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailDeliveryDashboard.LearnerId = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.EmailDeliveryDashboard.COL_ASSIGNMENTMODE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_ASSIGNMENTMODE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailDeliveryDashboard.AssignmentMode = (ActivityAssignmentMode)Enum.Parse(typeof(ActivityAssignmentMode), pSqlReader.GetString(iIndex));
                }

                // added by Gitanjali 26.08.2010
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.EmailDeliveryDashboard.COL_IsDistributionToManager))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_IsDistributionToManager);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailDeliveryDashboard.IsDistributionToManager = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.EmailDeliveryDashboard.COL_ManagerEmailId))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_ManagerEmailId);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailDeliveryDashboard.ManagerEmailId = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.EmailDeliveryDashboard.COL_ManagerName))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.EmailDeliveryDashboard.COL_ManagerName);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entEmailDeliveryDashboard.ManagerName = pSqlReader.GetString(iIndex);
                }


            }
            return entEmailDeliveryDashboard;
        }

        /// <summary>
        /// Find Email Delivery Dashboard
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<EmailDeliveryDashboard> FindEmailDeliveryDashboard(Search pEntSearch)
        {
            EmailTemplateDAM entEmailTemplateAdpt = new EmailTemplateDAM();
            EmailTemplate entEmailTemplate = new EmailTemplate();
            EmailDeliveryDashboard entEmailDeliveryDashboard = null;
            List<EmailDeliveryDashboard> entListEmailDeliveryDashboard = null;
            _sqlObject = new SQLObject();

            _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.EmailDeliveryDashboard.PROC_FIND_EMAIL_DELIVERY_DASHBOARD, _sqlConnection);
            entEmailDeliveryDashboard = new EmailDeliveryDashboard();
            entListEmailDeliveryDashboard = new List<EmailDeliveryDashboard>();

            if (pEntSearch.ListRange != null)
            {
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntSearch.ListRange.PageIndex);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntSearch.ListRange.PageSize);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                _sqlcmd.Parameters.Add(_sqlpara);
                if (!string.IsNullOrEmpty(pEntSearch.ListRange.RequestedById))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
            }

            if (pEntSearch.SearchObject != null && pEntSearch.SearchObject.Count > 0)
            {
                entEmailDeliveryDashboard = new EmailDeliveryDashboard();
                entEmailDeliveryDashboard = (EmailDeliveryDashboard)pEntSearch.SearchObject[0];

                if (!string.IsNullOrEmpty(entEmailDeliveryDashboard.DeliveryApprovalStatus))
                {
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_DELIVERY_APPROVAL_STATUS, entEmailDeliveryDashboard.DeliveryApprovalStatus);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (DateTime.MinValue.CompareTo(entEmailDeliveryDashboard.DateTimeSet) < 0)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_FROM_DATE, entEmailDeliveryDashboard.DateTimeSet);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntSearch.SearchObject.Count > 1)
                {
                    entEmailDeliveryDashboard = new EmailDeliveryDashboard();
                    entEmailDeliveryDashboard = (EmailDeliveryDashboard)pEntSearch.SearchObject[1];
                    if (DateTime.MinValue.CompareTo(entEmailDeliveryDashboard.DateTimeSet) < 0)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_TO_DATE, entEmailDeliveryDashboard.DateTimeSet);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
            }
            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entEmailDeliveryDashboard = FillObject(_sqlreader, true, _sqlObject);

                    //if (entEmailDeliveryDashboard != null)
                    //{
                    //    //-- Get EmailTemplate(Only Active) and its Languages (only Language Approved)
                    //    entEmailTemplate.ClientId = pEntSearch.ClientId;
                    //    entEmailTemplate.ID = entEmailDeliveryDashboard.EmailTemplateID;
                    //    entEmailTemplate.ApprovalStatus = EmailTemplate.EmailApprovalStatus.Approved;
                    //    entEmailTemplate.IsActive = true;

                    //    entEmailDeliveryDashboard.EmailTemplate = entEmailTemplateAdpt.GetEmailTemplateById(entEmailTemplate);

                    entListEmailDeliveryDashboard.Add(entEmailDeliveryDashboard);
                    //}
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
            if (entListEmailDeliveryDashboard != null && entListEmailDeliveryDashboard.Count > 0)
            {

                foreach (EmailDeliveryDashboard entEmailDeliveryDashboardinner in entListEmailDeliveryDashboard)
                {
                    //-- Get EmailTemplate(Only Active) and its Languages (only Language Approved)
                    entEmailTemplate.ClientId = pEntSearch.ClientId;
                    entEmailTemplate.ID = entEmailDeliveryDashboardinner.EmailTemplateID;
                    entEmailTemplate.ApprovalStatus = EmailTemplate.EmailApprovalStatus.Approved;
                    entEmailTemplate.IsActive = true;
                    entEmailDeliveryDashboardinner.EmailTemplate = entEmailTemplateAdpt.GetEmailTemplateById(entEmailTemplate);
                }
            }
            return entListEmailDeliveryDashboard;
        }

        /// <summary>
        /// Find Email Delivery Dashboard Schedular
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<EmailDeliveryDashboard> FindEmailDeliveryDashboardSchedular(Search pEntSearch)
        {
            EmailTemplateDAM entEmailTemplateAdpt = new EmailTemplateDAM();
            EmailTemplate entEmailTemplate = new EmailTemplate();
            EmailDeliveryDashboard entEmailDeliveryDashboard = null;
            List<EmailDeliveryDashboard> entListEmailDeliveryDashboard = null;
            _sqlObject = new SQLObject();

            _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.EmailDeliveryDashboard.PROC_FIND_EMAIL_DELIVERY_DASHBOARD_SCHEDULAR, _sqlConnection);
            entEmailDeliveryDashboard = new EmailDeliveryDashboard();
            entListEmailDeliveryDashboard = new List<EmailDeliveryDashboard>();

            if (pEntSearch.ListRange != null)
            {
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntSearch.ListRange.PageIndex);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntSearch.ListRange.PageSize);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                _sqlcmd.Parameters.Add(_sqlpara);
                if (!string.IsNullOrEmpty(pEntSearch.ListRange.RequestedById))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
            }
            if (pEntSearch.SearchObject != null && pEntSearch.SearchObject.Count > 0)
            {
                entEmailDeliveryDashboard = new EmailDeliveryDashboard();
                entEmailDeliveryDashboard = (EmailDeliveryDashboard)pEntSearch.SearchObject[0];

                if (!string.IsNullOrEmpty(entEmailDeliveryDashboard.DeliveryApprovalStatus))
                {
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_DELIVERY_APPROVAL_STATUS, entEmailDeliveryDashboard.DeliveryApprovalStatus);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (DateTime.MinValue.CompareTo(entEmailDeliveryDashboard.DateTimeSet) < 0)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_FROM_DATE, entEmailDeliveryDashboard.DateTimeSet);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (entEmailDeliveryDashboard.IsImmediate)
                {
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_IS_IMMEDIATE_FOR_SCHEDULAR, entEmailDeliveryDashboard.IsImmediate);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntSearch.SearchObject.Count > 1)
                {
                    entEmailDeliveryDashboard = new EmailDeliveryDashboard();
                    entEmailDeliveryDashboard = (EmailDeliveryDashboard)pEntSearch.SearchObject[1];
                    if (DateTime.MinValue.CompareTo(entEmailDeliveryDashboard.DateTimeSet) < 0)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_TO_DATE, entEmailDeliveryDashboard.DateTimeSet);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
            }
            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entEmailDeliveryDashboard = FillObject(_sqlreader, true, _sqlObject);

                    if (entEmailDeliveryDashboard != null)
                    {
                        //-- Get EmailTemplate(Only Active) and its Languages (only Language Approved)
                        entEmailTemplate.ClientId = pEntSearch.ClientId;
                        entEmailTemplate.ID = entEmailDeliveryDashboard.EmailTemplateID;
                        entEmailTemplate.ApprovalStatus = EmailTemplate.EmailApprovalStatus.Approved;
                        entEmailTemplate.IsActive = true;

                        entEmailDeliveryDashboard.EmailTemplate = entEmailTemplateAdpt.GetEmailTemplateById(entEmailTemplate);

                        entListEmailDeliveryDashboard.Add(entEmailDeliveryDashboard);
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
            return entListEmailDeliveryDashboard;
        }

        /// <summary>
        /// Get EmailDeliveryDashboard Details By Id
        /// </summary>
        /// <returns>List of EmailDeliveryDashboard Object</returns>
        public EmailDeliveryDashboard GetEmailDeliveryDashboardById(EmailDeliveryDashboard pEntEmailDeliveryDashboard)
        {
            EmailDeliveryDashboard entEmailDeliveryDashboard = null;
            EmailTemplateDAM entEmailTemplateAdpt = new EmailTemplateDAM();
            EmailTemplate entEmailTemplate = new EmailTemplate();
            _sqlObject = new SQLObject();

            _strConnString = _sqlObject.GetClientDBConnString(pEntEmailDeliveryDashboard.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.EmailDeliveryDashboard.PROC_GET_EMAIL_DELIVERY_DASHBOARD, _sqlConnection);
            _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_EMAIL_DELIVERY_INSTANCE_ID, pEntEmailDeliveryDashboard.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            entEmailDeliveryDashboard = new EmailDeliveryDashboard();
            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    entEmailDeliveryDashboard = FillObject(_sqlreader, false, _sqlObject);
                }
                if (entEmailDeliveryDashboard != null)
                {
                    //-- Get EmailTemplate(Only Active) and its Languages (only Language Approved)
                    entEmailTemplate.ClientId = pEntEmailDeliveryDashboard.ClientId;
                    entEmailTemplate.ID = entEmailDeliveryDashboard.EmailTemplateID;
                    entEmailTemplate.ApprovalStatus = EmailTemplate.EmailApprovalStatus.Approved;
                    entEmailTemplate.IsActive = true;

                    entEmailDeliveryDashboard.EmailTemplate = entEmailTemplateAdpt.GetEmailTemplateById(entEmailTemplate);

                    if (!string.IsNullOrEmpty(entEmailDeliveryDashboard.AttachmentPathList))//Add Bharat: 22-Feb-2016
                    {
                        string strSubject = entEmailDeliveryDashboard.AttachmentPathList;

                        if (strSubject.Contains(","))
                        {
                            string[] strAttachPath = strSubject.Split(',');
                            int iCount = 0;
                            for (; strAttachPath.Length > iCount; iCount++)
                            {
                                var webClient = new WebClient();
                                byte[] totalBytes = webClient.DownloadData(strAttachPath[iCount]);
                                Stream sRetStream = Stream.Null;
                                sRetStream = new MemoryStream(totalBytes);
                                System.Net.Mail.Attachment attachement = new System.Net.Mail.Attachment(sRetStream, strAttachPath[iCount], "");
                                List<System.Net.Mail.Attachment> attachList = new List<System.Net.Mail.Attachment>();
                                attachList.Add(attachement);
                                if (iCount == 0)
                                {
                                    entEmailDeliveryDashboard.AttachmentPathList = strAttachPath[iCount];
                                    //entEmailDeliveryDashboard.AttachmentPathList = GetAttachmentsPath(attachList);
                                }
                                else
                                {
                                    entEmailDeliveryDashboard.AttachmentPathList = strAttachPath[0] + "," + strAttachPath[iCount];
                                    //entEmailDeliveryDashboard.AttachmentPathList = entEmailDeliveryDashboard.AttachmentPathList +"," + GetAttachmentsPath(attachList);
                                }
                            }
                        }
                        else
                        {
                            var webClient = new WebClient();
                            byte[] totalBytes = webClient.DownloadData(entEmailDeliveryDashboard.AttachmentPathList);

                            if (totalBytes.Length > 0 && totalBytes.Length > 500001)
                            {
                                string strAttachURL = string.Empty;
                                try
                                {

                                    //string strLast = strSubject.Substring(strSubject.LastIndexOf('/') + 1).Replace("_", " ");
                                    //strSubject = strLast.Remove(strLast.LastIndexOf(' '));
                                    //strAttachURL = "<hr/><br/><br/><a href='" + entEmailDeliveryDashboard.AttachmentPathList + "' target='_blank'>Click here to download " + strSubject + "</a>";
                                    strAttachURL = "<hr/><br/><br/><a href='" + entEmailDeliveryDashboard.AttachmentPathList + "' target='_blank'>Click here to download</a>";
                                }
                                catch
                                {
                                    strAttachURL = "<hr/><br/><br/><a href='" + entEmailDeliveryDashboard.AttachmentPathList + "' target='_blank'>Click here to download</a>";
                                }
                                entEmailDeliveryDashboard.EmailTemplate.EmailTemplateLanguage[0].EmailBodyText += strAttachURL.ToString().Trim();
                            }
                            else
                            {
                                Stream sRetStream = Stream.Null;
                                sRetStream = new MemoryStream(totalBytes);
                                System.Net.Mail.Attachment attachement = new System.Net.Mail.Attachment(sRetStream, strSubject, "");
                                List<System.Net.Mail.Attachment> attachList = new List<System.Net.Mail.Attachment>();
                                attachList.Add(attachement);
                                entEmailDeliveryDashboard.AttachmentPathList = entEmailDeliveryDashboard.AttachmentPathList;
                                entEmailDeliveryDashboard.AttachmentPathList = GetAttachmentsPath(attachList);
                            }
                        }
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
            return entEmailDeliveryDashboard;
        }
        /// <summary>
        /// Get Attachments Path
        /// </summary>
        /// <param name="pListAttachments"></param>
        /// <returns></returns>
        public string GetAttachmentsPath(List<Attachment> pListAttachments)
        {
            string strAttachments = string.Empty;
            try
            {
                if (pListAttachments != null)
                {
                    if (pListAttachments.Count > 0)
                    {
                        foreach (Attachment attachment in pListAttachments)
                        {
                            if (!string.IsNullOrEmpty(attachment.Name))
                            {
                                if (!string.IsNullOrEmpty(strAttachments))
                                {
                                    strAttachments = strAttachments + "," + attachment.Name.ToString();
                                }
                                else
                                {
                                    strAttachments = attachment.Name.ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return strAttachments;
        }
        /// <summary>
        /// To update/add new Email Template
        /// </summary>
        /// <param name="pEntEmailDeliveryDashboard"></param>        
        /// <returns>EmailDeliveryDashboard object</returns>
        public EmailDeliveryDashboard AddUpdateEmailDeliveryDashboard(EmailDeliveryDashboard pEntEmailDeliveryDashboard)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            int iRowsAffected = -1;
            _sqlcmd.CommandText = Schema.EmailDeliveryDashboard.PROC_UPDATE_EMAIL_DELIVERY_DASHBOARD;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntEmailDeliveryDashboard.ClientId);
                if (string.IsNullOrEmpty(pEntEmailDeliveryDashboard.ID))
                {
                    //-- Re-assign new id
                    pEntEmailDeliveryDashboard.ID = YPLMS.Services.IDGenerator.GetUniqueKey(8);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                }
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_EMAIL_DELIVERY_INSTANCE_ID, pEntEmailDeliveryDashboard.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_DISTRIBUTION_LIST_ID, pEntEmailDeliveryDashboard.DistributionListId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_EMAIL_DELIVERY_TITLE, pEntEmailDeliveryDashboard.EmailDeliveryTitle);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_Email_Template_ID, pEntEmailDeliveryDashboard.EmailTemplateID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntEmailDeliveryDashboard.ApprovalDate) < 0)
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_APPROVALDATE, pEntEmailDeliveryDashboard.ApprovalDate);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_APPROVALDATE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_APPROVED_BY_ID, pEntEmailDeliveryDashboard.ApprovedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntEmailDeliveryDashboard.ToList))
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_TO_LIST, pEntEmailDeliveryDashboard.ToList);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_TO_LIST, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntEmailDeliveryDashboard.BCCList))
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_BCC_LIST, pEntEmailDeliveryDashboard.BCCList);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_BCC_LIST, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntEmailDeliveryDashboard.CCList))
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_CC_LIST, pEntEmailDeliveryDashboard.CCList);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_CC_LIST, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntEmailDeliveryDashboard.DateTimeSet) < 0)
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_DATETIME_SET, pEntEmailDeliveryDashboard.DateTimeSet);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_DATETIME_SET, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntEmailDeliveryDashboard.EndDate) < 0)
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_END_DATE, pEntEmailDeliveryDashboard.EndDate);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_END_DATE, null);
                _sqlcmd.Parameters.Add(_sqlpara);


                if (!String.IsNullOrEmpty(pEntEmailDeliveryDashboard.DeliveryApprovalStatus))
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_DELIVERY_APPROVAL_STATUS, pEntEmailDeliveryDashboard.DeliveryApprovalStatus);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_DELIVERY_APPROVAL_STATUS, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_IS_ALL_LANGUAGES, pEntEmailDeliveryDashboard.IsAllLanguages);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_IS_BCC_MANAGER, pEntEmailDeliveryDashboard.IsBCCManager);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_IS_CCMANAGER, pEntEmailDeliveryDashboard.IsCCManager);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_IS_IMMEDIATE, pEntEmailDeliveryDashboard.IsImmediate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_IS_MONTHLY, pEntEmailDeliveryDashboard.IsMonthly);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_IS_ONETIME, pEntEmailDeliveryDashboard.IsOneTime);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_IS_PERSONALIZED, pEntEmailDeliveryDashboard.IsPersonalized);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_IS_RECURRENCE_APPROVAL_REQUIRED, pEntEmailDeliveryDashboard.IsRecurrenceApprovalRequired);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_IS_SITE_DEFAULT_LANGUAGE, pEntEmailDeliveryDashboard.IsSiteDefaultLanguage);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_IS_USER_PREFERRED_LANGUAGE, pEntEmailDeliveryDashboard.IsUserPreferredLanguage);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_IS_WEEKLY, pEntEmailDeliveryDashboard.IsWeekly);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_IS_DAILY, pEntEmailDeliveryDashboard.IsDaily);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntEmailDeliveryDashboard.OneTimeDateSet) < 0)
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_ONETIME_DATESET, pEntEmailDeliveryDashboard.OneTimeDateSet);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_ONETIME_DATESET, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntEmailDeliveryDashboard.Recurrence != 0)
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_RECURRENCE, pEntEmailDeliveryDashboard.Recurrence);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_RECURRENCE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_ACTIVITY_ID, pEntEmailDeliveryDashboard.ActivityId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_ASSIGNMENT_ID, pEntEmailDeliveryDashboard.AssignmentId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_ATTACHMENTPATHLIST, pEntEmailDeliveryDashboard.AttachmentPathList);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_TASK_ID, pEntEmailDeliveryDashboard.TaskId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntEmailDeliveryDashboard.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntEmailDeliveryDashboard.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntEmailDeliveryDashboard.LearnerId))
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_LEARNER_ID, pEntEmailDeliveryDashboard.LearnerId);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_LEARNER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntEmailDeliveryDashboard.AssignmentMode.ToString()))
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_ASSIGNMENTMODE, pEntEmailDeliveryDashboard.AssignmentMode.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_ASSIGNMENTMODE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                // added by Gitanjali 26.08.2010

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_IsDistributionToManager, pEntEmailDeliveryDashboard.IsDistributionToManager);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntEmailDeliveryDashboard.ManagerEmailId))
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_ManagerEmailId, pEntEmailDeliveryDashboard.ManagerEmailId);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_ManagerEmailId, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntEmailDeliveryDashboard.ManagerName))
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_ManagerName, pEntEmailDeliveryDashboard.ManagerName);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_ManagerName, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                //added by Gitanjali 27.12.2010
                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_IS_WEEKDAYS_ONLY, pEntEmailDeliveryDashboard.IsWeekdaysOnly);
                _sqlcmd.Parameters.Add(_sqlpara);

                //added for dynamic assignment email 
                if (!string.IsNullOrEmpty(pEntEmailDeliveryDashboard.RuleId))
                {
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_RULEID, pEntEmailDeliveryDashboard.RuleId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntEmailDeliveryDashboard.AssignmentTypeID))
                {
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_ASSIGNMENTTYPEID, pEntEmailDeliveryDashboard.AssignmentTypeID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                //added for dynamic assignment email 
                if (pEntEmailDeliveryDashboard.IsDynamicAssignment)
                {
                    _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_DYNAMICASSIGNMENT, pEntEmailDeliveryDashboard.IsDynamicAssignment);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntEmailDeliveryDashboard;
        }

        /// <summary>
        /// Delete EmailDeliveryDashboard
        /// </summary>
        /// <param name="pEntEmailDeliveryDashboard"></param>
        /// <returns>EmailDeliveryDashboard Object</returns>
        public EmailDeliveryDashboard DeleteEmailDeliveryDashboard(EmailDeliveryDashboard pEntEmailDeliveryDashboard)
        {
            EmailDeliveryDashboard entEmailDeliveryDashboard = new EmailDeliveryDashboard();
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.EmailDeliveryDashboard.PROC_DELETE_EMAIL_DELIVERY_DASHBOARD;
            _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_EMAIL_DELIVERY_INSTANCE_ID, pEntEmailDeliveryDashboard.ID);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntEmailDeliveryDashboard.ClientId);
                int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntEmailDeliveryDashboard;
        }

        /// <summary>
        /// Check selected name exist
        /// </summary>
        /// <param name="pEntAssetLib"></param>
        /// <returns>EmailDeliveryDashboard Object</returns>
        public EmailDeliveryDashboard CheckExistByName(EmailDeliveryDashboard pEntEmailDeliveryDashboard)
        {
            EmailDeliveryDashboard entEmailDeliveryDashboard = new EmailDeliveryDashboard();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntEmailDeliveryDashboard.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.EmailDeliveryDashboard.PROC_EMAIL_DELIVERY_DASHBOARD_CHECK_BY_NAME, sqlConnection);

                _sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_EMAIL_DELIVERY_TITLE, pEntEmailDeliveryDashboard.EmailDeliveryTitle);
                _sqlcmd.Parameters.Add(_sqlpara);

                Object obj = null;
                obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
                if (obj != null)
                {
                    entEmailDeliveryDashboard.ID = Convert.ToString(obj);
                }
                else
                {
                    entEmailDeliveryDashboard = null;
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entEmailDeliveryDashboard;
        }
        public EmailDeliveryDashboard GetEmailDeliveryDashboardPendingApproval(EmailDeliveryDashboard pEntEmailDeliveryDashboard)
        {
            EmailDeliveryDashboard entEmailDeliveryDashboard = null;
            EmailTemplateDAM entEmailTemplateAdpt = new EmailTemplateDAM();
            EmailTemplate entEmailTemplate = new EmailTemplate();
            _sqlObject = new SQLObject();

            _strConnString = _sqlObject.GetClientDBConnString(pEntEmailDeliveryDashboard.ClientId);
            _sqlConnection = new SqlConnection(_strConnString);
            _sqlcmd = new SqlCommand(Schema.EmailDeliveryDashboard.PROC_GET_EMAIL_DELIVERY_DASHBOARD_PENDING_APPROVAL, _sqlConnection);
            //_sqlpara = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_EMAIL_DELIVERY_INSTANCE_ID, pEntEmailDeliveryDashboard.ID);
            //_sqlcmd.Parameters.Add(_sqlpara);
            entEmailDeliveryDashboard = new EmailDeliveryDashboard();
            try
            {
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    entEmailDeliveryDashboard = FillObject(_sqlreader, false, _sqlObject);
                }
                if (entEmailDeliveryDashboard != null)
                {
                    //-- Get EmailTemplate(Only Active) and its Languages (only Language Approved)
                    entEmailTemplate.ClientId = pEntEmailDeliveryDashboard.ClientId;
                    entEmailTemplate.ID = entEmailDeliveryDashboard.EmailTemplateID;
                    entEmailTemplate.ApprovalStatus = EmailTemplate.EmailApprovalStatus.Approved;
                    entEmailTemplate.IsActive = true;

                    entEmailDeliveryDashboard.EmailTemplate = entEmailTemplateAdpt.GetEmailTemplateById(entEmailTemplate);

                    if (!string.IsNullOrEmpty(entEmailDeliveryDashboard.AttachmentPathList))//Add Bharat: 22-Feb-2016
                    {
                        string strSubject = entEmailDeliveryDashboard.AttachmentPathList;

                        if (strSubject.Contains(","))
                        {
                            string[] strAttachPath = strSubject.Split(',');
                            int iCount = 0;
                            for (; strAttachPath.Length > iCount; iCount++)
                            {
                                var webClient = new WebClient();
                                byte[] totalBytes = webClient.DownloadData(strAttachPath[iCount]);
                                Stream sRetStream = Stream.Null;
                                sRetStream = new MemoryStream(totalBytes);
                                System.Net.Mail.Attachment attachement = new System.Net.Mail.Attachment(sRetStream, strAttachPath[iCount], "");
                                List<System.Net.Mail.Attachment> attachList = new List<System.Net.Mail.Attachment>();
                                attachList.Add(attachement);
                                if (iCount == 0)
                                {
                                    entEmailDeliveryDashboard.AttachmentPathList = strAttachPath[iCount];
                                    //entEmailDeliveryDashboard.AttachmentPathList = GetAttachmentsPath(attachList);
                                }
                                else
                                {
                                    entEmailDeliveryDashboard.AttachmentPathList = strAttachPath[0] + "," + strAttachPath[iCount];
                                    //entEmailDeliveryDashboard.AttachmentPathList = entEmailDeliveryDashboard.AttachmentPathList +"," + GetAttachmentsPath(attachList);
                                }
                            }
                        }
                        else
                        {
                            var webClient = new WebClient();
                            byte[] totalBytes = webClient.DownloadData(entEmailDeliveryDashboard.AttachmentPathList);

                            if (totalBytes.Length > 0 && totalBytes.Length > 500001)
                            {
                                string strAttachURL = string.Empty;
                                try
                                {

                                    //string strLast = strSubject.Substring(strSubject.LastIndexOf('/') + 1).Replace("_", " ");
                                    //strSubject = strLast.Remove(strLast.LastIndexOf(' '));
                                    //strAttachURL = "<hr/><br/><br/><a href='" + entEmailDeliveryDashboard.AttachmentPathList + "' target='_blank'>Click here to download " + strSubject + "</a>";
                                    strAttachURL = "<hr/><br/><br/><a href='" + entEmailDeliveryDashboard.AttachmentPathList + "' target='_blank'>Click here to download</a>";
                                }
                                catch
                                {
                                    strAttachURL = "<hr/><br/><br/><a href='" + entEmailDeliveryDashboard.AttachmentPathList + "' target='_blank'>Click here to download</a>";
                                }
                                entEmailDeliveryDashboard.EmailTemplate.EmailTemplateLanguage[0].EmailBodyText += strAttachURL.ToString().Trim();
                            }
                            else
                            {
                                Stream sRetStream = Stream.Null;
                                sRetStream = new MemoryStream(totalBytes);
                                System.Net.Mail.Attachment attachement = new System.Net.Mail.Attachment(sRetStream, strSubject, "");
                                List<System.Net.Mail.Attachment> attachList = new List<System.Net.Mail.Attachment>();
                                attachList.Add(attachement);
                                entEmailDeliveryDashboard.AttachmentPathList = entEmailDeliveryDashboard.AttachmentPathList;
                                entEmailDeliveryDashboard.AttachmentPathList = GetAttachmentsPath(attachList);
                            }
                        }
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
            return entEmailDeliveryDashboard;
        }
        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntEmailDeliveryDashboard"></param>
        /// <returns></returns>
        public EmailDeliveryDashboard Get(EmailDeliveryDashboard pEntEmailDeliveryDashboard)
        {
            return GetEmailDeliveryDashboardById(pEntEmailDeliveryDashboard);
        }
        /// <summary>
        /// Update EmailDeliveryDashboard
        /// </summary>
        /// <param name="pEntEmailDeliveryDashboard"></param>
        /// <returns></returns>
        public EmailDeliveryDashboard Update(EmailDeliveryDashboard pEntEmailDeliveryDashboard)
        {
            return AddUpdateEmailDeliveryDashboard(pEntEmailDeliveryDashboard);
        }

        public Learner FillUserObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            Learner _entLearner = new Learner();
            UserAdminRole entUserAdminRole = new UserAdminRole();
            //UserCustomFieldValue entUserCustomFieldValue;
            int index;
            if (pSqlReader.HasRows)
            {
                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.ID = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_FIRST_NAME);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.FirstName = pSqlReader.GetString(index);
                else
                    _entLearner.FirstName = "";

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_MIDDLE_NAME);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.MiddleName = pSqlReader.GetString(index);

                if (string.IsNullOrEmpty(_entLearner.MiddleName))
                    _entLearner.MiddleName = "";

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_LAST_NAME);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.LastName = pSqlReader.GetString(index);
                else
                    _entLearner.LastName = "";

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_EMAIL_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.EmailID = pSqlReader.GetString(index);

                if (string.IsNullOrEmpty(_entLearner.EmailID))
                    _entLearner.EmailID = "";

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_PREFERRED_DATE_FORMAT))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_PREFERRED_DATE_FORMAT);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.PreferredDateFormat = pSqlReader.GetString(index);

                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_PREFERREDTIMEZONE))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_PREFERREDTIMEZONE);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.PreferredTimeZone = pSqlReader.GetString(index);

                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_PHONE_NO))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_PHONE_NO);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.PhoneNo = pSqlReader.GetString(index);
                }

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_NAME_ALIAS);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.UserNameAlias = pSqlReader.GetString(index);
                else
                    _entLearner.UserNameAlias = "";

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_PASSWORD);
                if (!pSqlReader.IsDBNull(index))
                {
                    _entLearner.UserPassword = pSqlReader.GetString(index);
                    _entLearner.UserPassword = EncryptionManager.Decrypt(_entLearner.UserPassword);
                }
                index = pSqlReader.GetOrdinal(Schema.Learner.COL_ADDRESS);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.Address = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_DATE_OF_BIRTH);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.DateOfBirth = pSqlReader.GetDateTime(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_DATE_OF_REGISTRATION);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.DateOfRegistration = pSqlReader.GetDateTime(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_DATE_OF_TERMINATION);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.DateOfTermination = pSqlReader.GetDateTime(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_TYPE_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.UserTypeId = pSqlReader.GetString(index);
                else
                    _entLearner.UserTypeId = "";

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_DEFAULT_LANGUAGE_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.DefaultLanguageId = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_DEFAULT_THEME_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.DefaultThemeID = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_GENDER);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.Gender = pSqlReader.GetBoolean(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_MANAGER_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.ManagerId = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_MANAGER_EMAIL);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.ManagerEmailId = pSqlReader.GetString(index);


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_MANAGER_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_MANAGER_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.ManagerName = pSqlReader.GetString(index);
                }

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_DATE_LAST_LOGIN);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.DateLastLogin = pSqlReader.GetDateTime(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_IS_ACTIVE);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.IsActive = pSqlReader.GetBoolean(index);

                index = pSqlReader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.ClientId = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.CreatedById = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.DateCreated = pSqlReader.GetDateTime(index);

                index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.LastModifiedById = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.LastModifiedDate = pSqlReader.GetDateTime(index);


                index = pSqlReader.GetOrdinal(Schema.Learner.COL_UNIT_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.UnitId = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_LEVEL_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.LevelId = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_DEFAULT_RV);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.DefaultRegionView = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_CURRENT_RV);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.CurrentRegionView = pSqlReader.GetString(index);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_IS_FIRST_LOGIN))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_IS_FIRST_LOGIN);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.IsFirstLogin = pSqlReader.GetBoolean(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_IS_PASSWORD_EXPIRED))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_IS_PASSWORD_EXPIRED);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.IsPasswordExpired = pSqlReader.GetBoolean(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_SCOPE))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_SCOPE);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.UserScope = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_DEFAULT_ORG))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_DEFAULT_ORG);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.UserDefaultOrg = pSqlReader.GetString(index);
                }
            }
            return _entLearner;
        }

        public List<Learner> GetDynamicAssignmentUserList(EmailDeliveryDashboard pentDashBoard)
        {
            List<Learner> lstLearners = new List<Learner>();
            SQLObject sqlObj = new SQLObject();
            SqlCommand sqlCmd = null;
            string _sqlConnextionString = string.Empty;
            SqlConnection sqlConn = null;
            SqlDataReader sqlReader = null;
            SqlParameter sqlparam = null;
            try
            {
                _sqlConnextionString = sqlObj.GetClientDBConnString(pentDashBoard.ClientId);
                sqlConn = new SqlConnection(_sqlConnextionString);
                sqlConn.Open();
                sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = Schema.EmailDeliveryDashboard.PROC_DYNAMICASSIGNMENTUSER_EMAIL;
                //PARA_RULEID
                sqlparam = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_RULEID, pentDashBoard.RuleId);
                sqlCmd.Parameters.Add(sqlparam);

                sqlparam = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_ACTIVITY_ID, pentDashBoard.ActivityId);
                sqlCmd.Parameters.Add(sqlparam);

                sqlparam = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_EMAIL_DELIVERY_INSTANCE_ID, pentDashBoard.ID);
                sqlCmd.Parameters.Add(sqlparam);

                if (!string.IsNullOrEmpty(pentDashBoard.FromRecipants))
                {
                    sqlparam = new SqlParameter(Schema.EmailDeliveryDashboard.PARA_ISFROM_VIEWRECEIPANTS, pentDashBoard.FromRecipants);
                    sqlCmd.Parameters.Add(sqlparam);
                }

                sqlReader = sqlObj.SqlDataReader(sqlCmd, true);
                while (sqlReader.Read())
                {
                    Learner entLearner = FillUserObject(sqlReader, false, sqlObj);
                    lstLearners.Add(entLearner);
                }
            }
            catch
            {
            }
            finally
            {
                if (sqlConn.State != ConnectionState.Closed)
                {
                    sqlConn.Close();
                    sqlConn.Dispose();
                }
                if (!sqlReader.IsClosed)
                    sqlReader.Close();

            }
            return lstLearners;
        }


        #endregion
    }
}
