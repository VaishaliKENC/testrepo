/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh
* Created:<21/09/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class SignUpProfileAccessConfiguration : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public SignUpProfileAccessConfiguration()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkUpdate
        }

        private string _profileAccessConfigurationId;
        public string ProfileAccessConfigurationId
        {
            get { return _profileAccessConfigurationId; }
            set { if (this._profileAccessConfigurationId != value) { _profileAccessConfigurationId = value; } }
        }

        private string _profileFieldID;
        public string ProfileFieldID
        {
            get { return _profileFieldID; }
            set { if (this._profileFieldID != value) { _profileFieldID = value; } }
        }

        private string _fieldName;
        public string FieldName
        {
            get { return _fieldName; }
            set { if (this._fieldName != value) { _fieldName = value; } }
        }

        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { if (this._isReadOnly != value) { _isReadOnly = value; } }
        }

        private bool _isMandatory;
        public bool IsMandatory
        {
            get { return _isMandatory; }
            set { if (this._isMandatory != value) { _isMandatory = value; } }
        }

        private bool _isMandatoryDisabled;
        public bool IsMandatoryDisabled
        {
            get { return _isMandatoryDisabled; }
            set { if (this._isMandatoryDisabled != value) { _isMandatoryDisabled = value; } }
        }

        private bool _isVisibletoUser;
        public bool IsVisibletoUser
        {
            get { return _isVisibletoUser; }
            set { if (this._isVisibletoUser != value) { _isVisibletoUser = value; } }
        }

        private string _fieldType;
        public string FieldType
        {
            get { return _fieldType; }
            set { if (this._fieldType != value) { _fieldType = value; } }
        }
    }
}
