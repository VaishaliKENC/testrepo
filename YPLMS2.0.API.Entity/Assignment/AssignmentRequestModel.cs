using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;
using System.Data;


namespace YPLMS2._0.API.Entity
{
    
    public class AssignmentRequestModel
    {
        public string ClientId { get; set; }
        public string CurrentUserID { get; set; }
        public bool SendMail { get; set; }
        public bool MailOptionsVisible { get; set; }
        public bool IsAutoEmail { get; set; }
        public bool ForceReassignment { get; set; }
        public bool SendAlerts { get; set; }
        public string BusinessRuleId { get; set; }
        public bool IsProductAdmin { get; set; }
        public List<ActivityModel> Activities { get; set; }
        public List<LearnerModel> Learners { get; set; }

        

        // Nested class for Activity
        public class ActivityModel
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public bool IsSelected { get; set; }
        }

        // Nested class for Learner
        public class LearnerModel
        {
            public string ID { get; set; }
            public bool IsSelected { get; set; }
            public string RegistrationDate { get; set; }
        }

       
            public int AssignmentDateType { get; set; } // Corresponds to ddlAssignmentDate.SelectedIndex
            public string? AssignmentDateText { get; set; }
            public string? AssignmentDaysText { get; set; }

            public bool NoDueDate { get; set; }
            public int DueDateType { get; set; }
            public string? DueDateText { get; set; }
            public string? DueDaysText { get; set; }

            public bool NoExpiryDate { get; set; }
            public int ExpiryDateType { get; set; }
            public string? ExpiryDateText { get; set; }
            public string? ExpiryDaysText { get; set; }


       
            public bool IsReassignmentDateEmpty { get; set; }

            public int ReassignmentDateType { get; set; } // ddlReassignmentDate.SelectedIndex
            public string? ReassignmentDateText { get; set; }
            public string? ReassignmentDaysText { get; set; }

            public bool NoReassignDueDate { get; set; }
            public int ReassignDueDateType { get; set; } // ddlReDueDate.SelectedIndex
            public string? ReassignDueDateText { get; set; }
            public string? ReassignDueDaysText { get; set; }

            public bool NoReassignExpiryDate { get; set; }
            public int ReassignExpiryDateType { get; set; } // ddlReExpiryDate.SelectedIndex
            public string? ReassignExpiryDateText { get; set; }
            public string? ReassignExpiryDaysText { get; set; }
        

    }

}
