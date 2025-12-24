using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager
{

    /// <summary>
    /// class LearnerManager
    /// </summary>
    public class LearnerManager : IManager<Learner, Learner.Method, Learner.ListMethod>
    {

        private const string PREFERRED_DATE_FORMAT = "PreferredDateFormat";
        private const string PREFERRED_TIME_FORMAT = "PreferredTimeFormat";
        /// <summary>
        /// Default constructor
        /// </summary>
        public LearnerManager()
        {
        }

        /// <summary>
        /// Used for add,update,Delete,Get,Checklogin transactions.
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <param name="pMethod"></param>
        /// <returns>Learner object information</returns>
        public Learner Execute(Learner pEntLearner, Learner.Method pMethod)
        {
            Learner entLearner = null;
            LearnerDAM adaptorLearner = new LearnerDAM();

            switch (pMethod)
            {
                case Learner.Method.GetPreferredDateTimeFormat:
                    entLearner = adaptorLearner.GetPrefferredDateTime(pEntLearner);
                    break;
                case Learner.Method.GetUserDetailsByTypeID:
                    entLearner = adaptorLearner.GetUserDetailsByTypeID(pEntLearner);
                    break;
                case Learner.Method.GetUserByTypeId:
                    entLearner = adaptorLearner.GetUserByTypeID(pEntLearner);
                    break;
                case Learner.Method.GetUserByEmail:
                    entLearner = adaptorLearner.GetUserByAlias(pEntLearner);
                    break;
                case Learner.Method.GetUserByEmailUpdateProfile:
                    entLearner = adaptorLearner.GetUserByEmailUpdateProfile(pEntLearner);
                    break;
                case Learner.Method.GetUserBySystemUserGUID:
                    entLearner = adaptorLearner.GetUserBySystemUserGUID(pEntLearner);
                    break;
                case Learner.Method.CheckUserByAlias:
                    entLearner = adaptorLearner.CheckUserByAlias(pEntLearner);
                    break;
                case Learner.Method.GetUserByAlias:
                    entLearner = adaptorLearner.GetUserByAlias(pEntLearner);
                    break;
                case Learner.Method.GetUserByAliasILT:
                    entLearner = adaptorLearner.GetUserByAliasILT(pEntLearner);
                    break;
                case Learner.Method.GetUserByAliasForForgotPassword:

                    PasswordPolicyAdaptor objPasswordPolicyAdaptor = new PasswordPolicyAdaptor();
                    PasswordPolicyConfiguration entPasswordPolicyConfiguration = new PasswordPolicyConfiguration();
                    entPasswordPolicyConfiguration.ClientId = pEntLearner.ClientId;

                    entPasswordPolicyConfiguration = objPasswordPolicyAdaptor.GetPasswordPolicyById(entPasswordPolicyConfiguration);

                    if (entPasswordPolicyConfiguration.IsPasswordChange == true)
                    {
                        pEntLearner.UserPassword = PasswordCreator.CreatePassword(pEntLearner.ClientId);
                        entLearner = adaptorLearner.GetUserByAliasForgotPassword(pEntLearner, "UpdatePassword");
                    }
                    else
                    {
                        entLearner = adaptorLearner.GetUserByAliasForgotPassword(pEntLearner, "");
                    }
                    break;
                case Learner.Method.GetUserForReqById:
                    entLearner = adaptorLearner.GetUserRequestedByID(pEntLearner);
                    break;
                case Learner.Method.GetUser_CoursePlayer:
                    entLearner = adaptorLearner.GetUserByID_CoursePlayer(pEntLearner);
                    break;
                case Learner.Method.GetSSOLOGIN:
                    entLearner = adaptorLearner.GetUserByID(pEntLearner);
                    //LMSSession.AddSessionItem(PREFERRED_DATE_FORMAT, entLearner.PreferredDateFormat);
                    //LMSSession.AddSessionItem(PREFERRED_TIME_FORMAT, entLearner.PreferredTimeZone);
                    break;
                case Learner.Method.Get:
                    entLearner = adaptorLearner.GetUserByID(pEntLearner);
                    break;
                case Learner.Method.GetSelfRegiUser:
                    entLearner = adaptorLearner.GetUserByIDSelfRegi(pEntLearner);
                    break;
                case Learner.Method.Add:
                    if (string.IsNullOrEmpty(pEntLearner.UserPassword))
                    {
                        pEntLearner.UserPassword = PasswordCreator.CreatePassword(pEntLearner.ClientId);
                    }
                    entLearner = adaptorLearner.AddUser(pEntLearner, false);
                    break;
                case Learner.Method.ImportAdd:
                    if (string.IsNullOrEmpty(pEntLearner.UserPassword))
                    {
                        if (string.IsNullOrEmpty(pEntLearner.ClientId))
                        {
                            //pEntLearner.ClientId = Convert.ToString(LMSSession.GetValue(Client.CLIENT_SESSION_ID));
                        }
                        pEntLearner.UserPassword = PasswordCreator.CreatePassword(pEntLearner.ClientId);

                    }
                    entLearner = adaptorLearner.AddUserImport(pEntLearner);
                    break;
                case Learner.Method.Update:
                    entLearner = adaptorLearner.UpdateUser(pEntLearner, false);
                    entLearner = adaptorLearner.GetUserByID(pEntLearner);
                    break;
                case Learner.Method.UpdateRegiStatus:
                    entLearner = adaptorLearner.UpdateUserRegiStatus(pEntLearner);
                    break;
                case Learner.Method.ImportUpdate:
                    entLearner = adaptorLearner.UpdateUserImport(pEntLearner);
                    /* below call is commented by fatte base on suggestion from pravin on 11-May-2010.
                     entLearner = adaptorLearner.GetUserByID(pEntLearner);
                     */
                    break;
                case Learner.Method.Delete:
                    entLearner = adaptorLearner.DeleteUser(pEntLearner);
                    break;
                case Learner.Method.CheckLogin:
                    entLearner = adaptorLearner.GetUserByLogin(pEntLearner);
                    //LMSSession.AddSessionItem(PREFERRED_DATE_FORMAT, entLearner.PreferredDateFormat);
                    //LMSSession.AddSessionItem(PREFERRED_TIME_FORMAT, entLearner.PreferredTimeZone);
                    break;
                case Learner.Method.CheckStudentIsApproved:
                    entLearner = adaptorLearner.GetUserAprovedStudent(pEntLearner);
                    break;
                case Learner.Method.UpdateStatus:
                    entLearner = adaptorLearner.UpdateAllUserStatus(pEntLearner);
                    break;
                case Learner.Method.UpdateLanguage:
                    entLearner = adaptorLearner.UpdateUserLanguage(pEntLearner);
                    /* removed by fatte+ashish+nitin on 29-april-2010
                      entLearner = adaptorLearner.GetUserByID(pEntLearner); 
                     */
                    break;
                case Learner.Method.UpdateFirstLogin:
                    entLearner = adaptorLearner.UpdateUserFirstLogin(pEntLearner);
                    break;
                case Learner.Method.CheckNewPwd:
                    entLearner = adaptorLearner.ChecknewPwd(pEntLearner);
                    break;

                case Learner.Method.AssignManagers:
                    entLearner = adaptorLearner.AssignManagers(pEntLearner);
                    break;
                case Learner.Method.ValidateAddEdit:
                    entLearner = adaptorLearner.ValidateImportUser(pEntLearner, ImportAction.Add_Edit);
                    break;
                case Learner.Method.ValidateADC:
                    entLearner = adaptorLearner.ValidateImportUser(pEntLearner, ImportAction.Activate);
                    break;
                case Learner.Method.ValidateResetPassword:
                    entLearner = adaptorLearner.ValidateImportUser(pEntLearner, ImportAction.PasswordReset);
                    break;
                case Learner.Method.UpdateDBIndex:
                    entLearner = adaptorLearner.UpdateDBIndexes(pEntLearner);
                    break;
                case Learner.Method.SyncBRUsersOnImport:
                    entLearner = adaptorLearner.SynchBusinessRuleUsersOnImport(pEntLearner);
                    break;
                case Learner.Method.SetInitialSettingsOnImport:
                    entLearner = adaptorLearner.SetUserInitialSettingOnImport(pEntLearner);
                    break;
                case Learner.Method.UpdateProfile:
                    entLearner = adaptorLearner.UpdateUser(pEntLearner, false);
                    break;
                case Learner.Method.GetUserScope:
                    entLearner = adaptorLearner.GetUserScope(pEntLearner);
                    break;
                case Learner.Method.UpdateDateFormat:
                    entLearner = adaptorLearner.UpdateDateFormat(pEntLearner);
                    //LMSSession.AddSessionItem(PREFERRED_DATE_FORMAT, entLearner.PreferredDateFormat);
                    //LMSSession.AddSessionItem(PREFERRED_TIME_FORMAT, entLearner.PreferredTimeZone);
                    break;
                case Learner.Method.GetUserSystemGUID:
                    entLearner = adaptorLearner.GetUserSystemGUID(pEntLearner);
                    break;

                case Learner.Method.UpdateTermsAndCondition:
                    entLearner = adaptorLearner.UpdateTermsAndCondition(pEntLearner);
                    break;

                case Learner.Method.UpdateLockUnlockOTP:
                    entLearner = adaptorLearner.UpdateLockUnlockOTP(pEntLearner);
                    break;
                case Learner.Method.GetIsUserLock:
                    entLearner = adaptorLearner.GetIsUserLock(pEntLearner);
                    break;
                case Learner.Method.GetOTPNumber:
                    entLearner = adaptorLearner.GetOTPNumber(pEntLearner);
                    break;

                case Learner.Method.AddSubscriptionOfUserForNewsLetter:
                    entLearner = adaptorLearner.AddSubscriptionOfUserForNewsLetter(pEntLearner);
                    break;


                case Learner.Method.UpdateUserCustomField:
                    adaptorLearner.UpdateUserCustomField(pEntLearner);
                    break;
                case Learner.Method.GetActiveUserEmail:
                    entLearner = adaptorLearner.GetActiveUserEmail(pEntLearner);
                    break;
                default:
                    entLearner = null;
                    break;
            }
            return entLearner;
        }

        /// <summary>
        /// Used for add,update,Delete,Get,Checklogin transactions.
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <param name="pMethod"></param>
        /// <returns>Learner object information</returns>
        public Learner Execute(Learner pEntLearner, Learner.Method pMethod, out string strMessage)
        {
            strMessage = string.Empty;
            Learner entLearner = null;
            LearnerDAM adaptorLearner = new LearnerDAM();

            switch (pMethod)
            {
                case Learner.Method.CheckSetNewPassword:
                    entLearner = adaptorLearner.CheckSetNewPassword(pEntLearner, out strMessage);
                    break;
                default:
                    entLearner = null;
                    break;
            }
            return entLearner;
        }
        /// <summary>
        /// Returns list of users  
        /// </summary>
        /// <param name="pEntLearner"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of Learner objects</returns>
        public List<Learner> Execute(Learner pEntLearner, Learner.ListMethod pMethod)
        {
            List<Learner> entListLearner = null;
            LearnerDAM adaptorLearner = new LearnerDAM();
            switch (pMethod)
            {
                case Learner.ListMethod.DynamicUserList:
                    entListLearner = adaptorLearner.GetDynamicUserList(pEntLearner);
                    break;
                case Learner.ListMethod.OneTimeUserList:
                    entListLearner = adaptorLearner.GetOneTimeUserList(pEntLearner);
                    break;
                case Learner.ListMethod.GetBulkImport:
                    entListLearner = adaptorLearner.GetBulkImport(pEntLearner);
                    break;
                case Learner.ListMethod.GetAllForAssessmentCourse:
                    entListLearner = adaptorLearner.GetLearnersForAssessmentCourse(pEntLearner);
                    break;
                case Learner.ListMethod.GetAllUserSubscribeForNewsLetter:
                    entListLearner = adaptorLearner.GetAllUserSubscribeForNewsLetter(pEntLearner);
                    break;
                default:
                    entListLearner = null;
                    break;
            }
            return entListLearner;
        }

        /// <summary>
        /// For Bulk Add/Update
        /// </summary>
        /// <param name="pEntListBase">List Of users</param>
        /// <param name="pMethod">ListMethod</param>
        /// <returns>Updated List of Learner objects</returns>
        public List<Learner> Execute(List<Learner> pEntListBase, Learner.ListMethod pMethod)
        {
            List<Learner> entListLearner = null;
            LearnerDAM adaptorLearner = new LearnerDAM();
            if (pEntListBase.Count > 0)
            {
                switch (pMethod)
                {
                    case Learner.ListMethod.DeleteAll:

                        entListLearner = adaptorLearner.DeleteSelectedUser(pEntListBase);

                        break;
                    case Learner.ListMethod.BulkActivateDeactivate:
                        entListLearner = adaptorLearner.BulkActivateDeactivate(pEntListBase);
                        break;
                    case Learner.ListMethod.BulkUsersPasswordReset:
                        entListLearner = adaptorLearner.BulkUsersPasswordReset(pEntListBase);
                        break;

                    case Learner.ListMethod.BulkChangeId:
                        entListLearner = adaptorLearner.BulkChangeId(pEntListBase);
                        break;
                    default:
                        break;
                }
            }
            return entListLearner;
        }


        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="Learner"></typeparam>
        /// <param name="pEntBase">Learner object</param>
        /// <param name="pMethod">Learner.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(Learner pEntBase, Learner.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<Learner> listLearner = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<Learner>(listLearner);
            return dataSet;

        }
    }
}
