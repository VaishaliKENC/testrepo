using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class ProductCatalogSettings:BaseEntity
    {
        public const string SET_CURRENCY_AND_LOCATION_FILTER = "PCSETT01";
        public const string SET_CATALOG_ON_LOGIN_PAGE = "PCSETT02";
        public const string SET_CATALOG_PAGE_SIZE = "PCSETT03";
        public const string SET_LOGIN_PAGE_CATALOG_PAGE_SIZE = "PCSETT04";
        public const string SET_DEFAULT_CATALOG_VIEW = "PCSETT05";
        public const string SET_SHOW_MENU = "PCSETT06";
        public const string SET_OFFLINE_PAYMENTMODE = "PCSETT10";
        public const string SET_CURRENCY_SYMBOL = "PCSETT08";
        public const string SET_CURRENCY_CODE = "PCSETT09";
        public const string OrderCreation_AdminEmailID = "PCSETT11";
        public const string AskaQuestion_AdminEmailID = "PCSETT19";
        public const string ShareProduct_AdminEmailID = "PCSETT20";


        /// <summary>
        /// Default Contructor
        /// <summary>
        public ProductCatalogSettings()
        { }


        public string SettingTitle { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }

        public string Type { get; set; }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkUpdate
        }

        public new enum Method
        {
            Get
        }
    }
}
