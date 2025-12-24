using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Tls;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Tracking;
using Task = System.Threading.Tasks.Task;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.Entity.Tracking;
using YPLMS2._0.API.Entity.ViewModel;
using YPLMS2._0.API.YPLMS.Services;
using static System.Net.WebRequestMethods;
using static YPLMS2._0.API.Entity.Asset;
using ContentModuleTrackingManager = YPLMS2._0.API.DataAccessManager.ContentModuleTrackingManager;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContentModuleController : ControllerBase
    {
        private readonly IContentModuleAdaptor<ContentModule> _contentModuleAdaptor ;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        ContentModuleTrackingManager _contentModuleTrackingManager = new ContentModuleTrackingManager();
        private readonly IClientDAM<Client> _clientdam;
        private readonly ILearnerDAM<Learner> _learnerdam;
        private readonly IContentModuleMappingAdaptor<ContentModuleMapping> _contentModuleMappingAdaptor;
        //private readonly IContentModuleTrackingManager<LessonTracking> TrackingManager;
        //ContentModuleTracker TrackingManager = new ContentModuleTracker();

        #region IContentModuleTrackingManager

        //public ICoursePlayerAssignmentManager CoursePlayerAssignmentManager { get; set; }
        public IContentModuleTrackingManager<LessonTracking> TrackingManager
        {
            get;
            set;
        }
        #endregion
        public ContentModuleController(IContentModuleAdaptor<ContentModule> contentModuleAdaptor, IMapper mapper, IConfiguration config, IClientDAM<Client> clientdam, ILearnerDAM<Learner> learnerdam, IContentModuleMappingAdaptor<ContentModuleMapping> contentModuleMappingAdaptor)
        {
            _contentModuleAdaptor = contentModuleAdaptor;
            _mapper = mapper;
            _config = config;
            _clientdam = clientdam;
            _learnerdam = learnerdam;
            _contentModuleMappingAdaptor = contentModuleMappingAdaptor;
            //TrackingManager = ContentModuleTrackingManager;
        }

        [HttpPost]
        [Route("getcontentmodulebyid")]
        [Authorize]
        public async Task<IActionResult> GetContentModuleByID(ContentModuleVM pEntContModule)
        {
            try
            {
                ContentModule contentModule = new ContentModule();

                contentModule = _contentModuleAdaptor.GetContentModuleByID(_mapper.Map< ContentModule>(pEntContModule));

                #region //get content server Url
                API.Entity.Client entClient = new API.Entity.Client();
                ClientDAM clientDAM = new ClientDAM();
                entClient.ID = pEntContModule.ClientId;
                entClient = clientDAM.GetClientByID(entClient);
                contentModule.ContentServerURL = entClient.ContentServerURL;
                #endregion

                if (contentModule.ID != null)
                {

                    return Ok(new { ContentModule = contentModule, Code = 200 });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found"});
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        [Route("getContentModuleByidcourseplayer")]
        [Authorize]
        public async Task<IActionResult> GetContentModuleByID_CoursePlayer(ContentModule pEntContModule)
        {
            
            ContentModule contentModule = new ContentModule();

            contentModule = _contentModuleAdaptor.GetContentModuleByID_CoursePlayer(pEntContModule);
            if (contentModule != null)
            {                
                return Ok(new { ContentModule = contentModule });
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("getcontentmoduleurl")]
        [Authorize]
        public async Task<IActionResult> GetContentModuleURL(ContentModule pEntContModule)
        {
            
            ContentModule contentModule = new ContentModule();

            contentModule = _contentModuleAdaptor.GetContentModuleURL(pEntContModule);
            if (contentModule != null)
            {              
                return Ok(new { ContentModule = contentModule });
            }
            else
            {
                return BadRequest();
            }            

        }

        [HttpPost]
        [Route("getContentmodulebyidlearner")]
        [Authorize]
        public async Task<IActionResult> GetContentModuleByID_Learner(ContentModule pEntContModule)
        {
            
            ContentModule contentModule = new ContentModule();

            contentModule = _contentModuleAdaptor.GetContentModuleByID_Learner(pEntContModule);
            if (contentModule != null)
            {               
                return Ok(new { ContentModule = contentModule });
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("searchcontentmoduleurl")]
        [Authorize]
        public async Task<IActionResult> SearchContentModuleURL(ContentModule pEntContModule)
        {
            
            ContentModule contentModule = new ContentModule();

            contentModule = _contentModuleAdaptor.SearchContentModuleURL(pEntContModule);
            if (contentModule != null)
            {
               
                return Ok(new { ContentModule = contentModule });
            }
            else
            {
                return BadRequest();
            }
        }

        

        [HttpPost]
        [Route("findcontentmodulebyname")]
        [Authorize]
        public async Task<IActionResult> FindContentModuleByName(ContentModuleVM pEntContModule)
        {
            
            ContentModule contentModule = new ContentModule();

            contentModule = _contentModuleAdaptor.FindContentModuleByName(_mapper.Map< ContentModule>(pEntContModule));
            if (contentModule != null)
            {               
                return Ok(new { ContentModule = contentModule });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("addcontentmodule")]
        [Authorize]
        public async Task<IActionResult> AddContentModule(ContentModuleVM pEntContModule)
        {
            try
            {
                
                string _strmessage = string.Empty;
                bool Isavailable;
                string hdnFTPCoursePath = string.Empty;
                ContentModule ContentModule = new ContentModule();
                FileHandler FtpUpload = new FileHandler(pEntContModule.ClientId);
                hdnFTPCoursePath = FtpUpload.GetCourseFTPPath(pEntContModule.ContentModuleURL);

                string strCourseId = string.Empty;//= LMSSession.GetValue(ContentKeys.SESSION_COURSEID).ToString();
                if ( !string.IsNullOrEmpty(pEntContModule.ContentModuleURL)) //!string.IsNullOrEmpty(hidFldEdit.Value) &&
                {
                    string strCourseName = pEntContModule.ContentModuleURL.Substring(pEntContModule.ContentModuleURL.IndexOf(FileHandler.COURSE_FOLDER_PATH));
                    string[] aCoursePath = strCourseName.Split(Convert.ToChar("/"));
                    if (aCoursePath.Length > 0)
                        strCourseId = aCoursePath[1];
                }
               
                if ( !string.IsNullOrEmpty(hdnFTPCoursePath) && !string.IsNullOrEmpty(pEntContModule.ContentModuleURL)) //!string.IsNullOrEmpty(hidFldEdit.Value) &&
                {
                    string strCourseName = pEntContModule.ContentModuleURL.Substring(pEntContModule.ContentModuleURL.IndexOf(FileHandler.COURSE_FOLDER_PATH));
                    string[] aCoursePath = strCourseName.Split(Convert.ToChar("/"));
                    if (aCoursePath.Length > 0)
                        strCourseId = aCoursePath[1];
                }

                if (pEntContModule.ContentModuleEnglishName != null)
                {
                    pEntContModule.ContentModuleEnglishName = ValidationManager.RemoveSpecialCharsForNameAndDescription(pEntContModule.ContentModuleEnglishName);
                    ContentModule = _contentModuleAdaptor.FindContentModuleByName(_mapper.Map<ContentModule>(pEntContModule));
                }
                if (string.IsNullOrEmpty(ContentModule.ID))
                {
                    Isavailable = true;
                }
                else
                {
                    if (Convert.ToString(strCourseId) == ContentModule.ID) //hidFldEdit.Value == "true" &&
                    {
                        Isavailable = true;
                    }
                    else
                    {
                        Isavailable = false;
                    }
                }
                if (Isavailable)
                {
                    ManifestReader srvManifestReader = new ManifestReader(pEntContModule.ClientId);
                    string sManifestSplitPath = string.Empty;
                    string strFTP = hdnFTPCoursePath;//txtFtpCoursePath.Text;
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
                        sManifestSplitPath = strFTP;
                    }
                    else
                    {
                        sManifestSplitPath = FileHandler.CLIENTS_FOLDER_PATH + "/" + pEntContModule.ClientId + "/" + FileHandler.COURSE_FOLDER_PATH + "/" + strCourseId;
                    }
                    List<ContentModule> lstContent = srvManifestReader.ManifestSplit(sManifestSplitPath);



                   // string strMasterContentErrorMsg = ReadManifestAndMetaData(strCourseId, true);


                    string strErrorMsg = string.Empty;
                    string hdnMasteryScore = string.Empty;


                    DataTable tblMetaData = null;
                    //ManifestReader srvManifestReader = new ManifestReader(pEntContModule.ClientId);
                    try
                    {

                        string strCoursePath = hdnFTPCoursePath;//txtFtpCoursePath.Text;
                                                                      // string strCourseFolder = strCoursePath.Substring(strCoursePath.IndexOf(FileHandler.COURSE_FOLDER_PATH));
                        if (string.IsNullOrEmpty(hdnFTPCoursePath))
                        {
                            strCoursePath = pEntContModule.ContentModuleURL;
                        }
                        string strCourseFolder = strCoursePath.Substring(strCoursePath.IndexOf(FileHandler.CLIENTS_FOLDER_PATH));

                        if (strCourseFolder.ToLower().IndexOf("/imsmanifest.xml") != -1)
                        {
                            strCourseFolder = strCourseFolder.Substring(0, strCourseFolder.Length - 16);
                        }
                        if (pEntContModule.ContentModuleTypeId.ToLower() == ActivityContentType.Scorm2004.ToString().ToLower())
                        {
                            tblMetaData = srvManifestReader.ReadCourseManifestForScorm2004(strCourseFolder, strCourseId);
                            //GetCourseQuestionsAndAddRequiredFunction();
                        }
                        else
                        {
                            tblMetaData = srvManifestReader.ReadCourseManifest(strCourseFolder, strCourseId);
                        }

                        if (string.IsNullOrEmpty(API.YPLMS.Services.Common.CourseMasteryScore))
                        {
                            API.YPLMS.Services.Common.CourseMasteryScore = _contentModuleTrackingManager.GetMasteryScoreFromMasterContentXML(pEntContModule.ClientId, hdnFTPCoursePath, strCourseId);
                        }
                        hdnMasteryScore = API.YPLMS.Services.Common.CourseMasteryScore;
                    }
                    catch (CustomException ex)
                    {
                       // strErrorMsg = ex.GetCustomMessage(null);
                    }
                    //ViewState.Add("tblMetaData", tblMetaData);
                    if (tblMetaData != null && tblMetaData.Rows.Count > 0) //&& !bSaveClick
                    {
                        DataRow[] drDefClientLang = tblMetaData.Select("LanguageId = '" + Language.SYSTEM_DEFAULT_LANG_ID /*entClient.DefaultLanguageId*/ + "'");
                        if (drDefClientLang.Length == 1)
                        {
                            pEntContModule.ContentModuleEnglishName  = ValidationManager.RemoveSpecialCharsForNameAndDescription(Convert.ToString(drDefClientLang[0]["ContentModuleName"]));
                            pEntContModule.ContentModuleDescription  = ValidationManager.RemoveSpecialCharsForNameAndDescription(drDefClientLang[0]["ContentModuleDescription"].ToString());
                            pEntContModule.ContentModuleKeyWords  = ValidationManager.RemoveSpecialChars(drDefClientLang[0]["ContentModuleKeyWords"].ToString());
                        }
                    }
                    //return strErrorMsg;



                    if (lstContent.Count >= 2)
                    {
                        int count = 0;
                        try
                        {
                            foreach (ContentModule entContMod in lstContent)
                            {
                                string newCourseId = string.Empty;
                                string strContModIdPreFix = API.DataAccessManager.Schema.Common.VAL_CONTENT_MODULE_ID_PREFIX;
                                int strContModIdLen = API.DataAccessManager.Schema.Common.VAL_CONTENT_MODULE_ID_LENGTH;
                                newCourseId = API.YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(strContModIdPreFix, strContModIdLen);
                                if (count == 0)
                                {
                                    newCourseId = strCourseId;
                                    count++;
                                }
                                if (!string.IsNullOrEmpty(entContMod.ContentModuleEnglishName))
                                    entContMod.ContentModuleEnglishName = ValidationManager.RemoveSpecialCharsForNameAndDescription(pEntContModule.ContentModuleEnglishName.Trim()) + "(" + entContMod.ContentModuleEnglishName + ")";
                                else
                                    entContMod.ContentModuleEnglishName = ValidationManager.RemoveSpecialCharsForNameAndDescription(pEntContModule.ContentModuleEnglishName.Trim());

                                _strmessage = _contentModuleTrackingManager.SaveContentMaster(newCourseId, strCourseId, entContMod, _mapper.Map<ContentModule>(pEntContModule), tblMetaData,false, hdnFTPCoursePath);
                                
                            }
                        }
                        catch(Exception ex)
                        {
                            return BadRequest(ex.Message);
                        }
                    }
                    else //if (lstContent.Count == 1)
                    {
                        ContentModule entContMod = new ContentModule();
                        entContMod.ContentModuleEnglishName = ValidationManager.RemoveSpecialCharsForNameAndDescription(pEntContModule.ContentModuleEnglishName.Trim());
                        if (!string.IsNullOrEmpty(pEntContModule.SendEmailTo))
                        {
                            if (!ValidationManager.ValidateString(pEntContModule.SendEmailTo.Trim(), ValidationManager.DataType.EmailID))
                            {
                                
                                return BadRequest("Provide valid email id.");

                            }
                            string strRegex = @"</script>";

                            Regex regex = new Regex(strRegex);

                            bool is_valid = false;

                            if (regex.IsMatch(pEntContModule.SendEmailTo))
                            {
                                is_valid = true;
                            }

                            if (is_valid == true)
                            {
                                return BadRequest("Provide valid email id.");
                            }
                        }
                        _strmessage = _contentModuleTrackingManager.SaveContentMaster(strCourseId, strCourseId, entContMod, _mapper.Map<ContentModule>(pEntContModule), tblMetaData, false, hdnFTPCoursePath);

                    }
                }

                //contentModule = _contentModuleAdaptor.AddContentModule(_mapper.Map<ContentModule>( pEntContModule));
                if (!string.IsNullOrEmpty(_strmessage))
                {
                    return Ok(new { Code = 200, Msg = _strmessage, ContModule = pEntContModule });
                }
                else
                {
                    return NotFound(new { Code = 404 });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut]
        [Route("editcontentmodule")]
        [Authorize]
        public async Task<IActionResult> EditContentModule(ContentModuleVM pEntContModule)
        {
            try
            {
                
                string _strmessage = string.Empty;
                bool Isavailable;
                string hdnFTPCoursePath = string.Empty;
                ContentModule ContentModule = new ContentModule();
                FileHandler FtpUpload = new FileHandler(pEntContModule.ClientId);
                hdnFTPCoursePath = FtpUpload.GetCourseFTPPath(pEntContModule.ContentModuleURL);

                string strCourseId = string.Empty;//= LMSSession.GetValue(ContentKeys.SESSION_COURSEID).ToString();
                if (!string.IsNullOrEmpty(pEntContModule.ContentModuleURL)) //!string.IsNullOrEmpty(hidFldEdit.Value) &&
                {
                    string strCourseName = pEntContModule.ContentModuleURL.Substring(pEntContModule.ContentModuleURL.IndexOf(FileHandler.COURSE_FOLDER_PATH));
                    string[] aCoursePath = strCourseName.Split(Convert.ToChar("/"));
                    if (aCoursePath.Length > 0)
                        strCourseId = aCoursePath[1];
                }

                if (!string.IsNullOrEmpty(hdnFTPCoursePath) && !string.IsNullOrEmpty(pEntContModule.ContentModuleURL)) //!string.IsNullOrEmpty(hidFldEdit.Value) &&
                {
                    string strCourseName = pEntContModule.ContentModuleURL.Substring(pEntContModule.ContentModuleURL.IndexOf(FileHandler.COURSE_FOLDER_PATH));
                    string[] aCoursePath = strCourseName.Split(Convert.ToChar("/"));
                    if (aCoursePath.Length > 0)
                        strCourseId = aCoursePath[1];
                }

                if (pEntContModule.ContentModuleEnglishName != null)
                {
                    pEntContModule.ContentModuleEnglishName = ValidationManager.RemoveSpecialCharsForNameAndDescription(pEntContModule.ContentModuleEnglishName);
                    ContentModule = _contentModuleAdaptor.FindContentModuleByName(_mapper.Map<ContentModule>(pEntContModule));
                }
                if (string.IsNullOrEmpty(ContentModule.ID))
                {
                    Isavailable = true;
                }
                else
                {
                    if (Convert.ToString(strCourseId) == ContentModule.ID) //hidFldEdit.Value == "true" &&
                    {
                        Isavailable = true;
                    }
                    else
                    {
                        Isavailable = false;
                    }
                }
                if (Isavailable)
                {
                    ManifestReader srvManifestReader = new ManifestReader(pEntContModule.ClientId);
                    string sManifestSplitPath = string.Empty;
                    string strFTP = hdnFTPCoursePath;//txtFtpCoursePath.Text;
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
                        sManifestSplitPath = strFTP;
                    }
                    else
                    {
                        sManifestSplitPath = FileHandler.CLIENTS_FOLDER_PATH + "/" + pEntContModule.ClientId + "/" + FileHandler.COURSE_FOLDER_PATH + "/" + strCourseId;
                    }
                    List<ContentModule> lstContent = srvManifestReader.ManifestSplit(sManifestSplitPath);



                    // string strMasterContentErrorMsg = ReadManifestAndMetaData(strCourseId, true);


                    string strErrorMsg = string.Empty;
                    string hdnMasteryScore = string.Empty;


                    DataTable tblMetaData = null;
                    //ManifestReader srvManifestReader = new ManifestReader(pEntContModule.ClientId);
                    try
                    {

                        string strCoursePath = hdnFTPCoursePath;//txtFtpCoursePath.Text;
                                                                // string strCourseFolder = strCoursePath.Substring(strCoursePath.IndexOf(FileHandler.COURSE_FOLDER_PATH));
                        if (string.IsNullOrEmpty(hdnFTPCoursePath))
                        {
                            strCoursePath = pEntContModule.ContentModuleURL;
                        }
                        string strCourseFolder = strCoursePath.Substring(strCoursePath.IndexOf(FileHandler.CLIENTS_FOLDER_PATH));

                        if (strCourseFolder.ToLower().IndexOf("/imsmanifest.xml") != -1)
                        {
                            strCourseFolder = strCourseFolder.Substring(0, strCourseFolder.Length - 16);
                        }
                        if (pEntContModule.ContentModuleTypeId.ToLower() == ActivityContentType.Scorm2004.ToString().ToLower())
                        {
                            tblMetaData = srvManifestReader.ReadCourseManifestForScorm2004(strCourseFolder, strCourseId);
                            //GetCourseQuestionsAndAddRequiredFunction();
                        }
                        else
                        {
                            tblMetaData = srvManifestReader.ReadCourseManifest(strCourseFolder, strCourseId);
                        }

                        if (string.IsNullOrEmpty(API.YPLMS.Services.Common.CourseMasteryScore))
                        {
                            API.YPLMS.Services.Common.CourseMasteryScore = _contentModuleTrackingManager.GetMasteryScoreFromMasterContentXML(pEntContModule.ClientId, hdnFTPCoursePath, strCourseId);
                        }
                        hdnMasteryScore = API.YPLMS.Services.Common.CourseMasteryScore;
                    }
                    catch (CustomException ex)
                    {
                        // strErrorMsg = ex.GetCustomMessage(null);
                    }
                    //ViewState.Add("tblMetaData", tblMetaData);
                    if (tblMetaData != null && tblMetaData.Rows.Count > 0) //&& !bSaveClick
                    {
                        DataRow[] drDefClientLang = tblMetaData.Select("LanguageId = '" + Language.SYSTEM_DEFAULT_LANG_ID /*entClient.DefaultLanguageId*/ + "'");
                        if (drDefClientLang.Length == 1)
                        {
                            pEntContModule.ContentModuleEnglishName = ValidationManager.RemoveSpecialCharsForNameAndDescription(Convert.ToString(drDefClientLang[0]["ContentModuleName"]));
                            pEntContModule.ContentModuleDescription = ValidationManager.RemoveSpecialCharsForNameAndDescription(drDefClientLang[0]["ContentModuleDescription"].ToString());
                            pEntContModule.ContentModuleKeyWords = ValidationManager.RemoveSpecialChars(drDefClientLang[0]["ContentModuleKeyWords"].ToString());
                        }
                    }
                    //return strErrorMsg;



                    if (lstContent.Count >= 2)
                    {
                        int count = 0;
                        try
                        {
                            foreach (ContentModule entContMod in lstContent)
                            {
                                string newCourseId = string.Empty;
                                string strContModIdPreFix = API.DataAccessManager.Schema.Common.VAL_CONTENT_MODULE_ID_PREFIX;
                                int strContModIdLen = API.DataAccessManager.Schema.Common.VAL_CONTENT_MODULE_ID_LENGTH;
                                newCourseId = API.YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(strContModIdPreFix, strContModIdLen);
                                if (count == 0)
                                {
                                    newCourseId = strCourseId;
                                    count++;
                                }
                                if (!string.IsNullOrEmpty(entContMod.ContentModuleEnglishName))
                                    entContMod.ContentModuleEnglishName = ValidationManager.RemoveSpecialCharsForNameAndDescription(pEntContModule.ContentModuleEnglishName.Trim()) + "(" + entContMod.ContentModuleEnglishName + ")";
                                else
                                    entContMod.ContentModuleEnglishName = ValidationManager.RemoveSpecialCharsForNameAndDescription(pEntContModule.ContentModuleEnglishName.Trim());

                                _strmessage = _contentModuleTrackingManager.SaveContentMaster(newCourseId, strCourseId, entContMod, _mapper.Map<ContentModule>(pEntContModule), tblMetaData, true, hdnFTPCoursePath);

                            }
                        }
                        catch (Exception ex)
                        {
                            return BadRequest(ex.Message);
                        }
                    }
                    else //if (lstContent.Count == 1)
                    {
                        ContentModule entContMod = new ContentModule();
                        entContMod.ContentModuleEnglishName = ValidationManager.RemoveSpecialCharsForNameAndDescription(pEntContModule.ContentModuleEnglishName.Trim());
                        if (!string.IsNullOrEmpty(pEntContModule.SendEmailTo))
                        {
                            if (!ValidationManager.ValidateString(pEntContModule.SendEmailTo.Trim(), ValidationManager.DataType.EmailID))
                            {

                                return BadRequest("Provide valid email id.");

                            }
                            string strRegex = @"</script>";

                            Regex regex = new Regex(strRegex);

                            bool is_valid = false;

                            if (regex.IsMatch(pEntContModule.SendEmailTo))
                            {
                                is_valid = true;
                            }

                            if (is_valid == true)
                            {
                                return BadRequest("Provide valid email id.");
                            }
                        }
                        _strmessage = _contentModuleTrackingManager.SaveContentMaster(strCourseId, strCourseId, entContMod, _mapper.Map<ContentModule>(pEntContModule), tblMetaData, true, hdnFTPCoursePath);

                    }
                }

                //contentModule = _contentModuleAdaptor.AddContentModule(_mapper.Map<ContentModule>( pEntContModule));
                if (!string.IsNullOrEmpty(_strmessage))
                {
                    return Ok(new { Code = 200, Msg = _strmessage, ContModule = pEntContModule });
                }
                else
                {
                    return NotFound(new { Code = 404 });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("deletecontentmodules")]
        [Authorize]
        public async Task<IActionResult> BulkDelete(List<ContentModuleVM> pEntListContentModule)
        {
            List<ContentModule> entListContentModule = new List<ContentModule>();

            List<ContentModuleMapping> eCourseListContAdm = new List<ContentModuleMapping>();
            ContentModuleMapping eCourseContAdm = new ContentModuleMapping();
            
            List<ContentModule> eCourseList = new List<ContentModule>();
            ContentModule eCourse = new ContentModule();
            string strSelectedCourse = string.Empty;
            bool bSelected = false;
            bool blnIsAllocatedSelected = false;

            string strRecordID = string.Empty;

            try
            {
                Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = pEntListContentModule[0].LastModifiedById, ClientId = pEntListContentModule[0].ClientId });


                foreach (ContentModuleVM gvRow in pEntListContentModule)
                {
                   // HtmlInputCheckBox Ctrl = gvRow.FindControl("chkClient") as HtmlInputCheckBox;

                    //if (Ctrl.Checked)
                    {
                        strSelectedCourse = gvRow.ID; //YPLMS.Services.EncryptionManager.Decrypt(gvRow.Cells[intIdIndex].Text);
                        if (!string.IsNullOrEmpty(strSelectedCourse))
                        {
                            if (_entCurrentUser.IsContentAdmin())
                            {

                                bSelected = true;
                                eCourseContAdm = new ContentModuleMapping();
                                eCourseContAdm.ID = strSelectedCourse;
                                eCourseContAdm.ClientId = null;
                                eCourseListContAdm.Add(eCourseContAdm);
                                strRecordID += strSelectedCourse + ", ";
                            }
                            else
                            {
                                bSelected = true;
                                eCourse = new ContentModule();
                                eCourse.ID = strSelectedCourse;
                                eCourse.ClientId = gvRow.ClientId;
                                eCourse = _contentModuleAdaptor.GetContentModuleByID(eCourse);
                                eCourse.ClientId = gvRow.ClientId;
                                eCourseList.Add(eCourse);
                                strRecordID += strSelectedCourse + ", ";
                            }
                            if (eCourse.ID != null && eCourse.IsMasterContent == true)
                            {
                                return BadRequest( new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSES_RIGHTS) } );
                                
                            }
                        }
                    }
                }
                if (blnIsAllocatedSelected)
                {
                    return BadRequest(new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModuleAllocationLicensing.ALLOCATED_COURSES_CANT_DELETED) } );

                }
                else
                if (bSelected)
                {
                    if (eCourseList.Count > 0)
                    {
                        eCourseList = _contentModuleAdaptor.BulkDelete(eCourseList);
                        if (eCourseList != null)
                        {
                            if (eCourseList.Count > 0)
                            {
                                return BadRequest(new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModuleAllocationLicensing.ALLOCATED_COURSES_CANT_DELETED) });
                            }
                            else
                            {

                                return Ok(new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_DELETED) });
                            }
                        }
                        else
                        {
                            return BadRequest(new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_DELETION_FAILED) });
                        }
                    }
                    else if (eCourseListContAdm.Count > 0)
                    {
                        eCourseListContAdm = _contentModuleMappingAdaptor.BulkDelete(eCourseListContAdm);
                        if (eCourseListContAdm != null)
                        {
                            if (eCourseListContAdm.Count > 0)
                            {
                                return BadRequest(new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModuleAllocationLicensing.ALLOCATED_COURSES_CANT_DELETED) });
                            }
                            else
                            {

                                return Ok(new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_DELETED) });
                            }
                        }
                        else
                        {
                            return BadRequest(new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_DELETION_FAILED) });
                        }

                    }
                    else
                    {
                        if (blnIsAllocatedSelected)
                        {
                            return BadRequest(new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModuleAllocationLicensing.ALLOCATED_COURSES_CANT_DELETED) });
                        }
                    }
                }
                else
                {
                    return BadRequest(new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModuleAllocationLicensing.SELECT_COURSE) });
                }

                return BadRequest();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("courseuploadvalidate")]
        [Authorize]
        public async Task<IActionResult> CourseUploadValidate(ContentModuleVM pEntContModule)
        {

            try
            {
                ManifestReader srvManifestReader = new ManifestReader(pEntContModule.ClientId);
                //fileExplorer.VisibilityWindow(false);
                string sCourseUrl = string.Empty;
                string strCourseName = string.Empty;
                if (!string.IsNullOrEmpty(pEntContModule.ContentModuleURL))
                {
                    //pEntContModule.ContentModuleURL = pEntContModule.ContentModuleURL.Contains("\\") ? pEntContModule.ContentModuleURL.Replace("\\", "/"): pEntContModule.ContentModuleURL;
                    strCourseName = pEntContModule.ContentModuleURL.Substring(pEntContModule.ContentModuleURL.IndexOf(FileHandler.COURSE_FOLDER_PATH));
                }
                //else
                //{
                //    //string strCourseName = txtFtpCoursePath.Text.Substring(txtFtpCoursePath.Text.IndexOf(FileHandler.COURSE_FOLDER_PATH));
                //    strCourseName = hdnFTPCoursePath.Value.Substring(hdnFTPCoursePath.Value.IndexOf(FileHandler.CLIENTS_FOLDER_PATH));
                //}
                //if (!string.IsNullOrEmpty(hdnFTPCoursePath.Value) && !string.IsNullOrEmpty(txtFtpCoursePath.Text))
                //{
                //    strCourseName = txtFtpCoursePath.Text.Substring(txtFtpCoursePath.Text.IndexOf(FileHandler.COURSE_FOLDER_PATH));
                //}
                string[] aCoursePath = strCourseName.Split(Convert.ToChar("/"));
                if (aCoursePath.Length > 0)
                {
                    sCourseUrl = aCoursePath[0] + "/" + aCoursePath[1];
                    FileHandler FtpUpload = new FileHandler(pEntContModule.ClientId);

                    {

                        //string strCourseId = aCoursePath[1]; commented by sarita 
                        //string strCourseId = aCoursePath[3];
                        string strCourseId = string.Empty;
                        if (!string.IsNullOrEmpty(pEntContModule.ContentModuleURL))
                        {
                            strCourseId = aCoursePath[1];
                        }
                        //else if (string.IsNullOrEmpty(hdnFTPCoursePath.Value))
                        //    strCourseId = aCoursePath[1];
                        else
                            strCourseId = aCoursePath[3];

                        if (pEntContModule.ContentModuleTypeId.ToLower() != ActivityContentType.NonCompliant.ToString().ToLower())//"noncompliant")
                        {
                            string strErrorMsg = string.Empty;
                            AICCToManifest entAiccToManifest = new AICCToManifest();
                            if (pEntContModule.ContentModuleTypeId.ToLower() == ActivityContentType.AICC.ToString().ToLower())
                            {
                                string sFtpRootSharedPath = FtpUpload.RootSharedPath;
                                if (sFtpRootSharedPath.Contains("\\\\\\\\"))
                                {
                                    sFtpRootSharedPath = sFtpRootSharedPath.Replace("\\\\", "\\");
                                }
                                //string sCourseFolderPath = sFtpRootSharedPath + FileHandler.COURSE_FOLDER_PATH + "\\" + strCourseId + "\\";

                                //edited by sarita string sCourseFolderPath = sFtpRootSharedPath + FileHandler.CLIENTS_FOLDER_PATH + "\\" + _strSelectedClientId + "\\" + FileHandler.COURSE_FOLDER_PATH + "\\" + strCourseId + "\\";
                                string sCourseFolderPath = sFtpRootSharedPath + FileHandler.CLIENTS_FOLDER_PATH + "\\" + pEntContModule.ClientId + "\\" + FileHandler.COURSE_FOLDER_PATH + "\\" + strCourseId + "\\";

                                if (entAiccToManifest.IsAICCFilesExists(sCourseFolderPath))
                                {
                                    string path = FileHandler.CLIENTS_FOLDER_PATH + "\\" + pEntContModule.ClientId + "\\" + FileHandler.COURSE_FOLDER_PATH + "\\" + strCourseId + "\\";
                                    //if (entAiccToManifest.ConvertAICCToManifest(path, Server.MapPath("AICCLibrary/MasterFiles/ManifestTemplate.xml")))// if (entAiccToManifest.ConvertAICCToManifest(sCourseUrl, Server.MapPath("AICCLibrary/MasterFiles/ManifestTemplate.xml")))                                 
                                    //{

                                    //    {
                                    //        btnSave.Enabled = true;
                                    //        btnSave.CssClass = "btn input-btn btn-danger waves-effect";
                                    //        ddlStandardType.Enabled = false;
                                    //        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "script", "document.getElementById('ctl00_ContentPlaceHolder1_ddlStandardType').disabled = true;", true);

                                    //        lblPointUrlMsg.Text = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED);
                                    //    }

                                    //}
                                }
                                else
                                {
                                    
                                    return BadRequest( new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.INVALID_AICC_COURSE) });
                                }

                            }
                            else if (pEntContModule.ContentModuleTypeId.ToLower() == ActivityContentType.Scorm12.ToString().ToLower())
                            {
                                strErrorMsg = srvManifestReader.ReadManifestAndMetaData(strCourseId, _mapper.Map<ContentModule>(pEntContModule), pEntContModule.ContentModuleURL);
                                if (strErrorMsg == string.Empty)
                                {
                                   

                                    return Ok( new { code = 200, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED) });
                                }
                                else
                                {

                                    return BadRequest(new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.INVALID_MANIFEST_FILE) });
                                }
                            }
                            else if (pEntContModule.ContentModuleTypeId.ToLower() == ActivityContentType.Scorm2004.ToString().ToLower())
                            {
                                strErrorMsg = srvManifestReader.ReadManifestAndMetaData(strCourseId, _mapper.Map<ContentModule>(pEntContModule), pEntContModule.ContentModuleURL);
                                if (strErrorMsg == string.Empty)
                                {


                                    return Ok(new { code = 200, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED) });
                                }
                                else
                                {

                                    return BadRequest(new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.INVALID_MANIFEST_FILE) });
                                }
                            }
                            else
                            {

                                string strCoursePath = pEntContModule.ContentModuleURL;//txtFtpCoursePath.Text;
                                //string strCourseFolder = strCoursePath.Substring(strCoursePath.IndexOf(FileHandler.COURSE_FOLDER_PATH));
                                string strCourseFolder = strCoursePath.Substring(strCoursePath.IndexOf(FileHandler.CLIENTS_FOLDER_PATH));
                                string[] arrName = strCourseFolder.Split(Convert.ToChar("/"));
                                if (arrName[arrName.Length - 1].Contains("."))
                                {
                                   // lblCourseNameStatus.Visible = true;
                                }
                                else
                                {
                                   // lblCourseNameStatus.Visible = false;
                                }
                            }
                        }
                        else
                        {
                            return Ok(new { code = 200, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED) });
                            
                        }
                    }
                }
                else
                {
                    
                    return BadRequest(new { code = 400, msg = "Please select valid file." });

                }
                return BadRequest(new { code = 400, msg = "Please select valid file." });
            }
            catch (CustomException ex)
            {
               return BadRequest(ex.Message);
            }
           
        }

        /// <summary>
        /// Lazy-Loading Folder Structure Tree
        /// </summary>
        /// <param name="ClientId"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getcoursestructure")]
        [Authorize]
        public async Task<IActionResult> GetCourseStructure(string ClientId, string? relativePath = null)
        {
            try
            {
                if (string.IsNullOrEmpty(ClientId))
                {
                    return BadRequest(new { Code = 400, Msg = "Please Provide Client Id" });
                }

                FileHandler FtpUpload = new FileHandler(ClientId);
                string[] RootSharedPath = FtpUpload.RootSharedPath.Split(new string[] { "\\\\" }, StringSplitOptions.RemoveEmptyEntries);

                string basePath = "\\\\" + RootSharedPath[0] + "\\" + RootSharedPath[1] + "\\" + FileHandler.CLIENTS_FOLDER_PATH + "\\" + ClientId + "\\" + FileHandler.COURSE_FOLDER_PATH;

                string finalPath = string.IsNullOrEmpty(relativePath)
                    ? basePath
                    : Path.Combine(basePath, relativePath.Replace("/", "\\"));

                if (!Directory.Exists(finalPath))
                {
                    return NotFound(new { Code = 404, Msg = "Directory not found." });
                }

                var folderData = await Task.Run(() => _contentModuleTrackingManager.GetDirectoryData(finalPath));
                return Ok(new { Courses = folderData, Code = 200 });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Code = 500, Msg = ex.Message });
            }
        }

        [HttpPost]
        [Route("getcoursefilestructure")]
        [Authorize]
        public async Task<IActionResult> GetCourseFileStructure(CourseVM pENTCourseVM)
        {
            try
            {

                if (string.IsNullOrEmpty(pENTCourseVM.ClientId))
                {
                    return BadRequest(new { Code = 400, Msg = "Please Provide Client Id" });
                }
                if (string.IsNullOrEmpty(pENTCourseVM.FileURL))
                {
                    return BadRequest(new { Code = 400, Msg = "Please Provide path" });
                }
                FileHandler FtpUpload = new FileHandler(pENTCourseVM.ClientId);
                string[] RootSharedPath = FtpUpload.RootSharedPath.Split(new string[] { "\\\\" }, StringSplitOptions.RemoveEmptyEntries);
                pENTCourseVM.FileURL.Replace("/", @"\\");
                //var Path = "\\\\" + RootSharedPath[0] + "\\" + RootSharedPath[1] + "\\" + FileHandler.CLIENTS_FOLDER_PATH + "\\" + ClientId + "\\" + FileHandler.COURSE_FOLDER_PATH;// + "\\" + strCourseId;
                var Path = "\\\\" + RootSharedPath[0] + "\\" + RootSharedPath[1] + "\\" + FileHandler.CLIENTS_FOLDER_PATH + "\\" + pENTCourseVM.ClientId + "\\" + pENTCourseVM.FileURL; // + "\\" + FileHandler.COURSE_FOLDER_PATH;// + "\\" + strCourseId;

                if (!Directory.Exists(Path))
                {
                    return NotFound(new { Code = 404, Msg = "Directory not found." });
                }

                var folderData = _contentModuleTrackingManager.GetFiles(Path);
                List<FileVm> data = new List<FileVm>();
                if (!string.IsNullOrEmpty(pENTCourseVM.Keyword))
                {
                    data = folderData.Where(p => p.Name.Contains(pENTCourseVM.Keyword.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                }
                else
                {
                    data = folderData;
                }
                List<FileVm> retFilesData = new List<FileVm>();
                if (folderData != null) 
                {
                    retFilesData = Paginate(data, pENTCourseVM.ListRange.PageIndex, pENTCourseVM.ListRange.PageSize);
                }

                return Ok(new { Courses = retFilesData, TotalRows = data.Count(), Code = 200 });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public static List<FileVm> Paginate(List<FileVm> files, int pageNumber, int pageSize)
        {
            return files
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        [HttpPost]
        [Route("uploadcoursezip")]
        [Authorize]
        public async Task<IActionResult> UploadCoursezip(ContentModuleUploadVM pEntContModule)
        {
            
            try
            {
                    if (pEntContModule.file == null)
                    {
                        return BadRequest(new { Code = 400, Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.SELECT_VALID_FILE_UPLOAD) });
                    }
                    if (string.IsNullOrEmpty(pEntContModule.ClientId))
                    {
                        return BadRequest(new { Code = 400, Msg = "Please provide Client Id" });
                    }

                    string strStatusMsg = string.Empty;
                    string strCourseId = string.Empty;
                    if (string.IsNullOrEmpty(pEntContModule.ID))
                    {
                        string strContModIdPreFix = API.DataAccessManager.Schema.Common.VAL_CONTENT_MODULE_ID_PREFIX;
                        int strContModIdLen = API.DataAccessManager.Schema.Common.VAL_CONTENT_MODULE_ID_LENGTH;
                        strCourseId = API.YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(strContModIdPreFix, strContModIdLen);
                    }
                    else
                    {
                        strCourseId = pEntContModule.ID;
                    }
                    string strRootPath = FileHandler.CLIENTS_FOLDER_PATH + "/" + pEntContModule.ClientId + "/";


                    ManifestReader srvManifestReader = new ManifestReader(pEntContModule.ClientId);                    
                    string strCourseFolderName = string.Empty;
                    
                    FileHandler FtpUpload = new FileHandler(pEntContModule.ClientId);
                    string strUploadFileName = string.Empty;
                    string hdnFTPCoursePath = string.Empty;
                   

                    string strFileExtension = Path.GetExtension(pEntContModule.file.FileName);
                    //check for file extension 
                    if (strFileExtension.ToLower() != ".zip")
                    {
                        strStatusMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.SELECT_VALID_FILE_UPLOAD);
                        return BadRequest(new { Code = 400, Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.SELECT_VALID_FILE_UPLOAD) });
                        
                    }
                    strUploadFileName = FtpUpload.ValidateFile(pEntContModule.file.FileName);
                    if (strUploadFileName != string.Empty)
                    {
                        if (!FtpUpload.IsFolderExist(strRootPath, FileHandler.COURSE_FOLDER_PATH))
                        {
                            FtpUpload.CreateFolder(strRootPath, FileHandler.COURSE_FOLDER_PATH);
                        }

                        if (Convert.ToString(pEntContModule.Isedit) == "true")
                        {
                            //string strFTP = Convert.ToString(txtFtpCoursePath.Text);
                            string strFTP = Convert.ToString(pEntContModule.ContentModuleURL);

                            //if (!string.IsNullOrEmpty(strFTP) && strFTP.IndexOf(FileHandler.COURSE_FOLDER_PATH) > 0)
                            if (!string.IsNullOrEmpty(strFTP) && strFTP.IndexOf(strRootPath + FileHandler.COURSE_FOLDER_PATH) > 0)
                            {
                                //strCourseFolderName = strFTP.Substring(strFTP.IndexOf(FileHandler.COURSE_FOLDER_PATH));
                                strCourseFolderName = strFTP.Substring(strFTP.IndexOf(strRootPath + FileHandler.COURSE_FOLDER_PATH));
                                if (pEntContModule.ContentModuleTypeId.ToLower() == "noncompliant")
                                {
                                    strCourseFolderName = strCourseFolderName.Substring(0, strCourseFolderName.LastIndexOf(Convert.ToChar("/")));
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(strCourseFolderName))
                        {
                            ////strCourseFolderName =strRootPath +  FileHandler.COURSE_FOLDER_PATH + "/" + strCourseId;
                            strCourseFolderName = strCourseId;
                        }
                        string strCourseFolder = string.Empty;
                        ////if (strCourseFolderName.IndexOf(Convert.ToChar("/")) != -1)
                        ////{
                        ////    strCourseFolder = strCourseFolderName.Substring(strCourseFolderName.IndexOf(Convert.ToChar("/")) + 1);
                        ////}
                        ////else
                        {
                            strCourseFolder = strCourseFolderName;
                        }
                        //FtpUpload.CreateFolder(FileHandler.COURSE_FOLDER_PATH, strCourseFolder);
                        FtpUpload.CreateFolder(strRootPath + FileHandler.COURSE_FOLDER_PATH + "/", strCourseFolder);

                        //strCourseFolder = FileHandler.COURSE_FOLDER_PATH + "\\" + strCourseFolder;
                        strCourseFolder = FileHandler.CLIENTS_FOLDER_PATH + "\\" + pEntContModule.ClientId + "\\" + FileHandler.COURSE_FOLDER_PATH + "\\" + strCourseId;
                        string strPath = string.Empty;
                        if (pEntContModule.KeepZipFile == true)
                        {
                            strPath = FileHandler.CLIENTS_FOLDER_PATH + "\\" + pEntContModule.ClientId + "\\" + FileHandler.COURSE_FOLDER_PATH + "\\";
                            strUploadFileName = strCourseId + ".zip";
                        }
                        else
                        {
                            strPath = FileHandler.TEMP_ZIP_FOLDER_PATH;
                        }

                        byte[] fileBytes;
                        using (var ms = new MemoryStream())
                        {
                            await pEntContModule.file.CopyToAsync(ms);
                            fileBytes = ms.ToArray();
                        }

                        if (FtpUpload.Uploadfile(strPath, strUploadFileName, fileBytes) != String.Empty)
                        {
                            bool isExecuteFunctionForMobileLMS = false;

                            if (pEntContModule.ContentModuleTypeId == ActivityContentType.Scorm2004.ToString())
                            {

                                //if (chkKeepZipFile.Checked == true)
                                //{
                                //    isExecuteFunctionForMobileLMS = FtpUpload.UnZipServerFilesForMobileLMS(strPath + strUploadFileName, strCourseFolder);
                                //}
                                //else
                                //{
                                //    isExecuteFunctionForMobileLMS = FtpUpload.UnZipServerFiles(strPath + strUploadFileName, strCourseFolder);
                                //}
                                isExecuteFunctionForMobileLMS = FtpUpload.UnZipServerFilesForScorm2004(strPath + strUploadFileName, strCourseFolder);
                                //GetCourseQuestionsAndAddRequiredFunction();
                                //#region Add Question.js file
                                //string[] QuestionsFilePath = System.Web.HttpContext.Current.Session["QuestionFilesPath"] as string[];
                                //if (QuestionsFilePath != null)
                                //{

                                //    hdnQuestionsString.Value = string.Empty;
                                //    for (int i = 0; i < QuestionsFilePath.Length; i++)
                                //    {
                                //        string contents = File.ReadAllText(@QuestionsFilePath[i]);
                                //        hdnQuestionsString.Value = hdnQuestionsString.Value + contents;
                                //    }
                                //    //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyFun1", " ExecuteQuestionsFile1();", true);
                                //}
                                //#endregion

                            }
                            else
                            {
                                if (pEntContModule.KeepZipFile == true)
                                {
                                    isExecuteFunctionForMobileLMS = FtpUpload.UnZipServerFilesForMobileLMS(strPath + strUploadFileName, strCourseFolder);
                                }
                                else
                                {
                                    isExecuteFunctionForMobileLMS = FtpUpload.UnZipServerFiles(strPath + strUploadFileName, strCourseFolder);
                                }
                            }
                            if (isExecuteFunctionForMobileLMS)
                            {
                                //txtFtpCoursePath.Text = strCourseFolder.Replace("\\", "/").ToString();
                                //hdnFTPCoursePath.Value = FtpUpload.GetCourseFTPPath(strCourseFolder.Replace("\\", "/"));
                                pEntContModule.ContentModuleURL = strCourseFolder.Replace("\\", "/").ToString();
                                hdnFTPCoursePath = FtpUpload.GetCourseFTPPath(strCourseFolder.Replace("\\", "/"));
                                pEntContModule.FTPCoursePath = hdnFTPCoursePath;
                             AICCToManifest entAiccToManifest = new AICCToManifest();
                                if (pEntContModule.ContentModuleTypeId.ToLower() == ActivityContentType.AICC.ToString().ToLower())
                                {
                                    string sFtpRootSharedPath = FtpUpload.RootSharedPath;
                                    if (sFtpRootSharedPath.Contains("\\\\\\\\"))
                                    {
                                        sFtpRootSharedPath = sFtpRootSharedPath.Replace("\\\\", "\\");
                                    }

                                    //if (entAiccToManifest.IsAICCFilesExists(sFtpRootSharedPath + FileHandler.COURSE_FOLDER_PATH + "\\" + strCourseId + "\\"))
                                    if (entAiccToManifest.IsAICCFilesExists(sFtpRootSharedPath + FileHandler.CLIENTS_FOLDER_PATH + "\\" + pEntContModule.ClientId + "\\" + FileHandler.COURSE_FOLDER_PATH + "\\" + strCourseId + "\\"))
                                    {
                                        bool blnIsReturn = false;
                                        string strCoursePath = pEntContModule.ContentModuleURL;// txtFtpCoursePath.Text;
                                                                                      //string strCourseFolder = strCoursePath.Substring(strCoursePath.IndexOf(FileHandler.COURSE_FOLDER_PATH));
                                         strCourseFolder = strCoursePath.Substring(strCoursePath.IndexOf(FileHandler.CLIENTS_FOLDER_PATH));
                                        if (strCourseFolder.ToLower().IndexOf("/imsmanifest.xml") != -1)
                                        {
                                            strCourseFolder = strCourseFolder.Substring(0, strCourseFolder.Length - 16);
                                        }
                                        AICCToManifest AccToMnfst = new AICCToManifest();
                                        blnIsReturn = false; // AccToMnfst.ConvertAICCToManifest(strCourseFolder, Server.MapPath("AICCLibrary/MasterFiles/ManifestTemplate.xml"));

                                        if (blnIsReturn)
                                        {
                                            string strErrorMsg = srvManifestReader.ReadManifestAndMetaData(strCourseId, _mapper.Map<ContentModule>(pEntContModule), hdnFTPCoursePath);
                                            if (strErrorMsg == string.Empty)
                                            {
                                                //btnSave.Enabled = true;
                                                //btnSave.CssClass = "btn input-btn btn-danger waves-effect";
                                                //ddlStandardType.Enabled = false;
                                                strStatusMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED);
                                               return Ok( new { Code = 200, ContentModule = pEntContModule, Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED) });

                                                //lblPackageMsg.Text = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED);
                                            }
                                            else
                                            {
                                                //btnSave.Enabled = false;
                                                //btnSave.CssClass = "btn input-btn btn-danger waves-effect disabled";
                                                //lblPackageMsg.Text = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED) +
                                                //                  "<br><font color='red'>" + strErrorMsg + "</font>";
                                                strStatusMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED) +  strErrorMsg ;
                                                return Ok(new
                                                {
                                                    Code = 300,
                                                    ContentModule = pEntContModule,
                                                    Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED) + strErrorMsg 
                                                });
                                            }
                                        }
                                        else
                                        {
                                            strStatusMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.INVALID_AICC_COURSE);
                                            return BadRequest( MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.INVALID_AICC_COURSE));
                                        }
                                    }
                                    else
                                    {
                                        strStatusMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.INVALID_AICC_COURSE);
                                        return BadRequest(MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.INVALID_AICC_COURSE));
                                    }

                                }
                                else if (pEntContModule.ContentModuleTypeId.ToLower() == ActivityContentType.Scorm12.ToString().ToLower())
                                {
                                    if (!string.IsNullOrEmpty(strCourseId))
                                    {
                                        string strErrorMsg = srvManifestReader.ReadManifestAndMetaData(strCourseId, _mapper.Map<ContentModule>(pEntContModule), hdnFTPCoursePath);
                                        if (strErrorMsg == string.Empty)
                                        {
                                            //btnSave.Enabled = true;
                                            //btnSave.CssClass = "btn input-btn btn-danger waves-effect";
                                            //ddlStandardType.Enabled = false;
                                            //lblPackageMsg.Text = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED);
                                            strStatusMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED);
                                            return Ok( new { Code = 200, ContentModule = pEntContModule, Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED) });

                                        }
                                        else
                                        {
                                            //btnSave.Enabled = false;
                                            //btnSave.CssClass = "btn input-btn btn-danger waves-effect disabled";
                                            //lblPackageMsg.Text = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED)
                                            //    + "<font color='red'>" + strErrorMsg + "</font>";
                                            strStatusMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED) + strErrorMsg ;
                                            return Ok (new { code = 300,
                                                ContentModule = pEntContModule,
                                                Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED) + strErrorMsg 
                                            });
                                        }
                                    }
                                    else
                                    {
                                        strStatusMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOAD_FAILED);
                                        return BadRequest( MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOAD_FAILED));
                                    }
                                }
                                else if (pEntContModule.ContentModuleTypeId.ToLower() == ActivityContentType.Scorm2004.ToString().ToLower())
                                {
                                    if (!string.IsNullOrEmpty(strCourseId))
                                    {
                                        string strErrorMsg = srvManifestReader.ReadManifestAndMetaData(strCourseId, _mapper.Map<ContentModule>(pEntContModule), hdnFTPCoursePath);
                                        if (strErrorMsg == string.Empty)
                                        {
                                            //btnSave.Enabled = true;
                                            //btnSave.CssClass = "btn input-btn btn-danger waves-effect";
                                            //ddlStandardType.Enabled = false;
                                            //lblPackageMsg.Text = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED);
                                            strStatusMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED);
                                            return Ok (new { Code = 200, ContentModule = pEntContModule, Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED) });

                                        }
                                        else
                                        {
                                            //btnSave.Enabled = false;
                                            //btnSave.CssClass = "btn input-btn btn-danger waves-effect disabled";
                                            //lblPackageMsg.Text = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED)
                                            //    + "<font color='red'>" + strErrorMsg + "</font>";
                                            strStatusMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED) + strErrorMsg;
                                            return Ok(new
                                            {
                                                Code = 300,
                                                ContentModule = pEntContModule,
                                                Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED) +  strErrorMsg 
                                            });
                                        }
                                    }
                                    else
                                    {
                                        strStatusMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOAD_FAILED);
                                        return BadRequest( MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOAD_FAILED));
                                    }
                                }

                                else if (pEntContModule.ContentModuleTypeId.ToLower() == ActivityContentType.NonCompliant.ToString().ToLower())
                                {
                                    //lblPackageMsg.Text = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED) + " Please use Point to URL to point the launching file.";
                                    //btnSave.Enabled = false;
                                    //btnSave.CssClass = "btn input-btn btn-danger waves-effect disabled";
                                    //fileExplorer.BindDirectories( FileHandler.COURSE_FOLDER_PATH + "\\" + strCourseId, true, true);

                                    //fileExplorer.BindDirectories(FileHandler.CLIENTS_FOLDER_PATH + "\\" + _strSelectedClientId + "\\" + FileHandler.COURSE_FOLDER_PATH + "\\" + strCourseId, true, true);
                                    strStatusMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED) + " Please use Point to URL to point the launching file.";
                                    return Ok(new { Code = 200, ContentModule = pEntContModule, Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED) + " Please use Point to URL to point the launching file." });

                                }

                            }
                            else
                            {
                                //btnSave.Enabled = false;
                                //btnSave.CssClass = "btn input-btn btn-danger waves-effect disabled";
                                //lblPackageMsg.Text = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_UNZIP_FAILED);
                                strStatusMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UNZIP_FAILED);
                                return BadRequest( MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UNZIP_FAILED));

                            }
                        }
                        else
                        {
                            //btnSave.Enabled = false;
                            //btnSave.CssClass = "btn input-btn btn-danger waves-effect disabled";
                            //lblPackageMsg.Text = MessageAdaptor.GetMessage(YPLMS.Services.Messages.ContentModule.COURSE_UPLOAD_FAILED);
                            strStatusMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOAD_FAILED);
                            return BadRequest( MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOAD_FAILED));

                        }
                    }

                
                if (string.IsNullOrEmpty(strStatusMsg) && strStatusMsg == MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED))
                {
                    return Ok(new { Code = 200, ContentModule = pEntContModule, Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_UPLOADED) });
                }
                else
                {
                    return BadRequest( new { Code = 400, Msg = strStatusMsg });
                }

            }
            catch (CustomException ex)
            {
                return BadRequest(ex.Message);
            }

            
            
        }

        //[HttpGet]
        //[Route("binddirectories")]
        //public async Task<IActionResult> BindDirectories(string sourcePath, bool showRootFiles)
        //{
        //    try
        //    {
        //        var root = new DirectoryNode
        //        {
        //            Name = sourcePath,
        //            Path = sourcePath,
        //            SubDirectories = _contentModuleTrackingManager.GetSubDirectories(sourcePath)
        //        };

        //        if (showRootFiles)
        //        {
        //            root.Files = _contentModuleTrackingManager.GetFiles(sourcePath);
        //        }

        //        return Ok(root);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}



        [HttpPut]
        [Route("updatecoursedetails")]
        [Authorize]
        public async Task<IActionResult> UpdateCourseDetails(ContentModule pEntContModule)
        {
            
            ContentModule contentModule = new ContentModule();

            contentModule = _contentModuleAdaptor.UpdateCourseDetails(pEntContModule);
            if (contentModule != null)
            {               
                return Ok(new { ContentModule = contentModule });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("getcontentmodulelist")]
        [Authorize]
        public async Task<IActionResult> GetContentModuleList(ContentModule pEntContModule)
        {
            
            List<ContentModule> entListContentModules = new List<ContentModule>();

            entListContentModules = _contentModuleAdaptor.GetContentModuleList(pEntContModule);
            if (entListContentModules.Count>0)
            {               
                return Ok(new { ContentModule = entListContentModules });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("getcontentmodulelistnotcompletedlist")]
        [Authorize]
        public async Task<IActionResult> GetContentModuleListNotCompletedList(ContentModule pEntContModule)
        {
            
            List<ContentModule> entListContentModules = new List<ContentModule>();

            entListContentModules = _contentModuleAdaptor.GetContentModuleListNotCompletedList(pEntContModule);
            if (entListContentModules.Count > 0)
            {               
                return Ok(new { ContentModule = entListContentModules });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("getcontentmodulelistadminhome")]
        [Authorize]
        public async Task<IActionResult> GetContentModuleListAdminHome(ContentModule pEntContModule)
        {
            
            List<ContentModule> entListContentModules = new List<ContentModule>();

            entListContentModules = _contentModuleAdaptor.GetContentModuleListAdminHome(pEntContModule);
            if (entListContentModules.Count > 0)
            {               
                return Ok(new { ContentModule = entListContentModules });
            }
            else
            {
                return BadRequest(400);
            }
        }

        [HttpPost]
        [Route("addallmodules")]
        [Authorize]
        public async Task<IActionResult> AddAllModules(List<ContentModule> pEntListContentModule)
        {

            List<ContentModule> entListContentModule = new List<ContentModule>();

            entListContentModule = _contentModuleAdaptor.AddAllModules(pEntListContentModule);
            if (entListContentModule.Count > 0)
            {
                return Ok(new { ContentModuleList = entListContentModule });
            }
            else
            {
                return BadRequest();
            }
        }

        //[HttpPost]
        //[Route("activatedeactivatemodules")]
        //public async Task<IActionResult> UpdateAllModules(List<ContentModuleVM> pEntListContentModule)
        //{
        //    bool bSelected = false;           
        //    List<ContentModule> eCourseList = new List<ContentModule>();
        //    try
        //    {
        //        foreach (var item in pEntListContentModule)
        //        {
        //            ContentModule eCourse = new ContentModule();
        //            if (item.ID != null && item.ClientId !=null)
        //            {
        //                eCourse.ID = item.ID;
        //                eCourse.ClientId = item.ClientId;
        //                eCourse = _contentModuleAdaptor.GetContentModuleByID(eCourse);
        //                if (eCourse.ID != null && eCourse.IsMasterContent == false)
        //                {
        //                    if (eCourse.ID != null)
        //                    {
        //                        bSelected = true;
        //                        eCourse.ClientId = item.ClientId;
        //                        eCourse.IsActive = Convert.ToBoolean(item.IsActive);
        //                        eCourse.LastModifiedById = item.LastModifiedById;
        //                        eCourse.LastModifiedDate = DateTime.Now;
        //                        eCourseList.Add(eCourse);

        //                    }
        //                }
        //            }
                    
        //        }

        //        if (bSelected)
        //        {
        //            if (eCourseList != null && eCourseList.Count > 0)
        //            {
        //                eCourseList = _contentModuleAdaptor.AddAllModules(eCourseList);

        //            }
        //        }
        //        if (eCourseList != null && eCourseList.Count > 0)
        //        {
        //            if (eCourseList[0].IsActive == true)
        //            {
        //                return Ok(new { ContentModuleList = eCourseList, Code = 200, Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_ACTIVATED) });
        //            }
        //            else
        //            {
        //                return Ok(new { ContentModuleList = eCourseList, Code = 200, Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSES_DEACTIVATED) });

        //            }
        //        }
        //        else
        //        {
        //            return NotFound(new { Code = 404, Msg = "No data found" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message); 
        //    }
        //}



        [HttpPost]
        [Route("activatedeactivatemodules")]
        [Authorize]
        public async Task<IActionResult> UpdateAllModules(List<ContentModuleVM> pEntListContentModule)
        {

            string strSelectedCourse = string.Empty;
            string strRecordID = string.Empty;
            string strOldData = string.Empty;
            string strNewData = string.Empty;
            bool bSelected = false;
            bool IsChangedStatus = false;
            bool bSucessfulActivateDeactivate = false;
            int iChkSelCount = 0;
            bool ActivateOrDeactivate=false;

            try
            {
                Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = pEntListContentModule[0].LastModifiedById, ClientId = pEntListContentModule[0].ClientId });

                if (_entCurrentUser.IsContentAdmin())
                {
                    List<ContentModuleMapping> eCourseList = new List<ContentModuleMapping>();
                    ContentModuleMapping eCourse = new ContentModuleMapping();
                    ActivateOrDeactivate = Convert.ToBoolean(pEntListContentModule[0].IsActive);
                    foreach (ContentModuleVM gvRow in pEntListContentModule)
                    {

                        string strEventStatus = Convert.ToString(gvRow.IsActive);
                        //if (Ctrl.Checked)
                        {
                            iChkSelCount++;
                            strSelectedCourse = gvRow.ID;  //YPLMS.Services.EncryptionManager.Decrypt(gvRow.Cells[intIdIndex].Text);
                            if (!string.IsNullOrEmpty(strSelectedCourse))
                            {
                                eCourse = new ContentModuleMapping();
                                eCourse.ID = strSelectedCourse;
                                eCourse.ClientId = null;
                                if (eCourse.ID != null)
                                {
                                    bSelected = true;
                                    //eCourse.ClientId = _entCurrentUser.ClientId;
                                    eCourse.IsActive = Convert.ToBoolean(gvRow.IsActive);
                                    eCourse.LastModifiedById = _entCurrentUser.ID;
                                    eCourse.LastModifiedDate = DateTime.Now;
                                    eCourseList.Add(eCourse);
                                }
                            }
                        }
                    }
                    if (bSelected)
                    {
                        if (eCourseList.Count > 0)
                        {
                            eCourseList = _contentModuleMappingAdaptor.BulkActivateDeactivate(eCourseList);
                            IsChangedStatus = true;
                        }
                    }
                }
                else
                {
                    List<ContentModule> eCourseList = new List<ContentModule>();
                    ContentModule eCourse = new ContentModule();
                    ActivateOrDeactivate = Convert.ToBoolean(pEntListContentModule[0].IsActive);
                    foreach (ContentModuleVM gvRow in pEntListContentModule)
                    {
                        //HtmlInputCheckBox Ctrl = gvRow.FindControl("chkClient") as HtmlInputCheckBox;
                        string strEventStatus = Convert.ToString(gvRow.IsActive);
                        // if (Ctrl.Checked)
                        {
                            iChkSelCount++;
                            strSelectedCourse = gvRow.ID;  //YPLMS.Services.EncryptionManager.Decrypt(gvRow.Cells[intIdIndex].Text);
                            if (!string.IsNullOrEmpty(strSelectedCourse))
                            {
                                eCourse = new ContentModule();
                                eCourse.ID = strSelectedCourse;
                                eCourse.ClientId = gvRow.ClientId;
                                eCourse = _contentModuleAdaptor.GetContentModuleByID(eCourse);
                                if (eCourse.ID != null && eCourse.IsMasterContent == false)
                                {
                                    if (eCourse.ID != null)
                                    {
                                        bSelected = true;
                                        eCourse.ClientId = gvRow.ClientId;
                                        eCourse.IsActive = gvRow.IsActive;
                                        eCourse.LastModifiedById = _entCurrentUser.ID;
                                        eCourse.LastModifiedDate = DateTime.Now;
                                        eCourseList.Add(eCourse);
                                    }
                                }
                                else
                                {
                                    return BadRequest(new { code = 404, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSES_RIGHTS) });

                                }
                                #region Audit Log
                                strRecordID += strSelectedCourse + ", ";
                                strOldData += (strEventStatus.Trim().ToLower() == "active" ? strSelectedCourse + " : 1" : strSelectedCourse + " : 0") + ", ";
                                if (Convert.ToBoolean(gvRow.IsActive))
                                    strNewData += strSelectedCourse + " : 1" + ", ";
                                else
                                    strNewData += strSelectedCourse + " : 0" + ", ";
                                #endregion
                            }
                        }
                    }
                    if (bSelected)
                    {
                        if (eCourseList.Count > 0)
                        {
                            eCourseList = _contentModuleAdaptor.AddAllModules(eCourseList);
                            IsChangedStatus = true;
                        }
                    }
                }
                if (IsChangedStatus)
                {
                    if (!ActivateOrDeactivate)
                    {
                        bSucessfulActivateDeactivate = true;
                        return Ok(new { code = 200, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSES_DEACTIVATED) });
                    }
                    else
                    {
                        bSucessfulActivateDeactivate = true;
                        return Ok(new { code = 200, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_ACTIVATED) });
                    }
                }
                else
                {
                    return BadRequest(new { code = 200, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModuleAllocationLicensing.SELECT_COURSE) });
                }
            }
            //if (bSucessfulActivateDeactivate)
            //{
            //    #region Activate/Deactivate Content Courses Audit Log
            //    // If record has been selected and user has authority to perform action on the selected records
            //    if (iChkSelCount > 0)
            //    {
            //        //if (!string.IsNullOrEmpty(strRecordID))
            //        //{
            //        //    AuditTrailManager entAuditTrailManager = new AuditTrailManager();
            //        //    AuditTrail entAuditTrail = new AuditTrail();
            //        //    strRecordID = strRecordID.Trim().Trim(',');
            //        //    strOldData = strOldData.Trim().Trim(',');
            //        //    strNewData = strNewData.Trim().Trim(',');
            //        //    entAuditTrail.EntityName = "Content Courses";
            //        //    entAuditTrail.SystemUserGUID = _entCurrentUser.ID;
            //        //    entAuditTrail.RecordID = strRecordID;
            //        //    entAuditTrail.OldData = strOldData;
            //        //    entAuditTrail.NewData = strNewData;
            //        //    if (ActivateOrDeactivate)
            //        //        entAuditTrail.ActionId = CommonManager.AUDITTRAILACTIVATE;
            //        //    else
            //        //        entAuditTrail.ActionId = CommonManager.AUDITTRAILDEACTIVATE;
            //        //    entAuditTrail.ClientId = _entCurrentUser.ClientId;
            //        //    entAuditTrail = entAuditTrailManager.Execute(entAuditTrail, AuditTrail.Method.Add);
            //        //}
            //    }
            //    #endregion
            //}





            //bool bSelected = false;
            //List<ContentModule> eCourseList = new List<ContentModule>();
            //try
            //{
            //    foreach (var item in pEntListContentModule)
            //    {
            //        ContentModule eCourse = new ContentModule();
            //        if (item.ID != null && item.ClientId != null)
            //        {
            //            eCourse.ID = item.ID;
            //            eCourse.ClientId = item.ClientId;
            //            eCourse = _contentModuleAdaptor.GetContentModuleByID(eCourse);
            //            if (eCourse.ID != null && eCourse.IsMasterContent == false)
            //            {
            //                if (eCourse.ID != null)
            //                {
            //                    bSelected = true;
            //                    eCourse.ClientId = item.ClientId;
            //                    eCourse.IsActive = Convert.ToBoolean(item.IsActive);
            //                    eCourse.LastModifiedById = item.LastModifiedById;
            //                    eCourse.LastModifiedDate = DateTime.Now;
            //                    eCourseList.Add(eCourse);

            //                }
            //            }
            //        }

            //    }

            //    if (bSelected)
            //    {
            //        if (eCourseList != null && eCourseList.Count > 0)
            //        {
            //            eCourseList = _contentModuleAdaptor.AddAllModules(eCourseList);

            //        }
            //    }
            //    if (eCourseList != null && eCourseList.Count > 0)
            //    {
            //        if (eCourseList[0].IsActive == true)
            //        {
            //            return Ok(new { ContentModuleList = eCourseList, Code = 200, Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSE_ACTIVATED) });
            //        }
            //        else
            //        {
            //            return Ok(new { ContentModuleList = eCourseList, Code = 200, Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.ContentModule.COURSES_DEACTIVATED) });

            //        }
            //    }
            //    else
            //    {
            //        return NotFound(new { Code = 404, Msg = "No data found" });
            //    }
            //}
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("addallmodulelanguages")]
        [Authorize]
        public async Task<IActionResult> AddAllModuleLanguages(List<ContentModuleLanguages> pEntListContentModuleLanguages)
        {
            
            List<ContentModuleLanguages> entListContentModuleLanguages = new List<ContentModuleLanguages>();

            entListContentModuleLanguages = _contentModuleAdaptor.AddAllModuleLanguages(pEntListContentModuleLanguages);
            if (entListContentModuleLanguages.Count > 0)
            {
                return Ok(new { ContentModuleLanguagesList = entListContentModuleLanguages });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("findcontentmodule")]
        [Authorize]
        public async Task<IActionResult> FindContentModule(SearchVM pEntSearch)
        {
            try
            {
                List<ContentModule> entListContentModules = new List<ContentModule>();

                entListContentModules = _contentModuleAdaptor.FindContentModule(_mapper.Map<Search>(pEntSearch));
                if (entListContentModules!= null && entListContentModules.Count > 0)
                {
                    return Ok(new { ContentModuleList = entListContentModules, Code = 200, TotalRows = entListContentModules[0].ListRange.TotalRows });
                }
                else
                {
                    return NotFound( new { Code = 404, Msg = "No Data Found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }

        [HttpPost]
        [Route("checklockcourseassessment")]
        [Authorize]
        public async Task<IActionResult> CheckLockCourseAssessment(ContentModule pEntContModule)
        {
            
            ContentModule entContModule = new ContentModule();

            entContModule = _contentModuleAdaptor.CheckLockCourseAssessment(pEntContModule);
            if (entContModule!=null)
            {
                return Ok(new { ContentModule = entContModule });
            }
            else
            {
                return BadRequest("No Data Found");
            }
        }



        [HttpGet]
        [Route("getstandardtype")]
        [Authorize]
        public async Task<IActionResult> GetStandardType(string ClientId)
        {
            try
            {
                LookUpManager _lookupManager = new LookUpManager();
                List<Lookup> entBaseList;
                Lookup entLookUpData = new Lookup();
                entLookUpData.LookupType = LookupType.StandardType;
                entLookUpData.ClientId = ClientId;
                entBaseList = _lookupManager.Execute(entLookUpData, Lookup.ListMethod.GetAllByLookupType);
                if (entBaseList != null && entBaseList.Count > 0)
                {
                    return Ok(new { StandardTypeList = entBaseList, Code = 200 });
                }
                else
                {
                    return NotFound( new { Code = 404, Msg = "No Data Found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("getcoursetype")]
        [Authorize]
        public async Task<IActionResult> GetCourseType(string ClientId)
        {
            try
            {
                LookUpManager _lookupManager = new LookUpManager();
                List<Lookup> entBaseList;
                Lookup entLookUpData = new Lookup();
                entLookUpData.LookupType = LookupType.CourseType;
                entLookUpData.ClientId = ClientId;
                entBaseList = _lookupManager.Execute(entLookUpData, Lookup.ListMethod.GetAllByLookupType);
                if (entBaseList != null && entBaseList.Count > 0)
                {
                    return Ok(new { CourseTypeList = entBaseList, Code = 200 });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No Data Found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("savecontrolframe")]
        [Authorize]
        public async Task<IActionResult> SaveControlFrame(LessonTracking pEntLesson)
        {

            ScoTrackingSerializer trackObj = new ScoTrackingSerializer();
            var data = trackObj.WriteLessonTrackingConvert(pEntLesson);
            var requestXml = new XmlDocument();
            requestXml.LoadXml(data.ToString());
            CurrentSco currentScoObj=  new CurrentSco();
            currentScoObj.SaveCurrentSco(requestXml, pEntLesson);

            //entContModTracking = contentModuleTrackingAdaptor.UpdateScannedFileName(pEntContModTracking);
            //if (entContModTracking != null)
            //{
            return Ok();
            //}
            //else
            //{
            //    return BadRequest();
            //}
        }

        [HttpPost]
        [Route("uploadthumbnail")]
        [Authorize]
        public async Task<IActionResult> UploadThumbnail([FromForm] ThumbnailUploadModel model)
        {
            try
            {
                ClientDAM adaptorClient = new ClientDAM();

                var file = model.File;
                var clientId = model.ClientId;

                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file selected.");
                }

                // Get client entity
                API.Entity.Client pEntClient = new API.Entity.Client { ID = clientId };
                //Client _entClient = adaptorClient.GetClientIdByURL(pEntClient);
                //Client _entClient = Common.GetClientByURL();
                //Client _entClient = _commonService.GetClientByURL();

                Client _entClient = adaptorClient.GetClientByID(pEntClient);

                // Create FileHandler instance
                var fileHandler = new FileHandler(clientId);

                string strRootPath = FileHandler.CLIENTS_FOLDER_PATH + "/" + clientId + "/";

                // Check and create folder if not exists
                if (!fileHandler.IsFolderExist(strRootPath, FileHandler.COURSEThumbnail))
                {
                    fileHandler.CreateFolder(strRootPath, FileHandler.COURSEThumbnail);
                }

                strRootPath += FileHandler.COURSEThumbnail;

                // Validate extension
                string fileExt = Path.GetExtension(file.FileName).ToLower();
                List<string> allowedExtensions = new List<string> { ".jpg", ".png", ".bmp", ".gif", ".jpeg" };

                if (!allowedExtensions.Contains(fileExt))
                {
                    string error = MessageAdaptor.GetMessage(YPLMS2._0.API.YPLMS.Services.Messages.Common.FILE_ERROR);
                    return BadRequest(error);
                }

                // Sanitize filename
                string originalFileName = Path.GetFileName(file.FileName);
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(originalFileName);
                string ext = Path.GetExtension(originalFileName);
                fileNameWithoutExt = Regex.Replace(fileNameWithoutExt, @"[^\w]", "_");
                string finalFileName = "Thumbnail_" + fileNameWithoutExt + ext;

                // Convert to byte[]
                using var memoryStream = new MemoryStream();
                file.CopyTo(memoryStream);
                byte[] fileData = memoryStream.ToArray();

                // Upload file using fileHandler
                string uploadedFile = fileHandler.Uploadfile(strRootPath, finalFileName, fileData);

                // Build URLs
                string fullImageUrl = _entClient.ContentServerURL + FileHandler.COURSEThumbnail + "/" + finalFileName;
                string relativeThumbUrl = FileHandler.COURSEThumbnail + "/" + finalFileName;

                return Ok(new
                {
                    success = true,
                    fileName = finalFileName,
                    imageUrl = fullImageUrl,
                    descriptionUrl = relativeThumbUrl,
                    message = "Image uploaded successfully"
                });
            }
            catch (Exception ex)
            {
                // You can log ex.ToString() to your logs
                return StatusCode(500, "An error occurred while uploading the image.");
            }
        }
    }
}
