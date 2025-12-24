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
   [Serializable] public class UserAssessmentSessionResponses : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public UserAssessmentSessionResponses()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkUpdate
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

        private string _AssessmentOptionsID;
        public string AssessmentOptionsID
        {
            get { return _AssessmentOptionsID; }
            set { if (this._AssessmentOptionsID != value) { _AssessmentOptionsID = value; } }
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

        private string _explanationText;
        public string ExplanationText
        {
            get { return _explanationText; }
            set { if (this._explanationText != value) { _explanationText = value; } }
        }

        private System.DateTime _dateSubmitted;
        public System.DateTime DateSubmitted
        {
            get { return _dateSubmitted; }
            set { if (this._dateSubmitted != value) { _dateSubmitted = value; } }
        }

        private string _strReviewComments;
        /// <summary>
        /// Review Comments
        /// </summary>
        public string ReviewComments
        {
            get { return _strReviewComments; }
            set { if (this._strReviewComments != value) { _strReviewComments = value; } }
        }

        private bool _strIsForAdminPreview;
        /// <summary>
        /// Is For Admin Preview
        /// </summary>
        public bool IsForAdminPreview
        {
            get { return _strIsForAdminPreview; }
            set { if (this._strIsForAdminPreview != value) { _strIsForAdminPreview = value; } }
        }

        private AssessmentQuestion.AssessmentQuestionType _questionType;
        public AssessmentQuestion.AssessmentQuestionType QuestionType
        {
            get { return _questionType; }
            set { if (this._questionType != value) { _questionType = value; } }
        }

        private int _SeqNo;
        public int SeqNo
        {
            get { return _SeqNo; }
            set { if (this._SeqNo != value) { _SeqNo = value; } }
        }

    }
}