/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh 
* Created:<27/08/09>
* Last Modified:<27/08/09>
*/
using System;

namespace YPLMS2._0.API.Entity
{
   [Serializable] public class LanguageCode :BaseEntity 
    {
        public LanguageCode()
        { }

        /// <summary>
        /// Method enum
        /// </summary>
        public new enum Method
        {
            Get,
        }

        /// <summary>
        /// List method enum
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllLanguageCode
        }

        private string _strLongName;
        /// <summary>
        /// Language Id
        /// </summary>
        public string LongName
        {
            get { return _strLongName; }
            set { if (this._strLongName != value) { _strLongName = value; } }
        }


        private string _strShortName;
        /// <summary>
        /// Language Id
        /// </summary>
        public string ShortName
        {
            get { return _strShortName; }
            set { if (this._strShortName != value) { _strShortName = value; } }
        }       
    }
}
