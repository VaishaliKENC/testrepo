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
    /// class ProductGroupLanguage : BaseEntity 
    /// </summary>
    /// 
    [Serializable]
    public class ProductGroupLanguage : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ProductGroupLanguage()
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

        private string _strGroupName;
        /// <summary>
        /// GroupName
        /// </summary>
        public string GroupName
        {
            get { return _strGroupName; }
            set { if (this._strGroupName != value) { _strGroupName = value; } }
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

        private string _strGroupDescription;
        /// <summary>
        /// GroupDescription
        /// </summary>
        public string GroupDescription
        {
            get { return _strGroupDescription; }
            set { if (this._strGroupDescription != value) { _strGroupDescription = value; } }
        }

        private string _strGroupMetakeyword;
        /// <summary>
        /// GroupMetakeyword
        /// </summary>
        public string GroupMetakeyword
        {
            get { return _strGroupMetakeyword; }
            set { if (this._strGroupMetakeyword != value) { _strGroupMetakeyword = value; } }
        }

        private string _strGroupMetaDescription;
        /// <summary>
        /// GroupMetaDesription
        /// </summary>
        public string GroupMetaDescription
        {
            get { return _strGroupMetaDescription; }
            set { if (this._strGroupMetaDescription != value) { _strGroupMetaDescription = value; } }
        }

        private string _strGroupMetaTitle;
        /// <summary>
        /// GroupMetaTitle
        /// </summary>
        public string GroupMetaTitle
        {
            get { return _strGroupMetaTitle; }
            set { if (this._strGroupMetaTitle != value) { _strGroupMetaTitle = value; } }
        }


        private string _strAnalyticCode;
        /// <summary>
        /// GroupMetaTitle
        /// </summary>
        public string GroupAnalyticCode
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
            Update
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