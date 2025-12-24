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
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    ///  class Lookup:BaseEntity 
    /// </summary>
    [Serializable]
     public class Lookup : BaseEntity
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Lookup()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAllByLookupType,
            GetStatusAndTypeByLookupType
        }

        private string _strLookupText;
        /// <summary>
        /// LookupText
        /// </summary>
        public string LookupText
        {
            get { return _strLookupText; }
            set { if (this._strLookupText != value) { _strLookupText = value; } }
        }

        private string _strLookupValue;
        /// <summary>
        ///  LookupValue
        /// </summary>
        public string LookupValue
        {
            get { return _strLookupValue; }
            set { if (this._strLookupValue != value) { _strLookupValue = value; } }
        }

        private string _strLookupTypeId;
        /// <summary>
        /// Lookup Type Id
        /// </summary>
        public string LookupTypeId
        {
            get { return _strLookupTypeId; }
            set { if (this._strLookupTypeId != value) { _strLookupTypeId = value; } }
        }

        private LookupType _entLookupType;
        /// <summary>
        /// Lookup Type
        /// </summary>
        public LookupType LookupType
        {
            get { return _entLookupType; }
            set { if (this._entLookupType != value) { _entLookupType = value; } }
        }

        private string _strLanguageId;
        /// <summary>
        /// Language Id
        /// </summary>
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }

        private bool _bIsDefault;
        /// <summary>
        /// To check Is Default
        /// </summary>
        public bool IsDefault
        {
            get { return _bIsDefault; }
            set { if (this._bIsDefault != value) { _bIsDefault = value; } }
        }

        private bool _bIsCurriculum=false;
        /// <summary>
        /// To check Is Default
        /// </summary>
        public bool IsCurriculum
        {
            get { return _bIsCurriculum; }
            set { if (this._bIsCurriculum != value) { _bIsCurriculum = value; } }
        }
        public bool Validate(bool pIsUpdate)
        {
            if (pIsUpdate)
            {
                if (String.IsNullOrEmpty(ID))
                    return false;
            }
            else
            {
                if (String.IsNullOrEmpty(LookupText))
                    return false;

                if (String.IsNullOrEmpty(LookupValue))
                    return false;

                if (String.IsNullOrEmpty(LookupTypeId))
                    return false;

                if (String.IsNullOrEmpty(LanguageId))
                    return false;

                if (String.IsNullOrEmpty(CreatedById))
                    return false;
            }
            if (String.IsNullOrEmpty(LastModifiedById))
                return false;
            return true;
        }
    }

    /// <summary>
    /// enum LookupType
    /// </summary>
    public enum LookupType
    {
        StandardType,
        CourseType,
        ActivityType,
        ActivityCompletionStatus,
        MarkCompletionStatus,
        Gender,
        IsActive,
        UserType,
        CompletionCondition,
        PolicyFileTypes,
        AssetFileTypes,
        CertificationActivity,
        RuleBetweenCondition,
        RuleConditionAlphanumaric,
        RuleConditionDate,
        RuleConditionNumeric,
        RuleParameterConditionControlType,
        QuestionnaireOptionType,
        QuestionnaireQuestionType,
        QuestionnaireDisplayType,
        QuestionnaireSubmitType,
        QuestionnaireLogoPosition,
        ScheduleTaskType,
        SingleSignOnType,
        EmailType,
        AssignmentMode,
        Months,
        Days,
        Years,
        CustomReportSortDirection,
        ActivityAssignment,
        Color,
        DateFormats,
        RelativeCondition,
        AssessmentOptionType,
        AssessmentQuestionType,
        AssessmentDisplayType,
        AssessmentSubmitType,
        AssessmentLogoPosition,
        SpeakerType,
        TrainingSessionStatus,
        ViewResources,
        SelfNomination,
        QBQuestionType,
        QBOptionType,
        DocumentFileTypes,
        MessageType,
        IsRead,
        IsSelfNominated,
        LearnerType,
        TaxCode,
        ZoomTimeZone,
        IssueType
    }
    }
