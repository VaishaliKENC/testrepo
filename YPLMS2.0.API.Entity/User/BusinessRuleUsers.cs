/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh
* Created:01/10/09
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{

   [Serializable] public class BusinessRuleUsers : Learner
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public BusinessRuleUsers()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetResult,
            GetMultiRuleResult,
            GetBusinessRuleActiveUsers
        }
      
       
        private string _businessRuleId;
        public string BusinessRuleId
        {
            get { return _businessRuleId; }
            set { if (this._businessRuleId != value) { _businessRuleId = value; } }
        }

        private string _parameterGroupId;
        public string ParameterGroupId
        {
            get { return _parameterGroupId; }
            set { if (this._parameterGroupId != value) { _parameterGroupId = value; } }
        }

     

        private bool _bIsIncluded;
        /// <summary>
        ///  Is Included
        /// </summary>
        public bool IsIncluded
        {
            get { return _bIsIncluded; }
            set { if (this._bIsIncluded != value) { _bIsIncluded = value; } }
        }

        private string _userOrgHierarchy;
        public string UserOrgHierarchy
        {
            get { return _userOrgHierarchy; }
            set { if (this._userOrgHierarchy != value) { _userOrgHierarchy = value; } }
        }
    }
}
