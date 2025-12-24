using NLog;
using System;
using System.Collections.Generic;
using System.Threading;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.EmailService
{
    class ClsServiceManager
    {
        #region Variables
        static Logger _logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Properties

        /// <summary>
        /// Client object 
        /// Client object 
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// Time interval to fetch records before current time
        /// </summary>
        public double TimeIntervalFetchRecords { get; set; }

        /// <summary>
        /// Time interval to fetch records after current time
        /// </summary>
        public double TimeIntervalFetchRecordsAfterCurrentTime { get; set; }

        #endregion

        #region Methods
        /// <summary>
        /// Execute service task
        /// </summary>
        public virtual void ExecuteTask() { }
        /// <summary>
        /// Execute Email Task method
        /// </summary>
        public void ExecuteEmailTask(List<ClsServiceManager> pTaskList)
        {
            ExecuteTasks(pTaskList);
        }

        /// <summary>
        /// Execute all tasks
        /// </summary>
        /// <param name="tasks"></param>
        private void ExecuteTasks(List<ClsServiceManager> tasks)
        {
            _logger.Debug("Queue threads from ThreadPool for executing Tasks");
            foreach (ClsServiceManager task in tasks)
            {
                //TODO: remove threadpool and use dedicated threads running in foreground.
                //Use a waithandle for child threads to signal main thread.
                WaitCallback waitCallBack = ExecuteScheduledTask;
                ThreadPool.QueueUserWorkItem(waitCallBack, task);
            }
        }

        /// <summary>
        /// Execute each scheduled task
        /// </summary>
        /// <param name="obj"></param>
        private void ExecuteScheduledTask(object obj)
        {
            ClsServiceManager task;
            try
            {
                task = (ClsServiceManager)obj;
                task.ExecuteTask();
            }
            catch (Exception ex)
            {
                _logger.Fatal(ex);
            }
        }

        #endregion

    }
}
