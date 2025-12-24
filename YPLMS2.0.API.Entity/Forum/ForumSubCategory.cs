/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<6/13/2013>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ForumSubCategory : BaseEntity 
    /// </summary>
    /// 
    [Serializable]
    public class ForumSubCategory : ForumSubCategoryLanguage
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ForumSubCategory()
        { }


        private string _strCategoryID;
        /// <summary>
        /// CategoryID
        /// </summary>
        public string CategoryID
        {
            get { return _strCategoryID; }
            set { if (this._strCategoryID != value) { _strCategoryID = value; } }
        }


        private bool _strIsModerationRequired;
        /// <summary>
        /// IsModerationRequired
        /// </summary>
        public bool IsModerationRequired
        {
            get { return _strIsModerationRequired; }
            set { if (this._strIsModerationRequired != value) { _strIsModerationRequired = value; } }
        }

        private string _strCategoryName;
        /// <summary>
        ///CategoryName
        /// </summary>
        public string CategoryName
        {
            get { return _strCategoryName; }
            set { if (this._strCategoryName != value) { _strCategoryName = value; } }
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