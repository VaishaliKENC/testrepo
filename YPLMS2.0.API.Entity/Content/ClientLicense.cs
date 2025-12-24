/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Bharat
* Created:<12/04/13>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class ClientLicense : BaseEntity
    {
        
        private List<ClientLicenseActivities> _entListClientLicenseActivities;
        /// <summary>
        /// Default Contructor      
        /// <summary>
        public ClientLicense()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            AddAll,
            GetAllActUpdated,
            GetAllActNotUpdated,
            BulkDeleted,
            GetActivityLicensesPreview
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,           
            Add,
            Update,
            Delete
        }
      
        private string _licenseName;
        /// <summary>
        /// License Name
        /// </summary>

        public string LicenseName
        {
            get { return _licenseName; }
            set { if (this._licenseName != value) { _licenseName = value; } }
        }

        private string _unitId;
        /// <summary>
        /// Unit Id
        /// </summary>
        public string UnitId
        {
            get { return _unitId; }
            set { if (this._unitId != value) { _unitId = value; } }
        }

        private string _unitName;
        /// <summary>
        /// Unit Name
        /// </summary>
        public string UnitName
        {
            get { return _unitName; }
            set { if (this._unitName != value) { _unitName = value; } }
        }

        private Nullable<Boolean> _isUnlimitedLicense;
        /// <summary>
        /// Is Unlimited License
        /// </summary>
        public Nullable<Boolean> IsUnlimitedLicense
        {
            get { return _isUnlimitedLicense; }
            set { if (this._isUnlimitedLicense != value) { _isUnlimitedLicense = value; } }
        }

        private int _licenseCount;
        /// <summary>
        /// License Count
        /// </summary>
        public int LicenseCount
        {
            get { return _licenseCount; }
            set { if (this._licenseCount != value) { _licenseCount = value; } }
        }

        private Nullable<DateTime> _licenseAllocationDate;
        /// <summary>
        /// License Allocation Date
        /// </summary>
        public Nullable<DateTime> LicenseAllocationDate
        {
            get { return _licenseAllocationDate; }
            set { if (this._licenseAllocationDate != value) { _licenseAllocationDate = value; } }
        }

        private Nullable<DateTime> _licenseExpiryDate;
        /// <summary>
        /// License Expiry Date
        /// </summary>
        public Nullable<DateTime> LicenseExpiryDate
        {
            get { return _licenseExpiryDate; }
            set { if (this._licenseExpiryDate != value) { _licenseExpiryDate = value; } }
        }

        private string _activityId;
        /// <summary>
        /// Activity ID
        /// </summary>

        public string ActivityID
        {
            get { return _activityId; }
            set { if (this._activityId != value) { _activityId = value; } }
        }

        private string _activityName;
        /// <summary>
        /// License Name
        /// </summary>

        public string ActivityName
        {
            get { return _activityName; }
            set { if (this._activityName != value) { _activityName = value; } }
        }
        private string _activityType;
        /// <summary>
        /// Activity Type
        /// </summary>

        public string ActivityType
        {
            get { return _activityType; }
            set { if (this._activityType != value) { _activityType = value; } }
        }

        private string _preview;
        /// <summary>
        /// Preview
        /// </summary>

        public string Preview
        {
            get { return _preview; }
            set { if (this._preview != value) { _preview = value; } }
        }

        private string _assessmentTime;
        /// <summary>
        /// Assessment Time
        /// </summary>

        public string AssessmentTime
        {
            get { return _assessmentTime; }
            set { if (this._assessmentTime != value) { _assessmentTime = value; } }
        }

        private string _assessmentAlertTime;
        /// <summary>
        /// Assessment Alert Time
        /// </summary>

        public string AssessmentAlertTime
        {
            get { return _assessmentAlertTime; }
            set { if (this._assessmentAlertTime != value) { _assessmentAlertTime = value; } }
        }

        private string _questionnaireType;
        /// <summary>
        /// Assessment Alert Time
        /// </summary>

        public string QuestionnaireType
        {
            get { return _questionnaireType; }
            set { if (this._questionnaireType != value) { _questionnaireType = value; } }
        }
        private int _licenseAllocatedCount;
        /// <summary>
        /// License Allocated Count
        /// </summary>
        public int LicenseAllocatedCount
        {
            get { return _licenseAllocatedCount; }
            set { if (this._licenseAllocatedCount != value) { _licenseAllocatedCount = value; } }
        }

        private int _count;
        /// <summary>
        /// License Allocated Count
        /// </summary>
        public int Count
        {
            get { return _count; }
            set { if (this._count != value) { _count = value; } }
        }
        private string _createdById;
        /// <summary>
        /// CreatedById
        /// </summary>

        public string CreatedById
        {
            get { return _createdById; }
            set { if (this._createdById != value) { _createdById = value; } }
        }

          public List<ClientLicenseActivities> AllClientLicense { get; set; }
    }
}
