/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:
* Created:<12/11/13>
* Last Modified:<12/11/13>
*/
using System;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class FAQ : BaseEntity
    {
        public FAQ()
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
            UpdateLanguage
        }

        /// <summary>
        /// List method enum
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetFAQLanguages
        }

        private string _strLanguageId;
        /// <summary>
        /// Language Id
        /// </summary>
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }

        private string _strLanguageName;
        /// <summary>
        /// Language Name
        /// </summary>
        public string LanguageName
        {
            get { return _strLanguageName; }
            set { if (this._strLanguageName != value) { _strLanguageName = value; } }
        }

        private string _strCategoryId;
        /// <summary>
        /// CategoryId
        /// </summary>
        public string CategoryId
        {
            get { return _strCategoryId; }
            set { if (this._strCategoryId != value) { _strCategoryId = value; } }
        }

        private string _strQuestion;
        /// <summary>
        /// Question
        /// </summary>
        public string Question
        {
            get { return _strQuestion; }
            set { if (this._strQuestion != value) { _strQuestion = value; } }
        }

        private string _strAnswer;
        /// <summary>
        /// Answer
        /// </summary>
        public string Answer
        {
            get { return _strAnswer; }
            set { if (this._strAnswer != value) { _strAnswer = value; } }
        }

        private Nullable<DateTime> _dateClose;
        /// <summary>
        /// CloseDate
        /// </summary>
        public Nullable<DateTime> CloseDate
        {
            get { return _dateClose; }
            set { if (this._dateClose != value) { _dateClose = value; } }
        }

        private Nullable<bool> _bIsActive;
        /// <summary>
        /// To check Is Active
        /// </summary>
        public Nullable<bool> IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
        }

        private string _strAttachedFile;
        /// <summary>
        /// AttachedFile
        /// </summary>
        public string AttachedFile
        {
            get { return _strAttachedFile; }
            set { if (this._strAttachedFile != value) { _strAttachedFile = value; } }
        }

        private string _strAttachedLink;
        /// <summary>
        /// _strAttachedLink
        /// </summary>
        public string AttachedLink
        {
            get { return _strAttachedLink; }
            set { if (this._strAttachedLink != value) { _strAttachedLink = value; } }
        }

        private Nullable<bool> _bIsForLearner;
        /// <summary>
        /// To check In Stored Procedure fetch data on condition according to Admin and Learner
        /// </summary>
        public Nullable<bool> IsForLearner
        {
            get { return _bIsForLearner; }
            set { if (this._bIsForLearner != value) { _bIsForLearner = value; } }
        }

        public int DisplayOrder { get; set; }


    }

}