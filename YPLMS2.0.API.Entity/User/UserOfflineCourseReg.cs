using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class UserOfflineCourseReg : BaseEntity
    {

        public string SystemUserGUID;
        public string CourseId;
        public string CourseKey;
        public string RegKey;
        public bool IsCompleted;
        public DateTime RegistrationDate;
        public DateTime ExpiryDate;
        public bool IsActive;
        public string CourseFileName;


        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete,
            GetIsCourseRegistered

        }
        /// <summary>
        /// List method ENUM
        /// </summary>
        public new enum ListMethod
        {
            GetAll

        }

    }
}
