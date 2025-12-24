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
namespace YPLMS2._0.API.Entity
{

   [Serializable] public class UserPageElementLanguage:BaseEntity
    {
        /// <summary>
        /// Default Contructor
        /// <summary>
        public UserPageElementLanguage()
        { }

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
            GetAll,
            GetAllElements,
            BulkUpdate,
            GelAllMultipleElements
        }


        public enum UserPagesElementType
        {
            button,
            grid,
            image,
            link,
            multiline,
            option,
            password,
            text,
            user
        }
        private string _pageElementId;
        public string PageElementId
        {
            get { return _pageElementId; }
            set { if (this._pageElementId != value) { _pageElementId = value; } }
        }

        private string _languageID;
        public string LanguageID
        {
            get { return _languageID; }
            set { if (this._languageID != value) { _languageID = value; } }
        }

        private string _elementName;
        public string ElementName
        {
            get { return _elementName; }
            set { if (this._elementName != value) { _elementName = value; } }
        }


        private string _elementText;
        public string ElementText
        {
            get { return _elementText; }
            set { if (this._elementText != value) { _elementText = value; } }
        }

        private string _strElementDisplayName;
        public string ElementDisplayName
        {
            get { return _strElementDisplayName; }
            set { if (this._strElementDisplayName != value) { _strElementDisplayName = value; } }
        }


        private bool _isImageAvailable;
        public bool IsImageAvailable
        {
            get { return _isImageAvailable; }
            set { if (this._isImageAvailable != value) { _isImageAvailable = value; } }
        }

        private bool _bIsMandatory;
        public bool IsMandatory
        {
            get { return _bIsMandatory; }
            set { if (this._bIsMandatory != value) { _bIsMandatory = value; } }
        }

        private bool _bIsReadOnly;
        public bool IsReadOnly
        {
            get { return _bIsReadOnly; }
            set { if (this._bIsReadOnly != value) { _bIsReadOnly = value; } }
        }


        private string _elementImageFileName;
        public string ElementImageFileName
        {
            get { return _elementImageFileName; }
            set { if (this._elementImageFileName != value) { _elementImageFileName = value; } }
        }

        private UserPagesElementType _strElementType;
        public UserPagesElementType ElementType
        {
            get { return _strElementType; }
            set { if (this._strElementType != value) { _strElementType = value; } }
        }
        private string _strValidationId;
        public string ValidationId
        {
            get { return _strValidationId; }
            set { if (this._strValidationId != value) { _strValidationId = value; } }
        }

        private int _iImagewidth;
        public int ImageWidth
        {
            get { return _iImagewidth; }
            set { if (this._iImagewidth != value) { _iImagewidth = value; } }
        }

        private int _iImageheight;
        public int ImageHeight
        {
            get { return _iImageheight; }
            set { if (this._iImageheight != value) { _iImageheight = value; } }
        }

        private bool _bImageoverride;
        public bool IsImageoverride
        {
            get { return _bImageoverride; }
            set { if (this._bImageoverride != value) { _bImageoverride = value; } }
        }

    }	
}
