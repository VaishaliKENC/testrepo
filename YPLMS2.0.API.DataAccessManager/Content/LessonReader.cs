using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public abstract class LessonReader
    {
        protected XmlDocument ManifestXml;
        protected XmlElement ItemElement;
        protected int MasteryScore;
        protected string IdentifierRef;
        protected decimal? scaledpassingscore;
        protected string MaxTimeAllowed2004;
        public LessonReader(XmlNode lessonNode, int masteryScore)
        {
            ManifestXml = lessonNode.OwnerDocument;
            ItemElement = (XmlElement)lessonNode;
            var masteryNode = ItemElement.SelectSingleNode("masteryscore");
            if (masteryScore <= 0 && masteryNode != null)
            {
                int.TryParse(masteryNode.InnerText, out masteryScore);
            }
            MasteryScore = masteryScore;
            #region added by sarita 
            var mgr = new XmlNamespaceManager(ManifestXml.NameTable);
            mgr.AddNamespace("a", "http://www.imsglobal.org/xsd/imsss");
            XmlNodeList nodes = ManifestXml.SelectNodes("//a:minNormalizedMeasure", mgr);
            foreach (XmlNode node in nodes)
            {
                decimal d;
                if (!decimal.TryParse(node.InnerText, out d))
                    scaledpassingscore = null;
                else
                    scaledpassingscore = d;
            }

            XmlNodeList nod = ManifestXml.SelectNodes("//a:attemptAbsoluteDurationLimit", mgr);
            foreach (XmlNode node in nod)
            {
                MaxTimeAllowed2004 = node.InnerText;
            }
            #endregion added by sarita
        }

        public abstract Lesson ReadLesson();
        protected XmlElement GetResourceElement(string identifier)
        {
            return ManifestXml.SelectSingleNode("/manifest/resources/resource[@identifier='" + identifier.Trim() + "']") as XmlElement;
        }

        protected TimeSpan? GetTime(string time)
        {
            if (String.IsNullOrEmpty(time))
                return null;
            return new AiccTime(time).Time;
        }



        protected void FillStandardProperties(Lesson lesson)
        {
            var titleNode = ItemElement.SelectSingleNode("title");
            lesson.Title = titleNode == null ? String.Empty : titleNode.InnerText;
            lesson.Identifier = ItemElement.GetAttribute("identifier");
            lesson.MasteryScore = MasteryScore;
            lesson.Scaled_Passing_Score = scaledpassingscore; // added by sarita
            lesson.MaxTimeAllowed2004 = MaxTimeAllowed2004;  // added by sarita
            lesson.DataFromLmsSco = ItemElement.GetAttribute("datafromlms");
            lesson.PrerequisitesSco = ItemElement.GetAttribute("prerequisites");
            lesson.TargetWindowSco = ItemElement.GetAttribute("winTarget");

            var identifierRef = ItemElement.GetAttribute("identifierref");
            var resourceElement = GetResourceElement(identifierRef);
            lesson.ResourceSco = new ScormResource
            {
                Base = resourceElement.GetAttribute("base"),
                Href = resourceElement.GetAttribute("href"),
                Identifier = IdentifierRef,
                ScormType = resourceElement.GetAttribute("scormtype"),
                Type = resourceElement.GetAttribute("type")
            };
        }
    }
}
