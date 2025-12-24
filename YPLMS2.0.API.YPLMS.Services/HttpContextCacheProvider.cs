using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YPLMS2._0.API.YPLMS.Services
{
    public class HttpContextCacheProvider : ICacheProvider
    {
        public void Add<T>(string key, T obj)
        {
            throw new NotImplementedException();
        }

        public void Add<T>(string key, T obj, int duration)
        {
            throw new NotImplementedException();
        }

        public void Add<T>(string key, T obj, string fileDependencyPath)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key, Func<T> retriever)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(Predicate<string> match)
        {
            throw new NotImplementedException();
        }
    }
}

