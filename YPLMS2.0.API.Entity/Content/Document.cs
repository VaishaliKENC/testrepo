using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class Document:BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public Document()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            AddAll,
            GetAllWithChild,
            GetDocumentLanguages,
            GetAllVideos,
            GetAllForLearner,
            GetAllVideos_Learner,
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetDocumentForLearner,
            GetRelativePath,
            Add,
            Update,
            Delete,
            UpdateLanguage,
            DeleteLanguage
        }

        private string _documentName;
        public string DocumentName
        {
            get { return _documentName; }
            set { if (this._documentName != value) { _documentName = value; } }
        }
        private string _documentDescription;
        /// <summary>
        /// Description
        /// </summary>

        public string DocumentDescription
        {
            get { return _documentDescription; }
            set { if (this._documentDescription != value) { _documentDescription = value; } }
        }

        private string _documentKeywords;
        /// <summary>
        /// Keywords
        /// </summary>
        public string DocumentKeywords
        {
            get { return _documentKeywords; }
            set { if (this._documentKeywords != value) { _documentKeywords = value; } }
        }

        private string _author;
        public string Author
        {
            get { return _author; }
            set { if (this._author != value) { _author = value; } }
        }
        
        private string _strModifiedByName;
        public string ModifiedByName
        {
            get { return _strModifiedByName; }
            set { if (this._strModifiedByName != value) { _strModifiedByName = value; } }
        }

        private string _documentFolderId;
        /// <summary>
        /// Container Folder Id
        /// </summary>
        public string FolderId
        {
            get { return _documentFolderId; }
            set { if (this._documentFolderId != value) { _documentFolderId = value; } }
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

        private string _FileType;
        /// <summary>
        /// File Type from Lookup
        /// </summary>
        public string FileType
        {
            get { return _FileType; }
            set { if (this._FileType != value) { _FileType = value; } }
        }

        private string _relativePath;
        /// <summary>
        /// Relative Path from Asset e.g. /Folder1/thisfolderName
        /// </summary>
        public string RelativePath
        {
            get { return _relativePath; }
            set { if (this._relativePath != value) { _relativePath = value; } }
        }

        private string _thumbnailPath;
        /// <summary>
        /// Relative Path from Asset e.g. /Folder1/thisfolderName
        /// </summary>
        public string ThumbnailPath
        {
            get { return _thumbnailPath; }
            set { if (this._thumbnailPath != value) { _thumbnailPath = value; } }
        }

        private DateTime _publishDate;
        /// <summary>
        /// Relative Path from Asset e.g. /Folder1/thisfolderName
        /// </summary>
        public DateTime PublishDate
        {
            get { return _publishDate; }
            set { if (this._publishDate != value) { _publishDate = value; } }
        }

        private byte[] _documentFile;
        /// <summary>
        /// File Content
        /// </summary>
        public byte[] DocumentFile
        {
            get { return _documentFile; }
            set { if (this._documentFile != value) { _documentFile = value; } }
        }

        private string _documentFileName;
        /// <summary>
        /// Physical File Name
        /// </summary>
        public string DocumentFileName
        {
            get { return _documentFileName; }
            set { if (this._documentFileName != value) { _documentFileName = value; } }
        }

        private string _strCreatedName;
        /// <summary>
        /// Created by Name
        /// </summary>
        public string CreatedName
        {
            get { return _strCreatedName; }
            set { if (this._strCreatedName != value) { _strCreatedName = value; } }
        }

        private string _IconName;
        public string IconName
        {
            get { return _IconName; }
            set { if (this._IconName != value) { _IconName = value; } }
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
