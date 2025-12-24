using System;
namespace YPLMS2._0.API.Entity
{
   [Serializable] public class ObjectiveTracking
    {
        public string? Identifier { get; set; }
        public int? RawScore { get; set; }
        public int? MinScore { get; set; }
        public int? MaxScore { get; set; }
        public decimal? Scaled { get; set; }
        public string? status { get; set; }
        public string? success_status { get; set; }
        public string? description { get; set; }
        public ObjectiveType enumObjectiveType { get; set; }
        public decimal? Progress_Measure { get; set; }
        public string? CompletionStatus { get; set; }
        public string? CompletionStatusChangedDuringRuntime { get; set; }
        public string? MeasureChangedDuringRuntime { get; set; }
        public string? ProgressMeasureChangedDuringRuntime { get; set; }
        public string? SuccessStatusChangedDuringRuntime { get; set; }
    }

   public enum ObjectiveType
   {
       Primary = 1,
       Normal = 2
    }


}
