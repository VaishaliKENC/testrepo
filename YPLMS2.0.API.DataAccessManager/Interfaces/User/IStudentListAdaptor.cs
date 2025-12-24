using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface IStudentListAdaptor<T>
    {
        StudentList GetStudentList(StudentList pEntStudentList);
        List<StudentList> FindStudentList(Search pEntSearch);
    }
}
