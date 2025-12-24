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
    public class MasterContentsReader
    {
        string strClientID = string.Empty;
        string strOrgContext = "//organizations/organization";
        string strNmSpaceURI = "http://www.adlnet.org/xsd/adlcp_rootv1p2";
        string strNmSpacePrefix = "adlcp";

        /// <summary>
        /// Common class for Manifest related functions
        /// </summary>
        public MasterContentsReader(string pstrClientId)
        {
            strClientID = pstrClientId;
        }

        /// <summary>
        /// Read course manifest and add data to Table
        /// </summary>
        /// <param name="pstrCourseId">Course Id</param>
        /// <param name="pdtblMetaData">Table</param>
        /// <returns></returns>
        public DataTable ReadCourseMasterContents(string pstrCoursePath, string pCourseId, string pstrMastercontentPath)
        {
            DataTable dtblQuestionTable = null;
            string strClientId = EncryptionManager.Decrypt(ConfigurationManager.AppSettings[Client.BASE_CLIENT_KEY]);
            FileHandler fileHandler = new FileHandler(strClientId);
            string strMasterContents = fileHandler.RootSharedPath.Replace("\\\\", "\\");
            pstrCoursePath = pstrCoursePath.Replace("/", @"\");
            strMasterContents += pstrCoursePath;
            string strMetaDataXml = strMasterContents;
            strMasterContents += @"\mastercontents.xml";

            XmlDocument xDocMF = new XmlDocument();
            XMLLib xmlLib = new XMLLib();
            XmlDocument xDocMD = new XmlDocument();
            if (File.Exists(pstrMastercontentPath) && xmlLib.fOpenFreeXMLDoc(ref xDocMF, pstrMastercontentPath))
            {
                try
                {
                    bool isRapidelCourse = false;
                    var MainNode = xDocMF.SelectSingleNode("Pages[@CourseType='MURapidel']");
                    if (MainNode != null) isRapidelCourse = true;

                    XmlNodeList nodeList = null;
                    if (isRapidelCourse)
                        nodeList = xDocMF.SelectNodes("Pages/item/item[@Type='SkillCheck']");
                    else
                        nodeList = xDocMF.SelectNodes("Main/Module/Lesson/Topic[@TopicType='Assessment']/Mod");

                    if (nodeList == null) return dtblQuestionTable;
                    foreach (XmlNode xNodeInMod in nodeList)
                    {
                        if (xNodeInMod.Name.ToLower() == "mod" || xNodeInMod.Name.ToLower() == "item")
                        {
                            dtblQuestionTable = ReadQuestionData(xNodeInMod, pCourseId, pstrCoursePath, isRapidelCourse);
                        }
                    }

                    #region Commented by santosh 

                    //foreach (XmlNode xNodeInMF in xDocMF.DocumentElement.ChildNodes)
                    //{
                    //    if (xNodeInMF.Name.ToLower() == "module")
                    //    {
                    //        foreach (XmlNode xNodeInMD in xNodeInMF.ChildNodes)
                    //        {
                    //            if (xNodeInMD.Name.ToLower() == "lesson")
                    //            {
                    //                foreach (XmlNode xNodeInTopic in xNodeInMD.ChildNodes)
                    //                {
                    //                    if (xNodeInTopic.Name.ToLower() == "topic")
                    //                    {
                    //                        foreach (XmlNode xNodeInMod in xNodeInTopic.ChildNodes)
                    //                        {
                    //                            if (xNodeInMod.Name.ToLower() == "mod")
                    //                            {
                    //                                dtblQuestionTable = ReadQuestionData(xNodeInMod, pCourseId, pstrCoursePath);
                    //                            }
                    //                        }
                    //                    }                                       
                    //                }                                                                                                        
                    //            }                             
                    //        }                           
                    //    }
                    //}
                    #endregion

                    #region get the mastery score in case of IsAssessment course                    
                    nodeList = xDocMF.SelectNodes("Pages/passingscore");
                    if (nodeList != null)
                    {
                        foreach (XmlNode xNodeInMod in nodeList)
                        {
                            Common.CourseMasteryScore = xNodeInMod.InnerText;
                        }
                    }
                    #endregion get the mastery score in case of IsAssessment course
                }
                catch (Exception expXml)
                {
                    CustomException exCustom = new CustomException(YPLMS.Services.Messages.Common.MANIFEST_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expXml, true);
                    dtblQuestionTable = null;
                }
            }
            else
            {
                CustomException exCustom = new CustomException(YPLMS.Services.Messages.Common.MANIFEST_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, null, false);
                dtblQuestionTable = null;
            }
            return dtblQuestionTable;
        }

        /// <summary>
        /// Read Question Data
        /// </summary>
        /// <param name="xNodeInTopic"></param>
        /// <param name="pstrContModId"></param>
        /// <returns></returns>
        private DataTable ReadQuestionData(XmlNode xNodeInMod, string pstrContModId, string pstrCoursePath, bool isRapidelCourse)
        {
            DataTable dtblQuestionData = new DataTable();
            DataColumn dcolQuestionId = new DataColumn("QuestionId", Type.GetType("System.String"));
            dtblQuestionData.Columns.Add(dcolQuestionId);
            DataColumn dcolContentModuleId = new DataColumn("ContentModuleId", Type.GetType("System.String"));
            dtblQuestionData.Columns.Add(dcolContentModuleId);
            DataColumn dcolQuestionText = new DataColumn("QuestionText", Type.GetType("System.String"));
            dtblQuestionData.Columns.Add(dcolQuestionText);
            DataColumn dcolQuestionUniqueIndxNum = new DataColumn("QuestionUniqueIndxNum", Type.GetType("System.String"));
            dtblQuestionData.Columns.Add(dcolQuestionUniqueIndxNum);
            try
            {
                int indexCount = 0; //item[@SkillCheck='True']
                foreach (XmlNode xNodeInLes in xNodeInMod.ChildNodes)
                {
                    if (isRapidelCourse && (xNodeInLes.Name.ToLower() == "item" && xNodeInLes.Attributes["SkillCheck"] != null && xNodeInLes.Attributes["SkillCheck"].Value.ToLower() == "true"))
                    {
                        DataRow drowPageData = dtblQuestionData.NewRow();
                        drowPageData["QuestionId"] = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(DataAccessManager.Schema.Common.VAL_ASSET_QUESTION_ID_PREFIX, DataAccessManager.Schema.Common.VAL_ASSET_QUESTION_ID_LENGTH);
                        drowPageData["ContentModuleId"] = pstrContModId;
                        string strXMLPath = string.Empty;

                        try
                        {
                            strXMLPath = xNodeInLes.Attributes["XMLPath"].Value;

                        }
                        catch (Exception ex)
                        {
                            throw new CustomException(YPLMS.Services.Messages.Common.MANIFEST_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, ex, true);
                        }

                        drowPageData["QuestionUniqueIndxNum"] = indexCount++;
                        drowPageData["QuestionText"] = GetQuestionText(pstrCoursePath, strXMLPath, isRapidelCourse);
                        dtblQuestionData.Rows.Add(drowPageData);

                    }
                    else
                    {
                        if (xNodeInLes.Name.ToLower() == "les")
                        {
                            foreach (XmlNode xNodeInPage in xNodeInLes.ChildNodes)
                            {
                                DataRow drowPageData = dtblQuestionData.NewRow();
                                drowPageData["QuestionId"] = YPLMS.Services.IDGenerator.GetUniqueKeyWithPrefix(DataAccessManager.Schema.Common.VAL_ASSET_QUESTION_ID_PREFIX, DataAccessManager.Schema.Common.VAL_ASSET_QUESTION_ID_LENGTH);
                                drowPageData["ContentModuleId"] = pstrContModId;
                                string strUniqueIndxNum = string.Empty;
                                string strXMLPath = string.Empty;

                                try
                                {
                                    strUniqueIndxNum = xNodeInPage.Attributes["UniqueIndxNum"].Value;
                                    strXMLPath = xNodeInPage.Attributes["XMLPath"].Value;

                                }
                                catch { }

                                drowPageData["QuestionUniqueIndxNum"] = strUniqueIndxNum;
                                drowPageData["QuestionText"] = GetQuestionText(pstrCoursePath, strXMLPath, isRapidelCourse);

                                dtblQuestionData.Rows.Add(drowPageData);
                            }
                        }
                    }
                }
            }
            catch (Exception Outex)
            {
                throw new CustomException(YPLMS.Services.Messages.Common.MANIFEST_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, Outex, true);

            }


            return dtblQuestionData;
        }


        public string GetQuestionText(string pstrCoursePath, string pstrPath, bool isRapidelCourse)
        {
            string strClientId = EncryptionManager.Decrypt(ConfigurationManager.AppSettings[Client.BASE_CLIENT_KEY]);
            FileHandler fileHandler = new FileHandler(strClientId);
            string strMasterContents = fileHandler.RootSharedPath.Replace("\\\\", "\\");
            pstrCoursePath = pstrCoursePath.Replace("/", @"\");
            strMasterContents += pstrCoursePath;
            string strMetaDataXml = strMasterContents;
            XMLLib xmlLib = new XMLLib();

            if (isRapidelCourse)
            {
                string strmaifestfile = strMasterContents + "\\imsmanifest.xml";

                XmlDocument xDocMF = new XmlDocument();
                xmlLib.fOpenFreeXMLDoc(ref xDocMF, strmaifestfile);
                XmlElement xnRoot = xDocMF.DocumentElement;
                string strMastercontentPath = xnRoot.Attributes["identifier"].Value.ToLower();

                strMasterContents += @"\" + strMastercontentPath.Split('_')[0] + @"\" + pstrPath;
            }
            else
                strMasterContents += @"\Pages\" + pstrPath;

            XmlDocument xDocQuestion = new XmlDocument();
            xmlLib = new XMLLib();
            //XmlDocument xDocMD = new XmlDocument();
            string strQuestionText = string.Empty;

            if (File.Exists(strMasterContents) && xmlLib.fOpenFreeXMLDoc(ref xDocQuestion, strMasterContents))
            {
                try
                {
                    foreach (XmlNode xNodeIn in xDocQuestion.DocumentElement.ChildNodes)
                    {
                        if (xNodeIn.Name.ToLower() == "interactivity" || xNodeIn.Name.ToLower() == "constants")
                        {
                            foreach (XmlNode xNodeInQuestion in xNodeIn.ChildNodes)
                            {
                                if (xNodeInQuestion.Name.ToLower() == "question")
                                {
                                    strQuestionText = xNodeInQuestion.InnerText;

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
            }
            else
            {
                throw new CustomException(YPLMS.Services.Messages.Common.MANIFEST_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, null, false);
            }
            return strQuestionText;
        }

        /// <summary>
        /// Set languages from table to ContentModule 
        /// </summary>
        /// <param name="pentContMod"></param>
        /// <param name="pdtblMetaData"></param>
        /// <returns></returns>
        //public static List<BaseEntity> SetContentModuleLanguages(ContentModule pentContMod, DataTable pdtblMetaData)
        //{
        //    List<BaseEntity> entContModList = new List<BaseEntity>();
        //    try
        //    {
        //        if (pdtblMetaData != null && pdtblMetaData.Rows.Count > 0)
        //        {
        //            pentContMod.ContentModuleLanguages.Clear();
        //            foreach (DataRow drMetaData in pdtblMetaData.Rows)
        //            {
        //                ContentModule entContMod = (ContentModule) pentContMod.Clone();

        //                entContMod.LanguageId = drMetaData["LanguageId"].ToString();
        //                entContMod.ContentModuleName = drMetaData["ContentModuleName"].ToString();
        //                entContMod.ContentModuleDescription = drMetaData["ContentModuleDescription"].ToString();
        //                entContMod.ContentModuleKeyWords = drMetaData["ContentModuleKeyWords"].ToString();
        //                entContModList.Add(entContMod);
        //            }
        //        }
        //    }
        //    catch (Exception expData)
        //    {
        //        throw new CustomException(Services.Messages.Common.MANIFEST_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expData, true);

        //    }
        //    return entContModList;
        //}

        /// <summary>
        /// Splitting the Organization node and create new xml file
        /// </summary>
        /// <param name="pstrCourseId">Course Id</param>
        /// <param name="pdtblMetaData">Table</param>
        /// <returns></returns>
        //public List<ContentModule> ManifestSplit(string pstrPath)
        //{
        //    ContentModule entContentModule;
        //    List<ContentModule> entListContentModule = new List<ContentModule>();
        //    string strClientId = EncryptionManager.Decrypt(ConfigurationManager.AppSettings[YPLMS.Entity.Client.BASE_CLIENT_KEY]);
        //    FileHandler fileHandler = new FileHandler(strClientId);
        //    string strManifestPath = fileHandler.RootSharedPath.Replace("\\\\", "\\");

        //    string strStartPath = pstrPath; //strManifestPath.Substring(strManifestPath.IndexOf(FileHandler.COURSE_FOLDER_PATH));
        //    strStartPath = strStartPath.Replace(@"\", @"/");
        //    pstrPath = pstrPath.Replace("/", @"\");
        //    strManifestPath += pstrPath;
        //    string strMetaDataXml = strManifestPath;
        //    strManifestPath += @"\imsmanifest.xml";
        //    XmlDocument xDocMF = new XmlDocument();
        //    XMLLib xmlLib = new XMLLib();
        //    XmlNodeList orgNodeList = null;
        //    XmlNamespaceManager nsmanager = new XmlNamespaceManager(xDocMF.NameTable);
        //    XmlDocument xDocNew;
        //    if (File.Exists(strManifestPath) && xmlLib.fOpenFreeXMLDoc(ref xDocMF, strManifestPath))
        //    {
        //        nsmanager.AddNamespace(strNmSpacePrefix, strNmSpaceURI);
        //        xDocMF = xmlLib.StripDocumentNamespace(xDocMF);
        //        xmlLib.NSManager = nsmanager;
        //        xmlLib.fCreateContext(xDocMF, strOrgContext, ref orgNodeList);
        //        //If only one organization then no need to create File. 
        //        if (orgNodeList.Count > 1)
        //        {
        //            foreach (XmlNode xNode in orgNodeList)
        //            {
        //                string strId = xNode.Attributes["identifier"].Value;
        //                //string strOrgTitle = xNode.FirstChild.InnerText; //xNode.SelectSingleNode("title").Value;
        //                string strOrgTitle = xNode.SelectSingleNode("title").InnerText;


        //                string strNewFileNamePath = strMetaDataXml + "\\imsmanifest_" + strId + ".xml";
        //                string strNewPath = "";
        //                if (!strStartPath.EndsWith("/"))
        //                {
        //                    strNewPath = strStartPath + "/imsmanifest_" + strId + ".xml";
        //                }
        //                else
        //                {
        //                    strNewPath = strStartPath + "imsmanifest_" + strId + ".xml";
        //                }
        //                entContentModule = new ContentModule();
        //                entContentModule.ImanifestUrl = strNewPath;
        //                entContentModule.ContentModuleEnglishName = strOrgTitle;
        //                entListContentModule.Add(entContentModule);

        //                xDocNew = new XmlDocument();
        //                //Create New xml document object
        //                xmlLib.fOpenFreeXMLDoc(ref xDocNew, strManifestPath);
        //                XmlNodeList orgNodeListNew = null;
        //                xDocNew = xmlLib.StripDocumentNamespace(xDocNew);
        //                xmlLib.fCreateContext(xDocNew, strOrgContext, ref orgNodeListNew);
        //                foreach (XmlNode xNodeNew in orgNodeListNew)
        //                {
        //                    string curID = xNodeNew.Attributes["identifier"].Value;
        //                    if (!string.Equals(curID, strId))
        //                    {   //Delete this node
        //                        xmlLib.fRemoveNode(ref xDocNew, strOrgContext + "[@identifier='" + curID + "']");
        //                    }
        //                }
        //                xDocNew.Save(strNewFileNamePath);
        //            }
        //        }
        //        else
        //        {
        //            foreach (XmlNode xNode in orgNodeList)
        //            {
        //                string strId = xNode.Attributes["identifier"].Value;                        
        //                string strOrgTitle = xNode.SelectSingleNode("title").InnerText;
        //                string strNewFileNamePath = strMetaDataXml + "\\imsmanifest_" + strId + ".xml";
        //                string strNewPath = "";
        //                if (!strStartPath.EndsWith("/"))
        //                {
        //                    strNewPath = strStartPath + "/imsmanifest_" + strId + ".xml";
        //                }
        //                else
        //                {
        //                    strNewPath = strStartPath + "imsmanifest_" + strId + ".xml";
        //                }
        //                entContentModule = new ContentModule();
        //                entContentModule.ImanifestUrl = strNewPath;
        //                entContentModule.ContentModuleEnglishName = strOrgTitle;
        //                entListContentModule.Add(entContentModule);                        
        //            }
        //        }
        //    }
        //    return entListContentModule;
        //}
    }
}
