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
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    ///  class OrganizationLevelUnit:BaseEntity
    /// </summary>
   [Serializable] public class OrganizationLevelUnit : BaseEntity
    {
        public const string  BASE_UNIT_ID="U00000";
        public const string CACHE_SUFFIX = "_ORGUNITS";
        /// <summary>
        /// Default Contructor
        /// </summary>
        public OrganizationLevelUnit()
        {
           // _entListOrganizationSubUnits = new List<OrganizationLevelUnit>();
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
            GetByName,
            CheckandAdd,
            Reset
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAllUnitsForImport,
            GetAll,
        }      

        private string _strUnitName;
        /// <summary>
        /// Unit Name
        /// </summary>
        public string UnitName
        {
            get { return _strUnitName; }
            set { if (this._strUnitName != value) { _strUnitName = value; } }
        }

        private string _strLevelId;
        /// <summary>
        /// Level Id
        /// </summary>
        public string LevelId
        {
            get { return _strLevelId; }
            set { if (this._strLevelId != value) { _strLevelId = value; } }
        }

        private string _strParentUnitId;
        /// <summary>
        /// Parent Unit Id
        /// </summary>
        public string ParentUnitId
        {
            get { return _strParentUnitId; }
            set { if (this._strParentUnitId != value) { _strParentUnitId = value; } }
        }


        private string _strParentUnits;
        /// <summary>
        /// ParentUnits
        /// </summary>
         public string ParentUnits
        {
            get { return _strParentUnits; }
            set { if (this._strParentUnits != value) { _strParentUnits = value; } }
        }

      
        private int _iSequenceOrder;
        /// <summary>
        /// Sequence Order
        /// </summary>
        public int SequenceOrder
        {
            get { return _iSequenceOrder; }
            set { if (this._iSequenceOrder != value) { _iSequenceOrder = value; } }
        }

        private bool _bIsActive;
        /// <summary>
        /// To check Is Active
        /// </summary>
        public bool IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
        }

        private Int32 _iOrgTreeUnitId;
        /// <summary>
        /// Org Tree Unit Id
        /// </summary>
        public Int32 OrgTreeUnitId
        {
            get { return _iOrgTreeUnitId; }
            set { if (this._iOrgTreeUnitId != value) { _iOrgTreeUnitId = value; } }

        }

        private Int32 _iOrgTreeParentUnitId;
        /// <summary>
        /// Org Tree Parent Unit Id
        /// </summary>
        public Int32 OrgTreeParentUnitId
        {
            get { return _iOrgTreeParentUnitId; }
            set { if (this._iOrgTreeParentUnitId != value) { _iOrgTreeParentUnitId = value; } }

        }

        private bool _bIsUsed;
        /// <summary>
        /// To check Is Used
        /// </summary>
        public bool IsUsed
        {
            get { return _bIsUsed; }
            set { if (this._bIsUsed != value) { _bIsUsed = value; } }
        }


        private int _bChildCount;
        /// <summary>
        /// To get ChildCount
        /// </summary>
        public int ChildCount
        {
            get { return _bChildCount; }
            set { if (this._bChildCount != value) { _bChildCount = value; } }
        }

        private string _strChildUnits;
        /// <summary>
        /// ChildUnits
        /// </summary>
        public string ChildUnits
        {
            get { return _strChildUnits; }
            set { if (this._strChildUnits != value) { _strChildUnits = value; } }
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


        public bool Validate(bool pIsUpdate)
        {
            if (pIsUpdate)
            {
                if (String.IsNullOrEmpty(ID))
                    return false;
            }
            else
            {
                if (String.IsNullOrEmpty(UnitName))
                    return false;

                if (String.IsNullOrEmpty(LevelId))
                    return false;

                if (String.IsNullOrEmpty(ParentUnitId))
                    return false;

                if (String.IsNullOrEmpty(ClientId))
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