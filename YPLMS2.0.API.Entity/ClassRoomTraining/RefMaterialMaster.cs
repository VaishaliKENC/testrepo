/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:Abhay 
* Created:<12/26/2011>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class RefMaterialMaster : BaseEntity 
    /// </summary>
    /// 
    public class RefMaterialMaster : RefMaterialMasterLanguages
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public RefMaterialMaster()
        { }


        private string _strRefMaterialTypeId;
        /// <summary>
        /// RefMaterialTypeId
        /// </summary>
        public string RefMaterialTypeId
        {
            get { return _strRefMaterialTypeId; }
            set { if (this._strRefMaterialTypeId != value) { _strRefMaterialTypeId = value; } }
        }

        
        private string _strFileName;
        /// <summary>
        /// ResourceFileName
        /// </summary>
        public string FileName
        {
            get { return _strFileName; }
            set { if (this._strFileName != value) { _strFileName = value; } }
        }

        private string _RefMaterialTypeName;
        /// <summary>
        /// ResourceFileName
        /// </summary>
        public string RefMaterialTypeName
        {
            get { return _RefMaterialTypeName; }
            set { if (this._RefMaterialTypeName != value) { _RefMaterialTypeName = value; } }
        }

        private byte[] _RefMaterialFile;
        /// <summary>
        /// File Content
        /// </summary>
        public byte[] RefMaterialFile
        {
            get { return _RefMaterialFile; }
            set { if (this._RefMaterialFile != value) { _RefMaterialFile = value; } }
        }


        private Nullable<bool> _isActive;
        public Nullable<bool> IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
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
            AddLanguageRefMaterial,
            UpdateLanguageRefMaterial,
            UpdateLanguage,
            DeleteRefMaterialLanguage
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetResouceLanguages,
            ActivateDeActivateStatus
        }
    }
}