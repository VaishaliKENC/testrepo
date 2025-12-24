/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Amit Singh
* Created:<24/10/2016>
* Last Modified:</  /  >
*/
using System;

namespace YPLMS2._0.API.Entity
{
    public class Chat : BaseEntity
    {
        public Chat()
        { }

        /// <summary>
        /// Method enum
        /// </summary>
        public new enum Method
        {
            Add,
            Get,
            Delete
        }

        /// <summary>
        /// List method enum
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkDelete
        }

        private string _FromUserId;
        /// <summary>
        /// FromUserId
        /// </summary>
        public string FromUserId
        {
            get { return _FromUserId; }
            set { if (this._FromUserId != value) { _FromUserId = value; } }
        }

        private string _ToUserId;
        /// <summary>
        /// ToUserId
        /// </summary>
        public string ToUserId
        {
            get { return _ToUserId; }
            set { if (this._ToUserId != value) { _ToUserId = value; } }
        }

        private string _FromUserNameAlias;
        /// <summary>
        /// FromUserNameAlias
        /// </summary>
        public string FromUserNameAlias
        {
            get { return _FromUserNameAlias; }
            set { if (this._FromUserNameAlias != value) { _FromUserNameAlias = value; } }
        }

        private string _ToUserNameAlias;
        /// <summary>
        /// ToUserNameAlias
        /// </summary>
        public string ToUserNameAlias
        {
            get { return _ToUserNameAlias; }
            set { if (this._ToUserNameAlias != value) { _ToUserNameAlias = value; } }
        }


        private string _SearchText;
        /// <summary>
        /// SearchText
        /// </summary>
        public string SearchText
        {
            get { return _SearchText; }
            set { if (this._SearchText != value) { _SearchText = value; } }
        }

        private string _strFileName;
        /// <summary>
        /// FileName
        /// </summary>
        public string FileName
        {
            get { return _strFileName; }
            set { if (this._strFileName != value) { _strFileName = value; } }
        }

        private DateTime _DateCreated;
        /// <summary>
        /// DateCreated
        /// </summary>
        public DateTime ChatDateCreated
        {
            get { return _DateCreated; }
            set { if (this._DateCreated != value) { _DateCreated = value; } }
        }

        private DateTime _fromDate;
        /// <summary>
        /// FromDate
        /// </summary>
        public DateTime FromDate
        {
            get { return _fromDate; }
            set { if (this._fromDate != value) { _fromDate = value; } }
        }

        private DateTime _toDate;
        /// <summary>
        /// ToDate
        /// </summary>
        public DateTime ToDate
        {
            get { return _toDate; }
            set { if (this._toDate != value) { _toDate = value; } }
        }

    }
}
