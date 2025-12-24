/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:<Ashish>
* Created:<06/10/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    ///  UserAssetVideoTracking
    /// </summary>
    [Serializable]
   public class UserAssetVideoTracking : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public UserAssetVideoTracking()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
          
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            GetVideoTracking
        }

        private string _strAttemptVideoId;
        /// <summary>
        /// Attempt Video Id
        /// </summary>
        public string AttemptVideoId
        {
            get { return _strAttemptVideoId; }
            set { if (this._strAttemptVideoId != value) { _strAttemptVideoId = value; } }
        }

        private string _strAttemptId;
        /// <summary>
        /// Attempt Id
        /// </summary>
        public string AttemptId
        {
            get { return _strAttemptId; }
            set { if (this._strAttemptId != value) { _strAttemptId = value; } }
        }
        private string _strAssetId;
        /// <summary>
        /// Asset Id
        /// </summary>
        public string AssetId
        {
            get { return _strAssetId; }
            set { if (this._strAssetId != value) { _strAssetId = value; } }
        }

        private string _strSystemUserGUID;
        /// <summary>
        /// User ID
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }


        private Nullable<DateTime> _dateOfStart;
        /// <summary>
        /// Date of Completion
        /// </summary>
        public Nullable<DateTime> DateOfStart
        {
            get { return _dateOfStart; }
            set { if (this._dateOfStart != value) { _dateOfStart = value; } }
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
       
        private decimal? _iWatchedInMins;
        /// <summary>
        /// Watched In Mins
        /// </summary>
        public decimal? WatchedInMins
        {
            get { return _iWatchedInMins; }
            set { if (this._iWatchedInMins != value) { _iWatchedInMins = value; } }
        }

        private decimal? _iTotalVideoDurationInMins;
        /// <summary>
        /// Total Video Duration In Mins
        /// </summary>
        public decimal? TotalVideoDurationInMins
        {
            get { return _iTotalVideoDurationInMins; }
            set { if (this._iTotalVideoDurationInMins != value) { _iTotalVideoDurationInMins = value; } }
        }
        public decimal? Progress { get; set; }

        public class UpdateVideoTrackingRequest
        {
            public string ActivityId { get; set; }
            public string ActivityType { get; set; }
            public string ClientId { get; set; }
            public string LearnerId { get; set; }
            public string TotalDuration { get; set; }
            public string ElaspedTime { get; set; }
            public string VideoEvent { get; set; }
            public string ActivityName { get; set; }
            public int Counter { get; set; } = 0;
        }

    }
}