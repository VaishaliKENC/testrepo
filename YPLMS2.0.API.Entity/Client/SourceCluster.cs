/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Rajendra
* Created:<02/12/2010>
* Last Modified:<02/12/2010>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class SourceCluster:BaseEntity 
    /// </summary>
   [Serializable] public class SourceCluster : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public SourceCluster()
        { }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            
        }

     
        

        private string _strFTPUploadPath;
        /// <summary>
        /// FTPUploadPath
        /// </summary>
        public string FTPUploadPath
        {
            get { return _strFTPUploadPath; }
            set { if (this._strFTPUploadPath != value) { _strFTPUploadPath = value; } }
        }

        private string _strFTPFolderName;
        /// <summary>
        /// FTPFolderName
        /// </summary>
        public string FTPFolderName
        {
            get { return _strFTPFolderName; }
            set { if (this._strFTPFolderName != value) { _strFTPFolderName = value; } }
        }


        private string _strContentFolderPath;
        /// <summary>
        /// FTPUploadPath
        /// </summary>
        public string ContentFolderPath
        {
            get { return _strContentFolderPath; }
            set { if (this._strContentFolderPath != value) { _strContentFolderPath = value; } }
        }

        private string _strFTPUserName;
        /// <summary>
        /// FTPUserName
        /// </summary>
        public string FTPUserName
        {
            get { return _strFTPUserName; }
            set { if (this._strFTPUserName != value) { _strFTPUserName = value; } }
        }

         private string _strFTPPassword;
        /// <summary>
        /// FTPPassword
        /// </summary>
        public string FTPPassword
        {
            get { return _strFTPPassword; }
            set { if (this._strFTPPassword != value) { _strFTPPassword = value; } }
        }
    }
}