/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh & Ashish
* Created:<13/07/09>
* Last Modified:<21/07/09>
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// Admin Role class
    /// </summary>
    [Serializable]
    public class AdminRole : BaseEntity
    {
        private List<AdminRoleFeatures> _entListFeatures;
        private List<RuleRoleScope> _entListSelectedRuleRoleScope;
        private List<UserAdminRole> _entListUsers;
        /// <summary>
        /// constant USER_SESSION_ID
        /// </summary>
        public const string SUPER_ADMIN_ROLE_ID = "ROL0006";
        public const string SITE_ADMIN_ROLE_ID = "ROL0005";
        public const string PROGRAM_ADMIN_ROLE_ID = "ROL0004";
        public const string REPORT_ADMIN_ROLE_ID = "ROL0003";
        public const string MORTGAGE_SITE_ADMIN_ROLE_ID = "ROLzj1IkG2y";
        public const string SME_ADMIN_ROLE_ID = "ROL0002";
        public const string PRODUCT_ADMIN_ROLE_ID = "ROL0007";
        public const string CONTENT_ADMIN_ROLE_ID = "ROL0008";
        public const string GRSI_TCA_ADMIN_ROLE_ID = "ROL0009";
        public const string GRSI_ILT_INSTRUCTOR_ROLE_ID = "ROL0010";
        /// <summary>
        /// Default Contructor
        /// </summary>
        public AdminRole()
        {
            _entListFeatures = new List<AdminRoleFeatures>();            
            _entListUsers = new List<UserAdminRole>();
            _entListSelectedRuleRoleScope = new List<RuleRoleScope>(); 
        }

        /// <summary>
        /// Admin Role enum method for add,edit,delete transactions
        /// </summary>
        public new enum Method
        {
            Get,
            GetRoleName,
            CheckRoleName,
            Add,
            Update,
            Delete,
            AssignUserRoles,
            AllUnAssignRoleToUser
        }

        /// <summary>
        ///  AdminRoles ListMethod enum 
        /// </summary>
        public new enum ListMethod
        {
            GetAllRoles,
            GetAllRolesForReport,
            GetAllRolesByActiveStatus,
            UpdateAllRoles,
            GetUserByRole,
            AddBulkRoles,
            GetAllUserByRule
            
        }

        /// <summary>
        /// list of AdminRoleFeatures
        /// </summary>
        public List<AdminRoleFeatures> Features
        {
            get { return _entListFeatures; }
        }
        
        /// <summary>
        /// List of RuleRoleScope object
        /// </summary>
        public List<RuleRoleScope> SelectedRuleRoleScope
        {
            get { return _entListSelectedRuleRoleScope; }
        }


        //private string _strRoleId;
        ///// <summary>
        ///// Role Name
        ///// </summary>
        //public string RoleId
        //{
        //    get { return _strRoleId; }
        //    set { if (this._strRoleId != value) { _strRoleId = value; } }
        //}

        private string _strRoleName;
        /// <summary>
        /// Role Name
        /// </summary>
        public string RoleName
        {
            get { return _strRoleName; }
            set { if (this._strRoleName != value) { _strRoleName = value; } }
        }

        private string _strRoleDescription;
        /// <summary>
        /// Role Description
        /// </summary>
        public string RoleDescription
        {
            get { return _strRoleDescription; }
            set { if (this._strRoleDescription != value) { _strRoleDescription = value; } }
        }       

        private bool _bIsActive;
        /// <summary>
        /// Is Active
        /// </summary>
        public bool IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
        }

        
        /// <summary>
        /// List id User Admin roles
        /// </summary>
        public List<UserAdminRole> Users
        {
            get
            {
                return _entListUsers;
            }
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
                if (String.IsNullOrEmpty(RoleName))
                    return false;
                if (String.IsNullOrEmpty(ClientId))
                    return false;
            }
            if (String.IsNullOrEmpty(LastModifiedById))
                return false;

            return true;
        }

    }
    public enum RoleType
    {
        Learner,
        Admin
    }

}