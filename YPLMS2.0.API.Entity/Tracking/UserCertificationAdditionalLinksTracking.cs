/* 
* Copyright Brainvisa Technologies Pvt. Ltd.
* This source file and source code is proprietary property of Brainvisa Technologies Pvt. Ltd. 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Brainvisa’s Client.
* Author:<Ashish>
* Created:<06/10/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    ///  UserCertificationAdditionalLinksTracking
    /// </summary>
    [Serializable]
    public class UserCertificationAdditionalLinksTracking : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public UserCertificationAdditionalLinksTracking()
        { }

        public new enum ListMethod
        {
            GetAll
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add
        }

        private string _strLinkId;
        /// <summary>
        /// Link Id
        /// </summary>
        public string LinkId
        {
            get { return _strLinkId; }
            set { if (this._strLinkId != value) { _strLinkId = value; } }
        }

        private string _certificationId;
        public string CertificationId
        {
            get { return _certificationId; }
            set { if (this._certificationId != value) { _certificationId = value; } }
        }

        private string _strSystemUserGUID;
        /// <summary>
        /// User ID
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private ActivityCompletionStatus _completionStatus;
        public ActivityCompletionStatus CompletionStatus
        {
            get { return _completionStatus; }
            set { if (this._completionStatus != value) { _completionStatus = value; } }
        }

        private Nullable<DateTime> _dateOfCompletion;
        /// <summary>
        /// Date of Completion
        /// </summary>
        public Nullable<DateTime> DateOfCompletion
        {
            get { return _dateOfCompletion; }
            set { if (this._dateOfCompletion != value) { _dateOfCompletion = value; } }
        }
    }
}