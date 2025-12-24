/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Charu Singh
* Created:<17/09/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
   [Serializable] public class CertificationActivity : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public CertificationActivity()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllByAttempt
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            GetCertQuestSatus,
        }

        private string _strSystemUserGUID;
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private string _strCertificationId;
        public string CertificationId
        {
            get { return _strCertificationId; }
            set { if (this._strCertificationId != value) { _strCertificationId = value; } }
        }

        private string _strCertificationActivityId;
        public string CertificationActivityId
        {
            get { return _strCertificationActivityId; }
            set { if (this._strCertificationActivityId != value) { _strCertificationActivityId = value; } }
        }

        private string _strActivityName;
        public string ActivityName
        {
            get { return _strActivityName; }
            set { if (this._strActivityName != value) { _strActivityName = value; } }
        }

        private string _strActivityDescription;
        public string ActivityDescription
        {
            get { return _strActivityDescription; }
            set { if (this._strActivityDescription != value) { _strActivityDescription = value; } }
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

        private int _iDisplayOrder;
        public int DisplayOrder
        {
            get { return _iDisplayOrder; }
            set { if (this._iDisplayOrder != value) { _iDisplayOrder = value; } }
        }

        private bool _bIsPrintCertificate;
        public bool IsPrintCertificate
        {
            get { return _bIsPrintCertificate; }
            set { if (this._bIsPrintCertificate != value) { _bIsPrintCertificate = value; } }
        }

        private bool _bIsQuestionnaireActive;
        /// <summary>
        /// Computed Column - Only required in get method
        /// </summary>
        public bool IsQuestionnaireActive
        {
            get { return _bIsQuestionnaireActive; }
            set { if (this._bIsQuestionnaireActive != value) { _bIsQuestionnaireActive = value; } }
        }

        private string _strParaLanguageId;
        /// <summary>
        /// Language Id for Parameter
        /// </summary>
        public string ParaLanguageId
        {
            get { return _strParaLanguageId; }
            set { if (this._strParaLanguageId != value) { _strParaLanguageId = value; } }
        }

        private ActivityCompletionStatus _strStatus;
        public ActivityCompletionStatus Status
        {
            get { return _strStatus; }
            set { if (this._strStatus != value) { _strStatus = value; } }
        }


        private string _strParaAttemptId;
        /// <summary>
        /// Attempt Id for Parameter
        /// </summary>
        public string ParaAttemptId
        {
            get { return _strParaAttemptId; }
            set { if (this._strParaAttemptId != value) { _strParaAttemptId = value; } }
        }

        private int _iAttemptCount;
        public int AttemptCount
        {
            get { return _iAttemptCount; }
            set { if (this._iAttemptCount != value) { _iAttemptCount = value; } }
        }
    }
}
