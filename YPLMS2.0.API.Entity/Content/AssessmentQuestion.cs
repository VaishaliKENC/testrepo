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
    public class AssessmentQuestion : AssessmentQuestionLanguage
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public AssessmentQuestion()
        {
            _assessmentOptions = new List<AssessmentOptions>();
        }

        public new enum ListMethod
        {
            GetAll
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Add,
            Update,
            Delete,
            SequenceUpdate
        }
        private List<AssessmentOptions> _assessmentOptions;
        /// <summary>
        /// Option List
        /// </summary>
        public List<AssessmentOptions> AssessmentOptions
        {
            get { return _assessmentOptions; }
        }

        public enum AssessmentQuestionType
        {
            MCQ = 1,
            MRQ = 2
        }

        private string _AssessmentID;
        public string AssessmentID
        {
            get { return _AssessmentID; }
            set { if (this._AssessmentID != value) { _AssessmentID = value; } }
        }

        private string _sectionID;
        public string SectionID
        {
            get { return _sectionID; }
            set { if (this._sectionID != value) { _sectionID = value; } }
        }

        private AssessmentQuestionType _questionType;
        public AssessmentQuestionType QuestionType
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
        private bool _IsMappingActive;
        public bool IsMappingActive
        {
            get { return _IsMappingActive; }
            set { if (this._IsMappingActive != value) { _IsMappingActive = value; } }
        }
    }
}