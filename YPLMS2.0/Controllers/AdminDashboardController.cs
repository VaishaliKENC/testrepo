using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity.ViewModel;
using YPLMS2._0.API.Entity;
using System.Data;
using YPLMS2._0.API.YPLMS.Services;
using YPLMS2._0.API.DataAccessManager.BusinessManager;
using Microsoft.AspNetCore.Authorization;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
       private readonly IMapper _mapper;
       private readonly Learner _entCurrentUser = new Learner();
       private readonly LearnerManager _learnerManager = new LearnerManager();
       private readonly IAdminFeaturesAdaptor<AdminFeatures> _adminFeaturesAdaptor;
       private readonly ILearnerDAM<Learner> _learnerdam;

       public string _strTotalUsers = string.Empty;
       public string _strActiveUsers = string.Empty;
       public string _strTotalCourses = string.Empty;
       public string _strTotalCurriculums = string.Empty;
       public string _strTotalClients = string.Empty;

        DataSet _dset = new DataSet();
        CustomException _expCustom = null;

        public AdminDashboardController(IMapper mapper, IAdminFeaturesAdaptor<AdminFeatures> adminFeaturesAdaptor, ILearnerDAM<Learner> learnerdam)
        {
            _mapper = mapper;
            _adminFeaturesAdaptor = adminFeaturesAdaptor;
            _learnerdam = learnerdam;
        }

        [HttpGet]
        [Route("getadmindashboardcountdata")]
        [Authorize]
        public async Task<IActionResult> GetAdminDashboard(string ClientId, string CurrentUserID)
        {
            try
            {
                bool bStatistics = false;
                _dset = null;
                Report entStandardReport = new Report();
                ReportManager mgrReport = new ReportManager();
                //-- If Super Admin then no need to pass Client Id
                AdminFeatures entAdminFeature = new AdminFeatures();
                Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = CurrentUserID, ClientId = ClientId });
                entAdminFeature.ID = AdminFeatures.FEA_ID_SITE_STATISTICS;
                entAdminFeature.ClientId = ClientId;
                var adminFeatures = _adminFeaturesAdaptor.GetFeatureByID(entAdminFeature);
                if (entAdminFeature != null && entAdminFeature.CanView(_entCurrentUser))
                {
                    bStatistics = true;
                }
                if (_entCurrentUser.IsContentAdmin())
                {
                    bStatistics = true;

                }
                // if (ClientId != CommonManager.BaseClientId)
                {
                    entStandardReport.ClientId = ClientId;
                    entStandardReport.AddParameter(API.Entity.ReportSchema.Common.PARA_CLIENT_ID, ReportDataType.VarChar, ClientId);
                    if (!(_entCurrentUser.IsSiteAdmin() || _entCurrentUser.IsSuperAdmin()))
                        entStandardReport.AddParameter(API.Entity.ReportSchema.Common.PARA_CREATED_BY, ReportDataType.VarChar, CurrentUserID);
                }
                //if (bStatistics)
                {
                    _dset = mgrReport.ExecuteDataSet(entStandardReport, Report.ListMethod.Statistics);
                }

                if (_dset !=null &&_dset.Tables.Count > 0 && _dset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in _dset.Tables[0].Rows)
                    {
                        switch (Convert.ToString(dr["ItemKey"]))
                        {
                            case "Total Users":
                                {
                                    _strTotalUsers = Convert.ToString(dr["CountValue"]);
                                    break;
                                }
                            case "Active Users":
                                {
                                    _strActiveUsers = Convert.ToString(dr["CountValue"]);
                                    break;
                                }
                            case "Total Courses":
                                {
                                    _strTotalCourses = Convert.ToString(dr["CountValue"]);
                                    break;
                                }
                            case "Total Curriculum":
                                {
                                    _strTotalCurriculums = Convert.ToString(dr["CountValue"]);
                                    break;
                                }
                            case "Total Clients":
                                {
                                    if (_entCurrentUser.IsSuperAdmin())
                                        if (Convert.ToString(dr["ItemKey"]) == "Total Clients")
                                        {
                                            _strTotalClients = Convert.ToString(dr["CountValue"]);
                                           // divTotalClient.Visible = true;
                                           // divTotalCurriculums.Visible = false;
                                        }
                                    break;
                                }
                        }
                    }
                }                

            }
            catch (CustomException expCommon)
            {
                _expCustom = new CustomException(API.YPLMS.Services.Messages.Common.EXECUTION_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            if (_dset!=null && _dset.Tables.Count > 0 && _dset.Tables[0].Rows.Count > 0)
            {
                return Ok(new { Code = 200, TotalUsers = _strTotalUsers, ActiveUsers = _strActiveUsers, TotalCourses = _strTotalCourses, TotalCurriculums = _strTotalCurriculums });
            }
            else
            {
                return BadRequest( new { Code = 404, Msg = "No data found"  });
            }

        }

        [HttpGet]
        [Route("getrecentassignment")]
        [Authorize]
        public async Task<IActionResult> GetRecentAssignment(string ClientId, string CurrentUserID)
        {
            try
            {

                _dset = null;
                Report entStandardReport = new Report();
                ReportManager mgrReport = new ReportManager();

                //Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = CurrentUserID, ClientId = ClientId });

                entStandardReport.ClientId = ClientId;
               // if (!_entCurrentUser.IsSuperAdmin() && !_entCurrentUser.IsSiteAdmin())
                {
                    entStandardReport.AddParameter(API.Entity.ReportSchema.Common.PARA_CREATED_BY, ReportDataType.VarChar, CurrentUserID);
                }
                //_dset = mgrReport.ExecuteDataSet(entStandardReport, Report.ListMethod.CourseAssignmentAdminHome);
                _dset = mgrReport.ExecuteDataSet(entStandardReport, Report.ListMethod.RecentlyAccessedActivityCompletionGraph);
                // _dset = mgrReport.ExecuteDataSet(entStandardReport, Report.ListMethod.ActivityCompletionGraphDashboard);
               // _dset = mgrReport.ExecuteDataSet(entStandardReport, Report.ListMethod.GroupwiseAssignmentsCompletionGraph);


                //dtable =
                // CommonMethods.GetDataTable(new[]
                //                   {
                //                         "ID", "ActivityName", "AssignmentDateSet", "Int32:TotalAssigned",
                //                         "Int32:TotalCompleted", "CompletionPercentage" //, "ViewAssign"
                //                   });

                //if (_dset.Tables.Count > 0)
                //{
                //    foreach (DataRow dataRow in _dset.Tables[0].Rows)
                //    {
                //        Int32 iAssignUsers = 0;
                //        Int32 iCompletedUsers = 0;

                //        DataRow dtNewTableRow = dtable.NewRow();

                //        string strCoId = Convert.ToString(dataRow["ActivityId"]);
                //        string strActType = EncryptionManager.Encrypt(ActivityContentType.Course.ToString()).Replace("=", "%3D");
                //        if (strCoId != "")
                //        {
                //            //string sURLEdit = "../Assignment/GroupEditAssignments.aspx?ACTID=" + (EncryptionManager.Encrypt(Convert.ToString(dataRow["ActivityId"]))).Replace("=", "%3D") + "&ACTY=" + strActType;
                //            //dtNewTableRow["ViewEdit"] = dtNewTableRow["ViewEdit"] + "<a  href=\"" + CommonMethods.GetPageNameAndQueryString(sURLEdit) + "\">" + "<i class='zmdi zmdi-view-list-alt zmdi-hc-fw'></i>" + "</a>";

                //            //dtNewTableRow["ViewEdit"] = dtNewTableRow["ViewEdit"] + "<i class='zmdi zmdi-view-list-alt zmdi-hc-fw'></i>";
                //        }

                //        string strActivityName = Convert.ToString(dataRow["ActivityName"]);
                //        dtNewTableRow["ID"] = strCoId;

                //        //if (strActivityName.Length > 19)
                //        //{
                //        //    string ActivityName = strActivityName.Substring(0, 19) + "...";
                //        //    dtNewTableRow["ActivityName"] = "<p title='" + strActivityName + "'>" + ActivityName + "</p>";

                //        //}
                //        //else
                //        //{
                //        //    dtNewTableRow["ActivityName"] = "<p title='" + strActivityName + "'>" + strActivityName + "</p>";
                //        //}
                //        if (!String.IsNullOrEmpty(strActivityName))
                //        {
                //            dtNewTableRow["ActivityName"] = strActivityName;
                //        }


                //        if (DateTime.MinValue.CompareTo(Convert.ToDateTime(dataRow["AssignmentDateSet"])) < 0)
                //            dtNewTableRow["AssignmentDateSet"] =  Convert.ToDateTime(dataRow["AssignmentDateSet"]);

                //        if (dataRow["TotalAssigned"] != null)
                //        {
                //            iAssignUsers = Convert.ToInt32(dataRow["TotalAssigned"]);
                //        }
                //        dtNewTableRow["TotalAssigned"] = iAssignUsers;

                //        if (dataRow["TotalCompleted"] != null)
                //        {
                //            iCompletedUsers = Convert.ToInt32(dataRow["TotalCompleted"]);
                //        }
                //        dtNewTableRow["TotalCompleted"] = iCompletedUsers;

                //        if (!string.IsNullOrEmpty(Convert.ToString(dataRow["CompletionPercentage"])))
                //            dtNewTableRow["CompletionPercentage"] = Convert.ToDecimal(Math.Round(Convert.ToDouble(dataRow["CompletionPercentage"]))) + "%";

                //        //if (strCoId != "")
                //        //{
                //            //string sURL = "../Assignment/ShowAssignedUser.aspx?ACTID=" + (EncryptionManager.Encrypt(Convert.ToString(dataRow["ActivityId"]))).Replace("=", "%3D") + "&ACTY=" + strActType;
                //            //string sURLEdit = "../Assignment/GroupEditAssignments.aspx?ACTID=" + (EncryptionManager.Encrypt(Convert.ToString(dataRow["ActivityId"]))).Replace("=", "%3D") + "&ACTY=" + strActType;
                //            //dtNewTableRow["ViewAssign"] = "<a class='view' href='#' onClick=javascript:ShowUsers('" + sURL + "'); title='View'>" + "<i class='zmdi zmdi-eye zmdi-hc-fw'></i>" + "</a>";
                //            //dtNewTableRow["ViewAssign"] = dtNewTableRow["ViewAssign"] + "<a class='view' href=\"" + CommonMethods.GetPageNameAndQueryString(sURLEdit) + "\" title='Edit'>" + "<i class='fa fa-edit'></i>" + "</a>"; ;
                //        //}
                //        dtable.Rows.Add(dtNewTableRow);
                //    }
                //}

            }
            catch (CustomException expCommon)
            {
                _expCustom = new CustomException(API.YPLMS.Services.Messages.Common.EXECUTION_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            if (_dset != null && _dset.Tables[0].Rows.Count > 0)
            {
                return Ok(new { Code = 200, RecentAssignmentdata = _dset.Tables });
            }
            else
            {
                return BadRequest(new { Code = 404, Msg = "No data found" });
            }

        }


        [HttpGet]
        [Route("gettopassignment")]
        [Authorize]
        public async Task<IActionResult> GetTopAssignment(string ClientId, string CurrentUserID)
        {
            try
            {

                _dset = null;
                Report entStandardReport = new Report();
                ReportManager mgrReport = new ReportManager();

                //Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = CurrentUserID, ClientId = ClientId });

                entStandardReport.ClientId = ClientId;
                // if (!_entCurrentUser.IsSuperAdmin() && !_entCurrentUser.IsSiteAdmin())
                {
                    entStandardReport.AddParameter(API.Entity.ReportSchema.Common.PARA_CREATED_BY, ReportDataType.VarChar, CurrentUserID);
                }
                //_dset = mgrReport.ExecuteDataSet(entStandardReport, Report.ListMethod.CourseAssignmentAdminHome);
                _dset = mgrReport.ExecuteDataSet(entStandardReport, Report.ListMethod.ActivityCompletionGraphDashboard);               

            }
            catch (CustomException expCommon)
            {
                _expCustom = new CustomException(API.YPLMS.Services.Messages.Common.EXECUTION_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true);
                throw _expCustom;
            }
            if (_dset != null && _dset.Tables[0].Rows.Count > 0)
            {
                return Ok(new { Code = 200, TopAssignmentdata = _dset.Tables });
            }
            else
            {
                return BadRequest(new { Code = 404, Msg = "No data found" });
            }

        }

    }
}
