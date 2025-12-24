using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class SpeakerLanguage : BaseEntity
    {
        public SpeakerLanguage()
        {
        }

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

        private string _strSpeakerName;
        /// <summary>
        /// SpeakerName
        /// </summary>
        public string SpeakerName
        {
            get { return _strSpeakerName; }
            set { if (this._strSpeakerName != value) { _strSpeakerName = value; } }
        }

        private string _strEmail;
        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get { return _strEmail; }
            set { if (this._strEmail != value) { _strEmail = value; } }
        }

        private string _strSpeakerDetails;
        /// <summary>
        /// Email
        /// </summary>
        public string SpeakerDetails
        {
            get { return _strSpeakerDetails; }
            set { if (this._strSpeakerDetails != value) { _strSpeakerDetails = value; } }
        }

    }
}
