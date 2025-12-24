using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
     [Serializable]
   public class MessageTemplate : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public MessageTemplate()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetALLLearner,
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

        private bool _isDefault;
        public bool IsDefault
        {
            get { return _isDefault; }
            set { if (this._isDefault != value) { _isDefault = value; } }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
        }

        private string _languageID;
        public string LanguageID
        {
            get { return _languageID; }
            set { if (this._languageID != value) { _languageID = value; } }
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

        private string _LanguageName;
        public string LanguageName
        {
            get { return _LanguageName; }
            set { if (this._LanguageName != value) { _LanguageName = value; } }
        }
    }
}
