/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:<Fattesinh Pisal>
* Created:<25/09/09>
* Last Modified:<dd/mm/yy>
*/
using System.Collections.Generic;
using System;
namespace YPLMS2._0.API.Entity
{
   [Serializable] public class UserAssessmentTracking : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public UserAssessmentTracking()
        {
            _userSessionResponse = new List<UserAssessmentSessionResponses>();
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            MarkCompleted,
            BulkMarkCompleted
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetQuesTrackStatus,
            Update,
            Create,
            UpdateScannedFileName,
            AssessmentTrackingForAdminPreviewDelete,
            GetAssessmentTrackingSummary,
        }

        private List<UserAssessmentSessionResponses> _userSessionResponse;
        /// <summary>
        /// Session response.
        /// </summary>
        public List<UserAssessmentSessionResponses> UserSessionResponse
        {
            get { return _userSessionResponse; }
        }

        private string _AssessmentId;
        /// <summary>
        /// Assessment Id
        /// </summary>
        public string AssessmentId
        {
            get { return _AssessmentId; }
            set { if (this._AssessmentId != value) { _AssessmentId = value; } }
        }

        private string _systemUserGUID;
        /// <summary>
        /// User/Learner Id
        /// </summary>
        public string SystemUserGUID
        {
            get { return _systemUserGUID; }
            set { if (this._systemUserGUID != value) { _systemUserGUID = value; } }
        }

        private ActivityCompletionStatus _submissionStatus;
        /// <summary>
        /// Completion status
        /// </summary>
        public ActivityCompletionStatus SubmissionStatus
        {
            get { return _submissionStatus; }
            set { if (this._submissionStatus != value) { _submissionStatus = value; } }
        }

        private string _DisplaysubmissionStatus;
        /// <summary>
        /// Completion status
        /// </summary>
        public string DisplaySubmissionStatus
        {
            get { return _DisplaysubmissionStatus; }
            set { if (this._DisplaysubmissionStatus != value) { _DisplaysubmissionStatus = value; } }
        }

        private System.DateTime _startDate;
        /// <summary>
        /// Start Date
        /// </summary>
        public System.DateTime StartDate
        {
            get { return _startDate; }
            set { if (this._startDate != value) { _startDate = value; } }
        }

        private System.DateTime _completatedDate;
        /// <summary>
        /// Assessment completion date
        /// </summary>
        public System.DateTime CompletatedDate
        {
            get { return _completatedDate; }
            set { if (this._completatedDate != value) { _completatedDate = value; } }
        }

        private string _attemptLanguageId;
        /// <summary>
        /// Attempted Language id
        /// </summary>
        public string AttemptLanguageId
        {
            get { return _attemptLanguageId; }
            set { if (this._attemptLanguageId != value) { _attemptLanguageId = value; } }
        }

        private string _strMarkedCompletedById;
        /// <summary>
        /// Marked Completed By Id
        /// </summary>
        public string MarkedCompletedById
        {
            get { return _strMarkedCompletedById; }
            set { if (this._strMarkedCompletedById != value) { _strMarkedCompletedById = value; } }
        }

        private string _strScannedCertificationFileName;
        /// <summary>
        /// Scanned Certification File Name
        /// </summary>
        public string ScannedCertificationFileName
        {
            get { return _strScannedCertificationFileName; }
            set { if (this._strScannedCertificationFileName != value) { _strScannedCertificationFileName = value; } }
        }

        private string _strActivityName;
        /// <summary>
        /// Activity Name : Read only
        /// </summary>
        public string ActivityName
        {
            get { return _strActivityName; }
            set { if (this._strActivityName != value) { _strActivityName = value; } }
        }

        private string _strUserFirstLastName;
        /// <summary>
        /// User First and Last Name : Read only
        /// </summary>
        public string UserFirstLastName
        {
            set { if (this._strUserFirstLastName != value) { _strUserFirstLastName = value; } }
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

        private int _strTotalQuestionCount;
        /// <summary>
        /// TotalQuestionCount
        /// </summary>
        public int TotalQuestionCount
        {
            get { return _strTotalQuestionCount; }
            set { _strTotalQuestionCount = value; }
        }

        private int _strCorrectQuestionCount;
        /// <summary>
        /// CorrectQuestionCount
        /// </summary>
        public int CorrectQuestionCount
        {
            get { return _strCorrectQuestionCount; }
            set { _strCorrectQuestionCount = value; }
        }

        private int _strInCorrectQuestionCount;
        /// <summary>
        /// InCorrectQuestionCount
        /// </summary>
        public int InCorrectQuestionCount
        {
            get { return _strInCorrectQuestionCount; }
            set { _strInCorrectQuestionCount = value; }
        }

        private string _strScore;
        /// <summary>
        /// Review Score
        /// </summary>
        public string Score
        {
            get { return _strScore; }
            set { _strScore = value; }
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

        private bool _strIsBulkMarkCompleted;
        /// <summary>
        /// Is Bulk Mark Completed
        /// </summary>
        public bool IsBulkMarkCompleted
        {
            get { return _strIsBulkMarkCompleted; }
            set { if (this._strIsBulkMarkCompleted != value) { _strIsBulkMarkCompleted = value; } }
        }
    }
}
