using System;

namespace YPLMS2._0.API.Entity
{
   [Serializable] public class UserSearchCriteria
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? LoginId { get; set; }
        public string? ManagerName { get; set; }
        public string? Email { get; set; }
        public string? LevelId { get; set; }
        public string? UnitId { get; set; }
        public DateTime? RegistrationDateFrom { get; set; }
        public DateTime? RegistrationDateTo { get; set; }
        public DateTime? TerminationDateFrom { get; set; }
        public DateTime? TerminationDateTo { get; set; }
        public bool? Active { get; set; }
        public string? RuleUsers { get; set; }
        public bool? IsTermsAndCondAccepted { get; set; }
    }
}
