/* 
* Copyright Brainvisa Technologies Pvt. Ltd.
* This source file and source code is proprietary property of Brainvisa Technologies Pvt. Ltd. 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Brainvisa’s Client.
* Author:<Shailesh Patil>
* Created:<14/09/09>
* Last Modified:<dd/mm/yy>
*/
namespace YPLMS2._0.API.Entity
{
    public class UserQuestionnaireSessionResponses : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public UserQuestionnaireSessionResponses()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            BulkUpdate
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

        private string _questionOptionsID;
        public string QuestionOptionsID
        {
            get { return _questionOptionsID; }
            set { if (this._questionOptionsID != value) { _questionOptionsID = value; } }
        }

        private string _questionnaireId;
        public string QuestionnaireId
        {
            get { return _questionnaireId; }
            set { if (this._questionnaireId != value) { _questionnaireId = value; } }
        }

        private string _sectionID;
        public string SectionID
        {
            get { return _sectionID; }
            set { if (this._sectionID != value) { _sectionID = value; } }
        }

        private string _questionID;
        public string QuestionID
        {
            get { return _questionID; }
            set { if (this._questionID != value) { _questionID = value; } }
        }

        private string _explanationText;
        public string ExplanationText
        {
            get { return _explanationText; }
            set { if (this._explanationText != value) { _explanationText = value; } }
        }

        private System.DateTime _dateSubmitted;
        public System.DateTime DateSubmitted
        {
            get { return _dateSubmitted; }
            set { if (this._dateSubmitted != value) { _dateSubmitted = value; } }
        }
    }
}