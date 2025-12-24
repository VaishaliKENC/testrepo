/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:
* Created:
* Last Modified:
*/

using System;
using System.Collections.Generic;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
   public class Questionnaire : QuestionnaireLanguage
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public Questionnaire()
        {
            _QuestionnaireSections = new List<QuestionnaireSections>();
            _userAttemptTracking = new List<UserQuestionnaireTracking>();
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetQuestionnaireLanguages,
            GetQuestionnaire,
            GetQuestionnaireTracking,
            GetImportLanguages,
            GetExportLanguages,
            DeleteQuestionnaireList,
            ApproveQuestionnaireList,
            ActivateDeActivateStatus,
            GetQuestionnaireForAssignment,
            GetQuestionnaireTrackingWithOutPaging
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetQuesType,
            CheckQuestionnaireByName,
            Add,
            Update,
            CopyQuestionnaire,
            ImportQuestionnaire,
            CopyImportQuestionnaireLanguages,
            GetDefaultSequence,
            UpdateDefaultSequence,
            GetShowQuestionNumber,
            UpdateLanguage,
            DeleteLanguage
        }

        /// <summary>
        /// For Player
        /// </summary>
        public enum QuestionnaireDisplayType
        {
            ALL,
            Section
        }

        public enum QuestionnaireSubmitType
        {
            Named,
            Ananymous
        }

        public enum QuestionnaireLogoPosition
        {
            AlignTopLeft,
            AlignTopCenter,
            AlignTopRight,
            AlignBottomLeft,
            AlignBottomCenter,
            AlignBottomRight
        }

        public enum QuestionnaireApprovalStatus
        {
            Draft,
            Approved,
            SentForApproval
        }

        private List<UserQuestionnaireTracking> _userAttemptTracking;
        /// <summary>
        /// 
        /// </summary>
        public List<UserQuestionnaireTracking> UserAttemptTracking
        {
            get { return _userAttemptTracking; }
            set { if (this._userAttemptTracking != value) { _userAttemptTracking = value; } }
        }

        private QuestionnaireDisplayType _QuestionnaireType;
        /// <summary>
        /// To display Sections
        /// </summary>
        public QuestionnaireDisplayType QuestionnaireType
        {
            get { return _QuestionnaireType; }
            set { if (this._QuestionnaireType != value) { _QuestionnaireType = value; } }
        }

        private Nullable<bool> _isActive;
        public Nullable<bool> IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
        }

        private string _bGColor;
        public string BGColor
        {
            get { return _bGColor; }
            set { if (this._bGColor != value) { _bGColor = value; } }
        }

        private string _defaultLogoPath;
        public string DefaultLogoPath
        {
            get { return _defaultLogoPath; }
            set { if (this._defaultLogoPath != value) { _defaultLogoPath = value; } }
        }

        private bool _isLogoOnAll;
        public bool IsLogoOnAll
        {
            get { return _isLogoOnAll; }
            set { if (this._isLogoOnAll != value) { _isLogoOnAll = value; } }
        }


        private bool _isDefaultSequence;
        public bool IsDefaultSequence
        {
            get { return _isDefaultSequence; }
            set { if (this._isDefaultSequence != value) { _isDefaultSequence = value; } }
        }


        private bool _isShowQuestionNumber;
        public bool IsShowQuestionNumber
        {
            get { return _isShowQuestionNumber; }
            set { if (this._isShowQuestionNumber != value) { _isShowQuestionNumber = value; } }
        }

        

        private bool _isPartialSubmitAllowed;
        public bool IsPartialSubmitAllowed
        {
            get { return _isPartialSubmitAllowed; }
            set { if (this._isPartialSubmitAllowed != value) { _isPartialSubmitAllowed = value; } }
        }

        private bool _isMultiSubmitAllowed;
        public bool IsMultiSubmitAllowed
        {
            get { return _isMultiSubmitAllowed; }
            set { if (this._isMultiSubmitAllowed != value) { _isMultiSubmitAllowed = value; } }
        }

        private bool _isPrintCertificate;
        public bool IsPrintCertificate
        {
            get { return _isPrintCertificate; }
            set { if (this._isPrintCertificate != value) { _isPrintCertificate = value; } }
        }

        private bool _isReviewAnswer;
        public bool IsReviewAnswer
        {
            get { return _isReviewAnswer; }
            set { if (this._isReviewAnswer != value) { _isReviewAnswer = value; } }
        }

        private bool _allowUserLangSelection;
        public bool AllowUserLangSelection
        {
            get { return _allowUserLangSelection; }
            set { if (this._allowUserLangSelection != value) { _allowUserLangSelection = value; } }
        }

        private bool _bAllowSequencing;
        public bool AllowSequencing
        {
            get { return _bAllowSequencing; }
            set { if (this._bAllowSequencing != value) { _bAllowSequencing = value; } }
        }

      

        private string _strModifiedByName;
        public string ModifiedByName
        {
            get { return _strModifiedByName; }
            set { if (this._strModifiedByName != value) { _strModifiedByName = value; } }
        }

        private string _strParaSectionId;
        public string ParaSectionId
        {
            get { return _strParaSectionId; }
            set { if (this._strParaSectionId != value) { _strParaSectionId = value; } }
        }

        private string _strParaQuestionId;
        public string ParaQuestionId
        {
            get { return _strParaQuestionId; }
            set { if (this._strParaQuestionId != value) { _strParaQuestionId = value; } }
        }

        private string _strParaOptionId;
        public string ParaOptionId
        {
            get { return _strParaOptionId; }
            set { if (this._strParaOptionId != value) { _strParaOptionId = value; } }
        }

        private string _strBaseLanguageId;
        /// <summary>
        /// For Import Base LanguageId
        /// </summary>
        public string BaseLanguageId
        {
            get { return _strBaseLanguageId; }
            set { if (this._strBaseLanguageId != value) { _strBaseLanguageId = value; } }
        }

        private bool _isLocked;
        public bool IsLocked
        {
            get { return _isLocked; }
            set { if (this._isLocked != value) { _isLocked = value; } }
        }

        private QuestionnaireLogoPosition _enumLogoPosition;
        /// <summary>
        /// Logo Position
        /// </summary>
        public QuestionnaireLogoPosition LogoPosition
        {
            get { return _enumLogoPosition; }
            set { if (this._enumLogoPosition != value) { _enumLogoPosition = value; } }
        }

        private QuestionnaireSubmitType _enumTrackingType;
        /// <summary>
        /// Submit Type
        /// </summary>
        public QuestionnaireSubmitType TrackingType
        {
            get { return _enumTrackingType; }
            set { if (this._enumTrackingType != value) { _enumTrackingType = value; } }
        }

        private int _iQuestionCount;
        public int QuestionCount
        {
            get
            {
                _iQuestionCount = 0;
                foreach (QuestionnaireSections objSection in this.Sections)
                {
                    _iQuestionCount += objSection.QuestionnaireQuestion.Count;
                }
                return _iQuestionCount;
            }
        }


        private int _iParaQuestionCount;
        public int ParaQuestionCount
        {
            get { return _iParaQuestionCount; }
            set { if (this._iParaQuestionCount != value) { _iParaQuestionCount = value; } }
        }

        private List<QuestionnaireQuestion> _entListQuestions;
        public List<QuestionnaireQuestion> QuestionList
        {
            get
            {
                _entListQuestions = new List<QuestionnaireQuestion>();
                foreach (QuestionnaireSections objSection in this.Sections)
                {
                    _entListQuestions.AddRange(objSection.QuestionnaireQuestion);
                }
                return _entListQuestions;
            }
        }

        private List<QuestionnaireSections> _QuestionnaireSections;
        /// <summary>
        /// Questionnaire Sections
        /// </summary>
        public List<QuestionnaireSections> Sections
        {
            get { return _QuestionnaireSections; }
        }

        //default value added.
        private int _iMaxResponseLength = 500;
        public int MaxResponseLength
        {
            get { return _iMaxResponseLength; }
            set { if (this._iMaxResponseLength != value) { _iMaxResponseLength = value; } }
        }


        private bool _isUseSections;
        public bool IsUseSections
        {
            get { return _isUseSections; }
            set { if (this._isUseSections != value) { _isUseSections = value; } }
        }


        private string _bGColorHF;
        /// <summary>
        /// BG color Hearder and Footer
        /// </summary>
        public string BGColorHF
        {
            get { return _bGColorHF; }
            set { if (this._bGColorHF != value) { _bGColorHF = value; } }
        }

        private bool _isLogoHide;
        public bool IsLogoHide
        {
            get { return _isLogoHide; }
            set { if (this._isLogoHide != value) { _isLogoHide = value; } }
        }
        private bool _isLogoOnFirstPageOnly;
        public bool IsLogoOnFirstPageOnly
        {
            get { return _isLogoOnFirstPageOnly; }
            set { if (this._isLogoOnFirstPageOnly != value) { _isLogoOnFirstPageOnly = value; } }
        }


        private bool _isAllAnswerMust;
        public bool IsAllAnswerMust
        {
            get { return _isAllAnswerMust; }
            set { if (this._isAllAnswerMust != value) { _isAllAnswerMust = value; } }
        }

        private string _fontName;
        public string FontName
        {
            get { return _fontName; }
            set { if (this._fontName != value) { _fontName = value; } }
        }

        private int _fontSize;
        public int FontSize
        {
            get { return _fontSize; }
            set { if (this._fontSize != value) { _fontSize = value; } }
        }


        private string _fontColor;
        public string FontColor
        {
            get { return _fontColor; }
            set { if (this._fontColor != value) { _fontColor = value; } }
        }

        private string _questionaireBGColor;
        public string QuestionaireBGColor
        {
            get { return _questionaireBGColor; }
            set { if (this._questionaireBGColor != value) { _questionaireBGColor = value; } }
        }

        private bool _IsLearnerPrintable;
        public bool IsLearnerPrintable
        {
            get { return _IsLearnerPrintable; }
            set { if (this._IsLearnerPrintable != value) { _IsLearnerPrintable = value; } }
        }

        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }

        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }

        private bool _IsQuestionnaireForSurvey;
        public bool IsQuestionnaireForSurvey
        {
            get { return _IsQuestionnaireForSurvey; }
            set { if (this._IsQuestionnaireForSurvey != value) { _IsQuestionnaireForSurvey = value; } }
        }

    }
}