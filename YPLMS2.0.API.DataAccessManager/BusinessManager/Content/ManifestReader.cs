using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    public class ManifestReader
    {
        string strClientID = string.Empty;
        string strOrgContext = "//organizations/organization";
        string strNmSpaceURI = "http://www.adlnet.org/xsd/adlcp_rootv1p2";
        string strNmSpacePrefix = "adlcp";

        /// <summary>
        /// Common class for Manifest related functions
        /// </summary>
        public ManifestReader(string pstrClientId)
        {
            strClientID = pstrClientId;
        }

        /// <summary>
        /// Read course manifest and add data to Table
        /// </summary>
        /// <param name="pstrCourseId">Course Id</param>
        /// <param name="pdtblMetaData">Table</param>
        /// <returns></returns>
        public DataTable ReadCourseManifest(string pstrCoursePath, string pCourseId)
        {
            DataTable dtblMetaData = null;
            string strClientId = EncryptionManager.Decrypt("P+jUwjxdIhc="); //EncryptionManager.Decrypt(ConfigurationManager.AppSettings[Client.BASE_CLIENT_KEY]);
            FileHandler fileHandler = new FileHandler(strClientId);
            string strManifestPath = fileHandler.RootSharedPath.Replace("\\\\", "\\");
            pstrCoursePath = pstrCoursePath.Replace("/", @"\");
            strManifestPath += pstrCoursePath;
            string strMetaDataXml = strManifestPath;
            strManifestPath += @"\imsmanifest.xml";
            XmlDocument xDocMF = new XmlDocument();
            XMLLib xmlLib = new XMLLib();
            XmlDocument xDocMD = new XmlDocument();
            //strManifestPath = strManifestPath.Replace("\\", "/");
            if (File.Exists(strManifestPath) && xmlLib.fOpenFreeXMLDoc(ref xDocMF, strManifestPath))
            {
                try
                {
                    foreach (XmlNode xNodeInMF in xDocMF.DocumentElement.ChildNodes)

                    {
                        if (xNodeInMF.Name.ToLower() == "metadata")
                        {
                            foreach (XmlNode xNodeInMD in xNodeInMF.ChildNodes)
                            {
                                if (xNodeInMD.Name.ToLower() == "adlcp:location")
                                {
                                    strMetaDataXml += "\\" + xNodeInMD.InnerText;
                                    if (File.Exists(strMetaDataXml) && xmlLib.fOpenFreeXMLDoc(ref xDocMD, strMetaDataXml))
                                    {
                                        foreach (XmlNode xNodeInLocation in xDocMD.DocumentElement.ChildNodes)
                                        {
                                            dtblMetaData = ReadMetaData(xNodeInLocation, pCourseId);
                                            break;
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        throw new CustomException(YPLMS.Services.Messages.Common.METADATA_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, null, false);
                                    }
                                }
                                else if (xNodeInMD.Name.ToLower() == "lom")
                                {
                                    foreach (XmlNode xNodeInLocation in xNodeInMD.ChildNodes)
                                    {
                                        dtblMetaData = ReadMetaData(xNodeInLocation, pCourseId);
                                        break;
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    var mgr = new XmlNamespaceManager(xDocMF.NameTable);
                    mgr.AddNamespace("adlcp", "http://www.adlnet.org/xsd/adlcp_rootv1p2");
                    XmlNodeList nodes = xDocMF.SelectNodes("//adlcp:masteryscore", mgr);
                    foreach (XmlNode node in nodes)
                    {
                        Common.CourseMasteryScore = node.InnerText;
                    }
                }
                catch (Exception expXml)
                {
                    throw new CustomException(YPLMS.Services.Messages.Common.MANIFEST_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expXml, true);
                }
            }
            else
            {
                throw new CustomException(YPLMS.Services.Messages.Common.MANIFEST_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, null, false);
            }
            return dtblMetaData;
        }
        public DataTable ReadCourseManifestForScorm2004(string pstrCoursePath, string pCourseId)
        {
            DataTable dtblMetaData = null;
            string strClientId = EncryptionManager.Decrypt("P+jUwjxdIhc=");// EncryptionManager.Decrypt(ConfigurationManager.AppSettings[Client.BASE_CLIENT_KEY]);
            FileHandler fileHandler = new FileHandler(strClientId);
            string strManifestPath = fileHandler.RootSharedPath.Replace("\\\\", "\\");
            pstrCoursePath = pstrCoursePath.Replace("/", @"\");
            strManifestPath += pstrCoursePath;
            string strMetaDataXml = strManifestPath;
            strManifestPath += @"\imsmanifest.xml";
            XmlDocument xDocMF = new XmlDocument();
            XMLLib xmlLib = new XMLLib();
            XmlDocument xDocMD = new XmlDocument();
            //strManifestPath = strManifestPath.Replace("\\", "/");
            if (File.Exists(strManifestPath) && xmlLib.fOpenFreeXMLDoc(ref xDocMF, strManifestPath))
            {
                try
                {
                    foreach (XmlNode xNodeInMF in xDocMF.DocumentElement.ChildNodes)
                    {
                        if (xNodeInMF.Name.ToLower() == "metadata")
                        {
                            foreach (XmlNode xNodeInMD in xNodeInMF.ChildNodes)
                            {
                                if (xNodeInMD.Name.ToLower() == "adlcp:location")
                                {
                                    strMetaDataXml += "\\" + xNodeInMD.InnerText;
                                    if (File.Exists(strMetaDataXml) && xmlLib.fOpenFreeXMLDoc(ref xDocMD, strMetaDataXml))
                                    {
                                        foreach (XmlNode xNodeInLocation in xDocMD.DocumentElement.ChildNodes)
                                        {
                                            dtblMetaData = ReadMetaData(xNodeInLocation, pCourseId);
                                            break;
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        throw new CustomException(YPLMS.Services.Messages.Common.METADATA_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, null, false);
                                    }
                                }
                                else if (xNodeInMD.Name.ToLower() == "lom")
                                {
                                    foreach (XmlNode xNodeInLocation in xNodeInMD.ChildNodes)
                                    {
                                        dtblMetaData = ReadMetaData(xNodeInLocation, pCourseId);
                                        break;
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                catch (Exception expXml)
                {
                    throw new CustomException(YPLMS.Services.Messages.Common.MANIFEST_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expXml, true);
                }
                //get the Questions js files 
                string newPath = fileHandler.RootSharedPath + pstrCoursePath;

                string[] QuestionFilesPath = Directory.GetFiles(@newPath, "questions.js", SearchOption.AllDirectories);

                //HttpContext.Current.Session.Add("QuestionFilesPath", QuestionFilesPath);

                var mgr = new XmlNamespaceManager(xDocMF.NameTable);
                mgr.AddNamespace("a", "http://www.imsglobal.org/xsd/imsss");
                XmlNodeList nodes = xDocMF.SelectNodes("//a:minNormalizedMeasure", mgr);
                foreach (XmlNode node in nodes)
                {
                    decimal d;
                    if (!decimal.TryParse(node.InnerText, out d))
                        Common.CourseMasteryScore = "";
                    else
                        Common.CourseMasteryScore = Convert.ToString(d * 100);
                }
            }
            else
            {
                throw new CustomException(YPLMS.Services.Messages.Common.MANIFEST_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, null, false);
            }
            return dtblMetaData;
        }
        /// <summary>
        /// Read Meta Data
        /// </summary>
        /// <param name="pxNodeInMD"></param>
        /// <param name="pstrContModId"></param>
        /// <returns></returns>
        private DataTable ReadMetaData(XmlNode pxNodeInMD, string pstrContModId)
        {
            LanguageManager mgrLanguage = new LanguageManager();
            Language entLanguage = new Language();
            entLanguage.ClientId = strClientID;
            DataSet dsetClientLanguages = mgrLanguage.ExecuteDataSet(entLanguage, Language.ListMethod.GetClientList);
            DataTable dtblMetaData = new DataTable();
            DataColumn dcolContentModuleId = new DataColumn("ContentModuleId", Type.GetType("System.String"));
            dtblMetaData.Columns.Add(dcolContentModuleId);
            DataColumn dcolLanguageId = new DataColumn("LanguageId", Type.GetType("System.String"));
            dtblMetaData.Columns.Add(dcolLanguageId);
            DataColumn dcolTitle = new DataColumn("ContentModuleName", Type.GetType("System.String"));
            dtblMetaData.Columns.Add(dcolTitle);
            DataColumn dcolDescription = new DataColumn("ContentModuleDescription", Type.GetType("System.String"));
            dtblMetaData.Columns.Add(dcolDescription);
            DataColumn dcolKeyWords = new DataColumn("ContentModuleKeyWords", Type.GetType("System.String"));
            dtblMetaData.Columns.Add(dcolKeyWords);

            if (pxNodeInMD.Name.ToLower() == "general")
            {
                foreach (XmlNode xNodeInGeneral in pxNodeInMD.ChildNodes)
                {
                    if (xNodeInGeneral.Name.ToLower() == "title")
                    {
                        foreach (XmlNode xNodeInTitle in xNodeInGeneral.ChildNodes)
                        {
                            if (xNodeInTitle.Attributes.Count > 0 && xNodeInTitle.Attributes[0].Name.ToLower() == "xml:lang")
                            {
                                string strLang = xNodeInTitle.Attributes["xml:lang"].Value;
                                if (strLang != string.Empty)
                                {
                                    if (dsetClientLanguages != null && dsetClientLanguages.Tables.Count > 0)
                                    {
                                        if (dsetClientLanguages.Tables["Language"].Rows.Count > 0)
                                        {
                                            DataRow[] drowClientLang = dsetClientLanguages.Tables["Language"].Select("ID='" + strLang + "'");
                                            if (drowClientLang.Length == 1)
                                            {
                                                DataRow drowMetaData = dtblMetaData.NewRow();
                                                drowMetaData["ContentModuleId"] = pstrContModId;
                                                drowMetaData["LanguageId"] = drowClientLang[0]["ID"].ToString();
                                                if (xNodeInTitle.InnerText.Length <= 100)
                                                {
                                                    drowMetaData["ContentModuleName"] = xNodeInTitle.InnerText;
                                                }
                                                else
                                                {
                                                    drowMetaData["ContentModuleName"] = xNodeInTitle.InnerText.Substring(0, 99);
                                                }
                                                dtblMetaData.Rows.Add(drowMetaData);
                                            }
                                            else  //FOR NON SUPPORTED LANGUAGES
                                            {
                                                DataRow drowMetaData = dtblMetaData.NewRow();
                                                drowMetaData["ContentModuleId"] = pstrContModId;
                                                drowMetaData["LanguageId"] = strLang;
                                                if (xNodeInTitle.InnerText.Length <= 100)
                                                {
                                                    drowMetaData["ContentModuleName"] = xNodeInTitle.InnerText;
                                                }
                                                else
                                                {
                                                    drowMetaData["ContentModuleName"] = xNodeInTitle.InnerText.Substring(0, 99);
                                                }
                                                dtblMetaData.Rows.Add(drowMetaData);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (xNodeInGeneral.Name.ToLower() == "description")
                    {
                        foreach (XmlNode xNodeInDescription in xNodeInGeneral.ChildNodes)
                        {
                            if (xNodeInDescription.Attributes.Count > 0 && xNodeInDescription.Attributes[0].Name.ToLower() == "xml:lang")
                            {
                                string strLang = xNodeInDescription.Attributes["xml:lang"].Value;
                                if (strLang != string.Empty)
                                {
                                    DataRow[] drowMetaData = dtblMetaData.Select("LanguageId = '" + strLang + "'");
                                    if (drowMetaData.Length == 1)
                                    {
                                        drowMetaData[0]["ContentModuleDescription"] = xNodeInDescription.InnerText;
                                    }
                                }
                            }
                        }
                    }
                    else if (xNodeInGeneral.Name.ToLower() == "keywords")
                    {
                        foreach (XmlNode xNodeInKeywords in xNodeInGeneral.ChildNodes)
                        {
                            if (xNodeInKeywords.Attributes.Count > 0 && xNodeInKeywords.Attributes[0].Name.ToLower() == "xml:lang")
                            {
                                string strLang = xNodeInKeywords.Attributes["xml:lang"].Value;
                                if (strLang != string.Empty)
                                {
                                    DataRow[] drowMetaData = dtblMetaData.Select("LanguageId = '" + strLang + "'");
                                    if (drowMetaData.Length == 1)
                                    {
                                        drowMetaData[0]["ContentModuleKeyWords"] = xNodeInKeywords.InnerText;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return dtblMetaData;
        }

        /// <summary>
        /// Set languages from table to ContentModule 
        /// </summary>
        /// <param name="pentContMod"></param>
        /// <param name="pdtblMetaData"></param>
        /// <returns></returns>
        public static List<BaseEntity> SetContentModuleLanguages(ContentModule pentContMod, DataTable pdtblMetaData)
        {
            List<BaseEntity> entContModList = new List<BaseEntity>();
            try
            {
                if (pdtblMetaData != null && pdtblMetaData.Rows.Count > 0)
                {
                    pentContMod.ContentModuleLanguages.Clear();
                    foreach (DataRow drMetaData in pdtblMetaData.Rows)
                    {
                        ContentModule entContMod = (ContentModule)pentContMod.Clone();

                        entContMod.LanguageId = drMetaData["LanguageId"].ToString();
                        entContMod.ContentModuleName = drMetaData["ContentModuleName"].ToString();
                        entContMod.ContentModuleDescription = drMetaData["ContentModuleDescription"].ToString();
                        entContMod.ContentModuleKeyWords = drMetaData["ContentModuleKeyWords"].ToString();
                        entContModList.Add(entContMod);
                    }
                }
            }
            catch (Exception expData)
            {
                throw new CustomException(YPLMS.Services.Messages.Common.MANIFEST_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expData, true);

            }
            return entContModList;
        }

        /// <summary>
        /// Splitting the Organization node and create new xml file
        /// </summary>
        /// <param name="pstrCourseId">Course Id</param>
        /// <param name="pdtblMetaData">Table</param>
        /// <returns></returns>
        public List<ContentModule> ManifestSplit(string pstrPath)
        {
            ContentModule entContentModule;
            List<ContentModule> entListContentModule = new List<ContentModule>();
            string strClientId = EncryptionManager.Decrypt("P+jUwjxdIhc=");
            FileHandler fileHandler = new FileHandler(strClientId);
            string strManifestPath = fileHandler.RootSharedPath.Replace("\\\\", "\\");

            string strStartPath = pstrPath; //strManifestPath.Substring(strManifestPath.IndexOf(FileHandler.COURSE_FOLDER_PATH));
            strStartPath = strStartPath.Replace(@"\", @"/");
            pstrPath = pstrPath.Replace("/", @"\");
            strManifestPath += pstrPath;
            string strMetaDataXml = strManifestPath;
            strManifestPath += @"\imsmanifest.xml";
            XmlDocument xDocMF = new XmlDocument();
            XMLLib xmlLib = new XMLLib();
            XmlNodeList orgNodeList = null;
            XmlNamespaceManager nsmanager = new XmlNamespaceManager(xDocMF.NameTable);
            XmlDocument xDocNew;
            if (File.Exists(strManifestPath) && xmlLib.fOpenFreeXMLDoc(ref xDocMF, strManifestPath))
            {
                nsmanager.AddNamespace(strNmSpacePrefix, strNmSpaceURI);
                xDocMF = xmlLib.StripDocumentNamespace(xDocMF);
                xmlLib.NSManager = nsmanager;
                xmlLib.fCreateContext(xDocMF, strOrgContext, ref orgNodeList);
                //If only one organization then no need to create File. 
                if (orgNodeList.Count > 1)
                {
                    foreach (XmlNode xNode in orgNodeList)
                    {
                        string strId = xNode.Attributes["identifier"].Value;
                        //string strOrgTitle = xNode.FirstChild.InnerText; //xNode.SelectSingleNode("title").Value;
                        string strOrgTitle = xNode.SelectSingleNode("title").InnerText;


                        string strNewFileNamePath = strMetaDataXml + "\\imsmanifest_" + strId + ".xml";
                        string strNewPath = "";
                        if (!strStartPath.EndsWith("/"))
                        {
                            strNewPath = strStartPath + "/imsmanifest_" + strId + ".xml";
                        }
                        else
                        {
                            strNewPath = strStartPath + "imsmanifest_" + strId + ".xml";
                        }
                        entContentModule = new ContentModule();
                        entContentModule.ImanifestUrl = strNewPath;
                        entContentModule.ContentModuleEnglishName = strOrgTitle;
                        entListContentModule.Add(entContentModule);

                        xDocNew = new XmlDocument();
                        //Create New xml document object
                        xmlLib.fOpenFreeXMLDoc(ref xDocNew, strManifestPath);
                        XmlNodeList orgNodeListNew = null;
                        xDocNew = xmlLib.StripDocumentNamespace(xDocNew);
                        xmlLib.fCreateContext(xDocNew, strOrgContext, ref orgNodeListNew);
                        foreach (XmlNode xNodeNew in orgNodeListNew)
                        {
                            string curID = xNodeNew.Attributes["identifier"].Value;
                            if (!string.Equals(curID, strId))
                            {   //Delete this node
                                xmlLib.fRemoveNode(ref xDocNew, strOrgContext + "[@identifier='" + curID + "']");
                            }
                        }
                        xDocNew.Save(strNewFileNamePath);
                    }
                }
                else
                {
                    foreach (XmlNode xNode in orgNodeList)
                    {
                        string strId = xNode.Attributes["identifier"].Value;
                        string strOrgTitle = xNode.SelectSingleNode("title").InnerText;
                        string strNewFileNamePath = strMetaDataXml + "\\imsmanifest_" + strId + ".xml";
                        string strNewPath = "";
                        if (!strStartPath.EndsWith("/"))
                        {
                            strNewPath = strStartPath + "/imsmanifest_" + strId + ".xml";
                        }
                        else
                        {
                            strNewPath = strStartPath + "imsmanifest_" + strId + ".xml";
                        }
                        entContentModule = new ContentModule();
                        entContentModule.ImanifestUrl = strNewPath;
                        entContentModule.ContentModuleEnglishName = strOrgTitle;
                        entListContentModule.Add(entContentModule);
                    }
                }
            }
            return entListContentModule;
        }

        public string ReadManifestAndMetaData(string pstrCourseId, ContentModule pContentModule, string hdnFTPCoursePath)
        {
            string strErrorMsg = string.Empty;
            //Client entClient = new Client();
            //entClient.ID = _strSelectedClientId;
            //entClient = _clientManager.Execute(entClient, Client.Method.Get);
            string hdnMasteryScore = string.Empty;
            DataTable tblMetaData = null;
            ManifestReader srvManifestReader = new ManifestReader(pContentModule.ClientId);
            ContentModuleTrackingManager _contentModuleTrackingManager = new ContentModuleTrackingManager();
            try
            {

                string strCoursePath = hdnFTPCoursePath;//txtFtpCoursePath.Text;
                                                                       // string strCourseFolder = strCoursePath.Substring(strCoursePath.IndexOf(FileHandler.COURSE_FOLDER_PATH));
                if (string.IsNullOrEmpty(hdnFTPCoursePath))
                {
                    strCoursePath = pContentModule.ContentModuleURL;
                }
                string strCourseFolder = strCoursePath.Substring(strCoursePath.IndexOf(FileHandler.CLIENTS_FOLDER_PATH));

                if (strCourseFolder.ToLower().IndexOf("/imsmanifest.xml") != -1)
                {
                    strCourseFolder = strCourseFolder.Substring(0, strCourseFolder.Length - 16);
                }
                if (pContentModule.ContentModuleTypeId == ActivityContentType.Scorm2004.ToString().ToLower())
                {
                    tblMetaData = srvManifestReader.ReadCourseManifestForScorm2004(strCourseFolder, pstrCourseId);
                    //GetCourseQuestionsAndAddRequiredFunction();
                }
                else
                {
                    tblMetaData = srvManifestReader.ReadCourseManifest(strCourseFolder, pstrCourseId);
                }

                if (string.IsNullOrEmpty(YPLMS.Services.Common.CourseMasteryScore))
                {
                    YPLMS.Services.Common.CourseMasteryScore = _contentModuleTrackingManager.GetMasteryScoreFromMasterContentXML(pContentModule.ClientId, strCoursePath, pstrCourseId);
                }
                hdnMasteryScore = YPLMS.Services.Common.CourseMasteryScore;
            }
            catch (CustomException ex)
            {
                strErrorMsg = MessageAdaptor.GetMessage(ex.Message);
            }
            
            if (tblMetaData != null && tblMetaData.Rows.Count > 0 )
            {
                DataRow[] drDefClientLang = tblMetaData.Select("LanguageId = '" + Language.SYSTEM_DEFAULT_LANG_ID /*entClient.DefaultLanguageId*/ + "'");
                if (drDefClientLang.Length == 1)
                {
                    pContentModule.ContentModuleEnglishName = ValidationManager.RemoveSpecialCharsForNameAndDescription(Convert.ToString(drDefClientLang[0]["ContentModuleName"]));
                    pContentModule.ContentModuleDescription = ValidationManager.RemoveSpecialCharsForNameAndDescription(drDefClientLang[0]["ContentModuleDescription"].ToString());
                    pContentModule.ContentModuleKeyWords = ValidationManager.RemoveSpecialChars(drDefClientLang[0]["ContentModuleKeyWords"].ToString());
                }
            }
            return strErrorMsg;
        }
    }
}
