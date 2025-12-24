/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author: Ashish Phate
* Created:<19/09/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    ///  class Task 
    /// </summary>
    [Serializable]
     public class TaskRunHistory : BaseEntity
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public TaskRunHistory()
        {
           
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Add
        }
        
        private string _strTaskId;
        /// <summary>
        /// Task Id
        /// </summary>
        public string TaskId
        {
            get { return _strTaskId; }
            set { if (this._strTaskId != value) { _strTaskId = value; } }
        }

     

        private bool _bExecutionStatus;
        /// <summary>
        /// Execution Status
        /// </summary>
        public bool ExecutionStatus
        {
            get { return _bExecutionStatus; }
            set { if (this._bExecutionStatus != value) { _bExecutionStatus = value; } }
        }

        private DateTime _dateRunStartTime;
        /// <summary>
        /// Task RunStartTime
        /// </summary>
        public DateTime RunStartTime
        {
            get { return _dateRunStartTime; }
            set { if (this._dateRunStartTime != value) { _dateRunStartTime = value; } }
        }

        private DateTime _dateRunEndTime;
        /// <summary>
        /// Task RunEndTime
        /// </summary>
        public DateTime RunEndTime
        {
            get { return _dateRunEndTime; }
            set { if (this._dateRunEndTime != value) { _dateRunEndTime = value; } }
        }


        private string _strGeneratedReportFileName;
        /// <summary>
        /// GeneratedReportFileName
        /// </summary>
        public string GeneratedReportFileName
        {
            get { return _strGeneratedReportFileName; }
            set { if (this._strGeneratedReportFileName != value) { _strGeneratedReportFileName = value; } }
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

                if (String.IsNullOrEmpty(this.ClientId))
                    return false;

            }
            if (String.IsNullOrEmpty(LastModifiedById))
                return false;
            return true;
        }
    }
 
}