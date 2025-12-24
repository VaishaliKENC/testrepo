using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class ParticipantCompletionRequest : ParticipantDetails
    {

        // <summary>
        /// Default Contructor
        /// </summary>
        public ParticipantCompletionRequest()
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
            Delete
        }
        /// <summary>
        /// List method ENUM
        /// </summary>
        public new enum ListMethod
        {
            GetAll
        }


        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }       
        public string Response { get; set; }       
        public DateTime CompletionDate { get; set; }
        public DateTime RequestSentDate { get; set; }
        


    }

    public class ActivityAssignmentExt : ActivityAssignment
    {

        // <summary>
        /// Default Contructor
        /// </summary>
        public ActivityAssignmentExt()
        {
        }

        
        public bool IsURLLaunched { get; set; }
        public string ParticipantId { get; set; }
        public string CourseId { get; set; }
        public string TokenKey { get; set; }
        public string VendorCode { get; set; }
        public string SystemUserGUID { get; set; }

    }
}
