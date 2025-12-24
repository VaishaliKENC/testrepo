using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.YPLMS.Services
{
    public class DefaultUrlConverter : UrlConverter
    {
        public DefaultUrlConverter(IHttpContextAccessor accessor)
        {
            Request = accessor.HttpContext?.Request;
        }

        public override string ConvertUrl()
        {
            return Request?.Path.ToString() ?? string.Empty;
        }
    }

}