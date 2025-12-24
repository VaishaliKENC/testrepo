/* 
* Copyright Encora.
* This source file and source code is proprietary property of Encora Technologies Pvt. Ltd. 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:Charu Singh
* Created:<17/09/09>
* Last Modified:<dd/mm/yy>
*/
namespace YPLMS2._0.API.Entity
{
    public class CertificationAdditionalLinks : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public CertificationAdditionalLinks()
        { }

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

        private string _strCertificationId;
        public string CertificationId
        {
            get { return _strCertificationId; }
            set { if (this._strCertificationId != value) { _strCertificationId = value; } }
        }

        private string _strLinkURL;
        public string LinkURL
        {
            get { return _strLinkURL; }
            set { if (this._strLinkURL != value) { _strLinkURL = value; } }
        }

        private string _strLinkId;
        public string LinkId
        {
            get { return _strLinkId; }
            set { if (this._strLinkId != value) { _strLinkId = value; } }
        }
    }
}