using System;
using System.Collections;
using System.Web;
using System.Web.Caching;

namespace BlockToDB.Utils
{
    public class CacheHelper
    {
        private static CacheHelper instance;
        private readonly HttpContext context;

        private CacheHelper(HttpContext _context)
        {
            context = _context;
        }

        public static CacheHelper GetInstance()
        {
            if (instance == null)
            {
                instance = new CacheHelper(HttpContext.Current);
            }

            return instance;
        }

        public void AddToCache(object data, string key, string userId = "")
        {
            key = string.Concat(key, userId);
            if (context != null)
            {
                context.Cache.Add(
                    key,
                    data,
                    null,
                    System.Web.Caching.Cache.NoAbsoluteExpiration,
                    new TimeSpan(0, 15, 0),
                    CacheItemPriority.Normal,
                    null);
            }
        }

        public T Get<T>(string keyName, string userId = "")
        {
            keyName = string.Concat(keyName, userId);
            return context != null ? (T)context.Cache.Get(keyName) : default(T);
        }

        public T Get<T>(string keyName, Func<T> valueSetter)
        {
            return Get(keyName, null, valueSetter);
        }

        public T Get<T>(string keyName, string userId, Func<T> valueSetter)
        {
            if (context == null)
            {
                return default(T);
            }

            var keyToSearchFor = string.Concat(keyName, userId);

            var result = context.Cache.Get(keyToSearchFor);
            if (result == null && valueSetter != null)
            {
                result = valueSetter();
                AddToCache(result, keyName, userId);
            }

            return (T)result;
        }

        public void Remove(string keyName, string userId = "")
        {
            keyName = string.Concat(keyName, userId);
            if (context != null)
            {
                context.Cache.Remove(keyName);
            }
        }

        public void RemoveStartsWith(string keyStartSubstring)
        {
            if (context != null)
            {
                foreach (DictionaryEntry entry in context.Cache)
                {
                    if (entry.Key.ToString().StartsWith(keyStartSubstring))
                    {
                        context.Cache.Remove(entry.Key.ToString());
                    }
                }
            }
        }

        public void RemoveAll()
        {
            if (context != null)
            {
                foreach (DictionaryEntry entry in context.Cache)
                {
                    context.Cache.Remove(entry.Key.ToString());
                }
            }
        }
    }
}