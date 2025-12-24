/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Bharat Mohane
* Created:22/02/13
* Last Modified:22/07/09
*/
using System.Collections.Generic;
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class AdminLayoutConfig:BaseEntity 
    /// </summary>
   [Serializable] public class AdminLayoutConfig : BaseEntity
    {
        /// <summary>
        /// Default constructor
        /// </summary>
       public AdminLayoutConfig()
        {
       
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll = 0,
         
        }
        public new enum Method
        {
            Get
        }

        private string _strLayoutID;
        /// <summary>
        /// Layout ID
        /// </summary>
        public string LayoutID
        {
            get { return _strLayoutID; }
            set { if (this._strLayoutID != value) { _strLayoutID = value; } }
        }

        private string _strThemeID;
        /// <summary>
        /// Theme ID
        /// </summary>
        public string ThemeID
        {
            get { return _strThemeID; }
            set { if (this._strThemeID != value) { _strThemeID = value; } }
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
             
                if (String.IsNullOrEmpty(LayoutID))
                    return false;

                if (String.IsNullOrEmpty(ThemeID))
                    return false;               
            }           

            return true;
        }
    }
}