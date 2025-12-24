/* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:<Shailesh Patil>
* Created:<14/09/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class AssessmentLanguage : BaseEntity
    {
        /// <summary>
        /// Default Constructor
        /// <summary>
        public AssessmentLanguage()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            AddAll
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
                

        private string _languageId;
        public string LanguageId
        {
            get { return _languageId; }
            set { if (this._languageId != value) { _languageId = value; } }
        }

        private string _AssessmentTitle;
        public string AssessmentTitle
        {
            get { return _AssessmentTitle; }
            set { if (this._AssessmentTitle != value) { _AssessmentTitle = value; } }
        }

        private string _AssessmentDescription;
        public string AssessmentDescription
        {
            get { return _AssessmentDescription; }
            set { if (this._AssessmentDescription != value) { _AssessmentDescription = value; } }
        }

        private string _AssessmentInstructionTop;
        public string AssessmentInstructionTop
        {
            get { return _AssessmentInstructionTop; }
            set { if (this._AssessmentInstructionTop != value) { _AssessmentInstructionTop = value; } }
        }

        private string _AssessmentInstructionBottom;
        public string AssessmentInstructionBottom
        {
            get { return _AssessmentInstructionBottom; }
            set { if (this._AssessmentInstructionBottom != value) { _AssessmentInstructionBottom = value; } }
        }

        private string _cFForReviewEmail;
        public string CFForReviewEmail
        {
            get { return _cFForReviewEmail; }
            set { if (this._cFForReviewEmail != value) { _cFForReviewEmail = value; } }
        }

        private System.DateTime _dateLastReviewSent;
        public System.DateTime DateLastReviewSent
        {
            get { return _dateLastReviewSent; }
            set { if (this._dateLastReviewSent != value) { _dateLastReviewSent = value; } }
        }

        private string _reviewEmail;
        public string ReviewEmail
        {
            get { return _reviewEmail; }
            set { if (this._reviewEmail != value) { _reviewEmail = value; } }
        }

        private AssessmentDates.AssessmentApprovalStatus _approvalStatus;
        public AssessmentDates.AssessmentApprovalStatus ApprovalStatus
        {
            get { return _approvalStatus; }
            set { if (this._approvalStatus != value) { _approvalStatus = value; } }
        }

        private string _approvedById;
        public string ApprovedById
        {
            get { return _approvedById; }
            set { if (this._approvedById != value) { _approvedById = value; } }
        }

        private System.DateTime _dateApproved;
        public System.DateTime DateApproved
        {
            get { return _dateApproved; }
            set { if (this._dateApproved != value) { _dateApproved = value; } }
        }


        private string _buttonPrintTxt;
        public string ButtonPrintTxt
        {
            get { return _buttonPrintTxt; }
            set { if (this._buttonPrintTxt != value) { _buttonPrintTxt = value; } }
        }


        private string _buttonNextTxt;
        public string ButtonNextTxt
        {
            get { return _buttonNextTxt; }
            set { if (this._buttonNextTxt != value) { _buttonNextTxt = value; } }
        }

        private string _buttonPreviousTxt;
        public string ButtonPreviousTxt
        {
            get { return _buttonPreviousTxt; }
            set { if (this._buttonPreviousTxt != value) { _buttonPreviousTxt = value; } }
        }

        private string _buttonSubmitTxt;
        public string ButtonSubmitTxt
        {
            get { return _buttonSubmitTxt; }
            set { if (this._buttonSubmitTxt != value) { _buttonSubmitTxt = value; } }
        }

        private string _buttonSaveTxt;
        public string ButtonSaveTxt
        {
            get { return _buttonSaveTxt; }
            set { if (this._buttonSaveTxt != value) { _buttonSaveTxt = value; } }
        }

        private string _buttonExitTxt;
        public string ButtonExitTxt
        {
            get { return _buttonExitTxt; }
            set { if (this._buttonExitTxt != value) { _buttonExitTxt = value; } }
        }
        
        private string _languageLogoPath;
        public string LanguageLogoPath
        {
            get { return _languageLogoPath; }
            set { if (this._languageLogoPath != value) { _languageLogoPath = value; } }
        }       



    }

    
}