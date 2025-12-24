using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace YPLMS2._0.API.Entity
{
    [Serializable] public class DocumentLibrary : BaseEntity
    {
        
        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            AddAll
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
            GetChildCount
        }

        /// <summary>
        /// Default Contructor
        /// <summary>
        public DocumentLibrary()
        {
            _documentChildLibrary = new List<DocumentLibrary>();
        }

        private List<DocumentLibrary> _documentChildLibrary;
        /// <summary>
        /// Child Folders
        /// </summary>
        public List<DocumentLibrary> ChildLibrary
        {
            get { return _documentChildLibrary; }
        }

        private string _documentFolderName;
        /// <summary>
        /// Folder Name
        /// </summary>
        public string FolderName
        {
            get { return _documentFolderName; }
            set { if (this._documentFolderName != value) { _documentFolderName = value; } }
        }

        private string _parentFolderId;
        /// <summary>
        /// Parent Folder Id
        /// </summary>
        public string ParentFolderId
        {
            get { return _parentFolderId; }
            set { if (this._parentFolderId != value) { _parentFolderId = value; } }
        }

        private string _strDescription;
        /// <summary>
        /// DocumentFolder Description
        /// </summary>
        public string FolderDescription
        {
            get { return _strDescription; }
            set { if (this._strDescription != value) { _strDescription = value; } }
        }

        private bool _isVisible;
        /// <summary>
        /// Is visible
        /// </summary>
        public bool IsVisible
        {
            get { return _isVisible; }
            set { if (this._isVisible != value) { _isVisible = value; } }
        }

        private string _relativePath;
        /// <summary>
        /// Relative Path from Document e.g. /Folder1/thisfolderName
        /// </summary>
        public string RelativePath
        {
            get { return _relativePath; }
            set { if (this._relativePath != value) { _relativePath = value; } }
        }

        private XmlDocument _folderTreeXml;
        /// <summary>
        /// Folder xml
        /// </summary>
        public XmlDocument FolderTreeXml
        {
            get { return _folderTreeXml; }
            set { _folderTreeXml = value; }
        }
    }
}
