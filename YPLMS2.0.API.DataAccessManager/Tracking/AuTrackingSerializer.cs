using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    public class AuTrackingSerializer : ILessonTrackingSerializer
    {
        public string CourseId { get; set; }
        public string LearnerId { get; set; }
        public string LearnerName { get; set; }

        public Dictionary<string, LessonTracking> ReadLessonTracking(string userDataXml)
        {
            var trackingDocument = new XmlDocument();
            trackingDocument.LoadXml(userDataXml);
            var lessons = new Dictionary<string, LessonTracking>();
            var nodeList = trackingDocument.SelectNodes("//au");
            if (nodeList == null) return lessons;
            foreach (XmlNode scoNode in nodeList)
            {
                var au = ParseLesson(scoNode);
                lessons.Add(au.Identifier, au);
            }
            return lessons;
        }

        public string WriteLessonTracking(ContentModuleTracking contentModuleTracking)
        {

            var lessonTracking = contentModuleTracking.LessonTracking;
            var xmlDocument =
                new XDocument(new XElement("Root",
                                           new XElement("InitData",
                                                        new XElement("CourseId", CourseId),
                                                        new XElement("LearnerId", LearnerId),
                                                        new XElement("LearnerName", new XCData(LearnerName))
                                                        )));
            try
            {
                foreach (string auId in lessonTracking.Keys)
                {
                    xmlDocument.Root.Add(AuTrackingToXNode(lessonTracking[auId]));
                }
                if (!String.IsNullOrEmpty(contentModuleTracking.Bookmark))
                {
                    xmlDocument.Root.Add(new XAttribute("Bookmark", contentModuleTracking.Bookmark));
                }

            }
            catch (Exception e)
            {
                string exp = e.Message;
            }
            return xmlDocument.ToString();
        }

        private XElement AuTrackingToXNode(LessonTracking tracking)
        {
            XElement xe = null;
            try
            {
                xe = new XElement("au", new XAttribute("identifier", tracking.Identifier),
                                    new XElement("cmi",
                                                 new XElement("core",
                                                              new XElement("student_id", new XCData(LearnerId)),
                                                              new XElement("student_name", new XCData(LearnerName)),
                                                              new XElement("output_File"),
                                                              new XElement("lesson_location", tracking.LessonLocation),
                                                              new XElement("credit", tracking.Credit),
                                                              new XElement("lesson_mode", tracking.LessonMode),
                                                              new XElement("lesson_status",
                                                                           new XElement("part1",
                                                                                        new XCData(tracking.LessonStatus)),
                                                                           new XElement("part2", new XCData(string.IsNullOrEmpty(tracking.Entry)
                                                                                                ? String.Empty : tracking.Entry))
                                                                  ),
                                                              new XElement("path"),
                                                              new XElement("score",
                                                                           new XElement("part1",
                                                                                        new XCData(
                                                                                            tracking.RawScore.HasValue
                                                                                                ? tracking.RawScore.Value.
                                                                                                      ToString()
                                                                                                : String.Empty)),
                                                                           new XElement("part2",
                                                                                        new XCData(
                                                                                            tracking.MinScore.HasValue
                                                                                                ? tracking.MinScore.Value.
                                                                                                      ToString()
                                                                                                : String.Empty)),
                                                                           new XElement("part3",
                                                                                        new XCData(
                                                                                            tracking.MaxScore.HasValue
                                                                                                ? tracking.MaxScore.Value.
                                                                                                      ToString()
                                                                                                : String.Empty))
                                                                  ),
                                                              new XElement("time",
                                                                           new XCData(
                                                                               new AiccTime(tracking.TotalTime).ToString()))
                                                     ),
                                                 new XElement("core_lesson",
                                                              new XElement("core_lesson", new XCData(tracking.SuspendData))),
                                                 new XElement("core_vendor",
                                                              new XElement("core_vendor", new XCData(string.IsNullOrEmpty(tracking.LaunchData)
                                                                                                ? String.Empty : tracking.LaunchData))),
                                                 new XElement("student_data",
                                                              new XElement("mastery_score",
                                                                           new XCData(tracking.MasteryScore.HasValue
                                                                                          ? tracking.MasteryScore.Value.
                                                                                                ToString()
                                                                                          : String.Empty)),
                                                              new XElement("max_time_allowed",
                                                                           new XCData(tracking.MaxTimeAllowed.HasValue
                                                                                          ? new AiccTime(tracking.MaxTimeAllowed.Value).ToString()
                                                                                          : String.Empty)),
                                                              new XElement("time_limit_action",
                                                                           new XElement("part1",
                                                                                        new XCData(string.IsNullOrEmpty(tracking.TimeLimitAction)
                                                                                                ? String.Empty : tracking.TimeLimitAction)),
                                                                           new XElement("part2",
                                                                                        new XCData(string.IsNullOrEmpty(tracking.TimeLimitAction)
                                                                                                ? String.Empty : tracking.TimeLimitAction))
                                                                  )
                                                     )
                                        ),
                                    new XElement("exitau_status", tracking.Exit)
                    );

            }
            catch (Exception e)
            {
                string exp = e.Message;
            }
            return xe;
        }


        public LessonTracking ParseLesson(XmlNode lessonNode)
        {
            var exitNode = lessonNode.SelectSingleNode("exitau_status");
            Func<string, string> snagValue = s =>
            {
                var xmlNode = lessonNode.SelectSingleNode("cmi/" + s);
                if (xmlNode == null) return String.Empty;
                return xmlNode.InnerText;
            };
            Func<string, int?> snagInt = s =>
            {
                var str = snagValue(s);
                int j;
                if (!int.TryParse(str, out j)) return null;
                return j;
            };
            Func<string, decimal?> snagDecimal = s =>
            {
                var str = snagValue(s);
                decimal d;
                if (!decimal.TryParse(str, out d)) return null;
                return d;
            };
            Func<string, TimeSpan?> snagTime = s =>
            {
                var str = snagValue(s);
                if (str == String.Empty) return null;
                return new AiccTime(str).Time;
            };
            return new LessonTracking
            {
                Identifier = ((XmlElement)lessonNode).GetAttribute("identifier"),
                StudentId = snagValue("core/student_id"),
                StudentName = snagValue("core/student_name"),
                LessonLocation = snagValue("core/lesson_location"),
                Credit = snagValue("core/credit"),
                LessonStatus = snagValue("core/lesson_status/part1"),
                Entry = snagValue("core/entry/part2"),
                RawScore = snagDecimal("core/score/part1"),
                MinScore = snagDecimal("core/score/part2"),
                MaxScore = snagDecimal("core/score/part3"),
                TotalTime = snagTime("core/time") ?? new TimeSpan(0),
                LessonMode = snagValue("core/lesson_mode"),
                Exit = exitNode == null ? "" : exitNode.InnerText,
                SuspendData = snagValue("core_lesson"),
                LaunchData = snagValue("core_vendor"),
                Comments = snagValue("comments"),
                CommentsFromLms = snagValue("comments_from_lms"),
                MasteryScore = snagInt("student_data/mastery_score"),
                MaxTimeAllowed = snagTime("student_data/max_time_allowed"),
                TimeLimitAction = snagValue("student_data/time_limit_action/part1")
            };
        }
        public XElement AuTrackingToXNodeCovert(LessonTracking tracking)
        {
            XElement xe = null;
            try
            {
                xe = new XElement("Root", new XElement("sco",
                                    new XElement("cmi",
                                                 new XElement("core",
                                                              new XElement("student_id", new XCData(tracking.StudentId)),
                                                              new XElement("student_name", new XCData(tracking.StudentName)),
                                                              new XElement("output_File"),
                                                              new XElement("lesson_location", tracking.LessonLocation),
                                                              new XElement("credit", tracking.Credit),
                                                              new XElement("lesson_mode", tracking.LessonMode),
                                                              new XElement("lesson_status",
                                                                           new XElement("part1",
                                                                                        new XCData(tracking.LessonStatus)),
                                                                           new XElement("part2", new XCData(string.IsNullOrEmpty(tracking.Entry)
                                                                                                ? String.Empty : tracking.Entry))
                                                                  ),
                                                              new XElement("path"),
                                                              new XElement("score",
                                                                           new XElement("part1",
                                                                                        new XCData(
                                                                                            tracking.RawScore.HasValue
                                                                                                ? tracking.RawScore.Value.
                                                                                                      ToString()
                                                                                                : String.Empty)),
                                                                           new XElement("part2",
                                                                                        new XCData(
                                                                                            tracking.MinScore.HasValue
                                                                                                ? tracking.MinScore.Value.
                                                                                                      ToString()
                                                                                                : String.Empty)),
                                                                           new XElement("part3",
                                                                                        new XCData(
                                                                                            tracking.MaxScore.HasValue
                                                                                                ? tracking.MaxScore.Value.
                                                                                                      ToString()
                                                                                                : String.Empty))
                                                                  ),
                                                              new XElement("time",
                                                                           new XCData(
                                                                               new AiccTime(tracking.TotalTime).ToString()))
                                                     ),
                                                 new XElement("core_lesson",
                                                              new XElement("core_lesson", new XCData(tracking.SuspendData))),
                                                 new XElement("core_vendor",
                                                              new XElement("core_vendor", new XCData(string.IsNullOrEmpty(tracking.LaunchData)
                                                                                                ? String.Empty : tracking.LaunchData))),
                                                 new XElement("student_data",
                                                              new XElement("mastery_score",
                                                                           new XCData(tracking.MasteryScore.HasValue
                                                                                          ? tracking.MasteryScore.Value.
                                                                                                ToString()
                                                                                          : String.Empty)),
                                                              new XElement("max_time_allowed",
                                                                           new XCData(tracking.MaxTimeAllowed.HasValue
                                                                                          ? new AiccTime(tracking.MaxTimeAllowed.Value).ToString()
                                                                                          : String.Empty)),
                                                              new XElement("time_limit_action",
                                                                           new XElement("part1",
                                                                                        new XCData(string.IsNullOrEmpty(tracking.TimeLimitAction)
                                                                                                ? String.Empty : tracking.TimeLimitAction)),
                                                                           new XElement("part2",
                                                                                        new XCData(string.IsNullOrEmpty(tracking.TimeLimitAction)
                                                                                                ? String.Empty : tracking.TimeLimitAction))
                                                                  )
                                                     )
                                        ),
                                    new XElement("exitau_status", tracking.Exit)
                                    )
                    );

            }
            catch (Exception e)
            {
                string exp = e.Message;
            }
            return xe;
        }
    }
}
