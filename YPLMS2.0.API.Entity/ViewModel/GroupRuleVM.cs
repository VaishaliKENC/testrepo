using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.Entity
{
    public class GroupRuleVM: BaseEntityVM
    {
        public GroupRuleVM()
        {
            _listRuleParameterGroup = new List<RuleParameterGroup>();
            _listBusinessRuleUsers = new List<BusinessRuleUsers>();
        }


        private string? _strRuleName;
        /// <summary>
        /// Rule Name
        /// </summary>
        public string? RuleName
        {
            get { return _strRuleName; }
            set { if (this._strRuleName != value) { _strRuleName = value; } }
        }

        private string? _strRuleDesc;
        /// <summary>
        /// RuleDerscription
        /// </summary>
        public string? RuleDescription
        {
            get { return _strRuleDesc; }
            set { if (this._strRuleDesc != value) { _strRuleDesc = value; } }
        }

        private bool? _bIsActive;
        /// <summary>
        /// Is Active
        /// </summary>
        public bool? IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
        }

        private bool? _bIsForDistributionList;
        /// <summary>
        /// Is Active
        /// </summary>
        public bool? IsForDistributionList
        {
            get { return _bIsForDistributionList; }
            set { if (this._bIsForDistributionList != value) { _bIsForDistributionList = value; } }
        }


        private List<RuleParameterGroup>? _listRuleParameterGroup;
        /// <summary>
        /// RuleParameterGroup List
        /// </summary>
        public List<RuleParameterGroup>? RuleParameterGroupList
        {
            get { return _listRuleParameterGroup; }
        }





        private List<BusinessRuleUsers>? _listBusinessRuleUsers;
        /// <summary>
        /// BusinessRuleUsers List
        /// </summary>
        public List<BusinessRuleUsers>? BusinessRuleUsers
        {
            get { return _listBusinessRuleUsers; }
        }

        private bool? _bIsUsed;
        /// <summary>
        /// IsUsed
        /// </summary>
        public bool? IsUsed
        {
            get { return _bIsUsed; }
            set { if (this._bIsUsed != value) { _bIsUsed = value; } }
        }

        private string? _ruleParentId;

        public string? RuleParentId
        {
            get { return _ruleParentId; }
            set { if (this._ruleParentId != value) { _ruleParentId = value; } }
        }

        private string? _ruleSystemUserGUID;

        public string? ruleSystemUserGUID
        {
            get { return _ruleSystemUserGUID; }
            set { if (this._ruleSystemUserGUID != value) { _ruleSystemUserGUID = value; } }
        }


    }
}

