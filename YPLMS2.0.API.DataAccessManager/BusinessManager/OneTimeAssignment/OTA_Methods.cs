using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using System.Xml;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Assignment;
using YPLMS2._0.API.DataAccessManager.BusinessManager.VirtualTraining;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;
using System.Net;
using Microsoft.AspNetCore.Http;
using static YPLMS2._0.API.Entity.Assignment;
using DocumentFormat.OpenXml.Vml;
using System.Reflection.Emit;
using Lucene.Net.Messages;
using Org.BouncyCastle.Asn1.Cmp;
using DocumentFormat.OpenXml.Drawing.Charts;
using SysDataTable = System.Data.DataTable;



namespace YPLMS2._0.API.DataAccessManager.BusinessManager
{
    public class OTA_Methods 
    {
        ActivityAssignmentManager entMgrActivityAssignment = new ActivityAssignmentManager();
        //AdminFeatureManager entMgrAdminFeature = new AdminFeatureManager();
        LookUpManager entMgrLookUp = new LookUpManager();
        AssignmentManager entMgrAssignment = new AssignmentManager();
        //YPLMS.BusinessManager.SearchManager entMgrSearch = new YPLMS.BusinessManager.SearchManager();
        AutoEmailTemplateSettingManager entMgrAutoEmailTemplateSettings = new AutoEmailTemplateSettingManager();
        EmailTemplateManager entMgrEmailTemplate = new EmailTemplateManager();
        LearnerManager entMgrLearner = new LearnerManager();
        CustomException _expCustom = null;

        public static string strVirtualattendeeID = string.Empty;
        public static string strVirtualRegistrationID = string.Empty;
        public static string strVirtualresponseFromServer = string.Empty;

        EmailDeliveryDashboardManager entMgrEmailDeliveryDashboard = new EmailDeliveryDashboardManager();
        public static string strVirtualStatus = string.Empty;

        public OTA_Methods() { }

        public EmailTemplate GetEmailTemplate(Entity.Assignment pEntActivityAssignment)
        {
            EmailTemplate entTemplate = null;
            EmailDeliveryDashboard entEmailDashBoard = null;
            AutoEmailTemplateSettingManager entMgrAutoEmailTemplateSettings = new AutoEmailTemplateSettingManager();
            EmailTemplateManager entMgrEmailTemplate = new EmailTemplateManager();

            //if sendmail check get emial template for send email
            try
            {
                if (pEntActivityAssignment.IsMailChecked == true)
                {
                    //schedule email 
                    if (pEntActivityAssignment.rdbtnSchedule == true)
                    {
                        //entEmailDashBoard = usrScheduleCtrl.EmailScheduleDelivery;
                        entEmailDashBoard = null;
                        if (entEmailDashBoard != null)
                        {
                            entTemplate = new EmailTemplate();
                            entTemplate = entEmailDashBoard.EmailTemplate;
                        }
                    }
                    //auto email 
                    else if (pEntActivityAssignment.IsAutoMail == true)
                    {
                        AutoEmailTemplateSetting autoEmail = new AutoEmailTemplateSetting();
                        autoEmail.ID = AutoEmailTemplateSetting.EVENT_LEARNER_ASSIGNMENT;
                        autoEmail.ClientId = pEntActivityAssignment.ClientId;

                        autoEmail = entMgrAutoEmailTemplateSettings.Execute(autoEmail, AutoEmailTemplateSetting.Method.Get);

                        if (!string.IsNullOrEmpty(autoEmail.EmailTemplateID))
                        {
                            entTemplate = new EmailTemplate();
                            entTemplate.ID = autoEmail.EmailTemplateID;
                        }
                    }
                    if (entTemplate != null && entTemplate.ID != null)
                    {
                        entTemplate.ClientId = pEntActivityAssignment.ClientId;
                        entTemplate.ApprovalStatus = EmailTemplate.EmailApprovalStatus.Approved;
                        entTemplate = entMgrEmailTemplate.Execute(entTemplate, EmailTemplate.Method.Get);
                    }

                }
            }
            catch { }
            return entTemplate;
        }

        public void setAssignmnetDate(Entity.Assignment model, ref ActivityAssignment entAddActivity)
        {
            try
            {
                if (model.AssignmentDateType == 0) // absolute date
                {
                    //entAddActivity.AssignmentDateSet = CommonMethods.GetSpecificDateFormat(model.AssignmentDateText?.Trim());
                    entAddActivity.AssignmentDateSet = Convert.ToDateTime(model.AssignmentDateText); // ✅ Valid
                }
                else if (model.AssignmentDateType == 1) // relative Hire Date
                {
                    entAddActivity.IsAssignmentBasedOnHireDate = true;
                    entAddActivity.AssignAfterDaysOf = Convert.ToInt32(model.AssignmentDaysText?.Trim());
                }
                else // Creation Date
                {
                    entAddActivity.IsAssignmentBasedOnCreationDate = true;
                    entAddActivity.AssignAfterDaysOf = Convert.ToInt32(model.AssignmentDaysText?.Trim());
                }

                entAddActivity.IsNoDueDate = model.NoDueDate;

                if (!model.NoDueDate)
                {
                    switch (model.DueDateType)
                    {
                        case 0:
                            if (!string.IsNullOrEmpty(model.DueDateText?.Trim()))
                                entAddActivity.DueDateSet = Convert.ToDateTime(model.DueDateText);
                            //entAddActivity.DueDateSet = CommonMethods.GetSpecificDateFormat(model.DueDateText.Trim());
                            break;
                        case 1:
                            entAddActivity.IsDueBasedOnHireDate = true;
                            if (!string.IsNullOrEmpty(model.DueDaysText?.Trim()))
                                entAddActivity.DueAfterDaysOf = Convert.ToInt32(model.DueDaysText.Trim());
                            break;
                        case 2:
                            entAddActivity.IsDueBasedOnCreationDate = true;
                            if (!string.IsNullOrEmpty(model.DueDaysText?.Trim()))
                                entAddActivity.DueAfterDaysOf = Convert.ToInt32(model.DueDaysText.Trim());
                            break;
                        case 3:
                            entAddActivity.IsDueBasedOnAssignDate = true;
                            if (!string.IsNullOrEmpty(model.DueDaysText?.Trim()))
                                entAddActivity.DueAfterDaysOf = Convert.ToInt32(model.DueDaysText.Trim());
                            break;
                        case 4:
                            entAddActivity.IsDueBasedOnStartDate = true;
                            if (!string.IsNullOrEmpty(model.DueDaysText?.Trim()))
                                entAddActivity.DueAfterDaysOf = Convert.ToInt32(model.DueDaysText.Trim());
                            break;
                    }
                }

                entAddActivity.IsNoExpiryDate = model.NoExpiryDate;

                if (!model.NoExpiryDate)
                {
                    switch (model.ExpiryDateType)
                    {
                        case 0:
                            if (!string.IsNullOrEmpty(model.ExpiryDateText?.Trim()))
                                entAddActivity.ExpiryDateSet = Convert.ToDateTime(model.ExpiryDateText);
                            //entAddActivity.ExpiryDateSet = CommonMethods.GetSpecificDateFormat(model.ExpiryDateText.Trim());
                            break;
                        case 1:
                            entAddActivity.IsExpiryBasedOnAssignDate = true;
                            if (!string.IsNullOrEmpty(model.ExpiryDaysText?.Trim()))
                                entAddActivity.ExpireAfterDaysOf = Convert.ToInt32(model.ExpiryDaysText.Trim());
                            break;
                        case 2:
                            entAddActivity.IsExpiryBasedOnDueDate = true;
                            if (!string.IsNullOrEmpty(model.ExpiryDaysText?.Trim()))
                                entAddActivity.ExpireAfterDaysOf = Convert.ToInt32(model.ExpiryDaysText.Trim());
                            break;
                        case 3:
                            entAddActivity.IsExpiryBasedOnStartDate = true;
                            if (!string.IsNullOrEmpty(model.ExpiryDaysText?.Trim()))
                                entAddActivity.ExpireAfterDaysOf = Convert.ToInt32(model.ExpiryDaysText.Trim());
                            break;
                    }
                }
            }
            catch
            {
                // Optional: log error
            }
            //throw new NotImplementedException();
        }

        public void setReassignmnetdate(Entity.Assignment model, ref ActivityAssignment entAddActivity)
        {
            if (model.IsReassignmentDateEmpty)
                return;

            try
            {
                // Reassignment Date
                switch (model.ReassignmentDateType)
                {
                    case 0:
                        if (!string.IsNullOrEmpty(model.ReassignmentDateText?.Trim()))
                            entAddActivity.ReAssignmentDateSet = CommonMethods.GetSpecificDateFormat(model.ReassignmentDateText.Trim());
                        break;
                    case 1:
                        entAddActivity.IsReAssignmentBasedOnAssignmentDate = true;
                        if (!string.IsNullOrEmpty(model.ReassignmentDaysText?.Trim()))
                            entAddActivity.ReAssignAfterDaysOf = Convert.ToInt32(model.ReassignmentDaysText.Trim());
                        break;
                    case 2:
                        entAddActivity.IsReAssignmentBasedOnAssignmentCompletionDate = true;
                        if (!string.IsNullOrEmpty(model.ReassignmentDaysText?.Trim()))
                            entAddActivity.ReAssignAfterDaysOf = Convert.ToInt32(model.ReassignmentDaysText.Trim());
                        break;
                }

                // Due Date
                entAddActivity.IsReassignNoDueDate = model.NoReassignDueDate;

                if (!model.NoReassignDueDate)
                {
                    switch (model.ReassignDueDateType)
                    {
                        case 0:
                            if (!string.IsNullOrEmpty(model.ReassignDueDateText?.Trim()))
                                entAddActivity.ReassignDueDateSet = CommonMethods.GetSpecificDateFormat(model.ReassignDueDateText.Trim());
                            break;
                        case 1:
                            entAddActivity.IsReassignDueBasedOnAssignmentCompletionDate = true;
                            if (!string.IsNullOrEmpty(model.ReassignDueDaysText?.Trim()))
                                entAddActivity.ReassignDueAfterDaysOf = Convert.ToInt32(model.ReassignDueDaysText.Trim());
                            break;
                        case 2:
                            entAddActivity.IsReassignDueBasedOnReassignmentDate = true;
                            if (!string.IsNullOrEmpty(model.ReassignDueDaysText?.Trim()))
                                entAddActivity.ReassignDueAfterDaysOf = Convert.ToInt32(model.ReassignDueDaysText.Trim());
                            break;
                    }
                }

                // Expiry Date
                entAddActivity.IsReassignNoExpiryDate = model.NoReassignExpiryDate;

                if (!model.NoReassignExpiryDate)
                {
                    switch (model.ReassignExpiryDateType)
                    {
                        case 0:
                            if (!string.IsNullOrEmpty(model.ReassignExpiryDateText?.Trim()))
                                entAddActivity.ReassignExpiryDateSet = CommonMethods.GetSpecificDateFormat(model.ReassignExpiryDateText.Trim());
                            break;
                        case 1:
                            entAddActivity.IsReassignExpiryBasedOnAssignmentCompletionDate = true;
                            if (!string.IsNullOrEmpty(model.ReassignExpiryDaysText?.Trim()))
                                entAddActivity.ReassignExpireAfterDaysOf = Convert.ToInt32(model.ReassignExpiryDaysText.Trim());
                            break;
                        case 2:
                            entAddActivity.IsReassignExpiryBasedOnReassignmentDate = true;
                            if (!string.IsNullOrEmpty(model.ReassignExpiryDaysText?.Trim()))
                                entAddActivity.ReassignExpireAfterDaysOf = Convert.ToInt32(model.ReassignExpiryDaysText.Trim());
                            break;
                        case 3:
                            entAddActivity.IsReassignExpiryBasedOnReassignmentDueDate = true;
                            if (!string.IsNullOrEmpty(model.ReassignExpiryDaysText?.Trim()))
                                entAddActivity.ReassignExpireAfterDaysOf = Convert.ToInt32(model.ReassignExpiryDaysText.Trim());
                            break;
                    }
                }
            }
            catch
            {
                // Optional: add logging if needed
            }
        }

        public string GetActivityContentType(string pStrContentType)
        {
            string strActivityType = string.Empty;

            //check for activity type for course else return as it is 
            switch (pStrContentType.ToLower())
            {
                case "scorm 1.2":
                    strActivityType = ActivityContentType.Scorm12.ToString();
                    break;
                case "scorm 2004":
                    strActivityType = ActivityContentType.Scorm2004.ToString();
                    break;
                case "aicc":
                    strActivityType = ActivityContentType.AICC.ToString();
                    break;
                case "non compliant":
                    strActivityType = ActivityContentType.NonCompliant.ToString();
                    break;
                default:
                    strActivityType = pStrContentType;
                    break;
            }
            return strActivityType;
        }

        public string SaveAssignmentAndSendEmail(Entity.ActivityAssignment pEntParams)
        {
            string url = string.Empty;
            string jscript = string.Empty;
            string strActivityIdWithoutCompletion = string.Empty;
            string strRedirectUrl = string.Empty;

            try
            {
                if (pEntParams.LstActAssignments.Count > 0)
                {
                    List<ActivityAssignment> entListBaseAct = entMgrActivityAssignment.Execute(pEntParams.LstActAssignments, ActivityAssignment.ListMethod.AddAll);

                    #region nomination/attendee if curriculum contains training
                    for (int i = 0; i < pEntParams.LstActAssignments.Count; i++)
                    {
                        if (pEntParams.LstActAssignments[i].ActivityTypeId == ActivityContentType.Curriculum.ToString())
                        {
                            ActivityAssignment entActAssign = new ActivityAssignment();
                            entActAssign.ClientId = pEntParams.ClientId;
                            entActAssign.ID = pEntParams.LstActAssignments[i].ID;
                            entActAssign.UserID = pEntParams.LstActAssignments[i].UserID;
                            entActAssign.CreatedById = pEntParams.CurrentUserID;
                            entActAssign.ActivityTypeId = pEntParams.LstActAssignments[i].ActivityTypeId;
                            entActAssign.UserName = "";

                            DataSet _ds = entMgrActivityAssignment.ExecuteDataSet1(entActAssign, YPLMS2._0.API.Entity.ActivityAssignment.ListMethod.GetVirtualClassroomNominationDetailsDuringOneTimeAssignment);
                            if (_ds != null && _ds.Tables.Count > 0)
                            {
                                //SysDataTable dt = new SysDataTable();
                                SysDataTable _dt = _ds.Tables[0];
                                AddVirtualTrainingAttendee(_dt, pEntParams.ClientId, pEntParams.CurrentUserID);
                                string statuss = string.Empty;
                                if (_ds.Tables.Count > 1)
                                {
                                    statuss = Convert.ToString(_ds.Tables[1].Rows[0]["EventStatus_C"]);
                                }

                                if (statuss == "Pending")
                                {
                                    return "alert('" + MessageAdaptor.GetMessage("alert('" + MessageAdaptor.GetMessage(YPLMS.Services.Messages.UserSessionRegistration.Nominis_GreaterThan_MaxSessionRegistration) + "');") + "');";                                    
                                }
                            }
                        }
                    }
                    #endregion

                    if (entListBaseAct != null)
                    {
                        Learner eUser = new Learner();
                        eUser.ClientId = pEntParams.ClientId;

                        if (!string.IsNullOrEmpty(pEntParams.ActivtyId))
                            eUser.ParaActivityID = pEntParams.ActivtyId.Substring(0, pEntParams.ActivtyId.LastIndexOf(','));

                        if (!string.IsNullOrEmpty(pEntParams.BusinessRuleSelectedValue) && pEntParams.BusinessRuleSelectedValue != "0")
                        {
                            eUser.ParaRuleID = pEntParams.BusinessRuleSelectedValue.Trim();
                        }


                        //if (pEntParams.BusinessRuleSelectedValue != "0")
                        //    eUser.ParaRuleID = pEntParams.BusinessRuleSelectedValue.Trim();

                        if (!string.IsNullOrEmpty(pEntParams.SystemUserGuid))
                        {
                            string SystemUserGuid = pEntParams.SystemUserGuid.Substring(0, pEntParams.SystemUserGuid.LastIndexOf(","));
                            eUser.ID = SystemUserGuid;
                        }

                        if (pEntParams.IsReassignmentChecked)
                            eUser.IsReassign = "True";

                        List<Learner> entbaseList = entMgrLearner.Execute(eUser, Learner.ListMethod.OneTimeUserList);

                        for (int i = 0; i < entbaseList.Count; i++)
                        {
                            if (pEntParams.ActivtyId.Contains(entbaseList[i].ActivityId))
                            {
                                if (strActivityIdWithoutCompletion == string.Empty)
                                    strActivityIdWithoutCompletion = entbaseList[i].ActivityId;
                                else if (!strActivityIdWithoutCompletion.Contains(entbaseList[i].ActivityId))
                                    strActivityIdWithoutCompletion += "," + entbaseList[i].ActivityId;
                            }
                        }

                        pEntParams.ActivtyId = strActivityIdWithoutCompletion + ",";

                        List<Learner> entLearnerList = new List<Learner>();
                        ArrayList arrUserList = new ArrayList();

                        foreach (Learner entBase in entbaseList)
                        {
                            Learner entUSer = entBase;

                            if (!arrUserList.Contains(entUSer.ID.ToString()))
                            {
                                entLearnerList.Add(entUSer);
                                arrUserList.Add(entUSer.ID.ToString());

                                if (pEntParams.IsReassignmentChecked)
                                {
                                    string strContentSrvPath = FileHandler.COURSE_FOLDER_PATH + "/" + FileHandler.USER_DATAXML_PATH;

                                    FileHandler FtpUpload = new FileHandler(pEntParams.ClientId);

                                    if (pEntParams.ActivityType == ActivityContentType.Certification || pEntParams.ActivityType == ActivityContentType.Curriculum)
                                    {
                                        ActivityAssignment entInnerActivity = new ActivityAssignment();
                                        entInnerActivity.ID = pEntParams.ActivtyId.Substring(0, pEntParams.ActivtyId.LastIndexOf(','));
                                        entInnerActivity.ActivityTypeId = pEntParams.ActivityType.ToString();
                                        entInnerActivity.ClientId = pEntParams.ClientId;
                                        entInnerActivity.UserID = entUSer.ID.ToString();
                                        List<ActivityAssignment> entListInnerActivity = entMgrActivityAssignment.Execute(entInnerActivity, ActivityAssignment.ListMethod.GetInnerActivity);

                                        foreach (ActivityAssignment ss in entListInnerActivity)
                                        {
                                            string strActivity = ss.ID;
                                            string strXMLFilelePath = strContentSrvPath + "/" + pEntParams.ClientId + "/" + strActivity.Trim() + "/" + entUSer.ID.ToString();

                                            try
                                            {
                                                FtpUpload.RemoveFolder(strXMLFilelePath);
                                            }
                                            catch (Exception ex)
                                            {
                                                CustomException _expCustom = new CustomException(YPLMS.Services.Messages.PolicyLibrary.LIBRARY_DEL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, ex, true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string strXMLFilelePath = strContentSrvPath + "/" + pEntParams.ClientId + "/" + pEntParams.ActivtyId.Substring(0, pEntParams.ActivtyId.LastIndexOf(',')) + "/" + entUSer.ID.ToString();
                                        try
                                        {
                                            FtpUpload.RemoveFolder(strXMLFilelePath);
                                        }
                                        catch (Exception ex)
                                        {
                                            CustomException _expCustom = new CustomException(YPLMS.Services.Messages.PolicyLibrary.LIBRARY_DEL_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, ex, true);
                                        }
                                    }
                                }
                            }
                        }

                        if (pEntParams.IsMailChecked)
                        {
                            if (entLearnerList != null && entLearnerList.Count > 0)
                            {
                                string strLearnerIds = string.Join(",", entLearnerList.Select(x => x.ID));

                                if (pEntParams.IsAutoMail)
                                {
                                    SendEmail(entLearnerList, pEntParams.EmailTemplate, pEntParams.ActivtyId, strLearnerIds, pEntParams);
                                }
                                else if (pEntParams.EmailScheduleDelivery != null)
                                {
                                    EmailDeliveryDashboard entEmailDashBoard = null;
                                    EmailService mailMessage = new EmailService(pEntParams.ClientId);
                                    string strMailAddress = mailMessage.GetTOListFromLearners(entLearnerList);

                                    entEmailDashBoard.DistributionListId = null;
                                    entEmailDashBoard.ToList = strMailAddress;
                                    entEmailDashBoard.ActivityId = pEntParams.ActivtyId.Substring(0, pEntParams.ActivtyId.LastIndexOf(','));
                                    entEmailDashBoard.LearnerId = strLearnerIds;
                                    entEmailDashBoard.AssignmentMode = ActivityAssignmentMode.UI;
                                    entEmailDashBoard.DeliveryApprovalStatus = YPLMS.Services.Common.GetDescription(EmailDeliveryDashboard.ApprovalStatus.PendingApproval);

                                    if (pEntParams.BusinessRuleSelectedValue != "0")
                                    {
                                        entEmailDashBoard.RuleId = pEntParams.BusinessRuleSelectedValue;
                                        entEmailDashBoard.AssignmentTypeID = "UI_ONETINMEASSIGNMENT";
                                    }

                                    entMgrEmailDeliveryDashboard.Execute(entEmailDashBoard, EmailDeliveryDashboard.Method.Add);
                                }
                            }
                        }


                        return "alert('" + MessageAdaptor.GetMessage(YPLMS.Services.Messages.ActivityAssignment.ACTIVITY_ASSIGN_ADD_SUCCESS) + "');";
                    }
                }
                else
                {
                    if (!pEntParams.IsActivitySelected)
                        jscript = "alert('" + MessageAdaptor.GetMessage(YPLMS.Services.Messages.Assignment.SELECT_ACTIVITY_TO_ASSIGN) + "');";
                    else if (!pEntParams.IsLearnerSelected && (pEntParams.BusinessRuleSelectedIndex == 0 || pEntParams.BusinessRuleSelectedIndex == -1))
                        jscript = "alert('" + MessageAdaptor.GetMessage(YPLMS.Services.Messages.Assignment.SELECT_USER) + "');";
                    else if (!pEntParams.IsBusinessRuleSelected && !pEntParams.IsLearnerSelected)
                        jscript = "alert('Selected business rule not having any user(s)');";

                    if (!string.IsNullOrEmpty(jscript))
                    {
                        return "alert('"+ jscript + "');";                        
                    }
                }
            }
            catch (CustomException custEx)
            {
                strRedirectUrl = "../../errorpage.aspx?id=" + custEx.MessageId;
                return "alert('" + strRedirectUrl + "');";
                
            }

            
            return("");
        }

        private string SendEmail(List<Learner> pEntLearnerList, EmailTemplate pEntTemplate, string pActivityID, string pLearerIds, Entity.ActivityAssignment pEntParams)
        {
            try
            {
                bool bMailSend = false;
                List<ActivityAssignment> entActivityList = new();
                EmailService mailMessage = new EmailService(pEntParams.ClientId);

                if (pEntTemplate != null && !string.IsNullOrEmpty(pEntTemplate.ID))
                {
                    if (!string.IsNullOrEmpty(pActivityID.Trim()))
                    {
                        pActivityID = pActivityID.Substring(0, pActivityID.LastIndexOf(','));
                        string[] ActIDs = pActivityID.Split(',');

                        for (int sCount = 0; sCount < ActIDs.Length; sCount++)
                        {
                            ActivityAssignment entAssignedActID = new();

                            if (!string.IsNullOrEmpty(ActIDs[sCount]))
                            {
                                string[] LearnerIDs = pLearerIds.Split(',');
                                for (int j = 0; j < LearnerIDs.Length; j++)
                                {
                                    entAssignedActID.ID = ActIDs[sCount];
                                    entAssignedActID.ClientId = pEntParams.ClientId;
                                    entAssignedActID.UserID = LearnerIDs[j];
                                    entAssignedActID = entMgrActivityAssignment.Execute(entAssignedActID, ActivityAssignment.Method.Get);

                                    string strActivityType = entAssignedActID.ActivityType.ToString();
                                    if (strActivityType == ActivityContentType.AICC.ToString() ||
                                        strActivityType == ActivityContentType.NonCompliant.ToString() ||
                                        strActivityType == ActivityContentType.Scorm2004.ToString() ||
                                        strActivityType == ActivityContentType.Scorm12.ToString())
                                        entAssignedActID.ActivityType = ActivityContentType.Course;
                                    else if (strActivityType == ActivityContentType.Assessment.ToString())
                                        entAssignedActID.ActivityType = ActivityContentType.Assessment;
                                    else if (strActivityType == ActivityContentType.Asset.ToString())
                                        entAssignedActID.ActivityType = ActivityContentType.Asset;
                                    else if (strActivityType == ActivityContentType.Certification.ToString())
                                        entAssignedActID.ActivityType = ActivityContentType.Certification;
                                    else if (strActivityType == ActivityContentType.Classroom_Training.ToString())
                                        entAssignedActID.ActivityType = ActivityContentType.Classroom_Training;
                                    else if (strActivityType == ActivityContentType.Curriculum.ToString())
                                        entAssignedActID.ActivityType = ActivityContentType.Curriculum;
                                    else if (strActivityType == ActivityContentType.Policy.ToString())
                                        entAssignedActID.ActivityType = ActivityContentType.Policy;
                                    else if (strActivityType == ActivityContentType.Questionnaire.ToString())
                                        entAssignedActID.ActivityType = ActivityContentType.Questionnaire;
                                    else if (strActivityType == ActivityContentType.Virtual_Training.ToString())
                                        entAssignedActID.ActivityType = ActivityContentType.Virtual_Training;
                                }

                                entActivityList.Add(entAssignedActID);
                            }
                        }
                    }

                    EmailDeliveryDashboard entDelDashBoard = new();
                    Learner learner = new Learner();
                    if (learner.IsProductAdmin())
                    {
                        entDelDashBoard.AddToDashboard = false;
                    }
                    else
                    {
                        entDelDashBoard.AddToDashboard = pEntParams.IsDirectSendMail ? false : true;
                    }

                    entDelDashBoard.EmailTemplate = pEntTemplate;
                    entDelDashBoard.EmailDeliveryTitle = "One Time Assignment";
                    entDelDashBoard.CreatedById = pEntParams.CurrentUserID;
                    entDelDashBoard.IsPersonalized = true;
                    entDelDashBoard.LearnerId = pLearerIds;

                    if (pEntParams.BusinessRuleId != "0")
                    {
                        entDelDashBoard.RuleId = pEntParams.BusinessRuleId.ToString();
                        entDelDashBoard.AssignmentTypeID = "UI_ONETINMEASSIGNMENT";
                    }

                    List<System.Net.Mail.Attachment> pAttachment = new();
                    
                    bMailSend = mailMessage.SendPersonalizedAssignmentMail(pEntLearnerList, entDelDashBoard, null, entActivityList, null, pAttachment);

                    if (!bMailSend)
                    {
                        string msg1 = "Email sending failed.";
                        return "alert('" + msg1 + "');";                        
                    }
                }
                string msg = "Email sent successfully.";
                return "alert('" + msg + "');";                
            }
            catch (CustomException expCommon)
            {
                string _strMessageId = YPLMS.Services.Messages.FeedBack.UNABLE_TO_SEND_MAIL;
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
        }


        public void AddVirtualTrainingAttendee(SysDataTable _dt, string strClientId, string strCurrentUserId)
        {
            VirtualTrainingAttendeeMaster entVTAttendee = new VirtualTrainingAttendeeMaster();
            List<VirtualTrainingAttendeeMaster> entAttendeeList = new();
            List<VirtualTrainingAttendeeMaster> entAttendeeFailureList = new();
            bool IsMatch = false;
            //YPLMS2._0.API.DataAccessManager.BusinessManager.VirtualTraining.VirtualTrainingAttendeeManager entVTAttendeeManager = new YPLMS2._0.API.DataAccessManager.BusinessManager.VirtualTraining.VirtualTrainingAttendeeManager();
            VirtualTrainingAttendeeManager entVTAttendeeManager = new VirtualTrainingAttendeeManager();

            try
            {
                VirtualTrainingAttendeeMaster entVTAttendeeList;

                for (int i = 0; i < _dt.Rows.Count; i++)
                {
                    entVTAttendeeList = new VirtualTrainingAttendeeMaster();
                    entVTAttendee.TrainingID = Convert.ToString(_dt.Rows[i]["TrainingId"]);
                    entVTAttendee.SessionKey = Convert.ToString(_dt.Rows[i]["sessionKey"]);
                    entVTAttendeeList.SystemUserGUID = Convert.ToString(_dt.Rows[i]["systemuserguid"]);
                    entVTAttendeeList.ClientId = strClientId;
                    entVTAttendeeList.LastModifiedById = strCurrentUserId;
                    entVTAttendeeList.TrainingID = Convert.ToString(_dt.Rows[i]["TrainingId"]);
                    entVTAttendeeList.SessionKey = Convert.ToString(_dt.Rows[i]["sessionKey"]);
                    entVTAttendeeList.IsAdminAdded = true;

                    VirtualTrainingSessionMaster entVTSession = new VirtualTrainingSessionMaster
                    {
                        ClientId = strClientId,
                        ID = Convert.ToString(_dt.Rows[i]["TrainingId"]),
                        TrainingId = Convert.ToString(_dt.Rows[i]["TrainingId"])
                    };


                    VirtualTrainingSessionManager entVTSessionManager = new VirtualTrainingSessionManager();
                    entVTSession = entVTSessionManager.Execute(entVTSession, VirtualTrainingSessionMaster.Method.Get);

                    int Max = entVTSession.MaxRegistration;
                    int NoOfREgistred = Convert.ToInt32(entVTSession.NoOfRegistered);

                    if (entVTSession.IsSelfRegistration)
                    {
                        if (entVTSession.Autoapprove)
                        {
                            if (Max > NoOfREgistred || Max == 0)
                            {
                                entVTAttendeeList.Status = VirtualTrainingKeys.VTJOINSTATUS_ACCEPT;
                                strVirtualStatus = VirtualTrainingKeys.VTJOINSTATUS_ACCEPT;
                            }
                            else
                            {
                                entVTAttendeeList.Status = VirtualTrainingKeys.VTJOINSTATUS_WAITLIST;
                            }
                        }
                        else
                        {
                            entVTAttendeeList.Status = VirtualTrainingKeys.VTJOINSTATUS_REGISTER;
                            strVirtualStatus = VirtualTrainingKeys.VTJOINSTATUS_REGISTER;
                        }
                    }
                    else
                    {
                        entVTAttendeeList.Status = VirtualTrainingKeys.VTJOINSTATUS_INVITE;
                    }

                    if (GetTrainingSession(entVTAttendee.SessionKey, entVTSession.VirtualWebexSystemUserid, strClientId))
                    {
                        string userName = Convert.ToString(_dt.Rows[i]["FirstName"]) + ' ' + Convert.ToString(_dt.Rows[i]["LastName"]);
                        string userEmail = Convert.ToString(_dt.Rows[i]["EmailId"]);
                        bool selfregi = entVTSession.IsSelfRegistration;

                        if (AddAttendeeToSession(entVTAttendee.SessionKey, userName, userEmail, selfregi, entVTSession.VirtualWebexSystemUserid, strClientId))
                        {
                            IsMatch = true;
                            entVTAttendeeList.AttendeeID = Convert.ToString(strVirtualattendeeID);
                            entVTAttendeeList.RegisterID = Convert.ToString(strVirtualRegistrationID);
                            entVTAttendeeList.EmailId = userEmail;
                            entAttendeeList.Add(entVTAttendeeList);
                        }
                        else
                        {
                            IsMatch = false;
                            entAttendeeFailureList.Add(entVTAttendeeList);
                        }
                    }
                    else
                    {
                        IsMatch = false;
                    }

                    if (IsMatch && entVTAttendeeList.Status == VirtualTrainingKeys.VTJOINSTATUS_ACCEPT)
                    {
                        entVTSession.ClientId = strClientId;
                        entVTSession.NoOfRegistered = NoOfREgistred + 1;
                        entVTSession = entVTSessionManager.Execute(entVTSession, VirtualTrainingSessionMaster.Method.UpdateRegister);
                    }
                }

                int iTotalRowsAdded = 0;

                if (entAttendeeList.Count > 0)
                {
                    entAttendeeList = entVTAttendeeManager.Execute(entAttendeeList, VirtualTrainingAttendeeMaster.ListMethod.BulkUpdateVirtualTrainingAttendeeMaster);
                    if (entAttendeeList == null)
                    {
                        Exception ex = new Exception();
                        CustomException _expCustom = new CustomException(
                            YPLMS.Services.Messages.VirtualTrainingAttendeeMaster.VIRTUALTRAININGATTENDEEMASTER_DAM_ERROR,
                            CustomException.WhoCallsMe(),
                            ExceptionSeverityLevel.Information,
                            ex,
                            true
                        );
                    }
                }

                if (entAttendeeFailureList.Count > 0)
                {
                    entAttendeeFailureList = entVTAttendeeManager.Execute(entAttendeeFailureList, VirtualTrainingAttendeeMaster.ListMethod.BulkUpdateVirtualTrainingFailureList);
                }

                // ViewState / ScriptManager logic removed for .NET 8
                // You can return this data to frontend via an API response instead
            }
            catch (Exception ex)
            {
                // Optionally log exception
            }
        }


        public static bool GetTrainingSession(string sessionKey, string webexUserID, string strClientId)
        {
            string strXML = string.Empty;
            bool strReturn = false;
            StringBuilder sbParticipants;

            strXML += "<body>\r\n";
            strXML += "<bodyContent xsi:type=\"java:com.webex.service.binding.training.GetTrainingSession\">\r\n";
            strXML += "<sessionKey>" + sessionKey + "</sessionKey>\r\n";
            strXML += "</bodyContent>\r\n";
            strXML += "</body>\r\n";

            try
            {
                string responseFromServer = SendWebRequest(strXML, webexUserID, strClientId);

                if (responseFromServer.Contains("<serv:result>SUCCESS</serv:result>"))
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(responseFromServer);
                    MemoryStream stream = new MemoryStream(byteArray);

                    XPathDocument docAttendee = new XPathDocument(stream);
                    XPathNavigator nav = docAttendee.CreateNavigator();
                    XPathExpression expr = nav.Compile("//train:attendees/sess:participants/sess:participant");

                    XmlNamespaceManager context = new XmlNamespaceManager(nav.NameTable);
                    context.AddNamespace("serv", "http://www.webex.com/schemas/2002/06/service");
                    context.AddNamespace("train", "http://www.webex.com/schemas/2002/06/service/trainingsession");
                    context.AddNamespace("sess", "http://www.webex.com/schemas/2002/06/service/session");
                    context.AddNamespace("com", "http://www.webex.com/schemas/2002/06/common");
                    context.AddNamespace("qti", "http://www.webex.com/schemas/2002/06/service/trainingsessionqti");
                    context.AddNamespace("qtiasi", "http://www.webex.com/schemas/2002/06/service/trainingsessionqtiasi");
                    expr.SetContext(context);

                    XPathNodeIterator iterator = nav.Select(expr);
                    sbParticipants = new StringBuilder();
                    string strParticipantName = string.Empty;
                    string strParticipantEmail = string.Empty;

                    while (iterator.MoveNext())
                    {
                        XPathNavigator nav2 = iterator.Current.Clone();
                        XPathNodeIterator children = nav2.SelectChildren(XPathNodeType.Element);

                        sbParticipants.Append("<participant>\r\n");
                        while (children.MoveNext())
                        {
                            XPathNavigator nav3 = children.Current.Clone();

                            if (nav3.Name == "sess:person")
                            {
                                XPathNodeIterator personChildren = nav3.SelectChildren(XPathNodeType.Element);
                                while (personChildren.MoveNext())
                                {
                                    XPathNavigator nav4 = personChildren.Current.Clone();
                                    if (nav4.Name == "com:name")
                                        strParticipantName = nav4.Value;

                                    if (nav4.Name == "com:email")
                                        strParticipantEmail = nav4.Value;
                                }

                                sbParticipants.Append("<person>\r\n");
                                sbParticipants.Append("<name>" + strParticipantName + "</name>\r\n");
                                sbParticipants.Append("<email>" + strParticipantEmail + "</email>\r\n");
                                sbParticipants.Append("<type>VISITOR</type>\r\n");
                                sbParticipants.Append("</person>\r\n");

                                strParticipantName = string.Empty;
                                strParticipantEmail = string.Empty;
                            }

                            if (nav3.Name == "sess:role")
                            {
                                sbParticipants.Append("<role>" + nav3.Value + "</role>\r\n");
                            }
                        }
                        sbParticipants.Append("</participant>\r\n");
                    }

                    strReturn = true;
                }
                else
                {
                    strReturn = false;
                }
            }
            catch (CustomException)
            {
                // Optional: log error or rethrow if needed
            }

            return strReturn;
        }

        public static string SendWebRequest(string XMLBody, string WebexUserID, string strClientId)
        {
            string strWebRequestResponse = string.Empty;

            VirtualTrainingUserMaster entUSerMaster = new VirtualTrainingUserMaster();
            string _strMessageId = YPLMS.Services.Messages.VirtualTrainingSessionMaster.VIRTUALTRAININGSESSIONMASTER_WEBEXAPICALLINGERROR;
            StreamReader reader = null;
            Stream dataStream = null;
            WebResponse response = null;
            CustomException _expCustom = null;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                entUSerMaster = CommonData(WebexUserID, strClientId);
                if (!string.IsNullOrEmpty(entUSerMaster.XMLserver))
                {
                    WebRequest request = WebRequest.Create(entUSerMaster.XMLserver);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";

                    string strXML = string.Empty;

                    strXML += GetWebRequestXMLInputStart();
                    strXML += GetWebRequestXMLInputHeader(entUSerMaster);
                    strXML += XMLBody;
                    strXML += GetWebRequestXMLInputEnd();

                    byte[] byteArray = Encoding.UTF8.GetBytes(strXML);
                    request.ContentLength = byteArray.Length;

                    dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();

                    response = request.GetResponse();
                    dataStream = response.GetResponseStream();
                    reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();

                    if (responseFromServer.Contains("<serv:result>SUCCESS</serv:result>"))
                    {
                        strWebRequestResponse = responseFromServer;
                    }
                    else
                    {
                        byteArray = Encoding.UTF8.GetBytes(responseFromServer);
                        MemoryStream stream = new MemoryStream(byteArray);
                        XPathDocument docAttendee = new XPathDocument(stream);

                        XPathNavigator nav = docAttendee.CreateNavigator();
                        XPathExpression Expr = nav.Compile("//serv:response/serv:reason");
                        XmlNamespaceManager context = new XmlNamespaceManager(nav.NameTable);
                        context.AddNamespace("serv", "http://www.webex.com/schemas/2002/06/service");
                        context.AddNamespace("train", "http://www.webex.com/schemas/2002/06/service/trainingsession");
                        context.AddNamespace("sess", "http://www.webex.com/schemas/2002/06/service/session");
                        context.AddNamespace("com", "http://www.webex.com/schemas/2002/06/common");
                        context.AddNamespace("qti", "http://www.webex.com/schemas/2002/06/service/trainingsessionqti");
                        context.AddNamespace("qtiasi", "http://www.webex.com/schemas/2002/06/service/trainingsessionqtiasi");
                        Expr.SetContext(context);

                        XPathNodeIterator Iterator = nav.Select(Expr);
                        string Message = string.Empty;
                        while (Iterator.MoveNext())
                        {
                            XPathNavigator nav2 = Iterator.Current.Clone();
                            Message = nav2.Value;
                        }

                        Exception newexpCommon = new Exception();
                        _expCustom = new CustomException(Message, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, newexpCommon, true);
                        strWebRequestResponse = Message;
                    }

                    reader.Close();
                    dataStream.Close();
                    response.Close();
                }
                else
                {
                    // WebEx credentials not available.
                }
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (dataStream != null)
                    dataStream.Close();
                if (response != null)
                    response.Close();
            }

            return strWebRequestResponse;
        }

        public static VirtualTrainingUserMaster CommonData(string webexUserID, string strClientId)
        {
            VirtualTrainingUserMaster entWebexUserData = new VirtualTrainingUserMaster();
            VirtualTrainingUserManager _entMgrWebexUserMaster = new VirtualTrainingUserManager();

            string _strSelectedClientId = string.Empty;

            //if (httpContextAccessor.HttpContext?.Session != null &&
            //    httpContextAccessor.HttpContext.Session.TryGetValue(Client.CLIENT_SESSION_ID, out byte[] sessionValue))
            //{
            //    _strSelectedClientId = Encoding.UTF8.GetString(sessionValue);
            //}

            entWebexUserData.ClientId = strClientId;
            entWebexUserData.ID = webexUserID;

            entWebexUserData = _entMgrWebexUserMaster.Execute(entWebexUserData, VirtualTrainingUserMaster.Method.Get);

            return entWebexUserData;
        }

        public static string GetWebRequestXMLInputStart()
        {
            StringBuilder strStart = new StringBuilder();
            strStart.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>\r\n");
            strStart.Append("<serv:message xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\r\n");
            return Convert.ToString(strStart);
        }

        public static string GetWebRequestXMLInputHeader(VirtualTrainingUserMaster entUSerMaster)
        {
            //VirtualTrainingUserMaster entUSerMaster = new VirtualTrainingUserMaster();

            // entUSerMaster = CommonData(WebexUserID);

            StringBuilder strHeader = new StringBuilder();
            strHeader.Append("<header>\r\n");
            strHeader.Append("<securityContext>\r\n");
            strHeader.Append("<webExID>" + entUSerMaster.Userid + "</webExID>\r\n");
            strHeader.Append("<password>" + YPLMS.Services.EncryptionManager.Decrypt(entUSerMaster.Password) + "</password>\r\n");
            strHeader.Append("<siteID>" + entUSerMaster.TrainingUserSiteId + "</siteID>\r\n");
            strHeader.Append("<partnerID>" + entUSerMaster.TrainingUserPartnerId + "</partnerID>\r\n");
            strHeader.Append("<email>" + entUSerMaster.EmailId + "</email>\r\n");
            strHeader.Append("</securityContext>\r\n");
            strHeader.Append("</header>\r\n");
            return Convert.ToString(strHeader);
        }

        public static string GetWebRequestXMLInputEnd()
        {
            string strEnd = string.Empty;
            strEnd += "</serv:message>";
            return strEnd;
        }

        public static bool AddAttendeeToSession(string sessionKey, string userName, string userEmail, bool selregi, string webexUserID, string strClientId)
        {
            string strXML = string.Empty;
            string strName = string.Empty;
            string strEmail = string.Empty;

            strName = userName;
            strEmail = userEmail;

            if (selregi)
            {
                strXML += "<body>\r\n";
                strXML += "<bodyContent xsi:type=\"java:com.webex.service.binding.attendee.RegisterMeetingAttendee\">\r\n";
                strXML += "<attendees>\r\n";
                strXML += "<person>\r\n";
                strXML += "<name>" + strName + "</name>\r\n";
                strXML += "<title>title</title>\r\n";
                strXML += "<company>microsoft</company>\r\n";
                strXML += "<address>\r\n";
                strXML += "<addressType>PERSONAL</addressType>\r\n";
                strXML += "<city>sz</city>\r\n";
                strXML += "<country>India</country>\r\n";
                strXML += "</address>\r\n";
                strXML += "<email>" + strEmail + "</email>\r\n";
                strXML += "<notes>notes</notes>\r\n";
                strXML += "<url>https://</url>\r\n";
                strXML += "<type>VISITOR</type>\r\n";
                strXML += "</person>\r\n";
                strXML += "<joinStatus>" + strVirtualStatus + "</joinStatus>\r\n";
                strXML += "<role>ATTENDEE</role>\r\n";
                strXML += "<emailInvitations>true</emailInvitations>\r\n";
                strXML += "<sessionKey>" + sessionKey + "</sessionKey>\r\n";
                strXML += "</attendees>\r\n";
                strXML += "</bodyContent>\r\n";
                strXML += "</body>\r\n";

            }
            else
            {
                strXML += "<body>\r\n";
                strXML += "<bodyContent xsi:type=\"java:com.webex.service.binding.attendee.CreateMeetingAttendee\">\r\n";

                strXML += "<person>\r\n";
                strXML += "<name>" + strName + "</name>\r\n";
                strXML += "<title>title</title>\r\n";
                strXML += "<company>microsoft</company>\r\n";
                strXML += "<address>\r\n";
                strXML += "<addressType>PERSONAL</addressType>\r\n";
                strXML += "<city>sz</city>\r\n";
                strXML += "<country>china</country>\r\n";
                strXML += "</address>\r\n";
                strXML += "<email>" + strEmail + "</email>\r\n";
                strXML += "<notes>notes</notes>\r\n";
                strXML += "<url>https://</url>\r\n";
                strXML += "<type>VISITOR</type>\r\n";
                strXML += "</person>\r\n";
                strXML += "<role>ATTENDEE</role>\r\n";
                strXML += "<emailInvitations>true</emailInvitations>\r\n";
                strXML += "<sessionKey>" + sessionKey + "</sessionKey>\r\n";
                strXML += "</bodyContent>\r\n";
                strXML += "</body>\r\n";
            }

            string responseFromServer = SendWebRequest(strXML, webexUserID, strClientId);
            // Display the content.
            if (responseFromServer.Contains("<serv:result>SUCCESS</serv:result>"))
            {
                // convert string to stream
                byte[] byteArray = Encoding.UTF8.GetBytes(responseFromServer);
                //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
                MemoryStream stream = new MemoryStream(byteArray);

                XPathDocument docAttendee = new XPathDocument(stream);

                XPathNavigator nav = docAttendee.CreateNavigator();

                if (selregi)
                {
                    XPathExpression Expr = nav.Compile("//att:register");
                    XmlNamespaceManager context = new XmlNamespaceManager(nav.NameTable);
                    context.AddNamespace("serv", "http://www.webex.com/schemas/2002/06/service");
                    context.AddNamespace("com", "http://www.webex.com/schemas/2002/06/common");
                    context.AddNamespace("att", "http://www.webex.com/schemas/2002/06/service/attendee");

                    Expr.SetContext(context);
                    XPathNodeIterator Iterator = nav.Select(Expr);
                    string strAttendeeID = string.Empty;

                    while (Iterator.MoveNext())
                    {
                        XPathNavigator nav2 = Iterator.Current.Clone();
                        XPathNodeType oXPathNodeType = XPathNodeType.Element;
                        XPathNodeIterator Iterator2 = nav2.SelectChildren(oXPathNodeType);


                        while (Iterator2.MoveNext())
                        {
                            XPathNavigator nav3 = Iterator2.Current.Clone();
                            switch (nav3.Name)
                            {
                                case "att:attendeeID":
                                    {
                                        strVirtualattendeeID = nav3.Value;
                                    }
                                    break;
                                case "att:registerID":
                                    {
                                        strVirtualRegistrationID = nav3.Value;
                                    }
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    XPathExpression Expr = nav.Compile("//att:attendeeId");
                    XmlNamespaceManager context = new XmlNamespaceManager(nav.NameTable);
                    context.AddNamespace("serv", "http://www.webex.com/schemas/2002/06/service");
                    context.AddNamespace("com", "http://www.webex.com/schemas/2002/06/common");
                    context.AddNamespace("att", "http://www.webex.com/schemas/2002/06/service/attendee");

                    Expr.SetContext(context);
                    XPathNodeIterator Iterator = nav.Select(Expr);
                    string strAttendeeID = string.Empty;

                    while (Iterator.MoveNext())
                    {
                        XPathNavigator nav2 = Iterator.Current.Clone();

                        if (nav2.Name == "att:attendeeId")
                        {
                            //strAttendeeID = nav2.Value;
                            strVirtualattendeeID = nav2.Value;
                        }
                    }
                }

                return true;
            }
            else
            {
                strVirtualresponseFromServer = responseFromServer;
                return false;
            }
        }

        public static bool IsPageValid(SaveAssignmentInputModel input, ref string lblAssDtError, ref string lblDueDtError, ref string lblExpDtError)
        {
            bool isValid = true;
            bool isTempValid = true;

            try
            {
                string pValDateErrorMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ActivityAssignment.ENTER_VALID_DATE);
                AssignmentParameters entParam = new AssignmentParameters
                {
                    ValDateErrorMsg = pValDateErrorMsg,
                    ZeroMsg = API.YPLMS.Services.Messages.Assignment.ENTER_VALID_ASSIGNMENT_DAYS,
                    ReqDateErrorMsg = API.YPLMS.Services.Messages.ActivityAssignment.ENTER_ASSIGNMENT_DATE,
                    ReqDaysErrorMsg = API.YPLMS.Services.Messages.ActivityAssignment.ENTER_DAYS_FOR_ASSIGNMENT,
                    ScriptKey = "AsDate"
                };

                isTempValid = BaseDateValidator(input.ddlAssignmentDate, input.txtDefaultAssignmnetDays, input.txtAssignmentDays, ref lblAssDtError, entParam);
                isValid = isValid && isTempValid;

                if (isTempValid)
                {
                    if (!input.rbNoDueDate)
                    {
                        entParam.ZeroMsg = API.YPLMS.Services.Messages.Assignment.ENTER_VALID_DUE_DAYS;
                        entParam.ReqDateErrorMsg = API.YPLMS.Services.Messages.ActivityAssignment.ENT_ASSIGN_DUE_DATE;
                        entParam.ReqDaysErrorMsg = API.YPLMS.Services.Messages.ActivityAssignment.ENT_ASSIGN_DUE_DAYS;
                        entParam.ScriptKey = "DueDate";

                        isTempValid = BaseDateValidator(input.ddlDueDate, input.txtDefaultDueDays, input.txtDueDays, ref lblDueDtError, entParam);
                        isValid = isValid && isTempValid;
                    }

                    if (!input.rbNoExpiryDate)
                    {
                        entParam.ZeroMsg = API.YPLMS.Services.Messages.Assignment.ENTER_VALID_EXPIRY_DAYS;
                        entParam.ReqDateErrorMsg = API.YPLMS.Services.Messages.ActivityAssignment.ENT_ASSIGN_EXPIRY_DATE;
                        entParam.ReqDaysErrorMsg = API.YPLMS.Services.Messages.ActivityAssignment.ENT_ASSIGN_EXPIRY_DAYS;
                        entParam.ScriptKey = "ExpDate";

                        isTempValid = BaseDateValidator(input.ddlExprDate, input.txtDefaultExpDays, input.txtExprDays, ref lblExpDtError, entParam);
                        isValid = isValid && isTempValid;
                    }
                }
            }
            catch (CustomException custEx)
            {
                // Handle logging or redirect logic as needed
                string strRedirectUrl = "../../errorpage.aspx?id=" + custEx.MessageId;
                // You can optionally log or return this value in response instead of redirecting
            }

            return isValid;
        }

        private static bool BaseDateValidator(string selectedIndex, string absoluteValue, string relativeValue, ref string errorMessage, AssignmentParameters pEntParams)
        {
            // If selectedIndex is "0", it's considered Absolute Date
            if (selectedIndex == "0")
            {
                string reqDateErrorMsg = MessageAdaptor.GetMessage(pEntParams.ReqDateErrorMsg);

                if (string.IsNullOrWhiteSpace(absoluteValue))
                {
                    errorMessage = reqDateErrorMsg;
                    return false;
                }
                else
                {
                    errorMessage = string.Empty;
                }
            }
            else // Relative Date
            {
                string regErrorMessage = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ActivityAssignment.ENTER_POSITIVE_NUMBERS);
                string reqDaysErrorMsg = MessageAdaptor.GetMessage(pEntParams.ReqDaysErrorMsg);

                if (string.IsNullOrWhiteSpace(relativeValue))
                {
                    errorMessage = reqDaysErrorMsg;
                    return false;
                }

                try
                {
                    int days = Convert.ToInt32(relativeValue.Trim());

                    if (days < 0)
                    {
                        errorMessage = MessageAdaptor.GetMessage(pEntParams.ZeroMsg);
                        return false;
                    }
                    else
                    {
                        errorMessage = string.Empty;
                    }
                }
                catch
                {
                    errorMessage = regErrorMessage;
                    return false;
                }
            }

            return true;
        }

        
        public static bool IsDatesValids(SaveAssignmentInputModel input, out string alertMessage)
        {
            try
            {
                bool isDaysValid = false;
                alertMessage = string.Empty;

                // Absolute Assignment Date
                if (input.ddlAssignmentDate == "0")
                {
                    if (input.ddlDueDate == "0" && !input.rbNoDueDate)
                    {
                        try
                        {
                            if (!DatesValidations(Convert.ToInt32(input.txtDefaultDueDays.Trim()), Convert.ToInt32(input.txtDefaultAssignmnetDays.Trim())))
                            {
                                alertMessage = (MessageAdaptor.GetMessage(YPLMS.Services.Messages.ActivityAssignment.DUE_DT_MUSTBE_GE_ASSIGN_DT));
                                return false;
                            }
                        }
                        catch
                        {
                            alertMessage = (MessageAdaptor.GetMessage(YPLMS.Services.Messages.ActivityAssignment.DUE_DT_MUSTBE_GE_ASSIGN_DT));
                            return false;
                        }
                    }
                }

                // Relative Due Date
                if (input.ddlDueDate != "0" && !input.rbNoDueDate)
                {
                    string alert;
                    isDaysValid = BaseDaysValidator(
                        input.ddlAssignmentDate,
                        input.ddlDueDate,
                        input.txtAssignmentDays,
                    input.txtDueDays,
                        YPLMS.Services.Messages.ActivityAssignment.DUE_DT_MUSTBE_GE_ASSIGN_DT, out alert
                    );

                    if (isDaysValid == false)
                    {
                        alertMessage = alert;
                        return false;
                    }
                    else
                        return true;

                    //if (!isDaysValid)
                    //    alertMessage = alert;
                    //return false;
                }

                // Absolute Due → Absolute Expiry
                if (input.ddlDueDate == "0" && !input.rbNoDueDate)
                {
                    if (input.ddlExprDate == "0" && !input.rbNoExpiryDate)
                    {
                        try
                        {
                            if (!DatesValidations(Convert.ToInt32(input.txtDefaultExpDays.Trim()), Convert.ToInt32(input.txtDefaultDueDays.Trim())))
                            {
                                alertMessage = (MessageAdaptor.GetMessage(YPLMS.Services.Messages.ActivityAssignment.EXPIRY_DT_MUSTBE_GE_DUE_DT));
                                return false;
                            }
                        }
                        catch
                        {
                            alertMessage = (MessageAdaptor.GetMessage(YPLMS.Services.Messages.ActivityAssignment.EXPIRY_DT_MUSTBE_GE_DUE_DT));
                            return false;
                        }
                    }
                }

                // Relative Expiry vs Relative Due
                if (input.ddlExprDate != "0" && !input.rbNoExpiryDate)
                {
                    string alert;
                    if (input.ddlDueDate != "0" && !input.rbNoDueDate)
                    {
                        isDaysValid = BaseDaysValidator(
                            input.ddlDueDate,
                            input.ddlExprDate,
                            input.txtDueDays,
                            input.txtExprDays,
                            YPLMS.Services.Messages.ActivityAssignment.EXPIRY_DT_MUSTBE_GE_DUE_DT, out alert
                        );

                        if (!isDaysValid)
                            alertMessage = alert;
                        return false;
                    }
                }

                // Assignment vs Expiry (when no due date)
                if (input.rbNoDueDate && !input.rbNoExpiryDate)
                {
                    // Invalid: Expiry can't be relative to Due Date if due is disabled
                    if (input.ddlExprDate == "2")
                    {
                        alertMessage = (MessageAdaptor.GetMessage(YPLMS.Services.Messages.Assignment.ASSGNMT_EXP_DT_CNT_B_REL_ASSGNMT_DU_DT));
                        return false;
                    }

                    // Absolute validation
                    else if (input.ddlAssignmentDate == "0" && input.ddlExprDate == "0")
                    {
                        if (!DatesValidations(Convert.ToInt32(input.txtDefaultExpDays.Trim()), Convert.ToInt32(input.txtDefaultAssignmnetDays.Trim())))
                        {
                            alertMessage = (MessageAdaptor.GetMessage(YPLMS.Services.Messages.Assignment.ASSGNMT_EXP_DT_MUST_B_GRTR_ASSGNMT_DT));
                            return false;
                        }
                    }
                }
            }
            catch (CustomException custEx)
            {
                string strRedirectUrl = "../../errorpage.aspx?id=" + custEx.MessageId;
                // log or return instead of Response.Redirect
            }
            alertMessage = null;
            return true;
        }

        private static bool BaseDaysValidator(string toCompType, string toValidateType, string toCompText, string toValidateText, string msg, out string alertMessage)
        {
            alertMessage = string.Empty;

            // base function
            // check whether relative dates (days) are valid or not
            if (toCompType == toValidateType)
            {
                // compare relative days for dates based on same relative to date
                if (Convert.ToInt32(toCompText.Trim()) > Convert.ToInt32(toValidateText.Trim()))
                {
                    alertMessage = MessageAdaptor.GetMessage(msg);
                    return false;
                }
            }

            return true;
        }


        //private static bool BaseDaysValidator(string baseSelector, string compareSelector, string baseValue, string compareValue, string msgKey)
        //{
        //    string msg = null;
        //    if (baseSelector == compareSelector)
        //    {
        //        if (int.TryParse(baseValue?.Trim(), out int baseVal) &&
        //            int.TryParse(compareValue?.Trim(), out int compareVal))
        //        {
        //            if (baseVal > compareVal)
        //            {
                        
        //                msg = (MessageAdaptor.GetMessage(msgKey));
        //                bool result = AlertCall(msg);
        //            }
        //        }
        //        else
        //        {
        //            AlertCall(MessageAdaptor.GetMessage(YPLMS.Services.Messages.ActivityAssignment.ENTER_POSITIVE_NUMBERS));
        //            return false;
        //        }
        //    }
        //    msg = null;
        //    return true;
        //}

        private static bool DatesValidations(int date1, int date2)
        {
            return date1 >= date2;
        }
    }
}
