using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class ProductLocationMapping : ProductsLanguage
    {
         /// <summary>
        /// Default Contructor
        /// </summary>
        public ProductLocationMapping()
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

        private string _strLocationId;
        /// <summary>
        /// LocationId
        /// </summary>
        public string LocationId
        {
            get { return _strLocationId; }
            set { if (this._strLocationId != value) { _strLocationId = value; } }
        }

        private string _strLocationName;
        /// <summary>
        /// LocationName
        /// </summary>
        public string LocationName
        {
            get { return _strLocationName; }
            set { if (this._strLocationName != value) { _strLocationName = value; } }
        }

        private double _strPrice;
        /// <summary>
        /// PublishDate
        /// </summary>
        public double Price
        {
            get { return _strPrice; }
            set { if (this._strPrice != value) { _strPrice = value; } }
        }

        private double _strBasePrice;
        /// <summary>
        /// PublishDate
        /// </summary>
        public double BasePrice
        {
            get { return _strBasePrice; }
            set { if (this._strBasePrice != value) { _strBasePrice = value; } }
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
