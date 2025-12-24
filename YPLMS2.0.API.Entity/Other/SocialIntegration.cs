using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
   public  class SocialIntegration : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public SocialIntegration()
        { }
        /// <summary>
        /// ENUM Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetFBAppId,
        }
        public new enum ListMethod
        {
            GetByActivityType_ID,
        }
        private string _strTemplateId;
        /// <summary>
        /// User Id
        /// </summary>
        public string TemplateId
        {
            get { return _strTemplateId; }
            set { if (this._strTemplateId != value) { _strTemplateId = value; } }
        }

        private string _strTemplateName;
        /// <summary>
        /// User Id
        /// </summary>
        public string TemplateName
        {
            get { return _strTemplateName; }
            set { if (this._strTemplateName != value) { _strTemplateName = value; } }
        }


        private string _strActivityType;
        /// <summary>
        /// User Id
        /// </summary>
        public string ActivityType
        {
            get { return _strActivityType; }
            set { if (this._strActivityType != value) { _strActivityType = value; } }
        }


        private string _strTemplateString;
        /// <summary>
        /// User Id
        /// </summary>
        public string TemplateString
        {
            get { return _strTemplateString; }
            set { if (this._strTemplateString != value) { _strTemplateString = value; } }
        }


        private string _strActivityId;
        /// <summary>
        /// User Id
        /// </summary>
        public string ActivityId
        {
            get { return _strActivityId; }
            set { if (this._strActivityId != value) { _strActivityId = value; } }
        }

        private string _strLanguageId;
        /// <summary>
        /// User Id
        /// </summary>
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }

        private string _strSystemUserGUID;
        /// <summary>
        /// User Id
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private string _strAppID;
        /// <summary>
        /// User Id
        /// </summary>
        public string AppID
        {
            get { return _strAppID; }
            set { if (this._strAppID != value) { _strAppID = value; } }
        }
        private string _strIsDebugMode;
        /// <summary>
        /// User Id
        /// </summary>
        public string _IsDebugMode
        {
            get { return _strIsDebugMode; }
            set { if (this._strIsDebugMode != value) { _strIsDebugMode = value; } }
        }
    }
}