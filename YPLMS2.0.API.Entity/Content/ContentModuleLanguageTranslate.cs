using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Deepak Dangat
* Created:<21/05/13>
* Last Modified:<dd/mm/yy>
*/


namespace YPLMS2._0.API.Entity
{
    [Serializable]
    public class ContentModuleLanguageTranslate : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        ///</summary>

        public ContentModuleLanguageTranslate()
        { }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {            
            Add,
            Update,
            Get,
            Approve
        }

        public new enum ListMethod
        {
            GetContentModuleLanguageTranslateList,
            GetContentModuleLanguageTranslateUnApprovedList
        }

        public string ContentModuleId { get; set; }
        public string LanguageId { get; set; }
        public string ContentModuleName { get; set; }
        public string ExcelFilePath { get; set; }
        public string RecordingFilePath { get; set; }
        public string ExternalLink { get; set; }
        public string ContentFolderURL { get; set; }

        public string DraftExcelFilePath { get; set; }
        public string DraftRecordingFilePath { get; set; }
        public string DraftExternalLink { get; set; }


    }
}
