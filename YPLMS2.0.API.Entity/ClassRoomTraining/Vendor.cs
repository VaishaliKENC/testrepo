/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:Abhay Galande
* Created:<22-12-2011>
*/
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// Serializable class Session : BaseEntity    
    /// </summary>
    [Serializable]
    public class Vendor:BaseEntity
    {

        /// <summary>
        /// Method enum
        /// </summary>
        public new enum Method
        {
            Add,
            Update,
            Delete,
            Get
        }
        /// <summary>
        /// List method enum
        /// </summary>

        public new enum ListMethod
        {
            GetAll
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

        private string _strContactPerson;
        /// <summary>
        /// ContactPerson
        /// </summary>
        public string ContactPerson
        {
            get { return _strContactPerson; }
            set { if (this._strContactPerson != value) { _strContactPerson = value; } }
        }

        private string _strContactAddress;
        /// <summary>
        /// ContactAddress
        /// </summary>
        public string ContactAddress
        {
            get { return _strContactAddress; }
            set { if (this._strContactAddress != value) { _strContactAddress = value; } }
        }

        private string _strEmailId;
        /// <summary>
        /// EmailId
        /// </summary>
        public string EmailId
        {
            get { return _strEmailId; }
            set { if (this._strEmailId != value) { _strEmailId = value; } }
        }

        private string _strPhoneNo;
        /// <summary>
        /// PhoneNo
        /// </summary>
        public string PhoneNo
        {
            get { return _strPhoneNo; }
            set { if (this._strPhoneNo != value) { _strPhoneNo = value; } }
        }

        private string _strRemarks;
        /// <summary>
        /// Remarks
        /// </summary>
        public string Remarks
        {
            get { return _strRemarks; }
            set { if (this._strRemarks != value) { _strRemarks = value; } }
        }
    }
}
