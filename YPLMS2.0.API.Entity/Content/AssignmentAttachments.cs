using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class AssignmentAttachments:BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public AssignmentAttachments()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllAttachmentLanguages
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
            UpdateLanguage,
            DeleteLanguage
        }

        private string _attachmentName;
        public string AttachmentName
        {
            get { return _attachmentName; }
            set { if (this._attachmentName != value) { _attachmentName = value; } }
        }

        private string _assignmentId;
        public string AssignmentId
        {
            get { return _assignmentId; }
            set { if (this._assignmentId != value) { _assignmentId = value; } }
        }

        private byte[] _attachmentFile;
        /// <summary>
        /// File Content
        /// </summary>
        public byte[] AttachmentFile
        {
            get { return _attachmentFile; }
            set { if (this._attachmentFile != value) { _attachmentFile = value; } }
        }

        private string _LanguageId;
        public string LanguageId
        {
            get { return _LanguageId; }
            set { if (this._LanguageId != value) { _LanguageId = value; } }
        }

        private string _LanguageName;
        public string LanguageName
        {
            get { return _LanguageName; }
            set { if (this._LanguageName != value) { _LanguageName = value; } }
        }
    }
}
