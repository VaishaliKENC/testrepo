using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.Entity
{
    public class ActivityCertificateMapping : BaseEntity
    {
        public string ActivityName { get; set; }
        public string ActivityTypeId { get; set; }
        public string CertificateId { get; set; }
        public string CertificateName { get; set; }
        public string ActivityId { get; set; }
    
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllByCategory,
            BulkUpdate,
            BulkDelete

        }
    }
}
