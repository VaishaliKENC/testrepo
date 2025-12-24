using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class MerchantInfo:BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public MerchantInfo()
        { }

        private string _strMerchantID;
        /// <summary>
        /// ProgramTypeId
        /// </summary>
        public string MerchantID
        {
            get { return _strMerchantID; }
            set { if (this._strMerchantID != value) { _strMerchantID = value; } }
        }

        private string _strToken;
        /// <summary>
        /// ProgramTypeId
        /// </summary>
        public string Token
        {
            get { return _strToken; }
            set { if (this._strToken != value) { _strToken = value; } }
        }


        private bool _strIsActive;
        /// <summary>
        /// ProgramTypeId
        /// </summary>
        public bool IsActive
        {
            get { return _strIsActive; }
            set { if (this._strIsActive != value) { _strIsActive = value; } }
        }

        private string _strRedirectURL;
        /// <summary>
        /// this property is used at the time of selecting the cupon data.
        /// </summary>
        public string RedirectURL
        {
            get { return _strRedirectURL; }
            set { if (this._strRedirectURL != value) { _strRedirectURL = value; } }
        }

        private string _strterminalUrl;
        /// <summary>
        /// this property is used at the time of selecting the cupon data.
        /// </summary>
        public string terminalUrl
        {
            get { return _strterminalUrl; }
            set { if (this._strterminalUrl != value) { _strterminalUrl = value; } }
        }

        private string _strProtocol;
        /// <summary>
        /// ProgramTypeId
        /// </summary>
        public string Protocol
        {
            get { return _strProtocol; }
            set { if (this._strProtocol != value) { _strProtocol = value; } }
        }

        private string _strPartnerName;
        /// <summary>
        /// PartnerName
        /// </summary>
        public string PartnerName
        {
            get { return _strPartnerName; }
            set { if (this._strPartnerName != value) { _strPartnerName = value; } }
        }

        private string _strVendorName;
        /// <summary>
        /// VendorName
        /// </summary>
        public string VendorName
        {
            get { return _strVendorName; }
            set { if (this._strVendorName != value) { _strVendorName = value; } }
        }

        private string _strUserName;
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName
        {
            get { return _strUserName; }
            set { if (this._strUserName != value) { _strUserName = value; } }
        }

        private string _strUserPassword;
        /// <summary>
        /// UserPassword
        /// </summary>
        public string UserPassword
        {
            get { return _strUserPassword; }
            set { if (this._strUserPassword != value) { _strUserPassword = value; } }
        }


        public string PaymentGatewayName { get; set; }
        /// <summary>
        /// enum Method
        /// </summary>
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
