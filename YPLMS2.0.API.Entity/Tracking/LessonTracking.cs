using System;
using System.Collections.Generic;

namespace YPLMS2._0.API.Entity
{
   [Serializable] public class LessonTracking
    {
        public string? Identifier { get; set; }
        public string? StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? ManagerEmail { get; set; }
        public string? LessonLocation { get; set; }
        public string? Credit { get; set; }
        public string? LessonStatus { get; set; }
        public string? Entry { get; set; }
        public decimal? RawScore { get; set; }
        public decimal? MinScore { get; set; }
        public decimal? MaxScore { get; set; }
        public string? Exit { get; set; }
        public TimeSpan? SessionTime { get; set; }
        public TimeSpan TotalTime { get; set; }
        public string? LessonMode { get; set; }
        public string? SuspendData { get; set; }
        public string? LaunchData { get; set; }
        public string? Comments { get; set; }
        public string? CommentsFromLms { get; set; }
        public List<ObjectiveTracking>? Objectives { get; set; }
        public int? MasteryScore { get; set; }
        public TimeSpan? MaxTimeAllowed { get; set; }
        public string? TimeLimitAction { get; set; }
        public int? totalpages { get; set; }
        public int? completedpages { get; set; }
        public string? CourseId
        {
            get;
            set;
        }
        public string? ClientId
        {
            get;
            set;
        }
        public bool? audio { get; set; }
        public string? language { get; set; }
        public int? speed { get; set; }
        public string? text { get; set; }
        public int? InteractionCount { get; set; }
        public string? sessionId { get; set; }

        public LessonTracking()
        {
            TotalTime = new TimeSpan(0);
            Objectives = new List<ObjectiveTracking>();
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
