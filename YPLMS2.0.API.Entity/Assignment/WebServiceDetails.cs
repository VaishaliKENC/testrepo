using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class WebServiceDetails : BaseEntity
    {
        /// <summary>
        /// Default Contructor nn
        /// </summary>
        public WebServiceDetails()
        {
        }

        /// <summary>
        /// ENUM Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete,
            GetByVendorCode
        }
        /// <summary>
        /// List method ENUM
        /// </summary>
        public new enum ListMethod
        {
            GetAll
        }

        public string ServiceUrl { get; set; }
        public string AuthKey { get; set; }
        public string AdminEmail { get; set; }
        public string VendorCode { get; set; }
        public string MedKey_code { get; set; }
    }
}
