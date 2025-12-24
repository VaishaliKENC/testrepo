/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Ashish & Fattehsinh
* Created:<13/07/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    ///  class SystemConfiguration:BaseEntity 
    /// </summary>
   [Serializable] public class SystemConfiguration : BaseEntity
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public SystemConfiguration()
        {
            //_xmlConfigXML = new XmlDocument();
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete
        }

        private string _strAllowedDomainsList;
        /// <summary>
        /// Allowed Domains List
        /// </summary>
        public string AllowedDomainsList
        {
            get { return _strAllowedDomainsList; }
            set { if (this._strAllowedDomainsList != value) { _strAllowedDomainsList = value; } }
        }

        private string _strNonRestrictedDomainLis;
        /// <summary>
        /// Non-Restricted Domains List
        /// </summary>
        public string NonRestrictedDomainList
        {
            get { return _strNonRestrictedDomainLis; }
            set { if (this._strNonRestrictedDomainLis != value) { _strNonRestrictedDomainLis = value; } }
        }

        private string _strDefaultSiteLogo;
        /// <summary>
        /// DefaultSiteLogo
        /// </summary>
        public string DefaultSiteLogo
        {
            get { return _strDefaultSiteLogo; }
            set { if (this._strDefaultSiteLogo != value) { _strDefaultSiteLogo = value; } }
        }

        private string _strConfigurationTypeId;
        /// <summary>
        /// ConfigurationTypeId
        /// </summary>
        public string ConfigurationTypeId
        {
            get { return _strConfigurationTypeId; }
            set { if (this._strConfigurationTypeId != value) { _strConfigurationTypeId = value; } }
        }

        public bool Validate(bool pIsUpdate)
        {
            if (pIsUpdate)
            {
                if (String.IsNullOrEmpty(ID))
                    return false;
            }
            else
            {                
                if (String.IsNullOrEmpty(this.ClientId))
                    return false;

                if (String.IsNullOrEmpty(CreatedById))
                    return false;
            }
            if (String.IsNullOrEmpty(LastModifiedById))
                return false;

            return true;
        }

        private bool _IsDisplay;
        /// <summary>
        /// User Name Alias
        /// </summary>
        public bool IsDisplay
        {
            get { return _IsDisplay; }
            set { if (this._IsDisplay != value) { _IsDisplay = value; } }
        }

        private bool _IsAllowUploadPhoto;
        /// <summary>
        /// User Name Alias
        /// </summary>
        public bool IsAllowUploadPhoto
        {
            get { return _IsAllowUploadPhoto; }
            set { if (this._IsAllowUploadPhoto != value) { _IsAllowUploadPhoto = value; } }
        }

        private int _iAuditTrailPeriod;
        /// <summary>
        /// AuditTrailPeriod
        /// </summary>
        public int AuditTrailPeriod
        {
            get { return _iAuditTrailPeriod; }
            set { if (this._iAuditTrailPeriod != value) { _iAuditTrailPeriod = value; } }
        }

        private string _iJiraHelpDeskProjectId;
        /// <summary>
        /// JiraHelpDeskProjectId
        /// </summary>
        public string JiraHelpDeskProjectId
        {
            get { return _iJiraHelpDeskProjectId; }
            set { if (this._iJiraHelpDeskProjectId != value) { _iJiraHelpDeskProjectId = value; } }
        }

        private string _iJiraHelpDeskRequestType;
        /// <summary>
        /// JiraHelpDeskProjectId
        /// </summary>
        public string JiraHelpDeskRequestType
        {
            get { return _iJiraHelpDeskRequestType; }
            set { if (this._iJiraHelpDeskRequestType != value) { _iJiraHelpDeskRequestType = value; } }
        }

       private string _iDocLicensePath; //change to LicencePath
        /// <summary>
        /// DocLicensePath
        /// </summary>
        public string DocLicensePath
        {
            get { return _iDocLicensePath; }
            set { if (this._iDocLicensePath != value) { _iDocLicensePath = value; } }
        }
        private string _iRecipientEmailId;
        /// <summary>
        /// RecipientEmailId
        /// </summary>
        public string RecipientEmailId
        {
            get { return _iRecipientEmailId; }
            set { if (this._iRecipientEmailId != value) { _iRecipientEmailId = value; } }
        }
        private string _iGWTCADatabaseName;
        /// <summary>
        /// RecipientEmailId
        /// </summary>
        public string GWTCADatabaseName
        {
            get { return _iGWTCADatabaseName; }
            set { if (this._iGWTCADatabaseName != value) { _iGWTCADatabaseName = value; } }
        }
    }
}