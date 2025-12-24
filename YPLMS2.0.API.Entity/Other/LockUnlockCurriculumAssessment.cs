/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Bharat Mohane 
* Created:<30/09/20>
* Last Modified:</  /  >
*/
using System;

namespace YPLMS2._0.API.Entity
{
    public class LockUnlockCurriculumAssessment : BaseEntity
    {
        public const string STEP1 = "Step1";
        public const string STEP2 = "Step2";
        public LockUnlockCurriculumAssessment()
        { }

        /// <summary>
        /// Method enum
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete,
            AppemptUpdate
        }

        /// <summary>
        /// List method enum
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            UnLockSelectd
        }

       

        private string _LockUnlockId;
        /// <summary>
        /// EntityName
        /// </summary>
        public string LockUnlockId
        {
            get { return _LockUnlockId; }
            set { if (this._LockUnlockId != value) { _LockUnlockId = value; } }
        }

        private string _strSystemUserGUID;
        /// <summary>
        /// SystemUserGUID
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private string _strActivityId;
        /// <summary>
        /// Description
        /// </summary>
        public string ActivityId
        {
            get { return _strActivityId; }
            set { if (this._strActivityId != value) { _strActivityId = value; } }
        }


        private string _strActivityTypeId;
        /// <summary>
        /// RecordID
        /// </summary>
        public string ActivityTypeId
        {
            get { return _strActivityTypeId; }
            set { if (this._strActivityTypeId != value) { _strActivityTypeId = value; } }
        }



        private string _strLanguageId;
        /// <summary>
        /// RecordID
        /// </summary>
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strActivityTypeId != value) { _strLanguageId = value; } }
        }

        private DateTime _AutoUnLockHrs;
        /// <summary>
        /// AuditTrail Date
        /// </summary>
        public DateTime AutoUnLockHrs
        {
            get { return _AutoUnLockHrs; }
            set { if (this._AutoUnLockHrs != value) { _AutoUnLockHrs = value; } }
        }
                
        private DateTime _AdminUnlockHrs;
        /// <summary>
        /// ToDate
        /// </summary>
        public DateTime AdminUnlockHrs
        {
            get { return _AdminUnlockHrs; }
            set { if (this._AdminUnlockHrs != value) { _AdminUnlockHrs = value; } }
        }

      
        private string _strStep;
        /// <summary>
        /// ArchiveById
        /// </summary>
        public string Step
        {
            get { return _strStep; }
            set { if (this._strStep != value) { _strStep = value; } }
        }

      
        private bool _IsLock;
        /// <summary>
        /// ArchiveToDate
        /// </summary>
        public bool IsLock
        {
            get { return _IsLock; }
            set { if (this._IsLock != value) { _IsLock = value; } }
        }

        private int _iAttempt;
        /// <summary>
        /// ArchiveToDate
        /// </summary>
        public int Attempt
        {
            get { return _iAttempt; }
            set { if (this._iAttempt != value) { _iAttempt = value; } }
        }
    }
}
