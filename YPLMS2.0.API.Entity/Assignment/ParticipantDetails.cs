using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class ParticipantDetails : BaseEntity
    {

        // <summary>
        /// Default Contructor
        /// </summary>
        public ParticipantDetails()
        {
        }

        /// <summary>
        /// ENUM Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete,
            RegisterUser,
            RegisterUser_ExtCourseLaunch
            
           
        }
        /// <summary>
        /// List method ENUM
        /// </summary>
        public new enum ListMethod
        {
            GetAll
        }
               
        public string EmailId { get; set; }
        public string ParticipantId { get; set; }
        public string SystemUserGUID { get; set; }
        public string CourseId { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RequestDate { get; set; }
        public string Password { get; set; }
        public string TokenKey { get; set; }
        public string Result { get; set; }
        public string VendorCode { get; set; }
    }
}
