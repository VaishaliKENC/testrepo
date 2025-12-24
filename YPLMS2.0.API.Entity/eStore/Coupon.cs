using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class Coupon:BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public Coupon()
        { }

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

        private DateTime _strExpiryToDate;
        /// <summary>
        /// ProgramTypeId
        /// </summary>
        public DateTime ExpiryToDate
        {
            get { return _strExpiryToDate; }
            set { if (this._strExpiryToDate != value) { _strExpiryToDate = value; } }
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

        private bool _strIsUsed;
        /// <summary>
        /// ProgramTypeId
        /// </summary>
        public bool IsUsed
        {
            get { return _strIsUsed; }
            set { if (this._strIsUsed != value) { _strIsUsed = value; } }
        }

        private string _strIsUsedd;
        /// <summary>
        /// this property is used at the time of selecting the cupon data.
        /// </summary>
        public string IsUsedd
        {
            get { return _strIsUsedd; }
            set { if (this._strIsUsedd != value) { _strIsUsedd = value; } }
        }



        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetByCouponCode,
            Add,
            Update,
            Delete,
            GetCouponCodes
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
