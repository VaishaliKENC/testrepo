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
   public class AssessmentOptions : AssessmentOptionsLanguage
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public AssessmentOptions()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            UpdateOptions,
            BulkImportInsert
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Add
        }

        public enum AssessmentOptionType
        {
            Correct = 1,
            Incorrect = 2
        }

        private string _AssessmentId;
        public string AssessmentId
        {
            get { return _AssessmentId; }
            set { if (this._AssessmentId != value) { _AssessmentId = value; } }
        }

        private string _sectionID;
        public string SectionID
        {
            get { return _sectionID; }
            set { if (this._sectionID != value) { _sectionID = value; } }
        }

        private string _questionID;
        public string QuestionID
        {
            get { return _questionID; }
            set { if (this._questionID != value) { _questionID = value; } }
        }

        private bool _isExplanation;
        public bool IsExplanation
        {
            get { return _isExplanation; }
            set { if (this._isExplanation != value) { _isExplanation = value; } }
        }

        private string _goToQuestion;
        public string GoToQuestion
        {
            get { return _goToQuestion; }
            set { if (this._goToQuestion != value) { _goToQuestion = value; } }
        }

        private AssessmentOptionType _optionType;
        public AssessmentOptionType OptionType
        {
            get { return _optionType; }
            set { if (this._optionType != value) { _optionType = value; } }
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

        private bool _isAlert;
        public bool IsAlert
        {
            get { return _isAlert; }
            set { if (this._isAlert != value) { _isAlert = value; } }
        }

        private bool _isLaunchLU;
        public bool IsLaunchLU
        {
            get { return _isLaunchLU; }
            set { if (this._isLaunchLU != value) { _isLaunchLU = value; } }
        }
    }
}