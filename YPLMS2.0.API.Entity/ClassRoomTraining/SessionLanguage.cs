using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class SessionLanguage : BaseEntity
    {
        public SessionLanguage()
        { }

        private string _strLanguageId;
        /// <summary>
        /// LanguageId
        /// </summary>
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }

        private string _strLanguageName;
        /// <summary>
        /// LanguageId
        /// </summary>
        public string LanguageName
        {
            get { return _strLanguageName; }
            set { if (this._strLanguageName != value) { _strLanguageName = value; } }
        }

        private string _strSessionName;
        /// <summary>
        /// SessionName
        /// </summary>
        public string SessionName
        {
            get { return _strSessionName; }
            set { if (this._strSessionName != value) { _strSessionName = value; } }
        }

        private string _strSessionDescription;
        /// <summary>
        /// SpeakerName
        /// </summary>
        public string SessionDescription
        {
            get { return _strSessionDescription; }
            set { if (this._strSessionDescription != value) { _strSessionDescription = value; } }
        }
    }
}
