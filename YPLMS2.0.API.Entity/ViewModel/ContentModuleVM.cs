using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity.ViewModel;

namespace YPLMS2._0.API.Entity
{
    
    public class ContentModuleVM : ContentModuleLanguagesVM
    {
        /// <summary>
        /// Default Contructor
        ///</summary>

        public ContentModuleVM()
        {
            _entListContentModuleLanguages = new List<ContentModuleLanguagesVM>();
            //Sections = new Dictionary<string, CourseSection>();
        }

        private string ?_strRapidelCourseId;
        /// <summary>
        /// Content Module Name
        /// </summary>
        public string? RapidelCourseId
        {
            get { return _strRapidelCourseId; }
            set { if (this._strRapidelCourseId != value) { _strRapidelCourseId = value; } }
        }

        private string? _strPeruseCourseId;
        /// <summary>
        /// Content Module Name
        /// Added by : Samreen 23 Nov 2016
        /// </summary>
        public string? PeruseCourseId
        {
            get { return _strPeruseCourseId; }
            set { if (this._strPeruseCourseId != value) { _strPeruseCourseId = value; } }
        }


        private string? _strContentModuleEnglishName;
        /// <summary>
        /// Content Module Name
        /// </summary>
        public string? ContentModuleEnglishName
        {
            get { return _strContentModuleEnglishName; }
            set { if (this._strContentModuleEnglishName != value) { _strContentModuleEnglishName = value; } }
        }

        private string? _strContentModuleURL;
        /// <summary>
        /// Content Module URL
        /// </summary>
        public string? ContentModuleURL
        {
            get { return _strContentModuleURL; }
            set { if (this._strContentModuleURL != value) { _strContentModuleURL = value; } }
        }

        public string? AbsoluteFolderUrl { get; set; }

        private List<ContentModuleLanguagesVM>? _entListContentModuleLanguages;
        /// <summary>
        /// List of Theme Languages
        /// </summary>
        public List<ContentModuleLanguagesVM>? ContentModuleLanguages
        {
            get { return _entListContentModuleLanguages; }
            set { if (this._entListContentModuleLanguages != value) { _entListContentModuleLanguages = value; } }
        }

        private string? _strContentModuleTypeId;
        /// <summary>
        /// Content Module Type Id
        /// </summary>
        public string? ContentModuleTypeId
        {
            get { return _strContentModuleTypeId; }
            set { if (this._strContentModuleTypeId != value) { _strContentModuleTypeId = value; } }
        }

        private string? _strContentModuleSubTypeId;
        /// <summary>
        /// ContentModuleSubTypeId
        /// </summary>
        public string? ContentModuleSubTypeId
        {
            get { return _strContentModuleSubTypeId; }
            set { if (this._strContentModuleSubTypeId != value) { _strContentModuleSubTypeId = value; } }
        }

        private bool? _bIsAllocated;
        /// <summary>
        /// Is Allowcated
        /// </summary>
        public bool? IsAllocated
        {
            get { return _bIsAllocated; }
            set { if (this._bIsAllocated != value) { _bIsAllocated = value; } }
        }

        private bool? _bIsActive;
        /// <summary>
        /// Is Active
        /// </summary>
        public bool? IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
        }

        private bool? _bIsUploaded;
        /// <summary>
        /// Is Uploaded
        /// </summary>
        public bool? IsUploaded
        {
            get { return _bIsUploaded; }
            set { if (this._bIsUploaded != value) { _bIsUploaded = value; } }
        }

        private bool? _bScoreTracking;
        /// <summary>
        /// ScoreTracking
        /// </summary>
        public bool? ScoreTracking
        {
            get { return _bScoreTracking; }
            set { if (this._bScoreTracking != value) { _bScoreTracking = value; } }
        }

        private bool? _bQuestionResponseTracking;
        /// <summary>
        ///QuestionResponseTracking
        /// </summary>
        public bool? QuestionResponseTracking
        {
            get { return _bQuestionResponseTracking; }
            set { if (this._bQuestionResponseTracking != value) { _bQuestionResponseTracking = value; } }
        }

        private int? _iMasteryScore;
        /// <summary>
        /// MasteryScore
        /// </summary>
        public int? MasteryScore
        {
            get { return _iMasteryScore; }
            set { if (this._iMasteryScore != value) { _iMasteryScore = value; } }
        }

        private string? _strAVPath;
        /// <summary>
        /// AVPath
        /// </summary>
        public string? AVPath
        {
            get { return _strAVPath; }
            set { if (this._strAVPath != value) { _strAVPath = value; } }
        }

        private bool? _bCourseLaunchSameWindow;
        /// <summary>
        ///CourseLaunchSameWindow
        /// </summary>
        public bool? CourseLaunchSameWindow
        {
            get { return _bCourseLaunchSameWindow; }
            set { if (this._bCourseLaunchSameWindow != value) { _bCourseLaunchSameWindow = value; } }
        }

        private bool? _bCourseLaunchNewWindow;
        /// <summary>
        ///CourseLaunchNewWindow
        /// </summary>
        public bool? CourseLaunchNewWindow
        {
            get { return _bCourseLaunchNewWindow; }
            set { if (this._bCourseLaunchNewWindow != value) { _bCourseLaunchNewWindow = value; } }
        }

        private bool? _bAllowScroll;
        /// <summary>
        ///AllowScroll
        /// </summary>
        public bool? AllowScroll
        {
            get { return _bAllowScroll; }
            set { if (this._bAllowScroll != value) { _bAllowScroll = value; } }
        }

        private bool? _bAllowResize;
        /// <summary>
        ///AllowResize
        /// </summary>
        public bool? AllowResize
        {
            get { return _bAllowResize; }
            set { if (this._bAllowResize != value) { _bAllowResize = value; } }
        }

        private int? _iCourseWindowWidth;
        /// <summary>
        /// CourseWindowWidth
        /// </summary>
        public int? CourseWindowWidth
        {
            get { return _iCourseWindowWidth; }
            set { if (this._iCourseWindowWidth != value) { _iCourseWindowWidth = value; } }
        }

        private int? _iCourseWindowHeight;
        /// <summary>
        /// CourseWindowHeight
        /// </summary>
        public int? CourseWindowHeight
        {
            get { return _iCourseWindowHeight; }
            set { if (this._iCourseWindowHeight != value) { _iCourseWindowHeight = value; } }
        }

        private bool? _bIsPrintCertificate;
        /// <summary>
        ///IsPrintCertificate
        /// </summary>
        public bool? IsPrintCertificate
        {
            get { return _bIsPrintCertificate; }
            set { if (this._bIsPrintCertificate != value) { _bIsPrintCertificate = value; } }
        }

        private bool? _bIsCourseSessionNoExpiry;
        /// <summary>
        /// IsCourseSessionNoExpiry
        /// </summary>
        public bool? IsCourseSessionNoExpiry
        {
            get { return _bIsCourseSessionNoExpiry; }
            set { if (this._bIsCourseSessionNoExpiry != value) { _bIsCourseSessionNoExpiry = value; } }
        }

        private string? _strImanifestUrl;
        /// <summary>
        /// ImanifestUrl
        /// </summary>
        public string? ImanifestUrl
        {
            get { return _strImanifestUrl; }
            set { if (this._strImanifestUrl != value) { _strImanifestUrl = value; } }
        }

        private string? _strCourseGroupId;
        /// <summary>
        /// CourseGroupId
        /// </summary>
        public string? CourseGroupId
        {
            get { return _strCourseGroupId; }
            set { if (this._strCourseGroupId != value) { _strCourseGroupId = value; } }
        }

        private bool? _IsShortLanguageCode;
        /// <summary>
        /// Content Module Name
        /// </summary>
        public bool? IsShortLanguageCode
        {
            get { return _IsShortLanguageCode; }
            set { if (this._IsShortLanguageCode != value) { _IsShortLanguageCode = value; } }
        }


        private bool? _IsCourseModifiedByAdmin;
        /// <summary>
        /// Content Module Name
        /// </summary>
        public bool? IsCourseModifiedByAdmin
        {
            get { return _IsCourseModifiedByAdmin; }
            set { if (this._IsCourseModifiedByAdmin != value) { _IsCourseModifiedByAdmin = value; } }
        }

        //-aw 6/15/2011 Added course protocol
        private string? _protocol;

        public string? Protocol
        {
            get { return _protocol; }
            set { if (this._protocol != value) { _protocol = value; } }
        }

        private bool? _IsAssessment;
        public bool? IsAssessment
        {
            get { return _IsAssessment; }
            set { if (this._IsAssessment != value) { _IsAssessment = value; } }
        }

        private bool? _IsMiddlePage;
        public bool? IsMiddlePage
        {
            get { return _IsMiddlePage; }
            set { if (this._IsMiddlePage != value) { _IsMiddlePage = value; } }
        }

        private string? _systemUserGUID;
        public string? SystemUserGUID
        {
            get { return _systemUserGUID; }
            set { if (this._systemUserGUID != value) { _systemUserGUID = value; } }
        }

        private bool? _bIsHTML5;
        /// <summary>
        /// Is Uploaded
        /// </summary>
        public bool? IsHTML5
        {
            get { return _bIsHTML5; }
            set { if (this._bIsHTML5 != value) { _bIsHTML5 = value; } }
        }


        private bool? _bKeepZipFile;
        /// <summary>
        /// Is Uploaded
        /// </summary>
        public bool? KeepZipFile
        {
            get { return _bKeepZipFile; }
            set { if (this._bKeepZipFile != value) { _bKeepZipFile = value; } }
        }

       

       // public Dictionary<string, CourseSection>? Sections { get; set; }

        //public Lesson GetLessonById(string id)
        //{
        //    return (from section in Sections.Values where section.Lessons.ContainsKey(id) select section.Lessons[id]).FirstOrDefault();
        //}

       // public int? TotalLessons { get { return Sections.Values.Sum(s => s.Lessons.Count); } }//  public int TotalLessons { get { return Sections.Values.Sum(s => (from p in s.Lessons where p.Value.MenuItem.IsEnabled == true select p).Count()); } }

        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }

        public string? CategoryId { get; set; }
        public string? SubCategoryId { get; set; }

        List<ContentModuleMapping>? eCourseList;



        private bool? _IsMasterContent;
        /// <summary>
        /// Content Module Name
        /// </summary>
        public bool? IsMasterContent
        {
            get { return _IsMasterContent; }
            set { if (this._IsMasterContent != value) { _IsMasterContent = value; } }
        }

        private bool? _CouseAssessmentFlag;
        /// <summary>
        /// CouseAssessmentFlag
        /// </summary>
        public bool? CouseAssessmentFlag
        {
            get { return _CouseAssessmentFlag; }
            set { if (this._CouseAssessmentFlag != value) { _CouseAssessmentFlag = value; } }
        }

        private bool? _LockAssessment;
        /// <summary>
        /// _LockAssessment
        /// </summary>
        public bool? LockAssessment
        {
            get { return _LockAssessment; }
            set { if (this._LockAssessment != value) { _LockAssessment = value; } }
        }

        private int? _iFirstFailedAttemptLockHrs;
        public int? FirstFailedAttemptLockHrs
        {
            get { return _iFirstFailedAttemptLockHrs; }
            set { if (this._iFirstFailedAttemptLockHrs != value) { _iFirstFailedAttemptLockHrs = value; } }
        }

        private int? _iAdminUnlockHrs;
        public int? AdminUnlockHrs
        {
            get { return _iAdminUnlockHrs; }
            set { if (this._iAdminUnlockHrs != value) { _iAdminUnlockHrs = value; } }
        }

        private bool? _SendEmailToLearner;
        public bool? SendEmailToLearner
        {
            get { return _SendEmailToLearner; }
            set { if (this._SendEmailToLearner != value) { _SendEmailToLearner = value; } }
        }

        private bool? _SendEmail;
        public bool? SendEmail
        {
            get { return _SendEmail; }
            set { if (this._SendEmail != value) { _SendEmail = value; } }
        }

        private bool? _SendEmailToManager;
        public bool? SendEmailToManager
        {
            get { return _SendEmailToManager; }
            set { if (this._SendEmailToManager != value) { _SendEmailToManager = value; } }
        }
        private bool? _SendEmailToRegionalAdmin;
        public bool? SendEmailToRegionalAdmin
        {
            get { return _SendEmailToRegionalAdmin; }
            set { if (this._SendEmailToRegionalAdmin != value) { _SendEmailToRegionalAdmin = value; } }
        }
        private bool? _SendEmailToMore;
        public bool? SendEmailToMore
        {
            get { return _SendEmailToMore; }
            set { if (this._SendEmailToMore != value) { _SendEmailToMore = value; } }
        }

        private string? _sendEmailTo;
        public string? SendEmailTo
        {
            get { return _sendEmailTo; }
            set { if (this._sendEmailTo != value) { _sendEmailTo = value; } }
        }

        private string? _showAssessmentResult;
        public string? ShowAssessmentResult
        {
            get { return _showAssessmentResult; }
            set { if (this._showAssessmentResult != value) { _showAssessmentResult = value; } }
        }

        private bool? _LockAttemptAssessment;
        public bool? LockAttemptAssessment
        {
            get { return _LockAttemptAssessment; }
            set { if (this._LockAttemptAssessment != value) { _LockAttemptAssessment = value; } }
        }

        private int? _iAfterNumberofAttempts;
        public int? AfterNumberofAttempts
        {
            get { return _iAfterNumberofAttempts; }
            set { if (this._iAfterNumberofAttempts != value) { _iAfterNumberofAttempts = value; } }
        }              

        public string? FTPCoursePath { get; set; }
        public string? Isedit { get; set; }

        private string? _thumbnailImgRelativePath;
        public string? ThumbnailImgRelativePath
        {
            get { return _thumbnailImgRelativePath; }
            set { if (this._thumbnailImgRelativePath != value) { _thumbnailImgRelativePath = value; } }
        }
    }
}
