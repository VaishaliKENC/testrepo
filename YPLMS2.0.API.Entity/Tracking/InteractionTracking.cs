using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class InteractionTracking
    {
         
        public string Identifier { get; set; }
        public string Type { get; set; }
        public List<InteractionObjectiveTracking> Objective_Tracking { get; set; }
        public TimeSpan? TimeStamp { get; set; }
        public List<InteractionCorrectResponses> Correct_Responses { get; set; }
        public decimal? Weighting { get; set; }
        public string Learner_Response { get; set; }
        public string Result { get; set; }
        public TimeSpan? Latencey { get; set; }
        public string Description { get; set; }
    }
}
