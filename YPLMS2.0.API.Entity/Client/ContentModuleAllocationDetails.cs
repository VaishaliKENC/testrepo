/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Tanveer
* Created:<24/09/09>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ContentModuleAllocationDetails:BaseEntity 
    /// </summary>
   [Serializable] public class ContentModuleAllocationDetails : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        ///</summary>

         public ContentModuleAllocationDetails()
         {}

         /// <summary>
         /// enum Method
         /// </summary>
         public new enum Method
        {            
            Get,
            Add,
            Update,
            Delete,
            UpdateAtSiteAdmin,
            GetAtSiteAdmin

        }

         /// <summary>
         /// enum ListMethod
         /// </summary>
         public new enum ListMethod
         {
            GetAllByClientID
         }

         private string _MasterClientId;
         public string MasterClientId
         {
             get { return _MasterClientId; }
             set { if (this._MasterClientId != value) { _MasterClientId = value; } }
         }

         private string _LicenseId;
         public string LicenseId
        {
            get { return _LicenseId; }
            set { if (this._LicenseId != value) { _LicenseId = value; } }
        }

         private string _ContentModuleId;
         public string ContentModuleId
        {
            get { return _ContentModuleId; }
            set { if (this._ContentModuleId != value) { _ContentModuleId = value; } }
        }

         private string _ContentModuleEnglishName;
         public string ContentModuleEnglishName
         {
             get { return _ContentModuleEnglishName; }
             set { if (this._ContentModuleEnglishName != value) { _ContentModuleEnglishName = value; } }
         }

         private bool _IsDefault;
         public bool IsDefault
         {
             get { return _IsDefault; }
             set { if (this._IsDefault != value) { _IsDefault = value; } }
         }

         private bool _bIsAssignmentBasedOnHireDate;
         public bool IsAssignmentBasedOnHireDate
         {
             get { return _bIsAssignmentBasedOnHireDate; }
             set { if (this._bIsAssignmentBasedOnHireDate != value) { _bIsAssignmentBasedOnHireDate = value; } }
         }

         private bool _bIsAssignmentBasedOnCreationDate;
         public bool IsAssignmentBasedOnCreationDate
         {
             get { return _bIsAssignmentBasedOnCreationDate; }
             set { if (this._bIsAssignmentBasedOnCreationDate != value) { _bIsAssignmentBasedOnCreationDate = value; } }
         }

         private int _iAssignAfterDaysOf;
         public int AssignAfterDaysOf
         {
             get { return _iAssignAfterDaysOf; }
             set { if (this._iAssignAfterDaysOf != value) { _iAssignAfterDaysOf = value; } }
         }

         private DateTime _assignmentDateSet;
         public DateTime AssignmentDateSet
         {
             get { return _assignmentDateSet; }
             set { if (this._assignmentDateSet != value) { _assignmentDateSet = value; } }
         }

         private bool _bIsNoDueDate;
         public bool IsNoDueDate
         {
             get { return _bIsNoDueDate; }
             set { if (this._bIsNoDueDate != value) { _bIsNoDueDate = value; } }
         }

         private bool _bIsDueBasedOnAssignDate;
         public bool IsDueBasedOnAssignDate
         {
             get { return _bIsDueBasedOnAssignDate; }
             set { if (this._bIsDueBasedOnAssignDate != value) { _bIsDueBasedOnAssignDate = value; } }
         }

         private bool _bIsDueBasedOnHireDate;
         public bool IsDueBasedOnHireDate
         {
             get { return _bIsDueBasedOnHireDate; }
             set { if (this._bIsDueBasedOnHireDate != value) { _bIsDueBasedOnHireDate = value; } }
         }

         private bool _bIsDueBasedOnCreationDate;
         public bool IsDueBasedOnCreationDate
         {
             get { return _bIsDueBasedOnCreationDate; }
             set { if (this._bIsDueBasedOnCreationDate != value) { _bIsDueBasedOnCreationDate = value; } }
         }

         private bool _bIsDueBasedOnStartDate;
         public bool IsDueBasedOnStartDate
         {
             get { return _bIsDueBasedOnStartDate; }
             set { if (this._bIsDueBasedOnStartDate != value) { _bIsDueBasedOnStartDate = value; } }
         }
         private int _iDueAfterDaysOf;
         public int DueAfterDaysOf
         {
             get { return _iDueAfterDaysOf; }
             set { if (this._iDueAfterDaysOf != value) { _iDueAfterDaysOf = value; } }
         }

         private DateTime _dateDueSet;
         public DateTime DueDateSet
         {
             get { return _dateDueSet; }
             set { if (this._dateDueSet != value) { _dateDueSet = value; } }
         }

         private bool _bIsNoExpiryDate;
         public bool IsNoExpiryDate
         {
             get { return _bIsNoExpiryDate; }
             set { if (this._bIsNoExpiryDate != value) { _bIsNoExpiryDate = value; } }
         }

         private bool _bIsExpiryBasedOnAssignDate;
         public bool IsExpiryBasedOnAssignDate
         {
             get { return _bIsExpiryBasedOnAssignDate; }
             set { if (this._bIsExpiryBasedOnAssignDate != value) { _bIsExpiryBasedOnAssignDate = value; } }
         }

         private bool _bIsExpiryBasedOnStartDate;
         public bool IsExpiryBasedOnStartDate
         {
             get { return _bIsExpiryBasedOnStartDate; }
             set { if (this._bIsExpiryBasedOnStartDate != value) { _bIsExpiryBasedOnStartDate = value; } }
         }

         private bool _bIsExpiryBasedOnDueDate;
         public bool IsExpiryBasedOnDueDate
         {
             get { return _bIsExpiryBasedOnDueDate; }
             set { if (this._bIsExpiryBasedOnDueDate != value) { _bIsExpiryBasedOnDueDate = value; } }
         }

         private int _iExpireAfterDaysOf;
         public int ExpireAfterDaysOf
         {
             get { return _iExpireAfterDaysOf; }
             set { if (this._iExpireAfterDaysOf != value) { _iExpireAfterDaysOf = value; } }
         }

         private DateTime _dateExpirySet;
         public DateTime ExpiryDateSet
         {
             get { return _dateExpirySet; }
             set { if (this._dateExpirySet != value) { _dateExpirySet = value; } }
         }

    }
}