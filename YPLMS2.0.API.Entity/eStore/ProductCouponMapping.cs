using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class ProductCouponMapping : ProductsLanguage
    {
         /// <summary>
        /// Default Contructor
        /// </summary>
        public ProductCouponMapping()
        { 
        }

        private string _strProductId;
        /// <summary>
        /// ProductId
        /// </summary>
        public string ProductId
        {
            get { return _strProductId; }
            set { if (this._strProductId != value) { _strProductId = value; } }
        }

        private string _strCouponId;
        /// <summary>
        /// LocationId
        /// </summary>
        public string CouponID
        {
            get { return _strCouponId; }
            set { if (this._strCouponId != value) { _strCouponId = value; } }
        }

        private string _strCouponCode;
        /// <summary>
        /// ProgramTypeId
        /// </summary>
        public string CouponCode
        {
            get { return _strCouponCode; }
            set { if (this._strCouponCode != value) { _strCouponCode = value; } }
        }

        private string _strTitle;
        /// <summary>
        /// ProgramTypeId
        /// </summary>
        public string Title
        {
            get { return _strTitle; }
            set { if (this._strTitle != value) { _strTitle = value; } }
        }

        private DateTime _strExpiryDate;
        /// <summary>
        /// ProgramTypeId
        /// </summary>
        public DateTime ExpiryDate
        {
            get { return _strExpiryDate; }
            set { if (this._strExpiryDate != value) { _strExpiryDate = value; } }
        }

        private DateTime _strPurchaseDate;
        /// <summary>
        /// ProgramTypeId
        /// </summary>
        public DateTime PurchaseDate
        {
            get { return _strPurchaseDate; }
            set { if (this._strPurchaseDate != value) { _strPurchaseDate = value; } }
        }


        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete
        }

        public new enum ListMethod
        {
            GetAll
        }

    }
}
