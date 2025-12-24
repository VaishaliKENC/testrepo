using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface ICourseConfigurationAdaptor<T>
    {
        CourseConfiguration GetConfiguration(string clientId);
        int GetMasteryScore(string clientId);
        CourseConfiguration EditCourseConfiguration(CourseConfiguration pEntCourseConfiguration);
        CourseConfiguration GetAVPath(CourseConfiguration pEntCourseConfiguration);
        CourseConfiguration GetMasteryScore(CourseConfiguration pEntCourseConfiguration);
    }
}
