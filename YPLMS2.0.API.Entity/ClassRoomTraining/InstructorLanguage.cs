using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class InstructorLanguage : BaseEntity
    {
        public InstructorLanguage()
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

        private string _strInstructorName;
        /// <summary>
        /// InstructorName
        /// </summary>
        public string InstructorName
        {
            get { return _strInstructorName; }
            set { if (this._strInstructorName != value) { _strInstructorName = value; } }
        }

       

        private string _strInstructorDetails;
        /// <summary>
        /// Email
        /// </summary>
        public string InstructorDetails
        {
            get { return _strInstructorDetails; }
            set { if (this._strInstructorDetails != value) { _strInstructorDetails = value; } }
        }

    }
}
