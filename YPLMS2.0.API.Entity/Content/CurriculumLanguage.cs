using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
   public class CurriculumLanguage: BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
       public CurriculumLanguage()
        { }

       public string LanguageId { get; set; }
      // public string CurriculumId { get; set; }
       public string CurriculumName { get; set; }
       public string CurriculumDescription { get; set; }
       public string CurriculumInstruction { get; set; }


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

       /// <summary>
       /// enum ListMethod
       /// </summary>
       public new enum ListMethod
       {
           GetAll
       }
    }
}

