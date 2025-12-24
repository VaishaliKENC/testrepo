/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:Fattesinh
* Created:<28/08/09>
* Last Modified:<dd/mm/yy>
*/
using System;
namespace YPLMS2._0.API.Entity
{
   [Serializable] public class FeedBack:BaseEntity
    {
        public FeedBack()
        { }

        /// <summary>
        /// enum ListMethod
        /// </summary>
        public new enum ListMethod
        {
            GetAll
        }

        /// <summary>
        /// enum Method
        /// </summary>
        public new enum Method
        {
            Add
        }

        private string _strUserId;
        /// <summary>
        /// User Id
        /// </summary>
        public string UserId
        {
            get { return _strUserId; }
            set { if (this._strUserId != value) { _strUserId = value; } }
        }     

        private string _strName;
        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get { return _strName; }
            set { if (this._strName != value) { _strName = value; } }
        }

        private string _strEmail;
        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get { return _strEmail; }
            set { if (this._strEmail != value) { _strEmail = value; } }
        }

        private string _strComments;
        /// <summary>
        /// Comments
        /// </summary>
        public string Comments
        {
            get { return _strComments; }
            set { if (this._strComments != value) { _strComments = value; } }
        }
    }
}