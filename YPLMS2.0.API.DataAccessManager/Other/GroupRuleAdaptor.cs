using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    public class GroupRuleAdaptor : IDataManager<GroupRule>, IGroupRuleAdaptor<GroupRule>
    {
        #region Global Declaration
        string _strConnString = string.Empty;
        string _strMessageId = YPLMS.Services.Messages.GroupRule.RULE_ERROR;
        CustomException _expCustom;
        SqlCommand _sqlcmd = null;
        SqlParameter _sqlpara;
        SqlDataReader _sqlreader = null;
        SqlDataAdapter _sqladapter = null;
        GroupRule _entGroupRule = null;
        List<GroupRule> _entListRule = null;
        List<BaseEntity> _entListBase = null;
        SqlConnection _sqlcon = null;
        DataTable _dtable = null;
        SqlTransaction _sqlTransaction = null;
        EntityRange _entRange = null;
        SQLObject _sqlObject = null;
        #endregion

        /// <summary>
        /// Get Rule by Name
        /// </summary>
        /// <param name="pEntGroupRule"></param>
        /// <returns>GroupRule</returns>
        public GroupRule GetRuleByName(GroupRule pEntGroupRule)
        {
            _sqlObject = new SQLObject();
            GroupRule entGrpRuleReturn = null;
            entGrpRuleReturn = new GroupRule();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntGroupRule.ClientId);
                _sqlcon = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.GroupRule.PROC_GET_RULE_BY_NAME, _sqlcon);

                _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_NAME, pEntGroupRule.RuleName);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    entGrpRuleReturn = FillObject(_sqlreader, _sqlObject);
                }
                else
                {
                    entGrpRuleReturn = null;
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
                if (_sqlcon != null && _sqlcon.State != ConnectionState.Closed)
                    _sqlcon.Close();
            }
            return entGrpRuleReturn;
        }
        /// <summary>
        /// Get Rule by Name
        /// </summary>
        /// <param name="pEntGroupRule"></param>
        /// <returns>GroupRule</returns>
        public GroupRule GetRuleIdByName(GroupRule pEntGroupRule)
        {
            _sqlObject = new SQLObject();
            GroupRule entGrpRuleReturn = new GroupRule();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntGroupRule.ClientId);
                _sqlcon = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.GroupRule.PROC_GET_RULE_ID_BY_NAME, _sqlcon);
                _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_NAME, pEntGroupRule.RuleName);
                _sqlcmd.Parameters.Add(_sqlpara);
                Object obj = null;

                // updated by Gitanjali 09.08.2010 , added comment to if condition 
                //if (obj != null)
                //{
                //    obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
                //}

                obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
                entGrpRuleReturn.ID = Convert.ToString(obj);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            finally
            {
                if (_sqlcon != null && _sqlcon.State != ConnectionState.Closed)
                    _sqlcon.Close();
            }
            return entGrpRuleReturn;
        }

        /// <summary>
        /// Edit Rule
        /// </summary>
        /// <param name="pEntGroupRule"></param>
        /// <returns></returns>
        public GroupRule EditRule(GroupRule pEntGroupRule)
        {
            _entGroupRule = new GroupRule();
            _entListRule = new List<GroupRule>();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntGroupRule.ClientId);
                _entListRule.Add(pEntGroupRule);
                _entListRule = BulkUpdate(_entListRule);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            return _entListRule[0];
        }

        public GroupRule GetRuleIdByParentId(GroupRule pEntGroupRule)
        {
            _sqlObject = new SQLObject();
            GroupRule entGrpRuleReturn = new GroupRule();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntGroupRule.ClientId);
                _sqlcon = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.GroupRule.PROC_GET_RULE_BY_RULE_PARENTID, _sqlcon);
                _sqlpara = new SqlParameter(Schema.GroupRule.COL_RULE_PARENT_ID, pEntGroupRule.RuleParentId);
                _sqlcmd.Parameters.Add(_sqlpara);
                Object obj = null;

                // updated by Gitanjali 09.08.2010 , added comment to if condition 
                //if (obj != null)
                //{
                //    obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
                //}

                obj = _sqlObject.ExecuteScalar(_sqlcmd, false);
                entGrpRuleReturn.ID = Convert.ToString(obj);
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            finally
            {
                if (_sqlcon != null && _sqlcon.State != ConnectionState.Closed)
                    _sqlcon.Close();
            }
            return entGrpRuleReturn;
        }

        /// <summary>
        /// Add Update
        /// </summary>
        /// <param name="pEntGroupRule"></param>
        /// <param name="strMode"></param>
        /// <returns></returns>
        private GroupRule AddUpdate(GroupRule pEntGroupRule, string strMode)
        {
            _sqlObject = new SQLObject();
            GroupRule entGrpRuleReturn = new GroupRule();
            _entGroupRule = new GroupRule();
            _entListRule = new List<GroupRule>();
            _entListBase = new List<BaseEntity>();

            string strDataBaseName = string.Empty;
            _strConnString = _sqlObject.GetClientDBConnString(pEntGroupRule.ClientId);

            SqlCommand sqlcmdMaster = new SqlCommand();

            SqlCommand cmdGroup = new SqlCommand();
            cmdGroup.CommandText = Schema.RuleParameterGroup.PROC_UPS_RULE_PARA_GRP;
            cmdGroup.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdGrpParam = new SqlCommand();
            cmdGrpParam.CommandText = Schema.RuleParameters.PROC_UPDATE_RULE_PARA;
            cmdGrpParam.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdDel;
            SqlCommand cmdDelRuleParamGroup = new SqlCommand();
            cmdDelRuleParamGroup.CommandText = Schema.RuleParameterGroup.PROC_DEL_MULTI_RULE_PARA_GRP;
            cmdDelRuleParamGroup.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdDelRuleParam = new SqlCommand();
            cmdDelRuleParam.CommandText = Schema.RuleParameters.PROC_DEL_MULTI_RULE_PARA;
            cmdDelRuleParam.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdAddEditImpact = new SqlCommand();
            cmdAddEditImpact.CommandText = Schema.GroupRule.PROC_RULE_ADDEDIT_IMPACT;
            cmdAddEditImpact.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdBusinessRuleUsers = new SqlCommand();

            using (SqlConnection sqlcon = new SqlConnection(_strConnString))
            {
                try
                {
                    string strRuleParamGroup = "";
                    string strRuleParam = "";
                    // Open Connection
                    sqlcon.Open();
                    //---------------- Transaction (Start) ----------------------------
                    _sqlTransaction = sqlcon.BeginTransaction();

                    sqlcmdMaster.Transaction = _sqlTransaction;
                    pEntGroupRule = AddRuleDetails(pEntGroupRule, strMode, sqlcmdMaster, sqlcon);

                    entGrpRuleReturn = pEntGroupRule;
                    List<RuleParameterGroup> _entListRuleParaGroup = new List<RuleParameterGroup>();

                    foreach (RuleParameterGroup objGroup in pEntGroupRule.RuleParameterGroupList)
                    {
                        objGroup.ClientId = entGrpRuleReturn.ClientId;
                        objGroup.RuleId = entGrpRuleReturn.ID;
                        _entListRuleParaGroup.Add(objGroup);
                        //Concanate the ID 
                        strRuleParamGroup = strRuleParamGroup + "," + objGroup.ID;
                        strRuleParamGroup = strRuleParamGroup.TrimStart(',');
                    }
                    cmdGroup.Connection = sqlcon;
                    cmdGroup.Transaction = _sqlTransaction;
                    _entListRuleParaGroup = BulkUpdateRuleGroup(pEntGroupRule.RuleParameterGroupList, cmdGroup, strMode);

                    entGrpRuleReturn.RuleParameterGroupList.Clear();
                    entGrpRuleReturn.RuleParameterGroupList.AddRange(_entListRuleParaGroup);

                    List<RuleParameters> _entListRuleParamNew = new List<RuleParameters>();
                    foreach (RuleParameterGroup objRuleParam in _entListRuleParaGroup)
                    {
                        foreach (RuleParameters _ruleParameter in objRuleParam.RuleParameterList)
                        {
                            _ruleParameter.ClientId = objRuleParam.ClientId;
                            _ruleParameter.RuleId = objRuleParam.RuleId;
                            _ruleParameter.RuleParameterGroupId = objRuleParam.ID;
                            _entListRuleParamNew.Add(_ruleParameter);
                        }
                    }

                    cmdGrpParam.Connection = sqlcon;
                    cmdGrpParam.Transaction = _sqlTransaction;
                    List<RuleParameters> _entListRuleParam = BulkUpdateRuleParam(_entListRuleParamNew, cmdGrpParam, strMode);

                    for (int i = 0; i < entGrpRuleReturn.RuleParameterGroupList.Count; i++)
                    {
                        List<RuleParameters> entListRuleParamResult = _entListRuleParam.FindAll(delegate (RuleParameters entRuleParameetertoFind)
                        { return entRuleParameetertoFind.RuleParameterGroupId == entGrpRuleReturn.RuleParameterGroupList[i].ID; });
                        if (entListRuleParamResult != null)
                        {
                            entGrpRuleReturn.RuleParameterGroupList[i].RuleParameterList.AddRange(entListRuleParamResult);
                        }
                    }

                    if (strMode == Schema.Common.VAL_UPDATE_MODE)
                    {

                        foreach (RuleParameterGroup objRuleParam in _entListRuleParaGroup)
                        {
                            strRuleParam = "";
                            foreach (RuleParameters _ruleParameter in objRuleParam.RuleParameterList)
                            {
                                strRuleParam = strRuleParam + "," + _ruleParameter.ID;
                                strRuleParam = strRuleParam.TrimStart(',');
                            }

                            //-------- Delete Rule Parameter which are not exist with Data
                            cmdDel = new SqlCommand();
                            cmdDel.CommandType = CommandType.StoredProcedure;
                            cmdDel.CommandText = Schema.RuleParameters.PROC_DEL_MULTI_RULE_PARA;


                            _sqlpara = new SqlParameter(Schema.RuleParameters.PARA_PARAMETER_ID, strRuleParam);
                            cmdDel.Parameters.Add(_sqlpara);

                            _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_ID, pEntGroupRule.ID);
                            cmdDel.Parameters.Add(_sqlpara);

                            _sqlpara = new SqlParameter(Schema.RuleParameterGroup.PARA_PARAMETER_GROUP_ID, objRuleParam.ID);
                            cmdDel.Parameters.Add(_sqlpara);
                            try
                            {
                                cmdDel.Connection = sqlcon;
                                cmdDel.Transaction = _sqlTransaction;
                                bool bRet = _sqlObject.ExecuteNonQuery(cmdDel);
                            }
                            catch (Exception expCommon)
                            {
                                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                                throw _expCustom;
                            }
                            cmdDel.Parameters.Clear();
                        }

                        //-------- Delete Rule Parameter Group which are not exist with Data
                        cmdDel = new SqlCommand();
                        cmdDel.CommandText = Schema.RuleParameterGroup.PROC_DEL_MULTI_RULE_PARA_GRP;
                        cmdDel.CommandType = CommandType.StoredProcedure;

                        _sqlpara = new SqlParameter(Schema.RuleParameterGroup.PARA_PARAMETER_GROUP_ID, strRuleParamGroup);
                        cmdDel.Parameters.Add(_sqlpara);
                        _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_ID, pEntGroupRule.ID);
                        cmdDel.Parameters.Add(_sqlpara);

                        try
                        {
                            cmdDel.Connection = sqlcon;
                            cmdDel.Transaction = _sqlTransaction;
                            bool bRet = _sqlObject.ExecuteNonQuery(cmdDel);
                        }
                        catch (Exception expCommon)
                        {
                            _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                            throw _expCustom;
                        }
                    }

                    if (pEntGroupRule.BusinessRuleUsers != null && pEntGroupRule.BusinessRuleUsers.Count > 0)
                    {
                        //********************************* Add/Edit BusinessRuleUsers ****************                 
                        List<BusinessRuleUsers> _entListBusinessRuleUsers = new List<BusinessRuleUsers>();
                        cmdBusinessRuleUsers.Transaction = _sqlTransaction;
                        _entListBusinessRuleUsers = AddBusinessRuleUsersWithoutScopeUser(pEntGroupRule.BusinessRuleUsers, cmdBusinessRuleUsers, sqlcon);
                        //*****************************************************************************
                    }

                    //********************************* Add/Edit Impact ***************************
                    _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_ID, pEntGroupRule.ID);
                    cmdAddEditImpact.Parameters.Add(_sqlpara);
                    cmdAddEditImpact.CommandTimeout = 0;
                    cmdAddEditImpact.Connection = sqlcon;
                    cmdAddEditImpact.Transaction = _sqlTransaction;
                    _sqlObject.ExecuteNonQuery(cmdAddEditImpact);
                    //*****************************************************************************

                    //To avoid such error: This SqlTransaction has completed; it is no longer usable.
                    try
                    {
                        //Commit the transaction
                        _sqlTransaction.Commit();
                        CreateRuleQueryText(pEntGroupRule);
                    }
                    catch (Exception expCommon)
                    {
                        _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                    }
                    sqlcon.Close();
                    //---------------- Transaction (End) ----------------------------
                }
                catch (Exception expCommon)
                {
                    try
                    {
                        _sqlTransaction.Rollback();
                    }
                    catch (Exception exp)
                    {
                        _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, exp, true, _strConnString);
                    }
                    sqlcon.Close();
                    _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                    throw _expCustom;
                }
                finally
                {
                    if (sqlcon != null && sqlcon.State != ConnectionState.Closed) sqlcon.Close();
                }
            }
            return entGrpRuleReturn;
        }

        private int CreateRuleQueryText(GroupRule pEntGroupRule)
        {
            _sqlObject = new SQLObject();
            SqlCommand sqlCmd = new SqlCommand();
            int iCount = 0;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntGroupRule.ClientId);
                sqlCmd.CommandText = Schema.GroupRule.PROC_CREATE_RULE_QUERY_TEXT;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_ID, pEntGroupRule.ID);
                sqlCmd.Parameters.Add(_sqlpara);
                iCount = _sqlObject.ExecuteNonQuery(sqlCmd, _strConnString);
            }
            catch(Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            return iCount;
        }

        /// <summary>
        /// Add all BusinessRuleUsers from List
        /// </summary>
        /// <param name="pEntListUsers">User List</param>
        /// <returns>Updated List</returns>
        public List<BusinessRuleUsers> AddBusinessRuleUsersWithoutScopeUser(List<BusinessRuleUsers> pEntListBusinessRuleUsers, SqlCommand cmd, SqlConnection con)
        {
            List<BusinessRuleUsers> entListBusinessRuleUsers = new List<BusinessRuleUsers>();
            entListBusinessRuleUsers.AddRange(pEntListBusinessRuleUsers);

            entListBusinessRuleUsers = BulkAddBusinessRuleUsers(entListBusinessRuleUsers, Schema.Common.VAL_UPDATE_MODE, cmd, con);

            if (entListBusinessRuleUsers != null && pEntListBusinessRuleUsers.Count > 0)
            {
                string strUserId = "";
                string strRuleID = "";
                string strClientID = "";
                string strParamGroupId = "";

                BusinessRuleUsers entBRU = new BusinessRuleUsers();
                try
                {
                    entBRU = (BusinessRuleUsers)pEntListBusinessRuleUsers[0];
                    strClientID = entBRU.ClientId;
                    strRuleID = entBRU.BusinessRuleId;

                    foreach (BusinessRuleUsers entBRUser in pEntListBusinessRuleUsers)
                    {
                        strUserId = strUserId + "," + entBRUser.ID;
                        strParamGroupId = strParamGroupId + "," + entBRUser.ParameterGroupId;
                    }
                    if (strUserId != "" && strParamGroupId != "")
                    {
                        strUserId = strUserId.TrimStart(',');
                        strParamGroupId = strParamGroupId.TrimStart(',');
                    }

                    cmd.Parameters.Clear();

                    cmd.CommandText = Schema.BusinessRuleUsers.PROC_DELETE_MULTI_BUSINESS_RULE_USERS;
                    cmd.CommandType = CommandType.StoredProcedure;

                    _sqlpara = new SqlParameter(Schema.Learner.PARA_USER_ID, strUserId);
                    cmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.BusinessRuleUsers.PARA_BUSINESS_RULE_ID, strRuleID);
                    cmd.Parameters.Add(_sqlpara);
                    _sqlpara = new SqlParameter(Schema.BusinessRuleUsers.PARA_PARAMETER_GROUP_ID, strParamGroupId);
                    cmd.Parameters.Add(_sqlpara);

                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception expCommon)
                {
                    throw new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, con.ConnectionString);
                }
            }
            return entListBusinessRuleUsers;
        }

        /// <summary>
        /// Bulk Add/Update: BusinessRuleUsers
        /// </summary>
        /// <param name="pEntListBusinessRuleUsers"></param>
        /// <returns>List of added/updated BusinessRuleUsers</returns>
        private List<BusinessRuleUsers> BulkAddBusinessRuleUsers(List<BusinessRuleUsers> pEntListBusinessRuleUsers, string strMode, SqlCommand cmd, SqlConnection con)
        {
            _sqlObject = new SQLObject();
            List<BusinessRuleUsers> entListBusinessRuleUsers = new List<BusinessRuleUsers>();
            SqlDataAdapter sqladapter = null;
            DataTable dtable;
            int iBatchSize = 0;
            DataRow drow = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntListBusinessRuleUsers[0].ClientId);
                _sqlcon = new SqlConnection(_strConnString);
                dtable = new DataTable();
                dtable.Columns.Add(Schema.Learner.COL_USER_ID);
                dtable.Columns.Add(Schema.BusinessRuleUsers.COL_BUSINESS_RULE_ID);
                dtable.Columns.Add(Schema.BusinessRuleUsers.COL_PARAMETER_GROUP_ID);
                dtable.Columns.Add(Schema.BusinessRuleUsers.COL_IS_INCLUDED);
                dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                foreach (BusinessRuleUsers entBRUser in pEntListBusinessRuleUsers)
                {
                    drow = dtable.NewRow();

                    drow[Schema.Learner.COL_USER_ID] = entBRUser.ID;
                    drow[Schema.BusinessRuleUsers.COL_BUSINESS_RULE_ID] = entBRUser.BusinessRuleId;
                    drow[Schema.BusinessRuleUsers.COL_PARAMETER_GROUP_ID] = entBRUser.ParameterGroupId;
                    drow[Schema.BusinessRuleUsers.COL_IS_INCLUDED] = entBRUser.IsIncluded;
                    drow[Schema.Common.COL_MODIFIED_BY] = entBRUser.LastModifiedById;
                    drow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_INSERT_MODE;

                    dtable.Rows.Add(drow);
                    entListBusinessRuleUsers.Add(entBRUser);
                    iBatchSize++;
                }
                if (dtable.Rows.Count > 0)
                {
                    cmd.CommandText = Schema.BusinessRuleUsers.PROC_UPDATE_BUSINESS_RULE_USERS;
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(Schema.Learner.PARA_USER_ID, SqlDbType.VarChar, 100, Schema.Learner.COL_USER_ID);
                    cmd.Parameters.Add(Schema.BusinessRuleUsers.PARA_BUSINESS_RULE_ID, SqlDbType.NVarChar, 100, Schema.BusinessRuleUsers.COL_BUSINESS_RULE_ID);
                    cmd.Parameters.Add(Schema.BusinessRuleUsers.PARA_PARAMETER_GROUP_ID, SqlDbType.NVarChar, 100, Schema.BusinessRuleUsers.COL_PARAMETER_GROUP_ID);
                    cmd.Parameters.Add(Schema.BusinessRuleUsers.PARA_IS_INCLUDED, SqlDbType.Bit, 100, Schema.BusinessRuleUsers.COL_IS_INCLUDED);
                    cmd.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.NVarChar, 100, Schema.Common.COL_MODIFIED_BY);
                    cmd.Parameters.Add(Schema.Common.PARA_UPDATE_MODE, SqlDbType.NVarChar, 10, Schema.Common.COL_UPDATE_MODE);

                    sqladapter = new SqlDataAdapter();
                    sqladapter.InsertCommand = cmd;
                    sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    sqladapter.UpdateBatchSize = iBatchSize;
                    sqladapter.Update(dtable);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            return entListBusinessRuleUsers;
        }

        /// <summary>
        /// Add Single Rule with Child
        /// </summary>
        /// <param name="pEntGroupRule"></param>
        /// <returns></returns>
        public GroupRule AddSingleRule(GroupRule pEntGroupRule)
        {
            return AddUpdate(pEntGroupRule, Schema.Common.VAL_INSERT_MODE);
        }

        /// <summary>
        /// Update Single Rule with Child
        /// </summary>
        /// <param name="pEntGroupRule"></param>
        /// <returns></returns>
        public GroupRule UpdateSingleRule(GroupRule pEntGroupRule)
        {
            return AddUpdate(pEntGroupRule, Schema.Common.VAL_UPDATE_MODE);
        }

        /// <summary>
        /// Add Rule Details
        /// </summary>
        /// <param name="pGroupRule"></param>
        /// <param name="strMode"></param>
        /// <param name="cmd"></param>
        /// <param name="con"></param>
        /// <returns></returns>
        private GroupRule AddRuleDetails(GroupRule pGroupRule, string strMode, SqlCommand cmd, SqlConnection con)
        {
            cmd.CommandText = Schema.GroupRule.PROC_UPDATE_RULE;
            cmd.CommandType = CommandType.StoredProcedure;
            string strRuleId = string.Empty;
            int i = 0;

            if (String.IsNullOrEmpty(pGroupRule.ID))
            {
                strRuleId = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(Schema.Common.VAL_RULE_ID_PREFIX, Schema.Common.VAL_RULE_ID_LENGTH);
                pGroupRule.ID = strRuleId;
            }
            _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_ID, pGroupRule.ID);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pGroupRule.RuleDescription))
                _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_DESC, pGroupRule.RuleDescription);
            else
                _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_DESC, null);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Client.PARA_CLIENT_ID, pGroupRule.ClientId);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.GroupRule.PARA_IS_ACTIVE, pGroupRule.IsActive);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.GroupRule.PARA_IS_FOR_DISTRIBUTION_LIST, pGroupRule.IsForDistributionList);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pGroupRule.RuleName))
                _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_NAME, pGroupRule.RuleName);
            else
                _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_NAME, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pGroupRule.RuleParentId))
                _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_PARENT_ID, pGroupRule.RuleParentId);
            else
                _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_PARENT_ID, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pGroupRule.CreatedById))
                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, pGroupRule.CreatedById);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_CREATED_BY, null);
            cmd.Parameters.Add(_sqlpara);

            if (!String.IsNullOrEmpty(pGroupRule.LastModifiedById))
                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, pGroupRule.LastModifiedById);
            else
                _sqlpara = new SqlParameter(Schema.Common.PARA_MODIFIED_BY, null);
            cmd.Parameters.Add(_sqlpara);

            _sqlpara = new SqlParameter(Schema.Common.PARA_UPDATE_MODE, strMode);
            cmd.Parameters.Add(_sqlpara);

            cmd.Connection = con;
            i = cmd.ExecuteNonQuery();

            return pGroupRule;
        }

        /// <summary>
        /// Bulk Update
        /// </summary>
        /// <param name="pEntListGroupRule"></param>
        /// <returns></returns>
        public List<GroupRule> BulkUpdate(List<GroupRule> pEntListGroupRule)
        {
            _sqlObject = new SQLObject();
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            List<GroupRule> entListGroupRule = new List<GroupRule>();
            int iBatchSize = 0;
            try
            {
                if (pEntListGroupRule.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.GroupRule.COL_RULE_ID);
                    _dtable.Columns.Add(Schema.GroupRule.COL_RULE_NAME);
                    _dtable.Columns.Add(Schema.GroupRule.COL_RULE_DESC);
                    _dtable.Columns.Add(Schema.GroupRule.COL_IS_ACTIVE);
                    _dtable.Columns.Add(Schema.GroupRule.COL_IS_FOR_DISTRIBUTION_LIST);
                    _dtable.Columns.Add(Schema.Client.COL_CLIENT_ID);
                    _dtable.Columns.Add(Schema.Common.COL_CREATED_BY);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (GroupRule entGroupRule in pEntListGroupRule)
                    {
                        DataRow drow = _dtable.NewRow();

                        if (String.IsNullOrEmpty(_strConnString))
                            _strConnString = _sqlObject.GetClientDBConnString(entGroupRule.ClientId);

                        if (!String.IsNullOrEmpty(entGroupRule.ID))
                            drow[Schema.GroupRule.COL_RULE_ID] = entGroupRule.ID;
                        else
                        {
                            entGroupRule.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                            drow[Schema.GroupRule.COL_RULE_ID] = entGroupRule.ID;
                        }
                        drow[Schema.GroupRule.COL_RULE_NAME] = entGroupRule.RuleName;
                        drow[Schema.GroupRule.COL_RULE_DESC] = entGroupRule.RuleDescription;
                        drow[Schema.GroupRule.COL_IS_ACTIVE] = entGroupRule.IsActive;
                        drow[Schema.GroupRule.COL_IS_FOR_DISTRIBUTION_LIST] = entGroupRule.IsForDistributionList;
                        drow[Schema.Client.COL_CLIENT_ID] = entGroupRule.ClientId;
                        drow[Schema.Common.COL_CREATED_BY] = entGroupRule.CreatedById;
                        drow[Schema.Common.COL_MODIFIED_BY] = entGroupRule.LastModifiedById;

                        _dtable.Rows.Add(drow);
                        iBatchSize = iBatchSize + 1;
                        entListGroupRule.Add(entGroupRule);
                    }
                    if (_dtable.Rows.Count > 0)
                    {
                        _sqlcmd = new SqlCommand();
                        _sqlcmd.CommandText = Schema.GroupRule.PROC_UPDATE_RULE;
                        _sqlcmd.CommandType = CommandType.StoredProcedure;
                        _sqlcon = new SqlConnection(_strConnString);
                        _sqlcmd.Connection = _sqlcon;
                        _sqladapter.InsertCommand = _sqlcmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.GroupRule.PARA_RULE_ID, SqlDbType.VarChar, 100, Schema.GroupRule.COL_RULE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.GroupRule.PARA_RULE_NAME, SqlDbType.NVarChar, 100, Schema.GroupRule.COL_RULE_NAME);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.GroupRule.PARA_RULE_DESC, SqlDbType.NVarChar, 500, Schema.GroupRule.COL_RULE_DESC);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.GroupRule.PARA_IS_ACTIVE, SqlDbType.Bit, 10, Schema.GroupRule.COL_IS_ACTIVE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.GroupRule.PARA_IS_FOR_DISTRIBUTION_LIST, SqlDbType.Bit, 10, Schema.GroupRule.COL_IS_FOR_DISTRIBUTION_LIST);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Client.PARA_CLIENT_ID, SqlDbType.VarChar, 100, Schema.Client.COL_CLIENT_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_CREATED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_CREATED_BY);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.Parameters.AddWithValue(Schema.Common.PARA_UPDATE_MODE, Schema.Common.VAL_INSERT_MODE);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        _sqladapter.Update(_dtable);
                        _sqladapter.Dispose();
                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            return entListGroupRule;
        }

        /// <summary>
        /// Get Group Rule List
        /// </summary>
        /// <param name="pEntGroupRule"></param>
        /// <returns></returns>
        public List<GroupRule> GetGroupRuleList(GroupRule pEntGroupRule)
        {
            _entListRule = new List<GroupRule>();
            _entGroupRule = new GroupRule();
            _sqlObject = new SQLObject();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntGroupRule.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.GroupRule.PROC_GET_ALL_RULES, sqlConnection);
                if (pEntGroupRule.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntGroupRule.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntGroupRule.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntGroupRule.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntGroupRule.IsActive)
                {
                    _sqlpara = new SqlParameter(Schema.GroupRule.PARA_IS_ACTIVE, pEntGroupRule.IsActive);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntGroupRule.RuleName != null)
                {
                    _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_NAME, pEntGroupRule.RuleName);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!String.IsNullOrEmpty(pEntGroupRule.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.GroupRule.PARA_CREATED_BY_ID, pEntGroupRule.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                //if (!String.IsNullOrEmpty(pEntGroupRule.CreatedById))
                //    _sqlpara = new SqlParameter(Schema.GroupRule.PARA_CREATED_BY_ID, pEntGroupRule.CreatedById);
                //else
                //    _sqlpara = new SqlParameter(Schema.GroupRule.PARA_CREATED_BY_ID, null);
                //_sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.GroupRule.PARA_IS_FOR_DISTRIBUTION_LIST, pEntGroupRule.IsForDistributionList);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlcmd.CommandTimeout = 0;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entGroupRule = FillObject(_sqlreader, _sqlObject);
                    _entListRule.Add(_entGroupRule);
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
            return _entListRule;
        }

        public List<GroupRule> GetGroupRuleList_IPerform(GroupRule pEntGroupRule)
        {
            _entListRule = new List<GroupRule>();
            _entGroupRule = new GroupRule();
            _sqlObject = new SQLObject();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntGroupRule.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.GroupRule.PROC_GET_ALL_RULES_ForIPerform, sqlConnection);
                if (pEntGroupRule.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntGroupRule.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntGroupRule.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntGroupRule.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntGroupRule.IsActive)
                {
                    _sqlpara = new SqlParameter(Schema.GroupRule.PARA_IS_ACTIVE, pEntGroupRule.IsActive);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!string.IsNullOrEmpty(pEntGroupRule.ID))
                {
                    _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_ID, pEntGroupRule.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                if (!String.IsNullOrEmpty(pEntGroupRule.CreatedById))
                {
                    _sqlpara = new SqlParameter(Schema.GroupRule.PARA_CREATED_BY_ID, pEntGroupRule.CreatedById);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                //if (!String.IsNullOrEmpty(pEntGroupRule.CreatedById))
                //    _sqlpara = new SqlParameter(Schema.GroupRule.PARA_CREATED_BY_ID, pEntGroupRule.CreatedById);
                //else
                //    _sqlpara = new SqlParameter(Schema.GroupRule.PARA_CREATED_BY_ID, null);
                //_sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.GroupRule.PARA_IS_FOR_DISTRIBUTION_LIST, pEntGroupRule.IsForDistributionList);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlcmd.CommandTimeout = 0;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entGroupRule = FillObject(_sqlreader, _sqlObject);
                    _entListRule.Add(_entGroupRule);
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
            return _entListRule;
        }
        public List<GroupRule> GetGroupRuleListByUserId(GroupRule pEntGroupRule)
        {
            _entListRule = new List<GroupRule>();
            _entGroupRule = new GroupRule();
            _sqlObject = new SQLObject();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntGroupRule.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.GroupRule.PROC_GETBUSINESSRULE_BYUSERID, sqlConnection);


                _sqlpara = new SqlParameter(Schema.GroupRule.PARA_SYSTEMUSERGUID, pEntGroupRule.ruleSystemUserGUID);
                _sqlcmd.Parameters.Add(_sqlpara);



                _sqlcmd.CommandTimeout = 0;
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entGroupRule = FillObjectForGetRuleByUserId(_sqlreader, _sqlObject);
                    _entListRule.Add(_entGroupRule);
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
            return _entListRule;
        }

        /// <summary>
        /// Get Group Rule For DistributionList
        /// </summary>
        /// <param name="pEntGroupRule"></param>
        /// <returns></returns>
        public List<GroupRule> GetGroupRuleListForDistribution(GroupRule pEntGroupRule)
        {
            _entListRule = new List<GroupRule>();
            _entGroupRule = new GroupRule();
            _sqlObject = new SQLObject();
            SqlConnection sqlConnection = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntGroupRule.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.GroupRule.PROC_GET_RULE_FOR_DISTRIBUTIONLIST, sqlConnection);
                if (pEntGroupRule.ListRange != null)
                {
                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_INDEX, pEntGroupRule.ListRange.PageIndex);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_PAGE_SIZE, pEntGroupRule.ListRange.PageSize);
                    _sqlcmd.Parameters.Add(_sqlpara);

                    _sqlpara = new SqlParameter(Schema.Common.PARA_SORT_EXP, pEntGroupRule.ListRange.SortExpression);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (pEntGroupRule.IsActive)
                {
                    _sqlpara = new SqlParameter(Schema.GroupRule.PARA_IS_ACTIVE, pEntGroupRule.IsActive);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }

                if (!string.IsNullOrEmpty(pEntGroupRule.CreatedById))
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, pEntGroupRule.CreatedById);
                else
                    _sqlpara = new SqlParameter(Schema.Common.PARA_REQUESTED_BY_ID, null);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.GroupRule.PARA_IS_FOR_DISTRIBUTION_LIST, pEntGroupRule.IsForDistributionList);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                while (_sqlreader.Read())
                {
                    _entGroupRule = FillObject(_sqlreader, _sqlObject);
                    _entListRule.Add(_entGroupRule);
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
            return _entListRule;
        }

        /// <summary>
        /// Fill Rule Object
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <returns></returns>
        private GroupRule FillObject(SqlDataReader pSqlreader, SQLObject pSqlObject)
        {
            GroupRule entGroupRule = new GroupRule();
            int index;

            index = pSqlreader.GetOrdinal(Schema.GroupRule.COL_RULE_ID);
            if (!pSqlreader.IsDBNull(index))
                entGroupRule.ID = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.GroupRule.COL_RULE_NAME);
            if (!pSqlreader.IsDBNull(index))
                entGroupRule.RuleName = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.GroupRule.COL_RULE_DESC);
            if (!pSqlreader.IsDBNull(index))
                entGroupRule.RuleDescription = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.GroupRule.COL_IS_ACTIVE);
            if (!pSqlreader.IsDBNull(index))
                entGroupRule.IsActive = pSqlreader.GetBoolean(index);

            index = pSqlreader.GetOrdinal(Schema.GroupRule.COL_IS_FOR_DISTRIBUTION_LIST);
            if (!pSqlreader.IsDBNull(index))
                entGroupRule.IsForDistributionList = pSqlreader.GetBoolean(index);

            index = pSqlreader.GetOrdinal(Schema.GroupRule.COL_RULE_PARENT_ID);
            if (!pSqlreader.IsDBNull(index))
                entGroupRule.RuleParentId = pSqlreader.GetString(index);


            if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.GroupRule.COL_IS_USED))
            {
                index = pSqlreader.GetOrdinal(Schema.GroupRule.COL_IS_USED);
                if (!pSqlreader.IsDBNull(index))
                    entGroupRule.IsUsed = pSqlreader.GetBoolean(index);
            }

            index = pSqlreader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
            if (!pSqlreader.IsDBNull(index))
                entGroupRule.ClientId = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_BY);
            if (!pSqlreader.IsDBNull(index))
                entGroupRule.CreatedById = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_ON);
            if (!pSqlreader.IsDBNull(index))
                entGroupRule.DateCreated = pSqlreader.GetDateTime(index);

            index = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
            if (!pSqlreader.IsDBNull(index))
                entGroupRule.LastModifiedById = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
            if (!pSqlreader.IsDBNull(index))
                entGroupRule.LastModifiedDate = pSqlreader.GetDateTime(index);

            if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Common.COL_CREATED_BY_NAME))
            {
                index = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_BY_NAME);
                if (!pSqlreader.IsDBNull(index))
                    entGroupRule.CreatedByName = pSqlreader.GetString(index);
            }

            if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Common.COL_MODIFIED_BY_NAME))
            {
                index = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_BY_NAME);
                if (!pSqlreader.IsDBNull(index))
                    entGroupRule.LastModifiedByName = pSqlreader.GetString(index);
            }


            if (pSqlObject.ReaderHasColumn(pSqlreader, Schema.Common.COL_TOTAL_COUNT))
            {

                index = pSqlreader.GetOrdinal(Schema.Common.COL_TOTAL_COUNT);
                if (!pSqlreader.IsDBNull(index))
                {
                    _entRange = new EntityRange();
                    _entRange.TotalRows = pSqlreader.GetInt32(index);
                    entGroupRule.ListRange = _entRange;
                }
            }





            return entGroupRule;
        }

        private GroupRule FillObjectForGetRuleByUserId(SqlDataReader pSqlreader, SQLObject pSqlObject)
        {
            GroupRule entGroupRule = new GroupRule();
            int index;

            index = pSqlreader.GetOrdinal(Schema.GroupRule.COL_RULE_ID);
            if (!pSqlreader.IsDBNull(index))
                entGroupRule.ID = pSqlreader.GetString(index);

            if (pSqlObject.ReaderHasColumn(pSqlreader, "PreferredTimeZone"))
            {
                index = pSqlreader.GetOrdinal("PreferredTimeZone");
                if (!pSqlreader.IsDBNull(index))
                    entGroupRule.RuleName = pSqlreader.GetString(index);
            }
            return entGroupRule;
        }
        /// <summary>
        /// Get Rule by Id
        /// </summary>
        /// <param name="pEntGroupRule"></param>
        /// <returns></returns>
        public GroupRule GetRuleByID(GroupRule pEntGroupRule)
        {
            GroupRule entGrpRuleReturn = new GroupRule();
            List<RuleParameterGroup> entListRuleParaGroup = new List<RuleParameterGroup>();
            List<RuleParameters> entListRulePara = new List<RuleParameters>();
            List<BusinessRuleUsers> entListBusinessRuleUsers = new List<BusinessRuleUsers>();
            BusinessRuleUsersAdaptor objBusinessRuleUsersAdaptor = new BusinessRuleUsersAdaptor();
            SqlConnection sqlConnection = null;
            _entGroupRule = new GroupRule();
            _sqlObject = new SQLObject();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntGroupRule.ClientId);
                sqlConnection = new SqlConnection(_strConnString);
                _sqlcmd = new SqlCommand(Schema.GroupRule.PROC_GET_RULE, sqlConnection);
                if (!String.IsNullOrEmpty(pEntGroupRule.ID))
                {
                    _sqlpara = new SqlParameter(Schema.GroupRule.PARA_RULE_ID, pEntGroupRule.ID);
                    _sqlcmd.Parameters.Add(_sqlpara);
                }
                _sqlreader = _sqlObject.SqlDataReader(_sqlcmd, false);
                if (_sqlreader.Read())
                {
                    _entGroupRule = FillObject(_sqlreader, _sqlObject);
                }
                entGrpRuleReturn = _entGroupRule;
                if (_sqlreader.NextResult())
                {
                    while (_sqlreader.Read())
                    {
                        entListRuleParaGroup.Add(FillObject_RuleParameterGroup(_sqlreader));
                    }
                }
                if (_sqlreader.NextResult())
                {
                    while (_sqlreader.Read())
                    {
                        entListRulePara.Add(FillObject_RuleParameters(_sqlreader));
                    }
                }
                entGrpRuleReturn.RuleParameterGroupList.AddRange(entListRuleParaGroup);
                for (int i = 0; i < _entGroupRule.RuleParameterGroupList.Count; i++)
                {
                    List<RuleParameters> entListRuleParamResult = entListRulePara.FindAll(delegate (RuleParameters entRuleParameetertoFind)
                    { return entRuleParameetertoFind.RuleParameterGroupId == entGrpRuleReturn.RuleParameterGroupList[i].ID; });
                    if (entListRuleParamResult != null)
                    {
                        entGrpRuleReturn.RuleParameterGroupList[i].RuleParameterList.AddRange(entListRuleParamResult);
                    }
                }
                //-- Get the Buisness Rule Users
                if (_sqlreader.NextResult())
                {
                    while (_sqlreader.Read())
                    {
                        entListBusinessRuleUsers.Add(objBusinessRuleUsersAdaptor.FillObject(_sqlreader, false, _sqlObject));
                    }
                }
                //Find and add into group list
                for (int i = 0; i < _entGroupRule.RuleParameterGroupList.Count; i++)
                {
                    List<BusinessRuleUsers> entListBusinessRuleUsersResult = entListBusinessRuleUsers.FindAll(delegate (BusinessRuleUsers entBusinessRuleUsersToFind)
                    { return entBusinessRuleUsersToFind.ParameterGroupId == entGrpRuleReturn.RuleParameterGroupList[i].ID; });
                    if (entListBusinessRuleUsersResult != null)
                    {
                        entGrpRuleReturn.RuleParameterGroupList[i].BusinessRuleUsersList.AddRange(entListBusinessRuleUsersResult);
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
            return entGrpRuleReturn;
        }

        /// <summary>
        /// Bulk Update Rule Group
        /// </summary>
        /// <param name="pEntListRuleParaGroup"></param>
        /// <param name="cmd"></param>
        /// <param name="strMode"></param>
        /// <returns></returns>
        public List<RuleParameterGroup> BulkUpdateRuleGroup(List<RuleParameterGroup> pEntListRuleParaGroup, SqlCommand cmd, string strMode)
        {
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            List<RuleParameterGroup> entListRuleParaGroup = new List<RuleParameterGroup>();
            int iBatchSize = 0;

            if (pEntListRuleParaGroup.Count > 0)
            {
                _dtable = new DataTable();
                _dtable.Columns.Add(Schema.RuleParameterGroup.COL_PARAMETER_GROUP_ID);
                _dtable.Columns.Add(Schema.RuleParameterGroup.COL_PARA_GROUP_NAME);
                _dtable.Columns.Add(Schema.RuleParameters.COL_NEXT_CONDITION);
                _dtable.Columns.Add(Schema.RuleParameterGroup.COL_SORT_ORDER);
                _dtable.Columns.Add(Schema.GroupRule.COL_RULE_ID);
                _dtable.Columns.Add(Schema.Client.COL_CLIENT_ID);
                _dtable.Columns.Add(Schema.Common.COL_CREATED_BY);
                _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                foreach (RuleParameterGroup objBase in pEntListRuleParaGroup)
                {
                    DataRow drow = _dtable.NewRow();

                    if (String.IsNullOrEmpty(objBase.ID))
                    {
                        objBase.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                    }
                    drow[Schema.RuleParameterGroup.COL_PARAMETER_GROUP_ID] = objBase.ID;
                    drow[Schema.RuleParameterGroup.COL_PARA_GROUP_NAME] = objBase.Name;
                    drow[Schema.RuleParameterGroup.COL_SORT_ORDER] = objBase.SortOrder;
                    drow[Schema.RuleParameters.COL_NEXT_CONDITION] = objBase.NextCondition;
                    drow[Schema.GroupRule.COL_RULE_ID] = objBase.RuleId;
                    drow[Schema.Common.COL_MODIFIED_BY] = objBase.LastModifiedById;
                    drow[Schema.Common.COL_UPDATE_MODE] = strMode;

                    _dtable.Rows.Add(drow);

                    iBatchSize = iBatchSize + 1;
                    entListRuleParaGroup.Add(objBase);
                }
                if (_dtable.Rows.Count > 0)
                {
                    _sqladapter.InsertCommand = cmd;
                    _sqladapter.InsertCommand.Parameters.Add(Schema.RuleParameterGroup.PARA_PARAMETER_GROUP_ID, SqlDbType.VarChar, 100, Schema.RuleParameterGroup.COL_PARAMETER_GROUP_ID);
                    _sqladapter.InsertCommand.Parameters.Add(Schema.RuleParameterGroup.PARA_PARA_GROUP_NAME, SqlDbType.NVarChar, 100, Schema.RuleParameterGroup.COL_PARA_GROUP_NAME);
                    _sqladapter.InsertCommand.Parameters.Add(Schema.RuleParameterGroup.PARA_SORT_ORDER, SqlDbType.VarChar, 100, Schema.RuleParameterGroup.COL_SORT_ORDER);
                    _sqladapter.InsertCommand.Parameters.Add(Schema.RuleParameters.PARA_NEXT_CONDITION, SqlDbType.VarChar, 100, Schema.RuleParameters.COL_NEXT_CONDITION);
                    _sqladapter.InsertCommand.Parameters.Add(Schema.GroupRule.PARA_RULE_ID, SqlDbType.VarChar, 100, Schema.GroupRule.COL_RULE_ID);
                    _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                    _sqladapter.InsertCommand.Parameters.AddWithValue(Schema.Common.PARA_UPDATE_MODE, strMode);
                    _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    _sqladapter.UpdateBatchSize = iBatchSize;
                    _sqladapter.Update(_dtable);
                }
            }
            return entListRuleParaGroup;
        }

        /// <summary>
        /// Bulk Update
        /// </summary>
        /// <param name="pEntListRulePara"></param>
        /// <returns></returns>
        public List<RuleParameters> BulkUpdateRuleParam(List<RuleParameters> pEntListRulePara, SqlCommand cmd, string strMode)
        {
            _sqladapter = new SqlDataAdapter();
            _dtable = new DataTable();
            List<RuleParameters> entListRulePara = new List<RuleParameters>();
            int iBatchSize = 0;
            try
            {
                if (pEntListRulePara.Count > 0)
                {
                    _dtable = new DataTable();
                    _dtable.Columns.Add(Schema.RuleParameters.COL_PARAMETER_ID);
                    _dtable.Columns.Add(Schema.RuleParameters.COL_PARA_NAME);
                    _dtable.Columns.Add(Schema.RuleParameters.COL_LEFT_COND_ID);
                    _dtable.Columns.Add(Schema.RuleParameters.COL_LEFT_COND_VALUE);
                    _dtable.Columns.Add(Schema.RuleParameters.COL_CONDITION);
                    _dtable.Columns.Add(Schema.RuleParameters.COL_CONDITION_ID);
                    _dtable.Columns.Add(Schema.RuleParameters.COL_RIGHT_COND_ID);
                    _dtable.Columns.Add(Schema.RuleParameters.COL_RIGHT_COND_VALUE);
                    _dtable.Columns.Add(Schema.RuleParameters.COL_NEXT_CONDITION);
                    _dtable.Columns.Add(Schema.RuleParameters.COL_PARA_FIELD_TYPE);
                    _dtable.Columns.Add(Schema.RuleParameters.COL_GROUP_TYPE);
                    _dtable.Columns.Add(Schema.RuleParameterGroup.COL_PARAMETER_GROUP_ID);
                    _dtable.Columns.Add(Schema.GroupRule.COL_RULE_ID);
                    _dtable.Columns.Add(Schema.Client.COL_CLIENT_ID);
                    _dtable.Columns.Add(Schema.Common.COL_CREATED_BY);
                    _dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);
                    _dtable.Columns.Add(Schema.Common.COL_UPDATE_MODE);

                    foreach (RuleParameters objBase in pEntListRulePara)
                    {
                        DataRow drow = _dtable.NewRow();

                        if (!String.IsNullOrEmpty(objBase.ID))
                            drow[Schema.RuleParameters.COL_PARAMETER_ID] = objBase.ID;
                        else
                        {
                            objBase.ID = YPLMS.Services.IDGenerator.GetUniqueKey(12);
                            drow[Schema.RuleParameters.COL_PARAMETER_ID] = objBase.ID;
                        }
                        drow[Schema.RuleParameters.COL_PARA_NAME] = objBase.ParameterName;
                        drow[Schema.RuleParameters.COL_LEFT_COND_ID] = objBase.LeftConditionId;
                        drow[Schema.RuleParameters.COL_LEFT_COND_VALUE] = objBase.LeftConditionValue;
                        drow[Schema.RuleParameters.COL_CONDITION] = objBase.Condition;
                        drow[Schema.RuleParameters.COL_CONDITION_ID] = objBase.ConditionId;
                        drow[Schema.RuleParameters.COL_RIGHT_COND_ID] = objBase.RightConditionId;
                        drow[Schema.RuleParameters.COL_RIGHT_COND_VALUE] = objBase.RightConditionValue;
                        drow[Schema.RuleParameters.COL_NEXT_CONDITION] = objBase.NextCondition;
                        drow[Schema.RuleParameters.COL_GROUP_TYPE] = objBase.GroupType;
                        drow[Schema.GroupRule.COL_RULE_ID] = objBase.RuleId;
                        drow[Schema.Client.COL_CLIENT_ID] = objBase.ClientId;
                        drow[Schema.Common.COL_MODIFIED_BY] = objBase.LastModifiedById;
                        drow[Schema.Common.COL_UPDATE_MODE] = Schema.Common.VAL_INSERT_MODE;
                        drow[Schema.RuleParameters.COL_PARA_FIELD_TYPE] = objBase.ParameterFieldType.ToString();
                        drow[Schema.RuleParameterGroup.COL_PARAMETER_GROUP_ID] = objBase.RuleParameterGroupId;

                        _dtable.Rows.Add(drow);
                        iBatchSize = iBatchSize + 1;
                        entListRulePara.Add(objBase);
                    }

                    if (_dtable.Rows.Count > 0)
                    {
                        _sqladapter.InsertCommand = cmd;
                        _sqladapter.InsertCommand.Parameters.Add(Schema.RuleParameters.PARA_PARAMETER_ID, SqlDbType.VarChar, 100, Schema.RuleParameters.COL_PARAMETER_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.RuleParameters.PARA_PARA_NAME, SqlDbType.Int, 10, Schema.RuleParameters.COL_PARA_NAME);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.RuleParameters.PARA_LEFT_COND_ID, SqlDbType.VarChar, 100, Schema.RuleParameters.COL_LEFT_COND_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.RuleParameters.PARA_LEFT_COND_VALUE, SqlDbType.NVarChar, 50, Schema.RuleParameters.COL_LEFT_COND_VALUE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.RuleParameters.PARA_CONDITION, SqlDbType.VarChar, 100, Schema.RuleParameters.COL_CONDITION);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.RuleParameters.PARA_CONDITION_ID, SqlDbType.VarChar, 100, Schema.RuleParameters.COL_CONDITION_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.RuleParameters.PARA_RIGHT_COND_ID, SqlDbType.NVarChar, 100, Schema.RuleParameters.COL_RIGHT_COND_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.RuleParameters.PARA_RIGHT_COND_VALUE, SqlDbType.NVarChar, 500, Schema.RuleParameters.COL_RIGHT_COND_VALUE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.RuleParameters.PARA_NEXT_CONDITION, SqlDbType.VarChar, 100, Schema.RuleParameters.COL_NEXT_CONDITION);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.RuleParameters.PARA_GROUP_TYPE, SqlDbType.VarChar, 100, Schema.RuleParameters.COL_GROUP_TYPE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.GroupRule.PARA_RULE_ID, SqlDbType.VarChar, 100, Schema.GroupRule.COL_RULE_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Client.PARA_CLIENT_ID, SqlDbType.VarChar, 100, Schema.Client.COL_CLIENT_ID);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.VarChar, 100, Schema.Common.COL_MODIFIED_BY);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.RuleParameters.PARA_PARA_FIELD_TYPE, SqlDbType.VarChar, 100, Schema.RuleParameters.COL_PARA_FIELD_TYPE);
                        _sqladapter.InsertCommand.Parameters.Add(Schema.RuleParameterGroup.PARA_PARAMETER_GROUP_ID, SqlDbType.VarChar, 100, Schema.RuleParameterGroup.COL_PARAMETER_GROUP_ID);
                        _sqladapter.InsertCommand.Parameters.AddWithValue(Schema.Common.PARA_UPDATE_MODE, strMode);
                        _sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                        _sqladapter.UpdateBatchSize = iBatchSize;
                        _sqladapter.Update(_dtable);
                    }
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            return entListRulePara;
        }

        /// <summary>
        /// Bulk Add/Update
        /// </summary>
        /// <param name="pEntListBusinessRuleUsers"></param>
        /// <returns>List of added/updated BusinessRuleUsers</returns>
        private List<GroupRule> BulkUpdateStatus(List<GroupRule> pEntListGroupRule, string strMode)
        {
            _sqlObject = new SQLObject();
            List<GroupRule> entListGroupRule = new List<GroupRule>();
            SqlDataAdapter sqladapter = null;
            DataTable dtable;
            SqlCommand sqlcmdInsert;
            int iBatchSize = 0;
            DataRow drow = null;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntListGroupRule[0].ClientId);
                _sqlcon = new SqlConnection(_strConnString);
                dtable = new DataTable();
                dtable.Columns.Add(Schema.GroupRule.COL_RULE_ID);
                dtable.Columns.Add(Schema.GroupRule.COL_IS_ACTIVE);
                dtable.Columns.Add(Schema.Common.COL_MODIFIED_BY);

                foreach (GroupRule entGR in pEntListGroupRule)
                {
                    drow = dtable.NewRow();

                    drow[Schema.GroupRule.COL_RULE_ID] = entGR.ID;
                    drow[Schema.GroupRule.COL_IS_ACTIVE] = entGR.IsActive;
                    drow[Schema.Common.COL_MODIFIED_BY] = entGR.LastModifiedById;

                    dtable.Rows.Add(drow);

                    entListGroupRule.Add(entGR);
                    iBatchSize++;
                }

                if (dtable.Rows.Count > 0)
                {
                    sqlcmdInsert = new SqlCommand(Schema.GroupRule.PROC_UPDATE_RULE_STATUS, _sqlcon);
                    sqlcmdInsert.CommandType = CommandType.StoredProcedure;
                    sqlcmdInsert.Parameters.Add(Schema.GroupRule.PARA_RULE_ID, SqlDbType.NVarChar, 100, Schema.GroupRule.COL_RULE_ID);
                    sqlcmdInsert.Parameters.Add(Schema.GroupRule.PARA_IS_ACTIVE, SqlDbType.Bit, 100, Schema.GroupRule.COL_IS_ACTIVE);
                    sqlcmdInsert.Parameters.Add(Schema.Common.PARA_MODIFIED_BY, SqlDbType.NVarChar, 100, Schema.Common.COL_MODIFIED_BY);

                    sqladapter = new SqlDataAdapter();
                    sqladapter.InsertCommand = sqlcmdInsert;
                    sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    sqladapter.UpdateBatchSize = iBatchSize;
                    sqladapter.Update(dtable);
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            return entListGroupRule;
        }

        /// <summary>
        /// Deactivate Group Rule
        /// </summary>
        /// <param name="pEntListGroupRule"></param>
        /// <returns></returns>
        public List<GroupRule> DeactivateGroupRule(List<GroupRule> pEntListGroupRule)
        {
            List<GroupRule> entListGroupRule = new List<GroupRule>();
            foreach (GroupRule entBase in pEntListGroupRule)
            {
                entBase.IsActive = false;
                entListGroupRule.Add(entBase);
            }
            entListGroupRule = BulkUpdateStatus(entListGroupRule, Schema.Common.VAL_UPDATE_MODE);

            return entListGroupRule;
        }

        /// <summary>
        /// Activate Group Rule
        /// </summary>
        /// <param name="pEntListGroupRule"></param>
        /// <returns></returns>
        public List<GroupRule> ActivateGroupRule(List<GroupRule> pEntListGroupRule)
        {
            List<GroupRule> entListGroupRule = new List<GroupRule>();
            foreach (GroupRule entBase in pEntListGroupRule)
            {
                entBase.IsActive = true;
                entListGroupRule.Add(entBase);
            }
            entListGroupRule = BulkUpdateStatus(entListGroupRule, Schema.Common.VAL_UPDATE_MODE);

            return entListGroupRule;
        }

        /// <summary>
        /// Delete Group Rule
        /// </summary>
        /// <param name="pEntListGroupRule"></param>
        /// <returns></returns>
        public List<GroupRule> DeleteGroupRule(List<GroupRule> pEntListGroupRule)
        {
            List<GroupRule> entListGroupRule = new List<GroupRule>();
            entListGroupRule = BulkDelete(pEntListGroupRule);
            return entListGroupRule;
        }

        /// <summary>
        /// Bulk Add/Update
        /// </summary>
        /// <param name="pEntListBusinessRuleUsers"></param>
        /// <returns>List of added/updated BusinessRuleUsers</returns>
        private List<GroupRule> BulkDelete(List<GroupRule> pEntListGroupRule)
        {
            _sqlObject = new SQLObject();
            List<GroupRule> entListGroupRule = new List<GroupRule>();
            SqlDataAdapter sqladapter = null;
            DataTable dtable;
            SqlCommand sqlcmdDel;
            int iBatchSize = 0;
            DataRow drow = null;
            EntityRange entRange = new EntityRange();
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntListGroupRule[0].ClientId);
                _sqlcon = new SqlConnection(_strConnString);
                dtable = new DataTable();
                dtable.Columns.Add(Schema.GroupRule.COL_RULE_ID);

                //-- Adding Dummy Record on First Row(must be on 1st row) 
                if (pEntListGroupRule.Count > 0)
                {
                    //Add Dummy Record in Table, Bcoz if records which is all asssign then it will not execute nonquery so not get count
                    drow = dtable.NewRow();
                    drow[Schema.GroupRule.COL_RULE_ID] = "Temp0123456789";
                    dtable.Rows.Add(drow);
                    iBatchSize++;
                }
                foreach (GroupRule entGR in pEntListGroupRule)
                {
                    drow = dtable.NewRow();
                    drow[Schema.GroupRule.COL_RULE_ID] = entGR.ID;
                    dtable.Rows.Add(drow);
                    entListGroupRule.Add(entGR);
                    iBatchSize++;
                }
                if (dtable.Rows.Count > 0)
                {
                    sqlcmdDel = new SqlCommand(Schema.GroupRule.PROC_DELETE_RULE, _sqlcon);
                    sqlcmdDel.CommandType = CommandType.StoredProcedure;
                    sqlcmdDel.Parameters.Add(Schema.GroupRule.PARA_RULE_ID, SqlDbType.NVarChar, 100, Schema.GroupRule.COL_RULE_ID);
                    sqladapter = new SqlDataAdapter();
                    sqladapter.InsertCommand = sqlcmdDel;
                    sqladapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    sqladapter.UpdateBatchSize = iBatchSize;
                    entRange.TotalRows = sqladapter.Update(dtable);
                }
                //Bind Total Rows to List: To know how many records are affected/delete.
                entListGroupRule[0].ListRange = entRange;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }
            return entListGroupRule;

        }

        /// <summary>
        /// Fill Parameter object
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <returns></returns>
        private RuleParameters FillObject_RuleParameters(SqlDataReader pSqlreader)
        {
            RuleParameters entRulePara = new RuleParameters();
            int index;

            index = pSqlreader.GetOrdinal(Schema.RuleParameters.COL_PARAMETER_ID);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.ID = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.RuleParameters.COL_PARA_NAME);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.ParameterName = pSqlreader.GetInt32(index);

            index = pSqlreader.GetOrdinal(Schema.RuleParameters.COL_LEFT_COND_ID);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.LeftConditionId = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.RuleParameters.COL_LEFT_COND_VALUE);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.LeftConditionValue = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.RuleParameters.COL_CONDITION);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.Condition = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.RuleParameters.COL_RIGHT_COND_ID);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.RightConditionId = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.RuleParameters.COL_RIGHT_COND_VALUE);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.RightConditionValue = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.RuleParameters.COL_NEXT_CONDITION);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.NextCondition = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.RuleParameters.COL_CONDITION_ID);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.ConditionId = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.GroupRule.COL_RULE_ID);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.RuleId = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.Client.COL_CLIENT_ID);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.ClientId = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_BY);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.CreatedById = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_ON);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.DateCreated = pSqlreader.GetDateTime(index);

            index = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.LastModifiedById = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.LastModifiedDate = pSqlreader.GetDateTime(index);

            index = pSqlreader.GetOrdinal(Schema.RuleParameters.COL_PARA_FIELD_TYPE);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.ParameterFieldType = (ImportDefination.ValueType)Enum.Parse(typeof(ImportDefination.ValueType), pSqlreader.GetString(index));

            index = pSqlreader.GetOrdinal(Schema.RuleParameterGroup.COL_PARAMETER_GROUP_ID);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.RuleParameterGroupId = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.RuleParameters.COL_GROUP_TYPE);
            if (!pSqlreader.IsDBNull(index))
                entRulePara.GroupType = pSqlreader.GetString(index);

            return entRulePara;
        }

        /// <summary>
        /// Fill RuleParameterGroup object
        /// </summary>
        /// <param name="pSqlreader"></param>
        /// <returns></returns>
        public RuleParameterGroup FillObject_RuleParameterGroup(SqlDataReader pSqlreader)
        {
            RuleParameterGroup entRuleParaGroup = new RuleParameterGroup();
            int index;

            index = pSqlreader.GetOrdinal(Schema.RuleParameterGroup.COL_PARAMETER_GROUP_ID);
            if (!pSqlreader.IsDBNull(index))
                entRuleParaGroup.ID = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.RuleParameterGroup.COL_PARA_GROUP_NAME);
            if (!pSqlreader.IsDBNull(index))
                entRuleParaGroup.Name = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.RuleParameterGroup.COL_SORT_ORDER);
            if (!pSqlreader.IsDBNull(index))
                entRuleParaGroup.SortOrder = pSqlreader.GetInt32(index);

            index = pSqlreader.GetOrdinal(Schema.RuleParameters.COL_NEXT_CONDITION);
            if (!pSqlreader.IsDBNull(index))
                entRuleParaGroup.NextCondition = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.GroupRule.COL_RULE_ID);
            if (!pSqlreader.IsDBNull(index))
                entRuleParaGroup.RuleId = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_BY);
            if (!pSqlreader.IsDBNull(index))
                entRuleParaGroup.CreatedById = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.Common.COL_CREATED_ON);
            if (!pSqlreader.IsDBNull(index))
                entRuleParaGroup.DateCreated = pSqlreader.GetDateTime(index);

            index = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_BY);
            if (!pSqlreader.IsDBNull(index))
                entRuleParaGroup.LastModifiedById = pSqlreader.GetString(index);

            index = pSqlreader.GetOrdinal(Schema.Common.COL_MODIFIED_ON);
            if (!pSqlreader.IsDBNull(index))
                entRuleParaGroup.LastModifiedDate = pSqlreader.GetDateTime(index);

            return entRuleParaGroup;
        }


        public string CheckBusinessRuleNameExists(string NewName, int NoOfCopy, string originalName, GroupRule groupRule)
        {
            GroupRule entGroupRue = new GroupRule();
          
            try
            {
                entGroupRue.RuleName = NewName.Trim();
                entGroupRue.ClientId = groupRule.ClientId;
                entGroupRue = GetRuleByName(entGroupRue);
            }
            catch (CustomException expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                // ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "cex", "alert('" + cex.GetCustomMessage(null) + "')", true);
                return null;
            }
            if (entGroupRue != null)
            {
                if (!String.IsNullOrEmpty(entGroupRue.RuleName))
                {
                    NoOfCopy = NoOfCopy + 1;
                    string strCurrName = "Copy of (" + NoOfCopy.ToString() + ") " + originalName;
                    strCurrName = CheckBusinessRuleNameExists(strCurrName, NoOfCopy, originalName, entGroupRue);
                    return strCurrName;
                }
                else
                {
                    return NewName;
                }
            }
            else
            {
                return NewName;
            }
        }

        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntGroupRule"></param>
        /// <returns></returns>
        public GroupRule Get(GroupRule pEntGroupRule)
        {
            return GetRuleByID(pEntGroupRule);
        }
        /// <summary>
        /// Update GroupRule
        /// </summary>
        /// <param name="pEntGroupRule"></param>
        /// <returns></returns>
        public GroupRule Update(GroupRule pEntGroupRule)
        {
            return EditRule(pEntGroupRule);
        }
        #endregion
    }
}
