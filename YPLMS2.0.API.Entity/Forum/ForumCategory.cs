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
    /// class ForumCategory : BaseEntity 
    /// </summary>
    /// 
    [Serializable]
    public class ForumCategory : ForumCategoryLanguage
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ForumCategory()
        { }


        ////private string _strLanguageID;
        /////// <summary>
        /////// LanguageID
        /////// </summary>
        ////public string LanguageID
        ////{
        ////    get { return _strLanguageID; }
        ////    set { if (this._strLanguageID != value) { _strLanguageID = value; } }
        ////}

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