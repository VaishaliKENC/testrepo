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
using System.Collections.Generic;
using System.Xml;
using System;
namespace YPLMS2._0.API.Entity
{
   [Serializable] public class AssetLibrary : BaseEntity
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
            GetChildCount,
            GetForAssignment,
        }

        /// <summary>
        /// Default Contructor
        /// <summary>
        public AssetLibrary()
        {
            _assetChildLibrary = new List<AssetLibrary>();
        }

        private string _assetFolderName;
        /// <summary>
        /// Folder Name
        /// </summary>
        public string AssetFolderName
        {
            get { return _assetFolderName; }
            set { if (this._assetFolderName != value) { _assetFolderName = value; } }
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

        private string _emailIDString;
        /// <summary>
        /// Owner Email
        /// </summary>
        public string EmailIDString
        {
            get { return _emailIDString; }
            set { if (this._emailIDString != value) { _emailIDString = value; } }
        }

        private string _strDescription;
        /// <summary>
        /// AssetFolder Description
        /// </summary>
        public string AssetFolderDescription
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
        /// Relative Path from Asset e.g. /Folder1/thisfolderName
        /// </summary>
        public string RelativePath
        {
            get { return _relativePath; }
            set { if (this._relativePath != value) { _relativePath = value; } }
        }

        private List<AssetLibrary> _assetChildLibrary;
        /// <summary>
        /// Child Folders
        /// </summary>
        public List<AssetLibrary> ChildLibrary
        {
            get { return _assetChildLibrary; }
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