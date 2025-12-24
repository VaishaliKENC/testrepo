/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Ashish
* Created:<12/11/09>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
   [Serializable] public class BulkImport:BaseEntity
    {
        /// <summary>
        /// Default Contructor tblBulkImportMaster
        /// <summary>
        public BulkImport()
        { 
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


  

        private string _title;
        public string Title
        {
            get { return _title; }
            set { if (this._title != value) { _title = value; } }
        }


        private string _importFilePath;
        public string ImportFilePath
        {
            get { return _importFilePath; }
            set { if (this._importFilePath != value) { _importFilePath = value; } }
        }

        private string _paraPreferredDateFormat;
        public string PreferredDateFormat
        {
            get { return _paraPreferredDateFormat; }
            set { if (this._paraPreferredDateFormat != value) { _paraPreferredDateFormat = value; } }
        }


        private string _paraPreferredTimeZone;
        public string PreferredTimeZone
        {
            get { return _paraPreferredTimeZone; }
            set { if (this._paraPreferredTimeZone != value) { _paraPreferredTimeZone = value; } }
        }



        private System.DateTime _scheduledDateAndTime;
        public System.DateTime ScheduledDateAndTime
        {
            get { return _scheduledDateAndTime; }
            set { if (this._scheduledDateAndTime != value) { _scheduledDateAndTime = value; } }
        }


        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
        }

        private bool _isImmeidateForSchedular=false;
        public bool ImmeidateForSchedular
        {
            get { return _isImmeidateForSchedular; }
            set { if (this._isImmeidateForSchedular != value) { _isImmeidateForSchedular = value; } }
        }

         private bool _isSendEmail;
        public bool SendEmail
        {
            get { return _isSendEmail; }
            set { if (this._isSendEmail != value) { _isSendEmail = value; } }
        }

        private bool _DirectSendEmail;
        public bool DirectSendEmail
        {
            get { return _DirectSendEmail; }
            set { if (this._DirectSendEmail != value) { _DirectSendEmail = value; } }
        }

        private string _emailType;
        public string EmailType
        {
            get { return _emailType; }
            set { if (this._emailType != value) { _emailType = value; } }
        }

        private string _emailTemplateId;
        public string EmailTemplateId
        {
            get { return _emailTemplateId; }
            set { if (this._emailTemplateId != value) { _emailTemplateId = value; } }
        }


         private bool _isOverridePreviousAssignment;
        public bool IsOverridePreviousAssignment
        {
            get { return _isOverridePreviousAssignment; }
            set { if (this._isOverridePreviousAssignment != value) { _isOverridePreviousAssignment = value; } }
        }

         private bool _isAssigned;
        public bool IsAssigned
        {
            get { return _isAssigned; }
            set { if (this._isAssigned != value) { _isAssigned = value; } }
        }

        private string _fieldMapping;
        public string FieldMapping
        {
            get { return _fieldMapping; }
            set { if (this._fieldMapping != value) { _fieldMapping = value; } }
        }

        
         private ImportAction _enumImportAction;
        public ImportAction ImportAction
        {
            get { return _enumImportAction; }
            set { if (this._enumImportAction != value) { _enumImportAction = value; } }
        }

        private ImportType _enumImportType;
        public ImportType ImportType
        {
            get { return _enumImportType; }
            set { if (this._enumImportType != value) { _enumImportType = value; } }
        }


        private ImportStatus _importStatus;
        public ImportStatus ImportStatus
        {
            get { return _importStatus; }
            set { if (this._importStatus != value) { _importStatus = value; } }
        }


        private bool _createPassword;
        public bool CreatePassword
        {
            get { return _createPassword; }
            set { if (this._createPassword != value) { _createPassword = value; } }
        }


         private string _Password;
         public string Password
        {
            get { return _Password; }
            set { if (this._Password != value) { _Password = value; } }
        }

        private string _scheduledEmailTaskId;
        public string ScheduledEmailTaskId
        {
            get { return _scheduledEmailTaskId; }
            set { if (this._scheduledEmailTaskId != value) { _scheduledEmailTaskId = value; } }
        }

         private string _taskId;
        public string TaskId
        {
            get { return _taskId; }
            set { if (this._taskId != value) { _taskId = value; } }
        }
        private string _strComment;
        public string Comment
        {
            get { return _strComment; }
            set { if (this._strComment != value) { _strComment = value; } }
        }

        private Nullable<DateTime> _dtCompletionDate;
        public Nullable<DateTime> CompletionDate
        {
            get { return _dtCompletionDate; }
            set { if (this._dtCompletionDate != value) { _dtCompletionDate = value; } }
        }


         private string _strUploadedFileName;
        public string UploadedFileName
        {
            get { return _strUploadedFileName; }
            set { if (this._strUploadedFileName != value) { _strUploadedFileName = value; } }
        }

         private string _strAddNewCustomFields;
        public string AddNewCustomFields
        {
            get { return _strAddNewCustomFields; }
            set { if (this._strAddNewCustomFields != value) { _strAddNewCustomFields = value; } }
        }

         private string _strAddNewOrganizationLevels;
        public string AddNewOrganizationLevels
        {
            get { return _strAddNewOrganizationLevels; }
            set { if (this._strAddNewOrganizationLevels != value) { _strAddNewOrganizationLevels = value; } }
        }


          private bool _isKeepExistingPassword;
        public bool KeepExistingPassword
        {
            get { return _isKeepExistingPassword; }
            set { if (this._isKeepExistingPassword != value) { _isKeepExistingPassword = value; } }
        }

        private bool _isInProcess;
        public bool IsInProcess
        {
            get { return _isInProcess; }
            set { if (this._isInProcess != value) { _isInProcess = value; } }
        }
       

         
    }
}
