using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class SessionLocationLanguage : BaseEntity
    {
        public SessionLocationLanguage()
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

        private string _strLocationName;
        /// <summary>
        /// LocationName
        /// </summary>
        public string LocationName
        {
            get { return _strLocationName; }
            set { if (this._strLocationName != value) { _strLocationName = value; } }
        }

        private string _strLocationVenue;
        /// <summary>
        /// LocationVenue
        /// </summary>
        public string LocationVenue
        {
            get { return _strLocationVenue; }
            set { if (this._strLocationVenue != value) { _strLocationVenue = value; } }
        }
    }
}
