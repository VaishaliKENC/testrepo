/* 
* Copyright Brainvisa Technologies Pvt. Ltd.
* This source file and source code is proprietary property of Brainvisa Technologies Pvt. Ltd. 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Brainvisa’s Client.
* Author: Ashish Phate
* Created:<19/09/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    ///  class Task 
    /// </summary>
    [Serializable]
    public class PrintCertificateTemplateMaster : BaseEntity
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PrintCertificateTemplateMaster()
        {
           
        }

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
        
        private string _strCertificateTemplateName;
        /// <summary>
        /// Task Name
        /// </summary>
        public string CertificateTemplateName
        {
            get { return _strCertificateTemplateName; }
            set { if (this._strCertificateTemplateName != value) { _strCertificateTemplateName = value; } }
        }

        private string _strCertificateContentPath;
        /// <summary>
        /// Task Name
        /// </summary>
        public string CertificateContentPath
        {
            get { return _strCertificateContentPath; }
            set { if (this._strCertificateContentPath != value) { _strCertificateContentPath = value; } }
        }

        private bool _bIsDefault;
        /// <summary>
        /// Is Default
        /// </summary>
        public bool IsDefault
        {
            get { return _bIsDefault; }
            set { if (this._bIsDefault != value) { _bIsDefault = value; } }
        }              

        public bool Validate(bool pIsUpdate)
        {
            if (pIsUpdate)
            {
                if (String.IsNullOrEmpty(ID))
                    return false;
            }
            else
            {
                if (String.IsNullOrEmpty(this.ClientId))
                    return false;
            }
            if (String.IsNullOrEmpty(LastModifiedById))
                return false;
            return true;
        }
    }
}