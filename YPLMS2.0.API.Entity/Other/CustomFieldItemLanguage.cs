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
    /// Serializable  CustomFieldItemLanguage inherited from BaseEntity
    /// </summary>
    [Serializable]
    public class CustomFieldItemLanguage : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public CustomFieldItemLanguage()
        { }

        private string _strCustomFieldItemId;
        /// <summary>
        /// Custom Field Item Id
        /// </summary>
        public string CustomFieldItemId
        {
            get { return _strCustomFieldItemId; }
            set { if (this._strCustomFieldItemId != value) { _strCustomFieldItemId = value; } }
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

        private string _strCustomFieldItemDisplayText;
        /// <summary>
        /// Custom Field Item DisplayText
        /// </summary>
        public string CustomFieldItemDisplayText
        {
            get { return _strCustomFieldItemDisplayText; }
            set { if (this._strCustomFieldItemDisplayText != value) { _strCustomFieldItemDisplayText = value; } }
        }

        public bool Validate(bool pIsUpdate)
        {
            if (String.IsNullOrEmpty(CustomFieldItemId))
                return false;

            if (String.IsNullOrEmpty(LanguageId))
                return false;

            if (String.IsNullOrEmpty(CustomFieldItemDisplayText))
                return false;

            return true;
        }
    }
}