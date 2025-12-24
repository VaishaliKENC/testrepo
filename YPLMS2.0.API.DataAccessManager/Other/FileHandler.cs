using ICSharpCode.SharpZipLib.Zip;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager
{
    public class FileHandler
    {
        string strMessageId = YPLMS.Services.Messages.Common.FILE_ERROR;
        CustomException expCustom;
        //Stream streamFtp;
        //FtpWebRequest ftpWebReq;
        FileStream streamFtp;
        public const string ASSETThumbnail = "ASSETThumbnail";
        public const string COURSEThumbnail = "CourseThumbnail";
        public const string TEMP_ZIP_FOLDER_PATH = "TempForUnzip\\";
        public const string CSV_FOLDER_PATH = "CSV";
        public const string ARCHIVE_FOLDER_PATH = "ARCHIVE";
        public const string SITE_IMAGES_PATH = "images";
        public const string DEFAULT_LOGO_FILE_NAME = "DefaultLogo.jpg";

        public const string ASSET_FOLDER_PATH = "ASSET";
        public const string REFDOC_FOLDER_PATH = "REFDOC";
        public const string POLICY_FOLDER_PATH = "POLICY";
        public const string COURSE_FOLDER_PATH = "Courses";
        public const string CLIENTS_FOLDER_PATH = "Clients";
        public const string CLIENTS_OUTLOOK_PATH = "OutlookFiles";
        public const string QUESTIONNAIRE_FOLDER_PATH = "QUESTIONNAIRE";
        public const string ASSESSMENT_FOLDER_PATH = "ASSESSMENT";
        public const string SCAN_FOLDER_PATH = "SCANCOPY";
        public const string Report_FOLDER_PATH = "Reports";
        public const string COURSE_TRANSLATE_FOLDER_PATH = "CourseTranslation";
        public const string CLASSROOM_TRAINING = "ClassroomTraining";
        public const string ILT_BULK_IMPORT = "ILTBulkImport";

        public const string ATTENDENCE_FOLDER_PATH = "Attendence";
        public const string RESOURCE_FOLDER_PATH = "Resources";
        public const string USERPICS_PATH = "UserPics";
        public const string USERDOCS_PATH = "UserDocs";
        public const string CKEDITOR_FOLDER_PATH = "CkEditorImages";
        public const string PRODUCTS_PATH = "Products";
        public const string STORE_BANNER = "StoreBanners";
        public const string USER_DATAXML_PATH = "UserDataXml";
        public const string FEEDBACK_FOLDER_PATH = "FeedbackImages";
        public const string DOCUMENT_FOLDER_PATH = "DocumentLib";
        public const string REFMATERIAL_FOLDER_PATH = "RefMaterial";
        public const string OFFLINE_COURSE_FOLDER_PATH = "OfflineCoursePlayerFiles";
        public const string VIRTUAL_TRAINING = "VirtualTraining";
        public const string FAQ_PATH = "FAQ";
        public const string ASSIGNMENTS_FOLDER_PATH = "ASSIGNMENTS";
        public const string CERTIFICATE_PATH = "Certificate";
        public const string CERTIFICATE_CSS_PATH = "CSS";
        public const string CERTIFICATE_IMAGE_PATH = "Images";
        public const string CERTIFICATE_CSS_FILENAME = "certificate_en-US.css";
        public const string CERTIFICATE_IMAGE_FILENAME = "Certificate_BG2.jpg";
        public const string GRSI_FTP_FOLDER_PATH = "GRSIFTPFiles";
        public const string TESTING_FOLDER_PATH = "Testing";
        public const string TCA_REPORT_FOLDER_PATH = "DumpReports";
        public const string LMS_TRAINING_REPORT_FOLDER_PATH = "LMSTrainingDumpReports";
        public const string ACTIVITY_CERTIFICATE = "ActCertificate";
        //public const string ACTIVITY_CERTIFICATE_BACKGROUND = "BackGroundImages";
        //public const string ACTIVITY_CERTIFICATE_Images = "CertificateImages";
        public const string PRODUCTGROUPS_PATH = "ProductGroup";
        public const string PRODUCTCATEGORY_PATH = "Category";
        public const string HELPDESK_FOLDER_PATH = "HelpdeskImages";

        int iChunkSize = 131072; //128Kb
        int iMaxUploadSize = 0;
        System.Net.NetworkCredential ftpCredential = null;
        private Cluster entCluster;

        /// <summary>
        /// RootFtpPath
        /// </summary>
        ////public string RootFtpPath
        ////{
        ////    get { return EncryptionManager.Decrypt(entCluster.FTPUploadPath); }
        ////    //get { return @"ftp:\\vasudhappc\CoursePlayerContent\"; }
        ////}

        /// <summary>
        /// RootHTTPUrl
        /// </summary>
        public string RootHTTPUrl
        {

            get
            {

                string str = string.Empty;
                try
                {
                    str = ""; // ConfigurationManager.ConnectionStrings["APPSIDE"].ConnectionString; 
                }
                catch
                { }

                if (str.ToLower() == "CoursePlayer".ToLower())
                {
                    if (IsHttpsAllowed(entCluster.ClientId))
                    {
                        string str2 = "http"; //ConfigurationManager.ConnectionStrings["DefaultProtocol"].ConnectionString;
                        entCluster.ContentFolderURL = entCluster.ContentFolderURL.Replace("http:", str2 + ":");
                        return entCluster.ContentFolderURL;
                    }
                    else
                    {
                        entCluster.ContentFolderURL = entCluster.ContentFolderURL.Replace("https:", "http:");
                        return entCluster.ContentFolderURL;
                    }
                }
                else
                    return entCluster.ContentFolderURL;
            }

        }


        public static bool IsHttpsAllowed(string pstrClientId)
        {
            Client entClientHttpsEnabled = new Client();
            entClientHttpsEnabled.ID = pstrClientId;
            entClientHttpsEnabled.ClientId = pstrClientId;

            entClientHttpsEnabled = new ClientDAM().GetHTTPSAllow(entClientHttpsEnabled);
            return entClientHttpsEnabled.IsHTTPSAllowed;

        }

        /// <summary>
        /// RootSharedPath
        /// </summary>
        public string RootSharedPath
        {
            get { return EncryptionManager.Decrypt(entCluster.ContentFolderPath); }
        }

        /// <summary>
        /// FileHandler
        /// </summary>
        /// <param name="pClientId"></param>
        public FileHandler(string pClientId)
        {

            Client entClient = new Client();
            entClient.ID = pClientId;
            try
            {
                entClient = GetClient(pClientId);
                entCluster = entClient.ClientCluster;
                iMaxUploadSize = entClient.MaxFileUploadSizeMB;
            }
            catch (Exception expCommon)
            {
                throw new CustomException(strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expCommon, true);
            }
        }

        /// <summary>
        /// GetNetworkCredential
        /// </summary>
        /// <returns></returns>
        private NetworkCredential GetNetworkCredential()
        {
            ftpCredential = null;
            if (entCluster != null && !string.IsNullOrEmpty(entCluster.FTPUserName) && !string.IsNullOrEmpty(entCluster.FTPPassword))
            {
                if (entCluster.FTPUserName.Contains(@"\"))
                {
                    ftpCredential = new NetworkCredential(entCluster.FTPUserName.Substring(entCluster.FTPUserName.IndexOf(@"\") + 1),
                                                          entCluster.FTPPassword, entCluster.FTPUserName.Substring(0, entCluster.FTPUserName.IndexOf(@"\")));
                }
                else
                {
                    ftpCredential = new NetworkCredential(entCluster.FTPUserName, entCluster.FTPPassword);
                }
            }
            return ftpCredential;
        }

        /// <summary>
        /// FileHandler
        /// </summary>
        /// <param name="pCluster"></param>
        public FileHandler(Cluster pCluster)
        {
            entCluster = pCluster;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            entCluster = null;
        }

        /// <summary>
        /// Get Course Path
        /// </summary>
        /// <param name="pstrCourseId">Course ID</param>
        /// <returns>Complete Ftp Path ftp://***.***.**.**/ftproot/course/courseid </returns>
        public string GetCourseFTPPath(string pstrCoursePath)
        {
            return RootSharedPath + pstrCoursePath;
        }
        public string GetCourseSharedPath(string pstrCoursePath)
        {
            return RootSharedPath + pstrCoursePath;
        }
        /// <summary>
        /// Upload file stream to specified folder 
        /// </summary>
        /// <param name="pstrFtpFolderpath">folder on FTP</param>
        /// <param name="pstrFileName">File Name</param>
        /// <param name="pbLocalFile">File stream</param>
        /// <returns>pysical access path</returns>
        public string Uploadfile(string pstrFtpFolderpath, string pstrFileName, byte[] pbLocalFile)
        {
            string strFullFileName = String.Empty;
            string strUploadFolderPath;
            string strFtpfullpath;
            long lFileSize;
            long lSentBytes = 0;
            iChunkSize = 131072;
            byte[] Buffer = new byte[iChunkSize];
            try
            {
                if (iMaxUploadSize == 0)
                {
                    Client entClient = GetClient(entCluster.ClientId);
                    iMaxUploadSize = entClient.MaxFileUploadSizeMB;
                }
                if (ConvertSize(pbLocalFile.Length, FileSize.MegaBytes) > iMaxUploadSize)
                {
                    throw new CustomException(YPLMS.Services.Messages.Common.FILE_GREATER_MAX, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, null, false);
                }
                if (pstrFtpFolderpath != "" && !pstrFtpFolderpath.EndsWith(@"\\"))
                {
                    pstrFtpFolderpath += @"\\";
                }
                pstrFtpFolderpath = pstrFtpFolderpath.Replace("/", @"\\");
                //   pstrFtpFolderpath = pstrFtpFolderpath.Replace(@"\","");
                strFtpfullpath = RootSharedPath + pstrFtpFolderpath; //CheckForUnicode(pstrFtpFolderpath);// + @"\\";
                pstrFileName = CheckForUnicode(pstrFileName);
                ////Uri urlFtp = new Uri(strFtpfullpath + pstrFileName);
                ////ftpWebReq = (FtpWebRequest)FtpWebRequest.Create(urlFtp);
                //////userid and password for the ftp server to given 
                ////ftpCredential = GetNetworkCredential();
                ////if (ftpCredential != null)
                ////{
                ////    ftpWebReq.Credentials = ftpCredential;
                ////}
                ////ftpWebReq.Proxy = null;
                ////ftpWebReq.KeepAlive = true;
                ////ftpWebReq.UseBinary = true;
                ////ftpWebReq.Method = WebRequestMethods.Ftp.UploadFile;

                ////using (streamFtp = ftpWebReq.GetRequestStream())
                ////{
                ////    lFileSize = pbLocalFile.Length;
                ////    if (iChunkSize > lFileSize)
                ////    {
                ////        iChunkSize = Convert.ToInt32(lFileSize);
                ////    }
                ////    while (lSentBytes < lFileSize)
                ////    {
                ////        streamFtp.Write(pbLocalFile, Convert.ToInt32(lSentBytes), iChunkSize);
                ////        lSentBytes += iChunkSize;
                ////        if ((lFileSize - lSentBytes) < iChunkSize)
                ////        {
                ////            iChunkSize = Convert.ToInt32(lFileSize - lSentBytes);
                ////        }
                ////    }
                ////}

                string path = strFtpfullpath.Replace("\\\\", "/");
                if (File.Exists(path + pstrFileName))
                    File.Delete(path + pstrFileName);

                streamFtp = new FileStream(path + pstrFileName, FileMode.CreateNew, FileAccess.Write);

                //using (streamFtp = fs.GetRequestStream())
                //{
                lFileSize = pbLocalFile.Length;
                if (iChunkSize > lFileSize)
                {
                    iChunkSize = Convert.ToInt32(lFileSize);
                }
                while (lSentBytes < lFileSize)
                {
                    streamFtp.Write(pbLocalFile, Convert.ToInt32(lSentBytes), iChunkSize);
                    lSentBytes += iChunkSize;
                    if ((lFileSize - lSentBytes) < iChunkSize)
                    {
                        iChunkSize = Convert.ToInt32(lFileSize - lSentBytes);
                    }
                }
                //}

                strUploadFolderPath = RootSharedPath;
                pstrFtpFolderpath = pstrFtpFolderpath.Replace("//", "/");
                pstrFtpFolderpath = pstrFtpFolderpath.Replace("/", @"\");
                strUploadFolderPath = strUploadFolderPath.Replace("\\\\", @"\");
                pstrFileName = strUploadFolderPath + pstrFtpFolderpath + pstrFileName;// @"\" + pstrFileName;
            }
            //catch (WebException e)
            //{
            //    String status = ((FtpWebResponse)e.Response).StatusDescription;
            //    WriteLogFile("WebException : " + status);
            //    throw e;
            //}
            catch (CustomException expCust)
            {
                throw expCust;
            }
            catch (Exception expCommon)
            {
                expCustom = new CustomException(strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expCommon, true);
                throw expCustom;
            }
            finally
            {
                if (streamFtp != null)
                {
                    //-- streamFtp.Flush(); and streamFtp = null; Newly added
                    streamFtp.Flush();
                    streamFtp.Close();
                    streamFtp = null;
                }
            }
            return pstrFileName;
        }


        private void WriteLogFile(string strErroLog)
        {
            string _strLogFilePath = "E:\\Log\\";
            string _strLogFileName;
            string _strAppPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\";
            try
            {
                //if (string.IsNullOrEmpty(_strLogFilePath))
                //{
                _strLogFilePath = _strAppPath;
                //}
                _strLogFileName = "ServiceManager" + DateTime.Now.ToString("yyyy-MM-dd").ToString() + ".txt";
                if (!System.IO.Directory.Exists(_strLogFilePath))
                {
                    System.IO.Directory.CreateDirectory(_strLogFilePath);
                }
                System.IO.File.AppendAllText(_strLogFilePath + _strLogFileName, strErroLog);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Upload file stream to specified folder with return Root HTTP Url
        /// </summary>
        /// <param name="pstrFtpFolderpath">folder on FTP</param>
        /// <param name="pstrFileName">File Name</param>
        /// <param name="pbLocalFile">File stream</param>
        /// <returns>pysical access path</returns>
        public string Uploadfile(string pstrFtpFolderpath, string pstrFileName, byte[] pbLocalFile, bool bReturnPath)
        {
            string strFullFileName = String.Empty;
            string strUploadFolderPath;
            string strFtpfullpath;
            long lFileSize;
            long lSentBytes = 0;
            string _pstrFtpFolderpath = pstrFtpFolderpath;
            string _pstrFileName = pstrFileName;

            iChunkSize = 131072;
            byte[] Buffer = new byte[iChunkSize];
            try
            {
                if (iMaxUploadSize == 0)
                {
                    Client entClient = GetClient(entCluster.ClientId);
                    iMaxUploadSize = entClient.MaxFileUploadSizeMB;
                }
                if (ConvertSize(pbLocalFile.Length, FileSize.MegaBytes) > iMaxUploadSize)
                {
                    throw new CustomException(YPLMS.Services.Messages.Common.FILE_GREATER_MAX, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Information, null, false);
                }
                if (pstrFtpFolderpath != "" && !pstrFtpFolderpath.EndsWith(@"\\"))
                {
                    pstrFtpFolderpath += @"\\";
                }
                pstrFtpFolderpath = pstrFtpFolderpath.Replace("/", @"\\");
                //   pstrFtpFolderpath = pstrFtpFolderpath.Replace(@"\","");
                strFtpfullpath = RootSharedPath + pstrFtpFolderpath; //CheckForUnicode(pstrFtpFolderpath);// + @"\\";
                pstrFileName = CheckForUnicode(pstrFileName);

                ////Uri urlFtp = new Uri(strFtpfullpath + pstrFileName);
                ////ftpWebReq = (FtpWebRequest)FtpWebRequest.Create(urlFtp);
                //////userid and password for the ftp server to given 
                ////ftpCredential = GetNetworkCredential();
                ////if (ftpCredential != null)
                ////{
                ////    ftpWebReq.Credentials = ftpCredential;
                ////}
                ////ftpWebReq.Proxy = null;
                ////ftpWebReq.KeepAlive = true;
                ////ftpWebReq.UseBinary = true;
                ////ftpWebReq.Method = WebRequestMethods.Ftp.UploadFile;

                ////using (streamFtp = ftpWebReq.GetRequestStream())
                ////{
                ////    lFileSize = pbLocalFile.Length;
                ////    if (iChunkSize > lFileSize)
                ////    {
                ////        iChunkSize = Convert.ToInt32(lFileSize);
                ////    }
                ////    while (lSentBytes < lFileSize)
                ////    {
                ////        streamFtp.Write(pbLocalFile, Convert.ToInt32(lSentBytes), iChunkSize);
                ////        lSentBytes += iChunkSize;
                ////        if ((lFileSize - lSentBytes) < iChunkSize)
                ////        {
                ////            iChunkSize = Convert.ToInt32(lFileSize - lSentBytes);
                ////        }
                ////    }
                ////}

                if (File.Exists(strFtpfullpath + pstrFileName))
                    File.Delete(strFtpfullpath + pstrFileName);

                streamFtp = new FileStream(strFtpfullpath + pstrFileName, FileMode.OpenOrCreate, FileAccess.Write);

                //using (streamFtp = fs.GetRequestStream())
                //{
                lFileSize = pbLocalFile.Length;
                if (iChunkSize > lFileSize)
                {
                    iChunkSize = Convert.ToInt32(lFileSize);
                }
                while (lSentBytes < lFileSize)
                {
                    streamFtp.Write(pbLocalFile, Convert.ToInt32(lSentBytes), iChunkSize);
                    lSentBytes += iChunkSize;
                    if ((lFileSize - lSentBytes) < iChunkSize)
                    {
                        iChunkSize = Convert.ToInt32(lFileSize - lSentBytes);
                    }
                }


                if (bReturnPath)
                    pstrFileName = RootHTTPUrl + _pstrFtpFolderpath + "//" + _pstrFileName;
                else
                {
                    strUploadFolderPath = RootSharedPath;
                    pstrFtpFolderpath = pstrFtpFolderpath.Replace("//", "/");
                    pstrFtpFolderpath = pstrFtpFolderpath.Replace("/", @"\");
                    strUploadFolderPath = strUploadFolderPath.Replace("\\\\", @"\");
                    pstrFileName = strUploadFolderPath + pstrFtpFolderpath + pstrFileName;
                }


            }
            catch (CustomException expCust)
            {
                throw expCust;
            }
            catch (Exception expCommon)
            {
                expCustom = new CustomException(strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expCommon, true);
                throw expCustom;
            }
            finally
            {
                if (streamFtp != null)
                {
                    //-- streamFtp.Flush(); and streamFtp = null; Newly added
                    streamFtp.Flush();
                    streamFtp.Close();
                    streamFtp = null;
                }
            }
            return pstrFileName;
        }

        /// <summary>
        /// Copy File from Source to destination
        /// </summary>
        /// <param name="pSource">Source Path</param>
        /// <param name="pDestination">Destination Path</param>
        /// <returns>Copied Path</returns>
        public string CopyFile(string pSource, string pDestination)
        {
            try
            {
                string strContentFolderPath = RootSharedPath;
                pSource = strContentFolderPath + pSource.Replace("/", @"\");
                pDestination = strContentFolderPath + pDestination.Replace("/", @"\");
                File.Copy(pSource, pDestination, true);
            }
            catch (Exception expCommon)
            {
                expCustom = new CustomException(strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expCommon, true);
                throw expCustom;
            }
            return pDestination;
        }

        /// <summary>
        /// Delete File
        /// </summary>
        /// <param name="strFtpfullpath">Full Path</param>
        /// <returns></returns>
        public bool DeleteFileOnServer(string strFtpfullpath)
        {
            return PerformFolderAction("", strFtpfullpath, WebRequestMethods.Ftp.DeleteFile);
        }

        /// <summary>
        /// Create folder on specified path
        /// </summary>
        /// <param name="strFtpFolderpath">folder path on ftp</param>
        /// <param name="sDirName">new folder name</param>
        /// <returns>true if success</returns>
        public bool CreateFolder(string strFtpFolderpath, string sDirName)
        {
            return PerformFolderAction(strFtpFolderpath, sDirName, WebRequestMethods.Ftp.MakeDirectory);
        }

        /// <summary>
        /// Copy folder Content to destination Folder
        /// </summary>
        /// <param name="pSource">Source Path</param>
        /// <param name="pDestination">Destination Folder Path</param>
        /// <returns>true if success</returns>
        public bool CopyFolderContent(string pSourceFolder, string pDestinationFolder)
        {
            try
            {
                string strContentFolderPath = RootSharedPath; // EncryptionManager.Decrypt(ConfigurationManager.AppSettings[CONTENT_FOLDER_PATH]);
                pSourceFolder = strContentFolderPath + pSourceFolder.Replace("/", @"\");
                pDestinationFolder = strContentFolderPath + pDestinationFolder.Replace("/", @"\");

                //Copy all the files
                System.IO.FileInfo[] fiA = (new System.IO.DirectoryInfo(pSourceFolder).GetFiles());
                foreach (System.IO.FileInfo fi in fiA)
                {
                    fi.CopyTo(pDestinationFolder + "\\" + fi.Name);
                }
                //Recursively fill the child directories
                System.IO.DirectoryInfo[] diA = (new System.IO.DirectoryInfo(pSourceFolder).GetDirectories());
                foreach (System.IO.DirectoryInfo di in diA)
                {
                    CopyFolder(new object[] { di.FullName, pDestinationFolder, false });
                }
            }
            catch (Exception expCommon)
            {
                expCustom = new CustomException(strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expCommon, false);
                //throw expCustom;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get Directories of Source folder
        /// </summary>
        /// <param name="pSourcePath">if blank get root structure</param>
        /// <returns></returns>
        public DirectoryInfo[] GetAllFolders(string pSourcePath)
        {
            DirectoryInfo[] dirInfo = null;
            try
            {
                string strContentFolderPath = RootSharedPath;
                if (!string.IsNullOrEmpty(pSourcePath))
                {
                    pSourcePath = strContentFolderPath + pSourcePath.Replace("/", @"\");
                }
                else
                {
                    pSourcePath = strContentFolderPath;
                }
                dirInfo = new DirectoryInfo(pSourcePath).GetDirectories();
            }
            catch (Exception expCommon)
            {
                expCustom = new CustomException(strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expCommon, false);
            }
            return dirInfo;
        }

        /// <summary>
        /// Get Files of Source folder
        /// </summary>
        /// <param name="pSourcePath">if blank get root structure</param>
        /// <returns></returns>
        public FileInfo[] GetAllFiles(string pSourcePath, string pSearchPattern)
        {
            FileInfo[] dirInfo = null;
            try
            {
                string strContentFolderPath = RootSharedPath;
                if (!string.IsNullOrEmpty(pSourcePath))
                {
                    pSourcePath = strContentFolderPath + pSourcePath.Replace("/", @"\");
                }
                else
                {
                    pSourcePath = strContentFolderPath;
                }
                if (string.IsNullOrEmpty(pSearchPattern))
                {
                    dirInfo = new DirectoryInfo(pSourcePath).GetFiles();
                }
                else
                {
                    dirInfo = new DirectoryInfo(pSourcePath).GetFiles(pSearchPattern);
                }
            }
            catch (Exception expCommon)
            {
                expCustom = new CustomException(strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expCommon, false);
            }
            return dirInfo;
        }

        /// <summary>
        /// Returns FTP path of given shared path
        /// </summary>
        /// <param name="pSharedPath"></param>
        /// <returns>Ftp Path</returns>
        public string GetFTPPath(string pSharedPath)
        {
            try
            {
                string strContentFolderPath = RootSharedPath;// EncryptionManager.Decrypt(ConfigurationManager.AppSettings[CONTENT_FOLDER_PATH]);
                strContentFolderPath = strContentFolderPath.Replace("\\\\", @"\");
                pSharedPath = pSharedPath.Replace(strContentFolderPath, RootSharedPath);
                pSharedPath = pSharedPath.Replace(@"\", "/");
            }
            catch (Exception expCommon)
            {
                expCustom = new CustomException(strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expCommon, false);
                pSharedPath = string.Empty;
            }
            return pSharedPath;
        }

        /// <summary>
        /// Recursively Copies source folders and files to the destination folder.
        /// </summary>
        /// <param name="sourceFolderPath">The folder to be copied.</param>
        /// <param name="destinationFolderPath">The folder to where the folder to be copied will be copied.</param>
        /// <param name="overwrite">Overwrite existing files and folders that exist.</param>
        private void CopyFolder(object o)
        {
            string sourceFolderPath = "Source Not Found";
            string destinationFolderPath = "Destination Not Found";
            bool overwrite = false;
            sourceFolderPath = ((string)(((object[])o)[0]));
            destinationFolderPath = ((string)(((object[])o)[1]));
            overwrite = ((bool)(((object[])o)[2]));
            if (System.IO.Directory.Exists(sourceFolderPath))
            {
                if (System.IO.Directory.Exists(destinationFolderPath + "\\" + (new System.IO.DirectoryInfo(sourceFolderPath).Name)) && (!overwrite))
                    throw new System.Exception("Sorry, but the folder " + destinationFolderPath + " already exists.");
                else if (!System.IO.Directory.Exists(destinationFolderPath + "\\" + (new System.IO.DirectoryInfo(sourceFolderPath).Name)))
                    System.IO.Directory.CreateDirectory(destinationFolderPath + "\\" + (new System.IO.DirectoryInfo(sourceFolderPath).Name));

                //Copy all the files
                System.IO.FileInfo[] fiA = (new System.IO.DirectoryInfo(sourceFolderPath).GetFiles());
                foreach (System.IO.FileInfo fi in fiA)
                {
                    fi.CopyTo(destinationFolderPath + "\\" + (new System.IO.DirectoryInfo(sourceFolderPath).Name) + "\\" + fi.Name);
                }
                //Recursively fill the child directories
                System.IO.DirectoryInfo[] diA = (new System.IO.DirectoryInfo(sourceFolderPath).GetDirectories());
                foreach (System.IO.DirectoryInfo di in diA)
                {
                    CopyFolder(new object[] { di.FullName, destinationFolderPath + "\\" + (new System.IO.DirectoryInfo(sourceFolderPath).Name), overwrite });
                }
            }
        }

        /// <summary>
        /// Remove folder on specified path
        /// </summary>
        /// <param name="strFtpFolderpath">folder path on ftp</param>
        /// <param name="sDirName">folder name</param>
        /// <returns>true if success</returns>
        public bool RemoveFolder(string pFtpFolderpath)
        {
            string strContentFolderPath = "";
            try
            {
                strContentFolderPath = RootSharedPath.Replace("\\\\", "\\");// EncryptionManager.Decrypt(ConfigurationManager.AppSettings[CONTENT_FOLDER_PATH]).Replace("\\\\", "\\");
                pFtpFolderpath = pFtpFolderpath.Replace("/", @"\");
                strContentFolderPath += pFtpFolderpath;
                if (Directory.Exists(strContentFolderPath))
                {
                    Directory.Delete(strContentFolderPath, true);
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Rename folder on specified path
        /// </summary>
        /// <param name="strFtpFolderpath">folder path on ftp</param>
        /// <param name="sDirName">new folder name</param>
        /// <returns>true if success</returns>
        private bool PerformFolderAction(string pFtpFolderpath, string pDirName, string pAction)
        {
            // FtpWebResponse ftpWebResponse = null;
            FileStream streamFtp = null;
            string strFtpfullpath;
            bool returnValue = false;
            string urlFtp;
            try
            {
                strFtpfullpath = RootSharedPath + CheckForUnicode(pFtpFolderpath);
                if (pAction != WebRequestMethods.Ftp.Rename)
                {
                    if (!strFtpfullpath.EndsWith("/") && pDirName.Length > 0 && !pDirName.StartsWith("/"))
                    {
                        pDirName = "/" + pDirName;
                    }
                    pDirName = CheckForUnicode(pDirName);
                    urlFtp = strFtpfullpath + pDirName;
                }
                else
                {
                    urlFtp = strFtpfullpath;
                }
                ////ftpWebReq = (FtpWebRequest)FtpWebRequest.Create(urlFtp);
                //////userid and password for the ftp server to given 
                ////ftpCredential = GetNetworkCredential();
                ////if (ftpCredential != null)
                ////{
                ////    ftpWebReq.Credentials = ftpCredential;
                ////}
                ////ftpWebReq.Proxy = null;
                ////ftpWebReq.UseBinary = true;
                ////ftpWebReq.Method = pAction;
                ////if (pAction == WebRequestMethods.Ftp.Rename)
                ////{
                ////    ftpWebReq.RenameTo = pDirName;
                ////}
                //////ftpWebResponse = (FtpWebResponse)ftpWebReq.GetResponse();
                ////try
                ////{
                ////    ftpWebResponse = (FtpWebResponse)ftpWebReq.GetResponse();
                ////}
                ////catch
                ////{
                ////    returnValue = false;
                ////}
                ////if (ftpWebResponse != null)
                ////{
                ////    streamFtp = ftpWebResponse.GetResponseStream();
                ////    returnValue = true;
                ////}
                string path = strFtpfullpath.Replace("\\\\", "/");
                
                if (!Directory.Exists(path + pDirName))
                {
                    if (pAction == WebRequestMethods.Ftp.DeleteFile)
                    {
                        if (File.Exists(path + pDirName))
                            File.Delete(path + pDirName);
                    }
                    else if (pAction != WebRequestMethods.Ftp.Rename)
                    {
                        Directory.CreateDirectory(path + pDirName);
                    }
                    else
                    {
                        Directory.Move(path, urlFtp);
                    }
                    returnValue = true;
                }
                else
                {
                    returnValue = false;
                }
            }
            catch (Exception expCommon)
            {
                expCustom = new CustomException(strMessageId, CustomException.WhoCallsMe() + "--" + pFtpFolderpath + "#" + pDirName + "$" + pAction, ExceptionSeverityLevel.Fatal, expCommon, true);
                returnValue = false;
            }
            finally
            {
                //if (ftpWebResponse != null)
                //{
                //    ftpWebResponse.Close();
                //    ftpWebResponse = null;
                //}
                //if (streamFtp != null)
                //{
                //    streamFtp.Close();
                //    streamFtp = null;
                //}
            }
            return returnValue;
        }

        /// <summary>
        /// Check For Special Characters 
        /// </summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public static string CheckForUnicode(string pPath)
        {
            return pPath;
            //return System.Web.HttpUtility.UrlEncode(pPath).Replace("+", " ");
        }

        /// <summary>
        /// This funtion will unzip the zip uploaded files
        /// </summary>
        /// <param name="pstrFileName"></param>
        /// <returns></returns>
        /// 
        public bool UnZipServerFilesForScorm2004(string pstrFileName, string pstrCourseId)
        {

            bool bIsReturn = false;
            string strContentFolderPath = string.Empty;
            string strUploadZipContentFolderPath = string.Empty;
            long lSentBytes = 0;
            string strNewDirFilePath;
            try
            {
                string strFileExtension = Path.GetExtension(pstrFileName);
                if (strFileExtension.ToUpper() == ".ZIP")
                {
                    strContentFolderPath = RootSharedPath + pstrCourseId;
                    strUploadZipContentFolderPath = RootSharedPath;
                    pstrFileName = strUploadZipContentFolderPath + pstrFileName;
                    if (strContentFolderPath.IndexOf("\\\\\\") != -1)
                    {
                        strContentFolderPath = RootSharedPath.Replace("\\\\", "\\") + pstrCourseId;
                    }
                    //Creating Main folder directory
                    if (!Directory.Exists(strContentFolderPath))
                    {
                        Directory.CreateDirectory(strContentFolderPath);
                    }
                    using (ZipInputStream s = new ZipInputStream(File.OpenRead(pstrFileName)))
                    {
                        ZipEntry theEntry;
                        while ((theEntry = s.GetNextEntry()) != null)
                        {
                            //Internal directory (if .zip contains the dir)
                            string directoryName = Path.GetDirectoryName(theEntry.Name);
                            string fileName = Path.GetFileName(theEntry.Name);
                            if (directoryName.Length > 0)
                            {
                                string strNewDirPath = strContentFolderPath + "\\" + directoryName;
                                Directory.CreateDirectory(strNewDirPath);
                            }
                            if (fileName != String.Empty)
                            {
                                strNewDirFilePath = strContentFolderPath + "\\" + theEntry.Name;
                                using (FileStream streamWriter = File.Create(strNewDirFilePath))
                                {
                                    int size = 2048;
                                    byte[] data = new byte[2048];
                                    while (true)
                                    {
                                        size = s.Read(data, 0, data.Length);
                                        if (size > 0)
                                        {
                                            streamWriter.Write(data, 0, size);
                                            lSentBytes += size;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //get the Questions js files 

                    string[] QuestionFilesPath = Directory.GetFiles(@strContentFolderPath, "questions.js", SearchOption.AllDirectories);

                   // HttpContext.Current.Session.Add("QuestionFilesPath", QuestionFilesPath);
                    bIsReturn = true;
                    //-- Delete the Zip file from server
                    File.Delete(pstrFileName);
                }
            }
            catch (Exception exCommon)
            {
                bIsReturn = false;
                expCustom = new CustomException(strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, exCommon, true);
                throw expCustom;
            }
            return bIsReturn;
        }

        public bool UnZipServerFiles(string pstrFileName, string pstrCourseId)
        {
            bool bIsReturn = false;
            string strContentFolderPath = string.Empty;
            string strUploadZipContentFolderPath = string.Empty;
            long lSentBytes = 0;
            string strNewDirFilePath;
            try
            {
                string strFileExtension = Path.GetExtension(pstrFileName);
                if (strFileExtension.ToUpper() == ".ZIP")
                {
                    strContentFolderPath = RootSharedPath + pstrCourseId;
                    strUploadZipContentFolderPath = RootSharedPath;
                    pstrFileName = strUploadZipContentFolderPath + pstrFileName;
                    if (strContentFolderPath.IndexOf("\\\\\\") != -1)
                    {
                        strContentFolderPath = RootSharedPath.Replace("\\\\", "\\") + pstrCourseId;
                    }
                    //strContentFolderPath = strContentFolderPath.Replace("\\\\", "/");
                    //Creating Main folder directory
                    if (!Directory.Exists(strContentFolderPath))
                    {
                        Directory.CreateDirectory(strContentFolderPath);
                    }
                    pstrFileName = pstrFileName.Replace("\\\\", "/");
                    using (ZipInputStream s = new ZipInputStream(File.OpenRead(pstrFileName)))
                    {
                        ZipEntry theEntry;
                        while ((theEntry = s.GetNextEntry()) != null)
                        {
                            //Internal directory (if .zip contains the dir)
                            string directoryName = Path.GetDirectoryName(theEntry.Name);
                            string fileName = Path.GetFileName(theEntry.Name);
                            if (directoryName.Length > 0)
                            {
                                string strNewDirPath = strContentFolderPath + "\\" + directoryName;
                                Directory.CreateDirectory(strNewDirPath);
                            }
                            if (fileName != String.Empty)
                            {
                                strNewDirFilePath = strContentFolderPath + "\\" + theEntry.Name;
                                using (FileStream streamWriter = File.Create(strNewDirFilePath))
                                {
                                    int size = 2048;
                                    byte[] data = new byte[2048];
                                    while (true)
                                    {
                                        size = s.Read(data, 0, data.Length);
                                        if (size > 0)
                                        {
                                            streamWriter.Write(data, 0, size);
                                            lSentBytes += size;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    bIsReturn = true;
                    //-- Delete the Zip file from server
                    File.Delete(pstrFileName);
                }
            }
            catch (Exception exCommon)
            {
                bIsReturn = false;
                expCustom = new CustomException(strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, exCommon, true);
                //throw expCustom;
            }
            return bIsReturn;
        }


        /// <summary>
        /// This funtion will unzip the zip uploaded files
        /// </summary>
        /// <param name="pstrFileName"></param>
        /// <returns></returns>
        public bool UnZipServerFilesForMobileLMS(string pstrFileName, string pstrCourseId)
        {
            bool bIsReturn = false;
            string strContentFolderPath = string.Empty;
            string strUploadZipContentFolderPath = string.Empty;
            long lSentBytes = 0;
            string strNewDirFilePath;
            try
            {
                string strFileExtension = Path.GetExtension(pstrFileName);
                if (strFileExtension.ToUpper() == ".ZIP")
                {
                    strContentFolderPath = RootSharedPath + pstrCourseId;
                    strUploadZipContentFolderPath = RootSharedPath;
                    pstrFileName = strUploadZipContentFolderPath + pstrFileName;
                    if (strContentFolderPath.IndexOf("\\\\\\") != -1)
                    {
                        strContentFolderPath = RootSharedPath.Replace("\\\\", "\\") + pstrCourseId;
                    }
                    //Creating Main folder directory
                    if (!Directory.Exists(strContentFolderPath))
                    {
                        Directory.CreateDirectory(strContentFolderPath);
                    }
                    pstrFileName = pstrFileName.Replace("\\\\", "/");
                    using (ZipInputStream s = new ZipInputStream(File.OpenRead(pstrFileName)))
                    {
                        ZipEntry theEntry;
                        while ((theEntry = s.GetNextEntry()) != null)
                        {
                            //Internal directory (if .zip contains the dir)
                            string directoryName = Path.GetDirectoryName(theEntry.Name);
                            string fileName = Path.GetFileName(theEntry.Name);
                            if (directoryName.Length > 0)
                            {
                                string strNewDirPath = strContentFolderPath + "\\" + directoryName;
                                Directory.CreateDirectory(strNewDirPath);
                            }
                            if (fileName != String.Empty)
                            {
                                strNewDirFilePath = strContentFolderPath + "\\" + theEntry.Name;
                                using (FileStream streamWriter = File.Create(strNewDirFilePath))
                                {
                                    int size = 2048;
                                    byte[] data = new byte[2048];
                                    while (true)
                                    {
                                        size = s.Read(data, 0, data.Length);
                                        if (size > 0)
                                        {
                                            streamWriter.Write(data, 0, size);
                                            lSentBytes += size;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    bIsReturn = true;
                    //-- Delete the Zip file from server
                    // File.Delete(pstrFileName);
                }
            }
            catch (Exception exCommon)
            {
                bIsReturn = false;
                expCustom = new CustomException(strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, exCommon, true);
                throw expCustom;
            }
            return bIsReturn;
        }


        /// <summary>
        /// Check for invalid file extension, returns auto generated file name  
        /// </summary>
        /// <param name="pFileName">Original file name</param>
        /// <returns>auto generated file name</returns>
        public string ValidateFile(string pFileName)
        {
            string strFileName = String.Empty;
            if (!String.IsNullOrEmpty(pFileName) &&
                !pFileName.ToLower().EndsWith(".exe") &&
                !pFileName.ToLower().EndsWith(".dll"))
            {
                strFileName += IDGenerator.GetStringGUID();
                if (pFileName.LastIndexOf(".") > 0)
                {
                    strFileName += pFileName.Substring(pFileName.LastIndexOf(".") - 1);
                }
            }
            return strFileName;
        }

        /// <summary>
        /// Check if folder exist in folder
        /// </summary>
        /// <param name="strFtpFolderpath">send "" if on root</param>
        /// <param name="sDirName">directory to check</param>
        /// <returns>true if exists</returns>
        public bool IsFolderExist(string strFtpFolderpath, string sDirName)
        {
            //FtpWebResponse ftpWebResponse = null;
            bool bretValue = false;
            string strFtpfullpath;
            Uri urlFtp;

            try
            {
                if (strFtpFolderpath.Length > 1 && (strFtpFolderpath.StartsWith("/")))
                {
                    strFtpFolderpath = strFtpFolderpath.Remove(0, 1);
                }
                strFtpfullpath = RootSharedPath + CheckForUnicode(strFtpFolderpath);
                //by Shriihari FTP 7.5
                if (sDirName.Length > 0 && !(sDirName.EndsWith("/")))
                {
                    sDirName += "/";
                }
                ///////

                if (sDirName.Length > 0 && !(sDirName.StartsWith("/")))
                {
                    sDirName = "/" + CheckForUnicode(sDirName);
                }

                ////  urlFtp = new Uri(strFtpfullpath + sDirName);
                ////ftpWebReq = (FtpWebRequest)FtpWebRequest.Create(urlFtp);
                //////userid and password for the ftp server to given 
                ////ftpCredential = GetNetworkCredential();
                ////if (ftpCredential != null)
                ////{
                ////    ftpWebReq.Credentials = ftpCredential;
                ////}
                ////ftpWebReq.Proxy = null;
                ////ftpWebReq.UseBinary = true;
                ////ftpWebReq.Method = WebRequestMethods.Ftp.ListDirectory;
                ////try
                ////{
                ////    ftpWebResponse = (FtpWebResponse)ftpWebReq.GetResponse();
                ////}
                ////catch
                ////{
                ////    bretValue = false;
                ////}
                ////if (ftpWebResponse != null)
                ////{
                ////    try
                ////    {
                ////        ftpWebResponse.Close();
                ////        bretValue = true;
                ////    }
                ////    catch
                ////    {
                ////        bretValue = false;
                ////    }
                ////}
                string path = RootSharedPath.Replace("\\\\", "/") + strFtpFolderpath + sDirName;
                path = path.Replace('\\', '/');
                if (Directory.Exists(path))
                    bretValue = true;
                else
                    bretValue = false;
            }
            catch (Exception expCommon)
            {
                expCustom = new CustomException(strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expCommon, true);
                throw expCustom;
            }
            return bretValue;
        }

        /// <summary>
        /// Converts Byte Content Length to Type - MB,KB,GB depending upon the parameters supplied
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int ConvertSize(int pBytes, FileHandler.FileSize pType)
        {
            int CONVERSION_VALUE = 1024;
            double returnValue = 0;
            switch (pType)
            {
                case FileSize.Bytes:
                    return pBytes;
                case FileSize.KiloBytes:
                    try
                    {
                        returnValue = pBytes / CONVERSION_VALUE;
                    }
                    catch
                    {
                        returnValue = 0;
                    }
                    break;
                case FileSize.MegaBytes:
                    try
                    {
                        returnValue = (pBytes / CalculateSquare(CONVERSION_VALUE));
                    }
                    catch
                    {
                        returnValue = 0;
                    }
                    break;
                case FileSize.GigaBytes:
                    try
                    {
                        returnValue = (pBytes / CalculateCube(CONVERSION_VALUE));
                    }
                    catch
                    {
                        returnValue = 0;
                    }
                    break;
            }
            return Convert.ToInt32(returnValue);
        }

        /// <summary>

        /// Function to calculate the square of the provided number

        /// </summary>

        /// <param name="number">Int32 -> Number to be squared</param>

        /// <returns>Double -> THe provided number squared</returns>

        /// <remarks></remarks>
        private double CalculateSquare(Int32 number)
        {
            return Math.Pow(number, 2);
        }

        /// <summary>

        /// Function to calculate the cube of the provided number

        /// </summary>

        /// <param name="number">Int32 -> Number to be cubed</param>

        /// <returns>Double -> THe provided number cubed</returns>

        /// <remarks></remarks>
        private double CalculateCube(Int32 number)
        {
            return Math.Pow(number, 3);
        }

        /// <summary>
        /// Enum FileSize
        /// </summary>
        public enum FileSize
        {
            Bytes,
            KiloBytes,
            MegaBytes,
            GigaBytes
        }

        private static void AddToCache(Client pClient)
        {
            if (LMSCache.IS_IN_USE)
            {
                if (!LMSCache.IsInCache(pClient.ID))
                {
                    LMSCache.AddCacheItem(pClient.ID, pClient, pClient.ID);
                }
                //To add Feature List in Cache       
                if (!LMSCache.IsInCache(pClient.ID + AdminFeatures.CACHE_SUFFIX))
                {
                    AddFeaturesToCache(pClient.ID);
                }
                //To add Feature List in Cache       
                if (!LMSCache.IsInCache(pClient.ID + SystemMessage.CACHE_SUFFIX))
                {
                    AddMessagesToCache(pClient.ID);
                }
            }
        }

        /// <summary>
        /// Add Messages to Cache
        /// </summary>
        /// <param name="ClientId"></param>
        public static void AddMessagesToCache(string pClientId)
        {
            SystemMessage entMessage = new SystemMessage();
            SystemMessageAdaptor adaptorMessage = new SystemMessageAdaptor();
            SystemMessageCache entCMessage;
            List<SystemMessageCache> entListCMessage = new List<SystemMessageCache>();
            entMessage.ClientId = pClientId;
            try
            {
                List<SystemMessage> entListMessage = adaptorMessage.GetMessageList(entMessage);
                foreach (SystemMessage message in entListMessage)
                {
                    entCMessage = new SystemMessageCache();
                    entCMessage.ID = message.ID;
                    entCMessage.LanguageId = message.LanguageId;
                    entCMessage.MessageText = message.MessageText;
                    entListCMessage.Add(entCMessage);
                }
                if (LMSCache.IsInCache(pClientId + SystemMessage.CACHE_SUFFIX))
                {
                    LMSCache.RemoveCacheItems(pClientId);
                }
                LMSCache.AddCacheItem(pClientId + SystemMessage.CACHE_SUFFIX, entListCMessage, pClientId);
            }
            catch (Exception expCommon)
            {
                throw new CustomException(YPLMS.Services.Messages.Common.ERROR, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expCommon, true);
            }
        }

        /// <summary>
        /// Add Features To Cache
        /// </summary>
        /// <param name="pClientId"></param>
        public static void AddFeaturesToCache(string pClientId)
        {
            List<AdminFeatures> entListAdminFeature = null;
            AdminFeatures entAdminFeature = new AdminFeatures();
            entAdminFeature.ClientId = pClientId;
            AdminFeaturesAdaptor adaptorAdminFeature = new AdminFeaturesAdaptor();
            entListAdminFeature = adaptorAdminFeature.GetFeaturesList(entAdminFeature);
            if (entListAdminFeature != null)
            {
                LMSCache.AddCacheItem(pClientId + AdminFeatures.CACHE_SUFFIX, entListAdminFeature, pClientId);
            }
        }

        public static Client GetClient(string pClientId)
        {
            Client entClient = null;
            if (!String.IsNullOrEmpty(pClientId) && LMSCache.IsInCache(pClientId))
            {
                entClient = (Client)LMSCache.GetValue(pClientId);
            }
            if (entClient == null)
            {
                entClient = new ClientDAM().GetClientByID(new Client { ID = pClientId });
                if (entClient != null && !String.IsNullOrEmpty(entClient.ID))
                {
                    AddToCache(entClient);
                }
                else
                {
                    throw new CustomException(YPLMS.Services.Messages.Client.INVALID_CLIENT_ID, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Critical, null, true);
                }
            }
            return entClient;
        }

        public bool CopyFolderContentNew(string pSourceFolder, string pDestinationFolder)
        {
            try
            {
                string strContentFolderPath = RootSharedPath; // EncryptionManager.Decrypt(ConfigurationManager.AppSettings[CONTENT_FOLDER_PATH]);
                pSourceFolder = strContentFolderPath + pSourceFolder.Replace("/", @"\");
                pDestinationFolder = strContentFolderPath + pDestinationFolder.Replace("/", @"\");

                //Copy all the files
                System.IO.FileInfo[] fiA = (new System.IO.DirectoryInfo(pSourceFolder).GetFiles());
                foreach (System.IO.FileInfo fi in fiA)
                {
                    fi.CopyTo(pDestinationFolder + "\\" + fi.Name);
                }
                //Recursively fill the child directories
                System.IO.DirectoryInfo[] diA = (new System.IO.DirectoryInfo(pSourceFolder).GetDirectories());
                foreach (System.IO.DirectoryInfo di in diA)
                {
                    //if (di.Name.ToLower() == "certificate" || di.Name.ToLower() == "images" || di.Name.ToLower() == "publicdocs")
                    //{
                    CopyFolder(new object[] { di.FullName, pDestinationFolder, true });
                    //}
                }
            }
            catch (Exception expCommon)
            {
                expCustom = new CustomException(strMessageId, CustomException.WhoCallsMe(), ExceptionSeverityLevel.Fatal, expCommon, false);
                //throw expCustom;
                return false;
            }
            return true;
        }

        public static string GetOutlookAssignmentfile(int i, List<ActivityAssignment> plistActivityAssignment, string _strClientId, string _strEmailSubject)
        {
            string CLIENTS_OUTLOOK_PATH = "OutlookFiles";
            string attachmentFilename = string.Empty;

            //_strEmailSubject = _strEmailSubject + "_AssignmentDate_";
            _strEmailSubject = "Activity_Assignment_" + YPLMS.Services.IDGenerator.GetStringGUID();
            StringBuilder str = new StringBuilder();


            str.AppendLine("BEGIN:VCALENDAR");

            //str.AppendLine("VERSION:2.0");
            //str.AppendLine("METHOD:REQUEST");
            str.AppendLine("PRODID:-//Flo Inc.//FloSoft//EN");
            str.AppendLine("BEGIN:VEVENT");
            str.AppendLine("UID:1");
            if (i >= 0)
                i = 1;
            if (plistActivityAssignment[i - 1].AssignmentDateSet.ToString() != "01/01/0001 00:00:00")
            {
                str.AppendLine(string.Format("DTSTART:{0:yyyyMMdd}", plistActivityAssignment[i - 1].AssignmentDateSet.ToString("yyyyMMdd")) + "T043000Z");
                str.AppendLine(string.Format("DTEND:{0:yyyyMMdd}", plistActivityAssignment[i - 1].AssignmentDateSet.ToString("yyyyMMdd")) + "T043000Z");

                //str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", plistActivityAssignment[i - 1].AssignmentDateSet.ToString("yyyyMMdd") + "T100000Z"));
            }
            ////if (plistActivityAssignment[i - 1].DueDateSet.ToString() != "01/01/0001 00:00:00")
            ////    str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", plistActivityAssignment[i - 1].DueDateSet.ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z")));
            //str.AppendLine("SUMMARY:"+ plistActivityAssignment[i - 1].ActivityName + "(Activity Assignment- reminder)");
            str.AppendLine("SUMMARY: Activity Assignment");
            string strActivityNames = string.Empty;
            if (plistActivityAssignment != null && plistActivityAssignment.Count > 0)
            {
                for (int iCount = 0; iCount < plistActivityAssignment.Count; iCount++)
                {
                    if (iCount == 0)
                        strActivityNames = "Activity: ";

                    strActivityNames = strActivityNames + plistActivityAssignment[iCount].ActivityName + ",";
                }

                if (!string.IsNullOrEmpty(strActivityNames))
                {
                    strActivityNames = strActivityNames.Substring(0, strActivityNames.LastIndexOf(","));

                }
            }

            //str.AppendLine("LOCATION:" + plistActivityAssignment[i - 1].ActivityName);
            str.AppendLine("LOCATION:" + strActivityNames);
            str.AppendLine("PRIORITY:3");
            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");
            var fileHandler = new FileHandler(_strClientId);
            string rootSharedPath = fileHandler.RootSharedPath;

            string strContentImagePath = rootSharedPath + FileHandler.CLIENTS_FOLDER_PATH + "\\" + _strClientId + "\\" + CLIENTS_OUTLOOK_PATH + "\\";

            if (plistActivityAssignment[i - 1].AssignmentDateSet.ToString() != "01/01/0001 00:00:00")
                attachmentFilename = strContentImagePath + "\\" + _strEmailSubject + ".ics";
            else
                attachmentFilename = strContentImagePath + "\\" + _strEmailSubject + ".ics";
            
            attachmentFilename = attachmentFilename.Replace("\\\\", @"\\").Replace(@"\\", @"\"); 
           
           StreamWriter objStrWriter = new StreamWriter(attachmentFilename, true);
            objStrWriter.WriteLine(str);
            objStrWriter.Flush();
            objStrWriter.Dispose();
            objStrWriter.Close();
            Thread.Sleep(1000);
            return attachmentFilename;

        }

        public static string GetOutlookDueDatefile(int i, List<ActivityAssignment> plistActivityAssignment, string _strClientId, string _strEmailSubject)
        {
            string CLIENTS_OUTLOOK_PATH = "OutlookFiles";
            string attachmentFilename = string.Empty;
            _strEmailSubject = "Activity_Assignment_DueDate_" + YPLMS.Services.IDGenerator.GetStringGUID();
            StringBuilder str = new StringBuilder();
            str.AppendLine("BEGIN:VCALENDAR");

            str.AppendLine("PRODID:-//Flo Inc.//FloSoft//EN");
            str.AppendLine("BEGIN:VEVENT");
            str.AppendLine("UID:1");
            if (i >= 0)
                i = 1;
            if (plistActivityAssignment[i - 1].DueDateSet.ToString() != "01/01/0001 00:00:00")
            {
                // str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", plistActivityAssignment[i - 1].DueDateSet.ToString("yyyyMMdd\\THHmmss\\Z")));
                //if (plistActivityAssignment[i - 1].DueDateSet.ToString() != "01/01/0001 00:00:00")
                str.AppendLine(string.Format("DTSTART:{0:yyyyMMdd}", plistActivityAssignment[i - 1].DueDateSet.ToString("yyyyMMdd")) + "T043000Z"); //T173000Z
                str.AppendLine(string.Format("DTEND:{0:yyyyMMdd}", plistActivityAssignment[i - 1].DueDateSet.ToString("yyyyMMdd")) + "T043000Z");   //T173000Z
                //str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", plistActivityAssignment[i - 1].DueDateSet.ToString("yyyyMMdd")+ "T230000Z"));
            }
            str.AppendLine("SUMMARY: Activity Assignment Due Date");

            string strActivityNames = string.Empty;
            if (plistActivityAssignment != null && plistActivityAssignment.Count > 0)
            {
                for (int iCount = 0; iCount < plistActivityAssignment.Count; iCount++)
                {
                    if (iCount == 0)
                        strActivityNames = "Activity: ";

                    strActivityNames = strActivityNames + plistActivityAssignment[iCount].ActivityName + ",";
                }

                if (!string.IsNullOrEmpty(strActivityNames))
                {
                    strActivityNames = strActivityNames.Substring(0, strActivityNames.LastIndexOf(","));

                }
            }
            str.AppendLine("LOCATION:" + strActivityNames);
            //str.AppendLine("LOCATION:" + plistActivityAssignment[i - 1].ActivityName);// objApptEmail.Location);
            str.AppendLine("PRIORITY:3");
            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");
            var fileHandler = new FileHandler(_strClientId);
            string rootSharedPath = fileHandler.RootSharedPath;

            string strContentImagePath = rootSharedPath + FileHandler.CLIENTS_FOLDER_PATH + "\\" + _strClientId + "\\" + CLIENTS_OUTLOOK_PATH + "\\";
            if (plistActivityAssignment[i - 1].DueDateSet.ToString() != "01/01/0001 00:00:00")
                attachmentFilename = strContentImagePath + "\\" + _strEmailSubject + ".ics";
            else
                attachmentFilename = strContentImagePath + "\\" + _strEmailSubject + ".ics";

            //string path = @"\\192.168.90.23\YPLMSContent\Clients\CLIYPRevamp_New\OutlookFiles\Activity_Assignment_DueDate_c080d3d5fa61bb8d.ics";
            //attachmentFilename = path;
            attachmentFilename = attachmentFilename.Replace("\\\\", @"\\").Replace(@"\\", @"\");
            StreamWriter objStrWriter = new StreamWriter(attachmentFilename, true);
            objStrWriter.WriteLine(str);
            objStrWriter.Flush();
            objStrWriter.Dispose();
            objStrWriter.Close();
            Thread.Sleep(1000);
            return attachmentFilename;
        }
        public static string GetOutlookClassRoomFromDatefile(DateTime SessionStartDate, string _strClientId, string strSessionName, string strLocationName)
        {
            string CLIENTS_OUTLOOK_PATH = "OutlookFiles";
            StringBuilder str = new StringBuilder();
            str.AppendLine("BEGIN:VCALENDAR");

            str.AppendLine("PRODID:-//Flo Inc.//FloSoft//EN");
            str.AppendLine("BEGIN:VEVENT");

            if (SessionStartDate.ToString() != "01/01/0001 00:00:00")
            {
                str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmss}", SessionStartDate.ToString("yyyyMMdd\\THHmmss")));
                str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmss}", SessionStartDate.ToString("yyyyMMdd\\THHmmss")));
                //str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", SessionStartDate.ToString("yyyyMMdd\\THHmmss\\Z")));
                //str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", SessionStartDate.ToString("yyyyMMdd\\THHmmss\\Z")));

            }
            ////if (plistActivityAssignment[i - 1].DueDateSet.ToString() != "01/01/0001 00:00:00")
            ////    str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", plistActivityAssignment[i - 1].DueDateSet.ToUniversalTime().ToString("yyyyMMdd\\THHmmss\\Z")));

            str.AppendLine("SUMMARY: Class Room Training- Session Start Date");
            str.AppendLine("LOCATION:" + strLocationName);// objApptEmail.Location);
            str.AppendLine("PRIORITY:3");

            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");
            var fileHandler = new FileHandler(_strClientId);
            string rootSharedPath = fileHandler.RootSharedPath;
            strSessionName = "ClassRoom_SessionStartDate_" + YPLMS.Services.IDGenerator.GetStringGUID();
            string strContentImagePath = rootSharedPath + FileHandler.CLIENTS_FOLDER_PATH + "\\" + _strClientId + "\\" + CLIENTS_OUTLOOK_PATH + "\\";
            string attachmentFilename = strContentImagePath + "\\" + strSessionName + ".ics";
            StreamWriter objStrWriter = new StreamWriter(attachmentFilename, true);
            objStrWriter.WriteLine(str);
            objStrWriter.Flush();
            objStrWriter.Dispose();
            objStrWriter.Close();
            Thread.Sleep(1000);
            return attachmentFilename;
        }
        public static string GetOutlookClassRoomToDatefile(DateTime SessionToDate, string _strClientId, string strSessionName, string strLocationName)
        {
            string CLIENTS_OUTLOOK_PATH = "OutlookFiles";
            StringBuilder str = new StringBuilder();
            str.AppendLine("BEGIN:VCALENDAR");

            str.AppendLine("PRODID:-//Flo Inc.//FloSoft//EN");
            str.AppendLine("BEGIN:VEVENT");

            if (SessionToDate.ToString() != "01/01/0001 00:00:00")
            {
                str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmss}", SessionToDate.ToString("yyyyMMdd\\THHmmss")));
                str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmss}", SessionToDate.ToString("yyyyMMdd\\THHmmss")));
            }
            str.AppendLine("SUMMARY: Class Room Training- Session End Date");
            str.AppendLine("LOCATION:" + strLocationName);// objApptEmail.Location);
            str.AppendLine("PRIORITY:3");

            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");
            var fileHandler = new FileHandler(_strClientId);
            string rootSharedPath = fileHandler.RootSharedPath;
            strSessionName = "ClassRoom_SessionEndDate_" + YPLMS.Services.IDGenerator.GetStringGUID();

            string strContentImagePath = rootSharedPath + FileHandler.CLIENTS_FOLDER_PATH + "\\" + _strClientId + "\\" + CLIENTS_OUTLOOK_PATH + "\\";
            string attachmentFilename = strContentImagePath + "\\" + strSessionName + ".ics";
            StreamWriter objStrWriter = new StreamWriter(attachmentFilename, true);
            objStrWriter.WriteLine(str);
            objStrWriter.Flush();
            objStrWriter.Dispose();
            objStrWriter.Close();
            Thread.Sleep(1000);
            return attachmentFilename;
        }

        //Get Outlook reminder cancel assignment
        public static string CancelOutlookAssignment(int i, List<ActivityAssignment> plistActivityAssignment, string _strClientId, string _strEmailSubject)
        {
            string CLIENTS_OUTLOOK_PATH = "OutlookFiles";
            string attachmentFilename = string.Empty;

            _strEmailSubject = "Activity_Cancel_" + YPLMS.Services.IDGenerator.GetStringGUID();
            StringBuilder str = new StringBuilder();

            str.AppendLine("BEGIN:VCALENDAR");
            str.AppendLine("PRODID:-//Flo Inc.//FloSoft//EN");
            str.AppendLine("METHOD:CANCEL");
            str.AppendLine("BEGIN:VEVENT");
            str.AppendLine("UID:1");
            str.AppendLine("SEQUENCE:1");
            if (i == 0)
                i = 1;
            if (plistActivityAssignment[i - 1].AssignmentDateSet.ToString() != "01/01/0001 00:00:00")
            {
                str.AppendLine(string.Format("DTSTART:{0:yyyyMMdd}", plistActivityAssignment[i - 1].AssignmentDateSet.ToString("yyyyMMdd")));
            }
            str.AppendLine("STATUS:CANCELLED");
            str.AppendLine("SUMMARY: Activity Assignment");

            str.AppendLine("PRIORITY:3");

            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");
            var fileHandler = new FileHandler(_strClientId);
            string rootSharedPath = fileHandler.RootSharedPath;

            string strContentImagePath = rootSharedPath + FileHandler.CLIENTS_FOLDER_PATH + "\\" + _strClientId + "\\" + CLIENTS_OUTLOOK_PATH + "\\";

            if (plistActivityAssignment[i - 1].AssignmentDateSet.ToString() != "01/01/0001 00:00:00")
                attachmentFilename = strContentImagePath + "\\" + _strEmailSubject + ".ics";
            else
                attachmentFilename = strContentImagePath + "\\" + _strEmailSubject + ".ics";

            StreamWriter objStrWriter = new StreamWriter(attachmentFilename, true);
            objStrWriter.WriteLine(str);
            objStrWriter.Flush();
            objStrWriter.Dispose();
            objStrWriter.Close();
            Thread.Sleep(1000);
            return attachmentFilename;
        }
        ////Get Outlook reminder cancel due date assignment
        public static string CancelOutlookDueDate(int i, List<ActivityAssignment> plistActivityAssignment, string _strClientId, string _strEmailSubject)
        {
            string CLIENTS_OUTLOOK_PATH = "OutlookFiles";
            string attachmentFilename = string.Empty;
            _strEmailSubject = "Activity_Cancel_DueDate_" + YPLMS.Services.IDGenerator.GetStringGUID();
            StringBuilder str = new StringBuilder();
            str.AppendLine("BEGIN:VCALENDAR");

            str.AppendLine("PRODID:-//Flo Inc.//FloSoft//EN");
            str.AppendLine("METHOD:CANCEL");
            str.AppendLine("BEGIN:VEVENT");
            str.AppendLine("UID:1");
            str.AppendLine("SEQUENCE:1");
            if (i == 0)
                i = 1;
            if (plistActivityAssignment[i - 1].DueDateSet.ToString() != "01/01/0001 00:00:00")
            {
                str.AppendLine(string.Format("DTSTART:{0:yyyyMMdd}", plistActivityAssignment[i - 1].DueDateSet.ToString("yyyyMMdd")));
            }
            str.AppendLine("STATUS:CANCELLED");
            str.AppendLine("SUMMARY: Activity Assignment Due Date");

            str.AppendLine("PRIORITY:3");
            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");
            var fileHandler = new FileHandler(_strClientId);
            string rootSharedPath = fileHandler.RootSharedPath;

            string strContentImagePath = rootSharedPath + FileHandler.CLIENTS_FOLDER_PATH + "\\" + _strClientId + "\\" + CLIENTS_OUTLOOK_PATH + "\\";
            if (plistActivityAssignment[i - 1].DueDateSet.ToString() != "01/01/0001 00:00:00")
                attachmentFilename = strContentImagePath + "\\" + _strEmailSubject + ".ics";
            else
                attachmentFilename = strContentImagePath + "\\" + _strEmailSubject + ".ics";
            StreamWriter objStrWriter = new StreamWriter(attachmentFilename, true);
            objStrWriter.WriteLine(str);
            objStrWriter.Flush();
            objStrWriter.Dispose();
            objStrWriter.Close();
            Thread.Sleep(1000);
            return attachmentFilename;
        }
    }
}
