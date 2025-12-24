/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<7/8/2013>
* Last Modified:
*/

using System;
using System.Collections.Generic;


namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class OrderHistory : BaseEntity 
    /// </summary>
    /// 
    public class OrderHistory : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public OrderHistory()
        {
            _TransactionLog = new List<TransactionLog>();
            _TransactionErrorLog = new List<TransactionErrorLog>();
        }

        private List<TransactionLog> _TransactionLog;
        /// <summary>
        /// TransactionLog
        /// </summary>
        public List<TransactionLog> lTransactionLog
        {
            get { return _TransactionLog; }
        }

        private List<TransactionErrorLog> _TransactionErrorLog;
        /// <summary>
        /// TransactionErrorLog
        /// </summary>
        public List<TransactionErrorLog> lTransactionErrorLog
        {
            get { return _TransactionErrorLog; }
        }

        private DateTime _strOrderDate;
        /// <summary>
        /// OrderDate
        /// </summary>
        public DateTime OrderDate
        {
            get { return _strOrderDate; }
            set { if (this._strOrderDate != value) { _strOrderDate = value; } }
        }


        private DateTime _strOrderToDate;
        /// <summary>
        /// OrderDate
        /// </summary>
        public DateTime OrderToDate
        {
            get { return _strOrderToDate; }
            set { if (this._strOrderToDate != value) { _strOrderToDate = value; } }
        }

        private string _strTransactionID;
        /// <summary>
        /// TransanctionID
        /// 
        /// </summary>
        public string TransactionID
        {
            get { return _strTransactionID; }
            set { if (this._strTransactionID != value) { _strTransactionID = value; } }
        }

        private string _strSystemUserGUID;
        /// <summary>
        /// SystemUserGUID
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private string _strUserNameAlias;
        /// <summary>
        /// SystemUserGUID
        /// </summary>
        public string UserNameAlias
        {
            get { return _strUserNameAlias; }
            set { if (this._strUserNameAlias != value) { _strUserNameAlias = value; } }
        }

        private string _strSignUpID;
        /// <summary>
        /// SignUpID
        /// </summary>
        public string SignUpID
        {
            get { return _strSignUpID; }
            set { if (this._strSignUpID != value) { _strSignUpID = value; } }
        }
        private eTransactionStatus _strTransactionStatus;
        /// <summary>
        /// TransactionStatus
        /// </summary>
        public eTransactionStatus TransactionStatus
        {
            get { return _strTransactionStatus; }
            set { if (this._strTransactionStatus != value) { _strTransactionStatus = value; } }
        }

        private string _strTransactionStatus1;
        /// <summary>
        /// TransactionStatus
        /// </summary>
        public string paramTransactionStatus
        {
            get { return _strTransactionStatus1; }
            set { if (this._strTransactionStatus1 != value) { _strTransactionStatus1 = value; } }
        }

      

        private string _strCouponCode;
        /// <summary>
        /// CouponCode
        /// </summary>
        public string CouponCode
        {
            get { return _strCouponCode; }
            set { if (this._strCouponCode != value) { _strCouponCode = value; } }
        }

        

        private string _strCurrency;
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency
        {
            get { return _strCurrency; }
            set { if (this._strCurrency != value) { _strCurrency = value; } }
        }

      

        private string _strOrderDescription;
        /// <summary>
        /// OrderDescription
        /// </summary>
        public string OrderDescription
        {
            get { return _strOrderDescription; }
            set { if (this._strOrderDescription != value) { _strOrderDescription = value; } }
        }

        private double _strAmountTotal;
        /// <summary>
        /// AmountTotal
        /// </summary>
        public double AmountTotal
        {
            get { return _strAmountTotal; }
            set { if (this._strAmountTotal != value) { _strAmountTotal = value; } }
        }

        public double DiscountTotal { get; set; }

        private string _strMerchantId;
        /// <summary>
        /// MerchantId
        /// </summary>
        public string MerchantId
        {
            get { return _strMerchantId; }
            set { if (this._strMerchantId != value) { _strMerchantId = value; } }
        }

        private string _strCardPAN;
        /// <summary>
        /// CardPAN
        /// </summary>
        public string CardPAN
        {
            get { return _strCardPAN; }
            set { if (this._strCardPAN != value) { _strCardPAN = value; } }
        }

        private int _strCardExpiryDate;
        /// <summary>
        /// CardExpiryDate
        /// </summary>
        public int CardExpiryDate
        {
            get { return _strCardExpiryDate; }
            set { if (this._strCardExpiryDate != value) { _strCardExpiryDate = value; } }
        }

        private string _strIssuer;
        /// <summary>
        /// Issuer
        /// </summary>
        public string Issuer
        {
            get { return _strIssuer; }
            set { if (this._strIssuer != value) { _strIssuer = value; } }
        }

        private string _strIssuerCountry;
        /// <summary>
        /// IssuerCountry
        /// </summary>
        public string IssuerCountry
        {
            get { return _strIssuerCountry; }
            set { if (this._strIssuerCountry != value) { _strIssuerCountry = value; } }
        }

        private string _strPaymentMethod;
        /// <summary>
        /// PaymentMethod
        /// </summary>
        public string PaymentMethod
        {
            get { return _strPaymentMethod; }
            set { if (this._strPaymentMethod != value) { _strPaymentMethod = value; } }
        }

        private string _strCustomerIPAddress;
        /// <summary>
        /// CustomerIPAddress
        /// </summary>
        public string CustomerIPAddress
        {
            get { return _strCustomerIPAddress; }
            set { if (this._strCustomerIPAddress != value) { _strCustomerIPAddress = value; } }
        }

        //private string _strVoucherId;
        ///// <summary>
        ///// VoucherId
        ///// </summary>
        //public string VoucherId
        //{
        //    get { return _strVoucherId; }
        //    set { if (this._strVoucherId != value) { _strVoucherId = value; } }
        //}


        private bool? _strOrderCompletionStatus;
        /// <summary>
        /// VoucherId
        /// </summary>
        public bool? OrderCompletionStatus
        {
            get { return _strOrderCompletionStatus; }
            set { if (this._strOrderCompletionStatus != value) { _strOrderCompletionStatus = value; } }
        }

        private bool _IsCompleteByManually;
        /// <summary>
        /// IsCompleteByManually
        /// </summary>
        public bool IsCompleteByManually
        {
            get { return _IsCompleteByManually; }
            set { if (this._IsCompleteByManually != value) { _IsCompleteByManually = value; } }
        }

        private string _strCompletedByID;
        /// <summary>
        /// CompletedByID
        /// </summary>
        public string CompletedByID
        {
            get { return _strCompletedByID; }
            set { if (this._strCompletedByID != value) { _strCompletedByID = value; } }
        }


        private string _strProductId;
        /// <summary>
        /// CompletedByID
        /// </summary>
        public string ProductId
        {
            get { return _strProductId; }
            set { if (this._strProductId != value) { _strProductId = value; } }
        }



        private string _strProductCode;
        /// <summary>
        /// CompletedByID
        /// </summary>
        public string ProductCode
        {
            get { return _strProductCode; }
            set { if (this._strProductCode != value) { _strProductCode = value; } }
        }


        private string _strProductTitle;
        /// <summary>
        /// CompletedByID
        /// </summary>
        public string ProductTitle
        {
            get { return _strProductTitle; }
            set { if (this._strProductTitle != value) { _strProductTitle = value; } }
        }

        private int _iLicenseCount;
        /// <summary>
        /// CompletedByID
        /// </summary>
        public int LicenseCount
        {
            get { return _iLicenseCount; }
            set { if (this._iLicenseCount != value) { _iLicenseCount = value; } }
        }

        private int _iLicenseConsumed;
        /// <summary>
        /// CompletedByID
        /// </summary>
        public int LicenseConsumed
        {
            get { return _iLicenseConsumed; }
            set { if (this._iLicenseConsumed != value) { _iLicenseConsumed = value; } }
        }


        public int IsExternalProduct { get; set; }

        private string _strToAddress1;      
        public string ToAddress1
        {
            get { return _strToAddress1; }
            set { if (this._strToAddress1 != value) { _strToAddress1 = value; } }
        }

        private string _strToCity;
        public string ToCity
        {
            get { return _strToCity; }
            set { if (this._strToCity != value) { _strToCity = value; } }
        }

        private string _strToState;
        public string ToState
        {
            get { return _strToState; }
            set { if (this._strToState != value) { _strToState = value; } }
        }

        private string _strToZipCode;
        public string ToZipCode
        {
            get { return _strToZipCode; }
            set { if (this._strToZipCode != value) { _strToZipCode = value; } }
        }

        private string _strToCountry;
        public string ToCountry
        {
            get { return _strToCountry; }
            set { if (this._strToCountry != value) { _strToCountry = value; } }
        }

        private string _strToFirstName;
        public string ToFirstName
        {
            get { return _strToFirstName; }
            set { if (this._strToFirstName != value) { _strToFirstName = value; } }
        }
        private string _strToLastName;
        public string ToLastName
        {
            get { return _strToLastName; }
            set { if (this._strToLastName != value) { _strToLastName = value; } }
        }

        private double? _dTaxRate;
        public double? TaxRate
        {
            get { return _dTaxRate; }
            set { if (this._dTaxRate != value) { _dTaxRate = value; } }
        }

        private double? _dTaxAmount;
        public double? TaxAmount
        {
            get { return _dTaxAmount; }
            set { if (this._dTaxAmount != value) { _dTaxAmount = value; } }
        }

        private string _strToAddress2;
        public string ToAddress2
        {
            get { return _strToAddress2; }
            set { if (this._strToAddress2 != value) { _strToAddress2 = value; } }
        }

        private string _strToPhoneNo;
        public string ToPhoneNo
        {
            get { return _strToPhoneNo; }
            set { if (this._strToPhoneNo != value) { _strToPhoneNo = value; } }
        }

        private string _strAvaTaxId;
        public string AvaTaxId
        {
            get { return _strAvaTaxId; }
            set { if (this._strAvaTaxId != value) { _strAvaTaxId = value; } }
        }

        private int _UserOrderCount;
        public int UserOrderCount
        {
            get { return _UserOrderCount; }
            set { if (this._UserOrderCount != value) { _UserOrderCount = value; } }
        }
        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetAllDetails,
            Add,
            Update,
            Delete,
            UpdateStatus,
            GetByTransactionID,
            CompleteOrder,
            IsVoucherAvailable,
            GetOrderReceipt
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {

            GetAll,
            GetAllDetails,
            GetAll_LicenseDetails,
            GetAllOrderReceipt

        }
    }

    public enum eTransactionStatus
    {
        None,
        Initialised,
        Registered,
        Authorized,
        Captured,
        Successful,
        Cancelled,
        Failed
    }
}