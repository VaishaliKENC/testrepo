using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class CurriculumSection : CurriculumSectionLanguage
    {
         /// <summary>
        /// Default Contructor
        /// <summary>
        public CurriculumSection()
        {
           
        }

        public List<CurriculumActivity> CurriculumActivity { get; set; }
        public string CurriculumId { get; set; }
        public int SequenceOrder { get; set; }      
        public string CompletionOrder { get; set; }
        public bool EnforeceActivitySequencing { get; set; }
        public decimal Progress { get; set; }
        
      
        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetALLSectionLanguages,
            GetAllSectionByLearner,
            BulkDelete,
            UpdateSequenceOrder
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Get_Is_Name_Available,
            Add,
            Update,
            Delete,
            UpdateLanguage                     
        }
    }
}
