using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YPLMS2._0.API.Entity;

namespace YPLMS2._0.API.DataAccessManager.BusinessManager.Content
{
    /// <summary>
    /// class AssetManager
    /// </summary>
    public class QuestionnaireManager : IManager<Questionnaire, Questionnaire.Method, Questionnaire.ListMethod>
    {
        /// <summary>
        /// Default constructor for AssetManager
        /// </summary>
        public QuestionnaireManager()
        {
        }

        /// <summary>
        /// Use for Read,Add,Update,Delete Questionnaire transactions
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <param name="pMethod"></param>
        /// <returns>Questionnaire object</returns>
        public Questionnaire Execute(Questionnaire pEntQuestionnaire, Questionnaire.Method pMethod)
        {
            Questionnaire entQuestionnaireReturn = null;
            QuestionnaireAdaptor adaptorQuestionnaire = new QuestionnaireAdaptor();
            switch (pMethod)
            {
                case Questionnaire.Method.GetQuesType:
                    entQuestionnaireReturn = adaptorQuestionnaire.GetQuestionnaireTypeById_Learner(pEntQuestionnaire);
                    break;
                case Questionnaire.Method.Get:
                    entQuestionnaireReturn = adaptorQuestionnaire.GetQuestionnaireById(pEntQuestionnaire);
                    break;
                case Questionnaire.Method.CheckQuestionnaireByName:
                    entQuestionnaireReturn = adaptorQuestionnaire.CheckQuestionnaireByName(pEntQuestionnaire);
                    break;
                case Questionnaire.Method.Add:
                    entQuestionnaireReturn = adaptorQuestionnaire.UpdateQuestionnaire(pEntQuestionnaire, YPLMS2._0.API.DataAccessManager.Schema.Common.VAL_INSERT_MODE, false);
                    break;
                case Questionnaire.Method.Update:
                    entQuestionnaireReturn = adaptorQuestionnaire.UpdateQuestionnaire(pEntQuestionnaire, YPLMS2._0.API.DataAccessManager.Schema.Common.VAL_UPDATE_MODE, false);
                    break;
                case Questionnaire.Method.CopyQuestionnaire:
                    entQuestionnaireReturn = adaptorQuestionnaire.CopyQuestionnaire(pEntQuestionnaire);
                    break;
                case Questionnaire.Method.ImportQuestionnaire:
                    entQuestionnaireReturn = adaptorQuestionnaire.ImportQuestionnaire(pEntQuestionnaire);
                    break;
                case Questionnaire.Method.CopyImportQuestionnaireLanguages:
                    entQuestionnaireReturn = adaptorQuestionnaire.CopyImportQuestionnaireLanguages(pEntQuestionnaire);
                    break;
                case Questionnaire.Method.GetDefaultSequence:
                    entQuestionnaireReturn = adaptorQuestionnaire.GetQuestionnaireDefaultSequence(pEntQuestionnaire);
                    break;
                case Questionnaire.Method.UpdateDefaultSequence:
                    entQuestionnaireReturn = adaptorQuestionnaire.UpdateDefaultSequence(pEntQuestionnaire);
                    break;
                case Questionnaire.Method.GetShowQuestionNumber:
                    entQuestionnaireReturn = adaptorQuestionnaire.GetQuestionnaireShowQuestionNumber(pEntQuestionnaire);
                    break;
                case Questionnaire.Method.UpdateLanguage:
                    entQuestionnaireReturn = adaptorQuestionnaire.SaveQuestionnaireLanguage(pEntQuestionnaire);
                    break;
                case Questionnaire.Method.DeleteLanguage:
                    entQuestionnaireReturn = adaptorQuestionnaire.DeleteQuestionnaireLanguage(pEntQuestionnaire);
                    break;
                default:
                    break;
            }
            return entQuestionnaireReturn;
        }

        /// <summary>
        /// Used for finding Questionnaire List and Get all Questionnaire List
        /// </summary>
        /// <param name="pEntQuestionnaire"></param>
        /// <param name="pMethod"></param>
        /// <returns>List of Questionnaire objects</returns>
        public List<Questionnaire> Execute(Questionnaire pEntQuestionnaire, Questionnaire.ListMethod pMethod)
        {
            List<Questionnaire> entListQuestionnaire = new List<Questionnaire>();
            QuestionnaireAdaptor adaptorQuestionnaire = new QuestionnaireAdaptor();
            switch (pMethod)
            {
                case Questionnaire.ListMethod.GetAll:
                    entListQuestionnaire = adaptorQuestionnaire.GetQuestionnaireList(pEntQuestionnaire);
                    break;
                case Questionnaire.ListMethod.GetQuestionnaireForAssignment:
                    entListQuestionnaire = adaptorQuestionnaire.GetQuestionnaireForAssignment(pEntQuestionnaire);
                    break;
                case Questionnaire.ListMethod.GetQuestionnaireLanguages:
                    entListQuestionnaire = adaptorQuestionnaire.GetQuestionnaireLanguageList(pEntQuestionnaire);
                    break;
                case Questionnaire.ListMethod.GetQuestionnaire:
                    entListQuestionnaire = adaptorQuestionnaire.GetQuestionnaireDtls(pEntQuestionnaire);
                    break;
                case Questionnaire.ListMethod.GetQuestionnaireTracking:
                    entListQuestionnaire = adaptorQuestionnaire.GetQuestionnaireTracking(pEntQuestionnaire);
                    break;
                case Questionnaire.ListMethod.GetQuestionnaireTrackingWithOutPaging:
                    entListQuestionnaire = adaptorQuestionnaire.GetQuestionnaireTrackingWithOutPaging(pEntQuestionnaire);
                    break;
                default:
                    break;
            }
            return entListQuestionnaire;
        }

        //<summary>
        //Return base entity list
        //</summary>
        //<param name="pEntQuestionnaire"></param>
        //<param name="pMethod"></param>
        //<returns></returns>
        public List<Language> Execute(BaseEntity pEntQuestionnaire, Questionnaire.ListMethod pMethod)
        {
            List<Language> entListLanguage = new List<Language>();
            QuestionnaireAdaptor adaptorQuestionnaire = new QuestionnaireAdaptor();
            switch (pMethod)
            {
                case Questionnaire.ListMethod.GetImportLanguages:
                    entListLanguage = adaptorQuestionnaire.GetImportLanguages((Questionnaire)pEntQuestionnaire);
                    break;
                case Questionnaire.ListMethod.GetExportLanguages:
                    entListLanguage = adaptorQuestionnaire.GetExportLanguages((Questionnaire)pEntQuestionnaire);
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
        /// <returns>List of Questionnaire objects</returns>
        public List<Questionnaire> Execute(List<Questionnaire> pEntListQues, Questionnaire.ListMethod pMethod)
        {
            List<Questionnaire> entListQuesReturn = new List<Questionnaire>();
            QuestionnaireAdaptor adaptorQues = new QuestionnaireAdaptor();
            switch (pMethod)
            {
                case Questionnaire.ListMethod.DeleteQuestionnaireList:
                    entListQuesReturn = adaptorQues.DeleteQuestionnaireList(pEntListQues);
                    break;
                case Questionnaire.ListMethod.ApproveQuestionnaireList:
                    entListQuesReturn = adaptorQues.ApproveQuestionnaireList(pEntListQues);
                    break;
                case Questionnaire.ListMethod.ActivateDeActivateStatus:
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
        /// <typeparam name="Questionnaire"></typeparam>
        /// <param name="pEntBase">Questionnaire object</param>
        /// <param name="pMethod">Questionnaire.ListMethod</param>
        /// <returns>DataSet of Language or Questionnaire</returns>
        public DataSet ExecuteDataSet(Questionnaire pEntBase, Questionnaire.ListMethod pMethod)
        {
            DataSet dataSet = null;
            YPLMS.Services.Converter dsConverter = new YPLMS.Services.Converter();
            if (pMethod == Questionnaire.ListMethod.GetImportLanguages || pMethod == Questionnaire.ListMethod.GetExportLanguages)
            {
                List<Language> entListLanguage = Execute((BaseEntity)pEntBase, pMethod);
                dataSet = dsConverter.ConvertToDataSet<Language>(entListLanguage);
            }
            else
            {
                List<Questionnaire> listQuestionnaire = Execute(pEntBase, pMethod);
                dataSet = dsConverter.ConvertToDataSet<Questionnaire>(listQuestionnaire);
            }
            return dataSet;

        }
    }
}
