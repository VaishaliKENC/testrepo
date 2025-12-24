/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<6/28/2013>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class Products : ProductsLanguage 
    /// </summary>
    /// 
    [Serializable]
    public class Products : ProductsLanguage
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public Products()
        { }


        private string _strProductCode;
        /// <summary>
        /// ProductCode
        /// </summary>
        public string ProductCode
        {
            get { return _strProductCode; }
            set { if (this._strProductCode != value) { _strProductCode = value; } }
        }

        private string _strProductLicenseType;
        /// <summary>
        /// ProductPurchaseType
        /// </summary>
        public string ProductLicenseType
        {
            get { return _strProductLicenseType; }
            set { if (this._strProductLicenseType != value) { _strProductLicenseType = value; } }
        }

        private string _strRedirectionProdId;
        /// <summary>
        /// RedirectionProdId
        /// </summary>

        public string RedirectionProdId
        {
            get { return _strRedirectionProdId; }
            set { if (this._strRedirectionProdId != value) { _strRedirectionProdId = value; } }
        }

        private Nullable<bool> _strIsRedirection;
        /// <summary>
        /// IsRedirection
        /// </summary>
        public Nullable<bool> IsRedirection
        {
            get { return _strIsRedirection; }
            set { if (this._strIsRedirection != value) { _strIsRedirection = value; } }
        }
        // private string _strNoofLicense;
        /// <summary>
        /// NoofLicense
        /// </summary>
        //public string NoofLicense
        //{
        //    get { return _strNoofLicense; }
        //    set { if (this._strNoofLicense != value) { _strNoofLicense = value; } }
        //}
        public int NoofLicense { get; set; }


        private string _strCategoryId;
        /// <summary>
        /// CategoryId
        /// </summary>
        public string CategoryId
        {
            get { return _strCategoryId; }
            set { if (this._strCategoryId != value) { _strCategoryId = value; } }
        }

        private string _strCategoryName;
        /// <summary>
        /// CategoryId
        /// </summary>
        public string CategoryName
        {
            get { return _strCategoryName; }
            set { if (this._strCategoryName != value) { _strCategoryName = value; } }
        }

        private string _strLocationId;
        /// <summary>
        /// LocationId
        /// </summary>
        public string LocationId
        {
            get { return _strLocationId; }
            set { if (this._strLocationId != value) { _strLocationId = value; } }
        }

        private eProductType _strProductType;
        /// <summary>
        /// ProductType
        /// </summary>
        public eProductType ProductType
        {
            get { return _strProductType; }
            set { if (this._strProductType != value) { _strProductType = value; } }
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

        private string _strActivityId;
        /// <summary>
        /// ActivityId
        /// </summary>
        public string ActivityId
        {
            get { return _strActivityId; }
            set { if (this._strActivityId != value) { _strActivityId = value; } }
        }

        private string _strActivityName;
        /// <summary>
        /// ActivityId
        /// </summary>
        public string ActivityName
        {
            get { return _strActivityName; }
            set { if (this._strActivityName != value) { _strActivityName = value; } }
        }

        private string _strActivityTypeId;
        /// <summary>
        /// ActivityTypeId
        /// </summary>
        public string ActivityTypeId
        {
            get { return _strActivityTypeId; }
            set { if (this._strActivityTypeId != value) { _strActivityTypeId = value; } }
        }

        private string _strActivityTypeIdEng;
        /// <summary>
        /// ActivityTypeId
        /// </summary>
        public string ActivityTypeIdEng
        {
            get { return _strActivityTypeIdEng; }
            set { if (this._strActivityTypeIdEng != value) { _strActivityTypeIdEng = value; } }
        }


        
        private string _strTaxcCode;
        public string TaxCode
        {
            get { return _strTaxcCode; }
            set { if (this._strTaxcCode != value) { _strTaxcCode = value; } }
        }

        private System.Nullable<bool> _strIsActive;
        /// <summary>
        /// IsActive
        /// </summary>
        public System.Nullable<bool> IsActive
        {
            get { return _strIsActive; }
            set { if (this._strIsActive != value) { _strIsActive = value; } }
        }

        private Nullable<bool> _strIsPublished;
        /// <summary>
        /// IsPublished
        /// </summary>
        public Nullable<bool> IsPublished
        {
            get { return _strIsPublished; }
            set { if (this._strIsPublished != value) { _strIsPublished = value; } }
        }

        private DateTime _strPublishDate;
        /// <summary>
        /// PublishDate
        /// </summary>
        public DateTime PublishDate
        {
            get { return _strPublishDate; }
            set { if (this._strPublishDate != value) { _strPublishDate = value; } }
        }

        private double _strBasePrice;
        /// <summary>
        /// PublishDate
        /// </summary>
        public double BasePrice
        {
            get { return _strBasePrice; }
            set { if (this._strBasePrice != value) { _strBasePrice = value; } }
        }

        public double _strDiscountedPrice;
        /// <summary>
        /// DiscountedPrice
        /// </summary>
        public double DiscountedPrice
        {
            get { return _strDiscountedPrice; }
            set { if (this._strDiscountedPrice != value) { _strDiscountedPrice = value; } }
        }

        private string _strGroupShortUrl;
        public string GroupShortUrl
        {
            get { return _strGroupShortUrl; }
            set { if (this._strGroupShortUrl != value) { _strGroupShortUrl = value; } }
        }
        private string _strCategoryShortUrl;
        public string CategoryShortUrl
        {
            get { return _strCategoryShortUrl; }
            set { if (this._strCategoryShortUrl != value) { _strCategoryShortUrl = value; } }
        }

        public string ProductRegType { get; set; }
        public string EnrollKey { get; set; }
        public string AdminEmail { get; set; }
        public string SubCategoryId { get; set; }
        public bool IsAssignedActivity { get; set; }
        public string SystemUserGUID { get; set; }
        public string SubmissionStatus { get; set; }
        public DateTime? ActivityExpiryDate { get; set; }
        public DateTime? ProductExpiryDate { get; set; }
        public bool IsReqSent { get; set; }
        public string TypeOfReq { get; set; }

        public int Quantity { get; set; }

        public string ProductId { get; set; }
        public int CardId { get; set; }

        private string _strSignUpID;
        /// <summary>
        /// ActivityTypeId
        /// </summary>
        public string SignUpID
        {
            get { return _strSignUpID; }
            set { if (this._strSignUpID != value) { _strSignUpID = value; } }
        }

        private string _strRelatedProdID;
        /// <summary>
        /// ActivityTypeId
        /// </summary>
        public string RelatedProdID
        {
            get { return _strRelatedProdID; }
            set { if (this._strRelatedProdID != value) { _strRelatedProdID = value; } }
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

        private string _strExpiryMessage;
        /// <summary>
        /// ExpiryMessage
        /// </summary>
        public string ExpiryMessage
        {
            get { return _strExpiryMessage; }
            set { if (this._strExpiryMessage != value) { _strExpiryMessage = value; } }
        }

        private DateTime _strExpiryDate;
        /// <summary>
        /// ExpiryDate
        /// </summary>
        public DateTime ExpiryDate
        {
            get { return _strExpiryDate; }
            set { if (this._strExpiryDate != value) { _strExpiryDate = value; } }
        }

        private DateTime _strExpiryDateN;
        /// <summary>
        /// ExpiryDate for download excel
        /// </summary>
        public DateTime ExpiryDateN
        {
            get { return _strExpiryDateN; }
            set { if (this._strExpiryDateN != value) { _strExpiryDateN = value; } }
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetCatalog,
            GetByProductCode,
            Add,
            Update,
            Publish,
            DeleteItemCart,
            Delete,
            UpdateLanguage,
            AddCart,
            SetSaveCartItem,
            GetByShortUrl,
            IsShortUrlAvailable,
            GetProdRedirectionCatalog

            //GetAllSaveCartItem
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllCatalog,
            ActivateDeActivateStatus,
            GetImportLanguages,
            GetProductsLanguages,
            GetExportLanguages,
            GetAddRecentlyViewedProducts,
            GetAllSaveCartItem,
            GetAllDeactivateCartItem,
            GetAddRelatedProducts

        }

    }

    /// <summary>
    /// enum Product Type
    /// </summary>
    public enum eProductType
    {
        Internal,
        External
    }

    public enum eProductRegType
    {
        FLGPUR,
        FLGKEY,
        FLGAUTH,
        FLGREQ
    }
}