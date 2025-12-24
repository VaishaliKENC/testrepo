/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh & Ashish
* Created:<13/07/09>
* Last Modified:<25/12/09>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class EmailTemplate:BaseEntity 
    /// </summary>
   [Serializable] public class Cluster : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public Cluster()
        { }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll
        }


        private string _strClusterName;
        /// <summary>
        /// Cluster Name
        /// </summary>
        public string ClusterName
        {
            get { return _strClusterName; }
            set { if (this._strClusterName != value) { _strClusterName = value; } }
        }

        private string _strClusterIP;
        /// <summary>
        /// Master Cluster IP
        /// </summary>
        public string ClusterIP
        {
            get { return _strClusterIP; }
            set { if (this._strClusterIP != value) { _strClusterIP = value; } }
        }

        private string _strDNSServerIP;
        /// <summary>
        /// Master DNS Server IP
        /// </summary>
        public string DNSServerIP
        {
            get { return _strDNSServerIP; }
            set { if (this._strDNSServerIP != value) { _strDNSServerIP = value; } }
        }

       

        private string _strDNSServerUserName;
        /// <summary>
        /// DNS Server User Name
        /// </summary>
        public string DNSServerUserName
        {
            get { return _strDNSServerUserName; }
            set { if (this._strDNSServerUserName != value) { _strDNSServerUserName = value; } }
        }

        private string _strDNSServerPassword;
        /// <summary>
        /// DNS Server Password
        /// </summary>
        public string DNSServerPassword
        {
            get { return _strDNSServerPassword; }
            set { if (this._strDNSServerPassword != value) { _strDNSServerPassword = value; } }
        }

        private string _strContentServerIP;
        /// <summary>
        /// DNS Content Server IP
        /// </summary>
        public string ContentServerIP
        {
            get { return _strContentServerIP; }
            set { if (this._strContentServerIP != value) { _strContentServerIP = value; } }
        }

        private string _strDatabaseUserName;
        /// <summary>
        ///DatabaseUserName
        /// </summary>
        public string DatabaseUserName
        {
            get { return _strDatabaseUserName; }
            set { if (this._strDatabaseUserName != value) { _strDatabaseUserName = value; } }
        }

        private string _strDatabasePassword;
        /// <summary>
        ///DatabasePassword
        /// </summary>
        public string DatabasePassword
        {
            get { return _strDatabasePassword; }
            set { if (this._strDatabasePassword != value) { _strDatabasePassword = value; } }
        }

        private string _strContentFolderURL;
        /// <summary>
        ///DatabasePassword
        /// </summary>
        public string ContentFolderURL
        {
            get { return _strContentFolderURL; }
            set { if (this._strContentFolderURL != value) { _strContentFolderURL = value; } }
        }

        private string _strFTPUploadPath;
        /// <summary>
        ///DatabasePassword
        /// </summary>
        public string FTPUploadPath
        {
            get { return _strFTPUploadPath; }
            set { if (this._strFTPUploadPath != value) { _strFTPUploadPath = value; } }
        }


        private string _strContentFolderPath;
        /// <summary>
        ///DatabasePassword
        /// </summary>
        public string ContentFolderPath
        {
            get { return _strContentFolderPath; }
            set { if (this._strContentFolderPath != value) { _strContentFolderPath = value; } }
        }

        private string _strDomainName;
        /// <summary>
        ///DatabasePassword
        /// </summary>
        public string DomainName
        {
            get { return _strDomainName; }
            set { if (this._strDomainName != value) { _strDomainName = value; } }
        }

        private string _strDatabaseIP;
        /// <summary>
        /// Database IP
        /// </summary>
        public string DatabaseIP
        {
            get { return _strDatabaseIP; }
            set { if (this._strDatabaseIP != value) { _strDatabaseIP = value; } }
        }

        private bool _bIsActive;
        /// <summary>
        /// To check Is Active
        /// </summary>
        public bool IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
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

        private string _strCoursePlayerURL;
        /// <summary>
         /// CoursePlayerURL
        /// </summary>
         public string CoursePlayerURL
        {
            get { return _strCoursePlayerURL; }
            set { if (this._strCoursePlayerURL != value) { _strCoursePlayerURL = value; } }
        }
         private string _strLUWebServiceURL;
         /// <summary>
         /// LU WebService URL                          
         /// </summary>
         public string LUWebServiceURL
         {
             get { return _strLUWebServiceURL; }
             set { if (this._strLUWebServiceURL != value) { _strLUWebServiceURL = value; } }
         }
         private string _strLUApplicationLaunchURL;              
         /// <summary>
         /// Global LU Application Launch URL              
         /// </summary>
         public string LUApplicationLaunchURL
         {
             get { return _strLUApplicationLaunchURL; }
             set { if (this._strLUApplicationLaunchURL != value) { _strLUApplicationLaunchURL = value; } }
         }
    }
}