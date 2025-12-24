using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager
{

    /// <summary>
    /// class AssetManager
    /// </summary>
    public class PolicyManager : IManager<Policy, Policy.Method, Policy.ListMethod>
    {
        /// <summary>
        /// Default constructor for AssetManager
        /// </summary>
        public PolicyManager()
        {
        }

        /// <summary>
        /// Use for Read,Add,Update,Delete Policy transactions
        /// </summary>
        /// <param name="pEntPolicy"></param>
        /// <param name="pMethod"></param>
        /// <returns>Policy object</returns>
        public Policy Execute(Policy pEntPolicy, Policy.Method pMethod)
        {
            Policy entPolicyReturn = null;
            PolicyAdaptor adaptorPolicy = new PolicyAdaptor();
            switch (pMethod)
            {
                case Policy.Method.Get:
                    entPolicyReturn = adaptorPolicy.GetPolicyById(pEntPolicy);
                    break;
                case Policy.Method.Add:
                    entPolicyReturn = adaptorPolicy.AddPolicy(pEntPolicy);
                    break;
                case Policy.Method.Update:
                    entPolicyReturn = adaptorPolicy.EditPolicy(pEntPolicy);
                    break;
                case Policy.Method.Delete:
                    entPolicyReturn = adaptorPolicy.DeletePolicy(pEntPolicy);
                    break;
                case Policy.Method.UpdateStatus:
                    entPolicyReturn = adaptorPolicy.UpdateStatus(pEntPolicy);
                    break;
                case Policy.Method.Getforlearner:
                    entPolicyReturn = adaptorPolicy.GetPolicyByIdForLearner(pEntPolicy);
                    break;
                case Policy.Method.UpdateLanguage:
                    entPolicyReturn = adaptorPolicy.UpdateLanguage(pEntPolicy);
                    break;
                case Policy.Method.DeleteLanguage:
                    entPolicyReturn = adaptorPolicy.DeletePolicyLanguage(pEntPolicy);
                    break;
                default:
                    entPolicyReturn = null;
                    break;
            }
            return entPolicyReturn;
        }

        /// <summary>
        /// Used for finding Policy List and Get all Policy List
        /// </summary>
        /// <param name="pEntPolicy"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of Policy objects</returns>
        public List<Policy> Execute(Policy pEntPolicy, Policy.ListMethod pMethod)
        {
            List<Policy> entListAsset = new List<Policy>();
            PolicyAdaptor adaptorPolicy = new PolicyAdaptor();
            switch (pMethod)
            {
                case Policy.ListMethod.GetAll:
                    entListAsset = adaptorPolicy.GetPolicyList(pEntPolicy);
                    break;
                case Policy.ListMethod.GetAllForAssignments:
                    entListAsset = adaptorPolicy.GetPolicyListForAssignment(pEntPolicy);
                    break;
                case Policy.ListMethod.GetPolicyLanguages:
                    entListAsset = adaptorPolicy.GetAllPolicyLanguage(pEntPolicy);
                    break;
                default:
                    break;
            }
            return entListAsset;
        }


        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="Policy"></typeparam>
        /// <param name="pEntBase">Policy object</param>
        /// <param name="pMethod">Policy.ListMethod</param>
        /// <returns>DataSet</returns>
        public DataSet ExecuteDataSet(Policy pEntBase, Policy.ListMethod pMethod)
        {
            DataSet dataSet = null;
            List<Policy> listPolicy = Execute(pEntBase, pMethod);
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            dataSet = dsConverter.ConvertToDataSet<Policy>(listPolicy);
            return dataSet;

        }
    }
}
