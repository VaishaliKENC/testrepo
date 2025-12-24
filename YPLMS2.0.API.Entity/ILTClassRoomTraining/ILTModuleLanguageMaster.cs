/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:Bharat
* Created:<07/20/23>
* Last Modified:
*/

using System;


namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ILTModuleLanguageMaster : BaseEntity 
    /// </summary>
    /// 
    public class ILTModuleLanguageMaster : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ILTModuleLanguageMaster()
        { }


        //private string _strEventId;
        ///// <summary>
        ///// EventId
        ///// </summary>
        //public string EventId
        //{
        //get { return _strEventId; }
        //set { if (this._strEventId != value) { _strEventId = value; } }
        //}

        private string _strLanguageId;
        /// <summary>
        /// LanguageId
        /// </summary>
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }

        private string _strModuleName;
        /// <summary>
        /// ModuleName
        /// </summary>
        public string ModuleName
        {
            get { return _strModuleName; }
            set { if (this._strModuleName != value) { _strModuleName = value; } }
        }
        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete
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