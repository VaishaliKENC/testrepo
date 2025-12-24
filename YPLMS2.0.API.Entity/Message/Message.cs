using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
   public class Message : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public Message()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllTop10,
            GetALLLearner
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            UpdateDetails,
            Delete,
            GetMessageCount,
            GetTotalMessageCount,
            GetUnreadCustomCount,
            GetUnreadSystemCount
        }

        private string _messageID;
        public string MessageID
        {
            get { return _messageID; }
            set { if (this._messageID != value) { _messageID = value; } }
        }

        private string _recipientID;
        public string RecipientID
        {
            get { return _recipientID; }
            set { if (this._recipientID != value) { _recipientID = value; } }
        }

        private string _businessRuleID;
        public string BusinessRuleID
        {
            get { return _businessRuleID; }
            set { if (this._businessRuleID != value) { _businessRuleID = value; } }
        }

        private string _messageTitle;
        public string MessageTitle
        {
            get { return _messageTitle; }
            set { if (this._messageTitle != value) { _messageTitle = value; } }
        }

        private string _messageBody;
        public string MessageBody
        {
            get { return _messageBody; }
            set { if (this._messageBody != value) { _messageBody = value; } }
        }

        private bool _isRead;
        public bool IsRead
        {
            get { return _isRead; }
            set { if (this._isRead != value) { _isRead = value; } }
        }

        private bool _isDeleted;
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { if (this._isDeleted != value) { _isDeleted = value; } }
        }

        private Nullable<bool> _isCustomMessage;
        public Nullable<bool> IsCustomMessage
        {
            get { return _isCustomMessage; }
            set { if (this._isCustomMessage != value) { _isCustomMessage = value; } }
        }

        private DateTime _readDate;
        /// <summary>
        /// Read Date
        /// </summary>
        public DateTime ReadDate
        {
            get { return _readDate; }
            set { if (this._readDate != value) { _readDate = value; } }
        }

        private DateTime _dateCreatedTo;
        /// <summary>
        /// Read Date
        /// </summary>
        public DateTime DateCreatedTo
        {
            get { return _dateCreatedTo; }
            set { if (this._dateCreatedTo != value) { _dateCreatedTo = value; } }
        }

        private string _systemUserGUID;
        public string SystemUserGUID
        {
            get { return _systemUserGUID; }
            set { if (this._systemUserGUID != value) { _systemUserGUID = value; } }
        }

        private string _usernameAlias;
        public string UsernameAlias
        {
            get { return _usernameAlias; }
            set { if (this._usernameAlias != value) { _usernameAlias = value; } }
        }

        private string _recipients;
        public string Recipients
        {
            get { return _recipients; }
            set { if (this._recipients != value) { _recipients = value; } }
        }

        private string _languageId;
        public string LanguageId
        {
            get { return _languageId; }
            set { if (this._languageId != value) { _languageId = value; } }
        }

        private int _customCount;
        public int CustomCount
        {
            get { return _customCount; }
            set { if (this._customCount != value) { _customCount = value; } }
        }

        private int _systemCount;
        public int SystemCount
        {
            get { return _systemCount; }
            set { if (this._systemCount != value) { _systemCount = value; } }
        }
    }
}
