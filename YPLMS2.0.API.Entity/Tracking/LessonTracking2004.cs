using System;
using System.Collections.Generic;

namespace YPLMS2._0.API.Entity
{
   [Serializable] public class LessonTracking2004
    {
        public string Identifier { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string ManagerEmail { get; set; }
        public string LessonLocation { get; set; }
        public string Credit { get; set; }
        public string LessonStatus { get; set; }
        public string Entry { get; set; }
        public decimal? RawScore { get; set; }
        public decimal? MinScore { get; set; }
        public decimal? MaxScore { get; set; }
        public decimal? ScaledScore { get; set; }
        public string Exit { get; set; }
        //public TimeSpan? SessionTime { get; set; }
        //public TimeSpan TotalTime { get; set; }
        public string SessionTime { get; set; }
        public string TotalTime { get; set; }
        public string LessonMode { get; set; }
        public string SuspendData { get; set; }
        public string LaunchData { get; set; }
        public string Comments { get; set; }
        public string CommentsFromLms { get; set; }
        public List<ObjectiveTracking> Objectives { get; set; }
        public List<InteractionTracking> Interactions { get; set; }
        public int? MasteryScore { get; set; }
        //public TimeSpan? MaxTimeAllowed { get; set; }
        public string MaxTimeAllowed { get; set; }  // added by sarita
        public string TimeLimitAction { get; set; }
        public int? totalpages { get; set; }
        public int? completedpages { get; set; }
        public string Version { get; set; }
        public decimal? scaled_passing_score { get; set; }   // added by sarita
        public decimal? progress_measure { get; set; }   // added by sarita

       /// <summary>
       /// SCORM 2004 Additional Data Models
       /// </summary>
       /// 
        public string SuccessStatus { get; set; }

        public LessonTracking2004()
        {
            //TotalTime = new TimeSpan(0);
            Objectives = new List<ObjectiveTracking>();
            Interactions = new List<InteractionTracking>();
        }

        public bool IsComplete
        {
            get
            {
                if (String.IsNullOrEmpty(LessonStatus))
                    return false;
                var firstLetter = LessonStatus.Substring(0, 1).ToLower();
                return (firstLetter == "p" || firstLetter == "c");
            }
        }
        public bool ForCredit
        {
            get
            {
                return String.IsNullOrEmpty(Credit) ||
                       Credit.StartsWith("c", StringComparison.InvariantCultureIgnoreCase);
            }
        }
        public bool BrowseMode
        {
            get
            {
                return !String.IsNullOrEmpty(LessonMode) &&
                       LessonMode.StartsWith("b", StringComparison.InvariantCultureIgnoreCase);
            }
        }
    }
}
