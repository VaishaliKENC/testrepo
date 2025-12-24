/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh 
* Created:25/08/09
* Last Modified:25/08/09
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// MenuItems Class
    /// </summary>
   [Serializable] public class RegionView : BaseEntity
    {
        public RegionView()
        {

        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetName,
            GetByName,
            Add,
            Update,
            Delete
        }

        private string _strRegionViewName;
        /// <summary>
        /// RegionView Name
        /// </summary>
        public string RegionViewName
        {
            get { return _strRegionViewName; }
            set { if (this._strRegionViewName != value) { _strRegionViewName = value; } }
        }

        private string _strRegionViewDescription;
        /// <summary>
        /// RegionView Description
        /// </summary>
        public string RegionViewDescription
        {
            get { return _strRegionViewDescription; }
            set { if (this._strRegionViewDescription != value) { _strRegionViewDescription = value; } }
        }

        private string _strRuleId;
        /// <summary>
        /// RuleId
        /// </summary>
        public string RuleId
        {
            get { return _strRuleId; }
            set { if (this._strRuleId != value) { _strRuleId = value; } }
        }

        private bool _bIsUsed;
        /// <summary>
        /// IsUsed
        /// </summary>
        public bool IsUsed
        {
            get { return _bIsUsed; }
            set { if (this._bIsUsed != value) { _bIsUsed = value; } }
        }

      
    }
}