/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Tanveer 
* Created:<27/08/09>
* Last Modified:<27/08/09>
*/
using System;

namespace YPLMS2._0.API.Entity
{
   [Serializable] public class ReportPerformanceTracking : BaseEntity 
    {
        public ReportPerformanceTracking()
        { }

        /// <summary>
        /// Method enum
        /// </summary>
        public new enum Method
        {
            Add
        }

        private string _strUserId;
        /// <summary>
        /// Language Id
        /// </summary>
        public string UserId
        {
            get { return _strUserId; }
            set { if (this._strUserId != value) { _strUserId = value; } }
        }
         
        private string _strReportName;
        /// <summary>
        /// Title
        /// </summary>
        public string ReportName
        {
            get { return _strReportName; }
            set { if (this._strReportName != value) { _strReportName = value; } }
        }

    }
}
