/* 
* Copyright Brainvisa Technologies Pvt. Ltd.
* This source file and source code is proprietary property of Brainvisa Technologies Pvt. Ltd. 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Brainvisa’s Client.
* Author:Fattesinh & Ashish
* Created:<13/07/09>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// Custom Group Class
    /// </summary>
    public class CustomGroup : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public CustomGroup()
        {
            _entListCustomGroupUsers = new List<CustomGroupUser>();
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAllCustomGroups,
            BulkUpdate
        }
        /// <summary>
        /// ENUM method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete,
            AssignCustomGroupToUsers
        }

        private List<CustomGroupUser> _entListCustomGroupUsers;
        /// <summary>
        /// List of Custom Group Users
        /// </summary>
        public List<CustomGroupUser> CustomGroupUsers
        {
            get { return _entListCustomGroupUsers; }
        }

        private string _strCustomGroupName;
        /// <summary>
        /// Custom Group Name
        /// </summary>
        public string CustomGroupName
        {
            get { return _strCustomGroupName; }
            set { if (this._strCustomGroupName != value) { _strCustomGroupName = value; } }
        }

        private bool _bIsActive;
        /// <summary>
        /// Is sActive
        /// </summary>
        public bool IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
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

        public bool Validate(bool pIsUpdate)
        {

            if (pIsUpdate)
            {
                if (String.IsNullOrEmpty(ID))
                    return false;
                
            }
            else
            {
                if (String.IsNullOrEmpty(CreatedById))
                    return false;

                if (String.IsNullOrEmpty(CustomGroupName))
                    return false;

                if (String.IsNullOrEmpty(ClientId))
                    return false;
            }

            if (String.IsNullOrEmpty(LastModifiedById))
                return false;

            return true;
        }

    }
}