/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:
* Created:<24/1/2013>
* Last Modified:<24/1/2013>
*/
using System;
using System.Collections.Generic;
using System.Xml;
using System.Data.SqlClient;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// Serializable class Client : BaseEntity    
    /// </summary>
    [Serializable]
    public class ClientFeature : BaseEntity
    {

        #region CONSTANT FEATURE IDS
        //public const string CACHE_SUFFIX = "_FEATURES";

        /// <summary>
        /// Client Assesment module
        /// </summary>
        public const string FEA_ID_Assesment = "CFM002";
        public const string FEA_ID_UserDataXmlStoreType = "CFM003";
        public const string FEA_ID_Force_ReAssignment = "CFM007";
        public const string FEA_ID_Mobile_LMS = "CFM010";
        public const string FEA_ID_User_Expiry = "CFM013";
        public const string FEA_ID_Social_Integration = "CFM015";
        public const string FEA_ID_Enable_Social_Media_Comment_on_Menu = "FEA007";
        public const string FEA_ID_Social_Media_Comment_On_Activity_Completion = "FEA008";
        public const string FEA_ID_Product_Catalog_View = "CFM017";
        public const string FEA_ID_StandAloneCoursePlayer = "CFM014";
        public const string FEA_ID_VirtualTraining_View = "CFM022";
        public const string FEA_ID_ClassroomTraining = "CFM008";

        public const string FEA_ID_Blog = "CFM005";
        public const string FEA_ID_Forum = "CFM006";
        public const string FEA_ID_Document_Library = "CFM020";
        public const string FEA_ID_FAQ = "CFM012";
        public const string FEA_ID_IPerform = "CFM027";
        public const string FEA_ID_GroupDocConfiguration = "FEA029";
        public const string FEA_ID_Chat = "CFM029";
        public const string FEA_ID_ASSESSMENT_LOCK = "CFM030";
        public const string FEA_ID_GRSI_TCA = "CFM031";
        public const string FEA_ID_PSCI = "CFM032";
        public const string FEA_ID_UPS = "CFM033";

        #endregion

        private bool _strIsActive;
        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive
        {
            get { return _strIsActive; }
            set { if (this._strIsActive != value) { _strIsActive = value; } }
        }

        private string _strClientFeatureID;
        /// <summary>
        /// ClientId
        /// </summary>
        public string ClientFeatureID
        {
            get { return _strClientFeatureID; }
            set { if (this._strClientFeatureID != value) { _strClientFeatureID = value; } }
        }


        private string _strSystemUserGuid;
        /// <summary>
        /// SystemUserGuid
        /// </summary>
        public string SystemUserGuid
        {
            get { return _strSystemUserGuid; }
            set { if (this._strSystemUserGuid != value) { _strSystemUserGuid = value; } }
        }


        private string _strPubPortalCustomFields;
        /// <summary>
        /// PubPortalCustomFields
        /// </summary>
        public string PubPortalCustomFields
        {
            get { return _strPubPortalCustomFields; }
            set { if (this._strPubPortalCustomFields != value) { _strPubPortalCustomFields = value; } }
        }

        private string _strIperFormRegKey;
        /// <summary>
        /// IperFormRegKey
        /// </summary>
        public string IperFormRegKey
        {
            get { return _strIperFormRegKey; }
            set { if (this._strIperFormRegKey != value) { _strIperFormRegKey = value; } }
        }

        private string _strYPTabRegKey;
        /// <summary>
        /// IperFormRegKey
        /// </summary>
        public string YPTabRegKey
        {
            get { return _strYPTabRegKey; }
            set { if (this._strYPTabRegKey != value) { _strYPTabRegKey = value; } }
        }

        private string _strIPerformDBName;
        /// <summary>
        /// IperFormRegKey
        /// </summary>
        public string IPerformDBName
        {
            get { return _strIPerformDBName; }
            set { if (this._strIPerformDBName != value) { _strIPerformDBName = value; } }
        }

        public new enum Method
        {
            Add,
            Update,
            Get
           
        }
        /// <summary>
        /// List method enum
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            IS_Exist_YPTAB_IperformRegKey,
            IS_Exist_YPTAB_RegKey,
            IS_Exist_IperformRegKey,
            Get_YPTAB_IperformRegKey,
        }

   }
}