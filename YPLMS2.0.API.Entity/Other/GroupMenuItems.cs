/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh 
* Created:25/08/09
* Last Modified:25/08/09
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// MenuItems Class
    /// </summary>
    [Serializable]
    public class GroupMenuItems : MenuItems
    {
        public GroupMenuItems()
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
            UpdateStatus
        }


        private string _strGroupMenuLinkId;
        /// <summary>
        /// FeatureId
        /// </summary>
        public string GroupMenuLinkId
        {
            get { return _strGroupMenuLinkId; }
            set { if (this._strGroupMenuLinkId != value) { _strGroupMenuLinkId = value; } }
        }

        private string _strGroupMenuItemName;
        /// <summary>
        /// MenuItemEnglishName
        /// </summary>
        public string GroupMenuItemName
        {
            get { return _strGroupMenuItemName; }
            set { if (this._strGroupMenuItemName != value) { _strGroupMenuItemName = value; } }
        }

      
        private int _strGroupDisplayOrder;
        /// <summary>
        /// Client Id
        /// </summary>
        /// 
        public int GroupDisplayOrder
        {
            get { return _strGroupDisplayOrder; }
            set { if (this._strGroupDisplayOrder != value) { _strGroupDisplayOrder = value; } }
        }

        private string _strGroupLanguageId;
        /// <summary>
        /// Client Id
        /// </summary>
        /// 
        public string GroupLanguageId
        {
            get { return _strGroupLanguageId; }
            set { if (this._strGroupLanguageId != value) { _strGroupLanguageId = value; } }
        }


        private string _strGroupMenuCSSName;
        /// <summary>
        /// Client Id
        /// </summary>
        /// 
        public string GroupMenuCSSName
        {
            get { return _strGroupMenuCSSName; }
            set { if (this._strGroupMenuCSSName != value) { _strGroupMenuCSSName = value; } }
        }

        private string _strGroupMenuCSSNameStyle;
        /// <summary>
        /// Client Id
        /// </summary>
        /// 
        public string GroupMenuCSSNameStyle
        {
            get { return _strGroupMenuCSSNameStyle; }
            set { if (this._strGroupMenuCSSNameStyle != value) { _strGroupMenuCSSNameStyle = value; } }
        }

        private string _strSystemUserGuid;
        /// <summary>
        /// Client Id
        /// </summary>
        /// 
        public string SystemUserGuid
        {
            get { return _strSystemUserGuid; }
            set { if (this._strSystemUserGuid != value) { _strSystemUserGuid = value; } }
        }
    }
}