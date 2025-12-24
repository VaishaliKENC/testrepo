using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YPLMS2._0.API.Entity.ImportDefination;

namespace YPLMS2._0.API.Entity
{
    public class ImportDefinationVM:BaseEntityVM
    {
       
        private string? _strFieldName;
        /// <summary>
        /// Field Name
        /// </summary>
        public string? FieldName
        {
            get { return _strFieldName; }
            set { if (this._strFieldName != value) { _strFieldName = value; } }
        }
        private short? _iminLength;
        /// <summary>
        /// Minimum Length 
        /// </summary>
        public short? MinLength
        {
            get { return _iminLength; }
            set { if (this._iminLength != value) { _iminLength = value; } }
        }



        private short? _imaxLength;
        /// <summary>
        /// Maximun Length 
        /// </summary>
        public short? MaxLength
        {
            get { return _imaxLength; }
            set { if (this._imaxLength != value) { _imaxLength = value; } }
        }

        private int? _imaxLengthInDB;
        /// <summary>
        /// Max Length in DB
        /// </summary>
        public int? MaxLengthInDB
        {
            get { return _imaxLengthInDB; }
            set { if (this._imaxLengthInDB != value) { _imaxLengthInDB = value; } }
        }

        private ImportDefination.ValueType? _fieldValueType;
        /// <summary>
        /// Field Value Type
        /// </summary>
        public ImportDefination.ValueType? FieldValueType
        {
            get { return _fieldValueType; }
            set { if (this._fieldValueType != value) { _fieldValueType = value; } }
        }

        private FieldType? _fieldType;
        /// <summary>
        /// Field Type
        /// </summary>
        public FieldType? FieldTypes
        {
            get { return _fieldType; }
            set { if (this._fieldType != value) { _fieldType = value; } }
        }

        private bool? _bAllowBlank;
        /// <summary>
        /// Allow Blank
        /// </summary>
        public bool? AllowBlank
        {
            get { return _bAllowBlank; }
            set { if (this._bAllowBlank != value) { _bAllowBlank = value; } }
        }
        private ErrorLevel? _fieldErrorLevel;
        /// <summary>
        /// Error Level
        /// </summary>
        public ErrorLevel? FieldErrorLevel
        {
            get { return _fieldErrorLevel; }
            set { if (this._fieldErrorLevel != value) { _fieldErrorLevel = value; } }
        }

        private bool? _bInclude;
        /// <summary>
        /// Include
        /// </summary>
        public bool? Include
        {
            get { return _bInclude; }
            set { if (this._bInclude != value) { _bInclude = value; } }
        }

        private bool? _bIsMandatory;
        /// <summary>
        /// Is Mandatory
        /// </summary>
        public bool? IsMandatory
        {
            get { return _bIsMandatory; }
            set { if (this._bIsMandatory != value) { _bIsMandatory = value; } }
        }

        private bool? _bIsDefault;
        /// <summary>
        /// Is IsDefault
        /// </summary>
        public bool? IsDefault
        {
            get { return _bIsDefault; }
            set { if (this._bIsDefault != value) { _bIsDefault = value; } }
        }

        private string? _strDefaultvalue;
        /// <summary>
        /// Default Value
        /// </summary>
        public string? DefaultValue
        {
            get { return _strDefaultvalue; }
            set { if (this._strDefaultvalue != value) { _strDefaultvalue = value; } }
        }

        private ImportAction? _enumImportAction;
        /// <summary>
        /// Import Action
        /// </summary>
        public ImportAction? ImportAction
        {
            get { return _enumImportAction; }
            set { if (this._enumImportAction != value) { _enumImportAction = value; } }
        }

        private string? _strKeyword;
        public string? KeyWord
        {
            get { return _strKeyword; }
            set { if (this._strKeyword != value) { _strKeyword = value; } }
        }

        private string? _strFieldDataType;

        public string? FieldDataType
        {
            get { return _strFieldDataType; }
            set { if (this._strFieldDataType != value) { _strFieldDataType = value; } }
        }

        private string? _strErrorLevelType;

        public string? ErrorLevelType
        {
            get { return _strErrorLevelType; }
            set { if (this._strErrorLevelType != value) { _strErrorLevelType = value; } }
        }
        public bool Validate(bool pIsUpdate)
        {

            if (pIsUpdate)
            {
                if (String.IsNullOrEmpty(ID))
                    return false;

            }
            else
            {
                if (String.IsNullOrEmpty(CreatedById))
                    return false;

                if (String.IsNullOrEmpty(FieldName))
                    return false;

                if (String.IsNullOrEmpty(ClientId))
                    return false;
            }

            if (String.IsNullOrEmpty(LastModifiedById))
                return false;

            return true;
        }
        //added by Gitanjali 7.12.2010
        private string? _strReportId;
        /// <summary>
        /// Field Name
        /// </summary>
        public string? ReportId
        {
            get { return _strReportId; }
            set { if (this._strReportId != value) { _strReportId = value; } }
        }
    }
}
