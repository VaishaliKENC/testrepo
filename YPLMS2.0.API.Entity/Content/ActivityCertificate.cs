using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class ActivityCertificate: BaseEntity
    {
        
        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkDelete,
            ActivateDeactivate,
            GetDetailForEditMode,
            GetDetailByActivityId,
            GetAllForTrans,
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
            AddTrans,
        }


        private string _CreatedById;
        /// <summary>
        /// Folder Name
        /// </summary>
        public string CreatedById
        {
            get { return _CreatedById; }
            set { if (this._CreatedById != value) { _CreatedById = value; } }
        }


        private string _CertificateId;
        /// <summary>
        /// Folder Name
        /// </summary>
        public string CertificateId
        {
            get { return _CertificateId; }
            set { if (this._CertificateId != value) { _CertificateId = value; } }
        }

        private string _CertificateName;
        /// <summary>
        /// Parent Folder Id
        /// </summary>
        public string CertificateName
        {
            get { return _CertificateName; }
            set { if (this._CertificateName != value) { _CertificateName = value; } }
        }

        private string _CertificateDescription;
        /// <summary>
        /// DocumentFolder Description
        /// </summary>
        public string CertificateDescription
        {
            get { return _CertificateDescription; }
            set { if (this._CertificateDescription != value) { _CertificateDescription = value; } }
        }

        private string _LanguageID;
        /// <summary>
        /// DocumentFolder Description
        /// </summary>
        public string LanguageID
        {
            get { return _LanguageID; }
            set { if (this._LanguageID != value) { _LanguageID = value; } }
        }


        private bool _IsDefault;
        /// <summary>
        /// Is visible
        /// </summary>
        public bool IsDefault
        {
            get { return _IsDefault; }
            set { if (this._IsDefault != value) { _IsDefault = value; } }
        }

        private bool _IsActive;
        /// <summary>
        /// Is visible
        /// </summary>
        public bool IsActive
        {
            get { return _IsActive; }
            set { if (this._IsActive != value) { _IsActive = value; } }
        }

        private string _BodyMessage;
        /// <summary>
        /// Relative Path from Document e.g. /Folder1/thisfolderName
        /// </summary>
        public string BodyMessage
        {
            get { return _BodyMessage; }
            set { if (this._BodyMessage != value) { _BodyMessage = value; } }
        }


        private string _BackGroundImage;
        /// <summary>
        /// Relative Path from Document e.g. /Folder1/thisfolderName
        /// </summary>
        public string BackGroundImage
        {
            get { return _BackGroundImage; }
            set { if (this._BackGroundImage != value) { _BackGroundImage = value; } }
        }

        private string _ActionURL;
        /// <summary>
        /// Relative Path from Document e.g. /Folder1/thisfolderName
        /// </summary>
        public string ActionURL
        {
            get { return _ActionURL; }
            set { if (this._ActionURL != value) { _ActionURL = value; } }
        }

        private string _CertImagesData;
        /// <summary>
        /// Folder Name
        /// </summary>
        public string CertImagesData
        {
            get { return _CertImagesData; }
            set { if (this._CertImagesData != value) { _CertImagesData = value; } }
        }

        private string _ActivityId;
        /// <summary>
        /// Folder Name
        /// </summary>
        public string ActivityId
        {
            get { return _ActivityId; }
            set { if (this._ActivityId != value) { _ActivityId = value; } }
        }

    }
}
