using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.Entity
{
    public class BusinessRuleUsersVM:LearnerVM
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public BusinessRuleUsersVM()
        { }

      

        private string? _businessRuleId;
        public string? BusinessRuleId
        {
            get { return _businessRuleId; }
            set { if (this._businessRuleId != value) { _businessRuleId = value; } }
        }

        private string? _parameterGroupId;
        public string? ParameterGroupId
        {
            get { return _parameterGroupId; }
            set { if (this._parameterGroupId != value) { _parameterGroupId = value; } }
        }



        private bool? _bIsIncluded;
        /// <summary>
        ///  Is Included
        /// </summary>
        public bool? IsIncluded
        {
            get { return _bIsIncluded; }
            set { if (this._bIsIncluded != value) { _bIsIncluded = value; } }
        }

        private string ?_userOrgHierarchy;
        public string? UserOrgHierarchy
        {
            get { return _userOrgHierarchy; }
            set { if (this._userOrgHierarchy != value) { _userOrgHierarchy = value; } }
        }
    }
}
