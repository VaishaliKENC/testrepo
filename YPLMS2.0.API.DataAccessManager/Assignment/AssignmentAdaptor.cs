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
    public class AssignmentAdaptor : IDataManager<Assignment>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        Assignment _entAssignment = null;
        EntityRange entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.Assignment.ERROR_MSG_ID;
        #endregion

        /// <summary>
        /// Get Assignment List
        /// </summary>
        /// <param name="pEntAssignment"></param>
        /// <returns></returns>
        public List<Assignment> GetAssignmentList(Assignment pEntAssignment)
        {
            _sqlObject = new SQLObject();
            List<Assignment> entListAssignment = new List<Assignment>();
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.Connection = sqlConnection;

                _sqlcmd.CommandText = Schema.Assignment.PROC_GET_ALL_ASSIGNMENT;
                if (!String.IsNullOrEmpty(pEntAssignment.ID))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntAssignment.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!String.IsNullOrEmpty(pEntAssignment.RuleId))
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_RULE_ID, pEntAssignment.RuleId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_RULE_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!String.IsNullOrEmpty(pEntAssignment.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntAssignment.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntAssignment.ListRange != null)
                {
                    if (pEntAssignment.ListRange.PageIndex > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntAssignment.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntAssignment.ListRange.PageSize > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntAssignment.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntAssignment.ListRange.SortExpression))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntAssignment.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entAssignment = FillObject(_sqlreader, _sqlObject);
                    entListAssignment.Add(_entAssignment);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListAssignment;
        }

        //Tanveer : 01/04/2011
        /// <summary>
        /// Get Assignment List
        /// </summary>
        /// <param name="pEntAssignment"></param>
        /// <returns></returns>
        public List<Assignment> GetAssignmentListForEmailTemplate(Assignment pEntAssignment)
        {
            _sqlObject = new SQLObject();
            List<Assignment> entListAssignment = new List<Assignment>();
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.Connection = sqlConnection;

                _sqlcmd.CommandText = Schema.Assignment.PROC_GET_ALL_ASSIGNMENT_FOREMAILTEMPLATE;
                if (!String.IsNullOrEmpty(pEntAssignment.ID))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntAssignment.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!String.IsNullOrEmpty(pEntAssignment.RuleId))
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_RULE_ID, pEntAssignment.RuleId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_RULE_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntAssignment.ListRange != null)
                {
                    if (pEntAssignment.ListRange.PageIndex > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntAssignment.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntAssignment.ListRange.PageSize > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntAssignment.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntAssignment.ListRange.SortExpression))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntAssignment.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entAssignment = FillObject(_sqlreader, _sqlObject);
                    entListAssignment.Add(_entAssignment);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListAssignment;
        }


        /// <summary>
        /// Get Assignment by Rule or Activity ID
        /// </summary>
        /// <param name="pEntAssignment"></param>
        /// <returns></returns>
        public List<Assignment> GetAssignmentByRuleIDandActivityID(Assignment pEntAssignment, string NameOrID)
        {
            _sqlObject = new SQLObject();
            List<Assignment> entListAssignment = new List<Assignment>();
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.Connection = sqlConnection;
                if (NameOrID == "NAME")
                {
                    _sqlcmd.CommandText = Schema.Assignment.PROC_GET_ASSIGNMENT_BY_NAME;
                }
                else
                {
                    _sqlcmd.CommandText = Schema.Assignment.PROC_GET_ASSIGNMENT;
                }

                if (!String.IsNullOrEmpty(pEntAssignment.ID))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntAssignment.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!String.IsNullOrEmpty(pEntAssignment.RuleId))
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_RULE_ID, pEntAssignment.RuleId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_RULE_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!String.IsNullOrEmpty(pEntAssignment.AssignmentName))
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_ASSIGNMENT_NAME, pEntAssignment.AssignmentName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_ASSIGNMENT_NAME, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entAssignment = FillObject(_sqlreader, _sqlObject);
                    entListAssignment.Add(_entAssignment);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListAssignment;
        }

        /// <summary>
        /// Fill Object by Reader
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <returns></returns>
        private Assignment FillObject(SqlDataReader pSqlreader, SQLObject pSqlObject)
        {
            _entAssignment = new Assignment();
            int iIndex;
            if (pSqlreader.HasRows)
            {

                iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_ASSIGNMENT_DESC);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entAssignment.AssignmentDescription = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_ASSIGNMENT_NAME);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entAssignment.AssignmentName = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_ID);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entAssignment.ID = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_ACTIVITY_TYPE);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entAssignment.ActivityType = (ActivityContentType)Enum.Parse(typeof(ActivityContentType), Convert.ToString(pSqlreader.GetString(iIndex)));

                iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_COMPLETION_CONDITION_ID);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entAssignment.CompletionConditionId = (ActivityCompletionCondition)Enum.Parse(typeof(ActivityCompletionCondition), pSqlreader.GetString(iIndex));

                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_ASSIGNMENTMODE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_ASSIGNMENTMODE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.AssignmentMode = (Assignment.AssignMode)Enum.Parse(typeof(Assignment.AssignMode), Convert.ToString(pSqlreader.GetString(iIndex)));
                }

                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_TOTAL_ASSIGNMENTS))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_TOTAL_ASSIGNMENTS);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.TotalAssignments = Convert.ToInt32(pSqlreader.GetValue(iIndex));
                }

                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_TOTAL_COMPLETIONS))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_TOTAL_COMPLETIONS);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.TotalCompletions = Convert.ToInt32(pSqlreader.GetValue(iIndex));
                }

                //--New Property
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_CREATION_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_CREATION_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsAssignmentBasedOnCreationDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_HIRE_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_HIRE_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsAssignmentBasedOnHireDate = pSqlreader.GetBoolean(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_CUURENT_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_CUURENT_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsAssignmentBasedOnCurrentDate = pSqlreader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_NEWHIRECURRENT_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_NEWHIRECURRENT_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsNewHireAssignmentBasedOnCurrentDate = pSqlreader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_ACTIVITY_OR_CATEGORY))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_ACTIVITY_OR_CATEGORY);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsActivityCategory = pSqlreader.GetBoolean(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_CATEGORY_ID))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_CATEGORY_ID);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.CategoryId = pSqlreader.GetString(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_ASSIGN_AFTER_DAYS_OF))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_ASSIGN_AFTER_DAYS_OF);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.AssignAfterDaysOf = pSqlreader.GetInt32(iIndex);
                }
                iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_DATE_OF_ASSIGNMENT);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entAssignment.AssignmentDateSet = pSqlreader.GetDateTime(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_NO_DUE_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_NO_DUE_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsNoDueDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_ASSIGN_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_ASSIGN_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsDueBasedOnAssignDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_CREATION_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_CREATION_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsDueBasedOnCreationDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_HIRE_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_HIRE_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsDueBasedOnHireDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_START_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_START_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsDueBasedOnStartDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_DUE_AFTER_DAYS_OF))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_DUE_AFTER_DAYS_OF);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.DueAfterDaysOf = pSqlreader.GetInt32(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_DUE_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_DUE_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.DueDateSet = pSqlreader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_NO_EXPIRY_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_NO_EXPIRY_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsNoExpiryDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_ASSIGN_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_ASSIGN_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsExpiryBasedOnAssignDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_DUE_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_DUE_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsExpiryBasedOnDueDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_START_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_START_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsExpiryBasedOnStartDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_EXPIRY_AFTER_DAYS_OF))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_EXPIRY_AFTER_DAYS_OF);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.ExpireAfterDaysOf = pSqlreader.GetInt32(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_EXPIRY_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_EXPIRY_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.ExpiryDateSet = pSqlreader.GetDateTime(iIndex);
                }

                //------- CODE fOR NEW HIRE - STARTS
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_NEWHIRE_FROM_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_NEWHIRE_FROM_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.NewHireFromDate = pSqlreader.GetDateTime(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_NEWHIRE_TO_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_NEWHIRE_TO_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.NewHireToDate = pSqlreader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_IS_NEWHIRE_ASSIGNMENT_BASED_ON_CREATION_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_IS_NEWHIRE_ASSIGNMENT_BASED_ON_CREATION_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsNewHireAssignmentBasedOnCreationDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_IS_NEWHIRE_ASSIGNMENT_BASED_ON_HIRE_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_IS_NEWHIRE_ASSIGNMENT_BASED_ON_HIRE_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsNewHireAssignmentBasedOnHireDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_NEWHIRE_ASSIGN_AFTER_DAYS_OF))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_NEWHIRE_ASSIGN_AFTER_DAYS_OF);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.NewHireAssignAfterDaysOf = pSqlreader.GetInt32(iIndex);
                }
                iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_NEWHIRE_ASSIGNMENT_DATE_SET);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entAssignment.NewHireAssignmentDateSet = pSqlreader.GetDateTime(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_IS_NO_NEWHIRE_DUE_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_IS_NO_NEWHIRE_DUE_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsNoNewHireDueDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_IS_NEWHIRE_DUE_BASED_ON_ASSIGN_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_IS_NEWHIRE_DUE_BASED_ON_ASSIGN_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsNewHireDueBasedOnAssignDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_IS_NEWHIRE_DUE_BASED_ON_CREATION_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_IS_NEWHIRE_DUE_BASED_ON_CREATION_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsNewHireDueBasedOnCreationDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_IS_NEWHIRE_DUE_BASED_ON_HIRE_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_IS_NEWHIRE_DUE_BASED_ON_HIRE_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsNewHireDueBasedOnHireDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_IS_NEWHIRE_DUE_BASED_ON_START_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_IS_NEWHIRE_DUE_BASED_ON_START_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsNewHireDueBasedOnStartDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_NEWHIRE_DUE_AFTER_DAYS_OF))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_NEWHIRE_DUE_AFTER_DAYS_OF);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.NewHireDueAfterDaysOf = pSqlreader.GetInt32(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_NEWHIRE_DUE_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_NEWHIRE_DUE_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.NewHireDueDateSet = pSqlreader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_IS_NO_NEWHIRE_EXPIRY_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_IS_NO_NEWHIRE_EXPIRY_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsNoNewHireExpiryDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_IS_NEWHIRE_EXPIRY_BASED_ON_ASSIGN_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_IS_NEWHIRE_EXPIRY_BASED_ON_ASSIGN_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsNewHireExpiryBasedOnAssignDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_IS_NEWHIRE_EXPIRY_BASED_ON_DUE_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_IS_NEWHIRE_EXPIRY_BASED_ON_DUE_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsNewHireExpiryBasedOnDueDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_IS_NEWHIRE_EXPIRY_BASED_ON_START_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_IS_NEWHIRE_EXPIRY_BASED_ON_START_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsNewHireExpiryBasedOnStartDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_NEWHIRE_EXPIRY_AFTER_DAYS_OF))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_NEWHIRE_EXPIRY_AFTER_DAYS_OF);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.NewHireExpireAfterDaysOf = pSqlreader.GetInt32(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Assignment.COL_NEWHIRE_EXPIRY_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_NEWHIRE_EXPIRY_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.NewHireExpiryDateSet = pSqlreader.GetDateTime(iIndex);
                }

                // For Reassignment - assignment date
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_REASSIGN_BASED_ON_ASSIGN_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_REASSIGN_BASED_ON_ASSIGN_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsReAssignmentBasedOnAssignmentDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_REASSIGN_BASED_ON_ASSIGN_COMPL_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_REASSIGN_BASED_ON_ASSIGN_COMPL_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsReAssignmentBasedOnAssignmentCompletionDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_REASSIGN_AFTER_DAYS_OF))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_REASSIGN_AFTER_DAYS_OF);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.ReAssignAfterDaysOf = pSqlreader.GetInt32(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_REASSIGNMENT_DATE_SET))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_REASSIGNMENT_DATE_SET);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.ReAssignmentDateSet = pSqlreader.GetDateTime(iIndex);
                }

                // Reassign due date
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_REASSIGN_NO_DUE_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_REASSIGN_NO_DUE_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsReassignNoDueDate = pSqlreader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_REASSIGN_DUE_BASED_ON_ASSIGN_COMPL_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_REASSIGN_DUE_BASED_ON_ASSIGN_COMPL_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsReassignDueBasedOnAssignmentCompletionDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_REASSIGN_DUE_BASED_ON_REASSIGN_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_REASSIGN_DUE_BASED_ON_REASSIGN_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsReassignDueBasedOnReassignmentDate = pSqlreader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_REASSIGN_DUE_AFTER_DAYS_OF))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_REASSIGN_DUE_AFTER_DAYS_OF);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.ReassignDueAfterDaysOf = pSqlreader.GetInt32(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_REASSIGN_DUE_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_REASSIGN_DUE_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.ReassignDueDateSet = pSqlreader.GetDateTime(iIndex);
                }

                // Reassign expiry date
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_REASSIGN_NO_EXPIRY_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_REASSIGN_NO_EXPIRY_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsReassignNoExpiryDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_ASSIGN_COMPL_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_ASSIGN_COMPL_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsReassignExpiryBasedOnAssignmentCompletionDate = pSqlreader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsReassignExpiryBasedOnReassignmentDate = pSqlreader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DUE_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DUE_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.IsReassignExpiryBasedOnReassignmentDueDate = pSqlreader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_REASSIGN_EXPIRY_AFTER_DAYS_OF))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_REASSIGN_EXPIRY_AFTER_DAYS_OF);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.ReassignExpireAfterDaysOf = pSqlreader.GetInt32(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_REASSIGN_EXPIRY_DATE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_REASSIGN_EXPIRY_DATE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.ReassignExpiryDateSet = pSqlreader.GetDateTime(iIndex);
                }

                // other 
                iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_OVERRIDE_PREVIOUS_ASSIGNMENTS);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entAssignment.OverridePreviousAssignments = pSqlreader.GetBoolean(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_RULE_ID);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entAssignment.RuleId = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_SEND_EMAIL);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entAssignment.SendEmail = pSqlreader.GetBoolean(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_SEND_EMAIL_TYPE);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entAssignment.SendEmailType = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_EMAIL_TEMPLATE_ID);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entAssignment.EmailTemplateId = pSqlreader.GetString(iIndex);


                iIndex = pSqlreader.GetOrdinal(Schema.Assignment.COL_IS_ACTIVE);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entAssignment.IsActive = pSqlreader.GetBoolean(iIndex);





                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Common.COL_MODIFIED_BY_NAME))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_BY_NAME);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.ModifiedByName = pSqlreader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Common.COL_CREATED_BY_NAME))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entAssignment.CreatedByName = pSqlreader.GetString(iIndex);
                }

                iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entAssignment.LastModifiedById = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entAssignment.LastModifiedDate = pSqlreader.GetDateTime(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entAssignment.CreatedById = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entAssignment.DateCreated = pSqlreader.GetDateTime(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Common.COL_TOTAL_COUNT))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (pSqlreader.GetInt32(iIndex) > 0)
                    {
                        entRange = new EntityRange();
                        entRange.TotalRows = pSqlreader.GetInt32(iIndex);
                        _entAssignment.ListRange = entRange;
                    }
                }
            }
            return _entAssignment;
        }

        /// <summary>
        /// Add Assignment
        /// </summary>
        /// <param name="pEntAssignment"></param>
        /// <returns></returns>
        public Assignment AddAssignment(Assignment pEntAssignment)
        {
            _entAssignment = new Assignment();
            _entAssignment = Update(pEntAssignment, Schema.Common.VAL_INSERT_MODE);
            return _entAssignment;
        }

        /// <summary>
        /// Edit Assignment
        /// </summary>
        /// <param name="pEntAssignment"></param>
        /// <returns></returns>
        public Assignment EditAssignment(Assignment pEntAssignment)
        {
            _entAssignment = new Assignment();
            _entAssignment = Update(pEntAssignment, Schema.Common.VAL_UPDATE_MODE);
            return _entAssignment;
        }

        /// <summary>
        /// To deactivate dyn assignments.
        /// </summary>
        /// <param name="pEntListAssignment"></param>
        /// <returns></returns>
        public List<Assignment> BulkDeactivateAssignment(List<Assignment> pEntListAssignment)
        {
            _sqlObject = new SQLObject();
            int iRows = 0;
            bool bIsActive;
            string strParameter = string.Empty;
            Assignment entAssignment = new Assignment();
            List<Assignment> entListAssignment = new List<Assignment>();
            EntityRange entRange = new EntityRange();
            try
            {
                bIsActive = pEntListAssignment[0].IsActive;
                foreach (Assignment objAssignment in pEntListAssignment)
                {

                    if (string.IsNullOrEmpty(_strConnString))
                        _strConnString = _sqlObject.GetClientDBConnString(objAssignment.ClientId);

                    if (string.IsNullOrEmpty(strParameter))
                        strParameter = objAssignment.RuleId + "##" + objAssignment.ID;
                    else
                        strParameter = strParameter + "@@" + objAssignment.RuleId + "##" + objAssignment.ID;
                    entListAssignment.Add(objAssignment);
                }
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Assignment.PROC_DEACTIVATE_ASSIGMENT;
                _sqlpara = new SqlParameter(Schema.Assignment.PARA_ACTIVITYNRULEID, strParameter);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assignment.PARA_IS_ACTIVE, bIsActive);
                _sqlcmd.Parameters.Add(_sqlpara);


                iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

                entRange.TotalRows = iRows;
                entListAssignment[0].ListRange = entRange;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListAssignment;
        }

        /// <summary>
        /// Bulk Delete Assignments
        /// </summary>
        /// <param name="pEntListAssignment"></param>
        /// <returns></returns>
        public List<Assignment> BulkDeleteAssignment(List<Assignment> pEntListAssignment)
        {
            _sqlObject = new SQLObject();
            int iRows = 0;
            string strParameter = string.Empty;
            Assignment entAssignment = new Assignment();
            List<Assignment> entListAssignment = new List<Assignment>();
            EntityRange entRange = new EntityRange();
            try
            {
                foreach (Assignment objAssignment in pEntListAssignment)
                {
                    if (string.IsNullOrEmpty(_strConnString))
                        _strConnString = _sqlObject.GetClientDBConnString(objAssignment.ClientId);

                    if (string.IsNullOrEmpty(strParameter))
                        strParameter = objAssignment.RuleId + "##" + objAssignment.ID;
                    else
                        strParameter = strParameter + "@@" + objAssignment.RuleId + "##" + objAssignment.ID;
                    entListAssignment.Add(objAssignment);
                }
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Assignment.PROC_DELETE_MULTIPLE_ASSIGMENTS;
                _sqlpara = new SqlParameter(Schema.Assignment.PARA_ACTIVITYNRULEID, strParameter);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

                entRange.TotalRows = iRows;
                entListAssignment[0].ListRange = entRange;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListAssignment;
        }

        /// <summary>
        /// Update or Add Assignment
        /// </summary>
        /// <param name="pEntAssignment"></param>
        /// <param name="pUpdateMode"></param>
        /// <returns></returns>
        private Assignment Update(Assignment pEntAssignment, string pUpdateMode)
        {
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Assignment.PROC_UPDATE_ASSIGNMENT;
            SqlCommand sqlCommand = new SqlCommand();
            DataSet ds = new DataSet();
            List<BaseEntity> entListBase = new List<BaseEntity>();
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssignment.ClientId);
                if (pUpdateMode == Schema.Common.VAL_INSERT_MODE)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, pUpdateMode);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntAssignment.RuleId))
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_RULE_ID, pEntAssignment.RuleId);
                else
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_RULE_ID, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);


                if (!string.IsNullOrEmpty(pEntAssignment.ID))
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntAssignment.ID);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntAssignment.AssignmentName))
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_ASSIGNMENT_NAME, pEntAssignment.AssignmentName);
                else
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_ASSIGNMENT_NAME, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntAssignment.AssignmentDescription))
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_ASSIGNMENT_DESC, pEntAssignment.AssignmentDescription);
                else
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_ASSIGNMENT_DESC, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntAssignment.ActivityType.ToString()))
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_ACTIVITY_TYPE, pEntAssignment.ActivityType.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_ACTIVITY_TYPE, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                //New For isactive param 
                _sqlpara = new SqlParameter(Schema.Assignment.PARA_IS_ACTIVE, pEntAssignment.IsActive);
                _sqlcmd.Parameters.Add(_sqlpara);
                //End 



                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNMENT_MODE_FOR_OVERRIDE, pEntAssignment.AssignmentModeForOverride.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assignment.PARA_COMPLETION_CONDITION_ID, pEntAssignment.CompletionConditionId.ToString());
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assignment.PARA_OVERRIDE_PREVIOUS_ASSIGNMENTS, pEntAssignment.OverridePreviousAssignments);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_ASSIGNMENT_BASED_ON_HIRE_DATE, pEntAssignment.IsAssignmentBasedOnHireDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_ASSIGNMENT_BASED_ON_CREATION_DATE, pEntAssignment.IsAssignmentBasedOnCreationDate);
                _sqlcmd.Parameters.Add(_sqlpara);


                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_ASSIGNMENT_BASED_ON_CURRENT_DATE, pEntAssignment.IsAssignmentBasedOnCurrentDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_ASSIGNMENT_BASED_ON_NEWHIRECURRENT_DATE, pEntAssignment.IsNewHireAssignmentBasedOnCurrentDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_ACTIVITY_OR_CATEGORY, pEntAssignment.IsActivityCategory);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntAssignment.CategoryId.ToString()))
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CATEGORYID, pEntAssignment.CategoryId.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CATEGORYID, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGN_AFTER_DAYS_OF, pEntAssignment.AssignAfterDaysOf);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntAssignment.AssignmentDateSet) < 0)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_DATE_OF_ASSIGNMENT, pEntAssignment.AssignmentDateSet);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_NO_DUE_DATE, pEntAssignment.IsNoDueDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_DUE_BASED_ON_ASSIGN_DATE, pEntAssignment.IsDueBasedOnAssignDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_DUE_BASED_ON_HIRE_DATE, pEntAssignment.IsDueBasedOnHireDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_DUE_BASED_ON_CREATION_DATE, pEntAssignment.IsDueBasedOnCreationDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_DUE_BASED_ON_START_DATE, pEntAssignment.IsDueBasedOnStartDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_DUE_AFTER_DAYS_OF, pEntAssignment.DueAfterDaysOf);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntAssignment.DueDateSet) < 0)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_DUE_DATE, pEntAssignment.DueDateSet);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_NO_EXPIRY_DATE, pEntAssignment.IsNoExpiryDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_EXPIRY_BASED_ON_ASSIGN_DATE, pEntAssignment.IsExpiryBasedOnAssignDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_EXPIRY_BASED_ON_START_DATE, pEntAssignment.IsExpiryBasedOnStartDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_EXPIRY_BASED_ON_DUE_DATE, pEntAssignment.IsExpiryBasedOnDueDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_EXPIRY_AFTER_DAYS_OF, pEntAssignment.ExpireAfterDaysOf);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntAssignment.ExpiryDateSet) < 0)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_EXPIRY_DATE, pEntAssignment.ExpiryDateSet);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                /// Code for New Hire -starts.
                if (DateTime.MinValue.CompareTo(pEntAssignment.NewHireFromDate) < 0)
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_NEWHIRE_FROM_DATE, pEntAssignment.NewHireFromDate);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (DateTime.MinValue.CompareTo(pEntAssignment.NewHireToDate) < 0)
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_NEWHIRE_TO_DATE, pEntAssignment.NewHireToDate);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlpara = new SqlParameter(Schema.Assignment.PARA_IS_NEWHIRE_ASSIGNMENT_BASED_ON_HIRE_DATE, pEntAssignment.IsNewHireAssignmentBasedOnHireDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assignment.PARA_IS_NEWHIRE_ASSIGNMENT_BASED_ON_CREATION_DATE, pEntAssignment.IsNewHireAssignmentBasedOnCreationDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assignment.PARA_NEWHIRE_ASSIGN_AFTER_DAYS_OF, pEntAssignment.NewHireAssignAfterDaysOf);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntAssignment.NewHireAssignmentDateSet) < 0)
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_NEWHIRE_ASSIGNMENT_DATE_SET, pEntAssignment.NewHireAssignmentDateSet);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlpara = new SqlParameter(Schema.Assignment.PARA_IS_NO_NEWHIRE_DUE_DATE, pEntAssignment.IsNoNewHireDueDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assignment.PARA_IS_NEWHIRE_DUE_BASED_ON_ASSIGN_DATE, pEntAssignment.IsNewHireDueBasedOnAssignDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assignment.PARA_IS_NEWHIRE_DUE_BASED_ON_HIRE_DATE, pEntAssignment.IsNewHireDueBasedOnHireDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assignment.PARA_IS_NEWHIRE_DUE_BASED_ON_CREATION_DATE, pEntAssignment.IsNewHireDueBasedOnCreationDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assignment.PARA_IS_NEWHIRE_DUE_BASED_ON_START_DATE, pEntAssignment.IsNewHireDueBasedOnStartDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assignment.PARA_NEWHIRE_DUE_AFTER_DAYS_OF, pEntAssignment.NewHireDueAfterDaysOf);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntAssignment.NewHireDueDateSet) < 0)
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_NEWHIRE_DUE_DATE, pEntAssignment.NewHireDueDateSet);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlpara = new SqlParameter(Schema.Assignment.PARA_IS_NO_NEWHIRE_EXPIRY_DATE, pEntAssignment.IsNoNewHireExpiryDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assignment.PARA_IS_NEWHIRE_EXPIRY_BASED_ON_ASSIGN_DATE, pEntAssignment.IsNewHireExpiryBasedOnAssignDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assignment.PARA_IS_NEWHIRE_EXPIRY_BASED_ON_START_DATE, pEntAssignment.IsNewHireExpiryBasedOnStartDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assignment.PARA_IS_NEWHIRE_EXPIRY_BASED_ON_DUE_DATE, pEntAssignment.IsNewHireExpiryBasedOnDueDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Assignment.PARA_NEWHIRE_EXPIRY_AFTER_DAYS_OF, pEntAssignment.NewHireExpireAfterDaysOf);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntAssignment.NewHireExpiryDateSet) < 0)
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_NEWHIRE_EXPIRY_DATE, pEntAssignment.NewHireExpiryDateSet);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                // Reassign assignment  date.
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_BASED_ON_ASSIGN_DATE, pEntAssignment.IsReAssignmentBasedOnAssignmentDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_BASED_ON_ASSIGN_COMPL_DATE, pEntAssignment.IsReAssignmentBasedOnAssignmentCompletionDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REASSIGN_AFTER_DAYS_OF, pEntAssignment.ReAssignAfterDaysOf);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntAssignment.ReAssignmentDateSet) < 0)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REASSIGNMENT_DATE_SET, pEntAssignment.ReAssignmentDateSet);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                // Reassign due date.
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_NO_DUE_DATE, pEntAssignment.IsReassignNoDueDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_DUE_BASED_ON_ASSIGN_COMPL_DATE, pEntAssignment.IsReassignDueBasedOnAssignmentCompletionDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_DUE_BASED_ON_REASSIGN_DATE, pEntAssignment.IsReassignDueBasedOnReassignmentDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REASSIGN_DUE_AFTER_DAYS_OF, pEntAssignment.ReassignDueAfterDaysOf);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntAssignment.ReassignDueDateSet) < 0)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REASSIGN_DUE_DATE, pEntAssignment.ReassignDueDateSet);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                // Reassign expiry date.
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_NO_EXPIRY_DATE, pEntAssignment.IsReassignNoExpiryDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_EXPIRY_BASED_ON_ASSIGN_COMPL_DATE, pEntAssignment.IsReassignExpiryBasedOnAssignmentCompletionDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DATE, pEntAssignment.IsReassignExpiryBasedOnReassignmentDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DUE_DATE, pEntAssignment.IsReassignExpiryBasedOnReassignmentDueDate);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REASSIGN_EXPIRY_AFTER_DAYS_OF, pEntAssignment.ReassignExpireAfterDaysOf);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntAssignment.ReassignExpiryDateSet) < 0)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REASSIGN_EXPIRY_DATE, pEntAssignment.ReassignExpiryDateSet);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                //New Properties
                if (!string.IsNullOrEmpty(pEntAssignment.SendEmail.ToString()))
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_SEND_EMAIL, pEntAssignment.SendEmail);
                else
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_SEND_EMAIL, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntAssignment.SendEmailType))
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_SEND_EMAIL_TYPE, pEntAssignment.SendEmailType);
                else
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_SEND_EMAIL_TYPE, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntAssignment.EmailTemplateId))
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_EMAIL_TEMPLATE_ID, pEntAssignment.EmailTemplateId);
                else
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_EMAIL_TEMPLATE_ID, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntAssignment.LastModifiedById))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntAssignment.LastModifiedById);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                /// Used in Update/edit case only. 
                if (!string.IsNullOrEmpty(pEntAssignment.PreviousRuleId))
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_PREVIOUS_RULE_ID, pEntAssignment.PreviousRuleId);
                else
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_PREVIOUS_RULE_ID, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);






                if (pEntAssignment.ListRange != null)
                {
                    if (!string.IsNullOrEmpty(pEntAssignment.ListRange.RequestedById))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntAssignment.ListRange.RequestedById);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlcmd.CommandTimeout = 0;
                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
            }
            return pEntAssignment;
        }

        /// <summary>
        /// Get Assignment by ID
        /// </summary>
        /// <param name="pEntAssignment"></param>
        /// <returns></returns>
        public Assignment GetAssignmentByID(Assignment pEntAssignment)
        {
            List<Assignment> entListAssignment = new List<Assignment>();
            try
            {
                entListAssignment = GetAssignmentByRuleIDandActivityID(pEntAssignment, "ID");
                if (entListAssignment != null && entListAssignment.Count > 0)
                    _entAssignment = entListAssignment[0];
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return _entAssignment;
        }

        /// <summary>
        /// Get Assignement By Name
        /// </summary>
        /// <param name="pEntAssignment"></param>
        /// <returns></returns>
        public Assignment GetAssignmentByName(Assignment pEntAssignment)
        {
            List<Assignment> entListAssignment = new List<Assignment>();
            try
            {
                entListAssignment = GetAssignmentByRuleIDandActivityID(pEntAssignment, "NAME");
                if (entListAssignment != null && entListAssignment.Count > 0)
                    _entAssignment = entListAssignment[0];
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return _entAssignment;
        }

        /// <summary>
        /// To Get list of Users for selected rule for assignment.
        /// </summary>
        /// <param name="pEntAssignment"></param>
        /// <returns></returns>
        public List<Learner> GetUserByRule(Assignment pEntAssignment)
        {
            _sqlObject = new SQLObject();
            List<Learner> entListUsers = new List<Learner>();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Assignment.PROC_GET_ROLE_USERS;
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_ID, pEntAssignment.RuleId);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntAssignment.ListRange != null && !string.IsNullOrEmpty(pEntAssignment.ListRange.RequestedById))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntAssignment.ListRange.RequestedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                while (_sqlreader.Read())
                {
                    entListUsers.Add(FillLearner(_sqlreader));
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
            return entListUsers;
        }

        /// <summary>
        /// to Fill learner object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns></returns>
        private Learner FillLearner(SqlDataReader pReader)
        {
            _sqlObject = new SQLObject();
            Learner entLeaner = new Learner();

            int index;
            if (pReader.HasRows)
            {
                index = pReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                if (!pReader.IsDBNull(index))
                    entLeaner.ID = pReader.GetString(index);

                index = pReader.GetOrdinal(Schema.Learner.COL_DATE_OF_REGISTRATION);
                if (!pReader.IsDBNull(index))
                    entLeaner.DateOfRegistration = pReader.GetDateTime(index);

                if (_sqlObject.ReaderHasColumn(pReader, Schema.Learner.COL_EMAIL_ID))
                {
                    index = pReader.GetOrdinal(Schema.Learner.COL_EMAIL_ID);
                    if (!pReader.IsDBNull(index))
                        entLeaner.EmailID = pReader.GetString(index);
                }
            }
            return entLeaner;
        }

        /// <summary>
        /// To Get list of Rules - not used for assignment aganist selected activity.
        /// </summary>
        /// <param name="pEntAssignment"></param>
        /// <returns></returns>
        /// 

        public List<GroupRule> GetRuleList(Assignment pEntAssignment)
        {
            _sqlObject = new SQLObject();
            List<GroupRule> entListRules = new List<GroupRule>();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Assignment.PROC_GET_RULES;
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntAssignment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntAssignment.CreatedById != null)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CREATED_BY_ID, pEntAssignment.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntAssignment.CategoryId != null)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CATEGORYID, pEntAssignment.CategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }


                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                while (_sqlreader.Read())
                {
                    entListRules.Add(FillRule(_sqlreader));
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
            return entListRules;
        }

        //public List<Assignment> GetRuleList(Assignment pEntAssignment)
        //{
        //    _sqlObject = new SQLObject();
        //    List<Assignment> entListRules = new List<Assignment>();
        //    SqlConnection sqlConnection = null;
        //    try
        //    {
        //        _strConnString = _sqlObject.GetClientDBConnString(pEntAssignment.ClientId);
        //        sqlConnection = new SqlConnection(_strConnString);
        //        _sqlcmd = new SqlCommand();
        //        _sqlcmd.CommandText = Schema.Assignment.PROC_GET_RULES;
        //        _sqlcmd.Connection = sqlConnection;
        //        _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntAssignment.ID);
        //        _sqlcmd.Parameters.Add(_sqlpara);

        //        if (pEntAssignment.CreatedById != null)
        //        {
        //            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CREATED_BY_ID, pEntAssignment.CreatedById);
        //            _sqlcmd.Parameters.Add(_sqlpara);
        //        }

        //        if (pEntAssignment.CategoryId != null)
        //        {
        //            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CATEGORYID, pEntAssignment.CategoryId);
        //            _sqlcmd.Parameters.Add(_sqlpara);
        //        }


        //        _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

        //        while (_sqlreader.Read())
        //        {
        //            entListRules.Add(FillRule(_sqlreader));
        //        }
        //    }
        //    catch (Exception expCommon)
        //    {
        //        _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
        //        throw _expCustom;
        //    }
        //    finally
        //    {
        //        if (_sqlreader != null && !_sqlreader.IsClosed)
        //            _sqlreader.Close();
        //        if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
        //    }
        //    return entListRules;
        //}

        /// <summary>
        /// Fill Rule object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns></returns>
        //private Assignment FillRule(SqlDataReader pReader)
        //{
        //    Assignment entRule = new Assignment();
        //    int index;
        //    if (pReader.HasRows)
        //    {
        //        index = pReader.GetOrdinal(Schema.GroupRule.COL_RULE_ID);
        //        if (!pReader.IsDBNull(index))
        //            entRule.ID = pReader.GetString(index);

        //        index = pReader.GetOrdinal(Schema.GroupRule.COL_RULE_NAME);
        //        if (!pReader.IsDBNull(index))
        //            entRule.RuleName = pReader.GetString(index);
        //    }
        //    return entRule;
        //}

        private GroupRule FillRule(SqlDataReader pReader)
        {
            GroupRule entRule = new GroupRule();
            int index;
            if (pReader.HasRows)
            {
                index = pReader.GetOrdinal(Schema.GroupRule.COL_RULE_ID);
                if (!pReader.IsDBNull(index))
                    entRule.ID = pReader.GetString(index);

                index = pReader.GetOrdinal(Schema.GroupRule.COL_RULE_NAME);
                if (!pReader.IsDBNull(index))
                    entRule.RuleName = pReader.GetString(index);
            }
            return entRule;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntAssignment"></param>
        /// <returns></returns>
        public Assignment Get(Assignment pEntAssignment)
        {
            return GetAssignmentByID(pEntAssignment);
        }
        /// <summary>
        /// Update Assignment
        /// </summary>
        /// <param name="pEntAssignment"></param>
        /// <returns></returns>
        public Assignment Update(Assignment pEntAssignment)
        {
            return EditAssignment(pEntAssignment);
        }
        #endregion
    }
}
