using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class ProgramLanguage : BaseEntity
    {
        public ProgramLanguage()
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

        private string _strProgramName;
        /// <summary>
        /// LanguageId
        /// </summary>
        public string ProgramName
        {
            get { return _strProgramName; }
            set { if (this._strProgramName != value) { _strProgramName = value; } }
        }

        private string _strProgramDescription;
        /// <summary>
        /// LanguageId
        /// </summary>
        public string ProgramDescription
        {
            get { return _strProgramDescription; }
            set { if (this._strProgramDescription != value) { _strProgramDescription = value; } }
        }

        private string _strProgramPreWork;
        /// <summary>
        /// LanguageId
        /// </summary>
        public string ProgramPreWork
        {
            get { return _strProgramPreWork; }
            set { if (this._strProgramPreWork != value) { _strProgramPreWork = value; } }
        }

        private string _strProgramPostWork;
        /// <summary>
        /// LanguageId
        /// </summary>
        public string ProgramPostWork
        {
            get { return _strProgramPostWork; }
            set { if (this._strProgramPostWork != value) { _strProgramPostWork = value; } }
        }

        private string _strContactPersonEmailID;
        /// <summary>
        /// LanguageId
        /// </summary>
        public string ContactPersonEmailID
        {
            get { return _strContactPersonEmailID; }
            set { if (this._strContactPersonEmailID != value) { _strContactPersonEmailID = value; } }
        }
    }
}
