/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<12/23/2011>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class InstructorMaster : BaseEntity 
    /// </summary>
    /// 
    public class InstructorMaster : InstructorLanguage
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public InstructorMaster()
        { }


        private string _strInstructorEmail;
        /// <summary>
        /// Email
        /// </summary>
        public string InstructorEmail
        {
            get { return _strInstructorEmail; }
            set { if (this._strInstructorEmail != value) { _strInstructorEmail = value; } }
        }

        private Nullable<bool> _isActive;
        public Nullable<bool> IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
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
            UpdateLanguage,
            DeleteInstructorLanguage
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetInstructorLanguages,
            ActivateDeActivateStatus
            
        }
    }
}