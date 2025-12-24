/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh
* Created:<05/Dec/09>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;

namespace YPLMS2._0.API.Entity
{
   [Serializable] public class UserImportLog : BaseEntity
    {
        /// <summary>
        /// Default Contructor UserImportLog
        /// <summary>
        public UserImportLog()
        {
        }


        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            BulkUpdateWithFile
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete
        }


        private string _strFileLineNumber;
        public string FileLineNumber
        {
            get { return _strFileLineNumber; }
            set { if (this._strFileLineNumber != value) { _strFileLineNumber = value; } }
        }


        private string _strUserNameAlias;
        public string UserNameAlias
        {
            get { return _strUserNameAlias; }
            set { if (this._strUserNameAlias != value) { _strUserNameAlias = value; } }
        }

        private string _strErrorDescription;
        public string ErrorDescription
        {
            get { return _strErrorDescription; }
            set { if (this._strErrorDescription != value) { _strErrorDescription = value; } }
        }

        private UserImportErrorType _strErrorType;
        public UserImportErrorType ErrorType
        {
            get { return _strErrorType; }
            set { if (this._strErrorType != value) { _strErrorType = value; } }
        }


        private bool _bisExecution;
        public bool IsExecution
        {
            get { return _bisExecution; }
            set { if (this._bisExecution != value) { _bisExecution = value; } }
        }

        /// <summary>
        /// Import Connection
        /// </summary>
        private SqlConnection _sImportConn;
        public SqlConnection ImportConnection
        {
            get { return _sImportConn; }
            set { if (this._sImportConn != value) { _sImportConn = value; } }
        }

    }
    public enum UserImportErrorType
    { 
        None,
        [Description("Execution Error")]
        ExecutionError,
        [Description("Scope Error")]
    ScopeError,
    Warning,
    Fatal

    }
}
