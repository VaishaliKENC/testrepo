/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh 
* Created:25/08/09
* Last Modified:25/08/09
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// MenuItems Class
    /// </summary>
   [Serializable] public class MenuItems : BaseEntity
    {
        public MenuItems()
        {

        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetYPLSMenu,
            DoesGroupAdminHaveAccessToModule,
            YPLSAdminHaveAccessToModule
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            UpdateStatus
        }

        private string _strFeatureId;
        /// <summary>
        /// FeatureId
        /// </summary>
        public string FeatureId
        {
            get { return _strFeatureId; }
            set { if (this._strFeatureId != value) { _strFeatureId = value; } }
        }

        private string _strMenuItemName;
        /// <summary>
        /// MenuItemEnglishName
        /// </summary>
        public string MenuItemName
        {
            get { return _strMenuItemName; }
            set { if (this._strMenuItemName != value) { _strMenuItemName = value; } }
        }

        private string _strParentMenuID;
        /// <summary>
        /// ParentMenuID
        /// </summary>
        public string ParentMenuID
        {
            get { return _strParentMenuID; }
            set { if (this._strParentMenuID != value) { _strParentMenuID = value; } }
        }

        private string _strPageFileURL;
        /// <summary>
        /// PageFileURL
        /// </summary>
        public string PageFileURL
        {
            get { return _strPageFileURL; }
            set { if (this._strPageFileURL != value) { _strPageFileURL = value; } }
        }

        private bool _bIsActive;
        /// <summary>
        /// Client Id
        /// </summary>
        public bool IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
        }

        private string _strLanguageId;
        /// <summary>
        /// Client Id
        /// </summary>
        /// 
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }

        private RoleType _roleTypeMenu;
        /// <summary>
        /// Menu Role Type Learner/Admin
        /// </summary>
        public RoleType MenuRoleType
        {
            get { return _roleTypeMenu; }
            set { _roleTypeMenu = value; }
        }

        private string _strSystemUserGUID;
        /// <summary>
        /// Client Id
        /// </summary>
        /// 
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private bool _bCanView;
        public bool CanView { get { return _bCanView; } set { _bCanView = value; } }

    }
}