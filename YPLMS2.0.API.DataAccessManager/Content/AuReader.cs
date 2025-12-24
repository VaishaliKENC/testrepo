using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public class AuReader : LessonReader
    {
        public AuReader(XmlNode lessonNode, int masteryScore) : base(lessonNode, masteryScore)
        {
        }

        public override Lesson ReadLesson()
        {
            var au = new Au
            {
                AuPassword = SnagValue("au_password"),
                CoreVendor = SnagValue("core_vendor"),
                MaxTimeAllowed = GetTime(SnagValue("max_time_allowed")),
                TimeLimitAction = SnagValue("time_limit_action"),
                Scaled_Passing_Score = SnagValueDecimal("scaled_passing_score")  //added by sarita
            };
            FillStandardProperties(au);
            var resourceElement = GetResourceElement(au.Identifier);
            if (resourceElement != null)
            {
                au.Href = resourceElement.GetAttribute("href");
                au.WebLaunch = resourceElement.GetAttribute("web_launch");
            }
            return au;
        }

        private string SnagValue(string nodeName)
        {
            var xmlNode = ItemElement.SelectSingleNode(nodeName);
            if (xmlNode == null) return String.Empty;
            return xmlNode.InnerText;
        }
        private decimal? SnagValueDecimal(string nodeName)
        {
            var str = SnagValue(nodeName);
            decimal d;
            if (!decimal.TryParse(str, out d)) return null;
            return d;
        }
    }
}
