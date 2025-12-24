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
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// Serializable - CustomFieldItem Class inherited from BaseEntity
    /// </summary>
    [Serializable]
    public class CustomFieldItem : BaseEntity
    {
        private List<CustomFieldItemLanguage> _entListCustomFieldItemLanguages;
        /// <summary>
        /// Default Contructor
        /// </summary>
        public CustomFieldItem()
        {
            _entListCustomFieldItemLanguages = new List<CustomFieldItemLanguage>();
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetCustomFieldItemList,
            GetCustomFieldItemRange,
            DeleteAll
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            GetByName
        }

        /// <summary>
        /// Custom Field Item Languages
        /// </summary>
        public List<CustomFieldItemLanguage> CustomFieldItemLanguages
        {
            get { return _entListCustomFieldItemLanguages; }
        }

        private bool _bIsDefault;
        /// <summary>
        /// to check Is Default
        /// </summary>
        public bool IsDefault
        {
            get { return _bIsDefault; }
            set { if (this._bIsDefault != value) { _bIsDefault = value; } }
        }

        private string _strCustomFieldId;
        /// <summary>
        /// Custom Field Id
        /// </summary>
        public string CustomFieldId
        {
            get { return _strCustomFieldId; }
            set { if (this._strCustomFieldId != value) { _strCustomFieldId = value; } }
        }

        private string _strSystemUserGUID;
        /// <summary>
        /// System User GUID
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private string _strEnteredValue;
        /// <summary>
        /// Entered Value
        /// </summary>
        public string EnteredValue
        {
            get { return _strEnteredValue; }
            set { if (this._strEnteredValue != value) { _strEnteredValue = value; } }
        }

        private bool _bIsUsed;
        /// <summary>
        /// IsUsed
        /// </summary>
        public bool IsUsed
        {
            get { return _bIsUsed; }
            set { if (this._bIsUsed != value) { _bIsUsed = value; } }
        }

        public bool Validate(bool pIsUpdate)
        {
            if (pIsUpdate)
            {
                if (String.IsNullOrEmpty(ID))
                    return false;
            }
            else
            {
                if (String.IsNullOrEmpty(CustomFieldId))
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