using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    
    /// <summary>
    /// class DiscountCoupon : DiscountCouponCodes 
    /// </summary>
    /// 
    public class DiscountCoupon : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public DiscountCoupon()
        {

        }

      
        private bool? _IsUsed;
        /// <summary>
        /// CategoryName
        /// </summary>
        public bool? IsUsed
        {
            get { return _IsUsed; }
            set { if (this._IsUsed != value) { _IsUsed = value; } }
        }
        

        private bool? _strIsActive;
        /// <summary>
        /// IsActive
        /// </summary>
        public bool? IsActive
        {
            get { return _strIsActive; }
            set { if (this._strIsActive != value) { _strIsActive = value; } }
        }

        private string _strCouponPrefix;
        /// <summary>
        /// CouponPrefix
        /// </summary>
        public string CouponPrefix
        {
            get { return _strCouponPrefix; }
            set { if (this._strCouponPrefix != value) { _strCouponPrefix = value; } }
        }
        
        private string _strTitle;
        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return _strTitle; }
            set { if (this._strTitle != value) { _strTitle = value; } }
        }


        private DateTime ? _strExpirydate;
        /// <summary>
        /// Expirydate
        /// </summary>
        public DateTime?  Expirydate
        {
            get { return _strExpirydate; }
            set { if (this._strExpirydate != value) { _strExpirydate = value; } }
        }

        private int _strNoOfCoupons;
        /// <summary>
        /// NoOfCoupons
        /// </summary>
        public int NoOfCoupons
        {
            get { return _strNoOfCoupons; }
            set { if (this._strNoOfCoupons != value) { _strNoOfCoupons = value; } }
        }

        private string _strDiscountType;
        /// <summary>
        /// EmailList
        /// </summary>
        public string DiscountType
        {
            get { return _strDiscountType; }
            set { if (this._strDiscountType != value) { _strDiscountType = value; } }
        }

       

        public string ProdcatId { get; set; }

        private int _strNoOfUsers; 
        /// <summary>
        /// EmailList
        /// </summary>
        public int NoOfUsers
        {
            get { return _strNoOfUsers; }
            set { if (this._strNoOfUsers != value) { _strNoOfUsers = value; } }
        }


        private int _strUsedCount;
        /// <summary>
        /// EmailList
        /// </summary>
        public int UsedCount
        {
            get { return _strUsedCount; }
            set { if (this._strUsedCount != value) { _strUsedCount = value; } }
        }

        private string _strCouponCode;
        /// <summary>
        /// CouponPrefix
        /// </summary>
        public string CouponCode
        {
            get { return _strCouponCode; }
            set { if (this._strCouponCode != value) { _strCouponCode = value; } }
        }

        private string _strCouponAppliedTo; 
        /// <summary>
        /// EmailList
        /// </summary>
        public string CouponAppliedTo
        {
            get { return _strCouponAppliedTo; }
            set { if (this._strCouponAppliedTo != value) { _strCouponAppliedTo = value; } }
        }



        private string _strCouponType; 
        /// <summary>
        /// EmailList
        /// </summary>
        public string CouponType
        {
            get { return _strCouponType; }
            set { if (this._strCouponType != value) { _strCouponType = value; } }
        }


        private DateTime? _strStartdate;
        /// <summary>
        /// Expirydate
        /// </summary>
        public DateTime? Startdate
        {
            get { return _strStartdate; }
            set { if (this._strStartdate != value) { _strStartdate = value; } }
        }

        private Double _strDiscountPrice;
        /// <summary>
        /// EmailList
        /// </summary>
        public Double DiscountPrice
        {
            get { return _strDiscountPrice; }
            set { if (this._strDiscountPrice != value) { _strDiscountPrice = value; } }
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,         
            Add,
            Update,
            Delete,
            IsCouponcodeAvailable

        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkDelete
           
        }
    }
}
