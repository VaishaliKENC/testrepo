/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:<Rajendra Yadav>
* Created:<05/10/09>
* Last Modified:<dd/mm/yy>
 * 
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    ///  class Task 
    /// </summary>
    [Serializable]


    public class EmailDistributionList : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public EmailDistributionList()
        {
            _entListBusinessRuleUsers = new List<BusinessRuleUsers>();
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            GetRuleUsers,
            GetByName,
            Add,
            Update,
            Delete,
            GetListUsers,
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            Search
        }
      
        /// <summary>
        /// Rule ID
        /// </summary>
        private string _strruleId;
        public string RuleId
        {
            get { return _strruleId; }
            set { if (this._strruleId != value) { _strruleId = value; } }
        }

        /// <summary>
        /// Rule Name
        /// </summary>
        private string _strruleName;
        public string RuleName
        {
            get { return _strruleName; }
            set { if (this._strruleName != value) { _strruleName = value; } }
        }

        /// <summary>
        /// Rule ID
        /// </summary>
        private string _strDistributionListTitle;
        public string DistributionListTitle
        {
            get { return _strDistributionListTitle; }
            set { if (this._strDistributionListTitle != value) { _strDistributionListTitle = value; } }
        }

        /// <summary>
        /// Is Active
        /// </summary>
        private Nullable<Boolean> _bisActive;
        public Nullable<Boolean> IsActive
        {
            get { return _bisActive; }
            set { if (this._bisActive != value) { _bisActive = value; } }
        }

        private bool _bIsPrivate;
        /// <summary>
        /// IsPrivate
        /// </summary>
        public bool IsPrivate
        {
            get { return _bIsPrivate; }
            set { if (this._bIsPrivate != value) { _bIsPrivate = value; } }
        }

        private List<BusinessRuleUsers> _entListBusinessRuleUsers;
        /// <summary>
        /// Business Rule Users
        /// </summary>
        public List<BusinessRuleUsers> BusinessRuleUsers
        {
            get { return _entListBusinessRuleUsers; }
            set { if (this._entListBusinessRuleUsers != value) { _entListBusinessRuleUsers = value; } }
        }

        private string _strListCount;
        /// <summary>
        /// List Count
        /// </summary>
        public string ListCount
        {
            get { return _strListCount; }
            set { if (this._strListCount != value) { _strListCount = value; } }
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