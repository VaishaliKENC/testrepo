/* 
* Copyright Encora
* This source file and source code is proprietary property of Encora 
* Any copying, reproduction, distribution, modification and/or reverse-engineering of any part
* or in whole of this source file and source code is prohibited.
* The above holds true, unless defined otherwise in writing in the Contract for this work with
* Rapidel Client.
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
    //added by  ...10/12/2015 
    public class RapidelUserService
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public string ClientId { get; set; }
        public string ClientUrl { get; set; }
        public string ClientName { get; set; }
        public string AuthenticationToken { get; set; }

        public string Unassign { get; set; }
        public string LoginKey { get; set; }
        //public int ExpiryTime { get; set; }
        public string  ExpiryTime { get; set; }
        public string IsRapidel { get; set; }
        public string ExpiryDate { get; set; }

        public string RuleId { get; set; }
        public string  IsExist { get; set; }
        public string IsActive { get; set; }    
        public RapidelUserService()
        {
             ID =string.Empty;
             FirstName =string.Empty;
             LastName =string.Empty;
             Username=string.Empty;
             Password =string.Empty;
             EmailId =string.Empty;
             ClientId =string.Empty;
             ClientUrl =string.Empty;
             ClientName=string.Empty;
             AuthenticationToken=string.Empty;

             Unassign =string.Empty;
             LoginKey =string.Empty;

             ExpiryTime=string.Empty;
             IsRapidel = string.Empty;
             ExpiryDate = string.Empty;
             RuleId = string.Empty;
             IsExist = string.Empty;
             IsActive = string.Empty;
        }

        /// <summary>
        /// Admin Role enum method for add,edit,delete transactions
        /// </summary>
        public new enum Method
        {
            Get,
            Update,
            GetSystemUserGuid,
            CheckRapidelAccess, 
            CheckIsHttpsAllowed,
            ChangedPassword,
            UserActivateDeactivate
        }

        /// <summary>
        ///  AdminRoles ListMethod enum 
        /// </summary>
        public new enum ListMethod
        {
            GetAllRapidelUsers,
            GetAllRapidelUsersForDel,
            BulkUpdate,
            BulkDelete,
            GenerateUserLoginKey,
            ValidateUserLoginKey,
            GetAllUsersByRoleId,
            GetAllUsersByRoleIdForDel,
            

        }

        ///// <summary>
        ///// list of AdminRoleFeatures
        ///// </summary>
        //public List<RapidelUserService> RapidelUserServiceList
        //{
        //    get { return _entRapidelUserlist; }
        //}


    }

}
