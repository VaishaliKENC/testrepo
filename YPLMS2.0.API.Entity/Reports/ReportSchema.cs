/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Ashish and Shailesh patil
* Created:<15/10/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity.ReportSchema
{
   [Serializable] public class Common
    {
        //Columns
        public const string COL_TOTAL_COUNT = "TotalCount";

        //Procedures
        public const string PARA_DUE_DATE_FROM = "@DueDateFrom";
        public const string PARA_DUE_DATE_TO = "@DueDateTo";
        public const string PARA_EXPIRY_DATE_FROM = "@ExpiryDateFrom";
        public const string PARA_EXPIRY_DATE_TO = "@ExpiryDateTo";
        public const string PARA_SHOW_EXPIRED = "@ShowExpired";
        public const string PARA_SHOW_EXPIRED_NEXT_MONTH = "@ShowExpiredNextMonth";
        public const string PARA_DATE_CREATED_FROM = "@DateCreatedFrom";
        public const string PARA_DATE_CREATED_TO = "@DateCreatedTo";
        public const string PARA_CREATED_BY = "@CreatedById";
        public const string PARA_CLIENT_ID = "@ClientId";
        public const string PARA_REQUESTED_BY_ID = "@RequestedById";
        public const string PARA_CUSTOM_REPORT_ID = "@CustomReportId";
        public const string PARA_SORT_EXP = "@SortExp";
        public const string PARA_SHOW_ARACHIVE_DATA = "@ShowArchivedData";

        public const string PARA_CompletionDate_From = "@CompletionDateFrom";
        public const string PARA_CompletionDate_To = "@CompletionDateTo";

    }

    [Serializable]
   public class MOHReport
   {
       //Parameters
       public const string PARA_UserName = "@UserName";
       public const string PARA_Result = "@PResult";
   }

   [Serializable]
   public class CourseObjectiveTrackingReport
   {
       //Parameters
       public const string PARA_SYSTEMUSERGUID = "@strSystemUserGUID";
       public const string PARA_CONTENTMODULEID = "@StrContentModuleId";
   }
   [Serializable]
   public class CourseInteractionTrackingReport
   {
       //Parameters
       public const string PARA_SYSTEMUSERGUID = "@strSystemUserGUID";
       public const string PARA_CONTENTMODULEID = "@StrContentModuleId";
   }


   [Serializable] public class Client
    {
        //Parameters
        public const string PARA_NAME = "@ClientName";
    }

   [Serializable] public class CourseLibrary
    {
        //Parameters
        public const string PARA_NAME = "@CourseName";
        public const string PARA_COURSE_TYPE = "@CourseType";
    }

   [Serializable] public class Lookup
    {
        //Parameters
        public const string PARA_LOOKUP_TYPE = "@LookupType";        
    }

   [Serializable] public class UserAssignment
    {
        //Parameters
        public const string PARA_ACTIVITY_ID = "@ActivityID";
        public const string PARA_ACTIVITY_STATUS = "@ActivityStatus";
        public const string PARA_ASSIGNMENT_DATE_FROM = "@AssignmentDateFrom";
        public const string PARA_ASSIGNMENT_DATE_TO = "@AssignmentDateTo";
        public const string PARA_MANAGER_ID = "@ManagerId";
        public const string PARA_IS_DUE_DATE_PASSED = "@IsDueDatePassed";
        public const string PARA_INCLUDE_INACTIVE_LEARNERS = "@IncludeInactiveUsers";
        public const string PARA_INCLUDE_NONPREFERRED = "@IsNonPreferredAnswers";
        public const string PARA_UNIT_ID = "@UnitId";
        public const string PARA_ISFOR_EDITASSIGNMENT = "@IsForEditAssignment";
        public const string PARA_COMPLETED_DATE_FROM = "@CompletedFromDate";
        public const string PARA_COMPLETED_DATE_TO = "@CompletedToDate";
        public const string PARA_USER_ID  = "@SystemUserGUID";
        public const string PARA_CURRICULUM_ID = "@CurriculumId";
        public const string PARA_EXCLUDE_INNERACTIVITIES = "@ExcludeInnerActivites";
    }

   [Serializable] public class CourseLicensingByClient
    {
        //Parameters
        public const string PARA_LICENSE_ID = "@LicenseId";
    }

   [Serializable] public class CourseLicensingByCourse
    {
        //Parameters
        public const string PARA_COURSE_ID = "@CourseId";
    }
   [Serializable] public class ContentModule
    {
        //Parameters
        public const string PARA_CONTENT_MODULE_ID = "@ContentModuleId";
    }
   [Serializable] public class DelinquencyHistory
    {
        //Parameters
        public const string PARA_ACTIVITY_NAME = "@ActivityName";
        public const string PARA_ACTIVITY_TYPE = "@ActivityType";
    }
	public class PendingReview
	{
		//Parameters
		public const string PARA_ACTIVITY_NAME = "@ActivityName";
		public const string PARA_ACTIVITY_ID = "@ActivityId";
	}
   [Serializable] public class Learner
    {
        //Parameters
        //------- Learner Dump -------------------------------------------------------
        public const string PARA_IS_ACTIVE = "@IsActive";
        public const string PARA_ROLE_ID = "@RoleId";
        public const string PARA_LANGUAGE_ID = "@LanguageId";
        public const string PARA_HIRE_DATE_FROM = "@HireDateFrom";
        public const string PARA_HIRE_DATE_TO = "@HireDateTo";
        public const string PARA_CREATION_DATE_FROM = "@CreationDateFrom";
        public const string PARA_CREATION_DATE_TO = "@CreationDateTo";
        public const string PARA_DYNAMIC_WHERE_STANDARD_FIELDS = "@DynamicWhereStandardFields";
        public const string PARA_DYNAMIC_WHERE_CUSTOM_FIELDS = "@DynamicWhereCustomFields";
        public const string PARA_DYNAMIC_WHERE_BUSINESS_RULE = "@BusinessRuleId";

        //------- LearnerListByOrgGroup -----------------------------------------------
        public const string PARA_UNIT_ID = "@UnitId";
        //------- AssignedUsersByActivityStatus ---------------------------------------
        public const string PARA_SELECT_ATTEMPT = "@SelectAttempt";
        public const string PARA_ACTIVITY_NAME = "@ActivityName";
        public const string PARA_CURRICULUM_ID = "@CurriculumId";
        public const string PARA_ACTIVITY_TYPE = "@ActivityType";
        public const string PARA_COMPLETION_STATUS = "@CompletionStatus";
        public const string PARA_USER_NAME = "@UserName";
        public const string PARA_ASSIGNMENT_DATE_FROM = "@AssignmentDateFrom";
        public const string PARA_ASSIGNMENT_DATE_TO = "@AssignmentDateTo";
        public const string PARA_INCLUDE_INACTIVE_USERS = "@IncludeInactiveUsers";
        public const string PARA_SYSTEM_USER_GUID = "@SystemUserGuID";
        //-------- UsersByRole ----------------------------------------------------------        
        public const string PARA_ROLE = "@Role";
        //-------- NotAssignedUsersByActivity -------------------------------------------        
        public const string PARA_MANAGER_NAME = "@ManagerName";
        public const string PARA_LEVEL_ID = "@LevelId";
    
        public const string PARA_CUSTOM_REPORT_ID = "@CustomReportId";
        public const string PARA_ORG_UNIT_ID = "@UnitId";
        public const string PARA_BUSINESS_RULE_ID = "@BusinessRuleId";
        //-------- User Transcript Report -----------------------------------------------
        public const string REPORT_ID_TRANSCRIPT = "BuiltInUserTranscript";
    }

   [Serializable] public class ActivityCompletionProgress
    {
        //Parameters
        public const string PARA_ACTIVITY_NAME = "@ActivityName";
        public const string PARA_ACTIVITY_TYPE = "@ActivityType";
        public const string PARA_INCLUDE_INACTIVE_USERS = "@IncludeInactiveUsers";
        public const string PARA_BUSINESS_RULE_ID = "@BusinessRuleId";
    }

   [Serializable] public class Questionnaire
    {
        //Parameters
        //--------- AggregateResultsByQuestion -------------------------------------------
        public const string PARA_QUESTIONNAIRE_ID = "@QuestionnaireID";
        public const string PARA_INCLUDE_INACTIVE_USERS = "@IncludeInactiveUsers";
        //--------- NonpreferredAnswersForCertification -----------------------------------        
        public const string PARA_CERTIFICATION_ID = "@CertificationID";
        //--------- UserNonpreferredAnswersForCertification ------------------------------        
        public const string PARA_QUESTION_ID = "@QuestionId";

        public const string PARA_QUESTIONNAIRE_ATTEMPT_ID = "@QuestionnaireAttemptId";
        public const string PARA_LANGUAGE_ID = "@LanguageId";

        //added by Gitanjali 12.08.2010
        public const string PARA_COMPLETION_FROM_DATE = "@CompletionFromDate";
        public const string PARA_COMPLETION_TO_DATE = "@CompletionToDate";
        public const string PARA_ORGANIZATION_UNIT = "@UnitId";
        public const string PARA_BUSINESS_RULE_ID = "@BusinessRuleId";
        public const string PARA_ACTIVITY_STATUS = "@ActivityStatus";
        public const string PARA_USER_NAME = "@userName";
        public const string PARA_SYSTEM_USER_GUID = "@SystemUserGUID";
        public const string PARA_OPTION_TYPE = "@OptionType";
        public const string PARA_REQUESTED_BY_ID = "";

    }
   [Serializable]
   public class Assessment
   {
       //Parameters
       //--------- AggregateResultsByQuestion -------------------------------------------
       public const string PARA_ASSESSMENT_ID = "@AssessmentID";
       public const string PARA_INCLUDE_INACTIVE_USERS = "@IncludeInactiveUsers";
       public const string PARA_QUESTION_ID = "@QuestionId";

       public const string PARA_ASSESSMENT_ATTEMPT_ID = "@AssessmentAttemptId";
       public const string PARA_LANGUAGE_ID = "@LanguageId";

       public const string PARA_COMPLETION_FROM_DATE = "@CompletionFromDate";
       public const string PARA_COMPLETION_TO_DATE = "@CompletionToDate";
       public const string PARA_ORGANIZATION_UNIT = "@UnitId";

       public const string PARA_OPTION_TYPE = "@OptionType";
       public const string PARA_ASSESSMENT_COMPLETION_FROM_DATE = "@datefrom";
       public const string PARA_ASSESSMENT_COMPLETION_TO_DATE = "@dateto";
       public const string PARA_ASSESSMENT_REQUESTED_BY_ID = "@RequestedById";

   }

   [Serializable] public class Certification
    {
        //Parameters
        //--------- DetailedCertificationResultsbyUser -----------------------------------        
        public const string PARA_USER_NAME = "@userName";
        public const string PARA_INCLUDE_INACTIVE_USERS = "@IncludeInactiveUsers";

        //--------- DetailedCertificationUserResponses -----------------------------------        
        public const string PARA_CERTIFICATION_ID = "@CertificationID";
        public const string PARA_SYSTEM_USER_GUID = "@SystemUserGUID";

        public const string PARA_OPTION_TYPE = "@OptionType";
        //added by Gitanjali 11.08.2010
        public const string PARA_COMPLETION_FROM_DATE = "@CompletionFromDate";
        public const string PARA_COMPLETION_TO_DATE = "@CompletionToDate";
        public const string PARA_ORGANIZATION_UNIT = "@UnitId";
        //added by Gitanjali 22.11.2010
        public const string PARA_BUSINESS_RULE_ID = "@BusinessRuleId";
		public const string PARA_ACTIVITY_STATUS = "@ActivityStatus";

    }

   [Serializable] public class ReportingTool
    {
        //Parameters
        public const string PARA_REPORT_ID = "@ReportId";
        public const string PARA_GROUP_FIELD_VALUE = "@GroupFieldValue";
        public const string PARA_WHERE_CLAUSE = "@WhereClause";
        public const string PARA_GROUP_FIELD_TYPE = "@GroupFieldType";
    }

    [Serializable]
    public class ILTReport
    {
        public const string PARA_SESSIONNAME = "@SessionName";
        public const string PARA_EVENTNAME = "@EventName";
        public const string PARA_MODULENAME = "@ModuleName";
        public const string PARA_SEARCHNAME = "@SearchName";
    }

   [Serializable]
   public class ClassroomTrainingSession
   {
       //Parameters
       public const string PARA_SESSIONID = "@SessionId";
       public const string PARA_PROGRAMID = "@ProgramId";
       public const string PARA_PROGRAMNAME = "@ProgramName";
       public const string PARA_SPEAKERNAME = "@SpeakerName";
       public const string PARA_SESSIONNAME = "@SessionName";
       public const string PARA_SESSIONTYPEID = "@SessionTypeId";
       public const string PARA_SESSIONLOCATIONID = "@SessionLocationId";
       public const string PARA_SESSIONLOCATIONNAME = "@SessionLocationName";
       public const string PARA_SESSIONFROMDATE = "@SessionFromDate";
       public const string PARA_SESSIONTODATE = "@SessionToDate";
       public const string PARA_SESSIONSTATUS = "@SessionStatus";
       public const string PARA_SPEAKERID = "@SpeakerIds";
       public const string PARA_MAXREGISTRATIONS = "@MaxRegistrations";
       public const string PARA_MINREGISTRATIONS = "@MinRegistrations";
       public const string PARA_SELFNOMINATION = "@SelfNomination";
       public const string PARA_WAITLISTING = "@Waitlisting";
       public const string PARA_LASTDATEOFNOMINATION = "@LastDateOfNomination";
       public const string PARA_LASTDATEOFCANCELLATION = "@LastDateOfCancellation";
       public const string PARA_SESSIONREMINDER = "@SessionReminder";
       public const string PARA_SESSIONCOST = "@SessionCost";
       public const string PARA_SESSIONFROMTIME = "@SessionFromTime";
       public const string PARA_SESSIONTOTIME = "@SessionToTime";
       public const string PARA_SESSIONRESOURSEID = "@SupportResourceIds";
       public const string PARA_MAXWAITLISTING = "@MaxWaitlisting";
       public const string PARA_EVENTSTATUS = "@Status";
       public const string PARA_SESSION_TRAINING_NAME = "@TrainingTitle";
   }

   [Serializable]
   public class OrderHistory
   {
       //PARAMETERS
       public const string PARA_ORDERID = "@OrderID";
       public const string PARA_ORDERDATE = "@OrderDate";
       public const string PARA_TRANSANCTIONID = "@TransanctionID";
       public const string PARA_SYSTEMUSERGUID = "@SystemUserGUID";
       public const string PARA_SIGNUPID = "@SignUpID";
       public const string PARA_TRANSACTIONSTATUS = "@TransactionStatus";
       public const string PARA_PRODUCTID = "@ProductId";
       public const string PARA_LOCATIONID = "@LocationID";
       public const string PARA_COUPONCODE = "@CouponCode";
       public const string PARA_AMOUNT = "@Amount";
       public const string PARA_CURRENCY = "@Currency";
       public const string PARA_ORDERFEE = "@OrderFee";
       public const string PARA_ORDERDESCRIPTION = "@OrderDescription";
       public const string PARA_AMOUNTTOTAL = "@AmountTotal";
       public const string PARA_LICENSETYPE = "@LicenseType";
       public const string PARA_LICENSECOUNT = "@LicenseCount";
       public const string PARA_MERCHANTID = "@MerchantId";
       public const string PARA_CARDPAN = "@CardPAN";
       public const string PARA_CARDEXPIRYDATE = "@CardExpiryDate";
       public const string PARA_ISSUER = "@Issuer";
       public const string PARA_ISSUERCOUNTRY = "@IssuerCountry";
       public const string PARA_PAYMENTMETHOD = "@PaymentMethod";
       public const string PARA_CUSTOMERIPADDRESS = "@CustomerIPAddress";
       public const string PARA_ORDERFROMDATE = "@datefrom";
       public const string PARA_ORDERTODATE = "@dateto";
   //}

        public const string PARA_PriceFrom = "@PriceFrom";
        public const string PARA_PriceTo = "@PriceTo";
        public const string PARA_ProductTitle = "@ProductTitle";
        public const string PARA_ActivityType = "@ActivityType";
        public const string PARA_ProductCategory = "@ProdductCategory";


    }

    [Serializable]
   public class UserAssessmentQuestionTracking
   {
       public const string PARA_SYSTEMUSERGUID = "@SystemUserGUID";
       public const string PARA_CONTENTMODULE_ID = "@ContentModuleId";
   }

    [Serializable]
    public class NewsLetterScubscription
    {
        public const string PARA_FromDate = "@fromDate";
        public const string PARA_ToDate = "@ToDate";
    }
    [Serializable]
    public class AbandonededCartReport
    {
        public const string PARA_FromDate = "@FromDate";
        public const string PARA_ToDate = "@toDate";
    }

    [Serializable]

    public class ProductDetailReport
    {
        public const string PARA_ORDERFROMDATE = "@FromDate";
        public const string PARA_ORDERTODATE = "@toDate";
        public const string PARA_ProductTitle = "@ProductTitle";
        public const string PARA_SYSTEMUSERGUID = "@SystemUserGUID";
        public const string PARA_ActivityType = "@ActivityType";
        public const string PARA_ProductCategory = "@ProdductCategory";
        public const string PARA_ProductID = "@ProductId";
        public const string PARA_TRANSACTIONSTATUS = "@TransactionStatus";
        public const string PARA_LICENSETYPE = "@LicenseType";

    }
    [Serializable]
    public class EventDetailReport
    {
        public const string PARA_EVENTTITLE = "@EventTitle";
        public const string PARA_CREATEDBY= "@CreatedBy";
    }
    [Serializable]
    public class SessionDetailReport
    {
        public const string PARA_EVENTNAME = "@EventId";
        public const string PARA_CREATEDBY = "@CreatedBy";
        public const string PARA_SESSIONNAME = "@SessionId";
        public const string PARA_LOCATION = "@LocationId";


    }
    
}
