using NLog;
using System;
using System.Collections.Generic;
using System.Threading;
using YPLMS2._0.API.DataAccessManager;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;
using TaskStatus = YPLMS2._0.API.Entity.TaskStatus;

namespace YPLMS2._0.API.EmailService
{
    class ClsMailManager : ClsServiceManager
    {
        #region Variables
        static Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        #endregion

        #region Properties

        /// <summary>
        /// EmailDeliveryDashboard object 
        /// </summary>
        public List<EmailDeliveryDashboard> EmailDeliveryDashboard { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Method to execute Mail task
        /// </summary>
        public override void ExecuteTask()
        {
            try
            {
                if (EmailDeliveryDashboard != null && EmailDeliveryDashboard.Count > 0)
                {
                    foreach (EmailDeliveryDashboard emaildelivery in EmailDeliveryDashboard)
                    {
                        _logger.Debug(String.Format("Email Delivery started for {0}", emaildelivery.ClientId));
                        MasterTaskJobAdaptor adaptorTask = new MasterTaskJobAdaptor(); 
                        List<MasterTaskJob> cMasterTaskJobList = new List<MasterTaskJob>();
                        MasterTaskJob cMasterTaskJob = new MasterTaskJob();
                        cMasterTaskJob.ID = emaildelivery.ID;
                        cMasterTaskJob.ClientId = Common.BaseClientID;
                        cMasterTaskJob.Status = TaskStatus.Closed;
                        cMasterTaskJobList.Add(cMasterTaskJob);
                        //cMasterTaskJobMgr.Execute(cMasterTaskJobList, MasterTaskJob.ListMethod.BulkUpdateStatus);
                        adaptorTask.BulkUpdate(cMasterTaskJobList);
                       YPLMS2._0.API.DataAccessManager.EmailService  emailmessage = new YPLMS2._0.API.DataAccessManager.EmailService(emaildelivery.ClientId);
                        try
                        {
                            emailmessage.SendScheduledMail(emaildelivery);
                        }
                        catch (Exception exc)
                        {
                            _logger.FatalException("Error on : SendScheduledMail", exc);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                _logger.Fatal(exc);
            }
        }

        #endregion

    }
}
