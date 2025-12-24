using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public class ScoReader : LessonReader
    {
        public ScoReader(XmlNode lessonNode, int masteryScore) : base(lessonNode, masteryScore)
        {
        }

        public override Lesson ReadLesson()
        {
            var sco = new Sco
            {
                DataFromLms = ItemElement.GetAttribute("datafromlms"),
                MaxTimeAllowed = GetTime(ItemElement.GetAttribute("maxtimeallowed")),
                MaxTimeAllowed2004 = ItemElement.GetAttribute("maxtimeallowed"), //added by sarita 
                TimeLimitAction = ItemElement.GetAttribute("timelimitaction"),
                Prerequisites = ItemElement.GetAttribute("prerequisites"),
                TargetWindow = ItemElement.GetAttribute("winTarget")

            };
            FillStandardProperties(sco);

            var identifierRef = ItemElement.GetAttribute("identifierref");
            var resourceElement = GetResourceElement(identifierRef);

            if (resourceElement != null)
            {
                sco.Resource = new ScormResource
                {
                    Base = resourceElement.GetAttribute("base"),
                    Href = resourceElement.GetAttribute("href"),
                    Identifier = IdentifierRef,
                    ScormType = resourceElement.GetAttribute("scormtype"),
                    Type = resourceElement.GetAttribute("type")
                };
            }
            return sco;
        }
    }
}
