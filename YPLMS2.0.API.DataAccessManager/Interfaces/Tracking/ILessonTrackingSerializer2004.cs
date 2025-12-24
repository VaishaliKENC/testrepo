using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface ILessonTrackingSerializer2004
    {
        Dictionary<string, LessonTracking2004> ReadLessonTracking(string userDataXml); //TODO: Read and use Bookmark attribute
        LessonTracking2004 ParseLesson(XmlNode lessonNode);
        string WriteLessonTracking(ContentModuleTracking contentModuleTracking);
    }
}
