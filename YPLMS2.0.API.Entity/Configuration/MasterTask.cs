/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author: Ashish Phate
* Created:<05/03/10>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    ///  class Task 
    /// </summary>
    [Serializable]
   public class MasterTaskJob : BaseEntity
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MasterTaskJob()
        {
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            BulkUpdateStatus,
            GetAllOpenTaskList
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {         
            UpdateStatus
        }

        private TaskType _strType;
        /// <summary>
        /// Task Name
        /// </summary>
        public TaskType Type
        {
            get { return _strType; }
            set { if (this._strType != value) { _strType = value; } }
        }

        private TaskStatus _strStatus;
        /// <summary>
        /// Status
        /// </summary>
        public TaskStatus Status
        {
            get { return _strStatus; }
            set { if (this._strStatus != value) { _strStatus = value; } }
        }

        private DateTime _dtScheduledDateTime;
        /// <summary>
        /// ScheduledDateTime
        /// </summary>
        public DateTime ScheduledDateTime
        {
            get { return _dtScheduledDateTime; }
            set { _dtScheduledDateTime = value; }
        }

    }
    public enum TaskType
    {
        Import,
        Report,
        User,
        Assignment,
        UnAssignment,
        MarkComplete,
        Email,
        None
    }
    public enum TaskStatus
    {
        Open,
        InProgress,
        Closed
    }
}