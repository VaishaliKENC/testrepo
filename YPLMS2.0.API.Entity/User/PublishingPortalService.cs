/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Encora's Client.
* Author:
* Created:
* Last Modified:<dd/mm/yy>
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class PublishingPortalService
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public string ClientId { get; set; }      
        public string ClientName { get; set; }
        public string ClientUrl { get; set; }
        public string IsAdmin { get; set; }
        public string IsApprover { get; set; }
        public string IsEditor { get; set; }
        public string IsReviewer { get; set; }
        public string IsDownload { get; set; }
        public string IsBrokenLink { get; set; }
        public string IsActive { get; set; }
        public string AuthenticationToken{ get; set; }
        public string Unassign { get; set; }
        public string LoginKey { get; set; }      
        public string ExpiryTime { get; set; }
        public string IsService { get; set; }
        public string CustomFieldIdValue { get; set; }
        public string IsExist { get; set; }
        public string CustomFields { get; set; }
        public string ExpiryDate { get; set; }
        
        public PublishingPortalService()
        {
            ID = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            EmailId = string.Empty;
            ClientId = string.Empty;
            ClientUrl = string.Empty;
            ClientName = string.Empty;

            IsAdmin = "0";
            IsApprover = "0";
            IsEditor = "0";
            IsReviewer = "0";
            IsDownload = "0";
            IsBrokenLink = "0";
            IsActive = string.Empty;
            AuthenticationToken = string.Empty;
            Unassign = string.Empty;
            LoginKey = string.Empty;
            ExpiryTime = string.Empty;
            IsService = string.Empty;
            CustomFieldIdValue = string.Empty;
            IsExist = string.Empty;
            CustomFields = string.Empty;
            ExpiryDate = string.Empty;           
        }


        /// <summary>
        /// publishing portal enum method for add,edit transactions
        /// </summary>
        public new enum Method
        {
            Get,
            Update, 
            CheckPublishingPortalAccess,
            ChangedPassword,
            UserActivateDeactivate
        }

        /// <summary>
        ///  AdminRoles ListMethod enum 
        /// </summary>
        public new enum ListMethod
        {          
            BulkUpdate, 
            GetPublishingPortalUsers,
            GetPublishingPortalUsersDeactivate,
            GenerateUserLoginKey,
            ValidateUserLoginKey            
        }

    }
}
