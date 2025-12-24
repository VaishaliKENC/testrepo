/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:<Ashish Phate>
* Created:<09/09/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{

   [Serializable] public class Policy : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public Policy()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            AddAll,
            GetAllWithChild,
            GetPolicyLanguages,
            GetAllForAssignments,
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
            UpdateStatus,
            Getforlearner,
            UpdateLanguage,
            DeleteLanguage
        }

        private string _policyName;
        /// <summary>
        /// Name of Policy
        /// </summary>
        public string PolicyName
        {
            get { return _policyName; }
            set { if (this._policyName != value) { _policyName = value; } }
        }

        private string _policyFolderId;
        /// <summary>
        /// Container Folder Id
        /// </summary>
        public string PolicyFolderId
        {
            get { return _policyFolderId; }
            set { if (this._policyFolderId != value) { _policyFolderId = value; } }
        }

        private string _policyDescription;
        /// <summary>
        /// Folder Description
        /// </summary>
        public string PolicyDescription
        {
            get { return _policyDescription; }
            set { if (this._policyDescription != value) { _policyDescription = value; } }
        }

      

        private string _strModifiedByName;
        public string ModifiedByName
        {
            get { return _strModifiedByName; }
            set { if (this._strModifiedByName != value) { _strModifiedByName = value; } }
        }

        private string _policyKeywords;
        /// <summary>
        /// Policy Keywords
        /// </summary>
        public string PolicyKeywords
        {
            get { return _policyKeywords; }
            set { if (this._policyKeywords != value) { _policyKeywords = value; } }
        }

        private Nullable<Boolean> _isActive;
        /// <summary>
        /// Is active
        /// </summary>
        public Nullable<Boolean> IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
        }

        private string _policyFileNameLink;
        /// <summary>
        /// Physical Link
        /// </summary>
        public string PolicyFileNameLink
        {
            get { return _policyFileNameLink; }
            set { if (this._policyFileNameLink != value) { _policyFileNameLink = value; } }
        }

        private string _policyFileType;
        /// <summary>
        /// File Type
        /// </summary>
        public string PolicyFileType
        {
            get { return _policyFileType; }
            set { if (this._policyFileType != value) { _policyFileType = value; } }
        }

        private bool _isLink;
        /// <summary>
        /// Is external link
        /// </summary>
        public bool IsLink
        {
            get { return _isLink; }
            set { if (this._isLink != value) { _isLink = value; } }
        }

        private bool _isSecured;
        /// <summary>
        /// Is Secured
        /// </summary>
        public bool IsSecured
        {
            get { return _isSecured; }
            set { if (this._isSecured != value) { _isSecured = value; } }
        }

        private string _relativePath;
        /// <summary>
        /// Relative Path from Policy e.g. /Folder1/thisfolderName
        /// </summary>
        public string RelativePath
        {
            get { return _relativePath; }
            set { if (this._relativePath != value) { _relativePath = value; } }
        }        

        private byte[] _policyFile;
        /// <summary>
        /// File Content
        /// </summary>
        public byte[] PolicyFile
        {
            get { return _policyFile; }
            set { if (this._policyFile != value) { _policyFile = value; } }
        }

        private bool _isPrintCertificate;
        public bool IsPrintCertificate
        {
            get { return _isPrintCertificate; }
            set { if (this._isPrintCertificate != value) { _isPrintCertificate = value; } }
        }

        private bool _isAssigned;
        public bool IsAssigned
        {
            get { return _isAssigned; }
            set { if (this._isAssigned != value) { _isAssigned = value; } }
        }

        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }

        private string _LanguageId;
        public string LanguageId
        {
            get { return _LanguageId; }
            set { if (this._LanguageId != value) { _LanguageId = value; } }
        }

        private string _LanguageName;
        public string LanguageName
        {
            get { return _LanguageName; }
            set { if (this._LanguageName != value) { _LanguageName = value; } }
        }
    }
}
