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
    /// class ForumPost : BaseEntity 
    /// </summary>
    /// 
    [Serializable]
    public class ForumPost : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ForumPost()
        { }


        private string _strThreadID;
        /// <summary>
        /// ThreadID
        /// </summary>
        public string ThreadID
        {
            get { return _strThreadID; }
            set { if (this._strThreadID != value) { _strThreadID = value; } }
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

        private string _strPostContent;
        /// <summary>
        /// PostContent
        /// </summary>
        public string PostContent
        {
            get { return _strPostContent; }
            set { if (this._strPostContent != value) { _strPostContent = value; } }
        }

        private bool _strIsMarkAsAnswer;
        /// <summary>
        /// IsMarkAsAnswer
        /// </summary>
        public bool IsMarkAsAnswer
        {
            get { return _strIsMarkAsAnswer; }
            set { if (this._strIsMarkAsAnswer != value) { _strIsMarkAsAnswer = value; } }
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

        private string _strUserProfileImg;
        public string UserProfileImg
        {
            get { return _strUserProfileImg; }
            set { if (this._strUserProfileImg != value) { _strUserProfileImg = value; } }
        }

        public string SubCategoryID { get; set; }
        public string CategoryID { get; set; }
        public string CreatedBy { get; set; }
        public string ThreadTitle { get; set; }
        public int PostCountByUser { get; set; }



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

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll
        }
    }
}