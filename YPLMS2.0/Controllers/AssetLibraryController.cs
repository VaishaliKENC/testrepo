using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Tls;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;
using Lookup = YPLMS2._0.API.Entity.Lookup;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AssetLibraryController : ControllerBase
    {
        AssetLibraryManager _assetLibraryManager = new AssetLibraryManager();
        private readonly ILearnerDAM<Learner> _learnerdam;
        private readonly IMapper _mapper;
        private readonly IAssetLibraryAdaptor<AssetLibrary> _assetLibraryAdaptor;
        private readonly IAssetAdaptor<Asset> _assetAdaptor;
        public AssetLibraryController(ILearnerDAM<Learner> learnerdam, IMapper mapper, IAssetLibraryAdaptor<AssetLibrary> assetLibraryAdaptor, IAssetAdaptor<Asset> assetAdaptor) 
        {
            _learnerdam = learnerdam;
            _mapper = mapper;
            _assetLibraryAdaptor = assetLibraryAdaptor;
            _assetAdaptor = assetAdaptor;
        }

        [HttpPost]
        [Route("getassetfolders")]
        [Authorize]
        public async Task<IActionResult> GetAssetFolders(AssetLibraryVM pEntAssetLib)
        {
            try
            {
                Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = pEntAssetLib.CurrentUserId, ClientId = pEntAssetLib.ClientId });
                var assetTree = _assetLibraryManager.GetAssetTree(_entCurrentUser, pEntAssetLib.ClientId);
                if (assetTree != null && assetTree.Count > 0)
                {
                    return Ok(new { code = 200, AssetFolderTree = assetTree });
                }
                else
                {
                    return NotFound(new { code = 404, msg = "No data found" });
                }
                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("getassetlist")]
        [Authorize]
        public async Task<IActionResult> GetAssetList(AssetVM pEntAsset)
        {
            try
            {
                Asset entAssetObj = new Asset();
                EntityRange _entRange = new EntityRange();
                List<Asset> entAssetList = new List<Asset>();
                if (!string.IsNullOrEmpty(pEntAsset.AssetFolderId) && !string.IsNullOrEmpty(pEntAsset.ClientId))
                {
                    entAssetObj.AssetFolderId = pEntAsset.AssetFolderId;
                    entAssetObj.ClientId = pEntAsset.ClientId;
                    entAssetObj.Keyword = pEntAsset.Keyword;
                    if (pEntAsset.ListRange != null)
                    {
                        _entRange.PageIndex = pEntAsset.ListRange.PageIndex;
                        _entRange.PageSize = pEntAsset.ListRange.PageSize;
                        _entRange.SortExpression = pEntAsset.ListRange.SortExpression;
                    }
                    else
                    {
                        _entRange.PageIndex = 0;
                        _entRange.PageSize = 0;
                        _entRange.SortExpression = "AssetName Asc";
                    }

                    entAssetObj.ListRange = _entRange;
                    Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = pEntAsset.CurrentUserId, ClientId = pEntAsset.ClientId });

                    if (!(_entCurrentUser.IsSiteAdmin() || _entCurrentUser.IsSuperAdmin())) //Add Condition Bharat 06-Nov-2013
                        entAssetObj.CreatedById = _entCurrentUser.ID; //Add New Parameter bharat 21-Oct-2013


                    //call to get the all asset from the selected folder, returns DataSet
                     entAssetList = _assetAdaptor.GetAssetList(entAssetObj);
                }

                if (entAssetList != null && entAssetList.Count>0)
                {
                    return Ok(new { code = 200, AssetList = entAssetList, TotalRows = entAssetList[0].ListRange.TotalRows });
                }
                else
                {
                    return NotFound(new { code = 404, msg = "No Data found" });
                }
                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("deleteasset")]
        [Authorize]
        public async Task<IActionResult> DeleteAsset(string ClientId,string Id)
        {
            try
            {
                   Asset entAssetObj = new Asset();

                if (Id != null && ClientId != null) 
                {
                
                    entAssetObj.ID = Id;
                    entAssetObj.ClientId = ClientId;
                    entAssetObj = _assetAdaptor.DeleteAsset(entAssetObj);
                    if (entAssetObj != null)
                    {
                        return Ok(new { code = 200, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.ASSET_DELETE_SUCCESS) });

                    }
                    else
                    {
                        return NotFound( new { code = 404, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.ASSET_CANT_BE_DELETED) });
                    }
                    //#region Delete Content Asset Audit Log
                    //strRecordID += rdb.Value + ", ";
                    //if (!string.IsNullOrEmpty(strRecordID))
                    //{
                    //    AuditTrailManager entAuditTrailManager = new AuditTrailManager();
                    //    AuditTrail entAuditTrail = new AuditTrail();

                    //    strRecordID = strRecordID.Trim().Trim(',');

                    //    entAuditTrail.EntityName = "Content Asset";
                    //    entAuditTrail.SystemUserGUID = _entCurrentUser.ID;
                    //    entAuditTrail.RecordID = strRecordID;
                    //    entAuditTrail.ActionId = CommonManager.AUDITTRAILDELETE;
                    //    entAuditTrail.ClientId = _strSelectedClientId;

                    //    entAuditTrail = entAuditTrailManager.Execute(entAuditTrail, AuditTrail.Method.Add);
                    //}
                    //#endregion
                }
                else
                {
                    return BadRequest(new { code = 404, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.ASSET_CANT_BE_DELETED) });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        [Route("deactivateasset")]
        [Authorize]
        public async Task<IActionResult> DeactivateAsset(AssetVM pEntAsset)
        {
            try
            {
                Asset entAssetObj = new Asset();
                Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = pEntAsset.CurrentUserId, ClientId = pEntAsset.ClientId });

                if (pEntAsset.ID != null && pEntAsset.ClientId != null)
                {

                    entAssetObj.ID = pEntAsset.ID;
                    entAssetObj.ClientId = pEntAsset.ClientId;               
                    entAssetObj.AssetName = pEntAsset.AssetName;
                    entAssetObj.AssetFileType = pEntAsset.AssetFileType;
                    entAssetObj.IsActive = false;
                    entAssetObj.LastModifiedById = _entCurrentUser.ID;
                    entAssetObj.LanguageId = _entCurrentUser.DefaultLanguageId;
                    entAssetObj.AssetDescription = pEntAsset.AssetDescription;
                    entAssetObj.AssetFileName = pEntAsset.AssetFileName;
                    //call to update the asset, returns Asset object
                    entAssetObj = _assetAdaptor.EditAsset(entAssetObj);
                    //check if returned object is not Null
                  
                    if (entAssetObj != null)
                    {
                        return Ok(new { code = 200, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.ASSET_DEACTIVATED) });

                    }
                    else
                    {
                        return NotFound(new { code = 404, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.ASSET_ALREADY_DEACTIVATED) });
                    }
                    //#region Delete Content Asset Audit Log
                    //strRecordID += rdb.Value + ", ";
                    //if (!string.IsNullOrEmpty(strRecordID))
                    //{
                    //    AuditTrailManager entAuditTrailManager = new AuditTrailManager();
                    //    AuditTrail entAuditTrail = new AuditTrail();

                    //    strRecordID = strRecordID.Trim().Trim(',');

                    //    entAuditTrail.EntityName = "Content Asset";
                    //    entAuditTrail.SystemUserGUID = _entCurrentUser.ID;
                    //    entAuditTrail.RecordID = strRecordID;
                    //    entAuditTrail.ActionId = CommonManager.AUDITTRAILDELETE;
                    //    entAuditTrail.ClientId = _strSelectedClientId;

                    //    entAuditTrail = entAuditTrailManager.Execute(entAuditTrail, AuditTrail.Method.Add);
                    //}
                    //#endregion
                }
                else
                {
                    return BadRequest(new { code = 404, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.ASSET_CANT_BE_DELETED) });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("addassetfolders")]
        [Authorize]
        public async Task<IActionResult> AddAssetFolders(AssetLibraryVM pEntAssetLib)
        {
            try
            {
                if (pEntAssetLib.AssetFolderName != string.Empty && (!ValidationManager.ValidateString(pEntAssetLib.AssetFolderName, ValidationManager.DataType.AlphanumericUnicode) || CommonMethods.CheckSpecialChars(pEntAssetLib.AssetFolderName)))
                {
                   return BadRequest(new { code = 400, msg = ImportUsersBL.GetMessage("Please enter valid Folder name.", API.YPLMS.Services.Messages.AssetLibrary.ENTER_VALID_FOLDER_NAME, null) });
                   
                    
                }
                if (pEntAssetLib.AssetFolderDescription != string.Empty && (!ValidationManager.ValidateString(pEntAssetLib.AssetFolderDescription, ValidationManager.DataType.AlphanumericUnicode) || CommonMethods.CheckSpecialChars(pEntAssetLib.AssetFolderDescription)))
                {
                    return BadRequest(new { code = 400, msg = ImportUsersBL.GetMessage("Please enter valid  Folder description.", API.YPLMS.Services.Messages.Asset.ENTER_VALID_DESCRIPTION, null) });
                    
                }

                if (!string.IsNullOrEmpty(pEntAssetLib.AssetFolderName))
                    pEntAssetLib.AssetFolderName = CommonMethods.RemoveSpecialChars(pEntAssetLib.AssetFolderName);


                if (!string.IsNullOrEmpty(pEntAssetLib.ParentFolderId) && !string.IsNullOrEmpty(pEntAssetLib.ClientId) && !string.IsNullOrEmpty(pEntAssetLib.CurrentUserId))
                {
                    //Chk Foldername under same parent exists end
                    AssetLibrary entAssetLibraryObj = new AssetLibrary();
                    Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = pEntAssetLib.CurrentUserId, ClientId = pEntAssetLib.ClientId });
                    entAssetLibraryObj.ClientId = pEntAssetLib.ClientId;
                    entAssetLibraryObj.AssetFolderName = pEntAssetLib.AssetFolderName.Trim();
                    entAssetLibraryObj.AssetFolderDescription = pEntAssetLib.AssetFolderDescription;
                    entAssetLibraryObj.IsVisible = true;
                    entAssetLibraryObj.ParentFolderId = pEntAssetLib.ParentFolderId;
                    entAssetLibraryObj.RelativePath = _assetLibraryManager.GetFolderPath(_entCurrentUser, entAssetLibraryObj);
                    entAssetLibraryObj.LastModifiedById = _entCurrentUser.ID;
                    entAssetLibraryObj.CreatedById = _entCurrentUser.ID;

                    //call to add the asset folder, returns AssetLibrary object
                    entAssetLibraryObj = _assetLibraryAdaptor.AddAssetLibrary(entAssetLibraryObj);

                    //check if return object is not Null
                    if (entAssetLibraryObj != null)
                    {
                        
                        return Ok ( new { code = 200, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.AssetLibrary.FOLDER_ADD_SUCCESS) });
                    }
                    else
                    {
                        return NotFound(new { code = 404, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.AssetLibrary.FOLDER_ADD_FAIL) }); 

                    }
                }
                else //pop up alert to select folder
                {
                   return BadRequest ( new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.ASSET_SEL_FOLDER) });
                    
                }                

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        [Route("updateassetfolders")]
        [Authorize]
        public async Task<IActionResult> UpdateAssetFolders(AssetLibraryVM pEntAssetLib)
        {
            try
            {
                if (pEntAssetLib.AssetFolderName != string.Empty && (!ValidationManager.ValidateString(pEntAssetLib.AssetFolderName, ValidationManager.DataType.AlphanumericUnicode) || CommonMethods.CheckSpecialChars(pEntAssetLib.AssetFolderName)))
                {
                    return BadRequest(new { code = 400, msg = ImportUsersBL.GetMessage("Please enter valid Folder name.", API.YPLMS.Services.Messages.AssetLibrary.ENTER_VALID_FOLDER_NAME, null) });


                }
                if (pEntAssetLib.AssetFolderDescription != string.Empty && (!ValidationManager.ValidateString(pEntAssetLib.AssetFolderDescription, ValidationManager.DataType.AlphanumericUnicode) || CommonMethods.CheckSpecialChars(pEntAssetLib.AssetFolderDescription)))
                {
                    return BadRequest(new { code = 400, msg = ImportUsersBL.GetMessage("Please enter valid  Folder description.", API.YPLMS.Services.Messages.Asset.ENTER_VALID_DESCRIPTION, null) });

                }
                if (!string.IsNullOrEmpty(pEntAssetLib.AssetFolderName))
                    pEntAssetLib.AssetFolderName = CommonMethods.RemoveSpecialChars(pEntAssetLib.AssetFolderName);

                //if (string.IsNullOrEmpty(pEntAssetLib.ParentFolderId))
                //{
                //    AssetLibrary assetLibraryobj = _assetLibraryAdaptor.GetAssetLibraryById(_mapper.Map<AssetLibrary>(pEntAssetLib));                 
                //    pEntAssetLib.ParentFolderId = assetLibraryobj != null ? assetLibraryobj.ParentFolderId : "";
                //}

                if (!string.IsNullOrEmpty(pEntAssetLib.ID) && !string.IsNullOrEmpty(pEntAssetLib.ClientId) && !string.IsNullOrEmpty(pEntAssetLib.CurrentUserId))
                {
                    //Chk Foldername under same parent exists end
                    AssetLibrary entAssetLibraryObj = new AssetLibrary();
                    Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = pEntAssetLib.CurrentUserId, ClientId = pEntAssetLib.ClientId });
                    entAssetLibraryObj.ID = pEntAssetLib.ID;
                    entAssetLibraryObj.ClientId = pEntAssetLib.ClientId;
                    entAssetLibraryObj.AssetFolderName = pEntAssetLib.AssetFolderName.Trim();
                    entAssetLibraryObj.AssetFolderDescription = pEntAssetLib.AssetFolderDescription;
                    entAssetLibraryObj.IsVisible = true;
                    entAssetLibraryObj.ParentFolderId = pEntAssetLib.ParentFolderId;
                    entAssetLibraryObj.RelativePath = _assetLibraryManager.GetFolderPath(_entCurrentUser, entAssetLibraryObj);
                    entAssetLibraryObj.LastModifiedById = _entCurrentUser.ID;
                    entAssetLibraryObj.CreatedById = _entCurrentUser.ID;

                    //call to add the asset folder, returns AssetLibrary object
                    entAssetLibraryObj = _assetLibraryAdaptor.EditAssetLibrary(entAssetLibraryObj);

                    //check if return object is not Null
                    if (entAssetLibraryObj != null)
                    {

                        return Ok(new { code = 200, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.AssetLibrary.FOLDER_EDIT_SUCCESS) });
                    }
                    else
                    {
                        return NotFound(new { code = 404, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.AssetLibrary.FOLDER_EDIT_FAIL) });

                    }
                }
                else //pop up alert to select folder
                {
                    return BadRequest(new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.ASSET_SEL_FOLDER) });

                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("deleteassetfolders")]
        [Authorize]
        public async Task<IActionResult> DeleteAssetFolders(AssetLibraryVM pEntAssetLib)
        {
            AssetLibrary _entAssetLibraryobj = new AssetLibrary();
            AssetLibrary _entAssetLibraryReturnedobj = null;
            Asset _entAssetobj = new Asset();
            try
            {

                Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = pEntAssetLib.CurrentUserId, ClientId = pEntAssetLib.ClientId });
                if (!string.IsNullOrEmpty(Convert.ToString(pEntAssetLib.ID))
                    /*&& string.IsNullOrEmpty(this.tvAssetFolder.SelectedNode.Target)*/)
                {
                    //_strFolderId = Convert.ToString(ViewState["SelectedFolderId"]);

                    _entAssetLibraryobj.ID = pEntAssetLib.ID;
                    _entAssetLibraryobj.ClientId = pEntAssetLib.ClientId;
                    if (!(_entCurrentUser.IsSiteAdmin() || _entCurrentUser.IsSuperAdmin())) 
                        _entAssetLibraryobj.CreatedById = _entCurrentUser.ID;

                    //call to get the Asset details, returns Asset object
                    _entAssetLibraryobj = _assetLibraryAdaptor.GetAssetLibraryById(_entAssetLibraryobj);

                    //check if returned object is not Null
                    if (_entAssetLibraryobj == null)
                    {
                        return BadRequest(new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.AssetLibrary.INVALID_FOLDER_ID) });//Invalid Asset or Folder Not selected.

                    }

                    UserAdminRole entSiteAdmin = _entCurrentUser.UserAdminRole.Find(delegate (UserAdminRole entToFind)
                    { return (entToFind.RoleId == CommonKeys.SITE_ADMIN_ROLE_ID); });
                    
                    //check if folder is not selected
                    if (string.IsNullOrEmpty(pEntAssetLib.ID))
                    {
                       return BadRequest(new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Asset.ASSET_SEL_FOLDER) });
                        
                    }

                    #region Check if the Asset lib folder has any child folders

                    AssetLibrary entAssetLibraryobj = new AssetLibrary();
                    entAssetLibraryobj.ClientId = pEntAssetLib.ClientId;
                    entAssetLibraryobj.ID = pEntAssetLib.ID;

                    //call to get asset library, returns AssetLibrary object 
                    entAssetLibraryobj = _assetLibraryAdaptor.GetAssetLibraryChildCount(entAssetLibraryobj);

                    #endregion

                    //check if returned object is not Null
                    if (entAssetLibraryobj != null)
                    {
                        //check if returned object contains rows in it
                        if (entAssetLibraryobj.ListRange.TotalRows <= 0)
                        {
                            _entAssetLibraryobj.ID = pEntAssetLib.ID;
                            _entAssetLibraryobj.ClientId = pEntAssetLib.ClientId;

                            //call to deletet the asset folder, returns AssetLibrary object
                            _entAssetLibraryReturnedobj = _assetLibraryAdaptor.DeleteAssetLibrary(_entAssetLibraryobj);

                            //call to check if asset folder is deleted, returns AssetLibrary object
                            _entAssetLibraryobj = _assetLibraryAdaptor.GetAssetLibraryById(_entAssetLibraryobj);

                            //check if returned object is not Null
                            if (_entAssetLibraryobj != null)
                            {
                               return Ok(new { code = 200, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.AssetLibrary.FOLDER_DELETE_SUCCESS) });

                            }


                        }
                        else
                        {
                            return BadRequest(new { code = 400, msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.AssetLibrary.DELETE_ASSETS_UNDER_FOLDER) });
                        }
                            
                    }
                    
                }
                return BadRequest();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("getassetbyid")]
        [Authorize]
        public async Task<IActionResult> GetAssetById(AssetVM pEntAsset)
        {
            try
            {
                //Learner _entCurrentUser = _learnerdam.GetUserByID(new Learner { ID = pEntAssetLib.CurrentUserId, ClientId = pEntAssetLib.ClientId });
                Asset entAssetObj = new Asset();
                if (!string.IsNullOrEmpty(pEntAsset.ID) && !string.IsNullOrEmpty(pEntAsset.ClientId))
                {

                    entAssetObj.ID = pEntAsset.ID;// Convert.ToString(LMSSession.GetValue("AssetIdToEdit"));
                    entAssetObj.ClientId = pEntAsset.ClientId;
                    var assetobj = _assetAdaptor.GetAssetById(entAssetObj);
                    if (assetobj.ID != null)
                    {
                        LookUpManager _lookupManager = new LookUpManager();
                        List<Lookup> entBaseList;
                        Lookup entLookUpData = new Lookup();
                        entLookUpData.LookupType = LookupType.AssetFileTypes;
                        entLookUpData.ClientId = pEntAsset.ClientId;
                        entBaseList = _lookupManager.Execute(entLookUpData, Lookup.ListMethod.GetAllByLookupType);
                        assetobj.AssetFileType = entBaseList
                        .Where(x => x.LookupText.Contains(assetobj.AssetFileType)) // For partial match
                        .Select(x => x.LookupValue) // Select the matched value
                        .FirstOrDefault();
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

        

    }
}
