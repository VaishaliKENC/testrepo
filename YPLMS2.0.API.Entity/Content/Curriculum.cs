/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh & Ashish
* Created:<13/07/09>
* Last Modified:<dd/mm/yy>
*/

using System.Collections.Generic;
using System;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class Curriculum :CurriculumLanguage
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public Curriculum()
        {
            _strcurriculumActivitys = new List<CurriculumActivity>();
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetALLCurriculamLanguages,
            GetAllForEcatalog,
            GetAllForDynamicAssignment,
            GetCurriculumExistingActivity
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete,            
            GetByName,
            GetCurriculumsByID_Learner,
            UpdateLanguage,
            UpdateOptions,
            CopyCurriculum
        }

        private List<CurriculumActivity> _strcurriculumActivitys;
        public List<CurriculumActivity> CurriculumActivity
        {
            get { return _strcurriculumActivitys; }
            set { if (this._strcurriculumActivitys != value) { _strcurriculumActivitys = value; } }
        }

        private bool _strEnforceActivitySequencing;
        public bool EnforceActivitySequencing
        {
            get { return _strEnforceActivitySequencing; }
            set { if (this._strEnforceActivitySequencing != value) { _strEnforceActivitySequencing = value; } }
        }

        //private string _strCurriculumName;
        //public string CurriculumName
        //{
        //    get { return _strCurriculumName; }
        //    set { if (this._strCurriculumName != value) { _strCurriculumName = value; } }
        //}

        //private string _strCurriculumDescription;
        //public string CurriculumDescription
        //{
        //    get { return _strCurriculumDescription; }
        //    set { if (this._strCurriculumDescription != value) { _strCurriculumDescription = value; } }
        //}

        //private string _strCurriculumInstruction;
        //public string CurriculumInstruction
        //{
        //    get { return _strCurriculumInstruction; }
        //    set { if (this._strCurriculumInstruction != value) { _strCurriculumInstruction = value; } }
        //}

        private bool _bEnforceSequencing;
        public bool EnforceSequencing
        {
            get { return _bEnforceSequencing; }
            set { if (this._bEnforceSequencing != value) { _bEnforceSequencing = value; } }
        }

        private bool _bIsPrintCertificate;
        public bool IsPrintCertificate
        {
            get { return _bIsPrintCertificate; }
            set { if (this._bIsPrintCertificate != value) { _bIsPrintCertificate = value; } }
        }

        private string _strCompletionConditionId;
        public string CompletionConditionId
        {
            get { return _strCompletionConditionId; }
            set { if (this._strCompletionConditionId != value) { _strCompletionConditionId = value; } }
        }

      

        private string _strModifiedByName;
        public string ModifiedByName
        {
            get { return _strModifiedByName; }
            set { if (this._strModifiedByName != value) { _strModifiedByName = value; } }
        }

        private bool _bIsActive;
        public bool IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
        }

        private bool _bIsUsed;
        public bool IsUsed
        {
            get { return _bIsUsed; }
            set { if (this._bIsUsed != value) { _bIsUsed = value; } }
        }

        //private string _strLanguageId;
        //public string LanguageId
        //{
        //    get { return _strLanguageId; }
        //    set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        //}

        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }

        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }

        public bool IsContainSections { get; set; }
        public bool EnforceSectionSequencing { get; set; }

        public List<CurriculumSection> CurriculumSection { get; set; }
        public bool IsSequenceOrder  { get; set; }
        public decimal Progress { get; set; }

        private string _strCurriculumId;
        public string CurriculumId
        {
            get { return _strCurriculumId; }
            set { if (this._strCurriculumId != value) { _strCurriculumId = value; } }
        }

        private string _strActivityId;
        public string ActivityId
        {
            get { return _strActivityId; }
            set { if (this._strActivityId != value) { _strActivityId = value; } }
        }

    }
}