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
    public class AssessmentSectionsLanguage : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public AssessmentSectionsLanguage()
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

        private string _sectionTitle;
        public string SectionTitle
        {
            get { return _sectionTitle; }
            set { if (this._sectionTitle != value) { _sectionTitle = value; } }
        }

        private string _sectionName;
        public string SectionName
        {
            get { return _sectionName; }
            set { if (this._sectionName != value) { _sectionName = value; } }
        }

        private string _sectionDescription;
        public string SectionDescription
        {
            get { return _sectionDescription; }
            set { if (this._sectionDescription != value) { _sectionDescription = value; } }
        }
    }
}