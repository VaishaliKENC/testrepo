using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class Currency : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public Currency()
        {
        }

        private int _intSeqNo;
        /// <summary>
        /// SeqNo
        /// </summary>
        public int SeqNo
        {
            get { return _intSeqNo; }
            set { if (this._intSeqNo != value) { _intSeqNo = value; } }
        }          
     

        private string _strLanguageId;
        /// <summary>
        /// LanguageId
        /// </summary>
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }
      
       
        private string _strCurrencyName;
        /// <summary>
        /// CurrencyName
        /// </summary>
        public string CurrencyName
        {
            get { return _strCurrencyName; }
            set { if (this._strCurrencyName != value) { _strCurrencyName = value; } }
        }

        private string _strISOName;
        /// <summary>
        /// ISO Name
        /// </summary>
        public string ISOName
        {
            get { return _strISOName; }
            set { if (this._strISOName != value) { _strISOName = value; } }
        }


        private string _strCurrencySymbol;
        /// <summary>
        /// ISO Name
        /// </summary>
        public string CurrencySymbol
        {
            get { return _strCurrencySymbol; }
            set { if (this._strCurrencySymbol != value) { _strCurrencySymbol = value; } }
        }


        private string _strIsActive;
        /// <summary>
        /// IsActive
        /// </summary>
        public string IsActive
        {
            get { return _strIsActive; }
            set { if (this._strIsActive != value) { _strIsActive = value; } }
        }

       /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get=0,
            Activate=1,
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll=0,
            BulkActivate=1,
            BulkDeActivate = 2,
            GetAllCatalog

        }

      

    }
}
