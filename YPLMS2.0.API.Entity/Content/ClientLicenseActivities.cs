/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Bharat
* Created:<12/04/13>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class ClientLicenseActivities : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public ClientLicenseActivities()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            AddAll,
            BulkDeleted
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
        //private string _LicenseId;
        ///// <summary>
        ///// License Id
        ///// </summary>
        //public string LicenseId
        //{
        //    get { return _LicenseId; }
        //    set { if (this._LicenseId != value) { _LicenseId = value; } }
        //}
        private string _activityId;
        /// <summary>
        /// License Name
        /// </summary>

        public string ActivityId
        {
            get { return _activityId; }
            set { if (this._activityId != value) { _activityId = value; } }
        }

        private string _activityTypeId;
        /// <summary>
        /// Unit Id
        /// </summary>
        public string ActivityTypeId
        {
            get { return _activityTypeId; }
            set { if (this._activityTypeId != value) { _activityTypeId = value; } }
        }
    }
}
