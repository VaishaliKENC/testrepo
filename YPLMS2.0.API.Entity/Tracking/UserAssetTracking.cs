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
    ///  UserAssetTracking
    /// </summary>
    [Serializable]
   public class UserAssetTracking : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public UserAssetTracking()
        { }

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
            Add,
            UpdateScannedFileName,
            UpdateVideoTracking,
            UpdateVideoBookmark,
            Get
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

        private ActivityCompletionStatus _completionStatus;
        public ActivityCompletionStatus CompletionStatus
        {
            get { return _completionStatus; }
            set { if (this._completionStatus != value) { _completionStatus = value; } }
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

        private Nullable<DateTime> _dateOfCompletion;
        /// <summary>
        /// Date of Completion
        /// </summary>
        public Nullable<DateTime> DateOfCompletion
        {
            get { return _dateOfCompletion; }
            set { if (this._dateOfCompletion != value) { _dateOfCompletion = value; } }
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

        private Boolean _strIsForAdminPreview;
        /// <summary>
        /// Is For Admin Preview
        /// </summary>
        public Boolean IsForAdminPreview
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
        //WatchedInMins
        private decimal? _iWatchedInMins;
        /// <summary>
        /// Watched In Mins
        /// </summary>
        public decimal? WatchedInMins
        {
            get { return _iWatchedInMins; }
            set { if (this._iWatchedInMins != value) { _iWatchedInMins = value; } }
        }
        public decimal? Progress { get; set; }

        
        //WatchedInMins
        private decimal? _iTotalVideoDurationInMins;
        /// <summary>
        /// Total Video Duration In Mins
        /// </summary>
        public decimal? TotalVideoDurationInMins
        {
            get { return _iTotalVideoDurationInMins; }
            set { if (this._iTotalVideoDurationInMins != value) { _iTotalVideoDurationInMins = value; } }
        }
        //Bookmark Video Tracking
        
        private decimal? _iBookmarkWatchedInMins;
        /// <summary>
        /// Bookmark Video Tracking
        /// </summary>
        public decimal? BookmarkWatchedInMins
        {
            get { return _iBookmarkWatchedInMins; }
            set { if (this._iBookmarkWatchedInMins != value) { _iBookmarkWatchedInMins = value; } }
        }
    }
}