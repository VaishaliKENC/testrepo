using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    public class AICCToManifest
    {

        public string AICC_FILE_NAME = "course";

        /// <summary>
        /// AICCToManifest
        /// </summary>
        public AICCToManifest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Is AICC Files Exists
        /// </summary>
        /// <param name="pstrCourseDirPath"></param>
        /// <returns></returns>
        public bool IsAICCFilesExists(string pstrCourseDirPath)
        {
            bool blnIsExist = false;
            string strFileNameWithoutExt = string.Empty;
            try
            {
                DirectoryInfo dirInfoCoursePath = new DirectoryInfo(pstrCourseDirPath);
                if (dirInfoCoursePath.Exists)
                {
                    FileInfo[] FileList = dirInfoCoursePath.GetFiles("*.crs", SearchOption.TopDirectoryOnly);

                    foreach (FileInfo FI in FileList)
                    {
                        strFileNameWithoutExt = Path.GetFileNameWithoutExtension(FI.FullName);
                    }
                    if (strFileNameWithoutExt != string.Empty)
                    {
                        if (File.Exists(pstrCourseDirPath + strFileNameWithoutExt + ".au") &&
                            File.Exists(pstrCourseDirPath + strFileNameWithoutExt + ".des") &&
                            File.Exists(pstrCourseDirPath + strFileNameWithoutExt + ".cst") &&
                            File.Exists(pstrCourseDirPath + strFileNameWithoutExt + ".crs"))
                        {
                            AICC_FILE_NAME = strFileNameWithoutExt;
                            blnIsExist = true;
                        }
                    }
                }
            }
            catch { }
            return blnIsExist;
        }

        /// <summary>
        /// Create Xml From File
        /// </summary>
        /// <param name="pstrFileName"></param>
        /// <param name="pxmldocFileXmlObj"></param>
        public void CreateXmlFromFile(string pstrFileName, ref XmlDocument pxmldocFileXmlObj)
        {
            string[] strarrHeaderArray;
            string[] strarrValuesArray;
            int iCounter;
            XMLLib xlib = new XMLLib();

            XmlNode xmlnodeItemNode = null;
            XmlNode xmlnodeRootNode = null;

            StreamReader strmreaderFile = new StreamReader(pstrFileName);
            string strInput = string.Empty;

            xlib.fCreateXMLObj(ref pxmldocFileXmlObj);
            pxmldocFileXmlObj.LoadXml("<root></root>");
            xlib.fCreateFirstContext(pxmldocFileXmlObj.DocumentElement, "/root", ref xmlnodeRootNode);

            strInput = strmreaderFile.ReadLine();
            strInput = strInput.Replace(Convert.ToChar(34), Convert.ToChar(" "));
            char[] cSep = { ',' };
            strarrHeaderArray = strInput.Split(cSep);
            while ((strInput = strmreaderFile.ReadLine()) != null)
            {
                strInput = strInput.Replace(Convert.ToChar(34), Convert.ToChar(" "));
                strarrValuesArray = strInput.Split(cSep);
                if (strarrValuesArray.Length == strarrHeaderArray.Length)
                {
                    xlib.aCreateNode("item", ref xmlnodeItemNode);
                    xlib.fSetAttribute(ref xmlnodeItemNode, "id", Convert.ToString(strarrValuesArray[0]).Trim());
                    iCounter = 0;
                    foreach (string word in strarrValuesArray)
                    {
                        XmlNode xmlnodeContextNode = null;
                        XmlNode xmlnodeNewNode = null;
                        if (xlib.fCreateFirstContext(xmlnodeItemNode, ".", ref xmlnodeContextNode))
                        {
                            xlib.aCreateNode(Convert.ToString(strarrHeaderArray[iCounter]).ToLower().Trim(), ref xmlnodeNewNode);
                            xlib.fAddCdataToNode(ref xmlnodeNewNode, Convert.ToString(strarrValuesArray[iCounter]).Trim());
                            xlib.fAppendNode(ref xmlnodeContextNode, ref xmlnodeNewNode);
                        }
                        iCounter++;
                    }
                    try
                    {
                        pxmldocFileXmlObj.DocumentElement.AppendChild(pxmldocFileXmlObj.ImportNode(xmlnodeItemNode, true));
                    }
                    catch (Exception exComm)
                    {
                        CustomException expCustom = new CustomException(YPLMS.Services.Messages.Common.INVALID_KEY, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, exComm, true);
                        throw expCustom;
                    }
                }
            }
            strmreaderFile.Close();
            strmreaderFile.Dispose();
        }

        /// <summary>
        /// Open Free XML Doc
        /// </summary>
        /// <param name="xmldocManifestXmlObj"></param>
        /// <param name="pstrPath"></param>
        public void OpenFreeXMLDoc(ref XmlDocument xmldocManifestXmlObj, string pstrPath)
        {
            XMLLib xlib = new XMLLib();
            xlib.fOpenFreeXMLDoc(ref xmldocManifestXmlObj, pstrPath);
        }

        /// <summary>
        /// Put Values From CRS File
        /// </summary>
        /// <param name="pxmldocManifestXmlObj"></param>
        /// <param name="pstrCRSFilePath"></param>
        public void PutValuesFromCRSFile(ref XmlDocument pxmldocManifestXmlObj, string pstrCRSFilePath)
        {
            XMLLib xlib = new XMLLib();
            ArrayList arrListCRSList = new ArrayList();
            string strName = string.Empty;
            string strRetVal = string.Empty;
            arrListCRSList = GetValueFromFile(pstrCRSFilePath);
            XmlNode xmlnodeRetNode = null;
            if (this.GetValueFromCRSFile(arrListCRSList, "Course_ID", ref strRetVal))
            {
                xlib.fCreateFirstContext(pxmldocManifestXmlObj, "/manifest/organizations", ref xmlnodeRetNode);
                xlib.fSetAttribute(ref xmlnodeRetNode, "default", strRetVal);
                xlib.fCreateFirstContext(pxmldocManifestXmlObj, "/manifest/organizations/organization", ref xmlnodeRetNode);
                xlib.fSetAttribute(ref xmlnodeRetNode, "identifier", strRetVal);
            }
            if (this.GetValueFromCRSFile(arrListCRSList, "Course_Title", ref strRetVal))
            {
                XmlNode xmlnodeNewNode = null;
                xlib.aCreateNode("title", ref xmlnodeNewNode);
                xlib.fAddCdataToNode(ref xmlnodeNewNode, Convert.ToString(strRetVal));
                pxmldocManifestXmlObj.SelectSingleNode("/manifest/organizations/organization").AppendChild(pxmldocManifestXmlObj.ImportNode(xmlnodeNewNode, true));
            }
            XmlNode lCourseDataNode = pxmldocManifestXmlObj.SelectSingleNode("/manifest");
            CreateCourseDataNodeFromCRSFile(arrListCRSList, ref pxmldocManifestXmlObj, ref lCourseDataNode, xlib);
        }

        /// <summary>
        /// Create Course Data Node From CRS File
        /// </summary>
        /// <param name="parrListCRSList"></param>
        /// <param name="pxmldocManifestXmlObj"></param>
        /// <param name="pxmlnodeCourseDataNode"></param>
        /// <param name="xlib"></param>
        public void CreateCourseDataNodeFromCRSFile(ArrayList parrListCRSList, ref XmlDocument pxmldocManifestXmlObj, ref XmlNode pxmlnodeCourseDataNode, XMLLib xlib)
        {
            string strTitle = string.Empty;
            XmlNode xmlnodeNode = null;
            xlib.aCreateNode("coursedata", ref pxmlnodeCourseDataNode);
            foreach (string sTemp in parrListCRSList)
            {
                if (sTemp.Trim().StartsWith("["))
                {
                    strTitle = sTemp.Substring(sTemp.IndexOf('[') + 1, sTemp.IndexOf(']') - 1);
                    try
                    {
                        xlib.fCreateAndAppendNode(strTitle, ref xmlnodeNode, ref pxmlnodeCourseDataNode);
                    }
                    catch (Exception exComm)
                    {
                        CustomException expCustom = new CustomException(YPLMS.Services.Messages.Common.INVALID_KEY, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, exComm, true);
                        throw expCustom;
                    }
                }
                else
                {
                    string[] strarrTempArray;
                    char[] cSep = { '=' };
                    strarrTempArray = sTemp.Split(cSep);
                    if (strarrTempArray.Length > 1)
                    {
                        try
                        {
                            XmlNode xmlnodeContextNode = null;
                            XmlNode xmlnodeNewNode = null;
                            XmlNode xmlnodeItemNode = null;
                            xlib.aCreateNode(strTitle, ref xmlnodeItemNode);
                            if (xlib.fCreateFirstContext(pxmlnodeCourseDataNode, strTitle, ref xmlnodeContextNode))
                            {
                                xlib.aCreateNode(Convert.ToString(strarrTempArray[0]).Trim(), ref xmlnodeNewNode);
                                xlib.fAddCdataToNode(ref xmlnodeNewNode, Convert.ToString(strarrTempArray[1]).Trim());
                                xlib.fAppendNode(ref xmlnodeContextNode, ref xmlnodeNewNode);
                            }
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        if (strTitle.Trim().ToLower().CompareTo("course_description") == 0)
                        {
                            xlib.fAddCdataToNode(ref xmlnodeNode, Convert.ToString(strarrTempArray[0]).Trim());
                        }
                    }
                }
            }
            pxmldocManifestXmlObj.DocumentElement.AppendChild(pxmldocManifestXmlObj.ImportNode(pxmlnodeCourseDataNode, true));
        }

        /// <summary>
        /// Get Value From CRS File
        /// </summary>
        /// <param name="parrListCRSList"></param>
        /// <param name="pstrValueOf"></param>
        /// <param name="pstrRetVal"></param>
        /// <returns></returns>
        public bool GetValueFromCRSFile(ArrayList parrListCRSList, string pstrValueOf, ref string pstrRetVal)
        {
            bool blnIsValue = false;
            foreach (string strTemp in parrListCRSList)
            {
                string[] strarrTempArray;
                char[] cSep = { '=' };
                strarrTempArray = strTemp.Split(cSep);
                if (strarrTempArray.Length > 1)
                {
                    if (strarrTempArray[0].ToLower().Trim().CompareTo(pstrValueOf.ToLower().Trim()) == 0)
                    {
                        blnIsValue = true;
                        pstrRetVal = strarrTempArray[1].Trim();
                    }
                }
            }
            return blnIsValue;
        }

        /// <summary>
        /// Get Value From File
        /// </summary>
        /// <param name="pstrFilePath"></param>
        /// <returns></returns>
        public ArrayList GetValueFromFile(string pstrFilePath)
        {
            ArrayList arrListValueList = new ArrayList();
            StreamReader strmReaderStream = new StreamReader(pstrFilePath);
            string strInput = string.Empty;
            while ((strInput = strmReaderStream.ReadLine()) != null)
            {
                arrListValueList.Add(strInput);
            }
            strmReaderStream.Close();
            strmReaderStream.Dispose();
            return arrListValueList;
        }

        /// <summary>
        /// Create Item Nodes
        /// </summary>
        /// <param name="pstrCSTFilePath"></param>
        /// <param name="pxmldocManifestXmlObj"></param>
        /// <param name="pxmldocDESXMLObj"></param>
        /// <param name="pxmldocAUXMLObj"></param>
        public void CreateItemNodes(string pstrCSTFilePath, ref XmlDocument pxmldocManifestXmlObj, ref XmlDocument pxmldocDESXMLObj, ref XmlDocument pxmldocAUXMLObj)
        {
            XMLLib xLib = new XMLLib();
            //string[] strarrRowArray;
            string[] strarrRow1Array;
            string[] strarrRow2Array;


            string strRetVal, strCurName, strType, strTemp;
            ArrayList arrListOfFile = new ArrayList();
            arrListOfFile = this.GetValueFromFile(pstrCSTFilePath);
            //-- Rohit: To create item block----------      
            strTemp = arrListOfFile[0].ToString().Replace(Convert.ToChar(34), Convert.ToChar(" "));
            char[] cSep = { ',' };
            strarrRow1Array = strTemp.Split(cSep);

            strTemp = arrListOfFile[1].ToString().Replace(Convert.ToChar(34), Convert.ToChar(" "));
            strarrRow2Array = strTemp.Split(cSep);
            //-------------------------------

            for (int cnt = 0; cnt <= strarrRow2Array.Length - 1; cnt++)
            {
                XmlNode xmlnodeDestNode = null;
                XmlNode xmlnodeItemNode = null;
                XmlNode xmlnodeTempNode = null;
                try
                {
                    // if (strarrRowArray.Length > 1)
                    {
                        if (strarrRow2Array[cnt].ToLower().Trim().CompareTo("root") == 0)
                        {
                            if (strarrRow1Array[cnt].ToLower().Trim().CompareTo("block") == 0)
                            {
                                //xmlnodeTempNode = pxmldocManifestXmlObj.SelectSingleNode("/manifest/organizations/organization");
                                //xLib.fCreateFirstContext(xmlnodeTempNode, "/manifest/organizations/organization", ref xmlnodeDestNode);
                            }
                        }
                        else
                        {
                            xmlnodeTempNode = pxmldocManifestXmlObj.SelectSingleNode("/manifest/organizations/organization");
                            xLib.fCreateFirstContext(xmlnodeTempNode, "/manifest/organizations/organization", ref xmlnodeDestNode);

                            //xmlnodeTempNode = pxmldocManifestXmlObj.SelectSingleNode("/manifest/organizations/organization//item[@identifier='" + strarrRow2Array[cnt] + "']");
                            //xLib.fCreateFirstContext(xmlnodeTempNode, "/manifest/organizations/organization//item[@identifier='" + strarrRow2Array[cnt] + "']", ref  xmlnodeDestNode);


                            strCurName = Convert.ToString(strarrRow2Array[cnt]);
                            strType = "au";
                            xLib.aCreateNode("item", ref xmlnodeItemNode);
                            if (strType == "au")
                            {
                                xLib.fSetAttribute(ref xmlnodeItemNode, "resourceref", "RESOURCE" + strCurName.Trim());
                            }

                            xLib.fSetAttribute(ref xmlnodeItemNode, "identifier", strCurName.Trim());
                            xLib.fSetAttribute(ref xmlnodeItemNode, "type", strType);

                            XmlNode xmlnodeNewNode = null;
                            string strParseer = "//root/item[@id='" + strCurName.ToString().Trim() + "']";

                            strRetVal = pxmldocDESXMLObj.DocumentElement.SelectSingleNode(strParseer + "/system_id").InnerText;
                            xLib.aCreateNode("system_id", ref xmlnodeNewNode);
                            xLib.fAddCdataToNode(ref xmlnodeNewNode, Convert.ToString(strRetVal));
                            xLib.fAppendNode(ref xmlnodeItemNode, ref xmlnodeNewNode);

                            xmlnodeNewNode = null;
                            strRetVal = pxmldocDESXMLObj.DocumentElement.SelectSingleNode(strParseer + "/title").InnerText;
                            xLib.aCreateNode("title", ref xmlnodeNewNode);
                            xLib.fAddCdataToNode(ref xmlnodeNewNode, Convert.ToString(strRetVal));
                            xLib.fAppendNode(ref xmlnodeItemNode, ref xmlnodeNewNode);

                            xmlnodeNewNode = null;
                            strRetVal = pxmldocDESXMLObj.DocumentElement.SelectSingleNode(strParseer + "/developer_id").InnerText;
                            xLib.aCreateNode("developer_id", ref xmlnodeNewNode);
                            xLib.fAddCdataToNode(ref xmlnodeNewNode, Convert.ToString(strRetVal));
                            xLib.fAppendNode(ref xmlnodeItemNode, ref xmlnodeNewNode);

                            xmlnodeNewNode = null;
                            strRetVal = pxmldocDESXMLObj.DocumentElement.SelectSingleNode(strParseer + "/description").InnerText;
                            xLib.aCreateNode("description", ref xmlnodeNewNode);
                            xLib.fAddCdataToNode(ref xmlnodeNewNode, Convert.ToString(strRetVal));
                            xLib.fAppendNode(ref xmlnodeItemNode, ref xmlnodeNewNode);

                            if (strType == "au")
                            {

                                xmlnodeNewNode = null;
                                strRetVal = pxmldocAUXMLObj.DocumentElement.SelectSingleNode(strParseer + "/command_line").InnerText;
                                xLib.aCreateNode("command_line", ref xmlnodeNewNode);
                                xLib.fAddCdataToNode(ref xmlnodeNewNode, Convert.ToString(strRetVal).Trim());
                                xLib.fAppendNode(ref xmlnodeItemNode, ref xmlnodeNewNode);

                                xmlnodeNewNode = null;
                                strRetVal = pxmldocAUXMLObj.DocumentElement.SelectSingleNode(strParseer + "/type").InnerText;
                                xLib.aCreateNode("type", ref xmlnodeNewNode);
                                xLib.fAddCdataToNode(ref xmlnodeNewNode, Convert.ToString(strRetVal).Trim());
                                xLib.fAppendNode(ref xmlnodeItemNode, ref xmlnodeNewNode);

                                xmlnodeNewNode = null;
                                strRetVal = pxmldocAUXMLObj.DocumentElement.SelectSingleNode(strParseer + "/max_time_allowed").InnerText;
                                xLib.aCreateNode("max_time_allowed", ref xmlnodeNewNode);
                                xLib.fAddCdataToNode(ref xmlnodeNewNode, Convert.ToString(strRetVal).Trim());
                                xLib.fAppendNode(ref xmlnodeItemNode, ref xmlnodeNewNode);

                                xmlnodeNewNode = null;
                                strRetVal = pxmldocAUXMLObj.DocumentElement.SelectSingleNode(strParseer + "/time_limit_action").InnerText;
                                xLib.aCreateNode("time_limit_action", ref xmlnodeNewNode);
                                xLib.fAddCdataToNode(ref xmlnodeNewNode, Convert.ToString(strRetVal).Trim());
                                xLib.fAppendNode(ref xmlnodeItemNode, ref xmlnodeNewNode);

                                xmlnodeNewNode = null;
                                strRetVal = pxmldocAUXMLObj.DocumentElement.SelectSingleNode(strParseer + "/max_score").InnerText;
                                xLib.aCreateNode("max_score", ref xmlnodeNewNode);
                                xLib.fAddCdataToNode(ref xmlnodeNewNode, Convert.ToString(strRetVal).Trim());
                                xLib.fAppendNode(ref xmlnodeItemNode, ref xmlnodeNewNode);

                                xmlnodeNewNode = null;
                                strRetVal = pxmldocAUXMLObj.DocumentElement.SelectSingleNode(strParseer + "/core_vendor").InnerText;
                                strRetVal = strRetVal.Replace("<CR>", "'");
                                xLib.aCreateNode("core_vendor", ref xmlnodeNewNode);
                                xLib.fAddCdataToNode(ref xmlnodeNewNode, Convert.ToString(strRetVal).Trim());
                                xLib.fAppendNode(ref xmlnodeItemNode, ref xmlnodeNewNode);

                                xmlnodeNewNode = null;
                                strRetVal = pxmldocAUXMLObj.DocumentElement.SelectSingleNode(strParseer + "/system_vendor").InnerText;
                                xLib.aCreateNode("system_vendor", ref xmlnodeNewNode);
                                xLib.fAddCdataToNode(ref xmlnodeNewNode, Convert.ToString(strRetVal).Trim());
                                xLib.fAppendNode(ref xmlnodeItemNode, ref xmlnodeNewNode);

                                xmlnodeNewNode = null;
                                strRetVal = pxmldocAUXMLObj.DocumentElement.SelectSingleNode(strParseer + "/mastery_score").InnerText;
                                xLib.aCreateNode("mastery_score", ref xmlnodeNewNode);
                                xLib.fAddCdataToNode(ref xmlnodeNewNode, Convert.ToString(strRetVal).Trim());
                                xLib.fAppendNode(ref xmlnodeItemNode, ref xmlnodeNewNode);

                                xmlnodeNewNode = null;
                                strRetVal = pxmldocAUXMLObj.DocumentElement.SelectSingleNode(strParseer + "/au_password").InnerText;
                                xLib.aCreateNode("au_password", ref xmlnodeNewNode);
                                xLib.fAddCdataToNode(ref xmlnodeNewNode, Convert.ToString(strRetVal).Trim());
                                xLib.fAppendNode(ref xmlnodeItemNode, ref xmlnodeNewNode);
                            }
                            pxmldocManifestXmlObj.SelectSingleNode("/manifest/organizations/organization").AppendChild(pxmldocManifestXmlObj.ImportNode(xmlnodeItemNode, true));
                        }
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// Create Resource Nodes
        /// </summary>
        /// <param name="pxmldocAUXMLObj"></param>
        /// <param name="pxmldocManifestXmlObj"></param>
        public void CreateResourceNodes(ref XmlDocument pxmldocAUXMLObj, ref XmlDocument pxmldocManifestXmlObj)
        {
            XMLLib xLib = new XMLLib();
            XmlNode xmlnodeItemNode = null;
            XmlNodeList xmlnodeListRetNodeList = null;
            string strRetVal = string.Empty;

            xLib.fCreateContext(pxmldocAUXMLObj, "/root/item", ref xmlnodeListRetNodeList);
            foreach (XmlNode xmlnodeTempNode in xmlnodeListRetNodeList)
            {
                xLib.aCreateNode("resource", ref xmlnodeItemNode);
                strRetVal = xLib.fGetValue(xmlnodeTempNode, "system_id");
                xLib.fSetAttribute(ref xmlnodeItemNode, "resourceref", "RESOURCE" + strRetVal.Trim());
                xLib.fSetAttribute(ref xmlnodeItemNode, "identifier", strRetVal.Trim());
                strRetVal = xLib.fGetValue(xmlnodeTempNode, "file_name");
                xLib.fSetAttribute(ref xmlnodeItemNode, "href", strRetVal);
                strRetVal = xLib.fGetValue(xmlnodeTempNode, "web_launch");
                xLib.fSetAttribute(ref xmlnodeItemNode, "web_launch", strRetVal);
                pxmldocManifestXmlObj.SelectSingleNode("/manifest/resources").AppendChild(pxmldocManifestXmlObj.ImportNode(xmlnodeItemNode, true));
            }
        }

        /// <summary>
        /// Convert AICC To Manifest
        /// </summary>
        /// <param name="pCoursePath"></param>
        /// <param name="pTemplateXmlPath"></param>
        /// <returns></returns>
        public bool ConvertAICCToManifest(string pCoursePath, string pTemplateXmlPath)
        {
            bool blnIsReturn = true;
            string strContentFolderPath = string.Empty;
            string strClientId = EncryptionManager.Decrypt("P+jUwjxdIhc="); ; // EncryptionManager.Decrypt(ConfigurationManager.AppSettings[YPLMS.Entity.Client.BASE_CLIENT_KEY]);
            FileHandler fileHandler = new FileHandler(strClientId);
            strContentFolderPath = fileHandler.RootSharedPath;  //EncryptionManager.Decrypt(ConfigurationManager.AppSettings[FileHandler.CONTENT_FOLDER_PATH]);
            strContentFolderPath += pCoursePath.Replace("/", @"\");
            if (strContentFolderPath.IndexOf("\\\\\\") != -1)
            {
                strContentFolderPath = strContentFolderPath.Replace("\\\\", "\\");
            }
            if (!strContentFolderPath.EndsWith(@"\"))
            {
                strContentFolderPath += @"\";
            }
            if (IsAICCFilesExists(strContentFolderPath))
            {
                try
                {
                    XMLLib xLib = new XMLLib();
                    XmlDocument xmldocDESXMLObj = null;
                    XmlDocument xmldocAUXMLObj = null;
                    XmlDocument xmldocManifestXmlObj = null;
                    string strDESFilePath = strContentFolderPath + "\\" + AICC_FILE_NAME + ".des";
                    string strAUFilePath = strContentFolderPath + "\\" + AICC_FILE_NAME + ".au";
                    string strCRSFilePath = strContentFolderPath + "\\" + AICC_FILE_NAME + ".crs";
                    string strCSTFilePath = strContentFolderPath + "\\" + AICC_FILE_NAME + ".cst";

                    if (strDESFilePath.IndexOf("\\\\\\") != -1)
                    {
                        strDESFilePath = strDESFilePath.Replace("\\\\", "\\");
                    }
                    if (strAUFilePath.IndexOf("\\\\\\") != -1)
                    {
                        strAUFilePath = strAUFilePath.Replace("\\\\", "\\");
                    }
                    if (strCRSFilePath.IndexOf("\\\\\\") != -1)
                    {
                        strCRSFilePath = strCRSFilePath.Replace("\\\\", "\\");
                    }
                    if (strCSTFilePath.IndexOf("\\\\\\") != -1)
                    {
                        strCSTFilePath = strCSTFilePath.Replace("\\\\", "\\");
                    }
                    CreateXmlFromFile(strDESFilePath, ref xmldocDESXMLObj);
                    CreateXmlFromFile(strAUFilePath, ref xmldocAUXMLObj);

                    OpenFreeXMLDoc(ref xmldocManifestXmlObj, pTemplateXmlPath);
                    PutValuesFromCRSFile(ref xmldocManifestXmlObj, strCRSFilePath);

                    CreateItemNodes(strCSTFilePath, ref xmldocManifestXmlObj, ref xmldocDESXMLObj, ref xmldocAUXMLObj);
                    CreateResourceNodes(ref xmldocAUXMLObj, ref xmldocManifestXmlObj);
                    xmldocManifestXmlObj.Save(strContentFolderPath + "\\" + "imsmanifest.xml");
                }
                catch (Exception exCommon)
                {
                    CustomException exCustom = new CustomException(YPLMS.Services.Messages.Common.MANIFEST_ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, exCommon, true);
                    blnIsReturn = false;
                }
            }
            return blnIsReturn;
        }
    }
}
