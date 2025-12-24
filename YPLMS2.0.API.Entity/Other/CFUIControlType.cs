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
    /// Serializable CFUIControlType Class
    /// </summary>
    [Serializable]
     public class CFUIControlType : BaseEntity
    {

        #region CONSTANT CONTROL TYPE IDS
        public const string CNTL_TYPE_ID_FREETEXT_NUM = "CType1001";
        public const string CNTL_TYPE_ID_DROPDOWN = "CType1002";
        public const string CNTL_TYPE_ID_RADIOBUT = "CType1003";
        public const string CNTL_TYPE_ID_FREETEXT_ALPHANUM = "CType1009";
        public const string CNTL_TYPE_ID_FREETEXT_EMAIL = "CType1010";
        public const string CNTL_TYPE_ID_FREETEXT_HTTP = "CType1011";
        #endregion
        

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetCustomFieldTypeList

        }

        private string _strTypeName;
        /// <summary>
        /// Type Name
        /// </summary>
        public string TypeName
        {
            get { return _strTypeName; }
            set { if (this._strTypeName != value) { _strTypeName = value; } }
        }

        private Entity.ImportDefination.ValueType _fieldValueType;
        /// <summary>
        /// Field Value Type
        /// </summary>
        public Entity.ImportDefination.ValueType FieldValueType
        {
            get { return _fieldValueType; }
            set { if (this._fieldValueType != value) { _fieldValueType = value; } }
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
                if (String.IsNullOrEmpty(TypeName))
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