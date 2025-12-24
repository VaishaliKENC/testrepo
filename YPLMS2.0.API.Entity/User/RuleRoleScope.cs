/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh
* Created:<24/10/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
   public class RuleRoleScope:BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public RuleRoleScope()
        { }

        private string _ruleId;
        public string RuleId
        {
            get { return _ruleId; }
            set { if (this._ruleId != value) { _ruleId = value; } }
        }


        private string _roleId;
        public string RoleId
        {
            get { return _roleId; }
            set { if (this._roleId != value) { _roleId = value; } }
        }


        private string _roleName;
        public string RoleName
        {
            get { return _roleName; }
            set { if (this._roleName != value) { _roleName = value; } }
        }

        private string _ruleName;
        public string RuleName
        {
            get { return _ruleName; }
            set { if (this._ruleName != value) { _ruleName = value; } }
        }

        private string _ruleScope;
        public string RuleScope
        {
            get { return _ruleScope; }
            set { if (this._ruleScope != value) { _ruleScope = value; } }
        }

        private string _ruleDescription;
        public string RuleDescription
        {
            get { return _ruleDescription; }
            set { if (this._ruleDescription != value) { _ruleDescription = value; } }
        }


        private string _scopeUnitId;
        public string ScopeUnitId
        {
            get { return _scopeUnitId; }
            set { if (this._scopeUnitId != value) { _scopeUnitId = value; } }
        }


        private string _scopeLevelId;
        public string ScopeLevelId
        {
            get { return _scopeLevelId; }
            set { if (this._scopeLevelId != value) { _scopeLevelId = value; } }
        }


        private string _scopeRuleId;
        public string ScopeRuleId
        {
            get { return _scopeRuleId; }
            set { if (this._scopeRuleId != value) { _scopeRuleId = value; } }
        }


        private bool _isDefaultScope;
        public bool IsDefaultScope
        {
            get { return _isDefaultScope; }
            set { if (this._isDefaultScope != value) { _isDefaultScope = value; } }
        }

        private bool _IsTCAAdminRights;
        public bool IsTCAAdminRights
        {
            get { return _IsTCAAdminRights; }
            set { if (this._IsTCAAdminRights != value) { _IsTCAAdminRights = value; } }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
        }

        private DataAction _enumAction;
        /// <summary>
        /// Action for User
        /// </summary>
        public DataAction UserAction
        {
            get { return _enumAction; }
            set { if (this._enumAction != value) { _enumAction = value; } }
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            InRole,
            NotInRole,
            GetListAllByAllRoleList
        }

    }
}