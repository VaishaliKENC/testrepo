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
    /// Serializable - CustomFieldLanguage Class
    /// </summary>
    [Serializable]
     public class CustomFieldLanguage
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public CustomFieldLanguage()
        { }

        private string _strCustomFieldId;
        /// <summary>
        /// Custom Field Id
        /// </summary>
        public string CustomFieldId
        {
            get { return _strCustomFieldId; }
            set { if (this._strCustomFieldId != value) { _strCustomFieldId = value; } }
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

        private string _strCustomFieldDisplayText;
        /// <summary>
        /// Custom Field DisplayText
        /// </summary>
        public string CustomFieldDisplayText
        {
            get { return _strCustomFieldDisplayText; }
            set { if (this._strCustomFieldDisplayText != value) { _strCustomFieldDisplayText = value; } }
        }

        private string _strDescription;
        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get { return _strDescription; }
            set { if (this._strDescription != value) { _strDescription = value; } }
        }

        public bool Validate(bool pIsUpdate)
        {
            if (String.IsNullOrEmpty(CustomFieldId))
                return false;

            if (String.IsNullOrEmpty(LanguageId))
                return false;

            if (String.IsNullOrEmpty(CustomFieldDisplayText))
                return false;

            return true;
        }

    }
}