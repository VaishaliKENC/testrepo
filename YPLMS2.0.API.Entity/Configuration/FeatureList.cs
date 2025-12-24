/*
* Copyright Encora
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<10/30/2013>
* Last Modified:
*/

using System;

namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class FeatureList : BaseEntity 
    /// </summary>
    /// 
    public class FeatureList : BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// </summary>
        public FeatureList()
        { }

        #region CONSTANT FEATURE IDS
        //public const string CACHE_SUFFIX = "_FEATURES";

        /// <summary>
        /// Client Assesment module
        /// </summary>
        
        public const string FEA_ID_AdminContentRight = "FEA001";
        public const string FEA_ID_Blogs = "FEA002";
        public const string FEA_ID_Content = "FEA003";
        public const string FEA_ID_AutoPopulateLoginID = "FEA004";
        public const string FEA_ID_DonotAllowLoginOnNoncompatibleSystem = "FEA005";
        public const string FEA_ID_Display_eStore_on_LoginPage = "FEA006";
        public const string FEA_ID_EnableSocialMediaCommentOnMenu = "FEA007";
        public const string FEA_ID_SocialMediaCommentOnActivityCompletion = "FEA008";
        public const string FEA_ID_FavoriteList = "FEA009";
        public const string FEA_ID_Alerts = "FEA010";
        public const string FEA_ID_Share = "FEA011";
        public const string FEA_ID_Calender = "FEA017";
        public const string FEA_ID_Mobile_LMS = "CFM010";
        public const string FEA_ID_User_Expiry = "CFM013";

        public const string FEA_ID_Show_WelcomeText = "FEA012";
        public const string FEA_ID_Show_Statastic = "FEA013";

        public const string FEA_ID_Hide_Progress = "FEA022";
        public const string FEA_ID_Footer = "FEA026";
        public const string FEA_ID_TermsandCond_popup = "FEA027";
        public const string FEA_ID_Jira_Show = "FEA028";
        public const string FEA_ID_Move_QuickLink_To_Top = "FEA030";
        public const string FEA_ID_Login_popup = "FEA031";
        public const string FEA_Hide_Status_Progress_Bar = "FEA033";
        public const string FEA_ForceFul_Assignment_Default_Checked = "FEA035";
        public const string FEA_Disable_Activity_Launch_For_Completed_Activities = "FEA037";
        #endregion


        private string _strFeatureName;
        /// <summary>
        /// FeatureName
        /// </summary>
        public string FeatureName
        {
            get { return _strFeatureName; }
            set { if (this._strFeatureName != value) { _strFeatureName = value; } }
        }

        private string _strFeatureDescription;
        /// <summary>
        /// FeatureDescription
        /// </summary>
        public string FeatureDescription
        {
            get { return _strFeatureDescription; }
            set { if (this._strFeatureDescription != value) { _strFeatureDescription = value; } }
        }

        private bool _strIsActive;
        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive
        {
            get { return _strIsActive; }
            set { if (this._strIsActive != value) { _strIsActive = value; } }
        }

        private string _strValue;
        /// <summary>
        /// Value
        /// </summary>
        public string Value
        {
            get { return _strValue; }
            set { if (this._strValue != value) { _strValue = value; } }
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
            GetAll
        }
    }
}