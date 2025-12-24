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
    /// class QuestionBankOptionsLanguage : BaseEntity 
    /// </summary>
    /// 
    public class QuestionBankOptionsLanguage : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public QuestionBankOptionsLanguage()
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

        private string _strOptionTitle;
        /// <summary>
        /// OptionTitle
        /// </summary>
        public string OptionTitle
        {
            get { return _strOptionTitle; }
            set { if (this._strOptionTitle != value) { _strOptionTitle = value; } }
        }

        private string _strOptionDescription;
        /// <summary>
        /// OptionDescription
        /// </summary>
        public string OptionDescription
        {
            get { return _strOptionDescription; }
            set { if (this._strOptionDescription != value) { _strOptionDescription = value; } }
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