/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author: Fattesinh, Shailesh
* Created:15/07/09
* Last Modified:<24/09/09>
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;
using System.Transactions;
using System.IO;
using static Org.BouncyCastle.Math.EC.ECCurve;
namespace YPLMS2._0.API.DataAccessManager
{
    /// <summary>
    /// class LearnerDAM
    /// </summary>
    public class LearnerDAM:IDataManager<Learner>, ILearnerDAM<Learner>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        SqlConnection _sqlcon = null;
        Learner _entLearner = null;
        List<Learner> _entListLearner = null;
        EntityRange _entRange = null;
        DataSet _dset = null;
        UserAdminRole entUserAdminRole = null;
        string _strMessageId = YPLMS.Services.Messages.User.LEARNER_ERROR;
        static int iRecordsUpdated = 0;
        SQLObject _sqlObject = null;

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public LearnerDAM()
        {
        }
        
        public Learner GetUserBySamlIdentifier(string identifier, long userIdentifierColumnId, string clientId)
        { 
            try
            {
                SQLObject sqlObject = new SQLObject();
                using (SqlConnection sqlConnection = new SqlConnection(sqlObject.GetClientDBConnString(clientId)))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = "sproc_Learner_SearchBySamlSso";
                        sqlCommand.Parameters.Add(new SqlParameter("@UserIdentifierColumnID", userIdentifierColumnId));
                        sqlCommand.Parameters.Add(new SqlParameter("@FieldValue", identifier));
                       
                        using(var sqlReader = sqlObject.SqlDataReader(sqlCommand, true))
                        {
                            sqlReader.Read();
                            _entLearner = FillUserObject(sqlReader, false, sqlObject);
                        }
                    }
                }
            }
            catch (Exception expCommon)
            {
               _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(),ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return _entLearner;
        }

        /// <summary>
        /// To get user details by user id.
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        
        public Learner GetUserByID(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USER;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.ID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID.ToString());
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillUserObject(_sqlreader, false, _sqlObject);
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
            return _entLearner;
        }


        /// <summary>
        /// To get user details by user id.
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>

        public List<Learner> GetUserByRequestedID(Search pEntSearch)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USERS_BY_REQUESTEDID;
            _sqlObject = new SQLObject();
            _entListLearner = new List<Learner>();
            if (!String.IsNullOrEmpty(pEntSearch.CreatedById))
                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntSearch.CreatedById.ToString());
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntSearch.KeyWord))
                _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntSearch.KeyWord);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, null);

            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntSearch.ListRange != null)
            {
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntSearch.ListRange.PageIndex);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntSearch.ListRange.PageSize);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                _sqlcmd.Parameters.Add(_sqlpara);

                //For Scope
              //  _sqlpara = new SqlParameter(Schema.Learner.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
               // _sqlcmd.Parameters.Add(_sqlpara);
            }
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);                              
                while (_sqlreader.Read())
                {
                    _entLearner = FillObjectByRequestedId(_sqlreader, true, _sqlObject);
                    _entListLearner.Add(_entLearner);
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
            return _entListLearner;
        }

        /// <summary>
        /// To get user details by user id.
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>

        public List<Learner> GetUserByRuleID(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_SELECTED_BUSSINESS_RULE_USERS;
            _sqlObject = new SQLObject();
            _entListLearner = new List<Learner>();
            if (!String.IsNullOrEmpty(pEntLearner.RuleId))
                _sqlpara = new SqlParameter(Schema.Common.PARA_BUSSINESS_RULE_ID, pEntLearner.RuleId.ToString());
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_BUSSINESS_RULE_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntLearner.ListRange != null)
            {
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntLearner.ListRange.PageIndex);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntLearner.ListRange.PageSize);
                _sqlcmd.Parameters.Add(_sqlpara);
                _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntLearner.ListRange.SortExpression);
                _sqlcmd.Parameters.Add(_sqlpara);
            }
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entLearner = FillObjectBussniessRulebyUsers(_sqlreader, true, _sqlObject);
                    _entListLearner.Add(_entLearner);
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
            return _entListLearner;
        }

        /// <summary>
        /// To get user details by user id.
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner GetUserByIDSelfRegi(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USER_SELF_REGI;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.ID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID.ToString());
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillUserObjectSelfRegistration(_sqlreader, false, _sqlObject);
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
            return _entLearner;
        }
        /// <summary>
        /// To get user details by user id.
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner GetUserByID_CoursePlayer(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USER_COURSE_PLAYER;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.ID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID.ToString());
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillUserObject_CoursePlayer(_sqlreader);
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
            return _entLearner;
        }

        /// <summary>
        /// To get user details For Requested By ID call.
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner GetUserRequestedByID(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USER_FOR_REQ_BY_ID;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.ID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID.ToString());
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillUserObjectForGetReqById(_sqlreader, _sqlObject);
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
            return _entLearner;
        }


        /// <summary>
        /// To get user Preferred Date and Time format
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner GetPrefferredDateTime(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USER_PREFERED_DATETIME;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.ID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID.ToString());
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillUserObjectForPrefferdDateAndTime(_sqlreader, _sqlObject);
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
            return _entLearner;
        }

        /// <summary>
        /// To get user details by user type id.
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner GetUserByTypeID(Learner pEntLearner)
        {
            //Get user details by user TypeId if it pass Null then it gets only "firstadmin" record
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USER_BY_TYPE_ID;
            if (!String.IsNullOrEmpty(pEntLearner.UserTypeId))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_TYPE_ID, pEntLearner.UserTypeId.ToString());
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_TYPE_ID, null);

            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillUserObject(_sqlreader, false, _sqlObject);
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
            return _entLearner;
        }       


        /// <summary>
        /// To get user details by user type id.
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner GetUserDetailsByTypeID(Learner pEntLearner)
        {
            
            //Get user details by user TypeId if it pass Null then it gets only "firstadmin" record
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USER_DEFAULT_LANG_ID_BY_TYPE_ID;
            if (!String.IsNullOrEmpty(pEntLearner.UserTypeId))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_TYPE_ID, pEntLearner.UserTypeId.ToString());
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_TYPE_ID, null);

            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillObject(_sqlreader, _sqlObject);
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
            return _entLearner;
        }

        /// <summary>
        /// Find Learners
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<Learner> FindLearners(Search pEntSearch)
        {
            return FindLearners(pEntSearch, true);
        }

        /// <summary>
        /// Find Learners
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <param name="pIsIn"></param>
        /// <returns></returns>
        public List<Learner> FindSelfRegistration(Search pEntSearch)
        {
            Learner pEntLearner = null;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _entListLearner = new List<Learner>();
            SqlConnection sqlConnection = null;
            try
            {
                if (!string.IsNullOrEmpty(pEntSearch.ClientId))
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                    sqlConnection = new SqlConnection(_strConnString);
                    _sqlcmd.CommandText = Schema.Learner.PROC_FIND_USERS_SELF_REGI_LIST;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;


                    if (!String.IsNullOrEmpty(pEntSearch.KeyWord))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntSearch.KeyWord);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, null);

                    _sqlcmd.Parameters.Add(_sqlpara);


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
                        {
                            _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }

                    }

                    if (pEntSearch.SearchObject != null && pEntSearch.SearchObject.Count > 0)
                    {
                       
                        if (pEntSearch.SearchObject[0] is Learner)
                        {

                            pEntLearner = new Learner();
                            pEntLearner = (Learner)pEntSearch.SearchObject[0];

                            _sqlpara = new SqlParameter(Schema.Learner.PARA_REGISTRATION_STATUS, pEntLearner.RegistrationStatus);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                    _sqlcmd.Connection = sqlConnection;
                    _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                    while (_sqlreader.Read())
                    {
                        _entLearner = FillUserObjectSelfRegistration(_sqlreader, true, _sqlObject);
                        _entListLearner.Add(_entLearner);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return _entListLearner;
        }

        //
        public Learner GetActiveUserEmail(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USER_ISACTIVE_BY_EMAIL;
            _sqlObject = new SQLObject();

            if (!String.IsNullOrEmpty(pEntLearner.EmailID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_EMAIL_ID, pEntLearner.EmailID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_EMAIL_ID, null);

            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillUserObjectForEmail(_sqlreader, false, _sqlObject);
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
            return _entLearner;
        }
        /// <summary>
        /// Find Learners
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <param name="pIsIn"></param>
        /// <returns></returns>
        private List<Learner> FindLearners(Search pEntSearch, bool pIsIn)
        {
            Learner pEntLearner = null;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _entListLearner = new List<Learner>();
            SqlConnection sqlConnection = null;
            try
            {
                if (!string.IsNullOrEmpty(pEntSearch.ClientId))
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                    sqlConnection = new SqlConnection(_strConnString);
                    _sqlcmd.CommandText = Schema.Learner.PROC_FIND_USERS_WITH_RANGE;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;


                    if (!String.IsNullOrEmpty(pEntSearch.KeyWord))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntSearch.KeyWord);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, null);

                    _sqlcmd.Parameters.Add(_sqlpara);


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
                        {
                            _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }

                        //For Scope
                        _sqlpara = new SqlParameter(Schema.Learner.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (pEntSearch.SearchObject != null && pEntSearch.SearchObject.Count > 0)
                    {
                        if (pEntSearch.SearchObject[0] is UserAdminRole)
                        {
                            entUserAdminRole = new UserAdminRole();
                            entUserAdminRole = (UserAdminRole)pEntSearch.SearchObject[0];
                            _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, entUserAdminRole.RoleId);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            _sqlpara = new SqlParameter(Schema.Common.PARA_QUERY_TYPE, pIsIn);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                        if (pEntSearch.SearchObject[0] is Learner)
                        {

                            pEntLearner = new Learner();
                            pEntLearner = (Learner)pEntSearch.SearchObject[0];
                            _sqlpara = new SqlParameter(Schema.Learner.PARA_IS_ACTIVE, pEntLearner.IsActive);
                            _sqlcmd.Parameters.Add(_sqlpara);

                            if (pEntLearner.UserAdminRole.Count > 0)
                            {
                                entUserAdminRole = new UserAdminRole();
                                entUserAdminRole = (UserAdminRole)pEntLearner.UserAdminRole[0];
                                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, entUserAdminRole.RoleId);
                                _sqlcmd.Parameters.Add(_sqlpara);
                                _sqlpara = new SqlParameter(Schema.Common.PARA_QUERY_TYPE, pIsIn);
                                _sqlcmd.Parameters.Add(_sqlpara);
                            }
                        }
                    }
                    _sqlcmd.Connection = sqlConnection;
                    _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                    while (_sqlreader.Read())
                    {
                        _entLearner = FillUserObject(_sqlreader, true, _sqlObject);
                        _entListLearner.Add(_entLearner);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return _entListLearner;
        }

        /// <summary>
        /// Find Learners For Role Assignment
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <param name="pIsIn"></param>
        /// <returns></returns>
        public List<Learner> FindLearnersForRoleAssignment(Search pEntSearch, bool pIsIn)
        {
            Learner pEntLearner = null;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _entListLearner = new List<Learner>();
            DataTable dtableLearner;
            DataTable dtableRole;
            _dset = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(pEntSearch.ClientId))
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);

                    _sqlcmd.CommandText = Schema.Learner.PROC_FIND_USERS_FOR_ROLE_ASSIGNMENT;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;

                    if (!String.IsNullOrEmpty(pEntSearch.KeyWord))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntSearch.KeyWord);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, null);

                    _sqlcmd.Parameters.Add(_sqlpara);


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
                        {
                            _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }

                        //For Scope
                        _sqlpara = new SqlParameter(Schema.Learner.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (pEntSearch.SearchObject != null && pEntSearch.SearchObject.Count > 0)
                    {
                        if (pEntSearch.SearchObject[0] is UserAdminRole)
                        {
                            entUserAdminRole = new UserAdminRole();
                            entUserAdminRole = (UserAdminRole)pEntSearch.SearchObject[0];
                            _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, entUserAdminRole.RoleId);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            _sqlpara = new SqlParameter(Schema.Common.PARA_QUERY_TYPE, pIsIn);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                        if (pEntSearch.SearchObject[0] is Learner)
                        {

                            pEntLearner = new Learner();
                            pEntLearner = (Learner)pEntSearch.SearchObject[0];
                            _sqlpara = new SqlParameter(Schema.Learner.PARA_IS_ACTIVE, pEntLearner.IsActive);
                            _sqlcmd.Parameters.Add(_sqlpara);

                            if (pEntLearner.UserAdminRole.Count > 0)
                            {
                                entUserAdminRole = new UserAdminRole();
                                entUserAdminRole = (UserAdminRole)pEntLearner.UserAdminRole[0];
                                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, entUserAdminRole.RoleId);
                                _sqlcmd.Parameters.Add(_sqlpara);
                                _sqlpara = new SqlParameter(Schema.Common.PARA_QUERY_TYPE, pIsIn);
                                _sqlcmd.Parameters.Add(_sqlpara);
                            }
                        }
                    }

                    _dset = _sqlObject.SqlDataAdapter(_sqlcmd, _strConnString);
                    dtableLearner = _dset.Tables[0];
                    dtableRole = _dset.Tables[1];
                    foreach (DataRow drow in dtableLearner.Rows)
                    {
                        _entLearner = FillLearner(drow);
                        foreach (DataRow drowRole in dtableRole.Select(Schema.Learner.COL_USER_ID + "='" + _entLearner.ID + "'"))
                        {
                            _entLearner.UserAdminRole.Add(FillUserAdminRole(drowRole));
                        }
                        _entListLearner.Add(_entLearner);
                    }

                }
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return _entListLearner;
        }

        /// <summary>
        /// Find Learners in BO
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<Learner> FindBussinessRuleUsers(Search pEntSearch)
        {
            Learner pEntLearner = null;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _entListLearner = new List<Learner>();
            SqlConnection sqlConnection = null;
            try
            {
                if (!string.IsNullOrEmpty(pEntSearch.ClientId))
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                    sqlConnection = new SqlConnection(_strConnString);
                    _sqlcmd.CommandText = Schema.Learner.PROC_FIND_BUSSINESS_RULE_USERS_WITH_RANGE;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;

                    if (!String.IsNullOrEmpty(pEntSearch.KeyWord))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntSearch.KeyWord);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

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
                        {
                            _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                        //For Scope
                        if (!String.IsNullOrEmpty(pEntSearch.ListRange.RequestedById))
                            _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
                        else
                            _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, null);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (pEntSearch.SearchObject != null && pEntSearch.SearchObject.Count > 0)
                    {
                        if (pEntSearch.SearchObject[0] is Learner)
                        {
                            pEntLearner = new Learner();
                            pEntLearner = (Learner)pEntSearch.SearchObject[0];

                            if (!string.IsNullOrEmpty(pEntLearner.ID))
                            {
                                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
                                _sqlcmd.Parameters.Add(_sqlpara);
                            }
                        }
                    }
                    _sqlcmd.Connection = sqlConnection;
                    _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                    while (_sqlreader.Read())
                    {
                        _entLearner = FillUserObject(_sqlreader, true, _sqlObject);
                        _entListLearner.Add(_entLearner);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return _entListLearner;
        }

        /// <summary>
        /// Find Learners for Assignment
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<Learner> FindLearnersForAssignment(Search pEntSearch)
        {
            Learner entLearner = new Learner();
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _entListLearner = new List<Learner>();
            SqlConnection sqlConnection = null;
            try
            {
                if (!string.IsNullOrEmpty(pEntSearch.ClientId))
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                    sqlConnection = new SqlConnection(_strConnString);
                    _sqlcmd.CommandText = Schema.Learner.PROC_SEARCH_LEARNERS_FOR_ASSIGNMENTS;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;

                    if (!String.IsNullOrEmpty(pEntSearch.KeyWord))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntSearch.KeyWord);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, null);

                    _sqlcmd.Parameters.Add(_sqlpara);

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
                            _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, DBNull.Value);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        if (!string.IsNullOrEmpty(pEntSearch.ListRange.RequestedById))
                            _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
                        else
                            _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, DBNull.Value);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    //if (pEntSearch.SearchObject.Count > 0)
                    if (pEntSearch.SearchObject != null && pEntSearch.SearchObject.Count > 0)                    
                    {
                        entLearner = (Learner)pEntSearch.SearchObject[0];
                        _sqlpara = new SqlParameter(Schema.Common.PARA_IS_ACTIVE, entLearner.IsActive);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    _sqlcmd.Connection = sqlConnection;
                    _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                    while (_sqlreader.Read())
                    {
                        _entLearner = FillUserObjectforAssignment(_sqlreader, true, _sqlObject);
                        _entListLearner.Add(_entLearner);
                    }
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
            return _entListLearner;
        }


        /// <summary>
        /// Find Learners for Assignment
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<Learner> FindLearnersForAssignmentOptimized(Search pEntSearch)
        {
            Learner entLearner = new Learner(); ;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _entListLearner = new List<Learner>();
            SqlConnection sqlConnection = null;
            try
            {
                if (!string.IsNullOrEmpty(pEntSearch.ClientId))
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                    sqlConnection = new SqlConnection(_strConnString);
                    _sqlcmd.CommandText = Schema.Learner.PROC_SEARCH_LEARNERS_FOR_ASSIGNMENTS_OPTIMIZED;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;

                    if (!String.IsNullOrEmpty(pEntSearch.KeyWord))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntSearch.KeyWord);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, null);

                    _sqlcmd.Parameters.Add(_sqlpara);

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

                    if (pEntSearch.SearchObject.Count > 0)
                    {
                        entLearner = (Learner)pEntSearch.SearchObject[0];
                        _sqlpara = new SqlParameter(Schema.Common.PARA_IS_ACTIVE, entLearner.IsActive);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    _sqlcmd.Connection = sqlConnection;
                    _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                    while (_sqlreader.Read())
                    {
                        _entLearner = FillUserObjectOptimized(_sqlreader, true, _sqlObject);
                        _entListLearner.Add(_entLearner);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return _entListLearner;
        }

        /// <summary>
        /// Find Learners For UnAssignment 
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<Learner> FindLearnersForUnAssignment(Search pEntSearch)
        {
            Learner entLearner = new Learner();
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _entListLearner = new List<Learner>();
            SqlConnection sqlConnection = null;
            try
            {
                if (!string.IsNullOrEmpty(pEntSearch.ClientId))
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                    sqlConnection = new SqlConnection(_strConnString);
                    _sqlcmd.CommandText = Schema.Learner.PROC_SEARCH_LEARNERS_FOR_UNASSIGNMENTS;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;

                    if (!String.IsNullOrEmpty(pEntSearch.KeyWord))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntSearch.KeyWord);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, null);

                    _sqlcmd.Parameters.Add(_sqlpara);


                    if (!String.IsNullOrEmpty(pEntSearch.ManagerName))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_MANAGER_NAME, pEntSearch.ManagerName);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_MANAGER_NAME, null);

                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntSearch.UserCriteria != null)
                    {
                        _sqlpara = new SqlParameter("UserSearchCriteria", SqlDbType.Xml)
                                       {Value = SQLHelper.FormatUserSearchCriteria(pEntSearch.UserCriteria)};
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

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
                    _sqlcmd.Connection = sqlConnection;
                    _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                    while (_sqlreader.Read())
                    {
                        _entLearner = FillUserObject(_sqlreader, true, _sqlObject);
                        _entListLearner.Add(_entLearner);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return _entListLearner;
        }


        /// <summary>
        /// Find Learners For UnAssignment - Optimized
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<Learner> FindLearnersForUnAssignmentOptimized(Search pEntSearch)
        {
            Learner entLearner = new Learner();
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _entListLearner = new List<Learner>();
            SqlConnection sqlConnection = null;
            try
            {
                if (!string.IsNullOrEmpty(pEntSearch.ClientId))
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                    sqlConnection = new SqlConnection(_strConnString);
                    _sqlcmd.CommandText = Schema.Learner.PROC_SEARCH_LEARNERS_FOR_UNASSIGNMENTS_OPTIMIZED;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;

                    if (!String.IsNullOrEmpty(pEntSearch.KeyWord))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntSearch.KeyWord);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, null);

                    _sqlcmd.Parameters.Add(_sqlpara);


                    if (!String.IsNullOrEmpty(pEntSearch.ManagerName))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_MANAGER_NAME, pEntSearch.ManagerName);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_MANAGER_NAME, null);

                    _sqlcmd.Parameters.Add(_sqlpara);

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
                    _sqlcmd.Connection = sqlConnection;
                    _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                    while (_sqlreader.Read())
                    {
                        _entLearner = FillUserObjectOptimized(_sqlreader, true, _sqlObject);
                        _entListLearner.Add(_entLearner);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return _entListLearner;
        }

        /// <summary>
        /// Find Learners
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<Learner> SearchLearners(Search pEntSearch)
        {
            //Learner pEntLearner = null;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _entListLearner = new List<Learner>();
            SqlConnection sqlConnection = null;
            try
            {
                if (!string.IsNullOrEmpty(pEntSearch.ClientId))
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                    sqlConnection = new SqlConnection(_strConnString);
                    _sqlcmd.CommandText = Schema.Learner.PROC_SEARCH_LEARNERS;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;

                    if (pEntSearch.ListRange != null)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntSearch.ListRange.PageIndex);
                        _sqlcmd.Parameters.Add(_sqlpara);
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntSearch.ListRange.PageSize);
                        _sqlcmd.Parameters.Add(_sqlpara);
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        //For Scope
                        _sqlpara = new SqlParameter(Schema.Learner.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (pEntSearch.UserCriteria != null)
                    {
                        _sqlpara = new SqlParameter("UserSearchCriteria", SqlDbType.Xml)
                                       {Value = SQLHelper.FormatUserSearchCriteria(pEntSearch.UserCriteria)};
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (!String.IsNullOrEmpty(pEntSearch.KeyWord))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntSearch.KeyWord);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, null);

                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlcmd.Connection = sqlConnection;
                    _sqlcmd.CommandTimeout = 0;
                    _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                    while (_sqlreader.Read())
                    {
                        _entLearner = FillUserObject(_sqlreader, true, _sqlObject);
                        _entListLearner.Add(_entLearner);
                    }
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
            return _entListLearner;
        }


        /// <summary>
        /// To Get User base on login details
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner GetUserAprovedStudent(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _entLearner = new Learner();
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USER_STUDENT_REGI_APPROVED;
            if (!String.IsNullOrEmpty(pEntLearner.ID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                _sqlreader.Read();
                _entLearner = FillUserObject_SelfRegiCheckApproved(_sqlreader, false, _sqlObject);
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
            return _entLearner;
        }

        public List<Learner> SearchLearners_ForIPerform(Search pEntSearch)
        {
            //Learner pEntLearner = null;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _entListLearner = new List<Learner>();
            SqlConnection sqlConnection = null;
            try
            {
                if (!string.IsNullOrEmpty(pEntSearch.ClientId))
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                    sqlConnection = new SqlConnection(_strConnString);
                    _sqlcmd.CommandText = Schema.Learner.PROC_SEARCH_LEARNERS_ForIPerform;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;

                    if (pEntSearch.ListRange != null)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntSearch.ListRange.PageIndex);
                        _sqlcmd.Parameters.Add(_sqlpara);
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntSearch.ListRange.PageSize);
                        _sqlcmd.Parameters.Add(_sqlpara);
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        //For Scope
                        _sqlpara = new SqlParameter(Schema.Learner.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    if (pEntSearch.UserCriteria != null)
                    {
                        _sqlpara = new SqlParameter("UserSearchCriteria", SqlDbType.Xml) { Value = SQLHelper.FormatUserSearchCriteria(pEntSearch.UserCriteria) };
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    _sqlcmd.Connection = sqlConnection;
                    _sqlcmd.CommandTimeout = 0;
                    _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                    while (_sqlreader.Read())
                    {
                        _entLearner = FillUserObject(_sqlreader, true, _sqlObject);
                        _entListLearner.Add(_entLearner);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return _entListLearner;
        }
        /// <summary>
        /// To Get User base on login details
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner GetUserByLogin(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _entLearner = new Learner();
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USER_BY_PWD;
            if (!String.IsNullOrEmpty(pEntLearner.UserNameAlias))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, pEntLearner.UserNameAlias);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            if (!String.IsNullOrEmpty(pEntLearner.EmailID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_EMAIL_ID, pEntLearner.EmailID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_EMAIL_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.UserPassword))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_PASSWORD, pEntLearner.UserPassword); //EncryptionManager.Encrypt(pEntLearner.UserPassword)
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_PASSWORD, null);
            _sqlpara.DbType = DbType.String;
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                _sqlreader.Read();
                _entLearner = FillUserObject(_sqlreader, false, _sqlObject);
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
            return _entLearner;
        }

        public Learner SaveTokenToDB(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
            sqlConnection = new SqlConnection(_strConnString);
                        
            using (SqlConnection con = new SqlConnection(_strConnString))
            using (SqlCommand cmd = new SqlCommand("UPDATE tblUserMaster SET SessionToken = @Token WHERE SystemUserGUID = @UserId", con))
            {
                cmd.Parameters.AddWithValue("@Token", pEntLearner.Token);
                cmd.Parameters.AddWithValue("@UserId", pEntLearner.SystemUserGUID);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return null;
        }


        public Learner UpdateDateFormat(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_UPDATE_DATEFORMAT;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.ID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            if (!String.IsNullOrEmpty(pEntLearner.PreferredDateFormat))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_PREFERREDDATE_FORMAT, pEntLearner.PreferredDateFormat);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_PREFERREDDATE_FORMAT, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.PreferredTimeZone))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_PREFERREDTIMEZONE, pEntLearner.PreferredTimeZone);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_PREFERREDTIMEZONE, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                 sqlConnection = new SqlConnection(_strConnString);
                 _sqlcmd.Connection = sqlConnection;
                  sqlConnection.Open();
                 _sqlObject.ExecuteNonQuery(_sqlcmd);
                 
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
            return pEntLearner;
        }

        /// <summary>
        /// To search user information by alias
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner GetUserByAlias(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USER_BY_ALIAS;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.UserNameAlias))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, pEntLearner.UserNameAlias);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, null);

            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.EmailID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_EMAIL_ID, pEntLearner.EmailID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_EMAIL_ID, null);

            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.ClientId))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_CLIENT_ID, pEntLearner.ClientId);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_CLIENT_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillUserObject(_sqlreader, false, _sqlObject);
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
            return _entLearner;
        }
        
        /// <summary>
        /// To search user information by alias ILT
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner GetUserByAliasILT(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USER_BY_ALIAS_ILT;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.UserNameAlias))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, pEntLearner.UserNameAlias);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, null);

            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.EmailID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_EMAIL_ID, pEntLearner.EmailID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_EMAIL_ID, null);

            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.ClientId))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_CLIENT_ID, pEntLearner.ClientId);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_CLIENT_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillUserObject(_sqlreader, false, _sqlObject);
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
            return _entLearner;
        }

        /// <summary>
        /// To get user system userguid to pass parameter user id or email id - added by bharat:17-Dec-2015
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner GetUserSystemGUID(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USER_SYSTEMUSERGUID;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.UserNameAlias))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, pEntLearner.UserNameAlias);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, null);

            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillSystemGUIDObject(_sqlreader,_sqlObject);
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
            return _entLearner;
        }





        // <summary>
        /// To Update Terms And Condition checkbox - added by Chetan:6-Apr-2018
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner UpdateTermsAndCondition(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_UPDATE_TERMS_AND_CONDITION;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.ID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);

            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillSystemGUIDObject(_sqlreader, _sqlObject);
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
            return _entLearner;
        }

        /// <summary>
        /// To get user IS LOCK to pass parameter user id or email id - added by bharat:22-Dec-2015
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner GetIsUserLock(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_IS_USER_LOCK;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.UserNameAlias))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, pEntLearner.UserNameAlias);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, null);

            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillSystemGUIDObject(_sqlreader, _sqlObject);
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
            return _entLearner;
        }

        /// <summary>
        /// To get OTP Number to pass parameter user id or email id - added by bharat:24-Dec-2015
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner GetOTPNumber(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_OTP_NUMBER;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.ID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);

            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillSystemGUIDObject(_sqlreader, _sqlObject);
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
            return _entLearner;
        }
        /// <summary>
        /// To search user information by alias For ForgotPassword
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner GetUserByAliasForgotPassword(Learner pEntLearner,string PasswordUpdate)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USER_BY_ALIAS_FORGOTPASSWORD;
            _sqlObject = new SQLObject();
      
            _sqlcmd.Parameters.Add(new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, pEntLearner.UserNameAlias));
            _sqlcmd.Parameters.Add(new SqlParameter(Schema.Learner.PARA_EMAIL_ID, pEntLearner.EmailID));

            if(pEntLearner.UserPassword != null)
                _sqlcmd.Parameters.Add(new SqlParameter(Schema.Learner.PARA_USER_PASSWORD,  EncryptionManager.Encrypt(pEntLearner.UserPassword)));
            
            _sqlcmd.Parameters.Add(new SqlParameter(Schema.Learner.PARA_PASSWORD_CHANGED, PasswordUpdate));


            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillUserObjectForForgotPassword(_sqlreader, false, _sqlObject);
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
            return _entLearner;
        }

        /// <summary>
        /// To search user information by alias
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner CheckUserByAlias(Learner pEntLearner)
        {
            _entLearner = new Learner();
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_USER_CHECK_BY_ALIAS;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.UserNameAlias))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, pEntLearner.UserNameAlias);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                Object obj = null;
                obj=_sqlObject.ExecuteScalar(_sqlcmd, false);
                if (obj != null)
                {
                    _entLearner.ID = Convert.ToString(obj);
                }
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {                
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return _entLearner;
        }

        /// <summary>
        /// To search user information by alias
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner GetUserByEmailUpdateProfile(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USER_BY_EMAIL;
            _sqlObject = new SQLObject();
           
            if (!String.IsNullOrEmpty(pEntLearner.EmailID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_EMAIL_ID, pEntLearner.EmailID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_EMAIL_ID, null);

            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillUserObjectForEmail(_sqlreader, false, _sqlObject);
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
            return _entLearner;
        }

        /// <summary>
        /// To search user information by SystemUserGUID 
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner GetUserBySystemUserGUID(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USER_BY_SYSTEMUSERGUID;
            _sqlObject = new SQLObject();

            if (!String.IsNullOrEmpty(pEntLearner.SystemUserGUID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_SYSTEM_USER_GUID, pEntLearner.SystemUserGUID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_SYSTEM_USER_GUID, null);

            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillUserObjectForSystemUserGUID(_sqlreader, false, _sqlObject);
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
            return _entLearner;
        }

        /// <summary>
        /// This method does not Fill Child List
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>Learner</returns>
        public Learner FillUserObject(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entLearner = new Learner();
            UserCustomFieldValue entUserCustomFieldValue;
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

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_UserExpiryDate))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_UserExpiryDate);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.userExpiryDate = pSqlReader.GetDateTime(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_AUTHENTICATION_TOKEN))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_AUTHENTICATION_TOKEN);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.AuthenticationToken = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_ISTERMSANDCONDACCEPTED))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_ISTERMSANDCONDACCEPTED);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.IsTermsAndCondAccepted = pSqlReader.GetBoolean(index);

                }

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(index))
                        _entRange.TotalRows = pSqlReader.GetInt32(index);
                    _entLearner.ListRange = _entRange;
                    return _entLearner;
                }
                // Below code to retrieve the list of current user roles.
                pSqlReader.NextResult();

                while (pSqlReader.Read())
                {
                    entUserAdminRole = new UserAdminRole();

                    //User Id
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserAdminRole.ID = pSqlReader.GetString(index);

                    //Role Id
                    index = pSqlReader.GetOrdinal(Schema.AdminRole.COL_ROLE_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserAdminRole.RoleId = pSqlReader.GetString(index);
                    //Level Id
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_LEVEL_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserAdminRole.LevelId = pSqlReader.GetString(index);

                    //Unit Id
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_UNIT_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserAdminRole.UnitId = pSqlReader.GetString(index);

                    //Unit Id
                    index = pSqlReader.GetOrdinal(Schema.CustomGroup.COL_CSG_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserAdminRole.CustomGroupId = pSqlReader.GetString(index);

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.RuleRoleScope.COL_RULE_ID))
                    {
                        index = pSqlReader.GetOrdinal(Schema.RuleRoleScope.COL_RULE_ID);
                        if (!pSqlReader.IsDBNull(index))
                            entUserAdminRole.RuleId = pSqlReader.GetString(index);
                    }

                    //Admin Role Type
                    index = pSqlReader.GetOrdinal(Schema.AdminRole.COL_ROLE_TYPE);
                    if (!pSqlReader.IsDBNull(index))
                        entUserAdminRole.AdminRoleType = (RoleType)Enum.Parse(typeof(RoleType), pSqlReader.GetString(index));


                    //Role Is Active
                    index = pSqlReader.GetOrdinal(Schema.AdminRole.COL_IS_ACTIVE);
                    if (!pSqlReader.IsDBNull(index))
                        entUserAdminRole.IsRoleActive = pSqlReader.GetBoolean(index);

                    _entLearner.UserAdminRole.Add(entUserAdminRole);
                }

                pSqlReader.NextResult();

                while (pSqlReader.Read())
                {
                    entUserCustomFieldValue = new UserCustomFieldValue();

                    index = pSqlReader.GetOrdinal(Schema.UserCustomFieldValue.COL_CUSTOM_FIELD_ITEM_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.CustomFieldItemId = pSqlReader.GetString(index);

                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.SystemUserGUID = pSqlReader.GetString(index);

                    entUserCustomFieldValue.ID = entUserCustomFieldValue.CustomFieldItemId + entUserCustomFieldValue.SystemUserGUID;

                    index = pSqlReader.GetOrdinal(Schema.UserCustomFieldValue.COL_ENTERED_VALUE);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.EnteredValue = pSqlReader.GetString(index);

                    index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.CreatedById = pSqlReader.GetString(index);

                    index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.DateCreated = pSqlReader.GetDateTime(index);

                    index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.LastModifiedById = pSqlReader.GetString(index);

                    index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.LastModifiedDate = pSqlReader.GetDateTime(index);

                    index = pSqlReader.GetOrdinal(Schema.CustomField.COL_CUSTOM_FIELD_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.CustomFieldId = pSqlReader.GetString(index);

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.CustomFieldItemLanguage.COL_CUSTOM_FIELD_ITEM_DISPLAY_TEXT))
                    {
                        index = pSqlReader.GetOrdinal(Schema.CustomFieldItemLanguage.COL_CUSTOM_FIELD_ITEM_DISPLAY_TEXT);
                        if (!pSqlReader.IsDBNull(index))
                            entUserCustomFieldValue.CreatedByName = pSqlReader.GetString(index);
                    }


                    _entLearner.UserCustomFieldValue.Add(entUserCustomFieldValue);
                }
            }
            return _entLearner;
        }


        /// <summary>
        /// This method does not Fill Child List
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>Learner</returns>
        public Learner FillUserObjectforAssignment(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entLearner = new Learner();
            UserCustomFieldValue entUserCustomFieldValue;
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

                _entLearner.FirstName = Convert.ToString(_entLearner.FirstName) + " " + Convert.ToString(_entLearner.MiddleName) + " " + Convert.ToString(_entLearner.LastName);

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

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_UserExpiryDate))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_UserExpiryDate);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.userExpiryDate = pSqlReader.GetDateTime(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_AUTHENTICATION_TOKEN))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_AUTHENTICATION_TOKEN);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.AuthenticationToken = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_ISTERMSANDCONDACCEPTED))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_ISTERMSANDCONDACCEPTED);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.IsTermsAndCondAccepted = pSqlReader.GetBoolean(index);

                }

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(index))
                        _entRange.TotalRows = pSqlReader.GetInt32(index);
                    _entLearner.ListRange = _entRange;
                    return _entLearner;
                }
                // Below code to retrieve the list of current user roles.
                pSqlReader.NextResult();

                while (pSqlReader.Read())
                {
                    entUserAdminRole = new UserAdminRole();

                    //User Id
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserAdminRole.ID = pSqlReader.GetString(index);

                    //Role Id
                    index = pSqlReader.GetOrdinal(Schema.AdminRole.COL_ROLE_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserAdminRole.RoleId = pSqlReader.GetString(index);
                    //Level Id
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_LEVEL_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserAdminRole.LevelId = pSqlReader.GetString(index);

                    //Unit Id
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_UNIT_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserAdminRole.UnitId = pSqlReader.GetString(index);

                    //Unit Id
                    index = pSqlReader.GetOrdinal(Schema.CustomGroup.COL_CSG_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserAdminRole.CustomGroupId = pSqlReader.GetString(index);

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.RuleRoleScope.COL_RULE_ID))
                    {
                        index = pSqlReader.GetOrdinal(Schema.RuleRoleScope.COL_RULE_ID);
                        if (!pSqlReader.IsDBNull(index))
                            entUserAdminRole.RuleId = pSqlReader.GetString(index);
                    }

                    //Admin Role Type
                    index = pSqlReader.GetOrdinal(Schema.AdminRole.COL_ROLE_TYPE);
                    if (!pSqlReader.IsDBNull(index))
                        entUserAdminRole.AdminRoleType = (RoleType)Enum.Parse(typeof(RoleType), pSqlReader.GetString(index));


                    //Role Is Active
                    index = pSqlReader.GetOrdinal(Schema.AdminRole.COL_IS_ACTIVE);
                    if (!pSqlReader.IsDBNull(index))
                        entUserAdminRole.IsRoleActive = pSqlReader.GetBoolean(index);

                    _entLearner.UserAdminRole.Add(entUserAdminRole);
                }

                pSqlReader.NextResult();

                while (pSqlReader.Read())
                {
                    entUserCustomFieldValue = new UserCustomFieldValue();

                    index = pSqlReader.GetOrdinal(Schema.UserCustomFieldValue.COL_CUSTOM_FIELD_ITEM_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.CustomFieldItemId = pSqlReader.GetString(index);

                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.SystemUserGUID = pSqlReader.GetString(index);

                    entUserCustomFieldValue.ID = entUserCustomFieldValue.CustomFieldItemId + entUserCustomFieldValue.SystemUserGUID;

                    index = pSqlReader.GetOrdinal(Schema.UserCustomFieldValue.COL_ENTERED_VALUE);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.EnteredValue = pSqlReader.GetString(index);

                    index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.CreatedById = pSqlReader.GetString(index);

                    index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.DateCreated = pSqlReader.GetDateTime(index);

                    index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.LastModifiedById = pSqlReader.GetString(index);

                    index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.LastModifiedDate = pSqlReader.GetDateTime(index);

                    index = pSqlReader.GetOrdinal(Schema.CustomField.COL_CUSTOM_FIELD_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.CustomFieldId = pSqlReader.GetString(index);

                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.CustomFieldItemLanguage.COL_CUSTOM_FIELD_ITEM_DISPLAY_TEXT))
                    {
                        index = pSqlReader.GetOrdinal(Schema.CustomFieldItemLanguage.COL_CUSTOM_FIELD_ITEM_DISPLAY_TEXT);
                        if (!pSqlReader.IsDBNull(index))
                            entUserCustomFieldValue.CreatedByName = pSqlReader.GetString(index);
                    }


                    _entLearner.UserCustomFieldValue.Add(entUserCustomFieldValue);
                }
            }
            return _entLearner;
        }
        /// <summary>
        /// This method does not Fill Child List
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>Learner</returns>
        public Learner FillUserObjectSelfRegistration(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entLearner = new Learner();
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

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_NAME_ALIAS);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.UserNameAlias = pSqlReader.GetString(index);
                else
                    _entLearner.UserNameAlias = "";

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_LAST_NAME);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.LastName = pSqlReader.GetString(index);
                else
                    _entLearner.LastName = "";

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_EMAIL_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.EmailID = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_EMAIL_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.EmailID = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_REGISTRATION_STATUS);
                if (!pSqlReader.IsDBNull(index))
                {
                    _entLearner.RegistrationStatus = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_IDPROOFDOCUMENT))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_IDPROOFDOCUMENT);
                    if (!pSqlReader.IsDBNull(index))
                    {
                        _entLearner.IdProofDocument = pSqlReader.GetString(index);
                    }
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_REJECTCOMMENTS))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_REJECTCOMMENTS);
                    if (!pSqlReader.IsDBNull(index))
                    {
                        _entLearner.RejectComments = pSqlReader.GetString(index);
                    }
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_ADDRESS))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_ADDRESS);
                    if (!pSqlReader.IsDBNull(index))
                    {
                        _entLearner.Address = pSqlReader.GetString(index);
                    }
                }
                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(index))
                        _entRange.TotalRows = pSqlReader.GetInt32(index);
                    _entLearner.ListRange = _entRange;
                    return _entLearner;
                }

            }
            return _entLearner;
        }

        /// <summary>
        /// This method does not Fill Child List
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>Learner</returns>
        public Learner FillUserObject_SelfRegiCheckApproved(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entLearner = new Learner();
            int index;
            if (pSqlReader.HasRows)
            {
                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.ID = pSqlReader.GetString(index);

            }
            return _entLearner;
        }

        /// <summary>
        /// This method Fill List for ForgotPassword
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>Learner</returns>
        public Learner FillUserObjectForForgotPassword(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entLearner = new Learner();
            UserCustomFieldValue entUserCustomFieldValue;
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

                if (pRangeList)
                {
                    _entRange = new EntityRange();
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                    if (!pSqlReader.IsDBNull(index))
                        _entRange.TotalRows = pSqlReader.GetInt32(index);
                    _entLearner.ListRange = _entRange;
                    return _entLearner;
                }

                pSqlReader.NextResult();

                while (pSqlReader.Read())
                {
                    entUserCustomFieldValue = new UserCustomFieldValue();

                    index = pSqlReader.GetOrdinal(Schema.UserCustomFieldValue.COL_CUSTOM_FIELD_ITEM_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.CustomFieldItemId = pSqlReader.GetString(index);

                    index = pSqlReader.GetOrdinal(Schema.CustomField.COL_CUSTOM_FIELD_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.CustomFieldId = pSqlReader.GetString(index);

                    index = pSqlReader.GetOrdinal(Schema.UserCustomFieldValue.COL_ENTERED_VALUE);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.EnteredValue = pSqlReader.GetString(index);

                    _entLearner.UserCustomFieldValue.Add(entUserCustomFieldValue);
                }
            }
            return _entLearner;
        }

        /// <summary>
        /// This method does not Fill Child List
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>Learner</returns>
        public Learner FillUserObject_CoursePlayer(SqlDataReader pSqlReader)
        {
            _entLearner = new Learner();            
            int index;
            if (pSqlReader.HasRows)
            {
                if (_sqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.ID = pSqlReader.GetString(index);
                }
                index = pSqlReader.GetOrdinal(Schema.Learner.COL_FIRST_NAME);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.FirstName = pSqlReader.GetString(index);
                else
                    _entLearner.FirstName = "";
                
                index = pSqlReader.GetOrdinal(Schema.Learner.COL_LAST_NAME);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.LastName = pSqlReader.GetString(index);
                else
                    _entLearner.LastName = "";

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_NAME_ALIAS);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.UserNameAlias = pSqlReader.GetString(index);
                else
                    _entLearner.UserNameAlias = "";

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_MANAGEREMAIL);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.ManagerEmailId = pSqlReader.GetString(index);
                else
                    _entLearner.ManagerEmailId = "";

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_STUDENTEMAIL);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.EmailID = pSqlReader.GetString(index);
                else
                    _entLearner.EmailID = "";

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_DEFAULT_LANGUAGE_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.DefaultLanguageId = pSqlReader.GetString(index);                

                index = pSqlReader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.ClientId = pSqlReader.GetString(index);
                
                index = pSqlReader.GetOrdinal(Schema.Learner.COL_DEFAULT_RV);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.DefaultRegionView = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_CURRENT_RV);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.CurrentRegionView = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_RV_NAME);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.RegionViewName = pSqlReader.GetString(index);
                else
                    _entLearner.RegionViewName = String.Empty;

                if (_sqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_AV_PATH))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_AV_PATH);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.AvPath = pSqlReader.GetString(index);
                    else
                        _entLearner.AvPath = String.Empty;
                }
            }
            return _entLearner;
        }

        /// <summary>
        /// This method does not Fill Child List
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>Learner</returns>
        private Learner FillUserObjectForGetReqById(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            _entLearner = new Learner();            
            int index;
            if (pSqlReader.HasRows)
            {
                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.ID = pSqlReader.GetString(index);
                
                // Below code to retrieve the list of current user roles.
                pSqlReader.NextResult();

                while (pSqlReader.Read())
                {
                    entUserAdminRole = new UserAdminRole();

                    //User Id
                    //index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                    //if (!pSqlReader.IsDBNull(index))
                    //    entUserAdminRole.ID = pSqlReader.GetString(index);

                    //Role Id
                    index = pSqlReader.GetOrdinal(Schema.AdminRole.COL_ROLE_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserAdminRole.RoleId = pSqlReader.GetString(index);

                    ////Level Id
                    //index = pSqlReader.GetOrdinal(Schema.Learner.COL_LEVEL_ID);
                    //if (!pSqlReader.IsDBNull(index))
                    //    entUserAdminRole.LevelId = pSqlReader.GetString(index);

                    ////Unit Id
                    //index = pSqlReader.GetOrdinal(Schema.Learner.COL_UNIT_ID);
                    //if (!pSqlReader.IsDBNull(index))
                    //    entUserAdminRole.UnitId = pSqlReader.GetString(index);

                    ////Unit Id
                    //index = pSqlReader.GetOrdinal(Schema.CustomGroup.COL_CSG_ID);
                    //if (!pSqlReader.IsDBNull(index))
                    //    entUserAdminRole.CustomGroupId = pSqlReader.GetString(index);

                    //if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.RuleRoleScope.COL_RULE_ID))
                    //{
                    //    index = pSqlReader.GetOrdinal(Schema.RuleRoleScope.COL_RULE_ID);
                    //    if (!pSqlReader.IsDBNull(index))
                    //        entUserAdminRole.RuleId = pSqlReader.GetString(index);
                    //}

                    ////Admin Role Type
                    //index = pSqlReader.GetOrdinal(Schema.AdminRole.COL_ROLE_TYPE);
                    //if (!pSqlReader.IsDBNull(index))
                    //    entUserAdminRole.AdminRoleType = (RoleType)Enum.Parse(typeof(RoleType), pSqlReader.GetString(index));


                    ////Role Is Active
                    //index = pSqlReader.GetOrdinal(Schema.AdminRole.COL_IS_ACTIVE);
                    //if (!pSqlReader.IsDBNull(index))
                    //    entUserAdminRole.IsRoleActive = pSqlReader.GetBoolean(index);

                    _entLearner.UserAdminRole.Add(entUserAdminRole);
                }               
            }
            return _entLearner;
        }


        /// <summary>
        /// This method fills Prefferd Date and Time Format of User
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>Learner</returns>
        private Learner FillUserObjectForPrefferdDateAndTime(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            _entLearner = new Learner();
            int index;
            if (pSqlReader.HasRows)
            {
                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.ID = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_PREFERRED_DATE_FORMAT);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.PreferredDateFormat = pSqlReader.GetString(index);


                index = pSqlReader.GetOrdinal(Schema.Learner.COL_PREFERREDTIMEZONE);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.PreferredTimeZone = pSqlReader.GetString(index);
                
            }
            return _entLearner;
        }


        /// <summary>
        /// This method does not Fill Child List
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>Learner</returns>
        public Learner FillObject(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            _entLearner = new Learner();            
            int index;
            if (pSqlReader.HasRows)
            {

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.ID = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_DEFAULT_LANGUAGE_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.DefaultLanguageId = pSqlReader.GetString(index);

                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_NAME_ALIAS);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.UserNameAlias = pSqlReader.GetString(index);

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
            }
            return _entLearner;
        }


        /// <summary>
        /// This method does not Fill Child List
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>Learner</returns>
        public Learner FillObjectByRequestedId(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entLearner = new Learner();
            int index;
            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.ID = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.SystemUserGUID = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_FIRST_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_FIRST_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.FirstName = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DEFAULT_LANGUAGE_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_DEFAULT_LANGUAGE_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.DefaultLanguageId = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_NAME_ALIAS))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_NAME_ALIAS);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.UserNameAlias = pSqlReader.GetString(index);
                }

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
                if (pRangeList)
                {
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                    {
                        _entRange = new EntityRange();
                        index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                        if (!pSqlReader.IsDBNull(index))
                            _entRange.TotalRows = pSqlReader.GetInt32(index);
                        _entLearner.ListRange = _entRange;
                    }
                    return _entLearner;
                }
            }
            return _entLearner;
        }

        public Learner FillObjectBussniessRulebyUsers(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entLearner = new Learner();
            int index;
            if (pSqlReader.HasRows)
            {
                
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.SystemUserGUID = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_FIRST_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_FIRST_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.FirstName = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_LAST_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_LAST_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.LastName = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_NAME_ALIAS))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_NAME_ALIAS);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.UserNameAlias = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_EMAIL_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_EMAIL_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.EmailID = pSqlReader.GetString(index);
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_SCOPE))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_SCOPE);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.UserScope = pSqlReader.GetString(index);
                }

                if (pRangeList)
                {
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                    {
                        _entRange = new EntityRange();
                        index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                        if (!pSqlReader.IsDBNull(index))
                            _entRange.TotalRows = pSqlReader.GetInt32(index);
                        _entLearner.ListRange = _entRange;
                    }
                    return _entLearner;
                }
            }
            return _entLearner;
        }

        /// <summary>
        /// This method does not Fill user systemguid List
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>Learner</returns>
        public Learner FillSystemGUIDObject(SqlDataReader pSqlReader, SQLObject pSqlObject)
        {
            _entLearner = new Learner();
            int index;
            if (pSqlReader.HasRows)
            {
                
                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.ID = pSqlReader.GetString(index);

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_IS_USER_LOCK))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_IS_USER_LOCK);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.IsUserLock = pSqlReader.GetBoolean(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_NAME_ALIAS))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_NAME_ALIAS);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.UserNameAlias = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_EMAIL_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_EMAIL_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.EmailID = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_FIRST_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_FIRST_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.FirstName = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_LAST_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_LAST_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.LastName = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DEFAULT_LANGUAGE_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_DEFAULT_LANGUAGE_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.DefaultLanguageId = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.OTP.COL_OTP_NUMBER))
                {
                    index = pSqlReader.GetOrdinal(Schema.OTP.COL_OTP_NUMBER);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.OTPNumber = pSqlReader.GetString(index);
                }
            }
           
            return _entLearner;
        }
        /// <summary>
        /// This method does not Fill Child List
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>Learner</returns>
        public Learner FillUserObjectOptimized(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entLearner = new Learner();
            //UserCustomFieldValue entUserCustomFieldValue;
            int index;
            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.ID = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_FIRST_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_FIRST_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.FirstName = pSqlReader.GetString(index);
                    else
                        _entLearner.FirstName = "";
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_MIDDLE_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_MIDDLE_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.MiddleName = pSqlReader.GetString(index);
                }

                if (string.IsNullOrEmpty(_entLearner.MiddleName))
                    _entLearner.MiddleName = "";

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_LAST_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_LAST_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.LastName = pSqlReader.GetString(index);
                    else
                        _entLearner.LastName = "";
                }


                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_NAME_ALIAS))
                {

                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_NAME_ALIAS);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.UserNameAlias = pSqlReader.GetString(index);
                    else
                        _entLearner.UserNameAlias = "";
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_TYPE_ID))
                {

                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_TYPE_ID);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.UserTypeId = pSqlReader.GetString(index);
                    else
                        _entLearner.UserTypeId = "";
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_IS_ACTIVE))
                {

                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_IS_ACTIVE);
                    if (!pSqlReader.IsDBNull(index))
                        _entLearner.IsActive = pSqlReader.GetBoolean(index);
                }
                                
                               
                if (pRangeList)
                {
                    if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_TOTAL_COUNT))
                    {
                        _entRange = new EntityRange();
                        index = pSqlReader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                        if (!pSqlReader.IsDBNull(index))
                            _entRange.TotalRows = pSqlReader.GetInt32(index);
                        _entLearner.ListRange = _entRange;
                    }
                    return _entLearner;
                }
                // Below code to retrieve the list of current user roles.
               
                
                
            }
            return _entLearner;
        }


        /// <summary>
        /// This method does not Fill Child List
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>Learner</returns>
        public Learner FillUserObjectForEmail(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entLearner = new Learner();            
            int index;
            if (pSqlReader.HasRows)
            {
                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.ID = pSqlReader.GetString(index);             
            }
            return _entLearner;
        }

        /// <summary>
        /// To Get User Name
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>Learner</returns>
        public Learner FillUserObjectForSystemUserGUID(SqlDataReader pSqlReader, bool pRangeList, SQLObject pSqlObject)
        {
            _entLearner = new Learner();
            int index;
            if (pSqlReader.HasRows)
            {
                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_NAME_ALIAS);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.UserNameAlias = pSqlReader.GetString(index);
            }
            return _entLearner;
        }

        /// <summary>
        /// To update user information
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner object</returns>
        public Learner UpdateUser(Learner pEntLearner,bool pIsImport)
        {
            List<Learner> entListLearner = new List<Learner>();            
            _entLearner = new Learner();
            string strCFValues = string.Empty;
            _sqlObject = new SQLObject();
            SqlConnection sqlConn = null; 
            try
            {
                entListLearner.Add(pEntLearner);
                entListLearner = BulkUpdate(entListLearner);
                _entLearner = entListLearner[0];
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConn = new SqlConnection(_strConnString);                
                sqlConn.Open();
                
                    if (pEntLearner.UserCustomFieldValue.Count > 0)
                    {
                        if (pIsImport)
                        {
                            for (int c = 0; c < pEntLearner.UserCustomFieldValue.Count; c++)
                            {
                                if (string.IsNullOrEmpty(strCFValues))
                                {
                                    strCFValues = pEntLearner.UserCustomFieldValue[c].CustomFieldId + "$(!QU0TE!)$" + pEntLearner.UserCustomFieldValue[c].EnteredValue;
                                }
                                else
                                {
                                    strCFValues = strCFValues + "#(!QU0TE!)#" + pEntLearner.UserCustomFieldValue[c].CustomFieldId + "$(!QU0TE!)$" + pEntLearner.UserCustomFieldValue[c].EnteredValue;
                                }
                            }

                            //To Do new proc for send array of unit id
                            //Call for Custom Field 
                            _sqlcmd = new SqlCommand();
                            _sqlcmd.Connection = sqlConn;
                            _sqlcmd.CommandText = Schema.Learner.PROC_CUSTOM_FIELD_BULK_IMPORT;
                            _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            _sqlpara = new SqlParameter(Schema.CustomField.PARA_CUSTOM_FIELDS, strCFValues);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntLearner.ClientId);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntLearner.LastModifiedById);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            if (!string.IsNullOrEmpty(pEntLearner.DefaultLanguageId))
                                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntLearner.DefaultLanguageId);
                            else
                                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, Language.SYSTEM_DEFAULT_LANG_ID);
                            _sqlcmd.Parameters.Add(_sqlpara);

                            _sqlObject.ExecuteNonQuery(_sqlcmd);


                        }
                        else
                        {
                            UpdateUserCFValue(pEntLearner.UserCustomFieldValue,pEntLearner.IsDoNotDeleteCustomeFiledValue);
                        }

                    }
                    else
                    {
                        DeleteUserCFValue(_entLearner);
                    }

                    if (pIsImport)
                    {
                        //Call for Org Tree 
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.Connection = sqlConn;
                        _sqlcmd.CommandText = Schema.Learner.PROC_ORG_TREE_BULK_IMPORT;
                        _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
                        _sqlcmd.Parameters.Add(_sqlpara);
                        _sqlpara = new SqlParameter(Schema.Learner.PARA_UNITS, pEntLearner.UnitId);
                        _sqlcmd.Parameters.Add(_sqlpara);
                        _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntLearner.LastModifiedById);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        _sqlObject.ExecuteNonQuery(_sqlcmd);
                    }

                    _sqlcmd = new SqlCommand();
                    _sqlcmd.Connection = sqlConn;
                    _sqlcmd.CommandText = Schema.Learner.PROC_SET_USER_INITIAL_SETTINGS;
                    _sqlcmd.CommandTimeout = 0;
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlObject.ExecuteNonQuery(_sqlcmd);

                    if (YPLMS2._0.API.Entity.Client.BASE_CLIENT_ID != pEntLearner.ClientId)
                    {
                        // Added by sarita on 10-Dec-13
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.Connection = sqlConn;
                        _sqlcmd.CommandText = Schema.UserExpiry.PROC_INSERT_USEREXPIRY_WHILE_ADDEDITUSER;
                        _sqlpara = new SqlParameter(Schema.UserExpiry.PARA_SystemUserGUID, pEntLearner.ID);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntLearner.LastModifiedById);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_ON, DateTime.Today.ToUniversalTime());
                        _sqlcmd.Parameters.Add(_sqlpara);
                        _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_UPDATE_MODE);
                        _sqlcmd.Parameters.Add(_sqlpara);
                        _sqlObject.ExecuteNonQuery(_sqlcmd);
                        // END Of Added by sarita on 10-Dec-13
                    }
                return _entLearner;
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            finally
            {
                if (sqlConn != null && sqlConn.State != ConnectionState.Closed)
                    sqlConn.Close();
            }
        }

        /// <summary>
        /// To update user information
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner object</returns>
        public Learner UpdateUserRegiStatus(Learner pEntLearner)
        {
            int iRowsAffected = 0;
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlConn = null;
            string strCustomFields = string.Empty;
            _sqlcmd = new SqlCommand();
             _sqlcmd.CommandText = Schema.Learner.PROC_UPDATE_USER_REGI_STATUS;
           
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.ID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.RegistrationStatus))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_REGISTRATION_STATUS, pEntLearner.RegistrationStatus);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_REGISTRATION_STATUS, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.RejectComments))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_REJECT_COMMENTS, pEntLearner.RejectComments);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_REJECT_COMMENTS, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);

                sqlConn = new SqlConnection(_strConnString);
                sqlConn.Open();

                _sqlcmd.Connection = sqlConn;
                iRowsAffected = _sqlObject.ExecuteNonQueryCount(_sqlcmd);

            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlConn.State != ConnectionState.Closed)
                    sqlConn.Close();
            }
            return pEntLearner;
        }
        /// <summary>
        /// Update All User Status  
        /// </summary>
        /// <param name="pEntUser"></param>
        /// <returns></returns>
        public Learner UpdateAllUserStatus(Learner pEntUser)
        {
            Learner entUser = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_UPDATE_USER_STATUS;
            _sqlObject = new SQLObject();
            try
            {
                if (!String.IsNullOrEmpty(pEntUser.ClientId))
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntUser.ClientId);

                    _sqlpara = new SqlParameter(Schema.Learner.PARA_IS_ACTIVE, pEntUser.IsActive);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntUser.ListRange != null)
                    {
                        if (!string.IsNullOrEmpty(pEntUser.ListRange.RequestedById))
                        {
                            _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntUser.ListRange.RequestedById);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                    }
                    _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                    entUser = pEntUser;
                }
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entUser;
        }

        /// <summary>
        /// To add a new user.
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner AddUser(Learner pEntLearner,bool pIsImport)
        {
            int iRowsAffected = 0;
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlConn = null; 
            string strCustomFields = string.Empty;  
            _sqlcmd = new SqlCommand();

            if (pEntLearner.IsPPUser)
            {
                _sqlcmd.CommandText = Schema.Learner.PROC_UPDATE_PPUSER;
            }
            else
            {
                _sqlcmd.CommandText = Schema.Learner.PROC_UPDATE_USER;
            }

            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.UserNameAlias))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, pEntLearner.UserNameAlias.Trim());
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            //Password Encryption
            if (!String.IsNullOrEmpty(pEntLearner.UserPassword))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_PASSWORD, EncryptionManager.Encrypt(pEntLearner.UserPassword.Trim()));
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_PASSWORD, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.FirstName))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_FIRST_NAME, pEntLearner.FirstName.Trim());
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_FIRST_NAME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.MiddleName))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_MIDDLE_NAME, pEntLearner.MiddleName.Trim());
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_MIDDLE_NAME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.LastName))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_LAST_NAME, pEntLearner.LastName.Trim());
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_LAST_NAME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.Address))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_ADDRESS, pEntLearner.Address);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_ADDRESS, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            if (!String.IsNullOrEmpty(pEntLearner.PreferredDateFormat))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_PREFERREDDATE_FORMAT, pEntLearner.PreferredDateFormat);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_PREFERREDDATE_FORMAT, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            if (!String.IsNullOrEmpty(pEntLearner.PreferredTimeZone))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_PREFERREDTIMEZONE, pEntLearner.PreferredTimeZone);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_PREFERREDTIMEZONE, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            if (!String.IsNullOrEmpty(pEntLearner.EmailID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_EMAIL_ID, pEntLearner.EmailID.Trim());
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_EMAIL_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntLearner.DateOfBirth) < 0)
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_OF_BIRTH, pEntLearner.DateOfBirth);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_OF_BIRTH, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntLearner.DateOfRegistration) < 0)
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_OF_REGISTRATION, pEntLearner.DateOfRegistration);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_OF_REGISTRATION, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntLearner.DateOfTermination) < 0)
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_OF_TERMINATION, pEntLearner.DateOfTermination);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_OF_TERMINATION, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.UserTypeId))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_TYPE_ID, pEntLearner.UserTypeId);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_TYPE_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.DefaultLanguageId))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DEFAULT_LANGUAGE_ID, pEntLearner.DefaultLanguageId);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DEFAULT_LANGUAGE_ID, Language.SYSTEM_DEFAULT_LANG_ID);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.DefaultThemeID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DEFAULT_THEME_ID, pEntLearner.DefaultThemeID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DEFAULT_THEME_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            _sqlpara = new SqlParameter(Schema.Learner.PARA_GENDER, pEntLearner.Gender);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.ManagerId))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_MANAGER_ID, pEntLearner.ManagerId);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_MANAGER_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            //New Para ManagerName added:09-Apr-2010 By:Fatte 
            if (!String.IsNullOrEmpty(pEntLearner.ManagerName))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_MANAGER_NAME, pEntLearner.ManagerName.Trim());
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_MANAGER_NAME, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            if (!String.IsNullOrEmpty(pEntLearner.ManagerEmailId))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_MANAGER_EMAIL_ID, pEntLearner.ManagerEmailId.Trim());
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_MANAGER_EMAIL_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntLearner.DateLastLogin) < 0)
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_LAST_LOGIN, pEntLearner.DateLastLogin);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_LAST_LOGIN, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Learner.PARA_IS_ACTIVE, pEntLearner.IsActive);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.ClientId))
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntLearner.ClientId);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.CreatedById))
                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntLearner.CreatedById);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.LastModifiedById))
                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntLearner.LastModifiedById);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.UnitId) && !pIsImport)
                _sqlpara = new SqlParameter(Schema.Learner.PARA_UNIT_ID, pEntLearner.UnitId);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_UNIT_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.LevelId))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_LEVEL_ID, pEntLearner.LevelId);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_LEVEL_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.CurrentRegionView))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_CURRENT_RV, pEntLearner.CurrentRegionView);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_CURRENT_RV, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.PhoneNo))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_PHONE_NO, pEntLearner.PhoneNo);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_PHONE_NO, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (pEntLearner.IsPPUser)
            {
                if (!String.IsNullOrEmpty(pEntLearner.PPCustomFields))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_PP_CUSTOMFIELDS, pEntLearner.PPCustomFields);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_PP_CUSTOMFIELDS, null);
                _sqlcmd.Parameters.Add(_sqlpara);
            }

            _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
            _sqlcmd.Parameters.Add(_sqlpara);
            
            try
            {                
                pEntLearner.ID = YPLMS.Services.IDGenerator.GetStringGUIDWithPrefix(Schema.Common.VAL_USER_ID_PREFIX);
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
                _sqlcmd.Parameters.Add(_sqlpara);
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);

                sqlConn = new SqlConnection(_strConnString);
                sqlConn.Open();
                
                    _sqlcmd.Connection = sqlConn; 
                    iRowsAffected = _sqlObject.ExecuteNonQueryCount(_sqlcmd);

               
                    if (iRowsAffected < 0)
                    {
                        for (int c = 0; c < pEntLearner.UserCustomFieldValue.Count; c++)
                        {
                            pEntLearner.UserCustomFieldValue[c].SystemUserGUID = pEntLearner.ID;
                            pEntLearner.UserCustomFieldValue[c].CreatedById = pEntLearner.CreatedById;
                            pEntLearner.UserCustomFieldValue[c].LastModifiedById = pEntLearner.LastModifiedById;

                            if (string.IsNullOrEmpty(strCustomFields))
                            {
                                strCustomFields = pEntLearner.UserCustomFieldValue[c].CustomFieldId + "$(!QU0TE!)$" + pEntLearner.UserCustomFieldValue[c].EnteredValue;
                            }
                            else
                            {
                                strCustomFields = strCustomFields + "#(!QU0TE!)#" + pEntLearner.UserCustomFieldValue[c].CustomFieldId + "$(!QU0TE!)$" + pEntLearner.UserCustomFieldValue[c].EnteredValue;
                            }
                        }
                        if (pIsImport)
                        {
                        //To Do new proc for send array of unit id
                        //Call for Custom Field 
                        if (!string.IsNullOrEmpty(strCustomFields))
                        {
                            _sqlcmd = new SqlCommand();
                            _sqlcmd.Connection = sqlConn;
                            _sqlcmd.CommandText = Schema.Learner.PROC_CUSTOM_FIELD_BULK_IMPORT;
                            _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            _sqlpara = new SqlParameter(Schema.CustomField.PARA_CUSTOM_FIELDS, strCustomFields);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntLearner.ClientId);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntLearner.LastModifiedById);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            if (!string.IsNullOrEmpty(pEntLearner.DefaultLanguageId))
                                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntLearner.DefaultLanguageId);
                            else
                                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, Language.SYSTEM_DEFAULT_LANG_ID);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            _sqlObject.ExecuteNonQuery(_sqlcmd);
                        }
                        //Call for Org Tree 
                        
                            _sqlcmd = new SqlCommand();
                            _sqlcmd.Connection = sqlConn;
                            _sqlcmd.CommandText = Schema.Learner.PROC_ORG_TREE_BULK_IMPORT;
                            _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            _sqlpara = new SqlParameter(Schema.Learner.PARA_UNITS, pEntLearner.UnitId);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntLearner.LastModifiedById);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            _sqlObject.ExecuteNonQuery(_sqlcmd);
                        }
                        else
                        {
                            UpdateUserCFValue(pEntLearner.UserCustomFieldValue,false );
                        }

                        //Added by bajirao for ARAI customization
                        // Update Student Details For ARAI
                        if (!string.IsNullOrEmpty(pEntLearner.IdProofDocument))
                        {
                        string strStudentDetailId = "0";//Services.IDGenerator.GetStringGUIDWithPrefix(Schema.Common.VAL_STUDENT_DETAIL_PREFIX); 
                                
                                _sqlcmd = new SqlCommand();
                                _sqlcmd.Connection = sqlConn;
                                _sqlcmd.CommandText = Schema.Learner.PROC_UPDATE_USER_STUDENT_DETAILS;
                                _sqlpara = new SqlParameter(Schema.Learner.PARA_STUDENTS_DETAIL_ID, strStudentDetailId);
                                _sqlcmd.Parameters.Add(_sqlpara);
                                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
                                _sqlcmd.Parameters.Add(_sqlpara);
                                _sqlpara = new SqlParameter(Schema.Learner.PARA_STUDENTS_IDPROOFDOCUMENT, pEntLearner.IdProofDocument);
                                _sqlcmd.Parameters.Add(_sqlpara);
                                _sqlpara = new SqlParameter(Schema.Learner.PARA_STUDENTS_REGISTRATION_STATUS, Convert.ToString(YPLMS2._0.API.Entity.StudentList.RegistrationStatus.Pending));
                                _sqlcmd.Parameters.Add(_sqlpara);
                                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntLearner.LastModifiedById);
                                _sqlcmd.Parameters.Add(_sqlpara);
                                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntLearner.LastModifiedById);
                                _sqlcmd.Parameters.Add(_sqlpara);
                                _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                                _sqlcmd.Parameters.Add(_sqlpara);
                                _sqlObject.ExecuteNonQuery(_sqlcmd);

                            
                    }
                    }

                    #region UpdateUserScope
                        /*
                    Change:New proc execute call added to add/update user in scope (by rule or orgtree) by current adminuser scope 
                    Date:16-Feb-2010
                    By:fattesinh
                    */
                    _sqlcmd = new SqlCommand();
                    _sqlcmd.Connection = sqlConn;  
                    _sqlcmd.CommandText = Schema.Learner.PROC_UPDATE_USER_BY_ADMINSCOPE;
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntLearner.LastModifiedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    _sqlObject.ExecuteNonQuery(_sqlcmd);
                    #endregion
                
                    sqlcmd = new SqlCommand();
                    sqlcmd.Connection = sqlConn; 
                    sqlcmd.CommandText = Schema.Learner.PROC_SET_USER_INITIAL_SETTINGS;
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
                    sqlcmd.Parameters.Add(_sqlpara);
                    _sqlObject.ExecuteNonQuery(sqlcmd);

                
                // Added by sarita on 10-Dec-13
                    if (YPLMS2._0.API.Entity.Client.BASE_CLIENT_ID  != pEntLearner.ClientId)
                    {

                        _sqlcmd = new SqlCommand();
                        _sqlcmd.Connection = sqlConn;
                        _sqlcmd.CommandText = Schema.UserExpiry.PROC_INSERT_USEREXPIRY_WHILE_ADDEDITUSER;
                        _sqlpara = new SqlParameter(Schema.UserExpiry.PARA_SystemUserGUID, pEntLearner.ID);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntLearner.LastModifiedById);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_ON, DateTime.Today.ToUniversalTime());
                        _sqlcmd.Parameters.Add(_sqlpara);

                        _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                        _sqlcmd.Parameters.Add(_sqlpara);
                        _sqlObject.ExecuteNonQuery(_sqlcmd);
                    }
                // END Of Added by sarita on 10-Dec-13
                }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            finally
            {
                if (sqlConn.State != ConnectionState.Closed)
                    sqlConn.Close();
            }
            return pEntLearner;
        }
        /// <summary>
        /// To add a new user for Bulk Import.
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner AddUserImport(Learner pEntLearner)
        {
            return AddUpdateUserByImport(pEntLearner,Schema.Common.VAL_INSERT_MODE);    
        }
        /// <summary>
        /// To Update a user for Bulk Import.
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner</returns>
        public Learner UpdateUserImport(Learner pEntLearner)
        {
            return AddUpdateUserByImport(pEntLearner, Schema.Common.VAL_UPDATE_MODE);
        }
        /// <summary>
        /// To add/update a user for Bulk Import.
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <param name="pMode">Insert/Update</param>
        /// <returns>Learner</returns>
        private Learner AddUpdateUserByImport(Learner pEntLearner,string pMode)
        {
            int iRowsAffected = 0;
            SqlCommand sqlcmd = new SqlCommand();           
            string strCustomFields = string.Empty;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_UPDATE_USER_IMPORT;

            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.UserNameAlias))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, pEntLearner.UserNameAlias);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            //Password Encryption
            if (!String.IsNullOrEmpty(pEntLearner.UserPassword))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_PASSWORD, EncryptionManager.Encrypt(pEntLearner.UserPassword));
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_PASSWORD, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.FirstName))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_FIRST_NAME, pEntLearner.FirstName);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_FIRST_NAME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.MiddleName))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_MIDDLE_NAME, pEntLearner.MiddleName);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_MIDDLE_NAME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.LastName))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_LAST_NAME, pEntLearner.LastName);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_LAST_NAME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.Address))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_ADDRESS, pEntLearner.Address);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_ADDRESS, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.EmailID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_EMAIL_ID, pEntLearner.EmailID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_EMAIL_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntLearner.DateOfBirth) < 0)
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_OF_BIRTH, pEntLearner.DateOfBirth);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_OF_BIRTH, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntLearner.DateOfRegistration) < 0)
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_OF_REGISTRATION, pEntLearner.DateOfRegistration);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_OF_REGISTRATION, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntLearner.DateOfTermination) < 0)
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_OF_TERMINATION, pEntLearner.DateOfTermination);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_OF_TERMINATION, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.UserTypeId))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_TYPE_ID, pEntLearner.UserTypeId);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_TYPE_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.DefaultLanguageId))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DEFAULT_LANGUAGE_ID, pEntLearner.DefaultLanguageId);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DEFAULT_LANGUAGE_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.DefaultThemeID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DEFAULT_THEME_ID, pEntLearner.DefaultThemeID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DEFAULT_THEME_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            _sqlpara = new SqlParameter(Schema.Learner.PARA_GENDER, pEntLearner.Gender);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.ManagerId))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_MANAGER_ID, pEntLearner.ManagerId);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_MANAGER_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.ManagerName))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_MANAGER_NAME, pEntLearner.ManagerName);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_MANAGER_NAME, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.ManagerEmailId))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_MANAGER_EMAIL_ID, pEntLearner.ManagerEmailId);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_MANAGER_EMAIL_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (DateTime.MinValue.CompareTo(pEntLearner.DateLastLogin) < 0)
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_LAST_LOGIN, pEntLearner.DateLastLogin);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_LAST_LOGIN, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Learner.PARA_IS_ACTIVE, pEntLearner.IsActive);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.ClientId))
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntLearner.ClientId);
            else
                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.CreatedById))
                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntLearner.CreatedById);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.LastModifiedById))
                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pEntLearner.LastModifiedById);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.UnitId))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_UNIT_ID, pEntLearner.UnitId);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_UNIT_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.LevelId))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_LEVEL_ID, pEntLearner.LevelId);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_LEVEL_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.CurrentRegionView))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_CURRENT_RV, pEntLearner.CurrentRegionView);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_CURRENT_RV, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.PhoneNo))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_PHONE_NO, pEntLearner.PhoneNo);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_PHONE_NO, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            
            //added by Gitanjali 26.11.2010
            if (!String.IsNullOrEmpty(pEntLearner.PreferredDateFormat))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_PREFERREDDATE_FORMAT, pEntLearner.PreferredDateFormat);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_PREFERREDDATE_FORMAT, System.DBNull.Value   );
            _sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntLearner.PreferredTimeZone))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_PREFERREDTIMEZONE, pEntLearner.PreferredTimeZone);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_PREFERREDTIMEZONE, System.DBNull.Value   );
            _sqlcmd.Parameters.Add(_sqlpara);


            UserAdminRole adminRole = pEntLearner.UserAdminRole.Find(delegate(UserAdminRole adminRoleToMatch) { return (adminRoleToMatch.AdminRoleType == RoleType.Admin); });
            if (adminRole!=null)
            {
                if (!String.IsNullOrEmpty(adminRole.RoleId))
                {
                    if (pEntLearner.IsSuperAdmin())
                    {
                        adminRole.RoleId = AdminRole.SITE_ADMIN_ROLE_ID;  
                    }
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_ROLE_ID, adminRole.RoleId);
                }
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_ROLE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(adminRole.RuleId))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_RULE_ID, adminRole.RuleId);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_RULE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

            }

            _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, pMode);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                if (string.IsNullOrEmpty(pEntLearner.ID))
                {
                    pEntLearner.ID = YPLMS.Services.IDGenerator.GetStringGUIDWithPrefix(Schema.Common.VAL_USER_ID_PREFIX);
                }
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
                _sqlcmd.Parameters.Add(_sqlpara);               
                
                for (int c = 0; c < pEntLearner.UserCustomFieldValue.Count; c++)
                {                  

                    if (string.IsNullOrEmpty(strCustomFields))
                    {
                        strCustomFields = pEntLearner.UserCustomFieldValue[c].CustomFieldId + "$(!QU0TE!)$" + pEntLearner.UserCustomFieldValue[c].EnteredValue;
                    }
                    else
                    {
                        strCustomFields = strCustomFields + "#(!QU0TE!)#" + pEntLearner.UserCustomFieldValue[c].CustomFieldId + "$(!QU0TE!)$" + pEntLearner.UserCustomFieldValue[c].EnteredValue;
                    }
                }
                if(!string.IsNullOrEmpty(strCustomFields))
                {
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_CFIELDS_ARRAY, strCustomFields);
                    _sqlcmd.Parameters.Add(_sqlpara);  
                }
                

                /* Changed by pravin + fatte on 12-May-2010.*/
                //_strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);                
                //iRowsAffected = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);

                _sqlcmd.Connection = new SqlConnection(_strConnString); // pEntLearner.ImportConnection;
                iRowsAffected = _sqlObject.ExecuteNonQueryCount(_sqlcmd);

            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }           
            return pEntLearner;
        }
        /// <summary>
        ///  To Update custom field Values for multiple users/customfields/customfielditems.
        ///  Set sConnString and SystemUserGUID before calling this function
        /// </summary>
        /// <param name="pListCustomFieldValue"></param>
        /// <param name="pStrUpdateMode"></param>
        /// <returns></returns>
        private bool UpdateUserCFValue(List<UserCustomFieldValue> pListCustomFieldValue, bool pIsDeleteCustomFileds)
        {
            SqlDataAdapter sqladapterInsert;
            DataTable dtableUserCustVal = new DataTable();
            DataRow row = null;
            DataSet dsetExistingVal = new DataSet();
            int iBatchSize = 0;
            string strItemIds = string.Empty;
            string strUserId = string.Empty;
            SQLObject thisObject = new SQLObject();
            try
            {
                _sqlcon = new SqlConnection(_strConnString);

                //Check for new Insert
                dtableUserCustVal.Columns.Add(Schema.UserCustomFieldValue.COL_CUSTOM_FIELD_ITEM_ID);
                dtableUserCustVal.Columns.Add(Schema.Learner.COL_USER_ID);
                dtableUserCustVal.Columns.Add(Schema.UserCustomFieldValue.COL_ENTERED_VALUE);
                dtableUserCustVal.Columns.Add(Schema.CustomField.COL_CUSTOM_FIELD_ID);
                dtableUserCustVal.Columns.Add(Schema.Common.COL_CREATED_BY);
                dtableUserCustVal.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                dtableUserCustVal.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                foreach (UserCustomFieldValue custValue in pListCustomFieldValue)
                {
                    row = dtableUserCustVal.NewRow();
                    row[Schema.UserCustomFieldValue.COL_CUSTOM_FIELD_ITEM_ID] = custValue.CustomFieldItemId;
                    row[Schema.CustomField.COL_CUSTOM_FIELD_ID] = custValue.CustomFieldId;
                    row[Schema.Learner.COL_USER_ID] = custValue.SystemUserGUID;
                    row[Schema.UserCustomFieldValue.COL_ENTERED_VALUE] = custValue.EnteredValue;
                    if (String.IsNullOrEmpty(custValue.CreatedById))
                        custValue.CreatedById = custValue.SystemUserGUID;
                    if (String.IsNullOrEmpty(custValue.LastModifiedById))
                        custValue.LastModifiedById = custValue.SystemUserGUID;
                    row[Schema.Common.COL_CREATED_BY] = custValue.CreatedById;
                    row[Schema.CustomField.COL_CUSTOM_FIELD_ID] = custValue.CustomFieldId;
                    row[Schema.Common.COL_MODIFIED_BY] = custValue.LastModifiedById;
                    row[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_INSERT_MODE;

                    dtableUserCustVal.Rows.Add(row);
                    iBatchSize++;
                    strUserId = custValue.SystemUserGUID;
                    if (!string.IsNullOrEmpty(strItemIds))
                        strItemIds = strItemIds + "," + custValue.CustomFieldItemId;
                    else
                        strItemIds = custValue.CustomFieldItemId;
                }

                using (SqlConnection sqlconField = new SqlConnection(_strConnString))
                {
                    SqlTransaction transaction = null;
                    bool tr = false;
                    bool trRollBack = false;
                    try
                    {
                        sqlconField.Open();
                        transaction = sqlconField.BeginTransaction();
                        // new code added to delete not in used cf values.
                        if (!string.IsNullOrEmpty(strUserId) && !string.IsNullOrEmpty(strItemIds))
                        {
                            _sqlcmd = new SqlCommand();
                            _sqlcmd.Connection = sqlconField;
                            _sqlcmd.Transaction = transaction;
                            _sqlcmd.CommandText = Schema.UserCustomFieldValue.PROC_DELETE_NOT_IN_CFVALUES;
                            _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, strUserId);
                            _sqlcmd.Parameters.Add(_sqlpara);

                            if ((pIsDeleteCustomFileds))
                            {
                                _sqlpara = new SqlParameter(Schema.Learner.PARA_DO_DELETE_CUSTOMFILEDS, pIsDeleteCustomFileds);
                                _sqlcmd.Parameters.Add(_sqlpara);
                            }
                            _sqlpara = new SqlParameter(Schema.UserCustomFieldValue.PARA_CUSTOM_FIELD_ITEM_ID, strItemIds);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            thisObject.ExecuteNonQuery(_sqlcmd);
                        }
                        sqladapterInsert = new SqlDataAdapter();
                        iRecordsUpdated = 0;
                        sqladapterInsert.RowUpdating += new SqlRowUpdatingEventHandler(sqlDataAdapter_RowUpdating);
                        _sqlcmd = new SqlCommand(Schema.UserCustomFieldValue.PROC_UPDATE_CUSTOM_FIELD_VALUE, sqlconField);
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcmd.Transaction = transaction;
                        sqladapterInsert.InsertCommand = _sqlcmd;
                        sqladapterInsert.InsertCommand.Parameters.Add(Schema.UserCustomFieldValue.PARA_CUSTOM_FIELD_ITEM_ID, SqlDbType.VarChar, 100, Schema.UserCustomFieldValue.COL_CUSTOM_FIELD_ITEM_ID);
                        sqladapterInsert.InsertCommand.Parameters.Add(Schema.CustomField.PARA_CUSTOM_FIELD_ID, SqlDbType.VarChar, 100, Schema.CustomField.COL_CUSTOM_FIELD_ID);
                        sqladapterInsert.InsertCommand.Parameters.Add(Schema.Learner.PARA_USER_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_USER_ID);
                        sqladapterInsert.InsertCommand.Parameters.Add(Schema.UserCustomFieldValue.PARA_ENTERED_VALUE, SqlDbType.NVarChar, 500, Schema.UserCustomFieldValue.COL_ENTERED_VALUE);
                        sqladapterInsert.InsertCommand.Parameters.Add(Schema.Common.PARA_CREATED_BY, SqlDbType.NVarChar, 100, Schema.Common.COL_CREATED_BY);
                        sqladapterInsert.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.NVarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        sqladapterInsert.InsertCommand.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.NVarChar, 10, Schema.Common.COL_UPDATE_MODE);
                        sqladapterInsert.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        sqladapterInsert.UpdateBatchSize = iBatchSize;
                        sqladapterInsert.Update(dtableUserCustVal);
                        transaction.Commit();
                        tr = true;
                    }
                    catch (Exception expCommon)
                    {
                        if (!tr)
                        {
                            transaction.Rollback();
                            trRollBack = true;
                        }
                        throw _expCustom;
                    }
                    finally
                    {
                        if (!tr)
                        {
                            if (trRollBack == false)
                            {
                                transaction.Rollback();
                                trRollBack = true;
                            }
                        }
                        sqlconField.Close();
                    }
                }
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, new Exception(iRecordsUpdated.ToString()), false);
                throw _expCustom;
            }
            return true;
        }

        /// <summary>
        /// To delete all custom field value of a user.
        /// </summary>
        /// <param name="pEntUser"></param>
        /// <returns></returns>
        public Learner DeleteUserCFValue(Learner pEntUser)
        {
            Learner entUser = null;
            SQLObject delSqlObject = new SQLObject();
            try
            {
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Learner.PROC_DELETE_USER_CF_VALUES;

                if (!String.IsNullOrEmpty(pEntUser.ID))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntUser.ID);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if ((pEntUser.IsDoNotDeleteCustomeFiledValue))
                {
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_DO_DELETE_CUSTOMFILEDS, pEntUser.IsDoNotDeleteCustomeFiledValue);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _strConnString = _sqlObject.GetClientDBConnString(pEntUser.ClientId);
                delSqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entUser;
        }

        /// <summary>
        /// Delete User by ID
        /// </summary>
        /// <param name="pEntUser">User with ID</param>
        /// <returns>Deleted User with only ID</returns>
        public Learner DeleteUser(Learner pEntUser)
        {
            Learner entUser = null;
            _sqlcmd = new SqlCommand(); 
            _sqlcmd.CommandText = Schema.Learner.PROC_DELETE_USER;
            _sqlObject = new SQLObject();
            int i = 0;
            if (!String.IsNullOrEmpty(pEntUser.ID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntUser.ID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);

            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntUser.ClientId);
                i = _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
                if (i < 0)
                {
                    entUser = new Learner();
                    entUser.ID = pEntUser.ID;
                }
                else
                {
                    //No record effected
                }
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entUser;
        }

        /// <summary>
        /// Assign Managers
        /// </summary>
        /// <param name="pEntUser"></param>
        /// <returns></returns>
        public Learner AssignManagers(Learner pEntUser)
        {
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Learner.PROC_ASSIGN_MANAGERS;
                _strConnString = _sqlObject.GetClientDBConnString(pEntUser.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                sqlConnection.Open();
                _sqlcmd.Connection = sqlConnection;
                _sqlcmd.CommandTimeout = 0;
                _sqlObject.ExecuteNonQuery(_sqlcmd);
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {

                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return pEntUser;
        }


        /// <summary>
        /// Create or Drop Indexes FOR user import functionality
        /// </summary>
        /// <param name="pEntUser"></param>
        /// <returns></returns>
        public Learner UpdateDBIndexes(Learner pEntUser)
        {
            SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _sqlcmd = new SqlCommand();
                if(pEntUser.IsActive)
                    _sqlcmd.CommandText = Schema.Learner.PROC_CREATE_UD_INDEXES;
                else
                    _sqlcmd.CommandText = Schema.Learner.PROC_DROP_UD_INDEXES;

                _strConnString = _sqlObject.GetClientDBConnString(pEntUser.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                sqlConnection.Open();
                _sqlcmd.Connection = sqlConnection;
                _sqlcmd.CommandTimeout = 0;
                _sqlObject.ExecuteNonQuery(_sqlcmd);
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {

                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return pEntUser;
        }


        /// <summary>
        /// Synch business rule users after user import functionality
        /// </summary>
        /// <param name="pEntUser"></param>
        /// <returns></returns>
        public Learner SynchBusinessRuleUsersOnImport(Learner pEntUser)
        {
            //SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Learner.PROC_SYNC_RULEUSERS_AFTER_IMPORT;
                
                //_strConnString = _sqlObject.GetClientDBConnString(pEntUser.ClientId);
                //sqlConnection = new SqlConnection(_strConnString);
                //sqlConnection.Open();
                _sqlcmd.Connection = new SqlConnection(_strConnString); // pEntUser.ImportConnection;
                _sqlcmd.CommandTimeout = 0;
                _sqlObject.ExecuteNonQuery(_sqlcmd);
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {

                //if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                //    sqlConnection.Close();
            }
            return pEntUser;
        }


        /// <summary>
        /// Set User Initial Rule Activities after import
        /// </summary>
        /// <param name="pEntUser"></param>
        /// <returns></returns>
        public Learner SetUserInitialSettingOnImport(Learner pEntUser)
        {
            //SqlConnection sqlConnection = null;
            _sqlObject = new SQLObject();
            try
            {
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Learner.PROC_SET_USERINITIAL_SETTINGS_AFTER_IMPORT;

                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, pEntUser.UserNameAlias);
                _sqlcmd.Parameters.Add(_sqlpara);

                //_strConnString = _sqlObject.GetClientDBConnString(pEntUser.ClientId);
                //sqlConnection = new SqlConnection(_strConnString);
                //sqlConnection.Open();
                _sqlcmd.Connection = new SqlConnection(_strConnString); // pEntUser.ImportConnection;
                _sqlcmd.CommandTimeout = 0;
                _sqlObject.ExecuteNonQuery(_sqlcmd);
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {

                //if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                //    sqlConnection.Close();
            }
            return pEntUser;
        }

        /// <summary>
        /// to validate import user
        /// </summary>
        /// <param name="pEntUser"></param>
        /// <returns></returns>
        public Learner ValidateImportUser(Learner pEntUser, ImportAction pImportAction)
        {
            //SqlConnection sqlConnection = null;
            string strCFieldPatamater = string.Empty;
            _sqlObject = new SQLObject();

            try
            {
                _sqlcmd = new SqlCommand();
                _sqlcmd.CommandText = Schema.Learner.PROC_VALIDATE_IMPORT_USER;
                //_strConnString = _sqlObject.GetClientDBConnString(pEntUser.ClientId);

                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, pEntUser.UserNameAlias);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Learner.PARA_FIRST_NAME, pEntUser.FirstName);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntUser.LastName))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_LAST_NAME, pEntUser.LastName);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_LAST_NAME, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntUser.EmailID))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_EMAIL_ID, pEntUser.EmailID);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_EMAIL_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntUser.DateOfBirth) < 0)
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_OF_BIRTH, pEntUser.DateOfBirth);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_OF_BIRTH, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (DateTime.MinValue.CompareTo(pEntUser.DateOfRegistration) < 0)
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_OF_REGISTRATION, pEntUser.DateOfRegistration);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_DATE_OF_REGISTRATION, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntUser.ManagerEmailId))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_MANAGER_EMAIL_ID, pEntUser.ManagerEmailId);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_MANAGER_EMAIL_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntUser.ListRange != null && !string.IsNullOrEmpty(pEntUser.ListRange.RequestedById))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pEntUser.ListRange.RequestedById);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntUser.UnitId))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_UNIT_ID, pEntUser.UnitId);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_UNIT_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntUser.LevelId))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_LEVEL_ID, pEntUser.LevelId);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_LEVEL_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (!string.IsNullOrEmpty(pEntUser.DefaultLanguageId))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_DEFAULT_LANGUAGE_ID, pEntUser.DefaultLanguageId);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_DEFAULT_LANGUAGE_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pEntUser.UserCustomFieldValue.Count > 0)
                {
                    foreach (UserCustomFieldValue objUCFieldValue in pEntUser.UserCustomFieldValue)
                    {
                        if (string.IsNullOrEmpty(strCFieldPatamater))
                        {
                            if (objUCFieldValue.CustomFieldItemId == objUCFieldValue.CustomFieldId)
                                strCFieldPatamater = objUCFieldValue.CustomFieldId + "#(!QU0TE!)#" + objUCFieldValue.EnteredValue;
                            else
                                strCFieldPatamater = objUCFieldValue.CustomFieldId + "$(!QU0TE!)$" + objUCFieldValue.CustomFieldItemId;
                        }
                        else
                        {
                            if (objUCFieldValue.CustomFieldItemId == objUCFieldValue.CustomFieldId)
                                strCFieldPatamater = strCFieldPatamater + "@(!QU0TE!)@" + objUCFieldValue.CustomFieldId + "#(!QU0TE!)#" + objUCFieldValue.EnteredValue;
                            else
                                strCFieldPatamater = strCFieldPatamater + "@(!QU0TE!)@" + objUCFieldValue.CustomFieldId + "$(!QU0TE!)$" + objUCFieldValue.CustomFieldItemId;
                        }

                    }
                }
                if (!string.IsNullOrEmpty(strCFieldPatamater))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_DYNAMIC_WH_CFIELDS, strCFieldPatamater);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_DYNAMIC_WH_CFIELDS, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                if (pImportAction != ImportAction.None)
                    _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_ACTION, pImportAction.ToString());
                else
                    _sqlpara = new SqlParameter(Schema.ImportHistory.PARA_IMPORT_ACTION, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                // Code changed by pravin +fatte on 12-May-2010.
                //sqlConnection = new SqlConnection(_strConnString);
                //_sqlcmd.Connection = sqlConnection;

                _sqlcmd.Connection = new SqlConnection(_strConnString);// pEntUser.ImportConnection;

                _entLearner = new Learner();
                //_sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, true);
                _sqlreader.Read();
                pEntUser = FillValidateUser(_sqlreader, pEntUser, _sqlObject);
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (_sqlreader != null && !_sqlreader.IsClosed) _sqlreader.Close();
                //if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                //    sqlConnection.Close();
            }
            return pEntUser;
        }

        /// <summary>
        /// Fill validate import user data.
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns></returns>
        public Learner FillValidateUser(SqlDataReader pSqlReader, Learner pEntLearner, SQLObject pSqlObject)
        {
            int index;
            UserCustomFieldValue entUserCustomFieldValue = null;
            if (pSqlReader.HasRows)
            {
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.ID = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_NAME_ALIAS))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_NAME_ALIAS);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.UserNameAlias = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_FIRST_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_FIRST_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.FirstName = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_LAST_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_LAST_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.LastName = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_EMAIL_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_EMAIL_ID);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.EmailID = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_MANAGER_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_MANAGER_ID);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.ManagerId = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_MANAGER_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_MANAGER_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.ManagerName = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_MANAGER_EMAIL))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_MANAGER_EMAIL);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.ManagerEmailId = pSqlReader.GetString(index);
                }
                // added by Gitanjali 15.9.2010
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_IS_INSCOPE ))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_IS_INSCOPE);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.IsInScope = Convert.ToBoolean(pSqlReader.GetValue(index).ToString());
                }

                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_MIDDLE_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_MIDDLE_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.MiddleName = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_PHONE_NO))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_PHONE_NO);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.PhoneNo = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_PASSWORD))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_PASSWORD);
                    if (!pSqlReader.IsDBNull(index))
                    {
                        pEntLearner.UserPassword = pSqlReader.GetString(index);
                        pEntLearner.UserPassword = EncryptionManager.Decrypt(pEntLearner.UserPassword);
                    }
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_ADDRESS))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_ADDRESS);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.Address = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DATE_OF_BIRTH))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_DATE_OF_BIRTH);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.DateOfBirth = pSqlReader.GetDateTime(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DATE_OF_REGISTRATION))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_DATE_OF_REGISTRATION);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.DateOfRegistration = pSqlReader.GetDateTime(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DATE_OF_TERMINATION))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_DATE_OF_TERMINATION);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.DateOfTermination = pSqlReader.GetDateTime(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_USER_TYPE_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_TYPE_ID);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.UserTypeId = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DEFAULT_LANGUAGE_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_DEFAULT_LANGUAGE_ID);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.DefaultLanguageId = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DEFAULT_THEME_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_DEFAULT_THEME_ID);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.DefaultThemeID = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_GENDER))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_GENDER);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.Gender = pSqlReader.GetBoolean(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_MANAGER_NAME))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_MANAGER_NAME);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.ManagerName = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DATE_LAST_LOGIN))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_DATE_LAST_LOGIN);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.DateLastLogin = pSqlReader.GetDateTime(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_IS_ACTIVE))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_IS_ACTIVE);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.IsActive = pSqlReader.GetBoolean(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Client.COL_CLIENT_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.ClientId = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_BY))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.CreatedById = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_CREATED_ON))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.DateCreated = pSqlReader.GetDateTime(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_BY))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.LastModifiedById = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Common.COL_MODIFIED_ON))
                {
                    index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.LastModifiedDate = pSqlReader.GetDateTime(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_UNIT_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_UNIT_ID);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.UnitId = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_LEVEL_ID))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_LEVEL_ID);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.LevelId = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_DEFAULT_RV))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_DEFAULT_RV);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.DefaultRegionView = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_CURRENT_RV))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_CURRENT_RV);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.CurrentRegionView = pSqlReader.GetString(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_IS_FIRST_LOGIN))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_IS_FIRST_LOGIN);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.IsFirstLogin = pSqlReader.GetBoolean(index);
                }
                if (pSqlObject.ReaderHasColumn(pSqlReader, Schema.Learner.COL_IS_PASSWORD_EXPIRED))
                {
                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_IS_PASSWORD_EXPIRED);
                    if (!pSqlReader.IsDBNull(index))
                        pEntLearner.IsPasswordExpired = pSqlReader.GetBoolean(index);
                }

                //below code added by fatte+pravin as on 15-apr-2010
                pSqlReader.NextResult();

                while (pSqlReader.Read())
                {
                    entUserCustomFieldValue = new UserCustomFieldValue();

                    index = pSqlReader.GetOrdinal(Schema.UserCustomFieldValue.COL_CUSTOM_FIELD_ITEM_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.CustomFieldItemId = pSqlReader.GetString(index);

                    index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.SystemUserGUID = pSqlReader.GetString(index);

                    entUserCustomFieldValue.ID = entUserCustomFieldValue.CustomFieldItemId + entUserCustomFieldValue.SystemUserGUID;

                    index = pSqlReader.GetOrdinal(Schema.UserCustomFieldValue.COL_ENTERED_VALUE);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.EnteredValue = pSqlReader.GetString(index);

                    index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_BY);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.CreatedById = pSqlReader.GetString(index);

                    index = pSqlReader.GetOrdinal(Schema.Common.COL_CREATED_ON);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.DateCreated = pSqlReader.GetDateTime(index);

                    index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.LastModifiedById = pSqlReader.GetString(index);

                    index = pSqlReader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.LastModifiedDate = pSqlReader.GetDateTime(index);

                    index = pSqlReader.GetOrdinal(Schema.CustomField.COL_CUSTOM_FIELD_ID);
                    if (!pSqlReader.IsDBNull(index))
                        entUserCustomFieldValue.CustomFieldId = pSqlReader.GetString(index);

                    pEntLearner.UserCustomFieldValue.Add(entUserCustomFieldValue);
                }


            }
            return pEntLearner;
        }

        /// <summary>
        /// To Update User First Login
        /// </summary>
        /// <param name="pEntUser"></param>
        /// <returns></returns>
        public Learner UpdateUserFirstLogin(Learner pEntUser)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_UPDATE_FIRST_LOGIN;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntUser.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Learner.PARA_IS_FIRST_LOGIN, pEntUser.IsFirstLogin);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetClientDBConnString(pEntUser.ClientId);

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntUser;
        }

        /// <summary>
        /// To Update User lock or unlock for OTP system. added by bharat:17-Dec-2015
        /// </summary>
        /// <param name="pEntUser"></param>
        /// <returns></returns>
        public Learner UpdateLockUnlockOTP(Learner pEntUser)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_USER_LOCK_UNLOCK_OTP;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntUser.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Learner.PARA_IS_USER_LOCK, pEntUser.IsUserLock);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetClientDBConnString(pEntUser.ClientId);

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntUser;
        }
        /// <summary>
        /// To Update user language
        /// </summary>
        /// <param name="pEntUser"></param>
        /// <returns></returns>
        public Learner UpdateUserLanguage(Learner pEntUser)
        {
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_UPDATE_USER_LANGUAGE;
            _sqlObject = new SQLObject();
            try
            {
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntUser.ID);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Language.PARA_LANGUAGE_ID, pEntUser.DefaultLanguageId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pEntUser.ClientId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _strConnString = _sqlObject.GetClientDBConnString(pEntUser.ClientId);
                _sqlcmd.CommandTimeout = 0;
                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntUser;
        }

        /// <summary>
        /// To delete multiple users
        /// </summary>
        /// <param name="pEntListBase"></param>
        /// <returns>null if success</returns>
        public List<Learner> DeleteSelectedUser(List<Learner> pEntListBase)
        {
            List<Learner> entListUsers = new List<Learner>();
            SqlCommand sqlcmd;
            sqlcmd = new SqlCommand(Schema.Learner.PROC_DELETE_SEL_USER);
            string strUserIds = String.Empty;
            int iResult = 0;
            _sqlObject = new SQLObject();
            try
            {
                _entLearner = pEntListBase[0];
                _strConnString = _sqlObject.GetClientDBConnString(_entLearner.ClientId);
                foreach (Learner entBase in pEntListBase)
                {
                    _entLearner = new Learner();
                    _entLearner.ID = entBase.ID;
                    entListUsers.Add(_entLearner);
                    if (String.IsNullOrEmpty(strUserIds))
                        strUserIds = _entLearner.ID;
                    else strUserIds = strUserIds + "," + _entLearner.ID;
                }
                sqlcmd.Parameters.AddWithValue(Schema.Learner.PARA_MULTI_USER_IDS, strUserIds);
                iResult = _sqlObject.ExecuteNonQuery(sqlcmd, _strConnString);
                if (iResult > 0)
                {
                    entListUsers = null;
                }
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListUsers;
        }

        /// <summary>
        /// Fill Learner object from a Row
        /// </summary>
        /// <param name="pDRow">DataRow</param>
        /// <returns>Learner</returns>
        private Learner FillLearner(DataRow pDRow)
        {
            EntityRange entRange = new EntityRange();
            _entLearner = new Learner();

            _entLearner.ID = Convert.ToString(pDRow[Schema.Learner.COL_USER_ID]);

            if (pDRow[Schema.Learner.COL_FIRST_NAME] != null)
                _entLearner.FirstName = Convert.ToString(pDRow[Schema.Learner.COL_FIRST_NAME]);

            if (pDRow[Schema.Learner.COL_MIDDLE_NAME] != null)
                _entLearner.MiddleName = Convert.ToString(pDRow[Schema.Learner.COL_MIDDLE_NAME]);

            if (pDRow[Schema.Learner.COL_LAST_NAME] != null)
                _entLearner.LastName = Convert.ToString(pDRow[Schema.Learner.COL_LAST_NAME]);

            if (pDRow[Schema.Learner.COL_EMAIL_ID] != null)
                _entLearner.EmailID = Convert.ToString(pDRow[Schema.Learner.COL_EMAIL_ID]);

            if (pDRow[Schema.Learner.COL_PHONE_NO] != null)
                _entLearner.PhoneNo = Convert.ToString(pDRow[Schema.Learner.COL_PHONE_NO]);

            if (pDRow[Schema.Learner.COL_USER_NAME_ALIAS] != null)
                _entLearner.UserNameAlias = Convert.ToString(pDRow[Schema.Learner.COL_USER_NAME_ALIAS]);

            if (pDRow[Schema.Learner.COL_USER_PASSWORD] != null)
            {
                _entLearner.UserPassword = Convert.ToString(pDRow[Schema.Learner.COL_USER_PASSWORD]);
                _entLearner.UserPassword = EncryptionManager.Decrypt(_entLearner.UserPassword);
            }

            if (pDRow[Schema.Learner.COL_ADDRESS] != null)
                _entLearner.Address = Convert.ToString(pDRow[Schema.Learner.COL_ADDRESS]);

            if (!pDRow.IsNull(Schema.Learner.COL_DATE_OF_BIRTH))
                _entLearner.DateOfBirth = Convert.ToDateTime(pDRow[Schema.Learner.COL_DATE_OF_BIRTH]);

            if (!pDRow.IsNull(Schema.Learner.COL_DATE_OF_REGISTRATION))
                _entLearner.DateOfRegistration = Convert.ToDateTime(pDRow[Schema.Learner.COL_DATE_OF_REGISTRATION]);

            if (!pDRow.IsNull(Schema.Learner.COL_DATE_OF_TERMINATION))
                _entLearner.DateOfTermination = Convert.ToDateTime(pDRow[Schema.Learner.COL_DATE_OF_TERMINATION]);

            if (!pDRow.IsNull(Schema.Learner.COL_USER_TYPE_ID))
                _entLearner.UserTypeId = Convert.ToString(pDRow[Schema.Learner.COL_USER_TYPE_ID]);

            if (!pDRow.IsNull(Schema.Learner.COL_DEFAULT_LANGUAGE_ID))
                _entLearner.DefaultLanguageId = Convert.ToString(pDRow[Schema.Learner.COL_DEFAULT_LANGUAGE_ID]);

            if (!pDRow.IsNull(Schema.Learner.COL_DEFAULT_THEME_ID))
                _entLearner.DefaultThemeID = Convert.ToString(pDRow[Schema.Learner.COL_DEFAULT_THEME_ID]);

            if (!pDRow.IsNull(Schema.Learner.COL_GENDER))
                _entLearner.Gender = Convert.ToBoolean(pDRow[Schema.Learner.COL_GENDER]);

            if (pDRow[Schema.Learner.COL_MANAGER_ID] != null)
                _entLearner.ManagerId = Convert.ToString(pDRow[Schema.Learner.COL_MANAGER_ID]);

            if (pDRow[Schema.Learner.COL_MANAGER_EMAIL] != null)
                _entLearner.ManagerEmailId = Convert.ToString(pDRow[Schema.Learner.COL_MANAGER_EMAIL]);

            if (pDRow.Table.Columns.Contains(Schema.Learner.COL_MANAGER_NAME))
            {
                if (pDRow[Schema.Learner.COL_MANAGER_NAME] != null)
                    _entLearner.ManagerName= Convert.ToString(pDRow[Schema.Learner.COL_MANAGER_NAME]);
            }

            if (!pDRow.IsNull(Schema.Learner.COL_DATE_LAST_LOGIN))
                _entLearner.DateLastLogin = Convert.ToDateTime(pDRow[Schema.Learner.COL_DATE_LAST_LOGIN]);

            if (!pDRow.IsNull(Schema.Learner.COL_IS_ACTIVE))
                _entLearner.IsActive = Convert.ToBoolean(pDRow[Schema.Learner.COL_IS_ACTIVE]);

            if (pDRow[Schema.Client.COL_CLIENT_ID] != null)
                _entLearner.ClientId = Convert.ToString(pDRow[Schema.Client.COL_CLIENT_ID]);

            if (pDRow[Schema.Common.COL_CREATED_BY] != null)
                _entLearner.CreatedById = Convert.ToString(pDRow[Schema.Common.COL_CREATED_BY]);

            if (!pDRow.IsNull(Schema.Common.COL_CREATED_ON))
                _entLearner.DateCreated = Convert.ToDateTime(pDRow[Schema.Common.COL_CREATED_ON]);

            if (pDRow[Schema.Common.COL_MODIFIED_BY] != null)
                _entLearner.LastModifiedById = Convert.ToString(pDRow[Schema.Common.COL_MODIFIED_BY]);

            if (!pDRow.IsNull(Schema.Common.COL_MODIFIED_ON))
                _entLearner.LastModifiedDate = Convert.ToDateTime(pDRow[Schema.Common.COL_MODIFIED_ON]);

            if (pDRow[Schema.Learner.COL_UNIT_ID] != null)
                _entLearner.UnitId = Convert.ToString(pDRow[Schema.Learner.COL_UNIT_ID]);

            if (pDRow[Schema.Learner.COL_LEVEL_ID] != null)
                _entLearner.LevelId = Convert.ToString(pDRow[Schema.Learner.COL_LEVEL_ID]);

            if (pDRow[Schema.Learner.COL_DEFAULT_RV] != null)
                _entLearner.DefaultRegionView = Convert.ToString(pDRow[Schema.Learner.COL_DEFAULT_RV]);

            if (pDRow[Schema.Learner.COL_CURRENT_RV] != null)
                _entLearner.CurrentRegionView = Convert.ToString(pDRow[Schema.Learner.COL_CURRENT_RV]);

            if (pDRow[Schema.Learner.COL_ILT_ROLE_COUNT] != null)
                _entLearner.ILTRoleCount = Convert.ToInt32(pDRow[Schema.Learner.COL_ILT_ROLE_COUNT]);

            if (string.IsNullOrEmpty(_entLearner.ManagerName))
                _entLearner.ManagerName = "";

            if (pDRow.Table.Columns.Contains(Schema.Learner.COL_IS_FIRST_LOGIN))
            {
                if (!pDRow.IsNull(Schema.Learner.COL_IS_FIRST_LOGIN))
                    _entLearner.IsActive = Convert.ToBoolean(pDRow[Schema.Learner.COL_IS_FIRST_LOGIN]);
            }
          
            if (pDRow.Table.Columns.Contains(Schema.Common.COL_TOTAL_COUNT))
            {
                if (!pDRow.IsNull(Schema.Common.COL_TOTAL_COUNT))
                    entRange.TotalRows = Convert.ToInt32(pDRow[Schema.Common.COL_TOTAL_COUNT]);
                _entLearner.ListRange = entRange;
            }
            return _entLearner;
        }

        /// <summary>
        /// Fill AdminRole object from a Row
        /// </summary>
        /// <param name="pDRow">DataRow</param>
        /// <returns>AdminRole</returns>
        private UserAdminRole FillUserAdminRole(DataRow pDRow)
        {
            entUserAdminRole = new UserAdminRole();
            entUserAdminRole.ID = Convert.ToString(pDRow[Schema.Learner.COL_USER_ID]);

            if (pDRow[Schema.AdminRole.COL_ROLE_ID] != null)
                entUserAdminRole.RoleId = Convert.ToString(pDRow[Schema.AdminRole.COL_ROLE_ID]);

            if (pDRow.Table.Columns.Contains(Schema.AdminRole.COL_ROLE_NAME))
            {
                if (pDRow[Schema.AdminRole.COL_ROLE_NAME] != null)
                    entUserAdminRole.RoleName = Convert.ToString(pDRow[Schema.AdminRole.COL_ROLE_NAME]);
            }
            if (pDRow.Table.Columns.Contains(Schema.AdminRole.COL_IS_ACTIVE))
            {
                if (!pDRow.IsNull(Schema.AdminRole.COL_IS_ACTIVE))
                    entUserAdminRole.IsRoleActive = Convert.ToBoolean(pDRow[Schema.AdminRole.COL_IS_ACTIVE]);
            }

            if (!pDRow.IsNull(Schema.Learner.COL_UNIT_ID))
                entUserAdminRole.UnitId = Convert.ToString(pDRow[Schema.Learner.COL_UNIT_ID]);

            if (!pDRow.IsNull(Schema.Learner.COL_LEVEL_ID))
                entUserAdminRole.LevelId = Convert.ToString(pDRow[Schema.Learner.COL_LEVEL_ID]);

            if (!pDRow.IsNull(Schema.CustomGroup.COL_CSG_ID))
                entUserAdminRole.CustomGroupId = Convert.ToString(pDRow[Schema.CustomGroup.COL_CSG_ID]);

            if (pDRow.Table.Columns.Contains(Schema.RuleRoleScope.COL_RULE_ID))
            {
                if (!pDRow.IsNull(Schema.RuleRoleScope.COL_RULE_ID))
                    entUserAdminRole.RuleId = Convert.ToString(pDRow[Schema.RuleRoleScope.COL_RULE_ID]);
            }
           
            if (pDRow.Table.Columns.Contains(Schema.AdminRole.COL_USER_SCOPE))
             {
                    if (!pDRow.IsNull(Schema.AdminRole.COL_USER_SCOPE))
                        entUserAdminRole.UserScope = Convert.ToString(pDRow[Schema.AdminRole.COL_USER_SCOPE]);
             }
           
            if (pDRow.Table.Columns.Contains(Schema.AdminRole.COL_ROLE_TYPE))
            {
                if (!pDRow.IsNull(Schema.AdminRole.COL_ROLE_TYPE))
                    entUserAdminRole.AdminRoleType = (RoleType)Enum.Parse(typeof(RoleType), Convert.ToString(pDRow[Schema.AdminRole.COL_ROLE_TYPE]));
            }
            return entUserAdminRole;
        }

        /// <summary>
        /// /// This method does not Update Custom Field Values
        /// </summary>
        /// <param name="pEntListLearner"></param>
        /// <returns>List bulk updated/added users</returns>
        private List<Learner> BulkUpdate(List<Learner> pEntListLearner)
        {
            List<Learner> entListLearner = new List<Learner>();
            SqlDataAdapter sqladapter = null;
            DataTable dtableLearner;
            //SqlDataAdapter sqlAssignmentAdaptor = null;
            //SqlCommand sqlcmd;
            SqlCommand sqlcmdUpdate;
            SqlCommand sqlcmdSelect;
            _dset = new DataSet();
            int iBatchSize = 0;
            string strUserIds = String.Empty;
            DataRow drow = null;
            DataRow dAssignmentTableRow = null;
            DataTable dAssignmentTable;
            _sqlObject = new SQLObject();
            try
            {
                foreach (Learner learner in pEntListLearner)
                {
                    if (String.IsNullOrEmpty(strUserIds))
                        strUserIds = learner.ID;
                    else strUserIds = strUserIds + "," + learner.ID;
                }
                _strConnString = _sqlObject.GetClientDBConnString(pEntListLearner[0].ClientId);
                _sqlcon = new SqlConnection(_strConnString);
                sqlcmdSelect = new SqlCommand(Schema.Learner.PROC_GET_SELECTED_USERS, _sqlcon);
                sqlcmdSelect.CommandType = CommandType.StoredProcedure;
                _sqlpara = new SqlParameter(Schema.Learner.PARA_MULTI_USER_IDS, strUserIds);
                sqlcmdSelect.Parameters.Add(_sqlpara);
                sqladapter = new SqlDataAdapter(sqlcmdSelect);
                sqladapter.Fill(_dset);
                dtableLearner = _dset.Tables[0];
                dtableLearner.Columns.Add(Schema.Common.COL_UPDATE_MODE);
                dtableLearner.Columns.Add(Schema.Learner.COL_PREFERREDTIMEZONE);
                dtableLearner.Columns.Add(Schema.Learner.COL_PREFERRED_DATE_FORMAT);
                dtableLearner.PrimaryKey = new DataColumn[] { dtableLearner.Columns[Schema.Learner.COL_USER_ID] };

                dAssignmentTable = new DataTable();
                dAssignmentTable.Columns.Add(Schema.Learner.COL_USER_ID);

                #region ListData To Table
                foreach (Learner learner in pEntListLearner)
                {
                    drow = dtableLearner.Rows.Find(learner.ID);

                    dAssignmentTableRow = dAssignmentTable.NewRow();
                    dAssignmentTableRow[Schema.Learner.COL_USER_ID] = learner.ID;

                    drow[Schema.Learner.COL_USER_NAME_ALIAS] = learner.UserNameAlias;

                    //Password Encryption
                    drow[Schema.Learner.COL_USER_PASSWORD] = EncryptionManager.Encrypt(learner.UserPassword);
                    drow[Schema.Learner.COL_FIRST_NAME] = learner.FirstName;
                    drow[Schema.Learner.COL_MIDDLE_NAME] = learner.MiddleName;
                    drow[Schema.Learner.COL_LAST_NAME] = learner.LastName;
                    drow[Schema.Learner.COL_ADDRESS] = learner.Address;
                    drow[Schema.Learner.COL_EMAIL_ID] = learner.EmailID;
                    drow[Schema.Learner.COL_PHONE_NO] = learner.PhoneNo;
                    drow[Schema.Learner.COL_PREFERRED_DATE_FORMAT] = learner.PreferredDateFormat;
                    drow[Schema.Learner.COL_PREFERREDTIMEZONE] = learner.PreferredTimeZone;

                    if (DateTime.MinValue.CompareTo(learner.DateOfBirth) < 0)
                        drow[Schema.Learner.COL_DATE_OF_BIRTH] = learner.DateOfBirth;

                    if (DateTime.MinValue.CompareTo(learner.DateOfRegistration) < 0)
                        drow[Schema.Learner.COL_DATE_OF_REGISTRATION] = learner.DateOfRegistration;
                    else
                        drow[Schema.Learner.COL_DATE_OF_REGISTRATION] = System.DBNull.Value;

                    if (DateTime.MinValue.CompareTo(learner.DateOfTermination) < 0)
                        drow[Schema.Learner.COL_DATE_OF_TERMINATION] = learner.DateOfTermination;
                    else
                        drow[Schema.Learner.COL_DATE_OF_TERMINATION] = System.DBNull.Value;

                    drow[Schema.Learner.COL_USER_TYPE_ID] = learner.UserTypeId;

                    drow[Schema.Learner.COL_DEFAULT_LANGUAGE_ID] = learner.DefaultLanguageId;

                    drow[Schema.Learner.COL_DEFAULT_THEME_ID] = learner.DefaultThemeID;

                    drow[Schema.Learner.COL_GENDER] = learner.Gender;

                    drow[Schema.Learner.COL_MANAGER_ID] = learner.ManagerId;
                    
                    drow[Schema.Learner.COL_MANAGER_NAME] = learner.ManagerName;

                    drow[Schema.Learner.COL_MANAGER_EMAIL] = learner.ManagerEmailId;

                    if (DateTime.MinValue.CompareTo(learner.DateLastLogin) < 0)
                        drow[Schema.Learner.COL_DATE_LAST_LOGIN] = learner.DateLastLogin;

                    drow[Schema.Learner.COL_IS_ACTIVE] = learner.IsActive;

                    if (!String.IsNullOrEmpty(learner.ClientId))
                        drow[Schema.Client.COL_CLIENT_ID] = learner.ClientId;

                    if (!String.IsNullOrEmpty(learner.CreatedById))
                        drow[Schema.Common.COL_CREATED_BY] = learner.CreatedById;

                    drow[Schema.Common.COL_MODIFIED_BY] = learner.LastModifiedById;

                    drow[Schema.Learner.COL_UNIT_ID] = learner.UnitId;

                    drow[Schema.Learner.COL_LEVEL_ID] = learner.LevelId;

                    drow[Schema.Learner.COL_CURRENT_RV] = learner.CurrentRegionView;

                    drow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_UPDATE_MODE;

                    learner.DateCreated = DateTime.Today.ToUniversalTime();
                    learner.LastModifiedDate = DateTime.Today.ToUniversalTime();
                    entListLearner.Add(learner);
                    iBatchSize++;
                    dAssignmentTable.Rows.Add(dAssignmentTableRow);
                }
                #endregion

                sqlcmdUpdate = new SqlCommand(Schema.Learner.PROC_UPDATE_USER, _sqlcon);
                sqlcmdUpdate.CommandType = CommandType.StoredProcedure;
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_USER_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_USER_ID);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_USER_NAME_ALIAS, SqlDbType.NVarChar, 100, Schema.Learner.COL_USER_NAME_ALIAS);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_USER_PASSWORD, SqlDbType.NVarChar, 100, Schema.Learner.COL_USER_PASSWORD);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_FIRST_NAME, SqlDbType.NVarChar, 100, Schema.Learner.COL_FIRST_NAME);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_MIDDLE_NAME, SqlDbType.NVarChar, 100, Schema.Learner.COL_MIDDLE_NAME);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_LAST_NAME, SqlDbType.NVarChar, 100, Schema.Learner.COL_LAST_NAME);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_ADDRESS, SqlDbType.NVarChar, 500, Schema.Learner.COL_ADDRESS);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_EMAIL_ID, SqlDbType.NVarChar, 100, Schema.Learner.COL_EMAIL_ID);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_PHONE_NO, SqlDbType.NVarChar, 100, Schema.Learner.COL_PHONE_NO);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_DATE_OF_BIRTH, SqlDbType.DateTime, 100, Schema.Learner.COL_DATE_OF_BIRTH);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_DATE_OF_REGISTRATION, SqlDbType.DateTime, 100, Schema.Learner.COL_DATE_OF_REGISTRATION);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_DATE_OF_TERMINATION, SqlDbType.DateTime, 100, Schema.Learner.COL_DATE_OF_TERMINATION);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_USER_TYPE_ID, SqlDbType.NVarChar, 100, Schema.Learner.COL_USER_TYPE_ID);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_DEFAULT_LANGUAGE_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_DEFAULT_LANGUAGE_ID);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_DEFAULT_THEME_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_DEFAULT_THEME_ID);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_GENDER, SqlDbType.Bit, 1, Schema.Learner.COL_GENDER);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_MANAGER_ID, SqlDbType.NVarChar, 100, Schema.Learner.COL_MANAGER_ID);
                //managerName para added,09-April-2010,fatte.
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_MANAGER_NAME, SqlDbType.NVarChar, 200, Schema.Learner.COL_MANAGER_NAME);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_MANAGER_EMAIL_ID, SqlDbType.NVarChar, 100, Schema.Learner.COL_MANAGER_EMAIL);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_DATE_LAST_LOGIN, SqlDbType.DateTime, 100, Schema.Learner.COL_DATE_LAST_LOGIN);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_IS_ACTIVE, SqlDbType.Bit, 1, Schema.Learner.COL_IS_ACTIVE);
                sqlcmdUpdate.Parameters.Add(Schema.Client.PARA_CLIENT_ID, SqlDbType.VarChar, 100, Schema.Client.COL_CLIENT_ID);
                sqlcmdUpdate.Parameters.Add(Schema.Common.PARA_CREATED_BY, SqlDbType.NVarChar, 100, Schema.Common.COL_CREATED_BY);
                sqlcmdUpdate.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.NVarChar, 100, Schema.Common.COL_MODIFIED_BY);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_UNIT_ID, SqlDbType.NVarChar, 50, Schema.Learner.COL_UNIT_ID);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_LEVEL_ID, SqlDbType.NVarChar, 50, Schema.Learner.COL_LEVEL_ID);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_CURRENT_RV, SqlDbType.VarChar, 100, Schema.Learner.COL_CURRENT_RV);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.PARA_PREFERREDDATE_FORMAT, SqlDbType.NVarChar, 100, Schema.Learner.COL_PREFERRED_DATE_FORMAT);
                sqlcmdUpdate.Parameters.Add(Schema.Learner.COL_PREFERREDTIMEZONE, SqlDbType.NVarChar, 150, Schema.Learner.COL_PREFERREDTIMEZONE);
                sqlcmdUpdate.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.NVarChar, 10, Schema.Common.COL_UPDATE_MODE);

                sqladapter.RowUpdating += new SqlRowUpdatingEventHandler(sqlDataAdapter_RowUpdating);
                iRecordsUpdated = 0;
                sqladapter.UpdateCommand = sqlcmdUpdate;
                sqladapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;

                sqladapter.UpdateBatchSize = iBatchSize;
                sqladapter.Update(dtableLearner);

                /* Commented on 07-May-2010 to reduce multiple call. by:fatte+shailesh*/
                /*sqlcmd = new SqlCommand();
                sqlcmd.CommandText = Schema.Learner.PROC_SET_USER_INITIAL_SETTINGS;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                sqlcmd.Connection = _sqlcon;
                sqlcmd.CommandTimeout = 0;
                sqlcmd.Parameters.Add(Schema.Learner.PARA_USER_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_USER_ID);
                sqlAssignmentAdaptor = new SqlDataAdapter();
                sqlAssignmentAdaptor.InsertCommand = sqlcmd;
                sqlAssignmentAdaptor.InsertCommand.CommandTimeout = 0;
                sqlAssignmentAdaptor.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                sqlAssignmentAdaptor.UpdateBatchSize = iBatchSize;
                sqlAssignmentAdaptor.Update(dAssignmentTable);
                 */  
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListLearner;
        }

        /// <summary>
        /// Record Updating Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void sqlDataAdapter_RowUpdating(object sender, SqlRowUpdatingEventArgs e)
        {
            //This event will be fired for each record.
            iRecordsUpdated++;
        }

        /// <summary>
        /// Delete User by ID
        /// </summary>
        /// <param name="pEntUser">User with ID</param>
        /// <returns>User with check status</returns>
        public Learner ChecknewPwd(Learner pEntUser)
        {
            SqlCommand sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            sqlcmd.CommandText = Schema.Learner.PROC_CHECK_NEW_PWD;
            sqlcmd.CommandType = CommandType.StoredProcedure;
            _strConnString = _sqlObject.GetClientDBConnString(pEntUser.ClientId);
            sqlcmd.Connection = new SqlConnection(_strConnString);
            if (!String.IsNullOrEmpty(pEntUser.ID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntUser.ID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
            sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntUser.UserNameAlias))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, pEntUser.UserNameAlias);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NAME_ALIAS, null);
            sqlcmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pEntUser.UserPassword))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NEW_PASSWORD, EncryptionManager.Encrypt(pEntUser.UserPassword));
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NEW_PASSWORD, null);
            sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntUser.ClientId);
                DataSet ds = _sqlObject.SqlDataAdapter(sqlcmd, _strConnString);
                if (ds.Tables.Count > 3)
                {
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        pEntUser.CanUpdate = Convert.ToBoolean(ds.Tables[3].Rows[0]["IsPasswordChangeAllowed"]);
                    }
                }
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return pEntUser;
        }

        /// <summary>
        /// Change Passowrd
        /// </summary>
        /// <param name="pEntUser">User with ID, New Password</param>
        /// <returns>User with status</returns>
        public Learner CheckSetNewPassword(Learner pEntUser, out string strMessage)
        {
            strMessage = string.Empty;

                SqlCommand sqlcmd = new SqlCommand();
                _sqlObject = new SQLObject();
                sqlcmd.CommandText = Schema.Learner.PORC_CHECKSETNEWPASSWORD;
                sqlcmd.CommandType = CommandType.StoredProcedure;
                _strConnString = _sqlObject.GetClientDBConnString(pEntUser.ClientId);
                sqlcmd.Connection = new SqlConnection(_strConnString);
                if (!String.IsNullOrEmpty(pEntUser.ID))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntUser.ID);
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
                sqlcmd.Parameters.Add(_sqlpara);

                if (!String.IsNullOrEmpty(pEntUser.UserPassword))
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NEW_PASSWORD, EncryptionManager.Encrypt(pEntUser.UserPassword));
                else
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_NEW_PASSWORD, null);
                sqlcmd.Parameters.Add(_sqlpara);
                try
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntUser.ClientId);
                    DataSet ds = _sqlObject.SqlDataAdapter(sqlcmd, _strConnString);
                    if (ds.Tables.Count > 3)
                    {
                        if (ds.Tables[3].Rows.Count > 0)
                        {
                            pEntUser.CanUpdate = Convert.ToBoolean(ds.Tables[3].Rows[0]["IsPasswordChangeAllowed"]);
                        }
                    }
                }
                catch (Exception expCommon)
                {
                  _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                    throw _expCustom;
                }


            return pEntUser;
        }

        /// <summary>
        /// Bulk Activate + deactivate 
        /// </summary>
        /// <param name="pEntListLearners"></param>
        /// <returns></returns>
        public List<Learner> BulkActivateDeactivate(List<Learner> pEntListLearners)
        {
            List<Learner> entListLearner = new List<Learner>();
            SqlDataAdapter sqladapter = null;
            DataTable dtableLearner;
            SqlCommand sqlcmdInsert;
            _sqlObject = new SQLObject();
            int iBatchSize = 0;
            DataRow drow = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntListLearners[0].ClientId);
                _sqlcon = new SqlConnection(_strConnString);

                dtableLearner = new DataTable();
                dtableLearner.Columns.Add(Schema.Learner.COL_USER_ID);
                dtableLearner.Columns.Add(Schema.Learner.COL_USER_PASSWORD);
                dtableLearner.Columns.Add(Schema.Learner.COL_IS_ACTIVE);
                dtableLearner.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                #region ListData To Table
                foreach (Learner entLearner in pEntListLearners)
                {
                    drow = dtableLearner.NewRow();
                    drow[Schema.Learner.COL_USER_ID] = entLearner.ID;

                    if (!String.IsNullOrEmpty(entLearner.UserPassword))
                        drow[Schema.Learner.COL_USER_PASSWORD] = EncryptionManager.Encrypt(entLearner.UserPassword);
                    if (pEntListLearners.Count == 1)
                        drow[Schema.Learner.COL_IS_ACTIVE] = entLearner.IsActive;
                    else
                        drow[Schema.Learner.COL_IS_ACTIVE] = entLearner.IsActive ? 1 : 0;

                    drow[Schema.Common.COL_MODIFIED_BY] = entLearner.LastModifiedById;
                    dtableLearner.Rows.Add(drow);

                    entListLearner.Add(entLearner);

                    iBatchSize++;
                }
                #endregion

                sqlcmdInsert = new SqlCommand(Schema.Learner.PROC_BULK_ACTIVATE_DEACTIVATE, _sqlcon);
                sqlcmdInsert.CommandType = CommandType.StoredProcedure;
                sqlcmdInsert.Parameters.Add(Schema.Learner.PARA_USER_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_USER_ID);
                sqlcmdInsert.Parameters.Add(Schema.Learner.PARA_USER_PASSWORD, SqlDbType.NVarChar, 100, Schema.Learner.COL_USER_PASSWORD);
                sqlcmdInsert.Parameters.Add(Schema.Learner.PARA_IS_ACTIVE, SqlDbType.Bit, 1, Schema.Learner.COL_IS_ACTIVE);
                sqlcmdInsert.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.NVarChar, 100, Schema.Common.COL_MODIFIED_BY);
                sqladapter = new SqlDataAdapter();
                iRecordsUpdated = 0;
                sqladapter.RowUpdating += new SqlRowUpdatingEventHandler(sqlDataAdapter_RowUpdating);
                sqladapter.InsertCommand = sqlcmdInsert;
                sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                sqlcmdInsert.CommandTimeout = 0; //added for timeout problem 12.01.2010
                sqladapter.InsertCommand.CommandTimeout = 0;

                if (iBatchSize > 10000)
                {
                    iBatchSize = iBatchSize / 4;
                }
                sqladapter.UpdateBatchSize = iBatchSize;

                sqladapter.Update(dtableLearner);
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, new Exception(iRecordsUpdated.ToString()), false, _strConnString);
                throw _expCustom;
            }
            return entListLearner;
        }

        /// <summary>
        /// Bulk Users Password Reset
        /// </summary>
        /// <param name="pEntListLearners"></param>
        /// <returns></returns>
        public List<Learner> BulkUsersPasswordReset(List<Learner> pEntListLearners)
        {
            List<Learner> entListLearner = new List<Learner>();
            SqlDataAdapter sqladapter = null;
            DataTable dtableLearner;
            SqlCommand sqlcmdInsert;
            _sqlObject = new SQLObject();
            int iBatchSize = 0;
            DataRow drow = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntListLearners[0].ClientId);
                _sqlcon = new SqlConnection(_strConnString);

                dtableLearner = new DataTable();
                dtableLearner.Columns.Add(Schema.Learner.COL_USER_ID);
                dtableLearner.Columns.Add(Schema.Learner.COL_USER_PASSWORD);
                dtableLearner.Columns.Add(Schema.Learner.COL_IS_ACTIVE);
                dtableLearner.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                #region ListData To Table
                foreach (Learner entLearner in pEntListLearners)
                {
                    drow = dtableLearner.NewRow();
                    drow[Schema.Learner.COL_USER_ID] = entLearner.ID;

                    if (!String.IsNullOrEmpty(entLearner.UserPassword))
                        drow[Schema.Learner.COL_USER_PASSWORD] = EncryptionManager.Encrypt(entLearner.UserPassword);

                    drow[Schema.Learner.COL_IS_ACTIVE] = entLearner.IsActive;
                    drow[Schema.Common.COL_MODIFIED_BY] = entLearner.LastModifiedById;
                    dtableLearner.Rows.Add(drow);

                    entListLearner.Add(entLearner);

                    iBatchSize++;
                }
                #endregion

                sqlcmdInsert = new SqlCommand(Schema.Learner.PROC_BULK_RESET_PASSWORD, _sqlcon);
                sqlcmdInsert.CommandType = CommandType.StoredProcedure;
                sqlcmdInsert.Parameters.Add(Schema.Learner.PARA_USER_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_USER_ID);
                sqlcmdInsert.Parameters.Add(Schema.Learner.PARA_USER_PASSWORD, SqlDbType.NVarChar, 100, Schema.Learner.COL_USER_PASSWORD);
                sqlcmdInsert.Parameters.Add(Schema.Learner.PARA_IS_ACTIVE, SqlDbType.Bit, 1, Schema.Learner.COL_IS_ACTIVE);
                sqlcmdInsert.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.NVarChar, 100, Schema.Common.COL_MODIFIED_BY);
                sqladapter = new SqlDataAdapter();
                iRecordsUpdated = 0;
                sqladapter.RowUpdating += new SqlRowUpdatingEventHandler(sqlDataAdapter_RowUpdating);
                sqladapter.InsertCommand = sqlcmdInsert;
                sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                sqlcmdInsert.CommandTimeout = 0; //added for timeout problem 12.01.2010
                sqladapter.InsertCommand.CommandTimeout = 0;

                if (iBatchSize > 10000)
                {
                    iBatchSize = iBatchSize / 4;
                }
                sqladapter.UpdateBatchSize = iBatchSize;

                sqladapter.Update(dtableLearner);
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, new Exception(iRecordsUpdated.ToString()), false);
                throw _expCustom;
            }
            return entListLearner;
        }

        /// <summary>
        /// Bulk Change Id
        /// </summary>
        /// <param name="pEntListLearners"></param>
        /// <returns></returns>
        public List<Learner> BulkChangeId(List<Learner> pEntListLearners)
        {
            List<Learner> entListLearner = new List<Learner>();
            SqlDataAdapter sqladapter = null;
            DataTable dtableLearner;
            SqlCommand sqlcmdInsert;
            _sqlObject = new SQLObject();
            int iBatchSize = 0;
            DataRow drow = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntListLearners[0].ClientId);
                _sqlcon = new SqlConnection(_strConnString);

                dtableLearner = new DataTable();
                dtableLearner.Columns.Add(Schema.Learner.COL_USER_ID);
                dtableLearner.Columns.Add(Schema.Learner.COL_USER_NAME_ALIAS);
                dtableLearner.Columns.Add(Schema.Learner.COL_USER_PASSWORD);
                dtableLearner.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                #region ListData To Table
                foreach (Learner entLearner in pEntListLearners)
                {
                    drow = dtableLearner.NewRow();
                    drow[Schema.Learner.COL_USER_ID] = entLearner.ID;

                    if (!String.IsNullOrEmpty(entLearner.UserNameAlias))
                        drow[Schema.Learner.COL_USER_NAME_ALIAS] = entLearner.UserNameAlias;

                    if (!String.IsNullOrEmpty(entLearner.UserPassword))
                        drow[Schema.Learner.COL_USER_PASSWORD] = EncryptionManager.Encrypt(entLearner.UserPassword);
                    drow[Schema.Common.COL_MODIFIED_BY] = entLearner.LastModifiedById;

                    dtableLearner.Rows.Add(drow);
                    entListLearner.Add(entLearner);
                    iBatchSize++;
                }
                #endregion

                sqlcmdInsert = new SqlCommand(Schema.Learner.PROC_BULK_UPDATE_CHANGE_ID, _sqlcon);
                sqlcmdInsert.CommandType = CommandType.StoredProcedure;
                sqlcmdInsert.Parameters.Add(Schema.Learner.PARA_USER_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_USER_ID);
                sqlcmdInsert.Parameters.Add(Schema.Learner.PARA_USER_NAME_ALIAS, SqlDbType.NVarChar, 100, Schema.Learner.COL_USER_NAME_ALIAS);
                sqlcmdInsert.Parameters.Add(Schema.Learner.PARA_USER_PASSWORD, SqlDbType.NVarChar, 100, Schema.Learner.COL_USER_PASSWORD);
                sqlcmdInsert.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.NVarChar, 100, Schema.Common.COL_MODIFIED_BY);

                sqladapter = new SqlDataAdapter();
                iRecordsUpdated = 0;
                sqladapter.RowUpdating += new SqlRowUpdatingEventHandler(sqlDataAdapter_RowUpdating);
                sqladapter.InsertCommand = sqlcmdInsert;
                sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                sqladapter.UpdateBatchSize = iBatchSize;
                sqladapter.Update(dtableLearner);
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, new Exception(iRecordsUpdated.ToString()), false);
                throw _expCustom;
            }
            return entListLearner;
        }

        /// <summary>
        /// Find Learners For All Role Assignment
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <param name="pIsIn"></param>
        /// <returns></returns>
        public List<Learner> FindLearnersForAllRoleAssignment(Search pEntSearch, bool pIsIn)
        {
            Learner pEntLearner = null;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _entListLearner = new List<Learner>();
            DataTable dtableLearner;
            DataTable dtableRole;
            _dset = new DataSet();
            try
            {
                if (!string.IsNullOrEmpty(pEntSearch.ClientId))
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);

                    _sqlcmd.CommandText = Schema.Learner.PROC_FIND_USERS_FOR_ALL_ROLE_ASSIGNMENT;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;

                    if (!String.IsNullOrEmpty(pEntSearch.KeyWord))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntSearch.KeyWord);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

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
                        {
                            _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }

                        //For Scope
                        _sqlpara = new SqlParameter(Schema.Learner.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }
                    if (pEntSearch.SearchObject != null && pEntSearch.SearchObject.Count > 0)
                    {
                        if (pEntSearch.SearchObject[0] is UserAdminRole)
                        {
                            entUserAdminRole = new UserAdminRole();
                            entUserAdminRole = (UserAdminRole)pEntSearch.SearchObject[0];
                            _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, entUserAdminRole.RoleId);
                            _sqlcmd.Parameters.Add(_sqlpara);
                            _sqlpara = new SqlParameter(Schema.Common.PARA_QUERY_TYPE, pIsIn);
                            _sqlcmd.Parameters.Add(_sqlpara);
                        }
                        if (pEntSearch.SearchObject[0] is Learner)
                        {

                            pEntLearner = new Learner();
                            pEntLearner = (Learner)pEntSearch.SearchObject[0];
                            _sqlpara = new SqlParameter(Schema.Learner.PARA_IS_ACTIVE, pEntLearner.IsActive);
                            _sqlcmd.Parameters.Add(_sqlpara);

                            if (pEntLearner.UserAdminRole.Count > 0)
                            {
                                entUserAdminRole = new UserAdminRole();
                                entUserAdminRole = (UserAdminRole)pEntLearner.UserAdminRole[0];
                                _sqlpara = new SqlParameter(Schema.AdminRole.PARA_ROLE_ID, entUserAdminRole.RoleId);
                                _sqlcmd.Parameters.Add(_sqlpara);
                                _sqlpara = new SqlParameter(Schema.Common.PARA_QUERY_TYPE, pIsIn);
                                _sqlcmd.Parameters.Add(_sqlpara);
                            }
                        }
                    }
                    _dset = _sqlObject.SqlDataAdapter(_sqlcmd, _strConnString);
                    dtableLearner = _dset.Tables[0];
                    dtableRole = _dset.Tables[1];
                    foreach (DataRow drow in dtableLearner.Rows)
                    {
                        _entLearner = FillLearner(drow);
                        foreach (DataRow drowRole in dtableRole.Select(Schema.Learner.COL_USER_ID + "='" + _entLearner.ID + "'"))
                        {
                            _entLearner.UserAdminRole.Add(FillUserAdminRole(drowRole));
                        }
                        _entListLearner.Add(_entLearner);
                    }
                }
            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return _entListLearner;
        }

        /// <summary>
        /// Get User List For Dynamic Assinment
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns></returns>
        public List<Learner> GetDynamicUserList(Learner pEntLearner)
        {
            _sqlObject = new SQLObject();
            List<Learner> entListLearner = new List<Learner>();
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.Connection = sqlConnection;

                _sqlcmd.CommandText = Schema.Assignment.PROC_DYNAMIC_USERLIST;
                if (!String.IsNullOrEmpty(pEntLearner.ParaActivityID))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntLearner.ParaActivityID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntLearner.ParaRuleID))
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_RULE_ID, pEntLearner.ParaRuleID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_RULE_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entLearner = FillObjectDynamicOneTime(_sqlreader, _sqlObject, pEntLearner.ClientId);
                    _entLearner.ParaActivityID = pEntLearner.ParaActivityID;
                    entListLearner.Add(_entLearner);
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
            return entListLearner;
        }

        /// <summary>
        /// Get User List For One Time Assinment
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns></returns>
        public List<Learner> GetOneTimeUserList(Learner pEntLearner)
        {
            _sqlObject = new SQLObject();
            List<Learner> entListLearner = new List<Learner>();
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.Connection = sqlConnection;

                _sqlcmd.CommandText = Schema.Assignment.PROC_ONETIME_USERLIST;
                if (!String.IsNullOrEmpty(pEntLearner.ParaActivityID))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntLearner.ParaActivityID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!String.IsNullOrEmpty(pEntLearner.ParaRuleID))
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_RULE_ID, pEntLearner.ParaRuleID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_RULE_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntLearner.ID))
                {
                    _sqlpara = new SqlParameter(Schema.UserPolicyTracking.PARA_SYSTEM_USER_GUID, pEntLearner.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.UserPolicyTracking.PARA_SYSTEM_USER_GUID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntLearner.IsReassign))
                {
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_ISREASSIGN, pEntLearner.IsReassign);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_ISREASSIGN, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entLearner = FillObjectDynamicOneTime(_sqlreader, _sqlObject, pEntLearner.ClientId);
                    _entLearner.ParaActivityID = pEntLearner.ParaActivityID;//for activityID
                    entListLearner.Add(_entLearner);
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
            return entListLearner;
        }

        /// <summary>
        /// Fill Object by Reader
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <returns></returns>
        private Learner FillObjectDynamicOneTime(SqlDataReader pSqlreader, SQLObject pSqlObject, string strClientID)
        {
            _entLearner = new Learner();
            int iIndex;
            if (pSqlreader.HasRows)
            {

                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.UserPolicyTracking.COL_SYSTEM_USER_GUID))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.UserPolicyTracking.COL_SYSTEM_USER_GUID);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entLearner.ID = pSqlreader.GetString(iIndex);
                }
                _entLearner.ClientId = strClientID;
                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Learner.COL_EMAIL_ID))
                {

                    iIndex = pSqlreader.GetOrdinal(Schema.Learner.COL_EMAIL_ID);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entLearner.EmailID = pSqlreader.GetString(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_ACTIVITY_ID))
                 {
                        iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_ID);
                        if (!pSqlreader.IsDBNull(iIndex))
                            _entLearner.ParaActivityID = pSqlreader.GetString(iIndex);
                  }
                
                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Learner.COL_FIRST_NAME))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Learner.COL_FIRST_NAME);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entLearner.FirstName = pSqlreader.GetString(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Learner.COL_LAST_NAME))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Learner.COL_LAST_NAME);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entLearner.LastName = pSqlreader.GetString(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Learner.COL_MIDDLE_NAME))
                {

                    iIndex = pSqlreader.GetOrdinal(Schema.Learner.COL_MIDDLE_NAME);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entLearner.MiddleName = pSqlreader.GetString(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Learner.COL_USER_NAME_ALIAS))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Learner.COL_USER_NAME_ALIAS);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entLearner.UserNameAlias = pSqlreader.GetString(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Learner.COL_USER_PASSWORD))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Learner.COL_USER_PASSWORD);
                    if (!pSqlreader.IsDBNull(iIndex))
                    {
                        _entLearner.UserPassword = pSqlreader.GetString(iIndex);
                        _entLearner.UserPassword = EncryptionManager.Decrypt(_entLearner.UserPassword);
                    }
                }

                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Learner.COL_PREFERRED_DATE_FORMAT))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Learner.COL_PREFERRED_DATE_FORMAT);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entLearner.PreferredDateFormat = pSqlreader.GetString(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Learner.COL_PREFERREDTIMEZONE))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Learner.COL_PREFERREDTIMEZONE);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entLearner.PreferredTimeZone = pSqlreader.GetString(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Learner.COL_ActivityId))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Learner.COL_ActivityId);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entLearner.ActivityId = pSqlreader.GetString(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Learner.COL_DEFAULT_LANGUAGE_ID))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Learner.COL_DEFAULT_LANGUAGE_ID);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entLearner.DefaultLanguageId = pSqlreader.GetString(iIndex);
                }
            }
            return _entLearner;
        }
        
        /// <summary>
        /// Get User List For  Bulk Import 
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns></returns>
        public List<Learner> GetBulkImport(Learner pEntLearner)
        {
            _sqlObject = new SQLObject();
            List<Learner> entListLearner = new List<Learner>();
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.Connection = sqlConnection;

                _sqlcmd.CommandText = Schema.ActivityAssignment.PROC_BULK_IMPORT_UNASSIGNMENT_ONE_BY_ONE;
                if (!String.IsNullOrEmpty(pEntLearner.ParaActivityID))
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, pEntLearner.ParaActivityID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.ActivityAssignment.PARA_ACTIVITY_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntLearner.ParaRuleID))
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_RULE_ID, pEntLearner.ParaRuleID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Assignment.PARA_RULE_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntLearner.ID))
                {
                    _sqlpara = new SqlParameter(Schema.UserPolicyTracking.PARA_SYSTEM_USER_GUID, pEntLearner.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.UserPolicyTracking.PARA_SYSTEM_USER_GUID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entLearner = FillBulkImport(_sqlreader);
                    entListLearner.Add(_entLearner);
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
            return entListLearner;
        }

        /// <summary>
        /// Fill Bulk Import
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <returns></returns>
        private Learner FillBulkImport(SqlDataReader pSqlreader)
        {
            _entLearner = new Learner();
            int iIndex;
            if (pSqlreader.HasRows)
            {

              
                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Learner.COL_EMAIL_ID))
                {

                    iIndex = pSqlreader.GetOrdinal(Schema.Learner.COL_EMAIL_ID);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entLearner.EmailID = pSqlreader.GetString(iIndex);
                }
               
                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.ActivityAssignment.COL_ACTIVITY_ID))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.ActivityAssignment.COL_ACTIVITY_ID);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entLearner.ParaActivityID = pSqlreader.GetString(iIndex);
                }
               
                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Learner.COL_FIRST_NAME))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Learner.COL_FIRST_NAME);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entLearner.FirstName = pSqlreader.GetString(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Learner.COL_LAST_NAME))
                {
                    iIndex = pSqlreader.GetOrdinal(Schema.Learner.COL_LAST_NAME);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entLearner.LastName = pSqlreader.GetString(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Learner.COL_MIDDLE_NAME))
                {

                    iIndex = pSqlreader.GetOrdinal(Schema.Learner.COL_MIDDLE_NAME);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entLearner.MiddleName = pSqlreader.GetString(iIndex);
                }

                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Learner.COL_USER_ID))
                {

                    iIndex = pSqlreader.GetOrdinal(Schema.Learner.COL_USER_ID);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entLearner.ID = pSqlreader.GetString(iIndex);
                }
                if (SQLHelper.ReaderHasColumn(pSqlreader, Schema.Learner.COL_DEFAULT_LANGUAGE_ID))
                {

                    iIndex = pSqlreader.GetOrdinal(Schema.Learner.COL_DEFAULT_LANGUAGE_ID);
                    if (!pSqlreader.IsDBNull(iIndex))
                        _entLearner.DefaultLanguageId = pSqlreader.GetString(iIndex);
                }
            }
            return _entLearner;
        }

        public Learner GetUserScope(Learner pEntLearner)
        {
            SqlConnection sqlConnection = null;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_GET_USER_SCOPE;
            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.ID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                _sqlreader.Read();
                _entLearner = FillUserScopeObject(_sqlreader);
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
            return _entLearner;
        }

        /// <summary>
        /// This method does not Fill Child List
        /// </summary>
        /// <param name="pSqlReader"></param>
        /// <returns>Learner</returns>
        public Learner FillUserScopeObject(SqlDataReader pSqlReader)
        {
            _entLearner = new Learner();
            int index;
            if (pSqlReader.HasRows)
            {
                index = pSqlReader.GetOrdinal(Schema.Learner.COL_USER_SCOPE);
                if (!pSqlReader.IsDBNull(index))
                    _entLearner.UserScope = pSqlReader.GetString(index);
            }

            return _entLearner;
        }

        /// <summary>
        /// Get All Users who have been assigned Assessment Course
        /// </summary>
        /// <param name="pEntLearner">Learner object with ClientId & ActivityId as ContentModuleId</param>
        /// <returns></returns>
        public List<Learner> GetLearnersForAssessmentCourse(Learner pEntLearner)
        {
            _sqlObject = new SQLObject();
            List<Learner> entListLearner = new List<Learner>();
            SqlConnection sqlConnection = null;
            _sqlreader = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand();
                _sqlcmd.Connection = sqlConnection;

                _sqlcmd.CommandText = Schema.Assignment.PROC_GET_ALL_USER_ASSESSMENT_COURSE;
                if (!String.IsNullOrEmpty(pEntLearner.ParaActivityID))
                {
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_CONTENT_MODULE_ID, pEntLearner.ParaActivityID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Learner.PARA_CONTENT_MODULE_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntLearner.ClientId))
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CLIENT_ID, pEntLearner.ClientId);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                else
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_CLIENT_ID, System.DBNull.Value);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                _dset = _sqlObject.SqlDataAdapter(_sqlcmd, _strConnString);
                if (_dset != null && _dset.Tables != null && _dset.Tables[0] != null && _dset.Tables.Count > 0)
                {
                    DataTable dtableLearner = _dset.Tables[0];
                    foreach (DataRow drow in dtableLearner.Rows)
                    {
                        _entLearner = FillLearner(drow);
                        entListLearner.Add(_entLearner);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return entListLearner;
        }

        /// <summary>
        /// Find Learners For UnlockAssignment 
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<Learner> FindLearnersForUnlockAssignment(Search pEntSearch)
        {
            Learner entLearner = new Learner();
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _entListLearner = new List<Learner>();
            SqlConnection sqlConnection = null;
            try
            {
                if (!string.IsNullOrEmpty(pEntSearch.ClientId))
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                    sqlConnection = new SqlConnection(_strConnString);
                    if (pEntSearch.LockType == "0")
                        _sqlcmd.CommandText = Schema.Learner.PROC_SEARCH_LEARNERS_FOR_UNLOCKASSIGNMENTS;
                    else if (pEntSearch.LockType == "1")
                        _sqlcmd.CommandText = Schema.Learner.PROC_SEARCH_LEARNERS_FOR_ATTEMPTUNLOCKASSIGNMENTS;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;

                    if (!String.IsNullOrEmpty(pEntSearch.KeyWord))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntSearch.KeyWord);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, null);

                    _sqlcmd.Parameters.Add(_sqlpara);


                    if (!String.IsNullOrEmpty(pEntSearch.ManagerName))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_MANAGER_NAME, pEntSearch.ManagerName);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_MANAGER_NAME, null);

                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntSearch.UserCriteria != null)
                    {
                        _sqlpara = new SqlParameter("UserSearchCriteria", SqlDbType.Xml)
                        { Value = SQLHelper.FormatUserSearchCriteria(pEntSearch.UserCriteria) };
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

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
                    _sqlcmd.Connection = sqlConnection;
                    _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                    while (_sqlreader.Read())
                    {
                        _entLearner = FillUserObject(_sqlreader, true, _sqlObject);
                        _entListLearner.Add(_entLearner);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return _entListLearner;
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns></returns>
        public Learner Get(Learner pEntLearner)
        {
            return GetUserByID(pEntLearner);
        }
        /// <summary>
        /// Update Learner
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns></returns>
        public Learner Update(Learner pEntLearner)
        {
            return UpdateUser(pEntLearner,false);
        }
        #endregion

         /// <summary>
        /// Find Learners for Assignment
        /// </summary>
        /// <param name="pEntSearch"></param>
        /// <returns></returns>
        public List<Learner> FindLearnersForSession(Search pEntSearch)
        {
            Learner entLearner = new Learner(); ;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _entListLearner = new List<Learner>();
            SqlConnection sqlConnection = null;
            try
            {
                if (!string.IsNullOrEmpty(pEntSearch.ClientId))
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                    sqlConnection = new SqlConnection(_strConnString);
                    _sqlcmd.CommandText = Schema.Learner.PROC_SEARCH_LEARNERS_FOR_SESSIONID;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;

                    if (!String.IsNullOrEmpty(pEntSearch.KeyWord))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, pEntSearch.KeyWord);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_KEY_WORD, null);

                    //if (!String.IsNullOrEmpty(pEntSearch.SessionId))
                    //    _sqlpara = new SqlParameter(Schema.Learner.PARA_SESSION_ID, pEntSearch.SessionId);
                    //else
                    //    _sqlpara = new SqlParameter(Schema.Learner.PARA_SESSION_ID, null);

                    if (!String.IsNullOrEmpty(pEntSearch.ProgramId))
                        _sqlpara = new SqlParameter(Schema.Learner.PARA_PROGRAM_ID, pEntSearch.ProgramId);
                    else
                        _sqlpara = new SqlParameter(Schema.Learner.PARA_PROGRAM_ID, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    //Add By Kunal
                    if (!String.IsNullOrEmpty(pEntSearch.SystemUserGUID))
                        _sqlpara = new SqlParameter(Schema.UserSessionRegistration.PARA_SYSTEMUSERGUID, pEntSearch.SystemUserGUID.ToString());
                    else
                        _sqlpara = new SqlParameter(Schema.UserSessionRegistration.PARA_SYSTEMUSERGUID, null);
                    _sqlcmd.Parameters.Add(_sqlpara);
                    //End


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

                    if (pEntSearch.SearchObject.Count > 0)
                    {
                        entLearner = (Learner)pEntSearch.SearchObject[0];
                        _sqlpara = new SqlParameter(Schema.Common.PARA_IS_ACTIVE, entLearner.IsActive);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    _sqlcmd.Connection = sqlConnection;
                    _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                    while (_sqlreader.Read())
                    {
                        _entLearner = FillUserObject(_sqlreader, true, _sqlObject);
                        _entListLearner.Add(_entLearner);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return _entListLearner;
        }


        /// <summary>
        /// To update user information
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <returns>Learner object</returns>
        public Learner AddSubscriptionOfUserForNewsLetter(Learner pEntLearner)
        {
            int iRowsAffected = 0;
            SqlCommand sqlcmd = new SqlCommand();
            SqlConnection sqlConn = null;
            string strCustomFields = string.Empty;
            _sqlcmd = new SqlCommand();
            _sqlcmd.CommandText = Schema.Learner.PROC_Add_SubscriptionOfUser_ForNewsLetter;

            _sqlObject = new SQLObject();
            if (!String.IsNullOrEmpty(pEntLearner.ID))
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, pEntLearner.ID);
            else
                _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, null);
            _sqlcmd.Parameters.Add(_sqlpara);


            _sqlpara = new SqlParameter(Schema.Learner.PARA_IS_USER_SIGNUP, pEntLearner.IsSignUpUser);

            _sqlcmd.Parameters.Add(_sqlpara);


            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);

                sqlConn = new SqlConnection(_strConnString);
                sqlConn.Open();

                _sqlcmd.Connection = sqlConn;
                iRowsAffected = _sqlObject.ExecuteNonQueryCount(_sqlcmd);

            }
            catch (Exception expCommon)
            {
              _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (sqlConn.State != ConnectionState.Closed)
                    sqlConn.Close();
            }
            return pEntLearner;
        }
        public List<Learner> SearchRegionalAdmins(Search pEntSearch)
        {
            //Learner pEntLearner = null;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _entListLearner = new List<Learner>();
            SqlConnection sqlConnection = null;
            try
            {
                if (!string.IsNullOrEmpty(pEntSearch.ClientId))
                {
                    _strConnString = _sqlObject.GetClientDBConnString(pEntSearch.ClientId);
                    sqlConnection = new SqlConnection(_strConnString);
                    _sqlcmd.CommandText = Schema.Learner.PROC_SEARCH_REGIONAL_ADMINS;
                    _sqlcmd.CommandType = CommandType.StoredProcedure;

                    if (pEntSearch.ListRange != null)
                    {
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntSearch.ListRange.PageIndex);
                        _sqlcmd.Parameters.Add(_sqlpara);
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntSearch.ListRange.PageSize);
                        _sqlcmd.Parameters.Add(_sqlpara);
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntSearch.ListRange.SortExpression);
                        _sqlcmd.Parameters.Add(_sqlpara);

                        //For Scope
                        _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntSearch.ListRange.RequestedById);
                        _sqlcmd.Parameters.Add(_sqlpara);
                    }

                    _sqlcmd.Connection = sqlConnection;
                    _sqlcmd.CommandTimeout = 0;
                    _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                    while (_sqlreader.Read())
                    {
                        _entLearner = FillUserObject(_sqlreader, true, _sqlObject);
                        _entListLearner.Add(_entLearner);
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
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
            }
            return _entListLearner;
        }

        public List<Learner> GetAllUserSubscribeForNewsLetter(Learner pEntLearner)
        {
            Learner entLearner = new Learner(); ;
            _sqlcmd = new SqlCommand();
            _sqlObject = new SQLObject();
            _entListLearner = new List<Learner>();
            SqlConnection sqlConnection = null;
            try
            {

                _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd.CommandText = Schema.Learner.PROC_GETUSERS_Subscribe_ForNewsLetter;
                _sqlcmd.CommandType = CommandType.StoredProcedure;

                if (!String.IsNullOrEmpty(pEntLearner.ID))
                    _sqlpara = new SqlParameter(Schema.UserSessionRegistration.PARA_SYSTEMUSERGUID, pEntLearner.ID);
                else
                    _sqlpara = new SqlParameter(Schema.UserSessionRegistration.PARA_SYSTEMUSERGUID, null);
                _sqlcmd.Parameters.Add(_sqlpara);
                //End


                if (pEntLearner.ListRange != null)
                {
                    if (pEntLearner.ListRange.PageIndex > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntLearner.ListRange.PageIndex);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (pEntLearner.ListRange.PageSize > 0)
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntLearner.ListRange.PageSize);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    if (!string.IsNullOrEmpty(pEntLearner.ListRange.SortExpression))
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntLearner.ListRange.SortExpression);
                    else
                        _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, null);
                    _sqlcmd.Parameters.Add(_sqlpara);

                }

                _sqlcmd.Connection = sqlConnection;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);

                while (_sqlreader.Read())
                {
                    _entLearner = FillUserObject(_sqlreader, false, _sqlObject);
                    _entListLearner.Add(_entLearner);
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
            return _entListLearner;
        }

        public bool UpdateUserCustomField(Learner objLearner)
        {
            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(objLearner.ClientId);
          return  UpdateUserCFValue(objLearner.UserCustomFieldValue, false);
        }

        public bool IsLoginIdExists(Learner pEntLearner)
        {
            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
            using (SqlConnection conn = new SqlConnection(_strConnString))
            {
                SqlCommand cmd = new SqlCommand("CheckLoginIdExists", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserNameAlias", pEntLearner.UserNameAlias);

                conn.Open();

                // ExecuteScalar will return the first column of the first row.
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        public bool CheckEmailIdExists(Learner pEntLearner)
        {
            _sqlObject = new SQLObject();
            _strConnString = _sqlObject.GetClientDBConnString(pEntLearner.ClientId);
            using (SqlConnection conn = new SqlConnection(_strConnString))
            {
                SqlCommand cmd = new SqlCommand("CheckEmailIdExists", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailId", pEntLearner.EmailID);

                conn.Open();

                // ExecuteScalar will return the first column of the first row.
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

    }
}