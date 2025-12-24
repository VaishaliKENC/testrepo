/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh & Ashish
* Created:<13/07/09>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class EmailTemplate:BaseEntity 
    /// </summary>
    [Serializable]
    public class EmailTemplate : EmailTemplateLanguage
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public EmailTemplate()
        { _entListEmailTemplateLanguage = new List<EmailTemplateLanguage>(); }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetTypeById,
            Add,
            Update,
            AddEmailTemplateLanguage,
            DeleteEmailTemplateLanguage,
            SelectByName
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            DeleteEmailTemplate
        }

        private string _strEmailTemplateDefaultTitle;
        /// <summary>
        /// Email Template Default Title
        /// </summary>
        public string EmailTemplateDefaultTitle
        {
            get { return _strEmailTemplateDefaultTitle; }
            set { if (this._strEmailTemplateDefaultTitle != value) { _strEmailTemplateDefaultTitle = value; } }
        }

        private bool _bIsPrivate;
        /// <summary>
        /// IsPrivate
        /// </summary>
        public bool IsPrivate
        {
            get { return _bIsPrivate; }
            set { if (this._bIsPrivate != value) { _bIsPrivate = value; } }
        }


        private bool _bIsDefualt;
        /// <summary>
        /// IsPrivate
        /// </summary>
        public bool IsDefualt
        {
            get { return _bIsDefualt; }
            set { if (this._bIsDefualt != value) { _bIsDefualt = value; } }
        }

       

        private string _strEmailFromId;
        /// <summary>
        /// Email From Id
        /// </summary>
        public string EmailFromId
        {
            get { return _strEmailFromId; }
            set { if (this._strEmailFromId != value) { _strEmailFromId = value; } }
        }

        private string _strEmailReplyToId;
        /// <summary>
        /// Email Reply To Id
        /// </summary>
        public string EmailReplyToId
        {
            get { return _strEmailReplyToId; }
            set { if (this._strEmailReplyToId != value) { _strEmailReplyToId = value; } }
        }


        private int _iNoOfTimesUsed;
        /// <summary>
        /// NoOfTimesUsed
        /// </summary>
        public int NoOfTimesUsed
        {
            get { return _iNoOfTimesUsed; }
            set { if (this._iNoOfTimesUsed != value) { _iNoOfTimesUsed = value; } }
        }

        private bool _bIsUsed;
        /// <summary>
        /// IsUsed
        /// </summary>
        public bool IsUsed
        {
            get { return _bIsUsed; }
            set { if (this._bIsUsed != value) { _bIsUsed = value; } }
        }

        private List<EmailTemplateLanguage> _entListEmailTemplateLanguage;
        /// <summary>
        /// Email Template Language
        /// </summary>
        public List<EmailTemplateLanguage> EmailTemplateLanguage
        {
            get { return _entListEmailTemplateLanguage; }
            set { if (this._entListEmailTemplateLanguage != value) { _entListEmailTemplateLanguage = value; } }
        }
    }
}