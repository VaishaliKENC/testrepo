/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<6/13/2013>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ForumThread : BaseEntity 
    /// </summary>
    ///
    [Serializable]
    public class ForumThread : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ForumThread()
        { }


        private string _strSubCategoryID;
        /// <summary>
        /// SubCategoryID
        /// </summary>
        public string SubCategoryID
        {
            get { return _strSubCategoryID; }
            set { if (this._strSubCategoryID != value) { _strSubCategoryID = value; } }
        }

        private string _strCategoryID;
        /// <summary>
        /// CategoryID
        /// </summary>
        public string CategoryID
        {
            get { return _strCategoryID; }
            set { if (this._strCategoryID != value) { _strCategoryID = value; } }
        }

        private string _strCategoryName;
        /// <summary>
        /// CategoryName
        /// </summary>
        public string CategoryName
        {
            get { return _strCategoryName; }
            set { if (this._strCategoryName != value) { _strCategoryName = value; } }
        }

        private string _strSubCategoryName;
        /// <summary>
        /// SubCategoryName
        /// </summary>
        public string SubCategoryName
        {
            get { return _strSubCategoryName; }
            set { if (this._strSubCategoryName != value) { _strSubCategoryName = value; } }
        }

        private string _strSubCategoryDesc;
        /// <summary>
        /// SubCategoryDescription
        /// </summary>
        public string SubCategoryDescription
        {
            get { return _strSubCategoryDesc; }
            set { if (this._strSubCategoryDesc != value) { _strSubCategoryDesc = value; } }
        }
        private string _strLanguageID;
        /// <summary>
        /// LanguageID
        /// </summary>
        public string LanguageID
        {
            get { return _strLanguageID; }
            set { if (this._strLanguageID != value) { _strLanguageID = value; } }
        }

        private string _strTitle;
        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return _strTitle; }
            set { if (this._strTitle != value) { _strTitle = value; } }
        }

        private string _strDescription;
        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get { return _strDescription; }
            set { if (this._strDescription != value) { _strDescription = value; } }
        }

        private Int64 _strViewCount;
        /// <summary>
        /// ViewCount
        /// </summary>
        public Int64 ViewCount
        {
            get { return _strViewCount; }
            set { if (this._strViewCount != value) { _strViewCount = value; } }
        }

        private bool _strIsClosed;
        /// <summary>
        /// IsClosed
        /// </summary>
        public bool IsClosed
        {
            get { return _strIsClosed; }
            set { if (this._strIsClosed != value) { _strIsClosed = value; } }
        }

        private bool _strIsReplyModeration;
        /// <summary>
        /// IsReplyModeration
        /// </summary>
        public bool IsReplyModeration
        {
            get { return _strIsReplyModeration; }
            set { if (this._strIsReplyModeration != value) { _strIsReplyModeration = value; } }
        }

        private string _strStatus;
        /// <summary>
        /// Status
        /// </summary>
        public string Status
        {
            get { return _strStatus; }
            set { if (this._strStatus != value) { _strStatus = value; } }
        }

        private string _strModeratedById;
        /// <summary>
        /// ModeratedById
        /// </summary>
        public string ModeratedById
        {
            get { return _strModeratedById; }
            set { if (this._strModeratedById != value) { _strModeratedById = value; } }
        }

        private DateTime _strModeratedDate;
        /// <summary>
        /// ModeratedDate
        /// </summary>
        public DateTime ModeratedDate
        {
            get { return _strModeratedDate; }
            set { if (this._strModeratedDate != value) { _strModeratedDate = value; } }
        }

        public string CreatedBy { get; set; }
        public string LastPostBy { get; set; }
        public int PostCount { get; set; }


        private DateTime? _LastPostDate;
        /// <summary>
        /// LastPostDate
        /// </summary>
        public DateTime? LastPostDate
        {
            get { return _LastPostDate; }
            set { if (this._LastPostDate != value) { _LastPostDate = value; } }
        }

        private string _strUserProfileImg;
        public string UserProfileImg
        {
            get { return _strUserProfileImg; }
            set { if (this._strUserProfileImg != value) { _strUserProfileImg = value; } }
        }

        private string _strBookmarkId;
        /// <summary>
        /// Title
        /// </summary>
        public string BookmarkId
        {
            get { return _strBookmarkId; }
            set { if (this._strBookmarkId != value) { _strBookmarkId = value; } }
        }

        private string _systemUserGUID;
        /// <summary>
        /// Title
        /// </summary>
        public string SystemUserGUID
        {
            get { return _systemUserGUID; }
            set { if (this._systemUserGUID != value) { _systemUserGUID = value; } }
        }
        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete,
            UpdateViewCount,
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllBySubCat,
            GetAllLearner
        }
    }
}