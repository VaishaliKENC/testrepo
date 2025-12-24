using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.DataAccessManager.Tracking;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager
{
    public interface IBackgrounBRuleAssignmentManagerFactory
    {
        IBackgroundBRuleAssignmentManager Create(string activityId, string systemUserGuid, string activityType, string clientId);
    }
    public class BackgrounBRuleAssignmentManagerFactory : IBackgrounBRuleAssignmentManagerFactory
    {
        public IBackgroundBRuleAssignmentManager Create(string activityId, string systemUserGuid, string activityType, string clientId)
        {
            return new BackgroundBRuleAssignmentManager(activityType, systemUserGuid, activityType, clientId);
        }
    }

    public interface IBackgroundBRuleAssignmentManager
    {
        void AssignActivties();
    }

    public class BackgroundBRuleAssignmentManager : IBackgroundBRuleAssignmentManager, IManager<BackgroundBRuleAssignment, BackgroundBRuleAssignment.Method, BackgroundBRuleAssignment.ListMethod>
    {

        BackgroundBRuleAssignment entBackgroundBRuleAssignment;


        /// <summary>
        /// Default BackgroundBRuleAssignmentManager Constructor
        /// </summary>
        public BackgroundBRuleAssignmentManager(string pstrActivityId, string pSysteMUserGuId, string pActivityType, string pClientID)
        {
            this.ActivityId = pstrActivityId;
            this.SysteMUserGuId = pSysteMUserGuId;
            this.ActivityType = pActivityType;
            this.ClientId = pClientID;

        }

        public void AssignActivties()
        {
            // run threadless or threaded depending on feature flipper
            Entity.Configuration.ClientFeatureConfiguration cfg = new API.DataAccessManager.Configuration.ClientFeatureConfigurationDAM().GetForClient(this.ClientId);
            if (cfg.IsFeatureEnabled(API.Entity.Configuration.ClientFeatureConfiguration.ThreadlessAssignment))
            {
                RunAssignActivities();
            }
            else
            {
                var entThread = new Thread(new ThreadStart(this.RunAssignActivities));
                entThread.IsBackground = true;
                entThread.Start();
            }
        }

        private void RunAssignActivities()
        {
            try
            {
                entBackgroundBRuleAssignment = new BackgroundBRuleAssignment(this.ActivityId, this.SysteMUserGuId, this.ActivityType);
                entBackgroundBRuleAssignment.ClientId = this.ClientId;
                BackgroundBRuleAssignmentDAM _entDam = new BackgroundBRuleAssignmentDAM();
                entBackgroundBRuleAssignment = _entDam.PerformDynamicAssignment(entBackgroundBRuleAssignment);
            }
            catch
            {
                // swallow exception for now
            }
        }


        private string _ClientId;

        public string ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; }
        }


        private string _ActivityId;

        public string ActivityId
        {
            get { return _ActivityId; }
            set { _ActivityId = value; }
        }
        private string _SysteMUserGuId;

        public string SysteMUserGuId
        {
            get { return _SysteMUserGuId; }
            set { _SysteMUserGuId = value; }
        }
        private string _ActivityType;

        public string ActivityType
        {
            get { return _ActivityType; }
            set { _ActivityType = value; }
        }

        /// <summary>
        /// Execute: GetAll
        /// </summary>
        /// <param name="pEntBackgroundBRuleAssignment"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<BackgroundBRuleAssignment> Execute(BackgroundBRuleAssignment pEntBackgroundBRuleAssignment, BackgroundBRuleAssignment.ListMethod pMethod)
        {

            return null;
        }

        /// <summary>
        /// Execute: Get/Add
        /// </summary>
        /// <param name="pEntBackgroundBRuleAssignment"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public BackgroundBRuleAssignment Execute(BackgroundBRuleAssignment pEntBackgroundBRuleAssignment, BackgroundBRuleAssignment.Method pMethod)
        {
            return null;
        }


        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="BackgroundBRuleAssignment"></typeparam>
        /// <param name="pEntBase">BackgroundBRuleAssignment object</param>
        /// <param name="pMethod">BackgroundBRuleAssignment.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(BackgroundBRuleAssignment pEntBase, BackgroundBRuleAssignment.ListMethod pMethod)
        {

            return null;

        }
    }
}
