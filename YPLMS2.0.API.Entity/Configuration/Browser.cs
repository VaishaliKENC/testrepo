/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<10/23/2013>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class Browser : BaseEntity 
    /// </summary>
    /// 
    public class Browser : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public Browser()
        { }


        private string _strOS;
        /// <summary>
        /// OS
        /// </summary>
        public string OS
        {
            get { return _strOS; }
            set { if (this._strOS != value) { _strOS = value; } }
        }

        private string _strOSShortName;
        /// <summary>
        /// OS
        /// </summary>
        public string OSShortName
        {
            get { return _strOSShortName; }
            set { if (this._strOSShortName != value) { _strOSShortName = value; } }
        }

        private string _strBrowserName;
        /// <summary>
        /// BrowserName
        /// </summary>
        public string BrowserName
        {
            get { return _strBrowserName; }
            set { if (this._strBrowserName != value) { _strBrowserName = value; } }
        }

        private decimal _strMajorVersion;
        /// <summary>
        /// MajorVersion
        /// </summary>
        public decimal MajorVersion
        {
            get { return _strMajorVersion; }
            set { if (this._strMajorVersion != value) { _strMajorVersion = value; } }
        }

        private decimal _strMinorVersion;
        /// <summary>
        /// MinorVersion
        /// </summary>
        public decimal MinorVersion
        {
            get { return _strMinorVersion; }
            set { if (this._strMinorVersion != value) { _strMinorVersion = value; } }
        }

        private decimal _strMaxMajorVersion;
        /// <summary>
        /// MinorVersion
        /// </summary>
        public decimal MaxMajorVersion
        {
            get { return _strMaxMajorVersion; }
            set { if (this._strMaxMajorVersion != value) { _strMaxMajorVersion = value; } }
        }

        private string _strFlashVersion;
        /// <summary>
        /// FlashVersion
        /// </summary>
        public string FlashVersion
        {
            get { return _strFlashVersion; }
            set { if (this._strFlashVersion != value) { _strFlashVersion = value; } }
        }

        private bool _strIsSupport;
        /// <summary>
        /// IsSupport
        /// </summary>
        public bool IsSupport
        {
            get { return _strIsSupport; }
            set { if (this._strIsSupport != value) { _strIsSupport = value; } }
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
            GetByBrowser
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllSearch
        }
    }
}