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
   [Serializable] public class CurriculumActivity : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public CurriculumActivity()
        { }
        public new enum Method
        {
            Get
        }
        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllAttempt,
            GetAll_Curriculum
        }

        private string _strCurriculumId;
        public string CurriculumId
        {
            get { return _strCurriculumId; }
            set { if (this._strCurriculumId != value) { _strCurriculumId = value; } }
        }

        private string _strActivityId;
        public string ActivityId
        {
            get { return _strActivityId; }
            set { if (this._strActivityId != value) { _strActivityId = value; } }
        }

        private string _strLanguageId;
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }

        private ActivityCompletionStatus _strStatus;
        public ActivityCompletionStatus Status
        {
            get { return _strStatus; }
            set { if (this._strStatus != value) { _strStatus = value; } }
        }

        private string _strActivityName;
        public string ActivityName
        {
            get { return _strActivityName; }
            set { if (this._strActivityName != value) { _strActivityName = value; } }
        }

        private string _strActivityDesc;
        public string ActivityDescription
        {
            get { return _strActivityDesc; }
            set { if (this._strActivityDesc != value) { _strActivityDesc = value; } }
        }
       
        private string _strOrignalActivityName;
        public string OrignalActivityName
        {
            get { return _strOrignalActivityName; }
            set { if (this._strOrignalActivityName != value) { _strOrignalActivityName = value; } }
        }

        private string _strActivityMessage;
        public string ActivityMessage
        {
            get { return _strActivityMessage; }
            set { if (this._strActivityMessage != value) { _strActivityMessage = value; } }
        }

        private string _strActivityCompletionConditionId;
        public string ActivityCompletionConditionId
        {
            get { return _strActivityCompletionConditionId; }
            set { if (this._strActivityCompletionConditionId != value) { _strActivityCompletionConditionId = value; } }
        }

        private ActivityContentType _strActivityType;
        public ActivityContentType ActivityType
        {
            get { return _strActivityType; }
            set { if (this._strActivityType != value) { _strActivityType = value; } }
        }

        private string _strActivityStatus;
        public string ActivityStatus
        {
            get { return _strActivityStatus; }
            set { if (this._strActivityStatus != value) { _strActivityStatus = value; } }
        }

        private string _strSystemUserGUID;
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private int _iSortOrder;
        public int SortOrder
        {
            get { return _iSortOrder; }
            set { if (this._iSortOrder != value) { _iSortOrder = value; } }
        }

        private bool _bIsPrintCertificate;
        public bool IsPrintCertificate
        {
            get { return _bIsPrintCertificate; }
            set { if (this._bIsPrintCertificate != value) { _bIsPrintCertificate = value; } }
        }

        private int _iAttemptCount;
        public int AttemptCount
        {
            get { return _iAttemptCount; }
            set { if (this._iAttemptCount != value) { _iAttemptCount = value; } }
        }

        public string SectionID { get; set; }
        public string CompletionStatusForBookMark { get; set; }
        //public string SectionName { get; set; }

        private string _strBookmarkId;
        public string BookmarkId
        {
            get { return _strBookmarkId; }
            set { if (this._strBookmarkId != value) { _strBookmarkId = value; } }
        }
    }
}