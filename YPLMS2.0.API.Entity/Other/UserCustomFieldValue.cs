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
    /// Serializable class UserCustomFieldValue:CustomFieldItem
    /// </summary>
    [Serializable]
     public class UserCustomFieldValue : CustomFieldItem
    {
        private string _strUserID;
        /// <summary>
        ///  User ID
        /// </summary>
        public string UserID
        {
            get { return _strUserID; }
            set { if (this._strUserID != value) { _strUserID = value; } }
        }

        private string _strCustomFieldItemId;
        /// <summary>
        /// Custom Field Item Id
        /// </summary>
        public string CustomFieldItemId
        {
            get { return _strCustomFieldItemId; }
            set { if (this._strCustomFieldItemId != value) { _strCustomFieldItemId = value; } }
        }

        public new bool Validate(bool pIsUpdate)
        {
            
            if (!pIsUpdate)
            {
                if (String.IsNullOrEmpty(CreatedById))
                        return false;
            }
            if (String.IsNullOrEmpty(CustomFieldItemId))
                return false;
            if (String.IsNullOrEmpty(UserID))
                return false;
            if (String.IsNullOrEmpty(EnteredValue))
                return false;
            if (String.IsNullOrEmpty(LastModifiedById))
                return false;

            return true;
        }
    }
}