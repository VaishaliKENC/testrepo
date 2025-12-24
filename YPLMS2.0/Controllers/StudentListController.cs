using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentListController : ControllerBase
    {
        private readonly IStudentListAdaptor<StudentList> _studentListAdaptor;
        public StudentListController(IStudentListAdaptor<StudentList> studentListAdaptor)
        {
            _studentListAdaptor = studentListAdaptor;
        }

        [HttpPost]
        [Route("getstudentlist")]
        [Authorize]
        public async Task<IActionResult> GetStudentList(StudentList pEntStudentList)
        {
            try
            {
                StudentList entStudentList = new StudentList();
                entStudentList = _studentListAdaptor.GetStudentList(pEntStudentList);

                if (entStudentList != null)
                {
                    return Ok(new { StudentList = entStudentList, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("findstudentlist")]
        [Authorize]
        public async Task<IActionResult> FindStudentList(Search pEntSearch)
        {
            try
            {
                List<StudentList> entStudentList = new List<StudentList>();
                entStudentList = _studentListAdaptor.FindStudentList(pEntSearch);

                if (entStudentList != null)
                {
                    return Ok(new { StudentList = entStudentList, Code = 200 }); // Sends a JSON response with status code 200
                }
                else
                {
                    return NotFound(new { Code = 404, Status = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
