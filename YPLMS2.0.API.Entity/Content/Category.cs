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
using System.Collections.Generic;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ProductCategory : ProductCategoryLanguage 
    /// </summary>
    /// 
    [Serializable]
    public class Category : CategoryLanguage 
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public Category()
        {
       
        }

        private bool? _strIsActive;
        /// <summary>
        /// IsActive
        /// </summary>
        public bool? IsActive
        {
            get { return _strIsActive; }
            set { if (this._strIsActive != value) { _strIsActive = value; } }
        }

        private string _strThumbnailImage;
        /// <summary>
        /// ThumbnailImage
        /// </summary>
        public string ThumbnailImage
        {
            get { return _strThumbnailImage; }
            set { if (this._strThumbnailImage != value) { _strThumbnailImage = value; } }
        }


        private string _strFlag;
        /// <summary>
        /// Flag
        /// </summary>
        public string Flag
        {
            get { return _strFlag; }
            set { if (this._strFlag != value) { _strFlag = value; } }
        }

        private string _strShortUrl;
        /// <summary>
        /// ShortUrl
        /// </summary>
        public string ShortUrl
        {
            get { return _strShortUrl; }
            set { if (this._strShortUrl != value) { _strShortUrl = value; } }
        }

        private bool? _isCertifiedCategory;
        /// <summary>
        /// IsActive
        /// </summary>
        public bool? IsCertifiedCategory
        {
            get { return _isCertifiedCategory; }
            set { if (this._isCertifiedCategory != value) { _isCertifiedCategory = value; } }
        }
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
            GET_IsShortUrlAvailable,
            SetCategoryStatus
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllLearner,
            BulkDelete,
            
            GetProductCategoryLanguages,
            GetAllCertifiedCategory
        }
    }
}