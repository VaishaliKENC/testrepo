/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh
* Created:<30/09/09>
* Last Modified:<30/09/09>
*/
using System;
using System.Collections.Generic;
namespace YPLMS2._0.API.Entity
{
   [Serializable] public class UserPage : BaseEntity
    {
        public const string CACHE_SUFFIX = "_Pages"; 
        /// <summary>
        /// Default Contructor
        /// <summary>
        public UserPage()
        {
            _entListElements = new List<UserPageElementLanguage>(); 
        }

        /// <summary>
        /// ENUM Method
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete
        }
        /// <summary>
        /// List method ENUM
        /// </summary>
        public new enum ListMethod
        {
            GetAll
        }

        public enum UserPagesConfigType
        {
            pages,
            common,
            webparts
        }

        private List<UserPageElementLanguage> _entListElements;
        public List<UserPageElementLanguage> PageElementLanguage
        {
            get { return _entListElements; }
        }
        private string _pageEnglishName;
        public string PageEnglishName
        {
            get { return _pageEnglishName; }
            set { if (this._pageEnglishName != value) { _pageEnglishName = value; } }
        }


        private string _pageFileURL;
        public string PageFileURL
        {
            get { return _pageFileURL; }
            set { if (this._pageFileURL != value) { _pageFileURL = value; } }
        }

       
        private string _strParaLanguageId;
        public string ParaLanguageId
        {
            get { return _strParaLanguageId; }
            set { if (this._strParaLanguageId != value) { _strParaLanguageId = value; } }
        }

        private UserPagesConfigType _strConfigType;
        public UserPagesConfigType ConfigType
        {
            get { return _strConfigType; }
            set { if (this._strConfigType != value) { _strConfigType = value; } }
        }

        private int _displayOrder;
        public int DisplayOrder
        {
            get { return _displayOrder; }
            set { if (this._displayOrder != value) { _displayOrder = value; } }
        }

    }
}
