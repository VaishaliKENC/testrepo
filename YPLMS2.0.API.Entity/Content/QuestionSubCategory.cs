
using System;

namespace YPLMS2._0.API.Entity
{
   public class QuestionSubCategory:BaseEntity
    {
         /// <summary>
        /// Default Contructor
        /// </summary>
        public QuestionSubCategory()
        { }

        private string _strCategoryID;
        /// <summary>
        /// CategoryName
        /// </summary>
        public string CategoryID
        {
            get { return _strCategoryID; }
            set { if (this._strCategoryID != value) { _strCategoryID = value; } }
        }

        private string _strCategoryName;
        /// <summary>
        /// CategoryName
        /// </summary>
        public string CategoryName
        {
            get { return _strCategoryName; }
            set { if (this._strCategoryName != value) { _strCategoryName = value; } }
        }

        private string _strSubCategoryName;
        /// <summary>
        /// CategoryName
        /// </summary>
        public string SubCategoryName
        {
            get { return _strSubCategoryName; }
            set { if (this._strSubCategoryName != value) { _strSubCategoryName = value; } }
        }

        private string _strSubCategoryDescription;
        /// <summary>
        /// CategoryDescription
        /// </summary>
        public string SubCategoryDescription
        {
            get { return _strSubCategoryDescription; }
            set { if (this._strSubCategoryDescription != value) { _strSubCategoryDescription = value; } }
        }




        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetByName,
            Add,
            Update,
            Delete,
            IsSubCategoryNameAvailable,
            IsSubCategoryNameExits
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll, GetByCategoryID,
            BulkDelete
        }
    }
}
