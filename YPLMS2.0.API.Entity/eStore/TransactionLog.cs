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
    /// class TransactionLog : BaseEntity 
    /// </summary>
    /// 
    public class TransactionLog : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public TransactionLog()
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

        private Double _strAmount;
        /// <summary>
        /// Amount
        /// </summary>
        public Double Amount
        {
            get { return _strAmount; }
            set { if (this._strAmount != value) { _strAmount = value; } }
        }

        private float _strBatchNumber;
        /// <summary>
        /// BatchNumber
        /// </summary>
        public float BatchNumber
        {
            get { return _strBatchNumber; }
            set { if (this._strBatchNumber != value) { _strBatchNumber = value; } }
        }

        private DateTime _strTransactionDate;
        /// <summary>
        /// TransactionDate
        /// </summary>
        public DateTime TransactionDate
        {
            get { return _strTransactionDate; }
            set { if (this._strTransactionDate != value) { _strTransactionDate = value; } }
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

        private string _strOperation;
        /// <summary>
        /// Operation
        /// </summary>
        public string Operation
        {
            get { return _strOperation; }
            set { if (this._strOperation != value) { _strOperation = value; } }
        }

        private string _strTransactionReconRef;
        /// <summary>
        /// TransactionReconRef
        /// </summary>
        public string TransactionReconRef
        {
            get { return _strTransactionReconRef; }
            set { if (this._strTransactionReconRef != value) { _strTransactionReconRef = value; } }
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