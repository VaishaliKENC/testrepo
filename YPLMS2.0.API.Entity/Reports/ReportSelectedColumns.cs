/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author: SHaileSH
* Created:<30/11/09>
* Last Modified:
*/
using System;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
  public class ReportSelectedColumns : BaseEntity
    {
        public ReportSelectedColumns()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkAddUpdate,
            BulkAddUpdateColumnDisplayOrder,
            GetAllWithDetails,


        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get
        }

        private string _strReportColumnID;
        /// <summary>
        /// Report Column ID
        /// </summary>
        public string ReportColumnID
        {
            get { return _strReportColumnID; }
            set { if (this._strReportColumnID != value) { _strReportColumnID = value; } }
        }

        private string _strReportId;
        /// <summary>
        /// Report Id
        /// </summary>
        public string ReportId
        {
            get { return _strReportId; }
            set { if (this._strReportId != value) { _strReportId = value; } }
        }

        private bool _bIsSortBy;
        /// <summary>
        /// Is SortBy
        /// </summary>
        public bool IsSortBy
        {
            get { return _bIsSortBy; }
            set { if (this._bIsSortBy != value) { _bIsSortBy = value; } }
        }

        private bool _bIsGroupBy;
        /// <summary>
        /// Is GroupBy
        /// </summary>
        public bool IsGroupBy
        {
            get { return _bIsGroupBy; }
            set { if (this._bIsGroupBy != value) { _bIsGroupBy = value; } }
        }

        private bool _bIsFiltered;
        /// <summary>
        /// Is Filtered
        /// </summary>
        public bool IsFiltered
        {
            get { return _bIsFiltered; }
            set { if (this._bIsFiltered != value) { _bIsFiltered = value; } }
        }

        private bool _bIsConfigurable;
        /// <summary>
        /// Is Configurable
        /// </summary>
        public bool IsConfigurable
        {
            get { return _bIsConfigurable; }
            set { if (this._bIsConfigurable != value) { _bIsConfigurable = value; } }
        }

        private string _strFieldName;
        /// <summary>
        /// Field Name
        /// </summary>
        public string FieldName
        {
            get { return _strFieldName; }
            set { if (this._strFieldName != value) { _strFieldName = value; } }
        }
        // added by Gitanjali 2.11.2010
        private int _columnDisplayOrder;
        public int ColumnDisplayOrder
        {
            get { return _columnDisplayOrder; }
            set { if (this._columnDisplayOrder != value) { _columnDisplayOrder = value; } }
        }
        //added by Gitanjali 12.12.2010
        private string _fieldDataType;
        public string FieldDataType
        {
            get { return _fieldDataType; }
            set { if (this._fieldDataType != value) { _fieldDataType = value; } }
        }
    }
}