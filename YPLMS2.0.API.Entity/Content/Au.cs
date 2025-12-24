using System;
namespace YPLMS2._0.API.Entity
{
   [Serializable] public class Au : Lesson
    {
        public string Href { get; set; }
        public string WebLaunch { get; set; }
        public string AuPassword { get; set; }
        public string CoreVendor { get; set; }
    }
}
