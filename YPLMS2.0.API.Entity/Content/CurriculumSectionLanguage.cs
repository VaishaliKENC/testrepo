using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class CurriculumSectionLanguage:BaseEntity
    {
          /// <summary>
        /// Default Contructor
        /// <summary>
        public CurriculumSectionLanguage()
        {
           
        }

        //public string SectionId { get; set; }
        public string LanguageId { get; set; }
        public string SectionName { get; set; }
        public string SectionDescription { get; set; }
        public string SectionInstruction { get; set; }

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
            Get,
            Add,
            Update,
            Delete
          
        }
    }
}
