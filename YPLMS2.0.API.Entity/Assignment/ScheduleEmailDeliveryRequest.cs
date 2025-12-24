using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.Entity
{    
        public class ScheduleEmailDeliveryRequest
        {
            public string TemplateId { get; set; }
            public string EmailDeliveryTitle { get; set; }
            public string ToList { get; set; }
            public bool CbxCCManager { get; set; }
            public bool CbxBCCManager { get; set; }
            public string CC { get; set; }
            public string BCC { get; set; }
            public string EmailLanguage { get; set; }
            public string SendToAllOrRecipients { get; set; }
            public bool IsImmediate { get; set; }
            public string StartDate { get; set; }
            public string StartHour { get; set; }
            public string StartMinute { get; set; }
            public string StartAMPM { get; set; }
            public string ScheduleType { get; set; }
            public string DaysWeekly { get; set; }
            public string DaysMonthly { get; set; }
            public bool RequireApproval { get; set; }
            public string EndDate { get; set; }
        }

    
}
