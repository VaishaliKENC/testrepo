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
namespace YPLMS2._0.API.Entity
{
    /// <summary>
    /// class ThemeLanguages:BaseEntity 
    /// </summary>
   [Serializable] public class ThemeLanguages : BaseEntity
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ThemeLanguages()
        {

        }
        public new enum ListMethod
        {
            GetThemeLanguageList = 0,
            AddAll = 1,
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
       

       
        private string _strThemeId;
        /// <summary>
        /// Theme Id
        /// </summary>
        public string ThemeId
        {
            get { return _strThemeId; }
            set { if (this._strThemeId != value) { _strThemeId = value; } }
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

        private string _strCSSFileName1;
        /// <summary>
        /// CSS File Name 1
        /// </summary>
        public string CSSFileName1
        {
            get { return _strCSSFileName1; }
            set { if (this._strCSSFileName1 != value) { _strCSSFileName1 = value; } }
        }

        private string _strCSSFileName2;
        /// <summary>
        /// CSS File Name 2
        /// </summary>
        public string CSSFileName2
        {
            get { return _strCSSFileName2; }
            set { if (this._strCSSFileName2 != value) { _strCSSFileName2 = value; } }
        }

        private string _strCSSFileName3;
        /// <summary>
        /// CSS File Name 3
        /// </summary>
        public string CSSFileName3
        {
            get { return _strCSSFileName3; }
            set { if (this._strCSSFileName3 != value) { _strCSSFileName3 = value; } }
        }


        private string _strCSSFileName4;
        /// <summary>
        /// CSS File Name 4
        /// </summary>
        public string CSSFileName4
        {
            get { return _strCSSFileName4; }
            set { if (this._strCSSFileName4 != value) { _strCSSFileName4 = value; } }
        }

        private string _strCSSFileName5;
        /// <summary>
        /// CSS File Name 5
        /// </summary>
        public string CSSFileName5
        {
            get { return _strCSSFileName5; }
            set { if (this._strCSSFileName5 != value) { _strCSSFileName5 = value; } }
        }

        private string _strCSSFileName6;
        /// <summary>
        /// CSS File Name 6
        /// </summary>
        public string CSSFileName6
        {
            get { return _strCSSFileName6; }
            set { if (this._strCSSFileName6 != value) { _strCSSFileName6 = value; } }
        }

        private string _strCSSFileName7;
        /// <summary>
        /// CSS File Name 7
        /// </summary>
        public string CSSFileName7
        {
            get { return _strCSSFileName7; }
            set { if (this._strCSSFileName7 != value) { _strCSSFileName7 = value; } }
        }

        private string _strCSSFileName8;
        /// <summary>
        /// CSS File Name 8
        /// </summary>
        public string CSSFileName8
        {
            get { return _strCSSFileName8; }
            set { if (this._strCSSFileName8 != value) { _strCSSFileName8 = value; } }
        }

        private string _strCSSFileName9;
        /// <summary>
        /// CSS File Name 9
        /// </summary>
        public string CSSFileName9
        {
            get { return _strCSSFileName9; }
            set { if (this._strCSSFileName9 != value) { _strCSSFileName9 = value; } }
        }

        private string _strCSSFileName10;
        /// <summary>
        /// CSS File Name 9
        /// </summary>
        public string CSSFileName10
        {
            get { return _strCSSFileName10; }
            set { if (this._strCSSFileName10 != value) { _strCSSFileName10 = value; } }
        }

        private string _strCSSFileName11;
        /// <summary>
        /// CSS File Name 11
        /// </summary>
        public string CSSFileName11
        {
            get { return _strCSSFileName11; }
            set { if (this._strCSSFileName11 != value) { _strCSSFileName11 = value; } }
        }

        private string _strCSSFileName12;
        /// <summary>
        /// CSS File Name 12
        /// </summary>
        public string CSSFileName12
        {
            get { return _strCSSFileName12; }
            set { if (this._strCSSFileName12 != value) { _strCSSFileName12 = value; } }
        }

        private string _strCSSFileName13;
        /// <summary>
        /// CSS File Name 13
        /// </summary>
        public string CSSFileName13
        {
            get { return _strCSSFileName13; }
            set { if (this._strCSSFileName13 != value) { _strCSSFileName13 = value; } }
        }


        private string _strCSSFileName14;
        /// <summary>
        /// CSS File Name 14
        /// </summary>
        public string CSSFileName14
        {
            get { return _strCSSFileName14; }
            set { if (this._strCSSFileName14 != value) { _strCSSFileName14 = value; } }
        }

        private string _strCSSFileName15;
        /// <summary>
        /// CSS File Name 15
        /// </summary>
        public string CSSFileName15
        {
            get { return _strCSSFileName15; }
            set { if (this._strCSSFileName15 != value) { _strCSSFileName15 = value; } }
        }

        private string _strCSSFileName16;
        /// <summary>
        /// CSS File Name 16
        /// </summary>
        public string CSSFileName16
        {
            get { return _strCSSFileName16; }
            set { if (this._strCSSFileName16 != value) { _strCSSFileName16 = value; } }
        }

        private string _strCSSFileName17;
        /// <summary>
        /// CSS File Name 17
        /// </summary>
        public string CSSFileName17
        {
            get { return _strCSSFileName17; }
            set { if (this._strCSSFileName17 != value) { _strCSSFileName17 = value; } }
        }

        private string _strCSSFileName18;
        /// <summary>
        /// CSS File Name 18
        /// </summary>
        public string CSSFileName18
        {
            get { return _strCSSFileName18; }
            set { if (this._strCSSFileName18 != value) { _strCSSFileName18 = value; } }
        }

        private string _strCSSFileName19;
        /// <summary>
        /// CSS File Name 19
        /// </summary>
        public string CSSFileName19
        {
            get { return _strCSSFileName19; }
            set { if (this._strCSSFileName19 != value) { _strCSSFileName19 = value; } }
        }

        private string _strCSSFileName20;
        /// <summary>
        /// CSS File Name 20
        /// </summary>
        public string CSSFileName20
        {
            get { return _strCSSFileName20; }
            set { if (this._strCSSFileName20 != value) { _strCSSFileName20 = value; } }
        }

        public bool Validate(bool pIsUpdate)
        {

            if (String.IsNullOrEmpty(ThemeId))
                return false;

            if (String.IsNullOrEmpty(LanguageId))
                return false;

            return true;
        }
    }
}