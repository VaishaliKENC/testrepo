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
    public class QuestionnaireSections : QuestionnaireSectionsLanguage
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public QuestionnaireSections()
        {
            _questionnaireQuestion = new List<QuestionnaireQuestion>(); 
        }

        public new enum ListMethod
        {
            GetAll,
            GetSectionLanguages,
            GetImportLanguages
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
            GetQuestionnaireIdWise
        }
        private List<QuestionnaireQuestion> _questionnaireQuestion;
        /// <summary>
        /// Option List
        /// </summary>
        public List<QuestionnaireQuestion> QuestionnaireQuestion
        {
            get { return _questionnaireQuestion; }
        }      
 
        private string _questionnaireId;
        public string QuestionnaireId
        {
            get { return _questionnaireId; }
            set { if (this._questionnaireId != value) { _questionnaireId = value; } }
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