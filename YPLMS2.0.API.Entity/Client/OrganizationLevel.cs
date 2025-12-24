/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh & Ashish
* Created:<13/07/09>
* Last Modified:<dd/mm/yy>
*/
using System.Collections.Generic;
using System;
using System.Xml;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class OrganizationLevel:BaseEntity
    /// </summary>
   [Serializable] public class OrganizationLevel : BaseEntity
    {
        public const string  BASE_LEVEL_ID="L00000";
        public const string CACHE_SUFFIX = "_ORGLEVELS";
 
        /// <summary>
        /// Default Contructor
        /// </summary>
        public OrganizationLevel()
        {
            _entListOrganizationUnits = new List<OrganizationLevelUnit>();
            _xmlOrgTreeXML = new XmlDocument();
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete,
            CheckandAdd,
            GetOrgTreeDHTML
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetLevelsList,
            GetOnlyLevels
        }

        private List<OrganizationLevelUnit> _entListOrganizationUnits;
        /// <summary>
        /// List of Organization Units
        /// </summary>
        public List<OrganizationLevelUnit> OrganizationUnits
        {
            get { return _entListOrganizationUnits; }
            set { if (this._entListOrganizationUnits != value) { _entListOrganizationUnits = value; } }
        }

        private string _strLevelName;
        /// <summary>
        /// Level Name
        /// </summary>
        public string LevelName
        {
            get { return _strLevelName; }
            set { if (this._strLevelName != value) { _strLevelName = value; } }
        }

        private int _iLevelOrder;
        /// <summary>
        /// Level Order
        /// </summary>
        public int LevelOrder
        {
            get { return _iLevelOrder; }
            set { if (this._iLevelOrder != value) { _iLevelOrder = value; } }
        }

        private bool _bIsUsed;
        /// <summary>
        /// IsUsed
        /// </summary>
        public bool IsUsed
        {
            get { return _bIsUsed; }
            set { if (this._bIsUsed != value) { _bIsUsed = value; } }
        }

        private XmlDocument _xmlOrgTreeXML;
        /// <summary>
        /// XmlDocument OrganizationTreeXML
        /// </summary>
        public XmlDocument OrganizationTreeXML
        {
            get { return _xmlOrgTreeXML; }
            set { if (this._xmlOrgTreeXML != value) { _xmlOrgTreeXML = value; } }
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
                if (String.IsNullOrEmpty(LevelName))
                    return false;

                if (String.IsNullOrEmpty(ClientId))
                    return false;

                if (!pIsUpdate && String.IsNullOrEmpty(CreatedById))
                    return false;

                if (String.IsNullOrEmpty(LastModifiedById))
                    return false;
            }
            return true;
        }
    }
}