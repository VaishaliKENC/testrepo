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
   [Serializable] public class Announcement:BaseEntity 
    {
        public Announcement()
        { }

        /// <summary>
        /// Method enum
        /// </summary>
        public new enum Method
        {
            Get,
            Add,
            Update,
            Delete,
            AddLanguage,
            DeleteLanguage
        }

        /// <summary>
        /// List method enum
        /// </summary>
        public new enum ListMethod
        {
            GetAll,
            GetAllAnnouncement,
            GetAllLearner
        }

        private string _strLanguageId;
        /// <summary>
        /// Language Id
        /// </summary>
        public string LanguageId
        {
            get { return _strLanguageId; }
            set { if (this._strLanguageId != value) { _strLanguageId = value; } }
        }
         
        private string _strTitle;
        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return _strTitle; }
            set { if (this._strTitle != value) { _strTitle = value; } }
        }

         private string _strDescription;
        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get { return _strDescription; }
            set { if (this._strDescription != value) { _strDescription = value; } }
        }


        private string _strUrl;
        /// <summary>
        /// Description
        /// </summary>
        public string URL
        {
            get { return _strUrl; }
            set { if (this._strUrl != value) { _strUrl = value; } }
        }


        private DateTime _dateStart;
        /// <summary>
        /// Start Date
        /// </summary>
        public DateTime StartDate
        {
            get { return _dateStart; }
            set { if (this._dateStart != value) { _dateStart = value; } }
        }

        private Nullable<DateTime> _dateExpiry;
        /// <summary>
         /// ExpiryDate
        /// </summary>
         public Nullable<DateTime> ExpiryDate
        {
            get { return _dateExpiry; }
            set { if (this._dateExpiry != value) { _dateExpiry = value; } }
        }

         private Nullable<bool> _bIsActive;
        /// <summary>
        /// To check Is Active
        /// </summary>
         public Nullable<bool> IsActive
        {
            get { return _bIsActive; }
            set { if (this._bIsActive != value) { _bIsActive = value; } }
        }

        private Nullable<bool> _bIsForLearner;
        /// <summary>
        /// To check Is Active
        /// </summary>
        public Nullable<bool> IsForLearner
        {
            get { return _bIsForLearner; }
            set { if (this._bIsForLearner != value) { _bIsForLearner = value; } }
        }
        
        private string _strLanguageName;
        /// <summary>
        /// Language Name
        /// </summary>
        public string LanguageName
        {
            get { return _strLanguageName; }
            set { if (this._strLanguageName != value) { _strLanguageName = value; } }
        }

        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="pIsUpdate"></param>
        /// <returns></returns>
        public bool Validate(bool pIsUpdate)
        {
            if (pIsUpdate)
            {
                if (String.IsNullOrEmpty(ID))
                    return false;
            }
            else
            {
                if (String.IsNullOrEmpty(Title))
                    return false;

                if (String.IsNullOrEmpty(CreatedById))
                    return false;
            }
            return true;
        }
    }
}
