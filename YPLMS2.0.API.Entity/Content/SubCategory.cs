using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ForumSubCategory : BaseEntity 
    /// </summary>
    /// 
    [Serializable]
    public class SubCategory : SubCategoryLanguage
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public SubCategory()
        { }


        private bool? _strIsActive;
        /// <summary>
        /// IsActive
        /// </summary>
        public bool? IsActive
        {
            get { return _strIsActive; }
            set { if (this._strIsActive != value) { _strIsActive = value; } }
        }

        public string CategoryName { get; set; }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete,
            UpdateLanguage,
            GET_IsNameAvailable,
            SetCategoryStatus
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkDelete,
            GetProductSubCategoryLanguages
        }
    }
}
