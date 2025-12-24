/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh
* Created:<24/08/09>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
   [Serializable] public class StudentList: Learner 
    {

       public StudentList()
        {
           
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
           DeleteSelected
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            UpdateDetails,
            AddWithFile,
            UpdateDetailsWithFile
        }
//StudentDetailId
//SystemUserGUID
//IsApproved
//IdProofDocument
//CreatedById
//DateCreated
//LastModifiedById
//LastModifiedDate

        private string _strStudentDetailId;
        /// <summary>
        /// File Name
        /// </summary>
        public string StudentDetailId
        {
            get { return _strStudentDetailId; }
            set { if (this._strStudentDetailId != value) { _strStudentDetailId = value; } }
        }

        private string _strSystemUserGUID;
        /// <summary>
        /// File Name
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private string _strFileName;
        /// <summary>
        /// File Name
        /// </summary>
        public string FileName
        {
            get { return _strFileName; }
            set { if (this._strFileName != value) { _strFileName = value; } }
        }

        private string _strFilePath;
        /// <summary>
        /// File Path
        /// </summary>
        public string FilePath
        {
            get { return _strFilePath; }
            set { if (this._strFilePath != value) { _strFilePath = value; } }
        }

        private DateTime _dateImport;
        /// <summary>
        /// Date of Import
        /// </summary>
        public DateTime ImportDate
        {
            get { return _dateImport; }
            set { if (this._dateImport != value) { _dateImport = value; } }
        }

        private ImportType _enumImportType;
        /// <summary>
        /// Import Type
        /// </summary>
        public ImportType ImportType
        {
            get { return _enumImportType; }
            set { if (this._enumImportType != value) { _enumImportType = value; } }
        }

        private ImportStatus _enumStatus;
        /// <summary>
        /// Import Type
        /// </summary>
        public ImportStatus ImportStatus
        {
            get { return _enumStatus; }
            set { if (this._enumStatus != value) { _enumStatus = value; } }
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

        private string _strImportLog;
        /// <summary>
        /// Import Log
        /// </summary>
        public string ImportLog
        {
            get { return _strImportLog; }
            set { _strImportLog = value; }

        }

        private string _strAdminName;
        /// <summary>
        /// AdministratorName
        /// </summary>
        public string AdministratorName
        {
            get { return _strAdminName; }
            set { _strAdminName = value; }

        }


        /// <summary>
        /// Status
        /// </summary>
        public enum RegistrationStatus
        {
            Pending,
            Approved,
            Rejected
        }
    }
    
}
