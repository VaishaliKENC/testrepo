/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<7/11/2013>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class TransactionErrorLog : BaseEntity 
    /// </summary>
    /// 
    public class TransactionErrorLog : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public TransactionErrorLog()
        { }


        private string _strTransactionID;
        /// <summary>
        /// TransactionID
        /// </summary>
        public string TransactionID
        {
            get { return _strTransactionID; }
            set { if (this._strTransactionID != value) { _strTransactionID = value; } }
        }

        private DateTime _strErrorDate;
        /// <summary>
        /// ErrorDate
        /// </summary>
        public DateTime ErrorDate
        {
            get { return _strErrorDate; }
            set { if (this._strErrorDate != value) { _strErrorDate = value; } }
        }

        private string _strOperation;
        /// <summary>
        /// Operation
        /// </summary>
        public string Operation
        {
            get { return _strOperation; }
            set { if (this._strOperation != value) { _strOperation = value; } }
        }

        private string _strResponseCode;
        /// <summary>
        /// ResponseCode
        /// </summary>
        public string ResponseCode
        {
            get { return _strResponseCode; }
            set { if (this._strResponseCode != value) { _strResponseCode = value; } }
        }

        private string _strResponseSource;
        /// <summary>
        /// ResponseSource
        /// </summary>
        public string ResponseSource
        {
            get { return _strResponseSource; }
            set { if (this._strResponseSource != value) { _strResponseSource = value; } }
        }

        private string _strResponseText;
        /// <summary>
        /// ResponseText
        /// </summary>
        public string ResponseText
        {
            get { return _strResponseText; }
            set { if (this._strResponseText != value) { _strResponseText = value; } }
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