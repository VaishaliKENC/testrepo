using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class DefaultAssignmentValue : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public DefaultAssignmentValue()
        { }

        /// <summary>
        ///  enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetDefaultValueList,
            BulkAddDefaultValueList

        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            delete

        }


        private string _strModuleName;
        /// <summary>
        /// UserID
        /// </summary>
        public string ModuleName
        {
            get { return _strModuleName; }
            set { if (this._strModuleName != value) { _strModuleName = value; } }
        }

        private string _strFieldName;
        /// <summary>
        /// UserName
        /// </summary>
        public string FieldName
        {
            get { return _strFieldName; }
            set { if (this._strFieldName != value) { _strFieldName = value; } }
        }


        private string _strDataTypee;
        /// <summary>
        /// Activity Name
        /// </summary>
        public string DataTypee
        {
            get { return _strDataTypee; }
            set { if (this._strDataTypee != value) { _strDataTypee = value; } }
        }

        //
        private string _strDefaultValue;
        public string DefaultValue
        {
            get { return _strDefaultValue; }
            set { if (this._strDefaultValue != value) { _strDefaultValue = value; } }
        }
        //
        private string _strCondition;
        /// <summary>
        /// Activity Description
        /// </summary>
        public string Condition
        {
            get { return _strCondition; }
            set { if (this._strCondition != value) { _strCondition = value; } }
        }

        private DateTime _dtpCurrentDate;
        /// <summary>
        /// Activity Description
        /// </summary>
        public DateTime CurrentDate
        {
            get { return _dtpCurrentDate; }
            set { if (this._dtpCurrentDate != value) { _dtpCurrentDate = value; } }
        }

        private bool _IsUsedForDynamicAssignment;
        /// <summary>
        /// Activity Description
        /// </summary>
        public bool IsUsedForDynamicAssignment
        {
            get { return _IsUsedForDynamicAssignment; }
            set { if (this._IsUsedForDynamicAssignment != value) { _IsUsedForDynamicAssignment = value; } }
        }

    }
    
    
}