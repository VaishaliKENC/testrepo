using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.YPLMS.Services;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.Tracking
{
    /// <summary>
    /// class BackgroundBRuleAssignmentDAM
    /// </summary>
    public class BackgroundBRuleAssignmentDAM : IDataManager<BackgroundBRuleAssignment>
    {
        #region Declaration
        CustomException _expCustom = null;
        SqlDataReader _sqlreader = null;
        SqlParameter _sqlpara = null;
        SqlCommand _sqlcmd = null;
        SqlConnection _sqlConnection = null;
        SQLObject _sqlObject = null;
        string _strMessageId = API.YPLMS.Services.Messages.EmailTemplate.DL_ERROR;
        string _strConnString = string.Empty;
        #endregion


        public BackgroundBRuleAssignment PerformDynamicAssignment(BackgroundBRuleAssignment pEntBackgroundBRuleAssignment)
        {

            _sqlObject = new SQLObject();
            _sqlcmd = new SqlCommand();

            _sqlcmd.CommandText = Schema.BackgroundBRuleAssignment.PROC_PERFORM_BGDYNAMICASSIGNMENT;
            try
            {
                _strConnString = _sqlObject.GetClientDBConnString(pEntBackgroundBRuleAssignment.ClientId);

                _sqlpara = new SqlParameter(Schema.BackgroundBRuleAssignment.PARA_ACTIVITY_ID, pEntBackgroundBRuleAssignment.ActivityId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.BackgroundBRuleAssignment.PARA_SYSTEM_USER_GUID, pEntBackgroundBRuleAssignment.SysteMUserGuId);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlpara = new SqlParameter(Schema.BackgroundBRuleAssignment.PARA_ACTIVITYTYPE, pEntBackgroundBRuleAssignment.ActivityType);
                _sqlcmd.Parameters.Add(_sqlpara);

                _sqlObject.ExecuteNonQuery(_sqlcmd, _strConnString);
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
            return pEntBackgroundBRuleAssignment;
        }


        #region Interface Methods
        /// <summary>
        /// Get By ID
        /// </summary>
        /// <param name="pEntBackgroundBRuleAssignment"></param>
        /// <returns></returns>
        public BackgroundBRuleAssignment Get(BackgroundBRuleAssignment pEntBackgroundBRuleAssignment)
        {
            return null;
        }
        /// <summary>
        /// Update BackgroundBRuleAssignment
        /// </summary>
        /// <param name="pEntBackgroundBRuleAssignment"></param>
        /// <returns></returns>
        public BackgroundBRuleAssignment Update(BackgroundBRuleAssignment pEntBackgroundBRuleAssignment)
        {
            return null;
        }
        #endregion
    }
}
