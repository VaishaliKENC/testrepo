/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Bharat Mohane
* Created:22/02/13
* Last Modified:<dd/mm/yy>
*/
using System.Collections.Generic;
using System;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class Layout:BaseEntity 
    /// </summary>
   [Serializable] public class AdminLayout : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
       public AdminLayout()
        {
            _entListThemes = new List<AdminTheme>();
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

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
        }

        private List<AdminTheme> _entListThemes;
        /// <summary>
        /// List of Themes
        /// </summary>
        public List<AdminTheme> Themes
        {
            get { return _entListThemes; }
        }

        private string _strLayoutName;
        /// <summary>
        /// Layout Name
        /// </summary>
        public string LayoutName
        {
            get { return _strLayoutName; }
            set { if (this._strLayoutName != value) { _strLayoutName = value; } }
        }

        private string _strMasterPageURL;
        /// <summary>
        /// Master Page URL
        /// </summary>
        public string MasterPageURL
        {
            get { return _strMasterPageURL; }
            set { if (this._strMasterPageURL != value) { _strMasterPageURL = value; } }
        }

        private string _strMasterPageURLRTL;
        /// <summary>
        /// Master Page URL RTL
        /// </summary>
        public string MasterPageURLRTL
        {
            get { return _strMasterPageURLRTL; }
            set { if (this._strMasterPageURLRTL != value) { _strMasterPageURLRTL = value; } }
        }


        private string _strType;
        /// <summary>
        /// Type
        /// </summary>
        public string Type
        {
            get { return _strType; }
            set { if (this._strType != value) { _strType = value; } }
        }

        private string _strDefaultThemeId;
        /// <summary>
        /// Default Theme Id
        /// </summary>
        public string DefaultThemeId
        {
            get { return _strDefaultThemeId; }
            set { if (this._strDefaultThemeId != value) { _strDefaultThemeId = value; } }
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
                if (String.IsNullOrEmpty(LayoutName))
                    return false;

                if (String.IsNullOrEmpty(MasterPageURL))
                    return false;

                if (String.IsNullOrEmpty(this.ClientId))
                    return false;

                if (!pIsUpdate && String.IsNullOrEmpty(CreatedById))
                    return false;
                if (String.IsNullOrEmpty(DefaultThemeId))
                    return false;
            }
            if (String.IsNullOrEmpty(LastModifiedById))
                return false;

            return true;
        }
    }
}