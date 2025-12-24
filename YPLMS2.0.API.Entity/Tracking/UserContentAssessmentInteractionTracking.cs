/*
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Bharat
* Created:<4/4/2022>
* Last Modified:<dd/mm/yy>
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class UserContentAssessmentInteractionTracking : BaseEntity 
    /// </summary>
    /// 
    public class UserContentAssessmentInteractionTracking : BaseEntity
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public string Status { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public string Data { get; set; }
        public UserContentAssessmentInteractionTracking()
        {
        }

        private string _InteractionID;
        /// <summary>
        /// SystemUserGUID
        /// </summary>
        public string InteractionID
        {
            get { return _InteractionID; }
            set { if (this._InteractionID != value) { _InteractionID = value; } }
        }

        private string _strSystemUserGUID;
        /// <summary>
        /// SystemUserGUID
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private string _strContentModuleId;
        /// <summary>
        /// ContentModuleId
        /// </summary>
        public string ContentModuleId
        {
            get { return _strContentModuleId; }
            set { if (this._strContentModuleId != value) { _strContentModuleId = value; } }
        }

        private string _strQuestionID;
        /// <summary>
        /// QuestionID
        /// </summary>
        public string QuestionID
        {
            get { return _strQuestionID; }
            set { if (this._strQuestionID != value) { _strQuestionID = value; } }
        }

        private string _strQuestionText;
        /// <summary>
        /// QuestionText
        /// </summary>
        public string QuestionText
        {
            get { return _strQuestionText; }
            set { if (this._strQuestionText != value) { _strQuestionText = value; } }
        }

        private string _strOptions;
        /// <summary>
        /// Options
        /// </summary>
        public string Options
        {
            get { return _strOptions; }
            set { if (this._strOptions != value) { _strOptions = value; } }
        }

        private string _strCorrectOptions;
        /// <summary>
        /// CorrectOptions
        /// </summary>
        public string CorrectOptions
        {
            get { return _strCorrectOptions; }
            set { if (this._strCorrectOptions != value) { _strCorrectOptions = value; } }
        }

        private string _strUserResponse;
        /// <summary>
        /// UserResponse
        /// </summary>
        public string UserResponse
        {
            get { return _strUserResponse; }
            set { if (this._strUserResponse != value) { _strUserResponse = value; } }
        }

        private string _strResult;
        /// <summary>
        /// Result
        /// </summary>
        public string Result
        {
            get { return _strResult; }
            set { if (this._strResult != value) { _strResult = value; } }
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

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll
        }
    }
}