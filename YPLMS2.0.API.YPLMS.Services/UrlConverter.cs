using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.YPLMS.Services
{
    public abstract class UrlConverter
    {
        protected UrlConverter()
        {
        }

        public HttpRequest? Request { get; set; }

        public abstract string ConvertUrl();
    }
}
