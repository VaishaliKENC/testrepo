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

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class QuestionBankOptions : QuestionBankOptionsLanguage 
    /// </summary>
    /// 
    public class QuestionBankOptions : QuestionBankOptionsLanguage
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public QuestionBankOptions()
        { }

        public enum QBOptionType
        {
            Correct = 1,
            Incorrect = 2
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

        private QBOptionType _strOptionType;
        /// <summary>
        /// OptionType
        /// </summary>
        public QBOptionType OptionType
        {
            get { return _strOptionType; }
            set { if (this._strOptionType != value) { _strOptionType = value; } }
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
            GetAll
        }
    }
}