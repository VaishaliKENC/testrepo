using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.Entity
{
    public abstract class BaseEntityVM
    {
        private string? _strId;
        /// <summary>
        /// To Get Entity object ID
        /// </summary>
        public string? ID
        {
            get { return _strId; }
            set { if (this._strId != value) { _strId = value; } }
        }

        private string? _clientId;
        /// <summary>
        /// To Get Entity object ClientId
        /// </summary>
        public string? ClientId
        {
            get { return _clientId; }
            set { if (this._clientId != value) { _clientId = value; } }
        }

        private string? _token;
        /// <summary>
        /// To Get Entity object ClientId
        /// </summary>
        public string? Token
        {
            get { return _token; }
            set { if (this._token != value) { _token = value; } }
        }

        private string? _strRuleId;
        public string? RuleId
        {
            get { return _strRuleId; }
            set { if (this._strRuleId != value) { _strRuleId = value; } }
        }

        private string? _strCreatedById;
        /// <summary>
        /// Created By Id
        /// </summary>
        public string? CreatedById
        {
            get { return _strCreatedById; }
            set { if (this._strCreatedById != value) { _strCreatedById = value; } }
        }

        private string? _strCategoryId;
        public string? CategoryId
        {
            get { return _strCategoryId; }
            set { if (this._strCategoryId != value) { _strCategoryId = value; } }
        }

        private DateTime? _dateCreated;
        /// <summary>
        /// Date Created
        /// </summary>
        public DateTime? DateCreated
        {
            get { return _dateCreated; }
            set { if (this._dateCreated != value) { _dateCreated = value; } }
        }

        private string? _strLastModifiedById;
        /// <summary>
        /// Last Modified By Id
        /// </summary>
        public string? LastModifiedById
        {
            get { return _strLastModifiedById; }
            set { if (this._strLastModifiedById != value) { _strLastModifiedById = value; } }
        }

        private string? _strContactPersonDetails;
        /// <summary>
        /// Add Contact Person
        /// </summary>
        public string? ContactPersonDetails
        {
            get { return _strContactPersonDetails; }
            set { if (this._strContactPersonDetails != value) { _strContactPersonDetails = value; } }
        }

        private DateTime? _dateLastModified;
        /// <summary>
        /// Last Modified Date
        /// </summary>
        public DateTime? LastModifiedDate
        {
            get { return _dateLastModified; }
            set { if (this._dateLastModified != value) { _dateLastModified = value; } }
        }

        private string? _strCreatedByName;
        public string? CreatedByName
        {
            get { return _strCreatedByName; }
            set { if (this._strCreatedByName != value) { _strCreatedByName = value; } }
        }

        private string? _strLastModifiedByName;
        public string? LastModifiedByName
        {
            get { return _strLastModifiedByName; }
            set { if (this._strLastModifiedByName != value) { _strLastModifiedByName = value; } }
        }

        private string? _strCurrentUserId;
        public string? CurrentUserId
        {
            get { return _strCurrentUserId; }
            set { if (this._strCurrentUserId != value) { _strCurrentUserId = value; } }
        }

        private EntityRange? _listRange;
        /// <summary>
        /// Specify expected records list range.
        /// </summary>
        public EntityRange? ListRange
        {
            get { return _listRange; }
            set { _listRange = value; }
        }

    }
}
