using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class CategoryMapping:BaseEntity
    {
         /// <summary>
        /// Default Contructor
        /// </summary>
        public CategoryMapping()
        {
       
        }


       // public string ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityTypeId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }

        /// <summary>
        /// enum Method
        /// </summary>
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
            BulkDelete,
            GetAllCertifiedPrograms
            
        }
    }
}
