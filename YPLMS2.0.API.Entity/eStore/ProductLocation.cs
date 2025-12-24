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
    /// class ProductLocation : ProductLocationLanguage 
    /// </summary>
    /// 
    public class ProductLocation : ProductLocationLanguage 
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ProductLocation()
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
            SetStaus
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkDelete,
            GetProductLocationLanguages
        }
    }
}