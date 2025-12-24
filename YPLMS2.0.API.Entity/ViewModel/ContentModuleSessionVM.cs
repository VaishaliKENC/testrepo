using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.Entity.ViewModel
{
    public enum LaunchSite
    {
        Learner,
        Admin
    }
   
    public class ContentModuleSessionVM
    {
        public string ClientId { get; set; }

        public string ContentModuleId { get; set; }

        public string SystemUserGuid { get; set; }

        public int? Attempt { get; set; }

        public LaunchSite LaunchSite { get; set; }

        public bool IsReview { get; set; }

        public bool SsoLogin { get; set; }

        public bool SameWindow { get; set; }

        public string ReturnUrl { get; set; }

        public int? GridPageSize { get; set; }

      
    }
}
