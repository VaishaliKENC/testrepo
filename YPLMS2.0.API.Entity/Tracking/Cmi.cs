using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.Entity.Tracking
{
    public class Cmi
    {
        public Core core { get; set; }
        public string suspend_data { get; set; }
        public string launch_data { get; set; }
        public string comments { get; set; }
        public string comments_from_lms { get; set; }
        public Objectives objectives { get; set; }
        public StudentData student_data { get; set; }
        public StudentPreference student_preference { get; set; }
        public Interactions interactions { get; set; }
    }
    public class Core
    {
        public string student_id { get; set; }
        public string student_name { get; set; }
        public string lesson_location { get; set; }
        public string lesson_status { get; set; }
        public string credit { get; set; }
        public string entry { get; set; }
        public Score score { get; set; }
        public string total_time { get; set; }
        public string lesson_mode { get; set; }
        public string exit { get; set; }
        public string session_time { get; set; }
        public string totalpages { get; set; }
        public string completedpages { get; set; }
    }
    public class Score
    {
        public string raw { get; set; }
        public string max { get; set; }
        public string min { get; set; }
    }
    public class Objectives
    {
        public string _count { get; set; }
    }
    public class StudentData
    {
        public string mastery_score { get; set; }
        public string max_time_allowed { get; set; }
        public string time_limit_action { get; set; }
    }
    public class StudentPreference
    {
        public bool audio { get; set; }
        public string language { get; set; }
        public string speed { get; set; }
        public string text { get; set; }
    }
    public class Interactions
    {
        public string _count { get; set; }
    }
}
