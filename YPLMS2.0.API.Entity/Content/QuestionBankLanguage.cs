/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<6/18/2013>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class QuestionBankLanguage : BaseEntity 
    /// </summary>
    /// 
    public class QuestionBankLanguage : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public QuestionBankLanguage()
        { }


        private string _strLanguageId;
        /// <summary>
        /// LanguageId
        /// </summary>
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }

        private string _strQuestionTitle;
        /// <summary>
        /// QuestionTitle
        /// </summary>
        public string QuestionTitle
        {
            get { return _strQuestionTitle; }
            set { if (this._strQuestionTitle != value) { _strQuestionTitle = value; } }
        }

        private string _strQuestionDescription;
        /// <summary>
        /// QuestionDescription
        /// </summary>
        public string QuestionDescription
        {
            get { return _strQuestionDescription; }
            set { if (this._strQuestionDescription != value) { _strQuestionDescription = value; } }
        }




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