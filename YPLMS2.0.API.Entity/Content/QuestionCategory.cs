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
    /// class QuestionCategory : BaseEntity 
    /// </summary>
    /// 
    public class QuestionCategory : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public QuestionCategory()
        { }


        private string _strCategoryName;
        /// <summary>
        /// CategoryName
        /// </summary>
        public string CategoryName
        {
            get { return _strCategoryName; }
            set { if (this._strCategoryName != value) { _strCategoryName = value; } }
        }

        private string _strCategoryDescription;
        /// <summary>
        /// CategoryDescription
        /// </summary>
        public string CategoryDescription
        {
            get { return _strCategoryDescription; }
            set { if (this._strCategoryDescription != value) { _strCategoryDescription = value; } }
        }




        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetByName,
            Add,
            Update,
            Delete,
            IsCategoryNameAvailable,
            IsCategoryNameExits
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkDelete
        }
    }
}