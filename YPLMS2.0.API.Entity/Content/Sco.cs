using System;
namespace YPLMS2._0.API.Entity
{
   [Serializable] public class Sco : Lesson
    {
        public string TargetWindow { get; set; }

        public string Prerequisites { get; set; }

        public string DataFromLms { get; set; }

        public ScormResource Resource { get; set; }
    }
}
