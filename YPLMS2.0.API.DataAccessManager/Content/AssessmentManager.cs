using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager
{
    /// <summary>
    /// class AssetManager
    /// </summary>
    public class AssessmentManager : IManager<AssessmentDates, AssessmentDates.Method, AssessmentDates.ListMethod>
    {
        /// <summary>
        /// Default constructor for AssetManager
        /// </summary>
        public AssessmentManager()
        {
        }

        /// <summary>
        /// Use for Read,Add,Update,Delete Assessment transactions
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <param name="pMethod"></param>
        /// <returns>Assessment object</returns>
        public AssessmentDates Execute(AssessmentDates pEntAssessment, AssessmentDates.Method pMethod)
        {
            AssessmentDates entAssessmentReturn = null;
            AssessmentAdaptor adaptorAssessment = new AssessmentAdaptor();
            switch (pMethod)
            {
                case AssessmentDates.Method.GetQuesType:
                    entAssessmentReturn = adaptorAssessment.GetAssessmentTypeById_Learner(pEntAssessment);
                    break;
                case AssessmentDates.Method.Get:
                    entAssessmentReturn = adaptorAssessment.GetAssessmentById(pEntAssessment);
                    break;
                case AssessmentDates.Method.CheckAssessmentByName:
                    entAssessmentReturn = adaptorAssessment.CheckAssessmentByName(pEntAssessment);
                    break;
                case AssessmentDates.Method.Add:
                    entAssessmentReturn = adaptorAssessment.UpdateAssessment(pEntAssessment, YPLMS2._0.API.DataAccessManager.Schema.Common.VAL_INSERT_MODE, false);
                    break;
                case AssessmentDates.Method.Update:
                    entAssessmentReturn = adaptorAssessment.UpdateAssessment(pEntAssessment, YPLMS2._0.API.DataAccessManager.Schema.Common.VAL_UPDATE_MODE, false);
                    break;
                case AssessmentDates.Method.CopyAssessment:
                    entAssessmentReturn = adaptorAssessment.CopyAssessment(pEntAssessment);
                    break;
                case AssessmentDates.Method.ImportAssessment:
                    entAssessmentReturn = adaptorAssessment.ImportAssessment(pEntAssessment);
                    break;
                case AssessmentDates.Method.CopyImportAssessmentLanguages:
                    entAssessmentReturn = adaptorAssessment.CopyImportAssessmentLanguages(pEntAssessment);
                    break;
                case AssessmentDates.Method.GetDefaultSequence:
                    entAssessmentReturn = adaptorAssessment.GetAssessmentDefaultSequence(pEntAssessment);
                    break;
                //case Assessment.Method.UpdateDefaultSequence:
                //    entAssessmentReturn = adaptorAssessment.UpdateDefaultSequence(pEntAssessment);
                //    break;
                case AssessmentDates.Method.GetShowQuestionNumber:
                    entAssessmentReturn = adaptorAssessment.GetAssessmentShowQuestionNumber(pEntAssessment);
                    break;
                case AssessmentDates.Method.UpdateLanguage:
                    entAssessmentReturn = adaptorAssessment.SaveAssessmentLanguage(pEntAssessment);
                    break;
                case AssessmentDates.Method.DeleteLanguage:
                    entAssessmentReturn = adaptorAssessment.DeleteAssessmentLanguage(pEntAssessment);
                    break;
                case AssessmentDates.Method.GetAssessmentResult:
                    entAssessmentReturn = adaptorAssessment.GetAssessmentResult(pEntAssessment);
                    break;
                default:
                    break;
            }
            return entAssessmentReturn;
        }

        /// <summary>
        /// Used for finding Assessment List and Get all Assessment List
        /// </summary>
        /// <param name="pEntAssessment"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of Assessment objects</returns>
        public List<AssessmentDates> Execute(AssessmentDates pEntAssessment, AssessmentDates.ListMethod pMethod)
        {
            List<AssessmentDates> entListAssessment = new List<AssessmentDates>();
            AssessmentAdaptor adaptorAssessment = new AssessmentAdaptor();
            switch (pMethod)
            {
                case AssessmentDates.ListMethod.GetAll:
                    entListAssessment = adaptorAssessment.GetAssessmentList(pEntAssessment);
                    break;
                case AssessmentDates.ListMethod.GetAssessmentForAssignment:
                    entListAssessment = adaptorAssessment.GetAssessmentForAssignment(pEntAssessment);
                    break;
                case AssessmentDates.ListMethod.GetAssessmentLanguages:
                    entListAssessment = adaptorAssessment.GetAssessmentLanguageList(pEntAssessment);
                    break;
                case AssessmentDates.ListMethod.GetAssessment:
                    entListAssessment = adaptorAssessment.GetAssessmentDtls(pEntAssessment);
                    break;
                case AssessmentDates.ListMethod.GetAssessmentTracking:
                    entListAssessment = adaptorAssessment.GetAssessmentTracking(pEntAssessment);
                    break;

                case AssessmentDates.ListMethod.GetAssessmentTrackingPreviewAssessment:
                    entListAssessment = adaptorAssessment.GetAssessmentTrackingPreviewAssessment(pEntAssessment);
                    break;
                case AssessmentDates.ListMethod.GetAssessmentTrackingWithOutPaging:
                    entListAssessment = adaptorAssessment.GetAssessmentTrackingWithOutPaging(pEntAssessment);
                    break;
                default:
                    break;
            }
            return entListAssessment;
        }

        //<summary>
        //Return base entity list
        //</summary>
        //<param name="pEntAssessment"></param>
        //<param name="pMethod"></param>
        //<returns></returns>
        public List<Language> Execute(BaseEntity pEntAssessment, AssessmentDates.ListMethod pMethod)
        {
            List<Language> entListLanguage = new List<Language>();
            AssessmentAdaptor adaptorAssessment = new AssessmentAdaptor();
            switch (pMethod)
            {
                case AssessmentDates.ListMethod.GetImportLanguages:
                    entListLanguage = adaptorAssessment.GetImportLanguages((AssessmentDates)pEntAssessment);
                    break;
                case AssessmentDates.ListMethod.GetExportLanguages:
                    entListLanguage = adaptorAssessment.GetExportLanguages((AssessmentDates)pEntAssessment);
                    break;
                default:
                    break;
            }
            return entListLanguage;
        }

        /// <summary>
        /// Use for Bulk Delete.    
        /// </summary>
        /// <param name="pEntListContModule"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of Assessment objects</returns>
        public List<AssessmentDates> Execute(List<AssessmentDates> pEntListQues, AssessmentDates.ListMethod pMethod)
        {
            List<AssessmentDates> entListQuesReturn = new List<AssessmentDates>();
            AssessmentAdaptor adaptorQues = new AssessmentAdaptor();
            switch (pMethod)
            {
                case AssessmentDates.ListMethod.DeleteAssessmentList:
                    entListQuesReturn = adaptorQues.DeleteAssessmentList(pEntListQues);
                    break;
                case AssessmentDates.ListMethod.ApproveAssessmentList:
                    entListQuesReturn = adaptorQues.ApproveAssessmentList(pEntListQues);
                    break;
                case AssessmentDates.ListMethod.ActivateDeActivateStatus:
                    entListQuesReturn = adaptorQues.ActivateDeActivateStatusList(pEntListQues);
                    break;
                default:
                    entListQuesReturn = null;
                    break;
            }
            return entListQuesReturn;
        }


        /// <summary>
        /// Returns Dataset as per method
        /// </summary>
        /// <typeparam name="Assessment"></typeparam>
        /// <param name="pEntBase">Assessment object</param>
        /// <param name="pMethod">Assessment.ListMethod</param>
        /// <returns>DataSet of Language or Assessment</returns>
        public DataSet ExecuteDataSet(AssessmentDates pEntBase, AssessmentDates.ListMethod pMethod)
        {
            DataSet dataSet = null;
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            if (pMethod == AssessmentDates.ListMethod.GetImportLanguages || pMethod == AssessmentDates.ListMethod.GetExportLanguages)
            {
                List<Language> entListLanguage = Execute((BaseEntity)pEntBase, pMethod);
                dataSet = dsConverter.ConvertToDataSet<Language>(entListLanguage);
            }
            else
            {
                List<AssessmentDates> listAssessment = Execute(pEntBase, pMethod);
                dataSet = dsConverter.ConvertToDataSet<AssessmentDates>(listAssessment);
            }
            return dataSet;

        }
    }
}
