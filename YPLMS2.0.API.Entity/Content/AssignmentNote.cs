using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class AssignmentNote:BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public AssignmentNote()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllNoteLanguages
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

        private string _note;
        public string Note
        {
            get { return _note; }
            set { if (this._note != value) { _note = value; } }
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

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
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
