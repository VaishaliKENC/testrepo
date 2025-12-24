/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Ashish
* Created:<16/09/09>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class RuleParameterGroup:BaseEntity
    {

        public RuleParameterGroup()
        {
            _listRuleParameters = new List<RuleParameters>();
            _listBusinessRuleUsers = new List<BusinessRuleUsers>(); 
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
        /// SortOrder
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

        private string _strRuleId;
        /// <summary>
        /// Rule Id
        /// </summary>
        public string RuleId
        {
            get { return _strRuleId; }
            set { if (this._strRuleId != value) { _strRuleId = value; } }
        }

        private List<RuleParameters> _listRuleParameters;
        /// <summary>
        /// RuleParameter List
        /// </summary>
        public List<RuleParameters> RuleParameterList
        {
            get { return _listRuleParameters; }
        }

        private List<BusinessRuleUsers> _listBusinessRuleUsers;
        /// <summary>
        /// Business Rule Users List
        /// </summary>
        public List<BusinessRuleUsers> BusinessRuleUsersList
        {
            get { return _listBusinessRuleUsers; }
        }
    }
}