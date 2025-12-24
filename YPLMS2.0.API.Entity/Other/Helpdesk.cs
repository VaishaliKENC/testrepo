/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh
* Created:<28/08/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
   [Serializable] public class Helpdesk:BaseEntity
    {
        public Helpdesk()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Add,
            AddAttachment,
            Get
        }

        private string _strUserId;
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId
        {
            get { return _strUserId; }
            set { if (this._strUserId != value) { _strUserId = value; } }
        }     

        private string _strEmail;
        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get { return _strEmail; }
            set { if (this._strEmail != value) { _strEmail = value; } }
        }

        private string _strTitle;
        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return _strTitle; }
            set { if (this._strTitle != value) { _strTitle = value; } }
        }

        private string _strIssueType;
        /// <summary>
        /// IssueType
        /// </summary>
        public string IssueType
        {
            get { return _strIssueType; }
            set { if (this._strIssueType != value) { _strIssueType = value; } }
        }

        private string _strActivityType;
        /// <summary>
        /// ActivityType
        /// </summary>
        public string ActivityType
        {
            get { return _strActivityType; }
            set { if (this._strActivityType != value) { _strActivityType = value; } }
        }

        private string _strOperatingSystem;
        /// <summary>
        /// OperatingSystem
        /// </summary>
        public string OperatingSystem
        {
            get { return _strOperatingSystem; }
            set { if (this._strOperatingSystem != value) { _strOperatingSystem = value; } }
        }

        private string _strBrowser;
        /// <summary>
        /// Browser
        /// </summary>
        public string Browser
        {
            get { return _strBrowser; }
            set { if (this._strBrowser != value) { _strBrowser = value; } }
        }

        private string _strDescription;
        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get { return _strDescription; }
            set { if (this._strDescription != value) { _strDescription = value; } }
        }

        private string _strComments;
        /// <summary>
        /// Comments
        /// </summary>
        public string Comments
        {
            get { return _strComments; }
            set { if (this._strComments != value) { _strComments = value; } }
        }

        private System.Nullable<bool> _strIsActive;
        /// <summary>
        /// IsActive
        /// </summary>
        public System.Nullable<bool> IsActive
        {
            get { return _strIsActive; }
            set { if (this._strIsActive != value) { _strIsActive = value; } }
        }

        private string _strActivityName;
        /// <summary>
        /// ActivityName
        /// </summary>
        public string ActivityName
        {
            get { return _strActivityName; }
            set { if (this._strActivityName != value) { _strActivityName = value; } }
        }

        private Nullable<bool> _strIsClosed;
        /// <summary>
        /// IsClosed
        /// </summary>
        public Nullable<bool> IsClosed
        {
            get { return _strIsClosed; }
            set { if (this._strIsClosed != value) { _strIsClosed = value; } }
        }

        private string _strStatus;
        /// <summary>
        /// Status
        /// </summary>
        public string Status
        {
            get { return _strStatus; }
            set { if (this._strStatus != value) { _strStatus = value; } }
        }

        private string _strRelativePath;
        /// <summary>
        /// RelativePath
        /// </summary>
        public string RelativePath
        {
            get { return _strRelativePath; }
            set { if (this._strRelativePath != value) { _strRelativePath = value; } }
        }

        private string _strAttachmentHelpdeskId;
        /// <summary>
        /// AttachmentHelpdeskId
        /// </summary>
        public string AttachmentHelpdeskId
        {
            get { return _strAttachmentHelpdeskId; }
            set { if (this._strAttachmentHelpdeskId != value) { _strAttachmentHelpdeskId = value; } }
        }

        private string _strTicketNo;
        /// <summary>
        /// TicketNo
        /// </summary>
        public string TicketNo
        {
            get { return _strTicketNo; }
            set { if (this._strTicketNo != value) { _strTicketNo = value; } }
        }
    }
}