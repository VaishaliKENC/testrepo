/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:<Fattesinh pisal>
* Created:<14/09/09>
* Last Modified:<25/09/09>
*/

using System;
using System.Collections.Generic;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
   public class AssessmentDates : AssessmentLanguage
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public AssessmentDates()
        {
            _AssessmentSections = new List<AssessmentSections>();
            _userAttemptTracking = new List<UserAssessmentTracking>();
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAssessmentLanguages,
            GetAssessment,
            GetAssessmentTracking,
            GetImportLanguages,
            GetExportLanguages,
            DeleteAssessmentList,
            ApproveAssessmentList,
            ActivateDeActivateStatus,
            GetAssessmentForAssignment,
            GetAssessmentTrackingWithOutPaging,          
            GetAssessmentTrackingPreviewAssessment
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetQuesType,
            CheckAssessmentByName,
            Add,
            Update,
            CopyAssessment,
            ImportAssessment,
            CopyImportAssessmentLanguages,
            GetDefaultSequence,
            UpdateDefaultSequence,
            GetShowQuestionNumber,
            UpdateLanguage,
            DeleteLanguage,
            GetAssessmentResult
        }

        /// <summary>
        /// For Player
        /// </summary>
        public enum AssessmentDisplayType
        {
            ALL,
            Section
        }

        public enum AssessmentSubmitType
        {
            Named,
            Ananymous
        }

        public enum AssessmentLogoPosition
        {
            AlignTopLeft,
            AlignTopCenter,
            AlignTopRight,
            AlignBottomLeft,
            AlignBottomCenter,
            AlignBottomRight
        }

        public enum AssessmentApprovalStatus
        {
            Draft,
            Approved,
            SentForApproval
        }

        /// <summary>
        /// For Player
        /// </summary>
        public enum ShowAssessmentResultType
        {
            Null,
            OnLastAttempt,
            OnEveryAttempt,
            Never,
            OnAssessmentCompletion,
            NeverWithoutScore
        }

        private List<UserAssessmentTracking> _userAttemptTracking;
        /// <summary>
        /// 
        /// </summary>
        public List<UserAssessmentTracking> UserAttemptTracking
        {
            get { return _userAttemptTracking; }
            set { if (this._userAttemptTracking != value) { _userAttemptTracking = value; } }
        }

        private AssessmentDisplayType _AssessmentType;
        /// <summary>
        /// To display Sections
        /// </summary>
        public AssessmentDisplayType AssessmentType
        {
            get { return _AssessmentType; }
            set { if (this._AssessmentType != value) { _AssessmentType = value; } }
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
        //Add Bharat- 18-Nov-2013
        private string _strFeatureCreatedById;
        public string FeatureCreatedById
        {
            get { return _strFeatureCreatedById; }
            set { if (this._strFeatureCreatedById != value) { _strFeatureCreatedById = value; } }
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
     
        private string _AssessmentTime;
        public string AssessmentTime
        {
            get
            {
                return _AssessmentTime;
            }
            set
            {
                _AssessmentTime = value;
            }
        }

        private string _AssessmentAlertTime;
        public string AssessmentAlertTime
        {
            get
            {
                return _AssessmentAlertTime;
            }
            set
            {
                _AssessmentAlertTime = value;
            }
        }

        private string _AssessmentNumberOfAttempts;
        public string AssessmentNumberOfAttempts
        {
            get
            {
                return _AssessmentNumberOfAttempts;
            }
            set
            {
                _AssessmentNumberOfAttempts = value;
            }
        }



        private AssessmentLogoPosition _enumLogoPosition;
        /// <summary>
        /// Logo Position
        /// </summary>
        public AssessmentLogoPosition LogoPosition
        {
            get { return _enumLogoPosition; }
            set { if (this._enumLogoPosition != value) { _enumLogoPosition = value; } }
        }

        private AssessmentSubmitType _enumTrackingType;
        /// <summary>
        /// Submit Type
        /// </summary>
        public AssessmentSubmitType TrackingType
        {
            get { return _enumTrackingType; }
            set { if (this._enumTrackingType != value) { _enumTrackingType = value; } }
        }

        ////private int _iQuestionCount;
        ////public int QuestionCount
        ////{
        ////    get
        ////    {
        ////        _iQuestionCount = 0;
        ////        foreach (AssessmentSections objSection in this.Sections)
        ////        {
        ////            _iQuestionCount += objSection.AssessmentQuestion.Count;
        ////        }
        ////        return _iQuestionCount;
        ////    }
        ////}


        private int _iParaQuestionCount;
        public int ParaQuestionCount
        {
            get { return _iParaQuestionCount; }
            set { if (this._iParaQuestionCount != value) { _iParaQuestionCount = value; } }
        }

        private int _iMappingQuestionCount;
        public int MappingQuestionCount
        {
            get { return _iMappingQuestionCount; }
            set { if (this._iMappingQuestionCount != value) { _iMappingQuestionCount = value; } }
        }

        private bool _IsSendEmailOfResult;
        public bool IsSendEmailOfResult
        {
            get { return _IsSendEmailOfResult; }
            set { if (this._IsSendEmailOfResult != value) { _IsSendEmailOfResult = value; } }
        }

        private bool _IsCompleteAssignmentDepOnScrore;
        public bool IsCompleteAssignmentDepOnScrore
        {
            get { return _IsCompleteAssignmentDepOnScrore; }
            set { if (this._IsCompleteAssignmentDepOnScrore != value) { _IsCompleteAssignmentDepOnScrore = value; } }
        }

        private double  _PassingScore;
        public double PassingScore
        {
            get { return _PassingScore; }
            set { if (this._PassingScore != value) { _PassingScore = value; } }
        }
        private List<AssessmentQuestion> _entListQuestions;
        public List<AssessmentQuestion> QuestionList
        {
            get
            {
                _entListQuestions = new List<AssessmentQuestion>();
                foreach (AssessmentSections objSection in this.Sections)
                {
                    _entListQuestions.AddRange(objSection.AssessmentQuestion);
                }
                return _entListQuestions;
            }
        }

        private List<AssessmentSections> _AssessmentSections;
        /// <summary>
        /// Assessment Sections
        /// </summary>
        public List<AssessmentSections> Sections
        {
            get { return _AssessmentSections; }
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

         private bool _isAssessmentAttempExceded;
        public bool IsAssessmentAttempExceded
        {
            get { return _isAssessmentAttempExceded; }
            set { if (this._isAssessmentAttempExceded != value) { _isAssessmentAttempExceded = value; } }
        }

        private ShowAssessmentResultType _strShowAssessmentResult;
        public ShowAssessmentResultType ShowAssessmentResult
        {
            get { return _strShowAssessmentResult; }
            set { if (this._strShowAssessmentResult != value) { _strShowAssessmentResult = value; } }
        }

        private int _QuestionCount;
        public int QuestionCount
        {
            get { return _QuestionCount; }
            set { if (this._QuestionCount != value) { _QuestionCount = value; } }
        }

        private bool _isIncorrectQuestions;
        public bool IsIncorrectQuestions
        {
            get { return _isIncorrectQuestions; }
            set { if (this._isIncorrectQuestions != value) { _isIncorrectQuestions = value; } }
        }

        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }

        private int? _iSendEmailToAdminUser;
        public int? SendEmailToAdminUser
        {
            get { return _iSendEmailToAdminUser; }
            set { if (this._iSendEmailToAdminUser != value) { _iSendEmailToAdminUser = value; } }
        }



        private string _strSendEmailTo;
        public string SendEmailTo
        {
            get { return _strSendEmailTo; }
            set { if (this._strSendEmailTo != value) { _strSendEmailTo = value; } }
        }
        private bool _AllowOptionRandamization;
        public bool AllowOptionRandamization
        {
            get { return _AllowOptionRandamization; }
            set { if (this._AllowOptionRandamization != value) { _AllowOptionRandamization = value; } }
        }
        private bool _IsBookmarking;
        public bool IsBookmarking
        {
            get { return _IsBookmarking; }
            set { if (this._IsBookmarking != value) { _IsBookmarking = value; } }
        }

        private string _strIsBookmarking;
        public string strIsBookmarking
        {
            get { return _strIsBookmarking; }
            set { if (this._strIsBookmarking != value) { _strIsBookmarking = value; } }
        }        
    }
}