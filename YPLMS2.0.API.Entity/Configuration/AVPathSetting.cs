/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author: Fattesinh Pisal
* Created:<16/11/09>
* Last Modified:<16/11/09>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    ///  class Task 
    /// </summary>
    [Serializable]
   public class AVPathSetting : BaseEntity
    {


        public AVPathSetting()
        { }
        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkAdd
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

        private string _strCustomFieldId;
        /// <summary>
        /// CustomFieldId
        /// </summary>
        public string CustomFieldId
        {
            get { return _strCustomFieldId; }
            set { if (this._strCustomFieldId != value) { _strCustomFieldId = value; } }
        }

        private string _strCustomFieldItemId;
        /// <summary>
        /// CustomFieldItemId
        /// </summary>
        public string CustomFieldItemId
        {
            get { return _strCustomFieldItemId; }
            set { if (this._strCustomFieldItemId != value) { _strCustomFieldItemId = value; } }
        }

        private string _strItemPath;
        /// <summary>
        /// ItemPath
        /// </summary>
        public string ItemPath
        {
            get { return _strItemPath; }
            set { if (this._strItemPath != value) { _strItemPath = value; } }
        }

        private bool _isSubFolder;
        /// <summary>
        /// IsSubFolder
        /// </summary>
        public bool IsSubFolder
        {
            get { return _isSubFolder; }
            set { if (this._isSubFolder != value) { _isSubFolder = value; } }
        }

        private bool _isSameAVPathForAll;
        /// <summary>
        /// _isSameAVPathForAll
        /// </summary>
        public bool IsSameAVPathForAll
        {
            get { return _isSameAVPathForAll; }
            set { if (this._isSameAVPathForAll != value) { _isSameAVPathForAll = value; } }
        }

    }
}
