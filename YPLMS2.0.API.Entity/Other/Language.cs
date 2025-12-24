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
    /// class Language inherited from BaseEntity 
    /// </summary>
   [Serializable] public class Language : BaseEntity
    {
        public const string SYSTEM_DEFAULT_LANG_ID = "en-US";       
        public const string LANG_SUFFIX = "_LANGUAGES";

        /// <summary>
        /// Default Contructor
        /// </summary>
        public Language()
        {
           
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetClientList,
            GetMasterList,
            AddClientLanguages
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Update
        }

        private string _strLanguageEnglishName;
        /// <summary>
        /// Language English Name
        /// </summary>
        public string LanguageEnglishName
        {
            get { return _strLanguageEnglishName; }
            set { if (this._strLanguageEnglishName != value) { _strLanguageEnglishName = value; } }
        }

        private string _strLanguageName;
        /// <summary>
        /// Language Name
        /// </summary>
        public string LanguageName
        {
            get { return _strLanguageName; }
            set { if (this._strLanguageName != value) { _strLanguageName = value; } }
        }

        private string _strCharacterSetType;
        /// <summary>
        /// Character Set Type
        /// </summary>
        public string CharacterSetType
        {
            get { return _strCharacterSetType; }
            set { if (this._strCharacterSetType != value) { _strCharacterSetType = value; } }
        }

        private string _strTextDirection;
        /// <summary>
        /// Text Direction
        /// </summary>
        public string TextDirection
        {
            get { return _strTextDirection; }
            set { if (this._strTextDirection != value) { _strTextDirection = value; } }
        }              

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="pIsUpdate"></param>
        /// <returns></returns>
        public bool Validate(bool pIsUpdate)
        {
            if (pIsUpdate)
            {
                if (String.IsNullOrEmpty(ID))
                    return false;
            }
            else
            {
                if (String.IsNullOrEmpty(LanguageEnglishName))
                    return false;

                if (String.IsNullOrEmpty(LanguageName))
                    return false;

                if (String.IsNullOrEmpty(TextDirection))
                    return false;

                if (String.IsNullOrEmpty(ClientId))
                    return false;

                if (String.IsNullOrEmpty(CreatedById))
                    return false;
            }
            if (String.IsNullOrEmpty(LastModifiedById))
                return false;
            return true;
        }
    }
}