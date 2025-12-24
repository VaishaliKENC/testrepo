using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public interface ILessonTrackingSerializer
    {
        Dictionary<string, LessonTracking> ReadLessonTracking(string userDataXml); //TODO: Read and use Bookmark attribute
        LessonTracking ParseLesson(XmlNode lessonNode);
        string WriteLessonTracking(ContentModuleTracking contentModuleTracking);
    }
}
