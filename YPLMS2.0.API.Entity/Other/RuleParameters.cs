/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh
* Created:<09/09/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
     public class RuleParameters:BaseEntity
    {

        public RuleParameters()
        { }

        private string _strConditionId;
        /// <summary>
        /// Condition ID
        /// </summary>
        public string ConditionId
        {
            get { return _strConditionId; }
            set { if (this._strConditionId != value) { _strConditionId = value; } }
        }

        //-- Change string to int
        private int _iParameterName;
        /// <summary>
        /// Parameter Name
        /// </summary>
        public int ParameterName
        {
            get { return _iParameterName; }
            set { if (this._iParameterName != value) { _iParameterName = value; } }
        }

        private string _strLeftConditionId;
        /// <summary>
        /// LeftConditionId
        /// </summary>
        public string LeftConditionId
        {
            get { return _strLeftConditionId; }
            set { if (this._strLeftConditionId != value) { _strLeftConditionId = value; } }
        }

        private string _strLeftConditionValue;
        /// <summary>
        /// LeftConditionValue
        /// </summary>
        public string LeftConditionValue
        {
            get { return _strLeftConditionValue; }
            set { if (this._strLeftConditionValue != value) { _strLeftConditionValue = value; } }
        }

        private string _strCondition;
        /// <summary>
        /// Condition
        /// </summary>
        public string Condition
        {
            get { return _strCondition; }
            set { if (this._strCondition != value) { _strCondition = value; } }
        }

        private string _strRightConditionId;
        /// <summary>
        /// Right ConditionId 
        /// /// </summary>
        public string RightConditionId
        {
            get { return _strRightConditionId; }
            set { if (this._strRightConditionId != value) { _strRightConditionId = value; } }
        }

        private string _strRightConditionValue;
        /// <summary>
        /// Right Condition Value 
        /// /// </summary>
        public string RightConditionValue
        {
            get { return _strRightConditionValue; }
            set { if (this._strRightConditionValue != value) { _strRightConditionValue = value; } }
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

        private string _strRuleParameterGroupId;
        /// <summary>
        /// Rule Parameter GroupId
        /// </summary>
        public string RuleParameterGroupId
        {
            get { return _strRuleParameterGroupId; }
            set {  _strRuleParameterGroupId = value;  }
        }

        private ImportDefination.ValueType _parameterFieldType;
        /// <summary>
        /// Parameter Field Type
        /// </summary>
        public ImportDefination.ValueType ParameterFieldType
        {
            get { return _parameterFieldType; }
            set { _parameterFieldType = value; }
        }

        private string _strGroupType;
        /// <summary>
        /// Rule Parameter GroupType
        /// </summary>
        public string GroupType
        {
            get { return _strGroupType; }
            set { _strGroupType = value; }
        }
    }
}
