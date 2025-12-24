/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:Abhay 
* Created:<12/26/2011>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class SupportResourceMaster : BaseEntity 
    /// </summary>
    /// 
    public class SupportResourceMaster : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public SupportResourceMaster()
        { }


        private string _strSupportResourceTypeId;
        /// <summary>
        /// SupportResourceTypeId
        /// </summary>
        public string SupportResourceTypeId
        {
            get { return _strSupportResourceTypeId; }
            set { if (this._strSupportResourceTypeId != value) { _strSupportResourceTypeId = value; } }
        }

        private string _strResourceName;
        /// <summary>
        /// ResourceName
        /// </summary>
        public string ResourceName
        {
            get { return _strResourceName; }
            set { if (this._strResourceName != value) { _strResourceName = value; } }
        }

        private string _strResourceDescription;
        /// <summary>
        /// ResourceDescription
        /// </summary>
        public string ResourceDescription
        {
            get { return _strResourceDescription; }
            set { if (this._strResourceDescription != value) { _strResourceDescription = value; } }
        }

        private string _strResourceFileName;
        /// <summary>
        /// ResourceFileName
        /// </summary>
        public string ResourceFileName
        {
            get { return _strResourceFileName; }
            set { if (this._strResourceFileName != value) { _strResourceFileName = value; } }
        }

        private string _supportResourceTypeName;
        /// <summary>
        /// ResourceFileName
        /// </summary>
        public string SupportResourceTypeName
        {
            get { return _supportResourceTypeName; }
            set { if (this._supportResourceTypeName != value) { _supportResourceTypeName = value; } }
        }

        private decimal _strSupportResourceCost;
        /// <summary>
        /// SupportResourceCost
        /// </summary>
        public decimal SupportResourceCost
        {
            get { return _strSupportResourceCost; }
            set { if (this._strSupportResourceCost != value) { _strSupportResourceCost = value; } }
        }

        private byte[] _supportResourceFile;
        /// <summary>
        /// File Content
        /// </summary>
        public byte[] SupportResourceFile
        {
            get { return _supportResourceFile; }
            set { if (this._supportResourceFile != value) { _supportResourceFile = value; } }
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