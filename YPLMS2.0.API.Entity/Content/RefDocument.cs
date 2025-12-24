/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<8/1/2013>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class RefDocument : BaseEntity 
    /// </summary>
    /// 
    public class RefDocument : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public RefDocument()
        { }

        private string _strSystemUserGUId;
        /// <summary>
        /// SystemUserGUId
        /// </summary>
        public string SystemUserGUId
        {
            get { return _strSystemUserGUId; }
            set { if (this._strSystemUserGUId != value) { _strSystemUserGUId = value; } }
        }

        private string _strActivityId;
        /// <summary>
        /// ActivityId
        /// </summary>
        public string ActivityId
        {
            get { return _strActivityId; }
            set { if (this._strActivityId != value) { _strActivityId = value; } }
        }

        private string _strActivityName;
        /// <summary>
        /// ActivityName
        /// </summary>
        public string ActivityName
        {
            get { return _strActivityName; }
            set { if (this._strActivityName != value) { _strActivityName = value; } }
        }

        private string _strActivityTypeId;
        /// <summary>
        /// ActivityTypeId
        /// </summary>
        public string ActivityTypeId
        {
            get { return _strActivityTypeId; }
            set { if (this._strActivityTypeId != value) { _strActivityTypeId = value; } }
        }

        private string _strRefDocumentName;
        /// <summary>
        /// RefDocumentName
        /// </summary>
        public string RefDocumentName
        {
            get { return _strRefDocumentName; }
            set { if (this._strRefDocumentName != value) { _strRefDocumentName = value; } }
        }


        private string _strRefDocumentDescription;
        /// <summary>
        /// RefDocumentDescription
        /// </summary>
        public string RefDocumentDescription
        {
            get { return _strRefDocumentDescription; }
            set { if (this._strRefDocumentDescription != value) { _strRefDocumentDescription = value; } }
        }

        private bool _strIsActive;
        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive
        {
            get { return _strIsActive; }
            set { if (this._strIsActive != value) { _strIsActive = value; } }
        }

        private float _strDisplayOrder;
        /// <summary>
        /// DisplayOrder
        /// </summary>
        public float DisplayOrder
        {
            get { return _strDisplayOrder; }
            set { if (this._strDisplayOrder != value) { _strDisplayOrder = value; } }
        }

        private string _strRefDocumentFileName;
        /// <summary>
        /// RefDocumentFileName
        /// </summary>
        public string RefDocumentFileName
        {
            get { return _strRefDocumentFileName; }
            set { if (this._strRefDocumentFileName != value) { _strRefDocumentFileName = value; } }
        }

        private string _strRefDocumentFileType;
        /// <summary>
        /// RefDocumentFileType
        /// </summary>
        public string RefDocumentFileType
        {
            get { return _strRefDocumentFileType; }
            set { if (this._strRefDocumentFileType != value) { _strRefDocumentFileType = value; } }
        }

        private string _strLanguageId;
        /// <summary>
        /// RefDocumentFileType
        /// </summary>
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }

        private byte[] _RefDocumentFile;
        /// <summary>
        /// File Content
        /// </summary>
        public byte[] RefDocumentFile
        {
            get { return _RefDocumentFile; }
            set { if (this._RefDocumentFile != value) { _RefDocumentFile = value; } }
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
            IsRefDocNameAvailable
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllLearner
        }
    }
}