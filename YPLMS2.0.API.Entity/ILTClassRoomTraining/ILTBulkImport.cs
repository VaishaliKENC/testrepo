using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class ILTBulkImport : BaseEntity
    {

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
        public string ILTImportFilePath
        {
            get { return _importFilePath; }
            set { if (this._importFilePath != value) { _importFilePath = value; } }
        }


        private string _strUploadedFileName;
        public string UploadedFileName
        {
            get { return _strUploadedFileName; }
            set { if (this._strUploadedFileName != value) { _strUploadedFileName = value; } }
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


        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
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
        public ImportType ILTImportType
        {
            get { return _enumImportType; }
            set { if (this._enumImportType != value) { _enumImportType = value; } }
        }


        private ImportStatus _importStatus;
        public ImportStatus ILTImportStatus
        {
            get { return _importStatus; }
            set { if (this._importStatus != value) { _importStatus = value; } }
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


    }




}
