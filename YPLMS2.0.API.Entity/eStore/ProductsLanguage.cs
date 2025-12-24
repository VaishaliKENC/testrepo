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
    /// class ProductsLanguage : BaseEntity 
    /// </summary>
    /// 
    [Serializable]
    public class ProductsLanguage : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ProductsLanguage()
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

        private string _strProductTitle;
        /// <summary>
        /// ProductTitle
        /// </summary>
        public string ProductTitle
        {
            get { return _strProductTitle; }
            set { if (this._strProductTitle != value) { _strProductTitle = value; } }
        }

        private string _strProductDescription;
        /// <summary>
        /// ProductDescription
        /// </summary>
        public string ProductDescription
        {
            get { return _strProductDescription; }
            set { if (this._strProductDescription != value) { _strProductDescription = value; } }
        }

        private string _strProductMetakeyword;
        /// <summary>
        /// ProductMetakeyword
        /// </summary>
        public string ProductMetakeyword
        {
            get { return _strProductMetakeyword; }
            set { if (this._strProductMetakeyword != value) { _strProductMetakeyword = value; } }
        }

        private string _strProductMetaDescription;
        /// <summary>
        /// ProductMetaDesription
        /// </summary>
        public string ProductMetaDescription
        {
            get { return _strProductMetaDescription; }
            set { if (this._strProductMetaDescription != value) { _strProductMetaDescription = value; } }
        }

        private string _strProductMetaTitle;
        /// <summary>
        /// ProductMetaTitle
        /// </summary>
        public string ProductMetaTitle
        {
            get { return _strProductMetaTitle; }
            set { if (this._strProductMetaTitle != value) { _strProductMetaTitle = value; } }
        }


        private string _strAnalyticCode;
        /// <summary>
        /// ProductMetaTitle
        /// </summary>
        public string ProductAnalyticCode
        {
            get { return _strAnalyticCode; }
            set { if (this._strAnalyticCode != value) { _strAnalyticCode = value; } }
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