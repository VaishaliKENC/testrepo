using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    public class ImportUsersBL
    {


        // OrganizationLevelUnitDAM _organizationLevelUnitDAM = new OrganizationLevelUnitDAM();
        OrganizationLevelDAM _organizationLevelDAM = new OrganizationLevelDAM();


        /// <summary>
        /// Create and return organization levels table.
        /// </summary>
        /// <param name="sClientId"></param>
        /// <returns></returns>
        public static DataTable GetOrgLevels(string sClientId)
        {
            OrganizationLevelDAM _organizationLevelDAM = new OrganizationLevelDAM();
            DataTable dtableOrgLvl = new DataTable();
            try
            {
                OrganizationLevel endOrganizationLevels = new OrganizationLevel();
                endOrganizationLevels.ClientId = sClientId;
                List<OrganizationLevel> entListOrganizationLevels = _organizationLevelDAM.GetOnlyLevels(endOrganizationLevels);
                DataColumn dcolOrgLvlId = new DataColumn();
                dcolOrgLvlId.DataType = Type.GetType("System.String");
                dcolOrgLvlId.ColumnName = "LevelId";
                dtableOrgLvl.Columns.Add(dcolOrgLvlId);
                DataColumn dcolOrgLvlName = new DataColumn();
                dcolOrgLvlName.DataType = Type.GetType("System.String");
                dcolOrgLvlName.ColumnName = "LevelName";
                dtableOrgLvl.Columns.Add(dcolOrgLvlName);
                DataColumn dcolOrgLvlOrder = new DataColumn();
                dcolOrgLvlOrder.DataType = Type.GetType("System.Int32");
                dcolOrgLvlOrder.ColumnName = "LevelOrder";
                dtableOrgLvl.Columns.Add(dcolOrgLvlOrder);

                foreach (OrganizationLevel entOrganizationLevel in entListOrganizationLevels)
                {
                    DataRow drowOrgLvl = dtableOrgLvl.NewRow();
                    drowOrgLvl["LevelId"] = entOrganizationLevel.ID;
                    drowOrgLvl["LevelName"] = entOrganizationLevel.LevelName;
                    drowOrgLvl["LevelOrder"] = entOrganizationLevel.LevelOrder;
                    dtableOrgLvl.Rows.Add(drowOrgLvl);
                }
            }
            catch { }
            return dtableOrgLvl;
        }

        /// <summary>
        /// Function adds the levelName
        /// </summary>
        /// <param name="pstrClientId"></param>
        /// <param name="strLevelName"></param>
        /// <returns></returns>
        public static OrganizationLevel AddNewOrgLevel(string pstrClientId, string pstrCreatedById, string strLevelName)
        {
            OrganizationLevel entOrganizationLevel = new OrganizationLevel();
            OrganizationLevel entOrganizationLevelreturned = new OrganizationLevel();
            List<BaseEntity> entListlevelBase = new List<BaseEntity>();
            OrganizationLevelDAM _organizationLevelDAM = new OrganizationLevelDAM();
            int iLastLevelOrder = 1;
            try
            {
                //get levelorder start
                /* Commented By Ashish*/
                /*entOrganizationLevel.ClientId = pstrClientId;
                entListlevelBase = _entManager.Execute(entOrganizationLevel, (BaseEntity.ListMethod)OrganizationLevel.ListMethod.GetOnlyLevels);
                if (entListlevelBase.Count > 0) iLastLevelOrder = entListlevelBase.Count;
                foreach (BaseEntity baseEnt in entListlevelBase)
                {
                    OrganizationLevel orglvl = (OrganizationLevel)baseEnt;
                    if (orglvl.LevelName == strLevelName)
                    {
                        return entOrganizationLevel;
                    }
                }
                entOrganizationLevel.ClientId = pstrClientId;
                entOrganizationLevel.LevelName = strLevelName;
                entOrganizationLevel.LevelOrder = iLastLevelOrder;
                entOrganizationLevel.LastModifiedById = pstrCreatedById;
                entOrganizationLevel.CreatedById = pstrCreatedById;
                entOrganizationLevelreturned = (OrganizationLevel)_entManager.Execute(entOrganizationLevel, (BaseEntity.Method)OrganizationLevel.Method.Add);
                */
                entOrganizationLevel.ClientId = pstrClientId;
                entOrganizationLevel.LevelName = strLevelName;
                entOrganizationLevel.LevelOrder = iLastLevelOrder;
                entOrganizationLevel.LastModifiedById = pstrCreatedById;
                entOrganizationLevel.CreatedById = pstrCreatedById;
                entOrganizationLevelreturned = (OrganizationLevel)_organizationLevelDAM.CheckandAddOrganizationLevel(entOrganizationLevel);
            }
            catch { }
            return entOrganizationLevelreturned;
        }
        /// <summary>
        /// Function adds the Org units after checking if already exists
        /// </summary>
        /// <param name="pstrClientId"></param>
        /// <param name="pstrUnitName"></param>
        /// <param name="pstrUnitParentId"></param>
        /// <param name="pstrOrgTreeParentUnitId"></param>
        /// <param name="pstrLevelId"></param>
        /// <returns>OrganizationLevelUnit </returns>
        public static OrganizationLevelUnit AddUnit(ref List<BaseEntity> pentListDBAllUnitBase, string pstrClientId, string pstrUnitName, string pstrUnitParentId, int pstrOrgTreeParentUnitId, string pstrLevelId, bool bAddNewUnit)
        {
            OrganizationLevelUnit entOrganizationLevelUnit = new OrganizationLevelUnit();
            OrganizationLevelUnit orgAllUnits = new OrganizationLevelUnit();
            OrganizationLevelUnit objUnit = new OrganizationLevelUnit();
            OrganizationLevelUnitDAM _organizationLevelUnitDAM = new OrganizationLevelUnitDAM();
            //List<BaseEntity> entListUnitBase = new List<BaseEntity>();

            try
            {
                orgAllUnits.ClientId = pstrClientId;
                objUnit.ClientId = pstrClientId;
                //entListUnitBase = entManager.Execute(orgAllUnits, (BaseEntity.ListMethod)OrganizationLevelUnit.ListMethod.GetAll);
                //Check If already exists start
                OrganizationLevelUnit orgDBExistingUnitobj = new OrganizationLevelUnit();
                orgDBExistingUnitobj = (OrganizationLevelUnit)pentListDBAllUnitBase.Find(delegate (BaseEntity entBase)
                { return (pstrUnitParentId == ((OrganizationLevelUnit)entBase).ParentUnitId && ((OrganizationLevelUnit)entBase).UnitName.ToLower().Trim() == pstrUnitName.ToLower().Trim()); });
                if (orgDBExistingUnitobj != null) return orgDBExistingUnitobj;
                //Check If already exists end
                if (!bAddNewUnit)
                {
                    return orgDBExistingUnitobj;
                }
                /** Commented By Ashish **/
                /*
                //Get New sequneceorder start
                OrganizationLevel entOrganizationLevel = new OrganizationLevel();
                entOrganizationLevel.ID = pstrLevelId;
                entOrganizationLevel.ClientId = pstrClientId;
                entOrganizationLevel = (OrganizationLevel)entManager.Execute(entOrganizationLevel, (BaseEntity.Method)OrganizationLevel.Method.Get);
                //get new sequenceorder end

                entOrganizationLevelUnit.ParentUnitId = pstrUnitParentId;
                entOrganizationLevelUnit.SequenceOrder = entOrganizationLevel.OrganizationUnits.Count + 1;
                entOrganizationLevelUnit.LevelId = pstrLevelId;
                entOrganizationLevelUnit.OrgTreeParentUnitId = pstrOrgTreeParentUnitId;
                entOrganizationLevelUnit.CreatedById = pstrClientId;
                entOrganizationLevelUnit.ClientId = pstrClientId;
                entOrganizationLevelUnit.OrgTreeUnitId = 0;
                entOrganizationLevelUnit.UnitName = pstrUnitName;
                entOrganizationLevelUnit.LastModifiedById = pstrClientId;
                entOrganizationLevelUnit.LastModifiedDate = DateTime.Now;
                entOrganizationLevelUnit = (OrganizationLevelUnit)entManager.Execute(entOrganizationLevelUnit, (BaseEntity.Method)OrganizationLevelUnit.Method.Add);
                pentListDBAllUnitBase = _entManager.Execute(objUnit, (BaseEntity.ListMethod)OrganizationLevelUnit.ListMethod.GetAllUnitsForImport);//OrganizationLevelUnit.ListMethod.GetAll);
                 */
                entOrganizationLevelUnit.ParentUnitId = pstrUnitParentId;
                entOrganizationLevelUnit.SequenceOrder = 0;
                entOrganizationLevelUnit.LevelId = pstrLevelId;
                entOrganizationLevelUnit.OrgTreeParentUnitId = pstrOrgTreeParentUnitId;
                entOrganizationLevelUnit.CreatedById = pstrClientId;
                entOrganizationLevelUnit.ClientId = pstrClientId;
                entOrganizationLevelUnit.OrgTreeUnitId = 0;
                entOrganizationLevelUnit.UnitName = pstrUnitName;
                entOrganizationLevelUnit.LastModifiedById = pstrClientId;
                entOrganizationLevelUnit.LastModifiedDate = DateTime.Now;
                entOrganizationLevelUnit = _organizationLevelUnitDAM.CheckandAddOrganizationLevelUnit(entOrganizationLevelUnit);
                //Add New Entry to Existing list
                pentListDBAllUnitBase.Add(entOrganizationLevelUnit);
            }
            catch { }
            return entOrganizationLevelUnit;
        }
        /// <summary>
        /// Create and return custom fields and their values table.
        /// </summary>
        /// <param name="sClientId"></param>
        /// <returns></returns>
        //public static DataTable GetCustomFields(string sClientId)
        //{
        //    DataTable _dtableCustomField = new DataTable();
        //    try
        //    {
        //        CustomField objCF = new CustomField();
        //        objCF.ClientId = sClientId;
        //        objCF.DefaultLanguageId = "en-US";
        //        List<BaseEntity> objBaseList = _entManager.Execute(objCF, (BaseEntity.ListMethod)CustomField.ListMethod.GetCustomFieldDtls);
        //        int CFCnt = objBaseList.Count;
        //        if (CFCnt >= 0)
        //        {
        //            DataColumn dcCFSrNo = new DataColumn("SrNo", Type.GetType("System.Int32"));
        //            _dtableCustomField.Columns.Add(dcCFSrNo);
        //            DataColumn dcCFId = new DataColumn("CustomFieldId", Type.GetType("System.String"));
        //            _dtableCustomField.Columns.Add(dcCFId);
        //            DataColumn dcCFName = new DataColumn("CustomFieldDisplayText", Type.GetType("System.String"));
        //            _dtableCustomField.Columns.Add(dcCFName);
        //            DataColumn dcCFTypeName = new DataColumn("TypeName", Type.GetType("System.String"));
        //            _dtableCustomField.Columns.Add(dcCFTypeName);
        //            DataColumn dcSortOrder = new DataColumn("SortOrder", Type.GetType("System.Int32"));
        //            _dtableCustomField.Columns.Add(dcSortOrder);
        //            DataColumn dcIsMandatory = new DataColumn("IsMandatory", Type.GetType("System.Boolean"));
        //            _dtableCustomField.Columns.Add(dcIsMandatory);
        //            DataColumn dcIsActive = new DataColumn("IsActive", Type.GetType("System.Boolean"));
        //            _dtableCustomField.Columns.Add(dcIsActive);
        //            DataColumn dcCFValuesId = new DataColumn("CFValuesId", Type.GetType("System.String"));
        //            _dtableCustomField.Columns.Add(dcCFValuesId);
        //            DataColumn dcCFValues = new DataColumn("CFValues", Type.GetType("System.String"));
        //            _dtableCustomField.Columns.Add(dcCFValues);
        //            DataColumn dcCFTypeId = new DataColumn("CFTypeId", Type.GetType("System.String"));
        //            _dtableCustomField.Columns.Add(dcCFTypeId);
        //            DataColumn dcCFValueType = new DataColumn("CFValueType", Type.GetType("System.String"));
        //            _dtableCustomField.Columns.Add(dcCFValueType);

        //            int i = 1;
        //            foreach (BaseEntity cfItem in objBaseList)
        //            {
        //                CustomField objAddCFRow = (CustomField)cfItem;
        //                string strCFName = objAddCFRow.CustomFieldLanguages[0].CustomFieldDisplayText;
        //                DataRow drCF = _dtableCustomField.NewRow();
        //                drCF["SrNo"] = i;
        //                drCF["CustomFieldId"] = objAddCFRow.ID;
        //                drCF["CustomFieldDisplayText"] = strCFName;
        //                drCF["TypeName"] = objAddCFRow.TypeName;
        //                drCF["SortOrder"] = objAddCFRow.SortOrder;
        //                drCF["IsMandatory"] = objAddCFRow.IsMandatory;
        //                drCF["IsActive"] = objAddCFRow.IsActive;
        //                string CFIValues = string.Empty;
        //                string CFIValuesID = string.Empty;
        //                foreach (CustomFieldItem CFI in objAddCFRow.CustomFieldItems)
        //                {
        //                    if (string.IsNullOrEmpty(CFIValues) || string.IsNullOrEmpty(CFIValuesID))
        //                    {
        //                        CFIValuesID = CFI.ID;
        //                        CFIValues = CFI.CustomFieldItemLanguages[0].CustomFieldItemDisplayText;
        //                    }
        //                    else
        //                    {
        //                        CFIValuesID = CFIValuesID + "|" + CFI.ID;
        //                        CFIValues = CFIValues + "|" + CFI.CustomFieldItemLanguages[0].CustomFieldItemDisplayText;
        //                    }
        //                }
        //                drCF["CFValuesId"] = CFIValuesID;
        //                drCF["CFValues"] = CFIValues;
        //                drCF["CFTypeId"] = objAddCFRow.CustomFieldTypeId;
        //                drCF["CFValueType"] = objAddCFRow.FieldValueType;
        //                _dtableCustomField.Rows.Add(drCF);
        //                i++;
        //            }
        //        }
        //    }
        //    catch { }
        //    return _dtableCustomField;
        //}

        /// <summary>
        /// Add import history and return it's Id.
        /// </summary>
        /// <param name="pstrClientId"></param>
        /// <param name="pstrUserId"></param>
        /// <param name="pstrFileName"></param>
        /// <param name="pstrFilePath"></param>
        /// <param name="pImpStatus"></param>
        /// <param name="pImpAction"></param>
        /// <param name="pstrImpLog"></param>
        /// <param name="pImpType"></param>
        /// <returns></returns>
        public static string AddImportHistory(string pstrClientId, string pstrUserId, string pstrFileName, string pstrFilePath, ImportStatus pImpStatus, ImportAction pImpAction, string pstrImpLog, ImportType pImpType)
        {
            string strImpHisId = string.Empty;
            ImportHistoryAdaptor _importHistoryAdaptor = new ImportHistoryAdaptor();
            try
            {
                ImportHistory entImpHis = new ImportHistory();
                entImpHis.ClientId = pstrClientId;
                entImpHis.CreatedById = pstrUserId;
                entImpHis.DateCreated = DateTime.Now;
                entImpHis.FileName = pstrFileName;
                entImpHis.FilePath = pstrFilePath;
                entImpHis.ImportStatus = pImpStatus;
                entImpHis.ImportAction = pImpAction;
                entImpHis.ImportLog = pstrImpLog;
                entImpHis.ImportStatus = pImpStatus;
                entImpHis.ImportType = pImpType;
                entImpHis.LastModifiedById = pstrUserId;
                entImpHis.LastModifiedDate = DateTime.Now;
                entImpHis = (ImportHistory)_importHistoryAdaptor.AddImportHistory(entImpHis);
                strImpHisId = entImpHis.ID;
            }
            catch { }
            return strImpHisId;
        }
        /// <summary>
        /// Get and return import definition fields as per import action.
        /// </summary>
        /// <param name="pstrClientId"></param>
        /// <param name="pImpAction"></param>
        /// <returns></returns>
        //public static DataSet GetClientFields(string pstrClientId, ImportAction pImpAction)
        //{
        //    DataSet dsImpDef = new DataSet();
        //    ImportHistoryAdaptor _importHistoryAdaptor = new ImportHistoryAdaptor();
        //    try
        //    {
        //        ImportDefination entImpDef = new ImportDefination();
        //        entImpDef.ClientId = pstrClientId;
        //        entImpDef.ImportAction = pImpAction;
        //        dsImpDef = _entManager.ExecuteDataSet(entImpDef, (BaseEntity.ListMethod)ImportDefination.ListMethod.GetAll);

        //        //Added By Manoj - Issue No # 126
        //        if (dsImpDef != null)
        //        {
        //            if (dsImpDef.Tables.Count > 0 && dsImpDef.Tables[0].Rows.Count > 0)
        //            {

        //                foreach (DataRow Dr in dsImpDef.Tables[0].Rows)
        //                {

        //                    if (Dr["FieldName"].ToString() == "DateOfRegistration")
        //                        Dr["FieldName"] = "HireDate";

        //                    ////if (Convert.ToBoolean(Dr["include"]) == false)
        //                    ////{
        //                    ////    Dr.Delete();
        //                    ////}
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex) { }
        //    return dsImpDef;
        //}
        /// <summary>
        /// Get and return lookup data as per lookup type.
        /// </summary>
        /// <param name="pstrClientId"></param>
        /// <param name="pLookUpType"></param>
        /// <returns></returns>
        public static DataSet GetLookUp(string pstrClientId, LookupType pLookUpType)
        {
            DataSet dsLookUp = new DataSet();
            LookUpManager _lookUpManager = new LookUpManager();
            try
            {
                Lookup entLookUp = new Lookup();
                entLookUp.ClientId = pstrClientId;
                entLookUp.LookupType = pLookUpType;
                dsLookUp = _lookUpManager.ExecuteDataSet(entLookUp, Lookup.ListMethod.GetAllByLookupType);
            }
            catch { }
            return dsLookUp;
        }
        /// <summary>
        /// Format and return provided sting.
        /// </summary>
        /// <param name="pstrText"></param>
        /// <param name="strFieldType"></param>
        /// <returns></returns>
        public static string FromatDisplaytext(string pstrText, string strFieldType)
        {
            string strDisplayText = string.Empty;
            try
            {
                if (strFieldType == "Standard" || strFieldType == "StandardCustom")
                {
                    if (pstrText != "UserNameAlias")
                    {
                        pstrText = pstrText.Replace("ID", string.Empty);
                        pstrText = pstrText.Replace("Id", string.Empty);
                        pstrText = pstrText.Replace("iD", string.Empty);
                        foreach (char chrFT in pstrText)
                        {
                            if (chrFT.ToString() == chrFT.ToString().ToUpper())
                            {
                                strDisplayText = strDisplayText + " " + chrFT.ToString();
                            }
                            else
                            {
                                strDisplayText = strDisplayText + chrFT.ToString();
                            }
                        }
                    }
                    else
                    {
                        strDisplayText = "Login ID";
                    }
                }
                else if (strFieldType == "Title")
                {
                    switch (pstrText)
                    {
                        //case "Standard":
                        //    strDisplayText = "Standard Fields";
                        //    break;
                        //case "CustomField":
                        //    strDisplayText = "Custom Fields";
                        //    break;
                        //case "OrgTreeLevels":
                        //    strDisplayText = "Organization Levels";
                        //    break;

                        case "Standard":
                            strDisplayText = "<h5>Standard Fields</h5>";
                            break;
                        case "CustomField":
                            strDisplayText = "<h5>Custom Fields</h5>";
                            break;
                        case "OrgTreeLevels":
                            strDisplayText = "<h5>Organization Levels</h5>";
                            break;
                    }
                }
                else
                {
                    strDisplayText = pstrText;
                }
            }
            catch { }
            return strDisplayText.Trim();
        }
        /// <summary>
        /// Create CSV file and retrun path.
        /// </summary>
        /// <param name="pstrClientId"></param>
        /// <param name="pdtblImpDef"></param>
        /// <param name="pImpAction"></param>
        /// <param name="pbtye"></param>
        /// <returns></returns>
        public static string CreateCSVFile(string pstrClientId, DataTable pdtblImpDef, ImportAction pImpAction, out byte[] pbtye)
        {
            string strCSVPath = string.Empty;
            pbtye = null;
            try
            {

                StringBuilder sbCSV = new StringBuilder();
                int iRowCount = 0;
                foreach (DataRow dr in pdtblImpDef.Rows)
                {
                    if (Convert.ToBoolean(dr["Include"]) || Convert.ToBoolean(dr["IsMandatory"]))
                    {
                        if (pImpAction == ImportAction.ChangeId && iRowCount == 0)
                        {
                            sbCSV.Append("Current " + FromatDisplaytext(dr["FieldName"].ToString(), dr["FieldTypes"].ToString()));
                            sbCSV.Append(",");
                            sbCSV.Append("New " + FromatDisplaytext(dr["FieldName"].ToString(), dr["FieldTypes"].ToString()));
                        }
                        else
                        {
                            string strCSVCol = FromatDisplaytext(dr["FieldName"].ToString(), dr["FieldTypes"].ToString());
                            if (sbCSV.ToString().IndexOf(strCSVCol) != -1)
                            {
                                sbCSV.Append(strCSVCol + iRowCount.ToString());
                            }
                            else
                            {
                                sbCSV.Append(strCSVCol);
                            }
                        }
                        if (iRowCount < pdtblImpDef.Rows.Count)
                        {
                            sbCSV.Append(",");
                        }
                    }
                    iRowCount++;
                }
                byte[] byteArray = Encoding.ASCII.GetBytes(sbCSV.ToString());
                pbtye = byteArray;
                FileHandler FtpUpload = new FileHandler(pstrClientId);
                string strFileName = "SampleUserImport.csv";
                if (!FtpUpload.IsFolderExist(FileHandler.CSV_FOLDER_PATH, pstrClientId))
                {
                    FtpUpload.CreateFolder(FileHandler.CSV_FOLDER_PATH, pstrClientId);
                }
                strCSVPath = FtpUpload.Uploadfile(FileHandler.CSV_FOLDER_PATH + "/" + pstrClientId, strFileName, byteArray);
            }
            catch { }
            return strCSVPath;
        }

        /// <summary>
        /// Fills the approved email templates.
        /// </summary>
        /// <param name="pstrClientId"></param>
        /// <param name="pDDLEmailTemplates"></param>
        //public static void GetDDLEmailTemplates(string pstrClientId, DropDownList pDDLEmailTemplates)
        //{
        //    try
        //    {
        //        EmailTemplate _entEmailTemplate = new EmailTemplate();
        //        _entEmailTemplate.ClientId = pstrClientId;
        //        _entEmailTemplate.ApprovalStatus = EmailTemplateLanguage.EmailApprovalStatus.Approved;
        //        EntityRange entRange = new EntityRange();
        //        entRange.RequestedById = CommonMethods.GetRequestedById();
        //        _entEmailTemplate.ListRange = entRange;
        //        List<BaseEntity> _entEmailTemplateList = _entManager.Execute(_entEmailTemplate, (BaseEntity.ListMethod)EmailTemplate.ListMethod.GetAll);
        //        pDDLEmailTemplates.Items.Add("Select Template");
        //        foreach (EmailTemplate entEmailTemp in _entEmailTemplateList)
        //        {
        //            ListItem liEmail = new ListItem();
        //            liEmail.Text = entEmailTemp.EmailTemplateDefaultTitle;
        //            liEmail.Value = entEmailTemp.ID;
        //            pDDLEmailTemplates.Items.Add(liEmail);
        //        }
        //    }
        //    catch { }
        //}

        /// <summary>
        /// Validate custom field exist or not.
        /// </summary>
        /// <param name="pstrCustomFieldName"></param>
        /// <param name="pdtblCustomField"></param>
        /// <returns></returns>
        public static bool ValidateCustomField(string pstrCustomFieldName, DataTable pdtblCustomField)
        {
            bool bValidateCF = false;
            try
            {
                if (pdtblCustomField != null && pdtblCustomField.Rows.Count > 0)
                {
                    DataRow[] drCF = pdtblCustomField.Select("CustomFieldDisplayText ='" + pstrCustomFieldName + "'");
                    if (drCF.Length > 0)
                    {
                        bValidateCF = false;
                    }
                    else
                    {
                        bValidateCF = true;
                    }
                }
                else
                {
                    bValidateCF = true;
                }
            }
            catch { }
            return bValidateCF;
        }

        /// <summary>
        /// Get the password policy configuration.
        /// </summary>
        /// <param name="pClientId"></param>
        /// <returns></returns>
        public static PasswordPolicyConfiguration GetPasswordPolicyConfiguration(string pClientId)
        {
            try
            {
                PasswordPolicyAdaptor _adaptorPwdPolicyConfig = new PasswordPolicyAdaptor();
                PasswordPolicyConfiguration entPasswordPolicyConfiguration = new PasswordPolicyConfiguration();
                entPasswordPolicyConfiguration.ClientId = pClientId;
                entPasswordPolicyConfiguration = (PasswordPolicyConfiguration)_adaptorPwdPolicyConfig.GetPasswordPolicyById(entPasswordPolicyConfiguration);
                return entPasswordPolicyConfiguration;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Validate password as per password policy.
        /// </summary>
        /// <param name="pentPasswordPolicy"></param>
        /// <param name="pstrClientId"></param>
        /// <param name="pstrUserId"></param>
        /// <param name="pstrPassword"></param>
        /// <param name="pstrLang"></param>
        /// <returns></returns>
        public static string ValidatePasswordPolicy(PasswordPolicyConfiguration pentPasswordPolicy, string pstrClientId, string pstrUserId, string pstrPassword, string pstrLang)
        {
            string bValidate = string.Empty;
            string strExpre;
            bool bSpecialCharater = false;
            string escChars = "[!@#$%^&*?_~+:`()]";
            Regex re = null;
            Match me = null;
            try
            {
                if ((pentPasswordPolicy != null) && (!string.IsNullOrEmpty(pentPasswordPolicy.ID)))
                {
                    if (pstrPassword.Length > pentPasswordPolicy.MaxPaswordLength)
                    {
                        bValidate = MessageAdaptor.GetMessage(YPLMS.Services.Messages.PWDPolicyConfiguration.PWD_MAX_LENGHT, pstrLang) + " " + Convert.ToString(pentPasswordPolicy.MaxPaswordLength);
                        bValidate = bValidate + "<br />" + MessageAdaptor.GetMessage(YPLMS.Services.Messages.PWDPolicyConfiguration.PWD_MIN_LENGHT, pstrLang) + " " + Convert.ToString(pentPasswordPolicy.MinPaswordLength);
                    }
                    if (pstrPassword.Length < pentPasswordPolicy.MinPaswordLength)
                    {
                        bValidate = MessageAdaptor.GetMessage(YPLMS.Services.Messages.PWDPolicyConfiguration.PWD_MAX_LENGHT, pstrLang) + " " + Convert.ToString(pentPasswordPolicy.MaxPaswordLength);
                        bValidate = bValidate + "<br />" + MessageAdaptor.GetMessage(YPLMS.Services.Messages.PWDPolicyConfiguration.PWD_MIN_LENGHT, pstrLang) + " " + Convert.ToString(pentPasswordPolicy.MinPaswordLength);
                    }
                    if (pentPasswordPolicy.IsUpperCase)
                    {
                        strExpre = @"([A-Z])";
                        re = new Regex(strExpre, RegexOptions.ExplicitCapture);
                        me = re.Match(pstrPassword);
                        if (!me.Success)
                        {
                            bValidate = bValidate + "<br />" + MessageAdaptor.GetMessage(YPLMS.Services.Messages.PWDPolicyConfiguration.PWD_MUST_CONT_UCASE_CHAR, pstrLang);
                        }
                    }
                    if (pentPasswordPolicy.IsLowerCase)
                    {
                        strExpre = @"([a-z])";
                        re = new Regex(strExpre, RegexOptions.ExplicitCapture);
                        me = re.Match(pstrPassword);
                        if (!me.Success)
                        {
                            bValidate = bValidate + "<br />" + MessageAdaptor.GetMessage(YPLMS.Services.Messages.PWDPolicyConfiguration.PWD_MUST_CONT_LCASE_CHAR, pstrLang);
                        }
                    }
                    if (pentPasswordPolicy.IsSpecialCaracter)
                    {

                        for (int i = 0; i < escChars.Length; i++)
                        {
                            if (pstrPassword.IndexOf(escChars[i]) != -1)
                            {
                                bSpecialCharater = true;
                                break;
                            }
                        }
                        if (!bSpecialCharater)
                        {
                            bValidate = bValidate + "<br />" + MessageAdaptor.GetMessage(YPLMS.Services.Messages.PWDPolicyConfiguration.PWD_MUST_CONT_SPECIAL_CHAR, pstrLang);
                        }
                    }
                    if (pentPasswordPolicy.IsNumber)
                    {
                        strExpre = @"([0-9])";
                        re = new Regex(strExpre, RegexOptions.ExplicitCapture);
                        me = re.Match(pstrPassword);
                        if (!me.Success)
                        {
                            bValidate = bValidate + "<br />" + MessageAdaptor.GetMessage(YPLMS.Services.Messages.PWDPolicyConfiguration.PWD_MUST_CONT_NUMBER, pstrLang);
                        }
                    }
                    if (pstrPassword.IndexOf(Convert.ToChar(" ")) != -1)
                    {
                        bValidate = bValidate + "<br />Space is not allowed in password.";
                    }
                    try
                    {
                        LearnerDAM _adaptorLearner = new LearnerDAM();
                        if (!string.IsNullOrEmpty(pstrUserId))
                        {
                            Learner entLearnerPH = new Learner();
                            entLearnerPH.ClientId = pstrClientId;
                            entLearnerPH.ID = pstrUserId;
                            entLearnerPH.UserPassword = pstrPassword;
                            entLearnerPH = (Learner)_adaptorLearner.ChecknewPwd(entLearnerPH);

                            if (entLearnerPH.ID != null && !entLearnerPH.CanUpdate)
                            {
                                //Changed By Manoj - Issues No # 99
                                bValidate = bValidate + "<br />Has been used in the previous passwords.";
                            }
                        }
                    }
                    catch (CustomException exPH)
                    {
                        bValidate = bValidate + "<br />" + exPH.Message;
                    }
                }
            }
            catch (CustomException ex)
            {
                bValidate = bValidate + "<br />" + pstrLang;
            }
            return bValidate;
        }
        /// <summary>
        /// Get and return the message as per Id and language.
        /// </summary>
        /// <param name="pstrTempMsg"></param>
        /// <param name="pstrMsgId"></param>
        /// <param name="pstrLngId"></param>
        /// <returns></returns>
        public static string GetMessage(string pstrTempMsg, string pstrMsgId, string pstrLngId)
        {
            string strMsg = pstrTempMsg;
            try
            {
                if (!string.IsNullOrEmpty(pstrMsgId))
                {
                    if (string.IsNullOrEmpty(pstrLngId))
                    {
                        strMsg = MessageAdaptor.GetMessage(pstrMsgId);
                    }
                    else
                    {
                        strMsg = MessageAdaptor.GetMessage(pstrMsgId, pstrLngId);
                    }
                }
            }
            catch { }
            return strMsg;
        }
        /// <summary>
        /// Check Extension of file to validate as CSV file.
        /// </summary>
        /// <param name="pstrFilePath"></param>
        /// <returns></returns>
        public static bool CheckIsCSVFileExt(string pstrFilePath)
        {
            int iLstIndOfDot = pstrFilePath.LastIndexOf(Convert.ToChar("."));
            if (pstrFilePath.Substring(iLstIndOfDot).ToLower() == ".csv")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Create dummy file of given file name with duplicate second line from first line.
        /// </summary>
        /// <param name="pstrFilePath"></param>
        /// <returns></returns>
        public static string CreateDummyFile(string pstrFilePath)
        {
            try
            {
                string[] strAllLines = File.ReadAllLines(pstrFilePath);
                int iLstindOfDot = pstrFilePath.LastIndexOf(Convert.ToChar("."));
                string strNewFile = pstrFilePath.Substring(0, iLstindOfDot) + "_N" + pstrFilePath.Substring(iLstindOfDot);
                using (StreamWriter file = new StreamWriter(strNewFile))
                {
                    if (strAllLines != null && strAllLines.Length > 0)
                    {
                        file.WriteLine(strAllLines[0]);
                        foreach (string strLine in strAllLines)
                        {
                            if (strLine != string.Empty)
                            {
                                file.WriteLine(strLine);
                            }
                        }
                    }
                }
                return strNewFile;
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Get hierarchy.
        /// </summary>
        /// <param name="pOrgUnit"></param>
        /// <returns></returns>
        public static string GetHierarchy(OrganizationLevelUnit pOrgUnit)
        {
            OrganizationLevelUnitDAM _organizationLevelUnitDAM = new OrganizationLevelUnitDAM();
            List<OrganizationLevelUnit> lstNewUnits = new List<OrganizationLevelUnit>();
            List<OrganizationLevelUnit> levelBaseList = new List<OrganizationLevelUnit>();
            OrganizationLevelUnit entUnit = new OrganizationLevelUnit();
            entUnit.ClientId = pOrgUnit.ClientId;
            entUnit.ID = pOrgUnit.ID;
            string strNodeChain = string.Empty;
            if (!string.IsNullOrEmpty(entUnit.ID))
            {
                levelBaseList = _organizationLevelUnitDAM.GetAllUnits(entUnit);
                bool blLoopTerminator = true;
                if (levelBaseList != null)
                {
                    while (blLoopTerminator)
                    {
                        OrganizationLevelUnit entLevelUnit = new OrganizationLevelUnit();
                        foreach (BaseEntity entBase in levelBaseList)
                        {
                            entLevelUnit = new OrganizationLevelUnit();
                            entLevelUnit = (OrganizationLevelUnit)entBase;
                            if (entLevelUnit.ID == entUnit.ID)
                            {
                                lstNewUnits.Add((OrganizationLevelUnit)entBase);
                                entUnit.ID = entLevelUnit.ParentUnitId;
                                break;
                            }
                        }
                        if (entLevelUnit != null && entLevelUnit.ParentUnitId == "0") break;
                        if (entUnit.ID == pOrgUnit.ID) break;
                    }
                }
                int iCnt = 0;
                for (iCnt = lstNewUnits.Count - 1; iCnt >= 0; iCnt--)
                {
                    if (iCnt != lstNewUnits.Count - 1)
                    {
                        if (iCnt != 0)
                            strNodeChain += lstNewUnits[iCnt].UnitName + " > ";
                        else
                            strNodeChain += lstNewUnits[iCnt].UnitName;
                    }
                }
            }
            return strNodeChain;
        }

       
    }
}
