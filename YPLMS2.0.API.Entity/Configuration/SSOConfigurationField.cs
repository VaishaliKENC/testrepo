/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author: Ashish Phate
* Created:<30/09/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    ///  class Task 
    /// </summary>
    [Serializable]
    public class SSOConfigurationField : ImportDefination 
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SSOConfigurationField()
        {
           
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            AddAll
        }

        private string _strSSOFieldName;
        /// <summary>
        /// SSO Field Name on Request Form
        /// </summary>
        public string SSOFieldName
        {
            get { return _strSSOFieldName; }
            set { if (this._strSSOFieldName != value) { _strSSOFieldName = value; } }
        }

        private Int64 _strIDFID;
        /// <summary>
        /// IDFID
        /// </summary>
        public Int64 IDFID
        {
            get { return _strIDFID; }
            set { if (this._strIDFID != value) { _strIDFID = value; } }
        }

        private string _strImportDefinitionID;
        /// <summary>
        /// Import Definition ID
        /// </summary>
        public string ImportDefinitionID
        {
            get { return _strImportDefinitionID; }
            set { if (this._strImportDefinitionID != value) { _strImportDefinitionID = value; } }
        }

        private string _strSSOConfigurationId;
        /// <summary>
        /// SSO Configuration Id
        /// </summary>
        public string SSOConfigurationId
        {
            get { return _strSSOConfigurationId; }
            set { if (this._strSSOConfigurationId != value) { _strSSOConfigurationId = value; } }
        }

        private bool _bIsIncluded = false;        
        /// <summary>
        /// Is Included
        /// </summary>
        public bool IsIncluded
        {
            get { return _bIsIncluded; }
            set { if (this._bIsIncluded != value) { _bIsIncluded = value; } }
        }        
       
    }
}