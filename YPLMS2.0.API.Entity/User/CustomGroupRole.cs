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
namespace YPLMS2._0.API.Entity
{
    public class CustomGroupRole:CustomGroup
    {
        public CustomGroupRole()
        { }

        /// <summary>
        /// ENUM Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete
        }
        /// <summary>
        /// List method ENUM
        /// </summary>
        public new enum ListMethod
        {
            GetCustomGroupsInRole,
            GetCustomGroupsNotInRole
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

        private string _strRoleId;
        /// <summary>
        /// Role Id
        /// </summary>
        public string RoleId
        {
            get { return _strRoleId; }
            set { if (this._strRoleId != value) { _strRoleId = value; } }
        }

        private string _strScopeCustomGroupId;
        /// <summary>
        /// Custom Group Id
        /// </summary>
        public string ScopeCustomGroupId
        {
            get { return _strScopeCustomGroupId; }
            set { if (this._strScopeCustomGroupId != value) { _strScopeCustomGroupId = value; } }
        }

        private string _strScopeUnitId;
        /// <summary>
        /// Unit Id
        /// </summary>
        public string ScopeUnitId
        {
            get { return _strScopeUnitId; }
            set { if (this._strScopeUnitId != value) { _strScopeUnitId = value; } }
        }

        private string _strScopeLevelId;
        /// <summary>
        /// Level Id
        /// </summary>
        public string ScopeLevelId
        {
            get { return _strScopeLevelId; }
            set { if (this._strScopeLevelId != value) { _strScopeLevelId = value; } }
        }

        private bool _bIsDefaultScope;
        /// <summary>
        /// IsDefaultScope
        /// </summary>
        public bool IsDefaultScope
        {
            get { return _bIsDefaultScope; }
            set { if (this._bIsDefaultScope != value) { _bIsDefaultScope = value; } }
        }

    }
}
