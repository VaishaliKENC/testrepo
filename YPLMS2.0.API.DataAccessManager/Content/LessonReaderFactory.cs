using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public class LessonReaderFactory
    {
        private ActivityContentType _courseType;
        public LessonReaderFactory(ActivityContentType courseType)
        {
            _courseType = courseType;
        }
        public LessonReader GetLessonReader(XmlNode lessonNode, int masteryScore)
        {
            switch (_courseType)
            {
                case ActivityContentType.Scorm12:
                case ActivityContentType.Scorm2004:
                    return new ScoReader(lessonNode, masteryScore);
                case ActivityContentType.AICC:
                    return new AuReader(lessonNode, masteryScore);
            }
            return null;
        }
    }
}
