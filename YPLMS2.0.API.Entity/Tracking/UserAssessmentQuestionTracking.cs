/* 
Copyright to Encora.
This source file and source code is proprietary property of Encora. 

Any copying, reproduction, distribution, modification and/or reverse-engineering of any part or in whole of this source file and source 
code is prohibited. The above holds true, unless defined otherwise in writing in the Contract for this work with Encora’s Client.
Author: 
Last Modified:29/02/12
Last Modified By: 
*/
/* This class represents UserAssessmentQuestionTracking */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class UserAssessmentQuestionTracking : BaseEntity
    {
        public UserAssessmentQuestionTracking() { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkAdd,
            GetAllByCourseLearnerId
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

        string _strSystemUserGUID = string.Empty;
        public string SystemUserGUID
        {
            set { if (_strSystemUserGUID != value) _strSystemUserGUID = value; }
            get { return _strSystemUserGUID; }
        }

        string _strContentModuleId = string.Empty;
        public string ContentModuleId
        {
            set { if (_strContentModuleId != value) _strContentModuleId = value; }
            get { return _strContentModuleId; }
        }

        string _strQuestionId = string.Empty;
        public string QuestionId
        {
            set { if (_strQuestionId != value) _strQuestionId = value; }
            get { return _strQuestionId; }
        }

        string _strAttemptId = string.Empty;
        public string AttemptId
        {
            set { if (_strAttemptId != value) _strAttemptId = value; }
            get { return _strAttemptId; }
        }

        int _intCorrectCount;
        public int CorrectCount
        {
            set { if (_intCorrectCount != value) _intCorrectCount = value; }
            get { return _intCorrectCount; }
        }

        int _intInCorrectCount;
        public int InCorrectCount
        {
            set { if (_intInCorrectCount != value) _intInCorrectCount = value; }
            get { return _intInCorrectCount; }
        }

        string _strQuestionText = string.Empty;
        public string QuestionText
        {
            set { if (_strQuestionText != value) _strQuestionText = value; }
            get { return _strQuestionText; }
        }
        string _strQuestionUniqueIndxNum = string.Empty;
        public string QuestionUniqueIndxNum
        {
            set { if (_strQuestionUniqueIndxNum != value) _strQuestionUniqueIndxNum = value; }
            get { return _strQuestionUniqueIndxNum; }
        }
        string _strAttemptHistory = string.Empty;
        public string AttemptHistory
        {
            set { if (_strAttemptHistory != value) _strAttemptHistory = value; }
            get { return _strAttemptHistory; }
        }


        int _intTotalAttempt;
        public int TotalAttempt
        {
            set { if (_intTotalAttempt != value) _intTotalAttempt = value; }
            get { return _intTotalAttempt; }
        }
    }
}
