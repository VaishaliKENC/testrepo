/* 
* Copyright Brainvisa Technologies Pvt. Ltd.
* This source file and source code is proprietary property of Brainvisa Technologies Pvt. Ltd. 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Brainvisa’s Client.
* Author:Fattesinh & Ashish
* Created:<13/07/09>
* Last Modified:<dd/mm/yy>
*/
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// DataViewManagement Class inherited from OrganizationLevelUnit 
    /// </summary>
    public class DataViewManagement : OrganizationLevelUnit
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public DataViewManagement()
        { }

        private string _dataViewId;
        /// <summary>
        /// Data View Id
        /// </summary>
        public string DataViewId
        {
            get { return _dataViewId; }
            set { if (this._dataViewId != value) { _dataViewId = value; } }
        }

        private string _strEntityId;
        /// <summary>
        /// Entity Id
        /// </summary>
        public string EntityId
        {
            get { return _strEntityId; }
            set { if (this._strEntityId != value) { _strEntityId = value; } }
        }

        private string _strEntityTypeId;
        /// <summary>
        /// Entity Type Id
        /// </summary>
        public string EntityTypeId
        {
            get { return _strEntityTypeId; }
            set { if (this._strEntityTypeId != value) { _strEntityTypeId = value; } }
        }    
    }
}