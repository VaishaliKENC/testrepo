using System;
using System.Collections.Generic;

namespace YPLMS2._0.API.Entity
{
   [Serializable] public class AuTracking : LessonTracking
    {
        public AuTracking()
        {
            TotalTime = new TimeSpan(0);
            Objectives = new List<ObjectiveTracking>();
        }
    }
}
