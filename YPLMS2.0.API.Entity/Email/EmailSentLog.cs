/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author: Shailesh Patil
* Created: 24/10/09
* Last Modified:<dd/mm/yy>
*/
using System;

namespace YPLMS2._0.API.Entity
{
   [Serializable] public class EmailSentLog: BaseEntity
    {
        /// <summary>
        /// Default Contructor fro EmailSentLog
        /// <summary>
        public EmailSentLog()
        {
            
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add
        }

        /// <summary>
        /// Email Delivery Instance Id
        /// </summary>
        private string _strEmailDeliveryInstanceId;
        public string EmailDeliveryInstanceId
        {
            get { return _strEmailDeliveryInstanceId; }
            set { if (this._strEmailDeliveryInstanceId != value) { _strEmailDeliveryInstanceId = value; } }
        }

        /// <summary>
        /// Auto Email Event Id
        /// </summary>
        private string _strAutoEmailEventId;
        public string AutoEmailEventId
        {
            get { return _strAutoEmailEventId; }
            set { if (this._strAutoEmailEventId != value) { _strAutoEmailEventId = value; } }
        }

        /// <summary>
        /// Recipiant Email Address
        /// </summary>
        private string _strRecipiantEmailAddress;
        public string RecipiantEmailAddress
        {
            get { return _strRecipiantEmailAddress; }
            set { if (this._strRecipiantEmailAddress != value) { _strRecipiantEmailAddress = value; } }
        }

        /// <summary>
        /// Date Sent
        /// </summary>
        private DateTime _dtDateSent;
        public DateTime DateSent
        {
            get { return _dtDateSent; }
            set { if (this._dtDateSent != value) { _dtDateSent = value; } }
        }

        /// <summary>
        /// Recipiant CC Email Address
        /// </summary>
        private string _strRecipiantCCEmailAddress;
        public string RecipiantCCEmailAddress
        {
            get { return _strRecipiantCCEmailAddress; }
            set { if (this._strRecipiantCCEmailAddress != value) { _strRecipiantCCEmailAddress = value; } }
        }

        /// <summary>
        /// Recipiant BCC Email Address
        /// </summary>
        private string _strRecipiantBCCEmailAddress;
        public string RecipiantBCCEmailAddress
        {
            get { return _strRecipiantBCCEmailAddress; }
            set { if (this._strRecipiantBCCEmailAddress != value) { _strRecipiantBCCEmailAddress = value; } }
        }

        /// <summary>
        /// Email Template Id
        /// </summary>
        private string _strEmailTemplateId;
        public string EmailTemplateId
        {
            get { return _strEmailTemplateId; }
            set { if (this._strEmailTemplateId != value) { _strEmailTemplateId = value; } }
        }

        /// <summary>
        /// Email Msg File Name Path
        /// </summary>
        private string _strEmailMsgFileNamePath;
        public string EmailMsgFileNamePath
        {
            get { return _strEmailMsgFileNamePath; }
            set { if (this._strEmailMsgFileNamePath != value) { _strEmailMsgFileNamePath = value; } }
        }

        /// <summary>
        /// EmailTemplate
        /// </summary>
        private EmailTemplate _emailTemplate;
        public EmailTemplate EmailTemplate
        {
            get { return _emailTemplate; }
            set { if (this._emailTemplate != value) { _emailTemplate = value; } }
        }

        /// <summary>
        /// EmailDeliveryDashboard
        /// </summary>
        private EmailDeliveryDashboard _emailDeliveryDashboard;
        public EmailDeliveryDashboard EmailDeliveryDashboard
        {
            get { return _emailDeliveryDashboard; }
            set { if (this._emailDeliveryDashboard != value) { _emailDeliveryDashboard = value; } }
        }

        private string _strEmailLog;
        public string EmailLog
        {
            get { return _strEmailLog; }
            set { if (this._strEmailLog != value) { _strEmailLog = value; } }
        }

        private string _strSystemUserGuId;
        public string SystemUserGuId
        {
            get { return _strSystemUserGuId; }
            set { if (this._strSystemUserGuId != value) { _strSystemUserGuId = value; } }
        }


    }
}