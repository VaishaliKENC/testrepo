using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class ForumCategoryLanguage : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ForumCategoryLanguage()
        {

        }

        private string _strLanguageName;
        /// <summary>
        /// LanguageID
        /// </summary>
        public string LanguageName
        {
            get { return _strLanguageName; }
            set { if (this._strLanguageName != value) { _strLanguageName = value; } }
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

        private string _strCategoryName;
        /// <summary>
        /// CategoryName
        /// </summary>
        public string CategoryName
        {
            get { return _strCategoryName; }
            set { if (this._strCategoryName != value) { _strCategoryName = value; } }
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