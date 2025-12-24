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
    public class UserContentAssessmentInteractionTrackingSecondAPI : BaseEntity
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public string Status { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public string Data { get; set; }
        public UserContentAssessmentInteractionTrackingSecondAPI()
        {
        }

        private string _WebAPID;
        /// <summary>
        /// SystemUserGUID
        /// </summary>
        public string WebAPID
        {
            get { return _WebAPID; }
            set { if (this._WebAPID != value) { _WebAPID = value; } }
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

        private string _strCourseName;
        /// <summary>
        /// QuestionID
        /// </summary>
        public string CourseName
        {
            get { return _strCourseName; }
            set { if (this._strCourseName != value) { _strCourseName = value; } }
        }

        private string _strScreenTitle;
        /// <summary>
        /// QuestionText
        /// </summary>
        public string ScreenTitle
        {
            get { return _strScreenTitle; }
            set { if (this._strScreenTitle != value) { _strScreenTitle = value; } }
        }

        private string _strCompletionStatus;
        /// <summary>
        /// Options
        /// </summary>
        public string CompletionStatus
        {
            get { return _strCompletionStatus; }
            set { if (this._strCompletionStatus != value) { _strCompletionStatus = value; } }
        }

        private string _strUserDataXML;
        /// <summary>
        /// CorrectOptions
        /// </summary>
        public string UserDataXML
        {
            get { return _strUserDataXML; }
            set { if (this._strUserDataXML != value) { _strUserDataXML = value; } }
        }

        private string _strScore;
        /// <summary>
        /// UserResponse
        /// </summary>
        public string Score
        {
            get { return _strScore; }
            set { if (this._strScore != value) { _strScore = value; } }
        }

        private int _strAPICount;
        /// <summary>
        /// Result
        /// </summary>
        public int APICount
        {
            get { return _strAPICount; }
            set { if (this._strAPICount != value) { _strAPICount = value; } }
        }

        private string _strOperatingSystem;
        /// <summary>
        /// Result
        /// </summary>
        public string OperatingSystem
        {
            get { return _strOperatingSystem; }
            set { if (this._strOperatingSystem != value) { _strOperatingSystem = value; } }
        }

        private string _strBrowser;
        /// <summary>
        /// Result
        /// </summary>
        public string Browser
        {
            get { return _strBrowser; }
            set { if (this._strBrowser != value) { _strBrowser = value; } }
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