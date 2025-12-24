using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class Assignments:BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public Assignments()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAssignmentLanguages
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
            UpdateLanguage,
            DeleteLanguage
        }

        private string _assignmentTitle;
        public string AssignmentTitle
        {
            get { return _assignmentTitle; }
            set { if (this._assignmentTitle != value) { _assignmentTitle = value; } }
        }

        private string _objective;
        public string Objective
        {
            get { return _objective; }
            set { if (this._objective != value) { _objective = value; } }
        }

        private string _attachments;
        public string Attachments
        {
            get { return _attachments; }
            set { if (this._attachments != value) { _attachments = value; } }
        }

        private int _points;
        public int Point
        {
            get { return _points; }
            set { if (this._points != value) { _points = value; } }
        }

        private bool _isSubmissionApproval;
        public bool IsSubmissionApproval
        {
            get { return _isSubmissionApproval; }
            set { if (this._isSubmissionApproval != value) { _isSubmissionApproval = value; } }
        }

        private bool _isPrint;
        public bool IsPrint
        {
            get { return _isPrint; }
            set { if (this._isPrint != value) { _isPrint = value; } }
        }

        private bool _isSavePartialAssignment;
        public bool IsSavePartialAssignment
        {
            get { return _isSavePartialAssignment; }
            set { if (this._isSavePartialAssignment != value) { _isSavePartialAssignment = value; } }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
        }

        private DateTime _fromDate;
        public DateTime FromDate
        {
            get { return _fromDate; }
            set { if (this._fromDate != value) { _fromDate = value; } }
        }

        private DateTime _toDate;
        public DateTime ToDate
        {
            get { return _toDate; }
            set { if (this._toDate != value) { _toDate = value; } }
        }

        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }

        private string _LanguageId;
        public string LanguageId
        {
            get { return _LanguageId; }
            set { if (this._LanguageId != value) { _LanguageId = value; } }
        }

        private string _LanguageName;
        public string LanguageName
        {
            get { return _LanguageName; }
            set { if (this._LanguageName != value) { _LanguageName = value; } }
        }

        //private string _strCreatedName;
        ///// <summary>
        ///// Created by Name
        ///// </summary>
        //public string CreatedName
        //{
        //    get { return _strCreatedName; }
        //    set { if (this._strCreatedName != value) { _strCreatedName = value; } }
        //}

        //private string _strModifiedByName;
        //public string ModifiedByName
        //{
        //    get { return _strModifiedByName; }
        //    set { if (this._strModifiedByName != value) { _strModifiedByName = value; } }
        //}
    }
}
