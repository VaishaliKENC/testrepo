using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class AssignmentTopic:BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public AssignmentTopic()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllTopicLanguages
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

        private string _topicTitle;
        public string TopicTitle
        {
            get { return _topicTitle; }
            set { if (this._topicTitle != value) { _topicTitle = value; } }
        }

        private string _topicDescription;
        public string TopicDescription
        {
            get { return _topicDescription; }
            set { if (this._topicDescription != value) { _topicDescription = value; } }
        }

        private string _assignmentId;
        public string AssignmentId
        {
            get { return _assignmentId; }
            set { if (this._assignmentId != value) { _assignmentId = value; } }
        }

        private bool _isMandatoryForSubmission;
        public bool IsMandatoryForSubmission
        {
            get { return _isMandatoryForSubmission; }
            set { if (this._isMandatoryForSubmission != value) { _isMandatoryForSubmission = value; } }
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
