/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<6/18/2013>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ProductCategoryLanguage : BaseEntity 
    /// </summary>
    /// 
    [Serializable]
    public class CategoryLanguage : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public CategoryLanguage()
        { }


        private string _strLanguageId;
        /// <summary>
        /// LanguageId
        /// </summary>
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }
        private string _strLanguageName;
        /// <summary>
        /// LanguageId
        /// </summary>
        public string LanguageName
        {
            get { return _strLanguageName; }
            set { if (this._strLanguageName != value) { _strLanguageName = value; } }
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


        private string _strName;
        /// <summary>
        /// GroupName
        /// </summary>
        public string Name
        {
            get { return _strName; }
            set { if (this._strName != value) { _strName = value; } }
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


        private string _strCategoryMetakeyword;
        /// <summary>
        /// CategoryMetakeyword
        /// </summary>
        public string CategoryMetakeyword
        {
            get { return _strCategoryMetakeyword; }
            set { if (this._strCategoryMetakeyword != value) { _strCategoryMetakeyword = value; } }
        }

        private string _strCategoryMetaDescription;
        /// <summary>
        /// CategoryMetaDescription
        /// </summary>
        public string CategoryMetaDescription
        {
            get { return _strCategoryMetaDescription; }
            set { if (this._strCategoryMetaDescription != value) { _strCategoryMetaDescription = value; } }
        }

        private string _strCategoryMetaTitle;
        /// <summary>
        /// CategoryMetaTitle
        /// </summary>
        public string CategoryMetaTitle
        {
            get { return _strCategoryMetaTitle; }
            set { if (this._strCategoryMetaTitle != value) { _strCategoryMetaTitle = value; } }
        }

        private string _strCategoryAnalyticCode;
        /// <summary>
        /// CategoryAnalyticCode
        /// </summary>
        public string CategoryAnalyticCode
        {
            get { return _strCategoryAnalyticCode; }
            set { if (this._strCategoryAnalyticCode != value) { _strCategoryAnalyticCode = value; } }
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