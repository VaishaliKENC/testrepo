using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.YPLMS.Services
{
    public class UrlConverterFactory
    {
        public UrlConverter CreateUrlConverter(HttpRequest request)
        {
            return new DefaultUrlConverter((IHttpContextAccessor)request);
        }
    }
}