using AjaxControlToolkit;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Tls;
using SixLabors.ImageSharp;
using System.Data;
using System.Text.RegularExpressions;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.DataAccessManager.BusinessManager;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Client;
using YPLMS2._0.API.DataAccessManager.BusinessManager.Content;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;
using static YPLMS2._0.API.Entity.Asset;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        AssetManager _entAssetManager = new AssetManager();
        AssetLibraryManager _assetLibraryManager = new AssetLibraryManager();
        CurriculumActivityManager _activityManager = new CurriculumActivityManager();
        UserPolicyTrackingManager _usrPolicyTrackingManager = new UserPolicyTrackingManager();
        PolicyManager _policyManager = new PolicyManager();
        ClientManager _clientManager = new ClientManager();
        ContentModuleManager _contentModuleManager = new ContentModuleManager();
        QuestionnaireManager _questionnaireManager = new QuestionnaireManager();
        AssessmentManager _AssessmentManager = new AssessmentManager();
        UserAssetTrackingManager _userAssetTrackingManager = new UserAssetTrackingManager();
        Learner _entCurrentUser = new Learner();
        private readonly ILearnerDAM<Learner> _learnerdam;
        private readonly IAssetAdaptor<Asset> _assetAdaptor;
        private readonly IMapper _mapper;

        string _strSelectedClientId = string.Empty;
        string _strCurrentUserClientId = string.Empty;
        string _strCurrentUserId= string.Empty;
        string _strClientAccessURL = string.Empty;
        bool _IsGroupDocConfigurable = false;

        private readonly Common _commonService;

        

        public AssetController(ILearnerDAM<Learner> learnerdam, IMapper mapper, IAssetAdaptor<Asset> assetAdaptor, Common commonService) 
        {
            _learnerdam = learnerdam;
            _mapper = mapper;            
            _assetAdaptor = assetAdaptor;
            _commonService = commonService;
        }

        [HttpGet]
        [Route("getassettype")]
        [Authorize]
        public async Task<IActionResult> GetAssetType(string ClientId)
        {
            try
            {
                LookUpManager _lookupManager = new LookUpManager();
                List<Lookup> entBaseList;
                Lookup entLookUpData = new Lookup();
                entLookUpData.LookupType = LookupType.AssetFileTypes;
                entLookUpData.ClientId = ClientId;
                entBaseList = _lookupManager.Execute(entLookUpData, Lookup.ListMethod.GetAllByLookupType);
                if (entBaseList != null && entBaseList.Count > 0)
                {
                    return Ok(new { AssetTypeList = entBaseList, Code = 200 });
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
        [Route("addasset")]
        [Authorize]
        public async Task<IActionResult> AddAsset(Asset pEntAsset)
        {
            try
            {
                Asset entAssetObj = new Asset();
                string strMsg = string.Empty;

                if (pEntAsset.AssetName != string.Empty && (!ValidationManager.ValidateString(pEntAsset.AssetName, ValidationManager.DataType.AlphanumericUnicode) || CommonMethods.CheckSpecialChars(pEntAsset.AssetName)))
                {
                    return BadRequest(new { code = 400, msg = ImportUsersBL.GetMessage("Please enter valid Asset name.", API.YPLMS.Services.Messages.Common.PLZ_ENTER_VALID_DATA, null) });
                }
                if (pEntAsset.AssetDescription != null)
                {
                    if (pEntAsset.AssetDescription != string.Empty && (!ValidationManager.ValidateString(pEntAsset.AssetDescription, ValidationManager.DataType.AlphanumericUnicode) || CommonMethods.CheckSpecialChars(pEntAsset.AssetDescription)))
                    {
                        return BadRequest(new { code = 400, msg = ImportUsersBL.GetMessage("Please enter valid Asset description.", API.YPLMS.Services.Messages.Common.PLZ_ENTER_VALID_DATA, null) });
                    }
                }

                if (!string.IsNullOrEmpty(pEntAsset.AssetName))
                    pEntAsset.AssetName = CommonMethods.RemoveSpecialChars(pEntAsset.AssetName);

                string _strFolderId = pEntAsset.AssetFolderId;  // LMSSession.GetValue("AssetFolderId").ToString();

                //check if folder is selected & all fileds are valid
                if (!string.IsNullOrEmpty(_strFolderId) && ValidateFields(pEntAsset, (bool)pEntAsset.isEdit))
                {

                    #region Conditions to validate
                    Asset entAssetObj1 = new Asset();
                    DataSet dsAssets = new DataSet();

                    entAssetObj1.AssetFolderId = _strFolderId;
                    entAssetObj1.ClientId = pEntAsset.ClientId;

                    //call to get the all asset, returns DataSet
                    dsAssets = _entAssetManager.ExecuteDataSet(entAssetObj1, Asset.ListMethod.GetAll);
                    // WriteErrorToFile("2");
                    //check if AssetIdToEdit is in session, for edit
                    if (!string.IsNullOrEmpty(pEntAsset.ID))
                    {
                        //  WriteErrorToFile("3");
                        entAssetObj1.ID = pEntAsset.ID;
                        //call to get the Asset details, returns Asset Object
                        entAssetObj1 = _entAssetManager.Execute(entAssetObj1, Asset.Method.Get);

                        #region Check rights
                        //UserAdminRole entSiteAdmin = _entCurrentUser.UserAdminRole.Find(delegate (UserAdminRole entToFind)
                        //{ return (entToFind.RoleId == CommonKeys.SITE_ADMIN_ROLE_ID); });
                        
                        #endregion

                    }
                    else
                    {
                    }

                    #endregion


                    #region check if file name with same name already exists start

                    //check if FolderPath is in session
                    if (pEntAsset.file != null)
                    {
                        AssetLibrary entAssetLibraryObj = new AssetLibrary();
                        Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = pEntAsset.CurrentUserID, ClientId = pEntAsset.ClientId });
                        entAssetLibraryObj.ClientId = pEntAsset.ClientId;

                        var FolderPath= _assetLibraryManager.GetFolderPath(_entCurrentUser, entAssetLibraryObj);
                        //  WriteErrorToFile("5");
                        var UploadAssetArgs = pEntAsset.file;
                        if (FolderPath !=string.Empty && UploadAssetArgs.FileName != string.Empty)
                        {
                            entAssetObj.ClientId = pEntAsset.ClientId;
                            entAssetObj.AssetFolderId = _strFolderId;
                            //    WriteErrorToFile("6");
                            //call to get all asset, returns DataSet
                            DataSet dsAsset = _entAssetManager.ExecuteDataSet(entAssetObj, Asset.ListMethod.GetAll);

                            //check returned DataSet contains tables in it
                            if (dsAsset.Tables.Count != 0)
                            {

                                DataRow[] dsfoundRows = dsAsset.Tables[0].Select("RelativePath like'" + FolderPath.ToString() + UploadAssetArgs.FileName + "'");

                                //check if file name already exists
                                if (dsfoundRows.Length > 0)
                                {
                                    return BadRequest(new { code = 400, msg = ImportUsersBL.GetMessage("Folder already contains a File with the same Name.", API.YPLMS.Services.Messages.Asset.FILE_EXIST_WITH_SAME_NAME, null) });
                                }
                            }

                        }
                    }
                    #endregion //check if file name with same name already exists end


                    entAssetObj.ClientId = pEntAsset.ClientId;
                    entAssetObj.LanguageId = Language.SYSTEM_DEFAULT_LANG_ID;
                    entAssetObj.AssetName = pEntAsset.AssetName.Trim();
                    entAssetObj.AssetDescription = pEntAsset.AssetDescription != null ? pEntAsset.AssetDescription.Trim() : "";
                    entAssetObj.AssetKeywords = string.Empty;
                    entAssetObj.AssetFolderId = _strFolderId;
                    strMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.ASSET_ADD_SUCCESS);// "Asset added successfully";

                    string strGUID = API.YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix("", 8);

                    // WriteErrorToFile("7");

                    //check if user has uploaded asset file
                    if (pEntAsset.file != null)
                    {
                        //   WriteErrorToFile("8");
                        var UploadAssetArgs = pEntAsset.file;
                        if (!string.IsNullOrEmpty(UploadAssetArgs.FileName))
                        {
                            //check if uploaded file has  content in it
                            if (UploadAssetArgs.Length <= 0)
                            {
                                return BadRequest(new { code = 400, msg = ImportUsersBL.GetMessage("File is empty.", API.YPLMS.Services.Messages.Asset.FILE_SHOULD_NOTBE_EMPTY, null) });
                            }

                            //entAssetObj.AssetFileName = strGUID + "_" + UploadAssetArgs.FileName;
                            //Add function Get Valid FileName.
                            entAssetObj.AssetFileName = strGUID + "_" + CommonMethods.GetValidFileName(UploadAssetArgs.FileName);
                        }
                        else
                            entAssetObj.AssetFileName = pEntAsset.AssetFileName;
                        
                            if (pEntAsset.file.Length > 0)
                            {
                                using (var ms = new MemoryStream())
                                {
                                pEntAsset.file.CopyTo(ms);
                                    var fileBytes = ms.ToArray();
                                    entAssetObj.AssetFile = fileBytes;
                                    // act on the Base64 data
                                }
                            }
                        //entAssetObj.AssetFile = UploadAssetArgs.;
                    }
                    else
                        entAssetObj.AssetFileName = pEntAsset.AssetFileName;
                    //check if NewFileName has file name value in it
                    if (!string.IsNullOrEmpty(pEntAsset.AssetFileName))
                        entAssetObj.AssetFileName = strGUID + "_" + pEntAsset.AssetFileName;
                    //  WriteErrorToFile("9");
                    //check if user wants to print certificate
                    if (pEntAsset.IsPrintCertificate == true) entAssetObj.IsPrintCertificate = true;
                    else entAssetObj.IsPrintCertificate = false;

                    //check if user want to Download file
                    if (pEntAsset.IsDownload == true) entAssetObj.IsDownload = true;
                    else entAssetObj.IsDownload = false;


                    entAssetObj.LastModifiedById = pEntAsset.LastModifiedById;
                    entAssetObj.CreatedById = pEntAsset.CreatedById;

                    entAssetObj.ThumbnailImgRelativePath = pEntAsset.ThumbnailImgRelativePath;

                    //check if AssetIdToEdit is not in session, add request
                    if (!(bool)pEntAsset.isEdit)
                    {
                        //  WriteErrorToFile("10");
                        entAssetObj.IsActive = true;
                        //call to add the Asset, returns Asset Object
                        entAssetObj = _entAssetManager.Execute(entAssetObj, Asset.Method.Add);
                        //  WriteErrorToFile("11");
                        //check returned object for Null 
                        if (entAssetObj == null) strMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.ASSET_ADD_FAIL);
                        else
                        {
                            return Ok(new { code = 200, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.ASSET_ADD_SUCCESS) });
                        }
                    }
                    else //edit request
                    {
                        entAssetObj.ID = pEntAsset.ID;
                        //call to update the Asset, returns Asset Object
                        entAssetObj = _entAssetManager.Execute(entAssetObj, Asset.Method.Update);
                        strMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.ASSET_EDIT_SUCCESS); //"Asset Updated successfully";

                        //check returned object for Null
                        if (entAssetObj == null) strMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.ASSET_EDIT_FAIL); //"Asset failed";

                    }
                    //if (entAssetObj != null)
                    //{
                    //    SaveCategoryMapping(entAssetObj.ID, entAssetObj.ClientId, "Asset");
                    //}

                    return Ok(new { code = 200, msg = MessageAdaptor.GetMessage(strMsg) });
                    
                }
                else
                {
                    return BadRequest(new { code = 400, msg = ImportUsersBL.GetMessage("Folder Id Emplty.", API.YPLMS.Services.Messages.Asset.ASSET_SEL_FOLDER, null) });
                }


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Validate data before add udpate
        /// </summary>
        /// <returns></returns>
        private bool ValidateFields(Asset pEntAsset,bool isEdit)
        {
            string strMsg = string.Empty;
            //check if txtAssestName is Null or Empty
            if (string.IsNullOrEmpty(pEntAsset.AssetName))
                strMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.NAME_REQ);// "Asset Name is Required.";//
            //check if Asset Type is not selected
            else if (string.IsNullOrEmpty(pEntAsset.AssetFileType))
                strMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.FILETYPE_REQ);//"Select an Asset FileType from the dropdown.";
                                                                                         //check if Asset File has not been uploaded
                                                                                         //else if(string.IsNullOrEmpty(UploadAsset.FileName) && !LMSSession.IsInSession("AssetIdToEdit"))
                                                                                         //    strMsg = MessageAdaptor.GetMessage(Services.Messages.Asset.FILE_REQ);// "Select an Asset File to upload.";

            if (pEntAsset.file != null)
            {
                var UploadAssetFileName = pEntAsset.file;
                //check for proper asset file name

                string strFileExtension = Path.GetExtension(UploadAssetFileName.FileName); //abc.jpg
                if (pEntAsset.AssetFileType.Trim() == "4")
                {
                    if (!(strFileExtension.ToLower() == ".jpg" || strFileExtension.ToLower() == ".gif" || strFileExtension.ToLower() == ".jpeg" || strFileExtension.ToLower() == ".bmp"))
                    {
                        strMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.FILETYPE_MISMATCH);
                    }
                }
            }

            System.Text.RegularExpressions.Regex Regex = new System.Text.RegularExpressions.Regex(@"^([\w\.\-\~\@\!\#\$\^\&\(\)\+\=\}\{\[\]]|\s)*$");//^[A-Za-z0-9\$\!\#\^\(\)\-\[\]\&\@__\\.\+\;]+$

            if (pEntAsset.file != null)
            {
                var UploadAssetArgs = pEntAsset.file;
                //check for proper asset file name
                if (!Regex.IsMatch(UploadAssetArgs.FileName))
                {
                    strMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.NO_SPECIAL_CHARS_ALLOWED_IN_FILENAME);
                }
            }
            else
            {
                if (pEntAsset.file == null && !isEdit)
                    strMsg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.FILE_REQ);// "Select an Asset File to upload.";
            }
            //check if strMsg is not Null or Empty
            if (!string.IsNullOrEmpty(strMsg))
            {
                 return false;
            }
            else return true;
        }
       

        [HttpPost]
        [Route("getassetbyid")]
        [Authorize]
        public async Task<IActionResult> GetAssetById(string ClientId, string AssetId)
        {
            try
            {
                //Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = pEntAssetLib.CurrentUserId, ClientId = pEntAssetLib.ClientId });
                Asset entAssetObj = new Asset();
                if (!string.IsNullOrEmpty(AssetId) && !string.IsNullOrEmpty(ClientId))
                {
                    entAssetObj.ID = AssetId;// Convert.ToString(LMSSession.GetValue("AssetIdToEdit"));
                    entAssetObj.ClientId = ClientId;
                    var assetobj = _assetAdaptor.GetAssetById(entAssetObj);
                    if (assetobj.ID != null)
                    {
                        LookUpManager _lookupManager = new LookUpManager();
                        List<Lookup> entBaseList;
                        Lookup entLookUpData = new Lookup();
                        entLookUpData.LookupType = LookupType.AssetFileTypes;
                        entLookUpData.ClientId = ClientId;
                        entBaseList = _lookupManager.Execute(entLookUpData, Lookup.ListMethod.GetAllByLookupType);
                        assetobj.AssetFileType = entBaseList
                        .Where(x => x.LookupText.Contains(assetobj.AssetFileType)) // For partial match
                        .Select(x => x.LookupValue) // Select the matched value
                        .FirstOrDefault();
                        #region //get content server Url
                        API.Entity.Client entClient = new API.Entity.Client();
                        ClientDAM clientDAM = new ClientDAM();
                        entClient.ID = ClientId;
                        entClient = clientDAM.GetClientByID(entClient);
                        assetobj.ContentServerURL = entClient.ContentServerURL;
                        #endregion

                        return Ok(new { code = 200, Asset = assetobj });
                    }
                    else
                    {
                        return NotFound(new { code = 404, msg = "No data found" });
                    }
                }
                else
                {
                    return BadRequest("Please provide Asset Id and Cliend Id");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("previewasset")]
        [Authorize]
        public async Task<IActionResult> PreviewAsset([FromBody] PreviewAssetRequest request)  //(string ClientId,string CurrentUserID,string ActivityId,string ActivityType,string LanguageId,string IsForAdminPreview,string WatchedInMins,string Progress,string TotalVideoDurationInMins)
        {
            _strCurrentUserClientId = request.ClientId;
            _strCurrentUserId = request.CurrentUserID;

            #region Request from AssetLib and PolicylLib Preview link
            try
            {
                // Check if Querystring is Not Null
                //if (Request.QueryString["AdminAssetPolicyLib"] != null) Page.Title = Request.QueryString["AdminAssetPolicyLib"].ToString();
            }
            catch { }
            #endregion

            Client entClient = new Client();
            string strActivityId = request.ActivityId;
            string strActivityType = request.ActivityType;
            string strLanguageID = request.LanguageId;

            //LMSSession.AddSessionItem(CommonManager.SESSION_TRACK_ID, strActivityId);

            entClient.ID = _strCurrentUserClientId;
            // Get Client Details
            entClient = _clientManager.Execute(entClient, Client.Method.Get);
            // Check for Activity type
            //switch (strActivityType)
            //{
            //}

            _strSelectedClientId = request.ClientId;
            FeatureList entFeatureList = new FeatureList();
            FeatureListManager entFeatureListMngr = new FeatureListManager();
            entFeatureList.ClientId = _strSelectedClientId;
            entFeatureList.ID = ClientFeature.FEA_ID_GroupDocConfiguration;
            entFeatureList = entFeatureListMngr.Execute(entFeatureList, FeatureList.Method.Get);
            if (entFeatureList != null && entFeatureList.IsActive)
            {
                _IsGroupDocConfigurable = true;

                //if (CommonManager.IsDebugMode())
                //{
                //    _strClientAccessURL = "http://localhost:51272/Admin/Viewer.aspx";
                //}
                //else
                //{
                //    _strClientAccessURL = YPLMS2._0.API.YPLMS.Services.Common.GetClientAccessURL(entClient.ClientAccessURL, entClient.IsHTTPSAllowed) + "/" + Convert.ToString(ConfigurationManager.AppSettings["SubDirectoryName"]) + "/Admin/Viewer.aspx";

                //}
            }
            // Check for Activity type
            switch (strActivityType)
            {
                case "Asset": // If Asset
                    {
                        try
                        {
                            #region Call to Delete the Tracking for Admin

                            CurriculumTracking objTrackingDelete = new CurriculumTracking();
                            CurriculumTracking objTrackingDeleteReturn = null;
                            CurriculumTrackingManager _mgr = new CurriculumTrackingManager();

                            try
                            {
                                objTrackingDelete.ClientId = _strSelectedClientId;
                                objTrackingDelete.ID = strActivityId;
                                objTrackingDelete.UserID = request.CurrentUserID;
                                objTrackingDelete.ActivityName = Convert.ToString(ActivityContentType.Asset);
                                objTrackingDeleteReturn = _mgr.Execute(objTrackingDelete, CurriculumTracking.Method.DeleteAdminPreviewTracking);
                            }
                            catch { }

                            #endregion

                            #region Add Admin Asset tracking
                            try
                            {
                                UserAssetTracking UsrAssetObj = new UserAssetTracking();
                                Asset entAsset = new Asset();
                                AssetManager mgrAsset = new AssetManager();
                                entAsset.ID = strActivityId;
                                entAsset.LanguageId = null;
                                entAsset.ClientId = entClient.ID;
                                // Get Asset Details
                                entAsset = mgrAsset.Execute(entAsset, Asset.Method.Get);


                                if (entAsset.AssetFileType.ToLower() == ".mp4")
                                {
                                    UserAssetTracking ObjTracking = new UserAssetTracking();

                                    ObjTracking.ID = strActivityId + "-" + request.CurrentUserID;
                                    ObjTracking.ClientId = entClient.ID;
                                    ObjTracking.SystemUserGUID = request.CurrentUserID;
                                    ObjTracking.AssetId = strActivityId;
                                    ObjTracking = _userAssetTrackingManager.Execute(ObjTracking, UserAssetTracking.Method.Get);

                                    //if video is already started or completed then don't insert new tracking
                                    if (Convert.ToString(ObjTracking.CompletionStatus).ToLower().Trim() != "started" && Convert.ToString(ObjTracking.CompletionStatus).ToLower().Trim() != "completed")
                                    {
                                        UsrAssetObj.ID = strActivityId + "-" + request.CurrentUserID;
                                        UsrAssetObj.AssetId = strActivityId;
                                        UsrAssetObj.SystemUserGUID = request.CurrentUserID;
                                        UsrAssetObj.CompletionStatus = ActivityCompletionStatus.Started;
                                        UsrAssetObj.ClientId = entClient.ID;
                                        UsrAssetObj.IsForAdminPreview = request.IsForAdminPreview;
                                        UsrAssetObj.WatchedInMins = 0;
                                        UsrAssetObj.Progress = 0;
                                        UsrAssetObj.DateOfStart = DateTime.Now;
                                        //UsrAssetObj.AddSessionItem(CommonManager.SESSION_TRACK_STATUS, ActivityCompletionStatus.Started);

                                        UsrAssetObj = _userAssetTrackingManager.Execute(UsrAssetObj, UserAssetTracking.Method.Add);

                                    }
                                }
                                else
                                {
                                    //UserAssetTracking UsrAssetObj = new UserAssetTracking();
                                    // If User is SiteAdmin or SuperAdmin
                                    if (_entCurrentUser.IsSiteAdmin() || _entCurrentUser.IsSuperAdmin())
                                    {

                                        UsrAssetObj.IsForAdminPreview = true;
                                    }

                                    UsrAssetObj.AssetId = strActivityId;
                                    UsrAssetObj.SystemUserGUID = request.CurrentUserID;
                                    UsrAssetObj.DateOfCompletion = DateTime.Now;
                                    UsrAssetObj.CompletionStatus = ActivityCompletionStatus.Completed;
                                    UsrAssetObj.ClientId = entClient.ID;
                                    UsrAssetObj.IsForAdminPreview = request.IsForAdminPreview;
                                    UsrAssetObj.Progress = request.Progress;
                                    // Add Tracking
                                    UsrAssetObj = _userAssetTrackingManager.Execute(UsrAssetObj, YPLMS2._0.API.Entity.UserAssetTracking.Method.Add);
                                }
                            }
                            catch
                            {

                            }
                            #endregion

                            #region LaunchActivity

                            Asset entAssetObj = new Asset();
                            entAssetObj.ID = strActivityId;
                            entAssetObj.LanguageId = strLanguageID;
                            entAssetObj.ClientId = _strCurrentUserClientId;
                            // Get Asset Details
                            entAssetObj = _entAssetManager.Execute(entAssetObj, Asset.Method.Get);

                            AssetLibrary entAssetLibrary = new AssetLibrary();
                            AssetLibraryAdaptor entAssetLibAdp = new AssetLibraryAdaptor();
                            entAssetLibrary.ID = entAssetObj.AssetFolderId;
                            entAssetLibrary.ClientId = _strCurrentUserClientId;
                            //entAssetLibrary.CreatedById = pEntAsset.CreatedById;
                            entAssetLibrary = entAssetLibAdp.GetAssetLibraryById(entAssetLibrary);

                            if (entAssetObj.AssetFileType.ToLower() == ".mp4")
                            { 
                                UserAssetTracking Tracking = new UserAssetTracking();
                                Tracking.ID = strActivityId + "-" + request.CurrentUserID;
                                Tracking.ClientId = entClient.ID;
                                Tracking.SystemUserGUID = request.CurrentUserID;
                                Tracking.AssetId = strActivityId;
                                Tracking = _userAssetTrackingManager.Execute(Tracking, UserAssetTracking.Method.Get);
                                entAssetObj.TrackingData = Tracking;
                            }

                            entAssetObj.RelativePath = entAssetLibrary.RelativePath + entAssetObj.AssetFileName;

                            #endregion
                            return Ok(new { code = 200, Asset = entAssetObj });
                           // break;
                        }
                        catch (Exception ex)
                        {

                            return BadRequest(ex.Message);
                        }
                        

                       
                    }
                case "Policy": // If Activity is Policy
                    {
                        try
                        {

                            #region Call to Delete the Tracking for Admin

                            CurriculumTracking objTrackingDelete = new CurriculumTracking();
                            CurriculumTracking objTrackingDeleteReturn = null;
                            CurriculumTrackingManager _mgr = new CurriculumTrackingManager();

                            try
                            {
                                objTrackingDelete.ClientId = _strSelectedClientId;
                                objTrackingDelete.ID = strActivityId;
                                objTrackingDelete.UserID = _entCurrentUser.ID;
                                objTrackingDelete.ActivityName = Convert.ToString(ActivityContentType.Policy);
                                objTrackingDeleteReturn = _mgr.Execute(objTrackingDelete, CurriculumTracking.Method.DeleteAdminPreviewTracking);
                            }
                            catch { }

                            #endregion

                            #region Add Admin Policy Tracking

                            try
                            {
                                UserPolicyTracking objUserPolicyTracking = new UserPolicyTracking();
                                // Is User is SuperAdmin or SiteAdmin
                                if (_entCurrentUser.IsSiteAdmin() || _entCurrentUser.IsSuperAdmin())
                                {

                                    objUserPolicyTracking.IsForAdminPreview = true;

                                }

                                objUserPolicyTracking.ClientId = entClient.ID;
                                objUserPolicyTracking.CompletionStatus = ActivityCompletionStatus.Completed;
                                objUserPolicyTracking.SystemUserGUID = _entCurrentUser.ID;
                                objUserPolicyTracking.DateOfCompletion = DateTime.Now;
                                objUserPolicyTracking.PolicyId = strActivityId;
                                // Add Policy Tracking
                                objUserPolicyTracking = _usrPolicyTrackingManager.Execute(objUserPolicyTracking, UserPolicyTracking.Method.Add);


                            }
                            catch
                            { }
                            #endregion

                            #region LaunchActivity
                            Policy entPolicyObj = new Policy();
                            entPolicyObj.ID = strActivityId;
                            entPolicyObj.LanguageId = strLanguageID;
                            entPolicyObj.ClientId = _strCurrentUserClientId;
                            // Get Policy Details
                            entPolicyObj = _policyManager.Execute(entPolicyObj, Policy.Method.Get);

                            PolicyLibrary entPolicyLibrary = new PolicyLibrary();
                            PolicyLibraryAdaptor entPolicyLibAdp = new PolicyLibraryAdaptor();
                            entPolicyLibrary.ID = entPolicyObj.PolicyFolderId;
                            entPolicyLibrary.ClientId = _strCurrentUserClientId;
                            //entAssetLibrary.CreatedById = pEntAsset.CreatedById;
                            entPolicyLibrary = entPolicyLibAdp.GetPolicyLibraryById(entPolicyLibrary);

                            entPolicyObj.RelativePath = entPolicyLibrary.RelativePath + entPolicyObj.PolicyFileNameLink;


                            // Check Either IsLink is True or RelativePath is Not Null
                            if (entPolicyObj.IsLink != false || entPolicyObj.RelativePath != null)
                            {
                                return Ok(new { code = 200, Policy = entPolicyObj });
                            }
                            else
                            {
                                return BadRequest(new { code = 400, msg = "File Not Found." });
                            }


                            #endregion
                        }
                        catch (Exception ex)
                        {

                            return BadRequest(ex.Message);
                        }
                    }
                case "Certification": // If Activity is Certification
                    {

                        #region Call to Delete the Tracking for Admin

                        CurriculumTracking objTrackingDelete = new CurriculumTracking();
                        CurriculumTracking objTrackingDeleteReturn = null;
                        CurriculumTrackingManager _mgr = new CurriculumTrackingManager();

                        try
                        {
                            objTrackingDelete.ClientId = _strSelectedClientId;
                            objTrackingDelete.ID = strActivityId;
                            objTrackingDelete.UserID = _entCurrentUser.ID;
                            objTrackingDelete.ActivityName = Convert.ToString(ActivityContentType.Certification);
                            objTrackingDeleteReturn = _mgr.Execute(objTrackingDelete, CurriculumTracking.Method.DeleteAdminPreviewTracking);
                        }
                        catch { }

                        #endregion

                        try
                        {
                            return Ok(new { code = 200, Certificate = new { CertificateID = EncryptionManager.Encrypt(strActivityId), CurrId = request.CurrentUserID } });
                            
                        }
                        catch(Exception ex)
                        {

                            return BadRequest(ex.Message);
                        }
                    }

                case "Course": // If Activity is Course
                    {
                        if (entClient == null) // If Client is Null
                        {
                            entClient = new Client();
                            entClient.ID = _entCurrentUser.ClientId;
                            // Get Client Details
                            entClient = _clientManager.Execute(entClient, Client.Method.Get);
                        }


                        #region Call to Delete the Tracking for Admin

                        CurriculumTracking objTrackingDelete = new CurriculumTracking();
                        CurriculumTracking objTrackingDeleteReturn = null;
                        CurriculumTrackingManager _mgr = new CurriculumTrackingManager();

                        try
                        {
                            objTrackingDelete.ClientId = _strSelectedClientId;
                            objTrackingDelete.ID = strActivityId;
                            objTrackingDelete.UserID = _entCurrentUser.ID;
                            objTrackingDelete.ActivityName = Convert.ToString(ActivityContentType.Course);
                            objTrackingDeleteReturn = _mgr.Execute(objTrackingDelete, CurriculumTracking.Method.DeleteAdminPreviewTracking);
                        }
                        catch { }

                        #endregion


                        ContentModule entContentModuleObj = new ContentModule();
                        entContentModuleObj.ID = strActivityId;
                        entContentModuleObj.ClientId = _strCurrentUserClientId;
                        // Get Course Details
                        entContentModuleObj = _contentModuleManager.Execute(entContentModuleObj, ContentModule.Method.Get);

                        string sURL = entClient.ClientCluster.CoursePlayerURL + "?AID=" + System.Net.WebUtility.UrlEncode(EncryptionManager.Encrypt(strActivityId)) + "&client=" + System.Net.WebUtility.UrlEncode(EncryptionManager.Encrypt(Convert.ToString(_entCurrentUser.ClientId))) + "&learner=" + System.Net.WebUtility.UrlEncode(EncryptionManager.Encrypt(Convert.ToString(_entCurrentUser.ID))) + "&Preview=admin";
                        // Launch Course
                        //LounchActivityCourse(sURL);
                        return Ok(new { code = 200, URL = sURL });



                        break;
                    }
                case "Questionnaire": // If Questionnarie
                    {
                        Questionnaire entQuestionnaireObj = new Questionnaire();

                        entQuestionnaireObj.ID = strActivityId;
                        entQuestionnaireObj.ClientId = _strCurrentUserClientId;
                        // Get Questionnarie Details
                        entQuestionnaireObj = _questionnaireManager.Execute(entQuestionnaireObj, Questionnaire.Method.Get);

                        string sEncQntrId = System.Net.WebUtility.UrlEncode(EncryptionManager.Encrypt(Convert.ToString(strActivityId))).ToString();
                        string sEncLangId = System.Net.WebUtility.UrlEncode(EncryptionManager.Encrypt(Language.SYSTEM_DEFAULT_LANG_ID)).ToString();

                        return Ok(new { code = 200, Certificate = new { QntrId = sEncQntrId, LangId = sEncLangId } });

                    }

                case "Assessment": // If Assessment
                    {
                        AssessmentDates entAssessmentObj = new AssessmentDates();

                        entAssessmentObj.ID = strActivityId;
                        entAssessmentObj.ClientId = _strCurrentUserClientId;
                        // Get Assessment Details
                        entAssessmentObj = _AssessmentManager.Execute(entAssessmentObj, AssessmentDates.Method.Get);

                        string sEncQntrId = System.Net.WebUtility.UrlEncode(EncryptionManager.Encrypt(Convert.ToString(strActivityId))).ToString();
                        string sEncLangId = System.Net.WebUtility.UrlEncode(EncryptionManager.Encrypt(Language.SYSTEM_DEFAULT_LANG_ID)).ToString();

                        return Ok(new { code = 200, Certificate = new { AssessmentId = sEncQntrId, Time = entAssessmentObj.AssessmentTime, AlertTime = entAssessmentObj.AssessmentAlertTime, LangId = sEncLangId } });
                    }
            }
            return BadRequest(new { code = 400, msg = "File Not Found." });
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
                if (!fileHandler.IsFolderExist(strRootPath, FileHandler.ASSETThumbnail))
                {
                    fileHandler.CreateFolder(strRootPath, FileHandler.ASSETThumbnail);
                }

                strRootPath += FileHandler.ASSETThumbnail;

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
                string fullImageUrl = _entClient.ContentServerURL + FileHandler.ASSETThumbnail + "/" + finalFileName;
                string relativeThumbUrl = FileHandler.ASSETThumbnail + "/" + finalFileName;

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
