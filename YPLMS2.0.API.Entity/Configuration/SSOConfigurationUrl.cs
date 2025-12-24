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
     public class SSOConfigurationUrl : BaseEntity
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SSOConfigurationUrl()
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
            Update           
        }
        
        private string _strUrl;
        /// <summary>
        /// SSO Url
        /// </summary>
        public string URL
        {

            //get { return _strUrl; SSOConfigurationField f = new SSOConfigurationField();
            //f.}
            get { return _strUrl; }
            set { if (this._strUrl != value) { _strUrl = value; } }
        }

        private string _strSSOLogoutURL;
        /// <summary>
        /// SSOLogoutURL
        /// </summary>
        public string SSOLogoutURL
        {
            
            get { return _strSSOLogoutURL; }
            set { if (this._strSSOLogoutURL != value) { _strSSOLogoutURL = value; } }
        }
       

        /// <summary>
        /// For validatoin on server side
        /// </summary>
        /// <param name="pIsUpdate"></param>
        /// <returns></returns>
        public bool Validate(bool pIsUpdate)
        {
            if (pIsUpdate)
            {
                if (String.IsNullOrEmpty(ID))
                    return false;
            }
            
            if (String.IsNullOrEmpty(LastModifiedById))
                return false;
            return true;
        }
    }
}