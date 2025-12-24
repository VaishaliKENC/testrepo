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
    /// class BlogPostComments : BaseEntity 
    /// </summary>
    /// 
    public class BlogPostComments : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public BlogPostComments()
        { }


        private string _strLanguageID;
        /// <summary>
        /// LanguageID
        /// </summary>
        public string LanguageID
        {
            get { return _strLanguageID; }
            set { if (this._strLanguageID != value) { _strLanguageID = value; } }
        }

        private string _strPostID;
        /// <summary>
        /// PostID
        /// </summary>
        public string PostID
        {
            get { return _strPostID; }
            set { if (this._strPostID != value) { _strPostID = value; } }
        }

        private DateTime _strCommentDate;
        /// <summary>
        /// CommentDate
        /// </summary>
        public DateTime CommentDate
        {
            get { return _strCommentDate; }
            set { if (this._strCommentDate != value) { _strCommentDate = value; } }
        }

        private string _strCommentByUserId;
        /// <summary>
        /// CommentByUserId
        /// </summary>
        public string CommentByUserId
        {
            get { return _strCommentByUserId; }
            set { if (this._strCommentByUserId != value) { _strCommentByUserId = value; } }
        }

        private string _strComment;
        /// <summary>
        /// Comment
        /// </summary>
        public string Comment
        {
            get { return _strComment; }
            set { if (this._strComment != value) { _strComment = value; } }
        }

        //private bool _strIsApproved;
        ///// <summary>
        ///// IsApproved
        ///// </summary>
        //public bool IsApproved
        //{
        //get { return _strIsApproved; }
        //set { if (this._strIsApproved != value) { _strIsApproved = value; } }
        //}

        private float? _strRating;
        /// <summary>
        /// Rating
        /// </summary>
        public float? Rating
        {
            get { return _strRating; }
            set { if (this._strRating != value) { _strRating = value; } }
        }

        private string _strUserProfileImg;
        public string UserProfileImg
        {
            get { return _strUserProfileImg; }
            set { if (this._strUserProfileImg != value) { _strUserProfileImg = value; } }
        }


        public string IsApproved { get; set; }
        public string ShortTitle { get; set; }
        public string ShortComments { get; set; }
        public string LanguageName { get; set; }
        public string CommentBy { get; set; }
        public string ApprovedBy { get; set; }
        public string Title { get; set; }



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