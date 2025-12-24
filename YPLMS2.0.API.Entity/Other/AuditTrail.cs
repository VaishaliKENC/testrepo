/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Kunal R. Khairnar 
* Created:<28/09/15>
* Last Modified:</  /  >
*/
using System;

namespace YPLMS2._0.API.Entity
{
    public class AuditTrail : BaseEntity
    {
        public AuditTrail()
        { }

        /// <summary>
        /// Method enum
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete
        }

        /// <summary>
        /// List method enum
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllArchive
        }

        private int _ActionId;
        /// <summary>
        /// ActionId
        /// </summary>
        public int ActionId
        {
            get { return _ActionId; }
            set { if (this._ActionId != value) { _ActionId = value; } }
        }

        private string _strEntityName;
        /// <summary>
        /// EntityName
        /// </summary>
        public string EntityName
        {
            get { return _strEntityName; }
            set { if (this._strEntityName != value) { _strEntityName = value; } }
        }

        private string _strSystemUserGUID;
        /// <summary>
        /// SystemUserGUID
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private string _strDescription;
        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get { return _strDescription; }
            set { if (this._strDescription != value) { _strDescription = value; } }
        }


        private string _strRecordID;
        /// <summary>
        /// RecordID
        /// </summary>
        public string RecordID
        {
            get { return _strRecordID; }
            set { if (this._strRecordID != value) { _strRecordID = value; } }
        }


        private DateTime _AuditTrailDate;
        /// <summary>
        /// AuditTrail Date
        /// </summary>
        public DateTime AuditTrailDate
        {
            get { return _AuditTrailDate; }
            set { if (this._AuditTrailDate != value) { _AuditTrailDate = value; } }
        }

        private string _strOldData;
        /// <summary>
        /// OldData
        /// </summary>
        public string OldData
        {
            get { return _strOldData; }
            set { if (this._strOldData != value) { _strOldData = value; } }
        }

        private string _strNewData;
        /// <summary>
        /// NewData
        /// </summary>
        public string NewData
        {
            get { return _strNewData; }
            set { if (this._strNewData != value) { _strNewData = value; } }
        }

        private string _ActionIds;
        /// <summary>
        /// ActionIds
        /// </summary>
        public string ActionIds
        {
            get { return _ActionIds; }
            set { if (this._ActionIds != value) { _ActionIds = value; } }
        }

        private long _strAuditTrailID;
        /// <summary>
        /// AuditTrailID
        /// </summary>
        public long AuditTrailID
        {
            get { return _strAuditTrailID; }
            set { if (this._strAuditTrailID != value) { _strAuditTrailID = value; } }
        }

        private string _strUserName;
        /// <summary>
        /// UserName
        /// </summary>
        public string UserName
        {
            get { return _strUserName; }
            set { if (this._strUserName != value) { _strUserName = value; } }
        }

        private string _strUserNameAlias;
        /// <summary>
        /// UserNameAlias
        /// </summary>
        public string UserNameAlias
        {
            get { return _strUserNameAlias; }
            set { if (this._strUserNameAlias != value) { _strUserNameAlias = value; } }
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

        private int _ArchiveId;
        /// <summary>
        /// ArchiveId
        /// </summary>
        public int ArchiveId
        {
            get { return _ArchiveId; }
            set { if (this._ArchiveId != value) { _ArchiveId = value; } }
        }

        private string _strArchiveById;
        /// <summary>
        /// ArchiveById
        /// </summary>
        public string ArchiveById
        {
            get { return _strArchiveById; }
            set { if (this._strArchiveById != value) { _strArchiveById = value; } }
        }

        private string _strArchiveBy;
        /// <summary>
        /// ArchiveBy
        /// </summary>
        public string ArchiveBy
        {
            get { return _strArchiveBy; }
            set { if (this._strArchiveBy != value) { _strArchiveBy = value; } }
        }

        private string _strArchiveXMLPath;
        /// <summary>
        /// ArchiveXMLPath
        /// </summary>
        public string ArchiveXMLPath
        {
            get { return _strArchiveXMLPath; }
            set { if (this._strArchiveXMLPath != value) { _strArchiveXMLPath = value; } }
        }

        private DateTime _ArchiveDate;
        /// <summary>
        /// ArchiveDate
        /// </summary>
        public DateTime ArchiveDate
        {
            get { return _ArchiveDate; }
            set { if (this._ArchiveDate != value) { _ArchiveDate = value; } }
        }

        private DateTime _ArchiveFromDate;
        /// <summary>
        /// ArchiveFromDate
        /// </summary>
        public DateTime ArchiveFromDate
        {
            get { return _ArchiveFromDate; }
            set { if (this._ArchiveFromDate != value) { _ArchiveFromDate = value; } }
        }

        private DateTime _ArchiveToDate;
        /// <summary>
        /// ArchiveToDate
        /// </summary>
        public DateTime ArchiveToDate
        {
            get { return _ArchiveToDate; }
            set { if (this._ArchiveToDate != value) { _ArchiveToDate = value; } }
        }

    }
}
