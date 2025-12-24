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
   [Serializable] public class PolicyLibrary: BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public PolicyLibrary()
        {

            _policyChildLibrary = new List<PolicyLibrary>();        
        }

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

        private string _policyFolderName;
        /// <summary>
        /// Folder Name
        /// </summary>
        public string PolicyFolderName
        {
            get { return _policyFolderName; }
            set { if (this._policyFolderName != value) { _policyFolderName = value; } }
        }

        private string _policyFolderDescription;
        /// <summary>
        /// Policy Folder Description
        /// </summary>
        public string PolicyFolderDescription
        {
            get { return _policyFolderDescription; }
            set { if (this._policyFolderDescription != value) { _policyFolderDescription = value; } }
        }

        private string _parentFolderId;
        /// <summary>
        /// Parent folder Id
        /// </summary>
        public string ParentFolderId
        {
            get { return _parentFolderId; }
            set { if (this._parentFolderId != value) { _parentFolderId = value; } }
        }

        private string _emailIDString;
        /// <summary>
        /// Owner email
        /// </summary>
        public string EmailIDString
        {
            get { return _emailIDString; }
            set { if (this._emailIDString != value) { _emailIDString = value; } }
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

        private List<PolicyLibrary> _policyChildLibrary;
        /// <summary>
        /// Child Folders
        /// </summary>
        public List<PolicyLibrary> PolicyChildLibrary
        {
            get { return _policyChildLibrary; }
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