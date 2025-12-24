using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager.Content
{
    /// <summary>
    /// class CurriculumActivityDAM
    /// </summary>
    public class CurriculumActivityAdaptor : IDataManager<CurriculumActivity>
    {
        #region Declaration
        //string _strMessageId = Services.Messages.Client.ORG_LVL_DL_ERROR;
        string _strMessageId = string.Empty;
        string _strConnString = string.Empty;
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        SQLObject _sqlObject = null;
        #endregion

        /// <summary>
        /// Get CurriculumActivity By ID
        /// </summary>
        /// <param name="pEntCurriculumActivity"></param>
        /// <returns>CurriculumActivity Object</returns>
        public CurriculumActivity GetCurriculumActivityByID(CurriculumActivity pEntCurriculumActivity)
        {
            _sqlObject = new SQLObject();
            CurriculumActivity entCurriculumActivity = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntCurriculumActivity.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.CurriculumActivity.PROC_GET_CURRICULUM_ACTIVITY_MASTER_ID, sqlConnection);
                if (!string.IsNullOrEmpty(pEntCurriculumActivity.CurriculumId))
                {
                    _sqlpara = new SqlParameter(Schema.CurriculumActivity.PARA_CURRICULUM_ID, pEntCurriculumActivity.CurriculumId.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntCurriculumActivity.ActivityId))
                {
                    _sqlpara = new SqlParameter(Schema.CurriculumActivity.PARA_ACTIVITY_ID, pEntCurriculumActivity.ActivityId.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entCurriculumActivity = FillObject(_sqlreader);
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
            return entCurriculumActivity;
        }

        /// <summary>
        /// Fill reader object for Get All
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>CurriculumActivity Object</returns>
        private CurriculumActivity FillObjectForGetAll(SqlDataReader pReader, SQLObject pSqlObject)
        {
            CurriculumActivity entCurActivity = new CurriculumActivity();
            EntityRange entRange = new EntityRange();
            int iIndex;
            if (pReader.HasRows)
            {
                iIndex = pReader.GetOrdinal(Schema.CurriculumActivity.COL_CURRICULUM_ID);
                entCurActivity.CurriculumId = pReader.GetString(iIndex);
                iIndex = pReader.GetOrdinal(Schema.CurriculumActivity.COL_ACTIVITY_ID);
                entCurActivity.ActivityId = pReader.GetString(iIndex);
                entCurActivity.LanguageId = Convert.ToString(pReader[Schema.CurriculumActivity.COL_LANGUAGE_ID]);
                entCurActivity.ActivityName = Convert.ToString(pReader[Schema.CurriculumActivity.COL_ACTIVITY_NAME]);
                entCurActivity.OrignalActivityName = Convert.ToString(pReader[Schema.CurriculumActivity.COL_ORG_ACTIVITY_NAME]);
                entCurActivity.ActivityMessage = Convert.ToString(pReader[Schema.CurriculumActivity.COL_ACTIVITY_MESSAGE]);
                entCurActivity.ActivityCompletionConditionId = Convert.ToString(pReader[Schema.CurriculumActivity.COL_ACTIVITY_COMPLETION_CONDITION_ID]);
                entCurActivity.ActivityType = (ActivityContentType)Enum.Parse(typeof(ActivityContentType), Convert.ToString(pReader[Schema.CurriculumActivity.COL_ACTIVITY_TYPE]));
                entCurActivity.SortOrder = Convert.ToInt32(pReader[Schema.CurriculumActivity.COL_SORT_ORDER]);
                #region Commented by Rajendra - For IO - Not Needed
                //entCurActivity.CreatedById = Convert.ToString(pReader[Schema.Common.COL_CREATED_BY]);
                //entCurActivity.LastModifiedById = Convert.ToString(pReader[Schema.Common.COL_MODIFIED_BY]);
                //entCurActivity.LastModifiedDate = Convert.ToDateTime(pReader[Schema.Common.COL_MODIFIED_ON]);
                //entCurActivity.DateCreated = Convert.ToDateTime(pReader[Schema.Common.COL_CREATED_ON]);
                #endregion
                entCurActivity.IsPrintCertificate = Convert.ToBoolean(pReader[Schema.Curriculum.COL_IS_PRINT_CERTIFICATE]);
                if (pSqlObject.ReaderHasColumn(pReader, Schema.Curriculum.COL_STATUS))
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(pReader[Schema.Curriculum.COL_STATUS])))
                        entCurActivity.Status = (ActivityCompletionStatus)Enum.Parse(typeof(ActivityCompletionStatus), Convert.ToString(pReader[Schema.Curriculum.COL_STATUS]));
                }
                if (pSqlObject.ReaderHasColumn(pReader, Schema.ActivityAssignment.COL_ATTEMPT_COUNT))
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(pReader[Schema.ActivityAssignment.COL_ATTEMPT_COUNT])))
                        entCurActivity.AttemptCount = Convert.ToInt32(pReader[Schema.ActivityAssignment.COL_ATTEMPT_COUNT]);
                }

                entCurActivity.SectionID = Convert.ToString(pReader[Schema.CurriculumActivity.COL_SECTIONID]);

                //if (pSqlObject.ReaderHasColumn(pReader, Schema.CurriculumSectionLanguage.COL_CURRICULUM_SECTION_NAME))
                //{
                //    if (!string.IsNullOrEmpty(Convert.ToString(pReader[Schema.CurriculumSectionLanguage.COL_CURRICULUM_SECTION_NAME])))
                //        entCurActivity.SectionName = Convert.ToString(pReader[Schema.CurriculumSectionLanguage.COL_CURRICULUM_SECTION_NAME]);
                //}

                if (pSqlObject.ReaderHasColumn(pReader, Schema.Common.COL_TOTAL_COUNT))
                {
                    iIndex = pReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (pReader.GetInt32(iIndex) > 0)
                    {
                        entRange = new EntityRange();
                        entRange.TotalRows = pReader.GetInt32(iIndex);
                        entCurActivity.ListRange = entRange;
                    }
                }
            }
            return entCurActivity;
        }

        /// <summary>
        /// Edit CurriculumActivity
        /// </summary>
        /// <param name="pEntOrgLevel"></param>
        /// <returns>CurriculumActivity Object</returns>
        public CurriculumActivity EditCurriculumActivity(CurriculumActivity pEntCurriculumActivity)
        {
            try
            {
                pEntCurriculumActivity = UpdateCurriculumActivity(pEntCurriculumActivity, Schema.Common.VAL_UPDATE_MODE);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntCurriculumActivity;
        }

        /// <summary>
        /// Get All CurriculumActivity
        /// </summary>
        /// <param name="pEntOrgLevel"></param>
        /// <returns>List of CurriculumActivity Object</returns>
        public List<CurriculumActivity> GetAllCurriculumActivities(CurriculumActivity pEntCurriculumActivity)
        {
            _sqlObject = new SQLObject();
            List<CurriculumActivity> entListCurriculumActivitys = new List<CurriculumActivity>();
            CurriculumActivity entCurriculumActivity;
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntCurriculumActivity.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.CurriculumActivity.PROC_GET_ALL_CURRICULUM_ACTIVITIES, sqlConnection);
                if (!string.IsNullOrEmpty(pEntCurriculumActivity.CurriculumId))
                {
                    _sqlpara = new SqlParameter(Schema.CurriculumActivity.PARA_CURRICULUM_ID, pEntCurriculumActivity.CurriculumId.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntCurriculumActivity.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntCurriculumActivity.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntCurriculumActivity.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntCurriculumActivity.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntCurriculumActivity.ListRange.RequestedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Learner.PARA_REQUESTED_BY_ID, pEntCurriculumActivity.ListRange.RequestedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                if (!string.IsNullOrEmpty(pEntCurriculumActivity.LanguageId))
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntCurriculumActivity.LanguageId);
                else
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListCurriculumActivitys = new List<CurriculumActivity>();
                while (_sqlreader.Read())
                {
                    entCurriculumActivity = FillObjectForGetAll(_sqlreader, _sqlObject);
                    entListCurriculumActivitys.Add(entCurriculumActivity);
                }
            }
            catch
            {
                //In Child Method & SqlReader is used then use Try - Catch - Finally Block and Close that reader(if required) in that same method.
                throw;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entListCurriculumActivitys;
        }

        public List<CurriculumActivity> GetAllCurriculum(CurriculumActivity pEntCurriculumActivity)
        {
            _sqlObject = new SQLObject();
            List<CurriculumActivity> entCurriculumActivity = new List<CurriculumActivity>();
            CurriculumActivity entCurriculum;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntCurriculumActivity.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.CurriculumActivity.PROC_GET_CURRICULUM_ACTIVITY;
                _sqlcmd.Connection = sqlConnection;

                if (!string.IsNullOrEmpty(pEntCurriculumActivity.CurriculumId))
                {
                    _sqlpara = new SqlParameter(Schema.CurriculumActivity.PARA_CURRICULUM_ID, pEntCurriculumActivity.CurriculumId.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntCurriculumActivity.ActivityId))
                {
                    _sqlpara = new SqlParameter(Schema.CurriculumActivity.PARA_ACTIVITY_ID, pEntCurriculumActivity.ActivityId.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntCurriculumActivity.SystemUserGUID))
                {
                    _sqlpara = new SqlParameter(Schema.CurriculumActivity.PARA_SystemUserGUID, pEntCurriculumActivity.SystemUserGUID.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entCurriculumActivity = new List<CurriculumActivity>();
                while (_sqlreader.Read())
                {
                    entCurriculum = FillCurriclumObjectUser(_sqlreader);
                    entCurriculumActivity.Add(entCurriculum);
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
            return entCurriculumActivity;
        }

        public CurriculumActivity GetCurriculumById(CurriculumActivity pEntCurriculumActivity)
        {
            _sqlObject = new SQLObject();
            CurriculumActivity entCurriculumActivity = new CurriculumActivity();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntCurriculumActivity.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.CurriculumActivity.PROC_GET_CURRICULUM_ACTIVITY_MASTER_ID;
                _sqlcmd.Connection = sqlConnection;

                if (!string.IsNullOrEmpty(pEntCurriculumActivity.CurriculumId))
                {
                    _sqlpara = new SqlParameter(Schema.CurriculumActivity.PARA_CURRICULUM_ID, pEntCurriculumActivity.CurriculumId.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntCurriculumActivity.ActivityId))
                {
                    _sqlpara = new SqlParameter(Schema.CurriculumActivity.PARA_ACTIVITY_ID, pEntCurriculumActivity.ActivityId.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entCurriculumActivity = FillCurriclumObject(_sqlreader);
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
            return entCurriculumActivity;
        }


        /// <summary>
        /// Get All CurriculumActivity
        /// </summary>
        /// <param name="pEntOrgLevel"></param>
        /// <returns>List of CurriculumActivity Object</returns>
        public List<CurriculumActivity> GetAllCurriculumActivitiesByAttempt(CurriculumActivity pEntCurriculumActivity)
        {
            _sqlObject = new SQLObject();
            List<CurriculumActivity> entListCurriculumActivitys = new List<CurriculumActivity>();
            CurriculumActivity entCurriculumActivity;
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntCurriculumActivity.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.CurriculumActivity.PROC_GET_ALL_CURRICULUM_ACTIVITIES_ATTEMPT, sqlConnection);
                if (!string.IsNullOrEmpty(pEntCurriculumActivity.CurriculumId))
                {
                    _sqlpara = new SqlParameter(Schema.CurriculumActivity.PARA_CURRICULUM_ID, pEntCurriculumActivity.CurriculumId.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntCurriculumActivity.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntCurriculumActivity.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntCurriculumActivity.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntCurriculumActivity.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntCurriculumActivity.ListRange.RequestedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Learner.PARA_REQUESTED_BY_ID, pEntCurriculumActivity.ListRange.RequestedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                if (!string.IsNullOrEmpty(pEntCurriculumActivity.LanguageId))
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntCurriculumActivity.LanguageId);
                else
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntCurriculumActivity.SystemUserGUID))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntCurriculumActivity.SystemUserGUID);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListCurriculumActivitys = new List<CurriculumActivity>();
                while (_sqlreader.Read())
                {
                    entCurriculumActivity = FillObjectForGetAll(_sqlreader, _sqlObject);
                    entListCurriculumActivitys.Add(entCurriculumActivity);
                }
            }
            catch
            {
                //In Child Method & SqlReader is used then use Try - Catch - Finally Block and Close that reader(if required) in that same method.
                throw;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entListCurriculumActivitys;
        }

        /// <summary>
        /// Fill reader object for Activities
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>CurriculumActivityActivity Object</returns>
        private CurriculumActivity FillObject(SqlDataReader pReader)
        {
            CurriculumActivity entCurActivity = new CurriculumActivity();
            int iIndex;
            if (pReader.HasRows)
            {
                while (pReader.Read())
                {
                    iIndex = pReader.GetOrdinal(Schema.CurriculumActivity.COL_CURRICULUM_ID);
                    entCurActivity.CurriculumId = pReader.GetString(iIndex);
                    iIndex = pReader.GetOrdinal(Schema.CurriculumActivity.COL_ACTIVITY_ID);
                    entCurActivity.ActivityId = pReader.GetString(iIndex);
                    entCurActivity.LanguageId = pReader[Schema.CurriculumActivity.COL_LANGUAGE_ID].ToString();
                    entCurActivity.ActivityName = pReader[Schema.CurriculumActivity.COL_ACTIVITY_NAME].ToString();
                    entCurActivity.OrignalActivityName = Convert.ToString(pReader[Schema.CurriculumActivity.COL_ORG_ACTIVITY_NAME]);
                    entCurActivity.ActivityMessage = pReader[Schema.CurriculumActivity.COL_ACTIVITY_MESSAGE].ToString();
                    entCurActivity.ActivityCompletionConditionId = Convert.ToString(pReader[Schema.CurriculumActivity.COL_ACTIVITY_COMPLETION_CONDITION_ID]);
                    entCurActivity.ActivityType = (ActivityContentType)Enum.Parse(typeof(ActivityContentType), Convert.ToString(pReader[Schema.CertificationActivity.COL_ACTIVITY_TYPE]));
                    entCurActivity.SortOrder = Convert.ToInt32(pReader[Schema.CurriculumActivity.COL_SORT_ORDER]);
                    entCurActivity.CreatedById = pReader[Schema.Common.COL_CREATED_BY].ToString();
                    entCurActivity.LastModifiedById = Convert.ToString(pReader[Schema.Common.COL_MODIFIED_BY]);
                    entCurActivity.LastModifiedDate = Convert.ToDateTime(pReader[Schema.Common.COL_MODIFIED_ON]);
                    entCurActivity.DateCreated = Convert.ToDateTime(pReader[Schema.Common.COL_CREATED_ON]);
                    entCurActivity.SectionID = Convert.ToString(pReader[Schema.CurriculumActivity.COL_SECTIONID]);
                }
            }
            return entCurActivity;
        }

        private CurriculumActivity FillCurriclumObjectUser(SqlDataReader pReader)
        {
            CurriculumActivity entCurActivity = new CurriculumActivity();
            int iIndex;
            if (pReader.HasRows)
            {
                //while (pReader.Read())
                //{
                iIndex = pReader.GetOrdinal(Schema.CurriculumActivity.COL_CURRICULUM_ID);
                entCurActivity.CurriculumId = pReader.GetString(iIndex);
                iIndex = pReader.GetOrdinal(Schema.CurriculumActivity.COL_ACTIVITY_ID);
                entCurActivity.ActivityId = pReader.GetString(iIndex);
                entCurActivity.LanguageId = pReader[Schema.CurriculumActivity.COL_LANGUAGE_ID].ToString();
                entCurActivity.ActivityName = pReader[Schema.CurriculumActivity.COL_ACTIVITY_NAME].ToString();
                entCurActivity.ActivityMessage = pReader[Schema.CurriculumActivity.COL_ACTIVITY_MESSAGE].ToString();
                entCurActivity.SortOrder = Convert.ToInt32(pReader[Schema.CurriculumActivity.COL_SORT_ORDER]);
                entCurActivity.CreatedById = pReader[Schema.Common.COL_CREATED_BY].ToString();
                entCurActivity.LastModifiedById = Convert.ToString(pReader[Schema.Common.COL_MODIFIED_BY]);
                entCurActivity.LastModifiedDate = Convert.ToDateTime(pReader[Schema.Common.COL_MODIFIED_ON]);
                entCurActivity.DateCreated = Convert.ToDateTime(pReader[Schema.Common.COL_CREATED_ON]);

                //}
            }
            return entCurActivity;
        }

        private CurriculumActivity FillCurriclumObject(SqlDataReader pReader)
        {
            CurriculumActivity entCurActivity = new CurriculumActivity();
            int iIndex;
            if (pReader.HasRows)
            {
                while (pReader.Read())
                {
                    iIndex = pReader.GetOrdinal(Schema.CurriculumActivity.COL_CURRICULUM_ID);
                    entCurActivity.CurriculumId = pReader.GetString(iIndex);
                    iIndex = pReader.GetOrdinal(Schema.CurriculumActivity.COL_ACTIVITY_ID);
                    entCurActivity.ActivityId = pReader.GetString(iIndex);
                    entCurActivity.LanguageId = pReader[Schema.CurriculumActivity.COL_LANGUAGE_ID].ToString();
                    entCurActivity.ActivityName = pReader[Schema.CurriculumActivity.COL_ACTIVITY_NAME].ToString();
                    entCurActivity.ActivityMessage = pReader[Schema.CurriculumActivity.COL_ACTIVITY_MESSAGE].ToString();
                    entCurActivity.SortOrder = Convert.ToInt32(pReader[Schema.CurriculumActivity.COL_SORT_ORDER]);
                    entCurActivity.CreatedById = pReader[Schema.Common.COL_CREATED_BY].ToString();
                    entCurActivity.LastModifiedById = Convert.ToString(pReader[Schema.Common.COL_MODIFIED_BY]);
                    entCurActivity.LastModifiedDate = Convert.ToDateTime(pReader[Schema.Common.COL_MODIFIED_ON]);
                    entCurActivity.DateCreated = Convert.ToDateTime(pReader[Schema.Common.COL_CREATED_ON]);
                }
            }
            return entCurActivity;
        }
        /// <summary>
        /// Update the CurriculumActivity
        /// </summary>
        /// <param name="pOrgLevel"></param>
        /// <param name="pUpdateMode"></param>
        /// <returns>CurriculumActivity Object</returns>
        public CurriculumActivity UpdateCurriculumActivity(CurriculumActivity pCurriculumActivity, string pUpdateMode)
        {
            _sqlObject = new SQLObject();
            int iUpdateStatus = 0;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.CurriculumActivity.PROC_UPDATE_CURRICULUM_ACTIVITY;
            _strConnString = _sqlObject.GetClientDBConnString(pCurriculumActivity.ClientId);

            _sqlpara = new SqlParameter(Schema.CurriculumActivity.PARA_CURRICULUM_ID, pCurriculumActivity.CurriculumId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.CurriculumActivity.PARA_ACTIVITY_ID, pCurriculumActivity.ActivityId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.CurriculumActivity.PARA_LANGUAGE_ID, pCurriculumActivity.LanguageId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.CurriculumActivity.PARA_ACTIVITY_NAME, pCurriculumActivity.ActivityName);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.CurriculumActivity.PARA_ACTIVITY_MESSAGE, pCurriculumActivity.ActivityMessage);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.CurriculumActivity.PARA_ACTIVITY_COMPLETION_CONDITION_ID, pCurriculumActivity.ActivityCompletionConditionId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.CurriculumActivity.PARA_ACTIVITY_TYPE, pCurriculumActivity.ActivityType);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.CurriculumActivity.PARA_SORT_ORDER, pCurriculumActivity.SortOrder);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pCurriculumActivity.CreatedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pCurriculumActivity.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, pUpdateMode);
            _sqlcmd.Parameters.Add(_sqlpara);

            iUpdateStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            return pCurriculumActivity;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntCurriculumActivity"></param>
        /// <returns></returns>
        public CurriculumActivity Get(CurriculumActivity pEntCurriculumActivity)
        {
            return GetCurriculumActivityByID(pEntCurriculumActivity);
        }
        /// <summary>
        /// Update CurriculumActivity
        /// </summary>
        /// <param name="pEntCurriculumActivity"></param>
        /// <returns></returns>
        public CurriculumActivity Update(CurriculumActivity pEntCurriculumActivity)
        {
            return EditCurriculumActivity(pEntCurriculumActivity);
        }
        #endregion
    }
}
