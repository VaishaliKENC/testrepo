using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.Entity.ViewModel
{
    
    public class ContentModuleLanguagesVM : BaseEntityVM
    {
       
        private string? _strContentModuleName;
        /// <summary>
        /// Content Module Name
        /// </summary>
        public string? ContentModuleName
        {
            get { return _strContentModuleName; }
            set { if (this._strContentModuleName != value) { _strContentModuleName = value; } }
        }

        private string? _strContentModuleDescription;
        /// <summary>
        /// Content Module Description
        /// </summary>
        public string? ContentModuleDescription
        {
            get { return _strContentModuleDescription; }
            set { if (this._strContentModuleDescription != value) { _strContentModuleDescription = value; } }
        }

        private string? _strLanguageId;
        /// <summary>
        /// Language Id
        /// </summary>
        public string? LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }

        private string? _strContentModuleKeyWords;
        /// <summary>
        /// ContentModuleKeyWords
        /// </summary>
        public string? ContentModuleKeyWords
        {
            get { return _strContentModuleKeyWords; }
            set { if (this._strContentModuleKeyWords != value) { _strContentModuleKeyWords = value; } }
        }

        
    }
}
