using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;
using YPLMS2._0.API.YPLMS.Services;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager
{
    public class AssignmentManager : IManager<YPLMS2._0.API.Entity.Assignment, YPLMS2._0.API.Entity.Assignment.Method, YPLMS2._0.API.Entity.Assignment.ListMethod>
    {
        public AssignmentManager()
        { }

        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="pEntAssignment"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<YPLMS2._0.API.Entity.Assignment> Execute(YPLMS2._0.API.Entity.Assignment pEntAssignment, YPLMS2._0.API.Entity.Assignment.ListMethod pMethod)
        {
            List<YPLMS2._0.API.Entity.Assignment> entListAssignment = null;
            AssignmentAdaptor AssignmenttAdaptor = new AssignmentAdaptor();
            switch (pMethod)
            {
                case YPLMS2._0.API.Entity.Assignment.ListMethod.GetAll:
                    entListAssignment = AssignmenttAdaptor.GetAssignmentList(pEntAssignment);
                    break;
                case YPLMS2._0.API.Entity.Assignment.ListMethod.GerForEmailTemplate:
                    entListAssignment = AssignmenttAdaptor.GetAssignmentListForEmailTemplate(pEntAssignment);
                    break;
                default:
                    break;
            }
            return entListAssignment;
        }

        /// <summary>
        /// Add/update/Delete/Get
        /// </summary>
        /// <param name="pEntAssignment"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public YPLMS2._0.API.Entity.Assignment Execute(YPLMS2._0.API.Entity.Assignment pEntAssignment, YPLMS2._0.API.Entity.Assignment.Method pMethod)
        {
            YPLMS2._0.API.Entity.Assignment entAssignment = null;
            AssignmentAdaptor adaptorAssignment = new AssignmentAdaptor();

            switch (pMethod)
            {
                case YPLMS2._0.API.Entity.Assignment.Method.Get:
                    entAssignment = adaptorAssignment.GetAssignmentByID(pEntAssignment);
                    break;
                case YPLMS2._0.API.Entity.Assignment.Method.GetByName:
                    entAssignment = adaptorAssignment.GetAssignmentByName(pEntAssignment);
                    break;
                case YPLMS2._0.API.Entity.Assignment.Method.Add:
                    entAssignment = adaptorAssignment.AddAssignment(pEntAssignment);
                    break;
                case YPLMS2._0.API.Entity.Assignment.Method.Update:
                    entAssignment = adaptorAssignment.EditAssignment(pEntAssignment);
                    break;
                default:
                    entAssignment = null;
                    break;
            }
            return entAssignment;
        }

        /// <summary>
        /// Get List of User/Rules
        /// </summary>
        /// <param name="pEntAssignment"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<BaseEntity> Execute(BaseEntity pEntAssignment, YPLMS2._0.API.Entity.Assignment.ListMethod pMethod)
        {
            List<Learner> entListLearners = new List<Learner>();
            List<GroupRule> entListRules = new List<GroupRule>();            
            List<BaseEntity> entListBase = new List<BaseEntity>();
            AssignmentAdaptor adaptorAssignment = new AssignmentAdaptor();
            switch (pMethod)
            {
                case Entity.Assignment.ListMethod.GetUsersByRuleId:
                    entListLearners = adaptorAssignment.GetUserByRule((YPLMS2._0.API.Entity.Assignment)pEntAssignment);
                    entListBase = Common.AddRange(entListLearners, new List<BaseEntity>());
                    break;
                case Entity.Assignment.ListMethod.GetRules:
                    entListRules = adaptorAssignment.GetRuleList((Entity.Assignment)pEntAssignment);
                    entListBase = Common.AddRange(entListRules, new List<BaseEntity>());
                    break;

                    

                default:
                    break;
            }
            return entListBase;
        }

        /// <summary>
        /// Get Rule List
        /// </summary>
        /// <param name="pEntAssignment"></param>
        /// <param name="pMethod"></param>
        /// <returns></returns>
        public List<YPLMS2._0.API.Entity.Assignment> Execute(List<YPLMS2._0.API.Entity.Assignment> pEntListAssignment, YPLMS2._0.API.Entity.Assignment.ListMethod pMethod)
        {
            List<YPLMS2._0.API.Entity.Assignment> entListAssignment = new List<YPLMS2._0.API.Entity.Assignment>();
            AssignmentAdaptor adaptorAssignment = new AssignmentAdaptor();
            switch (pMethod)
            {
                case YPLMS2._0.API.Entity.Assignment.ListMethod.BulkDelete:
                    entListAssignment = adaptorAssignment.BulkDeleteAssignment(pEntListAssignment);
                    break;
                case YPLMS2._0.API.Entity.Assignment.ListMethod.BulkDeactivate:
                    entListAssignment = adaptorAssignment.BulkDeactivateAssignment(pEntListAssignment);
                    break;
                default:
                    break;
            }
            return entListAssignment;
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="pEntBase">Assignment</param>
        /// <param name="pMethod">Assignment.Method</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(YPLMS2._0.API.Entity.Assignment pEntBase, YPLMS2._0.API.Entity.Assignment.ListMethod pMethod)
        {
            Converter dsConverter = new Converter();
            List<YPLMS2._0.API.Entity.Assignment> entListAssignment = Execute(pEntBase, pMethod);
            DataSet dataSet = dsConverter.ConvertToDataSet<YPLMS2._0.API.Entity.Assignment>(entListAssignment);
            return dataSet;
        }
    }
}
