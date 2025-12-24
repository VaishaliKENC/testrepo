using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Tls;
using System.Collections;
using System.Data;
using System.Text.Json;
using System.Text.RegularExpressions;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Assignment;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.Entity.ReportSchema;
using YPLMS2._0.API.Entity.ViewModel;
using Learner = YPLMS2._0.API.Entity.ReportSchema.Learner;

namespace YPLMS2._0.Controllers.Learners
{
    [Route("[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private string _strDefaultLanguage = Language.SYSTEM_DEFAULT_LANG_ID;

        [HttpPost]
        [Route("getlearnerdashboard")]
        [Authorize]
        public async Task<IActionResult> GetLearnerDashboard(string ClientId, string UserId)
        {
            try
            {
                /*** Get Course Count & Curriculum Percentage ***/
                ActivityAssignment objActivityAssignment = new ActivityAssignment();
                objActivityAssignment.ClientId = ClientId;
                objActivityAssignment.UserID = UserId;

                ActivityAssignmentManager objActivityAssignmentManager = new ActivityAssignmentManager();
                objActivityAssignment = objActivityAssignmentManager.Execute(objActivityAssignment, ActivityAssignment.Method.GetCoursesCount);

                if (objActivityAssignment.TotalRowsAssigned != null)
                {
                    return Ok(new { ActivityAssignment = objActivityAssignment, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "Data not found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        [Route("getlearnerdashboardcurrentassignment")]
        [Authorize]
        public async Task<IActionResult> GetLearnerDashboardCurrentAssignment(string ClientId, string UserId)
        {
            var assignmentReport = new Report { ClientId = ClientId };
            assignmentReport.AddParameter(UserAssignment.PARA_USER_ID, ReportDataType.VarChar, UserId);
            assignmentReport.AddParameter(Learner.PARA_LANGUAGE_ID, ReportDataType.VarChar, _strDefaultLanguage);

            DataSet baseData = new ReportManager().ExecuteDataSet(assignmentReport, Report.ListMethod.LearnerUserActivityAssignmentNotCompletedDashboard);
            var table = baseData.Tables[0];
            var rows = new List<Dictionary<string, object>>();
            if (baseData.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    var row = new Dictionary<string, object>();
                    foreach (DataColumn col in table.Columns)
                    {
                        var value = dr[col];
                        if (value == DBNull.Value || (value is string str && string.IsNullOrWhiteSpace(str)))
                        {
                            row[col.ColumnName] = "";
                        }
                        else
                        {
                            row[col.ColumnName] = value;
                        }
                    }
                    rows.Add(row);
                }

                return Ok(new { ActivityAssignment = rows, Code = 200 }); // Sends a JSON response with status code 200
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Data not found" });
            }
        }
        [HttpPost]
        [Route("getlearnerdashboardcompletedassignment")]
        [Authorize]
        public async Task<IActionResult> GetLearnerDashboardCompletedAssignment(string ClientId, string UserId)
        {
            var assignmentReport = new Report { ClientId = ClientId };
            assignmentReport.AddParameter(UserAssignment.PARA_USER_ID, ReportDataType.VarChar, UserId);
            assignmentReport.AddParameter(Learner.PARA_LANGUAGE_ID, ReportDataType.VarChar, _strDefaultLanguage);

            DataSet baseData = new ReportManager().ExecuteDataSet(assignmentReport, Report.ListMethod.LearnerUserActivityAssignmentCompletedDashboard);
            //string json = JsonConvert.SerializeObject(baseData.Tables[0], Formatting.Indented);
            ////json = json.Replace(Environment.NewLine, string.Empty);
            //var obj = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            var table = baseData.Tables[0];
            var rows = new List<Dictionary<string, object>>();
            if (baseData.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    var row = new Dictionary<string, object>();
                    foreach (DataColumn col in table.Columns)
                    {
                        var value = dr[col];
                        if (value == DBNull.Value || (value is string str && string.IsNullOrWhiteSpace(str)))
                        {
                            row[col.ColumnName] = "";
                        }
                        else
                        {
                            row[col.ColumnName] = value;
                        }
                    }
                    rows.Add(row);
                }

                return Ok(new { ActivityAssignment = rows, Code = 200 }); // Sends a JSON response with status code 200
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Data not found" });
            }
        }
        [HttpPost]
        [Route("getcompletedassignment")]
        [Authorize]
        public async Task<IActionResult> GetCompletedAssignment(ReportVM pEntReport)
        {
            var entCompletedAssignment = new Report { ClientId = pEntReport.ClientId };
            entCompletedAssignment.AddParameter(UserAssignment.PARA_USER_ID, ReportDataType.VarChar, pEntReport.UserID);
            entCompletedAssignment.AddParameter(API.Entity.ReportSchema.Learner.PARA_LANGUAGE_ID, ReportDataType.VarChar, _strDefaultLanguage);
            entCompletedAssignment.ListRange = new EntityRange
            {
                PageIndex = pEntReport.ListRange.PageIndex,
                PageSize = pEntReport.ListRange.PageSize,
                KeyWord = pEntReport.ListRange.KeyWord
            };

            if (!string.IsNullOrEmpty(pEntReport.ListRange.SortExpression))
                entCompletedAssignment.ListRange.SortExpression = pEntReport.ListRange.SortExpression;

            var _data = new ReportManager().ExecuteDataSet(entCompletedAssignment, Report.ListMethod.LearnerUserActivityAssignmentCompletedMainPage);
            var table = _data.Tables[0];
            var rows = new List<Dictionary<string, object>>();
            if (_data.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    var row = new Dictionary<string, object>();
                    foreach (DataColumn col in table.Columns)
                    {
                        var value = dr[col];
                        if (value == DBNull.Value || (value is string str && string.IsNullOrWhiteSpace(str)))
                        {
                            row[col.ColumnName] = "";
                        }
                        else
                        {
                            row[col.ColumnName] = value;
                        }
                    }
                    rows.Add(row);
                }
                return Ok(new { ActivityAssignment = rows, Code = 200 }); // Sends a JSON response with status code 200
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Data not found" });
            }
        }
        [HttpPost]
        [Route("getcurrentassignment")]
        [Authorize]
        public async Task<IActionResult> GetCurrentAssignment(ReportVM pEntReport)
        {
            var entCurrentAssignment = new Report { ClientId = pEntReport.ClientId };
            entCurrentAssignment.AddParameter(UserAssignment.PARA_USER_ID, ReportDataType.VarChar, pEntReport.UserID);
            entCurrentAssignment.AddParameter(API.Entity.ReportSchema.Learner.PARA_LANGUAGE_ID, ReportDataType.VarChar, _strDefaultLanguage);
            entCurrentAssignment.ListRange = new EntityRange
            {
                PageIndex = pEntReport.ListRange.PageIndex,
                PageSize = pEntReport.ListRange.PageSize,
                KeyWord = pEntReport.ListRange.KeyWord
            };
            if (!string.IsNullOrEmpty(pEntReport.ListRange.SortExpression))
                entCurrentAssignment.ListRange.SortExpression = pEntReport.ListRange.SortExpression;

            var _data = new ReportManager().ExecuteDataSet(entCurrentAssignment, Report.ListMethod.LearnerUserActivityAssignmentNotCompletedMainPage);
            var table = _data.Tables[0];
            var rows = new List<Dictionary<string, object>>();
            if (_data.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow dr in table.Rows)
                {
                    var row = new Dictionary<string, object>();
                    foreach (DataColumn col in table.Columns)
                    {
                        var value = dr[col];
                        if (value == DBNull.Value || (value is string str && string.IsNullOrWhiteSpace(str)))
                        {
                            row[col.ColumnName] = "";
                        }
                        else
                        {
                            row[col.ColumnName] = value;
                        }
                    }
                    rows.Add(row);
                }

                return Ok(new { ActivityAssignment = rows, Code = 200 }); // Sends a JSON response with status code 200
            }
            else
            {
                return NotFound(new { Code = 404, Msg = "Data not found" });
            }
        }
    }
}
