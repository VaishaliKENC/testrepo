/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Ashish
* Created:<19/07/09>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// Import Definition Item Class
    /// </summary>
    [Serializable]
    public class ImportDefination : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ImportDefination()
        {
           
        }
        public enum ErrorLevel
        {
            None,
            Fatal,
            Warning            
        }
        
        public enum ValueType
        {
            Alphanumeric, 
            Date,
            Yes_No,
            Numeric,  
            Email,
            UserType,
            Language,
            ThemeName,
            Gender,
            IsActive,
            LoginId,
            ManagerEmail,
            WebAddress,
            DropDown,
            Radio,
            RegionView,
            Password,
            ActivityCompletionStatus,
            ActivityContentType
        }
        public enum FieldType
        {
            Standard,
            CustomField,
            OrgTreeLevels,
            StandardCustom,
            Assignment,
            Activity,
            Completion,
            eStore,
            Miscellaneous,
            ClassroomTraining,
            Certificate,
            ILTClassroomTraining

        }
        /// <summary>
        /// ENUM method
        /// </summary>
        public new enum Method
        {
            Update
        }

        /// <summary>
        /// List ENUM method
        /// </summary>
        public new enum ListMethod
        {
            GetAll            
        }   

        private string _strFieldName;
        /// <summary>
        /// Field Name
        /// </summary>
        public string FieldName
        {
            get { return _strFieldName; }
            set { if (this._strFieldName != value) { _strFieldName = value; } }
        }
        private short _iminLength;
        /// <summary>
        /// Minimum Length 
        /// </summary>
        public short MinLength
        {
            get { return _iminLength; }
            set { if (this._iminLength != value) { _iminLength = value; } }
        }

        

        private short _imaxLength;
        /// <summary>
        /// Maximun Length 
        /// </summary>
        public short MaxLength
        {
            get { return _imaxLength; }
            set { if (this._imaxLength != value) { _imaxLength = value; } }
        }

        private int _imaxLengthInDB;
        /// <summary>
        /// Max Length in DB
        /// </summary>
        public int MaxLengthInDB
        {
            get { return _imaxLengthInDB; }
            set { if (this._imaxLengthInDB != value) { _imaxLengthInDB = value; } }
        }

        private ValueType _fieldValueType;
        /// <summary>
        /// Field Value Type
        /// </summary>
        public ValueType FieldValueType
        {
            get { return _fieldValueType; }
            set { if (this._fieldValueType != value) { _fieldValueType = value; } }
        }

        private FieldType _fieldType;
        /// <summary>
        /// Field Type
        /// </summary>
        public FieldType FieldTypes
        {
            get { return _fieldType; }
            set { if (this._fieldType != value) { _fieldType = value; } }
        }

        private bool _bAllowBlank;
        /// <summary>
        /// Allow Blank
        /// </summary>
        public bool AllowBlank
        {
            get { return _bAllowBlank; }
            set { if (this._bAllowBlank != value) { _bAllowBlank = value; } }
        }
        private ErrorLevel _fieldErrorLevel;
        /// <summary>
        /// Error Level
        /// </summary>
        public ErrorLevel FieldErrorLevel
        {
            get { return _fieldErrorLevel; }
            set { if (this._fieldErrorLevel != value) { _fieldErrorLevel = value; } }
        }

        private bool _bInclude;
        /// <summary>
        /// Include
        /// </summary>
        public bool Include
        {
            get { return _bInclude; }
            set { if (this._bInclude != value) { _bInclude = value; } }
        }

        private bool _bIsMandatory;
        /// <summary>
        /// Is Mandatory
        /// </summary>
        public bool IsMandatory
        {
            get { return _bIsMandatory; }
            set { if (this._bIsMandatory != value) { _bIsMandatory = value; } }
        }

        private bool _bIsDefault;
        /// <summary>
        /// Is IsDefault
        /// </summary>
        public bool IsDefault
        {
            get { return _bIsDefault; }
            set { if (this._bIsDefault != value) { _bIsDefault = value; } }
        }

        private string _strDefaultvalue;
        /// <summary>
        /// Default Value
        /// </summary>
        public string DefaultValue
        {
            get { return _strDefaultvalue; }
            set { if (this._strDefaultvalue != value) { _strDefaultvalue = value; } }
        }

        private ImportAction _enumImportAction;
        /// <summary>
        /// Import Action
        /// </summary>
        public ImportAction ImportAction
        {
            get { return _enumImportAction; }
            set { if (this._enumImportAction != value) { _enumImportAction = value; } }
        }

        private string _strKeyword;
        public string KeyWord
        {
            get { return _strKeyword; }
            set { if (this._strKeyword != value) { _strKeyword = value; } }
        }

        private string _strFieldDataType;

        public string FieldDataType
        {
            get { return _strFieldDataType; }
            set { if (this._strFieldDataType != value) { _strFieldDataType = value; } }
        }

        private string _strErrorLevelType;

        public string ErrorLevelType
        {
            get { return _strErrorLevelType; }
            set { if (this._strErrorLevelType != value) { _strErrorLevelType = value; } }
        }

        private string _strlengthError;

        public string lengthError
        {
            get { return _strlengthError; }
            set { if (this._strlengthError != value) { _strlengthError = value; } }
        }

        private string _strOtherError;

        public string OtherError
        {
            get { return _strOtherError; }
            set { if (this._strOtherError != value) { _strOtherError = value; } }
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
        private string _strReportId;
        /// <summary>
        /// Field Name
        /// </summary>
        public string ReportId
        {
            get { return _strReportId; }
            set { if (this._strReportId != value) { _strReportId = value; } }
        }

    }
}