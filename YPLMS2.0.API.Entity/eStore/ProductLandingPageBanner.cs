/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:Shrihari
* Created:<5/3/2019>
* Last Modified:
*/

using YPLMS2._0.API.Entity;
using System;
using System.Collections.Generic;


namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ProductLandingPageBanner : ProductLandingPageBannerLanguage 
    /// </summary>
    /// 
    [Serializable]
    public class ProductLandingPageBanner : ProductLandingPageBannerLanguage
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ProductLandingPageBanner()
        { }


        private string _strBannerAltText;
        /// <summary>
        /// BannerAltText
        /// </summary>
        public string BannerAltText
        {
            get { return _strBannerAltText; }
            set { if (this._strBannerAltText != value) { _strBannerAltText = value; } }
        }

        private string _strBannerName;
        /// <summary>
        /// BannerName
        /// </summary>
        public string BannerName
        {
            get { return _strBannerName; }
            set { if (this._strBannerName != value) { _strBannerName = value; } }
        }

        private float _strSequence;
        /// <summary>
        /// Sequence
        /// </summary>
        public float Sequence
        {
            get { return _strSequence; }
            set { if (this._strSequence != value) { _strSequence = value; } }
        }


        public string ProductId { get; set; }

        public string Shorturl { get; set; }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete,
            UpdateBannerSequence
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