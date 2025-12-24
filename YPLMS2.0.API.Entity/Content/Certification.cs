/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Charu Singh
* Created:<17/09/09>
* Last Modified:<25/12/09>
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
   [Serializable] public class Certification:BaseEntity 
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public Certification()
        {
            _strCertificationActivitys = new List<CertificationActivity>();             
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetCertificationLanguages
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetByIDLearner,
            Add,
            Update,
            Delete,
            GetByName,
            UpdateLanguage,
            DeleteLanguage
        }

        private string _strCertificationId;
        public string CertificationId
        {
            get { return _strCertificationId; }
            set { if (this._strCertificationId != value) { _strCertificationId = value; } }
        }


        private List<CertificationActivity> _strCertificationActivitys;
        public List<CertificationActivity> CertificationActivity
        {
            get { return _strCertificationActivitys; }
            set { if (this._strCertificationActivitys != value) { _strCertificationActivitys = value; } }
        }       

        private string _strAdditionalLinks;
        public string AdditionalLinks
        {
            get { return _strAdditionalLinks; }
            set { if (this._strAdditionalLinks != value) { _strAdditionalLinks = value; } }
        }

        private string _strCertificationName;
        public string CertificationName
        {
            get { return _strCertificationName; }
            set { if (this._strCertificationName != value) { _strCertificationName = value; } }
        }


        private string _strCertificationDescription;
        public string CertificationDescription
        {
            get { return _strCertificationDescription; }
            set { if (this._strCertificationDescription != value) { _strCertificationDescription = value; } }
        }
        private string _strCertificationInstruction;
        public string CertificationInstruction
        {
            get { return _strCertificationInstruction; }
            set { if (this._strCertificationInstruction != value) { _strCertificationInstruction = value; } }
        }

        private bool _bIsReviewLocked;
        public bool IsReviewLocked
        {
            get { return _bIsReviewLocked; }
            set { if (this._bIsReviewLocked != value) { _bIsReviewLocked = value; } }
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

        private Nullable<bool> _bIsUsed;
        public Nullable<bool> IsUsed
        {
            get { return _bIsUsed; }
            set { if (this._bIsUsed != value) { _bIsUsed = value; } }
        }

        private Nullable<bool> _bIsActive;
        public Nullable<bool> IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
        }

      
        private string _strModifiedByName;
        public string ModifiedByName
        {
            get { return _strModifiedByName; }
            set { if (this._strModifiedByName != value) { _strModifiedByName = value; } }
        }
        private string _strLUClassificationId;
        public string LUClassificationId
        {
            get { return _strLUClassificationId; }
            set { if (this._strLUClassificationId != value) { _strLUClassificationId = value; } }
        }
        private string _strLUClassificationName;
        public string LUClassificationName
        {
            get { return _strLUClassificationName; }
            set { if (this._strLUClassificationName != value) { _strLUClassificationName = value; } }
        }
        private bool _bIsLUEnabled;
        public bool IsLUEnabled
        {
            get { return _bIsLUEnabled; }
            set { if (this._bIsLUEnabled != value) { _bIsLUEnabled = value; } }
        }
        private bool _bIsLULaunchInNewWindow;
        public bool IsLULaunchInNewWindow
        {
            get { return _bIsLULaunchInNewWindow; }
            set { if (this._bIsLULaunchInNewWindow != value) { _bIsLULaunchInNewWindow = value; } }
        }

        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }

        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }

        private string _LanguageId;
        public string LanguageId
        {
            get { return _LanguageId; }
            set { if (this._LanguageId != value) { _LanguageId = value; } }
        }

        private string _LanguageName;
        public string LanguageName
        {
            get { return _LanguageName; }
            set { if (this._LanguageName != value) { _LanguageName = value; } }
        }
    }
}