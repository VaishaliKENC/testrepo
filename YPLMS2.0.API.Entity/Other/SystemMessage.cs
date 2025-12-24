/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh
* Created:07/09/09
* Last Modified:07/09/09
*/
using System;

namespace YPLMS2._0.API.Entity
{
   [Serializable] public class SystemMessage : BaseEntity
    {

        public const string CACHE_SUFFIX = "_MESSAGES";
        /// <summary>
        /// ENUM Method
        /// </summary>
        public new enum Method
        {
            Get
        }

        /// <summary>
        /// List method ENUM
        /// </summary>
        public new enum ListMethod
        {
            GetAllMessages,
            BulkUpdate
        }

        private string _strLanguageId;
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }

        private string _strMessageText;
        public string MessageText
        {
            get { return _strMessageText; }
            set { if (this._strMessageText != value) { _strMessageText = value; } }
        }

        private string _strMessageType;
        public string MessageType
        {
            get { return _strMessageType; }
            set { if (this._strMessageType != value) { _strMessageType = value; } }
        }

        private string _strMessageDescription;
        public string MessageDescription
        {
            get { return _strMessageDescription; }
            set { if (this._strMessageDescription != value) { _strMessageDescription = value; } }
        }

        private MessageFor _enumMessageFor;
        public MessageFor MessageFor
        {
            get { return _enumMessageFor; }
            set { if (this._enumMessageFor != value) { _enumMessageFor = value; } }
        }
    }

    public enum MessageFor
    {
        Admin,
        Learner,
        Common
    }

    [Serializable]
    public class SystemMessageCache
    {
        private string _strId;
        /// <summary>
        /// To Get Entity object ID
        /// </summary>
        public string ID
        {
            get { return _strId; }
            set { if (this._strId != value) { _strId = value; } }
        }

        private string _strLanguageId;
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }

        private string _strMessageText;
        public string MessageText
        {
            get { return _strMessageText; }
            set { if (this._strMessageText != value) { _strMessageText = value; } }
        }
    }
}