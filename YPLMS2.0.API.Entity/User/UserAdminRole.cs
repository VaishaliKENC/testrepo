/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh & Ashish
* Created:<13/07/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// UserAdminRole Class
    /// </summary>
    /// 
    [Serializable]
     public class UserAdminRole : BaseEntity
    {
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

        private string _strRoleName;
        /// <summary>
        /// Role Name
        /// </summary>
        public string RoleName
        {
            get { return _strRoleName; }
            set { if (this._strRoleName != value) { _strRoleName = value; } }
        }

        private string _strUserScope;
        /// <summary>
        /// Role Name
        /// </summary>
        public string UserScope
        {
            get { return _strUserScope; }
            set { if (this._strUserScope != value) { _strUserScope = value; } }
        }

        private string _strUserNameAlias;
        /// <summary>
        /// User Name Alias
        /// </summary>
        public string UserNameAlias
        {
            get { return _strUserNameAlias; }
            set { if (this._strUserNameAlias != value) { _strUserNameAlias = value; } }
        }

        private string _strFirstName;
        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName
        {
            get { return _strFirstName; }
            set { if (this._strFirstName != value) { _strFirstName = value; } }
        }       

        private string _strLastName;
        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName
        {
            get { return _strLastName; }
            set { if (this._strLastName != value) { _strLastName = value; } }
        }

        private string _strCustomGroupId;
        /// <summary>
        /// Custom Group Id
        /// </summary>
        public string CustomGroupId
        {
            get { return _strCustomGroupId; }
            set { if (this._strCustomGroupId != value) { _strCustomGroupId = value; } }
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

        private string _strUnitId;
        /// <summary>
        /// Unit Id
        /// </summary>
        public string UnitId
        {
            get { return _strUnitId; }
            set { if (this._strUnitId != value) { _strUnitId = value; } }
        }

        private string _strLevelId;
        /// <summary>
        /// Level Id
        /// </summary>
        public string LevelId
        {
            get { return _strLevelId; }
            set { if (this._strLevelId != value) { _strLevelId = value; } }
        }

        private bool _bIsRoleActive;
        /// <summary>
        /// Is Role Active
        /// </summary>
        public bool IsRoleActive
        {
            get { return _bIsRoleActive; }
            set { if (this._bIsRoleActive != value) { _bIsRoleActive = value; } }
        }

        private RoleType _roleType;
        /// <summary>
        /// Role Type Learner/Admin
        /// </summary>
        public RoleType AdminRoleType
        {
            get { return _roleType; }
            set { _roleType = value; }
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


        public bool Validate(bool pIsUpdate)
        {

            if (String.IsNullOrEmpty(ID))
                return false;
            if (String.IsNullOrEmpty(RoleId))
                return false;
            if (String.IsNullOrEmpty(LastModifiedById))
                return false;

            if (!pIsUpdate)
            {
                if (String.IsNullOrEmpty(CreatedById))
                    return false;
            }

            return true;
        }
    }
}