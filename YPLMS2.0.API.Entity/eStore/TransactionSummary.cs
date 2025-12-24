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
    /// class TransactionSummary : BaseEntity 
    /// </summary>
    /// 
    public class TransactionSummary : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public TransactionSummary()
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

        private double _strAmountCaptured;
        /// <summary>
        /// AmountCaptured
        /// </summary>
        public double AmountCaptured
        {
            get { return _strAmountCaptured; }
            set { if (this._strAmountCaptured != value) { _strAmountCaptured = value; } }
        }

        private double _strAmountCredited;
        /// <summary>
        /// AmountCredited
        /// </summary>
        public double AmountCredited
        {
            get { return _strAmountCredited; }
            set { if (this._strAmountCredited != value) { _strAmountCredited = value; } }
        }

        private bool _strAnnulled;
        /// <summary>
        /// Annulled
        /// </summary>
        public bool Annulled
        {
            get { return _strAnnulled; }
            set { if (this._strAnnulled != value) { _strAnnulled = value; } }
        }

        private float _strAuthorizationId;
        /// <summary>
        /// AuthorizationId
        /// </summary>
        public float AuthorizationId
        {
            get { return _strAuthorizationId; }
            set { if (this._strAuthorizationId != value) { _strAuthorizationId = value; } }
        }

        private bool _strAuthorized;
        /// <summary>
        /// Authorized
        /// </summary>
        public bool Authorized
        {
            get { return _strAuthorized; }
            set { if (this._strAuthorized != value) { _strAuthorized = value; } }
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