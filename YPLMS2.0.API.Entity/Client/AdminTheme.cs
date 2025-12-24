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
    /// class AdminTheme:BaseEntity 
    /// </summary>
   [Serializable] public class AdminTheme : BaseEntity
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public AdminTheme()
        {
            _entListThemeLanguages = new List<ThemeLanguages>();
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll = 0,
        }

        private string _strThemeName;
        /// <summary>
        /// Theme Name
        /// </summary>
        public string ThemeName
        {
            get { return _strThemeName; }
            set { if (this._strThemeName != value) { _strThemeName = value; } }
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

        private string _strType;
       /// <summary>
       /// Type
       /// </summary>
        public string Type
        {
            get { return _strType; }
            set { if (this._strType != value) { _strType = value; } }
        }

        private List<ThemeLanguages> _entListThemeLanguages;
        /// <summary>
        /// List of Theme Languages
        /// </summary>
        public List<ThemeLanguages> ThemeLanguages
        {
            get { return _entListThemeLanguages; }
        }

        private string _strThemeBaseURL;
        /// <summary>
        /// Theme Base URL
        /// </summary>
        public string ThemeBaseURL
        {
            get { return _strThemeBaseURL; }
            set { if (this._strThemeBaseURL != value) { _strThemeBaseURL = value; } }
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
                if (String.IsNullOrEmpty(ThemeName))
                    return false;

                if (String.IsNullOrEmpty(LayoutID))
                    return false;

                if (String.IsNullOrEmpty(ThemeBaseURL))
                    return false;

                if (!pIsUpdate && String.IsNullOrEmpty(CreatedById))
                    return false;
            }
            if (String.IsNullOrEmpty(LastModifiedById))
                return false;

            return true;
        }
    }
}