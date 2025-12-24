using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.DataAccessManager.BusinessManager.User;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImportDefinationController : ControllerBase
    {
        private readonly IImportDefinitionAdaptor<ImportDefination> _importDefinitionAdaptor;
        private readonly IMapper _mapper;
        private readonly IAutoEmailTemplateSettingAdaptor<AutoEmailTemplateSetting> _autoEmailTemplateSettingAdaptor;
        private readonly ILearnerDAM<Learner> _learnerdam;
        public ImportDefinationController(IImportDefinitionAdaptor<ImportDefination> importDefinitionAdaptor, IMapper mapper, IAutoEmailTemplateSettingAdaptor<AutoEmailTemplateSetting> autoEmailTemplateSettingAdaptor, ILearnerDAM<Learner> learnerdam)
        {
            _importDefinitionAdaptor = importDefinitionAdaptor;
            _mapper = mapper;
            _autoEmailTemplateSettingAdaptor = autoEmailTemplateSettingAdaptor;
            _learnerdam = learnerdam;
        }

        [HttpPost]
        [Route("getimportdefinationbyid")]
        [Authorize]
        public async Task<IActionResult> GetImportDefinationById(ImportDefinationVM pEntImportDefination)
        {
            try
            {
                ImportDefination entImportDefinition = new ImportDefination();

                entImportDefinition = _importDefinitionAdaptor.GetImportDefinationById(_mapper.Map<ImportDefination>(pEntImportDefination));
                if (entImportDefinition != null)
                {

                    return Ok(new { ImportDefination = entImportDefinition, Code = 200 });
                }
                else
                {
                    return NotFound( new { Code = 404, Msg = "No data found" } );
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        [Route("editimportdefination")]
        [Authorize]
        public async Task<IActionResult> EditImportDefination(ImportDefinationVM pEntImportDefination)
        {
            try
            {
                ImportDefination entImportDefinition = new ImportDefination();

                entImportDefinition = _importDefinitionAdaptor.EditImportDefination(_mapper.Map<ImportDefination>(pEntImportDefination));
                if (entImportDefinition.ID != null)
                {

                    return Ok(new { ImportDefination = entImportDefinition, Code = 200, Msg = "Configure profile updated successfully" });
                }
                else
                {
                    return NotFound(new { Code = 404, Msg = "No data found" });
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            
        }

        [HttpPost]
        [Route("getimportdefinationlist")]
        [Authorize]
        public async Task<IActionResult> GetImportDefinationList(ImportDefinationVM pEntImportDefination)
        {
            try
            {
                List<ImportDefination> entListImportDefination = new List<ImportDefination>();

                entListImportDefination = _importDefinitionAdaptor.GetImportDefinationList(_mapper.Map<ImportDefination>(pEntImportDefination));
                if (entListImportDefination != null && entListImportDefination.Count>0)
                {

                    return Ok(new { ImportDefinationList = entListImportDefination, Code = 200, TotalRows = entListImportDefination[0].ListRange.TotalRows });
                }
                else
                {
                    return NotFound( new { Code = 404, Msg = "No data found"});
                }
            }
            catch (Exception ex)
            {

                return BadRequest( ex.Message);
            }

            
        }

        [HttpGet]
        [Route("importdefinationsendmail")]
        [Authorize]
        public async Task<IActionResult> ImportDefinationSendMail(string ClientID, string CurrentUserId, string EmailID)
        {
            try
            {

                if (ValidationManager.ValidateString(Convert.ToString(EmailID), ValidationManager.DataType.EmailID))
                {
                    try
                    {
                        // btn_CreateCSV.Enabled = false;
                        //lblMsg.Text = string.Empty;
                        //lblMsg.CssClass = "no-errorMsg";
                        //lblMsg.ForeColor = System.Drawing.Color.Black;                        
                        ImportDefination ImportDefination = new ImportDefination();
                        List<ImportDefination> entListImportDefination = new List<ImportDefination>();
                        ImportDefination.ClientId = ClientID;
                        ImportDefination.ListRange = new EntityRange();
                        ImportDefination.ListRange.PageSize = 0;
                        ImportDefination.ListRange.PageIndex = 0;
                        ImportDefination.ListRange.SortExpression = "MinLength";
                        entListImportDefination = _importDefinitionAdaptor.GetImportDefinationList(ImportDefination);
                        API.YPLMS.Services.Converter dsConverter = new API.YPLMS.Services.Converter();
                        DataSet dsData = dsConverter.ConvertToDataSet<ImportDefination>(entListImportDefination);
                        DataTable dtData = dsData.Tables[0];

                        var columnMap = new Dictionary<string, string>
                        {
                            {"ImportDefination_ID" , "Sr No" },
                            {"FieldName" , "Field Name" },
                            { "MinLength", "Min Length" },
                            { "MaxLength", "Max Length" },
                            { "AllowBlank", "Allow Blanks" },
                            { "FieldErrorLevel", "Criticality" },
                            { "FieldType", "Field Type" },
                            { "FieldValueType", "Field Data Type" },
                            { "IsDefault", "Use Default Value" },
                            { "DefaultValue", "Default Value" },
                            { "FieldTypes", "Field Types" },
                            { "IsMandatory", "Is Mandatory" },
                        };



                        // Remove the unwanted columns from the CSV file
                        dtData.Columns.Remove("ContactPersonDetails");
                        dtData.Columns.Remove("LastModifiedDate");
                        dtData.Columns.Remove("CreatedByName");
                        dtData.Columns.Remove("LastModifiedByName");
                        dtData.Columns.Remove("categoryId");
                        dtData.Columns.Remove("createdById");
                        dtData.Columns.Remove("currentUserID");
                        dtData.Columns.Remove("lastModifiedById");
                        //dtData.Columns.Remove("lastModifiedDate");
                        dtData.Columns.Remove("lengthError");
                        dtData.Columns.Remove("maxLengthInDB");
                        dtData.Columns.Remove("otherError");
                        dtData.Columns.Remove("reportId");
                        dtData.Columns.Remove("ruleId");
                        dtData.Columns.Remove("ClientId");
                        dtData.Columns.Remove("ImportAction");
                        dtData.Columns.Remove("KeyWord");
                        dtData.Columns.Remove("ErrorLevelType");
                        dtData.Columns.Remove("DateCreated");
                        dtData.Columns.Remove("ID");
                        dtData.Columns.Remove("FieldDataType");
                        dtData.Columns.Remove("RuleName");
                        dtData.Columns.Remove("SendMail");
                        dtData.Columns.Remove("MailOptionsVisible");
                        dtData.Columns.Remove("IsAutoMail");
                        dtData.Columns.Remove("BusinessRuleId");
                        dtData.Columns.Remove("IsProductAdmin");



                        foreach (DataColumn col in dtData.Columns)
                        {
                            if (columnMap.ContainsKey(col.ColumnName))
                            {
                                col.ColumnName = columnMap[col.ColumnName];
                            }
                        }

                        EmailMessages mail = new EmailMessages(ClientID);
                        EmailService mailMessage = new EmailService(ClientID);
                        AutoEmailTemplateSetting autoEmail = new AutoEmailTemplateSetting();
                        autoEmail.ID = AutoEmailTemplateSetting.EVENT_ADMIN_SEND_IDF;
                        autoEmail.ClientId = ClientID;
                        autoEmail = _autoEmailTemplateSettingAdaptor.GetEmailEventById(autoEmail);
                        if (!string.IsNullOrEmpty(autoEmail.EmailTemplateID))
                        {
                            System.Net.Mail.Attachment attachement = new System.Net.Mail.Attachment(ImportDefinationManager.CreateCSVFile(dtData), "UserImportFile.csv", "text/csv");
                            List<System.Net.Mail.Attachment> attachList = new List<System.Net.Mail.Attachment>();
                            attachList.Add(attachement);
                            //mail.SendPublicMail(autoEmail.EmailTemplateID, null, null, null, null, txtEmailAddr.Text.Trim(), false, attachList);

                            ///New Code Added Sujit /////
                            Learner _entNewUser = new Learner();

                            _entNewUser.ClientId = ClientID;
                            _entNewUser.ID = CurrentUserId;
                            _entNewUser = _learnerdam.GetUserByID(_entNewUser);

                            mail.SendPublicMailForLink(autoEmail.EmailTemplateID, null, _entNewUser.DefaultLanguageId, null, null, EmailID.Trim(), false, attachList, _entNewUser);

                            return Ok(new { Code = 200, Msg = MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.Common.EMAIL_SUCCESS_TO) });
                        }
                        else
                        {
                            //lblMsg.ForeColor = System.Drawing.Color.Red;


                            return BadRequest(MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.EmailMessages.EMAIL_NOT_CONFIGURED));
                        }
                        //btn_CreateCSV.Enabled = true;
                    }
                    catch (CustomException ex)
                    {
                        return BadRequest(ex.Message);
                    }
                    //txtEmailAddr.Text = string.Empty;
                }
                else
                {

                    return BadRequest(MessageAdaptor.GetMessage(API.YPLMS.Services.Messages.User.INVALID_EMAIL_ID) + " " + EmailID.Trim());

                }


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
