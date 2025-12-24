/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh & Ashish
* Created:<13/07/09>
* Last Modified:<dd/mm/yy>
*/
using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ContentModule:BaseEntity 
    /// </summary>
   [Serializable] public class ContentModuleLanguages : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        ///</summary>

        public ContentModuleLanguages()
        { }

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

        private string _strContentModuleName;
        /// <summary>
        /// Content Module Name
        /// </summary>
        public string ContentModuleName
        {
            get { return _strContentModuleName; }
            set { if (this._strContentModuleName != value) { _strContentModuleName = value; } }
        }

        private string _strContentModuleDescription;
        /// <summary>
        /// Content Module Description
        /// </summary>
        public string ContentModuleDescription
        {
            get { return _strContentModuleDescription; }
            set { if (this._strContentModuleDescription != value) { _strContentModuleDescription = value; } }
        }

        private string _strLanguageId;
        /// <summary>
        /// Language Id
        /// </summary>
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }

        private string _strContentModuleKeyWords;
        /// <summary>
        /// ContentModuleKeyWords
        /// </summary>
        public string ContentModuleKeyWords
        {
            get { return _strContentModuleKeyWords; }
            set { if (this._strContentModuleKeyWords != value) { _strContentModuleKeyWords = value; } }
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="pIsUpdate"></param>
        /// <returns></returns>
        public bool ValidateParent(bool pIsUpdate)
        {
            if (pIsUpdate)
            {
                if (String.IsNullOrEmpty(ID))
                    return false;
            }
            else
            {
                if (String.IsNullOrEmpty(ContentModuleName))
                    return false;



                if (String.IsNullOrEmpty(LanguageId))
                    return false;


            }



            return true;
        }
    }
}