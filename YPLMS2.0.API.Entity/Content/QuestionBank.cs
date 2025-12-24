/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<6/18/2013>
* Last Modified:
*/

using System;
using System.Collections.Generic;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class QuestionBank : QuestionBankLanguage 
    /// </summary>
    /// 
    public class QuestionBank : QuestionBankLanguage 
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public QuestionBank()
        {
            _QuestionBankOptions = new List<QuestionBankOptions>();
        
        }

        private List<QuestionBankOptions> _QuestionBankOptions;
        /// <summary>
        /// Option List
        /// </summary>
        public List<QuestionBankOptions> QuestionBankOptions
        {
            get { return _QuestionBankOptions; }
        }

        public enum QBQuestionType
        {
            MCQ = 1,
            MRQ = 2
        }


        private string _strCategoryID;
        /// <summary>
        /// CategoryID
        /// </summary>
        public string CategoryID
        {
            get { return _strCategoryID; }
            set { if (this._strCategoryID != value) { _strCategoryID = value; } }
        }

        private string _strSubCategoryID;
        /// <summary>
        /// CategoryID
        /// </summary>
        public string SubCategoryID
        {
            get { return _strSubCategoryID; }
            set { if (this._strSubCategoryID != value) { _strSubCategoryID = value; } }
        }

        private QBQuestionType _strQuestionType;
        /// <summary>
        /// QuestionType
        /// </summary>
        public QBQuestionType QuestionType
        {
            get { return _strQuestionType; }
            set { if (this._strQuestionType != value) { _strQuestionType = value; } }
        }

        private bool _strIsActive;
        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive
        {
            get { return _strIsActive; }
            set { if (this._strIsActive != value) { _strIsActive = value; } }
        }

        private string _strKeywards;
        /// <summary>
        /// Keywards
        /// </summary>
        public string Keywards
        {
            get { return _strKeywards; }
            set { if (this._strKeywards != value) { _strKeywards = value; } }
        }

        private string _strCategoryName;
        /// <summary>
        /// CategoryName
        /// </summary>
        public string CategoryName
        {
            get { return _strCategoryName; }
            set { if (this._strCategoryName != value) { _strCategoryName = value; } }
        }

        private string _strSubCategoryName;
        /// <summary>
        /// CategoryName
        /// </summary>
        public string SubCategoryName
        {
            get { return _strSubCategoryName; }
            set { if (this._strSubCategoryName != value) { _strSubCategoryName = value; } }
        }

        private string _strQuestionType_String;
        /// <summary>
        /// CategoryName
        /// </summary>
        public string QuestionType_String
        {
            get { return _strQuestionType_String; }
            set { if (this._strQuestionType_String != value) { _strQuestionType_String = value; } }
        }


        private string _strAssessmentId;
        /// <summary>
        /// CategoryName
        /// </summary>
        public string AssessmentId
        {
            get { return _strAssessmentId; }
            set { if (this._strAssessmentId != value) { _strAssessmentId = value; } }
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
            ImportQuestionBank,
            DeleteLanguage
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkDelete,
            ActivateDeActivateStatus,
            GetImportLanguages,
            GetQuestionBankLanguages,
            GetExportLanguages
        }
    }
}