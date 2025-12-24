using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class OrderDetails:BaseEntity
    {

        public string OrderID  { get; set; }
        public string ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductDescription { get; set; }
        public float Price { get; set; }

        public float TaxAmount { get; set; }

        public float TaxRate { get; set; }

        public string TaxCode { get; set; }


        public int Qty { get; set; }


        public int NoofLicense { get; set; }
        public string VoucherId { get; set; }
        public string VoucherCode { get; set; }

        public string ProductCode { get; set; }

        public float Discount { get; set; }

        public float TotalPrice { get; set; }
        public new enum Method
        {
            Get,            
            Add,
            Update,
            Delete,
            IsVoucherAvailable
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {

            GetAll,
            BulkUpdate

        }

    }
}
