using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class RefMaterialMasterLanguages:BaseEntity
    {
        public RefMaterialMasterLanguages()
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

        private string _strRefMaterialName;
        /// <summary>
        /// RefMaterialName
        /// </summary>
        public string RefMaterialName
        {
            get { return _strRefMaterialName; }
            set { if (this._strRefMaterialName != value) { _strRefMaterialName = value; } }
        }

        private string _strRefMaterialDescription;
        /// <summary>
        /// RefMaterialDescription
        /// </summary>
        public string RefMaterialDescription
        {
            get { return _strRefMaterialDescription; }
            set { if (this._strRefMaterialDescription != value) { _strRefMaterialDescription = value; } }
        }

        private string _strFileName;
        /// <summary>
        /// RefMaterialFileName
        /// </summary>
        public string FileName
        {
            get { return _strFileName; }
            set { if (this._strFileName != value) { _strFileName = value; } }
        }
    }
}
