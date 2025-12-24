using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ProductSubCategoryLanguage : BaseEntity 
    /// </summary>
    /// 
    [Serializable]
    public class SubCategoryLanguage : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public SubCategoryLanguage()
        { }


        private string _strLanguageName;
        /// <summary>
        /// LanguageID
        /// </summary>
        public string LanguageName
        {
            get { return _strLanguageName; }
            set { if (this._strLanguageName != value) { _strLanguageName = value; } }
        }

        private string _strCategoryID;
        /// <summary>
        /// CategoryID
        /// </summary>
        public string CategoryID
        {
            get { return _strCategoryID; }
            set { if (this._strCategoryID != value) { _strCategoryID = value; } }
        }

        private string _strLanguageID;
        /// <summary>
        /// LanguageID
        /// </summary>
        public string LanguageID
        {
            get { return _strLanguageID; }
            set { if (this._strLanguageID != value) { _strLanguageID = value; } }
        }

        private string _strSubCategoryName;
        /// <summary>
        /// SubCategoryName
        /// </summary>
        public string SubCategoryName
        {
            get { return _strSubCategoryName; }
            set { if (this._strSubCategoryName != value) { _strSubCategoryName = value; } }
        }

        private string _strDescription;
        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get { return _strDescription; }
            set { if (this._strDescription != value) { _strDescription = value; } }
        }


      
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
            GetAll
        }
    }
}
