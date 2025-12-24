/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author: SHaileSH
* Created:<27/11/09>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class ReportParameterGroup : BaseEntity
    {
        public ReportParameterGroup()
        {
            _listReportParameters = new List<ReportParameters>();
        }

        private string _strParameterGroupName;
        /// <summary>
        /// Parameter Name
        /// </summary>
        public string Name
        {
            get { return _strParameterGroupName; }
            set { if (this._strParameterGroupName != value) { _strParameterGroupName = value; } }
        }

        private int _iSortOrder;
        /// <summary>
        /// Sort Order
        /// </summary>
        public Int32 SortOrder
        {
            get { return _iSortOrder; }
            set { _iSortOrder = value; }
        }

        private string _strNextCondition;
        /// <summary>
        /// Next Condition
        /// </summary>
        public string NextCondition
        {
            get { return _strNextCondition; }
            set { if (this._strNextCondition != value) { _strNextCondition = value; } }
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

        private List<ReportParameters> _listReportParameters;
        /// <summary>
        /// ReportParameter List
        /// </summary>
        public List<ReportParameters> ReportParameterList
        {
            get { return _listReportParameters; }
        }
    }
}