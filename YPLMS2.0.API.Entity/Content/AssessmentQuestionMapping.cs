/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<8/5/2013>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class AssessmentQuestionMapping : BaseEntity 
    /// </summary>
    /// 
    public class AssessmentQuestionMapping : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public AssessmentQuestionMapping()
        { }


        private string _strQuestionId;
        /// <summary>
        /// QuestionId
        /// </summary>
        public string QuestionId
        {
            get { return _strQuestionId; }
            set { if (this._strQuestionId != value) { _strQuestionId = value; } }
        }

        private string _strSectionID;
        /// <summary>
        /// SectionID
        /// </summary>
        public string SectionID
        {
            get { return _strSectionID; }
            set { if (this._strSectionID != value) { _strSectionID = value; } }
        }

        private float _strSequenceOrder;
        /// <summary>
        /// SequenceOrder
        /// </summary>
        public float SequenceOrder
        {
            get { return _strSequenceOrder; }
            set { if (this._strSequenceOrder != value) { _strSequenceOrder = value; } }
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
            BulkUpdate
        }
    }
}