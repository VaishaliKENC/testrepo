/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<5/29/2013>
* Last Modified:
*/

using System.Collections.Generic;
using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class BlogPost : BaseEntity 
    /// </summary>
    /// 
    public class BlogPost : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public BlogPost()
        { }

        private int _CommentCount;
        /// <summary>
        /// LanguageID
        /// </summary>
        public int CommentCount
        {
            get { return _CommentCount; }
            set { if (this._CommentCount != value) { _CommentCount = value; } }
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

        private string _strCategoryName;
        /// <summary>
        /// CategoryID
        /// </summary>
        public string CategoryName
        {
            get { return _strCategoryName; }
            set { if (this._strCategoryName != value) { _strCategoryName = value; } }
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

        private string _strTitle;
        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return _strTitle; }
            set { if (this._strTitle != value) { _strTitle = value; } }
        }

        private string _strTags;
        /// <summary>
        /// Tags
        /// </summary>
        public string Tags
        {
            get { return _strTags; }
            set { if (this._strTags != value) { _strTags = value; } }
        }

        private string _strPostContent;
        /// <summary>
        /// PostContent
        /// </summary>
        public string PostContent
        {
            get { return _strPostContent; }
            set { if (this._strPostContent != value) { _strPostContent = value; } }
        }


        private bool _strIsCommentEnabled;
        /// <summary>
        /// IsCommentEnabled
        /// </summary>
        public bool IsCommentEnabled
        {
            get { return _strIsCommentEnabled; }
            set { if (this._strIsCommentEnabled != value) { _strIsCommentEnabled = value; } }
        }

        private bool _strIsCommentModerator;
        /// <summary>
        /// IsCommentModerator
        /// </summary>
        public bool IsCommentModerator
        {
            get { return _strIsCommentModerator; }
            set { if (this._strIsCommentModerator != value) { _strIsCommentModerator = value; } }
        }

        private bool _strIsForAllLanguage;
        /// <summary>
        /// IsForAllLanguage
        /// </summary>
        public bool IsForAllLanguage
        {
            get { return _strIsForAllLanguage; }
            set { if (this._strIsForAllLanguage != value) { _strIsForAllLanguage = value; } }
        }

        private string _strUserProfileImg;
        public string UserProfileImg
        {
            get { return _strUserProfileImg; }
            set { if (this._strUserProfileImg != value) { _strUserProfileImg = value; } }
        }

        public string ShortContent { get; set; }

        public string CreatedBy { get; set; }

        public int Rating { get; set; }

        public bool? IsPublished { get; set; }

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
            UpdateAll
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllForLearner
        }
    }
}