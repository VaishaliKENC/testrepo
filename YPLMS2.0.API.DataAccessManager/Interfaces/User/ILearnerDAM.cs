using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface ILearnerDAM<T>
    {
        Learner GetUserBySamlIdentifier(string identifier, long userIdentifierColumnId, string clientId);
        Learner GetUserByID(Learner pEntLearner);
        Learner GetUserByIDSelfRegi(Learner pEntLearner);
        Learner GetUserByID_CoursePlayer(Learner pEntLearner);
        Learner GetUserRequestedByID(Learner pEntLearner);
        Learner GetPrefferredDateTime(Learner pEntLearner);
        Learner GetUserByTypeID(Learner pEntLearner);
        Learner GetUserDetailsByTypeID(Learner pEntLearner);
        List<Learner> FindLearners(Search pEntSearch);
        List<Learner> FindSelfRegistration(Search pEntSearch);
        Learner GetUserByLogin(Learner pEntLearner);
        Learner SaveTokenToDB(Learner pEntLearner);
        Learner GetUserSystemGUID(Learner pEntLearner);
        Learner GetIsUserLock(Learner pEntLearner);
        Learner GetOTPNumber(Learner pEntLearner);
        Learner GetUserByAliasForgotPassword(Learner pEntLearner, string PasswordUpdate);
        Learner GetActiveUserEmail(Learner pEntLearner);
        List<Learner> FindLearnersForRoleAssignment(Search pEntSearch, bool pIsIn);
        List<Learner> FindBussinessRuleUsers(Search pEntSearch);
        List<Learner> FindLearnersForAssignment(Search pEntSearch);
        List<Learner> FindLearnersForAssignmentOptimized(Search pEntSearch);
        List<Learner> FindLearnersForUnAssignment(Search pEntSearch);
        List<Learner> FindLearnersForUnAssignmentOptimized(Search pEntSearch);
        List<Learner> SearchLearners(Search pEntSearch);
        Learner GetUserAprovedStudent(Learner pEntLearner);
        List<Learner> SearchLearners_ForIPerform(Search pEntSearch);
        Learner UpdateDateFormat(Learner pEntLearner);
        Learner GetUserByAlias(Learner pEntLearner);
        Learner GetUserByAliasILT(Learner pEntLearner);
        Learner UpdateTermsAndCondition(Learner pEntLearner);
        Learner CheckUserByAlias(Learner pEntLearner);
        Learner GetUserByEmailUpdateProfile(Learner pEntLearner);
        Learner GetUserBySystemUserGUID(Learner pEntLearner);
        Learner UpdateUser(Learner pEntLearner, bool pIsImport);
        Learner UpdateUserRegiStatus(Learner pEntLearner);
        Learner UpdateAllUserStatus(Learner pEntUser);
        Learner AddUser(Learner pEntLearner, bool pIsImport);
        Learner AddUserImport(Learner pEntLearner);
        Learner UpdateUserImport(Learner pEntLearner);
        Learner DeleteUser(Learner pEntUser);
        Learner AssignManagers(Learner pEntUser);
        Learner UpdateDBIndexes(Learner pEntUser);
        Learner SynchBusinessRuleUsersOnImport(Learner pEntUser);
        Learner SetUserInitialSettingOnImport(Learner pEntUser);
        Learner ValidateImportUser(Learner pEntUser, ImportAction pImportAction);
        Learner UpdateUserFirstLogin(Learner pEntUser);
        Learner UpdateLockUnlockOTP(Learner pEntUser);
        Learner UpdateUserLanguage(Learner pEntUser);
        List<Learner> DeleteSelectedUser(List<Learner> pEntListBase);
        Learner ChecknewPwd(Learner pEntUser);
        Learner CheckSetNewPassword(Learner pEntUser, out string strMessage);
        List<Learner> BulkActivateDeactivate(List<Learner> pEntListLearners);
        List<Learner> BulkUsersPasswordReset(List<Learner> pEntListLearners);
        List<Learner> BulkChangeId(List<Learner> pEntListLearners);
        List<Learner> FindLearnersForAllRoleAssignment(Search pEntSearch, bool pIsIn);
        List<Learner> GetDynamicUserList(Learner pEntLearner);
        List<Learner> GetOneTimeUserList(Learner pEntLearner);
        List<Learner> GetBulkImport(Learner pEntLearner);
        Learner GetUserScope(Learner pEntLearner);
        List<Learner> GetLearnersForAssessmentCourse(Learner pEntLearner);
        List<Learner> FindLearnersForUnlockAssignment(Search pEntSearch);
        List<Learner> FindLearnersForSession(Search pEntSearch);
        Learner AddSubscriptionOfUserForNewsLetter(Learner pEntLearner);
        List<Learner> SearchRegionalAdmins(Search pEntSearch);
        List<Learner> GetAllUserSubscribeForNewsLetter(Learner pEntLearner);
        bool UpdateUserCustomField(Learner objLearner);
        bool IsLoginIdExists(Learner EntLearner);
        bool CheckEmailIdExists(Learner EntLearner);
        List<Learner> GetUserByRequestedID(Search pEntSearch);

        List<Learner> GetUserByRuleID(Learner pEntLearner);
    }
}
