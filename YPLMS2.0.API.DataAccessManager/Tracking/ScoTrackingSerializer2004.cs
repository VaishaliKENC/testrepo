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
    public class ScoTrackingSerializer2004 : ILessonTrackingSerializer2004
    {
        public Dictionary<string, LessonTracking2004> ReadLessonTracking(string userDataXml)
        {
            var trackingDocument = new XmlDocument();
            trackingDocument.LoadXml(userDataXml);
            var lessons = new Dictionary<string, LessonTracking2004>();
            var nodeList = trackingDocument.SelectNodes("//sco");
            if (nodeList == null) return lessons;
            foreach (XmlNode scoNode in nodeList)
            {
                var sco = ParseLesson(scoNode);
                lessons.Add(sco.Identifier, sco);
            }
            return lessons;
        }

        public string WriteLessonTracking(ContentModuleTracking contentModuleTracking)
        {
            var lessonTracking = contentModuleTracking.LessonTracking2004;
            var xDoc = new XDocument(new XElement("Root",
                    lessonTracking.Values.Select(t => new XElement("sco", new XAttribute("identifier", t.Identifier),
                        new XElement("cmi",
                                //new XElement("core",
                                new XElement("_version", new XCData(t.Version)),
                                new XElement("learner_id", new XCData(t.StudentId)),
                                new XElement("learner_name", new XCData(t.StudentName)),
                                new XElement("location", new XCData(t.LessonLocation)),
                                new XElement("credit", t.Credit),
                                new XElement("completion_status", t.LessonStatus),
                                new XElement("success_status", t.SuccessStatus),
                                new XElement("entry", t.Entry),
                                new XElement("score",
                                    new XElement("raw", t.RawScore.HasValue ? t.RawScore.ToString() : String.Empty),
                                    new XElement("max", t.MaxScore.HasValue ? t.MaxScore.ToString() : String.Empty),
                                    new XElement("min", t.MinScore.HasValue ? t.MinScore.ToString() : String.Empty),
                                    new XElement("scaled", t.ScaledScore.HasValue ? t.ScaledScore.ToString() : String.Empty)
                                    ),
                                new XElement("total_time", t.TotalTime),//new AiccTime(t.TotalTime).ToString()),
                                new XElement("mode", t.LessonMode),
                                new XElement("exit", t.Exit),
                                new XElement("session_time", t.SessionTime),//t.SessionTime.HasValue ? new AiccTime(t.SessionTime.Value).ToString() : String.Empty),
                                new XElement("totalpages", t.totalpages),
                                new XElement("completedpages", t.completedpages),
                             // ),
                             new XElement("suspend_data", new XCData(t.SuspendData)),
                             new XElement("launch_data", new XCData(t.LaunchData)),
                             new XElement("comments", new XCData(t.Comments)),
                             new XElement("comments_from_lms", new XCData(t.CommentsFromLms)),
                             new XElement("objectives",
                                 new XElement("_count", t.Objectives.Count),
                                 t.Objectives.Select((o, i) => new XElement("_" + i, new XElement("id", o.Identifier),
                                 new XElement("score",
                                    new XElement("raw", o.RawScore.HasValue ? o.RawScore.ToString() : String.Empty),
                                    new XElement("max", o.MaxScore.HasValue ? o.MaxScore.ToString() : String.Empty),
                                    new XElement("min", o.MinScore.HasValue ? o.MinScore.ToString() : String.Empty),
                                    new XElement("scaled", o.Scaled.HasValue ? o.Scaled.ToString() : String.Empty)
                                 ),
                                 new XElement("completion_status", o.status),
                                 new XElement("progress_measure", o.Progress_Measure),
                                  new XElement("success_status", o.success_status),
                                   new XElement("description", o.description)))
                             ),

                                 // new XElement("student_data",
                                 new XElement("mastery_score", t.MasteryScore),
                                 new XElement("max_time_allowed", t.MaxTimeAllowed),//new AiccTime(t.MaxTimeAllowed).ToString()),//t.MaxTimeAllowed.HasValue ? new AiccTime(t.MaxTimeAllowed.Value).ToString() : String.Empty),
                                 new XElement("time_limit_action", t.TimeLimitAction),
                                 new XElement("scaled_passing_score", t.scaled_passing_score),   // added by sarita
                                    new XElement("progress_measure", t.progress_measure),   // added by sarita
                                                                                            //  ),
                                                                                            //new XElement("student_preference", //TODO: get these from somewhere
                                                                                            //    new XElement("audio"),
                                                                                            //    new XElement("language", "en-US"),
                                                                                            //    new XElement("speed"),
                                                                                            //    new XElement("text")
                                                                                            //),

                            new XElement("interactions",
                               new XElement("_count", t.Interactions.Count),
                                 t.Interactions.Select((o, i) => new XElement("_" + i, new XElement("id", o.Identifier),
                                 new XElement("type", o.Type),
                                 new XElement("timestamp", o.TimeStamp),
                                  new XElement("weighting", o.Weighting),
                                   new XElement("learner_response", o.Learner_Response),
                                    new XElement("result", o.Result),
                                     new XElement("latency", o.Latencey),
                                      new XElement("description", o.Description),
                                      new XElement("objectives",
                               new XElement("_count", o.Objective_Tracking.Count),
                                 o.Objective_Tracking.Select((oo, ii) => new XElement("_" + ii, new XElement("id", oo.Identifier)))),
                                  new XElement("correct_responses",
                               new XElement("_count", o.Correct_Responses.Count),
                                 o.Correct_Responses.Select((oo, ii) => new XElement("_" + ii, new XElement("pattern", oo.Pattern))))
                                      ))
                            )
                        )))));
            if (!String.IsNullOrEmpty(contentModuleTracking.Bookmark))
            {
                xDoc.Root.Add(new XAttribute("Bookmark", contentModuleTracking.Bookmark));
            }

            //return xDoc.ToString();
            return xDoc.ToString(SaveOptions.DisableFormatting);
        }

        public LessonTracking2004 ParseLesson(XmlNode lessonNode)
        {
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
            Func<string, string> snagTime = s =>
            {
                var str = snagValue(s);
                if (str == String.Empty) return null;
                return str;
                //return new AiccTime(str).Time;
            };
            return new LessonTracking2004
            {
                Identifier = ((XmlElement)lessonNode).GetAttribute("identifier"),
                Version = snagValue("_version"),
                StudentId = snagValue("learner_id"),
                StudentName = snagValue("learner_name"),
                LessonLocation = snagValue("location"),
                Credit = snagValue("credit"),
                LessonStatus = snagValue("completion_status"),
                SuccessStatus = snagValue("success_status"),
                Entry = snagValue("entry"),
                RawScore = snagDecimal("score/raw"),
                MinScore = snagDecimal("score/min"),
                MaxScore = snagDecimal("score/max"),
                ScaledScore = snagDecimal("score/scaled"),
                TotalTime = snagTime("total_time"),// ?? new TimeSpan(0),
                LessonMode = snagValue("mode"),
                Exit = snagValue("exit"),
                SessionTime = snagTime("session_time"),
                SuspendData = snagValue("suspend_data"),
                LaunchData = snagValue("launch_data"),
                Comments = snagValue("comments"),
                CommentsFromLms = snagValue("comments_from_lms"),
                progress_measure = snagDecimal("progress_measure"),
                Objectives = ParseObjectives(lessonNode.SelectSingleNode("cmi/objectives")),
                Interactions = ParseInteractions(lessonNode.SelectSingleNode("cmi/interactions"), lessonNode),
                MasteryScore = snagInt("mastery_score"),
                MaxTimeAllowed = snagValue("max_time_allowed"), // commented by sarita snagTime("max_time_allowed"),
                TimeLimitAction = snagValue("time_limit_action"),
                scaled_passing_score = snagDecimal("scaled_passing_score"),
                totalpages = snagInt("totalpages"),
                completedpages = snagInt("completedpages")
            };
        }

        private List<ObjectiveTracking> ParseObjectives(XmlNode objectivesNode)
        {
            //var objectives = new List<ObjectiveTracking>();
            //if (objectivesNode == null) return objectives;
            //var nodeList = objectivesNode.SelectNodes("//id | //completion_status | //progress_measure");
            //if (nodeList == null) return objectives;

            //foreach (XmlNode objectiveNode in nodeList)
            //{
            //    objectives.Add(ParseObjective(objectiveNode.ParentNode));
            //}
            //return objectives;
            var objectives = new List<ObjectiveTracking>();
            if (objectivesNode == null) return objectives;

            foreach (XmlNode chNode in objectivesNode.ChildNodes)
            {
                if (chNode.SelectSingleNode("id") != null)
                {
                    objectives.Add(ParseObjective(chNode));
                }
            }
            return objectives;
        }

        private ObjectiveTracking ParseObjective(XmlNode objectiveNode)
        {
            Func<string, string> snagValue = s =>
            {
                var xmlNode = objectiveNode.SelectSingleNode(s);
                if (xmlNode == null) return String.Empty;
                return xmlNode.InnerText;
            };
            Func<string, int?> snagScore = s =>
            {
                var str = snagValue("score/" + s);
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

            return new ObjectiveTracking
            {
                Identifier = snagValue("id"),
                MaxScore = snagScore("max"),
                MinScore = snagScore("min"),
                RawScore = snagScore("raw"),
                Scaled = snagDecimal("score/scaled"),
                status = snagValue("completion_status"),
                Progress_Measure = snagDecimal("progress_measure"),
                success_status = snagValue("success_status"),
                description = snagValue("description"),
            };
        }


        private List<InteractionTracking> ParseInteractions(XmlNode InteractionsNode, XmlNode lessonNode)
        {

            var Interactions = new List<InteractionTracking>();
            if (InteractionsNode == null) return Interactions;

            foreach (XmlNode chNode in InteractionsNode.ChildNodes)
            {
                if (chNode.SelectSingleNode("id") != null)
                {
                    Interactions.Add(ParseInteraction(chNode));
                }
            }
            return Interactions;
        }

        private InteractionTracking ParseInteraction(XmlNode InteractionNode)
        {
            Func<string, string> snagValue = s =>
            {
                var xmlNode = InteractionNode.SelectSingleNode(s);
                if (xmlNode == null) return String.Empty;
                return xmlNode.InnerText;
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
            return new InteractionTracking
            {
                Identifier = snagValue("id"),
                Type = snagValue("type"),
                TimeStamp = snagTime("timestamp"),
                Weighting = snagDecimal("weighting"),
                Learner_Response = snagValue("learner_response"),
                Result = snagValue("result"),
                Latencey = snagTime("latency"),
                Description = snagValue("description"),
                Correct_Responses = ParseInteractionCorrectResponses(InteractionNode.SelectSingleNode("correct_responses")),
                Objective_Tracking = ParseInteractionObjectives(InteractionNode.SelectSingleNode("objectives")),
            };
        }



        private List<InteractionCorrectResponses> ParseInteractionCorrectResponses(XmlNode CorrectResponsesNode)
        {

            var InteractionCorrectResponses = new List<InteractionCorrectResponses>();
            if (CorrectResponsesNode == null) return InteractionCorrectResponses;

            foreach (XmlNode chNode in CorrectResponsesNode.ChildNodes)
            {
                if (chNode.SelectSingleNode("pattern") != null)
                {
                    InteractionCorrectResponses.Add(ParseInteractionCorrectResponse(chNode));
                }
            }
            return InteractionCorrectResponses;
        }

        private InteractionCorrectResponses ParseInteractionCorrectResponse(XmlNode CorrectResponseNode)
        {
            Func<string, string> snagValue = s =>
            {
                var xmlNode = CorrectResponseNode.SelectSingleNode(s);
                if (xmlNode == null) return String.Empty;
                return xmlNode.InnerText;
            };



            return new InteractionCorrectResponses
            {
                Pattern = snagValue("pattern"),
            };
        }



        private List<InteractionObjectiveTracking> ParseInteractionObjectives(XmlNode IobjectivesNode)
        {

            var objectives = new List<InteractionObjectiveTracking>();
            if (IobjectivesNode == null) return objectives;

            foreach (XmlNode chNode in IobjectivesNode.ChildNodes)
            {
                if (chNode.SelectSingleNode("id") != null)
                {
                    objectives.Add(ParseInteractionObjective(chNode));
                }
            }
            return objectives;
        }

        private InteractionObjectiveTracking ParseInteractionObjective(XmlNode IobjectiveNode)
        {
            Func<string, string> snagValue = s =>
            {
                var xmlNode = IobjectiveNode.SelectSingleNode(s);
                if (xmlNode == null) return String.Empty;
                return xmlNode.InnerText;
            };


            return new InteractionObjectiveTracking
            {
                Identifier = snagValue("id"),
            };
        }



    }
}
