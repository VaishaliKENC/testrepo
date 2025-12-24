using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace YPLMS2._0.API.Entity
{
    public class AssetLibraryVM :BaseEntityVM
    {
        

        /// <summary>
        /// Default Contructor
        /// <summary>
        public AssetLibraryVM()
        {
            _assetChildLibrary = new List<AssetLibrary>();
        }

        private string? _assetFolderName;
        /// <summary>
        /// Folder Name
        /// </summary>
        public string? AssetFolderName
        {
            get { return _assetFolderName; }
            set { if (this._assetFolderName != value) { _assetFolderName = value; } }
        }

        private string? _parentFolderId;
        /// <summary>
        /// Parent Folder Id
        /// </summary>
        public string? ParentFolderId
        {
            get { return _parentFolderId; }
            set { if (this._parentFolderId != value) { _parentFolderId = value; } }
        }

        private string? _emailIDString;
        /// <summary>
        /// Owner Email
        /// </summary>
        public string? EmailIDString
        {
            get { return _emailIDString; }
            set { if (this._emailIDString != value) { _emailIDString = value; } }
        }

        private string ?_strDescription;
        /// <summary>
        /// AssetFolder Description
        /// </summary>
        public string? AssetFolderDescription
        {
            get { return _strDescription; }
            set { if (this._strDescription != value) { _strDescription = value; } }
        }

        private bool? _isVisible;
        /// <summary>
        /// Is visible
        /// </summary>
        public bool? IsVisible
        {
            get { return _isVisible; }
            set { if (this._isVisible != value) { _isVisible = value; } }
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

        private List<AssetLibrary>? _assetChildLibrary;
        /// <summary>
        /// Child Folders
        /// </summary>
        public List<AssetLibrary>? ChildLibrary
        {
            get { return _assetChildLibrary; }
        }
        
    }
}
