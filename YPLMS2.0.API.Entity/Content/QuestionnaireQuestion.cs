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
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
     public class QuestionnaireQuestion : QuestionnaireQuestionLanguage
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public QuestionnaireQuestion()
        {
            _questionOptions = new List<QuestionOptions>();
        }

        public new enum ListMethod
        {
            GetAll,
            GetQuestionLanguages
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Add,
            Update,
            Delete,
            SequenceUpdate,
            UpdateLanguage,
            DeleteLanguage,
            GetSectionIdWise
        }
        private List<QuestionOptions> _questionOptions;
        /// <summary>
        /// Option List
        /// </summary>
        public List<QuestionOptions> QuestionOptions
        {
            get { return _questionOptions; }
        }

        public enum QuestionnaireQuestionType
        {
            MCQ=1,
            MRQ = 2, 
            FreeText=3
        }

        private string _questionnaireID;
        public string QuestionnaireID
        {
            get { return _questionnaireID; }
            set { if (this._questionnaireID != value) { _questionnaireID = value; } }
        }

        private string _sectionID;
        public string SectionID
        {
            get { return _sectionID; }
            set { if (this._sectionID != value) { _sectionID = value; } }
        }

        private QuestionnaireQuestionType _questionType;
        public QuestionnaireQuestionType QuestionType
        {
            get { return _questionType; }
            set { if (this._questionType != value) { _questionType = value; } }
        }

        private int _sequenceOrder;
        public int SequenceOrder
        {
            get { return _sequenceOrder; }
            set { if (this._sequenceOrder != value) { _sequenceOrder = value; } }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
        }

        private bool _isMoveUp;
        public bool IsMoveUp
        {
            get { return _isMoveUp; }
            set { if (this._isMoveUp != value) { _isMoveUp = value; } }
        }

        private bool _isReviewAlert;
        public bool IsReviewAlert
        {
            get { return _isReviewAlert; }
            set { if (this._isReviewAlert != value) { _isReviewAlert = value; } }
        }

        private string _LanguageName;
        public string LanguageName
        {
            get { return _LanguageName; }
            set { if (this._LanguageName != value) { _LanguageName = value; } }
        }

        private string _strCreatedName;
        /// <summary>
        /// Created by Name
        /// </summary>
        public string CreatedName
        {
            get { return _strCreatedName; }
            set { if (this._strCreatedName != value) { _strCreatedName = value; } }
        }

        private string _strModifiedByName;
        public string ModifiedByName
        {
            get { return _strModifiedByName; }
            set { if (this._strModifiedByName != value) { _strModifiedByName = value; } }
        }
        
    }
}