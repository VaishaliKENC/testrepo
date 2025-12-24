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
    public class iPerformService
    {
        public string ID { get; set; }
        public string LoginKey { get; set; }      
        public string ExpiryTime { get; set; }
        public string ClientId { get; set; }

        public iPerformService()
        {
            ID = string.Empty;
            LoginKey = string.Empty;
            ExpiryTime = string.Empty;
            ClientId = string.Empty;
        }

        public new enum Method
        {
         
        }
       
        public new enum ListMethod
        {
            GenerateUserLoginKey,
            ValidateUserLoginKey
        }
    }
}
