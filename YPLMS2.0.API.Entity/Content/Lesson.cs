using System;
using System.Collections.Generic;

namespace YPLMS2._0.API.Entity
{
    public abstract class Lesson : IComparable<Lesson>
    {
        public string Identifier { get; set; }
        public string Title { get; set; }
        public int SortOrder { get; set; }
        public int? MasteryScore { get; set; }
        public TimeSpan? MaxTimeAllowed { get; set; }
        public string MaxTimeAllowed2004 { get; set; }   // added by sarita 
        public string TimeLimitAction { get; set; }
        public decimal? Scaled_Passing_Score { get; set; }    // added by sarita for  reading from manifest
        public List<ObjectiveTracking> Objectives { get; set; }

        public string TargetWindowSco { get; set; }

        public string PrerequisitesSco { get; set; }

        public string DataFromLmsSco { get; set; }

        public ScormResource ResourceSco { get; set; }

        public int CompareTo(Lesson other)
        {
            return SortOrder.CompareTo(other.SortOrder);
        }
    }
}
