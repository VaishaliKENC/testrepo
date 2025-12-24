/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh
* Created:<21/09/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
   [Serializable] public class RSSFeedConfiguration : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public RSSFeedConfiguration()
        { }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetClientRSSUrl,
            Add,
            Update
        }
       
        private string _rSSFeedURL;
        /// <summary>
        /// RSS feed URL
        /// </summary>
        public string RSSFeedURL
        {
            get { return _rSSFeedURL; }
            set { if (this._rSSFeedURL != value) { _rSSFeedURL = value; } }
        }   
    }
}