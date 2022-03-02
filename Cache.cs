using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
{

}

namespace Caso1NET
{
    internal class MyCache
    {


       /* private static MemoryCache _cache = new MemoryCache("listadoCantones");

        public static object GetItem(string key)
        {
            return AddOrGetExisting(key, () => InitItem(key));
        }
        /*
        private static T AddOrGetExisting<T>(string key, Func<T> valueFactory)
        {
            var newValue = new Lazy<T>(valueFactory);
            var oldValue = _cache.AddOrGetExisting(key, newValue, new CacheItemPolicy()) as Lazy<T>;
            try
            {
                return (oldValue ?? newValue).Value;
            }
            catch
            {
                // Handle cached lazy exception by evicting from cache. Thanks to Denis Borovnev for pointing this out!
                _cache.Remove(key);
                throw;
            }
        }

        private static object InitItem(string key)
        {
            // Do something expensive to initialize item
            return new { Value = key.ToUpper() };
        }*/
    }
}
