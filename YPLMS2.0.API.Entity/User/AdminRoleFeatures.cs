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
    /// AdminRoleFeatures class
    /// </summary>
   [Serializable] public class AdminRoleFeatures : BaseEntity
    {
        private string _strRoleId;
        /// <summary>
        /// Role Id
        /// </summary>
        public string RoleId
        {
            get { return _strRoleId; }
            set { if (this._strRoleId != value) { _strRoleId = value; } }
        }

        private string _strAdminFeatureId;
        /// <summary>
        /// Admin Feature Id
        /// </summary>
        public string AdminFeatureId
        {
            get { return _strAdminFeatureId; }
            set { if (this._strAdminFeatureId != value) { _strAdminFeatureId = value; } }
        }

        private bool _bCanView;
        /// <summary>
        /// To check Can View rights
        /// </summary>
        public bool CanView
        {
            get { return _bCanView; }
            set { if (this._bCanView != value) { _bCanView = value; } }
        }

        private bool _bCanAdd;
        /// <summary>
        /// To check Can add rights
        /// </summary>
        public bool CanAdd
        {
            get { return _bCanAdd; }
            set { if (this._bCanAdd != value) { _bCanAdd = value; } }
        }

        private bool _bCanEdit;
        /// <summary>
        /// To check Can edit rights
        /// </summary>
        public bool CanEdit
        {
            get { return _bCanEdit; }
            set { if (this._bCanEdit != value) { _bCanEdit = value; } }
        }

        private bool _bCanDelete;
        /// <summary>
        /// To check Can delete rights
        /// </summary>
        public bool CanDelete
        {
            get { return _bCanDelete; }
            set { if (this._bCanDelete != value) { _bCanDelete = value; } }
        }

        private bool _bCanPrint;
        /// <summary>
        /// To check Can print rights
        /// </summary>
        public bool CanPrint
        {
            get { return _bCanPrint; }
            set { if (this._bCanPrint != value) { _bCanPrint = value; } }
        }

        private bool _bCanExport;
        /// <summary>
        /// To check Can export rights
        /// </summary>
        public bool CanExport
        {
            get { return _bCanExport; }
            set { if (this._bCanExport != value) { _bCanExport = value; } }
        }
        private bool _bCanImport;
        /// <summary>
        /// To check Can Import rights
        /// </summary>
        public bool CanImport
        {
            get { return _bCanImport; }
            set { if (this._bCanImport != value) { _bCanImport = value; } }
        }
        private bool _bCanEmail;
        /// <summary>
        /// To check Can Email rights
        /// </summary>
        public bool CanEmail
        {
            get { return _bCanEmail; }
            set { if (this._bCanEmail != value) { _bCanEmail = value; } }
        }
        private bool _bCanCopy;
        /// <summary>
        /// To check Can Copy rights
        /// </summary>
        public bool CanCopy
        {
            get { return _bCanCopy; }
            set { if (this._bCanCopy != value) { _bCanCopy = value; } }
        }
        private bool _bCanActivate;
        /// <summary>
        /// To check Can Activate rights
        /// </summary>
        public bool CanActivate
        {
            get { return _bCanActivate; }
            set { if (this._bCanActivate != value) { _bCanActivate = value; } }
        }
        private bool _bCanDeactivate;
        /// <summary>
        /// To check Can Deactivate rights
        /// </summary>
        public bool CanDeactivate
        {
            get { return _bCanDeactivate; }
            set { if (this._bCanDeactivate != value) { _bCanDeactivate = value; } }
        }
        private bool _bCanUpload;
        /// <summary>
        /// To check Can Upload rights
        /// </summary>
        public bool CanUpload
        {
            get { return _bCanUpload; }
            set { if (this._bCanUpload != value) { _bCanUpload = value; } }
        }       

        public bool Validate(bool pIsUpdate)
        {
           
            if (!pIsUpdate)
            {
                if (String.IsNullOrEmpty(CreatedById))
                    return false;
            }
         
            if (String.IsNullOrEmpty(RoleId))
                return false;

            if (String.IsNullOrEmpty(AdminFeatureId))
                return false;

            if (String.IsNullOrEmpty(LastModifiedById))
                return false;

            return true;
        }
    }
}