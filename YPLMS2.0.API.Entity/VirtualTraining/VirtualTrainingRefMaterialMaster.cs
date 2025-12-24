using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class VirtualTrainingRefMaterialMaster : BaseEntity
    {
        public VirtualTrainingRefMaterialMaster() 
        { }



        private string _strRefMaterialTypeId;
        /// <summary>
        /// RefMaterialTypeId
        /// </summary>
        public string RefMaterialTypeId
        {
            get { return _strRefMaterialTypeId; }
            set { if (this._strRefMaterialTypeId != value) { _strRefMaterialTypeId = value; } }
        }

        private string _strFileName;
        /// <summary>
        /// ResourceFileName
        /// </summary>
        public string FileName
        {
            get { return _strFileName; }
            set { if (this._strFileName != value) { _strFileName = value; } }
        }

        private string _RefMaterialDocumentName;
        /// <summary>
        /// ResourceFileName
        /// </summary>
        public string RefMaterialDocumentName
        {
            get { return _RefMaterialDocumentName; }
            set { if (this._RefMaterialDocumentName != value) { _RefMaterialDocumentName = value; } }
        }

        private string _RefMaterialDocumentDescription;
        /// <summary>
        /// ResourceFileName
        /// </summary>
        public string RefMaterialDocumentDescription
        {
            get { return _RefMaterialDocumentDescription; }
            set { if (this._RefMaterialDocumentDescription != value) { _RefMaterialDocumentDescription = value; } }
        }


        private byte[] _RefMaterialFile;
        /// <summary>
        /// File Content
        /// </summary>
        public byte[] RefMaterialFile
        {
            get { return _RefMaterialFile; }
            set { if (this._RefMaterialFile != value) { _RefMaterialFile = value; } }
        }


        private Nullable<bool> _isActive;
        public Nullable<bool> IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
        }

        private string _strVirtualTrainingId;
        /// <summary>
        /// TrainingId
        /// </summary>
        public string TrainingId
        {
            get { return _strVirtualTrainingId; }
            set { if (this._strVirtualTrainingId != value) { _strVirtualTrainingId = value; } }
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
            RefMaterialMapping,
            DeleteMapping
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            ActivateDeActivateStatus,
            GetAllMappedRefMaterialMapping,
            GetAllNonMapped

        }
    }
}
