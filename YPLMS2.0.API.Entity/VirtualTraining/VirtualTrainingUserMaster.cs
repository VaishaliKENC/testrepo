using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class VirtualTrainingUserMaster : BaseEntity
    {
        public VirtualTrainingUserMaster()
        { }

        private string _strVirtualSystemUserid;
        /// <summary>
        /// _strVirtualSystemUserid
        /// </summary>
        public string VirtualSystemUserid
        {
            get { return _strVirtualSystemUserid; }
            set { if (this._strVirtualSystemUserid != value) { _strVirtualSystemUserid = value; } }
        }


        private string _strUserid;
        /// <summary>
        /// _strUserid
        /// </summary>
        public string Userid
        {
            get { return _strUserid; }
            set { if (this._strUserid != value) { _strUserid = value; } }
        }

        private string _strPassword;
        /// <summary>
        /// _strPassword
        /// </summary>
        public string Password
        {
            get { return _strPassword; }
            set { if (this._strPassword != value) { _strPassword = value; } }
        }

        private string _strUserFirstName;
        /// <summary>
        /// UserFirstName
        /// </summary>
        public string UserFirstName
        {
            get { return _strUserFirstName; }
            set { if (this._strUserFirstName != value) { _strUserFirstName = value; } }
        }

        private string _strUserLastName;
        /// <summary>
        /// UserLastName
        /// </summary>
        public string UserLastName
        {
            get { return _strUserLastName; }
            set { if (this._strUserLastName != value) { _strUserLastName = value; } }
        }

        private string _strEmailId;
        /// <summary>
        /// EmailId
        /// </summary>
        public string EmailId
        {
            get { return _strEmailId; }
            set { if (this._strEmailId != value) { _strEmailId = value; } }
        }

        private string _strTrainingSiteId;
        /// <summary>
        /// _strTrainingSiteId
        /// </summary>
        public string TrainingUserSiteId
        {
            get { return _strTrainingSiteId; }
            set { if (this._strTrainingSiteId != value) { _strTrainingSiteId = value; } }
        }

        private string _strTrainingPartnerId;
        /// <summary>
        /// _strTrainingPartnerId
        /// </summary>
        public string TrainingUserPartnerId
        {
            get { return _strTrainingPartnerId; }
            set { if (this._strTrainingPartnerId != value) { _strTrainingPartnerId = value; } }
        }

        private bool _strTrainingUserIsActive;
        /// <summary>
        /// _strTrainingUserIsActive
        /// </summary>
        public bool TrainingUserIsActive
        {
            get { return _strTrainingUserIsActive; }
            set { if (this._strTrainingUserIsActive != value) { _strTrainingUserIsActive = value; } }
        }

        private string _strXMLServer;
        /// <summary>
        /// XMLserver
        /// </summary>
        public string XMLserver
        {
            get { return _strXMLServer; }
            set { if (this._strXMLServer != value) { _strXMLServer = value; } }
        }

        private string _strUserTypeId;
        /// <summary>
        /// XMLserver
        /// </summary>
        public string UserTypeId
        {
            get { return _strUserTypeId; }
            set { if (this._strUserTypeId != value) { _strUserTypeId = value; } }
        }

        private string _strVirtualTrainingType;
        /// <summary>
        /// TrainingType
        /// </summary>
        public string TrainingType
        {
            get { return _strVirtualTrainingType; }
            set { if (this._strVirtualTrainingType != value) { _strVirtualTrainingType = value; } }
        }
        private string _strWebExURL;
        /// <summary>
        /// WebExURL
        /// </summary>
        public string WebExURL
        {
            get { return _strWebExURL; }
            set { if (this._strWebExURL != value) { _strWebExURL = value; } }
        }


        private string _strZoomAccessToken;
        /// <summary>
        /// WebExURL
        /// </summary>
        public string ZoomAccessToken
        {
            get { return _strZoomAccessToken; }
            set { if (this._strZoomAccessToken != value) { _strZoomAccessToken = value; } }
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
            GetUserNameAvailable,
            AddGroupAdmin,
            DeleteGroupAdmin
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkUpdateVirtualTrainingAttendeeMaster,
            AssignedAttendeeDelete,
            GetAllUsers,
            GetAllTrainingUsers,
            GetWebServiceDefaultUser,
            AdminGetAllUsers,
            AdminMappedGetAllUsers,
            GetUserMappingAll
            
        }
    }
}
