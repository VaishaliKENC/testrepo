/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Ashish
* Created:<24/09/09>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ContentModule:BaseEntity 
    /// </summary>
   [Serializable] public class ContentModuleAllocationLicensing : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        ///</summary>

        public ContentModuleAllocationLicensing()
        {
            _listCourses = new List<ContentModule>();
        }   

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {            
            Get,
            Add,
            Update,
            Delete,
            GetByName
        }
        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll            
        }      

        private string _strMasterClientId;
        /// <summary>
        /// Master Data Client Id
        /// </summary>
        public string MasterClientId
        {
            get { return _strMasterClientId; }
            set { if (this._strMasterClientId != value) { _strMasterClientId = value; } }
        }
        private string _strEnrollmentName;
        /// <summary>
        /// EnrollmentName
        /// </summary>
        public string EnrollmentName
        {
            get { return _strEnrollmentName; }
            set { if (this._strEnrollmentName != value) { _strEnrollmentName = value; } }
        }

        private string _strCourseName;
        /// <summary>
        /// CourseName
        /// </summary>
         public string CourseName
        {
            get { return _strCourseName; }
            set { if (this._strCourseName != value) { _strCourseName = value; } }
        }



         

        private bool _isDefault;
        /// <summary>
        /// IsDefault
        /// </summary>
        public bool IsDefault
        {
            get { return _isDefault; }
            set { if (this._isDefault != value) { _isDefault = value; } }
        }


        private bool _isActive;
        /// <summary>
        /// IsDefault
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
        }

        private LicensingType _allocationType;
        /// <summary>
        /// AllocationType
        /// </summary>
        public LicensingType AllocationType
        {
            get { return _allocationType; }
            set { if (this._allocationType != value) { _allocationType = value; } }
        }
        private Nullable<DateTime> _strAllocationDate;
        /// <summary>
        /// AllocationDate
        /// </summary>
        public Nullable<DateTime> AllocationDate
        {
            get { return _strAllocationDate; }
            set { if (this._strAllocationDate != value) { _strAllocationDate = value; } }
        }



        private Nullable<DateTime> _strExpiryDate;
        /// <summary>
        /// ExpiryDate
        /// </summary>
        public Nullable<DateTime> ExpiryDate
        {
            get { return _strExpiryDate; }
            set { if (this._strExpiryDate != value) { _strExpiryDate = value; } }
        }



        private double _iNumberOfLicenses;
        /// <summary>
        /// Number Of Licenses
        /// </summary>
        public double NumberOfLicenses
        {
            get { return _iNumberOfLicenses; }
            set { if (this._iNumberOfLicenses != value) { _iNumberOfLicenses = value; } }
        }
        private double _iLicensesConsumed;
        /// <summary>
        /// Number Of Licenses Consumed
        /// </summary>
        public double LicensesConsumed
        {
            get { return _iLicensesConsumed; }
            set { if (this._iLicensesConsumed != value) { _iLicensesConsumed = value; } }
        }

        private List<ContentModule> _listCourses;
        /// <summary>
        /// List of allocated Courses
        /// </summary>
        public List<ContentModule> Courses
        {
            get { return _listCourses; }
        }
        public enum LicensingType
        {
            Subscription,
            Enrollment
        }

        // Added on 13-apr-2010 to send UI values for default course assignment. 
        private AssignmentDates _clsAssignmentDates;
        public AssignmentDates AssignmentDates
        {
            get { return _clsAssignmentDates; }
            set { if (this._clsAssignmentDates != value) { _clsAssignmentDates = value; } }
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
                if (String.IsNullOrEmpty(EnrollmentName))
                    return false;
                       
            }         

            return true;
        }

    }
}