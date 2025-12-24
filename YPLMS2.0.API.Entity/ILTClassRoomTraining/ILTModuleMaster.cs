/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:Bharat
* Created:<07/20/23>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ILTModuleMaster : BaseEntity 
    /// </summary>
    /// 
    public class ILTModuleMaster : ILTModuleLanguageMaster
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public ILTModuleMaster()
        { }


        private string _strEventId;
        /// <summary>
        /// EventId
        /// </summary>
        public string EventId
        {
            get { return _strEventId; }
            set { if (this._strEventId != value) { _strEventId = value; } }
        }

        private string _strSessionId;
        /// <summary>
        /// SessionId
        /// </summary>
        public string SessionId
        {
            get { return _strSessionId; }
            set { if (this._strSessionId != value) { _strSessionId = value; } }
        }

        private string _strModuleId;
        /// <summary>
        /// ModuleId
        /// </summary>
        public string ModuleId
        {
            get { return _strModuleId; }
            set { if (this._strModuleId != value) { _strModuleId = value; } }
        }

        private float _strDay;
        /// <summary>
        /// Day
        /// </summary>
        public float Day
        {
            get { return _strDay; }
            set { if (this._strDay != value) { _strDay = value; } }
        }

        private float _strHours;
        /// <summary>
        /// Hours
        /// </summary>
        public float Hours
        {
            get { return _strHours; }
            set { if (this._strHours != value) { _strHours = value; } }
        }

        private int _strTotalRows;
        /// <summary>
        /// TotalRows
        /// </summary>
        public int TotalRows
        {
            get { return _strTotalRows; }
            set { if (this._strTotalRows != value) { _strTotalRows = value; } }
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
            UpdateLanguage
        }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAll_Module_Event
        }
    }
}