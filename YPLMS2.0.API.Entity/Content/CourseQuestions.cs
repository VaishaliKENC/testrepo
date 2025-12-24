using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class CourseQuestions : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public CourseQuestions()
        { }

        private string _strCourseId;
        /// <summary>
        /// FeatureName
        /// </summary>
        public string CourseId
        {
            get { return _strCourseId; }
            set { if (this._strCourseId != value) { _strCourseId = value; } }
        }



        private string _strId;
        /// <summary>
        /// FeatureName
        /// </summary>
        public string Id
        {
            get { return _strId; }
            set { if (this._strId != value) { _strId = value; } }
        }

        private string _strText;
        /// <summary>
        /// FeatureDescription
        /// </summary>
        public string Text
        {
            get { return _strText; }
            set { if (this._strText != value) { _strText = value; } }
        }

        private string  _strType;
        /// <summary>
        /// IsActive
        /// </summary>
        public string  Type
        {
            get { return _strType; }
            set { if (this._strType != value) { _strType = value; } }
        }

        private string _Answers;
        /// <summary>
        /// IsActive
        /// </summary>
        public string Answers
        {
            get { return _Answers; }
            set { if (this._Answers != value) { _Answers = value; } }
        }

        private string _strCorrectAnswer;
        /// <summary>
        /// IsActive
        /// </summary>
        public string CorrectAnswer
        {
            get { return _strCorrectAnswer; }
            set { if (this._strCorrectAnswer != value) { _strCorrectAnswer = value; } }
        }

        private string _strObjectiveId;
        /// <summary>
        /// IsActive
        /// </summary>
        public string ObjectiveId
        {
            get { return _strObjectiveId; }
            set { if (this._strObjectiveId != value) { _strObjectiveId = value; } }
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
            GetAll,
            BulkAdd,
        }
    }
}