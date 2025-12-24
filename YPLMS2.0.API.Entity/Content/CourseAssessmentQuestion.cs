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


namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// Assessment Question
    /// </summary>
    public class CourseAssessmentQuestion : BaseEntity
    {
        public CourseAssessmentQuestion() { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkAdd
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Delete
        }

        string _strAssessmentQuestionId = string.Empty;
        public string AssessmentQuestionId 
        {
            set { if (_strAssessmentQuestionId != value) _strAssessmentQuestionId = value; }
            get { return _strAssessmentQuestionId; }
        }

        string _strQuestionId = string.Empty;
        public string QuestionId
        {
            set { if (_strQuestionId != value) _strQuestionId = value; }
            get { return _strQuestionId; }
        }

        string _strContentModuleId = string.Empty;
        public string ContentModuleId
        {
            set { if (_strContentModuleId != value) _strContentModuleId = value; }
            get { return _strContentModuleId; }
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
    }
}
