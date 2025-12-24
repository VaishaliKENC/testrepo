using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class Bookmark : BaseEntity
    {
         /// <summary>
        /// Default Contructor
        /// <summary>
        public Bookmark()
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
            Get,
            Add,
            Update,
            Delete
        }

        private string _activityId;
        public string ActivityId
        {
            get { return _activityId; }
            set { if (this._activityId != value) { _activityId = value; } }
        }

        private string _activityType;
        public string ActivityType
        {
            get { return _activityType; }
            set { if (this._activityType != value) { _activityType = value; } }
        }

        private string _activityName;
        public string ActivityName
        {
            get { return _activityName; }
            set { if (this._activityName != value) { _activityName = value; } }
        }

        private string _completionStatus;
        public string CompletionStatus
        {
            get { return _completionStatus; }
            set { if (this._completionStatus != value) { _completionStatus = value; } }
        }

        private string _systemUserGUID;
        public string SystemUserGUID
        {
            get { return _systemUserGUID; }
            set { if (this._systemUserGUID != value) { _systemUserGUID = value; } }
        }

        private string _languageId;
        public string LanguageId
        {
            get { return _languageId; }
            set { if (this._languageId != value) { _languageId = value; } }
        }

        private DateTime _bookmarkDate;
        /// <summary>
        /// Bookmark Date
        /// </summary>
        public DateTime BookmarkDate
        {
            get { return _bookmarkDate; }
            set { if (this._bookmarkDate != value) { _bookmarkDate = value; } }
        }
    }
}
