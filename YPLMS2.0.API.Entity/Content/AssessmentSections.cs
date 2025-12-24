/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:<Shailesh Patil>
* Created:<14/09/09>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class AssessmentSections : AssessmentSectionsLanguage
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public AssessmentSections()
        {
            _AssessmentQuestion = new List<AssessmentQuestion>(); 
        }

        public new enum ListMethod
        {
            GetAll
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Add,
            Update,
            Delete,
            SequenceUpdate
        }
        private List<AssessmentQuestion> _AssessmentQuestion;
        /// <summary>
        /// Option List
        /// </summary>
        public List<AssessmentQuestion> AssessmentQuestion
        {
            get { return _AssessmentQuestion; }
            set { _AssessmentQuestion = value; }
        }      
 
        private string _AssessmentId;
        public string AssessmentId
        {
            get { return _AssessmentId; }
            set { if (this._AssessmentId != value) { _AssessmentId = value; } }
        }

        private int _sequenceOrder;
        public int SequenceOrder
        {
            get { return _sequenceOrder; }
            set { if (this._sequenceOrder != value) { _sequenceOrder = value; } }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { if (this._isActive != value) { _isActive = value; } }
        }

        private bool _isMoveUp;
        public bool IsMoveUp
        {
            get { return _isMoveUp; }
            set { if (this._isMoveUp != value) { _isMoveUp = value; } }
        }
       

        
    }
}