using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;
using System.Globalization;
namespace YPLMS2._0.API.DataAccessManager
{
    //test//

    public interface ICourseAssignmentRepository<T>
    {
        ActivityAssignment GetActivityAssignmentByID(ActivityAssignment pEntActivityAssignment);
        ActivityAssignment GetForCoursePlayer(string clientId, string courseId, string learnerId);
        ActivityAssignmentExt IsAlreadyCourseLaunched(string clientId, string tokenKey);
        ActivityAssignment AddActivityAssignment(ActivityAssignment assignmentToSave);
    }
    /// <summary>
    /// class ActivityAssignmentAdaptor
    /// </summary>
    public class ActivityAssignmentAdaptor : IDataManager<ActivityAssignment>, ICourseAssignmentRepository<ActivityAssignment>, IActivityAssignmentAdaptor<ActivityAssignment>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara = null;
        SqlDataAdapter _sqladapter = null;
        SqlConnection _sqlcon = null;
        DataTable _dtable = null;
        SQLObject _sqlObject = null;
        string _strMessageId = YPLMS.Services.Messages.ActivityAssignment.ACTIVITY_ASSIGN_ERROR;
        string _strConnString = string.Empty;
        #endregion

        /// <summary>
        /// Get Activity Assignment By ID
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns>ActivityAssignment Object</returns>
        public ActivityAssignment GetActivityAssignmentByID(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_GET_LEARNER_ASSIGNMENT, sqlConnection);
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entActivityAssignment = FillObject(_sqlreader, _sqlObject);
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
            return entActivityAssignment;
        }

        public ActivityAssignment GetUsersActivityAssignmentByID(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_GET_LEARNER_USERS, sqlConnection);
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entActivityAssignment = FillObject(_sqlreader, _sqlObject);
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
            return entActivityAssignment;
        }
        public ActivityAssignment GetUserActivity_ForBulkImport(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_USERActivitySearch_ForBulkImport, sqlConnection);
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entActivityAssignment = FillObject(_sqlreader, _sqlObject);
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
            return entActivityAssignment;
        }

        /// <summary>
        /// Get Activity Assignment By ID
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns>ActivityAssignment Object</returns>
        public ActivityAssignment GetCoursesCount(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_MAIN_PAGE_COURSE_COUNT, sqlConnection);
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entActivityAssignment = FillCourseCountObject(_sqlreader, _sqlObject);
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
            return entActivityAssignment;
        }
        /// <summary>
        /// Get Activity Assignment By ID
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns>ActivityAssignment Object</returns>
        public ActivityAssignment GetActivityAssignmentByID_Learner_Print(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_GET_LEARNER_ASSIGNMENT_PRINT, sqlConnection);
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntActivityAssignment.ParaLanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entActivityAssignment = FillObjectLearner_Print(_sqlreader, _sqlObject);
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
            return entActivityAssignment;
        }


        /// <summary>
        /// Get Activity Assignment By ID
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns>ActivityAssignment Object</returns>
        public ActivityAssignment GetActivityAssignmentByID_Learner_Print_ALL(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_GET_LEARNER_ASSIGNMENT_PRINT_ALL, sqlConnection);
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntActivityAssignment.ParaLanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entActivityAssignment = FillObjectLearner_Print(_sqlreader, _sqlObject);
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
            return entActivityAssignment;
        }

        /// <summary>
        /// To check for user assignment
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public ActivityAssignment CheckUserAssignmentByID(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_CHECK_USER_ASSIGNMENT, sqlConnection);
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entActivityAssignment = FillObject(_sqlreader, _sqlObject);
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
            return entActivityAssignment;
        }

        /// <summary>
        /// To check for user assignment
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public ActivityAssignmentExt IsAlreadyCourseLaunched(string clientId, string tokenKey)
        {
            ActivityAssignmentExt oActivityAssignmentExt = new ActivityAssignmentExt();
            _sqlObject = new SQLObject();
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(clientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ParticipantDetails.PROC_PARTICIPANT_CHECK_COURSE_ALREADY_LAUNCH, sqlConnection);

                _sqlpara = new SqlParameter(Schema.ParticipantDetails.PARA_TOKEN_KEY, tokenKey);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();

                oActivityAssignmentExt = FillObject_ActivityExt(_sqlreader, _sqlObject);
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
            return oActivityAssignmentExt;
        }


        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>ActivityAssignment Object</returns>
        private ActivityAssignmentExt FillObject_ActivityExt(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            ActivityAssignmentExt entActivityAssignment = new ActivityAssignmentExt();
            EntityRange entRange = new EntityRange();
            int iIndex;

            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignmentExt.COL_COURSEID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignmentExt.COL_COURSEID);
                    if (!pSqlReader.IsDBNull(iIndex))
                    {
                        entActivityAssignment.ID = pSqlReader.GetString(iIndex);
                        entActivityAssignment.CourseId = entActivityAssignment.ID;
                    }
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignmentExt.COL_SYSTEMUSERGUID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignmentExt.COL_SYSTEMUSERGUID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.SystemUserGUID = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignmentExt.COL_PARTICIPANTID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignmentExt.COL_PARTICIPANTID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ParticipantId = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignmentExt.COL_ISURLLAUNCHED))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignmentExt.COL_ISURLLAUNCHED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsURLLaunched = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignmentExt.COL_TOKENKEY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignmentExt.COL_TOKENKEY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.TokenKey = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignmentExt.COL_VENDORCODE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignmentExt.COL_VENDORCODE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.VendorCode = pSqlReader.GetString(iIndex);
                }

            }
            return entActivityAssignment;
        }


        /// <summary>
        /// To check for user assignment
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public ActivityAssignment CheckUserAssignmentByID_CoursePlayer(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_CHECK_USER_ASSIGNMENT_COURSE_PLAYER, sqlConnection);
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entActivityAssignment = FillObject_CoursePlayer(_sqlreader, _sqlObject);
            }
            catch (Exception expCommon)
            {
               _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entActivityAssignment;
        }
        /// <summary>
        /// To check for user assignment - Optimized
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public ActivityAssignment CheckUserAssignmentByIDOptimized(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_CHECK_USER_ASSIGNMENT_OPTIMIZED, sqlConnection);
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                entActivityAssignment = FillObjectOptimized(_sqlreader, _sqlObject);
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
            return entActivityAssignment;
        }

        private ActivityAssignment FillObject_ForUnAssignment(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            EntityRange entRange = new EntityRange();
            int iIndex;

            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                {
                    entActivityAssignment.ID = pSqlReader.GetString(iIndex);
                    entActivityAssignment.ActivityID = entActivityAssignment.ID;
                }

                //iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACT_ID);

                //iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_TYPE_ID);
                //if (!pSqlReader.IsDBNull(iIndex))
                //    entActivityAssignment.ActivityTypeId = pSqlReader.GetString(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_TYPE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_TYPE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ActivityType = (ActivityContentType)Enum.Parse(typeof(ActivityContentType), pSqlReader.GetString(iIndex));
                }

                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ActivityName = pSqlReader.GetString(iIndex);
                else
                    entActivityAssignment.ActivityName = "";
            }
            return entActivityAssignment;
        }



        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>ActivityAssignment Object</returns>
        private ActivityAssignment FillObject(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            EntityRange entRange = new EntityRange();
            int iIndex;

            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                {
                    entActivityAssignment.ID = pSqlReader.GetString(iIndex);
                    entActivityAssignment.ActivityID = entActivityAssignment.ID;
                }

                //iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACT_ID);
                //if (!pSqlReader.IsDBNull(iIndex))
                //    entActivityAssignment.ActivityID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.UserID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_TYPE_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ActivityTypeId = pSqlReader.GetString(iIndex);
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_COMPLETION_CONDITION_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_COMPLETION_CONDITION_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.CompletionConditionId = (ActivityCompletionCondition)Enum.Parse(typeof(ActivityCompletionCondition), pSqlReader.GetString(iIndex));
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ASSIGNMENT_MODE_FOR_OVERRIDE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ASSIGNMENT_MODE_FOR_OVERRIDE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.AssignmentModeForOverride = (ActivityAssignmentMode)Enum.Parse(typeof(ActivityAssignmentMode), pSqlReader.GetString(iIndex));
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_RULE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_RULE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.RuleId = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_LICENSE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_LICENSE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.LicenseId = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_DISPLAY_ACTIVITY_STATUS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_DISPLAY_ACTIVITY_STATUS);
                    if (!pSqlReader.IsDBNull(iIndex))
                    {
                        entActivityAssignment.DisplayActivityStatus = pSqlReader.GetString(iIndex);

                    }
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_ADMIN_ASSIGNMENT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_ADMIN_ASSIGNMENT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsForAdminPreview = pSqlReader.GetBoolean(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_IS_PRINT_CERTIFICATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_IS_PRINT_CERTIFICATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsPrintCertificate = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ATTEMPT_COUNT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ATTEMPT_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.AttemptCount = pSqlReader.GetInt32(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_EDIT_FROM_UI))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_EDIT_FROM_UI);
                    if (!pSqlReader.IsDBNull(iIndex))
                    {
                        entActivityAssignment.IsEditfromUI = pSqlReader.GetBoolean(iIndex);

                    }
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_CREATION_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_CREATION_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsAssignmentBasedOnCreationDate = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_HIRE_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_HIRE_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsAssignmentBasedOnHireDate = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ASSIGN_AFTER_DAYS_OF))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ASSIGN_AFTER_DAYS_OF);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.AssignAfterDaysOf = pSqlReader.GetInt32(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_DATE_OF_ASSIGNMENT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_DATE_OF_ASSIGNMENT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.AssignmentDateSet = pSqlReader.GetDateTime(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_NO_DUE_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_NO_DUE_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsNoDueDate = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_ASSIGN_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_ASSIGN_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsDueBasedOnAssignDate = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_CREATION_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_CREATION_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsDueBasedOnCreationDate = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_HIRE_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_HIRE_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsDueBasedOnHireDate = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_START_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_START_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsDueBasedOnStartDate = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_DUE_AFTER_DAYS_OF))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_DUE_AFTER_DAYS_OF);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.DueAfterDaysOf = pSqlReader.GetInt32(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_DUE_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_DUE_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.DueDateSet = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_NO_EXPIRY_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_NO_EXPIRY_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsNoExpiryDate = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_ASSIGN_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_ASSIGN_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsExpiryBasedOnAssignDate = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_DUE_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_DUE_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsExpiryBasedOnDueDate = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_START_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_START_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsExpiryBasedOnStartDate = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_EXPIRY_AFTER_DAYS_OF))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_EXPIRY_AFTER_DAYS_OF);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ExpireAfterDaysOf = pSqlReader.GetInt32(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_EXPIRY_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_EXPIRY_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ExpiryDateSet = pSqlReader.GetDateTime(iIndex);
                }

                //**** New Reassignment Fields set ends
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_REASSIGN_BASED_ON_ASSIGN_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_REASSIGN_BASED_ON_ASSIGN_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsReAssignmentBasedOnAssignmentDate = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_REASSIGN_BASED_ON_ASSIGN_COMPL_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_REASSIGN_BASED_ON_ASSIGN_COMPL_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsReAssignmentBasedOnAssignmentCompletionDate = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_REASSIGN_DUE_BASED_ON_ASSIGN_COMPL_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_REASSIGN_DUE_BASED_ON_ASSIGN_COMPL_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsReassignDueBasedOnAssignmentCompletionDate = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_REASSIGN_DUE_BASED_ON_REASSIGN_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_REASSIGN_DUE_BASED_ON_REASSIGN_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsReassignDueBasedOnReassignmentDate = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_ASSIGN_COMPL_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_ASSIGN_COMPL_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsReassignExpiryBasedOnAssignmentCompletionDate = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsReassignExpiryBasedOnReassignmentDate = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DUE_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DUE_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsReassignExpiryBasedOnReassignmentDueDate = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_SEND_EMAIL))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_SEND_EMAIL);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.SendEmail = pSqlReader.GetBoolean(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_SEND_EMAIL_TYPE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_SEND_EMAIL_TYPE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.SendEmailType = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_EMAIL_TEMPLATE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_EMAIL_TEMPLATE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.EmailTemplateId = pSqlReader.GetString(iIndex);
                }
                //New column added
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_REASSIGNMENT_COUNT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_REASSIGNMENT_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ReassignmentCount = pSqlReader.GetInt32(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.CreatedById = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.LastModifiedById = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.LastModifiedDate = pSqlReader.GetDateTime(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ActivityName = pSqlReader.GetString(iIndex);
                    else
                        entActivityAssignment.ActivityName = "";
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_DESC))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_DESC);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ActivityDescription = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_TYPE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_TYPE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ActivityType = (ActivityContentType)Enum.Parse(typeof(ActivityContentType), pSqlReader.GetString(iIndex));
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_URL))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_URL);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ActivityURL = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleTracking.COL_DATE_OF_START))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleTracking.COL_DATE_OF_START);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.DateOfStart = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_STATUS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_STATUS);
                    if (!pSqlReader.IsDBNull(iIndex))
                    {
                        entActivityAssignment.ActivityStatus = (ActivityCompletionStatus)Enum.Parse(typeof(ActivityCompletionStatus), pSqlReader.GetString(iIndex));
                        entActivityAssignment.AssignmentStatus = Convert.ToString(entActivityAssignment.ActivityStatus);
                    }
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleTracking.COL_DATE_OF_COMPLETION))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleTracking.COL_DATE_OF_COMPLETION);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.DateOfCompletion = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_PREVIOUS_ASSIGNMENT_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_PREVIOUS_ASSIGNMENT_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.PreviousAssignmentId = pSqlReader.GetString(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_CURRENTLY_ASSIGNED))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_CURRENTLY_ASSIGNED);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsCurrentlyAssigned = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_LEARNER_NAME))
                {

                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_LEARNER_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.UserName = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (pSqlReader.GetInt32(iIndex) > 0)
                    {
                        entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                        entActivityAssignment.ListRange = entRange;
                    }
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_SCORE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_SCORE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.Score = pSqlReader.GetString(iIndex);
                }

            }
            return entActivityAssignment;
        }


        /// <summary>
        /// Fill Reader Object for Course Count
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>ActivityAssignment Object</returns>
        private ActivityAssignment FillCourseCountObject(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            EntityRange entRange = new EntityRange();
            int iIndex;

            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_TOTAL_ROWS_COMPLETED);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.TotalRowsCompleted = pSqlReader.GetInt32(iIndex);


                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_TOTAL_ROWS_ASSIGNED);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.TotalRowsAssigned = pSqlReader.GetInt32(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_CURRICULUM_COMPLETION_PERCENTAGE);
                if (!pSqlReader.IsDBNull(iIndex))
                {
                    decimal perct = 0;
                    decimal.TryParse(Convert.ToString(pSqlReader[Schema.ActivityAssignment.COL_CURRICULUM_COMPLETION_PERCENTAGE]), out perct);
                    entActivityAssignment.CurriculumCompletionPercentage = Math.Round(perct, 0);
                    //entActivityAssignment.CurriculumCompletionPercentage = pSqlReader.GetDecimal(iIndex);
                }

            }
            return entActivityAssignment;
        }


        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>ActivityAssignment Object</returns>
        private ActivityAssignment FillObject_InnerActivity(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            EntityRange entRange = new EntityRange();
            int iIndex;

            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                {
                    entActivityAssignment.ID = pSqlReader.GetString(iIndex);
                    entActivityAssignment.ActivityID = entActivityAssignment.ID;
                }



            }
            return entActivityAssignment;
        }


        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>ActivityAssignment Object</returns>
        private ActivityAssignment FillObject_CoursePlayer(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            EntityRange entRange = new EntityRange();
            int iIndex;

            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ActivityName = pSqlReader.GetString(iIndex);
                else
                    entActivityAssignment.ActivityName = "";

                iIndex = pSqlReader.GetOrdinal("CompletionStatus");
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ActivityStatus = (ActivityCompletionStatus)Enum.Parse(typeof(ActivityCompletionStatus), pSqlReader.GetString(iIndex));
                else
                    entActivityAssignment.ActivityStatus = ActivityCompletionStatus.NotStarted;

                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ATTEMPT_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.AttemptId = pSqlReader.GetString(iIndex);
                else
                    entActivityAssignment.AttemptId = "";

                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_TYPE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ActivityType = (ActivityContentType)Enum.Parse(typeof(ActivityContentType), pSqlReader.GetString(iIndex));
            }
            return entActivityAssignment;
        }



        /// <summary>
        /// Fill Reader Object - Optimized
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>ActivityAssignment Object</returns>
        private ActivityAssignment FillObjectOptimized(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            EntityRange entRange = new EntityRange();
            int iIndex;

            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_STATUS))
                {

                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_STATUS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ActivityStatus = (ActivityCompletionStatus)Enum.Parse(typeof(ActivityCompletionStatus), pSqlReader.GetString(iIndex));
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (pSqlReader.GetInt32(iIndex) > 0)
                    {
                        entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                        entActivityAssignment.ListRange = entRange;
                    }
                }

            }
            return entActivityAssignment;
        }

        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>ActivityAssignment Object</returns>
        private ActivityAssignment FillObject_CertificationPrograms(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            EntityRange entRange = new EntityRange();
            int iIndex;

            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ActivityName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_USER_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.UserID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_USER_Name);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.UserName = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_NAME_ALIAS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.UserDataXML = pSqlReader.GetString(iIndex);
                else
                    entActivityAssignment.UserDataXML = "";

                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_CER_PROGRAM_IS_APPROVED);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.IsApproved = pSqlReader.GetBoolean(iIndex);
                else
                    entActivityAssignment.IsApproved = false;

                //iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_CER_PROGRAM_IS_REJECTED);
                //if (!pSqlReader.IsDBNull(iIndex))
                //    entActivityAssignment.IsRejected = pSqlReader.GetBoolean(iIndex);
                //else
                entActivityAssignment.IsRejected = false;

                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_TYPE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ActivityType = (ActivityContentType)Enum.Parse(typeof(ActivityContentType), pSqlReader.GetString(iIndex));

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (pSqlReader.GetInt32(iIndex) > 0)
                    {
                        entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                        entActivityAssignment.ListRange = entRange;
                    }
                }
            }
            return entActivityAssignment;
        }


        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>ActivityAssignment Object</returns>
        private ActivityAssignment FillObjectLearner_Print(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            EntityRange entRange = new EntityRange();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.UserID = pSqlReader.GetString(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_IS_PRINT_CERTIFICATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_IS_PRINT_CERTIFICATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsPrintCertificate = pSqlReader.GetBoolean(iIndex);
                }

                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ActivityName = pSqlReader.GetString(iIndex);
                else
                    entActivityAssignment.ActivityName = "";

                iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleTracking.COL_DATE_OF_COMPLETION);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.DateOfCompletion = pSqlReader.GetDateTime(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_STATUS);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ActivityStatus = (ActivityCompletionStatus)Enum.Parse(typeof(ActivityCompletionStatus), pSqlReader.GetString(iIndex));

            }
            return entActivityAssignment;
        }

        /// <summary>
        /// Fill Reader Object For Activity
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>ActivityAssignment Object</returns>
        private ActivityAssignment FillActivity(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            EntityRange entRange = new EntityRange();
            int iIndex;
            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.UserID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ATTEMPT_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ATTEMPT_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.AttemptId = pSqlReader.GetString(iIndex);
                }



                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_IS_ADMIN_ASSIGNMENT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_IS_ADMIN_ASSIGNMENT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsForAdminPreview = pSqlReader.GetBoolean(iIndex);
                }



                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_IS_PRINT_CERTIFICATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_IS_PRINT_CERTIFICATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsPrintCertificate = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_USER_Name))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_USER_Name);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.UserName = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_LEARNER_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_LEARNER_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.UserName = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleTracking.COL_USER_DATA_XML))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleTracking.COL_USER_DATA_XML);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.UserDataXML = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_TYPE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_TYPE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ActivityTypeId = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.CreatedByName = pSqlReader.GetString(iIndex);
                }
              
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_COMPLETION_CONDITION_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_COMPLETION_CONDITION_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.CompletionConditionId = (ActivityCompletionCondition)Enum.Parse(typeof(ActivityCompletionCondition), pSqlReader.GetString(iIndex));
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ASSIGNED_BY_ENTITY_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ASSIGNED_BY_ENTITY_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.RuleId = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ASSIGNED_BY_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ASSIGNED_BY_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.CreatedById = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.LastModifiedById = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.LastModifiedDate = pSqlReader.GetDateTime(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ActivityName = pSqlReader.GetString(iIndex);
                }
                //if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_TYPE))
                //{
                //    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_TYPE);
                //    if (!pSqlReader.IsDBNull(iIndex))
                //        entActivityAssignment.ActivityType = (ActivityContentType)Enum.Parse(typeof(ActivityContentType), pSqlReader.GetString(iIndex));
                //}

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_TYPE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_TYPE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ActivityType = (ActivityContentType)Enum.Parse(typeof(ActivityContentType), pSqlReader.GetString(iIndex));
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_URL))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_URL);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ActivityURL = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleTracking.COL_DATE_OF_START))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleTracking.COL_DATE_OF_START);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.DateOfStart = pSqlReader.GetDateTime(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_STATUS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_STATUS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ActivityStatus = (ActivityCompletionStatus)Enum.Parse(typeof(ActivityCompletionStatus), pSqlReader.GetString(iIndex));

                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_DISPLAY_ACTIVITY_STATUS))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_DISPLAY_ACTIVITY_STATUS);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.DisplayActivityStatus = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleTracking.COL_DATE_OF_COMPLETION))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleTracking.COL_DATE_OF_COMPLETION);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.DateOfCompletion = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_SCORE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_SCORE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.Score = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (pSqlReader.GetInt32(iIndex) > 0)
                    {
                        entRange = new EntityRange();
                        entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                        entActivityAssignment.ListRange = entRange;
                    }
                }
            }
            return entActivityAssignment;
        }

        /// <summary>
        /// Add Activity Assignment
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns>ActivityAssignment Object</returns>
        public ActivityAssignment AddActivityAssignment(ActivityAssignment pEntActivityAssignment)
        {
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            try
            {
                entActivityAssignment = Update(pEntActivityAssignment, Schema.Common.VAL_INSERT_MODE);
            }
            catch (Exception expCommon)
            {
               _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entActivityAssignment;
        }

        /// <summary>
        /// Edit Activity Assignment
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns>ActivityAssignment Object</returns>
        public ActivityAssignment EditActivityAssignment(ActivityAssignment pEntActivityAssignment)
        {
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            try
            {
                entActivityAssignment = Update(pEntActivityAssignment, Schema.Common.VAL_UPDATE_MODE);
            }
            catch (Exception expCommon)
            {
              //  _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entActivityAssignment;
        }

        /// <summary>
        /// Update Activity Assignment
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <param name="pUpdateMode"></param>
        /// <returns>ActivityAssignment Object</returns>
        private ActivityAssignment Update(ActivityAssignment pEntActivityAssignment, string pStrUpdateMode)
        {
            string strClientId = pEntActivityAssignment.ClientId;

            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _sqlcmd.CommandText = Schema.ActivityAssignment.PROC_UPDATE_ACTIVITY_ASSIGN;
            //_strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
            _strConnString = _sqlObject.GetClientDBConnString(strClientId);

            _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE_ID, pEntActivityAssignment.ActivityTypeId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNMENT_MODE_FOR_OVERRIDE, pEntActivityAssignment.AssignmentModeForOverride.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNMENT_TYPE_ID, pEntActivityAssignment.AssignmentTypeId.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_COMPLETION_CONDITION_ID, pEntActivityAssignment.CompletionConditionId.ToString());
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_RULE_ID, pEntActivityAssignment.RuleId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_LICENSE_ID, pEntActivityAssignment.LicenseId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_CURRENTLY_ASSIGNED, pEntActivityAssignment.IsCurrentlyAssigned);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_EDIT_FROM_UI, pEntActivityAssignment.IsEditfromUI);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_ASSIGNMENT_BASED_ON_HIRE_DATE, pEntActivityAssignment.IsAssignmentBasedOnHireDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_ASSIGNMENT_BASED_ON_CREATION_DATE, pEntActivityAssignment.IsAssignmentBasedOnCreationDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGN_AFTER_DAYS_OF, pEntActivityAssignment.AssignAfterDaysOf);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntActivityAssignment.AssignmentDateSet) < 0)
            {
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_DATE_OF_ASSIGNMENT, pEntActivityAssignment.AssignmentDateSet);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_NO_DUE_DATE, pEntActivityAssignment.IsNoDueDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_DUE_BASED_ON_ASSIGN_DATE, pEntActivityAssignment.IsDueBasedOnAssignDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_DUE_BASED_ON_HIRE_DATE, pEntActivityAssignment.IsDueBasedOnHireDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_DUE_BASED_ON_CREATION_DATE, pEntActivityAssignment.IsDueBasedOnCreationDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_DUE_BASED_ON_START_DATE, pEntActivityAssignment.IsDueBasedOnStartDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_DUE_AFTER_DAYS_OF, pEntActivityAssignment.DueAfterDaysOf);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntActivityAssignment.DueDateSet) < 0)
            {
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_DUE_DATE, pEntActivityAssignment.DueDateSet);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_NO_EXPIRY_DATE, pEntActivityAssignment.IsNoExpiryDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_EXPIRY_BASED_ON_ASSIGN_DATE, pEntActivityAssignment.IsExpiryBasedOnAssignDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_EXPIRY_BASED_ON_START_DATE, pEntActivityAssignment.IsExpiryBasedOnStartDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_EXPIRY_BASED_ON_DUE_DATE, pEntActivityAssignment.IsExpiryBasedOnDueDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_EXPIRY_AFTER_DAYS_OF, pEntActivityAssignment.ExpireAfterDaysOf);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntActivityAssignment.ExpiryDateSet) < 0)
            {
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_EXPIRY_DATE, pEntActivityAssignment.ExpiryDateSet);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            // Reassign assignment date
            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_BASED_ON_ASSIGN_DATE, pEntActivityAssignment.IsReAssignmentBasedOnAssignmentDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_BASED_ON_ASSIGN_COMPL_DATE, pEntActivityAssignment.IsReAssignmentBasedOnAssignmentCompletionDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REASSIGN_AFTER_DAYS_OF, pEntActivityAssignment.ReAssignAfterDaysOf);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntActivityAssignment.ReAssignmentDateSet) < 0)
            {
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REASSIGNMENT_DATE_SET, pEntActivityAssignment.ReAssignmentDateSet);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            // Reassign Due date
            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_NO_DUE_DATE, pEntActivityAssignment.IsReassignNoDueDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_DUE_BASED_ON_ASSIGN_COMPL_DATE, pEntActivityAssignment.IsReassignDueBasedOnAssignmentCompletionDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_DUE_BASED_ON_REASSIGN_DATE, pEntActivityAssignment.IsReassignDueBasedOnReassignmentDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REASSIGN_DUE_AFTER_DAYS_OF, pEntActivityAssignment.ReassignDueAfterDaysOf);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntActivityAssignment.ReassignDueDateSet) < 0)
            {
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REASSIGN_DUE_DATE, pEntActivityAssignment.ReassignDueDateSet);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            // Reassign Expiry date
            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_NO_EXPIRY_DATE, pEntActivityAssignment.IsReassignNoExpiryDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_EXPIRY_BASED_ON_ASSIGN_COMPL_DATE, pEntActivityAssignment.IsReassignExpiryBasedOnAssignmentCompletionDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DUE_DATE, pEntActivityAssignment.IsReassignExpiryBasedOnReassignmentDueDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DATE, pEntActivityAssignment.IsReassignExpiryBasedOnReassignmentDate);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REASSIGN_EXPIRY_AFTER_DAYS_OF, pEntActivityAssignment.ReassignExpireAfterDaysOf);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntActivityAssignment.ReassignExpiryDateSet) < 0)
            {
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REASSIGN_EXPIRY_DATE, pEntActivityAssignment.ReassignExpiryDateSet);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            // other 
            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_SEND_EMAIL, pEntActivityAssignment.SendEmail);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_SEND_EMAIL_TYPE, pEntActivityAssignment.SendEmailType);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_EMAIL_TEMPLATE_ID, pEntActivityAssignment.EmailTemplateId);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntActivityAssignment.LastModifiedById);
            _sqlcmd.Parameters.Add(_sqlpara);

            //Code - New Properties added as on 23-Nov-09 ends here. 
            if (pStrUpdateMode == Schema.Common.VAL_INSERT_MODE)
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntActivityAssignment.IsForAdminPreview)
            {
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_FOR_ADMIN_PREVIEW, pEntActivityAssignment.IsForAdminPreview);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            int intRows = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

            //-aw 9/7/2011 LCP-21088 Assign users based on business rules
            //UpdateDynamicAssignments(strClientId);

            return pEntActivityAssignment;
        }

        /// <summary>
        /// Get User Activity AssignmentList
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns>List of ActivityAssignment Object</returns>
        public List<ActivityAssignment> GetUserActivityAssignmentList(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            List<ActivityAssignment> entListLearnerAssignments = null;
            ActivityAssignment entAssignment = null;
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_GET_LEARNER_ACTIVITY_ASSIGN, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.UserID))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ID))
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ActivityType != ActivityContentType.None)
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, pEntActivityAssignment.ActivityType.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ActivityStatus != ActivityCompletionStatus.None)
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_STATUS, pEntActivityAssignment.ActivityStatus.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_STATUS, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById))
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNED_BY_ID, pEntActivityAssignment.CreatedById);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNED_BY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.IsCurrentlyAssigned != null)
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_CURRENTLY_ASSIGNED, pEntActivityAssignment.IsCurrentlyAssigned);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_CURRENTLY_ASSIGNED, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ListRange != null)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ListRange != null)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ParaLanguageId != null)
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntActivityAssignment.ParaLanguageId);
                else
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);


                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListLearnerAssignments = new List<ActivityAssignment>();
                while (_sqlreader.Read())
                {
                    entAssignment = FillObject(_sqlreader, _sqlObject);
                    entListLearnerAssignments.Add(entAssignment);
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
            return entListLearnerAssignments;
        }


        /// <summary>
        /// Get User Activity AssignmentList
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns>List of ActivityAssignment Object</returns>
        public List<ActivityAssignment> GetUserActivityAssignmentListFoDL(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            List<ActivityAssignment> entListLearnerAssignments = null;
            ActivityAssignment entAssignment = null;
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_GET_LEARNER_ASSIGNMENT_FOR_DL, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.UserID))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                //if (!string.IsNullOrEmpty(pEntActivityAssignment.ID))
                //    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                //else
                //    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, null);
                //_sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById))
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNED_BY_ID, pEntActivityAssignment.CreatedById);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNED_BY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);


                if (pEntActivityAssignment.ListRange != null)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ListRange != null)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListLearnerAssignments = new List<ActivityAssignment>();
                while (_sqlreader.Read())
                {
                    entAssignment = FillObject(_sqlreader, _sqlObject);
                    entListLearnerAssignments.Add(entAssignment);
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
            return entListLearnerAssignments;
        }
        //Tanveer 1/04/2011
        /// <summary>
        /// Get User Assignment List
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public List<ActivityAssignment> GetUserAssignmentListForEmailTemplate(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            List<ActivityAssignment> entListLearnerAssignments = null;
            ActivityAssignment entAssignment = null;
            SqlConnection sqlConnection = null;


            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_GET_LEARNER_ASSIGNMENT_FOREMAILTEMPLATE, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.UserID))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ID))
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ActivityType != ActivityContentType.None)
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, pEntActivityAssignment.ActivityType.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ActivityStatus != ActivityCompletionStatus.None)
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_STATUS, pEntActivityAssignment.ActivityStatus.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_STATUS, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById))
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNED_BY_ID, pEntActivityAssignment.CreatedById);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNED_BY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.IsCurrentlyAssigned != null)
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_CURRENTLY_ASSIGNED, pEntActivityAssignment.IsCurrentlyAssigned);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_CURRENTLY_ASSIGNED, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ListRange != null)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ListRange != null)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlcmd.CommandTimeout = 0;

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListLearnerAssignments = new List<ActivityAssignment>();
                while (_sqlreader.Read())
                {
                    entAssignment = FillObject(_sqlreader, _sqlObject);
                    entListLearnerAssignments.Add(entAssignment);
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
            return entListLearnerAssignments;
        }

        /// <summary>
        /// Get User Assignment List For Edir
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public List<ActivityAssignment> GetUserAssignmentListForEdit(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            List<ActivityAssignment> entListLearnerAssignments = null;
            ActivityAssignment entAssignment = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_GET_LEARNER_ASSIGNMENT_FOR_EDIT, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.UserID))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ID))
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ActivityType != ActivityContentType.None)
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, pEntActivityAssignment.ActivityType.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ActivityStatus != ActivityCompletionStatus.None)
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_STATUS, pEntActivityAssignment.ActivityStatus.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_STATUS, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById))
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNED_BY_ID, pEntActivityAssignment.CreatedById);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNED_BY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.IsCurrentlyAssigned != null)
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_CURRENTLY_ASSIGNED, pEntActivityAssignment.IsCurrentlyAssigned);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_CURRENTLY_ASSIGNED, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ListRange != null)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ListRange != null)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlcmd.CommandTimeout = 0;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListLearnerAssignments = new List<ActivityAssignment>();
                while (_sqlreader.Read())
                {
                    entAssignment = FillObject(_sqlreader, _sqlObject);
                    entListLearnerAssignments.Add(entAssignment);
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
            return entListLearnerAssignments;
        }

        /// <summary>
        /// Get User Assignment List For Unlock
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public List<ActivityAssignment> GetUserAssignmentListForUnlock(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            List<ActivityAssignment> entListLearnerAssignments = null;
            ActivityAssignment entAssignment = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_GET_LEARNER_ASSIGNMENT_FOR_UNLOCK, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.UserID))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ID))
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ActivityType != ActivityContentType.None)
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, pEntActivityAssignment.ActivityType.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ActivityStatus != ActivityCompletionStatus.None)
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_STATUS, pEntActivityAssignment.ActivityStatus.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_STATUS, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById))
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNED_BY_ID, pEntActivityAssignment.CreatedById);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNED_BY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.IsCurrentlyAssigned != null)
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_CURRENTLY_ASSIGNED, pEntActivityAssignment.IsCurrentlyAssigned);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_CURRENTLY_ASSIGNED, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ListRange != null)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ListRange != null)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlcmd.CommandTimeout = 0;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListLearnerAssignments = new List<ActivityAssignment>();
                while (_sqlreader.Read())
                {
                    entAssignment = FillObject(_sqlreader, _sqlObject);
                    entListLearnerAssignments.Add(entAssignment);
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
            return entListLearnerAssignments;
        }


        public List<ActivityAssignment> GetUserAssignmentsForAttemptUnlock(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            List<ActivityAssignment> entListLearnerAssignments = null;
            ActivityAssignment entAssignment = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_GET_LEARNER_ASSIGNMENT_FOR_ATTEMPTUNLOCK, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.UserID))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ID))
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ActivityType != ActivityContentType.None)
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, pEntActivityAssignment.ActivityType.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ActivityStatus != ActivityCompletionStatus.None)
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_STATUS, pEntActivityAssignment.ActivityStatus.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_STATUS, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById))
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNED_BY_ID, pEntActivityAssignment.CreatedById);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNED_BY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.IsCurrentlyAssigned != null)
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_CURRENTLY_ASSIGNED, pEntActivityAssignment.IsCurrentlyAssigned);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_CURRENTLY_ASSIGNED, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ListRange != null)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ListRange != null)
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlcmd.CommandTimeout = 0;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListLearnerAssignments = new List<ActivityAssignment>();
                while (_sqlreader.Read())
                {
                    entAssignment = FillObject(_sqlreader, _sqlObject);
                    entListLearnerAssignments.Add(entAssignment);
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
            return entListLearnerAssignments;
        }
        /// <summary>
        /// Find Activity List By Name
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public List<ActivityAssignment> FindActivityListByName(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivity = new ActivityAssignment();
            List<ActivityAssignment> entListActivity = new List<ActivityAssignment>();
            _sqlreader = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_SEARCH_ACTIVITY_BY_NAME, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ActivityName))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntActivityAssignment.ActivityName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntActivityAssignment.ActivityType != ActivityContentType.None)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, pEntActivityAssignment.ActivityType.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CREATED_BY_ID, pEntActivityAssignment.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntActivityAssignment.CategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.Category.PARA_CATEGORYID, pEntActivityAssignment.CategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.SubCategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.SubCategory.PARA_SUBCATEGORYID, pEntActivityAssignment.SubCategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }


                _sqlpara = new SqlParameter(Schema.ContentModule.PARA_COURSE_ASSESSMENT_FLAG, pEntActivityAssignment.CouseAssessmentFlag);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntActivityAssignment.IsActiveActivity)
                {
                    _sqlpara = new SqlParameter(Schema.ContentModule.PARA_IS_ACTIVE, pEntActivityAssignment.IsActiveActivity);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entActivity = FillActivityList(_sqlreader, _sqlObject);
                    entListActivity.Add(entActivity);
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
            return entListActivity;
        }


        /// <summary>
        /// Find Activity List By Name For Assignment
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public List<ActivityAssignment> FindActivityListByNameOptimized(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivity = new ActivityAssignment();
            List<ActivityAssignment> entListActivity = new List<ActivityAssignment>();
            _sqlreader = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_SEARCH_ACTIVITY_BY_NAME_OPTIMZED, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ActivityName))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntActivityAssignment.ActivityName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CREATED_BY_ID, pEntActivityAssignment.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.ActivityType != ActivityContentType.None)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, pEntActivityAssignment.ActivityType.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntActivityAssignment.IsActiveActivity)
                {
                    _sqlpara = new SqlParameter(Schema.ContentModule.PARA_IS_ACTIVE, pEntActivityAssignment.IsActiveActivity);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entActivity = FillActivityListOptimized(_sqlreader, _sqlObject);
                    entListActivity.Add(entActivity);
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
            return entListActivity;
        }

        /// <summary>
        /// Find Activity List By Name
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public List<ActivityAssignment> FindActivityListForDelequencyHistory(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivity = new ActivityAssignment();
            List<ActivityAssignment> entListActivity = new List<ActivityAssignment>();
            _sqlreader = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_SEARCH_ACTIVITY_DELEQUENCY_HISTORY, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ActivityName))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntActivityAssignment.ActivityName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntActivityAssignment.ActivityType != ActivityContentType.None)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, pEntActivityAssignment.ActivityType.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntActivityAssignment.CategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.Category.PARA_CATEGORYID, pEntActivityAssignment.CategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.SubCategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.SubCategory.PARA_SUBCATEGORYID, pEntActivityAssignment.SubCategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntActivityAssignment.IsActiveActivity)
                {
                    _sqlpara = new SqlParameter(Schema.ContentModule.PARA_IS_ACTIVE, pEntActivityAssignment.IsActiveActivity);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntActivityAssignment.LastModifiedById != null)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REQUESTEDBY_ID, pEntActivityAssignment.LastModifiedById.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.CreatedById != null)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CREATED_BY_ID, pEntActivityAssignment.CreatedById.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entActivity = FillActivityList(_sqlreader, _sqlObject);
                    entListActivity.Add(entActivity);
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
            return entListActivity;
        }

        //
        public List<ActivityAssignment> FindActivityListForInteractionTracking(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivity = new ActivityAssignment();
            List<ActivityAssignment> entListActivity = new List<ActivityAssignment>();
            _sqlreader = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_SEARCH_ACTIVITY_COURSEASSESSMENT_INTERACTIONTRACKING, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ActivityName))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntActivityAssignment.ActivityName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntActivityAssignment.ActivityType != ActivityContentType.None)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, pEntActivityAssignment.ActivityType.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntActivityAssignment.CategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.Category.PARA_CATEGORYID, pEntActivityAssignment.CategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.SubCategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.SubCategory.PARA_SUBCATEGORYID, pEntActivityAssignment.SubCategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntActivityAssignment.IsActiveActivity)
                {
                    _sqlpara = new SqlParameter(Schema.ContentModule.PARA_IS_ACTIVE, pEntActivityAssignment.IsActiveActivity);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntActivityAssignment.LastModifiedById != null)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REQUESTEDBY_ID, pEntActivityAssignment.LastModifiedById.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.CreatedById != null)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CREATED_BY_ID, pEntActivityAssignment.CreatedById.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entActivity = FillActivityList(_sqlreader, _sqlObject);
                    entListActivity.Add(entActivity);
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
            return entListActivity;
        }
        /// <summary>
        /// 
        /// 
        /// Find Activity for One Time Assignments
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public List<ActivityAssignment> FindActivityOneTimeAssignment(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivity = new ActivityAssignment();
            List<ActivityAssignment> entListActivity = new List<ActivityAssignment>();
            _sqlreader = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_SEARCH_ACTIVITY_ONETIMEASSIGNMENT, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ActivityName))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntActivityAssignment.ActivityName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.LastModifiedById)) //For Product Admin
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REQUESTEDBY_ID, pEntActivityAssignment.LastModifiedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CREATED_BY_ID, pEntActivityAssignment.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.ActivityType != ActivityContentType.None)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, pEntActivityAssignment.ActivityType.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.CategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.Category.PARA_CATEGORYID, pEntActivityAssignment.CategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.SubCategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.SubCategory.PARA_SUBCATEGORYID, pEntActivityAssignment.SubCategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }


                if (pEntActivityAssignment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntActivityAssignment.IsActiveActivity)
                {
                    _sqlpara = new SqlParameter(Schema.ContentModule.PARA_IS_ACTIVE, pEntActivityAssignment.IsActiveActivity);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entActivity = FillActivityList(_sqlreader, _sqlObject);
                    entListActivity.Add(entActivity);
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
            return entListActivity;
        }

        public List<ActivityAssignment> GetAllCertificationPrograms_ForApproval_FromLearner(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivity = new ActivityAssignment();
            List<ActivityAssignment> entListActivity = new List<ActivityAssignment>();
            _sqlreader = null;
            SqlConnection sqlConnection = null;
            string strCPId = string.Empty;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_ASSIGNMENT_FOR_CERTIFCATIONAPPROVAL_FROM_LEARNER, sqlConnection);


                if (!string.IsNullOrEmpty(pEntActivityAssignment.ActivityID))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ActivityID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.UserID)) //For Search by User
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_USER_ID, pEntActivityAssignment.UserID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                //if (!string.IsNullOrEmpty(pEntActivityAssignment.ActivityName)) //For Search by User
                //{
                //    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntActivityAssignment.ActivityName);
                //    _sqlcmd.Parameters.Add(_sqlpara);
                //}

                if (pEntActivityAssignment.ActivityTypeId != null)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE_ID, pEntActivityAssignment.ActivityTypeId.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!pEntActivityAssignment.IsForAdminPreview)
                {
                    strCPId = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_CERTIFICATION_PROGRAM_ID_PREFIX, Schema.Common.VAL_CERTIFICATION_PROGRAM_ID_LENGTH);
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CPID, strCPId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }



                //if (pEntActivityAssignment.IsForAdminPreview)
                // {
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_FOR_ADMIN_PREVIEW, pEntActivityAssignment.IsForAdminPreview);
                _sqlcmd.Parameters.Add(_sqlpara);
                // }
                if (pEntActivityAssignment.IsApproved)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CER_PROGRAM_IS_APPROVED, pEntActivityAssignment.IsApproved);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CER_PROGRAM_IS_APPROVED, false);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                //if (pEntActivityAssignment.IsRejected)
                //{
                //    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CER_PROGRAM_IS_REJECTED, pEntActivityAssignment.IsRejected);
                //    _sqlcmd.Parameters.Add(_sqlpara);
                //}
                //else
                //{
                //_sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CER_PROGRAM_IS_REJECTED, false);
                // _sqlcmd.Parameters.Add(_sqlpara);
                // }




                if (pEntActivityAssignment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                //if (pEntActivityAssignment.IsActiveActivity)
                //{
                //    _sqlpara = new SqlParameter(Schema.ContentModule.PARA_IS_ACTIVE, pEntActivityAssignment.IsActiveActivity);
                //    _sqlcmd.Parameters.Add(_sqlpara);
                //}
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entActivity = FillObject_CertificationPrograms(_sqlreader, _sqlObject);
                    entListActivity.Add(entActivity);
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
            return entListActivity;
        }

        public List<ActivityAssignment> GetAllCertificationPrograms_ForApproval(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivity = new ActivityAssignment();
            List<ActivityAssignment> entListActivity = new List<ActivityAssignment>();
            _sqlreader = null;
            SqlConnection sqlConnection = null;
            string strCPId = string.Empty;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_ASSIGNMENT_FOR_CERTIFCATIONAPPROVAL, sqlConnection);


                if (!string.IsNullOrEmpty(pEntActivityAssignment.ActivityID))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ActivityID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.UserID)) //For Search by User
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_USER_ID, pEntActivityAssignment.UserID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                //if (!string.IsNullOrEmpty(pEntActivityAssignment.ActivityName)) //For Search by User
                //{
                //    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntActivityAssignment.ActivityName);
                //    _sqlcmd.Parameters.Add(_sqlpara);
                //}

                if (pEntActivityAssignment.ActivityTypeId != null)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE_ID, pEntActivityAssignment.ActivityTypeId.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!pEntActivityAssignment.IsForAdminPreview)
                {
                    strCPId = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_CERTIFICATION_PROGRAM_ID_PREFIX, Schema.Common.VAL_CERTIFICATION_PROGRAM_ID_LENGTH);
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CPID, strCPId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }



                //if (pEntActivityAssignment.IsForAdminPreview)
                // {
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_FOR_ADMIN_PREVIEW, pEntActivityAssignment.IsForAdminPreview);
                _sqlcmd.Parameters.Add(_sqlpara);
                // }
                if (pEntActivityAssignment.IsApproved)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CER_PROGRAM_IS_APPROVED, pEntActivityAssignment.IsApproved);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CER_PROGRAM_IS_APPROVED, false);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.IsRejected)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CER_PROGRAM_IS_REJECTED, pEntActivityAssignment.IsRejected);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CER_PROGRAM_IS_REJECTED, false);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }




                if (pEntActivityAssignment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                //if (pEntActivityAssignment.IsActiveActivity)
                //{
                //    _sqlpara = new SqlParameter(Schema.ContentModule.PARA_IS_ACTIVE, pEntActivityAssignment.IsActiveActivity);
                //    _sqlcmd.Parameters.Add(_sqlpara);
                //}
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entActivity = FillObject_CertificationPrograms(_sqlreader, _sqlObject);
                    entListActivity.Add(entActivity);
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
            return entListActivity;
        }

        public ActivityAssignment UpdateCertificationPrograms_ForApproval(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandTimeout = 0;
                _sqlcmd.CommandText = Schema.ActivityAssignment.PROC_UPDATE_FOR_CERTIFCATIONAPPROVAL;
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ActivityID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.UserID))//Add Bharat: 02-Dec-2013
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_USER_ID, pEntActivityAssignment.UserID);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_USER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);


                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CER_PROGRAM_IS_APPROVED, pEntActivityAssignment.IsApproved);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CER_PROGRAM_IS_REJECTED, pEntActivityAssignment.IsRejected);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE_ID, pEntActivityAssignment.ActivityTypeId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CREATED_BY_ID, pEntActivityAssignment.CreatedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntActivityAssignment;
        }

        public List<ActivityAssignment> FindActivityOneTimeAssignment_ForCurriculum(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivity = new ActivityAssignment();
            List<ActivityAssignment> entListActivity = new List<ActivityAssignment>();
            _sqlreader = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_SEARCH_ACTIVITY_ONETIMEASSIGNMENT_FORCURRICULUM, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ActivityName))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntActivityAssignment.ActivityName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.LastModifiedById)) //For Product Admin
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REQUESTEDBY_ID, pEntActivityAssignment.LastModifiedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CREATED_BY_ID, pEntActivityAssignment.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.ActivityType != ActivityContentType.None)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, pEntActivityAssignment.ActivityType.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.CategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.Category.PARA_CATEGORYID, pEntActivityAssignment.CategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.SubCategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.SubCategory.PARA_SUBCATEGORYID, pEntActivityAssignment.SubCategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }


                if (pEntActivityAssignment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntActivityAssignment.IsActiveActivity)
                {
                    _sqlpara = new SqlParameter(Schema.ContentModule.PARA_IS_ACTIVE, pEntActivityAssignment.IsActiveActivity);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entActivity = FillActivityList(_sqlreader, _sqlObject);
                    entListActivity.Add(entActivity);
                }
            }
            catch (Exception expCommon)
            {
               _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entListActivity;
        }

        /// <summary>
        /// Find Activity for One Time Assignments
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public List<ActivityAssignment> GetAllActivityByCategoryMapping(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivity = new ActivityAssignment();
            List<ActivityAssignment> entListActivity = new List<ActivityAssignment>();
            _sqlreader = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_SEARCH_ACTIVITY_BY_CATEGORY_MAPPING, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ID))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ActivityName))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntActivityAssignment.ActivityName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.LastModifiedById)) //For Product Admin
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REQUESTEDBY_ID, pEntActivityAssignment.LastModifiedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CREATED_BY_ID, pEntActivityAssignment.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.ActivityType != ActivityContentType.None)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, pEntActivityAssignment.ActivityType.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntActivityAssignment.IsActiveActivity)
                {
                    _sqlpara = new SqlParameter(Schema.ContentModule.PARA_IS_ACTIVE, pEntActivityAssignment.IsActiveActivity);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entActivity = FillActivityList(_sqlreader, _sqlObject);
                    entListActivity.Add(entActivity);
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
            return entListActivity;
        }

        /// <summary>
        /// Find Activity for One Time Assignments
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public List<ActivityAssignment> GetAllCategory(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivity = new ActivityAssignment();
            List<ActivityAssignment> entListActivity = new List<ActivityAssignment>();
            _sqlreader = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.Category.PROC_GET_ALL_PRODUCTCATEGORY, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.CategoryName))
                {
                    _sqlpara = new SqlParameter(Schema.Category.PARA_CATEGORYNAME, pEntActivityAssignment.CategoryName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.LanguageId))
                {
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntActivityAssignment.LanguageId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.RequestedById)) //For Product Admin
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REQUESTEDBY_ID, pEntActivityAssignment.RequestedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.IsActiveActivity)
                {
                    _sqlpara = new SqlParameter(Schema.ContentModule.PARA_IS_ACTIVE, pEntActivityAssignment.IsActiveActivity);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entActivity = FillCategoryList(_sqlreader, _sqlObject);
                    entListActivity.Add(entActivity);
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
            return entListActivity;
        }

        public List<ActivityAssignment> GetProductSubCategoryByID(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivity = new ActivityAssignment();
            List<ActivityAssignment> entListActivity = new List<ActivityAssignment>();
            _sqlreader = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.SubCategory.PROC_GET_ALL_PRODUCT_SUBCATEGORY, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.SubCategoryName))
                    _sqlpara = new SqlParameter(Schema.SubCategory.PARA_SUBCATEGORYNAME, pEntActivityAssignment.SubCategoryName);
                else
                    _sqlpara = new SqlParameter(Schema.SubCategory.PARA_SUBCATEGORYNAME, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.LanguageId))
                    _sqlpara = new SqlParameter(Schema.SubCategoryLanguage.PARA_LANGUAGEID, pEntActivityAssignment.LanguageId);
                else
                    _sqlpara = new SqlParameter(Schema.SubCategoryLanguage.PARA_LANGUAGEID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ID))
                    _sqlpara = new SqlParameter(Schema.SubCategory.PARA_CATEGORYID, pEntActivityAssignment.ID);
                else
                    _sqlpara = new SqlParameter(Schema.SubCategory.PARA_CATEGORYID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntActivityAssignment.CreatedById);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.IsActiveActivity != null)
                {
                    _sqlpara = new SqlParameter(Schema.SubCategory.PARA_ISACTIVE, pEntActivityAssignment.IsActiveActivity);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entActivity = FillSubCategoryList(_sqlreader, false, _sqlObject);
                    entListActivity.Add(entActivity);
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
            return entListActivity;
        }

        //private List<SubCategory> GetProductSubCategoryByID(SubCategory pEntProductSubCategory)
        //{
        //    _sqlObject = new SQLObject();
        //    SubCategory entProductSubCategory = new SubCategory();
        //    List<SubCategory> entListActivity = new List<SubCategory>();
        //    SqlConnection sqlConnection = null;
        //    try
        //    {
        //        _strConnString = _sqlObject.GetClientDBConnString(pEntProductSubCategory.ClientId);
        //        sqlConnection = new SqlConnection(_strConnString);
        //        _sqlcmd = new SqlCommand(Schema.SubCategory.PROC_GET_PRODUCT_SUBCATEGORY, sqlConnection);

        //        _sqlpara = new SqlParameter(Schema.SubCategory.PARA_SUBCATEGORYID, pEntProductSubCategory.ID);
        //        _sqlcmd.Parameters.Add(_sqlpara);

        //        _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntProductSubCategory.LanguageID);
        //        _sqlcmd.Parameters.Add(_sqlpara);

        //        _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
        //        while (_sqlreader.Read())
        //        {
        //            entProductSubCategory = FillSubCategoryList(_sqlreader, false, _sqlObject);
        //            entListActivity.Add(entProductSubCategory);
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

        //    return entListActivity;
        //}



        /// <summary>
        /// Find Activity for One Time Assignments
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public List<ActivityAssignment> GetAllActivityByUnifiedMappingMapping(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivity = new ActivityAssignment();
            List<ActivityAssignment> entListActivity = new List<ActivityAssignment>();
            _sqlreader = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_SEARCH_ACTIVITY_BY_UNIFIED_ACTIVITY_MAPPING, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ID))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ActivityName))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntActivityAssignment.ActivityName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.LastModifiedById)) //For Product Admin
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REQUESTEDBY_ID, pEntActivityAssignment.LastModifiedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CREATED_BY_ID, pEntActivityAssignment.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.ActivityType != ActivityContentType.None)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, pEntActivityAssignment.ActivityType.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntActivityAssignment.IsActiveActivity)
                {
                    _sqlpara = new SqlParameter(Schema.ContentModule.PARA_IS_ACTIVE, pEntActivityAssignment.IsActiveActivity);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entActivity = FillActivityList(_sqlreader, _sqlObject);
                    entListActivity.Add(entActivity);
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
            return entListActivity;
        }


        /// <summary>
        /// Find Activity for One Time Assignments
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public List<ActivityAssignment> GetAllActivityByActivityCertificateMapping(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivity = new ActivityAssignment();
            List<ActivityAssignment> entListActivity = new List<ActivityAssignment>();
            _sqlreader = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_SEARCH_ACTIVITY_BY_Activity_Certificate_MAPPING, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ID))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ActivityName))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntActivityAssignment.ActivityName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.LastModifiedById)) //For Product Admin
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REQUESTEDBY_ID, pEntActivityAssignment.LastModifiedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CREATED_BY_ID, pEntActivityAssignment.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.ActivityType != ActivityContentType.None)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, pEntActivityAssignment.ActivityType.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntActivityAssignment.IsActiveActivity)
                {
                    _sqlpara = new SqlParameter(Schema.ContentModule.PARA_IS_ACTIVE, pEntActivityAssignment.IsActiveActivity);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entActivity = FillActivityList(_sqlreader, _sqlObject);
                    entListActivity.Add(entActivity);
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
            return entListActivity;
        }


        /// <summary>
        /// To get activity for mark completion
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public List<ActivityAssignment> GetActivityforMarkCompletion(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivity = new ActivityAssignment();
            List<ActivityAssignment> entListActivity = new List<ActivityAssignment>();
            _sqlreader = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_SEARCH_ACTIVITY_FOR_MARK_COMPLETION, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ActivityName))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntActivityAssignment.ActivityName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.LastModifiedById))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REQUESTEDBY_ID, pEntActivityAssignment.LastModifiedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById)) //Add Bharat:22-Oct-2013
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CREATED_BY_ID, pEntActivityAssignment.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.ActivityType != ActivityContentType.None)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, pEntActivityAssignment.ActivityType.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.CategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.Category.PARA_CATEGORYID, pEntActivityAssignment.CategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.SubCategoryId))
                {
                    _sqlpara = new SqlParameter(Schema.SubCategory.PARA_SUBCATEGORYID, pEntActivityAssignment.SubCategoryId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }



                if (pEntActivityAssignment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntActivityAssignment.IsActiveActivity)
                {
                    _sqlpara = new SqlParameter(Schema.ContentModule.PARA_IS_ACTIVE, pEntActivityAssignment.IsActiveActivity);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entActivity = FillActivityList(_sqlreader, _sqlObject);
                    entListActivity.Add(entActivity);
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
            return entListActivity;
        }



        /// <summary>
        /// To get activity for mark completion - Optimized
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public List<ActivityAssignment> GetActivityforMarkCompletionOptimized(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            ActivityAssignment entActivity = new ActivityAssignment();
            List<ActivityAssignment> entListActivity = new List<ActivityAssignment>();
            _sqlreader = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_SEARCH_ACTIVITY_FOR_MARK_COMPLETION_OPTIMIZED, sqlConnection);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ActivityName))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, pEntActivityAssignment.ActivityName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_CREATED_BY_ID, pEntActivityAssignment.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntActivityAssignment.ActivityType != ActivityContentType.None)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE, pEntActivityAssignment.ActivityType.ToString());
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntActivityAssignment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (pEntActivityAssignment.IsActiveActivity)
                {
                    _sqlpara = new SqlParameter(Schema.ContentModule.PARA_IS_ACTIVE, pEntActivityAssignment.IsActiveActivity);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entActivity = FillActivityListOptimized(_sqlreader, _sqlObject);
                    entListActivity.Add(entActivity);
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
            return entListActivity;
        }

        /// <summary>
        /// Add Activity Assignments
        /// </summary>
        /// <param name="pEntListActivityAssignments"></param>
        /// <returns>List of ActivityAssignment Object</returns>
        public List<ActivityAssignment> AddActivityAssignments(List<ActivityAssignment> pEntListActivityAssignments, bool ReRegister)
        {
            List<ActivityAssignment> entListActivityAssignmentList = null;
            try
            {
                entListActivityAssignmentList = BulkAddUpdate(pEntListActivityAssignments, Schema.Common.VAL_INSERT_MODE, false, ReRegister);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListActivityAssignmentList;
        }

        /// <summary>
        /// Bulk Import Assignments
        /// </summary>
        /// <param name="pEntListActivityAssignments"></param>
        /// <returns></returns>
        public List<ActivityAssignment> BulkImportAssignments(List<ActivityAssignment> pEntListActivityAssignments)
        {
            List<ActivityAssignment> entListActivityAssignmentList = null;
            try
            {
                entListActivityAssignmentList = BulkAddUpdate(pEntListActivityAssignments, Schema.Common.VAL_INSERT_MODE, true, false);
            }
            catch (Exception expCommon)
            {
               _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListActivityAssignmentList;
        }

        /// <summary>
        /// Bulk Add/Update
        /// </summary>
        /// <param name="pEntListActivityAssignment"></param>
        /// <param name="pMode"></param>
        /// <returns>List of ActivityAssignment Object</returns>
        private List<ActivityAssignment> BulkAddUpdate(List<ActivityAssignment> pEntListActivityAssignment, string pStrMode, bool pIsBulkImport, bool ReRegister)
        {
            string strClientId = (pEntListActivityAssignment[0]).ClientId;

            _sqlObject = new SQLObject();
            List<ActivityAssignment> entListActivityAssignment = new List<ActivityAssignment>();
            //string strConnectionString = _sqlObject.GetClientDBConnString((pEntListActivityAssignment[0]).ClientId);
            string strConnectionString = _sqlObject.GetClientDBConnString(strClientId);
            _sqlcon = new SqlConnection(strConnectionString);
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            int iBatchSize = 0;
            //Add Columns
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_USER_ID);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_ACTIVITY_ID);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_ACTIVITY_TYPE_ID);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_ASSIGNMENT_MODE_FOR_OVERRIDE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_ASSIGNMENT_TYPE_ID);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_COMPLETION_CONDITION_ID);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_RULE_ID);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_LICENSE_ID);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_CURRENTLY_ASSIGNED);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_EDIT_FROM_UI);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_HIRE_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_CREATION_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_ASSIGN_AFTER_DAYS_OF);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_DATE_OF_ASSIGNMENT);

            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_NO_DUE_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_ASSIGN_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_HIRE_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_CREATION_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_START_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_DUE_AFTER_DAYS_OF);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_DUE_DATE);

            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_NO_EXPIRY_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_ASSIGN_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_START_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_DUE_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_EXPIRY_AFTER_DAYS_OF);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_EXPIRY_DATE);

            // New Properties
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_REASSIGN_BASED_ON_ASSIGN_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_REASSIGN_BASED_ON_ASSIGN_COMPL_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_REASSIGN_AFTER_DAYS_OF);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_REASSIGNMENT_DATE_SET);

            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_REASSIGN_NO_DUE_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_REASSIGN_DUE_BASED_ON_REASSIGN_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_REASSIGN_DUE_BASED_ON_ASSIGN_COMPL_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_REASSIGN_DUE_AFTER_DAYS_OF);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_REASSIGN_DUE_DATE);

            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_REASSIGN_NO_EXPIRY_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DUE_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_ASSIGN_COMPL_DATE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_REASSIGN_EXPIRY_AFTER_DAYS_OF);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_REASSIGN_EXPIRY_DATE);

            _dtable.Columns.Add(Schema.ActivityAssignment.COL_SEND_EMAIL);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_SEND_EMAIL_TYPE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_EMAIL_TEMPLATE_ID);
            _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
            //_dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_CERTIFICATION_PROGRAM);

            if (pIsBulkImport)
            {
                _dtable.Columns.Add(Schema.ActivityAssignment.COL_OPERATIONMODE_REASSIGNMENT_FORBULK);
                _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_OVER_RIDE);
            }


            _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_ADMIN_ASSIGNMENT);
            _dtable.Columns.Add(Schema.ActivityAssignment.COL_FORCE_REASSIGNMENT);

            foreach (ActivityAssignment entActivityAssignment in pEntListActivityAssignment)
            {
                DataRow drow = _dtable.NewRow();

                drow[Schema.ActivityAssignment.COL_USER_ID] = entActivityAssignment.UserID;
                drow[Schema.ActivityAssignment.COL_ACTIVITY_ID] = entActivityAssignment.ID;
                drow[Schema.ActivityAssignment.COL_ACTIVITY_TYPE_ID] = entActivityAssignment.ActivityTypeId;
                drow[Schema.ActivityAssignment.COL_ASSIGNMENT_MODE_FOR_OVERRIDE] = entActivityAssignment.AssignmentModeForOverride.ToString();
                drow[Schema.ActivityAssignment.COL_ASSIGNMENT_TYPE_ID] = entActivityAssignment.AssignmentTypeId.ToString();
                drow[Schema.ActivityAssignment.COL_COMPLETION_CONDITION_ID] = entActivityAssignment.CompletionConditionId.ToString();

                drow[Schema.ActivityAssignment.COL_RULE_ID] = entActivityAssignment.RuleId;
                drow[Schema.ActivityAssignment.COL_LICENSE_ID] = entActivityAssignment.LicenseId;
                if (pEntListActivityAssignment.Count == 1)                
                    drow[Schema.ActivityAssignment.COL_IS_CURRENTLY_ASSIGNED] = entActivityAssignment.IsCurrentlyAssigned;
                else
                    drow[Schema.ActivityAssignment.COL_IS_CURRENTLY_ASSIGNED] = Convert.ToBoolean(entActivityAssignment.IsCurrentlyAssigned) ? 1 : 0;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_EDIT_FROM_UI] = entActivityAssignment.IsEditfromUI;
                else
                    drow[Schema.ActivityAssignment.COL_IS_EDIT_FROM_UI] = entActivityAssignment.IsEditfromUI ? 1 : 0;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_HIRE_DATE] = entActivityAssignment.IsAssignmentBasedOnHireDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_HIRE_DATE] = entActivityAssignment.IsAssignmentBasedOnHireDate ? 1 : 0;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_CREATION_DATE] = entActivityAssignment.IsAssignmentBasedOnCreationDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_CREATION_DATE] = entActivityAssignment.IsAssignmentBasedOnCreationDate ? 1 : 0;

                drow[Schema.ActivityAssignment.COL_ASSIGN_AFTER_DAYS_OF] = entActivityAssignment.AssignAfterDaysOf;

                if (DateTime.MinValue.CompareTo(entActivityAssignment.AssignmentDateSet) < 0)
                    drow[Schema.ActivityAssignment.COL_DATE_OF_ASSIGNMENT] =entActivityAssignment.AssignmentDateSet.ToString("yyyy-MM-dd HH:mm:ss");

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_NO_DUE_DATE] = entActivityAssignment.IsNoDueDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_NO_DUE_DATE] = entActivityAssignment.IsNoDueDate ? 1 : 0;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_ASSIGN_DATE] = entActivityAssignment.IsDueBasedOnAssignDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_ASSIGN_DATE] = entActivityAssignment.IsDueBasedOnAssignDate ? 1 : 0;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_HIRE_DATE] = entActivityAssignment.IsDueBasedOnHireDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_HIRE_DATE] = entActivityAssignment.IsDueBasedOnHireDate ? 1 : 0;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_CREATION_DATE] = entActivityAssignment.IsDueBasedOnCreationDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_CREATION_DATE] = entActivityAssignment.IsDueBasedOnCreationDate ? 1 : 0;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_START_DATE] = entActivityAssignment.IsDueBasedOnStartDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_START_DATE] = entActivityAssignment.IsDueBasedOnStartDate ? 1 : 0;

                drow[Schema.ActivityAssignment.COL_DUE_AFTER_DAYS_OF] = entActivityAssignment.DueAfterDaysOf;

                if (DateTime.MinValue.CompareTo(entActivityAssignment.DueDateSet) < 0)
                    drow[Schema.ActivityAssignment.COL_DUE_DATE] = entActivityAssignment.DueDateSet.ToString("yyyy-MM-dd HH:mm:ss");

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_NO_EXPIRY_DATE] = entActivityAssignment.IsNoExpiryDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_NO_EXPIRY_DATE] = entActivityAssignment.IsNoExpiryDate ? 1 : 0;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_ASSIGN_DATE] = entActivityAssignment.IsExpiryBasedOnAssignDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_ASSIGN_DATE] = entActivityAssignment.IsExpiryBasedOnAssignDate ? 1 : 0;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_START_DATE] = entActivityAssignment.IsExpiryBasedOnStartDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_START_DATE] = entActivityAssignment.IsExpiryBasedOnStartDate ? 1 : 0;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_DUE_DATE] = entActivityAssignment.IsExpiryBasedOnDueDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_DUE_DATE] = entActivityAssignment.IsExpiryBasedOnDueDate ? 1 : 0;

                drow[Schema.ActivityAssignment.COL_EXPIRY_AFTER_DAYS_OF] = entActivityAssignment.ExpireAfterDaysOf;

                if (DateTime.MinValue.CompareTo(entActivityAssignment.ExpiryDateSet) < 0)
                    drow[Schema.ActivityAssignment.COL_EXPIRY_DATE] = entActivityAssignment.ExpiryDateSet.ToString("yyyy-MM-dd HH:mm:ss");

                //*** New Properties added for ReAssignment
                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_BASED_ON_ASSIGN_DATE] = entActivityAssignment.IsReAssignmentBasedOnAssignmentDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_BASED_ON_ASSIGN_DATE] = entActivityAssignment.IsReAssignmentBasedOnAssignmentDate ? 1 : 0;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_BASED_ON_ASSIGN_COMPL_DATE] = entActivityAssignment.IsReAssignmentBasedOnAssignmentCompletionDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_BASED_ON_ASSIGN_COMPL_DATE] = entActivityAssignment.IsReAssignmentBasedOnAssignmentCompletionDate ? 1 : 0;

                drow[Schema.ActivityAssignment.COL_REASSIGN_AFTER_DAYS_OF] = entActivityAssignment.ReAssignAfterDaysOf;

                if (DateTime.MinValue.CompareTo(entActivityAssignment.ReAssignmentDateSet) < 0)
                    drow[Schema.ActivityAssignment.COL_REASSIGNMENT_DATE_SET] = entActivityAssignment.ReAssignmentDateSet;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_NO_DUE_DATE] = entActivityAssignment.IsReassignNoDueDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_NO_DUE_DATE] = entActivityAssignment.IsReassignNoDueDate ? 1 : 0;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_DUE_BASED_ON_REASSIGN_DATE] = entActivityAssignment.IsReassignDueBasedOnReassignmentDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_DUE_BASED_ON_REASSIGN_DATE] = entActivityAssignment.IsReassignDueBasedOnReassignmentDate ? 1 : 0;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_DUE_BASED_ON_ASSIGN_COMPL_DATE] = entActivityAssignment.IsReassignDueBasedOnAssignmentCompletionDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_DUE_BASED_ON_ASSIGN_COMPL_DATE] = entActivityAssignment.IsReassignDueBasedOnAssignmentCompletionDate ? 1 : 0;

                drow[Schema.ActivityAssignment.COL_REASSIGN_DUE_AFTER_DAYS_OF] = entActivityAssignment.ReassignDueAfterDaysOf;

                if (DateTime.MinValue.CompareTo(entActivityAssignment.ReassignDueDateSet) < 0)
                    drow[Schema.ActivityAssignment.COL_REASSIGN_DUE_DATE] = entActivityAssignment.ReassignDueDateSet;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_NO_EXPIRY_DATE] = entActivityAssignment.IsReassignNoExpiryDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_NO_EXPIRY_DATE] = entActivityAssignment.IsReassignNoExpiryDate ? 1 : 0;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DATE] = entActivityAssignment.IsReassignExpiryBasedOnReassignmentDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DATE] = entActivityAssignment.IsReassignExpiryBasedOnReassignmentDate ? 1 : 0;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DUE_DATE] = entActivityAssignment.IsReassignExpiryBasedOnReassignmentDueDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DUE_DATE] = entActivityAssignment.IsReassignExpiryBasedOnReassignmentDueDate ? 1 : 0;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_ASSIGN_COMPL_DATE] = entActivityAssignment.IsReassignExpiryBasedOnAssignmentCompletionDate;
                else
                    drow[Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_ASSIGN_COMPL_DATE] = entActivityAssignment.IsReassignExpiryBasedOnAssignmentCompletionDate ? 1 : 0;

                drow[Schema.ActivityAssignment.COL_REASSIGN_EXPIRY_AFTER_DAYS_OF] = entActivityAssignment.ReassignExpireAfterDaysOf;

                if (DateTime.MinValue.CompareTo(entActivityAssignment.ReassignExpiryDateSet) < 0)
                    drow[Schema.ActivityAssignment.COL_REASSIGN_EXPIRY_DATE] =entActivityAssignment.ReassignExpiryDateSet;
                //******* New Properies set end here

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_SEND_EMAIL] = entActivityAssignment.SendEmail;
                else
                    drow[Schema.ActivityAssignment.COL_SEND_EMAIL] = entActivityAssignment.SendEmail ? 1 : 0;

                drow[Schema.ActivityAssignment.COL_SEND_EMAIL_TYPE] = entActivityAssignment.SendEmailType;
                drow[Schema.ActivityAssignment.COL_EMAIL_TEMPLATE_ID] = entActivityAssignment.EmailTemplateId;
                drow[Schema.Common.COL_MODIFIED_BY] = entActivityAssignment.LastModifiedById;

                if (pIsBulkImport)
                {
                    drow[Schema.ActivityAssignment.COL_OPERATIONMODE_REASSIGNMENT_FORBULK] = entActivityAssignment.OperationModeForBulkAssignment;
                    drow[Schema.ActivityAssignment.COL_IS_OVER_RIDE] = entActivityAssignment.OverridePreviousAssignments;
                }


                drow[Schema.Common.COL_UPDATE_MODE] = pStrMode;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_IS_ADMIN_ASSIGNMENT] = entActivityAssignment.IsForAdminPreview;
                else
                    drow[Schema.ActivityAssignment.COL_IS_ADMIN_ASSIGNMENT] = entActivityAssignment.IsForAdminPreview ? 1 : 0;

                if (pEntListActivityAssignment.Count == 1)
                    drow[Schema.ActivityAssignment.COL_FORCE_REASSIGNMENT] = entActivityAssignment.ForceReAssignment;
                else
                    drow[Schema.ActivityAssignment.COL_FORCE_REASSIGNMENT] = entActivityAssignment.ForceReAssignment ? 1 : 0;


                //drow[Schema.ActivityAssignment.COL_IS_CERTIFICATION_PROGRAM] = entActivityAssignment.IsCertificationProgram;
                _dtable.Rows.Add(drow);
                entListActivityAssignment.Add(entActivityAssignment);
                iBatchSize++;
            }

            if (_dtable.Rows.Count > 0)
            {
                _sqlcmd = new SqlCommand();
                if (ReRegister)
                {
                    _sqlcmd.CommandText = Schema.ActivityAssignment.PROC_UPDATE_ACTIVITY_ASSIGN_RERGISTER;
                }
                else if (pIsBulkImport)
                {
                    _sqlcmd.CommandText = Schema.ActivityAssignment.PROC_BULK_IMPORT_ASSIGNMENTS;
                }
                else
                {
                    _sqlcmd.CommandText = Schema.ActivityAssignment.PROC_UPDATE_ACTIVITY_ASSIGN;
                }

                _sqlcmd.CommandType = CommandType.StoredProcedure;

                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_USER_ID, SqlDbType.VarChar, 100, Schema.ActivityAssignment.COL_USER_ID);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_ACTIVITY_ID, SqlDbType.VarChar, 100, Schema.ActivityAssignment.COL_ACTIVITY_ID);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE_ID, SqlDbType.VarChar, 100, Schema.ActivityAssignment.COL_ACTIVITY_TYPE_ID);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_ASSIGNMENT_MODE_FOR_OVERRIDE, SqlDbType.VarChar, 100, Schema.ActivityAssignment.COL_ASSIGNMENT_MODE_FOR_OVERRIDE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_ASSIGNMENT_TYPE_ID, SqlDbType.VarChar, 100, Schema.ActivityAssignment.COL_ASSIGNMENT_TYPE_ID);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_COMPLETION_CONDITION_ID, SqlDbType.VarChar, 100, Schema.ActivityAssignment.COL_COMPLETION_CONDITION_ID);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_RULE_ID, SqlDbType.VarChar, 100, Schema.ActivityAssignment.COL_RULE_ID);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_LICENSE_ID, SqlDbType.VarChar, 100, Schema.ActivityAssignment.COL_LICENSE_ID);

                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_CURRENTLY_ASSIGNED, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_CURRENTLY_ASSIGNED);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_EDIT_FROM_UI, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_EDIT_FROM_UI);

                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_ASSIGNMENT_BASED_ON_HIRE_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_HIRE_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_ASSIGNMENT_BASED_ON_CREATION_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_ASSIGNMENT_BASED_ON_CREATION_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_ASSIGN_AFTER_DAYS_OF, SqlDbType.Int, 19, Schema.ActivityAssignment.COL_ASSIGN_AFTER_DAYS_OF);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_DATE_OF_ASSIGNMENT, SqlDbType.DateTime, 19, Schema.ActivityAssignment.COL_DATE_OF_ASSIGNMENT);

               

                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_NO_DUE_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_NO_DUE_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_DUE_BASED_ON_ASSIGN_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_ASSIGN_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_DUE_BASED_ON_HIRE_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_HIRE_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_DUE_BASED_ON_CREATION_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_CREATION_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_DUE_BASED_ON_START_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_DUE_BASED_ON_START_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_DUE_AFTER_DAYS_OF, SqlDbType.Int, 19, Schema.ActivityAssignment.COL_DUE_AFTER_DAYS_OF);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_DUE_DATE, SqlDbType.DateTime, 19, Schema.ActivityAssignment.COL_DUE_DATE);

                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_NO_EXPIRY_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_NO_EXPIRY_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_EXPIRY_BASED_ON_ASSIGN_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_ASSIGN_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_EXPIRY_BASED_ON_START_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_START_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_EXPIRY_BASED_ON_DUE_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_EXPIRY_BASE_ON_DUE_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_EXPIRY_AFTER_DAYS_OF, SqlDbType.Int, 19, Schema.ActivityAssignment.COL_EXPIRY_AFTER_DAYS_OF);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_EXPIRY_DATE, SqlDbType.DateTime, 19, Schema.ActivityAssignment.COL_EXPIRY_DATE);

                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_REASSIGN_BASED_ON_ASSIGN_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_REASSIGN_BASED_ON_ASSIGN_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_REASSIGN_AFTER_DAYS_OF, SqlDbType.Int, 19, Schema.ActivityAssignment.COL_REASSIGN_AFTER_DAYS_OF);

                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_SEND_EMAIL, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_SEND_EMAIL);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_SEND_EMAIL_TYPE, SqlDbType.VarChar, 100, Schema.ActivityAssignment.COL_SEND_EMAIL_TYPE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_EMAIL_TEMPLATE_ID, SqlDbType.VarChar, 100, Schema.ActivityAssignment.COL_EMAIL_TEMPLATE_ID);
                _sqlcmd.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);

                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_REASSIGN_BASED_ON_ASSIGN_COMPL_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_REASSIGN_BASED_ON_ASSIGN_COMPL_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_REASSIGNMENT_DATE_SET, SqlDbType.DateTime, 19, Schema.ActivityAssignment.COL_REASSIGNMENT_DATE_SET);

                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_REASSIGN_NO_DUE_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_REASSIGN_NO_DUE_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_REASSIGN_DUE_BASED_ON_REASSIGN_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_REASSIGN_DUE_BASED_ON_REASSIGN_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_REASSIGN_DUE_BASED_ON_ASSIGN_COMPL_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_REASSIGN_DUE_BASED_ON_ASSIGN_COMPL_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_REASSIGN_DUE_AFTER_DAYS_OF, SqlDbType.Int, 19, Schema.ActivityAssignment.COL_REASSIGN_DUE_AFTER_DAYS_OF);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_REASSIGN_DUE_DATE, SqlDbType.DateTime, 19, Schema.ActivityAssignment.COL_REASSIGN_DUE_DATE);

                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_REASSIGN_NO_EXPIRY_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_REASSIGN_NO_EXPIRY_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DUE_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_REASSIGN_DUE_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_REASSIGN_EXPIRY_BASED_ON_ASSIGN_COMPL_DATE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_REASSIGN_EXPIRY_BASED_ON_ASSIGN_COMPL_DATE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_REASSIGN_EXPIRY_AFTER_DAYS_OF, SqlDbType.Int, 19, Schema.ActivityAssignment.COL_REASSIGN_EXPIRY_AFTER_DAYS_OF);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_REASSIGN_EXPIRY_DATE, SqlDbType.DateTime, 19, Schema.ActivityAssignment.COL_REASSIGN_EXPIRY_DATE);

                if (pIsBulkImport)
                {
                    _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_OPERATIONMODE_REASSIGNMENT_FORBULK, SqlDbType.VarChar, 10, Schema.ActivityAssignment.COL_OPERATIONMODE_REASSIGNMENT_FORBULK);
                    _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_OVER_RIDE, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_OVER_RIDE);
                }
                _sqlcmd.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.VarChar, 100, Schema.Common.COL_UPDATE_MODE);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_ADMIN_ASSIGNMENT, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_ADMIN_ASSIGNMENT);
                _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_FORCE_REASSIGNMENT, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_FORCE_REASSIGNMENT);
                //  _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_CERTIFICATION_PROGRAM, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_CERTIFICATION_PROGRAM);

                _sqlcmd.Connection = _sqlcon;
                _sqlcmd.CommandTimeout = 0;
                _sqladapter.InsertCommand = _sqlcmd;
                _sqladapter.InsertCommand.CommandTimeout = 0;
                _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                _sqladapter.UpdateBatchSize = iBatchSize;
                _sqladapter.Update(_dtable);

                //-aw 9/7/2011 LCP-21088 Assign users based on business rules
                //UpdateDynamicAssignments(strClientId);

            }
            return entListActivityAssignment;
        }

        /// <summary>
        /// Edit one Time Assignment - only updates IsCurrentlyAssigned=true/false.
        /// </summary>
        /// <param name="pEntListActivityAssignment"></param>
        /// <returns></returns>
        public List<ActivityAssignment> EditAssignments(List<ActivityAssignment> pEntListActivityAssignment)
        {
            _sqlObject = new SQLObject();
            List<ActivityAssignment> entListActivityAssignment = new List<ActivityAssignment>();
            string strConnectionString = string.Empty;
            int iBatchSize = 0;
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();

            try
            {
                strConnectionString = _sqlObject.GetClientDBConnString((pEntListActivityAssignment[0]).ClientId);
                _sqlcon = new SqlConnection(strConnectionString);
                //Add Columns
                _dtable.Columns.Add(Schema.ActivityAssignment.COL_USER_ID);
                _dtable.Columns.Add(Schema.ActivityAssignment.COL_ACTIVITY_ID);
                _dtable.Columns.Add(Schema.ActivityAssignment.COL_IS_CURRENTLY_ASSIGNED);
                _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);

                foreach (ActivityAssignment entActivityAssignment in pEntListActivityAssignment)
                {
                    DataRow drow = _dtable.NewRow();
                    drow[Schema.ActivityAssignment.COL_USER_ID] = entActivityAssignment.UserID;
                    drow[Schema.ActivityAssignment.COL_ACTIVITY_ID] = entActivityAssignment.ID;
                    drow[Schema.ActivityAssignment.COL_IS_CURRENTLY_ASSIGNED] = entActivityAssignment.IsCurrentlyAssigned;
                    drow[Schema.Common.COL_MODIFIED_BY] = entActivityAssignment.LastModifiedById;
                    _dtable.Rows.Add(drow);
                    entListActivityAssignment.Add(entActivityAssignment);
                    iBatchSize++;
                }

                if (_dtable.Rows.Count > 0)
                {
                    _sqlcmd = new SqlCommand();
                    _sqlcmd.CommandText = Schema.ActivityAssignment.PROC_UPDATE_ACTIVITY_ASSIGNMENT;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;
                    _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_USER_ID, SqlDbType.VarChar, 100, Schema.ActivityAssignment.COL_USER_ID);
                    _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_ACTIVITY_ID, SqlDbType.VarChar, 100, Schema.ActivityAssignment.COL_ACTIVITY_ID);
                    _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_IS_CURRENTLY_ASSIGNED, SqlDbType.Bit, 1, Schema.ActivityAssignment.COL_IS_CURRENTLY_ASSIGNED);
                    _sqlcmd.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                    _sqlcmd.Connection = _sqlcon;
                    _sqladapter.InsertCommand = _sqlcmd;
                    _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    _sqladapter.UpdateBatchSize = iBatchSize;
                    _sqladapter.Update(_dtable);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListActivityAssignment;
        }


        /// <summary>
        /// Save Selected Users
        /// </summary>
        /// <param name="pEntListLearner"></param>
        /// <returns></returns>
        public List<Learner> SaveSelectedLearner(List<Learner> pEntListLearner)
        {
            _sqlObject = new SQLObject();
            List<Learner> entListLearner = new List<Learner>();
            string strConnectionString = string.Empty;
            int iBatchSize = 0;
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();

            try
            {
                strConnectionString = _sqlObject.GetClientDBConnString((pEntListLearner[0]).ClientId);
                _sqlcon = new SqlConnection(strConnectionString);
                //Add Columns
                //_dtable.Columns.Add(Schema.Learner.COL_ID);
                _dtable.Columns.Add(Schema.Learner.COL_USER_ID);
                _dtable.Columns.Add(Schema.Learner.COL_LEARNER_NAME);
                //_dtable.Columns.Add(Schema.Learner.COL_ActivityId);
                //_dtable.Columns.Add(Schema.Learner.COL_ACTIVITY_NAME);
                _dtable.Columns.Add(Schema.Common.COL_CREATED_BY);

                foreach (Learner entlearner in pEntListLearner)
                {
                    DataRow drow = _dtable.NewRow();
                  //  drow[Schema.Learner.COL_ID] = entlearner.ID;
                    drow[Schema.Learner.COL_USER_ID] = entlearner.SystemUserGUID;
                    drow[Schema.Learner.COL_LEARNER_NAME] = Convert.ToString(entlearner.FirstName) + " "+ Convert.ToString(entlearner.MiddleName) + " " + Convert.ToString(entlearner.LastName);
                   // drow[Schema.Learner.COL_ActivityId] = entlearner.ActivityId;
                   // drow[Schema.Learner.COL_ACTIVITY_NAME] = entlearner.ActivityName;
                    drow[Schema.Common.COL_CREATED_BY] = entlearner.CreatedById;
                    _dtable.Rows.Add(drow);
                    entListLearner.Add(entlearner);
                    iBatchSize++;
                }

                if (_dtable.Rows.Count > 0)
                {
                    _sqlcmd = new SqlCommand();
                    _sqlcmd.CommandText = Schema.Learner.PROC_SAVE_SELECTED_USER_FOR_ONETIMEASSIGNMENT;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;
                    _sqlcmd.Parameters.Add(Schema.Learner.PARA_USER_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_USER_ID);
                    _sqlcmd.Parameters.Add(Schema.Learner.PARA_LEARNER_NAME, SqlDbType.VarChar, 200, Schema.Learner.COL_LEARNER_NAME);
                    //_sqlcmd.Parameters.Add(Schema.Learner.PARA_ACTIVITY_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_ActivityId);
                    //_sqlcmd.Parameters.Add(Schema.Learner.PARA_ACTIVITY_NAME, SqlDbType.VarChar, 100, Schema.Learner.COL_ACTIVITY_NAME);
                    _sqlcmd.Parameters.Add(Schema.Common.PARA_CREATED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_CREATED_BY);
                    _sqlcmd.Connection = _sqlcon;
                    _sqladapter.InsertCommand = _sqlcmd;
                    _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    _sqladapter.UpdateBatchSize = iBatchSize;
                    _sqladapter.Update(_dtable);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, strConnectionString);
                throw _expCustom;
            }
            return entListLearner;
        }


        /// <summary>
        /// Save Selected Activity
        /// </summary>
        /// <param name="pEntListActivityAssignment"></param>
        /// <returns></returns>
        public List<ActivityAssignment> SaveSelectedActivity(List<ActivityAssignment> pEntListActivityAssignment)
        {
            _sqlObject = new SQLObject();
            List<ActivityAssignment> entListActivityAssignment = new List<ActivityAssignment>();
            string strConnectionString = string.Empty;
            int iBatchSize = 0;
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();

            try
            {
                strConnectionString = _sqlObject.GetClientDBConnString((pEntListActivityAssignment[0]).ClientId);
                _sqlcon = new SqlConnection(strConnectionString);
                //Add Columns
                //_dtable.Columns.Add(Schema.Learner.COL_ID);
                //_dtable.Columns.Add(Schema.Learner.COL_USER_ID);
                //_dtable.Columns.Add(Schema.Learner.COL_LEARNER_NAME);
                _dtable.Columns.Add(Schema.ActivityAssignment.COL_ACTIVITY_ID);
                _dtable.Columns.Add(Schema.ActivityAssignment.COL_ACTIVITY_NAME);
                _dtable.Columns.Add(Schema.Common.COL_CREATED_BY);

                foreach (ActivityAssignment entActivityAssignment in pEntListActivityAssignment)
                {
                    DataRow drow = _dtable.NewRow();
                    //  drow[Schema.Learner.COL_ID] = entlearner.ID;
                    //drow[Schema.Learner.COL_USER_ID] = entlearner.SystemUserGUID;
                    //drow[Schema.Learner.COL_LEARNER_NAME] = entlearner.UserNameAlias;
                    drow[Schema.ActivityAssignment.COL_ACTIVITY_ID] = entActivityAssignment.ActivityID;
                    drow[Schema.ActivityAssignment.COL_ACTIVITY_NAME] = entActivityAssignment.ActivityName;
                    drow[Schema.Common.COL_CREATED_BY] = entActivityAssignment.CreatedById;
                    _dtable.Rows.Add(drow);
                    entListActivityAssignment.Add(entActivityAssignment);
                    iBatchSize++;
                }

                if (_dtable.Rows.Count > 0)
                {
                    _sqlcmd = new SqlCommand();
                    _sqlcmd.CommandText = Schema.ActivityAssignment.PROC_SAVE_SELETED_ACTIVITY_FOR_ONETIMEASSIGNMENT;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;
                    //_sqlcmd.Parameters.Add(Schema.Learner.PARA_USER_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_USER_ID);
                    //_sqlcmd.Parameters.Add(Schema.Learner.PARA_LEARNER_NAME, SqlDbType.VarChar, 200, Schema.Learner.COL_USER_ID);
                    _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_ACTIVITY_ID, SqlDbType.VarChar, 100, Schema.ActivityAssignment.COL_ACTIVITY_ID);
                    _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_ACTIVITY_NAME, SqlDbType.VarChar, 100, Schema.ActivityAssignment.COL_ACTIVITY_NAME);
                    _sqlcmd.Parameters.Add(Schema.Common.PARA_CREATED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_CREATED_BY);
                    _sqlcmd.Connection = _sqlcon;
                    _sqladapter.InsertCommand = _sqlcmd;
                    _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    _sqladapter.UpdateBatchSize = iBatchSize;
                    _sqladapter.Update(_dtable);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, strConnectionString);
                throw _expCustom;
            }
            return entListActivityAssignment;
        }


        /// <summary>
        /// Delete Selected Users
        /// </summary>
        /// <param name="pEntListLearner"></param>
        /// <returns></returns>
        public List<Learner> DeleteSelectedLearner(List<Learner> pEntListLearner)
        {
            _sqlObject = new SQLObject();
            List<Learner> entListLearner = new List<Learner>();
            string strConnectionString = string.Empty;
            int iBatchSize = 0;
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();

            try
            {
                strConnectionString = _sqlObject.GetClientDBConnString((pEntListLearner[0]).ClientId);
                _sqlcon = new SqlConnection(strConnectionString);
                //Add Columns
                _dtable.Columns.Add(Schema.Learner.COL_ID);
                //_dtable.Columns.Add(Schema.Learner.COL_USER_ID);
                //_dtable.Columns.Add(Schema.Learner.COL_LEARNER_NAME);
                //_dtable.Columns.Add(Schema.Learner.COL_ActivityId);
                //_dtable.Columns.Add(Schema.Learner.COL_ACTIVITY_NAME);
                _dtable.Columns.Add(Schema.Common.COL_CREATED_BY);

                foreach (Learner entlearner in pEntListLearner)
                {
                    DataRow drow = _dtable.NewRow();
                    drow[Schema.Learner.COL_ID] = entlearner.ID;
                    //drow[Schema.Learner.COL_USER_ID] = entlearner.SystemUserGUID;
                    //drow[Schema.Learner.COL_LEARNER_NAME] = entlearner.UserNameAlias;
                    //drow[Schema.Learner.COL_ActivityId] = entlearner.ActivityId;
                    //drow[Schema.Learner.COL_ACTIVITY_NAME] = entlearner.ActivityName;
                    drow[Schema.Common.COL_CREATED_BY] = entlearner.CreatedById;
                    _dtable.Rows.Add(drow);
                    entListLearner.Add(entlearner);
                    iBatchSize++;
                }

                if (_dtable.Rows.Count > 0)
                {
                    _sqlcmd = new SqlCommand();
                    _sqlcmd.CommandText = Schema.Learner.PROC_DELETE_SELECTED_USER_FOR_ONETIMEASSIGNMENT;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;
                    _sqlcmd.Parameters.Add(Schema.Learner.PARA_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_ID);
                    //_sqlcmd.Parameters.Add(Schema.Learner.PARA_USER_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_USER_ID);
                    //_sqlcmd.Parameters.Add(Schema.Learner.PARA_LEARNER_NAME, SqlDbType.VarChar, 100, Schema.Learner.COL_USER_ID);
                    //_sqlcmd.Parameters.Add(Schema.Learner.PARA_ACTIVITY_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_ActivityId);
                    //_sqlcmd.Parameters.Add(Schema.Learner.PARA_ACTIVITY_NAME, SqlDbType.Bit, 1, Schema.Learner.COL_ACTIVITY_NAME);
                    _sqlcmd.Parameters.Add(Schema.Common.PARA_CREATED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_CREATED_BY);
                    _sqlcmd.Connection = _sqlcon;
                    _sqladapter.InsertCommand = _sqlcmd;
                    _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    _sqladapter.UpdateBatchSize = iBatchSize;
                    _sqladapter.Update(_dtable);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, strConnectionString);
                throw _expCustom;
            }
            return entListLearner;
        }



        /// <summary>
        /// Delete Selected Users
        /// </summary>
        /// <param name="pEntListLearner"></param>
        /// <returns></returns>
        public List<ActivityAssignment> DeleteSelectedActivity(List<ActivityAssignment> pEntListActivityAssignment)
        {
            _sqlObject = new SQLObject();
            List<ActivityAssignment> entListActivityAssignment = new List<ActivityAssignment>();
            string strConnectionString = string.Empty;
            int iBatchSize = 0;
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();

            try
            {
                strConnectionString = _sqlObject.GetClientDBConnString((pEntListActivityAssignment[0]).ClientId);
                _sqlcon = new SqlConnection(strConnectionString);
                //Add Columns
                _dtable.Columns.Add(Schema.ActivityAssignment.COL_ACTIVITY_ID);
                //_dtable.Columns.Add(Schema.Learner.COL_USER_ID);
                //_dtable.Columns.Add(Schema.Learner.COL_LEARNER_NAME);
                //_dtable.Columns.Add(Schema.Learner.COL_ActivityId);
                //_dtable.Columns.Add(Schema.Learner.COL_ACTIVITY_NAME);
                _dtable.Columns.Add(Schema.Common.COL_CREATED_BY);

                foreach (ActivityAssignment entActivityAssignment in pEntListActivityAssignment)
                {
                    DataRow drow = _dtable.NewRow();
                    drow[Schema.ActivityAssignment.COL_ACTIVITY_ID] = entActivityAssignment.ActivityID;
                    //drow[Schema.Learner.COL_USER_ID] = entlearner.SystemUserGUID;
                    //drow[Schema.Learner.COL_LEARNER_NAME] = entlearner.UserNameAlias;
                    //drow[Schema.Learner.COL_ActivityId] = entlearner.ActivityId;
                    //drow[Schema.Learner.COL_ACTIVITY_NAME] = entlearner.ActivityName;
                    drow[Schema.Common.COL_CREATED_BY] = entActivityAssignment.CreatedById;
                    _dtable.Rows.Add(drow);
                    entListActivityAssignment.Add(entActivityAssignment);
                    iBatchSize++;
                }

                if (_dtable.Rows.Count > 0)
                {
                    _sqlcmd = new SqlCommand();
                    _sqlcmd.CommandText = Schema.ActivityAssignment.PROC_DELETE_SELETED_ACTIVITY_FOR_ONETIMEASSIGNMENT;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;
                    _sqlcmd.Parameters.Add(Schema.ActivityAssignment.PARA_ACTIVITY_ID, SqlDbType.VarChar, 100, Schema.ActivityAssignment.COL_ACTIVITY_ID);
                    //_sqlcmd.Parameters.Add(Schema.Learner.PARA_USER_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_USER_ID);
                    //_sqlcmd.Parameters.Add(Schema.Learner.PARA_LEARNER_NAME, SqlDbType.VarChar, 100, Schema.Learner.COL_USER_ID);
                    //_sqlcmd.Parameters.Add(Schema.Learner.PARA_ACTIVITY_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_ActivityId);
                    //_sqlcmd.Parameters.Add(Schema.Learner.PARA_ACTIVITY_NAME, SqlDbType.Bit, 1, Schema.Learner.COL_ACTIVITY_NAME);
                    _sqlcmd.Parameters.Add(Schema.Common.PARA_CREATED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_CREATED_BY);
                    _sqlcmd.Connection = _sqlcon;
                    _sqladapter.InsertCommand = _sqlcmd;
                    _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    _sqladapter.UpdateBatchSize = iBatchSize;
                    _sqladapter.Update(_dtable);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, strConnectionString);
                throw _expCustom;
            }
            return entListActivityAssignment;
        }



        /// <summary>
        /// To get user details by user id.
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>

        public List<ActivityAssignment> GetActivityByRequestedID(ActivityAssignment pEntActivityAssignment)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.ActivityAssignment.PROC_GET_SELECTED_ACTIVITY;
            _sqlObject = new SQLObject();
            List<ActivityAssignment> entListActivityAssignments = new List<ActivityAssignment>();
            ActivityAssignment entActivityAssignments = new ActivityAssignment();
            if (!String.IsNullOrEmpty(pEntActivityAssignment.CreatedById))
                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntActivityAssignment.CreatedById.ToString());
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntActivityAssignment.ListRange != null)
            {
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                _sqlcmd.Parameters.Add(_sqlpara);

                //For Scope
                //  _sqlpara = new SqlParameter(Schema.Learner.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
                // _sqlcmd.Parameters.Add(_sqlpara);
            }
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    entActivityAssignments = FillActivity(_sqlreader, _sqlObject);
                    entListActivityAssignments.Add(entActivityAssignments);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListActivityAssignments;
        }



        /// <summary>
        /// Get User Activity Attempts
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <param name="pIsPrevious"></param>
        /// <returns></returns>
        public List<ActivityAssignment> GetUserActivityAttempts(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            SqlDataReader sqlreader = null;
            List<ActivityAssignment> entListLearnerAssignments = null;
            ActivityAssignment entAssignment = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.Connection = sqlConnection;
                _sqlcmd.CommandText = Schema.ActivityAssignment.PROC_GET_CURRENT_ATTEMTPS;

                if (!string.IsNullOrEmpty(pEntActivityAssignment.UserID))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ID))
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ParaLanguageId))
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntActivityAssignment.ParaLanguageId);
                else
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListLearnerAssignments = new List<ActivityAssignment>();
                while (sqlreader.Read())
                {
                    entAssignment = FillActivity(sqlreader, _sqlObject);
                    entListLearnerAssignments.Add(entAssignment);
                }
            }
            catch (Exception expCommon)
            {
               _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            finally
            {
                if (sqlreader != null && !sqlreader.IsClosed) sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entListLearnerAssignments;
        }


        /// <summary>
        /// Get User Activity Attempts
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <param name="pIsPrevious"></param>
        /// <returns></returns>
        public List<ActivityAssignment> GetUserActivityAttemptsForMaxCourseScore(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            SqlDataReader sqlreader = null;
            List<ActivityAssignment> entListLearnerAssignments = null;
            ActivityAssignment entAssignment = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.Connection = sqlConnection;
                _sqlcmd.CommandText = Schema.ActivityAssignment.PROC_GET_CURRENT_ATTEMTPS_MAXSCORECOURSE;

                if (!string.IsNullOrEmpty(pEntActivityAssignment.UserID))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ID))
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.ParaLanguageId))
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntActivityAssignment.ParaLanguageId);
                else
                    _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntActivityAssignment.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntActivityAssignment.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntActivityAssignment.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntActivityAssignment.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListLearnerAssignments = new List<ActivityAssignment>();
                while (sqlreader.Read())
                {
                    entAssignment = FillActivity(sqlreader, _sqlObject);
                    entListLearnerAssignments.Add(entAssignment);
                }
            }
            catch (Exception expCommon)
            {
               _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlreader != null && !sqlreader.IsClosed) sqlreader.Close();
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed) sqlConnection.Close();
            }
            return entListLearnerAssignments;
        }

        /// <summary>
        /// Used in Admin DashBoard
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns></returns>
        private ActivityAssignment FillActivityList(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            EntityRange entRange = null;
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ID = pSqlReader.GetString(iIndex);

                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ActivityName = pSqlReader.GetString(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_COURSE_HOME_PAGE_URL))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_COURSE_HOME_PAGE_URL);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ActivityURL = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_SUBTYPE_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_SUBTYPE_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ActivitySubTypeID = pSqlReader.GetString(iIndex);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_IS_ACTIVE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_IS_ACTIVE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsActiveActivity = pSqlReader.GetBoolean(iIndex);
                }

                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_TYPE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ActivityType = (ActivityContentType)Enum.Parse(typeof(ActivityContentType), pSqlReader.GetString(iIndex));

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_ON))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.DateCreated = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY_NAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.CreatedByName = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                {

                    entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    entActivityAssignment.ListRange = entRange;
                }

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.Category.COL_CATEGORYNAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Category.COL_CATEGORYNAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.CategoryName = pSqlReader.GetString(iIndex);

                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.SubCategory.COL_SUBCATEGORYNAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.SubCategory.COL_SUBCATEGORYNAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.SubCategoryName = pSqlReader.GetString(iIndex);

                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModule.COL_COURSE_ASSESSMENT_FLAG))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModule.COL_COURSE_ASSESSMENT_FLAG);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.CouseAssessmentFlag = pSqlReader.GetBoolean(iIndex);
                }
            }
            return entActivityAssignment;
        }

        /// <summary>
        /// Used in Assignment - Bulk Import
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns></returns>
        private ActivityAssignment FillActivityListOptimized(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            EntityRange entRange = null;
            int iIndex;
            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_ID))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_ID);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ID = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_NAME))
                {

                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_NAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ActivityName = pSqlReader.GetString(iIndex);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_IS_ACTIVE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_IS_ACTIVE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsActiveActivity = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ACTIVITY_TYPE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_TYPE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ActivityType = (ActivityContentType)Enum.Parse(typeof(ActivityContentType), pSqlReader.GetString(iIndex));
                }





                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                {

                    entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    entActivityAssignment.ListRange = entRange;
                }

            }
            return entActivityAssignment;
        }


        /// <summary>
        /// Unassign activity to all user s
        /// </summary>
        /// <returns></returns>
        public ActivityAssignment UnassignByActivity(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandTimeout = 0;
                _sqlcmd.CommandText = Schema.ActivityAssignment.PROC_UNASSIGN_BY_ACTIVITY;
                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById))//Add Bharat: 02-Dec-2013
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REQUESTEDBY_ID, pEntActivityAssignment.CreatedById);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REQUESTEDBY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntActivityAssignment.AssignFromDate) < 0)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNMENT_DATE_FROM, pEntActivityAssignment.AssignFromDate);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (DateTime.MinValue.CompareTo(pEntActivityAssignment.AssignToDate) < 0)
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNMENT_DATE_TO, pEntActivityAssignment.AssignToDate);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                int assignStatus = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                if (assignStatus < 0)
                {
                    pEntActivityAssignment = null;
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntActivityAssignment;
        }

        /// <summary>
        /// To group edit assignments
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<ActivityAssignment> GroupEditAssignments(Search pEntSearch)
        {
            _sqlObject = new SQLObject();
            List<ActivityAssignment> entListAssignment = new List<ActivityAssignment>();
            ActivityAssignment entAAssignment = new ActivityAssignment();
            EntityRange entRange = new EntityRange();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandTimeout = 0;
                _sqlcmd.CommandText = Schema.ActivityAssignment.PROC_GROUP_ASSIGNMENT_EDIT;

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntSearch.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntSearch.ListRange != null)
                {
                    if (!string.IsNullOrEmpty(pEntSearch.ListRange.RequestedById))
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                }
                if (pEntSearch.SearchObject.Count > 0)
                {
                    entAAssignment = new ActivityAssignment();
                    entAAssignment = (ActivityAssignment)pEntSearch.SearchObject[0];
                    if (DateTime.MinValue.CompareTo(entAAssignment.AssignmentDateSet) < 0)
                    {
                        _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNMENT_DATE_FROM, entAAssignment.AssignmentDateSet);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (pEntSearch.SearchObject.Count > 1)
                    {
                        entAAssignment = new ActivityAssignment();
                        entAAssignment = (ActivityAssignment)pEntSearch.SearchObject[1];
                        if (DateTime.MinValue.CompareTo(entAAssignment.AssignmentDateSet) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGNMENT_DATE_TO, entAAssignment.AssignmentDateSet);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                    if (pEntSearch.SearchObject.Count > 2)
                    {
                        entAAssignment = new ActivityAssignment();
                        entAAssignment = (ActivityAssignment)pEntSearch.SearchObject[2];

                        _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_ASSIGNMENT_BASED_ON_HIRE_DATE, entAAssignment.IsAssignmentBasedOnHireDate);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_ASSIGNMENT_BASED_ON_CREATION_DATE, entAAssignment.IsAssignmentBasedOnCreationDate);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ASSIGN_AFTER_DAYS_OF, entAAssignment.AssignAfterDaysOf);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        if (DateTime.MinValue.CompareTo(entAAssignment.AssignmentDateSet) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_DATE_OF_ASSIGNMENT, entAAssignment.AssignmentDateSet);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }

                        _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_NO_DUE_DATE, entAAssignment.IsNoDueDate);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_DUE_BASED_ON_ASSIGN_DATE, entAAssignment.IsDueBasedOnAssignDate);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_DUE_BASED_ON_HIRE_DATE, entAAssignment.IsDueBasedOnHireDate);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_DUE_BASED_ON_CREATION_DATE, entAAssignment.IsDueBasedOnCreationDate);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_DUE_BASED_ON_START_DATE, entAAssignment.IsDueBasedOnStartDate);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_DUE_AFTER_DAYS_OF, entAAssignment.DueAfterDaysOf);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        if (DateTime.MinValue.CompareTo(entAAssignment.DueDateSet) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_DUE_DATE, entAAssignment.DueDateSet);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }

                        _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_NO_EXPIRY_DATE, entAAssignment.IsNoExpiryDate);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_EXPIRY_BASED_ON_ASSIGN_DATE, entAAssignment.IsExpiryBasedOnAssignDate);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_EXPIRY_BASED_ON_START_DATE, entAAssignment.IsExpiryBasedOnStartDate);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_IS_EXPIRY_BASED_ON_DUE_DATE, entAAssignment.IsExpiryBasedOnDueDate);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_EXPIRY_AFTER_DAYS_OF, entAAssignment.ExpireAfterDaysOf);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        if (DateTime.MinValue.CompareTo(entAAssignment.ExpiryDateSet) < 0)
                        {
                            _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_EXPIRY_DATE, entAAssignment.ExpiryDateSet);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                }
                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
               _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListAssignment;
        }

        /// <summary>
        /// Get User Assignment List For Email
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public List<ActivityAssignment> GetUserAssignmentListForEmail(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            List<ActivityAssignment> entListLearnerAssignments = null;
            ActivityAssignment entAssignment = null;
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_GET_RULE_USER_ACTIVITY_EMAIL, sqlConnection);

                //ID is DistributionListID
                if (!string.IsNullOrEmpty(pEntActivityAssignment.ID))
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_DISTRIBUTION_LIST_ID, pEntActivityAssignment.ID);
                else
                    _sqlpara = new SqlParameter(Schema.EmailDistributionList.PARA_DISTRIBUTION_LIST_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);


                if (!string.IsNullOrEmpty(pEntActivityAssignment.UserID))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntActivityAssignment.UserID);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListLearnerAssignments = new List<ActivityAssignment>();
                while (_sqlreader.Read())
                {
                    entAssignment = FillObjectForEmail(_sqlreader, _sqlObject);
                    entListLearnerAssignments.Add(entAssignment);
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
            return entListLearnerAssignments;
        }

        /// <summary>
        /// Fill Reader Object
        /// </summary>
        /// <param name="pReader"></param>
        /// <returns>ActivityAssignment Object</returns>
        private ActivityAssignment FillObjectForEmail(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            EntityRange entRange = new EntityRange();
            int iIndex;

            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                {
                    entActivityAssignment.ID = pSqlReader.GetString(iIndex);
                    entActivityAssignment.ActivityID = entActivityAssignment.ID;
                }

                iIndex = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.UserID = pSqlReader.GetString(iIndex);


                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_NAME);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ActivityName = pSqlReader.GetString(iIndex);
                else
                    entActivityAssignment.ActivityName = "";


                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_TYPE);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ActivityType = (ActivityContentType)Enum.Parse(typeof(ActivityContentType), pSqlReader.GetString(iIndex));

                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_STATUS);
                if (!pSqlReader.IsDBNull(iIndex))
                {

                    entActivityAssignment.ActivityStatus = (ActivityCompletionStatus)Enum.Parse(typeof(ActivityCompletionStatus), pSqlReader.GetString(iIndex));
                    entActivityAssignment.AssignmentStatus = Convert.ToString(entActivityAssignment.ActivityStatus);
                }
                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ASSIGNMENT_STATUS);
                if (!pSqlReader.IsDBNull(iIndex))
                {
                    entActivityAssignment.AssignmentStatus = pSqlReader.GetString(iIndex);
                }

                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_DATE_OF_ASSIGNMENT);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.AssignmentDateSet = pSqlReader.GetDateTime(iIndex);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_DUE_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_DUE_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.DueDateSet = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_EXPIRY_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_EXPIRY_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ExpiryDateSet = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ActivityAssignment.COL_ASSIGNMENT_COMPLETION_DATE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_ASSIGNMENT_COMPLETION_DATE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.AssignmentDateOfCompletion = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleTracking.COL_DATE_OF_START))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleTracking.COL_DATE_OF_START);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.DateOfStart = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.ContentModuleTracking.COL_DATE_OF_START))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.ContentModuleTracking.COL_DATE_OF_START);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.DateOfStart = pSqlReader.GetDateTime(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, "ReviewerComments"))
                {
                    iIndex = pSqlReader.GetOrdinal("ReviewerComments");
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.ReviewerComments = pSqlReader.GetString(iIndex);
                }



            }
            return entActivityAssignment;
        }

        //-aw 9/7/2011 LCP-21088 Assign users based on business rules
        /*public void UpdateDynamicAssignments(string ClientId)
		{
			_sqlObject = new SQLObject();
			try
			{
				_strConnString = _sqlObject.GetClientDBConnString(ClientId);
				_sqlcmd = new SqlCommand();
				_sqlcmd.CommandText = Schema.ActivityAssignment.PROC_REFRESH_DYNAMIC_ASSIGNMENT;

				_sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
			}
			catch (Exception expCommon)
			{
				_expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
				throw _expCustom;
			}
		}*/
        private Object FindIsProductExceedLicense(ActivityAssignment pEntActivityAssignment)
        {
            Object obj = true;
            _sqlObject = new SQLObject();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_PRODUCT_LICENSE_EXCEED, sqlConnection);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ActivityID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_USER_ID, pEntActivityAssignment.UserID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REQUESTEDBY_ID, pEntActivityAssignment.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
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
            return obj;
        }
        public List<ActivityAssignment> GetInnerActivity(ActivityAssignment pEntActivityAssignment)
        {

            List<ActivityAssignment> entListLearnerAssignments = null;
            ActivityAssignment entAssignment = null;
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_GET_INNER_ACTIVITY_OFCURRIANDCERTIFICATION, sqlConnection);

                //ID is DistributionListID
                if (!string.IsNullOrEmpty(pEntActivityAssignment.ID))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);

                }
                if (!string.IsNullOrEmpty(pEntActivityAssignment.ActivityTypeId))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE_ID, pEntActivityAssignment.ActivityTypeId);

                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntActivityAssignment.UserID))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_USER_ID, pEntActivityAssignment.UserID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                entListLearnerAssignments = new List<ActivityAssignment>();
                while (_sqlreader.Read())
                {
                    entAssignment = FillObject_InnerActivity(_sqlreader, _sqlObject);
                    entListLearnerAssignments.Add(entAssignment);
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
            return entListLearnerAssignments;
        }


        public DataSet GetVirtualClassroomNominationDetailsDuringOneTimeAssignment(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            SqlConnection sqlConnection = null;
            DataSet dsReturn = new DataSet();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_GetVirtualClassroomNominationDetailsDuringOneTimeAssignment, sqlConnection);

                if (!(string.IsNullOrEmpty(pEntActivityAssignment.ID)))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!(string.IsNullOrEmpty(pEntActivityAssignment.ActivityTypeId)))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_TYPE_ID, pEntActivityAssignment.ActivityTypeId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntActivityAssignment.UserID))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_USER_ID, pEntActivityAssignment.UserID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntActivityAssignment.CreatedById))//Add Bharat: 02-Dec-2013
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REQUESTEDBY_ID, pEntActivityAssignment.CreatedById);
                else
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REQUESTEDBY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntActivityAssignment.UserName))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_UserIdList, pEntActivityAssignment.UserName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                dsReturn = SQLHelper.SqlDataAdapter(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return dsReturn;
        }


        public DataSet Delete_VT_CT_AtTimeOf_EditAssignment(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            SqlConnection sqlConnection = null;
            DataSet dsReturn = new DataSet();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_DELETE_VTANDCTATTIMEOD_EDITASSIGNMENT, sqlConnection);

                if (!(string.IsNullOrEmpty(pEntActivityAssignment.ID)))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }


                if (!string.IsNullOrEmpty(pEntActivityAssignment.UserID))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_USER_ID, pEntActivityAssignment.UserID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }


                dsReturn = SQLHelper.SqlDataAdapter(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
               _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return dsReturn;
        }

        public DataSet GetVirtualClassroomNominationRegistrationCountOneTimeAssignment(ActivityAssignment pEntActivityAssignment)
        {
            _sqlObject = new SQLObject();
            SqlConnection sqlConnection = null;
            DataSet dsReturn = new DataSet();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntActivityAssignment.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.ActivityAssignment.PROC_GetVirtualClassroomNominationRegistrationCountOneTimeAssignment, sqlConnection);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntActivityAssignment.ActivityID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_USER_ID, pEntActivityAssignment.UserID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_REQUESTEDBY_ID, pEntActivityAssignment.LastModifiedById);
                _sqlcmd.Parameters.Add(_sqlpara);

                dsReturn = SQLHelper.SqlDataAdapter(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return dsReturn;
        }
        #region Interface Methods
        /// <summary>
        /// Get By IsProductExceedLicense
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public Object IsProductExceedLicense(ActivityAssignment pEntActivityAssignment)
        {
            return FindIsProductExceedLicense(pEntActivityAssignment);
        }

        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public ActivityAssignment Get(ActivityAssignment pEntActivityAssignment)
        {
            return GetActivityAssignmentByID(pEntActivityAssignment);
        }
        /// <summary>
        /// Update ActivityAssignment
        /// </summary>
        /// <param name="pEntActivityAssignment"></param>
        /// <returns></returns>
        public ActivityAssignment Update(ActivityAssignment pEntActivityAssignment)
        {
            return EditActivityAssignment(pEntActivityAssignment);
        }
        #endregion

        public ActivityAssignment GetForCoursePlayer(string clientId, string courseId, string learnerId)
        {
            return
                CheckUserAssignmentByID_CoursePlayer(new ActivityAssignment
                {
                    ClientId = clientId,
                    ID = courseId,
                    UserID = learnerId
                });
        }


        private ActivityAssignment FillCategoryList(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            ActivityAssignment entActivityAssignment = new ActivityAssignment();
            EntityRange entRange = null;
            int iIndex;
            if (pSqlReader.HasRows)
            {
                iIndex = pSqlReader.GetOrdinal(Schema.ActivityAssignment.COL_CATEGORY_ID);
                if (!pSqlReader.IsDBNull(iIndex))
                    entActivityAssignment.ID = pSqlReader.GetString(iIndex);

                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.Category.COL_CATEGORYNAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Category.COL_CATEGORYNAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.CategoryName = pSqlReader.GetString(iIndex);

                }
                if (SQLHelper.ReaderHasColumn(pSqlReader, Schema.SubCategory.COL_SUBCATEGORYNAME))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.SubCategory.COL_SUBCATEGORYNAME);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.SubCategoryName = pSqlReader.GetString(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_IS_ACTIVE))
                {
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_IS_ACTIVE);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entActivityAssignment.IsActiveActivity = pSqlReader.GetBoolean(iIndex);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                {
                    entRange = new EntityRange();
                    iIndex = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(iIndex))
                        entRange.TotalRows = pSqlReader.GetInt32(iIndex);
                    entActivityAssignment.ListRange = entRange;
                }                
            }
            return entActivityAssignment;
        }

        private ActivityAssignment FillSubCategoryList(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            ActivityAssignment _entProductSubCategory = new ActivityAssignment();
            EntityRange _entRange = null;
            int index;
            if (pSqlReader.HasRows)
            {
                index = pSqlReader.GetOrdinal(Schema.SubCategory.COL_SUBCATEGORYID);
                if (!pSqlReader.IsDBNull(index))
                    _entProductSubCategory.ID = pSqlReader.GetString(index);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.SubCategory.COL_CATEGORYID))
                {
                    index = pSqlReader.GetOrdinal(Schema.SubCategory.COL_CATEGORYID);
                    if (!pSqlReader.IsDBNull(index))
                        _entProductSubCategory.CategoryId = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.SubCategory.COL_SUBCATEGORYNAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.SubCategory.COL_SUBCATEGORYNAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entProductSubCategory.SubCategoryName = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.SubCategory.COL_ISACTIVE))
                {
                    index = pSqlReader.GetOrdinal(Schema.SubCategory.COL_ISACTIVE);
                    if (!pSqlReader.IsDBNull(index))
                        _entProductSubCategory.IsActiveActivity = pSqlReader.GetBoolean(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.SubCategoryLanguage.COL_LANGUAGEID))
                {
                    index = pSqlReader.GetOrdinal(Schema.SubCategoryLanguage.COL_LANGUAGEID);
                    if (!pSqlReader.IsDBNull(index))
                        _entProductSubCategory.LanguageId = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Language.COL_LANG_ENG_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Language.COL_LANG_ENG_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entProductSubCategory.LanguageName = pSqlReader.GetString(index);
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.SubCategoryLanguage.COL_DESCRIPTION))
                {
                    index = pSqlReader.GetOrdinal(Schema.SubCategoryLanguage.COL_DESCRIPTION);
                    if (!pSqlReader.IsDBNull(index))
                        _entProductSubCategory.Description = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(index))
                        _entProductSubCategory.CreatedById = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_ON))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(index))
                        _entProductSubCategory.DateCreated = pSqlReader.GetDateTime(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entProductSubCategory.CreatedByName = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entProductSubCategory.LastModifiedByName = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(index))
                        _entProductSubCategory.LastModifiedById = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(index))
                        _entProductSubCategory.LastModifiedDate = pSqlReader.GetDateTime(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Category.COL_CATEGORYNAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Category.COL_CATEGORYNAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entProductSubCategory.CategoryName = pSqlReader.GetString(index);
                }

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(index))
                        _entRange.TotalRows = pSqlReader.GetInt32(index);
                    _entProductSubCategory.ListRange = _entRange;
                    return _entProductSubCategory;
                }
            }
            return _entProductSubCategory;
        }
    }
}
