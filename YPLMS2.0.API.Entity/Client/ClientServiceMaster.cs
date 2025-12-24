using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YPLMS2._0.API.Entity
{
    public class ClientServiceMaster
    {
        public string ClientId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public new enum Method
        {
            Get
        }

        public new enum ListMethod
        {
            Get
        }
    }
}
