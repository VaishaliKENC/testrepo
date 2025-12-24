using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
     [Serializable]
    public class UserDocumentTracking : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public UserDocumentTracking()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            MarkCompleted
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Add
        }

        private string _strDocumentId;
        /// <summary>
        /// Asset Id
        /// </summary>
        public string DocumentId
        {
            get { return _strDocumentId; }
            set { if (this._strDocumentId != value) { _strDocumentId = value; } }
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
