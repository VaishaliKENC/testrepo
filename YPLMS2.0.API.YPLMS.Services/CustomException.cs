using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.YPLMS.Services
{
    [Serializable]
    public class CustomException : System.Exception
    {
        #region Private Variables
        // defines the severity level of the Exception
        private ExceptionSeverityLevel _severityLevelOfException = ExceptionSeverityLevel.Information;
        // System Exception that is thrown 
        private Exception _expInnerException;
        private bool _pWriteLogTemp = true;
        private bool _pWriteLog = true;
        private bool _inSerialization = false;
        //Exception Id
        private string _strMessageId = Messages.Common.ERROR;
        private string _pEventName;
        private string _stackTrace = string.Empty;

        #endregion

        #region Constructor
        // This constructor is needed for serialization.
        protected CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _pEventName = info == null ? getEventName(1) : info.GetString("EventName");
            //ProcessException(base.GetBaseException());
        }
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            //if (info != null)
            //{
            info.AddValue("EventName", PEventName);
            //}
        }
        /// <summary>
        /// Initiate CustomException
        /// </summary>
        /// <param name="pMessageId">Messaged Id in Configuration</param>
        /// <param name="pEventName" >Event Name for Event where error occured</param>
        /// <param name="pSeverityLevel">Severity Level</param>
        /// <param name="pException">Original Exception</param>
        /// <param name="pWriteLog">Whether to log the Original Exception</param>
        public CustomException(string pMessageId, string pEventName, ExceptionSeverityLevel pSeverityLevel, Exception pException, bool pWriteLog)
        {
            _severityLevelOfException = pSeverityLevel;
            _pEventName = pEventName;
            _expInnerException = pException;
            _pWriteLog = pWriteLog;
            _strMessageId = pMessageId;
            //ProcessException(pException,"");
        }

        public CustomException(string pMessageId, string pEventName, ExceptionSeverityLevel pSeverityLevel, Exception pException, bool pWriteLog,string pClientConnectionString)
        {
            _severityLevelOfException = pSeverityLevel;
            _pEventName = pEventName;
            _expInnerException = pException;
            _pWriteLog = pWriteLog;
            _strMessageId = pMessageId;
            ProcessException(pException, pClientConnectionString);
        }


        #endregion
        public string MessageId
        {
            get
            {
                return _strMessageId;
            }
        }
        public override string StackTrace
        {
            get
            {
                if (InnerException == null)
                {
                    _stackTrace = base.StackTrace;
                }
                else
                {
                    _stackTrace = InnerException.StackTrace;
                }
                return _stackTrace;
            }
        }

        /// <summary>
        /// Default Message
        /// </summary>
        public override string Message
        {
            get
            {
                if (_expInnerException != null)
                    return this._expInnerException.Message;
                else
                    return this.MessageId;
            }
        }
        public string PEventName
        {
            get { return _pEventName; }
        }
        #region Methods
        private static string getEventName(Int16 stackFrameIndex)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace();
            return trace.GetFrame(stackFrameIndex + 1).GetMethod().Name;
        }
        /// <summary>
        /// Get the name of the function Where this function is used 
        /// </summary>
        /// <returns></returns>
        public static string WhoCallsMe()
        {
            return getEventName(1);
        }

        #endregion

        [OnSerializing]
        private void TurnOffLoggingOnSerialization(StreamingContext context)
        {
            _pWriteLogTemp = _pWriteLog;
            _inSerialization = true;
        }

        [OnDeserialized]
        private void ResetLoggingOnSerialization(StreamingContext context)
        {
            _pWriteLog = _pWriteLogTemp;
            _inSerialization = false;
        }

        private void ProcessException(Exception pException, string pClientConnectionString)
        {
            //To Check double log
            //if (_pWriteLog && pException != null && pException.GetType() == typeof(CustomException))
            //{
            //    _pWriteLog = false;
            //}

            if (_inSerialization)
            {
                _pWriteLog = false;
            }
            if (_pWriteLog)
            {
                //if (HttpContext.Current != null)
                //{
                //    if (HttpContext.Current.Session[Learner.USER_SESSION_ID] != null)
                //    {
                //        object objSession = HttpContext.Current.Session[Learner.USER_SESSION_ID];
                //        if (objSession.GetType() == typeof(Learner))
                //        {
                //            Learner learnerCurrent = (Learner)objSession;
                //            ExceptionManager.LoginID = learnerCurrent.ID;
                //        }
                //        else
                //        {
                //            ExceptionManager.LoginID = objSession.ToString();
                //        }
                //    }
                //    if (HttpContext.Current.Session[Client.CLIENT_SESSION_ID] != null)
                //    {
                //        ExceptionManager.ClientID = Convert.ToString(HttpContext.Current.Session[Client.CLIENT_SESSION_ID]);
                //    }
                //}
                ExceptionManager.WriteError(pException, PEventName, Convert.ToInt16(_severityLevelOfException), _strMessageId, pClientConnectionString);
            }
        }
    }
    /// <summary>
    /// Severity level of Exception
    /// </summary>
    public enum ExceptionSeverityLevel
    {
        Fatal = 0,
        Critical = 1,
        Information = 2
    }
}
