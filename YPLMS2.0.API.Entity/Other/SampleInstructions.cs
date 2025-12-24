using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class SampleInstructions : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public SampleInstructions()
        { }

        /// <summary>
        /// ENUM Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add
        }

        private string _strModuleName;
        /// <summary>
        /// User Id
        /// </summary>
        public string ModuleName
        {
            get { return _strModuleName; }
            set { if (this._strModuleName != value) { _strModuleName = value; } }
        }

        private string _strSubModuleName;
        /// <summary>
        /// User Id
        /// </summary>
        public string SubModuleName
        {
            get { return _strSubModuleName; }
            set { if (this._strSubModuleName != value) { _strSubModuleName = value; } }
        }

        private string _strPage;
        /// <summary>
        /// User Id
        /// </summary>
        public string Page
        {
            get { return _strPage; }
            set { if (this._strPage != value) { _strPage = value; } }
        }

        private string _strImportOrTranslate;
        /// <summary>
        /// User Id
        /// </summary>
        public string ImportOrTranslate
        {
            get { return _strImportOrTranslate; }
            set { if (this._strImportOrTranslate != value) { _strImportOrTranslate = value; } }
        }

        private string _strPurpose;
        /// <summary>
        /// User Id
        /// </summary>
        public string Purpose
        {
            get { return _strPurpose; }
            set { if (this._strPurpose != value) { _strPurpose = value; } }
        }

        private string _strSampleInstructions;
        /// <summary>
        /// User Id
        /// </summary>
        public string SampleInstruction
        {
            get { return _strSampleInstructions; }
            set { if (this._strSampleInstructions != value) { _strSampleInstructions = value; } }
        }


        
    }
}
