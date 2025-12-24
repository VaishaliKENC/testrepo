/*
* Copyright Encora* 
* This source file and source code is proprietary property of Encora
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora’s Client.
* Author:
* Created:<12/27/2011>
* Last Modified:
*/


using System;

namespace YPLMS2._0.API.Entity
{
    public class BrandImage:BaseEntity
    {
        public BrandImage()
        {
        }
       
        private string _strImageName;
        /// <summary>
        /// Image Name
        /// </summary>
        public string ImageName
        {
            get { return _strImageName; }
            set { if (this._strImageName != value) { _strImageName = value; } }
        }

        private string _strImagePosition;
        /// <summary>
        ///Image's position
        /// </summary>
        public string ImagePosition
        {
            get { return _strImagePosition; }
            set { if (this._strImagePosition != value) { _strImagePosition = value; } }
        }

        private bool _strIsActive;
        /// <summary>
        ///Image's position
        /// </summary>
        public bool IsActive
        {
            get { return _strIsActive; }
            set { if (this._strIsActive != value) { _strIsActive = value; } }
        }

        private bool _strIsDisplay;
        /// <summary>
        ///Image's position
        /// </summary>
        public bool IsDisplay
        {
            get { return _strIsDisplay; }
            set { if (this._strIsDisplay != value) { _strIsDisplay = value; } }
        }


        public new enum Method
        {
            Get,
            Add,
            Delete
        }

        public new enum ListMethod
        {
            GetAll
        }
    }
}
