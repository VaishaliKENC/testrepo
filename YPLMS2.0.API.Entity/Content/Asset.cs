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
using Microsoft.AspNetCore.Http;
using System;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class Asset:BaseEntity 
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public Asset()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            AddAll,
            GetAllWithChild,
            GetAssetLanguages,
            GetAllForAssignments,
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetRelativePath,
            Add,
            Update,
            Delete,
            UpdateLanguage,
            DeleteLanguage,
            SearchRelativePath
        }
        private string? _assetName;
        public string? AssetName
        {
            get { return _assetName; }
            set { if (this._assetName != value) { _assetName = value; } }
        }
        private string? _assetDescription;
        /// <summary>
        /// Description
        /// </summary>
        
        public string? AssetDescription
        {
            get { return _assetDescription; }
            set { if (this._assetDescription != value) { _assetDescription = value; } }
        }

        private string? _assetKeywords;
        /// <summary>
        /// Keywords
        /// </summary>
        public string? AssetKeywords
        {
            get { return _assetKeywords; }
            set { if (this._assetKeywords != value) { _assetKeywords = value; } }
        }

        

        private string? _strModifiedByName;
        public string? ModifiedByName
        {
            get { return _strModifiedByName; }
            set { if (this._strModifiedByName != value) { _strModifiedByName = value; } }
        }

        private string? _assetFolderId;
        /// <summary>
        /// Container Folder Id
        /// </summary>
        public string? AssetFolderId
        {
            get { return _assetFolderId; }
            set { if (this._assetFolderId != value) { _assetFolderId = value; } }
        }

        private Nullable<Boolean> _isActive;
        /// <summary>
        /// Is Active
        /// </summary>
        public Nullable<Boolean> IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
        }

        private string? _assetFileName;
        /// <summary>
        /// Physical File Name
        /// </summary>
        public string? AssetFileName
        {
            get { return _assetFileName; }
            set { if (this._assetFileName != value) { _assetFileName = value; } }
        }

        private string? _assetFileType;
        /// <summary>
        /// File Type from Lookup
        /// </summary>
        public string? AssetFileType
        {
            get { return _assetFileType; }
            set { if (this._assetFileType != value) { _assetFileType = value; } }
        }

        private string? _relativePath;
        /// <summary>
        /// Relative Path from Asset e.g. /Folder1/thisfolderName
        /// </summary>
        public string? RelativePath
        {
            get { return _relativePath; }
            set { if (this._relativePath != value) { _relativePath = value; } }
        }        

        private string? _strCreatedName;
        /// <summary>
        /// Created by Name
        /// </summary>
        public string? CreatedName
        {
            get { return _strCreatedName; }
            set { if (this._strCreatedName != value) { _strCreatedName = value; } }
        }

        private byte[]? _assetFile;
        /// <summary>
        /// File Content
        /// </summary>
        public byte[]? AssetFile
        {
            get { return _assetFile; }
            set { if (this._assetFile != value) { _assetFile = value; } }
        }

        private bool? _isPrintCertificate;
        public bool? IsPrintCertificate
        {
            get { return _isPrintCertificate; }
            set { if (this._isPrintCertificate != value) { _isPrintCertificate = value; } }
        }

        private bool? _isAssigned;
        public bool? IsAssigned
        {
            get { return _isAssigned; }
            set { if (this._isAssigned != value) { _isAssigned = value; } }
        }
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public string? CategoryId { get; set; }
        public string? SubCategoryId { get; set; }

        private string? _LanguageId;
        public string? LanguageId
        {
            get { return _LanguageId; }
            set { if (this._LanguageId != value) { _LanguageId = value; } }
        }

        private string? _LanguageName;
        public string? LanguageName
        {
            get { return _LanguageName; }
            set { if (this._LanguageName != value) { _LanguageName = value; } }
        }

        private bool? _isDownload;
        public bool? IsDownload
        {
            get { return _isDownload; }
            set { if (this._isDownload != value) { _isDownload = value; } }
        }
        private string? _assetFileNameLink;        
        public string? AssetFileNameLink
        {
            get { return _assetFileNameLink; }
            set { if (this._assetFileNameLink != value) { _assetFileNameLink = value; } }
        }
        private IFormFile? _file;

        public IFormFile? file
        {
            get { return _file; }
            set { if (this._file != value) { _file = value; } }
        }
        private bool? _isEdit;
        public bool? isEdit
        {
            get { return _isEdit; }
            set { if (this._isEdit != value) { _isEdit = value; } }
        }
        private string? _contentServerURL;
        public string? ContentServerURL
        {
            get { return _contentServerURL; }
            set { if (this._contentServerURL != value) { _contentServerURL = value; } }
        }
        private string? _Keyword;
        public string? Keyword
        {
            get { return _Keyword; }
            set { if (this._Keyword != value) { _Keyword = value; } }
        }
        private string? _ActivityId;
        public string? ActivityId
        {
            get { return _ActivityId; }
            set { if (this._ActivityId != value) { _ActivityId = value; } }
        }
        private string? _ActivityType;
        public string? ActivityType
        {
            get { return _ActivityType; }
            set { if (this._ActivityType != value) { _ActivityType = value; } }
        }

        public class ThumbnailUploadModel
        {
            public IFormFile File { get; set; }


            public string ClientId { get; set; }
        }
        private string? _thumbnailImgRelativePath;
        public string? ThumbnailImgRelativePath
        {
            get { return _thumbnailImgRelativePath; }
            set { if (this._thumbnailImgRelativePath != value) { _thumbnailImgRelativePath = value; } }
        }

        public class PreviewAssetRequest
        {
            public string ClientId { get; set; }
            public string CurrentUserID { get; set; }
            public string ActivityId { get; set; }
            public string ActivityType { get; set; }
            public string LanguageId { get; set; }
            public decimal Progress { get; set; }
            public bool IsForAdminPreview { get; set; }
        }

        public UserAssetTracking? TrackingData { get; set; }
    }
}