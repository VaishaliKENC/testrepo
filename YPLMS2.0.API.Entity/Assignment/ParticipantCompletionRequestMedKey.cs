using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class ParticipantCompletionRequestMedKey : ParticipantDetails
    {

        // <summary>
        /// Default Contructor
        /// </summary>
        public ParticipantCompletionRequestMedKey()
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
        /// ENUM Method
        /// </summary>
        public new enum ScalarMethod
        {
            Get
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
        public string QueryCompletionStatus { get; set; }
        


    }
}
