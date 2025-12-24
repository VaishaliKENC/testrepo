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
    /// Serializable CustomField Class inherited from CFUIControlType
    /// </summary>
    [Serializable]
    public class CustomField : CFUIControlType
    {
       

        private List<CustomFieldLanguage> _entListCustomFieldLanguages;
        private List<CustomFieldItem> _entListCustomFieldItems;

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomField()
        {
            _entListCustomFieldLanguages = new List<CustomFieldLanguage>();
            _entListCustomFieldItems = new List<CustomFieldItem>();
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetCustomFieldList,
            GetClientCustomFieldList,
            GetCustomFieldDtls,
            GetCustomFieldwithRange,
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
            Delete,
            GetByName
        }

        /// <summary>
        /// List of CustomFieldLanguages
        /// </summary>
        public List<CustomFieldLanguage> CustomFieldLanguages
        {
            get { return _entListCustomFieldLanguages; }
        }

        /// <summary>
        /// List of CustomFieldItems
        /// </summary>
        public List<CustomFieldItem> CustomFieldItems
        {
            get { return _entListCustomFieldItems; }
        }

        private int _iSortOrder;
        /// <summary>
        /// Sort Order
        /// </summary>
        public int SortOrder
        {
            get { return _iSortOrder; }
            set { if (this._iSortOrder != value) { _iSortOrder = value; } }
        }

        private string _strCustomFieldTypeId;
        /// <summary>
        /// Custom Field Type Id
        /// </summary>
        public string CustomFieldTypeId
        {
            get { return _strCustomFieldTypeId; }
            set { if (this._strCustomFieldTypeId != value) { _strCustomFieldTypeId = value; } }
        }

        private bool _bIsMandatory;
        /// <summary>
        /// To check Is Mandatory
        /// </summary>
        public bool IsMandatory
        {
            get { return _bIsMandatory; }
            set { if (this._bIsMandatory != value) { _bIsMandatory = value; } }
        }

        private bool _bIsActive;
        /// <summary>
        /// To check Is Active
        /// </summary>
        public bool IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
        }

        private string _strDefaultLanguageId;
        /// <summary>
        /// Default Language Id to read specific language custom field data
        /// </summary>
        public string DefaultLanguageId
        {
            get { return _strDefaultLanguageId; }
            set { if (this._strDefaultLanguageId != value) { _strDefaultLanguageId = value; } }
        }

        private bool _bIsUsed;
        /// <summary>
        /// To check Is Active
        /// </summary>
        public bool IsUsed
        {
            get { return _bIsUsed; }
            set { if (this._bIsUsed != value) { _bIsUsed = value; } }
        }

        public new bool Validate(bool pIsUpdate)
        {
            if (pIsUpdate)
            {
                if (String.IsNullOrEmpty(ID))
                    return false;

            }
            else
            {
                if (String.IsNullOrEmpty(CustomFieldTypeId))
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


        public const string Address_1_ID = "CUST_Address1001";
        public const string Address_2_ID = "CUST_Address2001";
        public const string City_ID = "CUST_City001";
        public const string State_ID = "CUST_State001";
        public const string ZipCode_ID = "CUST_ZipCode001";
        public const string Country_ID = "CUST_Country001";
        public const string PhoneNo_ID = "CUST_PhoneNo001";

   
        public const string JobRole_ID = "CUST_JobRole001";
        public const string AreaOfRespo_ID = "CUST_AreaOfRespo001";
        public const string CompanyName_ID = "CUST_CompanyName001";
        public const string NMLSID_ID = "CUST_NMLSID001";
    }
}