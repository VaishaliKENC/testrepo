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
    public class BusinessRuleUsersAdaptor : IDataManager<BusinessRuleUsers>, IBusinessRuleUsersAdaptor<BusinessRuleUsers>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.User.LEARNER_ERROR;
        #endregion

        /// <summary>
        ///  To Fill BusinessRuleUsers object properties values from reader data 
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>BusinessRuleUsers Object</returns>
        public BusinessRuleUsers FillObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            BusinessRuleUsers entBusinessRuleUsers = new BusinessRuleUsers();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entBusinessRuleUsers.ID = pSqlReader.GetString(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.BusinessRuleUsers.COL_BUSINESS_RULE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.BusinessRuleUsers.COL_BUSINESS_RULE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.BusinessRuleId = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_NAME_ALIAS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_NAME_ALIAS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.UserNameAlias = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_FIRST_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_FIRST_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.FirstName = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_LAST_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_LAST_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.LastName = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_EMAIL_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_EMAIL_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.EmailID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_IS_ACTIVE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_IS_ACTIVE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.IsActive = pSqlReader.GetBoolean(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.BusinessRuleUsers.COL_PARAMETER_GROUP_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.BusinessRuleUsers.COL_PARAMETER_GROUP_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.ParameterGroupId = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.BusinessRuleUsers.COL_IS_INCLUDED))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.BusinessRuleUsers.COL_IS_INCLUDED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.IsIncluded = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.DateCreated = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.LastModifiedById = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.LastModifiedDate = pSqlReader.GetDateTime(iIndex);
                }


                #region NewUserFields


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.CreatedById = pSqlReader.GetString(iIndex);

                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_EMAIL_ID))
                {

                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_EMAIL_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.EmailID = pSqlReader.GetString(iIndex);
                }



                if (string.IsNullOrEmpty(entBusinessRuleUsers.EmailID))
                    entBusinessRuleUsers.EmailID = "";

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_PHONE_NO))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_PHONE_NO);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.PhoneNo = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_NAME_ALIAS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_NAME_ALIAS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.UserNameAlias = pSqlReader.GetString(iIndex);
                    else
                        entBusinessRuleUsers.UserNameAlias = "";
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_PASSWORD))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_PASSWORD);
                    if (!pSqlReader.IsDBNull(iIndex))
                    {
                        entBusinessRuleUsers.UserPassword = pSqlReader.GetString(iIndex);
                        entBusinessRuleUsers.UserPassword = EncryptionManager.Decrypt(entBusinessRuleUsers.UserPassword);
                    }
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_ADDRESS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_ADDRESS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.Address = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DATE_OF_BIRTH))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_DATE_OF_BIRTH);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.DateOfBirth = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DATE_OF_REGISTRATION))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_DATE_OF_REGISTRATION);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.DateOfRegistration = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DATE_OF_TERMINATION))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_DATE_OF_TERMINATION);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.DateOfTermination = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_TYPE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_TYPE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.UserTypeId = pSqlReader.GetString(iIndex);
                    else
                        entBusinessRuleUsers.UserTypeId = "";
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DEFAULT_LANGUAGE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_DEFAULT_LANGUAGE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.DefaultLanguageId = pSqlReader.GetString(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DEFAULT_THEME_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_DEFAULT_THEME_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.DefaultThemeID = pSqlReader.GetString(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_GENDER))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_GENDER);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.Gender = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_MANAGER_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_MANAGER_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.ManagerId = pSqlReader.GetString(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_MANAGER_EMAIL))
                {

                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_MANAGER_EMAIL);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.ManagerEmailId = pSqlReader.GetString(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_MANAGER_NAME))
                {

                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_MANAGER_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.ManagerName = pSqlReader.GetString(iIndex);
                }




                if (string.IsNullOrEmpty(entBusinessRuleUsers.ManagerName))
                    entBusinessRuleUsers.ManagerName = "";

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DATE_LAST_LOGIN))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_DATE_LAST_LOGIN);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.DateLastLogin = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_IS_ACTIVE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_IS_ACTIVE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.IsActive = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_UNIT_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_UNIT_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.UnitId = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_LEVEL_ID))
                {

                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_LEVEL_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.LevelId = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DEFAULT_RV))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_DEFAULT_RV);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.DefaultRegionView = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_CURRENT_RV))
                {

                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_CURRENT_RV);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.CurrentRegionView = pSqlReader.GetString(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_IS_FIRST_LOGIN))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_IS_FIRST_LOGIN);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.IsFirstLogin = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_IS_PASSWORD_EXPIRED))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_IS_PASSWORD_EXPIRED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.IsPasswordExpired = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_ORG_HIERARCHY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ORG_HIERARCHY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.UserOrgHierarchy = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_PREFERRED_DATE_FORMAT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_PREFERRED_DATE_FORMAT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entBusinessRuleUsers.PreferredDateFormat = pSqlReader.GetString(iIndex);
                }

                #endregion

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        _entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    entBusinessRuleUsers.ListRange = _entRange;
                }
            }
            return entBusinessRuleUsers;
        }

        /// <summary>
        /// Get Bussiness User Result
        /// </summary>
        /// <param name="pEntBusinessRuleUsers"></param>
        /// <returns></returns>
        public List<BusinessRuleUsers> GetBusinessRuleResult(BusinessRuleUsers pEntBusinessRuleUsers)
        {
            BusinessRuleUsers entBusinessRuleUsers = new BusinessRuleUsers();
            List<BusinessRuleUsers> entListBusinessRuleUsers = new List<BusinessRuleUsers>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand(Schema.BusinessRuleUsers.PROC_GET_BUSINESS_RULE_RESULT_USERS);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntBusinessRuleUsers.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;

                _sqlpara = new SqlParameter(Schema.BusinessRuleUsers.PARA_BUSINESS_RULE_ID, pEntBusinessRuleUsers.BusinessRuleId);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntBusinessRuleUsers.ListRange != null)
                {
                    if (pEntBusinessRuleUsers.ListRange.PageIndex > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntBusinessRuleUsers.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntBusinessRuleUsers.ListRange.PageSize > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntBusinessRuleUsers.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntBusinessRuleUsers.ListRange.SortExpression))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntBusinessRuleUsers.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntBusinessRuleUsers.ListRange.RequestedById))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntBusinessRuleUsers.ListRange.RequestedById);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, null);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entBusinessRuleUsers = FillObject(_sqlreader, true, _sqlObject);
                    entListBusinessRuleUsers.Add(entBusinessRuleUsers);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entListBusinessRuleUsers;
        }

        public List<BusinessRuleUsers> GetBusinessRuleMultiResult(BusinessRuleUsers pEntBusinessRuleUsers)
        {
            BusinessRuleUsers entBusinessRuleUsers = new BusinessRuleUsers();
            List<BusinessRuleUsers> entListBusinessRuleUsers = new List<BusinessRuleUsers>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand(Schema.BusinessRuleUsers.PROC_GET_MULTI_BUSINESS_RULE_RESULT_USERS);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntBusinessRuleUsers.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;

                _sqlpara = new SqlParameter(Schema.BusinessRuleUsers.PARA_BUSINESS_RULE_ID, pEntBusinessRuleUsers.BusinessRuleId);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntBusinessRuleUsers.ListRange != null)
                {
                    if (pEntBusinessRuleUsers.ListRange.PageIndex > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntBusinessRuleUsers.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntBusinessRuleUsers.ListRange.PageSize > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntBusinessRuleUsers.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntBusinessRuleUsers.ListRange.SortExpression))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntBusinessRuleUsers.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntBusinessRuleUsers.ListRange.RequestedById))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntBusinessRuleUsers.ListRange.RequestedById);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, null);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entBusinessRuleUsers = FillObject(_sqlreader, true, _sqlObject);
                    entListBusinessRuleUsers.Add(entBusinessRuleUsers);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entListBusinessRuleUsers;
        }
        // added by Gitanjali 10.08.2010 to get Business rule active user
        public List<BusinessRuleUsers> GetBusinessRuleActiveUsers(BusinessRuleUsers pEntBusinessRuleUsers)
        {
            BusinessRuleUsers entBusinessRuleUser = new BusinessRuleUsers();
            List<BusinessRuleUsers> lstBusinessRuleActiveUsers = new List<BusinessRuleUsers>();
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand(Schema.BusinessRuleUsers.PROC_GET_BUSINESS_RULE_ACTIVE_USERS);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntBusinessRuleUsers.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;

                _sqlpara = new SqlParameter(Schema.BusinessRuleUsers.PARA_BUSINESS_RULE_ID, pEntBusinessRuleUsers.BusinessRuleId);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entBusinessRuleUser = FillObject(_sqlreader, false, _sqlObject);
                    lstBusinessRuleActiveUsers.Add(entBusinessRuleUser);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return lstBusinessRuleActiveUsers;

        }

        #region Interface Methods
        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="pEntBusinessRuleUsers"></param>
        /// <returns></returns>
        public BusinessRuleUsers Get(BusinessRuleUsers pEntBusinessRuleUsers)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="pEntBusinessRuleUsers"></param>
        /// <returns></returns>
        public BusinessRuleUsers Update(BusinessRuleUsers pEntBusinessRuleUsers)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
