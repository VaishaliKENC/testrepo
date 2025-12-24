/* 
* Copyright Brainvisa Technologies Pvt. Ltd.
* This source file and source code is proprietary property of Brainvisa Technologies Pvt. Ltd. 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Brainvisa’s Client.
* Author: Shailesh Patil
* Created:<24/08/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    ///  class Lookup:BaseEntity 
    /// </summary>
    [Serializable]
    public class ApplicationLog : BaseEntity
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ApplicationLog()
        {
            _strServerName = Environment.MachineName;
            _strLoggedDateTime = DateTime.Now;  
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetApplicationLog,
            AddAll
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,           
            Delete
        }
        
        private string _strServerName;
        /// <summary>
        /// Server Name
        /// </summary>
        public string ServerName
        {
            get { return _strServerName; }
            set { if (this._strServerName != value) { _strServerName = value; } }
        }

    

        private string _strSystemUserGUID;
        /// <summary>
        /// System User GUID
        /// </summary>
        public string SystemUserGUID
        {
            get { return _strSystemUserGUID; }
            set { if (this._strSystemUserGUID != value) { _strSystemUserGUID = value; } }
        }

        private string _strEventID;
        /// <summary>
        /// Event Id
        /// </summary>
        public string EventID
        {
            get { return _strEventID; }
            set { if (this._strEventID != value) { _strEventID = value; } }
        }

        private int _strCategoryID;
        /// <summary>
        /// Category ID
        /// </summary>
        public int CategoryID
        {
            get { return _strCategoryID; }
            set { if (this._strCategoryID != value) { _strCategoryID = value; } }
        }

        private string _strLogInfo;
        /// <summary>
        /// Log Info
        /// </summary>
        public string LogInfo
        {
            get { return _strLogInfo; }
            set { if (this._strLogInfo != value) { _strLogInfo = value; } }
        }

        private string _strLogDescription;
        /// <summary>
        /// Log Description
        /// </summary>
        public string LogDescription
        {
            get { return _strLogDescription; }
            set { if (this._strLogDescription != value) { _strLogDescription = value; } }
        }

        private LogType _logType;
        /// <summary>
        /// Lookup Type
        /// </summary>
        public LogType LogType
        {
            get { return _logType; }
            set { if (this._logType != value) { _logType = value; } }
        }

        private DateTime _strLoggedDateTime;
        /// <summary>
        /// Logged DateTime
        /// </summary>
        public DateTime LoggedDateTime
        {
            get { return _strLoggedDateTime; }
            set { if (this._strLoggedDateTime != value) { _strLoggedDateTime = value; } }
        }

        public bool Validate(bool pIsUpdate)
        {
            if (pIsUpdate)
            {
                if (String.IsNullOrEmpty(ID))
                    return false;
            }
            else
            {
                if (String.IsNullOrEmpty(ServerName))
                    return false;

                if (String.IsNullOrEmpty(this.ClientId))
                    return false;

                if (String.IsNullOrEmpty(SystemUserGUID))
                    return false;

                if (String.IsNullOrEmpty(LogInfo))
                    return false;

                if (String.IsNullOrEmpty(LoggedDateTime.ToString()))
                    return false;
            }
            if (String.IsNullOrEmpty(LastModifiedById))
                return false;
            return true;
        }
    }
    
    /// <summary>
    /// enum LogType
    /// </summary>
    public enum LogType
    {
        Error,
        Exception,
        ImportLog
    }
 
}