/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author: Ashish
* Created:<10/11/09>
* Last Modified:<dd/mm/yy>
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class StandardReport:BaseEntity 
    {
        /// <summary>
        /// Default Contructor tblStandardReportMaster
        /// <summary>
        public StandardReport()
        {
            _standardColumns = new List<StandardReportColumn>(); 
        }
        public new enum Method
        {
            Get
        }

        public new enum ListMethod
        {
            GetAll
        }
        private string _reportName;
        public string ReportName
        {
            get { return _reportName; }
            set { if (this._reportName != value) { _reportName = value; } }
        }

        private string _pageName;
        public string PageName
        {
            get { return _pageName; }
            set { if (this._pageName != value) { _pageName = value; } }
        }


        private string _featureId;
        public string FeatureId
        {
            get { return _featureId; }
            set { if (this._featureId != value) { _featureId = value; } }
        }
        private List<StandardReportColumn> _standardColumns;
        public List<StandardReportColumn> StandardColumns
        {
            get { return _standardColumns; }
        }

        public const string CLIENT_CONTACT_REPORT = "REP00001";
        public const string AGG_RESULT_BY_QUE_REPORT = "REP00002";//Aggregate Results by Question
        public const string DTL_CERT_RESULT_BY_USER_REPORT = "REP00003";//Detailed Certification Results by User
        public const string INC_OF_NON_PRF_ANSWER_REPORT = "REP00004";//Incidences of Non-Preferred Answers 
        public const string LEARNER_DUMP_REPORT = "REP00005";//Learner Dump Report 
        public const string STUDENT_LIST_BY_ORG_REPORT = "REP00006";//Student List by Organizational Group 
        public const string DELIN_HISTORY_REPORT = "REP00007"; //Delinquency History Report 
        public const string ACT_COMP_PROGRESS_REPORT = "REP00008"; //Activity Completion Progress Report 
        public const string VIEW_ASSIGNED_USER_BY_ACT_STATUS_REPORT = "REP00009"; //View Assigned Users by Activity Status
        public const string VIEW_NOT_ASSIGNED_USER_BY_ACT_REPORT = "REP00010"; //View Not Assigned Users by Activity
        public const string VIEW_USER_BY_ROLE_REPORT = "REP00011";    //View Users by Role
        public const string CLIENT_SUMMARY_REPORT = "REP00012"; //Client Summary Report 
        public const string COURSE_LIB_REPORT = "REP00013"; //Course Library Report
        public const string COURSE_LIC_BY_CLIENT_REPORT = "REP00014"; //Course Licensing by Client Report 
        public const string COURSE_LIC_BY_COURSE_REPORT = "REP00015"; //Course Licensing by Course Report 
        public const string SYSTEM_ERROR_LOG_REPORT = "REP00016"; //System Error Log Report 
		public const string PENDING_REVIEW_REPORT = "REP00017"; //Pending Review Email Report 
        public const string Session_REPORT = "REP00018";
        public const string ORDERHISTORY_REPORT = "REP00019";
        public const string DTL_QUEST_RESULT_BY_USER_REPORT = "REP00029"; //Detailed Questionnaire Results by User
        public const string NOMINATIONATTENDEEBYSESSION_REPORT = "REP00030";
        public const string VIRTUALTRAINING_STATUS_REPORT = "REP00031";
        public const string VIRTUALTRAINING_ATTENDEE_REPORT = "REP00032";
        
        public const string OBJECTIVE_TRACKING_REPORT = "REP00033"; //Objective Tracking Report
        public const string INTERACTION_TRACKING_REPORT = "REP00034"; //Interaction Tracking Report
        public const string VIEWALLASSIGNED_ACTIVITY_REPORT = "REP00037"; //ViewAllAssignedActivityStatus
        public const string CURRICULUM_DETAILS_REPORT = "REP00038"; //CurriculumDetailsReport
        public const string EVENT_DETAIL_REPORT = "REP00041";
        public const string SESSION_DETAIL_REPORT = "REP00042";
    }
}
