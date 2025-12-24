/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:<Shailesh Patil>
* Created:<14/09/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
     public class QuestionnaireOptionsLanguage : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public QuestionnaireOptionsLanguage()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            AddAll
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete
        }
        
        private string _languageId;
        public string LanguageId
        {
            get { return _languageId; }
            set { if (this._languageId != value) { _languageId = value; } }
        }

        private string _optionTitle;
        public string OptionTitle
        {
            get { return _optionTitle; }
            set { if (this._optionTitle != value) { _optionTitle = value; } }
        }

        private string _optionDescription;
        public string OptionDescription
        {
            get { return _optionDescription; }
            set { if (this._optionDescription != value) { _optionDescription = value; } }
        }

        /// <summary>
        /// ExplainationTitle
        /// </summary>
        private string _explainationTitle;
        public string ExplainationTitle
        {
            get { return _explainationTitle; }
            set { if (this._explainationTitle != value) { _explainationTitle = value; } }
        }      
    }
}