/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<5/21/2018>
* Last Modified:
*/

using System;
using System.Collections.Generic;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ProductGroup : ProductGroupLanguage 
    /// </summary>
    /// 
    [Serializable]
    public class ProductGroup : ProductGroupLanguage
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ProductGroup()
        { }


        private string _strGroupCode;
        /// <summary>
        /// GroupCode
        /// </summary>
        public string GroupCode
        {
            get { return _strGroupCode; }
            set { if (this._strGroupCode != value) { _strGroupCode = value; } }
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


        private string _strFlag;
        /// <summary>
        /// ShortUrl
        /// </summary>
        public string Flag
        {
            get { return _strFlag; }
            set { if (this._strFlag != value) { _strFlag = value; } }
        }

        private Int32 _Sequence;
        /// <summary>
        /// Sequence
        /// </summary>
        public Int32 Sequence
        {
            get { return _Sequence; }
            set { if (this._Sequence != value) { _Sequence = value; } }
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




        private bool? _strIsActive;
        // private bool _strIsActive;
        /// <summary>
        /// IsActive
        /// </summary>
        //public bool? IsActive
        //{
        //    get { return _strIsActive; }
        //    set { if (this._strIsActive != value) { _strIsActive = value; } }
        //}

        public bool? IsActive
        {
            get { return _strIsActive; }
            set { if (this._strIsActive != value) { _strIsActive = value; } }
        }


        private string _strMappedProductid;

        public string MappedProductid
        {
            get { return _strMappedProductid; }
            set { if (this._strMappedProductid != value) { _strMappedProductid = value; } }
        }

        public string TypeOfReq { get; set; }

        List<Products> ObjProductLst = new List<Products>();
        public List<Products> ProductLst
        {
            get { return ObjProductLst; }
            set { if (this.ObjProductLst != value) { ObjProductLst = value; } }
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            SetStaus,
            GetByGroupName,
            GetByGroupCode,
            GetByShortUrl,
            GetCatalog,
            GetAddMappedProductGroups,
            UpdateGroupSequence

        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllForLearner,                       
            GetAllProductGroups,
            GetAllProductGroupsCategory


        }

    }
}
