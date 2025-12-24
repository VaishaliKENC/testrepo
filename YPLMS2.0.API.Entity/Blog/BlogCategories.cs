/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<5/29/2013>
* Last Modified:
*/

using System.Collections.Generic;
using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class BlogCategories : BlogCategoriesLanguage 
    /// </summary>
    /// 
    public class BlogCategories : BlogCategoriesLanguage
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public BlogCategories()
        { }


        private string _strParentID;
        /// <summary>
        /// ParentID
        /// </summary>
        public string ParentID
        {
            get { return _strParentID; }
            set { if (this._strParentID != value) { _strParentID = value; } }
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
            GET_IsNameAvailable
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