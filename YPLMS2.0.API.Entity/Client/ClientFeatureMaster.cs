/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:
* Created:<24/1/2013>
* Last Modified:<24/1/2013>
*/
using System;
using System.Collections.Generic;
using System.Xml;
using System.Data.SqlClient;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// Serializable class Client : BaseEntity    
    /// </summary>
    [Serializable]
    public class ClientFeatureMaster : BaseEntity
    {

        private string _strFeatureName;
        /// <summary>
        /// FeatureName
        /// </summary>
        public string FeatureName
        {
            get { return _strFeatureName; }
            set { if (this._strFeatureName != value) { _strFeatureName = value; } }
        }

        public new enum Method
        {
            Add,
            Update,
            Get
           
        }
        /// <summary>
        /// List method enum
        /// </summary>
        public new enum ListMethod
        {
            GetAll
        }

   }
}