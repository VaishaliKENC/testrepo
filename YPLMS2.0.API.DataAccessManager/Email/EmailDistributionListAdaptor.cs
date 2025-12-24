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
    public class EmailDistributionListAdaptor : IDataManager<EmailDistributionList>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        UserCustomFieldValue entUserCustomFieldValue = null;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        SqlConnection _sqlcon = null;
        EmailDistributionList _entEmailDistributionList = null;
        EntityRange entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.EmailDistributionList.ERROR_MSG_ID;
        #endregion

        /// <summary>
        /// Get Email Distribution List
        /// </summary>
        /// <param name="pEntEmailDistributionList"></param>
        /// <returns></returns>
        public List<EmailDistributionList> GetEmailDistributionListLIST(EmailDistributionList pEntEmailDistributionList)
        {
            List<EmailDistributionList> entListEmailDistributionList = new List<EmailDistributionList>();
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntEmailDistributionList.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.Connection = sqlConnection;

                if (pEntEmailDistributionList.IsActive != null)
                {
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_IS_ACTIVE, pEntEmailDistributionList.IsActive);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntEmailDistributionList.ID))
                {
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_DISTRIBUTION_LIST_ID, pEntEmailDistributionList.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlcmd.CommandText = Schema.EmailDistributionList.PROC_GET_EMAIL_DISTRIBUTION_LIST;
                }
                else
                {
                    _sqlcmd.CommandText = Schema.EmailDistributionList.PROC_GET_ALL_DISTRIBUTION_LIST;
                    if (pEntEmailDistributionList.ListRange != null)
                    {
                        if (pEntEmailDistributionList.ListRange != null)
                            _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntEmailDistributionList.ListRange.PageIndex);
                        else
                            _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        if (pEntEmailDistributionList.ListRange != null)
                            _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntEmailDistributionList.ListRange.PageSize);
                        else
                            _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        if (pEntEmailDistributionList.ListRange.SortExpression != null)
                            _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntEmailDistributionList.ListRange.SortExpression);
                        else
                            _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (!string.IsNullOrEmpty(pEntEmailDistributionList.CreatedById))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntEmailDistributionList.CreatedById);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, null);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entEmailDistributionList = FillObject(_sqlreader, _sqlObject);
                    entListEmailDistributionList.Add(_entEmailDistributionList);
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
            return entListEmailDistributionList;
        }

        /// <summary>
        /// Search Email Distribution List
        /// </summary>
        /// <param name="pEntEmailDistributionList"></param>
        /// <returns></returns>
        public List<EmailDistributionList> SearchEmailDistributionListLIST(EmailDistributionList pEntEmailDistributionList)
        {
            List<EmailDistributionList> entListEmailDistributionList = new List<EmailDistributionList>();
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntEmailDistributionList.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.Connection = sqlConnection;
                _sqlcmd.CommandText = Schema.EmailDistributionList.PROC_SEARCH_EMAIL_TEMPLATE;
                if (!String.IsNullOrEmpty(pEntEmailDistributionList.RuleId))
                {
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_RULE_ID, pEntEmailDistributionList.RuleId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_RULE_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntEmailDistributionList.DistributionListTitle))
                {
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_KEYWORD, pEntEmailDistributionList.DistributionListTitle);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_KEYWORD, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntEmailDistributionList.ListRange != null)
                {
                    if (pEntEmailDistributionList.ListRange != null)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntEmailDistributionList.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntEmailDistributionList.ListRange != null)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntEmailDistributionList.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntEmailDistributionList.ListRange.SortExpression != null)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntEmailDistributionList.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entEmailDistributionList = FillObject(_sqlreader, _sqlObject);
                    entListEmailDistributionList.Add(_entEmailDistributionList);
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
            return entListEmailDistributionList;
        }

        /// <summary>
        /// Fill Object
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <param name="pSqlObject"></param>
        /// <returns></returns>
        private EmailDistributionList FillObject(SqlDataReader pSqlreader, SQLObject pSqlObject)
        {
            _entEmailDistributionList = new EmailDistributionList();
            int iIndex;
            if (pSqlreader.HasRows)
            {
                iIndex = pSqlreader.GetOrdinal(Schema.EmailDistributionList.COL_DISTRIBUTION_LIST_ID);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entEmailDistributionList.ID = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.EmailDistributionList.COL_DISTRIBUTION_LIST_TITLE);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entEmailDistributionList.DistributionListTitle = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.EmailDistributionList.COL_RULE_ID);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entEmailDistributionList.RuleId = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.EmailDistributionList.COL_IS_PRIVATE);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entEmailDistributionList.IsPrivate = pSqlreader.GetBoolean(iIndex);


                //In every proc can not have these columns So it check that it exist in it.
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.GroupRule.COL_RULE_NAME))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.GroupRule.COL_RULE_NAME);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entEmailDistributionList.RuleName = pSqlreader.GetString(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.EmailDistributionList.COL_IS_USED))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.EmailDistributionList.COL_IS_USED);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entEmailDistributionList.IsUsed = pSqlreader.GetBoolean(iIndex);
                }

                iIndex = pSqlreader.GetOrdinal(Schema.EmailDistributionList.COL_IS_ACTIVE);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entEmailDistributionList.IsActive = pSqlreader.GetBoolean(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entEmailDistributionList.CreatedById = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entEmailDistributionList.DateCreated = pSqlreader.GetDateTime(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entEmailDistributionList.LastModifiedById = pSqlreader.GetString(iIndex);

                iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                if (!pSqlreader.IsDBNull(iIndex))
                    _entEmailDistributionList.LastModifiedDate = pSqlreader.GetDateTime(iIndex);


                //In every proc can not have these columns So it check that it exist in it.
                if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Common.COL_TOTAL_COUNT))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlreader.IsDBNull(iIndex))
                    {
                        entRange = new EntityRange();
                        entRange.TotalRows = pSqlreader.GetInt32(iIndex);
                        _entEmailDistributionList.ListRange = entRange;

                    }
                }

            }
            return _entEmailDistributionList;
        }

        /// <summary>
        /// Add Email Distribution List
        /// </summary>
        /// <param name="pEntEmailDistributionList"></param>
        /// <returns></returns>
        public EmailDistributionList AddEmailDistributionList(EmailDistributionList pEntEmailDistributionList)
        {
            _entEmailDistributionList = new EmailDistributionList();
            _entEmailDistributionList = Update(pEntEmailDistributionList, Schema.Common.VAL_INSERT_MODE);
            return _entEmailDistributionList;
        }

        /// <summary>
        /// Edit Email Distribution List
        /// </summary>
        /// <param name="pEntEmailDistributionList"></param>
        /// <returns></returns>
        public EmailDistributionList EditEmailDistributionList(EmailDistributionList pEntEmailDistributionList)
        {
            _entEmailDistributionList = new EmailDistributionList();
            _entEmailDistributionList = Update(pEntEmailDistributionList, Schema.Common.VAL_UPDATE_MODE);
            return _entEmailDistributionList;
        }

        /// <summary>
        /// Delete Email Distribution List
        /// </summary>
        /// <param name="pEntEmailDistributionList"></param>
        /// <returns></returns>
        public EmailDistributionList DeleteEmailDistributionList(EmailDistributionList pEntEmailDistributionList)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.EmailDistributionList.PROC_DELETE_DISTRIBUTION_LIST;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntEmailDistributionList.ClientId);
                _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_DISTRIBUTION_LIST_ID, pEntEmailDistributionList.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                try
                {
                    //Delete PassCode Instance from database
                    int iDelStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                    if (iDelStatus < 0)
                        pEntEmailDistributionList = null;
                }
                catch (Exception expCommon)
                {
                    _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                    throw _expCustom;
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntEmailDistributionList;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="pEntEmailDistributionList"></param>
        /// <param name="pUpdateMode"></param>
        /// <returns></returns>
        private EmailDistributionList Update(EmailDistributionList pEntEmailDistributionList, string pUpdateMode)
        {
            int iRows = 0;
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.EmailDistributionList.PROC_UPDATE_DISTRIBUTION_LIST;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntEmailDistributionList.ClientId);
                if (pUpdateMode == Schema.Common.VAL_INSERT_MODE)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                    pEntEmailDistributionList.ID = YPLMS.Services.IDGenerator.GetStringGUID();
                }
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_DISTRIBUTION_LIST_ID, pEntEmailDistributionList.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailDistributionList.IsActive.ToString()))
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_IS_ACTIVE, pEntEmailDistributionList.IsActive);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_IS_ACTIVE, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailDistributionList.DistributionListTitle))
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_DISTRIBUTION_LIST_TITLE, pEntEmailDistributionList.DistributionListTitle);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_DISTRIBUTION_LIST_TITLE, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailDistributionList.RuleId))
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_RULE_ID, pEntEmailDistributionList.RuleId);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_RULE_ID, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_IS_PRIVATE, pEntEmailDistributionList.IsPrivate);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntEmailDistributionList.LastModifiedById))
                    _sqlpara = new SqlParameter(Schema.Common.COL_MODIFIED_BY, pEntEmailDistributionList.LastModifiedById);
                else
                    _sqlpara = new SqlParameter(Schema.Common.COL_MODIFIED_BY, System.DBNull.Value);
                _sqlcmd.Parameters.Add(_sqlpara);

                iRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntEmailDistributionList;
        }

        /// <summary>
        /// Get Email Distribution List By ID
        /// </summary>
        /// <param name="pEntEmailDistributionList"></param>
        /// <returns></returns>
        public EmailDistributionList GetEmailDistributionListByID(EmailDistributionList pEntEmailDistributionList)
        {
            List<EmailDistributionList> entListEmailDistributionList = new List<EmailDistributionList>();
            try
            {
                entListEmailDistributionList = GetEmailDistributionListLIST(pEntEmailDistributionList);
                if (entListEmailDistributionList != null && entListEmailDistributionList.Count > 0)
                    _entEmailDistributionList = entListEmailDistributionList[0];
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return _entEmailDistributionList;
        }

        /// <summary>
        /// Get Business Rule Users By Distribution List ID
        /// </summary>
        /// <param name="pEntEmailDistributionList"></param>
        /// <returns></returns>
        public EmailDistributionList GetBusinessRuleUsersByDistributionListID(EmailDistributionList pEntEmailDistributionList)
        {
            EmailDistributionList entEmailDL = new EmailDistributionList();
            _sqlObject = new SQLObject();
            try
            {
                entEmailDL = GetEmailDistributionListByID(pEntEmailDistributionList);
                pEntEmailDistributionList.IsActive = entEmailDL.IsActive;
                pEntEmailDistributionList.IsPrivate = entEmailDL.IsPrivate;
                pEntEmailDistributionList.RuleName = entEmailDL.RuleName;
                pEntEmailDistributionList.DistributionListTitle = entEmailDL.DistributionListTitle;

                List<UserCustomFieldValue> entListCustFieldVal = null;
                List<BusinessRuleUsers> entListBusinessRuleUsers = new List<BusinessRuleUsers>();
                BusinessRuleUsers entBusinessRuleUsers = new BusinessRuleUsers();
                BusinessRuleUsersAdaptor entBusinessRuleUsersAdpt = new BusinessRuleUsersAdaptor();
                _strConnString = _sqlObject.GetClientDBConnString(pEntEmailDistributionList.ClientId);

                _sqlcon = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.EmailDistributionList.PROC_GET_BUSINESS_RULE_USERS, _sqlcon);
                _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_DISTRIBUTION_LIST_ID, pEntEmailDistributionList.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntEmailDistributionList.ListRange != null && !string.IsNullOrEmpty(pEntEmailDistributionList.ListRange.RequestedById))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntEmailDistributionList.ListRange.RequestedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntEmailDistributionList.ListCount != null)
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_LIST_COUNT, pEntEmailDistributionList.ListCount);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_LIST_COUNT, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entBusinessRuleUsers = entBusinessRuleUsersAdpt.FillObject(_sqlreader, false, _sqlObject);
                    entListBusinessRuleUsers.Add(entBusinessRuleUsers);
                }

                if (entListBusinessRuleUsers.Count > 0)
                {
                    pEntEmailDistributionList.BusinessRuleUsers.AddRange(entListBusinessRuleUsers);
                }

                if (_sqlreader.NextResult())
                {
                    entListCustFieldVal = new List<UserCustomFieldValue>();
                    while (_sqlreader.Read())
                    {
                        entListCustFieldVal.Add(FillUserCustomFieldValue(_sqlreader, _sqlObject));
                    }
                    if (entListCustFieldVal.Count > 0)
                    {
                        for (int i = 0; i < pEntEmailDistributionList.BusinessRuleUsers.Count; i++)
                        {
                            List<UserCustomFieldValue> entListUserCustomFieldValueResult = entListCustFieldVal.FindAll(delegate (UserCustomFieldValue entBusinessRuleUserstoFind)
                            { return entBusinessRuleUserstoFind.SystemUserGUID == pEntEmailDistributionList.BusinessRuleUsers[i].ID; });
                            if (entListUserCustomFieldValueResult != null)
                            {
                                pEntEmailDistributionList.BusinessRuleUsers[i].UserCustomFieldValue.AddRange(entListUserCustomFieldValueResult);
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
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (_sqlcon != null && _sqlcon.State != ConnectionState.Closed)
                    _sqlcon.Close();
            }
            return pEntEmailDistributionList;
        }

        /// <summary>
        /// Get Users By Distribution List ID
        /// </summary>
        /// <param name="pEntEmailDistributionList"></param>
        /// <returns></returns>
        public EmailDistributionList GetUsersByDistributionListID(EmailDistributionList pEntEmailDistributionList)
        {
            EmailDistributionList entEmailDL = new EmailDistributionList();
            _sqlObject = new SQLObject();
            try
            {
                entEmailDL = GetEmailDistributionListByID(pEntEmailDistributionList);
                pEntEmailDistributionList.IsActive = entEmailDL.IsActive;
                pEntEmailDistributionList.IsPrivate = entEmailDL.IsPrivate;
                pEntEmailDistributionList.RuleName = entEmailDL.RuleName;
                pEntEmailDistributionList.DistributionListTitle = entEmailDL.DistributionListTitle;

                //List<UserCustomFieldValue> entListCustFieldVal = null;
                List<BusinessRuleUsers> entListBusinessRuleUsers = new List<BusinessRuleUsers>();
                BusinessRuleUsers entBusinessRuleUsers = new BusinessRuleUsers();
                BusinessRuleUsersAdaptor entBusinessRuleUsersAdpt = new BusinessRuleUsersAdaptor();
                _strConnString = _sqlObject.GetClientDBConnString(pEntEmailDistributionList.ClientId);

                _sqlcon = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.EmailDistributionList.PROC_GET_EMAIL_DISTRIBUTION_LIST_USERS, _sqlcon);
                _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_DISTRIBUTION_LIST_ID, pEntEmailDistributionList.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntEmailDistributionList.ListRange != null && !string.IsNullOrEmpty(pEntEmailDistributionList.ListRange.RequestedById))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntEmailDistributionList.ListRange.RequestedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntEmailDistributionList.ListCount != null)
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_LIST_COUNT, pEntEmailDistributionList.ListCount);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_LIST_COUNT, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entBusinessRuleUsers = entBusinessRuleUsersAdpt.FillObject(_sqlreader, false, _sqlObject);
                    entListBusinessRuleUsers.Add(entBusinessRuleUsers);
                }

                if (entListBusinessRuleUsers.Count > 0)
                {
                    pEntEmailDistributionList.BusinessRuleUsers.AddRange(entListBusinessRuleUsers);
                }

                //if (_sqlreader.NextResult())
                //{
                //    entListCustFieldVal = new List<UserCustomFieldValue>();
                //    while (_sqlreader.Read())
                //    {
                //        entListCustFieldVal.Add(FillUserCustomFieldValue(_sqlreader, _sqlObject));
                //    }
                //    if (entListCustFieldVal.Count > 0)
                //    {
                //        for (int i = 0; i < pEntEmailDistributionList.BusinessRuleUsers.Count; i++)
                //        {
                //            List<UserCustomFieldValue> entListUserCustomFieldValueResult = entListCustFieldVal.FindAll(delegate(UserCustomFieldValue entBusinessRuleUserstoFind)
                //            { return entBusinessRuleUserstoFind.SystemUserGUID == pEntEmailDistributionList.BusinessRuleUsers[i].ID; });
                //            if (entListUserCustomFieldValueResult != null)
                //            {
                //                pEntEmailDistributionList.BusinessRuleUsers[i].UserCustomFieldValue.AddRange(entListUserCustomFieldValueResult);
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (_sqlcon != null && _sqlcon.State != ConnectionState.Closed)
                    _sqlcon.Close();
            }
            return pEntEmailDistributionList;
        }

        /// <summary>
        /// Fill User Custom Field Value
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <param name="pSqlObject"></param>
        /// <returns></returns>
        private UserCustomFieldValue FillUserCustomFieldValue(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            int index = 0;
            entUserCustomFieldValue = new UserCustomFieldValue();
            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.UserCustomFieldValue.COL_CUSTOM_FIELD_ITEM_ID))
            {
                index = pSqlReader.GetOrdinal(Schema.UserCustomFieldValue.COL_CUSTOM_FIELD_ITEM_ID);
                if (!pSqlReader.IsDBNull(index))
                    entUserCustomFieldValue.CustomFieldItemId = pSqlReader.GetString(index);
            }
            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_ID))
            {
                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                if (!pSqlReader.IsDBNull(index))
                    entUserCustomFieldValue.SystemUserGUID = pSqlReader.GetString(index);
            }
            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.UserCustomFieldValue.COL_ENTERED_VALUE))
            {
                index = pSqlReader.GetOrdinal(Schema.UserCustomFieldValue.COL_ENTERED_VALUE);
                if (!pSqlReader.IsDBNull(index))
                    entUserCustomFieldValue.EnteredValue = pSqlReader.GetString(index);
            }
            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY))
            {
                index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                if (!pSqlReader.IsDBNull(index))
                    entUserCustomFieldValue.CreatedById = pSqlReader.GetString(index);
            }
            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_ON))
            {
                index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                if (!pSqlReader.IsDBNull(index))
                    entUserCustomFieldValue.DateCreated = pSqlReader.GetDateTime(index);
            }
            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
            {
                index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                if (!pSqlReader.IsDBNull(index))
                    entUserCustomFieldValue.LastModifiedById = pSqlReader.GetString(index);
            }
            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
            {
                index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                if (!pSqlReader.IsDBNull(index))
                    entUserCustomFieldValue.LastModifiedDate = pSqlReader.GetDateTime(index);
            }
            if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.CustomField.COL_CUSTOM_FIELD_ID))
            {
                index = pSqlReader.GetOrdinal(Schema.CustomField.COL_CUSTOM_FIELD_ID);
                if (!pSqlReader.IsDBNull(index))
                    entUserCustomFieldValue.CustomFieldId = pSqlReader.GetString(index);
            }
            return entUserCustomFieldValue;
        }

        /// <summary>
        /// Get Email Distribution By Name
        /// </summary>
        /// <param name="pEntEmailDistributionList"></param>
        /// <returns></returns>
        public EmailDistributionList GetEmailDistributionByName(EmailDistributionList pEntEmailDistributionList)
        {
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            EmailDistributionList entEmailDistributionList = new EmailDistributionList();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntEmailDistributionList.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.EmailDistributionList.PROC_GET_EMAIL_DISTRIBUTION_BY_NAME;
                _sqlcmd.Connection = sqlConnection;
                _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_DISTRIBUTION_LIST_TITLE, pEntEmailDistributionList.DistributionListTitle);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entEmailDistributionList = FillObject(_sqlreader, _sqlObject);
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
            return entEmailDistributionList;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntEmailDistributionList"></param>
        /// <returns></returns>
        public EmailDistributionList Get(EmailDistributionList pEntEmailDistributionList)
        {
            return GetEmailDistributionListByID(pEntEmailDistributionList);
        }
        /// <summary>
        /// Update EmailDistributionList
        /// </summary>
        /// <param name="pEntEmailDistributionList"></param>
        /// <returns></returns>
        public EmailDistributionList Update(EmailDistributionList pEntEmailDistributionList)
        {
            return EditEmailDistributionList(pEntEmailDistributionList);
        }
        #endregion
    }

}
