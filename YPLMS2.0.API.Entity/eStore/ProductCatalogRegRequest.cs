using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace YPLMS2._0.API.Entity
{
    public class ProductCatalogRegRequest : BaseEntity
    {

        public string ProductId { get; set; }
        public string SystemUserGUID { get; set; }       
        public string Status { get; set; }
        public string ProductTitle { get; set; }
        public string UserNameAlias { get; set; }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,           
            Add,
            Update,          
            Delete,
            Get_IsActivityAssigned
            
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll          
        }
    }
}
