/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh & Ashish
* Created:<13/07/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class EmailTemplateLanguage : EmailTemplate 
    /// </summary>
    [Serializable]
   public class EmailTemplateLanguage : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public EmailTemplateLanguage()
        { }

        private string _strLanguageId;
        /// <summary>
        /// Language Id
        /// </summary>
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }

        private string _strEmailTemplateTitle;
        /// <summary>
        /// Email Template Title
        /// </summary>
        public string EmailTemplateTitle
        {
            get { return _strEmailTemplateTitle; }
            set { if (this._strEmailTemplateTitle != value) { _strEmailTemplateTitle = value; } }
        }

        private string _strEmailTemplateName;
        /// <summary>
        /// Email Template Name
        /// </summary>
        public string EmailTemplateName
        {
            get { return _strEmailTemplateName; }
            set { if (this._strEmailTemplateName != value) { _strEmailTemplateName = value; } }
        }

        private string _strDisplayName;
        /// <summary>
        /// Display Name
        /// </summary>
        public string DisplayName
        {
            get { return _strDisplayName; }
            set { if (this._strDisplayName != value) { _strDisplayName = value; } }
        }

        private string _strEmailSubjectText;
        /// <summary>
        /// Email Subject Text
        /// </summary>
        public string EmailSubjectText
        {
            get { return _strEmailSubjectText; }
            set { if (this._strEmailSubjectText != value) { _strEmailSubjectText = value; } }
        }

        private string _strEmailBodyText;
        /// <summary>
        /// Email Body Text
        /// </summary>
        public string EmailBodyText
        {
            get { return _strEmailBodyText; }
            set { if (this._strEmailBodyText != value) { _strEmailBodyText = value; } }
        }

        private string _strRTLType;
        /// <summary>
        /// RTL Type
        /// </summary>
        public string RTLType
        {
            get { return _strRTLType; }
            set { if (this._strRTLType != value) { _strRTLType = value; } }
        }

        private EmailApprovalStatus _strApprovalStatus;
        /// <summary>
        /// Approval Status
        /// </summary>
        public EmailApprovalStatus ApprovalStatus
        {
            get { return _strApprovalStatus; }
            set { if (this._strApprovalStatus != value) { _strApprovalStatus = value; } }
        }

        private string _strEmailAddressString;
        /// <summary>
        /// Email Address String
        /// </summary>
        public string EmailAddressString
        {
            get { return _strEmailAddressString; }
            set { if (this._strEmailAddressString != value) { _strEmailAddressString = value; } }
        }

        private string _strEmailType;
        /// <summary>
        /// Email Type
        /// </summary>
        public string EmailType
        {
            get { return _strEmailType; }
            set { if (this._strEmailType != value) { _strEmailType = value; } }
        }

        private Nullable<Boolean> _bIsActive;
        /// <summary>
        /// Is Active
        /// </summary>
        public Nullable<Boolean> IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
        }

        private string _strApprovedById;
        /// <summary>
        /// Approved By Id : SystemUserGUID for user who approved a language template
        /// </summary>
        public string ApprovedById
        {
            get { return _strApprovedById; }
            set { if (this._strApprovedById != value) { _strApprovedById = value; } }
        }

        private string _strModifiedByName;
        /// <summary>
        /// Modified by Name
        /// </summary>
        public string ModifiedByName
        {
            get { return _strModifiedByName; }
            set { if (this._strModifiedByName != value) { _strModifiedByName = value; } }
        }

        public enum EmailApprovalStatus
        {
            None, //for getting all languages
            Draft,
            SubmittedForApproval,
            Approved
        }
    }
}