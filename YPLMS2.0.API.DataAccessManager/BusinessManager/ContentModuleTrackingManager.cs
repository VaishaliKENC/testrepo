using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.Entity.ViewModel;
using YPLMS2._0.API.YPLMS.Services;
using YPLMS2._0.API.YPLMS.Services.Messages;
using static System.Net.Mime.MediaTypeNames;
using ContentModule = YPLMS2._0.API.Entity.ContentModule;
using LaunchSite = YPLMS2._0.API.Entity.LaunchSite;

namespace YPLMS2._0.API.DataAccessManager
{
    public class ContentModuleTrackingManager:IContentModuleTrackingManager<ContentModuleSession>
    {
        private readonly string _userDataPath;
        private readonly ICacheProvider _cache;
        string _strConnString = string.Empty;
        CustomException _expCustom;
        string _strMessageId = YPLMS.Services.Messages.Content.CONT_MOD_BL_ERROR;
        
        SQLObject _sqlObject = null;
        //public ContentModuleTrackingManager(string userDataPath, ICacheProvider cache)
        //{
        //    _userDataPath = userDataPath;
        //    _cache= cache;
        //}
        public ContentModuleTracking GetTracking(string clientId, ContentModuleSession session)
        {
            try
            {
                ExceptionManager.Log("GetTracking->GetContentModuleLessonTracking call");
                _strConnString = _sqlObject.GetClientDBConnString(clientId);
                var contModTracking = new ContentModuleTracking
                {
                    UserID = session.SystemUserGuid,
                    ContentModuleId = session.ContentModuleId,
                    ClientId = clientId
                };
                if (session.Attempt.HasValue)
                {
                    contModTracking.ID = session.Assignment.ID + "-" + session.SystemUserGuid + "-" + session.Attempt.Value;
                }
                contModTracking.IsResume = session.IsReview;
                contModTracking.ContentType = session.ContentModule.ContentModuleTypeId;
                if (contModTracking.ContentType == ActivityContentType.Scorm2004.ToString())
                {
                    contModTracking = GetUserDataXml2004(contModTracking);
                }
                else
                {
                    contModTracking = GetContentModuleLessonTracking(contModTracking);
                }
                contModTracking.IsForAdminPreview = (session.LaunchSite == LaunchSite.Admin);
                contModTracking.UserID = session.SystemUserGuid;
                contModTracking.UserFirstLastName = session.Learner.LastName + ", " + session.Learner.FirstName;
                contModTracking.ContentModuleId = session.ContentModuleId;
                contModTracking.SessionId = session.SessionId;
                contModTracking.TotalNoOfPages = session.ContentModule.TotalLessons;
                contModTracking.ContentType = session.ContentModule.ContentModuleTypeId;

                return contModTracking;

            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }

        }

        private ContentModuleTracking GetContentModuleLessonTracking(ContentModuleTracking pEntContModTracking)
        {
            ExceptionManager.Log("GetContentModuleLessonTracking->GetUserDataXml call");
            ContentModuleTracking entContModTrackingReturn = null;
            entContModTrackingReturn = GetUserDataXml(pEntContModTracking);
            if (String.IsNullOrEmpty(entContModTrackingReturn.UserDataXML))
            {
                entContModTrackingReturn.LessonTracking = new Dictionary<string, LessonTracking>();
            }
            else
            {
                ExceptionManager.Log("GetContentModuleLessonTracking->Create call");
                ILessonTrackingSerializer serializer = new LessonTrackingSerializerFactory().Create(pEntContModTracking.ContentType,
                    entContModTrackingReturn.ContentModuleId, entContModTrackingReturn.UserID, entContModTrackingReturn.UserFirstLastName);
                ExceptionManager.Log("GetContentModuleLessonTracking->ReadLessonTracking call");
                entContModTrackingReturn.LessonTracking = serializer.ReadLessonTracking(entContModTrackingReturn.UserDataXML);

                pEntContModTracking.NoOfPagesCompleted = entContModTrackingReturn.LessonTracking.Values.Count(l => l.IsComplete);
            }
            return entContModTrackingReturn;
        }

                  

         private ContentModuleTracking GetUserDataXml2004(ContentModuleTracking pEntContModTracking)
         {
            ContentModuleTracking entContModTrackingReturn = null;
            entContModTrackingReturn = GetUserDataXml(pEntContModTracking);
            if (String.IsNullOrEmpty(entContModTrackingReturn.UserDataXML))
            {
                entContModTrackingReturn.LessonTracking2004 = new Dictionary<string, LessonTracking2004>();
            }
            else
            {
                //ILessonTrackingSerializer2004 serializer = new LessonTrackingSerializerFactory2004().Create(pEntContModTracking.ContentType,
                //    entContModTrackingReturn.ContentModuleId, entContModTrackingReturn.UserID, entContModTrackingReturn.UserFirstLastName);

                //entContModTrackingReturn.LessonTracking2004 = serializer.ReadLessonTracking(entContModTrackingReturn.UserDataXML);

                ILessonTrackingSerializer2004 serializer2004 = new LessonTrackingSerializerFactory2004().Create(pEntContModTracking.ContentType,
                    entContModTrackingReturn.ContentModuleId, entContModTrackingReturn.UserID, entContModTrackingReturn.UserFirstLastName);
                entContModTrackingReturn.LessonTracking2004 = serializer2004.ReadLessonTracking(entContModTrackingReturn.UserDataXML);
                pEntContModTracking.NoOfPagesCompleted = entContModTrackingReturn.LessonTracking2004.Values.Count(l => l.IsComplete);
            }
            return entContModTrackingReturn;
        }

        public  ContentModuleTracking GetUserDataXml(ContentModuleTracking pEntContModTracking)
        {
            var entContModTrackingReturn =
               new ContentModuleTrackingAdaptor().GetContentModuleTrackingByID(pEntContModTracking);
            //var entContModTrackingReturn = base.GetUserDataXml(pEntContModTracking);
            var trackingDirectory = String.Format("{0}\\{1}\\{2}\\{3}", _userDataPath,
                                      entContModTrackingReturn.ClientId, entContModTrackingReturn.ContentModuleId,
                                      entContModTrackingReturn.UserID);
            if (!Directory.Exists(trackingDirectory)) Directory.CreateDirectory(trackingDirectory);
            var trackingFile = trackingDirectory + @"\" + entContModTrackingReturn.ID + "_UserDataXML.xml";
            if (File.Exists(trackingFile))
                entContModTrackingReturn.UserDataXML = File.ReadAllText(trackingFile);
            return entContModTrackingReturn;
        }

        public ContentModule GetContentModule(string clientId, string courseId)
        {
            
            try
            {
                //ExceptionManager.Log("GetContentModuleByID_CoursePlayer Call");
                var course = new ContentModuleAdaptor().GetContentModuleByID_CoursePlayer(new ContentModule { ClientId = clientId, ID = courseId });
                _sqlObject = new SQLObject();
                _strConnString = _sqlObject.GetClientDBConnString(clientId);
                //ExceptionManager.Log("GetContentModuleByID_CoursePlayer done");
                //ExceptionManager.Log("GetManifestPath call");
                var manifestPath = GetManifestPath(clientId, course);                
                var contentType = (ActivityContentType)Enum.Parse(typeof(ActivityContentType), course.ContentModuleTypeId);
                //ExceptionManager.Log("ReadSectionsFromManifest call");
                course.Sections = ReadSectionsFromManifest(manifestPath, course.MasteryScore, contentType);             
                return course;
            }
            catch (Exception expCommon)
            {
                _expCustom = new CustomException(_strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, expCommon, true, _strConnString);
                throw _expCustom;
            }

        }

        private string GetManifestPath(string clientId, ContentModule course)
        {
            var fileHandler = new FileHandler(clientId);
            string rootSharedPath = fileHandler.RootSharedPath;
            string contentFolderPath = rootSharedPath + course.ContentModuleURL.Replace("/", @"\\");
            course.AbsoluteFolderUrl = fileHandler.RootHTTPUrl + course.ContentModuleURL;

            if (String.IsNullOrEmpty(course.ImanifestUrl))
            {
                return contentFolderPath + "\\\\imsmanifest.xml";
            }
            return rootSharedPath + course.ImanifestUrl.Replace("/", @"\");
        }

        private Dictionary<string, CourseSection> ReadSectionsFromManifest(string manifestPath, int masteryScore, ActivityContentType courseType)
        {
            var sections = new Dictionary<string, CourseSection>();
            var manifestXml = new XmlDocument();
            ExceptionManager.Log("ReadSectionsFromManifest->StripDocumentNamespace call");
            try
            {
                manifestXml.Load(manifestPath);
                manifestXml = new XMLLib().StripDocumentNamespace(manifestXml);
            }
            catch (Exception ex)
            {
                var cex = new CustomException("Could not load manifest XML: " + manifestPath, "XML load failure",
                                              ExceptionSeverityLevel.Critical, ex, true);
                return null;
            }
            ExceptionManager.Log("ReadSectionsFromManifest->StripDocumentNamespace done");
            ExceptionManager.Log("ReadSectionsFromManifest->SelectNodes call");
            XmlNodeList sectionNodeList =
                manifestXml.SelectNodes("/manifest/organizations/organization");
            if (sectionNodeList == null) return sections;

            var lessonReaderFactory = new LessonReaderFactory(courseType);

            int sectionNumber = 1;
            foreach (XmlNode sectionNode in sectionNodeList)
            {
                var titleNode = sectionNode.SelectSingleNode("title");
                string title = titleNode == null ? String.Empty : titleNode.InnerText;
                string identifier = ((XmlElement)sectionNode).GetAttribute("identifier");
                var lessons = new Dictionary<string, Lesson>();
                XmlNodeList lessonNodeList = sectionNode.SelectNodes("//item[@identifierref | @resourceref]");
                if (lessonNodeList != null)
                {
                    int lessonNumber = 1;
                    foreach (XmlNode lessonNode in lessonNodeList)
                    {
                        ExceptionManager.Log("ReadSectionsFromManifest->GetLessonReader call");

                        var lessonReader = lessonReaderFactory.GetLessonReader(lessonNode, masteryScore);
                        ExceptionManager.Log("ReadSectionsFromManifest->ReadLesson call");

                        var lesson = lessonReader.ReadLesson();
                        lesson.SortOrder = lessonNumber;                        
                        lessons.Add(lesson.Identifier, lesson);
                        lessonNumber++;
                    }
                }

                sections.Add(identifier, new CourseSection
                {
                    Lessons = lessons,
                    Identifier = identifier,
                    Title = title,
                    SortOrder = sectionNumber
                });
                sectionNumber++;
            }
            return sections;
        }

        public string GetMasteryScoreFromMasterContentXML( string _strSelectedClientId, string hdnFTPCoursePath, string strCourseId)
        {
            MasterContentsReader srvMasterContentsReader = new MasterContentsReader(_strSelectedClientId);
            string MasteryScore = string.Empty;
            try
            {
                string pstrCoursePath = string.Empty;

                string strCoursePath = hdnFTPCoursePath;// txtFtpCoursePath.Text;
                //string strCourseFolder = strCoursePath.Substring(strCoursePath.IndexOf(FileHandler.COURSE_FOLDER_PATH));
                string strCourseFolder = strCoursePath.Substring(strCoursePath.IndexOf(FileHandler.CLIENTS_FOLDER_PATH));
                pstrCoursePath = strCourseFolder;
                if (strCoursePath.ToLower().IndexOf("/imsmanifest.xml") != -1)
                {
                    strCourseFolder = strCourseFolder.Substring(0, strCourseFolder.Length - 16);
                    pstrCoursePath = strCourseFolder;
                }
                // //string strClientId = EncryptionManager.Decrypt(ConfigurationManager.AppSettings[YPLMS.Entity.Client.BASE_CLIENT_KEY]);
                FileHandler fileHandler = new FileHandler(_strSelectedClientId);
                string strMasterContents = fileHandler.RootSharedPath.Replace("\\\\", "\\");
                pstrCoursePath = pstrCoursePath.Replace("/", @"\");
                strMasterContents += pstrCoursePath;
                string strMetaDataXml = strMasterContents;
                //strMasterContents += @"\mastercontents.xml";
                XMLLib xmlLib = new XMLLib();
                string strmaifestfile = strMasterContents + "\\imsmanifest.xml";

                XmlDocument xDocMF = new XmlDocument();
                xmlLib.fOpenFreeXMLDoc(ref xDocMF, strmaifestfile);
                XmlElement xnRoot = xDocMF.DocumentElement;   //.Attributes["CourseType"].Value
                if (xnRoot != null && xnRoot.Attributes["CourseType"] != null && xnRoot.Attributes["CourseType"].Value.ToLower() == "murapidel")
                {
                    if (xnRoot.Attributes["identifier"] != null)
                    {
                        string strMastercontentPath = xnRoot.Attributes["identifier"].Value.ToLower();

                        strMasterContents += @"\" + strMastercontentPath.Split('_')[0] + @"\mastercontents.xml";
                    }
                }
                else
                    strMasterContents += @"\mastercontents.xml";

                xmlLib = new XMLLib();
                if (File.Exists(strMasterContents))
                {
                    srvMasterContentsReader.ReadCourseMasterContents(strCourseFolder, strCourseId, strMasterContents);
                    if (!(string.IsNullOrEmpty(YPLMS.Services.Common.CourseMasteryScore)))
                        MasteryScore = YPLMS.Services.Common.CourseMasteryScore;
                }
            }
            catch (Exception ex)
            {
            }
            return MasteryScore;
        }

        public string SaveContentMaster(string strCourseId, string psGroupId, ContentModule entContMod, ContentModule pEntContModule, DataTable ptblMetaData, bool hidFldEdit, string hdnFTPCoursePath)
        {
            string strStatusMsg = string.Empty;
            string strRedirectTo = string.Empty;
            bool IsAssementError = false;

            //ContentModule entContMod = new ContentModule();
            ContentModuleAdaptor contentModuleAdaptor = new ContentModuleAdaptor();
            CourseConfigurationAdaptor courseConfigurationAdaptor = new CourseConfigurationAdaptor();
            Entity.Client entClient = new Entity.Client();
            ClientDAM clientDAM = new ClientDAM();
            entClient.ID = pEntContModule.ClientId;
            entClient = clientDAM.GetClientByID(entClient);

           // if (_strSelectedClientId != _strBaseClientId)
                entContMod.IsCourseModifiedByAdmin = true;

            entContMod.ID = strCourseId;
            entContMod.CourseGroupId = psGroupId;
            entContMod.ClientId = entClient.ID;
            if (hidFldEdit)
            {
                entContMod = contentModuleAdaptor.GetContentModuleByID(entContMod);
            }
            // has been set above
           // if (rblCourseLockAssessment.SelectedIndex == 1)
            {
                //if (pEntContModule.ContentModuleEnglishName.Trim().StartsWith("Assessment"))
                    entContMod.ContentModuleEnglishName = pEntContModule.ContentModuleEnglishName.Trim();
                //else
                //    entContMod.ContentModuleEnglishName = "Assessment - " + pEntContModule.ContentModuleEnglishName.Trim();
            }
           // else
                //entContMod.ContentModuleEnglishName = pEntContModule.ContentModuleEnglishName.Trim(); // // uncommented by shrihari
            entContMod.ContentModuleTypeId = pEntContModule.ContentModuleTypeId;
            entContMod.ContentModuleSubTypeId = pEntContModule.ContentModuleSubTypeId;

            if (!hidFldEdit)
            {
                entContMod.IsActive = true;
            }
            entContMod.ClientId = pEntContModule.ClientId;
            entContMod.ContentModuleURL = string.Empty;
            entContMod.AllowResize = pEntContModule.AllowResize;
            entContMod.AllowScroll = pEntContModule.AllowScroll;
            
            entContMod.CourseLaunchNewWindow = pEntContModule.CourseLaunchNewWindow;
            entContMod.CourseLaunchSameWindow = pEntContModule.CourseLaunchSameWindow;


            //-aw 6/15/2011 Added course protocol
            entContMod.Protocol = pEntContModule.Protocol;
            //

            if (Convert.ToString(pEntContModule.CourseWindowWidth).Trim() != string.Empty)
            {
                entContMod.CourseWindowWidth = pEntContModule.CourseWindowWidth;
            }
            else
            {
                Entity.CourseConfiguration entCourseConfig = courseConfigurationAdaptor.GetConfiguration(pEntContModule.ClientId); //GetClientCourseSettings();
                entContMod.CourseWindowWidth = entCourseConfig.CourseWindowWidth;
            }
            if (Convert.ToString(pEntContModule.CourseWindowHeight).Trim() != string.Empty)
            {
                entContMod.CourseWindowHeight = pEntContModule.CourseWindowHeight;
            }
            else
            {
                Entity.CourseConfiguration entCourseConfig = courseConfigurationAdaptor.GetConfiguration(pEntContModule.ClientId); //GetClientCourseSettings();
                entContMod.CourseWindowHeight = entCourseConfig.CourseWindowHeight;
            }
            entContMod.ScoreTracking = pEntContModule.ScoreTracking;
            entContMod.IsPrintCertificate = pEntContModule.IsPrintCertificate;
            entContMod.IsCourseSessionNoExpiry = pEntContModule.IsCourseSessionNoExpiry;
            entContMod.IsShortLanguageCode = pEntContModule.IsShortLanguageCode;

            entContMod.IsAssessment = pEntContModule.IsAssessment;
            entContMod.IsMiddlePage = pEntContModule.IsMiddlePage ;
            entContMod.IsHTML5 = pEntContModule.IsHTML5;
            entContMod.KeepZipFile = pEntContModule.KeepZipFile;
            if (Convert.ToString(pEntContModule.MasteryScore) != string.Empty)
                entContMod.MasteryScore = Convert.ToInt32(pEntContModule.MasteryScore);
            //entContMod.MasteryScore = Convert.ToInt32(CommonMethods.RemoveSpecialChars(txtMasteryScore.Text.Trim()));
            else
                entContMod.MasteryScore = 0;
            string strFTP = hdnFTPCoursePath;// txtFtpCoursePath.Text;
            //if (!string.IsNullOrEmpty(strFTP) && strFTP.IndexOf(FileHandler.COURSE_FOLDER_PATH) > 0)
            if (!string.IsNullOrEmpty(strFTP) && strFTP.IndexOf(FileHandler.CLIENTS_FOLDER_PATH) > 0)
            {
                if (pEntContModule.ContentModuleTypeId.ToLower() != "noncompliant")
                {
                    if (pEntContModule.ContentModuleTypeId.ToLower() == "aicc")
                    {
                        strFTP = strFTP.Substring(strFTP.IndexOf(FileHandler.CLIENTS_FOLDER_PATH));
                        if ((strFTP.Split('/').Length - 1) > 3)
                        {
                            strFTP = strFTP.Substring(0, strFTP.LastIndexOf("/"));
                        }
                    }
                    else
                    {
                        //strFTP = strFTP.Substring(strFTP.IndexOf(FileHandler.COURSE_FOLDER_PATH));
                        strFTP = strFTP.Substring(strFTP.IndexOf(FileHandler.CLIENTS_FOLDER_PATH));
                        if (strFTP.ToLower().IndexOf("/imsmanifest.xml") != -1)
                        {
                            strFTP = strFTP.Substring(0, strFTP.Length - 16);
                        }
                    }
                }
                else
                {
                    //strFTP = strFTP.Substring(strFTP.IndexOf(FileHandler.COURSE_FOLDER_PATH));
                    if (pEntContModule.ContentModuleTypeId.ToLower() == "noncompliant")
                        strFTP = pEntContModule.ContentModuleURL.Substring(pEntContModule.ContentModuleURL.IndexOf(FileHandler.CLIENTS_FOLDER_PATH));
                    else
                        strFTP = strFTP.Substring(strFTP.IndexOf(FileHandler.CLIENTS_FOLDER_PATH));
                }
                entContMod.ContentModuleURL = strFTP;
            }
            else
            {
                //entContMod.ContentModuleURL = FileHandler.COURSE_FOLDER_PATH + "/" + strCourseId;
                entContMod.ContentModuleURL = FileHandler.CLIENTS_FOLDER_PATH + "/" + pEntContModule.ClientId + "/" + FileHandler.COURSE_FOLDER_PATH + "/" + strCourseId;
            }
            //Add Logic Course Assessment
            
            {
                entContMod.CouseAssessmentFlag = pEntContModule.CouseAssessmentFlag;

                /* if (chkLockAssessment.Checked)
                 {
                     entContMod.LockAssessment = chkLockAssessment.Checked;
                     entContMod.FirstFailedAttemptLockHrs = Convert.ToInt32(txtFirstAttemptLockHrs.Text);
                     entContMod.AdminUnlockHrs = Convert.ToInt32(txtAdminUnlockHrs.Text);
                 }*/
                //Samreen - Added code for Switch Position functionality
               // if (rblSwitchLockAssessment.SelectedIndex == 0)
                {
                    //entContMod.LockAssessment = chkLockAssessment.Checked;
                    entContMod.LockAssessment = pEntContModule.LockAssessment;
                    entContMod.FirstFailedAttemptLockHrs = Convert.ToInt32(pEntContModule.FirstFailedAttemptLockHrs);
                    entContMod.AdminUnlockHrs = Convert.ToInt32(pEntContModule.AdminUnlockHrs);
                    entContMod.LockAttemptAssessment = pEntContModule.LockAttemptAssessment;
                    entContMod.AfterNumberofAttempts = Convert.ToInt32(pEntContModule.AfterNumberofAttempts);
                }
               // else if (rblSwitchLockAssessment.SelectedIndex == 1)
                {
                    entContMod.LockAssessment = pEntContModule.LockAssessment;
                    entContMod.LockAttemptAssessment = pEntContModule.LockAttemptAssessment; 
                    entContMod.AfterNumberofAttempts = Convert.ToInt32(pEntContModule.AfterNumberofAttempts);
                    //entContMod.FirstFailedAttemptLockHrs = Convert.ToInt32(0);
                    //entContMod.AdminUnlockHrs = Convert.ToInt32(0);

                }


                entContMod.SendEmailToLearner = pEntContModule.SendEmailToLearner;
                //if (chkSendEmails.Checked)
                {
                    entContMod.SendEmail = pEntContModule.SendEmail;
                    entContMod.SendEmailToManager = pEntContModule.SendEmailToManager;
                    entContMod.SendEmailToRegionalAdmin = pEntContModule.SendEmailToRegionalAdmin;
                    entContMod.SendEmailToMore = pEntContModule.SendEmailToMore;
                    if (!string.IsNullOrEmpty(pEntContModule.SendEmailTo))
                    {
                        entContMod.SendEmailTo = pEntContModule.SendEmailTo.Trim();

                    }
                    else
                    {
                        entContMod.SendEmailTo = pEntContModule.SendEmailTo;
                    }

                    entContMod.ShowAssessmentResult = pEntContModule.ShowAssessmentResult;
                }
            }
            entContMod.ThumbnailImgRelativePath = pEntContModule.ThumbnailImgRelativePath;
            entContMod.ContentModuleDescription = pEntContModule.ContentModuleDescription;
            entContMod.ContentModuleKeyWords = pEntContModule.ContentModuleKeyWords;

            //End
            if (hidFldEdit)
            {
                entContMod.LastModifiedById = pEntContModule.LastModifiedById;
                entContMod.CreatedById = pEntContModule.CreatedById;
                entContMod.IsActive = pEntContModule.IsActive;
                
                {
                    entContMod.ID = pEntContModule.ID; // LMSSession.GetValue(ContentKeys.SESSION_COURSEID).ToString();
                    strCourseId = entContMod.ID;
                }
                entContMod = contentModuleAdaptor.EditContentModule(entContMod);
            }
            else
            {
                entContMod.CreatedById = pEntContModule.CreatedById;
                entContMod.LastModifiedById = pEntContModule.LastModifiedById;               
                {
                    entContMod.ID = strCourseId; // LMSSession.GetValue(ContentKeys.SESSION_COURSEID).ToString();                    
                }

                entContMod = contentModuleAdaptor.AddContentModule(entContMod);
            }
            if (entContMod != null && entContMod.ID == strCourseId)
            {
                //new Activity-Category Mapping code by  deepak dangat on 23-Dec-2013
               // SaveCategoryMapping(entContMod.ID, entContMod.ClientId, entContMod.ContentModuleTypeId);

                DataTable dtblMetaData = (DataTable)ptblMetaData;

                if (dtblMetaData != null)
                {
                    if (dtblMetaData.TableName != null && dtblMetaData.TableName != "")
                    {
                        var removeList = dtblMetaData.DefaultView;
                        removeList.RowFilter = "LanguageId='x-none'";
                        for (var i = 0; i <= removeList.Count - 1; i++)
                        {
                            dtblMetaData.Rows.Remove(removeList[i].Row);
                        }
                    }
                }
                if (dtblMetaData != null && dtblMetaData.Rows.Count > 0)
                {
                    DataRow[] drDefClientLang = dtblMetaData.Select("LanguageId = '" + Entity.Language.SYSTEM_DEFAULT_LANG_ID /* entClient.DefaultLanguageId */+ "'");
                    if (drDefClientLang.Length == 1)
                    {
                        drDefClientLang[0]["ContentModuleName"] = entContMod.ContentModuleEnglishName;
                        drDefClientLang[0]["ContentModuleDescription"] = ValidationManager.RemoveSpecialCharsForNameAndDescription(string.IsNullOrEmpty(pEntContModule.ContentModuleDescription) ? "" : pEntContModule.ContentModuleDescription.Trim());
                        drDefClientLang[0]["ContentModuleKeyWords"] = ValidationManager.RemoveSpecialChars(string.IsNullOrEmpty(pEntContModule.ContentModuleKeyWords) ? "" : pEntContModule.ContentModuleKeyWords.Trim());
                        //Update Entry from manifest file [Used for Multi-Organization courses ]
                        dtblMetaData.Rows[0]["ContentModuleName"] = entContMod.ContentModuleEnglishName;
                        dtblMetaData.Rows[0]["ContentModuleDescription"] = ValidationManager.RemoveSpecialCharsForNameAndDescription(string.IsNullOrEmpty(pEntContModule.ContentModuleDescription) ? "" : pEntContModule.ContentModuleDescription.Trim());
                        dtblMetaData.Rows[0]["ContentModuleKeyWords"] = ValidationManager.RemoveSpecialChars(string.IsNullOrEmpty(pEntContModule.ContentModuleKeyWords) ? "" : pEntContModule.ContentModuleKeyWords.Trim());
                    }
                    else
                    {
                        DataRow drowDefLang = dtblMetaData.NewRow();
                        //Update Entry from manifest file
                        dtblMetaData.Rows[0]["ContentModuleName"] = entContMod.ContentModuleEnglishName;
                        //New entry for Default Language
                        drowDefLang["ContentModuleId"] = strCourseId;
                        drowDefLang["LanguageId"] = Entity.Language.SYSTEM_DEFAULT_LANG_ID;//entClient.DefaultLanguageId;
                        drowDefLang["ContentModuleName"] = entContMod.ContentModuleEnglishName;
                        drowDefLang["ContentModuleDescription"] = ValidationManager.RemoveSpecialCharsForNameAndDescription(string.IsNullOrEmpty(pEntContModule.ContentModuleDescription) ? "" : pEntContModule.ContentModuleDescription.Trim());
                        drowDefLang["ContentModuleKeyWords"] = ValidationManager.RemoveSpecialChars(string.IsNullOrEmpty(pEntContModule.ContentModuleKeyWords) ? "" : pEntContModule.ContentModuleKeyWords.Trim());
                        dtblMetaData.Rows.Add(drowDefLang);
                    }
                }
                else
                {
                    entContMod.LanguageId = Entity.Language.SYSTEM_DEFAULT_LANG_ID;//entClient.DefaultLanguageId;
                    entContMod.ContentModuleName = ValidationManager.RemoveSpecialCharsForNameAndDescription(pEntContModule.ContentModuleName.Trim());
                    entContMod.ContentModuleDescription = ValidationManager.RemoveSpecialCharsForNameAndDescription(string.IsNullOrEmpty(pEntContModule.ContentModuleDescription) ? "" : pEntContModule.ContentModuleDescription.Trim());
                    entContMod.ContentModuleKeyWords = ValidationManager.RemoveSpecialChars(string.IsNullOrEmpty(pEntContModule.ContentModuleKeyWords) ? "" : pEntContModule.ContentModuleKeyWords.Trim());


                    entContMod = contentModuleAdaptor.EditContentModule(entContMod);
                }

                if (entContMod.IsAssessment)
                {
                    #region Added by santosh For Asset Question insertion

                    MasterContentsReader srvMasterContentsReader = new MasterContentsReader(pEntContModule.ClientId);
                    //try
                    //{
                    //    string pstrCoursePath = string.Empty;

                    //    string strCoursePath = hdnFTPCoursePath.Value;//txtFtpCoursePath.Text;
                    //    //string strCourseFolder = strCoursePath.Substring(strCoursePath.IndexOf(FileHandler.COURSE_FOLDER_PATH));
                    //    string strCourseFolder = strCoursePath.Substring(strCoursePath.IndexOf(FileHandler.CLIENTS_FOLDER_PATH));
                    //    pstrCoursePath = strCourseFolder;
                    //    if (strCoursePath.ToLower().IndexOf("/imsmanifest.xml") != -1)
                    //    {
                    //        strCourseFolder = strCourseFolder.Substring(0, strCourseFolder.Length - 16);
                    //        pstrCoursePath = strCourseFolder;
                    //    }
                    //    // //string strClientId = EncryptionManager.Decrypt(ConfigurationManager.AppSettings[YPLMS.Entity.Client.BASE_CLIENT_KEY]);
                    //    FileHandler fileHandler = new FileHandler(pEntContModule.ClientId);
                    //    string strMasterContents = fileHandler.RootSharedPath.Replace("\\\\", "\\");
                    //    pstrCoursePath = pstrCoursePath.Replace("/", @"\");
                    //    strMasterContents += pstrCoursePath;
                    //    string strMetaDataXml = strMasterContents;
                    //    //strMasterContents += @"\mastercontents.xml";
                    //    XMLLib xmlLib = new XMLLib();
                    //    string strmaifestfile = strMasterContents + "\\imsmanifest.xml";

                    //    XmlDocument xDocMF = new XmlDocument();
                    //    xmlLib.fOpenFreeXMLDoc(ref xDocMF, strmaifestfile);
                    //    XmlElement xnRoot = xDocMF.DocumentElement;   //.Attributes["CourseType"].Value
                    //    if (xnRoot != null && xnRoot.Attributes["CourseType"] != null && xnRoot.Attributes["CourseType"].Value.ToLower() == "murapidel")
                    //    {
                    //        if (xnRoot.Attributes["identifier"] != null)
                    //        {
                    //            string strMastercontentPath = xnRoot.Attributes["identifier"].Value.ToLower();

                    //            strMasterContents += @"\" + strMastercontentPath.Split('_')[0] + @"\mastercontents.xml";
                    //        }
                    //    }
                    //    else
                    //        strMasterContents += @"\mastercontents.xml";

                    //    xmlLib = new XMLLib();
                    //    if (File.Exists(strMasterContents))
                    //    {
                    //        CourseAssessmentQuestionManager MgrAssesQuestion = new CourseAssessmentQuestionManager();
                    //        List<CourseAssessmentQuestion> entLstAssesQuestion = new List<CourseAssessmentQuestion>();
                    //        List<CourseAssessmentQuestion> RetLstAssesQuestion = new List<CourseAssessmentQuestion>();

                    //        DataTable tblMasterContent = srvMasterContentsReader.ReadCourseMasterContents(strCourseFolder, strCourseId, strMasterContents);
                    //        if (tblMasterContent != null)
                    //        {
                    //            foreach (DataRow dr in tblMasterContent.Rows)
                    //            {

                    //                CourseAssessmentQuestion entAssesQuestion = new CourseAssessmentQuestion();
                    //                CourseAssessmentQuestion RetAssesQuestion = new CourseAssessmentQuestion();
                    //                entAssesQuestion.ClientId = _strSelectedClientId;
                    //                entAssesQuestion.QuestionId = Convert.ToString(dr["QuestionId"]);
                    //                entAssesQuestion.ContentModuleId = Convert.ToString(dr["ContentModuleId"]);
                    //                entAssesQuestion.QuestionText = Convert.ToString(dr["QuestionText"]);
                    //                entAssesQuestion.QuestionUniqueIndxNum = Convert.ToString(dr["QuestionUniqueIndxNum"]);
                    //                entLstAssesQuestion.Add(entAssesQuestion);
                    //            }
                    //            RetLstAssesQuestion = MgrAssesQuestion.Execute(entLstAssesQuestion, CourseAssessmentQuestion.ListMethod.BulkAdd);
                    //            if (RetLstAssesQuestion != null)
                    //            {

                    //            }
                    //        }
                    //        else // course is not with assessment or thier might be some issue with reading questions of course. 
                    //        {
                    //            // update the course for isAssessment flag.
                    //            entContMod.LastModifiedById = pEntContModule.LastModifiedById;
                    //            entContMod.IsAssessment = false;
                    //            entContMod = _contentModuleManager.Execute(entContMod, YPLMS.Entity.ContentModule.Method.Update);
                    //            IsAssementError = true;
                    //        }
                    //    }
                    //    else // course is not with assessment or thier might be some issue with reading questions of course. 
                    //    {
                    //        // update the course for isAssessment flag.
                    //        entContMod.LastModifiedById = _entCurrentUser.ID;
                    //        entContMod.IsAssessment = false;
                    //        entContMod = _contentModuleManager.Execute(entContMod, YPLMS.Entity.ContentModule.Method.Update);
                    //        IsAssementError = true;
                    //    }
                    //}
                    //catch (CustomException ex)
                    //{
                    //    // strErrorMsg = ex.GetCustomMessage(null);
                    //}
                    #endregion
                }

                #region to add Course Questions in case of Scorm 2004 course           

                // delete previous questions
                //CourseQuestions objCourseQuestions1 = new CourseQuestions();
                //objCourseQuestions1.CourseId = strCourseId;
                //objCourseQuestions1.ClientId = _strSelectedClientId;


                //Test CourseQuestionsDetails = new JavaScriptSerializer().Deserialize<Test>(hdnQuestionText.Value);
                //List<CourseQuestions> lstCourseQuestions = new List<CourseQuestions>();


                //if (CourseQuestionsDetails != null)
                //{

                //    if (CourseQuestionsDetails.Questions.Count > 0)
                //    {

                //        objCourseQuestions1 = (new CourseQuestionManager()).Execute(objCourseQuestions1, CourseQuestions.Method.Delete);

                //        // add new questions
                //        for (int i = 0; i < CourseQuestionsDetails.Questions.Count; i++)
                //        {
                //            CourseQuestions objCourseQuestions = new CourseQuestions();
                //            objCourseQuestions.Id = CourseQuestionsDetails.Questions[i].Id;
                //            objCourseQuestions.Text = CourseQuestionsDetails.Questions[i].Text;
                //            objCourseQuestions.ObjectiveId = CourseQuestionsDetails.Questions[i].ObjectiveId;
                //            if (CourseQuestionsDetails.Questions[i].Answers != null)
                //                objCourseQuestions.Answers = string.Join("##", CourseQuestionsDetails.Questions[i].Answers.ToArray());
                //            objCourseQuestions.CorrectAnswer = CourseQuestionsDetails.Questions[i].CorrectAnswer;
                //            objCourseQuestions.CourseId = strCourseId;
                //            objCourseQuestions.Type = CourseQuestionsDetails.Questions[i].Type;
                //            objCourseQuestions.ClientId = _strSelectedClientId;
                //            lstCourseQuestions.Add(objCourseQuestions);
                //        }
                //        lstCourseQuestions = (new CourseQuestionManager()).Execute(lstCourseQuestions, CourseQuestions.ListMethod.BulkAdd);
                //    }
                //}

                #endregion


                List<BaseEntity> entContModBase = ManifestReader.SetContentModuleLanguages(entContMod, dtblMetaData);
                List<ContentModule> entContModList = new List<ContentModule>();

                foreach (ContentModule entCont in entContModBase)
                {
                    entContModList.Add((ContentModule)entCont);
                }
                if (entContModList.Count > 0)
                {
                    try
                    {
                        if (hidFldEdit)
                        {
                            entContModList = contentModuleAdaptor.AddAllModules(entContModList);
                            //lblmsg.Text = "<font name='verdana' color='blue'>Course updated <b><i>successfully</i></b></font>";
                            //strStatusMsg = "Course updated successfully";
                            strStatusMsg = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_UPDATED);
                            
                        }
                        else
                        {
                            entContModList = contentModuleAdaptor.AddAllModules(entContModList);

                            strStatusMsg = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_ADDED);
                            
                        }
                       // LMSSession.RemoveSessionItem(ContentKeys.SESSION_COURSEID);
                       // strRedirectTo = _strParentPage;
                    }
                    catch
                    {
                        if (hidFldEdit)
                        {

                            strStatusMsg = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_PARTLY_UPDATED);
                        }
                        else
                        {

                            strStatusMsg = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_PARTLY_ADDED);
                        }
                    }
                }
                else
                {
                    if (hidFldEdit)
                    {

                        strStatusMsg = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_UPDATED);
                        
                    }
                    else
                    {

                        strStatusMsg = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_ADDED);
                        

                    }
                   // LMSSession.RemoveSessionItem(ContentKeys.SESSION_COURSEID);
                   // strRedirectTo = _strParentPage;
                }
            }
            else
            {
                if (hidFldEdit)
                {

                    strStatusMsg = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_UPDATION_FAILED);
                }
                else
                {

                    strStatusMsg = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_ADDITION_FAILED);
                }
            }
            if (IsAssementError == true)
            {
                strStatusMsg = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_ASSESSMENT_UPLOAD_PROBLEM);
            }
            return strStatusMsg;
        }

        public List<object> GetDirectoryData(string path)
        {
            var items = new List<object>();

            try
            {
                var directories = Directory.GetDirectories(path);

                foreach (var dir in directories)
                {
                    bool hasSubFolders = Directory.GetDirectories(dir).Length > 0;

                    items.Add(new
                    {
                        name = Path.GetFileName(dir),
                        path = dir.Replace('\\', '/'),
                        isFile = false,
                        hasChildren = hasSubFolders
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading directory: {ex.Message}");
            }

            return items;
        }

        public List<FileVm> GetFiles(string path)
        {
            var items = new List<FileVm>();
            try
            {
                var files = Directory.GetFiles(path);

                foreach (var file in files)
                {
                    var fileInfo = new FileInfo(file);
                    items.Add(new FileVm
                    {
                        Name = fileInfo.Name,
                        Path = file.Replace('\\', '/'),
                        IsFile = true,
                        Type = Path.GetExtension(fileInfo.Name),
                        Size = fileInfo.Length,
                        LastModified = fileInfo.LastWriteTime
                    });
                }
                //return new List<string>(Directory.GetFiles(path));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing files in {path}: {ex.Message}");
                return new List<FileVm>();
            }
            return items.OrderByDescending(x => x.LastModified).ToList();
        }

    }
}
