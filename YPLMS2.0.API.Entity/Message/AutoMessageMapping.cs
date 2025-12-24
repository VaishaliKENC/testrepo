using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class AutoMessageMapping:BaseEntity
    {
        public AutoMessageMapping()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetMessageLanguages
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

        private string _templateID;
        public string TemplateID
        {
            get { return _templateID; }
            set { if (this._templateID != value) { _templateID = value; } }
        }

        private string _templateTitle;
        public string TemplateTitle
        {
            get { return _templateTitle; }
            set { if (this._templateTitle != value) { _templateTitle = value; } }
        }

        private string _eventName;
        public string EventName
        {
            get { return _eventName; }
            set { if (this._eventName != value) { _eventName = value; } }
        }

        private string _languageID;
        public string LanguageID
        {
            get { return _languageID; }
            set { if (this._languageID != value) { _languageID = value; } }
        }

        private string _LanguageName;
        public string LanguageName
        {
            get { return _LanguageName; }
            set { if (this._LanguageName != value) { _LanguageName = value; } }
        }



        #region EVENT ID CONSTANTS
        public const string EVENT_LEARNER_ACTIVITY_ASSIGNMENT = "MT000001";
        public const string EVENT_LEARNER_ACTIVITY_UNASSIGNMENT = "MTZ7PYaGnB";
        public const string EVENT_LEARNER_FORUMTHREAD_POST = "MT000002";
        public const string EVENT_LEARNER_BLOGPOST_COMMENT = "MT000003";
        public const string EVENT_ACTIVITY_COMPLETION = "MT000004";
        public const string EVENT_CLASSROOMTRAINING_EVENT_AVAILABLE = "MT000005";
        public const string EVENT_CLASSROOMTRAINING_NOMINATION_ADDED = "MT000006";
        public const string EVENT_CLASSROOMTRAINING_NOMINATION_STATUS_CHANGED = "MT000007";
        public const string EVENT_CLASSROOMTRAINING_ATTENDANCE = "MT000008";
        #endregion
    }
}
